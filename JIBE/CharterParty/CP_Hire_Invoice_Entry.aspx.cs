using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.CP;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class CP_Hire_Invoice_Entry : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public int Inv_ID = 0;
    public int CPID = 0;

    public int PortId = 0;
    public string  PortName = "";
    public string OType = "";
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    BLL_CP_CharterParty objCP = new BLL_CP_CharterParty();
    BLL_CP_HireInvoice objHI = new BLL_CP_HireInvoice();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();

        if (!IsPostBack)
        {
            if (Session["CPID"] != null && Request.QueryString["Inv_ID"] != null)
            {
                CPID = Convert.ToInt32(Session["CPID"]);
                Inv_ID = Convert.ToInt32(Request.QueryString["Inv_ID"]);
                ViewState["InvID"] = Inv_ID.ToString();
            }
            BindStatus();
            BindOwnerBankList();
            BindHourMinDDL();
            BindInvDetails();

        }
    }

    protected void BindInvDetails()
    {
        try
        {
            CPID = Convert.ToInt32(Session["CPID"]);
            if (CPID != null)
            {
                DataTable dtCP = objCP.GET_Charter_Party_Details(UDFLib.ConvertIntegerToNull(CPID));
                if (dtCP.Rows.Count > 0)
                {
                    DataRow dr = dtCP.Rows[0];
                    if (dr["Bank_Account_ID"] != null)
                        ddlBank.SelectedValue = dr["Bank_Account_ID"].ToString();
                    txtAddressCommision.Text = dr["Address_Comm"].ToString();
                }


                if (ViewState["InvID"] != null && ViewState["InvID"].ToString() != "0")
                {
                    CPID = Convert.ToInt32(Session["CPID"]);
                    //Inv_ID = Convert.ToInt32(ViewState["InvID"]);

                    DataTable dt = objHI.Get_Hire_InvDetail(UDFLib.ConvertIntegerToNull(ViewState["InvID"]));
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        txtDate.Text = dr["Hire_Invoice_Date"].ToString();
                        txtDueDate.Text = dr["Due_Date"].ToString();
                        if (dr["Hire_Invoice_Status"] != null)
                        {
                            string Status = dr["Hire_Invoice_Status"].ToString();
                            string StatusText = ddlStatus.Items.FindByText(Status).Text;
                            string strStatus = dr["strStatus"].ToString();
                            ddlStatus.SelectedValue = ddlStatus.Items.FindByText(Status).Value;
                            if (StatusText == "APPROVED")
                            {
                                if (strStatus != "Matched")
                                    btnUnapprove.Visible = true;
                                btnApprove.Visible = false;
                            }
                            if (StatusText == "PLANNED")
                            {
                                btnUnapprove.Visible = false;
                                btnApprove.Visible = true;
                            }

                        }
                        if (dr["Bank_Account_ID"] != null && dr["Bank_Account_ID"].ToString() != "0")
                            ddlBank.SelectedValue = dr["Bank_Account_ID"].ToString();
                        if (dr["Period_From"] != null)
                        {
                            DateTime dtOpening = Convert.ToDateTime(dr["Period_From"]);
                            string Hour = dtOpening.Hour.ToString().Trim();
                            string Min = dtOpening.Minute.ToString().Trim();
                            if (Hour.Length == 1)
                                Hour = "0" + Hour;
                            if (Min.Length == 1)
                                Min = "0" + Min;
                            dtBillingStart.Text = dtOpening.ToString("dd-MMM-yyyy");
                            ddlBillingStartHours.SelectedValue = Hour;
                            ddlBillingStartMins.SelectedValue = Min;
                        }
                        if (dr["Period_To"] != null)
                        {
                            DateTime dtOpening = Convert.ToDateTime(dr["Period_To"]);
                            string Hour = dtOpening.Hour.ToString().Trim();
                            string Min = dtOpening.Minute.ToString().Trim();
                            if (Hour.Length == 1)
                                Hour = "0" + Hour;
                            if (Min.Length == 1)
                                Min = "0" + Min;
                            dtBillingEnd.Text = dtOpening.ToString("dd-MMM-yyyy");
                            ddlBillingEndHours.SelectedValue = Hour;
                            ddlBillingEndMins.SelectedValue = Min;
                        }
                        if (dr["Coverage"] != null)
                        {
                            
                            ltCoverageOfdays.Text = " Coverage of " + dr["Coverage"].ToString() + " days.";
                        }
                        
                        txtReference.Text = dr["InvoiceRef"].ToString();
                        txtInvAmount.Text = dr["Billed_Amount"].ToString();
                        txtAddressCommision.Text = dr["Address_Comm"].ToString();
                        txtReceivedamount.Text = dr["Received_Amount"].ToString();

                        txtRcvDate.Text = dr["Received_Date"].ToString();
                        txtInvRemarks.Text = dr["invoice_Remarks"].ToString();
                        if (dr["DUE"].ToString() == "NOT DUE")
                            ltOutstandingamt.Text = "NOT DUE";
                        else
                            ltOutstandingamt.Text = dr["DUE"].ToString() + " US$";
                    }


                    if (txtInvAmount.Text == "")
                        btnApprove.Visible = false;
                }
                else
                    txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
        catch (Exception ex)
        {
          string err=  ex.ToString();
        }
    }

    protected void BindStatus()
    {
        DataTable dt = objHI.GET_Hire_InvStatusList();
        ddlStatus.DataSource = dt;
        ddlStatus.DataTextField = "VARIABLE_NAME";
        ddlStatus.DataValueField = "ID";
        ddlStatus.DataBind();
        //ddlStatus.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void BindOwnerBankList()
    {
        DataTable dt = objCP.GetOwnerBank_List();
        ddlBank.DataSource = dt;
        ddlBank.DataTextField = "Account";
        ddlBank.DataValueField = "Bank_Account_ID";
        ddlBank.DataBind();
        ddlBank.Items.Insert(0, new ListItem("-Select-", "0"));
        dt.Dispose();
    }

    public void BindHourMinDDL()
    {
        LoadHour(ddlBillingEndHours);
        LoadHour(ddlBillingStartHours);
        LoadMin(ddlBillingStartMins);
        LoadMin(ddlBillingEndMins);
    }

    public void LoadHour(DropDownList ddlHH)
    {
        ListItemCollection lic = new ListItemCollection();
        int count = 0;
        while (count < 24)
        {
            ListItem li = new ListItem();
            if (count < 10)
            {
                li.Text = "0" + count.ToString();
                li.Value = "0" + count.ToString();
            }
            else
            {
                li.Text = count.ToString();
                li.Value = count.ToString();
            }
            lic.Add(li);
            count++;

        }
        ddlHH.DataSource = lic;
        ddlHH.DataBind();

    }

    public void LoadMin(DropDownList ddlMin)
    {
        ListItemCollection lic = new ListItemCollection();
        int count = 0;
        while (count < 60)
        {
            ListItem li = new ListItem();
            if (count < 10)
            {
                li.Text = "0" + count.ToString();
                li.Value = "0" + count.ToString();
            }
            else
            {
                li.Text = count.ToString();
                li.Value = count.ToString();
            }
            lic.Add(li);
            count++;

        }
        ddlMin.DataSource = lic;
        ddlMin.DataBind();
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnSave.Enabled = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnSave.Enabled = false;
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    protected void ClearData()
    {

        ddlStatus.SelectedValue = "0";
        ddlBank.SelectedValue = "0";
        ltmessage.Text = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();

    }
    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        SaveData();
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "refreshAndClose();", true);
    }
    
    protected void SaveData()
    {
        try
        {
            string BillingStart = "";
            string BillingEnd = "";
            string LayCanEndDate = "";
            string Delivery_Date = "";
            string ReDelivery_Date = "";
            int msg = 0;
            double AddressComm = 0.00;
            double InvoiceAmt = 0.00;
            double RecveiveAmt = 0.00;
            if (txtAddressCommision.Text != "")
                AddressComm = Convert.ToDouble(txtAddressCommision.Text);
            if (txtInvAmount.Text != "")
                InvoiceAmt = Convert.ToDouble(txtInvAmount.Text);
            if (txtReceivedamount.Text != "")
                RecveiveAmt = Convert.ToDouble(txtReceivedamount.Text);

            if (dtBillingStart.Text != "")
                BillingStart = AddHourMin(dtBillingStart.Text, ddlBillingStartHours.SelectedValue, ddlBillingStartMins.SelectedValue);
            if (dtBillingEnd.Text != "")
                BillingEnd = AddHourMin(dtBillingEnd.Text, ddlBillingStartHours.SelectedValue, ddlBillingStartMins.SelectedValue);
            if (Session["CPID"] != null)
            {
                if (ViewState["InvID"] != null && ViewState["InvID"].ToString() != "0")
                {
                    msg = objHI.UPD_Hire_Invoice(UDFLib.ConvertIntegerToNull(ViewState["InvID"]), txtReference.Text, UDFLib.ConvertIntegerToNull(ddlStatus.SelectedValue), ddlStatus.SelectedItem.Text, ddlBank.SelectedValue, AddressComm, InvoiceAmt, RecveiveAmt,
                        UDFLib.ConvertDateToNull(BillingStart), UDFLib.ConvertDateToNull(BillingEnd), UDFLib.ConvertDateToNull(txtDate.Text), UDFLib.ConvertDateToNull(txtDueDate.Text), txtInvRemarks.Text, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
                }
                else if (ViewState["InvID"].ToString() == "0")
                {
                    int RetValue = objHI.INS_Hire_Invoice(UDFLib.ConvertIntegerToNull(Session["CPID"]), UDFLib.ConvertIntegerToNull(ddlStatus.SelectedValue),ddlStatus.SelectedItem.Text, ddlBank.SelectedValue, txtReference.Text, AddressComm, InvoiceAmt,
                        UDFLib.ConvertDateToNull(BillingStart), UDFLib.ConvertDateToNull(BillingEnd), UDFLib.ConvertDateToNull(txtDate.Text),UDFLib.ConvertDateToNull(txtDueDate.Text),txtInvRemarks.Text, UDFLib.ConvertToInteger(Session["UserID"].ToString()));

                    ViewState["InvID"] = RetValue.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.ToString();
        }
    }


    protected string AddHourMin(string dtText, string HH, string MIN)
    {
        DateTime dt;

        dt = Convert.ToDateTime(dtText);
        int TotalMin = Convert.ToInt16(HH) * 60 + Convert.ToInt16(MIN);
        dt = dt.AddMinutes(TotalMin);

        return dt.ToString();
    }



    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ListItem itm = ddlStatus.Items.FindByText("APPROVED");
            ddlStatus.SelectedValue = itm.Value;
            SaveData();

            btnUnapprove.Visible = true;
            btnApprove.Visible = false;
            btnSaveClose.Visible = false;
            btnSave.Visible = false;
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "refreshAndClose();", true);

        }
        catch(Exception ex)
        {
            string error = ex.ToString();
        }
    }
    protected void btnUnapprove_Click(object sender, EventArgs e)
    {
        try
        {
            ListItem itm = ddlStatus.Items.FindByText("PLANNED");
            ddlStatus.SelectedValue = itm.Value;
            SaveData();

            btnUnapprove.Visible = false;
            btnApprove.Visible = true;
            btnSaveClose.Visible = true;
            btnSave.Visible = true;
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "refreshAndClose();", true);
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
        }
    }
}
   
