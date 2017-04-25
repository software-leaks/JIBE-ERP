using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;

public partial class TravelQuotationAcessAssignment : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUserList();
            BindTravQtnUserList();
        }

        MarknEnabelCheckBox();
    }

    public void BindUserList()
    {
        DataTable dt = objUser.Get_UserList();
        string filter = "User_type ='OFFICE USER' ";

        if (txtUserListSearch.Text.Trim() != "")
            filter += "and  UserName like '%" + txtUserListSearch.Text + "%'";

        dt.DefaultView.RowFilter = filter;

        chkLstUser.DataSource = dt.DefaultView;
        chkLstUser.DataTextField = "UserName";
        chkLstUser.DataValueField = "UserID";
        chkLstUser.DataBind();

        MarknEnabelCheckBox(); 
    }

    public void BindTravQtnUserList()
    {
        DataTable dt = objUser.Get_Travel_Quotation_Acess();
        string filter = "";

        if (txtQtnUserListSearch.Text.Trim() != "")
            filter = "UserName like '%" + txtQtnUserListSearch.Text + "%'";

        dt.DefaultView.RowFilter = filter;

        chkLstQtnUser.DataSource = dt.DefaultView;
        chkLstQtnUser.DataTextField = "UserName";
        chkLstQtnUser.DataValueField = "UserID";
        chkLstQtnUser.DataBind();
    }

    protected void btnMoveLeft_Click(object sender, EventArgs e)
    {
        foreach (ListItem li in chkLstQtnUser.Items)
        {
            if (li.Selected == true)
            {
                int val = objUser.Insert_Travel_Quotation_Acess(Convert.ToInt32(li.Value), "REMOVEFROMLIST", Convert.ToInt32(Session["USERID"].ToString()));
            }
        }

        BindTravQtnUserList();
        BindUserList();
     
        Updpnl.Update();
    }



    protected void MarknEnabelCheckBox()
    {
        foreach (ListItem liQ in chkLstQtnUser.Items)
        {
            foreach (ListItem liU in chkLstUser.Items)
            {
                if (liQ.Value == liU.Value)
                {
                    liU.Selected = true;
                    liU.Enabled = false;
                }
            }
        }
    }



    protected void btnMoveRight_Click(object sender, EventArgs e)
    {
        foreach (ListItem li in chkLstUser.Items)
        {
            if (li.Selected == true)
            {
                int val = objUser.Insert_Travel_Quotation_Acess(Convert.ToInt32(li.Value), "ADDTOLIST", Convert.ToInt32(Session["USERID"].ToString()));
            }
        }
        BindTravQtnUserList();
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

    protected void btnQtnUserFilter_Click(object sender, ImageClickEventArgs e)
    {
        BindTravQtnUserList();
        Updpnl.Update();
    }

    protected void btnQtnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtQtnUserListSearch.Text = "";
        BindTravQtnUserList();
        Updpnl.Update();
    }
}