using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Services;
using SMS.Business.PMS;
using System.Data;
using System.Drawing;
using SMS.Properties;
using SMS.Business.Technical;
using SMS.Business.PURC;


[WebServiceBinding(ConformsTo = WsiProfiles.None)]
public partial class JibeWebService_Purc
{

    #region Equipment Structure


    [WebMethod]
    public string TEC_Get_FunctionalTreeDataEqpStruct(string vesselid)
    {

        BLL_PMS_Eqp objBl = new BLL_PMS_Eqp();
        DataTable dt = objBl.TEC_Get_FunctionalTreeDataEqpStruct(int.Parse(vesselid));

        return ConvertDataTabletoJson(dt);

    }
    [WebMethod]
    public int TEC_Insert_PMSGroup(int? ParentEqpID, string EqpName, string EqpDescription, string Maker, string Model, string NodeType, int Function, int Vessel_ID, int CreatedBy, int ActiveStatus)
    {
        BLL_PMS_Eqp objJob = new BLL_PMS_Eqp();

        return objJob.TEC_Insert_PMSGroup(ParentEqpID, EqpName, EqpDescription, Maker, Model, NodeType, Function, Vessel_ID, CreatedBy, ActiveStatus);
    }

    [WebMethod]
    public int TEC_Update_PMSGroup(int EqpID, string EqpName, string EqpDescription, string Maker, string Model, int ModifiedBy)
    {
        BLL_PMS_Eqp objJob = new BLL_PMS_Eqp();

        return objJob.TEC_Update_PMSGroup(EqpID, EqpName, EqpDescription, Maker, Model, ModifiedBy);
    }
    [WebMethod]
    public string TEC_Get_PMSGroupInfo(string EQPID)
    {

        BLL_PMS_Eqp objBl = new BLL_PMS_Eqp();

        DataSet ds = objBl.TEC_Get_PMSGroupInfo(int.Parse(EQPID));

        return ConvertDataTabletoJson(ds.Tables[0]);

    }

