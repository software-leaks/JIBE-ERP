using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS.Data.PURC
{
    public class DAL_PURC_Provision
    {
        static string _Connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        public DAL_PURC_Provision()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static DataSet Get_YearMonthList()
        {
            return SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_Provision_YearMonth");
        }

        public static DataTable Get_Provison_Victualing_Rate(DataTable dtFleet, DataTable dtVessel, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            SqlParameter[] prm = new SqlParameter[]{ 
                                                     new SqlParameter("@FLEET_ID",dtFleet),
                                                     new SqlParameter("@VESSEL_ID",dtVessel),
                                                   
                                                     
                                                     new SqlParameter("@PAGE_INDEX",Page_Index),
                                                     new SqlParameter("@PAGE_SIZE",Page_Size),
                                                     new SqlParameter("@RECORD_COUNT",Record_count),
                                                   };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_Items_Quantity_List", prm).Tables[0];
            Record_count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return dt;

        }
        public static DataTable Get_Provison_Victualing_Rate(string searchtext, DataTable dtFleet, DataTable dtVessel, string sortby, int? sortdirection, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            SqlParameter[] prm = new SqlParameter[]{ 
                                                     new SqlParameter("@SearchText",searchtext),
                                                     new SqlParameter("@FLEET_ID",dtFleet),
                                                     new SqlParameter("@VESSEL_ID",dtVessel), 
                                                     new SqlParameter("@SORTBY",sortby),
                                                     new SqlParameter("@SORTDIRECTION",sortdirection),                                                                                                        
                                                     new SqlParameter("@PAGENUMBER",Page_Index),
                                                     new SqlParameter("@PAGESIZE",Page_Size),
                                                     new SqlParameter("@ISFETCHCOUNT",Record_count),
                                                   };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_VICTUALINGRATE_SEARCH", prm).Tables[0];
            Record_count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return dt;

        }

        public static DataTable Get_Victualing_Rate_Edit(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]{ 
                                                     new SqlParameter("@ID",ID)};
            return SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_VICTUALINGRATE_EDIT", prm).Tables[0];

        }

        public static int InsUpt_Victualing_Rate(int? ID, int vesselid, Decimal victualingRate, DateTime? FromDate, int userId)
        {
            SqlParameter[] prm = new SqlParameter[]{ 
                                                     new SqlParameter("@ID",ID),
                                                     new SqlParameter("@Vessel_ID",vesselid),                                                     
                                                     new SqlParameter("@Victualling_Rate",victualingRate),
                                                     new SqlParameter("@Victualing_From_Date",FromDate),     
                                                     new SqlParameter("@Created_By",userId),
                                                   };
            return SqlHelper.ExecuteNonQuery(_Connection, CommandType.StoredProcedure, "PURC_INS_UPD_VICTUALLING_RATE", prm);


        }
        public static DataTable Get_Provison_MostExpensive_ItemList(DataTable dtFleet, DataTable dtVessel, string FromDate, string ToDate, int reportType, string SearchText, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            DateTime? fromdt = null;
            if (FromDate != null && FromDate != "")
                fromdt = DateTime.Parse(FromDate);
            DateTime? todt = null;
            if (ToDate != null && ToDate != "")
                todt = DateTime.Parse(ToDate);
            SqlParameter[] prm = new SqlParameter[]
            { 
                        new SqlParameter("@FLEET_ID",dtFleet),
                        new SqlParameter("@VESSEL_ID",dtVessel),                                               
                        new SqlParameter("@FromDate",fromdt),
                        new SqlParameter("@ToDate",todt),
                        new SqlParameter("@ReportType",reportType),
                        new SqlParameter("@SearchText",SearchText),
                        new SqlParameter("@PAGE_INDEX",Page_Index),
                        new SqlParameter("@PAGE_SIZE",Page_Size),
                        new SqlParameter("@RECORD_COUNT",Record_count)
            };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_PROVISION_MOST_EXPENSIVE_ITEMS_NEW", prm).Tables[0];
            Record_count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return dt;

        }

        public static DataTable Get_Provison_MostOrdered_ItemList(DataTable dtFleet, DataTable dtVessel, string FromDate, string ToDate, int reportType, string SearchText, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            DateTime? fromdt = null;
            if (FromDate != null && FromDate != "")
                fromdt = DateTime.Parse(FromDate);
            DateTime? todt = null;
            if (ToDate != null && ToDate != "")
                todt = DateTime.Parse(ToDate);
            SqlParameter[] prm = new SqlParameter[]{ 
                                                     new SqlParameter("@FLEET_ID",dtFleet),
                                                     new SqlParameter("@VESSEL_ID",dtVessel),                                               
                                                     new SqlParameter("@FromDate",fromdt),
                                                     new SqlParameter("@ToDate",todt),
                                                     new SqlParameter("@ReportType",reportType),
                                                     new SqlParameter("@SearchText",SearchText),
                                                     new SqlParameter("@PAGE_INDEX",Page_Index),
                                                     new SqlParameter("@PAGE_SIZE",Page_Size),
                                                     new SqlParameter("@RECORD_COUNT",Record_count),
                                                   };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_Provision_Most_OrderItem_List_New", prm).Tables[0];
            Record_count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return dt;

        }
        public static DataTable Get_Provison_Item_frequency_List(string searchtext, string sortby, int? sortdirection, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            SqlParameter[] prm = new SqlParameter[]{ 
                                                     new SqlParameter("@SearchText",searchtext),
                                                     new SqlParameter("@SORTBY",sortby),
                                                     new SqlParameter("@SORTDIRECTION",sortdirection),
                                                                                                        
                                                     new SqlParameter("@PAGENUMBER",Page_Index),
                                                     new SqlParameter("@PAGESIZE",Page_Size),
                                                     new SqlParameter("@ISFETCHCOUNT",Record_count),
                                                   };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_PROVISION_ITEM_FREQUENCY_SEARCH", prm).Tables[0];
            Record_count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return dt;

        }
        public static DataTable Get_Provison_Item_frequency_Edit(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]{ 
                                                     new SqlParameter("@ID",ID)};
            return SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_PROVISION_FREQUENCY_EDIT", prm).Tables[0];

        }

        public static int InsUpt_Provison_Item_frequency(int? ID, int? VESSEL_ID, string subsystem_code, Decimal reqsn_indays, int userId)
        {
            SqlParameter[] prm = new SqlParameter[]{ 
                                                     new SqlParameter("@ID",ID),  
                                                     new SqlParameter("@VESSEL_ID",VESSEL_ID),                                                  
                                                     new SqlParameter("@PROVISION_TYPE",subsystem_code),
                                                     new SqlParameter("@SUPPLY_PERIOD",reqsn_indays),
                                                     new SqlParameter("@Created_By",userId),
                                                   };
            return SqlHelper.ExecuteNonQuery(_Connection, CommandType.StoredProcedure, "PURC_INS_UPD_PROVISION_FREQUENCY", prm);


        }

        public static DataTable Get_Provison_ExtraMeal(DataTable dtFleet, DataTable dtVessel, string FromDate, string ToDate, string sortby, int? sortdirection, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            DateTime? fromdt = null;
            if (FromDate != null && FromDate != "")
                fromdt = DateTime.Parse(FromDate);
            DateTime? todt = null;
            if (ToDate != null && ToDate != "")
                todt = DateTime.Parse(ToDate);
            SqlParameter[] prm = new SqlParameter[]{ 
                                                     new SqlParameter("@FLEET_ID",dtFleet),
                                                     new SqlParameter("@VESSEL_ID",dtVessel), 
                                                     new SqlParameter("@FromDate",fromdt),
                                                     new SqlParameter("@ToDate",todt),
                                                     new SqlParameter("@SORTBY",sortby),
                                                     new SqlParameter("@SORTDIRECTION",sortdirection),                                                                                                        
                                                     new SqlParameter("@PAGENUMBER",Page_Index),
                                                     new SqlParameter("@PAGESIZE",Page_Size),
                                                     new SqlParameter("@ISFETCHCOUNT",Record_count),
                                                   };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_EXTRAMEALS_SEARCH", prm).Tables[0];
            Record_count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return dt;

        }

        public static DataTable Get_Provison_OrderVictualing_rate(DataTable dtFleet, DataTable dtVessel, string searchtext, int? Page_Index, int? Page_Size, ref int Record_count)
        {
            SqlParameter[] prm = new SqlParameter[]{ 
                                                     new SqlParameter("@FLEET_ID",dtFleet),
                                                     new SqlParameter("@VESSEL_ID",dtVessel),                                               
                                                      new SqlParameter("@SearchText",searchtext),
                                                     new SqlParameter("@PAGE_INDEX",Page_Index),
                                                     new SqlParameter("@PAGE_SIZE",Page_Size),
                                                     new SqlParameter("@RECORD_COUNT",Record_count),
                                                   };

            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt = SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_PROVISION_ORDER_VICTULING", prm).Tables[0];
            Record_count = Convert.ToInt32(prm[prm.Length - 1].Value);
            return dt;

        }

        public static DataTable Get_Reqsn_Items_Percent(string Reqsn_Code, string Order_Code, string System_Code)
        {
            DataTable dt = new DataTable();
            SqlParameter[] obj = new SqlParameter[] 
            { 
                   new  SqlParameter("@REQSN_CODE", Reqsn_Code),                   
                   new  SqlParameter("@ORDER_CODE",Order_Code),
                   new  SqlParameter("@SYSTEM_CODE",System_Code)
            };
            return SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_Get_Reqsn_Items_Percent", obj).Tables[0];

        }

        public static DataSet Get_Calculate_Victualling_Rate(int VesselCode, string requisitioncode, string ordercode, int userid)
        {
            SqlParameter[] obj = new SqlParameter[] 
            { 
                   new  SqlParameter("@Vessel_ID", VesselCode),
                   new  SqlParameter("@Requisition_Code", requisitioncode),
                   new  SqlParameter("@Order_code",ordercode),
                   new  SqlParameter("@USERID",userid),
            };
            DataSet ds = SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_VICTUALLING_RATE_MEAT_DETAILS", obj);
            ds.Tables[0].TableName = "VICRATE";
            ds.Tables[1].TableName = "MEAT";
            return ds;

        }
        public static DataSet PURC_GET_SUBSYSTEMCODE_PROVISIONTYPE(int userid)
        {
            SqlParameter[] obj = new SqlParameter[] 
            { 
                   new  SqlParameter("@USERID",userid),
            };
            DataSet ds = SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_SUBSYSTEMCODE_PROVISIONTYPE", obj);
            return ds;

        }
        public static int PURC_UPDATE_SUBSYSTEMCODE_PROVISIONTYPE(int ProviID, string ProvisionType, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ProviID",ProviID),
                                            new SqlParameter("@ProvisionType",ProvisionType),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(_Connection, CommandType.StoredProcedure, "PURC_UPDATE_SUBSYSTEMCODE_PROVISIONTYPE", sqlprm).ToString());
        }
        public static int PURC_DELETE_SUBSYSTEMCODE_PROVISIONTYPE(int ProviID, string ProvisionType, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ProviID",ProviID),
                                            new SqlParameter("@ProvisionType",ProvisionType),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(_Connection, CommandType.StoredProcedure, "PURC_DELETE_SUBSYSTEMCODE_PROVISIONTYPE", sqlprm).ToString());
        }
        public static int PURC_INS_SUBSYSTEMCODE_PROVISIONTYPE(int SUBSYSTEM_ID, string ProvisionType, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@SUBSYSTEM_ID",SUBSYSTEM_ID),
                new SqlParameter("@PROVISION_TYPE",ProvisionType),
                new SqlParameter("@UserID",UserID),
            };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(_Connection, CommandType.StoredProcedure, "PURC_INS_SUBSYSTEMCODE_PROVISIONTYPE", sqlprm));
        }


        public static DataSet PURC_GET_PROVISIONTYPE()
        {
            return SqlHelper.ExecuteDataset(_Connection, CommandType.StoredProcedure, "PURC_GET_PROVISIONTYPE");
        }
    }
}
