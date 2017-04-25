<%@ Page Title="Approval Limit" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="PO_Log_Approval_Limit.aspx.cs" EnableEventValidation="false" Inherits="PO_LOG_PO_Log_Approval_Limit" %>

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
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function Validation() {



            return true;
        }

        function OpenScreen(ID, MAXAPPROVALLIMIT) {

            var url = 'POLOG_Limit_Entry.aspx?Limit_ID=' + ID + '&MAX_APPROVAL_LIMIT=' + MAXAPPROVALLIMIT;
            OpenPopupWindowBtnID('Approval_Group', 'Approval Limit Entry', url, 'popup', 800, 750, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        } 

    </script>
    <style type="text/css">
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
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
        .HeaderStyle-center
        {
            background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #333333;
            font-size: 11px;
            padding: 5px;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #959EAF;
            border-collapse: collapse;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <center>     
      <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">              
           <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
      <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 900px;
            height: 100%;">
             <div class="page-title">
              Approval Limit
            </div>
            <div style="height: 650px; width: 900px; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 12%">
                                        Group :&nbsp;
                                    </td>
                                    <td align="left" colspan="5" style="width: 30%">
                                       <asp:DropDownList ID="DDLGroupFilter" runat="server" Width="400px" CssClass="txtInput">
                                        </asp:DropDownList>
                                    </td>
                                   
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Approval Limit" OnClientClick="OpenScreen(0, 0)"
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvUserType" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvUserType_RowDataBound" DataKeyNames="Limit_ID" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both" OnSorting="gvUserType_Sorting" AllowSorting="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                   <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Group Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblGroupNameHeader" runat="server" CommandName="Sort" CommandArgument="Group_Name"
                                                    ForeColor="Black">Group Name&nbsp;</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGroupName" runat="server" Text='<%#Eval("Group_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Min Approval Limit">
                                            <HeaderTemplate>
                                               Min Approval Amount
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblMin_Amt" runat="server" Text='<%#Eval("Min_Approval_Limit")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Max Approval Amount">
                                             <ItemTemplate>
                                                <asp:Label ID="lblMAX_APPROVAL_LIMIT" runat="server" Text='<%#Eval("Max_Approval_Limit")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="PO Approver">
                                            <HeaderTemplate>
                                               PO Approver
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblPOApprover" runat="server" Text='<%#Eval("PO_Approver")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Approver">
                                            <HeaderTemplate>
                                               Invoice Approver
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblInvoiceApprover" runat="server" Text='<%#Eval("Invoice_Approver")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Advance Approver">
                                            <HeaderTemplate>
                                               Advance Approver
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblAdvanceApprover" runat="server" Text='<%#Eval("Advance_Approver")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                         
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnClientClick='<%#"OpenScreen(&#39;"+ Eval("Limit_ID") +"&#39;);return false;"%>'
                                                                 ForeColor="Black" 
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                 OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[Limit_ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindUserApproval" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                       
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
