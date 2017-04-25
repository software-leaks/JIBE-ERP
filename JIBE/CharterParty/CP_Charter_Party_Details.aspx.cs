using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.CP;
using System.IO;
using SMS.Properties;


public partial class CP_Charter_Party_Details : System.Web.UI.Page
{

    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_CP_CharterParty oCP = new BLL_CP_CharterParty();
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
    UserAccess objUA = new UserAccess();
    public string Supplier = null;
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    public Boolean ChangeRequest = false;
    public Boolean Registered = false;
    int CPID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        UserAccessValidation();
        if (!IsPostBack)
         {
            if (Request.QueryString["CPID"] != null)
            {
                CPID = Convert.ToInt32(Request.QueryString["CPID"]);
                Session["CPID"] = CPID.ToString();
            }

            if (CPID != 0)
            {
                dtCP.Text = DateTime.Now.ToString();
                CPID = Convert.ToInt32(Session["CPID"]);
            }
            LoadRemark();
            BindPort();
            BindCPStatus();
            BindCPType();
            BindVessels();
            BindOwnerList();
            BindOwnerBankList();
            BindChartererList();
            BindBrokerList();
            BindBrokerList2();
            BindBrokerList3();
            BindHourMinDDL();
            BindHireType();
            BindCPDetails();
            BindFuelType();
           }


    }
    protected void BindFuelType()
    {
        DataTable dt = oCP.Get_FuelTypes();
        ddlFuelType.DataSource = dt;
        ddlFuelType.DataTextField = "Fuel_Name";
        ddlFuelType.DataValueField = "FuelType_ID";
        ddlFuelType.DataBind();
        dt.Dispose();
    }

    protected void InitDeliverySection()
    {
        if (Session["CPID"] != null)
            CPID = Convert.ToInt32(Session["CPID"]);

        if (CPID > 0)
        {
            lblSaveFirst.Visible = false;
            //btnDeliverybunker.Enabled = true;
            //btnReDeliveryBunker.Enabled = true;
        }
    }

    protected void BindPort()
    {

        DataTable dt = objBLLPort.Get_PortList_Mini();
        ddlPort.DataSource = dt;
        ddlPort.DataTextField = "PORT_NAME";
        ddlPort.DataValueField = "PORT_ID";
        ddlPort.DataBind();
        ddlPort.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void BindBrokerList2()
    {

        DataTable dt = oCP.GetSupplierBroker_List();
        ddlBroker2.DataSource = dt;
        ddlBroker2.DataTextField = "Supplier_Name";
        ddlBroker2.DataValueField = "Supplier_Code";
        ddlBroker2.DataBind();
        ddlBroker2.Items.Insert(0, new ListItem("-Select-", "0"));

    }
    protected void BindBrokerList3()
    {

        DataTable dt = oCP.GetSupplierBroker_List();
        ddlBroker3.DataSource = dt;
        ddlBroker3.DataTextField = "Supplier_Name";
        ddlBroker3.DataValueField = "Supplier_Code";
        ddlBroker3.DataBind();
        ddlBroker3.Items.Insert(0, new ListItem("-Select-", "0"));

    }

    protected void BindHireType()
    {

        DataTable dt = oCP.GetHireType_List();
        ddlHireType.DataSource = dt;
        ddlHireType.DataTextField = "Variable_Code";
        ddlHireType.DataValueField = "ID";
        ddlHireType.DataBind();
       // ddlHireType.Items.Insert(0, new ListItem("-Select-", "0"));

    }
    protected void BindOwnerList()
    {
        DataTable dt = oCP.GetSupplierOwner_List();
        ddlOwner.DataSource = dt;
        ddlOwner.DataTextField = "Supplier_Name";
        ddlOwner.DataValueField = "Supplier_Code";
        ddlOwner.DataBind();
        ddlOwner.Items.Insert(0, new ListItem("-Select-", "0"));
        dt.Dispose();


    }
    protected void BindChartererList()
    {
        DataTable dt = oCP.GetSupplierCharterer_List();
        ddlCharterer.DataSource = dt;
        ddlCharterer.DataTextField = "Supplier_Name";
        ddlCharterer.DataValueField = "Supplier_Code";
        ddlCharterer.DataBind();
        ddlCharterer.Items.Insert(0, new ListItem("-Select-", "0"));
        dt.Dispose();


    }

    protected void BindBrokerList()
    {
        DataTable dt = oCP.GetSupplierBroker_List();
        ddlBroker.DataSource = dt;
        ddlBroker.DataTextField = "Supplier_Name";
        ddlBroker.DataValueField = "Supplier_Code";
        ddlBroker.DataBind();
        ddlBroker.Items.Insert(0, new ListItem("-Select-", "0"));
        dt.Dispose();


    }
    protected void BindOwnerBankList()
    {
        DataTable dt = oCP.GetOwnerBank_List();
        ddlOwnerBank.DataSource = dt;
        ddlOwnerBank.DataTextField = "Account";
        ddlOwnerBank.DataValueField = "Bank_Account_ID";
        ddlOwnerBank.DataBind();
        ddlOwnerBank.Items.Insert(0, new ListItem("-Select-", "0"));
        dt.Dispose();
    }

    protected void BindCPStatus()
    {
        DataTable dt = oCP.CP_GetStatus_List();
        ddlStatus.DataSource = dt;
        ddlStatus.DataTextField = "Variable_Code";
        ddlStatus.DataValueField = "ID";
        ddlStatus.DataBind();
        dt.Dispose();

    }
    protected void BindCPType()
    {
        DataTable dt = oCP.GetType_List();
        ddlCPType.DataSource = dt;
        ddlCPType.DataTextField = "Variable_Code";
        ddlCPType.DataValueField = "ID";
        ddlCPType.DataBind();
        dt.Dispose();

    }
    protected void BindVessels()
    {

        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-Select-", "0"));
        dt.Dispose();
    }
    protected void BindHourMinDDL()
    {
        LoadHour(ddlDeliveryPortHour);
        LoadHour(ddlLayCanEndHours);
        LoadHour(ddlLayCanStartHours);
        LoadHour(ddlRedeliveryHour);
        LoadMin(ddlDeliveryPortMin);
        LoadMin(ddlLayCanEndMins);
        LoadMin(ddlLayCanStartMins);
        LoadMin(ddlRedeliveryMin);
        LoadHour(ddlOpeningHours);
        LoadMin(ddlOpeningMins);
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

        }
        if (objUA.Edit == 1)
        {
            uaEditFlag = true;
        }
        else
        {
            btnSave.Enabled = false;
            btnSaveExit.Enabled = false;
        }
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void BindCPDetails()
    {
        clearControl();
        if (Session["CPID"] != null)
          CPID = Convert.ToInt32(Session["CPID"]);
        try
        {
            if (CPID != 0)
            {
                btnAddDeliveryBunker.Visible = true;
                btnAddRedeliveryBunker.Visible = true;
                DataTable dt = oCP.GET_Charter_Party_Details(UDFLib.ConvertIntegerToNull(CPID));
                if (dt.Rows.Count > 0)
                 {
                    DataRow dr = dt.Rows[0];
                    lblCRNo.Text = dr["Charter_Id"].ToString();
                   if( dr["Vessel_Opening_Port_ID"] != null)
                        ddlPort.SelectedValue = dr["Vessel_Opening_Port_ID"].ToString();
  
                       txtDeliveryPort.Text = dr["Delivery_Port"].ToString();
                     if( dr["Delivery_Date_Zone"] != null) 
                       ddlDeliveryLTGMT.SelectedValue = dr["Delivery_Date_Zone"].ToString(); 
                   
                       txtRedeliveryPort.Text = dr["Redelivery_Port"].ToString();
                     if( dr["ReDelivery_Date_Zone"] != null)
                       ddlReDeliveryLTGMT.SelectedValue = dr["ReDelivery_Date_Zone"].ToString(); 


                    if (dr["Vessel_Opening_Date"] != null)
                    {
                        DateTime dtOpening = Convert.ToDateTime(dr["Vessel_Opening_Date"]);
                        dtOP.Text = dtOpening.ToString("dd-MMM-yyyy");
                        string Hour = dtOpening.Hour.ToString().Trim();
                        string Min = dtOpening.Minute.ToString().Trim();
                        if (Hour.Length == 1)
                            Hour = "0" + Hour;
                        if (Min.Length == 1)
                            Min = "0" + Min;

                        ddlOpeningHours.SelectedValue = Hour;
                        ddlOpeningMins.SelectedValue = Min;
                    }


                    DateTime dtCPDate = Convert.ToDateTime(dr["CP_Date"]);
                    dtCP.Text = dtCPDate.ToString("dd-MMM-yyyy");
                    ddlStatus.SelectedValue = dr["CP_Status_ID"].ToString();
                    ddlCPType.SelectedValue = dr["CP_Type_ID"].ToString();
                    ddlVessel.SelectedValue = dr["Vessel_Id"].ToString();
                    if (dr["Charterer_Code"] != null)
                        ddlCharterer.SelectedValue = dr["Charterer_Code"].ToString();
                    if (dr["Vessel_Opening_Port_ID"] != null)
                    {
                        ddlPort.SelectedValue = dr["Vessel_Opening_Port_ID"].ToString();
                       
                    }
  

                    if (dr["OWNER_Code"] != null)
                        ddlOwner.SelectedValue = dr["OWNER_Code"].ToString();
                    if (dr["Broker_Code"] != null)
                        ddlBroker.SelectedValue = dr["Broker_Code"].ToString();
                    if (dr["Broker2_Code"] != null)
                        ddlBroker2.SelectedValue = dr["Broker2_Code"].ToString();
                    if (dr["Broker3_Code"] != null)
                        ddlBroker3.SelectedValue = dr["Broker3_Code"].ToString();

                    if (dr["Bank_Account_ID"] != null)
                        ddlOwnerBank.SelectedValue = dr["Bank_Account_ID"].ToString();
                    if (dr["Hire_Type_ID"] != null)
                        ddlHireType.SelectedValue = dr["Hire_Type_ID"].ToString();

                   // txtLaycan.Text = dr["LayCan"].ToString();
                    txtGMTTimeZone.Text = dr["LayCan_Time_Zone"].ToString();
                    txtRedeliveryDays.Text = dr["Redelivery_Notice_Days"].ToString();
                    if (dr["LayCan_Start"] != null && dr["LayCan_Start"].ToString() != "")
                    {
                        DateTime dtLCStart = Convert.ToDateTime(dr["LayCan_Start"]);
                        string LCHour =  dtLCStart.Hour.ToString().Trim();
                        string LCMin = dtLCStart.Minute.ToString().Trim();
                        if (LCHour.Length == 1)
                            LCHour = "0" + LCHour;
                        if (LCMin.Length == 1)
                            LCMin = "0" + LCMin;
                        dtLayCanStart.Text = dtLCStart.ToString("dd-MMM-yyyy");

                        ddlLayCanStartHours.SelectedValue = LCHour;
                        ddlLayCanStartMins.SelectedValue = LCMin;
                    }

                    if (dr["LayCan_End"] != null && dr["LayCan_End"].ToString() != "")
                    {
                        DateTime dtLCEnd = Convert.ToDateTime(dr["LayCan_End"]);
                        string LCHour = dtLCEnd.Hour.ToString().Trim();
                        string LCMin = dtLCEnd.Minute.ToString().Trim();
                        if (LCHour.Length == 1)
                            LCHour = "0" + LCHour;
                        if (LCMin.Length == 1)
                            LCMin = "0" + LCMin;
                        dtLayCanEnd.Text = dtLCEnd.ToString("dd-MMM-yyyy");
                        ddlLayCanEndHours.SelectedValue = LCHour;
                        ddlLayCanEndMins.SelectedValue = LCMin;
                    }
                    if (dr["Delivery_Date"] != null && dr["Delivery_Date"].ToString() != "")
                    {
                        DateTime dtDelivery = Convert.ToDateTime(dr["Delivery_Date"]);
                        string Hour = dtDelivery.Hour.ToString().Trim();
                        string Min = dtDelivery.Minute.ToString().Trim();
                        if (Hour.Length == 1)
                            Hour = "0" + Hour;
                        if (Min.Length == 1)
                            Min = "0" + Min;
                        dtdelivery.Text = dtDelivery.ToString("dd-MMM-yyyy");
                        ddlDeliveryPortHour.SelectedValue = Hour;
                        ddlDeliveryPortMin.SelectedValue = Min;
                    }
                    if (dr["Redelivery_Date"] != null && dr["Redelivery_Date"].ToString() != "")
                    {
                        DateTime dtReDelivery = Convert.ToDateTime(dr["Redelivery_Date"]);
                        dtRedelivery.Text = dtReDelivery.ToString("dd-MMM-yyyy");
                        string Hour = dtReDelivery.Hour.ToString().Trim();
                        string Min = dtReDelivery.Minute.ToString().Trim();
                        if (Hour.Length == 1)
                            Hour = "0" + Hour;
                        if (Min.Length == 1)
                            Min = "0" + Min;

                        ddlRedeliveryHour.SelectedValue = Hour;
                        ddlRedeliveryMin.SelectedValue = Min;
                    }
                    txtHireTerms.Text = dr["Hire_Terms"].ToString();
                    txtCommisionTerms.Text = dr["Commission_Terms"].ToString();
                    txtCurrentHireRate.Text = dr["Hire_Rate"].ToString();
                    txtAddressCommision.Text = dr["Address_Comm"].ToString();
                    txtBrokCommision.Text = dr["Brokerage_Comm"].ToString();
                    txtBrokCommision2.Text = dr["Brokerage2_Comm"].ToString();
                    txtBrokCommision3.Text = dr["Brokerage3_Comm"].ToString();

                    txtBillingCycle.Text = dr["Billing_Period"].ToString();
                    txtSpreadBy.Text = dr["Billing_Cycle_Value"].ToString();
                    if (dr["Billing_Cycle_Type"] != null)
                        ddlSpreadByInterval.SelectedItem.Text = dr["Billing_Cycle_Type"].ToString();
                    if (dr["Brokerage_Comm_Payment"] != null)
                        ddlBrokPayment.SelectedValue = dr["Brokerage_Comm_Payment"].ToString();
                    if (dr["Brokerage2_Comm_Payment"] != null)
                        ddlBrokPayment2.SelectedValue = dr["Brokerage2_Comm_Payment"].ToString();
                    if (dr["Brokerage3_Comm_Payment"] != null)
                        ddlBrokPayment3.SelectedValue = dr["Brokerage3_Comm_Payment"].ToString();
                }
                BindBunkerDetail();
                ReBindBunkerDetail();

                }
                else
                {
                    lblCRNo.Text = "[Not Created.]";
                    lblCRNo.ForeColor = System.Drawing.Color.Red;
                    
                }

            }
        catch { }
        }



    protected void BindBunkerDetail()
    {

        DataTable dt = oCP.Get_BunkerList(UDFLib.ConvertIntegerToNull(Session["CPID"]), "D");

        gvDeliveryBunker.DataSource = dt;
        gvDeliveryBunker.DataBind();
    }


    protected void ReBindBunkerDetail()
    {

        DataTable dt = oCP.Get_BunkerList(UDFLib.ConvertIntegerToNull(Session["CPID"]), "R");

        gvReDeliveryBunker.DataSource = dt;
        gvReDeliveryBunker.DataBind();
    }


    public string GetSuppID()
    {
        try
        {
            if (ltCPRef.Text.Trim() != "")
            {
                ddlBroker.Text = Supplier;
                return ddlBroker.Text.ToString();
            }
            else if (Request.QueryString["CPID"] != null)
            {
                return Request.QueryString["CPID"].ToString();
            }

            else
                return "00000";
        }
        catch { return "00000"; }
    }


    protected void clearControl()
    {
        dtOP.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        dtCP.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        txtCurrentHireRate.Text = "0.00";
        txtAddressCommision.Text = "0.00";
        txtBrokCommision.Text = "0.00";
        //dtdelivery.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        txtRedeliveryDays.Text = "0";
        
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
    }
    protected void btnSaveExit_Click(object sender, EventArgs e)
    {
        SaveData();
        Response.Redirect("../CharterParty/CP_Charter_Party_List.aspx");
    }

    protected void SaveData()
    {
        try
        {
            if (Session["CPID"] != null)
            {
                string Vessel_Opening_Date = "";
                string LayCanStartDate = "";
                string LayCanEndDate = "";
                string Delivery_Date = "";
                string ReDelivery_Date = "";
                string Laycan = "";
                double HireRate = 0.00;
                double AddressComm = 0.00;
                double BrokerageComm = 0.00;
                double BrokerageComm2 = 0.00;
                double BrokerageComm3 = 0.00;
                string Billing_Cycle_Type = ddlSpreadByInterval.SelectedItem.Text;
                int msg = 0;
                CPID = Convert.ToInt32(Session["CPID"]);
                if (txtCurrentHireRate.Text != "")
                    HireRate = Convert.ToDouble(txtCurrentHireRate.Text);
                if (txtAddressCommision.Text != "")
                    AddressComm = Convert.ToDouble(txtAddressCommision.Text);
                if (txtBrokCommision.Text != "")
                    BrokerageComm = Convert.ToDouble(txtBrokCommision.Text);
                if (txtBrokCommision2.Text != "")
                    BrokerageComm2 = Convert.ToDouble(txtBrokCommision2.Text);
                if (txtBrokCommision3.Text != "")
                    BrokerageComm3 = Convert.ToDouble(txtBrokCommision3.Text);
                double Redelivery_Notice_Days = Convert.ToDouble(txtRedeliveryDays.Text);
                if (dtOP.Text != "")
                    Vessel_Opening_Date = AddHourMin(dtOP.Text, ddlOpeningHours.SelectedValue, ddlOpeningMins.SelectedValue);
                if (dtdelivery.Text != "")
                    Delivery_Date = AddHourMin(dtdelivery.Text, ddlDeliveryPortHour.SelectedValue, ddlDeliveryPortMin.SelectedValue);
                if (dtRedelivery.Text != "")
                    ReDelivery_Date = AddHourMin(dtRedelivery.Text, ddlRedeliveryHour.SelectedValue, ddlRedeliveryMin.SelectedValue);
                if (dtLayCanStart.Text != "")
                    LayCanStartDate = AddHourMin(dtLayCanStart.Text, ddlLayCanStartHours.SelectedValue, ddlLayCanStartMins.SelectedValue);
                if (dtLayCanEnd.Text != "")
                    LayCanEndDate = AddHourMin(dtLayCanEnd.Text, ddlLayCanEndHours.SelectedValue, ddlLayCanEndMins.SelectedValue);
                if (CPID != 0)
                {

                    msg = oCP.UPD_CharterParty(CPID, UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), ddlCharterer.SelectedValue, ddlOwner.SelectedValue, ddlBroker.SelectedValue,
                        ddlBroker2.SelectedValue, ddlBroker3.SelectedValue, UDFLib.ConvertDateToNull(Vessel_Opening_Date), UDFLib.ConvertIntegerToNull(ddlPort.SelectedValue), txtDeliveryPort.Text,
                        txtRedeliveryPort.Text,
                      ddlDeliveryLTGMT.SelectedValue, ddlReDeliveryLTGMT.SelectedValue, UDFLib.ConvertDateToNull(Delivery_Date), UDFLib.ConvertDateToNull(ReDelivery_Date), UDFLib.ConvertDateToNull(dtCP.Text),
                      UDFLib.ConvertIntegerToNull(ddlCPType.SelectedValue), UDFLib.ConvertIntegerToNull(ddlStatus.SelectedValue), Laycan, txtGMTTimeZone.Text, UDFLib.ConvertDateToNull(LayCanStartDate),
                      UDFLib.ConvertDateToNull(LayCanEndDate), txtHireTerms.Text, txtCommisionTerms.Text, HireRate, AddressComm, txtBillingCycle.Text, Billing_Cycle_Type, txtSpreadBy.Text, BrokerageComm,
                     BrokerageComm2, BrokerageComm3, ddlBrokPayment.SelectedValue, ddlBrokPayment2.SelectedValue, ddlBrokPayment3.SelectedValue,
                      UDFLib.ConvertIntegerToNull(ddlHireType.SelectedValue), ddlOwnerBank.SelectedValue, Redelivery_Notice_Days, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
                }
                else
                {
                    int RetValue = oCP.Ins_CharterParty(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), ddlCharterer.SelectedValue, ddlOwner.SelectedValue, ddlBroker.SelectedValue,
                        ddlBroker2.SelectedValue, ddlBroker3.SelectedValue, UDFLib.ConvertDateToNull(Vessel_Opening_Date), UDFLib.ConvertIntegerToNull(ddlPort.SelectedValue), txtDeliveryPort.Text, txtRedeliveryPort.Text,
                        ddlDeliveryLTGMT.SelectedValue, ddlReDeliveryLTGMT.SelectedValue,
                        UDFLib.ConvertDateToNull(Delivery_Date), UDFLib.ConvertDateToNull(ReDelivery_Date), UDFLib.ConvertDateToNull(dtCP.Text),
                        UDFLib.ConvertIntegerToNull(ddlCPType.SelectedValue), UDFLib.ConvertIntegerToNull(ddlStatus.SelectedValue), Laycan, txtGMTTimeZone.Text, UDFLib.ConvertDateToNull(LayCanStartDate),
                        UDFLib.ConvertDateToNull(LayCanEndDate), txtHireTerms.Text, txtCommisionTerms.Text, HireRate, AddressComm, txtBillingCycle.Text, Billing_Cycle_Type, txtSpreadBy.Text, BrokerageComm,
                        BrokerageComm2, BrokerageComm3, ddlBrokPayment.SelectedValue, ddlBrokPayment2.SelectedValue, ddlBrokPayment3.SelectedValue,
                        UDFLib.ConvertIntegerToNull(ddlHireType.SelectedValue), ddlOwnerBank.SelectedValue, Redelivery_Notice_Days, UDFLib.ConvertToInteger(Session["UserID"].ToString()));

                    Session["CPID"] = RetValue;
                }
                BindCPDetails();
            }
        }
        catch(Exception ex)
        {
            string msg = ex.ToString();
        }

    }

    protected string AddHourMin(string dtText , string HH, string MIN)
    {
        DateTime dt;

        dt = Convert.ToDateTime(dtText);
        int TotalMin = Convert.ToInt16(HH) * 60 + Convert.ToInt16(MIN);
        dt = dt.AddMinutes(TotalMin);

        return dt.ToString();
    }



    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try{
        if (ddlVessel.SelectedValue != "0")
            dt = oCP.GET_VesselOwner_Detail(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            ListItem li = new ListItem();
            string Owner_Id = dr["Owner_Id"].ToString();
            string Bank_Account_Id = dr["Bank_Account_ID"].ToString();
            if (Owner_Id != "0")
            {
               li =  ddlOwner.Items.FindByValue(Owner_Id);
               if (li != null)
               {
                   ddlOwner.SelectedValue = Owner_Id;
                   li.Attributes.Add("style", "background-color:#FFFF00");
               }
               else
                   ddlOwner.SelectedValue = "0";
            }
            if (Bank_Account_Id != "0")
            {
               li = ddlOwnerBank.Items.FindByValue(Bank_Account_Id);
               if (li != null)
               {
                   ddlOwnerBank.SelectedValue = Bank_Account_Id;
                   li.Attributes.Add("style", "background-color:#FFFF00");
               }
               else
                   ddlOwnerBank.SelectedValue = "0";
            }

        }

        }
            catch{}
    }
    protected void btnCharterList_Click(object sender, EventArgs e)
    {
        Response.Redirect("CP_Charter_Party_List.aspx");
    }
    protected void btnRemarks_Click(object sender, EventArgs e)
    {
        LoadRemark();
    }
    protected void btnCharterHire_Click(object sender, EventArgs e)
    {
        if (Session["CPID"] != null)
        {
            iFrame1.Attributes["src"] = " CP_Hire_Invoice.aspx?CPID=" + Session["CPID"].ToString();
           
           
        }
    }
    protected void btnRemittance_Click(object sender, EventArgs e)
    {
        if (Session["CPID"] != null)
        {
            iFrame1.Attributes["src"] = "CP_Remittance_Receipt.aspx?CPID=" + Session["CPID"].ToString();
        }
    }
    protected void btnOwnerExpenses_Click(object sender, EventArgs e)
    {
        if (Session["CPID"] != null)
        {
            iFrame1.Attributes["src"] = " CP_Owners_Expenses.aspx?CPID=" + Session["CPID"].ToString();
        }
    }
    protected void btnIncomeMatching_Click(object sender, EventArgs e)
    {
        if (Session["CPID"] != null)
        {
            iFrame1.Attributes["src"] = "CP_Income_Matching.aspx?CPID=" + Session["CPID"].ToString();
        }
    }
    protected void btnDocuments_Click(object sender, EventArgs e)
    {
        if (Session["CPID"] != null)
        {
            iFrame1.Attributes["src"] = "CP_Attachments.aspx?CPID=" + Session["CPID"].ToString();
        }
    }

    protected void btnBillingItems_Click(object sender, EventArgs e)
    {
        if (Session["CPID"] != null)
        {
            iFrame1.Attributes["src"] = "CP_Billing_Item_Entry.aspx?CPID=" + Session["CPID"].ToString();
         
        }
    }

    protected void btnTradingRange_Click(object sender, EventArgs e)
    {
        if (Session["CPID"] != null)
        {
            iFrame1.Attributes["src"] = "CP_Trading_Range.aspx?CPID=" + Session["CPID"].ToString();
        }
    }

    protected void LoadRemark()
    {
        if (Session["CPID"] != null)
        {
            iFrame1.Attributes["src"] = "CP_Remarks.aspx?CPID=" + Session["CPID"].ToString();
        }
    }

    protected void btnAddRedeliveryBunker_Click(object sender, ImageClickEventArgs e)
    {
        ClearBunker();
        ViewState["OType"] = "R";
        ltPageHeader.Text = "Redelivery Bunker";
        string show = String.Format("showDivAddBunker('dvAddBunker')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showmodal", show, true);
    }
    protected void btnAddDeliveryBunker_Click(object sender, ImageClickEventArgs e)
    {
        ClearBunker();
        ViewState["OType"] = "D";
        ltPageHeader.Text = "Delivery Bunker";
        string show = String.Format("showDivAddBunker('dvAddBunker')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showmodal", show, true);
    }


    protected void onUpdateDelivery_Click(object source, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            ClearBunker();
            int Bunker_Id = UDFLib.ConvertToInteger(e.CommandArgument);
            ViewState["Bunker_Id"] = Bunker_Id.ToString();
            ltPageHeader.Text = "Delivery Bunker";
            ViewState["OType"] = "D";
            dt = oCP.Get_BunkerDetail(Bunker_Id);
            txtPricePerUnit.Text = dt.DefaultView[0]["Unit_Price"].ToString();
            txtUnit.Text = dt.DefaultView[0]["Fuel_Amt"].ToString();
            ddlFuelType.SelectedValue = dt.DefaultView[0]["Fuel_Type_Id"].ToString();
            string show = String.Format("showDivAddBunker('dvAddBunker')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showmodal", show, true);
        }
        catch { }

    }
    protected void onDeleteDelivery_Click(object source, CommandEventArgs e)
    {
        try
        {
            int Delivery_Bunker_ID = Convert.ToInt32(e.CommandArgument);
            DeleteBunker(Delivery_Bunker_ID);
        }
        catch { }


    }
    
    
    protected void onUpdateReDelivery_Click(object source, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            ClearBunker();
            int Bunker_Id = UDFLib.ConvertToInteger(e.CommandArgument);
            ViewState["Bunker_Id"] = Bunker_Id.ToString();
            ViewState["OType"] = "R";
            ltPageHeader.Text = "Redelivery Bunker";
            dt = oCP.Get_BunkerDetail(Bunker_Id);
            txtPricePerUnit.Text = dt.DefaultView[0]["Unit_Price"].ToString();
            txtUnit.Text = dt.DefaultView[0]["Fuel_Amt"].ToString();
            ddlFuelType.SelectedValue = dt.DefaultView[0]["Fuel_Type_Id"].ToString();
            string show = String.Format("showDivAddBunker('dvAddBunker')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showmodal", show, true);
        }
        catch { }

    }
    protected void onDeleteReDelivery_Click(object source, CommandEventArgs e)
    {

        try
        {
           int ReDelivery_Bunker_ID = Convert.ToInt32(e.CommandArgument);
           DeleteBunker(ReDelivery_Bunker_ID);
        }
        catch { }

    }


    private void DeleteBunker(int Bunker_Id)
    {
        oCP.Delete_BunkerDetail(Bunker_Id, GetSessionUserID());
        BindBunkerDetail();
        ReBindBunkerDetail();
    }



    protected void btnSavebunker_Click(object sender, EventArgs e)
    {
        SaveBunkerData();

    }
    protected void SaveBunkerData()
    {
        ltmessage.Text = "";
        int res = -1;
        if (ViewState["Bunker_Id"] != null)

            res = oCP.UPD_BunkerDetail(UDFLib.ConvertIntegerToNull(ViewState["Bunker_Id"]), UDFLib.ConvertIntegerToNull(Session["CPID"]), UDFLib.ConvertIntegerToNull(ddlFuelType.SelectedValue),
                ddlFuelType.SelectedItem.Text, ViewState["OType"].ToString(), UDFLib.ConvertToDouble(txtUnit.Text), UDFLib.ConvertToDouble(txtPricePerUnit.Text), GetSessionUserID());
        else

            res = oCP.INS_BunkerDetail(UDFLib.ConvertIntegerToNull(Session["CPID"]),
                UDFLib.ConvertIntegerToNull(ddlFuelType.SelectedValue), ddlFuelType.SelectedItem.Text, ViewState["OType"].ToString(), UDFLib.ConvertToDouble(txtUnit.Text), UDFLib.ConvertToDouble(txtPricePerUnit.Text), GetSessionUserID());
        ViewState["Bunker_Id"] = null;
        if (res == 0)
        {
            ltmessage.Text = "Record already exist for same type ! Please check.";
            string show = String.Format("showDivAddBunker('dvAddBunker')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showmodal", show, true);
        }
        else
        {
            ClearBunker();
            BindBunkerDetail();
            ReBindBunkerDetail();
            string hidemodal = String.Format("hideModal('dvAddBunker')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
    }

    private void ClearBunker()
    {
        txtPricePerUnit.Text = "0.00";
        txtUnit.Text = "0.00";
        ddlFuelType.SelectedIndex = -1;
        ltmessage.Text = "";
    }

}