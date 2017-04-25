//   Google Maps User Control for ASP.Net 
//   ========================
/***
 * 
 * This is the user control to display the google map in the asp.net application. By using this application, we can add images
 * on the google map according to thier longitude and latitude locations.
 * 
 * for further comments any suggestions and querys' 
 * 
   
 * 
 * 
 * */






using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Drawing;
using System.Web.Services;

public partial class GoogleMapForASPNet : System.Web.UI.UserControl
{

    public delegate void PushpinMovedHandler(string pID);
    public event PushpinMovedHandler PushpinMoved;
    // The method which fires the Event

    public void OnPushpinMoved(string pID)
    {
        // Check if there are any Subscribers
        if (PushpinMoved != null)
        {
            // Call the Event
            GoogleMapObject = (GoogleObject)System.Web.HttpContext.Current.Session["GOOGLE_MAP_OBJECT"];
            PushpinMoved(pID);
        }
    }


    #region Properties

    GoogleObject _googlemapobject = new GoogleObject();
    public GoogleObject GoogleMapObject
    {
        get { return _googlemapobject; }
        set { _googlemapobject = value; }
    }


    bool _showcontrols = false;
    public bool ShowControls
    {
        get { return _showcontrols; }
        set { _showcontrols = value; }
    }


    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        //Console.Write(hidEventName.Value);
        //Console.Write(hidEventValue.Value);
        //Fire event for Pushpin Move



        if (hidEventName.Value == "PushpinMoved")
        {
            //Set event name to blank string, so on next postback same event doesn't fire again.
            hidEventName.Value = "";
            OnPushpinMoved(hidEventValue.Value);
        }
        if (!IsPostBack)
        {
            Session["GOOGLE_MAP_OBJECT"] = GoogleMapObject;
        }
        else
        {
            GoogleMapObject = (GoogleObject)Session["GOOGLE_MAP_OBJECT"];
            if (GoogleMapObject == null)
            {
                GoogleMapObject = new GoogleObject();
                Session["GOOGLE_MAP_OBJECT"] = GoogleMapObject;
            }

        }
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "onLoadCall", "<script language='javascript'> if (window.DrawGoogleMap) { DrawGoogleMap(); } </script>");
    }
}
