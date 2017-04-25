<%@ Page Title="Approval Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Approval_Report.aspx.cs" Inherits="PO_LOG_Approval_Report" %>


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
      <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
            height: 100%;">
             <div class="page-title">
              Approval Limit
            </div>
            <div style="height: 650px;  color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 12%">
                                        PO Type :&nbsp;
                                    </td>
                                    <td align="left"  style="width: 10%">
                                       <asp:DropDownList ID="ddlPOType" runat="server" Width="200px" 
                                            CssClass="txtInput" onselectedindexchanged="ddlPOType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                     <td align="right" style="width: 10%">
                                        Group Name :&nbsp;
                                    </td>
                                    <td align="left"  style="width: 10%">
                                       <asp:DropDownList ID="ddlGroupName" runat="server" Width="200px" CssClass="txtInput">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 10%">
                                        Account Classification :&nbsp;
                                    </td>
                                    <td align="left"  style="width: 10%">
                                       <asp:DropDownList ID="ddlAccClassifictaion" runat="server" Width="200px" CssClass="txtInput">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="left" style="width: 20%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvApproval" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvApproval_RowDataBound" DataKeyNames="variable_Code" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both" OnSorting="gvApproval_Sorting" AllowSorting="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                   <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Group Name">
                                            <HeaderTemplate>
                                             Group Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblApproval_Group_Name" runat="server" Text='<%#Eval("Approval_Group_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="8%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acct Code">
                                            <HeaderTemplate>
                                               Acct Code
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblvariable_Code" runat="server" Text='<%#Eval("variable_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="5%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acct Name">
                                             <ItemTemplate>
                                                <asp:Label ID="lblVARIABLE_NAME" runat="server" Text='<%#Eval("VARIABLE_NAME")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="10%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO Approver">
                                            <HeaderTemplate>
                                              From
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblFrom_USD_Amount" runat="server" Text='<%#Eval("From_USD_Amount")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="5%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Approver">
                                            <HeaderTemplate>
                                               To
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblTo_USD_Amount" runat="server" Text='<%#Eval("To_USD_Amount")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="5%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" PO Approver">
                                            <HeaderTemplate>
                                               PO Approver
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblApprover_Users" runat="server" Text='<%#Eval("Approver_Users")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="18%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Invoice Approvers">
                                            <HeaderTemplate>
                                               Invoice Approvers
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblInvoice_Approvers" runat="server" Text='<%#Eval("Invoice_Approvers")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="15%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Advance Approver">
                                            <HeaderTemplate>
                                               Advance Approver
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblAdvanceApprover" runat="server" Text='<%#Eval("Advance_Approver")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="15%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindApproval" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                       
                    </ContentTemplate>
                   
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
