<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KPI_Goal.aspx.cs" Inherits="TMSA_KPI_KPI_Goal" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script type="text/javascript">
        function numbersonly(e) {
            var keycode = e.charCode ? e.charCode : e.keyCode;
            if (!(keycode == 46 || keycode == 8 || keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57))) {
                return false
            }
            return true;
        }

    </script>
    <script type="text/javascript">
    //Method to close the KPI Goal on Cancel button click
        function CloseGoal() {
            CloseWindow('KPIGOAL');
        }
</script>
<style>
    .MasterTable_Office2007{font:11px tahoma,verdana,arial,sans-serif}
    .GridHeader_Office2007
    {
    padding-top: 0px;
    padding-bottom: 0px;
    padding-right:0px;
    padding-left:0px;
    background: url(WebResource.axd?d=YjsxPGJZbQmgOHnsMBQeYtw-2PqHOs514t_gVo7fxLni5GjsyJP2PnGtx…f-rJoHoMTPzTQbptKHbudzrRjHftlSdJ_vk7P28mU49lwnB3jAAA1&t=635253968460000000) 0 -200px repeat-x #d3dbe9;
    text-align: center;font:11px tahoma,verdana,arial,sans-serif; padding:5px;
    }
    .GridRow
    {
    padding-top: 0px;
    padding-bottom: 0px;
    padding-left:0px;
    text-align: left;font:11px tahoma,verdana,arial,sans-serif; padding:5px;
    }

