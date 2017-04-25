<%@ WebHandler Language="C#" Class="PURC_ReqsnAttachment" %>

using System;
using System.Web;
using System.IO;
using SMS.Business.PURC;


public class PURC_ReqsnAttachment :  IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();

            string DocumentName = ""; string DocFileName = "";

            HttpPostedFile file = context.Request.Files["Filedata"];

            string VesselID = context.Request["vesselid"];
            string userid = context.Request["userid"];
            string uploadpath = context.Request["uploadpath"];
            string Reqsnno = context.Request["reqsnno"];
            string SuppCode = context.Request["suppcode"];

            Guid GUID = Guid.NewGuid();
            String FileExtension = Path.GetExtension(file.FileName).ToLower();
            DocumentName = file.FileName.Replace(FileExtension, "");
            DocFileName = GUID.ToString() + FileExtension;

          

          

            int sts = objTechService.SaveAttachedFileInfo(VesselID, Reqsnno, SuppCode, FileExtension, DocumentName, "../Uploads/Purchase/" + DocFileName, userid,0);

            if (sts > 0)
            {
               
                file.SaveAs(uploadpath + "\\" + DocFileName);

                ASP.global_asax.AttachedFile += sts.ToString()+",";
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