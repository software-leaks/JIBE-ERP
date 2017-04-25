using System;
using SMS.Data.INFRA;
using System.Data;

namespace SMS.Business.INFRA
{
    public class BLL_Infra_SynchronizerSettings
    {

        public static DataTable Get_Office_Import_Attachment_Rules()
        {
            return DAL_Infra_SynchronizerSettings.Get_Office_Import_Attachment_Rules();
        }

        public static int Upd_Office_Import_Attachment_Rules(int Rule_ID, string ATTACH_PREFIX, string ATTACH_PATH, int User_ID)
        {
            return DAL_Infra_SynchronizerSettings.Upd_Office_Import_Attachment_Rules(Rule_ID, ATTACH_PREFIX, ATTACH_PATH, User_ID);
        }

        public static int Del_Office_Import_Attachment_Rules(int Rule_ID, int User_ID)
        {
            return DAL_Infra_SynchronizerSettings.Del_Office_Import_Attachment_Rules(Rule_ID, User_ID);
        }

        public static DataTable Get_Office_Sync_Setting(string Setting_Key, int Page_Index, int Page_Size, ref int is_Fetch_Count)
        {
            return DAL_Infra_SynchronizerSettings.Get_Office_Sync_Setting(Setting_Key, Page_Index, Page_Size,ref is_Fetch_Count);
        }

        public static int Upd_Office_Sync_Setting(int Setting_ID, string Setting_Key, string Setting_Value, int User_ID)
        {
            return DAL_Infra_SynchronizerSettings.Upd_Office_Sync_Setting(Setting_ID, Setting_Key, Setting_Value, User_ID);
        }

        public static int Del_Office_Sync_Setting(int Setting_ID, int User_ID)
        {
            return DAL_Infra_SynchronizerSettings.Del_Office_Sync_Setting(Setting_ID, User_ID);
        }


        public static DataTable Get_Vessel_Import_Attachment_Rules()
        {
            return DAL_Infra_SynchronizerSettings.Get_Vessel_Import_Attachment_Rules();
        }

        public static int Upd_Vessel_Import_Attachment_Rules(int Rule_ID, string ATTACH_PREFIX, string ATTACH_PATH, int User_ID)
        {
            return DAL_Infra_SynchronizerSettings.Upd_Vessel_Import_Attachment_Rules(Rule_ID, ATTACH_PREFIX, ATTACH_PATH, User_ID);
        }

        public static int Del_Vessel_Import_Attachment_Rules(int Rule_ID, int User_ID)
        {
            return DAL_Infra_SynchronizerSettings.Del_Vessel_Import_Attachment_Rules(Rule_ID, User_ID);
        }

    }
}
