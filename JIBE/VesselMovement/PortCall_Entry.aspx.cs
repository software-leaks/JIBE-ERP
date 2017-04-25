using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using SMS.Business.CP;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class VesselMovement_PortCall_Entry : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    public string DateFormat = "";

    UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        //This change has been done to change the date format as per user selection

        caltxtDepartureDate.Format = Convert.ToString(Session["User_DateFormat"]);
        caltxtArrival.Format = Convert.ToString(Session["User_DateFormat"]);
        caltxtBerthingDate.Format = Convert.ToString(Session["User_DateFormat"]);
        DateFormat = UDFLib.GetDateFormat();//Get User date format
        

        if (!IsPostBack)
        {
            Load_VesselList();
            Load_PortList();
            Load_AgentList();
            ViewState["ReturnPortCallID"] = 0;
            if (!String.IsNullOrEmpty(Request.QueryString["ID"].ToString()))
            {

                ddlVessel.SelectedValue = Request.QueryString["ID"].ToString();
                ddlVessel.Enabled = false;
            }
            if (!String.IsNullOrEmpty(Request.QueryString["StatusID"].ToString()))
            {

                FillPortCall();
            }
            Load_PortCalls();
        }

    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        if ( CurrentUserID == 0)
            Response.Redirect("~/default.aspx?msgid=1");

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public void Load_AgentList()
    {

        DataTable dtcharterer = objPortCall.GetAgent_List();
        ddlCharterAgent.DataSource = dtcharterer;
        ddlCharterAgent.DataTextField = "Supplier_Name";
        ddlCharterAgent.DataValueField = "SUPPLIER_CODE";
        ddlCharterAgent.DataBind();
        ddlCharterAgent.Items.Insert(0, new ListItem("-Select-", "0"));

        DataTable dtOwnerAgent = objPortCall.GetAgent_List();
        ddlOwnersAgent.DataSource = dtOwnerAgent;
        ddlOwnersAgent.DataTextField = "Supplier_Name";
        ddlOwnersAgent.DataValueField = "SUPPLIER_CODE";
        ddlOwnersAgent.DataBind();
        ddlOwnersAgent.Items.Insert(0, new ListItem("-Select-", "0"));

    }
    public void FillPortCall()
    {
        int Port_Call_Id = UDFLib.ConvertToInteger(Request.QueryString["StatusID"].ToString());
        DataTable dt = objPortCall.Get_PortCall_List(Port_Call_Id, UDFLib.ConvertToInteger(ddlVessel.SelectedValue));

        //if(Port_Call_Id == 0)
        //    chkAutoDate.Enabled=false;
        if (dt.Rows.Count > 0)
        {
            tr1.Visible = true;
            ViewState["ReturnPortCallID"] = Request.QueryString["StatusID"].ToString();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string ArrivalDate, BerthingDate, DepartureDate;
            string[] ADate, BDate, DDate;
            string Arrhr, Arrmin, Brhr,BrMin, DHr,DMin;

            if (dt.Rows[0]["Arrival"].ToString() != "")
            {
                ArrivalDate = dt.Rows[0]["Arrival"].ToString();
                DateTime dtArr =   Convert.ToDateTime(dt.Rows[0]["Arrival"].ToString());
                ADate = ArrivalDate.Split(delimiterChars);
                //This change has been done to change the date format as per user selection
                dtpArrival.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dtArr));
                Arrhr = ADate[1].ToString();
                Arrmin = ADate[2].ToString();
                ddlArrHour.SelectedValue = Arrhr.ToString();
                ddlArrMin.SelectedValue = Arrmin.ToString();
            }
            else
            {
                if (ViewState["dtpArrival"] != null)
                {
                    //This change has been done to change the date format as per user selection
                    dtpArrival.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(ViewState["dtpArrival"]));
                    
                    ddlArrHour.SelectedValue = ViewState["ddlArrHour"].ToString();
                    ddlArrMin.SelectedValue = ViewState["ddlArrMin"].ToString();
                }

            }
            if (dt.Rows[0]["Berthing"].ToString() != "")
            {
                BerthingDate = dt.Rows[0]["Berthing"].ToString();
                DateTime dtBer = Convert.ToDateTime(dt.Rows[0]["Berthing"].ToString());
                BDate = BerthingDate.Split(delimiterChars);
                //This change has been done to change the date format as per user selection
                dtpBerthing.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dtBer));
                
                 Brhr = BDate[1].ToString();
                 BrMin = BDate[2].ToString();
                 ddlBerthingHour.SelectedValue = Brhr.ToString();
                 ddlBerthingMin.SelectedValue = BrMin.ToString();
                 
            }
            if (dt.Rows[0]["Departure"].ToString() != "")
            {
                DepartureDate = dt.Rows[0]["Departure"].ToString();
                DateTime dtDep = Convert.ToDateTime(dt.Rows[0]["Departure"].ToString());
                DDate = DepartureDate.Split(delimiterChars);
                //This change has been done to change the date format as per user selection
                dtpDeparture.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dtDep));
                DHr = DDate[1].ToString();
                DMin = DDate[2].ToString();
                ddlDepHr.SelectedValue = DHr.ToString();
                ddlDepmin.SelectedValue = DMin.ToString();
            }
            else 
            {
                if (ViewState["dtpDeparture"] != null)
                {
                    //This change has been done to change the date format as per user selection
                    dtpDeparture.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(ViewState["dtpDeparture"]));
                    
                    ddlDepHr.SelectedValue = ViewState["ddlDepHr"].ToString();
                    ddlDepmin.SelectedValue = ViewState["ddlDepmin"].ToString();
                }

            }
            DDLPort.SelectedValue = dt.Rows[0]["Port_ID"].ToString();
            if (dt.Rows[0]["Port_ID"].ToString() == "0")
            {
                txtlocation.Text = dt.Rows[0]["Port_Name"].ToString();
                txtlocation.Visible = true;
                chkNewLocation.Checked = true;
                DDLPort.Visible = false;
            }
            else
            {
                txtlocation.Visible = false;
                DDLPort.Visible = true;
                //DDLPort.Enabled = false;
            }

            //chkNewLocation.Visible = false;
            ddlCharterAgent.SelectedValue = dt.Rows[0]["Charterers_Agent"].ToString();
            ddlOwnersAgent.SelectedValue = dt.Rows[0]["Owners_ID"].ToString();
            int warrisk = Convert.ToInt16(dt.Rows[0]["IsWarRisk"].ToString());
            int ShipCrane = Convert.ToInt16(dt.Rows[0]["IsShipCraneReq1"].ToString());
            if (warrisk == 1)
            {
                chkWarRisk.Checked = true;
            }
            if (ShipCrane == 1)
            {
                chkShipCrane.Checked = true;
            }
            string Port_Call_Status = Convert.ToString(dt.Rows[0]["Port_Call_Status"].ToString());
            if (Port_Call_Status == "OMITTED")
            {
                chkPortCallStatus.Checked = true;
            }
            txtPortRemark.Text = dt.Rows[0]["Port_Remarks"].ToString();


            if (dt.Rows[0]["Auto_Date"].ToString() == "Y")
            {
                chkAutoDate.Checked = true;
                dtpArrival.Enabled = false;
                dtpBerthing.Enabled = false;
                dtpDeparture.Enabled = false;
                ddlArrHour.Enabled = false;
                ddlArrMin.Enabled = false;
                ddlBerthingHour.Enabled = false;
                ddlBerthingMin.Enabled = false;
                ddlDepHr.Enabled = false;
                ddlDepmin.Enabled = false;
            }
            else
            {
                chkAutoDate.Checked = false;
                dtpArrival.Enabled = true;
                dtpBerthing.Enabled = true;
                dtpDeparture.Enabled = true;
                ddlArrHour.Enabled = true;
                ddlArrMin.Enabled = true;
                ddlBerthingHour.Enabled = true;
                ddlBerthingMin.Enabled = true;
                ddlDepHr.Enabled = true;
                ddlDepmin.Enabled = true;
            }
        }
    }
    public void Load_PortCalls()
    {
        DataTable dt = objPortCall.Get_PortCalls(UDFLib.ConvertToInteger(ViewState["ReturnPortCallID"]), UDFLib.ConvertToInteger(ddlVessel.SelectedValue));

        if (dt.Rows.Count > 0)
        {
            dhDepDate.Value = dt.Rows[0]["Departure"].ToString();
            hdPortID.Value = dt.Rows[0]["Port_ID"].ToString();
           
        }
        else
        {

            dhDepDate.Value = "1900-01-01 02:00:00.000";
        }
      
    }
    public void Load_VesselList()
    {
        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();

        
       
    }
    public void Load_PortList()
    {

        DataTable dt = objBLLPort.Get_PortList_Mini();

        DDLPort.DataSource = dt;
        DDLPort.DataTextField = "Port_Name";
        DDLPort.DataValueField = "Port_ID";
        DDLPort.DataBind();
        DDLPort.Items.Insert(0, new ListItem("-Select-", "0"));

    }
    /// <summary>
    /// Method to add or modify the port call setails
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string ArrivalTime = ddlArrHour.SelectedValue + ":" + ddlArrMin.SelectedValue;
            string BerthingTime = ddlBerthingHour.Text + ":" + ddlBerthingMin.SelectedValue;
            string DepartureTime = ddlDepHr.Text + ":" + ddlDepmin.SelectedValue;
            bool bAutoDate = chkAutoDate.Checked;
            string PortCallStatus = "";
            DateTime Berthingdt;
            int str = 0;
            int PortId = 0;
            string PortLocation = string.Empty;
            //Done changes for the Date format issue on save
            var Arrival="";
            var Dep = "";
            var Berthing = "";
            if (dtpArrival.Text != "")
            {
                
                Arrival = UDFLib.ConvertToDefaultDt(dtpArrival.Text);
            }
            if (dtpDeparture.Text != "")
            {
               
               Dep = UDFLib.ConvertToDefaultDt(dtpDeparture.Text);
            }
            if (dtpBerthing.Text != "")
            {
                
                Berthing = UDFLib.ConvertToDefaultDt(dtpBerthing.Text);
            }


            if (dtpBerthing.Text == "")
            {
                Berthingdt = Convert.ToDateTime(Arrival + " " + ArrivalTime);
            }
            else
            {
                Berthingdt = Convert.ToDateTime(Berthing + " " + BerthingTime);
            }
            if (chkNewLocation.Checked == false)
            {
                PortId = UDFLib.ConvertToInteger(DDLPort.SelectedValue);
            }
            else
            {
                PortLocation = txtlocation.Text;
            }


            if (dtpArrival.Text.Trim() != "" && dtpDeparture.Text.Trim() != "")
            {
                DateTime Arrivaldt = Convert.ToDateTime(Arrival + " " + ArrivalTime);
                DateTime Depdt = Convert.ToDateTime(Dep + " " + DepartureTime);

                ViewState["dtpArrival"] = Arrival;
                ViewState["dtpDeparture"] = Dep;
                ViewState["ddlArrHour"] = ddlArrHour.SelectedValue;
                ViewState["ddlArrMin"] = ddlArrMin.SelectedValue;
                ViewState["ddlDepHr"] = ddlDepHr.SelectedValue;
                ViewState["ddlDepmin"] = ddlDepmin.SelectedValue;


                if (Arrivaldt > Berthingdt)
                {
                    lbl1.Text = "Berthing Date can't be before of Arrival Date.";
                }
                else if (Arrivaldt > Depdt)
                {
                    lbl1.Text = "Departure Date can't be before of Arrival Date.";
                }
                else if (Berthingdt > Depdt)
                {
                    lbl1.Text = "Departure Date can't be before of Berthing Date.";
                }
                else
                {
                    if (UDFLib.ConvertToInteger(ViewState["ReturnPortCallID"]) == 0)
                    {
                        str = objPortCall.Ins_PortCall_Details(UDFLib.ConvertToInteger(ViewState["ReturnPortCallID"]), UDFLib.ConvertToInteger(ddlVessel.SelectedValue), PortId
                              , UDFLib.ConvertDateToNull(Arrival)
                              , UDFLib.ConvertStringToNull(Berthing)
                              , UDFLib.ConvertDateToNull(Dep), UDFLib.ConvertStringToNull(ArrivalTime),
                                UDFLib.ConvertStringToNull(BerthingTime), UDFLib.ConvertStringToNull(DepartureTime)
                              , txtPortRemark.Text, UDFLib.ConvertStringToNull(ddlOwnersAgent.SelectedValue), UDFLib.ConvertStringToNull(ddlCharterAgent.SelectedValue)
                              , Convert.ToInt32(GetSessionUserID()), Convert.ToInt32(chkWarRisk.Checked), Convert.ToInt32(chkShipCrane.Checked), PortCallStatus, PortLocation, bAutoDate);
                        //str = objPortCall.Ins_PortCall_Details(UDFLib.ConvertIntegerToNull(str));

                    }
                    else
                    {
                        if (chkPortCallStatus.Checked == true)
                        {
                            PortCallStatus = "OMITTED";
                        }
                        str = objPortCall.Upd_PortCall_Details(UDFLib.ConvertToInteger(ViewState["ReturnPortCallID"]), UDFLib.ConvertToInteger(ddlVessel.SelectedValue), PortId
                            , UDFLib.ConvertDateToNull(Arrival)
                            , UDFLib.ConvertStringToNull(Berthing)
                            , UDFLib.ConvertDateToNull(Dep), UDFLib.ConvertStringToNull(ArrivalTime),
                              UDFLib.ConvertStringToNull(BerthingTime), UDFLib.ConvertStringToNull(DepartureTime)
                            , txtPortRemark.Text, UDFLib.ConvertStringToNull(ddlOwnersAgent.SelectedValue), UDFLib.ConvertStringToNull(ddlCharterAgent.SelectedValue)
                            , Convert.ToInt32(GetSessionUserID()), Convert.ToInt32(chkWarRisk.Checked), Convert.ToInt32(chkShipCrane.Checked), PortCallStatus, PortLocation, bAutoDate);


                    }
                    if (str == -1)
                    {
                        lbl1.Text = "Input Date should not come in between existing arrival/departure Date .";
                    }
                    else if (str == -2)
                    {
                        lbl1.Text = "Input Date should not come in between existing arrival/departure Date .";
                    }
                    else if (str == -3)
                    {
                        lbl1.Text = "Input Date time should not be less than previous port call time.";
                    }
                    else
                    {
                        //str = objPortCall.Ins_PortCall_Details(UDFLib.ConvertIntegerToNull(str));
                        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
                    }
                }
            }
            else
            {
                if (UDFLib.ConvertToInteger(ViewState["ReturnPortCallID"]) == 0)
                {
                    if (dtpArrival.Text != "" && dtpBerthing.Text != "")
                    {
                        if (Convert.ToDateTime(Berthing) < Convert.ToDateTime(Arrival))
                            lbl1.Text = "Berthing Date can't be before of Arrival Date.";
                        else
                        {
                            str = objPortCall.Ins_PortCall_Details(UDFLib.ConvertToInteger(ViewState["ReturnPortCallID"]), UDFLib.ConvertToInteger(ddlVessel.SelectedValue), PortId
                              , UDFLib.ConvertDateToNull(Arrival)
                              , UDFLib.ConvertStringToNull(Berthing)
                              , UDFLib.ConvertDateToNull(Dep), UDFLib.ConvertStringToNull(ArrivalTime),
                                UDFLib.ConvertStringToNull(BerthingTime), UDFLib.ConvertStringToNull(DepartureTime)
                              , txtPortRemark.Text, UDFLib.ConvertStringToNull(ddlOwnersAgent.SelectedValue), UDFLib.ConvertStringToNull(ddlCharterAgent.SelectedValue)
                              , Convert.ToInt32(GetSessionUserID()), Convert.ToInt32(chkWarRisk.Checked), Convert.ToInt32(chkShipCrane.Checked), PortCallStatus, PortLocation, bAutoDate);
                            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
                        }
                    }
                    else if (dtpBerthing.Text != "" && dtpDeparture.Text != "")
                    {
                        if (Convert.ToDateTime(dtpDeparture.Text) < Convert.ToDateTime(dtpBerthing.Text))
                            lbl1.Text = "Departure Date can't be before of Berthing Date.";
                        else
                        {
                            str = objPortCall.Ins_PortCall_Details(UDFLib.ConvertToInteger(ViewState["ReturnPortCallID"]), UDFLib.ConvertToInteger(ddlVessel.SelectedValue), PortId
                              , UDFLib.ConvertDateToNull(Arrival)
                              , UDFLib.ConvertStringToNull(Berthing)
                              , UDFLib.ConvertDateToNull(Dep), UDFLib.ConvertStringToNull(ArrivalTime),
                                UDFLib.ConvertStringToNull(BerthingTime), UDFLib.ConvertStringToNull(DepartureTime)
                              , txtPortRemark.Text, UDFLib.ConvertStringToNull(ddlOwnersAgent.SelectedValue), UDFLib.ConvertStringToNull(ddlCharterAgent.SelectedValue)
                              , Convert.ToInt32(GetSessionUserID()), Convert.ToInt32(chkWarRisk.Checked), Convert.ToInt32(chkShipCrane.Checked), PortCallStatus, PortLocation, bAutoDate);

                            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
                        }
                    }
                    else
                    {
                        str = objPortCall.Ins_PortCall_Details(UDFLib.ConvertToInteger(ViewState["ReturnPortCallID"]), UDFLib.ConvertToInteger(ddlVessel.SelectedValue), PortId
                              , UDFLib.ConvertDateToNull(Arrival)
                              , UDFLib.ConvertStringToNull(Berthing)
                              , UDFLib.ConvertDateToNull(Dep), UDFLib.ConvertStringToNull(ArrivalTime),
                                UDFLib.ConvertStringToNull(BerthingTime), UDFLib.ConvertStringToNull(DepartureTime)
                              , txtPortRemark.Text, UDFLib.ConvertStringToNull(ddlOwnersAgent.SelectedValue), UDFLib.ConvertStringToNull(ddlCharterAgent.SelectedValue)
                              , Convert.ToInt32(GetSessionUserID()), Convert.ToInt32(chkWarRisk.Checked), Convert.ToInt32(chkShipCrane.Checked), PortCallStatus, PortLocation, bAutoDate);
                        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);

                    }
                }
                else
                {
                    if (chkPortCallStatus.Checked == true)
                    {
                        PortCallStatus = "OMITTED";
                    }


                    if (dtpArrival.Text != "" && dtpBerthing.Text != "")
                    {
                        if (Convert.ToDateTime(dtpBerthing.Text) < Convert.ToDateTime(dtpArrival.Text))
                            lbl1.Text = "Berthing Date can't be before of Arrival Date.";
                        else
                        {
                            str = objPortCall.Upd_PortCall_Details(UDFLib.ConvertToInteger(ViewState["ReturnPortCallID"]), UDFLib.ConvertToInteger(ddlVessel.SelectedValue), PortId
                       , UDFLib.ConvertDateToNull(Arrival)
                       , UDFLib.ConvertStringToNull(Berthing)
                       , UDFLib.ConvertDateToNull(Dep), UDFLib.ConvertStringToNull(ArrivalTime),
                         UDFLib.ConvertStringToNull(BerthingTime), UDFLib.ConvertStringToNull(DepartureTime)
                       , txtPortRemark.Text, UDFLib.ConvertStringToNull(ddlOwnersAgent.SelectedValue), UDFLib.ConvertStringToNull(ddlCharterAgent.SelectedValue)
                       , Convert.ToInt32(GetSessionUserID()), Convert.ToInt32(chkWarRisk.Checked), Convert.ToInt32(chkShipCrane.Checked), PortCallStatus, PortLocation, bAutoDate);

                            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
                        }
                    }
                    else if (dtpBerthing.Text != "" && dtpDeparture.Text != "")
                    {
                        if (Convert.ToDateTime(dtpDeparture.Text) < Convert.ToDateTime(dtpBerthing.Text))
                            lbl1.Text = "Departure Date can't be before of Berthing Date.";
                        else
                        {
                            str = objPortCall.Upd_PortCall_Details(UDFLib.ConvertToInteger(ViewState["ReturnPortCallID"]), UDFLib.ConvertToInteger(ddlVessel.SelectedValue), PortId
                      , UDFLib.ConvertDateToNull(Arrival)
                      , UDFLib.ConvertStringToNull(Berthing)
                      , UDFLib.ConvertDateToNull(Dep), UDFLib.ConvertStringToNull(ArrivalTime),
                        UDFLib.ConvertStringToNull(BerthingTime), UDFLib.ConvertStringToNull(DepartureTime)
                      , txtPortRemark.Text, UDFLib.ConvertStringToNull(ddlOwnersAgent.SelectedValue), UDFLib.ConvertStringToNull(ddlCharterAgent.SelectedValue)
                      , Convert.ToInt32(GetSessionUserID()), Convert.ToInt32(chkWarRisk.Checked), Convert.ToInt32(chkShipCrane.Checked), PortCallStatus, PortLocation, bAutoDate);
                            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
                        }
                    }
                    else
                    {
                        str = objPortCall.Upd_PortCall_Details(UDFLib.ConvertToInteger(ViewState["ReturnPortCallID"]), UDFLib.ConvertToInteger(ddlVessel.SelectedValue), PortId
                            , UDFLib.ConvertDateToNull(Arrival)
                            , UDFLib.ConvertStringToNull(Berthing)
                            , UDFLib.ConvertDateToNull(Dep), UDFLib.ConvertStringToNull(ArrivalTime),
                              UDFLib.ConvertStringToNull(BerthingTime), UDFLib.ConvertStringToNull(DepartureTime)
                            , txtPortRemark.Text, UDFLib.ConvertStringToNull(ddlOwnersAgent.SelectedValue), UDFLib.ConvertStringToNull(ddlCharterAgent.SelectedValue)
                            , Convert.ToInt32(GetSessionUserID()), Convert.ToInt32(chkWarRisk.Checked), Convert.ToInt32(chkShipCrane.Checked), PortCallStatus, PortLocation, bAutoDate);
                        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);

                    }
                }
                //str = objPortCall.Ins_PortCall_Details(UDFLib.ConvertIntegerToNull(str));

            }
        }
        catch(Exception ex)
        {
            string Error = ex.ToString(); 
        }

    }

    protected void chkNewLocation_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNewLocation.Checked == true)
        {
            DDLPort.Visible = false;
            txtlocation.Visible = true;
        }
        else
        {
            DDLPort.Visible = true;
            txtlocation.Visible = false;
        }
    }
    protected void chkAutoDate_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAutoDate.Checked == true)
        {
            dtpArrival.Enabled = false;
            dtpBerthing.Enabled = false;
            dtpDeparture.Enabled = false;
            ddlArrHour.Enabled = false;
            ddlArrMin.Enabled = false;
            ddlBerthingHour.Enabled = false;
            ddlBerthingMin.Enabled = false;
            ddlDepHr.Enabled = false;
            ddlDepmin.Enabled = false;
            dtpArrival.Text = "";
            dtpBerthing.Text = "";
            dtpDeparture.Text = "";
            ddlArrHour.SelectedIndex = 0;
            ddlArrMin.SelectedIndex = 0;
            ddlBerthingHour.SelectedIndex = 0;
            ddlBerthingMin.SelectedIndex = 0;
            ddlDepHr.SelectedIndex = 0;
            ddlDepmin.SelectedIndex = 0;
        }
        else
        {
            dtpArrival.Enabled = true;
            dtpBerthing.Enabled = true;
            dtpDeparture.Enabled = true;
            ddlArrHour.Enabled = true;
            ddlArrMin.Enabled = true;
            ddlBerthingHour.Enabled = true;
            ddlBerthingMin.Enabled = true;
            ddlDepHr.Enabled = true;
            ddlDepmin.Enabled = true;
        }
    }
}