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
public partial class ASL_ASL_Payment_History : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    private string GetSessionSupplierName()
    {
        try
        {
            if (Request.QueryString["Supplier_Name"] != null)
            {
                return Request.QueryString["Supplier_Name"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
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
            //ImgAdd.Visible = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        //else
        //    btnsave.Visible = false;

        if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    protected void BindGrid()
    {
        lblSuppliername.Text = GetSessionSupplierName();
        lblSupplierCode.Text = GetSuppID();
        SearchBindGrid();

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
    protected void SearchBindGrid()
    {
       
        string SupplierID = GetSuppID();
        int Type = 0;
        DataSet ds = BLL_ASL_Supplier.Get_Supplier_PaymentHistory(SupplierID, Type,0,0);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvPaymentHistory.DataSource = ds;
            gvPaymentHistory.DataBind();
        }
        else
        {
            gvPaymentHistory.DataSource = ds;
            gvPaymentHistory.DataBind();
        }
    }
    protected void gvPaymentHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string SupplierID = GetSuppID();
            GridView gv = (GridView)e.Row.FindControl("gvPaymentHistoryDetails");
            int PaymentID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Payment_ID").ToString());
            int Paymentyear = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PAYMENT_YEAR").ToString());
            int Type = 1;
            DataSet ds = BLL_ASL_Supplier.Get_Supplier_PaymentHistory(SupplierID, Type, PaymentID, Paymentyear);


            if (ds.Tables[0].Rows.Count > 0)
            {
                gv.DataSource = ds;
                gv.DataBind();
            }
            else
            {
                gv.DataSource = ds;
                gv.DataBind();
            }
        }
    }
}