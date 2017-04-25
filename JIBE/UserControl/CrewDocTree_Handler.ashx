<%@ WebHandler Language="C#" Class="CrewDocTree_Handler" %>

using System;
using System.Web;

public class CrewDocTree_Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string strOut = "";
        try
        {
            if (context.Request["root"].ToString() == "source")
            {
                strOut = @"[{'text': '1. Pre Lunch (120 min)','expanded': true,'classes': 'important',
                        'children':
                            [{'text': '1.1 The State of the Powerdome (30 min)'},
                             {'text': '1.2 The Future of jQuery (30 min)'},
                             {'text': '1.2 jQuery UI - A step to richnessy (60 min)'}
		                    ]
	                    }
                       ]";
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