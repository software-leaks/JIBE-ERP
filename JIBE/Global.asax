<%@ Application Language="C#" %>
<script RunAt="server">

    public static string AttachedFile = "";
    void Application_Start(object sender, EventArgs e)
    {

        // Code that runs on application startup
        //Application["appCtr"] = 0;
        //Application["noOfUsers"] = 0;

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
        string Errormsg = Server.GetLastError().StackTrace;
        Response.Write(Errormsg);
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        //Application.Lock();
        //Application["noOfUsers"] = (int)Application["noOfUsers"] + 1;
        //Application.UnLock(); 
        AttachedFile = "";
        Session["ReqsnCancelLog"] = "0";
        
        Session["VesselCode"] = "";
        Session["Company_Address_GL"] = @"Jibe<br><br>
                                         <br>
                                         <br><br>";


        Session["Company_Name_GL"] = @"Jibe";

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

        //Application.Lock();
        //Application["noOfUsers"] = (int)Application["noOfUsers"] - 1;
        //Application.UnLock(); 

    }
       
</script>
