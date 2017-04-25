using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Technical;

namespace SMS.Business.Technical
{
    public class BLL_Tec_ErLog
    {
        static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
        DAL_Tec_ErLog a = new DAL_Tec_ErLog();

        public static DataTable ErLog_ME_TB_Search(int logId, int vessel_id, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Tec_ErLog.ErLog_ME_TB_Search(logId, vessel_id, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable ErLog_ME_01_Search(int logId, int vessel_id, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Tec_ErLog.ErLog_ME_01_Search(logId, vessel_id, pagenumber, pagesize, ref isfetchcount);
        }

        public static DataTable ErLog_MEngine_01_Search(int logId, int vessel_id, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Tec_ErLog.ErLog_MEngine_01_Search(logId, vessel_id, pagenumber, pagesize, ref isfetchcount);
        }

        public static DataTable ErLog_ME_02_Search(int Log_id, int Vessel_ID, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Tec_ErLog.ErLog_ME_02_Search(Log_id, Vessel_ID, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable ErLog_AC_FM_MISC_Search(int Log_id, int Vessel_ID, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Tec_ErLog.ErLog_AC_FM_MISC_Search(Log_id, Vessel_ID, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable ErLog_ME_00_Search(int? VesselList, string FromDate, string ToDate, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            DateTime? fromdt = null;
            if (FromDate != null && FromDate != "")
                fromdt = DateTime.Parse(FromDate);
            DateTime? todt = null;
            if (ToDate != null && ToDate != "")
                todt = DateTime.Parse(ToDate);
            return DAL_Tec_ErLog.ErLog_ME_00_Search(VesselList, fromdt, todt, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable ErLog_ME_00_Get(int logId, int vessel_id)
        {
            return DAL_Tec_ErLog.ErLog_ME_00_Get(logId, vessel_id);
        }
        public static DataTable GET_ERLOGVERSIONS(int logId, int vessel_id)
        {
            return DAL_Tec_ErLog.GET_ERLOGVERSIONS(logId, vessel_id);
        }
        public static DataTable ErLog_Generator_Engine_Search(int Log_id, int Vessel_ID, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Tec_ErLog.ErLog_Generator_Engine_Search(Log_id, Vessel_ID, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable ErLog_TASG_Search(int Log_id, int Vessel_ID, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Tec_ErLog.ErLog_TASG_Search(Log_id, Vessel_ID, pagenumber, pagesize, ref isfetchcount);
        }
        public static DataTable ErLog_Tank_Levels_Search(int Log_id, int Vessel_ID, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Tec_ErLog.ErLog_Tank_Levels_Search(Log_id, Vessel_ID, pagenumber, pagesize, ref isfetchcount);
        }

        public static DataTable ErLog_Engineer_Officer_Remarks_Search(int Log_id, int Vessel_ID, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return DAL_Tec_ErLog.ErLog_Engineer_Officer_Remarks_Search(Log_id, Vessel_ID, pagenumber, pagesize, ref isfetchcount);
        }

        public static DataTable ErLog_AC_FM_MISC_EDIT(int Vessel_ID)
        {
            return DAL_Tec_ErLog.ErLog_AC_FM_MISC_EDIT(Vessel_ID);
        }
        public static DataTable ErLog_ME_01_EDIT(int Vessel_ID)
        {
            return DAL_Tec_ErLog.ErLog_ME_01_EDIT(Vessel_ID);
        }
        public static DataTable ErLog_TASG_EDIT(int Vessel_ID)
        {
            return DAL_Tec_ErLog.ErLog_TASG_EDIT(Vessel_ID);
        }
        public static DataTable ERLOG_GENERATOR_ENGINES_EDIT(int Vessel_ID)
        {
            return DAL_Tec_ErLog.ERLOG_GENERATOR_ENGINES_EDIT(Vessel_ID);
        }
        public static DataTable ErLog_ME_02_EDIT(int Vessel_ID)
        {
            return DAL_Tec_ErLog.ErLog_ME_02_EDIT(Vessel_ID);
        }

        public static DataTable ErLog_TANK_LEVELS_EDIT(int Vessel_ID)
        {
            return DAL_Tec_ErLog.ErLog_TANK_LEVELS_EDIT(Vessel_ID);
        }
        public static DataTable ErLog_ThresHold_Main_EDIT(int Vessel_ID)
        {
            return DAL_Tec_ErLog.ErLog_ThresHold_Main_EDIT(Vessel_ID);
        }
        public static int ErLog_ME_01_THRESHOLD_Update(int? id, int vessel_id, decimal? watch_hours_min, decimal? watch_minutes_min, decimal? me_counter_min, decimal? me_rpm_min, decimal? me_control_min, decimal? me_gov_ctrl_min, decimal? me_load_indicator_min,
          decimal? me_fuel_pp_index_min, decimal? me_fuel_flowmeter_min, decimal? me_exh_temp_min, decimal? watch_hours_max, decimal? watch_minutes_max, decimal? me_counter_max, decimal? me_rpm_max, decimal? me_control_max, decimal? me_gov_ctrl_max, decimal? me_load_indicator_max,
          decimal? me_fuel_pp_index_max, decimal? me_fuel_flowmeter_max, decimal? me_exh_temp_max, decimal? tc_rpm_min, decimal? tc_rpm_max, decimal? t_exh_in_min, decimal? t_exh_in_max, decimal? t_exh_out_min, decimal? t_exh_out_max, decimal? t_air_in_min, decimal? t_air_in_max,
          decimal? t_air_out_min, decimal? t_air_out_max, decimal? t_scavenge_min, decimal? t_scavenge_max, decimal? t_cw_in_min, decimal? t_cw_in_max, decimal? t_cw_out_min, decimal? t_cw_out_max, decimal? t_lo_b_min, decimal? t_lo_b_max, decimal? t_lo_t_min, decimal? t_lo_t_max,
          decimal? p_scavenge_kgpcms_min, decimal? p_scavenge_kgpcms_max, decimal? p_exh_back_min, decimal? p_exh_back_max, decimal? p_pd_ac_min, decimal? p_pd_ac_max, decimal? p_pd_af_min, decimal? p_pd_af_max, int? modified_by)
        {
            return DAL_Tec_ErLog.ErLog_ME_01_THRESHOLD_Update(id, vessel_id, watch_hours_min, watch_minutes_min, me_counter_min, me_rpm_min, me_control_min, me_gov_ctrl_min, me_load_indicator_min,
           me_fuel_pp_index_min, me_fuel_flowmeter_min, me_exh_temp_min, watch_hours_max, watch_minutes_max, me_counter_max, me_rpm_max, me_control_max, me_gov_ctrl_max, me_load_indicator_max,
           me_fuel_pp_index_max, me_fuel_flowmeter_max, me_exh_temp_max, tc_rpm_min, tc_rpm_max, t_exh_in_min, t_exh_in_max, t_exh_out_min, t_exh_out_max, t_air_in_min, t_air_in_max,
           t_air_out_min, t_air_out_max, t_scavenge_min, t_scavenge_max, t_cw_in_min, t_cw_in_max, t_cw_out_min, t_cw_out_max, t_lo_b_min, t_lo_b_max, t_lo_t_min, t_lo_t_max,
           p_scavenge_kgpcms_min, p_scavenge_kgpcms_max, p_exh_back_min, p_exh_back_max, p_pd_ac_min, p_pd_ac_max, p_pd_af_min, p_pd_af_max, modified_by);
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
            return DAL_Tec_ErLog.ErLog_ME_02_THRESHOLD_Update(id, vessel_id, mb_in_min, mb_in_max, mb_out_min, mb_out_max, jc_in_min, jc_in_max,
          jc_out_min, jc_out_max, pc_in_min, pc_in_max, pc_out_min, pc_out_max, fuel_oil_min, fuel_oil_max, fuel_visc_min, fuel_visc_max,
          jc_sw_in_min, jc_sw_in_max, jc_sw_out_min, jc_sw_out_max, jc_fw_in_min, jc_fw_in_max, jc_fw_out_min, jc_fw_out_max, lc_sw_in_min,
          lc_sw_in_max, lc_sw_out_min, lc_sw_out_max, lc_lo_in_min, lc_lo_in_max, lc_lo_out_min, lc_lo_out_max, pc_sw_in_min, pc_sw_in_max,
          pc_sw_out_min, pc_sw_out_max, pc_lo_in_min, pc_lo_in_max, pc_lo_out_min, pc_lo_out_max, p_sea_water_min, p_sea_water_max, p_jacket_water_min,
          p_jacket_water_max, p_bearing_xnd_lo_min, p_bearing_xnd_lo_max, p_camshaft_lo_min, p_camshaft_lo_max, p_fv_cooling_min, p_fv_cooling_max,
          p_fuel_oil_min, p_fuel_oil_max, p_piston_cooling_min, p_piston_cooling_max, p_control_air_min, p_control_air_max, p_service_air_min,
          p_service_air_max, modified_by);
        }
        public static int ErLog_GENERATOR_ENGINES_THRESHOLD_Update(int? id, int? vessel_id, int? generator_no, int? min_running_hr,
            decimal? min_temp_exh_max, decimal? min_temp_exh_min, decimal? min_temp_cw_in, decimal? min_temp_cw_out, decimal? min_temp_lo_in, decimal? min_temp_lo_out, decimal? min_temp_boostair,
            decimal? min_temp_pdl_bearing, decimal? min_temp_fuel_in, decimal? min_temp_ae_sw_in, decimal? min_temp_ae_sw_out, decimal? min_temp_ae_lo_in, decimal? min_temp_ae_lo_out,
            decimal? min_press_lo, decimal? min_press_cw, decimal? min_press_boost_air, decimal? min_press_fuel_oil, decimal? min_amps, decimal? min_kw, decimal? max_running_hr, decimal? max_temp_exh_max,
            decimal? max_temp_exh_min, decimal? max_temp_cw_in, decimal? max_temp_cw_out, decimal? max_temp_lo_in, decimal? max_temp_lo_out, decimal? max_temp_boostair, decimal? max_temp_pdl_bearing,
            decimal? max_temp_fuel_in, decimal? max_temp_ae_sw_in, decimal? max_temp_ae_sw_out, decimal? max_temp_ae_lo_in, decimal? max_temp_ae_lo_out, decimal? max_press_lo, decimal? max_press_cw,
            decimal? max_press_boost_air, decimal? max_press_fuel_oil, decimal? max_amps, decimal? max_kw, int modified_by)
        {
            return DAL_Tec_ErLog.ErLog_GENERATOR_ENGINES_THRESHOLD_Update(id, vessel_id, generator_no, min_running_hr,
               min_temp_exh_max, min_temp_exh_min, min_temp_cw_in, min_temp_cw_out, min_temp_lo_in, min_temp_lo_out, min_temp_boostair,
               min_temp_pdl_bearing, min_temp_fuel_in, min_temp_ae_sw_in, min_temp_ae_sw_out, min_temp_ae_lo_in, min_temp_ae_lo_out,
               min_press_lo, min_press_cw, min_press_boost_air, min_press_fuel_oil, min_amps, min_kw, max_running_hr, max_temp_exh_max,
               max_temp_exh_min, max_temp_cw_in, max_temp_cw_out, max_temp_lo_in, max_temp_lo_out, max_temp_boostair, max_temp_pdl_bearing,
               max_temp_fuel_in, max_temp_ae_sw_in, max_temp_ae_sw_out, max_temp_ae_lo_in, max_temp_ae_lo_out, max_press_lo, max_press_cw,
               max_press_boost_air, max_press_fuel_oil, max_amps, max_kw, modified_by);
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
            return DAL_Tec_ErLog.ErLog_AC_FW_MISC_THRESHOLD_Update(id, vessel_id, ac_p_suct_press_min, ac_p_disch_press_min, ac_p_lo_press_min,
            ac_p_cw_press_min, ac_s_suct_press_min, ac_s_disch_press_min, ac_s_lo_press_min, ac_s_cw_press_min, ac_p_in_air_temp_min,
            ac_p_out_air_temp_min, ac_s_in_air_temp_min, ac_s_out_air_temp_min, ref_comp_no_min, ref_suct_press_min, ref_disch_press_min,
            ref_lo_press_min, ref_meat_temp_min, ref_fish_temp_min, ref_veg_lobby_temp_min, fwgen_rh_min, fwgen_fw_in_min, fwgen_fw_out_min,
            fwgen_sw_in_min, fwgen_sw_out_min, fwgen_vaccum_min, fwgen_shell_temp_min, fwgen_salinity_ppm_min, fwgen_flowmeter_min,
            blr_oil_firing_hrs_min, blr_steam_press_min, blr_feed_wtr_temp_min, blr_ege_soot_blown_min, pur_ho_rh_min, pur_ho_temp_min,
            pur_lo_rh_min, pur_lo_temp_min, pur_do_temp_min, misc_sft_grounding_min, misc_thrust_brg_temp_min, misc_interm_brg_temp_min,
            misc_stern_tube_oil_temp_min, misc_sea_wtr_temp_min, misc_er_temp_min, misc_ho_sett_1_min, misc_ho_sett_2_min, misc_ho_serv_1_min,
            misc_ho_serv_2_min, misc_incinerator_rh_min, ac_p_suct_press_max, ac_p_disch_press_max, ac_p_lo_press_max, ac_p_cw_press_max,
            ac_s_suct_press_max, ac_s_disch_press_max, ac_s_lo_press_max, ac_s_cw_press_max, ac_p_in_air_temp_max, ac_p_out_air_temp_max,
            ac_s_in_air_temp_max, ac_s_out_air_temp_max, ref_comp_no_max_max, ref_suct_press_max, ref_disch_press_max, ref_lo_press_max,
            ref_meat_temp_max, ref_fish_temp_max, ref_veg_lobby_temp_max, fwgen_rh_max, fwgen_fw_in_max, fwgen_fw_out_max, fwgen_sw_in_max,
            fwgen_sw_out_max, fwgen_vaccum_max, fwgen_shell_temp_max, fwgen_salinity_ppm_max, fwgen_flowmeter_max, blr_oil_firing_hrs_max,
            blr_steam_press_max, blr_feed_wtr_temp_max, blr_ege_soot_blown_max, pur_ho_rh_max, pur_ho_temp_max, pur_lo_rh_max, pur_lo_temp_max,
            pur_do_temp_max, misc_sft_grounding_max, misc_thrust_brg_temp_max, misc_interm_brg_temp_max, misc_stern_tube_oil_temp_max,
            misc_sea_wtr_temp_max, misc_er_temp_max, misc_ho_sett_1_max, misc_ho_sett_2_max, misc_ho_serv_1_max, misc_ho_serv_2_max,
            misc_incinerator_rh_max, modified_by);
        }

        public static int ErLog_ERLOG_TASG_THRESHOLD_Update(int? id, int? vessel_id, decimal? min_ta_running_hr, decimal? min_ta_steam_press, decimal? min_ta_cond_vac, decimal? min_ta_gland_steam,
           decimal? min_ta_lo_press, decimal? min_ta_lo_temp, decimal? min_ta_thust_big, decimal? min_ta_free_end, decimal? min_ta_kw, decimal? min_ta_amps, decimal? min_sg_running_hrg, decimal? min_sg_clutch_air_pr,
           decimal? min_sg_lo_press, decimal? min_sg_lo_temp, decimal? min_sg_sg_cond_brg_temp1, decimal? min_sg_sg_cond_brg_temp2, decimal? min_sg_kw, decimal? min_sg_amps, decimal? max_ta_running_hr,
           decimal? max_ta_steam_press, decimal? max_ta_cond_vac, decimal? max_ta_gland_steam, decimal? max_ta_lo_press, decimal? max_ta_lo_temp, decimal? max_ta_thust_big, decimal? max_ta_free_end, decimal? max_ta_kw,
           decimal? max_ta_amps, decimal? max_sg_running_hrg, decimal? max_sg_clutch_air_pr, decimal? max_sg_lo_press, decimal? max_sg_lo_temp, decimal? max_sg_sg_cond_brg_temp1, decimal? max_sg_sg_cond_brg_temp2,
           decimal? max_sg_kw, decimal? max_sg_amps, int modified_by)
        {
            return DAL_Tec_ErLog.ErLog_ERLOG_TASG_THRESHOLD_Update(id, vessel_id, min_ta_running_hr, min_ta_steam_press, min_ta_cond_vac, min_ta_gland_steam,
             min_ta_lo_press, min_ta_lo_temp, min_ta_thust_big, min_ta_free_end, min_ta_kw, min_ta_amps, min_sg_running_hrg, min_sg_clutch_air_pr,
             min_sg_lo_press, min_sg_lo_temp, min_sg_sg_cond_brg_temp1, min_sg_sg_cond_brg_temp2, min_sg_kw, min_sg_amps, max_ta_running_hr,
             max_ta_steam_press, max_ta_cond_vac, max_ta_gland_steam, max_ta_lo_press, max_ta_lo_temp, max_ta_thust_big, max_ta_free_end, max_ta_kw,
             max_ta_amps, max_sg_running_hrg, max_sg_clutch_air_pr, max_sg_lo_press, max_sg_lo_temp, max_sg_sg_cond_brg_temp1, max_sg_sg_cond_brg_temp2,
             max_sg_kw, max_sg_amps, modified_by);
        }

        public static int ErLog_TANK_LEVELS_THRESHOLD_Update(int? id, int? vessel_id, decimal? min_cyl_oil_day_tk, decimal? min_me_sump, decimal? min_heavy_oil_settl_tk, decimal? min_heavy_oil_serv_tk,
          decimal? min_belended_oil, decimal? min_do_serv_tk, decimal? max_cyl_oil_day_tk, decimal? max_me_sump, decimal? max_heavy_oil_settl_tk, decimal? max_heavy_oil_serv_tk, decimal? max_belended_oil,
          decimal? max_do_serv_tk, int modified_by)
        {
            return DAL_Tec_ErLog.ErLog_TANK_LEVELS_THRESHOLD_Update(id, vessel_id, min_cyl_oil_day_tk, min_me_sump, min_heavy_oil_settl_tk, min_heavy_oil_serv_tk,
          min_belended_oil, min_do_serv_tk, max_cyl_oil_day_tk, max_me_sump, max_heavy_oil_settl_tk, max_heavy_oil_serv_tk, max_belended_oil,
          max_do_serv_tk, modified_by);

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
            return DAL_Tec_ErLog.ErLog_BLR_CW_THRESHOLD_Update(id, vessel_id, blr_cw_chlorides_blr_min, blr_cw_chlorides_blr_max, blr_cw_chlorides_mejw_min,
              blr_cw_chlorides_mejw_max, blr_cw_chlorides_mepw_min, blr_cw_chlorides_mepw_max, blr_cw_chlorides_ae_min, blr_cw_chlorides_ae_max, blr_cw_chlorides_cmpr_min,
              blr_cw_chlorides_cmpr_max, blr_cw_alkalinity_blr_min, blr_cw_alkalinity_blr_max, blr_cw_alkalinity_mejw_min, blr_cw_alkalinity_mejw_max, blr_cw_alkalinity_mepw_min,
              blr_cw_alkalinity_mepw_max, blr_cw_alkalinity_ae_min, blr_cw_alkalinity_cmpr_min, blr_cw_talkalinity_blr_min, blr_cw_talkalinity_mejw_min, blr_cw_talkalinity_mepw_mn,
              blr_cw_talkalinity_ae_min, blr_cw_talkalinity_cmpr_min, blr_cw_phosphates_blr_min, blr_cw_phosphates_mejw_min, blr_cw_phosphates_mepw_min, blr_cw_phosphates_ae_min,
              blr_cw_phosphates_cmpr_min, blr_cw_blowdown_blr_min, blr_cw_blowdown_mejw_min, blr_cw_blowdown_mepw_min, blr_cw_blowdown_ae_min, blr_cw_blowdown_cmpr_min,
              blr_cw_nitrites_blr_min, blr_cw_nitrites_mejw_min, blr_cw_nitrites_mepw_min, blr_cw_nitrites_ae_min, blr_cw_nitrites_cmpr_min, blr_cw_dosage_blr_min,
              blr_cw_dosage_mejw_min, blr_cw_dosage_mepw_min, blr_cw_dosage_ae_min, blr_cw_dosage_cmpr_min, blr_cw_alkalinity_ae_max, blr_cw_alkalinity_cmpr_max,
              blr_cw_talkalinity_blr_max, blr_cw_talkalinity_mejw_max, blr_cw_talkalinity_mepw_max, blr_cw_talkalinity_ae_max, blr_cw_talkalinity_cmpr_max, blr_cw_phosphates_blr_max,
              blr_cw_phosphates_mejw_max, blr_cw_phosphates_mepw_max, blr_cw_phosphates_ae_max, blr_cw_phosphates_cmpr_max, blr_cw_blowdown_blr_max, blr_cw_blowdown_mejw_max,
              blr_cw_blowdown_mepw_max, blr_cw_blowdown_ae_max, blr_cw_blowdown_cmpr_max, blr_cw_nitrites_blr_max, blr_cw_nitrites_mejw_max, blr_cw_nitrites_mepw_max,
              blr_cw_nitrites_ae_max, blr_cw_nitrites_cmpr_max, blr_cw_dosage_blr_max, blr_cw_dosage_mejw_max, blr_cw_dosage_mepw_max, blr_cw_dosage_ae_max,
             blr_cw_dosage_cmpr_max, modified_by);
        }
        public static int ErLog_WRKHRS_THRESHOLD_Update(int? id, int? vessel_id, decimal? wrkhrs_me_prev_min, decimal? wrkhrs_me_nn_min, decimal? wrkhrs_me_ttl_min, decimal? wrkhrs_ae1_prev_min,
               decimal? wrkhrs_ae1_nn_min, decimal? wrkhrs_ae1_ttl_min, decimal? wrkhrs_ae2_prev_min, decimal? wrkhrs_ae2_nn_min, decimal? wrkhrs_ae2_ttl_min, decimal? wrkhrs_ae3_prev_min, decimal? wrkhrs_ae3_nn_min,
               decimal? wrkhrs_ae3_ttl_min, decimal? wrkhrs_ae4_prev_min, decimal? wrkhrs_ae4_nn_min, decimal? wrkhrs_ae4_ttl_min, decimal? wrkhrs_ta_prev_min, decimal? wrkhrs_ta_nn_min, decimal? wrkhrs_ta_ttl_min,
               decimal? wrkhrs_sg_prev_min, decimal? wrkhrs_sg_nn_min, decimal? wrkhrs_sg_ttl_min, decimal? wrkhrs_me_prev_max, decimal? wrkhrs_me_nn_max, decimal? wrkhrs_me_ttl_max, decimal? wrkhrs_ae1_prev_max,
               decimal? wrkhrs_ae1_nn_max, decimal? wrkhrs_ae1_ttl_max, decimal? wrkhrs_ae2_prev_max, decimal? wrkhrs_ae2_nn_max, decimal? wrkhrs_ae2_ttl_max, decimal? wrkhrs_ae3_prev_max, decimal? wrkhrs_ae3_nn_max,
               decimal? wrkhrs_ae3_ttl_max, decimal? wrkhrs_ae4_prev_max, decimal? wrkhrs_ae4_nn_max, decimal? wrkhrs_ae4_ttl_max, decimal? wrkhrs_ta_prev_max, decimal? wrkhrs_ta_nn_max, decimal? wrkhrs_ta_ttl_max,
               decimal? wrkhrs_sg_prev_max, decimal? wrkhrs_sg_nn_max, decimal? wrkhrs_sg_ttl_max, int modified_by)
        {
            return DAL_Tec_ErLog.ErLog_WRKHRS_THRESHOLD_Update(id, vessel_id, wrkhrs_me_prev_min, wrkhrs_me_nn_min, wrkhrs_me_ttl_min, wrkhrs_ae1_prev_min,
             wrkhrs_ae1_nn_min, wrkhrs_ae1_ttl_min, wrkhrs_ae2_prev_min, wrkhrs_ae2_nn_min, wrkhrs_ae2_ttl_min, wrkhrs_ae3_prev_min, wrkhrs_ae3_nn_min,
             wrkhrs_ae3_ttl_min, wrkhrs_ae4_prev_min, wrkhrs_ae4_nn_min, wrkhrs_ae4_ttl_min, wrkhrs_ta_prev_min, wrkhrs_ta_nn_min, wrkhrs_ta_ttl_min,
             wrkhrs_sg_prev_min, wrkhrs_sg_nn_min, wrkhrs_sg_ttl_min, wrkhrs_me_prev_max, wrkhrs_me_nn_max, wrkhrs_me_ttl_max, wrkhrs_ae1_prev_max,
             wrkhrs_ae1_nn_max, wrkhrs_ae1_ttl_max, wrkhrs_ae2_prev_max, wrkhrs_ae2_nn_max, wrkhrs_ae2_ttl_max, wrkhrs_ae3_prev_max, wrkhrs_ae3_nn_max,
             wrkhrs_ae3_ttl_max, wrkhrs_ae4_prev_max, wrkhrs_ae4_nn_max, wrkhrs_ae4_ttl_max, wrkhrs_ta_prev_max, wrkhrs_ta_nn_max, wrkhrs_ta_ttl_max,
            wrkhrs_sg_prev_max, wrkhrs_sg_nn_max, wrkhrs_sg_ttl_max, modified_by);
        }

        public static int ErLog_DP_THRESHOLD_Update(decimal? id, decimal? vessel_id, decimal? dp_wind_force_min, decimal? dp_wind_dir_min, decimal? dp_sea_cond_min, decimal? dp_swell_min, decimal? dp_swell_dir_min,
      decimal? dp_curr_min, decimal? dp_revs_ntn_min, decimal? dp_avg_rpm_min, decimal? dp_eng_dist_min, decimal? dp_obs_dist_min, decimal? dp_ttl_dist_min, decimal? dp_hrs_ful_spd_min, decimal? dp_slip_min,
      decimal? dp_dist_togo_min, decimal? dp_hrs_red_spd_min, decimal? dp_hrs_stopd_min, decimal? dp_obs_spd_min, decimal? eta_min, decimal? dp_wind_force_max, decimal? dp_wind_dir_max, decimal? dp_sea_cond_max,
      decimal? dp_swell_max, decimal? dp_swell_dir_max, decimal? dp_curr_max, decimal? dp_revs_ntn_max, decimal? dp_avg_rpm_max, decimal? dp_eng_dist_max, decimal? dp_obs_dist_max, decimal? dp_ttl_dist_max,
      decimal? dp_hrs_ful_spd_max, decimal? dp_slip_max, decimal? dp_dist_togo_max, decimal? dp_hrs_red_spd_max, decimal? dp_hrs_stopd_max, decimal? dp_obs_spd_max, decimal? eta_max, int? modified_by)
        {
            return DAL_Tec_ErLog.ErLog_DP_THRESHOLD_Update(id, vessel_id, dp_wind_force_min, dp_wind_dir_min, dp_sea_cond_min, dp_swell_min, dp_swell_dir_min,
       dp_curr_min, dp_revs_ntn_min, dp_avg_rpm_min, dp_eng_dist_min, dp_obs_dist_min, dp_ttl_dist_min, dp_hrs_ful_spd_min, dp_slip_min,
       dp_dist_togo_min, dp_hrs_red_spd_min, dp_hrs_stopd_min, dp_obs_spd_min, eta_min, dp_wind_force_max, dp_wind_dir_max, dp_sea_cond_max,
       dp_swell_max, dp_swell_dir_max, dp_curr_max, dp_revs_ntn_max, dp_avg_rpm_max, dp_eng_dist_max, dp_obs_dist_max, dp_ttl_dist_max,
       dp_hrs_ful_spd_max, dp_slip_max, dp_dist_togo_max, dp_hrs_red_spd_max, dp_hrs_stopd_max, dp_obs_spd_max, eta_max, modified_by);
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
            return DAL_Tec_ErLog.ErLog_FODA_THRESHOLD_Update(id, vessel_id, foda_ho_rob_pnn_min, foda_ho_cons_me_min, foda_ho_cons_ae_min,
           foda_ho_cons_blr_min, foda_ho_cons_tc_min, foda_ho_cons_htg_min, foda_ho_cons_ttl_min, foda_ho_rcvd_min, foda_ho_amend_min,
           foda_ho_rob_min, foda_do_rob_pnn_min, foda_do_cons_me_min, foda_do_cons_ae_min, foda_do_cons_blr_min, foda_do_cons_tc_min,
           foda_do_cons_htg_min, foda_do_cons_ttl_min, foda_do_rcvd_min, foda_do_amend_min, foda_do_rob_min, foda_go_rob_pnn_min,
           foda_go_cons_me_min, foda_go_cons_ae_min, foda_go_cons_blr_min, foda_go_cons_tc_min, foda_go_cons_htg_min, foda_go_cons_ttl_min,
           foda_go_rcvd_min, foda_go_amend_min, foda_go_rob_min, foda_ho_rob_pnn_max, foda_ho_cons_me_max, foda_ho_cons_ae_max, foda_ho_cons_blr_max,
           foda_ho_cons_tc_max, foda_ho_cons_htg_max, foda_ho_cons_ttl_max, foda_ho_rcvd_max, foda_ho_amend_max, foda_ho_rob_max, foda_do_rob_pnn_max,
           foda_do_cons_me_max, foda_do_cons_ae_max, foda_do_cons_blr_max, foda_do_cons_tc_max, foda_do_cons_htg_max, foda_do_cons_ttl_max,
           foda_do_rcvd_max, foda_do_amend_max, foda_do_rob_max, foda_go_rob_pnn_max, foda_go_cons_me_max, foda_go_cons_ae_max, foda_go_cons_blr_max,
           foda_go_cons_tc_max, foda_go_cons_htg_max, foda_go_cons_ttl_max, foda_go_rcvd_max, foda_go_amend_max, foda_go_rob_max, modified_by);
        }

        public static int ErLog_LODA_THRESHOLD_Update(int? id, int? vessel_id, decimal? loda_mecc_rob_pnn_min, decimal? loda_mecc_rcvd_min, decimal? loda_mecc_cnsmp_min, decimal? loda_mecc_rob_min,
    decimal? loda_mecyl_rob_pnn_min, decimal? loda_mecyl_rcvd_min, decimal? loda_mecyl_cnsmp_min, decimal? loda_mecyl_rob_min, decimal? loda_aecc_rob_pnn_min, decimal? loda_aecc_rcvd_min, decimal? loda_aecc_cnsmp_min,
    decimal? loda_aecc_rob_min, decimal? loda_mecc_rob_pnn_max, decimal? loda_mecc_rcvd_max, decimal? loda_mecc_cnsmp_max, decimal? loda_mecc_rob_max, decimal? loda_mecyl_rob_pnn_max, decimal? loda_mecyl_rcvd_max,
    decimal? loda_mecyl_cnsmp_max, decimal? loda_mecyl_rob_max, decimal? loda_aecc_rob_pnn_max, decimal? loda_aecc_rcvd_max, decimal? loda_aecc_cnsmp_max, decimal? loda_aecc_rob_max, decimal? modified_by)
        {
            return DAL_Tec_ErLog.ErLog_LODA_THRESHOLD_Update(id, vessel_id, loda_mecc_rob_pnn_min, loda_mecc_rcvd_min, loda_mecc_cnsmp_min, loda_mecc_rob_min,
       loda_mecyl_rob_pnn_min, loda_mecyl_rcvd_min, loda_mecyl_cnsmp_min, loda_mecyl_rob_min, loda_aecc_rob_pnn_min, loda_aecc_rcvd_min, loda_aecc_cnsmp_min,
       loda_aecc_rob_min, loda_mecc_rob_pnn_max, loda_mecc_rcvd_max, loda_mecc_cnsmp_max, loda_mecc_rob_max, loda_mecyl_rob_pnn_max, loda_mecyl_rcvd_max,
       loda_mecyl_cnsmp_max, loda_mecyl_rob_max, loda_aecc_rob_pnn_max, loda_aecc_rcvd_max, loda_aecc_cnsmp_max, loda_aecc_rob_max, modified_by);
        }

        public static int ErLog_FWDa_THRESHOLD_Update(int? id, int? vessel_id, decimal? fwda_pot_rob_pnn_min, decimal? fwda_pot_prod_min, decimal? fwda_pot_rcvd_min, decimal? fwda_pot_cnsmp_min,
    decimal? fwda_pot_rob_min, decimal? fwda_washp_rob_pnn_min, decimal? fwda_washp_prod_min, decimal? fwda_washp_rcvd_min, decimal? fwda_washp_cnsmp_min, decimal? fwda_washp_rob_min, decimal? fwda_washs_rob_pnn_min,
    decimal? fwda_washs_prod_min, decimal? fwda_washs_rcvd_min, decimal? fwda_washs_cnsmp_min, decimal? fwda_washs_rob_min, decimal? fwda_distl_rob_pnn_min, decimal? fwda_distl_prod_min, decimal? fwda_distl_rcvd_min,
    decimal? fwda_distl_cnsmp_min, decimal? fwda_distl_rob_min, decimal? fwda_pot_rob_pnn_max, decimal? fwda_pot_prod_max, decimal? fwda_pot_rcvd_max, decimal? fwda_pot_cnsmp_max, decimal? fwda_pot_rob_max,
    decimal? fwda_washp_rob_pnn_max, decimal? fwda_washp_prod_max, decimal? fwda_washp_rcvd_max, decimal? fwda_washp_cnsmp_max, decimal? fwda_washp_rob_max, decimal? fwda_washs_rob_pnn_max, decimal? fwda_washs_prod_max,
    decimal? fwda_washs_rcvd_max, decimal? fwda_washs_cnsmp_max, decimal? fwda_washs_rob_max, decimal? fwda_distl_rob_pnn_max, decimal? fwda_distl_prod_max, decimal? fwda_distl_rcvd_max, decimal? fwda_distl_cnsmp_max,
    decimal? fwda_distl_rob_max, int? modified_by)
        {
            return DAL_Tec_ErLog.ErLog_FWDa_THRESHOLD_Update(id, vessel_id, fwda_pot_rob_pnn_min, fwda_pot_prod_min, fwda_pot_rcvd_min, fwda_pot_cnsmp_min,
      fwda_pot_rob_min, fwda_washp_rob_pnn_min, fwda_washp_prod_min, fwda_washp_rcvd_min, fwda_washp_cnsmp_min, fwda_washp_rob_min, fwda_washs_rob_pnn_min,
      fwda_washs_prod_min, fwda_washs_rcvd_min, fwda_washs_cnsmp_min, fwda_washs_rob_min, fwda_distl_rob_pnn_min, fwda_distl_prod_min, fwda_distl_rcvd_min,
      fwda_distl_cnsmp_min, fwda_distl_rob_min, fwda_pot_rob_pnn_max, fwda_pot_prod_max, fwda_pot_rcvd_max, fwda_pot_cnsmp_max, fwda_pot_rob_max,
      fwda_washp_rob_pnn_max, fwda_washp_prod_max, fwda_washp_rcvd_max, fwda_washp_cnsmp_max, fwda_washp_rob_max, fwda_washs_rob_pnn_max, fwda_washs_prod_max,
      fwda_washs_rcvd_max, fwda_washs_cnsmp_max, fwda_washs_rob_max, fwda_distl_rob_pnn_max, fwda_distl_prod_max, fwda_distl_rcvd_max, fwda_distl_cnsmp_max,
      fwda_distl_rob_max, modified_by);
        }

        public static DataTable Get_ERLogME_01_Values_Changes(int Id, int Log_Id, int Vessel_ID, string Column_name)
        {
            return DAL_Tec_ErLog.Get_ERLogME_01_Values_Changes(Id, Log_Id, Vessel_ID, Column_name);
        }
        public static DataTable Get_ERLogME_02_Values_Changes(int Id, int Log_Id, int Vessel_ID, string Column_name)
        {
            return DAL_Tec_ErLog.Get_ERLogME_02_Values_Changes(Id, Log_Id, Vessel_ID, Column_name);
        }
        public static DataTable Get_ERLogME_TB_Values_Changes(int Id, int Log_Id, int Vessel_ID, string Column_name)
        {
            return DAL_Tec_ErLog.Get_ERLogME_TB_Values_Changes(Id, Log_Id, Vessel_ID, Column_name);
        }
        public static DataTable Get_ERLog_Details_Values_Changes(int Log_Id, int Vessel_ID, string Column_name)
        {
            return DAL_Tec_ErLog.Get_ERLog_Details_Values_Changes(Log_Id, Vessel_ID, Column_name);
        }
        public static DataTable Get_ERLog_AC_FW_MISC_Values_Changes(int Id, int Log_Id, int Vessel_ID, string Column_name)
        {
            return DAL_Tec_ErLog.Get_ERLog_AC_FW_MISC_Values_Changes(Id, Log_Id, Vessel_ID, Column_name);
        }
        public static DataTable Get_ERLog_TASG_Values_Changes(int Id, int Log_Id, int Vessel_ID, string Column_name)
        {
            return DAL_Tec_ErLog.Get_ERLog_TASG_Values_Changes(Id, Log_Id, Vessel_ID, Column_name);
        }
        public static DataTable Get_ERLog_GTREngine_Values_Changes(int Id, int Log_Id, int Vessel_ID, string Column_name)
        {
            return DAL_Tec_ErLog.Get_ERLog_GTREngine_Values_Changes(Id, Log_Id, Vessel_ID, Column_name);
        }
        public static DataTable Get_ERLog_TankLevel_Values_Changes(int Id, int Log_Id, int Vessel_ID, string Column_name)
        {
            return DAL_Tec_ErLog.Get_ERLog_TankLevel_Values_Changes(Id, Log_Id, Vessel_ID, Column_name);
        }
        public static DataSet Get_ErLogBookThresHold_EDIT(int Vessel_ID, int? Log_Id, int? Threshold_ID)
        {
            return DAL_Tec_ErLog.Get_ErLogBookThresHold_EDIT(Vessel_ID, Log_Id, Threshold_ID);
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
            return DAL_Tec_ErLog.Inser_Update_ErLogBookThresHold(ID, VESSEL_ID, ME_TEMP_EXH_Min, ME_TEMP_EXH_Max, METB_T_EXH_INLET_MIN, METB_T_EXH_INLET_MAX, METB_T_EXH_OUTLET_MIN,
               METB_T_EXH_OUTLET_Max, METB_T_EXH_AIR_IN_MIN, METB_T_EXH_AIR_OUT_MIN, METB_T_EXH_AIR_IN_Max, METB_T_EXH_AIR_OUT_Max, METB_T_SCAVENGE_MIN, METB_T_SCAVENGE_Max,
               METB_T_LO_B_MIN, METB_T_LO_B_MAX, METB_T_LO_T_Min, METB_T_LO_T_Max, METB_P_PD_AC_Min, METB_P_PD_AC_Max, ME_MB_IN_Min, ME_MB_IN_Max, ME_MB_OUT_Min,
               ME_MB_OUT_Max, ME_JC_IN_Min, ME_JC_IN_Max, ME_JC_OUT_Min, ME_JC_OUT_Max, ME_PC_IN_Min, ME_PC_IN_Max, ME_PC_OUT_Min, ME_PC_OUT_Max,
               ME_FUEL_OIL_Min, ME_FUEL_OIL_Max, ME_JC_FW_IN_min, ME_JC_FW_IN_Max, ME_JC_FW_OUT_Min, ME_JC_FW_OUT_Max, ME_LC_LO_IN_min, ME_LC_LO_IN_Max,
               ME_LC_LO_OUT_Min, ME_LC_LO_OUT_Max, ME_PC_LO_IN_min, ME_PC_LO_IN_Max, ME_PC_LO_OUT_min, ME_PC_LO_OUT_Max, ME_P_JACKET_WATER_min, ME_P_JACKET_WATER_Max,
               ME_P_BEARING_XND_LO_Min, ME_P_BEARING_XND_LO_Max, ME_P_CAMSHAFT_LO_Min, ME_P_CAMSHAFT_LO_Max, ME_P_FV_COOLING_Min, ME_P_FV_COOLING_Max, ME_P_FUEL_OIL_min,
               ME_P_FUEL_OIL_Max, ME_P_PISTON_COOLING_Min, ME_P_PISTON_COOLING_Max, ME_P_CONTROL_AIR_min, ME_P_CONTROL_AIR_Max, REF_MEAT_TEMP_Min, REF_MEAT_TEMP_Max,
               REF_FISH_TEMP_Min, REF_FISH_TEMP_Max, REF_VEG_LOBBY_TEMP_Min, REF_VEG_LOBBY_TEMP_Max, FWGEN_VACCUM_Min, FWGEN_VACCUM_Max, FWGEN_SHELL_TEMP_Min,
               FWGEN_SHELL_TEMP_Max, FWGEN_SALINITY_PPM_Min, FWGEN_SALINITY_PPM_Max, PUR_HO_TEMP_Min, PUR_HO_TEMP_Max, PUR_LO_TEMP_Min, PUR_LO_TEMP_Max, MISC_THRUST_BRG_TEMP_Min,
               MISC_THRUST_BRG_TEMP_Max, MISC_INTERM_BRG_TEMP_Min, MISC_INTERM_BRG_TEMP_Max, MISC_STERN_TUBE_OIL_TEMP_Min, MISC_STERN_TUBE_OIL_TEMP_Max, MISC_HO_SETT_1_Min,
               MISC_HO_SETT_1_Max, MISC_HO_SETT_2_Min, MISC_HO_SETT_2_Max, MISC_HO_SERV_1_Min, MISC_HO_SERV_1_Max, MISC_HO_SERV_2_Min, MISC_HO_SERV_2_Max, GE_TEMP_EXH_MAX_Min,
               GE_TEMP_EXH_MAX_Max, GE_TEMP_EXH_MIN_Min, GE_TEMP_EXH_MIN_Max, GE_TEMP_CW_IN_Min, GE_TEMP_CW_IN_Max, GE_TEMP_CW_OUT_Min, GE_TEMP_CW_OUT_Max, GE_TEMP_LO_IN_Min,
               GE_TEMP_LO_IN_Max, GE_TEMP_LO_OUT_Min, GE_TEMP_LO_OUT_Max, GE_PRESS_LO_Min, GE_PRESS_LO_Max, GE_PRESS_CW_Min, GE_PRESS_CW_Max, SG_LO_PRESS_Min,
               SG_LO_PRESS_Max, SG_LO_TEMP_Min, SG_LO_TEMP_Max, CREATED_BY);
        }
        public static int CopyERLogBookThreshold(int Vessel_ID, int copyfromvesselid, int userid)
        {
            return DAL_Tec_ErLog.CopyERLogBookThreshold(Vessel_ID, copyfromvesselid, userid);

        }



        public static DataSet ErLog_Seach_All_Details(int logId, int vessel_id, int? pagenumber, int? pagesize, ref int isfetchcount, int? PageFrom, int? PageTo, DateTime? DateFrom, DateTime? DateTo)
        {
            return DAL_Tec_ErLog.ErLog_Seach_All_Details(logId, vessel_id, pagenumber, pagesize, ref  isfetchcount, PageFrom, PageTo, DateFrom, DateTo);
        }

        public static DataSet Get_Erlog_WatchHours_Anomaly(int Vessel_ID, DateTime Log_date)
        {
            return DAL_Tec_ErLog.Get_Erlog_WatchHours_Anomaly(Vessel_ID, Log_date);
        }
        public DataTable Get_VesselList(int FleetID, int VesselID, int VesselManager, string SearchText, int UserCompanyID)
        {
            try
            {
                return DAL_Tec_ErLog.Get_VesselList_DL(FleetID, VesselID, VesselManager, SearchText, UserCompanyID, -1);
            }
            catch
            {
                throw;
            }
        }
        public static int ErLog_Update_Status(int Log_ID, int Vessel_Id, int USER_ID, string Reason)
        {
            return DAL_Tec_ErLog.ErLog_Update_Status(Log_ID, Vessel_Id, USER_ID, Reason);

        }
        public static DataTable Get_FollowupList(int? VESSEL_ID, int? LOG_ID)
        {
            return DAL_Tec_ErLog.Get_FollowupList(VESSEL_ID, LOG_ID);
        }

        public static int Update_Followup_Settings(string MAILTO, int LOGMAIL, int LOGDASH, int FOLLOWUPMAIL, int FOLLOWUPDASH, int ALERTTOMASTER, int ALERTTOCE, int CREATED_BY)
        {
            return DAL_Tec_ErLog.Update_Followup_Settings(MAILTO, LOGMAIL, LOGDASH, FOLLOWUPMAIL, FOLLOWUPDASH, ALERTTOMASTER, ALERTTOCE, CREATED_BY);
        }
        public static DataSet Get_Followup_Settings()
        {
            return DAL_Tec_ErLog.Get_Followup_Settings();
        }

    }
}
