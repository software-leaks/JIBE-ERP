using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;

public class SetUserStyle : System.Web.UI.Page
{
    private static string _style = "";
    public SetUserStyle()
    {
        _style = Session["USERSTYLE"].ToString();
    }
    public static Literal AddThemeInHeader()
    {
        SetUserStyle obj = new SetUserStyle();
        Literal link = new Literal();
        string APP_NAME = ConfigurationManager.AppSettings["APP_NAME"].ToString();
        link.Text = "<link href=\"/"+ APP_NAME +"/STYLES/" + _style + "\" rel=\"stylesheet\" type=\"text/css\" />";
        return link;
    }
}