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
using SMS.Business.PURC;
using Telerik.Web.UI;
using System.Data.SqlClient;
using SMS.Business.Infrastructure;
using System.Runtime.InteropServices;
using System.Text;

public partial class PendingRequisitionDetails : System.Web.UI.Page
{
    protected void Page_Init(object source, System.EventArgs e)
    {

    }
    DataTable dtRequistion = new DataTable();
    string reqsnStageCode = "";

    protected void Page_Load(object sender, EventArgs e)
    {



        if (!IsPostBack)
        {



            Session["sFleet"] = DDLFleet.SelectedValues;
            Session["sVesselCode"] = DDLVessel.SelectedValues;
            Session["sDeptCode"] = cmbDept.SelectedValues;
            Session["sCatalogue"] = "0";
            Session["sReqsnType"] = "0";
            Session["sDeptType"] = "0";

            BindDeptTypeOpt();
            BindFleetDLL();
            DDLFleet.SelectItems(new string[] { Session["USERFLEETID"].ToString() });
            BindVesselDDL();

            // FillDDL();

            //BindDepartmentByST_SP();
            
            Bind_Account_Type();
            Bind_PO_Type();
            Bind_Urgency_Level();
            Bind_Reqsn_Status();
            Bind_Reqsn_Type("");
            Bind_Dept_Function();
            BindCatalogue(UDFLib.ConvertToInteger(GetSessionUserID()));
            if (Request.QueryString["POLOG"] != null)
            {
                LinkUrl.Value = "PendingEvalGrid.aspx?Type=QEV&POLOG=1";
                txtSelMenu.Value = lnkMenu1.ClientID;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectNode", "selMe('" + txtSelMenu.Value + "');", true);
                mainFrame.Attributes["src"] = LinkUrl.Value;

                ViewState["reqsnStageCode"] = "QEV";

            }
            else
            {
                LinkUrl.Value = "NewRequisition.aspx?Type=NRQ";
                txtSelMenu.Value = lnkMenu1.ClientID;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectNode", "selMe('" + txtSelMenu.Value + "');", true);
                mainFrame.Attributes["src"] = Convert.ToString(gerNavigationURL("NRQ"));
                ViewState["reqsnStageCode"] = "NRQ";
            }


        }

        Session["sCatalogue"] = "0";

        //if (txtRfqPODOInvNo.Text != "")
        //    Session["REQNUM"] = txtRfqPODOInvNo.Text;
        //else
        //    Session["REQNUM"] = "0";
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectNode", "selMe('" + txtSelMenu.Value + "');", true);
    }

