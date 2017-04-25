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

public partial class PO_LOG_PO_Log_AuditTrail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Code"] != null)
        {
            txtPOCode.Text = Request.QueryString["Code"].ToString();
            BindGrid();
        }
    }
    protected void BindGrid()
    {

        DataTable dt = BLL_POLOG_Register.POLOG_Get_TransactionLog(UDFLib.ConvertStringToNull(txtPOCode.Text.ToString()));

        if (dt.Rows.Count > 0)
        {
            gvlog.DataSource = dt;
            gvlog.DataBind();
        }
    }
}