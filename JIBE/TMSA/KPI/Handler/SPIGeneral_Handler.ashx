<%@ WebHandler Language="C#" Class="SPIGeneral_Handler" %>

using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using SMS.Business.TMSA;

public class SPIGeneral_Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {

        //string list = context.Request["list"];
       // string query = context.Request["query"];

        string strOut = "";

        try
        {
            
            BLL_TMSA_KPI objBLL = new BLL_TMSA_KPI();
            DataSet dt;
            
                dt = objBLL.Get_SPIList();
          
                int counter = 1;
            string sugg = "<table width='100%' style=' padding-right:5px;'>";
            string data = "";
            string a = ""; 
            DataTable table1 = dt.Tables[0];
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {
                var img2 = "";
                if (table1.Rows[i]["Name"].ToString() == "Environmental Performance")
                    img2 = "Resource/Enviromental.png";
                //if (table1.Rows[i]["Name"].ToString() == "HR Management Performance")
                //    img2 = "Resource/HRManagement.png";
                //if (table1.Rows[i]["Name"].ToString() == "Navigational Safety Performance")
                //    img2 = "Resource/NavigationalSafety.png";
                //if (table1.Rows[i]["Name"].ToString() == "Technical Performance")
                //    img2 = "Resource/Technical.png";
                if (table1.Rows[i]["Name"].ToString() == "Health and Safety Management and Performance")
                    img2 = "Resource/HealthSafety.png";
                
                if (counter % 2 == 1)
                {

                    sugg += "<tr><td class='style1'><div class='KPItext'>" + table1.Rows[i]["Name"].ToString() + "</div><div><img src='" + img2 + "' title='" + table1.Rows[i]["Name"].ToString() + "' /></div>";
                    sugg += "<div class='Desc' onclick='SelectTab(" + table1.Rows[i]["SPI_ID"] + ");'><b>Description :</b>" + table1.Rows[i]["Description"].ToString() + "<a class='aStyle' href='javascript:SelectTab(" + table1.Rows[i]["SPI_ID"] + ")'> Show KPI...</a></div></td>";
                    
                }
                if (counter % 2 == 0)
                {
                    sugg += "<td class='style1'><div class='KPItext'>" + table1.Rows[i]["Name"].ToString() + "</div><div><img src='" + img2 + "'  title='" + table1.Rows[i]["Name"].ToString() + "' /></div>";
                    sugg += "<div class='Desc' onclick='SelectTab(" + table1.Rows[i]["SPI_ID"] + ");'><b>Description :</b>" + table1.Rows[i]["Description"].ToString() + "<a class='aStyle' href='javascript:SelectTab(" + table1.Rows[i]["SPI_ID"] + ")'> Show KPI...</a></div></td></tr><tr><td colspan='2'></td></tr>";
                    
                }
                counter += 1;
                data = "<table width='100%' ><tr><td class='KPIheader' colspan='2'>" + table1.Rows[i]["Name"].ToString() + "</td></tr><tr><td></td><td><table width='100%' ><tr><td class='SPI' colspan='2'><div><img src='" + img2 + "' id='Img8' title='" + table1.Rows[i]["Name"].ToString() + "' /></div></td></tr>";
                data += "<tr><td colspan='2'></td></tr><tr><td class='SPItext'>Description :</td><td>" + table1.Rows[i]["Description"].ToString() + "</td></tr><tr><td colspan='2'></td></tr><tr><td class='SPItext'>Formula :</td><td>" + table1.Rows[i]["Context"].ToString() + "</td></tr>";
                data += "<tr><td colspan='2'></td></tr><tr><td class='KPIheader' colspan='2'>KPI</td></tr><tr><td colspan='2'></td></tr>";
                DataTable dtChild = dt.Tables[1];
                dtChild.DefaultView.RowFilter = "SPI_ID= '" + table1.Rows[i]["SPI_ID"].ToString() + "'";
                if (dtChild.DefaultView.Count > 0)
                {
                    for (int j = 0; j < dtChild.DefaultView.Count; j++)
                    {
                        data += "<tr><td class='stylw2' colspan='2'>" + dtChild.DefaultView[j]["Name"].ToString() + "</td></tr>";
                        data += ProcessRequest(int.Parse(dtChild.DefaultView[j]["KPI_ID"].ToString())) + "<tr><td colspan='2'><hr/></td></tr>";
                    }
                }
                data += "</table></td><td></td></tr></table>";
                a += data + "~~SPI~~";
            }
            if (dt.Tables[0].Rows.Count % 2 != 0)
                sugg += "</tr></table>";
            else
                sugg += "</table>";


            strOut = sugg + "~~SPI~~" + a;
            //strOut = "{\"aaData\": [" + strOut + "]}";

        }
        catch { }
        context.Response.ContentType = "text/plain";
        context.Response.Write(strOut);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    public string ProcessRequest(int KPI_ID)
    {
        string KPI_Details = "";
        try
        {
            BLL_TMSA_KPI objBLL = new BLL_TMSA_KPI();
            DataSet ds = objBLL.Get_KPIDetails(KPI_ID);

            UDCHyperLink alink = new UDCHyperLink();
            alink.Target = "_blank";
            alink.NaviagteURL = "TMSA_PI_Details.aspx";
            alink.QueryStringDataColumn = new string[] { "PI_ID" };
            alink.QueryStringText = new string[] { "PI_ID" };

            Dictionary<string, UDCHyperLink> diclink = new Dictionary<string, UDCHyperLink>();

            //diclink.Add("PI_Name", alink);
            //DataTable dtValues=UDFLib.PivotTable("Effective_Date", "V1", "ID", new string[] { "VesselID" }, new string[] { }, ds.Tables[1]);
            //string KPIGrid = UDFLib.CreateHtmlTableFromDataTable(dtValues,
            //        new string[] { "Vessel Name", dtValues.Columns[1].ToString(), dtValues.Columns[2].ToString(), dtValues.Columns[3].ToString(), dtValues.Columns[4].ToString() },
            //        new string[] { "VesselName", "Value1", "Value2", "Value3", "Value4" },
            //        diclink,
            //        new Dictionary<string, UDCToolTip>(),
            //        new string[] { },
            //        "tbl-common-Css",
            //        "hdr-common-Css",
            //        "row-common-Css");

            //KPI_Details = "<tr><td class='KPI' colspan='2'><div id='dvKPIGrid'>" + KPIGrid + "</div></td></tr>";
            string img = "";


            if (ds.Tables[0].Rows[0]["KPI_Name"].ToString() == "CO2 efficiency")
                img = "Resource/CO2_Effeiciency.png";
            
            if (ds.Tables[0].Rows[0]["KPI_Name"].ToString() == "Releases of substances as def by MARPOL Annex 1-6")
                img = "Resource/Releases_of_substances.png";
            if (ds.Tables[0].Rows[0]["KPI_Name"].ToString() == "Ballast water management violations")
                img = "Resource/Ballast_water_management_violations.png";
            if (ds.Tables[0].Rows[0]["KPI_Name"].ToString() == "Contained spills")
                img = "Resource/Contained spills.png";
            if (ds.Tables[0].Rows[0]["KPI_Name"].ToString() == "Environmental deficiencies")
                img = "Resource/Environmental_deficiencies.png";
            if (ds.Tables[0].Rows[0]["KPI_Name"].ToString() == "Passenger Injury Ratio")
                img = "Resource/PassengerInjury.png";
            if (ds.Tables[0].Rows[0]["KPI_Name"].ToString() == "Lost Time Injury Frequency")
                img = "Resource/LostTimeInjury.png";
            if (ds.Tables[0].Rows[0]["KPI_Name"].ToString() == "Lost Time Sickness Frequency")
                img = "Resource/LostTimeSickness.png";
            if (ds.Tables[0].Rows[0]["KPI_Name"].ToString() == "Health and Safety deficiencies")
                img = "Resource/HealthSafetyDeficiencies.png";
            if (ds.Tables[0].Rows[0]["KPI_Name"].ToString() == "Port state control performance")
                img = "Resource/PortStateControl.png";

            KPI_Details += "<tr><td  class='KPI' colspan='2'><div><img src='" + img + "'/></div></td></tr>";
            //KPI_Details += "<tr><td  class='KPI' colspan='2'><div><img src='Resource/Ballast_Table.png'/></div></td></tr>";

            if (img == "Resource/CO2_Effeiciency.png")
            {
                string img2 = "Resource/CO2_PI.htm";
                string img3 = "Resource/CO2_PI_Manatee.htm";
                string img4 = "Resource/CO2_PI_Baleen.htm";
                KPI_Details += "<tr><td  align='center' colspan='2'><div>";
                KPI_Details += "<table width = '80%'><tr><th style='font-size:11px;background-color:#5a97f2;line-height:20px;text-align:center' width='15%'> Vessel </th>";
                KPI_Details += "<th style='font-size:11px;background-color:#5a97f2;line-height:20px;text-align:center'>Average SOx(MT) </th> <th style='font-size:11px;background-color:#5a97f2;line-height:20px;text-align:center'> GOAL(MT) </th> ";
                KPI_Details += " <th style='font-size:11px;background-color:#5a97f2;line-height:20px;text-align:center'  width='35%'> Voyage </th>   </tr>";
                
                KPI_Details += "<tr><td  style='text-align:left'>";
                
                KPI_Details += "<input type='checkbox' checked='true' name='Akari'>";

                KPI_Details += "<a  style='color:#0D3E6E' target='_blank' href='" + img2 + "'> AKARI </a></td> <td align='center'>0.000280617 </td> <td align='center'> N/A</td></tr>";

                KPI_Details += "<tr><td  style='color:#0D3E6E;line-height:20px;text-align:left'>";

                KPI_Details += "<input type='checkbox' checked='false' name='Bluemoon'>";
                KPI_Details += "<a  style='font-size:11px;color:#0D3E6E' target='_blank' href='" + img3 + "'> BLUE MOON  </a></td> <td align='center'> 0.000306298</td> <td align='center'> N/A</td></tr>";

                KPI_Details += "<tr><td  style='color:#0D3E6E;line-height:20px;text-align:left'>";
                KPI_Details += "<input type='checkbox' checked='false' name='Ponente'>";
                KPI_Details += "<a  style='font-size:11px;color:#0D3E6E' target='_blank' href='" + img3 + "'> PONENTE  </a></td> <td align='center'> 0.000182583 </td> <td align='center'> N/A </td></tr>";

                KPI_Details += "<tr><td  style='color:#0D3E6E;line-height:20px;text-align:left'>";
                KPI_Details += "<input type='checkbox' checked='false' name='SeaPrinces'>";
                KPI_Details += "<a  style='font-size:11px;color:#0D3E6E' target='_blank' href='" + img3 + "'> SEA PRINCES  </a></td> <td align='center'>0.000326768 </td> <td align='center'> N/A </td></tr>";
                
                KPI_Details += "<tr><td  style='color:#0D3E6E;line-height:20px;text-align:left'>";
                KPI_Details += "<input type='checkbox' checked='true' name='Manatee'>";
                KPI_Details += "<a  style='font-size:11px;color:#0D3E6E' target='_blank' href='" + img3 + "'> MANATEE  </a></td> <td align='center'> 0.000169902 </td> <td align='center'> N/A </td></tr>";
                
                KPI_Details += "<tr><td  style='color:#0D3E6E;line-height:20px;text-align:left'>";
                KPI_Details += "<input type='checkbox' checked='true' name='Baleen'>";
                KPI_Details += "<a  style='font-size:11px;color:#0D3E6E' target='_blank' href='" + img4 + "'> BALEEN  </a></td> <td align='center'> 0.000567821 </td> <td align='center'> N/A </td></tr>";
                
                KPI_Details += "</table></div></td></tr>";
            }
            else
                KPI_Details += "<tr><td  class='KPI' colspan='2'><div><img src='Resource/Ballast_Table.png'/></div></td></tr>";
            
            KPI_Details += "<tr><td class='KPI' colspan='2'><table><tr><td class='SPItext'>KPI Description :</td><td align='left'>" + ds.Tables[0].Rows[0]["Description"].ToString() + "</td></tr>";
            //KPI_Details += "<tr><td class='KPI' colspan='2'><table><tr><td><table><tr><td>KPI Description :</td><td>" + ds.Tables[0].Rows[0]["Description"].ToString() + "</td></tr>";
            KPI_Details += "<tr><td class='SPItext'>Time Period :</td><td  align='left'>" + ds.Tables[0].Rows[0]["Time_Period"].ToString() + "</td></tr>";
            KPI_Details += "<tr><td class='SPItext'>Vessel/Fleet measurements :</td><td  align='left'>" + ds.Tables[0].Rows[0]["Measurement_Detail"].ToString() + "</td></tr>";
            if (img == "Resource/CO2_Effeiciency.png")
            {
                KPI_Details += "<tr><td class='SPItext'>PI Used :</td><td><div><img src='Resource/CO2_PI_List.png'/></div> </td></tr>";
            }
            else
                KPI_Details += "<tr><td class='SPItext'>PI Used :</td><td><div><img src='Resource/PI.png'/></div> </td></tr>";
            //" + UDFLib.CreateHtmlTableFromDataTable(ds.Tables[2],
            //        new string[] { "S.No", "PI Name" },
            //        new string[] { "SNo", "Name" },
            //        diclink,
            //        new Dictionary<string, UDCToolTip>(),
            //        new string[] { },
            //        "tbl-common-Css",
            //        "hdr-common-Css",
            //        "row-common-Css") + "


            KPI_Details += "<tr><td class='SPItext'>KPI Formula :</td><td  align='left'>" + ds.Tables[0].Rows[0]["Context"].ToString() + "</td></tr></table></td></tr>";
           // KPI_Details += "<td><div id='dvKPIGrid'><img src='Resource/Ballast_Table.png'/></div></td></tr></table></td></tr>";                                          
                            
        }
        catch { }
        return KPI_Details;
    }
}