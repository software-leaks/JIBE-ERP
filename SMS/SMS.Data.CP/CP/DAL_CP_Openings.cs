using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

using SMS.Data;

namespace SMS.Data.CP
{
    public class DAL_CP_Openings
    {
        
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private string connection = "";
       
        public DAL_CP_Openings(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_CP_Openings()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }


        public int UPD_BunkerDetail(int? Delivery_Bunker_ID, int? Charter_ID, int? Port_Id, int? Fuel_Type_Id, string Fuel_Type, string Operation_Type, double? Fuel_Amt, double? Unit_Price, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Delivery_Bunker_ID", Delivery_Bunker_ID),
                                        new SqlParameter("@Charter_ID", Charter_ID),
                                        new SqlParameter("@Port_Id", Port_Id),
                                        new SqlParameter("@Fuel_Type_Id", Fuel_Type_Id),
                                         new SqlParameter("@Fuel_Type", Fuel_Type),
                                        new SqlParameter("@Operation_Type", Operation_Type),
                                        new SqlParameter("@Fuel_Amt", Fuel_Amt),
                                        new SqlParameter("@Unit_Price", Unit_Price),
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int) SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_UPD_BunkerDetail", sqlprm);
        }



        public DataTable GetOpeningList(DataTable @StatusList,  int? pagenumber, int? pagesize, int? Vessel_Id, ref int isfetchcount)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                                new SqlParameter("@StatusList",@StatusList),
                                                new System.Data.SqlClient.SqlParameter("@PAGE_INDEX",pagenumber),
                                                new System.Data.SqlClient.SqlParameter("@PAGE_SIZE",pagesize),
                                                 new System.Data.SqlClient.SqlParameter("@Vessel_ID",Vessel_Id),
                                                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)
                                            };
             sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
             DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_SP_GetOpeningList", sqlprm);
             isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
             return ds.Tables[0];
        }



        public DataSet Get_OpeningDetails(int? Opening_ID, int? Vessel_Id)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Opening_Id",Opening_ID),
                                            new SqlParameter("@Vessel_Id",Vessel_Id)
                                            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_SP_Get_OpeningDetails", sqlprm);
        }


        public int Ins_Opening(int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_SP_Ins_Opening", sqlprm);
        }

        public int Ins_Opening_Updates(int? Opening_ID, int? Vessel_ID, string Entry_Type, string Progress_Remarks, string Charterer_Name, string Broker_Name,  string Contact_Email, string Contact_Mobile, string Contact_Name, string Opening, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@Opening_ID", Opening_ID),
                                        new SqlParameter("@Vessel_ID", Vessel_ID),
                                        new SqlParameter("@Entry_Type", Entry_Type),
                                        new SqlParameter("@Progress_Remarks", Progress_Remarks),
                                        new SqlParameter("@Charterer_Name", Charterer_Name),
                                        new SqlParameter("@Broker_Name", Broker_Name),
                                        new SqlParameter("@Contact_Email", Contact_Email),
                                        new SqlParameter("@Contact_Mobile", Contact_Mobile),
                                        new SqlParameter("@Contact_Name", Contact_Name),
                                        new SqlParameter("@Opening", Opening),


                                        new SqlParameter("@Created_By", Created_By)
                                    };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "CP_SP_Ins_Opening_Updates", sqlprm);
        }


        public DataTable GetVesselListAll(int? UserCompanyID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@UserCompanyID", UserCompanyID) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "CP_SP_GetVesselListAll", sqlprm).Tables[0];
        }


        public int DEL_Opening_Update(int? Created_By, int? Progress_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Created_By", Created_By),
                                             new SqlParameter("@Progress_ID", Progress_ID)
                                            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "CP_SP_DEL_Opening_Update", sqlprm);
        }




    }
}
