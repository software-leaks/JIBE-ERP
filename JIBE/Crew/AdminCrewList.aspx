<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminCrewList.aspx.cs" Inherits="Crew_AdminCrewList"  MasterPageFile="~/Site.master"  Title="Admin Crew List" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/CrewIndex_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/InterviewSchedule_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                 <td style="width: 34%; text-align: center; font-weight: bold;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Crew List"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            color: Black; text-align: left; background-color: #fff;">
            <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
                   
                        <table border="0" cellpadding="2" cellspacing="0" style="width: auto;">
                            <tr>
                                <td>
                                    Search&nbsp&nbsp<asp:TextBox ID="txtFreeTextSearch" runat="server" Width="80px"></asp:TextBox>
                                </td>
                                 <td>
                                    Manning Office&nbsp&nbsp<asp:DropDownList ID="ddlManningOfficeList" runat="server" Width="156px"></asp:DropDownList>
                                </td>
                                 <td style="text-align: right" rowspan="2">
                                    <asp:Button ID="BtnSearch" runat="server" Width="75px" OnClick="btnSearch_Click" Text="Search"
                                         CssClass="btnCSS" />
                                </td>
                                <td style="text-align: right" rowspan="2">
                                    <asp:Button ID="btnClearFilter" runat="server" Width="75px" OnClick="btnClearFilter_Click" Text="Clear"
                                         CssClass="btnCSS" />
                                </td>
                            </tr>
                       </table>
                 
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel_Grid" runat="server">
            <ContentTemplate>
                <div id="grid-container" style="margin-top: 2px">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3"
                        CellSpacing="0" Width="100%" EmptyDataText="No Record Found" CaptionAlign="Bottom" 
                        GridLines="None" DataKeyNames="ID"  AllowPaging="false" AllowSorting="true" CssClass="GridView-css"  OnRowEditing="GridView_RowEditing">
                        <Columns>
                            <asp:TemplateField HeaderText="Staff Code" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("ID")%>'   Visible="false"  CssClass="sailingInfo"></asp:Label>
                                    <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("ID")%>'
                                        Target="_blank" Text='<%# Eval("STAFF_CODE")%>' CssClass="pin-it"></asp:HyperLink>
                                    <asp:Label ID="lblX" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblSTAFF_NAME" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("ID")%>'
                                        Target="_blank" Text='<%# Eval("Staff_FullName")%>' ForeColor="Black"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Name")%>'  crewid='<%# Eval("ID")%>'  CssClass="sailingInfo"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Manning Office" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblManningOffice" runat="server" Text='<%# Eval("MANNING_OFFICE")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nation" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSTAFF_NATIONALITY" runat="server" Text='<%# Eval("ISO_Code")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Passport No." HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPassportNo" runat="server" Text='<%# Eval("Passport_Number")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Age" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblAvailable_From_Date" runat="server" Text='<%# Eval("Age")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Status" >
                                <ItemTemplate>
                                    <asp:Label ID="lblCrewStatus" runat="server" Text='<%# Eval("CrewStatus")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60px" Font-Size="10px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField ShowHeader="False" ItemStyle-Width="20px">
                                <HeaderTemplate>
                                            Manning Office 
                                </HeaderTemplate>
                                <ItemTemplate>
                                       <asp:ImageButton ID="LinkButton2" runat="server" AlternateText="Edit" CausesValidation="False"
                                            CommandName="Edit" ImageUrl="~/images/FBMMsgBody.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                         </Columns>
                        <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                        <PagerStyle CssClass="PagerStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                    </asp:GridView>
                </div>
                <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                    background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                    background-color: #F6CEE3;">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 60%">
                                <uc1:ucCustomPager ID="ucCustomPager_CrewList" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Staff"
                                    AlwaysGetRecordsCount="true" OnBindDataItem="FillGridView" />
                            </td>                         
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   
<asp:UpdatePanel ID="UpdatePnl" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="divManningOffice" style="font-family: Tahoma; color: black; display: none;">
                    <center>
                        <div class="error-message" onclick="javascript:this.style.display='none';">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                        <div style="font-family: Tahoma; font-size: 12px; border: 1px solid Gray; width: auto">
                            <div style="padding: 0px; padding: 2px; border-top: 0; background-color: #5588BB;
                                color: #FFFFFF; text-align: center;">
                                <b>Change Manning Office</b>
                            </div>
                     </center>
                     <table>
                        <tr>
                            <td>
                                    Manning Office
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlManningOffice" runat="server" Width="156px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                   Remarks
                            </td>
                            <td>
                                <asp:TextBox ID="txtCrewRemarks" runat="server" TextMode="MultiLine" Height="200px"
                                    Width="450px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align ="center" >
                                </hr>
                                <div style="background-color: #F0F0F0">
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" Text="Save" runat="server" />
                                </div>
                            </td>
                        </tr>
                     </table>
                 </div>
                </ContentTemplate>
 </asp:UpdatePanel>  
</asp:Content>

