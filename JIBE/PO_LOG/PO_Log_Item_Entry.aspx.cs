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
using EO.Pdf;
using System.Drawing;
using ClsBLLTechnical;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using System.Text;
using System.Xml.Linq;
using Telerik.Web.UI;
using System.Web.Caching;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Configuration;

public partial class PO_LOG_PO_Log_Item_Entry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblInvoiceID.Text = Request.QueryString["Invoice_ID"].ToString();
            BindReqsnAttchment();
        }
    }
    protected void btnAttachment_Click(object sender, EventArgs e)
    {
        string InvoiceAttachment = String.Format("showModal('dvAttachment',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvoiceAttachment", InvoiceAttachment, true);
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {

            string DocType = "Invoice";
            //string savePath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\Uploads\\Files_Uploaded");
            Guid GUID = Guid.NewGuid();

            string Flag_Attach = GUID.ToString() + Path.GetExtension(FileUpload1.FileName);


            string OCA_URL = null;
            if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["OCA_APP_URL"]))
            {
                OCA_URL = ConfigurationManager.AppSettings["OCA_APP_URL"];
            }
            string OCA_URL1 = OCA_URL + @"\Files_Uploaded";


           string savePath = (OCA_URL1);

            string FileID = BLL_POLOG_Register.POLOG_Insert_AttachedFile(UDFLib.ConvertStringToNull(lblInvoiceID.Text), Path.GetExtension(FileUpload1.FileName),
                UDFLib.Remove_Special_Characters(Path.GetFileName(FileUpload1.FileName)), Flag_Attach, DocType, 2714);

            FileID = FileID.PadLeft(8, '0');
            string F1 = Mid(FileID, 0, 2);
            string F2 = Mid(FileID, 2, 2);
            string F3 = Mid(FileID, 4, 2);
            if (!Directory.Exists(savePath + "\\" + F1 + "\\" + F2 + "\\" + F3))
            {
                Directory.CreateDirectory(savePath + "\\" + F1 + "\\" + F2 + "\\" + F3);
            }

            string filePath = savePath + "\\" + F1 + "\\" + F2 + "\\" + F3 + "\\" + FileID + "" + Path.GetExtension(FileUpload1.FileName);
            FileUpload1.SaveAs(filePath);
           // int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(lblInvoiceID.Text), "Uploaded Invoice", "UploadedInvoice", 2714);
            BindReqsnAttchment();
        }
        catch (Exception ex)
        {

        }
    }
    public static string Mid(string param, int startIndex, int length)
    {
        //start at the specified index in the string ang get N number of
        //characters depending on the lenght and assign it to a variable
        string result = param.Substring(startIndex, length);
        //return the result of the operation
        return result;
    }
    public void BindReqsnAttchment()
    {
        string DocType = "Invoice";
        DataTable dtAttachment = BLL_POLOG_Register.POLOG_Get_Attachments(UDFLib.ConvertStringToNull(lblInvoiceID.Text), DocType);

        if (dtAttachment.Rows.Count > 0)
        {
            string FilePath = dtAttachment.Rows[0]["File_Path"].ToString();
            divAttachment.Visible = true;
            gvReqsnAttachment.DataSource = dtAttachment;
            gvReqsnAttachment.DataBind();
            //BindAttchement(hdnFilePath.Value);
        }
        else
        {
            divAttachment.Visible = false;
            gvReqsnAttachment.DataSource = dtAttachment;
            gvReqsnAttachment.DataBind();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        BindReqsnAttchment();
    }
    public void imgbtnDelete_Click(object s, EventArgs e)
    {
        try
        {
            string[] arg = (((ImageButton)s).CommandArgument.Split(new char[] { ',' }));
            int res = BLL_POLOG_Register.POLOG_Delete_Attachments(arg[0].ToString(), arg[1].ToString(), 2714);
            //string SavePath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\Uploads\\Files_Uploaded");
            //string SavePath = ("../Uploads/Files_Uploaded");
            //string File_ID = arg[0];
            //File_ID = File_ID.PadLeft(8, '0');
            //string F1 = Mid(File_ID, 0, 2);
            //string F2 = Mid(File_ID, 2, 2);
            //string F3 = Mid(File_ID, 4, 2);
            //string filePath = SavePath + "\\" + F1 + "\\" + F2 + "\\" + F3 + "\\" + arg[2];
            //if (File.Exists(filePath))
            //{
            //    File.Delete(filePath);
            //}




        }
        catch
        { }
    }
    protected void ImgView_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string FilePath = UDFLib.ConvertStringToNull(arg[0]);
        //iFrame1.Attributes["src"] = FilePath;
        BindAttchement(FilePath);

        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + FilePath + "');", true);
    }
    protected void BindAttchement(string FilePath)
    {
        if (FilePath != "")
        {
           // divIframe.Visible = true;
            Random r = new Random();
            string ver = r.Next().ToString();
            //iFrame1.Attributes.Add("src", FilePath + "?ver=" + ver);
        }
    }
    protected void gvReqsnAttachment_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnImg = (ImageButton)e.Row.FindControl("ImgView");
            ImageButton imgDownload = (ImageButton)e.Row.FindControl("imgDownload");

            string SavePath = ("../Uploads/Files_Uploaded");
            string File_ID = DataBinder.Eval(e.Row.DataItem, "Id").ToString();
            File_ID = File_ID.PadLeft(8, '0');
            string F1 = Mid(File_ID, 0, 2);
            string F2 = Mid(File_ID, 2, 2);
            string F3 = Mid(File_ID, 4, 2);
            string filePath = SavePath + "/" + F1 + "/" + F2 + "/" + F3 + "/" + DataBinder.Eval(e.Row.DataItem, "File_Path").ToString();
            btnImg.CommandArgument = filePath;
            imgDownload.CommandArgument = filePath;
            //hdnFilePath.Value = filePath;

        }
    }
    protected void ImgDownload_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string FilePath = UDFLib.ConvertStringToNull(arg[0]);

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + FilePath + "');", true);
    }
}