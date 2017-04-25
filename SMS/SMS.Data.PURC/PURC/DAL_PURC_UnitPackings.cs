using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;

namespace SMS.Data.PURC
{
    public class DAL_PURC_UnitPackings
    {

        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_UnitPackings()
        {

        }

        public DataTable UnitPackings_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Unit_Packings_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }


        public DataTable UnitPackings_List(int? ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ID", ID)
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Lib_Unit_Packings_List", sqlprm).Tables[0];

        }


        public int InsertUnitPackings(string MAIN_PACK, string ABREVIATION, int Created_By)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@MAIN_PACK", MAIN_PACK),
                new SqlParameter("@ABREVIATION", ABREVIATION),
                new SqlParameter("@Created_By", Created_By),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_Lib_Unit_Packings", sqlprm);
        }


        public int EditUnitPackings(int ID, string MAIN_PACK, string ABREVIATION, int Modified_By)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ID", ID),
                new SqlParameter("@MAIN_PACK", MAIN_PACK),
                new SqlParameter("@ABREVIATION", ABREVIATION),
                new SqlParameter("@Modified_By", Modified_By),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Upd_Lib_Unit_Packings", sqlprm);
        }

        public int DeleteUnitPackings(int ID, int Deleted_By)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@ID", ID),
                new SqlParameter("@Deleted_By", Deleted_By),
            };
            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Del_Lib_Unit_Packings", sqlprm);

        }

    }
}
