using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using SMS.Data;

/// <summary>
/// Summary description for DAL_SEP_Task
/// </summary>
public sealed class DAL_SEP_Task
{
    string Connection = ConfigurationManager.ConnectionStrings["smsconn"].ToString();

    public void AddNewUser(string Fname, string Lname, string Mname, string User, string Pwd, string Email, string OrgID, string role, int AccessLevel, int ReportingTo)
    {
        string paswrd = Pwd;

        SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@Fname",Fname),
                                          new SqlParameter("@Lname",Lname),
                                          new SqlParameter("@Mname",Mname),
                                          new SqlParameter("@Username",User),
                                          new SqlParameter("@Pwd",paswrd),
                                          new SqlParameter("@Email",Email),
                                          new SqlParameter("@OrgID",OrgID),
                                          new SqlParameter("@role",role),
                                          new SqlParameter("@AccessLevel",AccessLevel),
                                          new SqlParameter("@ReportingTo",ReportingTo)
                                         };

        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_AddNewUser", sqlprm);
    }
    public DataSet UsersDetails(string FLAG)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Flag",FLAG)
                                        };
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_UserDetails", sqlprm);
    }
    public DataSet ProjectDetails(string ORG_ID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@OrgID",ORG_ID)
                                        };

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_ProjectDetails", sqlprm);
    }
    public DataSet ProjectDetForUserProfile(int userID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",userID)
                                        };

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_ProjectDetForUserProfile", sqlprm);
    }

    public DataSet getProfileDetailsbyUser(int UserID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@userID",UserID)
                                        };

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getProfiledetail", sqlprm);
    }

    public DataSet getModuleDetails(int OrgID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@OrgID",OrgID)
                                        };

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getModuleDetails", sqlprm);
    }

    public DataSet ProjectDetailsForUserProfile(int UserID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@userID",UserID)
                                        };

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_ProjecDetForUserProfile", sqlprm);
    }

    public DataSet ModuleDetails(string FLAG)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ProjectID",FLAG)
                                        };

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_ModuleDetails", sqlprm);
    }

    public DataSet GetUserIDByName()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_GetUserIDByName");
    }

    public DataSet getVesselList()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_GetVesselList");
    }

    public DataSet GetUsersByOrganisation(string OrgID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@OrgID",OrgID)
                                        };
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_GetUsersByOrganisation", sqlprm);
    }
    public DataSet getTotalleaveInfoByUser(int UserID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getTotalleaveInfoByUser", sqlprm);
    }

    public DataSet getApplicantName(int UserID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",SqlDbType.Int)
                                                     };
        sqlprm[0].Value = UserID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getApplicantName", sqlprm);

    }

    public DataSet getLeaveStatusForUser(int UserID, int month)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID),
                                              new SqlParameter("@Month",month)
                                        };
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getLeaveStatusForUser", sqlprm);
    }
    public int getLeaveDaysForUser(int UserID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",SqlDbType.Int),
                                             new SqlParameter("@@LeaveDaysValue",SqlDbType.Int)
                                        };
        sqlprm[0].Value = UserID;
        sqlprm[1].Direction = ParameterDirection.ReturnValue;
        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_getLeaveDaysForUser", sqlprm);
        return Convert.ToInt32(sqlprm[1].Value);
    }
    public DataSet getLeaveStatusByApplication(int ApplicationID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ApplicationID",ApplicationID)                                            
                                        };
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getLeaveStatusByApplication", sqlprm);
    }
    public void updateLeaveDaysByUser(int UserID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID)
                                        };
        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_updateLeaveDaysByUser", sqlprm);
    }
    public void updateLeaveApprovalStatusInfo(int ApplicationID, int UserID, int MgrID, int MgrLevel)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ApplicationID",ApplicationID),
                                             new SqlParameter("@UserID",UserID),
                                             new SqlParameter("@MgrID",MgrID),
                                             new SqlParameter("@MgrLevel",MgrLevel)
                                        };
        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_updateApprovalInfo", sqlprm);
    }
    public DataSet UpdateLeaveApprovalStatusInfo(int ApplicationID, int MgrID, int Status, string Remarks, int userID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ApplicationID",ApplicationID),
                                            new SqlParameter("@MgrID",MgrID),
                                            new SqlParameter("@Status",Status),
                                            new SqlParameter("@Remarks",Remarks),
                                             new SqlParameter("@userID",userID)
                                        };
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_UpdateLeaveApproval", sqlprm);
    }
    public int AddNewModule(string projectname, string moduleame, string stDate, string EndDate, int lead)
    {
        IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
        DateTime startDt = DateTime.Parse(stDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        DateTime endDt = DateTime.Parse(EndDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

        SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@projectname",SqlDbType.VarChar,200),
                                          new SqlParameter("@moduleame",SqlDbType.VarChar,200),
                                          new SqlParameter("@stDate",SqlDbType.DateTime),
                                          new SqlParameter("@Edntdt",SqlDbType.DateTime),
                                          new SqlParameter("@lead",SqlDbType.Int),
                                          new SqlParameter("@return",SqlDbType.Int)
                                        };
        sqlprm[0].Value = projectname;
        sqlprm[1].Value = moduleame;
        sqlprm[2].Value = startDt;
        sqlprm[3].Value = endDt;
        sqlprm[4].Value = lead;
        sqlprm[5].Direction = ParameterDirection.ReturnValue;
        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_AddNewModule", sqlprm);
        return Int32.Parse(sqlprm[5].Value.ToString());
    }
    public void AddNewProject(string projectname, int typeproj, string stDate, string EndDate, int lead, string location, string OrgId, int projID)
    {
        stDate = "1900/01/01";
        EndDate = "1900/01/01";
        IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
        DateTime startDt = DateTime.Parse(stDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        DateTime endDt = DateTime.Parse(EndDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

        SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@projectname",SqlDbType.VarChar,200),
                                          new SqlParameter("@typeproj",SqlDbType.VarChar,200),
                                          new SqlParameter("@stDate",SqlDbType.DateTime),
                                          new SqlParameter("@Edntdt",SqlDbType.DateTime),
                                          new SqlParameter("@lead",SqlDbType.Int),
                                          new SqlParameter("@location",SqlDbType.VarChar,200),
                                          new SqlParameter("@OrgID",SqlDbType.VarChar,10),
                                           new SqlParameter("@projID",SqlDbType.Int)
                                        };

        sqlprm[0].Value = projectname;
        sqlprm[1].Value = typeproj;
        sqlprm[2].Value = startDt;
        sqlprm[3].Value = endDt;
        sqlprm[4].Value = lead;
        sqlprm[5].Value = location;
        sqlprm[6].Value = OrgId;
        sqlprm[7].Value = projID;

        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_AddNewProject", sqlprm);
    }
    public void AddNewTask(string projectid, string task, string stDate, string Edntdt, string lead, string depends)
    {

        SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@projectid",projectid),
                                          new SqlParameter("@task",task),
                                          new SqlParameter("@stDate",stDate),
                                          new SqlParameter("@Edntdt",Edntdt),
                                          new SqlParameter("@lead",lead),
                                          new SqlParameter("@depends",depends)
                                        };

        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_AddNewTask", sqlprm);
    }
    public string GetTaskID(string ProjectId)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@ProjectId",ProjectId)
                                       };

        return SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_bugID", sqlprm).ToString(); ;
    }
    public int AddnewBug(int projId, int Moduid, int clientID, string compo, string type, string perority, string Sevr, int status, int rptby, string SeenVer, string subj, string desc, int VesselCode, string mailReciept, DateTime StartDate, DateTime EndDate)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                            new SqlParameter("@projId",SqlDbType.Int),
                                            new SqlParameter("@Moduid",SqlDbType.Int),
                                            new SqlParameter("@ClientID",SqlDbType.Int),
                                            new SqlParameter("@compo",SqlDbType.VarChar),
                                            new SqlParameter("@type",SqlDbType.VarChar,20),
                                            new SqlParameter("@perority",SqlDbType.VarChar,20),
                                            new SqlParameter("@Sevr",SqlDbType.VarChar,20),
                                            new SqlParameter("@status",SqlDbType.Int),
                                            new SqlParameter("@rptby",SqlDbType.Int),
                                            new SqlParameter("@SeenVe",SqlDbType.VarChar,20),
                                            new SqlParameter("@subj",SqlDbType.VarChar,5000),
                                            new SqlParameter("@desc",SqlDbType.VarChar,5000),
                                            new SqlParameter("@bugid",SqlDbType.Int),
                                             new SqlParameter("@VesselCode",SqlDbType.Int),
                                            new SqlParameter("@mailReciept",SqlDbType.VarChar,500),
                                            new SqlParameter("@StartDate",SqlDbType.DateTime),
                                            new SqlParameter("@EndDate",SqlDbType.DateTime)

                                         
                                       };
        sqlprm[0].Value = projId;
        sqlprm[1].Value = Moduid;
        sqlprm[2].Value = clientID;
        sqlprm[3].Value = compo;
        sqlprm[4].Value = type;
        sqlprm[5].Value = perority;
        sqlprm[6].Value = Sevr;
        sqlprm[7].Value = status;
        sqlprm[8].Value = rptby;
        sqlprm[9].Value = SeenVer;
        sqlprm[10].Value = subj;
        sqlprm[11].Value = desc;
        sqlprm[12].Direction = ParameterDirection.ReturnValue;
        sqlprm[13].Value = VesselCode;
        sqlprm[14].Value = mailReciept;
        sqlprm[15].Value = StartDate;
        sqlprm[16].Value = EndDate;

        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_NewBugAdd", sqlprm);
        return Convert.ToInt32(sqlprm[12].Value);

    }
    public string BUGATTACH(string BUGID, string FULLDECRIP, string PATH)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@BUGID",BUGID),
                                            new SqlParameter("@ddESCR",FULLDECRIP),
                                            new SqlParameter("@PATH",PATH)
                                       };
        return SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_BUGaTTACH", sqlprm).ToString(); ;
    }
    public DataSet Tasklist()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_TaskList");
    }
    public DataSet Tasklist_Apply_Filter(string Querry)
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.Text, Querry.ToString());
    }
    public DataSet BugDetails(int bugId)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@BUGID",bugId)
                                       };
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_BugDetails", sqlprm);
    }
    public DataSet BugDetailsbyID(string bugId)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@BUGID",bugId)
                                       };
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_BugDetailsbyID", sqlprm);

    }

    public int getImageCount(int bugID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@BUGID",SqlDbType.Int),
                                            new SqlParameter("@ImageCount",SqlDbType.Int)
                                       };
        sqlprm[0].Value = bugID;
        sqlprm[1].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_getUploadImageCount", sqlprm);
        return Convert.ToInt32(sqlprm[1].Value);
    }

    public void AssignTask(int bugid, int AssignedTo, string StDate, string EnDate, int AssignedBy)
    {

        IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
        DateTime startDt = DateTime.Parse(StDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        DateTime endDt = DateTime.Parse(EnDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@Bugid",SqlDbType.Int),
                                           new SqlParameter("@AssignedTo",SqlDbType.Int),
                                           new SqlParameter("@StDate",SqlDbType.DateTime),
                                           new SqlParameter("@EnDate",SqlDbType.DateTime),
                                           new SqlParameter("@AssignedBy",SqlDbType.Int)
                                          
                                       };
        sqlprm[0].Value = bugid;
        sqlprm[1].Value = AssignedTo;
        sqlprm[2].Value = startDt;
        sqlprm[3].Value = endDt;
        sqlprm[4].Value = AssignedBy;
        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_AssignTask", sqlprm);
    }
    public string Authentication(string username, string pasw)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@username",username),
                                           new SqlParameter("@PASSWORD",pasw)
                                       };
        return Convert.ToString(SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_Authentication", sqlprm));
    }

    public string Get_User_Role(int UserID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@UserID",UserID)
                                       };
        return Convert.ToString(SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_Get_User_Role", sqlprm));
    }
    public string Get_User_AccessLevel(int UserID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@UserID",UserID)
                                       };
        return Convert.ToString(SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_Get_User_AccessLevel", sqlprm));
    }
    public DataSet Tasklist(string username)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@username",username)
                                       };
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_TaskListByUser", sqlprm);


    }

    public DataSet MailID(int UserID, int UserIDToSendMail, int BugId)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@UserID",UserID),
                                           new SqlParameter("@UserIDToSendMail",UserIDToSendMail),
                                           new SqlParameter("@BugId",BugId)
                                       };
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_MailId", sqlprm);
    }
    public void SaveComments(int Bug_Id, int username, string Comments)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@Bug_Id",Bug_Id),
                                           new SqlParameter("@userid",username),
                                           new SqlParameter("@Comments",Comments)
                                       };
        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_PostComments", sqlprm);
    }
    public DataSet UpdateStatus(string bugid, string status)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@Bug_Id",bugid),
                                           new SqlParameter("@status",status)
                                           
                                       };
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_updatestatus", sqlprm);
    }

    public DataSet getUserListOfOrganisation(int userID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@UserID",userID)
                                                                                    
                                       };
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getUserListOfOrganisation", sqlprm);
    }


    public DateTime getLastAssignedDate(int bugid)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@Bug_Id",SqlDbType.Int),
                                           new SqlParameter("@dt",SqlDbType.DateTime)
                                       };
        sqlprm[0].Value = bugid;
        sqlprm[1].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_getLastAssignedDate", sqlprm);
        return Convert.ToDateTime(sqlprm[1].Value.ToString());
    }

    public DataSet checkForUserLeaveYearly(int userID)
    {

        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@UserID",SqlDbType.Int)
                                          
                                       };
        sqlprm[0].Value = userID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_checkForUserLeaveYearly", sqlprm);
    }

    public DataSet getUserListByOrg(int userID)
    {

        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@UserID",SqlDbType.Int)
                                          
                                       };
        sqlprm[0].Value = userID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getUserListByOrg", sqlprm);
    }
    public DataSet LeaveSummaryByUser(int userID)
    {

        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@UserID",SqlDbType.Int)
                                          
                                       };
        sqlprm[0].Value = userID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_LeaveSummaryByUser", sqlprm);
    }
    public void updateLeaveInfoOfUserInYear(int userID, int year, int month)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@UserID",SqlDbType.Int),
                                           new SqlParameter("@Year",SqlDbType.Int),
                                           new SqlParameter("@Month",SqlDbType.Int)
                                       };

        sqlprm[0].Value = userID;
        sqlprm[1].Value = year;
        sqlprm[2].Value = month;

        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_updateLeaveInfoOfUserInYear", sqlprm);
    }

    public void updateLeaveByAdmin(int month, int userID, Double BF, Double Earned, Double days)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                            new SqlParameter("@Month",SqlDbType.Float),
                                           new SqlParameter("@UserID",SqlDbType.Float),
                                           new SqlParameter("@BF",SqlDbType.Float),
                                           new SqlParameter("@Earned",SqlDbType.Float),
                                            new SqlParameter("@days",SqlDbType.Float)
                                       };
        sqlprm[0].Value = month;
        sqlprm[1].Value = userID;
        sqlprm[2].Value = BF;
        sqlprm[3].Value = Earned;
        sqlprm[4].Value = days;

        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_updateLeaveByAdmin", sqlprm);
    }

    public DataSet getUsersDetailsByUserID(string userid)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@userid",userid)
                                       };
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_UserDetailsbyId", sqlprm);
    }
    public void UpdateUser(string Fname, string Lname, string Mname, string User, string Email, string Desing, string role, string Type)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@Fname",Fname),
                                          new SqlParameter("@Lname",Lname),
                                          new SqlParameter("@Mname",Mname),
                                          new SqlParameter("@User",User),
                                          new SqlParameter("@Email",Email),
                                          new SqlParameter("@Desing",Desing),
                                          new SqlParameter("@role",role),
                                          new SqlParameter("@Type",Type)
                                         };
        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_UpdateUser", sqlprm);
    }
    public DataSet Project_Apply_Filter(string querry)
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.Text, querry);
    }
    public DataSet User_Apply_Filter(string querry)
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.Text, querry);
    }
    public DataSet UserRoles()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getUserRoles");
    }
    public DataSet Organisations()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getOrgList");
    }
    public DataSet AccessLevel()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getAccessLevel");
    }
    public void DeleteUserByUserID(string userID)
    {
        SqlParameter obj = new SqlParameter("@UserID", SqlDbType.Int);
        obj.Value = userID;
        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_DeleteUserByUserID", obj);
    }
    public int getDeleteStatusOfProject(string ProjectID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@Project_ID",SqlDbType.Int),
                                            new SqlParameter("@BugStatus",SqlDbType.Int)
                                       };
        sqlprm[0].Value = ProjectID;
        sqlprm[1].Direction = ParameterDirection.ReturnValue;
        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_getDeleteStatusOfProject", sqlprm);
        return Convert.ToInt32(sqlprm[1].Value);
    }
    public void UpdateBugDetails(int clientID, string Prty, string srvr, string version, string status, string descrp, string BugID, string subject, int ProjectID, int ModuleID)
    {

        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@clientID", SqlDbType.Int),
                                           new SqlParameter("@Prty", SqlDbType.VarChar,200),
                                           new SqlParameter("@srvr", SqlDbType.VarChar,200),
                                           new SqlParameter("@version", SqlDbType.VarChar,200),
                                           new SqlParameter("@status", SqlDbType.VarChar,200),
                                           new SqlParameter("@descrp", SqlDbType.VarChar,4000),
                                           new SqlParameter("@BugID", SqlDbType.VarChar,200),
                                           new SqlParameter("@subject", SqlDbType.VarChar,4000),
                                           new SqlParameter("@ProjectID", SqlDbType.Int),
                                           new SqlParameter("@ModuleID", SqlDbType.Int)
                                       };
        sqlprm[0].Value = clientID;
        sqlprm[1].Value = Prty;
        sqlprm[2].Value = srvr;
        sqlprm[3].Value = version;
        sqlprm[4].Value = status;
        sqlprm[5].Value = descrp;
        sqlprm[6].Value = BugID;
        sqlprm[7].Value = subject;
        sqlprm[8].Value = ProjectID;
        sqlprm[9].Value = ModuleID;
        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_BugUpdate", sqlprm);
    }
    public void EditUpdationBugLog_Info(int Bug_Id, string Subject, string Description, int status, int ReptBy, int assign, string perority, string Sevrity, int bugEditedBy, string updationText)
    {
        SqlParameter[] obj = new SqlParameter[]
                                       {
                                           new SqlParameter("@BugID",SqlDbType.Int),
                                           new SqlParameter("@Subject",SqlDbType.VarChar,1000),
                                           new SqlParameter("@description",SqlDbType.VarChar,5000),
                                           new SqlParameter("@Status",SqlDbType.Int),
                                           new SqlParameter("@ReportedBy",SqlDbType.Int),
                                           new SqlParameter("@AssignedTo",SqlDbType.Int),
                                           new SqlParameter("@Periority ",SqlDbType.VarChar,50),
                                           new SqlParameter("@Severity",SqlDbType.VarChar,50),
                                           new SqlParameter("@bugEditedBy",SqlDbType.Int),
                                           new SqlParameter("@BugEditedText",SqlDbType.VarChar,100)
                                           };
        obj[0].Value = Bug_Id;
        obj[1].Value = Subject;
        obj[2].Value = Description;
        obj[3].Value = status;
        obj[4].Value = ReptBy;
        obj[5].Value = assign;
        obj[6].Value = perority;
        obj[7].Value = Sevrity;
        obj[8].Value = bugEditedBy;
        obj[9].Value = updationText;
        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_EditUpdationBugLog_Info", obj);
    }
    public void UpdatePwd(int username, string NewPwd, string PWd)
    {
        string paswrd =PWd;
        string Npaswrd =NewPwd;
        SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@username",username),
                                          new SqlParameter("@Pwd",Npaswrd),
                                          new SqlParameter("@NewPwd",paswrd)
                                        };
        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_ChangePassword", sqlprm);
    }
    public DataSet ModuleDetailsByProject(string ProjectID)
    {
        SqlParameter sqlprm = new SqlParameter("@ProjectID", SqlDbType.Int);
        sqlprm.Value = ProjectID;

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getModuleDetailsByProject", sqlprm);
    }
    public DataSet ProjectforXML()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getProjectforXML");
    }
    public DataSet ModuleforXML()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getModuleforXML");
    }
    public DataSet BugsInProgress_ProjectsXML()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_GetBugsInprogressProjects");
    }
    public DataSet BugsInProgressXML()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_GetBugsInprogress");
    }
    public DataSet ProjectPlanXML()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_ProjectPlan");
    }
    public DataSet ProjectPlanDetailXML()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_Dtl_ProjectPlan");
    }
    public DataSet getBugDetailsByID(int bugID)
    {

        SqlParameter sqlprm = new SqlParameter("@BUGID", SqlDbType.Int);
        sqlprm.Value = bugID;

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getBugDetailsByID", sqlprm);
    }
    public DataSet getuserInfo(string userName)
    {
        SqlParameter sqlprm = new SqlParameter("@userName", SqlDbType.VarChar, 500);
        sqlprm.Value = userName;

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getuserInfo", sqlprm);
    }
    public int getUserbyBugID(int userID, int bugID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                        {
                                          new SqlParameter("@userID",SqlDbType.Int),
                                          new SqlParameter("@bugID",SqlDbType.Int),
                                          new SqlParameter("@UserStatus",SqlDbType.Int)
                                        };
        sqlprm[0].Value = userID;
        sqlprm[1].Value = bugID;
        sqlprm[2].Direction = ParameterDirection.ReturnValue;
        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_getLatestUserInfo", sqlprm);
        return Convert.ToInt32(sqlprm[2].Value);
    }
    public DataSet getSystemParameterList(string paramName)
    {
        SqlParameter sqlprm = new SqlParameter("@paramName", SqlDbType.VarChar, 500);
        sqlprm.Value = paramName.ToUpper();

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getSystemParameterList", sqlprm);
    }

    public DataSet getTypeList()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getTypeList");
    }
    public DataSet getReportingToManagerList(int org_ID)
    {
        SqlParameter sqlprm = new SqlParameter("@orgID", SqlDbType.Int);
        sqlprm.Value = org_ID;

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getReportingToManagerList", sqlprm);
    }

    public DataSet getModuleDetailsByProjectID(int projectID)
    {
        SqlParameter sqlprm = new SqlParameter("@projectID", SqlDbType.Int);
        sqlprm.Value = projectID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getModuleDtlByProjectID", sqlprm);
    }


    public DataSet getSystemParameterList(string paramName, string filter)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                    {
                                        new SqlParameter("@paramName", SqlDbType.VarChar, 500),
                                        new SqlParameter("@filter", SqlDbType.VarChar, 500)
                                    };

        sqlprm[0].Value = paramName.ToUpper();
        sqlprm[1].Value = filter;

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getSystemParameterListWithFilter", sqlprm);
    }
    public DataSet getSystemParameterList(string paramName, string filter, int OrderDesc)
    {
        string queryText = "";
        paramName = paramName.ToUpper();
        if (OrderDesc == 1)
            queryText = "SELECT a.ID as ID, a.Name,a.FILTER  FROM SEP_Task_SystemParameters AS a INNER JOIN SEP_Task_SystemParameters AS b ON a.Prarent_Code = b.ID WHERE (upper(b.Name) = '" + paramName.ToUpper() + "' and a.filter = '" + filter + "')  order by a.DisplayOrder desc";
        else
            queryText = "SELECT a.ID as ID, a.Name,a.FILTER  FROM SEP_Task_SystemParameters AS a INNER JOIN SEP_Task_SystemParameters AS b ON a.Prarent_Code = b.ID WHERE (upper(b.Name) = '" + paramName.ToUpper() + "' and a.filter = '" + filter + "')  order by a.DisplayOrder";

        return SqlHelper.ExecuteDataset(Connection, CommandType.Text, queryText);
    }
    public void updateClosingDate(int bugID)
    {
        SqlParameter[] obj = new SqlParameter[]
             {
                 new SqlParameter("@BugID", SqlDbType.Int)
             };
        obj[0].Value = bugID;
        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_UpdateClosingDate", obj);
    }
    public void updateCompletionDate(int bugID, DateTime EndDate, DateTime StartDate)
    {
        SqlParameter[] obj = new SqlParameter[]
             {
                 new SqlParameter("@BugID", SqlDbType.Int),
                 new SqlParameter("@EndDate", SqlDbType.DateTime),
                 new SqlParameter("@startDT",SqlDbType.DateTime)
             };
        obj[0].Value = bugID;
        obj[1].Value = EndDate;
        obj[2].Value = StartDate;

        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_UpdateCompletionDate", obj);
    }
    public void updateModuleInfo(string ModuleID, string ModuleName, string StartDate, string EndDate, string ModuleLead)
    {


        DateTime startDt = DateTime.Parse(StartDate);
        DateTime endDt = DateTime.Parse(EndDate);

        SqlParameter[] obj = new SqlParameter[]
             {   new SqlParameter("@ModuleName", SqlDbType.VarChar, 200),
                 new SqlParameter("@StartDate", SqlDbType.DateTime),
                 new SqlParameter("@EndDate", SqlDbType.DateTime),
                 new SqlParameter("@ModuleLeadID",  SqlDbType.VarChar, 200),
                 new SqlParameter("@ModuleID",  SqlDbType.VarChar, 20),

                };
        obj[0].Value = ModuleName;
        obj[1].Value = startDt;
        obj[2].Value = endDt;
        obj[3].Value = ModuleLead;
        obj[4].Value = ModuleID;
        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_UpdateModuleInfo", obj);
    }
    public void updateProjectInfo(int ProjectID, int ProjectLeadID, string ProjectName, int ProjectType, string StartDate, string endDate, string Location, int projID, int orgid)
    {

        StartDate = "1900/01/01";
        endDate = "1900/01/01";
        IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
        DateTime startDt = DateTime.Parse(StartDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        DateTime endDt = DateTime.Parse(endDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

        SqlParameter[] obj = new SqlParameter[]
             {
                 new SqlParameter("@ProjectID", SqlDbType.Int),
                 new SqlParameter("@ProjectLeadID", SqlDbType.Int),
                 new SqlParameter("@ProjectName", SqlDbType.VarChar,500),
                 new SqlParameter("@ProjectType", SqlDbType.Int),
                 new SqlParameter("@StartDate",  SqlDbType.DateTime),
                 new SqlParameter("@EndDate",  SqlDbType.DateTime),
                 new SqlParameter("@Location", SqlDbType.VarChar,500),
                 new SqlParameter("@ProjectManager", SqlDbType.Int),
                 new SqlParameter("@orgid",SqlDbType.Int)
                };
        obj[0].Value = ProjectID;
        obj[1].Value = ProjectLeadID;
        obj[2].Value = ProjectName;
        obj[3].Value = ProjectType;
        obj[4].Value = startDt;
        obj[5].Value = endDt;
        obj[6].Value = Location;
        obj[7].Value = projID;
        obj[8].Value = orgid;

        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_UpdateProjectInfo", obj);
    }


    public int DeleteModuleInfoByID(string ModuleID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@ModuleID",SqlDbType.VarChar,50),
                                           new SqlParameter("@BugStatus",SqlDbType.Int)
                                       };
        sqlprm[0].Value = ModuleID;
        sqlprm[1].Direction = ParameterDirection.ReturnValue;
        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_DeleteModuleInfoByID", sqlprm);
        return Convert.ToInt32(sqlprm[1].Value);
    }
    public DataSet getOpenBugIDs(int org_id)
    {

        SqlParameter sqlprm = new SqlParameter("@org_Id", SqlDbType.Int);
        sqlprm.Value = org_id;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getOpenBugIDs", sqlprm);
    }
    public DataSet getFixedBugIDs()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getFixedBugIDs");
    }
    public DataSet getFixedNClosedBugs()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_GetBugsFixedNClosed");
    }
    public string getStatusName(int bugid)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@Bug_Id",SqlDbType.Int),
                                           new SqlParameter("@status",SqlDbType.VarChar,20)
                                       };
        sqlprm[0].Value = bugid;
        sqlprm[1].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_getStatusName", sqlprm);

        return sqlprm[1].Value.ToString();
    }
    public DataSet getMailInfo(int bugid, int loggedUser)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@Bug_Id",SqlDbType.Int),
                                           new SqlParameter("@loggedUser",SqlDbType.Int)
                                       };
        sqlprm[0].Value = bugid;
        sqlprm[1].Value = loggedUser;

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getMailInfo", sqlprm);

    }



    public DataSet getMailInfoForCreateTask(int bugid)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@Bug_Id",SqlDbType.Int)
                                       };
        sqlprm[0].Value = bugid;

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getMailInfoForCreateTask", sqlprm);

    }

    public DataSet getMailReceipients(int projid, int modid, int reported_by)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@projid",SqlDbType.Int),
                                           new SqlParameter("@modid",SqlDbType.Int),
                                           new SqlParameter("@reported_by",SqlDbType.Int)
                                       };
        sqlprm[0].Value = projid;
        sqlprm[1].Value = modid;
        sqlprm[2].Value = reported_by;

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getMailReceipients", sqlprm);

    }

    public DataSet getMailInfoForMgrLead(int bugid, int orgID, int projectID)
    {
        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@Bug_Id",SqlDbType.Int),
                                           new SqlParameter("@Org_Id",SqlDbType.Int),
                                           new SqlParameter("@Project_Id",SqlDbType.Int)
                                       };
        sqlprm[0].Value = bugid;
        sqlprm[1].Value = orgID;
        sqlprm[2].Value = projectID;

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getMailInfoForMgrLead", sqlprm);

    }


    //public void UpdateBugStatusById(int BugID, int StatusId)
    //{
    //    SqlParameter[] obj = new SqlParameter[]
    //         {
    //             new SqlParameter("@BugID", SqlDbType.Int),
    //             new SqlParameter("@StatusId", SqlDbType.Int)
    //         };
    //    obj[0].Value = BugID;
    //    obj[1].Value = StatusId;

    //    SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_UpdateBugStatusById", obj);
    //}
    public void UpdateBugStatusById(int BugID, int StatusId, string Remarks)
    {
        SqlParameter[] obj = new SqlParameter[]
             {
                 new SqlParameter("@BugID", SqlDbType.Int),
                 new SqlParameter("@StatusId", SqlDbType.Int),
                 new SqlParameter("@StatusRemarks", SqlDbType.VarChar)
             };
        obj[0].Value = BugID;
        obj[1].Value = StatusId;
        obj[2].Value = Remarks;
        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_UpdateBugStatusById", obj);
    }
    public void NewUpdationBug_LogInfo(int bugID, int projectid, int Moduleid, string subj, string desc, string status, string type, string perority, string Sevr, string rptby, string SeenVer, string compo, string logDate, string updatedText, string createdBy)
    {
        IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
        DateTime logDateText = DateTime.Parse(logDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

        SqlParameter[] obj = new SqlParameter[]
                                       {  
                                           new SqlParameter("@BugID",SqlDbType.Int),
                                           new SqlParameter("@ProjectId",SqlDbType.Int),
                                           new SqlParameter("@ModuleId",SqlDbType.Int),
                                           new SqlParameter("@Subject",SqlDbType.VarChar,1000),
                                           new SqlParameter("@description",SqlDbType.VarChar,5000),
                                           new SqlParameter("@Status",SqlDbType.VarChar,50),
                          
                                           new SqlParameter("@Type",SqlDbType.VarChar,50),
                                           new SqlParameter("@Periority ",SqlDbType.VarChar,50),
                                           new SqlParameter("@Severity",SqlDbType.VarChar,50),
                                           new SqlParameter("@ReportedBy",SqlDbType.VarChar,50),
                                           new SqlParameter("@SeenVersion",SqlDbType.VarChar,50),
                                                                                    
                                           new SqlParameter("@Component",SqlDbType.VarChar,200),
                                           new SqlParameter("@LogDate",SqlDbType.DateTime),
                                           new SqlParameter("@UpdatedText",SqlDbType.VarChar,200) , 
                                           new SqlParameter("@createdBy",SqlDbType.VarChar,200)
      
                           };

        obj[0].Value = bugID;
        obj[1].Value = projectid;
        obj[2].Value = Moduleid;
        obj[3].Value = subj;
        obj[4].Value = desc;
        obj[5].Value = status;
        obj[6].Value = type;
        obj[7].Value = perority;
        obj[8].Value = Sevr;
        obj[9].Value = rptby;
        obj[10].Value = SeenVer;
        obj[11].Value = compo;
        obj[12].Value = logDateText;
        obj[13].Value = updatedText;
        obj[14].Value = createdBy;

        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_NewUpdationBug_LogInfo", obj);
    }
    public void AssignUpdateLog_InfoByID(int bugid, int AssignedTo, string StDate, string EnDate, int AssignedBy, string AssignUpdatedText)
    {

        IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
        DateTime startDt = DateTime.Parse(StDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        DateTime endDt = DateTime.Parse(EnDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

        SqlParameter[] sqlprm = new SqlParameter[]
                                       {
                                           new SqlParameter("@Bugid",SqlDbType.Int),
                                           new SqlParameter("@AssignedTo",SqlDbType.Int),
                                           new SqlParameter("@StDate",SqlDbType.DateTime),
                                           new SqlParameter("@EnDate",SqlDbType.DateTime),
                                           new SqlParameter("@AssignedBy",SqlDbType.Int),
                                           new SqlParameter("@upatedtext",SqlDbType.VarChar,200)
                                          
                                       };
        sqlprm[0].Value = bugid;
        sqlprm[1].Value = AssignedTo;
        sqlprm[2].Value = startDt;
        sqlprm[3].Value = endDt;
        sqlprm[4].Value = AssignedBy;
        sqlprm[5].Value = AssignUpdatedText;
        SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "AssignUpdateLog_InfoByID", sqlprm);
    }
    public void StatusUpdationBugLog_Info(int BugID, int StatusId, int updatedBy, string updatedText)
    {
        SqlParameter[] obj = new SqlParameter[]
             {
                 new SqlParameter("@BugID", SqlDbType.Int),
                 new SqlParameter("@StatusId", SqlDbType.Int),
                 new SqlParameter("@StatusChangedBy", SqlDbType.Int),
                 new SqlParameter("@BugEditedText", SqlDbType.VarChar,100)
             };
        obj[0].Value = BugID;
        obj[1].Value = StatusId;
        obj[2].Value = updatedBy;
        obj[3].Value = updatedText;

        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_StatusUpdationBugLog_Info", obj);
    }
    public int InsertLeaveInfoByUser(int UserID, string UserName, string fromDate, string toDate, int TotalDays, int TotalWorkingDays, int LeaveDays, string LeaveType, string Reason, string Contactdetails, string SystemInformation, int unpaidDays)
    {
        IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
        DateTime FromDateText = DateTime.Parse(fromDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        DateTime toDateText = DateTime.Parse(toDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

        SqlParameter[] obj = new SqlParameter[]
             {
                 new SqlParameter("@UserID", SqlDbType.Int),
                 new SqlParameter("@UserName", SqlDbType.VarChar,200),
                 new SqlParameter("@fromDate", SqlDbType.DateTime),
                 new SqlParameter("@toDate", SqlDbType.DateTime),
                 new SqlParameter("@TotalDays", SqlDbType.Int),
                 new SqlParameter("@Earned_Days", SqlDbType.Int),
                 new SqlParameter("@Unpaid_Days", SqlDbType.Int),
                 new SqlParameter("@TotalWorkingDays", SqlDbType.Int),
                 new SqlParameter("@Leavedays", SqlDbType.Int),
                 new SqlParameter("@LeaveType", SqlDbType.VarChar,200),
                 new SqlParameter("@Reason", SqlDbType.VarChar,2000),
                 new SqlParameter("@Contactdetails", SqlDbType.VarChar,2000),
                 new SqlParameter("@SystemInformation", SqlDbType.VarChar,2000),
                 new SqlParameter("@ApplicationID", SqlDbType.Int)
             };

        obj[0].Value = UserID;
        obj[1].Value = UserName;
        obj[2].Value = FromDateText;
        obj[3].Value = toDateText;
        obj[4].Value = TotalDays;
        obj[5].Value = TotalWorkingDays;
        obj[6].Value = unpaidDays;
        obj[7].Value = TotalWorkingDays;
        obj[8].Value = LeaveDays;
        obj[9].Value = LeaveType;
        obj[10].Value = Reason;
        obj[11].Value = Contactdetails;
        obj[12].Value = SystemInformation;
        obj[13].Direction = ParameterDirection.ReturnValue;

        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_InsertLeaveInfoByUser", obj);
        return Convert.ToInt32(obj[13].Value.ToString());
    }
    public void UpdateLeaveInfoByUser(int applicationID, int UserID, string UserName, string fromDate, string toDate, int TotalDays, int TotalWorkingDays, int LeaveDays, string LeaveType, string Reason, string Contactdetails, string SystemInformation)
    {
        IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
        DateTime FromDateText = DateTime.Parse(fromDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        DateTime toDateText = DateTime.Parse(toDate, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

        SqlParameter[] obj = new SqlParameter[]
             {
                 new SqlParameter("@UserID", SqlDbType.Int),
                 new SqlParameter("@UserName", SqlDbType.VarChar,200),
                 new SqlParameter("@fromDate", SqlDbType.DateTime),
                 new SqlParameter("@toDate", SqlDbType.DateTime),
                 new SqlParameter("@TotalDays", SqlDbType.Int),
                 new SqlParameter("@TotalWorkingDays", SqlDbType.Int),
                 new SqlParameter("@LeaveDays", SqlDbType.Int),
                 new SqlParameter("@LeaveType", SqlDbType.VarChar,200),
                 new SqlParameter("@Reason", SqlDbType.VarChar,2000),
                 new SqlParameter("@Contactdetails", SqlDbType.VarChar,2000),
                 new SqlParameter("@SystemInformation", SqlDbType.VarChar,2000),
                 new SqlParameter("@ApplicationID", SqlDbType.Int)
             };

        obj[0].Value = UserID;
        obj[1].Value = UserName;
        obj[2].Value = FromDateText;
        obj[3].Value = toDateText;
        obj[4].Value = TotalDays;
        obj[5].Value = TotalWorkingDays;
        obj[6].Value = LeaveDays;
        obj[7].Value = LeaveType;
        obj[8].Value = Reason;
        obj[9].Value = Contactdetails;
        obj[10].Value = SystemInformation;
        obj[11].Value = applicationID;

        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_UpdateLeaveInfoByUser", obj);
    }
    public DataSet getLeaveInfoByUser(int UserID)
    {
        SqlParameter obj = new SqlParameter("@UserID", SqlDbType.Int);
        obj.Value = UserID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getLeaveInfoByUser", obj);
    }
    public DataSet getLeaveSummaryByUser(int UserID)
    {
        SqlParameter obj = new SqlParameter("@UserID", SqlDbType.Int);
        obj.Value = UserID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getLeaveSummaryByUser", obj);
    }
    public DataSet getLeaveInfoByUserForReport(int ApplicationID, int userID)
    {
        SqlParameter[] obj = new SqlParameter[]
            {
                new SqlParameter("@AppID", SqlDbType.Int),
                new SqlParameter("@UserID", SqlDbType.Int)
            };

        obj[0].Value = ApplicationID;
        obj[1].Value = userID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getLeaveInfoByUserForReport", obj);
    }
    public DataSet getleaveInfoByLeaveID(int ApplicationID)
    {
        SqlParameter obj = new SqlParameter("@AppliactionID", SqlDbType.Int);
        obj.Value = ApplicationID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getleaveInfoByLeaveID", obj);
    }
    public string DeleteApplicationIDByUser(int AppliactionID)
    {
        SqlParameter[] obj = new SqlParameter[]
            {
                new SqlParameter("@AppliactionID", SqlDbType.Int),
                new SqlParameter("@Status", SqlDbType.VarChar,100)
            };
        obj[0].Value = AppliactionID;
        obj[1].Direction = ParameterDirection.ReturnValue;
        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_DeleteApplicationIDByUser", obj);
        return obj[1].Value.ToString();
    }
    public void getUpdationLeaveLibrary(int UserID, int totalLeaves, int totalLeaveTaken, int totalLeavePending)
    {
        SqlParameter[] obj = new SqlParameter[]
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@totalLeaves", totalLeaves),
                new SqlParameter("@totalLeaveTaken", totalLeaveTaken),
                new SqlParameter("@totalLeavePending", totalLeavePending)
            };
        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_getUpdationLeaveLibrary", obj);

    }
    public DataSet getLeaveInfoByAdmin(int MgrID)
    {
        SqlParameter[] obj = new SqlParameter[]
        {
            new SqlParameter("@MgrID",SqlDbType.Int)
        };
        obj[0].Value = MgrID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getLeaveInfoByAdmin", obj);
    }
    public DataSet getMyPendingApprovals(int MgrID)
    {
        SqlParameter[] obj = new SqlParameter[]
        {
            new SqlParameter("@MgrID",SqlDbType.Int)
        };
        obj[0].Value = MgrID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getMyPendingApprovals", obj);
    }
    public DataSet getMyPendingApprovalsRatio(int UserID, int applicationID)
    {
        SqlParameter[] obj = new SqlParameter[]
        {
            new SqlParameter("@UserID",SqlDbType.Int),
            new SqlParameter("@applicationID",SqlDbType.Int)
        };

        obj[0].Value = UserID;
        obj[1].Value = applicationID;

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "getMyPendingApprovalsRatio", obj);
    }

    public DataSet getMyApprovals(int MgrID)
    {
        SqlParameter[] obj = new SqlParameter[]
        {
            new SqlParameter("@MgrID",SqlDbType.Int)
        };
        obj[0].Value = MgrID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getMyApprovals", obj);
    }
    public int getUserIDbyUsername(string userName)
    {
        SqlParameter[] obj = new SqlParameter[]
        {
            new SqlParameter ("@UserName",SqlDbType.VarChar,200),
            new SqlParameter("@UserID",SqlDbType.Int)
         };

        obj[0].Value = userName;
        obj[1].Direction = ParameterDirection.ReturnValue;
        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_getUserIDbyUsername", obj);
        return Convert.ToInt32(obj[1].Value);
    }
    public DataSet getStatusbyApplication(int applicationID)
    {
        SqlParameter[] obj = new SqlParameter[]
        {
          
            new SqlParameter("@applicationID",SqlDbType.Int)
    };
        obj[0].Value = applicationID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getStatusbyApplication", obj);

    }
    public void UpdateUserInfo(int userID, string FirstName, string MiddleName, string LastName, string MailId, string ContactNo, int AccessLevelID, int Mgr_ID)
    {

        SqlParameter[] obj = new SqlParameter[]
             {
                 new SqlParameter("@userID", SqlDbType.Int),
                 new SqlParameter("@FirstName", SqlDbType.VarChar,50),
                 new SqlParameter("@MiddleName", SqlDbType.VarChar,50),
                 new SqlParameter("@LastName", SqlDbType.VarChar,50),
                 new SqlParameter("@MailId",SqlDbType.VarChar,50),
                 new SqlParameter("@ContactNo",SqlDbType.VarChar,50),
                 new SqlParameter("@AccessLevelID", SqlDbType.Int),
                 new SqlParameter("@Mgr_ID", SqlDbType.Int)
             };

        obj[0].Value = userID;
        obj[1].Value = FirstName;
        obj[2].Value = MiddleName;
        obj[3].Value = LastName;
        obj[4].Value = MailId;
        obj[5].Value = ContactNo;
        obj[6].Value = AccessLevelID;
        obj[7].Value = Mgr_ID;

        SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_UpdateUserInfo", obj);

    }
    public void UpdateProjectForUser(int userID, string ProjectIDs)
    {
        string[] projectID = ProjectIDs.ToString().Split(',');
        SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, "delete from SEP_User_Project_Assignment where User_ID=" + userID);
        for (int i = 0; i < projectID.Length; i++)
        {
            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, "insert into SEP_User_Project_Assignment(User_ID,ProjectID) values(" + userID + "," + projectID[i] + ")");
        }

    }
    public DataSet getProjectsForUser(int userID)
    {
        SqlParameter sqlprm = new SqlParameter("@UserID", SqlDbType.Int);
        sqlprm.Value = userID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getProjectsForUser", sqlprm);
    }
    public DataSet getProjectsDetails(int orgID)
    {
        SqlParameter sqlprm = new SqlParameter("@orgID", SqlDbType.Int);
        sqlprm.Value = orgID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getProjectsDtlByOrgID", sqlprm);
    }



    public DataSet getAssignedUserListByOrg(int orgID)
    {
        SqlParameter sqlprm = new SqlParameter("@orgID", SqlDbType.Int);
        sqlprm.Value = orgID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getAssignedUserLstByOrgID", sqlprm);
    }

    public DataSet getUserListWithMail()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getUserListWithMail");
    }

    public DataSet getBugStats(int org_id)
    {
        SqlParameter param = new SqlParameter("@org_id", SqlDbType.Int);
        param.Value = org_id;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getBugStats", param);
    }
    public DataSet getHistoryInfoByMonthwise()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getHistoryInfoByMonthwise");
    }
    public DataSet GETUserReleationInfo()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_GETUserReleationInfo");
    }


    public DataSet getTotalCompanyLeave()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getTotalCompanyLeave");
    }
    public DataSet getDetailsCompanyLeave()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getDetailsCompanyLeave");
    }
    public DataSet GetUserDetails(int organisationID)
    {
        SqlParameter param = new SqlParameter("@org_id", SqlDbType.Int);
        param.Value = organisationID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_GetUserDetailsByOrgID", param);
    }

    public DataSet getVesselListFromIssues()
    {
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getVesselListFromIssues");
    }
    public DataSet getBugComments(int BugID)
    {


        SqlParameter param = new SqlParameter("@BugID", SqlDbType.Int);
        param.Value = BugID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getBugComments", param);
    }
    public DataSet getBugAttachments(string BugID)
    {
        SqlParameter param = new SqlParameter("@BugID", SqlDbType.Int);
        param.Value = BugID;
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getBugAttachments", param);
    }

    public DataSet getFixedIssues(DateTime StDt, DateTime EndDt)
    {
        SqlParameter[] param = new SqlParameter[]
             {
                 new SqlParameter("@StDt", SqlDbType.DateTime),
                 new SqlParameter("@EndDt", SqlDbType.DateTime),
             };
        param[0].Value = StDt;
        param[1].Value = EndDt;

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getFixedIssues", param);
    }


    public int INS_BuildRelease(string rls, string vsl, string bug)
    {
        SqlParameter[] param = new SqlParameter[]
             {
                 new SqlParameter("@buildRLS", rls),
                 new SqlParameter("@buildVSL", vsl),
                 new SqlParameter("@buildBUG", bug)
             };


        return SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "SEP_INS_BuildRelease", param);
    }

    public DataSet getBuildDetails(int BuildID)
    {
        SqlParameter[] param = new SqlParameter[]
             {
                 new SqlParameter("@BuildId", BuildID)
             };

        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_getBuildDetails", param);
    }
    public int AddNewToDoTask(string ToDo, string ToDoDesc, string Privacy, string Priority, string TaskID, string ParentListID, string CreatedBy)
    {

        SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@ToDo",ToDo),
                                          new SqlParameter("@ToDoDesc",ToDoDesc),
                                          new SqlParameter("@ParentListID",ParentListID),
                                          new SqlParameter("@TaskID",TaskID),
                                          new SqlParameter("@Privacy",Privacy),
                                          new SqlParameter("@Priority",Priority),
                                          new SqlParameter("@CreatedBy",CreatedBy)
                                        };

        return Convert.ToInt32(SqlHelper.ExecuteScalar(Connection, CommandType.StoredProcedure, "SEP_AddNewToDoList", sqlprm));
    }
    public DataSet GetTimeSheet(int UserID, string FromDt, string ToDt)
    {
        IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
        DateTime FromDateText = DateTime.Parse(FromDt, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        DateTime toDateText = DateTime.Parse(ToDt, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

        SqlParameter[] sqlprm = new SqlParameter[]
                       {
                           new SqlParameter("@UserID",UserID),
                           new SqlParameter("@StDt",FromDateText),
                           new SqlParameter("@EndDt",toDateText)

                       };
        return SqlHelper.ExecuteDataset(Connection, CommandType.StoredProcedure, "SEP_GetTimeSheet", sqlprm);
    }
}

