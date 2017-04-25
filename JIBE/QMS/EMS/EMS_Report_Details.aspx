<%@ Page Title="EMS Report Details" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="EMS_Report_Details.aspx.cs" Inherits="QMS_EMS_EMS_Report_Details" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .table td
        {
            font-size: 11px;
            padding: 3px 0px 3px 2px;
            border-bottom: 1px solid gray;
            border-right: 1px solid gray;
            color: Black;
        }
        .leafTD-header
        {
            width: 200px;
            text-align: left;
            background-color: #E0ECF8;
            color: Black;
            font-size: 11px;
        }
        .leafTD-data
        {
            width: 100px;
            background-color: White;
            text-align: right;
            background-color:#F2F5A9;
           
        }
        #pageTitle
        {
            background-color: gray;
            color: White;
            font-size: 12px;
            text-align: center;
            padding: 2px;
            font-weight: bold;
        }
        
        .MeargeHD
        {
            width: 200px;
            text-align: left;
            background-color: #99CCFF;
            color: Black;
            font-size: 11px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
      EMS Report Details
    
    </div>
    <table>
        <tr valign="top">
            <td>
                <table cellpadding="3" cellspacing="0" style="color: Black;border-top:1px solid gray;border-left:1px solid gray" class="table">
                    <tr class="trMain">
                        <td style="background-color: #99CCFF; width: 120px; font-weight: bold">
                            Vessel
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblVslName" runat="server"></asp:Label>   &nbsp;
                        </td>
                    </tr>
                    <tr class="trMain">
                        <td style="background-color: #99CCFF; width: 120px; font-weight: bold; color: Black">
                            Date From
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lbldtFrom" runat="server"></asp:Label>   &nbsp;
                        </td>
                    </tr>
                    <tr class="trMain">
                        <td style="background-color: #99CCFF; width: 120px; font-weight: bold; color: Black">
                            Date To
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lbldtTO" runat="server"></asp:Label>   &nbsp;
                           
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 20px">
                &nbsp;
            </td>
            <td>
                <table cellpadding="3" cellspacing="0" class="table" style="border-right: 1px solid gray;
                    border-top: 1px solid gray; border-left: 1px solid gray; color: Black">
                    <tr>
                        <td style="background-color: #99CCFF; width: 120px; font-weight: bold; color: Black">
                            Remark
                        </td>
                    </tr>
                    <tr class="trMain">
                        <td style="text-align: left;height:50px;vertical-align:top">
                            <asp:Label ID="lblRemark" Width="500px" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <div style="position: relative">
        <cc1:TabContainer ID="TabContainerDetails" runat="server" ActiveTabIndex="0">
            <cc1:TabPanel runat="server" HeaderText="Instructions" ID="TabInst">
                <ContentTemplate>
                    <%-- <asp:FormView ID="fvInst" runat="server">--%>
                    <%--   <ItemTemplate>--%>
                    <br />
                    <table cellpadding="0" cellspacing="0" class="table">
                        <tr>
                            <td style="border-left: 1px solid gray; border-top: 1px solid gray">
                                Since above data is also directly fed into database, please observe the following:<br />
                                <br />
                                1. All figures rounded off to indicate digits with no decimals
                                <br />
                                (except for sludge, garbage and ballast exchange fuel consumption to one decimal
                                place).
                                <br />
                                2. Any remarks may be written below this space.<br />
                                3. For nil entry, please write ' 0' and do not write NIL or NA.
                                <br />
                                4. All data is for the reporting period and not to be added to previous reports'
                                figures.<br />
                                5. Once the report is SAVED, it will be set to office in the next UPDATE from 
                                the vessel.<br />  <%--In Place of "UPDATE" there "VLOG UPDATE" was return which is wrong. So , "VLOG UPDATE" is replaced with "UPDATE"--%>
                            </td>
                        </tr>
                    </table>
                    <%--   </ItemTemplate>
                </asp:FormView>--%>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Bunkering" ID="TabBunk">
                <ContentTemplate>
                    <br />
                    <asp:FormView ID="fvBunk" runat="server">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" class="table">
                                <tr class="trMain">
                                    <td colspan="5" style="background-color: Green; color: White; font-weight: bold">
                                        Bunkering
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td colspan="2" class="MeargeHD">
                                        Number of Operations
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#  Eval("BUNK_1")%>
                                    </td>
                                    <td>
                                        Number of operations
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="padding: 0px 0px 0px 0px; height: 10px; border-right: 0px solid gray;
                                        font-size: 9px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td rowspan="2" class="MeargeHD">
                                        Stemmed
                                    </td>
                                    <td class="leafTD-header">
                                        FO (MT)
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#  Eval("BUNK_2")%>
                                    </td>
                                    <td>
                                        Total Fuel Oil bunkered (BDR figures)
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header">
                                        DO (MT)
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#  Eval("BUNK_3")%>
                                    </td>
                                    <td>
                                        Total Diesel Oil bunkered (BDR figures)
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="padding: 0px 0px 0px 0px; height: 10px; border-right: 0px solid gray;
                                        font-size: 9px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td rowspan="3" class="MeargeHD">
                                        Spills
                                    </td>
                                    <td class="leafTD-header">
                                        On Deck
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#  Eval("BUNK_4")%>
                                    </td>
                                    <td>
                                        Number of spill incidents
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header">
                                        Overboard
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#  Eval("BUNK_5")%>
                                    </td>
                                    <td>
                                        Number of spill incidents
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header">
                                        Quantity (Barrels)
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#  Eval("BUNK_6")%>
                                    </td>
                                    <td>
                                        Total quantity spilt (deck+overboard)
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Engine Room Debilging" ID="TabEng">
                <ContentTemplate>
                    <br />
                    <asp:FormView ID="fvEng" runat="server">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" class="table">
                                <tr class="trMain">
                                    <td colspan="5" style="background-color: Green; color: White; font-weight: bold">
                                        Engine Room Debilging
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td rowspan="2" class="MeargeHD">
                                        Through OWS
                                    </td>
                                    <td class="leafTD-header">
                                        No. of Operations
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("ER_DEBILGE_1")%>
                                    </td>
                                    <td>
                                        Total quantity discharged through OWS
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header">
                                        Quantity (CBM)
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("ER_DEBILGE_2")%>
                                    </td>
                                    <td>
                                        Number of discharge operations through OWS
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="padding: 0px 0px 0px 0px; height: 10px; border-right: 0px solid gray;
                                        font-size: 9px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td rowspan="2" class="MeargeHD">
                                        To Barge / Ashore due to OWS malfunction or special requirements
                                    </td>
                                    <td class="leafTD-header">
                                        No. of Operations
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("ER_DEBILGE_3")%>
                                    </td>
                                    <td>
                                        Number of discharge operations to shore and barge facilities due to OWS defect
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header">
                                        Quantity (CBM)
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("ER_DEBILGE_4")%>
                                    </td>
                                    <td>
                                        Total Volume discharged to shore facilities / barge due to OWS defect
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="padding: 0px 0px 0px 0px; height: 10px; border-right: 0px solid gray;
                                        font-size: 9px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td rowspan="2" class="MeargeHD">
                                        To barge / ashore due to port regulations
                                    </td>
                                    <td class="leafTD-header">
                                        No. of Operations
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("ER_DEBILGE_5")%>
                                    </td>
                                    <td>
                                        Number of discharge operations to shore facilities or barge due to port regulations
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header">
                                        Quantity (CBM)
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("ER_DEBILGE_6")%>
                                    </td>
                                    <td>
                                        Total Volume discharged to shore facilities or barge due to port regulations
                                    </td>
                                </tr>
                               
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Cargo Operations" ID="TabCargo">
                <ContentTemplate>
                    <br />
                    <asp:FormView ID="fvCargo" runat="server">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" class="table">
                                <tr class="trMain">
                                    <td colspan="5" style="background-color: Green; color: White; font-weight: bold">
                                        Cargo Operations
                                    </td>
                                </tr>
             

                                <tr>
                                    <td colspan="4" style="padding: 0px 0px 0px 0px; height: 10px; border-right: 0px solid gray;
                                        font-size: 9px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td rowspan="3" class="MeargeHD">
                                        Accidental Spills
                                    </td>
                                    <td class="leafTD-header">
                                        On Deck
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("CARGO_OPS_1")%>
                                    </td>
                                    <td>
                                        Number of spill incidents
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header">
                                        Overboard
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("CARGO_OPS_2")%>
                                    </td>
                                    <td>
                                        Number of spill incidents
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header">
                                        Quantity (Barrels)
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("CARGO_OPS_3")%>
                                    </td>
                                    <td>
                                        Total quantity spilt (deck+overboard)
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Sludge and Garbage Mgt" ID="TabSlud">
                <ContentTemplate>
                    <br />
                    <asp:FormView ID="fvSlud" runat="server">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" class="table">
                                <tr class="trMain">
                                    <td colspan="4" style="background-color: Green; color: White; font-weight: bold">
                                        Sludge Management
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td colspan="2" class="MeargeHD">
                                        Disposed onboard (CBM)
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("SLUDGE_1")%>
                                    </td>
                                    <td>
                                        Total Quantity (burnt in boiler + burnt in incinerator + volume evaporated). Only
                                        total quantiy required, break-up not required.
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td colspan="2" class="MeargeHD">
                                        Disposed ashore (CBM)
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("SLUDGE_2")%>
                                    </td>
                                    <td>
                                        Quantity discharged ashore (due to incinerator malfunction OR due to port regulations).
                                    </td>
                                </tr>
                            
                                <tr class="trMain">
                                    <td colspan="4">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td colspan="4" style="background-color: Green; color: White; font-weight: bold">
                                        Garbage Management
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td colspan="2" class="MeargeHD">
                                        Quantity Incinerated (CBM)
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("GARBAGE_1")%>
                                    </td>
                                    <td>
                                        Total garbage incinerated (category-wise break-up not required)
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td colspan="2" class="MeargeHD">
                                        Discharged at sea (CBM)
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("GARBAGE_2")%>
                                    </td>
                                    <td>
                                        Total garbage disposed to the sea (category-wise break-up not required)
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td colspan="2" class="MeargeHD">
                                        Discharged ashore (CBM)
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("GARBAGE_3")%>
                                    </td>
                                    <td>
                                        Total garbage discharged ashore (category-wise break-up not required)
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="Water Mgt" ID="TabWater">
                <ContentTemplate>
                    <br />
                    <asp:FormView ID="fvWater" runat="server">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" class="table">
                                <tr class="trMain">
                                    <td colspan="3" style="background-color: Green; color: White; font-weight: bold">
                                        Water Ballast Management
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header" style=" font-weight: bold">
                                        Number of Operations
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("WBM_1")%>
                                    </td>
                                    <td>
                                        Number of ballast water exchange operations (not operational ballasting/ deballasting)
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header" style=" font-weight: bold">
                                        Amount (CBM)
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("WBM_2")%>
                                    </td>
                                    <td>
                                        Volume of ballast water through the pumps.
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header" style=" font-weight: bold">
                                        Fuel Consumption (MT)
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("WBM_3")%>
                                    </td>
                                    <td>
                                        Consumption for the pumping.
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                               
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel runat="server" HeaderText="SECA" ID="TabSeca">
                <ContentTemplate>
                    <br />
                    <asp:FormView ID="fvSeca" runat="server">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" class="table">
                                <tr class="trMain">
                                    <td colspan="3" style="background-color: Green; color: White; font-weight: bold">
                                        Feed ONLY if vessel has visited SECA area
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header" style="width: 300px; font-weight: bold">
                                        Time taken for changeover-in hrs
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("SECA_1")%>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header" style=" font-weight: bold">
                                        SECA transit on which fuel
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("SECA_2")%>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header" style=" font-weight: bold">
                                        Type of changeover
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("SECA_3")%>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header" style=" font-weight: bold">
                                        Sulphur content (in %): HSFO
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("SECA_4")%>
                                    </td>
                                    <td>
                                        in %
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header" style=" font-weight: bold">
                                        Sulphur content (in %): LSFO
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("SECA_5")%>
                                    </td>
                                    <td>
                                        in %
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding: 0px 0px 0px 0px; height: 10px; border-right: 0px solid gray;
                                        font-size: 9px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr class="trMain">
                                    <td class="leafTD-header" style=" font-weight: bold">
                                        SECA area REMARKS, if any
                                    </td>
                                    <td class="leafTD-data">
                                        &nbsp;
                                        <%#Eval("SECA_6")%>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
</asp:Content>
