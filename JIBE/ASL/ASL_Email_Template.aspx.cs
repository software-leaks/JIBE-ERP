using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.ASL;
using System.IO;
using SMS.Properties;

public partial class ASL_ASL_Email_Template : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            pnlEmail.Visible = false;
            lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
        }
        else
        {
            pnlEmail.Visible = true;
        }

        if (objUA.Add == 0)
        {
            //ImgAdd.Visible = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        //else
        //    btnsave.Visible = false;

        if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    protected void BindGrid()
    {
        //int? SuppID = UDFLib.ConvertIntegerToNull(Request.QueryString["Supp_ID"]);
        //string SupplierID = "2743"; //GetSuppID();
        DataTable dt = BLL_ASL_Supplier.Get_Supplier_Email_Template(UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"]));
        if (dt.Rows.Count > 0)
        {
            lblSupplierName.Text = dt.Rows[0]["Register_Name"].ToString();
            lblSub.Text = dt.Rows[0]["MailSub"].ToString();
            lblBody.Text = dt.Rows[0]["Body"].ToString();
        }

    }
}