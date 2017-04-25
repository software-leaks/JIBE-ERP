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

public partial class PO_LOG_PO_Log_Supplier_Details : System.Web.UI.Page
{
    public string Type = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Code"] != null)
            {
                txtPOCode.Text = Request.QueryString["Code"].ToString();
                Type = "1";
                BindGrid();
            }
        }
    }
    protected void BindGrid()
    {

        DataSet ds = BLL_POLOG_Register.POLOG_Get_SupplierPO(UDFLib.ConvertStringToNull(txtPOCode.Text.ToString()), Type);
        if (Type == "1")
        {
          
            lblMsg.Text = "All PO issued to Supplier";
            gvPODetails.DataSource = ds.Tables[0];
            gvPODetails.DataBind();
            
        }
        else
        {
           
            lblMsg.Text = "All PO issued to Account Type";
            gvPODetails.DataSource = ds.Tables[0];
            gvPODetails.DataBind();
          
        }
    }
    protected void btnSupplier_Click(object sender, EventArgs e)
    {
         Type = "1";
        BindGrid();
    }
    protected void btnAccountType_Click(object sender, EventArgs e)
    {
         Type = "2";
        BindGrid();
    }
}