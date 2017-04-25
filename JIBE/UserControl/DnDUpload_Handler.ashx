<%@ WebHandler Language="C#" Class="DnDUpload_Handler" %>
using System;
using System.Web;
using SMS.Business.Technical;

public class DnDUpload_Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        handleFileUpload(context);
    }
    /// <summary>
    /// Checks if a file is sent to the server
    /// and saves it to the Uploads folder.
    /// </summary>
    private void handleFileUpload(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        try
        {
            string DocumentName = "", DocFileName = "", UploadPath ="";

            DocFileName = System.Web.HttpContext.Current.Request.Headers["X-FILE-NAME"];
            UploadPath = System.Web.HttpContext.Current.Request.Headers["X-UPLOAD-PATH"];
            
            int Worklist_ID = Convert.ToInt32(context.Request.Headers["WORKLIST_ID"]);
            int Vessel_ID = Convert.ToInt32(context.Request.Headers["VESSEL_ID"]);
            int WL_Office_ID = Convert.ToInt32(context.Request.Headers["WL_OFFICE_ID"]);
            int UserID = Convert.ToInt32(context.Request.Headers["USERID"]);

            Guid GUID = Guid.NewGuid();
            String FileExtension = System.IO.Path.GetExtension(DocFileName).ToLower();
            DocumentName = DocFileName.Replace(FileExtension, "");
            DocFileName = GUID.ToString() + FileExtension;

            if(string.IsNullOrEmpty(UploadPath))
                UploadPath= System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Uploads/{0}", DocFileName));
            else
                UploadPath = System.Web.HttpContext.Current.Server.MapPath(string.Format("~/{0}", UploadPath + "/" + DocFileName));
            
            System.IO.Stream inputStream = System.Web.HttpContext.Current.Request.InputStream;
            System.IO.FileStream fileStream = new System.IO.FileStream(UploadPath, System.IO.FileMode.OpenOrCreate);
            inputStream.CopyTo(fileStream);
            long SIZE_BYTES = fileStream.Length;
            fileStream.Close();
            
            
            BLL_Tec_Worklist objBLLTech = new BLL_Tec_Worklist();
            int FileID = objBLLTech.Insert_Worklist_Attachment(Vessel_ID, Worklist_ID, WL_Office_ID, DocFileName, DocFileName, SIZE_BYTES, UserID);
            
            context.Response.Write(DocFileName);
        }
        catch
        {
            context.Response.Write("Error uploading file!!");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
