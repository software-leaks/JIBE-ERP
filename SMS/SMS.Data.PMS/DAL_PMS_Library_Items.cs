using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

namespace SMS.Data.PMS
{
    public class DAL_PMS_Library_Items
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DataSet LibraryItemsGetToCopy(string systemcode, int? subsystemid, int? vesselid, int? IsActive, string reqsnType, string functions)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID",subsystemid),
                   new System.Data.SqlClient.SqlParameter("@VESSELID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive), 
                   new System.Data.SqlClient.SqlParameter("@ReqsnType",reqsnType), 
                   new System.Data.SqlClient.SqlParameter("@FunctionsId",functions)
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_Items_TO_COPY", obj);
        }



        public DataSet LibraryItemsGetToMove(string systemcode, int? subsystemid, int? vesselid, int? IsActive, string reqsnType, string functions, string searchtxt)
        {


            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID",subsystemid),
                   new System.Data.SqlClient.SqlParameter("@VESSELID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive),  
                   new System.Data.SqlClient.SqlParameter("@ReqsnType",reqsnType), 
                   new System.Data.SqlClient.SqlParameter("@FunctionsId",functions),
                   new System.Data.SqlClient.SqlParameter("@serchText",searchtxt)
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_PR_GET_Items_TO_MOVE", obj);

        }

        public int Items_MOVE(string systemcode, int? subsystemid, int? vesselid, string selectedItems, string user)
        {


            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID",subsystemid),
                   new System.Data.SqlClient.SqlParameter("@VESSELID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@SelectedItems",selectedItems),
                   new System.Data.SqlClient.SqlParameter("@user",user)
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Items_MOVE", obj);

        }

        public int Items_COPY(string systemcode, int? subsystemid, int? vesselid, int? IsActive, string user)
        {


            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID",subsystemid),
                   new System.Data.SqlClient.SqlParameter("@VESSELID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive),  
                   new System.Data.SqlClient.SqlParameter("@user",user)
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Items_COPY", obj);

        }
        public int PURC_Items_COPY_Append(string systemcode, int? subsystemid, int? vesselid, string TOsystemcode, string TOsubsystemid, int? IsActive, string user)
        {


            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SYSTEMCODE", systemcode),
                   new System.Data.SqlClient.SqlParameter("@SUBSYSTEMID",subsystemid),
                   new System.Data.SqlClient.SqlParameter("@VESSELID",vesselid),
                   new System.Data.SqlClient.SqlParameter("@ToSystemCode",TOsystemcode),
                   new System.Data.SqlClient.SqlParameter("@ToSubSystemCode",TOsubsystemid),
                   new System.Data.SqlClient.SqlParameter("@ISACTIVE",IsActive),  
                   new System.Data.SqlClient.SqlParameter("@user",user)
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Items_COPY_Append", obj);

        }
        public int PURC_Items_COPY_OVERWRITE(string FROMsystemcode, string FROMsubsystemid, string FROMvesselid, string user, string TOsystemcode, string TOsubsystemid, string TOvesselid)
        {


            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@TOSYSTEMCODE", FROMsystemcode),
                   new System.Data.SqlClient.SqlParameter("@TOSUBSYSTEMID",FROMsubsystemid),

                   new System.Data.SqlClient.SqlParameter("@FROMSYSTEMCODE", TOsystemcode),
                   new System.Data.SqlClient.SqlParameter("@FROMSUBSYSTEMID",TOsubsystemid),
                   new System.Data.SqlClient.SqlParameter("@user",user)
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Items_COPY_OVERWRITE", obj);

        }
        public DataTable Get_Functional_Tree_Data_ManageSystems( int Vessel_ID,  string SearchText, int? IsActive, string reqsnType)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                  new System.Data.SqlClient.SqlParameter("@SearchText",SearchText),
                   new System.Data.SqlClient.SqlParameter("@IsActive",IsActive),
                   new System.Data.SqlClient.SqlParameter("@Function",null),
                   new System.Data.SqlClient.SqlParameter("@Equipment_Type",null),
                   new System.Data.SqlClient.SqlParameter("@FilterFuntionCode",null),
                   new System.Data.SqlClient.SqlParameter("@FormType",reqsnType)
                  
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_Functional_Tree_Data_ManageSystem_1709", obj).Tables[0];

        }
        public DataTable Get_Functional_Tree_ManageSystem_Data(int Vessel_ID, string SearchText, int? IsActive, string reqsnType,string Function,string Equipment_Type, string FilterFunctionCode)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                  new System.Data.SqlClient.SqlParameter("@SearchText",SearchText),
                   new System.Data.SqlClient.SqlParameter("@IsActive",IsActive),
                   new System.Data.SqlClient.SqlParameter("@Function",Function),
                   new System.Data.SqlClient.SqlParameter("@Equipment_Type",Equipment_Type),
                   new System.Data.SqlClient.SqlParameter("@FilterFuntionCode",FilterFunctionCode),
                   new System.Data.SqlClient.SqlParameter("@FormType",reqsnType)
                  
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "TEC_GET_Functional_Tree_Data_ManageSystem_1709", obj).Tables[0];

        }
       
    }
}
