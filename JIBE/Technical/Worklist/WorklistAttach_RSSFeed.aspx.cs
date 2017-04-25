using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.Crew;
using SMS.Business.Technical;
using System.IO;
using System.Text;
using System.Configuration;
using System.Xml;
using System.ServiceModel.Syndication;

public partial class Technical_WorklistAttach_RSSFeed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Load_Attachments();
        }
        
    }

    protected void Load_Attachments()
    {
        BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();

        string Vessel_ID = GetQueryString("vid");
        string Worklist_ID = GetQueryString("wlid");
        string WL_Office_ID = GetQueryString("wl_off_id");
        string AttID = GetQueryString("AttID");


        if (!IsPostBack)
        {

            if (Vessel_ID != "" && Worklist_ID != "" && WL_Office_ID != "")
            {
                DataTable dt = objBLL.Get_Worklist_Attachments(UDFLib.ConvertToInteger(Vessel_ID), UDFLib.ConvertToInteger(Worklist_ID), UDFLib.ConvertToInteger(WL_Office_ID), UDFLib.ConvertToInteger(Session["UserID"]));
                GenerateRSS(dt);
            }
        }
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

    public string GetQueryString(string Query)
    {
        try
        {
            if (Request.QueryString[Query] != null)
            {
                return Request.QueryString[Query].ToString();
            }
            else
                return "";
        }
        catch { return ""; }
    }

    public string GetFileName(string Path)
    {
        try
        {
            return System.IO.Path.GetFileName(Path);
        }
        catch { return ""; }
    }

    private string GetFullyQualifiedUrl(string url)
    {
        return string.Concat(Request.Url.GetLeftPart(UriPartial.Authority), ResolveUrl(url));
    }

    protected void GenerateRSS(DataTable dt)
    {
        Response.Clear();
        Response.ContentType = "application/rss+xml";
        XmlTextWriter objX = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
        objX.WriteStartDocument();
        objX.WriteStartElement("rss");
        objX.WriteAttributeString("version", "2.0");
        objX.WriteAttributeString("xmlns:media", "http://search.yahoo.com/mrss/");
        objX.WriteAttributeString("xmlns:atom", "http://www.w3.org/2005/Atom");
        objX.WriteStartElement("channel");
        

        //SqlCommand cmd = new SqlCommand("Select TOP 10 * From Articles ORDER BY ID DESC", new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString));
        //cmd.Connection.Open();
        //SqlDataReader dr = cmd.ExecuteReader();

        objX.WriteElementString("title", "RSS Feed");
        objX.WriteElementString("link", System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "technical/worklist/worklist.aspx");
        objX.WriteElementString("description", "Worklist");
        objX.WriteElementString("language", "en-us");
        objX.WriteElementString("ttl", "60");
        objX.WriteElementString("image", System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "Images/companylogo.png");
        objX.WriteElementString("lastBuildDate", String.Format("{0:R}", DateTime.Now));

        foreach (DataRow dr in dt.Rows)
        {
            objX.WriteStartElement("item");
                objX.WriteElementString("title", dr["attach_name"].ToString());
                objX.WriteElementString("media:description", dr["attach_name"].ToString());
                objX.WriteElementString("link", System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "uploads/technical/" + GetFileName(dr["attach_path"].ToString()));
            
                objX.WriteStartElement("media:thumbnail");
                objX.WriteAttributeString("URL", System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "uploads/technical/" + GetFileName(dr["attach_path"].ToString()));            
                objX.WriteEndElement();

                objX.WriteStartElement("media:content");
                objX.WriteAttributeString("URL", System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "uploads/technical/" + GetFileName(dr["attach_path"].ToString()));
                objX.WriteEndElement();
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