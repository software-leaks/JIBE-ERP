using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ClsBLLTechnical;
using SMS.Business.PURC;

public partial class Technical_INV_Audittrailsummery : System.Web.UI.Page
{

    TechnicalBAL objtec = new TechnicalBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    protected void BindData()
    {

        //Step First

        //DataSet ds = objtec.GetAuditTrailSummary("BEX-10-00004", "313", "313100510095907", "");
        DataSet dsAuditSumry = objtec.GetAuditTrailSummary(Request.QueryString["REQUISITION_CODE"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["document_code"].ToString(), Request.QueryString["QUOTATION_CODE"].ToString());

        DataTable dtVesselDtl = objtec.Get_VID_VesselDetails(UDFLib.ConvertToInteger(Request.QueryString["Vessel_Code"].ToString()));
        if (dtVesselDtl.Rows.Count > 0)
        {
            //lblVessel.Text = Convert.ToString(dtVesselDtl.Rows[0]["VesselName"]);
            //lblVesselExName1.Text = Convert.ToString(dtVesselDtl.Rows[0]["VesselExNames"]);
            //lblVesselHullNo.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Hull_No"]);
            //lblVesselType.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Type"]);
            //lblVesselYard.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Yard"]);
            //lblVesselDelvDate.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Delvry_Date"]);
            //lblIMOno.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_IMO_No"]);
        }

        BLL_PURC_Purchase objhold = new BLL_PURC_Purchase();
        rgdHoldLog.DataSource = objhold.GetRequisitionOnHoldLogHistory_ByReqsn(Request.QueryString["REQUISITION_CODE"]);
        rgdHoldLog.DataBind();

        DataTable dtCancel = BLL_PURC_Common.GET_ReqsnPermanentCancelLog_DL(UDFLib.ConvertToInteger(Request.QueryString["Vessel_Code"]));
        if (dtCancel.Rows.Count > 0)
        {
            dtCancel.DefaultView.RowFilter = "REQUISITION_CODE='" + Request.QueryString["REQUISITION_CODE"]+"'";
        }
        gvReqsnCAncel.DataSource = dtCancel.DefaultView;
        gvReqsnCAncel.DataBind();
        if (dsAuditSumry.Tables[0].Rows.Count > 0)
        {
            lblReqNo.Text = Convert.ToString(dsAuditSumry.Tables[0].Rows[0]["RequistionCode"]);
            lblTotalItem.Text = Convert.ToString(dsAuditSumry.Tables[0].Rows[0]["TotalItems"]);
            lblToDate.Text = Convert.ToString(dsAuditSumry.Tables[0].Rows[0]["ToDate"]);
            txtComments.Text = Convert.ToString(dsAuditSumry.Tables[0].Rows[0]["ReqComents"]);
            lblDept.Text = Convert.ToString(dsAuditSumry.Tables[0].Rows[0]["DeptName"]);
        }
        //Step 2
        ReptrQtnSummry.DataSource = dsAuditSumry.Tables[1];
        ReptrQtnSummry.DataBind();


        //////Step 3

        ReptrQtnEve.DataSource = dsAuditSumry.Tables[2];
        ReptrQtnEve.DataBind();

        //////Step 4

        ReptrPurchaseOrd.DataSource = dsAuditSumry.Tables[3];
        ReptrPurchaseOrd.DataBind();


        //////Step 5

        ReptrPendingSupConfirm.DataSource = dsAuditSumry.Tables[4];
        ReptrPendingSupConfirm.DataBind();

        //////Step 6

        ReptrDeliveryStatus.DataSource = dsAuditSumry.Tables[5];
        ReptrDeliveryStatus.DataBind();

        //////Step 7

        ReptrDeliveredItems.DataSource = dsAuditSumry.Tables[6];
        ReptrDeliveredItems.DataBind();

    }
}
