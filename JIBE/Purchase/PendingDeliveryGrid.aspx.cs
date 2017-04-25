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
using SMS.Business.Crew;
using SMS.Business.Infrastructure;

public partial class Technical_INV_PendingDeliveryGrid : System.Web.UI.Page
{
    DataTable dtRequistion = new DataTable();
    public string sVesselCode = "0";
    public string sCatalog = "0";
    public string sRequiPendingType = "UPD";
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
        UserAccessValidation();
        ViewState["NeedDataSource"] = "0";
        divReqStages.Visible = false;
        if (!IsPostBack)
        {

            Session["PoolSelection"] = Request.QueryString["type"].ToString();

            BindRequisitionGrid();
            divOnHold.Visible = false;

            ucPurc_Rollback_Reqsn1.BindRequisitionStatus(sRequiPendingType);
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


    protected void btndivCancel_Click(object sender, EventArgs e)
    {
        divOnHold.Visible = false;
    }

    public void BindRequisitionGrid()
    {
        try
        {


            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                switch (sRequiPendingType)
                {
                    case "UPD":

                        int Fetch_Count = ucCustomPagerDeliveryStatus.isCountRecord;

                        dtRequistion = objTechService.SelectPendingDeliveryUpdate((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"],
                                                                                     (DataTable)Session["sDeptCode"],
                                                                                     UDFLib.ConvertStringToNull(Session["sDeptType"]),
                                                                                     UDFLib.ConvertStringToNull(Session["REQNUM"]),
                                                                                     UDFLib.ConvertIntegerToNull(Session["ReqsnType"]),

                                                                                    ucCustomPagerDeliveryStatus.CurrentPageIndex,
                                                                                    ucCustomPagerDeliveryStatus.PageSize,
                                                                                    ref Fetch_Count);

                        if (ucCustomPagerDeliveryStatus.isCountRecord == 1)
                        {
                            ucCustomPagerDeliveryStatus.CountTotalRec = Fetch_Count.ToString();
                            ucCustomPagerDeliveryStatus.BuildPager();
                        }

                        break;

                }


                rgdPending.DataSource = dtRequistion;


                if (ViewState["NeedDataSource"] == "0")
                    rgdPending.DataBind();

                string script = " var height = document.body.scrollHeight;parent.ResizeFromChild(height,'1');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "resize" + DateTime.Now.Millisecond.ToString(), script, true);
            }
        }
        catch
        {
        }
        finally
        {
        }

    }

    protected void onSelect(object source, CommandEventArgs e)
    {

        string strOption = "UPD";

        divOnHold.Visible = false;
        divReqStages.Visible = false;
        HiddenArgument.Value = e.CommandArgument.ToString();
        string[] strArgs = HiddenArgument.Value.Split('&');

        string sOnHold = strArgs[4];

        switch (strOption)
        {
            case "UPD":
                if (sOnHold.ToString() == "False")
                {
                    ResponseHelper.Redirect("DeliveredItems.aspx?Requisitioncode=" + e.CommandArgument.ToString(), "Blank", "");
                }
                else
                {
                    String msg = String.Format("alert('This requisition has been marked as OnHold.'); window.close();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                }
                break;
        }

    }

    private bool IsFileAttached(string ReqCode, string FileType)
    {
        try
        {
            DataTable dtProItemsCons = new DataTable();

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dtProItemsCons = objTechService.GetAttachedFileInfo((string)Session["sVesselCode"]);
                dtProItemsCons.DefaultView.RowFilter = "Requisition_Code='" + ReqCode + "'";

            }

            if (dtProItemsCons.DefaultView.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        catch //(Exception ex)
        {
            return false;
        }

    }

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

    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            ViewState["NeedDataSource"] = "1";

            BindRequisitionGrid();


        }
    }

    protected void RadGrid1_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        switch (e.DetailTableView.Name)
        {

            case "Items":
                {
                    string REQUISITION_CODE = dataItem.GetDataKeyValue("REQUISITION_CODE").ToString();
                    string DocumentCode = dataItem.GetDataKeyValue("document_code").ToString();
                    string Vessel_Code = dataItem.GetDataKeyValue("Vessel_Code").ToString();
                    Session["VSLCODE"] = dataItem.GetDataKeyValue("Vessel_Code").ToString();
                    e.DetailTableView.DataSource = BindItemsInHirarchy(REQUISITION_CODE, Vessel_Code, DocumentCode);
                    break;
                }
        }
    }

    private DataTable BindRequisitionInHirarchy()
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
                dtItems = objTechService.SelectItemsHierarchyForPenDeliveredReqsn(strRequistionCode, strVesselCode);




            }
            return dtItems;
        }
        catch (Exception ex)
        {

            return dtItems = null;
        }
        finally
        {

        }

    }

    protected void CancelPO(object sender, CommandEventArgs e)
    {
        string[] strIds = e.CommandArgument.ToString().Split(',');

        try
        {
            string QUTCODE = strIds[0];
            string ODRCODE = strIds[1];
            string SUPCODE = strIds[2];
            string DOCCODE = strIds[5];
            string REQCODE = strIds[4];
            string VSLCODE = strIds[3];
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {

                objTechService.CancelPO(QUTCODE, ODRCODE, SUPCODE, DOCCODE, REQCODE, VSLCODE);
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void BtnCancelPO_Click(object sender, EventArgs e)
    {

    }

    protected void btnOnHold_Click(object sender, EventArgs e)
    {
        try
        {
            HoldUnHold.Remarks = "";
            divOnHold.Visible = true;
            divReqStages.Visible = false;

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
                HoldUnHold.lblHeader = "Un Hold the Requisition";
            }
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }

    }

    protected void OnHold(object sender, CommandEventArgs e)
    {
        HiddenArgument.Value = e.CommandArgument.ToString();
        string[] strArgs = HiddenArgument.Value.Split(',');
        BLL_PURC_Purchase objhold = new BLL_PURC_Purchase();
        HoldUnHold.DTLog = objhold.GetRequisitionOnHoldLogHistory_ByReqsn(strArgs[0]);
        HoldUnHold.BindLog();
    }

    protected void btndivSave_Click(object sender, EventArgs e)
    {
        if (HoldUnHold.Remarks != "")
        {
            string[] strArgs = HiddenArgument.Value.Split(',');
            string sRequisitionCode = strArgs[0];
            string sDocumentCode = strArgs[1];
            string sVessel_Code = strArgs[2];
            string sLinkType = sRequiPendingType;
            string sOnHold = strArgs[4];
            string sOnHoldName = "";
            string sRemarks = HoldUnHold.Remarks;
            string sSupplierID = "";

            string sReqCode = (string)Session["REQNUM"];
            string sDeptType = (string)Session["sDeptType"];

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
                    int iReturn = objTechService.InsRequisitionOnHoldLogHistory(sRequisitionCode, sVessel_Code, sDocumentCode, sLinkType, sOnHold, sRemarks, sSupplierID, Convert.ToInt32(Session["userid"].ToString()));
                    if (iReturn == 1)
                    {
                        DataTable dtQuotationList = new DataTable();
                        dtQuotationList.Columns.Add("Qtncode");
                        dtQuotationList.Columns.Add("amount");

                        BLL_PURC_Common.INS_Remarks(sDocumentCode, Convert.ToInt32(Session["userid"].ToString()), sRemarks, 306);
                        objTechService.InsertRequisitionStageStatus(sRequisitionCode, sVesselCode, sDocumentCode, sOnHold, sRemarks, Convert.ToInt32(Session["USERID"]), dtQuotationList);
                        divOnHold.Visible = false;
                        String msg = String.Format("alert('Requisition UPD has been marked on " + sOnHoldName + "'); window.close();");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

                        BindRequisitionGrid();
                    }

                }

            }
            catch (Exception ex)
            {
                //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            }
        }
        else
        {

        }
    }

    protected void OnCancelReq(object sender, CommandEventArgs e)
    {
        divOnHold.Visible = false;
        string OrderSupp = ((ImageButton)sender).AlternateText;
        string[] strArgs = e.CommandArgument.ToString().Split(',');
        string sOnHold = strArgs[4];
        if (sOnHold.ToString() == "False")
        {
            divReqStages.Visible = true;
            string ReqsnCode = e.CommandArgument.ToString().Split(new char[] { ',' })[0];
            //DDLReqStages.SelectedIndex = 0;
            HiddenArgument.Value = e.CommandArgument.ToString();
            Session["ReqsnCancelLog"] = ReqsnCode;
            ucPurc_Rollback_Reqsn1.BindGrid();
            ucPurc_Rollback_Reqsn1.RequisitionCode = ReqsnCode;
            //dlistPONumber.DataSource = BLL_PURC_Common.Get_PONumbers(ReqsnCode);
            //dlistPONumber.DataBind();
            ucPurc_Rollback_Reqsn1.Order_Code = OrderSupp.Split('~')[0];
            ucPurc_Rollback_Reqsn1.HRef = "~/purchase/POPreview.aspx?RFQCODE=" + ReqsnCode + "&Vessel_Code=" + strArgs[2] + "&Order_Code=" + OrderSupp.Split('~')[0];
            ucPurc_Rollback_Reqsn1.SupplierName = OrderSupp.Split('~')[1];
        }
        else
        {
            String msg = String.Format("alert('This requisition has been marked as OnHold.'); window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }

    //protected void btndivReqPrioCancel_Click(object sender, EventArgs e)
    //{
    //    divReqStages.Visible = false;
    //    txtReason.Text = "";
    //}

    protected void btndivReqprioOK_Click(object sender, EventArgs e)
    {
        int iReturn = 0;
        if (ucPurc_Rollback_Reqsn1.Reason != "")
        {
            string[] strArgs = HiddenArgument.Value.Split(',');
            string sRequisitionCode = strArgs[0];
            string sDocumentCode = strArgs[1];
            string sVessel_Code = strArgs[2];
            string sLinkType = sRequiPendingType;
            string sOnHold = strArgs[4];
            string QuotationCode = strArgs[5];
            string sReason = ucPurc_Rollback_Reqsn1.Reason;
            string sCanceledOnStage = "";
            string sRemarks = ucPurc_Rollback_Reqsn1.Reason;

            sCanceledOnStage = ucPurc_Rollback_Reqsn1.StageValue;

            string sReqCode = (string)Session["REQNUM"];
            string sDeptType = (string)Session["sDeptType"];



            try
            {
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {

                    iReturn = objTechService.CancelRequisitionStages(Convert.ToInt32(sVessel_Code), sRequisitionCode, sDocumentCode, sCanceledOnStage, sReason, Convert.ToInt32(Session["userid"].ToString()), Request.QueryString["type"].ToString(), QuotationCode);
                    if (iReturn > 0)
                    {

                        BLL_PURC_Common.INS_Remarks(sDocumentCode, Convert.ToInt32(Session["userid"].ToString()), sReason, 305);
                        divOnHold.Visible = false;
                        String msg = String.Format("alert('Requisition has been shifted  on " + ucPurc_Rollback_Reqsn1.StageText + "');");

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                        BindRequisitionGrid();
                        #region To Generate Cancel PO Pdf
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


                    divReqStages.Visible = false;
                }

            }
            catch (Exception ex)
            {
                //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            }
        }
        else
        {
            String msg = String.Format("alert('Reason is mandatory field.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }



    }

    protected void rgdPending_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;


            ImageButton ImgPriority = (ImageButton)(item.FindControl("ImgPriority") as ImageButton);
            if (!string.IsNullOrWhiteSpace(item["URGENCY_CODE"].Text))
            {
                if (item["URGENCY_CODE"].Text == "Urgent")
                {
                    ImgPriority.ImageUrl = "~/Images/exclamation.png";
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
                //ImgAttach.ToolTip = "View Attachments";

            }
            else
            {
                ImgAttach.ImageUrl = "~/images/attachment32.png";
                //ImgAttach.ImageUrl = "~/purchase/Image/attach1.gif";
                ImgAttach.ToolTip = "View Attachments";
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
        }
    }

    protected void rgdPending_DataBound(object sender, EventArgs e)
    {
        RadGrid grid = sender as RadGrid;
        int gridItems = grid.MasterTableView.Items.Count;
        if (gridItems < 12)
        {
            grid.ClientSettings.Scrolling.ScrollHeight = Unit.Pixel(gridItems * 33);
        }
    }

    protected void onAddNewItem(object sender, CommandEventArgs e)
    {
        ResponseHelper.Redirect("AddNotListItems.aspx?ReqCode=" + e.CommandArgument.ToString(), "Blank", "");

    }

}