    [WebMethod]
    public string TEC_Get_PMSJobs(int? EqpID, int? vesselid, int? deptid, int? rankid, string jobtitle, int? IsActive, string sortby, int? sortdirection, int? pagenumber, int? pagesize, int isfetchcount, string TableID)
    {
        BLL_PMS_Eqp objBl = new BLL_PMS_Eqp();

        Dictionary<string, UDCHyperLinkImage> dicLink = new Dictionary<string, UDCHyperLinkImage>();


        // UDCHyperLink lnk = new UDCHyperLink("Job_Title", "", new string[] { "ID", "Job Code", "Title", "Freq.", "Freq.type", "Department", "Rank", "CMS", "Critical", }, new string[] { "ID", "Job_Code", "Job_Title", "Frequency", "Frequency_Name", "Department", "RankName", "CMS", "Critical" }, "");
        //dicLink.Add("Job_Title", lnk);
        List<UDCActionNew> dicAction = new List<UDCActionNew>();
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "AddSPMAttachment", "onclick" } }, "../../Images/AddAttchment.png", new string[] { "Task_ID", "Assignment_ID" }));
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "ASync_Get_Remark", "onmouseover" }, new string[] { "ShowAddRemark", "onclick" } }, "../../Images/remark.png", new string[] { "Task_ID" }));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onEditJob", "onclick" } }, "../../Images/edit.gif", new string[] { "ID" }, "Edit"));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onDeleteJob", "onclick" } }, "../../Images/Delete2.gif", new string[] { "ID" }, "Delete"));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onRestoreJob", "onclick" } }, "../../purchase/Image/restore.png", new string[] { "ID" }, "Restore"));
        Dictionary<string, UDCCheckBox> dicCheckBox = new Dictionary<string, UDCCheckBox>();
        UDCCheckBox chk = new UDCCheckBox("Selected", "Selected");


        dicCheckBox.Add("Selected", chk);

        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onEditJob", "onclick" } }, new string[] { "ID" }, "ID"));
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onDeleteJob", "onclick" } }, new string[] { "ID" }, "ID"));
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onRestoreJob", "onclick" } }, new string[] { "ID" }, "ID"));

        UDCToolTip TP = new UDCToolTip("Job_Title", "Job_Description", false);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();
        dicToolTip.Add("Job_Title", TP);


        string result = UDFLib.CreateHtmlTableFromDataTableWithCheckBox(objBl.TEC_Get_PMSJobs(EqpID, vesselid, deptid, rankid, jobtitle, IsActive, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount).Tables[0],
           new string[] { "ID=", "Job Code", "Title=", "Freq.", "Freq.type", "Department", "Rank", "CMS", "Critical", "" },
           new string[] { "ID", "JobCode", "JobTitle", "Frequency", "Frequency_Name", "Department", "RankName", "CMS", "Critical", "Selected" },
           dicLink,
            dicToolTip,
           new string[] { "left", "left", "left", "left", "left", "left", "left", "center", "center", "center" },
           TableID,
          "tbl-common-Css",
           "hdr-common-Css",
           "RowStyle-css", "AlternatingRowStyle-css", dicAction, dicJSEvent, dicCheckBox);
        return result + "~totalrecordfound~" + isfetchcount.ToString();
    }

    [WebMethod]
    public int TEC_Insert_PMSJobs(int VesselID, int EqpID, string JobCode, string JobTitle, string JobDesc, int Frequency, int FrequencyType, int DeptID, int RankID, int CMS, int Critical, int IsTechReq, int CreatedBy)
    {
        BLL_PMS_Eqp objBl = new BLL_PMS_Eqp();
        return objBl.TEC_Insert_PMSJobs(VesselID, EqpID, JobCode, JobTitle, JobDesc, Frequency, FrequencyType, DeptID, RankID, CMS, Critical, IsTechReq, CreatedBy);
    }

    [WebMethod]
    public string TEC_Get_PMSJobsID(int? EqpID, int? vesselid, int? deptid, int? rankid, string jobtitle, int? IsActive, string sortby, int? sortdirection, int? pagenumber, int? pagesize, int isfetchcount, string TableID)
    {
        BLL_PMS_Eqp objBl = new BLL_PMS_Eqp();

        Dictionary<string, UDCHyperLinkImage> dicLink = new Dictionary<string, UDCHyperLinkImage>();

        List<UDCActionNew> dicAction = new List<UDCActionNew>();
        Dictionary<string, UDCCheckBox> dicCheckBox = new Dictionary<string, UDCCheckBox>();
        UDCCheckBox chk = new UDCCheckBox("Selected", "Selected");

        dicCheckBox.Add("Selected", chk);

        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();

        string result = UDFLib.CreateHtmlTableFromDataTableWithCheckBox(objBl.TEC_Get_PMSJobs(EqpID, vesselid, deptid, rankid, jobtitle, IsActive, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount).Tables[0],
           new string[] { "JobID=", "" },
           new string[] { "ID", "Selected" },
           dicLink,
            dicToolTip,
           new string[] { "left", "left", },
           TableID,
           "tbl-common-Css",
           "HeaderStyle-css",
           "row-common-Css", "", dicAction, dicJSEvent, dicCheckBox);
        return result + "~totalrecordfound~" + isfetchcount.ToString();
    }


    [WebMethod]
    public string TEC_Get_PMSJobListByJobID(int JobID)
    {

        BLL_PMS_Eqp objBl = new BLL_PMS_Eqp();

        DataSet ds = objBl.TEC_Get_PMSJobListByJobID(JobID);

        return ConvertDataTabletoJson(ds.Tables[0]);

    }
    #endregion

    #region Manage System

    public class PMSParamaterList
    {
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }


    }
    public class CatalogList
    {
        public string SYS_CODE { get; set; }
        public string SYS_DESC { get; set; }
        public string SYS_PARTICULAR { get; set; }
        public string MAKER { get; set; }
        public string FUNCTIONS { get; set; }
        public string SET_INSTALLED { get; set; }
        public string MODULE_TYPE { get; set; }
        public string DEPT1 { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string RUN_HOUR { get; set; }
        public string CRITICAL { get; set; }
        public string ACC_CODE { get; set; }
        public string CREATED_BY { get; set; }
        public string MODIFY_BY { get; set; }
        public string DELETED_BY { get; set; }
        public string Vessel_ID { get; set; }
        public string ServiceACC_CODE { get; set; }


    }
    public class SubCatalogList
    {
        public string SUBSYS_CODE { get; set; }
        public string SUBSYS_DESC { get; set; }
        public string SUBSYS_PARTICULAR { get; set; }
        public string SET_INSTALLED { get; set; }
        public string MAKER { get; set; }
        public string MODULE_TYPE { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string RUN_HOUR { get; set; }
        public string CRITICAL { get; set; }
        public string CP_RUN_HOUR { get; set; }
        public string CREATED_BY { get; set; }
        public string MODIFY_BY { get; set; }
        public string DELETED_BY { get; set; }



    }
    public class SystemParameter
    {
        public string CODE { get; set; }
        public string SHORT_CODE { get; set; }
        public string DESCRIPTION { get; set; }

    }
    public class Supplier
    {
        public string SUPPLIER { get; set; }
        public string SHORT_NAME { get; set; }
        public string SUPPLIER_NAME { get; set; }
        public string SUPPLIER_TYPE { get; set; }
        public string SUPPLIER_CATEGORY { get; set; }
        public string CITY { get; set; }
        public string COUNTRY { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public string ASL_STATUS { get; set; }


    }
    public class Department
    {
        public int ID { get; set; }
        public string NAME_DEPT { get; set; }
        public string CODE { get; set; }
        public string FORM_TYPE { get; set; }

    }
    public class Account
    {

        public string BUDGET_CODE { get; set; }
        public string BUDGET_NAME { get; set; }
        public string BUDGET_GROUP { get; set; }

    }
    public class Location
    {

        public string LOC_ID { get; set; }
        public string LOC_SHORT_CODE { get; set; }
        public string LOC_NAME { get; set; }
        public string LOC_LONG_DESC { get; set; }
        public string PARENT_TYPE { get; set; }
        public string VESSEL_CODE { get; set; }
        public string SYSTEM_CODE { get; set; }
        public string SYS_DESCRIPTION { get; set; }
        public string LOC_ASSIGNFLAG { get; set; }
        public string SUBSYS_DESC { get; set; }
        public string CATEGORY_CODE { get; set; }


    }
    public class Vessel
    {
        public int Vessel_ID { get; set; }
        public string Vessel_Name { get; set; }
    }
    public class Rank
    {
        public int ID { get; set; }
        public string RANK_NAME { get; set; }
    }
    public class Unit
    {
        public string MAIN_PACK { get; set; }
        public string ABB { get; set; }
    }
    public class Jobs
    {
        public string ID { get; set; }
        public string JOB_CODE { get; set; }
        public string VESSEL_ID { get; set; }
        public string DEPT { get; set; }
        public string SYS_ID { get; set; }
        public string SYS_DESC { get; set; }
        public string SUBSYS_ID { get; set; }
        public string SUBSYS_DESC { get; set; }
        public string RANK_ID { get; set; }
        public string JOB_TITLE { get; set; }
        public string JOB_DESC { get; set; }
        public string FREQ { get; set; }
        public string FREQ_TYPE { get; set; }
        public string CMS { get; set; }
        public string CRITCAL { get; set; }
        public string IS_TECH_REQ { get; set; }
        public string IS_SAFETY_ALARM { get; set; }
        public string IS_CALIBRATION { get; set; }
        public string ACT_STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public string MODIFY_BY { get; set; }
        public string DELETED_BY { get; set; }
        public string IsRAMandatory { get; set; }   //Added by reshma RA in JIT: 11705
        public string IsRAApproval { get; set; }    //Added by reshma RA in JIT: 11705


    }
    public class Spares
    {
        public string ID { get; set; }
        public string SYS_CODE { get; set; }
        public string SUBSYS_CODE { get; set; }
        public string DRAW_NUM { get; set; }
        public string PART_NUM { get; set; }
        public string SHORT_DESC { get; set; }
        public string LONG_DESC { get; set; }
        public string UNIT { get; set; }
        public string MINQty { get; set; }
        public string MAXQty { get; set; }
        public string ITEM_CAT { get; set; }
        public string IMG_URL { get; set; }
        public string PROD_DETAILS { get; set; }
        public string CRITCAL { get; set; }
        public string ACT_STATUS { get; set; }
        public string CREATED_BY { get; set; }
        public string MODIFY_BY { get; set; }
        public string DELETED_BY { get; set; }


    }
    public class AssignLocation
    {
        public string LOC_NAME { get; set; }
        public string ASS_LOC_ID { get; set; }



    }
    public class PMSJobsCount
    {
        public string TotalJobsCount { get; set; }
        public string OverdueJobsCount { get; set; }
        public string CriticalJobsCount { get; set; }

    }
    public List<SystemParameter> SystemParameters { get; set; }
    public List<Supplier> Suppliers { get; set; }
    public List<Department> Departments { get; set; }
    public List<Account> Accounts { get; set; }
    public List<Location> Locations { get; set; }
    public List<Rank> Ranks { get; set; }
    public List<Unit> Units { get; set; }
    public List<CatalogList> Catalog { get; set; }
    public List<SubCatalogList> SubCatalog { get; set; }
    public List<PMSParamaterList> PMSParam { get; set; }
    public List<Jobs> Job { get; set; }
    public List<Spares> Spare { get; set; }
    public List<AssignLocation> AsLoc { get; set; }
    public List<Vessel> Vessels { get; set; }
    public PMSJobsCount JobsCount { get; set; }
    public static Dictionary<int, Color> InspColor;
    [WebMethod]
    public string Get_Function_Tree(string id, string vesselid, string Equipment_Type, string Safety_Alarm, string Calibration, string Critical)
    {

        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        DataTable dt = objBl.Get_Functional_Tree_Data(id, int.Parse(vesselid), UDFLib.ConvertStringToNull(Equipment_Type), UDFLib.ConvertIntegerToNull(Safety_Alarm), UDFLib.ConvertIntegerToNull(Calibration), UDFLib.ConvertIntegerToNull(Critical));

        return ConvertDataTabletoJson(dt);

    }

    [WebMethod]
    public string Get_Functional_Tree_Data_ManageSystem(string id, string vesselid, string function_code, string searchText, string Equipment_Type, int? IsActive, string Form_Type)
    {

        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        DataTable dt = objBl.Get_Functional_Tree_Data_ManageSystem(id, int.Parse(vesselid), UDFLib.ConvertStringToNull(function_code), searchText, UDFLib.ConvertStringToNull(Equipment_Type), IsActive, Form_Type);

        return ConvertDataTabletoJson(dt);

    }

    [WebMethod]
    public int PMS_Get_SystemStatus(int systemid)
    {

        SMS.Business.PURC.BLL_PURC_Purchase objBl = new SMS.Business.PURC.BLL_PURC_Purchase();
        int res = objBl.PMS_Get_SystemStatus(systemid);
        return res;

    }
    [WebMethod]
    public int PMS_Get_SubSystemStatus(int subsystemid)
    {

        SMS.Business.PURC.BLL_PURC_Purchase objBl = new SMS.Business.PURC.BLL_PURC_Purchase();
        int res = objBl.PMS_Get_SubSystemStatus(subsystemid);
        return res;

    }
    [WebMethod]
    public int PMS_System_Delete(int userid, int systemid)
    {

        SMS.Business.PURC.BLL_PURC_Purchase objBl = new SMS.Business.PURC.BLL_PURC_Purchase();
        int res = objBl.LibraryCatalogueDelete(userid, systemid);
        return res;

    }
    [WebMethod]
    public int PMS_System_Restore(int userid, int systemid)
    {

        SMS.Business.PURC.BLL_PURC_Purchase objBl = new SMS.Business.PURC.BLL_PURC_Purchase();
        int res = objBl.LibraryCatalogueRestore(userid, systemid);
        return res;

    }
    [WebMethod]
    public int PMS_SubSystem_Delete(int userid, int subsystemid)
    {

        SMS.Business.PURC.BLL_PURC_Purchase objBl = new SMS.Business.PURC.BLL_PURC_Purchase();
        int res = objBl.LibrarySubCatalogueDelete(userid, subsystemid);
        return res;

    }
    [WebMethod]
    public int PMS_SubSystem_Restore(int userid, int subsystemid)
    {

        SMS.Business.PURC.BLL_PURC_Purchase objBl = new SMS.Business.PURC.BLL_PURC_Purchase();
        int res = objBl.LibrarySubCatalogueRestore(userid, subsystemid);
        return res;

    }
    [WebMethod]
    public List<SystemParameter> LibraryGetSystemParameterList(string parenttypecode, string searchtext)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objBl = new SMS.Business.PURC.BLL_PURC_Purchase();

        DataTable dt = new DataTable();
        SystemParameters = new List<SystemParameter>();
        dt = objBl.LibraryGetSystemParameterList(parenttypecode, searchtext);
        try
        {
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SystemParameters.Add(new SystemParameter()
                        {
                            CODE = dr["CODE"].ToString(),
                            SHORT_CODE = dr["SHORT_CODE"].ToString(),
                            DESCRIPTION = dr["DESCRIPTION"].ToString()
                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            // throw ex;
        }

        return SystemParameters;
    }

    [WebMethod]
    public List<PMSParamaterList> LibraryGetPMSSystemParameterList(string parenttypecode, string searchtext)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();

        DataTable dt = new DataTable();
        PMSParam = new List<PMSParamaterList>();
        dt = objBl.LibraryGetPMSSystemParameterList(parenttypecode, searchtext);
        try
        {
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PMSParam.Add(new PMSParamaterList()
                        {
                            CODE = dr["Code"].ToString(),
                            NAME = dr["Name"].ToString(),
                            DESCRIPTION = dr["Description"].ToString()
                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            // throw ex;
        }

        return PMSParam;
    }

    [WebMethod]
    public List<Supplier> Get_Supplier()
    {
        using (SMS.Business.PURC.BLL_PURC_Purchase objsupplier = new SMS.Business.PURC.BLL_PURC_Purchase())
        {
            Suppliers = new List<Supplier>();
            DataTable dt = objsupplier.SelectSupplier();
            dt.DefaultView.RowFilter = "SUPPLIER_CATEGORY='M'";
            dt = dt.DefaultView.ToTable();
            try
            {
                if (dt != null)
                {

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Suppliers.Add(new Supplier()
                            {
                                SUPPLIER = dr["SUPPLIER"].ToString(),
                                // SHORT_NAME = dr["SHORT_NAME"].ToString(),
                                SUPPLIER_NAME = dr["SUPPLIER_NAME"].ToString(),
                                SUPPLIER_TYPE = dr["SUPPLIER_TYPE"].ToString(),
                                SUPPLIER_CATEGORY = dr["Supplier_Category"].ToString(),
                                CITY = dr["CITY"].ToString(),
                                COUNTRY = dr["COUNTRY"].ToString(),
                                EMAIL_ADDRESS = dr["Email_Addres"].ToString(),
                                ASL_STATUS = dr["ASL_Status"].ToString()
                            });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        return Suppliers;
    }

    [WebMethod]
    public List<Department> Get_Department(string FormType)
    {
        using (SMS.Business.PURC.BLL_PURC_Purchase objDept = new SMS.Business.PURC.BLL_PURC_Purchase())
        {
            Departments = new List<Department>();
            DataTable dt = objDept.SelectDepartment();
            DataView dv = new DataView();
            try
            {


                dt.DefaultView.RowFilter = "Form_Type='" + FormType + "'";
                dv = dt.DefaultView;

                if (dv.ToTable().Rows != null)
                {

                    if (dv.ToTable().Rows.Count > 0)
                    {
                        foreach (DataRow dr in dv.ToTable().Rows)
                        {
                            Departments.Add(new Department()
                            {
                                ID = Convert.ToInt32(dr["ID"].ToString()),
                                NAME_DEPT = dr["name_dept"].ToString(),
                                CODE = dr["code"].ToString(),
                                FORM_TYPE = dr["Form_Type"].ToString(),

                            });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        return Departments;
    }



    [WebMethod]
    public List<Vessel> Get_Vessel(int CompanyID)
    {
        SMS.Business.Infrastructure.BLL_Infra_VesselLib objVess = new SMS.Business.Infrastructure.BLL_Infra_VesselLib();

        Vessels = new List<Vessel>();
        DataTable dtvsl = objVess.Get_VesselList(0, 0, UDFLib.ConvertToInteger(CompanyID), "", UDFLib.ConvertToInteger(CompanyID));
        DataView dv = new DataView();
        try
        {

            dv = dtvsl.DefaultView;

            if (dv.ToTable().Rows != null)
            {

                if (dv.ToTable().Rows.Count > 0)
                {
                    foreach (DataRow dr in dv.ToTable().Rows)
                    {
                        Vessels.Add(new Vessel()
                        {
                            Vessel_ID = Convert.ToInt32(dr["Vessel_ID"].ToString()),
                            Vessel_Name = dr["Vessel_Name"].ToString(),

                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }

        return Vessels;
    }

    [WebMethod]
    public List<Account> Get_AccountCode()
    {

        using (SMS.Business.PURC.BLL_PURC_Purchase objDept = new SMS.Business.PURC.BLL_PURC_Purchase())
        {
            Accounts = new List<Account>();
            DataTable dt = objDept.SelectBudgetCode().Tables[0];

            try
            {
                if (dt != null)
                {

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Accounts.Add(new Account()
                            {

                                BUDGET_CODE = dr["Budget_Code"].ToString(),
                                BUDGET_NAME = dr["Budget_Name"].ToString(),
                                BUDGET_GROUP = dr["Budget_Group"].ToString(),

                            });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        return Accounts;
    }



    [WebMethod]
    public List<Unit> Get_Units()
    {


        SMS.Business.PURC.BLL_PURC_Purchase objUnits = new SMS.Business.PURC.BLL_PURC_Purchase();

        Units = new List<Unit>();
        DataTable dt = objUnits.SelectUnitnPackage();

        try
        {
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Units.Add(new Unit()
                        {

                            MAIN_PACK = dr["Main_Pack"].ToString(),
                            ABB = dr["ABREVIATION"].ToString()


                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }

        return Units;
    }

    [WebMethod]
    public List<AssignLocation> Get_System_Assign_Location(string systemcode, int vesselID)
    {


        SMS.Business.PMS.BLL_PMS_Library_Jobs objLoc = new SMS.Business.PMS.BLL_PMS_Library_Jobs();

        AsLoc = new List<AssignLocation>();
        DataTable dt = objLoc.LibraryGetCatalogueLocationAssign(systemcode, vesselID);

        try
        {
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AsLoc.Add(new AssignLocation()
                        {

                            LOC_NAME = dr["LocationName"].ToString(),
                            ASS_LOC_ID = dr["AssginLocationID"].ToString()


                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }

        return AsLoc;
    }

    [WebMethod]
    public List<AssignLocation> Get_SubSystem_Assign_Location(string systemcode, int subsystemcode, int vesselID)
    {


        SMS.Business.PURC.BLL_PURC_Purchase objLoc = new SMS.Business.PURC.BLL_PURC_Purchase();

        AsLoc = new List<AssignLocation>();
        DataTable dt = objLoc.GET_SUBSYTEMSYSTEM_ASSIGNED_LOCATION(systemcode, subsystemcode, vesselID);

        try
        {
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AsLoc.Add(new AssignLocation()
                        {

                            LOC_NAME = dr["LocationName"].ToString(),
                            ASS_LOC_ID = dr["ID"].ToString()


                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }

        return AsLoc;
    }
    [WebMethod]
    public List<CatalogList> Get_Next_System_Code()
    {


        SMS.Business.PURC.BLL_PURC_Purchase objSys = new SMS.Business.PURC.BLL_PURC_Purchase();

        Catalog = new List<CatalogList>();
        DataTable dt = objSys.LibraryGetNextSystemCode();

        try
        {
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Catalog.Add(new CatalogList()
                        {

                            SYS_CODE = dr[0].ToString(),



                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }

        return Catalog;
    }


    [WebMethod]
    public List<CatalogList> Get_Catalog_List(string systemid)
    {


        SMS.Business.PURC.BLL_PURC_Purchase objCat = new SMS.Business.PURC.BLL_PURC_Purchase();

        Catalog = new List<CatalogList>();
        DataTable dt = objCat.LibraryCatalogueList(systemid).Tables[0];

        try
        {
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Catalog.Add(new CatalogList()
                        {

                            SYS_CODE = dr["System_Code"].ToString(),
                            SYS_DESC = dr["System_Description"].ToString(),
                            SYS_PARTICULAR = dr["System_Particulars"].ToString(),
                            MAKER = dr["Maker"].ToString(),
                            FUNCTIONS = dr["ID"].ToString(),
                            SET_INSTALLED = dr["Set_Instaled"].ToString(),
                            MODULE_TYPE = dr["Module_Type"].ToString(),
                            DEPT1 = dr["Dept1"].ToString(),
                            SERIAL_NUMBER = dr["Serial_Number"].ToString(),
                            RUN_HOUR = dr["Run_Hour"].ToString(),
                            CRITICAL = dr["Critical"].ToString(),
                            ACC_CODE = dr["ACCOUNT_CODE"].ToString(),
                            CREATED_BY = dr["CREATEDBY"].ToString(),
                            MODIFY_BY = dr["MODIFIEDBY"].ToString(),
                            DELETED_BY = dr["DELETEDBY"].ToString(),
                            Vessel_ID = dr["Vessel_Code"].ToString(),
                            ServiceACC_CODE = dr["Service_Account_Code"].ToString()

                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }

        return Catalog;
    }

    [WebMethod]
    public List<SubCatalogList> Get_Sub_Catalog_List(string subsystemid)
    {


        SMS.Business.PURC.BLL_PURC_Purchase objCat = new SMS.Business.PURC.BLL_PURC_Purchase();

        SubCatalog = new List<SubCatalogList>();
        DataTable dt = objCat.LibrarySubCatalogueList(subsystemid).Tables[0];

        try
        {
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SubCatalog.Add(new SubCatalogList()
                        {

                            SUBSYS_CODE = dr["Subsystem_Code"].ToString(),
                            SUBSYS_DESC = dr["Subsystem_Description"].ToString(),
                            SUBSYS_PARTICULAR = dr["Subsystem_Particulars"].ToString(),
                            MAKER = dr["Maker"].ToString(),
                            MODULE_TYPE = dr["Module_Type"].ToString(),
                            SERIAL_NUMBER = dr["Serial_Number"].ToString(),
                            RUN_HOUR = dr["Run_Hour"].ToString(),
                            CRITICAL = dr["Critical"].ToString(),
                            CP_RUN_HOUR = dr["Copy_Run_hour"].ToString(),
                            CREATED_BY = dr["CREATEDBY"].ToString(),
                            MODIFY_BY = dr["MODIFIEDBY"].ToString(),
                            DELETED_BY = dr["DELETEDBY"].ToString(),
                            SET_INSTALLED = dr["Set_Instaled"].ToString()

                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }

        return SubCatalog;
    }

    [WebMethod]
    public string PMS_Get_AssignLocation(string systemcode, string SubSystemCode, string searchtext, int vesselid, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, string TableID)
    {

        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();

        Dictionary<string, UDCHyperLinkImage> dicLink = new Dictionary<string, UDCHyperLinkImage>();
        List<UDCAction> lstAction = new List<UDCAction>();

        Dictionary<string, UDCCheckBox> dicCheckBox = new Dictionary<string, UDCCheckBox>();
        UDCCheckBox chk = new UDCCheckBox("LocAssignFlag", "LocAssignFlag");
        UDCCheckBox chk1 = new UDCCheckBox("Category_Code", "Category_Code");

        dicCheckBox.Add("LocAssignFlag", chk);
        dicCheckBox.Add("Category_Code", chk1);
        // UDCHyperLink lnk = new UDCHyperLink("Job_Title", "", new string[] { "ID", "Job Code", "Title", "Freq.", "Freq.type", "Department", "Rank", "CMS", "Critical", }, new string[] { "ID", "Job_Code", "Job_Title", "Frequency", "Frequency_Name", "Department", "RankName", "CMS", "Critical" }, "");
        //dicLink.Add("Job_Title", lnk);
        List<UDCActionNew> dicAction = new List<UDCActionNew>();
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onEditLocation", "onclick" } }, "../../Images/edit.gif", new string[] { "LocationID", "LocationShortCode", "LocationName" }, "Edit"));
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "ASync_Get_Remark", "onmouseover" }, new string[] { "ShowAddRemark", "onclick" } }, "../../Images/remark.png", new string[] { "Task_ID" }));
        //   dicAction.Add(new UDCAction(new string[][] { new string[] { "onEditJob", "onclick" } }, "../../Images/edit.png", new string[] { "ID" }));
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "Async_Get_Task_More_Information", "onmouseover" } }, "../../Images/MoreInfo.png", new string[] { "Task_ID" }));


        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();
        //    dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onEditJob", "onclick" } }, new string[] { "ID" }, "ID"));
        //dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onEditLocation", "onclick" } }, new string[] { "LocationID" }, "Action"));


        UDCToolTip TP = new UDCToolTip("LocationShortCode", "LocationShortCode", false);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();
        dicToolTip.Add("LocationShortCode", TP);


        return UDFLib.CreateHtmlTableFromDataTableWithCheckBox(objBl.PMS_Get_AssignLocation(systemcode, SubSystemCode, searchtext, vesselid, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount).Tables[0],
           new string[] { "ID=", "Location Name=", "System", "SubSystem", "LocAssign", "Is Spare" },
           new string[] { "LocationID", "LocationName", "System_Description", "Subsystem_Description", "LocAssignFlag", "Category_Code" },
           dicLink,
            dicToolTip,
           new string[] { "left", "left", "left", "left", "left", "left" }, TableID,
           "tbl-common-Css",
           "PMSGridHeaderStyle-css",
           "PMSGridRowStyle-css", "PMSGridAlternatingRowStyle-css", dicAction, dicJSEvent, dicCheckBox);

    }

    [WebMethod]
    public string PMS_Get_EForm(string searchtext, int vesselid, int jobid, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, string TableID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        Dictionary<string, UDCHyperLinkImage> dicLink = new Dictionary<string, UDCHyperLinkImage>();
        List<UDCAction> lstAction = new List<UDCAction>();
        Dictionary<string, UDCCheckBox> dicCheckBox = new Dictionary<string, UDCCheckBox>();
        UDCCheckBox chk = new UDCCheckBox("AssignFlag", "AssignFlag");

        dicCheckBox.Add("AssignFlag", chk);

        List<UDCActionNew> dicAction = new List<UDCActionNew>();
        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();
        UDCToolTip TP = new UDCToolTip("Form_Display_Name", "Form_Display_Name", false);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();

        return UDFLib.CreateHtmlTableFromDataTableWithCheckBox(objBl.LibraryGetJOB_eFORM_MAPPING_SEARCH(searchtext, vesselid, jobid, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount),
           new string[] { "MapID", "Form ID=", "Vessel", "eForm Name=", "Select" },
           new string[] { "ID", "Form_ID", "Vessel_Name", "Form_Display_Name", "AssignFlag" },
           dicLink,
            dicToolTip,
           new string[] { "left", "left", "left", "left", "center" }, TableID,
           "tbl-common-Css",
           "PMSGridHeaderStyle-css",
           "PMSGridRowStyle-css", "PMSGridAlternatingRowStyle-css", dicAction, dicJSEvent, dicCheckBox);

    }

    [WebMethod]
    public string Get_Lib_Planned_Jobs_ManageSystem(string Vessel_ID, string SystemID, string SubSystemID, int? DeptID, int? rankid, string jobtitle, int? isactive, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, int isfetchcount, string TableID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();

        Dictionary<string, UDCHyperLinkImage> dicLink = new Dictionary<string, UDCHyperLinkImage>();


        // UDCHyperLink lnk = new UDCHyperLink("Job_Title", "", new string[] { "ID", "Job Code", "Title", "Freq.", "Freq.type", "Department", "Rank", "CMS", "Critical", }, new string[] { "ID", "Job_Code", "Job_Title", "Frequency", "Frequency_Name", "Department", "RankName", "CMS", "Critical" }, "");
        //dicLink.Add("Job_Title", lnk);
        List<UDCActionNew> dicAction = new List<UDCActionNew>();
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "AddSPMAttachment", "onclick" } }, "../../Images/AddAttchment.png", new string[] { "Task_ID", "Assignment_ID" }));
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "ASync_Get_Remark", "onmouseover" }, new string[] { "ShowAddRemark", "onclick" } }, "../../Images/remark.png", new string[] { "Task_ID" }));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onEditJob", "onclick" } }, "../../Images/edit.gif", new string[] { "ID" }, "Edit"));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onDeleteJob", "onclick" } }, "../../Images/Delete2.gif", new string[] { "ID" }, "Delete"));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onRestoreJob", "onclick" } }, "../../purchase/Image/restore.png", new string[] { "ID" }, "Restore"));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onAssignEForm", "onclick" } }, "../../Images/eForms.png", new string[] { "ID" }, "e-Form Assign"));
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "Async_Get_Task_More_Information", "onmouseover" } }, "../../Images/MoreInfo.png", new string[] { "Task_ID" }));
        Dictionary<string, UDCCheckBox> dicCheckBox = new Dictionary<string, UDCCheckBox>();
        UDCCheckBox chk = new UDCCheckBox("Selected", "Selected");


        dicCheckBox.Add("Selected", chk);

        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onEditJob", "onclick" } }, new string[] { "ID" }, "ID"));
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onDeleteJob", "onclick" } }, new string[] { "ID" }, "ID"));
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onRestoreJob", "onclick" } }, new string[] { "ID" }, "ID"));
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onAssignEForm", "onclick" } }, new string[] { "ID" }, "ID"));

        UDCToolTip TP = new UDCToolTip("Job_Title", "Job_Description", false);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();
        dicToolTip.Add("Job_Title", TP);


        string result = UDFLib.CreateHtmlTableFromDataTableWithCheckBox(objBl.ManageSystemJobSearch(Convert.ToInt32(SystemID), Convert.ToInt32(SubSystemID), Convert.ToInt32(Vessel_ID), DeptID, rankid, jobtitle, isactive, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount).Tables[0],
           new string[] { "Job Code", "Title=", "Freq.", "Freq.type", "Department", "Rank", "CMS", "Critical", "Select To Move" },
           new string[] { "Job_Code", "Job_Title", "Frequency", "Frequency_Name", "Department", "RankName", "CMS", "Critical", "Selected" },
           dicLink,
            dicToolTip,
           new string[] { "left", "left", "left", "left", "left", "left", "center", "center", "center" },
           TableID,
          "tbl-common-Css",
           "PMSGridHeaderStyle-css",
           "PMSGridRowStyle-css", "PMSGridAlternatingRowStyle-css", dicAction, dicJSEvent, dicCheckBox);
        return result + "~totalrecordfound~" + isfetchcount.ToString();
    }


    /// <summary>
    /// Added by reshma : parameter added for RA : Is_RAMandatory & Is_RAApproval
    /// </summary>
    /// <param name="Vessel_ID">Vessel Id</param>
    /// <param name="SystemID">system id for specific job</param>
    /// <param name="SubSystemID">sub system id for specific job</param>
    /// <param name="DeptID">Department id for specific job</param>
    /// <param name="rankid">rank id</param>
    /// <param name="jobtitle"> job title</param>
    /// <param name="isactive">for check active status</param>
    /// <param name="searchtext"> serach text </param>
    /// <param name="Is_RAMandatory">check if RA is mandatory or not</param>
    /// <param name="Is_RAApproval">check if RA form is approve by office or not  </param>
    /// <param name="sortby">Column name by which data to be sorted</param>
    /// <param name="sortdirection">Direction in which data to be sorted 'ASC' or 'DESC'</param>
    /// <param name="pagenumber">Page Number of displaying data </param>
    /// <param name="pagesize">Max data to be return</param>
    /// <param name="isfetchcount">Return Total Job Count</param>
    /// <param name="TableID"></param>
    /// <returns></returns>
    [WebMethod]
    public string Get_Lib_Planned_Jobs_ManageSystem(string Vessel_ID, string SystemID, string SubSystemID, int? DeptID, int? rankid, string jobtitle, int? isactive, string searchtext, int? Is_RAMandatory, int? Is_RAApproval, string sortby, int? sortdirection, int? pagenumber, int? pagesize, int isfetchcount, string TableID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();

        Dictionary<string, UDCHyperLinkImage> dicLink = new Dictionary<string, UDCHyperLinkImage>();


        // UDCHyperLink lnk = new UDCHyperLink("Job_Title", "", new string[] { "ID", "Job Code", "Title", "Freq.", "Freq.type", "Department", "Rank", "CMS", "Critical", }, new string[] { "ID", "Job_Code", "Job_Title", "Frequency", "Frequency_Name", "Department", "RankName", "CMS", "Critical" }, "");
        //dicLink.Add("Job_Title", lnk);
        List<UDCActionNew> dicAction = new List<UDCActionNew>();
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "AddSPMAttachment", "onclick" } }, "../../Images/AddAttchment.png", new string[] { "Task_ID", "Assignment_ID" }));
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "ASync_Get_Remark", "onmouseover" }, new string[] { "ShowAddRemark", "onclick" } }, "../../Images/remark.png", new string[] { "Task_ID" }));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onEditJob", "onclick" } }, "../../Images/edit.gif", new string[] { "ID" }, "Edit"));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onDeleteJob", "onclick" } }, "../../Images/Delete2.gif", new string[] { "ID" }, "Delete"));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onRestoreJob", "onclick" } }, "../../purchase/Image/restore.png", new string[] { "ID" }, "Restore"));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onAssignEForm", "onclick" } }, "../../Images/eForms.png", new string[] { "ID" }, "e-Form Assign"));
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "Async_Get_Task_More_Information", "onmouseover" } }, "../../Images/MoreInfo.png", new string[] { "Task_ID" }));
        Dictionary<string, UDCCheckBox> dicCheckBox = new Dictionary<string, UDCCheckBox>();
        UDCCheckBox chk = new UDCCheckBox("Selected", "Selected");


        dicCheckBox.Add("Selected", chk);

        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onEditJob", "onclick" } }, new string[] { "ID" }, "ID"));
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onDeleteJob", "onclick" } }, new string[] { "ID" }, "ID"));
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onRestoreJob", "onclick" } }, new string[] { "ID" }, "ID"));
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onAssignEForm", "onclick" } }, new string[] { "ID" }, "ID"));

        UDCToolTip TP = new UDCToolTip("Job_Title", "Job_Description", false);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();
        dicToolTip.Add("Job_Title", TP);


        string result = UDFLib.CreateHtmlTableFromDataTableWithCheckBox(objBl.ManageSystemJobSearch(Convert.ToInt32(SystemID), Convert.ToInt32(SubSystemID), Convert.ToInt32(Vessel_ID), DeptID, rankid, jobtitle, isactive, searchtext, Is_RAMandatory, Is_RAApproval, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount).Tables[0],
           new string[] { "Job Code", "Title=", "Freq.", "Freq.type", "Department", "Rank", "CMS", "Critical", "Risk Assessment", "Select To Move" },
           new string[] { "Job_Code", "Job_Title", "Frequency", "Frequency_Name", "Department", "RankName", "CMS", "Critical", "IsRAMandatory", "Selected" },
           dicLink,
            dicToolTip,
           new string[] { "left", "left", "left", "left", "left", "left", "center", "center", "center", "center" },
           TableID,
          "tbl-common-Css",
           "PMSGridHeaderStyle-css",
           "PMSGridRowStyle-css", "PMSGridAlternatingRowStyle-css", dicAction, dicJSEvent, dicCheckBox);
        return result + "~totalrecordfound~" + isfetchcount.ToString();
    }

    /// <summary>
    /// Added by reshma : parameter added for RA : Is_RAMandatory & Is_RAApproval
    /// </summary>
    /// <param name="Vessel_ID">Vessel Id</param>
    /// <param name="SystemID">system id for specific job</param>
    /// <param name="SubSystemID">sub system id for specific job</param>
    /// <param name="DeptID">Department id for specific job</param>
    /// <param name="rankid">rank id</param>
    /// <param name="jobtitle"> job title</param>
    /// <param name="isactive">for check active status</param>
    /// <param name="searchtext"> serach text </param>
    /// <param name="Is_RAMandatory">check if RA is mandatory or not</param>
    /// <param name="Is_RAApproval">check if RA form is approve by office or not  </param>
    /// <param name="sortby">Column name by which data to be sorted</param>
    /// <param name="sortdirection">Direction in which data to be sorted 'ASC' or 'DESC'</param>
    /// <param name="pagenumber">Page Number of displaying data </param>
    /// <param name="pagesize">Max data to be return</param>
    /// <param name="isfetchcount">Return Total Job Count</param>
    /// <param name="TableID"></param>
    /// <returns></returns>
    [WebMethod]
    public string Get_Lib_Planned_JobsID_ManageSystem(string Vessel_ID, string SystemID, string SubSystemID, int? DeptID, int? rankid, string jobtitle, int? isactive, string searchtext, int? Is_RAMandatory, int? Is_RAApproval, string sortby, int? sortdirection, int? pagenumber, int? pagesize, int isfetchcount, string TableID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();

        Dictionary<string, UDCHyperLinkImage> dicLink = new Dictionary<string, UDCHyperLinkImage>();



        List<UDCActionNew> dicAction = new List<UDCActionNew>();
        Dictionary<string, UDCCheckBox> dicCheckBox = new Dictionary<string, UDCCheckBox>();
        UDCCheckBox chk = new UDCCheckBox("Selected", "Selected");


        dicCheckBox.Add("Selected", chk);

        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();


        // UDCToolTip TP = new UDCToolTip("Job_Title", "Job_Description", false);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();
        // dicToolTip.Add("Job_Title", TP);


        string result = UDFLib.CreateHtmlTableFromDataTableWithCheckBox(objBl.ManageSystemJobSearch(Convert.ToInt32(SystemID), Convert.ToInt32(SubSystemID), Convert.ToInt32(Vessel_ID), DeptID, rankid, jobtitle, isactive, searchtext, Is_RAMandatory, Is_RAApproval, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount).Tables[0],
           new string[] { "JobID=", "" },
           new string[] { "ID", "Selected" },
           dicLink,
            dicToolTip,
           new string[] { "left", "left" },
           TableID,
           "tbl-common-Css",
           "PMSGridHeaderStyle-css",
           "PMSGridRowStyle-css", "PMSGridAlternatingRowStyle-css", dicAction, dicJSEvent, dicCheckBox);
        return result + "~totalrecordfound~" + isfetchcount.ToString();
    }


    [WebMethod]
    public string Get_Lib_Planned_JobsID_ManageSystem(string Vessel_ID, string SystemID, string SubSystemID, int? DeptID, int? rankid, string jobtitle, int? isactive, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, int isfetchcount, string TableID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();

        Dictionary<string, UDCHyperLinkImage> dicLink = new Dictionary<string, UDCHyperLinkImage>();



        List<UDCActionNew> dicAction = new List<UDCActionNew>();
        Dictionary<string, UDCCheckBox> dicCheckBox = new Dictionary<string, UDCCheckBox>();
        UDCCheckBox chk = new UDCCheckBox("Selected", "Selected");


        dicCheckBox.Add("Selected", chk);

        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();


        // UDCToolTip TP = new UDCToolTip("Job_Title", "Job_Description", false);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();
        // dicToolTip.Add("Job_Title", TP);


        string result = UDFLib.CreateHtmlTableFromDataTableWithCheckBox(objBl.ManageSystemJobSearch(Convert.ToInt32(SystemID), Convert.ToInt32(SubSystemID), Convert.ToInt32(Vessel_ID), DeptID, rankid, jobtitle, isactive, searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount).Tables[0],
           new string[] { "JobID=", "" },
           new string[] { "ID", "Selected" },
           dicLink,
            dicToolTip,
           new string[] { "left", "left" },
           TableID,
           "tbl-common-Css",
           "PMSGridHeaderStyle-css",
           "PMSGridRowStyle-css", "PMSGridAlternatingRowStyle-css", dicAction, dicJSEvent, dicCheckBox);
        return result + "~totalrecordfound~" + isfetchcount.ToString();
    }

    [WebMethod]
    public string Get_Lib_Planned_JobsAttachment_ManageSystem(int Vessel_ID, int jobid, string TableID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();

        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();


        dicLink.Add("ATTACHMENT_NAME", new UDCHyperLink("ATTACHMENT_NAME", "../Uploads/PmsJobs/", new string[] { "ATTACHMENT_PATH" }, new string[] { "ATTACHMENT_PATH" }, ""));
        List<UDCActionNew> dicAction = new List<UDCActionNew>();

        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onDeleteJobAttachment", "onclick" } }, "../../Images/Delete.png", new string[] { "ATTACHMENT_PATH" }, "Delete"));

        Dictionary<string, UDCCheckBox> dicCheckBox = new Dictionary<string, UDCCheckBox>();
        // UDCCheckBox chk = new UDCCheckBox("Selected", "Selected");


        //  dicCheckBox.Add("Selected", chk);

        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();


        // UDCToolTip TP = new UDCToolTip("Job_Title", "Job_Description", false);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();
        // dicToolTip.Add("Job_Title", TP);


        string result = UDFLib.CreateHtmlDataListFromDataTable(objBl.LibraryGetJobInstructionAttachment(Vessel_ID, jobid),
           new string[] { "ATTACHMENT_NAME" },
           new string[] { "ATTACHMENT_NAME" },
           dicLink,
            dicToolTip,
           new string[] { "left", "left" },
           TableID,
           "",
           "",
           "", "", dicAction, dicJSEvent, dicCheckBox);
        return result;
    }
    /// <summary>
    /// Fetching Item record for manage system.
    /// </summary>
    /// <param name="deptcode"></param>
    /// <param name="systemcode"></param>
    /// <param name="subsystemcode"></param>
    /// <param name="vesselid"></param>
    /// <param name="partnumber"></param>
    /// <param name="drawingnumber"></param>
    /// <param name="name"></param>
    /// <param name="longdesc"></param>
    /// <param name="IsActive"></param>
    /// <param name="searchtext"></param>
    /// <param name="sortby"></param>
    /// <param name="sortdirection"></param>
    /// <param name="pagenumber"></param>
    /// <param name="pagesize"></param>
    /// <param name="isfetchcount"></param>
    /// <param name="rowcount"></param>
    /// <param name="TableID"></param>
    /// <returns></returns>
    [WebMethod]
    public string Get_Lib_Items_ManageSystem(string deptcode, string systemcode, string subsystemcode, int? vesselid, string partnumber, string drawingnumber, string name, string longdesc, int? IsActive, string searchtext, string sortby, int? sortdirection, int pagenumber, int pagesize, int? isfetchcount, int rowcount, string TableID)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objBl = new SMS.Business.PURC.BLL_PURC_Purchase();

        Dictionary<string, UDCHyperLinkImage> dicLink = new Dictionary<string, UDCHyperLinkImage>();


        UDCHyperLinkImage lnk = new UDCHyperLinkImage("Image_Url_Att", "../Uploads/PURC_Items/", new string[] { "Image_Url" }, new string[] { "Image_Url" }, "", "../../Images/attach-icon.png");
        UDCHyperLinkImage lnk1 = new UDCHyperLinkImage("Product_Details_Att", "../Uploads/PURC_Items/", new string[] { "Product_Details" }, new string[] { "Product_Details" }, "", "../../Images/attach-icon.png");
        dicLink.Add("Image_Url_Att", lnk);
        dicLink.Add("Product_Details_Att", lnk1);
        //dicLink.Add("Job_Title", lnk);
        List<UDCActionNew> dicAction = new List<UDCActionNew>();
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "AddSPMAttachment", "onclick" } }, "../../Images/AddAttchment.png", new string[] { "Task_ID", "Assignment_ID" }));
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "ASync_Get_Remark", "onmouseover" }, new string[] { "ShowAddRemark", "onclick" } }, "../../Images/remark.png", new string[] { "Task_ID" }));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onEditSpare", "onclick" } }, "../../Images/edit.gif", new string[] { "ID" }, "Edit"));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onDeleteSpare", "onclick" } }, "../../Images/Delete2.gif", new string[] { "ID" }, "Delete"));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onRestoreSpare", "onclick" } }, "../../purchase/Image/restore.png", new string[] { "ID" }, "Restore"));
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "Async_Get_Task_More_Information", "onmouseover" } }, "../../Images/MoreInfo.png", new string[] { "Task_ID" }));
        Dictionary<string, UDCCheckBox> dicCheckBox = new Dictionary<string, UDCCheckBox>();





        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onEditSpare", "onclick" } }, new string[] { "ID" }, "ID"));
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onDeleteSpare", "onclick" } }, new string[] { "ID" }, "ID"));
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onRestoreSpare", "onclick" } }, new string[] { "ID" }, "ID"));


        UDCToolTip TP = new UDCToolTip("Short_Description", "Long_Description", false);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();
        dicToolTip.Add("Short_Description", TP);
        string result = string.Empty;
        if (deptcode == "PR")
        {
            UDCCheckBox chk = new UDCCheckBox("Meat", "Meat", new string[][] { new string[] { "onMeatCheck", "onchange" } }, new string[] { "ID" });
            dicCheckBox.Add("Meat", chk);
            // DataTable dt=objBl.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount).Tables[0];
            result = UDFLib.CreateHtmlTableFromDataTableWithCheckBox(objBl.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, searchtext, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount).Tables[0],
              new string[] { "Drawing Number", "Part Number=", "Name=", "Unit", "Critical", "MinQty", "MaxQty", "ROB", " Img Att.", "Dtls Att.", "Meat" },
              new string[] { "Drawing_Number", "Part_Number", "Short_Description", "Unit_and_Packings", "Critical_Flag", "Inventory_Min", "Inventory_Max", "ROB", "Image_Url_Att", "Product_Details_Att", "Meat" },
              dicLink,
               dicToolTip,
              new string[] { "left", "left", "left", "left", "left", "center", "center", "center", "center", "center", "center", "center" },
              TableID,
              "tbl-common-Css",
              "PMSGridHeaderStyle-css",
              "PMSGridRowStyle-css", "PMSGridAlternatingRowStyle-css", dicAction, dicJSEvent, dicCheckBox);
        }
        else if (deptcode == "BS")
        {
            UDCCheckBox chk = new UDCCheckBox("IsSlopchest", "IsSlopchest", new string[][] { new string[] { "onSlopChestCheck", "onchange" } }, new string[] { "ID" });
            dicCheckBox.Add("IsSlopchest", chk);
            // DataTable dt=objBl.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount).Tables[0];
            result = UDFLib.CreateHtmlTableFromDataTableWithCheckBox(objBl.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, searchtext, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount).Tables[0],
               new string[] { "Drawing Number", "Part Number=", "Name=", "Unit", "Critical", "MinQty", "MaxQty", "ROB", " Img Att.", "Dtls Att.", "Slopchest Item " },
               new string[] { "Drawing_Number", "Part_Number", "Short_Description", "Unit_and_Packings", "Critical_Flag", "Inventory_Min", "Inventory_Max", "ROB", "Image_Url_Att", "Product_Details_Att", "IsSlopchest" },
               dicLink,
                dicToolTip,
               new string[] { "left", "left", "left", "left", "left", "center", "center", "center", "center", "center", "center", "center" },
               TableID,
               "tbl-common-Css",
               "PMSGridHeaderStyle-css",
               "PMSGridRowStyle-css", "PMSGridAlternatingRowStyle-css", dicAction, dicJSEvent, dicCheckBox);
        }
        else
        {
            // DataTable dt=objBl.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount).Tables[0];
            result = UDFLib.CreateHtmlTableFromDataTableWithCheckBox(objBl.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, searchtext, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount).Tables[0],
               new string[] { "Drawing Number", "Part Number=", "Name=", "Unit", "Critical", "MinQty", "MaxQty", "ROB", " Img Att.", "Dtls Att.", },
               new string[] { "Drawing_Number", "Part_Number", "Short_Description", "Unit_and_Packings", "Critical_Flag", "Inventory_Min", "Inventory_Max", "ROB", "Image_Url_Att", "Product_Details_Att", },
               dicLink,
                dicToolTip,
               new string[] { "left", "left", "left", "left", "left", "center", "center", "center", "center", "center", "center", },
               TableID,
               "tbl-common-Css",
               "PMSGridHeaderStyle-css",
               "PMSGridRowStyle-css", "PMSGridAlternatingRowStyle-css", dicAction, dicJSEvent, dicCheckBox);
        }
        return result + "~totalrecordfound~" + rowcount.ToString();

    }



    [WebMethod]
    public List<Jobs> Get_Jobs(int JobID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objJob = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        Job = new List<Jobs>();
        DataTable dt = objJob.LibraryJobList(JobID).Tables[0];

        try
        {
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Job.Add(new Jobs()
                        {

                            ID = dr["ID"].ToString(),
                            JOB_CODE = dr["Job_Code"].ToString(),
                            VESSEL_ID = dr["Vessel_ID"].ToString(),
                            DEPT = dr["Department_ID"].ToString(),
                            SYS_ID = dr["System_ID"].ToString(),
                            SYS_DESC = dr["System_Description"].ToString(),
                            SUBSYS_ID = dr["SubSystem_ID"].ToString(),
                            SUBSYS_DESC = dr["Subsystem_Description"].ToString(),
                            RANK_ID = dr["Rank_ID"].ToString(),
                            JOB_TITLE = dr["Job_Title"].ToString(),
                            JOB_DESC = dr["Job_Description"].ToString(),
                            FREQ = dr["Frequency"].ToString(),
                            FREQ_TYPE = dr["Frequency_Type"].ToString(),
                            CMS = dr["CMS"].ToString(),
                            CRITCAL = dr["Critical"].ToString(),
                            IS_TECH_REQ = dr["Is_Tech_Required"].ToString(),
                            IS_SAFETY_ALARM = dr["IsSafetyAlarm"].ToString(),
                            IS_CALIBRATION = dr["IsCalibration"].ToString(),
                            ACT_STATUS = dr["ACTIVE_STATUS"].ToString(),
                            CREATED_BY = dr["CREATEDBY"].ToString(),
                            MODIFY_BY = dr["MODIFIEDBY"].ToString(),
                            DELETED_BY = dr["DELETEDBY"].ToString(),
                            IsRAMandatory = dr["IsRAMandatory"].ToString(),   //Added by reshma for RA : To read value of RA_Mandatory
                            IsRAApproval = dr["IsRAApproval"].ToString()      //Added by reshma for RA :To read value of RA_Mandatory
                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }

        return Job;
    }


    [WebMethod]
    public int Delete_Jobs(int userid, int JobID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objJob = new SMS.Business.PMS.BLL_PMS_Library_Jobs();

        return objJob.LibraryJobDelete(userid, JobID);
    }


    [WebMethod]
    public int Delete_Jobs_Attachement(string Attchment_Path, int userid)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objJob = new SMS.Business.PMS.BLL_PMS_Library_Jobs();

        return objJob.LibraryDeleteJobInstructionAttachment(Attchment_Path, userid);
    }


    [WebMethod]
    public int Restore_Jobs(int userid, int JobID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objJob = new SMS.Business.PMS.BLL_PMS_Library_Jobs();

        return objJob.LibraryJobRestore(userid, JobID);
    }

    [WebMethod]
    public List<Spares> Get_Spare(string ItemID)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objBl = new SMS.Business.PURC.BLL_PURC_Purchase();
        Spare = new List<Spares>();
        DataTable dt = objBl.LibraryItemList(ItemID).Tables[0];

        try
        {
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Spare.Add(new Spares()
                        {

                            ID = dr["ID"].ToString(),
                            SYS_CODE = dr["SYSTEM_CODE"].ToString(),
                            SUBSYS_CODE = dr["SUBSYSTEM_CODE"].ToString(),
                            DRAW_NUM = dr["Drawing_Number"].ToString(),
                            PART_NUM = dr["PART_NUMBER"].ToString(),
                            SHORT_DESC = dr["SHORT_DESCRIPTION"].ToString(),
                            LONG_DESC = dr["LONG_DESCRIPTION"].ToString(),
                            UNIT = dr["UNIT_AND_PACKINGS"].ToString(),
                            MINQty = dr["INVENTORY_MIN"].ToString(),
                            MAXQty = dr["INVENTORY_MAX"].ToString(),
                            ITEM_CAT = dr["Item_Category"].ToString(),
                            IMG_URL = dr["Image_Url"].ToString(),
                            PROD_DETAILS = dr["Product_Details"].ToString(),
                            CRITCAL = dr["Critical_Flag"].ToString(),
                            ACT_STATUS = dr["ACTIVE_STATUS"].ToString(),
                            CREATED_BY = dr["CREATEDBY"].ToString(),
                            MODIFY_BY = dr["MODIFIEDBY"].ToString(),
                            DELETED_BY = dr["DELETEDBY"].ToString()




                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }

        return Spare;
    }


    [WebMethod]
    public int Delete_Spare(int userid, string ItemID)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objLibCat = new SMS.Business.PURC.BLL_PURC_Purchase();
        return objLibCat.LibraryItemDelete(userid, ItemID);
    }

    [WebMethod]
    public int Restore_Spare(int userid, string ItemID)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objLibCat = new SMS.Business.PURC.BLL_PURC_Purchase();
        return objLibCat.LibraryItemRestore(userid, ItemID);
    }

    [WebMethod]
    public string Get_Spare_ManageSystem(string Vessel_ID, string SystemID, string SubSystemID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();


        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();


        return UDFLib.CreateHtmlTableFromDataTable(objBl.Get_EQP_Spare_Consumption_ManageSystem(int.Parse(Vessel_ID), SystemID, SubSystemID),
           new string[] { "Machine", "Date", "Consumption", },
           new string[] { "Description", "Created_On", "Used_Qty" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "center", "center" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");
    }

    [WebMethod]
    public int SaveLocation(string ShortCode, string ShortDescription, string userid)
    {
        using (SMS.Business.PURC.BLL_PURC_Purchase objTechService = new SMS.Business.PURC.BLL_PURC_Purchase())
        {
            LocationData objLocDo = new LocationData();

            // In PURC_LIB_SYSTEM_PARAMETERS  table for location  'ParentType = 1'

            objLocDo.ParentType = "1";
            objLocDo.ShortCode = ShortCode;
            objLocDo.ShortDiscription = ShortDescription;
            objLocDo.LongDiscription = "";
            objLocDo.CurrentUser = userid;
            objLocDo.NoOfLoc = "1";
            int retVal = objTechService.SaveLocation(objLocDo);

            return retVal;

        }




    }
    [WebMethod]
    public int SaveEditLocation(int LocationID, string ShortDescription, string userid)
    {
        using (SMS.Business.PURC.BLL_PURC_Purchase objTechService = new SMS.Business.PURC.BLL_PURC_Purchase())
        {
            LocationData objLocDo = new LocationData();

            // In PURC_LIB_SYSTEM_PARAMETERS  table for location  'ParentType = 1'

            objLocDo.LocationID = LocationID;

            objLocDo.ShortDiscription = ShortDescription;

            objLocDo.CurrentUser = userid;

            int retVal = objTechService.SaveEditLocation(objLocDo);

            return retVal;

        }




    }
    [WebMethod]
    public int PMS_Insert_AssignLocation(string ShortCode, string ShortDesc, string SystemCode, string SubSysCode, int VesselID, string CatCode, int CreatedBy)
    {
        using (SMS.Business.PURC.BLL_PURC_Purchase objTechService = new SMS.Business.PURC.BLL_PURC_Purchase())
        {

            int retVal = objTechService.PMS_Insert_AssignLocation(ShortCode, ShortDesc, SystemCode, SubSysCode, VesselID, CatCode, CreatedBy);
            return retVal;
        }


    }

    [WebMethod]
    public int PMS_Insert_AssignEForm(int? ID, int? Vessel_ID, int? Job_ID, int? Form_ID, int? ChkStatus, int? UserID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objTechSer = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        int retVal = objTechSer.LibrarySaveJob_eForm_Mapping(ID, Vessel_ID, Job_ID, Form_ID, ChkStatus, UserID);
        return retVal;
    }
    [WebMethod]
    public int PMS_Update_AssignLocationStatus(string systemcode, string SubSystemCode, string CategoryCode, int ModifiedBy, int? LocationCode, int VesselID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objJobs = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        return objJobs.PMS_Update_AssignLocationStatus(systemcode, SubSystemCode, CategoryCode, ModifiedBy, LocationCode, VesselID);
    }
    [WebMethod]
    public int LibraryCatalogueLocationAssignSave(int userid, string systemcode, string SubSystemCode, int? locationid, int vesselid, string Category_Code)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objJobs = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        return objJobs.LibraryCatalogueLocationAssignSave(userid, systemcode, SubSystemCode, locationid, vesselid, Category_Code);
    }

    [WebMethod]
    public int LibraryCatalogueLocationAssignDelete(int userid, int AssignlocationID, int vesselcode)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objJobs = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        return objJobs.LibraryCatalogueLocationAssignDelete(userid, AssignlocationID, vesselcode);
    }

    [WebMethod]
    public string LibraryItemSave(int userid, string systemcode, string subsystemcode, string partnumber, string name, string description, string drawingnumber,
                string unit, decimal? inventorymin, decimal? inverntorymax, string vesselid, int? itemcategory, string image_url, string product_details, int? critical_flag)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objLibCat = new SMS.Business.PURC.BLL_PURC_Purchase();
        return objLibCat.LibraryItemSave(userid, systemcode, subsystemcode, partnumber, name, description, drawingnumber, unit, inventorymin, inverntorymax, vesselid, itemcategory, image_url, product_details, critical_flag);
    }

    [WebMethod]
    public int LibraryItemUpdate(int userid, string itemid, string systemcode, string subsystemcode, string partnumber, string name, string description, string drawingnumber,
         string unit, decimal? inventorymin, decimal? inventorymax, string vesselid, int? itemcategory, string image_url, string product_details, int? critical_flag)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objLibCat = new SMS.Business.PURC.BLL_PURC_Purchase();
        return objLibCat.LibraryItemUpdate(userid, itemid, systemcode, subsystemcode, partnumber, name, description, drawingnumber, unit, inventorymin, inventorymax, vesselid, itemcategory, image_url, product_details, critical_flag);
    }

    [WebMethod]
    public int LibraryCatalogueSave(int userid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
          , string dept, string vesselid, string functionid, string accountcode, int? AddSubSysFlag, string SerialNumber, int IsLocationRequired, int Run_Hour, int Critical)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objLibCat = new SMS.Business.PURC.BLL_PURC_Purchase();


        return objLibCat.LibraryCatalogueSave(userid, systemcode, systemdesc, stystemparticular, maker, setinstalled, model, dept, vesselid, functionid, accountcode, AddSubSysFlag, SerialNumber, IsLocationRequired, Run_Hour, Critical);
    }

    //Overloaded function doesn't work in webservice.
    //Commented as per discussion with Someshwar

    //   [WebMethod(MessageName = "PMSInsert")]
    //public int PMS_Insert_NewSystem(int userid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
    //      , string dept, string vesselid, string functionid, string accountcode, int? AddSubSysFlag, string SerialNumber, int IsLocationRequired, int Run_Hour, int Critical)
    //{
    //    SMS.Business.PURC.BLL_PURC_Purchase objLibCat = new SMS.Business.PURC.BLL_PURC_Purchase();


    //    return objLibCat.PMS_Insert_NewSystem(userid, systemcode, systemdesc, stystemparticular, maker, setinstalled, model, dept, vesselid, functionid, accountcode, AddSubSysFlag, SerialNumber, IsLocationRequired, Run_Hour, Critical);
    //}
    [WebMethod]
    public int PMS_Insert_NewSystem(int userid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
          , string dept, string vesselid, string functionid, string accountcode, int? AddSubSysFlag, string SerialNumber, int IsLocationRequired, int Run_Hour, int Critical, string DeptType, string ServiceAccount)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objLibCat = new SMS.Business.PURC.BLL_PURC_Purchase();


        return objLibCat.PMS_Insert_NewSystem(userid, systemcode, systemdesc, stystemparticular, maker, setinstalled, model, dept, vesselid, functionid, accountcode, AddSubSysFlag, SerialNumber, IsLocationRequired, Run_Hour, Critical, DeptType, ServiceAccount);
    }
    [WebMethod]
    public int LibraryCatalogueUpdate(int userid, int systemid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
           , string dept, string vesselid, string functionid, string accountcode, string SerialNumber, int Run_Hour, int Critical)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objLibCat = new SMS.Business.PURC.BLL_PURC_Purchase();

        return objLibCat.LibraryCatalogueUpdate(userid, systemid, systemcode, systemdesc, stystemparticular, maker, setinstalled, model, dept, vesselid, functionid, accountcode, SerialNumber, Run_Hour, Critical);
    }
    [WebMethod]
    public int PMS_Update_System(int userid, int systemid, string systemcode, string systemdesc, string stystemparticular, string maker, string setinstalled, string model
           , string dept, string vesselid, string functionid, string accountcode, string SerialNumber, int Run_Hour, int Critical, string ServiceAccountCode)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objLibCat = new SMS.Business.PURC.BLL_PURC_Purchase();

        return objLibCat.PMS_Update_System(userid, systemid, systemcode, systemdesc, stystemparticular, maker, setinstalled, model, dept, vesselid, functionid, accountcode, SerialNumber, Run_Hour, Critical, ServiceAccountCode);
    }
    [WebMethod]
    public int LibrarySubCatalogueSave(int userid, string systemcode, string subsystemcode, string substystemdesc, string subsytemparticular, string Maker, string Model, string SerialNo, int IsLocationRequired, int Run_Hour, int Critical, int Copy_Run_Hour)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objLibCat = new SMS.Business.PURC.BLL_PURC_Purchase();
        return objLibCat.LibrarySubCatalogueSave(userid, systemcode, subsystemcode, substystemdesc, subsytemparticular, Maker, Model, SerialNo, IsLocationRequired, Run_Hour, Critical, Copy_Run_Hour);
    }
    [WebMethod]
    public int PMS_Insert_NewSubSystem(int userid, string systemcode, string substystemdesc, string subsytemparticular, string Maker, string Model, string SerialNo, int IsLocationRequired, int Run_Hour, int Critical, string setInstalled)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objLibCat = new SMS.Business.PURC.BLL_PURC_Purchase();
        return objLibCat.PMS_Insert_NewSubSystem(userid, systemcode, substystemdesc, subsytemparticular, Maker, Model, SerialNo, IsLocationRequired, Run_Hour, Critical, setInstalled);
    }
    [WebMethod]
    public int LibrarySubCatalogueUpdate(int userid, int subsystemid, string systemcode, string subsystemcode, string substystemdesc, string subsytemparticular, string Maker, string Model, string SerialNo, int Run_Hour, int Critical, int Copy_Run_Hour)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objLibCat = new SMS.Business.PURC.BLL_PURC_Purchase();
        return objLibCat.LibrarySubCatalogueUpdate(userid, subsystemid, systemcode, subsystemcode, substystemdesc, subsytemparticular, Maker, Model, SerialNo, Run_Hour, Critical, Copy_Run_Hour);
    }
    [WebMethod]
    public int PMS_Update_SubSystem(int userid, int subsystemid, string systemcode, string subsystemcode, string substystemdesc, string subsytemparticular, string Maker, string Model, string SerialNo, int Run_Hour, int Critical, string setinstalled)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objLibCat = new SMS.Business.PURC.BLL_PURC_Purchase();
        return objLibCat.PMS_Update_SubSystem(userid, subsystemid, systemcode, subsystemcode, substystemdesc, subsytemparticular, Maker, Model, SerialNo, Run_Hour, Critical, setinstalled);
    }
    [WebMethod]
    public int LibraryJobSave(int userid, int systemid, int subsystemid, int vesselid, int deptid, int rankid, string jobtitle
        , string jobdesc, int frequency, int frequencytype, int cms, int critical, string Job_Code, int? Is_Tech_Required, int? Is_SafetyAlarm, int? Is_Calibration)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objjobs = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        return objjobs.LibraryJobSave(userid, systemid, subsystemid, vesselid, deptid, rankid, jobtitle, jobdesc, frequency, frequencytype, cms, critical, Job_Code, Is_Tech_Required, Is_SafetyAlarm, Is_Calibration);
    }


    /// <summary>
    /// Added by reshma : parameter added for RA : Is_RAMandatory & Is_RAApproval
    /// </summary>
    /// <param name="userid">user Id </param>
    /// <param name="systemid">system id for specific job</param>
    /// <param name="subsystemid">sub system id for specific job</param>
    /// <param name="vesselid">vessel id</param>
    /// <param name="deptid">Department id for specific job</param>
    /// <param name="rankid">rank id</param>
    /// <param name="jobtitle">job title</param>
    /// <param name="jobdesc">job description</param>
    /// <param name="frequency">check frequency</param>
    /// <param name="frequencytype">check frequency type</param>
    /// <param name="cms">check if job is CMS or not</param>
    /// <param name="critical">check if job is critical or not</param>
    /// <param name="Job_Code"> job code</param>
    /// <param name="Is_Tech_Required"></param>
    /// <param name="Is_SafetyAlarm">check for SafetyAlarm</param>
    /// <param name="Is_Calibration">check for calibration</param>
    /// <param name="Is_RAMandatory">check if RA is mandatory or not</param>
    /// <param name="Is_RAApproval">check if RA form is approve by office or not</param>
    /// <returns></returns>
    [WebMethod]
    public int LibraryJobSave(int userid, int systemid, int subsystemid, int vesselid, int deptid, int rankid, string jobtitle
        , string jobdesc, int frequency, int frequencytype, int cms, int critical, string Job_Code, int? Is_Tech_Required, int? Is_SafetyAlarm, int? Is_Calibration, int? Is_RAMandatory, int? Is_RAApproval)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objjobs = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        return objjobs.LibraryJobSave(userid, systemid, subsystemid, vesselid, deptid, rankid, jobtitle, jobdesc, frequency, frequencytype, cms, critical, Job_Code, Is_Tech_Required, Is_SafetyAlarm, Is_Calibration, Is_RAMandatory, Is_RAApproval);
    }

    [WebMethod]
    public int LibraryJobUpdate(int userid, int jobsid, int systemid, int subsystemid, int vesselid, int deptid, int rankid, string jobtitle
       , string jobdesc, int frequency, int frequencytype, int cms, int critical, string Job_Code, int? Is_Tech_Required, int? Is_SafetyAlarm, int? Is_Calibration)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objjobs = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        return objjobs.LibraryJobUpdate(userid, jobsid, systemid, subsystemid, vesselid, deptid, rankid, jobtitle, jobdesc, frequency, frequencytype, cms, critical, Job_Code, Is_Tech_Required, Is_SafetyAlarm, Is_Calibration);
    }
    /// <summary>
    /// Added by reshma : parameter added for RA : Is_RAMandatory & Is_RAApproval
    /// </summary>
    /// <param name="userid">user Id </param>
    /// <param name="systemid">system id for specific job</param>
    /// <param name="subsystemid">sub system id for specific job</param>
    /// <param name="vesselid">vessel id</param>
    /// <param name="deptid">Department id for specific job</param>
    /// <param name="rankid">rank id</param>
    /// <param name="jobtitle">job title</param>
    /// <param name="jobdesc">job description</param>
    /// <param name="frequency">check frequency</param>
    /// <param name="frequencytype">check frequency type</param>
    /// <param name="cms">check if job is CMS or not</param>
    /// <param name="critical">check if job is critical or not</param>
    /// <param name="Job_Code"> job code</param>
    /// <param name="Is_Tech_Required"></param>
    /// <param name="Is_SafetyAlarm">check for SafetyAlarm</param>
    /// <param name="Is_Calibration">check for calibration</param>
    /// <param name="Is_RAMandatory">check if RA is mandatory or not</param>
    /// <param name="Is_RAApproval">check if RA form is approve by office or not</param>
    /// <returns></returns>
    [WebMethod]
    public int LibraryJobUpdate(int userid, int jobsid, int systemid, int subsystemid, int vesselid, int deptid, int rankid, string jobtitle
       , string jobdesc, int frequency, int frequencytype, int cms, int critical, string Job_Code, int? Is_Tech_Required, int? Is_SafetyAlarm, int? Is_Calibration, int? Is_RAMandatory, int? Is_RAApproval)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objjobs = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        return objjobs.LibraryJobUpdate(userid, jobsid, systemid, subsystemid, vesselid, deptid, rankid, jobtitle, jobdesc, frequency, frequencytype, cms, critical, Job_Code, Is_Tech_Required, Is_SafetyAlarm, Is_Calibration, Is_RAMandatory, Is_RAApproval);
    }


    [WebMethod]
    public PMSJobsCount PMs_Get_OverdueJobsCount(int? jobid, int? fleetcode, string[] dtVessel, int? Function_ID, int? System_ID, int? System_Location_ID, int? SubSystem_ID, int? SubSystem_Location_ID,
                                           string[] DTCF_RANKID, string SearchJobID, string SearchJobTitle, int IsCritical, int IsCMS, string fromdate, string todate, int? IsHistory,
                                           int? JobStatus, string DueDateFlageSearch, int? PendingOfcVerify, int? SafetyAlarm, int? Calibration, string advfromdate, string advtodate, int? PostponeJob, int? FollowupRAdded,
                                           int? JobWithMandateRiskAssess, int? JobWithSubRiskAssess, int? JobWithDDock, int? Is_RAMandatory, int? Is_RASubmitted)
    {
        BLL_PMS_Job_Status objBl = new BLL_PMS_Job_Status();

        int res;
        JobsCount = new PMSJobsCount();
        int odjobcount = 1;
        int critodjobcount = 1;
        int totaljobcount = 1;
        res = objBl.PMs_Get_OverdueJobsCount(jobid, fleetcode, ConvertListToDataTable(dtVessel), Function_ID, System_ID, System_Location_ID, SubSystem_ID, SubSystem_Location_ID, ConvertListToDataTable(DTCF_RANKID), UDFLib.ConvertStringToNull(SearchJobID), UDFLib.ConvertStringToNull(SearchJobTitle), IsCritical, IsCMS, UDFLib.ConvertDateToNull(fromdate), UDFLib.ConvertDateToNull(todate), IsHistory, JobStatus, DueDateFlageSearch, PendingOfcVerify, SafetyAlarm, Calibration, UDFLib.ConvertDateToNull(advfromdate), UDFLib.ConvertDateToNull(advtodate), PostponeJob, FollowupRAdded, JobWithMandateRiskAssess, JobWithSubRiskAssess, JobWithDDock, Is_RAMandatory, Is_RASubmitted, ref totaljobcount, ref odjobcount, ref critodjobcount);
        try
        {
            JobsCount.TotalJobsCount = totaljobcount.ToString();
            JobsCount.OverdueJobsCount = odjobcount.ToString();
            JobsCount.CriticalJobsCount = critodjobcount.ToString();

        }
        catch (Exception ex)
        {
            // throw ex;
        }

        return JobsCount;
    }
    static DataTable ConvertListToDataTable(string[] list)
    {
        // New table.
        DataTable table = new DataTable();
        // Get max columns.

        table.Columns.Add();

        // Add rows.
        foreach (var array in list)
        {
            table.Rows.Add(array);
        }
        return table;
    }

    #endregion


    #region . log

    [WebMethod]
    public string Get_ERLog_Details_Values_Changes(string Log_ID, string Vessel_ID, string Column_Name)
    {

        DataTable dt = BLL_Tec_ErLog.Get_ERLog_Details_Values_Changes(UDFLib.ConvertToInteger(Log_ID), UDFLib.ConvertToInteger(Vessel_ID), Column_Name);

        return UDFLib.CreateHtmlTableFromDataTable(dt
                              , new string[] { "Old Value", "New Value", "PC Name", "Modified By" }
                             , new string[] { "OLD_VALUE", "NEW_VAULE", "PC_Name", "Created_By" }, "");
    }

    [WebMethod]
    public string Get_ERLogME_02_Values_Changes(string ID, string Log_ID, string Vessel_ID, string Column_Name)
    {

        DataTable dt = BLL_Tec_ErLog.Get_ERLogME_02_Values_Changes(UDFLib.ConvertToInteger(ID), UDFLib.ConvertToInteger(Log_ID), UDFLib.ConvertToInteger(Vessel_ID), Column_Name);

        return UDFLib.CreateHtmlTableFromDataTable(dt
                               , new string[] { "Old Value", "New Value", "PC Name", "Modified By" }
                             , new string[] { "OLD_VALUE", "NEW_VAULE", "PC_Name", "Created_By" }, "");

    }
    [WebMethod]
    public string Get_ERLogME_01_Values_Changes(string ID, string Log_ID, string Vessel_ID, string Column_Name)
    {

        DataTable dt = BLL_Tec_ErLog.Get_ERLogME_01_Values_Changes(UDFLib.ConvertToInteger(ID), UDFLib.ConvertToInteger(Log_ID), UDFLib.ConvertToInteger(Vessel_ID), Column_Name);

        return UDFLib.CreateHtmlTableFromDataTable(dt
                             , new string[] { "Old Value", "New Value", "PC Name", "Modified By" }
                            , new string[] { "OLD_VALUE", "NEW_VAULE", "PC_Name", "Created_By" }, "");

    }
    [WebMethod]
    public string Get_ERLogME_TB_Values_Changes(string ID, string Log_ID, string Vessel_ID, string Column_Name)
    {
        DataTable dt = BLL_Tec_ErLog.Get_ERLogME_TB_Values_Changes(UDFLib.ConvertToInteger(ID), UDFLib.ConvertToInteger(Log_ID), UDFLib.ConvertToInteger(Vessel_ID), Column_Name);
        return UDFLib.CreateHtmlTableFromDataTable(dt
                              , new string[] { "Old Value", "New Value", "PC Name", "Modified By" }
                             , new string[] { "OLD_VALUE", "NEW_VAULE", "PC_Name", "Created_By" }, "");

    }
    [WebMethod]
    public string Get_ERLog_AC_FW_MISC_Values_Changes(string ID, string Log_ID, string Vessel_ID, string Column_Name)
    {

        DataTable dt = BLL_Tec_ErLog.Get_ERLog_AC_FW_MISC_Values_Changes(UDFLib.ConvertToInteger(ID), UDFLib.ConvertToInteger(Log_ID), UDFLib.ConvertToInteger(Vessel_ID), Column_Name);

        return UDFLib.CreateHtmlTableFromDataTable(dt
                               , new string[] { "Old Value", "New Value", "PC Name", "Modified By" }
                             , new string[] { "OLD_VALUE", "NEW_VAULE", "PC_Name", "Created_By" }, "");

    }
    [WebMethod]
    public string Get_ERLog_TASG_Values_Changes(string ID, string Log_ID, string Vessel_ID, string Column_Name)
    {

        DataTable dt = BLL_Tec_ErLog.Get_ERLog_TASG_Values_Changes(UDFLib.ConvertToInteger(ID), UDFLib.ConvertToInteger(Log_ID), UDFLib.ConvertToInteger(Vessel_ID), Column_Name);
        return UDFLib.CreateHtmlTableFromDataTable(dt
                             , new string[] { "Old Value", "New Value", "PC Name", "Modified By" }
                             , new string[] { "OLD_VALUE", "NEW_VAULE", "PC_Name", "Created_By" }, "");
    }
    [WebMethod]
    public string Get_ERLog_GTREngine_Values_Changes(string ID, string Log_ID, string Vessel_ID, string Column_Name)
    {

        DataTable dt = BLL_Tec_ErLog.Get_ERLog_GTREngine_Values_Changes(UDFLib.ConvertToInteger(ID), UDFLib.ConvertToInteger(Log_ID), UDFLib.ConvertToInteger(Vessel_ID), Column_Name);

        return UDFLib.CreateHtmlTableFromDataTable(dt
                               , new string[] { "Old Value", "New Value", "PC Name", "Modified By" }
                             , new string[] { "OLD_VALUE", "NEW_VAULE", "PC_Name", "Created_By" }, "");

    }
    [WebMethod]
    public string Get_ERLog_TankLevel_Values_Changes(string ID, string Log_ID, string Vessel_ID, string Column_Name)
    {

        DataTable dt = BLL_Tec_ErLog.Get_ERLog_TankLevel_Values_Changes(UDFLib.ConvertToInteger(ID), UDFLib.ConvertToInteger(Log_ID), UDFLib.ConvertToInteger(Vessel_ID), Column_Name);
        return UDFLib.CreateHtmlTableFromDataTable(dt
                             , new string[] { "Old Value", "New Value", "PC Name", "Modified By" }
                             , new string[] { "OLD_VALUE", "NEW_VAULE", "PC_Name", "Created_By" }, "");
    }

    #endregion

    #region . Equipment statistics


    [WebMethod]
    public string Get_Lib_Items__EQPState(string systemcode, string subsystemcode, int? vesselid, string partnumber, string drawingnumber, string name, string longdesc, int? IsActive, string sortby, int? sortdirection, int pagenumber, int pagesize, int? isfetchcount, int rowcount, string TableID)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objBl = new SMS.Business.PURC.BLL_PURC_Purchase();

        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();


        //UDCHyperLinkImage lnk = new UDCHyperLinkImage("Image_Url_Att", "../Uploads/PURC_Items/", new string[] { "Image_Url" }, new string[] { "Image_Url" }, "", "../../Images/attach-icon.png");
        //UDCHyperLinkImage lnk1 = new UDCHyperLinkImage("Product_Details_Att", "../Uploads/PURC_Items/", new string[] { "Product_Details" }, new string[] { "Product_Details" }, "", "../../Images/attach-icon.png");
        //dicLink.Add("Image_Url_Att", lnk);
        //dicLink.Add("Product_Details_Att", lnk1);

        //dicLink.Add("Job_Title", lnk);
        // List<UDCActionNew> dicAction = new List<UDCActionNew>();
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "AddSPMAttachment", "onclick" } }, "../../Images/AddAttchment.png", new string[] { "Task_ID", "Assignment_ID" }));
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "ASync_Get_Remark", "onmouseover" }, new string[] { "ShowAddRemark", "onclick" } }, "../../Images/remark.png", new string[] { "Task_ID" }));
        //   dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onEditSpare", "onclick" } }, "../../Images/edit.gif", new string[] { "ID" }, "Edit"));
        // dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onDeleteSpare", "onclick" } }, "../../Images/Delete2.gif", new string[] { "ID" }, "Delete"));
        //dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onRestoreSpare", "onclick" } }, "../../purchase/Image/restore.png", new string[] { "ID" }, "Restore"));
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "Async_Get_Task_More_Information", "onmouseover" } }, "../../Images/MoreInfo.png", new string[] { "Task_ID" }));
        // Dictionary<string, UDCCheckBox> dicCheckBox = new Dictionary<string, UDCCheckBox>();

        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();
        //  dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onEditSpare", "onclick" } }, new string[] { "ID" }, "ID"));
        // dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onDeleteSpare", "onclick" } }, new string[] { "ID" }, "ID"));
        //dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onRestoreSpare", "onclick" } }, new string[] { "ID" }, "ID"));


        UDCToolTip TP = new UDCToolTip("Short_Description", "Short_Description", false);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();
        dicToolTip.Add("Short_Description", TP);
        // DataTable dt=objBl.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount).Tables[0];
        string result = UDFLib.CreateHtmlTableFromDataTable(objBl.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount).Tables[0],
           new string[] { "Drawing Number", "Part Number=", "Name=", "Unit", "Critical", "MinQty", "MaxQty", "ROB" },
           new string[] { "Drawing_Number", "Part_Number", "Short_Description", "Unit_and_Packings", "Critical_Flag", "Inventory_Min", "Inventory_Max", "ROB" },
           dicLink,
            dicToolTip,
           new string[] { "left", "left", "left", "left", "left", "left", "left", "left", "left", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "RowStyle-css", "AlternatingRowStyle-css");

        return result + "~totalrecordfound~" + rowcount.ToString();

    }

    [WebMethod]
    public string Get_EQP_Planned_Jobs(string Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();


        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();


        return UDFLib.CreateHtmlTableFromDataTable(objBl.Get_EQP_Planned_Jobs(int.Parse(Vessel_ID), SystemID, SubSystemID, SystemLocation, SubSystemLocation),
           new string[] { "Sub-System", "Job Code", "Job Title", "Job Description", "Done Date" },
           new string[] { "SUBLocation", "Job_Code", "JOB_TITLE", "Job_Description", "LAST_DONE" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "left", "left", "left", "left" },
        "tbl-common-Css",
           "hdr-common-Css",
           "RowStyle-css", "AlternatingRowStyle-css");
    }
    [WebMethod]
    public string Get_EQP_UnPlanned_Jobs(string Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();


        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();


        return UDFLib.CreateHtmlTableFromDataTable(objBl.Get_EQP_UnPlanned_Jobs(int.Parse(Vessel_ID), SystemID, SubSystemID, SystemLocation, SubSystemLocation),
           new string[] { "Sub-System", "Job Code", "Job Title", "Job Description", "Done Date" },
           new string[] { "SUBLocation", "Job_Code", "JOB_TITLE", "Job_Description", "LAST_DONE" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "left", "left", "left", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "RowStyle-css", "AlternatingRowStyle-css");
    }

    [WebMethod]
    public string Get_EQP_Requisitions(string Vessel_ID, string SystemID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();


        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();


        return UDFLib.CreateHtmlTableFromDataTable(objBl.Get_EQP_Requisitions(int.Parse(Vessel_ID), SystemID),
           new string[] { "Requisition Code", "Reqsn Date", "Order Code", "Approved On", "Total Items" },
           new string[] { "REQUISITION_CODE", "DOCUMENT_DATE", "ORDER_CODE", "APPROVED_DATE", "TOTAL_ITEMS" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "left", "left", "left", "left" },
            "tbl-common-Css",
           "hdr-common-Css",
           "RowStyle-css", "AlternatingRowStyle-css");
    }

    [WebMethod]
    public string Get_EQP_Run_Hours(string Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();


        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();


        return UDFLib.CreateHtmlTableFromDataTable(objBl.Get_EQP_Run_Hours(int.Parse(Vessel_ID), SystemID, SubSystemID, SystemLocation, SubSystemLocation),
           new string[] { "Machine", "Date", "Runing Hours", },
           new string[] { "Description", "Date_Hours_Read", "Runing_Hours" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "left", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "RowStyle-css", "AlternatingRowStyle-css");
    }

    [WebMethod]
    public string Get_EQP_Spare_Consumption(string Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();


        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();


        return UDFLib.CreateHtmlTableFromDataTable(objBl.Get_EQP_Spare_Consumption(int.Parse(Vessel_ID), SystemID, SubSystemID, SystemLocation, SubSystemLocation),
           new string[] { "Machine", "Date", "Consumption", },
           new string[] { "Description", "Created_On", "Used_Qty" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "left", "left" },
            "tbl-common-Css",
           "hdr-common-Css",
           "RowStyle-css", "AlternatingRowStyle-css");
    }

    [WebMethod]
    public string Get_EQP_Replacement_History(string Vessel_ID, string Location)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();


        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();


        return UDFLib.CreateHtmlTableFromDataTable(objBl.Get_EQP_Replacement_History(int.Parse(Vessel_ID), Int32.Parse(Location)),
           new string[] { "Replaced Equipment", "Replaced with", "User", "Date", "Remark" },
           new string[] { "SPARE_EQUIPMENT", "ACTIVE_EQUIPMENT", "USERNAME", "RPDATE", "REMARK" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "left", "left", "left", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "RowStyle-css", "AlternatingRowStyle-css");
    }


    [WebMethod]
    public string Get_Machinery_Details(string Vessel_ID, string SystemID, string SubSystemID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        return UDFLib.CreateHtmlTableFromDataTableWithCustomizeError(objBl.Get_Machinery_Details(int.Parse(Vessel_ID), SystemID, SubSystemID),
            new string[] { "System Particulars", "Model", "Serial Number", "Maker", "Account Code" },
           new string[] { "System_Particulars", "Module_Type", "Serial_Number", "Full_NAME", "Budget_Name" },
           new string[] { "left", "left", "left", "left", "left" }, "Function Level Selected <br> Select System To View Details <br> Click <b>Add System</b> to Add New System",
           "tbl-common-Css",
           "hdr-common-Css-Vertical",
           "row-common-Css-Vertical",
           UDTRepeatDirection.Vertical);


    }


    [WebMethod]
    public string Get_SubMachinery_Details(string Vessel_ID, string SystemID, string SubSystemID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        return UDFLib.CreateHtmlTableFromDataTableWithCustomizeError(objBl.Get_Machinery_Details(int.Parse(Vessel_ID), SystemID, SubSystemID),
            new string[] { "System Particulars", "Model", "Serial Number", "Maker" },
           new string[] { "System_Particulars", "Module_Type", "Serial_Number", "Full_NAME" },
           new string[] { "left", "left", "left", "left" }, "Function Level Selected <br> Select System To View Details <br> Click <b>Add System</b> to Add New System",
           "tbl-common-Css",
           "hdr-common-Css-Vertical",
           "row-common-Css-Vertical",
           UDTRepeatDirection.Vertical);
    }
    [WebMethod]
    public string Get_EQP_Lib_Planned_Jobs(string Vessel_ID, string SystemID, string SubSystemID, string SystemLocation, string SubSystemLocation)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();

        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();

        return UDFLib.CreateHtmlTableFromDataTable(objBl.Get_EQP_Lib_Planned_Jobs(int.Parse(Vessel_ID), SystemID, SubSystemID, SystemLocation, SubSystemLocation),
           new string[] { "Sub-System", "Job Code", "Job Title", "Job Description", "Frequency Type", "Frequency", "Critical", "Done Date" },
           new string[] { "SUBLocation", "Job_Code", "JOB_TITLE", "Job_Description", "Frequency_Name", "FREQUENCY", "Critical", "LAST_DONE" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "left", "left", "left", "left", "left", "left", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "RowStyle-css", "AlternatingRowStyle-css");
    }


    #endregion




    [WebMethod]
    public string Upd_Convert_Audio_To_Text(string Vessel_ID, string Worklist_ID, string Followup_ID, string WL_Office_ID, string Followup, string UserID, string AttachPath, string Action)
    {
        BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
        int sts = objBLL.Upd_Worklist_Attachment_FOLLOWUP(Convert.ToInt32(Vessel_ID), Convert.ToInt32(Followup_ID), Convert.ToInt32(WL_Office_ID), Convert.ToInt32(UserID), Action, AttachPath, Action, Convert.ToInt32(Worklist_ID), Followup);
        return sts.ToString();
    }

    [WebMethod]
    public string[] asyncLoadWoklistCounts(int ResultCateg)
    {

        int Record_Count = 0;
        BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
        objBLL.Get_WorkList_Index_Filter(ResultCateg, 1, null, ref Record_Count);
        string[] result = { Record_Count.ToString() };
        return result;
    }
    #region CONVERT DATATABLE TO JSON DATA

    private string GetJsArray(DataTable dt)
    {
        string res = "";
        string sColumnValues = "";

        foreach (DataRow dr in dt.Rows)
        {
            if (res.Length > 0) res += ",";
            res += "{";
            sColumnValues = "";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (sColumnValues.Length > 0) sColumnValues += ",";
                sColumnValues += dt.Columns[i].ColumnName.ToString() + ":'" + dr[i].ToString() + "'";
            }
            res += ReplaceSpecialCharacter(sColumnValues);
            res += "}";
        }
        if (!string.IsNullOrEmpty(res))
        {
            res = "[" + res + "]";
        }
        return res;

    }


    public string ConvertDataTabletoJson(DataTable dtData)
    {


        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dtData.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dtData.Columns)
            {
                if (col.DataType.ToString() == "System.DateTime")
                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                else
                    row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }
    public static string ReplaceSpecialCharacter(string str)
    {
        //return str.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;").Replace("\"", "&quot;");
        string ret = str.Replace(@"\", @"\\");
        return ret;
    }

    #endregion

    //Added By Vasu 07/03/2016
    [WebMethod]
    public string asyncGet_Function_Information(string Worklist_ID, string WL_Office_ID, string Vessel_ID)
    {
        BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
        string sts = objBLL.asyncGet_Function_Information(Convert.ToInt32(Worklist_ID), Convert.ToInt32(WL_Office_ID), Convert.ToInt32(Vessel_ID));
        return sts.ToString();
    }
    //Adding End by vasu

    [WebMethod]
    public string Get_Department_By_System_Code(string systemid)
    {

        SMS.Business.PURC.BLL_PURC_Purchase objBl = new SMS.Business.PURC.BLL_PURC_Purchase();
        string res = objBl.Get_Department((systemid));
        return res;

    }

    // Added by Reshma JIT : 11222
    [WebMethod]
    public int PMS_GET_JobsPerformOnLocation(int? SystemLocationID, int? SubSysLocationID, int VESSELID)
    {
        SMS.Business.PMS.BLL_PMS_Library_Jobs objJobs = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        return objJobs.PMS_GET_JobsPerformOnLocation(SystemLocationID, SubSysLocationID, VESSELID);
    }

    
    // Prashant -- 20/02/2017
    #region .Purchase Manage system New
    public List<Account> Accountslist { get; set; }
    public List<ReqsnTypeAccess> ReqsnType_Access { get; set; }
    public class ReqsnTypeAccess
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string Access_Code { get; set; }
        public string VARIABLE_TYPE { get; set; }
        public string VARIABLE_CODE { get; set; }
        public string variable_Name { get; set; }
        public string IsAccess { get; set; }
        public string Access_Add { get; set; }
        public string Access_Edit { get; set; }
        public string Access_Delete { get; set; }
        public string Access_Approve { get; set; }
        public string Access_Admin { get; set; }
        public string Reqsn_Short_Code { get; set; }
        public string IsAdminUser { get; set; }

    }


    /// <summary>
    /// gets the Catalogue Account Code and service account Code 
    /// </summary>
    [WebMethod]
    public List<Account> Get_CatalogueAccountCode()
    {
        Accountslist = new List<Account>();
        DataTable dt = SMS.Business.PURC.BLL_PURC_Common.PURC_Get_Sys_Variable(0, "Account_Classification");

        try
        {
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Accountslist.Add(new Account()
                        {

                            BUDGET_CODE = dr["VARIABLE_Code"].ToString(),
                            BUDGET_NAME = dr["VARIABLE_NAME"].ToString(),

                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //throw ex;		
        }
        return Accountslist;
    }

    /// <summary>
    /// Search Functions
    /// </summary>
    [WebMethod]
    public List<SystemParameter> LibraryGetFunctionsList(string searchtext)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objBl = new SMS.Business.PURC.BLL_PURC_Purchase();

        DataTable dt = new DataTable();
        SystemParameters = new List<SystemParameter>();
        int a = 0;
        dt = objBl.Get_Purc_LIB_Functions(searchtext, "", "", "", 0, 0, 0, ref a).Tables[0];
        try
        {
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SystemParameters.Add(new SystemParameter()
                        {
                            CODE = dr["ID"].ToString(),
                            SHORT_CODE = dr["Function_Short_Code"].ToString(),
                            DESCRIPTION = dr["Function_Name"].ToString()
                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            // throw ex;
        }

        return SystemParameters;
    }

    /// <summary>
    /// Get Configured requisition Types 
    /// </summary>
    [WebMethod]
    public DataTable Get_Configured_ReqsnType()
    {
        SMS.Business.PURC.BLL_PURC_Config_PO objBl = new SMS.Business.PURC.BLL_PURC_Config_PO();
        return objBl.PURC_Get_Configured_ReqsnType().Tables[0];

    }

    /// <summary>
    ///  Checks the requisition Type of the Function/Department selected    
    /// </summary>
    [WebMethod]
    public bool CheckFunctionType(string id)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objBl = new SMS.Business.PURC.BLL_PURC_Purchase();
        return objBl.CheckFunctionType(id);
    }

    [WebMethod]
    public List<Account> Get_ConfigAccount_Type(string user, string reqsntype)
    {
        SMS.Business.PURC.BLL_PURC_Config_PO objBl = new SMS.Business.PURC.BLL_PURC_Config_PO();



        Accountslist = new List<Account>();
        DataTable dt = objBl.PURC_Get_Configured_AccountType(user, reqsntype).Tables[0];

        try
        {
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Accountslist.Add(new Account()
                        {

                            BUDGET_CODE = dr["VARIABLE_CODE"].ToString(),
                            BUDGET_NAME = dr["VARIABLE_NAME"].ToString(),

                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //throw ex;		
        }
        return Accountslist;
    }

    //[WebMethod]
    //public string Get_Functional_Tree_ManageSystem_Data(string vesselid, string searchText, int? IsActive, string Form_Type)
    //{

    //    BLL_PMS_Library_Items objItems = new BLL_PMS_Library_Items();
    //    DataTable dt = objItems.Get_Functional_Tree_ManageSystem_Data(int.Parse(vesselid), searchText, IsActive, Form_Type);

    //    return ConvertDataTabletoJson(dt);
    //}

    [WebMethod]
    public string Get_Functional_Tree_Data_ManageSystems(string vesselid, string searchText, int? IsActive, string Form_Type)
    {

        BLL_PMS_Library_Items objItems = new BLL_PMS_Library_Items();
        DataTable dt = objItems.Get_Functional_Tree_Data_ManageSystems(int.Parse(vesselid), searchText, IsActive, Form_Type);

        return ConvertDataTabletoJson(dt);

    }

    [WebMethod]
    public List<ReqsnTypeAccess> BindReqsnList(int userid, string reqsn_type)
    {
        BLL_PURC_Permissions objbllPrmsion = new BLL_PURC_Permissions();
        ReqsnType_Access = new List<ReqsnTypeAccess>();
        DataTable dt = objbllPrmsion.PURC_Get_UserTypeAccess(userid, reqsn_type);
        try
        {
            if (dt != null)
            {

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ReqsnType_Access.Add(new ReqsnTypeAccess()
                        {

                            ID = dr["ID"].ToString(),
                            UserID = dr["UserID"].ToString(),
                            Access_Code = dr["Access_Code"].ToString(),
                            VARIABLE_TYPE = dr["VARIABLE_TYPE"].ToString(),
                            VARIABLE_CODE = dr["VARIABLE_CODE"].ToString(),
                            variable_Name = dr["variable_Name"].ToString(),
                            IsAccess = dr["IsAccess"].ToString(),
                            Access_Add = dr["Access_Add"].ToString(),
                            Access_Edit = dr["Access_Edit"].ToString(),
                            Access_Delete = dr["Access_Delete"].ToString(),
                            Access_Approve = dr["Access_Approve"].ToString(),
                            Access_Admin = dr["Access_Admin"].ToString(),
                            Reqsn_Short_Code = dr["Reqsn_Short_Code"].ToString(),
                            IsAdminUser = dr["IsAdminUser"].ToString(),


                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //throw ex;		
        }
        return ReqsnType_Access;
    }

    //-- Prashant (Method Edited to put Checkbox)
    [WebMethod]
    public string Get_Lib_Items_ManageSystems(string deptcode, string systemcode, string subsystemcode, int? vesselid, string partnumber, string drawingnumber, string name, string longdesc, int? IsActive, string searchtext, string sortby, int? sortdirection, int pagenumber, int pagesize, int? isfetchcount, int rowcount, string TableID)
    {
        SMS.Business.PURC.BLL_PURC_Purchase objBl = new SMS.Business.PURC.BLL_PURC_Purchase();

        Dictionary<string, UDCHyperLinkImage> dicLink = new Dictionary<string, UDCHyperLinkImage>();


        UDCHyperLinkImage lnk = new UDCHyperLinkImage("Image_Url_Att", "../Uploads/PURC_Items/", new string[] { "Image_Url" }, new string[] { "Image_Url" }, "", "../../Images/attach-icon.png");
        UDCHyperLinkImage lnk1 = new UDCHyperLinkImage("Product_Details_Att", "../Uploads/PURC_Items/", new string[] { "Product_Details" }, new string[] { "Product_Details" }, "", "../../Images/attach-icon.png");
        dicLink.Add("Image_Url_Att", lnk);
        dicLink.Add("Product_Details_Att", lnk1);
        //dicLink.Add("Job_Title", lnk);
        List<UDCActionNew> dicAction = new List<UDCActionNew>();
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "AddSPMAttachment", "onclick" } }, "../../Images/AddAttchment.png", new string[] { "Task_ID", "Assignment_ID" }));
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "ASync_Get_Remark", "onmouseover" }, new string[] { "ShowAddRemark", "onclick" } }, "../../Images/remark.png", new string[] { "Task_ID" }));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onEditSpare", "onclick" } }, "../../Images/edit.gif", new string[] { "ID" }, "Edit"));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onDeleteSpare", "onclick" } }, "../../Images/Delete2.gif", new string[] { "ID" }, "Delete"));
        dicAction.Add(new UDCActionNew(new string[][] { new string[] { "onRestoreSpare", "onclick" } }, "../../purchase/Image/restore.png", new string[] { "ID" }, "Restore"));
        // dicAction.Add(new UDCAction(new string[][] { new string[] { "Async_Get_Task_More_Information", "onmouseover" } }, "../../Images/MoreInfo.png", new string[] { "Task_ID" }));
        Dictionary<string, UDCCheckBox> dicCheckBox = new Dictionary<string, UDCCheckBox>();





        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onEditSpare", "onclick" } }, new string[] { "ID" }, "ID"));
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onDeleteSpare", "onclick" } }, new string[] { "ID" }, "ID"));
        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "onRestoreSpare", "onclick" } }, new string[] { "ID" }, "ID"));


        UDCToolTip TP = new UDCToolTip("Short_Description", "Long_Description", false);
        Dictionary<string, UDCToolTip> dicToolTip = new Dictionary<string, UDCToolTip>();
        dicToolTip.Add("Short_Description", TP);
        string result = string.Empty;
        if (deptcode == "PR")
        {
            UDCCheckBox chk = new UDCCheckBox("Meat", "Meat", new string[][] { new string[] { "onMeatCheck", "onchange" } }, new string[] { "ID" });
            dicCheckBox.Add("Meat", chk);
            // DataTable dt=objBl.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount).Tables[0];
            result = UDFLib.CreateHtmlTableFromDataTableWithCheckBox(objBl.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, searchtext, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount).Tables[0],
              new string[] { "Drawing Number", "Part Number=", "Name=", "Unit", "Critical", "MinQty", "MaxQty", "ROB", " Img Att.", "Dtls Att.", "Meat" },
              new string[] { "Drawing_Number", "Part_Number", "Short_Description", "Unit_and_Packings", "Critical_Flag", "Inventory_Min", "Inventory_Max", "ROB", "Image_Url_Att", "Product_Details_Att", "Meat" },
              dicLink,
               dicToolTip,
              new string[] { "left", "left", "left", "left", "left", "center", "center", "center", "center", "center", "center", "center" },
              TableID,
              "tbl-common-Css",
              "PMSGridHeaderStyle-css",
              "PMSGridRowStyle-css", "PMSGridAlternatingRowStyle-css", dicAction, dicJSEvent, dicCheckBox);
        }
        else if (deptcode == "BS")
        {
            UDCCheckBox chk = new UDCCheckBox("IsSlopchest", "IsSlopchest", new string[][] { new string[] { "onSlopChestCheck", "onchange" } }, new string[] { "ID" });
            dicCheckBox.Add("IsSlopchest", chk);
            // DataTable dt=objBl.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount).Tables[0];
            result = UDFLib.CreateHtmlTableFromDataTableWithCheckBox(objBl.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, searchtext, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount).Tables[0],
               new string[] { "Drawing Number", "Part Number=", "Name=", "Unit", "Critical", "MinQty", "MaxQty", "ROB", " Img Att.", "Dtls Att.", "Slopchest Item " },
               new string[] { "Drawing_Number", "Part_Number", "Short_Description", "Unit_and_Packings", "Critical_Flag", "Inventory_Min", "Inventory_Max", "ROB", "Image_Url_Att", "Product_Details_Att", "IsSlopchest" },
               dicLink,
                dicToolTip,
               new string[] { "left", "left", "left", "left", "left", "center", "center", "center", "center", "center", "center", "center" },
               TableID,
               "tbl-common-Css",
               "PMSGridHeaderStyle-css",
               "PMSGridRowStyle-css", "PMSGridAlternatingRowStyle-css", dicAction, dicJSEvent, dicCheckBox);
        }
        else
        {
            UDCCheckBox chk = new UDCCheckBox("Selected", "Selected", new string[][] { new string[] { "onSelected", "onchange" } }, new string[] { "ID" });
            dicCheckBox.Add("Selected", chk);
            // DataTable dt=objBl.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount).Tables[0];
            result = UDFLib.CreateHtmlTableFromDataTableWithCheckBox(objBl.LibraryItemSearch(systemcode, subsystemcode, vesselid, partnumber, drawingnumber, name, longdesc, IsActive, searchtext, sortby, sortdirection, pagenumber, pagesize, isfetchcount, out rowcount).Tables[0],
              new string[] { "Drawing Number", "Part Number=", "Name=", "Unit", "Critical", "MinQty", "MaxQty", "ROB", " Img Att.", "Dtls Att.", "Select", },
               new string[] { "Drawing_Number", "Part_Number", "Short_Description", "Unit_and_Packings", "Critical_Flag", "Inventory_Min", "Inventory_Max", "ROB", "Image_Url_Att", "Product_Details_Att", "Selected", },
               dicLink,
                dicToolTip,
               new string[] { "left", "left", "left", "left", "left", "center", "center", "center", "center", "center", "center", },
               TableID,
               "tbl-common-Css",
               "PMSGridHeaderStyle-css",
               "PMSGridRowStyle-css", "PMSGridAlternatingRowStyle-css", dicAction, dicJSEvent, dicCheckBox);
        }
        return result + "~totalrecordfound~" + rowcount.ToString();

    }


    #endregion
    // Prashant -- 20/02/2017
}
