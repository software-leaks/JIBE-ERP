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
using SMS.Business.POLOG;


public partial class PO_LOG_PO_Log_Payment_Entry : System.Web.UI.Page
{
    BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
    UserAccess objUA = new UserAccess();
    public string Type = null;
    public string OperationMode = "";
    public string CurrStatus = null;
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        //UserAccessValidation();
        if (!IsPostBack)
        {
            txtSupplierCode.Text = Request.QueryString["Supplier_Code"].ToString();
            txtPayMode.Text = Request.QueryString["PayMode"].ToString();
            BindType();
            BindSupplierDetails();
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void BindSupplierDetails()
    {
        try
        {
            DataSet dt = BLL_POLOG_Register.POLOG_Get_Payment_Supplier_Invoice(UDFLib.ConvertStringToNull(txtSupplierCode.Text), UDFLib.ConvertStringToNull(txtPayMode.Text), UDFLib.ConvertIntegerToNull(GetSessionUserID()));


            if (dt.Tables[0].Rows.Count > 0)
            {
               
                gvPaymentDetails.DataSource = dt;
                gvPaymentDetails.DataBind();
            }
            else
            {

                gvPaymentDetails.DataSource = dt;
                gvPaymentDetails.DataBind();
            }

            gvlinkPayment.DataSource = null;
            gvlinkPayment.DataBind();
            gvNewPayment.DataSource = null;
            gvNewPayment.DataBind();
        }
        catch { }
        {
        }
    }
    protected void BindType()
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()), "PO_TYPE");

        ddlPayMode.DataSource = ds.Tables[11];
        ddlPayMode.DataTextField = "VARIABLE_NAME";
        ddlPayMode.DataValueField = "VARIABLE_CODE";
        ddlPayMode.DataBind();
        ddlPayMode.Items.Insert(0, new ListItem("-Select-", "0"));

        rdbPaymode.DataSource = ds.Tables[12];
        rdbPaymode.DataTextField = "VARIABLE_NAME";
        rdbPaymode.DataValueField = "VARIABLE_CODE";
        rdbPaymode.DataBind();
        //rdbPaymode.Items.Insert(0, new ListItem("-Select-", "0"));

        ddlAccount.DataSource = ds.Tables[13];
        ddlAccount.DataTextField = "Account_Name";
        ddlAccount.DataValueField = "Account_ID";
        ddlAccount.DataBind();
        ddlAccount.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void gvPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ImgStatus = (ImageButton)e.Row.FindControl("ImgView");
            Label lblStatus = (Label)e.Row.FindControl("lblUrgency");

            if (DataBinder.Eval(e.Row.DataItem, "Urgency").ToString() == "ASAP")
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.Black;
            }
            if (DataBinder.Eval(e.Row.DataItem, "Payment_Status").ToString() == "")
            {
                ImgStatus.ImageUrl = "~/Images/asl_view.png";
            }
            else
            {

                ImgStatus.ImageUrl = "~/Images/locked.bmp";
            }
        }

    }
}