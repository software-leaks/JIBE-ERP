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
using SMS.Business.FMS;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.JRA;
using System.Collections.Generic;
using AjaxControlToolkit4;

public partial class AddNewFile : System.Web.UI.Page
{
    BLL_FMS_Document objFMS = new BLL_FMS_Document();
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
            Load_DepartmentList();
            Load_UserList();
            string strName = FolderPath.Remove(FolderPath.Length - 1, 1);
            txtParentFolderE.Text = strName.Substring(0, strName.LastIndexOf('/') + 1);
            txtFolderNameE.Text = strName.Substring(strName.LastIndexOf('/') + 1);
            GetFolderAccess(chklstUser);
            GetApprovalRequired();
            BindDepartment();
            BindFormType();
            GetScheduleHistory();
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
            //int fileID = objFMS.getFileIDByPath(FolderPath + FileName);

            int fileID = objFMS.checkFileExits(FileName, txtFolderName.Text);

            string oldfileName = "";
            DataSet dsFileDetails = objFMS.getFileDetailsByID(fileID, 0);

            if (dsFileDetails.Tables[0].Rows.Count > 0)
            {
                oldfileName = System.IO.Path.GetFileName(Convert.ToString(dsFileDetails.Tables[0].Rows[0]["FilePath"]));

                string sFileExt = System.IO.Path.GetExtension(FileName);
                string sGuidFileName = "FMSL_" + System.Guid.NewGuid() + sFileExt;

                File.Copy(SourcePath, DestinationPath + "\\" + sGuidFileName, true);

                objFMS.UpdateVersionInfoOfNewFileAdd(fileID, sGuidFileName, Convert.ToInt32(Session["USERID"]), txtRemarks.Text.Trim());

                txtRemarks.Text = "";
                Response.Redirect("FMSFileLoader.aspx?DocID=" + fileID);

            }

        }

        UserAccessValidation();
    }

    public void BindDepartment()
    {
        DataTable dt = objCompBLL.Get_CompanyDepartmentList(UDFLib.ConvertToInteger(Session["APPCOMPANYID"].ToString()));
        ddlDepartments.DataSource = dt;
        ddlDepartments.DataTextField = "VALUE";
        ddlDepartments.DataValueField = "ID";
        ddlDepartments.DataBind();
        ddlDepartments.Items.Insert(0, new ListItem("-- Select --", null));
    }
    public void BindFormType()
    {
        DataSet ds = objFMS.FMS_Get_FormType();
        ddlFormType.DataSource = ds.Tables[0];
        ddlFormType.DataTextField = "FormType";
        ddlFormType.DataValueField = "ID";
        ddlFormType.DataBind();
        ddlFormType.Items.Insert(0, new ListItem("-- Select --", null));
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Convert.ToString(Session["USERID"]));
        else
            return 0;
    }

    public void GetScheduleHistory()
    {
        ucCustomPagerSch.Visible = true;
        int rowcount = ucCustomPagerSch.isCountRecord;


        int ParentFolderID = UDFLib.ConvertToInteger(Request.QueryString["id"]);

        DataSet ds = new DataSet();
        ds = objFMS.FMS_Get_DocScheduleListByFolder(ParentFolderID, ucCustomPagerSch.CurrentPageIndex, ucCustomPagerSch.PageSize, ref rowcount);


        if (ucCustomPagerSch.isCountRecord == 1)
        {

            ucCustomPagerSch.CountTotalRec = rowcount.ToString();
            ucCustomPagerSch.BuildPager();
        }
        gvScheduleHistory.DataSource = ds.Tables[0];
        gvScheduleHistory.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        lblMessage.Text = "";
        string ParentFolderPath = txtFolderName.Text;
        string ParentFolderMapPath = Server.MapPath("../Uploads/FMSL/");
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
                        dt = objUploadFilesize.Get_Module_FileUpload("FMS_");

                        if (dt.Rows.Count > 0)
                        {
                            string datasize = dt.Rows[0]["Size_KB"].ToString();
                            if (FileUploader.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                            {

                                string filepath = FileUploader.PostedFile.FileName;
                                string sFileName = System.IO.Path.GetFileName(FileUploader.PostedFile.FileName);
                                ViewState["FileName"] = sFileName;
                                string sFileExt = System.IO.Path.GetExtension(sFileName);
                                string sGuidFileName = "FMSL_" + System.Guid.NewGuid() + sFileExt;
                                int fileID = objFMS.checkFileExits(sFileName, ParentFolderPath);

                                //int FileExistCount = objFMS.getFileCountByFileID(fileID);
                                if (fileID == 0)
                                {
                                    if (ViewState["Cat_Table"] == null)
                                    {
                                        DataTable dtemt = new DataTable();
                                        dtemt.Columns.Add("ID");
                                        dtemt.Columns.Add("Work_Categ_ID");
                                        dtemt.Columns.Add("Work_Category_Name");
                                        ViewState["Cat_Table"] = dtemt;
                                    }
                                    FileUploader.PostedFile.SaveAs(ParentFolderMapPath + sGuidFileName);

                                    int FileID = objFMS.Add_NewDocument(ParentFolderID, sFileName, "Uploads/FMSL/" + sGuidFileName, Convert.ToInt32(Session["USERID"]), txtRemarks.Text.Trim(), UDFLib.ConvertToInteger(ddlFormType.SelectedValue), UDFLib.ConvertToInteger(ddlDepartments.SelectedValue), txtFormat.Text, (DataTable)ViewState["Cat_Table"]);
                                    txtRemarks.Text = "";

                                    //For File Approval 
                                    DataTable dtApproval_Level = objFMS.Check_FolderApprovalExists(ParentFolderID);
                                    if (dtApproval_Level.Rows.Count > 0)
                                    {
                                        string iFolderName = tempPath[tempPath.Length - 2];
                                        objFMS.CreateNewFileApproval(FileID, sFileName, ParentFolderID, iFolderName, dtApproval_Level, Convert.ToInt32(Session["USERID"]), Convert.ToInt32(Session["USERCOMPANYID"]));
                                    }
                                    if (FileID > 0)
                                    {
                                        ViewState["Cat_Table"] = null;
                                        DataTable dtnull = new DataTable();
                                        dlRAForms.DataSource = dtnull;
                                        dlRAForms.DataBind();
                                        UpdatePanel6.Update();
                                        lblMessage.Text = "File added successfully.";
                                        //string jsFile = "alert('File added successfully.');parent.ChildCallBack();";
                                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", jsFile, true);

                                    }

                                }
                                else
                                {
                                    DataSet dsOperationInfo = objFMS.getCheckedFileInfo(fileID);
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
                                            //string js = "alert('" + Message + "')";
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "msgz", js, true);
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
                            //string js2 = "alert('Upload size not set!');";
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                            lblMessage.Text = "Upload size not set!";

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
                btnAddFolderAccess.Visible = false;
                btnRemoveFolderAccess.Visible = false;
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
                                string NewFolderMapPath = Server.MapPath("../Uploads/FMSL");


                                string TempPath = ParentFolderPath + txtNewFolderName.Text.Trim();

                                DataSet dsTemp = objFMS.FMS_GET_FORM_TREE(txtNewFolderName.Text.Trim());
                                string tempurl = "";
                                DataRow dr1 = dsTemp.Tables[0].NewRow();
                                string expression = "vURL='" + TempPath + "'";
                                DataRow[] dr2;
                                dr2 = dsTemp.Tables[0].Select(expression);
                                bool folderExists = false;
                                if (dr2.Length > 0)
                                {

                                    folderExists = true;


                                }
                                //if (objFMS.FMS_Get_FolderExist(txtNewFolderName.Text.Trim(), "Uploads/FMSL" + ParentFolderPath) == 1)
                                //{
                                //    folderExists = true;
                                //}
                                //else
                                //{
                                //    folderExists = false;
                                //}


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
                                            //System.IO.Directory.CreateDirectory(NewFolderMapPath);
                                            ParentFolderID = UDFLib.ConvertToInteger(Convert.ToString(Request.QueryString["id"]));

                                            DataTable dtUsers = GetFolderAccessUserList();

                                            //int iFolderID = objFMS.insertRecordAtFolderCreation(txtNewFolderName.Text.Trim(), ParentFolderName, ParentFolderPath + txtNewFolderName.Text.Trim(), NodeType);
                                            int iFolderID = objFMS.CreateNewFolder(txtNewFolderName.Text.Trim(), ParentFolderID, dtUsers, GetSessionUserID(), ParentFolderPath + txtNewFolderName.Text.Trim());

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
                                                objFMS.CreateNewFolderApproval(iFolderID, dtApproval_Level, GetSessionUserID());
                                            }


                                            if (iFolderID > 0)
                                            {
                                                lblMessage.Text = "Folder created successfully.";
                                                //string js = "alert('Folder created successfully.');parent.ChildCallBack();";
                                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                                            }

                                        }
                                        else
                                        {
                                            lblMessage.Text = "Folder access must be given to atleast one user.";
                                            //string js2 = "alert('Folder access must be given to atleast one user.');";
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg2", js2, true);
                                        }
                                    }
                                    else
                                    {
                                        lblMessage.Text = "Folder access must be given to atleast one user.";
                                        //string js3 = "alert('Folder access must be given to atleast one user.');";
                                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg3", js3, true);
                                    }
                                }
                                else
                                {
                                    lblMessage.Text = "Folder with same name already exists !!.";
                                    //string js4 = "alert('Folder with same name already exists !!.');";
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg4", js4, true);
                                }
                            }
                            else
                            {
                                lblMessage.Text = "Folder name is invalid.Please use only alphanumeric,underscore for the folder name.";
                                //string js5 = "alert('This is not a valid folder name,.it should  be alphanumeric, underscore and whitespace.')";
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg5", js5, true);
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
                            //string js6 = "alert('User is already selected! please select different user.')";
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg6", js6, true);
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Please select a user to approve!";
                        //string js7 = "alert('Please select a user to approve!')";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg7", js7, true);
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
            UDFLib.WriteExceptionLog(ex);
            string Error10 = "alert('" + ex.Message.Replace("'", "") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error10", Error10, true);
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

    private void RenameFolder(string oldFolderPath, string newFolderName)
    {
        bool folderExists = Directory.Exists(Server.MapPath("../Uploads/" + oldFolderPath));
        if (folderExists)
        {
            string newFolderPath = oldFolderPath.Substring(0, oldFolderPath.LastIndexOf('/')) + "/" + newFolderName;

            System.IO.Directory.Move(Server.MapPath("../Uploads/" + oldFolderPath), Server.MapPath("../Uploads/" + newFolderPath));
            //System.IO.Directory.Delete(Server.MapPath(oldFolderPath));
            objFMS.RenameFolder(oldFolderPath, newFolderName);
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
            lblMessage.Text = ex.Message.Replace("'", "");
            //js = "alert('" + ex.Message.Replace("'", "") + "');";
            //Page.ClientScript.RegisterStartupScript(GetType(), "script", js, true);
        }

    }

    protected void rdoDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {

        changeControlStatus();
    }

    protected void chkReqApproval_OnCheckedChanged(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
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

            //if (Convert.ToString(Session["ACCESSLEVEL"]) == "1")
            //{
            //    btnAddFolderAccess.Visible = true;
            //    btnRemoveFolderAccess.Visible = true;
            //}
            //else
            //{
            //    btnAddFolderAccess.Visible = false;
            //    btnRemoveFolderAccess.Visible = false;
            //}

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
        DataTable dt = objFMS.GET_FolderUser(UDFLib.ConvertToInteger(Request.QueryString["id"]));
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
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string filepath = ((Label)e.Row.FindControl("lblFileName")).Text;
                ((ImageButton)e.Row.Controls[5].FindControl("ImgOpenExt")).ImageUrl = getNodeImageURL(filepath);

                string strRowId = DataBinder.Eval(e.Row.DataItem, "FileID").ToString();
                ImageButton ImgPreview = (ImageButton)(e.Row.FindControl("ImgOpenForView"));
                if (ImgPreview != null)
                    ImgPreview.Attributes.Add("onclick", "javascript:window.location.href='FMSFileLoader.aspx?DocID=" + strRowId + "'");

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
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
        try
        {
            BLL_FMS_Document objFMS = new BLL_FMS_Document();

            int fileID = 0;
            string sPath = Convert.ToString(Args.CommandName);

            fileID = getFileIDByDocPath(sPath);
            int MaxVersionByFileID = objFMS.getMaxVersionFromParentTable(fileID);
            string msg = String.Format("window.location.href='FMSFileLoader.aspx?docid=" + fileID + "';");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void OpenFileExternal(object sendepr, CommandEventArgs Args)
    {
        string sPath = Convert.ToString(Args.CommandName);
        //OpenFileExternal(sPath);
        sPath = "../Uploads/FMS/" + sPath;
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
        BLL_FMS_Document objFMS = new BLL_FMS_Document();

        string[] arrPath = IndexingPath.Split('\\');
        DataSet dsFileInfo = objFMS.getFileIDByDocInfo(arrPath[arrPath.Length - 1].ToString());
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
        try
        {
            ViewState["sortDirectionTEXT"] = System.Web.UI.WebControls.SortDirection.Ascending;
            SearchCatalog();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Used to clear filters.
    /// </summary>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        pnlAdvSearch.Visible = false;
        pnlViewSearchResult.Visible = false;   // Result panel make visible false on clear filter.     
        dvMessage.Text = "";

    }

    protected void ImgBtnAdvSearch_Click(object sender, ImageClickEventArgs e)
    {
        pnlAdvSearch.Visible = true;
        //txtSearch.Text = "";
        //lblParentFolder.Text = "DOCUMENTS";
    }




    public void BindUserList()
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
                DataTable dtUsers = objInfra.Get_UserList(UserCompanyID, "");
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
        DataTable dt = objFMS.GET_FolderUser(UDFLib.ConvertToInteger(Request.QueryString["id"]));
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
        try
        {
            RemoveFolderAccess(lstUser);
            BindUserList();
            GetFolderAccess(lstUser);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public void RemoveFolderAccess(CheckBoxList UserList)
    {
        if (UserList.Items.Count != 0 && UserList.SelectedItem != null)
        {

            if (txtFolderName.Text.Trim().Length > 0)
            {
                int iFolderID = UDFLib.ConvertToInteger(Request.QueryString["id"]);
                //int iFolderID = objFMS.getFileIDByPath(txtFolderName.Text.Trim());

                //objFMS.Delete_User_Folder_Access(iFolderID);
                if (UserList.Items[0].Selected == true)
                {
                    for (int i = 0; i < UserList.Items.Count; i++)
                    {
                        objFMS.Delete_User_Folder_Access(iFolderID, int.Parse(UserList.Items[i].Value));
                    }
                }
                else
                {
                    for (int i = 0; i < UserList.Items.Count; i++)
                    {
                        if (UserList.Items[i].Selected == true)
                        {
                            objFMS.Delete_User_Folder_Access(iFolderID, int.Parse(UserList.Items[i].Value));
                        }
                    }
                }

                //objFMS.Insert_User_Folder_Access(iFolderID, GetSessionUserID(), GetSessionUserID());

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
        try
        {
            AddFolderAccess(lstUser);
            BindUserList();
            GetFolderAccess(lstUser);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public void AddFolderAccess(CheckBoxList UserList)
    {
        if (UserList.Items.Count != 0 && UserList.SelectedItem != null)
        {

            if (txtFolderName.Text.Trim().Length > 0)
            {
                int iFolderID = UDFLib.ConvertToInteger(Request.QueryString["id"]);
                //int iFolderID = objFMS.getFileIDByPath(txtFolderName.Text.Trim());

                //objFMS.Delete_User_Folder_Access(iFolderID);
                if (UserList.Items[0].Selected == true)
                {
                    for (int i = 0; i < UserList.Items.Count; i++)
                    {
                        objFMS.Insert_User_Folder_Access(iFolderID, int.Parse(UserList.Items[i].Value), GetSessionUserID());
                    }
                }
                else
                {
                    for (int i = 0; i < UserList.Items.Count; i++)
                    {
                        if (UserList.Items[i].Selected == true)
                        {
                            objFMS.Insert_User_Folder_Access(iFolderID, int.Parse(UserList.Items[i].Value), GetSessionUserID());
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
    /// <summary>
    /// Display file details, which is inside a selected folder.
    /// </summary>
    protected void SearchCatalog()
    {
        string Query = txtSearch.Text;
     

        try
        {            
            BLL_FMS_SearchCatalog FMSSearch = new BLL_FMS_SearchCatalog();
            FMSSearchParameters Param = new FMSSearchParameters();
            string[] ParentFolder = lblParentFolder.Text.Split('/');
            DataTable dt1 = objFMS.Get_FileName(lblParentFolder.Text, txtSearch.Text);

            Query = ValidateQuery(Query);

            Param.FMSUserID = GetSessionUserID();
            Param.FMSStorage = ConfigurationManager.AppSettings["FMS"].ToString();
            Param.FMSSearchFolder = ParentFolder[0].ToString(); //lblParentFolder.Text;
            Param.FMSQuery = Query;

            if (txtFromDate.Text.Trim() != "")
            {
                Param.FMSDateModifiedFrom = txtFromDate.Text;
            }
            else
            {
                Param.FMSDateModifiedFrom = "01/01/1990";
            }
            if (txtToDate.Text.Trim() != "")
            {
                Param.FMSDateModifiedTo = txtToDate.Text;
            }
            else
            {
                Param.FMSDateModifiedTo = DateTime.Now.ToShortDateString();
            }
            

            DataTable dt = FMSSearch.ExecuteQuery(Param, dt1);

            DataTable dtNew = dt.Clone();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    if (Path.GetFileName(dt.Rows[j]["FilePath"].ToString()).Split('.')[0].ToString() == dt1.Rows[i]["FileID"].ToString().ToLower())
                    {
                        dtNew.ImportRow(dt.Rows[j]);
                    }

                }

            }
            Session["SearchResult"] = dtNew;

            if (dtNew.Rows.Count > 0)
            {
                pnlViewSearchResult.Visible = true;        // Result panel make visible true when it has data.     
                gvSearchResult.DataSource = dtNew;
                gvSearchResult.DataBind();

                dvMessage.Text = Convert.ToString(dtNew.Rows.Count) + " Documents matched the Query";
            }
            else
            {
                gvSearchResult.DataSource = null;
                gvSearchResult.DataBind();
                dvMessage.Text = Convert.ToString(dtNew.Rows.Count) + " Documents matched the Query";
               
            }
        }
        catch (Exception ex)
        {
            //lblMessage.Text = ex.Message.Replace("'", "");
            UDFLib.WriteExceptionLog(ex);
            string js1 = "alert('" + ex.Message.Replace("'", "") + "');";
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
    /// <summary>
    /// To check access of logged in user.
    /// e.g if user have only VIEW access user not able to add / update /delete.
    /// </summary>
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

        // If user have ADD access then user can add files / folders.
        if (objUA.Add == 0)
        {
            AddNewFile1.Visible = false;
            fragment1.Visible = false;
        }

        // If user have EDIT access then user can edit files / folders.
        if (objUA.Edit == 0)
        {
            EditNewFile.Visible = false;
            fragment2.Visible = false;
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
        string result = Request.Form["__EVENTTARGET"];
        string[] checkedBox = result.Split('$'); ;
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (index == 0 && lstUser.Items[index].Selected)
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
        int CompanyID = 0;
        if (UDFLib.ConvertIntegerToNull(Session["USERCOMPANYID"]) != null)
        {
            CompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"]);
        }
        BLL_Infra_UserCredentials objBllUser = new BLL_Infra_UserCredentials();
        DataTable dt = objBllUser.Get_UserList(CompanyID, "");
        chklstUser.DataSource = dt;
        chklstUser.DataBind();
        chklstUser.Items.Insert(0, new ListItem("- ALL -", "0"));
    }

    public void GetApprovalRequired()
    {
        try
        {
            DataTable dt = objFMS.GET_FolderApproverList(UDFLib.ConvertToInteger(Request.QueryString["id"]));
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
        try
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

                ddlUser1E.SelectedValue = "0";
                ddlUser2E.SelectedValue = "0";
                ddlUser3E.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
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
        try
        {
            if (ddlUser1E.SelectedValue != "0")
            {
                ddlUser2E.Enabled = true;

            }
            else
            {
                ddlUser2E.Enabled = false;
                ddlUser3E.Enabled = false;
                ddlUser2E.SelectedValue = "0";
                ddlUser3E.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ddlUser2E_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlUser2E.SelectedValue != "0")
            {
                ddlUser3E.Enabled = true;
            }
            else
            {
                ddlUser3E.Enabled = false;
                ddlUser3E.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnAddFolderAccessE_Click(object sender, EventArgs e)
    {
        try
        {
            AddFolderAccess(chklstUser);
            GetFolderAccess(chklstUser);
            ScriptManager1.RegisterDataItem(lblMessage, lblMessage.Text);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void btnRemoveFolderAccessE_Click(object sender, EventArgs e)
    {
        try
        {
            RemoveFolderAccess(chklstUser);
            Load_UserList();
            GetFolderAccess(chklstUser);
            ScriptManager1.RegisterDataItem(lblMessage, lblMessage.Text);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

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
                                string NewFolderMapPath = Server.MapPath("../Uploads/FMSL/");

                                // bool folderExists = Directory.Exists(NewFolderMapPath);

                                string TempPath = txtParentFolderE.Text + txtFolderNameE.Text.Trim();

                                DataSet dsTemp = objFMS.FMS_GET_FORM_TREE(txtNewFolderName.Text.Trim());
                                string tempurl = "";
                                DataRow dr1 = dsTemp.Tables[0].NewRow();
                                string expression = "vURL='" + TempPath + "'";
                                DataRow[] dr2;
                                dr2 = dsTemp.Tables[0].Select(expression);
                                bool folderExists = false;
                                if (dr2.Length > 0)
                                {

                                    folderExists = true;


                                }

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
                                                RenameFolder(Convert.ToString(Request.QueryString["Path"]).Remove(Convert.ToString(Request.QueryString["Path"]).Length - 1, 1), txtFolderNameE.Text.Trim());

                                            FolderID = UDFLib.ConvertToInteger(Convert.ToString(Request.QueryString["id"]));

                                            objFMS.FMS_Update_Folder(FolderID, "Uploads/" + txtFolderNameE.Text.Trim(), txtFolderNameE.Text.Trim(), txtParentFolderE.Text + txtFolderNameE.Text.Trim());

                                            DataTable dt = objFMS.GET_FolderUser(UDFLib.ConvertToInteger(Request.QueryString["id"]));
                                            bool iSelected = false;
                                            for (int i = 1; i < chklstUser.Items.Count; i++)
                                            {
                                                if (dt.Rows.Count > 0)
                                                {
                                                    foreach (DataRow row in dt.Rows)
                                                    {
                                                        if (row["UserID"].ToString() == chklstUser.Items[i].Value.ToString())
                                                        {
                                                            if (chklstUser.Items[i].Selected == false)
                                                                objFMS.Delete_User_Folder_Access(UDFLib.ConvertToInteger(Request.QueryString["id"]), UDFLib.ConvertToInteger(row["UserID"]));
                                                            else
                                                                iSelected = false;
                                                            break;
                                                        }
                                                        else if (chklstUser.Items[i].Selected == true)
                                                            iSelected = true;
                                                    }
                                                }
                                                else
                                                {
                                                    if (chklstUser.Items[i].Selected == true)
                                                        iSelected = true;
                                                }

                                                if (iSelected == true)
                                                    objFMS.Insert_User_Folder_Access(UDFLib.ConvertToInteger(Request.QueryString["id"]), UDFLib.ConvertToInteger(chklstUser.Items[i].Value.ToString()), GetSessionUserID());

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
                                            objFMS.UpdateDMSApprovarList(FolderID, dtApproval_Level, GetSessionUserID());


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
                                strMsg = "Folder name is invalid.Please use only alphanumeric,underscore for the folder name.";
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
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnDeleteE_Click(object sender, EventArgs e)
    {
        objFMS.Delete_DMSFile_Folder(UDFLib.ConvertToInteger(Request.QueryString["id"]), GetSessionUserID());
        string js1 = "alert('Folder deleted Successfully!');parent.ChildCallBackDelete();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", js1, true);
    }

    /// <summary>
    /// Depending on department only office user should be displayed. for Add new folder.
    /// </summary>
    protected void lstDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetUsersOnDepartmentSelection("folder");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

        //string strDept = "";
        //BLL_Infra_UserCredentials objInfra = new BLL_Infra_UserCredentials();
        //string result = Request.Form["__EVENTTARGET"];
        //string[] checkedBox = result.Split('$'); ;
        //int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        //if (index == 0)
        //{
        //    if (lstDept.Items[0].Selected == true)
        //    {
        //        int UserCompanyID = int.Parse(getSessionString("USERCOMPANYID"));
        //        DataTable dtUsers = objInfra.Get_UserList(UserCompanyID, "");
        //        lstUser.DataSource = dtUsers;
        //        lstUser.DataBind();
        //        lstUser.Items.Insert(0, new ListItem("- ALL -", "0"));
        //        for (int i = 1; i < chklstDept.Items.Count; i++)
        //        {
        //            lstDept.Items[i].Selected = true;
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 1; i < lstDept.Items.Count; i++)
        //        {
        //            lstDept.Items[i].Selected = false;
        //        }
        //    }
        //}
        //else
        //{
        //    foreach (ListItem li in lstDept.Items)
        //    {
        //        if (li.Selected == true)
        //        {
        //            if (strDept.Length > 0) strDept += ",";
        //            strDept += li.Value;
        //        }
        //    }

        //    if (strDept.Length > 0)
        //    {
        //        int UserCompanyID = int.Parse(getSessionString("USERCOMPANYID"));
        //        DataTable dtUsers = objInfra.Get_UserList_By_Dept_DL(UserCompanyID, strDept);
        //        lstUser.DataSource = dtUsers;
        //        lstUser.DataBind();
        //        lstUser.Items.Insert(0, new ListItem("- ALL -", "0"));

        //    }

        //}
        //DataTable dt = objFMS.GET_FolderUser(UDFLib.ConvertToInteger(Request.QueryString["id"]));
        //foreach (DataRow row in dt.Rows)
        //{
        //    foreach (ListItem item in lstUser.Items)
        //    {
        //        if (item.Value.ToString() != "0")
        //        {
        //            if (row["UserID"].ToString() == item.Value.ToString())
        //            {

        //                item.Selected = true;
        //                break;

        //            }
        //        }
        //    }

        //}
    }

    /// <summary>
    /// Department Slection event for edit file/
    /// </summary>
    protected void chklstDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetUsersOnDepartmentSelection("File");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Author : Anjali
    /// Created on : 08-08-2016
    /// for selected department , users in selected department displays.
    /// </summary>
    private void GetUsersOnDepartmentSelection(string _value)
    {
        BLL_Infra_UserCredentials objInfra = new BLL_Infra_UserCredentials();
        string result = Request.Form["__EVENTTARGET"];
        string[] checkedBox = result.Split('$'); ;
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);
        CheckBoxList _departmentCheckListBox;
        CheckBoxList _UserCheckListBox;

        DataTable _dtDepartmentTable = new DataTable();
        if (_value == "File")
        {
            _departmentCheckListBox = chklstDept;
            _UserCheckListBox = chklstUser;
        }
        else
        {
            _departmentCheckListBox = lstDept;
            _UserCheckListBox = lstUser;
        }

        if (index == 0)
        {
            // User selects 'ALL' as department
            //if (chklstDept.Items[index].Selected)
            if (_departmentCheckListBox.Items[index].Selected)
            {
                int UserCompanyID = int.Parse(getSessionString("USERCOMPANYID"));
                DataTable dtUsers = objInfra.Get_UserList(UserCompanyID, "");
                _UserCheckListBox.DataSource = dtUsers;
                _UserCheckListBox.DataBind();

                _UserCheckListBox.Items.Insert(0, new ListItem("- ALL -", "0"));
                // All department will be selected.
                for (int i = 1; i < _departmentCheckListBox.Items.Count; i++)
                {
                    _departmentCheckListBox.Items[i].Selected = true;
                }
                // All users will be selected.

                for (int i = 0; i < chklstUser.Items.Count; i++)
                {
                    chklstUser.Items[i].Selected = true;
                }

            }
            else   // User un check 'ALL', remove selection for department and users.
            {
                for (int i = 1; i < _departmentCheckListBox.Items.Count; i++)
                {
                    _departmentCheckListBox.Items[i].Selected = false;
                }

                for (int i = 0; i < chklstUser.Items.Count; i++)
                {
                    chklstUser.Items[i].Selected = false;
                }

            }
        }
        else if (index != 0)                        // Selected department other than 'ALL'
        {
            _dtDepartmentTable.Columns.Add("DepartmentID");

            _departmentCheckListBox.Items[0].Selected = false;
            foreach (ListItem li in _departmentCheckListBox.Items)
            {
                if (li.Selected == true)
                {
                    _dtDepartmentTable.Rows.Add(new object[] { li.Value });
                }
            }

            // Get users for selected department.
            if (_dtDepartmentTable.Rows.Count > 0)
            {
                int UserCompanyID = int.Parse(getSessionString("USERCOMPANYID"));
                DataTable dtUsers = objInfra.Get_UserList_By_Dept_DL(UserCompanyID, _dtDepartmentTable);
                _UserCheckListBox.DataSource = dtUsers;
                _UserCheckListBox.DataBind();
                _UserCheckListBox.Items.Insert(0, new ListItem("- ALL -", "0"));

                DataTable dt = objFMS.GET_FolderUser(UDFLib.ConvertToInteger(Request.QueryString["id"]));
                foreach (DataRow row in dt.Rows)
                {
                    foreach (ListItem item in _UserCheckListBox.Items)
                    {
                        if (row["UserID"].ToString() == item.Value.ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                }
            }
            else   // No department selected , then restore deault state.
            {
                if (_value == "File")
                {
                    Load_UserList();
                    GetFolderAccess(chklstUser);
                }
                else
                {
                    lstUser.Items.Clear();
                }
            }
        }
    }

    protected void chklstUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        string result = Request.Form["__EVENTTARGET"];
        string[] checkedBox = result.Split('$'); ;
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);

        if (index == 0 && chklstUser.Items[index].Selected)
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

    //by Vasu
    private void Bind_RAFormCategory(string Category_Name)
    {
        try
        {

            DataTable dt = objFMS.Get_WorkCategoryList(Category_Name);
            chklRAF_Category.DataSource = dt;
            chklRAF_Category.DataTextField = "Work_Category_Name";
            chklRAF_Category.DataValueField = "Work_Categ_ID";
            chklRAF_Category.DataBind();
            //  chklRAF_Category.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));


            for (int i = 0; i < dlRAForms.Items.Count; i++)
            {
                HiddenField hlRaForm = (HiddenField)dlRAForms.Items[i].FindControl("hdnRAFrm");
                for (int j = 0; j < chklRAF_Category.Items.Count; j++)
                {
                    if (hlRaForm.Value == chklRAF_Category.Items[j].Value)
                    {
                        chklRAF_Category.Items[j].Selected = true;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void OnClick_lbtnRAForms(object sender, EventArgs e)
    {
        try
        {

            Bind_RAFormCategory("");

            string AddSalStrmodal = String.Format("showModal('divRACategory',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSalStrmodal", AddSalStrmodal, true);
            udpRACategory.Update();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void imgbtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Bind_RAFormCategory(txtCatSearch.Text.Trim());
            string AddSalStrmodal = String.Format("showModal('divRACategory',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSalStrmodal", AddSalStrmodal, true);
            udpRACategory.Update();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        string selected = string.Empty;
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Work_Categ_ID");
        dt.Columns.Add("Work_Category_Name");
        try
        {
            int i = 1;
            //FileUploader.FileName = ViewState["FileName"].ToString();
            ViewState["Cat_Table"] = null;
            foreach (ListItem li in chklRAF_Category.Items)
            {
                if (li.Selected == true)
                {
                    dt.Rows.Add(i, li.Value, li.Text);
                    i++;
                }
            }
            dlRAForms.DataSource = dt;
            dlRAForms.DataBind();
            ViewState["Cat_Table"] = dt;
            UpdatePanel6.Update();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        string AddSalStrmodal = String.Format("hideModal('divRACategory',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSalStrmodal", AddSalStrmodal, true);
    }

}

