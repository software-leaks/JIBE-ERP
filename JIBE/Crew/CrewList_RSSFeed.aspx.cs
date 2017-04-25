using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.Crew;
using System.Xml;
using System.Text;
using System.ServiceModel.Syndication;

public partial class Crew_CrewList_RSSFeed : System.Web.UI.Page
{
    string _App_URL =Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["APP_URL"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();

        int Vessel_ID = 1;
        
        if(Request.QueryString["vid"]!=null)
            Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["vid"].ToString());
        

        int SelectRecordCount = 1;
        DataTable dtFilters = new DataTable();
        dtFilters.Columns.Add("VesselManager", typeof(int));
        dtFilters.Columns.Add("Fleet", typeof(int));
        dtFilters.Columns.Add("Vessel", typeof(int));
        dtFilters.Columns.Add("RankID", typeof(int));
        dtFilters.Columns.Add("Nationality", typeof(int));
        dtFilters.Columns.Add("Status", typeof(String));
        dtFilters.Columns.Add("ManningOfficeID", typeof(int));
        dtFilters.Columns.Add("EOCDueIn", typeof(int));
        dtFilters.Columns.Add("JoiningDateFrom", typeof(String));
        dtFilters.Columns.Add("JoiningDateTo", typeof(String));
        dtFilters.Columns.Add("SearchText", typeof(String));

        DateTime dtFrom = DateTime.Parse("1900/01/01");
        DateTime dtTo = DateTime.Parse("2900/01/01");

        dtFilters.Rows.Add(1, 0, Vessel_ID, 0, 0, "CURRENT", 0, 0, dtFrom.ToString("yyyy/MM/dd"), dtTo.ToString("yyyy/MM/dd"), "");

        DataTable dt = BLL_Crew_CrewList.Get_Crewlist_Index(dtFilters, GetSessionUserID(),2000, 1, ref SelectRecordCount);

        //RepeaterRSS.DataSource = dt;
        //RepeaterRSS.DataBind();

        GenerateRSS(dt);
    }

    protected string RemoveIllegalCharacters(object input)
    {
        // cast the input to a string  
        string data = input.ToString();

        // replace illegal characters in XML documents with their entity references  
        data = data.Replace("&", "&amp;");
        data = data.Replace("\"", "&quot;");
        data = data.Replace("'", "&apos;");
        data = data.Replace("<", "&lt;");
        data = data.Replace(">", "&gt;");

        return data;
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public string GetFileName(string Path)
    {
        try
        {
            return System.IO.Path.GetFileName(Path);
        }
        catch { return ""; }
    }

    protected void GenerateRSS(DataTable dt)
    {
        Response.Clear();
        Response.ContentType = "application/rss+xml";
        XmlTextWriter objX = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
        //objX.WriteAttributeString("standalone", "yes");

        objX.WriteStartDocument();
        objX.WriteStartElement("rss");
        objX.WriteAttributeString("version", "2.0");
        objX.WriteAttributeString("xmlns:media", "http://search.yahoo.com/mrss/");
        objX.WriteAttributeString("xmlns:atom", "http://www.w3.org/2005/Atom");
        objX.WriteStartElement("channel");

        objX.WriteElementString("title", "Crew RSS Feed");
        objX.WriteElementString("link", _App_URL);
        objX.WriteElementString("description", "CrewList");
        objX.WriteElementString("language", "en-us");

       

        foreach (DataRow dr in dt.Rows)
        {
            objX.WriteStartElement("item");
            objX.WriteElementString("title",  dr["Staff_Code"].ToString() + " - " + dr["Staff_FullName"].ToString() );
            objX.WriteElementString("link", _App_URL+"/Crew/CrewDetails.aspx?ID=" + dr["ID"].ToString());

            //objX.WriteStartElement("guid");
            //objX.WriteAttributeString("isPermaLink", "false");
            //objX.WriteEndElement();

            objX.WriteStartElement("media:thumbnail");
            objX.WriteAttributeString("url", _App_URL+"/uploads/CrewImages/" + dr["PhotoURL"].ToString());
            objX.WriteEndElement();
                        
            objX.WriteStartElement("media:content");
            objX.WriteAttributeString("url", _App_URL+"/uploads/CrewImages/" + dr["PhotoURL"].ToString());
            objX.WriteEndElement();

            //objX.WriteElementString("media:description", dr["Staff_FullName"].ToString());

            objX.WriteEndElement();
        }

        objX.WriteEndElement();
        objX.WriteEndElement();
        objX.WriteEndDocument();
        
        objX.Flush();
        objX.Close();
        Response.End();
    }
}