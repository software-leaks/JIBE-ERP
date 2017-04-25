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
using SMS.Business.PMS;
using System.Collections;
using System.Text;
using SMS.Properties;


public partial class PMSMachineryChangeRequestAction : System.Web.UI.Page
{
    BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    public UserAccess objUA = new UserAccess();


    protected void Page_Load(object sender, EventArgs e)
    {

            UserAccessValidation();


        if (!IsPostBack)
        {
            BindSupplierDetails();
            BindAccountCode();
            BindDepartmentByST_SP();

            ddlAccountCode.SelectedValue = "6100";
            Bindfunction();

            BindMachineryChangeRequestsList();
        }
    }

    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {
            btnDivApprove.Enabled = false;
            btnDivReject.Enabled = false;

            //imgDeleteAssignLoc.Enabled = false;

        }
    }

    public void BindCatalogueList(string systemid)
    {
       
        DataSet ds = objBLLPurc.LibraryCatalogueList(systemid);

        BLL_PMS_Library_Jobs objJob = new BLL_PMS_Library_Jobs();






        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];

            string systemcode = dr["System_code"].ToString();

            txtName.Text = dr["System_Description"].ToString();
            txtParticular.Text = dr["System_Particulars"].ToString();
            txtSetsInstalled.Text = dr["Set_Instaled"].ToString();
            txtModel.Text = dr["Module_Type"].ToString();
            txtSerialNo.Text = dr["Serial_Number"].ToString();
            ddlMaker.SelectedValue = dr["Maker"].ToString() != "" ? dr["Maker"].ToString() : "0";

            //ddlCatalogDept.SelectedValue = dr["Dept1"].ToString() != "" ? dr["Dept1"].ToString() : "0";
            ddlDepartment.SelectedValue = dr["Dept1"].ToString() != "" ? dr["Dept1"].ToString() : "0";




            if ((dr["Functions"].ToString() != "") && (dr["Functions"].ToString() != "0"))
            {
                ddlFunction.SelectedValue = dr["Functions"].ToString();
            }
            else
            {
                ddlFunction.SelectedValue = "0";
            }

            if ((dr["ACCOUNT_CODE"].ToString() != "") && (dr["ACCOUNT_CODE"].ToString() != "0"))
            {
                ddlAccountCode.SelectedValue = dr["ACCOUNT_CODE"].ToString();
            }
            else
            {
                ddlAccountCode.SelectedValue = "0";
            }

            //DataTable dt = objJob.LibraryGetCatalogueLocationAssign(dr["System_code"].ToString(), Convert.ToInt32(dr["Vessel_Code"].ToString()));

            //if (dt.Rows.Count > 0)
            //{
            //    lstcatalogLocation.DataTextField = "LocationName";
            //    lstcatalogLocation.DataValueField = "AssginLocationID";
            //    lstcatalogLocation.DataSource = dt;
            //    lstcatalogLocation.DataBind();
            //}
            //else
            //{
            //    lstcatalogLocation.Items.Clear();
            //}

        }

    }

    protected void BindSupplierDetails()
    {
        using (BLL_PURC_Purchase objsupplier = new BLL_PURC_Purchase())
        {
            DataTable dt = objsupplier.SelectSupplier();
            dt.DefaultView.RowFilter = "SUPPLIER_CATEGORY='M'";
            ddlMaker.DataTextField = "SUPPLIER_NAME";
            ddlMaker.DataValueField = "SUPPLIER";
            ddlMaker.DataSource = dt.DefaultView.ToTable();
            ddlMaker.DataBind();


            ddlCatalogMaker.DataTextField = "SUPPLIER_NAME";
            ddlCatalogMaker.DataValueField = "SUPPLIER";
            ddlCatalogMaker.DataSource = dt.DefaultView.ToTable();
            ddlCatalogMaker.DataBind();

            ddlMaker.Items.Insert(0, new ListItem("--SELECT--", "0"));
            ddlCatalogMaker.Items.Insert(0, new ListItem("--SELECT--", "0"));
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
            ddlAccountCode.Items.Insert(0, new ListItem("--SELECT--", "0"));

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
                dtDepartment.DefaultView.RowFilter = "Form_Type='SP'";


                ddlDepartment.DataTextField = "Name_Dept";
                ddlDepartment.DataValueField = "Code";
                ddlDepartment.DataSource = dtDepartment;
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("--SELECT--", "0"));



                ddlCrDepartment.DataTextField = "Name_Dept";
                ddlCrDepartment.DataValueField = "Code";
                ddlCrDepartment.DataSource = dtDepartment;
                ddlCrDepartment.DataBind();
                ddlCrDepartment.Items.Insert(0, new ListItem("--SELECT--", "0"));

           
            }

        }
        catch (Exception ex)
        {

        }

    }

    public void Bindfunction()
    {
        DataTable dt = objBLLPurc.LibraryGetSystemParameterList("115", "");
        ddlFunction.Items.Clear();
        ddlFunction.DataSource = dt;
        ddlFunction.DataValueField = "CODE";
        ddlFunction.DataTextField = "DESCRIPTION";
        ddlFunction.DataBind();
        ddlFunction.Items.Insert(0, new ListItem("--SELECT--", "0"));
    }

    public void BindMachineryChangeRequestsList()
    {
        BLL_PMS_Change_Request objChangeRqst = new BLL_PMS_Change_Request();

        DataSet ds = objChangeRqst.TecMachineryChangeRequestList(UDFLib.ConvertIntegerToNull(Request.QueryString["Change_Reqst_ID"]), UDFLib.ConvertIntegerToNull(Request.QueryString["VESSELID"].ToString()));

        if (ds.Tables[0].Rows.Count > 0)
        {
            //Make a default font colour black.

            txtCrName.ForeColor = System.Drawing.Color.Black;
            txtCrParticular.ForeColor = System.Drawing.Color.Black;
            txtCrModel.ForeColor = System.Drawing.Color.Black;
            txtCrSetsInstalled.ForeColor = System.Drawing.Color.Black;
            txtCrMakerDetials.ForeColor = System.Drawing.Color.Black;
            txtCrReason.ForeColor = System.Drawing.Color.Black;


            DataRow dr = ds.Tables[0].Rows[0];

      
           

            if (dr["System_ID"].ToString() != "")
            {
                ViewState["System_ID"] = dr["System_ID"].ToString();
            }

            ViewState["REQUEST_FOR"] = dr["REQUEST_FOR"].ToString();
           

            ViewState["CR_Actual"] = dr["CR_Actual"].ToString();

            txtName.Text = dr["System_Description"].ToString();
            txtCrName.Text = dr["CR_System_Description"].ToString();
            txtParticular.Text = dr["System_Particulars"].ToString();
            txtCrParticular.Text = dr["CR_System_Particulars"].ToString();

            txtModel.Text = dr["Model"].ToString();
            txtCrModel.Text = dr["CR_Model"].ToString();

            txtSetsInstalled.Text = dr["Set_Installed"].ToString();
            txtCrSetsInstalled.Text = dr["CR_Set_Installed"].ToString();

            txtCrMakerDetials.Text = dr["CR_Maker"].ToString();
            txtCrReason.Text = dr["CR_Reason"].ToString();
            txtCrRemark.Text = dr["CR_Remarked_Actioned"].ToString();
            lblActionedBy.Text = dr["ACTIONEDBY"].ToString();
            lblRequestedBy.Text = dr["REQUESTBY"].ToString(); 
            hdfAddNewFlag.Value = dr["REQUEST_FOR"].ToString();

            ddlCatalogMaker.SelectedValue = dr["Maker_Code"].ToString();
       
            txtCrSerialNo.Text = dr["CR_Serial_Number"].ToString();


            ddlCrDepartment.SelectedValue = dr["CR_Dept1"].ToString();
            txtCrDepartChangeRemark.Text = dr["CR_Dept1_Change_Reason"].ToString();



            if (dr["System_ID"].ToString() != "")
            {
                BindCatalogueList(ViewState["System_ID"].ToString());
            }

            if (dr["DiffSysDescFlag"].ToString() == "N")
            {
                txtName.ForeColor = System.Drawing.Color.Blue;
                txtCrName.ForeColor = System.Drawing.Color.Blue;
            }

            if (dr["DiffSysPartFlag"].ToString() == "N")
            {
                txtParticular.ForeColor = System.Drawing.Color.Blue;
                txtCrParticular.ForeColor = System.Drawing.Color.Blue;
            }

            if (dr["DiffSetInstalledFlag"].ToString() == "N")
            {
                txtSetsInstalled.ForeColor = System.Drawing.Color.Blue;
                txtCrSetsInstalled.ForeColor = System.Drawing.Color.Blue;
            }

            if (dr["DiffModelFlag"].ToString() == "N")
            {
                txtModel.ForeColor = System.Drawing.Color.Blue;
                txtCrModel.ForeColor = System.Drawing.Color.Blue;
            }

            if (dr["REQUEST_FOR"].ToString() == "ADDNEW")
            {
                txtCrName.ForeColor = System.Drawing.Color.Red;
                txtCrParticular.ForeColor = System.Drawing.Color.Red;
                txtCrModel.ForeColor = System.Drawing.Color.Red;
                txtCrSetsInstalled.ForeColor = System.Drawing.Color.Red;
                txtCrMakerDetials.ForeColor = System.Drawing.Color.Red;
                txtCrReason.ForeColor = System.Drawing.Color.Red;
            }


            if (Request.QueryString["Status"].ToString().ToUpper() == "PENDING" || Request.QueryString["Status"].ToString() == "")
            {
                btnDivApprove.Enabled = true;
                btnDivReject.Enabled = true;
                txtCrName.ReadOnly = false;
                txtCrParticular.ReadOnly = false;
                txtCrModel.ReadOnly = false;
                txtCrSetsInstalled.ReadOnly = false;
               
                txtCrReason.ReadOnly = false;
                txtCrRemark.ReadOnly = false;

            }
            else
            {
                btnDivApprove.Enabled = false;
                btnDivReject.Enabled = false;

                txtCrName.ReadOnly = true;
                txtCrParticular.ReadOnly = true;
                txtCrModel.ReadOnly = true;
                txtCrSetsInstalled.ReadOnly = true;
               
                txtCrReason.ReadOnly = true;
                txtCrRemark.ReadOnly = true;

                ddlCatalogMaker.SelectedItem.Text = txtCrMakerDetials.Text;
              

            }

        }
    }

    protected void imgMakerRefresh_Click(object sender, ImageClickEventArgs e)
    {
        BindSupplierDetails();

    }

    protected void btnDivApprove_Click(object sender, EventArgs e)
    {
        try
        {
            BLL_PMS_Change_Request objChangeRqst = new BLL_PMS_Change_Request();
            BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();

            int? function = null;
            if (ddlFunction.SelectedValue != "")
                function = Convert.ToInt32(ddlFunction.SelectedValue);
            StringBuilder cr_actual_values = new StringBuilder();


            if ((string)ViewState["REQUEST_FOR"] == "ADDNEW")
            {
                string systemcode = "";

                objChangeRqst.TecMachineryChangeRequestSave(Convert.ToInt32(Session["userid"].ToString())
                    , Convert.ToInt32(Request.QueryString["Change_Reqst_ID"]), txtCrRemark.Text, txtCrName.Text, txtCrParticular.Text, UDFLib.ConvertStringToNull(ddlCatalogMaker.SelectedValue)
                    , txtCrSetsInstalled.Text, txtCrModel.Text, UDFLib.ConvertStringToNull(ddlCrDepartment.SelectedValue)
                    , function
                    , UDFLib.ConvertIntegerToNull(ddlAccountCode.SelectedValue), UDFLib.ConvertIntegerToNull(Request.QueryString["VESSELID"].ToString()), txtCrSerialNo.Text, ref systemcode);


            }

            if ((string)ViewState["REQUEST_FOR"] == "DELETE")
            {
                objChangeRqst.TecMachineryChangeRequestDelete(Convert.ToInt32(Session["userid"].ToString())
                   , Convert.ToInt32(Request.QueryString["Change_Reqst_ID"].ToString()), txtCrRemark.Text, Convert.ToInt32(Request.QueryString["VESSELID"].ToString()), Convert.ToInt32(ViewState["System_ID"].ToString()));
            }

            if ((string)ViewState["REQUEST_FOR"] == "EDIT")
            {
                cr_actual_values.Append("Machinery Name : ");
                cr_actual_values.Append(txtCrName.Text);
                cr_actual_values.AppendLine();
                cr_actual_values.Append("Maker :");
                cr_actual_values.Append(ddlMaker.SelectedValue != "0" ? ddlMaker.SelectedItem.Text : "");
                cr_actual_values.AppendLine();
                cr_actual_values.Append("Set Installed :");
                cr_actual_values.Append(txtSetsInstalled.Text);
                cr_actual_values.AppendLine();

                cr_actual_values.Append("Department :");/* This store actual department before changing */
                cr_actual_values.Append(ddlDepartment.SelectedValue != "0" ? ddlDepartment.SelectedItem.Text : "");
                cr_actual_values.AppendLine();

                cr_actual_values.Append("Model :");
                cr_actual_values.Append(txtCrModel.Text);
                cr_actual_values.AppendLine();
                cr_actual_values.Append("Particular :");
                cr_actual_values.Append(txtCrParticular.Text);
                cr_actual_values.Append("Actioned Remark  :");
                cr_actual_values.Append(txtCrRemark.Text);

                /* parameter "dept" is added to "TecMachineryChangeRequestUpdate" function By Someshwar On 09-06-2016   */
                objChangeRqst.TecMachineryChangeRequestUpdate(Convert.ToInt32(Session["userid"].ToString())
                   , Convert.ToInt32(Request.QueryString["Change_Reqst_ID"].ToString()), txtCrRemark.Text, Convert.ToInt32(ViewState["System_ID"].ToString()), txtCrName.Text, txtCrParticular.Text
                   , txtCrSetsInstalled.Text, txtCrModel.Text, UDFLib.ConvertIntegerToNull(Request.QueryString["VESSELID"].ToString()), ddlCrDepartment.SelectedValue, cr_actual_values.ToString());

            }


            String Mscript = String.Format("alert('Change Request has been Approved.');javascript:parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mscript", Mscript, true);

        }
        catch (Exception ex)
        {
            String ErrScr1 = String.Format("alert("+ ex.Message.ToString() +");");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErrScr1", ErrScr1, true);
        }
    }


    protected void btnDivReject_Click(object sender, EventArgs e)
    {

        BLL_PMS_Change_Request objChangeRqst = new BLL_PMS_Change_Request();


        objChangeRqst.TecMachineryChangeRequestReject(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(Request.QueryString["Change_Reqst_ID"].ToString())
            , txtCrRemark.Text, Convert.ToInt32(Request.QueryString["VESSELID"].ToString()));

        String Mscript = String.Format("alert('Change Request has been Rejected.');javascript:parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mscript", Mscript, true);
    }

    

    protected void btnDivAssignLocation_Click(object sender, EventArgs e)
    {
        lblErrMsg.Text = "";
        this.SetFocus("txtSearchLocation");

        string AssginLocmodal = String.Format("showModal('divAddLocation',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);

        BindCatalogueAssignLocation();
    }

    protected void imgDeleteAssignLoc_click(object sender, ImageClickEventArgs e)
    {
        //if (lstcatalogLocation.SelectedIndex != -1)
        //    lstcatalogLocation.Items.RemoveAt(lstcatalogLocation.SelectedIndex);


    }

    #region  Search/Add Location popup

    public void BindCatalogueAssignLocation()
    {
        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
        int rowcount = ucDivCustomPager.isCountRecord;

        string System_ID = (ViewState["System_ID"] == null) ? null : (ViewState["System_ID"].ToString());

        string sortbycoloumn = (ViewState["ITEMSORTBYCOLOUMN"] == null) ? null : (ViewState["ITEMSORTBYCOLOUMN"].ToString());

        int? sortdirection = null;
        if (ViewState["ITEMSORTDIRECTION"] != null)
            sortdirection = Int32.Parse(ViewState["ITEMSORTDIRECTION"].ToString());


        DataSet ds = objJobs.LibraryCatalogueLocationAssignSearch(System_ID,null, txtSearchLocation.Text, Convert.ToInt32(ViewState["Vessel_id"].ToString()), sortbycoloumn
            , sortdirection, ucDivCustomPager.CurrentPageIndex, ucDivCustomPager.PageSize, ref rowcount);


        if (ucDivCustomPager.isCountRecord == 1)
        {
            ucDivCustomPager.CountTotalRec = rowcount.ToString();
            ucDivCustomPager.BuildPager();
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
        //divAddLocation.Visible = true;

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

            int retVal = objTechService.SaveLocation(objLocDo);

        }

        txtLoc_ShortCode.Text = "";
        txtLoc_Description.Text = "";

        BindCatalogueAssignLocation();

    }



    public void btnDivSave_click(object sender, EventArgs e)
    {

        Boolean blnRecSel = false;

        DataTable dt = new DataTable();
        dt.Columns.Add("LocationID");
        dt.Columns.Add("LocationName");

        string System_ID = (ViewState["System_ID"] == null) ? null : (ViewState["System_ID"].ToString());

        
 
        foreach (GridViewRow gr in gvLocation.Rows)
        {
            CheckBox chkAssignLoc = (CheckBox)gr.FindControl("chkDivAssingLoc");

            if (chkAssignLoc.Checked == true && chkAssignLoc.Enabled == true)
            {
                blnRecSel = true;
                string Locationcode = ((Label)gr.FindControl("lblDivLocationCode")).Text;
                string LocationName = ((Label)gr.FindControl("lblDivLocationName")).Text;

                if (!string.IsNullOrEmpty(System_ID)) // When Change request is in (update or delete Mode)
                {
                    string Category_Code = ((CheckBox)gr.FindControl("chkIsSpare")).Checked ? "SP" : "AC";

                    BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
                    int retval = objJobs.LibraryCatalogueLocationAssignSave(Convert.ToInt32(Session["userid"].ToString())
                        , System_ID, null, Convert.ToInt32(Locationcode), Convert.ToInt32(ViewState["Vessel_id"].ToString()), Category_Code);
                }
                else
                {
                    // When Change request is in  Add Mode
                    AddDataTempLocation(Locationcode, LocationName, dt);
                }
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
            lblErrMsg.Text = "";
            string AssginLocmodalHide = String.Format("hideModal('divAddLocation');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", AssginLocmodalHide, true);
        }

        if (!string.IsNullOrEmpty(System_ID))
        {
            BindCatalogueList(System_ID);
        }
        else
        {

            //lstcatalogLocation.DataSource = (DataTable)ViewState["TempDtLocation"];
            //lstcatalogLocation.DataTextField = "LocationName";
            //lstcatalogLocation.DataValueField = "LocationID";
            //lstcatalogLocation.DataBind();
        }


        updCatalogue.Update();

    }

    public void btnDivCancel_click(object sender, EventArgs e)
    {


        string AssginLocmodalHide = String.Format("hideModal('divAddLocation');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", AssginLocmodalHide, true);

        //UpdCatalogueEntry.Update();

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

   
    //protected void lnkAddMaker_Click(object sender, EventArgs e)
    //{
    //    lnkAddMaker.Attributes.Add("onclick", "javascript:window.open('../../Infrastructure/Libraries/Supplier_Makers.aspx'); return false;");
    //}
    #endregion

    protected void btnClearFilter_Click(object sender, ImageClickEventArgs e)
    {
        BindSupplierDetails();
    }
}