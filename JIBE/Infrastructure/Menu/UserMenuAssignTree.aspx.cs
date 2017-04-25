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
using System.Drawing;


public partial class Infrastructure_Menu_UserMenuAssignTree : System.Web.UI.Page
{
    BLL_Infra_MenuManagement objMenuBLL = new BLL_Infra_MenuManagement();
    BLL_Infra_UserCredentials objBllUser = new BLL_Infra_UserCredentials();

    protected void Page_Load(object sender, EventArgs e)
    {
        string strConn = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        try

        {
            if (!IsPostBack)
            {

                UserAccessValidation();
                if (Session["USERID"] != null)
                {
                    SqlConnection sCon = new SqlConnection(strConn);
                    SqlDataAdapter sqlAdp = new SqlDataAdapter("SP_INF_MNU_Get_MenuLibAccess", sCon);
                    sqlAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlAdp.SelectCommand.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int));

                    sqlAdp.SelectCommand.Parameters["@userid"].Value = Convert.ToInt32(Session["USERID"].ToString());
                    DataSet ds = new DataSet();
                    sqlAdp.Fill(ds, "Menu");
                    GenerateUL(ds);

                    Load_UserList();
                    BindUserDLL();
                    Get_Role();
                    if (Session["USERName"].ToString().ToLower() != "admin")
                    {
                        rdbCopy.Items.Remove(rdbCopy.Items.FindByValue("2"));
                    }
                }

            }

        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.Message + "')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
        }

    }

    protected void Get_Role()
    {
       DataTable dt= objMenuBLL.Get_Role();
       ddlCopyFromRole.DataSource = dt;
       ddlCopyFromRole.DataTextField = "Role";
       ddlCopyFromRole.DataValueField = "Role_Id";
       ddlCopyFromRole.DataBind();
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

    private void GenerateUL(DataSet ds)
    {

        DataRow[] drs = ds.Tables["Menu"].Select("Menu_Type is null");
        int meni = 0;
        int child = 0;

        foreach (DataRow dr in drs)
        {
            TreeNode mi = new TreeNode(dr["Menu_Short_Discription"].ToString().Trim(), dr["menu_type"].ToString() + "_" + dr["Menu_Code"].ToString());
            TreeView1.Nodes.Add(mi);

            DataRow[] drInners = ds.Tables["Menu"].Select("Menu_Type ='" + dr["Menu_Code"].ToString() + "' ");
            if (drInners.Length != 0)
            {
                child = 0;
                foreach (DataRow drInner in drInners)
                {
                    TreeNode miner;
                    miner = new TreeNode(drInner["Menu_Short_Discription"].ToString().Trim(), drInner["menu_type"].ToString() + "_" + drInner["Menu_Code"].ToString());

                    TreeView1.Nodes[meni].ChildNodes.Add(miner);
                    TreeView1.Nodes[meni].CollapseAll();

                    string filter = "Menu_Type = '" + drInner["Menu_Code"].ToString() + "'";

                    ds.Tables["Menu"].AcceptChanges();
                    DataRow[] drInnerLinks = ds.Tables["Menu"].Select(filter);

                    if (drInnerLinks.Length != 0)
                    {
                        foreach (DataRow drInnerLink in drInnerLinks)
                        {
                            TreeNode milink = new TreeNode(drInnerLink["Menu_Short_Discription"].ToString().Trim(), drInnerLink["menu_type"].ToString() + "_" + drInnerLink["Menu_Code"].ToString());
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

    protected void Load_UserList()
    {
        int CompanyID = UDFLib.ConvertToInteger(lstCompany.SelectedValue);
        if (CompanyID == 0)
        {
            if (Session["USERCOMPANYID"] != null)
                CompanyID = int.Parse(Session["USERCOMPANYID"].ToString());
            // CompanyID = 1; 
        }

        DataTable dt = objBllUser.Get_UserList(CompanyID, "", Convert.ToInt32(Session["USERID"].ToString()));
        ddlCopyFromUser.DataSource = dt;
        ddlCopyFromUser.DataBind();

    }

    protected void btnInitializeMenu_Click(object sender, EventArgs e)
    {
        //if (lstUserList.SelectedValue == "" || lstModuleList.SelectedValue == "")
        //return;

        //int iMenu = 0;
        //int iView = 0;
        //int iAdd = 0;
        //int iEdit = 0;
        //int iDelete = 0;
        //int iApprove = 0;
        //int iMod_Code = 0;

        //iMod_Code = int.Parse(lstModuleList.SelectedValue);

        //objMenuBLL.Initialize_User_Menu(int.Parse(lstUserList.SelectedValue), iMod_Code, iMenu, iView, iAdd, iEdit, iDelete, iApprove, int.Parse(getSessionString("USERID")));

        //GridView1.DataBind();
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        Load_UserMenu();
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
      
       
        //int iCopyToUser = int.Parse(lstUserList.SelectedValue);
        int iAppendMode = int.Parse(ddlAppendMode.SelectedValue);
        int iCopyMenu = int.Parse(ddlCopyMenu.SelectedValue);
        int i = 0;
        int index = lstUserList.SelectedIndex;
        #region menu
        if (rdbCopy.SelectedIndex == 0)
        {
            int iCopyFromUser = int.Parse(ddlCopyFromUser.SelectedValue);

            foreach (DataRow dr in DDLUser.SelectedValues.Rows)
            {
                if (iCopyFromUser != Convert.ToInt32(dr[0]))
                {
                    i++;
                    int Selected_Mod_Code = 0;
                    if (iCopyMenu == 1)
                    {
                        if (TreeView1.SelectedNode != null)
                        {

                            string[] NodeValue = TreeView1.SelectedNode.Value.Split('_');
                            Selected_Mod_Code = UDFLib.ConvertToInteger(NodeValue[1]);

                            {
                                objMenuBLL.Copy_MenuAccessFromUser(iCopyFromUser, Convert.ToInt32(dr[0]), iAppendMode, Selected_Mod_Code, GetSessionUserID());
                                GridView1.DataBind();
                                Load_UserMenu();

                                string js1 = "alert('Selected Module/Sub-Module menu access copied')";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);
                            }

                        }
                        else
                        {
                            string js = "alert('Select Module/Sub-Module from Menu Tree')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);

                        }
                    }

                    else
                    {
                        objMenuBLL.Copy_MenuAccessFromUser(iCopyFromUser, Convert.ToInt32(dr[0]), iAppendMode, 0, GetSessionUserID());
                        Load_UserMenu();
                        string js2 = "alert('All menu access copied')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js2, true);
                    }
                }
            }
            if (i > 1)
            {
                //Response.Redirect(Request.RawUrl);
                GridView1.DataBind();
                Load_UserMenu();
                lstUserList.SelectedIndex = index;
                foreach (TreeNode n1 in Collect(TreeView1.Nodes))
                {

                    if (n1.Value.Split('_')[1].ToString() == ViewState["modId"].ToString())
                    {

                        n1.Selected = true;
                        break;
                    }
                }
            }
        }
        #endregion

        #region role

        else
        {
            int iCopyFromRole = int.Parse(ddlCopyFromRole.SelectedValue);

            foreach (DataRow dr in DDLUser.SelectedValues.Rows)
            {


                int Selected_Mod_Code = 0;
                if (iCopyMenu == 1)
                {
                    if (TreeView1.SelectedNode != null)
                    {

                        string[] NodeValue = TreeView1.SelectedNode.Value.Split('_');
                        Selected_Mod_Code = UDFLib.ConvertToInteger(NodeValue[1]);

                        {
                            objMenuBLL.Copy_MenuAccessFromRole(iCopyFromRole, Convert.ToInt32(dr[0]), iAppendMode, Selected_Mod_Code, GetSessionUserID());
                            GridView1.DataBind();
                            Load_UserMenu();

                            string js1 = "alert('Selected Module/Sub-Module menu access copied')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);
                        }

                    }
                    else
                    {
                        string js = "alert('Select Module/Sub-Module from Menu Tree')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);

                    }
                }

                else
                {
                    objMenuBLL.Copy_MenuAccessFromRole(iCopyFromRole, Convert.ToInt32(dr[0]), iAppendMode, 0, GetSessionUserID());
                    Load_UserMenu();
                    string js2 = "alert('All menu access copied')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js2, true);
                }
            }


        }
        #endregion
    }

    protected void btnResetMenu_Click(object sender, EventArgs e)
    {
        int iUserID = 0;
        foreach (ListItem liUser in lstUserList.Items)
        {
            if (liUser.Selected == true)
            {
                iUserID = int.Parse(liUser.Value);
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {


                        ((CheckBox)row.FindControl("chkAll")).Checked = false;
                    ((CheckBox)row.FindControl("Access_Menu")).Checked = false;
                      ((CheckBox)row.FindControl("Access_View")).Checked = false;
                       ((CheckBox)row.FindControl("Access_Add")).Checked = false;
                       ((CheckBox)row.FindControl("Access_Edit")).Checked = false;
                      ((CheckBox)row.FindControl("Access_Delete")).Checked = false;
                       ((CheckBox)row.FindControl("Access_Approve")).Checked = false;
                      ((CheckBox)row.FindControl("Access_Admin")).Checked = false;
                       ((CheckBox)row.FindControl("Unverify")).Checked = false;
                       ((CheckBox)row.FindControl("Revoke")).Checked = false;
                       ((CheckBox)row.FindControl("Urgent")).Checked = false;
                       ((CheckBox)row.FindControl("Close")).Checked = false;
                       ((CheckBox)row.FindControl("Unclose")).Checked = false;

                    }
                }

                btnSave_Click(null, null);
               

                int iSessionUserID = GetSessionUserID();
       
                if (iUserID == iSessionUserID)
                {
                    TreeView1.Nodes.Clear();
                    SqlConnection sCon = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
                    SqlDataAdapter sqlAdp = new SqlDataAdapter("SP_INF_MNU_Get_MenuLibAccess", sCon);
                    sqlAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlAdp.SelectCommand.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int));

                    sqlAdp.SelectCommand.Parameters["@userid"].Value = Convert.ToInt32(Session["USERID"].ToString());
                    DataSet ds = new DataSet();
                    sqlAdp.Fill(ds, "Menu");
                    GenerateUL(ds);


                    foreach (TreeNode n1 in Collect(TreeView1.Nodes))
                    {

                        if (n1.Value.Split('_')[1].ToString() == ViewState["modId"].ToString())
                        {

                            n1.Selected = true;
                            break;
                        }
                    }
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int iMenu = 1;
           
            int iView = 1;
            int iAdd = 1;
            int iEdit = 1;
            int iDelete = 1;
            int iApprove = 1;
            int iAdmin = 1;
            int iUnverify = 1;
            int iRevoke = 1;
            int iUrgent = 1;
            int iClose = 1;
            int iUnclose = 1;
            int iMenu_Code = 0;
            int iAll = 1;
            int iUserID = 0;
           
            int iSessionUserID = GetSessionUserID();
       

            foreach (ListItem liUser in lstUserList.Items)
            {
                if (liUser.Selected == true)
                {
                    iUserID = int.Parse(liUser.Value);
                    iMenu_Code =Convert.ToInt32( ViewState["modId"].ToString());
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {

                            iMenu_Code = UDFLib.ConvertToInteger(GridView1.DataKeys[row.RowIndex].Value.ToString());
                            iAll = (((CheckBox)row.FindControl("chkAll")).Checked == true) ? 1 : 0;

                            
                            {
                                iMenu = (((CheckBox)row.FindControl("Access_View")).Checked == true) ? 1 : 0;
                                iView = (((CheckBox)row.FindControl("Access_View")).Checked == true) ? 1 : 0;
                                iAdd = (((CheckBox)row.FindControl("Access_Add")).Checked == true) ? 1 : 0;
                                iEdit = (((CheckBox)row.FindControl("Access_Edit")).Checked == true) ? 1 : 0;
                                iDelete = (((CheckBox)row.FindControl("Access_Delete")).Checked == true) ? 1 : 0;
                                iApprove = (((CheckBox)row.FindControl("Access_Approve")).Checked == true) ? 1 : 0;
                                iAdmin = (((CheckBox)row.FindControl("Access_Admin")).Checked == true) ? 1 : 0;
                                iUnverify = (((CheckBox)row.FindControl("Unverify")).Checked == true) ? 1 : 0;
                                iRevoke= (((CheckBox)row.FindControl("Revoke")).Checked == true) ? 1 : 0;
                                iUrgent = (((CheckBox)row.FindControl("Urgent")).Checked == true) ? 1 : 0;
                                iClose = (((CheckBox)row.FindControl("Close")).Checked == true) ? 1 : 0;
                                iUnclose = (((CheckBox)row.FindControl("Unclose")).Checked == true) ? 1 : 0;
                              
                            }

                            if (iMenu_Code != 0)
                            {
                                objMenuBLL.Update_User_Menu_Access_DL(iUserID, iMenu_Code, iMenu, iView, iAdd, iEdit, iDelete, iApprove, iAdmin, iUnverify, iRevoke, iUnclose, iUrgent, iClose, iSessionUserID);
                            //    Load_UserMenu();
                            }
                        }
                    }
                }
            }
            Load_UserMenu();
            if (iUserID == iSessionUserID && iView==0)
            {
                TreeView1.Nodes.Clear();
                SqlConnection sCon = new SqlConnection(ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString);
                SqlDataAdapter sqlAdp = new SqlDataAdapter("SP_INF_MNU_Get_MenuLibAccess", sCon);
                sqlAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlAdp.SelectCommand.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int));

                sqlAdp.SelectCommand.Parameters["@userid"].Value = Convert.ToInt32(Session["USERID"].ToString());
                DataSet ds = new DataSet();
                sqlAdp.Fill(ds, "Menu");
                GenerateUL(ds);


                foreach (TreeNode n1 in Collect(TreeView1.Nodes))
                {

                    if (n1.Value.Split('_')[1].ToString() == ViewState["modId"].ToString())
                    {

                        n1.Selected = true;
                        break;
                    }
                }
            }

         
        }
        catch { }
    }

    IEnumerable<TreeNode> Collect(TreeNodeCollection nodes)
    {
        foreach (TreeNode node in nodes)
        {
            yield return node;

            foreach (var child in Collect(node.ChildNodes))
                yield return child;
        }
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        //if (GridView1.Rows.Count > 0)
        //{
        //    btnResetMenu.Visible = true;
        //}
        //else
        //{
        //    btnResetMenu.Visible = False;
        //}

        
    }

    protected void Load_UserMenu()
    {

        if (TreeView1.SelectedNode != null)
        {
            int userid = 0;

            if (lstUserList.SelectedItem != null)
                userid = UDFLib.ConvertToInteger(lstUserList.SelectedValue);

            string[] NodeValue = TreeView1.SelectedNode.Value.Split('_');


            int mod_code = UDFLib.ConvertToInteger(NodeValue[1]);
            ViewState["modId"] = mod_code;


            DataTable dt = objMenuBLL.Get_UserMenuApproach(mod_code, userid, Convert.ToInt32(Session["USERID"].ToString())).Tables[0];
            DataTable dtable = objMenuBLL.Get_UserMenuApproach(mod_code, userid, Convert.ToInt32(Session["USERID"].ToString())).Tables[1];
            ViewState["UserType"] = dtable.Rows[0]["User_Type"].ToString();
            //DataView dv = new DataView(dt);
            // dv.Sort = "Sequence_Order,Menu_Type, Mod_Code";
            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (dt.Rows.Count > 0)
            {
                btnResetMenu.Visible = true;
                btnSave.Visible = true;
            }
            else
            {
                btnResetMenu.Visible = false;
                btnSave.Visible = false;
            }



        }

    }


    //protected void Load_UserMenu()
    //{
    //    DataTable dtbl = new DataTable();

    //    if (TreeView1.SelectedNode != null)
    //    {
    //        int userid = 0;

    //        if (lstUserList.SelectedItem != null)
    //            userid = UDFLib.ConvertToInteger(lstUserList.SelectedValue);

    //        string[] NodeValue = TreeView1.SelectedNode.Value.Split('_');


    //        int mod_code = UDFLib.ConvertToInteger(NodeValue[1]);
    //        ViewState["modId"] = mod_code;


    //        DataSet ds = objMenuBLL.Get_UserMenuApproach(mod_code, userid, Convert.ToInt32(Session["USERID"].ToString()));
    //        dtbl = ds.Tables["usdnme"].Clone();
    //        DataRow[] drs = ds.Tables["usdnme"].Select("Menu_Type is null");

    //        if (drs.Length != 0)
    //        {
    //            int meni = 0;
    //            int child = 0;

    //            foreach (DataRow dr in drs)
    //            {

    //                //// add
    //                if (!ContainDataRowInDataTable(dtbl, dr))
    //                {
    //                    dtbl.ImportRow(dr);
    //                }
    //                DataRow[] drInners = ds.Tables[0].Select("Menu_Type ='" + dr["Menu_Code"].ToString() + "' ");
    //                if (drInners.Length != 0)
    //                {
    //                    child = 0;

    //                    foreach (DataRow drInner in drInners)
    //                    {
    //                        ////   add
    //                        if (!ContainDataRowInDataTable(dtbl, dr))
    //                        {
    //                            dtbl.ImportRow(drInner);
    //                        }

    //                        string filter = "Menu_Type = '" + drInner["Menu_Code"].ToString() + "'";

    //                        ds.Tables[0].AcceptChanges();
    //                        DataRow[] drInnerLinks = ds.Tables[0].Select(filter);

    //                        if (drInnerLinks.Length != 0)
    //                        {
    //                            foreach (DataRow drInnerLink in drInnerLinks)
    //                            {
    //                                //// add
    //                                if (!ContainDataRowInDataTable(dtbl, dr))
    //                                {
    //                                    dtbl.ImportRow(drInnerLink);
    //                                }
    //                            }
    //                        }
    //                        child++;
    //                    }
    //                }
    //                meni++;
    //            }
    //        }
    //        //else
    //        //{

    //        DataRow[] drs2 = ds.Tables["usdnme"].Select("Menu_Type is not null");

    //        int meni2 = 0;
    //        int child2 = 0;

    //        foreach (DataRow dr in drs2)
    //        {

    //            ////   add
    //            if (!ContainDataRowInDataTable(dtbl, dr))
    //            {
    //                dtbl.ImportRow(dr);
    //            }
    //            DataRow[] drInners = ds.Tables[0].Select("Menu_Type ='" + dr["Menu_Code"].ToString() + "' ");
    //            if (drInners.Length != 0)
    //            {
    //                child2 = 0;

    //                foreach (DataRow drInner in drInners)
    //                {
    //                    //// add
    //                    if (!ContainDataRowInDataTable(dtbl, dr))
    //                    {
    //                        dtbl.ImportRow(drInner);
    //                    }
    //                    string filter = "Menu_Type = '" + drInner["Menu_Code"].ToString() + "'";

    //                    ds.Tables[0].AcceptChanges();
    //                    DataRow[] drInnerLinks = ds.Tables[0].Select(filter);

    //                    if (drInnerLinks.Length != 0)
    //                    {
    //                        foreach (DataRow drInnerLink in drInnerLinks)
    //                        {
    //                            ////  add
    //                            if (!ContainDataRowInDataTable(dtbl, dr))
    //                            {
    //                                dtbl.ImportRow(drInnerLink);
    //                            }
    //                        }
    //                    }
    //                    child2++;
    //                }
    //            }
    //            meni2++;
    //        }
    //    }
    //    DataView dv = new DataView(dtbl);
    //    //dv.Sort = "Mod_Code,Menu_Type,Menu_Code";
    //    dv.Sort = "Menu_Code";
    //    GridView1.DataSource = dv;
    //    GridView1.DataBind();
    //    if (dtbl.Rows.Count > 0)
    //    {
    //        btnResetMenu.Visible = true;
    //        btnSave.Visible = true;
    //    }
    //    else
    //    {
    //        btnResetMenu.Visible = false;
    //        btnSave.Visible = false;
    //    }
    //    //}

    //}

    bool ContainDataRowInDataTable(DataTable T, DataRow R)
    {
        foreach (DataRow item in T.Rows)
        {
            if (Enumerable.SequenceEqual(item.ItemArray, R.ItemArray))
                return true;
        }
        return false;
    }

    protected void lstUserList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_UserMenu();
    }

    protected void lstCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_UserList();
        Load_UserMenu();
        BindUserDLL();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Load_UserMenu();
    }

    protected void lstCompany_DataBound(object sender, EventArgs e)
    {
        if (lstCompany.Items.Count > 0)
        {
            try
            {
                if (Session["USERCOMPANYID"] != null)
                    lstCompany.Text = Session["USERCOMPANYID"].ToString();
                //lstCompany.Text = "1";
            }
            catch
            { }
        }
    }

    public void BindUserDLL()
    {
        try
        {
            int CompanyID = UDFLib.ConvertToInteger(lstCompany.SelectedValue);
            if (CompanyID == 0)
            {
                if (Session["USERCOMPANYID"] != null)
                    CompanyID = int.Parse(Session["USERCOMPANYID"].ToString());
                // CompanyID = 1; 
            }
            DDLUser.DataSource = objBllUser.Get_UserList(CompanyID, "", Convert.ToInt32(Session["USERID"].ToString()));
            DDLUser.DataTextField = "USERNAME";
            DDLUser.DataValueField = "USERID";
            DDLUser.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label Menu_Link = (Label)e.Row.FindControl("lblLink");
                if (Menu_Link.Text.ToLower() == "infrastructure/dashboard_common.aspx" || Menu_Link.Text.ToLower() == "infrastructure/dashboard.aspx")
                {
                


                    CheckBox checkView = (CheckBox)e.Row.FindControl("Access_View");
                    CheckBox checkMenu = (CheckBox)e.Row.FindControl("Access_Menu");
                    CheckBox checkAdd = (CheckBox)e.Row.FindControl("Access_Add");
                    CheckBox checkEdit = (CheckBox)e.Row.FindControl("Access_Edit");
                    CheckBox checkDelete = (CheckBox)e.Row.FindControl("Access_Delete");
                    CheckBox checkApprove = (CheckBox)e.Row.FindControl("Access_Approve");
                    CheckBox checkAdmin = (CheckBox)e.Row.FindControl("Access_Admin");
                    CheckBox checkUnverify = (CheckBox)e.Row.FindControl("Unverify");
                    CheckBox checkRevoke = (CheckBox)e.Row.FindControl("Revoke");
                    CheckBox checkUrgent = (CheckBox)e.Row.FindControl("Urgent");
                    CheckBox checkClose = (CheckBox)e.Row.FindControl("Close");
                    CheckBox checkUnclose = (CheckBox)e.Row.FindControl("Unclose");


                    //checkView.Checked = true;
                    //checkView.Enabled = false;

                    //checkMenu.Checked = true;
                    //checkMenu.Enabled = false;
                    //checkAdd.Checked = true;
                    //checkAdd.Enabled = false;
                    //checkEdit.Checked = true;
                    //checkEdit.Enabled = false;
                    //checkDelete.Checked = true;
                    //checkDelete.Enabled = false;
                    //checkApprove.Checked = true;
                    //checkApprove.Enabled = false;
                    //checkAdmin.Checked = true;
                    //checkAdmin.Enabled = false;
                    //checkUnverify.Checked = true;
                    //checkUnverify.Enabled = false;
                    //checkRevoke.Checked = true;
                    //checkRevoke.Enabled = false;
                    //checkUrgent.Checked = true;
                    //checkUrgent.Enabled = false;
                    //checkClose.Checked = true;
                    //checkClose.Enabled = false;
                    //checkUnclose.Checked = true;
                    //checkUnclose.Enabled = false;
                }
                else
                {
                  

                    Label Menu_Id = (Label)e.Row.FindControl("Menu_Id");
                    DataTable dt = objMenuBLL.Get_MenuAccess(null, Convert.ToInt32(Menu_Id.Text));

                    #region loop
                    for (Int32 i = 0; i < dt.Rows.Count; i++)
                    {

                        if (dt.Rows[i]["Key_Name"].ToString() == "Access_View")
                        {
                            CheckBox check = (CheckBox)e.Row.FindControl("Access_View");
                            CheckBox checkMenu = (CheckBox)e.Row.FindControl("Access_Menu");
                            if (Convert.ToBoolean(dt.Rows[i]["Key_Enabled"]) == true)
                            {

                                check.Enabled = true;
                                checkMenu.Enabled = true;

                            }
                            else
                            {
                                check.Enabled = false;
                                checkMenu.Enabled = false;
                                // check.Checked = false;
                            }
                            check.Text = dt.Rows[i]["Description"].ToString();
                        }
                        if (dt.Rows[i]["Key_Name"].ToString() == "Access_Add")
                        {
                            CheckBox check = (CheckBox)e.Row.FindControl("Access_Add");
                            if (Convert.ToBoolean(dt.Rows[i]["Key_Enabled"]) == true)
                            {

                                check.Enabled = true;

                            }
                            else
                            {
                                check.Enabled = false;
                                //  check.Checked = false;
                            }
                            check.Text = dt.Rows[i]["Description"].ToString();
                        }
                        if (dt.Rows[i]["Key_Name"].ToString() == "Access_Edit")
                        {
                            CheckBox check = (CheckBox)e.Row.FindControl("Access_Edit");
                            if (Convert.ToBoolean(dt.Rows[i]["Key_Enabled"]) == true)
                            {

                                check.Enabled = true;

                            }
                            else
                            {
                                check.Enabled = false;
                                //  check.Checked = false;
                            }
                            check.Text = dt.Rows[i]["Description"].ToString();
                        }
                        if (dt.Rows[i]["Key_Name"].ToString() == "Access_Delete")
                        {
                            CheckBox check = (CheckBox)e.Row.FindControl("Access_Delete");
                            if (Convert.ToBoolean(dt.Rows[i]["Key_Enabled"]) == true)
                            {

                                check.Enabled = true;

                            }
                            else
                            {
                                check.Enabled = false;
                                //  check.Checked = false;
                            }
                            check.Text = dt.Rows[i]["Description"].ToString();
                        }
                        if (dt.Rows[i]["Key_Name"].ToString() == "Access_Approve")
                        {
                            CheckBox check = (CheckBox)e.Row.FindControl("Access_Approve");
                            if (Convert.ToBoolean(dt.Rows[i]["Key_Enabled"]) == true)
                            {

                                check.Enabled = true;
                            }
                            else
                            {
                                check.Enabled = false;
                                // check.Checked = false;
                            }
                            check.Text = dt.Rows[i]["Description"].ToString();
                        }
                        if (dt.Rows[i]["Key_Name"].ToString() == "Access_Admin")
                        {

                            CheckBox check = (CheckBox)e.Row.FindControl("Access_Admin");
                            if (Convert.ToBoolean(dt.Rows[i]["Key_Enabled"]) == true)
                            {

                                check.Enabled = true;

                            }
                            else
                            {
                                check.Enabled = false;
                                //   check.Checked = false;
                            }
                            check.Text = dt.Rows[i]["Description"].ToString();
                            if (ViewState["UserType"].ToString().ToLower() != "admin")
                            {
                                check.Enabled = false;
                            }
                        }

                        if (dt.Rows[i]["Key_Name"].ToString() == "Unverify")
                        {
                            CheckBox check = (CheckBox)e.Row.FindControl("Unverify");
                            if (Convert.ToBoolean(dt.Rows[i]["Key_Enabled"]) == true)
                            {

                                check.Enabled = true;

                            }
                            else
                            {
                                check.Enabled = false;
                                // check.Checked = false;
                            }
                            check.Text = dt.Rows[i]["Description"].ToString();
                        }
                        if (dt.Rows[i]["Key_Name"].ToString() == "Revoke")
                        {
                            CheckBox check = (CheckBox)e.Row.FindControl("Revoke");
                            if (Convert.ToBoolean(dt.Rows[i]["Key_Enabled"]) == true)
                            {

                                check.Enabled = true;

                            }
                            else
                            {
                                check.Enabled = false;
                                //   check.Checked = false;
                            }
                            check.Text = dt.Rows[i]["Description"].ToString();
                        }
                        if (dt.Rows[i]["Key_Name"].ToString() == "Urgent")
                        {
                            CheckBox check = (CheckBox)e.Row.FindControl("Urgent");
                            if (Convert.ToBoolean(dt.Rows[i]["Key_Enabled"]) == true)
                            {

                                check.Enabled = true;

                            }
                            else
                            {
                                check.Enabled = false;
                                //  check.Checked = false;
                            }
                            check.Text = dt.Rows[i]["Description"].ToString();
                        }
                        if (dt.Rows[i]["Key_Name"].ToString() == "Close")
                        {
                            CheckBox check = (CheckBox)e.Row.FindControl("Close");
                            if (Convert.ToBoolean(dt.Rows[i]["Key_Enabled"]) == true)
                            {

                                check.Enabled = true;

                            }
                            else
                            {
                                check.Enabled = false;
                                //    check.Checked = false;
                            }
                            check.Text = dt.Rows[i]["Description"].ToString();
                        }
                        if (dt.Rows[i]["Key_Name"].ToString() == "Unclose")
                        {
                            CheckBox check = (CheckBox)e.Row.FindControl("Unclose");
                            if (Convert.ToBoolean(dt.Rows[i]["Key_Enabled"]) == true)
                            {

                                check.Enabled = true;

                            }
                            else
                            {
                                check.Enabled = false;
                                //   check.Checked = false;
                            }
                            check.Text = dt.Rows[i]["Description"].ToString();
                        }
                    }
                    #endregion
                }


            }
        }
        catch
        {
        }
    }

 


    protected void rdbCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbCopy.SelectedIndex == 0)
        {
            ddlCopyFromUser.Visible = true;
            ddlCopyFromRole.Visible = false;
            ddlAppendMode.Enabled = true;
            ddlCopyMenu.Enabled = true;
            if (ddlCopyFromUser.Items.Count == 0)
            {
                btnCopy.Enabled = false;
            }
            else
            {
                btnCopy.Enabled = true;
            }
        }
        else
        {
            ddlCopyFromRole.Visible = true;
            ddlCopyFromUser.Visible = false;
            ddlAppendMode.SelectedIndex = 0;
            ddlAppendMode.Enabled = false;
            ddlCopyMenu.SelectedIndex = 0;
            ddlCopyMenu.Enabled = false;

            if (ddlCopyFromRole.Items.Count == 0)
            {
                btnCopy.Enabled = false;
            }
            else
            {
                btnCopy.Enabled = true;
            }
        }
    }
}