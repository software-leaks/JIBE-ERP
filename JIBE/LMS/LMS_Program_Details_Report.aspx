<%@ Page Title="Training/Drill Detail Report" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="LMS_Program_Details_Report.aspx.cs" Inherits="LMS_Program_Details_Report" %>

<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="auc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />    
<%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: left;
            padding: 0px 3px 0px 0px;
            width: 150px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
        }
        
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 0px 2px 0px 3px;
            height: 20px;
            vertical-align: middle;
        }
        
        .tdrow
        {
            font-size: 11px;
            text-align: left;
            padding: 2px;
            vertical-align: top;
            line-height: 16px;
        }
    </style>
    <script type="text/javascript">

        function fn_OnClose() {

        }
        function showDiv(dv) {
            if (dv) {
                $('#' + dv).show();
            }
        }
        function closeDiv(dv) {
            if (dv) {
                $('#' + dv).hide();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Training/Drill Detail Report
    </div>
    <div class="page-content-main">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 40%; top: 30px; z-index: 2;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td class="tdh" style="vertical-align:top">
                       Vessel       :
                        </td>
                        <td class="tdd"  style="vertical-align:top">
                            <asp:Label ID="lblVessel" runat="server"></asp:Label>
                        </td>
                         <td class="tdh"  style="vertical-align:top">
                            Remarks :
                        </td>
                        <td class="tdd" colspan="2">
                            <asp:TextBox TextMode="MultiLine" Width="400px" Height="50px" ID="lblRemarks" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdh">
                            Training/Drill Name :
                        </td>
                        <td class="tdd">
                            <asp:Label ID="lblProgramName" runat="server"></asp:Label>
                        </td>
                         <td class="tdh" valign="top">
                            Training/Drill Description :
                        </td>
                        <td class="tdd">
                            <asp:TextBox ID="lblProgramDescription" runat="server" TextMode="MultiLine" Rows="3" Width="100%" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btndrillactivity" runat="server" Text="Drill Activity" Width="120px"
                                Height="24px"   ValidationGroup="additems" 
                                 onclick="btndrillactivity_Click"/>
                                                         
                        </td>

                    </tr>
                    <tr>
                      <td class="tdh">
                            Training Start Date :
                        </td>
                        <td class="tdd">
                            <asp:Label ID="lblTrgStDate" runat="server"></asp:Label>
                        </td>
                         <td class="tdh">
                            Training End Date :
                        </td>
                        <td class="tdd">
                            <asp:Label ID="lblTrgEnDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                      <tr>
                       
                          <td colspan="5" style="vertical-align: top;">
                          <div style="width: 100%; height: 400px; overflow: scroll">
                          <asp:GridView ID="gvTrainingProgram_Details" runat="server" 
                                  AutoGenerateColumns="false" CellPadding="4" CellSpacing="0" 
                                  CssClass="gridmain-css" DataKeyNames="CHAPTER_ID" 
                                  EmptyDataText="No Records Found !!" GridLines="None" 
                                  OnRowDataBound="gvTrainingProgram_Details_RowDataBound" Width="100%">
                                  <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                  <RowStyle CssClass="RowStyle-css" />
                                  <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                  <EmptyDataRowStyle Font-Bold="true" Font-Size="12px" ForeColor="Red" 
                                      HorizontalAlign="Center" />
                                  <Columns>
                                      <asp:TemplateField HeaderText="Chapter">
                                          <ItemTemplate>
                                              <asp:Label ID="lblChapter" runat="server" 
                                                  Text='<%#Eval("CHAPTER_DESCRIPTION")%>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Resource Item">
                                          <ItemTemplate>
                                              <asp:DataList ID="dlResourceItem" runat="server" BackColor="Transparent" 
                                                  ForeColor="Black" RepeatColumns="1" RepeatDirection="Vertical" 
                                                  RepeatLayout="Table">
                                                  <HeaderStyle Font-Bold="true" Font-Names="verdana" Font-Size="11px" 
                                                      ForeColor="Black" />
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblResourceItem" runat="server" Text='<%#Eval("ITEM_NAME") %>'></asp:Label>
                                                  </ItemTemplate>
                                              </asp:DataList>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Trainer">
                                          <ItemTemplate>
                                              <asp:Label ID="lblTrainer" runat="server" Text='<%#Eval("Rank_Short_Name")%>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Attendees">
                                          <ItemTemplate>
                                              <asp:DataList ID="dlAttendees" runat="server" BackColor="Transparent" 
                                                  ForeColor="Black" RepeatColumns="1" RepeatDirection="Vertical" 
                                                  RepeatLayout="Table">
                                                  <HeaderStyle Font-Bold="true" Font-Names="verdana" Font-Size="11px" 
                                                      ForeColor="Black" />
                                                  <ItemTemplate>
                                                      <table style="border:0px">
                                                          <tr>
                                                              <td style="border:0px CellPadding:1px">
                                                                  <asp:Label ID="lblAttendees" runat="server" Text='<%#Eval("Staff_Code") %>' CssClass="staffInfo"></asp:Label>
                                                              </td>
                                                              <td style="border:0px">
                                                                  <asp:Label ID="lblRank_Short_Name" runat="server" 
                                                                      Text='<%#Eval("Rank_Short_Name") %>'></asp:Label>
                                                              </td>
                                                              <td style="border:0px">
                                                                  <asp:Label ID="lblStaff_Name" runat="server" Text='<%#Eval("Staff_Name") %>'></asp:Label>
                                                              </td>
                                                          </tr>
                                                      </table>
                                                  </ItemTemplate>
                                              </asp:DataList>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Status">
                                          <ItemTemplate>
                                              <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Program_Status")%>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                  </Columns>
                              </asp:GridView>
                          </div>
                              
                              <auc:CustomPager ID="ucCustomPagerProgram_List" runat="server" 
                                  AlwaysGetRecordsCount="true" 
                                  OnBindDataItem="BindTrainingProgramDetailsInGrid" />
                          </td>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="divMessage" align="center">
            <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
        </div>
        <br />
    </div>
</asp:Content>
