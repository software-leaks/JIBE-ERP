<%@ WebHandler Language="C#" Class="JQueryAttachmentUploader" %>

using System;
using System.Web;
using System.IO;

using SMS.Business.Technical;


public class JQueryAttachmentUploader : IHttpHandler
{
    BLL_Tec_Worklist objDLLTech = new BLL_Tec_Worklist();

    public void ProcessRequest(HttpContext context)
    {
        string DocumentName = ""; string DocFileName = ""; int SizeByte = 0; 

        try
        {
            HttpPostedFile file = context.Request.Files["Filedata"];
            string uploadpath = context.Request["uploadpath"];

            int Vessel_ID = UDFLib.ConvertToInteger(context.Request["vessel_id"]);
            int Worklist_ID = UDFLib.ConvertToInteger(context.Request["worklist_id"]);
            int WL_Office_ID = UDFLib.ConvertToInteger(context.Request["wl_office_id"]);
            int Created_By = UDFLib.ConvertToInteger(context.Request["userid"]);

                        
            Guid GUID = Guid.NewGuid();
            String FileExtension = Path.GetExtension(file.FileName).ToLower();
            DocumentName = file.FileName.Replace(FileExtension,"");
            DocFileName = GUID.ToString() + FileExtension;
            
                
            file.SaveAs(uploadpath + "\\" + DocFileName);


            int DocID = objDLLTech.Insert_Worklist_Attachment(Vessel_ID, Worklist_ID, WL_Office_ID, DocumentName, DocFileName, SizeByte, Created_By);
        
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