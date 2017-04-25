using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class VesselMovement_portCall : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
     
    public  UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
    public int Port_call_ID;
    public int Vessel_ID;
    public string DateFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        UserAccessValidation();
        //This change has been done to change the date format as per user selection
        CalStartDate.Format = Convert.ToString(Session["User_DateFormat"]);
        CalEndDate.Format = Convert.ToString(Session["User_DateFormat"]);
        CalendarExtender1.Format = Convert.ToString(Session["User_DateFormat"]);
        CalendarExtender2.Format = Convert.ToString(Session["User_DateFormat"]);
        calAsOfDate.Format = Convert.ToString(Session["User_DateFormat"]);
        DateFormat = UDFLib.GetDateFormat();//Get User date format

        if (!IsPostBack)
        {
            string tabid = TabSCM.ActiveTab.ID;
            string tabindex = TabSCM.ActiveTab.TabIndex.ToString();
            //This change has been done to change the date format as per user selection
            txtFrom.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(System.DateTime.Now));

            txtTo.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(System.DateTime.Now.AddMonths(6)));
            txtAsofDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(System.DateTime.Now));
            DateTime dt = System.DateTime.Now;
            DateTime Fistdaydate = dt.AddDays(-(dt.Day - 1));

            DateTime LastdayDate = dt.AddMonths(1);
            LastdayDate = LastdayDate.AddDays(-(LastdayDate.Day));

            txtStartDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(Fistdaydate));

            txtEndDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(LastdayDate));
            Load_VesselList();
            Load_PortList();
            BindPortCall();
            BindTab();

        }
      
    }
    public string GetPortCallID()
    {
        try
        {
           
           if (Request.QueryString["Supp_ID"] != null)
            {
                return Request.QueryString["Supp_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    private void BindPortTemplate()
    {
        DataTable dt = objPortCall.Get_PortCallTemplate(UDFLib.ConvertIntegerToNull(DDLVesselFilter.SelectedValue));

        if (dt.Rows.Count > 0)
        {

            gvTemplate.DataSource = dt;
            gvTemplate.DataBind();
            gvTemplate.Visible = true;

        }
        else
        {
            gvTemplate.DataSource = dt;
            gvTemplate.DataBind();
            //gvTemplate.Visible = false;
        }
       
    }
    
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        //if (objUA.Add == 0) //ImgAdd.Visible = false;
        //if (objUA.Edit == 1)
        //    uaEditFlag = true;
        //else
        //    btnsave.Visible = false;
        //if (objUA.Delete == 1) uaDeleteFlage = true;

    }


    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    public void Load_VesselList()
    {
        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", 1);


        DDLVesselFilter.DataSource = dt;
        DDLVesselFilter.DataTextField = "VESSEL_NAME";
        DDLVesselFilter.DataValueField = "VESSEL_ID";
        DDLVesselFilter.DataBind();


        ddlvessel.DataSource = dt;
        ddlvessel.DataTextField = "VESSEL_NAME";
        ddlvessel.DataValueField = "VESSEL_ID";
        ddlvessel.DataBind();
        ddlvessel.Items.Insert(0, new ListItem("All Vessel", "0"));

        ddlCrewVessel.DataSource = dt;
        ddlCrewVessel.DataTextField = "VESSEL_NAME";
        ddlCrewVessel.DataValueField = "VESSEL_ID";
        ddlCrewVessel.DataBind();
        ddlCrewVessel.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    public void Load_PortList()
    {

        DataTable dt = objPortCall.Get_PortCall_PortList(0,UDFLib.ConvertIntegerToNull(DDLVesselFilter.SelectedValue),1);
        if (dt.Rows.Count > 0)
        {
            DDLPort.DataSource = dt;
            DDLPort.DataTextField = "Port_Name";
            DDLPort.DataValueField = "Port_ID";
            DDLPort.DataBind();
            btnsave.Enabled = true;
        }
        else
        {
            DDLPort.DataSource = dt;
            DDLPort.DataTextField = "Port_Name";
            DDLPort.DataValueField = "Port_ID";
            DDLPort.DataBind();
            btnsave.Enabled = false;
        }
        DataTable dtport = objBLLPort.Get_PortList_Mini();

        ddlportfilter.DataSource = dtport;
        ddlportfilter.DataTextField = "Port_Name";
        ddlportfilter.DataValueField = "Port_ID";
        ddlportfilter.DataBind();
        ddlportfilter.Items.Insert(0, new ListItem("All Port", "0"));

        ddlPortCost.DataSource = dtport;
        ddlPortCost.DataTextField = "Port_Name";
        ddlPortCost.DataValueField = "Port_ID";
        ddlPortCost.DataBind();
        ddlPortCost.Items.Insert(0, new ListItem("All Port", "0"));
    }
    public void BindPortCall()
    {
        string js = "";
        if (Convert.ToDateTime(txtFrom.Text) > Convert.ToDateTime(txtTo.Text))
        {
            js = "alert('From Date can't be before of To Date.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertpc", js, true);
            //string message = "alert('From Date can't be before of To Date.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

        }
        int rowcount = 1;// ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objPortCall.Get_PortCall_Search(null, UDFLib.ConvertIntegerToNull(DDLVesselFilter.SelectedValue), null,Convert.ToDateTime(txtFrom.Text),Convert.ToDateTime(txtTo.Text), sortbycoloumn, sortdirection, 1,1000, ref  rowcount);
        if (dt.Rows.Count > 0)
        {
            gvPortCall.DataSource = dt;
            gvPortCall.DataBind();
            table1.Visible = true;
        }
        else
        {
            gvPortCall.DataSource = dt;
            gvPortCall.DataBind();
            table1.Visible = false;
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        string tabid = TabSCM.ActiveTab.ID;
        string tabindex = TabSCM.ActiveTab.TabIndex.ToString();
            BindPortCall();
            Load_PortList();
            //BindPortCall();
            Session["Vessel_ID"] = DDLVesselFilter.SelectedValue;
            BindTab();
    
    }

    

    protected void onUpdate(object s, CommandEventArgs e)
    {
        BLL_Infra_Supplier objSupp = new BLL_Infra_Supplier();
        HiddenFlag.Value = "Edit";

        OperationMode = "Edit Port Call";



        string[] arg = e.CommandArgument.ToString().Split(',');
        int Port_call_ID = UDFLib.ConvertToInteger(arg[0]);
        int Vessel_ID = UDFLib.ConvertToInteger(arg[1]);


        DataTable dtPortCall = objPortCall.Get_PortCall_List(Convert.ToInt32(Port_call_ID), Vessel_ID);

        string AddMaker = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddMaker", AddMaker, true);
    }

    protected void lbtnDelete_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int Port_call_ID = UDFLib.ConvertToInteger(arg[0]);
        int Vessel_ID = UDFLib.ConvertToInteger(arg[1]);

        objPortCall.Del_PortCall_Details_DL(Convert.ToInt32(Port_call_ID), Vessel_ID, Convert.ToInt32(Session["USERID"].ToString()));
        string tabid = TabSCM.ActiveTab.ID;
        string tabindex = TabSCM.ActiveTab.TabIndex.ToString();
        BindPortCall();
        Load_PortList();
        //BindPortCall();
        BindTab();

    }
   

    protected void lnkAuto_Click(object s, CommandEventArgs e)
    {
        string Autodate = null;
        LinkButton btn = (LinkButton)s;
        DataListItem item = (DataListItem)btn.NamingContainer;
        LinkButton lnkAuto = (LinkButton)item.FindControl("lnkAuto");
        ImageButton ImgUpdate = (ImageButton)item.FindControl("ImgUpdate");
        string[] arg = e.CommandArgument.ToString().Split(',');
        int Port_call_ID = UDFLib.ConvertToInteger(arg[0]);
        int Vessel_ID = UDFLib.ConvertToInteger(arg[1]);
        if (lnkAuto.Text == "Auto Date is Off")
        {
            ImgUpdate.Visible = false;
            lnkAuto.Text = "Auto Date is On";
            Autodate = "NO";
        }
        else
        {
            ImgUpdate.Visible = true;
            lnkAuto.Text = "Auto Date is Off";
            Autodate = "YES";
        }
        objPortCall.Update_PortCall_Details_AutoDate(Convert.ToInt32(Port_call_ID), Vessel_ID, Autodate, Convert.ToInt32(Session["USERID"].ToString()));
        string tabid = TabSCM.ActiveTab.ID;
        string tabindex = TabSCM.ActiveTab.TabIndex.ToString();
        BindPortCall();
        Load_PortList();
        //BindPortCall();
        BindTab();
    }
    
   
    protected void gvPortCall_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            int Port_call_ID = UDFLib.ConvertToInteger(arg[0]);
            int Vessel_ID = UDFLib.ConvertToInteger(arg[1]);

            objPortCall.Del_PortCall_Details_DL(Convert.ToInt32(Port_call_ID), Vessel_ID, Convert.ToInt32(Session["USERID"].ToString()));
            BindPortCall();
        }
        if (e.CommandName == "Select")
        {
            TabSCM.ActiveTabIndex = 0;
            //string tabid = "4";
            //string tabindex = "4";
            string[] arg = e.CommandArgument.ToString().Split(',');
            //Port_call_ID = UDFLib.ConvertToInteger(arg[1]);
            //Vessel_ID = UDFLib.ConvertToInteger(arg[1]);
            Session["Port_call_ID"] = UDFLib.ConvertToInteger(arg[1]);
           // Session["Vessel_ID"] = UDFLib.ConvertToInteger(arg[0]);
            Session["Vessel_ID"] = DDLVesselFilter.SelectedValue;
            DateTime AsofDate =  Convert.ToDateTime(arg[2]);
            string PortID = Convert.ToString(arg[3]);
            txtAsofDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(AsofDate));
            
            ddlportfilter.SelectedValue = PortID;
            ddlPortCost.SelectedValue = PortID;
            //BindcrewList();
            //BindPortCallHistory(1, UDFLib.ConvertIntegerToNull(ddlportfilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlvessel.SelectedValue));
            BindTab();
            
        }
    }


    protected void btnsave_Click(object sender, EventArgs e)
    {

        objPortCall.Ins_Copy_PortCall_Details(UDFLib.ConvertToInteger(DDLVesselFilter.SelectedValue), UDFLib.ConvertToInteger(DDLPort.SelectedValue), DDLPort.SelectedItem.Text, Convert.ToInt32(Session["USERID"].ToString()));
        BindPortCall();
        Load_PortList();
        BindTab();
    }
   
    
    protected void gvTemplate_RowEditing(object sender, GridViewEditEventArgs de)
    {
        GridView _gridView = (GridView)sender;

        _gridView.EditIndex = de.NewEditIndex;
        _gridView.SelectedIndex = de.NewEditIndex;
        BindPortTemplate();
        
        GridViewRow row = gvTemplate.Rows[de.NewEditIndex];
        DropDownList ddlCAgent = (DropDownList)(row.FindControl("ddlCAgent"));
        DropDownList ddlOAgent = (DropDownList)(row.FindControl("ddlOAgent"));
         Label lblCagent = (Label)(row.FindControl("lblc"));
         Label lblOagent = (Label)(row.FindControl("lblo"));
        DataSet ds = objPortCall.Get_PortCall_AgentList(UDFLib.ConvertToInteger(DDLVesselFilter.SelectedValue));
        ddlCAgent.DataSource = ds.Tables[0];
        ddlCAgent.DataTextField = "SHORT_NAME";
        ddlCAgent.DataValueField = "SUPPLIER";
        ddlCAgent.DataBind();
        ddlCAgent.Items.Insert(0, new ListItem("-Select-", "0"));
        ddlOAgent.DataSource = ds.Tables[1];
        ddlOAgent.DataTextField = "SHORT_NAME";
        ddlOAgent.DataValueField = "SUPPLIER";
        ddlOAgent.DataBind();
        ddlOAgent.Items.Insert(0, new ListItem("-Select-", "0"));

        ddlCAgent.SelectedValue = lblCagent.Text.ToString();
        ddlOAgent.SelectedValue = lblOagent.Text.ToString();
        
    }
    protected void gvTemplate_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        _gridView.EditIndex = -1;
        BindPortTemplate();
    }
    protected void gvTemplate_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        GridView _gridView = (GridView)sender;
        int nCurrentRow = e.RowIndex;
        _gridView.EditIndex = -1;
        BindPortTemplate();
    }
    protected void gvTemplate_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        GridView _gridView = (GridView)sender;
        int nCurrentRow = Int32.Parse(e.CommandArgument.ToString());

        if (e.CommandName.ToUpper().Equals("UPDATE"))
        {
            int id =  Convert.ToInt16(_gridView.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString());
            TextBox txtSeaTime = (TextBox)_gridView.Rows[nCurrentRow].FindControl("txtSeaTime");
            TextBox txtInportTime = (TextBox)_gridView.Rows[nCurrentRow].FindControl("txtInportTime");
            DropDownList ddlCAgent = (DropDownList)_gridView.Rows[nCurrentRow].FindControl("ddlCAgent");
            DropDownList ddlOAgent = (DropDownList)_gridView.Rows[nCurrentRow].FindControl("ddlOAgent");

            char[] delimiterChars = { ' ', ',', '.', ':', '\t','/' };

            string seatime = txtSeaTime.Text;
            string porttime = txtInportTime.Text;
            string[] Seatime1 = seatime.Split(delimiterChars);
            string[] porttime1 = porttime.Split(delimiterChars);

            if (Seatime1.Length == 3 && porttime1.Length == 3)
            {

                if (Convert.ToInt16(Seatime1[1]) >= 24 || Convert.ToInt16(Seatime1[2]) > 60 || Convert.ToInt16(porttime1[1]) > 24 || Convert.ToInt16(porttime1[2]) > 60)
                {

                    string message = "alert('Please Enter Correct Time.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                }
                else
                {
                    //objPortCall.Upd_PortCall_Template_Details(Convert.ToInt32(id), txtSeaTime.Text, txtInportTime.Text, UDFLib.ConvertStringToNull(ddlCAgent.SelectedValue), UDFLib.ConvertStringToNull(ddlOAgent.SelectedValue), Convert.ToInt32(Session["USERID"].ToString()));
                }
            }
            else
            {
                string message = "alert('Please Enter Correct format(day/hh:mm)')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
        }

        BindPortTemplate();
    }
    protected void TabSCM_ActiveTabChanged(object sender, EventArgs e)
    {
        BindTab();
        
    }
    protected void BindTab()
    {
        if (TabSCM.ActiveTabIndex == 1)
        {
            BindPortTemplate();
        }
        else if (TabSCM.ActiveTabIndex == 5)
        {
            ddlCrewVessel.SelectedValue = DDLVesselFilter.SelectedValue;
           
            ddlCrewStatus.SelectedValue = "2";
            BindcrewList();
        }
        else if (TabSCM.ActiveTabIndex == 2)
        {
            ddlvessel.SelectedValue = DDLVesselFilter.SelectedValue;
            BindPortCallHistory(1, UDFLib.ConvertIntegerToNull(ddlportfilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlvessel.SelectedValue));
        }
        else if (TabSCM.ActiveTabIndex == 3)
        {
            BindPortCallAlert(0, 0, 0);
        }
        else if (TabSCM.ActiveTabIndex == 4)
        {
            BindPortCostList();
        }
        else if (TabSCM.ActiveTabIndex == 0)
        {
            iFrame1.Attributes["src"] = "Port_Call_Report.aspx";
        }
    }
    protected void btnportfilter_Click(object s, EventArgs e)
    {

        BindPortCallHistory(1, UDFLib.ConvertIntegerToNull(ddlportfilter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlvessel.SelectedValue));

    }
    protected void imgCrewFilter_Click(object s, EventArgs e)
    {
        BindcrewList();

    }
    protected void BindPortCallAlert(int Type, int? PortID, int? VesselID)
    {
        DateTime Startdate = Convert.ToDateTime(txtStartDate.Text);
        DateTime EndDate = Convert.ToDateTime(txtStartDate.Text);
        DataTable dt = objPortCall.Get_PortCallHistory(Type, PortID, VesselID, Startdate, EndDate,"0");
        if (dt.Rows.Count > 0)
        {
            gvPortAlert.DataSource = dt;
            gvPortAlert.DataBind();
            gvPortAlert.Visible = true;
        }
        else
        {
            gvPortAlert.DataSource = dt;
            gvPortAlert.DataBind();
        }
    }
    protected void BindPortCallHistory(int Type,int? PortID,int?VesselID)
    {
        DateTime Startdate = Convert.ToDateTime(txtStartDate.Text);
        DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
        DataTable dt = objPortCall.Get_PortCallHistory(Type, PortID, VesselID, Startdate, EndDate,rdborder.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            gvPortCallHistory.DataSource = dt;
            gvPortCallHistory.DataBind();
            gvPortCallHistory.Visible = true;
        }
        else
        {
            gvPortCallHistory.DataSource = dt;
            gvPortCallHistory.DataBind();
        }
    }
    protected void BindcrewList()
    {
        DataTable dt = objPortCall.Get_PortCall_CrewList(Convert.ToInt32(ddlCrewVessel.SelectedValue), Convert.ToDateTime(txtAsofDate.Text), UDFLib.ConvertIntegerToNull(ddlCrewStatus.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            gvCrewList.DataSource = dt;
            gvCrewList.DataBind();
            gvCrewList.Visible = true;
        }
        else
        {
            gvCrewList.DataSource = dt;
            gvCrewList.DataBind();
            //gvCrewList.Visible = true;
        }
    }
    

    protected void ImgPortCost_Click(object s, EventArgs e)
    {
        BindPortCostList();

    }
    protected void BindPortCostList()
    {
        //DataSet ds = objPortCall.Get_PortCall_PortCost(UDFLib.ConvertIntegerToNull(ddlPortCost.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVesselFilter.SelectedValue));
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    gvPortCost.DataSource = ds;
        //    gvPortCost.DataBind();
        //    gvPortCost.Visible = true;
        //}
        //else
        //{
        //    gvPortCost.DataSource = ds;
        //    gvPortCost.DataBind();
        //    //gvCrewList.Visible = true;
        //}
    }
    protected void btnPrintDPL_Click(object sender, EventArgs e)
    {
       
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../VesselMovement/Port_DPL.aspx', '_blank');", true);
       // Response.Redirect("Port_DPL.aspx");
    }
    protected void btnVesselReport_Click(object sender, EventArgs e)
    {
       
        Session["Vessel_ID"] = DDLVesselFilter.SelectedValue;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../Operations/Default.aspx', '_blank');", true);
       // Response.Redirect("../Operations/Default.aspx?Vessel_ID=" + DDLVesselFilter.SelectedValue + "");
    }
    protected void gvPortCall_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {

            Image ImgCard = ((ImageButton)e.Item.FindControl("ImgView"));
            Image imgPurchasebtn = ((ImageButton)e.Item.FindControl("imgPurchasebtn"));
            Image imgPoAgencybtn = ((ImageButton)e.Item.FindControl("imgPoAgencybtn"));
            Image imgWorkList = ((ImageButton)e.Item.FindControl("imgWorkList"));
            Image imgAgencyWork = ((ImageButton)e.Item.FindControl("imgAgencyWork"));
            ImageButton ImgUpdate = ((ImageButton)e.Item.FindControl("ImgUpdate"));
            LinkButton lnkAuto = (LinkButton)e.Item.FindControl("lnkAuto");
      
            if (DataBinder.Eval(e.Item.DataItem, "Auto_Date").ToString() == "YES")
            {
                ImgUpdate.Visible = true;
                lnkAuto.Text = "Auto Date is Off";
            }
            else if (DataBinder.Eval(e.Item.DataItem, "Auto_Date").ToString() == "NO")
            {
                ImgUpdate.Visible = false;
                lnkAuto.Text = "Auto Date is On";
            }
            if (DataBinder.Eval(e.Item.DataItem, "CrewOn").ToString() != "0" || DataBinder.Eval(e.Item.DataItem, "CrewOffCount").ToString() != "0")
            {
                ImgCard.Visible = true;
            }
            else
            {
                ImgCard.Visible = false;
            }
            if (Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "AgencyPo").ToString()) > 0)
            {
                imgAgencyWork.Visible = true;
            }
            else
            {
                imgAgencyWork.Visible = false;
            }
            if (Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "PO").ToString()) > 0)
            {
                imgPurchasebtn.Visible = true;
            }
            else
            {
                imgPurchasebtn.Visible = false;
            }
            if (Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "WorkList").ToString()) > 0)
            {
                imgWorkList.Visible = true;
            }
            else
            {
                imgWorkList.Visible = false;
            }


        }
        catch { }
    }
    protected void gvPortCost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnDAEdit = (ImageButton)e.Row.FindControl("lbtnDAEdit");
                Label lAgent = (Label)e.Row.FindControl("lblAgent");
                if(lAgent.Text != "")
                {
                    btnDAEdit.Visible = true;
                }
                else
                {
                    btnDAEdit.Visible = false;
                }
            }

        }
        catch { }
    }
    protected void lbtnDAEdit_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? DA_ID = UDFLib.ConvertIntegerToNull(arg[0]);
        ViewState["DA_ID"] = UDFLib.ConvertIntegerToNull(arg[0]);
        ViewState["Agent_Code"] = UDFLib.ConvertIntegerToNull(arg[1]);
        DataSet ds = objPortCall.Get_PortCall_DAItem(UDFLib.ConvertStringToNull(ViewState["DA_ID"]), UDFLib.ConvertStringToNull(ViewState["Agent_Code"]), UDFLib.ConvertStringToNull(ddlPortCost.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {

            gvDAItem.DataSource = ds;
            gvDAItem.DataBind();
        }

        //BindActivity();

    }
    protected void btnSupplier_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_General_Data.aspx?Supp_ID=0000', '_blank');", true);
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        ChangeDateTime(-7);
        BindPortCall();
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        ChangeDateTime(7);
         BindPortCall();
    }
    protected void ChangeDateTime(int Days)
    {
        DateTime From = Convert.ToDateTime(txtFrom.Text);
        DateTime To = Convert.ToDateTime(txtTo.Text);
        From = From.AddDays(Days);
        To = To.AddDays(Days);
        txtFrom.Text = From.ToString("dd-MM-yyyy");
        txtTo.Text = To.ToString("dd-MM-yyyy");
    }
    //else if (e.CommandName.ToUpper().Equals("DELETE"))
    //{

    //    Label lblDeptID = (Label)_gridView.Rows[nCurrentRow].FindControl("lblDeptID");
    //    Label lblScmResID = (Label)_gridView.Rows[nCurrentRow].FindControl("lblScmResID");

    //    //OfficeRepresentativeDelete(lblDeptID.Text, lblScmResID.Text);

    //}
    //protected void btnsave_Click(object s, EventArgs e)
    //{

    //    if (HiddenFlag.Value == "Add")
    //    {
    //        //objPortCall.Ins_PortCall_Details(null, null, DDLPort.SelectedItem.ToString()
    //        //    , UDFLib.ConvertDateToNull(dtpArrival.Text)
    //        //    , UDFLib.ConvertDateToNull(dtpBerthing.Text)
    //        //    , UDFLib.ConvertDateToNull(dtpDeparture.Text), null, null, txtPortRemark.Text, null, null
    //        //    , UDFLib.ConvertToInteger(DDLPort.SelectedValue), null, UDFLib.ConvertToInteger(ddlVessel.SelectedValue), Convert.ToInt32(Session["USERID"].ToString()), Convert.ToInt32(chkWarRisk.Checked), Convert.ToInt32(chkShipCrane.Checked));
    //    }
    //    else
    //    {
    //        //objPortCall.Upd_PortCall_Details(Convert.ToInt32(txtPortCallID.Text), txtVesselCode.Text, null, DDLPort.SelectedItem.ToString()
    //        //   , UDFLib.ConvertDateToNull(dtpArrival.Text)
    //        //   , UDFLib.ConvertDateToNull(dtpBerthing.Text)
    //        //   , UDFLib.ConvertDateToNull(dtpDeparture.Text), null, null, txtPortRemark.Text, null, null
    //        //   , UDFLib.ConvertToInteger(DDLPort.SelectedValue), null, UDFLib.ConvertToInteger(ddlVessel.SelectedValue), Convert.ToInt32(Session["USERID"].ToString()), Convert.ToInt32(chkWarRisk.Checked), Convert.ToInt32(chkShipCrane.Checked));

    //    }


    //    string hideMaker = String.Format("hideModal('divadd')");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hideMaker", hideMaker, true);

    //    BindPortCall();
    //}

    //protected void lnkAddNew_Click(object s, EventArgs e)
    //{
    //    HiddenFlag.Value = "Add";
    //    OperationMode = "Add Port Call";

    //    this.SetFocus("ctl00_MainContent_ddlVessel");


    //    DDLPort.SelectedValue = "0";
    //    ddlVessel.SelectedValue = "0";

    //    dtpArrival.Text = "";
    //    dtpBerthing.Text = "";
    //    dtpDeparture.Text = "";
    //    txtPortRemark.Text = "";


    //    string AddPortCall = String.Format("showModal('divadd',false);");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPortCall", AddPortCall, true);

    //}
    //protected void gvPortCall_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        if (ViewState["SORTBYCOLOUMN"] != null)
    //        {
    //            HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
    //            if (img != null)
    //            {
    //                if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
    //                    img.Src = "~/purchase/Image/arrowUp.png";
    //                else
    //                    img.Src = "~/purchase/Image/arrowDown.png";

    //                img.Visible = true;
    //            }
    //        }
    //    }


    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {


    //        e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
    //        e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";


    //        Label lblAddress = (Label)e.Row.FindControl("lblAddress");
    //        Label lblCountry = (Label)e.Row.FindControl("lblCountry");


    //        if (DataBinder.Eval(e.Row.DataItem, "Port_Remarks").ToString().Length > 20)
    //        {
    //            lblAddress.Text = lblAddress.Text + "..";
    //            lblAddress.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Address] body=[" + DataBinder.Eval(e.Row.DataItem, "Port_Remarks").ToString() + "]");
    //        }



    //    }

    //}

    //protected void gvPortCall_Sorting(object sender, GridViewSortEventArgs se)
    //{

    //    ViewState["SORTBYCOLOUMN"] = se.SortExpression;

    //    if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
    //        ViewState["SORTDIRECTION"] = 1;
    //    else
    //        ViewState["SORTDIRECTION"] = 0;

    //    BindPortCall();

    //}
}