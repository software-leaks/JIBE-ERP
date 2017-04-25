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
using SMS.Business.QMS;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class QMS_SCM_SCM_DrillTypes : System.Web.UI.Page
{
    BLL_SCM_DrillType objBLL = new BLL_SCM_DrillType();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    BLL_Infra_VesselLib objVBLL = new BLL_Infra_VesselLib();
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
            Load_VesselList();
            BindDrillType();
        }

    }

    public void BindDrillType()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        //int? isfavorite = null; if (ddlFavorite.SelectedValue != "2") isfavorite = Convert.ToInt32(ddlFavorite.SelectedValue.ToString());
        //int? countrycode = null; if (ddlSearchCountry.SelectedValue != "0") countrycode = Convert.ToInt32(ddlSearchCountry.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.SearchDrillType(UDFLib.ConvertIntegerToNull(ddlVessel_Name.SelectedValue), UDFLib.ConvertStringToNull(ddlDrill_Name.SelectedValue), sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvWorkListAccess.DataSource = dt;
            gvWorkListAccess.DataBind();
        }
        else
        {
            gvWorkListAccess.DataSource = dt;
            gvWorkListAccess.DataBind();
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

        if (objUA.Add == 0) ImgAdd.Visible = false;
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

    protected void Load_VesselList()
    {
        DataTable dt = objVBLL.Get_VesselList(0, 0, 0, "", 1);

        ddlVessel_Name.DataSource = dt;
        ddlVessel_Name.DataTextField = "VESSEL_NAME";
        ddlVessel_Name.DataValueField = "VESSEL_ID";
        ddlVessel_Name.DataBind();

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_ddlVessel");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Drill Type";
        lblMsg.Text = "";
        ddlVessel.SelectedValue = "0";
        ddlDrillName.SelectedValue = "0";
        ddlFrequency.SelectedValue = "0";

        string AddDeptmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddDeptmodal", AddDeptmodal, true);
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        int responseid = 0;
        if (HiddenFlag.Value == "Add")
        {
            responseid = objBLL.InsertDrillType(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlDrillName.SelectedValue), UDFLib.ConvertIntegerToNull(ddlFrequency.SelectedValue), Convert.ToInt32(Session["USERID"]));

        }
        else
        {
            responseid = objBLL.EditDrillType(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertStringToNull(ddlDrillName.SelectedValue), UDFLib.ConvertIntegerToNull(ddlFrequency.SelectedValue), int.Parse(txtDrillID.Text), Convert.ToInt32(Session["USERID"]));

        }
        if (responseid > 0)
        {
            BindDrillType();
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
        else
        {
            lblMsg.Text = "Vessel Name And Drill Name Already Exists!";
            string AddDeptmodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddDeptmodal", AddDeptmodal, true);
        }

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Drill Type";
        lblMsg.Text = "";
        DataTable dt = new DataTable();
        dt = objBLL.Get_DrillTypeList(Convert.ToInt32(e.CommandArgument.ToString()));
        txtDrillID.Text = e.CommandArgument.ToString();
        ddlVessel.SelectedValue = dt.Rows[0]["VESSEL_ID"].ToString();
        ddlDrillName.SelectedValue = dt.Rows[0]["DRILL_NAME"].ToString();
        ddlFrequency.SelectedValue = dt.Rows[0]["FREQUENCY"].ToString();

        string Deptmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Deptmodal", Deptmodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteDrillType(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindDrillType();


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindDrillType();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {

        ddlVessel_Name.SelectedValue = "0";
        ddlDrill_Name.SelectedValue = "0";

        BindDrillType();

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        //int? isfavorite = null; if (ddlFavorite.SelectedValue != "2") isfavorite = Convert.ToInt32(ddlFavorite.SelectedValue.ToString());
        //int? countrycode = null; if (ddlSearchCountry.SelectedValue != "0") countrycode = Convert.ToInt32(ddlSearchCountry.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.SearchDrillType(UDFLib.ConvertIntegerToNull(ddlVessel_Name.SelectedValue), UDFLib.ConvertStringToNull(ddlDrill_Name.SelectedValue), sortbycoloumn, sortdirection
             , null, null, ref  rowcount);

        string[] HeaderCaptions = { "Vessel Name", "Drill Name", "Frequency" };
        string[] DataColumnsName = { "Vessel_Name", "DRILL_NAME", "FREQUENCY" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Drill Type", "Drill Type", "");

    }


    protected void gvWorkListAccess_RowDataBound(object sender, GridViewRowEventArgs e)
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


    protected void gvWorkListAccess_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindDrillType();
    }
}