

<%@ Page Language="C#" AutoEventWireup="true" Title="PI Details" MasterPageFile="~/Site.master" EnableEventValidation="false" CodeFile="PI_Details.aspx.cs"
    Inherits="PI_Details" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script type="text/javascript">
        function numbersonly(e) {
            var keycode = e.charCode ? e.charCode : e.keyCode;
            if (!(keycode == 46 || keycode == 8 ||keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57)))
                {
                    return false
                    }
            return true;
            }



            function ValidateSearch() {
                if (document.getElementById("ctl00_MainContent_ddlVessel").value == "0") {
                    alert("Please Select Vessel");
                    document.getElementById("ctl00_MainContent_ddlVessel").focus();
                    return false;
                }
                  return true;
            }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

        <center>
         <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">



            <table width="100%" cellpadding="2" cellspacing="0">
                            <tr>
                    <td align="center" colspan="6">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltPageHeader" Text ="PI detail" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                                <td align="right" >
                   
                     <asp:ImageButton ID="btnExport" runat="server" ToolTip="Export to Excel" 
                                ImageUrl="~/Images/Excel-icon.png" Visible="false" onclick="btnExport_Click" />&nbsp;
                </td>
                </tr>
                <tr>
                <td>
                <div width="100%">
                    <asp:UpdatePanel ID="updSingleValue" runat="server">
    <ContentTemplate>
        <center>
        
                <table width="100%">
                <tr>
                

                <td width="70%" valign="top"  style="border: 1px solid #cccccc">
                <table width="98%">
                <tr>
                    <td width="23%" align="right">
                    <asp:Literal ID="ltForYear" runat="server" Text="For Year :"></asp:Literal>
                </td>
                    <td align="right" class="style1" style="color: #FF0000; width:2% ">
                        *
                    </td>
                    <td width="25%" align="left">
                        <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                    </td>
                

                <td width="23%" align="right">
                    <asp:Literal ID="ltQtrMonth" runat="server" Text="Quarter :"></asp:Literal>
                </td>
                    <td align="right" class="style1" style="color: #FF0000; width:2% ">
              &nbsp;
                    </td>
                    <td width="25%" align="left">
            
                      <asp:DropDownList ID="ddlQuarter" runat="server">
                  
                      <asp:ListItem Value="Qtr-1" Text="Quarter 1"></asp:ListItem>
                      <asp:ListItem Value="Qtr-2" Text="Quarter 2"></asp:ListItem>
                      <asp:ListItem Value="Qtr-3" Text="Quarter 3"></asp:ListItem>
                      <asp:ListItem Value="Qtr-4" Text="Quarter 4"></asp:ListItem>
                      </asp:DropDownList>

                       <asp:DropDownList ID="ddlMonths" runat="server">
                  
                      <asp:ListItem Value="Jan" Text="January"></asp:ListItem>
                      <asp:ListItem Value="Feb" Text="February"></asp:ListItem>
                      <asp:ListItem Value="Mar" Text="March"></asp:ListItem>
                      <asp:ListItem Value="Apr" Text="April"></asp:ListItem>
                      <asp:ListItem Value="May" Text="May"></asp:ListItem>
                      <asp:ListItem Value="June" Text="June"></asp:ListItem>
                      <asp:ListItem Value="Jul" Text="July"></asp:ListItem>
                      <asp:ListItem Value="Aug" Text="August"></asp:ListItem>
                      <asp:ListItem Value="Sep" Text="September"></asp:ListItem>
                      <asp:ListItem Value="Oct" Text="October"></asp:ListItem>
                      <asp:ListItem Value="Nov" Text="November"></asp:ListItem>
                      <asp:ListItem Value="Dec" Text="December"></asp:ListItem>
                      </asp:DropDownList>


                    </td>

                    </tr>
                <tr>
                
                 <td width="23%" align="right">
                    <asp:Literal ID="ltItemName" runat="server" Text="Effective From :"></asp:Literal>
                </td>
                    <td align="right" class="style1" style="color: #FF0000; width:2% ">
                    *
                    </td>
                    <td width="25%" align="left">
                    <asp:TextBox ID="txtEffectivedate" runat="server" MaxLength="200" Width="100px" onkeypress="return false;" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender TargetControlID="txtEffectivedate" ID="caltxtWEF"  Format="dd-MM-yyyy" 
                      runat="server">  </ajaxToolkit:CalendarExtender>
                      <asp:CompareValidator ID="cmpEffectivedate" runat="server"
                ControlToValidate="txtEffectivedate" Display="None" ErrorMessage="Invalid effective Date" ValidationGroup="vgSubmit"
                Operator="DataTypeCheck" Type="Date">
                    </asp:CompareValidator>
                      <asp:RequiredFieldValidator ID="rfvEffectiveDate" runat="server" ValidationGroup="vgSubmit" ControlToValidate="txtEffectivedate" ErrorMessage ="Effective date required ." Display="None"  ></asp:RequiredFieldValidator>
                    </td>
                
                    <td width="23%" align="right">
                    <asp:Literal ID="Literal1" runat="server" Text="Effective To :"></asp:Literal>
                </td>
                    <td align="right" class="style1" style="color: #FF0000; width:2% ">

                    </td>
                    <td width="25%" align="left">
                    <asp:TextBox ID="txtEfffectTo" runat="server" MaxLength="200" Width="100px" onkeypress="return false;" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender TargetControlID="txtEfffectTo" ID="ceEffectTo"  Format="dd-MM-yyyy" 
                      runat="server">  </ajaxToolkit:CalendarExtender>

                      <asp:CompareValidator ID="cmpEffectiveTo" runat="server"
                        ControlToValidate="txtEfffectTo" Display="None" ErrorMessage="Invalid effective to Date" ValidationGroup="vgSubmit"
                        Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>

                    </td>

                    </tr>
                    
                <tr>
 
                <td width="23%" align="right">
                    <asp:Literal ID="ltSBUValue" Visible="false" runat="server" Text="SBU Value :"></asp:Literal>
                </td>
                    <td align="right" class="style1" style="color: #FF0000; width:2% ">
              &nbsp;
                    </td>
                    <td width="25%" align="left">
                    <asp:TextBox ID="txtSBU" runat="server" Visible="false"  Width="100px" MaxLength="50"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="rfvSeq" ControlToValidate = "txtSBU" runat="server" Display="None" ValidationGroup="vgSubmit" ErrorMessage ="Enter numbers only." ValidationExpression="((\d+)((\.\d{1,4})?))$"></asp:RegularExpressionValidator>
                    </td>

                </tr>
             

                    <tr>
                    <td colspan="6" align="center">
                    &nbsp;
                     <asp:ValidationSummary ID="vsDelivery" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"  runat ="server" ValidationGroup = "vgSubmit" />

                       <asp:Button ID="btnSave" runat="server" Width ="100px" ValidationGroup="vgSubmit" Font-Bold="true"
                                Text="Save" onclick="btnSave_Click" />&nbsp;
                        
                        <asp:Button ID="btnClear" runat="server" Text="Clear Selection" 
                                onclick="btnClear_Click" />

                        </td>
                               
                </tr>
                <tr>
                
                     <td align="right">
                    <asp:Literal ID="ltVessel"  runat="server" Text="Vessel :"></asp:Literal>
                    </td>
                    <td>
               &nbsp;
              </td>
              <td align="left">
                <asp:DropDownList ID="ddlVessel" width="90%" runat="server"></asp:DropDownList>
              </td>
              <td colspan="3" align ="left" valign="middle">
                 From:
                         <asp:TextBox ID="txtSearchFrom" runat="server" MaxLength="200" Width="100px" onkeypress="return false;" ></asp:TextBox>&nbsp;
                    <ajaxToolkit:CalendarExtender TargetControlID="txtSearchFrom" ID="ceSearchFrom"  Format="dd-MM-yyyy" 
                      runat="server">  </ajaxToolkit:CalendarExtender>
                To:
                     <asp:TextBox ID="txtSearchTo" runat="server" MaxLength="200" Width="100px" onkeypress="return false;"></asp:TextBox>&nbsp;
                    <ajaxToolkit:CalendarExtender TargetControlID="txtSearchTo" ID="ceSearchTo"  Format="dd-MM-yyyy" 
                      runat="server">  </ajaxToolkit:CalendarExtender>&nbsp;
                 <asp:ImageButton ID="btnportfilter" runat="server" ToolTip="Search" 
                   ImageUrl="~/Images/SearchButton.png" onclick="btnportfilter_Click"   />

                </td>
                </tr>
             <tr>

                <td  colspan="6">

                        <asp:GridView ID="gvPIList" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                CellPadding="1" CellSpacing="0" Width="100%"  DataKeyNames="Detail_ID"
                                AllowSorting="true" >
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                   <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                <asp:BoundField HeaderText="Vessel" DataField="Vessel_Name"  ItemStyle-Width="10%"  ItemStyle-CssClass="PMSGridItemStyle-css"/>
                                <asp:BoundField HeaderText="Effect From" DataField="Effective_From_Str"  ItemStyle-Width="15%" ItemStyle-CssClass="PMSGridItemStyle-css" />
                                 <asp:BoundField HeaderText="Effect To" DataField="Effective_To" ItemStyle-Width="15%" ItemStyle-CssClass="PMSGridItemStyle-css" />
                                 <asp:BoundField HeaderText="PI Value" DataField="Value" ItemStyle-Width="10%"  ItemStyle-ForeColor="Brown" ItemStyle-CssClass="PMSGridItemStyle-css"/>
                                <asp:BoundField HeaderText="Created ON" DataField="Date_Of_Creation"  ItemStyle-Width="15%"  ItemStyle-CssClass="PMSGridItemStyle-css"/>

                                    <asp:TemplateField >
                                        <HeaderTemplate>
                                         Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                    <asp:HiddenField ID="hdnVessel_Id" runat="server" Value= '<%#Eval("Vessel_ID")%>' />
                                                        <asp:ImageButton ID="ImgView" runat="server" Text="Update" OnCommand="onUpdate" CommandArgument='<%#Eval("[Detail_ID]")%>' 
                                                            CommandName="Select" ForeColor="Black" ToolTip="Update PI Value" ImageUrl="../../Images/edit.gif"
                                                            Height="16px"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="10" OnBindDataItem="BindGrid" />
                            

                </td>
     
       </tr>
       <tr>
       <td>  <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" /></td>
       </tr>
          
                    <tr>
                    <td colspan="6" align = "center" style="color: #FF0000;" >
                    
                    <asp:Literal ID="ltmessage" runat = "server"> </asp:Literal>
                    </td>
                
                </tr>
                
                </table>
                
                </td>

                <td width="30%" valign="top">
                
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

                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Vessel Name" DataField="Vessel_Name"
                                                                UniqueName="SortOrder" Visible="true">
                                                                <ItemTemplate>
                                                                <asp:HiddenField ID="hdnVessel_Id" runat="server" Value= '<%#Eval("Vessel_ID")%>' />
                                                                   <asp:label ID="lblVesselName" Enabled="false"  runat="server" Width="98%"   Text='<%#Eval("Vessel_Name")%>'></asp:label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="30%" VerticalAlign="Top" Wrap="true"/>
                                                            </telerik:GridTemplateColumn>

                                                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Value" DataField="Value"
                                                                UniqueName="SortOrder" Visible="true">
                                                                <ItemTemplate>
                                                                          <asp:TextBox ID="txtItem_Amount" runat="server"  Width="100px" MaxLength="10" Text='<%#Eval("Value")%>'></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvItem_Quantity" runat="server" ControlToValidate="txtItem_Amount"
                                                                        ErrorMessage="Value cannot be blank." ValidationGroup="vgSubmit" ></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="40%" />
                                                            </telerik:GridTemplateColumn>

                                                        </Columns>
                                                        <EditFormSettings>
                                                            <PopUpSettings ScrollBars="None" />
                                                        </EditFormSettings>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                                <div id="divEDIT" runat="server" visible="false">


                                                <table>
                                                <tr>
                                                <td width="50%">
                                                Vessel Name:
                                                </td>
                                               <td>
                                               <asp:Label ID="lblVesselName" ForeColor="CadetBlue" Font-Size="Large" runat="server" ></asp:Label>
                                               </td>
                                                </tr>
                                                <tr>
                                                 <td  width="50%">
                                                Value:
                                                </td>
                                                <td>
                                                <asp:TextBox ID="txtSB" runat="server" MaxLength="15" ></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Regex1" runat="server" ValidationExpression="((\d+)((\.\d{1,4})?))$" ErrorMessage="Format not correct!"
                                                ControlToValidate="txtSB" />

                                                </td>
                                                </tr>
                                                <tr>
                                                <td align="center" colspan="2">
                                                <asp:Button Text="Update" ID="btnUpdate" runat="server" onclick="btnUpdate_Click" />
                                                </td>
                                                </tr>
                                                </table>

                                                </div>

                </td>
              
                 </tr>
                </table>
                </center>
            </ContentTemplate>
            </asp:UpdatePanel>
            </div>
                </td>
  </tr>
</table>
</div>

</center>
</asp:Content>