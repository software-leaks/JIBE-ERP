<%@ Page Title="Supplier Evaluation List" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="ASL_Evalution_Index.aspx.cs"
    Inherits="ASL_ASL_Evalution_Index" %>

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

        function OpenScreen(ID, Job_ID) {

            var url = 'ASL_Evalution.aspx?ID=' + ID + '&Supp_ID=' + Job_ID;
            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Evalution', url, 'popup', 800, 1030, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <progresstemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </progresstemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-title" class="page-title">
                Supplier Evaluation List
            </div>
            <div style="height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanelport" UpdateMode="Conditional" runat="server">
                    <contenttemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px;">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td style="width: 15%" align="right">
                                        Search By Supplier Name / Code :&nbsp;
                                    </td>
                                    <td style="width: 20%" align="left">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="98%" CssClass="txtInput"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td style="width: 8%" align="right">
                                      
                                    </td>
                                    <td style="width: 20%" align="left">
                                       
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
                                       <%-- <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Propose Supplier" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" OnClientClick='OpenScreen(null,null);return false;' />--%>
                                    </td>
                                    <td style="width: 5%">
                                       <%-- <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Excel-icon.png" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvEvaluation" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False"  DataKeyNames="Evaluation_ID,Supplier_Code"
                                    CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"   CssClass="gridmain-css"  
                                    AllowSorting="true">
                                  <HeaderStyle CssClass="HeaderStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                    <Columns>
                                    <asp:TemplateField HeaderText="Supplier_Name">
                                        <HeaderTemplate>
                                           Supplier Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:Label ID="lblSupplierName" runat="server" Text='<%#Eval("Supplier_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="For Period">
                                        <HeaderTemplate>
                                            For Period(days)
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPeriod" runat="server" Text='<%#Eval("For_Period")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Status">
                                        <HeaderTemplate>
                                            Proposed Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPropose_Status" runat="server" Text='<%#Eval("Propose_Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Evalution Status">
                                        <HeaderTemplate>
                                            Evalution Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEval_Status" runat="server" Text='<%#Eval("Eval_Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Propose By">
                                        <HeaderTemplate>
                                            Propose By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPropose_By" runat="server" Text='<%#Eval("Proposed_By")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created Date">
                                        <HeaderTemplate>
                                            Created Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateOfCreatation" runat="server" Text='<%# Eval("Created_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved By">
                                        <HeaderTemplate>
                                            Approved By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved_By" runat="server" Text='<%#Eval("Approved_By")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approver remarks">
                                        <HeaderTemplate>
                                            Approver remarks
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Approver_Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="200px" CssClass="PMSGridItemStyle-css">
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
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnClientClick='<%#"OpenScreen(&#39;"+ Eval("Evaluation_ID") +"&#39;);return false;"%>'
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Evaluation_ID]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                          <asp:Button ID="btnApprove" runat="server" CommandArgument='<%#Eval("[Evaluation_ID]")%>' Text="Approve" OnCommand="btnApprove_Click"  />
                                                        </td>
                                                      <td>
                                                          <asp:Button ID="btnReject" runat="server" CommandArgument='<%#Eval("[Evaluation_ID]")%>' Text="Reject" OnCommand="btnReject_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindGrid" />
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
