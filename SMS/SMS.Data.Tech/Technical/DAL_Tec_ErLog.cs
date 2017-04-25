using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.Data.Technical
{
    public class DAL_Tec_ErLog
    {
        private static string connection = "";
        public DAL_Tec_ErLog(string ConnectionStr)
        {
            connection = ConnectionStr;
        }

        public DAL_Tec_ErLog()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }


        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
            }
        }

        public static DataTable ErLog_ME_TB_Search(int logId, int vessel_id, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                   new System.Data.SqlClient.SqlParameter("@LOGID",logId),
                    new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ERLogME_01", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable ErLog_ME_01_Search(int logId, int vessel_id, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@LOGID",logId),
                    new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ERLogME_01", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable ErLog_MEngine_01_Search(int logId, int vessel_id, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@LOGID",logId),
                    new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ERLogME_01", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public static DataTable ErLog_ME_02_Search(int logId, int vessel_id, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@LOGID",logId),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ERLogME_02", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public static DataTable ErLog_AC_FM_MISC_Search(int logId, int vessel_id, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@LOGID",logId),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ACFWMISC", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public static DataTable ErLog_ME_00_Search(int? VesselList, DateTime? Fromdate, DateTime? Todate, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {      
                    
                    new System.Data.SqlClient.SqlParameter("@VESSELLIST", VesselList), 
                    new System.Data.SqlClient.SqlParameter("@FROMDATE",Fromdate),
                    new System.Data.SqlClient.SqlParameter("@TODATE",Todate),
                    new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                    new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                    new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                    new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                    new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ERLOGDETAILS", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public static DataTable ErLog_ME_00_Get(int logId, int vessel_id)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {      
                     new System.Data.SqlClient.SqlParameter("@LOGID",logId),   
                     new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_GET_ERLOGDETAILS", obj);

            return ds.Tables[0];
        }
        public static DataTable GET_ERLOGVERSIONS(int logId, int vessel_id)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {      
                     new System.Data.SqlClient.SqlParameter("@LOGID",logId),   
                     new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_GET_ERLOGVERSIONS", obj);

            return ds.Tables[0];
        }
        public static DataTable ErLog_Generator_Engine_Search(int logId, int vessel_id, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {      
                   new System.Data.SqlClient.SqlParameter("@LOGID",logId),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_GTRENGINE", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public static DataTable ErLog_TASG_Search(int logId, int vessel_id, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {      
                   new System.Data.SqlClient.SqlParameter("@LOGID",logId),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ERLOGTASG", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public static DataTable ErLog_Tank_Levels_Search(int logId, int vessel_id, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {       
                   new System.Data.SqlClient.SqlParameter("@LOGID",logId),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_TANKLEVEL", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }


        public static DataTable ErLog_Engineer_Officer_Remarks_Search(int logId, int vessel_id, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                    
                   new System.Data.SqlClient.SqlParameter("@LOGID",logId),
                   new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH__ENGOFFICER_REMARKS", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }


        public static DataTable ErLog_AC_FM_MISC_EDIT(int Vessel_Id)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Vessel_Id", Vessel_Id)                       
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_EDIT_ACFWMISC_THRESHOLD", obj);
            return ds.Tables[0];
        }
        public static DataTable ErLog_ME_01_EDIT(int Vessel_Id)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Vessel_Id", Vessel_Id)                                     
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_EDIT_ERLOGME_01_THRESHOLD", obj);
            return ds.Tables[0];
        }

        public static DataTable ErLog_TASG_EDIT(int Vessel_Id)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {
                new System.Data.SqlClient.SqlParameter("@Vessel_Id", Vessel_Id)                                     
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_EDIT_ERLOGTASG_THRESHOLD", obj);
            return ds.Tables[0];
        }

        public static DataTable ERLOG_GENERATOR_ENGINES_EDIT(int Vessel_Id)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Vessel_Id", Vessel_Id)                 
                    
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_EDIT_TEC_GTRENGINE_THRESHOLD", obj);
            return ds.Tables[0];
        }

        public static DataTable ErLog_ME_02_EDIT(int Vessel_Id)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Vessel_Id", Vessel_Id)                                     
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_EDIT_ERLOGME_02_THRESHOLD", obj);
            return ds.Tables[0];
        }
        public static DataTable ErLog_TANK_LEVELS_EDIT(int Vessel_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Vessel_Id", Vessel_ID)                 
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_EDIT_TANKLEVEL_THRESHOLD", obj);

            return ds.Tables[0];
        }

        public static DataTable ErLog_ThresHold_Main_EDIT(int Vessel_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Vessel_Id", Vessel_ID)                      
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_EDIT_ERLOGDETAILS_THRESHOLD", obj);

            return ds.Tables[0];
        }

        public static int ErLog_ME_01_THRESHOLD_Update(int? id, int vessel_id, decimal? watch_hours_min, decimal? watch_minutes_min, decimal? me_counter_min, decimal? me_rpm_min, decimal? me_control_min, decimal? me_gov_ctrl_min, decimal? me_load_indicator_min,
          decimal? me_fuel_pp_index_min, decimal? me_fuel_flowmeter_min, decimal? me_exh_temp_min, decimal? watch_hours_max, decimal? watch_minutes_max, decimal? me_counter_max, decimal? me_rpm_max, decimal? me_control_max, decimal? me_gov_ctrl_max, decimal? me_load_indicator_max,
          decimal? me_fuel_pp_index_max, decimal? me_fuel_flowmeter_max, decimal? me_exh_temp_max, decimal? tc_rpm_min, decimal? tc_rpm_max, decimal? t_exh_in_min, decimal? t_exh_in_max, decimal? t_exh_out_min, decimal? t_exh_out_max, decimal? t_air_in_min, decimal? t_air_in_max,
          decimal? t_air_out_min, decimal? t_air_out_max, decimal? t_scavenge_min, decimal? t_scavenge_max, decimal? t_cw_in_min, decimal? t_cw_in_max, decimal? t_cw_out_min, decimal? t_cw_out_max, decimal? t_lo_b_min, decimal? t_lo_b_max, decimal? t_lo_t_min, decimal? t_lo_t_max,
          decimal? p_scavenge_kgpcms_min, decimal? p_scavenge_kgpcms_max, decimal? p_exh_back_min, decimal? p_exh_back_max, decimal? p_pd_ac_min, decimal? p_pd_ac_max, decimal? p_pd_af_min, decimal? p_pd_af_max, int? modified_by)
        {


            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@ID",id),    
                new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                new System.Data.SqlClient.SqlParameter("@WATCH_HOURS_Min",watch_hours_min),
                new System.Data.SqlClient.SqlParameter("@WATCH_MINUTES_MIN",watch_minutes_min),
                new System.Data.SqlClient.SqlParameter("@ME_COUNTER_MIN",me_counter_min),
                new System.Data.SqlClient.SqlParameter("@ME_RPM_MIN",me_rpm_min),
                new System.Data.SqlClient.SqlParameter("@ME_CONTROL_MIN",me_control_min),
                new System.Data.SqlClient.SqlParameter("@ME_GOV_CTRL_MIN",me_gov_ctrl_min),
                new System.Data.SqlClient.SqlParameter("@ME_LOAD_INDICATOR_MIN",me_load_indicator_min),
                new System.Data.SqlClient.SqlParameter("@ME_FUEL_PP_INDEX_Min",me_fuel_pp_index_min),
                new System.Data.SqlClient.SqlParameter("@ME_FUEL_FLOWMETER_Min",me_fuel_flowmeter_min),
                new System.Data.SqlClient.SqlParameter("@ME_EXH_TEMP_Min",me_exh_temp_min),
                new System.Data.SqlClient.SqlParameter("@WATCH_HOURS_Max",watch_hours_max),
                new System.Data.SqlClient.SqlParameter("@WATCH_MINUTES_Max",watch_minutes_max	),
                new System.Data.SqlClient.SqlParameter("@ME_COUNTER_Max",me_counter_max	),
                new System.Data.SqlClient.SqlParameter("@ME_RPM_Max",me_rpm_max	),
                new System.Data.SqlClient.SqlParameter("@ME_CONTROL_Max",me_control_max	),
                new System.Data.SqlClient.SqlParameter("@ME_GOV_CTRL_Max",me_gov_ctrl_max	),
                new System.Data.SqlClient.SqlParameter("@ME_LOAD_INDICATOR_Max",me_load_indicator_max),
                new System.Data.SqlClient.SqlParameter("@ME_FUEL_PP_INDEX_Max",me_fuel_pp_index_max),
                new System.Data.SqlClient.SqlParameter("@ME_FUEL_FLOWMETER_Max",me_fuel_flowmeter_max),
                new System.Data.SqlClient.SqlParameter("@ME_EXH_TEMP_Max",me_exh_temp_max	),                
                new System.Data.SqlClient.SqlParameter("@TC_RPM_min",tc_rpm_min),
                new System.Data.SqlClient.SqlParameter("@TC_RPM_Max",tc_rpm_max),
                new System.Data.SqlClient.SqlParameter("@T_EXH_IN_Min",t_exh_in_min),
                new System.Data.SqlClient.SqlParameter("@T_EXH_IN_Max",t_exh_in_max),
                new System.Data.SqlClient.SqlParameter("@T_EXH_OUT_Min",t_exh_out_min),
                new System.Data.SqlClient.SqlParameter("@T_EXH_OUT_Max",t_exh_out_max),               
                new System.Data.SqlClient.SqlParameter("@T_EXH_AIR_IN_MIN",t_air_in_min),
                new System.Data.SqlClient.SqlParameter("@T_EXH_AIR_IN_Max",t_air_in_max),
                new System.Data.SqlClient.SqlParameter("@T_EXH_AIR_OUT_MIN",t_air_out_min),
                new System.Data.SqlClient.SqlParameter("@T_EXH_AIR_OUT_Max",t_air_out_max),
                new System.Data.SqlClient.SqlParameter("@T_SCAVENGE_Min",t_scavenge_min), 
                new System.Data.SqlClient.SqlParameter("@T_SCAVENGE_Max",t_scavenge_max), 
                new System.Data.SqlClient.SqlParameter("@T_CW_IN_Min",t_cw_in_min),
                new System.Data.SqlClient.SqlParameter("@T_CW_IN_Max",t_cw_in_max),
                new System.Data.SqlClient.SqlParameter("@T_CW_OUT_Min",t_cw_out_min),
                new System.Data.SqlClient.SqlParameter("@T_CW_OUT_Max",t_cw_out_max),
                new System.Data.SqlClient.SqlParameter("@T_LO_B_Min",t_lo_b_min),
                new System.Data.SqlClient.SqlParameter("@T_LO_B_Max",t_lo_b_max),               
                new System.Data.SqlClient.SqlParameter("@T_LO_T_Min",t_lo_t_min),
                new System.Data.SqlClient.SqlParameter("@T_LO_T_Max",t_lo_t_max),               
                new System.Data.SqlClient.SqlParameter("@P_SCAVENGE_KGPCMS_Min",p_scavenge_kgpcms_min),
                new System.Data.SqlClient.SqlParameter("@P_SCAVENGE_KGPCMS_Max",p_scavenge_kgpcms_max),
                new System.Data.SqlClient.SqlParameter("@P_EXH_BACK_Min",p_exh_back_min),
                 new System.Data.SqlClient.SqlParameter("@P_EXH_BACK_Max",p_exh_back_max),
                new System.Data.SqlClient.SqlParameter("@P_PD_AC_Min",p_pd_ac_min),
                new System.Data.SqlClient.SqlParameter("@P_PD_AC_Max",p_pd_ac_max),
                new System.Data.SqlClient.SqlParameter("@P_PD_AF_Min",p_pd_af_min),
                new System.Data.SqlClient.SqlParameter("@P_PD_AF_Max",p_pd_af_max),               
                new System.Data.SqlClient.SqlParameter("@MODIFIED_BY",modified_by)                   
            };

            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_UPDATE_ERLOGME_01_THRESHOLD", obj);


        }
        public static int ErLog_ME_02_THRESHOLD_Update(int? id, int? vessel_id, decimal? mb_in_min, decimal? mb_in_max, decimal? mb_out_min, decimal? mb_out_max, decimal? jc_in_min, decimal? jc_in_max,
          decimal? jc_out_min, decimal? jc_out_max, decimal? pc_in_min, decimal? pc_in_max, decimal? pc_out_min, decimal? pc_out_max, decimal? fuel_oil_min, decimal? fuel_oil_max, decimal? fuel_visc_min, decimal? fuel_visc_max,
          decimal? jc_sw_in_min, decimal? jc_sw_in_max, decimal? jc_sw_out_min, decimal? jc_sw_out_max, decimal? jc_fw_in_min, decimal? jc_fw_in_max, decimal? jc_fw_out_min, decimal? jc_fw_out_max, decimal? lc_sw_in_min,
          decimal? lc_sw_in_max, decimal? lc_sw_out_min, decimal? lc_sw_out_max, decimal? lc_lo_in_min, decimal? lc_lo_in_max, decimal? lc_lo_out_min, decimal? lc_lo_out_max, decimal? pc_sw_in_min, decimal? pc_sw_in_max,
          decimal? pc_sw_out_min, decimal? pc_sw_out_max, decimal? pc_lo_in_min, decimal? pc_lo_in_max, decimal? pc_lo_out_min, decimal? pc_lo_out_max, decimal? p_sea_water_min, decimal? p_sea_water_max, decimal? p_jacket_water_min,
          decimal? p_jacket_water_max, decimal? p_bearing_xnd_lo_min, decimal? p_bearing_xnd_lo_max, decimal? p_camshaft_lo_min, decimal? p_camshaft_lo_max, decimal? p_fv_cooling_min, decimal? p_fv_cooling_max,
          decimal? p_fuel_oil_min, decimal? p_fuel_oil_max, decimal? p_piston_cooling_min, decimal? p_piston_cooling_max, decimal? p_control_air_min, decimal? p_control_air_max, decimal? p_service_air_min,
          decimal? p_service_air_max, int modified_by)
        {


            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@ID",id),
                new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                new System.Data.SqlClient.SqlParameter("@MB_IN_Min",mb_in_min),
                new System.Data.SqlClient.SqlParameter("@MB_IN_Max",mb_in_max),
                new System.Data.SqlClient.SqlParameter("@MB_OUT_Min",mb_out_min),
                new System.Data.SqlClient.SqlParameter("@MB_OUT_Max",mb_out_max),
                new System.Data.SqlClient.SqlParameter("@JC_IN_Min",jc_in_min),
                new System.Data.SqlClient.SqlParameter("@JC_IN_Max",jc_in_max),
                new System.Data.SqlClient.SqlParameter("@JC_OUT_Min",jc_out_min),
                new System.Data.SqlClient.SqlParameter("@JC_OUT_Max",jc_out_max),
                new System.Data.SqlClient.SqlParameter("@PC_IN_Min",pc_in_min),
                new System.Data.SqlClient.SqlParameter("@PC_IN_Max",pc_in_max),
                new System.Data.SqlClient.SqlParameter("@PC_OUT_Min",pc_out_min),
                new System.Data.SqlClient.SqlParameter("@PC_OUT_Max",pc_out_max),
                new System.Data.SqlClient.SqlParameter("@FUEL_OIL_Min",fuel_oil_min),
                new System.Data.SqlClient.SqlParameter("@FUEL_OIL_Max",fuel_oil_max),
                new System.Data.SqlClient.SqlParameter("@FUEL_VISC_Min",fuel_visc_min),
                new System.Data.SqlClient.SqlParameter("@FUEL_VISC_Max",fuel_visc_max),
                new System.Data.SqlClient.SqlParameter("@JC_SW_IN_Min",jc_sw_in_min),
                new System.Data.SqlClient.SqlParameter("@JC_SW_IN_Max",jc_sw_in_max),
                new System.Data.SqlClient.SqlParameter("@JC_SW_OUT_Min",jc_sw_out_min),
                new System.Data.SqlClient.SqlParameter("@JC_SW_OUT_Max",jc_sw_out_max),
                new System.Data.SqlClient.SqlParameter("@JC_FW_IN_min",jc_fw_in_min),
                new System.Data.SqlClient.SqlParameter("@JC_FW_IN_Max",jc_fw_in_max),
                new System.Data.SqlClient.SqlParameter("@JC_FW_OUT_Min",jc_fw_out_min),
                new System.Data.SqlClient.SqlParameter("@JC_FW_OUT_Max",jc_fw_out_max),
                new System.Data.SqlClient.SqlParameter("@LC_SW_IN_Min",lc_sw_in_min),
                new System.Data.SqlClient.SqlParameter("@LC_SW_IN_Max",lc_sw_in_max),
                new System.Data.SqlClient.SqlParameter("@LC_SW_OUT_Min",lc_sw_out_min),
                new System.Data.SqlClient.SqlParameter("@LC_SW_OUT_Max",lc_sw_out_max),
                new System.Data.SqlClient.SqlParameter("@LC_LO_IN_min",lc_lo_in_min),
                new System.Data.SqlClient.SqlParameter("@LC_LO_IN_Max",lc_lo_in_max),
                new System.Data.SqlClient.SqlParameter("@LC_LO_OUT_Min",lc_lo_out_min),
                new System.Data.SqlClient.SqlParameter("@LC_LO_OUT_Max",lc_lo_out_max),
                new System.Data.SqlClient.SqlParameter("@PC_SW_IN_Min",pc_sw_in_min),
                new System.Data.SqlClient.SqlParameter("@PC_SW_IN_Max",pc_sw_in_max),
                new System.Data.SqlClient.SqlParameter("@PC_SW_OUT_min",pc_sw_out_min),
                new System.Data.SqlClient.SqlParameter("@PC_SW_OUT_Max",pc_sw_out_max),
                new System.Data.SqlClient.SqlParameter("@PC_LO_IN_min",pc_lo_in_min),
                new System.Data.SqlClient.SqlParameter("@PC_LO_IN_Max",pc_lo_in_max),
                new System.Data.SqlClient.SqlParameter("@PC_LO_OUT_min",pc_lo_out_min),
                new System.Data.SqlClient.SqlParameter("@PC_LO_OUT_Max",pc_lo_out_max),
                new System.Data.SqlClient.SqlParameter("@P_SEA_WATER_min",p_sea_water_min),
                new System.Data.SqlClient.SqlParameter("@P_SEA_WATER_Max",p_sea_water_max),
                new System.Data.SqlClient.SqlParameter("@P_JACKET_WATER_min",p_jacket_water_min),
                new System.Data.SqlClient.SqlParameter("@P_JACKET_WATER_Max",p_jacket_water_max),
                new System.Data.SqlClient.SqlParameter("@P_BEARING_XND_LO_Min",p_bearing_xnd_lo_min),
                new System.Data.SqlClient.SqlParameter("@P_BEARING_XND_LO_Max",p_bearing_xnd_lo_max),
                new System.Data.SqlClient.SqlParameter("@P_CAMSHAFT_LO_Min",p_camshaft_lo_min),
                new System.Data.SqlClient.SqlParameter("@P_CAMSHAFT_LO_Max",p_camshaft_lo_max),
                new System.Data.SqlClient.SqlParameter("@P_FV_COOLING_Min",p_fv_cooling_min),
                new System.Data.SqlClient.SqlParameter("@P_FV_COOLING_Max",p_fv_cooling_max),
                new System.Data.SqlClient.SqlParameter("@P_FUEL_OIL_min",p_fuel_oil_min),
                new System.Data.SqlClient.SqlParameter("@P_FUEL_OIL_Max",p_fuel_oil_max),
                new System.Data.SqlClient.SqlParameter("@P_PISTON_COOLING_Min",p_piston_cooling_min),
                new System.Data.SqlClient.SqlParameter("@P_PISTON_COOLING_Max",p_piston_cooling_max),
                new System.Data.SqlClient.SqlParameter("@P_CONTROL_AIR_min",p_control_air_min),
                new System.Data.SqlClient.SqlParameter("@P_CONTROL_AIR_Max",p_control_air_max),
                new System.Data.SqlClient.SqlParameter("@P_SERVICE_AIR_min",p_service_air_min),
                new System.Data.SqlClient.SqlParameter("@P_SERVICE_AIR_Max",p_service_air_max),          
                new System.Data.SqlClient.SqlParameter("@MODIFIED_BY",modified_by),
            };

            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_UPDATE_ERLOGME_02_THRESHOLD", obj);

        }
        public static int ErLog_GENERATOR_ENGINES_THRESHOLD_Update(int? id, int? vessel_id, int? generator_no, int? min_running_hr,
            decimal? min_temp_exh_max, decimal? min_temp_exh_min, decimal? min_temp_cw_in, decimal? min_temp_cw_out, decimal? min_temp_lo_in, decimal? min_temp_lo_out, decimal? min_temp_boostair,
            decimal? min_temp_pdl_bearing, decimal? min_temp_fuel_in, decimal? min_temp_ae_sw_in, decimal? min_temp_ae_sw_out, decimal? min_temp_ae_lo_in, decimal? min_temp_ae_lo_out,
            decimal? min_press_lo, decimal? min_press_cw, decimal? min_press_boost_air, decimal? min_press_fuel_oil, decimal? min_amps, decimal? min_kw, decimal? max_running_hr, decimal? max_temp_exh_max,
            decimal? max_temp_exh_min, decimal? max_temp_cw_in, decimal? max_temp_cw_out, decimal? max_temp_lo_in, decimal? max_temp_lo_out, decimal? max_temp_boostair, decimal? max_temp_pdl_bearing,
            decimal? max_temp_fuel_in, decimal? max_temp_ae_sw_in, decimal? max_temp_ae_sw_out, decimal? max_temp_ae_lo_in, decimal? max_temp_ae_lo_out, decimal? max_press_lo, decimal? max_press_cw,
            decimal? max_press_boost_air, decimal? max_press_fuel_oil, decimal? max_amps, decimal? max_kw, int modified_by)
        {


            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] {new System.Data.SqlClient.SqlParameter("@ID",id),                   
                    new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),                  
                    new System.Data.SqlClient.SqlParameter("@GENERATOR_NO",generator_no),
                    new System.Data.SqlClient.SqlParameter("@MIN_RUNNING_HR",min_running_hr),
                    new System.Data.SqlClient.SqlParameter("@MIN_TEMP_EXH_MAX",min_temp_exh_max),
                    new System.Data.SqlClient.SqlParameter("@MIN_TEMP_EXH_MIN",min_temp_exh_min),
                    new System.Data.SqlClient.SqlParameter("@MIN_TEMP_CW_IN",min_temp_cw_in),
                    new System.Data.SqlClient.SqlParameter("@MIN_TEMP_CW_OUT",min_temp_cw_out),
                    new System.Data.SqlClient.SqlParameter("@MIN_TEMP_LO_IN",min_temp_lo_in),
                    new System.Data.SqlClient.SqlParameter("@MIN_TEMP_LO_OUT",min_temp_lo_out),
                    new System.Data.SqlClient.SqlParameter("@MIN_TEMP_BOOSTAIR",min_temp_boostair),
                    new System.Data.SqlClient.SqlParameter("@MIN_TEMP_PDL_BEARING",min_temp_pdl_bearing),
                    new System.Data.SqlClient.SqlParameter("@MIN_TEMP_FUEL_IN",min_temp_fuel_in),
                    new System.Data.SqlClient.SqlParameter("@MIN_TEMP_AE_SW_IN",min_temp_ae_sw_in),
                    new System.Data.SqlClient.SqlParameter("@MIN_TEMP_AE_SW_OUT",min_temp_ae_sw_out),
                    new System.Data.SqlClient.SqlParameter("@MIN_TEMP_AE_LO_IN",min_temp_ae_lo_in),
                    new System.Data.SqlClient.SqlParameter("@MIN_TEMP_AE_LO_OUT",min_temp_ae_lo_out),
                    new System.Data.SqlClient.SqlParameter("@MIN_PRESS_LO",min_press_lo),
                    new System.Data.SqlClient.SqlParameter("@MIN_PRESS_CW",min_press_cw),
                    new System.Data.SqlClient.SqlParameter("@MIN_PRESS_BOOST_AIR",min_press_boost_air),
                    new System.Data.SqlClient.SqlParameter("@MIN_PRESS_FUEL_OIL",min_press_fuel_oil),
                    new System.Data.SqlClient.SqlParameter("@MIN_AMPS",min_amps),
                    new System.Data.SqlClient.SqlParameter("@MIN_KW",min_kw),
                    new System.Data.SqlClient.SqlParameter("@MAX_RUNNING_HR",max_running_hr),
                    new System.Data.SqlClient.SqlParameter("@MAX_TEMP_EXH_MAX",max_temp_exh_max),
                    new System.Data.SqlClient.SqlParameter("@MAX_TEMP_EXH_MIN",max_temp_exh_min),
                    new System.Data.SqlClient.SqlParameter("@MAX_TEMP_CW_IN",max_temp_cw_in),
                    new System.Data.SqlClient.SqlParameter("@MAX_TEMP_CW_OUT",max_temp_cw_out),
                    new System.Data.SqlClient.SqlParameter("@MAX_TEMP_LO_IN",max_temp_lo_in),
                    new System.Data.SqlClient.SqlParameter("@MAX_TEMP_LO_OUT",max_temp_lo_out),
                    new System.Data.SqlClient.SqlParameter("@MAX_TEMP_BOOSTAIR",max_temp_boostair),
                    new System.Data.SqlClient.SqlParameter("@MAX_TEMP_PDL_BEARING",max_temp_pdl_bearing),
                    new System.Data.SqlClient.SqlParameter("@MAX_TEMP_FUEL_IN",max_temp_fuel_in),
                    new System.Data.SqlClient.SqlParameter("@MAX_TEMP_AE_SW_IN",max_temp_ae_sw_in),
                    new System.Data.SqlClient.SqlParameter("@MAX_TEMP_AE_SW_OUT",max_temp_ae_sw_out),
                    new System.Data.SqlClient.SqlParameter("@MAX_TEMP_AE_LO_IN",max_temp_ae_lo_in),
                    new System.Data.SqlClient.SqlParameter("@MAX_TEMP_AE_LO_OUT",max_temp_ae_lo_out),
                    new System.Data.SqlClient.SqlParameter("@MAX_PRESS_LO",max_press_lo),
                    new System.Data.SqlClient.SqlParameter("@MAX_PRESS_CW",max_press_cw),
                    new System.Data.SqlClient.SqlParameter("@MAX_PRESS_BOOST_AIR",max_press_boost_air),
                    new System.Data.SqlClient.SqlParameter("@MAX_PRESS_FUEL_OIL",max_press_fuel_oil),
                    new System.Data.SqlClient.SqlParameter("@MAX_AMPS",max_amps),
                    new System.Data.SqlClient.SqlParameter("@MAX_KW",max_kw),                  
                    new System.Data.SqlClient.SqlParameter("@MODIFIED_BY",modified_by)
                   };

            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_UPDATE_GTRENGINE_THRESHOLD", obj);

        }
        public static int ErLog_AC_FW_MISC_THRESHOLD_Update(int? id, int? vessel_id, decimal? ac_p_suct_press_min, decimal? ac_p_disch_press_min, decimal? ac_p_lo_press_min,
           decimal? ac_p_cw_press_min, decimal? ac_s_suct_press_min, decimal? ac_s_disch_press_min, decimal? ac_s_lo_press_min, decimal? ac_s_cw_press_min, decimal? ac_p_in_air_temp_min,
           decimal? ac_p_out_air_temp_min, decimal? ac_s_in_air_temp_min, decimal? ac_s_out_air_temp_min, decimal? ref_comp_no_min, decimal? ref_suct_press_min, decimal? ref_disch_press_min,
           decimal? ref_lo_press_min, decimal? ref_meat_temp_min, decimal? ref_fish_temp_min, decimal? ref_veg_lobby_temp_min, decimal? fwgen_rh_min, decimal? fwgen_fw_in_min, decimal? fwgen_fw_out_min,
           decimal? fwgen_sw_in_min, decimal? fwgen_sw_out_min, decimal? fwgen_vaccum_min, decimal? fwgen_shell_temp_min, decimal? fwgen_salinity_ppm_min, decimal? fwgen_flowmeter_min,
           decimal? blr_oil_firing_hrs_min, decimal? blr_steam_press_min, decimal? blr_feed_wtr_temp_min, decimal? blr_ege_soot_blown_min, decimal? pur_ho_rh_min, decimal? pur_ho_temp_min,
           decimal? pur_lo_rh_min, decimal? pur_lo_temp_min, decimal? pur_do_temp_min, decimal? misc_sft_grounding_min, decimal? misc_thrust_brg_temp_min, decimal? misc_interm_brg_temp_min,
           decimal? misc_stern_tube_oil_temp_min, decimal? misc_sea_wtr_temp_min, decimal? misc_er_temp_min, decimal? misc_ho_sett_1_min, decimal? misc_ho_sett_2_min, decimal? misc_ho_serv_1_min,
           decimal? misc_ho_serv_2_min, decimal? misc_incinerator_rh_min, decimal? ac_p_suct_press_max, decimal? ac_p_disch_press_max, decimal? ac_p_lo_press_max, decimal? ac_p_cw_press_max,
           decimal? ac_s_suct_press_max, decimal? ac_s_disch_press_max, decimal? ac_s_lo_press_max, decimal? ac_s_cw_press_max, decimal? ac_p_in_air_temp_max, decimal? ac_p_out_air_temp_max,
           decimal? ac_s_in_air_temp_max, decimal? ac_s_out_air_temp_max, decimal? ref_comp_no_max_max, decimal? ref_suct_press_max, decimal? ref_disch_press_max, decimal? ref_lo_press_max,
           decimal? ref_meat_temp_max, decimal? ref_fish_temp_max, decimal? ref_veg_lobby_temp_max, decimal? fwgen_rh_max, decimal? fwgen_fw_in_max, decimal? fwgen_fw_out_max, decimal? fwgen_sw_in_max,
           decimal? fwgen_sw_out_max, decimal? fwgen_vaccum_max, decimal? fwgen_shell_temp_max, decimal? fwgen_salinity_ppm_max, decimal? fwgen_flowmeter_max, decimal? blr_oil_firing_hrs_max,
           decimal? blr_steam_press_max, decimal? blr_feed_wtr_temp_max, decimal? blr_ege_soot_blown_max, decimal? pur_ho_rh_max, decimal? pur_ho_temp_max, decimal? pur_lo_rh_max, decimal? pur_lo_temp_max,
           decimal? pur_do_temp_max, decimal? misc_sft_grounding_max, decimal? misc_thrust_brg_temp_max, decimal? misc_interm_brg_temp_max, decimal? misc_stern_tube_oil_temp_max,
           decimal? misc_sea_wtr_temp_max, decimal? misc_er_temp_max, decimal? misc_ho_sett_1_max, decimal? misc_ho_sett_2_max, decimal? misc_ho_serv_1_max, decimal? misc_ho_serv_2_max,
           decimal? misc_incinerator_rh_max, int modified_by)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] {new System.Data.SqlClient.SqlParameter("@ID",id),
                new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                new System.Data.SqlClient.SqlParameter("@AC_P_SUCT_PRESS_Min",ac_p_suct_press_min),
                new System.Data.SqlClient.SqlParameter("@AC_P_DISCH_PRESS_Min",ac_p_disch_press_min),
                new System.Data.SqlClient.SqlParameter("@AC_P_LO_PRESS_Min",ac_p_lo_press_min),
                new System.Data.SqlClient.SqlParameter("@AC_P_CW_PRESS_Min",ac_p_cw_press_min),
                new System.Data.SqlClient.SqlParameter("@AC_S_SUCT_PRESS_Min",ac_s_suct_press_min),
                new System.Data.SqlClient.SqlParameter("@AC_S_DISCH_PRESS_Min",ac_s_disch_press_min),
                new System.Data.SqlClient.SqlParameter("@AC_S_LO_PRESS_Min",ac_s_lo_press_min),
                new System.Data.SqlClient.SqlParameter("@AC_S_CW_PRESS_Min",ac_s_cw_press_min),
                new System.Data.SqlClient.SqlParameter("@AC_P_IN_AIR_TEMP_Min",ac_p_in_air_temp_min),
                new System.Data.SqlClient.SqlParameter("@AC_P_OUT_AIR_TEMP_Min",ac_p_out_air_temp_min),
                new System.Data.SqlClient.SqlParameter("@AC_S_IN_AIR_TEMP_Min",ac_s_in_air_temp_min),
                new System.Data.SqlClient.SqlParameter("@AC_S_OUT_AIR_TEMP_Min",ac_s_out_air_temp_min),
                new System.Data.SqlClient.SqlParameter("@REF_COMP_NO_Min",ref_comp_no_min),
                new System.Data.SqlClient.SqlParameter("@REF_SUCT_PRESS_Min",ref_suct_press_min),
                new System.Data.SqlClient.SqlParameter("@REF_DISCH_PRESS_Min",ref_disch_press_min),
                new System.Data.SqlClient.SqlParameter("@REF_LO_PRESS_Min",ref_lo_press_min),
                new System.Data.SqlClient.SqlParameter("@REF_MEAT_TEMP_Min",ref_meat_temp_min),
                new System.Data.SqlClient.SqlParameter("@REF_FISH_TEMP_Min",ref_fish_temp_min),
                new System.Data.SqlClient.SqlParameter("@REF_VEG_LOBBY_TEMP_Min",ref_veg_lobby_temp_min),
                new System.Data.SqlClient.SqlParameter("@FWGEN_RH_Min",fwgen_rh_min),
                new System.Data.SqlClient.SqlParameter("@FWGEN_FW_IN_Min",fwgen_fw_in_min),
                new System.Data.SqlClient.SqlParameter("@FWGEN_FW_OUT_Min",fwgen_fw_out_min),
                new System.Data.SqlClient.SqlParameter("@FWGEN_SW_IN_Min",fwgen_sw_in_min),
                new System.Data.SqlClient.SqlParameter("@FWGEN_SW_OUT_Min",fwgen_sw_out_min),
                new System.Data.SqlClient.SqlParameter("@FWGEN_VACCUM_Min",fwgen_vaccum_min),
                new System.Data.SqlClient.SqlParameter("@FWGEN_SHELL_TEMP_Min",fwgen_shell_temp_min),
                new System.Data.SqlClient.SqlParameter("@FWGEN_SALINITY_PPM_Min",fwgen_salinity_ppm_min),
                new System.Data.SqlClient.SqlParameter("@FWGEN_FLOWMETER_Min",fwgen_flowmeter_min),
                new System.Data.SqlClient.SqlParameter("@BLR_OIL_FIRING_HRS_Min",blr_oil_firing_hrs_min),
                new System.Data.SqlClient.SqlParameter("@BLR_STEAM_PRESS_Min",blr_steam_press_min),
                new System.Data.SqlClient.SqlParameter("@BLR_FEED_WTR_TEMP_Min",blr_feed_wtr_temp_min),
                new System.Data.SqlClient.SqlParameter("@BLR_EGE_SOOT_BLOWN_Min",blr_ege_soot_blown_min),
                new System.Data.SqlClient.SqlParameter("@PUR_HO_RH_Min",pur_ho_rh_min),
                new System.Data.SqlClient.SqlParameter("@PUR_HO_TEMP_Min",pur_ho_temp_min),
                new System.Data.SqlClient.SqlParameter("@PUR_LO_RH_Min",pur_lo_rh_min),
                new System.Data.SqlClient.SqlParameter("@PUR_LO_TEMP_Min",pur_lo_temp_min),
                new System.Data.SqlClient.SqlParameter("@PUR_DO_TEMP_Min",pur_do_temp_min),
                new System.Data.SqlClient.SqlParameter("@MISC_SFT_GROUNDING_Min",misc_sft_grounding_min),
                new System.Data.SqlClient.SqlParameter("@MISC_THRUST_BRG_TEMP_Min",misc_thrust_brg_temp_min),
                new System.Data.SqlClient.SqlParameter("@MISC_INTERM_BRG_TEMP_Min",misc_interm_brg_temp_min),
                new System.Data.SqlClient.SqlParameter("@MISC_STERN_TUBE_OIL_TEMP_Min",misc_stern_tube_oil_temp_min),
                new System.Data.SqlClient.SqlParameter("@MISC_SEA_WTR_TEMP_Min",misc_sea_wtr_temp_min),
                new System.Data.SqlClient.SqlParameter("@MISC_ER_TEMP_Min",misc_er_temp_min),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SETT_1_Min",misc_ho_sett_1_min),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SETT_2_Min",misc_ho_sett_2_min),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SERV_1_Min",misc_ho_serv_1_min),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SERV_2_Min",misc_ho_serv_2_min),
                new System.Data.SqlClient.SqlParameter("@MISC_INCINERATOR_RH_Min",misc_incinerator_rh_min),
                new System.Data.SqlClient.SqlParameter("@AC_P_SUCT_PRESS_Max",ac_p_suct_press_max),
                new System.Data.SqlClient.SqlParameter("@AC_P_DISCH_PRESS_Max",ac_p_disch_press_max),
                new System.Data.SqlClient.SqlParameter("@AC_P_LO_PRESS_Max",ac_p_lo_press_max),
                new System.Data.SqlClient.SqlParameter("@AC_P_CW_PRESS_Max",ac_p_cw_press_max),
                new System.Data.SqlClient.SqlParameter("@AC_S_SUCT_PRESS_Max",ac_s_suct_press_max),
                new System.Data.SqlClient.SqlParameter("@AC_S_DISCH_PRESS_Max",ac_s_disch_press_max),
                new System.Data.SqlClient.SqlParameter("@AC_S_LO_PRESS_Max",ac_s_lo_press_max),
                new System.Data.SqlClient.SqlParameter("@AC_S_CW_PRESS_Max",ac_s_cw_press_max),
                new System.Data.SqlClient.SqlParameter("@AC_P_IN_AIR_TEMP_Max",ac_p_in_air_temp_max),
                new System.Data.SqlClient.SqlParameter("@AC_P_OUT_AIR_TEMP_Max",ac_p_out_air_temp_max),
                new System.Data.SqlClient.SqlParameter("@AC_S_IN_AIR_TEMP_Max",ac_s_in_air_temp_max),
                new System.Data.SqlClient.SqlParameter("@AC_S_OUT_AIR_TEMP_Max",ac_s_out_air_temp_max),
                new System.Data.SqlClient.SqlParameter("@REF_COMP_NO_Max_Max",ref_comp_no_max_max),
                new System.Data.SqlClient.SqlParameter("@REF_SUCT_PRESS_Max",ref_suct_press_max),
                new System.Data.SqlClient.SqlParameter("@REF_DISCH_PRESS_Max",ref_disch_press_max),
                new System.Data.SqlClient.SqlParameter("@REF_LO_PRESS_Max",ref_lo_press_max),
                new System.Data.SqlClient.SqlParameter("@REF_MEAT_TEMP_Max",ref_meat_temp_max),
                new System.Data.SqlClient.SqlParameter("@REF_FISH_TEMP_Max",ref_fish_temp_max),
                new System.Data.SqlClient.SqlParameter("@REF_VEG_LOBBY_TEMP_Max",ref_veg_lobby_temp_max),
                new System.Data.SqlClient.SqlParameter("@FWGEN_RH_Max",fwgen_rh_max),
                new System.Data.SqlClient.SqlParameter("@FWGEN_FW_IN_Max",fwgen_fw_in_max),
                new System.Data.SqlClient.SqlParameter("@FWGEN_FW_OUT_Max",fwgen_fw_out_max),
                new System.Data.SqlClient.SqlParameter("@FWGEN_SW_IN_Max",fwgen_sw_in_max),
                new System.Data.SqlClient.SqlParameter("@FWGEN_SW_OUT_Max",fwgen_sw_out_max),
                new System.Data.SqlClient.SqlParameter("@FWGEN_VACCUM_Max",fwgen_vaccum_max),
                new System.Data.SqlClient.SqlParameter("@FWGEN_SHELL_TEMP_Max",fwgen_shell_temp_max),
                new System.Data.SqlClient.SqlParameter("@FWGEN_SALINITY_PPM_Max",fwgen_salinity_ppm_max),
                new System.Data.SqlClient.SqlParameter("@FWGEN_FLOWMETER_Max",fwgen_flowmeter_max),
                new System.Data.SqlClient.SqlParameter("@BLR_OIL_FIRING_HRS_Max",blr_oil_firing_hrs_max),
                new System.Data.SqlClient.SqlParameter("@BLR_STEAM_PRESS_Max",blr_steam_press_max),
                new System.Data.SqlClient.SqlParameter("@BLR_FEED_WTR_TEMP_Max",blr_feed_wtr_temp_max),
                new System.Data.SqlClient.SqlParameter("@BLR_EGE_SOOT_BLOWN_Max",blr_ege_soot_blown_max),
                new System.Data.SqlClient.SqlParameter("@PUR_HO_RH_Max",pur_ho_rh_max),
                new System.Data.SqlClient.SqlParameter("@PUR_HO_TEMP_Max",pur_ho_temp_max),
                new System.Data.SqlClient.SqlParameter("@PUR_LO_RH_Max",pur_lo_rh_max),
                new System.Data.SqlClient.SqlParameter("@PUR_LO_TEMP_Max",pur_lo_temp_max),
                new System.Data.SqlClient.SqlParameter("@PUR_DO_TEMP_Max",pur_do_temp_max),
                new System.Data.SqlClient.SqlParameter("@MISC_SFT_GROUNDING_Max",misc_sft_grounding_max),
                new System.Data.SqlClient.SqlParameter("@MISC_THRUST_BRG_TEMP_Max",misc_thrust_brg_temp_max),
                new System.Data.SqlClient.SqlParameter("@MISC_INTERM_BRG_TEMP_Max",misc_interm_brg_temp_max),
                new System.Data.SqlClient.SqlParameter("@MISC_STERN_TUBE_OIL_TEMP_Max",misc_stern_tube_oil_temp_max),
                new System.Data.SqlClient.SqlParameter("@MISC_SEA_WTR_TEMP_Max",misc_sea_wtr_temp_max),
                new System.Data.SqlClient.SqlParameter("@MISC_ER_TEMP_Max",misc_er_temp_max),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SETT_1_Max",misc_ho_sett_1_max),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SETT_2_Max",misc_ho_sett_2_max),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SERV_1_Max",misc_ho_serv_1_max),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SERV_2_Max",misc_ho_serv_2_max),
                new System.Data.SqlClient.SqlParameter("@MISC_INCINERATOR_RH_Max",misc_incinerator_rh_max),
                new System.Data.SqlClient.SqlParameter("@MODIFIED_BY",modified_by) };

            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_UPDATE_ACFWMISC_THRESHOLD", obj);

        }

        public static int ErLog_TANK_LEVELS_THRESHOLD_Update(int? id, int? vessel_id, decimal? min_cyl_oil_day_tk, decimal? min_me_sump, decimal? min_heavy_oil_settl_tk, decimal? min_heavy_oil_serv_tk,
          decimal? min_belended_oil, decimal? min_do_serv_tk, decimal? max_cyl_oil_day_tk, decimal? max_me_sump, decimal? max_heavy_oil_settl_tk, decimal? max_heavy_oil_serv_tk, decimal? max_belended_oil,
          decimal? max_do_serv_tk, int modified_by)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] {new System.Data.SqlClient.SqlParameter("@ID",id),
                    new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                    new System.Data.SqlClient.SqlParameter("@MIN_CYL_OIL_DAY_TK",min_cyl_oil_day_tk),
                    new System.Data.SqlClient.SqlParameter("@Min_ME_SUMP",min_me_sump),
                    new System.Data.SqlClient.SqlParameter("@MIN_HEAVY_OIL_SETTL_TK",min_heavy_oil_settl_tk),
                    new System.Data.SqlClient.SqlParameter("@MIN_HEAVY_OIL_SERV_TK",min_heavy_oil_serv_tk),
                    new System.Data.SqlClient.SqlParameter("@MIN_BELENDED_OIL",min_belended_oil),
                    new System.Data.SqlClient.SqlParameter("@MIN_DO_SERV_TK",min_do_serv_tk),
                    new System.Data.SqlClient.SqlParameter("@MAX_CYL_OIL_DAY_TK",max_cyl_oil_day_tk),
                    new System.Data.SqlClient.SqlParameter("@MAX_ME_SUMP",max_me_sump),
                    new System.Data.SqlClient.SqlParameter("@MAX_HEAVY_OIL_SETTL_TK",max_heavy_oil_settl_tk),
                    new System.Data.SqlClient.SqlParameter("@MAX_HEAVY_OIL_SERV_TK",max_heavy_oil_serv_tk),
                    new System.Data.SqlClient.SqlParameter("@MAX_BELENDED_OIL",max_belended_oil),
                    new System.Data.SqlClient.SqlParameter("@MAX_DO_SERV_TK",max_do_serv_tk),          
                    new System.Data.SqlClient.SqlParameter("@MODIFIED_BY",modified_by) };

            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_UPDATE_TANKLEVEL_THRESHOLD", obj);

        }
        public static int ErLog_ERLOG_TASG_THRESHOLD_Update(int? id, int? vessel_id, decimal? min_ta_running_hr, decimal? min_ta_steam_press, decimal? min_ta_cond_vac, decimal? min_ta_gland_steam,
           decimal? min_ta_lo_press, decimal? min_ta_lo_temp, decimal? min_ta_thust_big, decimal? min_ta_free_end, decimal? min_ta_kw, decimal? min_ta_amps, decimal? min_sg_running_hrg, decimal? min_sg_clutch_air_pr,
           decimal? min_sg_lo_press, decimal? min_sg_lo_temp, decimal? min_sg_sg_cond_brg_temp1, decimal? min_sg_sg_cond_brg_temp2, decimal? min_sg_kw, decimal? min_sg_amps, decimal? max_ta_running_hr,
           decimal? max_ta_steam_press, decimal? max_ta_cond_vac, decimal? max_ta_gland_steam, decimal? max_ta_lo_press, decimal? max_ta_lo_temp, decimal? max_ta_thust_big, decimal? max_ta_free_end, decimal? max_ta_kw,
           decimal? max_ta_amps, decimal? max_sg_running_hrg, decimal? max_sg_clutch_air_pr, decimal? max_sg_lo_press, decimal? max_sg_lo_temp, decimal? max_sg_sg_cond_brg_temp1, decimal? max_sg_sg_cond_brg_temp2,
           decimal? max_sg_kw, decimal? max_sg_amps, int modified_by)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@ID",id),
            new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
            new System.Data.SqlClient.SqlParameter("@MIN_TA_RUNNING_HR",min_ta_running_hr),
            new System.Data.SqlClient.SqlParameter("@MIN_TA_STEAM_PRESS",min_ta_steam_press),
            new System.Data.SqlClient.SqlParameter("@MIN_TA_COND_VAC",min_ta_cond_vac),
            new System.Data.SqlClient.SqlParameter("@MIN_TA_GLAND_STEAM",min_ta_gland_steam),
            new System.Data.SqlClient.SqlParameter("@MIN_TA_LO_PRESS",min_ta_lo_press),
            new System.Data.SqlClient.SqlParameter("@MIN_TA_LO_TEMP",min_ta_lo_temp),
            new System.Data.SqlClient.SqlParameter("@MIN_TA_THUST_BIG",min_ta_thust_big),
            new System.Data.SqlClient.SqlParameter("@MIN_TA_FREE_END",min_ta_free_end),
            new System.Data.SqlClient.SqlParameter("@MIN_TA_KW",min_ta_kw),
            new System.Data.SqlClient.SqlParameter("@MIN_TA_AMPS",min_ta_amps),
            new System.Data.SqlClient.SqlParameter("@MIN_SG_RUNNING_HRG",min_sg_running_hrg),
            new System.Data.SqlClient.SqlParameter("@MIN_SG_CLUTCH_AIR_PR",min_sg_clutch_air_pr),
            new System.Data.SqlClient.SqlParameter("@MIN_SG_LO_PRESS",min_sg_lo_press),
            new System.Data.SqlClient.SqlParameter("@MIN_SG_LO_TEMP",min_sg_lo_temp),
            new System.Data.SqlClient.SqlParameter("@MIN_SG_SG_COND_BRG_TEMP1",min_sg_sg_cond_brg_temp1),
            new System.Data.SqlClient.SqlParameter("@MIN_SG_SG_COND_BRG_TEMP2",min_sg_sg_cond_brg_temp2),
            new System.Data.SqlClient.SqlParameter("@MIN_SG_KW",min_sg_kw),
            new System.Data.SqlClient.SqlParameter("@MIN_SG_AMPS",min_sg_amps),
            new System.Data.SqlClient.SqlParameter("@MAX_TA_RUNNING_HR",max_ta_running_hr),
            new System.Data.SqlClient.SqlParameter("@MAX_TA_STEAM_PRESS",max_ta_steam_press),
            new System.Data.SqlClient.SqlParameter("@MAX_TA_COND_VAC",max_ta_cond_vac),
            new System.Data.SqlClient.SqlParameter("@MAX_TA_GLAND_STEAM",max_ta_gland_steam),
            new System.Data.SqlClient.SqlParameter("@MAX_TA_LO_PRESS",max_ta_lo_press),
            new System.Data.SqlClient.SqlParameter("@MAX_TA_LO_TEMP",max_ta_lo_temp),
            new System.Data.SqlClient.SqlParameter("@MAX_TA_THUST_BIG",max_ta_thust_big),
            new System.Data.SqlClient.SqlParameter("@MAX_TA_FREE_END",max_ta_free_end),
            new System.Data.SqlClient.SqlParameter("@MAX_TA_KW",max_ta_kw),
            new System.Data.SqlClient.SqlParameter("@MAX_TA_AMPS",max_ta_amps),
            new System.Data.SqlClient.SqlParameter("@MAX_SG_RUNNING_HRG",max_sg_running_hrg),
            new System.Data.SqlClient.SqlParameter("@MAX_SG_CLUTCH_AIR_PR",max_sg_clutch_air_pr),
            new System.Data.SqlClient.SqlParameter("@MAX_SG_LO_PRESS",max_sg_lo_press),
            new System.Data.SqlClient.SqlParameter("@MAX_SG_LO_TEMP",max_sg_lo_temp),
            new System.Data.SqlClient.SqlParameter("@MAX_SG_SG_COND_BRG_TEMP1",max_sg_sg_cond_brg_temp1),
            new System.Data.SqlClient.SqlParameter("@MAX_SG_SG_COND_BRG_TEMP2",max_sg_sg_cond_brg_temp2),
            new System.Data.SqlClient.SqlParameter("@MAX_SG_KW",max_sg_kw),
            new System.Data.SqlClient.SqlParameter("@MAX_SG_AMPS",max_sg_amps),
            new System.Data.SqlClient.SqlParameter("@MODIFIED_BY",modified_by)
            };

            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_UPDATE_ERLOGTASG_THRESHOLD", obj);

        }

        public static int ErLog_BLR_CW_THRESHOLD_Update(int? id, int? vessel_id, decimal? blr_cw_chlorides_blr_min, decimal? blr_cw_chlorides_blr_max, decimal? blr_cw_chlorides_mejw_min,
           decimal? blr_cw_chlorides_mejw_max, decimal? blr_cw_chlorides_mepw_min, decimal? blr_cw_chlorides_mepw_max, decimal? blr_cw_chlorides_ae_min, decimal? blr_cw_chlorides_ae_max, decimal? blr_cw_chlorides_cmpr_min,
           decimal? blr_cw_chlorides_cmpr_max, decimal? blr_cw_alkalinity_blr_min, decimal? blr_cw_alkalinity_blr_max, decimal? blr_cw_alkalinity_mejw_min, decimal? blr_cw_alkalinity_mejw_max, decimal? blr_cw_alkalinity_mepw_min,
           decimal? blr_cw_alkalinity_mepw_max, decimal? blr_cw_alkalinity_ae_min, decimal? blr_cw_alkalinity_cmpr_min, decimal? blr_cw_talkalinity_blr_min, decimal? blr_cw_talkalinity_mejw_min, decimal? blr_cw_talkalinity_mepw_mn,
           decimal? blr_cw_talkalinity_ae_min, decimal? blr_cw_talkalinity_cmpr_min, decimal? blr_cw_phosphates_blr_min, decimal? blr_cw_phosphates_mejw_min, decimal? blr_cw_phosphates_mepw_min, decimal? blr_cw_phosphates_ae_min,
           decimal? blr_cw_phosphates_cmpr_min, decimal? blr_cw_blowdown_blr_min, decimal? blr_cw_blowdown_mejw_min, decimal? blr_cw_blowdown_mepw_min, decimal? blr_cw_blowdown_ae_min, decimal? blr_cw_blowdown_cmpr_min,
           decimal? blr_cw_nitrites_blr_min, decimal? blr_cw_nitrites_mejw_min, decimal? blr_cw_nitrites_mepw_min, decimal? blr_cw_nitrites_ae_min, decimal? blr_cw_nitrites_cmpr_min, decimal? blr_cw_dosage_blr_min,
           decimal? blr_cw_dosage_mejw_min, decimal? blr_cw_dosage_mepw_min, decimal? blr_cw_dosage_ae_min, decimal? blr_cw_dosage_cmpr_min, decimal? blr_cw_alkalinity_ae_max, decimal? blr_cw_alkalinity_cmpr_max,
           decimal? blr_cw_talkalinity_blr_max, decimal? blr_cw_talkalinity_mejw_max, decimal? blr_cw_talkalinity_mepw_max, decimal? blr_cw_talkalinity_ae_max, decimal? blr_cw_talkalinity_cmpr_max, decimal? blr_cw_phosphates_blr_max,
           decimal? blr_cw_phosphates_mejw_max, decimal? blr_cw_phosphates_mepw_max, decimal? blr_cw_phosphates_ae_max, decimal? blr_cw_phosphates_cmpr_max, decimal? blr_cw_blowdown_blr_max, decimal? blr_cw_blowdown_mejw_max,
           decimal? blr_cw_blowdown_mepw_max, decimal? blr_cw_blowdown_ae_max, decimal? blr_cw_blowdown_cmpr_max, decimal? blr_cw_nitrites_blr_max, decimal? blr_cw_nitrites_mejw_max, decimal? blr_cw_nitrites_mepw_max,
           decimal? blr_cw_nitrites_ae_max, decimal? blr_cw_nitrites_cmpr_max, decimal? blr_cw_dosage_blr_max, decimal? blr_cw_dosage_mejw_max, decimal? blr_cw_dosage_mepw_max, decimal? blr_cw_dosage_ae_max,
           decimal? blr_cw_dosage_cmpr_max, int modified_by)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@ID",id),
            new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_CHLORIDES_BLR_Min",blr_cw_chlorides_blr_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_CHLORIDES_BLR_Max",blr_cw_chlorides_blr_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_CHLORIDES_MEJW_Min",blr_cw_chlorides_mejw_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_CHLORIDES_MEJW_Max",blr_cw_chlorides_mejw_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_CHLORIDES_MEPW_Min",blr_cw_chlorides_mepw_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_CHLORIDES_MEPW_Max",blr_cw_chlorides_mepw_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_CHLORIDES_AE_Min",blr_cw_chlorides_ae_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_CHLORIDES_AE_Max",blr_cw_chlorides_ae_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_CHLORIDES_CMPR_Min",blr_cw_chlorides_cmpr_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_CHLORIDES_CMPR_Max",blr_cw_chlorides_cmpr_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_ALKALINITY_BLR_Min",blr_cw_alkalinity_blr_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_ALKALINITY_BLR_Max",blr_cw_alkalinity_blr_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_ALKALINITY_MEJW_Min",blr_cw_alkalinity_mejw_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_ALKALINITY_MEJW_MAX",blr_cw_alkalinity_mejw_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_ALKALINITY_MEPW_min",blr_cw_alkalinity_mepw_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_ALKALINITY_MEPW_Max",blr_cw_alkalinity_mepw_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_ALKALINITY_AE_min",blr_cw_alkalinity_ae_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_ALKALINITY_CMPR_min",blr_cw_alkalinity_cmpr_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_TALKALINITY_BLR_min",blr_cw_talkalinity_blr_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_TALKALINITY_MEJW_Min",blr_cw_talkalinity_mejw_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_TALKALINITY_MEPW_mn",blr_cw_talkalinity_mepw_mn),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_TALKALINITY_AE_min",blr_cw_talkalinity_ae_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_TALKALINITY_CMPR_min",blr_cw_talkalinity_cmpr_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_PHOSPHATES_BLR_min",blr_cw_phosphates_blr_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_PHOSPHATES_MEJW_min",blr_cw_phosphates_mejw_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_PHOSPHATES_MEPW_min",blr_cw_phosphates_mepw_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_PHOSPHATES_AE_min",blr_cw_phosphates_ae_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_PHOSPHATES_CMPR_min",blr_cw_phosphates_cmpr_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_BLOWDOWN_BLR_Min",blr_cw_blowdown_blr_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_BLOWDOWN_MEJW_Min",blr_cw_blowdown_mejw_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_BLOWDOWN_MEPW_Min",blr_cw_blowdown_mepw_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_BLOWDOWN_AE_Min",blr_cw_blowdown_ae_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_BLOWDOWN_CMPR_Min",blr_cw_blowdown_cmpr_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_NITRITES_BLR_Min",blr_cw_nitrites_blr_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_NITRITES_MEJW_Min",blr_cw_nitrites_mejw_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_NITRITES_MEPW_Min",blr_cw_nitrites_mepw_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_NITRITES_AE_Min",blr_cw_nitrites_ae_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_NITRITES_CMPR_Min",blr_cw_nitrites_cmpr_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_DOSAGE_BLR_Min",blr_cw_dosage_blr_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_DOSAGE_MEJW_Min",blr_cw_dosage_mejw_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_DOSAGE_MEPW_Min",blr_cw_dosage_mepw_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_DOSAGE_AE_Min",blr_cw_dosage_ae_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_DOSAGE_CMPR_Min",blr_cw_dosage_cmpr_min),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_ALKALINITY_AE_MAX",blr_cw_alkalinity_ae_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_ALKALINITY_CMPR_MAX",blr_cw_alkalinity_cmpr_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_TALKALINITY_BLR_MAX",blr_cw_talkalinity_blr_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_TALKALINITY_MEJW_MAX",blr_cw_talkalinity_mejw_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_TALKALINITY_MEPW_MAX",blr_cw_talkalinity_mepw_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_TALKALINITY_AE_MAX",blr_cw_talkalinity_ae_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_TALKALINITY_CMPR_MAX",blr_cw_talkalinity_cmpr_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_PHOSPHATES_BLR_MAX",blr_cw_phosphates_blr_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_PHOSPHATES_MEJW_MAX",blr_cw_phosphates_mejw_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_PHOSPHATES_MEPW_MAX",blr_cw_phosphates_mepw_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_PHOSPHATES_AE_MAX",blr_cw_phosphates_ae_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_PHOSPHATES_CMPR_MAX",blr_cw_phosphates_cmpr_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_BLOWDOWN_BLR_MAX",blr_cw_blowdown_blr_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_BLOWDOWN_MEJW_MAX",blr_cw_blowdown_mejw_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_BLOWDOWN_MEPW_MAX",blr_cw_blowdown_mepw_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_BLOWDOWN_AE_MAX",blr_cw_blowdown_ae_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_BLOWDOWN_CMPR_MAX",blr_cw_blowdown_cmpr_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_NITRITES_BLR_MAX",blr_cw_nitrites_blr_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_NITRITES_MEJW_MAX",blr_cw_nitrites_mejw_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_NITRITES_MEPW_MAX",blr_cw_nitrites_mepw_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_NITRITES_AE_MAX",blr_cw_nitrites_ae_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_NITRITES_CMPR_MAX",blr_cw_nitrites_cmpr_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_DOSAGE_BLR_MAX",blr_cw_dosage_blr_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_DOSAGE_MEJW_MAX",blr_cw_dosage_mejw_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_DOSAGE_MEPW_MAX",blr_cw_dosage_mepw_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_DOSAGE_AE_MAX",blr_cw_dosage_ae_max),
            new System.Data.SqlClient.SqlParameter("@BLR_CW_DOSAGE_CMPR_MAX",blr_cw_dosage_cmpr_max)
            ,new System.Data.SqlClient.SqlParameter("@MODIFIED_BY",modified_by)
            };
            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_UPDATE_ERLOGDETAILS_THRESHOLD_BLR_CW", obj);
        }
        public static int ErLog_WRKHRS_THRESHOLD_Update(int? id, int? vessel_id, decimal? wrkhrs_me_prev_min, decimal? wrkhrs_me_nn_min, decimal? wrkhrs_me_ttl_min, decimal? wrkhrs_ae1_prev_min,
               decimal? wrkhrs_ae1_nn_min, decimal? wrkhrs_ae1_ttl_min, decimal? wrkhrs_ae2_prev_min, decimal? wrkhrs_ae2_nn_min, decimal? wrkhrs_ae2_ttl_min, decimal? wrkhrs_ae3_prev_min, decimal? wrkhrs_ae3_nn_min,
               decimal? wrkhrs_ae3_ttl_min, decimal? wrkhrs_ae4_prev_min, decimal? wrkhrs_ae4_nn_min, decimal? wrkhrs_ae4_ttl_min, decimal? wrkhrs_ta_prev_min, decimal? wrkhrs_ta_nn_min, decimal? wrkhrs_ta_ttl_min,
               decimal? wrkhrs_sg_prev_min, decimal? wrkhrs_sg_nn_min, decimal? wrkhrs_sg_ttl_min, decimal? wrkhrs_me_prev_max, decimal? wrkhrs_me_nn_max, decimal? wrkhrs_me_ttl_max, decimal? wrkhrs_ae1_prev_max,
               decimal? wrkhrs_ae1_nn_max, decimal? wrkhrs_ae1_ttl_max, decimal? wrkhrs_ae2_prev_max, decimal? wrkhrs_ae2_nn_max, decimal? wrkhrs_ae2_ttl_max, decimal? wrkhrs_ae3_prev_max, decimal? wrkhrs_ae3_nn_max,
               decimal? wrkhrs_ae3_ttl_max, decimal? wrkhrs_ae4_prev_max, decimal? wrkhrs_ae4_nn_max, decimal? wrkhrs_ae4_ttl_max, decimal? wrkhrs_ta_prev_max, decimal? wrkhrs_ta_nn_max, decimal? wrkhrs_ta_ttl_max,
               decimal? wrkhrs_sg_prev_max, decimal? wrkhrs_sg_nn_max, decimal? wrkhrs_sg_ttl_max, int modified_by)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] {  new System.Data.SqlClient.SqlParameter("@ID",id),
                new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_ME_PREV_Min",wrkhrs_me_prev_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_ME_NN_Min",wrkhrs_me_nn_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_ME_TTL_Min",wrkhrs_me_ttl_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE1_PREV_Min",wrkhrs_ae1_prev_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE1_NN_Min",wrkhrs_ae1_nn_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE1_TTL_Min",wrkhrs_ae1_ttl_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE2_PREV_Min",wrkhrs_ae2_prev_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE2_NN_Min",wrkhrs_ae2_nn_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE2_TTL_Min",wrkhrs_ae2_ttl_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE3_PREV_Min",wrkhrs_ae3_prev_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE3_NN_Min",wrkhrs_ae3_nn_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE3_TTL_Min",wrkhrs_ae3_ttl_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE4_PREV_Min",wrkhrs_ae4_prev_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE4_NN_Min",wrkhrs_ae4_nn_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE4_TTL_Min",wrkhrs_ae4_ttl_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_TA_PREV_Min",wrkhrs_ta_prev_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_TA_NN_Min",wrkhrs_ta_nn_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_TA_TTL_Min",wrkhrs_ta_ttl_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_SG_PREV_Min",wrkhrs_sg_prev_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_SG_NN_Min",wrkhrs_sg_nn_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_SG_TTL_Min",wrkhrs_sg_ttl_min),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_ME_PREV_Max",wrkhrs_me_prev_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_ME_NN_Max",wrkhrs_me_nn_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_ME_TTL_Max",wrkhrs_me_ttl_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE1_PREV_Max",wrkhrs_ae1_prev_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE1_NN_Max",wrkhrs_ae1_nn_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE1_TTL_Max",wrkhrs_ae1_ttl_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE2_PREV_Max",wrkhrs_ae2_prev_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE2_NN_Max",wrkhrs_ae2_nn_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE2_TTL_Max",wrkhrs_ae2_ttl_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE3_PREV_Max",wrkhrs_ae3_prev_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE3_NN_Max",wrkhrs_ae3_nn_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE3_TTL_Max",wrkhrs_ae3_ttl_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE4_PREV_Max",wrkhrs_ae4_prev_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE4_NN_Max",wrkhrs_ae4_nn_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_AE4_TTL_Max",wrkhrs_ae4_ttl_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_TA_PREV_Max",wrkhrs_ta_prev_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_TA_NN_Max",wrkhrs_ta_nn_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_TA_TTL_Max",wrkhrs_ta_ttl_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_SG_PREV_Max",wrkhrs_sg_prev_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_SG_NN_Max",wrkhrs_sg_nn_max),
                new System.Data.SqlClient.SqlParameter("@WRKHRS_SG_TTL_Max",wrkhrs_sg_ttl_max),
                new System.Data.SqlClient.SqlParameter("@MODIFIED_BY",modified_by) };
            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_UPDATE_ERLOGDETAILS_THRESHOLD_WRKHRS", obj);
        }
        public static int ErLog_DP_THRESHOLD_Update(decimal? id, decimal? vessel_id, decimal? dp_wind_force_min, decimal? dp_wind_dir_min, decimal? dp_sea_cond_min, decimal? dp_swell_min, decimal? dp_swell_dir_min,
            decimal? dp_curr_min, decimal? dp_revs_ntn_min, decimal? dp_avg_rpm_min, decimal? dp_eng_dist_min, decimal? dp_obs_dist_min, decimal? dp_ttl_dist_min, decimal? dp_hrs_ful_spd_min, decimal? dp_slip_min,
            decimal? dp_dist_togo_min, decimal? dp_hrs_red_spd_min, decimal? dp_hrs_stopd_min, decimal? dp_obs_spd_min, decimal? eta_min, decimal? dp_wind_force_max, decimal? dp_wind_dir_max, decimal? dp_sea_cond_max,
            decimal? dp_swell_max, decimal? dp_swell_dir_max, decimal? dp_curr_max, decimal? dp_revs_ntn_max, decimal? dp_avg_rpm_max, decimal? dp_eng_dist_max, decimal? dp_obs_dist_max, decimal? dp_ttl_dist_max,
            decimal? dp_hrs_ful_spd_max, decimal? dp_slip_max, decimal? dp_dist_togo_max, decimal? dp_hrs_red_spd_max, decimal? dp_hrs_stopd_max, decimal? dp_obs_spd_max, decimal? eta_max, int? modified_by)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] {  new System.Data.SqlClient.SqlParameter("@ID",id),
            new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id) ,
            new System.Data.SqlClient.SqlParameter("@DP_WIND_FORCE_MIN",dp_wind_force_min),
            new System.Data.SqlClient.SqlParameter("@DP_WIND_DIR_MIN",dp_wind_dir_min),
            new System.Data.SqlClient.SqlParameter("@DP_SEA_COND_MIN",dp_sea_cond_min),
            new System.Data.SqlClient.SqlParameter("@DP_SWELL_MIN",dp_swell_min),
            new System.Data.SqlClient.SqlParameter("@DP_SWELL_DIR_MIN",dp_swell_dir_min),
            new System.Data.SqlClient.SqlParameter("@DP_CURR_MIN",dp_curr_min),
            new System.Data.SqlClient.SqlParameter("@DP_REVS_NTN_MIN",dp_revs_ntn_min),
            new System.Data.SqlClient.SqlParameter("@DP_AVG_RPM_MIN",dp_avg_rpm_min),
            new System.Data.SqlClient.SqlParameter("@DP_ENG_DIST_MIN",dp_eng_dist_min),
            new System.Data.SqlClient.SqlParameter("@DP_OBS_DIST_MIN",dp_obs_dist_min),
            new System.Data.SqlClient.SqlParameter("@DP_TTL_DIST_MIN",dp_ttl_dist_min),
            new System.Data.SqlClient.SqlParameter("@DP_HRS_FUL_SPD_MIN",dp_hrs_ful_spd_min),
            new System.Data.SqlClient.SqlParameter("@DP_SLIP_MIN",dp_slip_min),
            new System.Data.SqlClient.SqlParameter("@DP_DIST_TOGO_MIN",dp_dist_togo_min),
            new System.Data.SqlClient.SqlParameter("@DP_HRS_RED_SPD_MIN",dp_hrs_red_spd_min),
            new System.Data.SqlClient.SqlParameter("@DP_HRS_STOPD_MIN",dp_hrs_stopd_min),
            new System.Data.SqlClient.SqlParameter("@DP_OBS_SPD_MIN",dp_obs_spd_min),
            new System.Data.SqlClient.SqlParameter("@ETA_MIN",eta_min),
            new System.Data.SqlClient.SqlParameter("@DP_WIND_FORCE_MAX",dp_wind_force_max),
            new System.Data.SqlClient.SqlParameter("@DP_WIND_DIR_MAX",dp_wind_dir_max),
            new System.Data.SqlClient.SqlParameter("@DP_SEA_COND_MAX",dp_sea_cond_max),
            new System.Data.SqlClient.SqlParameter("@DP_SWELL_MAX",dp_swell_max),
            new System.Data.SqlClient.SqlParameter("@DP_SWELL_DIR_MAX",dp_swell_dir_max),
            new System.Data.SqlClient.SqlParameter("@DP_CURR_MAX",dp_curr_max),
            new System.Data.SqlClient.SqlParameter("@DP_REVS_NTN_MAX",dp_revs_ntn_max),
            new System.Data.SqlClient.SqlParameter("@DP_AVG_RPM_MAX",dp_avg_rpm_max),
            new System.Data.SqlClient.SqlParameter("@DP_ENG_DIST_MAX",dp_eng_dist_max),
            new System.Data.SqlClient.SqlParameter("@DP_OBS_DIST_MAX",dp_obs_dist_max),
            new System.Data.SqlClient.SqlParameter("@DP_TTL_DIST_MAX",dp_ttl_dist_max),
            new System.Data.SqlClient.SqlParameter("@DP_HRS_FUL_SPD_MAX",dp_hrs_ful_spd_max),
            new System.Data.SqlClient.SqlParameter("@DP_SLIP_MAX",dp_slip_max),
            new System.Data.SqlClient.SqlParameter("@DP_DIST_TOGO_MAX",dp_dist_togo_max),
            new System.Data.SqlClient.SqlParameter("@DP_HRS_RED_SPD_MAX",dp_hrs_red_spd_max),
            new System.Data.SqlClient.SqlParameter("@DP_HRS_STOPD_MAX",dp_hrs_stopd_max),
            new System.Data.SqlClient.SqlParameter("@DP_OBS_SPD_MAX",dp_obs_spd_max),
            new System.Data.SqlClient.SqlParameter("@ETA_MAX",eta_max),
            new System.Data.SqlClient.SqlParameter("@MODIFIED_BY",modified_by)
      };
            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_UPDATE_ERLOGDETAILS_THRESHOLD_DP", obj);
        }
        public static int ErLog_FODA_THRESHOLD_Update(decimal? id, decimal? vessel_id, decimal? foda_ho_rob_pnn_min, decimal? foda_ho_cons_me_min, decimal? foda_ho_cons_ae_min,
            decimal? foda_ho_cons_blr_min, decimal? foda_ho_cons_tc_min, decimal? foda_ho_cons_htg_min, decimal? foda_ho_cons_ttl_min, decimal? foda_ho_rcvd_min, decimal? foda_ho_amend_min,
            decimal? foda_ho_rob_min, decimal? foda_do_rob_pnn_min, decimal? foda_do_cons_me_min, decimal? foda_do_cons_ae_min, decimal? foda_do_cons_blr_min, decimal? foda_do_cons_tc_min,
            decimal? foda_do_cons_htg_min, decimal? foda_do_cons_ttl_min, decimal? foda_do_rcvd_min, decimal? foda_do_amend_min, decimal? foda_do_rob_min, decimal? foda_go_rob_pnn_min,
            decimal? foda_go_cons_me_min, decimal? foda_go_cons_ae_min, decimal? foda_go_cons_blr_min, decimal? foda_go_cons_tc_min, decimal? foda_go_cons_htg_min, decimal? foda_go_cons_ttl_min,
            decimal? foda_go_rcvd_min, decimal? foda_go_amend_min, decimal? foda_go_rob_min, decimal? foda_ho_rob_pnn_max, decimal? foda_ho_cons_me_max, decimal? foda_ho_cons_ae_max, decimal? foda_ho_cons_blr_max,
            decimal? foda_ho_cons_tc_max, decimal? foda_ho_cons_htg_max, decimal? foda_ho_cons_ttl_max, decimal? foda_ho_rcvd_max, decimal? foda_ho_amend_max, decimal? foda_ho_rob_max, decimal? foda_do_rob_pnn_max,
            decimal? foda_do_cons_me_max, decimal? foda_do_cons_ae_max, decimal? foda_do_cons_blr_max, decimal? foda_do_cons_tc_max, decimal? foda_do_cons_htg_max, decimal? foda_do_cons_ttl_max,
            decimal? foda_do_rcvd_max, decimal? foda_do_amend_max, decimal? foda_do_rob_max, decimal? foda_go_rob_pnn_max, decimal? foda_go_cons_me_max, decimal? foda_go_cons_ae_max, decimal? foda_go_cons_blr_max,
            decimal? foda_go_cons_tc_max, decimal? foda_go_cons_htg_max, decimal? foda_go_cons_ttl_max, decimal? foda_go_rcvd_max, decimal? foda_go_amend_max, decimal? foda_go_rob_max, decimal? modified_by)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@ID",id),
            new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_ROB_PNN_Min",foda_ho_rob_pnn_min),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_CONS_ME_Min",foda_ho_cons_me_min),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_CONS_AE_Min",foda_ho_cons_ae_min),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_CONS_BLR_Min",foda_ho_cons_blr_min),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_CONS_TC_Min",foda_ho_cons_tc_min),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_CONS_HTG_Min",foda_ho_cons_htg_min),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_CONS_TTL_Min",foda_ho_cons_ttl_min),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_RCVD_Min",foda_ho_rcvd_min),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_AMEND_Min",foda_ho_amend_min),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_ROB_Min",foda_ho_rob_min),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_ROB_PNN_Min",foda_do_rob_pnn_min),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_CONS_ME_Min",foda_do_cons_me_min),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_CONS_AE_Min",foda_do_cons_ae_min),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_CONS_BLR_Min",foda_do_cons_blr_min),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_CONS_TC_Min",foda_do_cons_tc_min),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_CONS_HTG_Min",foda_do_cons_htg_min),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_CONS_TTL_Min",foda_do_cons_ttl_min),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_RCVD_Min",foda_do_rcvd_min),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_AMEND_Min",foda_do_amend_min),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_ROB_Min",foda_do_rob_min),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_ROB_PNN_Min",foda_go_rob_pnn_min),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_CONS_ME_Min",foda_go_cons_me_min),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_CONS_AE_Min",foda_go_cons_ae_min),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_CONS_BLR_Min",foda_go_cons_blr_min),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_CONS_TC_Min",foda_go_cons_tc_min),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_CONS_HTG_Min",foda_go_cons_htg_min),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_CONS_TTL_Min",foda_go_cons_ttl_min),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_RCVD_Min",foda_go_rcvd_min),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_AMEND_Min",foda_go_amend_min),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_ROB_Min",foda_go_rob_min),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_ROB_PNN_Max",foda_ho_rob_pnn_max),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_CONS_ME_Max",foda_ho_cons_me_max),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_CONS_AE_Max",foda_ho_cons_ae_max),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_CONS_BLR_Max",foda_ho_cons_blr_max),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_CONS_TC_max",foda_ho_cons_tc_max),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_CONS_HTG_max",foda_ho_cons_htg_max),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_CONS_TTL_max",foda_ho_cons_ttl_max),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_RCVD_max",foda_ho_rcvd_max),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_AMEND_max",foda_ho_amend_max),
            new System.Data.SqlClient.SqlParameter("@FODA_HO_ROB_max",foda_ho_rob_max),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_ROB_PNN_max",foda_do_rob_pnn_max),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_CONS_ME_max",foda_do_cons_me_max),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_CONS_AE_max",foda_do_cons_ae_max),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_CONS_BLR_max",foda_do_cons_blr_max),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_CONS_TC_max",foda_do_cons_tc_max),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_CONS_HTG_max",foda_do_cons_htg_max),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_CONS_TTL_max",foda_do_cons_ttl_max),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_RCVD_max",foda_do_rcvd_max),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_AMEND_max",foda_do_amend_max),
            new System.Data.SqlClient.SqlParameter("@FODA_DO_ROB_max",foda_do_rob_max),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_ROB_PNN_max",foda_go_rob_pnn_max),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_CONS_ME_max",foda_go_cons_me_max),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_CONS_AE_max",foda_go_cons_ae_max),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_CONS_BLR_max",foda_go_cons_blr_max),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_CONS_TC_max",foda_go_cons_tc_max),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_CONS_HTG_max",foda_go_cons_htg_max),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_CONS_TTL_max",foda_go_cons_ttl_max),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_RCVD_max",foda_go_rcvd_max),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_AMEND_max",foda_go_amend_max),
            new System.Data.SqlClient.SqlParameter("@FODA_GO_ROB_max",foda_go_rob_max),
            new System.Data.SqlClient.SqlParameter("@MODIFIED_BY",modified_by) };
            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_UPDATE_ERLOGDETAILS_THRESHOLD_FODA", obj);

        }
        public static int ErLog_LODA_THRESHOLD_Update(int? id, int? vessel_id, decimal? loda_mecc_rob_pnn_min, decimal? loda_mecc_rcvd_min, decimal? loda_mecc_cnsmp_min, decimal? loda_mecc_rob_min,
          decimal? loda_mecyl_rob_pnn_min, decimal? loda_mecyl_rcvd_min, decimal? loda_mecyl_cnsmp_min, decimal? loda_mecyl_rob_min, decimal? loda_aecc_rob_pnn_min, decimal? loda_aecc_rcvd_min, decimal? loda_aecc_cnsmp_min,
          decimal? loda_aecc_rob_min, decimal? loda_mecc_rob_pnn_max, decimal? loda_mecc_rcvd_max, decimal? loda_mecc_cnsmp_max, decimal? loda_mecc_rob_max, decimal? loda_mecyl_rob_pnn_max, decimal? loda_mecyl_rcvd_max,
          decimal? loda_mecyl_cnsmp_max, decimal? loda_mecyl_rob_max, decimal? loda_aecc_rob_pnn_max, decimal? loda_aecc_rcvd_max, decimal? loda_aecc_cnsmp_max, decimal? loda_aecc_rob_max, decimal? modified_by)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@ID",id),
             new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
            new System.Data.SqlClient.SqlParameter("@LODA_MECC_ROB_PNN_Min",loda_mecc_rob_pnn_min),
            new System.Data.SqlClient.SqlParameter("@LODA_MECC_RCVD_Min",loda_mecc_rcvd_min),
            new System.Data.SqlClient.SqlParameter("@LODA_MECC_CNSMP_min",loda_mecc_cnsmp_min),
            new System.Data.SqlClient.SqlParameter("@LODA_MECC_ROB_min",loda_mecc_rob_min),
            new System.Data.SqlClient.SqlParameter("@LODA_MECYL_ROB_PNN_Min",loda_mecyl_rob_pnn_min),
            new System.Data.SqlClient.SqlParameter("@LODA_MECYL_RCVD_Min",loda_mecyl_rcvd_min),
            new System.Data.SqlClient.SqlParameter("@LODA_MECYL_CNSMP_Min",loda_mecyl_cnsmp_min),
            new System.Data.SqlClient.SqlParameter("@LODA_MECYL_ROB_Min",loda_mecyl_rob_min),
            new System.Data.SqlClient.SqlParameter("@LODA_AECC_ROB_PNN_Min",loda_aecc_rob_pnn_min),
            new System.Data.SqlClient.SqlParameter("@LODA_AECC_RCVD_Min",loda_aecc_rcvd_min),
            new System.Data.SqlClient.SqlParameter("@LODA_AECC_CNSMP_Min",loda_aecc_cnsmp_min),
            new System.Data.SqlClient.SqlParameter("@LODA_AECC_ROB_Min",loda_aecc_rob_min),
            new System.Data.SqlClient.SqlParameter("@LODA_MECC_ROB_PNN_MAX",loda_mecc_rob_pnn_max),
            new System.Data.SqlClient.SqlParameter("@LODA_MECC_RCVD_MAX",loda_mecc_rcvd_max),
            new System.Data.SqlClient.SqlParameter("@LODA_MECC_CNSMP_MAX",loda_mecc_cnsmp_max),
            new System.Data.SqlClient.SqlParameter("@LODA_MECC_ROB_MAX",loda_mecc_rob_max),
            new System.Data.SqlClient.SqlParameter("@LODA_MECYL_ROB_PNN_MAX",loda_mecyl_rob_pnn_max),
            new System.Data.SqlClient.SqlParameter("@LODA_MECYL_RCVD_MAX",loda_mecyl_rcvd_max),
            new System.Data.SqlClient.SqlParameter("@LODA_MECYL_CNSMP_MAX",loda_mecyl_cnsmp_max),
            new System.Data.SqlClient.SqlParameter("@LODA_MECYL_ROB_MAX",loda_mecyl_rob_max),
            new System.Data.SqlClient.SqlParameter("@LODA_AECC_ROB_PNN_MAX",loda_aecc_rob_pnn_max),
            new System.Data.SqlClient.SqlParameter("@LODA_AECC_RCVD_MAX",loda_aecc_rcvd_max),
            new System.Data.SqlClient.SqlParameter("@LODA_AECC_CNSMP_MAX",loda_aecc_cnsmp_max),
            new System.Data.SqlClient.SqlParameter("@LODA_AECC_ROB_MAX",loda_aecc_rob_max),
            new System.Data.SqlClient.SqlParameter("@MODIFIED_BY",modified_by) };
            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_UPDATE_ERLOGDETAILS_THRESHOLD_LODA", obj);
        }
        public static int ErLog_FWDa_THRESHOLD_Update(int? id, int? vessel_id, decimal? fwda_pot_rob_pnn_min, decimal? fwda_pot_prod_min, decimal? fwda_pot_rcvd_min, decimal? fwda_pot_cnsmp_min,
          decimal? fwda_pot_rob_min, decimal? fwda_washp_rob_pnn_min, decimal? fwda_washp_prod_min, decimal? fwda_washp_rcvd_min, decimal? fwda_washp_cnsmp_min, decimal? fwda_washp_rob_min, decimal? fwda_washs_rob_pnn_min,
          decimal? fwda_washs_prod_min, decimal? fwda_washs_rcvd_min, decimal? fwda_washs_cnsmp_min, decimal? fwda_washs_rob_min, decimal? fwda_distl_rob_pnn_min, decimal? fwda_distl_prod_min, decimal? fwda_distl_rcvd_min,
          decimal? fwda_distl_cnsmp_min, decimal? fwda_distl_rob_min, decimal? fwda_pot_rob_pnn_max, decimal? fwda_pot_prod_max, decimal? fwda_pot_rcvd_max, decimal? fwda_pot_cnsmp_max, decimal? fwda_pot_rob_max,
          decimal? fwda_washp_rob_pnn_max, decimal? fwda_washp_prod_max, decimal? fwda_washp_rcvd_max, decimal? fwda_washp_cnsmp_max, decimal? fwda_washp_rob_max, decimal? fwda_washs_rob_pnn_max, decimal? fwda_washs_prod_max,
          decimal? fwda_washs_rcvd_max, decimal? fwda_washs_cnsmp_max, decimal? fwda_washs_rob_max, decimal? fwda_distl_rob_pnn_max, decimal? fwda_distl_prod_max, decimal? fwda_distl_rcvd_max, decimal? fwda_distl_cnsmp_max,
          decimal? fwda_distl_rob_max, int? modified_by)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@ID",id),
            new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
            new System.Data.SqlClient.SqlParameter("@FWDA_POT_ROB_PNN_Min",fwda_pot_rob_pnn_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_POT_PROD_Min",fwda_pot_prod_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_POT_RCVD_Min",fwda_pot_rcvd_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_POT_CNSMP_Min",fwda_pot_cnsmp_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_POT_ROB_Min",fwda_pot_rob_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHP_ROB_PNN_Min",fwda_washp_rob_pnn_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHP_PROD_Min",fwda_washp_prod_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHP_RCVD_Min",fwda_washp_rcvd_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHP_CNSMP_Min",fwda_washp_cnsmp_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHP_ROB_Min",fwda_washp_rob_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHS_ROB_PNN_Min",fwda_washs_rob_pnn_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHS_PROD_Min",fwda_washs_prod_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHS_RCVD_Min",fwda_washs_rcvd_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHS_CNSMP_Min",fwda_washs_cnsmp_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHS_ROB_Min",fwda_washs_rob_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_DISTL_ROB_PNN_Min",fwda_distl_rob_pnn_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_DISTL_PROD_Min",fwda_distl_prod_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_DISTL_RCVD_Min",fwda_distl_rcvd_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_DISTL_CNSMP_Min",fwda_distl_cnsmp_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_DISTL_ROB_Min",fwda_distl_rob_min),
            new System.Data.SqlClient.SqlParameter("@FWDA_POT_ROB_PNN_MAX",fwda_pot_rob_pnn_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_POT_PROD_MAX",fwda_pot_prod_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_POT_RCVD_MAX",fwda_pot_rcvd_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_POT_CNSMP_MAX",fwda_pot_cnsmp_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_POT_ROB_MAX",fwda_pot_rob_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHP_ROB_PNN_MAX",fwda_washp_rob_pnn_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHP_PROD_MAX",fwda_washp_prod_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHP_RCVD_MAX",fwda_washp_rcvd_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHP_CNSMP_MAX",fwda_washp_cnsmp_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHP_ROB_MAX",fwda_washp_rob_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHS_ROB_PNN_MAX",fwda_washs_rob_pnn_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHS_PROD_MAX",fwda_washs_prod_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHS_RCVD_MAX",fwda_washs_rcvd_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHS_CNSMP_MAX",fwda_washs_cnsmp_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_WASHS_ROB_MAX",fwda_washs_rob_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_DISTL_ROB_PNN_MAX",fwda_distl_rob_pnn_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_DISTL_PROD_MAX",fwda_distl_prod_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_DISTL_RCVD_MAX",fwda_distl_rcvd_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_DISTL_CNSMP_MAX",fwda_distl_cnsmp_max),
            new System.Data.SqlClient.SqlParameter("@FWDA_DISTL_ROB_MAX",fwda_distl_rob_max), 
            new System.Data.SqlClient.SqlParameter("@MODIFIED_BY",modified_by)};

            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_UPDATE_ERLOGDETAILS_THRESHOLD_FWDA", obj);
        }
        public static DataTable Get_ERLogME_01_Values_Changes(int Id, int Log_Id, int Vessel_ID, string Column_name)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@LOG_WATCH", Id),
                  new System.Data.SqlClient.SqlParameter("@Log_Id", Log_Id),
                  new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                  new System.Data.SqlClient.SqlParameter("@Column_name", Column_name)
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ERLOGME_01_LOGCHANGES", obj);

            return ds.Tables[0];
        }
        public static DataTable Get_ERLogME_02_Values_Changes(int Id, int Log_Id, int Vessel_ID, string Column_name)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@LOG_WATCH", Id),
                  new System.Data.SqlClient.SqlParameter("@Log_Id", Log_Id),
                  new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                  new System.Data.SqlClient.SqlParameter("@Column_name", Column_name)
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ERLOGME_02_LOGCHANGES", obj);

            return ds.Tables[0];
        }
        public static DataTable Get_ERLogME_TB_Values_Changes(int Id, int Log_Id, int Vessel_ID, string Column_name)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@LOG_WATCH", Id),
                  new System.Data.SqlClient.SqlParameter("@Log_Id", Log_Id),
                  new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                  new System.Data.SqlClient.SqlParameter("@Column_name", Column_name)
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ERLOGMETB_LOGCHANGES", obj);

            return ds.Tables[0];
        }
        public static DataTable Get_ERLog_Details_Values_Changes(int Log_Id, int Vessel_ID, string Column_name)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@LOG_ID", Log_Id),
                  new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                  new System.Data.SqlClient.SqlParameter("@Column_name", Column_name)
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ERLOGDETAILS_LOGCHANGES", obj);

            return ds.Tables[0];
        }
        public static DataTable Get_ERLog_AC_FW_MISC_Values_Changes(int Id, int Log_Id, int Vessel_ID, string Column_name)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@LOG_WATCH", Id),
                  new System.Data.SqlClient.SqlParameter("@Log_Id", Log_Id),
                  new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                  new System.Data.SqlClient.SqlParameter("@Column_name", Column_name)
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ACFWMISC_LOGCHANGES", obj);

            return ds.Tables[0];
        }
        public static DataTable Get_ERLog_TASG_Values_Changes(int Id, int Log_Id, int Vessel_ID, string Column_name)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@LOG_WATCH", Id),
                  new System.Data.SqlClient.SqlParameter("@Log_Id", Log_Id),
                  new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                  new System.Data.SqlClient.SqlParameter("@Column_name", Column_name)
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ERLOGTASG_LOGCHANGES", obj);

            return ds.Tables[0];
        }
        public static DataTable Get_ERLog_GTREngine_Values_Changes(int Id, int Log_Id, int Vessel_ID, string Column_name)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@LOG_WATCH", Id),
                  new System.Data.SqlClient.SqlParameter("@Log_Id", Log_Id),
                  new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                  new System.Data.SqlClient.SqlParameter("@Column_name", Column_name)
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_GTRENGINE_LOGCHANGES", obj);

            return ds.Tables[0];
        }
        public static DataTable Get_ERLog_TankLevel_Values_Changes(int Id, int Log_Id, int Vessel_ID, string Column_name)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@LOG_WATCH", Id),
                  new System.Data.SqlClient.SqlParameter("@Log_Id", Log_Id),
                  new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                  new System.Data.SqlClient.SqlParameter("@Column_name", Column_name)
                    
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_TANKLEVEL_LOGCHANGES", obj);

            return ds.Tables[0];
        }

        public static DataSet Get_ErLogBookThresHold_EDIT(int Vessel_ID, int? Log_Id, int? Threshold_ID)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Vessel_Id", Vessel_ID),
                  new System.Data.SqlClient.SqlParameter("@Log_ID", Log_Id)  ,
                  new System.Data.SqlClient.SqlParameter("@Threshold_Id", Threshold_ID)  
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_GET_ERLOG_THRESHOLD", obj);

            return ds;
        }

        public static int Inser_Update_ErLogBookThresHold(int? ID, int VESSEL_ID, decimal? ME_TEMP_EXH_Min, decimal? ME_TEMP_EXH_Max, decimal? METB_T_EXH_INLET_MIN, decimal? METB_T_EXH_INLET_MAX, decimal? METB_T_EXH_OUTLET_MIN,
            decimal? METB_T_EXH_OUTLET_Max, decimal? METB_T_EXH_AIR_IN_MIN, decimal? METB_T_EXH_AIR_OUT_MIN, decimal? METB_T_EXH_AIR_IN_Max, decimal? METB_T_EXH_AIR_OUT_Max, decimal? METB_T_SCAVENGE_MIN, decimal? METB_T_SCAVENGE_Max,
            decimal? METB_T_LO_B_MIN, decimal? METB_T_LO_B_MAX, decimal? METB_T_LO_T_Min, decimal? METB_T_LO_T_Max, decimal? METB_P_PD_AC_Min, decimal? METB_P_PD_AC_Max, decimal? ME_MB_IN_Min, decimal? ME_MB_IN_Max, decimal? ME_MB_OUT_Min,
            decimal? ME_MB_OUT_Max, decimal? ME_JC_IN_Min, decimal? ME_JC_IN_Max, decimal? ME_JC_OUT_Min, decimal? ME_JC_OUT_Max, decimal? ME_PC_IN_Min, decimal? ME_PC_IN_Max, decimal? ME_PC_OUT_Min, decimal? ME_PC_OUT_Max,
            decimal? ME_FUEL_OIL_Min, decimal? ME_FUEL_OIL_Max, decimal? ME_JC_FW_IN_min, decimal? ME_JC_FW_IN_Max, decimal? ME_JC_FW_OUT_Min, decimal? ME_JC_FW_OUT_Max, decimal? ME_LC_LO_IN_min, decimal? ME_LC_LO_IN_Max,
            decimal? ME_LC_LO_OUT_Min, decimal? ME_LC_LO_OUT_Max, decimal? ME_PC_LO_IN_min, decimal? ME_PC_LO_IN_Max, decimal? ME_PC_LO_OUT_min, decimal? ME_PC_LO_OUT_Max, decimal? ME_P_JACKET_WATER_min, decimal? ME_P_JACKET_WATER_Max,
            decimal? ME_P_BEARING_XND_LO_Min, decimal? ME_P_BEARING_XND_LO_Max, decimal? ME_P_CAMSHAFT_LO_Min, decimal? ME_P_CAMSHAFT_LO_Max, decimal? ME_P_FV_COOLING_Min, decimal? ME_P_FV_COOLING_Max, decimal? ME_P_FUEL_OIL_min,
            decimal? ME_P_FUEL_OIL_Max, decimal? ME_P_PISTON_COOLING_Min, decimal? ME_P_PISTON_COOLING_Max, decimal? ME_P_CONTROL_AIR_min, decimal? ME_P_CONTROL_AIR_Max, decimal? REF_MEAT_TEMP_Min, decimal? REF_MEAT_TEMP_Max,
            decimal? REF_FISH_TEMP_Min, decimal? REF_FISH_TEMP_Max, decimal? REF_VEG_LOBBY_TEMP_Min, decimal? REF_VEG_LOBBY_TEMP_Max, decimal? FWGEN_VACCUM_Min, decimal? FWGEN_VACCUM_Max, decimal? FWGEN_SHELL_TEMP_Min,
            decimal? FWGEN_SHELL_TEMP_Max, decimal? FWGEN_SALINITY_PPM_Min, decimal? FWGEN_SALINITY_PPM_Max, decimal? PUR_HO_TEMP_Min, decimal? PUR_HO_TEMP_Max, decimal? PUR_LO_TEMP_Min, decimal? PUR_LO_TEMP_Max, decimal? MISC_THRUST_BRG_TEMP_Min,
            decimal? MISC_THRUST_BRG_TEMP_Max, decimal? MISC_INTERM_BRG_TEMP_Min, decimal? MISC_INTERM_BRG_TEMP_Max, decimal? MISC_STERN_TUBE_OIL_TEMP_Min, decimal? MISC_STERN_TUBE_OIL_TEMP_Max, decimal? MISC_HO_SETT_1_Min,
            decimal? MISC_HO_SETT_1_Max, decimal? MISC_HO_SETT_2_Min, decimal? MISC_HO_SETT_2_Max, decimal? MISC_HO_SERV_1_Min, decimal? MISC_HO_SERV_1_Max, decimal? MISC_HO_SERV_2_Min, decimal? MISC_HO_SERV_2_Max, decimal? GE_TEMP_EXH_MAX_Min,
            decimal? GE_TEMP_EXH_MAX_Max, decimal? GE_TEMP_EXH_MIN_Min, decimal? GE_TEMP_EXH_MIN_Max, decimal? GE_TEMP_CW_IN_Min, decimal? GE_TEMP_CW_IN_Max, decimal? GE_TEMP_CW_OUT_Min, decimal? GE_TEMP_CW_OUT_Max, decimal? GE_TEMP_LO_IN_Min,
            decimal? GE_TEMP_LO_IN_Max, decimal? GE_TEMP_LO_OUT_Min, decimal? GE_TEMP_LO_OUT_Max, decimal? GE_PRESS_LO_Min, decimal? GE_PRESS_LO_Max, decimal? GE_PRESS_CW_Min, decimal? GE_PRESS_CW_Max, decimal? SG_LO_PRESS_Min,
            decimal? SG_LO_PRESS_Max, decimal? SG_LO_TEMP_Min, decimal? SG_LO_TEMP_Max, int CREATED_BY)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                new System.Data.SqlClient.SqlParameter("@ID",ID),
                new System.Data.SqlClient.SqlParameter("@VESSEL_ID",VESSEL_ID),
                new System.Data.SqlClient.SqlParameter("@ME_TEMP_EXH_Min",ME_TEMP_EXH_Min),
                new System.Data.SqlClient.SqlParameter("@ME_TEMP_EXH_Max",ME_TEMP_EXH_Max),
                new System.Data.SqlClient.SqlParameter("@METB_T_EXH_INLET_MIN",METB_T_EXH_INLET_MIN),
                new System.Data.SqlClient.SqlParameter("@METB_T_EXH_INLET_MAX",METB_T_EXH_INLET_MAX),
                new System.Data.SqlClient.SqlParameter("@METB_T_EXH_OUTLET_MIN",METB_T_EXH_OUTLET_MIN),
                new System.Data.SqlClient.SqlParameter("@METB_T_EXH_OUTLET_Max",METB_T_EXH_OUTLET_Max),
                new System.Data.SqlClient.SqlParameter("@METB_T_EXH_AIR_IN_MIN",METB_T_EXH_AIR_IN_MIN),
                new System.Data.SqlClient.SqlParameter("@METB_T_EXH_AIR_OUT_MIN",METB_T_EXH_AIR_OUT_MIN),
                new System.Data.SqlClient.SqlParameter("@METB_T_EXH_AIR_IN_Max",METB_T_EXH_AIR_IN_Max),
                new System.Data.SqlClient.SqlParameter("@METB_T_EXH_AIR_OUT_Max",METB_T_EXH_AIR_OUT_Max),
                new System.Data.SqlClient.SqlParameter("@METB_T_SCAVENGE_MIN",METB_T_SCAVENGE_MIN),
                new System.Data.SqlClient.SqlParameter("@METB_T_SCAVENGE_Max",METB_T_SCAVENGE_Max),
                new System.Data.SqlClient.SqlParameter("@METB_T_LO_B_MIN",METB_T_LO_B_MIN),
                new System.Data.SqlClient.SqlParameter("@METB_T_LO_B_MAX",METB_T_LO_B_MAX),
                new System.Data.SqlClient.SqlParameter("@METB_T_LO_T_Min",METB_T_LO_T_Min),
                new System.Data.SqlClient.SqlParameter("@METB_T_LO_T_Max",METB_T_LO_T_Max),
                new System.Data.SqlClient.SqlParameter("@METB_P_PD_AC_Min",METB_P_PD_AC_Min),
                new System.Data.SqlClient.SqlParameter("@METB_P_PD_AC_Max",METB_P_PD_AC_Max),
                new System.Data.SqlClient.SqlParameter("@ME_MB_IN_Min",ME_MB_IN_Min),
                new System.Data.SqlClient.SqlParameter("@ME_MB_IN_Max",ME_MB_IN_Max),
                new System.Data.SqlClient.SqlParameter("@ME_MB_OUT_Min",ME_MB_OUT_Min),
                new System.Data.SqlClient.SqlParameter("@ME_MB_OUT_Max",ME_MB_OUT_Max),
                new System.Data.SqlClient.SqlParameter("@ME_JC_IN_Min",ME_JC_IN_Min),
                new System.Data.SqlClient.SqlParameter("@ME_JC_IN_Max",ME_JC_IN_Max),
                new System.Data.SqlClient.SqlParameter("@ME_JC_OUT_Min",ME_JC_OUT_Min),
                new System.Data.SqlClient.SqlParameter("@ME_JC_OUT_Max",ME_JC_OUT_Max),
                new System.Data.SqlClient.SqlParameter("@ME_PC_IN_Min",ME_PC_IN_Min),
                new System.Data.SqlClient.SqlParameter("@ME_PC_IN_Max",ME_PC_IN_Max),
                new System.Data.SqlClient.SqlParameter("@ME_PC_OUT_Min",ME_PC_OUT_Min),
                new System.Data.SqlClient.SqlParameter("@ME_PC_OUT_Max",ME_PC_OUT_Max),
                new System.Data.SqlClient.SqlParameter("@ME_FUEL_OIL_Min",ME_FUEL_OIL_Min),
                new System.Data.SqlClient.SqlParameter("@ME_FUEL_OIL_Max",ME_FUEL_OIL_Max),
                new System.Data.SqlClient.SqlParameter("@ME_JC_FW_IN_min",ME_JC_FW_IN_min),
                new System.Data.SqlClient.SqlParameter("@ME_JC_FW_IN_Max",ME_JC_FW_IN_Max),
                new System.Data.SqlClient.SqlParameter("@ME_JC_FW_OUT_Min",ME_JC_FW_OUT_Min),
                new System.Data.SqlClient.SqlParameter("@ME_JC_FW_OUT_Max",ME_JC_FW_OUT_Max),
                new System.Data.SqlClient.SqlParameter("@ME_LC_LO_IN_min",ME_LC_LO_IN_min),
                new System.Data.SqlClient.SqlParameter("@ME_LC_LO_IN_Max",ME_LC_LO_IN_Max),
                new System.Data.SqlClient.SqlParameter("@ME_LC_LO_OUT_Min",ME_LC_LO_OUT_Min),
                new System.Data.SqlClient.SqlParameter("@ME_LC_LO_OUT_Max",ME_LC_LO_OUT_Max),
                new System.Data.SqlClient.SqlParameter("@ME_PC_LO_IN_min",ME_PC_LO_IN_min),
                new System.Data.SqlClient.SqlParameter("@ME_PC_LO_IN_Max",ME_PC_LO_IN_Max),
                new System.Data.SqlClient.SqlParameter("@ME_PC_LO_OUT_min",ME_PC_LO_OUT_min),
                new System.Data.SqlClient.SqlParameter("@ME_PC_LO_OUT_Max",ME_PC_LO_OUT_Max),
                new System.Data.SqlClient.SqlParameter("@ME_P_JACKET_WATER_min",ME_P_JACKET_WATER_min),
                new System.Data.SqlClient.SqlParameter("@ME_P_JACKET_WATER_Max",ME_P_JACKET_WATER_Max),
                new System.Data.SqlClient.SqlParameter("@ME_P_BEARING_XND_LO_Min",ME_P_BEARING_XND_LO_Min),
                new System.Data.SqlClient.SqlParameter("@ME_P_BEARING_XND_LO_Max",ME_P_BEARING_XND_LO_Max),
                new System.Data.SqlClient.SqlParameter("@ME_P_CAMSHAFT_LO_Min",ME_P_CAMSHAFT_LO_Min),
                new System.Data.SqlClient.SqlParameter("@ME_P_CAMSHAFT_LO_Max",ME_P_CAMSHAFT_LO_Max),
                new System.Data.SqlClient.SqlParameter("@ME_P_FV_COOLING_Min",ME_P_FV_COOLING_Min),
                new System.Data.SqlClient.SqlParameter("@ME_P_FV_COOLING_Max",ME_P_FV_COOLING_Max),
                new System.Data.SqlClient.SqlParameter("@ME_P_FUEL_OIL_min",ME_P_FUEL_OIL_min),
                new System.Data.SqlClient.SqlParameter("@ME_P_FUEL_OIL_Max",ME_P_FUEL_OIL_Max),
                new System.Data.SqlClient.SqlParameter("@ME_P_PISTON_COOLING_Min",ME_P_PISTON_COOLING_Min),
                new System.Data.SqlClient.SqlParameter("@ME_P_PISTON_COOLING_Max",ME_P_PISTON_COOLING_Max),
                new System.Data.SqlClient.SqlParameter("@ME_P_CONTROL_AIR_min",ME_P_CONTROL_AIR_min),
                new System.Data.SqlClient.SqlParameter("@ME_P_CONTROL_AIR_Max",ME_P_CONTROL_AIR_Max),
                new System.Data.SqlClient.SqlParameter("@REF_MEAT_TEMP_Min",REF_MEAT_TEMP_Min),
                new System.Data.SqlClient.SqlParameter("@REF_MEAT_TEMP_Max",REF_MEAT_TEMP_Max),
                new System.Data.SqlClient.SqlParameter("@REF_FISH_TEMP_Min",REF_FISH_TEMP_Min),
                new System.Data.SqlClient.SqlParameter("@REF_FISH_TEMP_Max",REF_FISH_TEMP_Max),
                new System.Data.SqlClient.SqlParameter("@REF_VEG_LOBBY_TEMP_Min",REF_VEG_LOBBY_TEMP_Min),
                new System.Data.SqlClient.SqlParameter("@REF_VEG_LOBBY_TEMP_Max",REF_VEG_LOBBY_TEMP_Max),
                new System.Data.SqlClient.SqlParameter("@FWGEN_VACCUM_Min",FWGEN_VACCUM_Min),
                new System.Data.SqlClient.SqlParameter("@FWGEN_VACCUM_Max",FWGEN_VACCUM_Max),
                new System.Data.SqlClient.SqlParameter("@FWGEN_SHELL_TEMP_Min",FWGEN_SHELL_TEMP_Min),
                new System.Data.SqlClient.SqlParameter("@FWGEN_SHELL_TEMP_Max",FWGEN_SHELL_TEMP_Max),
                new System.Data.SqlClient.SqlParameter("@FWGEN_SALINITY_PPM_Min",FWGEN_SALINITY_PPM_Min),
                new System.Data.SqlClient.SqlParameter("@FWGEN_SALINITY_PPM_Max",FWGEN_SALINITY_PPM_Max),
                new System.Data.SqlClient.SqlParameter("@PUR_HO_TEMP_Min",PUR_HO_TEMP_Min),
                new System.Data.SqlClient.SqlParameter("@PUR_HO_TEMP_Max",PUR_HO_TEMP_Max),
                new System.Data.SqlClient.SqlParameter("@PUR_LO_TEMP_Min",PUR_LO_TEMP_Min),
                new System.Data.SqlClient.SqlParameter("@PUR_LO_TEMP_Max",PUR_LO_TEMP_Max),
                new System.Data.SqlClient.SqlParameter("@MISC_THRUST_BRG_TEMP_Min",MISC_THRUST_BRG_TEMP_Min),
                new System.Data.SqlClient.SqlParameter("@MISC_THRUST_BRG_TEMP_Max",MISC_THRUST_BRG_TEMP_Max),
                new System.Data.SqlClient.SqlParameter("@MISC_INTERM_BRG_TEMP_Min",MISC_INTERM_BRG_TEMP_Min),
                new System.Data.SqlClient.SqlParameter("@MISC_INTERM_BRG_TEMP_Max",MISC_INTERM_BRG_TEMP_Max),
                new System.Data.SqlClient.SqlParameter("@MISC_STERN_TUBE_OIL_TEMP_Min",MISC_STERN_TUBE_OIL_TEMP_Min),
                new System.Data.SqlClient.SqlParameter("@MISC_STERN_TUBE_OIL_TEMP_Max",MISC_STERN_TUBE_OIL_TEMP_Max),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SETT_1_Min",MISC_HO_SETT_1_Min),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SETT_1_Max",MISC_HO_SETT_1_Max),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SETT_2_Min",MISC_HO_SETT_2_Min),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SETT_2_Max",MISC_HO_SETT_2_Max),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SERV_1_Min",MISC_HO_SERV_1_Min),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SERV_1_Max",MISC_HO_SERV_1_Max),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SERV_2_Min",MISC_HO_SERV_2_Min),
                new System.Data.SqlClient.SqlParameter("@MISC_HO_SERV_2_Max",MISC_HO_SERV_2_Max),
                new System.Data.SqlClient.SqlParameter("@GE_TEMP_EXH_MAX_Min",GE_TEMP_EXH_MAX_Min),
                new System.Data.SqlClient.SqlParameter("@GE_TEMP_EXH_MAX_Max",GE_TEMP_EXH_MAX_Max),
                new System.Data.SqlClient.SqlParameter("@GE_TEMP_EXH_MIN_Min",GE_TEMP_EXH_MIN_Min),
                new System.Data.SqlClient.SqlParameter("@GE_TEMP_EXH_MIN_Max",GE_TEMP_EXH_MIN_Max),
                new System.Data.SqlClient.SqlParameter("@GE_TEMP_CW_IN_Min",GE_TEMP_CW_IN_Min),
                new System.Data.SqlClient.SqlParameter("@GE_TEMP_CW_IN_Max",GE_TEMP_CW_IN_Max),
                new System.Data.SqlClient.SqlParameter("@GE_TEMP_CW_OUT_Min",GE_TEMP_CW_OUT_Min),
                new System.Data.SqlClient.SqlParameter("@GE_TEMP_CW_OUT_Max",GE_TEMP_CW_OUT_Max),
                new System.Data.SqlClient.SqlParameter("@GE_TEMP_LO_IN_Min",GE_TEMP_LO_IN_Min),
                new System.Data.SqlClient.SqlParameter("@GE_TEMP_LO_IN_Max",GE_TEMP_LO_IN_Max),
                new System.Data.SqlClient.SqlParameter("@GE_TEMP_LO_OUT_Min",GE_TEMP_LO_OUT_Min),
                new System.Data.SqlClient.SqlParameter("@GE_TEMP_LO_OUT_Max",GE_TEMP_LO_OUT_Max),
                new System.Data.SqlClient.SqlParameter("@GE_PRESS_LO_Min",GE_PRESS_LO_Min),
                new System.Data.SqlClient.SqlParameter("@GE_PRESS_LO_Max",GE_PRESS_LO_Max),
                new System.Data.SqlClient.SqlParameter("@GE_PRESS_CW_Min",GE_PRESS_CW_Min),
                new System.Data.SqlClient.SqlParameter("@GE_PRESS_CW_Max",GE_PRESS_CW_Max),
                new System.Data.SqlClient.SqlParameter("@SG_LO_PRESS_Min",SG_LO_PRESS_Min),
                new System.Data.SqlClient.SqlParameter("@SG_LO_PRESS_Max",SG_LO_PRESS_Max),
                new System.Data.SqlClient.SqlParameter("@SG_LO_TEMP_Min",SG_LO_TEMP_Min),
                new System.Data.SqlClient.SqlParameter("@SG_LO_TEMP_Max",SG_LO_TEMP_Max),
                new System.Data.SqlClient.SqlParameter("@USERID",CREATED_BY)
                  
            };

            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_INS_UPT_ERLOG_THRESHOLD", obj);


        }

        public static int CopyERLogBookThreshold(int Vessel_ID, int copyfromvesselid, int userid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                 
                  new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID),
                  new System.Data.SqlClient.SqlParameter("@COPYFROMVESSEL_ID",copyfromvesselid),    
                  new System.Data.SqlClient.SqlParameter("@USERID", userid)
                    
            };

            return SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_COPY_ERLOG_THRESHOLD", obj);

        }


        public static DataSet ErLog_Seach_All_Details(int logId, int vessel_id, int? pagenumber, int? pagesize, ref int isfetchcount, int? PageFrom, int? PageTo, DateTime? DateFrom, DateTime? DateTo)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@LOGID",logId),
                    new System.Data.SqlClient.SqlParameter("@VESSEL_ID",vessel_id),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),                 
                     new System.Data.SqlClient.SqlParameter("@PageFrom",PageFrom),
                       new System.Data.SqlClient.SqlParameter("@PageTo",PageTo),
                         new System.Data.SqlClient.SqlParameter("@DateFrom",DateFrom),
                           new System.Data.SqlClient.SqlParameter("@DateTo",DateTo),
                             new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ERLog_ALLDETAILS", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds; ;
        }
        public static DataSet Get_Erlog_WatchHours_Anomaly(int Vessel_ID, DateTime Log_date)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Log_Date",Log_date),  
                  new System.Data.SqlClient.SqlParameter("@Vessel_ID", Vessel_ID) 
                    
                  
                    
            };

            return SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_SEARCH_ERLOGDETAILS_WATCHHOURS", obj);

        }

        public static DataTable Get_VesselList_DL(int FleetID, int VesselID, int Vessel_Manager, string SearchText, int UserCompanyID, int IsVessel)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_VesselList", new SqlParameter("FleetID", FleetID),
                                                                                                new SqlParameter("VesselID", VesselID),
                                                                                                new SqlParameter("Vessel_Manager", Vessel_Manager),
                                                                                                new SqlParameter("SearchText", SearchText),
                                                                                                new SqlParameter("UserCompanyID", UserCompanyID),
                                                                                                new SqlParameter("IsVessel", IsVessel)
                                                                                                ).Tables[0];


        }


        public static int ErLog_Update_Status(int Log_ID, int Vessel_Id, int USER_ID, string Reason)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            {                   
                  new System.Data.SqlClient.SqlParameter("@Log_ID", Log_ID),
                    new System.Data.SqlClient.SqlParameter("@Vessel_Id", Vessel_Id),
                            new System.Data.SqlClient.SqlParameter("@USER_ID", USER_ID),
                                  new System.Data.SqlClient.SqlParameter("@Reason", Reason),
                        new SqlParameter("return",SqlDbType.Int),

            };
            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_UPD_ERLOGSTATUS", obj);
            return Convert.ToInt32(obj[obj.Length - 1].Value);
        }


        public static DataTable Get_FollowupList(int? VESSEL_ID, int? LOG_ID)
        {
            System.Data.SqlClient.SqlParameter[] sqlprm = new System.Data.SqlClient.SqlParameter[]
              { 
                  new System.Data.SqlClient.SqlParameter("@LOG_ID",LOG_ID)                ,
        
                  new System.Data.SqlClient.SqlParameter("@VESSEL_ID",VESSEL_ID) ,               
              
            };
            return SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_Get_Followups", sqlprm).Tables[0];
        }

        public static int Update_Followup_Settings(string MAILTO, int LOGMAIL, int LOGDASH, int FOLLOWUPMAIL, int FOLLOWUPDASH, int ALERTTOMASTER, int ALERTTOCE, int CREATED_BY)
        {
            System.Data.SqlClient.SqlParameter[] sqlprm = new System.Data.SqlClient.SqlParameter[]
              { 
                
                new System.Data.SqlClient.SqlParameter("@MAILTO",MAILTO),
                new System.Data.SqlClient.SqlParameter("@LOGMAIL",LOGMAIL),
                new System.Data.SqlClient.SqlParameter("@LOGDASH",LOGDASH), 
                    new System.Data.SqlClient.SqlParameter("@FOLLOWUPMAIL",FOLLOWUPMAIL),
                new System.Data.SqlClient.SqlParameter("@FOLLOWUPDASH",FOLLOWUPDASH), 
                    new System.Data.SqlClient.SqlParameter("@ALERTTOMASTER",ALERTTOMASTER),
                new System.Data.SqlClient.SqlParameter("@ALERTTOCE",ALERTTOCE),  
                new System.Data.SqlClient.SqlParameter("@CREATED_BY",CREATED_BY)               
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_ERL_UPD_ALERTSETTINGS", sqlprm));
        }
        public static DataSet Get_Followup_Settings()
        {
            return SqlHelper.ExecuteDataset(DAL_Tec_ErLog.ConnectionString, CommandType.StoredProcedure, "TEC_GET_ALERTSETTINGS");
        }

    }
}




