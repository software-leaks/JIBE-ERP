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

public partial class QMS_SCM_SCM_Response : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

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

            DDLOfficeDept.SelectedValue = ViewState["DeptID"].ToString();

            if (ViewState["DeptID"].ToString() == "7")
            {
                btnRelease.Enabled = true;
            }
            else 
            {
                btnRelease.Enabled = false;
            }
 



             SetPreviousMonthYear();

            ucCustomPagerItems.PageSize =20;
            BindSCMResponseSearch();

      
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
            btnRelease.Enabled = false;

            lnkCheckAll.Enabled = false;
            lnkUnChekAll.Enabled = false;

        }

    }

   

    public void SetPreviousMonthYear()
    {

        if (ddlYear.Items.Count > 1)
            ddlYear.SelectedValue = DateTime.Now.ToString("yyyy");

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
                ddlYear.SelectedValue = DateTime.Now.ToString("yyyy");
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


    public void FillDivDDLOfficeDept()
    {

        int? companyid = null;
        if ((Session["USERCOMPANYID"].ToString() != "") || (Session["USERCOMPANYID"] == null))
            companyid = Convert.ToInt32(Session["USERCOMPANYID"].ToString());

        DataTable dtOfficeDept = BLL_SCM_Report.SCMGetOfficeDepartment(companyid);

        DivResponseDDLDeptpartment.Items.Clear();
        DivResponseDDLDeptpartment.DataSource = dtOfficeDept;
        DivResponseDDLDeptpartment.DataTextField = "VALUE";
        DivResponseDDLDeptpartment.DataValueField = "ID";
        DivResponseDDLDeptpartment.DataBind();
        ListItem lid = new ListItem("--SELECT ALL--", "0");
        DivResponseDDLDeptpartment.Items.Insert(0, lid);
    
    }


    public void FillDDLYear()
    {
        try
        {

            DataTable dtyears = BLL_SCM_Report.GetVesselIssuesYears();
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


    public void BindSCMResponseSearch()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());

        int? deptcode = null; if (DDLOfficeDept.SelectedValue != "0") deptcode = Int32.Parse(DDLOfficeDept.SelectedValue);
        int? year = null; if (ddlYear.SelectedValue != "0") year = Int32.Parse(ddlYear.SelectedValue.ToString());
        int? month = null; if (ddlMonth.SelectedValue != "0") month = Int32.Parse(ddlMonth.SelectedValue.ToString());

        int? smsnextreview = null; if (optSMSReview.SelectedValue != "2") smsnextreview = Int32.Parse(optSMSReview.SelectedValue);

        int? responsestatus = null; if (optResponseStatus.SelectedValue != "2") responsestatus = Int32.Parse(optResponseStatus.SelectedValue);

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());



        DataSet ds = BLL_SCM_Report.SCMReportOfficeResponseSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(vesselcode)
            , null, deptcode, year, month, txtSearchBy.Text.Trim(), smsnextreview,responsestatus
            , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvSCMResponse.DataSource = ds.Tables[0];
            gvSCMResponse.DataBind();

            if (ViewState["ID"] == null)
            {
                ViewState["ID"] = ds.Tables[0].Rows[0]["ResponseID"].ToString();
                //gvSCMResponse.SelectedIndex = 0;
            }

            // SetRowSelection();
        }
        else
        {
            gvSCMResponse.DataSource = ds.Tables[0];
            gvSCMResponse.DataBind();
        }
    }


    private void SetRowSelection()
    {
        gvSCMResponse.SelectedIndex = -1;
        for (int i = 0; i < gvSCMResponse.Rows.Count; i++)
        {
            if (gvSCMResponse.DataKeys[i].Value.ToString().Equals(ViewState["ID"].ToString()))
            {
                gvSCMResponse.SelectedIndex = i;
            }
        }
    }


    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        ucCustomPagerItems.isCountRecord = 1;
        string vesselcode = DDLVessel.SelectedValue.ToString();

        ViewState["VesselCode"] = DDLVessel.SelectedValue.ToString();

        ViewState["DeptCode"] = DDLOfficeDept.SelectedValue.ToString();

        BindSCMResponseSearch();
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


        DDLOfficeDept.SelectedValue = "0";


        optResponseStatus.SelectedValue = "1";
        optSMSReview.SelectedValue = "2";

        txtSearchBy.Text = "";

        BindSCMResponseSearch();
        UpdPnlGrid.Update();
    }


    protected void DDLOfficeDept_SelectedIndexChanged(object sender, EventArgs e)
    {


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


    protected void gvSCMResponse_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.color='blue';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
        }
    }


    protected void gvSCMResponse_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblVslIssueTooltip = (Label)e.Row.FindControl("lblVslIssueTooltip");
            Label lblOfficeResponseTooltip = (Label)e.Row.FindControl("lblOfficeResponseTooltip");
            Label lblVesselIssue = (Label)e.Row.FindControl("lblVesselIssue");
            Label lblVesselIssueFullDetails = (Label)e.Row.FindControl("lblVesselIssueFullDetails");
            Label lblDeptID = (Label)e.Row.FindControl("lblDeptID");
            Label lblCreatedByID = (Label)e.Row.FindControl("lblCreatedByID");
            Label lblCreatedBy = (Label)e.Row.FindControl("lblCreatedBy");

            ImageButton ImgVslIssue = (ImageButton)e.Row.FindControl("ImgVslIssue");
            ImageButton ImgOfficeResponse = (ImageButton)e.Row.FindControl("ImgOfficeResponse");

       
            CheckBox chk = (CheckBox)e.Row.FindControl("ChkSMSReview");
            Label lblSmsReviewFlag = (Label)e.Row.FindControl("lblSmsReviewFlag");


            if (lblSmsReviewFlag.Text == "1")
                chk.Checked = true;
            else
                chk.Checked = false;


            if (lblDeptID.Text != "")
            {
                chk.Enabled = true;
            }
            else
            {
                chk.Enabled = false;
            }



            //if (lblVesselIssueFullDetails.Text.Length > 100)
            //    lblVesselIssue.Text = lblVesselIssue.Text + "...";

            //lblVesselIssue.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Vessel Issue] body=[" + lblVslIssueTooltip.Text + "]");
            ImgVslIssue.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Vessel Issue] body=[" + lblVslIssueTooltip.Text + "]");
            ImgOfficeResponse.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[" + lblOfficeResponseTooltip.Text + "]");
            lblCreatedBy.Attributes.Add("onclick", "window.open('/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/Crew/CrewDetails.aspx?ID=" + lblCreatedByID.Text.Trim() + "');");




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


    protected void gvSCMResponse_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {
          
        gvSCMResponse.SelectedIndex = se.NewSelectedIndex;

        divResponsetxtOfficeResponse.Text = "";

        Label lblTabIssueID = ((Label)gvSCMResponse.Rows[se.NewSelectedIndex].FindControl("lblTabIssueID"));
        Label lblSCMTab = ((Label)gvSCMResponse.Rows[se.NewSelectedIndex].FindControl("lblSCMTab"));
        Label lblDeptID = ((Label)gvSCMResponse.Rows[se.NewSelectedIndex].FindControl("lblDeptID"));
        Label lblVesselID = ((Label)gvSCMResponse.Rows[se.NewSelectedIndex].FindControl("lblVesselID"));

        Label lblOfficeResponse = ((Label)gvSCMResponse.Rows[se.NewSelectedIndex].FindControl("lblOfficeResponse"));

        

        Label lblVesselIssueFullDetails = ((Label)gvSCMResponse.Rows[se.NewSelectedIndex].FindControl("lblVesselIssueFullDetails"));
        ViewState["ResponseID"] = ((Label)gvSCMResponse.Rows[se.NewSelectedIndex].FindControl("lblResponseID")).Text;

            
        
        FillDivDDLOfficeDept();
        DivResponseDDLDeptpartment.SelectedValue = lblDeptID.Text;
        divResponsetxtVesselIssue.Text = lblVesselIssueFullDetails.Text;
        divResponsetxtOfficeResponse.Text = lblOfficeResponse.Text;

        Label lblReleaseFlag = ((Label)gvSCMResponse.Rows[se.NewSelectedIndex].FindControl("lblReleaseFlag"));

        if (lblReleaseFlag.Text == "Y")
        {
            divResponsebtnResponse.Enabled = false;
            divResponsebtnModifyDept.Enabled = false;
        }
        else
        {
            divResponsebtnResponse.Enabled = true;
            divResponsebtnModifyDept.Enabled = true;
        }

        string msgdivResponseShow = string.Format("showModal('divResponse');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        UpdatePnlSCMResponse.Update();

    }


    protected void gvSCMResponse_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindSCMResponseSearch();
    }


    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());

        int? deptcode = null; if (DDLOfficeDept.SelectedValue != "0") deptcode = Int32.Parse(DDLOfficeDept.SelectedValue);
        int? year = null; if (ddlYear.SelectedValue != "0") year = Int32.Parse(ddlYear.SelectedValue.ToString());
        int? month = null; if (ddlMonth.SelectedValue != "0") month = Int32.Parse(ddlMonth.SelectedValue.ToString());
        int? smsnextreview = null; if (optSMSReview.SelectedValue != "2") smsnextreview = Int32.Parse(optSMSReview.SelectedValue);
        int? responsestatus = null; if (optResponseStatus.SelectedValue != "2") responsestatus = Int32.Parse(optResponseStatus.SelectedValue);

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataSet ds = BLL_SCM_Report.SCMReportOfficeResponseSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(vesselcode)
            , null, deptcode, year, month, txtSearchBy.Text.Trim(), smsnextreview, responsestatus
            , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        string[] HeaderCaptions = { "Vessel", "Month", "Year" ,"Department" ,"Tab Name" ,"Vessel Issue" ,"Created By" , "Office Response" };
        string[] DataColumnsName = { "Vessel_Name", "Month", "Year", "Dept_Name", "Tab_Name", "Vessel_Issue", "Issue_Created_By", "Office_Response" };

        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "SCM Office Response", "SCM Office Response", HtmlFilterTable());
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


    protected void btnRelease_Click(object sender, EventArgs e)
    {

        DataTable dtGetResponseRelease = BLL_SCM_Report.GetSCMReportResponseRelease();

        if (dtGetResponseRelease.Rows.Count > 0)
        {
            gvRelease.DataSource = dtGetResponseRelease;
            gvRelease.DataBind();

            string msgdiv = string.Format("showModal('divRelease');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdiv", msgdiv, true);

            UpdatePnlSCMRelease.Update();
        }
        else
        {
            string js = "alert('There is no response to be released!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
        }
    }


    protected void divReleasebtnOk_Click(object sender, EventArgs e)
    {
      

        int rowscount = gvRelease.Rows.Count;
        
        for (int i = 0; i < rowscount; i++)
        {

            CheckBox chk = (CheckBox)gvRelease.Rows[i].FindControl("chkRelease");
            Label lblmonth = (Label)gvRelease.Rows[i].FindControl("lblMonthNumber");
            Label lblyear = (Label)gvRelease.Rows[i].FindControl("lblReleaseYear");

           if (chk.Checked)
           {
               BLL_SCM_Report.SCMReportRealeaseResponseToShip(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(lblyear.Text), Convert.ToInt32(lblmonth.Text));
           }
           
        }     


        string msgDivReleaseHide = string.Format("hideModal('divRelease');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDivReleaseHide", msgDivReleaseHide, true);

        BindSCMResponseSearch();

        UpdPnlFilter.Update();
        UpdPnlGrid.Update();

    }


    protected void divResponsebtnResponse_Click(object sender, EventArgs e)
    {

        BLL_SCM_Report.SCMReportOfficeResponseSave(Convert.ToInt32(Session["userid"].ToString()) ,Convert.ToInt32(ViewState["ResponseID"].ToString()),divResponsetxtOfficeResponse.Text);

        string msgDivResponseHide = string.Format("hideModal('divResponse');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDivResponseHide", msgDivResponseHide, true);

        BindSCMResponseSearch();

        UpdPnlFilter.Update();
        UpdPnlGrid.Update();
    }


    protected void divResponsebtnModifyDept_Click(object sender, EventArgs e)
    {

        int? deptid = null;
        if (DivResponseDDLDeptpartment.SelectedValue != "0")
            deptid = Convert.ToInt32(DivResponseDDLDeptpartment.SelectedValue.ToString());

        BLL_SCM_Report.SCMReportOfficeDepartmentUpdate(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(ViewState["ResponseID"].ToString()),deptid);


        string msgDivResponseHide = string.Format("hideModal('divResponse');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDivResponseHide", msgDivResponseHide, true);


        BindSCMResponseSearch();

        UpdPnlFilter.Update();
        UpdPnlGrid.Update();
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

        int rowscount = gvSCMResponse.Rows.Count;
        for (int i = 0; i < rowscount; i++)
        {
            CheckBox chk = (CheckBox)gvSCMResponse.Rows[i].FindControl("ChkSMSReview");
            Label lblDeptID = (Label)gvSCMResponse.Rows[i].FindControl("lblDeptID");

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

        int rowscount = gvSCMResponse.Rows.Count;

        for (int i = 0; i < rowscount; i++)
        {

            CheckBox chk = (CheckBox)gvSCMResponse.Rows[i].FindControl("ChkSMSReview");
            Label lblDeptID = (Label)gvSCMResponse.Rows[i].FindControl("lblDeptID");

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

        int rowscount = gvSCMResponse.Rows.Count;
        int? responseid = null;
        int? smsreviewflag = null;


        for (int i = 0; i < rowscount; i++)
        {

            CheckBox chk = (CheckBox)gvSCMResponse.Rows[i].FindControl("ChkSMSReview");
            Label lblResponse = (Label)gvSCMResponse.Rows[i].FindControl("lblResponseID");

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