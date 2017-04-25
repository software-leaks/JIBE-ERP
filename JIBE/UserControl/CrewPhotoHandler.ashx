<%@ WebHandler Language="C#" Class="CrewPhotoHandler" %>

using System;
using System.Web;
using System.IO;

using SMS.Business.Crew;


public class CrewPhotoHandler : IHttpHandler
{
    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            HttpPostedFile file = context.Request.Files["Filedata"];

            int CrewID = (Int32.Parse(context.Request["id"]));
            string uploadpath = context.Request["uploadpath"];
            
            Guid GUID = Guid.NewGuid();
            String FileExtension = Path.GetExtension(file.FileName).ToLower();

            string newFileName = GUID.ToString() + FileExtension;

            file.SaveAs(uploadpath + "\\" + newFileName);

            int result = objCrewBLL.UPDATE_CrewPhotoURL(CrewID, newFileName);
            if (result == 1)
                context.Response.Write("1");
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