using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;


public partial class Infrastructure_ShipSettings_ShipMenuAssignment : System.Web.UI.Page
{
    BLL_Infra_MenuManagement objMenuBLL = new BLL_Infra_MenuManagement();
    BLL_Infra_UserCredentials objBllUser = new BLL_Infra_UserCredentials();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillVesselDDL();
            Load_RankList();
            BindProjectModuleTree();
        }
        lblMessage.Text = "";
    }

    protected void BindProjectModuleTree()
    {
        DataSet ds = BLL_Infra_ShipSettings.Get_Project_Module_Tree(Convert.ToInt32(Session["USERID"].ToString()));
        if (ds.Tables.Count > 0)
        {
            int meni = 0;
            int child = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TreeNode mi = new TreeNode(dr["Name"].ToString().Trim(), dr["Project_ID"].ToString());
                TreeView1.Nodes.Add(mi);

                DataRow[] drInners = ds.Tables[1].Select("Project_ID ='" + dr["Project_ID"].ToString() + "' ");
                if (drInners.Length != 0)
                {

                    foreach (DataRow drInner in drInners)
                    {
                        TreeNode miner;
                        miner = new TreeNode(drInner["Name"].ToString().Trim(), drInner["Module_ID"].ToString());

                        TreeView1.Nodes[meni].ChildNodes.Add(miner);

                        ///////////////

                        DataRow[] drInners1 = ds.Tables[2].Select("Module_ID ='" + drInner["Module_ID"].ToString() + "' ");
                        if (drInners1.Length != 0)
                        {

                            foreach (DataRow drInner1 in drInners1)
                            {
                                TreeNode miner1;
                                miner1 = new TreeNode(drInner1["Name"].ToString().Trim(), drInner1["Screen_ID"].ToString());
                                miner.ChildNodes.Add(miner1);
                                
                            }

                            miner.CollapseAll();
                          
                        }

                        //////////////////
          
                        TreeView1.Nodes[meni].CollapseAll();
 
                        child++;
                    }
                }
                meni++;
            }
        }
    }

    public void FillVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataBind();
            DDLVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            DDLVessel.SelectedIndex = 0;

            DDLFromVessel.DataTextField = "Vessel_name";
            DDLFromVessel.DataValueField = "Vessel_id";
            DDLFromVessel.DataSource = dtVessel;
            DDLFromVessel.DataBind();
            DDLFromVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            DDLFromVessel.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

        }

    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.IsAdmin == 0)
        {
            if (objUA.View == 0)
                Response.Redirect("~/default.aspx?msgid=1");

            if (objUA.Add == 0)
            {
                Response.Redirect("~/default.aspx?msgid=2");
            }
            if (objUA.Edit == 0)
            {
                Response.Redirect("~/default.aspx?msgid=3");
            }
            if (objUA.Delete == 0)
            {
                Response.Redirect("~/default.aspx?msgid=4");
            }
            if (objUA.Approve == 0)
            {
                Response.Redirect("~/default.aspx?msgid=5");
            }
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void Load_RankList()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        lstRankList.DataSource = dt;
        lstRankList.DataTextField = "Rank_Short_Name";
        lstRankList.DataValueField = "ID";
        lstRankList.DataBind();


        DDLFromRank.DataSource = dt;
        DDLFromRank.DataTextField = "Rank_Short_Name";
        DDLFromRank.DataValueField = "ID";
        DDLFromRank.DataBind();
        DDLFromRank.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        DDLFromRank.SelectedIndex = 0;
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        Load_Rank_Menu();
    }

    private TreeNode FindRootNode(TreeNode treeNode)
    {
        while (treeNode.Parent != null)
        {
            treeNode = treeNode.Parent;
        }
        return treeNode;
    }


    protected void btnCopy_Click(object sender, EventArgs e)
    {

        if (TreeView1.SelectedNode != null)
        {
            TreeNode tn = TreeView1.SelectedNode.Parent;
            string Project_ID = tn != null ? tn.Value : TreeView1.SelectedNode.Value;
            string Module_ID = tn != null ? TreeView1.SelectedNode.Value : null;

            BLL_Infra_ShipSettings.Copy_Rank_Menu_Acess(UDFLib.ConvertToInteger(DDLFromVessel.SelectedValue), UDFLib.ConvertToInteger(DDLFromRank.SelectedValue)
                , UDFLib.ConvertToInteger(DDLVessel.SelectedValue), UDFLib.ConvertIntegerToNull(lstRankList.SelectedValue), UDFLib.ConvertToInteger(ddlAppendMode.SelectedValue)
                , UDFLib.ConvertToInteger(ddlCopyMenu.SelectedValue), UDFLib.ConvertIntegerToNull(Project_ID), UDFLib.ConvertIntegerToNull(Module_ID), UDFLib.ConvertToInteger(Session["USERID"].ToString()));

            Load_Rank_Menu();

            lblMessage.Text = "Access rights has been copied.";

        }
    }

    protected void btnResetMenu_Click(object sender, EventArgs e)
    {

        try
        {

            int iSessionUserID = GetSessionUserID();
            if (lstRankList.SelectedValue != "0")
            {
                foreach (GridViewRow row in gvMenu.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {

                        BLL_Infra_ShipSettings.Ins_Remove_Rank_Menu_Acess(UDFLib.ConvertIntegerToNull(gvMenu.DataKeys[row.RowIndex].Value.ToString()), iSessionUserID);

                    }
                }
            }


            Load_Rank_Menu();
            lblMessage.Text = "Access rights has been removed.";

        }
        catch (Exception ex)
        {

        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            int iSessionUserID = GetSessionUserID();
            if (lstRankList.SelectedValue != "0")
            {
                foreach (GridViewRow row in gvMenu.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {

                        BLL_Infra_ShipSettings.Ins_Upd_Rank_Menu_Acess(UDFLib.ConvertIntegerToNull(gvMenu.DataKeys[row.RowIndex].Value.ToString())
                                                                        , Convert.ToInt32(DDLVessel.SelectedValue), Convert.ToInt32(lstRankList.SelectedValue)
                                                                        , Convert.ToInt32((((Label)row.FindControl("lblScreenID")).Text).ToString())
                                                                        , (((CheckBox)row.FindControl("chkMenu")).Checked == true) ? 1 : 0
                                                                        , (((CheckBox)row.FindControl("chkView")).Checked == true) ? 1 : 0
                                                                        , (((CheckBox)row.FindControl("chkAdd")).Checked == true) ? 1 : 0
                                                                        , (((CheckBox)row.FindControl("chkEdit")).Checked == true) ? 1 : 0
                                                                        , (((CheckBox)row.FindControl("chkDelete")).Checked == true) ? 1 : 0
                                                                        , (((CheckBox)row.FindControl("chkApprove")).Checked == true) ? 1 : 0
                                                                        , iSessionUserID);


                    }
                }
            }


            Load_Rank_Menu();
            lblMessage.Text = "Access rights has been saved.";

        }
        catch (Exception ex)
        {

        }
    }

    protected void gvMenu_DataBound(object sender, EventArgs e)
    {


    }

    protected void Load_Rank_Menu()
    {

        if (TreeView1.SelectedNode != null)
        {

            TreeNode tn = TreeView1.SelectedNode.Parent;
            string Project_ID = tn != null ? tn.Value : TreeView1.SelectedNode.Value;
            string Module_ID = tn != null ? TreeView1.SelectedNode.Value : null;


            DataTable dt = BLL_Infra_ShipSettings.Get_Rank_Menu_Acess(UDFLib.ConvertToInteger(Project_ID), UDFLib.ConvertIntegerToNull(Module_ID)
                    , UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), UDFLib.ConvertToInteger(lstRankList.SelectedValue));


            if (dt.Rows.Count > 0)
            {
                gvMenu.DataSource = dt;
                gvMenu.DataBind();
                btnSave.Visible = true;
                btnResetMenu.Visible = true;
            }
            else
            {
                gvMenu.DataSource = dt;
                gvMenu.DataBind();
                btnSave.Visible = false;
                btnResetMenu.Visible = false;
            }
        }
    }

    protected void lstRankList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Rank_Menu();
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((CheckBox)sender).Parent.Parent;
        Boolean bln = true;

        if (((CheckBox)gvr.FindControl("chkAll")).Checked)
            bln = true;
        else
            bln = false;

        ((CheckBox)gvr.FindControl("chkMenu")).Checked = bln;
        ((CheckBox)gvr.FindControl("chkView")).Checked = bln;
        ((CheckBox)gvr.FindControl("chkAdd")).Checked = bln;
        ((CheckBox)gvr.FindControl("chkEdit")).Checked = bln;
        ((CheckBox)gvr.FindControl("chkDelete")).Checked = bln;
        ((CheckBox)gvr.FindControl("chkApprove")).Checked = bln;
    }

    protected void lstCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Load_UserList();
        Load_Rank_Menu();
    }

    protected void gvMenu_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMenu.PageIndex = e.NewPageIndex;
        Load_Rank_Menu();
    }

}