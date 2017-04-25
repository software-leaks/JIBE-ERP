using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;
using System.Text;
using System.Globalization;
using SMS.Business.VET;

public partial class VesselMovement_PortCallVessel : System.Web.UI.Page
{
    public string OperationMode = "";
    
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    private long lCurrentRecord = 0;
    private long lRecordsPerRow = 200;
    public UserAccess objUA = new UserAccess();
    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();

        //This change has been done to change the date format as per user selection
        CalendarExtender1.Format = Convert.ToString(Session["User_DateFormat"]);
        

        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "myFunction();", true);
        if (!IsPostBack)
        {
            Session["Port_ID"] = null;
            Session["Port_call_ID"] = null;
            Session["Vessel_ID"] = null;
            Session["Arrival"] = null;
            Session["PortCall"] = null;
            Session["FromDate"] = null;

            txtFrom.Text = System.DateTime.Now.ToString("dd-MM-yyyy");

            BindFleetDLL();
           
            Load_VesselList();
           
            BindPortCall();
          
        }
      
    }
    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            if(FleetDT.Rows.Count > 0)
            {
                if (Session["USERFLEETID"] != null && Session["USERFLEETID"].ToString() != "0")
                {
                    DDLFleet.SelectItems(new string[] { Session["USERFLEETID"].ToString() });
                }
                else
                {
                    foreach (DataRow dr in FleetDT.Rows)
                    {
                        DDLFleet.SelectItems(new string[] { dr["code"].ToString() });
                    }
                }

            }
            CheckBox chAll = (CheckBox)DDLFleet.FindControl("chkSelectAll");
            if (FleetDT.Rows.Count == DDLFleet.SelectedValues.Rows.Count)
            {
                
                chAll.Checked = true;
            }
            else
                chAll.Checked = true;
            Session["sFleet"] = DDLFleet.SelectedValues;
        }
        catch (Exception ex)
        {

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

        //if (objUA.Add == 0)
        //    btnsave.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
        {
            //btnAlert.Visible = false;
            //btnPortCost.Visible = false;
            //btnPort.Visible = false;
            //PortCallNotification.Visible = false;
        }
        if (objUA.Delete == 1) uaDeleteFlage = true;

        DataTable dt = objPortCall.Get_PortCallAlertList(Convert.ToInt32(Session["USERID"].ToString()), "0");
        if (dt.Rows.Count > 0)
        {
            int iCount = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["READ_ID"]) == 0)
                {
                    iCount++;
                }
            }

            if (iCount > 0)
            {
                btnAlerts.Visible = true;

            }
        }
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
        try
        {
            DataTable dtVessel = objPortCall.Get_PortCall_VesselList((DataTable)Session["sFleet"], 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()),GetSessionUserID());
            foreach (DataRow dr in dtVessel.Rows)
            {
                if (dr["Vessel_Id"] == "11" || dr["Vessel_Id"] == "13")
                    dtVessel.Rows.Remove(dr);
            }

            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            if (dtVessel.Rows.Count > 0)
            {
                for (int i = 0; i < dtVessel.Rows.Count; i++)
                {
                    DDLVessel.SelectItems(new string[] { dtVessel.Rows[i]["Vessel_id"].ToString() });
                  
                }

            }
            Session["sVesselCode"] = DDLVessel.SelectedValues;

            CheckBox chAll = (CheckBox)DDLVessel.FindControl("chkSelectAll");
            if (dtVessel.Rows.Count == DDLVessel.SelectedValues.Rows.Count)
            {

                chAll.Checked = true;
            }
            else
                chAll.Checked = true;


        }
        catch (Exception ex)
        {

        }

       
    }
    public void BindPortCall()
    {
        try
        {
            BLL_VET_VettingLib objVet = new BLL_VET_VettingLib();
            Session["FromDate"] = Convert.ToDateTime(txtFrom.Text.ToString());

            DataSet ds = objPortCall.Get_PortCall_Search_DPL((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"], DateTime.Parse(txtFrom.Text));
            DataTable dtVetting = objVet.VET_Get_VettingByPortCall();
            DataTable dtVetToolTip = objVet.VET_Get_ToolTipForPortCall();
            DataTable dtInspector = objVet.VET_Get_InspectorList();
            ViewState["dtVetting"] = dtVetting;
            ViewState["dtVetToolTip"] = dtVetToolTip;
            ViewState["dtInspector"] = dtInspector;
            ds.Relations.Add(new DataRelation("NestedCat", ds.Tables[1].Columns["Vessel_ID"], ds.Tables[0].Columns["Vessel_ID"]));

            ds.Tables[1].TableName = "Members";

            rpt1.DataSource = ds.Tables[1];
            rpt1.DataBind();
           
        }
        catch (Exception ex)
        {

        }

    }
    protected void rpt1_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        DataRowView dv = e.Item.DataItem as DataRowView;
        Image ImgCard = ((ImageButton)e.Item.FindControl("ImgView"));
        if (dv != null)
        {
            Repeater nestedRepeater = e.Item.FindControl("rpt2") as Repeater;
            if (nestedRepeater != null)
            {
               
                nestedRepeater.DataSource = dv.CreateChildView("NestedCat");
                nestedRepeater.DataBind();
            }
        }
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList myDDL = (DropDownList)e.Item.FindControl("DDLPort");
            Button btnSave = (Button)e.Item.FindControl("btnsave");
            DataTable dt = objPortCall.Get_PortCall_PortList(0, UDFLib.ConvertIntegerToNull(DataBinder.Eval(e.Item.DataItem, "Vessel_ID").ToString()),1);
            if (dt.Rows.Count > 0)
            {

                myDDL.DataSource = dt;
                myDDL.DataTextField = "Port_Name";
                myDDL.DataValueField = "Port_ID";
                myDDL.DataBind();
                if (uaEditFlag)
                    btnSave.Enabled = true;
            }
            else
            {
                myDDL.DataSource = dt;
                myDDL.DataTextField = "Port_Name";
                myDDL.DataValueField = "Port_ID";
                myDDL.DataBind();
                myDDL.Enabled = false;
                btnSave.Enabled = false;
            }
            Button GeneralButton = (Button)e.Item.FindControl("btnsave");  
           
        }

        if (rpt1 != null && rpt1.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                // Show the Error Label (if no data is present).
                Label lblErrorMsg = e.Item.FindControl("lblErrorMsg") as Label;
                if (lblErrorMsg != null)
                {
                    lblErrorMsg.Visible = true;
                }
            }
        }
    }
    protected void rpt2_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType != ListItemType.Header & e.Item.ItemType != ListItemType.Footer)
            {
                for (int i = 0; i <= e.Item.Controls.Count - 1; i++)
                {
                    System.Web.UI.WebControls.Literal obLiteral = e.Item.Controls[i] as System.Web.UI.WebControls.Literal;
                    if (obLiteral != null)
                    {
                        if (obLiteral.ID == "litRowStart")
                        {
                            lCurrentRecord += 1;
                            if ((lCurrentRecord == 1))
                            {
                                obLiteral.Text = "<tr>";
                            }
                            break;
                        }
                    }
                }
                for (int i = 0; i <= e.Item.Controls.Count - 1; i++)
                {
                    System.Web.UI.WebControls.Literal obLiteral = e.Item.Controls[i] as System.Web.UI.WebControls.Literal;
                    if (obLiteral != null)
                    {
                        if (obLiteral.ID == "litRowEnd")
                        {
                            if (lCurrentRecord % lRecordsPerRow == 0)
                            {
                                obLiteral.Text = "</tr>";
                                lCurrentRecord = 0;
                            }
                            break;
                        }
                    }
                }

                Image ImgCard = ((ImageButton)e.Item.FindControl("ImgView"));
                Image imgPurchasebtn = ((ImageButton)e.Item.FindControl("imgPurchasebtn"));
                Image imgPoAgencybtn = ((ImageButton)e.Item.FindControl("imgPoAgencybtn"));
                Image imgWorkList = ((ImageButton)e.Item.FindControl("imgWorkList"));
                Image imgAgencyWork = ((ImageButton)e.Item.FindControl("imgAgencyWork"));
                ImageButton ImgUpdate = ((ImageButton)e.Item.FindControl("ImgUpdate"));
                ImageButton ImgDelete = ((ImageButton)e.Item.FindControl("ImgDelete"));
                ImageButton Iswarrisk = ((ImageButton)e.Item.FindControl("Iswarrisk"));
                ImageButton IsShipCraneReq = ((ImageButton)e.Item.FindControl("IsShipCraneReq"));
                HtmlTableRow trArrival = ((HtmlTableRow)e.Item.FindControl("trArrival"));
                HtmlTableRow trDeparture = ((HtmlTableRow)e.Item.FindControl("trDeparture"));
                HtmlTableRow trBerthing = ((HtmlTableRow)e.Item.FindControl("trBerthing"));
                LinkButton lnkselect = (LinkButton)e.Item.FindControl("SelectButton");
                Label lblCharter = (Label)e.Item.FindControl("lblCharter");
                Label lblOwner = (Label)e.Item.FindControl("lblOwner");
               
                ListView lstVetting = (ListView)e.Item.FindControl("lstVetting");
   
               
                if (DataBinder.Eval(e.Item.DataItem, "IsWarRisk").ToString() == "1")
                {
                    Iswarrisk.Visible = true;

                }

                if (DataBinder.Eval(e.Item.DataItem, "IsShipCraneReq").ToString() == "1")
                {
                    IsShipCraneReq.Visible = true;

                }

                if (DataBinder.Eval(e.Item.DataItem, "Auto_Date").ToString() == "Y")
                {
                    if (uaEditFlag)
                    {
                        ImgUpdate.Visible = true;
                        trArrival.Attributes["class"] = "dvGray";
                        trDeparture.Attributes["class"] = "dvGray";
                        trBerthing.Attributes["class"] = "dvGray";
                    }
                }

                if (DataBinder.Eval(e.Item.DataItem, "CrewOn").ToString() != "0" || DataBinder.Eval(e.Item.DataItem, "CrewOffCount").ToString() != "0")
                {
                    ImgCard.Visible = true;

                }
                else
                {
                    ImgCard.Visible = false;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "Agency_Work").ToString()) > 0)
                {
                    imgAgencyWork.Visible = true;
                    imgAgencyWork.Style.Clear();
                    ImgDelete.Visible = false;
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
                if (Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "AgencyPo").ToString()) > 0)
                {
                    imgPoAgencybtn.Visible = true;
                }
                else
                {
                    imgPoAgencybtn.Visible = false;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "WorkList").ToString()) > 0)
                {
                    imgWorkList.Visible = true;
                }
                else
                {
                    imgWorkList.Visible = false;
                }
                if (Convert.ToInt16(DataBinder.Eval(e.Item.DataItem, "Port_ID").ToString()) == 0)
                {
                    lnkselect.BackColor = System.Drawing.Color.Yellow;
                }

                if (DataBinder.Eval(e.Item.DataItem, "Vetting_Planned_Count").ToString() != "0")
                {
                    DataTable dtVetting = (DataTable)ViewState["dtVetting"];

                    string filterpenexpression = "( Port_Call_ID=" + DataBinder.Eval(e.Item.DataItem, "Port_Call_ID").ToString() + " or  Port_ID=" + DataBinder.Eval(e.Item.DataItem, "Port_ID").ToString() + " )  and Vessel_ID=" + DataBinder.Eval(e.Item.DataItem, "Vessel_ID").ToString() + "";
                    DataRow[] drVetting = dtVetting.Select(filterpenexpression);

                    DataTable dtVetfiltered = drVetting.CopyToDataTable();

                    lstVetting.DataSource = dtVetfiltered;
                    lstVetting.DataBind();
                    lstVetting.Visible = true;

                }
                else
                {
                    lstVetting.Visible = false;
                }

            }
        }
        catch(Exception ex) {

            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        
        }

    }

    protected void lstVetting_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
       
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ImageButton imgPlannedVetting = ((ImageButton)e.Item.FindControl("imgPlannedVetting"));

            if (DataBinder.Eval(e.Item.DataItem, "Vetting_ID").ToString() != "0" || DataBinder.Eval(e.Item.DataItem, "Vetting_ID").ToString() != "")
            {
                DataTable dtVetToolTip = (DataTable)ViewState["dtVetToolTip"];

                string filterpenexpression = " Vetting_ID=" + DataBinder.Eval(e.Item.DataItem, "Vetting_ID").ToString();
                DataRow[] drVetting = dtVetToolTip.Select(filterpenexpression);
                if (drVetting.Length > 0)
                {
                    DataTable dtInspector = (DataTable)ViewState["dtInspector"];
                    string filterpenexpression1 = "";
                    if (drVetting[0]["IsInternal"] == "1")
                    {
                        filterpenexpression1 = " UserID='" + drVetting[0]["Inspector_ID"].ToString() + "_In'";
                    }
                    else
                    {
                        filterpenexpression1 = " UserID='" + drVetting[0]["Inspector_ID"].ToString() + "_Ex'";
                    }
                    DataRow[] drInspector = dtInspector.Select(filterpenexpression1);
                    string InspectorName = "";
                    if (drInspector.Length > 0)
                    {
                        InspectorName = drInspector[0][1].ToString().Split('_')[0].ToString();
                    }

                    string ToolTip = "Vetting Type:<b>" + drVetting[0]["Vetting_Type_Name"].ToString() + "</b><br>" + "Date :<b>" + UDFLib.ConvertUserDateFormat(Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "Vetting_Date").ToString()).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat()) + "</b><br>" + "Oil Major:<b>" + drVetting[0]["Oil_Major_Name"].ToString() + "</b><br>" + "Inspector:<b>" + InspectorName + "</b>";
                    imgPlannedVetting.Attributes.Add("onmouseover", "js_ShowToolTip('" + ToolTip + "',event,this)");
                }

            }
        }
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        Session["sVesselCode"] = DDLVessel.SelectedValues;
        Session["sFleet"] = DDLFleet.SelectedValues;
        BindPortCall();
       
       //iFrame1.Visible = false;
       Session["Port_ID"] = null;
       Session["Port_call_ID"] = null;
       Session["Vessel_ID"] = null;
       Session["Arrival"] = null;
       Session["PortCall"] = null;
       Session["FromDate"] = null;
       Session["sVesselCode"] = null;
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Session["Port_ID"] = null;
        Session["Port_call_ID"] = null;
        Session["Vessel_ID"] = null;
        Session["Arrival"] = null;
        Session["PortCall"] = null;
        Session["FromDate"] = null;
        BindFleetDLL();
        Load_VesselList();
        BindPortCall();
    }
    
    protected void DDLVessel_SelectedIndexChanged()
    {

        Session["sVesselCode"] = DDLVessel.SelectedValues;
        BindPortCall();
        
    }
    protected void lbtnDelete_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int Port_call_ID = UDFLib.ConvertToInteger(arg[0]);
        int Vessel_ID = UDFLib.ConvertToInteger(arg[1]);

        objPortCall.Del_PortCall_Details_DL(Convert.ToInt32(Port_call_ID), Vessel_ID, Convert.ToInt32(Session["USERID"].ToString()));
        BindPortCall();
       
       Response.Redirect(Request.RawUrl);
    }
    protected void rpt2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            int Port_call_ID = UDFLib.ConvertToInteger(arg[0]);
            int Vessel_ID = UDFLib.ConvertToInteger(arg[1]);

            objPortCall.Del_PortCall_Details_DL(Convert.ToInt32(Port_call_ID), Vessel_ID, Convert.ToInt32(Session["USERID"].ToString()));
            BindPortCall();
        }

    }
    protected void btnsave_Click(object s, EventArgs e)
    {
       
       
        Button myGeneralButton = (Button)s;
        int Vessel_ID = UDFLib.ConvertToInteger(myGeneralButton.CommandArgument);
        DropDownList myDDL = (DropDownList)myGeneralButton.NamingContainer.FindControl("DDLPort");
        int PortID = UDFLib.ConvertToInteger(myDDL.SelectedValue);
        string PortName = myDDL.SelectedItem.Text;
        objPortCall.Ins_Copy_PortCall_Details(UDFLib.ConvertToInteger(Vessel_ID), UDFLib.ConvertToInteger(PortID),PortName, Convert.ToInt32(Session["USERID"].ToString())); 
        BindPortCall();
      
        Response.Redirect(Request.RawUrl);
    }

    protected void btnvesselReport_Click(object s, CommandEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(e.CommandArgument.ToString());
       // Session["Vessel_ID"] = Vessel_ID.ToString();
        //string url = String.Format("OpenPopupWindow('VesselArrival', 'Vessel Movement', '../VesselMovement/PortCall_VesselArrival_Reports.aspx?ID="+ID + "','popup',800,1200,null,null,false,false,true,null);");

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", url, true);

        //Niesh Pawar
        //Mdf on:20062016
        ImageButton myGeneralButton = (ImageButton)s;
        int Vessel_ID = UDFLib.ConvertToInteger(myGeneralButton.CommandArgument);
        Session["Vessel_ID"] = Vessel_ID;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../Operations/Default.aspx', '_newtab' );", true);
    }



    protected void btnPrintDPL_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../VesselMovement/Port_DPL.aspx', '_newtab' );", true);
      
    }
    protected void btnPrevious_Click(object sender, CommandEventArgs e)
    {
        
        int Vessel_ID = UDFLib.ConvertToInteger(e.CommandArgument.ToString());
        objPortCall.Update_PortCall_Details_MoveDPLSeq(-1, Vessel_ID, Convert.ToInt32(Session["USERID"].ToString()));
        ChangeDateTime(-7);
        BindPortCall();
        btnFilter_Click(sender, e);
      //  Response.Redirect(Request.RawUrl);
    }
    protected void btnNext_Click(object sender, CommandEventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(e.CommandArgument.ToString());
        objPortCall.Update_PortCall_Details_MoveDPLSeq(1, Vessel_ID, Convert.ToInt32(Session["USERID"].ToString()));
        ChangeDateTime(7);
        BindPortCall();
        btnFilter_Click(sender, e);
    }
    public void BindPortCall_VesselID()
    {

    }
    protected void ChangeDateTime(int Days)
    {
        DateTime From = Convert.ToDateTime(txtFrom.Text);
        From = From.AddDays(Days);
        //This change has been done to change the date format as per user selection
        txtFrom.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(From));
        
        
    }
    protected void MoveDPLPortSeq(int Direction)
    { 
            
    }
    protected void lnkAuto_Click(object s, CommandEventArgs e)
    {
        string Autodate = null;
        LinkButton btn = (LinkButton)s;
        RepeaterItem Itemnew = (RepeaterItem)btn.NamingContainer;
        //Repeater item = (Repeater)btn.NamingContainer;
        LinkButton lnkAuto = (LinkButton)Itemnew.FindControl("lnkAuto");
        ImageButton ImgUpdate = (ImageButton)Itemnew.FindControl("ImgUpdate");
        string[] arg = e.CommandArgument.ToString().Split(',');
        int Port_call_ID = UDFLib.ConvertToInteger(arg[0]);
        int Vessel_ID = UDFLib.ConvertToInteger(arg[1]);
        if (lnkAuto.Text == "Auto Date is Off")
        {
            //ImgUpdate.Visible = false;
            lnkAuto.Text = "Auto Date is OFF";
            Autodate = "NO";
        }
        else
        {
            ImgUpdate.Visible = true;
            lnkAuto.Text = "Auto Date is ON";
            Autodate = "YES";
        }
        objPortCall.Update_PortCall_Details_AutoDate(Convert.ToInt32(Port_call_ID), Vessel_ID, Autodate, Convert.ToInt32(Session["USERID"].ToString()));

        BindPortCall();

    }

    protected void DDLFleet_SelectedIndexChanged()
    {
        Session["sFleet"] = DDLFleet.SelectedValues;
        Load_VesselList();
        Session["sVesselCode"] = null;
        Session["sVesselCode"] = DDLVessel.SelectedValues;
        BindPortCall();
    }

    protected void PortCallNotification_Click(object sender, EventArgs e)
    {
        Response.Redirect("../VesselMovement/Port_Call_Notification.aspx"); 
    }


    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void rpt1_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        ScriptManager scriptMan = ScriptManager.GetCurrent(this);
        ImageButton btn1 = e.Item.FindControl("btnPrevious") as ImageButton;
        ImageButton btn2 = e.Item.FindControl("btnNext") as ImageButton;
        if (btn1 != null)
        {
           
            scriptMan.RegisterPostBackControl(btn1);
        }
        if (btn2 != null)
        {

            scriptMan.RegisterPostBackControl(btn2);
        }
    }
}