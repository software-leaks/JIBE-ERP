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

public partial class QMS_LoadTree : System.Web.UI.Page
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
     
    
    //string dir;
    //if(Request.Form["dir"] == null || Request.Form["dir"].Length <= 0)
    //    dir = "/";
    //else
    //    dir = Server.UrlDecode(Request.Form["dir"]);
    //System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dir);
    //Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">\n");
    //foreach (System.IO.DirectoryInfo di_child in di.GetDirectories())
    //    Response.Write("\t<li class=\"directory collapsed\"><a href=\"#\" rel=\"" + dir + di_child.Name + "/\">" + di_child.Name + "</a></li>\n");
    //foreach (System.IO.FileInfo fi in di.GetFiles())
    //{
    //    string ext = ""; 
    //    if(fi.Extension.Length > 1)
    //        ext = fi.Extension.Substring(1).ToLower();			
    //    Response.Write("\t<li class=\"file ext_" + ext + "\"><a href=\"#\" rel=\"" + dir + fi.Name + "\">" + fi.Name + "</a></li>\n");		
    //}
    //Response.Write("</ul>");


}