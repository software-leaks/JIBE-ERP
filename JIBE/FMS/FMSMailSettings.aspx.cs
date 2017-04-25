using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.FMS;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class FMS_FMSMailSettings : System.Web.UI.Page
{
    BLL_FMS_Document objFMS = new BLL_FMS_Document();
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtMailID.Text = objFMS.GetMailID();

            if (txtMailID.Text == string.Empty)
            {
                btnsave.Text = "Save";
            }
            else
            {
                btnsave.Text = "Update";
            }
            UserAccessValidation();
        }
    }
    /// <summary>
    /// To validate controls
    /// </summary>
    /// <returns>True : If Validation successeds || False : If validation fails.</returns>
    private bool ValidateMailID()
    {
        try
        {
            if (txtMailID.Text.Trim() == string.Empty)
            {
                string Error10 = "alert('Please enter E-Mail.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error10", Error10, true);
                txtMailID.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return true;
    }

    /// <summary>
    /// Added by Anjali .To save / update Mail id , used to forward mail with forms attchment.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateMailID())
            {
                SaveMailID();
            }
            else
            {
                txtMailID.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

        }

    }
   
    /// <summary>
    /// Added by Anjali .To save / update Mail id , used to forward mail with forms attchment.
    /// </summary>
    private void SaveMailID()
    {
        try
        {

            objFMS.SaveMailID(txtMailID.Text.Trim(), GetSessionUserID());
            Session["MAIL_ID"] = txtMailID.Text.Trim();

            string jsNoFile = "alert('E-Mail saved successfully.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsNoFile", jsNoFile, true);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// To Get logged in users' id.
    /// </summary>
    /// <returns></returns>
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Convert.ToString(Session["USERID"]));
        else
            return 0;
    }

    /// <summary>
    /// To check acces for logged in user.
    /// like Add,Edit ,delete access for requested page.
    /// </summary>
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        try
        {
            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

            if (objUA != null)
            {
                if (objUA.View == 0)
                {
                    Response.Write("<center><br><br><h2><font color=gray>You do not have enough priviledge to access this page.</font></h2></center>");
                    Response.End();
                }

                // Only Admin can add/update mail id
                if (objUA.Admin == 1)
                {
                    btnsave.Visible = true;
                }
                else
                {
                    btnsave.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }

}