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


public partial class ShipHelpFileSettings : System.Web.UI.Page
{


    BLL_Infra_VesselFlag objBLL = new BLL_Infra_VesselFlag();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    BLL_Infra_Company objBLLComp = new BLL_Infra_Company();
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
            FillScreenDLL();

            BindHelpFileSettings();
        }

    }

 
    public void FillScreenDLL()
    {
        DataTable dt = BLL_Infra_ShipSettings.Get_Module_Screens();

        ddlScreenFilter.DataTextField = "Class_Name";
        ddlScreenFilter.DataValueField = "Screen_ID";
        ddlScreenFilter.DataSource = dt;
        ddlScreenFilter.DataBind();
        ddlScreenFilter.Items.Insert(0, new ListItem("-SELECT-", "0"));


        ddlScreen.DataSource = dt;
        ddlScreen.DataTextField = "Class_Name";
        ddlScreen.DataValueField = "Screen_ID";
        ddlScreen.DataBind();
        ddlScreen.Items.Insert(0, new ListItem("-ALL-", "0"));

    }

    public void BindHelpFileSettings()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_Infra_ShipSettings.Get_Help_File_Settings_Search(txtfilter.Text != "" ? txtfilter.Text : null
                                        , UDFLib.ConvertIntegerToNull(ddlScreenFilter.SelectedValue),null, sortbycoloumn, sortdirection
                                        , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvFileSettings.DataSource = dt;
            gvFileSettings.DataBind();
        }
        else
        {
            gvFileSettings.DataSource = dt;
            gvFileSettings.DataBind();
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

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_ddlScreen");
        HiddenFlag.Value = "Add";
        OperationMode = "Add File settings";

        ClearField();

        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }

    protected void ClearField()
    {
        ddlScreen.SelectedValue = "0";
        txtHelpFileName.Text = "";
        txtTopicID.Text = "";
        txtTopicDesc.Text = "";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (HiddenFlag.Value == "Add")
        {
            int retval = BLL_Infra_ShipSettings.INS_Help_File_Settings(UDFLib.ConvertToInteger(ddlScreen.SelectedValue),txtTopicID.Text,txtTopicDesc.Text
                , txtHelpFileName.Text, UDFLib.ConvertToInteger(Session["USERID"]));
        }
        else
        {
            int retval = BLL_Infra_ShipSettings.UPD_Help_File_Settings(Convert.ToInt32(txtID.Text.Trim()), UDFLib.ConvertToInteger(ddlScreen.SelectedValue), txtTopicID.Text, txtTopicDesc.Text
                , txtHelpFileName.Text, UDFLib.ConvertToInteger(Session["USERID"]));
        }

        BindHelpFileSettings();
        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit File settings";

        DataTable dt = new DataTable();
        dt = BLL_Infra_ShipSettings.Get_Help_File_Settings_List(Convert.ToInt32(e.CommandArgument.ToString()));

        txtID.Text = dt.Rows[0]["ID"].ToString();
        ddlScreen.SelectedValue = dt.Rows[0]["Screen_ID"].ToString();
        txtHelpFileName.Text = dt.Rows[0]["Help_File_Name"].ToString();
        txtTopicID.Text = dt.Rows[0]["Topic_ID"].ToString();
        txtTopicDesc.Text = dt.Rows[0]["Topic_Description"].ToString();
         

        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = BLL_Infra_ShipSettings.DEL_Help_File_Settings(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));

        BindHelpFileSettings();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindHelpFileSettings();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        ddlScreenFilter.SelectedValue = "0";

        BindHelpFileSettings();

    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_Infra_ShipSettings.Get_Help_File_Settings_Search(txtfilter.Text != "" ? txtfilter.Text : null
                                 , UDFLib.ConvertIntegerToNull(ddlScreenFilter.SelectedValue), null, sortbycoloumn, sortdirection
                                 , null, null, ref  rowcount);



        string[] HeaderCaptions = { "Screen", "Class", "Topic" , "Topic Description" , "Help File Name" };
        string[] DataColumnsName = { "Screen_Name", "Class_Name", "Topic_ID", "Topic_Description", "Help_File_Name" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Help_File_Settings", "Help File Settings", "");

    }

    protected void gvFileSettings_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvFileSettings_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindHelpFileSettings();
    }

}
