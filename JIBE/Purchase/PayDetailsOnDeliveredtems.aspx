<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PayDetailsOnDeliveredtems.aspx.cs" Inherits="Technical_INV_PayDetailsOnDeliveredtems"
    Title="View Delivered Requisition" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="RJS" Namespace="RJS.Web.WebControl" Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>
                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="background-color: #808080; font-size: small; color: #FFFFFF;">
                            <b>View Delivered Requisition</b>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" align="center" style="height: 25px; width: 930px;
                    background-color: #C0C0C0;">
                    <tr>
                        <td width="100" align="left" style="background-color: #CCCCCC">
                        </td>
                        <td align="right" style="width: 77px; font-size: small; color: #333333; background-color: #CCCCCC;">
                            <b>Fleet : </b>
                        </td>
                        <td style="width: 116px; background-color: #CCCCCC;" align="left">
                            <b>
                                <asp:DropDownList ID="DDLFleet" runat="server" Width="109px" Font-Size="XX-Small"
                                    AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="0">ALL</asp:ListItem>
                                </asp:DropDownList>
                            </b>
                        </td>
                        <td width="100" align="left" style="background-color: #CCCCCC; width: 200px;" 
                            colspan="2">
                            <asp:RadioButtonList ID="optList" runat="server" AutoPostBack="True" 
                                Font-Size="XX-Small" ForeColor="White" 
                                OnSelectedIndexChanged="optList_SelectedIndexChanged" 
                                RepeatDirection="Horizontal" Style="color: #333333" Width="200px">
                            </asp:RadioButtonList>
                        </td>
                        <td width="100" align="left" style="background-color: #CCCCCC">
                        </td>
                        <td width="100" align="left" style="background-color: #CCCCCC" colspan="2">
                        </td>
                        <td width="100" align="left" style="background-color: #CCCCCC">
                        </td>
                    </tr>
                    <tr>
                        <td width="100" align="right">
                        </td>
                        <td align="right" style="font-size: small; color: #333333;">
                            <b>Vessel :</b>
                        </td>
                        <td width="100" align="left">
                            <b>
                                <asp:DropDownList ID="DDLVessel" runat="server" Width="109px" Font-Size="XX-Small"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem Selected="True" Value="0">ALL</asp:ListItem>
                                </asp:DropDownList>
                            </b>
                        </td>
                        <td align="right" style="font-size:small">
                            <b>Department :</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="cmbDept" runat="server" AppendDataBoundItems="True" 
                                AutoPostBack="true" Font-Size="XX-Small" 
                                OnSelectedIndexChanged="cmbDept_OnSelectedIndexChanged" Width="109px">
                                <asp:ListItem Selected="True" Value="0">ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td  align="right" style="font-size:small">
                            Catalog :
                        </td>
                        <td  align="left" colspan="2">
                            <asp:DropDownList ID="cmbCatalog" runat="server" AppendDataBoundItems="True" 
                                Font-Size="XX-Small" Width="110px">
                                <asp:ListItem Selected="True" Value="0">ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="100" align="left">
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" 
                                Style="font-size: small" Text="Retrieve" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="100">
                            &nbsp;</td>
                        <td align="right" style="font-size: small; color: #333333;">
                            <b>From Date:</b></td>
                        <td align="left" colspan="2" >
                            <asp:TextBox ID="txtfrom" runat="server" Style="font-size: small" >
                                </asp:TextBox>
                            <RJS:PopCalendar ID="frcal" runat="server" Control="txtfrom" />
                        </td>
                        <td align="right" style="font-size: small; color: #333333;" >
                            To Date :</td>
                        <td align="left" style="font-size: small; color: #333333;" colspan="2">
                            <asp:TextBox ID="txtto" runat="server" Style="font-size: small"></asp:TextBox>
                            <RJS:PopCalendar ID="tocal" runat="server" Control="txtto" />
                        </td>
                        <td align="left" style="font-size: small; color: #333333;">
                            &nbsp;</td>
                        <td align="left" style="font-size: small; color: #333333;">
                            <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" 
                                Style="font-size: small" Text="Refresh" />
                        </td>
                        <td align="left" width="100">
                            &nbsp;</td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="1" align="center" style="width: 930px; background-color: #C0C0C0;">
                    <tr align="left" style="font-size: small">
                        <td style="width: 109px">
                        </td>
                        <td style="font-size: small; color: #333333" align="right">
                            &nbsp;</td>
                        <td style="width: 156px">
                            &nbsp;</td>
                        <td style="font-size: small; color: #333333"  align="right">
                            &nbsp;</td>
                        <td style="width: 130px">
                            &nbsp;</td>
                        <td style="width: 60px">
                            &nbsp;</td>
                        <td style="width: 109px">
                            &nbsp;</td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="1" align="center" style="width: 930px; background-color: #CCCCCC;">
                    <tr align="center">
                        <td>
                            <div class="freezing" style="height: 400px; overflow: scroll; margin-left: 0px; width: 930px;">
                                <telerik:RadGrid ID="rgdPayDetails" runat="server" AllowAutomaticInserts="True" GridLines="None"
                                    Height="370px" Skin="WebBlue" Width="910px" AutoGenerateColumns="False" PageSize="5"
                                    ShowDesignTimeSmartTagMessage="False" OnDetailTableDataBind="rgdPayDetails_DetailTableDataBind"
                                    OnNeedDataSource="rgdPayDetails_NeedDataSource" OnSelectedIndexChanged="rgdPayDetails_SelectedIndexChanged"
                                    Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Larger"
                                    Font-Strikeout="False" Font-Underline="False">
                                    <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <MasterTableView DataKeyNames="REQUISITION_CODE ,DELIVERY_CODE ,Vessel_Code" AllowMultiColumnSorting="True"
                                        PageSize="20">
                                        <DetailTables>
                                            <telerik:GridTableView DataKeyNames="ID" Name="Items" Width="100%" Font-Bold="False"
                                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                ShowFooter="False">
                                                <RowIndicatorColumn Visible="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Visible="False" Resizable="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <%--<telerik:GridBoundColumn SortExpression="ID" HeaderText="ID" DataField="ID" UniqueName="ID">
                                                    </telerik:GridBoundColumn>--%>
                                                    
                                                    
                                                     <telerik:GridBoundColumn SortExpression="Drawing_Number" HeaderText="Drawing No." DataField="Drawing_Number"
                                                        UniqueName="Drawing_Number">
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
                                                  
                                                    <telerik:GridBoundColumn DataField="ORDER_RATE" HeaderText="Order Rate" UniqueName="ORDER_RATE"
                                                        DataFormatString="{0:F2}">
                                                        <ItemStyle HorizontalAlign="Right" /> 
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ORDER_DISCOUNT" HeaderText="Order Discount%"
                                                        UniqueName="ORDER_DISCOUNT" DataFormatString="{0:F2}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ORDER_PRICE" HeaderText="Order Price" UniqueName="ORDER_PRICE"
                                                        DataFormatString="{0:F2}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ORDER_VAT" HeaderText="Order Vat" UniqueName="ORDER_VAT"
                                                        DataFormatString="{0:F2}">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    
                                                    <telerik:GridBoundColumn DataField="ORDER_QTY" HeaderText="Order Qty." UniqueName="ORDER_QTY">
                                                    <ItemStyle HorizontalAlign="NotSet" />
                                                    </telerik:GridBoundColumn>
                                                    
                                                     <telerik:GridBoundColumn DataField="DELIVERD_QTY" HeaderText="Delivered Qty." UniqueName="DELIVERD_QTY"
                                                        DataFormatString="{0:F2}">
                                                        <ItemStyle HorizontalAlign="NotSet" />
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
                                        
                                            
                                            <telerik:GridBoundColumn UniqueName="REQUISITION_CODE" DataField="REQUISITION_CODE"
                                                HeaderText="Requisition No.">
                                                <ItemStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            
                                            <telerik:GridBoundColumn SortExpression="SHORT_NAME" DataField="SHORT_NAME" HeaderText="Supplier" UniqueName="SHORT_NAME">
                                            </telerik:GridBoundColumn>
                                            
                                            <telerik:GridBoundColumn DataField="ORDER_CODE" HeaderText="ORDER NO" UniqueName = "ORDER_CODE">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="DELIVERY_DATE" DataField="DELIVERY_DATE" HeaderText="Delivery Date">
                                                <ItemStyle Width="80px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="name_dept" DataField="name_dept" HeaderText="Department">
                                                <ItemStyle Width="150px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="System_Description" DataField="System_Description"
                                                HeaderText="Catalog" Visible="false">
                                                <ItemStyle Width="250px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Vessel_Code" Visible="false" DataField="Vessel_Code"
                                                HeaderText="Vessel_Code">
                                                <ItemStyle Width="0px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="DEPARTMENT" Visible="false" DataField="DEPARTMENT"
                                                HeaderText="Department">
                                                <ItemStyle Width="150px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ITEM_SYSTEM_CODE" Visible="false" DataField="ITEM_SYSTEM_CODE"
                                                HeaderText="ITEM_SYSTEM_CODE">
                                                <ItemStyle Width="0px" />
                                            </telerik:GridBoundColumn>
                                            <%--       <telerik:GridBoundColumn 
                                                    UniqueName="ORDER_SUPPLIER" DataField="ORDER_SUPPLIER" 
                                                   HeaderText="Supplier">
                                                    <ItemStyle Width="80px" />  
                                                </telerik:GridBoundColumn>
                                             --%>
                                            <telerik:GridBoundColumn UniqueName="DELIVERY_PORT" DataField="DELIVERY_PORT" HeaderText="Port Name">
                                                <ItemStyle Width="80px" />
                                            </telerik:GridBoundColumn>
                                         <%-- <telerik:GridBoundColumn DataField="DELIVERD_QTY" HeaderText="Deliver Qty." UniqueName="DELIVERD_QTY">
                                                <ItemStyle Width="80px" />
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn DataField="total_Pay" HeaderText="Total Pay" DataFormatString="{0:F2}"
                                                UniqueName="total_Pay" Visible="false">
                                                <ItemStyle Width="80px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="total_item" HeaderText="Total Items" UniqueName="total_item">
                                                <ItemStyle Width="60px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DELIVERY_CODE" HeaderText="DELIVERY_CODE" UniqueName="DELIVERY_CODE"
                                                Visible="false">
                                                <ItemStyle Width="80px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PAY_ON" HeaderText="Pay On" UniqueName="PAY_ON"
                                                Visible="true">
                                                <ItemStyle BackColor="#ffe1e1" Width="80px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Total_Pay_Amount" HeaderText="Total Pay" DataFormatString="{0:F2}"
                                                UniqueName="Total_Pay_Amount">
                                                <ItemStyle Width="80px" />
                                            </telerik:GridBoundColumn>
                                            
                                            
                                            <%-- <telerik:GridBoundColumn DataField="" HeaderText="Status"  
                                                UniqueName="">
                                                <ItemStyle Width="80px" />
                                            </telerik:GridBoundColumn> --%>
                                            
                                            <%--<telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Mark as Close">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onMarkAsClose"
                                                        CommandName="Delete" CommandArgument='<%#Eval("[REQUISITION_CODE]")%>' ForeColor="Black"
                                                        ToolTip="Delete" ImageUrl="~/Images/Delete.gif" Width="12px" Height="12px">
                                                        </asp:ImageButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>--%>
                                            
                                        </Columns>
                                        <EditFormSettings>
                                            <PopUpSettings ScrollBars="None" />
                                        </EditFormSettings>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="1" align="center" style="width: 930px; background-color: #CCCCCC;">
                    <tr align="right">
                        <td style="width: 168px">
                        </td>
                        <td style="width: 168px">
                        </td>
                        <td style="width: 168px">
                        </td>
                        <td style="width: 120px; font-size: small;" align="right">
                            Total Pay Amount :
                        </td>
                        <td style="width: 168px" align="left">
                            <asp:TextBox ID="txtTotalAmt" Enabled="false" runat="server" Style="font-size: small"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
