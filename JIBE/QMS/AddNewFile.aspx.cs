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
using System.Drawing;
using System.Text.RegularExpressions;
using SMS.Business.QMS;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class AddNewFile : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document();
    BLL_Infra_Company objCompBLL = new BLL_Infra_Company();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();

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
            FolderPath = Convert.ToString(Request.QueryString["Path"]);

            txtFolderName.Text = FolderPath;
            lblParentFolder.Text = FolderPath;
            if (txtFolderName.Text == "DOCUMENTS/")                           //If Root folder is selected then all control on Edit panel will get disable
            {
                
                pnlEditFolder.Enabled = false;
            }
            Load_DepartmentList();
            Load_UserList();
            string strName = FolderPath.Remove(FolderPath.Length - 1, 1);
            txtParentFolderE.Text = strName.Substring(0, strName.LastIndexOf('/') + 1);
            txtFolderNameE.Text = strName.Substring(strName.LastIndexOf('/') + 1);
            GetFolderAccess(chklstUser);
            GetApprovalRequired();
        }

        // when uploading the same file again
        // and click OK
        if (Request.Form["hdnVesionChecked"] == "true")
        {
            FolderPath = Convert.ToString(Request.Form["txtFolderName"]);
            hdnVesionChecked.Value = "";
            string FileName = Convert.ToString(Session["FileName"]);
            string SourcePath = Server.MapPath("TempUpload").ToUpper() + "\\" + FileName;
            string DestinationPath = Server.MapPath(FolderPath);

            ////get the File ID by the file Name
            //int fileID = objQMS.getFileIDByPath(FolderPath + FileName);

            int fileID = objQMS.checkFileExits(FileName, txtFolderName.Text);

            string oldfileName = "";
            DataSet dsFileDetails = objQMS.getFileDetailsByID(fileID, 0);

            if (dsFileDetails.Tables[0].Rows.Count > 0)
            {
                oldfileName = System.IO.Path.GetFileName(Convert.ToString(dsFileDetails.Tables[0].Rows[0]["FilePath"]));

                string sFileExt = System.IO.Path.GetExtension(FileName);
                string sGuidFileName = "QMS_" + System.Guid.NewGuid() + sFileExt;

                File.Copy(SourcePath, DestinationPath + "\\" + sGuidFileName, true);
                
                objQMS.UpdateVersionInfoOfNewFileAdd(fileID, sGuidFileName, Convert.ToInt32(Session["USERID"]), txtRemarks.Text.Trim());
                
                txtRemarks.Text = "";
                Response.Redirect("FileLoader.aspx?DocID=" + fileID);

            }
            
        }

        UserAccessValidation();
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Convert.ToString(Session["USERID"]));
        else
            return 0;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        string ParentFolderPath = txtFolderName.Text;
        string ParentFolderMapPath = Server.MapPath(ParentFolderPath);
        string[] tempPath = ParentFolderPath.Split('/');
        string ParentFolderName = tempPath[tempPath.Length - 1];

        int ParentFolderID = UDFLib.ConvertToInteger(Request.QueryString["id"]);

        try
        {
            if (Convert.ToString(rdoDocumentType.SelectedValue) == "FILE")
            {
                //this section for the adding the file 
                txtNewFolderName.Enabled = false;

                if (FileUploader.HasFile)
                {
                     if (ParentFolderPath != "DOCUMENTS/")
                    {
                        DataTable dt = new DataTable();
                        dt = objUploadFilesize.Get_Module_FileUpload("FBM_");
                   
                        if (dt.Rows.Count > 0)
                        {
                            string datasize = dt.Rows[0]["Size_KB"].ToString();
                            if (FileUploader.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                            {
                            
                                string filepath = FileUploader.PostedFile.FileName;
                                string sFileName = System.IO.Path.GetFileName(FileUploader.PostedFile.FileName);

                                string sFileExt = System.IO.Path.GetExtension(sFileName);
                                string sGuidFileName = "QMS_" + System.Guid.NewGuid() + sFileExt;
                            

                                int fileID = objQMS.checkFileExits(sFileName, ParentFolderPath);

                                //int FileExistCount = objQMS.getFileCountByFileID(fileID);
                                if (fileID == 0)
                                {
                                    FileUploader.PostedFile.SaveAs(ParentFolderMapPath + sGuidFileName);
                                      FileInfo f = new FileInfo(ParentFolderMapPath + sGuidFileName);
                                    long filelengh = f.Length;
                                    int FileID = objQMS.Add_NewDocument(ParentFolderID, sFileName, ParentFolderPath + sGuidFileName, Convert.ToInt32(Session["USERID"]), txtRemarks.Text.Trim(), filelengh);
                                    txtRemarks.Text = "";

                                    //For File Approval 
                                    DataTable dtApproval_Level = objQMS.Check_FolderApprovalExists(ParentFolderID);
                                  

                                    BLL_Infra_UploadFileSize objBLL = new BLL_Infra_UploadFileSize();
                                    int rowcountx = 0;
                                    long defsize = 450 * 1024;
                                    DataTable dtx = objBLL.SearchConfigureFileSize("QMS_", null, null, 1, 10, ref  rowcountx);
                                    if (dtx.Rows.Count > 0)
                                    {
                                        if (dtx.Rows[0]["Syncable"].ToString() == "1")
                                        {
                                            defsize = Convert.ToInt32(dtx.Rows[0]["Size_KB"]) * 1024;
                                        }
                                    }

                                    if (dtApproval_Level.Rows.Count > 0)
                                    {
                                        string iFolderName = tempPath[tempPath.Length - 2];
                                        objQMS.CreateNewFileApproval(FileID, sFileName, ParentFolderID, iFolderName, dtApproval_Level, Convert.ToInt32(Session["USERID"]), Convert.ToInt32(Session["USERCOMPANYID"]));
                                    }
                                    if (FileID > 0)
                                    {

                                        lblMessage.Text = "File added successfully.";
                                        string jsFile = "alert('File added successfully.');parent.ChildCallBack();";
                                        long sizeinkb = defsize / 1024;
                                        if (filelengh > defsize)
                                        {
                                            lblMessage.Text += "This file exceeds the file size limit(" + sizeinkb + "KB), you have to sync this file manually";
                                            jsFile = "alert('File added successfully; This file exceeds the file size limit(" + sizeinkb + "KB), you have to sync this file manually.');parent.ChildCallBack();";
                                        }


                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", jsFile, true);

                                    }

                                }
                                else
                                {
                                    DataSet dsOperationInfo = objQMS.getCheckedFileInfo(fileID);

                                    //get the last row of the table to know the current status of the file
                                    int dataRow = dsOperationInfo.Tables[0].Rows.Count - 1;
                                    string FileStatus = Convert.ToString(dsOperationInfo.Tables[0].Rows[dataRow]["Operation_Type"]);
                                    int UserID = Convert.ToInt32(Convert.ToString(dsOperationInfo.Tables[0].Rows[dataRow]["UserID"]));
                                    string UserName = Convert.ToString(dsOperationInfo.Tables[0].Rows[dataRow]["UserName"]);
                             
                                    if (dsOperationInfo.Tables[0].Rows.Count > 0)
                                    {

                                        if (FileStatus.ToUpper().Equals("CHECKED OUT") == true && (UserID.Equals(Convert.ToString(Session["USERID"])) == false))
                                        {
                                            string Message = "file is already checkout by " + UserName + ". File should be CHECKED-IN before uploading again.";
                                            string js = "alert('" + Message + "')";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgz", js, true);
                                        }

                                        else
                                        {
                                            addExistingFile(fileID, sFileName);
                                        }
                                    }
                                    else
                                    {
                                        addExistingFile(fileID, sFileName);
                                    }
                                }
                            }
                            else
                            {
                                lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                            }
                        }
                        else
                        {
                            string js2 = "alert('Upload size not set!');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);

                        }
                    }
                    else
                    {
                        lblMessage.Text = "Please select a parent folder.";
                    }
                }
                else
                {
                    lblMessage.Text = "Please browse the file from the system.";

                }
            }
            else
            {
                //THIS SECTION FOR THE ADDING THE FOLDER 

                if (txtNewFolderName.Text.Trim() != "")
                {
                    int iUserCount = 0;
                    Regex regex;
                    regex = new Regex(@"^[a-zA-Z0-9-' '_.]*$");
                    if ((ddlLevel1.SelectedValue != "0" && chkReqApproval.Checked == true) || chkReqApproval.Checked == false)
                    {
                        if ((ddlLevel1.SelectedValue != ddlLevel2.SelectedValue && ddlLevel1.SelectedValue != ddlLevel3.SelectedValue && (ddlLevel2.SelectedValue != ddlLevel3.SelectedValue || ddlLevel2.SelectedValue == "0")) || chkReqApproval.Checked == false)
                        {
                            if (regex.IsMatch(txtNewFolderName.Text.Trim()))
                            {
                                string NewFolderMapPath = Server.MapPath(ParentFolderPath + txtNewFolderName.Text.Trim());

                                bool folderExists = Directory.Exists(NewFolderMapPath);

                                if (!folderExists)
                                {

                                    if (lstUser.Items.Count > 0)
                                    {

                                        for (int i = 0; i < lstUser.Items.Count; i++)
                                        {
                                            if (lstUser.Items[i].Selected == true)
                                            {
                                                iUserCount += 1;
                                                break;
                                            }

                                        }

                                        if (iUserCount > 0)
                                        {
                                            System.IO.Directory.CreateDirectory(NewFolderMapPath);
                                            ParentFolderID = UDFLib.ConvertToInteger(Convert.ToString(Request.QueryString["id"]));

                                            DataTable dtUsers = GetFolderAccessUserList();

                                            //int iFolderID = objQMS.insertRecordAtFolderCreation(txtNewFolderName.Text.Trim(), ParentFolderName, ParentFolderPath + txtNewFolderName.Text.Trim(), NodeType);
                                            int iFolderID = objQMS.CreateNewFolder(txtNewFolderName.Text.Trim(), ParentFolderID, dtUsers, GetSessionUserID());

                                            //Added for Folder Approval
                                            if (chkReqApproval.Checked == true)
                                            {
                                                DataTable dtApproval_Level = new DataTable();
                                                DataColumn dtLevelID = new DataColumn("Level_ID");
                                                DataColumn dtApproverID = new DataColumn("Approver_ID");
                                                dtApproval_Level.Columns.Add(dtLevelID);
                                                dtApproval_Level.Columns.Add(dtApproverID);
                                                for (int i = 1; i < 4; i++)
                                                {
                                                    string Level = "ddlLevel";
                                                    Level = Level + i;
                                                    DropDownList Leveli = (DropDownList)pnlDeptFolder.FindControl(Level);
                                                    if (Leveli.SelectedValue != "0")
                                                    {
                                                        DataRow dr = dtApproval_Level.NewRow();
                                                        dr["Level_ID"] = i;
                                                        dr["Approver_ID"] = Convert.ToInt32(Leveli.SelectedValue);
                                                        dtApproval_Level.Rows.Add(dr);
                                                    }
                                                }
                                                objQMS.CreateNewFolderApproval(iFolderID, dtApproval_Level, GetSessionUserID());
                                            }
                                        

                                            if (iFolderID > 0)
                                            {
                                                lblMessage.Text = "Folder created successfully.";
                                                string js = "alert('Folder created successfully.');parent.ChildCallBack();";
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                                            }

                                        }
                                        else
                                        {
                                            lblMessage.Text = "Folder access must be given to atleast one user.";
                                            string js2 = "alert('Folder access must be given to atleast one user.');";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg2", js2, true);
                                        }
                                    }
                                    else
                                    {
                                        lblMessage.Text = "Folder access must be given to atleast one user.";
                                        string js3 = "alert('Folder access must be given to atleast one user.');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg3", js3, true);
                                    }
                                }
                                else
                                {
                                    lblMessage.Text = "Folder with same name already exists !!.";
                                    string js4 = "alert('Folder with same name already exists !!.');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg4", js4, true);
                                }
                            }
                            else
                            {
                                lblMessage.Text = "This is not a valid folder name,.it should  be alphanumeric, underscore and whitespace.";
                                string js5 = "alert('This is not a valid folder name,.it should  be alphanumeric, underscore and whitespace.')";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg5", js5, true);
                            }
                        }
                        else
                        {
                            if (ddlLevel1.SelectedValue == ddlLevel2.SelectedValue)
                            {
                                ddlLevel2.SelectedValue = "0";
                                ddlLevel3.Enabled = false;
                                ddlLevel3.SelectedValue = "0";
                            }
                            else
                            {
                                ddlLevel3.SelectedValue = "0";
                            }

                            lblMessage.Text = "User is already selected! please select different user.";
                            string js6 = "alert('User is already selected! please select different user.')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg6", js6, true);
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Please select a user to approve!";
                        string js7 = "alert('Please select a user to approve!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg7", js7, true);
                    }
                }
                else
                {
                    lblMessage.Text = "Please enter folder name.";
                }
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

    }

    private DataTable GetFolderAccessUserList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PID", typeof(int));

        //--USERID LIST--
        if (lstUser.Items[0].Selected == true)
        {
            for (int i = 1; i < lstUser.Items.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["PID"] = UDFLib.ConvertToInteger(lstUser.Items[i].Value);
                dt.Rows.Add(dr);
            }
        }
        else
        {
            for (int i = 1; i < lstUser.Items.Count; i++)
            {
                if (lstUser.Items[i].Selected == true)
                {
                    DataRow dr = dt.NewRow();
                    dr["PID"] = UDFLib.ConvertToInteger(lstUser.Items[i].Value);
                    dt.Rows.Add(dr);
                }
            }
        }
        return dt;
    }

    private void RenameFolder(string oldFolderPath, string newFolderName,int FolderID)
    {
        bool folderExists = Directory.Exists(Server.MapPath(oldFolderPath));
        if (folderExists)
        {
            string newFolderPath = oldFolderPath.Substring(0, oldFolderPath.LastIndexOf('/')) + "/" + newFolderName;

            System.IO.Directory.Move(Server.MapPath(oldFolderPath), Server.MapPath(newFolderPath));
            //System.IO.Directory.Delete(Server.MapPath(oldFolderPath));
            objQMS.RenameFolder(oldFolderPath, newFolderName,FolderID);
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

    public void addExistingFile(int fileID, string sFileName)
    {
        string js = "";
        

        // clean the tempUpload folder 
        string TempUploadPath = Server.MapPath("TempUpload");

        bool folderExists = Directory.Exists(TempUploadPath);
        if (!folderExists)
        {
            System.IO.Directory.CreateDirectory(TempUploadPath);
        }

        string[] filePaths = Directory.GetFiles(TempUploadPath);
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
            FileUploader.PostedFile.SaveAs(TempUploadPath + "\\" + sFileName);
            
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

    protected void rdoDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {

        changeControlStatus();
    }

    protected void chkReqApproval_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkReqApproval.Checked == true)
        {
            ddlLevel1.Enabled = true;
            ddlLevel2.Enabled = false;
            ddlLevel3.Enabled = false;
        }
        else
        {
            ddlLevel1.Enabled = false;
            ddlLevel2.Enabled = false;
            ddlLevel3.Enabled = false;
            ddlLevel1.SelectedValue = "0";
            ddlLevel2.SelectedValue = "0";
            ddlLevel3.SelectedValue = "0";
        }
    }

    protected void ddlLevel1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLevel1.SelectedValue != "0")
        {
            ddlLevel2.Enabled = true;

        }
        else
        {
            ddlLevel2.Enabled = false;
            ddlLevel3.Enabled = false;
            ddlLevel2.SelectedValue = "0";
            ddlLevel3.SelectedValue = "0";
        }
    }

    protected void ddlLevel2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLevel2.SelectedValue != "0")
        {
            ddlLevel3.Enabled = true;

        }
        else
        {
            ddlLevel3.Enabled = false;
            ddlLevel3.SelectedValue = "0";
        }
    }

    public void changeControlStatus()
    {
        if (rdoDocumentType.SelectedValue.ToString().Equals("FOLDER") == true)
        {
            lblFolderName.Enabled = true;
            txtNewFolderName.Enabled = true;
            lblRemarks.Enabled = false;
            txtRemarks.BackColor = Color.White;
            txtRemarks.Enabled = false;
            FileUploader.Enabled = false;

            FileUploader.BackColor = Color.White;
            btnSave.Text = "Add New Folder";

            pnlDeptFolder.Visible = true;
            pnlBrowseFile.Visible = false;

            if (Convert.ToString(Session["ACCESSLEVEL"]) == "1")
            {
                btnAddFolderAccess.Visible = true;
                btnRemoveFolderAccess.Visible = true;
            }
            else
            {
                btnAddFolderAccess.Visible = false;
                btnRemoveFolderAccess.Visible = false;
            }

            GetFolderAccess(lstUser);
        }
        else
        {

            lblFolderName.Enabled = false;
            txtNewFolderName.Enabled = false;
            lblRemarks.Enabled = true;
            txtRemarks.Enabled = true;
            FileUploader.Enabled = true;
            txtNewFolderName.BackColor = Color.White;

            btnSave.Text = "Add Document";
            btnAddFolderAccess.Visible = false;
            btnRemoveFolderAccess.Visible = false;

            pnlDeptFolder.Visible = false;
            pnlBrowseFile.Visible = true;
        }
    }

    protected void Load_DepartmentList()
    {
        if (getSessionString("USERCOMPANYID") != "")
        {
            int iCompID = int.Parse(getSessionString("USERCOMPANYID"));
            DataTable dt = objCompBLL.Get_CompanyDepartmentList(iCompID);
            lstDept.DataSource = dt;
            lstDept.DataTextField = "VALUE";
            lstDept.DataValueField = "ID";
            lstDept.DataBind();
            lstDept.Items.Insert(0, new ListItem("- ALL -", "0"));


            chklstDept.DataSource = dt;
            chklstDept.DataTextField = "VALUE";
            chklstDept.DataValueField = "ID";
            chklstDept.DataBind();
            chklstDept.Items.Insert(0, new ListItem("- ALL -", "0"));
        }
    }
    public void GetFolderAccess(CheckBoxList UserList)
    {
        DataTable dt = objQMS.GET_FolderUser(UDFLib.ConvertToInteger(Request.QueryString["id"]));
        foreach (DataRow row in dt.Rows)
        {
            if (row["UserID"].ToString() != "0")
            {
                foreach (ListItem item in UserList.Items)
                {
                    if (row["UserID"].ToString() == item.Value.ToString())
                    {

                        item.Selected = true;
                        break;

                    }
                }
                // if (row["UserID"].ToString() == "0")
                //  break;
            }
        }
    }
    protected string getSessionString(string ID)
    {
        string ret = "";
        if (Session[ID] != null)
        {
            ret = Convert.ToString(Session[ID]);
        }
        else
        {
            Response.Redirect("~/Account/Login.aspx?ReturnURL=~/Infrastructure/Libraries/User.aspx");
        }

        return ret;
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        SearchCatalog();
        pnlAdvSearch.Visible = false;
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
        gvSearchResult.PageIndex = e.NewPageIndex;
        SearchCatalog();
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


        this.gvSearchResult.DataSource = dv;
        gvSearchResult.DataBind();
    }

    protected void ShowFile(object sendepr, CommandEventArgs Args)
    {
        BLL_QMS_Document objQMS = new BLL_QMS_Document();

        int fileID = 0;
        string sPath = Convert.ToString(Args.CommandName);

        fileID = getFileIDByDocPath(sPath);
        int MaxVersionByFileID = objQMS.getMaxVersionFromParentTable(fileID);
        string msg = String.Format("window.location.href='fileloader.aspx?docid=" + fileID + "';");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
    }

    protected void OpenFileExternal(object sendepr, CommandEventArgs Args)
    {
        string sPath = Convert.ToString(Args.CommandName);
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
            Response.AddHeader("Content-Length", Convert.ToString(file.Length));
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

    //private string getNodeImageURL(string NodeText)
    //{
    //    string extenssion = Path.GetExtension(NodeText);

    //    if (NodeText.IndexOf(".") > 0)
    //    {
    //        switch (extenssion)
    //        {
    //            case ".xls":
    //                return "~/images/DocTree/xls.gif";
    //            case ".xlsx":
    //                return "~/images/DocTree/xls.gif";
    //            case ".pdf":
    //                return "~/images/DocTree/pdf.gif";
    //            case ".htm":
    //                return "~/images/DocTree/page.gif";
    //            case ".html":
    //                return "~/images/DocTree/page.gif";
    //            case ".txt":
    //                return "~/images/DocTree/txt.gif";
    //            case ".doc":
    //                return "~/images/DocTree/doc.gif";
    //            case ".docx":
    //                return "~/images/DocTree/doc.gif";

    //            case ".tiff":
    //                return "~/images/DocTree/gif.gif";
    //            case ".tif":
    //                return "~/images/DocTree/gif.gif";
    //            case ".zip":
    //                return "~/images/DocTree/zip.gif";
    //            case ".csv":
    //                return "~/images/DocTree/xls.gif";
    //            case ".gif":
    //                return "~/images/DocTree/bmp.gif";
    //            case ".jpg":
    //                return "~/images/DocTree/page.gif";
    //            case ".jpeg":
    //                return "~/images/DocTree/jpeg.gif";
    //            case ".bmp":
    //                return "~/images/DocTree/bmp.gif";
    //            case ".png":
    //                return "~/images/DocTree/bmp.gif";
    //            case ".rtf":
    //                return "~/images/DocTree/page.gif";
    //            case "FDC":
    //                return "~/images/DocTree/network.gif";
    //            default:
    //                return "~/images/DocTree/page.gif";
    //        }

    //    }
    //    else
    //    { return "~/images/DocTree/folder.gif"; }
    //}

    public int getFileIDByDocPath(string IndexingPath)
    {
        BLL_QMS_Document objQMS = new BLL_QMS_Document();

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

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        ViewState["sortDirectionTEXT"] = System.Web.UI.WebControls.SortDirection.Ascending;
        SearchCatalog();

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        pnlAdvSearch.Visible = false;

    }

    protected void ImgBtnAdvSearch_Click(object sender, ImageClickEventArgs e)
    {
        pnlAdvSearch.Visible = true;
        //txtSearch.Text = "";
        //lblParentFolder.Text = "DOCUMENTS";
    }

    /// <summary>
    /// Depending on department only office user should be displayed
    /// </summary>
    protected void lstDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strDept = "";
        BLL_Infra_UserCredentials objInfra = new BLL_Infra_UserCredentials();
        string result = Request.Form["__EVENTTARGET"];
        string[] checkedBox = result.Split('$'); ;
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (index == 0)
        {
            if (lstDept.Items[0].Selected == true)
            {
                int UserCompanyID = int.Parse(getSessionString("USERCOMPANYID"));
                DataTable dtUsers = objInfra.Get_UserList(UserCompanyID,"");
                lstUser.DataSource = dtUsers;
                lstUser.DataBind();
                lstUser.Items.Insert(0, new ListItem("- ALL -", "0"));
                for (int i = 1; i < chklstDept.Items.Count; i++)
                {
                    lstDept.Items[i].Selected = true;
                }
            }
            else
            {
                for (int i = 1; i < lstDept.Items.Count; i++)
                {
                    lstDept.Items[i].Selected = false;
                }
            }
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
        DataTable dt = objQMS.GET_FolderUser(UDFLib.ConvertToInteger(Request.QueryString["id"]));
        foreach (DataRow row in dt.Rows)
        {
            foreach (ListItem item in lstUser.Items)
            {
                if (item.Value.ToString() != "0")
                {
                    if (row["UserID"].ToString() == item.Value.ToString())
                    {

                        item.Selected = true;
                        break;

                    }
                }
            }

        }            
    }

    protected void btnRemoveFolderAccess_Click(object sender, EventArgs e)
    {
        RemoveFolderAccess(lstUser);
    }
    public void RemoveFolderAccess(CheckBoxList UserList)
    {
        if (UserList.Items.Count != 0 && UserList.SelectedItem != null)
        {

            if (txtFolderName.Text.Trim().Length > 0)
            {
                int iFolderID = UDFLib.ConvertToInteger(Request.QueryString["id"]);
                //int iFolderID = objQMS.getFileIDByPath(txtFolderName.Text.Trim());

                //objQMS.Delete_User_Folder_Access(iFolderID);
                if (UserList.Items[0].Selected == true)
                {
                    for (int i = 0; i < UserList.Items.Count; i++)
                    {
                        objQMS.Delete_User_Folder_Access(iFolderID, int.Parse(UserList.Items[i].Value));
                    }
                }
                else
                {
                    for (int i = 0; i < UserList.Items.Count; i++)
                    {
                        if (UserList.Items[i].Selected == true)
                        {
                            objQMS.Delete_User_Folder_Access(iFolderID, int.Parse(UserList.Items[i].Value));
                        }
                    }
                }

                //objQMS.Insert_User_Folder_Access(iFolderID, GetSessionUserID(), GetSessionUserID());

                lblMessage.Text = "Folder access changed successfully.";
            }
        }
        else
        {
            lblMessage.Text = "Select user name";
        }
    }
    protected void btnAddFolderAccess_Click(object sender, EventArgs e)
    {
        AddFolderAccess(lstUser);
    }
    public void AddFolderAccess(CheckBoxList UserList)
    {
        if (UserList.Items.Count != 0 && UserList.SelectedItem != null)
        {

            if (txtFolderName.Text.Trim().Length > 0)
            {
                int iFolderID = UDFLib.ConvertToInteger(Request.QueryString["id"]);
                //int iFolderID = objQMS.getFileIDByPath(txtFolderName.Text.Trim());

                //objQMS.Delete_User_Folder_Access(iFolderID);
                if (UserList.Items[0].Selected == true)
                {
                    for (int i = 0; i < UserList.Items.Count; i++)
                    {
                        objQMS.Insert_User_Folder_Access(iFolderID, int.Parse(UserList.Items[i].Value), GetSessionUserID());
                    }
                }
                else
                {
                    for (int i = 0; i < UserList.Items.Count; i++)
                    {
                        if (UserList.Items[i].Selected == true)
                        {
                            objQMS.Insert_User_Folder_Access(iFolderID, int.Parse(UserList.Items[i].Value), GetSessionUserID());
                        }
                    }
                }

                lblMessage.Text = "Folder access changed successfully.";
            }
        }
        else
        {
            lblMessage.Text = "Select user name";
        }
    }
    protected void SearchCatalog()
    {
        string Query = txtSearch.Text;
        try
        {
            BLL_QMS_SearchCatalog QMSSearch = new BLL_QMS_SearchCatalog();
            SearchParameters Param = new SearchParameters();



            var withoutSpecial = new string(txtSearch.Text.Where(c => Char.IsLetterOrDigit(c) 
                                            || Char.IsWhiteSpace(c)).ToArray());
        if (txtSearch.Text.Trim().Length==1)
        {

            string js2 = "alert('Query should not contain a single character.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js2, true);
            return;
        }

        if (txtSearch.Text != withoutSpecial)
        {
            
            string js2 = "alert('Query should not contain any special characters.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js2, true);
            return;
        }


            DataTable dt1 = objQMS.Get_FileName(lblParentFolder.Text, txtSearch.Text);

            Query = ValidateQuery(Query);

            Param.UserID = GetSessionUserID();
            Param.Storage = ConfigurationManager.AppSettings["QMS"].ToString();
            Param.SearchFolder = lblParentFolder.Text;
            Param.Query = Query;


            if (txtToDate.Text.Trim() != "")
                Param.DateModifiedFrom = txtFromDate.Text;
            if (txtToDate.Text.Trim() != "")
                Param.DateModifiedTo = txtToDate.Text;





            DataTable dt = QMSSearch.ExecuteQuery(Param, dt1);

            Session["SearchResult"] = dt;
            gvSearchResult.DataSource = dt;
            gvSearchResult.DataBind();

            dvMessage.Text = Convert.ToString(dt.Rows.Count) + " Documents matched the Query";
        }
        catch (Exception ex)
        {


            string msg = ex.Message;
            msg = msg.Replace("'", "");
            if(msg=="The query contained only ignored words.")
            {
                msg = "Query should not contain or start with special characters/numbers.";
            }
                  
            string js1 = "alert('" + msg.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js1, true);
        }

    }

    private string ValidateQuery(string Query)
    {

        if (Query.IndexOf(" ") > 0)
        {
            Query = '"' + Query + '"';
        }

        if (Query.IndexOf("[") > 0)
            Query = Query.Replace("[", "");

        if (Query.IndexOf("]") > 0)
            Query = Query.Replace("]", "");

        if (Query.IndexOf("'") > 0)
        {
            if (Query.IndexOf("'") - 1 > 0)
            {
                Query = Query.Substring(0, Query.IndexOf("'") - 1);
            }
        }
        return Query;
    }
    
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = Session["Pageurl"].ToString();//UDFLib.GetPageURL(Request.Path.ToUpper());

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
            //pnlUploadDocument.Enabled = false;
            //FileUploader.Enabled = false;
            //btnAddFolderAccess.Enabled = false;
        }
        if (objUA.Edit == 0)
        {
           // pnlUploadDocument.Enabled = false;
            //FileUploader.Enabled = false;

        }
        if (objUA.Delete == 0)
        {

            //btnRemoveFolderAccess.Enabled = false;
        }

        if (objUA.Approve == 0)
        {

        }
        if (objUA.Admin == 1)
        {
            Session["Admin"] = 1;
            btnDeleteE.Visible = true;
        }

        
        Session["ACCESSLEVEL"] = objUA.Admin;

    }

    protected void lstUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string msg = "";
    

        //    for (int i = 1; i < lstUser.Items.Count; i++)
        //    {
        //        if (lstUser.Items[0].Selected == true)
        //        {
        //            lstUser.Items[i].Selected = true;
        //        }
        //        else
        //        {
        //            lstUser.Items[i].Selected = false;
        //        }
        //    }
        string result = Request.Form["__EVENTTARGET"];
        string[] checkedBox = result.Split('$'); ;
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (index==0 && lstUser.Items[index].Selected)
        {
            for (int i = 1; i < lstUser.Items.Count; i++)
            {
                lstUser.Items[i].Selected = true;
            }
        }
        else if (index == 0 && !lstUser.Items[index].Selected)
        {
            for (int i = 1; i < lstUser.Items.Count; i++)
            {
                lstUser.Items[i].Selected = false;
            }
        }
        else if (index != 0 && !lstUser.Items[index].Selected)
        {
            lstUser.Items[0].Selected = false; 
        }
    }

    protected void Load_UserList()
    {
        if (UDFLib.ConvertIntegerToNull(Session["USERCOMPANYID"]) == null)
            Response.Redirect("~/default.aspx?msgid=3");
        else
        {
            BLL_Infra_UserCredentials objBllUser = new BLL_Infra_UserCredentials();
            DataTable dt = objBllUser.Get_UserList(UDFLib.ConvertToInteger(Session["USERCOMPANYID"]),"");
            
            chklstUser.DataSource = dt;
            chklstUser.DataBind();
            chklstUser.Items.Insert(0, new ListItem("- ALL -", "0"));
        }
    }

    public void GetApprovalRequired()
    {
        try
        {
            DataTable dt = objQMS.GET_FolderApproverList(UDFLib.ConvertToInteger(Request.QueryString["id"]));
            BindDropDown(ddlUser1E);
            BindDropDown(ddlUser2E);
            BindDropDown(ddlUser3E);
            if (dt.Rows.Count > 0)
            {
                chkApprovalE.Checked = true;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["Level_ID"].ToString() == "1")
                    {
                        ddlUser1E.Enabled = true;
                        ddlUser2E.Enabled = true;
                        ddlUser1E.SelectedValue = ddlUser1E.Items.FindByValue(row["Approver_ID"].ToString()) != null ? row["Approver_ID"].ToString() : "0";
                    }
                    if (row["Level_ID"].ToString() == "2")
                    {
                        ddlUser2E.Enabled = true;
                        ddlUser3E.Enabled = true;
                        ddlUser2E.SelectedValue = ddlUser2E.Items.FindByValue(row["Approver_ID"].ToString()) != null ? row["Approver_ID"].ToString() : "0";
                    }
                    if (row["Level_ID"].ToString() == "3")
                    {
                        ddlUser3E.Enabled = true;
                        ddlUser3E.SelectedValue = ddlUser3E.Items.FindByValue(row["Approver_ID"].ToString()) != null ? row["Approver_ID"].ToString() : "0";
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void chkApprovalE_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkApprovalE.Checked == true)
        {
            ddlUser1E.Enabled = true;
            if (ddlUser1E.SelectedValue != "0")
            {
                ddlUser2E.Enabled = true;
            }
            else
            {
                ddlUser2E.Enabled = false;
            }
            if (ddlUser2E.SelectedValue != "0")
            {
                ddlUser3E.Enabled = true;
            }
            else
            {
                ddlUser3E.Enabled = false;
            }
        }
        else
        {
            ddlUser1E.Enabled = false;
            ddlUser2E.Enabled = false;
            ddlUser3E.Enabled = false;
        }
    }

    public void BindDropDown(DropDownList ddlUserList)
    {
        BLL_Infra_UserCredentials objBllUser = new BLL_Infra_UserCredentials();
        DataTable dt = objBllUser.Get_UserList(UDFLib.ConvertToInteger(Session["USERCOMPANYID"]), "");

        ddlUserList.DataSource = dt;
        ddlUserList.DataTextField = "USERNAME";
        ddlUserList.DataValueField = "USERID";
        ddlUserList.DataBind();
        ddlUserList.Items.Insert(0, new ListItem("-SELECT-", "0"));

    }

    protected void ddlUser1E_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUser1E.SelectedValue != "0")
        {
            ddlUser2E.Enabled = true;

        }
        else
        {
            ddlUser2E.Enabled = false;
            ddlUser3E.Enabled = false;
        }
    }

    protected void ddlUser2E_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUser2E.SelectedValue != "0")
        {
            ddlUser3E.Enabled = true;

        }
        else
        {
            ddlUser3E.Enabled = false;
        }
    }

    protected void btnAddFolderAccessE_Click(object sender, EventArgs e)
    {
        AddFolderAccess(chklstUser);
        ScriptManager1.RegisterDataItem(lblMessage, lblMessage.Text);

    }

    protected void btnRemoveFolderAccessE_Click(object sender, EventArgs e)
    {
        RemoveFolderAccess(chklstUser);
        ScriptManager1.RegisterDataItem(lblMessage, lblMessage.Text);

    }

    protected void btnSaveE_Click(object sender, EventArgs e)
    {
        try
        {

            string strMsg;
            if (txtParentFolderE.Text.Trim() != "")
            {
                if (txtFolderNameE.Text.Trim() != "")
                {
                    int FolderID;
                    int iUserCount = 0;
                    Regex regex;
                    //string Filter;
                    regex = new Regex(@"^[a-zA-Z0-9-' '_.]*$");
                    if ((ddlUser1E.SelectedValue != "0" && chkApprovalE.Checked == true) || chkApprovalE.Checked == false)
                    {
                        if ((ddlUser1E.SelectedValue != ddlUser2E.SelectedValue && ddlUser1E.SelectedValue != ddlUser3E.SelectedValue && (ddlUser2E.SelectedValue != ddlUser3E.SelectedValue || ddlUser2E.SelectedValue == "0")) || chkApprovalE.Checked == false)
                        {
                            if (regex.IsMatch(txtFolderNameE.Text.Trim()))
                            {
                                string NewFolderMapPath = Server.MapPath(txtParentFolderE.Text + txtFolderNameE.Text.Trim());

                                bool folderExists = Directory.Exists(NewFolderMapPath);

                                if (!folderExists || Convert.ToString(Request.QueryString["Path"]) == txtParentFolderE.Text + txtFolderNameE.Text.Trim() + "/")
                                {
                                    if (chklstUser.Items.Count > 0)
                                    {

                                        for (int i = 0; i < chklstUser.Items.Count; i++)
                                        {
                                            if (chklstUser.Items[i].Selected == true)
                                            {
                                                iUserCount += 1;
                                                break;
                                            }

                                        }

                                        if (iUserCount > 0)
                                        {
                                            if (Convert.ToString(Request.QueryString["Path"]) != txtParentFolderE.Text + txtFolderNameE.Text.Trim() + "/")
                                                RenameFolder(Convert.ToString(Request.QueryString["Path"]).Remove(Convert.ToString(Request.QueryString["Path"]).Length - 1, 1), txtFolderNameE.Text.Trim(), UDFLib.ConvertToInteger(Convert.ToString(Request.QueryString["id"])));/* This function is used to rename the folder . Folder ID Parameter is added by Someshwar on 08-07-2016 */

                                            FolderID = UDFLib.ConvertToInteger(Convert.ToString(Request.QueryString["id"]));

                                            DataTable dt = objQMS.GET_FolderUser(UDFLib.ConvertToInteger(Request.QueryString["id"]));
                                            bool iSelected = false;
                                            for (int i = 1; i < chklstUser.Items.Count; i++)
                                            {
                                                foreach (DataRow row in dt.Rows)
                                                {
                                                    if (row["UserID"].ToString() == chklstUser.Items[i].Value.ToString())
                                                    {
                                                        if (chklstUser.Items[i].Selected == false)
                                                            objQMS.Delete_User_Folder_Access(UDFLib.ConvertToInteger(Request.QueryString["id"]), UDFLib.ConvertToInteger(row["UserID"]));
                                                        else
                                                            iSelected = false;
                                                        break;
                                                    }
                                                    else if (chklstUser.Items[i].Selected == true)
                                                        iSelected = true;
                                                }

                                                if (iSelected == true)
                                                    objQMS.Insert_User_Folder_Access(UDFLib.ConvertToInteger(Request.QueryString["id"]), UDFLib.ConvertToInteger(chklstUser.Items[i].Value.ToString()), GetSessionUserID());

                                                iSelected = false;
                                            }


                                            DataTable dtApproval_Level = new DataTable();
                                            DataColumn dtLevelID = new DataColumn("Level_ID");
                                            DataColumn dtApproverID = new DataColumn("Approver_ID");
                                            dtApproval_Level.Columns.Add(dtLevelID);
                                            dtApproval_Level.Columns.Add(dtApproverID);
                                            for (int i = 1; i < 4; i++)
                                            {
                                                string Level = "ddlUser";
                                                Level = Level + i + "E";
                                                DropDownList Leveli = (DropDownList)pnlDeptFolder.FindControl(Level);
                                                if (Leveli.SelectedValue != "0" && Leveli.Enabled == true)
                                                {
                                                    DataRow dr = dtApproval_Level.NewRow();
                                                    dr["Level_ID"] = i;
                                                    dr["Approver_ID"] = Convert.ToInt32(Leveli.SelectedValue);
                                                    dtApproval_Level.Rows.Add(dr);
                                                }
                                                else
                                                {
                                                    DataRow dr = dtApproval_Level.NewRow();
                                                    dr["Level_ID"] = i;
                                                    dr["Approver_ID"] = 0;
                                                    dtApproval_Level.Rows.Add(dr);
                                                }
                                            }
                                            objQMS.UpdateDMSApprovarList(FolderID, dtApproval_Level, GetSessionUserID());


                                            strMsg = "Folder Updated successfully.";
                                            ScriptManager1.RegisterDataItem(lblMessage, strMsg);


                                        }
                                        else
                                        {
                                            strMsg = "Folder access must be given to atleast one user.";
                                            ScriptManager1.RegisterDataItem(lblMessage, strMsg);

                                        }
                                    }
                                    else
                                    {
                                        strMsg = "Folder access must be given to atleast one user.";
                                        ScriptManager1.RegisterDataItem(lblMessage, strMsg);

                                    }
                                }
                                else
                                {
                                    strMsg = "Folder with same name already exists !!.";
                                    ScriptManager1.RegisterDataItem(lblMessage, strMsg);

                                }
                            }
                            else
                            {
                                strMsg = "This is not a valid folder name,.it should  be alphanumeric, underscore and whitespace.";
                                ScriptManager1.RegisterDataItem(lblMessage, strMsg);

                            }
                        }
                        else
                        {
                            if (ddlLevel1.SelectedValue == ddlLevel2.SelectedValue)
                            {
                                ddlLevel2.SelectedValue = "0";
                                ddlLevel3.Enabled = false;
                                ddlLevel3.SelectedValue = "0";
                            }
                            else
                            {
                                ddlLevel3.SelectedValue = "0";
                            }

                            strMsg = "User is already selected! please select different user.";
                            ScriptManager1.RegisterDataItem(lblMessage, strMsg);

                        }
                    }
                    else
                    {
                        strMsg = "Please select a user to approve!";
                        ScriptManager1.RegisterDataItem(lblMessage, strMsg);

                    }
                }
                else
                {
                    strMsg = "Please enter folder name.";
                    ScriptManager1.RegisterDataItem(lblMessage, strMsg);
                }
            }
            else
            {
                strMsg = "Please Select Parent folder.";
                ScriptManager1.RegisterDataItem(lblMessage, strMsg);
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        
   
            string jsErr = "alert('" + msg.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgErr", jsErr, true);
        }
    }

    protected void btnDeleteE_Click(object sender, EventArgs e)
    {
        objQMS.Delete_DMSFile_Folder(UDFLib.ConvertToInteger(Request.QueryString["id"]), GetSessionUserID());
        string js1 = "alert('Folder deleted Successfully!');parent.ChildCallBackDelete();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", js1, true);
    }
    /// <summary>
    /// Depending on department only office user should be displayed
    /// </summary>
    protected void chklstDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL_Infra_UserCredentials objInfra = new BLL_Infra_UserCredentials();
        string strDept = "";
        string result = Request.Form["__EVENTTARGET"];
        string[] checkedBox = result.Split('$'); ;
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (index == 0 )
        {
            if (chklstDept.Items[index].Selected)
            {
                int UserCompanyID = int.Parse(getSessionString("USERCOMPANYID"));
                DataTable dtUsers = objInfra.Get_UserList(UserCompanyID,"");
                chklstUser.DataSource = dtUsers;
                chklstUser.DataBind();
                chklstUser.Items.Insert(0, new ListItem("- ALL -", "0"));
                for (int i = 1; i < chklstDept.Items.Count; i++)
                {
                    chklstDept.Items[i].Selected = true;
                }
                for (int i = 0; i < chklstUser.Items.Count; i++)
                {
                    chklstUser.Items[i].Selected = true;
                }
            }
            else
            {
                for (int i = 1; i < chklstDept.Items.Count; i++)
                {
                    chklstDept.Items[i].Selected = false;
                }
                for (int i = 0; i < chklstUser.Items.Count; i++)
                {
                    chklstUser.Items[i].Selected = false;
                }
            }
        }
        else if (index != 0 )
        {
            chklstDept.Items[0].Selected = false;
            foreach (ListItem li in chklstDept.Items)
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
                chklstUser.DataSource = dtUsers;
                chklstUser.DataBind();
                chklstUser.Items.Insert(0, new ListItem("- ALL -", "0"));

                DataTable dt = objQMS.GET_FolderUser(UDFLib.ConvertToInteger(Request.QueryString["id"]));
                foreach (DataRow row in dt.Rows)
                {
                    foreach (ListItem item in chklstUser.Items)
                    {
                        if (row["UserID"].ToString() == item.Value.ToString())
                        {

                            item.Selected = true;
                            break;

                        }
                    }

                }
            }
        }

    }

    protected void chklstUser_SelectedIndexChanged(object sender, EventArgs e)
    {    
        string result = Request.Form["__EVENTTARGET"];
        string[] checkedBox = result.Split('$'); ;
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (index==0 && chklstUser.Items[index].Selected)
        {
            for (int i = 1; i < chklstUser.Items.Count; i++)
            {
                chklstUser.Items[i].Selected = true;
            }
        }
        else if (index == 0 && !chklstUser.Items[index].Selected)
        {
            for (int i = 1; i < chklstUser.Items.Count; i++)
            {
                chklstUser.Items[i].Selected = false;
            }
        }
        else if (index != 0 && !chklstUser.Items[index].Selected)
        {
            chklstUser.Items[0].Selected = false; 
        }
    }
}

