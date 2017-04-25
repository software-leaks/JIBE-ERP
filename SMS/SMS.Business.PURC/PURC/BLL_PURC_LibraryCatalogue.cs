using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using SMS.Data.PURC;
using SMS.Properties;
using SMS.Data;


namespace SMS.Business.PURC
{

    public partial class BLL_PURC_Purchase
    {
        DAL_PURC_LibraryCatalogue objLibCat = new DAL_PURC_LibraryCatalogue();


        public DataSet LibraryCatalogueList(string systemid)
        {
            return objLibCat.LibraryCatalogueList(systemid);
        }


        public DataSet LibraryCatalogueSearch(string systemcode, string systemdesc, string deptType, string deptcode, int vesselcode, string makercode
           , int? IsActive, string sortby, int? sortdirection, int pagenumber, int pagesize, int isfetchcount)
        {
            return objLibCat.LibraryCatalogueSearch(systemcode, systemdesc, deptType, deptcode, vesselcode, makercode
                , IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount);
        }


        public DataSet LibraryCatalogueSearch(string systemcode, string systemdesc, string deptType, string deptcode, int fleetid, int vesselcode, string makercode
           , int? IsActive, string sortby, int? sortdirection, int pagenumber, int pagesize, int isfetchcount)
        {
            return objLibCat.LibraryCatalogueSearch(systemcode, systemdesc, deptType, deptcode, fleetid, vesselcode, makercode
                , IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount);
        }


        public DataSet LibraryCatalogueSearch_PMS(string systemcode, string systemdesc, string deptType, int? Function_ID, int fleetid, int vesselcode, string makercode
         , int? IsActive, string sortby, int? sortdirection, int pagenumber, int pagesize, int isfetchcount)
        {
            return objLibCat.LibraryCatalogueSearch_PMS(systemcode, systemdesc, deptType, Function_ID, fleetid, vesselcode, makercode
                , IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount);
        }


        public int LibraryCatalogueSave(int userid, string systemcode, string systemdesc, string systemparticular, string maker, string setinstalled
            , string model, string dept, string vesselid, string functionid, string accountcode)
        {
            return objLibCat.LibraryCatalogueSave(userid, systemcode, systemdesc, systemparticular, maker, setinstalled, model, dept, vesselid, functionid, accountcode);
        }


        public int PMS_Insert_NewSystem(int userid, string systemcode, string systemdesc, string systemparticular, string maker, string setinstalled
        , string model, string dept, string vesselid, string functionid, string accountcode, int? AddSubSysFlag, string SerialNumber, int IsLocationRequired, int Run_Hour, int Critical, string DeptType)
        {
            return objLibCat.PMS_Insert_NewSystem(userid, systemcode, systemdesc, systemparticular, maker, setinstalled, model, dept, vesselid, functionid, accountcode, AddSubSysFlag, SerialNumber, IsLocationRequired, Run_Hour, Critical, DeptType);
        }
        //Added by prashant ("ServiceAccount" added)
        public int PMS_Insert_NewSystem(int userid, string systemcode, string systemdesc, string systemparticular, string maker, string setinstalled
        , string model, string dept, string vesselid, string functionid, string accountcode, int? AddSubSysFlag, string SerialNumber, int IsLocationRequired, int Run_Hour, int Critical, string DeptType, string ServiceAccount)
        {
            return objLibCat.PMS_Insert_NewSystem(userid, systemcode, systemdesc, systemparticular, maker, setinstalled, model, dept, vesselid, functionid, accountcode, AddSubSysFlag, SerialNumber, IsLocationRequired, Run_Hour, Critical, DeptType,ServiceAccount);
        } 

        public int LibraryCatalogueSave(int userid, string systemcode, string systemdesc, string systemparticular, string maker, string setinstalled
        , string model, string dept, string vesselid, string functionid, string accountcode, int? AddSubSysFlag, string SerialNumber, int IsLocationRequired, int Run_Hour, int Critical)
        {
            return objLibCat.LibraryCatalogueSave(userid, systemcode, systemdesc, systemparticular, maker, setinstalled, model, dept, vesselid, functionid, accountcode, AddSubSysFlag, SerialNumber, IsLocationRequired, Run_Hour, Critical);
        }

