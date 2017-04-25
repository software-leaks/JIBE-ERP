using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Text;
using SMS.Business.QMS;

public partial class SCM_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFleetDLL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();

            ViewState["VesselCode"] = 0;
            ViewState["ID"] = null;

            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 10;
            BindSCMReport();
        }
    }

    public void BindSCMReport()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataSet ds = BLL_SCM_Report.SCMReportMainSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(vesselcode)
            , UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text), sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);



        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvSCMReport.DataSource = ds.Tables[0];
            gvSCMReport.DataBind();

            if (ViewState["ID"] == null)
            {
                ViewState["ID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                gvSCMReport.SelectedIndex = 0;
            }

            SetRowSelection();
        }
        else
        {
            gvSCMReport.DataSource = ds.Tables[0];
            gvSCMReport.DataBind();
        }

        if (ds.Tables[0].Rows.Count == 0)
        {
            btnExport.Visible = false;
        }
        else
        {
            btnExport.Visible = true;
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

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {

        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
        DDLVessel.Items.Clear();
        //DataTable dtVessel = objVsl.GetVesselsByFleetID(int.Parse(DDLFleet.SelectedValue.ToString()));
        DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
        DDLVessel.DataSource = dtVessel;
        DDLVessel.DataTextField = "Vessel_name";
        DDLVessel.DataValueField = "Vessel_ID";
        DDLVessel.DataBind();
        ListItem li = new ListItem("--SELECT ALL--", "0");
        DDLVessel.Items.Insert(0, li);

    }

    protected void gvSCMReport_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ////e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';this.style.font='bold';";
            ////e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.font='bold';";
            ////e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            //e.Row.Attributes["onclick"] = ClientScript.GetPostBackEventReference(this.gvSCMReport, "Select$" + e.Row.RowIndex);
            //    LinkButton lbtnRequestNumber = (LinkButton)e.Row.FindControl("lbtVoyageNumberr");
        }

    }

    protected void gvSCMReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbnVesselName = (LinkButton)e.Row.FindControl("lbnVesselName");
            LinkButton lbnMeetingDate = (LinkButton)e.Row.FindControl("lbnMeetingDate");
            LinkButton lblPresentPosition = (LinkButton)e.Row.FindControl("lblIsVerified");
            LinkButton lblIsVerified = (LinkButton)e.Row.FindControl("lblPresentPosition");
            string SCMID = lbnVesselName.CommandArgument.ToString().Split(',')[0];
            string MeetingDate = lbnVesselName.CommandArgument.ToString().Split(',')[1];
            string VesselID = lbnVesselName.CommandArgument.ToString().Split(',')[2];

            


            lbnMeetingDate.Attributes.Add("onclick", "javascript:window.open('../SCM/SCM_Report_Details.aspx?SCMID=" + SCMID.Trim() + "&MeetingDate=" + MeetingDate + "&VesselID=" + VesselID + "'); return false;");
            lbnVesselName.Attributes.Add("onclick", "javascript:window.open('../SCM/SCM_Report_Details.aspx?SCMID=" + SCMID.Trim() + "&MeetingDate=" + MeetingDate + "&VesselID=" + VesselID + "'); return false;");
            lblPresentPosition.Attributes.Add("onclick", "javascript:window.open('../SCM/SCM_Report_Details.aspx?SCMID=" + SCMID.Trim() + "&MeetingDate=" + MeetingDate + "&VesselID=" + VesselID + "'); return false;");
            lblIsVerified.Attributes.Add("onclick", "javascript:window.open('../SCM/SCM_Report_Details.aspx?SCMID=" + SCMID.Trim() + "&MeetingDate=" + MeetingDate + "&VesselID=" + VesselID + "'); return false;");

            // ResponseHelper.Redirect("../SCM/SCM_Report_Details.aspx?SCMID=" + lblSCMID.Text.Trim() + "&MeetingDate=" + lbnMeetingDate.Text + "&VesselID=" + lblVesselID.Text, "Blank", "");
        }



    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataSet ds = BLL_SCM_Report.SCMReportMainSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(vesselcode)
            , UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text), sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, null, ref  rowcount);


        string[] HeaderCaptions = { "Vessel", "Meeting Date", "Present Position", "Verified" };
        string[] DataColumnsName = { "Vessel_Name", "MEETING_DATE_S", "PRESENT_POSITION", "IsVerified" };
        ds.Tables[0].Columns.Add("MEETING_DATE_S");
        foreach (DataRow item in ds.Tables[0].Rows)
        {
            item["MEETING_DATE_S"] = UDFLib.ConvertDateToNull(item["MEETING_DATE"]).Value.ToString("dd/MMM/yyyy");
        }

        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "SCM", "Safety Committee Meetings", HtmlFilterTable());
    }

    protected string HtmlFilterTable()
    {
        StringBuilder HtmlFilterTable = new StringBuilder();

        HtmlFilterTable.Append("<table border='1' cellpadding='2' cellpadding='0' width='100%'>");
        HtmlFilterTable.Append("<tr style='background-color: #F2F2F2;'><td>");
        HtmlFilterTable.Append("<b>Filters</b></td></tr>");
        HtmlFilterTable.Append("<tr><td>");

        HtmlFilterTable.Append("<table  border='0'  cellpadding='0' cellspacing='1' width='100%' style='color: Black;'>");

        HtmlFilterTable.Append("<tr>");
        HtmlFilterTable.Append("<td align='right'> Fleet:&nbsp;&nbsp;       </td> <td align='left'>" + DDLFleet.SelectedItem.Text + "</td>");
        HtmlFilterTable.Append("<td align='right'> From:&nbsp;&nbsp;        </td> <td align='left'>" + txtFromDate.Text + "</td>");
        HtmlFilterTable.Append("</tr>");

        HtmlFilterTable.Append("<tr>");
        HtmlFilterTable.Append("<td align='right'> Vessel :&nbsp;&nbsp;     </td> <td align='left'>" + DDLVessel.SelectedItem.Text + "</td>");
        HtmlFilterTable.Append("<td align='right'> To:&nbsp;&nbsp;          </td> <td align='left'>" + txtToDate.Text + "</td>");
        HtmlFilterTable.Append("</tr>");

        HtmlFilterTable.Append("</table>");

        HtmlFilterTable.Append("</td></tr>");
        HtmlFilterTable.Append("</table>");

        return HtmlFilterTable.ToString();
    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {

        ucCustomPagerItems.isCountRecord = 1;
        string vesselcode = DDLVessel.SelectedValue.ToString();

        ViewState["VesselCode"] = DDLVessel.SelectedValue.ToString();

        BindSCMReport();
        UpdPnlGrid.Update();

    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        ViewState["VesselCode"] = 0;
        ViewState["SORTDIRECTION"] = null;
        ViewState["SORTBYCOLOUMN"] = null;

        BindFleetDLL();
        BindVesselDDL();

        txtFromDate.Text = "";
        txtToDate.Text = "";

        BindSCMReport();
        UpdPnlGrid.Update();
    }

    protected void gvSCMReport_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindSCMReport();
        SetRowSelection();

    }

    private void SetRowSelection()
    {
        gvSCMReport.SelectedIndex = -1;
        for (int i = 0; i < gvSCMReport.Rows.Count; i++)
        {
            if (gvSCMReport.DataKeys[i].Value.ToString().Equals(ViewState["ID"].ToString()))
            {
                gvSCMReport.SelectedIndex = i;
            }
        }
    }


    protected void ImgSCMDetails_Click(object sender, CommandEventArgs e)
    {
        ResponseHelper.Redirect("../SCM/SCM_Report_Details.aspx?VesselID=" + e.CommandArgument.ToString(), "Blank", "");
    }


    protected void gvSCMReport_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {

        Label lblSCMID = (Label)gvSCMReport.Rows[se.NewSelectedIndex].FindControl("lblSCMID");
        LinkButton lbnMeetingDate = (LinkButton)gvSCMReport.Rows[se.NewSelectedIndex].FindControl("lbnMeetingDate");


        Label lblVesselID = (Label)gvSCMReport.Rows[se.NewSelectedIndex].FindControl("lblVesselID");



        ViewState["ID"] = ((Label)gvSCMReport.Rows[se.NewSelectedIndex].FindControl("lblSCMID")).Text;

        ResponseHelper.Redirect("../SCM/SCM_Report_Details.aspx?SCMID=" + lblSCMID.Text.Trim() + "&MeetingDate=" + lbnMeetingDate.Text + "&VesselID=" + lblVesselID.Text, "Blank", "");


    }



}