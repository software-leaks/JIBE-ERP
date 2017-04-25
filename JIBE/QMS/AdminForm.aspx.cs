using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SMS.Business.QMS;
using System.Management;
using System.IO;
using System.Drawing;

public partial class AdminForm : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlDataInfoBind();
            UserInfoBind();
        }
    }

    /// <summary>
    /// this is use to bind the combo box with AccessLevel Name & AccessLevel ID.
    /// </summary>
    public void ddlDataInfoBind()
    {
        DataSet dsAccessLevel = objQMS.AccessLevel();
        DDLAccessLevel.Items.Clear();
        DDLAccessLevel.DataSource = dsAccessLevel.Tables[0];
        DDLAccessLevel.DataTextField = "AccessLevel";
        DDLAccessLevel.DataValueField = "AccessLevel_id";
        DDLAccessLevel.DataBind();
        DDLAccessLevel.Items.Insert(0, new ListItem("Select Role", "Select Role"));
    }

    /// <summary>
    /// bind the datagrid with a existing user from the database.
    /// </summary>
    public void UserInfoBind()
    {
        DataSet UserDs = objQMS.UserDetails();
        UserGrid.DataSource = UserDs.Tables[0];
        UserGrid.DataBind();
    }

    /// <summary>
    /// based on the selction & filled data user information going to save & update accordingly.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        String msg2 = "";
        dvMessage.Text = "";
        if (txtuser.Text != "")
        {
            if (txtFName.Text != "")
            {
                if (txtPWd.Text != "" && txtRePwd.Text != "" && txtPWd.Text.ToUpper().Equals(txtRePwd.Text.ToUpper()) == true)
                {
                    if (txtEmail.Text != "")
                    {
                        if (DDLAccessLevel.SelectedIndex != 0)
                        {

                            if (Convert.ToString(Session["UserIDForUpdate"]) == "")
                            {
                                objQMS.AddNewUser(txtFName.Text, txtLNAme.Text, txtMName.Text, txtuser.Text, txtPWd.Text, txtEmail.Text, Convert.ToInt32(DDLAccessLevel.SelectedValue));
                                msg2 = String.Format("myMessage('User has been created successfully.')");
                             }
                            else
                            {
                                objQMS.UpdateUserInfo(Convert.ToInt32(Session["UserIDForUpdate"]), txtFName.Text, txtLNAme.Text, txtMName.Text, txtuser.Text, txtPWd.Text, txtEmail.Text, Convert.ToInt32(DDLAccessLevel.SelectedValue));
                                msg2 = String.Format("myMessage('Updated successfully.')");
                                Session["UserIDForUpdate"] = "";
                            }

                            clearControlValues();
                             btnSave.Text = "Save";
                        }
                        else
                            msg2 = String.Format("myMessage('Please select role.')");
                    }
                    else
                        msg2 = String.Format("myMessage('Please enter email.')");
                }
                else
                    msg2 = String.Format("myMessage('Please enter Password,re-password both should be same .')");
            }
            else
                msg2 = String.Format("myMessage('Please enter first name.')");

        }
        else
            msg2 = String.Format("myMessage('Please enter user name.')");

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg2", msg2, true);
    }

    /// <summary>
    /// onClick ,it set the default values & clear the filter controls.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnSave.Text = "Save";
        Session["UserIDForUpdate"] = "";
        clearControlValues();
    }

    public void clearControlValues()
    {
        txtFName.Text = "";
        txtLNAme.Text = "";
        txtMName.Text = "";
        txtuser.Text = "";
        txtPWd.Text = "";
        txtEmail.Text = "@smsship.com";
        DDLAccessLevel.SelectedIndex = 0;
    }

    protected void UserGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        UserGrid.PageIndex = e.NewPageIndex;
        UserInfoBind();
    }

    /// <summary>
    /// it uses user  record has been deleted from the database.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteByUserID(object sender, CommandEventArgs e)
    {
        int UserID = Convert.ToInt32(e.CommandArgument.ToString());
        objQMS.DeleteUserByID(UserID);
        String msg2 = String.Format("myMessage('Record has been deleted successfully.')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg2", msg2, true);
        UserInfoBind();
        ViewState["sortDirection"] = SortDirection.Ascending;
    }

    /// <summary>
    /// onclick on the grid cell, populate all the user row information  into the mentioned control for the editing.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ShowUser(object sender, CommandEventArgs e)
    {
        int UserID = Convert.ToInt32(e.CommandArgument.ToString());
        Session["UserIDForUpdate"] = UserID;
        DataSet dsUserDetails = objQMS.getUserDetailsByID(UserID);
        if (dsUserDetails.Tables[0].Rows.Count > 0)
        {
            txtFName.Text = dsUserDetails.Tables[0].Rows[0]["First_Name"].ToString();
            txtLNAme.Text = dsUserDetails.Tables[0].Rows[0]["Last_Name"].ToString();
            txtMName.Text = dsUserDetails.Tables[0].Rows[0]["Middle_Name"].ToString();
            txtEmail.Text = dsUserDetails.Tables[0].Rows[0]["MailId"].ToString();
            txtPWd.Text = dsUserDetails.Tables[0].Rows[0]["Password"].ToString();
            txtuser.Text = dsUserDetails.Tables[0].Rows[0]["UserName"].ToString();
            DDLAccessLevel.SelectedValue = dsUserDetails.Tables[0].Rows[0]["AccessLevel"].ToString();
            btnSave.Text = "Update";
        }
    }

    protected void UserGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;
        ViewState["z_sortexpresion"] = e.SortExpression;
        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
            SortGridView(sortExpression, "DESC");
        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;
            SortGridView(sortExpression, "ASC");
        }
    }

    private void SortGridView(string sortExpression, string direction)
    {
        DataSet ds = objQMS.UserDetails();  
        DataView dv = new DataView(ds.Tables[0]);
        dv.Sort = sortExpression + " " + direction;
        this.UserGrid.DataSource = dv;
        UserGrid.DataBind();
    }

    public string SortExpression
    {
        get
        {
            if (ViewState["z_sortexpresion"] == null)
                ViewState["z_sortexpresion"] = this.UserGrid.DataKeyNames[0].ToString();
            return ViewState["z_sortexpresion"].ToString();
        }
        set
        {
            ViewState["z_sortexpresion"] = value;
        }
    }

    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;
            return (SortDirection)ViewState["sortDirection"];
        }
        set
        {
            ViewState["sortDirection"] = value;
        }
    }
}


