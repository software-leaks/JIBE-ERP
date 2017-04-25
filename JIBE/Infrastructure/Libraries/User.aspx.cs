using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.DirectoryServices;
using System.Data;

using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class Infrastructure_Libraries_User : System.Web.UI.Page
{
    BLL_Infra_Company objCompBLL = new BLL_Infra_Company();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();

    UserAccess objUA = new UserAccess();
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            Load_FleetList();
            Load_CompanyList();
            Load_CountryList();
            Load_UserTypes();
            Load_DepartmentList();
        }
        txtPassword.Attributes["value"] = txtPassword.Text;
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            Response.Redirect("~/default.aspx?msgid=2");
        }
        if (objUA.Edit == 0)
        {
            Response.Redirect("~/default.aspx?msgid=3");
        }
        if (objUA.Delete == 0)
        {
            Response.Redirect("~/default.aspx?msgid=4");
        }
        if (objUA.Approve == 0)
        {
            Response.Redirect("~/default.aspx?msgid=5");
        }

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void btnSaveUserDetails_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
         UserProperties User = new UserProperties();
             try
             {
                 if (txtDateOfBirth.Text != "")
                  {
                     DateTime DOB = DateTime.Parse(txtDateOfBirth.Text);
                   }
             }
             catch (Exception) { lblMessage.Text = "Please enter valid Date Of Birth.";
             return;
             }
            try
            {
                 if (txtDateOfJoining.Text != "")
                 {
                     DateTime DOJ = DateTime.Parse(txtDateOfJoining.Text);
                 }
            }
            catch (Exception) { lblMessage.Text = "Please enter valid Date Of Joining.";
            return;
            }
            try
            {
                 if (txtDateOfProbation.Text != "")
                 {
                     DateTime DOP = DateTime.Parse(txtDateOfProbation.Text);
                 }
            }
            catch (Exception) { lblMessage.Text = "Please enter valid Date Of Probation.";
            return;
            }

            try
            {
       
                objUserBLL.CreateUser(Convert.ToInt32(Session["USERID"]), txtFirstName.Text, "", txtLastName.Text, txtUserName.Text,DMS.DES_Encrypt_Decrypt.Encrypt(txtPassword.Text), null, txtEMail.Text
                    , UDFLib.ConvertIntegerToNull(ddlDepartment.SelectedValue), UDFLib.ConvertIntegerToNull(ddlManager.SelectedValue), UDFLib.ConvertDecimalToNull(txtApprovalLimit.Text)
                    , txtDesignation.Text, txtMobileNo.Text, UDFLib.ConvertDateToNull(txtDateOfBirth.Text), UDFLib.ConvertDateToNull(txtDateOfJoining.Text), UDFLib.ConvertDateToNull(txtDateOfProbation.Text)
                    , UDFLib.ConvertStringToNull(txtPermanentAddress.Text), UDFLib.ConvertStringToNull(txtPresentAddress.Text), UDFLib.ConvertStringToNull(ddlUserType.SelectedValue)
                    , UDFLib.ConvertIntegerToNull(ddlCompany.SelectedValue), UDFLib.ConvertIntegerToNull(ddlFleet.SelectedValue), null,int.Parse(ddlNationality.SelectedValue));


                //lblMessage.Text = "User created successfully";
                lblMessage.Text = "User account has been created successfully";
                hlnk1.Text = "Click here to login as " + txtUserName.Text.Trim();
                lblOr.Text = "Or";
                hlnk2.Text = "Go back to User List";
                btnSaveUserDetails.Enabled = false;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
 
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
       
        txtUserName.Text = "";
        txtUserName.Enabled = true;
        lblMessage.Text = "";
        btnSaveUserDetails.Enabled = true;


        pnlUserDetails.Visible = false;
    }
    protected void Load_CountryList()
    {
        BLL_Infra_Country objCountry = new BLL_Infra_Country();

        ddlNationality.DataSource = objCountry.Get_CountryList();
        ddlNationality.DataTextField = "COUNTRY";
        ddlNationality.DataValueField = "ID";
        ddlNationality.DataBind();
        ddlNationality.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void Load_DepartmentList()
    {
        ddlDepartment.Items.Clear();

        int iCompID = int.Parse(ddlCompany.SelectedValue);
        ddlDepartment.DataSource = objCompBLL.Get_CompanyDepartmentList(iCompID);
        ddlDepartment.DataTextField = "VALUE";
        ddlDepartment.DataValueField = "ID";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    //protected void Load_NetworkUsers()
    //{
    //    ddlNetwkid.DataSource = GetAllADDomainUsers_dl();
    //    ddlNetwkid.DataTextField = "UserName";
    //    ddlNetwkid.DataValueField = "UserID";
    //    ddlNetwkid.DataBind();
    //    ddlNetwkid.Items.Insert(0, new ListItem("-Select-", ""));
    //}

    protected void Load_UserTypes()
    {

        BLL_Infra_UserCredentials objBLLInfra = new BLL_Infra_UserCredentials();

        ddlUserType.DataSource =objBLLInfra.Get_UserTypeList();
        ddlUserType.DataTextField ="user_type";
        ddlUserType.DataValueField="user_type";
        ddlUserType.DataBind();
        ddlUserType.Items.Insert(0, new ListItem("-Select-", "0"));
      

    }

    protected void Load_CompanyList()
    {
        try
        {
            int iCompID = 0;
            if (getValueFromSession("UserCompanyID") != "")
            {
                iCompID = int.Parse(getValueFromSession("UserCompanyID"));               
            }

            ddlCompany.DataSource = objCompBLL.Get_CompanyListMini(iCompID);
            ddlCompany.DataTextField = "company_name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("-Select-", "0"));
            
        }
        catch
        { }
    }


    protected void Load_Company_By_User_Type()
    {

        int iCompID = 0;
        if (getValueFromSession("UserCompanyID") != "")
        {
            iCompID = int.Parse(getValueFromSession("UserCompanyID"));
        }

        DataTable dt = objCompBLL.Get_Company_By_User_Type(iCompID, ddlUserType.SelectedValue.ToString());

        ddlCompany.Items.Clear();

        if (dt.Rows.Count > 0)
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataTextField = "company_name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        else
        {
            ddlCompany.Items.Insert(0, new ListItem("-Select-", "0"));
        }
    
    }






    protected void Load_FleetList()
    {

        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
        int iCompID = UDFLib.ConvertToInteger(ddlCompany.SelectedValue);
        ddlFleet.Items.Clear();
        ddlFleet.DataSource = objVessel.GetFleetList_ByID(null, iCompID);

        ddlFleet.DataTextField = "FleetName";
        ddlFleet.DataValueField = "FleetCode";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void Load_ManagerList()
    {
        int iCompID = UDFLib.ConvertToInteger(ddlCompany.SelectedValue);
        ddlManager.Items.Clear();

        ddlManager.DataSource = objUserBLL.Get_UserList(iCompID);
        ddlManager.DataTextField = "UserName";
        ddlManager.DataValueField = "UserID";
        ddlManager.DataBind();
        ddlManager.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected string getValueFromSession(string ID)
    {
        string ret = "";
        if (Session[ID] != null)
        {
            ret = Session[ID].ToString();
        }
        else
        {
            Response.Redirect("~/Account/Login.aspx?ReturnURL=~/Infrastructure/Libraries/User.aspx");

        }

        return ret;
    }

    //protected DataTable GetAllADDomainUsers_dl()
    //{
    //    string domainpath = "192.168.0.100";

    //    DirectoryEntry searchRoot = new DirectoryEntry("LDAP://" + domainpath);
    //    DirectorySearcher search = new DirectorySearcher(searchRoot);
    //    search.Filter = "(&(objectClass=user)(objectCategory=person)(!userAccountControl:1.2.840.113556.1.4.803:=2))";

    //    search.PropertiesToLoad.Add("samaccountname");
    //    search.PropertiesToLoad.Add("givenname");
    //    search.PropertiesToLoad.Add("sn");
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add(new DataColumn("UserId", typeof(string)));
    //    dt.Columns.Add(new DataColumn("UserName", typeof(string)));

    //    SearchResult result;
    //    SearchResultCollection resultCol = search.FindAll();
    //    if (resultCol != null)
    //    {
    //        for (int counter = 0; counter < resultCol.Count; counter++)
    //        {
    //            result = resultCol[counter];
    //            if (result.Properties.Contains("samaccountname"))
    //            {
    //                if (result.Properties.Contains("userAccountControl"))
    //                {
    //                    int val = (int)result.Properties["userAccountControl"][0];
    //                }
    //                DataRow dr = dt.NewRow();
    //                dr.BeginEdit();
    //                dr["UserId"] = ((String)result.Properties["samaccountname"][0]);
    //                string FirstName = "";
    //                String LastName = "";
    //                if (result.Properties.Contains("givenname"))
    //                {
    //                    FirstName = ((String)result.Properties["givenname"][0]);
    //                }

    //                if (result.Properties.Contains("sn"))
    //                {
    //                    LastName = ((String)result.Properties["sn"][0]);
    //                }
    //                dr["UserName"] = FirstName + " " + LastName;
    //                dr.EndEdit();
    //                if (FirstName != "" && LastName != "")
    //                {
    //                    dt.Rows.Add(dr);
    //                }
    //            }
    //        }
    //    }

    //    dt.DefaultView.Sort = "UserName";
    //    return dt;

    //}//function retrive the all the User information

    //protected string[] getActiveUserDetails(string UserName)
    //{
    //    string filter = string.Format("(&(ObjectClass={0})(sAMAccountName={1}))", "person", UserName);
    //    string domain = "192.168.0.100";
    //    string[] properties = new string[] { "fullname" };

    //    string displayName = "";
    //    string firstName = "";
    //    string lastName = "";
    //    string email = "";
    //    string jobtitle = "";
    //    string department = "";

    //    try
    //    {
    //        DirectoryEntry adRoot = new DirectoryEntry("LDAP://" + domain, null, null, AuthenticationTypes.Secure);
    //        DirectorySearcher searcher = new DirectorySearcher(adRoot); searcher.SearchScope = SearchScope.Subtree;
    //        searcher.ReferralChasing = ReferralChasingOption.All;
    //        searcher.PropertiesToLoad.AddRange(properties);
    //        searcher.Filter = filter;
    //        SearchResult result = searcher.FindOne();
    //        DirectoryEntry directoryEntry = result.GetDirectoryEntry();

    //        if (directoryEntry.Properties.Contains("displayName"))
    //            displayName = directoryEntry.Properties["displayName"][0].ToString();

    //        if (directoryEntry.Properties.Contains("givenName"))
    //            firstName = directoryEntry.Properties["givenName"][0].ToString();

    //        if (directoryEntry.Properties.Contains("sn"))
    //            lastName = directoryEntry.Properties["sn"][0].ToString();

    //        if (directoryEntry.Properties.Contains("mail"))
    //            email = directoryEntry.Properties["mail"][0].ToString();

    //        if (directoryEntry.Properties.Contains("title"))
    //            jobtitle = directoryEntry.Properties["title"][0].ToString();

    //        if (directoryEntry.Properties.Contains("department"))
    //            jobtitle = directoryEntry.Properties["department"][0].ToString();

    //    }
    //    catch
    //    {

    //    }
    //    return new string[] { displayName, firstName, lastName, email, jobtitle, department };
    //}

    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {
        if (txtUserName.Text != "")
        {

            BLL_Infra_UserCredentials objuserBLL = new BLL_Infra_UserCredentials();

            DataTable dt = objuserBLL.Get_UserDetails(txtUserName.Text);
            if (dt.Rows.Count > 0)
            {
                lblMessage.Text = "UserName already exists!!";
            }
            else
            {
                pnlUserDetails.Visible = true;
                lblMessage.Text = "";
                txtUserName.Enabled = false;
            }
            //ddlNetwkid.SelectedIndex = 0;
        }
    }

    protected void ddlNetwkid_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string sUserID ;//= ddlNetwkid.SelectedValue;

        //string[] strDetails ; // getActiveUserDetails(sUserID);

        txtFirstName.Text = "";// strDetails[1];
        txtLastName.Text = ""; //strDetails[2];
        txtEMail.Text = "";// strDetails[3];
        txtDesignation.Text = "";// strDetails[4];

        //try
        //{
        //    ddlDepartment.SelectedValue = "";// strDetails[5];
        //}
        //catch { }

    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_ManagerList();
        Load_FleetList();
        Load_DepartmentList();
    }    

    protected Boolean ValidateForm()
    {
        Boolean ret = true;
        string msg = "";
        if (txtUserName.Text == "")
        {
            ret = false;
            msg = "Please enter USER NAME.";
        }
        else if (txtPassword.Text == "")
        {
            ret = false;
            msg = "Please enter PASSWORD";
        }
        else if (txtFirstName.Text == "")
        {
            ret = false;
            msg = "Please enter FIRST NAME";
        }
        else if (txtLastName.Text == "")
        {
            ret = false;
            msg = "Please select LAST NAME";
        }
        else if (ddlUserType.SelectedValue == "")
        {
            ret = false;
            msg = "Please select USER TYPE";
        }
        else if (txtEMail.Text == "")
        {
            ret = false;
            msg = "Please enter  EMAIL ID";
        }
      
        else if (ddlCompany.SelectedValue == "0")
        {
            ret = false;
            msg = "Please select company";
        }

        if (msg != "")
        {
            string js = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
        return ret;


    }

    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {

        Load_Company_By_User_Type();

    }

    protected void hlnk1_Click(object sender, EventArgs e)
    {
        if (Session["USERID"] != null)
        {
            BLL_Infra_UserCredentials objBLL = new BLL_Infra_UserCredentials();
            try
            {
                objBLL.End_Session(int.Parse(Session["USERID"].ToString()));
            }
            catch { }
            finally { objBLL = null; }
        }
        System.Web.Security.FormsAuthentication.SignOut();
        Session.RemoveAll();
        Session.Abandon();

        Response.Redirect("~/Account/Login.aspx");
    }
}