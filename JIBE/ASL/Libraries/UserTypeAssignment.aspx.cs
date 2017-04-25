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
using SMS.Business.ASL;

public partial class ASL_Libraries_UserTypeAssignment : System.Web.UI.Page
{
    BLL_Infra_MenuManagement objMenuBLL = new BLL_Infra_MenuManagement();
    BLL_Infra_UserCredentials objBllUser = new BLL_Infra_UserCredentials();
    BLL_TypeManagement objTypeBLL = new BLL_TypeManagement();
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
                    SqlDataAdapter sqlAdp = new SqlDataAdapter("ASL_Get_System_Variable_Type", sCon);
                    sqlAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //sqlAdp.SelectCommand.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int));

                    //sqlAdp.SelectCommand.Parameters["@userid"].Value = 1;
                    DataSet ds = new DataSet();
                    sqlAdp.Fill(ds, "Menu");
                    GenerateUL(ds);

                    Load_UserList();
                    BindUserDLL();
                }

            }

        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.Message + "')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
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

    private void GenerateUL(DataSet ds)
    {

        DataRow[] drs = ds.Tables["Menu"].Select("Menu_Type is null");
        int meni = 0;
        int child = 0;

        foreach (DataRow dr in drs)
        {
            TreeNode mi = new TreeNode(dr["VARIABLETYPE"].ToString().Trim(), dr["VARIABLE_TYPE"].ToString());
            TreeView1.Nodes.Add(mi);
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

        DataTable dt = objBllUser.Get_UserList(CompanyID, "");
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
        int iCopyFromUser = int.Parse(ddlCopyFromUser.SelectedValue);
        //int iCopyToUser = int.Parse(lstUserList.SelectedValue);
        int iAppendMode = int.Parse(ddlAppendMode.SelectedValue);
        int iCopyMenu = int.Parse(ddlCopyMenu.SelectedValue);

        foreach (DataRow dr in DDLUser.SelectedValues.Rows)
        {
            string Selected_Variable_type = null;
            if (iCopyMenu == 1)
            {
                if (TreeView1.SelectedNode != null)
                {
                    string[] NodeValue = TreeView1.SelectedNode.Value.Split(',');
                    Selected_Variable_type = UDFLib.ConvertStringToNull(NodeValue[0]);

                    objTypeBLL.Copy_TypeAccessFromUser(iCopyFromUser, Convert.ToInt16(dr[0]), iAppendMode, Selected_Variable_type, GetSessionUserID());
                    GridView1.DataBind();
                    Load_UserMenu();

                    string js1 = "alert('Selected Type access copied')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);
                }
                else
                {
                    string js = "alert('Select Type')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);

                }
            }
            else
            {
                objTypeBLL.Copy_TypeAccessFromUser(iCopyFromUser, Convert.ToInt32(dr[0]), iAppendMode,null, GetSessionUserID());
                Load_UserMenu();
                string js2 = "alert('All Type access copied')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js2, true);
            }
        }
    }

    protected void btnResetMenu_Click(object sender, EventArgs e)
    {
        foreach (ListItem liUser in lstUserList.Items)
        {
            if (liUser.Selected == true)
            {

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        ((CheckBox)row.FindControl("chkAll")).Checked = false;
                        ((CheckBox)row.FindControl("chkView")).Checked = false;
                        ((CheckBox)row.FindControl("chkAdd")).Checked = false;
                        ((CheckBox)row.FindControl("chkEdit")).Checked = false;
                        ((CheckBox)row.FindControl("chkDelete")).Checked = false;
                        ((CheckBox)row.FindControl("chkApprove")).Checked = false;
                        ((CheckBox)row.FindControl("chkAdmin")).Checked = false;
                    }
                }

                btnSave_Click(null, null);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int iAccess = 1;
            int iView = 1;
            int iAdd = 1;
            int iEdit = 1;
            int iDelete = 1;
            int iApprove = 1;
            int iAll = 0;
            int iAdmin = 0;
            string iVCode = "0";
            int iUserID = 0;
            int iSessionUserID = GetSessionUserID();
            foreach (ListItem liUser in lstUserList.Items)
            {
                if (liUser.Selected == true)
                {
                    iUserID = int.Parse(liUser.Value);
                    string[] NodeValue = TreeView1.SelectedNode.Value.Split(',');
                    string variable_Type = UDFLib.ConvertStringToNull(NodeValue[0]);
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {

                            iVCode = UDFLib.ConvertStringToNull(GridView1.DataKeys[row.RowIndex].Value.ToString());
                            iAll = (((CheckBox)row.FindControl("chkAll")).Checked == true) ? 1 : 0;
                            //iAccess = (((CheckBox)row.FindControl("chkAccess")).Checked == true) ? 1 : 0;
                            if (iAll == 1)
                            {
                                iView = 1;
                                iAdd = 1;
                                iEdit = 1;
                                iDelete = 1;
                                iApprove = 1;
                                iAdmin = 1;
                            }
                            else
                            {
                                iView = (((CheckBox)row.FindControl("chkView")).Checked == true) ? 1 : 0;
                                iAdd = (((CheckBox)row.FindControl("chkAdd")).Checked == true) ? 1 : 0;
                                iEdit = (((CheckBox)row.FindControl("chkEdit")).Checked == true) ? 1 : 0;
                                iDelete = (((CheckBox)row.FindControl("chkDelete")).Checked == true) ? 1 : 0;
                                iApprove = (((CheckBox)row.FindControl("chkApprove")).Checked == true) ? 1 : 0;
                                iAdmin = (((CheckBox)row.FindControl("chkAdmin")).Checked == true) ? 1 : 0;
                            }
                            if (iVCode != "0")
                            {
                                objTypeBLL.Update_User_Type_Access(iUserID, iVCode, iView,iAdd,iEdit,iDelete,iApprove,iAdmin, variable_Type, iSessionUserID);
                                Load_UserMenu();

                                //string js1 = "alert('Selected Type access copied')";
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);
                            }
                        }
                    }
                }
            }



        }
        catch { }
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            btnResetMenu.Visible = true;
        }
        else
        {
            btnResetMenu.Visible = false;
        }
    }

    protected void Load_UserMenu()
    {

        if (TreeView1.SelectedNode != null)
        {
            string[] NodeValue = TreeView1.SelectedNode.Value.Split(',');
            string variable_Type = UDFLib.ConvertStringToNull(NodeValue[0]);
            int userid = 0;
            //int mod_code = UDFLib.ConvertToInteger(NodeValue[1]);

            if (lstUserList.SelectedItem != null)
                userid = UDFLib.ConvertToInteger(lstUserList.SelectedValue);

            DataTable dt = objTypeBLL.Get_UserTypeAccess(variable_Type, userid, Convert.ToInt32(Session["USERID"].ToString()));
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

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
            DDLUser.DataSource = objBllUser.Get_UserList(CompanyID, "");
            DDLUser.DataTextField = "USERNAME";
            DDLUser.DataValueField = "USERID";
            DDLUser.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
}