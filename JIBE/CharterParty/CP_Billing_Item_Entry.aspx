

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site_NoMenu.master"  CodeFile="CP_Billing_Item_Entry.aspx.cs"
    Inherits="CP_Billing_Item_Entry2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
    <script type="text/javascript">
        function numbersonly(e) {
            var keycode = e.charCode ? e.charCode : e.keyCode;
            if (!(keycode == 46 || keycode == 8 ||keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57)))
                {
                    return false
                    }
            return true;
            }

    
    </script>
     <style type="text/css">
           body, html
        {
            overflow-x: hidden;
        }
         
         .page

        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }

  .page
        {
            width: 100%;
           
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

        <center>
    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">
        <center>
            <table width="100%" cellpadding="2" cellspacing="0" >
                            <tr>
                    <td align="center" colspan="2">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltPageHeader" Text ="Billing Items" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                <td colspan="2" width="100%">

                <asp:GridView ID="gvBillingItems" runat="server"
                        AllowPaging="true" PageSize="10" OnPageIndexChanging = "gvBillingItems_PageIndexChanging"
                                GridLines="Both" Width="100%" DataKeyNames="Billing_Item_ID" 
                        onrowdatabound="gvBillingItems_RowDataBound">
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Action"  >
                                        <ItemTemplate >

                                                  <table cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td>
                                                            <asp:ImageButton ID="imgUpdate" style="border: 0; width: 14px; height: 14px" Text="Update"  onclick="imgUpdate_Click"   ForeColor="Black"
                                                                ImageUrl="../Images/edit.gif" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="lbtnDelete" runat="server" 
                                                                    ImageUrl="~/images/delete.png" onclick="lbtnDelete_Click"
                                                                    OnClientClick="return confirm('Are you sure want to delete?')" ToolTip="Delete">
                                                                </asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                    </table>


                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="5%" 
                                            Wrap="False" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>


                </td>
                  </tr>
                    <tr>

                   <td width="30%" align="left" valign="top"><table> 
                <tr>
                

                <td width="23%" align="right">
                    <asp:Literal ID="ltItemGroup" runat="server" Text="Item Group :"></asp:Literal>
                </td>
                    <td align="right" class="style1" style="color: #FF0000; width:2% ">
                        *
                    </td>
                    <td width="25%" align="left">
                        <asp:DropDownList ID="ddlItemgroup" runat="server" Width="150px">
                        </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvFuelType" runat="server" Display="None" InitialValue="0"
                    ErrorMessage="Item group is mandatory." ControlToValidate="ddlItemgroup" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    </tr>
                <tr>
                <td width="23%" align="right">
                    <asp:Literal ID="ltUnit" runat="server" Text="Item Description :"></asp:Literal>
                </td>
                  <td align="right" class="style1" style="color: #FF0000; width:2% ">
       
                    </td>
                    <td width="25%" align="left">
                       <asp:TextBox ID="txtItemDescription" runat="server" Width="60%" TextMode="MultiLine"  MaxLength="1000" ></asp:TextBox>
<%--                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" 
                    ErrorMessage="Description is mandatory."  ControlToValidate="txtItemDescription" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>--%>

                    </td>
                    </tr>

               <tr>
                <td width="23%" align="right">
                    <asp:Literal ID="Literal1" runat="server" Text="Billing Interval :"></asp:Literal>
                </td>
                   <td align="right" class="style1" style="color: #FF0000; width:2% ">
                        *
                    </td>
                    <td width="25%" align="left">
                        <asp:TextBox ID="txtBilling_Interval" runat="server" Width="40px"  MaxLength="5" ></asp:TextBox>&nbsp;

                         <asp:DropDownList ID="ddlIntervalUnit"  runat="server"  Width="80px">
                        <asp:ListItem Text="Day" value="Day"></asp:ListItem>
                        <asp:ListItem Text="Month" value="Month"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RegularExpressionValidator ID="RegInvoiceValue" runat="server" ErrorMessage="Interval is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtBilling_Interval"
                                    ForeColor="Red" ValidationExpression="^[0-9.-]+$">
                                </asp:RegularExpressionValidator>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  Display="None"
                    ErrorMessage="Billing Interval is mandatory." ControlToValidate="txtBilling_Interval" ValidationGroup="vgSubmit" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>

                    </tr>
                    <tr>
                <td width="23%" align="right">
                    <asp:Literal ID="Literal2" runat="server" Text="Billing Rate :"></asp:Literal>
                </td>
                   
                        <td align="right" class="style1" style="color: #FF0000; width:2% ">
                        *
                    </td>
                    <td width="25%" align="left">
                        <asp:DropDownList ID="ddlItemRate"  runat="server" >
                        <asp:ListItem Text="Per day Prorata" value="Per day Prorata"></asp:ListItem>
                        <asp:ListItem Text="Per month Prorata" value="Per month Prorata"></asp:ListItem>
                        <asp:ListItem Text="Per 30 Days" value="Per 30 Days"></asp:ListItem>
                        <asp:ListItem Text="Per round Voyage" value="Per round Voyage"></asp:ListItem>
                        <asp:ListItem Text="Percent" value="Percent"></asp:ListItem>
                        <asp:ListItem Text="Lumpsum" value="Lumpsum"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>

                    <tr>
                    <td colspan="3" align = "center" style="color: #FF0000;" >
                    
                    <asp:Literal ID="ltmessage" runat = "server"> </asp:Literal>
                    </td>
                    </tr>
                    <tr>
                    <td colspan="3">
                    &nbsp;
                     <asp:ValidationSummary ID="vsDelivery" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"  runat ="server" ValidationGroup = "vgSubmit" />
                    </td>
                    </tr>
                     <tr>
                        <td colspan="3" align="center">
                       <asp:Button ID="btnSave" runat="server" Width ="100px" ValidationGroup="vgSubmit" Font-Bold="true"
                                Text="Save" onclick="btnSave_Click" />&nbsp;
                        <asp:Button ID="btnSaveClose" runat="server" ValidationGroup="vgSubmit" Text="Save & Close" Visible="false" onclick="btnSaveClose_Click" />&nbsp;
                        <asp:Button ID="btnClear" runat="server" Text="Clear Selection" 
                                onclick="btnClear_Click" />

                        </td>
                    </tr>             
                    </table></td>



                         <td  align="center" width="70%">
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
                                                                  <asp:HiddenField ID="hdnBilling_Item_Id" runat = "server" Value='<%#Eval("Billing_Item_Amount_Id")%>' />
                                                                <asp:HiddenField ID="hdnTPId" runat = "server" Value='<%#Eval("Trading_Range_Id")%>' />
                                                                  <asp:HiddenField ID="hdnDate" runat = "server" Value='<%#Eval("Trading_Date")%>' />
                                                                <asp:TextBox ID="txtPWFF" Width="90px"  Text='<%#Eval("Trading_Date")%>'   runat="server"></asp:TextBox>
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
                                                                 <FooterTemplate>
                                                                    <asp:Button ID="btnAddNewItem" Text="Add Period" BackColor="#0066cc" BorderStyle="None"
                                                                        ForeColor="WhiteSmoke" runat="server" OnClick="btnAddNewItem_Click" />
                                                                </FooterTemplate>
                                                                 <FooterStyle HorizontalAlign="Left" />
                                                                 <ItemStyle Width="40%"  VerticalAlign="Top"/>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Trading Range" DataField="Trading_Range"
                                                                UniqueName="SortOrder" Visible="true">
                                                                <ItemTemplate>
                                                                          <asp:TextBox ID="txtTradingRange" Enabled="false"  runat="server" Width="98%"  MaxLength="1000" Text='<%#Eval("Trading_Range")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="35%" VerticalAlign="Top"/>
                                                            </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Amount" DataField="Item_Amount"
                                                                UniqueName="SortOrder" Visible="true">
                                                                <ItemTemplate>
                                                                          <asp:TextBox ID="txtItem_Amount" runat="server"  Width="98%"  MaxLength="1000" Text='<%#Eval("Amount")%>'></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvItem_Quantity" runat="server" ControlToValidate="txtItem_Amount"
                                                                        ErrorMessage="Amount cannot be blank." ValidationGroup="vgSubmit" ></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="15%" />
                                                            </telerik:GridTemplateColumn>

                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Action"
                                                                Visible="false" UniqueName="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgDelete" runat="server" OnCommand="lbtnDelete_Click" OnClientClick="return confirm('Are you sure want to delete?')"
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

                             &nbsp;</td>
                    </tr>

</table>
</center>
</div>
</center>
</asp:Content>