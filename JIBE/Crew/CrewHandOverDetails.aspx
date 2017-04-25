<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Title="Crew Handover Details"
    CodeFile="CrewHandOverDetails.aspx.cs" Inherits="CrewHandOverDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
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
    <div style="width: 1200px; text-align: left;">
        <div class="page-title">
            <b>Crew Handover Details </b>
        </div>
        <div id="dvPageContent" class="page-content-main">
            <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                color: Black; text-align: left; background-color: #fff;">
                <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="BtnSearch">
                    <table border="0" cellpadding="2" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td>
                                Vessel :
                            </td>
                            <td>
                                <asp:TextBox ID="txtVesselName" ReadOnly="true" runat="server" Width="250px" CssClass="input" Height ="22px"></asp:TextBox>
                            </td>
                           
                            <td>
                                Staff Rank :
                            </td>
                            <td>
                                <asp:TextBox ID="txtStaffRank" ReadOnly="true" runat="server" Width="250px" CssClass="input" Height ="22px"></asp:TextBox>
                            </td>
                            <td>
                                Handover Date:
                            </td>
                            <td>
                                <asp:TextBox ID="txtHandOverDate" ReadOnly="true" runat="server" Width="90px" CssClass="input" Height ="22px"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                <asp:Button ID="BtnSearch" runat="server" OnClick="BtnSearch_Click" Text="Search"
                                    Width="80px" Height="25px" CssClass="btnCSS" />
                            </td>
                        </tr>
                        <tr>
                         <td>
                                Staff Code :
                            </td>
                            <td>
                                <asp:TextBox ID="txtStaffCode" ReadOnly="true" runat="server" Width="250px" CssClass="input" Height ="22px"></asp:TextBox>
                            </td>
                            <td>
                                Staff Name :
                            </td>
                            <td>
                                <asp:TextBox ID="txtStaffName" ReadOnly="true" runat="server" Width="250px" CssClass="input" Height ="22px"></asp:TextBox>
                            </td>
                            <td>Search :</td>
                            <td><asp:TextBox ID="txtSearchText" runat="server" Width="200px" CssClass="input" Height ="22px"  ></asp:TextBox>  </td>
                            <td></td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div style="border: 1px solid gray; margin-top: 1px;">
                <br />
                <cc1:TabContainer ID="TabSCM" runat="server" ActiveTabIndex="0" OnActiveTabChanged="TabSCM_ActiveTabChanged"
                    AutoPostBack="true">
                    <cc1:TabPanel runat="server" HeaderText="&nbsp; Handover &nbsp;" ID="HandOver" TabIndex="0">
                        <ContentTemplate>
                            <div>
                                <div id="grid-container" style="margin-top: 2px">
                                    <asp:GridView ID="gvHandOver" runat="server" AutoGenerateColumns="False" 
                                        CellPadding="3" Width="100%" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                                        GridLines="None" DataKeyNames="ID" OnRowDataBound="gvHandOver_RowDataBound"
                                        AllowSorting="True" 
                                        OnSorting="gvHandOver_Sorting" CssClass="GridView-css">
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="True" 
                                            Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("ROWNUM")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Handover List">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("HANDOVER_QUESTION")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="450px" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Handover Details">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblreview" runat="server" Text='<%# Eval("HANDOVER_Answer")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="450px" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                        <PagerStyle CssClass="PagerStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    </asp:GridView>
                                </div>
                                <br />
                                  <br />  

                                <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                                    background: url(../Images/bg.png) repeat left top; color: Black; text-align: left;
                                   ">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="width: 60%">
                                                     <uc1:ucCustomPager ID="ucCustomPager" runat="server" PageSize="30" OnBindDataItem="BindHandOver" />
                                            </td>
                                            <td style="width: 20px">
                                            </td>
                                            <td style="width: 210px">
                                            </td>
                                            <td style="text-align: left">
                                                <div id="dvInterviewSchedule">
                                                </div>
                                            </td>
                                            <td style="text-align: right">
                                                <span style="color: Blue;">
                                                    <asp:Label ID="lblSEQ" runat="server"></asp:Label></span>&nbsp;&nbsp; &nbsp;
                                                <img id="imgLoading" src="../Images/ajax.gif" alt="" style="display: none;" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                                    background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;">
                             <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                          <td style="width: 20%; text-align:left;vertical-align:top;font-weight:bold">
                                          <asp:Label ID="lbltxtRelievingMasterRemark" runat ="server" Text="Relieving Master's Remarks :"></asp:Label> </td>
                                            <td style="width: 30%">
                                            <asp:TextBox ID="txtRelievingMasterRemark" runat ="server" ReadOnly ="True" TextMode ="MultiLine" Height ="60px" Width ="95%"  CssClass="input"></asp:TextBox>
                                            </td>
                                           <td style="width: 20%;text-align:left;vertical-align:top;font-weight:bold">
                                              <asp:Label ID="lbltxtRelievedMasterRemark" runat ="server" Text="Relieved Master's Remarks :"></asp:Label>                                              
                                            </td>
                                            <td style="width: 30%">
                                             <asp:TextBox ID="txtRelievedMasterRemark" runat ="server" ReadOnly ="True" 
                                                    TextMode ="MultiLine" Height ="60px" Width ="95%"  CssClass="input"></asp:TextBox>
                                            </td>
                                                
                                        </tr>
                                    </table>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="&nbsp; Check List &nbsp;" ID="CheckList" TabIndex="1">
                        <ContentTemplate>
                            <div>
                                <div id="Div1" style="margin-top: 2px">
                                    <asp:GridView ID="gvCheckList" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                        CellSpacing="0" Width="100%" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                                        GridLines="None" DataKeyNames="ID" OnRowDataBound="gvCheckList_RowDataBound"
                                        AllowPaging="false" AllowSorting="true"
                                       OnSorting="gvCheckList_Sorting" CssClass="GridView-css">
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("ROWNUM")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Handover Check List" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("HANDOVER_QUESTION")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="450px" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Handed Over" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAnswer" Visible='<%# Eval("chkVisible").ToString()=="1"?true:false%>'
                                                        runat="server" Checked='<%# Eval("IsANSWER").ToString()=="1"?true:false%>' Enabled ="false"  />
                                                    <asp:Label ID="lblreview" runat="server" Text='<%# Eval("Checklist_Answer")%>' Visible='<%# Eval("chkTextbox").ToString()=="1"?true:false%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="450px" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                        <PagerStyle CssClass="PagerStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    </asp:GridView>
                                </div>
                                <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                                    background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                                    ">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="width: 60%">
                                              <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="30" OnBindDataItem="BindCheckList" />
                                               <%-- <uc1:ucCustomPager ID="ucCustomPager1" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Staff"
                                                    AlwaysGetRecordsCount="true" OnBindDataItem="BindCheckList" />--%>
                                            </td>
                                            <td style="width: 20px">
                                            </td>
                                            <td style="width: 210px">
                                            </td>
                                            <td style="text-align: left">
                                                <div id="Div2">
                                                </div>
                                            </td>
                                            <td style="text-align: right">
                                                <span style="color: Blue;">
                                                    <asp:Label ID="Label1" runat="server"></asp:Label></span>&nbsp;&nbsp; &nbsp;
                                                <img id="img1" src="../Images/ajax.gif" alt="" style="display: none;" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </div>
        </div>
        <div id="dialog" title="Remarks">
        </div>
        <div id="dvToolTip" style="display: none;">
        </div>
    </div>
</asp:Content>
