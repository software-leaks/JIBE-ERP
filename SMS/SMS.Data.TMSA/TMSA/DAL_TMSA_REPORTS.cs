using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.TMSA
{
    public class DAL_TMSA_REPORTS
    {
        private string connection = "";

        public DAL_TMSA_REPORTS()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }


        /// <summary>
        /// Description: Method to search Data for Overall Report
        /// Created By: Gargi
        /// Created On: 29/09/2016
        /// </summary>
        public DataSet Search_OverallReport(string ElementIDs, string StageIDs, string LevelIDs, string Role, int iVersionNo)
        {
            DataSet DS = new DataSet();

            String phraseE = ElementIDs;
            ElementIDs = phraseE.Replace("All,", "");

            String  phraseS = StageIDs;
            StageIDs = phraseS.Replace("All,", "");

            String phraseL = LevelIDs;
            LevelIDs = phraseL.Replace("All,", "");

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ElementIDs",ElementIDs) ,
                                            new SqlParameter("@StageIDs",StageIDs),
                                            new SqlParameter("@LevelIDs",LevelIDs),
                                            new SqlParameter("@Version",iVersionNo) 
                                        };
            if (Role == "Admin")
            {
                DS= SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TSMA_Report_Search_OverallReport", sqlprm);
            }
            if (Role == "View")
            {
                DS= SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TSMA_Report_OverallReport_ViewMode", sqlprm);
            }
            
            return DS;


        }
        /// <summary>
        /// Description: Method to Get data in Excel Sheet
        /// Created By: Gargi
        /// Created On: 29/09/2016
        /// </summary>
        public DataSet Search_ExportToExcelOverallReport(string ElementIDs, string StageIDs, string LevelIDs, int iVersionNo)
        {
            String phraseE = ElementIDs;
            ElementIDs = phraseE.Replace("All,", "");

            String phraseS = StageIDs;
            StageIDs = phraseS.Replace("All,", "");

            String phraseL = LevelIDs;
            LevelIDs = phraseL.Replace("All,", "");

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ElementIDs",ElementIDs) ,
                                            new SqlParameter("@StageIDs",StageIDs),
                                            new SqlParameter("@LevelIDs",LevelIDs),
                                            new SqlParameter("@Version",iVersionNo)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TSMA_ExportToExcel_OverallReport", sqlprm);

        }
        /// <summary>
        /// Description: Method to get element list 
        /// Created By: Gargi
        /// Created On: 29/09/2016
        /// </summary>
        public DataTable Get_ElementList_DL(int iVersionNo)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 

                                            new SqlParameter("@Version",iVersionNo)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Report_Get_ElementList", sqlprm).Tables[0];
        }
        /// <summary>
        /// Description: Method to get Stage list 
        /// Created By: Gargi
        /// Created On: 29/09/2016
        /// </summary>
        public DataTable Get_StageList_DL(int iVersionNo)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 

                                            new SqlParameter("@Version",iVersionNo)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Report_Get_StageList", sqlprm).Tables[0];
        }
        /// <summary>
        /// Description: Method to get Level list
        /// Created By: Gargi
        /// Created On: 29/09/2016
        /// </summary>
        public DataTable Get_LevelList_DL(int iVersionNo)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 

                                            new SqlParameter("@Version",iVersionNo)
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Report_Get_LevelList", sqlprm).Tables[0];
        }
        /// <summary>
        /// Description: Method to get Version list
        /// Created By: Gargi
        /// Created On: 29/09/2016
        /// </summary>
        public DataTable Get_VersionList_DL()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Report_Get_VersionList").Tables[0];
        }
        /// <summary>
        /// Description: Method to search Data for Overall Report
        /// Created By: Gargi
        /// Created On: 29/09/2016
        /// </summary>
        public DataSet GetCount(int iParentID,  string LinkType)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ParentID",iParentID),
                                            new SqlParameter("@LinkType",LinkType)

                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TSMA_Report_Get_Count", sqlprm);
        }

        /// <summary>
        /// Description: Method to search Data for Overall Report
        /// Created By: Gargi
        /// Created On: 29/09/2016
        /// </summary>
        public DataSet LinkExists(int ParentID, string LinkType, string LinkID, string DPath, string Notes)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ParentID",ParentID),
                                            new SqlParameter("@LinkType",LinkType),
                                            new SqlParameter("@LinkID",LinkID),
                                            new SqlParameter("@DPath",DPath),
                                            new SqlParameter("@Notes",Notes)

                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TSMA_Report_LinkExists", sqlprm);
        }
        /// <summary>
        /// Description: Method to Save Data of report
        /// Created By: Gargi
        /// Created On: 29/09/2016
        /// </summary>
        public int SaveData(int ParentID, string LinkType, string LinkID, string DPath, string Notes)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ParentID",ParentID),
                                            new SqlParameter("@LinkType",LinkType),
                                            new SqlParameter("@LinkID",LinkID),
                                            new SqlParameter("@DPath",DPath),
                                            new SqlParameter("@Notes",Notes),
                                            new SqlParameter("return",SqlDbType.Int)

                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TSMA_Report_Save_Data", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            
        }
        /// <summary>
        /// Method to Update Data of report
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ParentID"></param>
        /// <param name="AuditedProcess"></param>
        /// <param name="Compliance"></param>
        /// <returns></returns>
        public int UpdateData(int ID, int ParentID, string AuditedProcess, int Compliance)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@ParentID",ParentID),
                                            new SqlParameter("@AuditedProcess",AuditedProcess),
                                            new SqlParameter("@Compliance",Compliance),
                                            new SqlParameter("return",SqlDbType.Int)

                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TSMA_Report_Update_Data", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
/// <summary>
/// Method to Delete Data of report
/// </summary>
/// <param name="LinkID"></param>
/// <returns></returns>
        public int DeleteData(int LinkID)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@LinkID",LinkID),
                                            new SqlParameter("return",SqlDbType.Int)

                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TSMA_Report_Delete_Data", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        /// <summary>
        /// Method Create New Version
        /// </summary>
        /// <returns></returns>
        public int CopyNewVersion()
        {

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TSMA_Report_Create_NewVersion");

        }
        /// <summary>
        /// Method used to get Pie chart data
        /// </summary>
        /// <param name="LevelNO"></param>
        /// <param name="iVersionNo"></param>
        /// <returns></returns>
        public DataTable Get_PieChartData_DL(int LevelNO, int iVersionNo)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@LevelNO",LevelNO),
                                             new SqlParameter("@Version",iVersionNo)

                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Report_Get_PieChartData", sqlprm).Tables[0];
            
        }

        /// <summary>
        /// Description: Method to search Data for Overall Report
        /// Created By: Gargi
        /// Created On: 29/09/2016
        /// </summary>
        public DataSet FillTree(int userid)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@userid",userid)
                                        
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_MNU_Get_MenuLibAccess", sqlprm);
        }

    }
}
