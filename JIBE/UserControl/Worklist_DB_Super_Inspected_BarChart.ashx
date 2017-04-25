<%@ WebHandler Language="C#" Class="Worklist_DB_NCR_Trend_BarChart" %>

using System;
using System.Web;
using SMS.Business.Technical;
using System.Data;

public class Worklist_DB_NCR_Trend_BarChart : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        string strOut = "";
        try
        {
            BLL_Tec_Worklist objBllWL = new BLL_Tec_Worklist();
            DataTable dt = objBllWL.Get_WL_Super_Inspected_BarChart();

            foreach (DataRow dr in dt.Rows)
            {
                if (strOut.Length > 0) 
                    strOut += ",";
                                
                strOut += "[\"" + dr["MonthName"].ToString() + "\"," + dr["WLCount"].ToString() + "]";
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
