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

public partial class Technical_INV_DeliveryStatusGrid : System.Web.UI.Page
{
    DataTable dtRequistion = new DataTable();
    public string sVesselCode = "";
    public string sCatalog = "0";
    string sRequiPendingType = "UPD";
    protected static bool IsMultipleUpdatePO = false;
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

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
    {UserAccessValidation();
        ViewState["NeedDataSource"] = "0";

        divReqStages.Visible = false;
        Session["PoolSelection"] = Request.QueryString["type"].ToString();



        Session["PURC_CurrentCity"] = "";
        Session["PURC_CurrentPort"] = "";
        Session["PURC_DeliveryPort"] = "";
        Session["PURC_CurrentStage"] = "";


        if (!IsPostBack)
        {
            BindMovementStatus();
            BindSupplier();

            BindRequisitionGrid();
           
            divOnHold.Visible = false;

            ucPurc_Rollback_Reqsn1.BindRequisitionStatus(sRequiPendingType);


        }
    }

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

    protected void BindMovementStatus()
    {

        DataTable dtMovementStatus =new DataTable();

        dtMovementStatus.Columns.Add("ID", typeof(string));
        dtMovementStatus.Columns.Add("Value", typeof(string));

        dtMovementStatus.Rows.Add("-", "Supplier");
        dtMovementStatus.Rows.Add("FORWARDER", "Forwarder");
        dtMovementStatus.Rows.Add("AGENT", "Agent");
        dtMovementStatus.Rows.Add("DELIVERED", "Delivered to vessel");
        dtMovementStatus.Rows.Add("VESSELACKNOWLEDGED", "Vessel Acknowledged");


        ddlCurrentStatus.DataSource = dtMovementStatus;
        ddlCurrentStatus.DataTextField = "Value";
        ddlCurrentStatus.DataValueField = "ID";
        ddlCurrentStatus.DataBind();

        Session["PURC_CurrentStage"] = ddlCurrentStatus.SelectedValues;
    
    }

    protected void BindSupplier()
    { 
    
        DataTable dtSupplier =new DataTable();
  
        dtSupplier =BLL_PURC_Common.Get_SupplierList(null,"");

        ddlSupplier.DataSource = dtSupplier;
        ddlSupplier.DataTextField = "fullname";
        ddlSupplier.DataValueField = "SUPPLIER";
        ddlSupplier.DataBind();

        Session["PURC_Supplier_Name"] = ddlSupplier.SelectedValues;
     
    }





