using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.Technical;
using SMS.Business.Crew;

public partial class Crew_Libraries_Service_Types : System.Web.UI.Page
{
    BLL_Crew_Admin objBLL = new BLL_Crew_Admin();

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

            BindJoiningType();
        }

    }



    public void BindJoiningType()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.ServiceType_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        gvJoiningType.DataSource = dt;
        gvJoiningType.DataBind();
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            ImgAdd.Visible = false;
        }
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
        this.SetFocus("ctl00_MainContent_txtJoiningType");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Service Type";

        ClearField();

        string JoiningType = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "JoiningType", JoiningType, true);
    }

    protected void ClearField()
    {
        txtJoiningTypeID.Text = "";
        txtJoiningType.Text = "";
        txtJoiningCode.Text = "";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        string strMsg="";
        if (HiddenFlag.Value == "Add")
        {
            bool iInsert = true;
            foreach (GridViewRow row in gvJoiningType.Rows)
            {
                if ((row.FindControl("lblJCode") as Label).Text.Trim() == txtJoiningCode.Text.Trim() || (row.FindControl("lblService_Type") as LinkButton).Text.Trim() == txtJoiningType.Text.Trim())
                {
                    iInsert = false;
                    break;
                }
            }

            if (iInsert == true)
            {
                int retval = objBLL.InsertServiceType(txtJoiningType.Text.Trim(), txtJoiningCode.Text.Trim(), chkSeniorityConsidered.Checked, Convert.ToInt32(Session["USERID"]));
            }
            else
            {
                strMsg = "Service type or Code already exists.";
            }
        }
        else
        {
            bool iEdit = true;
            foreach (GridViewRow row in gvJoiningType.Rows)
            {
                if (((row.FindControl("lblJCode") as Label).Text.Trim() == txtJoiningCode.Text.Trim() || (row.FindControl("lblService_Type") as LinkButton).Text.Trim() == txtJoiningType.Text.Trim()) && HiddenID.Value.ToString().Trim() !=(row.FindControl("hdnID") as HiddenField).Value.ToString().Trim())
                {
                    iEdit = false;
                    break;
                }
            }

            if (iEdit == true)
            {
                int retval = objBLL.EditServiceType(Convert.ToInt32(txtJoiningTypeID.Text.Trim()), txtJoiningType.Text.Trim(), txtJoiningCode.Text.Trim(), chkSeniorityConsidered.Checked, Convert.ToInt32(Session["USERID"]));
            }
            else
            {
                strMsg = "Service type or Code already exists.";
            }
        }

        BindJoiningType();
        if(strMsg=="")
        {
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
        else
        {
            string hidemodal = String.Format("alert('"+ strMsg +"');showModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Service Type";
        HiddenID.Value = e.CommandArgument.ToString();
        DataTable dt = new DataTable();
        dt = objBLL.Get_ServiceType_List(Convert.ToInt32(e.CommandArgument.ToString()));


        txtJoiningTypeID.Text = dt.Rows[0]["ID"].ToString();
        txtJoiningType.Text = dt.Rows[0]["Service_Type"].ToString();
        txtJoiningCode.Text = dt.Rows[0]["SCode"].ToString();
       chkSeniorityConsidered.Checked = dt.Rows[0]["SeniorityConsidered"].ToString() == "True" ? true : false;


        string InfoDiv = "Get_Record_Information_Details('CRW_LIB_SERVICETYPES','ID=" + txtJoiningTypeID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);


        string AddSingoffReasonmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSingoffReasonmodal", AddSingoffReasonmodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteServiceType(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));

        BindJoiningType();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindJoiningType();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        BindJoiningType();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.ServiceType_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
            , null, null, ref  rowcount);



        string[] HeaderCaptions = { "Service Type", "Service Code" };
        string[] DataColumnsName = { "Service_Type", "SCode", };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "ServiceType", "Service Type", "");

    }

    protected void gvJoiningType_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvJoiningType_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindJoiningType();
    }
}