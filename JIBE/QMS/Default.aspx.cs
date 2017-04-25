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
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using SMS.Business.QMS;
using SMS.Business.Infrastructure;
using SMS.Properties;


public partial class Main : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document();
    string FILE_SERVER_NAME = "";
    ArrayList ar = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("Main.aspx");
        
        //LoadtreeView();

        //if (Session["NAV_URL"] != null)
        //{
        //    if (Session["NAV_URL"].ToString() != "")
        //    {
        //        NavigateNodeURL(Session["NAV_URL"].ToString());
        //        TreeNode tNode = BrowseTreeView.FindNode(Session["NAV_URL"].ToString());
        //        hidden_SelectedNodeURL.Value = Session["NAV_URL"].ToString();
        //        //OpenfileInfo.Disabled = true;
        //        Session["NAV_URL"] = "";
        //    }
        //}
        //Session["FolderPath"] = hidden_SelectedNodeURL.Value;
    }
    
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Search.HRef = Search.HRef + ((Label)myTree.FindControl("lblVersion")).Text.Trim();

    }
    public void opentab(object sender, EventArgs e)
    {
        ResponseHelper.Redirect("~/web/MainLog.aspx", "self", "");
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    /// <summary>
    /// this is method uses for the load & bind the treeview control from database. 
    /// </summary>
    public void LoadtreeView()
    {
        
        DataTable dtFolders = objQMS.getFolderList(GetSessionUserID());
        DataTable dtFiles = objQMS.getFileList(GetSessionUserID());

        //this is for the root node
        TreeNode parentNode = new TreeNode("DOCUMENTS", "DOCUMENTS", getNodeImageURL("Parent.FDC"));
        parentNode.NavigateUrl = "AddNewFile.aspx?Path=DOCUMENTS";
        parentNode.Target = "docPreview";
        parentNode.SelectAction = TreeNodeSelectAction.Select;
        parentNode.Expand();
        BrowseTreeView.Nodes.Add(parentNode);

        if (dtFolders.Rows.Count > 0)
        {

            foreach (DataRow dr in dtFolders.Rows)
            {
                CreateChildNode(dr["FilePath"].ToString(), dr["LogFileID"].ToString(), Convert.ToInt32(dr["NodeType"].ToString()), Convert.ToInt32(dr["ID"].ToString()));
            }
        }

        if (dtFiles.Rows.Count > 0)
        {

            foreach (DataRow dr in dtFiles.Rows)
            {
                CreateChildNode(dr["FilePath"].ToString(), dr["LogFileID"].ToString(), Convert.ToInt32(dr["NodeType"].ToString()), Convert.ToInt32(dr["ID"].ToString()));
            }

        }

    }
    
    /// <summary>
    /// this is for the child node by passsing the ParentNodePath,Node Name,node type & node ID.
    /// </summary>
    /// <param name="NodePath"></param>
    /// <param name="NodeName"></param>
    /// <param name="NodeType"></param>
    /// <param name="NodeID"></param>
    /// <returns></returns>
    /// 
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

    /// <summary>
    /// this is a load the document into the IFRAME for display.
    /// </summary>
    /// <param name="navURL"></param>
    protected void NavigateNodeURL(string navURL)
    {
        string script = "document.getElementById('docPreview').src='" + navURL + "';";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Message", script, true);
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

    protected void BrowseTreeView_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNode selectedNode = BrowseTreeView.SelectedNode;

        if (selectedNode.ChildNodes.Count > 0)
        {
            //node type is a folder
            // lblDocName.Text = "";

        }
        else
        {
            string fileName = selectedNode.Text;
            // lblDocName.Text = fileName;
            selectedNode.NavigateUrl = selectedNode.ValuePath;
            selectedNode.Target = "docPreview";

            string js = "<script language='javascript' type='text/javascript'>DocOpenOnIFrame('" + Convert.ToString(selectedNode.ValuePath) + "');</script>";
            Page.ClientScript.RegisterStartupScript(GetType(), "script", js);

        }
    }

    protected void btnCreateFolder_Click(object sender, EventArgs e)
    {
        //  DivRemarks.Visible = true;
    }

    protected void btndivCancel_Click(object sender, EventArgs e)
    {
        // DivRemarks.Visible = false;
    }

    protected void btndivSave_Click(object sender, EventArgs e)
    {
        bool folderExists = Directory.Exists("");
        if (!folderExists)
        {
            System.IO.Directory.CreateDirectory("");
        }


        string js2 = "<script language='javascript' type='text/javascript'>myMessage('Folder created successfully.')</script>";
        Page.ClientScript.RegisterStartupScript(GetType(), "script", js2);
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        
        UserAccess objUA = new UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
           
        }
        if (objUA.Edit == 0)
        {

        }
        if (objUA.Delete == 0)
        {
        }

        if (objUA.Approve == 0)
        {

        }

    }

}