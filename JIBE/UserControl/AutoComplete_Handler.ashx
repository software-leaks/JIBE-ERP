<%@ WebHandler Language="C#" Class="AutoComplete_Handler" %>

using System;
using System.Web;
using System.Data;

using SMS.Business.DMS;

public class AutoComplete_Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        string list = context.Request["list"];
        string query = context.Request["query"];
        
        string strOut = "";

        try
        {
           if (list!=null && list!="")
            {

                BLL_DMS_Admin  objBLL =new BLL_DMS_Admin();
                DataTable dt = objBLL.Get_ListItemsFromListSource(list,query);

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