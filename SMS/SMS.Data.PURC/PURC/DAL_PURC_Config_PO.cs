using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using SMS.Properties.PURC;


namespace SMS.Data.PURC
{
    public class DAL_PURC_Config_PO
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public DAL_PURC_Config_PO()
        { }
        public string PURC_Save_Config_DAL(POConfig POconfig, DataTable ConfigPOTable)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                      new SqlParameter("@id",POconfig.ID),
                                                      new SqlParameter("@PO_Type",POconfig.POType),
                                                      new SqlParameter("@Auto_Owner_Selection",POconfig.Owner),
                                                      new SqlParameter("@Delivery_Port",POconfig.Delivery_Port),
                                                      new SqlParameter("@Delivery_Date",POconfig.Delivery_Port_date),
                                                      new SqlParameter("@Vessel_Movement_Date",POconfig.Vessel_movement_Date),
                                                      new SqlParameter("@Item_Category",POconfig.Item_Category),
                                                      new SqlParameter("@Direct_Quotation",POconfig.Quote_required),
                                                      new SqlParameter("@Min_QTN_Required",POconfig.QuoteNo),
                                                      new SqlParameter("@Vessel_Processing_PO",POconfig.Vessel_Processing_PO),
                                                      new SqlParameter("@Free_Text_Items_Addition",POconfig.Enable_Free_text),
                                                      new SqlParameter("@Copy_To_Vessel",POconfig.copy_to_vessel),
                                                      new SqlParameter("@Supplier_PO_Confirmation",POconfig.Sup_Po_Confirmation),
                                                      new SqlParameter("@Vessel_Delivery_Confirmation",POconfig.Vessel_Delivery_Confirm),
                                                      new SqlParameter("@Office_Delivery_Confirmation",POconfig.Office_Delivery_Confirmation),
                                                      new SqlParameter("@Witholding_Tax",POconfig.Withhold_tax),
                                                      new SqlParameter("@VAT_Config_By_Purchaser",POconfig.Vat_Config_Purc),
                                                      new SqlParameter("@Verification_Required",POconfig.require_verify),
                                                      new SqlParameter("@Auto_PO_Closing",POconfig.Auto_POClosing),
                                                      new SqlParameter("@Currentuser",POconfig.Currentuser),
                                                      new SqlParameter("@ConfigPOTable",ConfigPOTable),
                                                      new SqlParameter("@Result",SqlDbType.VarChar)
                                                     };
            prm[prm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_INS_UPD_POConfig", prm);
            return prm[prm.Length - 1].Value.ToString();


        }
        public string PURC_Update_Config_DAL(POConfig POconfig, DataTable ConfigPOTable)
        {
            SqlParameter[] prm = new SqlParameter[] {
                                                      new SqlParameter("@id",POconfig.ID),
                                                      new SqlParameter("@PO_Type",POconfig.POType),
                                                      new SqlParameter("@Auto_Owner_Selection",POconfig.Owner),
                                                      new SqlParameter("@Delivery_Port",POconfig.Delivery_Port),
                                                      new SqlParameter("@Delivery_Date",POconfig.Delivery_Port_date),
                                                      new SqlParameter("@Vessel_Movement_Date",POconfig.Vessel_movement_Date),
                                                      new SqlParameter("@Item_Category",POconfig.Item_Category),
                                                      new SqlParameter("@Direct_Quotation",POconfig.Quote_required),
                                                      new SqlParameter("@Min_QTN_Required",POconfig.QuoteNo),
                                                      new SqlParameter("@Vessel_Processing_PO",POconfig.Vessel_Processing_PO),
                                                      new SqlParameter("@Free_Text_Items_Addition",POconfig.Enable_Free_text),
                                                      new SqlParameter("@Copy_To_Vessel",POconfig.copy_to_vessel),
                                                      new SqlParameter("@Supplier_PO_Confirmation",POconfig.Sup_Po_Confirmation),
                                                      new SqlParameter("@Vessel_Delivery_Confirmation",POconfig.Vessel_Delivery_Confirm),
                                                      new SqlParameter("@Office_Delivery_Confirmation",POconfig.Office_Delivery_Confirmation),
                                                      new SqlParameter("@Witholding_Tax",POconfig.Withhold_tax),
                                                      new SqlParameter("@VAT_Config_By_Purchaser",POconfig.Vat_Config_Purc),
                                                      new SqlParameter("@Verification_Required",POconfig.require_verify),
                                                      new SqlParameter("@Auto_PO_Closing",POconfig.Auto_POClosing),
                                                      new SqlParameter("@Currentuser",POconfig.Currentuser),
                                                      new SqlParameter("@ConfigPOTable",ConfigPOTable),
                                                      new SqlParameter("@Result",SqlDbType.VarChar)
                                                     };
            prm[prm.Length - 1].Direction = ParameterDirection.Input;
            SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_POConfig", prm);
            return prm[prm.Length - 1].Value.ToString();


        }
        public int PURC_Delete_Config_DAL(POConfig POconfig)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] sqlpram = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new SqlParameter("@ID",POconfig.ID),
                                                      new SqlParameter("@DELETED_BY",POconfig.Currentuser)
            };

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_DEL_POConfig]", sqlpram);
                return 0;
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        public DataSet Get_ddlList_DAL()
        {
            
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Config_ddlList");

            return ds;

        }


        public DataTable PURC_POConfigSearch_DAL(string searchtext,
         string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_POConfig_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

        public DataSet PURC_Get_Config_DAL(string potype)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@POType", potype)
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.Input;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_POConfig", obj);

            return ds;

        }
        public DataSet Purc_Get_ConfiguredPO_type(string userid, string Reqsn_Types)
        {
             System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@reqsnTypes", Reqsn_Types),
                   new System.Data.SqlClient.SqlParameter("@UserID", userid)
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.Input;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Purc_Get_Configured_AccountCode", obj);

            return ds;
            
        }
        public DataSet PURC_Get_Configured_ReqsnType()
        {
             System.Data.DataTable dt = new System.Data.DataTable();


             System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Purc_Get_Configured_ReqsnType", null);

            return ds;
            
        }
        public DataSet PURC_Get_Configured_Functions(string Reqsn_type)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@reqsnTypes", Reqsn_type)
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.Input;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "Purc_Get_Configured_Functions", obj);

            return ds;
        }
    }
}
