<%@ WebHandler Language="C#" Class="CrewDocumentUploader" %>

using System;
using System.Web;
using System.IO;

using SMS.Business.Crew;


public class CrewDocumentUploader : IHttpHandler
{
    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

    public void ProcessRequest(HttpContext context)
    {
        string DocumentName = ""; string DocFileName = ""; int SizeByte = 0; int DocTypeID = 0; int Created_By = 0;

        try
        {
            HttpPostedFile file = context.Request.Files["Filedata"];

            int CrewID = (Int32.Parse(context.Request["id"]));
            string userid = context.Request["userid"];
            string uploadpath = context.Request["uploadpath"];

                        
            Guid GUID = Guid.NewGuid();
            String FileExtension = Path.GetExtension(file.FileName).ToLower();
            DocumentName = file.FileName.Replace(FileExtension,"");
            DocFileName = GUID.ToString() + FileExtension;
            
            if(userid.Length > 0)
            Created_By = int.Parse(userid);
                
            file.SaveAs(uploadpath + "\\" + DocFileName);

            int DocID = objCrewBLL.INS_CrewDocuments(CrewID, DocumentName, DocFileName, FileExtension, DocTypeID, Created_By);
            
            //int result = 0;


            if (DocID > 0)
                context.Response.Write(DocID);
            else
                context.Response.Write("0");
        }
        catch
        {
            context.Response.Write("-1");
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}