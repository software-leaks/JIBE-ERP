using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS.Data.Crew
{
    public class DAL_Crew_Disbursement
    {
        private static string connection = "";
        public DAL_Crew_Disbursement(string ConnectionString)
        {
            connection = ConnectionString;
        }
        static DAL_Crew_Disbursement()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        #region -- GET --
        public static DataTable Get_MODisbursementFeeTypes_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_DISB_SP_Get_DisbFeeTypes").Tables[0];
        }
        public static DataTable Get_MODisbursementFee_DL(int ManningOfficeID, int FeeType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ManningOfficeID",ManningOfficeID),
                                            new SqlParameter("@FeeType",FeeType)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_DISB_SP_Get_MODisbursementFee").Tables[0];
        }
        public static DataSet Get_MOAgencyFee_DL(int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_DISB_SP_Get_MOAgencyFee", sqlprm);
        }
        public static DataSet Get_MOProcessingFee_DL(int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_DISB_SP_Get_MOProcessingFee", sqlprm);
        }
        public static DataTable Get_AllCrewFeeStatus_DL(int FleetCode, int VesselID, int ManningOfficeID, int Rank_Category, int UserID, int Crew_Status, int Fee_Type, int Approved_YesNo, DateTime Sign_On_From, DateTime Sign_On_To, DateTime Approved_From, DateTime Approved_To, int? PAGE_SIZE, int? PAGE_INDEX, ref int SelectRecordCount, ref decimal GrandTotal, int Month, int Year, string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FleetCode",FleetCode),
                                            new SqlParameter("@VesselID",VesselID),
                                            new SqlParameter("@ManningOfficeID",ManningOfficeID),
                                            new SqlParameter("@Rank_Category",Rank_Category),
                                            new SqlParameter("@Crew_Status",Crew_Status),
                                            new SqlParameter("@Fee_Type",Fee_Type),
                                            new SqlParameter("@Approved_YesNo",Approved_YesNo),
                                            new SqlParameter("@Sign_On_From",Sign_On_From),
                                            new SqlParameter("@Sign_On_To",Sign_On_To),
                                            new SqlParameter("@Approved_From",Approved_From),
                                            new SqlParameter("@Approved_To",Approved_To),
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@Month",Month),
                                            new SqlParameter("@Year",Year),
                                            new SqlParameter("@SearchText",SearchText),
                                            new SqlParameter("@PAGE_SIZE",PAGE_SIZE),
                                            new SqlParameter("@PAGE_INDEX",PAGE_INDEX),
                                            new SqlParameter("@SelectRecordCount",SelectRecordCount)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_DISB_SP_Get_AllCrewFeeStatus", sqlprm);
            SelectRecordCount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            GrandTotal = Convert.ToDecimal(ds.Tables[1].Rows[0][0].ToString());

            return ds.Tables[0];
        }
        public static DataTable Get_MODisbursementFeeDetails_DL(int ID, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_DISB_SP_Get_FeeDetails", sqlprm).Tables[0];
        }
        public static DataTable Get_VesselList_CrewFee_DL(int UserID, int FleetCode)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@UserID",UserID),
                                            new SqlParameter("@FleetCode",FleetCode)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CRW_DISB_SP_Get_VesselList_CrewFee", sqlprm).Tables[0];
        }

        #endregion

        public static int AddNew_ProcessingFee_DL(int CrewID, int Vessel_ID, int VoyageID, int ManningOfficeID, int FeeType, decimal Amount, DateTime Due_Date, int Created_By, string Remarks)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@CrewID",CrewID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@VoyageID",VoyageID),
                                            new SqlParameter("@ManningOfficeID",ManningOfficeID),
                                            new SqlParameter("@FeeType",FeeType),
                                            new SqlParameter("@Amount",Amount),
                                            new SqlParameter("@Due_Date",Due_Date),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_DISB_SP_AddNew_ProcessingFee", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        #region -- UPDATE --
        public static int UPDATE_MODisbursementFee_DL(int ManningOfficeID, int FeeType, decimal Amount, int Id, DateTime EffectiveDate, int Created_By, int Rank_Category)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ManningOfficeID",ManningOfficeID),
                                            new SqlParameter("@FeeType",FeeType),
                                            new SqlParameter("@Amount",Amount),
                                            new SqlParameter("@ID",Id),
                                            new SqlParameter("@EffectiveDate",EffectiveDate),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Rank_Category",Rank_Category),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_DISB_SP_UPDATE_MODisbursementFEE_OLD", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public static int UPDATE_MODisbursementFee_DL(DataTable dt, int FeeType,int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@dtMODisbursementFEE",dt),
                                            new SqlParameter("@FeeType",FeeType),                                            
                                            new SqlParameter("@Created_By",Created_By),
                                          
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_DISB_SP_UPDATE_MODisbursementFEE", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public static int UPDATE_ApprovedStatus_DL(int ID, int CrewID, int Approved_YesNo, DateTime Approved_Date, int Approved_By, string Remarks)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CrewID",CrewID),                                            
                                            new SqlParameter("@Approved_YesNo",Approved_YesNo),
                                            new SqlParameter("@Approved_Date",Approved_Date),
                                            new SqlParameter("@Approved_By",Approved_By),
                                            new SqlParameter("@Remarks",Remarks),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };

            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CRW_DISB_SP_UPDATE_ApprovedStatus", sqlprm);

            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        #endregion
    }
}
