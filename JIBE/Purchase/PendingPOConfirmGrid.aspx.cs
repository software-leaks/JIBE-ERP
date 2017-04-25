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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text;
using ClsBLLTechnical;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using System.IO;

public partial class Technical_INV_PendingPOConfirmGrid : System.Web.UI.Page
{
    public string sRequiPendingType = "SCN";
    
    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch { }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            hdnHost.Value = Request.Url.AbsoluteUri.ToString().Substring(0, Request.Url.AbsoluteUri.ToString().ToLower().IndexOf("/purchase/pendingpoconfirmgrid.aspx")) + "/";

            UserAccessValidation();
            ViewState["NeedDataSource"] = "0";

            dvCancelPO.Visible = false;
            dvcancelPOItems.Visible = false;
            divReqStages.Visible = false;
            if (!IsPostBack)
            {
                Session["PoolSelection"] = Request.QueryString["type"].ToString();

                divOnHold.Visible = false;


                BindData();
                ucPurc_Rollback_Reqsn1.BindRequisitionStatus(sRequiPendingType);
            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/useraccess.htm");

        if (objUA.Add == 0)
        {


        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }


    }
    public void BindData()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = (ViewState["SORTDIRECTION"].ToString());


        BLL_PURC_Purchase objPurcBLL = new BLL_PURC_Purchase();

