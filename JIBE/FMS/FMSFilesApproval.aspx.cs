using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Properties;
using System.Data;
using SMS.Business.Infrastructure;
//using SMS.Business.Operations;
using SMS.Business.FMS;
using System.Web.UI.HtmlControls;
using System.IO;



public partial class FMSFilesApproval : System.Web.UI.Page
{
    BLL_FMS_Document objFMS = new BLL_FMS_Document();
    UserAccess objUA = new UserAccess();

    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
        
            BindGrid();
        }

    }

    public void BindGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        int ApproverId = 0;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        if (chkMyApproval.Checked == true)
            ApproverId = Int32.Parse(Session["USERID"].ToString());

        DataTable dt = objFMS.FMS_Files_Approval_Search(txtfilter.Text != "" ? txtfilter.Text : null, Convert.ToInt32(optApprove.SelectedValue), UDFLib.ConvertIntegerToNull(ApproverId)
            , sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvFMSFile.DataSource = dt;
            gvFMSFile.DataBind();
        }
        else
        {
            gvFMSFile.DataSource = dt;
            gvFMSFile.DataBind();
        }

    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");
       
        if (objUA.Edit == 1)uaEditFlag = true;
    
        if (objUA.Delete == 1) uaDeleteFlage = true;

        if (objUA.Approve == 1)
        {

        }//btnApprove.Enabled = true;

        
    }
   
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

  

    protected void ClearField()
    {
        //txtHoldTankID.Text = "";
        //txtHoldTankName.Text = "";
        //ddlStructureType.SelectedValue = "0";
        //DDLVessel.SelectedValue = "0";
    }

    protected void lblLogFileID_onClick(object sender,EventArgs e)
    { 
    LinkButton btn = (LinkButton)sender;
    GridViewRow row = (GridViewRow)btn.NamingContainer;

    string filePath = gvFMSFile.DataKeys[row.RowIndex].Value.ToString();

    btn.PostBackUrl = "../FMS/" + filePath;
      
        
    }


    


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        optApprove.SelectedValue = "0";
        
        BindGrid();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        DataTable dt;
        if (chkMyApproval.Checked == true)
        {
             dt = objFMS.FMS_Files_Approval_Search(txtfilter.Text != "" ? txtfilter.Text : null, Convert.ToInt32(optApprove.SelectedValue), GetSessionUserID()
                , sortbycoloumn, sortdirection
                 , null, null, ref  rowcount);
        }
        else
        {
              dt = objFMS.FMS_Files_Approval_Search(txtfilter.Text != "" ? txtfilter.Text : null, Convert.ToInt32(optApprove.SelectedValue), null
                , sortbycoloumn, sortdirection
                 , null, null, ref  rowcount);
        
        }

        string[] HeaderCaptions = { "File Name", "Created Date", "Version", "Approval Name", "Approval Date/Rejection Date", "Status" };
        string[] DataColumnsName = { "LogFileID", "LogDate", "Version", "ApproverName", "Date_Of_Approval", "App_Status" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "FMSFileApproval", "FMS File Approval Status", "");

    }

    protected void gvFMSFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //LinkButton lblLogFileID = (LinkButton)e.Row.FindControl("lblLogFileID");

            Label lblFilePath = (Label)e.Row.FindControl("lblFilePath");

            //lblLogFileID.Attributes.Add("onclick", "DocOpen('../" + lblFilePath.Text + "'); return false;");

            LinkButton lnkLogFileID =(LinkButton)e.Row.Cells[0].FindControl("lnkLogFileID");
            HyperLink lnkLogFileID1 = (HyperLink)e.Row.Cells[0].FindControl("lnkLogFileID1");
            lnkLogFileID1.Text = lnkLogFileID.Text;
            lnkLogFileID1.NavigateUrl = "../" + lblFilePath.Text;
            string FileExtension = new FileInfo(lnkLogFileID.Text).Extension;

            if (FileExtension == ".xsl" || FileExtension == ".xml")/* As told by Bikash Sir For xml and xsl file when User click on link then those document should be force download and rest of documents should open or download as per support in browser  */
            {
                lnkLogFileID.Visible = true;
                lnkLogFileID1.Visible = false;
            }
            else
            {
                lnkLogFileID.Visible = false;
                lnkLogFileID1.Visible = true;
            }
           


            if (Convert.ToInt32(Session["USERID"]) == UDFLib.ConvertToInteger(gvFMSFile.DataKeys[e.Row.RowIndex].Values["ApproverID"]))
            {
                //((CheckBox)e.Row.FindControl("chkStatus")).Enabled = true;
                ((Button)e.Row.FindControl("BtnApprove")).Enabled = true;
                ((Button)e.Row.FindControl("BtnReject")).Enabled = true;
            }
            else
            {
                //((CheckBox)e.Row.FindControl("chkStatus")).Enabled = false;
                ((Button)e.Row.FindControl("BtnApprove")).Enabled = false;
                ((Button)e.Row.FindControl("BtnReject")).Enabled = false;
            }

        }
        
        
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        }


        //ImageButton ImgUpdate = (ImageButton)e.Row.FindControl("ImgUpdate");


        //if (objUA.Delete == 1)
        //{ 
        
        //}

    }

    protected void gvFMSFile_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindGrid();
    }
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        BindGrid();
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtfilter.Text = "";
        chkMyApproval.Checked = true;
        optApprove.SelectedValue = "0";

        BindGrid();
    }
 
    protected void btnSaveRemark_Click(object sender, EventArgs e)
    {
        try
        {
            int Result = 0;

            int RowIndex = UDFLib.ConvertToInteger(ViewState["ApproveRowIndex"].ToString());
            Label lblFileID = (Label)gvFMSFile.Rows[RowIndex].FindControl("lblID");
            Label lblFilePath = (Label)gvFMSFile.Rows[RowIndex].FindControl("lblFilePath");
            Label lblVersion = (Label)gvFMSFile.Rows[RowIndex].FindControl("lblVersion");

            int Level_ID = UDFLib.ConvertToInteger(gvFMSFile.DataKeys[RowIndex].Values["LevelID"]);
            //objFMS.FMS_Insert_ScheduleFileApproval(UDFLib.ConvertToInteger(lblStatusID.Text), UDFLib.ConvertToInteger(lblOfficeID.Text), UDFLib.ConvertToInteger(lblVesselID.Text), UDFLib.ConvertToInteger(lblFileID.Text), txtRemark.Text, ApprovedBy, CreatedBy, UDFLib.ConvertToInteger(lblVersion.Text), Approval_Level, 1);
            Result = objFMS.FMS_Update_FileApproval(UDFLib.ConvertToInteger(lblFileID.Text), Level_ID, txtRemark.Text);
            if (Result == 0)
            {
                string js = " alert('Form Approved Successfully');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);

            }
            else if (Result == 1)
            {
                string js = " alert('Forms already Approved or Rejected.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }

            string js1 = " hideModal('dvRemark');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hdModal", js1, true);
            txtRemark.Text = "";
            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }


    protected void btnSaveRRemark_Click(object sender, EventArgs e)
    {
        try
        {
        int Result = 0;
        int RowIndex = UDFLib.ConvertToInteger(ViewState["ReworkRowIndex"].ToString());
        Label lblFileID = (Label)gvFMSFile.Rows[RowIndex].FindControl("lblID");
        Label lblFilePath = (Label)gvFMSFile.Rows[RowIndex].FindControl("lblFilePath");
        Label lblVersion = (Label)gvFMSFile.Rows[RowIndex].FindControl("lblVersion");
      
         int Level_ID = UDFLib.ConvertToInteger(gvFMSFile.DataKeys[RowIndex].Values["LevelID"]);

         
             Result=objFMS.FMS_Insert_FileRejection(UDFLib.ConvertToInteger(lblFileID.Text), txtRRemark.Text, Level_ID, UDFLib.ConvertToInteger(lblVersion.Text), GetSessionUserID());
             if (Result == 0)
             {
                 string js = " alert('Form Rejected Successfully'); ";
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
               
             }
             else if (Result == 1)
             {
                 string js = " alert('Forms already Approved or Rejected.');";
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
             }
             string js1 = " hideModal('dvReworkRemark');";
             ScriptManager.RegisterStartupScript(this, this.GetType(), "hdModal", js1, true);
             txtRRemark.Text = "";
             BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }
    public string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".mdi": return "";
            case ".xml": return "application/rdf+xml";
            case ".323": return "text/h323";
            case ".acx": return "application/internet-property-stream";
            case ".ai": return "application/postscript";
            case ".aif": return "audio/x-aiff";
            case ".aifc": return "audio/x-aiff";
            case ".aiff": return "audio/x-aiff";
            case ".asf": return "video/x-ms-asf";
            case ".asr": return "video/x-ms-asf";
            case ".asx": return "video/x-ms-asf";
            case ".au": return "audio/basic";
            case ".avi": return "video/x-msvideo";
            case ".axs": return "application/olescript";
            case ".bas": return "text/plain";
            case ".bcpio": return "application/x-bcpio";
            case ".bin": return "application/octet-stream";
            case ".bmp": return "image/bmp";
            case ".c": return "text/plain";
            case ".cat": return "application/vnd.ms-pkiseccat";
            case ".cdf": return "application/x-cdf";
            case ".cer": return "application/x-x509-ca-cert";
            case ".class": return "application/octet-stream";
            case ".clp": return "application/x-msclip";
            case ".cmx": return "image/x-cmx";
            case ".cod": return "image/cis-cod";
            case ".cpio": return "application/x-cpio";
            case ".crd": return "application/x-mscardfile";
            case ".crl": return "application/pkix-crl";
            case ".crt": return "application/x-x509-ca-cert";
            case ".csh": return "application/x-csh";
            case ".css": return "text/css";
            case ".dcr": return "application/x-director";
            case ".der": return "application/x-x509-ca-cert";
            case ".dir": return "application/x-director";
            case ".dll": return "application/x-msdownload";
            case ".dms": return "application/octet-stream";
            case ".doc": return "application/msword";
            case ".dot": return "application/msword";
            case ".dvi": return "application/x-dvi";
            case ".dxr": return "application/x-director";
            case ".eps": return "application/postscript";
            case ".etx": return "text/x-setext";
            case ".evy": return "application/envoy";
            case ".exe": return "application/octet-stream";
            case ".flr": return "x-world/x-vrml";
            case ".gif": return "image/gif";
            case ".gtar": return "application/x-gtar";
            case ".gz": return "application/x-gzip";
            case ".h": return "text/plain";
            case ".hdf": return "application/x-hdf";
            case ".hlp": return "application/winhlp";
            case ".hqx": return "application/mac-binhex40";
            case ".hta": return "application/hta";
            case ".htc": return "text/x-component";
            case ".htm": return "text/html";
            case ".html": return "text/html";
            case ".htt": return "text/webviewhtml";
            case ".ico": return "image/x-icon";
            case ".ief": return "image/ief";
            case ".iii": return "application/x-iphone";
            case ".ins": return "application/x-internet-signup";
            case ".isp": return "application/x-internet-signup";
            case ".jfif": return "image/pipeg";
            case ".jpe": return "image/jpeg";
            case ".jpeg": return "image/jpeg";
            case ".jpg": return "image/jpeg";
            case ".js": return "application/x-javascript";
            case ".latex": return "application/x-latex";
            case ".lha": return "application/octet-stream";
            case ".lsf": return "video/x-la-asf";
            case ".lsx": return "video/x-la-asf";
            case ".lzh": return "application/octet-stream";
            case ".m13": return "application/x-msmediaview";
            case ".m14": return "application/x-msmediaview";
            case ".m3u": return "audio/x-mpegurl";
            case ".man": return "application/x-troff-man";
            case ".mdb": return "application/x-msaccess";
            case ".me": return "application/x-troff-me";
            case ".mht": return "message/rfc822";
            case ".mhtml": return "message/rfc822";
            case ".mid": return "audio/mid";
            case ".mny": return "application/x-msmoney";
            case ".mov": return "video/quicktime";
            case ".movie": return "video/x-sgi-movie";
            case ".mp2": return "video/mpeg";
            case ".mp3": return "audio/mpeg";
            case ".mp4": return "video/mp4";
            case ".mpa": return "video/mpeg";
            case ".mpe": return "video/mpeg";
            case ".mpeg": return "video/mpeg";
            case ".mpg": return "video/mpeg";
            case ".mpp": return "application/vnd.ms-project";
            case ".mpv2": return "video/mpeg";
            case ".ms": return "application/x-troff-ms";
            case ".mvb": return "application/x-msmediaview";
            case ".nws": return "message/rfc822";
            case ".oda": return "application/oda";
            case ".p10": return "application/pkcs10";
            case ".p12": return "application/x-pkcs12";
            case ".p7b": return "application/x-pkcs7-certificates";
            case ".p7c": return "application/x-pkcs7-mime";
            case ".p7m": return "application/x-pkcs7-mime";
            case ".p7r": return "application/x-pkcs7-certreqresp";
            case ".p7s": return "application/x-pkcs7-signature";
            case ".pbm": return "image/x-portable-bitmap";
            case ".pdf": return "application/pdf";
            case ".pfx": return "application/x-pkcs12";
            case ".pgm": return "image/x-portable-graymap";
            case ".pko": return "application/ynd.ms-pkipko";
            case ".pma": return "application/x-perfmon";
            case ".pmc": return "application/x-perfmon";
            case ".pml": return "application/x-perfmon";
            case ".pmr": return "application/x-perfmon";
            case ".pmw": return "application/x-perfmon";
            case ".pnm": return "image/x-portable-anymap";
            case ".pot": return "application/vnd.ms-powerpoint";
            case ".ppm": return "image/x-portable-pixmap";
            case ".pps": return "application/vnd.ms-powerpoint";
            case ".ppt": return "application/vnd.ms-powerpoint";
            case ".prf": return "application/pics-rules";
            case ".ps": return "application/postscript";
            case ".pub": return "application/x-mspublisher";
            case ".qt": return "video/quicktime";
            case ".ra": return "audio/x-pn-realaudio";
            case ".ram": return "audio/x-pn-realaudio";
            case ".ras": return "image/x-cmu-raster";
            case ".rgb": return "image/x-rgb";
            case ".rmi": return "audio/mid";
            case ".roff": return "application/x-troff";
            case ".rtf": return "application/rtf";
            case ".rtx": return "text/richtext";
            case ".scd": return "application/x-msschedule";
            case ".sct": return "text/scriptlet";
            case ".setpay": return "application/set-payment-initiation";
            case ".setreg": return "application/set-registration-initiation";
            case ".sh": return "application/x-sh";
            case ".shar": return "application/x-shar";
            case ".sit": return "application/x-stuffit";
            case ".snd": return "audio/basic";
            case ".spc": return "application/x-pkcs7-certificates";
            case ".spl": return "application/futuresplash";
            case ".src": return "application/x-wais-source";
            case ".sst": return "application/vnd.ms-pkicertstore";
            case ".stl": return "application/vnd.ms-pkistl";
            case ".stm": return "text/html";
            case ".svg": return "image/svg+xml";
            case ".sv4cpio": return "application/x-sv4cpio";
            case ".sv4crc": return "application/x-sv4crc";
            case ".t": return "application/x-troff";
            case ".tar": return "application/x-tar";
            case ".tcl": return "application/x-tcl";
            case ".tex": return "application/x-tex";
            case ".texi": return "application/x-texinfo";
            case ".texinfo": return "application/x-texinfo";
            case ".tgz": return "application/x-compressed";
            case ".tif": return "image/tiff";
            case ".tiff": return "image/tiff";
            case ".tr": return "application/x-troff";
            case ".trm": return "application/x-msterminal";
            case ".tsv": return "text/tab-separated-values";
            case ".txt": return "text/plain";
            case ".uls": return "text/iuls";
            case ".ustar": return "application/x-ustar";
            case ".vcf": return "text/x-vcard";
            case ".vrml": return "x-world/x-vrml";
            case ".wav": return "audio/x-wav";
            case ".wcm": return "application/vnd.ms-works";
            case ".wdb": return "application/vnd.ms-works";
            case ".wks": return "application/vnd.ms-works";
            case ".wmf": return "application/x-msmetafile";
            case ".wps": return "application/vnd.ms-works";
            case ".wri": return "application/x-mswrite";
            case ".wrl": return "x-world/x-vrml";
            case ".wrz": return "x-world/x-vrml";
            case ".xaf": return "x-world/x-vrml";
            case ".xbm": return "image/x-xbitmap";
            case ".xla": return "application/vnd.ms-excel";
            case ".xlc": return "application/vnd.ms-excel";
            case ".xlm": return "application/vnd.ms-excel";
            case ".xls": return "application/vnd.ms-excel";
            case ".xlt": return "application/vnd.ms-excel";
            case ".xlw": return "application/vnd.ms-excel";
            case ".xof": return "x-world/x-vrml";
            case ".xpm": return "image/x-xpixmap";
            case ".xwd": return "image/x-xwindowdump";
            case ".z": return "application/x-compress";
            case ".zip": return "application/zip";
            default: return "application/octet-stream";


        }
    }
    protected void gvFMSFile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approve")
        {
            int RowIndex = UDFLib.ConvertToInteger(e.CommandArgument.ToString());

            ViewState["ApproveRowIndex"] = RowIndex;
            string js = " showModal('dvRemark');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "handleModal", js, true);
        }
        if (e.CommandName == "Reject")
        {
            int RowIndex = UDFLib.ConvertToInteger(e.CommandArgument.ToString());

            ViewState["ReworkRowIndex"] = RowIndex;
            string js = " showModal('dvRRemark');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "handleModal", js, true);
        }

        if (e.CommandName == "DownladFile")
        {
            try
            {
                int RowIndex = UDFLib.ConvertToInteger(e.CommandArgument.ToString());
                Label lblPath = (Label)gvFMSFile.Rows[RowIndex].Cells[0].FindControl("lblFilePath");
                lblPath.Text = "../" + lblPath.Text;
                if (File.Exists(Server.MapPath(lblPath.Text)) == true)
                {
                    string FileName = new FileInfo(lblPath.Text).Name;
                    string FileExtension = new FileInfo(lblPath.Text).Extension;
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = ReturnExtension(FileExtension);
                    Response.AppendHeader("content-disposition", "attachment;filename=" + FileName);

                    Response.TransmitFile(Server.MapPath(lblPath.Text));
                    Response.End();
                }
                else
                {
                    string jsNoFile = " alert('File Not Found!!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsNoFile", jsNoFile, true);
                }

            }

            catch (Exception ex)
            {
                string jsFError = " alert('" + ex + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsFError", jsFError, true);
            }
        }
    }

}