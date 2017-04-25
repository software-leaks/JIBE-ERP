using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.VET;
//using System.Drawing;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using SMS.Properties;
public partial class Technical_Vetting_Vetting_Planner : System.Web.UI.Page
{
    MergeGridviewHeader_Info objVetMerge = new MergeGridviewHeader_Info();
    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    public string strDateFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");

        strDateFormat = UDFLib.GetDateFormat();
        String msgretv = String.Format("setTimeout(getOperatingSystem,500);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);
        UserAccessValidation();
        if (!IsPostBack)
        {
            BindFleetDLL();
            //BindCFleetDLL();
            if (Session["USERFLEETID"] == null)
            {
                DDLFleet.SelectedValue = "0";
            }
            else
            {
                DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            }

            BindVesselDDL();
            BindOilMajorDDL();
            BindVettingTypeDDL();
            BindInspectorDDL();
            BindCVesselDDL();
            CheckedAssignVessel(DDLVessel);
            CheckedAssignVessel(DDLCVessel);
            lblPageTitle.Text = "Vetting Planner";

            BindVetting();
            BindCalVetting(DateTime.Now);
            BindMonths();
            DDLMonth.SelectedValue = DateTime.Now.Month.ToString();
            ViewState["CurrentIndex"] = 1;
            ViewState["CurrentDate"] = DateTime.Now;
            tbCntr.ActiveTabIndex = 0;
        }
        setDateFormat();
     
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        
    }
    /// <summary>
    /// Method is used to check wheather user have access to this page or not.
    /// </summary>
    protected void UserAccessValidation()
    {
       try
       {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
          

        }

        ViewState["del"] = objUA.Delete;
        ViewState["edit"] = objUA.Edit;

       }
       catch (Exception ex)
       {
           UDFLib.WriteExceptionLog(ex);
       }

    }

    /// <summary>
    /// This function the date format to datepicker as per the format set in User Setting
    /// </summary>
    public void setDateFormat()
    {
        try
        {
        cexLVetFromDate.Format = UDFLib.GetDateFormat();
        cexLVetToDate.Format = UDFLib.GetDateFormat();
        cexEVetFromDate.Format = UDFLib.GetDateFormat();
        cexEVetToDate.Format = UDFLib.GetDateFormat();
        cexPVetFromDate.Format = UDFLib.GetDateFormat();
        cexPVetToDate.Format = UDFLib.GetDateFormat();
        cexCLVetFromDate.Format = UDFLib.GetDateFormat();
        cexCLVetToDate.Format = UDFLib.GetDateFormat();
        cexCEVetFromDate.Format = UDFLib.GetDateFormat();
        cexCEVetToDate.Format = UDFLib.GetDateFormat();
        cexCPVetFromDate.Format = UDFLib.GetDateFormat();
        cexCPVetToDate.Format = UDFLib.GetDateFormat();
       
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method is used to get login user id
    /// </summary>
    /// <returns>retrun user id</returns>
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    /// <summary>
    /// Bind Fleet dropdown
    /// </summary>
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


            DDLCFleet.Items.Clear();
            DDLCFleet.DataSource = FleetDT;
            DDLCFleet.DataTextField = "Name";
            DDLCFleet.DataValueField = "code";
            DDLCFleet.DataBind();
            ListItem li1 = new ListItem("--SELECT ALL--", "0");
            DDLCFleet.Items.Insert(0, li1);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// Bind Vessel Dropdown
    /// </summary>
    public void BindVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataBind();

         
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// Bind Calender view Vessel Dropdown
    /// </summary>
    public void BindCVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLCFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLCVessel.DataTextField = "Vessel_name";
            DDLCVessel.DataValueField = "Vessel_id";
            DDLCVessel.DataSource = dtVessel;
            DDLCVessel.DataBind();

          
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    public void CheckedAssignVessel(UserControl_ucCustomDropDownList ucfDropdown)
    {
        BLL_VET_VettingLib objVsl = new BLL_VET_VettingLib();
        DataTable dtUserVessel = objVsl.VET_Get_UserVesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID());
        if (dtUserVessel.Rows.Count > 0)
        {
            CheckBoxList chk = (CheckBoxList)ucfDropdown.Controls[0].Controls[0].FindControl("CheckBoxListItems");

            for (int j = 0; j < chk.Items.Count; j++)
            {
                for (int i = 0; i < dtUserVessel.Rows.Count; i++)
                {


                    if (chk.Items[j].Value == dtUserVessel.Rows[i]["Vessel_ID"].ToString())
                    {
                        ((CheckBoxList)ucfDropdown.Controls[0].Controls[0].FindControl("CheckBoxListItems")).Items[j].Selected = true;
                    }


                }
            }


        }
    }
    /// <summary>
    /// Bind Vetting Type Dropdown
    /// </summary>
    public void BindVettingTypeDDL()
    {
        try
        {

            BLL_VET_Planner objVsl = new BLL_VET_Planner();

            DataTable dtVetType = objVsl.VET_Get_VettingType();           
            DDLVettingType.DataTextField = "Vetting_Type_Name";
            DDLVettingType.DataValueField = "Vetting_Type_ID";
            DDLVettingType.DataSource = dtVetType;
            DDLVettingType.DataBind();

            DDLCVettingType.DataTextField = "Vetting_Type_Name";
            DDLCVettingType.DataValueField = "Vetting_Type_ID";
            DDLCVettingType.DataSource = dtVetType;
            DDLCVettingType.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Bind Oil Major dropdown
    /// </summary>
    public void BindOilMajorDDL()
    {
        try
        {

            BLL_VET_VettingLib objOilMajor = new BLL_VET_VettingLib();
            DataTable dtOilMajor = objOilMajor.VET_Get_OilMajorList();
            DDLOilMajors.DataTextField = "Display_Name";
            DDLOilMajors.DataValueField = "ID";
            DDLOilMajors.DataSource = dtOilMajor;
            DDLOilMajors.DataBind();

            
            DDLCOilMajor.DataTextField = "Display_Name";
            DDLCOilMajor.DataValueField = "ID";
            DDLCOilMajor.DataSource = dtOilMajor;
            DDLCOilMajor.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Bind Inspector Dropdown
    /// </summary>
    public void BindInspectorDDL()
    {
        try
        {

            BLL_VET_VettingLib objInsp = new BLL_VET_VettingLib();

            DataTable dtInsp = objInsp.VET_Get_ExtInspectorList();          
            DDLInspector.DataTextField = "NAME";
            DDLInspector.DataValueField = "UserID";
            DDLInspector.DataSource = dtInsp;
            DDLInspector.DataBind();

            DDLCInspector.DataTextField = "NAME";
            DDLCInspector.DataValueField = "UserID";
            DDLCInspector.DataSource = dtInsp;
            DDLCInspector.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Bind Calendar view Months Dropdown
    /// </summary>
    public void BindMonths()
    {
        try{
        DataTable dtMonth = new DataTable();
        dtMonth.Columns.Add("Month_No");
        dtMonth.Columns.Add("Month_Name");
        DataRow dr;
        dr = dtMonth.NewRow();
        dr[0] = 0;
        dr[1] = "--Select Month--";
        dtMonth.Rows.Add(dr);
        for (int i = 1; i <= 12; i++)
        {
            dr = dtMonth.NewRow();
            
            dr[0]=i;
            dr[1]=new DateTime(2010, i, 1).ToString("MMMM");
            dtMonth.Rows.Add(dr);
        }

        DDLMonth.DataSource = dtMonth;
        DDLMonth.DataTextField = "Month_Name";
        DDLMonth.DataValueField = "Month_No";
        DDLMonth.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    /// <summary>
    /// Bind Overview Grid
    /// </summary>
    public void BindVetting()
    {
        try
        {
            BLL_VET_Planner objBlPlan = new BLL_VET_Planner();
            DataSet ds = new DataSet();

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            BLL_VET_VettingLib objOilMajor = new BLL_VET_VettingLib();
            int rowcount=0;
            string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            int IsPlanned = 0;
                   
            
            DataTable dtVessel = new DataTable();
            DataTable dtOilMajors = new DataTable();
            DataTable dtVettingTypes = new DataTable();
            DataTable dtIntInspector = new DataTable();
            DataTable dtExtInspector = new DataTable();

            dtVessel.Columns.Add("VID");
            dtOilMajors.Columns.Add("OMID");
            dtVettingTypes.Columns.Add("VTID");
            dtIntInspector.Columns.Add("VID");
            dtExtInspector.Columns.Add("VID");
       
            
            dtVessel = DDLVessel.SelectedValues;

            if (dtVessel.Rows.Count <= 0)/*Add all vessel to data table if no vessel is selected in dropdown
                                          * */
            {
                DataTable dtVes = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
                foreach (DataRow dr in dtVes.Rows)
                {
                    dtVessel.Rows.Add(dr[0]);
                }
            }
            dtOilMajors = DDLOilMajors.SelectedValues;
            dtVettingTypes = DDLVettingType.SelectedValues;
            dtExtInspector = DDLInspector.SelectedValues;
                   
            if (rbtVetState.Items[0].Selected == true)
            {
                IsPlanned = 0;
            }
            else if (rbtVetState.Items[1].Selected == true)
            {
                IsPlanned=1;
            }
            else if (rbtVetState.Items[2].Selected == true)
            {
                IsPlanned = 2;
            }
            int? jobid = null;
            int? isHistory = 0;


            DateTime? LVetFromDate,LVetToDate,EVetFromDate,EVetToDate,PVetFromDate,PVetToDate;
            LVetFromDate = txtLVetFromDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtLVetFromDate.Text));
            LVetToDate = txtLVetToDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtLVetToDate.Text));
            EVetFromDate = txtEVetFromDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtEVetFromDate.Text));
            EVetToDate = txtEVetToDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtEVetToDate.Text));
            PVetFromDate = txtPVetDateFromDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtPVetDateFromDate.Text));
            PVetToDate = txtPVetDateToDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtPVetDateToDate.Text));
            ds = objBlPlan.VET_Get_Vetting(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), dtVessel, dtOilMajors, IsPlanned, dtVettingTypes, UDFLib.ConvertIntegerToNull(DDLExInDays.SelectedValue), dtExtInspector, LVetFromDate, LVetToDate, EVetFromDate, EVetToDate, PVetFromDate, PVetToDate, sortbycoloumn, sortdirection);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                DataTable dtGroupHead = ds.Tables[1];
                DataTable dtVesVettingSetting = ds.Tables[2];
                ViewState["dtVesVettingSetting"] = dtVesVettingSetting;
                DataTable dtOverview = CreateOverview(new string[] { "Vetting_Type_ID", "Vetting_Type_Name", "Vessel_Name" }, "Vessel_Name", new string[] { "VESSEL_ID" }, new string[] { "ROWNUM", "FleetCode", "Vessel_Code" }, dt, dtGroupHead);

                DataTable dtOverviewTemp = dtOverview.Copy();


                for (int i = 0; i < dtOverview.Rows.Count; i++) /* This will copy Vessel Name and Vessel ID of Different Vetting Type group into  Vessel Name and Vessel ID Column of 1st Vetting type group in Grid*/
                {
                    foreach (DataColumn dcol in dtOverview.Columns)
                    {
                        if (dcol.ColumnName.Contains("Vessel_ID"))
                        {
                            if (dtOverviewTemp.Columns.IndexOf(dcol.ColumnName) > 0)
                            {
                                if (dtOverview.Rows[i][dcol].ToString().Trim() != "")
                                {
                                    dtOverview.Rows[i][0] = dtOverview.Rows[i][dcol].ToString();
                                }

                            }
                        }
                        if (dcol.ColumnName.Contains("Vessel_Name"))
                        {
                            if (dtOverviewTemp.Columns.IndexOf(dcol.ColumnName) > 1)
                            {
                                if (dtOverview.Rows[i][dcol].ToString().Trim() != "")
                                {
                                    dtOverview.Rows[i][1] = dtOverview.Rows[i][dcol].ToString();
                                }
                            }
                        }

                    }
                }


                foreach (DataColumn dcol in dtOverviewTemp.Columns)  /* This will remove Vessel ID and Vessel Name column Of different Vetting Type group except 1 vetting type group*/
                {
                    if (dcol.ColumnName.Contains("Vessel_ID"))
                    {
                        if (dtOverviewTemp.Columns.IndexOf(dcol.ColumnName.ToString()) > 0)
                        {

                            dtOverview.Columns.Remove(dcol.ColumnName.ToString());

                        }
                    }
                    if (dcol.ColumnName.Contains("Vessel_Name"))
                    {
                        if (dtOverviewTemp.Columns.IndexOf(dcol.ColumnName.ToString()) > 1)
                        {

                            dtOverview.Columns.Remove(dcol.ColumnName.ToString());
                        }
                    }


                }

                foreach (DataColumn dcol in dtOverviewTemp.Columns)
                {
                    if (dcol.ColumnName.Contains("Vessel_ID"))
                    {
                        if (dtOverviewTemp.Columns.IndexOf(dcol) == 0)
                        {
                            gvVetting.DataKeyNames = new[] { dcol.ColumnName.ToString() };
                        }
                    }
                }
                gvVetting.DataSource = dtOverview;
                gvVetting.DataBind();
                if (dtOverview.Rows.Count > 0)
                {
                    btnExport.Enabled = true;
                }
                else
                {
                    btnExport.Enabled = false;
                }



                UpdPnlGrid.Update();
                UpdPnlFilter.Update();

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Bind Calendar View Grid
    /// </summary>
    /// <param name="dtCenterMonth">   This Contain 1st date of month as per month selected in month Dropdown  </param>
    public void BindCalVetting(DateTime dtCenterMonth)
    {
        try
        {
            BLL_VET_Planner objBlPlan = new BLL_VET_Planner();
            DataSet ds = new DataSet();

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            BLL_VET_VettingLib objOilMajor = new BLL_VET_VettingLib();
            int rowcount = 0;
            string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            int IsPlanned = 0;

        

            DataTable dtVessel = new DataTable();
            DataTable dtOilMajors = new DataTable();
            DataTable dtVettingTypes = new DataTable();
            DataTable dtIntInspector = new DataTable();
            DataTable dtExtInspector = new DataTable();

            dtVessel.Columns.Add("VID");
            dtOilMajors.Columns.Add("OMID");
            dtVettingTypes.Columns.Add("VTID");
            dtIntInspector.Columns.Add("VID");
            dtExtInspector.Columns.Add("VID");
            dtVessel = DDLCVessel.SelectedValues;
            
            if (dtVessel.Rows.Count <= 0)/*Add all vessel to data table if no vessel is selected in dropdown
                                          * */
            {
                DataTable dtVes = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLCFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
                foreach (DataRow dr in dtVes.Rows)
                {
                    dtVessel.Rows.Add(dr[0]);
                }
            }
            dtOilMajors = DDLCOilMajor.SelectedValues;
            dtVettingTypes = DDLCVettingType.SelectedValues;


            dtExtInspector = DDLCInspector.SelectedValues;
        
            if (rbtCVetState.Items[0].Selected == true)
            {
                IsPlanned = 0;
            }
            else if (rbtCVetState.Items[1].Selected == true)
            {
                IsPlanned = 1;
            }
            else if (rbtCVetState.Items[2].Selected == true)
            {
                IsPlanned = 2;
            }
            int? jobid = null;
            int? isHistory = 0;


            DateTime? CLVetFromDate, CLVetToDate, CEVetFromDate, CEVetToDate, CPVetFromDate, CPVetToDate;
            CLVetFromDate = txtCLVetFromDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtCLVetFromDate.Text));
            CLVetToDate = txtCLVetToDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtCLVetToDate.Text));
            CEVetFromDate = txtCEVetFromDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtCEVetFromDate.Text));
            CEVetToDate = txtCEVetToDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtCEVetToDate.Text));
            CPVetFromDate = txtCPVetFromDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtCPVetFromDate.Text));
            CPVetToDate = txtCPVetToDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtCPVetToDate.Text));

            ds = objBlPlan.VET_Get_CalendarViewVetting(UDFLib.ConvertIntegerToNull(DDLCFleet.SelectedValue), dtVessel, dtOilMajors, IsPlanned, dtVettingTypes, UDFLib.ConvertIntegerToNull(DDLCExInDays.SelectedValue), dtIntInspector, dtExtInspector, CLVetFromDate, CLVetToDate, CEVetFromDate, CEVetToDate, CPVetFromDate, CPVetToDate, sortbycoloumn, sortdirection);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                DataTable dtVetExpDate = ds.Tables[1];
                DataTable dtObsNotesCount = ds.Tables[2];
                DataTable dtJobsCount = ds.Tables[3];

                ViewState["dtVetExpDate"] = dtVetExpDate;
                ViewState["dtObsNotesCount"] = dtObsNotesCount;
                ViewState["dtJobsCount"] = dtJobsCount;



                DataTable dtCalview = CreateCalenderview("Vetting_Type_Name", "Vetting_Date", new string[] { "VESSEL_ID", "Vessel_Name", "Vetting_Type_ID", "Vetting_Type_Name" }, new string[] { "ROWNUM", "FleetCode", "Vessel_Code" }, dtCenterMonth, dt);


                gvCalVetting.DataSource = dtCalview;
                gvCalVetting.DataBind();
             
                if (dtCalview.Rows.Count > 0)
                {
                    btnCExport.Enabled = true;
                }
                else
                {
                    btnCExport.Enabled = false;
                }
                updCPnlGrid.Update();
                UpdCPnlFilter.Update();
            }


        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }


    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselDDL();
    }
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objVetMerge.MergedColumns.Clear();
            objVetMerge.StartColumns.Clear();
            objVetMerge.Titles.Clear();

            BindVetting();
            
            ExportGridviewToExcel(gvVetting,"Vetting Overview");

          
        }
        catch (Exception ex)
        {

            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
        finally
        {


        }
    }


    /// <summary>
    /// This will export data to Excel
    /// </summary>
    /// <param name="GridViewexp"> Object Of Gridview to be exported </param>
    /// <param name="ExportHeader"> Header to be set to exported excel</param>
    public void ExportGridviewToExcel(GridView GridViewexp,string ExportHeader)
    {

        try
        {
        // Reference your own GridView here

        string filename = String.Format("Vetting_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(),
            DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString());

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
        HttpContext.Current.Response.Charset = "";

        // SetCacheability doesn't seem to make a difference (see update)
        HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

        HttpContext.Current.Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

        // Replace all gridview controls with literals
   
        GridViewRow grh = GridViewexp.HeaderRow;      
        grh.ForeColor = System.Drawing.Color.Black;
        grh.HorizontalAlign = HorizontalAlign.Left;
        GridViewexp.GridLines = GridLines.Both;     
           
        foreach (TableCell cl in grh.Cells)
        {
             
            cl.HorizontalAlign = HorizontalAlign.Left;         
            cl.Attributes.Add("class", "text");
            PrepareControlForExport_GridView(cl);
            
        }


        foreach (GridViewRow gr in GridViewexp.Rows)
        {
            
            foreach (TableCell cl in gr.Cells)
            {
                cl.HorizontalAlign = HorizontalAlign.Left;
                
          
                cl.Attributes.Add("class", "text");
                PrepareControlForExport_GridView(cl);
              
            }
        }


        System.Web.UI.HtmlControls.HtmlForm form
            = new System.Web.UI.HtmlControls.HtmlForm();
        Controls.Add(form);

        Label lbl=new Label();
        lbl.Text = ExportHeader;
        lbl.Font.Size = 14;
        lbl.Font.Bold = true;
        form.Controls.Add(lbl);
        
        form.Controls.Add(GridViewexp);
        form.RenderControl(htmlWriter);
        string style = @"<style> .text { mso-number-format:\@; } </style> ";
        HttpContext.Current.Response.Write(style);
        HttpContext.Current.Response.Write(stringWriter.ToString());
        HttpContext.Current.Response.End();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }
   
    /// <summary>
    /// This will prepare gridview before export( for e.g This will convert link button to a normal text )
    /// </summary>
    /// <param name="control"> Object of the control to be converted to normal text+   </param>
    public static void PrepareControlForExport_GridView(Control control)
    {
      try
      {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            
            if (current is LinkButton)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as LinkButton).Text;

            }
            else if (current is ImageButton)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as ImageButton).ToolTip;
            }
            else if (current is HyperLink)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as HyperLink).Text;
            }
            else if (current is DropDownList)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as DropDownList).Items.Count > 0 ? (current as DropDownList).SelectedItem.Text : ""; ;
            }
            else if (current is CheckBox)
            {

                current.Visible = false;
            }
            else if (current is TextBox)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as TextBox).Text;

            }
            else if (current is Image)
            {
                TableCell cl = control as TableCell;
              
                cl.Text = (current as Image).AlternateText;

            }
            else if (current is Label)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as Label).Text;

            }


        }
      }
      catch (Exception ex)
      {
          UDFLib.WriteExceptionLog(ex);
      }

    }

    
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
  
  
    protected void btnRetrieve_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        BindVetting();
        if (hfAdv.Value == "o")
        {
            String tgladvsearch = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearch", tgladvsearch, true);
        }
        else
        {
            String tgladvsearch1 = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearch1", tgladvsearch1, true);
        }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void btnClearFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        DDLFleet.SelectedValue = "0";
        DDLVessel.ClearSelection();
        DDLExInDays.SelectedValue = "0";
        DDLOilMajors.ClearSelection();
        DDLVettingType.ClearSelection();
        DDLInspector.ClearSelection();
      //  rbtVetState.Items[0].Selected = true;
        rbtVetState.SelectedValue = "0";
        chkOVesselAssign.Checked = true;
        BindVesselDDL();
        CheckedAssignVessel(DDLVessel);
             
        txtEVetFromDate.Text = "";
        txtEVetToDate.Text = "";
        txtLVetFromDate.Text = "";
        txtLVetToDate.Text = "";
        txtPVetDateFromDate.Text = "";
        txtPVetDateToDate.Text = "";
        
        UpdAdvFltr.Update();
        UpdPnlGrid.Update();

        BindVetting();
       
        if (hfAdv.Value == "o")
        {
            String tgladvsearchClr = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearchClr", tgladvsearchClr, true);
        }
        else
        {
            String tgladvsearchClr1 = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearchClr1", tgladvsearchClr1, true);
        }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void gvVetting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
        BLL_VET_Planner objVetP=new BLL_VET_Planner();
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Width = 200;

           /*Hide Vetting_Type_ID column*/
            for (int i = 8; i <= e.Row.Controls.Count; i+=7)
            {
                if (i < e.Row.Controls.Count)
                {
                    e.Row.Cells[i].Visible = false;
                }
            }
            for (int i = 4; i <= e.Row.Controls.Count; i += 7)
            {
                if (i < e.Row.Controls.Count)
                {
                    e.Row.Cells[i].Visible = false;
                }
            }
            
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Height = 30;
            e.Row.Cells[1].Attributes.Add("style", "border-left:1px solid #cacaca; border-right:1px solid #cacaca;");
 
            string dateFormat = UDFLib.GetDateFormat();
            for (int i = 3; i <= e.Row.Controls.Count; i += 7)/*this will add image into Oil Major Logo Column */
            {
                e.Row.Cells[i-1].Width = 100;
                e.Row.Cells[i].Width = 350;
                e.Row.Cells[i+2].Width = 200;
                e.Row.Cells[i + 3].Width = 100;
                e.Row.Cells[i + 4].Width = 100;
                if (i < e.Row.Controls.Count)
                {

                    if (e.Row.Cells[i].Text.Trim() != "" && e.Row.Cells[i].Text.Trim()!="&nbsp;") 
                    {

                        System.Web.UI.WebControls.Image Imgtemps = new System.Web.UI.WebControls.Image();
                        Imgtemps.ImageUrl = e.Row.Cells[i+1].Text;
                        Imgtemps.Visible = true;
                        Imgtemps.Height = 20;
                       
                       

                        Table table = new Table();
                        //  table.Attributes.Add("style", "border-collapse: separate;border-spacing: 1px; border-collapse: expression('separate', cellSpacing = '1px');");
                        table.Attributes.Add("align", "center");
                        table.Attributes.Add("style", "broder:none !important");
                        TableRow tr = new TableRow();
                        TableCell td = new TableCell();
                        TableCell td1 = new TableCell();
                        table.Attributes.Add("style", "white-space:break;");
                        td.Text = e.Row.Cells[i].Text;
                        tr.Controls.Add(td);
                        if (e.Row.Cells[i + 1].Text.Trim() != "")
                        {
                            if (File.Exists(Server.MapPath(e.Row.Cells[i + 1].Text)))
                            {
                                td1.Controls.Add(Imgtemps);
                                tr.Controls.Add(td1);
                            }
                        }
                        table.Controls.Add(tr);
                        table.Attributes.Add("align", "left");
                        e.Row.Cells[i].Controls.Add(table);
                        
                    }
                }
            }
           
            for (int i = 2; i <= e.Row.Controls.Count; i += 7)/* This will set date format for date displayed in Last Vetting Date Column*/
            {
                if (i < e.Row.Controls.Count)
                {

                    if (e.Row.Cells[i].Text.Trim() != "" && e.Row.Cells[i].Text.Trim() != "&nbsp;")
                    {

                        e.Row.Cells[i].Text = UDFLib.ConvertUserDateFormat(e.Row.Cells[i].Text); //DateTime.Parse(e.Row.Cells[i].Text).ToString(dateFormat); 


                        string State = e.Row.Cells[i + 3].Text.ToString().Split(new string[] { "~_" }, StringSplitOptions.None)[1];
                        if (State == "In-Progress")
                        {
                            e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow; /* This will highlight Last Vetting date Column */
                        }
                        e.Row.Cells[i + 3].Text = e.Row.Cells[i + 3].Text.ToString().Split(new string[] { "~_" }, StringSplitOptions.None)[0];
                        
                                           
                    }
                }
               
            }
            for (int i = 6; i <= e.Row.Controls.Count; i += 7)/* This will set date format for date displayed in Expiry Date Column along with backcolor based the dates*/
            {

                  DataTable dtVesVettingSetting= (DataTable) ViewState["dtVesVettingSetting"];
                   DataRow[] drVetSet = dtVesVettingSetting.Select("Vessel_ID=" + e.Row.Cells[0].Text + " and Vetting_Type_ID=" + e.Row.Cells[i + 2].Text);
                   if (drVetSet.Length > 0)
                   {
                       if (i < e.Row.Controls.Count)
                       {

                           if (e.Row.Cells[i].Text.Trim() != "" && e.Row.Cells[i].Text.Trim() != "&nbsp;")
                           {

                               e.Row.Cells[i].Text = UDFLib.ConvertUserDateFormat(e.Row.Cells[i].Text); //DateTime.Parse(e.Row.Cells[i].Text).ToString(dateFormat);

                               double Days = 0;
                               Days = (UDFLib.ConvertToDate(UDFLib.ConvertUserDateFormat(e.Row.Cells[i].Text)) - DateTime.Now).TotalDays;
                               if (Days > 30 && Days < 60)
                               {
                                   e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                               }
                               if (Days > 0 && Days < 30)
                               {
                                   e.Row.Cells[i].BackColor = System.Drawing.Color.Orange;
                               }
                               if (Days <= 0)
                               {
                                   e.Row.Cells[i].BackColor = System.Drawing.Color.Red;
                               }




                           }
                           else
                           {
                               int HasVetting = objVetP.VET_Get_VettingSetting(UDFLib.ConvertToInteger(e.Row.Cells[0].Text), UDFLib.ConvertToInteger(e.Row.Cells[i + 2].Text));

                               if (HasVetting > 0)
                               {
                                   if (e.Row.Cells[i - 4].Text.Trim() == "" || e.Row.Cells[i - 4].Text.Trim() == "&nbsp;")
                                   {
                                       e.Row.Cells[i].BackColor = System.Drawing.Color.Red;
                                   }
                               }
                           }
                       }
                   }
            }
            for (int i = 7; i <= e.Row.Controls.Count; i += 7)/*this will add image into Planned Vetting Date for unplanned vetting Column */
            {
                if (i < e.Row.Controls.Count)
                {
                    System.Web.UI.WebControls.ImageButton ImgPBtn = new System.Web.UI.WebControls.ImageButton();
                   DataTable dtVesVettingSetting= (DataTable) ViewState["dtVesVettingSetting"];
                   DataRow[] drVetSet = dtVesVettingSetting.Select("Vessel_ID=" + e.Row.Cells[0].Text + " and Vetting_Type_ID=" + e.Row.Cells[i + 1].Text);
                   if (drVetSet.Length > 0)
                   {
                       if (e.Row.Cells[i].Text.Trim() != "" && e.Row.Cells[i].Text.Trim() != "&nbsp;")
                       {

                           e.Row.Cells[i].Text = UDFLib.ConvertUserDateFormat(e.Row.Cells[i].Text);  //DateTime.Parse(e.Row.Cells[i].Text).ToString(dateFormat);
                       }
                       else
                       {
                            
                           ImgPBtn.ImageUrl = "~/Images/UplannedClock.png";
                           ImgPBtn.Visible = true;
                           ImgPBtn.Height = 20;
                          
                           ImgPBtn.Attributes.Add("onclick", "document.getElementById('iFrmNewVetting').src ='../Vetting/Vetting_CreateNewVetting.aspx?&VesselID=" + e.Row.Cells[0].Text + "&Vetting_Type_ID=" + e.Row.Cells[i + 1].Text + "'; $('#dvNewVetting').prop('title', 'Add Vetting');showModal('dvNewVetting');return false;");
                           e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;    //.Attributes.Add("Style", "text-align:center;");
                           e.Row.Cells[i].Controls.Add(ImgPBtn);
                         

                       }
                       
                   }
                   e.Row.Cells[i].Attributes.Add("style", "border-right:1px solid #cacaca;");
                 
                }
            }
          

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Height = 30;
            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Split(new string[] { "~-" }, StringSplitOptions.None)[1].Replace('_', ' ');
            e.Row.Cells[2].Width = 100;
            e.Row.Cells[3].Width = 350;
            e.Row.Cells[5].Width = 200;
            e.Row.Cells[6].Width = 100;
            e.Row.Cells[7].Width = 100;
            
        }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void gvVetting_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
        ViewState["SORTBYCOLOUMN"] = e.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindVetting();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void ImgSIREPVettingDate_Click(object sender, ImageClickEventArgs e)
    {
        //objVetMerge.AddMergedColumns(new int[] { 1, 2, 3, 4, 5 }, "SIRE Vetting", "HeaderStyle-css");
        //objVetMerge.AddMergedColumns(new int[] { 6, 7, 8, 9, 10 }, "CDI Vetting", "HeaderStyle-css");
    }
    protected void ImgCDIPVettingDate_Click(object sender, ImageClickEventArgs e)
    {
        //objVetMerge.AddMergedColumns(new int[] { 1, 2, 3, 4, 5 }, "SIRE Vetting", "HeaderStyle-css");
        //objVetMerge.AddMergedColumns(new int[] { 6, 7, 8, 9, 10 }, "CDI Vetting", "HeaderStyle-css");
    }

   /// <summary>
   /// This function dynamically creates overview in format as it is to be diplayed on page for Overview tab
   /// </summary>
   /// <param name="Group_Column_Name"> Name of column(Value in which will become the group header)  </param>
   /// <param name="GroupColumnOrder">Column Name on which sorting will be done</param>
   /// <param name="PrimaryKey_Columns">Column name based on which data to be displayed and Value for this column will not be duplicated</param>
   /// <param name="HideColumns"> Column that is not to be displayed in grid</param>
   /// <param name="dtIn">Input data which is to be formatted</param>
   /// <param name="dtGroupHead"> Group Header Name</param>
   /// <returns></returns>
    public DataTable CreateOverview(string[] Group_Column_Name ,string GroupColumnOrder, string[] PrimaryKey_Columns, string[] HideColumns, DataTable dtIn,DataTable dtGroupHead)
    {
        DataTable dtOut = new DataTable();
        try
        {
          
            StringBuilder sbPKs = new StringBuilder();
            BLL_VET_Planner objBlPlan = new BLL_VET_Planner();
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataView dvPivotColumnNames = dtGroupHead.DefaultView.ToTable(true, Group_Column_Name).DefaultView;
            dvPivotColumnNames.Sort = GroupColumnOrder;

            DataTable dtPivotPrimaryKeys = dtIn.DefaultView.ToTable(true, PrimaryKey_Columns);

            foreach (DataRow drCol in dvPivotColumnNames.ToTable().Rows)
            {
                foreach (DataColumn dcol in dtIn.Columns)
                {
                    if (dcol.ColumnName != Group_Column_Name[1].ToString() )
                    {

                        if (!dtOut.Columns.Contains(drCol[0].ToString()+"~#"+drCol[1].ToString() + "~-" + dcol.ColumnName))
                        {

                            dtOut.Columns.Add(drCol[0].ToString()+"~#"+drCol[1].ToString() + "~-" + dcol.ColumnName);
                        }


                    }
                }
            }
            

            foreach (DataRow drPK in dtPivotPrimaryKeys.Rows)
            {
                DataRow drNew = dtOut.NewRow();

               
                 sbPKs.Clear();
                 foreach (string pk in PrimaryKey_Columns)
                 {
                    sbPKs.Append(pk + " = " + drPK[pk].ToString() + " and ");
                 }
                 sbPKs.Append(" 1=1  ");

                DataRow[] drcoll = dtIn.Select(sbPKs.ToString());//[0][dcol.ColumnName];
                for (int i = 0; i < drcoll.Length; i++)
                {
                    foreach (DataColumn dcol in dtIn.Columns)
                    {

                        if (dcol.ColumnName != Group_Column_Name[1].ToString())
                        {
                            drNew[drcoll[i][11].ToString()+"~#"+drcoll[i][12].ToString() + "~-" + dcol.ColumnName] = drcoll[i][dcol.ColumnName];
                           
                            
                        }

                    }


                }
             

                dtOut.Rows.Add(drNew);
            }
            for (int i = 0; i < dtOut.Rows.Count; i++)
            {
                for (int j = 0; j < dtOut.Columns.Count; j++)
                {
                    if (dtOut.Columns[j].ColumnName.Split(new string[] { "~-" }, StringSplitOptions.None)[1].ToString() == Group_Column_Name[0].ToString())
                    {
                        dtOut.Rows[i][j] = dtOut.Columns[j].ColumnName.Split(new string[]{"~#"},StringSplitOptions.None)[0].ToString();
                        // drNew[drcoll[i][12].ToString() + "#" + drcoll[i][12].ToString() + "-" + dcol.ColumnName] = drcoll[i][11].ToString();
                    }
                }
            }

            DataTable dtOutTemp = dtOut.Copy() ;
            foreach (string strColToremove in HideColumns)
            {

                foreach (DataColumn dcol in dtOutTemp.Columns)
                {
                    if (dcol.ColumnName.Contains(strColToremove))
                        dtOut.Columns.Remove(dcol.ColumnName.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
        return dtOut;
     
    }

    
    
    /// <summary>
    /// This function dynamically creates Calendar view  in format as it is to be diplayed on page for Calendar view tab
    /// </summary>
    /// <param name="Group_Column_Name">Name of column(Value in which will become the group header)</param>
    /// <param name="ValueColumn">Column name based on which data to be displayed in grid </param>
    /// <param name="PrimaryKey_Columns">Column name based on which data to be displayed and Value for this column will not be duplicated</param>
    /// <param name="HideColumns">Column that is not to be displayed in grid</param>
    /// <param name="CenterDate"> First date of center month selected in Month Dropdown(If not selected then current month is consider as center month)</param>
    /// <param name="dtIn">Input data which is to be formatted</param>
    /// <returns></returns>
    public DataTable CreateCalenderview(string Group_Column_Name, string ValueColumn, string[] PrimaryKey_Columns, string[] HideColumns, DateTime CenterDate, DataTable dtIn)
    {
        DataTable dtOut = new DataTable();
        try
        {

            StringBuilder sbPKs = new StringBuilder();
            BLL_VET_Planner objBlPlan = new BLL_VET_Planner();
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
          

            DataTable dtPivotPrimaryKeys = dtIn.DefaultView.ToTable(true, PrimaryKey_Columns);
            DataTable dtExpDate = (DataTable)ViewState["dtVetExpDate"];
            var first = CenterDate.AddMonths(-6);  
            var Last = CenterDate.AddMonths(6);
           
            foreach (DataColumn dcol in dtPivotPrimaryKeys.Columns)
            {
                dtOut.Columns.Add(dcol.ColumnName);

            }

            for (int i = 0; i < 12; i++)
            {
                dtOut.Columns.Add(first.AddMonths(i).ToString("MMM") + "-" + first.AddMonths(i).Year.ToString());
            }
          
            foreach (DataRow drPK in dtPivotPrimaryKeys.Rows)
            {
                DataRow drNew = dtOut.NewRow();


                sbPKs.Clear();
                foreach (string pk in PrimaryKey_Columns)
                {
                    sbPKs.Append(pk + " = '" + drPK[pk].ToString() + "' and ");
                }
                sbPKs.Append(" 1=1  ");

                DataRow[] drcoll = dtIn.Select(sbPKs.ToString());//[0][dcol.ColumnName];
              
                for (int i = 0; i < drcoll.Length; i++)
                {
                    foreach (DataColumn dcol in dtIn.Columns)
                    {
                                            
                        {
                            if (dcol.ColumnName == ValueColumn)
                            {
                                if (drcoll[i][ValueColumn].ToString() != "")
                                {
                                    var Month = UDFLib.ConvertToDate(UDFLib.ConvertUserDateFormat(drcoll[i][ValueColumn].ToString())).ToString("MMM");
                                    var Year = UDFLib.ConvertToDate(UDFLib.ConvertUserDateFormat(drcoll[i][ValueColumn].ToString())).Year.ToString();
                                    if (dtOut.Columns.Contains(Month + "-" + Year))
                                    {
                                        if (drNew[Month + "-" + Year].ToString().Trim() == "")
                                        {
                                            drNew[Month + "-" + Year] = drcoll[i]["Vetting_ID"] + "~_" + drcoll[i]["Expiry_Date"] + "~_" + drcoll[i]["Status"] + "~_" + drcoll[i]["Vetting_Date"] + "~_" + drcoll[i]["Oil_Major_Name"] + "~_" + drcoll[i]["Inspector_Name"] + "~_" + drcoll[i]["PORT_NAME"] + "~#";
                                        }
                                        else
                                        {
                                            drNew[Month + "-" + Year] = drNew[Month + "-" + Year].ToString() + "~#" + drcoll[i]["Vetting_ID"] + "~_" + drcoll[i]["Expiry_Date"] + "~_" + drcoll[i]["Status"] + "~_" + drcoll[i]["Vetting_Date"] + "~_" + drcoll[i]["Oil_Major_Name"] + "~_" + drcoll[i]["Inspector_Name"] + "~_" + drcoll[i]["PORT_NAME"];
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (dtOut.Columns.Contains(dcol.ColumnName))
                                {
                                    drNew[dcol.ColumnName] = drcoll[i][dcol.ColumnName];
                                }
                            }
                        }

                    }
                    
                }


                DataRow[] drecoll = dtExpDate.Select(sbPKs.ToString());//[0][dcol.ColumnName];
                for (int i = 0; i < drecoll.Length; i++)
                {
                    foreach (DataColumn dcol in dtExpDate.Columns)
                    {

                        {
                            if (dcol.ColumnName == "Expiry_Date")
                            {
                                if (drecoll[i]["Expiry_Date"].ToString() != "")
                                {
                                    var Month = UDFLib.ConvertToDate(UDFLib.ConvertUserDateFormat(drecoll[i]["Expiry_Date"].ToString())).ToString("MMM");
                                    var Year = UDFLib.ConvertToDate(UDFLib.ConvertUserDateFormat(drecoll[i]["Expiry_Date"].ToString())).Year.ToString();
                                    if (dtOut.Columns.Contains(Month + "-" + Year))
                                    {
                                        if (drNew[Month + "-" + Year].ToString().Trim() == "")
                                        {
                                            drNew[Month + "-" + Year] = drecoll[i]["Vetting_ID"] + "~_" + drecoll[i]["Expiry_Date"] + "~_Expires~_" + drecoll[i]["Expiry_Date"] + "~_~_~_~#";
                                        }
                                        else
                                        {
                                            drNew[Month + "-" + Year] = drNew[Month + "-" + Year].ToString() + "~#" + drecoll[i]["Vetting_ID"] + "~_" + drecoll[i]["Expiry_Date"] + "~_Expires~_" + drecoll[i]["Expiry_Date"] + "~_~_~_";
                                        }
                                    }
                                }
                            }
                            //else
                            //{
                            //    if (dtOut.Columns.Contains(dcol.ColumnName))
                            //    {
                            //        drNew[dcol.ColumnName] = drcoll[i][dcol.ColumnName];
                            //    }
                            //}
                        }

                    }

                }
                dtOut.Rows.Add(drNew);
            }
             
        
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
        return dtOut;

    }


    protected void gvVetting_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objVetMerge);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);

        }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void gvVetting_DataBound(object sender, EventArgs e)
    {
        try
        {
        int igr = 0, tmp = 0;
        foreach (TableCell cl in gvVetting.HeaderRow.Cells)
        {

            if (igr > 1 && igr < gvVetting.HeaderRow.Cells.Count - 1)
            {
              
                if (igr == 2)
                {
                    string headername = "";


                    headername = cl.Text.Split(new string[] { "~-" }, StringSplitOptions.None)[0].Split(new string[] { "~#" }, StringSplitOptions.None)[1].ToString();

                    objVetMerge.AddMergedColumns(new int[] { igr, igr + 1, igr + 3, igr + 4, igr + 5 }, headername + " Vetting", "GroupHeaderStyle-css HeaderStyle-css");
                    //tmp = 0;
                }
                else if ((tmp % 7) == 0)
                {
                    string headername = "";

                    headername = cl.Text.Split(new string[] { "~-" }, StringSplitOptions.None)[0].Split(new string[] { "~#" }, StringSplitOptions.None)[1].ToString();

                    objVetMerge.AddMergedColumns(new int[] { igr, igr + 1, igr + 3, igr + 4, igr + 5 }, headername + " Vetting", "GroupHeaderStyle-css HeaderStyle-css");
                    tmp = 0;
                }
                cl.Text = cl.Text.Split(new string[] { "~-" }, StringSplitOptions.None)[1].Replace('_', ' ');
                tmp++;
            }
            igr++;

        }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void btnCRetrieve_Click(object sender, ImageClickEventArgs e)
    {
       try
       {
        DateTime dtCurrentDate = (DateTime)ViewState["CurrentDate"];
        DDLMonth.SelectedValue = DDLMonth.SelectedValue;
        int CurrentMonth = DDLMonth.SelectedValue == "0" || DDLMonth.SelectedValue == "" ? dtCurrentDate.Month : UDFLib.ConvertToInteger(DDLMonth.SelectedValue);
        DateTime dtCenterMonth = new DateTime(dtCurrentDate.Year, CurrentMonth, 1);
        BindCalVetting(dtCenterMonth);
       
        if (hfCAdv.Value == "o")
        {
            String tglcadvsearch = String.Format("toggleCOnSearchClearFilter(advCText,'" + hfCAdv.Value + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tglcadvsearch", tglcadvsearch, true);
        }
        else
        {
            String tglcadvsearch1 = String.Format("toggleCOnSearchClearFilter(advCText,'" + hfCAdv.Value + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tglcadvsearch1", tglcadvsearch1, true);
        }
       }
       catch (Exception ex)
       {
           UDFLib.WriteExceptionLog(ex);
       }
    }
    protected void btnCClearFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        DDLCFleet.SelectedValue = "0";
        
        DDLCVessel.ClearSelection();
        DDLCExInDays.SelectedValue = "0";
        DDLCOilMajor.ClearSelection();
        DDLCVettingType.ClearSelection();
        DDLCInspector.ClearSelection();
      //  rbtVetState.Items[0].Selected = true;
        rbtCVetState.SelectedValue = "0";
        ViewState["CurrentDate"] = DateTime.Now;
        chkCVesselAssign.Checked = true;
        BindCVesselDDL();
        CheckedAssignVessel(DDLCVessel);
       
        txtCEVetFromDate.Text = "";
        txtCEVetToDate.Text = "";
        txtCLVetFromDate.Text = "";
        txtCLVetToDate.Text = "";
        txtCPVetFromDate.Text = "";
        txtCPVetToDate.Text = "";
        
        updCAdvFilter.Update();
        updCPnlGrid.Update();
        BindCalVetting(DateTime.Now);
        if (hfCAdv.Value == "o")
        {
            String tgladvsearchClr = String.Format("toggleCOnSearchClearFilter(advCText,'" + hfCAdv.Value + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tglcadvsearchClr", tgladvsearchClr, true);
        }
        else
        {
            String tgladvsearchClr1 = String.Format("toggleCOnSearchClearFilter(advCText,'" + hfCAdv.Value + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tglcadvsearchClr1", tgladvsearchClr1, true);
        }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnCExport_Click(object sender, ImageClickEventArgs e)
    {
       try
       {
        DateTime dtCurrentDate = (DateTime)ViewState["CurrentDate"];
        DDLMonth.SelectedValue = DDLMonth.SelectedValue;
        int CurrentMonth = DDLMonth.SelectedValue == "0" || DDLMonth.SelectedValue == "" ? dtCurrentDate.Month : UDFLib.ConvertToInteger(DDLMonth.SelectedValue);
        DateTime dtCenterMonth = new DateTime(dtCurrentDate.Year, CurrentMonth, 1);
        BindCalVetting(dtCenterMonth);
      

        ExportGridviewToExcel(gvCalVetting,"Calendar View");
       }
       catch (Exception ex)
       {
           UDFLib.WriteExceptionLog(ex);
       }
    }
    protected void gvCalVetting_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvCalVetting_DataBound(object sender, EventArgs e)
    {
        try
        {
        for (int rowIndex = gvCalVetting.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow gvRow = gvCalVetting.Rows[rowIndex];
            GridViewRow gvPreviousRow = gvCalVetting.Rows[rowIndex + 1];
           
                if (gvRow.Cells[1].Text == gvPreviousRow.Cells[1].Text)
                {
                    if (gvPreviousRow.Cells[1].RowSpan < 2)
                    {
                        gvRow.Cells[1].RowSpan = 2;
                    }
                    else
                    {
                        gvRow.Cells[1].RowSpan = gvPreviousRow.Cells[1].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[1].Visible = false;
                   
                    
                }
           // }
        }

        gvCalVetting.HeaderRow.Cells[1].Text = "Vessel Name";
        gvCalVetting.HeaderRow.Cells[3].Text = "Expiration Date";

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void gvCalVetting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            BLL_VET_Planner objBlPlan = new BLL_VET_Planner();

            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Height = 30;



            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Height = 30;
                string dateFormat = UDFLib.GetDateFormat();
                DataTable dtExpDate = (DataTable)ViewState["dtVetExpDate"];
                string filterexpression = " Vessel_ID=" + UDFLib.ConvertToInteger(e.Row.Cells[0].Text) + " and Vetting_Type_ID=" + UDFLib.ConvertToInteger(e.Row.Cells[2].Text);
                DataRow[] drExpDate = dtExpDate.Select(filterexpression);
                string ExpDate=string.Empty;
                if (drExpDate.Length > 0)
                {
                    ExpDate = drExpDate[0][2].ToString();
                }

                e.Row.Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                e.Row.Cells[1].BorderColor = System.Drawing.Color.LightGray;
                e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                e.Row.Cells[3].BorderColor = System.Drawing.Color.LightGray;
               
                if (ExpDate.Trim() != "")
                {
                    e.Row.Cells[3].Text += ": " + UDFLib.ConvertUserDateFormat(ExpDate); //DateTime.Parse(ExpDate).ToString(dateFormat);
                   
                    double Days = 0;

                    Days = (UDFLib.ConvertToDate(UDFLib.ConvertUserDateFormat(ExpDate)) - DateTime.Now).TotalDays;

                    if (Days > 30 && Days < 60)
                    {
                        e.Row.Cells[3].BackColor = System.Drawing.Color.Yellow;
                    }
                    if (Days > 0 && Days < 30)
                    {
                        e.Row.Cells[3].BackColor = System.Drawing.Color.Orange;
                    }
                    if (Days <= 0)
                    {
                        e.Row.Cells[3].BackColor = System.Drawing.Color.Red;

                    }
                    
                   
                      
                   

                }

                for (int i = 4; i < e.Row.Controls.Count; i++)
                {
                    Table table = new Table();
                    table.Attributes.Add("style", "border-collapse: separate;border-spacing: 1px; border-collapse: expression('separate', cellSpacing = '1px');");
                    table.Attributes.Add("align", "center");
                    if (e.Row.Cells[i].Text.Contains("~#"))
                    {
                        string[] HasVetting = e.Row.Cells[i].Text.Split(new string[] { "~#" }, StringSplitOptions.None);
                        if (HasVetting.Length > 0)
                        {

                           
                            TableRow tr = new TableRow();
                            int tdcount = 0;
                            for (int j = 0; j < HasVetting.Length; j++)
                            {
                                if (tdcount > 3)
                                {
                                    tr = new TableRow();
                                    tdcount = 0;
                                }
                                TableCell td = new TableCell();
                                td.Width = 20;
                                td.Height = 20;
                                td.HorizontalAlign = HorizontalAlign.Center;

                               
                                {
                                    if (HasVetting[j].Contains("~_"))
                                    {
                                        string[] HasVetDetails = HasVetting[j].Split(new string[] { "~_" }, StringSplitOptions.None);


                                        if (HasVetDetails.Length > 0)
                                        {
                                            DataSet dsVetDetail = new DataSet();

                                            string ToolTip;
                                          
                                            if (HasVetDetails[2].ToString() == "Completed")
                                            {
                                              
                                                    td.BackColor = System.Drawing.Color.Green;
                                                    td.BorderColor = System.Drawing.Color.White;
                                                    td.BorderWidth = 2;
                                               
                                            }
                                            else if (HasVetDetails[2].ToString() == "Planned")
                                            {
                                                
                                                    td.BackColor = System.Drawing.Color.Blue;
                                                    td.BorderColor = System.Drawing.Color.White;
                                                    td.BorderWidth = 2;
                                                
                                            }
                                            else if (HasVetDetails[2].ToString() == "In-Progress")
                                            {
                                                td.BackColor = System.Drawing.Color.Green;
                                                td.BorderColor = System.Drawing.Color.Yellow;
                                                td.BorderWidth = 2;
                                            }
                                            else if (HasVetDetails[2].ToString() == "Failed")
                                            {
                                                td.BackColor = System.Drawing.Color.Green;
                                                td.BorderColor = System.Drawing.Color.Red;
                                                td.BorderWidth = 2;
                                            }
                                            else if (HasVetDetails[2].ToString() == "Expires")
                                            {
                                                System.Web.UI.WebControls.Image Imgtemps = new System.Web.UI.WebControls.Image();
                                                Imgtemps.ImageUrl = "../../Images/exflag.png";
                                                Imgtemps.Visible = true;
                                                Imgtemps.Height = 15;
                                                td.Controls.Add(Imgtemps);

                                                double ExDays = 0;

                                                ExDays = (UDFLib.ConvertToDate(UDFLib.ConvertUserDateFormat(HasVetDetails[1].ToString())) - DateTime.Now).TotalDays;

                                                if (ExDays > 30 && ExDays < 60)
                                                {
                                                    td.BackColor = System.Drawing.Color.Yellow;
                                                }
                                                if (ExDays > 0 && ExDays < 30)
                                                {
                                                    td.BackColor = System.Drawing.Color.Orange;
                                                }
                                                if (ExDays <= 0)
                                                {
                                                    td.BackColor = System.Drawing.Color.Red;
                                                }
                                              
                                           
                                                                                         
                                                td.BorderColor = System.Drawing.Color.Transparent;
                                                td.BorderWidth = 2;
                                            }
                                               
                                            td.Attributes.Add("style", " padding: 1px; ");

                                            string NotesCount, ObsCount, PendCount, ComCount;

                                            DataTable dtObsNotesCount = (DataTable)ViewState["dtObsNotesCount"];
                                            string filternoteexpression = " Observation_Type_ID=1 and Vetting_ID=" + UDFLib.ConvertToInteger(HasVetDetails[0].ToString());
                                            DataRow[] drNotesCount = dtObsNotesCount.Select(filternoteexpression);
                                            if (drNotesCount.Length <= 0)
                                            {
                                                NotesCount = "0";
                                            }
                                            else
                                            {
                                                NotesCount = drNotesCount[0][0].ToString();
                                            }
                                            string filterobsexpression = " Observation_Type_ID=2 and Vetting_ID=" + UDFLib.ConvertToInteger(HasVetDetails[0].ToString());
                                            DataRow[] drObsCount = dtObsNotesCount.Select(filterobsexpression);
                                            if (drObsCount.Length <= 0)
                                            {
                                                ObsCount = "0";
                                            }
                                            else
                                            {
                                                ObsCount = drObsCount[0][0].ToString();
                                            }

                                            DataTable dtJobsCount = (DataTable)ViewState["dtJobsCount"];
                                            string filterpenexpression = " Vetting_ID=" + UDFLib.ConvertToInteger(HasVetDetails[0].ToString()) + " and Job_Status='PENDING'";
                                            DataRow[] drPenJobsCount = dtJobsCount.Select(filterpenexpression);
                                            if (drPenJobsCount.Length <= 0)
                                            {
                                                PendCount = "0";
                                            }
                                            else
                                            {
                                                PendCount = drPenJobsCount[0][0].ToString();
                                            }


                                            string filtercomexpression = " Vetting_ID=" + UDFLib.ConvertToInteger(HasVetDetails[0].ToString()) + " and Job_Status='COMPLETED'";
                                            DataRow[] drComJobsCount = dtJobsCount.Select(filtercomexpression);
                                            if (drComJobsCount.Length <= 0)
                                            {
                                                ComCount = "0";
                                            }
                                            else
                                            {
                                                ComCount = drComJobsCount[0][0].ToString();
                                            }

                                          
                                            {
                                                if (HasVetDetails[2].ToString() != "Planned" && HasVetDetails[2].ToString() != "Expires")
                                                {
                                                    ToolTip = "Vessel:<b>" + e.Row.Cells[1].Text + "</b><br>" + "Vetting:<b>" + e.Row.Cells[3].Text.Split(':')[0] + "</b><br>" + "Status:<b>" + HasVetDetails[2].ToString() + "</b><br>" + "Date:<b>" + UDFLib.ConvertUserDateFormat(HasVetDetails[3].ToString()) + "</b><br>" + "Oil Major:<b>" + HasVetDetails[4].ToString() + "</b><br>" + "Inspector:<b>" + HasVetDetails[5].ToString() + "</b><br><br>" + "No. Of Observations:<b>" + ObsCount + "</b><br>" + "No. Of Notes:<b>" + NotesCount + "</b><br><br>" + "Pending Jobs:<b>" + PendCount + "</b><br>" + "Completed Jobs:<b>" + ComCount + "<b>";
                                                    td.Attributes.Add("style", "cursor:pointer");
                                                    td.Attributes.Add("onmouseover", "js_ShowToolTip('" + ToolTip + "',event,this)");
                                                    td.Attributes.Add("onclick", "window.open('Vetting_Details.aspx?Vetting_ID=" + HasVetDetails[0].ToString() + "');");
                                                }
                                                else if (HasVetDetails[2].ToString() == "Planned")
                                                {
                                                    ToolTip = "Vessel:<b>" + e.Row.Cells[1].Text + "</b><br>" + "Vetting:<b>" + e.Row.Cells[3].Text.Split(':')[0] + "</b><br>" + "Status:<b>" + HasVetDetails[2].ToString() + "</b><br>" + "Planned Date:<b>" + UDFLib.ConvertUserDateFormat(HasVetDetails[3].ToString()) + "</b><br>" + "Planned Port:<b>" + HasVetDetails[6].ToString() + "</b><br>" + "Oil Major:<b>" + HasVetDetails[4].ToString() + "</b><br>" + "Inspector:<b>" + HasVetDetails[5].ToString() + "</b>";
                                                    td.Attributes.Add("style", "cursor:pointer");
                                                    td.Attributes.Add("onmouseover", "js_ShowToolTip('" + ToolTip + "',event,this)");
                                                    td.Attributes.Add("onclick", "window.open('Vetting_Details.aspx?Vetting_ID=" + HasVetDetails[0].ToString() + "');");
                                                }
                                                else if (HasVetDetails[2].ToString() == "Expires")
                                                {
                                                    ToolTip = "Vessel:<b>" + e.Row.Cells[1].Text + "</b><br>" + "Vetting:<b>" + e.Row.Cells[3].Text.Split(':')[0] + "</b><br>" + "Expiry Date:<b>" + UDFLib.ConvertUserDateFormat(HasVetDetails[1].ToString()) + "</b><br>" + "Status:<b>" + HasVetDetails[2].ToString();
                                                    td.Attributes.Add("style", "cursor:pointer");
                                                    td.Attributes.Add("onmouseover", "js_ShowToolTip('" + ToolTip + "',event,this)");
                                                    td.Attributes.Add("onclick", "window.open('Vetting_Details.aspx?Vetting_ID=" + HasVetDetails[0].ToString() + "');");
                                                }
                                            }
                                            tr.Controls.Add(td);

                                        }

                                    }
                                }
                                tdcount++;
                                table.Controls.Add(tr);
                            }


                        }


                    }



                    e.Row.Cells[i].Controls.Add(table);
                }

              
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void DDLCFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCVesselDDL();
    }
    protected void gvCalVetting_Sorting(object sender, GridViewSortEventArgs e)
    {
       try
       {
       
        ViewState["SORTBYCOLOUMN"] = e.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;
      
        DateTime dtCurrentDate = (DateTime)ViewState["CurrentDate"];
        DDLMonth.SelectedValue = DDLMonth.SelectedValue;
        int CurrentMonth = DDLMonth.SelectedValue == "0" || DDLMonth.SelectedValue == "" ? dtCurrentDate.Month : UDFLib.ConvertToInteger(DDLMonth.SelectedValue);
        DateTime dtCenterMonth = new DateTime(dtCurrentDate.Year, CurrentMonth, 1);
        BindCalVetting(dtCenterMonth);
       }
       catch (Exception ex)
       {
           UDFLib.WriteExceptionLog(ex);
       }
       
    }
    protected void DDLMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {     
        DateTime dtCurrentDate = (DateTime)ViewState["CurrentDate"];
        int CurrentMonth = DDLMonth.SelectedValue == "0" || DDLMonth.SelectedValue == "" ? dtCurrentDate.Month : UDFLib.ConvertToInteger(DDLMonth.SelectedValue);
        DateTime dtCenterMonth = new DateTime(dtCurrentDate.Year, CurrentMonth, 1);
        BindCalVetting(dtCenterMonth);     
        ViewState["CurrentDate"] = dtCenterMonth;
        if (hfCAdv.Value == "o")
        {
            String tglcddlmonadvsearch = String.Format("toggleCOnSearchClearFilter(advCText,'" + hfCAdv.Value + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tglcddlmonadvsearch", tglcddlmonadvsearch, true);
        }
        else
        {
            String tglcddlmonadvsearch1 = String.Format("toggleCOnSearchClearFilter(advCText,'" + hfCAdv.Value + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tglcddlmonadvsearch1", tglcddlmonadvsearch1, true);
        }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void ImgPrev_Click(object sender, ImageClickEventArgs e)
    {
       try
       {
        DateTime dtCurrentDate = (DateTime)ViewState["CurrentDate"];
   
        int DDLSelectedValue;
        if (UDFLib.ConvertToInteger(DDLMonth.SelectedValue)-1 < 1)
        {
            DDLSelectedValue = 12;
        }
        else
        {
            DDLSelectedValue = UDFLib.ConvertToInteger(DDLMonth.SelectedValue)-1;
        }
        int CurrentMonth = DDLMonth.SelectedValue == "0" || DDLMonth.SelectedValue == "" ? dtCurrentDate.AddMonths(-1).Month : DDLSelectedValue;
        DateTime dtCenterMonth = new DateTime(dtCurrentDate.AddMonths(-1).Year, CurrentMonth, 1);
        DDLMonth.SelectedValue = Convert.ToString(dtCurrentDate.AddMonths(-1).Month);
        BindCalVetting(dtCenterMonth);
       
        ViewState["CurrentDate"] = dtCenterMonth;


        if (hfCAdv.Value == "o")
        {
            String tglcimgprevadvsearch = String.Format("toggleCOnSearchClearFilter(advCText,'" + hfCAdv.Value + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tglcimgprevadvsearch", tglcimgprevadvsearch, true);
        }
        else
        {
            String tglcimgprevadvsearch1 = String.Format("toggleCOnSearchClearFilter(advCText,'" + hfCAdv.Value + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tglcimgprevadvsearch1", tglcimgprevadvsearch1, true);
        }
       }
       catch (Exception ex)
       {
           UDFLib.WriteExceptionLog(ex);
       }
    }
    protected void ImgNext_Click(object sender, ImageClickEventArgs e)
    {
      try
      {
        DateTime dtCurrentDate = (DateTime)ViewState["CurrentDate"];
       
        int DDLSelectedValue;
        if(UDFLib.ConvertToInteger(DDLMonth.SelectedValue)+1>12)
        {
            DDLSelectedValue=1;
        }
        else
        {
            DDLSelectedValue=UDFLib.ConvertToInteger(DDLMonth.SelectedValue)+1;
        }

        int CurrentMonth = DDLMonth.SelectedValue == "0" || DDLMonth.SelectedValue == "" ? dtCurrentDate.AddMonths(1).Month : DDLSelectedValue;
        DateTime dtCenterMonth = new DateTime(dtCurrentDate.AddMonths(1).Year, CurrentMonth, 1);
        DDLMonth.SelectedValue = Convert.ToString(CurrentMonth);
        BindCalVetting(dtCenterMonth);
       
        ViewState["CurrentDate"] = dtCenterMonth;


        if (hfCAdv.Value == "o")
        {
            String tglcimgnxtadvsearch = String.Format("toggleCOnSearchClearFilter(advCText,'" + hfCAdv.Value + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tglcimgnxtadvsearch", tglcimgnxtadvsearch, true);
        }
        else
        {
            String tglcimgnxtadvsearch1 = String.Format("toggleCOnSearchClearFilter(advCText,'" + hfCAdv.Value + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tglcimgnxtadvsearch1", tglcimgnxtadvsearch1, true);
        }
      }
      catch (Exception ex)
      {
          UDFLib.WriteExceptionLog(ex);
      }
    }

    protected void chkOVesselAssign_CheckedChanged(object sender, EventArgs e)
    {
        if (chkOVesselAssign.Checked == false)
        {
            DDLVessel.ClearSelection();
           
        }
        else
        {
            CheckedAssignVessel(DDLVessel);
           
        }
        UpdPnlFilter.Update();
    }
    protected void chkCVesselAssign_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCVesselAssign.Checked == false)
        {
            DDLCVessel.ClearSelection();
           
        }
        else
        {
            CheckedAssignVessel(DDLCVessel);
           
        }
        UpdCPnlFilter.Update();
    }
}