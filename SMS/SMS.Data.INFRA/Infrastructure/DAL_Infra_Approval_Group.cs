using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_Approval_Group
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_Approval_Group(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Infra_Approval_Group()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }


        public DataTable Get_Approval_Group_List_DL(int Group_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Group_ID",Group_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_Approval_Group_List", sqlprm).Tables[0];
        }



        public DataTable Get_Approval_Group_Search(string searchText, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@SearchText", searchText),
                  
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_Approval_Group_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }


        public int INS_Approval_Group_DL(string Group_Name, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                       
                                            new SqlParameter("@Group_Name",Group_Name),
                                            new SqlParameter("@Created_By",Created_By),
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_INS_Approval_Group_DETAILS", sqlprm);
        }

        public int Upd_Approval_Group_DL(int? Group_ID, string Group_Name, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                           
                                            new SqlParameter("@Group_ID",Group_ID),
                                            new SqlParameter("@Group_Name",Group_Name),
                                            new SqlParameter("@Created_By",Created_By),
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_UPD_Approval_Group_DETAILS", sqlprm);

        }


        public int Del_Approval_Group_DL(int? ID, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                         new SqlParameter("@ID",ID),
                                         new SqlParameter("@Created_By",Created_By),
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_DEL_Approval_Group_DETAILS", sqlprm);

        }



 


    }
}
