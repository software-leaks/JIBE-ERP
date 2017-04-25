<%@ WebHandler Language="C#" Class="Worklist_fullCalendar" %>

using System;
using System.Web;
using SMS.Business.Technical;
using System.Data;

public class Worklist_fullCalendar : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        string strOut = "";
        try
        {
            string JType = context.Request["J"].ToString();
            string Dt = context.Request["Dt"].ToString();
            
            DateTime? StartDate = UDFLib.ConvertDateToNull(context.Request["SDt"]);
            DateTime? EndDate = UDFLib.ConvertDateToNull(context.Request["EDt"]);
            int UserID    = UDFLib.ConvertToInteger(context.Request["UserID"]);
            
            //int WLID = UDFLib.ConvertToInteger(context.Request["WLID"]);
            //int VID = UDFLib.ConvertToInteger(context.Request["VID"]);
            //int OFFID = UDFLib.ConvertToInteger(context.Request["OFFID"]);

            BLL_Tec_Worklist objBllWL = new BLL_Tec_Worklist();
            DataTable dt = objBllWL.Get_WorkList_Calendar(JType, StartDate, EndDate, UserID);

            foreach (DataRow dr in dt.Rows)
            {
                if (strOut.Length > 0) 
                    strOut += ",";
                if (JType == "WL")
                {
                    strOut += "{" +
                        "\"id\":" + dr["WLID"].ToString() +
                        ",\"title\":\"" + dr["JOBE_CODE"].ToString() + "\"" +
                        ",\"start\": \"" + dr["DueDate"].ToString() + "\"" +
                        ",\"className\": \"" + dr["JobStatus"].ToString() + "\"" +
                        ",\"url\": \"javascript:viewWorklistDetails(" + dr["VID"].ToString() + "," + dr["WLID"].ToString() + "," + dr["OFFID"].ToString() + ");\"" +
                        "}";
                }
                else
                {
                    strOut += "{" +
                        "\"id\":" + dr["JID"].ToString() +
                        ",\"title\":\"" + dr["JOBE_CODE"].ToString() + "\"" +
                        ",\"start\": \"" + dr["DueDate"].ToString() + "\"" +
                        ",\"className\": \"" + dr["JobStatus"].ToString() + "\"" +
                        ",\"url\": \"javascript:viewPMSJobDetails(" + dr["VID"].ToString() + "," + dr["JID"].ToString() + "," + dr["JHID"].ToString() + ");\"" +
                        "}";

                }
            }
        }
        catch { }

        if (strOut.Length > 0) 
            strOut = "[" + strOut + "]";
        
        //strOut =  "[{\"id\":111,\"title\":\"Event1\",\"start\":\"2013-07-10\",\"url\":\"yahoo.com\"},{\"id\":222,\"title\":\"Event2\",\"start\":\"2013-07-20\",\"end\":\"2013-07-22\",\"url\":\"yahoo.com\"}]";
        
        
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