using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.ServiceModel;
using System.Web.Services;
using SMS.Business.VM;
using System.Data;
using System.ComponentModel;
using SMS.Business.Infrastructure;
using SMS.Properties;

[WebService(Namespace = "VMService")]
//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class VMService : System.Web.Services.WebService
{
    [WebMethod (EnableSession=true)]
    public string CreateTableDPLPort(int Vessel_Id, int user)
    {
        BLL_VM_PortCall VMPortCall = new BLL_VM_PortCall();
        StringBuilder strTable = new StringBuilder();
        //strTable.Append("<div style='overflow: auto'><table CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse;border:1px solid #cccccc; margin-top:10px;' >");
        strTable.Append("<div style='overflow: auto'><table CELLPADDING='2' CELLSPACING='0'  >");
        strTable.Append("<tr >");
        

        //User Access

        //string pageurl = Session["PageURL"].ToString();
        // UserAccess objUA = new UserAccess();
        //BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        //objUA = objUser.Get_UserAccessForPage(user, pageurl);

        DataTable dt = VMPortCall.GET_Port_Call_DPL_Details(@Vessel_Id);
        int inc = 0;
        if (dt.Rows.Count > 0)
        {

            foreach (DataRow dr in dt.Rows)
            {

                //strTable.Append("<td valign='top' align='center'  style='width:150px;height:220px;'>");
                strTable.Append("<td>");
                strTable.Append("<table   style='width:120px; border-collapse:collapse; border:1px solid #cccccc;'>");
                strTable.Append("<tr style='width:125px;'  >");
                strTable.Append("<td valign='top' align='center' style='width:125px;' >");
                strTable.Append("");
                strTable.Append("</td>");
                strTable.Append("</tr>");

                strTable.Append("<tr style='width:125px;height:22px;' >");
                strTable.Append("<td valign='top' align='center' style='width:125px;height:20px;' >");
                strTable.Append("<u>");
                strTable.Append("<b>");
                strTable.Append("<font  color='#4566cc'>");
                if (dr["Port_Id"].ToString() == "0")
                {
                    strTable.Append("<label style='cursor:pointer; background-color: yellow;'  onclick='OpenPCallDetail(").Append(dr["Port_Call_ID"]).Append(',').Append(dr["Vessel_Id"]).Append(',').Append(dr["Port_Id"]).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');

                }
                else
                {
                    strTable.Append("<label style='cursor:pointer'  onclick='OpenPCallDetail(").Append(dr["Port_Call_ID"]).Append(',').Append(dr["Vessel_Id"]).Append(',').Append(dr["Port_Id"]).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                }
                if (dr["Port_Name"].ToString().Length < 15)
                {
                    strTable.Append(dr["Port_Name"].ToString());
                }
                else
                {
                    strTable.Append(dr["Port_Name"].ToString().Substring(0, 12) + "...");
                }
                strTable.Append("</label >");
                strTable.Append("</font>");
                strTable.Append("</b>");
                strTable.Append("</u>");
                strTable.Append("</td>");
                strTable.Append("</tr>");

                if (dr["Auto_Date"].ToString() == "Y")
                {
                    strTable.Append("<div style='background-color: #e0e0e0;'>");
                    strTable.Append("<tr bgcolor='#e0e0e0' style='width:125px;height:22px;' >");

                    strTable.Append("<td valign='top' bgcolor='#e0e0e0' align='center' style='width:125px; height:17px;'>");
                    strTable.Append(dr["Arrival"].ToString());
                    strTable.Append("</td>");
                    strTable.Append("</tr>");
                    strTable.Append("<tr bgcolor='#e0e0e0' style='width:125px;height:22px;' >");
                    strTable.Append("<td valign='top' bgcolor='#e0e0e0' align='center' style='width:125px; height:17px;'>");
                    strTable.Append(dr["Berthing"].ToString());
                    strTable.Append("</td>");
                    strTable.Append("</tr>");
                    strTable.Append("<tr bgcolor='#e0e0e0' style='width:125px;height:22px;' >");
                    strTable.Append("<td valign='top' bgcolor='#e0e0e0' align='center' style='width:125px; height:17px;'>");
                    strTable.Append(dr["Departure"].ToString());
                    strTable.Append("</td>");
                    strTable.Append("</tr>");
                    strTable.Append("</div>");
                }
                else
                {
                    strTable.Append("<tr style='width:125px;height:22px;' >");

                    strTable.Append("<td valign='top'  align='center' style='width:125px; height:17px;'>");
                    strTable.Append(dr["Arrival"].ToString());
                    strTable.Append("</td>");
                    strTable.Append("</tr>");
                    strTable.Append("<tr style='width:125px;height:22px;' >");
                    strTable.Append("<td valign='top'  align='center' style='width:125px; height:17px;'>");
                    strTable.Append(dr["Berthing"].ToString());
                    strTable.Append("</td>");
                    strTable.Append("</tr>");
                    strTable.Append("<tr style='width:125px;height:22px;' >");
                    strTable.Append("<td valign='top'  align='center' style='width:125px; height:17px;'>");
                    strTable.Append(dr["Departure"].ToString());
                    strTable.Append("</td>");
                    strTable.Append("</tr>");
                }

                strTable.Append("<tr style='width:125px; height:22px;' >");
                strTable.Append("<td valign='top' align='center' style='width:125px; height:17px;' >");
                if (dr["IsWarRisk"].ToString() == "1")
                {

                    strTable.Append(" <img ForeColor='Black' src='../Images/Soldier.png' title='WarRisk' Height='16px'>");

                }
                else
                {
                    strTable.Append("");
                }

                if (dr["IsShipCraneReq"].ToString() == "1")
                {

                    strTable.Append(" <img ForeColor='Black' src='../Images/shipcrane2.png' title='ShipCraneReq' Height='16px'>");

                }
                else
                {
                    strTable.Append("");
                }

                strTable.Append("</td>");
                strTable.Append("</tr>");

                strTable.Append("<tr style='width:125px; height:22px;' >");
                strTable.Append("<td valign='top' align='center' style='width:125px; height:17px;' >");

                if (!string.IsNullOrEmpty(dr["CSUPPLIER"].ToString()))
                {
                    strTable.Append("<font color='Green'>");
                    if (dr["CSUPPLIER"].ToString().Length < 15)
                    {
                        strTable.Append(dr["CSUPPLIER"].ToString());
                    }
                    else
                    {
                        strTable.Append(dr["CSUPPLIER"].ToString().Substring(0, 12) + "...");
                    }
                    strTable.Append("</font>");
                }
                else
                {
                    strTable.Append("");
                }
                strTable.Append("</td>");
                strTable.Append("</tr>");

                strTable.Append("<tr style='width:125px; height:22px;' >");
                strTable.Append("<td valign='top' align='center' style='width:125px; height:17px;' >");

                if (!string.IsNullOrEmpty(dr["OSUPPLIER"].ToString()))
                {
                    strTable.Append("<font color='BlueViolet'>");
                    if (dr["OSUPPLIER"].ToString().Length < 15)
                    {
                        strTable.Append(dr["OSUPPLIER"].ToString());
                    }
                    else
                    {
                        strTable.Append(dr["OSUPPLIER"].ToString().Substring(0, 12) + "...");
                    }
                    strTable.Append("</font>");
                }
                else
                {
                    strTable.Append("");
                }
                strTable.Append("</td>");
                strTable.Append("</tr>");


                strTable.Append("<tr style='width:125px; height:22px;' >");
                strTable.Append("<td valign='top' align='center' style='width:125px; height:17px;' >");

                if (!string.IsNullOrEmpty(dr["Port_Remarks"].ToString()))
                {
                    strTable.Append("<font color='Brown'>");
                    if (dr["Port_Remarks"].ToString().Length < 15)
                    {
                        strTable.Append(dr["Port_Remarks"].ToString());
                    }
                    else
                    {
                        strTable.Append(dr["Port_Remarks"].ToString().Substring(0, 12) + "...");
                    }
                    strTable.Append("</font>");
                }
                else
                {
                    strTable.Append("");
                }
                strTable.Append("</td>");
                strTable.Append("</tr>");


                strTable.Append("<tr style='width:125px; height:22px;' >");
                strTable.Append("<td valign='top' align='center' style='width:125px; height:17px;' >");
                if (dr["PO"].ToString() == "1")
                {
                    strTable.Append(" <img ForeColor='Black' src='../Images/supply_icon.jpg' title='Purchase Order' Height='16px'>");
                }
                else
                {
                    strTable.Append("");
                }
                if (dr["Agency_work"].ToString() == "1")
                {
                    strTable.Append(" <img ForeColor='Black' src='../Images/Agency_PO.jpg' title='Agency Work' Height='16px'>");
                }
                else
                {
                    strTable.Append("");
                }
                if (dr["WorkList"].ToString() == "1")
                {
                    strTable.Append(" <img ForeColor='Black' src='../Images/alert.jpg' title='Work List' Height='16px'>");
                }
                else
                {
                    strTable.Append("");
                }
                if (dr["CrewOn"].ToString() == "1" || dr["CrewOffCount"].ToString() == "1")
                {
                    strTable.Append(" <img ForeColor='Black' src='../Images/CrewChange.bmp' title='CrewOnOff' Height='16px'").Append("onclick=' OpenCrewOnOff(").Append(dr["Port_Call_ID"]).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                }
                else
                {
                    strTable.Append("");
                }
                strTable.Append("</td>");
                strTable.Append("</tr>");

                strTable.Append("<tr style='width:125px; height:22px;' >");

                strTable.Append("<td valign='top' align='center' style='width:125px; height:22px;' >");
                
                if (Convert.ToInt32(Session["Edit"].ToString()) == 1 )
                {
                    strTable.Append(" <input type='image' ForeColor='Black' src='../Images/Edit.gif' title='Edit' Height='16px'").Append("onclick='OpenScreen1(").Append(dr["Vessel_Id"]).Append(',').Append(dr["Port_Call_ID"]).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                }
                else
                {
                    strTable.Append(" <input type='image' style='display:None' ForeColor='Black' src='../Images/Edit.gif' title='Edit' Height='16px'").Append("onclick='OpenScreen1(").Append(dr["Vessel_Id"]).Append(',').Append(dr["Port_Call_ID"]).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                }
                if (Convert.ToInt32(Session["Delete"].ToString()) == 1)
                {
                    strTable.Append(" <input type='image' ForeColor='Black' src='../Images/delete.png' title='Delete' Height='16px'").Append("onclick='Deleteport(").Append(dr["Port_Call_ID"]).Append(',').Append(dr["Vessel_Id"]).Append(',').Append(user).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');

                }
                else
                {
                    strTable.Append(" <input type='image' style='display:None' ForeColor='Black' src='../Images/delete.png' title='Delete' Height='16px'").Append("onclick='Deleteport(").Append(dr["Port_Call_ID"]).Append(',').Append(dr["Vessel_Id"]).Append(',').Append(user).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                }
                strTable.Append("</td>");
                strTable.Append("</tr>");
              
                strTable.Append("</table >");
                strTable.Append("</td>");


            }

            strTable.Append("<td>");
            strTable.Append("<table>");
            strTable.Append("<tr>");
            strTable.Append("<td>");
            strTable.Append("</td>");
            strTable.Append("<td>");
            if (Convert.ToInt32(Session["Add"].ToString()) == 1)
            {
                strTable.Append("<input type=button   class=buttonnew id=btnAddPort_" + @Vessel_Id + " onclick=OpenScreen1(" + Vessel_Id + ") />");
                //strTable.Append("<input type='image' ForeColor='Black' src='../Images/vmaddnew.png' title='Add new Port' id=btnAddPort_" + @Vessel_Id + " onclick=OpenScreen1(" + Vessel_Id + ") />");
            }
            else
            {
                strTable.Append("<input type=button  style='display:none;' class=buttonnew id=btnAddPort_" + @Vessel_Id + " onclick=OpenScreen1(" + Vessel_Id + ") />");
                //  strTable.Append("<input type='image' ForeColor='Black' src='../Images/vmaddnew.png' style='display:none;' id=btnAddPort_" + @Vessel_Id + " onclick=OpenScreen1(" + Vessel_Id + ") />");
            }
            strTable.Append("<td>");
            strTable.Append("</tr>");
            strTable.Append("<tr>");
            strTable.Append("<td colspan='2'>");
            strTable.Append("</td>");
            strTable.Append("</tr>");
            //Bind dropdown
            strTable.Append("<tr>");
            strTable.Append("<td>");
            int PortID = 0;
            DataTable dtable = VMPortCall.Get_PortCall_PortList(0, UDFLib.ConvertIntegerToNull(@Vessel_Id), 1);
            if (dtable.Rows.Count > 0)
            {
                if (Convert.ToInt32(Session["Add"].ToString()) == 1)
                {
                    strTable.Append("<select id=ddlPort_" + @Vessel_Id + ">");
                    inc++;


                    for (int i = 0; i < dtable.Rows.Count; i++)
                    {
                        if (dtable.Rows[i]["Port_Name"].ToString().Length < 12)
                            strTable.Append("<option value=" + dtable.Rows[i]["Port_ID"] + ">" + dtable.Rows[i]["Port_Name"].ToString() + "</option>");
                        else
                            strTable.Append("<option value=" + dtable.Rows[i]["Port_ID"] + ">" + dtable.Rows[i]["Port_Name"].ToString().Substring(0, 10) + "</option>");
                    }
                }
                else
                {

                    strTable.Append("<select style='display:none' id=ddlPort_" + @Vessel_Id + ">");

                }
            }
            else
            {
                if (Convert.ToInt32(Session["Add"].ToString()) == 1)
                {
                    strTable.Append("<select disabled id=ddlPort_" + @Vessel_Id + ">");
                }
                else
                {
                    strTable.Append("<select style='display:none' id=ddlPort_" + @Vessel_Id + ">");
                }
            }
            strTable.Append("</select>");
            strTable.Append("</td>");

            strTable.Append("<td>");
            if (dtable.Rows.Count > 0)
            {
                if (Convert.ToInt32(Session["Add"].ToString()) == 1)
                {
                    strTable.Append("<input type=button class=buttonthis  id=btnSave_" + @Vessel_Id + " onclick=AddThisPort(" + Vessel_Id + ") />");
                    //   strTable.Append("<input type='image' ForeColor='Black' src='../Images/vmaddthis.png' id=btnSave_" + @Vessel_Id + " onclick=AddThisPort(" + Vessel_Id + ") />");
                }
                else
                {
                    strTable.Append("<input type=button style='display:none' class=buttonthis id=btnSave_" + @Vessel_Id + " onclick=AddThisPort(" + Vessel_Id + ") />");
                    //  strTable.Append("<input type='image' ForeColor='Black' src='../Images/vmaddthis.png' style=' display:none'/>");
                }
            }
            else
            {
                if (Convert.ToInt32(Session["Add"].ToString()) == 1)
                {
                    // strTable.Append("<input type=button value='Add This port' disabled />");
                    strTable.Append("<input type='image' ForeColor='Black' src='../Images/vmaddthisd.png' disabled/>");
                }
                else
                {
                    //  strTable.Append("<input type=button value='Add This port'  style='display:none' />");
                    strTable.Append("<input type='image' ForeColor='Black' src='../Images/vmaddthis.png' style=' display:none'/>");
                }
            }
            strTable.Append("</td>");
            strTable.Append("</tr>");
            strTable.Append("</table>");
            strTable.Append("</td>");

        }
        else
        {
            strTable.Append("<td>");
            if (Convert.ToInt32(Session["Add"].ToString()) == 1)
            {
                strTable.Append("<input type=button class=buttonnew id=btnAddPort_" + @Vessel_Id + " onclick=OpenScreen1(" + Vessel_Id + ") />");
                //  strTable.Append("<input type='image' ForeColor='Black' src='../Images/vmaddnew.png' title='Add new Port' id=btnAddPort_" + @Vessel_Id + " onclick=OpenScreen1(" + Vessel_Id + ") />");
            }
            else
            {
                strTable.Append("<input type=button class=buttonnew style='display:none;' id=btnAddPort_" + @Vessel_Id + " onclick=OpenScreen1(" + Vessel_Id + ") />");
                // strTable.Append("<input type='image' style='display:none;'  ForeColor='Black' src='../Images/vmaddnew.png' title='Add new Port' id=btnAddPort_" + @Vessel_Id + " onclick=OpenScreen1(" + Vessel_Id + ") />");
            }
            strTable.Append("</td>");
            strTable.Append("<td>");
            strTable.Append("<table>");
            strTable.Append("<tr>");
            strTable.Append("<td>");
            if (Convert.ToInt32(Session["Add"].ToString()) == 1)
            {
                strTable.Append("<select  id=ddlPort_" + @Vessel_Id + " disabled>");
            }
            else
            {
                strTable.Append("<select  id=ddlPort_" + @Vessel_Id + " style='display:none'>");
            }
            strTable.Append("</td>");
            strTable.Append("<td>");
            if (Convert.ToInt32(Session["Add"].ToString()) == 1)
            {
                //   strTable.Append("<input type=button value='Add This port' disabled />");
                strTable.Append("<input type='image' ForeColor='Black' src='../Images/vmaddthisd.png' disabled />");
            }
            else
            {
                //  strTable.Append("<input type=button value='Add This port'  style='display:none' />");
                strTable.Append("<input type='image' ForeColor='Black' src='../Images/vmaddthis.png' style='display:none'  />");
            }
            strTable.Append("</td>");
            strTable.Append("</tr>");
            strTable.Append("</table>");
            strTable.Append("</td>");
        }
        strTable.Append("</tr>");
        strTable.Append("</table>");
        strTable.Append("</div>");

        return strTable.ToString();

    }

    [WebMethod]
    public void DeletePort(int portId, int vesselid, int userid)
    {
        BLL_VM_PortCall VMPortCall = new BLL_VM_PortCall();
        VMPortCall.Del_PortCall_Details_DL(Convert.ToInt32(portId), vesselid, Convert.ToInt32(userid));
    }
    [WebMethod]
    public void AddThisPort(int portId, int vesselid, string portName, int userid)
    {
       
        BLL_VM_PortCall VMPortCall = new BLL_VM_PortCall();
        VMPortCall.Ins_Copy_PortCall_Details(UDFLib.ConvertToInteger(vesselid), UDFLib.ConvertToInteger(portId), portName, Convert.ToInt32(userid));
      
    }

   [WebMethod(EnableSession = true)]
    public void funNext(int vesselid, int userid)
    {
        BLL_VM_PortCall VMPortCall = new BLL_VM_PortCall();
        VMPortCall.Update_PortCall_Details_MoveDPLSeq(1, vesselid, userid);
        CreateTableDPLPort(vesselid, userid);
    }

[WebMethod(EnableSession = true)]
    public void funPrev(int vesselid, int userid)
    {
        BLL_VM_PortCall VMPortCall = new BLL_VM_PortCall();
        VMPortCall.Update_PortCall_Details_MoveDPLSeq(-1, vesselid, userid);
        CreateTableDPLPort( vesselid, userid);
    }

     //[WebMethod]
     //public string VM_Get_DPL_Count(int vesselid)
     //{
     //    BLL_VM_PortCall VMPortCall = new BLL_VM_PortCall();
     //  return   VMPortCall.VM_Get_DPL_Count(vesselid).ToString();
     //}
 
}

