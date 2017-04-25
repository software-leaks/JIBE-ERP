using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ClsBLLTechnical;
using SMS.Business.PURC;
using Telerik.Web.UI;
using System.Data.SqlClient;
using SMS.Business.Infrastructure;


public partial class Technical_INV_SearchRequisitionStages : System.Web.UI.Page
{

    public DataTable dtQTNReceived = new DataTable();
    public DataTable dtQTNSent = new DataTable();
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();

    protected void Page_Load(object sender, EventArgs e)
    {
        TechnicalBAL objtechBAL = new TechnicalBAL();

        dtQTNReceived = objtechBAL.GetToopTipsForQtnRecve();
        dtQTNSent = objtechBAL.GetToopTipsForQtnSent();

        if (!IsPostBack)
        {
            BindFleetDLL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();
            BindDeptTypeOpt();
            BindDepartmentByST_SP();
            //BindPendingStatas();
            rgdReqAllStage.Attributes.Add("bordercolor", "#D8D8D8");
            txtSearchJoinFromDate.Text = (DateTime.Now.AddDays(-(DateTime.Now.DayOfYear) + 1)).ToString("dd-MM-yyyy");
            txtSearchJoinToDate.Text = (DateTime.Now.AddMonths(1).AddDays(-(DateTime.Now.Day))).ToString("dd-MM-yyyy");
            BindSearchRequistionstages();
            
            //txtSearchJoinFromDate.Text.Trim() == "" ? "1900/01/01" : txtSearchJoinFromDate.Text.Trim(),
            //txtSearchJoinToDate.Text.Trim() == "" ? "2099/01/01" : txtSearchJoinToDate.Text.Trim(),


        }
        objChangeReqstMerge.AddMergedColumns(new int[] { 2, 3, 4 }, "Requisition", "HeaderStyle-css");
        objChangeReqstMerge.AddMergedColumns(new int[] { 5, 6, 7, 8 }, "Quotation", "HeaderStyle-css");
        objChangeReqstMerge.AddMergedColumns(new int[] { 9, 10, 11, 12 }, "Purchase Order", "HeaderStyle-css");
        objChangeReqstMerge.AddMergedColumns(new int[] { 13, 14, 15 }, "Delivery", "HeaderStyle-css");
    }

    //private void BindPendingStatas()
    //{
    //    try
    //    {
    //        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
    //        {
    //            DataSet ds = new DataSet();

    //            DataTable dt = objTechService.GetRequisitionStageDetails(
    //                "1", "313", (DateTime.Now.AddDays(-(DateTime.Now.DayOfYear) + 1)).ToString("dd-MM-yyyy"), (DateTime.Now.AddMonths(1).AddDays(-(DateTime.Now.Day))).ToString("dd-MM-yyyy"));

    //            rgdReqAllStage.DataSource = dt;
    //            rgdReqAllStage.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
    //    }
    //    finally
    //    {

    //    }
    //}

    public void BindSearchRequistionstages()
    {

        string ddDept = null;
        if (cmbDept.SelectedIndex != 0)
            ddDept = UDFLib.ConvertStringToNull(cmbDept.SelectedValue.ToString());

        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();


        //DataTable dt = objTechService.GetRequisitionStageDetails(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue),
        //    (txtReqNo.Text.Trim() != "" ? txtReqNo.Text.Trim() : null),(txtPoNo.Text.Trim() != "" ? txtPoNo.Text.Trim() : null),ddDept,
        //    UDFLib.ConvertStringToNull(cmbUrgency.SelectedValue),UDFLib.ConvertIntegerToNull(txtTimeLapse.Text),UDFLib.ConvertIntegerToNull(ddlHoldUnhold.SelectedValue),
        //     UDFLib.ConvertDateToNull(txtSearchJoinFromDate.Text.Trim()),UDFLib.ConvertDateToNull(txtSearchJoinToDate.Text.Trim()),
        //     sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        DataTable dt = objTechService.GetRequisitionStageDetails(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue)
            , UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
            , txtReqNo.Text.Trim() != "" ? txtReqNo.Text.Trim() : null
            , txtPoNo.Text.Trim() != "" ? txtPoNo.Text.Trim() : null
            , ddDept, UDFLib.ConvertStringToNull(cmbUrgency.SelectedValue), UDFLib.ConvertIntegerToNull(txtTimeLapse.Text), UDFLib.ConvertIntegerToNull(ddlHoldUnhold.SelectedValue)
            , UDFLib.ConvertDateToNull(txtSearchJoinFromDate.Text.Trim()), UDFLib.ConvertDateToNull(txtSearchJoinToDate.Text.Trim())
            , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        //DataTable dt = objTechService.GetRequisitionStageDetails(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
        //    , txtReqNo.Text.Trim() != "" ? txtReqNo.Text.Trim() : null, txtPoNo.Text.Trim() != "" ? txtPoNo.Text.Trim() : null, ddDept
        //    , UDFLib.ConvertStringToNull(cmbUrgency.SelectedValue)
        //    , UDFLib.ConvertIntegerToNull(txtTimeLapse.Text), UDFLib.ConvertIntegerToNull(ddlHoldUnhold.SelectedValue)
        //    , UDFLib.ConvertDateToNull(txtSearchJoinFromDate.Text.Trim())
        //    , UDFLib.ConvertDateToNull(txtSearchJoinToDate.Text.Trim())
        //    , sortbycoloumn, sortdirection
        //    , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            rgdReqAllStage.DataSource = dt;
            rgdReqAllStage.DataBind();
        }
        else
        {
            rgdReqAllStage.DataSource = dt;
            rgdReqAllStage.DataBind();
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

    public void BindDeptTypeOpt()
    {

        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable DeptDt = objTechService.GetDeptType();
            optList.DataSource = DeptDt;
            optList.DataTextField = "Description";
            optList.DataValueField = "Short_Code";
            optList.SelectedIndex = 0;
            optList.DataBind();

        }

    }

