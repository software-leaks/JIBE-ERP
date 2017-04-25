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
using System.Data.OleDb;
using System.Xml;
using System.Xml.Xsl;
using System.Text;
using System.IO;
using System.Net;
using System.Management;
using System.DirectoryServices;
using SMS.Business.QMS;



public partial class Web_AdvQuery : System.Web.UI.Page
{
    //public static DataSet resultDS = new DataSet();
    IPHostEntry host = new IPHostEntry();
    BLL_QMS_Document objQMS = new BLL_QMS_Document();
    
    private string GetSharedFolderPath(string serverName)
    {
        return @"\\" + serverName + "\\" + "DOCUMENTS" + "\\";
    }

    private string GetCatalogPath(string serverName)
    {
        return serverName + "." + "QMS";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        host = Dns.GetHostEntry(Dns.GetHostName());

        if (!IsPostBack)
        {

            LoadFoldersFromFilePath();
        }

        ToModeDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    /// <summary>
    /// bind the combo box with the folder name by the first hierarchy  
    /// of the saved file path from the database.
    /// </summary>
    protected void LoadFoldersFromFilePath()
    {
        DataTable dt = objQMS.getFolderList(GetSessionUserID());

        foreach (DataRow dr in dt.Rows)
        {
            if (dr[0].ToString() != "")
            {
                string[] DirectoryList = dr[1].ToString().Split('/');
                if (DirectoryList.Length > 0)
                {
                    if (FolderName.Items.FindByValue(DirectoryList[DirectoryList.Length - 1]) == null)
                    {
                        FolderName.Items.Add(new ListItem(DirectoryList[DirectoryList.Length - 1], DirectoryList[DirectoryList.Length - 1]));
                    }
                }
            }
        }
        FolderName.Items.Insert(0, new ListItem("Select Folder", "Select Folder"));

    }

    /// <summary>
    /// this is use for the bind the datagrid by filter out the user.
    /// </summary>
    protected void BindData()
    {
        string ServerName = ConfigurationManager.AppSettings.Get("QMS_SERVER_PATH");

        string QMSCatalogPath = GetCatalogPath(ServerName);

        DataTable dt = SearchCatalog(QMSCatalogPath);

        Session["SearchResult"] = dt;
        gvSearchResult.DataSource = dt;
        gvSearchResult.DataBind();
        lblDisplay.Text = Convert.ToString(dt.Rows.Count) + " Documents matched the Query";
    }

    /// <summary>
    /// onclick , based on the filter condition result will be display accordingly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        ViewState["sortDirectionTEXT"] = System.Web.UI.WebControls.SortDirection.Ascending;
        BindData();

    }

