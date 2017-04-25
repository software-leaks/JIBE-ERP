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
    public class DAL_Infra_VesselLib
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";
        public DAL_Infra_VesselLib(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infra_VesselLib()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }


        //public DataTable Get_VesselList_DL()
        //{            
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselList").Tables[0];
        //}
        //public DataTable Get_VesselList_DL(int IsVessel)
        //{
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselList", new SqlParameter("IsVessel", IsVessel)).Tables[0];
        //}
        //public DataTable Get_VesselList_DL(int FleetID, int IsVessel)
        //{
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselList", new SqlParameter("FleetID", FleetID), new SqlParameter("IsVessel", IsVessel)).Tables[0];
        //}
        public DataTable Get_VesselList_DL(DataTable FleetIDList, int VesselID, int Vessel_Manager, string SearchText, int UserCompanyID, int IsVessel)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselList_MultiFleet", new SqlParameter("FleetIDList", FleetIDList),
                                                                                                new SqlParameter("VesselID", VesselID),
                                                                                                new SqlParameter("Vessel_Manager", Vessel_Manager),
                                                                                                new SqlParameter("SearchText", SearchText),
                                                                                                new SqlParameter("UserCompanyID", UserCompanyID),
                                                                                                new SqlParameter("IsVessel", IsVessel)
                                                                                                ).Tables[0];


        }
        //public DataTable Get_VesselList_DL(int? FleetID, int VesselID, int Vessel_Manager, string SearchText, int UserCompanyID, int IsVessel)
        //{

        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselList", new SqlParameter("FleetID", FleetID),
        //                                                                                        new SqlParameter("VesselID", VesselID),
        //                                                                                        new SqlParameter("Vessel_Manager", Vessel_Manager),
        //                                                                                        new SqlParameter("SearchText", SearchText),
        //                                                                                        new SqlParameter("UserCompanyID", UserCompanyID),
        //                                                                                        new SqlParameter("IsVessel", IsVessel)
        //                                                                                        ).Tables[0];


        //}
        public DataTable Get_VesselList_DL(int FleetID, int VesselID, int Vessel_Manager, string SearchText, int UserCompanyID, int IsVessel)
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselList", new SqlParameter("FleetID", FleetID),
                                                                                                new SqlParameter("VesselID", VesselID),
                                                                                                new SqlParameter("Vessel_Manager", Vessel_Manager),
                                                                                                new SqlParameter("SearchText", SearchText),
                                                                                                new SqlParameter("UserCompanyID", UserCompanyID),
                                                                                                new SqlParameter("IsVessel", IsVessel)
                                                                                                ).Tables[0];


        }
        public DataTable Get_UserVesselList_DL(int FleetID, int VesselID, int Vessel_Manager, string SearchText, int UserCompanyID, int IsVessel, int? UserID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_UserVesselList", new SqlParameter("FleetID", FleetID),
                                                                                                new SqlParameter("VesselID", VesselID),
                                                                                                new SqlParameter("Vessel_Manager", Vessel_Manager),
                                                                                                new SqlParameter("SearchText", SearchText),
                                                                                                new SqlParameter("UserCompanyID", UserCompanyID),
                                                                                                new SqlParameter("IsVessel", IsVessel),
                                                                                                new SqlParameter("UserID", UserID)
                                                                                                ).Tables[0];


        }
        public DataTable Get_SURVEY_VesselList_DL(int FleetID, int VesselID, int Vessel_Manager, string SearchText, int UserCompanyID, int IsVessel)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_SURVEY_Get_VesselList", new SqlParameter("FleetID", FleetID),
                                                                                                new SqlParameter("VesselID", VesselID),
                                                                                                new SqlParameter("Vessel_Manager", Vessel_Manager),
                                                                                                new SqlParameter("SearchText", SearchText),
                                                                                                new SqlParameter("UserCompanyID", UserCompanyID),
                                                                                                new SqlParameter("IsVessel", IsVessel)
                                                                                                ).Tables[0];


        }

        public DataTable Get_VesselListPreJoining_DL(int UserCompanyID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselList_PreJoining", new SqlParameter("UserCompanyID", UserCompanyID)).Tables[0];

        }

        public DataTable SearchVessel(string searchtext, int? fleet_id, int? vessel_id, int? vessel_manager, int? vessel_Flag, int? user_company_id, int? is_vessel, int? Vessel_Type
               , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@FleetID", fleet_id),
                   new System.Data.SqlClient.SqlParameter("@VesselID", vessel_id), 
                   new System.Data.SqlClient.SqlParameter("@Vessel_Manager", vessel_manager), 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@UserCompanyID", user_company_id), 
                   new System.Data.SqlClient.SqlParameter("@IsVessel", is_vessel), 
                   new System.Data.SqlClient.SqlParameter("@Vessel_Flag", vessel_Flag), 
                    new System.Data.SqlClient.SqlParameter("@Vessel_type", Vessel_Type), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_Vessel_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public DataTable Search_SURVEY_Vessel(string searchtext, int? fleet_id, int? vessel_id, int? vessel_manager, int? vessel_Flag, int? user_company_id, int? is_vessel, int? Vessel_Type
       , string sortby, int? sortdirection, int? pagenumber, int? pagesize, string CompanyID, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@FleetID", fleet_id),
                   new System.Data.SqlClient.SqlParameter("@VesselID", vessel_id), 
                   new System.Data.SqlClient.SqlParameter("@Vessel_Manager", vessel_manager), 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@UserCompanyID", user_company_id), 
                   new System.Data.SqlClient.SqlParameter("@IsVessel", is_vessel), 
                   new System.Data.SqlClient.SqlParameter("@Vessel_Flag", vessel_Flag), 
                    new System.Data.SqlClient.SqlParameter("@Vessel_type", Vessel_Type), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                    new System.Data.SqlClient.SqlParameter("@CompanyID",CompanyID),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_SURVEY_Get_Vessel_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public DataTable SearchFleet(string searchtext, int? vessel_manager, int? vessel_owner, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext), 
                   new System.Data.SqlClient.SqlParameter("@Vessel_Manager", vessel_manager), 
                   new System.Data.SqlClient.SqlParameter("@Vessel_Owner", vessel_owner), 
                 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_Fleet_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }


        public DataTable Search_SURVEY_Fleet(string searchtext, int? vessel_manager, int? vessel_owner, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, string CompanyID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext), 
                   new System.Data.SqlClient.SqlParameter("@Vessel_Manager", vessel_manager), 
                   new System.Data.SqlClient.SqlParameter("@Vessel_Owner", vessel_owner), 
                 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@companyID", CompanyID), 
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_SURVEY_Get_Fleet_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }




        public DataTable GetVesselDetails_ByID_DL(int Vessel_ID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_GetVesselDetails_ByID", new SqlParameter("Vessel_ID", Vessel_ID)).Tables[0];
        }

        public DataTable Get_SURVEY_VesselDetails_ByID_DL(int Vessel_ID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_SURVEY_GetVesselDetails_ByID", new SqlParameter("Vessel_ID", Vessel_ID)).Tables[0];
        }

        public DataTable GetFleetList_DL()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_FleetList").Tables[0];
        }

        public DataTable GetFleetList_DL(int UserCompanyID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_FleetList", new SqlParameter("UserCompanyID", UserCompanyID)).Tables[0];
        }

        public DataTable GetFleetList_DL(int UserCompanyID, int VesselManager)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_FleetList", new SqlParameter("UserCompanyID", UserCompanyID), new SqlParameter("VesselManager", VesselManager)).Tables[0];
        }


        public DataTable GetFleetList_ByID_DL(int? FleetID, int? CompanyID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                              { 
                                 new SqlParameter("@FleetID",FleetID)
                                 ,new SqlParameter("@CompanyID",CompanyID)
                              };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_Fleet_List_ByID", sqlprm).Tables[0];

        }



        //public DataTable GetVesselsByFleetID_DL(int FleetID)
        //{
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselList", new SqlParameter("FleetID", FleetID)).Tables[0];
        //}
        //public DataTable GetVesselsByFleetID_DL(int FleetID, int IsVessel)
        //{
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselList", new SqlParameter("FleetID", FleetID), new SqlParameter("IsVessel", IsVessel)).Tables[0];

        //    //string SQL = "";
        //    //if (FleetID > 0)
        //    //    SQL = "SELECT * FROM LIB_VESSELS WHERE active_status=1 and FLEETCODE = @FleetID and IsVessel = @IsVessel ORDER BY ISVESSEL,  VESSEL_NAME";
        //    //else
        //    //    SQL = "SELECT * FROM LIB_VESSELS WHERE active_status=1 and IsVessel = @IsVessel ORDER BY ISVESSEL,  VESSEL_NAME";

        //    //return SqlHelper.ExecuteDataset(connection, CommandType.Text, SQL, new SqlParameter("FleetID", FleetID), new SqlParameter("IsVessel", IsVessel)).Tables[0];
        //}

        //public DataTable GetVesselSearchData_DL(int flag, int vessel, int Mngby, int Fleet, string VesselValue, string FleetCode, string MgdByValue)
        //{
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GetVesselSearchData", new SqlParameter("flag", flag),
        //                                                                                                    new SqlParameter("vessel", vessel),
        //                                                                                                    new SqlParameter("Mngby", Mngby),
        //                                                                                                    new SqlParameter("Fleet", Fleet),
        //                                                                                                    new SqlParameter("VesselValue", VesselValue),
        //                                                                                                    new SqlParameter("FleetCode", FleetCode),
        //                                                                                                    new SqlParameter("MgdByValue", MgdByValue)).Tables[0];
        //}

        public DataSet ExecuteQuery_DL(string strSQL)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, strSQL);
        }

        public void DeleteVessel_DL(int Vessel_ID)
        {
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Vessel_SoftDelete_ByID", new SqlParameter("Vessel_ID", Vessel_ID));
        }
        public void DeleteVessel_DL(string Vessel_Code)
        {
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Vessel_SoftDelete_ByCode", new SqlParameter("Vessel_Code", Vessel_Code));
        }
        public void DeleteVessel_DL(int Vessel_ID, int Deleted_By)
        {
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "Vessel_SoftDelete_ByID", new SqlParameter("Vessel_ID", Vessel_ID), new SqlParameter("Deleted_By", Deleted_By));
        }

        public void AddVesselDetails_DL(string ExName1, string ExName2, string ExName3, string ExName4, string ShortName, int VesselCode, string Owner, string Operator, string Flag, string Callsign, int imono, int OfcNo, string classname, string classno, float Servspeed, string vesseltype, string size,
                          string KeelDt, string DlvryDt, string VesselYard, int hullno, string Hullltype, float LengthOA, float LengthBp, float Depth, float Breadth, float MastKeel, int MMISNo, float CargoTkCap, float SlopTkCap, float BallastTkCap, float Dw_Trop, float Dw_summ, float Dw_wint, float Dw_ballast,
                          float Disp_trop, float Disp_summ, float Disp_wint, float Disp_Ballast, float Drft_Trop, float Drft_summ, float Drft_wint, float Drft_ballast, float grt_inter, float grt_suez, float grt_panama, float nrt_inter, float nrt_suez, float nrt_panam, float lwt_inter, float lwt_suez, float lwt_panama,
                          string MEngine, string Aux_boiler, string Ablr_Cap, string ME_MCR, string ME_NCR, string Cop_Cap, string Aux_Engine, string Deck_Mech, string AE_Kw, string turb_Gent, string TG_Kw,
            string Dry_Last, string Dry_Next, string Dry_Latest, string Spl_Last, string Spl_Next, string Spl_Latest,
                          string Tail_Last, string Tail_Next, string Tail_Latest, string shipImage, string TankImage, int flag, string Email)
        {


            DateTime dtKeelDt = DateTime.Parse(KeelDt, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            DateTime dtDlvryDt = DateTime.Parse(DlvryDt, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            DateTime dtDry_Last = DateTime.Parse(Dry_Last, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            DateTime dtDry_Next = DateTime.Parse(Dry_Next, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            DateTime dtDry_Latest = DateTime.Parse(Dry_Latest, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            DateTime dtSpl_Last = DateTime.Parse(Spl_Last, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            DateTime dtSpl_Next = DateTime.Parse(Spl_Next, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            DateTime dtSpl_Latest = DateTime.Parse(Spl_Latest, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            DateTime dtTail_Last = DateTime.Parse(Tail_Last, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            DateTime dtTail_Next = DateTime.Parse(Tail_Next, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            DateTime dtTail_Latest = DateTime.Parse(Tail_Latest, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);


            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_AddVesselDetails", new SqlParameter("ExName1", ExName1),
                                       new SqlParameter("ExName2", ExName2), new SqlParameter("ExName3", ExName3), new SqlParameter("ExName4", ExName4),
                                       new SqlParameter("shortName", ShortName), new SqlParameter("VesselCode", VesselCode), new SqlParameter("Owner", Owner),
                                       new SqlParameter("Operator", Operator), new SqlParameter("Flag", Flag), new SqlParameter("CallSign", Callsign),
                                       new SqlParameter("imono", imono), new SqlParameter("Ofcno", OfcNo), new SqlParameter("ClassName", classname),
                                       new SqlParameter("ClassNo", classno), new SqlParameter("ServSpeed", Servspeed), new SqlParameter("vesselType", vesseltype),
                                       new SqlParameter("Size", size), new SqlParameter("KeelDt", dtKeelDt), new SqlParameter("dlvryDt", dtDlvryDt),
                                       new SqlParameter("VesselYard", VesselYard), new SqlParameter("hullno", hullno), new SqlParameter("HullType", Hullltype),
                                       new SqlParameter("LengthOA", LengthOA), new SqlParameter("LengthBP", LengthBp), new SqlParameter("Depth", Depth),
                                       new SqlParameter("Breadth", Breadth), new SqlParameter("MastKeel", MastKeel), new SqlParameter("MMSINo", MMISNo),
                                       new SqlParameter("CargoTkCap", CargoTkCap), new SqlParameter("SlopTkCap", SlopTkCap), new SqlParameter("BallasrTkCap", BallastTkCap),
                                       new SqlParameter("Dw_Trop", Dw_Trop), new SqlParameter("Dw_Summ", Dw_summ), new SqlParameter("Dw_wint", Dw_wint),
                                       new SqlParameter("Dw_Ballast", Dw_ballast), new SqlParameter("Disp_trop", Disp_trop), new SqlParameter("Disp_summ", Disp_summ),
                                       new SqlParameter("Disp_wint", Disp_wint), new SqlParameter("Disp_Ballast", Disp_Ballast), new SqlParameter("Drft_Trop", Drft_Trop),
                                       new SqlParameter("Drft_summ", Drft_summ), new SqlParameter("Drft_wint", Drft_wint), new SqlParameter("Drft_Ballast", Drft_ballast),
                                       new SqlParameter("grt_inter", grt_inter), new SqlParameter("grt_suez", grt_suez), new SqlParameter("grt_panama", grt_panama),
                                         new SqlParameter("nrt_inter", nrt_inter), new SqlParameter("nrt_suez", nrt_suez), new SqlParameter("nrt_panama", nrt_panam),
                                        new SqlParameter("lwt_inter", lwt_inter), new SqlParameter("lwt_suez", lwt_suez), new SqlParameter("lwt_panama", lwt_panama),
                                      new SqlParameter("MEngine", MEngine), new SqlParameter("Aux_Boiler", Aux_boiler), new SqlParameter("Ablr_cap", Ablr_Cap),
                                      new SqlParameter("ME_MCR", ME_MCR), new SqlParameter("ME_NCR", ME_NCR), new SqlParameter("Cop_Cap", Cop_Cap),
                                      new SqlParameter("Aux_Engine", Aux_Engine), new SqlParameter("Deck_Mech", Deck_Mech), new SqlParameter("AE_Kw", AE_Kw),
                                      new SqlParameter("Turb_Gent", turb_Gent), new SqlParameter("TG_Kw", TG_Kw), new SqlParameter("Dry_Last", dtDry_Last),
                                     new SqlParameter("Dry_Next", dtDry_Next), new SqlParameter("Dry_Latest", dtDry_Latest), new SqlParameter("Spl_Last", dtSpl_Last),
                                       new SqlParameter("Spl_Next", dtSpl_Next), new SqlParameter("Spl_Latest", dtSpl_Latest), new SqlParameter("Tail_Last", dtTail_Last),
                                    new SqlParameter("Tail_Next", dtTail_Next), new SqlParameter("Tail_Latest", dtTail_Latest), new SqlParameter("ShipImage", shipImage),
                                       new SqlParameter("TankImg", TankImage), new SqlParameter("Action", flag), new SqlParameter("Email", Email));
        }

        public DataSet GetInmarsatValues_ForVesselID_DL(int Vessel_ID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_GetInmarsatValues_ForVesselID", new SqlParameter("Vessel_ID", Vessel_ID));
        }

        public DataTable GetVesselLocations_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_VesselLocations").Tables[0];
        }
        public DataTable Get_VesselTypeList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_VesselTypeList").Tables[0];
        }

        public string Get_VesselCode_ByID_DL(int Vessel_ID)
        {
            return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselCode_ByID", new SqlParameter("Vessel_ID", Vessel_ID)).ToString();
        }
        public DataTable Get_VesselType_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselType").Tables[0];

        }
        public string Get_VesselID_ByCode_DL(string Vessel_Short_Code)
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselID_ByCode", new SqlParameter("Vessel_Short_Code", Vessel_Short_Code)));
        }
        public int INSERT_New_Vessel_DL(string Vessel_Code, string Vessel_Name, string Vessel_Short_Name, string EmailID, int Fleet_Code, int VesselOwner
            , DateTime? Takeoverdate, DateTime? HandoverDate, int? VesselFlag, int CreatedBy, decimal? min_CTM, string Odm_Enabled, string FileName,int? vesselType_Add)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_Code",Vessel_Code),
                                            new SqlParameter("@Vessel_Name",Vessel_Name),
                                            new SqlParameter("@Vessel_Short_Name",Vessel_Short_Name),
                                            new SqlParameter("@EmailID",EmailID),
                                            new SqlParameter("@Fleet_Code",Fleet_Code),
                                            new SqlParameter("@VesselOwner",VesselOwner),
                                            
                                            new SqlParameter("@Takeoverdate",Takeoverdate),
                                            new SqlParameter("@HandoverDate",HandoverDate),
                                            new SqlParameter("@VesselFlag",VesselFlag),
                                            new SqlParameter("@CreatedBy",CreatedBy),

                                            new SqlParameter("@Min_CTM",min_CTM),
                                            new SqlParameter("@ODM_ENABLED",Odm_Enabled),
                                            new SqlParameter("@FileName",FileName),
                                            new SqlParameter("@Vessel_type",vesselType_Add),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_INSERT_New_Vessel", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INSERT_New_Vessel_DL_WithImageName(string Vessel_Code, string Vessel_Name, string Vessel_Short_Name, string EmailID, int Fleet_Code, int VesselOwner
           , DateTime? Takeoverdate, DateTime? HandoverDate, int? VesselFlag, int CreatedBy, decimal? min_CTM, string Odm_Enabled, string FileName, string VesselImgName, int? vesselType_Add, Boolean IsVessel, string IMO_NO, string Call_Sign)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_Code",Vessel_Code),
                                            new SqlParameter("@Vessel_Name",Vessel_Name),
                                            new SqlParameter("@Vessel_Short_Name",Vessel_Short_Name),
                                            new SqlParameter("@EmailID",EmailID),
                                            new SqlParameter("@Fleet_Code",Fleet_Code),
                                            new SqlParameter("@VesselOwner",VesselOwner),
                                            
                                            new SqlParameter("@Takeoverdate",Takeoverdate),
                                            new SqlParameter("@HandoverDate",HandoverDate),
                                            new SqlParameter("@VesselFlag",VesselFlag),
                                            new SqlParameter("@CreatedBy",CreatedBy),

                                            new SqlParameter("@Min_CTM",min_CTM),
                                            new SqlParameter("@ODM_ENABLED",Odm_Enabled),
                                            new SqlParameter("@FileName",FileName),
                                            new SqlParameter("@VesselImgName",VesselImgName),
                                            new SqlParameter("@Vessel_type",vesselType_Add),
                                            new SqlParameter("@IsVessel",IsVessel),
                                            new SqlParameter("@IMO_NO",IMO_NO),
                                            new SqlParameter("@CALL_SIGN",Call_Sign),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_INSERT_New_Vessel", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public int INSERT_New_SURVEY_VesselDetails_DL(string Vessel_ID, string Vessel_Call_sign, string Vessel_IMO_No, string Vessel_Length_OA, string Vessel_MMSI_No, DateTime? yearBuilt)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Vessel_Call_sign",Vessel_Call_sign),
                                            new SqlParameter("@Vessel_IMO_No",Vessel_IMO_No),
                                            new SqlParameter("@Vessel_Length_OA",Vessel_Length_OA),
                                            new SqlParameter("@Vessel_MMSI_No",Vessel_MMSI_No),
                                            new SqlParameter("@Vessel_YearBuilt",yearBuilt),
                                            
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_SURVEY_INSERT_New_VesselDetail", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UPDATE_Vessel_DL(int Vessel_ID, string Vessel_Code, string Vessel_Name, string Vessel_Short_Name, string EmailID, int? Fleet_Code, int? Vessel_Manager
            , DateTime? Takeoverdate, DateTime? HandoverDate, int? VesselFlag, int CreatedBy, decimal? min_CTM, string Odm_Enabled, string FileName,int? vesselType_Add)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Vessel_Code",Vessel_Code),
                                            new SqlParameter("@Vessel_Name",Vessel_Name),
                                            new SqlParameter("@Vessel_Short_Name",Vessel_Short_Name),
                                            new SqlParameter("@EmailID",EmailID),
                                            new SqlParameter("@Fleet_Code",Fleet_Code),
                                            new SqlParameter("@Vessel_Manager",Vessel_Manager),
                                            new SqlParameter("@Takeoverdate",Takeoverdate),
                                            new SqlParameter("@HandoverDate",HandoverDate),
                                            new SqlParameter("@VesselFlag",VesselFlag),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            new SqlParameter("@Min_CTM",min_CTM),
                                            new SqlParameter("@ODM_ENABLED",Odm_Enabled),
                                            new SqlParameter("@FileName",FileName),
                                            new SqlParameter("@Vessel_type",vesselType_Add),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_UPDATE_Vessel", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public int UPDATE_Vessel_WithImageName(int Vessel_ID, string Vessel_Code, string Vessel_Name, string Vessel_Short_Name, string EmailID, int? Fleet_Code, int? Vessel_Manager
            , DateTime? Takeoverdate, DateTime? HandoverDate, int? VesselFlag, int CreatedBy, decimal? min_CTM, string Odm_Enabled, string FileName, string VesselImgName, int? vesselType_Add, Boolean IsVessel, string IMO_NO, string Call_Sign)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Vessel_Code",Vessel_Code),
                                            new SqlParameter("@Vessel_Name",Vessel_Name),
                                            new SqlParameter("@Vessel_Short_Name",Vessel_Short_Name),
                                            new SqlParameter("@EmailID",EmailID),
                                            new SqlParameter("@Fleet_Code",Fleet_Code),
                                            new SqlParameter("@Vessel_Manager",Vessel_Manager),
                                            new SqlParameter("@Takeoverdate",Takeoverdate),
                                            new SqlParameter("@HandoverDate",HandoverDate),
                                            new SqlParameter("@VesselFlag",VesselFlag),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            new SqlParameter("@Min_CTM",min_CTM),
                                            new SqlParameter("@ODM_ENABLED",Odm_Enabled),
                                            new SqlParameter("@FileName",FileName),
                                            new SqlParameter("@VesselImgName",VesselImgName),
                                            new SqlParameter("@Vessel_type",vesselType_Add),
                                            new SqlParameter("@IsVessel",IsVessel),
                                            new SqlParameter("@IMO_NO",IMO_NO),
                                            new SqlParameter("@CALL_SIGN",Call_Sign),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_UPDATE_Vessel", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int UPDATE_SURVEY_VesselDetails_DL(string Vessel_ID, string Vessel_Call_sign, string Vessel_IMO_No, string Vessel_Length_OA, string Vessel_MMSI_No, DateTime? YearBuilt)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Vessel_Call_sign",Vessel_Call_sign),
                                            new SqlParameter("@Vessel_IMO_No",Vessel_IMO_No),
                                            new SqlParameter("@Vessel_Length_OA",Vessel_Length_OA),
                                            new SqlParameter("@Vessel_MMSI_No",Vessel_MMSI_No),
                                            new SqlParameter("@Vessel_YearBuilt",YearBuilt),
                                          
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_SURVEY_UPDATE_VesselDetails", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public int Delete_MEPowerCurveAttachment(int Vessel_ID, int CreatedBy)
        {

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_DELETE_Power_Curve_Attachment", new SqlParameter("@Vessel_ID", Vessel_ID), new SqlParameter("@CreatedBy", CreatedBy));

        }





        public int INSERT_New_Fleet_DL(string FleetName, int VesselManager, string suptdEmail, string TechTeamEmail, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FleetName",FleetName),
                                            new SqlParameter("@VesselManager",VesselManager),

                                            new SqlParameter("@SuptdEmail",suptdEmail),
                                            new SqlParameter("@TechTeamEmail",TechTeamEmail),

                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_INSERT_New_Fleet", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public int Update_Fleet_DL(int FleetID, string FleetName, int VesselManager, string suptdEmail, string TechTeamEmail, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FleetID",FleetID),
                                            new SqlParameter("@FleetName",FleetName),
                                            new SqlParameter("@VesselManager",VesselManager),
                                            new SqlParameter("@SuptdEmail",suptdEmail),
                                            new SqlParameter("@TechTeamEmail",TechTeamEmail),
                                            new SqlParameter("@Created_By",Created_By),

                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_UPDATE_Fleet", sqlprm);
        }



        public int Delete_Fleet_DL(int FleetID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@FleetID",FleetID),
                                            new SqlParameter("@Created_By",Created_By),

                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_DELETE_Fleet", sqlprm);
        }




        public DataTable Get_VesselFlagList_DL(int UserCompanyID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_VesselFlagList", new SqlParameter("@UserCompanyID", UserCompanyID)).Tables[0];
        }

        public DataTable Get_Survey_VesselFlagList_DL()//int UserCompanyID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SURVEY_SP_Get_VesselFlagList"/*, new SqlParameter("@UserCompanyID", UserCompanyID)*/).Tables[0];
        }

        public DataTable Get_VesselFlagDetails_DL(int Vessel_Flag, int UserCompanyID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_SP_Get_VesselFlagDetails", new SqlParameter("@Vessel_Flag", Vessel_Flag), new SqlParameter("@UserCompanyID", UserCompanyID)).Tables[0];
        }

        public DataTable Get_VesselManagerList_DL()
        {

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_VesselManagers").Tables[0];
        }

        public DataTable Get_VesselInfo_VID_DL(int Vessel_ID, string Vessel_Code, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@Vessel_Code",Vessel_Code),
                                            new SqlParameter("@UserID",UserID)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselInfo_VID", sqlprm).Tables[0];
        }

        public int Delete_VesselImageAttachment(int Vessel_ID, int Created_By)
        {
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_DELETE_Vessel_Image_Attachment", new SqlParameter("@Vessel_ID", Vessel_ID), new SqlParameter("@CreatedBy", Created_By));
        }

        public DataTable Get_JiBePacketStatus_Search_DL(int? VesselCode, int? Status, int CompanyID, DateTime? Start_Date, DateTime? End_Date, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VesselCode",VesselCode),
                                            new SqlParameter("@Status",Status),
                                            new SqlParameter("@CompanyID",CompanyID),
                                             new SqlParameter("@Start_Date",Start_Date),
                                             new SqlParameter("@End_Date",End_Date),
                                            new SqlParameter("@SORTBY",sortby),
                                            new SqlParameter("@SORTDIRECTION",sortdirection),
                                            new SqlParameter("@PAGENUMBER",pagenumber),
                                            new SqlParameter("@PAGESIZE",pagesize),
                                            new SqlParameter("@ISFETCHCOUNT",isfetchcount),
                                       
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_JiBEPacketStatus", sqlprm);
            isfetchcount = Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
            return ds.Tables[0];
        }
        public DataSet Get_JiBeBuildAssemblies_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Get_JibeBuildAssemblies");
        }
        /// <summary>
        /// Rertive Vessel list according to User Veseel Assigment
        /// </summary>

        public DataTable Get_UserVesselList_Search_DL(int FleetID, int VesselID, int Vessel_Manager, string SearchText, int UserCompanyID, int IsVessel, int? UserID)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "FMS_Get_UserVesselList_Search", new SqlParameter("FleetID", FleetID),
                                                                                                new SqlParameter("VesselID", VesselID),
                                                                                                new SqlParameter("Vessel_Manager", Vessel_Manager),
                                                                                                new SqlParameter("SearchText", SearchText),
                                                                                                new SqlParameter("UserCompanyID", UserCompanyID),
                                                                                                new SqlParameter("IsVessel", IsVessel),
                                                                                                new SqlParameter("UserID", UserID)
                                                                                                ).Tables[0];


        }

        public DataTable Get_JiBeDeltaLicenseKeyRequest_DL(int Vessel_Owner)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
              { 
                  new SqlParameter("@Vessel_Owner",Vessel_Owner)
              };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_Delta_LicenseKey_Request",sqlprm).Tables[0];
        }

        public int Upd_JiBeDeltaSyncFlag_DL(int Vessel_ID)
        {
            string syncConnection = ConfigurationManager.ConnectionStrings["synconn"].ConnectionString;
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID)
                                        };
            return (int)SqlHelper.ExecuteNonQuery(syncConnection, CommandType.StoredProcedure, "UPD_Delta_Sync_Ins_Flag", sqlprm);
        }

        public int Upd_JiBeDeltaAutoSyncFlag_DL(int Vessel_ID)
        {
            string syncConnection = ConfigurationManager.ConnectionStrings["synconn"].ConnectionString;
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID)
                                        };
            return (int)SqlHelper.ExecuteNonQuery(syncConnection, CommandType.StoredProcedure, "UPD_Delta_Auto_Sync_Flag", sqlprm);
        }

        public string Upd_JiBeDeltaLicenseKey_DL(int Vessel_ID, string LicenseKey)
        {
            string License = LicenseKey;
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                            new SqlParameter("@LicenseKey",LicenseKey)
                                        };

            if (Convert.ToInt16(SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "UPD_Delta_LicenseKey", sqlprm)) > 0)
                return License;
            else
                return "";
        }

        public DataTable Get_JibeDeltaDllAcknowledgement_DL(int Vessel_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Vessel_ID",Vessel_ID),
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Get_Delta_DLL_Ack_Status",sqlprm).Tables[0];
        }
        //Description: Method used to Sync SQL to vessel after verification from JIT End
        public void SYNC_SQlQueryToVessels_DL(string TableName, string PKID, string PKValue, int Vessel_ID, string strQuery, string PK_NAMES_PR, string PK_VALUES_PR)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@TableName",TableName),
                                            new SqlParameter("@PKID",PKID),
                                            new SqlParameter("@PKValue",PKValue),
                                             new SqlParameter("@Vessel_ID",Vessel_ID),
                                             new SqlParameter("@SQL_UPDATE",strQuery),
                                            new SqlParameter("@PK_NAMES_PR",PK_NAMES_PR),
                                            new SqlParameter("@PK_VALUES_PR",PK_VALUES_PR),                                       
                                        };
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SYNC_SP_DataSynchronizer_DataLog", sqlprm);
        }
        // Description Method used to send script details to the JIT
        public DataTable GET_SQL_Script_Log_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GET_SQL_Script_Log").Tables[0];
        }
    }
}