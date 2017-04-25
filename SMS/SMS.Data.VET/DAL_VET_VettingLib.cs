using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
//using System.Configuration;

namespace SMS.Data.VET
{

    public class DAL_VET_VettingLib
    {
        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        /// <summary>
        /// To bind oil major list
        /// </summary>
        /// <returns></returns>
        public DataTable VET_Get_OilMajorList()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_OilMajorList").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// To bind Questionnaire type
        /// </summary>
        /// <returns></returns>
        public DataTable VET_Get_Module()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_Module").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To bind Vetting type
        /// </summary>
        /// <returns></returns>
        public DataTable VET_Get_VettingTypeList()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingTypeList").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// To get all Vetting Type.Bind only those vetting type 
        /// </summary>
        /// <returns></returns>
        public DataTable VET_Get_VettingTypeList_Insp()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingTypeList_Insp").Tables[0];
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Bind Vetting type according to vessel id
        /// </summary>
        /// <param name="VesselID">selected vessel id</param>
        /// <returns>returns vetting type</returns>
        public DataTable VET_Get_VettingType_BySetting(int VesselID)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@VesselID",VesselID),
               };
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingType_BySetting",obj).Tables[0];
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_InspectorList()
        {
            try
            {

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_InspectorList");

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable VET_Get_ExtInspectorList()
        {
            try
            {

                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ExtInspectorList");

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method is used to get vetting list
        /// </summary>
        /// <returns>List of Vettings</returns>
        public DataTable VET_Get_VettingList(int VesselID)
        {
            try
            {
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingList", new SqlParameter("VesselID", VesselID)).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
             

        /// <summary>
        /// Method is used to get observation list according to question
        /// </summary>
        /// <returns>List of observation</returns>
        public DataTable VET_Get_ObservationList(int Vetting_ID)
        {
            try
            {
                return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ObservationList", new SqlParameter("Vetting_ID", Vetting_ID)).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet VET_Get_VettingSetting(string searchtext, string sortby, int? sortdirection, ref int result)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),               
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)               };
            
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_GET_VETTINGSETTINGS", obj);               
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
                return ds;
            }
            catch
            {
                throw;
            }
        }
        public int VET_INS_VslStng(DataTable DtRecord, int UserId)
        {
            try
            {
                SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@VTST",DtRecord),                                           
                                            new SqlParameter("@UserID",UserId)
                                        };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_INS_VesselVettingSetting", sqlprm);
            }
            catch
            {
                throw;
            }


        }


        public DataTable GetSettingTypeByVessel(string Action, int vesselId)
        {
            try
            {
            SqlParameter[] obj = new SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Action", Action),                
                   new System.Data.SqlClient.SqlParameter("@Vessel_id", vesselId)         
                    
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_ExportVettingSetting", obj).Tables[0];
            }
            catch
            {
                throw;
            }

        }

        public DataTable GetSettingsVessel(string Action, int? vesselId)
        {
            try
            {
            SqlParameter[] obj = new SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@Action", Action),                
                   new System.Data.SqlClient.SqlParameter("@Vessel_id", vesselId)         
                    
            };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_ExportVettingSetting", obj).Tables[0];
            }
            catch
            {
                throw;
            }

        }
          

        public DataTable VET_Get_Category(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref int result)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
               };
                obj[obj.Length - 2].Direction = ParameterDirection.InputOutput;
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_GET_Categories", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 2].Value);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
            
                    return ds.Tables[0];
            }     
            catch
            {
                throw;
            }
                     
        }

        public DataTable VET_Get_VettingTypForLibrary(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   
               };
                
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingTypForLibrary", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
              

                return ds.Tables[0];
            }
            catch
            {
                throw;
            }

        }

        public int VET_INS_Categories(string CategoryName, int Created_By, ref int result)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@CategoryName",CategoryName),
                   new System.Data.SqlClient.SqlParameter("@Created_By",Created_By),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
                   
               };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_INS_Categories", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
            }
            catch (Exception ex) {
                throw;
            }
            return result;

        }

        public int VET_UPD_Categories(int CategoryId, string CategoryName, int Modified_By, ref int result)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@OBSCategory_ID",CategoryId),
                   new System.Data.SqlClient.SqlParameter("@CategoryName",CategoryName),
                   new System.Data.SqlClient.SqlParameter("@Modified_By",Modified_By),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
                   
               };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Upd_Categories", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
            }
            catch (Exception ex) {

                throw;
            }
            return result;

        }

        public int VET_DEL_Categories(int CategoryId, int Modified_By, ref int result)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@OBSCategory_ID",CategoryId),
                   new System.Data.SqlClient.SqlParameter("@Modified_By",Modified_By),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
                   
               };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Del_Categories", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
            }
            catch (Exception ex) {

                throw;
            }
            return result;

        }
        public int VET_Ins_VettingType(string Vetting_Type_Name, int ExInDays, int isInternal, int IsActive, int Created_By, int IsExApplicable, ref int result)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_Type_Name",Vetting_Type_Name),
                    new System.Data.SqlClient.SqlParameter("@ExInDays",ExInDays),
                   new System.Data.SqlClient.SqlParameter("@IsInternal",isInternal),
                   new System.Data.SqlClient.SqlParameter("@IsActive",IsActive),
                   new System.Data.SqlClient.SqlParameter("@Created_By",Created_By),
                   new System.Data.SqlClient.SqlParameter("@IsExApplicable",IsExApplicable),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
                   
               };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Ins_VettingType", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;

        }

        public int VET_Upd_VettingType(int Vetting_Type_ID, string Vetting_Type_Name, int ExInDays, int isInternal, int isActive, int Modified_By,int IsExApplicable, ref int result)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_Type_ID",Vetting_Type_ID),
                   new System.Data.SqlClient.SqlParameter("@Vetting_Type_Name",Vetting_Type_Name),
                    new System.Data.SqlClient.SqlParameter("@ExInDays",ExInDays),
                   new System.Data.SqlClient.SqlParameter("@IsInternal",isInternal),
                   new System.Data.SqlClient.SqlParameter("@IsActive",isActive),
                   new System.Data.SqlClient.SqlParameter("@Modified_By",Modified_By),
                   new System.Data.SqlClient.SqlParameter("@IsExApplicable",IsExApplicable),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
                   
               };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Upd_VettingType", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;

        }

        public int VET_Del_Vetting_Type(int Vetting_Type_ID, int Modified_By, ref int result)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_Type_ID",Vetting_Type_ID),
                   new System.Data.SqlClient.SqlParameter("@Deleted_By",Modified_By),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
                   
               };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Del_Vetting_Type", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;

        }
        public DataTable VET_Export_ObservationCategory()
        {
            System.Data.DataTable ds = new DataTable();
            try
            { ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Export_ObservationCategory", null).Tables[0]; }
            catch (Exception ex) { ds = null; }
            return ds;
        }   
        public DataTable VET_Get_ExternalInspector(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@SerchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
               };

                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ExternalInspector", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);             
                    return ds.Tables[0];
            }
            catch (Exception ex)
            { throw; }

           
        }

      

        public int VET_INS_ExternalInspector(string First_Name, string Last_Name, string Company_Name, string Document_Type, string Document_Number,DataTable  dtVetType, int Created_By, ref int result)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@First_Name",First_Name),
                   new System.Data.SqlClient.SqlParameter("@Last_Name",Last_Name),
                   new System.Data.SqlClient.SqlParameter("@Company_Name",Company_Name),
                   new System.Data.SqlClient.SqlParameter("@Document_Type",Document_Type),
                   new System.Data.SqlClient.SqlParameter("@Document_Number",Document_Number),
                   new System.Data.SqlClient.SqlParameter("@dtVetType", dtVetType),
                   new System.Data.SqlClient.SqlParameter("@Created_By",Created_By),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
                   
               };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Ins_ExternalInspector", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
            }
            catch (Exception ex) { throw;
            }
            return result;

        }

        public int VET_UPD_ExternalInspector(int InspectorId, string First_Name, string Last_Name, string Company_Name, string Document_Type, string Document_Number, DataTable dtVetType, string ImagePath, int Modified_By, ref int result)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Inspector_ID",InspectorId),
                   new System.Data.SqlClient.SqlParameter("@First_Name",First_Name),
                   new System.Data.SqlClient.SqlParameter("@Last_Name",Last_Name),
                   new System.Data.SqlClient.SqlParameter("@Company_Name",Company_Name),
                   new System.Data.SqlClient.SqlParameter("@Document_Type",Document_Type),
                   new System.Data.SqlClient.SqlParameter("@Document_Number",Document_Number),
                    new System.Data.SqlClient.SqlParameter("@dtVetType", dtVetType),
                   new System.Data.SqlClient.SqlParameter("@image",ImagePath),
                   new System.Data.SqlClient.SqlParameter("@Modified_By",Modified_By),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
                   
               };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Upd_ExternalInspector", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
            }
            catch (Exception ex) { throw; }
            return result;

        }

        public int VET_DEL_ExternalInspector(int InspectorId, int DeletedBy, ref int result)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Inspector_ID",InspectorId),
                   new System.Data.SqlClient.SqlParameter("@Deleted_By",DeletedBy),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
                   
               };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Del_ExternalInspector", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
            }
            catch (Exception ex) { throw; }
            return result;

        }

        public DataSet VET_Get_ExternalInspectorbyID(int InspectorId)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Inspector_ID",InspectorId)
               };
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ExternalInspectorbyID", obj);

               
                    return ds;
            }
            catch (Exception ex) { throw; }

        }

        public DataTable VET_Export_ExternaInspector()
        {
            System.Data.DataTable ds = new DataTable();
            try
            { ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Export_ExternaInspector", null).Tables[0];

            return ds;
            }
            catch (Exception ex) { throw; }
            
        }
        public DataTable VET_Get_VettingByPortCall()
        {
            System.Data.DataTable ds = new DataTable();
            try
            {
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingByPortCall", null).Tables[0];

                return ds;
            }
            catch (Exception ex) { throw; }

        }
        public DataTable VET_Get_ToolTipForPortCall()
        {
            System.Data.DataTable ds = new DataTable();
            try
            {
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_ToolTipForPortCall", null).Tables[0];

                return ds;
            }
            catch (Exception ex) { throw; }

        }
        public DataTable VET_Get_UserVesselList(int FleetID, int VesselID, int Vessel_Manager, string SearchText, int UserCompanyID, int IsVessel, int? UserID)
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_UserVesselList", new SqlParameter("FleetID", FleetID),
                                                                                                new SqlParameter("VesselID", VesselID),
                                                                                                new SqlParameter("Vessel_Manager", Vessel_Manager),
                                                                                                new SqlParameter("SearchText", SearchText),
                                                                                                new SqlParameter("UserCompanyID", UserCompanyID),
                                                                                                new SqlParameter("IsVessel", IsVessel),
                                                                                                new SqlParameter("UserID", UserID)
                                                                                                ).Tables[0];


        }


        public DataSet VET_Get_AutoComplete_ExtInspectorFNList(string SearchFName)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                  new System.Data.SqlClient.SqlParameter("@SearchFName",SearchFName),
               };
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_AutoComplete_ExtInspectorFNList", obj);

                return ds;
            }
            catch (Exception ex) 
            { throw ex; }

        }
        public DataSet VET_Get_AutoComplete_ExtInspectorLNList(string SearchLName)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                  new System.Data.SqlClient.SqlParameter("@SearchLName",SearchLName),
               };
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_AutoComplete_ExtInspectorLNList", obj);

                return ds;
            }
            catch (Exception ex)
            { throw ex; }

        }
        public DataTable VET_Get_VettingAttachmentTypeForLibrary(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@SearchText",searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby),
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection),
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                   
               };

                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "VET_Get_VettingAttachmentTypeForLibrary", obj);
                isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);


                return ds.Tables[0];
            }
            catch
            {
                throw;
            }

        }
        public int VET_Ins_Upd_Del_VettingTypeAttachment(int Vetting_Attachmt_Type_ID, string Vetting_Attachmt_Type_Name, int? UserId, string Action, ref int result)
        {

            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
               { 
                   new System.Data.SqlClient.SqlParameter("@Vetting_Attachmt_Type_ID",Vetting_Attachmt_Type_ID),
                   new System.Data.SqlClient.SqlParameter("@Vetting_Attachmt_Type_Name",Vetting_Attachmt_Type_Name),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserId),
                   new System.Data.SqlClient.SqlParameter("@Action",Action),
                   new System.Data.SqlClient.SqlParameter("@Result",SqlDbType.Int)
                   
               };
                obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "VET_Ins_Upd_Del_VettingTypeAttachment", obj);
                result = Convert.ToInt32(obj[obj.Length - 1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }

             
    }
}
