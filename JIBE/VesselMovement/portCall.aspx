<%@ Page Title="Vessel Movement" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="portCall.aspx.cs" EnableEventValidation="false" Inherits="VesselMovement_portCall" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function ValidateText() {
            var strDateFormat = "<%= DateFormat %>";
            if (IsInvalidDate($("#txtStartDate").val(), strDateFormat)) {
                alert("Please Enter Valid Start Date.");
                document.getElementById("txtStartDate").focus();
                return false;
            }

            if (IsInvalidDate($("#txtEndDate").val(), strDateFormat)) {
                alert("Please Enter Valid End Date.");
                document.getElementById("txtEndDate").focus();
                return false;
            }

        }

        function ValidateText1() {
            var strDateFormat = "<%= DateFormat %>";

            if (IsInvalidDate($("#txtAsofDate").val(), strDateFormat)) {
                alert("Please Enter Valid  Crew list As of Date.");
                document.getElementById("txtAsofDate").focus();
                return false;
            }

        }

        

        function validationOnSave() {
            var txteta = document.getElementById("ctl00_MainContent_txtFrom").value;
            var txtetd = document.getElementById("ctl00_MainContent_txtTo").value; ;


            var strDateFormat = "<%= DateFormat %>";

            if (IsInvalidDate($("#txtFrom").val(), strDateFormat)) {
                alert("Please Enter Valid From Date.");
                document.getElementById("txtFrom").focus();
                return false;
            }

            if (IsInvalidDate($("#txtTo").val(), strDateFormat)) {
                alert("Please Enter Valid To Date.");
                document.getElementById("txtTo").focus();
                return false;
            }



            var dt1 = parseInt(txteta.substring(0, 2), 10);
            var mon1 = parseInt(txteta.substring(3, 5), 10);
            var yr1 = parseInt(txteta.substring(6, 10), 10);

            var dt2 = parseInt(txtetd.substring(0, 2), 10);
            var mon2 = parseInt(txtetd.substring(3, 5), 10);
            var yr2 = parseInt(txtetd.substring(6, 10), 10);


            var ArrivalDt = new Date(yr1, mon1, dt1);
            var DepartureDate = new Date(yr2, mon2, dt2);

            if (txteta != "" && txtetd != "") {
                if (ArrivalDt > DepartureDate) {
                    alert("From Date can't be before of To Date.");
                    return false;
                }
            }
            return true;
        }





        function OpenScreen(ID, Job_ID) {
            var vesselID = document.getElementById("ctl00_MainContent_DDLVesselFilter").selectedIndex;
            var VID = document.getElementById("ctl00_MainContent_DDLVesselFilter").options[vesselID].value;
            var url = 'PortCall_Entry.aspx?ID=' + VID + '&StatusID=' + Job_ID;
            OpenPopupWindowBtnID('PortCall_Entry', 'Vessel Movement', url, 'popup', 530, 630, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }
        function OpenScreen1(ID, Job_ID) {
            var vesselID = document.getElementById("ctl00_MainContent_DDLVesselFilter").selectedIndex;
            var VID = document.getElementById("ctl00_MainContent_DDLVesselFilter").options[vesselID].value;
            var url = 'PortCall_Entry.aspx?ID=' + VID + '&StatusID=' + ID;
            OpenPopupWindowBtnID('PortCall_Entry', 'Vessel Movement', url, 'popup', 530, 630, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }
        function OpenScreen2(ID, Job_ID) {
            var url = 'Port_Call_Report.aspx?VesselID=' + ID + '&PortCallID=' + Job_ID;
            OpenPopupWindowBtnID('PortCall_Entry', 'Vessel Movement', url, 'popup', 790, 1200, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
            //            var url = 'Port_Call_Report.aspx?VesselID=' + ID + '&PortCallID=' + Job_ID;
            //            OpenPopupWindowBtnID('Port Call Details', 'Vessel Movement', url, 'popup', 530, 630, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loader.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <center>
        <div style="color: Black;">
            <asp:UpdatePanel ID="updMaker" runat="server">
                <ContentTemplate>
                    <div style="border: 1px solid #cccccc" class="page-title">
                        Vessel Movement
                    </div>
                    <div id="dvpage-content" class="page-content-main" style="padding: 1px">
                        <table width="100%" cellpadding="2" cellspacing="1">
                            <tr>
                                <td align="right" style="width: 10%">
                                    Vessel Name &nbsp;:&nbsp;
                                </td>
                                <td align="left" style="width: 20%">
                                    <asp:DropDownList ID="DDLVesselFilter" runat="server" Width="200px" CssClass="txtInput">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 8%">
                                    From Date&nbsp;:&nbsp;
                                </td>
                                <td align="left" style="width: 12%">
                                    <asp:TextBox ID="txtFrom" runat="server" Width="120px" BackColor="#FFFFCC"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrom">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td align="left" style="width: 8%">
                                    To Date&nbsp;:&nbsp;
                                </td>
                                <td align="right" style="width: 6%">
                                    <asp:TextBox ID="txtTo" runat="server" Width="120px" BackColor="#FFFFCC"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTo">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td align="left" style="width: 8%">
                                    <asp:Button ID="btnFilter" runat="server" OnClientClick="return validationOnSave();"
                                        Text="Search" Width="90px" OnClick="btnFilter_Click" />
                                    <%--<asp:ImageButton ID="btnFilter" OnClientClick="return validationOnSave();" runat="server"
                                        OnClick="btnFilter_Click" ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />--%>
                                </td>
                                <td align="left" style="width: 15px">
                                    <asp:Button ID="btnSupplier" runat="server" Text="Manage Contacts" OnClick="btnSupplier_Click" />
                                </td>
                                <td align="left" style="width: 15px">
                                    <asp:Button ID="btnVesselReport" runat="server" Text="Vessel Report" OnClick="btnVesselReport_Click" />
                                </td>
                                <td style="width: 5%">
                                    <asp:Button ID="btnPrintDPL" runat="server" Text="DPL Print Preview" OnClick="btnPrintDPL_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div style="width: 1200px; text-align: center; border: 1px solid #cccccc; font-family: Tahoma;
                            font-size: 12px; overflow: Auto">
                            <table>
                                <tr>
                                    <td valign="top" align="right">
                                        <table width="100px" runat="server" id="table1" style="height: 100px">
                                            <tr>
                                                <td width="75%" style="color: Black;" align="right">
                                                    Port Name :
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="75%" style="color: Black;" align="right">
                                                    Arrival :
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="75%" style="color: Black;" align="right">
                                                    Berthing :
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="75%" style="color: Black;" align="right">
                                                    Departure :
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="75%" style="color: Black;" align="right">
                                                    <asp:Button ID="btnPrevious" runat="server" Text="<<" OnClick="btnPrevious_Click" />
                                                    <asp:Button ID="btnNext" runat="server" Text=">>" OnClick="btnNext_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:DataList ID="gvPortCall" runat="server" Width="100px" RepeatColumns="50" RepeatDirection="Horizontal"
                                                        OnItemCommand="gvPortCall_ItemCommand" OnItemDataBound="gvPortCall_ItemDataBound">
                                                        <ItemTemplate>
                                                            <table style="width: 150px; height: 220px; text-align: center; border: 1px solid #cccccc;
                                                                font-family: Tahoma; font-size: 12px; overflow: Auto">
                                                                <tr>
                                                                    <td width="75%" style="color: Black; height: 20px;" align="center">
                                                                        <%--<asp:Label ID="FromportName" runat="server" Text='<%# Eval("Port_Name")%>'></asp:Label>OnClientClick='<%#"OpenScreen2((&#39;" + Eval("[Vessel_ID]") +"&#39;),(&#39;"+ Eval("[Port_Call_ID]") + "&#39;));return false;"%>'--%>
                                                                        <asp:LinkButton ID="SelectButton" Text='<%#Eval("Port_Name")%>' CommandArgument='<%#Eval("[Vessel_ID]") + "," + Eval("[Port_Call_ID]") + "," + Eval("[Arrival]") + "," + Eval("[Port_ID]") %>'
                                                                            CommandName="Select" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="75%" valign="middle" style="color: Black; height: 15px;">
                                                                        <asp:Label ID="lblArrival" runat="server" Text='<%# Eval("Arrival","{0:g}")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="50%" valign="middle" style="color: Black; height: 20px;">
                                                                        <asp:Label ID="lblBerthing" runat="server" Text='<%# Eval("Berthing","{0:g}")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="75%" valign="middle" style="color: Black; height: 20px;">
                                                                        <asp:Label ID="lblDeparture" runat="server" Text='<%# Eval("Departure","{0:g}")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="75%" valign="middle" style="color: Black; height: 20px;">
                                                                        <%-- <asp:Label ID="lblPort_Remarks" runat="server" Visible="false" Text='<%# Eval("Port_Remarks")%>'></asp:Label>--%>
                                                                        <asp:CheckBox ID="Iswarrisk" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsWarRisk")) %>'
                                                                            Text="WarRisk" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="75%" valign="middle" style="color: Black; height: 20px;">
                                                                        <%--    <asp:Label ID="Label1" runat="server" Visible="false" Text='<%# Eval("Port_Remarks")%>'></asp:Label>--%>
                                                                        <asp:CheckBox ID="IsShipCraneReq" runat="server" Checked='<%# Convert.ToBoolean(Eval("IsShipCraneReq")) %>'
                                                                            Text="ShipCrane Req." />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="75%" valign="middle" style="color: Black; height: 20px;">
                                                                        <asp:LinkButton ID="lnkAuto" Text="Auto Date is Off" OnCommand="lnkAuto_Click" CommandArgument='<%#Eval("[Port_Call_ID]") + "," + Eval("[Vessel_ID]") %>'
                                                                            CommandName="Select" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="75%" style="color: Black; height: 20px;">
                                                                        <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnClientClick='<%#"OpenScreen1(&#39;"+ Eval("Port_Call_ID") +"&#39;);return false;"%>'
                                                                            CommandArgument='<%#Eval("[Port_Call_ID]")%>' ForeColor="Black" ToolTip="Edit"
                                                                            ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("[Port_Call_ID]") + "," + Eval("[Vessel_ID]") %>'
                                                                            ForeColor="Black" ToolTip="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure want to delete?')"
                                                                            ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="75%" style="color: Black; height: 10px; font-weight: bold">
                                                                        <asp:Label ID="lblSatatus" runat="server" ForeColor="Red" Text='<%# Eval("Port_Call_Status")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="75%" style="color: Black; height: 10px; font-weight: bold">
                                                                        <asp:Label ID="lblPortRemarks" runat="server" Text='<%# Eval("Port_Remarks")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="75%" style="color: Black; height: 10px; font-weight: bold">
                                                                        <asp:Label ID="LBLCSUPPLIER" runat="server" Text='<%# Eval("CSUPPLIER")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="75%" style="color: Black; height: 10px; font-weight: bold">
                                                                        <asp:Label ID="LBLOSUPPLIER" runat="server" Text='<%# Eval("OSUPPLIER")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="75%" style="color: Black; height: 10px; font-weight: bold">
                                                                        <asp:ImageButton ID="imgPurchasebtn" runat="server" Text="Update" CommandArgument='<%#Eval("[Port_Call_ID]")%>'
                                                                            ForeColor="Black" ImageUrl="~/Images/supply_icon.jpg" ToolTip="Purchase Order"
                                                                            Height="16px"></asp:ImageButton>
                                                                        <asp:ImageButton ID="imgPoAgencybtn" Visible="false" runat="server" Text="Update"
                                                                            CommandArgument='<%#Eval("[Port_Call_ID]")%>' ForeColor="Black" ImageUrl="~/Images/Agency_PO.jpg"
                                                                            ToolTip="Agency PO" Height="16px"></asp:ImageButton>
                                                                        <asp:ImageButton ID="ImgView" runat="server" Text="Update" CommandArgument='<%#Eval("[Port_Call_ID]")%>'
                                                                            ForeColor="Black" ImageUrl="~/Images/CrewChange.bmp" ToolTip='<%# Eval("CrewOff") %>'
                                                                            Height="16px"></asp:ImageButton>
                                                                        <asp:ImageButton ID="imgWorkList" runat="server" Text="Update" CommandArgument='<%#Eval("[Port_Call_ID]")%>'
                                                                            ForeColor="Black" ImageUrl="~/Images/alert.jpg" ToolTip="Work List" Height="16px">
                                                                        </asp:ImageButton>
                                                                        <asp:ImageButton ID="imgAgencyWork" runat="server" Text="Update" CommandArgument='<%#Eval("[Port_Call_ID]")%>'
                                                                            ForeColor="Black" ImageUrl="~/Images/Agency_Work.jpg" ToolTip='<%# Eval("Agency_Work") %>'
                                                                            Height="16px"></asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="75%" style="color: Black; height: 10px; font-weight: bold">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" align="right">
                                        <table width="100px" runat="server" id="table2" style="height: 100px">
                                            <tr>
                                                <td width="75%" style="color: Black; font-weight: bold" align="right">
                                                    <asp:DropDownList ID="DDLPort" runat="server" Width="120px" CssClass="txtInput">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="75%" style="color: Black; font-weight: bold" align="right">
                                                    <asp:Button ID="btnsave" runat="server" Text="Add this port" OnClick="btnsave_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="75%" colspan="2" style="color: Black; font-weight: bold" align="left">
                                                    <asp:Button ID="btnAddNew" runat="server" OnClientClick='OpenScreen(null,null);return false;'
                                                        Text="Add new port" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <%--  <uc1:ucCustomPager ID="ucCustomPagerItems" Visible="false" runat="server" OnBindDataItem="BindPortCall" />--%>
                            <asp:HiddenField ID="HiddenFlag" Visible="false" runat="server" EnableViewState="False" />
                        </div>
                        <div align="left" style="border: 0px solid gray; margin-top: 1px;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <cc1:TabContainer ID="TabSCM" runat="server" ActiveTabIndex="2" OnActiveTabChanged="TabSCM_ActiveTabChanged"
                                        AutoPostBack="true">
                                        <cc1:TabPanel runat="server" HeaderText="Port Call Details" ID="TabPanel2" TabIndex="0">
                                            <ContentTemplate>
                                                <div style="height: 400px; overflow-x: hidden; overflow-y: scroll; max-height: 400px;">
                                                    <table style="margin-top: 10px;">
                                                        <tr>
                                                            <td valign="top">
                                                                <iframe id="iFrame1" runat="server" style="width: 1200px; height: 450px; border: 0px;">
                                                                </iframe>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="Template" ID="temp" TabIndex="1">
                                            <ContentTemplate>
                                                <div style="height: 400px; background-color: White; overflow-x: hidden; overflow-y: scroll;
                                                    max-height: 400px;">
                                                    <asp:GridView ID="gvTemplate" runat="server" EmptyDataText="NO RECORDS FOUND" ShowHeaderWhenEmpty="True"
                                                        AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" Width="90%" AllowSorting="True"
                                                        OnRowCancelingEdit="gvTemplate_RowCancelingEdit" OnRowEditing="gvTemplate_RowEditing"
                                                        OnRowUpdating="gvTemplate_RowUpdating" OnRowCommand="gvTemplate_RowCommand">
                                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="From Port">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="FromportName" runat="server" Text='<%# Eval("FROMPORTNAME")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                <ItemStyle Wrap="True" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sea Time">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtSeaTime" runat="server" Width="120px" Text='<%# Eval("SeaTime")%>'
                                                                        MaxLength="255"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSeaTime" Visible="true" runat="server" Width="80px" Text='<%# Eval("SeaTime")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                <ItemStyle Wrap="True" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Port Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="InPortName" runat="server" Text='<%# Eval("INPORTNAME")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                <ItemStyle Wrap="True" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="InPort Time">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtInportTime" runat="server" Width="80px" Text='<%# Eval("InPortTime")%>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPortTime" Visible="true" runat="server" Width="80px" Text='<%# Eval("InPortTime")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                <ItemStyle Wrap="True" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Charterers Agent">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlCAgent" runat="server" Width="280px" DataValueField="Charter_ID"
                                                                        DataTextField="CharterName" />
                                                                    <asp:Label ID="lblc" runat="server" Visible="false" Width="1px" Text='<%# Eval("Charter_ID")%>'></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblChartName" Visible="true" runat="server" Width="300px" Text='<%# Eval("CharterName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                <ItemStyle Wrap="True" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Owners Agent">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlOAgent" runat="server" DataValueField="Owners_ID" DataTextField="OwnerName"
                                                                        Width="280px" />
                                                                    <asp:Label ID="lblo" runat="server" Visible="false" Width="300px" Text='<%# Eval("Owners_ID")%>'></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblownersName" Visible="true" runat="server" Width="300px" Text='<%# Eval("OwnerName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                <ItemStyle Wrap="True" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Representative Email">
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton runat="server" AlternateText="Save" ImageUrl="~/Images/Save.png"
                                                                        CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdSave"
                                                                        ToolTip="Save"></asp:ImageButton><img id="Img2" runat="server" alt="" src="~/Images/transp.gif"
                                                                            width="3" /><asp:ImageButton runat="server" AlternateText="Cancel" ImageUrl="~/Images/Delete.png"
                                                                                CommandName="Cancel" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdCancel"
                                                                                ToolTip="Cancel"></asp:ImageButton>
                                                                </EditItemTemplate>
                                                                <HeaderTemplate>
                                                                    Action
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" AlternateText="Edit" ImageUrl="~/Images/edit.gif"
                                                                        CommandName="EDIT" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdEdit"
                                                                        Visible='<%# objUA.Edit ==0 ? false : true%>' ToolTip="Edit"></asp:ImageButton><img
                                                                            id="Img1" runat="server" alt="" src="~/Images/transp.gif" width="3" />
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                                        <RowStyle CssClass="RowStyle-css" />
                                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="Port Call History" ID="PortCall" TabIndex="2">
                                            <ContentTemplate>
                                                <div style="border: 0px solid Gray; margin-top: 0px">
                                                    <table style="margin-top: 10px;">
                                                        <tr>
                                                            <td valign="top" align="right">
                                                                Port Name :
                                                            </td>
                                                            <td valign="top" align="left">
                                                                <asp:DropDownList ID="ddlportfilter" runat="server" Width="200px" CssClass="txtInput">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td valign="top" align="right">
                                                                Vessel Name :
                                                            </td>
                                                            <td valign="top" align="left">
                                                                <asp:DropDownList ID="ddlvessel" runat="server" Width="200px" CssClass="txtInput">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                Start Date :
                                                            </td>
                                                            <td valign="top" align="left">
                                                                <asp:TextBox ID="txtStartDate" runat="server" Width="120px" CssClass="txtInput"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalStartDate" runat="server" TargetControlID="txtStartDate">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td align="right">
                                                                End Date :
                                                            </td>
                                                            <td valign="top" align="left">
                                                                <asp:TextBox ID="txtEndDate" runat="server" Width="120px" CssClass="txtInput"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalEndDate" runat="server" TargetControlID="txtEndDate">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td valign="top" align="left">
                                                                <asp:ImageButton ID="btnportfilter" runat="server" OnClick="btnportfilter_Click"
                                                                    ToolTip="Search" ImageUrl="~/Images/SearchButton.png" OnClientClick="return ValidateText();" />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR1" runat="server" visible="false">
                                                            <td valign="top" align="right">
                                                                Sort Arrival in :
                                                            </td>
                                                            <td valign="top" align="left">
                                                                <asp:RadioButtonList ID="rdborder" Width="200px" CssClass="txtInput" RepeatDirection="Horizontal"
                                                                    runat="server">
                                                                    <asp:ListItem Value="desc" Selected="True" Text="Descending"></asp:ListItem>
                                                                    <asp:ListItem Value="asc" Text="Ascending"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td valign="top" align="right">
                                                            </td>
                                                            <td valign="top" align="left">
                                                            </td>
                                                            <td valign="top" align="right">
                                                            </td>
                                                            <td valign="top" align="left">
                                                            </td>
                                                            <td valign="top" align="left">
                                                            </td>
                                                            <td valign="top" align="left">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div style="height: 400px; overflow-x: hidden; overflow-y: scroll; max-height: 400px;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="gvPortCallHistory" runat="server" EmptyDataText="NO RECORDS FOUND"
                                                                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1"
                                                                        Width="90%">
                                                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                                                        <RowStyle CssClass="RowStyle-css" />
                                                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Vessel Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVessel_Name" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Wrap="True" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                                                </ItemStyle>
                                                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Port Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPORT_NAME" Visible="true" runat="server" Width="300px" Text='<%# Eval("PORT_NAME")%>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Wrap="True" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                                                                </ItemStyle>
                                                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Arrival">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblArrival" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Arrival")))%>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Wrap="True" HorizontalAlign="Center" Width="200px" CssClass="PMSGridItemStyle-css">
                                                                                </ItemStyle>
                                                                                <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Departure">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDeparture" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Departure")))%>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Wrap="True" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                                </ItemStyle>
                                                                                <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Port Remarks">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPort_Remarks" Visible="true" runat="server" Width="250px" Text='<%# Eval("Port_Remarks")%>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Wrap="True" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                                                                </ItemStyle>
                                                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ShipCraneReq">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblShipCraneReq" Visible="true" runat="server" Width="40px" Text='<%# Eval("ShipCraneReq")%>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Wrap="True" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                                                </ItemStyle>
                                                                                <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="Port Call Alerts" ID="PortCallAlerts" TabIndex="3">
                                            <ContentTemplate>
                                                <div style="height: 400px; overflow-x: hidden; overflow-y: scroll; max-height: 400px;">
                                                    <table style="margin-top: 25px;">
                                                        <tr>
                                                            <td align="center" valign="top">
                                                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                                                    All Vessel Calling Singapore</div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="gvPortAlert" runat="server" EmptyDataText="NO RECORDS FOUND" ShowHeaderWhenEmpty="True"
                                                                    AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" Width="90%">
                                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                                    <RowStyle CssClass="RowStyle-css" />
                                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Vessel Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblVessel_Name" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Port Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPORT_NAME" Visible="true" runat="server" Width="300px" Text='<%# Eval("PORT_NAME")%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Arrival">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblArrival" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Arrival")))%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Departure">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDeparture" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Departure")))%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Port Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPort_Remarks" Visible="true" runat="server" Width="250px" Text='<%# Eval("Port_Remarks")%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ShipCraneReq">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblShipCraneReq" Visible="true" runat="server" Width="40px" Text='<%# Eval("ShipCraneReq")%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="Port Cost" ID="TabPanel1" TabIndex="4">
                                            <ContentTemplate>
                                                <div style="height: 400px; overflow-x: hidden; overflow-y: scroll; max-height: 400px;">
                                                    <table style="margin-top: 10px;">
                                                        <tr>
                                                            <td valign="top" align="right">
                                                                Port Name :
                                                            </td>
                                                            <td valign="top" align="left">
                                                                <asp:DropDownList ID="ddlPortCost" runat="server" Width="200px" CssClass="txtInput">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td valign="top" align="left">
                                                                <asp:ImageButton ID="ImgPortCost" runat="server" OnClick="ImgPortCost_Click" ToolTip="Search"
                                                                    ImageUrl="~/Images/SearchButton.png" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table style="margin-top: 25px;">
                                                        <tr>
                                                            <td>
                                                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                                                    Vessel Calls</div>
                                                            </td>
                                                            <td>
                                                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                                                    DA Items</div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="gvPortCost" runat="server" EmptyDataText="NO RECORDS FOUND" ShowHeaderWhenEmpty="True"
                                                                    AutoGenerateColumns="False" DataKeyNames="DA_ID" CellPadding="1" Width="90%"
                                                                    OnRowDataBound="gvPortCost_RowDataBound">
                                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                                    <RowStyle CssClass="RowStyle-css" />
                                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Vessel Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPVessel_Name" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Departure">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPArrival" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Arrival")))%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Departure">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPDeparture" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Departure")))%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Agent">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAgent" Visible="true" runat="server" Width="250px" Text='<%# Eval("Agent_Name")%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="DA Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStatus" Visible="true" runat="server" Width="40px" Text='<%# Eval("DA_Status")%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="DA Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAmount" Visible="true" runat="server" Width="40px" Text='<%# Eval("DA_Amount")%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Action">
                                                                            <HeaderTemplate>
                                                                                Action
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <table cellpadding="2" cellspacing="2">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="lbtnDAEdit" runat="server" CommandArgument='<%#Eval("[DA_ID]") + "," + Eval("[Agent_Code]") %>'
                                                                                                OnCommand="lbtnDAEdit_Click" ImageUrl="~/Images/SearchButton.png" Text="Edit">
                                                                                            </asp:ImageButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                            <td>
                                                                <asp:GridView ID="gvDAItem" runat="server" EmptyDataText="NO RECORDS FOUND" ShowHeaderWhenEmpty="True"
                                                                    AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" Width="90%">
                                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                                    <RowStyle CssClass="RowStyle-css" />
                                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Item">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblItem_Category" runat="server" Text='<%# Eval("Item_Category")%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Proforma Description">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPerfome_Desc" runat="server" Text='<%# Eval("Perfome_Desc")%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPamount" runat="server" Text='<%# Eval("Pamount")%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Final Description">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFinal_Desc" Visible="true" runat="server" Width="250px" Text='<%# Eval("Final_Desc")%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFAmount" Visible="true" runat="server" Width="40px" Text='<%# Eval("FAmount")%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Desc">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAmount" Visible="true" runat="server" Width="40px" Text='<%# Eval("Item_Desc")%>'></asp:Label></ItemTemplate>
                                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                                            </ItemStyle>
                                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="Crew List" ID="crew" TabIndex="5">
                                            <ContentTemplate>
                                                <div style="border: 0px solid Gray; margin-top: 0px">
                                                    <table style="margin-top: 10px;">
                                                        <tr>
                                                            <td valign="top">
                                                                Vessel :
                                                                <asp:DropDownList ID="ddlCrewVessel" runat="server" Width="200px" CssClass="txtInput">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td valign="top">
                                                                Crew list As of Date :
                                                                <asp:TextBox ID="txtAsofDate" runat="server" Width="120px" CssClass="txtInput" BackColor="#FFFFCC"></asp:TextBox><ajaxToolkit:CalendarExtender
                                                                    ID="calAsOfDate" runat="server" TargetControlID="txtAsofDate">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td valign="top">
                                                                Status : &nbsp;&nbsp;
                                                                <asp:DropDownList ID="ddlCrewStatus" runat="server" CssClass="txtInput" BackColor="#FFFFCC">
                                                                    <asp:ListItem Value="0" Text="ALL"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="Signed-Off"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Current" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:ImageButton ID="imgCrewFilter" runat="server" OnClick="imgCrewFilter_Click"
                                                                    ToolTip="Search" ImageUrl="~/Images/SearchButton.png" OnClientClick="return ValidateText1();" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div style="height: 400px; overflow-x: hidden; overflow-y: scroll; max-height: 400px;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="gvCrewList" runat="server" EmptyDataText="NO RECORDS FOUND" ShowHeaderWhenEmpty="True"
                                                                        AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" Width="90%">
                                                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                                                        <RowStyle CssClass="RowStyle-css" />
                                                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Staff Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSTAFF_CODE" runat="server" Text='<%# Eval("Staff_Code")%>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Wrap="True" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                                                                </ItemStyle>
                                                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Staff Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSTAFF_NAME" Visible="true" runat="server" Width="350px" Text='<%# Eval("Staff_FullName")%>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Wrap="True" HorizontalAlign="Left" Width="350px" CssClass="PMSGridItemStyle-css">
                                                                                </ItemStyle>
                                                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Rank Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRank_Name" runat="server" Text='<%# Eval("Rank_Name")%>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Wrap="True" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                                </ItemStyle>
                                                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nationality">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSTAFF_NATIONALITY" runat="server" Text='<%# Eval("Nationality")%>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Wrap="True" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                                </ItemStyle>
                                                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sign On Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSIGN_ON_DATE" Visible="true" runat="server" Width="150px" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Joining_Date")))%>' ></asp:Label></ItemTemplate>
                                                                                <ItemStyle Wrap="True" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                                </ItemStyle>
                                                                                <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sign On Port">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSIGN_ON_PORT" Visible="true" runat="server" Width="150px" Text='<%# Eval("JoiningPort")%>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Wrap="True" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                                </ItemStyle>
                                                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Est Sign Off">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEst_Sing_Off_Date" Visible="true" runat="server" Width="150px"
                                                                                        Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Est_Sing_Off_Date")))%>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle Wrap="True" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                                </ItemStyle>
                                                                                <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                    </cc1:TabContainer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </center>
</asp:Content>
