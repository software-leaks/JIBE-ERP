using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.DMS;

public partial class DMS_DocumentGroup : System.Web.UI.Page
{
    BLL_DMS_Admin objDMSAdmin = new BLL_DMS_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_GroupList();
        }
    }
    protected void UserAccessValidation()
    {
        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            dvPageContent.Visible = false;
        }
        if (objUA.Add == 0)
        {
            lnkAddNewGroup.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            gvDocumentGroup.Columns[gvDocumentGroup.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            gvDocumentGroup.Columns[gvDocumentGroup.Columns.Count - 1].Visible = false;
        }
        if (objUA.Approve == 0)
        {
        }

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void Load_GroupList()
    {
        DataTable dt = objDMSAdmin.Get_GroupList(txtfilter.Text);
        gvDocumentGroup.DataSource = dt;
        gvDocumentGroup.DataBind();
    }
    protected void gvDocumentGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(gvDocumentGroup.DataKeys[e.RowIndex].Value.ToString());

        objDMSAdmin.DELETE_Group(ID, GetSessionUserID());
        Load_GroupList();
    }
    protected void gvDocumentGroup_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(gvDocumentGroup.DataKeys[e.RowIndex].Value.ToString());
        string Category_Name = e.NewValues["GroupName"].ToString();

        ID = objDMSAdmin.INSERT_Group(ID, Category_Name, GetSessionUserID());
        if (ID > 0)
        {
            gvDocumentGroup.EditIndex = -1;
            Load_GroupList();
        }
        else
        {
            string js = "Group Name already exsist!";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);
        }
    }
    protected void gvDocumentGroup_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvDocumentGroup.EditIndex = e.NewEditIndex;
            Load_GroupList();

        }
        catch
        {
        }
    }
    protected void gvDocumentGroup_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvDocumentGroup.EditIndex = -1;
            Load_GroupList();

        }
        catch
        {
        }


    }
    protected void txtfilter_TextChanged(object sender, EventArgs e)
    {
        Load_GroupList();
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int ID = objDMSAdmin.INSERT_Group(0, txtGroupName.Text.Trim(), GetSessionUserID());
        txtGroupName.Text = "";
        txtfilter.Text = "";
        Load_GroupList();
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        int ID = objDMSAdmin.INSERT_Group(0, txtGroupName.Text.Trim(), GetSessionUserID());
        if (ID > 0)
        {
            txtGroupName.Text = "";
            txtfilter.Text = "";
            Load_GroupList();

            string js = "closeDiv('dvAddNewGroup');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
        }
        else
        {
            string js = "Group Name already exsist!";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);
        }
    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

        txtfilter.Text = "";
        Load_GroupList();

    }
}