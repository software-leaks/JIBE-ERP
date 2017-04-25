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

public partial class ASL_ASL_Change_Request_History : System.Web.UI.Page
{
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
    BLL_Infra_AirPort objBLLAirPort = new BLL_Infra_AirPort();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    UserAccess objUA = new UserAccess();
    public string Type = null;
    public string OperationMode = "";
    public string CurrStatus = null;
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            //ddlStatus.SelectedValue = "Pending";
            BindGrid();
        }
    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        BindGrid();
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
        {
            pnl.Visible = false;
            lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
        }
        else
        {
            pnl.Visible = true;
        }

        if (objUA.Add == 0)
        {

        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        //else
            // btnsave.Visible = false;

            if (objUA.Delete == 1) uaDeleteFlage = true;

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
        catch { return "2743"; }
    }
    public void BindGrid()
    {
        
        string Supplier_Code =  GetSuppID();
        DataSet ds = BLL_ASL_Supplier.Get_ChangeRequest_Search(UDFLib.ConvertToInteger(Session["UserID"].ToString()), Supplier_Code);

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvSupplier.DataSource = ds.Tables[0];
            gvSupplier.DataBind();
        }
        else
        {
            gvSupplier.DataSource = ds.Tables[0];
            gvSupplier.DataBind();
        }

    }
}