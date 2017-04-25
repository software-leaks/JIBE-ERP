using System;
using System.Collections;

using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for wsSEP
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class wsSEP : System.Web.Services.WebService {

    public wsSEP () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string getApplicationsFortheMonth(string m, string y)
    {
        string sql = "";

//        sql = @"SELECT     SEP_LeaveInfoByUser.User_id, SEP_User_Profile.UserName, SEP_LeaveInfoByUser.Application_id, SEP_LeaveInfoByUser.From_Date, SEP_LeaveInfoByUser.To_Date
//                FROM         SEP_LeaveInfoByUser INNER JOIN
//                                      SEP_User_Profile ON SEP_LeaveInfoByUser.User_id = SEP_User_Profile.UserID
//                WHERE month(from_date)=2 or month(to_date)=2
//                ORDER BY SEP_LeaveInfoByUser.Application_id";
        
        string tables = "SEP_LeaveInfoByUser INNER JOIN SEP_User_Profile ON SEP_LeaveInfoByUser.User_id = SEP_User_Profile.UserID INNER JOIN SEP_Task_SystemParameters ON SEP_LeaveInfoByUser.Status = SEP_Task_SystemParameters.ID";
        string fields = "user_id, username, application_id, from_date, to_date, SEP_Task_SystemParameters.name as status,color";

        string filter = "(month(from_date)=" + m + " or month(to_date)=" + m + ")and(year(from_date)=" + y + " or year(to_date)=" + y + ")";
        string orderby = " From_Date";

        string retStr = getRecords(tables, fields, filter, orderby);
        return retStr;
    }

    [WebMethod]
    public string getBugFixFortheMonth(string m, string y, string orgid,string planned,string actual, string userid)
    {
        string tables = "";
        if (userid == "0")
        {
             tables = "SEP_Lib_bug LEFT OUTER JOIN Lib_Vessels ON SEP_Lib_bug.vessel_code = Lib_Vessels.Vessel_id ";
        }
        else
        {
            tables = "SEP_Lib_bug LEFT OUTER JOIN Lib_Vessels ON SEP_Lib_bug.vessel_code = Lib_Vessels.Vessel_id inner join SEP_Bug_Assing_Info ASN on ASN.AssignedTo="+userid+" ";
        }
        string fields = "SEP_Lib_bug.bug_id, convert(varchar,cmpl_date,101) as cmpl_date, status, convert(varchar,open_date,101) as open_date, vessel_short_name as vessel, replace(ISNULL(REPLACE( REPLACE(replace(Subject,'''',' '),char(10),''),CHAR(13),''),'') ,'''','') as Subject , replace(ISNULL(REPLACE( REPLACE(replace(Description,'''',' '),char(10),''),CHAR(13),''),'') ,'''','') as Description";

        string filter = "";
        if (orgid != "" && orgid != "63")
            filter = "org_id=" + orgid + " and ";

        filter += "((month(cmpl_date)=" + m + " and year(cmpl_date)=" + y + ") or (month(open_date)=" + m + " and year(open_date)=" + y + "))";

        if (planned.ToUpper() == "TRUE" && actual.ToUpper() == "FALSE")
        {
            filter += "and  cmpl_date is null ";
        }
        if (actual.ToUpper() == "TRUE" && planned.ToUpper() == "FALSE")
        {
            filter += "and  cmpl_date is not null ";
        }

        if (planned.ToUpper() == "TRUE" && actual.ToUpper() == "TRUE")
        {

            filter += "and  (cmpl_date is not null or cmpl_date is null) ";
        }

        
        string orderby = " bug_id,cmpl_date,open_date";

        string retStr = getRecords(tables, fields, filter, orderby);
        return retStr;
    }

    [WebMethod]
    public string getBuildReleaseForTheMonth(string m, string y)
    {
        string sql = "";

        string tables = "BuildRelease INNER JOIN BuildVessels ON BuildRelease.ID = BuildVessels.BuildID";
        string fields = "buildrelease.id, buildno, builddate, projectid, buildpath, createdby, count(buildvessels.id) as vessels, sum(status) as sent";

        string filter = "";
        filter += "(month(builddate)=" + m + " and year(builddate)=" + y + ") GROUP BY BuildRelease.ID, BuildNo, BuildDate, ProjectID, BuildPath, CreatedBy, SentStatus";

        string orderby = " builddate";

        string retStr = getRecords(tables, fields, filter, orderby);
        return retStr;
    }

    [WebMethod]
    public string MarkAsSent(string buildid,string vessel_code)
    {
        string sql = "";

        string tables = "BuildVessels";
        string fields = "status=1,sentdate=getdate()";
        string filter = "BuildID=" + buildid + " AND VesselCode=" + vessel_code;

        string retStr = UpdateRecord(tables, fields, filter);
        if (retStr == "1")
            return buildid;
        else
            return retStr;
    }

    [WebMethod]
    public string getToDoList(string listid,string createdby,string userid)
    {
        string filter = ""; 
        string sql1 = @"SELECT  SEP_ToDoList.ID,0 as ItemId, 
	            SEP_ToDoList.ParentListID,
	            ToDo, 
	            REPLACE(REPLACE(REPLACE(ToDoDesc, CHAR(10), 'newline'), CHAR(13), ''), CHAR(9), '') AS tododesc, 
	            SEP_ToDoList.PIC, 
	            SEP_ToDoList.EntryDate, 
	            SEP_ToDoList.CompletionDate, 
	            SEP_ToDoList.Status, 
	            SEP_ToDoList.Priority, 
	            SEP_ToDoList.Privacy, 
	            SEP_ToDoList.SubscribeID, 
	            SEP_ToDoList.ProjectID, 
	            SEP_ToDoList.TaskID, 
	            SEP_ToDoList.NotifyFlag, 
	            SEP_ToDoList.createdby, 
	            SEP_User_Profile.Firsr_Name AS l_createdby
            FROM         SEP_ToDoList LEFT OUTER JOIN
                                  SEP_User_Profile ON SEP_ToDoList.CreatedBy = SEP_User_Profile.UserID";

        if (Convert.ToInt32(listid) > 0)
            filter = " SEP_ToDoList.id=" + listid;
        else
        {
            if (Convert.ToInt32(createdby) > 0)
            {
                if (filter.Length > 0) filter += " AND ";
                filter += " SEP_ToDoList.createdby=" + createdby;
            }
            if (filter.Length > 0) filter += " OR ";
            filter += " (SEP_ToDoList.privacy=3)";

            if (filter.Length > 0) filter += " OR ";
            filter += " (SEP_ToDoList.privacy=2)";

            if (filter.Length > 0) filter += " OR ";
            filter += " (SEP_ToDoList.createdby=" + userid + " AND SEP_ToDoList.privacy=1)";
        }

            if (filter.Length > 0)
                filter = " WHERE " + filter;
            sql1 += filter;

        string sql2 = @"SELECT  0, SEP_ToDoListItems.ID, 
	            SEP_ToDoListItems.ParentListID,
	            SEP_ToDoListItems.ToDoItem as todo, 
	            '' AS tododesc, 
	            SEP_ToDoListItems.pic, 
	            SEP_ToDoListItems.EntryDate, 
	            SEP_ToDoListItems.CompletionDate, 
	            SEP_ToDoListItems.Status, 
	            SEP_ToDoListItems.Priority, 
	            SEP_ToDoListItems.Privacy, 
	            SEP_ToDoListItems.SubscribeID, 
	            0 as ProjectID, 
	            0 as TaskID, 
	            0 as NotifyFlag, 
	            SEP_ToDoListItems.createdby	, 
	            SEP_User_Profile.Firsr_Name AS l_createdby	 
            FROM         SEP_ToDoListItems LEFT OUTER JOIN
                                  SEP_User_Profile ON SEP_ToDoListItems.CreatedBy = SEP_User_Profile.UserID";
        filter = "";
        if (Convert.ToInt32(listid) > 0)
            filter = " WHERE SEP_ToDoListItems.ParentListID=" + listid;
        else
        {
            //if (Convert.ToInt32(createdby) > 0)
            //    filter = " WHERE ToDoListItems.createdby=" + createdby;
        }
        if (filter.Length > 0)
            sql2 = sql2 + filter;

        sql2 += " ORDER BY ID DESC";

        string retStr = ExecuteSQL(sql1 + " UNION " + sql2);
        return retStr;
    }
    
    [WebMethod]
    public string SaveToDoItem(string listid, string todo)
    {
        string sql = "";

        string tables = "SEP_ToDoListItems";
        string fields = "ParentListID, ToDoItem";
        string values = listid + ",'" + todo + "'";

        string retStr = InsertRecord(tables, fields, values);
        if (retStr == "1")
            return listid;
        else
            return retStr;
    }

    [WebMethod]
    public string ToDo_UpdateStatus(string todoId, string status)
    {
        string sql = "";

        string tables = "SEP_ToDoListItems";
        string fields = "status=" + status + ",completiondate=getdate()";
        string filter = "ID=" + todoId;

        string retStr = UpdateRecord(tables, fields, filter);
        if (retStr == "1")
            return todoId;
        else
            return retStr;
    }

    [WebMethod]
    public string UpdateToDoItem(string todoId, string text)
    {
        string sql = "";

        string tables = "SEP_ToDoListItems";
        string fields = "todoitem='" + text + "'";
        string filter = "ID=" + todoId;

        string retStr = UpdateRecord(tables, fields, filter);
        if (retStr == "1")
            return todoId;
        else
            return retStr;
    }
    
    private string getRecords(string table, string fields, string filter, string orderby)
    {
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["smsconn"].ToString();
        SqlConnection myConnection = new SqlConnection(ConnectionString);
        SqlDataReader dr;
        string res = "";
        string sColumnValues = "";
        string[] arFields = fields.Split(',');

        string sql = "SELECT " + fields + " FROM " + table;
        if (filter.Length > 0) sql += " WHERE " + filter;
        if (orderby.Length > 0) sql += " ORDER BY " + orderby;

        SqlCommand myCommand = new SqlCommand(sql, myConnection);
        try
        {
            myConnection.Open();
            dr = myCommand.ExecuteReader();
            while (dr.Read())
            {
                if (res.Length > 0) res += ",";
                res += "{";
                sColumnValues = "";
         
                for (int i = 0; i < dr.VisibleFieldCount; i++)
                {
                    if (sColumnValues.Length > 0) sColumnValues += ",";
                    sColumnValues += dr.GetName(i).ToString() + ":'" + dr[i].ToString() + "'";
                }
                res += sColumnValues;
                res += "}";
            }
        }
        catch (Exception ex)
        {
            return "ERROR:" + ex.Message;
        }
        finally
        {
            myConnection.Close();
            myConnection.Dispose();
        }
        if (!string.IsNullOrEmpty(res)) {
            res = "[" + res + "]";
        }
        return res;

    }
       
    private string getRecordCount(string table, string filter)
    {
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["smsconn"].ToString();
        SqlConnection myConnection = new SqlConnection(ConnectionString);

        string sql = "SELECT count(*) FROM " + table;
        if (filter.Length > 0) sql += " WHERE " + filter;
        
        SqlCommand myCommand = new SqlCommand( sql, myConnection);
        int res = 0;

        try
        {
            myConnection.Open();
            res = Convert.ToInt32( myCommand.ExecuteScalar());
        }
        catch (Exception ex)
        {
            return "ERROR:" + ex.Message;
        }
        finally
        {
            myConnection.Close();
            myConnection.Dispose();
        }
        return res.ToString();
    }
     
    private string UpdateRecord(string table, string setFields, string filter)
    {
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["smsconn"].ToString();
        SqlConnection myConnection = new SqlConnection(ConnectionString);
        int res = 0;
        SqlCommand myCommand = new SqlCommand("UPDATE " + table + " SET " + setFields + " WHERE " + filter, myConnection);
        try
        {
            myConnection.Open();
            res = myCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            return "ERROR:" + ex.Message;
        }
        finally
        {
            myConnection.Close();
            myConnection.Dispose();
        }

        return res.ToString();
    }
    
    private string InsertRecord(string table, string fields, string values)
    {
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["smsconn"].ToString();
        SqlConnection myConnection = new SqlConnection(ConnectionString);
        int res = 0;
        SqlCommand myCommand = new SqlCommand("INSERT INTO " + table + "(" + fields + ") VALUES(" + values + ")", myConnection);
        try
        {
            myConnection.Open();

            res = myCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            return "ERROR:" + ex.Message;
        }
        finally
        {
            myConnection.Close();
            myConnection.Dispose();
        }

        return  res.ToString();
    }

    private string ExecuteSQL(string sql)
    {
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["smsconn"].ToString();
        SqlConnection myConnection = new SqlConnection(ConnectionString);
        SqlDataReader dr;
        string res = "";
        string sColumnValues = "";


        SqlCommand myCommand = new SqlCommand(sql, myConnection);
        try
        {
            myConnection.Open();
            dr = myCommand.ExecuteReader();
            while (dr.Read())
            {
                if (res.Length > 0) res += ",";
                res += "{";
                sColumnValues = "";

                for (int i = 0; i < dr.VisibleFieldCount; i++)
                {
                    if (sColumnValues.Length > 0) sColumnValues += ",";
                    sColumnValues += dr.GetName(i).ToString().ToLower() + ":'" + dr[i].ToString() + "'";
                }
                res += sColumnValues;
                res += "}";
            }
        }
        catch (Exception ex)
        {
            return "ERROR:" + ex.Message;
        }
        finally
        {
            myConnection.Close();
            myConnection.Dispose();
        }
        if (!string.IsNullOrEmpty(res))
        {
            res = "[" + res + "]";
        }
        return res;

    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    
}

