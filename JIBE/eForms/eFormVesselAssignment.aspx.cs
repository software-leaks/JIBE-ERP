using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.eForms;
using SMS.Business.Infrastructure;
using System.Configuration;

public partial class eForms_eFormVesselAssignment : System.Web.UI.Page
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
                Load_eFormList();
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


    protected void Load_eFormList()
    {

        DataTable dt = BLL_eForms_Admin.Get_Form_Library();
        lsteFormList.DataSource = dt;
        lsteFormList.DataBind();

    }



    protected void eFormVesselAssignmentList()
    {

        int eFormID = UDFLib.ConvertToInteger(lsteFormList.SelectedValue);
        if (eFormID == 0)
            eFormID = 1;

        DataTable dt = BLL_eForms_Admin.Get_eFormAssign_Vessel_List(eFormID);

        GridView1.DataSource = dt;
        GridView1.DataBind();

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

    //protected void btnCopy_Click(object sender, EventArgs e)
    //{
    //    int iCopyFromUser = int.Parse(ddlCopyFromUser.SelectedValue);
    //    int iCopyToUser = int.Parse(lstUserList.SelectedValue);
    //    int iAppendMode = int.Parse(ddlAppendMode.SelectedValue);
    //    int iCopyMenu = int.Parse(ddlCopyMenu.SelectedValue);

    //    int Selected_Mod_Code = 0;
    //    if (iCopyMenu == 1)
    //    {
    //        if (TreeView1.SelectedNode != null)
    //        {
    //            string[] NodeValue = TreeView1.SelectedNode.Value.Split('_');
    //            Selected_Mod_Code = UDFLib.ConvertToInteger(NodeValue[1]);

    //            objMenuBLL.Copy_MenuAccessFromUser(iCopyFromUser, iCopyToUser, iAppendMode, Selected_Mod_Code, GetSessionUserID());
    //            GridView1.DataBind();
    //            Load_UserMenu();

    //            string js1 = "alert('Selected Module/Sub-Module menu access copied')";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);
    //        }
    //        else
    //        {
    //            string js = "alert('Select Module/Sub-Module from Menu Tree')";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);

    //        }
    //    }
    //    else
    //    {
    //        objMenuBLL.Copy_MenuAccessFromUser(iCopyFromUser, iCopyToUser, iAppendMode,0, GetSessionUserID());
    //        Load_UserMenu();
    //        string js2 = "alert('All menu access copied')";
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js2, true);
    //    }
    //}

    //protected void btnResetMenu_Click(object sender, EventArgs e)
    //{
    //    foreach (ListItem liUser in lsteFormList.Items)
    //    {
    //        if (liUser.Selected == true)
    //        {

    //            foreach (GridViewRow row in GridView1.Rows)
    //            {
    //                if (row.RowType == DataControlRowType.DataRow)
    //                {

    //                    ((CheckBox)row.FindControl("chkMenu")).Checked = false;
    //                    ((CheckBox)row.FindControl("chkView")).Checked = false;
    //                    ((CheckBox)row.FindControl("chkAdd")).Checked = false;
    //                    ((CheckBox)row.FindControl("chkEdit")).Checked = false;
    //                    ((CheckBox)row.FindControl("chkDelete")).Checked = false;
    //                    ((CheckBox)row.FindControl("chkApprove")).Checked = false;
    //                }
    //            }

    //            btnAssigneForm(null, null);
    //        }
    //    }
    //}

    protected void btnAssigneForm_Click(object sender, EventArgs e)
    {
        try
        {
            int iMenu = 1;
            int iView = 1;
            int iAdd = 1;
            int iEdit = 1;
            int iDelete = 1;
            int iApprove = 1;
            int iMenu_Code = 0;
            int iAll = 0;
            int iUserID = 0;
            int iSessionUserID = GetSessionUserID();


            foreach (ListItem liUser in lsteFormList.Items)
            {
                if (liUser.Selected == true)
                {
                    iUserID = int.Parse(liUser.Value);

                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {

                            iMenu_Code = UDFLib.ConvertToInteger(GridView1.DataKeys[row.RowIndex].Value.ToString());
                            iAll = (((CheckBox)row.FindControl("chkAll")).Checked == true) ? 1 : 0;

                            if (iAll == 1)
                            {
                                iMenu = 1;
                                iView = 1;
                                iAdd = 1;
                                iEdit = 1;
                                iDelete = 1;
                                iApprove = 1;
                            }
                            else
                            {
                                iMenu = (((CheckBox)row.FindControl("chkMenu")).Checked == true) ? 1 : 0;
                                iView = (((CheckBox)row.FindControl("chkView")).Checked == true) ? 1 : 0;
                                iAdd = (((CheckBox)row.FindControl("chkAdd")).Checked == true) ? 1 : 0;
                                iEdit = (((CheckBox)row.FindControl("chkEdit")).Checked == true) ? 1 : 0;
                                iDelete = (((CheckBox)row.FindControl("chkDelete")).Checked == true) ? 1 : 0;
                                iApprove = (((CheckBox)row.FindControl("chkApprove")).Checked == true) ? 1 : 0;
                            }

                            if (iMenu_Code != 0)
                            {
                                objMenuBLL.Update_User_Menu_Access(iUserID, iMenu_Code, iMenu, iView, iAdd, iEdit, iDelete, iApprove,0, iSessionUserID);

                                Load_UserMenu();
                            }
                        }
                    }
                }
            }



        }
        catch { }
    }



    protected void Load_UserMenu()
    {

        int eFormid = 0;

        if (lsteFormList.SelectedItem != null)
            eFormid = UDFLib.ConvertToInteger(lsteFormList.SelectedValue);

        //DataTable dt = objMenuBLL.Get_UserMenuAccess(mod_code, eFormid);
        //GridView1.DataSource = dt;
        //GridView1.DataBind();
    }

    protected void lstUserList_SelectedIndexChanged(object sender, EventArgs e)
    {
        eFormVesselAssignmentList();
    }



    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Load_UserMenu();
    }


}