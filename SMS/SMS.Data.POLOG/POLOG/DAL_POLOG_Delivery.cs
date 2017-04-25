using System;
using System.Collections.Generic;

using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace SMS.Data.POLOG
{
    public class DAL_POLOG_Delivery
    {
         IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        public static string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        //private static string connection = "";
        public DAL_POLOG_Delivery(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_POLOG_Delivery()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
         public static DataSet POLOG_Get_Delivery_List(int? Supply_ID, string CurrStatus, int? CreatedBy)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                new System.Data.SqlClient.SqlParameter("@CurrStatus", CurrStatus),
                 new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Delivery_List", obj);
        }

        public static int POLog_Insert_Delivery_Item(int? ID, int? Delivery_ID, string Name, decimal? PO_Qty, decimal? PO_Price, string Confirm_Unit, decimal? Confirm_Qty, decimal? Confirm_Price, string Remarks, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@ID", ID),
                 new System.Data.SqlClient.SqlParameter("@Delivery_ID", Delivery_ID),
                 new System.Data.SqlClient.SqlParameter("@Name", Name),
                 new System.Data.SqlClient.SqlParameter("@POQty", PO_Qty),
                 new System.Data.SqlClient.SqlParameter("@POprice", PO_Price),
                 new System.Data.SqlClient.SqlParameter("@Confirm_Unit", Confirm_Unit),
                 new System.Data.SqlClient.SqlParameter("@Confirm_Qty", Confirm_Qty),
                 new System.Data.SqlClient.SqlParameter("@Confirm_Price", Confirm_Price),
                 new System.Data.SqlClient.SqlParameter("@Remarks", Remarks),
                 new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
                 //new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            //obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_INS_Delivery_Item", obj);
            //return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static string POLOG_Insert_Delivery_Details(string Delivery_ID, int? Supply_ID, DateTime? DeliveryDate, string Location, int? PortCallID, string Remarks, string Action_By_Button, string DeliveryStatus, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Delivery_ID", Delivery_ID),
                 new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@DeliveryDate", DeliveryDate),
                 new System.Data.SqlClient.SqlParameter("@Location", Location),
                 new System.Data.SqlClient.SqlParameter("@PortCallID", PortCallID),
                 new System.Data.SqlClient.SqlParameter("@Remarks", Remarks),
                 new System.Data.SqlClient.SqlParameter("@Action_By_Button", Action_By_Button),
                 new System.Data.SqlClient.SqlParameter("@DeliveryStatus", DeliveryStatus),
                 new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
                 //new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
           // obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            return (string)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_INS_Delivery_Details", obj);
            //return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static int POLOG_Delete_Delivery_Details(string Delivery_ID, int? Supply_ID, string Action_By_Button, string DeliveryStatus, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Delivery_ID", Delivery_ID),
                 new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                  new System.Data.SqlClient.SqlParameter("@Action_By_Button", Action_By_Button),
                 new System.Data.SqlClient.SqlParameter("@DeliveryStatus", DeliveryStatus),
                 new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_DEL_Delivery_Details", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
        public static DataSet POLOG_Get_Delivery_Details(string Delivery_ID, int? Supply_ID, string Type, int? CreatedBy)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Delivery_ID", Delivery_ID),
                new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                new System.Data.SqlClient.SqlParameter("@Type", Type),
                new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Delivery_Deatils", obj);
        }
        public static DataSet POLOG_Get_Delivery_Item_Details(string Delivery_ID, int? Supply_ID, string Type, int? CreatedBy)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Delivery_ID", Delivery_ID),
                new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                new System.Data.SqlClient.SqlParameter("@Type", Type),
                new System.Data.SqlClient.SqlParameter("@CreatedBy", CreatedBy),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Delivery_Item_Deatils", obj);
        }
        public static DataSet POLOG_Get_Delivery_Item_Details(int? ID, int? DeliveryID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@ID", ID),
                new System.Data.SqlClient.SqlParameter("@DeliveryID", DeliveryID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "POLOG_Get_Delivery_Item_Deatils", obj);
        }
        public static int POLOG_Delete_Delivery_Item(int? ID, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@ID", ID),
                 new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
                 new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "POLOG_DEL_Delivery_Item", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }

        public static int POLog_Insert_Delivery_Item(string Delivery_ID, int? Supply_ID, DataTable dtItem, int? CreatedBy)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@Delivery_ID", Delivery_ID),
                 new System.Data.SqlClient.SqlParameter("@Supply_ID", Supply_ID),
                 new System.Data.SqlClient.SqlParameter("@dtExtraItems", dtItem),
                 new System.Data.SqlClient.SqlParameter("@Created_By", CreatedBy),
                 //new System.Data.SqlClient.SqlParameter("RETURN",SqlDbType.Int)
            };
            //obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "POLOG_INS_Delivery_Item_Details", obj);
            //return Convert.ToInt32(obj[obj.Length - 1].Value);
        }
    }
}
