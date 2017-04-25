

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site_NoMenu.master" EnableEventValidation="false" CodeFile="CP_Hire_Invoice_Prep.aspx.cs"
    Inherits="CP_Hire_Invoice_Prep" %>

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

    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
         <script language="javascript" type="text/javascript">
          function OpenScreen(ID, Inv_ID) {
                 var CPID = <%=CPID %>
                 var url = 'CP_Hire_Invoice_Entry.aspx?CPID=' + CPID + '&Inv_ID=' + Inv_ID;
                 OpenPopupWindowBtnID('CP', 'Hire Invoice Detail', url, 'popup', 650, 1000, null, null, false, false, true, null, 'ctl00_MainContent_btnSearch');
             }


          function MaskMoney(evt) {
            if (!(evt.keyCode == 9 || evt.keyCode == 190 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 37 || evt.keyCode == 39 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105))) {
                return false;
            }
            var parts = evt.srcElement.value.split(',');

            if (parts.length > 2) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
            if (parts.length == 2 && parts[1].length >= 2) return false;
        }

        function numbersonly(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode
            if (unicode != 8) { //if the key isn't the backspace key (which we should allow)
                if (unicode < 48 || unicode > 57) //if not a number
                    return false //disable key press
            }
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
            <table width="100%" cellpadding="2" cellspacing="0" >
                <tr>
                    <td align="center" colspan="2">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltPageHeader" Text ="Hire Invoice" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                <td colspan="2">
                <table width="100%">
                  <tr>
                <td width="10%" align="right">
                 <asp:Literal ID="ltInvoiceStatus" Text="Invoice Status :" runat="server"></asp:Literal>
                </td>
                <td align="left" width="40%" >
                <asp:UpdatePanel ID="updStatus" runat="server">
                    <ContentTemplate>
                  <asp:CheckBoxList ID="chkStatusList"  RepeatDirection="Horizontal" runat="server" AutoPostBack="true"
                        onselectedindexchanged="chkStatusList_SelectedIndexChanged"></asp:CheckBoxList>   
                        </ContentTemplate>
                        </asp:UpdatePanel>  
                 </td>
                    <td width="10%" align="right"> 
                        <asp:Literal ID="ltPayment" Text="Payment :" runat="server"></asp:Literal>
                    </td>
                    <td width="25%" align="left">
                    <asp:UpdatePanel ID="updatesearch" runat="server">
                    <ContentTemplate>
                    
                    <asp:CheckBoxList ID="chkPaymentType"  RepeatDirection="Horizontal" runat="server" AutoPostBack="true"
                            onselectedindexchanged="chkPaymentType_SelectedIndexChanged">
                    
                    <asp:ListItem Text="Outstanding" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Not Due" Value="2" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Matched" Value="3"></asp:ListItem>
                    </asp:CheckBoxList> 
                    </ContentTemplate>
                    </asp:UpdatePanel>  
                        </td>
                        <td align="left" width="15%" >
                           <asp:ImageButton ID="btnSearch" runat="server" ToolTip="Search" 
                                ImageUrl="~/Images/SearchButton.png" onclick="btnSearch_Click" />&nbsp;
                           <asp:ImageButton ID="ibtnAdd" runat="server" ToolTip="Add New Invoice" OnClientClick='OpenScreen(0,0);return false;'
                                            ImageUrl="~/Images/Add-icon.png"  />  
<%--                           <asp:ImageButton ID="imgInvPrep" runat="server" ToolTip="Invoice Preparation" OnClientClick='OpenInvPrep(0);return false;'
                        ImageUrl="~/Images/issue-list.gif"  />    --%>                
                        </td>
                    </tr>
                
                
                </table>
                
                </td>
                
                </tr>


                <tr>
                <td colspan="2">
                <table width="100%">
               <tr>
                         <td align="center" width="40%" valign="top">
                            <asp:GridView ID="gvHireInvoices" runat="server" AutoGenerateColumns="False" 
                                GridLines="Both" Width="98%" DataKeyNames="ID" AllowPaging="true" 
                                 PageSize="10" onpageindexchanging="gvHireInvoices_PageIndexChanging"  >
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Inv Date">
                                           <ItemTemplate>
                                            <asp:Label ID="lblCreatedOn" runat="server" 
                                                Text='<%# Eval("Hire_Invoice_Date") %>' 
                                                Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" ForeColor="Blue" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Inv No">
                                    <ItemTemplate>
                                         <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Hire_Invoice_No") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Wrap="true" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Due">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDueDate" runat="server" 
                                                Text='<%# Eval("Due_Date") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Period From">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPeriodfrom" runat="server" 
                                                Text='<%# Eval("PeriodFrom") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="15%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="To">
                                        <ItemTemplate>
                                            <asp:Label ID="lblperiodTo" runat="server" 
                                                Text='<%# Eval("PeriodTo") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Invoice Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBilledAmt" runat="server" 
                                                Text='<%# Eval("Billed_Amount") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" 
                                                Text='<%# Eval("Hire_Invoice_Status") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate >
                                        
                                        <asp:ImageButton ID="ibtnEdit" style="border: 0; width: 14px; height: 14px" ToolTip="Edit Header"  OnClientClick='<%#"OpenScreen((&#39;" + Eval("[Charter_Party_ID]") +"&#39;),(&#39;"+ Eval("[ID]") + "&#39;));return false;"%>'
                                         ForeColor="Black"  ImageUrl="../Images/edit.gif" runat="server" /> &nbsp;
                                        <asp:ImageButton ID="ibtnView" style="border: 0; width: 14px; height: 14px" ToolTip="Invoice Item"    OnClick="ibtnView_Click"
                                         ForeColor="Black" ImageUrl="~/Images/asl_view.png" runat="server" />
                                       <asp:ImageButton ID="imgPrint" style="border: 0; width: 14px; height: 14px" Text="Print Preview"  OnClick="imgPrint_Click"
                                         ForeColor="Black" ImageUrl="~/Images/Printer.png" runat="server" />
                                          </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="5%" 
                                            Wrap="False" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td width="60%" valign="top">
                         <div style="width: 100%;  overflow: auto">
                        <asp:gridview ID="gvbillingitems" runat="server" AllowAutomaticInserts="True" GridLines="None"  width="100%"
                                                    ShowFooter="false" ViewStateMode="Enabled" Skin="Office2007" Style="margin-left: 0px"
                                                     AutoGenerateColumns="true" OnRowDataBound="gvBillingItems_ItemDataBound"
                                                    
                                                    AllowMultiRowSelection="True" PageSize="10"  HeaderStyle-HorizontalAlign="Center"
                                                    AlternatingItemStyle-BackColor="#CEE3F6">
                                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css"  HorizontalAlign="Center" />
                                
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                <asp:TemplateField  HeaderText="Action"
                                            Visible="false" >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgDelete" runat="server" OnCommand="onDelete" OnClientClick="return confirm('Are you sure want to delete?')"
                                                    CommandArgument='<%#Eval("Billing_Item_ID")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                    Height="16px"></asp:ImageButton> &nbsp;
                                            <asp:ImageButton runat="server" AlternateText="Edit" ImageUrl="~/Images/edit.gif" Visible='<%# uaEditFlag %>'
                                            ID="cmdEdit"  OnClientClick='<%#"OpenScreen((&#39;" + Eval("Billing_Item_ID") +"&#39;));return false;"%>'  ToolTip="Edit"></asp:ImageButton>
                                            </ItemTemplate>
                                            </asp:TemplateField>

                                    </Columns>
                      </asp:gridview>
                     </div>

                        </td>
                    </tr>
                </table>
                </td>
                
                </tr>

                   </hr>
                    <tr>
                    
                    <td width="60%" valign="top">

                    <div class="freezing" style="width: 100%;" >
                        <telerik:RadGrid ID="rgdItems" runat="server" AllowAutomaticInserts="True" GridLines="None" Visible="true"
                                                    ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007" Style="margin-left: 0px;margin-top:10px;"
                                                    Width="100%" AutoGenerateColumns="False" OnItemDataBound="rgdItems_ItemDataBound" SelectedItemStyle-BackColor="Azure"
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
                                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="View"
                                                                UniqueName="CheckID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="lblID" runat="server" Value='<%#Eval("Hire_Invoice_Item_Id")%>' />
                                                                     <asp:ImageButton ID="ibtnView" style="border: 0; width: 14px; height: 14px" Visible="false"  OnClientClick='<%#"OpenScreen(0,(&#39;"+ Eval("[Hire_Invoice_Item_Id]") + "&#39;));return false;"%>'
                                                                        ForeColor="Black"       ImageUrl="~/Images/asl_view.png" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5%" VerticalAlign="Top" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Sl No" 
                                                                UniqueName="SortOrder" Visible="true">
                                                                <ItemTemplate>
                                                                          <asp:TextBox ID="txtSrno" Width="50px" MaxLength="2" runat="server" Text='<%#Eval("Sort_Order")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="5%" VerticalAlign="Top"/>
                                                            </telerik:GridTemplateColumn>

                                                             <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Group" 
                                                                UniqueName="Group" >
                                                                <ItemTemplate>
                                                                            <asp:HiddenField ID="hdItemGroup" Value='<%# Bind("Item_Group")%>' runat = "server" />
                                                                         <asp:DropDownList ID="ddlItemgroup" AutoPostBack="true" OnSelectedIndexChanged="ddlItemgroup_SelectedIndexChanged" runat="server" Width="100%"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" VerticalAlign="Top" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Detail"
                                                                Visible="true" UniqueName="ItemName">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Item_Name" Enabled="true" MaxLength="200" Width="100%" EnableViewState="true"
                                                                        runat="server" Text='<%# Bind("Item_Name")%>' Style="font-size: x-small"></asp:TextBox>

                                                                </ItemTemplate>
                                                               <FooterTemplate>
                                                                    <asp:Button ID="btnAddNewItem" Text="Add new item" BackColor="#0066cc" BorderStyle="None"  Visible='<%# pendingApprove %>'
                                                                        ForeColor="WhiteSmoke" runat="server" OnClick="btnAddNewItem_Click" />
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Left"  />
                                                               <ItemStyle Width="10%" VerticalAlign="Top"/>
                                                            </telerik:GridTemplateColumn>
                                                            
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="70px" HeaderText="Narration"
                                                                Visible="true" UniqueName="Item_details">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Item_details" Enabled="true" EnableViewState="true" MaxLength="200"
                                                                        runat="server" Text='<%# Bind("Item_Details")%>' Style="font-size: x-small"
                                                                        Width="100%" TextMode="MultiLine" Height="30px"></asp:TextBox>
 
                                                                </ItemTemplate>
                                                                <ItemStyle Width="15%" />
                                                            </telerik:GridTemplateColumn>

                                                             <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Add. Comm"
                                                                Visible="false" UniqueName="AddCommision">
                                                                <ItemTemplate>
                                                                <asp:HiddenField ID="hdAddComm" Value='<%# Bind("Address_Comm")%>' runat = "server" />
                                                                         <asp:DropDownList ID="ddlAddComm" runat="server" Width="100%">
                                                                         
                                                                         <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                         <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                         </asp:DropDownList>
                                                                </ItemTemplate>
                                                               <ItemStyle Width="7%"  VerticalAlign="Top"/>
                                                            </telerik:GridTemplateColumn>

                                                               <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Amount"
                                                                Visible="true" UniqueName="ItemAmt">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Item_Amount" Enabled="true" MaxLength="17" EnableViewState="true"
                                                                        runat="server" Text='<%# Bind("Item_Amount")%>' Style="font-size: x-small" Width="100px"
                                                                        Height="15px"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvItem_Amount" runat="server" ControlToValidate="Item_Amount"
                                                                        ErrorMessage="Amount cannot be blank." ValidationGroup ="ValidateItem" ></asp:RequiredFieldValidator>
                                                      
                                                                </ItemTemplate>
                                                               <ItemStyle Width="10%"  VerticalAlign="Top"/>
                                                            </telerik:GridTemplateColumn>

                                                              <telerik:GridTemplateColumn AllowFiltering="false"  HeaderText="Quantity"
                                                                Visible="true" UniqueName="ItemQty">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Item_Quantity" Enabled="true" MaxLength="10" EnableViewState="true"
                                                                        runat="server" Text='<%# Bind("Item_Quantity")%>' Style="font-size: x-small" Width="70px"
                                                                        Height="10px"></asp:TextBox>

                                                                      <asp:RequiredFieldValidator ID="rfvItem_Quantity" runat="server" ControlToValidate="Item_Quantity"
                                                                        ErrorMessage="Quantity cannot be blank." ValidationGroup ="ValidateItem" ></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="7%" VerticalAlign="Top"/>
                                                            </telerik:GridTemplateColumn>
                                                              <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Unit"
                                                                Visible="false" UniqueName="Unit">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Charter_Item_Unit" Enabled="true" MaxLength="10" EnableViewState="true"
                                                                        runat="server" Text='<%# Bind("Charter_Item_Unit")%>' Style="font-size: x-small" Width="50px"
                                                                        Height="15px"></asp:TextBox>
 
                                                                </ItemTemplate>
                                                                 <ItemStyle Width="5%" VerticalAlign="Top"/>
                                                            </telerik:GridTemplateColumn>

                                                            
                                                            <telerik:GridTemplateColumn HeaderText="Debit" UniqueName="Debit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LineTotalDebit" Text='<%#Eval("LineTotalDebit")%>' MaxLength="10" Style="font-size: x-small"
                                                                        Height="10px" runat="server" Width=" 70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="70px"></HeaderStyle>
                                                                <ItemStyle Width="7%" HorizontalAlign="Right"  VerticalAlign="Top" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Credit" UniqueName="Credit">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LineTotalCredit" Text='<%#Eval("LineTotalCredit") %>' MaxLength="5" Style="font-size: x-small"
                                                                        Height="10px" runat="server" Width=" 70px"></asp:Label>
                                                                </ItemTemplate>
                                                               <ItemStyle Width="7%" VerticalAlign="Top"  HorizontalAlign="Right"/>
                                                            </telerik:GridTemplateColumn>



                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Action"
                                                                 UniqueName="Delete">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgDelete" runat="server" Visible='<%# pendingApprove %>' OnCommand="onDelete" OnClientClick="return confirm('Are you sure want to delete?')"
                                                                        CommandArgument='<%#Eval("[Hire_Invoice_Item_Id]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                        Height="16px"></asp:ImageButton>
                                                                </ItemTemplate>
                                                             <ItemStyle Width="5%" VerticalAlign="Top"/>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                        <EditFormSettings>
                                                            <PopUpSettings ScrollBars="None" />
                                                            <PopUpSettings ScrollBars="None" />
                                                        </EditFormSettings>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                                <asp:Label ID="lblError" runat="server"></asp:Label>
                                                <br />
                                                <asp:Button ID="btnSave" runat="server" Visible = "false" Text="Save" ValidationGroup ="ValidateItem"  Height="30px" OnClick="btnSaveItem_Click"
                                                Width="80px" />
                     </div>
                     
                    </td>
                    <td width="40%">
                    
                      <iframe id="iFrame" runat="server" style="width:100%; height:450px; border:0px;"></iframe>
                    </td>
                    </tr>
</table>
</center>

</asp:Content>