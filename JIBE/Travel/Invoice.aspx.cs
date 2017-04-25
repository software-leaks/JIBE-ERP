using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//custom defined libaries
using SMS.Business.TRAV;
using System.Configuration;
using SMS.Business.Infrastructure;

public partial class Travel_Invoice : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ViewState["STATUS"] = "";
            if (!IsPostBack) {
                
                GetInvoices("");

                    BLL_Infra_Currency objCurr = new BLL_Infra_Currency();
                    cmbCurrency.DataTextField = "Currency_Code";
                    cmbCurrency.DataValueField = "Currency_Code";
                    cmbCurrency.DataSource = objCurr.Get_CurrencyList();
                    cmbCurrency.DataBind();
                    ListItem lis = new ListItem("-Select Currency-", "");
                    cmbCurrency.Items.Insert(0, lis);            
            }
        }
        catch { }
    }

    protected void GetInvoices(string status)
    {
        BLL_TRV_Invoice objInvoice = new BLL_TRV_Invoice();
        try
        {
            int VCode = 0;
            if (!String.IsNullOrEmpty(cmbVessel.SelectedValue))
                VCode = Convert.ToInt32(cmbVessel.SelectedValue);

            DataSet ds = objInvoice.Get_Request_For_Invoice(Convert.ToInt32(cmbFleet.SelectedValue), VCode, Convert.ToInt32(cmbSupplier.SelectedValue),txtTrvDateFrom.Text, txtTrvDateTo.Text, status,UDFLib.ConvertToInteger(Session["USERID"].ToString()) );

            grdInvoice.DataSource = ds;
            grdInvoice.DataBind();

            

        }
        catch { }
        finally { objInvoice = null; }
    }

    protected void grd_Invoice_RowCommand(object source, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PAID")
        {
            BLL_TRV_Invoice objInvoice = new BLL_TRV_Invoice();
            try
            {                
                objInvoice.Save_Invoice(Convert.ToInt32(e.CommandArgument), UDFLib.ConvertToInteger(Session["USERID"].ToString()));
                GetInvoices(ViewState["STATUS"].ToString());
            }
            catch { }
            finally { objInvoice = null; }

        }
    }

    protected void grd_Invoice_RowDataBound(object source, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((ImageButton)e.Row.FindControl("imgPaid")).Attributes.Add("onclick", "return(confirm('Sure mark to it as PAID'));");
            
            string Invoice_Number = DataBinder.Eval(e.Row.DataItem, "Invoice_Number").ToString();

            if(Invoice_Number!="")
            {
                ((CheckBox)e.Row.FindControl("chkSelect")).Visible = false;
            }
            
                
        }
    }

    protected void cmbFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try {
            GetInvoices(ViewState["STATUS"].ToString());
        }
        catch { }
    }

    protected void cmbVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try {
            GetInvoices(ViewState["STATUS"].ToString());
        }
        catch { }
    }

    protected void cmbVessel_OnDataBound(object source, EventArgs e) { cmbVessel.Items.Insert(0, new ListItem("-Select All-", "0")); }

    protected void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        try {
            GetInvoices(ViewState["STATUS"].ToString());
        }
        catch { }
    }

    protected void cmdAllInvoices_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["STATUS"] = "";

            GetInvoices(ViewState["STATUS"].ToString());
            cmdPending.BackColor = System.Drawing.Color.White;
            cmdAllInvoice.BackColor = System.Drawing.Color.LightSkyBlue;
            cmdClosed.BackColor = System.Drawing.Color.White;
        }
        catch { }
    }

    protected void cmdPending_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["STATUS"]= "PENDING";
            GetInvoices(ViewState["STATUS"].ToString());
            cmdPending.BackColor = System.Drawing.Color.LightSkyBlue;
            cmdAllInvoice.BackColor = System.Drawing.Color.White;
            cmdClosed.BackColor = System.Drawing.Color.White;
        }
        catch { }
    }

    protected void cmdClosed_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["STATUS"] = "PAID";
            GetInvoices(ViewState["STATUS"].ToString());
            cmdPending.BackColor = System.Drawing.Color.White;
            cmdAllInvoice.BackColor = System.Drawing.Color.White;
            cmdClosed.BackColor = System.Drawing.Color.LightSkyBlue;


        }
        catch { }
    }

    protected void imgUploadInvoice_Click(object sender, EventArgs e)
    {


        string js = "showModal('dvUploadInvoice');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "bindEvents", js, true);

    }

    protected void cmdUpload_Click(object source, EventArgs e)
    {
        BLL_TRV_Attachment att = new BLL_TRV_Attachment();
        try
        {
            int RequestID = 0;
            
            
            if (flName.PostedFile.ContentLength > 0)
            {
                string sFileName, filePath;

                sFileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetExtension(flName.PostedFile.FileName);

                filePath = ConfigurationManager.AppSettings["TRV_UPLOAD_PATH"].ToString();
                if (flName.PostedFile.ContentLength <= 3048000)
                {

                    filePath = Server.MapPath("~/") + ConfigurationManager.AppSettings["TRV_UPLOAD_PATH"].ToString() + sFileName;
                    flName.PostedFile.SaveAs(filePath);

                    att.SaveAttchement(RequestID, sFileName, System.IO.Path.GetFileName(flName.PostedFile.FileName), "INVOICE", "",
                        Convert.ToInt32(Session["USERID"].ToString()), UDFLib.ConvertIntegerToNull(Request.QueryString["SUPPLIER_ID"]));

                    SaveInvoice();

                    GetInvoices("");
                }
            }

        }
        catch (UnauthorizedAccessException ex) { throw new Exception(ex.Message + "Permission to upload file denied"); }
        finally { att = null; }
    }

    protected void SaveInvoice()
    {
        BLL_TRV_Invoice objInvoice = new BLL_TRV_Invoice();
        try
        {
            if (txtInvNo.Text.Trim() == "" || txtInvAmount.Text.Trim() == "" || txtInvDueDate.Text.Trim() == "")
                Response.Write("<script type='text/javascript'>alert('Invoice Number, Amount and Due Date are MANDATORY');</script>");
            else
            {
                foreach (GridViewRow gvR in grdInvoice.Rows)
                {
                    CheckBox chkSelect = ((CheckBox)gvR.FindControl("chkSelect"));
                    HiddenField hdnRequestID = ((HiddenField)gvR.FindControl("hdnRequestID"));

                    if (chkSelect != null && hdnRequestID!= null)
                    {
                        if (chkSelect.Checked == true)
                        {
                            int RequestID = UDFLib.ConvertToInteger(hdnRequestID.Value);

                            objInvoice.Save_Invoice(RequestID, txtInvNo.Text, Convert.ToDateTime(txtInvDueDate.Text),
                                UDFLib.ConvertToDecimal(txtInvAmount.Text), cmbCurrency.SelectedValue, Convert.ToDateTime(txtInvDueDate.Text),
                                UDFLib.ConvertToInteger(Session["USERID"].ToString()), txtInvoiceRemarks.Text);

                        }
                    }
                }
            }
        }
        catch { }
        finally { objInvoice = null; }
    }
}