using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Web.UI.HtmlControls;
using SMS.Business.Technical;
using SMS.Business.PMS;
using System.Text;
using SMS.Properties;
using System.Data.SqlClient;
using System.IO;


public partial class Purchase_LibraryCatalogue : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUserAcess = new UserAccess();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        UserAccessValidation();


        if (!IsPostBack)
        {
            BindDeptOptList();
            BindFleetDLL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();


            optList.SelectedValue = "SP";

            BindDepartmentByST_SP();

            BindUnitPakageDDL();
            BindItemCategory();

            BindSupplierDetails();
            BindSubCatSupplierDetails();
            BindAccountCode();
            BindSubCatAccountCode();

            hdfDeptType.Value = optList.SelectedValue;

            ddlAccountCode.SelectedValue = "6100";
            ddlSubCatAccountCode.SelectedValue = "6100";

            ViewState["DeptType"] = "SP";

            ViewState["DeptCode"] = null;

            //To display Active Records
            ViewState["ActiveStatus"] = 1;
            ViewState["VesselCode"] = 0;

            ViewState["SystemCode"] = null;
            ViewState["SubSystemCode"] = null;

            ViewState["SystemId"] = null;
            ViewState["SubSystemId"] = null;
            ViewState["ItemId"] = null;

            ViewState["SelectAllItem"] = null;

            ViewState["VesselCodeEditMode"] = null;

            ViewState["CATALOGUESORTBYCOLOUMN"] = null;
            ViewState["CATALOGUESORTDIRECTION"] = null;
            ViewState["SUBCATALOGUESORTBYCOLOUMN"] = null;
            ViewState["SUBCATALOGUESORTDIRECTION"] = null;
            ViewState["ITEMSORTDIRECTION"] = null;
            ViewState["ITEMSORTBYCOLOUMN"] = null;

            ViewState["CatlogueOperationMode"] = "ADD";
            hdfCatlogueOperationMode.Value = "ADD";

            ViewState["SubCatlogueOperationMode"] = "ADD";

            ViewState["ItemOperationMode"] = "ADD";
            hdfItemOperationMode.Value = "ADD";

            ucCustomPagerItems.PageSize = 14;


            //btnDivAddLocation.Enabled = false;

            btnCatalogueAdd.Enabled = false;
            btnCatalogueSave.Enabled = false;

            btnSaveSubCatalogue.Enabled = false;
            btnAddNewSubCatalogue.Enabled = false;

            btnAddNewItem.Enabled = false;
            btnSaveItem.Enabled = false;
            btnSelectAllItem.Enabled = false;

            AssignSPMModuleID();
            //Get_Auto_Req();
        }

        lblCatalogueErrorMsg.Text = "";
        lblSubCatalogErrorMsg.Text = "";
        lblItemErrorMsg.Text = "";


    }

    protected void AssignSPMModuleID()
    {

        // Get the Absolute path of the url
        System.IO.FileInfo oInfo = new System.IO.FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath);

        // Get the Page Name
        SqlDataReader dr = BLL_Infra_Common.Get_SPM_Module_ID(oInfo.Name);

        if (dr.HasRows)
        {
            dr.Read();
            string ModuleID = dr["SPM_Module_ID"].ToString();
            //Assign the module ID into the Feeb back button.

            /* if there is ModuleID is Empty it means there is no  value of  SPM_Module_ID  coloumn in Lib_Menu table
                In this case Feedback would be record under 'COMMON' module ID = 13  under SPM bug traker. 
             */
          
        }


    }

    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUserAcess = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUserAcess.View == 0)
        {

            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUserAcess.Add == 0)
        {
            //catalogue
            btnCatalogueAdd.Enabled = false;
            btnDivAddLocation.Enabled = false;
            btnSubCatDivAddLocation.Enabled = false;
            btnCatalogueSave.Enabled = false;

            //Sub catalogue
            btnAddNewSubCatalogue.Enabled = false;
            btnSaveSubCatalogue.Enabled = false;

            //item
            btnAddNewItem.Enabled = false;
            btnSaveItem.Enabled = false;

        }

    }




    public void BindCatalogue()
    {
        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {
            string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());
            string deptcode = (ViewState["DeptCode"] == null) ? null : (ViewState["DeptCode"].ToString());

            string depttype = (ViewState["DeptType"] == null) ? null : (ViewState["DeptType"].ToString());

            int? isactivestatus = null;
            if (ViewState["ActiveStatus"] != null)
                isactivestatus = Convert.ToInt32(ViewState["ActiveStatus"]);

            string sortbycoloumn = (ViewState["CATALOGUESORTBYCOLOUMN"] == null) ? null : (ViewState["CATALOGUESORTBYCOLOUMN"].ToString());
            int? sortdirection = null;

            if (ViewState["CATALOGUESORTDIRECTION"] != null)
                sortdirection = Int32.Parse(ViewState["CATALOGUESORTDIRECTION"].ToString());

            DataSet ds = objBLLPurc.LibraryCatalogueSearch(null, txtSearchCatalogue.Text.Trim(), depttype, UDFLib.ConvertStringToNull(deptcode), Int32.Parse(DDLFleet.SelectedValue), Int32.Parse(vesselcode), "", isactivestatus, sortbycoloumn, sortdirection, 1, 500, 1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCatalogue.DataSource = ds.Tables[0];
                gvCatalogue.DataBind();

                if (ViewState["SystemId"] == null)
                {
                    ViewState["SystemId"] = ds.Tables[0].Rows[0]["ID"].ToString();
                    ViewState["SystemCode"] = ds.Tables[0].Rows[0]["System_Code"].ToString();
                    ViewState["VesselCodeEditMode"] = ds.Tables[0].Rows[0]["Vessel_Code"].ToString();
                    gvCatalogue.SelectedIndex = 0;
                }

                if ((String)ViewState["CatlogueOperationMode"] != "ADD")
                {
                    BindCatalogueList(ViewState["SystemId"].ToString());
                    BindCatalogAssingLocationDLL();


                }




                SetCatalogueRowSelection();

            }
            else
            {

                gvCatalogue.DataSource = ds.Tables[0];
                gvCatalogue.DataBind();
                ViewState["SystemId"] = null;
                ViewState["SystemCode"] = null;
                ViewState["CatlogueOperationMode"] = "ADD";
                hdfCatlogueOperationMode.Value = "ADD";

                ddlAccountCode.SelectedValue = "6100";

                //btnDivAddLocation.Enabled = false;
                btnSaveSubCatalogue.Enabled = false;
                btnAddNewSubCatalogue.Enabled = false;
                btnAddNewItem.Enabled = false;
                btnSaveItem.Enabled = false;
            }
        }
    }

    public void BindSubCatalogue()
    {

        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {

            string systemcode = (ViewState["SystemCode"] == null) ? null : (ViewState["SystemCode"].ToString());
            int? isactivestatus = null;
            if (ViewState["ActiveStatus"] != null)
                isactivestatus = Convert.ToInt32(ViewState["ActiveStatus"]);

            string sortbycoloumn = (ViewState["SUBCATALOGUESORTBYCOLOUMN"] == null) ? null : (ViewState["SUBCATALOGUESORTBYCOLOUMN"].ToString());
            int? sortdirection = null;

            if (ViewState["SUBCATALOGUESORTDIRECTION"] != null)
                sortdirection = Int32.Parse(ViewState["SUBCATALOGUESORTDIRECTION"].ToString());


            DataSet ds = objBLLPurc.LibrarySubCatalogueSearch(systemcode, txtSearchSubCatalogue.Text.Trim(), null, isactivestatus, sortbycoloumn, sortdirection, 1, 1000, 1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSubCatalogue.DataSource = ds.Tables[0];
                gvSubCatalogue.DataBind();

                if (ViewState["SubSystemId"] == null)
                {
                    ViewState["SubSystemId"] = ds.Tables[0].Rows[0]["ID"].ToString();
                    ViewState["SubSystemCode"] = ds.Tables[0].Rows[0]["Subsystem_Code"].ToString();
                    gvSubCatalogue.SelectedIndex = 0;
                }
                if ((String)ViewState["SubCatlogueOperationMode"] != "ADD")
                    BindSubCatalogueList(ViewState["SubSystemId"].ToString());


                btnSelectAllItem.Enabled = true;

                if (objUserAcess.Add == 1)
                {
                    btnSaveSubCatalogue.Enabled = true;
                    btnAddNewSubCatalogue.Enabled = true;

                    btnAddNewItem.Enabled = true;
                    btnSaveItem.Enabled = true;

                }


                SetSubCatalogueRowSelection();
            }
            else
            {
                gvSubCatalogue.DataSource = ds.Tables[0];
                gvSubCatalogue.DataBind();


                //ClearSubCatalogueFields();
                ViewState["SubSystemId"] = null;
                ViewState["SubSystemCode"] = null;
                ViewState["SubCatlogueOperationMode"] = "ADD";
                ddlSubCatAccountCode.SelectedValue = "6100";

                btnAddNewItem.Enabled = false;
                btnSaveItem.Enabled = false;
                btnSelectAllItem.Enabled = false;
            }
        }
    }

    public void BindItems()
    {

        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {
            int rowcount = 0;

            //ViewState["VesselCodeEditMode"]

            //string vesselcode = (ViewState["VesselCodeEditMode"] == null) ? null : (ViewState["VesselCodeEditMode"].ToString());
            DataSet ds;
            int? vesselcode = null;
            if (ViewState["VesselCodeEditMode"] != null && (string)ViewState["VesselCodeEditMode"].ToString() != "")
                vesselcode = Convert.ToInt32(ViewState["VesselCodeEditMode"]);


            string systemcode = (ViewState["SystemCode"] == null) ? null : (ViewState["SystemCode"].ToString());
            string subsystemcode = (ViewState["SubSystemCode"] == null) ? null : (ViewState["SubSystemCode"].ToString());
            int? isactivestatus = null;
            if (ViewState["ActiveStatus"] != null)
                isactivestatus = Convert.ToInt32(ViewState["ActiveStatus"]);

            string sortbycoloumn = (ViewState["ITEMSORTBYCOLOUMN"] == null) ? null : (ViewState["ITEMSORTBYCOLOUMN"].ToString());
            int? sortdirection = null;

            if (ViewState["ITEMSORTDIRECTION"] != null)
                sortdirection = Int32.Parse(ViewState["ITEMSORTDIRECTION"].ToString());


            if ((String)ViewState["SelectAllItem"] == "Y")
            {
                //For Selecting all the item in all subcatalog under selected catelog.

                ds = objBLLPurc.LibraryItemSearch(systemcode, null, vesselcode, null, null, txtSearchItemName.Text.Trim(), null
                   , isactivestatus, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ucCustomPagerItems.isCountRecord, out rowcount);
            }
            else
            {

                ds = objBLLPurc.LibraryItemSearch(systemcode, subsystemcode, vesselcode, null, null, txtSearchItemName.Text.Trim(), null
               , isactivestatus, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ucCustomPagerItems.isCountRecord, out rowcount);
            }


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvItem.DataSource = ds.Tables[0];
                gvItem.DataBind();

                if (ViewState["ItemId"] == null)
                {
                    ViewState["ItemId"] = ds.Tables[0].Rows[0]["ID"].ToString();
                    // ViewState["VesselCodeEditMode"] = ds.Tables[0].Rows[0]["Vessel_ID"].ToString();
                    gvItem.SelectedIndex = 0;
                }

                SetItemsRowSelection();

                if (objUserAcess.Add == 1)
                {
                    btnAddNewItem.Enabled = true;
                    btnSaveItem.Enabled = true;
                }
            }
            else
            {

                gvItem.DataSource = ds.Tables[0];
                gvItem.DataBind();
                ViewState["ItemId"] = null;
                ViewState["ItemOperationMode"] = "ADD";
                hdfItemOperationMode.Value = "ADD";
            }
        }
    }

    public void BindCatalogueList(string systemid)
    {
        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        DataSet ds = objBLLPurc.LibraryCatalogueList(systemid);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtCatalogueCode.Text = dr["System_Code"].ToString();
            txtCatalogueSetInstalled.Text = dr["Set_Instaled"].ToString();
            txtCatalogName.Text = dr["System_Description"].ToString();
            txtCatalogueParticular.Text = dr["System_Particulars"].ToString();
            ddlCalalogueMaker.SelectedValue = dr["Maker"].ToString() != "" ? dr["Maker"].ToString() : "0";
            ddlCatalogDept.SelectedValue = dr["Dept1"].ToString() != "" ? dr["Dept1"].ToString() : "0";
            txtCatalogueModel.Text = dr["Module_Type"].ToString();
            txtCatalogueSerialNumber.Text = dr["Serial_Number"].ToString();
            chkRunHourS.Checked = dr["Run_Hour"].ToString() == "1" ? true : false;
            chkCriticalS.Checked = dr["Critical"].ToString() == "1" ? true : false;
            if (chkRunHourS.Checked == true)
                ViewState["SystemRHrs"] = 1;
            else
                ViewState["SystemRHrs"] = 0;

            if ((dr["Functions"].ToString() != "") && (dr["Functions"].ToString() != "0"))
            {
                ddlCatalogFunction.SelectedValue = dr["Functions"].ToString();
            }
            else
            {
                ddlCatalogFunction.SelectedValue = "";
            }

            if ((dr["ACCOUNT_CODE"].ToString() != "") && (dr["ACCOUNT_CODE"].ToString() != "0"))
            {
                ddlAccountCode.SelectedValue = dr["ACCOUNT_CODE"].ToString();
            }
            else
            {
                ddlAccountCode.SelectedValue = "0";
            }


            lblCatalogueCreatedBy.Text = dr["CREATEDBY"].ToString();
            lblCatalogueModifiedby.Text = dr["MODIFIEDBY"].ToString();
            lblCatalogueDeletedby.Text = dr["DELETEDBY"].ToString();

        }

    }

    public void BindSubCatalogueList(string subsystemid)
    {
        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        DataSet ds = objBLLPurc.LibrarySubCatalogueList(subsystemid);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtSubCatalogueCode.Text = dr["Subsystem_Code"].ToString();
            txtSubCatalogueName.Text = dr["Subsystem_Description"].ToString();
            txtSubCatalogueParticulars.Text = dr["Subsystem_Particulars"].ToString();

            txtSubCatModel.Text = Convert.ToString(dr["Module_Type"]);
            txtSubCatSerialNo.Text = Convert.ToString(dr["Serial_Number"]);
            chkRunHourSS.Checked = dr["Run_Hour"].ToString() == "1" ? true : false;
            chkCriticalSS.Checked = dr["Critical"].ToString() == "1" ? true : false;
            chkCopyRunHourSS.Checked = dr["Copy_Run_Hour"].ToString() == "1" ? true : false;
            if (chkRunHourSS.Checked == true)
            {
                if (ViewState["SystemRHrs"].ToString().Trim() == "1")
                    chkCopyRunHourSS.Enabled = true;
            }
            ListItem limaker = ddlSubCatMaker.Items.FindByValue(Convert.ToString(dr["Maker"]));
            if (limaker != null)
            {
                ddlSubCatMaker.ClearSelection();
                limaker.Selected = true;
            }
            else
                ddlSubCatMaker.SelectedValue = "0";

            lstSubCatVesselLocation.DataSource = objBLLPurc.GET_SUBSYTEMSYSTEM_ASSIGNED_LOCATION(Convert.ToString(ViewState["SystemId"]), int.Parse(subsystemid), Convert.ToInt32(ViewState["VesselCodeEditMode"]));
            lstSubCatVesselLocation.DataTextField = "LocationName";
            lstSubCatVesselLocation.DataValueField = "ID";
            lstSubCatVesselLocation.DataBind();


            lblSubCatalogueCreatedBy.Text = dr["CREATEDBY"].ToString();
            lblSubCatalogueModifiedby.Text = dr["MODIFIEDBY"].ToString();
            lblSubCatalogueDeletedBy.Text = dr["DELETEDBY"].ToString();

        }
    }

    public void BindItemsList(string itemid)
    {
        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        DataSet ds = objBLLPurc.LibraryItemList(itemid);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtItemDrawingNumber.Text = dr["Drawing_Number"].ToString();
            txtItemPartNumber.Text = dr["PART_NUMBER"].ToString();
            txtItemName.Text = dr["SHORT_DESCRIPTION"].ToString();
            txtItemDescription.Text = dr["LONG_DESCRIPTION"].ToString();

            txtMinQty.Text = dr["INVENTORY_MIN"].ToString();
            txtMaxQty.Text = dr["INVENTORY_MAX"].ToString();



            ddlUnit.SelectedValue = dr["UNIT_AND_PACKINGS"].ToString() != "" ? dr["UNIT_AND_PACKINGS"].ToString() : "0";
            ddlItemCategory.SelectedValue = dr["Item_Category"].ToString() != "" ? dr["Item_Category"].ToString() : "0";

            lnkImageUploadName.Text = dr["Image_Url"].ToString();
            //lnkImageUploadName.NavigateUrl = @"\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + @"\Uploads\PURC_Items\" + lnkImageUploadName.Text;
            lnkImageUploadName.NavigateUrl = @"~\Uploads\PURC_Items\" + lnkImageUploadName.Text;

            lnkProductDetailUploadName.Text = dr["Product_Details"].ToString();
            //lnkProductDetailUploadName.NavigateUrl = @"\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + @"\Uploads\PURC_Items\" + lnkProductDetailUploadName.Text;
            lnkProductDetailUploadName.NavigateUrl = @"~\Uploads\PURC_Items\" + lnkProductDetailUploadName.Text;
            chkItemCritical.Checked = dr["Critical_Flag"].ToString() == "1" ? true : false;

            lblItemCreatedBy.Text = dr["CREATEDBY"].ToString();
            lblItemmodifiedby.Text = dr["MODIFIEDBY"].ToString();
            lblItemDeletedBy.Text = dr["DELETEDBY"].ToString();

        }

    }

    public void BindDeptOptList()
    {
        try
        {
            using (BLL_PURC_Purchase objBLLPUR = new BLL_PURC_Purchase())
            {
                DataTable DeptDt = objBLLPUR.GetDeptType();
                optList.DataSource = DeptDt;
                optList.DataTextField = "Description";
                optList.DataValueField = "Short_Code";
                optList.DataBind();
                optList.SelectedIndex = 0;
                optList.Items.Remove("ALL");
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.Items.Clear();
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFleet.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }

    public void BindVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.Items.Clear();
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLVessel.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }


    private void BindAccountCode()
    {

        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {

            DataTable dtAccountCode = new DataTable();
            dtAccountCode = objTechService.SelectBudgetCode().Tables[0];
            ddlAccountCode.DataTextField = "Budget_Name";
            ddlAccountCode.DataValueField = "Budget_Code";
            ddlAccountCode.DataSource = dtAccountCode;
            ddlAccountCode.DataBind();
            ddlAccountCode.Items.Insert(0, new ListItem("SELECT", "0"));

        }
    }


    private void BindSubCatAccountCode()
    {

        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {

            DataTable dtAccountCode = new DataTable();
            dtAccountCode = objTechService.SelectBudgetCode().Tables[0];
            ddlSubCatAccountCode.DataTextField = "Budget_Name";
            ddlSubCatAccountCode.DataValueField = "Budget_Code";
            ddlSubCatAccountCode.DataSource = dtAccountCode;
            ddlSubCatAccountCode.DataBind();
            ddlSubCatAccountCode.Items.Insert(0, new ListItem("SELECT", "0"));

        }
    }


    private void BindUnitPakageDDL()
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtUnitnPack = new DataTable();
                dtUnitnPack = objTechService.SelectUnitnPackage();
                ddlUnit.DataTextField = "ABREVIATION";
                ddlUnit.DataValueField = "Main_Pack";
                ddlUnit.DataSource = dtUnitnPack;
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, new ListItem("SELECT", "0"));

            }
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }


    private void BindItemCategory()
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable dt = new DataTable();
            dt = objTechService.LibraryGetSystemParameterList("469", null);

            ddlItemCategory.DataTextField = "Description";
            ddlItemCategory.DataValueField = "Code";
            ddlItemCategory.DataSource = dt;
            ddlItemCategory.DataBind();
            ddlItemCategory.Items.Insert(0, new ListItem("SELECT", "0"));
        }


    }


    //private void BindCatalogLocation()
    //{
    //    using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
    //    {
    //        DataTable dt = new DataTable();
    //        dt = objTechService.LibraryGetSystemParameterList("1",null);

    //        ddlcatalogLocation.DataTextField = "Description";
    //        ddlcatalogLocation.DataValueField = "Code";
    //        ddlcatalogLocation.DataSource = dt;
    //        ddlcatalogLocation.DataBind();
    //        ddlcatalogLocation.Items.Insert(0, new ListItem("SELECT", "0"));
    //    }

    //}

    //private void BindCatalogFunction()
    //{

    //    using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
    //    {
    //        DataTable dt = new DataTable();
    //        dt = objTechService.LibraryGetSystemParameterList("115",null);

    //        ddlCatalogFunction.DataTextField = "Description";
    //        ddlCatalogFunction.DataValueField = "Code";
    //        ddlCatalogFunction.DataSource = dt;
    //        ddlCatalogFunction.DataBind();
    //        ddlCatalogFunction.Items.Insert(0, new ListItem("SELECT", "0"));
    //    }
    //}


    private void BindCatalogAssingLocationDLL()
    {

        if ((String)ViewState["CatlogueOperationMode"] == "EDIT")
        {
            BLL_PMS_Library_Jobs objJob = new BLL_PMS_Library_Jobs();
            DataTable dt = objJob.LibraryGetCatalogueLocationAssign(ViewState["SystemCode"].ToString(), Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString()));
            if (dt.Rows.Count > 0)
            {
                lstcatalogLocation.DataTextField = "LocationName";
                lstcatalogLocation.DataValueField = "AssginLocationID";
                lstcatalogLocation.DataSource = dt;
                lstcatalogLocation.DataBind();
            }
            else
            {
                lstcatalogLocation.Items.Clear();
            }

        }

        //if ((String)ViewState["CatlogueOperationMode"] == "ADD")
        //{
        //    DataTable dt = (DataTable)ViewState["TempDtLocation"];
        //    lstcatalogLocation.DataTextField = "LocationName";
        //    lstcatalogLocation.DataValueField = "LocationID";
        //    lstcatalogLocation.DataSource = dt;
        //    lstcatalogLocation.DataBind();
        //}
    }

    private void BindCatalogueDepartment()
    {
        using (BLL_PURC_Purchase objBLLPURC = new BLL_PURC_Purchase())
        {
            DataTable dtDepartment = new DataTable();
            dtDepartment = objBLLPURC.SelectDepartment();
            ddlCatalogDept.DataTextField = "Name_Dept";
            ddlCatalogDept.DataValueField = "Code";
            ddlCatalogDept.DataSource = dtDepartment;
            ddlCatalogDept.DataBind();
            ddlCatalogDept.Items.Insert(0, new ListItem("SELECT", "0"));
        }

    }

    private void BindDepartmentByST_SP()
    {
        try
        {

            using (BLL_PURC_Purchase objBLLPURC = new BLL_PURC_Purchase())
            {
                DataTable dtDepartment = new DataTable();
                dtDepartment = objBLLPURC.SelectDepartment();

                if (optList.SelectedItem != null)
                {
                    ViewState["DeptType"] = optList.SelectedValue;
                    hdfDeptType.Value = optList.SelectedValue;

                    if (optList.SelectedItem.Text == "Spares")
                    {
                        btnDivAddLocation.Enabled = true;
                        btnSubCatDivAddLocation.Enabled = true;
                        dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";
                    }
                    else if (optList.SelectedItem.Text == "Stores")
                    {
                        btnDivAddLocation.Enabled = false;
                        btnSubCatDivAddLocation.Enabled = false;

                        dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";
                    }
                    else if (optList.SelectedItem.Text == "Repairs")
                    {
                        btnDivAddLocation.Enabled = true;
                        btnSubCatDivAddLocation.Enabled = true;
                        dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";
                    }
                    cmbDept.Items.Clear();
                    cmbDept.DataSource = dtDepartment;
                    cmbDept.AppendDataBoundItems = true;
                    ListItem item = new ListItem();
                    item.Value = "ALL";
                    item.Text = "SELECT";
                    cmbDept.Items.Add(item);
                    cmbDept.DataTextField = "Name_Dept";
                    cmbDept.DataValueField = "Code";
                    cmbDept.DataBind();


                    //-----------catalog department------
                    ddlCatalogDept.Items.Clear();
                    ddlCatalogDept.DataSource = dtDepartment;
                    ddlCatalogDept.AppendDataBoundItems = true;
                    ddlCatalogDept.DataTextField = "Name_Dept";
                    ddlCatalogDept.DataValueField = "Code";
                    ddlCatalogDept.DataBind();
                    ddlCatalogDept.Items.Insert(0, new ListItem("SELECT", "0"));
                }

            }

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {
        }
    }

    protected void BindSupplierDetails()
    {
        using (BLL_PURC_Purchase objsupplier = new BLL_PURC_Purchase())
        {
            DataTable dt = objsupplier.SelectSupplier();
            dt.DefaultView.RowFilter = "SUPPLIER_CATEGORY='M'";
            ddlCalalogueMaker.DataTextField = "SUPPLIER_NAME";
            ddlCalalogueMaker.DataValueField = "SUPPLIER";
            ddlCalalogueMaker.DataSource = dt.DefaultView.ToTable();
            ddlCalalogueMaker.DataBind();
            ddlCalalogueMaker.Items.Insert(0, new ListItem("SELECT", "0"));
        }
    }

    protected void BindSubCatSupplierDetails()
    {
        using (BLL_PURC_Purchase objsupplier = new BLL_PURC_Purchase())
        {
            DataTable dt = objsupplier.SelectSupplier();
            dt.DefaultView.RowFilter = "SUPPLIER_CATEGORY='M'";
            ddlSubCatMaker.DataTextField = "SUPPLIER_NAME";
            ddlSubCatMaker.DataValueField = "SUPPLIER";
            ddlSubCatMaker.DataSource = dt.DefaultView.ToTable();
            ddlSubCatMaker.DataBind();
            ddlSubCatMaker.Items.Insert(0, new ListItem("SELECT", "0"));
        }
    }



    protected void ddldisplayRecordType_SelectedIndexChanged(object sender, EventArgs e)
    {

        string activestatus = ddldisplayRecordType.SelectedValue.ToString();

        if (activestatus == "2")
            ViewState["ActiveStatus"] = null;
        else
            ViewState["ActiveStatus"] = ddldisplayRecordType.SelectedValue.ToString();
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindVesselDDL();
        cmbDept.SelectedIndex = 0;

    }

    protected void optList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ucCustomPagerItems.isCountRecord = 1;


        BindDepartmentByST_SP();

        DDLVessel.SelectedValue = "0";
        ViewState["VesselCode"] = DDLVessel.SelectedValue.ToString();


        gvCatalogue.DataSource = null;
        gvCatalogue.DataBind();

        gvSubCatalogue.DataSource = null;
        gvSubCatalogue.DataBind();

        gvItem.DataSource = null;
        gvItem.DataBind();

        ClearCatalogueFields();
        ClearSubCatalogueFields();
        ClearItemsFields();



        UpdCatalogueEntry.Update();
        UpdCatelogueGrid.Update();

        UpdSubCatalogueEntry.Update();
        UpdSubCatelogueGrid.Update();

        UpdItemEntry.Update();
        UpdItemGrid.Update();




    }

    protected void btnCatalogueAdd_Click(object sender, EventArgs e)
    {
        ViewState["CatlogueOperationMode"] = "ADD";
        hdfCatlogueOperationMode.Value = "ADD";

        ViewState["NEXTSYSTEMID"] = GetNextSystemCode();

        txtCatalogueCode.Text = ViewState["NEXTSYSTEMID"].ToString();
        chkSubSysAdd.Checked = true;

        ClearCatalogueFields();
    }

    protected string GetNextSystemCode()
    {
        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        DataTable dt = objBLLPurc.LibraryGetNextSystemCode();
        return dt.Rows[0][0].ToString();
    }

    private void ClearCatalogueFields()
    {

        // txtCatalogueCode.Text = "";
        txtCatalogueSetInstalled.Text = "";
        txtCatalogName.Text = "";
        txtCatalogueModel.Text = "";
        txtCatalogueSerialNumber.Text = "";
        txtCatalogueParticular.Text = "";
        ddlCalalogueMaker.SelectedValue = "0"; ;
        ddlCatalogDept.SelectedValue = "0";

        ddlAccountCode.SelectedValue = "0";

        lstcatalogLocation.Items.Clear();
        ddlCatalogFunction.SelectedValue = "";

        lblCatalogueCreatedBy.Text = "";
        lblCatalogueDeletedby.Text = "";
        lblCatalogueModifiedby.Text = "";

        ViewState["CatlogueOperationMode"] = "ADD";
        ViewState["NEXTSYSTEMID"] = GetNextSystemCode();
        txtCatalogueCode.Text = ViewState["NEXTSYSTEMID"].ToString();

        chkSubSysAdd.Checked = true;


    }

    private void ClearSubCatalogueFields()
    {
        txtSubCatalogueCode.Text = "";
        txtSubCatalogueName.Text = "";
        txtSubCatalogueParticulars.Text = "";
        lblSubCatalogueCreatedBy.Text = "";
        lblSubCatalogueModifiedby.Text = "";
        lblSubCatalogueDeletedBy.Text = "";
        ddlSubCatMaker.SelectedValue = "0";
        ddlSubCatMaker.SelectedValue = "0";
        txtSubCatModel.Text = "";
        txtSubCatSerialNo.Text = "";
        lstSubCatVesselLocation.Items.Clear();

    }

    private void ClearItemsFields()
    {
        txtItemDrawingNumber.Text = "";
        txtItemName.Text = "";
        txtItemPartNumber.Text = "";
        txtItemName.Text = "";
        txtItemDescription.Text = "";
        txtMaxQty.Text = "";
        txtMinQty.Text = "";

        // ddlUnit.SelectedValue = "0";

        ddlItemCategory.SelectedValue = "0";
        chkItemCritical.Checked = false;

        lblItemCreatedBy.Text = "";
        lblItemmodifiedby.Text = "";
        lblItemDeletedBy.Text = "";

        lnkImageUploadName.Text = "";
        lnkProductDetailUploadName.Text = "";

    }

    protected void btnCatalogueSave_Click(object sender, EventArgs e)
    {
        try
        {

            BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
            BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();


            int? AddSubSysFlag = null; if (chkSubSysAdd.Checked == true) AddSubSysFlag = 1;

            int IsLocationRequired = optList.SelectedValue.ToUpper() == "SP" ? 1 : 0;

            if ((String)ViewState["CatlogueOperationMode"] == "EDIT")
            {
                int val = objBLLPurc.LibraryCatalogueUpdate(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(ViewState["SystemId"].ToString())
                    , txtCatalogueCode.Text.Trim(), txtCatalogName.Text.Trim(), txtCatalogueParticular.Text.Trim(), General.GetNullableString(ddlCalalogueMaker.SelectedValue)
                    , txtCatalogueSetInstalled.Text.Trim(), txtCatalogueModel.Text.Trim(), ddlCatalogDept.SelectedValue, ViewState["VesselCodeEditMode"].ToString()
                    , ddlCatalogFunction.SelectedValue, ddlAccountCode.SelectedValue, txtCatalogueSerialNumber.Text.Trim(), chkRunHourS.Checked ? 1 : 0, chkCriticalS.Checked ? 1 : 0);
            }

            if ((String)ViewState["CatlogueOperationMode"] == "ADD")
            {
                if(optList.SelectedValue.ToUpper() != "ST" && UDFLib.ConvertToInteger(DDLVessel.SelectedValue) < 1)
                {
                    string msgVessel = String.Format("alert('Please select vessel');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgVessel", msgVessel, true);
                    return;
                }
                                

                int val = objBLLPurc.LibraryCatalogueSave(Convert.ToInt32(Session["userid"].ToString()), txtCatalogueCode.Text.Trim(), txtCatalogName.Text.Trim()
                     , txtCatalogueParticular.Text.Trim(), General.GetNullableString(ddlCalalogueMaker.SelectedValue), txtCatalogueSetInstalled.Text.Trim()
                     , txtCatalogueModel.Text.Trim(), ddlCatalogDept.SelectedValue, DDLVessel.SelectedValue
                     , ddlCatalogFunction.SelectedValue, ddlAccountCode.SelectedValue, AddSubSysFlag, txtCatalogueSerialNumber.Text.Trim(), IsLocationRequired, chkRunHourS.Checked ? 1 : 0, chkCriticalS.Checked ? 1 : 0);


                ViewState["SystemId"] = val;
               

                //Make Entry in Vessel Location Table
                //for (int i = 0; i < lstcatalogLocation.Items.Count; i++)
                //{
                //    int retval = objJobs.LibraryCatalogueLocationAssignSave(Convert.ToInt32(Session["userid"].ToString())
                //        , ViewState["SystemCode"].ToString(), Convert.ToInt32(lstcatalogLocation.Items[i]), Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString()));
                //}
            }

            BindCatalogue();

            BindSubCatalogue();

            //BindEmptySubCatalogGrid();
            BindEmptyItemGrid();

            ViewState["CatlogueOperationMode"] = "ADD";
            hdfCatlogueOperationMode.Value = "ADD";
            ClearCatalogueFields();


            ViewState["NEXTSYSTEMID"] = GetNextSystemCode();
            txtCatalogueCode.Text = ViewState["NEXTSYSTEMID"].ToString();

            UpdCatalogueEntry.Update();
            UpdCatelogueGrid.Update();

            UpdSubCatalogueEntry.Update();
            UpdSubCatelogueGrid.Update();

            UpdItemEntry.Update();
            UpdItemGrid.Update();
        }
        catch (Exception ex)
        {

        }

    }

    protected void btnAddNewSubCatalogue_Click(object sender, EventArgs e)
    {
        ViewState["SubCatlogueOperationMode"] = "ADD";
        ClearSubCatalogueFields();
        chkRunHourSS.Enabled = true;

    }

    protected void btnSaveSubCatalogue_Click(object sender, EventArgs e)
    {
        try
        {

            BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();

            int IsLocationRequired = optList.SelectedValue.ToUpper() == "SP" ? 1 : 0;

            if ((String)ViewState["SubCatlogueOperationMode"] == "EDIT")
            {
                int val = objBLLPurc.LibrarySubCatalogueUpdate(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(ViewState["SubSystemId"].ToString()), ViewState["SystemCode"].ToString(), txtSubCatalogueCode.Text.Trim()
                    , txtSubCatalogueName.Text.Trim(), txtSubCatalogueParticulars.Text.Trim(), ddlSubCatMaker.SelectedValue, txtSubCatModel.Text.Trim(), txtSubCatSerialNo.Text.Trim(), chkRunHourSS.Checked ? 1 : 0, chkCriticalSS.Checked ? 1 : 0, chkCopyRunHourSS.Checked ? 1 : 0);
            }

            if ((String)ViewState["SubCatlogueOperationMode"] == "ADD")
            {
                int val = objBLLPurc.LibrarySubCatalogueSave(Convert.ToInt32(Session["userid"].ToString()), ViewState["SystemCode"].ToString(), txtSubCatalogueCode.Text.Trim()
                      , txtSubCatalogueName.Text.Trim(), txtSubCatalogueParticulars.Text.Trim(), ddlSubCatMaker.SelectedValue, txtSubCatModel.Text.Trim(), txtSubCatSerialNo.Text.Trim(), IsLocationRequired, chkRunHourSS.Checked ? 1 : 0, chkCriticalSS.Checked ? 1 : 0, chkCopyRunHourSS.Checked ? 1 : 0);

                ViewState["SubSystemId"] = val;
            }




            BindSubCatalogue();
            BindEmptyItemGrid();

            ViewState["SubCatlogueOperationMode"] = "ADD";
            ClearSubCatalogueFields();

            UpdSubCatalogueEntry.Update();
            UpdSubCatelogueGrid.Update();


            UpdItemEntry.Update();
            UpdItemGrid.Update();



        }
        catch (Exception ex)
        {
            lblSubCatalogErrorMsg.Text = ex.ToString();
        }

    }

    protected void btnAddNewItem_Click(object sender, EventArgs e)
    {
        ViewState["ItemOperationMode"] = "ADD";
        hdfItemOperationMode.Value = "ADD";
        ClearItemsFields();
    }
    
    
    protected void btnSaveItem_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = objUploadFilesize.Get_Module_FileUpload("PURC_");
        try
        {
            BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();


            string image_url = "";
            string product_dtl_url = "";
            if (dt.Rows.Count > 0)
            {
                string datasize = dt.Rows[0]["Size_KB"].ToString();
                if (ImageUploader.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                {
                    string strImagePath = ImageUploader.PostedFile != null ? ImageUploader.PostedFile.FileName : "";

                    if (DetailsImageUploader.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                    {
                        string strProductDtlPath = DetailsImageUploader.PostedFile != null ? DetailsImageUploader.PostedFile.FileName : "";

                        if ((String)ViewState["ItemOperationMode"] == "EDIT")
                        {

                            if (strImagePath != "")
                            {
                                image_url = "itm_" + System.Guid.NewGuid() + Path.GetExtension(strImagePath);
                                ImageUploader.PostedFile.SaveAs(Server.MapPath("~/Uploads/PURC_Items/" + image_url));
                            }
                            else
                            {
                                image_url = lnkImageUploadName.Text;
                            }

                            if (strProductDtlPath != "")
                            {
                                product_dtl_url = "itm_" + System.Guid.NewGuid() + Path.GetExtension(strProductDtlPath);
                                DetailsImageUploader.PostedFile.SaveAs(Server.MapPath("~/Uploads/PURC_Items/" + product_dtl_url));

                            }
                            else
                            {
                                product_dtl_url = lnkProductDetailUploadName.Text;
                            }



                            int val = objBLLPurc.LibraryItemUpdate(Convert.ToInt32(Session["userid"].ToString()), ViewState["ItemId"].ToString()
                                , ViewState["SystemCode"].ToString(), ViewState["SubSystemCode"].ToString()
                                , txtItemPartNumber.Text.Trim(), txtItemName.Text.Trim(), txtItemDescription.Text.Trim(), txtItemDrawingNumber.Text.Trim(), ddlUnit.SelectedValue.ToString()
                                , UDFLib.ConvertDecimalToNull(txtMinQty.Text.Trim()), UDFLib.ConvertDecimalToNull(txtMaxQty.Text.Trim()), ViewState["VesselCodeEditMode"].ToString()
                                , UDFLib.ConvertIntegerToNull(ddlItemCategory.SelectedValue.ToString()), image_url != "" ? image_url : null, product_dtl_url != "" ? product_dtl_url : null, chkItemCritical.Checked ? 1 : 0);
                        }

                        if ((String)ViewState["ItemOperationMode"] == "ADD")
                        {
                            if (strImagePath != "")
                                image_url = "itm_" + System.Guid.NewGuid() + Path.GetExtension(strImagePath);

                            if (strProductDtlPath != "")
                                product_dtl_url = "itm_" + System.Guid.NewGuid() + Path.GetExtension(strProductDtlPath);


                            string val = objBLLPurc.LibraryItemSave(Convert.ToInt32(Session["userid"].ToString()), ViewState["SystemCode"].ToString()
                                , ViewState["SubSystemCode"].ToString(), txtItemPartNumber.Text.Trim(), txtItemName.Text.Trim(), txtItemDescription.Text.Trim()
                                , txtItemDrawingNumber.Text.Trim(), ddlUnit.SelectedValue.ToString()
                                , UDFLib.ConvertDecimalToNull(txtMinQty.Text.Trim()), UDFLib.ConvertDecimalToNull(txtMaxQty.Text.Trim()), ViewState["VesselCodeEditMode"].ToString()
                                , UDFLib.ConvertIntegerToNull(ddlItemCategory.SelectedValue.ToString()), image_url != "" ? image_url : null, product_dtl_url != "" ? product_dtl_url : null, chkItemCritical.Checked ? 1 : 0);

                            ViewState["ItemId"] = val;

                            if (ImageUploader.PostedFile.FileName != "")
                            {
                                ImageUploader.PostedFile.SaveAs(Server.MapPath("~/Uploads/PURC_Items/" + image_url));
                            }
                            if (DetailsImageUploader.PostedFile.FileName != "")
                            {
                                DetailsImageUploader.PostedFile.SaveAs(Server.MapPath("~/Uploads/PURC_Items/" + product_dtl_url));
                            }

                        }

                        BindItems();

                        ViewState["ItemOperationMode"] = "ADD";
                        hdfItemOperationMode.Value = "ADD";
                        ClearItemsFields();

                        UpdItemEntry.Update();
                        UpdItemGrid.Update();


                    }

                    else
                    {
                        lblMessage.Text = " KB File size exceeds maximum limit";
                    }
                }
                else
                {
                    lblMessage.Text = " KB File size exceeds maximum limit";
                }
            }
            else
            {
                
                string js2 = "alert('Upload size not set!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
            }
        }
        catch (Exception ex)
        {
            lblItemErrorMsg.Text = ex.ToString();

        }
    }

    protected void gvCatalogue_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {
        ucCustomPagerItems.isCountRecord = 1;
        gvCatalogue.SelectedIndex = se.NewSelectedIndex;

        chkSubSysAdd.Checked = false;
        ViewState["SystemId"] = ((Label)gvCatalogue.Rows[se.NewSelectedIndex].FindControl("lblSystemId")).Text;
        ViewState["SystemCode"] = ((Label)gvCatalogue.Rows[se.NewSelectedIndex].FindControl("lblSystemCode")).Text;
        ViewState["VesselCodeEditMode"] = ((Label)gvCatalogue.Rows[se.NewSelectedIndex].FindControl("lblVesselCode")).Text;

        ViewState["SubSystemId"] = null;
        ViewState["ItemId"] = null;

        ViewState["CatlogueOperationMode"] = "EDIT";
        hdfCatlogueOperationMode.Value = "EDIT";


        ViewState["SubCatlogueOperationMode"] = "ADD";

        ViewState["ItemOperationMode"] = "ADD";
        hdfItemOperationMode.Value = "ADD";


        BindCatalogueList(ViewState["SystemId"].ToString());
        BindCatalogAssingLocationDLL();

        BindCatalogue();
        BindSubCatalogue();
        BindItems();

        ClearSubCatalogueFields();
        ClearItemsFields();


        UpdCatalogueEntry.Update();
        UpdSubCatelogueGrid.Update();
        UpdSubCatalogueEntry.Update();
        UpdItemGrid.Update();
        UpdItemEntry.Update();



        string jsCatalogGridScroll = ScriptSaveCatalogGridScroll();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ScriptSaveCatalogGridScroll", jsCatalogGridScroll, true);


    }

    private void BindEmptyItemGrid()
    {
        // Give false condition in item search.
        int rowcount = 0;

        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        DataSet ds = objBLLPurc.LibraryItemSearch(null, null, null, null, null, null, null
            , null, null, null, 0, 1, 500, out rowcount);

        gvItem.DataSource = ds.Tables[0];
        gvItem.DataBind();

        ucCustomPagerItems.Visible = false;
        ClearSubCatalogueFields();
        ClearItemsFields();
    }

    private void BindEmptySubCatalogGrid()
    {

        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        DataSet ds = objBLLPurc.LibrarySubCatalogueSearch("-#001###", "-1#", null, -1, null, null, 0, 0, 0);

        gvSubCatalogue.DataSource = ds.Tables[0];
        gvSubCatalogue.DataBind();

    }

    protected void gvCatalogue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCalogueActiveSatus = (Label)e.Row.FindControl("lblCalogueActiveSatus");
            LinkButton lnkSystemName = (LinkButton)e.Row.FindControl("lnbSystemName");
            ImageButton ImgCatalogueRestore = (ImageButton)e.Row.FindControl("ImgCatalogueRestore");

            ImageButton ImgCatalogueDelete = (ImageButton)e.Row.FindControl("ImgCatalogueDelete");

            Label lblMaker = (Label)e.Row.FindControl("lblMaker");
            Label lblModel = (Label)e.Row.FindControl("lblModel");
            Label lblParticulars = (Label)e.Row.FindControl("lblParticulars");

            if (lblMaker.Text != "" || lblModel.Text != "" || lblParticulars.Text != "")
                lnkSystemName.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Maker/ Model / Particulars] body=[" + lblMaker.Text + " /  " + lblModel.Text + " / " + lblParticulars.Text + "]");


            Int64 result = 0;
            if (Int64.TryParse(lblCalogueActiveSatus.Text, out result))
            {
                e.Row.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                lnkSystemName.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;

                ImgCatalogueRestore.Visible = (result == 0) ? true : false;
                ImgCatalogueDelete.Visible = (result == 0) ? false : true;
            }


            if (objUserAcess.Delete == 1 && result == 1)
            {
                ImgCatalogueDelete.Visible = true;
            }
            else
            {
                ImgCatalogueDelete.Visible = false;
            }

        }



        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["CATALOGUESORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["CATALOGUESORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["CATALOGUESORTDIRECTION"] == null || ViewState["CATALOGUESORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowDown.png";
                    else
                        img.Src = "~/purchase/Image/arrowUp.png";

                    img.Visible = true;
                }
            }
        }






    }

    protected void gvSubCatalogue_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {
        ucCustomPagerItems.isCountRecord = 1;

        gvSubCatalogue.SelectedIndex = se.NewSelectedIndex;
        ViewState["SubSystemId"] = ((Label)gvSubCatalogue.Rows[se.NewSelectedIndex].FindControl("lblSubSystemID")).Text;
        ViewState["SubSystemCode"] = ((Label)gvSubCatalogue.Rows[se.NewSelectedIndex].FindControl("lblSubSystemCode")).Text;

        string IsGeneral = ((LinkButton)gvSubCatalogue.Rows[se.NewSelectedIndex].FindControl("lblSubCatalogueName")).Text;
        chkRunHourSS.Checked = false;
        chkCopyRunHourSS.Checked = false;
        if (IsGeneral.ToLower() == "general")
        {
            chkRunHourSS.Enabled = false;
            chkCopyRunHourSS.Enabled = false;
        }
        else
        {
            if (chkRunHourS.Checked == true)
            {
                chkRunHourSS.Enabled = true;
                
            }
        }

        //ViewState["ItemId"] = null;

        ViewState["SelectAllItem"] = null;
        ViewState["SubCatlogueOperationMode"] = "EDIT";
        ViewState["ItemOperationMode"] = "ADD";
        hdfItemOperationMode.Value = "ADD";

        BindSubCatalogueList(ViewState["SubSystemId"].ToString());

        BindSubCatalogue();
        BindItems();

        UpdItemGrid.Update();
        UpdItemEntry.Update();
        UpdSubCatalogueEntry.Update();


        ClearItemsFields();

        string jsSubCatalogGridScroll = ScriptSaveSubCatalogGridScroll();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ScriptSaveCatalogGridScroll", jsSubCatalogGridScroll, true);
    }

    protected void gvSubCatalogue_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSubCalogueActiveSatus = (Label)e.Row.FindControl("lblSubCalogueActiveSatus");
            LinkButton lnkSubCatalogueName = (LinkButton)e.Row.FindControl("lblSubCatalogueName");
            ImageButton ImgSubCatalogueRestore = (ImageButton)e.Row.FindControl("ImgSubCatalogueRestore");
            ImageButton ImgSubCatalogueDelete = (ImageButton)e.Row.FindControl("ImgSubCatalogueDelete");

            Label lblSubsystemParticulars = (Label)e.Row.FindControl("lblSubsystemParticulars");

            if (lblSubsystemParticulars.Text != "")
                lnkSubCatalogueName.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Particulars] body=[" + lblSubsystemParticulars.Text + "]");

            Int64 result = 0;
            if (Int64.TryParse(lblSubCalogueActiveSatus.Text, out result))
            {
                e.Row.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                lnkSubCatalogueName.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;

                ImgSubCatalogueRestore.Visible = (result == 0) ? true : false;
                ImgSubCatalogueDelete.Visible = (result == 0) ? false : true;
            }

            if (objUserAcess.Delete == 1 && result == 1)
            {
                ImgSubCatalogueDelete.Visible = true;
            }
            else
            {
                ImgSubCatalogueDelete.Visible = false;
            }

        }


        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SUBCATALOGUESORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SUBCATALOGUESORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SUBCATALOGUESORTDIRECTION"] == null || ViewState["SUBCATALOGUESORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowDown.png";
                    else
                        img.Src = "~/purchase/Image/arrowUp.png";

                    img.Visible = true;
                }
            }
        }
    }

    protected void gvItem_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {
        gvItem.SelectedIndex = se.NewSelectedIndex;
        ViewState["ItemId"] = ((Label)gvItem.Rows[se.NewSelectedIndex].FindControl("lblItemID")).Text;
        //ViewState["VesselCodeEditMode"] = ((Label)gvItem.Rows[se.NewSelectedIndex].FindControl("lblVesselcode")).Text;


        BindItemsList(ViewState["ItemId"].ToString());

        ViewState["CatlogueOperationMode"] = "EDIT";
        hdfCatlogueOperationMode.Value = "EDIT";

        ViewState["SubCatlogueOperationMode"] = "EDIT";

        ViewState["ItemOperationMode"] = "EDIT";
        hdfItemOperationMode.Value = "EDIT";

        BindItems();

        UpdItemEntry.Update();
    }

    protected void gvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        /*Provision Module: IsMeat column display only for Provision Department Items ,hidden for non-provision department items.(Added BY Pranali_14042015)*/
        if (ViewState["SystemCode"].ToString() == "PROVI")
        {
            e.Row.Cells[7].Visible = true;
        }
        else
        {
            e.Row.Cells[7].Visible = false;
        }
       
        //Slopchest Module : IsSlopChest Column display only for Bond Store Items,hidden for other department items [Pranali_25072015]
        if (ViewState["SystemCode"].ToString() == "BOND")
        {
            e.Row.Cells[8].Visible = true;
        }
        else
        {
            e.Row.Cells[8].Visible = false;
        }
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblItemActiveSatus = (Label)e.Row.FindControl("lblItemActiveSatus");
            LinkButton lnkItemName = (LinkButton)e.Row.FindControl("lblItemName");
            ImageButton ImgItemRestore = (ImageButton)e.Row.FindControl("ImgItemRestore");
            ImageButton ImgItemDelete = (ImageButton)e.Row.FindControl("ImgItemDelete");
            Label lblLongDescription = (Label)e.Row.FindControl("lblLongDescription");


            if (lblLongDescription.Text != "")
                lnkItemName.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Long Description] body=[" + lblLongDescription.Text + "]");

            Int64 result = 0;
            if (Int64.TryParse(lblItemActiveSatus.Text, out result))
            {
                e.Row.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                lnkItemName.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;

                ImgItemRestore.Visible = (result == 0) ? true : false;
                ImgItemDelete.Visible = (result == 0) ? false : true;
            }

            if (objUserAcess.Delete == 1 && result == 1)
            {
                ImgItemDelete.Visible = true;
            }
            else
            {
                ImgItemDelete.Visible = false;
            }



        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ImgUrlAtt = (ImageButton)e.Row.FindControl("ImgUrlAtt");
            if (DataBinder.Eval(e.Row.DataItem, "Image_Url_Att").ToString() == "1")
            {
                ImgUrlAtt.Attributes.Add("onclick", "DocOpen('" + DataBinder.Eval(e.Row.DataItem, "Image_Url").ToString() + "'); return false;");
            }


            ImageButton ImgDetialsUrlAtt = (ImageButton)e.Row.FindControl("ImgDetialsUrlAtt");
            if (DataBinder.Eval(e.Row.DataItem, "Product_Details_Att").ToString() == "1")
            {
                ImgDetialsUrlAtt.Attributes.Add("onclick", "DocOpen('" + DataBinder.Eval(e.Row.DataItem, "Product_Details").ToString() + "'); return false;");
            }

        }








        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["ITEMSORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["ITEMSORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["ITEMSORTDIRECTION"] == null || ViewState["ITEMSORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowDown.png";
                    else
                        img.Src = "~/purchase/Image/arrowUp.png";

                    img.Visible = true;
                }
            }
        }


    }

    protected void imgCatalogueSearch_Click(object sender, ImageClickEventArgs e)
    {

        BindCatalogue();

        BindEmptySubCatalogGrid();
        BindEmptyItemGrid();




        ClearCatalogueFields();
        ClearSubCatalogueFields();
        ClearItemsFields();


        UpdCatelogueGrid.Update();
        UpdCatalogueEntry.Update();

        UpdSubCatelogueGrid.Update();
        UpdSubCatalogueEntry.Update();
        UpdItemGrid.Update();
        UpdItemEntry.Update();


    }

    protected void imgSubCatalogueSearch_Click(object sender, ImageClickEventArgs e)
    {

        BindSubCatalogue();

        ClearSubCatalogueFields();
        BindEmptyItemGrid();
        ClearItemsFields();



        UpdSubCatelogueGrid.Update();
        UpdSubCatalogueEntry.Update();
        UpdItemGrid.Update();
        UpdItemEntry.Update();


    }

    protected void btnSelectAllItem_Click(object sender, EventArgs e)
    {

        ucCustomPagerItems.isCountRecord = 1;
        ViewState["SelectAllItem"] = "Y";

        BindItems();

        ViewState["ItemOperationMode"] = "ADD";
        hdfItemOperationMode.Value = "ADD";

        ClearItemsFields();

        UpdItemGrid.Update();
        UpdItemEntry.Update();

    }

    protected void imgItemSearch_Click(object sender, ImageClickEventArgs e)
    {

        BindItems();
        ClearItemsFields();

        UpdItemGrid.Update();
        UpdItemEntry.Update();

    }

    protected void ImgItemDelete_Click(object sender, CommandEventArgs e)
    {
        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {
            int retval = objBLLPurc.LibraryItemDelete(Convert.ToInt32(Session["userid"].ToString()), e.CommandArgument.ToString());
        }

        BindItems();

        ClearItemsFields();

        UpdItemGrid.Update();
        UpdItemEntry.Update();

    }

    protected void ImgItemRestore_Click(object sender, CommandEventArgs e)
    {
        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {
            int retval = objBLLPurc.LibraryItemRestore(Convert.ToInt32(Session["userid"].ToString()), e.CommandArgument.ToString());
        }

        BindItems();

        ClearItemsFields();

        UpdItemGrid.Update();
        UpdItemEntry.Update();
    }

    protected void ImgCatalogueDelete_Click(object sender, CommandEventArgs e)
    {
        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {
            int retval = objBLLPurc.LibraryCatalogueDelete(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(e.CommandArgument.ToString()));
        }

        BindCatalogue();

        BindEmptySubCatalogGrid();
        BindEmptyItemGrid();


        ClearCatalogueFields();
        ClearSubCatalogueFields();
        ClearItemsFields();


        btnSaveSubCatalogue.Enabled = false;
        btnAddNewSubCatalogue.Enabled = false;

        btnSaveItem.Enabled = false;
        btnAddNewItem.Enabled = false;


        UpdCatelogueGrid.Update();
        UpdCatalogueEntry.Update();

        UpdSubCatelogueGrid.Update();
        UpdSubCatalogueEntry.Update();

        UpdItemGrid.Update();
        UpdItemEntry.Update();

    }

    protected void ImgCatalogueRestore_Click(object sender, CommandEventArgs e)
    {
        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {
            int retval = objBLLPurc.LibraryCatalogueRestore(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(e.CommandArgument.ToString()));
        }

        BindCatalogue();

        BindEmptySubCatalogGrid();
        BindEmptyItemGrid();


        ClearCatalogueFields();
        ClearSubCatalogueFields();
        ClearItemsFields();



        UpdCatelogueGrid.Update();
        UpdCatalogueEntry.Update();

        UpdSubCatelogueGrid.Update();
        UpdSubCatalogueEntry.Update();

        UpdItemGrid.Update();
        UpdItemEntry.Update();

    }

    protected void ImgSubCatalogueDelete_Click(object sender, CommandEventArgs e)
    {
        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {
            int retval = objBLLPurc.LibrarySubCatalogueDelete(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(e.CommandArgument.ToString()));
        }


        BindSubCatalogue();

        BindEmptyItemGrid();

        ClearSubCatalogueFields();
        ClearItemsFields();

        UpdSubCatelogueGrid.Update();
        UpdSubCatalogueEntry.Update();

        UpdItemGrid.Update();
        UpdItemEntry.Update();


    }

    protected void ImgSubCatalogueRestore_Click(object sender, CommandEventArgs e)
    {
        using (BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase())
        {
            int retval = objBLLPurc.LibrarySubCatalogueRestore(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(e.CommandArgument.ToString()));
        }

        BindSubCatalogue();

        BindEmptyItemGrid();

        ClearSubCatalogueFields();
        ClearItemsFields();

        UpdSubCatelogueGrid.Update();
        UpdSubCatalogueEntry.Update();

        UpdItemGrid.Update();
        UpdItemEntry.Update();
    }


    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        if (objUserAcess.Add == 1)
        {
            btnCatalogueAdd.Enabled = true;
            btnCatalogueSave.Enabled = true;
            btnSaveSubCatalogue.Enabled = true;
            btnAddNewSubCatalogue.Enabled = true;
            btnAddNewItem.Enabled = true;
            btnSaveItem.Enabled = true;
        }


        txtCatalogueCode.Text = "";

        ViewState["SystemId"] = null;
        ViewState["SubSystemId"] = null;
        ViewState["ItemId"] = null;


        ucCustomPagerItems.isCountRecord = 1;

        string vesselcode = DDLVessel.SelectedValue.ToString();

        ViewState["VesselCode"] = DDLVessel.SelectedValue.ToString();
        ViewState["DeptCode"] = cmbDept.SelectedValue.ToString() == "ALL" ? null : cmbDept.SelectedValue;

        ddlAccountCode.SelectedValue = "6100";


        ViewState["CatlogueOperationMode"] = "ADD";
        hdfCatlogueOperationMode.Value = "ADD";
        ViewState["SubCatlogueOperationMode"] = "ADD";
        ViewState["ItemOperationMode"] = "ADD";
        hdfItemOperationMode.Value = "ADD";


        ViewState["NEXTSYSTEMID"] = GetNextSystemCode();
        txtCatalogueCode.Text = ViewState["NEXTSYSTEMID"].ToString();


        BindCatalogue();
        BindEmptySubCatalogGrid();
        BindEmptyItemGrid();


        btnAddNewItem.Enabled = false;
        btnSaveItem.Enabled = false;
        //btnDivAddLocation.Enabled = false;
        btnSelectAllItem.Enabled = false;




        ClearCatalogueFields();
        ClearSubCatalogueFields();
        ClearItemsFields();

        UpdCatelogueGrid.Update();
        UpdCatalogueEntry.Update();

        UpdSubCatelogueGrid.Update();
        UpdSubCatalogueEntry.Update();

        UpdItemGrid.Update();
        UpdItemEntry.Update();

    }

    protected void imgDeleteAssignLoc_click(object sender, ImageClickEventArgs e)
    {

        if ((String)ViewState["CatlogueOperationMode"] == "EDIT")
        {
            BLL_PMS_Library_Jobs objjobs = new BLL_PMS_Library_Jobs();
            if (lstcatalogLocation.SelectedValue != "")
            {
                objjobs.LibraryCatalogueLocationAssignDelete(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(lstcatalogLocation.SelectedValue), Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString()));
                BindCatalogue();
            }
            if (lstSubCatVesselLocation.SelectedValue != "")
            {
                //objjobs.LibraryCatalogueLocationAssignDelete(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(lstcatalogLocation.SelectedValue), Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString()));
                //BindCatalogue();
            }
        }

        if ((String)ViewState["CatlogueOperationMode"] == "ADD")
        {
            if (lstcatalogLocation.SelectedIndex != -1)
                lstcatalogLocation.Items.RemoveAt(lstcatalogLocation.SelectedIndex);
        }

    }

    protected void ImgSubSystemDelAssVslLocation_click(object sender, ImageClickEventArgs e)
    {
        BLL_PMS_Library_Jobs objjobs = new BLL_PMS_Library_Jobs();

        if (Convert.ToString(ViewState["SubCatlogueOperationMode"]) == "EDIT")
        {
            objjobs.LibraryCatalogueLocationAssignDelete(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(lstSubCatVesselLocation.SelectedValue), Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString()));
            BindSubCatalogue();
        }

        if ((String)ViewState["SubCatlogueOperationMode"] == "ADD")
        {
            if (lstSubCatVesselLocation.SelectedIndex != -1)
                lstSubCatVesselLocation.Items.RemoveAt(lstSubCatVesselLocation.SelectedIndex);
        }

    }

    protected void btnDivAddLocation_Click(object sender, EventArgs e)
    {
        lblErrMsg.Text = "";
        this.SetFocus("txtSearchLocation");
        ViewState["ISSYSTEMLOCATION"] = "1";
        string AssginLocmodal = String.Format("showModal('divAddLocation',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);

        BindCatalogueAssignLocation();
    }

    protected void btnSubCatDivAddLocation_Click(object sender, EventArgs e)
    {

        lblErrMsg.Text = "";
        this.SetFocus("txtSearchLocation");
        ViewState["ISSYSTEMLOCATION"] = "0";
        string AssginLocmodal = String.Format("showModal('divAddLocation',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);

        BindCatalogueAssignLocation();

    }



    protected void gvCatalogue_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["CATALOGUESORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["CATALOGUESORTDIRECTION"] != null && ViewState["CATALOGUESORTDIRECTION"].ToString() == "0")
            ViewState["CATALOGUESORTDIRECTION"] = 1;
        else
            ViewState["CATALOGUESORTDIRECTION"] = 0;

        BindCatalogue();

    }

    protected void gvSubCatalogue_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SUBCATALOGUESORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SUBCATALOGUESORTDIRECTION"] != null && ViewState["SUBCATALOGUESORTDIRECTION"].ToString() == "0")
            ViewState["SUBCATALOGUESORTDIRECTION"] = 1;
        else
            ViewState["SUBCATALOGUESORTDIRECTION"] = 0;

        BindSubCatalogue();

    }

    protected void gvItem_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["ITEMSORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["ITEMSORTDIRECTION"] != null && ViewState["ITEMSORTDIRECTION"].ToString() == "0")
            ViewState["ITEMSORTDIRECTION"] = 1;
        else
            ViewState["ITEMSORTDIRECTION"] = 0;

        BindItems();

    }

    private void SetCatalogueRowSelection()
    {
        gvCatalogue.SelectedIndex = -1;
        for (int i = 0; i < gvCatalogue.Rows.Count; i++)
        {
            if (gvCatalogue.DataKeys[i].Value.ToString().Equals(ViewState["SystemId"].ToString()))
            {
                gvCatalogue.SelectedIndex = i;

                ViewState["SystemId"] = ((Label)gvCatalogue.Rows[i].FindControl("lblSystemId")).Text;
                ViewState["SystemCode"] = ((Label)gvCatalogue.Rows[i].FindControl("lblSystemCode")).Text;
                ViewState["VesselCodeEditMode"] = ((Label)gvCatalogue.Rows[i].FindControl("lblVesselCode")).Text;
                ViewState["SubSystemId"] = null;
                ViewState["ItemId"] = null;


            }
        }
    }

    private void SetSubCatalogueRowSelection()
    {
        gvSubCatalogue.SelectedIndex = -1;
        for (int i = 0; i < gvSubCatalogue.Rows.Count; i++)
        {
            if (gvSubCatalogue.DataKeys[i].Value.ToString().Equals(ViewState["SubSystemId"].ToString()))
            {
                gvSubCatalogue.SelectedIndex = i;

                ViewState["SubSystemId"] = ((Label)gvSubCatalogue.Rows[i].FindControl("lblSubSystemID")).Text;
                ViewState["SubSystemCode"] = ((Label)gvSubCatalogue.Rows[i].FindControl("lblSubSystemCode")).Text;

                ViewState["ItemId"] = null;
            }
        }
    }

    private void SetItemsRowSelection()
    {
        gvItem.SelectedIndex = -1;
        for (int i = 0; i < gvItem.Rows.Count; i++)
        {
            if (gvItem.DataKeys[i].Value.ToString().Equals(ViewState["ItemId"].ToString()))
            {
                gvItem.SelectedIndex = i;
            }
        }
    }

    protected void lnbHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/crew/default.aspx");
    }


    #region  Search/Add Location popup

    public void BindCatalogueAssignLocation()
    {
        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["ITEMSORTBYCOLOUMN"] == null) ? null : (ViewState["ITEMSORTBYCOLOUMN"].ToString());

        int? sortdirection = null;
        if (ViewState["ITEMSORTDIRECTION"] != null)
            sortdirection = Int32.Parse(ViewState["ITEMSORTDIRECTION"].ToString());

        DataSet ds = objJobs.LibraryCatalogueLocationAssignSearch(ViewState["SystemCode"].ToString(), Convert.ToString(ViewState["SubSystemId"]), txtSearchLocation.Text, Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString()), sortbycoloumn
            , sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvLocation.DataSource = ds.Tables[0];
            gvLocation.DataBind();
        }
        else
        {
            gvLocation.DataSource = ds.Tables[0];
            gvLocation.DataBind();
        }
    }


    protected void imgLocationSearch_Click(object sender, ImageClickEventArgs e)
    {
        string AssginLocmodal = String.Format("showModal('divAddLocation',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);

        BindCatalogueAssignLocation();
    }

    public void btnDivLocationSave_Click(object sender, EventArgs e)
    {

        string AssginLocSavemodal = String.Format("showModal('divAddLocation',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AssginLocSavemodal", AssginLocSavemodal, true);


        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            LocationData objLocDo = new LocationData();

            // In PURC_LIB_SYSTEM_PARAMETERS  table for location  'ParentType = 1'

            objLocDo.ParentType = "1";
            objLocDo.ShortCode = txtLoc_ShortCode.Text;
            objLocDo.ShortDiscription = txtLoc_Description.Text;
            objLocDo.LongDiscription = "";
            objLocDo.CurrentUser = Session["userid"].ToString();
            objLocDo.NoOfLoc = txtNoLoc.Text;

            int retVal = objTechService.SaveLocation(objLocDo);

        }

        txtLoc_ShortCode.Text = "";
        txtLoc_Description.Text = "";
        txtNoLoc.Text = "1";
        BindCatalogueAssignLocation();
    }



    public void btnDivSave_click(object sender, EventArgs e)
    {
        Boolean blnRecSel = false;

        DataTable dt = new DataTable();

        dt.Columns.Add("LocationID");
        dt.Columns.Add("LocationName");


        string systemcode = Convert.ToString(ViewState["SystemId"]);
        string subsystemcode = ViewState["ISSYSTEMLOCATION"].ToString() == "0" ? Convert.ToString(ViewState["SubSystemId"]) : null;

        if (UDFLib.ConvertToInteger(systemcode) > 0)
        {
            foreach (GridViewRow gr in gvLocation.Rows)
            {
                CheckBox chkAssignLoc = (CheckBox)gr.FindControl("chkDivAssingLoc");

                if (chkAssignLoc.Checked == true && chkAssignLoc.Enabled == true)
                {
                    blnRecSel = true;
                    string Locationcode = ((Label)gr.FindControl("lblDivLocationCode")).Text;
                    string LocationName = ((Label)gr.FindControl("lblDivLocationName")).Text;
                    string Category_Code = ((CheckBox)gr.FindControl("chkIsSpare")).Checked ? "SP" : "AC";
                    BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
                    int retval = objJobs.LibraryCatalogueLocationAssignSave(Convert.ToInt32(Session["userid"].ToString())
                        , systemcode, subsystemcode, Convert.ToInt32(Locationcode), Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString()), Category_Code);

                    //AddDataTempLocation(Locationcode, LocationName, dt); 

                }
            }

            if (!blnRecSel)
            {

                lblErrMsg.Text = "Please select location/s to assign";

                string AssginLocmodal = String.Format("showModal('divAddLocation',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);
            }
            else
            {
                BindCatalogue();
                UpdCatalogueEntry.Update();

                lblErrMsg.Text = "";
                string AssginLocmodalHide = String.Format("hideModal('divAddLocation');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", AssginLocmodalHide, true);
            }
        }
        else
        {
            string AssginLocmodal = String.Format("alert(Please select system!)");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);
        }

    }

    public void btnDivCancel_click(object sender, EventArgs e)
    {
        string AssginLocmodalHide = String.Format("hideModal('divAddLocation');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", AssginLocmodalHide, true);

        UpdCatalogueEntry.Update();

    }


    public void AddDataTempLocation(string LocationID, string LocationName, DataTable dt)
    {

        DataRow dr;
        dr = dt.NewRow();

        dr["LocationID"] = LocationID;
        dr["LocationName"] = LocationName;

        dt.Rows.Add(dr);

        ViewState["TempDtLocation"] = dt;

    }



    protected void gvLocation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void gvLocation_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (((CheckBox)e.Row.FindControl("chkDivAssingLoc")).Checked)
            {
                ((Label)e.Row.FindControl("lblDivLocationCode")).ForeColor = System.Drawing.Color.Silver;
                ((Label)e.Row.FindControl("lblDivLocationCode")).Font.Bold = true;

                ((Label)e.Row.FindControl("lblDivShortCode")).ForeColor = System.Drawing.Color.Silver;
                ((Label)e.Row.FindControl("lblDivShortCode")).Font.Bold = true;

                ((Label)e.Row.FindControl("lblDivLocationName")).ForeColor = System.Drawing.Color.Silver;
                ((Label)e.Row.FindControl("lblDivLocationName")).Font.Bold = true;

                ((Label)e.Row.FindControl("lblDivMachinery")).ForeColor = System.Drawing.Color.Silver;
                ((Label)e.Row.FindControl("lblDivMachinery")).Font.Bold = true;
            }

        }

    }

    protected void gvLocation_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void gvLocation_Sorting(object sender, GridViewSortEventArgs e)
    {


    }


    #endregion


    private string ScriptSaveCatalogGridScroll()
    {
        //HiddenField1.Value is set in the getScrollPosition() javascript function
        //with the onscroll event on the divHolder in the markup
        StringBuilder strScript = new StringBuilder();

        //strScript.Append("<script language=javascript>");
        strScript.Append("var obj = document.getElementById('DivCatalogGridHolder');");
        strScript.Append("var objHidden = document.getElementById('" + hdfCatelogScrollPos.ClientID + "');");

        strScript.Append("objHidden.value");
        strScript.Append(" = " + hdfCatelogScrollPos.Value + ";");

        strScript.Append("obj.scrollTop=objHidden.value;");
        //strScript.Append("objHidden.value = obj.scrollLeft;");
        strScript.Append("objHidden.value = obj.scrollTop;");
        //strScript.Append("alert(obj.scrollTop);");
        //strScript.Append("</script>");

        return strScript.ToString();
    }

    private string ScriptSaveSubCatalogGridScroll()
    {
        //HiddenField1.Value is set in the getScrollPosition() javascript function
        //with the onscroll event on the divHolder in the markup
        StringBuilder strScript = new StringBuilder();

        //strScript.Append("<script language=javascript>");
        strScript.Append("var obj = document.getElementById('DivSubCatalogGridHolder');");
        strScript.Append("var objHidden = document.getElementById('" + hdfSubCatelogScrollPos.ClientID + "');");

        strScript.Append("objHidden.value");
        strScript.Append(" = " + hdfSubCatelogScrollPos.Value + ";");

        strScript.Append("obj.scrollTop=objHidden.value;");
        //strScript.Append("objHidden.value = obj.scrollLeft;");
        strScript.Append("objHidden.value = obj.scrollTop;");
        //strScript.Append("alert(obj.scrollTop);");
        //strScript.Append("</script>");

        return strScript.ToString();
    }

    protected void btnMakerHiddenSubmit_Click(object sender, EventArgs e)
    {
        BindSupplierDetails();
        UpdCatalogueEntry.Update();
    }
    
    //#region AutoRequisition
    //private void SaveAutoRequsition()
    //{
    //    BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
    //    try
    //    {
    //        int Company_ID = Convert.ToInt32(Session["USERCOMPANYID"]);
    //        int a = objBLLPurc.INSERT_AUTOMATIC_REQUISTION(Company_ID, ChkAuto_Req.Checked);
    //    }
    //    catch
    //    {
    //    }
    //}


    //protected void btnReq_Save_Click(object sender, EventArgs e)
    //{
    //    SaveAutoRequsition();
    //}

    //private void Get_Auto_Req()
    //{
    //    BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
    //    try
    //    {
    //        int Company_ID = Convert.ToInt32(Session["USERCOMPANYID"]);
    //        DataTable DT= objBLLPurc.GET_AUTOMATIC_REQUISTION(Company_ID);
    //        if (DT.Rows.Count > 0)
    //        {
    //            ChkAuto_Req.Checked = Convert.ToBoolean(DT.Rows[0]["Is_Auto_Requisition"]);
    //        }
    //    }
    //    catch
    //    {
    //    }
    //}
    //#endregion

    protected void chkRunHourSS_CheckedChanged(object sender, EventArgs e)
    {
        if (ViewState["SystemRHrs"].ToString().Trim() == "1" && chkRunHourSS.Checked == true)
        {
            chkCopyRunHourSS.Enabled = true;
        }
        else
        {
            chkCopyRunHourSS.Enabled = false;
            chkCopyRunHourSS.Checked = false;
        }
    }

}