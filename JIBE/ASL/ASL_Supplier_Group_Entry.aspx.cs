using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.ASL;
using System.IO;
using SMS.Properties;
using System.Web.UI.HtmlControls;

public partial class ASL_ASL_Supplier_Group_Entry : System.Web.UI.Page
{
    BLL_ASL_Lib objBLL = new BLL_ASL_Lib();
    UserAccess objUA = new UserAccess();
    UserAccess objUAType = new UserAccess();
    
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
        //    UserAccessValidation();
        //    UserAccessTypeValidation();
            ViewState["ReturnSupplierID"] = 0;
            BindRemarksGrid();
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            pnlRemarks.Visible = false;
            lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
        }
        else
        {
            pnlRemarks.Visible = true;
        }

        if (objUA.Add == 0)
        {
            btnRemarks.Visible = false;
        }
        else
        {
            btnRemarks.Visible = true;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;

        if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private string GetSessionSupplierType()
    {
        if (Session["Supplier_Type"] != null)
            return Session["Supplier_Type"].ToString();
        else
            return null;
    }
    protected void UserAccessTypeValidation()
    {
        int CurrentUserID = GetSessionUserID();
        //string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_TypeManagement objType = new BLL_TypeManagement();
        string Variable_Type = "Supplier_Type";
        string Approver_Type = null;
        objUAType = objType.Get_UserTypeAccess(CurrentUserID, Variable_Type, GetSessionSupplierType(), Approver_Type);


        if (objUAType.Add == 0)
        {
            btnRemarks.Visible = false;
        }
        else
        {
            btnRemarks.Visible = true;
        }
        if (objUAType.Edit == 1)
            uaEditFlag = true;
       
        if (objUAType.Delete == 1) uaDeleteFlage = true;

    }
    protected void BindRemarksGrid()
    {
        DataTable dt = objBLL.Get_Supplier_Group_List(0,GetSessionUserID());
        gvGroup.DataSource = dt;
        gvGroup.DataBind();

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public string GetSuppID()
    {
        try
        {

            if (Request.QueryString["Supp_ID"] != null)
            {
                return Request.QueryString["Supp_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    protected void btnRemarks_Click(object sender, EventArgs e)
    {
        int RetValue = objBLL.Supplier_Group_Insert(UDFLib.ConvertToInteger(ViewState["ReturnSupplierID"]), txtGroup.Text,
              UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        BindRemarksGrid();
        ClearControl();
    }
    protected void ClearControl()
    {
        txtGroup.Text = "";
        ViewState["ReturnSupplierID"] = 0;
        btnRemarks.Text = "Add Group";
    }
  

    protected void lbtnEdit_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int GroupID = UDFLib.ConvertToInteger(arg[0]);
        ViewState["ReturnSupplierID"] = UDFLib.ConvertIntegerToNull(arg[0]);
        DataTable dt = new DataTable();
        dt = objBLL.Get_Supplier_Group_List(GroupID, GetSessionUserID());
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            txtGroup.Text = dr["GROUP_NAME"].ToString();
            btnRemarks.Text = "Edit Group";
        }



    }
    protected void lbtnDelete_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int GroupID = UDFLib.ConvertToInteger(arg[0]);

        int RetValue = objBLL.Supplier_Group_Delete(GroupID, GetSessionUserID());
        BindRemarksGrid();

    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {
            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
    }
}