<%@ WebHandler Language="C#" Class="Worklist_DB_JobsCreated_InLast24Hours" %>

using System;
using System.Web;
using SMS.Business.Technical;
using System.Data;

public class Worklist_DB_JobsCreated_InLast24Hours : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        string strOut = "";
        try
        {
            BLL_Tec_Worklist objBllWL = new BLL_Tec_Worklist();
            DataTable dt = objBllWL.Get_WL_JobsCreated_InLast24Hours();

            foreach (DataRow dr in dt.Rows)
            {
                if (strOut.Length > 0) 
                    strOut += ",";
                
                strOut += "[" +
                    "\"" + dr["VESSEL_SHORT_NAME"].ToString() + "\"" +
                    ",\"" + dr["WORKLIST_ID"].ToString() + "\"" +
                    ",\"" + dr["ASSIGNOR_NAME"].ToString() + "\"" +
                    ",\"" + dr["DATE_RAISED"].ToString() + "\"" +
                    ",\"" + dr["DATE_ESTMTD_CMPLTN"].ToString() + "\"" +
                    ",\"" + dr["DATE_COMPLETED"].ToString() + "\"" +
                    ",\"" + dr["NCR_YN"].ToString() + "\"" +                     
                    "]";
            }
        }
        catch { }
    
        strOut = "{\"aaData\": [" + strOut + "]}";
        
        context.Response.ContentType = "text/plain";
        context.Response.Write(strOut);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
