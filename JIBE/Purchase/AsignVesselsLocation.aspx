<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AsignVesselsLocation.aspx.cs" ValidateRequest="false" EnableEventValidation="false"
    Inherits="AsignVesselsLocation" Title="Assign Vessels" %>    

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
            <table idth="930px" cellpadding="0" cellspacing="0" border="0">
            <tr>
            <td>
           
                <table width="930px" cellpadding="0" cellspacing="0" border="0">
                    <tr align="center">
                        <td style="background-color: #808080; font-size: small; color: #FFFFFF;">
                            <b><span style="font-size: small">Vessels Location and Catalogue detail Page</span> </b>
                        </td>
                    </tr>
                </table>
                 </td>
            </tr>
            <tr>
            <td>
                <table width="930px"  cellpadding="0" cellspacing="0" border="1">
                    <tr valign="top">
                        <td align="left" style="width: 32%" rowspan="2">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr valign="top">
                                    <td style="background-color: #999999; height:16px; font-size: small; color: #FFFFFF;">
                                        <b>Vessels</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: small">
                                        <asp:DropDownList ID="fleet" runat="server" Width="98%" AutoPostBack="True" OnSelectedIndexChanged="fleet_SelectedIndexChanged"
                                            Style="font-size: small" Height="16px">
                                            <asp:ListItem>ALL</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">                                  
                                            <telerik:RadGrid ID="grdVessel" runat="server"   AllowSorting="True"
                                                GridLines="None" Skin="WebBlue" Width="100%" OnNeedDataSource="grdVessel_NeedDataSource"
                                                OnItemCommand="grdVessel_ItemCommand" OnItemDataBound="grdVessel_ItemDataBound"
                                                OnSelectedIndexChanged="grdVessel_SelectedIndexChanged" Font-Size="XX-Small" 
                                                Font-Strikeout="False" Height="555px" Style="margin-left: 0px">
                                                
                                                <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                                                <Selecting AllowRowSelect="true" />
                                                </ClientSettings>
                                                
                                                <MasterTableView AutoGenerateColumns="False">
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn Resizable="False" Visible="False">
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Vessel_Code" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="Vessels Name" UniqueName="Vessel_Code"
                                                            Visible="false" FilterListOptions="VaryByDataTypeAllowCustom">
                                                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                        </telerik:GridBoundColumn>
                                                        
                                                        <telerik:GridBoundColumn DataField="Vessels" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="Vessels" UniqueName="Vessels"
                                                            FilterListOptions="VaryByDataType">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Left" Width="90%"  Font-Size="Smaller" />
                                                        </telerik:GridBoundColumn>
                                                        
                                                     
                                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="15%" HeaderText="View">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnlinkOld0" runat="server" Text="Select" OnCommand="onVeselSelect"
                                                                    CommandName="Select" CommandArgument='<%#Eval("[Vessel_Code]")%>' ForeColor="Black"
                                                                    ImageUrl="~/Technical/INV/Image/view.gif" Width="12px" Height="12px"></asp:ImageButton>
                                                            </ItemTemplate>
                                                             
                                                        <ItemStyle HorizontalAlign="Left" Width="60px"  Font-Size="Smaller" />
                                                       </telerik:GridTemplateColumn>
                                                       
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <PopUpSettings ScrollBars="None" />
                                                    </EditFormSettings>
                                                    <ItemStyle Height="16px" />
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Selecting AllowRowSelect="True" />
                                                    <ClientEvents OnRowSelected="RowSelected" />
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                </ClientSettings>
                                                <ItemStyle Height="12px" />
                                            </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left"  style="width: 68%">
                            <table width ="100%">
                                <tr>
                                    <td style="background-color: #999999; height:20px; width:50%; font-size: small; color: #FFFFFF;">
                                        <b>Assign Location</b>
                                    </td>
                                     <td style="background-color: #999999;height:20px; width:50%;  font-size: small; color: #FFFFFF;">
                                        <b>Assign Catalogue</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="50%">
                                          <telerik:RadGrid ID="grdAssignLocation" runat="server" 
                                                AllowSorting="True" GridLines="None" Skin="WebBlue" Width="100%" AllowMultiRowSelection="True"
                                                OnNeedDataSource="grdAssignLocation_NeedDataSource"  Height="260px"
                                            Font-Size="XX-Small" OnItemDataBound="grdAssignLocation_ItemDataBound" >
                                                <MasterTableView AutoGenerateColumns="False">
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn Resizable="False" Visible="False">
                                                        <HeaderStyle Width="16px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="SN" AllowFiltering="false"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="checkHeder" runat="server" AutoPostBack="true" OnCheckedChanged="checkchangAll" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                  <asp:CheckBox ID="ChecLocation" runat="server" AutoPostBack="true" OnCheckedChanged="RowCheckedChanged"
                                                                    Font-Size="Smaller" />
                                                              
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" />
                                                            </telerik:GridTemplateColumn>
                                                      
                                                        <telerik:GridBoundColumn DataField="Code" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                            HeaderText="Location" UniqueName="Code" Visible="false">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle Width="30%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Description" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="Description" UniqueName="Description">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle Width="90%" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <PopUpSettings ScrollBars="None" />
                                                    </EditFormSettings>
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Selecting AllowRowSelect="True" />
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                    </td>
                                     <td align="center" width="50%">
                                            <telerik:RadGrid ID="grdAsigncatalog" runat="server" Height="260px" Width="100%"  
                                                AllowSorting="True" GridLines="None" Skin="WebBlue" AllowMultiRowSelection="True"
                                                OnNeedDataSource="grdAsigncatalog_NeedDataSource" Font-Size="XX-Small" OnItemDataBound="grdAsigncatalog_ItemDataBound">
                                                <MasterTableView AutoGenerateColumns="False">
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn Resizable="False" Visible="False">
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" Text="row_number"
                                                            CommandArgument="System_code" HeaderText="" />
                                                        <telerik:GridBoundColumn DataField="SYSTEM_Code" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="SYSTEM_Code" UniqueName="SYSTEM_Code"
                                                            Visible="false">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="SYSTEM_DESCRIPTION" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="Catalogue" UniqueName="SYSTEM_DESCRIPTION"
                                                            Visible="true">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle Width="45%" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="system_particulars" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="Details" UniqueName="SYSTEM_PARTICULARS">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle Width="45%" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="view">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnlinkOld1" runat="server" Text="Select" OnCommand="onSelect"
                                                                    CommandName="Select" CommandArgument='<%#Eval("[System_code]")%>' ForeColor="Black"
                                                                    ImageUrl="~/Technical/INV/Image/view.gif" Width="12px" Height="12px"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        <ItemStyle  Width="10%" />
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <PopUpSettings ScrollBars="None" />
                                                    </EditFormSettings>
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Selecting AllowRowSelect="True" />
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td  width="50%" style="background-color: #808080; font-size: small; color: #FFFFFF;">
                                        <asp:ImageButton ID="ImgAssignLocation" runat="server" ImageUrl="~/images/up.PNG"
                                            OnClick="ImgAssignLocation_Click" Width="16px" Style="height: 16px" ToolTip="Click for assign Location" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImgUnassignLocation" runat="server" ImageUrl="~/images/down.PNG"
                                            Width="16px" ToolTip="Click for Unassign Location" OnClick="ImgUnassignLocation_Click"
                                            Style="height: 16px" />
                                    </td>
                                     <td width="50%" style="background-color: #808080; font-size: small; color: #FFFFFF;">
                                        <asp:ImageButton ID="ImgAssignCatalog" runat="server" ImageUrl="~/images/up.PNG"
                                            Width="16px" ToolTip="Click for Assign Catalog" OnClick="ImgAssignCatalog_Click"
                                            Style="height: 16px" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImgUnassignCatalog" runat="server" ImageUrl="~/images/down.PNG"
                                            Width="16px" ToolTip="Click for Unassign Catalog" OnClick="ImgUnassignCatalog_Click"
                                            Style="height: 16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%" style="background-color: #999999; height:20px; font-size: small; color: #FFFFFF;">
                                        <b>Un-Assign Location</b>
                                        <br />
                                    </td>
                                     <td width="50%" style="background-color: #999999; height:20px; font-size: small; color: #FFFFFF;">
                                        <b>Un-Assign Catalogue</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="50%" >                                     
                                            <telerik:RadGrid ID="grdunAssignLocation" runat="server" AllowAutomaticInserts="True"
                                                AllowSorting="True" GridLines="None" Skin="WebBlue" Width="100%" ImagesPath="~/grid_images/"
                                                OnNeedDataSource="grdunAssignLocation_NeedDataSource" AllowMultiRowSelection="True"
                                                OnPageIndexChanged="grdunAssignLocation_PageIndexChanged" OnSelectedIndexChanged="grdunAssignLocation_SelectedIndexChanged"
                                                Font-Size="XX-Small" Font-Strikeout="False" OnItemDataBound="grdunAssignLocation_ItemDataBound"
                                                AutoGenerateColumns="False" Height="260px">
                                                <MasterTableView>
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn Resizable="False" Visible="False">
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" Text="row_number"
                                                            CommandArgument="Code" HeaderText="" />
                                                        <telerik:GridBoundColumn DataField="SN" UniqueName="ItemCode" AllowFiltering="false"
                                                            HeaderText="S. No." Visible="false">
                                                            <ItemStyle Width="10%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Code" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                            HeaderText="Location" UniqueName="Code" Visible="false">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle Width="30%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Description" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="Description" UniqueName="Description">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle Width="90%" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <PopUpSettings ScrollBars="None" />
                                                    </EditFormSettings>
                                                </MasterTableView>
                                                <SelectedItemStyle BorderStyle="Solid" />
                                                <ClientSettings>
                                                    <Selecting AllowRowSelect="True" />
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                            <!-- </div> -->
                                    </td>
                                    <td align="center" width="50%">
                                      <!--  <div class="Freezing" style="width: 100%; height: 220px; overflow: scroll;" backcolor="White"> -->
                                            <telerik:RadGrid ID="grdunAsigncatalog" runat="server" AllowAutomaticInserts="True"
                                                AllowSorting="True" GridLines="None" Skin="WebBlue" Width="100%" Height="260px" AllowMultiRowSelection="True"
                                                OnNeedDataSource="grdunAsigncatalog_NeedDataSource" Font-Size="XX-Small" OnItemDataBound="grdunAsigncatalog_ItemDataBound">
                                                <MasterTableView AutoGenerateColumns="False">
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn Resizable="False" Visible="False">
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" Text="row_number"
                                                            CommandArgument="System_code" HeaderText="" />
                                                        <telerik:GridBoundColumn DataField="SYSTEM_Code" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="SYSTEM_Code" UniqueName="SYSTEM_Code"
                                                            Visible="false">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="SYSTEM_DESCRIPTION" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="Catalogue" UniqueName="SYSTEM_DESCRIPTION"
                                                            Visible="true">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle Width="45%" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="system_particulars" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="Details" UniqueName="SYSTEM_PARTICULARS">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle Width="45%" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="view">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnlinkOld" runat="server" Text="Select" OnCommand="onSelect"
                                                                    CommandName="Select" CommandArgument='<%#Eval("[System_code]")%>' ForeColor="Black"
                                                                    ImageUrl="~/Technical/INV/Image/view.gif" Width="12px" Height="12px"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        <ItemStyle  Width="10%" />
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <PopUpSettings ScrollBars="None" />
                                                    </EditFormSettings>
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Selecting AllowRowSelect="True" />
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    
                    </tr>
                </table>
                </td></tr>
            </table>
           <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
         <script language="javascript" type="text/javascript">
                function RowSelected(row, eventArgs)
               {
               }
                </script>

    </center>
</asp:Content>
