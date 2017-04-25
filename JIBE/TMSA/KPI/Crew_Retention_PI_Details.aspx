

<%@ Page Language="C#" AutoEventWireup="true" Title="Rank wise PI Details" MasterPageFile="~/Site.master" CodeFile="Crew_Retention_PI_Details.aspx.cs"
    Inherits="Crew_Retention_PI_Details" %>
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
                            <asp:Literal ID="ltPageHeader" Text ="Rank wise PI detail" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>

                <tr>
                <td>
                <div width="100%">
<%--                    <asp:UpdatePanel ID="updSingleValue" runat="server">
    <ContentTemplate>--%>
        <center>
        
                <table width="100%">
                <tr>
                

                <td width="70%" valign="top"  style="border: 1px solid #cccccc">
                <table width="98%">
                <tr>
                
                     <td align="right" width="10%">
                    <asp:Literal ID="ltRanks"  runat="server" Text="Ranks :"></asp:Literal>
                    </td>
              <td align="left"  width="15%">
                <asp:DropDownList ID="ddlRank" width="90%" runat="server"></asp:DropDownList>
              </td>
              <td  align ="right" valign="middle" width="10%">
                  <asp:Literal ID="ltFrom"  runat="server" Text="From:"></asp:Literal>
     
                  
                  </td>
                   <td width="15%" >
                         <asp:TextBox ID="txtSearchFrom" runat="server" MaxLength="200" Width="100px" onkeypress="return false;" ></asp:TextBox>&nbsp;
                    <ajaxToolkit:CalendarExtender TargetControlID="txtSearchFrom" ID="ceSearchFrom"  Format="dd-MM-yyyy" 
                      runat="server">  </ajaxToolkit:CalendarExtender>
                     </td>
                      <td width="10%" align="right">
                 <asp:Literal ID="ltTo"  runat="server" Text="To:"></asp:Literal>
                 </td>
                 <td align="left">
                     <asp:TextBox ID="txtSearchTo" runat="server" MaxLength="200" Width="100px" onkeypress="return false;"></asp:TextBox>&nbsp;
                    <ajaxToolkit:CalendarExtender TargetControlID="txtSearchTo" ID="ceSearchTo"  Format="dd-MM-yyyy" 
                      runat="server">  </ajaxToolkit:CalendarExtender>&nbsp;


                </td>
                <td>
                   <asp:ImageButton ID="btnportfilter" runat="server" ToolTip="Search" 
                   ImageUrl="~/Images/SearchButton.png" onclick="btnportfilter_Click"   />
                </td>
                  <td align="right" >
                   
                     <asp:ImageButton ID="btnExport" runat="server" ToolTip="Export to Excel" 
                                ImageUrl="~/Images/Excel-icon.png"  onclick="btnExport_Click" />&nbsp;
                </td>
                </tr>
             <tr>

                <td  colspan="8">

                        <asp:GridView ID="gvPIDetails" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                         OnRowCancelingEdit="gvPIDetails_RowCancelingEdit" OnRowEditing="gvPIDetails_RowEditing"
                                                OnRowUpdating="gvPIDetails_RowUpdating" 
                                CellPadding="1" CellSpacing="0" Width="100%"  DataKeyNames="ID"
                                CssClass="gridmain-css" AllowSorting="true" >
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>

                                <asp:TemplateField  HeaderText="Rank" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                
                               <ItemTemplate>
                               <asp:Label ID="lblRank" runat="server"  Text='<%# Eval("Rank")%>'></asp:Label>
                               
                               </ItemTemplate>
                                </asp:TemplateField>
                               
                               <asp:TemplateField  HeaderText="Effect From" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                
                               <ItemTemplate>
                               <asp:Label ID="lblEffectiveFrom" runat="server"  Text='<%# Eval("Effective_From_Str")%>'></asp:Label>
                               
                               </ItemTemplate>
                                </asp:TemplateField>

                                
                               <asp:TemplateField  HeaderText="Effect To" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                
                               <ItemTemplate>
                               <asp:Label ID="lblEffectiveTo" runat="server"  Text='<%# Eval("Effective_To")%>'></asp:Label>
                               
                               </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="PI Value" HeaderStyle-HorizontalAlign="center"  ItemStyle-Width="15%"   ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Brown">
                                 <ItemTemplate>
                                 <asp:Label ID="lblValue" Text='<%# Eval("Value")%>' runat="server" />
                                 </ItemTemplate>
                   
                                    <EditItemTemplate>
                                    
                                    <asp:TextBox ID="txtValue" runat="server" Text='<%# Eval("Value")%>' ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvValue" runat="server" ControlToValidate="txtValue" ErrorMessage="Value is required!"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="reValue" runat="server" ControlToValidate="txtValue" ValidationExpression="^[0-9]{0,9}$" ErrorMessage="Only numbers allowed!" ValidationGroup="ValidatePI"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                 </asp:TemplateField>

                                <asp:TemplateField  HeaderText="Created On" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                               <ItemTemplate>
                               <asp:Label ID="lblCreatedOn" runat="server"  Text='<%# Eval("Date_Of_Creation")%>'></asp:Label>
                               </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField  HeaderText="Last Modified" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                               <ItemTemplate>
                               <asp:Label ID="lblModifiedOn" runat="server"  Text='<%# Eval("Date_Of_Modification")%>'></asp:Label>
                               </ItemTemplate>
                                </asp:TemplateField>


                            <asp:TemplateField HeaderText="Edit"  HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%"  >
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgEdit" ToolTip="Edit" runat="server" AlternateText="Edit" CausesValidation="False"
                                        CommandName="Edit" ImageUrl="~/images/edit.gif" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="ImgBtnAccept" ToolTip="Update" runat="server" AlternateText="Update"  ValidationGroup="ValidatePI" CausesValidation="true"
                                        CommandName="Update" ImageUrl="~/images/accept.png" />
                                    <asp:ImageButton ID="ImgBtnCancel" ToolTip="Cancel" runat="server" AlternateText="Cancel" CausesValidation="False"
                                        CommandName="Cancel" ImageUrl="~/images/reject.png" />
                                </EditItemTemplate>
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="10" OnBindDataItem="LoadSearchResults" />
                            

                </td>
     
       </tr>
       
                </table>
                
                </td>

              
                 </tr>
                </table>
                </center>
<%--            </ContentTemplate>
            </asp:UpdatePanel>--%>
            </div>
                </td>
  </tr>
</table>
</div>

</center>
</asp:Content>