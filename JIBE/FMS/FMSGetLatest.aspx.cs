using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Management;
using System.IO;

using SMS.Business.FMS;

public partial class GetLatest : System.Web.UI.Page
{
    BLL_FMS_Document objFMS = new BLL_FMS_Document();

    string fileID = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["USERID"]) == "")
        {
            Response.Write("<center><br><br><h2><font color=gray>Session is lost. Please click on the Logout option and login again.</font></h2></center>");
            Response.End();
        }

        if (Convert.ToString(Request.QueryString["FileID"]) == "")
        {
            Response.Write("<center><br><br><h2><font color=gray>Please select a document to get the latest version.</font></h2></center>");
            Response.End();
        }

        string DocVersion = "0";
        if (Request.QueryString["DocVer"] != null)
        {
            if (Request.QueryString["DocVer"].ToString() != "")
            {
                string[] temp = Request.QueryString["DocVer"].ToString().Split('-');
                fileID = temp[0];
                DocVersion = temp[1];
            }
        }
        else
        {
            fileID = Request.QueryString["FileID"].ToString();

        }
        DataSet dsFileDetails = objFMS.getFileDetailsByID(int.Parse(fileID),int.Parse(DocVersion) );



        if (dsFileDetails.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsFileDetails.Tables[0].Rows[0];
            string navURL = "";
            //if version is not supplied with the querystring
            if (DocVersion == "")
                DocVersion = dr["Version"].ToString();

           
            navURL = "../"+dr["filepath"].ToString();
            string fileName = dr["LogFileID"].ToString();
            OpenFileExternal(navURL, fileName);
        }
    }


    /// <summary>
    /// this is uses for the display a document in the external window.
    /// </summary>
    /// <param name="sendepr"></param>
    /// <param name="Args"></param>
    public void OpenFileExternal(string url, string fileName)
    {
        string filepath = Server.MapPath(url);
        FileInfo file = new FileInfo(filepath);
        bool IE = Request.UserAgent.IndexOf("MSIE") > -1;

        if (file.Exists)
        {
            Response.ClearContent();
            if (!IE)
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            else
                Response.AddHeader("Content-Disposition", "inline; filename=" + fileName); 
            
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = ReturnExtension(file.Extension.ToLower());
            Response.TransmitFile(file.FullName);
            Response.End();

        }
    }


    /// <summary>
    /// return a file extension based on passed file extension of the file.
    /// </summary>
    /// <param name="fileExtension"></param>
    /// <returns></returns>
    public string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }
}