        public int PMS_Update_System(int userid, int systemid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
            , string dept, string vesselid, string functionid, string accountcode, string SerialNumber, int Run_Hour, int Critical)
        {
            return objLibCat.PMS_Update_System(userid, systemid, systemcode, systemdesc, stystemparticular, maker, setinstalled, model, dept, vesselid, functionid, accountcode, SerialNumber, Run_Hour, Critical);
        }
        //Added by prashant ("ServiceAccount" added)
        public int PMS_Update_System(int userid, int systemid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
            , string dept, string vesselid, string functionid, string accountcode, string SerialNumber, int Run_Hour, int Critical, string ServiceAccount)
        {
            return objLibCat.PMS_Update_System(userid, systemid, systemcode, systemdesc, stystemparticular, maker, setinstalled, model, dept, vesselid, functionid, accountcode, SerialNumber, Run_Hour, Critical, ServiceAccount);
        }
        public int LibraryCatalogueUpdate(int userid, int systemid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
          , string dept, string vesselid, string functionid, string accountcode, string SerialNumber, int Run_Hour, int Critical)
        {
            return objLibCat.LibraryCatalogueUpdate(userid, systemid, systemcode, systemdesc, stystemparticular, maker, setinstalled, model, dept, vesselid, functionid, accountcode, SerialNumber, Run_Hour, Critical);
        }
        public int LibraryCatalogueDelete(int userid, int systemid)
        {
            return objLibCat.LibraryCatalogueDelete(userid, systemid);
        }


        public int LibraryCatalogueRestore(int userid, int systemid)
        {
            return objLibCat.LibraryCatalogueRestore(userid, systemid);
        }

        public int PMS_Get_SystemStatus(int systemid)
        {

            return objLibCat.PMS_Get_SystemStatus(systemid);
        }

        public int PMS_Get_SubSystemStatus(int subsystemid)
        {

            return objLibCat.PMS_Get_SubSystemStatus(subsystemid);
        }

        public DataSet LibrarySubCatalogueList(string subsystemid)
        {

            return objLibCat.LibrarySubCatalogueList(subsystemid);
        }


        public DataSet LibrarySubCatalogueSearch(string systemcode, string subsystemdesc, string subsystemparticular
            , int? IsActive, string sortby, int? sortdirection, int pagenumber, int pagesize, int? isfetchcount)
        {
            return objLibCat.LibrarySubCatalogueSearch(systemcode, subsystemdesc, subsystemparticular, IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount);
        }
        public int PMS_Insert_NewSubSystem(int userid, string systemcode, string substystemdesc, string subsytemparticular, string Maker, string Model, string SerialNo, int IsLocationRequired, int Run_Hour, int Critical, string setInstalled)
        {

            return objLibCat.PMS_Insert_NewSubSystem(userid, systemcode, substystemdesc, subsytemparticular, Maker, Model, SerialNo, IsLocationRequired, Run_Hour, Critical, setInstalled);
        }
        public int LibrarySubCatalogueSave(int userid, string systemcode, string subsystemcode, string substystemdesc, string subsytemparticular, string Maker, string Model, string SerialNo, int IsLocationRequired, int Run_Hour, int Critical, int Copy_Run_Hour)
        {

            return objLibCat.LibrarySubCatalogueSave(userid, systemcode, subsystemcode, substystemdesc, subsytemparticular, Maker, Model, SerialNo, IsLocationRequired, Run_Hour, Critical, Copy_Run_Hour);
        }

        public int LibrarySubCatalogueUpdate(int userid, int subsystemid, string systemcode, string subsystemcode, string substystemdesc, string subsytemparticular, string Maker, string Model, string SerialNo, int Run_Hour, int Critical, int Copy_Run_Hour)
        {
            return objLibCat.LibrarySubCatalogueUpdate(userid, subsystemid, systemcode, subsystemcode, substystemdesc, subsytemparticular, Maker, Model, SerialNo, Run_Hour, Critical, Copy_Run_Hour);
        }
        public int PMS_Update_SubSystem(int userid, int subsystemid, string systemcode, string subsystemcode, string substystemdesc, string subsytemparticular, string Maker, string Model, string SerialNo, int Run_Hour, int Critical, string setinstalled)
        {
            return objLibCat.PMS_Update_SubSystem(userid, subsystemid, systemcode, subsystemcode, substystemdesc, subsytemparticular, Maker, Model, SerialNo, Run_Hour, Critical, setinstalled);
        }

        public int LibrarySubCatalogueDelete(int userid, int subsystemid)
        {
            return objLibCat.LibrarySubCatalogueDelete(userid, subsystemid);
        }


        public int LibrarySubCatalogueRestore(int userid, int subsystemid)
        {
            return objLibCat.LibrarySubCatalogueRestore(userid, subsystemid);
        }


        public DataSet LibraryItemList(string itemid)
        {
            return objLibCat.LibraryItemList(itemid);
        }

