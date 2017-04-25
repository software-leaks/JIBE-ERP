using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.VET;
using System.Data;

namespace SMS.Business.VET
{
    public class BLL_VET_VettingLib
    {
        DAL_VET_VettingLib objVetDAL = new DAL_VET_VettingLib();

        /// <summary>
        /// To bind oil major list
        /// </summary>
        /// <returns></returns>
        public DataTable VET_Get_OilMajorList()
        {
            try
            {
                return objVetDAL.VET_Get_OilMajorList();
            }
            catch
            {
                throw;
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
                return objVetDAL.VET_Get_Module();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// To bind Vetting Type
        /// </summary>
        /// <returns></returns>
        public DataTable VET_Get_VettingTypeList()
        {
            try
            {
                return objVetDAL.VET_Get_VettingTypeList();
            }
            catch
            {
                throw;
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
                return objVetDAL.VET_Get_VettingTypeList_Insp();
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
                return objVetDAL.VET_Get_VettingType_BySetting(VesselID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Bind inspector list
        /// </summary>
        /// <returns>retrun inspector</returns>
        public DataTable VET_Get_InspectorList()
        {
            try
            {
                return objVetDAL.VET_Get_InspectorList();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Bind external inspector list
        /// </summary>
        /// <returns>return external inspector</returns>
        public DataTable VET_Get_ExtInspectorList()
        {
            try
            {
                return objVetDAL.VET_Get_ExtInspectorList();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Method is used to get vetting list
        /// </summary>
        /// <param name="VesselID">selected vessel id</param>
        /// <returns>vetting list</returns>
        public DataTable VET_Get_VettingList(int VesselID)
        {
            try
            {
                return objVetDAL.VET_Get_VettingList(VesselID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        /// <summary>
        /// Method is used to get observation list according to vetting
        /// </summary>
        /// <param name="VesselID">selected Vetting_ID</param>
        /// <returns>List of observation</returns>
        public DataTable VET_Get_ObservationList(int Vetting_ID)
        {
            try
            {
                return objVetDAL.VET_Get_ObservationList(Vetting_ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method is used to get vessel vetting settings detail
        /// </summary>
        /// <param name="searchtext">Entered serach text</param>
        /// <param name="sortby">Sort by column name</param>
        /// <param name="sortdirection">sort direction</param>
        /// <param name="result">vessel vetting setting details</param>
        /// <returns></returns>
        public DataSet VET_Get_VettingSetting(string searchtext, string sortby, int? sortdirection, ref int result)
        {
            return objVetDAL.VET_Get_VettingSetting(searchtext, sortby, sortdirection, ref result);
        }
        /// <summary>
        /// Method is used to insert vetting setting details
        /// </summary>
        /// <param name="DtRecord">vetting types</param>
        /// <param name="UserId">login user id</param>
        /// <returns>return rows affected</returns>
        public int VET_INS_VslStng(DataTable DtRecord, int UserId)
        {
            try
            {
                return objVetDAL.VET_INS_VslStng(DtRecord, UserId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Method is used to get vessel vetting type details
        /// </summary>
        /// <param name="Action">Action of filter.Value is either null or vessel</param>
        /// <param name="vesselId">selected vessel id</param>
        /// <returns>get vessel settings</returns>
        public DataTable GetSettingTypeByVessel(string Action, int vesselId)
        {
            try
            {
                return objVetDAL.GetSettingTypeByVessel(Action, vesselId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Method is used to get those vessel list whos setting with vetting type
        /// </summary>
        /// <param name="Action">action of filter</param>
        /// <param name="vesselId">selected vessel id</param>
        /// <returns>vessel list</returns>
        public DataTable GetSettingsVessel(string Action, int? vesselId)
        {
            try
            {
                return objVetDAL.GetSettingsVessel(Action, vesselId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Method is used to get category details
        /// </summary>
        /// <param name="searchtext">Entered search text</param>
        /// <param name="sortby">sort by column name</param>
        /// <param name="sortdirection">sort direction</param>
        /// <param name="pagenumber">page no.s for paging</param>
        /// <param name="pagesize">page size</param>
        /// <param name="isfetchcount"></param>
        /// <param name="result">result that is return</param>
        /// <returns>Get Vetting Type Library</returns>
        public DataTable VET_Get_VettingTypForLibrary(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            try
            {
                return objVetDAL.VET_Get_VettingTypForLibrary(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount );
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Method is used to get category details
        /// </summary>
        /// <param name="searchtext">Entered search text</param>
        /// <param name="sortby">sort by column name</param>
        /// <param name="sortdirection">sort direction</param>
        /// <param name="pagenumber">page no.s for paging</param>
        /// <param name="pagesize">page size</param>
        /// <param name="isfetchcount"></param>
        /// <param name="result">result that is return</param>
        /// <returns>category details</returns>
        public DataTable VET_Get_Category(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, ref int result)
        {

            try
            {
                return objVetDAL.VET_Get_Category(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount, ref result);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Method is used to insert category details
        /// </summary>
        /// <param name="CategoryName">Entered category name</param>
        /// <param name="Created_By">login user id </param>
        /// <param name="result">result that is return</param>
        /// <returns>return rows affected</returns>
        public int VET_INS_Categories(string CategoryName, int Created_By, ref int result)
        {
            try
            {
                return objVetDAL.VET_INS_Categories(CategoryName, Created_By, ref result);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method is used to update categories details
        /// </summary>
        /// <param name="CategoryId">Selected category id</param>
        /// <param name="CategoryName">Entered category name</param>
        /// <param name="Modified_By">Login user id</param>
        /// <param name="result"></param>
        /// <returns>return rows affected</returns>
        public int VET_UPD_Categories(int CategoryId, string CategoryName, int Modified_By, ref int result)
        {
            try
            {
                return objVetDAL.VET_UPD_Categories(CategoryId, CategoryName, Modified_By, ref result);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method is used to delete category
        /// </summary>
        /// <param name="CategoryId">Selected category</param>
        /// <param name="Modified_By">Login user id</param>
        /// <param name="result"></param>
        /// <returns>return rows affected</returns>
        public int VET_DEL_Categories(int CategoryId, int Modified_By, ref int result)
        {
            try
            {
                return objVetDAL.VET_DEL_Categories(CategoryId, Modified_By, ref result);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Method is used to insert vetting type details
        /// </summary>
        /// <param name="Vetting_Type_Name">Entered Vetting Type Name</param>
        /// <param name="Created_By">login user id </param>
        /// <param name="result">result that is return</param>
        /// <returns>return rows affected</returns>
        public int VET_Ins_VettingType(string Vetting_Type_Name, int ExInDays, int isInternal, int IsActive, int Created_By, int IsExApplicable, ref int result)
        {
            try
            {
                return objVetDAL.VET_Ins_VettingType(Vetting_Type_Name, ExInDays, isInternal, IsActive, Created_By, IsExApplicable, ref result);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method is used to update vetting type details
        /// </summary>
        /// <param name="Vetting_Type_ID">Selected Vetting Type id</param>
        /// <param name="Vetting_Type_Name">Entered Vetting Type name</param>
        /// <param name="IsInternal">1: Internal 0: External</param>
        /// <param name="Modified_By">Login user id</param>
        /// <param name="result"></param>
        /// <returns>return rows affected</returns>
        public int VET_Upd_VettingType(int Vetting_Type_ID, string Vetting_Type_Name, int ExInDays, int isInternal, int isActive, int Modified_By, int IsExApplicable, ref int result)
        {
            try
            {
                return objVetDAL.VET_Upd_VettingType(Vetting_Type_ID, Vetting_Type_Name, ExInDays, isInternal, isActive, Modified_By, IsExApplicable, ref result);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Method is used to delete vetting type
        /// </summary>
        /// <param name="Vetting_Type_ID">Selected Vetting Type_ID</param>
        /// <param name="Modified_By">Login user id</param>
        /// <param name="result"></param>
        /// <returns>return rows affected</returns>
        public int VET_Del_Vetting_Type(int Vetting_Type_ID, int Modified_By, ref int result)
        {
            try
            {
                return objVetDAL.VET_Del_Vetting_Type(Vetting_Type_ID, Modified_By, ref result);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Method is used to export observation categories
        /// </summary>
        /// <returns>no. of records </returns>
        public DataTable VET_Export_ObservationCategory()
        {
            try
            {
                return objVetDAL.VET_Export_ObservationCategory();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Method is used to get external inspector details
        /// </summary>
        /// <param name="searchtext">entered text</param>
        /// <param name="sortby">sort by column</param>
        /// <param name="sortdirection">sort direction</param>
        /// <param name="pagenumber">page nos. for pageing</param>
        /// <param name="pagesize">page size count</param>
        /// <param name="isfetchcount"></param>
        /// <returns>records</returns>
        public DataTable VET_Get_ExternalInspector(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            try
            {
                return objVetDAL.VET_Get_ExternalInspector(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            catch
            {
                throw;
            }
        }
              
        /// <summary>
        /// Method is used to insert external inspector details
        /// </summary>
        /// <param name="First_Name">eneterd first name of inspector</param>
        /// <param name="Last_Name">entered last name of inspector</param>
        /// <param name="Company_Name">entered company name </param>
        /// <param name="Document_Type">entered document type</param>
        /// <param name="Document_Number">entered document number</param>
        /// <param name="dtVetType">selected vetting type in datatable</param>
        /// <param name="Created_By">Login user id</param>   
        public int VET_INS_ExternalInspector(string First_Name, string Last_Name, string Company_Name, string Document_Type, string Document_Number, DataTable dtVetType , int Created_By, ref int result)
        {
            try
            {
                return objVetDAL.VET_INS_ExternalInspector(First_Name, Last_Name, Company_Name, Document_Type, Document_Number, dtVetType, Created_By, ref result);
            }
            catch
            {
                throw;
            }
        }
    
      /// <summary>
      /// Method is used to update external inspector details
      /// </summary>
      /// <param name="InspectorId">selected inspector for update</param>
      /// <param name="First_Name">eneterd first name of inspector</param>
      /// <param name="Last_Name">entered last name of inspector</param>
      /// <param name="Company_Name">entered company name </param>
      /// <param name="Document_Type">entered document type</param>
      /// <param name="Document_Number">entered document number</param>
      /// <param name="dtVetType">selected vetting type in datatable</param>
      /// <param name="ImagePath">uploaded image path</param>
      /// <param name="Modified_By">Login user id</param> 
        public int VET_UPD_ExternalInspector(int InspectorId, string First_Name, string Last_Name, string Company_Name, string Document_Type, string Document_Number, DataTable dtVetType, string ImagePath, int Modified_By, ref int result)
        {
            try
            {
                return objVetDAL.VET_UPD_ExternalInspector(InspectorId, First_Name, Last_Name, Company_Name, Document_Type, Document_Number, dtVetType, ImagePath, Modified_By, ref result);
            }
            catch
            {
                throw;
            }
        }
       /// <summary>
       /// Methos is used to delete external inspector
       /// </summary>
        /// <param name="InspectorId">selected inspector for delete</param>
        /// <param name="DeletedBy">Login user id</param>     
        public int VET_DEL_ExternalInspector(int InspectorId, int DeletedBy, ref int result)
        {
            try
            {
                return objVetDAL.VET_DEL_ExternalInspector(InspectorId, DeletedBy, ref result);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Methos is used to get inspector details by id
        /// </summary>
        /// <param name="InspectorId">selected inspector id</param>
        /// <returns>details of inspector</returns>
        public DataSet VET_Get_ExternalInspectorbyID(int InspectorId)
        {
            try
            {
                return objVetDAL.VET_Get_ExternalInspectorbyID(InspectorId);
            }
            catch
            {
                throw;
            }
        }

      /// <summary>
      /// Method is used to export external inspector
      /// </summary>
      /// <returns>records</returns>
        public DataTable VET_Export_ExternaInspector()
        {
            try
            {
                return objVetDAL.VET_Export_ExternaInspector();
            }
            catch
            {
                throw;
            }
        }
        public DataTable VET_Get_VettingByPortCall()
        {
            try
            {
                return objVetDAL.VET_Get_VettingByPortCall();
            }
            catch
            {
                throw;
            }
        }
        public DataTable VET_Get_ToolTipForPortCall()
        {
            try
            {
                return objVetDAL.VET_Get_ToolTipForPortCall();
            }
            catch
            {
                throw;
            }
        }

        public DataTable VET_Get_UserVesselList(int FleetID, int VesselID, int VesselManager, string SearchText, int UserCompanyID, int? UserID)
        {
            try
            {
                return objVetDAL.VET_Get_UserVesselList(FleetID, VesselID, VesselManager, SearchText, UserCompanyID, -1, UserID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// auto complete for first name
        /// </summary>
        /// <param name="SearchFName">first name</param>
        /// <returns></returns>
        public DataSet VET_Get_AutoComplete_ExtInspectorFNList(string SearchFName)
        {
            try
            {
                return objVetDAL.VET_Get_AutoComplete_ExtInspectorFNList(SearchFName);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// auto complete for last name
        /// </summary>
        /// <param name="SearchLName"> last name</param>
        /// <returns></returns>
        public DataSet VET_Get_AutoComplete_ExtInspectorLNList(string SearchLName)
        {
            try
            {
                return objVetDAL.VET_Get_AutoComplete_ExtInspectorLNList(SearchLName);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Get vetting attachment type
        /// </summary>
        /// <param name="searchtext"> search by attachment type name</param>
        /// <param name="sortby"> sort by attachment type name</param>
        /// <param name="sortdirection">ASC/DESC</param>
        /// <param name="pagenumber">page number</param>
        /// <param name="pagesize">page size</param>
        /// <param name="isfetchcount">total fetch count</param>
        /// <returns></returns>
        public DataTable VET_Get_VettingAttachmentTypeForLibrary(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            try
            {
                return objVetDAL.VET_Get_VettingAttachmentTypeForLibrary(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// insert/update/delete vetting attachment type name
        /// </summary>
        /// <param name="Vetting_Attachmt_Type_ID"> attachment type id</param>
        /// <param name="Vetting_Attachmt_Type_Name">attachment type name</param>
        /// <param name="UserId">user id </param>
        /// <param name="Action">insert: I/update: U/delete : D</param>
        /// <param name="result">return id</param>
        /// <returns></returns>
        public int VET_Ins_Upd_Del_VettingTypeAttachment(int Vetting_Attachmt_Type_ID, string Vetting_Attachmt_Type_Name, int? UserId, string Action, ref int result)
        {
            try
            {
                return objVetDAL.VET_Ins_Upd_Del_VettingTypeAttachment(Vetting_Attachmt_Type_ID, Vetting_Attachmt_Type_Name, UserId, Action, ref result);
            }
            catch
            {
                throw;
            }
        }
    }
}
