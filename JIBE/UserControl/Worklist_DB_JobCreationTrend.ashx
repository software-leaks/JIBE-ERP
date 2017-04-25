<%@ WebHandler Language="C#" Class="Worklist_DB_JobCreationTrend" %>

using System;
using System.Web;
using SMS.Business.Technical;
using System.Data;

public class Worklist_DB_JobCreationTrend : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        string strOut = "";
        try
        {
            BLL_Tec_Worklist objBllWL = new BLL_Tec_Worklist();
            DataTable dt = objBllWL.Get_WL_JobCreationTrend();

            foreach (DataRow dr in dt.Rows)
            {
                if (strOut.Length > 0) 
                    strOut += ",";
                
                strOut += "{" +
                    "\"label\":\"" + dr["Data_Label"].ToString() + "\"" + 
                    ",\"data\":\"" + dr["Data_Value"].ToString() + "\"" +
                    ",\"color\":\"" + dr["Data_Color"].ToString() + "\"" +
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
