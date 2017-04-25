using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Web.UI.HtmlControls;
using SMS.Business.PMS;
using SMS.Properties;
using SMS.Business.Crew;


public partial class PMSJobChangeRequest : System.Web.UI.Page
{


    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();

    public UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserAccessValidation();

            if (!IsPostBack)
            {


                BindFleetDLL();
                DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
                BindVesselDDL();
                BindSystem();
                BindSubSystem();

                BindPMSDepartmentDDL();
                BindRankDDL();

                ViewState["Vessel_id"] = null;
                ViewState["System_ID"] = null;
                ViewState["IsNewFlage"] = null;

                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                ViewState["Status"] = null;

                optStatus.SelectedValue = "P";

                // ucCustomPagerItems.PageSize = 30;

                BindGrid();


            }
            objChangeReqstMerge.AddMergedColumns(new int[] { 2, 3, 4 }, "Original Job Deatils", "HeaderStyle-css HeadetTHStyle");
            objChangeReqstMerge.AddMergedColumns(new int[] { 5, 6, 7, 8, 9, 10, 11 }, "Change Request", "HeaderStyle-css HeadetTHStyle");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// User Access
    /// </summary>
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

        }
    }

    /// <summary>
    /// Binding fleet
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
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Binding Vessel
    /// </summary>
    /// <param name="FleetID"> Fleet Code</param>

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
            throw ex;
        }
    }

    /// <summary>
    /// Binding System
    /// </summary>
    public void BindSystem()
    {
        try
        {
            string filter;
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtCatalog = objTechService.SelectCatalog();


                string vsl = DDLVessel.SelectedValue;
                filter = "1=1";
                if (vsl != "0")
                {
                    filter += "  and  Vessel_Code=" + vsl + " and Form_Type = 'SP' ";
                    dtCatalog.DefaultView.RowFilter = filter;

                    if (dtCatalog.DefaultView.Count > 0)
                    {
                        ddlSystem.Items.Clear();
                        ddlSystem.DataTextField = "system_description";
                        ddlSystem.DataValueField = "system_code";
                        ddlSystem.DataSource = dtCatalog.DefaultView.ToTable();
                        ddlSystem.DataBind();
                        ListItem li = new ListItem("--SELECT ALL--", "0");
                        ddlSystem.Items.Insert(0, li);
                    }
                }



            }
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    /// <summary>
    /// Binding SubSytem
    /// </summary>
    public void BindSubSystem()
    {
        try
        {

            string filter = "1=1";

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtSubSystem = new DataTable();
                string CatalogId = ddlSystem.SelectedValue;
                dtSubSystem = objTechService.SelectSubCatalogs();

                filter += " and (System_Code ='" + CatalogId + "' or SubSystem_code='0') and ID <>0";


                //dtSubSystem.DefaultView.RowFilter = "(System_Code ='" + CatalogId + "' or SubSystem_code='0') and ID <>0";

                dtSubSystem.DefaultView.RowFilter = filter;

                ddlSubSystem.Items.Clear();
                ddlSubSystem.DataTextField = "subsystem_description";
                ddlSubSystem.DataValueField = "ID";
                ddlSubSystem.DataSource = dtSubSystem.DefaultView;
                ddlSubSystem.DataBind();
                ListItem li = new ListItem("--SELECT ALL--", "0");
                ddlSubSystem.Items.Insert(0, li);

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    /// <summary>
    /// Binding Department using parent type code
    /// </summary>
    private void BindPMSDepartmentDDL()
    {
        try
        {
            BLL_PMS_Library_Jobs obj = new BLL_PMS_Library_Jobs();

            DataTable dt = obj.LibraryGetPMSSystemParameterList("2487", "");

            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "Code";
            ddlDepartment.DataTextField = "Name";

            ddlDepartment.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    private void BindRankDDL()
    {
        try
        {
            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
            DataTable dtRank = new DataTable();
            dtRank = objCrewAdmin.Get_RankList();

            ddlRank.DataTextField = "Rank_Short_Name";
            ddlRank.DataValueField = "ID";
            ddlRank.DataSource = dtRank;
            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("--SELECT ALL--", "0"));

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    /// <summary>
    /// Binding change request detail 
    /// </summary>
    /// <param name="Fleet_ID"> fleet id</param>
    /// <param name="Vessel_ID">vessel</param>
    /// <param name="SearchText">search keyword</param>
    /// <param name="Status">Active status</param>
    /// <param name="SYSTEMID">System</param>
    /// <param name="SUBSYSTEMID">subsystem</param>
    /// <param name="DEPARTMENTID">department</param>
    /// <param name="RANKID">rank</param>
    public void BindGrid()
    {
        try
        {
            BLL_PMS_Change_Request objChangeRqst = new BLL_PMS_Change_Request();

            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString()); int? sortdirection = null;
            if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            int? status = null;
            if (optStatus.SelectedValue == "P")
                status = 2;
            else if (optStatus.SelectedValue == "A")
                status = 1;
            else if (optStatus.SelectedValue == "R")
                status = 0;



            DataSet ds = objChangeRqst.TecJobChangeRequestSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), txtSearch.Text.Trim() != "" ? txtSearch.Text : null, status,
                  UDFLib.ConvertIntegerToNull(ddlSystem.SelectedValue), UDFLib.ConvertIntegerToNull(ddlSubSystem.SelectedValue), UDFLib.ConvertIntegerToNull(ddlDepartment.SelectedValue)
                  , UDFLib.ConvertIntegerToNull(ddlRank.SelectedValue), null, null, UDFLib.ConvertDateToNull(txtFromDate.Text.Trim()), UDFLib.ConvertDateToNull(txtToDate.Text.Trim())
                  , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvJobChangeRqst.DataSource = ds.Tables[0];
                    gvJobChangeRqst.DataBind();
                }

                else
                {
                    gvJobChangeRqst.DataSource = ds.Tables[0];
                    gvJobChangeRqst.DataBind();
                }
            }
            UpdPnlGrid.Update();
            UpdPnlFilter.Update();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindVesselDDL();

    }

    protected void gvJobChangeRqst_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);

            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);


        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ImgApprove = (ImageButton)e.Row.FindControl("ImgApprove");
            if (ImgApprove != null) ImgApprove.Attributes.Add("onclick", "javascript:OpenRequestDetails(" + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + ", " + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + " , '" + DataBinder.Eval(e.Row.DataItem, "Status").ToString() + "'); return false;");
        }


    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            BLL_PMS_Change_Request objChangeRqst = new BLL_PMS_Change_Request();

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString()); int? sortdirection = null;
            if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            int? status = null;
            if (optStatus.SelectedValue == "P")
                status = 2;
            else if (optStatus.SelectedValue == "A")
                status = 1;
            else if (optStatus.SelectedValue == "R")
                status = 0;


            DataSet ds = objChangeRqst.TecJobChangeRequestSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), txtSearch.Text.Trim() != "" ? txtSearch.Text : null, status,
                  UDFLib.ConvertIntegerToNull(ddlSystem.SelectedValue), UDFLib.ConvertIntegerToNull(ddlSubSystem.SelectedValue), UDFLib.ConvertIntegerToNull(ddlDepartment.SelectedValue)
                  , UDFLib.ConvertIntegerToNull(ddlRank.SelectedValue), null, null, UDFLib.ConvertDateToNull(txtFromDate.Text.Trim()), UDFLib.ConvertDateToNull(txtToDate.Text.Trim())
                  , sortbycoloumn, sortdirection, null, null, ref  rowcount);

            string[] HeaderCaptions = { "ID", "Vessel", "System", "Sub System", "Job Title", "Freq.", "Freq.Name", "Department", "Rank", "CMS", "CRITICAL", "CR Job Title", "CR Freq.", "CR Freq.Name", "CR Department", "CR Rank", "CR CMS", "CR CRITICAL", "Status", "Actioned By", "Actioned On" };
            string[] DataColumnsName = { "ID", "Vessel_Name", "System_Description", "Subsystem_Description", "JOB_TITLE", "FREQUENCY", "Frequency_Name", "Department", "RankName", "CMS", "CRITICAL", "CR_JOB_TITLE", "CR_FREQUENCY", "CR_Frequency_Name", "CR_Department", "CR_RankName", "CR_CMS", "CR_CRITICAL", "STATUS", "ACTIONEDBY", "CR_Actioned_On" };

            GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "JOBChangeRequest", "JOB Change Request");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }




    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            ucCustomPagerItems.isCountRecord = 1;

            string vesselcode = DDLVessel.SelectedValue.ToString();


            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void gvJobChangeRqst_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;

            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }


    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearch.Text = "";
            ViewState["Status"] = null;
            optStatus.SelectedValue = "P";
            txtFromDate.Text = "";
            txtToDate.Text = "";

            BindVesselDDL();

            DDLVessel.SelectedValue = "0";
            DDLFleet.SelectedValue = "0";

            BindSystem();
            BindSubSystem();

            ddlSystem.SelectedValue = "0";
            ddlSubSystem.SelectedValue = "0";

            ddlDepartment.SelectedValue = "0";
            ddlRank.SelectedValue = "0";


            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }




    protected void ddlSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubSystem();

    }
    protected void ddlSubSystem_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSystem();
    }
}