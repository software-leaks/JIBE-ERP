using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SMS.Business.QMSDB;
using AjaxControlToolkit4;
using SMS.Business.Infrastructure;

public partial class CheckOutForm : System.Web.UI.Page
{
    
    
    //int fileID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblProcedureId.Attributes.Add("style", "visibility:hidden");
       
        if (AjaxFileUploadInsertImage.IsInFileUploadPostBack)
        {
        }
        else
        {
            if (!IsPostBack)
            {
                BindUserList();
                txtProcedureSectionDetails.config.toolbar = new object[]
            {new object[] { "Source", "-", "Save", "NewPage", "Preview", "-", "Templates" },
                new object[] { "Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-", "Print", "SpellChecker", "Scayt" },
                new object[] { "Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat" },
                new object[] { "Form", "Checkbox", "Radio", "TextField", "Textarea", "Select", "Button", "ImageButton", "HiddenField" },
                "/",
                new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
                new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv" },
                new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
                new object[] { "BidiLtr", "BidiRtl" },
                new object[] { "Link", "Unlink", "Anchor" },
                new object[] { "Image", "Flash", "Table", "HorizontalRule", "Smiley", "SpecialChar", "PageBreak", "Iframe" },
                "/",
                new object[] { "Styles", "Format", "Font", "FontSize" },
                new object[] { "TextColor", "BGColor" },
                new object[] { "Maximize", "ShowBlocks", "-", "InsertImage" },
                
                              
              
            };
             // fileID = Convert.ToInt32(Request.PathInfo.Substring(1));
                lblProcedureId.Text = Request.QueryString["FileID"].ToString();
                txtProcedureSectionDetails.Width = 800;
                ////get the version & Operation info by Check out fileID
                DataTable dsOperationInfo = BLL_QMSDB_Procedures.QMSDBProcedures_Edit(int.Parse(lblProcedureId.Text));
                if (dsOperationInfo.Rows.Count > 0)
                {
                    //get the last row of the table to know the current status of the file

                    string FileStatus = dsOperationInfo.Rows[0]["CHECK_INOUT_STATUS"].ToString();
                    string User = dsOperationInfo.Rows[0]["MODIFIED_BY"].ToString();

                    if (FileStatus == "true" && (Convert.ToInt32(Session["USERID"]) != Convert.ToInt32(dsOperationInfo.Rows[0]["MODIFIED_BY"].ToString())))
                    {
                        string Message = "The file is already Checked Out by " + dsOperationInfo.Rows[0]["MODIFIED_BY"].ToString();
                        String msg = String.Format("myMessage('" + Message + "')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                    }
                    else
                        showSaveDialog();
                }
                else
                    showSaveDialog();
            }
           
        }
        LoadtreeView();

    }
    protected void BindUserList()
    {
          BLL_Infra_UserCredentials objInfra = new BLL_Infra_UserCredentials();
           int UserCompanyID = int.Parse(Session["USERCOMPANYID"].ToString());
            DataTable dtUsers = objInfra.Get_UserList(UserCompanyID);
            lstUser.DataSource = dtUsers;
            lstUser.DataBind();
            lstUser.Items.Insert(0, new ListItem("- ALL -", "0"));       

    }
    public void LoadtreeView()
    {

        if (Request.QueryString["status"] != null && Request.QueryString["status"].ToString() == "New")
        {
            TreeNode parentNode = new TreeNode("Contents", "Contents", getNodeImageURL("Parent.doc"));
            parentNode.Target = "docPreview";
            parentNode.SelectAction = TreeNodeSelectAction.Select;
            parentNode.Expand();
            BrowseTreeView.Nodes.Add(parentNode);

            TreeNode NewNode = new TreeNode("Header", "0", getNodeImageURL("Parent.doc"));
            NewNode.Target = "docPreview";
            NewNode.SelectAction = TreeNodeSelectAction.Select;
            NewNode.Expand();
            parentNode.ChildNodes.Add(NewNode);

            NewNode = new TreeNode("Details", "1", getNodeImageURL("Parent.doc"));
            NewNode.Target = "docPreview";
            NewNode.SelectAction = TreeNodeSelectAction.Select;
            NewNode.Expand();
            parentNode.ChildNodes.Add(NewNode);


            NewNode = new TreeNode("Footer", "2", getNodeImageURL("Parent.doc"));
            NewNode.Target = "docPreview";
            NewNode.SelectAction = TreeNodeSelectAction.Select;
            NewNode.Expand();
            parentNode.ChildNodes.Add(NewNode);

            

        }
        else
        {


            DataTable dtSection = BLL_QMSDB_ProcedureSection.QMSDBProcedureSection_List(int.Parse(lblProcedureId.Text));
            ddlSection.DataValueField = "SECTION_ID";
            ddlSection.DataTextField = "FILESNAME";
            ddlSection.DataSource = dtSection;
            ddlSection.DataBind();

            if (dtSection.Rows.Count > 0)
            {

                TreeNode parentNode = new TreeNode(dtSection.Rows[0]["FILESNAME"].ToString(), dtSection.Rows[0]["SECTION_ID"].ToString(), getNodeImageURL("Parent.Doc"));
                parentNode.Target = "docPreview";
                parentNode.SelectAction = TreeNodeSelectAction.Select;
                parentNode.Expand();
                BrowseTreeView.Nodes.Add(parentNode);
                foreach (DataRow dr in dtSection.Rows)
                {
                    if (dr["SECTION_ID"].ToString() != "0")
                    {

                        TreeNode NewNode = new TreeNode(dr["FILESNAME"].ToString(), dr["SECTION_ID"].ToString(), getNodeImageURL(dr["FILESNAME"].ToString() + ".doc"));
                        NewNode.SelectAction = TreeNodeSelectAction.Select;
                        NewNode.Expand();
                        parentNode.ChildNodes.Add(NewNode);
                    }

                }
            }

        }


    }
    private int CreateChildNode(string NodePath, string NodeName, int NodeType, int NodeID)
    {
        try
        {
            string ParentNodePath = NodePath.Replace("/" + NodeName, "");
            TreeNode ParentNode = BrowseTreeView.FindNode(ParentNodePath);

            if (ParentNode != null)
            {
                TreeNode NewNode = new TreeNode(NodeName, NodeName.ToString(), getNodeImageURL(NodeName));


                if (NodeType == 0)
                {
                    NewNode.NavigateUrl = "FileLoader.aspx?DocID=" + NodeID;
                    NewNode.Target = "docPreview";
                    NewNode.SelectAction = TreeNodeSelectAction.Select;
                    NewNode.Expand();
                }
                else
                {
                    NewNode.NavigateUrl = "AddNewFile.aspx?Path=" + NodePath;
                    NewNode.Target = "docPreview";
                    NewNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                    NewNode.Collapse();
                }

                ParentNode.ChildNodes.Add(NewNode);
            }



        }
        catch (Exception ex)
        {
            throw ex;
        }
        return 0;
    }

    private int CreateChildNode111(TreeNode ParentNodee, int stIndexdd, string[] DirectoryList, string DocID, int NodeType)
    {
        TreeNode ParentNode = null;
        try
        {
            for (int stIndex = 0; stIndex < DirectoryList.Length; stIndex++)
            {
                // string nodeText = ParentNode.ValuePath + "/" + DirectoryList[stIndex];
                if (stIndex == 0)
                    ParentNode = BrowseTreeView.FindNode(DirectoryList[stIndex]);
                else
                    ParentNode = BrowseTreeView.FindNode(DirectoryList[stIndex - 1]);

                TreeNode ChildNode = BrowseTreeView.FindNode(DirectoryList[stIndex]);
                TreeNode NewNode = new TreeNode();

                if (ChildNode == null)
                {
                    ParentNode.ChildNodes.Add(new TreeNode(DirectoryList[stIndex], DirectoryList[stIndex], getNodeImageURL(DirectoryList[stIndex]), "", DocID));
                }

                // string nodeText1 = ParentNode.ValuePath + "/" + DirectoryList[stIndex];
                NewNode = BrowseTreeView.FindNode(ParentNode.ValuePath + "/" + DirectoryList[stIndex]);
                if (NewNode != null)
                {
                    //if (NodeType == 0 && ChildNode == null)
                    if (DirectoryList[stIndex].ToString().IndexOf(".") > 0)
                    {
                        NewNode.NavigateUrl = "FileLoader.aspx?DocID=" + DocID;
                        NewNode.Target = "docPreview";
                        NewNode.SelectAction = TreeNodeSelectAction.Select;
                        NewNode.Expand();
                    }
                    else
                    {
                        string value = Convert.ToString(NewNode.ValuePath);
                        NewNode.NavigateUrl = "AddNewFile.aspx?Path=" + value;
                        NewNode.Target = "docPreview";
                        NewNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                        NewNode.Collapse();
                    }

                }
                // stIndex++;
                //CreateChildNode(NewNode, stIndex, DirectoryList, DocID);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return 0;
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
    /// <summary>
    /// this is  check the all the condition & operation for particular file, give the apropriate  message to end user.
    /// </summary>
    public void showSaveDialog()
    {
        //checking for avoid the multiple Check Out the file
        DataTable dsOperationInfo = BLL_QMSDB_Procedures.QMSDBProcedures_Edit(int.Parse(lblProcedureId.Text));

        if (dsOperationInfo.Rows.Count > 0)
        {
            if (dsOperationInfo.Rows[0]["CHECK_INOUT_STATUS"].ToString() == "true")
            {
                String msg = String.Format("myMessage('You have already Checked Out the file.');window.location.href='fileloader.aspx?docid=" + lblProcedureId.Text + "';");
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
     int i=   BLL_QMSDB_Procedures.QMSDBProcedure_CheckOUT(int.Parse(lblProcedureId.Text), Convert.ToInt32(Session["USERID"]));
        //get latest File details by the file ID
     DataTable dsFileDetails = BLL_QMSDB_Procedures.QMSDBProcedures_Edit(int.Parse(lblProcedureId.Text));

        string navURL = "";       
        if (dsFileDetails.Rows.Count > 0)
        {
            txtProcedureSectionDetails.Text = dsFileDetails.Rows[0]["DETAILS"].ToString();
        }
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
    protected void AjaxFileUploadInsertImage_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        Byte[] fileBytes = file.GetContents();
        string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\QMSDB");
        Guid GUID = Guid.NewGuid();
        string FullFilename = Path.Combine(sPath, "QMSDB_" + GUID.ToString() + Path.GetExtension(file.FileName));

        FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
        fileStream.Write(fileBytes, 0, fileBytes.Length);
        fileStream.Close();

        txtProcedureSectionDetails.Text = txtProcedureSectionDetails.Text + "<img src=\"" + FullFilename + "\" />";
    }
    protected void btnSaveQueryAs_Click(object sender, EventArgs e)
    {
        //if (txtQuery.Text != "")
        //{
        string js = "showModal('AddNewSection');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
        //}
    }
    protected void btnSaveSection_Click(object sender, EventArgs e)
    {
        string js = "alert('Query Saved !!'); hideModal('AddNewSection');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
    }
    protected void btnCancelSction_Click(object sender, EventArgs e)
    {
        string js = "hideModal('AddNewSection');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = BrowseTreeView.SelectedNode;
        int sectionId = int.Parse(selectedNode.Value.ToString());
        int i = BLL_QMSDB_ProcedureSection.QMSDBProcedureSection_Update_Details(sectionId, int.Parse(lblProcedureId.Text), txtProcedureSectionDetails.Text, int.Parse(Session["USERID"].ToString()));
        string js = "alert('Query Saved !!');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
    
    }
    protected void btnSendTo_Click(object sender, EventArgs e)
    {
        string js = "showModal('DivSendForApp');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
       
    }
    protected void btnFinalize_Click(object sender, EventArgs e)
    {
        

        //int Res = BLL_QMSDB_Procedures.QMSDBProcedures_InsertWithSection("TEST01", txtProcedureName.Text, UDFLib.ConvertIntegerToNull(ddlFolderName.SelectedValue), FileWateMark.Checked == true ? 1 : 0, UDFLib.ConvertIntegerToNull(ddlHeaderTemplate.SelectedValue), UDFLib.ConvertIntegerToNull(ddlFooterTemlate.SelectedValue), UDFLib.ConvertToInteger(Session["USERID"]), txtProcedureSectionDetails.Text);
        //if (Res > 0)
        //{
        //    string js = "alert('Query Saved !!'); hideModal('dvSaveCommand');";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
        //}
    }
    protected void BrowseTreeView_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNode selectedNode = BrowseTreeView.SelectedNode;
        if (selectedNode.Parent != null)
        {

            DataTable dt = BLL_QMSDB_ProcedureSection.QMSDBProcedureSection_Edit(int.Parse(selectedNode.Value.ToString()),int.Parse(lblProcedureId.Text));
            if (dt.Rows.Count > 0)
                txtProcedureSectionDetails.Text = dt.Rows[0]["CHECKOUTDETAILS"].ToString();
        }

    }

    protected void btnCancelApp_Click(object sender, EventArgs e)
    {
        string js = "hideModal('DivSendForApp');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
    }
    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = BrowseTreeView.SelectedNode;
        int sectionId = int.Parse(selectedNode.Value.ToString());
        int i = BLL_QMSDB_Procedures.QMSDBProcedures_SendApprovel(int.Parse(lblProcedureId.Text),txtComments.Text,UDFLib.ConvertIntegerToNull(lstUser.SelectedValue),ddlStatus.SelectedItem.Text, int.Parse(Session["USERID"].ToString()));
        string js = "alert('Query Saved !!');hideModal('DivSendForApp');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);

    }

}