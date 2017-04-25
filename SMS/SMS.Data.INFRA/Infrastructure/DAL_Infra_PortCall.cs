using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_PortCall
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_PortCall(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Infra_PortCall()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }


        public DataTable Get_PortCall_List_DL(int Port_Call_ID, int Vessel_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 

                                            new SqlParameter("@Port_Call_ID",Port_Call_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_Port_Call_DETAILS_List", sqlprm).Tables[0];
        }



        public DataTable Get_PortCall_Search(string searchText, int? Vessel_ID,int? Port_ID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 

                new System.Data.SqlClient.SqlParameter("@SearchText", searchText),
                new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                new System.Data.SqlClient.SqlParameter("@Port_ID", Port_ID),

                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),

            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_Port_Call_List_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }


        public int Ins_PortCall_Details_DL(string Vessel_Code, string Sub_Voyage, string Port_Name, DateTime? Arrival, DateTime? Berthing
            , DateTime? Departure, string Auto_Date, string Berth_Number, string Port_Remarks, int? Charterers_Agent, int? Owners_Agent
            , int Port_ID, string Charter_ID, int Vessel_ID, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 

                                            new SqlParameter("@Vessel_Code",Vessel_Code),
                                            new SqlParameter("@Sub_Voyage",Sub_Voyage),

                                            new SqlParameter("@Port_Name",Port_Name),
                                            new SqlParameter("@Arrival",Arrival),
                                            new SqlParameter("@Berthing",Berthing),
                                            new SqlParameter("@Departure",Departure),
                                            new SqlParameter("@Auto_Date",Auto_Date),
                                            new SqlParameter("@Berth_Number",Berth_Number),
                                            new SqlParameter("@Port_Remarks",Port_Remarks),
                                            new SqlParameter("@Charterers_Agent",Charterers_Agent),
                                            new SqlParameter("@Owners_Agent",Owners_Agent),
                                            new SqlParameter("@Port_ID",Port_ID),
                                            new SqlParameter("@Charter_ID",Charter_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Created_By",Created_By),
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_INS_Port_Call_DETAILS", sqlprm);
        }

        public int Upd_PortCall_Details_DL(int Port_Call_ID, string Vessel_Code, string Sub_Voyage, string Port_Name, DateTime? Arrival, DateTime? Berthing
            , DateTime? Departure, string Auto_Date, string Berth_Number, string Port_Remarks, int? Charterers_Agent, int? Owners_Agent
            , int Port_ID, string Charter_ID, int Vessel_ID, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 

                                            new SqlParameter("@Port_Call_ID",Port_Call_ID),
                                            new SqlParameter("@Vessel_Code",Vessel_Code),
                                            new SqlParameter("@Sub_Voyage",Sub_Voyage),

                                            new SqlParameter("@Port_Name",Port_Name),
                                            new SqlParameter("@Arrival",Arrival),
                                            new SqlParameter("@Berthing",Berthing),
                                            new SqlParameter("@Departure",Departure),
                                            new SqlParameter("@Auto_Date",Auto_Date),
                                            new SqlParameter("@Berth_Number",Berth_Number),
                                            new SqlParameter("@Port_Remarks",Port_Remarks),
                                            new SqlParameter("@Charterers_Agent",Charterers_Agent),
                                            new SqlParameter("@Owners_Agent",Owners_Agent),
                                            new SqlParameter("@Port_ID",Port_ID),
                                            new SqlParameter("@Charter_ID",Charter_ID),
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Created_By",Created_By),

                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_UPD_Port_Call_DETAILS", sqlprm);

        }


        public int Del_PortCall_Details_DL(int Port_Call_ID, int Vessel_ID, int Created_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Port_Call_ID", Port_Call_ID),
                                        new SqlParameter("@Vessel_ID", Vessel_ID),
                                        new SqlParameter("@Created_By", Created_By),
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_DEL_Port_Call_DETAILS", sqlprm);

        }
    }
}
