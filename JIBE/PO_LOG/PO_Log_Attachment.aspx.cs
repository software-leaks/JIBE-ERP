using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.PURC;
using System.IO;
using Telerik.Web.UI;
using SMS.Business.Infrastructure;
using AjaxControlToolkit4;
using SMS.Business.POLOG;
using System.IO;


public partial class PO_LOG_PO_Log_Attachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (AjaxFileUpload1.IsInFileUploadPostBack)
        {

        }
        else
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    if (Request.QueryString["DocType"] != null)
                    {
                        if (Request.QueryString["DocType"] == "PO_DOCUMENT")
                        {
                            lblReqsnNumber.Text = "Supply ID : " + Request.QueryString["ID"].ToString();
                        }
                        else if (Request.QueryString["DocType"] == "Invoice")
                        {
                            lblReqsnNumber.Text = "Invoice ID : " + Request.QueryString["ID"].ToString();
                        }

                    }
                    BindReqsnAttchment();
                }

            }
            lblErrorMsg.Text = "";
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public void BindReqsnAttchment()
    {


        BLL_PURC_Purchase objPurch = new BLL_PURC_Purchase();
        DataTable dtAttachment = BLL_POLOG_Register.POLOG_Get_Attachments(UDFLib.ConvertStringToNull(Request.QueryString["ID"].ToString()), Request.QueryString["DocType"].ToString());

        gvReqsnAttachment.DataSource = dtAttachment;
        gvReqsnAttachment.DataBind();
    }

    public void imgbtnDelete_Click(object s, EventArgs e)
    {
        try
        {
            string[] arg = (((ImageButton)s).CommandArgument.Split(new char[] { ',' }));
            int res = BLL_POLOG_Register.POLOG_Delete_Attachments(arg[0].ToString(), arg[1].ToString(), Convert.ToInt16(GetSessionUserID()));
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

            BindReqsnAttchment();


        }
        catch
        { }
    }

    

    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {
            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
            
            Byte[] fileBytes = file.GetContents();
            string SavePath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\Uploads\\Files_Uploaded");
            Guid GUID = Guid.NewGuid();
            string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);


            string FileID = BLL_POLOG_Register.POLOG_Insert_AttachedFile(UDFLib.ConvertStringToNull(Request.QueryString["ID"].ToString()), Path.GetExtension(file.FileName),
                UDFLib.Remove_Special_Characters(Path.GetFileName(file.FileName)), Flag_Attach, Request.QueryString["DocType"].ToString(), Convert.ToInt16(GetSessionUserID()));

            FileID = FileID.PadLeft(8, '0');
            string F1 = Mid(FileID, 0, 2);
            string F2 = Mid(FileID, 2, 2);
            string F3 = Mid(FileID, 4, 2);
            if (!Directory.Exists(SavePath + F1 + "\\" + F2 + "\\" + F3))
            {
                Directory.CreateDirectory(SavePath + F1 + "\\" + F2 + "\\" + F3);
            }



            string FullFilename = Path.Combine(SavePath + "\\" + F1 + "\\" + F2 + "\\" + F3, FileID.ToString() + Path.GetExtension(file.FileName));

            //string FullFilename = Path.Combine(sPath, GUID.ToString() + Path.GetExtension(file.FileName));
            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();

            ScriptManager.RegisterStartupScript(this, typeof(Page), "refresh", "fn_OnClose(a,b)", true);
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
    protected void btnLoadFiles_Click(object sender, EventArgs e)
    {

        BindReqsnAttchment();
    }
    protected void ImgDownload_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string FilePath = UDFLib.ConvertStringToNull(arg[0]);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + FilePath + "');", true);
    }
    protected void gvReqsnAttachment_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //ImageButton btnImg = (ImageButton)e.Row.FindControl("ImgView");
            ImageButton imgDownload = (ImageButton)e.Row.FindControl("imgDownload");
            string SavePath = ("../Uploads/Files_Uploaded");
            string File_ID = DataBinder.Eval(e.Row.DataItem, "Id").ToString();
            File_ID = File_ID.PadLeft(8, '0');
            string F1 = Mid(File_ID, 0, 2);
            string F2 = Mid(File_ID, 2, 2);
            string F3 = Mid(File_ID, 4, 2);
            string filePath = SavePath + "/" + F1 + "/" + F2 + "/" + F3 + "/" + DataBinder.Eval(e.Row.DataItem, "File_Path").ToString();
          
            imgDownload.CommandArgument = filePath;

        }
    }
}