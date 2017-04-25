using System;
using System.Collections.Generic;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

/// <summary>
/// Summary description for DAL_Infra_VesselMaster
/// </summary>
/// 

namespace SMS.Data.Infrastructure
{

    public class DAL_Infra_VesselType
    {

        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_VesselType(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_VesselType()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }
        public int InsertVesselType_DL(string VesselTypes, int CreatedBy, string TankerType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {  new SqlParameter("@VesselTypes",VesselTypes),
                                           new SqlParameter("@CreatedBy",CreatedBy),
                                           new SqlParameter("@TankerType",TankerType)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_VesselType", sqlprm);
        }

        public DataTable Get_VesselTypeList_DL(int? VesselTypeID)
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselTypeList").Tables[0];
        }

        public int EditVesselType_DL(int VesselTypeID, string UserType, int CreatedBy,string TankerType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@ID",VesselTypeID),
                                          new SqlParameter("@VesselTypes",UserType),
                                          new SqlParameter("@CreatedBy",CreatedBy),
                                          new SqlParameter("@TankerType",TankerType)

                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_VesselType", sqlprm);
        }
        public int DeleteVesselType_DL(int VesselTypeID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@ID",VesselTypeID),
                                          new SqlParameter("@CreatedBy",CreatedBy),

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Del_VesselType", sqlprm);
        }
        public DataTable SearchVesselType(string searchtext
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_VesselType_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public DataTable Get_VesselType_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselType").Tables[0];

        }


    }

}

