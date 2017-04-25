//System libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

//Custom defined libraries
using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business;
using SMS.Business.TRAV;
using SMS.Business.Infrastructure;
using System.Configuration;

public partial class RequestList : System.Web.UI.Page
{
    protected int quoted;
    protected string currentstatus = "";

    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    int? Approver_ID = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        lblMessageOnSendApproval.Text = "";
        lblMessageOnSendApproval.Visible = false;
        //UserAccessValidation();
        try
        {
            hdf_UserID.Value = Session["userid"].ToString();
            if (!IsPostBack)
            {
                ListItem li = cmbFleet.Items.FindByValue(Convert.ToString(Session["USERFLEETID"]));
                if (li != null)
                    li.Selected = true;

                BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
                DataTable dtvsl = objBLLVessel.Get_VesselList(int.Parse(cmbFleet.SelectedValue), 0, UDFLib.ConvertToInteger(Session["USERCOMPANYID"]), "", UDFLib.ConvertToInteger(Session["USERCOMPANYID"]));
                DDlVessel_List.DataSource = dtvsl;
                DDlVessel_List.DataBind();
                DDlVessel_List.Items.Insert(0, new ListItem("--VESSELS--", "0"));

                ViewState["Status"] = "NEW";
                BindRequestList();

                txtSelMenu.Value = lnkMenu1.ClientID;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectNode", "selMe('" + txtSelMenu.Value + "');", true);

                BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
                chklistUser.DataSource = objUser.Get_UserList(1);
                chklistUser.DataTextField = "USERNAME";
                chklistUser.DataValueField = "USERID";
                chklistUser.DataBind();
            }
            chkShowAllPendingApproval.Visible = false;

            string js = "bindClientEvents();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bindEvents", js, true);

