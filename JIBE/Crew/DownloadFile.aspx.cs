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
using SMS.Business.Crew;

public partial class DownloadFile : System.Web.UI.Page
{
    string fileID = "";
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        string DownloadFileURL = Convert.ToString(Request.QueryString["url"]);

        if (Convert.ToString(Session["USERID"]) == "")
        {
            Response.Write("<center><br><br><h2><font color=gray>Session is lost. Please click on the Logout option and login again.</font></h2></center>");
            Response.End();
        }
        // Manning office downloading signed contract
        int iAgreementID = UDFLib.ConvertToInteger(Request.QueryString["AgreementID"]);
        if (iAgreementID != 0)
        {
            DataTable dt = objBLLCrew.Download_Contract_ByManningOffice(iAgreementID, UDFLib.ConvertToInteger(Session["USERID"]));
            if (dt.Rows.Count > 0)
            {
                DownloadFileURL = "~/Uploads/CrewDocuments/" + dt.Rows[0]["DocFilePath"].ToString();
            }
        }

        OpenFileExternal(DownloadFileURL);
       
    }


    /// <summary>
    /// this is uses for the display a document in the external window.
    /// </summary>
    /// <param name="sendepr"></param>
    /// <param name="Args"></param>
    public void OpenFileExternal(string url)
    {
        string filepath = Server.MapPath(url);
        FileInfo file = new FileInfo(filepath);
        if (file.Exists)
        {
            Response.ClearContent();
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Disposition", "inline; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = GetContentType(file.Extension.ToLower());
            Response.TransmitFile(file.FullName);
            Response.End();

        }
        else
        {
            string js = "alert('File not found!!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "js1", js, true);

        }
    }


    /// <summary>
    /// return a file extension based on passed file extension of the file.
    /// </summary>
    /// <param name="fileExtension"></param>
    /// <returns></returns>
    public string GetContentType(string fileExtension)
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
