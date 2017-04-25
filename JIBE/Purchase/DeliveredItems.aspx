<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DeliveredItems.aspx.cs"
    Inherits="Technical_INV_DeliveredItems" Title="Delivered Items Entry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script language="javascript" type="text/javascript">

        function CloseDiv() {

            var control = document.getElementById("ctl00_MainContent_divCharge");
            control.style.visibility = "hidden";
        }

        function Validation() {
            var txtRoundoff = document.getElementById("ctl00_MainContent_txtRoundoff").value;

            if (txtRoundoff != "") {
                if (isNaN(txtRoundoff)) {
                    alert("Please enter numeric value.");
                    return false;
                }

            }

            return true;
        }

        function ValidationOnAddChrges() {

            var txtCharges = document.getElementById("ctl00_MainContent_txtAddChargeDesc").value;
            var txtAdditonalAmt = document.getElementById("ctl00_MainContent_txtAddnAmount").value;

            if (txtCharges == "") {
                alert("Please enter the Additional charge description.");
                return false;
            }

            if (txtAdditonalAmt == "") {
                alert("Please enter the Amount.");
                return false;
            }

            if (txtAdditonalAmt != "") {
                if (isNaN(txtAdditonalAmt)) {
                    alert("Please enter numeric value.");
                    return false;
                }

            }

            return true;
        }

        function MaskDecimal(evt) {
            if (!(evt.keyCode == 9 || evt.keyCode == 190 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 37 || evt.keyCode == 39 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105))) {
                return false;
            }
            var parts = evt.srcElement.value.split(',');

            if (parts.length > 2) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
            if (parts.length == 2 && parts[1].length >= 2) return false;
        }



    </script>
    <center>
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
                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="background-color: #808080; font-size: small; color: #FFFFFF;">
                            <b>Delivered Items Entry</b>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 95%; background-color: #f4ffff;
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
                        <td class="tdd" colspan="3">
                            <asp:Label ID="lblToDate" runat="server"></asp:Label>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <table cellpadding="0" cellspacing="0" align="center" style="height: 25px; width: 95%;
            background-color: #C0C0C0;">
            <tr align="left">
                <td style="width: 108px; font-size: small; color: #333333; background-color: #CCCCCC;"
                    align="left">
                    <b>&nbsp;Requisition/Order/Supplier :</b>
                </td>
                <td style="border: thin none #FFFFFF; background-color: #CCCCCC;" align="left">
                    <asp:DropDownList ID="cmbRequisitionList" runat="server" AutoPostBack="true" AppendDataBoundItems="True"
                        Width="258px" Font-Size="11px" OnSelectedIndexChanged="cmbRequisitionList_SelectedIndexChanged"
                      >
                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="background-color: #CCCCCC">
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" align="center" style="width: 95%; background-color: #808080;">
                    <tr style="background-color: #C0C0C0">
                        <td style="width: 80px; height: 15px; color: #FFFFFF; font-size: small; font-weight: 700;">
                            <asp:Label ID="lblSrchtitle" runat="server" Text="Search By :" Style="color: #333333"></asp:Label>
                        </td>
                        <td style="font-size: small; text-align: left;">
                            <asp:Label ID="lblSrchPartNo" runat="server" Text="Part No : " Style="color: #333333;"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <span style="font-size: small">
                                <asp:TextBox ID="txtSrchPartNo" runat="server" Style="font-size: small" Width="71px"></asp:TextBox>
                            </span>
                        </td>
                        <td>
                            <asp:Label ID="lblSrchDrawingNo" runat="server" Text="Drawing No : " Style="font-size: small;
                                color: #333333;"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSrchDrawingNo" runat="server" Style="font-size: small" Width="73px"></asp:TextBox>
                        </td>
                        <td style="font-size: small; text-align: left;">
                            <asp:Label ID="lblSrchDesc" runat="server" Text="Description :" Style="color: #333333;"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtSrchDesc" runat="server" Style="font-size: small" Width="112px"></asp:TextBox>
                        </td>
                        <td style="text-align: left">
                            <asp:Button ID="btnItemSearch" runat="server" Text="Search" Height="21px" Width="45px"
                                Font-Size="XX-Small" OnClick="btnItemSearch_Click" />
                        </td>
                        <td width="100" align="left" style="background-color: #C0C0C0">
                            <asp:Button ID="btnAddItem" runat="server" Visible="false" Style="font-size: small" Text="Additonal Charges"
                                OnClick="btnAddItem_Click" />
                        </td>
                        <td width="100" align="left">
                        </td>
                        <td width="100" align="left">
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="1" align="center" style="width: 95%; background-color: #C0C0C0;">
                    <tr>
                        <td align="left">
                            <telerik:RadGrid ID="rgdDeliveredItems" runat="server" AllowAutomaticInserts="True"
                                GridLines="None" Height="354px" Skin="WebBlue" Width="100%" AutoGenerateColumns="False"
                                OnItemDataBound="rgdDeliveredItems_ItemDataBound" AllowMultiRowSelection="True">
                                <MasterTableView ShowFooter="False">
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn Resizable="False" Visible="False">
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridBoundColumn UniqueName="Part_Number" HeaderText="Part No." Visible="false"
                                            DataField="Part_Number">
                                            <ItemStyle Width="50px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="Drawing_Number" HeaderText="Drawing No." Visible="false"
                                            DataField="Drawing_Number">
                                            <ItemStyle Width="50px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Short_Description" HeaderText="Description" UniqueName="Short_Description">
                                            <ItemStyle Width="350px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ROB_QTY" HeaderText="ROB" Display="false" UniqueName="ROB_QTY"
                                            DataFormatString="{0:N0}">
                                            <ItemStyle Width="50px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Req.Qty." UniqueName="REQUESTED_QTY" DataField="REQUESTED_QTY"
                                            DataFormatString="{0:N0}">
                                            <ItemStyle Width="60px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ORDER_QTY" HeaderText="Order Qty." UniqueName="ORDER_QTY"
                                            DataFormatString="{0:N0}">
                                            <ItemStyle Width="60px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Pending_Qty" HeaderText="Pend. Qty." UniqueName="Pending_Qty"
                                            DataFormatString="{0:N0}">
                                            <ItemStyle Width="60px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="ITEM_SERIAL_NO" Visible="false" UniqueName="ITEM_SERIAL_NO"
                                            DataField="ITEM_SERIAL_NO">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="REQUISITION_CODE" Visible="false" UniqueName="REQUISITION_CODE"
                                            DataField="REQUISITION_CODE">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="DELIVERY_CODE" UniqueName="DELIVERY_CODE" Visible="false"
                                            DataField="DELIVERY_CODE">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="DOCUMENT_CODE" HeaderText="DOCUMENT_CODE" Visible="false"
                                            DataField="DOCUMENT_CODE">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="APPROVED_QTY" HeaderText="APPROVED_QTY" UniqueName="APPROVED_QTY"
                                            Visible="False">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ITEM_SYSTEM_CODE" Visible="false" HeaderText="ITEM_SYSTEM_CODE"
                                            DataField="ITEM_SYSTEM_CODE">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ITEM_SUBSYSTEM_CODE" Visible="false" HeaderText="ITEM_SUBSYSTEM_CODE"
                                            DataField="ITEM_SUBSYSTEM_CODE">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ITEM_REF_CODE" HeaderText="ITEM CODE" Visible="true"
                                            UniqueName="ITEM_REF_CODE">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ITEM_INTERN_REF" Visible="false" HeaderText="ITEM_INTERN_REF"
                                            UniqueName="ITEM_INTERN_REF">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="Unit_and_Packings" HeaderText="U & P" DataField="Unit_and_Packings"
                                            Visible="False">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ORDER_SUPPLIER" HeaderText="ORDER_SUPPLIER" UniqueName="ORDER_SUPPLIER"
                                            Visible="False">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ITEM_COMMENT" HeaderText="ITEM_COMMENT" Visible="false"
                                            UniqueName="ITEM_COMMENT">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="REQUISITION_COMMENT" Visible="false" HeaderText="REQUISITION_COMMENT"
                                            UniqueName="REQUISITION_COMMENT">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Min_Qty" HeaderText="Min Qty" UniqueName="Min_Qty"
                                            Visible="False">
                                            <ItemStyle Width="0px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Max_Qty" HeaderText="Max Qty" Visible="False"
                                            UniqueName="Max_Qty">
                                            <ItemStyle Width="0px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Vessel_code" HeaderText="Vessel_code" Visible="false"
                                            UniqueName="Vessel_code">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Department_code" Visible="false" HeaderText="Department_code"
                                            UniqueName="Department_code">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="inventory_date" HeaderText="inventory_date" Visible="false"
                                            UniqueName="inventory_date">
                                            <ItemStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Inventory_qty" HeaderText="Inventory_qty" Visible="false"
                                            UniqueName="Inventory_qty">
                                            <ItemStyle Width="0px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Pending_Qty" HeaderText="Pend. Qty." UniqueName="Pending_Copy"
                                            DataFormatString="{0:N0}" Visible="false">
                                            <ItemStyle Width="60px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Deliverd Qty" UniqueName="DELIVERD_QTY">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDeliverdQty" runat="server" Text='<%#Eval("DELIVERd_QTY1")%> '
                                                    OnTextChanged="txtDelivered_Change" onKeydown="JavaScript:return MaskDecimal(event);"
                                                    Style="font-size: 9px; text-align: right" Width="50px" Height="12px" AutoPostBack="True"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="70px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="Rate" HeaderText="Unit Price" Visible="true"
                                            UniqueName="Rate" Display="false">
                                            <ItemStyle Width="60px" HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Amount" Display="false"  HeaderText="Amount"
                                            Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" Text='<%# Bind("Amount")%>' runat="server" Style="font-size: small;
                                                    text-align: right" Width="55"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Update ROB">
                                            <ItemTemplate>
                                                <asp:Label ID="txtUpdateROB" runat="server" Text='<%#Eval("DELIVERd_QTY1")%> ' Style="font-size: small;
                                                    text-align: right" Width="55"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Delivery Remarks" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDeliveryRemarks" TextMode="MultiLine" runat="server" ToolTip="Enter Delivey Remarks" Width="200px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <PopUpSettings ScrollBars="Horizontal" />
                                    </EditFormSettings>
                                </MasterTableView>
                                <ClientSettings>
                                    <Scrolling AllowScroll="true" UseStaticHeaders="false" />
                                </ClientSettings>
                            </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" align="center" style="height: 25px; width: 95%;
                    background-color: #C0C0C0;">
                    <tr>
                        
                        <td style="color: #333333;">
                            <asp:CheckBox ID="Paycheck" Text="Pay On Office" Checked="true" runat="server" Style="font-size: small;
                                font-weight: 700; color: #333333;" BackColor="#FFCCFF" BorderColor="#000099"
                                Visible="false" />
                        </td>
                        <td colspan="4">

                          <asp:Button ID="btnDeliver" runat="server" Text="Deliver" Style="font-size: small"
                                OnClick="btnDeliver_Click" />

                                 <asp:Button ID="btnZeroAllPending" runat="server" Text="Zero All Pending" Style="font-size: small"
                                 OnClick="btnZeroAllPending_Click" />

                                  <asp:Button ID="btnRefresh" runat="server" Visible="false" Text="Refresh" Style="font-size: small"
                                OnClick="btnRefresh_Click" />

                            <asp:Button ID="btnSave" runat="server" Text="Save" Style="font-size: small;" Width="60px"
                                OnClientClick="return Validation();" OnClick="btnSave_Click" />
                            <%-- </td>  
                       
                 <td>--%>
                           
                            <%--</td>   
                       
               
                 
                     <td>--%>
                          
                            <%-- </td>
                  
                   <td>--%>
                           
                        </td>
                        <!-- jj -->
                        <td width="200px" style="height: 19px; font-size: small;" align="right">
                            Total Item to be delivered:
                        </td>
                        <td width="70" style="height: 19px" align="left">
                            <asp:TextBox ID="txtItems" runat="server" Style="font-size: small; text-align: right"
                                Width="51px" Enabled="False" Text="0"></asp:TextBox>
                        </td>
                       
                        <td width="50" style="height: 19px" align="left">
                            <asp:TextBox ID="txtAmount" runat="server" Visible="false" Style="font-size: small; text-align: right"
                                Width="61px" Enabled="False" Text="0"></asp:TextBox>
                        </td>
                        <!-- jj-->
                       
                        <td align="left" style="height: 19px">
                            <asp:HiddenField ID="buttonClickStatus" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="color: #333333;">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td colspan="4" style="height: 19px; font-size: small;" align="right">
                           
                        </td>
                        <td align="left" style="height: 19px" width="50">
                            <asp:TextBox ID="txtRoundoff" runat="server" Visible="false" Style="font-size: small; text-align: right"
                                onKeydown="JavaScript:return MaskDecimal(event);" Width="61px" Text="0"></asp:TextBox>
                        </td>
                        <td align="left" style="height: 19px">
                            &nbsp;
                        </td>
                        <td align="left" style="height: 19px">
                            &nbsp;
                        </td>
                        <td align="left" style="height: 19px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" align="center" style="height: 25px; width: 95%;
                    background-color: #C0C0C0;">
                    <tr>
                        <td>
                            <asp:Label ID="lblError" runat="server" Width="697px" Height="16px" ForeColor="#FF3300"
                                Style="font-size: small"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAddItem" />
                <asp:PostBackTrigger ControlID="btnAddition" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="divCharge" style="background-color: #E0E0E0; height: 120px; width: 369px;
            position: absolute; left: 30%; top: 12%; z-index: 2; color: black" runat="server"
            visible="False">
            <table border="1" cellpadding="0" cellspacing="0" style="height: 100px; width: 367px">
                <tr>
                    <td>
                        <table style="width: 363px; height: 12px;" cellpadding="0" cellspacing="0">
                            <tr align="center">
                                <td style="background-color: #808080; font-size: small; color: #FFFFFF;">
                                    Add Additional Charges
                                </td>
                                <td align="right" style="width: 16px; background-color: #808080;">
                                    <%--<asp:ImageButton ID="ImageButton1" ImageUrl="~/Technical/INV/Image/Close.gif" 
                                    runat="server" style="font-size: small" Width="14px" Height="14px" 
                                    onclick="btncancel_Click"/>--%>
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
                                    Description:
                                </td>
                                <%--           <td style="font-size: small; color: #FF3300;">*</td>--%>
                                <td align="left">
                                    <asp:TextBox ID="txtAddChargeDesc" Style="font-size: small" runat="server" Height="39px"
                                        TextMode="MultiLine" Width="281px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="font-size: small">
                                    Amount:
                                </td>
                                <%-- <td style="font-size: small; color: #FF3300;">*</td>--%>
                                <td align="left">
                                    <asp:TextBox ID="txtAddnAmount" runat="server" Style="font-size: small"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnAddition" runat="server" Style="font-size: small" Text="Save"
                                        OnClientClick="return ValidationOnAddChrges();" OnClick="btnAddition_Click" />
                                </td>
                                <td align="center" style="width: 41px">
                                    <%--<asp:Button ID="btncancel" runat="server" style="font-size: small" 
                           Text="Cancel" onclick="btncancel_Click" />--%>
                                    <input type="button" name="btnCancel" style="font-size: small" onclick="JavaScript:CloseDiv();"
                                        value="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </center>
</asp:Content>