    private void BindDepartmentByST_SP()
    {

        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtDepartment = new DataTable();
                dtDepartment = objTechService.SelectDepartment();
                if (optList.SelectedIndex != 0)
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";
                }
                cmbDept.Items.Clear();
                cmbDept.DataSource = dtDepartment;
                cmbDept.AppendDataBoundItems = true;
                cmbDept.Items.Add("--ALL--");
                cmbDept.DataTextField = "Name_Dept";
                cmbDept.DataValueField = "Code";
                cmbDept.DataBind();

            }

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {


        }

    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselDDL();
    }

    protected void optList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSearchRequistionstages();
            BindDepartmentByST_SP();
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
    }

    protected void rgdReqAllStage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);

            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
            

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = "PMSGridItemStyle-css";
            
            Label lbldocument_code = (Label)e.Row.FindControl("lblInfo");
            Label lblRFQSend = ((Label)e.Row.FindControl("lblRFQSend"));
            Label lblQuotReceived = ((Label)e.Row.FindControl("lblQuotReceived"));
            
            lblRFQSend.Attributes.Add("onmouseover", "Get_ToopTipsForQtnSent('" + lbldocument_code.Text.ToString() + "',event,this)");
            lblQuotReceived.Attributes.Add("onmouseover", "Get_ToopTipsForQtnRecve('" + lbldocument_code.Text.ToString() + "',event,this)");
            string Received = BindToolTipsSuppNameOnQtnRecvd("", ((Label)e.Row.FindControl("lblInfo")).ToolTip.Split(new char[] { ',' })[0], ((Label)e.Row.FindControl("lblInfo")).ToolTip.Split(new char[] { ',' })[1]);
            string Sent = BindToolTipsSuppNameOnQtnSent("", ((Label)e.Row.FindControl("lblInfo")).ToolTip.Split(new char[] { ',' })[0], ((Label)e.Row.FindControl("lblInfo")).ToolTip.Split(new char[] { ',' })[1]);
            //string[] st = Sent.Split(')');
            //string[] rcvd = Received.Split(')');
            lblQuotReceived.Text = Received;//(rcvd.Length - 1) >= 0 ? (rcvd.Length - 1).ToString() : "0";
            lblRFQSend.Text = Sent;//(st.Length - 1) >= 0 ? (st.Length - 1).ToString() : "0";
            if (Convert.ToInt32(lblQuotReceived.Text) > 0)
            {
                lblQuotReceived.Style.Add("text-decoration", "underline");
            }
            if (Convert.ToInt32(lblRFQSend.Text) > 0)
            {
                lblRFQSend.Style.Add("text-decoration", "underline");
            }
               
            
            //lblRFQSend.Attributes.Add("onclick", "Get_ToopTipsForQtnSent('" + lbldocument_code.Text.Split(new char[] { ',' })[0] + "',event,this)");
            //lblQuotReceived.Attributes.Add("onclick", "Get_ToopTipsForQtnRecve('" + lbldocument_code.Text.Split(new char[] { ',' })[0] + "',event,this)");

            //string Received = BindToolTipsSuppNameOnQtnRecvd("", ((Label)e.Row.FindControl("lblInfo")).ToolTip.Split(new char[] { ',' })[0], ((Label)e.Row.FindControl("lblInfo")).ToolTip.Split(new char[] { ',' })[1]);
            //string Sent = BindToolTipsSuppNameOnQtnSent("", ((Label)e.Row.FindControl("lblInfo")).ToolTip.Split(new char[] { ',' })[0], ((Label)e.Row.FindControl("lblInfo")).ToolTip.Split(new char[] { ',' })[1]);
            //Label lblsent = ((Label)e.Row.FindControl("lblRFQSend"));
            //Label lblrecvd = ((Label)e.Row.FindControl("lblQuotReceived"));
            //string[] st = Sent.Split(')');
            //string[] rcvd = Received.Split(')');
            //lblrecvd.Text = (rcvd.Length - 1) >= 0 ? (rcvd.Length - 1).ToString() : "0";
            //lblsent.Text = (st.Length - 1) >= 0 ? (st.Length - 1).ToString() : "0";
            //lblsent.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Quotation Sent To :] body=[" + Sent + "]");
            //lblrecvd.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Quotation Received From :] body=[" + Received + "]");
           

           
            
        }
    }

    protected void cmbDept_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cmbUrgency_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        BindSearchRequistionstages();
        //string VesselCode = DDLVessel.SelectedValue.ToString();
        //string sReqNo = (txtReqNo.Text.ToString() == "") ? "0" : txtReqNo.Text.ToString();
        //string sPoNo = (txtPoNo.Text.ToString() == "") ? "0" : txtPoNo.Text.ToString();
        //string sDoNo = (txtDoNo.Text.ToString() == "") ? "0" : txtDoNo.Text.ToString();
        //string sTimeLapse = (txtTimeLapse.Text.ToString() == "") ? "0" : txtTimeLapse.Text.ToString();
        //string sQutRef = (txtQutRef.Text.ToString() == "") ? "0" : txtQutRef.Text.ToString();
        //string sInvoiceNo = (txtInvoiceNo.Text.ToString() == "") ? "0" : txtInvoiceNo.Text.ToString();
        //string sFrom = txtfrom.Text.ToString();
        //string sTo = txtto.Text.ToString();
        //string sDeliveryType = (txtDeliveryType.Text.ToString() == "") ? "0" : txtDeliveryType.Text.ToString();
        //string sDeliveryStatus = (txtDeliveryStatus.Text.ToString() == "") ? "0" : txtDeliveryType.Text.ToString();
        //string scmbDept = cmbDept.SelectedValue.ToString();
        //string scmbUrgency = cmbUrgency.SelectedValue.ToString();
        //string soptList = optList.SelectedValue.ToString();
        //string sFleet = DDLFleet.SelectedValue;
        //string Filtersql = "";
        //string sHoldUnhold = ddlHoldUnhold.SelectedItem.ToString();

        //if (sReqNo != "0")
        //{
        //    Filtersql = Filtersql + " and requisition_code like '%" + sReqNo + "%'";
        //}
        //if (sPoNo != "0")
        //{
        //    Filtersql = Filtersql + " and Order_CODE like '%" + sPoNo + "%'";
        //}
        //if (sDoNo != "0")
        //{
        //    Filtersql = Filtersql + " and DELIVERY_CODE like '%" + sDoNo + "%'";
        //}
        //if (sQutRef != "0")
        //{
        //    Filtersql = Filtersql + " and Quotation_Code like '%" + sQutRef + "%'";
        //}


        //if (sTimeLapse != "0")
        //{
        //    Filtersql = Filtersql + " and TimeLaps = " + sTimeLapse + "";
        //}
        //if (sInvoiceNo != "0")
        //{
        //    Filtersql = Filtersql + " and Invoice_No like '%" + sInvoiceNo + "%'";
        //}
        //if (sDeliveryType != "0")
        //{
        //    Filtersql = Filtersql + " and Delivery_CODE like '%" + sDeliveryType + "%'";
        //}
        //if (sDeliveryStatus != "0")
        //{
        //    Filtersql = Filtersql + " and Status like '%" + sDeliveryStatus + "%'";
        //}
        //if (sHoldUnhold != "--ALL--")
        //{
        //    Filtersql = Filtersql + " and OnHold  ='" + sHoldUnhold + "'";
        //}


        //if (sFrom != "")
        //{
        //    Filtersql = Filtersql + " and (requestion_Date  >= '" + DateTime.Parse(sFrom) + "'";
        //}
        //if (sTo != "")
        //{
        //    Filtersql = Filtersql + " and requestion_Date <='" + DateTime.Parse(sTo) + "')";
        //}
        //if (VesselCode != "0" && VesselCode != "--ALL--")
        //{
        //    Filtersql = Filtersql + " and Vessel_Code='" + VesselCode + "'";
        //}
        //if (scmbDept != "--ALL--")
        //{
        //    Filtersql = Filtersql + " and Department='" + scmbDept + "'";
        //}
        //if (scmbUrgency != "0")
        //{
        //    Filtersql = Filtersql + " and URGENCY Like '" + scmbUrgency + "%'";
        //}
        //if (soptList != "ALL")
        //{
        //    Filtersql = Filtersql + " and Form_Type Like '%" + soptList + "%'";
        //}
        //if (sFleet != "0")
        //{
        //    Filtersql = Filtersql + " and Tech_Manager='" + sFleet + "'";
        //}
        //if (Filtersql.Length > 0)
        //{
        //    Filtersql = Filtersql.Substring(5, Filtersql.Length - 5);
        //}

        //try
        //{
        //    using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        //    {
        //        DataSet ds = new DataSet();

        //        DataTable dt = objTechService.GetRequisitionStageDetails("0", "0", txtfrom.Text, txtto.Text);

        //        dt.DefaultView.RowFilter = Filtersql;
        //        rgdReqAllStage.DataSource = dt.DefaultView;
        //        rgdReqAllStage.DataBind();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        //}
        //finally
        //{

        //}

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        DDLFleet.SelectedIndex = 0;
        BindVesselDDL();
        txtTimeLapse.Text = "";
        txtSearchJoinFromDate.Text = (DateTime.Now.AddDays(-(DateTime.Now.DayOfYear) + 1)).ToString("dd-MM-yyyy");
        txtSearchJoinToDate.Text = (DateTime.Now.AddMonths(1).AddDays(-(DateTime.Now.Day))).ToString("dd-MM-yyyy");
        DDLVessel.SelectedIndex = 0;
        cmbDept.SelectedIndex = 0;
        ddlHoldUnhold.SelectedIndex = 0;
        optList.SelectedIndex = 0;
        txtReqNo.Text = "";
        cmbUrgency.SelectedIndex = 0;
        //txtQutRef.Text = "";
        //txtDeliveryType.Text = "";
        txtPoNo.Text = "";
        //txtDoNo.Text = "";
        txtInvoiceNo.Text = "";
        //txtDeliveryStatus.Text = "";
        btnRetrieve_Click(null, null);
    }

    protected void btnExport_Click(object s, EventArgs e)
    {
        //string ddDept = null;
        //if (cmbDept.SelectedIndex != 0)
        //    ddDept = UDFLib.ConvertStringToNull(cmbDept.SelectedValue.ToString());

        //int rowcount = ucCustomPagerItems.isCountRecord;
        //string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        //int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        //BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        //DataTable dt = objTechService.GetRequisitionStageDetails(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
        //    , txtReqNo.Text.Trim() != "" ? txtReqNo.Text.Trim() : null, txtPoNo.Text.Trim() != "" ? txtPoNo.Text.Trim() : null, ddDept
        //    , UDFLib.ConvertStringToNull(cmbUrgency.SelectedValue), txtDoNo.Text != "" ? txtDoNo.Text : null
        //    , UDFLib.ConvertIntegerToNull(txtTimeLapse.Text), UDFLib.ConvertIntegerToNull(ddlHoldUnhold.SelectedValue), txtQutRef.Text.Trim() != "" ? txtQutRef.Text.Trim() : null
        //    , UDFLib.ConvertDateToNull(txtSearchJoinFromDate.Text.Trim())
        //    , UDFLib.ConvertDateToNull(txtSearchJoinToDate.Text.Trim())
        //    , txtDeliveryType.Text.Trim() != "" ? txtDeliveryType.Text.Trim() : null, txtDeliveryStatus.Text.Trim() != "" ? txtDeliveryStatus.Text.Trim() : null
        //    , sortbycoloumn, sortdirection
        //    , null, null, ref  rowcount);



        //string[] HeaderCaptions = { "Vessel Name", "Number", "Date", "URGENCY", "RFQ Sent", "Qtn Rcvd", "Evaluation Date", "Number", "Date", "Sent to supp.", "conf by supp", "Status", "DO Number", "Date.", "Time Lapsed.", "On Hold" };
        //string[] DataColumnsName = { "Vessel_Short_Name", "REQUISITION_CODE", "requestion_Date", "URGENCY", "RFQSend", "QuotReceived", "EVALUATION_DATE", "ORDER_CODE", "ORDER_DATE", "SentToSupp", "ConfBySupp", "Status", "DELIVERY_CODE", "DELIVERY_DATE", "TimeLaps", "OnHold" };

        //GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "View Requisition Summary", "View Requisition Summary", "");
        BindSearchRequistionstages();
        objChangeReqstMerge.MergedColumns.Clear();
        objChangeReqstMerge.StartColumns.Clear();
        objChangeReqstMerge.Titles.Clear();

        GridViewExportUtil objex = new GridViewExportUtil();
        rgdReqAllStage.Columns[0].Visible = false;
        objex.ExportGridviewToExcel(rgdReqAllStage);
        rgdReqAllStage.Columns[0].Visible = true;




    }

    protected void rgdReqAllStage_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
        {
            //string Received = BindToolTipsSuppNameOnQtnRecvd("", ((Label)e.Item.FindControl("lblInfo")).ToolTip.Split(new char[] { ',' })[0], ((Label)e.Item.FindControl("lblInfo")).ToolTip.Split(new char[] { ',' })[1]);
            //string Sent = BindToolTipsSuppNameOnQtnSent("", ((Label)e.Item.FindControl("lblInfo")).ToolTip.Split(new char[] { ',' })[0], ((Label)e.Item.FindControl("lblInfo")).ToolTip.Split(new char[] { ',' })[1]);
            //Label lblsent = ((Label)e.Item.FindControl("lblRFQSend"));
            //Label lblrecvd = ((Label)e.Item.FindControl("lblQuotReceived"));
            //string[] st = Sent.Split(')');
            //string[] rcvd = Received.Split(')');
            //lblrecvd.Text = (rcvd.Length - 1) >= 0 ? (rcvd.Length - 1).ToString() : "0";
            //lblsent.Text = (st.Length - 1) >= 0 ? (st.Length - 1).ToString() : "0";
            //lblsent.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Quotation Sent To :] body=[" + Sent + "]");
            //lblrecvd.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Quotation Received From :] body=[" + Received + "]");


        }
       

    }

    protected string BindToolTipsSuppNameOnQtnRecvd(string QuotCode, string DocCode, string VesselCode)
    {

        DataTable dtToolTipsQtnRecvd = new DataTable();

        string strTootips = "";

        dtToolTipsQtnRecvd = dtQTNReceived;
        dtToolTipsQtnRecvd.DefaultView.RowFilter = "Document_code='" + DocCode + "' and Vessel_Code='" + VesselCode + "'";
        dtToolTipsQtnRecvd = dtToolTipsQtnRecvd.DefaultView.ToTable();
        int i = 0;
        if (dtToolTipsQtnRecvd.Rows.Count > 0)
        {

            

            foreach (DataRow dr in dtToolTipsQtnRecvd.Rows)
            {
                //strTootips = strTootips + i.ToString();//+ ")" + dr[0].ToString() + "<br>";
                i += 1;
            }
        }
        return i.ToString();
    }

    protected string BindToolTipsSuppNameOnQtnSent(string QuotCode, string DocCode, string VesselCode)
    {
        DataTable dtToolTipsQtnSent = new DataTable();
        TechnicalBAL objtechBAL = new TechnicalBAL();
        string strTootips = "";
        dtToolTipsQtnSent = dtQTNSent;
        dtToolTipsQtnSent.DefaultView.RowFilter = "Document_code='" + DocCode + "' and Vessel_Code='" + VesselCode + "'";
        dtToolTipsQtnSent = dtToolTipsQtnSent.DefaultView.ToTable();
        int i = 0;
        if (dtToolTipsQtnSent.Rows.Count > 0)
        {

            

            foreach (DataRow dr in dtToolTipsQtnSent.Rows)
            {
                //strTootips = strTootips + i + ")" + dr[0].ToString() + "<br>";
                //strTootips = strTootips + i.ToString();// +")" + dr[0] + "<br>";
                i += 1;
            }
        }

        return i.ToString() ;


    }

    protected void rgdReqAllStage_OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            btnRetrieve_Click(null, null);

        }
    }
}
