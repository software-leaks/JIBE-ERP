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
using ClsBLLTechnical;
using System.Text;

public partial class Technical_INV_PendingEvalGrid : System.Web.UI.Page
{

    public string sRequiPendingType = "QEV";
    public string PageKeyValue = "QUOTEVAL";
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
        divReqStages.Visible=false;
        ViewState["NeedDataSource"] = "0";
        dvCancelReq.Visible = false;
       
        if (!IsPostBack)
        {
            if (Request.QueryString["POLOG"] != null)
                chkAllApprovals.Checked = false;
            else
                chkAllApprovals.Checked = true;


            Session["PoolSelection"] = Request.QueryString["type"].ToString();
            divOnHold.Visible = false;
     

            BindData();
            BindApprovers();
            BindSupplier();
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
        int? LogedIn_User = null;
        Session["SHOWALL_PENDING_APPROVAL"] = 0;
        if (chkAllApprovals.Checked == false)
        {
            LogedIn_User = Convert.ToInt32(Session["USERID"].ToString());
            Session["SHOWALL_PENDING_APPROVAL"] = 1;
        }

        int rowcount = ucCustomPagerItems.isCountRecord;
        BLL_PURC_Purchase objPurcBLL = new BLL_PURC_Purchase();

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = (ViewState["SORTDIRECTION"].ToString());

        //DataTable dt = objPurcBLL.SelectPendingQuatationEvalution((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
        //                        , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
        //                        , UDFLib.ConvertIntegerToNull(Session["ReqsnType"].ToString()), UDFLib.ConvertStringToNull(Session["REQNUM"].ToString()),LogedIn_User
        //                        , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, sortbycoloumn, sortdirection);

        DataTable dt = objPurcBLL.SelectPendingQuatationEvalution_New
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
                                , UDFLib.ConvertStringToNull(Session["accclass"])
                                , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                , UDFLib.ConvertStringToNull((Session["reqsnstatus"]))
                                , UDFLib.ConvertStringToNull(Session["sQuotRec"])
                                , UDFLib.ConvertStringToNull(Session["sPendingApprover"])
                                , UDFLib.ConvertStringToNull(Session["USERID"])
                                ,ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));
        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

       
      
        rgdEval.DataSource = dt;
     

        if (ViewState["NeedDataSource"] == "0")
            rgdEval.DataBind();


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

    private void ColourForReSendforQtn()
    {
        DataTable dtRFQReQtn = new DataTable();
        foreach (GridDataItem dataItem in rgdEval.MasterTableView.Items)
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dtRFQReQtn = objTechService.CountRFQSendForReQuotation(dataItem["REQUISITION_CODE"].Text.ToString(), dataItem["Vessel_Code"].Text.ToString(), dataItem["document_code"].Text.ToString());
                int ReQtnCount = Convert.ToInt32(dtRFQReQtn.Rows[0]["ReSendForQtn"].ToString());
                if (ReQtnCount >= 1)
                {
                    dataItem.BackColor = System.Drawing.Color.LightGreen;
                }

            }
        }
    }

    protected void onSelectAttachment(object source, CommandEventArgs e)
    {
        String msg = "CheckFileAndOpen('" + ((ImageButton)source).ValidationGroup + "')";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString(), msg, true);
        if (((ImageButton)source).ValidationGroup == "")
        {
            ResponseHelper.Redirect("FileAttachmentInfo.aspx?Requisitioncode=" + e.CommandArgument.ToString(), "Blank", "");
        }
    }

    protected void onSelect(object source, CommandEventArgs e)
    {
        HiddenArgument.Value = e.CommandArgument.ToString();
        string[] strArgs = HiddenArgument.Value.Split('&');
        string sOnHold = strArgs[4];

        if (sOnHold.ToString() == "False")
        {
            BLL_PURC_Common objComm = new BLL_PURC_Common();
            string CompanyID = Session["USERCOMPANYID"].ToString();
            DataTable dt = objComm.Get_LIB_Page_Config(UDFLib.ConvertToInteger(CompanyID), PageKeyValue);

            if (dt.Rows.Count > 0 )
            {
                ResponseHelper.Redirect("QuatationEvalutionDetails.aspx?Requisitioncode=" + e.CommandArgument.ToString(), "blank", "");
            }
            else
            {
                ResponseHelper.Redirect("QuatationEvalution.aspx?Requisitioncode=" + e.CommandArgument.ToString(), "blank", "");
            }
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
                        String msg = String.Format("alert('Requisition QEV has been marked on " + sOnHoldName + "');");
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
            String msg = String.Format("alert('Remark is mandatory field.');");
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
            Session["ReqsnCancelLog"] = e.CommandArgument.ToString().Split(new char[] { ',' })[0];
            HiddenArgument.Value = e.CommandArgument.ToString();
            ucPurc_Rollback_Reqsn1.RequisitionCode = e.CommandArgument.ToString().Split(',')[0];
            ucPurc_Rollback_Reqsn1.BindGrid();
            ucPurc_Rollback_Reqsn1.Order_Code = "";
        }
        else
        {
            String msg = String.Format("alert('This requisition has been marked as OnHold.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }

    protected void btndivReqPrioCancel_Click(object sender, EventArgs e)
    {
       
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
            try
            {
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    int iReturn = objTechService.CancelRequisitionStages(Convert.ToInt32(sVessel_Code), sRequisitionCode, sDocumentCode, ucPurc_Rollback_Reqsn1.StageValue
                                , ucPurc_Rollback_Reqsn1.Reason, Convert.ToInt32(Session["userid"].ToString()), Request.QueryString["type"].ToString());
                    if (iReturn > 0)
                    {
                        BLL_PURC_Common.INS_Remarks(sDocumentCode, Convert.ToInt32(Session["userid"].ToString()), ucPurc_Rollback_Reqsn1.Reason, 305);
                        divOnHold.Visible = false;
                        String msg = String.Format("alert('Requisition has been shifted on " + ucPurc_Rollback_Reqsn1.StageText + "'); ");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                        ucCustomPagerItems.isCountRecord = 1;
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
            String msg = String.Format("alert('Reason is mandatory field.'); ");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }

    protected void rgdEval_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            item["URGENCY_CODE"].ForeColor = System.Drawing.Color.Red;
            item["URGENCY_CODE"].Font.Bold = true;

            item["RFQSend"].Attributes.Add("onclick", "Get_ToopTipsForQtnSent('" + item["document_code"].Text.ToString() + "',event,this)");
            item["QuotReceived"].Attributes.Add("onclick", "Get_ToopTipsForQtnRecve('" + item["document_code"].Text.ToString() + "',event,this)");

            item["RFQSend"].ToolTip = "Click to view detail";
            item["QuotReceived"].ToolTip = "Click to view detail";

            ImageButton btnOnHold = (ImageButton)(item.FindControl("btnOnHold") as ImageButton);
            ImageButton ImgAttach = (ImageButton)(item.FindControl("ImgAttachment") as ImageButton);
            string strAttFlag = item["Attach_Status"].Text.ToString();

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

            if (strAttFlag == "0")
            {
                ImgAttach.ImageUrl = "~/images/Attach.png";
                //ImgAttach.Enabled = false;
                ImgAttach.ToolTip = "Add Attachments";
                ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('Add Attachments' ,'rgdEval');");
            }
            else
            {
                ImgAttach.ImageUrl = "~/images/attachment32.png";
                ImgAttach.ToolTip = "View Attachments";
                ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('View Attachments' ,'rgdEval');");
            }
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
            #region Minimum Quot Received 
            if (item["is_minquot_received"].Text == "0")
            {
                Button ImgSelect = (item.FindControl("ImgSelect") as Button);
                ImgSelect.Attributes.Add("onclick", "alert('The minimum number of quotations was not received.')");
            }
            #endregion

        }
    }

   
    protected void onAddNewItem(object sender, CommandEventArgs e)
    {
        ResponseHelper.Redirect("AddNotListItems.aspx?ReqCode=" + e.CommandArgument.ToString(), "Blank", "");
    }

    protected void imgbtnCancel_Click(object s, EventArgs e)
    {
        string[] CommParam = ((ImageButton)s).CommandArgument.Split(new char[] { ',' });
        dvCancelReq.Visible = true;
        ucPurcCancelReqsnNew.ReqsnNumber = CommParam[0];
        ucPurcCancelReqsnNew.DocCode = CommParam[1];
    }

    protected void chkAllApprovals_CheckedChanged(object s, EventArgs e)
    {
        ucCustomPagerItems.isCountRecord = 1;
        BindData();
    }
    protected void rgdEval_SortCommand(object sender, GridSortCommandEventArgs e)
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
    private void BindApprovers()
    {
        BLL_PURC_Purchase objPurcBLL = new BLL_PURC_Purchase();
        try
        {
                DataTable dt =  objPurcBLL.GetApproverList("");
                cmdApprover.DataSource = dt;
                cmdApprover.DataTextField = "UserName";
                cmdApprover.DataValueField = "UserID";
                cmdApprover.DataBind();
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #region
    protected void BindSupplier()
    {

        DataTable dtSupplier = new DataTable();

        dtSupplier = BLL_PURC_Common.Get_SupplierList(null, "");

        ddlSupplier.DataSource = dtSupplier;
        ddlSupplier.DataTextField = "fullname";
        ddlSupplier.DataValueField = "SUPPLIER";
        ddlSupplier.DataBind();

    }
    protected void ddlSupplier_SelectedIndexChanged()
    {
        StringBuilder sbFilterFlt = new StringBuilder();
        foreach (DataRow dr in ddlSupplier.SelectedValues.Rows)
        {
            sbFilterFlt.Append(dr[0]);
            sbFilterFlt.Append(",");
        }
        Session["sQuotRec"] = ddlSupplier.SelectedValues;
        BindData();
    }
    protected void cmdApprover_SelectedIndexChanged()
    {
     StringBuilder sbFilterFlt = new StringBuilder();
     foreach (DataRow dr in cmdApprover.SelectedValues.Rows)
        {
            sbFilterFlt.Append(dr[0]);
            sbFilterFlt.Append(",");
        }
        Session["sPendingApprover"] = cmdApprover.SelectedValues;
        BindData();
    }
    #endregion

}
