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
using SMS.Business.ASL;

public partial class ASL_ASL_EmailTemplate : System.Web.UI.Page
{

    BLL_Infra_AirPort objBLLAirPort = new BLL_Infra_AirPort();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    BLL_ASL_Lib objBLLASLLib = new BLL_ASL_Lib();
    UserAccess objUA = new UserAccess();
    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            BindEmailStatus();
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;
            //ucCustomPagerItems.PageSize = 20;      
            BindGrid();
        }
    }
    protected void BindEmailStatus()
    {
        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(55, null, UDFLib.ConvertToInteger(Session["UserID"].ToString()));

        ddlEmailStatus.DataSource = dt;
        ddlEmailStatus.DataValueField = "Name";
        ddlEmailStatus.DataTextField = "Description";
        ddlEmailStatus.DataBind();
        ddlEmailStatus.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
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
        //else
            // btnsave.Visible = false;

            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    public void BindGrid()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        DataTable dt = objBLLASLLib.SearchEmailTemplate(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvVesselType.DataSource = dt;
            gvVesselType.DataBind();
        }
        else
        {
            gvVesselType.DataSource = dt;
            gvVesselType.DataBind();
        }

    }
    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtUserType");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Email Template";

        ddlEmailStatus.SelectedValue = "0";
        txtemailbody.Text = "";
        txtemailsubject.Text = "";


        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        int responseid;
        if (HiddenFlag.Value == "Add")
        {
             responseid = objBLLASLLib.InsertASL_EmailTemplate(ddlEmailStatus.SelectedValue, txtemailbody.Text.Trim(), txtemailsubject.Text.Trim(), Convert.ToInt32(Session["USERID"]));

        }
        else
        {
             responseid = objBLLASLLib.EditASL_EmailTemplate(Convert.ToInt32(txtUserTypeID.Text), ddlEmailStatus.SelectedValue, txtemailbody.Text.Trim(), txtemailsubject.Text.Trim(), Convert.ToInt32(Session["USERID"]));

        }
        if (responseid == 0)
        {
            string SupplierService = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", SupplierService, true);
            string message = "alert('Email Status Already Existed.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        else
        {
            BindGrid();
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }

    }
    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit EmailTemplate";

        DataTable dt = new DataTable();
        dt = objBLLASLLib.Get_ASL_EmailTemplateList(Convert.ToInt32(e.CommandArgument.ToString()));
        //dt.DefaultView.RowFilter = "ID= '" + e.CommandArgument.ToString() + "'";
        //txtUserTypeID.Text = dt.DefaultView[0]["ID"].ToString();
        //txtEmailstatus.Text = dt.DefaultView[0]["Email_Status"].ToString();
        //txtemailbody.Text = dt.DefaultView[0]["Email_Body"].ToString();
        //txtemailsubject.Text = dt.DefaultView[0]["Email_Subject"].ToString();
        txtUserTypeID.Text = dt.Rows[0]["ID"].ToString();
        ddlEmailStatus.SelectedValue = dt.Rows[0]["Email_Status"].ToString();
        txtemailbody.Text = dt.Rows[0]["Email_Body"].ToString();
        txtemailsubject.Text = dt.Rows[0]["Email_Subject"].ToString();

        //string InfoDiv = "Get_Record_Information_Details('ASL_LIB_SUPPLIER_EMAIL','ID=" + txtUserTypeID.Text + "')";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);

        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);

    }
    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLLASLLib.DeleteEmailTemplate(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindGrid();

    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        BindGrid();

    }
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        DataTable dt = objBLLASLLib.SearchEmailTemplate(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                    , null, null, ref  rowcount);

        string[] HeaderCaptions = { "ID", "EmailBody","EmailSubject","EmailStatus"};
        string[] DataColumnsName = { "ID", "Email_Body","Email_Subject","Email_Status"};

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "EmailTemplate", "EmailTemplate", "");

    }
    protected void gvVesselType_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void gvVesselType_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindGrid();
    }
}