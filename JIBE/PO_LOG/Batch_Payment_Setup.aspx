<%@ Page Title="Batch Payment Setup" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Batch_Payment_Setup.aspx.cs" Inherits="PO_LOG_Batch_Payment_Setup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
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
    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("ctl00_MainContent_txtApprovalGroup").value == "") {
                alert("Please enter group.");
                document.getElementById("ctl00_MainContent_txtApprovalGroup").focus();
                return false;
            }


            return true;
        }
        function OpenScreen(ID, Job_ID) {

            var url = 'Batch_Payment_Setup_Entry.aspx?Payment_ID=' + ID + '&Supp_ID=' + Job_ID;
            OpenPopupWindowBtnID('Batch_Payment_Setup_Entry', 'Batch Payment Setup', url, 'popup', 520, 1100, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
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
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <progresstemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                 <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </progresstemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 900px;
            height: 100%;">
            <div class="page-title">
               Batch Payment Setup
            </div>
            <div style="height: 650px; width: 900px; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <contenttemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 12%">
                                        Supplier Name :&nbsp;
                                    </td>
                                    <td align="left" style="width: 30%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
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
                                      <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Approval Group"  OnClientClick="OpenScreen(0, 0)"
                                            ImageUrl="~/Images/Add-icon.png" />
                                        <%--<asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Approval Group" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" />--%>
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
                                <asp:GridView ID="gvPayment" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvPayment_RowDataBound" DataKeyNames="Payment_Mode_ID" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both" OnSorting="gvPayment_Sorting" AllowSorting="true">
                                   <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                   <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                          <asp:TemplateField HeaderText="Approval Group">
                                            <HeaderTemplate>
                                                Supplier Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplier_NAME" runat="server" Text='<%#Eval("Supplier_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approval Group">
                                            <HeaderTemplate>
                                               Payment Currency
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblPaymentCurrency" runat="server" Text='<%#Eval("Payment_Currency")%>'></asp:Label>
                                              
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Bank Name">
                                            <HeaderTemplate>
                                             Bank Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblBank_Name" runat="server" Text='<%#Eval("Receiving_Bank_Name")%>'></asp:Label>
                                              
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Country">
                                            <HeaderTemplate>
                                             Bank Country
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblCountry" runat="server" Text='<%#Eval("Receiving_Bank_Country_Code")%>'></asp:Label>
                                              
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="US Bank State">
                                            <HeaderTemplate>
                                             US Bank State
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblState" runat="server" Text='<%#Eval("Bank_State_Code")%>'></asp:Label>
                                              
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="wift/BIC Code">
                                            <HeaderTemplate>
                                             Swift/BIC Code
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblSwift_Code" runat="server" Text='<%#Eval("SWIFT_IBAN_Code")%>'></asp:Label>
                                              
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name">
                                            <HeaderTemplate>
                                             Destination ABA Number
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblABA_Number" runat="server" Text='<%#Eval("Destination_ABA")%>'></asp:Label>
                                              
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Code">
                                            <HeaderTemplate>
                                             Bank Code
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblPaymentCurrency" runat="server" Text='<%#Eval("Receiving_Bank_Code")%>'></asp:Label>
                                              
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Branch Code">
                                            <HeaderTemplate>
                                             Branch Code
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblBranch_Code" runat="server" Text='<%#Eval("Receiving_Branch_Code")%>'></asp:Label>
                                              
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Bank Code">
                                            <HeaderTemplate>
                                             Account Number
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblAccount_Number" runat="server" Text='<%#Eval("Receiving_Account_Number")%>'></asp:Label>
                                              
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Beneficary">
                                            <HeaderTemplate>
                                             Beneficary
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblBeneficary" runat="server" Text='<%#Eval("Receiving_Beneficiary_Name")%>'></asp:Label>
                                              
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Pay From Account">
                                            <HeaderTemplate>
                                             Pay From Account
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblPay_From_Account" runat="server" Text='<%#Eval("Payment_Account_ID")%>'></asp:Label>
                                              
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pay Mode">
                                            <HeaderTemplate>
                                             Pay Mode
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblPay_Mode" runat="server" Text='<%#Eval("Payment_Mode")%>'></asp:Label>
                                              
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update"  OnClientClick='<%#"OpenScreen(&#39;"+ Eval("Payment_Mode_ID") +"&#39;);return false;"%>'
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Payment_Mode_ID]")%>'  ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[Payment_Mode_ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
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
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindBatchPaymentSetup" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                       
                    </contenttemplate>
                    <triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>