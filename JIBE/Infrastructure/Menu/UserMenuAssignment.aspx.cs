using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Infrastructure_Menu_UserMenuAssignment : System.Web.UI.Page
{
    BLL_Infra_MenuManagement objMenuBLL = new BLL_Infra_MenuManagement();
    UserAccess objUA = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
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

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

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
    private string getSessionString(string SessionField)
    {
        try
        {
            if (Session[SessionField] != null && Session[SessionField].ToString() != "")
            {
                return Session[SessionField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
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
            int iMenu_Code = 0;
            int iAll = 0;
            int iUserID = 0;
            int iSessionUserID = GetSessionUserID();
            int iAdmin = 0;

            foreach (ListItem liUser in lstUserList.Items)
            {
                if (liUser.Selected == true)
                {
                    iUserID = int.Parse(liUser.Value);

                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {


                            iAll = (((CheckBox)row.FindControl("chkAll")).Checked == true) ? 1 : 0;
                            iMenu_Code = int.Parse(((CheckBox)row.FindControl("chkAll")).Text);

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
                                objMenuBLL.Update_User_Menu_Access(iUserID, iMenu_Code, iMenu, iView, iAdd, iEdit, iDelete, iApprove, iAdmin, iSessionUserID);
                                GridView1.DataBind();
                            }
                        }
                    }
                }
            }



        }
        catch { }
    }

    //protected void chkMenu_CheckedChanged(object sender, EventArgs e)
    //{
    //    int iMenu = -1;
    //    int iView = -1;
    //    int iAdd = -1;
    //    int iEdit = -1;
    //    int iDelete = -1;
    //    int iApprove = -1;
    //    int iMenu_Code = -1;

    //    iMenu_Code = int.Parse(((CheckBox)sender).Text);
    //    iMenu = (((CheckBox)sender).Checked == true) ? 1 : 0;
    //    objMenuBLL.Update_User_Menu_Access(int.Parse(lstUserList.SelectedValue), iMenu_Code, iMenu, iView, iAdd, iEdit, iDelete, iApprove, int.Parse(getSessionString("USERID")));
    //}
    //protected void chkView_CheckedChanged(object sender, EventArgs e)
    //{
    //    int iMenu = -1;
    //    int iView = -1;
    //    int iAdd = -1;
    //    int iEdit = -1;
    //    int iDelete = -1;
    //    int iApprove = -1;
    //    int iMenu_Code = -1;

    //    iMenu_Code = int.Parse(((CheckBox)sender).Text);
    //    iView = (((CheckBox)sender).Checked == true) ? 1 : 0;
    //    objMenuBLL.Update_User_Menu_Access(int.Parse(lstUserList.SelectedValue), iMenu_Code, iMenu, iView, iAdd, iEdit, iDelete, iApprove, int.Parse(getSessionString("USERID")));
    //}
    //protected void chkAdd_CheckedChanged(object sender, EventArgs e)
    //{
    //    int iMenu = -1;
    //    int iView = -1;
    //    int iAdd = -1;
    //    int iEdit = -1;
    //    int iDelete = -1;
    //    int iApprove = -1;
    //    int iMenu_Code = -1;

    //    iMenu_Code = int.Parse(((CheckBox)sender).Text);
    //    iAdd = (((CheckBox)sender).Checked == true) ? 1 : 0;
    //    objMenuBLL.Update_User_Menu_Access(int.Parse(lstUserList.SelectedValue), iMenu_Code, iMenu, iView, iAdd, iEdit, iDelete, iApprove, int.Parse(getSessionString("USERID")));
    //}
    //protected void chkEdit_CheckedChanged(object sender, EventArgs e)
    //{

    //    int iMenu = -1;
    //    int iView = -1;
    //    int iAdd = -1;
    //    int iEdit = -1;
    //    int iDelete = -1;
    //    int iApprove = -1;
    //    int iMenu_Code = -1;

    //    iMenu_Code = int.Parse(((CheckBox)sender).Text);
    //    iEdit = (((CheckBox)sender).Checked == true) ? 1 : 0;
    //    objMenuBLL.Update_User_Menu_Access(int.Parse(lstUserList.SelectedValue), iMenu_Code, iMenu, iView, iAdd, iEdit, iDelete, iApprove, int.Parse(getSessionString("USERID")));

    //}
    //protected void chkDelete_CheckedChanged(object sender, EventArgs e)
    //{
    //    int iMenu = -1;
    //    int iView = -1;
    //    int iAdd = -1;
    //    int iEdit = -1;
    //    int iDelete = -1;
    //    int iApprove = -1;
    //    int iMenu_Code = -1;

    //    iMenu_Code = int.Parse(((CheckBox)sender).Text);
    //    iDelete = (((CheckBox)sender).Checked == true) ? 1 : 0;
    //    objMenuBLL.Update_User_Menu_Access(int.Parse(lstUserList.SelectedValue), iMenu_Code, iMenu, iView, iAdd, iEdit, iDelete, iApprove, int.Parse(getSessionString("USERID")));

    //}
    //protected void chkApprove_CheckedChanged(object sender, EventArgs e)
    //{
    //    int iMenu = -1;
    //    int iView = -1;
    //    int iAdd = -1;
    //    int iEdit = -1;
    //    int iDelete = -1;
    //    int iApprove = -1;
    //    int iMenu_Code = -1;

    //    iMenu_Code = int.Parse(((CheckBox)sender).Text);
    //    iApprove = (((CheckBox)sender).Checked == true) ? 1 : 0;
    //    objMenuBLL.Update_User_Menu_Access(int.Parse(lstUserList.SelectedValue), iMenu_Code, iMenu, iView, iAdd, iEdit, iDelete, iApprove, int.Parse(getSessionString("USERID")));
    //}

    //protected void chkAll_CheckedChanged(object sender, EventArgs e)
    //{
    //    int iMenu = 1;
    //    int iView = 1;
    //    int iAdd = 1;
    //    int iEdit = 1;
    //    int iDelete = 1;
    //    int iApprove = 1;
    //    int iMenu_Code = -1;

    //    iMenu_Code = int.Parse(((CheckBox)sender).Text);
    //    objMenuBLL.Update_User_Menu_Access(int.Parse(lstUserList.SelectedValue), iMenu_Code, iMenu, iView, iAdd, iEdit, iDelete, iApprove, int.Parse(getSessionString("USERID")));

    //}

    protected void btnInitializeMenu_Click(object sender, EventArgs e)
    {
        if (lstUserList.SelectedValue == "" || lstModuleList.SelectedValue == "")
            return;

        int iMenu = 0;
        int iView = 0;
        int iAdd = 0;
        int iEdit = 0;
        int iDelete = 0;
        int iApprove = 0;
        int iMod_Code = 0;
        int iAdmin = 0;
        iMod_Code = int.Parse(lstModuleList.SelectedValue);

        objMenuBLL.Initialize_User_Menu(int.Parse(lstUserList.SelectedValue), iMod_Code, iMenu, iView, iAdd, iEdit, iDelete, iApprove, iAdmin, int.Parse(getSessionString("USERID")));

        GridView1.DataBind();
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

                        ((CheckBox)row.FindControl("chkMenu")).Checked = false;
                        ((CheckBox)row.FindControl("chkView")).Checked = false;
                        ((CheckBox)row.FindControl("chkAdd")).Checked = false;
                        ((CheckBox)row.FindControl("chkEdit")).Checked = false;
                        ((CheckBox)row.FindControl("chkDelete")).Checked = false;
                        ((CheckBox)row.FindControl("chkApprove")).Checked = false;
                    }
                }

                btnSave_Click(null, null);
            }
        }
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        int iCopyFromUser = int.Parse(ddlCopyFromUser.SelectedValue);
        int iCopyToUser = int.Parse(lstUserList.SelectedValue);
        int iAppendMode = int.Parse(ddlAppendMode.SelectedValue);

        objMenuBLL.Copy_MenuAccessFromUser(iCopyFromUser, iCopyToUser, iAppendMode, GetSessionUserID());
        GridView1.DataBind();
    }

    protected void lstCompany_DataBound(object sender, EventArgs e)
    {
        if (lstCompany.Items.Count > 0)
        {
            try
            {
                lstCompany.Text = "1";
            }
            catch
            { }
        }
    }
}