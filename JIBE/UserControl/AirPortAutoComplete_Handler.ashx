<%@ WebHandler Language="C#" Class="AirPortAutoComplete_Handler" %>

using System;
using System.Web;
using System.Data;

using SMS.Business.Crew;

public class AirPortAutoComplete_Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        string list = context.Request["list"];
        string query = context.Request["query"];
        
        string strOut = "";

        try
        {
            if (query != null && query != "")
            {

                BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
                DataTable dt = objCrew.Get_AirportList(query);

                string sugg = "";
                string data = "";
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sugg += (sugg.Length > 0) ? "," : "";
                    sugg += "'" + dt.Rows[i]["text"].ToString().Replace("'","") + "'";

                    data += (data.Length > 0) ? "," : "";
                    data += "'" + dt.Rows[i]["value"].ToString() + "'";
                    
                }


                strOut = @"{query:'" + query + "',suggestions:[" + sugg + "], data:[" + data + "]}";
            }
        }
        catch { }
        context.Response.ContentType = "text/plain";
        context.Response.Write(strOut);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}