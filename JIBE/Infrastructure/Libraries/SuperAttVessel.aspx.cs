using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;

public partial class SuperAttVessel : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUserList();
            Bind_SelectedSuperList();
        }

        //MarknEnabelCheckBox();
    }

    public int GetSessionUserID()
    {
        return UDFLib.ConvertToInteger(Session["USERID"]);
    }

    public void BindUserList()
    {
        DataTable dt = objUser.Get_Super_List();
        string filter = "User_type ='OFFICE USER' ";

        if (txtUserListSearch.Text.Trim() != "")
            filter += "and  UserName like '%" + txtUserListSearch.Text + "%'";

        dt.DefaultView.RowFilter = filter;

        chkLstUser.DataSource = dt.DefaultView;
        chkLstUser.DataTextField = "UserName";
        chkLstUser.DataValueField = "UserID";
        chkLstUser.DataBind();

        //MarknEnabelCheckBox(); 
    }

    public void Bind_SelectedSuperList()
    {
        DataTable dt = objUser.Get_SuperAttendingVessel_List();
        string filter = "";

        if (txtSuperUserListSearch.Text.Trim() != "")
            filter = "UserName like '%" + txtSuperUserListSearch.Text + "%'";

        dt.DefaultView.RowFilter = filter;
        chkLstSelectedUser.DataSource = dt.DefaultView;
        chkLstSelectedUser.DataTextField = "UserName";
        chkLstSelectedUser.DataValueField = "UserID";
        chkLstSelectedUser.DataBind();
    }

    protected void btnMoveLeft_Click(object sender, EventArgs e)
    {
        foreach (ListItem li in chkLstSelectedUser.Items)
        {
            if (li.Selected == true)
            {
                int val = objUser.UPDATE_SuperAttendingVessel(Convert.ToInt32(li.Value), "REMOVEFROMLIST", GetSessionUserID());
            }
        }

        Bind_SelectedSuperList();
        BindUserList();     
        Updpnl.Update();
    }    

    //protected void MarknEnabelCheckBox()
    //{
    //    foreach (ListItem liQ in chkLstUser.Items)
    //    {
    //        foreach (ListItem liU in chkLstUser.Items)
    //        {
    //            if (liQ.Value == liU.Value)
    //            {
    //                liU.Selected = true;
    //                liU.Enabled = false;
    //            }
    //        }
    //    }
    //}
    
    protected void btnMoveRight_Click(object sender, EventArgs e)
    {
        foreach (ListItem li in chkLstUser.Items)
        {
            if (li.Selected == true)
            {
                int val = objUser.UPDATE_SuperAttendingVessel(Convert.ToInt32(li.Value), "ADDTOLIST", Convert.ToInt32(Session["USERID"].ToString()));
            }
        }
        Bind_SelectedSuperList();
        BindUserList();
        Updpnl.Update();
    }

    protected void btnUserFilter_Click(object sender, ImageClickEventArgs e)
    {
        BindUserList();
        Updpnl.Update();
    }

    protected void btnUserRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtUserListSearch.Text = "";
        BindUserList();
        Updpnl.Update();
    }

    protected void btnSuperUserFilter_Click(object sender, ImageClickEventArgs e)
    {
        Bind_SelectedSuperList();
        Updpnl.Update();
    }

    protected void btnSuperRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtSuperUserListSearch.Text = "";
        Bind_SelectedSuperList();
        Updpnl.Update();
    }
}