    protected void BindRequisitionGrid()
    {
        try
        {

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {

                switch (Request.QueryString["Type"])
                {
                    case "DVS":

                        int Fetch_Count = ucCustomPagerDeliveryStatus.isCountRecord;
                        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
                        string sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = (ViewState["SORTDIRECTION"].ToString());


                        Session["PURC_CurrentCity"] = "";
                        Session["PURC_CurrentPort"] = "";
                        Session["PURC_DeliveryPort"] = ddlAgentPortDeliveryOnbaord.SelectedValue;
                        Session["PURC_CurrentStage"] = ddlCurrentStatus.SelectedValues;

                        Session["PURC_PO_Date"] = txtFromPODate.Text;
                        Session["PURC_TO_PO_Date"] = txtToPODate.Text;

                        Session["PURC_Supplier_Name"] = ddlSupplier.SelectedValues;

                        //dtRequistion = objTechService.SelectRequisitionDeliveryStatus(null,
                        //                                                             null,
                        //                                                             UDFLib.ConvertIntegerToNull(ddlAgentPortDeliveryOnbaord.SelectedValue),
                        //                                                             (DataTable)Session["PURC_CurrentStage"],
                        //                                                             (DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"],
                        //                                                             (DataTable)Session["sDeptCode"],
                        //                                                             UDFLib.ConvertStringToNull(Session["sDeptType"]),
                        //                                                             UDFLib.ConvertStringToNull(Session["REQNUM"]),
                        //                                                             UDFLib.ConvertIntegerToNull(Session["ReqsnType"]),
                        //                                                             (DataTable)Session["PURC_Supplier_Name"],
                        //                                                             UDFLib.ConvertDateToNull(txtFromPODate.Text),
                        //                                                             UDFLib.ConvertDateToNull(txtToPODate.Text),
                        //                                                            ucCustomPagerDeliveryStatus.CurrentPageIndex,
                        //                                                            ucCustomPagerDeliveryStatus.PageSize,
                        //                                                            ref Fetch_Count,sortbycoloumn,sortdirection);


                        dtRequistion = objTechService.SelectRequisitionDeliveryStatus_New(UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                , UDFLib.ConvertStringToNull(Session["sDeptType"])
                                , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                , UDFLib.ConvertStringToNull(Session["REQNUM"])
                                , UDFLib.ConvertStringToNull(Session["sPOType"])
                                , UDFLib.ConvertStringToNull(Session["sAccType"])
                                , UDFLib.ConvertStringToNull(Session["ReqsnType"])
                                , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                , UDFLib.ConvertStringToNull(Session["sFrom"])
                                , UDFLib.ConvertStringToNull(Session["sTO"])
                                , UDFLib.ConvertStringToNull(Session["sAccClass"])
                                , UDFLib.ConvertStringToNull(Session["dturgrcy"])
                                , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                                , UDFLib.ConvertStringToNull((Session["DeliveryPort"]))
                                , UDFLib.ConvertStringToNull((Session["Supplier"]))
                                , ucCustomPagerDeliveryStatus.CurrentPageIndex, ucCustomPagerDeliveryStatus.PageSize, ref  Fetch_Count, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));

                        if (ucCustomPagerDeliveryStatus.isCountRecord == 1)
                        {
                            ucCustomPagerDeliveryStatus.CountTotalRec = Fetch_Count.ToString();
                            ucCustomPagerDeliveryStatus.BuildPager();
                        }


                        break;
                }


                rgdDeliveryStatus.DataSource = dtRequistion;
                
                if (ViewState["NeedDataSource"].ToString() == "0")
                    rgdDeliveryStatus.DataBind();

                string script = " var height = document.body.scrollHeight;parent.ResizeFromChild(height,'1');";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "resize" + DateTime.Now.Millisecond.ToString(), script, true);
            }



        }
        catch //(Exception ex)
        {
        }
        finally
        {
        }

    }

    protected void SaveReqDelStatus(object sender, CommandEventArgs e)
    {
        string strCommandArgument = Convert.ToString(e.CommandArgument);
        string[] strArr = strCommandArgument.Split(',');

        ResponseHelper.Redirect("UpdateDeliveryStatus.aspx?sOrderCode=" + strArr[4] + "&SupplierCode=" + strArr[5] + "&VesselCode=" + strArr[5], "blank", "");


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

    protected void rgdDeliveryStatus_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            ViewState["NeedDataSource"] = "1";

            BindRequisitionGrid();

        }
    }

    protected void btnUpdatedelivery_Click(object sender, EventArgs e)
    {

        string strorderCode = "";

        foreach (GridDataItem dr in rgdDeliveryStatus.Items)
        {
            if (((CheckBox)dr.FindControl("chkPO")).Checked == true)
            {
                strorderCode += dr["ORDER_CODE"].Text + ",";
            }
        }

        ResponseHelper.Redirect("UpdateDeliveryStatus.aspx?sOrderCode=" + strorderCode, "blank", "");
    }

    protected void imgViewLog_Click(object sender, EventArgs e)
    {

        string strCommandArgument = ((LinkButton)sender).CommandArgument;
        string[] strArr = strCommandArgument.Split(',');
        ResponseHelper.Redirect("TrackDeliveryStatus.aspx?sOrderCode=" + strArr[4], "blank", "");

    }

    protected void onAddNewItem(object sender, CommandEventArgs e)
    {
        ResponseHelper.Redirect("AddNotListItems.aspx?ReqCode=" + e.CommandArgument.ToString(), "Blank", "");

    }

    protected void rgdDeliveryStatus_ItemDataBound(object sender, GridItemEventArgs e)
    {

        if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
        {
            GridDataItem dataItem = e.Item as GridDataItem;

            GridDataItem item = (GridDataItem)e.Item;
            ImageButton ImgPriority = (ImageButton)(item.FindControl("ImgPriority") as ImageButton);
            if (!string.IsNullOrWhiteSpace(item["URGENCY_CODE"].Text))
            {
                if (item["URGENCY_CODE"].Text == "Urgent")
                {
                    ImgPriority.ImageUrl = "~/Images/exclamation.png";
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

            ImageButton ImgAttach = (ImageButton)(dataItem.FindControl("ImgAttachment") as ImageButton);
            string strAttFlag = "0";
            strAttFlag = dataItem["Attach_Status"].Text.ToString();
            if (strAttFlag == "0")
            {
                ImgAttach.ImageUrl = "~/images/Attach.png";
                ImgAttach.ToolTip = "Add Attachments";
                ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('Add Attachments' ,'rgdDeliveryStatus');");
            }
            else
            {
                ImgAttach.ImageUrl = "~/images/attachment32.png";
                ImgAttach.ToolTip = "View Attachments";
                ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('View Attachments' ,'rgdDeliveryStatus');");
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

    protected void btnSearch_Click(object s, EventArgs e)
    {
        ViewState["NeedDataSource"] = "0";
        ucCustomPagerDeliveryStatus.isCountRecord = 1;

        Session["sCurrentStatus"] = ddlCurrentStatus.SelectedValues;
        Session["sSuppliers"] = ddlSupplier.SelectedValues;

        BindRequisitionGrid();
    }


    protected void ddlCurrentStatus_SelectedIndexChanged()
    {
        Session["sCurrentStatus"] = ddlCurrentStatus.SelectedValues;
    }

    protected void ddlSupplier_SelectedIndexChanged()
    {
        Session["sSuppliers"] = ddlSupplier.SelectedValues;
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


    protected void btndivCancel_Click(object sender, EventArgs e)
    {
        divOnHold.Visible = false;
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
    protected void onUpdateDelivery(object source, CommandEventArgs e)
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
    protected void rgdDeliveryStatus_SortCommand(object sender, GridSortCommandEventArgs e)
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

        BindRequisitionGrid();

    }

}