</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvGrids" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadGrid ID="rgdItems" runat="server" AllowAutomaticInserts="True" GridLines="None"
            ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007" Style="margin-left: 0px"
            Width="95%" AutoGenerateColumns="False" AllowMultiRowSelection="True" PageSize="100"
            TabIndex="6" HeaderStyle-HorizontalAlign="Center" AlternatingItemStyle-BackColor="#CEE3F6"
            Visible="false">
            <%--  <ClientSettings>
                                                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                    </ClientSettings>--%>
            <MasterTableView TableLayout="Auto">
                <RowIndicatorColumn Visible="true">
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                </RowIndicatorColumn>
                <ExpandCollapseColumn Resizable="False" Visible="False">
                    <HeaderStyle Width="20px" />
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Vessel Name" DataField="Vessel_Name"
                        UniqueName="SortOrder" Visible="true">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnVessel_Id" runat="server" Value='<%#Eval("Vessel_ID")%>' />
                            <asp:Label ID="lblVesselName" Enabled="false" runat="server" Width="60%" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="30%" VerticalAlign="Top" Wrap="true" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Goal" DataField="Value"
                        UniqueName="SortOrder" Visible="true">
                        <ItemTemplate>
                            <asp:TextBox ID="txtItem_Amount" runat="server" Width="100px" MaxLength="13" Text='<%#Eval("Goal")%>'
                                ValidationGroup="vgSubmit" onkeypress="return numbersonly(event)"></asp:TextBox><br />
                            <asp:RangeValidator Display="Dynamic" ID="rngAmount" runat="server" ControlToValidate="txtItem_Amount"
                                MinimumValue="0.00" MaximumValue="1000000000.00" Type="Double" ValidationGroup="vgSubmit"
                                ErrorMessage="Value not in range."></asp:RangeValidator>
                            <asp:RegularExpressionValidator  Display="Dynamic"  ID="reItemAmount" runat="server" ValidationExpression="^\d+(\.\d{1,2})?$"
                                ErrorMessage="Not in Correct format!" ControlToValidate="txtItem_Amount" ValidationGroup="vgSubmit"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator  Display="Dynamic"  ID="rfvItem_Quantity" runat="server" ControlToValidate="txtItem_Amount"
                                ErrorMessage="Goal cannot be blank." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                        <ItemStyle Width="40%" HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Goal" DataField="Value"
                        UniqueName="SortOrder" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAverage" runat="server" Width="100px" MaxLength="13" Visible="false" ValidationGroup="vgSubmit" onkeypress="return numbersonly(event)"></asp:TextBox>                            
                        </ItemTemplate>
                        <ItemStyle Width="40%" HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                </Columns>
                <EditFormSettings>
                    <PopUpSettings ScrollBars="None" />
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
        <telerik:RadGrid ID="rgdFleetGoals" runat="server" AllowAutomaticInserts="True" GridLines="None"
            ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007" Style="margin-left: 0px"
            Width="95%" AutoGenerateColumns="False" AllowMultiRowSelection="True" PageSize="100"
            TabIndex="6" HeaderStyle-HorizontalAlign="Center" AlternatingItemStyle-BackColor="#CEE3F6"
            Visible="false">
            <MasterTableView TableLayout="Auto">
                <RowIndicatorColumn Visible="true">
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                </RowIndicatorColumn>
                <ExpandCollapseColumn Resizable="False" Visible="False">
                    <HeaderStyle Width="20px" />
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Fleet Name" DataField="name"
                        UniqueName="SortOrder" Visible="true">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnFleet_Id" runat="server" Value='<%#Eval("FleetCode")%>' />
                            <asp:Label ID="lblFleetName" Enabled="false" runat="server" Width="65%" Text='<%#Eval("FleetName")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="30%" VerticalAlign="Top" Wrap="true" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Goal" DataField="Value"
                        UniqueName="SortOrder" Visible="true">
                        <ItemTemplate>
                            <asp:TextBox ID="txtItemFleet_Amount" runat="server" Width="100px" MaxLength="13"
                                Text='<%#Eval("Goal")%>' ValidationGroup="vgSubmit" onkeypress="return numbersonly(event)"></asp:TextBox><br />
                            <asp:RangeValidator ID="rngFleetAmount" runat="server" ControlToValidate="txtItemFleet_Amount"
                                MinimumValue="0.00" MaximumValue="1000000000.00" Type="Double" ValidationGroup="vgSubmit"
                                ErrorMessage="Value not in range."  Display="Dynamic" ></asp:RangeValidator>
                            <asp:RegularExpressionValidator ID="reItemFleetAmount" runat="server" ValidationExpression="^\d+(\.\d{1,2})?$"
                                ErrorMessage="Not in Correct format!" ControlToValidate="txtItemFleet_Amount"
                                ValidationGroup="vgSubmit"  Display="Dynamic" ></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvItemFleet_Quantity" runat="server" ControlToValidate="txtItemFleet_Amount"
                                ErrorMessage="Goal cannot be blank." ValidationGroup="vgSubmit"  Display="Dynamic" ></asp:RequiredFieldValidator>
                        </ItemTemplate>
                        <ItemStyle Width="40%" HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Average" DataField="Value"
                        UniqueName="SortOrder" Visible="true">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAvgFleet_Amount" runat="server" Width="100px" MaxLength="13"
                                Text='<%#Eval("Average_Goal")%>' ValidationGroup="vgSubmit" onkeypress="return numbersonly(event)"></asp:TextBox><br />
                            <asp:RangeValidator ID="rngAvgFleetAmount" runat="server" ControlToValidate="txtAvgFleet_Amount"
                                MinimumValue="0.00" MaximumValue="1000000000.00" Type="Double" ValidationGroup="vgSubmit"
                                ErrorMessage="Value not in range."  Display="Dynamic" ></asp:RangeValidator>
                            <asp:RegularExpressionValidator ID="reItemAvgFleetAmount" runat="server" ValidationExpression="^\d+(\.\d{1,2})?$"
                                ErrorMessage="Not in Correct format!" ControlToValidate="txtAvgFleet_Amount"
                                ValidationGroup="vgSubmit"  Display="Dynamic" ></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvItemAvgFleet_Quantity" runat="server" ControlToValidate="txtAvgFleet_Amount"
                                ErrorMessage="Average cannot be blank." ValidationGroup="vgSubmit"  Display="Dynamic"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                        <ItemStyle Width="40%" HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                </Columns>
                <EditFormSettings>
                    <PopUpSettings ScrollBars="None" />
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
        <telerik:RadGrid ID="rgdVesselTypeGoals" runat="server" AllowAutomaticInserts="True"
            GridLines="None" ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007"
            Style="margin-left: 0px" Width="95%" AutoGenerateColumns="False" AllowMultiRowSelection="True"
            PageSize="100" TabIndex="6" HeaderStyle-HorizontalAlign="Center" AlternatingItemStyle-BackColor="#CEE3F6"
            Visible="false">
            <MasterTableView TableLayout="Auto">
                <RowIndicatorColumn Visible="true">
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                </RowIndicatorColumn>
                <ExpandCollapseColumn Resizable="False" Visible="False">
                    <HeaderStyle Width="20px" />
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Vessel Type" DataField="name"
                        UniqueName="SortOrder" Visible="true">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnVesselType_Id" runat="server" Value='<%#Eval("ID")%>' />
                            <asp:Label ID="lblVesselType" Enabled="false" runat="server" Width="90%" Text='<%#Eval("VesselTypes")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="30%" VerticalAlign="Top" Wrap="true" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Goal" DataField="Value"
                        UniqueName="SortOrder" Visible="true">
                        <ItemTemplate>
                            <asp:TextBox ID="txtItemVesselType_Amount" runat="server" Width="100px" MaxLength="13"
                                Text='<%#Eval("Goal")%>' ValidationGroup="vgSubmit" onkeypress="return numbersonly(event)"></asp:TextBox><br />
                            <asp:RangeValidator ID="rngVesselTypeAmount" runat="server" ControlToValidate="txtItemVesselType_Amount"
                                MinimumValue="0.00" MaximumValue="1000000000.00" Type="Double" ValidationGroup="vgSubmit"
                                ErrorMessage="Value not in range." Display="Dynamic"></asp:RangeValidator>
                            <asp:RegularExpressionValidator ID="reItemVesselTypeAmount" runat="server" ValidationExpression="^\d+(\.\d{1,2})?$"
                                ErrorMessage="Not in Correct format!" ControlToValidate="txtItemVesselType_Amount"
                                ValidationGroup="vgSubmit" Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvItemVesselType_Quantity" runat="server" ControlToValidate="txtItemVesselType_Amount"
                                ErrorMessage="Goal cannot be blank." ValidationGroup="vgSubmit" Display="Dynamic"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                        <ItemStyle Width="40%" HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Average" DataField="Value"
                        UniqueName="SortOrder" Visible="true">
                        <ItemTemplate>
                            <asp:TextBox ID="txtItemAvgVesselType_Amount" runat="server" Width="100px" MaxLength="13"
                                Text='<%#Eval("Average_Goal")%>' ValidationGroup="vgSubmit" onkeypress="return numbersonly(event)"></asp:TextBox><br />
                            <asp:RangeValidator ID="rngVesselAvgTypeAmount" runat="server" ControlToValidate="txtItemAvgVesselType_Amount"
                                MinimumValue="0.00" MaximumValue="1000000000.00" Type="Double" ValidationGroup="vgSubmit"
                                ErrorMessage="Value not in range." Display="Dynamic"></asp:RangeValidator>
                            <asp:RegularExpressionValidator ID="reItemAvgVesselTypeAmount" runat="server" ValidationExpression="^\d+(\.\d{1,2})?$"
                                ErrorMessage="Not in Correct format!" ControlToValidate="txtItemAvgVesselType_Amount"
                                ValidationGroup="vgSubmit" Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvItemAvgVesselType_Quantity" runat="server" ControlToValidate="txtItemAvgVesselType_Amount"
                                ErrorMessage="Average cannot be blank." ValidationGroup="vgSubmit" Display="Dynamic"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                        <ItemStyle Width="40%" HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                </Columns>
                <EditFormSettings>
                    <PopUpSettings ScrollBars="None" />
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>


        <telerik:RadGrid ID="rgdCompany" runat="server" AllowAutomaticInserts="True"
            GridLines="None" ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007"
            Style="margin-left: 0px" Width="95%" AutoGenerateColumns="False" AllowMultiRowSelection="True"
            PageSize="100" TabIndex="6" HeaderStyle-HorizontalAlign="Center" AlternatingItemStyle-BackColor="#CEE3F6"
            Visible="false">
            <MasterTableView TableLayout="Auto">
                <RowIndicatorColumn Visible="true">
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                </RowIndicatorColumn>
                <ExpandCollapseColumn Resizable="False" Visible="False">
                    <HeaderStyle Width="20px" />
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Company" DataField="name"
                        UniqueName="SortOrder" Visible="true">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnCompany_Id" runat="server" Value='<%#Eval("ID")%>' />
                            <asp:Label ID="lblCompany" Enabled="false" runat="server" Width="50%" ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="30%" VerticalAlign="Top" Wrap="true" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Goal" DataField="Value"
                        UniqueName="SortOrder" Visible="true">
                        <ItemTemplate>
                            <asp:TextBox ID="txtItemCompany_Amount" runat="server" Width="100px" MaxLength="13"
                                Text='<%#Eval("Goal")%>' ValidationGroup="vgSubmit" onkeypress="return numbersonly(event)"></asp:TextBox>
                            <asp:RangeValidator ID="rngCompanyAmount" runat="server" ControlToValidate="txtItemCompany_Amount"
                                MinimumValue="0.00" MaximumValue="1000000000.00" Type="Double" ValidationGroup="vgSubmit"
                                ErrorMessage="Value not in range."></asp:RangeValidator>
                            <asp:RegularExpressionValidator ID="reItemCompanyAmount" runat="server" ValidationExpression="^\d+(\.\d{1,2})?$"
                                ErrorMessage="Not in Correct format!" ControlToValidate="txtItemCompany_Amount"
                                ValidationGroup="vgSubmit"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvItemCompany_Quantity" runat="server" ControlToValidate="txtCompany_Amount"
                                ErrorMessage="Goal cannot be blank." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                        <ItemStyle Width="40%" HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Average" DataField="Value"
                        UniqueName="SortOrder" Visible="true">
                        <ItemTemplate>
                            <asp:TextBox ID="txtItemAvgCompany_Amount" runat="server" Width="100px" MaxLength="13"
                                Text='<%#Eval("Average_Goal")%>' ValidationGroup="vgSubmit" onkeypress="return numbersonly(event)"></asp:TextBox>
                            <asp:RangeValidator ID="rngCompanyAvgTypeAmount" runat="server" ControlToValidate="txtItemAvgCompany_Amount"
                                MinimumValue="0.00" MaximumValue="1000000000.00" Type="Double" ValidationGroup="vgSubmit"
                                ErrorMessage="Value not in range."></asp:RangeValidator>
                            <asp:RegularExpressionValidator ID="reItemAvgCompanyAmount" runat="server" ValidationExpression="^\d+(\.\d{1,2})?$"
                                ErrorMessage="Not in Correct format!" ControlToValidate="txtItemAvgCompany_Amount"
                                ValidationGroup="vgSubmit"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfvItemAvgCompany_Quantity" runat="server" ControlToValidate="txtItemAvgCompany_Amount"
                                ErrorMessage="Average cannot be blank." ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                        <ItemStyle Width="40%" HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                </Columns>
                <EditFormSettings>
                    <PopUpSettings ScrollBars="None" />
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
        
        <div id="companyDiv" runat="server">
            <table class="MasterTable_Office2007" style="border: 1px solid #9eb6ce;" cellpadding="0" cellspacing="0"  >
                <tr>
                    <td  class="GridHeader_Office2007" style="border-right: solid 1px #9eb6ce";width="100px";>Company</td>
                    <td  class="GridHeader_Office2007" style="border-right: solid 1px #9eb6ce;">Goal</td>
                    <td  class="GridHeader_Office2007">Average</td>
                </tr>
                <tr>
                    <td valign="top" class="GridRow" style="border-right: solid 1px #9eb6ce"; width="100px";>
                        Company - All vessels
                    </td>
                    <td  class="GridRow" style="border-right: solid 1px #9eb6ce;">
                        <asp:TextBox ID="txtItemCompany_Amount" runat="server" Width="100px" MaxLength="13" ValidationGroup="vgSubmit" Text="0.00" onkeypress="return numbersonly(event)"></asp:TextBox><br />
                        <asp:RangeValidator ID="rngCompanyAmount" runat="server" ControlToValidate="txtItemCompany_Amount"
                            MinimumValue="0.00" MaximumValue="1000000000.00" Type="Double" ValidationGroup="vgSubmit"
                            ErrorMessage="Value not in range." Display="Dynamic"></asp:RangeValidator>
                        <asp:RegularExpressionValidator ID="reItemCompanyAmount" runat="server" ValidationExpression="^\d+(\.\d{1,2})?$"
                            ErrorMessage="Not in Correct format!" ControlToValidate="txtItemCompany_Amount"
                            ValidationGroup="vgSubmit" Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvItemCompany_Quantity" runat="server" ControlToValidate="txtItemCompany_Amount"
                            ErrorMessage="Goal cannot be blank." ValidationGroup="vgSubmit" Display="Dynamic"></asp:RequiredFieldValidator>                        
                    </td>
                    <td  class="GridRow">
                        <asp:TextBox ID="txtAvgCompany" runat="server" Width="100px" MaxLength="13" ValidationGroup="vgSubmit" Text="0.00" onkeypress="return numbersonly(event)"></asp:TextBox><br />
                        <asp:RangeValidator ID="rngCompanyAvg" runat="server" ControlToValidate="txtAvgCompany"
                            MinimumValue="0.00" MaximumValue="1000000000.00" Type="Double" ValidationGroup="vgSubmit"
                            ErrorMessage="Value not in range." Display="Dynamic"></asp:RangeValidator>
                        <asp:RegularExpressionValidator ID="reCompanyAvg" runat="server" ValidationExpression="^\d+(\.\d{1,2})?$"
                            ErrorMessage="Not in Correct format!" ControlToValidate="txtAvgCompany"
                            ValidationGroup="vgSubmit" Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfvCompanyAvg" runat="server" ControlToValidate="txtAvgCompany"
                            ErrorMessage="Average cannot be blank." ValidationGroup="vgSubmit" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <center>
            <asp:Label ID="ltMessage" Style="color: green" runat="server"></asp:Label>
        </center>
    </div>
    <center>
        <asp:Button ID="btnCloseWindow" Text="Cancel" runat="server" OnClick="Close"/>
        <asp:Button Text="Save" runat="server" ValidationGroup="vgSubmit" OnClick="Save_Click" />        
    </center>
    </form>
</body>
</html>