        DataTable dt = objPurcBLL.SelectPendingPOConfirm_New(UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                , UDFLib.ConvertStringToNull(Session["sPOType"])
                                , UDFLib.ConvertStringToNull(Session["sAccType"])
                                , UDFLib.ConvertStringToNull(Session["ReqsnType"])
                                , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                , UDFLib.ConvertDateToNull(Session["sFrom"])
                                , UDFLib.ConvertDateToNull(Session["sTO"])
                                , UDFLib.ConvertStringToNull(Session["accclass"])
                                , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                , UDFLib.ConvertStringToNull((Session["reqsnstatus"]))
                                ,UDFLib.ConvertStringToNull((Session["Supplier"]))
                                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));

        //DataTable dt = objPurcBLL.SelectPendingPOConfirm((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
        // , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
        // , UDFLib.ConvertIntegerToNull(Session["ReqsnType"].ToString()), UDFLib.ConvertStringToNull(Session["REQNUM"].ToString())
        // , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, sortbycoloumn, sortdirection);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        rgdPOConfirm.DataSource = dt;

        if (ViewState["NeedDataSource"] == "0")
            rgdPOConfirm.DataBind();

        string script = " var height = document.body.scrollHeight;parent.ResizeFromChild(height,'1');";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "resize" + DateTime.Now.Millisecond.ToString(), script, true);

    }

    protected void rgd_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            ViewState["NeedDataSource"] = "1";

            BindData();

        }
    }



    protected void btndivCancel_Click(object sender, EventArgs e)
    {
        divOnHold.Visible = false;
    }

    //private void BindRequisitionStatus(string sRequiPendingType)
    //{

    //    using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
    //    {
    //        DataTable dtREQStatus = objTechService.GetREQStatus();
    //        DataRow[] filterRows = dtREQStatus.Select("short_code ='" + sRequiPendingType + "'");
    //        dtREQStatus.DefaultView.RowFilter = "code < '" + filterRows[0][0].ToString() + "'";
    //        DDLReqStages.DataSource = dtREQStatus.DefaultView;
    //        DDLReqStages.DataTextField = "Description";
    //        DDLReqStages.DataValueField = "short_code";
    //        DDLReqStages.DataBind();


    //    }
    //}

    protected void onSelectAttachment(object source, CommandEventArgs e)
    {
        divOnHold.Visible = false;
        divReqStages.Visible = false;
        String msg = "CheckFileAndOpen('" + ((ImageButton)source).ValidationGroup + "')";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString(), msg, true);
        if (((ImageButton)source).ValidationGroup == "")
        {
            ResponseHelper.Redirect("FileAttachmentInfo.aspx?Requisitioncode=" + e.CommandArgument.ToString(), "Blank", "");
        }
    }

    protected void btndivReqPrioCancel_Click(object sender, EventArgs e)
    {
        divReqStages.Visible = false;
    }


    protected void onSelect(object source, CommandEventArgs e)
    {


    }

    protected void onSendPO(object source, CommandEventArgs e)
    {


    }

    protected void btnOnHold_Click(object sender, EventArgs e)
    {
        try
        {
            HoldUnHold.Remarks = "";
            divReqStages.Visible = false;
            divOnHold.Visible = true;

            GridDataItem dataItem = (GridDataItem)((ImageButton)sender).Parent.Parent;
            ImageButton btnOnHold = (ImageButton)(dataItem.FindControl("btnOnHold") as ImageButton);

            string sOnHoldFlag = dataItem["OnHold"].Text.ToString();
            if (sOnHoldFlag.ToString() == "False")
            {
                btnOnHold.ImageUrl = "~/purchase/Image/OnHold.png";
                HoldUnHold.lblHeader = "Hold the Requisition";
            }
            else
            {
                btnOnHold.ImageUrl = "~/purchase/Image/release.png";
                HoldUnHold.lblHeader = "Hold the Requisition";
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void OnHold(object sender, CommandEventArgs e)
    {
        try
        {
            HiddenArgument.Value = e.CommandArgument.ToString();
            string[] strArgs = HiddenArgument.Value.Split(',');
            BLL_PURC_Purchase objhold = new BLL_PURC_Purchase();
            HoldUnHold.DTLog = objhold.GetRequisitionOnHoldLogHistory_ByReqsn(strArgs[0]);
            HoldUnHold.BindLog();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void OnConfirm(object sender, CommandEventArgs e)
    {
        try
        {
            DataTable dtQuotationList = new DataTable();
            dtQuotationList.Columns.Add("Qtncode");
            dtQuotationList.Columns.Add("amount");

            string[] strIds = e.CommandArgument.ToString().Split(',');
            string reqCode = strIds[0].ToString();
            string DocumentCode = strIds[1].ToString();
            string vesselCode = strIds[2].ToString();
            string SupplierCode = strIds[3].ToString();
            string OrdCode = strIds[4].ToString();
            string QuotCode = strIds[5].ToString();

            DataRow dtrow = dtQuotationList.NewRow();
            dtrow[0] = strIds[5].ToString();
            dtrow[1] = "0";
            dtQuotationList.Rows.Add(dtrow);

            TechnicalBAL objTechBAL = new TechnicalBAL();
            int insRec = objTechBAL.UpdatePOConfirmation(reqCode, DocumentCode, SupplierCode, vesselCode, OrdCode, QuotCode);
            BLL_PURC_Purchase objPurc = new BLL_PURC_Purchase();
            objPurc.InsertRequisitionStageStatus(reqCode, vesselCode, DocumentCode, "UPD", " ", Convert.ToInt32(Session["USERID"]), dtQuotationList);
            String msg = String.Format("alert('Order has been Confirmed'); window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

            ucCustomPagerItems.isCountRecord = 1;
            BindData();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }

    protected void btndivSave_Click(object sender, EventArgs e)
    {
        if (HoldUnHold.Remarks != "")
        {
            DataSet dsSendMailInfo = new DataSet();
            string[] Attchment = new string[10];
            string[] strArgs = HiddenArgument.Value.Split(',');
            string sRequisitionCode = strArgs[0];
            string sDocumentCode = strArgs[1];
            string sVessel_Code = strArgs[2];
            string sLinkType = sRequiPendingType;
            string sOnHold = strArgs[4];
            string sOnHoldName = "";
            string sRemarks = HoldUnHold.Remarks;
            string sSupplier = "C";

            if (sOnHold.ToString() == "False")
            {
                sOnHold = "1";
            }
            else
            {
                sOnHold = "0";
            }
            if (sOnHold == "1")
                sOnHoldName = "hold";
            else
                sOnHoldName = "un hold";

            try
            {
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    int iReturn = objTechService.InsRequisitionOnHoldLogHistory(sRequisitionCode, sVessel_Code, sDocumentCode, sLinkType, sOnHold, sRemarks, sSupplier, Convert.ToInt32(Session["userid"].ToString()));
                    if (iReturn == 1)
                    {
                        DataTable dtQuotationList = new DataTable();
                        dtQuotationList.Columns.Add("Qtncode");
                        dtQuotationList.Columns.Add("amount");

                        BLL_PURC_Common.INS_Remarks(sDocumentCode, Convert.ToInt32(Session["userid"].ToString()), sRemarks, 306);
                        objTechService.InsertRequisitionStageStatus(sRequisitionCode, sVessel_Code, sDocumentCode, sOnHold, sRemarks, Convert.ToInt32(Session["USERID"]), dtQuotationList);
                        divOnHold.Visible = false;
                        String msg = String.Format("alert('Requisition POC has been marked on " + sOnHoldName + "'); ");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                        BindData();
                    }

                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
        }
        else
        {
            String msg = String.Format("alert('Remark is mandatory field.'); ");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }

    protected void OnCancelReq(object sender, CommandEventArgs e)
    {
        try
        {
            string OrderSupp = ((ImageButton)sender).AlternateText;
            divOnHold.Visible = false;
            string[] strArgs = e.CommandArgument.ToString().Split(',');
            string sOnHold = strArgs[4];
            if (sOnHold.ToString() == "False")
            {
                divReqStages.Visible = true;
                divOnHold.Visible = false;

                string ReqsnCode = e.CommandArgument.ToString().Split(new char[] { ',' })[0];
                HiddenArgument.Value = e.CommandArgument.ToString();
                Session["ReqsnCancelLog"] = ReqsnCode;
                ucPurc_Rollback_Reqsn1.BindGrid();
                ucPurc_Rollback_Reqsn1.RequisitionCode = ReqsnCode;
                ucPurc_Rollback_Reqsn1.Order_Code = OrderSupp.Split('~')[0];
                ucPurc_Rollback_Reqsn1.HRef = "~/purchase/POPreview.aspx?RFQCODE=" + ReqsnCode + "&Vessel_Code=" + strArgs[2] + "&Order_Code=" + OrderSupp.Split('~')[0];
                ucPurc_Rollback_Reqsn1.SupplierName = OrderSupp.Split('~')[1];
            }
            else
            {
                divReqStages.Visible = false;
                String msg = String.Format("alert('This requisition has been marked as OnHold.'); ");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btndivReqprioOK_Click(object sender, EventArgs e)
    {

        if (ucPurc_Rollback_Reqsn1.Reason != "")
        {
            string[] strArgs = HiddenArgument.Value.Split(',');
            string sRequisitionCode = strArgs[0];
            string sDocumentCode = strArgs[1];
            string sVessel_Code = strArgs[2];
            string sLinkType = sRequiPendingType;
            string sOnHold = strArgs[4];
            string sQuotationCode = strArgs[5];
            string sReason = ucPurc_Rollback_Reqsn1.Reason;
            string sCanceledOnStage = "";
            string sRemarks = ucPurc_Rollback_Reqsn1.Reason;
            sCanceledOnStage = ucPurc_Rollback_Reqsn1.StageValue;

            try
            {
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {

                    int iReturn = objTechService.CancelRequisitionStages(Convert.ToInt32(sVessel_Code), sRequisitionCode, sDocumentCode, sCanceledOnStage, sReason, Convert.ToInt32(Session["userid"].ToString()), Request.QueryString["type"].ToString(), sQuotationCode);
                    if (iReturn > 0)
                    {
                        BLL_PURC_Common.INS_Remarks(sDocumentCode, Convert.ToInt32(Session["userid"].ToString()), sReason, 305);
                        divOnHold.Visible = false;
                        String msg = String.Format("alert('Requisition has been shifted on " + ucPurc_Rollback_Reqsn1.StageText + "'); ");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                        ucCustomPagerItems.isCountRecord = 1;
                        BindData();
                        #region To Generate Cancel PO
                        if (sCanceledOnStage == "QEV" || sCanceledOnStage == "RFQ")
                        {
                            String funname = "GeneratePDf();";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "funname", funname, true);
                        }
                        #endregion
                    }
                    else if (iReturn == -10001)
                    {
                        String msg = String.Format("alert('action can not be completed because invoice has been uploaded');");

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                    }

                }
                divReqStages.Visible = false;
            }
            catch (Exception ex)
            {
                String msg = String.Format("alert('" + ex.Message + "')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

            }
        }
        else
        {
            String msg = String.Format("alert('Reason is mandatory field.'); ");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }


    }

    protected void rgdPOConfirm_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            ImageButton ImgPriority = (ImageButton)(item.FindControl("ImgPriority") as ImageButton);

            if (!string.IsNullOrWhiteSpace(item["URGENCY_CODE"].Text))
            {
                if (item["URGENCY_CODE"].Text == "Urgent")
                {
                    ImgPriority.ImageUrl = "~/Images/exclamation.gif";
                    ImgPriority.ToolTip = "Urgent";
                }
                if (item["URGENCY_CODE"].Text == "Immediate")
                {
                    ImgPriority.ImageUrl = "~/Images/double_Exclamation.png";
                    ImgPriority.ToolTip = item["URGENCY_CODE"].Text;
                }
            }
            else
            {
                ImgPriority.ImageUrl = "~/purchase/Image/transparent.gif";
            }

            ImageButton ImgAttach = (ImageButton)(item.FindControl("ImgAttachment") as ImageButton);
            string strAttFlag = item["Attach_Status"].Text.ToString();
            if (strAttFlag == "0")
            {
                ImgAttach.ImageUrl = "~/images/Attach.png";
                //ImgAttach.ImageUrl = "~/purchase/Image/transparent.gif";
                //ImgAttach.Enabled = false;
                ImgAttach.ToolTip = "Add Attachments";
                ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('Add Attachments' ,'rgdPOConfirm');");
            }
            else
            {
                ImgAttach.ImageUrl = "~/images/attachment32.png";
                //ImgAttach.ImageUrl = "~/purchase/Image/attach1.gif";
                ImgAttach.ToolTip = "View Attahments";
                ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('View Attachments' ,'rgdPOConfirm');");
            }

            ImageButton btnOnHold = (ImageButton)(item.FindControl("btnOnHold") as ImageButton);
            string sOnHoldFlag = item["OnHold"].Text.ToString();
            if (sOnHoldFlag.ToString() == "False")
            {
                btnOnHold.ImageUrl = "~/purchase/Image/OnHold.png";
                btnOnHold.ToolTip = "Put on Hold";
            }
            else
            {
                btnOnHold.ImageUrl = "~/purchase/Image/release.png";
                btnOnHold.ToolTip = "Cancel Hold";
            }
            #region Requisition containing critical Items requisition code should display in red color.

            Label lblCriticalFlag = (Label)(item.FindControl("lblCriticalFlag"));
            HyperLink hlinkReq = new HyperLink();
            if (lblCriticalFlag.Text == "1")
            {
                hlinkReq = (HyperLink)item.FindControl("hlinkReq");
                hlinkReq.ForeColor = System.Drawing.Color.Red;
            }

            #endregion
        }
    }

    protected void rgdPOConfirm_DataBound(object sender, EventArgs e)
    {
        RadGrid grid = sender as RadGrid;
        int gridItems = grid.MasterTableView.Items.Count;
        if (gridItems < 12)
        {
            grid.ClientSettings.Scrolling.ScrollHeight = Unit.Pixel(gridItems * 24);
        }
    }

    protected void onAddNewItem(object sender, CommandEventArgs e)
    {
        ResponseHelper.Redirect("AddNotListItems.aspx?ReqCode=" + e.CommandArgument.ToString(), "Blank", "");
    }

    /// <summary>
    /// Below Function does Cancel PO functionality.
    /// Modified  By :Pranali_1/10/2016_JIT_11377
    /// Added Validation for requisition status as  On Hold po's should not able to cancel .
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e">
    /// CommandArgument Param 1 : Order Code 
    /// CommandArgument Param 2:  OnHold =True or False.
    /// </param>
    protected void btnCancelPO_Click(object s, EventArgs e)
    {
        try
        {
            string[] strArgs = ((ImageButton)s).CommandArgument.Split(',');
            string sOnHold = strArgs[1];
            if (sOnHold.ToString() == "False")
            {
                HiddenOrderCode.Value = strArgs[0];//((ImageButton)s).CommandArgument;
                hdfDocumentcode.Value = ((ImageButton)s).ToolTip;
                lblCancelPO.Text = HiddenOrderCode.Value;
                dvCancelPO.Visible = true;
            }
            else
            {
                divReqStages.Visible = false;
                String msg = String.Format("alert('This requisition has been marked as OnHold.'); ");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnCancelPOItems_Click(object s, EventArgs e)
    {
        try
        {
            HiddenOrderCode.Value = ((ImageButton)s).CommandArgument;
            hdfDocumentcode.Value = ((ImageButton)s).ToolTip;
            dvcancelPOItems.Visible = true;
            lblCancelPOItems.Text = HiddenOrderCode.Value;
            gvPOItems.DataSource = BLL_PURC_Common.Get_POItem_ToCancel(HiddenOrderCode.Value);
            gvPOItems.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnCancelPOSave_Click(object s, EventArgs e)
    {
        try
        {
            BLL_PURC_Common.UPDATE_CancelPO(HiddenOrderCode.Value, txtRemarkPO.Text.Trim(), UDFLib.ConvertToInteger(Session["USERID"].ToString()));
            BLL_PURC_Common.INS_Remarks(hdfDocumentcode.Value, Convert.ToInt32(Session["userid"].ToString()), txtRemarkPO.Text.Trim(), 308);
            ucCustomPagerItems.isCountRecord = 1;
            BindData();
            //String msg = String.Format("GeneratePDf('" + Request.QueryString["RFQCODE"] + "," + Request.QueryString["Vessel_Code"] + "," + Request.QueryString["Order_Code"] + "," + Request.QueryString["Document_Code"] + "'); ");
            #region Generate PDF
            String msg = "GeneratePDf();";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg15", msg, true);
            #endregion
            String msg1 = String.Format("alert('This requisition has been shifted to RFQ stage.'); ");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg1", msg1, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        txtRemarkPO.Text = "";
    }

    protected void btnCancelPOCancel_Click(object s, EventArgs e)
    {
        try
        {
            txtRemarkPO.Text = "";
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnCancellPOItemsSave_Click(object s, EventArgs e)
    {
        try
        {
            StringBuilder itemID = new StringBuilder();
            int cntItem = 0;
            int POItemCount = gvPOItems.Rows.Count;
            foreach (GridViewRow gr in gvPOItems.Rows)
            {
                CheckBox chItem = ((CheckBox)gr.FindControl("chkCencel"));
                if (chItem.Checked == true)
                {
                    itemID.Append("  SELECT  ");
                    itemID.Append(chItem.ToolTip);
                    cntItem++;
                }
            }
            BLL_PURC_Common.UPDATE_CancelPOItems(HiddenOrderCode.Value, txtRemarkPO.Text.Trim(), UDFLib.ConvertToInteger(Session["USERID"].ToString()), itemID.ToString());

            #region Cancel Po If all items are cancelled.
            ///#ref JIT:11883 : If all items are cancelled in Po then that po will be considered as cancelled and will move to RFQ stage.
            if (POItemCount == cntItem)
            {
                BLL_PURC_Common.UPDATE_CancelPO(HiddenOrderCode.Value, txtRemarkPO.Text.Trim(), UDFLib.ConvertToInteger(Session["USERID"].ToString()));
                #region Generate PDF
                String msg = "GeneratePDf();";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg96", msg, true);
                #endregion
                String msg1 = String.Format("alert('This requisition has been shifted to RFQ stage.'); ");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg1", msg1, true);
                ///End
            }
            #endregion
            BLL_PURC_Common.INS_Remarks(hdfDocumentcode.Value, Convert.ToInt32(Session["userid"].ToString()), txtRemarkPO.Text.Trim(), 308);
            ucCustomPagerItems.isCountRecord = 1;
            BindData();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        txtRemarkPOItems.Text = "";
    }

    protected void btnCancelPOItemsCancel_Click(object s, EventArgs e)
    {
        try
        {
            txtRemarkPOItems.Text = "";
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ImgbtnReSendPO_Click(object s, CommandEventArgs e)
    {
        try
        {
            string[] strIds = e.CommandArgument.ToString().Split(',');
            string Requisitioncode = strIds[0].ToString();
            string Document_Code = strIds[1].ToString();
            string Vessel_Code = strIds[2].ToString();
            string SUPPLIER = strIds[3].ToString();
            string OrderCode = strIds[4].ToString();
            string QUOTATION_CODE = strIds[5].ToString();


            string strPath = Server.MapPath(".") + "\\SendPO\\";
            DataSet DsPO = new DataSet();
            DataSet dsSendMailInfo = new DataSet();

            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            DataTable dtUser = objUser.Get_UserDetails(Convert.ToInt32(Session["USERID"]));

            string emailIDcc = dtUser.Rows[0]["MailID"].ToString();


            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DsPO = objTechService.GetDataToGeneratPO(Requisitioncode, Vessel_Code, SUPPLIER, QUOTATION_CODE, Document_Code);

                dsSendMailInfo = objTechService.GetRFQsuppInfoSendEmail(SUPPLIER, QUOTATION_CODE, Vessel_Code, Document_Code, Session["userid"].ToString());
                string FileName = "PO_" + Vessel_Code + "_" + OrderCode + "_" + SUPPLIER + DateTime.Now.ToString("yyMMddss") + ".pdf";
                string sToEmailAddress = "", strSubject = "", strEmailBody = "";
                //Generate the PDF file and check the include amount status


                DataTable dtPO = DsPO.Tables[0];



                GeneratePOAsPDF(dtPO, strPath, FileName);


                FormateEmail(dsSendMailInfo, out  sToEmailAddress, out  strSubject, out strEmailBody, false, OrderCode);

                BLL_Crew_CrewDetails objMail = new BLL_Crew_CrewDetails();
                int MailID = 0;
                MailID = objMail.Send_CrewNotification(0, 0, 0, 0, sToEmailAddress, emailIDcc, "", strSubject, strEmailBody, "", "MAIL", "", UDFLib.ConvertToInteger(Session["USERID"].ToString()), "DRAFT");


                string UploadFilePath = ConfigurationManager.AppSettings["PURC_UPLOAD_PATH"];
                //string uploadpath = @"\\server01\uploads\Purchase";
                string uploadpath = @"uploads\Purchase";
                BLL_Infra_Common.Insert_EmailAttachedFile(MailID, FileName, uploadpath + @"\" + FileName);


                string URL = String.Format("window.open('../crew/EmailEditor.aspx?ID=+" + MailID.ToString() + @"&FILEPATH=" + UploadFilePath + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "k" + MailID.ToString(), URL, true);


            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }
    /// <summary>
    /// JIT-11845
    /// Add vessel owner name and vessel owner address as per JIT
    /// Change signature of email template (Add Company Name) 
    /// </summary>
    /// <param name="dsEmailInfo"></param>
    /// <param name="sEmailAddress"></param>
    /// <param name="strSubject"></param>
    /// <param name="strBody"></param>
    /// <param name="IsPendingApprovalPO"></param>
    /// <param name="OrderCode"></param>
    protected void FormateEmail(DataSet dsEmailInfo, out string sEmailAddress, out string strSubject, out string strBody, bool IsPendingApprovalPO, string OrderCode)
    {
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        DataTable dtUser = objUser.Get_UserDetails(Convert.ToInt32(Session["USERID"]));

        string ServerIPAdd = ConfigurationManager.AppSettings["WebQuotSite"].ToString();

        strBody = @"Dear " + dsEmailInfo.Tables[0].DefaultView[0]["SHORT_NAME"].ToString() + @"<br><br>
                     We are pleased to attach our Purchase Order for delivery of the goods quoted per your Supplier reference:" + dsEmailInfo.Tables[0].DefaultView[0]["Supplier_Quotation_Reference"].ToString() + @"  for M.V.:" + dsEmailInfo.Tables[0].DefaultView[0]["Vessel_Name"].ToString() + @".<br><br>
                    The purchase order can be downloaded from the following link   <br>
             <a href='" + ServerIPAdd.Trim() + "'>" + ServerIPAdd.Trim() + @"</a> <br>
                    User Name&nbsp;:  " + dsEmailInfo.Tables[0].Rows[0]["User_name"].ToString() + @"<br> 
                    Password&nbsp;&nbsp; :  " + dsEmailInfo.Tables[0].Rows[0]["Password"].ToString() + @" <br><br>
                   <span style='color:Red'> “ANNOUNCEMENT OF NEW POLICY” </span><br>   
                    With effect from 12th Feb 2013, It is mandate to obtain only Master/ Chief Engineer’s signature and  vessel stamp on any “DELIVERY NOTE” for orders delivered on board. <br>
                    Stores Suppliers - delivering to third party/forwarder must obtain the vessel signed Delivery Note from the c/o parties.<br>
                    Supplier confirms that " + Session["Company_Name_GL"].ToString() + @" will not pay for any delivers that are not confirmed in accordance with the above condition.

                   <br><br/> <b> KINDLY ACKNOWLEDGE OUR PO WITH DELIVERY DAYS / READINESS DATE VIA THE ABOVE LINK WITHIN SAME WORKING DAY.</b><br><br>

                    Please ensure accurate and complete delivery of this order to the agent, Forwarder or vessel. <br><br>
                    If there are partial deliveries, the sender of the Purchase Order we must be informed immediately via email.  <br><br>
                    Following packing and documentation procedures to adhere strictly; <br>
                    1 To pack in airworthy packing (strictly in carton box) <br>
                    2. Markings ON ALL PACKAGES: Address to vessel name :" + dsEmailInfo.Tables[0].DefaultView[0]["Vessel_Name"].ToString() + @" and our purchase order number:" + dsEmailInfo.Tables[0].DefaultView[0]["ORDER_CODE"].ToString() + @" and vessel department____________ (eg. Engine Stores, Cabin Stores, Electrical stores, etc)<br>
                    3. Pls indicate the dim (LXBXH in CM), cargo weight, no.of pieces, value of goods on the manifest invoice and packing list or delivery note 
                    4 . Kindly enclose 02 copies of delivery note into the carton/crate for ship's checking <br>
                    5. Original Invoice and Original Delivery Note (with original company stamp and signature of receiving staff of our forwarder) to be send to " + Session["Company_Name_GL"] + @" by email in PDF format.<br><br>

                    Any inaccuracies, missing or damaged items will result in a delayed payment <br>
                    of your invoice. The PO number must be indicated in the invoice for <br>
                    prompt processing and  payment. <br><br>
                    IMPORTANT REMARK: All invoices must be issued as follows:<br>
                    M/V..<br>
                    " + dsEmailInfo.Tables[0].Rows[0]["Owner_Name"].ToString() + @"<br>
                    " + dsEmailInfo.Tables[0].Rows[0]["Owner_Address"].ToString().Replace("\n", "<br>") + @"<br><br>
                    Thank you for your co-operation.<br><br> 
                     " + Session["Company_Name_GL"].ToString() + @"<br>
                     PURCHASING DEPARTMENT
                                 
                     <br><br>
                    ";


        strSubject = "Purchase Order No. " + OrderCode + " from " + Session["Company_Name_GL"] + "  for M.V.:" + dsEmailInfo.Tables[0].DefaultView[0]["Vessel_Name"].ToString() + " ,  Date :" + DateTime.Now.ToString();
        sEmailAddress = dsEmailInfo.Tables[0].DefaultView[0]["SuppEmailIDs"].ToString();
      

    }

    protected void GeneratePOAsPDF(DataTable dt, string strPath, string FileName)
    {

        string repFilePath = Server.MapPath("POReport.rpt");
        //using (ReportDocument repDoc = new ReportDocument())
        //{

        using (ReportDocument repDoc = new ReportDocument())
        {
            repDoc.Load(repFilePath);

            repDoc.SetDataSource(dt);

           
                decimal Total_Price = 0;
                decimal Exchane_rate = 1;
                decimal Vat = 0;
                decimal sercharge = 0;
                decimal discount = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    Exchane_rate = Convert.ToDecimal(dr["EXCHANGE_RATE"].ToString());
                    discount = Convert.ToDecimal(dr["DISCOUNT"].ToString());
                    Vat = Convert.ToDecimal(dr["VAT"].ToString());
                    sercharge = Convert.ToDecimal(dr["SURCHARGES"].ToString());
                    Total_Price = Total_Price + (Convert.ToDecimal(dr["REQUESTED_QTY"].ToString()) * Convert.ToDecimal(dr["QUOTED_RATE"].ToString()) - (Convert.ToDecimal(dr["REQUESTED_QTY"].ToString()) * Convert.ToDecimal(dr["QUOTED_RATE"].ToString()) * Convert.ToDecimal(dr["QUOTED_DISCOUNT"].ToString()) / 100));
                }
                repDoc.DataDefinition.FormulaFields[1].Text = (Total_Price / Exchane_rate).ToString();
                repDoc.DataDefinition.FormulaFields[2].Text = (Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) * discount / 100).ToString();
                repDoc.DataDefinition.FormulaFields[3].Text = ((Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) - (Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) * discount / 100)) * sercharge / 100).ToString();
                repDoc.DataDefinition.FormulaFields[4].Text = ((Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) - Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[2].Text) + Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[3].Text)) * Vat / 100).ToString();
                repDoc.DataDefinition.FormulaFields[0].Text = ((Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[1].Text) - Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[2].Text) + Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[3].Text) + Convert.ToDecimal(repDoc.DataDefinition.FormulaFields[4].Text)).ToString());
                      
            ExportOptions exp = new ExportOptions();
            DiskFileDestinationOptions dk = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions pd = new PdfRtfWordFormatOptions();

            string sFile = strPath + FileName;
            dk.DiskFileName = strPath + FileName;

            exp.ExportDestinationType = ExportDestinationType.DiskFile;
            exp.ExportFormatType = ExportFormatType.PortableDocFormat;
            exp.DestinationOptions = dk;
            exp.FormatOptions = pd;
            repDoc.Export(exp);

            //for email attachment

            string destFile = Server.MapPath("../Uploads/Purchase") + "\\" + FileName; ;
            File.Copy(sFile, destFile, true);

            repDoc.Close();
            repDoc.Dispose();
        }
        //}

    }
    public string ReplaceSpecialCharacterinFileName(string strFileName)
    {
        return strFileName.Replace(" ", "_").Replace(".", "_").Replace("\\", "_").Replace("/", "_").Replace("?", "_").Replace("*", "_").Replace("<", "_").Replace(">", "_").Replace("|", "_").Replace(":", "_").Trim();
    }

    protected void rgdPOConfirm_SortCommand(object sender, GridSortCommandEventArgs e)
    {
        ViewState["SORTBYCOLOUMN"] = e.CommandArgument.ToString();
        switch (e.NewSortOrder)
        {
            case GridSortOrder.Ascending:
                ViewState["SORTDIRECTION"] = "ASC";
                break;
            case GridSortOrder.Descending:
                ViewState["SORTDIRECTION"] = "DESC";
                break;
        }
        switch (e.CommandArgument.ToString())
        {
            case "requestion_Date":
                ViewState["SORTBYCOLOUMN"] = null;
                break;
        }

        BindData();

    }
}
