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



public partial class PMSMachineryChangeRequest : System.Web.UI.Page
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

                ViewState["System_ID"] = null;
                ViewState["IsNewFlage"] = null;

                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                ViewState["Status"] = null;

                optStatus.SelectedValue = "P";
                BindMachineryChageRequests();

            }

            objChangeReqstMerge.AddMergedColumns(new int[] { 2, 3, 4, 5 }, "ORIGINAL", "HeaderStyle-css");
            objChangeReqstMerge.AddMergedColumns(new int[] { 6, 7, 8, 9 }, "CHANGE REQUEST", "HeaderStyle-css");

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    /// <summary>
    /// To check access rights of user for requested page
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
    /// To fill details of machinaery change request to grid view
    /// </summary>
    public void BindMachineryChageRequests()
    {
        BLL_PMS_Change_Request objChangeRqst = new BLL_PMS_Change_Request();

        int rowcount = ucCustomPagerItems.isCountRecord;


        int? vesselcode = null;
        if (ViewState["VesselCode"] != null && (string)ViewState["VesselCode"] != "0")
            vesselcode = Convert.ToInt32(ViewState["VesselCode"].ToString());


        string deptcode = (ViewState["DeptCode"] == null) ? null : (ViewState["DeptCode"].ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null;

        if (ViewState["SORTDIRECTION"] != null)
            sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        int? status = null;


        if (optStatus.SelectedValue == "P")
            status = 2;
        else if (optStatus.SelectedValue == "A")
            status = 1;
        else if (optStatus.SelectedValue == "R")
            status = 0;

        DataSet ds = objChangeRqst.TecMachineryChangeRequestSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
              , txtSearchSystem.Text.Trim() != "" ? txtSearchSystem.Text : null, status, null, null, UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text)
              , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvMachinery.DataSource = ds.Tables[0];
            gvMachinery.DataBind();
        }
        else
        {
            gvMachinery.DataSource = ds.Tables[0];
            gvMachinery.DataBind();
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

    protected void gvMachinery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblDiffSysDescFlag = (Label)e.Row.FindControl("lblDiffSysDescFlag");
                Label lblDiffSysParticularFlag = (Label)e.Row.FindControl("lblDiffSysPartFlag");
                Label lblDiffSetInstalledFlag = (Label)e.Row.FindControl("lblDiffSetInstalledFlag");
                Label lblDiffModelFlag = (Label)e.Row.FindControl("lblDiffModelFlag");
                Label lblSystemParticular = (Label)e.Row.FindControl("lblSystemParticular");
                Label lblCRSystemParticular = (Label)e.Row.FindControl("lblCRSystemParticular");
                Label lblSystemName = (Label)e.Row.FindControl("lblSystemName");
                Label lblCRSystemName = (Label)e.Row.FindControl("lblCRSystemName");
                Label lblAddNewFlag = (Label)e.Row.FindControl("lblAddNewFlag");
                Label lbl_CR_Actual = (Label)e.Row.FindControl("lbl_CR_Actual");
                Label lblChangeReqstID = (Label)e.Row.FindControl("lblChangeReqstID");
                Label lblVesselCode = (Label)e.Row.FindControl("lblVesselCode");



                lblSystemName.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Particulars] body=[" + lblSystemParticular.Text + "]");
                if (lbl_CR_Actual.Text != "")
                    lblCRSystemName.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Change Reqst Actual] body=[" + lbl_CR_Actual.Text + "]");


                if (lblDiffSysDescFlag.Text == "N")
                {
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Blue;

                }
                if (lblDiffSetInstalledFlag.Text == "N")
                {
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.Blue;

                }
                if (lblDiffModelFlag.Text == "N")
                {
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[9].ForeColor = System.Drawing.Color.Blue;

                }

                if (lblAddNewFlag.Text == "Y")
                {

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
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;
            BLL_PMS_Change_Request objChangeRqst = new BLL_PMS_Change_Request();

            int? vesselcode = null;
            if (ViewState["VesselCode"] != null && (string)ViewState["VesselCode"] != "0")
                vesselcode = Convert.ToInt32(ViewState["VesselCode"].ToString());

            string deptcode = (ViewState["DeptCode"] == null) ? null : (ViewState["DeptCode"].ToString());

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null;

            if (ViewState["SORTDIRECTION"] != null)
                sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            int? status = null;

            if (optStatus.SelectedValue == "ALL")
                ViewState["Status"] = -1;
            else if (optStatus.SelectedValue == "P")
                ViewState["Status"] = null;
            else if (optStatus.SelectedValue == "A")
                ViewState["Status"] = 1;
            else if (optStatus.SelectedValue == "R")
                ViewState["Status"] = 0;


            DataSet ds = objChangeRqst.TecMachineryChangeRequestSearch(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
                  , txtSearchSystem.Text.Trim() != "" ? txtSearchSystem.Text : null, status, null, null, null, null, sortbycoloumn, sortdirection, null, null, ref  rowcount);


            string[] HeaderCaptions = { "ID", "Vessel", "Machinery", "Maker", "Sets", "Model", "CR Machinery", "CR Maker", "CR Sets", "CR Model", "Status", "Actioned By", "Actioned On" };
            string[] DataColumnsName = { "ID", "Vessel_Name", "System_Description", "Maker", "Set_Installed", "Model", "CR_System_Description", "CR_Maker", "CR_Set_Installed", "CR_Model", "Status", "ACTIONEDBY", "CR_Actioned_On" };

            GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "MachineryChangeRequest", "Machinery Change Request", HtmlFilterTable());
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// to format data in html format when export to excel
    /// </summary>
    /// <returns></returns>
    protected string HtmlFilterTable()
    {
        StringBuilder HtmlFilterTable = new StringBuilder();

        HtmlFilterTable.Append("<table border='1' CELLPADDING='2' CELLSPACING='0' width='100%'>");
        HtmlFilterTable.Append("<tr style='background-color: #F2F2F2;'><td>");
        HtmlFilterTable.Append("<b>Filters</b></td></tr>");
        HtmlFilterTable.Append("<tr><td>");

        HtmlFilterTable.Append("<table  border='0'  cellpadding='0' cellspacing='1' width='100%' style='color: Black;'>");

        HtmlFilterTable.Append("<tr>");
        HtmlFilterTable.Append("<td align='right'> Fleet:&nbsp;&nbsp;       </td> <td align='left'>" + DDLFleet.SelectedItem.Text + "</td>");
        HtmlFilterTable.Append("<td align='right'> System Name:&nbsp;&nbsp; </td> <td align='left'>" + txtSearchSystem.Text + "</td>");
        HtmlFilterTable.Append("<td align='right'> From:&nbsp;&nbsp;        </td> <td align='left'>" + txtFromDate.Text + "</td>");
        HtmlFilterTable.Append("</tr>");

        HtmlFilterTable.Append("<tr>");
        HtmlFilterTable.Append("<td align='right'> Vessel :&nbsp;&nbsp;     </td> <td align='left'>" + DDLVessel.SelectedItem.Text + "</td>");
        HtmlFilterTable.Append("<td align='right'> Status :&nbsp;&nbsp;     </td> <td align='left'>" + optStatus.SelectedItem.Text + "</td>");
        HtmlFilterTable.Append("<td align='right'> To :&nbsp;&nbsp;         </td> <td align='left'>" + txtToDate.Text + "</td>");
        HtmlFilterTable.Append("</tr>");

        HtmlFilterTable.Append("</table>");

        HtmlFilterTable.Append("</td></tr>");
        HtmlFilterTable.Append("</table>");

        return HtmlFilterTable.ToString();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            ucCustomPagerItems.isCountRecord = 1;

            string vesselcode = DDLVessel.SelectedValue.ToString();

            ViewState["VesselCode"] = DDLVessel.SelectedValue.ToString();


            BindMachineryChageRequests();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void gvMachinery_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;

            BindMachineryChageRequests();
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
            txtSearchSystem.Text = "";
            ViewState["Status"] = null;
            optStatus.SelectedValue = "P";
            txtFromDate.Text = "";
            txtToDate.Text = "";

            DDLFleet.SelectedValue = "0";
            BindVesselDDL();

            ViewState["VesselCode"] = null;
            BindMachineryChageRequests();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

}