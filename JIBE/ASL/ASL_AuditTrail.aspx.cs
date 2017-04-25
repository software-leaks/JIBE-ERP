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
using System.Configuration;

public partial class ASL_ASL_AuditTrail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Supp_ID"] != null)
            {
                lblSuppliername.Text = GetSessionSupplierName();
                lblSupplierCode.Text = GetSuppID();
                BindGrid();
            }
        }
    }
    protected void BindGrid()
    {
        
        txtSupplierCode.Text = GetSuppID();
        DataTable dt = BLL_ASL_Supplier.ASL_Get_TransactionLog(UDFLib.ConvertStringToNull(txtSupplierCode.Text.ToString()));

        if (dt.Rows.Count > 0)
        {
            gvlog.DataSource = dt;
            gvlog.DataBind();
        }
        else
        {
            gvlog.DataSource = null;
            gvlog.DataBind();
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
}