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
using System.Xml.Linq;

using System.Management;
using System.IO;

using SMS.Business.QMS;

public partial class CheckOutForm : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document(); 
    
    int fileID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        fileID = Convert.ToInt32(Request.QueryString["FileID"].ToString());

        //get the version & Operation info by Check out fileID
        DataSet dsOperationInfo = objQMS.getCheckedFileInfo(fileID);
        if (dsOperationInfo.Tables[0].Rows.Count > 0)
        {
            //get the last row of the table to know the current status of the file
            int dataRow = dsOperationInfo.Tables[0].Rows.Count - 1;
            string FileStatus = dsOperationInfo.Tables[0].Rows[dataRow]["Operation_Type"].ToString();
            string User = dsOperationInfo.Tables[0].Rows[dataRow]["UserName"].ToString();

            if (FileStatus.Equals("CHECKED OUT") == true && (Convert.ToInt32(Session["USERID"]) != Convert.ToInt32(dsOperationInfo.Tables[0].Rows[dataRow]["UserID"].ToString())))
            {
                string Message = "The file is already Checked Out by " + dsOperationInfo.Tables[0].Rows[dataRow]["UserName"].ToString();
                String msg = String.Format("myMessage('" + Message + "')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            }
            else
                showSaveDialog();
        }
        else
            showSaveDialog();


    }


    /// <summary>
    /// this is  check the all the condition & operation for particular file, give the apropriate  message to end user.
    /// </summary>
    public void showSaveDialog()
    {
        //checking for avoid the multiple Check Out the file
        DataSet dsLastestFileAccessType = objQMS.getLatestFileOperationByUserID(fileID, Convert.ToInt32(Session["USERID"]));

        if (dsLastestFileAccessType.Tables[0].Rows.Count > 0)
        {
            if (dsLastestFileAccessType.Tables[0].Rows[0]["Operation_Type"].ToString() == "CHECKED OUT")
            {
                String msg = String.Format("myMessage('You have already Checked Out the file.');window.location.href='fileloader.aspx?docid=" + fileID + "';");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

            }
            else
                browseFileDialog();
        }
        else
            browseFileDialog();
    }

    public void browseFileDialog()
    {
        //save the check out information into the database
        objQMS.insertRecordAtCheckout(Convert.ToInt32(Session["USERID"]), fileID);

        //get latest File details by the file ID
        DataSet dsFileDetails = objQMS.getFileDetailsByID(fileID,0);
        string navURL = "";

        if (dsFileDetails.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsFileDetails.Tables[0].Rows[0];
            navURL = dr["filepath"].ToString();
            string filename =System.IO.Path.GetFileName(dr["filepath"].ToString());

           

            OpenFileExternal(navURL, dr["LogFileID"].ToString());

        }

    }

    /// <summary>
    /// this is uses for the display a document in the external window.
    /// </summary>
    /// <param name="sendepr"></param>
    /// <param name="Args"></param>
    public void OpenFileExternal(string url,string filename)
    {
        string filepath = Server.MapPath(url);
        FileInfo file = new FileInfo(filepath);
        bool IE = Request.UserAgent.IndexOf("MSIE") > -1;


        if (file.Exists)
        {
            Response.ClearContent();

            if (!IE)            
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);            
            else
                Response.AddHeader("Content-Disposition", "inline; filename=" + filename);

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
