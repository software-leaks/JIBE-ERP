using System;
using System.Data;
using System.Configuration;

using System.Web.Security;

using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SysParameterDL
/// </summary>
namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_SysParameter
    {
        private string _messagename;	//Error or success message string
        private long _messagecode;	//Error code or success code value (0 for success)
        private string _constring;		//Connection String

        public DAL_Infra_SysParameter()
        {

            //this._constring = System.Configuration.ConfigurationSettings.AppSettings["IMACSConnectionString"];
            _messagecode = 0;
            _messagename = null;
            this._constring = "";
        }
        public string MessageName
        {
            get { return this._messagename; }
            set { this._messagename = value; }
        }
        public long MessageCode
        {
            get { return this._messagecode; }
        }
        public string ConnectionString
        {
            get { return this._constring; }
            set { this._constring = value; }
        }
        string con = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DataSet GetModules(int status)
        {

            DataSet ds = new DataSet();
            if (status != 2)
            {
                SqlParameter[] obj = new SqlParameter[] { new SqlParameter("@active", SqlDbType.Bit) };
                obj[0].Value = status;
                SqlHelper.FillDataset(con, CommandType.StoredProcedure, "SP_Sys_Par_GetModules_Active", ds, new string[] { "Modules" }, obj);
            }
            else
                SqlHelper.FillDataset(con, CommandType.StoredProcedure, "SP_Sys_Par_GetModules", ds, new string[] { "Modules" });
            return ds;
        }

        public int insertSysModule(string name, string TableName, int user, int active)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                new SqlParameter("@name",SqlDbType.VarChar),
                new SqlParameter("@tablename",SqlDbType.VarChar),
                new SqlParameter("@user",SqlDbType.BigInt),
                new SqlParameter("@active",SqlDbType.BigInt)
            };
            obj[0].Value = name;
            obj[1].Value = TableName;
            obj[2].Value = user;
            obj[3].Value = active;
            return SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "SP_Sys_Par_InsertModule", obj);
        }

        public DataSet GetModulesTable()
        {

            DataSet ds = new DataSet();
            SqlHelper.FillDataset(con, CommandType.StoredProcedure, "SP_Sys_Par_GetTables", ds, new string[] { "tables" });
            return ds;
        }


        public DataSet GetListValues(string tablename, string parentcode, string status)
        {

            string Query = "";


            switch (tablename.ToUpper())
            {

                case "PURC_LIB_SYSTEM_PARAMETERS":

                    if (parentcode == "")
                    {
                        Query = "select code,Description as name from " + tablename + " where Parent_Type=0 or Parent_Type is null ";
                        if (status != "2")
                            Query += " and Active_Status=" + status + " order by Short_Code";
                    }
                    else
                    {
                        Query = "select code, Description as name from " + tablename + " where Parent_Type =" + parentcode;
                        if (status != "2")
                            Query += " and Active_Status=" + status + " order by Short_Code";
                    }

                    break;

                case "LIB_VESSELLIB_SYSTEMS_PARAMETERS":
                case "FBM_LIB_SYSTEMS_PARAMETERS":
                case "ACC_LIB_SYSTEMS_PARAMETERS":
                case "TEC_LIB_SYSTEMS_PARAMETERS":
                case "OPS_LIB_SYSTEMS_PARAMETERS":

                    if (parentcode == "")
                    {
                        Query = "select code,Name as name from " + tablename + " where Parent_Code=0 or Parent_Code is null";
                        if (status != "2")
                            Query += " and Active_Status=" + status + " order by Name";
                    }
                    else
                    {
                        Query = "select code, Name as name from " + tablename + " where Parent_Code =" + parentcode;
                        if (status != "2")
                            Query += " and Active_Status=" + status + " order by Name";
                    }

                    break;

                case "QMS_SYSTEMPARAMETERS":
                case "SEP_TASK_SYSTEMPARAMETERS":

                    if (parentcode == "")
                    {
                        Query = "select  ID as code,Name as name from " + tablename + " where Prarent_Code=0 or Prarent_Code is null";
                        if (status != "2")
                            Query += " and Active_Status=" + status + " order by Name";
                    }
                    else
                    {
                        Query = "select ID as code, Name as name from " + tablename + " where Prarent_Code =" + parentcode;
                        if (status != "2")
                            Query += " and Active_Status=" + status + " order by Name";
                    }

                    break;

                default:

                    if (parentcode == "")
                    {
                        Query = "select code,Description as name from " + tablename + " where Parent_Type=0 or Parent_Type is null";
                        if (status != "2")
                            Query += " and Active_Status=" + status + " order by Short_Code";
                    }
                    else
                    {
                        Query = "select code, Description as name from " + tablename + " where Parent_Type =" + parentcode;
                        if (status != "2")
                            Query += " and Active_Status=" + status + " order by Short_Code";
                    }

                    break;
            }


            DataSet ds = SqlHelper.ExecuteDataset(new SqlConnection(con), CommandType.Text, Query);
            return ds;

        }

        public int insertSysparameter(string query)
        {
            return SqlHelper.ExecuteNonQuery(new SqlConnection(con), CommandType.Text, query);
        }

        public int deleteSysparameter(string query)
        {
            return SqlHelper.ExecuteNonQuery(new SqlConnection(con), CommandType.Text, query);
        }
        public DataSet GetDetailOfSystemParameter(int code, string tablename)
        {

            string Query = "";

            switch (tablename.ToUpper())
            {

                case "PURC_LIB_SYSTEM_PARAMETERS":
                    Query = "Select code,Parent_Type as Parent_Code, Short_Code as name,description,Active_Status from " + tablename + " where Code=" + code.ToString();
                    break;

                case "LIB_VESSELLIB_SYSTEMS_PARAMETERS":
                case "FBM_LIB_SYSTEMS_PARAMETERS":
                case "ACC_LIB_SYSTEMS_PARAMETERS":
                case "TEC_LIB_SYSTEMS_PARAMETERS":
                case "OPS_LIB_SYSTEMS_PARAMETERS":
                    Query = "Select code, Parent_Code as Parent_Code ,Name as name,description,Active_Status from " + tablename + " where Code=" + code.ToString();
                    break;

                case "QMS_SYSTEMPARAMETERS":
                case "SEP_TASK_SYSTEMPARAMETERS":
                    Query = "Select ID as code,Prarent_Code as Parent_Code , Name as name,description,Active_Status from " + tablename + " where ID=" + code.ToString();
                    break;
                default:
                    Query = "Select code,PARENT_CODE as Parent_Code, Short_Code as name,description,Active_Status from " + tablename + " where Code=" + code.ToString();
                    break;

            }

            DataSet ds = SqlHelper.ExecuteDataset(new SqlConnection(con), CommandType.Text, Query);
            return ds;

        }
        public int updateSysParam(int ChildId, string Name, int Parent, string Description, int Active, string tablename, int user)
        {

            StringBuilder sbquery = new StringBuilder();


            switch (tablename.ToUpper())
            {

                case "PURC_LIB_SYSTEM_PARAMETERS":

                    if (Parent == -1)
                        sbquery.Append("update " + tablename + " set Parent_Type=0,Short_Code='" + Name.ToString() + "',description='" + Description.ToString() + "',Active_Status=" + Active.ToString() + ",Modified_By=" + user.ToString() + " where Code=" + ChildId.ToString());
                    else
                    {
                        // a new record will be created for the po legal term 
                        if (Parent == 281)
                            sbquery.Append(" insert into " + tablename + "(code,Parent_type,Short_Code,Description,Created_By,Date_Of_Created,Active_Status)  ( select isnull(max(Code),0)+1 ," + Parent.ToString() + ",'" + Name.ToString() + "','" + Description.ToString() + "'," + user.ToString() + ",GetDate(), 1 " + " from " + tablename + ")");
                        else
                            sbquery.Append("update " + tablename + " set Parent_Type=" + Parent.ToString() + ",Short_Code='" + Name.ToString() + "',description='" + Description.ToString() + "',Active_Status=" + Active.ToString() + ",Modified_By=" + user.ToString() + " where Code=" + ChildId.ToString());
                    }

                    sbquery.Append(@"  DECLARE @Code INT ,@PkCondition VARCHAR(400) ,@TableName VARCHAR(100),@VSlcount INT ,@VSLID VARCHAR(25)  
                                        SET @Code=" + ChildId.ToString() + @"  
                                        SET @PkCondition = 'CODE=''' + cast(@Code as varchar) + '''' 
                                        SET @TableName='" + tablename + @"'
                                        EXEC SYNC_SP_DataSynch_MultiPK_DataLog @TableName, @PkCondition, 0
                                    ");

                    break;

                case "LIB_VESSELLIB_SYSTEMS_PARAMETERS":
                case "FBM_LIB_SYSTEMS_PARAMETERS":
                case "ACC_LIB_SYSTEMS_PARAMETERS":
                case "TEC_LIB_SYSTEMS_PARAMETERS":
                case "OPS_LIB_SYSTEMS_PARAMETERS":

                    if (Parent == -1)
                        sbquery.Append("update " + tablename + " set Parent_Code=0,Name='" + Name.ToString() + "',Description='" + Description.ToString() + "',Active_Status=" + Active.ToString() + ",Modified_By=" + user.ToString() + " where Code=" + ChildId.ToString());
                    else
                        sbquery.Append("update " + tablename + " set Parent_Code=" + Parent.ToString() + ",Name='" + Name.ToString() + "',Description='" + Description.ToString() + "',Active_Status=" + Active.ToString() + ",Modified_By=" + user.ToString() + " where Code=" + ChildId.ToString());


                    sbquery.Append(@"  DECLARE @Code INT ,@PkCondition VARCHAR(400) ,@TableName VARCHAR(100),@VSlcount INT ,@VSLID VARCHAR(25)  
                                        SET @Code=" + ChildId.ToString() + @"  
                                        SET @PkCondition = 'CODE=''' + cast(@Code as varchar) + '''' 
                                        SET @TableName='" + tablename + @"'
                                        EXEC SYNC_SP_DataSynch_MultiPK_DataLog @TableName, @PkCondition, 0
                                    ");

                    break;

                case "QMS_SYSTEMPARAMETERS":
                case "SEP_TASK_SYSTEMPARAMETERS":

                    if (Parent == -1)
                        sbquery.Append("update " + tablename + " set Prarent_Code=0,Name='" + Name.ToString() + "',description='" + Description.ToString() + "',Active_Status=" + Active.ToString() + " where ID=" + ChildId.ToString());
                    else
                        sbquery.Append("update " + tablename + " set Prarent_Code=" + Parent.ToString() + ",Name='" + Name.ToString() + "',description='" + Description.ToString() + "',Active_Status=" + Active.ToString() + " where ID=" + ChildId.ToString());


                    sbquery.Append(@"  DECLARE @Code INT ,@PkCondition VARCHAR(400) ,@TableName VARCHAR(100),@VSlcount INT ,@VSLID VARCHAR(25)  
                                        SET @Code=" + ChildId.ToString() + @"  
                                        SET @PkCondition = 'ID=''' + cast(@Code as varchar) + '''' 
                                        SET @TableName='" + tablename + @"'
                                        EXEC SYNC_SP_DataSynch_MultiPK_DataLog @TableName, @PkCondition, 0
                                    ");

                    break;
                default:

                    if (Parent == -1)
                        sbquery.Append("update " + tablename + " set Parent_Type=0,Short_Code='" + Name.ToString() + "',description='" + Description.ToString() + "',Active_Status=" + Active.ToString() + ",Modified_By=" + user.ToString() + " where Code=" + ChildId.ToString());
                    else
                        sbquery.Append("update " + tablename + " set Parent_Type=" + Parent.ToString() + ",Short_Code='" + Name.ToString() + "',description='" + Description.ToString() + "',Active_Status=" + Active.ToString() + ",Modified_By=" + user.ToString() + " where Code=" + ChildId.ToString());


                    sbquery.Append(@"  DECLARE @Code INT ,@PkCondition VARCHAR(400) ,@TableName VARCHAR(100),@VSlcount INT ,@VSLID VARCHAR(25)  
                                        SET @Code=" + ChildId.ToString() + @"  
                                        SET @PkCondition = 'CODE=''' + cast(@Code as varchar) + '''' 
                                        SET @TableName='" + tablename + @"'
                                        EXEC SYNC_SP_DataSynch_MultiPK_DataLog @TableName, @PkCondition, 0
                                    ");

                    break;



            }




            return SqlHelper.ExecuteNonQuery(new SqlConnection(con), CommandType.Text, sbquery.ToString());
        }
    }
}
