using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.QMS;
using System.IO;
using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class TMSA_Report_ProcedureLoadTree : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document();

    protected void Page_Load(object sender, EventArgs e)
    {


        string sTree = "";
        string dir;
        int pid = 0;
       

        if (Request.Form["dir"] == null || Request.Form["dir"].Length <= 0 || Request.Form["dir"] == "DOCUMENTS")
            dir = "DOCUMENTS/";
        else
            dir = Server.UrlDecode(Request.Form["dir"]);

        pid = UDFLib.ConvertToInteger(Request.Form["pid"]);

        String originalPath = Request.UrlReferrer.AbsolutePath.ToString();
        String parentDirectory = UDFLib.GetPageURL(originalPath);

        DataSet ds = objQMS.getFolderAsync(UDFLib.ConvertToInteger(Session["Userid"]), pid, parentDirectory);

        DataTable dtDir = ds.Tables[0];
        DataTable dtFiles = ds.Tables[1];

        sTree = "<ul class=\"jqueryFileTree\" style=\"display: none;\">\n";

        foreach (DataRow dir_child in dtDir.Rows)
            sTree += "\t<li class=\"directory collapsed\"><a href=\"#\" id=" + dir_child["ID"] + " rel=\"" + dir + dir_child["LogFileID"] + "/\">" + dir_child["LogFileID"] + "</a></li>\n";


        foreach (DataRow fi in dtFiles.Rows)
        {
            string ext = "";
            string filename = fi["LogFileID"].ToString();

            if (Path.GetExtension(filename).Length > 1)
                ext = Path.GetExtension(filename).Substring(1).ToLower();

            sTree += "\t<li class=\"file ext_" + ext + "\"><a href=\"#\" id=" + fi["ID"] + " rel=\"" + dir + filename + "\">" + filename + "</a></li>\n";
        }
        sTree += "</ul>";
        tree.Text = sTree;
        
    }

 

}