        public DataSet LibraryItemSearch(string systemcode, string subsystemcode, int? vesselid, string partnumber, string drawingnumber, string name, string longdesc,
           int? IsActive, string sortby, int? sortdirection, int pagenumber, int pagesize, int? isfetchcount, out int rowcount)
        {

            return objLibCat.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount);
        }
        public DataSet LibraryItemSearch(string systemcode, string subsystemcode, int? vesselid, string partnumber, string drawingnumber, string name, string longdesc,
          int? IsActive, string searchtext, string sortby, int? sortdirection, int pagenumber, int pagesize, int? isfetchcount, out int rowcount)
        {

            return objLibCat.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, searchtext, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount);
        }


        public DataSet LibraryALLItemSearch(string systemcode, string subsystemcode, int? vesselid, string partnumber, string drawingnumber, string name, string longdesc,
        int? IsActive, string sortby, int? sortdirection, int pagenumber, int pagesize, int? isfetchcount, out int rowcount)
        {

            return objLibCat.LibraryALLItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount);

        }


        public string LibraryItemSave(int userid, string systemcode, string subsystemcode, string partnumber, string name, string description, string drawingnumber,
                 string unit, decimal? inventorymin, decimal? inverntorymax, string vesselid, int? itemcategory, string image_url, string product_details, int? critical_flag)
        {

            return objLibCat.LibraryItemSave(userid, systemcode, subsystemcode, partnumber, name, description, drawingnumber, unit, inventorymin, inverntorymax, vesselid, itemcategory, image_url, product_details, critical_flag);
        }
        public string LibraryItemSave(int userid, string systemcode, string subsystemcode, string partnumber, string name, string description, string drawingnumber,
                 string unit, decimal? inventorymin, decimal? inverntorymax, string vesselid, int? itemcategory, string image_url, string product_details, int? critical_flag,Boolean Catalogue_Item)
        {

            return objLibCat.LibraryItemSave(userid, systemcode, subsystemcode, partnumber, name, description, drawingnumber, unit, inventorymin, inverntorymax, vesselid, itemcategory, image_url, product_details, critical_flag, Catalogue_Item);
        }
        public int LibraryItemUpdate(int userid, string itemid, string systemcode, string subsystemcode, string partnumber, string name, string description, string drawingnumber,
             string unit, decimal? inventorymin, decimal? inventorymax, string vesselid, int? itemcategory, string image_url, string product_details, int? critical_flag)
        {

            return objLibCat.LibraryItemUpdate(userid, itemid, systemcode, subsystemcode, partnumber, name, description, drawingnumber, unit, inventorymin, inventorymax, vesselid, itemcategory, image_url, product_details, critical_flag);
        }

        public int LibraryItemDelete(int userid, string itemid)
        {
            return objLibCat.LibraryItemDelete(userid, itemid);
        }



        public int LibraryItemRestore(int userid, string itemid)
        {
            return objLibCat.LibraryItemRestore(userid, itemid);
        }

        public DataTable LibraryGetSystemParameterList(string parenttypecode, string searchtext)
        {
            return objLibCat.LibraryGetSystemParameterList(parenttypecode, searchtext);
        }

        public DataTable LibraryGetNextSystemCode()
        {
            return objLibCat.LibraryGetNextSystemCode();
        }


        public DataTable GET_ROB_Less_Min_Quantity_Search(string searchtext, string Form_Type, int? VESSEL_ID, string System_Code
                  , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objLibCat.GET_ROB_Less_Min_Quantity_Search(searchtext, Form_Type, VESSEL_ID, System_Code, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }



        public DataTable GET_SYSTEM_LOCATION(int Function, int VESSEL_ID)
        {

            return objLibCat.GET_SYSTEM_LOCATION(Function, VESSEL_ID);
        }


        public DataTable GET_SUBSYTEMSYSTEM_LOCATION(string SYSTEMCODE, int? SUBSYSTEMID, int VESSEL_ID)
        {
            return objLibCat.GET_SUBSYTEMSYSTEM_LOCATION(SYSTEMCODE, SUBSYSTEMID, VESSEL_ID);

        }


        public DataTable GET_Job_Done_History_List(int? Job_ID, int? Vessel_ID)
        {
            return objLibCat.GET_Job_Done_History_List(Job_ID, Vessel_ID);

        }

        public DataTable GET_SUBSYTEMSYSTEM_ASSIGNED_LOCATION(string SYSTEMCODE, int? SUBSYSTEMID, int VESSEL_ID)
        {
            return objLibCat.GET_SUBSYTEMSYSTEM_ASSIGNED_LOCATION(SYSTEMCODE, SUBSYSTEMID, VESSEL_ID);

        }



        public int INSERT_ADHOC_JOB(int? Job_History_id, int? Office_ID, int Vessel_ID, int Function_ID, int Location_ID, int System_ID, int SubSystem_ID, int SubSystem_Location_ID, string Job_Short_Description
           , string Job_Long_Description, string Job_Status, DateTime? Date_Done, int Created_By
           , int? PIC, int? Assigner, int? Defer_to_DryDock, string Priority, int? Inspector, DateTime? Inspection_Date, int? Dept_On_Ship, int? Dept_on_Office
           , DateTime? Expected_completion, DateTime? Completed_on, int PrimaryCategory, int SecondaryCategory, int? PSC_SIRE, string REQUISITION_CODE, int? IsSafetyAlam, int? IsCalibration
            , int? IsFunctional, string Effect, decimal? FunctionalDecimal,int? Unit, int? JobWorkType, ref int Return_ID)
        {


            return objLibCat.INSERT_ADHOC_JOB(Job_History_id, Office_ID, Vessel_ID, Function_ID, Location_ID, System_ID, SubSystem_ID, SubSystem_Location_ID, Job_Short_Description
             , Job_Long_Description, Job_Status, Date_Done, Created_By
             , PIC, Assigner, Defer_to_DryDock, Priority, Inspector, Inspection_Date, Dept_On_Ship, Dept_on_Office, Expected_completion, Completed_on, PrimaryCategory, SecondaryCategory, PSC_SIRE, REQUISITION_CODE
             , IsSafetyAlam, IsCalibration, IsFunctional, Effect, FunctionalDecimal,Unit, JobWorkType, ref Return_ID);

        }


        public int Verify_By_office_Adhoc_Job(int? Job_History_id, int Vessel_ID, int Office_ID, int Created_By)
        {
            return objLibCat.Verify_By_office_Adhoc_Job(Job_History_id, Vessel_ID, Office_ID, Created_By);
        }


        public int Rework_Adhoc_Job(int? Job_History_id, int Vessel_ID, int Office_ID, int Created_By)
        {
            return objLibCat.Rework_Adhoc_Job(Job_History_id, Vessel_ID, Office_ID, Created_By);
        }


        public DataTable GET_Adhoc_Job_Search(string searchtext, int? FleetID, int? Vessel_ID, int? Location_ID, int? System_ID, int? SubSystem_ID, int? SubSysLoc_ID, string Job_Status
                    , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objLibCat.GET_Adhoc_Job_Search(searchtext, FleetID, Vessel_ID, Location_ID, System_ID, SubSystem_ID, SubSysLoc_ID, Job_Status, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }
        public DataTable GET_Adhoc_Job_Search(string searchtext, int? FleetID, int? Vessel_ID, int? Location_ID, int? System_ID, int? SubSystem_ID, string Job_Status
                   , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objLibCat.GET_Adhoc_Job_Search(searchtext, FleetID, Vessel_ID, Location_ID, System_ID, SubSystem_ID, Job_Status, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }
        public DataTable GET_Adhoc_Job_Details(int? Job_Histroy_Id, int? Vessel_ID, int? Office_ID, int Job_ID)
        {
            return objLibCat.GET_Adhoc_Job_Details(Job_Histroy_Id, Vessel_ID, Office_ID, Job_ID);
        }

        public DataTable GET_JOB_DONE_ATTACHMENT(int VESSEL_ID, int? Office_ID, int? JOB_HISTORY_ID, int? JH_Office_ID)
        {

            return objLibCat.GET_JOB_DONE_ATTACHMENT(VESSEL_ID, Office_ID, JOB_HISTORY_ID, JH_Office_ID);
        }


        public int DELETE_JOB_DONE_ATTACHMENT(int ID, int Vessel_ID, int JOB_HISTORY_ID, int Office_ID, int Created_By)
        {

            return objLibCat.DELETE_JOB_DONE_ATTACHMENT(ID, Vessel_ID, JOB_HISTORY_ID, Office_ID, Created_By);


        }


        public int INSERT_JOB_DONE_ATTACHMENT(int Vessel_ID, int JOB_HISTORY_ID, int Office_ID, string ATTACHMENT_NAME, string ATTACHMENT_PATH, int SIZE, int Created_By, int? JH_Office_ID)
        {

            return objLibCat.INSERT_JOB_DONE_ATTACHMENT(Vessel_ID, JOB_HISTORY_ID, Office_ID, ATTACHMENT_NAME, ATTACHMENT_PATH, SIZE, Created_By, JH_Office_ID);

        }


        public DataTable GET_JOB_HISTORY_REMARKS(int VESSEL_ID, int? Office_ID, int? JOB_HISTORY_ID, int? JH_Office_ID)
        {
            return objLibCat.GET_JOB_HISTORY_REMARKS(VESSEL_ID, Office_ID, JOB_HISTORY_ID, JH_Office_ID);
        }


        public int INSERT_JOB_HISTORY_REMARKS(int Vessel_ID, int History_Office_ID, int History_ID, string Remark, int Created_By, int? Office_ID)
        {
            return objLibCat.INSERT_JOB_HISTORY_REMARKS(Vessel_ID, History_Office_ID, History_ID, Remark, Created_By, Office_ID);

        }

        public int Delete_JOB_HISTORY_REMARKS(int Vessel_ID, int Office_ID, int History_ID, int Created_By)
        {
            return objLibCat.Delete_JOB_HISTORY_REMARKS(Vessel_ID, Office_ID, History_ID, Created_By);

        }

        public DataSet Get_CriticalEquipmentIndex(int? Vessel_ID, int? Page_Index, int? Page_Size, ref int is_Fetch_Count)
        {
            return objLibCat.Get_CriticalEquipmentIndex(Vessel_ID, Page_Index, Page_Size, ref is_Fetch_Count);
        }

        /*Added By Pranali_12/03/2015*/
        #region
        public int INSERT_AUTOMATIC_REQUISTION(int Company_Code, Boolean IsAutoReqsn,Boolean IsReqSupplier_Confirm)
        {
            return objLibCat.INSERT_AUTOMATIC_REQUISTION(Company_Code, IsAutoReqsn,IsReqSupplier_Confirm);
        }
        public DataTable GET_AUTOMATIC_REQUISTION(int Company_Code)
        {
            return objLibCat.GET_AUTOMATIC_REQUISTION(Company_Code);
        }
        #endregion
        /*Added by pranali_14042015 for MeatItem Setting*/
        public DataTable GetMeatItemSetting()
        {
            return objLibCat.GetMeatItemSetting();
        }
        public void UpdateMeatItemSetting(string MeatAllowance, string MeatLimit, int USERID)
        {
            objLibCat.UpdateMeatItemSetting(MeatAllowance, MeatLimit, USERID);
        }
        public DataTable GetRequisitionList(int Vessel_ID)
        {
            return objLibCat.GetRequisitionList(Vessel_ID);
        }

        #region Gesco - EO Alarm Changes 
        public DataTable LibraryGetAlarmUnit()
        {
            return objLibCat.LibraryGetAlarmUnit();
        }
        public DataTable LibraryGetAlarmEffect()
        {
            return objLibCat.LibraryGetAlarmEffect();
        }
        #endregion

        #region Purchase Crew Settings
        public int INS_Rank_Purc(int RankID, string Mode, int UserID)
        {
            try
            {
                return objLibCat.INS_Rank_Purc(RankID, Mode, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_Purc_Rank()
        {
            try
            {
                return objLibCat.Get_Purc_Rank();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        public string Get_Department(string SystemID)
        {
            string Dept = "";
            try
            {
                Dept = objLibCat.Get_Department(SystemID);
            }
            catch
            {
                throw;
            }
            return Dept;
        }
        public DataSet Get_Purc_LIB_ItemCategory(string searchtext, string Reqsn_Type, string id , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objLibCat.Get_Purc_LIB_ItemCategory(searchtext != "" ? searchtext : null, Reqsn_Type != "" ? Reqsn_Type : null, id, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            
        }
        public int PURC_INS_UPD_ItemCategory(string CatName,string CatShortName,string Cat_Type,string Cat_id,string user,int del)
        {
            return objLibCat.PURC_INS_UPD_ItemCategory(CatName, CatShortName,Cat_Type, Cat_id, user, del);
        }
        public DataSet Get_Purc_LIB_Functions(string searchtext, string Reqsn_Type, string id, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objLibCat.Get_Purc_LIB_Functions(searchtext != "" ? searchtext : null, Reqsn_Type != "" ? Reqsn_Type : null, id != "" ? id : null, sortby != "" ? sortby : null, sortdirection, pagenumber, pagesize, ref isfetchcount);
            
        }
        public int Purc_INS_UPD_Functions(string Func_Name, string Func_Code, string ReqsnType, string Func_Type, string Func_id, string User, string action)
        {
            return objLibCat.Purc_INS_UPD_Functions(Func_Name, Func_Code, ReqsnType, Func_Type, Func_id, User, action);
        }

        public bool CheckFunctionType(string id)
        {
            return objLibCat.CheckFunctionType(id);
        }

        public DataTable Get_ReqsnType()
        {
            return objLibCat.Purc_GetReqsnTypes();
        }
       
    }
}
