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
using SMS.Properties;
using System.Text;


public partial class VesselMovement_PortCallVessel : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaAddFlag = false;
    public Boolean uaDeleteFlage = false;
    private long lCurrentRecord = 0;
    private long lRecordsPerRow = 200;
    public UserAccess objUA = new UserAccess();
    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();

    protected void Page_Load(object sender, EventArgs e)
    {
         UserAccessValidation();
       Page.ClientScript.RegisterStartupScript(this.GetType(), "", "myFunction();", true);

  

        if (!IsPostBack)
        {
            hdVesselId.Value = "";
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
            Session["Vid"] = null;
            Session["ID"] = null;
        }
 
        string msg1 = String.Format("try{{meFunction1();}}catch(exp){{}}");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);

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
            if (FleetDT.Rows.Count > 0)
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
        Session["PageURL"] = UDFLib.GetPageURL(Request.Path.ToUpper());
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);
        Session["Edit"] = objUA.Edit;
        Session["Delete"] = objUA.Delete;
        Session["Add"] = objUA.Add;
        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 1)
            uaAddFlag = true;
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
            DataTable dtVessel = objPortCall.Get_PortCall_VesselList((DataTable)Session["sFleet"], 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
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
                    Session[dtVessel.Rows[i]["Vessel_id"].ToString()] = null;
                }

            }
            Session["sVesselCode"] = DDLVessel.SelectedValues;

           

        }
        catch (Exception ex)
        {

        }


    }
    public void BindPortCall()
    {
        try
        {
            Session["FromDate"] = Convert.ToDateTime(txtFrom.Text.ToString());
            DataSet ds = objPortCall.Get_PortCall_Search_DPL((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"], Convert.ToDateTime(txtFrom.Text.ToString()));

            ds.Relations.Add(new DataRelation("NestedCat", ds.Tables[1].Columns["Vessel_ID"], ds.Tables[0].Columns["Vessel_ID"]));

            ds.Tables[1].TableName = "Members";

             StringBuilder strTable = new StringBuilder();
       // strTable.Append(" <div style='float: left; text-align: left; width: 100%; border: 1px solid #cccccc;font-family: Tahoma; font-size: 11px;  background-color: #ffffff; overflow-x:scroll' >");

             strTable.Append(" <table align='left' border-collapse: collapse;>");
       


        if (ds.Tables[1].Rows.Count > 0)
        {
             int i=0;
            foreach (DataRow dr in ds.Tables[1].Rows)
            {

                strTable.Append(" <tr style='outline: thin  #CCCCCC solid' >");
                //td  start
                strTable.Append("<td valign='top'  align='left'  >");
                
                //start
                strTable.Append(" <table width='180px' runat='server' id='table1' style='height: 100px; overflow:auto;'>");
                //1
                strTable.Append(" <tr>");
                strTable.Append("  <td  width='75%'  align='right' style='background-color:Menu; font-weight: bold'  colspan='2'>");
                strTable.Append("<label>");
                strTable.Append( dr["Vessel_Name"].ToString() );
                strTable.Append("</label >");
                strTable.Append("<input type='hidden' name='HiddenVesselID' value='" + dr["Vessel_ID"].ToString() + "'>");
                strTable.Append("</td>");
                strTable.Append("</tr>");
                //2
                strTable.Append(" <tr>");
                strTable.Append(" <td width='75%' style='color: Black;height: 20px;' align='right' colspan='2'>");
                strTable.Append(" Port Name :");
                strTable.Append("</td>");
                strTable.Append("</tr>");
                //3
                strTable.Append(" <tr>");
                strTable.Append(" <td width='75%' style='color: Black;height: 20px;' align='right' colspan='2'>");
                strTable.Append(" Arrival :");
                strTable.Append("</td>");
                strTable.Append("</tr>");
                //4
                strTable.Append(" <tr>");
                strTable.Append(" <td width='75%' style='color: Black;height: 20px;' align='right' colspan='2'>");
                strTable.Append(" Berthing :");
                strTable.Append("</td>");
                strTable.Append("</tr>");
                //5
                strTable.Append(" <tr>");
                strTable.Append(" <td width='75%' style='color: Black;height: 20px;' align='right' colspan='2'>");
                strTable.Append(" Departure :");
                strTable.Append("</td>");
                strTable.Append("</tr>");
                //6
                strTable.Append(" <tr>");
                strTable.Append("<td align=right>");
                strTable.Append("<table>");
                if (uaAddFlag == true)
                {
                strTable.Append("<tr>");
                strTable.Append("<td width='75%' style='color: Black;cursor:pointer;' align='right'  colspan='2'>");
                strTable.Append("<img Height='16px' src='../Images/Arrow2Left.png' ").Append("onclick='funPrev(").Append(ds.Tables[1].Rows[i]["Vessel_Id"]).Append(',').Append(Convert.ToInt32(Session["USERID"].ToString())).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>'); ; ;
                strTable.Append("</td>");
                strTable.Append("<td width='75%' style='color: Black;cursor:pointer;' align='right'  colspan='2'>");
                strTable.Append("<img Height='16px' src='../Images/Arrow2right.png'").Append("onclick='funNext(").Append(ds.Tables[1].Rows[i]["Vessel_Id"]).Append(',').Append(Convert.ToInt32(Session["USERID"].ToString())).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>'); ;
                strTable.Append("</td>");
                strTable.Append("</td>");
                strTable.Append("</tr>");
                strTable.Append("</table>");
                strTable.Append("</td>");
                strTable.Append("</tr>");
                
                    strTable.Append(" <tr>");
                    strTable.Append("<td align=right>");
                    strTable.Append("<table>");
                    strTable.Append("<tr>");
                    strTable.Append("<td width='75%' style='color: Black;cursor:pointer;'align='right'  colspan='2'>");
                    strTable.Append("<img Height='16px'  style='display:none;' src='../Images/Add-icon.png' ").Append("onclick='OpenScreen1(").Append(ds.Tables[1].Rows[i]["Vessel_Id"]).Append(',').Append("0").Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                    strTable.Append("</td>");
                    strTable.Append("<td width='75%' style='color: Black;cursor:pointer;' align='right'  colspan='2'>");
                   var vessel_name= dr["Vessel_Name"].ToString().Replace(" ","&nbsp;");
                   strTable.Append("<img Height='16px'  src='../Images/task-list.gif' title='Template'").Append("onclick=OpenTemplate(").Append("'").Append(ds.Tables[1].Rows[i]["Vessel_Id"].ToString()).Append("'").Append(',').Append("'").Append(vessel_name).Append("'").Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                    strTable.Append("</td>");
                    strTable.Append("</td>");
                    strTable.Append("</tr>");
                    strTable.Append("</table>");
                    strTable.Append("</td>");
                    strTable.Append("</tr>");

                 
                }
                else
                {
                    strTable.Append("<tr>");
                    strTable.Append("<td width='75%' style='color: Black;cursor:pointer;' align='right'  colspan='2'>");
                    strTable.Append("<img Height='16px' style='display:none;' src='../Images/Arrow2Left.png' ").Append("onclick='funPrev(").Append(ds.Tables[1].Rows[i]["Vessel_Id"]).Append(',').Append(Convert.ToInt32(Session["USERID"].ToString())).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>'); ; ;
                    strTable.Append("</td>");
                    strTable.Append("<td width='75%' style='color: Black;cursor:pointer;' align='right'  colspan='2'>");
                    strTable.Append("<img Height='16px' style='display:none;' src='../Images/Arrow2right.png'").Append("onclick='funNext(").Append(ds.Tables[1].Rows[i]["Vessel_Id"]).Append(',').Append(Convert.ToInt32(Session["USERID"].ToString())).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>'); ;
                    strTable.Append("</td>");
                    strTable.Append("</td>");
                    strTable.Append("</tr>");
                    strTable.Append("</table>");
                    strTable.Append("</td>");
                    strTable.Append("</tr>");
                    strTable.Append(" <tr>");
                    strTable.Append("<td align=right>");
                    strTable.Append("<table>");
                    strTable.Append("<tr>");
                    strTable.Append("<td width='75%' style='color: Black;cursor:pointer;'align='right'  colspan='2'>");
                    strTable.Append("<img Height='16px' style='display:none;'  src='../Images/Add-icon.png' ").Append("onclick='OpenScreen1(").Append(ds.Tables[1].Rows[i]["Vessel_Id"]).Append(',').Append("0").Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                    strTable.Append("</td>");
                    strTable.Append("<td width='75%' style='color: Black;cursor:pointer;' align='right'  colspan='2'>");
                    strTable.Append("<img Height='16px' style='display:none;' src='../Images/task-list.gif' title='Template' ").Append("onclick=OpenTemplate(").Append("'").Append(ds.Tables[1].Rows[i]["Vessel_Id"].ToString()).Append("'").Append(',').Append("'").Append(dr["Vessel_Name"].ToString()).Append("'").Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                    strTable.Append("</td>");
                    strTable.Append("</td>");
                    strTable.Append("</tr>");
                    strTable.Append("</table>");
                    strTable.Append("</td>");
                    strTable.Append("</tr>");

                   
                }

                strTable.Append(" <tr>");
                strTable.Append("<td align=right>");
                strTable.Append("<table>");
                strTable.Append("<tr>");
                strTable.Append("<td width='75%' style='color: Black;cursor:pointer;' align='right'  colspan='2'>");
                strTable.Append("<img Height='16px' src='../Images/crew-on.png' title='Crew List' ").Append("onclick=OpenCrewList(").Append("'").Append(ds.Tables[1].Rows[i]["Vessel_Short_Name"].ToString()).Append("'").Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                strTable.Append("</td>");
                strTable.Append("<td width='75%' style='color: Black;cursor:pointer;' align='right'  colspan='2'>");
                if (uaEditFlag == true)
                {
                    strTable.Append("<img Height='16px' src='../Images/table-icon.png' title='Vessel Report' ").Append("onclick='OpenVesselArrival(").Append(ds.Tables[1].Rows[i]["Vessel_Id"]).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                }
                strTable.Append("</td>");
                strTable.Append("</td>");
                strTable.Append("</tr>");
                strTable.Append("</table>");
                strTable.Append("</td>");
                strTable.Append("</tr>");
                //7
                strTable.Append(" <tr>");
              
                strTable.Append("</td>");
                strTable.Append("</tr>");
                //8
                strTable.Append("<tr>");
                strTable.Append("<td>");
                int? Vessel_ID = UDFLib.ConvertIntegerToNull(dr["Vessel_ID"].ToString());
                hdVesselId.Value = hdVesselId.Value + "," + Vessel_ID.ToString();
                hdVesselId.Value = hdVesselId.Value.Trim(',');
                strTable.Append("</td>");
                strTable.Append("</tr>");

                //end
                strTable.Append("</table>");
              
                strTable.Append("</td>");
            
                strTable.Append("<td style='color: Black;'  height='260px'  align='left'  colspan='2'>");
                strTable.Append("<div  ID='" + "dynamicPortDiv_" + dr["Vessel_ID"].ToString() + "'>");
                strTable.Append("</div>");
                strTable.Append("</td>");
              
                
              
             
                i++;
            }
        }
       
        strTable.Append("</table>");
      //  strTable.Append("</div>");
       divLiteral.Text = strTable.ToString();
      
        }
        catch (Exception ex)
        {

        }

    }
   
   


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        hdVesselId.Value = "";
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
        hdVesselId.Value = "";
        Session["sVesselCode"] = null;
        Session["sVesselCode"] = DDLVessel.SelectedValues;
       
        BindPortCall();

    }
    [System.Web.Services.WebMethod]
    protected void DeletePort(int portId, int vesselid, int userid)
    {
        objPortCall.Del_PortCall_Details_DL(Convert.ToInt32(portId), vesselid, Convert.ToInt32(userid));
    }
    protected void lbtnDelete_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int Port_call_ID = UDFLib.ConvertToInteger(arg[0]);
        int Vessel_ID = UDFLib.ConvertToInteger(arg[1]);

        objPortCall.Del_PortCall_Details_DL(Convert.ToInt32(Port_call_ID), Vessel_ID, Convert.ToInt32(Session["USERID"].ToString()));
        BindPortCall();

       
    }

    protected void btnsave_Click(object s, EventArgs e)
    {
        Button myGeneralButton = (Button)s;
            int Vessel_ID = UDFLib.ConvertToInteger(myGeneralButton.CommandArgument);
            
            DropDownList myDDL = (DropDownList)myGeneralButton.NamingContainer.FindControl("DDLPort");
            int PortID = UDFLib.ConvertToInteger(myDDL.SelectedValue);
            string PortName = myDDL.SelectedItem.Text;
            objPortCall.Ins_Copy_PortCall_Details(UDFLib.ConvertToInteger(Vessel_ID), UDFLib.ConvertToInteger(PortID), PortName, Convert.ToInt32(Session["USERID"].ToString()));
             BindPortCall();
          
     //  Response.Redirect(Request.RawUrl);
        //Vessel_ID = 0;
        //Page.MaintainScrollPositionOnPostBack = true;
    }

 
    protected void btnvesselReport_Click(object s, CommandEventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(e.CommandArgument.ToString());
        Session["Vessel_ID"] = Vessel_ID.ToString();
        string url = String.Format("OpenPopupWindow('VesselArrival', 'Vessel Movement', '../VesselMovement/PortCall_VesselArrival_Reports.aspx'" + ",'popup',800,1200,null,null,false,false,true,null);");

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", url, true);
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
       // BindPortCall();
       // btnFilter_Click(sender, e);
        //  Response.Redirect(Request.RawUrl);
    }
    protected void btnNext_Click(object sender, CommandEventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(e.CommandArgument.ToString());
        objPortCall.Update_PortCall_Details_MoveDPLSeq(1, Vessel_ID, Convert.ToInt32(Session["USERID"].ToString()));
        ChangeDateTime(7);
       // BindPortCall();
        //btnFilter_Click(sender, e);
    }
    public void BindPortCall_VesselID()
    {

    }
    protected void ChangeDateTime(int Days)
    {
        DateTime From = Convert.ToDateTime(txtFrom.Text);
        From = From.AddDays(Days);
        txtFrom.Text = From.ToString("dd-MM-yyyy");
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



    public void btnRaisePostBack_click(object sender, EventArgs e)
    {

    }
}