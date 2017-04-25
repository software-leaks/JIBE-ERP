using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.QMS;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Data;

public partial class QMS_SCM_SCM_VesselIssueAssignment : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {





        if (!IsPostBack)
        {
            this.gvIssueAssign.Columns[8].HeaderText = " Assign Department"; 

            BindFleetDLL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();

            Get_UserDetails();

            FillDDLOfficeDept();
            FillDDLYear();

            ViewState["VesselCode"] = 0;

            ViewState["DeptCode"] = null;
            ViewState["ID"] = null;

            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;
            ViewState["ResponseID"] = null;

            //  DDLOfficeDept.SelectedValue = ViewState["DeptID"].ToString();

            SetPreviousMonthYear();

            ucCustomPagerItems.PageSize = 15;
            BindSCMIssueAssignmentSearch();



        }

        UserAccessValidation();

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

            btnRqstUptResponse.Enabled = false;
            btnUnMarkSMSReview.Enabled = false;
            lnkCheckAll.Enabled = false;
            lnkUnChekAll.Enabled = false;

            gvIssueAssign.Columns[gvIssueAssign.Columns.Count - 2].Visible = false;

        }



    }


    public void SetPreviousMonthYear()
    {


        if (ddlYear.Items.Count > 1)
            ddlYear.SelectedValue = Session["maxYear"].ToString();//DateTime.Now.ToString("yyyy");
        //else
        //    ddlYear.SelectedValue = ddlYear.SelectedValue;

        string CurrMonth = System.DateTime.Now.ToString("MM");
        ddlMonth.ClearSelection();

        if (CurrMonth == "01")
        {
            ddlMonth.SelectedIndex = 12;
            ddlYear.SelectedValue = (Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1).ToString();
        }
        else
        {
            ddlMonth.SelectedIndex = Convert.ToInt32(CurrMonth) - 1;
            if (ddlYear.Items.Count > 1)
                ddlYear.SelectedValue = Session["maxYear"].ToString();
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




    public void Get_UserDetails()
    {
        BLL_Infra_UserCredentials ojbInfra = new BLL_Infra_UserCredentials();
        DataTable dtuser = ojbInfra.Get_UserDetails(Convert.ToInt32(Session["userid"].ToString()));

        ViewState["DeptID"] = dtuser.Rows[0]["Dep_Code"].ToString();
        ViewState["USERTYPE"] = dtuser.Rows[0]["User_Type"].ToString();
    }

    public void FillDDLOfficeDept()
    {
        try
        {

            int? companyid = null;
            if ((Session["USERCOMPANYID"].ToString() != "") || (Session["USERCOMPANYID"] == null))
                companyid = Convert.ToInt32(Session["USERCOMPANYID"].ToString());

            DataTable dtOfficeDept = BLL_SCM_Report.SCMGetOfficeDepartment(companyid);
            DDLOfficeDept.Items.Clear();
            DDLOfficeDept.DataSource = dtOfficeDept;
            DDLOfficeDept.DataTextField = "VALUE";
            DDLOfficeDept.DataValueField = "ID";
            DDLOfficeDept.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLOfficeDept.Items.Insert(0, li);

        }
        catch (Exception ex)
        {

        }

    }

    public void FillDDLYear()
    {
        try
        {

            DataTable dtyears = BLL_SCM_Report.GetVesselIssuesYears();


            int minyear = int.MaxValue;
            int maxyear = int.MinValue;
            if (dtyears.Rows.Count>0)
            {
                foreach (DataRow dr in dtyears.Rows)
                {
                    int year = dr.Field<int>("ISSUE_CREATED_YEAR");
                    minyear = Math.Min(minyear, year);
                    maxyear = Math.Max(maxyear, year);
                }

                Session["maxYear"] = maxyear.ToString();
            }
           
            ddlYear.Items.Clear();
            ddlYear.DataSource = dtyears;
            ddlYear.DataTextField = "ISSUE_CREATED_YEAR";
            ddlYear.DataValueField = "ISSUE_CREATED_YEAR";
            ddlYear.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            ddlYear.Items.Insert(0, li);

        }
        catch (Exception ex)
        {


        }
    }

    public void BindSCMIssueAssignmentSearch()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());

        int? deptcode = null; if (DDLOfficeDept.SelectedValue != "0") deptcode = Int32.Parse(DDLOfficeDept.SelectedValue);
        int? year = null; if (ddlYear.SelectedValue != "0") year = Int32.Parse(ddlYear.SelectedValue.ToString());
        int? month = null; if (ddlMonth.SelectedValue != "0") month = Int32.Parse(ddlMonth.SelectedValue.ToString());


        int? assigndeptstatus = null; if (optAssignDept.SelectedValue != "2") assigndeptstatus = Int32.Parse(optAssignDept.SelectedValue);
        int? smsnextreview = null; if (optSMSReview.SelectedValue != "2") smsnextreview = Int32.Parse(optSMSReview.SelectedValue);
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataSet ds = BLL_SCM_Report.SCMReportIssueAssignmentSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(vesselcode)
            , null, deptcode, year, month, txtSearchBy.Text.Trim(), assigndeptstatus, smsnextreview
            , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvIssueAssign.DataSource = ds.Tables[0];
            gvIssueAssign.DataBind();

            if (ViewState["ID"] == null)
            {
                ViewState["ID"] = ds.Tables[0].Rows[0]["ResponseID"].ToString();
                //gvIssueAssign.SelectedIndex = 0;
            }

            // SetRowSelection();
        }
        else
        {
            gvIssueAssign.DataSource = ds.Tables[0];
            gvIssueAssign.DataBind();
        }
    }

    private void SetRowSelection()
    {
        gvIssueAssign.SelectedIndex = -1;
        for (int i = 0; i < gvIssueAssign.Rows.Count; i++)
        {
            if (gvIssueAssign.DataKeys[i].Value.ToString().Equals(ViewState["ID"].ToString()))
            {
                gvIssueAssign.SelectedIndex = i;
            }
        }
    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        ucCustomPagerItems.isCountRecord = 1;
        string vesselcode = DDLVessel.SelectedValue.ToString();

        ViewState["VesselCode"] = DDLVessel.SelectedValue.ToString();

        ViewState["DeptCode"] = DDLOfficeDept.SelectedValue.ToString();

        BindSCMIssueAssignmentSearch();
        UpdPnlGrid.Update();

    }



    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

        ViewState["VesselCode"] = 0;

        ViewState["DeptCode"] = null;

        ViewState["SORTDIRECTION"] = null;
        ViewState["SORTBYCOLOUMN"] = null;

        BindFleetDLL();
        BindVesselDDL();

        FillDDLOfficeDept(); 
        

        optSMSReview.SelectedValue = "2";
        optAssignDept.SelectedValue = "2";

        txtSearchBy.Text = "";
        BindFleetDLL();
        DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();   
      
        FillDDLYear();
        SetPreviousMonthYear();

        ucCustomPagerItems.PageSize = 15;
        BindSCMIssueAssignmentSearch();
        UpdPnlGrid.Update();
    }

    protected void DDLOfficeDept_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {

        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

        DDLVessel.Items.Clear();
        DataTable dtVessel = objVsl.GetVesselsByFleetID(int.Parse(DDLFleet.SelectedValue.ToString()));
        DDLVessel.DataSource = dtVessel;
        DDLVessel.DataTextField = "Vessel_name";
        DDLVessel.DataValueField = "Vessel_ID";
        DDLVessel.DataBind();
        ListItem li = new ListItem("--SELECT ALL--", "0");
        DDLVessel.Items.Insert(0, li);

    }

    protected void gvIssueAssign_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.color='blue';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
        }
    }

    protected void gvIssueAssign_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblVslIssueTooltip = (Label)e.Row.FindControl("lblVslIssueTooltip");
            Label lblOfficeResponseTooltip = (Label)e.Row.FindControl("lblOfficeResponseTooltip");
            Label lblVesselIssue = (Label)e.Row.FindControl("lblVesselIssue");
            Label lblVesselIssueFullDetails = (Label)e.Row.FindControl("lblVesselIssueFullDetails");
            Label lblReleaseFlag = (Label)e.Row.FindControl("lblReleaseFlag");
            Label lblDeptID = (Label)e.Row.FindControl("lblDeptID");
            Label lblCreatedByID = (Label)e.Row.FindControl("lblCreatedByID");
            Label lblCreatedBy = (Label)e.Row.FindControl("lblCreatedBy");

            CheckBox chk = (CheckBox)e.Row.FindControl("ChkSMSReview");

            Label lblSmsReviewFlag = (Label)e.Row.FindControl("lblSmsReviewFlag");


            if (lblSmsReviewFlag.Text == "1")
                chk.Checked = true;
            else
                chk.Checked = false;

            RadioButtonList rdodept = (RadioButtonList)e.Row.FindControl("optDepartment");

            if (lblDeptID.Text != "")
            {
                rdodept.SelectedValue = lblDeptID.Text;
                chk.Enabled = true;
                // rdodept.SelectedItem.Attributes.Add("onclick", "this.style.backgroundColor='red';this.style.color='#006699'");
            }
            else
            {
                chk.Enabled = false;
            }

            //lblCreatedByID.Attributes.Add("onmouseover", "this.style.cursor='hand'");
            lblCreatedBy.Attributes.Add("onclick", "window.open('/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/Crew/CrewDetails.aspx?ID=" + lblCreatedByID.Text.Trim() + "');");



            //e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.color='blue';";
            //if (lblVesselIssueFullDetails.Text.Length > 100)
            //    lblVesselIssue.Text = lblVesselIssue.Text + "...";


            //lblVesselIssue.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Vessel Issue] body=[" + lblVslIssueTooltip.Text + "]");

            string tootips = "";

            foreach (ListItem item in rdodept.Items)
            {
                if (item.Value == "1")
                    tootips = "Admin";
                else if (item.Value == "2")
                    tootips = "Crew";
                else if (item.Value == "3")
                    tootips = "Purchasing";
                else if (item.Value == "4")
                    tootips = "Operations";
                else if (item.Value == "5")
                    tootips = "Technical";
                else if (item.Value == "6")
                    tootips = "IT";
                else if (item.Value == "7")
                    tootips = "SQA";
                else if (item.Value == "8")
                    tootips = "HR";
                else if (item.Value == "9")
                    tootips = "Chartering";
                else if (item.Value == "10")
                    tootips = "Management";
                else if (item.Value == "11")
                    tootips = "Accounts";
                else if (item.Value == "12")
                    tootips = "Manning Office";


                // item.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[" + tootips + "]");
                // item.Attributes.Add("onmouseover", "js_ShowToolTip('<span style=&#39;padding:20px&#39;>" + tootips + "</span>',event,this)");

                item.Attributes.Add("onmouseover", "js_ShowToolTip('&nbsp;&nbsp;&nbsp;" + tootips + "&nbsp;&nbsp;&nbsp;',event,this)");
                item.Attributes.Add("onclick", "this.style.backgroundColor='red';this.style.color='#006699'");

            }


        }

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

    }

    protected void gvIssueAssign_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {

        gvIssueAssign.SelectedIndex = se.NewSelectedIndex;


    }

    protected void gvIssueAssign_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindSCMIssueAssignmentSearch();
        //SetRowSelection();
    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());

        int? deptcode = null; if (DDLOfficeDept.SelectedValue != "0") deptcode = Int32.Parse(DDLOfficeDept.SelectedValue);
        int? year = null; if (ddlYear.SelectedValue != "0") year = Int32.Parse(ddlYear.SelectedValue.ToString());
        int? month = null; if (ddlMonth.SelectedValue != "0") month = Int32.Parse(ddlMonth.SelectedValue.ToString());


        int? assigndeptstatus = null; if (optAssignDept.SelectedValue != "2") assigndeptstatus = Int32.Parse(optAssignDept.SelectedValue);
        int? smsnextreview = null; if (optSMSReview.SelectedValue != "2") smsnextreview = Int32.Parse(optSMSReview.SelectedValue);
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());



        DataSet ds = BLL_SCM_Report.SCMReportIssueAssignmentSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(vesselcode)
            , null, deptcode, year, month, txtSearchBy.Text.Trim(), assigndeptstatus, smsnextreview
            , sortbycoloumn, sortdirection, null, null, ref  rowcount);


        string[] HeaderCaptions = { "Vessel", "Month", "Year", "Assign Dept.", "SCM Tab", "Vessel Issue", "Created By", "SMS Review" };
        string[] DataColumnsName = { "Vessel_Name", "Month", "Year", "Department", "TAB_NAME", "VESSEL_ISSUE", "Issue_Created_By", "SMS_NEXT_REVIEW" };

        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "Vessel Issue Assignment", "Vessel Issue Assignment", HtmlFilterTable());
    }

    protected string HtmlFilterTable()
    {
        StringBuilder HtmlFilterTable = new StringBuilder();

        //HtmlFilterTable.Append("<table border='1' cellpadding='2' cellpadding='0' width='100%'>");
        //HtmlFilterTable.Append("<tr style='background-color: #F2F2F2;'><td>");
        //HtmlFilterTable.Append("<b>Filters</b></td></tr>");
        //HtmlFilterTable.Append("<tr><td>");

        //HtmlFilterTable.Append("<table  border='0'  cellpadding='0' cellspacing='1' width='100%' style='color: Black;'>");

        //HtmlFilterTable.Append("<tr>");
        //HtmlFilterTable.Append("<td align='right'> Fleet:&nbsp;&nbsp;       </td> <td align='left'>" + DDLFleet.SelectedItem.Text + "</td>");
        //HtmlFilterTable.Append("<td align='right'> From:&nbsp;&nbsp;        </td> <td align='left'>" + txtFromDate.Text + "</td>");
        //HtmlFilterTable.Append("</tr>");

        //HtmlFilterTable.Append("<tr>");
        //HtmlFilterTable.Append("<td align='right'> Vessel :&nbsp;&nbsp;     </td> <td align='left'>" + DDLVessel.SelectedItem.Text + "</td>");
        //HtmlFilterTable.Append("<td align='right'> To:&nbsp;&nbsp;          </td> <td align='left'>" + txtToDate.Text + "</td>");
        //HtmlFilterTable.Append("</tr>");

        //HtmlFilterTable.Append("</table>");

        //HtmlFilterTable.Append("</td></tr>");
        //HtmlFilterTable.Append("</table>");

        return HtmlFilterTable.ToString();
    }

    protected void optDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((RadioButtonList)sender).Parent.Parent;

        IssueAssignToDepartment(((RadioButtonList)gvr.FindControl("optDepartment")).SelectedValue, ((Label)gvr.FindControl("lblSCMTab")).Text, ((Label)gvr.FindControl("lblResponseID")).Text
                                    , ((Label)gvr.FindControl("lblTabIssueID")).Text
                                  , ((Label)gvr.FindControl("lblVesselID")).Text, ((Label)gvr.FindControl("lblLink")).Text);


        BindSCMIssueAssignmentSearch();

        UpdPnlGrid.Update();

    }

    public void IssueAssignToDepartment(string deptid, string tabname, string strresponseid, string tabpkid, string vesselid, string link)
    {

        int? responseid = null;
        if (strresponseid != "")
            responseid = Convert.ToInt32(strresponseid);


        int retval = BLL_SCM_Report.SCMReportOfficeIssueAssigned(Convert.ToInt32(Session["userid"].ToString())
                                                                , Convert.ToInt32(deptid), tabname
                                                                , responseid, Convert.ToInt32(tabpkid)
                                                                , Convert.ToInt32(vesselid), Convert.ToInt32(link));


    }


    protected void btnRqstUptResponse_Click(object sender, EventArgs e)
    {

        BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
        BLL_Infra_UserCredentials ojbInfra = new BLL_Infra_UserCredentials();

        string sToEmailAddress = "", strEmailAddCc = "", strFormatSubject = "Update response";

        DataTable dtEmailAdd = BLL_SCM_Report.SCMReportGetEmailAddToSendMailForResponse(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue));

        foreach (DataRow dr in dtEmailAdd.Rows)
        {
            sToEmailAddress += dr["EMAIL_ADD"].ToString() + ";";
        }

        if (dtEmailAdd.Rows.Count > 0)
        {

            DataTable dtRequesterDetails = ojbInfra.Get_UserDetails(Convert.ToInt32(Session["userid"].ToString()));

            StringBuilder sbEmailbody = new StringBuilder();
            string path = System.Configuration.ConfigurationManager.AppSettings["APP_URL"].ToString() + "/QMS/SCM/SCM_Response.aspx";
            //string path = "http://seachange.dyndns.info/smslog/QMS/SCM/SCM_Response.aspx";

            sbEmailbody.Append("Dear All,");
            sbEmailbody.AppendLine("<br><br>");
            sbEmailbody.AppendLine("Please click on below link to update the response.");
            sbEmailbody.Append("<a href=" + path + ">" + path + "</a>");
            sbEmailbody.AppendLine("<br><br>");
            sbEmailbody.AppendLine("<br>");
            sbEmailbody.AppendLine("<br>");
            sbEmailbody.AppendLine("Best Regards,");
            sbEmailbody.AppendLine("<br>");
            sbEmailbody.AppendLine(dtRequesterDetails.Rows[0]["User_name"].ToString().ToUpper() + " " + dtRequesterDetails.Rows[0]["Last_Name"].ToString().ToUpper());
            sbEmailbody.AppendLine("<br>");
            sbEmailbody.AppendLine(dtRequesterDetails.Rows[0]["Designation"].ToString());
            sbEmailbody.AppendLine(Convert.ToString(Session["Company_Address_GL"]));
            sbEmailbody.AppendLine("<br>");
            

            int MailID = objBLLCrew.Send_CrewNotification(0, 0, 0, 0, sToEmailAddress, strEmailAddCc, "", strFormatSubject, sbEmailbody.ToString(), "", "MAIL", "", UDFLib.ConvertToInteger(Session["USERID"].ToString()), "DRAFT");

            string URL = String.Format("window.open('/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/crew/EmailEditor.aspx?ID=+" + MailID.ToString() + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "k" + MailID.ToString(), URL, true);

        }
    }

    protected void lnkCheckAll_Click(object sender, EventArgs e)
    {

        int rowscount = gvIssueAssign.Rows.Count;
        for (int i = 0; i < rowscount; i++)
        {
            CheckBox chk = (CheckBox)gvIssueAssign.Rows[i].FindControl("ChkSMSReview");
            Label lblDeptID = (Label)gvIssueAssign.Rows[i].FindControl("lblDeptID");

            /* If Department is assign then only check box should select*/
            if (lblDeptID.Text != "")
            {
                chk.Checked = true;
            }
        }

        UpdPnlGrid.Update();

    }


    protected void lnkUnChekAll_Click(object sender, EventArgs e)
    {

        int rowscount = gvIssueAssign.Rows.Count;

        for (int i = 0; i < rowscount; i++)
        {

            CheckBox chk = (CheckBox)gvIssueAssign.Rows[i].FindControl("ChkSMSReview");
            Label lblDeptID = (Label)gvIssueAssign.Rows[i].FindControl("lblDeptID");

            /* If Department is assign then only check box should De select*/
            if (lblDeptID.Text != "")
            {
                chk.Checked = false;
            }

        }

        UpdPnlGrid.Update();

    }


    protected void btnUnMarkSMSReview_Click(object sender, EventArgs e)
    {

        int rowscount = gvIssueAssign.Rows.Count;
        int? responseid = null;
        int? smsreviewflag = null;


        for (int i = 0; i < rowscount; i++)
        {

            CheckBox chk = (CheckBox)gvIssueAssign.Rows[i].FindControl("ChkSMSReview");
            Label lblResponse = (Label)gvIssueAssign.Rows[i].FindControl("lblResponseID");

            if (chk.Checked)
            {
                if (lblResponse.Text != "")
                {
                    responseid = Convert.ToInt32(lblResponse.Text);
                    smsreviewflag = 1;
                    BLL_SCM_Report.SCMReportSMSReviewFlagUpdate(responseid, smsreviewflag);
                }
            }
            else
            {
                if (lblResponse.Text != "")
                {
                    responseid = Convert.ToInt32(lblResponse.Text);
                    smsreviewflag = null;
                    BLL_SCM_Report.SCMReportSMSReviewFlagUpdate(responseid, smsreviewflag);
                }

            }

        }
    }

}