<%@ Page Title="Crew Rotation Report" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CrewRotationReport.aspx.cs" Inherits="Crew_CrewRotationReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
       <div class="page-title">
        Crew Rotation Report
    </div>
     <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2;">
     
        <div id="dvFilter" style="border: 1px solid #cccccc; margin: 2px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%" cellspacing="2">
                        <tr>
                            <td>
                                Vessel Manager
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVessel_Manager" runat="server" Width="256px" OnSelectedIndexChanged="ddlVessel_Manager_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Fleet
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                    Style="width: 150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                From Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtFromDate" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtFromDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td rowspan="2">
                                Search:
                                <asp:TextBox ID="txtSearch" runat="server" Style="width: 100px" />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" ClientIDMode="Static" />
                                <%--<asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Manning Office
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlManningOffice" runat="server" Width="256px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Vessel Name
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"
                                    Style="width: 150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                To Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtToDate" runat="server"  ClientIDMode="Static"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calTo" runat="server" TargetControlID="txtToDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="dvMain" style="border: 1px solid #cccccc; margin: 2px; overflow: auto;">
                    <asp:Repeater ID="rpt1" runat="server">
                        <HeaderTemplate>
                            <table style="background-color: White; border-collapse: collapse; width: 100%;" border="1"
                                cellpadding="2px" cellspacing="0" border="1">
                                <tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <td style="vertical-align: top;">
                                <div style="background-color:#00BFFF;text-align: center; font-weight: bold;
                                    color: #333;">
                                    <%# Eval("vessel_name")%></div>
                                <asp:Repeater runat="server" ID="rpt2" DataSource='<%# ((System.Data.DataRowView) Container.DataItem).Row.GetChildRows("ChildMembers") %>'>
                                    <HeaderTemplate>
                                        <table border="1">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="background-color: #F2FBEF;">
                                                <div style="text-transform: uppercase;width:300px">
                                                    <a href='CrewDetails.aspx?ID=<%# ((System.Data.DataRow)Container.DataItem)["ID"]%>' target="_blank"><%# ((System.Data.DataRow)Container.DataItem)["Staff_Surname"]%>
                                                    <%# ((System.Data.DataRow)Container.DataItem)["Staff_Name"]%></a></div>
                                                <div>
                                                   <%#(UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Sign_On1"])))!= "" ? "S/ON: " + UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Sign_On1"])):""%></div>
                                                <div>
                                                    <%# (UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Sign_Off1"]))) != "" ? "COC: " + UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Sign_Off1"])): ""%></div>
                                                <div>
                                                    <%#  (UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Available_From_Date1"]))) != "" ? "Ex to Join: " + UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Available_From_Date1"])) : ""%></div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td style="background-color: #F5EFFB;">
                                                <div style="text-transform: uppercase;width:300px">
                                                    <a href='CrewDetails.aspx?ID=<%# ((System.Data.DataRow)Container.DataItem)["ID"]%>' target="_blank"><%# ((System.Data.DataRow)Container.DataItem)["Staff_Surname"]%>
                                                    <%# ((System.Data.DataRow)Container.DataItem)["Staff_Name"]%></a></div>
                                               <div>
                                                   <%#(UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Sign_On1"])))!= "" ? "S/ON: " + UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Sign_On1"])):""%></div>
                                                <div>
                                                    <%# (UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Sign_Off1"]))) != "" ? "COC: " + UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Sign_Off1"])): ""%></div>
                                                <div>
                                                    <%#  (UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Available_From_Date1"]))) != "" ? "Ex to Join: " + UDFLib.ConvertUserDateFormat(Convert.ToString(((System.Data.DataRow)Container.DataItem)["Available_From_Date1"])) : ""%></div>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tr> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
         
    </div>
   <script type="text/javascript">
       $(document).ready(function () {
           var strDateFormat = "<%= DateFormat %>";
           var CurrentDateFormatMessage = '<%= UDFLib.DateFormatMessage() %>';

           $("body").on("click", "#btnSearch", function () {
               var Msg = "";
               if ($("[id$='txtFromDate']").val() != "") {
                   if (IsInvalidDate($("#txtFromDate").val(), strDateFormat)) {
                       Msg += "Enter valid From Date" + CurrentDateFormatMessage + "\n";
                   }
               }
                if ($("[id$='txtToDate']").val() != "") {
                    if (IsInvalidDate($("#txtToDate").val(), strDateFormat)) {
                        Msg += "Enter valid To Date" + CurrentDateFormatMessage + "\n";
                    }
                }
                if ($("#txtFromDate").length > 0 && $("#txtToDate").length > 0) {
                    if (DateAsFormat(document.getElementById("txtFromDate").value, strDateFormat) > DateAsFormat(document.getElementById("txtToDate").value, strDateFormat)) {
                        Msg += "Issue Date should be less than Expiry Date\n";
                    }
                }
                if (Msg != "") {
                    alert(Msg);
                    return false;
                }
                else
                    return true;
           });
       });
       function PrintDiv(dvID) {

           var a = window.open('', '', 'left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
           a.document.write(document.getElementById(dvID).innerHTML);
           a.document.close();
           a.focus();
           a.print();
           a.close();
           return false;
       }
    </script>
</asp:Content>
