using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SMS.Data.INFRA
{
    public class DAL_Infra_SynchronizerSettings
    {

        static string _connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public static DataTable Get_Office_Import_Attachment_Rules()
        {
            return SqlHelper.ExecuteDataset(_connection, CommandType.StoredProcedure, "SYNC_Get_Import_Attachments_Rule").Tables[0];
        }

        public static int Upd_Office_Import_Attachment_Rules(int Rule_ID, string ATTACH_PREFIX, string ATTACH_PATH, int User_ID)
        {
            SqlParameter[] SqlPrm = new SqlParameter[] { new SqlParameter("@Rule_ID", Rule_ID),
                                                        new SqlParameter("@ATTACH_PREFIX",ATTACH_PREFIX),
                                                        new SqlParameter("@ATTACH_PATH",ATTACH_PATH),
                                                        new SqlParameter("@User_ID",User_ID)
                                                       };

            return SqlHelper.ExecuteNonQuery(_connection, CommandType.StoredProcedure, "SYNC_UPD_IMPORT_ATTACHMENTS_RULE",SqlPrm);
        }

        public static int Del_Office_Import_Attachment_Rules(int Rule_ID, int User_ID)
        {
            SqlParameter[] SqlPrm = new SqlParameter[] { new SqlParameter("@Rule_ID", Rule_ID),
                                                        new SqlParameter("@User_ID",User_ID)
                                                       };

            return SqlHelper.ExecuteNonQuery(_connection, CommandType.StoredProcedure, "SYNC_DEL_IMPORT_ATTACHMENTS_RULE",SqlPrm);
        }


        public static DataTable Get_Office_Sync_Setting(string Setting_Key,int Page_Index, int Page_Size, ref int is_Fetch_Count)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                      new SqlParameter("@SETTING_KEY",Setting_Key),
                                                      new SqlParameter("@PAGE_INDEX",Page_Index),
                                                      new SqlParameter("@PAGE_SIZE",Page_Size),
                                                      new SqlParameter("@IS_FETCH_COUNT",is_Fetch_Count),

                                                    };
            prm[prm.Length - 1].Direction = ParameterDirection.InputOutput;

            DataTable dt= SqlHelper.ExecuteDataset(_connection, CommandType.StoredProcedure, "SYNC_GET_OFFICE_SYNC_SETTING",prm).Tables[0];
            is_Fetch_Count= Convert.ToInt32(prm[prm.Length - 1].Value);
            return dt;


        }

        public static int Upd_Office_Sync_Setting(int Setting_ID, string Setting_Key, string Setting_Value, int User_ID)
        {
            SqlParameter[] SqlPrm = new SqlParameter[] { new SqlParameter("@SETTING_ID", Setting_ID),
                                                        new SqlParameter("@SETTING_KEY",Setting_Key),
                                                        new SqlParameter("@SETTING_VALUE",Setting_Value),
                                                        new SqlParameter("@USER_ID",User_ID)
                                                       };

            return SqlHelper.ExecuteNonQuery(_connection, CommandType.StoredProcedure, "SYNC_UPD_OFFICE_SYNC_SETTING", SqlPrm);
        }

        public static int Del_Office_Sync_Setting(int Setting_ID, int User_ID)
        {
            SqlParameter[] SqlPrm = new SqlParameter[] { new SqlParameter("@SETTING_ID", Setting_ID),
                                                        new SqlParameter("@USER_ID",User_ID)
                                                       };

            return SqlHelper.ExecuteNonQuery(_connection, CommandType.StoredProcedure, "SYNC_DEL_OFFICE_SYNC_SETTING", SqlPrm);
        }


        public static DataTable Get_Vessel_Import_Attachment_Rules()
        {
            return SqlHelper.ExecuteDataset(_connection, CommandType.StoredProcedure, "SYNC_Get_Vessel_Import_Attachments_Rule").Tables[0];
        }

        public static int Upd_Vessel_Import_Attachment_Rules(int Rule_ID, string ATTACH_PREFIX, string ATTACH_PATH, int User_ID)
        {
            SqlParameter[] SqlPrm = new SqlParameter[] { new SqlParameter("@Rule_ID", Rule_ID),
                                                        new SqlParameter("@ATTACH_PREFIX",ATTACH_PREFIX),
                                                        new SqlParameter("@ATTACH_PATH",ATTACH_PATH),
                                                        new SqlParameter("@User_ID",User_ID)
                                                       };

            return SqlHelper.ExecuteNonQuery(_connection, CommandType.StoredProcedure, "SYNC_UPD_Vessel_IMPORT_ATTACHMENTS_RULE", SqlPrm);
        }

        public static int Del_Vessel_Import_Attachment_Rules(int Rule_ID, int User_ID)
        {
            SqlParameter[] SqlPrm = new SqlParameter[] { new SqlParameter("@Rule_ID", Rule_ID),
                                                        new SqlParameter("@User_ID",User_ID)
                                                       };

            return SqlHelper.ExecuteNonQuery(_connection, CommandType.StoredProcedure, "SYNC_DEL_Vessel_IMPORT_ATTACHMENTS_RULE", SqlPrm);
        }

    }
}
