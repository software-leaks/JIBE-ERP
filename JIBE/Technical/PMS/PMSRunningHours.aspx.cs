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

public partial class PMSRunningHours : System.Web.UI.Page
{

    BLL_PURC_Purchase objBLLPURC = new BLL_PURC_Purchase();
    BLL_PMS_Job_Status objJobStatus = new BLL_PMS_Job_Status();

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                ViewState["RecordDisplayType"] = "C";  //Current Running Hours 
                BindFleetDLL();
                DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
                BindVesselDDL();


                if (!string.IsNullOrWhiteSpace(Request.QueryString["Systemid"]))
                {

                    ViewState["LocationID"] = Request.QueryString["Systemid"].ToString();
                    ViewState["FleetID"] = Request.QueryString["FleetID"].ToString();
                    DDLFleet.SelectedValue = "0";
                    ViewState["VesselID"] = Request.QueryString["VesselID"].ToString();


                }
                BindVesselDDL();
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["VESSEL_ID"])))
                {
                    DDLVessel.SelectedValue = Request.QueryString["VESSEL_ID"].ToString();
                }
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Fleet_ID"])))
                {
                    DDLFleet.SelectedValue = Request.QueryString["Fleet_ID"].ToString();
                }

                Bindfunction();
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Function_ID"])))
                {
                    ddlFunction.SelectedValue = Request.QueryString["Function_ID"].ToString();
                }

                BindSystem_Location();
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["System_ID"])))
                {
                    ddlSystem_location.SelectedValue = Request.QueryString["System_ID"].ToString();
                }

                BindSubSystem_Location();
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["SubSystem_ID"])))
                {
                    ddlSubSystem_location.SelectedValue = Request.QueryString["SubSystem_ID"].ToString();
                }

                //To display records for linked system /subsystem
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["DisplayInheritingCounters"])))
                {
                    if (Request.QueryString["DisplayInheritingCounters"].ToString() == "1")
                    {
                        chkDisplayInheritingCounter.Checked = true;
                    }
                }

                BindRuningHours();
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
    /// to bind list of fleets to drop down
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
    /// to bind list of vessels to drop down
    /// </summary>
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
    /// to fill machinery running hours data to grid view.
    /// </summary>
    public void BindRuningHours()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            DataSet ds = new DataSet();

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            ds = objJobStatus.TecMachineryRunningHoursSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue),
                                                             UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text), null, null, ViewState["RecordDisplayType"].ToString(),
                                                             UDFLib.ConvertIntegerToNull(ddlFunction.SelectedValue), UDFLib.ConvertStringToNull(ddlSystem_location.SelectedValue.Split(',')[1]), UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[0]), UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[1]), UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[0])
                                                            , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, chkDisplayInheritingCounter.Checked == true ? 1 : 0);


            if (ViewState["RecordDisplayType"] == "H")
            {
                gvRhrs.Columns[3].Visible = false;
            }
            else
            {
                gvRhrs.Columns[3].Visible = true;
            }


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRhrs.DataSource = ds.Tables[0];
                gvRhrs.DataBind();
            }
            else
            {
                gvRhrs.DataSource = ds.Tables[0];
                gvRhrs.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindVesselDDL();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSystem_Location();
            BindSubSystem_Location();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void gvRhrs_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvRhrs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCRW_ID = (Label)e.Row.FindControl("lblCRW_ID");
                Label lblCreatedBy = (Label)e.Row.FindControl("lblCreatedBy");
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.SetRenderMethodDelegate(RenderHeaderApprHistory);
                ViewState["DynamicHeaderCSS"] = "HeaderStyle-css";
            }


            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ViewState["SORTBYCOLOUMN"] != null)
                {
                    HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                    if (img != null)
                    {
                        if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                            img.Src = "../../purchase/Image/arrowUp.png";

                        else
                            img.Src = "../../purchase/Image/arrowDown.png";

                        img.Visible = true;
                    }
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

    /// <summary>
    /// Excel export
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataSet ds = objJobStatus.TecMachineryRunningHoursSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
                                                                      , UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text), null, null, ViewState["RecordDisplayType"].ToString(),
                                                                       UDFLib.ConvertIntegerToNull(ddlFunction.SelectedValue), UDFLib.ConvertStringToNull(ddlSystem_location.SelectedValue.Split(',')[1]),
                                                                       UDFLib.ConvertIntegerToNull(ddlSystem_location.SelectedValue.Split(',')[0]), UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[1]),
                                                                       UDFLib.ConvertIntegerToNull(ddlSubSystem_location.SelectedValue.Split(',')[0]), sortbycoloumn, sortdirection, null, null,
                                                                       ref  rowcount, chkDisplayInheritingCounter.Checked == true ? 1 : 0);

            string[] HeaderCaptions = { "Vessel", "System Location", "Sub-System Location", "Date Hours Read", "Differential R.Hours", "Running Hours", "Created by", "Created Date" };
            string[] DataColumnsName = { "Vessel_Name", "Location", "SubLocation", "Date_Hours_Read", "diff_in_Rhrs", "Runing_Hours", "Created_By", "Created_On" };

            GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "Machinery Running Hours", "Machinery Running Hours", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        try
        {
            ucCustomPagerItems.isCountRecord = 1;
            BindRuningHours();
            UpdPnlGrid.Update();
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

            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ViewState["RecordDisplayType"] = "C";
            optRecordDisplayType.SelectedValue = "C";
            ViewState["LocationID"] = null;

            DDLFleet.SelectedValue = "0";
            BindVesselDDL();
            DDLVessel.SelectedValue = "0";
            txtFromDate.Text = "";
            txtToDate.Text = "";
            ddlFunction.ClearSelection();

            BindSystem_Location();
            BindSubSystem_Location();
            BindRuningHours();
            UpdPnlGrid.Update();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void optRecordDisplayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["RecordDisplayType"] = optRecordDisplayType.SelectedValue;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    protected void gvRhrs_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;

            BindRuningHours();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }


    #region "Function - Merge Grid Header"

    [Serializable]
    private class MergedColumnsInfoAppr
    {
        // indexes of merged columns
        public List<int> MergedColumnsAppr = new List<int>();
        // key-value pairs: key = the first column index, value = number of the merged columns
        public Hashtable StartColumnsAppr = new Hashtable();
        // key-value pairs: key = the first column index, value = common title of the merged columns 
        public Hashtable TitlesAppr = new Hashtable();

        //parameters: the merged columns indexes, common title of the merged columns 
        public void AddMergedColumnsAppr(int[] columnsIndexes, string title)
        {
            MergedColumnsAppr.AddRange(columnsIndexes);
            StartColumnsAppr.Add(columnsIndexes[0], columnsIndexes.Length);
            TitlesAppr.Add(columnsIndexes[0], title);
        }
    }

    private MergedColumnsInfoAppr infoAppr
    {
        get
        {
            if (ViewState["infoAppr"] == null)
                ViewState["infoAppr"] = new MergedColumnsInfoAppr();
            return (MergedColumnsInfoAppr)ViewState["infoAppr"];
        }
    }

    private void RenderHeaderApprHistory(HtmlTextWriter output, Control container)
    {

        for (int i = 0; i < container.Controls.Count; i++)
        {
            TableCell cell = (TableCell)container.Controls[i];
            //stretch non merged columns for two rows
            if (!infoAppr.MergedColumnsAppr.Contains(i))
            {
                cell.Attributes["rowspan"] = "2";

                cell.RenderControl(output);
            }
            else //render merged columns common title
                if (infoAppr.StartColumnsAppr.Contains(i))
                {


                    output.Write(string.Format("<th align='center'  colspan='{0}'>{1}</th>",
                             infoAppr.StartColumnsAppr[i], infoAppr.TitlesAppr[i]));


                }
        }

        //close the first row	
        output.RenderEndTag();
        //set attributes for the second row
        //start the second row
        output.RenderBeginTag("tr");

        //render the second row (only the merged columns)
        for (int i = 0; i < infoAppr.MergedColumnsAppr.Count; i++)
        {
            //if qtn eval 

            TableCell cell = (TableCell)container.Controls[infoAppr.MergedColumnsAppr[i]];

            cell.CssClass = "HeaderStyle-css";
            cell.RenderControl(output);

        }

        infoAppr.MergedColumnsAppr.Clear();
        infoAppr.StartColumnsAppr.Clear();
        infoAppr.TitlesAppr.Clear();
    }

    #endregion


    protected void ddlFunction_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSystem_Location();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void ddlSystem_location_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSubSystem_Location();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// To bind function list to drop down
    /// </summary>
    public void Bindfunction()
    {

        DataTable dt = objBLLPURC.LibraryGetSystemParameterList("115", "");

        ddlFunction.Items.Clear();
        ddlFunction.DataSource = dt;
        ddlFunction.DataValueField = "CODE";
        ddlFunction.DataTextField = "DESCRIPTION";
        ddlFunction.DataBind();
        ddlFunction.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlFunction.SelectedIndex = 0;
    }

    /// <summary>
    /// To bind system for selected of vessel and function
    /// </summary>
    public void BindSystem_Location()
    {
        DataTable dt = objBLLPURC.GET_SYSTEM_LOCATION(UDFLib.ConvertToInteger(ddlFunction.SelectedValue), UDFLib.ConvertToInteger(DDLVessel.SelectedValue));

        ddlSystem_location.Items.Clear();
        ddlSystem_location.DataSource = dt;
        ddlSystem_location.DataValueField = "AssginLocationID";
        ddlSystem_location.DataTextField = "LocationName";
        ddlSystem_location.DataBind();
        ddlSystem_location.Items.Insert(0, new ListItem("- ALL-", "0,0"));
        if (ddlSystem_location.SelectedIndex == -1)
            ddlSystem_location.SelectedIndex = 0;

    }

    /// <summary>
    /// To bind sub-system on for selected system.
    /// </summary>
    public void BindSubSystem_Location()
    {


        if (ddlSystem_location.SelectedValue != "0")
        {
            DataTable dt = objBLLPURC.GET_SUBSYTEMSYSTEM_LOCATION(ddlSystem_location.SelectedValue.ToString().Split(',')[1], null, UDFLib.ConvertToInteger(DDLVessel.SelectedValue));


            ddlSubSystem_location.Items.Clear();
            ddlSubSystem_location.DataSource = dt;
            ddlSubSystem_location.DataValueField = "AssginLocationID";
            ddlSubSystem_location.DataTextField = "LocationName";
            ddlSubSystem_location.DataBind();
            ddlSubSystem_location.Items.Insert(0, new ListItem("- ALL-", "0,0"));
            if (ddlSubSystem_location.SelectedIndex == -1)
                ddlSubSystem_location.SelectedIndex = 0;
        }

    }

}