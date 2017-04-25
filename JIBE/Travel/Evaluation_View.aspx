<%@ Page Title="View Quotation Evaluation" Language="C#" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="Evaluation_View.aspx.cs" Inherits="Evaluation" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>JIBE::Approve Travel Request</title>
    <style type="text/css">
        body
        {
            font-family: Tahoma,verdana;
            font-size: 11px;
        }
        .GridView-css
        {
            border-collapse: collapse;
            border: 1px solid #959EAF;
        }
        .HeaderStyle-css
        {
            background-color: #DCDCDC;
            color: #333333;
            font-size: 11px;
            padding: 1px;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #959EAF;
            border-collapse: collapse;
        }
        .HeaderStyle-css th
        {
            border: 1px solid #959EAF;
            border-collapse: collapse;
        }
        
        
        .AlternatingRowStyle-css
        {
            /*EDF1F8*/
            background-color: #F6F6F6;
            color: #333333;
            font-size: 11px;
        }
        
        
        .AlternatingRowStyle-css td
        {
            border-bottom: 1px solid #EAEAEA;
        }
        
        
        .RowStyle-css
        {
            font-size: 11px;
            background-color: white;
            color: #333333;
        }
        
        .RowStyle-css td
        {
            border-bottom: 1px solid #EAEAEA;
        }
        
        .gridmain-css
        {
            border-collapse: collapse;
            border: 1px solid #959EAF;
        }
        
        .FooterStyle-css
        {
            background-color: #5D7B9D;
            font-weight: normal;
            font-size: 11px;
            color: White; /*ForeColor:White;*/
            border: 1px solid #959EAF;
        }
        
        
        .RowStyle-css td
        {
            border: 1px solid #EAEAEA;
        }
        
        
        .AlternatingRowStyle-css td
        {
            border: 1px solid #EAEAEA;
            background-color: #F7FBFB;
        }
        
        .GridView-css
        {
            border: 1px solid black;
            border-collapse: collapse;
        }
        .tdHeader
        {
            font-weight: bold;
            color: Black;
        }
        .btnhyperlink
        {
            text-decoration: underline;
            color: Blue;
            cursor: pointer;
        }
        .costhd
        {
            padding-right: 10px;
            text-align: right;
            font-size: 11px;
            font-weight: bold;
        }
        .costdt
        {
            padding-left: 10px;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdAgentID" runat="server" />
    <div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: bottom;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
        padding: 2px; background-color: #F6CEE3; text-align: center; font-weight: bold;width:600px">
        <table width="100%">
            <tr>
                <td style="text-align: left;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Travel Request ID "></asp:Label>
                    :
                    <asp:Literal ID="ltRequestid" runat="server"></asp:Literal>
                </td>
                <td>
                    <span style="background-color: Yellow; height: 25px; padding: 2px 10px 2px 10px">
                        <asp:Label ID="lblSeamanStatus" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
        </table>
    </div>
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 5px; min-height: 620px; overflow: auto;
        width: 600px">
        <div>
            <asp:Repeater ID="rptParent" DataMember="Request" runat="server" OnItemDataBound="rptParent_OnItemDataBound">
                <HeaderTemplate>
                    <table cellpadding="1px" cellspacing="0px" width="100%" class="GridView-css" style="border-collapse: separate;
                        border: 1px solid gray">
                        <tr class="HeaderStyle-css" style="font-weight: bold; height: 30px">
                            <td style="width: 30px;">
                                &nbsp;
                            </td>
                            <td style="width: 150px;">
                                Vessel Name
                            </td>
                            <td>
                                Route/Pax
                            </td>
                            <td style="width: 80px;">
                                Departure Date
                            </td>
                            <td style="width: 100px;">
                                Travel Type
                            </td>
                            <td style="width: 80px;">
                                Quote Due Date
                            </td>
                            <td style="width: 80px;">
                                No. Of Pax
                            </td>
                            <td style="width: 140px;">
                                Requested By
                            </td>
                            <td style="width: 140px;">
                                Attach
                            </td>
                            <td style="width: 40px;">
                                Seaman?
                            </td>
                            <td align="center">
                                Remarks
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="RowStyle-css">
                        <td>
                            &nbsp;<%#DataBinder.Eval(Container.DataItem, "id")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "vessel_name")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "FlightRoute")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "departureDate")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "classOfTravel")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "QuoteDueDate")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "PaxCount")%>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "created_by")%>
                        </td>
                        <td>
                            <center>
                            </center>
                        </td>
                        <td>
                            <%#DataBinder.Eval(Container.DataItem, "isSeaman")%>
                        </td>
                        <td align="center">
                          
                        </td>
                    </tr>
                    <tr style="display: none; font-weight: bold; background-color: #C1D5F8">
                        <td style="background-color: white">
                            &nbsp;
                        </td>
                        <td>
                            <span>Code</span>
                        </td>
                        <td>
                            <span>Sur Name</span>
                        </td>
                        <td>
                            <span>Given Name</span>
                        </td>
                        <td>
                            <span>Rank</span>
                        </td>
                        <td>
                            Nationality
                        </td>
                        <td>
                            <span>DOB</span>
                        </td>
                        <td>
                            <span>Place Of Birth</span>
                        </td>
                        <td>
                            Personal?
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <asp:Repeater ID="rptChild" runat="server" 
                        DataSource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("RequestPax") %>'>
                        <ItemTemplate>
                            <tr style="text-align: left; " >
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <a href='../crew/crewdetails.aspx?id=<%# ((System.Data.DataRow)Container.DataItem)["staffid"]%>'
                                        target="_blank">
                                        <%# ((System.Data.DataRow)Container.DataItem)["staff_code"]%>
                                    </a>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["staff_surname"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["staff_name"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["CurrentRank"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["Staff_Nationality"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["Staff_Birth_Date"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["Staff_Born_Place"]%>
                                </td>
                                <td>
                                    <%# ((System.Data.DataRow)Container.DataItem)["isPersonalTicket"]%>
                                </td>
                                <td colspan="2">
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <br />
        <div id="dvmaingrid">
            <asp:Repeater ID="rptQuotes" DataMember="dtQuotes" runat="server" OnItemCommand="rptQuotes_ItemCommand"
                OnItemDataBound="rptQuotes_OnItemDataBound">
                <HeaderTemplate>
                    <table cellpadding="0" cellspacing="0" width="100%" class="GridView-css">
                        <tr class="HeaderStyle-css" style="height: 35px">
                            <th style="width: 15%">
                                Agent / GDS / Deadline
                            </th>
                            <th style="width:60%">
                                <table width="100%" style="border-collapse: collapse">
                                    <tr>
                                        <td style="width: 20%">
                                            Airline Locator
                                        </td>
                                        <td style="width: 20%">
                                            From / Departure Date
                                        </td>
                                        <td style="width: 20%">
                                            To / Arrival Date
                                        </td>
                                        <td style="width: 16%">
                                            Flight
                                        </td>
                                        <td style="width: 10%">
                                            Class
                                        </td>
                                        <td style="width: 10%">
                                            Status
                                        </td>
                                        <td style="width: 4%">
                                        </td>
                                    </tr>
                                </table>
                            </th>
                            <th style="width: 8%; text-align: center">
                                Cost<br />
                            </th>
                       
                            <th >
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr id="datarow" runat="server">
                        <td style="width: 15%; border-left: 1px solid #666666; vertical-align: top">
                            <table width="100%" style="border-collapse: collapse" cellpadding="2">
                                <%--  <tr>
                                            <td style="" colspan="2">
                                                <asp:Label ID="lblOption" runat="server" Font-Bold="true" ForeColor="#8A2BE2" Text='<%# "Option: " + Eval("rowid").ToString()  %>'></asp:Label>
                                            </td>
                                        </tr>--%>
                                <tr>
                                    <td colspan="2" style="color: #8A2BE2; font-weight: bold">
                                        <%#DataBinder.Eval(Container.DataItem, "QuotedBy")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdHeader">
                                        GDS Locator
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "GDSLocator")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdHeader">
                                        Deadline
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "TicketingDeadline").ToString()%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 60%;" valign="top">
                            <table width="100%" style="border-collapse: collapse" cellpadding="2px">
                                <asp:Repeater ID="rptFlights" runat="server" DataSource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("RequestQuotes") %>'
                                    OnItemDataBound="rptFlights_OnItemDataBound">
                                    <ItemTemplate>
                                        <tr style="text-align: left;" class='child<%#((System.Data.DataRow)Container.DataItem)["id"]%>'>
                                            <td style="width: 20%">
                                                <%# ((System.Data.DataRow)Container.DataItem)["AirlineLocator"]%>
                                            </td>
                                            <td style="width: 20%">
                                                <span style="color: #006161; font-weight: bold; cursor: default" title='<%# "cssbody=[dvbdy1] cssheader=[dvhdr1] header=["+((System.Data.DataRow)Container.DataItem)["FromAirportName"].ToString()+ "]" %>'>
                                                    <%# ((System.Data.DataRow)Container.DataItem)["TravelFrom"]%>
                                                </span>
                                                <br />
                                                <%# ((System.Data.DataRow)Container.DataItem)["DepartureDate"]%>
                                            </td>
                                            <td style="width: 20%">
                                                <b style="color: #006161"><span style="color: #006161; font-weight: bold; cursor: default"
                                                    title='<%# "cssbody=[dvbdy1] cssheader=[dvhdr1] header=["+((System.Data.DataRow)Container.DataItem)["ToAirportName"].ToString()+ "]" %>'>
                                                    <%# ((System.Data.DataRow)Container.DataItem)["TravelTo"]%>
                                                </span>
                                                    <br />
                                                </b>
                                                <%# ((System.Data.DataRow)Container.DataItem)["ArrivalDate"]%>
                                            </td>
                                            <td style="width: 16%">
                                                <%# ((System.Data.DataRow)Container.DataItem)["FlightName"]%>&nbsp;[<%# ((System.Data.DataRow)Container.DataItem)["FlightNo"]%>]
                                            </td>
                                            <td style="width: 10%">
                                                <%# ((System.Data.DataRow)Container.DataItem)["TravelClass"]%>
                                            </td>
                                            <td style="width: 10%">
                                                <%# ((System.Data.DataRow)Container.DataItem)["FlightStatus"]%>
                                            </td>
                                            <td style="width: 4%" align="center">
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                        <td style="width: 8%; text-align: center; border-collapse: collapse; line-height: 25px;">
                            <b style="color: Red; font-size: 11px">$ &nbsp;
                                <asp:Label ID="lblGrandTotal_usd" Text='<%#Eval("USD_Total_Amount")%>' runat="server"></asp:Label>
                            </b>
                         
                        </td>
                    
                        <td style="line-height: 30px; border-collapse: collapse; border-right: 1px solid #666666;text-align:center">
                            <asp:Label ID="lblApprovedOption" runat="server" Font-Size="12px" Width="100%" Font-Bold="true" ForeColor="Red" Text='<%# UDFLib.ConvertToInteger(Eval("apprSts")) ==1 || UDFLib.ConvertToInteger(Eval("VerifSts")) ==1 ?"Approved":"" %>'
                                BackColor='<%#  UDFLib.ConvertToInteger(Eval("apprSts")) ==1 || UDFLib.ConvertToInteger(Eval("VerifSts")) ==1  ?System.Drawing.Color.Yellow:System.Drawing.Color.Transparent %>'></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="border-bottom: 1px solid #666666;">
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <br />
        <div id="dvTotalAmount" style="float: right; padding-right: 30px; font-size: 18px;
            font-weight: bold; color: Red;">
        </div>
        <asp:HiddenField ID="hdf_Totalamount" runat="server" />
        <asp:HiddenField ID="hdf_Cheapest_Totalamount" runat="server" />
        <asp:HiddenField ID="hdf_No_of_Quotation" runat="server" />
      
        <asp:TextBox ID="txtApproverRemark" Width="98%" runat="server" Visible="false" Font-Size="12px"
            Font-Names="Tohama" TextMode="MultiLine">
        </asp:TextBox>
      
        <hr />
        <div id="divTrackApproval" title="Approval History" style="display: block; color: black">
            <b>Approval History :</b><br />
            <br />
            <asp:GridView ID="gvApprovals" runat="server" CellPadding="2" CellSpacing="0" AutoGenerateColumns="true">
                <HeaderStyle CssClass="HeaderStyle-css" Font-Size="10px" />
                <RowStyle CssClass="RowStyle-css" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
