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
using BLLQuotation;

public partial class WebQuotation_SupplierLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login1_Load(object sender, EventArgs e)
    {

        //Login1.FailureText = "Login Failure"; 

    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        string strUserID = "";
        string strPassword = "";

        strUserID = Login1.UserName.Trim().ToString();
        strPassword = Login1.Password.Trim().ToString();


        if (CheckCredential(strUserID, strPassword))
        {

            DataTable dtSupp = GetSupplierInfo(strUserID, strPassword);
            GetSupplierName(strUserID);
            bool isAgent = false;
            bool isSupp = false;
            bool isForwd = false;

            foreach (DataRow dr in dtSupp.Rows)
            {
                if (dr["user_type"].ToString() == "S")
                {
                    isSupp = true;
                }
                else if (dr["user_type"].ToString() == "A")
                {
                    isAgent = true;
                }
                else if (dr["user_type"].ToString() == "F")
                {
                    isForwd = true;
                }

            }

            if (isSupp)
            {
                FormsAuthentication.SetAuthCookie(strUserID, false);
                Response.Redirect("WebQuotationDetails.aspx");
            }
          

        }
        else
        {
            Login1.FailureText = "Authentication unsuccessful";
        }
    }


    public DataTable GetSupplierInfo(string UserId, string strPassword)
    {
        clsQuotationBLL objBLLQuot = new clsQuotationBLL();
        DataTable dtSuppInfo = new DataTable();
        dtSuppInfo = objBLLQuot.GetSupplierInfo(UserId, strPassword);

        //Assign Mandatory Info into the session

        Session["SuppCode"] = dtSuppInfo.Rows[0]["SUPPLIER"].ToString().Trim();
        Session["pwd"] = Login1.Password;
        Session["userid"] = dtSuppInfo.Rows[0]["ID"].ToString().Trim();

        return dtSuppInfo;

    }

    public DataTable GetSupplierName(string suppcode)
    {
        clsQuotationBLL objBLLQuot = new clsQuotationBLL();
        DataTable dtSupp = new DataTable();

        dtSupp = objBLLQuot.GetSupplierName(suppcode);
        //Assign Mandatory Info into the session
        Session["SuppName"] = dtSupp.Rows[0]["SHORT_NAME"].ToString();
        Session["PassString"] = dtSupp.Rows[0]["PassString"].ToString();

        return dtSupp;

    }





    public bool CheckCredential(string UserId, string strPassword)
    {
        try
        {

            clsQuotationBLL objBLLQuot = new clsQuotationBLL();
            return objBLLQuot.IsValidSupplier(UserId, strPassword);
        }
        catch //(Exception ex)
        {
            return false;
        }

    }

    public void btnSendPassword_Click(object s, EventArgs e)
    {

        int sts = BLL_PURC_Purchase.GET_UPD_SUPPLIER_PASSWORD(txtUserid.Text.Trim());
        String msg = "";
        if (sts == 1)
        {
            txtUserid.Text = "";
            msg = String.Format("alert('Password will be send on registered email address.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mssucc", msg, true);
        }
        else
        {
            txtUserid.Text = "";
            msg = String.Format("alert('Please enter correct User ID !');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mserr", msg, true);
        }
    }
}
