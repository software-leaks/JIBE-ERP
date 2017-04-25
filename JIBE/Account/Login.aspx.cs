using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using SMS.Business.Infrastructure;
using SMS.Data;
using System.IO;
using System.Net; //Include this namespace

public partial class Account_Login : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objBLL = new BLL_Infra_UserCredentials();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Supplier_Code"] != null)
        {
            DataSet ds_ntLan = new DataSet();

            ds_ntLan = objBLL.Get_Supplier_UserCredentials(Request.QueryString["Supplier_Code"]);

            string strUserID = "";
            if (ds_ntLan.Tables[0].Rows.Count > 0)
            {
                if (ds_ntLan.Tables[0].Rows[0]["UserId"].ToString() != "0")
                {
                    strUserID = ds_ntLan.Tables[0].Rows[0]["UserId"].ToString();
                    Session["USERID"] = ds_ntLan.Tables[0].Rows[0]["UserId"].ToString();
                    Session["SUPPLIER_ID"] = ds_ntLan.Tables[0].Rows[0]["SUPPLIER_ID"].ToString();
                    Session["SUPPNAME"] = ds_ntLan.Tables[0].Rows[0]["FULL_NAME"].ToString(); ;
                    Session["PASSSTRING"] = ds_ntLan.Tables[0].Rows[0]["PASSSTRING"].ToString();
                    Session["SUPPCODE"] = ds_ntLan.Tables[0].Rows[0]["SUPPLIER"].ToString();
                    Session["USERMAILID"] = ds_ntLan.Tables[0].Rows[0]["MailID"].ToString();

                    FormsAuthentication.SetAuthCookie(strUserID, false);
                    Response.Redirect("~/ASL/ASL_Data_Entry.aspx?Supp_ID=" + Session["SUPPCODE"] + "");
                }
                else
                {
                    var a = Request.Url.AbsoluteUri;
                    string ReturnUrl = "";
                    if (Request.QueryString != null)
                    {
                        if (Request.QueryString["ReturnUrl"] != null)
                            ReturnUrl = Request.QueryString["ReturnUrl"].ToString();
                    }

                    if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["APP_URL"]))
                    {
                        if (!String.IsNullOrWhiteSpace(ReturnUrl))
                            Response.Redirect(ConfigurationManager.AppSettings["APP_URL"] + "/Account/Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(ReturnUrl));
                        else
                            Response.Redirect(ConfigurationManager.AppSettings["APP_URL"] + "/Account/Login.aspx");
                    }
                    else
                    {
                        if (ReturnUrl == "" && a != ConfigurationManager.AppSettings["APP_URL"] + "/Account/Login.aspx")
                        {
                            Response.Redirect(ConfigurationManager.AppSettings["APP_URL"] + "/Account/Login.aspx");
                        }
                    }

                }
            }
        }
        else
        {
            var a = Request.Url.AbsoluteUri;
            string ReturnUrl = "";
            if (Request.QueryString != null)
            {
                if (Request.QueryString["ReturnUrl"] != null)
                    ReturnUrl = Request.QueryString["ReturnUrl"].ToString();
            }



            if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["APP_URL"]))
            {
                if (!String.IsNullOrWhiteSpace(ReturnUrl))
                    Response.Redirect(ConfigurationManager.AppSettings["APP_URL"] + "/Account/Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(ReturnUrl));
                else
                    Response.Redirect(ConfigurationManager.AppSettings["APP_URL"] + "/Account/Login.aspx");
            }
            else
            {
                if (ReturnUrl == "" && a != ConfigurationManager.AppSettings["APP_URL"] + "/Account/Login.aspx")
                {
                    Response.Redirect(ConfigurationManager.AppSettings["APP_URL"] + "/Account/Login.aspx");
                }
            }
        }

        if (Session["USERID"] != null && Session["UTYPE"] != null)
        {
            string strUserID = "";
            string strUserType = "";

            strUserID = Session["USERID"].ToString();
            strUserType = Session["UTYPE"].ToString();

            if (strUserType.ToUpper() == "SUPPLIER".ToUpper())
            {
                FormsAuthentication.SetAuthCookie(strUserID, false);
                Response.Redirect("~/webqtn/WebQuotationDetails.aspx");
            }
            else if (strUserType.ToUpper() == "TRAVEL AGENT".ToUpper())
            {
                FormsAuthentication.SetAuthCookie(strUserID, false);
                Response.Redirect("~/travel/RequestListAgent.aspx");
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(Request.QueryString["ReturnUrl"]))
                {
                    FormsAuthentication.RedirectFromLoginPage(strUserID, false);
                }
                else
                {
                    //Response.Redirect("~/Infrastructure/DashBoard_Common.aspx");
                    Response.Redirect(ConfigurationManager.AppSettings["DeafaultURL"]);
                }
            }
        }

    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// Modified by Anjali DT:6-jun-2016.
    /// To Authenticate User , 
    ///     1, If entered user cretentilas are valid , user will be authenticted.   
    ///     2. If entered user cretentilas are not  valid , alert will be displayed.
    /// After authentication .
    ///     1. User information needed for further reference  will be added in Session such as UserId,Username,role,UserType etc.
    ///     2. Depend on User type user will be redirected to respective pages.
    ///         eg.Usertype ='SUPPLIER' redirected to 'WebQuotationDetails.aspx' etc.
    ///     3. If entered password is default password i.e. 1234  ,user will be redirected to change password page.
    ///     4. If user not updated his/her password more than 180 days , in this case also user will be redirected to change password page.
    ///     Modified By Alok /19/10/2016
    ///     Session ID store in session variable. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
    {

        DataSet ds_ntLan = new DataSet();
        DataSet ds_admincheck = new DataSet();

        string strUserID = "";
        string strUserStyle = "";
        string strUserType = "";
        string strUserFullName = "";
        string strUserCompany = "";
        string strUserCompanyID = "";
        string UserIP = "";
        string ClientBrowser = "";       
        string userid = LoginUser.UserName.Trim().ToString().ToUpper();
        string UserName = LoginUser.UserName.Trim().ToString();
        string password = LoginUser.Password.Trim().ToString();
     

        ds_ntLan = objBLL.Get_UserCredentials(userid, DMS.DES_Encrypt_Decrypt.Encrypt(password));
        ClientBrowser = Request.UserAgent;
        UserIP = Request.UserHostAddress;
        string hostName = Dns.GetHostName(); // Retrive the Name of HOST
        string MachineIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
        if (ds_ntLan.Tables["Login"] != null)
        {
            if (ds_ntLan.Tables["Login"].Rows.Count > 0)
            {
                strUserID = ds_ntLan.Tables["Login"].Rows[0]["UserId"].ToString();
                strUserStyle = ds_ntLan.Tables["Login"].Rows[0]["style"].ToString();
                strUserType = ds_ntLan.Tables["Login"].Rows[0]["User_Type"].ToString();
                strUserFullName = ds_ntLan.Tables["Login"].Rows[0]["User_FullName"].ToString();
                strUserCompany = ds_ntLan.Tables["Login"].Rows[0]["Company_Name"].ToString();
                strUserCompanyID = ds_ntLan.Tables["Login"].Rows[0]["COMPANY_ID"].ToString();

                int PWD_Last_Updated_InDays = UDFLib.ConvertToInteger(ds_ntLan.Tables["Login"].Rows[0]["PWD_Last_Updated_InDays"]);
                string Role = objBLL.Get_User_Role(int.Parse(strUserID));

                Session["OCAGUID"] = Guid.NewGuid().ToString();
                if (!string.IsNullOrEmpty(strUserID) && !string.IsNullOrEmpty(Convert.ToString(Session["OCAGUID"])))
                {


                    try
                    {

                        int result = SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["demoasp"].ConnectionString, CommandType.Text, "UPDATE USER_MASTER SET PassKey='" + Convert.ToString(Session["OCAGUID"]) + "' WHERE SMSLOG_User_ID=" + strUserID);

                    }
                    catch (Exception ex)
                    {
                        UDFLib.WriteExceptionLog(ex);
                    }


                }

                Session["ACCESSLEVEL"] = ds_ntLan.Tables["Login"].Rows[0]["ACCESSLEVEL"].ToString();
                Session["ROLE"] = Role;
                Session["USERNAME"] = ds_ntLan.Tables["Login"].Rows[0]["User_name"].ToString();
                Session["USERID"] = strUserID;
                Session["USERSTYLE"] = strUserStyle;
                Session["UTYPE"] = strUserType;
                Session["USERFULLNAME"] = strUserFullName;
                Session["USERCOMPANY"] = strUserCompany;
                Session["USERCOMPANYID"] = strUserCompanyID;
                Session["SUPPLIER_ID"] = ds_ntLan.Tables["Login"].Rows[0]["SUPPLIER_ID"].ToString();
                Session["SUPPNAME"] = ds_ntLan.Tables["Login"].Rows[0]["FULL_NAME"].ToString(); ;
                Session["PASSSTRING"] = ds_ntLan.Tables["Login"].Rows[0]["PASSSTRING"].ToString();
                Session["SUPPCODE"] = ds_ntLan.Tables["Login"].Rows[0]["SUPPLIER"].ToString();
                Session["pwd"] = password;
                Session["APPCOMPANYID"] = ConfigurationManager.AppSettings["Company_ID"];
                Session["COMPANYTYPE"] = ds_ntLan.Tables["Login"].Rows[0]["Company_Type"].ToString();
                Session["USERDEPARTMENTID"] = ds_ntLan.Tables["Login"].Rows[0]["Dep_Code"].ToString();
                Session["USERFLEETID"] = ds_ntLan.Tables["Login"].Rows[0]["Tech_Manager"].ToString() != "" ? ds_ntLan.Tables["Login"].Rows[0]["Tech_Manager"].ToString() : "0";
                Session["USERMAILID"] = ds_ntLan.Tables["Login"].Rows[0]["MailID"].ToString();

                Session["Company_Name_GL"] = ds_ntLan.Tables["Login"].Rows[0]["Company_Name"].ToString();
                Session["Company_Address_GL"] = ds_ntLan.Tables["Login"].Rows[0]["Company_Address"].ToString();
                Session["PWD_Last_Updated_InDays"] = PWD_Last_Updated_InDays;  //Added by Anjali DT:6-Jun-2016 JIT:9490 ||  To enforce Office user to change password ,when Office user not updated his/her password more than 180 days or password is default password i.e 1234 for all users.

                //Added a new session variable to store date format for logged in user. 
                //Dateformat will be fetched from Lib_User table, if Lib_User doesn't have value then dateformat will be fetched from Lib_Company.
                //Session["User_DateFormat"] = ds_ntLan.Tables["Login"].Rows[0]["User_dateFormat"].ToString();

                Session["User_DateFormat"] = "dd-MM-yyyy";
                if (Convert.ToString(ds_ntLan.Tables["Login"].Rows[0]["Date_Format"])!="")
                    Session["User_DateFormat"] = ds_ntLan.Tables["Login"].Rows[0]["Date_Format"].ToString();

                //string UserIP = "";
                //string ClientBrowser = "";

                //UserIP = Request.UserHostAddress;
                if (UserIP == null)
                {
                    UserIP = Request.ServerVariables["REMOTE_ADDR"];
                }

            
                objBLL.Start_Session(int.Parse(strUserID), Session.SessionID, UserIP, ClientBrowser);
                Session["Session"] = Session.SessionID;

                if (strUserType.ToUpper() == "SUPPLIER".ToUpper())
                {
                    FormsAuthentication.SetAuthCookie(strUserID, false);
                    Response.Redirect("~/webqtn/WebQuotationDetails.aspx");
                }
                else if (strUserType.ToUpper() == "TRAVEL AGENT".ToUpper())
                {
                    FormsAuthentication.SetAuthCookie(strUserID, false);
                    Response.Redirect("~/travel/RequestListAgent.aspx");
                }
                else
                {
                    //-- Default Password should be changed--
                    if (password == "1234" || password == Convert.ToString((1234 + Convert.ToInt32(Session["USERID"]))))
                        Response.Redirect("~/Account/ChangePassword.aspx?msg=YOUR DEFAULT PASSWORD IS EXPIRED!");
                    else if (strUserType == "OFFICE USER" && PWD_Last_Updated_InDays > 180)
                    {
                        FormsAuthentication.SetAuthCookie(strUserID, false);
                        Response.Redirect("~/Account/ChangePassword.aspx?msg=YOUR CURRENT PASSWORD IS EXPIRED! PLEASE CHANGE YOUR PASSWORD.");
                    }
                    else
                        FormsAuthentication.RedirectFromLoginPage(strUserID, false);
                    UserAccessLog(Session["USERNAME"].ToString(), Session["USERID"].ToString(), Session["Session"].ToString(), DateTime.Now, MachineIP, "Success", ClientBrowser,null);

                }


            }
            else
            {
              
                Session.Abandon();
                LoginUser.FailureText = "Log-In ID or Password is incorrect.";
                UserAccessLog(UserName, "NULL", "NULL", DateTime.Now, MachineIP, "Failure", ClientBrowser, ds_ntLan.Tables[1].Rows[0][0].ToString());
               
            }
        }
        else
        {
            Session.Abandon();
            LoginUser.FailureText = "Log-In ID or Password is incorrect.";
            UserAccessLog(UserName, "NULL", "NULL", DateTime.Now, MachineIP, "Failure", ClientBrowser, ds_ntLan.Tables[1].Rows[0][0].ToString());
        }
     
    }
    /// <summary>
    /// This method is used to write login attempts in to text file.
    /// </summary>
    /// <param name="UserName">Entered user namefor login</param>
    /// <param name="USERID">Login user ID</param>
    /// <param name="SessionID">After successfully login, session id is generated.</param>
    /// <param name="sessionstart">Login time</param>
    /// <param name="UserIP">Login user ip address</param>
    /// <param name="browser">User Login on which browser</param>
    /// <param name="Status">Wheather login is failed or success.</param>
    public void UserAccessLog(string UserName, string USERID, string SessionID, DateTime sessionstart, string MachineIP, string Status, string ClientBrowser, string Reason)
    {
        string UserAccessLog = "";
        FileInfo finfo = null;
        try
        {
            string UserAccessLogFolderPath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\UserAccessLog";   /* Created main folder UserAccessLog */
            string UserAccessLogBackupFolder = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\UserAccessLog\\UserAccessLogBackup";  /* Created back up folder */

            if (!Directory.Exists(UserAccessLogFolderPath))
            {
                Directory.CreateDirectory(UserAccessLogFolderPath);
               
                if (!Directory.Exists(UserAccessLogBackupFolder))
                    Directory.CreateDirectory(UserAccessLogBackupFolder);
            }


            if (Directory.Exists(UserAccessLogFolderPath))
            {
                 UserAccessLog = UserAccessLogFolderPath + "\\UserAccessLog.txt";    /* Created text file for writing login details.*/
                if (File.Exists(UserAccessLog))
                {
                   
                     finfo = new FileInfo(UserAccessLog);
                    DateTime dtcreation=finfo.CreationTime;                  /* Retrived file creation date */
                    string StrmonthName, Stryear;
                    StrmonthName = String.Format("{0:MMMM}", dtcreation);    /* Retrived month from creation date */
                    Stryear = dtcreation.Year.ToString();                    /* Retrived year from creation date*/ 
                    if (dtcreation.Month == DateTime.Now.Month)              /* check wheather file creation month is same as current month*/
                   {

                       CreateNewUserAccessLog(UserAccessLog, UserName, USERID, SessionID, sessionstart, MachineIP, Status, ClientBrowser, Reason);    /* both months is same then details write in to same text file*/
                      
                    }
                    else
                    {
                        if (Directory.Exists(UserAccessLogBackupFolder))
                        {
                            System.IO.File.Move(UserAccessLog, UserAccessLogBackupFolder + "\\UserAccessLog_" + StrmonthName + "_" + Stryear + ".txt");  /* both months is diffrenet then text file move in to back up folder and created new text file for new month */
                             File.Delete(UserAccessLog);
                             CreateNewUserAccessLog(UserAccessLog, UserName, USERID, SessionID, sessionstart, MachineIP, Status, ClientBrowser, Reason);
                             File.SetCreationTime(UserAccessLog, DateTime.Now);   /* set creation date of file */
                            
                        }                     
                    }
                }
                else
                {

                    CreateNewUserAccessLog(UserAccessLog, UserName, USERID, SessionID, sessionstart, MachineIP, Status, ClientBrowser, Reason);
                    
                
                }
            }
           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            if (finfo.IsReadOnly)       /* check wheather file  readonly property is true or false */
            {
               
                RemoveAccess(UserAccessLog);  /* remove readonly property */
                CreateNewUserAccessLog(UserAccessLog, UserName, USERID, SessionID, sessionstart, MachineIP, Status, ClientBrowser, Reason);  
            }
        }
    }
    /// <summary>
    /// Handled access denied exception.If file readonly property is true then make it false and write login details in to file.
    /// </summary>
    /// <param name="UserAccessLog">Getting file details</param>
    public void RemoveAccess(string UserAccessLog)
    {
        try
        {
            FileInfo finfo = new FileInfo(UserAccessLog);
            if (finfo.IsReadOnly)
            {
                finfo.IsReadOnly = false;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    /// <summary>
    /// This method is used to write login details in to text file with headers.
    /// </summary>
    /// <param name="UserAccessLog">path of text file</param>
    /// <param name="UserName">Entered user namefor login</param>
    /// <param name="USERID">Login user ID</param>
    /// <param name="SessionID">After successfully login, session id is generated.</param>
    /// <param name="sessionstart">Login time</param>
    /// <param name="UserIP">Login user ip address</param>
    /// <param name="browser">User Login on which browser</param>
    /// <param name="Status">Wheather login is failed or success.</param>
    public void CreateNewUserAccessLog(string UserAccessLog, string UserName, string USERID, string SessionID, DateTime sessionstart, string UserIP, string Status, string ClientBrowser, string Reason)
    {
        using (System.IO.StreamWriter strmWriter = new System.IO.StreamWriter(UserAccessLog, true))
        {
                          
            strmWriter.WriteLine("****** Login attempt by " + UserName + " on " + DateTime.Now.Date + " ******");
            strmWriter.WriteLine("User Name\tUser Id\tSession Id\tSession Start\tUser IP\tStatus\tClient Browser\tAccess Denied Reason");
            strmWriter.WriteLine(UserName + "\t" + USERID + "\t" + SessionID + "\t" + sessionstart + "\t" + UserIP + "\t" + Status + "\t" + ClientBrowser + "\t" + Reason);
            strmWriter.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }
            
    }
    public void menu_noMenu()
    {
        string strConn = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        SqlConnection sCon = new SqlConnection(strConn);
        SqlDataAdapter sqlAdp = new SqlDataAdapter("SP_INF_MNU_Get_MenuLib", sCon);
        sqlAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
        sqlAdp.SelectCommand.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int));
        string test = Session["USERID"].ToString();
        sqlAdp.SelectCommand.Parameters["@userid"].Value = Session["USERID"].ToString();
        DataSet ds = new DataSet();
        sqlAdp.Fill(ds, "Menu");

        int intLink = 0;

        if (ds.Tables[0].Rows.Count > 0)
        {
            //if there is no menu_link information stop to generate the menu
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr[3].ToString() != "")
                {

                    intLink = 1;
                }
            }
            if (intLink == 1)
            {
                Session["nomenu"] = 2;
            }
            else
            {
                Session["nomenu"] = 1;
            }

        }
    }



}

