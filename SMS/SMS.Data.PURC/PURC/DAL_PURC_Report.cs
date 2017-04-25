using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.PURC
{
    public class DAL_PURC_Report
    {
        static string _Connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public static DataTable GET_Reqsn_Processing_Time_BY_Reqsn_DL(string Requisition_Code)
        {
            return SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_REQSN_PROCESSING_TIME_BY_REQSN", new SqlParameter("@REQUISITION_CODE", Requisition_Code)).Tables[0];
        }

        public static DataTable Get_Items_Quantity_List(DataTable dtFleet, DataTable dtVessel, string Search_Items, string Department_Code, string System_Code, string SubSystem_Code, int? Latest, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            SqlParameter[] prm = new SqlParameter[]{ 
                                                     new SqlParameter("@FLEET_ID",dtFleet),
                                                     new SqlParameter("@VESSEL_ID",dtVessel),
                                                     new SqlParameter("@SEARCH_ITEMS",Search_Items),
                                                     new SqlParameter("@DEPARTMENT_CODE",Department_Code),
                                                     new SqlParameter("@SYSTEM_CODE",System_Code),
                                                     new SqlParameter("@SUBSYSTEM_CODE",SubSystem_Code),
                                                     new SqlParameter("@Latest",Latest),
                                                     
                                                     new SqlParameter("@PAGE_INDEX",Page_Index),
                                                     new SqlParameter("@PAGE_SIZE",Page_Size),
                                                     new SqlParameter("@RECORD_COUNT",Record_count),
                                                   };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dtRes = SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_Items_Quantity_List", prm).Tables[0];
            Record_count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return dtRes;

        }

        public static int UPD_Item_Quantity(int Vessel_ID, string Item_Ref_Code, int ID, decimal Min_Qty, decimal Max_Qty, string Effective_Date, int User_ID)
        {

           

            SqlParameter[] prm = new SqlParameter[]{ new SqlParameter("@Vessel_ID",Vessel_ID),
                                                     new SqlParameter("@Item_Ref_Code",Item_Ref_Code),
                                                     new SqlParameter("@ID",ID),
                                                     new SqlParameter("@Min_Qty",Min_Qty),
                                                     new SqlParameter("@Max_Qty",Max_Qty),
                                                     new SqlParameter("@Effective_Date",Convert.ToDateTime(Effective_Date)),
                                                     new SqlParameter("@User_ID",User_ID),

                                                     new SqlParameter("@return",SqlDbType.Int)
                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_Connection, CommandType.StoredProcedure, "PURC_UPD_Item_Quantity", prm);
            return Convert.ToInt32(prm[prm.Length - 1].Value);


        }

        public static DataTable Get_Inventory_UpdatedBy(int ID, int Office_ID, int Vessel_ID)
        {
            SqlParameter[] prm = new SqlParameter[]
             { new SqlParameter("@Vessel_ID",Vessel_ID),
               new SqlParameter("@Office_ID",Office_ID),
               new SqlParameter("@ID",ID)
             };

            return SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_Inventory_UpdatedBy", prm).Tables[0];
        }

        public static DataTable GetToopTipsForQtnRecve(string DocCode)
        {
            System.Data.DataTable dt;
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                 
               new SqlParameter("@DocumentCode",DocCode)
               
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_SP_GET_TOOLTIPS_QTN_RECVD", sqlprm);
            return dt = ds.Tables[0];
        }

        public static DataTable GetToopTipsForQtnSent(string DocCode)
        {
            System.Data.DataTable dt;
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                  
               new SqlParameter("@DocumentCode",DocCode),  
                
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_SP_GET_TOOLTIPS_QTN_SENT", sqlprm);
            return dt = ds.Tables[0];


        }
        public static DataTable GetToopTipsForQtnINProgress(string DocCode)
        {
            System.Data.DataTable dt;
            SqlParameter[] sqlprm = new SqlParameter[]
            {
               
               new SqlParameter("@DocumentCode",DocCode),  
               
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_SP_GET_TOOLTIPS_QTN_IN_PROGRESS", sqlprm);
            return dt = ds.Tables[0];


        }
        public static DataTable Get_ToopTipsForQtnDeclined(string DocCode)
        {
            System.Data.DataTable dt;
            SqlParameter[] sqlprm = new SqlParameter[]
            {
               
               new SqlParameter("@DocumentCode",DocCode),  
               
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_SP_GET_TOOPTIPSFORQTNDECLINED", sqlprm);
            return dt = ds.Tables[0];


        }
        
    }
}
