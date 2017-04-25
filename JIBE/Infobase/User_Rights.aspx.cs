using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SMS.Properties;
using SMS.Business.Infrastructure;
using SMS.Business.OCAAdmin;

public partial class Infobase_User_Rights : System.Web.UI.Page
{
    public UserAccess objUA = new UserAccess();
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    BLL_Infobase objINFO = new BLL_Infobase();
    public int Folder_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserAccessValidation();
        if (!IsPostBack)
        {
            if (Request.QueryString["i"] != null)
            {
                Folder_ID = Convert.ToInt32(Request.QueryString["i"]);
                ltHeader.Text = "FOLDER : [" + Request.QueryString["ii"] + "]";
                Load_Department();
                if (Request.QueryString["iii"] != null)
                    ddlDepartment.SelectedValue = Request.QueryString["iii"];
                Load_Users();
                BindUserRights();
            }
        }

    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Edit == 1)
            uaEditFlag = true;

        if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void Load_Department()
    {
        if (ddlDepartment.Items.Count == 0)
        {
            BLL_Infra_Company objCompany = new BLL_Infra_Company();
            DataTable dt = objCompany.Get_CompanyDepartmentList(UDFLib.ConvertToInteger(Session["USERCOMPANYID"]));

            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "Value";
            ddlDepartment.DataValueField = "ID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("-SELECT-", "0"));

        }
    }

    
    private void BindUserRights()
    {
        Folder_ID = Convert.ToInt32(Request.QueryString["i"]);
        int DepartmentId = UDFLib.ConvertToInteger(ddlDepartment.SelectedValue);
        DataSet dtUserRights = objINFO.GET_UserRightDetails(DepartmentId, Folder_ID);
        if (dtUserRights.Tables[0].Rows.Count > 0)
        {
            rdlistAccess.SelectedValue = dtUserRights.Tables[0].Rows[0]["FOLDER_ACCESS"].ToString();
        }
        else
            rdlistAccess.SelectedValue = "Company";

        gvSelectedUser.DataSource = dtUserRights.Tables[1];
        gvSelectedUser.DataBind();


    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

        Load_Users();

    }


    private void Load_Users()
    {
        int DepartmentId = UDFLib.ConvertToInteger(ddlDepartment.SelectedValue);
        Folder_ID = UDFLib.ConvertToInteger(Request.QueryString["i"]);
        DataTable dtUserList = objINFO.Get_Dept_UserList(DepartmentId, UDFLib.ConvertToInteger(Session["USERCOMPANYID"]), Folder_ID);

        if (dtUserList.Rows.Count > 0)
        {
            lstUserList.DataSource = dtUserList;
            lstUserList.DataTextField = "User_name";
            lstUserList.DataValueField = "UserID";
            lstUserList.DataBind();
            lstUserList.Visible = true;
            btnSelectUser.Visible = true;
            lblUserList.Visible = true;
            chkAll.Visible = true;
            lblNoUser.Visible = false;
        }
        else
        {
            lblNoUser.Visible = true;
            lstUserList.Visible = false;
            btnSelectUser.Visible = false;
            lblUserList.Visible = false;
            chkAll.Visible = false;
        }

    }
    protected void btnSelectUser_Click(object sender, EventArgs e)
    {
        
            string Folder_Access = rdlistAccess.SelectedValue;
            Folder_ID = Convert.ToInt32(Request.QueryString["i"]);
            DataTable dtUserRights = new DataTable();
            dtUserRights.Columns.Add("User_Id");
            dtUserRights.Columns.Add("User_Type");
            dtUserRights.Columns.Add("IsOwner");
            if (chkAll.Checked)
            {
                foreach (ListItem item in lstUserList.Items)
                {
                    DataRow dr = dtUserRights.NewRow();
                    dr["User_Id"] = item.Value;
                    dr["User_Type"] = "User";
                    dr["IsOwner"] = 0;
                    dtUserRights.Rows.Add(dr);
                }
            }

            else
            {
                foreach (ListItem item in lstUserList.Items)
                {
                    if (item.Selected)
                    {
                        DataRow dr = dtUserRights.NewRow();
                        dr["User_Id"] = item.Value;
                        dr["User_Type"] = "User";
                        dr["IsOwner"] = 0;
                        dtUserRights.Rows.Add(dr);
                    }
                }
            }

            int Success = objINFO.INS_UserRights(Folder_ID, Folder_Access, dtUserRights, GetSessionUserID());
            if (dtUserRights.Rows.Count > 0)
            {
                foreach (DataRow dr in dtUserRights.Rows)
                {
                    lstUserList.Items.Remove(lstUserList.Items.FindByValue(dr["User_Id"].ToString()));
                }
            }
            lstUserList.SelectedIndex = -1;
            BindUserRights();
            chkAll.Checked = false;
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string Folder_Access = rdlistAccess.SelectedValue;
            Folder_ID = Convert.ToInt32(Request.QueryString["i"]);
            DataTable dtUserRights = new DataTable();
            dtUserRights.Columns.Add("User_Id");
            dtUserRights.Columns.Add("User_Type");
            dtUserRights.Columns.Add("IsOwner");

            foreach (GridViewRow gvr in gvSelectedUser.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    DataRow dr = dtUserRights.NewRow();
                    string User_Id = gvSelectedUser.DataKeys[gvr.RowIndex].Value.ToString();    
                    CheckBox chkOwner = (CheckBox)gvr.FindControl("chkOwner");

                    dr["User_Id"] = User_Id;
                    if (chkOwner.Checked)
                    {
                        dr["User_Type"] = "Owner";
                        dr["IsOwner"] = 1;
                    }

                    else
                    {
                        dr["User_Type"] = "User";
                        dr["IsOwner"] = 0;
                    }


                    dtUserRights.Rows.Add(dr);
                }
            }


            int Success = objINFO.UPD_UserRights(Folder_ID, Folder_Access, dtUserRights, GetSessionUserID());


        }
        catch(Exception ex)
        {
            string Error = ex.ToString();
        }

    }
    protected void gvSelectedUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSelectedUser.PageIndex = e.NewPageIndex;
        BindUserRights();
    }

    protected void ibtnDeleteUser_Click(object source, CommandEventArgs e)
    {
        try
        {

            int Folder_Id = Convert.ToInt32(Request.QueryString["i"]);
            int UserRightID = Convert.ToInt32(e.CommandArgument.ToString());
            int deleted = objINFO.Delete_UserRight(Folder_Id,UserRightID, GetSessionUserID());
            BindUserRights();
            Load_Users();
        }
        catch { }

    }


}