using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.Globalization;
using System.DirectoryServices;

using SMS.Business.QMS;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Drawing;
using System.Web.UI.HtmlControls;


public partial class Web_MainLog : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document();

    public static string ddlFolderName = "";
    public static string parentFolderName = "";
    public static int totalRecord = 0;
    public static string parentFolder = null;
    public static DataSet resultDS = new DataSet();
    string todate = "";
    string fromDate = "";
    DataSet dsSearchResult = new DataSet();
    public int UserDropDown;

    protected void Page_Load(object sender, EventArgs e)
    {
        string currentUser = "";
        if (Session["user"] != null)
        {
            currentUser = Session["user"].ToString();
        }
        else
        {
            //IF RUNNING IN LOCAL
            Session["user"] = "100028";
            currentUser = Session["user"].ToString();
        }

        if (!IsPostBack)
        {
            LoadFoldersFromFilePath();
            GetDefaultDate();
            FillUser();
            RdoViewRead.Checked = true;
        }

    }

    /// <summary>
    /// this is use for the set the default date setting while page loading.
    /// </summary>
    public void GetDefaultDate()
    {
        if (FromModeDate.Text == "" && ToModDate.Text == "")
        {
            FromModeDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            DateTime dt = new DateTime(2009, 10, 01);
            ToModDate.Text = dt.ToString("dd/MM/yyyy");
        }
    }

    /// <summary>
    /// onclick, based on the filter condition result will be display in grid accordingly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        if (DDUser.SelectedIndex == 0)
        {

            string js = "<script language='javascript' type='text/javascript'>CheckUser();</script>";
            Page.ClientScript.RegisterStartupScript(GetType(), "script", js);
            GrdQMSlog.Visible = false;
            lblDocumentCount.Visible = false;
        }

        else
        {

            if (RdoViewAll.Checked == false && RdoViewNotRead.Checked == false)
            {
                getRdoViewRead();
            }
            else if (RdoViewNotRead.Checked == true)
            {
                getRdoViewNotRead();
            }

            else if (RdoViewAll.Checked == true)
            {
                getRdoViewReadAll();
            }

            Session["GridDataInfo"] = dsSearchResult;
            Session["GridDefault"] = dsSearchResult;
            GrdQMSlog.Visible = true;
            lblDocumentCount.Visible = true;
            GrdQMSlog.DataSource = dsSearchResult.Tables[0];
            GrdQMSlog.DataBind();
            lblDocumentCount.Text = dsSearchResult.Tables[0].Rows.Count + " Record Found";
        }
    }
    protected void GrdQMSlog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdQMSlog.PageIndex = e.NewPageIndex;
        DataSet ds = (DataSet)Session["GridDataInfo"];
        GrdQMSlog.DataSource = ds.Tables[0];
        GrdQMSlog.DataBind();
    }
    protected void GrdQMSlog_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void RdoViewRead_CheckedChanged(object sender, EventArgs e)
    {
    }
    protected void RdoViewAll_CheckedChanged(object sender, EventArgs e)
    {
    }
    protected void RdoViewNotRead_CheckedChanged(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// when the user has selected View Not Read(radio button) of the filter control.
    /// </summary>
    public void getRdoViewNotRead()
    {
        todate = ToModDate.Text.Trim();
        fromDate = FromModeDate.Text.Trim();
        string vMan = Manual1.Text.ToString().ToUpper();
        UserDropDown = Convert.ToInt32(DDUser.SelectedValue);

        dsSearchResult = objQMS.getDetailsgViewNotRead(UserDropDown, fromDate, todate, vMan);
        Session["GridDataInfo"] = dsSearchResult;
        Session["GridDefault"] = dsSearchResult;
        GrdQMSlog.DataSource = dsSearchResult.Tables[0];
        GrdQMSlog.DataBind();
        GrdQMSlog.PageIndex = 0;

    }

    /// <summary>
    /// when the user has selected View  Read(radio button) of the filter control.
    /// </summary>
    public void getRdoViewRead()
    {
        todate = ToModDate.Text.Trim();
        fromDate = FromModeDate.Text.Trim();
        string vMan = Manual1.Text.ToString().ToUpper();
        UserDropDown = Convert.ToInt32(DDUser.SelectedValue);

        dsSearchResult = objQMS.getSearchResult(UserDropDown, todate, fromDate, vMan);
        Session["GridDataInfo"] = dsSearchResult;
        Session["GridDefault"] = dsSearchResult;
        GrdQMSlog.DataSource = dsSearchResult.Tables[0];
        GrdQMSlog.DataBind();
        GrdQMSlog.PageIndex = 0;
    }

    /// <summary>
    /// when the user has selected View All(radio button) of the filter control.
    /// </summary>
    public void getRdoViewReadAll()
    {
        fromDate = ToModDate.Text.Trim();
        todate = FromModeDate.Text.Trim();
        string ManualReadAll = Manual1.Text.ToString().ToUpper();
        UserDropDown = Convert.ToInt32(DDUser.SelectedValue);

        dsSearchResult = objQMS.getDetailsgViewReadAll(UserDropDown, fromDate, todate, ManualReadAll);
        Session["GridDataInfo"] = dsSearchResult;
        Session["GridDefault"] = dsSearchResult;
        GrdQMSlog.DataSource = dsSearchResult.Tables[0];
        GrdQMSlog.DataBind();
        GrdQMSlog.PageIndex = 0;

        RdoViewAll.Checked = true;
    }

    /// <summary>
    /// bind the combo box with the folder name by the first hierarchy  
    /// of the saved file path from the database.
    /// </summary>
    protected void LoadFoldersFromFilePath()
    {
        DataSet ds = objQMS.getFilpath();

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string[] DirectoryList = dr[0].ToString().Split('\\');
            if (DirectoryList.Length > 3)
            {
                if (Manual1.Items.FindByValue(DirectoryList[4]) == null)
                {
                    Manual1.Items.Add(new ListItem(DirectoryList[4], DirectoryList[4]));
                }
            }
        }
        Manual1.Items.Insert(0, new ListItem("Select", "Select"));

    }

    /// <summary>
    /// this is uses for the display a document in IFrame of the application.
    /// </summary>
    /// <param name="sendepr"></param>
    /// <param name="Args"></param>
    protected void ShowFile(object sendepr, CommandEventArgs Args)
    {
        string sPath = Args.CommandName.ToString();
        int iSt = sPath.IndexOf("\\DOCUMENT\\");
        sPath = sPath.Substring(iSt + 1);
        string navURL = sPath.Replace("\\", "/");

        Session["NAV_URL"] = navURL;
        string script = "";

        script += "opener.location.reload(true);";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenFile", script, true);


   }


    protected void OpenFileExternal(object sendepr, CommandEventArgs Args)
    {
        string sPath = Args.CommandName.ToString();
        int iSt = sPath.IndexOf("\\DOCUMENT\\");
        sPath = sPath.Substring(iSt + 1);
        //navURL = qms/foldername/filename.ext
        string navURL = sPath.Replace("\\", "/");
        OpenFileExternal(navURL);
    }

    /// <summary>
    /// this is uses for the diaplay a document in the external window.
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
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Disposition", "inline; filename=" + file.Name);
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

    /// <summary>
    /// bind the combo box with the User name & User ID from the database.
    /// </summary>
    public void FillUser()
    {
        try
        {
            DataSet ds = objQMS.FillDDUserForOffice();
            DDUser.DataSource = ds.Tables[0];
            DDUser.DataTextField = "User_name";
            DDUser.DataValueField = "UserID";
            DDUser.DataBind();
            DDUser.Items.Insert(0, new ListItem("Select"));
            DDUser.SelectedValue = Session["user"].ToString();
        }
        catch { }
    }

    protected void DDUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        //UserDropDown = Convert.ToInt32(DDUser.SelectedValue);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        DataSet GridDS = new DataSet();
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=FileName.xls");
        Response.Charset = "";
        Response.ContentType = "application/QMSLog.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.Html32TextWriter htmlWrite = new Html32TextWriter(stringWrite);

        for (int i = 0; i < GrdQMSlog.Columns.Count - 1; i++)
        {
            GrdQMSlog.HeaderRow.Cells[i].ForeColor = Color.Black;
            GrdQMSlog.HeaderRow.Cells[i].BackColor = Color.LightGray;
            GrdQMSlog.HeaderStyle.BackColor = Color.LightGray;
            GrdQMSlog.HeaderStyle.ForeColor = Color.Black;
            GrdQMSlog.HeaderStyle.Wrap = false;
            GrdQMSlog.FooterRow.Cells[i].ForeColor = Color.White;
            GrdQMSlog.FooterRow.Cells[i].BackColor = Color.LightGray;
            GrdQMSlog.FooterStyle.BackColor = Color.LightGray;
            GrdQMSlog.FooterStyle.ForeColor = Color.Black;

            GrdQMSlog.RowStyle.Wrap = false;
            GrdQMSlog.PagerStyle.BackColor = Color.White;
            GrdQMSlog.PagerStyle.ForeColor = Color.Black;
        }


        GridDS = (DataSet)Session["GridDefault"];
        GrdQMSlog.DataSource = GridDS.Tables[0];
        GrdQMSlog.AllowPaging = false;
        GrdQMSlog.DataBind();

        for (int i = 0; i < GrdQMSlog.Columns.Count - 1; i++)
        {
            GrdQMSlog.HeaderRow.Cells[i].ForeColor = Color.Black;
            GrdQMSlog.HeaderRow.Cells[i].BackColor = Color.LightGray;
            GrdQMSlog.HeaderStyle.BackColor = Color.LightGray;
            GrdQMSlog.HeaderStyle.ForeColor = Color.Black;
            GrdQMSlog.HeaderStyle.Wrap = false;
            GrdQMSlog.FooterRow.Cells[i].ForeColor = Color.White;
            GrdQMSlog.FooterRow.Cells[i].BackColor = Color.LightGray;
            GrdQMSlog.FooterStyle.BackColor = Color.LightGray;
            GrdQMSlog.FooterStyle.ForeColor = Color.Black;
            GrdQMSlog.RowStyle.Wrap = false;
            GrdQMSlog.PagerStyle.BackColor = Color.White;
            GrdQMSlog.PagerStyle.ForeColor = Color.Black;
        }


        HtmlForm frm = new HtmlForm();
        GrdQMSlog.Parent.Controls.Add(frm);// .Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(GrdQMSlog);
        frm.RenderControl(htmlWrite);
        System.Web.HttpContext.Current.Response.Write(stringWrite.ToString());
        GrdQMSlog.AllowPaging = true;
        System.Web.HttpContext.Current.Response.End();
    }

    /// <summary>
    /// it returns a path of the icon of file accordingly passed file extension. 
    /// </summary>
    /// <param name="NodeText"></param>
    /// <returns></returns>
    private string getNodeImageURL(string NodeText)
    {
        if (NodeText.IndexOf(".") > 0)
        {
            string[] strAr = NodeText.Split('.');
            if (strAr[strAr.Length - 1].Length == 3)
            {
                switch (strAr[strAr.Length - 1].ToLower())
                {
                    case "xls":
                    case "pdf":
                    case "htm":
                    case "html":
                    case "txt":
                    case "doc":
                    case "tiff":
                    case "tif":
                    case "zip":
                    case "csv":
                    case "gif":
                    case "jpg":
                    case "jpeg":
                    case "bmp":
                    case "rtf":
                        return "~/images/DocTree/" + strAr[strAr.Length - 1].ToLower() + ".gif";
                    default:
                        return "~/images/DocTree/page.gif";
                }

            }
            else
            { return ""; }

        }
        else
        { return ""; }
    }
    protected void GrdQMSlog_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             string filepath = ((Label)e.Row.Controls[0].FindControl("logfileid")).Text;
             ((ImageButton)e.Row.Controls[4].FindControl("ImgOpenExt")).ImageUrl = getNodeImageURL(filepath);
         }

    }
}
