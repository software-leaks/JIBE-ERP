using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Web.UI.HtmlControls;
using SMS.Business.Technical;
using System.Text;
using SMS.Properties;
using System.Data.SqlClient;
using System.IO;
using SMS.Business.SLC;


public partial class Purchase_SlopChest_Commission : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Get_SlopChest_Items();
        }
    }
    private void Get_SlopChest_Items()
    {
        try
        {
            DataTable dtItems = BLL_SLC_Admin.Get_SlopChest_Items();
            grdSlopChestCommision.DataSource = dtItems;
            grdSlopChestCommision.DataBind();
        }
        catch
        {
        }

    }
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/useraccess.htm");

        if (objUA.Add == 0)
        {


        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }


    }
    private void SaveSlopChestCommision()
    {
        string STR = "";
        try
        {
            foreach (GridViewRow GridRow in grdSlopChestCommision.Rows)
            {
                string Items = ((Label)GridRow.FindControl("lblItemID")).Text;
                string Commision = ((TextBox)GridRow.FindControl("txtCommision")).Text;
                if (Commision != "")
                {
                    STR += "|" + Items + "," + Commision;
                }
            }
            int retval = BLL_SLC_Admin.INS_UPD_SlopChest_Commision(STR.Substring(1), Convert.ToInt32(Session["USERID"]));
            string message1 = "alert('Saved Successfully.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message1", message1, true);
        }
        catch
        {
            string message1 = "alert('Error Occurred while Saving.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message1", message1, true);
        }
        finally
        {

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SaveSlopChestCommision();
        }
        catch
        {

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Default.aspx");
        }
        catch
        {

        }
    }
}