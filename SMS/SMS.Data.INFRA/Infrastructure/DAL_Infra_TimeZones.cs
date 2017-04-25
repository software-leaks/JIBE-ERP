using System;
using System.Collections.Generic;
 
 
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SMS.Data.Infrastructure
{
   public class DAL_Infra_TimeZones
    {

         IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_TimeZones(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_TimeZones()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }



        public DataTable Get_TimeZoneList_DL(string searchtext 
           , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                
                    
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_TimeZone_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            
            return ds.Tables[0];
        }

        public DataTable Get_TimeZoneList_DL(int? TimeZoneID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TimeZoneID",TimeZoneID)                                         
                                         };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_TimeZoneList").Tables[0];
        }

        public int PopulateTimeZones_DL(int CreatedBy)
        {
            DataTable dtTimeZones = Get_TimeZoneList_DL();
            TimeZone zone = TimeZone.CurrentTimeZone;

            SqlParameter[] sqlprm = new SqlParameter[]
            {
                new SqlParameter("@dtTimeZones",dtTimeZones), 
                new SqlParameter("@CurrentTimeZone",zone.StandardName),
                new SqlParameter("@CreatedBy",CreatedBy),
                new SqlParameter("@return",SqlDbType.Int)
            };
            
            sqlprm[3].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_PopulateTimeZones", sqlprm);
            return Convert.ToInt32(sqlprm[2].Value);
        }

        private DataTable Get_TimeZoneList_DL()
        {
            ReadOnlyCollection<TimeZoneInfo> tzCollection;
            tzCollection = TimeZoneInfo.GetSystemTimeZones();

            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(TimeZoneInfo));
            DataTable table = new DataTable();
            
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name);
            foreach (TimeZoneInfo item in tzCollection)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public int EditTimeZone_DL(int TimeZoneID, string TimeZone_DisplayName,string BaseUtcOffSet,int DefaultTimeZone, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@ID",TimeZoneID),
                                          new SqlParameter("@DisplayName",TimeZone_DisplayName),
                                          new SqlParameter("@BaseUtcOffSet",BaseUtcOffSet),
                                          new SqlParameter("@DefaultTimeZone",DefaultTimeZone),
                                          new SqlParameter("@CreatedBy",CreatedBy),

                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_TimeZone", sqlprm);
        }

        public int InsertTimeZone_DL(string TimeZone, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {  new SqlParameter("@TimeZone",TimeZone),
                                           new SqlParameter("@CreatedBy",CreatedBy),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_TimeZone", sqlprm);
        }

        public int DeleteTimeZone_DL(int TimeZoneID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@TimeZoneID",TimeZoneID),
                                          new SqlParameter("@CreatedBy",CreatedBy),

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Del_TimeZone", sqlprm);
        }
       
    }
    
}
