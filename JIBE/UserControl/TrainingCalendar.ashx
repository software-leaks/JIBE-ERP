<%@ WebHandler Language="C#" Class="TrainingCalendar" %>

using System;
using System.Web;
using SMS.Business.Technical;
using System.Data;
using SMS.Business.LMS;

public class TrainingCalendar : IHttpHandler
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

         DataTable dt =   BLL_LMS_Training.Get_Training_Calendar(JType, StartDate, EndDate, UserID);

            foreach (DataRow dr in dt.Rows)
            {
                if (strOut.Length > 0) 
                    strOut += ",";
                if (JType == "TR")
                {
                    strOut += "{" +
                        "\"id\":" + dr["ID"].ToString() +
                        ",\"title\":\"" + dr["PROGRAM_NAME"].ToString() + "\"" +
                        ",\"start\": \"" + dr["StartDate"].ToString() + "\"" +
                        ",\"className\": \"" + dr["Status"].ToString() + "\"" +
                         //",\"url\": \"javascript:viewWorklistDetails(" + dr["ID"].ToString() + "," + dr["ID"].ToString() + "," + dr["ID"].ToString() + ");\"" +
                        "}";
                }
                else
                {
                    strOut += "{" +
                        "\"id\":" + dr["ID"].ToString() +
                        ",\"title\":\"" + dr["PROGRAM_NAME"].ToString() + "\"" +
                        ",\"start\": \"" + dr["StartDate"].ToString() + "\"" +
                        ",\"className\": \"" + dr["Status"].ToString() + "\"" +
                        // ",\"url\": \"javascript:viewWorklistDetails(" + dr["ID"].ToString() + "," + dr["ID"].ToString() + "," + dr["ID"].ToString() + ");\"" +
                        "}";

                }
            }
        }
        catch { }

        if (strOut.Length > 0) 
            strOut = "[" + strOut + "]";
        
        //strOut =  "[{\"id\":111,\"title\":\"Event1\",\"start\":\"2014-10-10\",\"url\":\"yahoo.com\"},{\"id\":222,\"title\":\"Event2\",\"start\":\"2013-07-20\",\"end\":\"2013-07-22\",\"url\":\"yahoo.com\"}]";
        
        
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