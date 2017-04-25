using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;

public partial class Infrastructure_Snippets_Dash_Edit_MyShortcuts_Menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (hdfissaveClicked.Value == "1")
        {
            TreeView1.Visible = false;
        }
        if (!IsPostBack)
        {
            DataTable dtmenu = BLL_Infra_DashBoard.Get_User_Menu_List(Convert.ToInt32(Session["userid"].ToString()));
         
            GenerateUL(dtmenu);
        }
    }

    private void GenerateUL(DataTable dtmenu)
    {

        DataRow[] drs = dtmenu.Select("Menu_Type is null");
        int meni = 0;
        int child = 0;

        foreach (DataRow dr in drs)
        {
            TreeNode mi = new TreeNode(dr["Menu_Short_Discription"].ToString().Trim(), dr["Menu_Code"].ToString());
           // if (string.IsNullOrWhiteSpace(Convert.ToString(dr["menu_link"])))
                mi.ShowCheckBox = false;

            TreeView1.Nodes.Add(mi);

            DataRow[] drInners = dtmenu.Select("Menu_Type ='" + dr["Menu_Code"].ToString() + "' ");
            if (drInners.Length != 0)
            {
                child = 0;
                foreach (DataRow drInner in drInners)
                {
                    TreeNode miner;
                    miner = new TreeNode(drInner["Menu_Short_Discription"].ToString().Trim(),  drInner["Menu_Code"].ToString());
                    miner.Checked = Convert.ToBoolean(drInner["Favourite"]);
                    if(string.IsNullOrWhiteSpace(Convert.ToString(drInner["menu_link"])))
                        miner.ShowCheckBox = false;

                    TreeView1.Nodes[meni].ChildNodes.Add(miner);
                    TreeView1.Nodes[meni].CollapseAll();

                    string filter = "Menu_Type = '" + drInner["Menu_Code"].ToString() + "'";

                    dtmenu.AcceptChanges();
                    DataRow[] drInnerLinks = dtmenu.Select(filter);

                    if (drInnerLinks.Length != 0)
                    {
                        foreach (DataRow drInnerLink in drInnerLinks)
                        {
                            TreeNode milink = new TreeNode(drInnerLink["Menu_Short_Discription"].ToString().Trim(),  drInnerLink["Menu_Code"].ToString());
                            milink.Checked = Convert.ToBoolean(drInnerLink["Favourite"]);
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

     DataTable dtMenu = new DataTable();
      
    protected void btnSaveFavourite_Click(object s, EventArgs e)
    {
         dtMenu.Columns.Add("menu_code");
        dtMenu.Columns.Add("is_favourite");
        dtMenu.PrimaryKey = new DataColumn[] {dtMenu.Columns["menu_code"] };

            Traverse(TreeView1.Nodes);
     
        BLL_Infra_DashBoard.UPD_User_Menu_Favourite(Convert.ToInt32(Session["userid"].ToString()), dtMenu);
      
        string msgmodal = String.Format("parent.asyncGet_MyShortsCuts_Menu();parent.CloseWindow('POP__Menu');");
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Apprmodala", msgmodal, true);
      
    }

    private void Traverse(TreeNodeCollection nodes)
    {
        foreach (TreeNode node in nodes)
        {
            if (!dtMenu.Rows.Contains(node.Value))
            {
                DataRow dr = dtMenu.NewRow();
                dr["menu_code"] = node.Value;
                dr["is_favourite"] = Convert.ToInt32(node.Checked);
                dtMenu.Rows.Add(dr);
            }

            Traverse(node.ChildNodes);
        }
    } 

}