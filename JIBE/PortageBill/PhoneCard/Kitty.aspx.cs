using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SMS.Business.PortageBill;
using System.Data;
using SMS.Business.Infrastructure;
using System.Text;

public partial class PortageBill_PhoneCard_Kitty : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");

        calFromDate.Format = calToDate.Format = UDFLib.GetDateFormat();

        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;
            ucCustomPagerItems.PageSize = 20;
            Session["sFleet"] = DDLFleet.SelectedValues;
            Session["sVesselCode"] = DDLVessel.SelectedValues;
            btnUpload.Attributes.Add("onclick", "javascript:window.open('../PhoneCard/KittyUpload.aspx'); return false;");
            FillDDL();
            txtFromDate.Text = UDFLib.ConvertUserDateFormat((DateTime.Now.AddDays(-(DateTime.Now.DayOfYear) + 1)).ToShortDateString());
            txtToDate.Text = UDFLib.ConvertUserDateFormat((DateTime.Now.AddMonths(1).AddDays(-(DateTime.Now.Day)).ToShortDateString()));
            BindKitty();

            Load_VesselList();
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
    public void BindKitty()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = BLL_PB_PhoneCard.PhoneCord_Kitty_Search(txtSatatus.Text != "" ? txtSatatus.Text : null, null, txtTitle.Text != "" ? txtTitle.Text : null, null, null, null, (DataTable)Session["sVesselCode"], UDFLib.ConvertUserDateFormat(txtFromDate.Text), UDFLib.ConvertUserDateFormat(txtToDate.Text), sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (dt.Rows.Count > 0)
            {
                gvPhoneCardKitty.DataSource = dt;
                gvPhoneCardKitty.DataBind();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void gvPhoneCardKitty_RowDataBound(object sender, GridViewRowEventArgs e)
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
        }


    }
    protected void gvPhoneCardKitty_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["SORTBYCOLOUMN"] = e.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindKitty();
    }
    protected void OnViewRequest(object source, CommandEventArgs e)
    {
        //ResponseHelper.Redirect("VesselRequestDetails.aspx?ID=" + e.CommandArgument.ToString(), "", ""); ;
    }
    protected void DDLVessel_SelectedIndexChanged()
    {
        Session["sVesselCode"] = DDLVessel.SelectedValues;
        BindKitty();
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
        BindKitty();
    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            //txtFromDate.Text = "";
            //txtToDate.Text = "";
            txtFromDate.Text = UDFLib.ConvertUserDateFormat((DateTime.Now.AddDays(-(DateTime.Now.DayOfYear) + 1)).ToShortDateString());
            txtToDate.Text = UDFLib.ConvertUserDateFormat((DateTime.Now.AddMonths(1).AddDays(-(DateTime.Now.Day)).ToShortDateString()));
            DDLVessel.ClearSelection();
            txtSatatus.Text = "";
            txtTitle.Text = "";
            DDLFleet.ClearSelection();

            BindKitty();
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

            DataTable dt = BLL_PB_PhoneCard.PhoneCord_Kitty_Export(txtSatatus.Text != "" ? txtSatatus.Text : null, null, txtTitle.Text != "" ? txtTitle.Text : null, null, null, null, (DataTable)Session["sVesselCode"], UDFLib.ConvertToDate(txtFromDate.Text.Trim(), UDFLib.GetDateFormat()).ToShortDateString(), UDFLib.ConvertToDate(txtToDate.Text.Trim(),UDFLib.GetDateFormat()).ToShortDateString(), sortbycoloumn, sortdirection
               , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize);

            for (int i = 0; i < dt.Rows.Count; i++)
                dt.Rows[i]["DATE_OF_CREATION"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[i]["DATE_OF_CREATION"]));

            string[] HeaderCaptions = { "Vessel Name", "Request Date", "Card Number", "Card Pin", "Unit", "Title", "Sub Title", "Supplier ", "Voyage" };
            string[] DataColumnsName = { "VESSEL_NAME", "DATE_OF_CREATION", "CARD_NUM", "CARD_PIN", "CARD_UNITS", "TITLE", "SUBTITLE", "Full_NAME", "STAFF_NAME" };

            GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "KittyList", "Kitty List");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ResponseHelper.Redirect("KittyUpload.aspx", "", "");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        txtCardNumber.Text = "";
        txtPin.Text = "";
        txtUnit.Text = "";
        txtDetail.Text = "";
        txtSubDetail.Text = "";
        ddlVesselAdd.SelectedIndex = 0;
        SupplierList.SelectedValue = "0";
        string Deptmodal = String.Format("showModal('divUploadCard',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divUploadCard", Deptmodal, true);
    }
    protected void btnSaveEventEdit_Click(object sender, EventArgs e)
    {
        try
        {
            int vesselid = int.Parse(ddlVesselAdd.SelectedValue);
            int res = BLL_PB_PhoneCard.PhoneCard_Kitty_Insert(int.Parse(txtCardNumber.Text), txtPin.Text, int.Parse(txtUnit.Text), txtDetail.Text, txtSubDetail.Text, "", 1, vesselid, UDFLib.ConvertIntegerToNull(SupplierList.SelectedValue));

            //string js = "alert('Query Saved !!');hideModal('DivSendForApp');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
            string Deptmodal = String.Format("alert('Data Saved !!');hideModal('divUploadCard',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divUploadCard", Deptmodal, true);
            BindKitty();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnCloseEventEdit_Click(object sender, EventArgs e)
    {
        string Deptmodal = String.Format("hideModal('divUploadCard',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divUploadCard", Deptmodal, true);
    }

    public void Load_VesselList()
    {
        try
        {
            BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
            int Fleet_ID = 0;
            int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

            ddlVesselAdd.DataSource = objBLLVessel.Get_VesselList(Fleet_ID, 0, UserCompanyID, "", UserCompanyID);

            ddlVesselAdd.DataTextField = "VESSEL_NAME";
            ddlVesselAdd.DataValueField = "VESSEL_ID";
            ddlVesselAdd.DataBind();
            ddlVesselAdd.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
            ddlVesselAdd.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

}