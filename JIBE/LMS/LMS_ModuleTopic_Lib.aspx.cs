using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Properties;
using SMS.Business.Infrastructure;
using SMS.Business.LMS;
using System.Data;
using SMS.Business.FAQ;

public partial class LMS_LMS_ModuleTopic_Lib : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPager1.PageSize = 15;

            //BindfltModule();
            BindModuleGrid();
            BindTopicGrid();
            //string Companymodal = String.Format("DivShow('Topic');");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Companymodal", Companymodal, true);
        }
        //string Mode;
        //if (rdoStatus.SelectedValue == "1")
        //{
        //    lblfltModule.Visible = true;
        //    ddlfltModule.Visible = true;
           
        //    Mode = "Topic";
        //}
        //else
        //{
        //    lblfltModule.Visible = false;
        //    ddlfltModule.Visible = false;
            
        //    Mode = "Module";
        //}
        //string Companymodal1 = String.Format("DivShow('" + Mode + "');");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Companymodal1", Companymodal1, true);
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0) { ImgAdd.Visible = false; ImageButton1.Visible = false; }
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
    protected void BindfltModule()
    {
        //ddlfltModule.Items.Clear();
        //ddlfltModule.DataTextField = "Module_Description";
        //ddlfltModule.DataValueField = "Module_ID";
        //ddlfltModule.DataSource = BLL_LMS_FAQ.Get_FAQModule_List().Tables[0];
        //ddlfltModule.DataBind();
        //ListItem li = new ListItem("--All--", "0");
        //ddlfltModule.Items.Insert(0, li);
    }
    protected void BindModule()
    {
        ddlModule.Items.Clear();
        ddlModule.DataTextField = "Module_Description";
        ddlModule.DataValueField = "Module_ID";
        ddlModule.DataSource = BLL_FAQ_Item.Get_FAQModule_List().Tables[0];
        ddlModule.DataBind();
        ListItem li = new ListItem("--Select--", "0");
        ddlModule.Items.Insert(0, li);
    }
    public void BindModuleGrid()
    {
        int rowcount = ucCustomPager1.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_FAQ_Item.Get_ModuleTopic_Details(""
          , ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize,"Module",null ,null, ref  rowcount).Tables[0];

        if (ucCustomPager1.isCountRecord == 1)
        {
            ucCustomPager1.CountTotalRec = rowcount.ToString();
            ucCustomPager1.BuildPager();
        }

        grdModule.DataSource = dt;
        grdModule.DataBind();
       
    }
    public void BindTopicGrid()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        //DataTable dt = BLL_LMS_FAQ.Get_ModuleTopic_Details(txtfilter.Text != "" ? txtfilter.Text : null
        //  , ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, "Topic", null, UDFLib.ConvertIntegerToNull(ddlfltModule.SelectedValue), ref  rowcount).Tables[0];
        DataTable dt = BLL_FAQ_Item.Get_ModuleTopic_Details(""
          , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, "Topic", null, UDFLib.ConvertIntegerToNull(hdnModuleID.Value), ref  rowcount).Tables[0];

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        grdTopic.DataSource = dt;
        grdTopic.DataBind();

    }
    protected void btnAddTopic_Click(object sender, EventArgs e)
    {
        HiddenFlag.Value = "Add";
        //if (rdoStatus.SelectedValue == "1")
        //{
            OperationMode = "Add Topic";
            lblName.Text = "Topic Name :";
            ddlModule.Visible = true;
            lblModule.Visible = true;
            td_Relation.Visible = true;
            BindModule();
            ddlModule.SelectedValue = "0";
            hdnMode.Value = "Topic";
        //}
        //else
        //{
        //    OperationMode = "Add Module";
        //    lblName.Text = "Module Name :";
        //    ddlModule.Visible = false;
        //    lblModule.Visible = false;
        //    td_Relation.Visible = false;
        //    hdnMode.Value = "Module";
        //}
        txtName.Text = "";

        string AddTopicmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddTopicmodal", AddTopicmodal, true);
    }
    protected void btnAddModule_Click(object sender, EventArgs e)
    {
        HiddenFlag.Value = "Add";

        OperationMode = "Add Module";
        lblName.Text = "Module Name :";
        ddlModule.Visible = false;
        lblModule.Visible = false;
        td_Relation.Visible = false;
        hdnMode.Value = "Module";
       
        txtName.Text = "";

        string AddModulemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddModulemodal", AddModulemodal, true);
    }
    protected void onUpdateModule(object source, CommandEventArgs e)
    {
        int rowcount = ucCustomPager1.isCountRecord;
        HiddenFlag.Value = "Edit";
        //if (rdoStatus.SelectedValue == "1")
        //{
        //    OperationMode = "Edit Topic";
        //    lblName.Text = "Topic Name :";
        //    ddlModule.Visible = true;
        //    lblModule.Visible = true;
        //    td_Relation.Visible = true;
        //    BindModule();
        //    DataTable dt = new DataTable();
        //    dt = BLL_LMS_FAQ.Get_ModuleTopic_Details("",null, null, "Topic",Convert.ToInt32(e.CommandArgument.ToString()),null,ref rowcount).Tables[0];
            
        //    if (ddlModule.Items.FindByValue(dt.DefaultView[0]["Module_ID"].ToString()) != null)
        //        ddlModule.SelectedValue = dt.DefaultView[0]["Module_ID"].ToString() != "" ? dt.DefaultView[0]["Module_ID"].ToString() : "0";
        //    else
        //        ddlModule.SelectedValue = "0";
        //    txtName.Text = dt.DefaultView[0]["Description"].ToString();
        //    txtID.Text = dt.DefaultView[0]["Topic_ID"].ToString();
        //    hdnMode.Value = "Topic";
        //}
        //else
        //{
            OperationMode = "Edit Module";
            lblName.Text = "Module Name :";
            ddlModule.Visible = false;
            lblModule.Visible = false;
            td_Relation.Visible = false;
            DataTable dt = new DataTable();
            dt = BLL_FAQ_Item.Get_ModuleTopic_Details("", null, null, "Module", Convert.ToInt32(e.CommandArgument.ToString()), null, ref rowcount).Tables[0];
            txtName.Text = dt.DefaultView[0]["Module_Description"].ToString();
            txtID.Text = dt.DefaultView[0]["Module_ID"].ToString();
            hdnMode.Value = "Module";
        //}

        string UpdateCompmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "UpdateCompmodal", UpdateCompmodal, true);
    }
    protected void onUpdateTopic(object source, CommandEventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
        HiddenFlag.Value = "Edit";

        OperationMode = "Edit Topic";
        lblName.Text = "Topic Name :";
        ddlModule.Visible = true;
        lblModule.Visible = true;
        td_Relation.Visible = true;
        BindModule();
        DataTable dt = new DataTable();
        dt = BLL_FAQ_Item.Get_ModuleTopic_Details("", null, null, "Topic", Convert.ToInt32(e.CommandArgument.ToString()), null, ref rowcount).Tables[0];

        if (ddlModule.Items.FindByValue(dt.DefaultView[0]["Module_ID"].ToString()) != null)
            ddlModule.SelectedValue = dt.DefaultView[0]["Module_ID"].ToString() != "" ? dt.DefaultView[0]["Module_ID"].ToString() : "0";
        else
            ddlModule.SelectedValue = "0";
        txtName.Text = dt.DefaultView[0]["Description"].ToString();
        txtID.Text = dt.DefaultView[0]["Topic_ID"].ToString();
        hdnMode.Value = "Topic";
       

        string UpdateCompmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "UpdateCompmodal", UpdateCompmodal, true);
    }
    protected void onDeleteModule(object source, CommandEventArgs e)
    {
        //string Mode;
        //if (rdoStatus.SelectedValue == "1")
        //    Mode = "Topic";
        //else
         //   Mode = "Module";

       // BLL_LMS_FAQ.Del_ModuleTopic(Mode, Convert.ToInt32(e.CommandArgument.ToString()), UDFLib.ConvertToInteger(Session["UserID"]));
       // BindGrid();
        BLL_FAQ_Item.Del_ModuleTopic("Module", Convert.ToInt32(e.CommandArgument.ToString()), UDFLib.ConvertToInteger(Session["UserID"]));
        BindModuleGrid();
        BindTopicGrid();
    }
    protected void onDeleteTopic(object source, CommandEventArgs e)
    {

        BLL_FAQ_Item.Del_ModuleTopic("Topic", Convert.ToInt32(e.CommandArgument.ToString()), UDFLib.ConvertToInteger(Session["UserID"]));
        BindTopicGrid();
    }
    //public void BindGrid()
    //{
    //    string Mode;
    //    if (rdoStatus.SelectedValue == "1")
    //    {
    //        lblfltModule.Visible = true;
    //        ddlfltModule.Visible = true;
    //        BindTopicGrid();
    //        Mode="Topic";
    //    }
    //    else
    //    {
    //        lblfltModule.Visible = false;
    //        ddlfltModule.Visible = false;
    //        BindModuleGrid();
    //        Mode = "Module";
    //    }
    //    string Companymodal = String.Format("DivShow('" + Mode + "');");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Companymodal", Companymodal, true);
    //}
    //protected void btnFilter_Click(object sender, EventArgs e)
    //{
    //    BindGrid();
    //}
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        //txtfilter.Text = "";
        //ddlfltModule.SelectedValue = "0";

        //BindGrid();
        hdnModuleID.Value ="";
        BindTopicGrid();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (HiddenFlag.Value == "Add")
        {
            if (hdnMode.Value == "Topic")
            {
                int responseid = BLL_FAQ_Item.Update_ModuleTopic(txtName.Text.Trim(), null, "Topic", Convert.ToInt32(ddlModule.SelectedValue), UDFLib.ConvertToInteger(Session["UserID"]));
                BindTopicGrid();

                //if (responseid == -1)
                //{
                //    string js1 = "alert('Topic name already exists');";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);
                //}
            }
            else
            {
                int responseid = BLL_FAQ_Item.Update_ModuleTopic(txtName.Text.Trim(), null, "Module", null, UDFLib.ConvertToInteger(Session["UserID"]));
                BindModuleGrid();

                //if (responseid == -1)
                //{
                //    string js1 = "alert('Topic name already exists');";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);
                //}
            }

        }
        else
        {
            if (hdnMode.Value == "Topic")
            {
                int responseid = BLL_FAQ_Item.Update_ModuleTopic(txtName.Text.Trim(), Convert.ToInt32(txtID.Text), "Topic", Convert.ToInt32(ddlModule.SelectedValue), UDFLib.ConvertToInteger(Session["UserID"]));
                BindTopicGrid();
            }
            else
            {
                int responseid = BLL_FAQ_Item.Update_ModuleTopic(txtName.Text.Trim(), Convert.ToInt32(txtID.Text), "Module", null, UDFLib.ConvertToInteger(Session["UserID"]));
                BindModuleGrid();
                BindTopicGrid();
            }
        }

        //BindGrid();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }
    //protected void ImgExpExcel_Click(object sender, EventArgs e)
    //{


    //    int rowcount = ucCustomPager1.isCountRecord;
    //    DataTable dt = new DataTable();
    //    string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
    //    int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
    //    if (rdoStatus.SelectedValue == "1")
    //    {
    //         dt = BLL_LMS_FAQ.Get_ModuleTopic_Details(txtfilter.Text != "" ? txtfilter.Text : null
    //       , null, null, "Topic", null,UDFLib.ConvertIntegerToNull(ddlfltModule.SelectedValue), ref  rowcount).Tables[0];
    //    }
    //    else
    //    {
    //         dt = BLL_LMS_FAQ.Get_ModuleTopic_Details(txtfilter.Text != "" ? txtfilter.Text : null
    //     , null, null, "Module", null,null, ref  rowcount).Tables[0];
    //    }

    //    string[] HeaderCaptions = { "Code", "Company Type", "Name", "Short Name", "Reg No", "Date Of Incorp", "Country of Incorp", "Currency", "Email", "Phone" };
    //    string[] DataColumnsName = { "Company_Code", "Company_Type", "Company_Name", "Short_Name", "Reg_Number", "Date_Of_Incorp", "COUNTRY_INCORP", "Currency_code", "Email1", "Phone1" };

    //    GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Company", "Company", "");

    //}
    //protected void rdoStatus_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindGrid();
    //}
    protected void onFilter(object source, CommandEventArgs e)
    {
        ucCustomPagerItems.PageSize = 15;
        hdnModuleID.Value = e.CommandArgument.ToString();
        BindTopicGrid();
    }
}