            string js1 = "bindPaxsName();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bindPaxsName", js1, true);


            string js2 = "bindRoutInfo();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bindRoutInfo", js2, true);

            string js3 = "bindVesselPortCall();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bindVesselPortCall", js3, true);

            string js4 = "bindselectors();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bindselectors", js4, true);
        }
        catch { throw; }
    }
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

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
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectNode", "selMe('" + txtSelMenu.Value + "');", true);
    }

    protected void imgMarkTraveled_click(object sender, CommandEventArgs e)
    {

        BLL_TRV_TravelRequest treq = new BLL_TRV_TravelRequest();
        int retval = treq.Update_Travel_Flag(0, Convert.ToInt32(Session["USERID"].ToString()), Convert.ToInt32(e.CommandArgument.ToString()));

        BindRequestList();

    }

    protected void BindRequestList()
    {
        BLL_TRV_TravelRequest treq = new BLL_TRV_TravelRequest();
        int rowcount = ucCustomPagerItems.isCountRecord;

        string status = ViewState["Status"].ToString();


     
        try
        {
            if ((status == "RFQ SENT" || status == "QUOTE RECEIVED") && !chkShowAllPendingApproval.Checked)
                Approver_ID = UDFLib.ConvertToInteger(Session["userid"]);

            DataSet ds = new DataSet();
            ds = treq.GetRequestList(UDFLib.ConvertIntegerToNull(cmbFleet.SelectedValue), UDFLib.ConvertIntegerToNull(cmbVessel.SelectedValue),UDFLib.ConvertIntegerToNull(cmbSupplier.SelectedValue),
               UDFLib.ConvertStringToNull(txtSectorFrom.Text), UDFLib.ConvertStringToNull(txtSectorTo.Text),UDFLib.ConvertStringToNull(txtTrvDateFrom.Text)
               , UDFLib.ConvertStringToNull(txtTrvDateTo.Text), UDFLib.ConvertStringToNull(txtPaxName.Text),UDFLib.ConvertStringToNull(status),UDFLib.ConvertIntegerToNull(Approver_ID), ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            rptParent.DataSource = ds;
            rptParent.DataBind();

        }
        catch { }
        finally { treq = null; }

    }
    
    protected void rptParent_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if ("SENDTICKET" == e.CommandName)
            {
                string UploadFilePath = ConfigurationManager.AppSettings["TRV_UPLOAD_PATH"];
                BLL_TRV_Attachment objatt = new BLL_TRV_Attachment();
                int MailID = objatt.Send_Ticket(int.Parse(e.CommandArgument.ToString()), int.Parse(Session["userid"].ToString()));
                string URL = String.Format("window.open('../crew/EmailEditor.aspx?ID=+" + MailID.ToString() + @"&FILEPATH=" + UploadFilePath.Remove(UploadFilePath.Length - 1, 1).Replace(@"\", @"\\") + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "k" + MailID.ToString(), URL, true);
            }

            if (e.CommandName == "delete")
            {
                BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
                TRequest.CancelRequest(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session["USERID"].ToString()));
                TRequest = null;
                ViewState["Status"] = "";

                string jsc = @"__doPostBack('ctl00$MainContent$lnkMenu' + currentlink, '')";
              
              //  string msgmodal = String.Format("asyncGet_Quote_Count_Approval(" + e.CommandArgument.ToString() + ",'divSendForApproval');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "jsc", jsc, true);


                
            }
            if (e.CommandName == "refund")
            {
                hdnRequestID.Value = e.CommandArgument.ToString();
                lblRequestIDRefund.Text = "Request ID : " + e.CommandArgument.ToString();
                string js = "showModal('dvPopup_Refund');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsPopup_Refund", js, true);

            }
            if (e.CommandName == "RequestApproval")
            {
                BLL_TRV_QuoteRequest QR = new BLL_TRV_QuoteRequest();

                ViewState["RequestID_App"] = e.CommandArgument.ToString();
                hdnRequestID_App.Value = e.CommandArgument.ToString();


                DataTable dt = QR.Get_Qtn_Approver_DeptMgr(0, 0);

                lstUserList.DataSource = dt;
                lstUserList.DataBind();
                lstUserList.Items.Insert(0, new ListItem("SELECT", "0"));
                lstUserList.SelectedIndex = 0;
                ListItem itemrmv = lstUserList.Items.FindByValue(Session["userid"].ToString());
                lstUserList.Items.Remove(itemrmv);


                ListBoxPOApprover.DataSource = QR.Get_Qtn_Approver();
                ListBoxPOApprover.DataBind();
                ListBoxPOApprover.Items.Insert(0, new ListItem("SELECT", "0"));
                ListBoxPOApprover.SelectedIndex = 0;

                lstUserList.Items.Remove(itemrmv);

                string msgmodal = String.Format("asyncGet_Quote_Count_Approval(" + e.CommandArgument.ToString() + ",'divSendForApproval');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Apprmodal", msgmodal, true);

            }
        }
        catch { }
    }

    protected void rptParent_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        try
        {

            if (rptParent.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    HtmlGenericControl noRecordsDiv = (e.Item.FindControl("NoRecords") as HtmlGenericControl);
                    if (noRecordsDiv != null)
                    {
                        noRecordsDiv.Visible = true;
                    }
                }
            }
            if (e.Item.ItemType == ListItemType.Header)
            {
                PlaceHolder rptPlhrHearderExpDate = (PlaceHolder)e.Item.FindControl("rptPlhrHearderExpDate");

                if (ViewState["Status"].ToString() == "RFQ SENT" || ViewState["Status"].ToString() == "QUOTE RECEIVED" || ViewState["Status"].ToString() == "")
                    rptPlhrHearderExpDate.Visible = true;
                else
                    rptPlhrHearderExpDate.Visible = false;

            }


            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
               
                System.Data.DataRowView drv = (System.Data.DataRowView)e.Item.DataItem;

                Label lblRemark = (Label)e.Item.FindControl("lblRemark");
                Label lblColorFlag = (Label)e.Item.FindControl("lblColorFlag");
                Label lblOptionExpiry = (Label)e.Item.FindControl("lblOptionExpiry");
                Label lblAttFlag = (Label)e.Item.FindControl("lblAttFlag");
                Label lblRqstID = (Label)e.Item.FindControl("lblRqstID");


                ImageButton imgAtt = (ImageButton)e.Item.FindControl("imgAtt");

                ImageButton imgAddAtt = (ImageButton)e.Item.FindControl("imgAddAtt");

                if (drv["reqstStatus"].ToString() == "RFQ SENT" || drv["reqstStatus"].ToString() == "QUOTE RECEIVED" || drv["reqstStatus"].ToString() == "NEW")
                {
                    (e.Item.FindControl("lblVesselname") as Label).Enabled = true;
                }
                else
                    (e.Item.FindControl("lblVesselname") as Label).Enabled = false;


                PlaceHolder rptPlhrExpDate = (PlaceHolder)e.Item.FindControl("rptPlhrExpDate");


                if (drv["reqstStatus"].ToString() == "RFQ SENT" || drv["reqstStatus"].ToString() == "QUOTE RECEIVED" || ViewState["Status"].ToString() == "")
                    rptPlhrExpDate.Visible = true;
                else
                    rptPlhrExpDate.Visible = false;


                HtmlGenericControl div = (HtmlGenericControl)e.Item.FindControl("divDateColor");

                if (lblColorFlag.Text == "RED")
                {
                    div.Attributes.Add("style", "background-color:Red;color:White;");
                }
                else if (lblColorFlag.Text == "ORANGE")
                {
                    div.Attributes.Add("style", "background-color:Orange;color:White;");
                }
                else if (lblColorFlag.Text == "YELLOW")
                {
                    div.Attributes.Add("style", "background-color:Yellow;color:Black;");
                }

                string ReqSts = "NOTAPPROVED";
                if (drv["reqstStatus"].ToString() != "RFQ SENT" && drv["reqstStatus"].ToString() != "QUOTE RECEIVED" && drv["reqstStatus"].ToString() != "NEW")
                {
                    ReqSts = "APPROVED";
                }
                imgAtt.Attributes.Add("onclick", "javascript:openAttachments('" + lblRqstID.Text.Trim() + "','" + ReqSts + "');false;");

                imgAddAtt.Attributes.Add("onclick", "javascript:openAttachments('" + lblRqstID.Text.Trim() + "','" + ReqSts + "');false;");

                if (lblAttFlag.Text.Trim() == "N")
                {
                    imgAtt.Visible = false;
                    imgAddAtt.Visible = true;
                }
                else
                {
                    imgAtt.Visible = true;
                    imgAddAtt.Visible = false;
                }

                // check for access rights
                if (objUA.Edit == 0)
                {
                    if (e.Item.ItemType == ListItemType.AlternatingItem)
                        (e.Item.FindControl("imgUserPrefcA") as ImageButton).Visible = false;
                    else
                        (e.Item.FindControl("imgUserPrefc") as ImageButton).Visible = false;

                    (e.Item.FindControl("imgSendRFQ") as HtmlImage).Visible = false;
                    (e.Item.FindControl("imgEvaluation") as HtmlImage).Visible = false;

                    (e.Item.FindControl("ImageButton4") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgRefund") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgUpdTicket") as Image).Visible = false;
                    (e.Item.FindControl("imgSendTicket") as ImageButton).Visible = false;
                    (e.Item.FindControl("lbtnRollBackTr") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgMarkTraveled") as ImageButton).Visible = false;
                }

                // check on action button
                if (drv["reqstStatus"].ToString() == "NEW")
                {

                    (e.Item.FindControl("imgEvaluation") as HtmlImage).Visible = false;
                    if (e.Item.ItemType == ListItemType.AlternatingItem)
                        (e.Item.FindControl("imgUserPrefcA") as ImageButton).Visible = false;
                    else
                        (e.Item.FindControl("imgUserPrefc") as ImageButton).Visible = false;

                    (e.Item.FindControl("ImageButton4") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgRefund") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgUpdTicket") as Image).Visible = false;
                    (e.Item.FindControl("imgSendTicket") as ImageButton).Visible = false;
                    (e.Item.FindControl("lbtnRollBackTr") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgPODetails") as Image).Visible = false;
                    (e.Item.FindControl("imgMarkTraveled") as ImageButton).Visible = false;


                }
                if (drv["reqstStatus"].ToString() == "QUOTE RECEIVED" || drv["reqstStatus"].ToString() == "RFQ SENT")
                {
                    (e.Item.FindControl("imgRefund") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgUpdTicket") as Image).Visible = false;
                    (e.Item.FindControl("imgSendTicket") as ImageButton).Visible = false;
                    (e.Item.FindControl("lbtnRollBackTr") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgPODetails") as Image).Visible = false;
                    (e.Item.FindControl("imgMarkTraveled") as ImageButton).Visible = false;
                }
                if (drv["reqstStatus"].ToString() == "APPROVED")
                {
                    if (e.Item.ItemType == ListItemType.AlternatingItem)
                        (e.Item.FindControl("imgUserPrefcA") as ImageButton).Visible = false;
                    else
                        (e.Item.FindControl("imgUserPrefc") as ImageButton).Visible = false;

                    (e.Item.FindControl("ImageButton1") as ImageButton).Visible = false;
                    (e.Item.FindControl("ImageButton4") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgSendRFQ") as HtmlImage).Visible = false;
                    (e.Item.FindControl("imgSendTicket") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgMarkTraveled") as ImageButton).Visible = false;
                }
                if (drv["reqstStatus"].ToString() == "ISSUED")
                {
                    if (e.Item.ItemType == ListItemType.AlternatingItem)
                        (e.Item.FindControl("imgUserPrefcA") as ImageButton).Visible = false;
                    else
                        (e.Item.FindControl("imgUserPrefc") as ImageButton).Visible = false;

                    (e.Item.FindControl("ImageButton1") as ImageButton).Visible = false;
                    (e.Item.FindControl("ImageButton4") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgSendRFQ") as HtmlImage).Visible = false;


                    if (drv["IsTravelled"].ToString() == "1")
                    {
                        (e.Item.FindControl("imgMarkTraveled") as ImageButton).Visible = false;
                        (e.Item.FindControl("imgUpdTicket") as Image).Visible = false;
                    }

                }
                if (drv["reqstStatus"].ToString() == "REFUND PENDING" || drv["reqstStatus"].ToString() == "REFUND" || drv["reqstStatus"].ToString() == "REFUND CLOSED")
                {
                    if (e.Item.ItemType == ListItemType.AlternatingItem)
                        (e.Item.FindControl("imgUserPrefcA") as ImageButton).Visible = false;
                    else
                        (e.Item.FindControl("imgUserPrefc") as ImageButton).Visible = false;

                    (e.Item.FindControl("imgSendRFQ") as HtmlImage).Visible = false;

                    (e.Item.FindControl("ImageButton4") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgRefund") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgUpdTicket") as Image).Visible = false;
                    (e.Item.FindControl("imgSendTicket") as ImageButton).Visible = false;
                    (e.Item.FindControl("lbtnRollBackTr") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgMarkTraveled") as ImageButton).Visible = false;
                    (e.Item.FindControl("ImageButton1") as ImageButton).Visible = false;
                }
                if (drv["reqstStatus"].ToString() == "CLOSED")
                {
                    if (e.Item.ItemType == ListItemType.AlternatingItem)
                        (e.Item.FindControl("imgUserPrefcA") as ImageButton).Visible = false;
                    else
                        (e.Item.FindControl("imgUserPrefc") as ImageButton).Visible = false;

                    (e.Item.FindControl("imgSendRFQ") as HtmlImage).Visible = false;
                    (e.Item.FindControl("imgEvaluation") as HtmlImage).Visible = false;

                    (e.Item.FindControl("ImageButton4") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgRefund") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgUpdTicket") as Image).Visible = false;
                    (e.Item.FindControl("imgSendTicket") as ImageButton).Visible = false;
                    (e.Item.FindControl("lbtnRollBackTr") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgPODetails") as Image).Visible = false;
                    (e.Item.FindControl("imgMarkTraveled") as ImageButton).Visible = false;
                    (e.Item.FindControl("ImageButton1") as ImageButton).Visible = false;

                }
                if (drv["reqstStatus"].ToString() == "CANCELLED")
                {
                    if (e.Item.ItemType == ListItemType.AlternatingItem)
                        (e.Item.FindControl("imgUserPrefcA") as ImageButton).Visible = false;
                    else
                        (e.Item.FindControl("imgUserPrefc") as ImageButton).Visible = false;

                    (e.Item.FindControl("imgSendRFQ") as HtmlImage).Visible = false;
                    (e.Item.FindControl("imgEvaluation") as HtmlImage).Visible = false;

                    (e.Item.FindControl("ImageButton4") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgRefund") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgUpdTicket") as Image).Visible = false;
                    (e.Item.FindControl("imgSendTicket") as ImageButton).Visible = false;
                    (e.Item.FindControl("lbtnRollBackTr") as ImageButton).Visible = false;
                    (e.Item.FindControl("imgPODetails") as Image).Visible = false;
                    (e.Item.FindControl("imgMarkTraveled") as ImageButton).Visible = false;
                    (e.Item.FindControl("ImageButton1") as ImageButton).Visible = false;

                }



            }
        }
        catch { }
    }

   protected void cmbFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindRequestList();
        }
        catch { }
    }

    protected void cmbVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindRequestList();

        }
        catch { }
    }

    protected void cmbVessel_OnDataBound(object source, EventArgs e)
    {

        cmbVessel.Items.Insert(0, new ListItem("-Select All-", "0"));

    }

    protected void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            BindRequestList();

        }
        catch { }
    }

    protected void cmdRefund_Click(object sender, EventArgs e)
    {
        BLL_TRV_TravelRequest TRequest = new BLL_TRV_TravelRequest();
        string ReqID = hdnRequestID.Value;

        TRequest.RefundRequest(UDFLib.ConvertToInteger(ReqID), UDFLib.ConvertToInteger(Session["USERID"].ToString()), txtRefunRemarks.Text);

        //string msg = "Please note that the subject request has been request for refund. Please process the same and update on the website. Regards SeaChange";
        //TRequest.SaveEmail("REFUND FOR REQUEST ID " + ReqID, "", "", msg, Convert.ToInt32(Session["USERID"].ToString()));
        TRequest = null;
        string msgmodal = String.Format("hideModal('dvPopup_Refund');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refundclose", msgmodal, true);

        //GetTravelRequests("");

        ViewState["Status"] = "";
        BindRequestList();

    }

    protected void cmdRefundCancel_Click(object sender, EventArgs e)
    {
        string msgmodal = String.Format("hideModal('dvPopup_Refund');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "refundclose", msgmodal, true);
    }


    public int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }



    protected void NavMenu_Click(object sender, EventArgs e)
    {
        string Selection = ((LinkButton)sender).CommandArgument;
        string status = "";
        ucCustomPagerItems.CurrentPageIndex = 1;

        txtSelMenu.Value = ((LinkButton)sender).ClientID;

        switch (Selection)
        {
            case "NEW":
                status = "NEW";
                ViewState["Status"] = "NEW";
                BindRequestList();

                break;
            case "RFQ":
                status = "RFQ SENT";
                ViewState["Status"] = "RFQ SENT";

                chkShowAllPendingApproval.Visible = true;
                BindRequestList();


                break;
            case "APP":
                status = "APPROVED";
                ViewState["Status"] = "APPROVED";
                BindRequestList();


                break;
            case "RCL":
                status = "CLOSED";
                ViewState["Status"] = "CLOSED";
                BindRequestList();


                break;
            case "TKT":
                status = "ISSUED";
                ViewState["Status"] = "ISSUED";
                BindRequestList();

                break;
            case "REF":
                status = "REFUND PENDING";
                ViewState["Status"] = "REFUND PENDING";
                BindRequestList();

                break;

            case "REC":

                status = "REFUND CLOSED";
                ViewState["Status"] = "REFUND CLOSED";
                BindRequestList();

                break;
            case "CEN":

                status = "CANCELLED";
                ViewState["Status"] = "CANCELLED";
                BindRequestList();

                break;
            case "ALL":
                status = "";
                ViewState["Status"] = "";
                BindRequestList();

                break;
        }

        ViewState["Status"] = status;
        //string js4 = "bindselectors();";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "bindselectors", js4, true);
    }

    protected void chkShowAllPendingApproval_CheckedChanged(object sender, EventArgs e)
    {
        chkShowAllPendingApproval.Visible = true;
        string status = "RFQ SENT";
        ViewState["Status"] = status;

        BindRequestList();
    }

    protected void btnSendForApproval_Click(object s, EventArgs e)
    {
        BLL_TRV_QuoteRequest QR = new BLL_TRV_QuoteRequest();

        int ApproverID = 0;
        int ForApproval;

        if (lstUserList.SelectedItem.Value == "0" && ListBoxPOApprover.SelectedItem.Value != "0") // sending for po approval
        {
            ApproverID = int.Parse(ListBoxPOApprover.SelectedItem.Value);
            ForApproval = 1;
        }
        else if (lstUserList.SelectedItem.Value != "0" && ListBoxPOApprover.SelectedItem.Value == "0")// sending for Manager approval
        {
            ApproverID = int.Parse(lstUserList.SelectedItem.Value);
            ForApproval = 2;
        }
        else if (lstUserList.SelectedItem.Value == "0" && ListBoxPOApprover.SelectedItem.Value == "0") // atleat  one should be selected
        {
            lblMessageOnSendApproval.Visible = true;
            lblMessageOnSendApproval.Text = "Please select Approver !";
            return;
        }
        else // both can not be selected at a time
        {
            lblMessageOnSendApproval.Visible = true;
            lblMessageOnSendApproval.Text = "PO approver and Managerial approver can not be selected simultaneously !";
            return;
        }

        if (ApproverID != 0)
        {
            QR.Insert_Approval_Entry(UDFLib.ConvertToInteger(Session["USERID"].ToString()),
                                    ApproverID,
                                     0,
                                     UDFLib.ConvertToInteger(ViewState["RequestID_App"]),
                                     txtRemark.Text,
                                     "", ForApproval);


            string msgmodal = String.Format("alert('Request sent successfully');hideModal('divSendForApproval');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Apprmodal", msgmodal, true);
        }
    }

    protected void imgUserPrefc_Click(object s, EventArgs e)
    {
        int ReqID = 0;
        if (s.GetType() == typeof(LinkButton))
        {
            Int32.TryParse(((LinkButton)s).CommandArgument, out ReqID);
        }
        else
        {
            Int32.TryParse(((ImageButton)s).CommandArgument, out ReqID);
        }


        btnSendUserPreference.CommandArgument = ReqID.ToString();
        BLL_TRV_TravelRequest objtrvrerq = new BLL_TRV_TravelRequest();
        string[] strUserListID = objtrvrerq.Get_User_Preference(ReqID).Split(',');

        foreach (string ID in strUserListID)
        {
            ListItem lit = chklistUser.Items.FindByValue(ID);
            if (lit != null)
                lit.Selected = true;
        }
        updUserlist.Update();
        string msgmodal = String.Format("showModal('dvUserList', false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", msgmodal, true);

    }


    protected void btnSendUserPreference_Click(object s, EventArgs e)
    {
        int ReqID = 0;
        string msgmodal = "";
        if (Int32.TryParse(((Button)s).CommandArgument, out ReqID))
        {
            DataTable dtUser = new DataTable();
            dtUser.Columns.Add("id");
            DataRow dr;
            foreach (ListItem lit in chklistUser.Items)
            {
                if (lit.Selected)
                {
                    dr = dtUser.NewRow();
                    dr["id"] = lit.Value;
                    dtUser.Rows.Add(dr);
                }
            }

            if (dtUser.Rows.Count > 0)
            {

                BLL_TRV_TravelRequest objtrvrerq = new BLL_TRV_TravelRequest();
                objtrvrerq.RequestUserPreference(ReqID, Convert.ToInt32(Session["userid"].ToString()), dtUser);
                msgmodal = String.Format("hideModal('dvUserList');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closeUseronsave", msgmodal, true);
            }
            else
            {
                msgmodal = String.Format("alert('Please select user .');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sendalUser", msgmodal, true);

            }


        }
    }

    protected void btnRollbackTR_Click(object s, EventArgs e)
    {
        BLL_TRV_TravelRequest objTr = new BLL_TRV_TravelRequest();
        objTr.RollBack_TravelRequest(Convert.ToInt32(hdf_TRID_Rollback.Value), txtRemarkRollback.Text, Convert.ToInt32(Session["userid"]));
        BindRequestList();
    }




    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRequestList();
    }
}