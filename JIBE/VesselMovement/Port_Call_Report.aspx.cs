using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using SMS.Business.CP;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class VesselMovement_Port_Call_Report : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlag = false;
    public string Vessel_ID,Port_CallID;
    public UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
    public string DateFormat = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        //This change has been done to change the date format as per user selection
        CalendarExtender1.Format = Convert.ToString(Session["User_DateFormat"]);
        CalendarExtender2.Format = Convert.ToString(Session["User_DateFormat"]);
        DateFormat = UDFLib.GetDateFormat();//Get User date format
        if (!IsPostBack)
        {
           

            if (Request.QueryString["PCID"] != null)
                Session["Port_call_ID"] = Request.QueryString["PCID"];
            if (Request.QueryString["VID"] != null)
                Session["Vessel_ID"] = Request.QueryString["VId"];
            if (Request.QueryString["PID"] != null)
                Session["Port_ID"] = Request.QueryString["PId"];

               ViewState["ActiVityID"] = 0;
              

                BindAgent();
                BindCharter();
                Load_PortList();
                BindPortCall();
                BindCrewList();
                BindActivity();
                BindTask();
                BindPurchaseOrder();
                BindDARecord();

        }
        string msg1 = String.Format("StaffInfo();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Edit == 1)
        {
            uaEditFlag = true;
            btnCharter.Visible = true;
            btnExit.Visible = true;
            btnCSupplier.Visible = true;
            btnOwner.Visible = true;
            btnTask.Visible = true;
            btnOSupplier.Visible = true;
            ddlOwnerAgent.Visible = true;
            ddlCharterAgent.Visible = true;
            lblOwnerAgent.Visible = false;
            lblCharterAgent.Visible = false;
            lblCharterer.Visible = false;
            ddlCharter.Visible = true;
        }
        else
        {
            uaEditFlag = false;
            btnCharter.Visible = false;
            btnExit.Visible = false;
            btnOwner.Visible = false;
            ddlOwnerAgent.Visible = false;
            ddlCharterAgent.Visible = false;
            lblOwnerAgent.Visible = true;
            lblCharterAgent.Visible = true;
            lblCharterer.Visible = true;
            ddlCharter.Visible = false;
        }

        if (objUA.Add == 1)
        {
            btnTask.Visible = true;
            
            //Previous was it False
            //Mdf By:Nilesh Pawar,Mdf on:20-06-2016
            btnActivity.Visible = gvActivity.Visible;
            //end
        }
        else
        {
            btnActivity.Visible = false;
            btnTask.Visible = false;
        }

        if (objUA.Delete == 1)
            uaDeleteFlag = true;

      

    }
    protected void BindDARecord()
    {
        DataSet ds = objPortCall.Get_PortCall_DA(UDFLib.ConvertIntegerToNull(Request.QueryString["PCID"]));

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvDARecord.DataSource = ds;
            gvDARecord.DataBind();
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    /// <summary>
    /// Method to bild the detail informations for particular  port call
    /// </summary>
    protected void BindPortCall()
    {
        try
        {
            DataSet ds = objPortCall.Get_PortCall_Search_List(UDFLib.ConvertIntegerToNull(Request.QueryString["VId"]), UDFLib.ConvertIntegerToNull(Request.QueryString["PCID"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                lblPortCode.Text = dr["Port_Call_ID"].ToString();
                lblVesselname.Text = dr["Vessel_Name"].ToString();
                lblPortName.Text = dr["PORT_NAME"].ToString();
                if (Convert.ToInt16(dr["Port_ID"]) == 0)
                {
                    Label4.Visible = false;
                    DDLPort.Visible = true;
                    lblPortName.BackColor = System.Drawing.Color.Yellow;
                    btnLinkPort.Visible = true;
                }
                else
                    lblPortName.BackColor = System.Drawing.Color.White;
                DDLPort.SelectedValue = dr["Port_ID"].ToString();
                if (dr["CHARTERID"].ToString() != null && dr["CHARTERID"].ToString() != "")
                {
                    ddlCharter.SelectedValue = dr["CHARTERID"].ToString();
                    if (ddlCharter.SelectedValue != "0")
                        lblCharterer.Text = ddlCharter.SelectedItem.Text;

                }
                if (dr["Charter_ID"].ToString() != null && dr["Charter_ID"].ToString() != "0")
                {
                    btnCharter.Text = "Update";
                    ddlCharterAgent.SelectedValue = dr["Charter_ID"].ToString();
                    if (ddlCharterAgent.SelectedValue != "0")
                        lblCharterAgent.Text = ddlCharterAgent.SelectedItem.Text;
                }



                if (dr["Owners_ID"].ToString() != null && dr["Owners_ID"].ToString() != "0")
                {
                    btnOwner.Text = "Update";
                    ddlOwnerAgent.SelectedValue = dr["Owners_ID"].ToString();
                    if (ddlOwnerAgent.SelectedValue != "0")
                        lblOwnerAgent.Text = ddlOwnerAgent.SelectedItem.Text;

                }

                txtRemarks.Text = dr["Port_Remarks"].ToString();
                if (dr["Port_Call_Status"].ToString() == "OMITTED")
                {
                    chkomit.Checked = true;
                }
                else
                {
                    chkomit.Checked = false;
                }


                txtAddress.Text = dr["ADD1"].ToString();
                txtCity.Text = dr["City1"].ToString();
                txtCountry.Text = dr["Country1"].ToString();
                txtPhone.Text = dr["phone1"].ToString();
                txtFax.Text = dr["Fax1"].ToString();
                txtEmail.Text = dr["Email1"].ToString();
                txtPICName.Text = dr["PICName1"].ToString();
                txtPICName2.Text = dr["PicName2"].ToString();
                txtPICEmail.Text = dr["PICEmail1"].ToString();
                txtPICEmail2.Text = dr["PicEmail2"].ToString();
                txtPICPhone.Text = dr["PICPhone1"].ToString();
                txtPICPhone2.Text = dr["PICPhone2"].ToString();

                txtOAddress.Text = dr["Add2"].ToString();
                txtOCity.Text = dr["City2"].ToString();
                txtOCountry.Text = dr["Country2"].ToString();
                txtOPhone.Text = dr["Phone2"].ToString();
                txtOFax.Text = dr["Fax2"].ToString();
                txtOEmail.Text = dr["Email2"].ToString();
                txtOPICName.Text = dr["PICOName1"].ToString();
                txtOPICName2.Text = dr["PICOName2"].ToString();
                txtOPICEmail.Text = dr["PICOEmail1"].ToString();
                txtOPICEmail2.Text = dr["PICOEmail2"].ToString();
                txtOPICPhone.Text = dr["PICOPhone1"].ToString();
                txtOPICPhone2.Text = dr["PICOPhone2"].ToString();


                if (dr["DA_ID"].ToString() == "00000")
                {
                    gvActivity.Visible = false;
                    btnActivity.Visible = false;
                    txtDesc.Visible = false;
                    ltDes.Visible = false;
                    ltCost.Visible = false;
                    txtCost.Visible = false;
                    ltactivity.Visible = false;
                    txtActivateDate.Visible = false;
                }

                else
                {
                    gvtask.Visible = false;
                    ltPlanDes.Visible = false;
                    txtPlanDesc.Visible = false;
                    btnTask.Visible = false;
                    ltCheckDate.Visible = false;
                    txtCheckDate.Visible = false;
                    btnTask.Visible = false;
                }

                //Mdf By:Nilesh Pawar,Mdf on:20-06-2016
                btnActivity.Visible = gvActivity.Visible;
                rwAct.Visible = gvActivity.Visible;
                rwTask.Visible = gvtask.Visible;
                //end

            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void BindAgent()
    {
        //DataSet ds = objPortCall.Get_PortCall_AgentList(UDFLib.ConvertToInteger(ViewState["Port_call_ID"]));
        DataTable dtcharterer = objPortCall.GetAgent_List();
        ddlCharterAgent.DataSource = dtcharterer;
        ddlCharterAgent.DataTextField = "Supplier_Name";
        ddlCharterAgent.DataValueField = "SUPPLIER_CODE";
        ddlCharterAgent.DataBind();
        ddlCharterAgent.Items.Insert(0, new ListItem("-Select-", "0"));

        DataTable dtOwnerAgent = objPortCall.GetAgent_List();
        ddlOwnerAgent.DataSource = dtOwnerAgent;
        ddlOwnerAgent.DataTextField = "Supplier_Name";
        ddlOwnerAgent.DataValueField = "SUPPLIER_CODE";
        ddlOwnerAgent.DataBind();
        ddlOwnerAgent.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void BindCharter()
    {
        DataSet ds = objPortCall.Get_PortCall_CharterParty(UDFLib.ConvertToInteger(Request.QueryString["VId"]));
        ddlCharter.DataSource = ds.Tables[0];
        ddlCharter.DataTextField = "Charter";
        ddlCharter.DataValueField = "CharterID";
        ddlCharter.DataBind();
        ddlCharter.Items.Insert(0, new ListItem("-Select-", "0"));
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
    protected void btnCharter_Click(object sender, EventArgs e)
    {
        string PortCallStatus = null;
        //if (chkomit.Checked == true)
        //{
        //    PortCallStatus = "OMITTED";
        //}
        int str = objPortCall.Update_PortCall(UDFLib.ConvertToInteger(Request.QueryString["PCID"]), UDFLib.ConvertToInteger(Request.QueryString["VId"]), UDFLib.ConvertStringToNull(ddlCharter.SelectedValue), UDFLib.ConvertStringToNull(ddlCharterAgent.SelectedValue), UDFLib.ConvertStringToNull(ddlOwnerAgent.SelectedValue), txtRemarks.Text, Convert.ToInt32(GetSessionUserID()), PortCallStatus);
        BindPortCall();
    }

    protected void btnOwner_Click(object sender, EventArgs e)
    {
        string PortCallStatus = null;
        //if (chkomit.Checked == true)
        //{
        //    PortCallStatus = "OMITTED";
        //}
        int str = objPortCall.Update_PortCall(UDFLib.ConvertToInteger(Request.QueryString["PCID"]), UDFLib.ConvertToInteger(Request.QueryString["VId"]), UDFLib.ConvertStringToNull(ddlCharter.SelectedValue), UDFLib.ConvertStringToNull(ddlCharterAgent.SelectedValue), UDFLib.ConvertStringToNull(ddlOwnerAgent.SelectedValue), txtRemarks.Text, Convert.ToInt32(GetSessionUserID()), PortCallStatus);
        BindPortCall();
    }
    protected void BindCrewList()
    {
        DataSet ds = objPortCall.Get_PortCall_CrewChange(UDFLib.ConvertIntegerToNull(Request.QueryString["PCID"]));

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvCrewOn.DataSource = ds.Tables[0];
            gvCrewOn.DataBind();
            lbl1.Visible = true;
            lblOn.Visible = true;
        }
        else
        {
            lbl1.Visible = false;
            lblOn.Visible = false;
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            gvCrewOff.DataSource = ds.Tables[1];
            gvCrewOff.DataBind();
            lbl1.Visible = true;
            lblOff.Visible = true;
        }
        else
        {
            lbl1.Visible = false;
            lblOff.Visible = false;
        }
       
    }
    protected void btnActivity_Click(object sender, EventArgs e)
    {
        //If request("DA_Status") = "DRAFT" Then
        string   Request_Type = "URGENT";
        int ActiVityType = 1;
        int STR = objPortCall.Insert_PortCall_Activity(UDFLib.ConvertToInteger(ViewState["ActiVityID"]), UDFLib.ConvertToInteger(Request.QueryString["PCID"]), UDFLib.ConvertToInteger(Request.QueryString["VId"]), UDFLib.ConvertDateToNull(txtActivateDate.Text), Request_Type,
                                               UDFLib.ConvertDecimalToNull(txtCost.Text), UDFLib.ConvertStringToNull(txtDesc.Text),
                                               UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()), ActiVityType);

         BindActivity();

         ClearControl();
         btnActivity.Text = "Add Activity";
         btnTask.Visible = false;
         ViewState["ActiVityID"] = null;
    }
    protected void ClearControl()
    {
        txtActivateDate.Text = "";
        txtDesc.Text = "";
        txtCost.Text = "";
    }
    protected void BindActivity()//ActiVityType = 1
    {
        DataSet ds = objPortCall.Get_PortCall_ActivitySearch(UDFLib.ConvertToInteger(Request.QueryString["PCID"]), UDFLib.ConvertToInteger(Request.QueryString["VId"]));

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvActivity.DataSource = ds;
            gvActivity.DataBind();
        }
    }

    protected void btnTask_Click(object sender, EventArgs e)
    {
        //If request("DA_Status") = "DRAFT" Then
        string Request_Type = "PLANNED";
        int ActiVityType = 2;
        int STR = objPortCall.Insert_PortCall_Activity(UDFLib.ConvertToInteger(ViewState["ActiVityID"]), UDFLib.ConvertToInteger(Request.QueryString["PCID"]), UDFLib.ConvertToInteger(Request.QueryString["VId"]), UDFLib.ConvertDateToNull(txtCheckDate.Text), Request_Type,
                                               UDFLib.ConvertDecimalToNull(0), UDFLib.ConvertStringToNull(txtPlanDesc.Text),
                                               UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()), ActiVityType);
        BindTask();
        ClearTaskControl();
        btnTask.Text = "Add Plan";
        ViewState["ActiVityID"] = null;
    }
    protected void ClearTaskControl()
    {
        txtCheckDate.Text = "";
        txtPlanDesc.Text = "";
    }
    protected void BindTask() //ActiVityType = 2
    {
        DataSet ds = objPortCall.Get_PortCall_TaskSearch(UDFLib.ConvertToInteger(Request.QueryString["PCID"]), UDFLib.ConvertToInteger(Request.QueryString["VId"]));

       // if (ds.Tables[0].Rows.Count > 0)
        {
            gvtask.DataSource = ds;
            gvtask.DataBind();
        }
    }
    protected void BindPurchaseOrder()
    {
        DataSet ds = objPortCall.Get_PortCall_PurchaseOrder(UDFLib.ConvertIntegerToNull(Request.QueryString["PCID"]));

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvPurchase.DataSource = ds.Tables[0];
            gvPurchase.DataBind();
            
        }
        

    }

  
    protected void lbtnEdit_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? ActivityID = UDFLib.ConvertIntegerToNull(arg[0]);
        ViewState["ActiVityID"] = UDFLib.ConvertIntegerToNull(arg[0]);
        DataSet ds = objPortCall.Get_PortCall_ActivityList(UDFLib.ConvertToInteger(ViewState["ActiVityID"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txtActivateDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Activity_Date"]));
            txtDesc.Text = dr["Request_Description"].ToString();
            txtCost.Text = dr["Estimated_Cost"].ToString();
            btnActivity.Text = "Edit Activity";
        }

        BindActivity();
        btnTask.Visible = false;
    }
    protected void lbtnDelete_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? ActivityID = UDFLib.ConvertIntegerToNull(arg[0]);

        int RetValue = objPortCall.Delete_PortCall_ActiVity(ActivityID, UDFLib.ConvertToInteger(GetSessionUserID()));
        BindActivity();
        btnTask.Visible = false;
    }
    protected void lbtnTEdit_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? TaskID = UDFLib.ConvertIntegerToNull(arg[0]);
        ViewState["ActiVityID"] = UDFLib.ConvertIntegerToNull(arg[0]);
        DataSet ds = objPortCall.Get_PortCall_ActivityList(UDFLib.ConvertToInteger(ViewState["ActiVityID"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtCheckDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Activity_Date"]));
            
            txtPlanDesc.Text = dr["Request_Description"].ToString();
            btnTask.Text = "Edit Task";
        }



    }
    protected void lbtnTDelete_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? TaskID = UDFLib.ConvertIntegerToNull(arg[0]);

        int RetValue = objPortCall.Delete_PortCall_ActiVity(TaskID, UDFLib.ConvertToInteger(GetSessionUserID()));
        BindTask();

    }
    protected void gvDARecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                Label lDA_Status = (Label)e.Row.FindControl("DA_Status");
                if (lDA_Status.Text == "Appointed")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
                
            }

        }
        catch { }
    }
    protected void btnOSupplier_Click(object sender, EventArgs e)
    {
        String OwnerID = ddlOwnerAgent.SelectedValue;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_General_Data.aspx?Supp_ID=" + OwnerID + "', '_blank');", true);
    }
    protected void btnCSupplier_Click(object sender, EventArgs e)
    {
        String CharterID = ddlCharterAgent.SelectedValue;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_General_Data.aspx?Supp_ID=" + CharterID + "', '_blank');", true);
    }
    protected void btnLinkPort_Click(object sender, EventArgs e)
    {
        int str = objPortCall.Update_PortCallLink(UDFLib.ConvertToInteger(Request.QueryString["PCID"]), UDFLib.ConvertStringToNull(DDLPort.SelectedValue), Convert.ToInt32(GetSessionUserID()));
        BindPortCall();
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
    }
}