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
using SMS.Business.QMS;
using System.IO;
//using SyncronizerDataLog;


public partial class Web_frmRevision : System.Web.UI.Page
{

    DataSet dsTreeResult = new DataSet();
    DataSet dsIndex = new DataSet();
    DataSet dsDefaultForExport = new DataSet();
    DataTable dt = new DataTable();
    BLL_QMS_Document objQMS = new BLL_QMS_Document();
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
        {
            LoadtreeView();
            DateTime today = DateTime.Now;
            txtModeDate.Text = today.ToString("dd/MM/yyyy");
            DataSet dsVesselList = objQMS.getVesselList();
            GrdVesselNameList.DataSource = dsVesselList.Tables[0];
            GrdVesselNameList.DataBind();
        }

    }

    /// <summary>
    /// this is method uses for the load & bind the treeview control from database. 
    /// </summary>
    public void LoadtreeView()
    {

        //string FilePath[]= "";            
        dsTreeResult = objQMS.getFilpath();
        DataTable dtTree = dsTreeResult.Tables[0];
        //foreach (DataRow dr in dtTree.Rows)
        //{
        //    string[] DirectoryList = dr[0].ToString().Split('\\');
        //   // string DirectoryList = dr[0].ToString();
        //    TreeNode tr = new TreeNode();
        //    string sd = DirectoryList[4].ToUpper();
        //    int iTreeIndex = 0;
        //    //for (int i = 3; i < DirectoryList.Length; i++)
        //    //{
        //    //    if(DirectoryList[i].ToUpper() == "QMS")
        //    //    {
        //    //        iTreeIndex = i+1;
        //    //        startindex = i+2;
        //    //        tr = new TreeNode(DirectoryList[i+1].ToUpper());
        //    //        BrowseTreeView.Nodes.Add(tr);

        //    //    }
        //        CreateChildNode(DirectoryList[3].ToUpper(), DirectoryList[4].ToUpper(), iTreeIndex, DirectoryList);
        //   //}
        //}

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

    /// <summary>
    /// this is for the find out the node having checked out checkbox by the user in tree nodes.
    /// </summary>
    /// <param name="tview"></param>
    /// <returns></returns>
    public DataTable TraverseTreeView(TreeView tview)
    {

        dt.Columns.Add("Path", typeof(string));
        TreeNode temp = new TreeNode();

        //Loop through the Parent Nodes
        for (int k = 0; k < tview.Nodes.Count; k++)
        {
            //Store the Parent Node in temp
            temp = tview.Nodes[k];
            //Display the Text of the Parent Node i.e. temp
            // MessageBox.Show(temp.Text);

            //Now Loop through each of the child nodes in this parent node i.e.temp
            for (int i = 0; i < temp.ChildNodes.Count; i++)
                visitChildNodes(temp.ChildNodes[i], dt); //send every child to the function for further traversal
        }
        return dt;
    }

    /// <summary>
    /// traversal in the child node of the treeview control.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="dt"></param>
    public void visitChildNodes(TreeNode node, DataTable dt)
    {
        string fullpath = node.ValuePath.ToString();
        string[] fileArr = Convert.ToString(fullpath).Split('.');
        switch (fileArr[fileArr.Length - 1].ToUpper())
        {
            case "PDF":
            case "DOC":
            case "TXT":
            case "XLS":
                if (node.Checked)
                {
                    DataRow drManual = dt.NewRow();
                    drManual[0] = fullpath;
                    dt.Rows.Add(drManual);
                }

                break;

        }

        //Loop Through this node and its childs recursively
        for (int j = 0; j < node.ChildNodes.Count; j++)
            visitChildNodes(node.ChildNodes[j], dt);

    }

    protected void BrowseTreeView_SelectedNodeChanged(object sender, EventArgs e)
    {
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //TraverseTreeView(BrowseTreeView);
        //InsertVesselRecord(0, 0);

    }


    public int InsertVesselRecord(int vesselCode, int CreatedBy_ID)
    {
        int ID = 0;
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dtR in dt.Rows)
            {
                string sDatarow = dtR[0].ToString();
                string[] splitDatarow = sDatarow.Split('/');
                iFilename = (splitDatarow[splitDatarow.Length - 1].ToString());
                string SDate = txtModeDate.Text.ToString();
                ID = objQMS.insertDataQmsRev(vesselCode, sDatarow, iFilename, SDate, CreatedBy_ID);

            }
        }
        return ID;
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// this method does the database updation as well as synchronisation the data.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSendToVessel_Click(object sender, EventArgs e)
    {
        DataTable dtSelectedNode = TraverseTreeView(BrowseTreeView);
        String msg = "";
        int i = 0;
        try
        {
            if (dtSelectedNode.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in this.GrdVesselNameList.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                    {

                        Label lblVesselCode = (Label)gvr.FindControl("lblVessel");
                        int VesselCode = Convert.ToInt32(lblVesselCode.Text);
                        int CreatedBy_ID = 100028;
                        int ID = InsertVesselRecord(VesselCode, CreatedBy_ID);

                        //////Hashtable ht = SyncronizerDataLog.SyncLogMaintain.RetrivePKHashTable("QMSdtls_Revision");
                        //////ht["ID"] = ID;
                        //////ht["Vessel_Code"] = VesselCode;
                        //////SyncronizerDataLog.SyncLogMaintain.InsertRecord("QMSdtls_Revision", ht, "~GET");
                        i++;
                    }
                }
                if (i > 0)
                    msg = String.Format("myMessage('Selected Documents Revised Succesfully')");
                else
                    msg = String.Format("myMessage('Please select the vessel from list')");
            }
            else
            {
                msg = String.Format("myMessage('Please select the  document  from tree view')");
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }


        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// on click, all vessel going to select at a time
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSelectAll_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in this.GrdVesselNameList.Rows)
        {
            CheckBox chk = (CheckBox)gvr.FindControl("chkSelect");
            chk.Checked = true;
        }
    }

    /// <summary>
    /// on click, all vessel going to Deselect at a time
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnDeselectAll_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in this.GrdVesselNameList.Rows)
        {
            CheckBox chk = (CheckBox)gvr.FindControl("chkSelect");
            chk.Checked = false;
        }
    }
}
