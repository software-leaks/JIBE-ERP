using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Properties;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;

public partial class Crew_Libraries_CrewRuleMapping : System.Web.UI.Page
{

    #region BusinessLayerAccess
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    UserAccess objUA = new UserAccess();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            bindGrid();
        }

    }


    /// <summary>
    /// Create Dynamic Controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        txtRule.Text = "";
        int n = UDFLib.ConvertToInteger(txtNo.Text);
        int value;
        if (int.TryParse(txtNo.Text, out value))
        {
            if (n < 5 && n > 0)
            {
                string[] Arr = new string[n];
                for (int i = 0; i < n; i++)
                {
                    Arr[i] = "N" + (i + 1);

                }
                for (int i = 0; i < Arr.Length; i++)
                {
                    txtRule.Text += Arr[i].ToString();
                    txtRule.Text += " ";
                }
                bindGrid();
            }
            else
            {
                string js = "Parameters count should not be more than 5 or less than 0";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "alert('" + js + "');", true);
            }
        }
        else
        {
            string js = "Please enter numeric values";
            txtNo.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "alert('" + js + "');", true);
        }
    }


    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            tblRule.Visible = false;
        }
        else
            tblRule.Visible = true;

        if (objUA.Edit == 0)
        {

        }
        if (objUA.Delete == 0)
        {
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

    /// <summary>
    /// Create New Rule
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnRule_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtNo.Text) && !string.IsNullOrEmpty(txtRule.Text))
            {
                objCrewAdmin.InsertAdditionalRule(UDFLib.ConvertToInteger(txtNo.Text), txtRule.Text, GetSessionUserID());
                bindGrid();
            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
       
    }



    protected void lblEdit_Click(object sender, EventArgs e)
    {
        try
        {
            //LinkButton lbtn = (LinkButton)sender;
            ImageButton lbtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)lbtn.NamingContainer;
            if (row != null)
            {
                btnUpdate.Visible = true;
                btnRule.Visible = false;
                txtNo.Text = gridRule.DataKeys[row.RowIndex].Values[1].ToString();
                txtRule.Text = gridRule.DataKeys[row.RowIndex].Values[0].ToString();
                ViewState["RuleID"] = gridRule.DataKeys[row.RowIndex].Values[2].ToString();
                txtNo.ReadOnly = true;
                btnCreate.Enabled = false;
            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

        }
    }

    protected void lblDelete_Click(object sender, EventArgs e)
    {
        //  LinkButton lbtn = (LinkButton)sender;
        ImageButton lbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lbtn.NamingContainer;
        if (row != null)
        {
            ViewState["RuleIDD"] = gridRule.DataKeys[row.RowIndex].Values[2].ToString();

            objCrewAdmin.DeleteAdditionalRule(UDFLib.ConvertToInteger(ViewState["RuleIDD"].ToString()));
            bindGrid();
        }
    }
   


 

    public class LinkTextBox : ITemplate
    {
        public void InstantiateIn(System.Web.UI.Control container)
        {
            TextBox link = new TextBox();
            link.ID = "txtParameter";
            container.Controls.Add(link);
        }
    }

    /// <summary>
    /// Bind Rules & Show In Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void bindGrid()
    {
        try
        {
            gridRule.DataSource = objCrewAdmin.getAdditionalRule();
            gridRule.DataBind();
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }




    protected void txtNo_TextChanged(object sender, EventArgs e)
    {
        btnCreate_Click(sender, e);
    }

    /// <summary>
    /// Update rule
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            btnRule.Visible = true;
            btnUpdate.Visible = false;

            objCrewAdmin.UpdateAdditionalRule(UDFLib.ConvertToInteger(txtNo.Text), txtRule.Text, GetSessionUserID(), UDFLib.ConvertToInteger(ViewState["RuleID"].ToString()));
            bindGrid();
            txtNo.ReadOnly = false;
            btnCreate.Enabled = true;
            txtNo.Text = "";
            txtRule.Text = "";
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Delete Rule
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnlDelete_Click(object sender, ImageClickEventArgs e)
    {
        txtRule.Text = "";
        txtNo.Text = "";
        Response.Redirect(Request.RawUrl);
    }

   
    protected void gridRule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnDelete = (ImageButton)e.Row.FindControl("lnlDelete");
            ImageButton btnEdit = (ImageButton)e.Row.FindControl("lblEdit");
            int CurrentUserID = GetSessionUserID();
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

            SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

            if (objUA.Edit == 0)
                btnEdit.Visible = false;
            else
                btnEdit.Visible = true;

            if (objUA.Delete == 0)
                btnDelete.Visible = false;
            else
                btnDelete.Visible = true;
        }
    }
}