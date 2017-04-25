using System;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SMS.Business.Crew;
public class FileHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string strResponse = "error";
        try
        {
            Guid GUID = Guid.NewGuid();



            string sPath = context.Server.MapPath("\\" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APP_NAME"]) + "\\uploads\\CrewDocuments");
            string sFilePath = sPath + "\\" + GUID.ToString() + Path.GetExtension(context.Request.Files[0].FileName);

            byte[] docBytes = new byte[context.Request.Files[0].InputStream.Length];
            context.Request.Files[0].InputStream.Read(docBytes, 0, docBytes.Length);
            int Res = UploadFile(docBytes, sFilePath, context.Request.Files[0].FileName);
            strResponse = Res.ToString();

        }
        catch
        {

        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(strResponse);

    }
    public int UploadFile(byte[] bFile, string sFilePath, string DisplayName)
    {
        int iAttachID = 0;
        try
        {
            FileStream fileStream = new FileStream(sFilePath, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(bFile, 0, bFile.Length);
            fileStream.Close();

            iAttachID = BLL_Crew_Training.INSERT_Training_Attachment(DisplayName, Path.GetFileName(sFilePath), bFile.Length, 0, 0);
            
        }
        catch { }

        return iAttachID;
    }
        
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}