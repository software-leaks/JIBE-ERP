<%@ WebHandler Language="C#" Class="Crew_BirthDayCalendar" %>

using System;
using System.Web;
using SMS.Business.Technical;
using System.Data;
using SMS.Business.Crew;

public class Crew_BirthDayCalendar : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        string strOut = "";
        try
        {
            int UserID = 0;  //UDFLib.ConvertToInteger(context.Session["USERID"]);
             DataTable dt = BLL_Crew_CrewList.Get_Crew_Birthday_Calendar(UserID);

            foreach (DataRow dr in dt.Rows)
            {
                if (strOut.Length > 0) 
                    strOut += ",";
                
                    strOut += "{" +
                        "\"id\":" + dr["CREWID"].ToString() +
                        ",\"title\":\"" + dr["STAFF_CODE"].ToString() + "-" + dr["RANK_SHORT_NAME"].ToString() + "-" + dr["STAFF_FULLNAME"].ToString() + "\"" +
                        ",\"start\": \"" + dr["BIRTH_DAY_DATE"].ToString() + "\"" +
                        ",\"className\": \"crewIcon\"" +
                        ",\"icon\": \"" + dr["PhotoURL"].ToString() + "\"" +
                        ",\"url\": \"javascript:viewCrewDetails(" + dr["CrewID"].ToString() + ");\"" +
                        "}";
                
            }
        }
        catch { }

        if (strOut.Length > 0) 
            strOut = "[" + strOut + "]";
        
        context.Response.ContentType = "text/plain";
        context.Response.Write(strOut);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}

//[{"id":111,"title":"Event1","start":"2013-07-10","url":"http:\/\/yahoo.com\/"},{"id":222,"title":"Event2","start":"2013-07-20","end":"2013-07-22","url":"http:\/\/yahoo.com\/"}]