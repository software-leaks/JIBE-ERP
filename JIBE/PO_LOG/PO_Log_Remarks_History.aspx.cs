using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.POLOG;
using System.IO;
using SMS.Properties;


public partial class PO_LOG_PO_Log_Remarks_History : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Invoice_ID"] != null)
            {
                //txtInvoiceID.Text = Request.QueryString["ID"].ToString();
                txtInvoiceCode.Text = Request.QueryString["Invoice_ID"].ToString();
                BindRemarks();
            }
        }
    }
    protected void BindRemarks()
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Remarks_ByInvoiceID(UDFLib.ConvertStringToNull(txtInvoiceCode.Text.ToString()));
            gvRemarkslog.DataSource = ds.Tables[0];
            gvRemarkslog.DataBind();
        }
        catch { }
        {
        }
    }

    protected void gvRemarkslog_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Invoice_ID = DataBinder.Eval(e.Row.DataItem, "Invoice_ID").ToString();
            string Supply_ID = DataBinder.Eval(e.Row.DataItem, "Supply_ID").ToString();
            if (Invoice_ID != "")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.White;
            }
        }
    }
}