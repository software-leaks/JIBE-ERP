<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ApprovedPurchaseOrder.aspx.cs"
    Inherits="Technical_INV_ApprovedPurchaseOrder" Title="Raise Purchase Order" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControl/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="uc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" Namespace="RJS.Web.WebControl"
    TagPrefix="RJS" %>
<asp:Content ID="Contenthead" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../CSS/StyleSheetMsg.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
       <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function CloseDiv() {

            var control = document.getElementById("ctl00_MainContent_divApprove");
            control.style.visibility = "hidden";
        }

        function CloseDiv2() {

            var control = document.getElementById("ctl00_MainContent_divpurcomment");
            control.style.visibility = "hidden";
            document.getElementById("ctl00_MainContent_btnSendOrder").disabled = false;
        }

        function Validation() {
           
            var DDLPort = document.getElementById("ctl00_MainContent_DDLPort").value;
            var txtdlvins = document.getElementById("ctl00_MainContent_txtdlvins").value;
            var txteta = document.getElementById("ctl00_MainContent_txteta").value;
            var txtetd = document.getElementById("ctl00_MainContent_txtetd").value; ;

            var txtETATime = document.getElementById("ctl00_MainContent_txtETAAPPM").value; ;
            var txtETDTime = document.getElementById("ctl00_MainContent_txtETDAMPM").value; ;


            var dt1 = parseInt(txteta.substring(0, 2), 10);
            var mon1 = parseInt(txteta.substring(3, 5), 10);
            var yr1 = parseInt(txteta.substring(6, 10), 10);

            var dt2 = parseInt(txtetd.substring(0, 2), 10);
            var mon2 = parseInt(txtetd.substring(3, 5), 10);
            var yr2 = parseInt(txtetd.substring(6, 10), 10);


            var ArrivalDt = new Date(yr1, mon1, dt1);
            var DepartureDate = new Date(yr2, mon2, dt2);

            if (txteta != "") {
                if (txtetd == "") {
                    alert("Please select date for vessel ETD.");
                    return false;
                }
            }
            if (txtetd != "") {
                if (txteta == "") {
                    alert("Please select date for vessel ETA.");
                    return false;
                }
            }
            if (txteta != "" && txtetd != "") {
                if (ArrivalDt > DepartureDate) 
                {
                    alert("Vessel ETD should be greater than Vessel ETA.");// JIT_11426 :CHANGES MESSAge as mentioned .
                    return false;
                }
            }

            if (txtETATime.split(":")[0] > 24 || txtETATime.split(":")[1] > 60 || txtETDTime.split(":")[0] > 24 || txtETDTime.split(":")[1] > 60) {
                alert("Please enter correct time");
                return false;
            }

        }

        function BlockEnterAndAllowtime(varNumericFlag) {
            if (varNumericFlag == 1) {
                if (event.keyCode == 58) {
                    return true;
                }
                else if (event.keyCode < 48 || event.keyCode > 57) {
                    event.keyCode = null; return false;
                }
                else {
                    return true;
                }
            }
            else {
                if (event.keyCode == 13) {
                    event.keyCode = null; return false;
                }
                else
                    return true;
            }
        }

        function GetAgentDetails(selectedItem) {
            Async_Get_SupplierDetails_ByCode(selectedItem.value);


        }
        function response_getSupplierDetails(retval) {
            var ar, arS;

            if (retval.indexOf('Working') >= 0) { return; }
            try {

                retval = clearXMLTags(retval);

                if (retval.indexOf('ERROR:', 0) >= 0 || retval.indexOf('error', 0) >= 0) {
                    alert(retval);
                    return;
                }
                if (retval.trim().length > 0) {
                    var arVal = eval(retval);

                    document.getElementById("ctl00_MainContent_txtagent").value = String(arVal[0].fullname);
                    document.getElementById("ctl00_MainContent_txtagent").value += "\r\n" + String(arVal[0].address);
                    document.getElementById("ctl00_MainContent_txtagent").value += "\r\n" + String(arVal[0].phone);
                    document.getElementById("ctl00_MainContent_txtagent").value += "\r\n" + String(arVal[0].email);
                    document.getElementById("ctl00_MainContent_lblOnerCharts").value = "";
                }
            }
            catch (ex) { alert(ex.message); }
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>
                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="background-color: #808080; font-size: small; color: #FFFFFF;">
                            <b>Raise Purchase Order</b>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #f4ffff;
                    border-right: 1px solid gray; border-top: 1px solid gray">
                    <tr>
                        <td class="tdh">
                            Vessel :
                        </td>
                        <td class="tdd">
                            <asp:Label ID="lblVessel" runat="server"></asp:Label>
                        </td>
                        <td class="tdh">
                            Date :
                        </td>
                        <td class="tdd" colspan="2">
                            <asp:Label ID="lblToDate" runat="server"></asp:Label>
                        </td>
                        <td class="tdd">
                            <asp:HyperLink ID="hlinkViewEval" runat="server" Target="_blank" Text="View evaluation"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdh">
                            Catalogue :
                        </td>
                        <td class="tdd">
                            <asp:Label ID="lblCatalog" runat="server"></asp:Label>
                        </td>
                        <td class="tdh">
                            Requisition Number :
                        </td>
                        <td class="tdd">
                            <asp:HyperLink ID="lblReqNo" Target="_blank" runat="server"> </asp:HyperLink>
                        </td>
                        <td class="tdh">
                            Total Item :
                        </td>
                        <td class="tdd">
                            <asp:Label ID="lblTotalItem" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center">
                            <div class="freezing" style="margin-left: 0px; width: 100%;">
                                <asp:Label ID="lblmsgt" ForeColor="Red" runat="server"></asp:Label>
                                <telerik:RadGrid ID="rgdSuppliers" runat="server" AllowAutomaticInserts="True" AllowPaging="True"
                                    Height="389px" GridLines="None" Skin="WebBlue" Width="100%" AutoGenerateColumns="False"
                                    OnNeedDataSource="rgdSuppliers_NeedDataSource" OnDetailTableDataBind="rgdSuppliers_DetailTableDataBind">
                                    <MasterTableView DataKeyNames="SUPPLIER" Width="100%" AllowPaging="False">
                                        <DetailTables>
                                            <telerik:GridTableView DataKeyNames="ID" Name="Items" Width="98%" ShowFooter="true"
                                                HierarchyDefaultExpanded="true" AllowPaging="False">
                                                <RowIndicatorColumn Visible="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Visible="False" Resizable="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn SortExpression="ID" HeaderText="ID" DataField="ID" UniqueName="ID">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="Part_Number" HeaderText="Part No." DataField="Part_Number"
                                                        UniqueName="Part_Number">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="Short_Description" HeaderText="Item Name"
                                                        DataField="Short_Description" UniqueName="Short_Description">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="Unit_and_Packings" HeaderText="Unit" DataField="Unit_and_Packings"
                                                        UniqueName="Unit_and_Packings">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="REQUESTED_QTY" HeaderText="Rqst.Qty." DataField="REQUESTED_QTY"
                                                        UniqueName="REQUESTED_QTY">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="Order_QTY" HeaderText="Order Qty." DataField="Order_QTY"
                                                        UniqueName="Order_QTY">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="UnitPrice" HeaderText="Unit price" DataField="UnitPrice"
                                                        UniqueName="UnitPrice">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="QUOTED_DISCOUNT" HeaderText="Discount" DataField="QUOTED_DISCOUNT"
                                                        UniqueName="QUOTED_DISCOUNT">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="Lead_Time" HeaderText="Lead Time(In days)" DataField="Lead_Time"
                                                        UniqueName="Lead_Time">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="Amount" HeaderText="Amount" DataField="Amount"
                                                        UniqueName="Amount">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <PopUpSettings ScrollBars="None"></PopUpSettings>
                                                </EditFormSettings>
                                            </telerik:GridTableView>
                                        </DetailTables>
                                        <RowIndicatorColumn Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Resizable="False" Visible="False">
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="QUOTATION_CODE" DataField="QUOTATION_CODE" HeaderText="Quotation Code"
                                                Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="SUPPLIER" DataField="SUPPLIER" HeaderText="Supplier Code">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="SHORT_NAME" DataField="SHORT_NAME" HeaderText="Supplier Name"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Supp. Name">
                                                <ItemTemplate>
                                                    <a href="ViewSupplierDetails.aspx?SupplierCode=<%# Eval("SUPPLIER") %> " target="_blank">
                                                        <%# DataBinder.Eval(Container.DataItem, "SHORT_NAME")%></a>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="Total_Item" DataField="Total_Item" HeaderText="Selected Items">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Currency" DataField="Currency" HeaderText="Currency">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Total_Amount_Local" DataField="Total_Amount_Local"
                                                HeaderText="Total Amount">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Total_Amount_Uds" DataField="Total_Amount_Uds"
                                                HeaderText="Total in USD">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="send_date" UniqueName="send_date" HeaderText="Approved Date">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="APPROVED_Status" UniqueName="APPROVED_Status"
                                                HeaderText="Approve Status">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ManagerName" UniqueName="ManagerName" HeaderText="Approver">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="Select PO" AllowFiltering="false"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSendOrder" Checked="false" runat="server" Font-Size="Smaller" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Approve PO" UniqueName="ApprovePO" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Button runat="server" ID="btnImgApprive" OnCommand="onSelectApprovePO" CommandName="ApprovePO"
                                                        OnClientClick="aspnetForm.target ='_blank'" CommandArgument='<%# Eval("QUOTATION_CODE") +","+ Eval("SUPPLIER")+","+Eval("APPROVED_Status") +","+Eval("APPROVER") %>'
                                                        Text="Mark as approve" Font-Size="XX-Small" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Preview" UniqueName="Preview">
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="ImgPreviewBtn" Height="18px" Width="18px" OnCommand="onSelectPOPreview"
                                                        CommandName="Preview" OnClientClick="aspnetForm.target ='_blank'" CommandArgument='<%#Eval("QUOTATION_CODE") +","+ Eval("SUPPLIER") %>'
                                                        ImageUrl="~/purchase/image/preview.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Add Item" Visible="false"
                                                UniqueName="TemplateColumnImgAddItm">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgAddItm" runat="server" Text="Select" OnCommand="onAddNewItem"
                                                        CommandName="onAddNewItem" CommandArgument='<%#Eval("REQUISITION_CODE")+"&VCode="+Eval("Vessel_Code")+"&sOrderCode="+Eval("ORDER_CODE")+"&ReqStage=APR"%>'
                                                        ForeColor="Black" ToolTip="Add Additional Items with Requisition" ImageUrl="~/purchase/Image/briefcase-add.gif"
                                                        Width="14px" Height="14px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="32px" />
                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="Delivery_Instructions" DataField="Delivery_Instructions"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Delivery_Port" DataField="Delivery_Port" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ORDER_CODE" DataField="ORDER_CODE" Visible="false">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <PopUpSettings ScrollBars="None" />
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling UseStaticHeaders="false" AllowScroll="true" SaveScrollPosition="true" />
                                        <Resizing EnableRealTimeResize="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
                <table cellpadding="2" cellspacing="0" style="width: 100%; background-color: #f4ffff">
                    <tr align="center">
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSendOrder" runat="server" OnClick="btnSendOrder_Click" Height="30px"
                                        Style="font-size: small" Text="Send PO" Width="149px" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Label ID="lblError" Width="500px" runat="server" ForeColor="#FF3300"></asp:Label>
                        </td>
                    </tr>
                </table>
            </center>
            <div id="divApprove" style="background-color: #E0E0E0; height: 100px; width: 369px;
                position: absolute; left: 28%; top: 29%; z-index: 2; color: black" runat="server">
                <table border="1" cellpadding="0" cellspacing="0" style="height: 100px; width: 367px">
                    <tr>
                        <td>
                            <table style="width: 363px; height: 12px;" cellpadding="0" cellspacing="0">
                                <tr align="center">
                                    <td style="background-color: #808080; font-size: small;">
                                        Approve PO
                                    </td>
                                    <td align="right" style="width: 16px; background-color: #808080;">
                                        <img src="Image/Close.gif" alt="Click to close." width="12px" height="12px" onclick="JavaScript:CloseDiv();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100px; height: 58px;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right" style="font-size: small">
                                        Approval Comments: <span style="color: Red">*</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtComment" runat="server" Height="40px" TextMode="MultiLine" Width="306px"
                                            Style="font-size: small"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:HiddenField ID="HiddenArgument" runat="server" Value="" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnApprove" runat="server" Text="Appove PO" Style="font-size: small" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtComment"
                                            ErrorMessage="Comments are mandatory"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divpurcomment" style="width: 800px; position: absolute; top: 20%; left: 30%;
                padding: 15px; border: 1px solid black; top: 20%; z-index: 2; color: black;"
                runat="server" class="popup-css">
                <table border="1" cellspacing="0" style="width: 100%; border-collapse: collapse">
                    <tr>
                        <td>
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr align="center">
                                    <td style="background-color: #808080; font-size: small; color: #FFFFFF; font-weight: bold;">
                                        PO Details
                                    </td>
                                    <td align="right" style="width: 16px; background-color: #808080;">
                                        <img src="Image/Close.gif" alt="Click to close." width="12px" height="12px" onclick="JavaScript:CloseDiv2();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="margin: 5px">
                            <div style="max-height: 200px; width: 800px; overflow: auto; text-align: center">
                                <asp:GridView ID="gvPortCalls" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#336666" BorderStyle="Double" BorderWidth="1px" CellPadding="2"
                                    GridLines="Horizontal" DataKeyNames="Port_Call_ID" Font-Size="11px" Width="780px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vessel" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVessel_Short_Name" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Port Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPort_Name" runat="server" Text='<%# Eval("Port_Name")%>'></asp:Label>
                                                <asp:HiddenField ID="hdnPortID" runat="server" Value='<%# Eval("Port_ID")%>'></asp:HiddenField>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Arrival">
                                            <ItemTemplate>
                                                <asp:Label ID="lblArrival" runat="server" Text='<%# Eval("Arrival","{0:dd/MM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Departure" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeparture" runat="server" Text='<%# Eval("Departure","{0:dd/MM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Owners Agent" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOwners_Agent" runat="server" Text='<%# Eval("Owners_Agent")%>'
                                                    ToolTip='<%#Eval("Owners_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Charterers Agent" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCharterers_Agent" runat="server" Text='<%# Eval("Charterers_Agent")%>'
                                                    ToolTip='<%#Eval("Charterers_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkSelect" runat="server" ImageUrl="~/images/select1.gif" CausesValidation="False"
                                                    CommandName="Select" OnCommand="lnkSelect_Click" AlternateText="Select"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoRec" runat="server" Text="No record found."></asp:Label>
                                    </EmptyDataTemplate>
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#ffffff" ForeColor="Black" HorizontalAlign="Center" Font-Size="Large"
                                        CssClass="pager" />
                                    <RowStyle BackColor="White" ForeColor="#333333" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#F7BE81" Font-Bold="True" ForeColor="Black" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#487575" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#275353" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 507px; height: 155px;" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="2">
                                        <uc1:MyMessageBox ID="MyMessageBox6" runat="server" ShowCloseButton="true" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: small; font-weight: 700;">
                                        Vessel ETA:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txteta" runat="server" Style="font-size: small" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtenderETA" TargetControlID="txteta" Format="dd/MM/yyyy"
                                            runat="server">
                                        </cc1:CalendarExtender>
                                        <asp:TextBox ID="txtETAAPPM" Width="50px" Text="00:00" Style="font-size: small" runat="server"
                                            MaxLength="5"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="txtETAAPPM" Mask="99:99"
                                            AcceptAMPM="false" AcceptNegative="None" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                            MessageValidatorTip="true" runat="server" ErrorTooltipEnabled="true">
                                        </cc1:MaskedEditExtender>
                                        (hh:mm)24 Hours
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: small; font-weight: 700;">
                                        Vessel ETD:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtetd" runat="server" Style="font-size: small" Width="120px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtenderETD" Format="dd/MM/yyyy" TargetControlID="txtetd"
                                            runat="server">
                                        </cc1:CalendarExtender>
                                        <asp:TextBox ID="txtETDAMPM" Width="50px" Style="font-size: small" Text="00:00" runat="server"
                                            MaxLength="5"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="txtETDAMPM" Mask="99:99"
                                            AcceptAMPM="false" AcceptNegative="None" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                            MessageValidatorTip="true" runat="server" ErrorTooltipEnabled="true">
                                        </cc1:MaskedEditExtender>
                                        (hh:mm)24 Hours
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: small; font-weight: 700;">
                                        Delivery Port:
                                    </td>
                                    <td style="color: #FF0000; font-size: small;" align="left">
                                        <asp:DropDownList ID="DDLPort" runat="server" DataTextField="Port_Name" DataValueField="Id"
                                            Style="font-size: 12px" Width="194px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: small; font-weight: 700; text-align: right; width: 120px">
                                        Delivery Instructions:
                                    </td>
                                    <td style="color: #FF0000; font-size: small;" align="left">
                                        <asp:TextBox ID="txtdlvins" runat="server" Height="40px" TextMode="MultiLine" Width="500px"
                                            MaxLength="1000" Style="font-size: small"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table cellpadding="2" cellspacing="0">
                                            <tr>
                                                <td align="right" style="font-size: small; font-weight: 700; width: 120px">
                                                    Agent:
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlSentFrom" Font-Size="12px" runat="server" Width="220px"
                                                        AutoPostBack="false" onChange="GetAgentDetails(this)">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblOnerCharts" ForeColor="Red" Font-Size="10px" Font-Names="verdana"
                                                        runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-size: small; font-weight: 700;" align="right">
                                                    Details:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtagent" runat="server" Height="80px" Style="font-size: small"
                                                        TextMode="MultiLine" Width="500px" MaxLength="1000"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: small; font-weight: 700;">
                                        Other Remarks:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtremark" runat="server" Height="80px" TextMode="MultiLine" Width="500px"
                                            MaxLength="1000" Style="font-size: small"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="rbtnIncludeAmount" Text="Send PO with value" Checked="true"
                                            ForeColor="Black" GroupName="poamount" runat="server" />
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="rbtnexcludeAmount" Text="Send PO without value" GroupName="poamount"
                                            ForeColor="Black" runat="server" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnSendPOContinue" Visible="false" runat="server" OnClick="btnSendPOContinue_click"
                                            Style="font-size: small" Text="Continue.." />
                                    </td>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnSendPO" runat="server" OnClick="btnSendPO_click" Height="30px"
                                             Style="font-size: small" Text="Save &amp; Continue.." />
                                        &nbsp;&nbsp;
                                        <input type="button" name="btnCancel" style="font-size: small; height: 30px" onclick="JavaScript:CloseDiv2();"
                                            value="Cancel" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2" style="color: #FF0000; font-size: small;">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
