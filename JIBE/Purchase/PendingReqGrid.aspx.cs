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
using System.Text;
using ClsBLLTechnical;
using SMS.Business.Infrastructure;
using SMS.Data.PURC;
public partial class Technical_INV_PendingReqGrid : System.Web.UI.Page
{



    public string sRequiPendingType = "";
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
        ViewState["NeedDataSource"] = "0";

        dvSendForApproval.Visible = false;
        sRequiPendingType = Request.QueryString["type"].ToString();
        dvCancelReq.Visible = false;

        divOnHold.Visible = false;
        divReqStages.Visible = false;



        if (!IsPostBack)
        {
            BindData();
            BindRequisitionStatus(sRequiPendingType);
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
    protected void BindData()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
        BLL_PURC_Purchase objPurcBLL = new BLL_PURC_Purchase();
        
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = (ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objPurcBLL.SelectPendingRequistion_New(
                                UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
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
                                , UDFLib.ConvertIntegerToNull(ddlMinQuot.SelectedValue == "--All--" ? null : ddlMinQuot.SelectedValue)
                                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, sortbycoloumn, sortdirection);

        //DataTable dt = objPurcBLL.SelectPendingRequistion((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
        // , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
        // , UDFLib.ConvertIntegerToNull(Session["ReqsnType"].ToString()), UDFLib.ConvertStringToNull(Session["REQNUM"].ToString())
        // , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, sortbycoloumn, sortdirection);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        rgdPending.DataSource = dt;
        if (ViewState["NeedDataSource"] == "0")
            rgdPending.DataBind();

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


    private void BindRequisitionStatus(string sRequiPendingType)
    {

        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable dtREQStatus = objTechService.GetREQStatus();
            DataRow[] filterRows = dtREQStatus.Select("short_code ='" + sRequiPendingType + "'");
            dtREQStatus.DefaultView.RowFilter = "code < '" + filterRows[0][0].ToString() + "'";
            DDLReqStages.DataSource = dtREQStatus.DefaultView;
            DDLReqStages.DataTextField = "Description";
            DDLReqStages.DataValueField = "short_code";
            DDLReqStages.DataBind();

        }
    }

    protected void onSendRFQ(object source, CommandEventArgs e)
    {
        divOnHold.Visible = false;
        divReqStages.Visible = false;
        HiddenArgument.Value = e.CommandArgument.ToString();
        string[] strArgs = HiddenArgument.Value.Split('&');

        string sOnHold = strArgs[4];
        if (sOnHold.ToString() == "False")
        {

            ResponseHelper.Redirect("SelectSuppliers.aspx?Requisitioncode=" + e.CommandArgument.ToString(), "blank", "");
        }
        else
        {
            String msg = String.Format("alert('This requisition has been marked as OnHold.'); window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }

    protected void onSelect(object source, CommandEventArgs e)
    {
        divOnHold.Visible = false;
        divReqStages.Visible = false;

        HiddenArgument.Value = e.CommandArgument.ToString();
        string[] strArgs = HiddenArgument.Value.Split('&');

        string sOnHold = strArgs[4];
        if (sOnHold.ToString() == "False")
        {
            ResponseHelper.Redirect("QuatationEntry.aspx?Requisitioncode=" + e.CommandArgument.ToString(), "blank", "");
        }
        else
        {
            String msg = String.Format("alert('This requisition has been marked as OnHold.'); window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
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
                //btnOnHold.Text = "Hold";
                btnOnHold.ImageUrl = "~/purchase/Image/OnHold.png";
                //lblUrgencyTitle.Text = "Hold the Requisition";
                HoldUnHold.lblHeader = "Hold the Requisition";
            }
            else
            {
                // btnOnHold.Text = "Un Hold";
                btnOnHold.ImageUrl = "~/purchase/Image/release.png";
                //lblUrgencyTitle.Text = "Un Hold the Requisition";
                HoldUnHold.lblHeader = "Un Hold the Requisition";
            }

            BindData();

        }
        catch (Exception ex)
        {

        }

    }
    protected void OnCancelReq(object sender, CommandEventArgs e)
    {
        divReqStages.Visible = true;
        divOnHold.Visible = false;
        DDLReqStages.SelectedIndex = 0;
        HiddenArgument.Value = e.CommandArgument.ToString();
        lblReqsnNoRollBack.Text = e.CommandArgument.ToString().Split(',')[0];
        Session["ReqsnCancelLog"] = e.CommandArgument.ToString().Split(new char[] { ',' })[0];
        ucReqsncancelLog1.BindGrid();
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
    protected void btndivReqPrioCancel_Click(object sender, EventArgs e)
    {
        divReqStages.Visible = false;
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
                    int iReturn = objTechService.InsRequisitionOnHoldLogHistory(sRequisitionCode, sVessel_Code, sDocumentCode, sLinkType, sOnHold, sRemarks, sSupplierID, Convert.ToInt32(Session["userid"].ToString()));
                    if (iReturn == 1)
                    {
                        DataTable dtQuotationList = new DataTable();
                        dtQuotationList.Columns.Add("Qtncode");
                        dtQuotationList.Columns.Add("amount");

                        BLL_PURC_Common.INS_Remarks(sDocumentCode, Convert.ToInt32(Session["userid"].ToString()), sRemarks, 306);
                        objTechService.InsertRequisitionStageStatus(sRequisitionCode, sVessel_Code, sDocumentCode, sOnHold, sRemarks, Convert.ToInt32(Session["USERID"]), dtQuotationList);
                        divOnHold.Visible = false;
                        String msg = String.Format("alert('Requisition UPQ has been marked on " + sOnHoldName + "'); ");
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

    protected void btndivReqprioOK_Click(object sender, EventArgs e)
    {


        if (txtReason.Text != "")
        {
            string[] strArgs = HiddenArgument.Value.Split(',');
            string sRequisitionCode = strArgs[0];
            string sDocumentCode = strArgs[1];
            string sVessel_Code = strArgs[2];
            string sLinkType = sRequiPendingType;
            string sOnHold = strArgs[4];
            string sReason = txtReason.Text;
            string sCanceledOnStage = "";
            string sRemarks = HoldUnHold.Remarks;
            sCanceledOnStage = DDLReqStages.SelectedValue.ToString();

            try
            {
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    int iReturn = objTechService.CancelRequisitionStages(Convert.ToInt32(sVessel_Code), sRequisitionCode, sDocumentCode, sCanceledOnStage, sReason, Convert.ToInt32(Session["userid"].ToString()), Request.QueryString["type"].ToString());
                    if (iReturn > 0)
                    {
                        BLL_PURC_Common.INS_Remarks(sDocumentCode, Convert.ToInt32(Session["userid"].ToString()), sReason, 305);
                        String msg = String.Format("alert('Requisition has Canceled Stage on " + DDLReqStages.SelectedItem.Text.ToString() + "'); ");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                        ucCustomPagerItems.isCountRecord = 1;
                        BindData();
                    }

                }
                divReqStages.Visible = false;
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

    protected void rgdPending_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;

                item["RFQSend"].Attributes.Add("onclick", "Get_ToopTipsForQtnSent('" + item["document_code"].Text.ToString() + "',event,this)");
                item["QuotReceived"].Attributes.Add("onclick", "Get_ToopTipsForQtnRecve('" + item["document_code"].Text.ToString() + "',event,this)");
                item["InProcess"].Attributes.Add("onclick", "Get_ToopTipsForQtnINProgress('" + item["document_code"].Text.ToString() + "',event,this)");
                item["QuotDeclined"].Attributes.Add("onclick", "Get_ToopTipsForQtnDeclined('" + item["document_code"].Text.ToString() + "',event,this)");

                item["RFQSend"].ToolTip = "Click to view detail";
                item["QuotReceived"].ToolTip = "Click to view detail";
                item["InProcess"].ToolTip = "Click to view detail";


                ImageButton ImgAttach = (item.FindControl("ImgAttachment") as ImageButton);
                ImageButton btnOnHold = (item.FindControl("btnOnHold") as ImageButton);
                ImageButton btnOnRelease = (item.FindControl("btnOnRelease") as ImageButton);
                Label lblrework = (item.FindControl("lblrework") as Label);

                Panel pnlSenttosupdt = (item.FindControl("pnlSenttosupdt") as Panel);
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

                string strAttFlag = item["Attach_Status"].Text.ToString();
                if (strAttFlag == "0")
                {
                    ImgAttach.ImageUrl = "~/images/Attach.png";
                    //ImgAttach.Enabled = false;
                    //ImgAttach.ToolTip = "No Attachment(s)";
                    ImgAttach.ToolTip = "Add Attachments";
                    ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('Add Attachments' ,'rgdPending');");
                }
                else
                {
                    ImgAttach.ImageUrl = "~/images/attachment32.png";
                    ImgAttach.ToolTip = "View Attachments";
                    ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('View Attachments' ,'rgdPending');");
                }

                string sOnHoldFlag = item["OnHold"].Text.ToString();
                if (sOnHoldFlag.ToString() == "False")
                {
                    btnOnRelease.Visible = false;
                }
                else
                {
                    btnOnHold.Visible = false;
                }

                if (lblrework.Text == "1")
                {
                    LinkButton lbtnsentTosuppdt = ((LinkButton)item.FindControl("btnSenttosupdt"));

                    lbtnsentTosuppdt.ForeColor = System.Drawing.Color.Red;
                    lbtnsentTosuppdt.Font.Bold = true;


                    lbtnsentTosuppdt.Attributes.Add("onmouseover", "javascript:GetRemark('" + item["document_code"].Text + "')");
                    lbtnsentTosuppdt.Attributes.Add("onmouseout", "javascript:CloseRemark()");
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
                #region Quote_status_colour
                
                DataTable dtPending =  DAL_PURC_Report.GetToopTipsForQtnINProgress(item["document_code"].Text);
                var v_pending = from pending in dtPending.AsEnumerable()
                        where pending.Field<Int32>("IS_SUPPLIER_EXPIRED") == 0
                        select pending;

                DataTable dtReceived =  DAL_PURC_Report.GetToopTipsForQtnRecve(item["document_code"].Text);
                var v_received = from received in dtReceived.AsEnumerable()
                                 where received.Field<Int32>("IS_SUPPLIER_EXPIRED") == 0
                        select received;

                if (v_pending.Count()>0)
                {
                    item["InProcess"].ForeColor = System.Drawing.Color.Red; 
                }
                if (v_received.Count() > 0)
                {
                    item["QuotReceived"].ForeColor = System.Drawing.Color.Red; 
                }

                #endregion

            }
            catch (Exception ex)
            {
            }

        }

    }



    protected void btnSenttosupdt_Click(object s, EventArgs e)
    {


        LinkButton lbtn = (LinkButton)s;
        int RcvdQTN = Convert.ToInt32(((LinkButton)((GridDataItem)((LinkButton)s).Parent.Parent).FindControl("lbtnQtnRcvd")).Text.Trim());
        if (RcvdQTN > 0)
        {
            HiddenFieldSuppdtRemark.Value = lbtn.CommandArgument;
            string[] prm = HiddenFieldSuppdtRemark.Value.Split(new char[] { ',' });

            ucApprovalUser1.ReqsnCode = prm[2].ToString();

            ucApprovalUser1.FillUser();
            DataSet ds  = new DataSet();
            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();

            ds = objTechService.SelectBudgetCode(prm[2].ToString(), prm[4].ToString(),0);
            if(ds.Tables[0].Rows.Count > 0)
            {
                ddlBudgetCode.DataSource = ds.Tables[0];
                ddlBudgetCode.DataTextField = "Budget_Name";
                ddlBudgetCode.DataValueField = "Linked_Budget_Code";
                ddlBudgetCode.DataBind();

                ListItem li = new ListItem("---Select---", "0");
                ddlBudgetCode.Items.Insert(0, li);

                Session["BudgetDetails"] = ds.Tables[0];
                lblBudgetMsg.Text = "";
            }
            else
            {
                 //ds = objTechService.SelectBudgetCode();

                ddlBudgetCode.DataSource = ds.Tables[1];
                ddlBudgetCode.DataTextField = "Budget_Name";
                ddlBudgetCode.DataValueField = "Budget_Code";
                ddlBudgetCode.DataBind();

                ListItem li = new ListItem("---Select---", "0");
                ddlBudgetCode.Items.Insert(0, li);
                lblBudgetMsg.Text = "Budget limit not defined for this vessel.";
            }
            dvSendForApproval.Visible = true;
            gvQuotationList.DataSource = BLL_PURC_Common.PURC_GET_Quotation_ByReqsnCode(prm[2].ToString());
            gvQuotationList.DataBind();

        }
        else
        {
            String msg = String.Format("alert('You have not received any quotation for this requisition !');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }


    }

    protected void OnStsSaved(object s, EventArgs e)
    {
        try
        {
            TechnicalBAL objTechBAL = new TechnicalBAL();
            TechnicalBAL objpurc = new TechnicalBAL();

            // reqsncode will be set to "" in uc after update
            string[] prm = HiddenFieldSuppdtRemark.Value.Split(new char[] { ',' });

            DataTable dtQuotations = BLL_PURC_Common.PURC_GET_Quotation_ByReqsnCode(prm[2].ToString());

            DataTable dtQuotationList = new DataTable();
            dtQuotationList.Columns.Add("Qtncode");
            dtQuotationList.Columns.Add("amount");

            foreach (DataRow dr in dtQuotations.Rows)
            {
                if (dr["active_PO"].ToString() == "0") // for those POs have been cancelled(means active_PO is zero)
                {
                    DataRow dtrow = dtQuotationList.NewRow();
                    dtrow[0] = dr["QUOTATION_CODE"].ToString();
                    dtrow[1] = "0";
                    dtQuotationList.Rows.Add(dtrow);
                }
            }
            // save the requested qty into order qty and order qty column on grid will be binded to order qty (change so store the updated qty by supp at the time of eval.) { this functionality has been implemented at rfq send stage}
            // code to update the order qty have been commented ,this is used to save bgt code only.
            // 
            //BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
            //int retval = 0;
            //if (Session["BudgetDetails"] != null)
            //{
            //    retval = objTechService.Check_Update_BudgetCode(prm[2].ToString(), prm[3].ToString(), ddlBudgetCode.SelectedValue);
            //}
            //else
            //{
                BLL_PURC_Common.Update_OrderQty_From_ReqstQty(prm[2].ToString(), prm[3].ToString(), ddlBudgetCode.SelectedValue);
            //}

            //qtnbased
            int stsEntry = objTechBAL.InsertUserApprovalEntries(prm[2].ToString(), prm[3].ToString(), prm[4].ToString(), Session["userid"].ToString(), ucApprovalUser1.ApproverID, 0, 0, "", ucApprovalUser1.Remark, dtQuotationList);
            if (stsEntry > 0)
            {
                // dtQuotationList is passing but not using in sp ,all quotation will be set as senttosuppdt as true for this reqsn
                int res = objpurc.PURC_Update_SentToSupdt(int.Parse(prm[0]), int.Parse(prm[1]), prm[2].ToString(), int.Parse(Session["USERID"].ToString()), ucApprovalUser1.Remark, dtQuotationList);


                if (res > 0)
                {
                    BLL_PURC_Purchase objPurc = new BLL_PURC_Purchase();
                    //Requisition stage status update and remark
                    objPurc.InsertRequisitionStageStatus(prm[2].ToString(), prm[4].ToString(), prm[3].ToString(), "QEV", " ", Convert.ToInt32(Session["USERID"]), dtQuotationList);
                    BLL_PURC_Common.INS_Remarks(prm[3].ToString(), Convert.ToInt32(Session["userid"].ToString()), ucApprovalUser1.Remark, 302);

                    divOnHold.Visible = false;
                    divReqStages.Visible = false;

                    ucCustomPagerItems.isCountRecord = 1;
                    BindData();

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
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
        ucPurcCancelReqsnNew.VesselCode = CommParam[2];

    }

    protected void lbtnQtnRcvd_Click(object s, EventArgs e)
    {
        string[] strArgs = ((LinkButton)s).CommandArgument.Split('&');
        string sOnHold = strArgs[4];

        if (sOnHold.ToString() != "False")
        {
            String msg = String.Format("alert('This requisition has been marked as OnHold.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
        else if (sOnHold.ToString() == "False" && UDFLib.ConvertToInteger(((LinkButton)s).Text.Trim()) > 0)
        {
            BLL_PURC_Common objComm = new BLL_PURC_Common();
            string CompanyID = Session["USERCOMPANYID"].ToString();
            DataTable dt = objComm.Get_LIB_Page_Config(UDFLib.ConvertToInteger(CompanyID), PageKeyValue);

            if (dt.Rows.Count > 0)
            {
                ResponseHelper.Redirect("QuatationEvalutionDetails.aspx?Requisitioncode=" + ((LinkButton)s).CommandArgument, "blank", "");
            }
            else
            {
                ResponseHelper.Redirect("QuatationEvalution.aspx?Requisitioncode=" + ((LinkButton)s).CommandArgument, "blank", "");
           }            
        }
       
    }
    protected void btnRequestAmount_Click(object sender, EventArgs e)
    {
        string OCA_URL = null;
        if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["OCA_APP_URL"]))
        {
            OCA_URL = ConfigurationManager.AppSettings["OCA_APP_URL"];
        }
        //string PassString = txtPassString.Text.ToString().ToString();
       //string OCA_URL1 = OCA_URL + "/PO_LOG/Supplier_Online_Invoice_Status_V2.asp?P=" + PassString + "";
        string OCA_URL1 = string.Empty;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + OCA_URL1 + "');", true);
    }
    protected void rgdPending_SortCommand(object sender, GridSortCommandEventArgs e)
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
    protected void ddlMinQuot_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
