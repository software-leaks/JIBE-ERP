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
using BLLQuotation;
using Telerik.Web.UI;
using System.Text;



public partial class WebQuotation_WebQuotationDetails : System.Web.UI.Page
{

    public static int intNewQuot = 0;

    string ReqsnStatus = "";


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["SuppCode"]) == "")
            FormsAuthentication.RedirectToLoginPage();
        else if ((Session["pwd"].ToString().Trim() == Session["SuppCode"].ToString().Trim()))
        {
            Response.Redirect("ChangePassword.aspx");
        }

        if (!IsPostBack)
        {
            hdfSelectedStageValue.Value = "P-S";
            hdfSelectedStage.Value = lnkMenu1.ClientID;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectNode", "selMe('" + hdfSelectedStage.Value + "');", true);

            int count = BLL_PURC_Purchase.Get_NewContractRequest(Session["SuppCode"].ToString());
            if (count > 0)
                lbtnContractListAlert.Text = count.ToString() + " &nbsp; Pending Contract for your action ";
            BindGrid(out intNewQuot);
        }

        if (intNewQuot > 0 && intNewQuot == 1)
        {
            lblInboxMsg.Text = "There is " + intNewQuot + " new request for Quotation.";
        }
        else if (intNewQuot > 0 && intNewQuot > 1)
        {
            lblInboxMsg.Text = "There are " + intNewQuot + " new requests for Quotation.";
        }
        else
        {
            lblInboxMsg.Text = "";
        }

        //txtfrom.Text = (DateTime.Now.AddDays(-(DateTime.Now.Day) + 1)).ToString("dd-MM-yyyy");
        if (!IsPostBack)
        {
            BindVessel();
            txtfrom.Text = "01-01-2010";
            //txtto.Text = (DateTime.Now.AddMonths(1).AddDays(-(DateTime.Now.Day))).ToString("dd-MM-yyyy");


        }

        ReqsnStatus = hdfSelectedStageValue.Value;
        //  UserAccessValidation();

    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectNode", "selMe('" + hdfSelectedStage.Value + "');", true);
    }
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");

        if (objUA.Add == 0)
        {


        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Approve == 0)
        {
            rgdWebQuoDetails.Columns.FindByUniqueName("AcceptPO_ID").Visible = false;
        }
        if (objUA.Delete == 0)
        {


        }


    }

    protected void BindVessel()
    {

        clsQuotationBLL objQuoBLL = new clsQuotationBLL();
        DataTable dt = new DataTable();
        dt = objQuoBLL.GetVessel();

        DDLVessel.DataSource = dt;
        DDLVessel.DataTextField = "Vessels";
        DDLVessel.DataValueField = "Vessel_Code";
        DDLVessel.DataBind();

    }

    protected void BindGrid(out int intNewQuo)
    {
        ReqsnStatus = hdfSelectedStageValue.Value;
        clsQuotationBLL objQuoBLL = new clsQuotationBLL();
        DataTable dt = new DataTable();

        string strVesselCode = "", strStatus = "0", FromDT = "1900/01/01", ToDT = "2099/01/01";
        string Req_code = "0";
        if (txtReqNo.Text.Trim() != "")
        {
            Req_code = txtReqNo.Text;
        }


        if (DDLVessel.SelectedValue.ToString() == "0")
            strVesselCode = "0";
        else
            strVesselCode = DDLVessel.SelectedValue.ToString();

        if (ReqsnStatus.Length > 2)
        {
            string[] sts = ReqsnStatus.Split(new char[] { '-' });
            strStatus = sts[0] + sts[1];
        }
        else if (ReqsnStatus != "")
        {

            strStatus = ReqsnStatus.ToString();
        }
        else
            strStatus = ReqsnStatus.ToString();


        if (txtfrom.Text.Trim() != "")
        {
            FromDT = txtfrom.Text.Trim();
        }
        if (txtto.Text.Trim() != "")
        {
            ToDT = txtto.Text.Trim();
        }
        int isCount = ucCustomPager1.isCountRecord;
        dt = objQuoBLL.GetWebQuotation(Session["SuppCode"].ToString(), strVesselCode, strStatus, FromDT, ToDT, Req_code, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref isCount);

        ucCustomPager1.CountTotalRec = isCount.ToString();
        ucCustomPager1.BuildPager();

        rgdWebQuoDetails.DataSource = dt;
        rgdWebQuoDetails.DataBind();
        intNewQuo = 0;

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["Quotation_Status"].ToString() == "New")
            {
                intNewQuo = intNewQuo + 1;
            }
        }


        //Changes the Attache Image on the basis of whethere file is Attached or not with Requisiton.
        OperationRightsOnSupplier();

    }

    protected void BindGriddataitem()
    {
        ReqsnStatus = hdfSelectedStageValue.Value;
        clsQuotationBLL objQuoBLL = new clsQuotationBLL();
        DataTable dt = new DataTable();

        string strVesselCode = "", strStatus = "0", FromDT = "1900/01/01", ToDT = "2099/01/01";
        string Req_code = "0";
        if (txtReqNo.Text.Trim() != "")
        {
            Req_code = txtReqNo.Text;
        }


        if (DDLVessel.SelectedValue.ToString() == "0")
            strVesselCode = "0";
        else
            strVesselCode = DDLVessel.SelectedValue.ToString();

        if (ReqsnStatus.Length > 2)
        {
            string[] sts = ReqsnStatus.Split(new char[] { '-' });
            strStatus = sts[0] + sts[1];
        }
        else if (ReqsnStatus != "")
        {

            strStatus = ReqsnStatus;
        }
        else
            strStatus = ReqsnStatus;


        if (txtfrom.Text.Trim() != "")
        {
            FromDT = txtfrom.Text.Trim();
        }
        if (txtto.Text.Trim() != "")
        {
            ToDT = txtto.Text.Trim();
        }

        int isCount = ucCustomPager1.isCountRecord;
        dt = objQuoBLL.GetWebQuotation(Session["SuppCode"].ToString(), strVesselCode, strStatus, FromDT, ToDT, Req_code, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref isCount);

        ucCustomPager1.CountTotalRec = isCount.ToString();
        ucCustomPager1.BuildPager();


        rgdWebQuoDetails.DataSource = dt;
        rgdWebQuoDetails.DataBind();



        //Changes the Attache Image on the basis of whethere file is Attached or not with Requisiton.
        OperationRightsOnSupplier();

    }

    private void OperationRightsOnSupplier()
    {
        string strIsAttPO = "";
        string ReqStatus = "";
        bool IsRequiredConfirmaion = GetSupplierConfirmation();
        foreach (GridDataItem dataItem in rgdWebQuoDetails.MasterTableView.Items)
        {

            ImageButton ImgSelectPOPreview = (ImageButton)(dataItem.FindControl("ImgSelectPOPreview") as ImageButton);
            HyperLink btnPreviewPO = dataItem.FindControl("btnPreviewPO") as HyperLink;
            Button btnPOStatus = (Button)(dataItem.FindControl("btnPOStatus") as Button);
            strIsAttPO = dataItem["Quotation_Status"].Text.ToString();

            
            if (strIsAttPO == "PO Received" || strIsAttPO == "A")
            {
                btnPreviewPO.ToolTip = "Click to Preview PO";
                btnPOStatus.ToolTip = "Click to confirm PO";
                ReqStatus = GetRequistionQuotationStatus(Convert.ToString(dataItem["REQUISITION_CODE"].Text), Convert.ToString(dataItem["QUOTATION_CODE"].Text));
                if(IsRequiredConfirmaion == false && ReqStatus=="DLV")
                {
                    btnPOStatus.Enabled = false;
                }
            }
            else if (strIsAttPO == "PO Confirmed")
            {
                btnPreviewPO.Enabled = true;
                btnPOStatus.Enabled = false;
                btnPreviewPO.ToolTip = "Click to Preview PO";

            }
            else
            {

                btnPreviewPO.Enabled = false;
                btnPOStatus.Enabled = false;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid(out intNewQuot);

        OperationRightsOnSupplier();

    }

    protected void onPOConfirm(object source, CommandEventArgs e)
    {
        DataTable dtQuotationList = new DataTable();
        dtQuotationList.Columns.Add("Qtncode");
        dtQuotationList.Columns.Add("amount");

        string[] strIds = e.CommandArgument.ToString().Split(',');
        int intNewQuot = 0;
        string reqCode = strIds[0].ToString();
        string DocumentCode = strIds[1].ToString();
        string vesselCode = strIds[2].ToString();
        string QuotCode = strIds[3].ToString();
        string QuotStatus = strIds[4].ToString();
        string DeptCode = strIds[5].ToString();
        string strFormatSubject = "", strFormatBody = "", sToEmailAddress = "";
        string[] Attchment = new string[10];
        DataSet dsEmailInfor = new DataSet();
        clsQuotationBLL objQuoBLL = new clsQuotationBLL();

        DataRow dtrow = dtQuotationList.NewRow();
        dtrow[0] = QuotCode;
        dtrow[1] = "0";
        dtQuotationList.Rows.Add(dtrow);

        int intRet = objQuoBLL.UpdatePOConfirm(reqCode, vesselCode, DocumentCode, QuotCode, Session["SuppCode"].ToString().Trim());
        SMS.Business.PURC.BLL_PURC_Purchase objsts = new SMS.Business.PURC.BLL_PURC_Purchase();
        objsts.InsertRequisitionStageStatus(reqCode, vesselCode, DocumentCode, "UPD", " ", 1, dtQuotationList);


        BindGrid(out intNewQuot);


    }

    protected void FormateEmail(DataSet dsEmailInfo, string strServerIPAdd, out string sEmailAddress, out string strSubject, out string strBody, string[] Attachment)
    {

        strBody = "";
        sEmailAddress = "";
        strSubject = "";

    }


    protected void lbtnbank_Click(object sender, EventArgs e)
    {
      //  ResponseHelper.Redirect("http://seachange.dyndns.info/XT/asl/Supplier_data.asp?P=" + Session["PassString"].ToString(), "blank", "");
    }
    /// <summary>
    /// Add code for open link in OCA. Modified by Alok kumar/Dated = 5/07/2016
    /// Need to add Appsetting in web cofig for OCA_App_URL
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnInv_Click(object sender, EventArgs e)
    {
        try
        {
            string OCA_URL = null;
            if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["OCA_APP_URL"]))
            {
                OCA_URL = ConfigurationManager.AppSettings["OCA_APP_URL"];
            }
            string PassString = Session["PassString"].ToString();
            string OCA_URL1 = OCA_URL + "/PO_LOG/Supplier_Online_Invoice_Status_V2.asp?P=" + PassString + "";

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + OCA_URL1 + "');", true);
            //  ResponseHelper.Redirect("http://seachange.dyndns.info/XT/po_log/Supplier_Online_Invoice_Status.asp?P=" + Session["PassString"].ToString(), "blank", "");
        }
        catch { }
        {
        }
    }

    protected void lbtnContractListAlert_Click(object s, EventArgs e)
    {
        string strSitePath = ConfigurationManager.AppSettings["INVFolderPath"].ToString();
        ResponseHelper.Redirect("~/purchase/CTP_Contract_List.aspx?__supplier_`_code=" + Session["SuppCode"].ToString(), "_Blank", "");
    }

    protected void NavMenu_Click(object sender, EventArgs e)
    {
        hdfSelectedStageValue.Value = (((LinkButton)sender).CommandArgument).ToString();
        hdfSelectedStage.Value = ((LinkButton)sender).ClientID;
        BindGriddataitem();


    }

    protected string gerNavigationURL(string Selection)
    {
        string retURL = "";

        return retURL;
    }
    private bool GetSupplierConfirmation()
    {
        bool IsRequiredConfirmaion = true;

        SMS.Business.PURC.BLL_PURC_Purchase objBLLPURC = new SMS.Business.PURC.BLL_PURC_Purchase();
        //int Company_ID = Convert.ToInt32(Session["USERCOMPANYID"]);
        int Company_ID = 0;
        try
        {
            DataTable dtSupp = objBLLPURC.GET_AUTOMATIC_REQUISTION(Company_ID);
            if (dtSupp.Rows.Count > 0)
            {
                IsRequiredConfirmaion = Convert.ToBoolean(dtSupp.Rows[0]["Is_Req_Supplier_Confirm"]);
            }
        }
        catch
        {
        }
        return IsRequiredConfirmaion;
    }
    private string GetRequistionQuotationStatus(string ReqsnCode,string QuotationCode)
    {
        clsQuotationBLL objQuoBLL = new clsQuotationBLL();
        string RequisitionStatus=string.Empty;
        try
        {
            DataTable dtStatus = objQuoBLL.GetRequistion_Quotation_Status(ReqsnCode, QuotationCode);
            if (dtStatus.Rows.Count > 0)
            {
                RequisitionStatus = Convert.ToString(dtStatus.Rows[0]["REQSTATUS"]);
            }
        }
        catch
        {
        }
        return RequisitionStatus;
    }
}
