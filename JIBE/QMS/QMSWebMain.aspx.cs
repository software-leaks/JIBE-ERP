using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;

using SMS.Business.QMS;
using System.Web.UI.WebControls;



public partial class QMSWebMain : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document();

    DataSet dsTreeResult = new DataSet();
    DataSet dsIndex = new DataSet();
    DataSet dsDefaultForExport = new DataSet();
    
    

    public string DocServername;
    private Hashtable _systemIcons = new Hashtable();
    public string ipath = "";
    public string iFilename = "";
    DataSet dsSearchResult = new DataSet();
    public int UserDropDown;

    //Public Declaration of Indexing part

    public static string ddlFolderName = "";
    public static string parentFolderName = "";
    public static int totalRecord = 0;
    public static string parentFolder = null;
    public static DataSet resultDS = new DataSet();
    public string TheQuery = "";
    public static readonly int Folder = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadtreeView();
    }

    /// <summary>
    /// this is method uses for the load & bind the treeview control from database. 
    /// </summary>
    public void LoadtreeView()
    {
          
        dsTreeResult = objQMS.getFilpath();
        DataTable dtTree = dsTreeResult.Tables[0];

        BrowseTreeView.Nodes.Add(new TreeNode("DOCUMENT", "DOCUMENT"));
        foreach (DataRow dr in dtTree.Rows)
        {
            string[] DirectoryList = dr[0].ToString().Split('\\');
            int stIndex = 3;
            TreeNode ParentNode = BrowseTreeView.FindNode("DOCUMENT");
            CreateChildNode(ParentNode, stIndex, DirectoryList);
        }

    }

    /// <summary>
    /// this is for the child node by passsing the ParentNode,starting index of the node & directory list.
    /// </summary>
    /// <param name="ParentNode"></param>
    /// <param name="stIndex"></param>
    /// <param name="DirectoryList"></param>
    /// <returns></returns>
    private int CreateChildNode(TreeNode ParentNode, int stIndex, string[] DirectoryList)
    {
        try
        {
            if (stIndex < DirectoryList.Length - 1)
            {
                TreeNode ChildNode = BrowseTreeView.FindNode(ParentNode.ValuePath + "/" + DirectoryList[stIndex + 1]);
                TreeNode NewNode = new TreeNode();
                if (ChildNode == null)
                {
                    ParentNode.ChildNodes.Add(new TreeNode(DirectoryList[stIndex + 1], DirectoryList[stIndex + 1]));
                }
                NewNode = BrowseTreeView.FindNode(ParentNode.ValuePath + "/" + DirectoryList[stIndex + 1]);
                stIndex++;
                CreateChildNode(NewNode, stIndex, DirectoryList);
           }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return 0;
    }



}




