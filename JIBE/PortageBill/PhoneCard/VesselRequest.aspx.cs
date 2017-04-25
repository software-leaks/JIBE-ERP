using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using SMS.Business.PortageBill;
using System.Text;
using SMS.Business.Infrastructure;

public partial class PortageBill_PhoneCard_VesselRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");

        calFromDate.Format = calToDate.Format = UDFLib.GetDateFormat();

        try
        {
            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;
                //ucCustomPagerItems.PageSize = 20;
                Session["sFleet"] = DDLFleet.SelectedValues;
                Session["sVesselCode"] = DDLVessel.SelectedValues;
                FillDDL();
                txtFromDate.Text = UDFLib.ConvertUserDateFormat((DateTime.Now.AddDays(-(DateTime.Now.DayOfYear) + 1)).ToShortDateString());
                txtToDate.Text = UDFLib.ConvertUserDateFormat((DateTime.Now.AddMonths(1).AddDays(-(DateTime.Now.Day)).ToShortDateString()));
                BindIndex();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    public void FillDDL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public void BindIndex()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = BLL_PB_PhoneCard.PhoneCord_Request_Search(txtCardNumber.Text != "" ? txtCardNumber.Text : null, (DataTable)Session["sVesselCode"], UDFLib.ConvertToDate(txtFromDate.Text).ToShortDateString(), UDFLib.ConvertToDate(txtToDate.Text).ToShortDateString(), ddlStatus.SelectedIndex == 0 ? null : ddlStatus.SelectedValue, sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            gvPhoneCardRequest.DataSource = dt;
            gvPhoneCardRequest.DataBind();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void gvPhoneCardRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/Images/arrowUp.png";
                    else
                        img.Src = "~/Images/arrowDown.png";

                    img.Visible = true;
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
            LinkButton lbtnRequestNumber = (LinkButton)e.Row.FindControl("lbtnRequestNumber");
            string requestid = lbtnRequestNumber.CommandArgument.ToString().Split(',')[0].ToString();
            string vesselid = lbtnRequestNumber.CommandArgument.ToString().Split(',')[1].ToString();
            lbtnRequestNumber.Attributes.Add("onclick", "javascript:window.open('../PhoneCard/VesselRequestDetails.aspx?ID=" + requestid + "&VESSELID=" + vesselid + "'); return false;");
        }
    }
    protected void gvPhoneCardRequest_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["SORTBYCOLOUMN"] = e.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindIndex();
    }
    protected void OnViewRequest(object source, CommandEventArgs e)
    {
        ResponseHelper.Redirect("VesselRequestDetails.aspx?ID=" + e.CommandArgument.ToString().Split(',')[0].ToString() + "&VESSELID=" + e.CommandArgument.ToString().Split(',')[1].ToString(), "", "");
    }
    protected void UpdateStatus(object source, CommandEventArgs e)
    {
        try
        {
            BLL_PB_PhoneCard.PhoneCord_Request_UpdateStatus(int.Parse(e.CommandArgument.ToString().Split(',')[0].ToString()), int.Parse(e.CommandArgument.ToString().Split(',')[1].ToString()), int.Parse(Session["USERID"].ToString()));
            //string js = "alert('Query Saved !!');hideModal('DivSendForApp');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
            string js = "alert('eChat card has been uploaded for the requested staffs !!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
            BindIndex();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void DDLVessel_SelectedIndexChanged()
    {
        Session["sVesselCode"] = DDLVessel.SelectedValues;
    }
    protected void DDLFleet_SelectedIndexChanged()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            StringBuilder sbFilterFlt = new StringBuilder();
            string VslFilter = "";
            foreach (DataRow dr in DDLFleet.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            if (sbFilterFlt.Length > 1)
            {
                sbFilterFlt.Remove(sbFilterFlt.Length - 1, 1);
                VslFilter = string.Format("fleetCode in (" + sbFilterFlt.ToString() + ")");
                dtVessel.DefaultView.RowFilter = VslFilter;
            }

            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            Session["sVesselCode"] = DDLVessel.SelectedValues;
            Session["sFleet"] = DDLFleet.SelectedValues;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindIndex();
    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            txtCardNumber.Text = "";
            ddlStatus.SelectedValue = "-1";
            txtFromDate.Text = UDFLib.ConvertUserDateFormat((DateTime.Now.AddDays(-(DateTime.Now.DayOfYear) + 1)).ToShortDateString());
            txtToDate.Text = UDFLib.ConvertUserDateFormat((DateTime.Now.AddMonths(1).AddDays(-(DateTime.Now.Day)).ToShortDateString()));
            DDLVessel.ClearSelection();

            DDLFleet.ClearSelection();
            Session["sFleet"] = DDLFleet.SelectedValues;
            Session["sVesselCode"] = DDLVessel.SelectedValues;
            BindIndex();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void ImgExpExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = BLL_PB_PhoneCard.PhoneCord_Request_Export(txtCardNumber.Text != "" ? txtCardNumber.Text : null, (DataTable)Session["sVesselCode"], UDFLib.ConvertToDate(txtFromDate.Text).ToShortDateString(), UDFLib.ConvertToDate(txtToDate.Text).ToShortDateString(), ddlStatus.SelectedIndex == 0 ? null : ddlStatus.SelectedValue);

            UDFLib.ChangeColumnDataType(dt, "DATE_OF_CREATION", typeof(string));
            UDFLib.ChangeColumnDataType(dt, "PBILL_DATE", typeof(string));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["DATE_OF_CREATION"] = "&nbsp;"+ UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[i]["DATE_OF_CREATION"]));
                dt.Rows[i]["PBILL_DATE"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[i]["PBILL_DATE"])); 
            }

            string[] HeaderCaptions = { "Request Number", "Vessel Name", "Status", "Request Date", "Total Cards", "Bill Date" };
            string[] DataColumnsName = { "REQUEST_NUMBER", "VESSEL_NAME", "REQUEST_STATUS", "DATE_OF_CREATION", "TOTAL_REQUEST", "PBILL_DATE" };

            GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "PhoneCardRequest", "Request List");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}