    private DataTable 
        BindRequisitionInHirarchy()
    {
        dtRequistion = new DataTable();
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dtRequistion = objTechService.SelectRequisitionForHierarchy();


            }
            return dtRequistion;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            return dtRequistion = null; ;
        }
        finally
        {

        }

    }

    private DataTable BindItemsInHirarchy(string strRequistionCode, string strVesselCode, string strDocumnetCode)
    {
        DataTable dtItems = new DataTable();
        try
        {

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dtItems = objTechService.SelectItemsForHierarchy(strRequistionCode, strVesselCode, strDocumnetCode);

            }
            return dtItems;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            return dtItems = null;
        }
        finally
        {

        }

    }

    public void BindDeptTypeOpt()
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable DeptDt = objTechService.GetDeptType();
                //optList.DataSource = DeptDt;
                //optList.DataTextField = "Description";
                //optList.DataValueField = "Short_Code";
                //optList.SelectedIndex = 0;
                //optList.DataBind();

            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public void BindVesselDDL()
    {
        try
        {

            
            StringBuilder sbFilterFlt = new StringBuilder();
            string VslFilter = "";
            foreach (DataRow dr in DDLFleet.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }

            DataTable dtVessel = BLL_PURC_Common.Get_User_Vessel_Assign(0,GetSessionUserID());

            if (sbFilterFlt.Length > 1)
            {
                sbFilterFlt.Remove(sbFilterFlt.Length - 1, 1);
                VslFilter = string.Format("fleetCode in (" + sbFilterFlt.ToString() + ")");
                dtVessel.DefaultView.RowFilter = VslFilter;
            }

            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_Name";
            DDLVessel.DataValueField = "Vessel_ID";
            DDLVessel.DataBind();
            Session["sVesselCode"] = DDLVessel.SelectedValues;
            Session["sFleet"] = sbFilterFlt;//DDLFleet.SelectedValues;

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
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



            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable DeptDt = objTechService.GetDeptType();
                //optList.DataSource = DeptDt;
                //optList.DataTextField = "Description";
                //optList.DataValueField = "Short_Code";
                //optList.SelectedIndex = 0;
                //optList.DataBind();

            }

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

    private void BindDepartmentByST_SP()
    {

        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtDepartment = new DataTable();
                dtDepartment = objTechService.SelectDepartment();

                //if (optList.SelectedItem.Text == "Spares")
                //{
                //    dtDepartment.DefaultView.RowFilter = "Form_Type='SP'";
                //}
                //else if (optList.SelectedItem.Text.ToLower() == "Stores".ToLower())
                //{
                //    dtDepartment.DefaultView.RowFilter = "Form_Type='ST'";

                //}
                //else if (optList.SelectedItem.Text == "Repairs")
                //{
                //    dtDepartment.DefaultView.RowFilter = "Form_Type='RP'";

                //}

                //cmbDept.DataSource = dtDepartment;
                //cmbDept.DataTextField = "Name_Dept";
                //cmbDept.DataValueField = "Code";
                //cmbDept.DataBind();


            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            ////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {


        }

    }

    protected void cmbDept_OnSelectedIndexChanged()
    {
        try
        {
            StringBuilder sbFilterFlt = new StringBuilder();
            foreach (DataRow dr in cmbDept.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }
            Session["sPurc_Dept"] = sbFilterFlt;
            
        }
        catch(Exception ex)
        {

        }

        Session["sPurc_Dept"] = cmbDept.SelectedValues;


    }

    //protected void ddlReqsnType_OnSelectedIndexChanged(object source, System.EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlReqsnType.SelectedValue != "0")
    //            Session["sReqsnType"] = ddlReqsnType.SelectedValue;
    //        else
    //            Session["sReqsnType"] = "0";
    //    }
    //    catch (Exception ex)
    //    {
    //        UDFLib.WriteExceptionLog(ex);
    //    }

    //}
    /// <summary>
    /// 
    /// </summary>
    protected void DDLFleet_SelectedIndexChanged()
    {
        BindVesselDDL();
        BindGrid();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void optList_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        Session["sDeptType"] = optList.SelectedValue.ToLower() == "all" ? "0" : optList.SelectedValue;
    //        BindDepartmentByST_SP();
    //        cmbDept_OnSelectedIndexChanged();

    //    }
    //    catch (Exception ex)
    //    {
    //        UDFLib.WriteExceptionLog(ex);
    //    }

    //}


    /// <summary>
    /// 
    /// </summary>
    /// <param name="Selection"></param>
    /// <returns></returns>
    protected string gerNavigationURL(string Selection)
    {
        string retURL = "";
        try
        {           
            switch (Selection)
            {
                case "NRQ":
                    retURL = "NewRequisition.aspx?Type=NRQ";
                    Session["PURC_ReqsnStatusATAll"] = "";
                    break;
                case "RFQ":
                    retURL = "PendingReqGrid.aspx?Type=RFQ";
                    Session["PURC_ReqsnStatusATAll"] = "";
                    break;
                case "UPQ": 
                    retURL = "PendingReqGrid.aspx?Type=UPQ";
                    Session["PURC_ReqsnStatusATAll"] = "";
                    break;
                case "QEV":
                    retURL = "PendingEvalGrid.aspx?Type=QEV";
                    Session["PURC_ReqsnStatusATAll"] = "";
                    break;
                case "PFA":
                    retURL = "PendingPOApproveGrid.aspx?Type=PFA";
                    Session["PURC_ReqsnStatusATAll"] = "";
                    break;
                case "RPO":
                    retURL = "PendingPOGrid.aspx?Type=RPO";
                    Session["PURC_ReqsnStatusATAll"] = "";
                    break;
                case "SCN":
                    retURL = "PendingPOConfirmGrid.aspx?Type=SCN";
                    Session["PURC_ReqsnStatusATAll"] = "";
                    break;
                case "DVS":
                    retURL = "DeliveryStatusGrid.aspx?Type=DVS";
                    Session["PURC_ReqsnStatusATAll"] = "";
                    lblhdrDate.Text = "PO Issue Date";
                    break;
                case "UPD":
                    retURL = "PendingDeliveryGrid.aspx?Type=UPD";
                    Session["PURC_ReqsnStatusATAll"] = "";
                    break;
                case "ALL":
                    retURL = "ALLRequisitioinStatusGrid.aspx?Type=ALL";
                    break;
                case "CAN":
                    retURL = "CancelledLog.aspx?Type=CAN";
                    Session["PURC_ReqsnStatusATAll"] = "";
                    break;
                case "DLV":
                    retURL = "Delivered_Requisition.aspx?Type=DLV";
                    Session["PURC_ReqsnStatusATAll"] = "";
                    lblhdrDate.Text = "Delivery Date";
                    break;
                case "BPR":
                    retURL = "PURC_Bulk_Purchase_Reqsn.aspx?Type=BPR";
                    Session["PURC_ReqsnStatusATAll"] = "";
                    break;
            }
            
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        return retURL;
    }




    protected void btnRefresh_Click(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            DDLFleet.ClearSelection();
            DDLVessel.ClearSelection();
            ddlPO_Type.ClearSelection();
            ddlAccType.ClearSelection();
            ddlCatalogue.ClearSelection();
            ddlAccClassification.ClearSelection();
            ddlReqsnStatus.ClearSelection();
            ddlReqsnType.ClearSelection();
            ddlUrgencyLvl.ClearSelection();
            
            BindVesselDDL();

            cmbDept.ClearSelection();
            //txtRfqPODOInvNo.Text = "";

            Session["sFleet"] = DDLFleet.SelectedValues;
            Session["sVesselCode"] = DDLVessel.SelectedValues;
            Session["sDeptCode"] = cmbDept.SelectedValues;
            Session["sCatalogue"] = "0";
            Session["REQNUM"] = "0";
            Session["ReqsnType"] = "0";
            Session["sDeptType"] = "0";
            Session["sFrom"] = "";
            Session["sTO"] = "";

            mainFrame.Attributes["src"] = "NewRequisition.aspx?Type=NRQ";
            txtSelMenu.Value = lnkMenu1.ClientID;
            ViewState["reqsnStageCode"] = "NRQ";
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Export to excel click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            BLL_PURC_Purchase objPendingReqsn = new BLL_PURC_Purchase();
            DataTable dtexportdata = new DataTable();

            string[] HeaderCaptions = { };
            string[] DataColumnsName = { };
            string FileName = "";
            string FileHeaderName = "";

            int rowcount = 0;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            string sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Convert.ToString(ViewState["SORTDIRECTION"]);
            switch (ViewState["reqsnStageCode"].ToString())
            {
                case "NRQ":

                    dtexportdata = objPendingReqsn.SelectNewRequisitionList_New(UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                                                , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                                                , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                                                , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                                                , UDFLib.ConvertStringToNull(Session["sPOType"])
                                                                , UDFLib.ConvertStringToNull(Session["sAccType"])
                                                                , UDFLib.ConvertStringToNull(Session["sReqsnType"])
                                                                , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                                                , UDFLib.ConvertDateToNull(Session["sFrom"])
                                                                , UDFLib.ConvertDateToNull(Session["sTO"])
                                                                , UDFLib.ConvertStringToNull(Session["sAccClass"])
                                                                , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                                                , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                                                                , null, null, ref  rowcount, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));


                    HeaderCaptions = new string[] { "Vessel", "Requisition", "Sent Date", "Dept Name", "Catalogue Name", "Total Item", "Urgency" };
                    DataColumnsName = new string[] { "Vessel_Name", "REQUISITION_CODE", "requestion_date", "Name_Dept", "sYSTEM_DESCRIPTION", "TOTAL_ITEMS", "URGENCY_CODE" };
                    FileHeaderName = "New Requisition";
                    FileName = "New_Requisition";


                    //dtexportdata = objPendingReqsn.SelectNewRequisitionList((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
                    //                                                       , UDFLib.ConvertStringToNull(Session["sDeptType"])
                    //                                                       , (DataTable)Session["sDeptCode"]
                    //                                                       , UDFLib.ConvertIntegerToNull(Session["sReqsnType"].ToString())
                    //                                                       , UDFLib.ConvertStringToNull(Session["REQNUM"].ToString())
                    //                                                       , null, null, ref  rowcount, sortbycoloumn, sortdirection);

                    //HeaderCaptions = new string[] { "Vessel", "Requisition", "Sent Date", "Dept Name", "Catalogue Name", "Total Item", "Urgency" };
                    //DataColumnsName = new string[] { "Vessel_Name", "REQUISITION_CODE", "requestion_date", "Name_Dept", "sYSTEM_DESCRIPTION", "TOTAL_ITEMS", "URGENCY_CODE" };
                    //FileHeaderName = "New Requisition";
                    //FileName = "New_Requisition";

                    break;
                case "RFQ":
                    dtexportdata = objPendingReqsn.SelectPendingRequistion_New(
                                UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                , UDFLib.ConvertStringToNull(Session["sPOType"])
                                , UDFLib.ConvertStringToNull(Session["sAccType"])
                                , UDFLib.ConvertStringToNull(Session["sReqsnType"])
                                , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                , UDFLib.ConvertDateToNull(Session["sFrom"])
                                , UDFLib.ConvertDateToNull(Session["sTO"])
                                , UDFLib.ConvertStringToNull(Session["sAccClass"])
                                , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                , UDFLib.ConvertStringToNull((Session["sMinQuot"]))
                                , UDFLib.ConvertToInteger((Session["sReqsnStatus"]))

                                , null, null, ref  rowcount, sortbycoloumn, sortdirection);
                    //dtexportdata = objPendingReqsn.SelectPendingRequistion((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
                    //                                                        , UDFLib.ConvertStringToNull(Session["sDeptType"])
                    //                                                        , (DataTable)Session["sDeptCode"]
                    //                                                        , UDFLib.ConvertIntegerToNull(Session["sReqsnType"].ToString())
                    //                                                        , UDFLib.ConvertStringToNull(Session["REQNUM"].ToString())
                    //                                                        , null, null, ref  rowcount, sortbycoloumn, sortdirection);

                    HeaderCaptions = new string[] { "Vessel", "Requisition", "Sent Date", "Dept Name", "Catalogue Name", "Total Item", "RFQ Sent", "In Progress", "Qtn Received", "Urgency" };
                    DataColumnsName = new string[] { "Vessel_Name", "REQUISITION_CODE", "requestion_date", "Name_Dept", "sYSTEM_DESCRIPTION", "TOTAL_ITEMS", "RFQSend", "Sent_Reqsn_Status", "QuotReceived", "URGENCY_CODE" };
                    FileHeaderName = "RFQ/Quotation";
                    FileName = "RFQ_Quotation";

                    break;
                case "QEV":

                    int? LogedIn_User = null;
                    if (Session["SHOWALL_PENDING_APPROVAL"].ToString() == "1")
                    {
                        LogedIn_User = Convert.ToInt32(Session["USERID"].ToString());

                    }



                    //dtexportdata = objPendingReqsn.SelectPendingQuatationEvalution((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
                    //                , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
                    //                , UDFLib.ConvertIntegerToNull(Session["sReqsnType"].ToString()), UDFLib.ConvertStringToNull(Session["REQNUM"].ToString()), LogedIn_User
                    //                , null, null, ref  rowcount, sortbycoloumn, sortdirection);

                    dtexportdata = objPendingReqsn.SelectPendingQuatationEvalution_New
                                        (UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                        , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                        , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                        , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                        , UDFLib.ConvertStringToNull(Session["sPOType"])
                                        , UDFLib.ConvertStringToNull(Session["sAccType"])
                                        , UDFLib.ConvertStringToNull(Session["ReqsnType"])
                                        , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                        , UDFLib.ConvertDateToNull(Session["sFrom"])
                                        , UDFLib.ConvertDateToNull(Session["sTO"])
                                        , UDFLib.ConvertStringToNull(Session["sAccClass"])
                                        , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                        , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                                        , UDFLib.ConvertStringToNull((Session["sQuotRec"]))
                                        , UDFLib.ConvertStringToNull((Session["sPendingApprover"]))
                                        , UDFLib.ConvertStringToNull((Session["USERID"]))
                                        , null, null, ref  rowcount, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));

                    HeaderCaptions = new string[] { "Vessel", "Requisition", "Sent Date", "Dept Name", "Catalogue Name", "Total Item", "RFQ Sent", "Quotation Received", "Urgency", "Pending With" };
                    DataColumnsName = new string[] { "Vessel_Name", "REQUISITION_CODE", "requestion_date", "Name_Dept", "sYSTEM_DESCRIPTION", "TOTAL_ITEMS", "RFQSend", "QuotReceived", "URGENCY_CODE", "PendApprover" };
                    FileHeaderName = "Pending Quotation Approval";
                    FileName = "Quotation_Approval";
                    break;

                case "RPO":

                    //dtexportdata = objPendingReqsn.SelectPendingPurchasedOrderRaise((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
                    //                , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
                    //                , UDFLib.ConvertIntegerToNull(Session["sReqsnType"].ToString()), UDFLib.ConvertStringToNull(Session["REQNUM"].ToString())
                    //                , null, null, ref  rowcount, sortbycoloumn, sortdirection);

                    dtexportdata = objPendingReqsn.SelectPendingPurchasedOrderRaise_New(UDFLib.ConvertStringToNull(Session["sFleet"])
                                , UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                , UDFLib.ConvertStringToNull(Session["sPOType"])
                                , UDFLib.ConvertStringToNull(Session["sAccType"])
                                , UDFLib.ConvertStringToNull(Session["ReqsnType"])
                                , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                , UDFLib.ConvertDateToNull(Session["sFrom"])
                                , UDFLib.ConvertDateToNull(Session["sTO"])
                                , UDFLib.ConvertStringToNull(Session["sAccClass"])
                                , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                                , UDFLib.ConvertStringToNull((Session["sPort"]))
                                , null, null, ref  rowcount, sortbycoloumn, sortdirection);

                    HeaderCaptions = new string[] { "Vessel", "Requisition", "Sent Date", "Dept Name", "Catalogue Name", "Total Item", "Lead Time", "Urgency" };
                    DataColumnsName = new string[] { "Vessel_Name", "REQUISITION_CODE", "requestion_date", "Name_Dept", "sYSTEM_DESCRIPTION", "TOTAL_ITEMS", "Lead_Time", "URGENCY_CODE" };
                    FileHeaderName = "Send Purchase Order";
                    FileName = "Send_Purchase_Order";
                    break;
                case "SCN":
                    dtexportdata = objPendingReqsn.SelectPendingPOConfirm_New(UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                    , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                    , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                    , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                    , UDFLib.ConvertStringToNull(Session["sPOType"])
                                    , UDFLib.ConvertStringToNull(Session["sAccType"])
                                    , UDFLib.ConvertStringToNull(Session["ReqsnType"])
                                    , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                    , UDFLib.ConvertDateToNull(Session["sFrom"])
                                    , UDFLib.ConvertDateToNull(Session["sTO"])
                                    , UDFLib.ConvertStringToNull(Session["sAccClass"])
                                    , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                    , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                                    , UDFLib.ConvertStringToNull((Session["Supplier"]))
                                    , null, null, ref  rowcount, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));
                    //dtexportdata = objPendingReqsn.SelectPendingPOConfirm_Export((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
                    //                                                    , UDFLib.ConvertStringToNull(Session["sDeptType"])
                    //                                                    , (DataTable)Session["sDeptCode"]
                    //                                                    , UDFLib.ConvertIntegerToNull(Session["sReqsnType"].ToString())
                    //                                                    , UDFLib.ConvertStringToNull(Session["REQNUM"].ToString())
                    //                                                    , null, null, ref  rowcount);

                    HeaderCaptions = new string[] { "Vessel", "Dept Name", "Requisition", "Catalogue Name", "Order Number", "Supplier Name", "Total Item", "Lead Time", "Currency", "PO Amount", "Urgency" };
                    DataColumnsName = new string[] { "Vessel_Name", "Name_Dept", "REQUISITION_CODE", "sYSTEM_DESCRIPTION", "ORDER_CODE", "SHORT_NAME", "PO_ITEMS_COUNT", "MAX_LEAD_TIME", "CURRENCY", "PO_AMOUNT", "URGENCY_CODE" };
                    FileHeaderName = "Pending Supplier Confirmation";
                    FileName = "Pending_Supplier_Confirmation";
                    break;
                case "DVS":

                    dtexportdata = objPendingReqsn.SelectRequisitionDeliveryStatus_Export(null,
                                                                                          null,
                                                                                         UDFLib.ConvertIntegerToNull(Session["PURC_DeliveryPort"]),
                                                                                         (DataTable)Session["PURC_CurrentStage"],
                                                                                         (DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"],
                                                                                         (DataTable)Session["sDeptCode"],
                                                                                         UDFLib.ConvertStringToNull(Session["sDeptType"]),
                                                                                         UDFLib.ConvertStringToNull(Session["REQNUM"]),
                                                                                         UDFLib.ConvertIntegerToNull(Session["sReqsnType"]),
                                                                                         (DataTable)Session["PURC_Supplier_Name"],
                                                                                         UDFLib.ConvertDateToNull(Session["PURC_PO_Date"]),
                                                                                         UDFLib.ConvertDateToNull(Session["PURC_TO_PO_Date"]),
                                                                                         null,
                                                                                         null,
                                                                                         ref rowcount);
                    HeaderCaptions = new string[] { "Vessel", "Dept Name", "Requisition", "Catalogue Name", "Order Number", "PO Date", "Supplier Name", "Total Item", "Lead Time", "Currency", "PO Amount", "Urgency" };
                    DataColumnsName = new string[] { "Vessel_Name", "Name_Dept", "REQUISITION_CODE", "sYSTEM_DESCRIPTION", "ORDER_CODE", "po_date", "SHORT_NAME", "PO_ITEMS_COUNT", "MAX_LEAD_TIME", "CURRENCY", "PO_AMOUNT", "URGENCY_CODE" };
                    FileHeaderName = "Delivery Status";
                    FileName = "Delivery_Status";

                    break;
                case "UPD":

                    dtexportdata = objPendingReqsn.SelectPendingDeliveryUpdate((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"],
                                                                                         (DataTable)Session["sDeptCode"],
                                                                                         UDFLib.ConvertStringToNull(Session["sDeptType"]),
                                                                                         UDFLib.ConvertStringToNull(Session["REQNUM"]),
                                                                                         UDFLib.ConvertIntegerToNull(Session["sReqsnType"]),
                                                                                         null,
                                                                                         null,
                                                                                         ref rowcount);
                    HeaderCaptions = new string[] { "Vessel", "Dept Name", "Requisition", "Order No", "PO Date", "Catalogue Name", "Supplier", "Total Item", "Lead Time", "Urgency" };
                    DataColumnsName = new string[] { "Vessel_Name", "Name_Dept", "REQUISITION_CODE", "ORDER_CODE", "po_date", "sYSTEM_DESCRIPTION", "SHORT_NAME", "TOTAL_ITEMS", "Lead_Time", "URGENCY_CODE" };
                    FileHeaderName = "Pending Delivery Update";
                    FileName = "Pending_Delivery_Update";
                    break;
                case "ALL":
                    dtexportdata = objPendingReqsn.SelectAllRequisitionStages((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"],
                                                                                      (DataTable)Session["sDeptCode"],
                                                                                     UDFLib.ConvertStringToNull(Session["sDeptType"]),
                                                                                     UDFLib.ConvertStringToNull(Session["REQNUM"]),
                                                                                     UDFLib.ConvertIntegerToNull(Session["sReqsnType"]),
                                                                                     UDFLib.ConvertStringToNull(Session["PURC_ReqsnStatusATAll"]),
                                                                                    null,
                                                                                    null,
                                                                                    ref rowcount, sortbycoloumn, sortdirection);

                    HeaderCaptions = new string[] { "Vessel", "Requisition", "Order Code", "Supplier", "Sent Date", "Dept Name", "Catalogue Name", "Total Item", "Status", "Urgency" };
                    DataColumnsName = new string[] { "Vessel_Name", "REQUISITION_CODE", "ORDER_CODE", "SHORT_NAME", "requestion_date", "Name_Dept", "sYSTEM_DESCRIPTION", "TOTAL_ITEMS", "status", "URGENCY_CODE" };
                    FileHeaderName = "All Status";
                    FileName = "All_Status";
                    break;
                case "CAN":
                    dtexportdata = BLL_PURC_Common.Get_CancelReqsn((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
                                    , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
                                    , UDFLib.ConvertIntegerToNull(Session["sReqsnType"].ToString()), UDFLib.ConvertStringToNull(Session["REQNUM"].ToString())
                                    , null, null, ref  rowcount, sortbycoloumn, sortdirection);
                    HeaderCaptions = new string[] { "Vessel", "Requisition", "Sent Date", "Dept Name", "Catalogue Name", "Total Item", "Lead Time", "Urgency" };
                    DataColumnsName = new string[] { "Vessel_Name", "REQUISITION_CODE", "requestion_date", "Name_Dept", "sYSTEM_DESCRIPTION", "TOTAL_ITEMS", "Lead_Time", "URGENCY_CODE" };
                    FileHeaderName = "Cancelled";
                    FileName = "Cancelled";
                    break;
                case "DLV":
                    dtexportdata = BLL_PURC_Common.Get_Delivered_Requisition_Stage((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
                                                                                   , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
                                                                                   , UDFLib.ConvertIntegerToNull(Session["sReqsnType"].ToString())
                                                                                   , UDFLib.ConvertStringToNull(Session["REQNUM"].ToString()), null
                                                                                   , null, null, ref rowcount, sortbycoloumn, sortdirection);


                    HeaderCaptions = new string[] { "Vessel", "Requisition", "Order No.", "Delivery No", "Supplier", "Received on", "Catalogue Name", "Total Item", "Status", "Urgency" };
                    DataColumnsName = new string[] { "Vessel_Name", "REQUISITION_CODE", "ORDER_CODE", "DELIVERY_CODE", "SHORT_NAME", "requestion_date", "sYSTEM_DESCRIPTION", "TOTAL_ITEMS", "status", "URGENCY_CODE" };
                    FileHeaderName = "Delivered";
                    FileName = "Delivered";

                    break;
            }

            GridViewExportUtil.ShowExcel(dtexportdata, HeaderCaptions, DataColumnsName, FileName, FileHeaderName);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void ddlUrgencyLvl_SelectedIndexChanged()
    {
        try
        {
            StringBuilder sbFilterFlt = new StringBuilder();
            foreach (DataRow dr in ddlUrgencyLvl.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }
            Session["sUrgency"] = Convert.ToString(sbFilterFlt);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void DDLVessel_SelectedIndexChanged()
    {
        try
        {
            StringBuilder sbFilterFlt = new StringBuilder();
            foreach (DataRow dr in DDLVessel.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }
            Session["sVesselCode"] = Convert.ToString(sbFilterFlt);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Purchase process status tab selection changed it will redirect to linked page.
    /// </summary>
    /// <param name="sender">selected link button</param>
    /// <param name="e">Click event</param>
    protected void NavMenu_Click(object sender, EventArgs e)
    {
        try
        {
            LinkUrl.Value = gerNavigationURL(((LinkButton)sender).CommandArgument).ToString();
            txtSelMenu.Value = ((LinkButton)sender).ClientID;
            mainFrame.Attributes["src"] = gerNavigationURL(((LinkButton)sender).CommandArgument);

            ViewState["reqsnStageCode"] = ((LinkButton)sender).CommandArgument;
            //Below code displays header path with selected status name.
           
         

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }
    #region Bind PO Type
    private void Bind_PO_Type()
    {
        int LogedIn_User = Convert.ToInt32(Session["USERID"]);
        try
        {

            DataTable dtPoType =BLL_PURC_Common.PURC_Get_PO_Type(LogedIn_User,"PO_Access");
            ddlPO_Type.DataSource = dtPoType;
            ddlPO_Type.DataTextField = "PO_Type";
            ddlPO_Type.DataValueField = "PO_Code";
            ddlPO_Type.DataBind();

            
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

        }
    }
    #endregion

    #region Bind Account Type
    private void Bind_Account_Type()
    {
        int LogedIn_User = Convert.ToInt32(Session["USERID"]);
        try
        {
            DataTable dtAccType = BLL_PURC_Common.PURC_Get_Type(LogedIn_User,"PO_TYPE").Tables[1];
            ddlAccType.DataSource = dtAccType;
            ddlAccType.DataTextField = "VARIABLE_NAME";
            ddlAccType.DataValueField = "VARIABLE_CODE";
            ddlAccType.DataBind();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #endregion

    #region Bind Requisition Type
    /// <summary>
    /// To Bind Requisition Type
    /// 
    /// </summary>
    /// <param name="PO_Type">Selected Po type from PO TYPE Dropdown.</param>
    private void Bind_Reqsn_Type(string PO_Type)
    {
        try
        {
            DataTable dtReqsnType = BLL_PURC_Common.PURC_Get_Requisition_Type(PO_Type, UDFLib.ConvertIntegerToNull(GetSessionUserID())).Tables[0];
            ddlReqsnType.DataSource = dtReqsnType;
            ddlReqsnType.DataTextField = "Reqsn_Name";
            ddlReqsnType.DataValueField = "ID";
            ddlReqsnType.DataBind();

        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    
    protected void ddlPO_Type_SelectedIndexChanged()
    {
        try
        {
            StringBuilder sbFilterFlt = new StringBuilder();
            foreach (DataRow dr in ddlPO_Type.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }
            Bind_Reqsn_Type(Convert.ToString(sbFilterFlt));
            Session["sPOType"] = Convert.ToString(sbFilterFlt);
            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void ddlAccType_SelectedIndexChanged()
    {
        try
        {
            StringBuilder sbAccType = new StringBuilder();
            foreach (DataRow dr in ddlAccType.SelectedValues.Rows)
            {
                sbAccType.Append(dr[0]);
                sbAccType.Append(",");
            }
            Session["sAccType"] = Convert.ToString(sbAccType);
            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void ddlReqsnType_SelectedIndexChanged()
    {
        try
        {
            StringBuilder sbFilterFlt = new StringBuilder();
            foreach (DataRow dr in ddlReqsnType.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }
            Session["sReqsnType"] = Convert.ToString(sbFilterFlt);
            
            Bind_Dept_Function();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void ddlCatalogue_SelectedIndexChanged()
    {
        try
        {
            StringBuilder sbFilterFlt = new StringBuilder();
            foreach (DataRow dr in ddlCatalogue.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }
            Session["sCatalogue"] = Convert.ToString(sbFilterFlt);
            
            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ddlReqsnStatus_SelectedIndexChanged()
    {
        try
        {
            StringBuilder sbFilterFlt = new StringBuilder();
            foreach (DataRow dr in ddlReqsnStatus.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }

            Session["sReqsnStatus"] = Convert.ToString(sbFilterFlt);
            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void ddlAccClassification_SelectedIndexChanged()
    {
        try
        {
            StringBuilder sbFilterFlt = new StringBuilder();
            foreach (DataRow dr in ddlAccClassification.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }

            Session["sAccClass"] = Convert.ToString(sbFilterFlt);
            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void Bind_Dept_Function()
    {
        try
        {
            BLL_PURC_Permissions objPURC_Permission = new BLL_PURC_Permissions();
            DataSet ds = objPURC_Permission.Get_Dep_Cat_SubCat(Convert.ToString(GetSessionUserID())); //BLL_PURC_Common.PURC_Filter_AccountClassification(UDFLib.ConvertToInteger(GetSessionUserID()), Convert.ToString(Session["sPOType"]), Convert.ToString(Session["sReqsnType"]));

            //if (Convert.ToString(Session["sReqsnType"]) == "7595")
            //{
            cmbDept.DataSource = ds.Tables[0];
            cmbDept.DataTextField = "Text";
            cmbDept.DataValueField = "Value";
            cmbDept.DataBind();
            BindGrid();
            //ddlSystem.SelectedIndex = 0;
            //lblCatalogue.Text = "Department";
            //}
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void BindCatalogue(int UserID)
    {
        try
        {
            BLL_PURC_Permissions objPURC_Permission = new BLL_PURC_Permissions();

            DataSet ds = objPURC_Permission.Get_Dep_Cat_SubCat(Convert.ToString(GetSessionUserID()));// BLL_PURC_Common.Filter_Catalog(UserID,System_Code);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCatalogue.DataSource = ds.Tables[1];
                ddlCatalogue.DataTextField = "Text";
                ddlCatalogue.DataValueField = "Value";
                ddlCatalogue.DataBind();
                BindGrid();
            }
            else
            {
                //ddlCatalogue.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        //ddlSystem.Items.Insert(0, new ListItem("Store", "0"));
        //ddlCatalogue.SelectedIndex = 0;
    }

    //protected void BindFunction()
    //{
    //    DataSet ds = BLL_PURC_Common.PURC_Get_AccountClassification(UDFLib.ConvertToInteger(GetSessionUserID()), ddlPOType.SelectedValue, ddlReqsnType.SelectedValue);

    //    if (ddlReqsnType.SelectedValues == "7595")
    //    {
    //        cmbDept.DataSource = ds.Tables[0];
    //        cmbDept.DataTextField = "Dept_Name";
    //        cmbDept.DataValueField = "Dept_Short_Code";
    //        cmbDept.DataBind();
    //        //ddlSystem.SelectedIndex = 0;
    //        //lblCatalogue.Text = "Department";
    //    }
    //    else if (ddlReqsnType.SelectedValue == "161" || ddlReqsnType.SelectedValue == "160")
    //    {
    //        cmbDept.DataSource = ds.Tables[0];
    //        cmbDept.DataTextField = "Description";
    //        cmbDept.DataValueField = "Short_Code";
    //        cmbDept.DataBind();
    //        //ddlSystem.SelectedIndex = 0;
    //        //lblCatalogue.Text = "Function";
    //    }
    //    else if (ddlReqsnType.SelectedValue == "159")
    //    {
    //        cmbDept.ClearSelection();

    //        cmbDept.Items.Insert(0, new ListItem("Store", "0"));
    //    }
    //}
    //protected void BindCatalogue(int UserID, string Reqn_Type, string Dept)
    //{
    //    try
    //    {
    //        DataSet ds = BLL_PURC_Common.SelectCatalog(UserID, Reqn_Type, Dept);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlCatalogue.DataSource = ds.Tables[0];
    //            ddlCatalogue.DataTextField = "SYSTEM_DESCRIPTION";
    //            ddlCatalogue.DataValueField = "SYSTEM_CODE";
    //            ddlCatalogue.DataBind();
    //        }
    //        else
    //        {
                
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        UDFLib.WriteExceptionLog(ex);
    //    }
    //    //ddlSystem.Items.Insert(0, new ListItem("Store", "0"));
    //    //ddlCatalogue.SelectedIndex = 0;
    //}
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    #endregion

    #region Bind Urgency Levels
    private void Bind_Urgency_Level()
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtItem = objTechService.LibraryGetSystemParameterList("7609", "");
                ddlUrgencyLvl.DataSource = dtItem;
                ddlUrgencyLvl.DataTextField = "DESCRIPTION";
                ddlUrgencyLvl.DataValueField = "CODE";
                ddlUrgencyLvl.DataBind();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #endregion

    #region Requisition Status
    private void Bind_Reqsn_Status()
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dt = objTechService.LibraryGetSystemParameterList("150", "");
                ddlReqsnStatus.DataSource = dt;
                ddlReqsnStatus.DataTextField = "DESCRIPTION";
                ddlReqsnStatus.DataValueField = "SHORT_CODE";
                ddlReqsnStatus.DataBind();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    #endregion

    protected void imgSearch_Click(object sender, ImageClickEventArgs e)
    {
        BLL_PURC_Purchase objPendingReqsn = new BLL_PURC_Purchase();
        DataTable dt = new DataTable();
        string[] HeaderCaptions = { };
        string[] DataColumnsName = { };
        try
        {
            if (txtReqsnOrderNo.Text.Trim() != string.Empty)
            {
                Session["REQNUM"] = Convert.ToString(txtReqsnOrderNo.Text.Trim());
            }
            else
            {
                Session["REQNUM"] = null;
            }
            int rowcount = 0;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            string sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Convert.ToString(ViewState["SORTDIRECTION"]);

            switch (ViewState["reqsnStageCode"].ToString())
            {
                case "NRQ":
                 dt = objPendingReqsn.SelectNewRequisitionList_New
                                    (UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                    , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                    , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                    , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                    , UDFLib.ConvertStringToNull(Session["sPOType"])
                                    , UDFLib.ConvertStringToNull(Session["sAccType"])
                                    , UDFLib.ConvertStringToNull(Session["ReqsnType"])
                                    , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                    , UDFLib.ConvertDateToNull(Session["sFrom"])
                                    , UDFLib.ConvertDateToNull(Session["sTO"])
                                    , UDFLib.ConvertStringToNull(Session["sAccClass"])
                                    , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                    , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                                    , null, null, ref  rowcount, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));
                 break;
                case "RFQ":
                dt = objPendingReqsn.SelectPendingRequistion_New(
                            UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                            , UDFLib.ConvertStringToNull(Session["sDeptType"])
                            , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                            , UDFLib.ConvertStringToNull(Session["REQNUM"])
                            , UDFLib.ConvertStringToNull(Session["sPOType"])
                            , UDFLib.ConvertStringToNull(Session["sAccType"])
                            , UDFLib.ConvertStringToNull(Session["sReqsnType"])
                            , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                            , UDFLib.ConvertDateToNull(Session["sFrom"])
                            , UDFLib.ConvertDateToNull(Session["sTO"])
                            , UDFLib.ConvertStringToNull(Session["sAccClass"])
                            , UDFLib.ConvertStringToNull(Session["sUrgency"])
                            , UDFLib.ConvertStringToNull((Session["sMinQuot"]))
                            , UDFLib.ConvertToInteger((Session["sReqsnStatus"]))

                            , null, null, ref  rowcount, sortbycoloumn, sortdirection);
                break;
                case "QEV":
                int? LogedIn_User = null;
                if (Session["SHOWALL_PENDING_APPROVAL"].ToString() == "1")
                {
                    LogedIn_User = Convert.ToInt32(Session["USERID"].ToString());

                }
                dt = objPendingReqsn.SelectPendingQuatationEvalution_New
                                                        (UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                                        , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                                        , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                                        , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                                        , UDFLib.ConvertStringToNull(Session["sPOType"])
                                                        , UDFLib.ConvertStringToNull(Session["sAccType"])
                                                        , UDFLib.ConvertStringToNull(Session["ReqsnType"])
                                                        , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                                        , UDFLib.ConvertDateToNull(Session["sFrom"])
                                                        , UDFLib.ConvertDateToNull(Session["sTO"])
                                                        , UDFLib.ConvertStringToNull(Session["sAccClass"])
                                                        , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                                        , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                                                        , UDFLib.ConvertStringToNull((Session["sQuotRec"]))
                                                        , UDFLib.ConvertStringToNull((Session["sPendingApprover"]))
                                                        , UDFLib.ConvertStringToNull(Session["USERID"])
                                                        , null, null, ref  rowcount, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));
                break;
                case "RPO":
                dt = objPendingReqsn.SelectPendingPurchasedOrderRaise_New(UDFLib.ConvertStringToNull(Session["sFleet"])
                            , UDFLib.ConvertStringToNull(Session["sVesselCode"])
                            , UDFLib.ConvertStringToNull(Session["sDeptType"])
                            , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                            , UDFLib.ConvertStringToNull(Session["REQNUM"])
                            , UDFLib.ConvertStringToNull(Session["sPOType"])
                            , UDFLib.ConvertStringToNull(Session["sAccType"])
                            , UDFLib.ConvertStringToNull(Session["ReqsnType"])
                            , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                            , UDFLib.ConvertDateToNull(Session["sFrom"])
                            , UDFLib.ConvertDateToNull(Session["sTO"])
                            , UDFLib.ConvertStringToNull(Session["sAccClass"])
                            , UDFLib.ConvertStringToNull(Session["sUrgency"])
                            , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                            , UDFLib.ConvertStringToNull((Session["sPort"]))
                            , null, null, ref  rowcount, sortbycoloumn, sortdirection);
                break;
                case "SCN":
                dt = objPendingReqsn.SelectPendingPOConfirm_New(UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                , UDFLib.ConvertStringToNull(Session["sPOType"])
                                , UDFLib.ConvertStringToNull(Session["sAccType"])
                                , UDFLib.ConvertStringToNull(Session["ReqsnType"])
                                , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                , UDFLib.ConvertDateToNull(Session["sFrom"])
                                , UDFLib.ConvertDateToNull(Session["sTO"])
                                , UDFLib.ConvertStringToNull(Session["sAccClass"])
                                , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                                , UDFLib.ConvertStringToNull((Session["Supplier"]))
                                , null, null, ref  rowcount, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));
                break;
                case "DVS":
                dt = objPendingReqsn.SelectRequisitionDeliveryStatus_Export(null,
                                                                                      null,
                                                                                     UDFLib.ConvertIntegerToNull(Session["PURC_DeliveryPort"]),
                                                                                     (DataTable)Session["PURC_CurrentStage"],
                                                                                     (DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"],
                                                                                     (DataTable)Session["sDeptCode"],
                                                                                     UDFLib.ConvertStringToNull(Session["sDeptType"]),
                                                                                     UDFLib.ConvertStringToNull(Session["REQNUM"]),
                                                                                     UDFLib.ConvertIntegerToNull(Session["sReqsnType"]),
                                                                                     (DataTable)Session["PURC_Supplier_Name"],
                                                                                     UDFLib.ConvertDateToNull(Session["PURC_PO_Date"]),
                                                                                     UDFLib.ConvertDateToNull(Session["PURC_TO_PO_Date"]),
                                                                                     null,
                                                                                     null,
                                                                                     ref rowcount);

                break;
                case "UPD":
                dt = objPendingReqsn.SelectPendingDeliveryUpdate((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"],
                                                                                     (DataTable)Session["sDeptCode"],
                                                                                     UDFLib.ConvertStringToNull(Session["sDeptType"]),
                                                                                     UDFLib.ConvertStringToNull(Session["REQNUM"]),
                                                                                     UDFLib.ConvertIntegerToNull(Session["sReqsnType"]),
                                                                                     null,
                                                                                     null,
                                                                                     ref rowcount);
                break;
                case "ALL":
                dt = objPendingReqsn.SelectAllRequisitionStages((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"],
                                                                                  (DataTable)Session["sDeptCode"],
                                                                                 UDFLib.ConvertStringToNull(Session["sDeptType"]),
                                                                                 UDFLib.ConvertStringToNull(Session["REQNUM"]),
                                                                                 UDFLib.ConvertIntegerToNull(Session["sReqsnType"]),
                                                                                 UDFLib.ConvertStringToNull(Session["PURC_ReqsnStatusATAll"]),
                                                                                null,
                                                                                null,
                                                                                ref rowcount, sortbycoloumn, sortdirection);
                break;
                case "CAN":
                dt = BLL_PURC_Common.Get_CancelReqsn((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
                                , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
                                , UDFLib.ConvertIntegerToNull(Session["sReqsnType"].ToString()), UDFLib.ConvertStringToNull(Session["REQNUM"].ToString())
                                , null, null, ref  rowcount, sortbycoloumn, sortdirection);
                break;
                case "DLV":
                dt = BLL_PURC_Common.Get_Delivered_Requisition_Stage((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
                                                                               , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
                                                                               , UDFLib.ConvertIntegerToNull(Session["sReqsnType"].ToString())
                                                                               , UDFLib.ConvertStringToNull(Session["REQNUM"].ToString()), null
                                                                               , null, null, ref rowcount, sortbycoloumn, sortdirection);
                break;
            };
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void txtfrom_TextChanged(object sender, EventArgs e)
    {
        if (txtfrom.Text != string.Empty)
        {
            Session["sFrom"] = txtfrom.Text.Trim();
            BindGrid();
        }
        else
        {
            Session["sFrom"] = null;
        }
    }
    protected void txtTo_TextChanged(object sender, EventArgs e)
    {
        if (txtTo.Text != string.Empty)
        {
            Session["sTO"] = txtTo.Text.Trim();
            BindGrid();
        }
        else
        {
            Session["sTO"] = null;
        }
    }
    /// <summary>
    /// Bind Data to Main Grid.
    /// </summary>
    private void BindGrid()
    {
        BLL_PURC_Purchase objPendingReqsn = new BLL_PURC_Purchase();
        DataTable dt = new DataTable();
        string[] HeaderCaptions = { };
        string[] DataColumnsName = { };
        try
        {
            if (txtReqsnOrderNo.Text.Trim() != string.Empty)
            {
                Session["REQNUM"] = Convert.ToString(txtReqsnOrderNo.Text.Trim());
            }
            else
            {
                Session["REQNUM"] = null;
            }
            int rowcount = 0;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            string sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Convert.ToString(ViewState["SORTDIRECTION"]);

            switch (ViewState["reqsnStageCode"].ToString())
            {
                case "NRQ":
                    dt = objPendingReqsn.SelectNewRequisitionList_New
                                       (UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                       , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                       , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                       , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                       , UDFLib.ConvertStringToNull(Session["sPOType"])
                                       , UDFLib.ConvertStringToNull(Session["sAccType"])
                                       , UDFLib.ConvertStringToNull(Session["ReqsnType"])
                                       , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                       , UDFLib.ConvertDateToNull(Session["sFrom"])
                                       , UDFLib.ConvertDateToNull(Session["sTO"])
                                       , UDFLib.ConvertStringToNull(Session["sAccClass"])
                                       , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                       , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                                       , null, null, ref  rowcount, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));
                    break;
                case "RFQ":
                    dt = objPendingReqsn.SelectPendingRequistion_New(
                                UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                , UDFLib.ConvertStringToNull(Session["sPOType"])
                                , UDFLib.ConvertStringToNull(Session["sAccType"])
                                , UDFLib.ConvertStringToNull(Session["sReqsnType"])
                                , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                , UDFLib.ConvertDateToNull(Session["sFrom"])
                                , UDFLib.ConvertDateToNull(Session["sTO"])
                                , UDFLib.ConvertStringToNull(Session["sAccClass"])
                                , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                , UDFLib.ConvertStringToNull((Session["sMinQuot"]))
                                , UDFLib.ConvertToInteger((Session["sReqsnStatus"]))

                                , null, null, ref  rowcount, sortbycoloumn, sortdirection);
                    break;
                case "QEV":
                    int? LogedIn_User = null;
                    if (Session["SHOWALL_PENDING_APPROVAL"].ToString() == "1")
                    {
                        LogedIn_User = Convert.ToInt32(Session["USERID"].ToString());

                    }
                    dt = objPendingReqsn.SelectPendingQuatationEvalution_New
                                                            (UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                                            , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                                            , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                                            , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                                            , UDFLib.ConvertStringToNull(Session["sPOType"])
                                                            , UDFLib.ConvertStringToNull(Session["sAccType"])
                                                            , UDFLib.ConvertStringToNull(Session["ReqsnType"])
                                                            , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                                            , UDFLib.ConvertDateToNull(Session["sFrom"])
                                                            , UDFLib.ConvertDateToNull(Session["sTO"])
                                                            , UDFLib.ConvertStringToNull(Session["sAccClass"])
                                                            , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                                            , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                                                            , UDFLib.ConvertStringToNull((Session["sQuotRec"]))
                                                            , UDFLib.ConvertStringToNull((Session["sPendingApprover"]))
                                                            , UDFLib.ConvertStringToNull(Session["USERID"])
                                                            , null, null, ref  rowcount, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));
                    break;
                case "RPO":
                    dt = objPendingReqsn.SelectPendingPurchasedOrderRaise_New(UDFLib.ConvertStringToNull(Session["sFleet"])
                                , UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                , UDFLib.ConvertStringToNull(Session["sPOType"])
                                , UDFLib.ConvertStringToNull(Session["sAccType"])
                                , UDFLib.ConvertStringToNull(Session["ReqsnType"])
                                , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                , UDFLib.ConvertDateToNull(Session["sFrom"])
                                , UDFLib.ConvertDateToNull(Session["sTO"])
                                , UDFLib.ConvertStringToNull(Session["sAccClass"])
                                , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                                , UDFLib.ConvertStringToNull((Session["sPort"]))
                                , null, null, ref  rowcount, sortbycoloumn, sortdirection);
                    break;
                case "SCN":
                    dt = objPendingReqsn.SelectPendingPOConfirm_New(UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                    , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                    , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                    , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                    , UDFLib.ConvertStringToNull(Session["sPOType"])
                                    , UDFLib.ConvertStringToNull(Session["sAccType"])
                                    , UDFLib.ConvertStringToNull(Session["ReqsnType"])
                                    , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                    , UDFLib.ConvertDateToNull(Session["sFrom"])
                                    , UDFLib.ConvertDateToNull(Session["sTO"])
                                    , UDFLib.ConvertStringToNull(Session["sAccClass"])
                                    , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                    , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                                    , UDFLib.ConvertStringToNull((Session["Supplier"]))
                                    , null, null, ref  rowcount, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));
                    break;
                case "DVS":
                    dt = objPendingReqsn.SelectRequisitionDeliveryStatus_Export(null,
                                                                                          null,
                                                                                         UDFLib.ConvertIntegerToNull(Session["PURC_DeliveryPort"]),
                                                                                         (DataTable)Session["PURC_CurrentStage"],
                                                                                         (DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"],
                                                                                         (DataTable)Session["sDeptCode"],
                                                                                         UDFLib.ConvertStringToNull(Session["sDeptType"]),
                                                                                         UDFLib.ConvertStringToNull(Session["REQNUM"]),
                                                                                         UDFLib.ConvertIntegerToNull(Session["sReqsnType"]),
                                                                                         (DataTable)Session["PURC_Supplier_Name"],
                                                                                         UDFLib.ConvertDateToNull(Session["PURC_PO_Date"]),
                                                                                         UDFLib.ConvertDateToNull(Session["PURC_TO_PO_Date"]),
                                                                                         null,
                                                                                         null,
                                                                                         ref rowcount);

                    break;
                case "UPD":
                    dt = objPendingReqsn.SelectPendingDeliveryUpdate((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"],
                                                                                         (DataTable)Session["sDeptCode"],
                                                                                         UDFLib.ConvertStringToNull(Session["sDeptType"]),
                                                                                         UDFLib.ConvertStringToNull(Session["REQNUM"]),
                                                                                         UDFLib.ConvertIntegerToNull(Session["sReqsnType"]),
                                                                                         null,
                                                                                         null,
                                                                                         ref rowcount);
                    break;
                case "ALL":
                    dt = objPendingReqsn.SelectAllRequisitionStages((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"],
                                                                                      (DataTable)Session["sDeptCode"],
                                                                                     UDFLib.ConvertStringToNull(Session["sDeptType"]),
                                                                                     UDFLib.ConvertStringToNull(Session["REQNUM"]),
                                                                                     UDFLib.ConvertIntegerToNull(Session["sReqsnType"]),
                                                                                     UDFLib.ConvertStringToNull(Session["PURC_ReqsnStatusATAll"]),
                                                                                    null,
                                                                                    null,
                                                                                    ref rowcount, sortbycoloumn, sortdirection);
                    break;
                case "CAN":
                    dt = BLL_PURC_Common.Get_CancelReqsn((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
                                    , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
                                    , UDFLib.ConvertIntegerToNull(Session["sReqsnType"].ToString()), UDFLib.ConvertStringToNull(Session["REQNUM"].ToString())
                                    , null, null, ref  rowcount, sortbycoloumn, sortdirection);
                    break;
                case "DLV":
                    dt = BLL_PURC_Common.Get_Delivered_Requisition_Stage((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
                                                                                   , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
                                                                                   , UDFLib.ConvertIntegerToNull(Session["sReqsnType"].ToString())
                                                                                   , UDFLib.ConvertStringToNull(Session["REQNUM"].ToString()), null
                                                                                   , null, null, ref rowcount, sortbycoloumn, sortdirection);
                    break;
            };
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
}








