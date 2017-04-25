using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SMS.Business.QMS;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Text;
using SMS.Business.QMSDB;

public partial class AddNewFile : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document();
    BLL_Infra_Company objCompBLL = new BLL_Infra_Company();

    //string FolderPath = "";
    //string[] fileName = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        string FolderPath = "";
        if (Convert.ToString(Session["USERID"]) == "")
        {
            Response.Write("<center><br><br><h2><font color=gray>Session is lost. Please click on the Logout option and login again.</font></h2></center>");
            Response.End();
        }
        if (Request.QueryString["Path"] == null)
        {
            Response.Write("<center><br><br><h2><font color=gray>This page is not accessible. Please click on the Logout option and login again.</font></h2></center>");
            Response.End();
        }

        if (!IsPostBack)
        {
            FolderPath = Request.QueryString["Path"].ToString();
            string folderid = Request.QueryString["DocID"].ToString();
            BindFolder();
            Load_DepartmentList();
            BindFleetDLL();
            if (!string.IsNullOrWhiteSpace(Convert.ToString(Session["USERFLEETID"])))
                DDLFleet.SelectItems(new string[] { Convert.ToString(Session["USERFLEETID"]) });
            BindVesselDDL();
            BindHeader();

        }


        if (Request.Form["hdnVesionChecked"] == "true")
        {
            FolderPath = Request.Form["txtFolderName"].ToString();
            hdnVesionChecked.Value = "";
            string FileName = Session["FileName"].ToString();
            string SourcePath = Server.MapPath("TempUpload").ToUpper() + "\\" + FileName;
            string DestinationPath = Server.MapPath(FolderPath);

            ////get the File ID by the file Name
            int fileID = objQMS.getFileIDByPath(FolderPath + "/" + FileName);
            

            objQMS.UpdateVersionInfoOfNewFileAdd(fileID, FileName,Convert.ToInt32(Session["USERID"]), "");
            //need to create a another folder to keep the version file
            DataSet FileInfo = objQMS.getFileVersion(fileID);
            //create the version folder for the file at the first time
            if (FileInfo.Tables[0].Rows.Count > 0)
            {
                bool folderExists = Directory.Exists(DestinationPath + "\\" + FileInfo.Tables[0].Rows[0]["ID"].ToString());
                if (!folderExists)
                {
                    System.IO.Directory.CreateDirectory(DestinationPath + "\\" + FileInfo.Tables[0].Rows[0]["ID"].ToString());
                }

                //create the version folder
                DestinationPath = DestinationPath + "\\" + FileInfo.Tables[0].Rows[0]["ID"].ToString() + "\\" + FileInfo.Tables[1].Rows[0]["Version"].ToString();
                System.IO.Directory.CreateDirectory(DestinationPath);
                File.Copy(SourcePath, DestinationPath + "\\" + FileName, true);
                //txtRemarks.Text = "";

                Response.Redirect("FileLoader.aspx?DocID=" + fileID);
            }

        }

        //UserAccessValidation();
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    public void BindHeader()
    {
        try
        {
            
            DataTable FleetDT = BLL_QMSDB_Folders.QMSDBHeaderList();
            ddlHeaderTemplate.DataSource = FleetDT;
            ddlHeaderTemplate.DataTextField = "HEADERNAME";
            ddlHeaderTemplate.DataValueField = "HEADERID";
            ddlHeaderTemplate.DataBind();
            ddlHeaderTemplate.Items.Insert(0, new ListItem("- Select -", "0"));
        }
        catch (Exception ex)
        {

        }
    }
    protected void DDLFleet_SelectedIndexChanged()
    {
        BindVesselDDL();
        BindFolderToVEsssel();
    }
    protected void DDLVessel_SelectedIndexChanged()
    {
        Session["sVesselCode"] = DDLVessel.SelectedValues;
        BindFolderToVEsssel();
    }
    public void BindVesselDDL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            StringBuilder sbFilterFlt = new StringBuilder();
            string VslFilter = "";
            foreach (DataRow dr in DDLFleet.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            if (sbFilterFlt.Length > 1)
            {
                sbFilterFlt.Remove(sbFilterFlt.Length - 1, 1);
                VslFilter = string.Format("fleetCode in (" + sbFilterFlt.ToString() + ")");
                dtVessel.DefaultView.RowFilter = VslFilter;
            }

            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            Session["sVesselCode"] = DDLVessel.SelectedValues;
            Session["sFleet"] = DDLFleet.SelectedValues;

        }
        catch (Exception ex)
        {

        }
    }
    public void BindFolder()
    {
        try
        {

            DataTable dtFolders = BLL_QMSDB_Folders.QMSDBFoldes_List(null, null, null, null, UDFLib.ConvertIntegerToNull(Request.QueryString["DocID"].ToString()));
            ddlFolderList.DataSource = dtFolders;
            ddlFolderList.DataTextField = "FOLDER_NAME";
            ddlFolderList.DataValueField = "FOLDER_ID";
            ddlFolderList.DataBind();

            ddlFolderName.DataSource = dtFolders;
            ddlFolderName.DataTextField = "FOLDER_NAME";
            ddlFolderName.DataValueField = "FOLDER_ID";
            ddlFolderName.DataBind();

            ddlFolderName.SelectedValue = Request.QueryString["DocID"].ToString();
            if (dtFolders.Rows.Count > 0)
            {
                BindTrvFolder(dtFolders);
                GenerateUL(dtFolders);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private int CreateChildNode(string NodePath, string NodeName, int NodeType, int NodeID, string rootNode)
    {
        try
        {
            string ParentNodePath = NodePath.Replace("/" + NodeName + "/", "");
            ParentNodePath = ParentNodePath.Replace(rootNode, "");
            TreeNode ParentNode = trvFolder.FindNode(ParentNodePath);
            if (ParentNode != null)
            {
                TreeNode NewNode = new TreeNode(NodeName, NodeID.ToString(), getNodeImageURL(NodeName));
                NewNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                NewNode.Collapse();
                ParentNode.ChildNodes.Add(NewNode);
            }
            if (ParentNode == null)
            {
                TreeNode NewNode = new TreeNode(NodeName, NodeID.ToString(), getNodeImageURL(NodeName));
                NewNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                NewNode.Expand();
                trvFolder.Nodes.Add(NewNode);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return 0;
    }
    private int CreateNodes(string NodePath, string NodeName, int NodeType, int NodeID, string rootNode)
    {
        try
        {
            string ParentNodePath = NodePath.Replace("/" + NodeName + "/", "");
            ParentNodePath = ParentNodePath.Replace(rootNode, "");
            TreeNode ParentNode = TreeView1.FindNode(ParentNodePath);
            if (ParentNode != null)
            {
                TreeNode NewNode = new TreeNode(NodeName, NodeName.ToString(), getNodeImageURL(NodeName));
                NewNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                NewNode.Collapse();
                ParentNode.ChildNodes.Add(NewNode);
            }
            if (ParentNode == null)
            {
                TreeNode NewNode = new TreeNode(NodeName, NodeName.ToString(), getNodeImageURL(NodeName));
                NewNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                NewNode.Expand();
                TreeView1.Nodes.Add(NewNode);

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return 0;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            if (txtFolderName.Text.Trim() == "")
            {
                lblMessage.Text = "Please enter folder name.";
                return;
            }
            int i = BLL_QMSDB_Folders.QMSDBFoldes_Insert(UDFLib.ConvertIntegerToNull(ddlFolderList.SelectedValue), txtFolderName.Text, 1, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
            String msg = String.Format("alert('Folder has created');");
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
             ScriptManager.RegisterStartupScript(this, this.GetType(), "reloadparent", "parent.RealoadParent();", true);
        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.Message.Replace("'", "") + "');";
            Page.ClientScript.RegisterStartupScript(GetType(), "script", js, true);
        }
    }

    protected void btnRenameFolder_Click(object sender, EventArgs e)
    {
        try
        {  
        if (txtFolderName.Text.Trim() == "")
        {
            lblMessage.Text = "Please enter folder name.";
            return;
        }
        int i = BLL_QMSDB_Folders.Upd_QMSDBFoldes_Rename(int.Parse(ddlFolderList.SelectedValue), txtFolderName.Text, 1, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
        String msg = String.Format("alert('Folder name has changed');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "reloadparent", "parent.RealoadParent();", true);
        }
        catch (Exception ex)
        {
        }
    }

    public string getNewFolderPath(string newFolderPath)
    {
        string FolderPath = "";
        if (newFolderPath != "")
        {
            int DocumentsIndex = newFolderPath.IndexOf("\\DOCUMENTS");
            FolderPath = newFolderPath.Substring(DocumentsIndex + 1);
        }
        return FolderPath;

    }

    public void addExistingFile(string sFileName)
    {
        string js = "";
        string TempMappedPath = Server.MapPath("TempUpload");
        string[] filePaths = Directory.GetFiles(TempMappedPath);
        foreach (string filePath in filePaths)
        {
            try
            {
                File.Delete(filePath);
            }
            catch { }

        }
        try
        {
            //FileUploader.PostedFile.SaveAs(TempMappedPath + "\\" + sFileName);
            js = "SaveConfirmation();";
            Session["FileName"] = sFileName;
            Page.ClientScript.RegisterStartupScript(GetType(), "script", js, true);
            string VersionYesOrN0 = hdnVesionChecked.Value.ToUpper();
        }
        catch (Exception ex)
        {
            js = "alert('" + ex.Message.Replace("'", "") + "');";
            Page.ClientScript.RegisterStartupScript(GetType(), "script", js, true);
        }

    }




    protected void Load_DepartmentList()
    {
        if (getSessionString("USERCOMPANYID") != "")
        {
            int iCompID = int.Parse(getSessionString("USERCOMPANYID"));
            lstDept.DataSource = objCompBLL.Get_CompanyDepartmentList(iCompID);
            lstDept.DataTextField = "VALUE";
            lstDept.DataValueField = "ID";
            lstDept.DataBind();
            lstDept.Items.Insert(0, new ListItem("- ALL -", "0"));
        }
    }

    protected string getSessionString(string ID)
    {
        string ret = "";
        if (Session[ID] != null)
        {
            ret = Session[ID].ToString();
        }
        else
        {
            Response.Redirect("~/Account/Login.aspx?ReturnURL=~/Infrastructure/Libraries/User.aspx");
        }

        return ret;
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        //SearchCatalog(txtSearch.Text);
        //pnlAdvSearch.Visible = false;
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

    protected void gvSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filepath = ((Label)e.Row.FindControl("lblFileName")).Text;
            ((ImageButton)e.Row.Controls[5].FindControl("ImgOpenExt")).ImageUrl = getNodeImageURL(filepath);

            string strRowId = DataBinder.Eval(e.Row.DataItem, "FileID").ToString();
            ImageButton ImgPreview = (ImageButton)(e.Row.FindControl("ImgOpenForView"));
            if (ImgPreview != null)
                ImgPreview.Attributes.Add("onclick", "javascript:window.location.href='FileLoader.aspx?DocID=" + strRowId + "'");

        }

    }

    protected void gvSearchResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //gvSearchResult.PageIndex = e.NewPageIndex;
        //SearchCatalog(txtSearch.Text);
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

    private void SortGridView(string sortExpression, string direction)
    {
        DataView dv = new DataView((DataTable)Session["SearchResult"]);
        dv.Sort = sortExpression + " " + direction;


        //this.gvSearchResult.DataSource = dv;
        //gvSearchResult.DataBind();
    }

  

    protected void OpenFileExternal(object sendepr, CommandEventArgs Args)
    {
        string sPath = Args.CommandName.ToString();
        //OpenFileExternal(sPath);

        string js = "window.open('" + sPath + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openfile", js, true);
    }

    public void OpenFileExternal(string url)
    {
        //string virtualFilePath = getVirtualFilePath(url);
        //string filepath = Server.MapPath(virtualFilePath);

        FileInfo file = new FileInfo(url);
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

    private string getNodeImageURL(string NodeText)
    {
        string extenssion = Path.GetExtension(NodeText);

        extenssion = extenssion.ToLower();

        if (extenssion.IndexOf(".") >= 0)
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
                case ".docx":
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
                    return "~/images/DocTree/jpg.gif";
                case ".jpeg":
                    return "~/images/DocTree/jpg.gif";
                case ".bmp":
                    return "~/images/DocTree/bmp.gif";
                case ".png":
                    return "~/images/DocTree/bmp.gif";
                case ".rtf":
                    return "~/images/DocTree/page.gif";
                case ".fdc":
                    return "~/images/DocTree/network.gif";
                default:
                    return "~/images/DocTree/page.gif";
            }

        }
        else
        { return "~/images/DocTree/folder.gif"; }
    }

 
   

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        ViewState["sortDirectionTEXT"] = System.Web.UI.WebControls.SortDirection.Ascending;
        //SearchCatalog(txtSearch.Text);

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        //txtSearch.Text = "";
        //txtFromDate.Text = "";
        //txtToDate.Text = "";
        //pnlAdvSearch.Visible = false;

    }

    protected void ImgBtnAdvSearch_Click(object sender, ImageClickEventArgs e)
    {
        //pnlAdvSearch.Visible = true;
        //txtSearch.Text = "";
        //lblParentFolder.Text = "DOCUMENTS";
    }

    protected void lstDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strDept = "";
        BLL_Infra_UserCredentials objInfra = new BLL_Infra_UserCredentials();

        if (lstDept.Items[0].Selected == true)
        {
            int UserCompanyID = int.Parse(getSessionString("USERCOMPANYID"));
            DataTable dtUsers = objInfra.Get_UserList(UserCompanyID);
            lstUser.DataSource = dtUsers;
            lstUser.DataBind();
            lstUser.Items.Insert(0, new ListItem("- ALL -", "0"));
        }
        else
        {
            foreach (ListItem li in lstDept.Items)
            {
                if (li.Selected == true)
                {
                    if (strDept.Length > 0) strDept += ",";
                    strDept += li.Value;
                }
            }

            if (strDept.Length > 0)
            {
                int UserCompanyID = int.Parse(getSessionString("USERCOMPANYID"));
                DataTable dtUsers = objInfra.Get_UserList_By_Dept_DL(UserCompanyID, strDept);
                lstUser.DataSource = dtUsers;
                lstUser.DataBind();
                lstUser.Items.Insert(0, new ListItem("- ALL -", "0"));
            }

        }

    }

    protected void btnRemoveFolderAccess_Click(object sender, EventArgs e)
    {
        if (txtFolderName.Text.Trim().Length > 0)
        {
            int iFolderID = objQMS.getFileIDByPath(txtFolderName.Text.Trim());
           

            //objQMS.Delete_User_Folder_Access(iFolderID);
            if (lstUser.Items[0].Selected == true)
            {
                for (int i = 0; i < lstUser.Items.Count; i++)
                {
                    objQMS.Delete_User_Folder_Access(iFolderID, int.Parse(lstUser.Items[i].Value));
                }
            }
            else
            {
                for (int i = 0; i < lstUser.Items.Count; i++)
                {
                    if (lstUser.Items[i].Selected == true)
                    {
                        objQMS.Delete_User_Folder_Access(iFolderID, int.Parse(lstUser.Items[i].Value));
                    }
                }
            }

            objQMS.Insert_User_Folder_Access(iFolderID, GetSessionUserID(), GetSessionUserID());

            lblMessage.Text = "Folder access changed successfully.";
        }
    }

    protected void btnAddFolderAccess_Click(object sender, EventArgs e)
    {
        if (txtFolderName.Text.Trim().Length > 0)
        {
            int iFolderID = objQMS.getFileIDByPath(txtFolderName.Text.Trim());
           

            //objQMS.Delete_User_Folder_Access(iFolderID);
            if (lstUser.Items[0].Selected == true)
            {
                for (int i = 0; i < lstUser.Items.Count; i++)
                {
                    objQMS.Insert_User_Folder_Access(iFolderID, int.Parse(lstUser.Items[i].Value), GetSessionUserID());
                }
            }
            else
            {
                for (int i = 0; i < lstUser.Items.Count; i++)
                {
                    if (lstUser.Items[i].Selected == true)
                    {
                        objQMS.Insert_User_Folder_Access(iFolderID, int.Parse(lstUser.Items[i].Value), GetSessionUserID());
                    }
                }
            }

            lblMessage.Text = "Folder access changed successfully.";
        }
    }

  

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        UserAccess objUA = new UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            Response.Write("<center><br><br><h2><font color=gray>You do not have enough priviledge to access this page.</font></h2></center>");
            Response.End();
        }
        if (objUA.Add == 0)
        {
            pnlUploadDocument.Enabled = false;
            //btnAddFolderAccess.Enabled = false;
        }
        if (objUA.Edit == 0)
        {
            pnlUploadDocument.Enabled = false;

        }
        if (objUA.Delete == 0)
        {

            //btnRemoveFolderAccess.Enabled = false;
        }

        if (objUA.Approve == 0)
        {

        }

    }

    protected void trvFolder_SelectedNodeChanged(object sender, EventArgs e)
    {
        BindFolderToVEsssel();
    }

    protected void BindFolderToVEsssel()
    {
        if (trvFolder.SelectedNode != null)
        {
            int rowcount = ucCustomPager1.isCountRecord;
            string NodeValue = trvFolder.SelectedNode.Value;
            DataTable dt = BLL_QMSDB_Folders.QMSDBFoldes_VesselAccess((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"], UDFLib.ConvertIntegerToNull(NodeValue), ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount);
            gvFolderAccessVessel.DataSource = dt;
            gvFolderAccessVessel.DataBind();
            if (ucCustomPager1.isCountRecord == 1)
            {
                ucCustomPager1.CountTotalRec = rowcount.ToString();
                ucCustomPager1.BuildPager();
            }
        }

    }
    protected void BindFolderToUserAccess()
    {
        if (TreeView1.SelectedNode != null)
        {
            int rowcount = ucCustomPagerItems.isCountRecord;
            string NodeValue = TreeView1.SelectedNode.Value;
            DataTable dt = BLL_QMSDB_Folders.QMSDBFoldes_UserAccess(UDFLib.ConvertIntegerToNull(lstDept.SelectedValue), UDFLib.ConvertIntegerToNull(lstUser.SelectedValue), UDFLib.ConvertIntegerToNull(NodeValue), ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            dvFolderUserAccess.DataSource = dt;
            dvFolderUserAccess.DataBind();
            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }
        }

    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        BindFolderToUserAccess();
    }
    private void GenerateUL(DataTable dt)
    {

        DataRow[] drs = dt.Select("Folder_Id='" + Request.QueryString["DocID"].ToString() + "'");
        int meni = 0;
        int child = 0;
        //CreateChildNode(dr["XNPATH"].ToString(), dr["FOLDER_NAME"].ToString(), 1, Convert.ToInt32(dr["FOLDER_ID"].ToString()), dtFolders.Rows[0]["XNPATH"].ToString().Replace("/" + dtFolders.Rows[0]["FOLDER_NAME"].ToString(), ""));
        foreach (DataRow dr in drs)
        {
            TreeNode mi = new TreeNode(dr["FOLDER_NAME"].ToString().Trim(), dr["FOLDER_ID"].ToString(), getNodeImageURL(dr["FOLDER_NAME"].ToString().Trim()));
            mi.ExpandAll();
            TreeView1.Nodes.Add(mi);

            DataRow[] drInners = dt.Select("PARENT_FOLDER_ID ='" + dr["FOLDER_ID"].ToString() + "' ");
            if (drInners.Length != 0)
            {
                child = 0;
                foreach (DataRow drInner in drInners)
                {
                    TreeNode miner;
                    miner = new TreeNode(drInner["FOLDER_NAME"].ToString().Trim(), drInner["FOLDER_ID"].ToString(), getNodeImageURL(drInner["FOLDER_NAME"].ToString().Trim()));

                    TreeView1.Nodes[meni].ChildNodes.Add(miner);
                    TreeView1.Nodes[meni].CollapseAll();

                    string filter = "PARENT_FOLDER_ID ='" + drInner["FOLDER_ID"].ToString() + "'";

                    dt.AcceptChanges();
                    DataRow[] drInnerLinks = dt.Select(filter);

                    if (drInnerLinks.Length != 0)
                    {
                        foreach (DataRow drInnerLink in drInnerLinks)
                        {
                            TreeNode milink = new TreeNode(drInnerLink["FOLDER_NAME"].ToString().Trim(), drInnerLink["FOLDER_ID"].ToString(), getNodeImageURL(drInnerLink["FOLDER_NAME"].ToString().Trim()));
                            TreeView1.Nodes[meni].ChildNodes[child].ChildNodes.Add(milink);
                            TreeView1.Nodes[meni].ChildNodes[child].CollapseAll();
                        }
                    }
                    child++;
                }
            }
            meni++;
        }




    }
    private void BindTrvFolder(DataTable dt)
    {

        DataRow[] drs = dt.Select("Folder_Id='" + Request.QueryString["DocID"].ToString() + "'");
        int meni = 0;
        int child = 0;
        foreach (DataRow dr in drs)
        {
            TreeNode mi = new TreeNode(dr["FOLDER_NAME"].ToString().Trim(), dr["FOLDER_ID"].ToString(), getNodeImageURL(dr["FOLDER_NAME"].ToString().Trim()));
            trvFolder.Nodes.Add(mi);

            DataRow[] drInners = dt.Select("PARENT_FOLDER_ID ='" + dr["FOLDER_ID"].ToString() + "' ");
            if (drInners.Length != 0)
            {
                child = 0;
                foreach (DataRow drInner in drInners)
                {
                    TreeNode miner;
                    miner = new TreeNode(drInner["FOLDER_NAME"].ToString().Trim(), drInner["FOLDER_ID"].ToString(), getNodeImageURL(drInner["FOLDER_NAME"].ToString().Trim()));

                    trvFolder.Nodes[meni].ChildNodes.Add(miner);
                    trvFolder.Nodes[meni].CollapseAll();

                    string filter = "PARENT_FOLDER_ID ='" + drInner["FOLDER_ID"].ToString() + "'";

                    dt.AcceptChanges();
                    DataRow[] drInnerLinks = dt.Select(filter);

                    if (drInnerLinks.Length != 0)
                    {
                        foreach (DataRow drInnerLink in drInnerLinks)
                        {
                            TreeNode milink = new TreeNode(drInnerLink["FOLDER_NAME"].ToString().Trim(), drInnerLink["FOLDER_ID"].ToString(), getNodeImageURL(drInnerLink["FOLDER_NAME"].ToString().Trim()));
                            trvFolder.Nodes[meni].ChildNodes[child].ChildNodes.Add(milink);
                            trvFolder.Nodes[meni].ChildNodes[child].CollapseAll();
                        }
                    }
                    child++;
                }
            }
            meni++;
        }



    }
    protected void btnVesselRemove_Click(object sender, EventArgs e)
    {
        try
        {


        }
        catch (Exception ex)
        {

        }
    }
    protected void btnVesselSave_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvr in gvFolderAccessVessel.Rows)
            {
                if ((gvr.FindControl("chkVessel") as CheckBox).Checked == true)
                {
                    string lblFleet = ((Label)gvr.FindControl("lblFleet")).Text;
                    string VesselId = ((Label)gvr.FindControl("lblVesselId")).Text; ;
                    string fldFloderId = ((Label)gvr.FindControl("lblFolderId")).Text;
                    int i = BLL_QMSDB_Folders.QMSDBFoldes_Insert_Vessel(UDFLib.ConvertIntegerToNull(lblFleet), UDFLib.ConvertIntegerToNull(VesselId), UDFLib.ConvertIntegerToNull(fldFloderId), UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()), 1);
                }
            }
            BindFolderToVEsssel();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnResetMenu_Click(object sender, EventArgs e)
    {
        try
        {
            //int i = BLL_QMSDB_Folders.QMSDBFoldes_VesselAccess(null, null, null);
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnAccessSave_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvr in dvFolderUserAccess.Rows)
            {
                if ((gvr.FindControl("chkAll") as CheckBox).Checked == true || (gvr.FindControl("chkView") as CheckBox).Checked == true || (gvr.FindControl("chkAdd") as CheckBox).Checked == true || (gvr.FindControl("chkEdit") as CheckBox).Checked == true || (gvr.FindControl("chkDelete") as CheckBox).Checked == true)
                {
                    string lblDepartment = ((Label)gvr.FindControl("lblDeptId")).Text;
                    string lblUserId = ((Label)gvr.FindControl("lblUerId")).Text; ;
                    string fldFloderId = ((Label)gvr.FindControl("lblFolderId")).Text;
                    if ((gvr.FindControl("chkAll") as CheckBox).Checked == true)
                    {
                        int i = BLL_QMSDB_Folders.QMSDBFoldes_Insert_UserAccess(UDFLib.ConvertIntegerToNull(lblDepartment), UDFLib.ConvertIntegerToNull(lblUserId), UDFLib.ConvertIntegerToNull(fldFloderId), 1, 1, 1, 1, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
                    }
                    else
                    {
                        int chkView = (gvr.FindControl("chkView") as CheckBox).Checked == true ? 1 : 0;
                        int chkAdd = (gvr.FindControl("chkAdd") as CheckBox).Checked == true ? 1 : 0;
                        int chkEdit = (gvr.FindControl("chkEdit") as CheckBox).Checked == true ? 1 : 0;
                        int chkDelete = (gvr.FindControl("chkDelete") as CheckBox).Checked == true ? 1 : 0;
                        int i = BLL_QMSDB_Folders.QMSDBFoldes_Insert_UserAccess(UDFLib.ConvertIntegerToNull(lblDepartment), UDFLib.ConvertIntegerToNull(lblUserId), UDFLib.ConvertIntegerToNull(fldFloderId), chkView, chkAdd, chkEdit, chkDelete, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
                    }
                }
            }
            BindFolderToUserAccess();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSaveCommand_Click(object sender, EventArgs e)
    {
        try
        {

            int PROCEDURE_ID = BLL_QMSDB_Procedures.QMSDBProcedures_InsertWithSection(txtProcedureCode.Text, txtProcedureName.Text, UDFLib.ConvertToInteger(ddlFolderName.SelectedValue), FileWateMark.Checked == true ? 1 : 0, UDFLib.ConvertIntegerToNull(ddlHeaderTemplate.SelectedValue), UDFLib.ConvertIntegerToNull(ddlFooterTemlate.SelectedValue), UDFLib.ConvertToInteger(Session["USERID"]), "");
            if (PROCEDURE_ID > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "reloadparent", "parent.RealoadParent();", true);
                ResponseHelper.Redirect("ProcedureBuilder.aspx?PROCEDURE_ID=" + PROCEDURE_ID.ToString(), "blank", "");

            }
        }

        catch (Exception ex)
        {
            string js = "alert('" + ex.Message.Replace("'", "") + "');";
            Page.ClientScript.RegisterStartupScript(GetType(), "script", js, true);
        }
    }

    protected void btnCancelCommand_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "reloadparent", "parent.RealoadParent();", true);
    }
    protected void lstUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFolderToUserAccess();
    }
}

