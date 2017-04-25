using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class VesselMovement_PortCallNotification_Entry : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    private long lCurrentRecord = 0;
    private long lRecordsPerRow = 200;
    private int NotificationId = 0;
    bool IsAllPorts = false;
    bool IsallVessels = false;
    bool IsAllUsers = false;
    bool IsAllCountries = false;
    public string DateFormat = "";
    public UserAccess objUA = new UserAccess();
    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();
    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();
        //This change has been done to change the date format as per user selection
        cedtArrivalFrom.Format = Convert.ToString(Session["User_DateFormat"]);
        cedtArrivalTo.Format = Convert.ToString(Session["User_DateFormat"]);
        DateFormat = UDFLib.GetDateFormat();//Get User date format

        if (!IsPostBack)
        {

            if(!String.IsNullOrEmpty(Request.QueryString["ID"].ToString()))
                ViewState["NotificationId"] = Convert.ToInt32(Request.QueryString["ID"]);
                    
            BindNotificationDeatil();

        }
    }

    


    protected void BindNotificationDeatil()
    {
        try
        {
            NotificationId = Convert.ToInt32(ViewState["NotificationId"]);
            if (NotificationId != 0)
            {
                DataTable dt = objPortCall.Get_PortCall_Notification_detail(NotificationId);
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["Notification_Name"].ToString();
                    txtDescription.Text = dt.Rows[0]["Notification_Description"].ToString();
                    string Status = dt.Rows[0]["Notification_Status"].ToString();
                    if(ddlStatus.Items.FindByText(Status) != null)
                        ddlStatus.SelectedValue = ddlStatus.Items.FindByText(Status).Value;
                    IsAllPorts = Convert.ToBoolean(dt.Rows[0]["IsAllPorts"]);
                    IsallVessels = Convert.ToBoolean(dt.Rows[0]["IsAllVessels"]);
                    IsAllUsers = Convert.ToBoolean(dt.Rows[0]["IsAllUsers"]);
                    IsAllCountries = Convert.ToBoolean(dt.Rows[0]["IsAllCountries"]);
                    //This change has been done to change the date format as per user selection

                    //if (dt.Rows[0]["Start_Date"].ToString() != "")
                    //{
                        dtArrivalFrom.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Start_Date"]));
                       
                    //}
                    //This change has been done to change the date format as per user selection
                    //if (dt.Rows[0]["End_Date"].ToString() != "")
                    //{
                        dtArrivalTo.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["End_Date"]));
                        
                    //}

                    if (IsAllCountries)
                    {
                        chkCountryAll.Checked = true;
                        chkCountry.Visible = false;
                        btnCountryAdd.Visible = false;
                        ddlCountry.Visible = false;
                        dvCountry.Visible = false;
                    }

                    if (IsAllPorts)
                    {
                        chkPortAll.Checked = true;
                        chkPort.Visible = false;
                        btnPortAdd.Visible = false;
                        ddlPort.Visible = false;
                        dvPortList.Visible = false;
                    }
                    if (IsallVessels)
                    {
                        chkVesselAll.Checked = true;
                        chkVessel.Visible = false;
                        btnVesselAdd.Visible = false;
                        ddlVessel.Visible = false;
                        dvVesselList.Visible = false;
                    }
                    if (IsAllUsers)
                    {
                        chkUserAll.Checked = true;
                        chkUser.Visible = false;
                        btnUserAdd.Visible = false;
                        ddlUser.Visible = false;
                        dvUserList.Visible = false;
                    }
                }
            }
            else
            {
                cedtArrivalFrom.SelectedDate = DateTime.Now;
                
                cedtArrivalTo.SelectedDate = DateTime.Now;
            }
            BindPort();
            BindCountries();
            BindVessels();
            BindUsers();
           //ClearControls();
        }
        catch { }
        {
        }
    }

    protected void ClearControls()
    {
        txtDescription.Text = "";
        txtName.Text = "";     
        DataTable dtPort = (DataTable)Session["dtPort"];
        dtPort.Clear();
        chkPort.DataSource = dtPort;
        chkPort.DataBind();
    }

    
   
    protected void BindPort()
    {
        this.InitialPortBind();
        DataTable dt1 = objPortCall.Get_PortCall_Notification_detail_Ports(NotificationId);//Load by notificationId
        Session["dtPort"] = dt1;
        chkPort.DataSource = dt1;
        chkPort.DataTextField = "PORT_NAME";
        chkPort.DataValueField = "PORT_ID";
        chkPort.DataBind();
        foreach (ListItem chkitem in chkPort.Items)
        {
            chkitem.Selected = true;
            if (ddlPort.Items.FindByValue(chkitem.Value) != null)
            {
                ListItem itemToRemove = ddlPort.Items.FindByValue(chkitem.Value);
                ddlPort.Items.Remove(itemToRemove);
            }
        }
    }

    protected void BindCountries()
    {
        this.InitialCountryBind();
        DataTable dt1 = objPortCall.Get_PortCall_Notification_detail_Countries(NotificationId);//Load by notificationId
        Session["dtCountry"] = dt1;
        chkCountry.DataSource = dt1;
        chkCountry.DataTextField = "Country_Name";
        chkCountry.DataValueField = "CountryId";
        chkCountry.DataBind();
        //Session["dtCountry"] = dt1;
        foreach (ListItem chkitem in chkCountry.Items)
        {
            chkitem.Selected = true;
            if (ddlCountry.Items.FindByValue(chkitem.Value) != null)
            {

                    ListItem itemToRemove = ddlCountry.Items.FindByValue(chkitem.Value);
                    ddlCountry.Items.Remove(itemToRemove);

               
            }
        }
    }

    protected void BindVessels()
    {
        this.InitialVesselBind();
        DataTable dtVessel = objPortCall.Get_PortCall_Notification_detail_Vessels(NotificationId);//Load by notificationId
        Session["dtVessel"] = dtVessel;
        chkVessel.DataSource = dtVessel;
        chkVessel.DataTextField = "Vessel_Name";
        chkVessel.DataValueField = "VesselId";
        chkVessel.DataBind();
        Session["dtVessels"] = dtVessel;
        foreach (ListItem chkitem in chkVessel.Items)
        {
            chkitem.Selected = true;
            if (ddlVessel.Items.FindByValue(chkitem.Value) != null)
            {
                ListItem itemToRemove = ddlVessel.Items.FindByValue(chkitem.Value);
                ddlVessel.Items.Remove(itemToRemove);

            }
        }
    }

    protected void BindUsers()
    {
        this.InitialUserBind();
        DataTable dtuser = objPortCall.Get_PortCall_Notification_detail_Users(NotificationId);//Load by notificationId
        Session["dtusers"] = dtuser;
        chkUser.DataSource = dtuser;
        chkUser.DataTextField = "User_name";
        chkUser.DataValueField = "UserId";
        chkUser.DataBind();
        Session["dtusers"] = dtuser;
        foreach (ListItem chkitem in chkUser.Items)
        {
            chkitem.Selected = true;
            if (ddlUser.Items.FindByValue(chkitem.Value) != null)
            {
                ListItem itemToRemove = ddlUser.Items.FindByValue(chkitem.Value);
                ddlUser.Items.Remove(itemToRemove);
            }
        }
    }


    protected void btnPortAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtPort"];
        DataRow dr = dt.NewRow();


        dr["PORT_ID"] = ddlPort.SelectedValue;
        dr["PORT_NAME"] = ddlPort.SelectedItem.Text;
        dt.Rows.Add(dr);

        ddlPort.Items.RemoveAt(ddlPort.SelectedIndex);

        Session["dtPort"] = dt;
        List<string> li = new List<string>();
        for (int i = 0; i < chkPort.Items.Count; i++)
        {
            if (chkPort.Items[i].Selected == false)
            {
                var rows = dt.Select("Port_Name='" + chkPort.Items[i] + "'");
                foreach (var row in rows)
                    row.Delete();
                li.Add(chkPort.Items[i].Value);
            }
        }
        InitialPortBind();
        
        for (int i = 0; i < li.ToArray().Length; i++)
        {
            ddlPort.Items.Remove(li[i]);
        }
        chkPort.DataSource = dt;
        chkPort.DataValueField = "PORT_ID";
        chkPort.DataTextField = "PORT_NAME";
        chkPort.DataBind();
        //chk1.SelectedItem.Selected = true;

        if (chkPort.Items.Count > 0)
        {
            foreach (ListItem chkitem in chkPort.Items)
            {
                chkitem.Selected = true;
            }

        }
        for (int i = 0; i < chkPort.Items.Count; i++)
        {
            ListItem removeItem = chkPort.Items.FindByValue(chkPort.Items[i].Value);
            ddlPort.Items.Remove(removeItem);
        }

    }
    protected void btnPortRemove_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtPort"];
        BindPort();

        dt.Clear();
        Session["dtPort"] = dt;
        chkPort.DataSource = dt;

        chkPort.DataBind();
    }
    protected void btnVesselAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtVessel"];
        DataRow dr = dt.NewRow();


        dr["VesselId"] = ddlVessel.SelectedValue;
        dr["Vessel_Name"] = ddlVessel.SelectedItem.Text;
        dt.Rows.Add(dr);

        ddlVessel.Items.RemoveAt(ddlVessel.SelectedIndex);

        Session["dtVessel"] = dt;
        List<string> li = new List<string>();
        for (int i = 0; i < chkVessel.Items.Count; i++)
        {
            if (chkVessel.Items[i].Selected == false)
            {
                var rows = dt.Select("Vessel_Name='" + chkVessel.Items[i] + "'");
                foreach (var row in rows)
                    row.Delete();
                li.Add(chkVessel.Items[i].Value);
            }
        }
        InitialVesselBind();
        for (int i = 0; i < li.ToArray().Length; i++)
        {
            ddlVessel.Items.Remove(li[i]);
        }
        chkVessel.DataSource = dt;
        chkVessel.DataValueField = "VesselId";
        chkVessel.DataTextField = "Vessel_Name";
        chkVessel.DataBind();
        //chk1.SelectedItem.Selected = true;

        if (chkVessel.Items.Count > 0)
        {
            foreach (ListItem chkitem in chkVessel.Items)
            {
                chkitem.Selected = true;
            }

        }
        for (int i = 0; i < chkVessel.Items.Count; i++)
        {
            ListItem removeItem = chkVessel.Items.FindByValue(chkVessel.Items[i].Value);
            ddlVessel.Items.Remove(removeItem);
        }
    }

    protected void InitialUserBind()
    {
        BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        DataTable dt = objUserBLL.Get_UserList(UserCompanyID, "");
        ddlUser.DataSource = dt;
        ddlUser.DataTextField = "USER_NAME";
        ddlUser.DataValueField = "USERID";
        ddlUser.DataBind();
        ddlUser.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void InitialVesselBind()
    {
        BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        if (Session["sFleet"] == null)
        {
            DataTable FleetDT = objBLL.GetFleetList(UserCompanyID);
            FleetDT.Columns.RemoveAt(1);
            FleetDT.Columns.RemoveAt(1);
            FleetDT.Columns.RemoveAt(1);
            FleetDT.Columns.RemoveAt(1);
            FleetDT.Columns.RemoveAt(1);
            FleetDT.Columns.RemoveAt(1);
            FleetDT.Columns.RemoveAt(1);
            Session["sFleet"] = FleetDT;
        }
        DataTable dt = objPortCall.Get_PortCall_VesselList((DataTable)Session["sFleet"], 0, 0, "", UserCompanyID);

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void InitialPortBind()
    {
        BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
        DataTable dt = objBLLPort.Get_PortList_Mini();
        ddlPort.DataSource = dt;
        ddlPort.DataTextField = "PORT_NAME";
        ddlPort.DataValueField = "PORT_ID";
        ddlPort.DataBind();
        ddlPort.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void InitialCountryBind()
    {

        BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
        DataTable dt = objBLLCountry.Get_CountryList();
        ddlCountry.DataSource = dt;
        ddlCountry.DataTextField = "COUNTRY";
        ddlCountry.DataValueField = "ID";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void btnVesselRemove_Click(object sender, EventArgs e)
    {

        DataTable dt = (DataTable)Session["dtVessel"];
        BindVessels();

        dt.Clear();
        Session["dt"] = dt;
        chkVessel.DataSource = dt;

        chkVessel.DataBind();


    }
    protected void btnCountryAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtCountry"];
        DataRow dr = dt.NewRow();


        dr["CountryId"] = ddlCountry.SelectedValue;
        dr["Country_Name"] = ddlCountry.SelectedItem.Text;
        dt.Rows.Add(dr);

        ddlCountry.Items.RemoveAt(ddlCountry.SelectedIndex);

        Session["dtCountry"] = dt;

        List<string> li = new List<string>();
        for (int i = 0; i < chkCountry.Items.Count; i++)
        {
            if (chkCountry.Items[i].Selected == false)
            {
                var rows = dt.Select("Country_Name='" + chkCountry.Items[i] + "'");
                foreach (var row in rows)
                {
                    row.Delete();
                }
                li.Add(chkCountry.Items[i].Value);
               
            }
        }
        InitialCountryBind();
        for (int i = 0; i < li.ToArray().Length; i++)
        {
            ddlCountry.Items.Remove(li[i]);
        }
       
        chkCountry.DataSource = dt;
        chkCountry.DataValueField = "CountryId";
        chkCountry.DataTextField = "Country_Name";
        chkCountry.DataBind();
        //chk1.SelectedItem.Selected = true;

        if (chkCountry.Items.Count > 0)
        {
            foreach (ListItem chkitem in chkCountry.Items)
            {
                chkitem.Selected = true;
            }

        }
        for (int i = 0; i < chkCountry.Items.Count; i++)
        {
            ListItem removeItem = ddlCountry.Items.FindByValue(chkCountry.Items[i].Value);
            ddlCountry.Items.Remove(removeItem);
        }
    }
    protected void btnCountryRemove_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtCountry"];
        BindCountries();

        dt.Clear();
        Session["dtCountry"] = dt;
        chkPort.DataSource = dt;

        chkCountry.DataBind();

    }
    protected void btnUserAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtusers"];
        DataRow dr = dt.NewRow();


        dr["UserId"] = ddlUser.SelectedValue;
        dr["User_name"] = ddlUser.SelectedItem.Text;
        dt.Rows.Add(dr);

        ddlUser.Items.RemoveAt(ddlUser.SelectedIndex);

        Session["dtusers"] = dt;
        List<string> li = new List<string>();
        for (int i = 0; i < chkUser.Items.Count; i++)
        {
            if (chkUser.Items[i].Selected == false)
            {
                var rows = dt.Select("User_name='" + chkUser.Items[i] + "'");
                foreach (var row in rows)
                    row.Delete();
                li.Add(chkUser.Items[i].Value);
            }
        }
        InitialUserBind();
        for (int i = 0; i < li.ToArray().Length; i++)
        {
            ddlUser.Items.Remove(li[i]);
        }
        chkUser.DataSource = dt;
        chkUser.DataValueField = "UserId";
        chkUser.DataTextField = "User_name";
        chkUser.DataBind();
        //chk1.SelectedItem.Selected = true;

        if (chkUser.Items.Count > 0)
        {
            foreach (ListItem chkitem in chkUser.Items)
            {
                chkitem.Selected = true;
            }

        }
        for (int i = 0; i < chkUser.Items.Count; i++)
        {
            ListItem removeItem = chkUser.Items.FindByValue(chkUser.Items[i].Value);
            ddlUser.Items.Remove(removeItem);
        }
    }
    protected void btnUserRemove_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtusers"];
        BindCountries();

        dt.Clear();
        Session["dtusers"] = dt;
        chkUser.DataSource = dt;

        chkUser.DataBind();

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        
        if(ViewState["NotificationId"]!=null)
             NotificationId = Convert.ToInt32(ViewState["NotificationId"]);
        string status= "";
        //if(NotificationId == 0)
        //    status = "DRAFT";
        //else
            status = ddlStatus.SelectedItem.Text;
        DataTable dtnotification = new DataTable();
        dtnotification.Columns.Add("ID");
        dtnotification.Columns.Add("NotificationId");
        dtnotification.Columns.Add("Key_Name");
        dtnotification.Columns.Add("FKID");
        dtnotification.Columns.Add("Value");

        if (chkCountryAll.Checked)
        {
            IsAllCountries = true;
            IsAllPorts = true;
            ViewState["Country"] = "1";
        }
        else
        {
            foreach (ListItem chkitem in chkCountry.Items)
            {
                if (chkitem.Selected)
                {
                    DataRow dr = dtnotification.NewRow();

                    dr["ID"] = dtnotification.Rows.Count + 1;
                    dr["NotificationId"] = NotificationId;
                    dr["Key_Name"] = "COUNTRY";
                    dr["FKID"] = chkitem.Value;
                    dr["Value"] = chkitem.Selected == true ? 1 : 0;
                    dtnotification.Rows.Add(dr);
                    ViewState["Country"] = "1";
                }
                else
                {
                    if(ViewState["Country"]!="1")
                    ViewState["Country"] = "0";
                }
            }
        }
            if (chkPortAll.Checked)
            {
                IsAllPorts = true;
                ViewState["Port"] = "1";
            }
            else
            {
                IsAllPorts = false;
                foreach (ListItem chkitem in chkPort.Items)
                {
                    if (chkitem.Selected)
                    {
                        DataRow dr = dtnotification.NewRow();
                        dr["ID"] = dtnotification.Rows.Count + 1;
                        dr["NotificationId"] = NotificationId;
                        dr["Key_Name"] = "PORT";
                        dr["FKID"] = chkitem.Value;
                        dr["Value"] = chkitem.Selected == true ? 1 : 0;
                        dtnotification.Rows.Add(dr);
                        ViewState["Port"] = "1";
                    }
                    else
                    {
                        if (ViewState["Port"] != "1")
                        {
                            ViewState["Port"] = "0";
                        }
                    }
                }
           
        }

        if (chkVesselAll.Checked)
        {
            IsallVessels = true;
            ViewState["Vessels"] = "1";
        }
        else
        {
            IsallVessels = false;
            foreach (ListItem chkitem in chkVessel.Items)
            {
                if (chkitem.Selected)
                {
                    DataRow dr = dtnotification.NewRow();

                    dr["ID"] = dtnotification.Rows.Count + 1;
                    dr["NotificationId"] = NotificationId;
                    dr["Key_Name"] = "VESSEL";
                    dr["FKID"] = chkitem.Value;
                    dr["Value"] = chkitem.Selected == true ? 1 : 0;
                    dtnotification.Rows.Add(dr);
                    ViewState["Vessels"] = "1";
                }
                else
                {
                    if( ViewState["Vessels"] != "1")
                        ViewState["Vessels"] = "0";
                }
            }
        }

        if (chkUserAll.Checked)
        {
            IsAllUsers = true;
            ViewState["Users"] = "1";
        }
        else
        {
            IsAllUsers = false;
            foreach (ListItem chkitem in chkUser.Items)
            {
                if (chkitem.Selected)
                {
                    DataRow dr = dtnotification.NewRow();

                    dr["ID"] = dtnotification.Rows.Count + 1;
                    dr["NotificationId"] = NotificationId;
                    dr["Key_Name"] = "USER";
                    dr["FKID"] = chkitem.Value;
                    dr["Value"] = chkitem.Selected == true ? 1 : 0;
                    dtnotification.Rows.Add(dr);
                    ViewState["Users"] = "1";
                }
                else
                {
                    if (ViewState["Users"] != "1")
                    {
                        ViewState["Users"] = "0";
                    }
                }
            }
        }
        if (ViewState["Country"] != "0" && ViewState["Port"] != "0" && ViewState["Vessels"] != "0" && ViewState["Users"] != "0")
        {
            lblStatus.Visible = false;
            //Done changes for the Date format issue on save

            var from = UDFLib.ConvertToDefaultDt(dtArrivalFrom.Text);
            var To = UDFLib.ConvertToDefaultDt(dtArrivalTo.Text);

            DateTime Startdate = Convert.ToDateTime(from);
            DateTime EndDate = Convert.ToDateTime(To);
            if (EndDate < Startdate)
            {
                
                string message = "To Date should be greater than  or equal to From Date.";
               
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SavedCrewMatrix", "alert('" + message + "');", true);
            }
            else
            {

                objPortCall.Update_Port_Call_Notification(NotificationId, status, txtName.Text.Trim(), UDFLib.ConvertDateToNull(Startdate), UDFLib.ConvertDateToNull(EndDate), txtDescription.Text.Trim(),
                    dtnotification, UDFLib.ConvertToInteger(Session["UserID"].ToString()), IsAllCountries, IsAllPorts, IsallVessels, IsAllUsers);


                string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
            }
        }
        else
        {
            lblStatus.Visible = true;
            if(ViewState["Country"]=="0")
            lblStatus.Text = "Please select atleast one Country";
            if (ViewState["Port"] == "0")
                lblStatus.Text = "Please select atleast one Port";
            if (ViewState["Vessels"] == "0")
                lblStatus.Text = "Please select atleast one Vessel";
            if (ViewState["Users"] == "0")
                lblStatus.Text = "Please select atleast one User";
           
        }
    }


    protected void chkCountryAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCountryAll.Checked)
        {
            btnCountryAdd.Visible = false;
            chkCountry.Visible = false;
            ddlCountry.Visible = false;
            dvCountry.Visible = false;
        }
        else
        {
            BindCountries();
            btnCountryAdd.Visible = true;
            chkCountry.Visible = true;
            ddlCountry.Visible = true;
            dvCountry.Visible = true;
        }
    }

    protected void chkPortAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPortAll.Checked)
        {
            btnPortAdd.Visible = false;
            chkPort.Visible = false;
            ddlPort.Visible = false;
            dvPortList.Visible = false;
        }
        else
        {
            BindPort();
            btnPortAdd.Visible = true;
            chkPort.Visible = true;
            ddlPort.Visible = true;
            dvPortList.Visible = true;
        }
    }
    protected void chkVesselAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkVesselAll.Checked)
        {
            btnVesselAdd.Visible = false;
            chkVessel.Visible = false;
            ddlVessel.Visible = false;
            dvVesselList.Visible = false;
        }
        else
        {
            BindVessels();
            btnVesselAdd.Visible = true;
            chkVessel.Visible = true;
            ddlVessel.Visible = true;
            dvVesselList.Visible = true;
        }
    }
    protected void chkUserAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkUserAll.Checked)
        {
            btnUserAdd.Visible = false;
            chkUser.Visible = false;
            ddlUser.Visible = false;
            dvUserList.Visible = false;
        }
        else
        {
            BindUsers();
            btnUserAdd.Visible = true;
            chkUser.Visible = true;
            ddlUser.Visible = true;
            dvUserList.Visible = true;
        }
    }

}