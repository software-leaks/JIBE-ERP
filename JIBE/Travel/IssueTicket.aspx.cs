using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//custome defined libararies
using SMS.Business.TRAV;
using System.IO;
using System.Data;
using System.Drawing;
using SMS.Business.Infrastructure;


public partial class IssueTicket : System.Web.UI.Page
{
    int PaxID;
    public int RequestID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
           
            
            RequestID = UDFLib.ConvertToInteger(Request.QueryString["requestid"].ToString());
            if (!IsPostBack)
            {

                txtMailBody.config.toolbar = new object[]
            {
               
                new object[] { "Cut", "Copy", "Paste", "PasteText", "-", "Print", "SpellChecker", "Scayt" },
                new object[] { "Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat" },
                new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
                new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote" },
                new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
                "/",
                new object[] { "HorizontalRule", "PageBreak"},
                new object[] { "Styles", "Format", "Font", "FontSize" },
                new object[] { "TextColor", "BGColor" },
                new object[] { "Maximize", "ShowBlocks"}
            };
                txtMailBody.Height = 200;
                txtMailBody.Width = 490;

                txtMailBody.ResizeEnabled = false;


                GetETickets();
                GetQuotations();
            }

            
        }
        catch { }
    }
    
    /// Agent will be able to upload the ticket if the status is Issued or Approved
    /// Status can be checked in TRequest.Get_TravelStatus menthod
    protected void cmdTicketNo_onClick(object source, EventArgs e)
    {
        lblMessage.Text = "";
        TextBox txtETicketNumber = (TextBox)((GridViewRow)((Button)source).Parent.Parent).FindControl("txtETicketNumber");
        FileUpload flName = (FileUpload)((GridViewRow)((Button)source).Parent.Parent).FindControl("flName");


        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        BLL_TRV_Attachment att = new BLL_TRV_Attachment();
        BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
        DataTable dt = new DataTable();
        dt = objUploadFilesize.Get_Module_FileUpload("TRV_");
        string status = "";
        DataTable dtStatus = TRequest.Get_TravelStatus(UDFLib.ConvertToInteger(Request.QueryString["requestid"].ToString()));
        if (dtStatus.Rows.Count > 0)
        {
            status = dtStatus.Rows[0]["currentStatus"].ToString();
            if (status == "APPROVED" || status == "ISSUED")
            {
                if (dt.Rows.Count > 0)
                {
                    string datasize = dt.Rows[0]["Size_KB"].ToString();
                    if (flName.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                    {

                        try
                        {
                            int RequestID = UDFLib.ConvertToInteger(Request.QueryString["requestid"].ToString());
                            int Flightid;
                            PaxID = UDFLib.ConvertToInteger(((Button)source).CommandArgument.Split(',')[0]);
                            Flightid = UDFLib.ConvertToInteger(((Button)source).CommandArgument.Split(',')[1]);
                            if (!String.IsNullOrEmpty(txtETicketNumber.Text) && flName.PostedFile.ContentLength > 0)
                            {
                                if (TRequest.UpdateETicketNumber(RequestID, Flightid, txtETicketNumber.Text,
                                    UDFLib.ConvertToInteger(Session["USERID"].ToString()), PaxID))
                                {
                                    //string msg = "E-TICKET for Request #" + RequestID.ToString() + " has been uploaded on the website, please download the same";
                                    //    UDFLib.ConvertToInteger(Session["USERID"].ToString()));

                                    if (flName.PostedFile.ContentLength > 0)
                                    {
                                        string sFileName, filePath;

                                        sFileName = System.Guid.NewGuid().ToString() + Path.GetExtension(flName.PostedFile.FileName);

                                        filePath = ConfigurationManager.AppSettings["TRV_UPLOAD_PATH"].ToString();
                                        if (flName.PostedFile.ContentLength <= 3048000)
                                        {


                                            flName.PostedFile.SaveAs((Server.MapPath("~/") + ConfigurationManager.AppSettings["TRV_UPLOAD_PATH"].ToString() + sFileName));

                                            att.SaveAttchement(RequestID, Path.GetFileName(flName.PostedFile.FileName), sFileName, "ETICKET", txtETicketNumber.Text,
                                                Convert.ToInt32(Session["USERID"].ToString()), UDFLib.ConvertIntegerToNull(Request.QueryString["SUPPLIER_ID"]));

                                            //File.Copy(filePath, @"\\server01\uploads\Travel\ticket\" + sFileName);

                                        }
                                    }


                                }
                                GetETickets();
                            }

                        }
                        catch { }
                        finally { TRequest = null; }

                    }

                    else
                    {
                        lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                    }
                }
                else
                {
                    string js2 = "alert('Upload size not set!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                }
            }
        }

    }
    protected void GetETickets()
    {
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        try
        {
            DataTable dt = TRequest.Get_ETicket_By_RequestID(UDFLib.ConvertToInteger(Request.QueryString["requestid"].ToString()));
            grdTickets.DataSource = dt;
            grdTickets.DataBind();

            if (dt.Rows.Count == 0)
            {
                Response.Write("<br><br><center><h2>E-Ticket is not yet issued for the travel request</h2></center>");
            }
        }
        catch { }
        finally { TRequest = null; }
    }

    protected void GetQuotations()
    {
        BLL_TRV_QuoteRequest qr = new BLL_TRV_QuoteRequest();
        DataSet ds = new DataSet();
        try
        {
            ds = qr.Get_Quotation_Ticket(UDFLib.ConvertToInteger(Request.QueryString["requestid"].ToString()));
            rptParent.DataSource = ds.Tables[0];
            rptParent.DataBind();

            rptChild.DataSource = ds.Tables[1];
            rptChild.DataBind();
        }
        catch { }
        finally { qr = null; }
    }

    protected void rptParent_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToUpper() == "SENDQUOTE")
            {
                BLL_TRV_QuoteRequest Qr = new BLL_TRV_QuoteRequest();
                Qr.SendQuotaion(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session["USERID"].ToString()));
                Qr = null;
                GetQuotations();
            }
        }
        catch { }
    }

    protected void rptParent_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Data.DataRowView drv = (System.Data.DataRowView)e.Item.DataItem;
                string strName = drv.Row["quotedate"].ToString();
                if (!String.IsNullOrEmpty(strName))
                {
                    ((Button)e.Item.FindControl("cmdSendQuote")).Enabled = false;
                }
            }
        }
        catch { }
    }

    protected void rptChild_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id;
        try
        {
            if (e.CommandName == "removeflight")
            {
                BLL_TRV_QuoteRequest qr = new BLL_TRV_QuoteRequest();
                id = Convert.ToInt32(e.CommandArgument);
                qr.DeleteQuoteFlight(id, Convert.ToInt32(Session["USERID"].ToString()));
                qr = null;
                GetQuotations();
            }

        }
        catch { }
    }

    protected void rptChild_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        BLL_TRV_TravelRequest treq = new BLL_TRV_TravelRequest();
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRow drv = (DataRow)e.Item.DataItem;
                string strName = drv["quotedate"].ToString();
                if (UDFLib.ConvertStringToNull(strName) != null)
                    ((ImageButton)e.Item.FindControl("btnDelete")).Visible = false;
                else
                    ((ImageButton)e.Item.FindControl("btnDelete")).Attributes.Add("onclick", "return confirm('This will DELETE flight, are your sure?');");
            }
        }
        catch { }
        finally { treq = null; }
    }

    protected void btnCreateTicket_Click(object s, EventArgs e)
    {
        try
        {
        string sFileName=Guid.NewGuid().ToString();
        string filePath = Server.MapPath("~/") + ConfigurationManager.AppSettings["TRV_UPLOAD_PATH"].ToString() + sFileName + ".pdf";

        EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
          "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
          "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
          "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
          "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
          "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");

        EO.Pdf.HtmlToPdf.Options.AfterRenderPage = new EO.Pdf.PdfPageEventHandler(On_AfterRenderPage);
        EO.Pdf.HtmlToPdf.ConvertHtml(@"<html><div>"+txtMailBody.Text+"</div> </html> ", filePath);
         
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        BLL_TRV_Attachment att = new BLL_TRV_Attachment();
     
            int RequestID = UDFLib.ConvertToInteger(Request.QueryString["requestid"].ToString());
           

            if (!String.IsNullOrEmpty(hdf_TicketNumber.Value))
            {
                if (TRequest.UpdateETicketNumber(RequestID, Convert.ToInt32(hdf_Flightid.Value), hdf_TicketNumber.Value,
                    UDFLib.ConvertToInteger(Session["USERID"].ToString()), Convert.ToInt32(hdf_PaxID.Value)))
                {
                    att.SaveAttchement(RequestID, sFileName + ".pdf", sFileName + ".pdf", "ETICKET", hdf_TicketNumber.Value,
                              Convert.ToInt32(Session["USERID"].ToString()), UDFLib.ConvertIntegerToNull(Request.QueryString["SUPPLIER_ID"]));
                }
            }
            GetETickets();
        }
        catch(Exception ex) 
        {
            String msgmodalexp = String.Format("alert('"+ex.Message+"');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "expCreateTicket", msgmodalexp, true);
        }

        String msgmodal = String.Format("hideModal('dvCreateTicket');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dvCreateTicket", msgmodal, true);
    }
    //This function is called after every page is created
    private void On_AfterRenderPage(object sender, EO.Pdf.PdfPageEventArgs e)
    {
        EO.Pdf.PdfPage page = e.Page;
        EO.Pdf.Acm.AcmRender render = new EO.Pdf.Acm.AcmRender(page, 0, new EO.Pdf.Acm.AcmPageLayout(new EO.Pdf.Acm.AcmPadding(0, 0, 0, 0)));
        render.SetDefPageSize(new SizeF(EO.Pdf.PdfPageSizes.A4.Width, EO.Pdf.PdfPageSizes.A4.Height));
        EO.Pdf.Acm.AcmBlock footer = new EO.Pdf.Acm.AcmBlock(new EO.Pdf.Acm.AcmText("."));
        footer.Style.Top = 10.4f;
        footer.Style.BackgroundColor = Color.Blue;
        footer.Style.ForegroundColor = Color.Blue;
        render.Render(footer);


    }
    /// Agent will be able to paste the ticket if the status is Issued or Approved
    /// Status can be checked in TRequest.Get_TravelStatus menthod
    protected void btnPasteTicket_Click(object source, EventArgs e)
    {
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
          string status = "";
        DataTable dtStatus = TRequest.Get_TravelStatus(UDFLib.ConvertToInteger(Request.QueryString["requestid"].ToString()));
        if (dtStatus.Rows.Count > 0)
        {
            status = dtStatus.Rows[0]["currentStatus"].ToString();
            if (status == "APPROVED" || status == "ISSUED")
            {
                string ticketno = ((TextBox)((GridViewRow)((Button)source).Parent.Parent).FindControl("txtETicketNumber")).Text;
                if (ticketno.Trim().Length > 1)
                {
                    hdf_TicketNumber.Value = ticketno;
                    hdf_PaxID.Value = ((Button)source).CommandArgument.Split(',')[0];
                    hdf_Flightid.Value = ((Button)source).CommandArgument.Split(',')[1];

                    String msgmodal = String.Format("showModal('dvCreateTicket');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dvCreateTicket", msgmodal, true);
                }
                else
                {
                    String msgmodal = String.Format("alert('Please enter ticket number !');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "vlddvCreateTicket", msgmodal, true);
                }
            }
        }



    }
}