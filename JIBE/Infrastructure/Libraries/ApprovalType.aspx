<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalType.aspx.cs" Inherits="Infrastructure_Libraries_ApprovalType" MasterPageFile="~/Site.master" Title="Approval Type"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>     
      <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">              
           <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
      </asp:UpdateProgress>
      <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 900px;
            height: 100%;">
             <div class="page-title">
              Approval Type
            </div>
            <div style="height: 650px; width: 900px; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 12%">
                                        Type :&nbsp;
                                    </td>
                                    <td align="left" style="width: 30%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Approval Limit" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="6">
                                        <asp:GridView ID="gvApprovalType" runat="server" AutoGenerateColumns="False"  DataSourceId="ObjectDataSource_ApprovalType"  DataKeyNames="ID" EmptyDataText="No Record Found"
                                            AllowSorting="True" CaptionAlign="Bottom" CellPadding="4"
                                            Font-Size="14px" GridLines="None" Width="600px" CssClass="gridmain-css">
                                          <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                          <RowStyle CssClass="RowStyle-css" />
                                          <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                            <Columns>
                                                <asp:TemplateField  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left">
                                                     <HeaderTemplate>
                                                        Type
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTYPE_Value" runat="server" Text='<%#Eval("TYPE_VALUE")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtTypeName" Font-Size="12px" MaxLength="50" Width="400px"
                                                            runat="server" Text='<%#Bind("TYPE_VALUE")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                 <asp:CheckBoxField DataField="AMOUNT_APPLICABLE" HeaderText="Amount Applicable" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                     <HeaderTemplate>
                                                        Action
                                                    </HeaderTemplate>
                                                    <EditItemTemplate>
                                                        <asp:ImageButton ID="btnAccept" runat="server" AlternateText="Update" CausesValidation="False"
                                                            CommandName="Update" ImageUrl="~/images/accept.png" />
                                                        <asp:ImageButton ID="btnReject" runat="server" AlternateText="Cancel" CausesValidation="False"
                                                            CommandName="Cancel" ImageUrl="~/images/reject.png" />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit" CausesValidation="False"
                                                            CommandName="Edit" ImageUrl="~/images/edit.gif" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left" ShowHeader="False">
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete" CausesValidation="False" 
                                                            CommandName="Delete" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to delete the record?')" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="crew-interview-grid-alternaterow" />
                                            <EditRowStyle CssClass="crew-interview-grid-editrow" />
                                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle CssClass="crew-interview-grid-row" />
                                            <SortedAscendingCellStyle CssClass="crew-interview-grid-col-sorted-asc" />
                                            <SortedAscendingHeaderStyle CssClass="crew-interview-grid-header-sorted-asc" />
                                            <SortedDescendingCellStyle CssClass="crew-interview-grid-col-sorted-desc" />
                                            <SortedDescendingHeaderStyle CssClass="crew-interview-grid-header-sorted-desc" />
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindApprovalType" />
                                <asp:ObjectDataSource ID="ObjectDataSource_ApprovalType" runat="server"  TypeName="SMS.Business.Infrastructure.BLL_Infra_Approval_Type" SelectMethod="Get_Approval_Type"
                                  DeleteMethod="DEL_Approval_Type"  UpdateMethod="UPD_Approval_Type"  OnUpdating="ObjectDataSource_Updating" >
                                    <UpdateParameters> 
                                        <asp:Parameter Name="ID" Type="Int32" />
                                        <asp:Parameter Name="TYPE_VALUE" Type="String" />
                                        <asp:Parameter Name="AMOUNT_APPLICABLE" Type="Int32" />
                                        <asp:SessionParameter Name="Modified_By" SessionField="userid" Type="Int32" />
                                    </UpdateParameters>
                                    <DeleteParameters>
                                        <asp:Parameter Name="ID" Type="Int32" />
                                        <asp:SessionParameter Name="Deleted_By" SessionField="userid" Type="Int32" />
                                    </DeleteParameters>
                                    <SelectParameters>
                                        <asp:ControlParameter Name="searchText" ControlID="txtfilter" PropertyName="Text"
                                            Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                    </td>
                                </tr>
                             </table>
                        </div>
                        <div id="divadd" title="Add Type" style="display: none; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 300px">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 150px">
                                        Type Key
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                       <asp:TextBox ID="txtTypeKey" CssClass="txtInput" Width="150px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 150px">
                                        Type Name
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtTypeName" CssClass="txtInput" Width="150px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 150px">
                                        Amount Application
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkAmountApplicable" runat="server" />
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td  colspan="3" >
                                        <asp:Button ID="btnsave" runat="server" Text="Save"
                                            OnClick="btnSave_Click" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                            OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                            </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
    </center>
</asp:Content>