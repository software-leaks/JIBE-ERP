<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AsignVessels.aspx.cs" ValidateRequest="false" EnableEventValidation="false"
    Inherits="AsignVessels" Title="Vessels Location and Catalogue detail Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
     <div class="page-title">
      Vessels Location and Catalogue detail Page
    
    </div>
    <center>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             s
                <table width="930px" cellpadding="0" cellspacing="0" border="0">
                                <tr valign="middle">
                                    <td width="100px" style="font-size: small;height:24px" align="right">
                                        <b>Fleet :</b>
                                    </td>
                                     <td colspan="3" style="font-size: small" align="left">
                                        <asp:DropDownList ID="DDLFleet" runat="server" AutoPostBack="True" OnSelectedIndexChanged="fleet_SelectedIndexChanged"
                                            Style="font-size: small" Width="150px" AppendDataBoundItems="True"  >
                                            <asp:ListItem>--Select--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    
                                   
                                </tr>
                                <tr>
                                <td colspan="4" style="background-color: #808080; font-size: small; color: #FFFFFF; height:1px "></td></tr>
                                <tr>
                                 <td width="100px" align="right" style="height:24px; font-size: small; color: #333333;" ><b>Vessel :</b>
                                 </td>
            
                                    <td  align="left"> 
                                     <asp:DropDownList ID="DDLVessel" runat="server" Width="200px" 
                                            Font-Size="XX-Small" AppendDataBoundItems="True" TabIndex="1" 
                                            AutoPostBack="True" onselectedindexchanged="DDLVessel_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                  </asp:DropDownList>
                                  </td>
                                  <td style="width: 150px">
                                  
                                  </td>     
                                  <td style="width: 150px"></td>
                                </tr>
                                
                                
                               </table
                
                
                
                
                
                
                
                
                
                
                
                
                <table width="930px" style="height :85% " cellpadding="0" cellspacing="0" border="1" >
                    <tr valign="top">
                        
                        <td align="center" >
                            <table width ="930px" cellpadding="0" cellspacing="0" >
                                <tr valign="top">
                                    <td style="background-color: #999999; height:20px;  font-size: small; color: #FFFFFF;">
                                        <b>Assign Location</b>
                                    </td>
                                     <td style="background-color: #999999;height:20px;   font-size: small; color: #FFFFFF;">
                                        <b>Assign Catalogue</b>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td align="center" Width="400px">
                                       <!-- <div class="Freezing" style="width: 100%; height: 220px; overflow: scroll;" backcolor="White"> -->
                                            <telerik:RadGrid ID="grdAssignLocation" runat="server" 
                                                AllowSorting="True" GridLines="None" Skin="WebBlue" AllowMultiRowSelection="True"
                                                OnNeedDataSource="grdAssignLocation_NeedDataSource"  Height="220px"
                                            Font-Size="XX-Small" OnItemDataBound="grdAssignLocation_ItemDataBound" 
                                            Width="390px" AutoGenerateColumns="False" >
                                                <MasterTableView>
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
                                                                <asp:CheckBox ID="checkHeder" runat="server" AutoPostBack="true" OnCheckedChanged="checkchangAll"  />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%-- <asp:Panel ID="Panel1" runat="server">--%>
                                                                <asp:CheckBox ID="ChecLocation" runat="server" AutoPostBack="true" OnCheckedChanged="RowCheckedChanged"
                                                                    Font-Size="Smaller" />
                                                                <%-- </asp:Panel>--%>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                             <telerik:GridBoundColumn DataField="Location_Code" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                            HeaderText="Location" UniqueName="Code" Visible="false">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle Width="0" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Description" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="Description" UniqueName="Description">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="330px" />
                                                            <ItemStyle Width="330px" HorizontalAlign="Left" Font-Size="Smaller" />
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
                                     <td align="center" width="520px"  >
                                               <telerik:RadGrid ID="grdAsigncatalog" runat="server" Height="220px" 
                                                AllowSorting="True" GridLines="None" Skin="WebBlue" AllowMultiRowSelection="True"
                                                OnNeedDataSource="grdAsigncatalog_NeedDataSource" Font-Size="XX-Small" 
                                                   OnItemDataBound="grdAsigncatalog_ItemDataBound" Width="520px">
                                                <MasterTableView AutoGenerateColumns="False">
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn Resizable="False" Visible="False">
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" CommandArgument="System_Code"
                                                            HeaderText="" ItemStyle-Width ="20px"  HeaderStyle-Width="20px"/>
                                                        <telerik:GridBoundColumn DataField="SYSTEM_Code" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="SYSTEM_Code" UniqueName="SYSTEM_Code"
                                                            Visible="false">
                                                            <HeaderStyle HorizontalAlign="Center" Width ="0"   VerticalAlign="Middle" />
                                                            <ItemStyle Width ="0" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="system_description" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="Catalogue" UniqueName="system_description"
                                                            Visible="true">
                                                            <HeaderStyle HorizontalAlign="Center" Width="230px" VerticalAlign="Middle" />
                                                            <ItemStyle Width="230px" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="system_particulars" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="Details" UniqueName="system_particulars">
                                                            <HeaderStyle HorizontalAlign="Center" Width="200px" VerticalAlign="Middle" />
                                                            <ItemStyle Width="200px" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="view">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnlinkOld1" runat="server" Text="Select" OnCommand="onSelect"
                                                                    CommandName="Select" CommandArgument='<%#Eval("[System_code]")%>' ForeColor="Black"
                                                                    ImageUrl="~/Technical/INV/Image/view.gif" Width="12px" Height="12px"></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="45px" />
                                                            <ItemStyle Width="45px" />
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <PopUpSettings ScrollBars="Horizontal" />
                                                        
                                                    </EditFormSettings>
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Selecting AllowRowSelect="True" />
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr align="center" valign="top">
                                    <td  style="background-color: #808080; font-size: small; color: #FFFFFF;">
                                        <asp:ImageButton ID="ImgAssignLocation" runat="server" ImageUrl="~/images/up.PNG"
                                            OnClick="ImgAssignLocation_Click" Width="16px" Style="height: 16px" ToolTip="Click for assign Location" />&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImgUnassignLocation" runat="server" ImageUrl="~/images/down.PNG"
                                            Width="16px" ToolTip="Click for Unassign Location" OnClick="ImgUnassignLocation_Click"
                                            Style="height: 16px" />
                                    </td>
                                     <td style="background-color: #808080; font-size: small; color: #FFFFFF;">
                                        <asp:ImageButton ID="ImgAssignCatalog" runat="server" ImageUrl="~/images/up.PNG"
                                            Width="16px" ToolTip="Click for Assign Catalog" OnClick="ImgAssignCatalog_Click"
                                            Style="height: 16px" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImgUnassignCatalog" runat="server" ImageUrl="~/images/down.PNG"
                                            Width="16px" ToolTip="Click for Unassign Catalog" OnClick="ImgUnassignCatalog_Click"
                                            Style="height: 16px" />
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td  style="background-color: #999999; height:20px; font-size: small; color: #FFFFFF;">
                                        <b>Un-Assign Location</b>
                                        <br />
                                    </td>
                                     <td style="background-color: #999999; height:20px; font-size: small; color: #FFFFFF;">
                                        <b>Un-Assign Catalogue</b>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td align="center" Width="400px" >
                                      <!--  <div class="Freezing" style="width: 100%;backcolor="White"> --> 
                                            <telerik:RadGrid ID="grdunAssignLocation" runat="server" AllowAutomaticInserts="True"
                                                AllowSorting="True" GridLines="None" Skin="WebBlue"  ImagesPath="~/grid_images/"
                                                OnNeedDataSource="grdunAssignLocation_NeedDataSource" AllowMultiRowSelection="True"
                                                OnPageIndexChanged="grdunAssignLocation_PageIndexChanged" OnSelectedIndexChanged="grdunAssignLocation_SelectedIndexChanged"
                                                Font-Size="XX-Small" Font-Strikeout="False" OnItemDataBound="grdunAssignLocation_ItemDataBound"
                                                AutoGenerateColumns="False" Height="220px" Width="400px">
                                                <MasterTableView>
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn Resizable="False" Visible="False">
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" Text="row_number"
                                                            CommandArgument="Code" HeaderText=" "  />
                                                        <telerik:GridBoundColumn DataField="SN" UniqueName="ItemCode" AllowFiltering="false"
                                                            HeaderText="S. No." Visible="false">
                                                            <ItemStyle Width="60px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Location_Code" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                            HeaderText="Location" UniqueName="Code" Visible="false">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle Width="0" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Description" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="Description" UniqueName="Description">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="340px" />
                                                            <ItemStyle Width="340px" HorizontalAlign="Left" Font-Size="Smaller" />
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
                                    <td align="center" width="520px" >
                                      <!--  <div class="Freezing" style="width: 100%; height: 220px; overflow: scroll;" backcolor="White"> -->
                                            <telerik:RadGrid ID="grdunAsigncatalog" runat="server" AllowAutomaticInserts="True"
                                                AllowSorting="True" GridLines="None" Skin="WebBlue" width="520px" 
                                            Height="220px" AllowMultiRowSelection="True"
                                                OnNeedDataSource="grdunAsigncatalog_NeedDataSource" 
                                            Font-Size="XX-Small" OnItemDataBound="grdunAsigncatalog_ItemDataBound">
                                                <MasterTableView AutoGenerateColumns="False">
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn Resizable="False" Visible="False">
                                                        <HeaderStyle Width="20px" />
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" Text="row_number"
                                                            CommandArgument="System_code" HeaderText=" " ItemStyle-Width ="20px"  HeaderStyle-Width="20px"/>
                                                        <telerik:GridBoundColumn DataField="SYSTEM_Code" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="SYSTEM_Code" UniqueName="SYSTEM_Code"
                                                            Visible="false">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                             <ItemStyle Width="0" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="SYSTEM_DESCRIPTION" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="Catalogue" UniqueName="SYSTEM_DESCRIPTION"
                                                            Visible="true">
                                                            <HeaderStyle HorizontalAlign="Center" Width="230px" VerticalAlign="Middle" />
                                                            <ItemStyle Width="230px" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </telerik:GridBoundColumn>
                                                      <telerik:GridBoundColumn DataField="system_particulars" HeaderStyle-HorizontalAlign="Center"
                                                            HeaderStyle-VerticalAlign="Middle" HeaderText="Details" UniqueName="SYSTEM_PARTICULARS">
                                                            <HeaderStyle HorizontalAlign="Center" Width="200px" VerticalAlign="Middle" />
                                                            <ItemStyle Width="200px" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="view">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnlinkOld" runat="server" Text="Select" OnCommand="onSelect"
                                                                    CommandName="Select" CommandArgument='<%#Eval("[System_code]")%>' ForeColor="Black"
                                                                    ImageUrl="~/Technical/INV/Image/view.gif" Width="12px" Height="12px"> </asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="45px" />
                                                        <ItemStyle  Width="45px" />
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <PopUpSettings ScrollBars="Horizontal" />
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
                    <tr>
                    <td style ="height:20px"></td></tr>
                </table>
             

                <script language="javascript" type="text/javascript">
                function RowSelected(row, eventArgs)
               {
               }
                </script>

            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
