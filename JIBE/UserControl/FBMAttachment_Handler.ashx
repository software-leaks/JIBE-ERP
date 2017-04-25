<%@ WebHandler Language="C#" Class="PURC_ReqsnAttachment" %>

using System;
using System.Web;
using System.IO;
using SMS.Business.PURC;


public class PURC_ReqsnAttachment : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
 
            HttpPostedFile file = context.Request.Files["Filedata"];
            Guid GUID = Guid.NewGuid();

            String FileName = file.FileName;
            String FileGuid = GUID.ToString();
            String FileExtension = Path.GetExtension(file.FileName).ToLower();
            
            string tempuploadpath= System.Web.HttpContext.Current.Server.MapPath("~/QMS/TempUpload/");

            file.SaveAs(tempuploadpath + "\\" + FileGuid + FileExtension);

            context.Session["Attached_Files"] += FileGuid + "|" + FileName + "|" + FileExtension + "?";
            context.Response.Write("1");
        }
        catch
        { context.Response.Write("-1"); }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}