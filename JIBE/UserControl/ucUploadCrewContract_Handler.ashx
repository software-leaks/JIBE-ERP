<%@ WebHandler Language="C#" Class="ucUploadCrewContract_Handler" %>

using System;
using System.Web;
using System.IO;
using SMS.Business.Crew;

public class ucUploadCrewContract_Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
            
            HttpPostedFile file = context.Request.Files["Filedata"];

            int CrewID = Convert.ToInt32(context.Request["crewid"]);
            int VoyID = Convert.ToInt32(context.Request["voyid"]);            
            int UserID = Convert.ToInt32(context.Request["userid"]);
            int StageID = Convert.ToInt32(context.Request["stageid"]);
            
            //Guid GUID = Guid.NewGuid();
            //String FileName = file.FileName;
            //String FileGuid = GUID.ToString();
            //String FileExtension = Path.GetExtension(file.FileName).ToLower();

            string sFileName = CrewID.ToString() + "_" + VoyID.ToString() + "_" + StageID.ToString() + ".pdf";

            int DocID = objBLLCrew.Insert_CrewAgreementRecord(CrewID, VoyID, StageID, 0, "Crew Agreement - Signed by Office", sFileName, sFileName, UserID);

            if (DocID > 0)
            {
                string Upload_Path = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/CrewDocuments/");
                file.SaveAs(Upload_Path + "\\" + sFileName);

                context.Response.Write(DocID.ToString());
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



