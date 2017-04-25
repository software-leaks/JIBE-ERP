using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace SMS.Data.TMSA
{
   public class DAL_TMSA_EEDI
    {
        private string connection = "";

        public DAL_TMSA_EEDI()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public int INSERT_EEDI_DL(int VesselID,double EEDI_Value,string Remarks, int Created_By)
       {
           SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselID",VesselID),
                                            new SqlParameter("@EEDI_Value",EEDI_Value),
                                              new SqlParameter("@Remarks",Remarks),
                                                new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
           sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
           SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TMSA_Insert_Vessel_EEDI", sqlprm);
           return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
       }

        public DataTable Check_EEDI(int VesselId)
        {
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@VesselId", VesselId) ,   
                 
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_CHECKDUP_EEDI", obj).Tables[0];

        }

        public DataTable SearchEEDI(int Vessel_ID, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                   new System.Data.SqlClient.SqlParameter("@zPAGE_INDEX",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@zPAGE_SIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@IS_FETCH_COUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_Search_Vessel_EEDI", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public DataTable Get_EEDI(int VesselId)
        {
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@VesselId", VesselId) ,   
                 
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "TMSA_GET_EEDI_Details", obj).Tables[0];

        }

        public int Delete_EEDI(int VesselID,  int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselID",VesselID),
                                         
                                            new SqlParameter("@Created_By",CreatedBy),

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TMSA_DEL_EEDI", sqlprm);
        }

        public int Edit_EEDI(int vesselid, decimal eedival, string remarks,int created_by)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselId",vesselid),
                                            new SqlParameter("@EEDI_Value",eedival),
                                            new SqlParameter("@Remarks",remarks), 
                                            new SqlParameter("@Created_By",created_by)
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "TMSA_UPD_EEDI", sqlprm);
        }
    }
}