    /// <summary>
    /// clear & set with default value of the filter controls.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        SearchString.Text = "";
        FileNameRestriction.Text = "";
        DocAuthorRestriction.Text = "";
        FSRestOther.Text = "";
        FMModDate.Text = "";
        ToModeDate.Text = "";
        lblDisplay.Visible = false;
        ddlSubFolderName.SelectedIndex = 0;
        FolderName.SelectedIndex = 0;
    }

    /// <summary>
    /// returns a datatable by passing the catalogpath.
    /// </summary>
    /// <param name="CatalogPath"></param>
    /// <returns></returns>
    public DataTable SearchCatalog(string CatalogPath)
    {

        DataTable dt = new DataTable();
        DataTable dtSearchRes = new DataTable();
        try
        {
            string fileTypes = "\".doc\" OR \".rtf\" OR \".XLS\" OR \".txt\" OR \".pdf\"";
            string freeTextQuery = BindDataFreeText();
            string queryAppend = BinddataThroughQuery();

            string SearchFolder = "";

            {
                StringBuilder Query = new StringBuilder();
                //Query.Append("SELECT Filename,Size,Path,Write,url from ");
                ////Query.Append(CatalogPath);
                //Query.Append(" SCOPE()");
                //Query.Append(" WHERE ");
                //Query.Append(freeTextQuery);
                //Query.Append(" CONTAINS(Filename, '");
                //Query.Append(fileTypes);
                //Query.Append("') ");
                //Query.Append(queryAppend);
                //Query.Append(" ORDER BY Filename");

                Query.Append("SELECT  Filename,Size,Path,Write,url FROM SCOPE() WHERE FREETEXT(Contents, 'bikash')");


                string con = "Provider= \"MSIDXS\"; Data Source=\"Bikash\";";

                OleDbConnection cn = new OleDbConnection(con);
                OleDbDataAdapter cmd = new OleDbDataAdapter(Query.ToString(), cn);

                //result which is comes directly comes from indexing service
                cmd.Fill(dtSearchRes);

                string QueryRecord = "";
                DataRow[] drSearchRes;
                //if (FolderName.SelectedIndex != 0)
                //{
                //    SearchFolder = FolderName.SelectedValue.ToLower();
                //    string subFolderName = ddlSubFolderName.SelectedValue.ToString().ToLower();

                //    QueryRecord = "PATH like '%\\documents\\" + SearchFolder + "\\%'";
                //    if (ddlSubFolderName.SelectedIndex != 0)
                //        QueryRecord = "PATH like '%\\documents\\" + SearchFolder + "\\" + subFolderName + "\\%'";

                //    string strSortField = "PATH";
                //    drSearchRes = dtSearchRes.Select(QueryRecord,strSortField);

                //    //this section of the code when the Folder Name & SubFolder Name has selected.

                //    dt = getDtdata(drSearchRes);

                //}
                //else
                //{
                //    //this section of the code when the Folder Name & SubFolder Name has not  selected.
                //    drSearchRes = dtSearchRes.Select("1=1");
                //    dt = getDtdata(drSearchRes);
                //}

                drSearchRes = dtSearchRes.Select("1=1");
                dt = getDtdata(drSearchRes);
            }

        }
        catch (Exception ex)
        {

            string script = "myMessage('Error in query !! Error: " + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Message", script, true);
        }

        return dt;
    }

    struct SearchParameters
    {
        private string storage;

        public string Storage
        {
            get { return storage; }
            set { storage = value; }
        }

        private string query;

        public string Query
        {
            get { return query; }
            set { query = value; }
        }
    }

    /// <summary>
    /// processes rows by rows of the resultant grid.
    /// </summary>
    /// <param name="drDataRows"></param>
    /// <returns></returns>
    public DataTable getDtdata(DataRow[] drDataRows)
    {
        DataTable dt = new DataTable();

        dt.Columns.Add(new DataColumn("Filename", typeof(string)));
        dt.Columns.Add(new DataColumn("File Size", typeof(int)));
        dt.Columns.Add(new DataColumn("Read Date", typeof(string)));
        dt.Columns.Add(new DataColumn("FilePath", typeof(string)));
        dt.Columns.Add(new DataColumn("write", typeof(DateTime)));
        dt.Columns.Add(new DataColumn("Version", typeof(string)));
        DataRow dr;

        foreach (DataRow dtR in drDataRows)
        {
            //get the fileId by the path which is comes from indexing service
            int fileID = getFileIDByDocPath(dtR["path"].ToString());
            if (fileID != 0)
            {
                DataSet dsVersionInfo = objQMS.getFileVersion(fileID);
                //this is max version of the file
                int MaxVersionByFileID = objQMS.getMaxVersionFromParentTable(fileID);
                //this is for the file Id having Version is "0" & filId is not present(file path who having with fileID\\Version type) 
                if (MaxVersionByFileID.ToString() == "1" && fileID.ToString() != "0")
                {
                    dr = dt.NewRow();
                    dr[0] = FormatFilePath(dtR["path"].ToString());
                    dr[1] = Convert.ToInt32(dtR["size"].ToString()) / (1024);
                    dr[2] = ((DateTime)dtR["write"]).ToString("dd/MM/yyyy");
                    dr[3] = dtR["path"].ToString();
                    dr[4] = dtR["write"];
                    dr[5] = Convert.ToString("1");
                    dt.Rows.Add(dr);
                }
                else
                {
                    if (dsVersionInfo.Tables[0].Rows.Count > 0 && dsVersionInfo.Tables[1].Rows.Count > 0)
                    {
                        //going to show distinct file who having the maximum file version out of the same fileId
                        if (dsVersionInfo.Tables[1].Rows[0]["Version"].ToString().ToString().Equals(MaxVersionByFileID.ToString()) == true)
                        {
                            dr = dt.NewRow();
                            dr[0] = FormatFilePath(dtR["path"].ToString());
                            dr[1] = Convert.ToInt32(dtR["size"].ToString()) / (1024);
                            dr[2] = ((DateTime)dtR["write"]).ToString("dd/MM/yyyy");
                            dr[3] = dtR["path"].ToString();
                            dr[4] = dtR["write"];
                            dr[5] = dsVersionInfo.Tables[1].Rows[0]["Version"].ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                }
            }

        }
        return dt;

    }

    /// <summary>
    /// this is a method returns a file path or part of file path to show in the resultant grid (feel when 2
    /// same file name aur present in diff folder location, might be user get confused).
    /// </summary>
    /// <param name="FilePath"> file path of the document</param>
    /// <returns></returns>
    private string FormatFilePath(string FilePath)
    {
        int iDocPos = FilePath.ToLower().IndexOf("\\documents\\");

        string[] arFilePath = FilePath.Substring(iDocPos + 1).Split('\\');

        if (arFilePath.Length > 5)
        {
            string strPath = "Documents\\...\\" + arFilePath[arFilePath.Length - 2].ToString() + "\\" + arFilePath[arFilePath.Length - 1].ToString();
            return strPath;
        }
        else
        {
            return FilePath.Substring(iDocPos + 1);
        }
    }

    /// <summary>
    /// convert the physical path into virutal path for further processes.
    /// </summary>
    /// <param name="phFolderPath"></param>
    /// <returns></returns>
    public string getVirtualFilePath(string phFolderPath)
    {
        if (phFolderPath != "")
        {
            int DMSPos = phFolderPath.ToLower().IndexOf("\\documents\\");
            string virtualPath = phFolderPath.Substring(DMSPos + 1).ToString().Replace("\\", "/");
            return virtualPath;
        }
        return "";
    }


    /// <summary>
    /// it return the file ID by passing the document path with particular predefined format.
    /// </summary>
    /// <param name="IndexingPath"></param>
    /// <returns></returns>
    public int getFileIDByDocPath(string IndexingPath)
    {
        string[] arrPath = IndexingPath.Split('\\');
        DataSet dsFileInfo = objQMS.getFileIDByDocInfo(arrPath[arrPath.Length - 1].ToString());
        string VirtualPathFormat = getVirtualFilePath(IndexingPath);
        for (int i = 0; i < dsFileInfo.Tables[0].Rows.Count; i++)
        {
            string[] arrFileName = dsFileInfo.Tables[0].Rows[i]["FILEPATH"].ToString().Split('/');
            string PathWithoutFileName = dsFileInfo.Tables[0].Rows[i]["FILEPATH"].ToString().Replace(arrFileName[arrFileName.Length - 1].ToString(), "");

            string FullVirtualPathFormatDB = "";
            if (dsFileInfo.Tables[0].Rows[i]["VERSION"].ToString().Equals("1") == true)
                FullVirtualPathFormatDB = PathWithoutFileName + arrFileName[arrFileName.Length - 1].ToString();
            else
                FullVirtualPathFormatDB = PathWithoutFileName + dsFileInfo.Tables[0].Rows[i]["ID"].ToString() + "/" + dsFileInfo.Tables[0].Rows[i]["VERSION"].ToString() + "/" + arrFileName[arrFileName.Length - 1].ToString();

            if (VirtualPathFormat.ToUpper().Equals(FullVirtualPathFormatDB.ToUpper()) == true)
                return Convert.ToInt32(dsFileInfo.Tables[0].Rows[i]["ID"].ToString());
        }
        return 0;
    }

    /// <summary>
    /// this is append the query text for the free text search supplied by the user.
    /// </summary>
    /// <returns></returns>
    public string BindDataFreeText()
    {
        if (SearchString.Text.Equals("") == false)
        {
            StringBuilder freeText = new StringBuilder(SearchString.Text.Trim());
            int length = freeText.Length;
            freeText.Insert(0, "\"");
            freeText.Insert(length + 1, "\"");
            freeText.Insert(length + 1, "*");
            return "CONTAINS('" + freeText.ToString() + "') AND";
        }
        return null;

    }

    protected void gvSearchResult_Sorting(object sender, GridViewSortEventArgs e)
    {

        string sortExpression = e.SortExpression;
        ViewState["z_sortexpresion"] = e.SortExpression;
        if (GridViewSortDirection == System.Web.UI.WebControls.SortDirection.Ascending)
        {
            GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Descending;
            SortGridView(sortExpression, "DESC");
        }
        else
        {
            GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
            SortGridView(sortExpression, "ASC");
        }
    }

    public string SortExpression
    {
        get
        {
            if (ViewState["z_sortexpresion"] == null)
                ViewState["z_sortexpresion"] = this.gvSearchResult.DataKeyNames[0].ToString();

            return ViewState["z_sortexpresion"].ToString();
        }
        set
        {
            ViewState["z_sortexpresion"] = value;
        }
    }

    public System.Web.UI.WebControls.SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirectionTEXT"] == null)
                ViewState["sortDirectionTEXT"] = System.Web.UI.WebControls.SortDirection.Ascending;

            return (System.Web.UI.WebControls.SortDirection)ViewState["sortDirectionTEXT"];
        }
        set
        {
            ViewState["sortDirectionTEXT"] = value;
        }
    }

    private void SortGridView(string sortExpression, string direction)
    {
        DataView dv = new DataView((DataTable)Session["SearchResult"]);
        dv.Sort = sortExpression + " " + direction;


        this.gvSearchResult.DataSource = dv;
        gvSearchResult.DataBind();
    }

    /// <summary>
    /// this is makes the query string by considering all the filter condition supplied by the user.
    /// </summary>
    /// <returns></returns>
    public string BinddataThroughQuery()
    {
        string Composer = " AND ";
        StringBuilder FNameRestriction = new StringBuilder(FileNameRestriction.Text.Trim());
        int length1 = FNameRestriction.Length;
        string TheQuery = "";
        IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);

        if (IsPostBack)
        {
            if (SearchString != null)
            {
                if (FSRestVal.SelectedValue.Equals("any") == false)
                {
                    if (FSRestVal.SelectedValue.Equals("other") == false)
                        TheQuery = Composer + "size" + FSRest.SelectedValue.ToString() + FSRestVal.SelectedValue.ToString() + "" + TheQuery;
                    else
                    {
                        if (FSRestOther.Text.Trim().Length > 0)
                            TheQuery = Composer + "size" + FSRest.SelectedValue.ToString() + FSRestOther.Text + "" + TheQuery;

                    }
                }

                if ((DocAuthorRestriction.Text).Equals("") == false)
                    TheQuery = Composer + "CONTAINS(DocTitle,'" + DocAuthorRestriction.Text.ToString() + "')" + TheQuery;

                if (FMModDate.Text.Equals("") == true && ToModeDate.Text.Equals("") == false)
                {
                    DateTime fromDate = DateTime.Parse(ToModeDate.Text, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    TheQuery = Composer + "write >='" + ((DateTime)fromDate).ToString("yyyy-MM-dd") + "'";

                }


                if (FMModDate.Text.Equals("") == false && ToModeDate.Text.Equals("") == false)
                {
                    DateTime startDate = DateTime.Parse(FMModDate.Text, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    DateTime fromDate = DateTime.Parse(ToModeDate.Text, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    TheQuery = Composer + "write >='" + ((DateTime)startDate).ToString("yyyy-MM-dd") + "' and write <= '" + ((DateTime)fromDate).ToString("yyyy-MM-dd") + "' " + TheQuery;
                }

                if ((FileNameRestriction.Text.Equals("") == false) && (FileNameRestriction.Text.Equals("any") == false))

                    if (length1 != 0)
                    {
                        FNameRestriction.Insert(0, "\"");
                        FNameRestriction.Insert(length1 + 1, "\"");
                        TheQuery = Composer + "CONTAINS(Filename,'" + FNameRestriction.ToString() + "')" + TheQuery;
                    }
                return TheQuery;
            }
            return TheQuery;
        }
        return null;
    }


    /// <summary>
    /// this is uses for the display a document in IFrame of the application.
    /// </summary>
    /// <param name="sendepr"></param>
    /// <param name="Args"></param>
    protected void ShowFile(object sendepr, CommandEventArgs Args)
    {
        int fileID = 0;
        string sPath = Args.CommandName.ToString();

        fileID = getFileIDByDocPath(sPath);
        int MaxVersionByFileID = objQMS.getMaxVersionFromParentTable(fileID);
        string msg = String.Format("window.location.href='fileloader.aspx?docid=" + fileID + "';");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
    }

    /// <summary>
    /// this is uses for the display a document in the external window.
    /// </summary>
    /// <param name="sendepr"></param>
    /// <param name="Args"></param>
    protected void OpenFileExternal(object sendepr, CommandEventArgs Args)
    {
        string sPath = Args.CommandName.ToString();
        OpenFileExternal(sPath);
    }

    /// <summary>
    /// this is uses for the display a document in the external window.
    /// </summary>
    /// <param name="sendepr"></param>
    /// <param name="Args"></param>
    public void OpenFileExternal(string url)
    {
        string virtualFilePath = getVirtualFilePath(url);
        string filepath = Server.MapPath(virtualFilePath);

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

    protected void btnClear_Click1(object sender, EventArgs e)
    {
        SearchString.Text = "";
        DocAuthorRestriction.Text = "";
        FileNameRestriction.Text = "";
        FMModDate.Text = "";
        ToModeDate.Text = "";
        FolderName.SelectedIndex = 0;
        FSRest.SelectedIndex = 0;
        FSRestVal.SelectedIndex = 0;
        ddlSubFolderName.SelectedIndex = 0;
    }

    protected void resultsDataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }

    /// <summary>
    /// it returns a path of the icon of file accordingly passed file extension. 
    /// </summary>
    /// <param name="NodeText"></param>
    /// <returns></returns>
    private string getNodeImageURL(string NodeText)
    {
        string extenssion = Path.GetExtension(NodeText);

        if (NodeText.IndexOf(".") > 0)
        {
            switch (extenssion)
            {
                case ".xls":
                    return "~/images/DocTree/xls.gif";
                case ".xlsx":
                    return "~/images/DocTree/xls.gif";
                case ".pdf":
                    return "~/images/DocTree/pdf.gif";
                case ".htm":
                    return "~/images/DocTree/page.gif";
                case ".html":
                    return "~/images/DocTree/page.gif";
                case ".txt":
                    return "~/images/DocTree/txt.gif";
                case ".doc":
                    return "~/images/DocTree/doc.gif";
                case ".tiff":
                    return "~/images/DocTree/gif.gif";
                case ".tif":
                    return "~/images/DocTree/gif.gif";
                case ".zip":
                    return "~/images/DocTree/zip.gif";
                case ".csv":
                    return "~/images/DocTree/xls.gif";
                case ".gif":
                    return "~/images/DocTree/bmp.gif";
                case ".jpg":
                    return "~/images/DocTree/page.gif";
                case ".jpeg":
                    return "~/images/DocTree/jpeg.gif";
                case ".bmp":
                    return "~/images/DocTree/bmp.gif";
                case ".png":
                    return "~/images/DocTree/bmp.gif";
                case ".rtf":
                    return "~/images/DocTree/page.gif";
                case "FDC":
                    return "~/images/DocTree/network.gif";
                default:
                    return "~/images/DocTree/page.gif";
            }

        }
        else
        { return "~/images/DocTree/folder.gif"; }
    }

    protected void gvSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filepath = ((Label)e.Row.FindControl("lblFileName")).Text;
            ((ImageButton)e.Row.Controls[5].FindControl("ImgOpenExt")).ImageUrl = getNodeImageURL(filepath);
        }

    }

    protected void gvSearchResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSearchResult.PageIndex = e.NewPageIndex;
        BindData();

    }

    protected void FolderName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objQMS.getFilpath();
        string SelectedFolderName = FolderName.SelectedValue.ToString().ToUpper();
        ddlSubFolderName.Items.Clear();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string[] DirectoryList = dr["FilePath"].ToString().Split('/');
            string NodeType = dr["NodeType"].ToString();
            if (DirectoryList.Length > 2)
            {
                if (DirectoryList[1].ToString().ToUpper().Equals(SelectedFolderName) == true)
                {

                    if (Convert.ToString(ddlSubFolderName.Items.FindByValue(DirectoryList[2])) == "" && NodeType == "1")
                        ddlSubFolderName.Items.Add(new ListItem(DirectoryList[2], DirectoryList[2]));
                }
            }
        }
        ddlSubFolderName.Items.Insert(0, new ListItem("Select SubFolder", "Select subFolder"));
    }
}









