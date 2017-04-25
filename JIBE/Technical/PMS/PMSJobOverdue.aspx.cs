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
using SMS.Business.Crew;


public partial class PMSJobOverdue : System.Web.UI.Page
{

    BLL_PMS_Job_Status objJobStatus = new BLL_PMS_Job_Status();
    public MergeGridviewHeader_Info mergeheadgvOverdue;
    public MergeGridviewHeader_Info mergeheadgvOverdueHistory  ;
    public MergeGridviewHeader_Info mergeheadgvJobHistory;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Custom_Filters();
            FillDDL();
            FillMonth();

            string CurrYear = System.DateTime.Now.ToString("yyyy");
            string CurrMonth = System.DateTime.Now.ToString("MM");

            ddlMonth.ClearSelection();
            ddlMonth.SelectedIndex = Convert.ToInt32(CurrMonth) - 1;

            ddlYear.ClearSelection();
            ddlYear.SelectedValue = CurrYear;



            ViewState["DeptType"] = null;
            ViewState["Status"] = "P";
            ViewState["VesselCode"] = 0;
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;


            ucCustomPagerItems.PageSize = 15;
            FillDLLjobDepartment();
           

            BindOverDueJobs();

            DivSuptdResponse.Visible = false;

            hfAppName.Value = System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString();
        }

        // Job Overdue Merged Header

        //job Overdue History

        // Job History

    }




    private void Bind_Custom_Filters()
    {
        if (!IsPostBack)
        {

            // Job Rank - Filter
            DataTable dtJobRank = objJobStatus.TecJobsGetRanks();
            ucf_DDLRank.DataValueField = "ID";
            ucf_DDLRank.DataTextField = "Rank_Short_Name";
            ucf_DDLRank.DataSource = dtJobRank;


            // Job Critical - Filter
            DataTable dtJobCritical = new DataTable();
            dtJobCritical.Columns.Add("ID", typeof(int));
            dtJobCritical.Columns.Add("Critical_Name", typeof(string));

            dtJobCritical.Rows.Add(1, "Critical");
            dtJobCritical.Rows.Add(0, "Non Critical");

            ucf_optCritical.DataValueField = "ID";
            ucf_optCritical.DataTextField = "Critical_Name";
            ucf_optCritical.DataSource = dtJobCritical;

            // Job CMS - Filter
            DataTable dtJobCMS = new DataTable();
            dtJobCMS.Columns.Add("ID", typeof(int));
            dtJobCMS.Columns.Add("CMS_Name", typeof(string));

            dtJobCMS.Rows.Add(1, "CMS");
            dtJobCMS.Rows.Add(0, "Non CMS");

            ucf_optCMS.DataValueField = "ID";
            ucf_optCMS.DataTextField = "CMS_Name";
            ucf_optCMS.DataSource = dtJobCMS;
             

        }
    }
    
    

    public void FillMonth()
    {
        string CurrYear = System.DateTime.Now.ToString("yy");

        ddlMonth.Items.Insert(01, "Jan  " + CurrYear);
        ddlMonth.Items.Insert(02, "Feb  " + CurrYear);
        ddlMonth.Items.Insert(03, "Mar  " + CurrYear);
        ddlMonth.Items.Insert(04, "Apr   " + CurrYear);
        ddlMonth.Items.Insert(05, "May  " + CurrYear);
        ddlMonth.Items.Insert(06, "Jun  " + CurrYear);
        ddlMonth.Items.Insert(07, "Jul  " + CurrYear);
        ddlMonth.Items.Insert(08, "Aug  " + CurrYear);
        ddlMonth.Items.Insert(09, "Sep  " + CurrYear);
        ddlMonth.Items.Insert(10, "Oct  " + CurrYear);
        ddlMonth.Items.Insert(11, "Nov  " + CurrYear);
        ddlMonth.Items.Insert(12, "Dec  " + CurrYear);

        //ddlMonth.Items.FindByText(System.DateTime.Now.Month.ToString("MM")).Selected = true;
        //ddlMonth.Items.FindByValue(CurrMonth).Selected = true;
    }

   
    

    private void FillDLLjobDepartment()
    {
        try
        {
            BLL_PMS_Library_Jobs obj = new BLL_PMS_Library_Jobs();

            DataTable dt = obj.LibraryGetPMSSystemParameterList("2487", "");
            DDLJobDepartment.DataSource = dt;
            DDLJobDepartment.DataValueField = "Code";
            DDLJobDepartment.DataTextField = "Name";
            DDLJobDepartment.DataBind();
        }
        catch
        {
        }
    }

    public void BindOverDueJobs()
    {  
        mergeheadgvOverdue = new MergeGridviewHeader_Info();
        mergeheadgvOverdue.AddMergedColumns(new int[] { 7, 8 }, "Frequency", "PMSGridHeaderStyle-css");
        mergeheadgvOverdue.AddMergedColumns(new int[] { 9, 10 }, "Last Done", "PMSGridHeaderStyle-css");
        mergeheadgvOverdue.AddMergedColumns(new int[] { 14, 15 }, "M/C R. Hrs.", "PMSGridHeaderStyle-css");

 
        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

       
        DataSet ds = objJobStatus.TecJobOverDueSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), ucf_DDLVessel.SelectedValues, ucf_DLLLocation.SelectedValues, ucf_DDLSubCatalogue.SelectedValues
            , UDFLib.ConvertIntegerToNull(DDLJobDepartment.SelectedValue), ucf_DDLRank.SelectedValues,UDFLib.ConvertStringToNull(ucf_txtSearchJobTitle.FilterText), ucf_txtSearchJobTitle.FilterType
            , UDFLib.ConvertIntegerToNull(ucf_txtSearchJobID.FilterNumber), ucf_txtSearchJobID.FilterType, ucf_optCritical.SelectedValues, ucf_optCMS.SelectedValues, null, null
            , UDFLib.ConvertIntegerToNull(ddlMonth.SelectedValue), UDFLib.ConvertIntegerToNull(ddlYear.SelectedValue), UDFLib.ConvertStringToNull(optStatus.SelectedValue)
            , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvOverDueJobs.DataSource = ds.Tables[0];
            gvOverDueJobs.DataBind();

            if (ViewState["Jobid"] == null)
            {
                ViewState["Jobid"] = ds.Tables[0].Rows[0]["JOB_ID"].ToString();
                ViewState["Job_OverDue_ID"] = ds.Tables[0].Rows[0]["OverDue_ID"].ToString();
                ViewState["Vessel_ID"] = ds.Tables[0].Rows[0]["Vessel_ID"].ToString();

                gvOverDueJobs.SelectedIndex = 0;
                //BindJobsOverdueHistory(Convert.ToInt32(ViewState["Jobid"].ToString()));
            }

            SetJobOverdueRowSelection();

        }
        else
        {
            gvOverDueJobs.SelectedIndex = -1;
            gvOverDueJobs.DataSource = ds.Tables[0];
            gvOverDueJobs.DataBind();

            gvJobHistory.DataSource = ds.Tables[0];
            gvJobHistory.DataBind();

        }
    }

    public int? GetMonth(string strMonth)
    {

        int? month = null;

        switch (strMonth.ToUpper())
        {
            case "JAN":
                month = 01;
                break;
            case "FEB":
                month = 02;
                break;
            case "MAR":
                month = 03;
                break;
            case "APR":
                month = 04;
                break;
            case "MAY":
                month = 05;
                break;
            case "JUN":
                month = 06;
                break;
            case "JUL":
                month = 07;
                break;
            case "AUG":
                month = 08;
                break;
            case "SEP":
                month = 09;
                break;
            case "OCT":
                month = 10;
                break;
            case "NOV":
                month = 11;
                break;
            case "DEC":
                month = 12;
                break;
        }
        return month;
    }

    public void BindJobsOverdueHistory(int? Jobid)
    {

        BLL_PMS_Job_Status objJobStatus = new BLL_PMS_Job_Status();

         mergeheadgvOverdueHistory = new MergeGridviewHeader_Info();
         mergeheadgvOverdueHistory.AddMergedColumns(new int[] { 6, 7 }, "Frequency", "PMSGridHeaderStyle-css");


         //mergeheadgvJobHistory = new MergeGridviewHeader_Info();
         //mergeheadgvOverdueHistory.AddMergedColumns(new int[] { 6, 7 }, "Frequency", "PMSGridHeaderStyle-css");

        int rowcount = ucCustomPagerItems.isCountRecord;

        int? jobid = null; if (ViewState["Jobid"] != null) jobid = Convert.ToInt32(ViewState["Jobid"].ToString());

        DataSet ds = objJobStatus.TecJobsOverdueHistorySearch(jobid, null
            , null, null, null, null, null, null, null, null
            , null, null, null, null, null, null
            , null, null, ref  rowcount);


        if (ds.Tables[0].Rows.Count > 0)
        {
            gvOverDueJobHistory.DataSource = ds.Tables[0];
            gvOverDueJobHistory.DataBind();

            //mergeheadgvJobHistory.AddMergedColumns(new int[] { 6, 7 }, "Frequency", "PMSGridHeaderStyle-css");

            gvJobHistory.DataSource = ds.Tables[1];
            gvJobHistory.DataBind();
        }
        else
        {
            gvOverDueJobHistory.DataSource = ds.Tables[0];
            gvOverDueJobHistory.DataBind();

           // mergeheadgvJobHistory.AddMergedColumns(new int[] { 6, 7 }, "Frequency", "PMSGridHeaderStyle-css");
            gvJobHistory.DataSource = ds.Tables[0];
            gvJobHistory.DataBind();
        }
    }

    public void BindJobsOverdueList(int JobOverDueid)
    {

        BLL_PMS_Job_Status objJobStatus = new BLL_PMS_Job_Status();

        DataSet ds = objJobStatus.TecJobsOverdueList(Convert.ToInt32(ViewState["Job_OverDue_ID"].ToString()));
        DataRow dr = ds.Tables[0].Rows[0];
        txtDivJobTitle.Text = dr["Job_Title"].ToString();
        txtDivOverDueReason.Text = dr["Overdue_Reason"].ToString();
        txtDivTentativeCompletionDate.Text = dr["Tentative_Completion_Date"].ToString();

        if (dr["Modified_Completion_Date"].ToString() != "")
            txtDivModifiedCompletionDate.Text = dr["Modified_Completion_Date"].ToString();
        else
            txtDivModifiedCompletionDate.Text = dr["Tentative_Completion_Date"].ToString();

        txtDivSuptdResponse.Text = dr["Suptd_Response"].ToString();

        if (dr["MODIFIEDCOMPLETIONDATEFLAG"].ToString() == "1")
            btnDivSave.Enabled = true;
        else
            btnDivSave.Enabled = false;

    }

    public void FillDDL()
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
            DDLFleet.Items.Insert(0, new ListItem("--SELECT ALL--", "0"));

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            ucf_DDLVessel.DataTextField = "Vessel_name";
            ucf_DDLVessel.DataValueField = "Vessel_id";
            ucf_DDLVessel.DataSource = dtVessel;

        }
        catch (Exception ex)
        {

        }
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {

        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
        if (DDLFleet.SelectedValue.ToString() == "0")
        {
            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            //Bind Vessel for user control of header
            ucf_DDLVessel.DataTextField = "Vessel_name";
            ucf_DDLVessel.DataValueField = "Vessel_id";
            ucf_DDLVessel.DataSource = dtVessel;
        }
        else
        {
            DataTable dtVessel = objVsl.GetVesselsByFleetID(int.Parse(DDLFleet.SelectedValue.ToString()));
            //Bind Vessel for user control of header
            ucf_DDLVessel.DataTextField = "Vessel_name";
            ucf_DDLVessel.DataValueField = "Vessel_id";
            ucf_DDLVessel.DataSource = dtVessel;
        }
    }



    protected void gvOverDueJobs_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {
        ucCustomPagerItems.isCountRecord = 1;
        gvOverDueJobs.SelectedIndex = se.NewSelectedIndex;
        ViewState["Jobid"] = ((LinkButton)gvOverDueJobs.Rows[se.NewSelectedIndex].FindControl("lblJobCode")).Text;
        BindOverDueJobs();

        BindJobsOverdueHistory(Convert.ToInt32(ViewState["Jobid"].ToString()));

        //UpdPnlJobOverdueHistoryGrid.Update();
        // UpdPnlJobHistoryGrid.Update();

        // btnRetrieve_Click(null, null);
    }

    protected void gvOverDueJobs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCMS = (Label)e.Row.FindControl("lblCMS");
            Label lblCritical = (Label)e.Row.FindControl("lblCritical");
            Label lblJobTitle = (Label)e.Row.FindControl("lblJobTitle");
            Label lblLocationID = (Label)e.Row.FindControl("lblLocationID");
            Label lblLocation = (Label)e.Row.FindControl("lblLocation");

            Label lblOverDueFlage = (Label)e.Row.FindControl("lblOverDueFlage");
            Label lblNext30dayFlage = (Label)e.Row.FindControl("lblNext30dayFlage");
            Label lblJobDescription = (Label)e.Row.FindControl("lblJobDescription");

            Label lblMachineryDeatils = (Label)e.Row.FindControl("lblMachineryDeatils");

            Label lblModifiedCompletionDateFlage = (Label)e.Row.FindControl("lblModifiedCompletionDateFlage");
            ImageButton ImgResponded = (ImageButton)e.Row.FindControl("ImgResponded");
            ImageButton ImgPendingResponse = (ImageButton)e.Row.FindControl("ImgPendingResponse");



            Label lblRespondedby = (Label)e.Row.FindControl("lblRespondedby");
            Label lblTentativecompletionDate = (Label)e.Row.FindControl("lblTentativecompletionDate");
            Label lblModifiedcompletionDate = (Label)e.Row.FindControl("lblModifiedcompletionDate");

            Label lblOverDueReason = (Label)e.Row.FindControl("lblOverDueReason");
            Label lblSuptdResponse = (Label)e.Row.FindControl("lblSuptdResponse");

            LinkButton lnkbtnJobCode = (LinkButton)e.Row.FindControl("lblJobCode");


       
         

            lblJobTitle.Attributes.Add("onclick", "document.getElementById('iFrmJobsDetails').src ='/"+System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString()+"/Technical/PMS/PMSJobIndividualDetails.aspx?JobID=" + lnkbtnJobCode.Text + "';showModal('dvJobsDetails');return false;");




            lblJobTitle.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Job Description] body=[" + lblJobDescription.Text + "]");
            lblLocation.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Machinery Details] body=[" + lblMachineryDeatils.Text + "]");

            if (lblOverDueReason.Text != "")
                lblTentativecompletionDate.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Vessel Reason for Over Due] body=[" + lblOverDueReason.Text + "]");
            if (lblSuptdResponse.Text != "")
                lblModifiedcompletionDate.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + lblRespondedby.Text + ": Responded] body=[" + lblSuptdResponse.Text + "]");


            Int64 result = 0;
            if (Int64.TryParse(lblModifiedCompletionDateFlage.Text, out result))
            {
                ImgResponded.Visible = (result == 0) ? true : false;
                ImgPendingResponse.Visible = (result == 0) ? false : true;
            }


            if (lblOverDueFlage.Text == "Y")
            {
                e.Row.Cells[11].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[11].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[11].Font.Bold = true;
            }
            if (lblNext30dayFlage.Text == "Y")
            {
                e.Row.Cells[11].BackColor = System.Drawing.Color.Orange;
                e.Row.Cells[11].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[11].Font.Bold = true;
            }
            if (lblCMS.Text == "Y")
            {
                e.Row.Cells[12].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[12].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[12].Font.Bold = true;
            }
            if (lblCritical.Text == "Y")
            {
                e.Row.Cells[13].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[13].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[13].Font.Bold = true;
            }
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(mergeheadgvOverdue);
            
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
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

    protected void imgCatalogueSearch_Click(object sender, ImageClickEventArgs e)
    {
        ucCustomPagerItems.isCountRecord = 1;
        BindOverDueJobs();
    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataSet ds = objJobStatus.TecJobOverDueSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), ucf_DDLVessel.SelectedValues, ucf_DLLLocation.SelectedValues, ucf_DDLSubCatalogue.SelectedValues
           , UDFLib.ConvertIntegerToNull(DDLJobDepartment.SelectedValue), ucf_DDLRank.SelectedValues, UDFLib.ConvertStringToNull(ucf_txtSearchJobTitle.FilterText), ucf_txtSearchJobTitle.FilterType
           , UDFLib.ConvertIntegerToNull(ucf_txtSearchJobID.FilterNumber), ucf_txtSearchJobID.FilterType, ucf_optCritical.SelectedValues, ucf_optCMS.SelectedValues, null, null
           , UDFLib.ConvertIntegerToNull(ddlMonth.SelectedValue), UDFLib.ConvertIntegerToNull(ddlYear.SelectedValue), UDFLib.ConvertStringToNull(optStatus.SelectedValue)
          , sortbycoloumn, sortdirection, null, null, ref  rowcount);

        string[] HeaderCaptions = { "Vessel", "Location", "SubSystem", "Job Code", "Job Title", "Frequency", "Frequency Name", "Last Done", "Rhrs", "Next Due", "CMS", "Critical", "Tentative Completion Date", "Modified Completion Date", "Responded By" };
        string[] DataColumnsName = { "Vessel_Name", "Location", "SubSystem", "JOB_ID", "JOB_TITLE", "FREQUENCY", "Frequency_Name", "LAST_DONE", "RHRS_DONE", "DATE_NEXT_DUE", "CMS", "Critical", "Tentative_Completion_Date", "Modified_Completion_Date", "Actioned_By" };

        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "PMS Overdue Jobs", "PMS Overdue Jobs","");

    }



    #region "Code commented on 31.05.2012"

    //protected string HtmlFilterTable()
    //{
    //    StringBuilder HtmlFilterTable = new StringBuilder();

    //    HtmlFilterTable.Append("<table BORDER='1' CELLPADDING='2' CELLSPACING='0' width='100%'>");
    //    HtmlFilterTable.Append("<tr style='background-color: #F2F2F2;'><td>");
    //    HtmlFilterTable.Append("<b>Filters</b></td></tr>");
    //    HtmlFilterTable.Append("<tr><td>");

    //    HtmlFilterTable.Append("<table  BORDER='0'  cellpadding='0' cellspacing='1' width='100%' style='color: Black;'>");

    //    HtmlFilterTable.Append("<tr>");
    //    HtmlFilterTable.Append("<td align='right'> Fleet:&nbsp;&nbsp;       </td> <td align='left'>" + DDLFleet.SelectedItem.Text + "</td>");
    //    HtmlFilterTable.Append("<td align='right'> Location:&nbsp;&nbsp;    </td> <td align='left'>" + DLLLocation.SelectedText != "" ? DLLLocation.SelectedText : "--SELECT ALL--" + "</td>");
    //    HtmlFilterTable.Append("<td align='right'> Type:&nbsp;&nbsp;        </td> <td align='left'>" + optCMS.SelectedItem.Text.Trim() + "</td>");
    //    HtmlFilterTable.Append("<td align='right'> Month:&nbsp;&nbsp;       </td> <td align='left'>" + ddlMonth.SelectedItem.Text + "</td>");
    //    HtmlFilterTable.Append("<td align='right'> Year:&nbsp;&nbsp;        </td> <td align='left'>" + ddlYear.SelectedItem.Text + "</td>");
    //    HtmlFilterTable.Append("</tr>");

    //    HtmlFilterTable.Append("<tr>");
    //    HtmlFilterTable.Append("<td align='right'> Vessel :&nbsp;&nbsp;     </td> <td align='left'>" + DDLVessel.SelectedItem.Text + "</td>");
    //    HtmlFilterTable.Append("<td align='right'> SubCat. :&nbsp;&nbsp;    </td> <td align='left'>" + DDLSubCatalogue.SelectedItem.Text + "</td>");
    //    HtmlFilterTable.Append("<td align='right'> Criticality :&nbsp;&nbsp;</td> <td align='left'>" + optCritical.SelectedItem.Text.Trim() + "</td>");
    //    HtmlFilterTable.Append("<td align='right'> Job Code :&nbsp;&nbsp;   </td> <td align='left'>" + txtSearchJobID.Text + "</td>");
    //    //HtmlFilterTable.Append("<td align='right'> From :&nbsp;&nbsp;       </td> <td align='left'>" + txtFromDate.Text + "</td>");
    //    HtmlFilterTable.Append("</tr>");

    //    HtmlFilterTable.Append("<tr>");
    //    HtmlFilterTable.Append("<td align='right'>                             </td><td align='left'></td>");
    //    HtmlFilterTable.Append("<td align='right'>Department :&nbsp;&nbsp;     </td><td align='left'>" + DDLJobDepartment.SelectedItem.Text.Trim() + "</td>");
    //    HtmlFilterTable.Append("<td align='right'>Response Status :&nbsp;&nbsp;</td><td align='left'>" + optStatus.SelectedItem.Text.Trim() + "</td>");
    //    HtmlFilterTable.Append("<td align='right'>Job Title :&nbsp;&nbsp;      </td><td align='left'>" + txtSearchTitleJobCode.Text + "</td>");
    //    //HtmlFilterTable.Append("<td align='right'>To :&nbsp;&nbsp;             </td><td align='left'>" + txtToDate.Text + "</td>");
    //    HtmlFilterTable.Append("</tr>");

    //    HtmlFilterTable.Append("</table>");

    //    HtmlFilterTable.Append("</td></tr>");
    //    HtmlFilterTable.Append("</table>");

    //    return HtmlFilterTable.ToString();
    //}

    #endregion


    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        ucCustomPagerItems.isCountRecord = 1;
       
 
        BindOverDueJobs();

        //UpdPnlOverDueGrid.Update();
        //UpdPnlJobOverdueHistoryGrid.Update();
        //UpdPnlJobHistoryGrid.Update();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

        ViewState["Status"] = "P";

        ViewState["DeptType"] = null;
        ViewState["VesselCode"] = 0;
        ViewState["Jobid"] = null;
        ViewState["SORTDIRECTION"] = null;
        ViewState["SORTBYCOLOUMN"] = null;

        DDLFleet.SelectedValue = "0";

        ucf_DDLVessel.ClearSelection();
        ucf_DLLLocation.ClearSelection();
        ucf_DDLRank.ClearSelection();
        ucf_DLLLocation.ClearSelection();
        ucf_DDLSubCatalogue.ClearSelection();
        ucf_optCMS.ClearSelection();
        ucf_optCritical.ClearSelection();

        ucf_txtSearchJobID.FilterNumber = "";
        ucf_txtSearchJobTitle.FilterText = "";

        DDLJobDepartment.SelectedValue = "0";
        ddlMonth.SelectedValue = "0";
        ddlYear.SelectedValue = "0";

        optStatus.SelectedValue = "P";
     
        BindOverDueJobs();

        ViewState["Jobid"] = null;
        BindJobsOverdueHistory(0);



        // UpdPnlOverDueGrid.Update();
        // UpdPnlJobHistoryGrid.Update();
    }

    protected void gvOverDueJobs_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindOverDueJobs();

    }

    private void SetCatalogueRowSelection()
    {
        gvOverDueJobs.SelectedIndex = -1;
        for (int i = 0; i < gvOverDueJobs.Rows.Count; i++)
        {
            if (gvOverDueJobs.DataKeys[i].Value.ToString().Equals(ViewState["SystemId"].ToString()))
            {
                gvOverDueJobs.SelectedIndex = i;
            }
        }
    }

    protected void ImgResponse_Click(object sender, CommandEventArgs e)
    {
        string[] strIds = e.CommandArgument.ToString().Split(',');

        string overdue_id = strIds[0].ToString();
        string job_id = strIds[1].ToString();
        string vesselid = strIds[2].ToString();

        ViewState["Jobid"] = job_id;
        ViewState["Job_OverDue_ID"] = overdue_id;
        ViewState["Vessel_ID"] = vesselid;
        DivSuptdResponse.Visible = true;

        BindJobsOverdueHistory(Convert.ToInt32(ViewState["Jobid"].ToString()));

        BindJobsOverdueList(Convert.ToInt32(ViewState["Job_OverDue_ID"].ToString()));

        SetJobOverdueRowSelection();

        // UpdPnlJobHistoryGrid.Update();
        updpnlDivSuptdResponse.Update();

        btnRetrieve_Click(null, null);

    }
     


    protected void gvJobHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblJobHistoryRemaks = (Label)e.Row.FindControl("lblRemarks");
            Label lblFullJobHistoryRemaks = (Label)e.Row.FindControl("lblFullRemarks");

            Label lblCMS_H = (Label)e.Row.FindControl("lblCMS_H");
            Label lblCritical_H = (Label)e.Row.FindControl("lblCritical_H");

            System.Data.DataRowView gvrow = (System.Data.DataRowView)e.Row.DataItem;

            ImageButton ImgSpareUsed = (ImageButton)e.Row.FindControl("ImgSpareUsed");
            ImgSpareUsed.Attributes.Add("onclick", "document.getElementById('iFrmJobsDetails').src ='/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/Technical/PMS/PMS_SparesUsed.aspx?JobID=" + gvrow["JOB_ID"].ToString() + "&Vessel_ID=" + gvrow["Vessel_ID"].ToString() + "';showModal('dvJobsDetails');return false;");


            if (lblFullJobHistoryRemaks.Text != "")
                lblJobHistoryRemaks.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Remarks] body=[" + lblFullJobHistoryRemaks.Text + "]");


            ImgSpareUsed.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Remarks] body=[Spare Item Used]");




            if (lblCMS_H.Text == "Y")
            {
                e.Row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                e.Row.Cells[10].ForeColor = System.Drawing.Color.Black;

            }
            if (lblCritical_H.Text == "Y")
            {
                e.Row.Cells[11].BackColor = System.Drawing.Color.Salmon;
                e.Row.Cells[11].ForeColor = System.Drawing.Color.Black;

            }

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {

            //MergeGridviewHeader.SetProperty(mergeheadgvJobHistory);
            //e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);

        }
    }

    protected void gvJobHistory_Sorting(object sender, GridViewSortEventArgs e)
    {



    }

    private void SetJobOverdueRowSelection()
    {
        gvOverDueJobs.SelectedIndex = -1;
        for (int i = 0; i < gvOverDueJobs.Rows.Count; i++)
        {
            if (gvOverDueJobs.DataKeys[i].Value.ToString().Equals(ViewState["Jobid"].ToString()))
            {
                gvOverDueJobs.SelectedIndex = i;
            }
        }
    }

    protected void btnDivClose_Click(object sender, EventArgs e)
    {
        DivSuptdResponse.Visible = false;

        btnRetrieve_Click(null, null);
    }

    protected void btnDivSave_Click(object sender, EventArgs e)
    {

        BLL_PMS_Job_Status objJobStatus = new BLL_PMS_Job_Status();

        objJobStatus.TecJobsOverdueResponse(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(ViewState["Job_OverDue_ID"].ToString())
            , Convert.ToDateTime(txtDivModifiedCompletionDate.Text)
            , txtDivSuptdResponse.Text, Convert.ToInt32(ViewState["Vessel_ID"].ToString()));

        ViewState["Jobid"] = null;

        BindOverDueJobs();
        //BindJobsOverdueHistory(Convert.ToInt32(ViewState["Jobid"].ToString()));
        DivSuptdResponse.Visible = false;
        //UpdPnlOverDueGrid.Update();
        // UpdPnlJobHistoryGrid.Update();


    }




    protected void gvOverDueJobHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblJobDescription_OH = (Label)e.Row.FindControl("lblJobDescription_OH");
            Label lblMachineryDeatils_OH = (Label)e.Row.FindControl("lblMachineryDeatils_OH");

            Label lblJobTitle_OH = (Label)e.Row.FindControl("lblJobTitle_OH");
            Label lblLocation_OH = (Label)e.Row.FindControl("lblLocation_OH");

            Label lblRespondedby_OH = (Label)e.Row.FindControl("lblRespondedby_OH");
            Label lblTentativecompletionDate_OH = (Label)e.Row.FindControl("lblTentativecompletionDate_OH");
            Label lblModifiedcompletionDate_OH = (Label)e.Row.FindControl("lblModifiedcompletionDate_OH");

            Label lblOverDueReason_OH = (Label)e.Row.FindControl("lblOverDueReason_OH");
            Label lblSuptdResponse_OH = (Label)e.Row.FindControl("lblSuptdResponse_OH");

            lblJobTitle_OH.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Job Description] body=[" + lblJobDescription_OH.Text + "]");
            lblLocation_OH.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Machinery Details] body=[" + lblMachineryDeatils_OH.Text + "]");

            if (lblOverDueReason_OH.Text != "")
                lblTentativecompletionDate_OH.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Vessel Reason for Over Due] body=[" + lblOverDueReason_OH.Text + "]");
            if (lblSuptdResponse_OH.Text != "")
                lblModifiedcompletionDate_OH.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + lblRespondedby_OH.Text + ": Responded] body=[" + lblSuptdResponse_OH.Text + "]");

            Label lblCMS_OH = (Label)e.Row.FindControl("lblCMS_OH");
            Label lblCritical_OH = (Label)e.Row.FindControl("lblCritical_OH");

            if (lblCMS_OH.Text == "Y")
            {
                e.Row.Cells[10].BackColor = System.Drawing.Color.LightGreen;
                e.Row.Cells[10].ForeColor = System.Drawing.Color.Black;
            }
            if (lblCritical_OH.Text == "Y")
            {
                e.Row.Cells[11].BackColor = System.Drawing.Color.Salmon;
                e.Row.Cells[11].ForeColor = System.Drawing.Color.Black;
                //e.Row.Cells[11].BackColor = System.Drawing.Color.Moccasin;
            }

        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            //MergeGridviewHeader.SetProperty(mergeheadgvOverdueHistory);
            //e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);

        }
    }

    protected void gvOverDueJobHistory_Sorting(object sender, GridViewSortEventArgs e)
    {

    }


}