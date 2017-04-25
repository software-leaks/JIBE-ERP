using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS.Data.Technical
{
   public class DAL_Tec_Worklist_Admin
    {
        
        private string connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DataTable SearchAssigner(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
             DataTable dt = new  DataTable();

            SqlParameter[] obj = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
               
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_SP_Assigner_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }


        public DataTable Get_Assigner_List_DL(int? AssignerID)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
             SqlParameter[] obj = new SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@AssignerID", AssignerID),
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURV_SP_Get_Assigner_List", obj).Tables[0];
        }





        public int EditAssigner_DL(int AssignerID, string AssignerValue, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@AssignerID",AssignerID),
                                            new SqlParameter("@AssignerValue",AssignerValue),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_SP_Update_Assigner", sqlprm);
        }

        public int InsertAssigner_DL(string AssignerValue, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@AssignerValue",AssignerValue),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_SP_Insert_Assigner", sqlprm);
        }

        public int DeleteAssigner_DL(int AssignerID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@AssignerID",AssignerID),
                                          new SqlParameter("@CreatedBy",CreatedBy),

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SURV_SP_Del_Assigner", sqlprm);
        }


    }
}
