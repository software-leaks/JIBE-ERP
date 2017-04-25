using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
//user defined libararies
using SMS.Data.TRAV;
using SMS.Business.TRAV;
public partial class Attachment : System.Web.UI.Page
{
    int RequestID;
    string Attach_Type = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();


        RequestID = Convert.ToInt32(Request.QueryString["requestid"].ToString());
        dvInvoice.Visible = false;



        if (!IsPostBack)
        {

            Attach_Type = UDFLib.ConvertStringToNull(Request.QueryString["AttType"]);

            if (Attach_Type != null)
            {

                ListItem liAttch = ddlAttachmentType.Items.FindByValue(Attach_Type);
                if (liAttch != null)
                    liAttch.Selected = true;

                if (liAttch.Value == "INVOICE")
                    dvInvoice.Visible = true;
                else
                    dvInvoice.Visible = false;

            }

            if (UDFLib.ConvertStringToNull((Request.QueryString["REQSTS"])) == "APPROVED")
            {
                ddlAttachmentType.ClearSelection();
                ddlAttachmentType.Items.FindByValue("ETICKET").Selected = true;
            }



            //string CkeckForInvoce = String.Format("CkeckForInvoce();");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CkeckForInvoce", CkeckForInvoce, true);
           
        }

       
        Attach_Type = ddlAttachmentType.SelectedValue;
        if ( Attach_Type == "ETICKET")
        {
            lblhdrAttach.Visible = false;
            flName.Visible = false;
            cmdUpload.Visible = false;
        }
       else 
        {
            lblhdrAttach.Visible = true;
            flName.Visible = true;
            cmdUpload.Visible = true;
        }

        GetAttachment(RequestID, Attach_Type);

    }

    private void GetAttachment(int RequestID, string Attach_Type)
    {
        objAttachment.SelectParameters[0].DefaultValue = RequestID.ToString();
        objAttachment.SelectParameters[1].DefaultValue = Attach_Type;
        objAttachment.SelectParameters[2].DefaultValue = UDFLib.ConvertIntegerToNull(Request.QueryString["SUPPLIER_ID"]).ToString();
        grdFiles.DataBind();
    }

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {

            cmdUpload.Visible = false;
        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }


    }

    protected void cmdUpload_Click(object source, EventArgs e)
    {
        BLL_TRV_Attachment att = new BLL_TRV_Attachment();
        try
        {


            if (IsPostBack)
            {

                if (Attach_Type.ToString().ToUpper() == "INVOICE")
                {
                    SaveInvoice();
                    ddlAttachmentType.SelectedIndex = 0;
                    txtInvAmount.Text = "";
                    txtInvNo.Text = "";
                }

                if (flName.PostedFile.ContentLength > 0)
                {
                    string sFileName, filePath;

                    sFileName = System.Guid.NewGuid().ToString() + Path.GetExtension(flName.PostedFile.FileName);

                    filePath = ConfigurationManager.AppSettings["TRV_UPLOAD_PATH"].ToString();
                    if (flName.PostedFile.ContentLength <= 3048000)
                    {

                        filePath = Server.MapPath("~/") + ConfigurationManager.AppSettings["TRV_UPLOAD_PATH"].ToString() + sFileName;
                        flName.PostedFile.SaveAs(filePath);

                        att.SaveAttchement(RequestID,Path.GetFileName(flName.PostedFile.FileName), sFileName,  Attach_Type, "",
                            Convert.ToInt32(Session["USERID"].ToString()), UDFLib.ConvertIntegerToNull(Request.QueryString["SUPPLIER_ID"]));
                    }
                }
            }
            GetAttachment(RequestID, Attach_Type);
        }
        catch (UnauthorizedAccessException ex) { throw new Exception(ex.Message + "Permission to upload file denied"); }
        finally { att = null; }
    }

    protected void grdFiles_onRowCommand(object source, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString().ToUpper() == "DELETE")
            {
                objAttachment.DeleteParameters["userid"].DefaultValue = Session["USERID"].ToString();



            }
        }
        catch { }
    }

    protected void grdFiles_onRowDataBound(object source, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (objUA.Delete == 0)
                e.Row.Cells[e.Row.Cells.Count - 1].Enabled = false;

            ((ImageButton)e.Row.Cells[e.Row.Cells.Count - 1].Controls[0]).OnClientClick = "if (!confirm('Are you sure to delete the attachment?')) return false;";
        }
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
                objInvoice.Save_Invoice(RequestID, txtInvNo.Text, Convert.ToDateTime(txtInvDueDate.Text),
                    UDFLib.ConvertToDecimal(txtInvAmount.Text), cmbCurrency.SelectedValue, Convert.ToDateTime(txtInvDueDate.Text),
                    UDFLib.ConvertToInteger(Session["USERID"].ToString()), txtInvoiceRemarks.Text);
                grdFiles.DataBind();
            }
        }
        catch { }
        finally { objInvoice = null; }
    }

    protected void ddlAttachmentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAttachmentType.SelectedValue == "INVOICE")
            dvInvoice.Visible = true;
        else
            dvInvoice.Visible = false;


        GetAttachment(RequestID, Attach_Type);
    }
}