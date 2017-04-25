using System;
using System.Web;
using System.Data;
using System.Web.Services;
using SMS.Data.Infrastructure;
using SMS.Business.CP;
using System.Collections.Generic;
using System.Text;
using SMS.Business.VM;
using System.Globalization;


public partial class CPService
{


    [WebMethod]
    public string CreateTableDPLPort(int Vessel_Id,int user)
    {
        BLL_VM_PortCall VMPortCall = new BLL_VM_PortCall();
        StringBuilder strTable = new StringBuilder();
        strTable.Append("<div style='overflow: auto'><table CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse; margin-top:10px;' >");
        strTable.Append("<tr >");
      
        DataTable dt = VMPortCall.GET_Port_Call_DPL_Details(@Vessel_Id);

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {

                //strTable.Append("<td valign='top' align='center'  style='width:150px;height:220px;'>");
                strTable.Append("<td>");
                strTable.Append("<table   style='width:120px;border-collapse:collapse; border:1px solid #cccccc;'>");
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
             //   strTable.Append("<a onclick='OpenPCallDetail((''" + dr["Port_Call_ID"] + "'),('" + dr["Vessel_Id"] + "'),('" + dr["Port_Id"] + "'));>");
                strTable.Append("<label style='cursor:pointer'  onclick='OpenPCallDetail(").Append(dr["Port_Call_ID"]).Append(',').Append(dr["Vessel_Id"]).Append(',').Append(dr["Port_Id"]).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                //strTable.Append("<a onclick='OpenPCallDetail('" + dr["Port_Call_ID"] + "'),('" + dr["Vessel_Id"] + "'),('" + dr["Port_Id"] + "')'");
                strTable.Append(dr["Port_Name"].ToString());
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
                    strTable.Append(dr["CSUPPLIER"].ToString());
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
                    strTable.Append(dr["OSUPPLIER"].ToString());
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
                    strTable.Append(dr["Port_Remarks"].ToString());
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
                if (dr["CrewOn"].ToString() == "1" || dr["CrewOffCount"].ToString()=="1")
                {
                    strTable.Append(" <img ForeColor='Black' src='../Images/CrewChange.bmp' title='CrewOnOff' Height='16px'>");
                }
                else
                {
                    strTable.Append("");
                }
                strTable.Append("</td>");
                strTable.Append("</tr>");
               
                strTable.Append("<tr style='width:125px; height:22px;' >");
                strTable.Append("<td valign='top' align='center' style='width:125px; height:22px;' >");
                strTable.Append(" <input type='image' ForeColor='Black' src='../Images/Edit.gif' title='Edit' Height='16px'").Append("onclick='OpenScreen1(").Append(dr["Vessel_Id"]).Append(',').Append(dr["Port_Call_ID"]).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                strTable.Append(" <input type='image' ForeColor='Black' src='../Images/delete.png' title='Delete' Height='16px'").Append("onclick='Deleteport(").Append(dr["Port_Call_ID"]).Append(',').Append(dr["Vessel_Id"]).Append(',').Append(user).Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                //strTable.Append(" <input type='image' ForeColor='Black' src='../Images/delete.png' title='Delete' Height='16px'").Append("OnClick='return ").Append(" confirm").Append("(").Append(")").Append('\'').Append("onclick='Deleteport(").Append(dr["Port_Id"]).Append(',').Append(dr["Vessel_Id"]).Append(',').Append("625").Append(')').Append(";").Append("return ").Append("false").Append(";").Append('\'').Append('>');
                strTable.Append("</td>");
                strTable.Append("</tr>");
                strTable.Append("</table >");
                strTable.Append("</td>");
           
              
            }
        }
        strTable.Append("</tr>");
        strTable.Append("</table></div>");

        return strTable.ToString();

        //return "Bikash";
    }

    [WebMethod]
    public void DeletePort(int portId, int vesselid, int userid)
    {
        BLL_VM_PortCall VMPortCall = new BLL_VM_PortCall();
        VMPortCall.Del_PortCall_Details_DL(Convert.ToInt32(portId), vesselid, Convert.ToInt32(userid));
    }

}
