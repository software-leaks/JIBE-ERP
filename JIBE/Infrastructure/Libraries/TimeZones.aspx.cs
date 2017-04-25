using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class TimeZones : System.Web.UI.Page
{
    BLL_Infra_TimeZones objBLL = new BLL_Infra_TimeZones();    
    UserAccess objUA = new UserAccess();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

            Load_TimeZones();
        }

    }

    public void Load_TimeZones()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
      
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.Get_TimeZoneList(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        
        
        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvTimeZone.DataSource = dt;
            gvTimeZone.DataBind();
            ImgAdd.Visible = false;
        }
        else
        {
            gvTimeZone.DataSource = null;
            gvTimeZone.DataBind();
            ImgAdd.Visible = false;
        }
        

    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnsave.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

   

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        objBLL.PopulateTimeZones(GetSessionUserID());
        Load_TimeZones();
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (HiddenFlag.Value == "Add")
        {
            int responseid = objBLL.InsertTimeZone(txtTimeZone.Text.Trim(),Convert.ToInt32(Session["USERID"]));

        }
        else
        {
            int responseid = objBLL.EditTimeZone(Convert.ToInt32(txtTimeZoneID.Text), txtTimeZone.Text,txtBaseUtc.Text, chkDefaultTimeZone.Checked == true ? 1 : 0,Convert.ToInt32(Session["USERID"]));

        }

        Load_TimeZones();
        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Time Zone";

        DataTable dt = new DataTable();
        dt = objBLL.Get_TimeZoneList(Convert.ToInt32(e.CommandArgument.ToString()));
        dt.DefaultView.RowFilter = "ID= '" + e.CommandArgument.ToString() + "'";

        txtTimeZoneID.Text = dt.DefaultView[0]["ID"].ToString();
        txtTimeZone.Text = dt.DefaultView[0]["DisplayName"].ToString();
        txtBaseUtc.Text = Convert.ToString(dt.DefaultView[0]["BaseUtcOffSet"]);
        chkDefaultTimeZone.Checked = dt.DefaultView[0]["DefaultTimeZone"].ToString() == "0" ? false : true; 
        
        string AddTimeZonemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddTimeZonemodal", AddTimeZonemodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteTimeZone(Convert.ToInt32(e.CommandArgument.ToString()),Convert.ToInt32(Session["USERID"]));
        Load_TimeZones();


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        Load_TimeZones();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";


        Load_TimeZones();

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


         DataTable dt = objBLL.Get_TimeZoneList(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , null, null, ref  rowcount);

         string[] HeaderCaptions = { "ID", "Time Zone", "Time Zone Display", "Base UTC Offset" };
         string[] DataColumnsName = { "ID", "TimeZone", "DisplayName", "BaseUtcOffSet" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "TimeZone", "TimeZone", "");

    }


    protected void gvTimeZone_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        }

    }


    protected void gvTimeZone_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        Load_TimeZones();
    }
}