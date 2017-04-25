<%@ WebHandler Language="C#" Class="EmailAttachment" %>

using System;
using System.Web;
using System.IO;
using SMS.Business.Infrastructure;

public class EmailAttachment : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {


            string DocumentName = ""; string DocFileName = "";

            HttpPostedFile file = context.Request.Files["Filedata"];

            int EmailID = Convert.ToInt32(context.Request["emailid"]);

            //string uploadpath = @"\\server01\";
            //uploadpath = uploadpath + context.Request["uploadpath"];
            string uploadpath = context.Request["uploadpath"];


            Guid GUID = Guid.NewGuid();
            String FileExtension = Path.GetExtension(file.FileName).ToLower();
            DocumentName = file.FileName.Replace(FileExtension, "");
            DocFileName = GUID.ToString() + FileExtension;


            int sts = BLL_Infra_Common.Insert_EmailAttachedFile(EmailID, DocumentName, uploadpath + @"\" + DocFileName);

            if (sts > 0)
            {

                file.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/"+context.Request["uploadpath"]) + "\\" + DocFileName);

                context.Response.Write(sts);
            }
            else
                context.Response.Write("0");
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



