using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.ASL;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class ASL_Extend_Expiry_Period : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
          

            BindProposedStatusDDL();

            if (!String.IsNullOrEmpty(Request.QueryString["Supp_ID"].ToString()))
            {

                BindProposedList();
                BindStatus();
                BindProposedStatusDDL();
          
            }
        }
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
            //ImgAdd.Visible = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;

            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    protected void BindStatus()
    {
        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(8, "", UDFLib.ConvertToInteger(Session["UserID"].ToString()));

        ddlStatus.DataSource = dt;
        ddlStatus.DataTextField = "Name";
        ddlStatus.DataValueField = "Code";
        ddlStatus.DataBind();
        ddlStatus.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }


    private void BindProposedStatusDDL()
    {
        try
        {

            DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(4, null, UDFLib.ConvertToInteger(Session["UserID"].ToString()));

            ddlType.DataSource = dt;
            ddlType.DataValueField = "Code";
            ddlType.DataTextField = "Name";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("SELECT", "0"));

        }
        catch
        {
        }
    }
  

    private void BindProposedList()
    {

        DataSet ds = BLL_ASL_Supplier.Get_Supplier_Data_List(UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString()));

        if (ds.Tables[0].Rows.Count > 0)
        {

            txtRegisterName.Text = ds.Tables[0].Rows[0]["Register_Name"].ToString();
            ddlPeriod.SelectedValue = ds.Tables[0].Rows[0]["For_Period"].ToString();
            txtExpireON.Text = ds.Tables[0].Rows[0]["Expire_On"].ToString();
            ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["Supp_Status"].ToString();
            ddlType.SelectedValue = ds.Tables[0].Rows[0]["Propose_Type"].ToString();
           
        }

    }

 
    protected void btnExtend_Click(object sender, EventArgs e)
    {
        try
        {


            int retval = BLL_ASL_Supplier.Supplier_Extend_Period(UDFLib.ConvertIntegerToNull(Request.QueryString["Supp_ID"].ToString()), UDFLib.ConvertIntegerToNull(ddlExtendedPeriod.SelectedValue));

            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);

        }
        catch
        {
        }


    }

   


}