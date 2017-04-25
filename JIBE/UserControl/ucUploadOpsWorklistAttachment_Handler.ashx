<%@ WebHandler Language="C#" Class="ucUploadOpsWorklistAttachment_Handler" %>

using System;
using System.Web;
using System.IO;
using SMS.Business.Technical;

public class ucUploadOpsWorklistAttachment_Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            BLL_Tec_Worklist objBLLTech = new BLL_Tec_Worklist();
            
            int Worklist_ID = Convert.ToInt32(context.Request["Worklist_ID"]);
            int Vessel_ID = Convert.ToInt32(context.Request["Vessel_ID"]);
            int WL_Office_ID = Convert.ToInt32(context.Request["WL_Office_ID"]);
            int UserID = Convert.ToInt32(context.Request["UserID"]);

            HttpPostedFile file = context.Request.Files["Filedata"];
            if (file != null)
            {                
                if (Vessel_ID != 0 && Worklist_ID != 0)
                {
                    Guid GUID = Guid.NewGuid();

                    string FileName = file.FileName;
                    FileName = UDFLib.Remove_Special_Characters(FileName);
                    string File_Path = GUID.ToString() + System.IO.Path.GetExtension(FileName);
                                        
                    int SIZE_BYTES = file.ContentLength;

                    if (SIZE_BYTES > 0)
                    {
                        int FileID = objBLLTech.Insert_Worklist_Attachment(Vessel_ID, Worklist_ID, WL_Office_ID, FileName, File_Path, SIZE_BYTES, UserID);
                        if (FileID > 0)
                        {
                            string Upload_Path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/Technical/") + Vessel_ID + "_" + Worklist_ID + "_" + WL_Office_ID + "_" + "O" + "_" + FileID.ToString() + "_" + File_Path; 
                            file.SaveAs(Upload_Path);
                            context.Response.Write(FileID.ToString());
                        }
                        else
                            context.Response.Write("-1");
                    }
                }
            }

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



