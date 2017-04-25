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
using SMS.Business.Infrastructure;

public partial class Technical_INV_PendingPOGrid : System.Web.UI.Page
{
    public string sRequiPendingType = "RPO";
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
        hdnHost.Value = Request.Url.AbsoluteUri.ToString().Substring(0, Request.Url.AbsoluteUri.ToString().ToLower().IndexOf("/purchase/pendingpogrid.aspx")) + "/";
        UserAccessValidation();
        ViewState["NeedDataSource"] = "0";

        divReqStages.Visible = false;

        if (!IsPostBack)
        {
            Session["PoolSelection"] = Request.QueryString["type"].ToString();
            divOnHold.Visible = false;
            BindPorts();
            BindData();
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

    public void BindData()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        BLL_PURC_Purchase objPurcBLL = new BLL_PURC_Purchase();
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = (ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objPurcBLL.SelectPendingPurchasedOrderRaise_New(UDFLib.ConvertStringToNull(Session["sFleet"])
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
                                , UDFLib.ConvertStringToNull(Session["accclass"])
                                , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                , UDFLib.ConvertStringToNull((Session["reqsnstatus"]))
                                , UDFLib.ConvertStringToNull((Session["sPort"]))
                                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, sortbycoloumn, sortdirection);


        //DataTable dt = objPurcBLL.SelectPendingPurchasedOrderRaise((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
        //                        , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
        //                        , UDFLib.ConvertIntegerToNull(Session["ReqsnType"].ToString()), UDFLib.ConvertStringToNull(Session["REQNUM"].ToString())
        //                        , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, sortbycoloumn, sortdirection);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        rgdPO.DataSource = dt;
        if (ViewState["NeedDataSource"] == "0")
            rgdPO.DataBind();


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

    protected void onSelect(object source, CommandEventArgs e)
    {

        divOnHold.Visible = false;
        divReqStages.Visible = false;
        string strOption = "RPO";

        HiddenArgument.Value = e.CommandArgument.ToString();
        string[] strArgs = HiddenArgument.Value.Split('&');

        string sOnHold = strArgs[3];

        switch (strOption)
        {
            case "RPO":
                if (sOnHold.ToString() == "False")
                {
                   

                    ResponseHelper.Redirect("ApprovedPurchaseOrder.aspx?Requisitioncode=" + e.CommandArgument.ToString() + "&Type=RPO", "Blank", "");
                }
                else
                {
                    String msg = String.Format("alert('This requisition has been marked as OnHold.'); window.close();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                }
                break;
        }

    }

    protected void btnOnHold_Click(object sender, EventArgs e)
    {
        try
        {
            HoldUnHold.Remarks = "";
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
                HoldUnHold.lblHeader = "Un Hold the Requisition";
            }
        }
        catch (Exception ex)
        {

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
                    int iReturn = objTechService.InsRequisitionOnHoldLogHistory(sRequisitionCode, sVessel_Code, sDocumentCode, sLinkType, sOnHold, sRemarks, sSupplierID, Convert.ToInt32(Session["USERID"].ToString()));
                    if (iReturn == 1)
                    {
                        DataTable dtQuotationList = new DataTable();
                        dtQuotationList.Columns.Add("Qtncode");
                        dtQuotationList.Columns.Add("amount");

                        BLL_PURC_Common.INS_Remarks(sDocumentCode, Convert.ToInt32(Session["userid"].ToString()), sRemarks, 306);
                        objTechService.InsertRequisitionStageStatus(sRequisitionCode, sVessel_Code, sDocumentCode, sOnHold, sRemarks, Convert.ToInt32(Session["USERID"]), dtQuotationList);
                        divOnHold.Visible = false;
                        String msg = String.Format("alert('Requisition RPO has been marked on " + sOnHoldName + "'); ");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                        BindData();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            String msg = String.Format("alert('Remark is mandatory field.'); window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }

    protected void btndivCancel_Click(object sender, EventArgs e)
    {
        divOnHold.Visible = false;
    }

    protected void OnCancelReq(object sender, CommandEventArgs e)
    {
        string[] strArgs = e.CommandArgument.ToString().Split(',');
        string sOnHold = strArgs[4];
        if (sOnHold.ToString() == "False")
        {
            divReqStages.Visible = true;
            //DDLReqStages.SelectedIndex = 0;
            HiddenArgument.Value = e.CommandArgument.ToString();
            Session["ReqsnCancelLog"] = e.CommandArgument.ToString().Split(new char[] { ',' })[0];
            ucPurc_Rollback_Reqsn1.RequisitionCode = e.CommandArgument.ToString().Split(new char[] { ',' })[0];
            ucPurc_Rollback_Reqsn1.BindGrid();
            ucPurc_Rollback_Reqsn1.Order_Code = "";
        }
        else
        {
            String msg = String.Format("alert('This requisition has been marked as OnHold.'); window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }

    protected void btndivReqPrioCancel_Click(object sender, EventArgs e)
    {
        divReqStages.Visible = false;
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
            string QuotationCode = strArgs[5];
            string sReason = ucPurc_Rollback_Reqsn1.Reason;
            string sCanceledOnStage = "";
            string sRemarks = ucPurc_Rollback_Reqsn1.Reason;

            sCanceledOnStage = ucPurc_Rollback_Reqsn1.StageValue;
            try
            {
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    int iReturn = objTechService.CancelRequisitionStages(Convert.ToInt32(sVessel_Code), sRequisitionCode, sDocumentCode, sCanceledOnStage, sReason, Convert.ToInt32(Session["userid"].ToString()), Request.QueryString["type"].ToString(), QuotationCode);
                    if (iReturn > 0)
                    {
                        BLL_PURC_Common.INS_Remarks(sDocumentCode, Convert.ToInt32(Session["userid"].ToString()), sReason, 305);
                        divOnHold.Visible = false;

                        String msg = String.Format("alert('Requisition has been shifted on " + ucPurc_Rollback_Reqsn1.StageText + "'); ");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                        ucCustomPagerItems.isCountRecord = 1;
                        BindData();
                        #region to Generate Cancel PO PDF
                        if (sCanceledOnStage == "QEV" || sCanceledOnStage == "RFQ")
                        {
                            String funname = "GeneratePDf();";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "funname", funname, true);
                        }
                        #endregion
                    }

                    divReqStages.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }
        else
        {
            String msg = String.Format("alert('Reason is mandatory field.'); ");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }

    }

    protected void rgdPO_ItemDataBound(object sender, GridItemEventArgs e)
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
                    ImgPriority.ToolTip = item["URGENCY_CODE"].Text;
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
                ImgAttach.ToolTip = "Add Attachments";
                ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('Add Attachments' ,'rgdPO');");

            }
            else
            {
                ImgAttach.ImageUrl = "~/images/attachment32.png";
                ImgAttach.ToolTip = "View Attachments";
                ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('View Attachments' ,'rgdPO');");
             
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

    protected void rgdPO_DataBound(object sender, EventArgs e)
    {
        RadGrid grid = sender as RadGrid;
        int gridItems = grid.MasterTableView.Items.Count;
        if (gridItems < 7)
        {
            grid.ClientSettings.Scrolling.ScrollHeight = Unit.Pixel(gridItems * 20);
        }
    }

    protected void onAddNewItem(object sender, CommandEventArgs e)
    {
        ResponseHelper.Redirect("AddNotListItems.aspx?ReqCode=" + e.CommandArgument.ToString(), "Blank", "");

    }
    protected void rgdPO_SortCommand(object sender, GridSortCommandEventArgs e)
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
    private void BindPorts()
    {
        try
        {

            BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
            DataTable dt = objBLLPort.Get_PortList_Mini();
            DDLPort.DataTextField = "Port_Name"; 
            DDLPort.DataValueField="Port_ID";
            DDLPort.DataSource = dt;
            DDLPort.DataBind();
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void DDLPort_SelectedIndexChanged()
    {
        try
        {
            StringBuilder sbFilterFlt = new StringBuilder();
            foreach (DataRow dr in DDLPort.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }
            Session["sPort"] = Convert.ToString(sbFilterFlt);
            BindData();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }       
    }
}
