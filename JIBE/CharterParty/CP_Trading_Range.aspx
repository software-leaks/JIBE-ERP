<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="CP_Trading_Range.aspx.cs"
    Inherits="CP_Trading_Range" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
         <script language="javascript" type="text/javascript">
        function numbersonly(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode
            if (unicode != 8) { //if the key isn't the backspace key (which we should allow)
                if (unicode < 48 || unicode > 57) //if not a number
                    return false //disable key press
            }
        }
    </script>
</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 98%;
    height: 100%;">
    <form id="form1" runat="server">
    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
            <table width="100%" cellpadding="2" cellspacing="0" >
                <tr>
                    <td align="center" colspan="2">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltPageHeader" Text ="Trading Range" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>
                    <tr>
                    
                    <td colspan="2">
                    <div class="freezing" style="width: 100%;">
                        <telerik:RadGrid ID="rgdItems" runat="server" AllowAutomaticInserts="True" GridLines="None" 
                                                    ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007" Style="margin-left: 0px"
                                                    Width="100%" AutoGenerateColumns="False" OnItemDataBound="rgdItems_ItemDataBound"
                                                    AllowMultiRowSelection="True" PageSize="100" TabIndex="6" HeaderStyle-HorizontalAlign="Center"
                                                    AlternatingItemStyle-BackColor="#CEE3F6">
                                                    <MasterTableView>
                                                        <RowIndicatorColumn Visible="true">
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                        </RowIndicatorColumn>
                                                        <ExpandCollapseColumn Resizable="False" Visible="False">
                                                            <HeaderStyle Width="20px" />
                                                        </ExpandCollapseColumn>
                                                        <Columns>
                                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="WEF"
                                                                UniqueName="CheckID" Visible="true">
                                                                <ItemTemplate>
                                                                     <asp:Label ID="lblWEF" Text='<%#Eval("Trading_Date")%>' Visible="false"  style="border: 0; width: 14px; height: 14px" 
                                                                        ForeColor="Black"  runat="server" />
                                                                        <asp:HiddenField ID="hdnTPId" runat="server" Value='<%#Eval("Trading_Range_Id")%>' />

                                                                         <asp:HiddenField ID="hdnDate" runat = "server" Value='<%#Eval("Trading_Date")%>' />
                                                                        <asp:TextBox ID="txtPWFF" Width="100px"  Text='<%#Eval("Trading_Date")%>'   runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="ceP1WEF" runat="server" Format="dd-MMM-yyyy" 
                                                                            TargetControlID="txtPWFF"></cc1:CalendarExtender>
                                                                  &nbsp;
                                                                <asp:DropDownList ID="ddlHoursWEFP" runat="server" Width="40px"></asp:DropDownList>
                                                               :
                                                                 <asp:DropDownList ID="ddlMinsWEFP" runat="server"  Width="40px"></asp:DropDownList>
                                                                 <asp:DropDownList ID="ddlLTGMTP" runat="server"  Width="50px">
                                                                     <asp:ListItem Text="LT" Value="LT"></asp:ListItem>
                                                                     <asp:ListItem Text="GMT" Value="GMT"></asp:ListItem>
                                                                 </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="15%" />
                                                               <FooterTemplate>
                                                                    <asp:Button ID="btnAddNewItem" Text="Add Period" BackColor="#0066cc" BorderStyle="None"
                                                                        ForeColor="WhiteSmoke" runat="server" OnClick="btnAddNewItem_Click" />
                                                                </FooterTemplate>
                                                                 <FooterStyle HorizontalAlign="Left" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Trading Range" DataField="Trading_Range"
                                                                UniqueName="SortOrder" Visible="true">
                                                                <ItemTemplate>
                                                                          <asp:TextBox ID="txtTradingRange" runat="server" Width="98%"  MaxLength="1000" Text='<%#Eval("Trading_Range")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20%" />
                                                            </telerik:GridTemplateColumn>

                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Trading Period" DataField="Trading_Period"
                                                                UniqueName="SortOrder" Visible="true">
                                                                <ItemTemplate>
                                                                          <asp:TextBox ID="txtTradingPeriod" runat="server"  Width="98%"  MaxLength="1000" Text='<%#Eval("Trading_Period")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="15%" />
                                                            </telerik:GridTemplateColumn>

                                                             <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Minimum"
                                                                Visible="true" UniqueName="Minimum" >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Min_Ext_Value" Enabled="true" MaxLength="4" EnableViewState="true" 
                                                                        runat="server" Text='<%# Bind("Min_Ext_Value")%>' Style="font-size: x-small" 
                                                                        Width="30%"></asp:TextBox>
                                                                       <asp:label ID="Min_Ext_Unit" runat="server" Text='<%# Bind("Min_Ext_Unit")%>'  Visible="false"></asp:label>
                                                                        <asp:DropDownList ID="ddlminUnit"  runat="server" >
                                                                        <asp:ListItem Text="Dys" value="DD"></asp:ListItem>
                                                                        <asp:ListItem Text="Wks" value="WW"></asp:ListItem>
                                                                        <asp:ListItem Text="Mths" value="MM"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                </ItemTemplate>
                                                               <ItemStyle Width="10%" />
                                                            </telerik:GridTemplateColumn>

                                                             <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Maximum" DataField="Max_Ext_Unit"
                                                                Visible="true" UniqueName="Minimum">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Max_Ext_Value" Enabled="true" MaxLength="4" EnableViewState="true" 
                                                                        runat="server" Text='<%# Bind("Max_Ext_Value")%>' Style="font-size: x-small" 
                                                                        Width="30%"></asp:TextBox>
                                                                         <asp:label ID="Max_Ext_Unit" runat="server" Text='<%# Bind("Max_Ext_Unit")%>'  Visible="false"></asp:label>
                                                                        <asp:DropDownList ID="ddlmaxUnit"  runat="server" >
                                                                        <asp:ListItem Text="Dys" value="DD"></asp:ListItem>
                                                                        <asp:ListItem Text="Wks" value="WW"></asp:ListItem>
                                                                        <asp:ListItem Text="Mths" value="MM"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                </ItemTemplate>
                                                               <ItemStyle Width="10%" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Remarks" 
                                                                Visible="true" UniqueName="Remarks">
                                                                <ItemTemplate>
                                                                <asp:HiddenField ID="hdnActivePeriod" runat="server" Value='<%# Bind("Active_Period")%>' />
                                                                       <asp:Image ID="imgGreenArrow" ImageUrl="../Images/nav-left.png" runat="server" Visible="false" />
                                                               
                                                                         <asp:label ID="lblRedelivery" runat="server" Text='<%# Bind("ActiveText")%>'  Visible="false"></asp:label>
                      
                                                                </ItemTemplate>
                                                               <ItemStyle Width="10%" ForeColor="Blue"  />
                                                            </telerik:GridTemplateColumn>

                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Action"
                                                               UniqueName="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgDelete" runat="server" OnCommand="onDelete" OnClientClick="return confirm('Are you sure want to delete?')"
                                                                        CommandArgument='<%#Eval("[Trading_Range_Id]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                        Height="16px"></asp:ImageButton>
                                                                </ItemTemplate>
                                                             <ItemStyle Width="5%" />
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                        <EditFormSettings>
                                                            <PopUpSettings ScrollBars="None" />
                                                            <PopUpSettings ScrollBars="None" />
                                                        </EditFormSettings>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                                <asp:Label ID="lblError" ForeColor="Red" runat="server"></asp:Label>
                                                <br />
                                                <asp:Button ID="btnSave" runat="server" Text="Save" Height="30px" OnClick="btnSaveItem_Click"
                                                Enabled="true" Width="80px" />
                     </div>
                                            
                    </td>
                    </tr>
</table>
</center>
</div>
</form>
</body>
</html>