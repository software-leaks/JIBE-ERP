<%@ Page Title="PMS Jobs Pending" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PMSJobNotDoneSummary.aspx.cs" Inherits="PMSJobNotDoneSummary" %>

<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
 <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function OnExportToExcel() {

            if (document.getElementById("ctl00_MainContent_optRptType_0").checked == true) {

                if (document.getElementById("ctl00_MainContent_DDLVessel").value == "0") {
                    alert("Please select vessel.");
                    document.getElementById("ctl00_MainContent_DDLVessel").focus();
                    return false;
                }
            }

            return true;
        }


        function OnSendEmail() {

            if (document.getElementById("ctl00_MainContent_optRptType_0").checked == false) {
                alert("Please select Rank - wise option.");
                return false;
            }

            if (document.getElementById("ctl00_MainContent_DDLVessel").value == "0") {
                alert("Please select vessel.");
                document.getElementById("ctl00_MainContent_DDLVessel").focus();
                return false;
            }


            return true;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="font-family: Tahoma; font-size: 12px; width: 100%;">
         <div class="page-title">
             Jobs Done / Not Done Summary
    
        </div>
            <div style="border: 1px solid #cccccc; padding-top: 10px; width: 100%; height: 45px;">
                <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Always" runat="server">
                    <ContentTemplate>
                        <table cellpadding="1" cellspacing="1" width="100%" style="color: Black; border-color: #0000FF;">
                            <tr>
                                <td style="width: 10%" align="right">
                                    Fleet :&nbsp;&nbsp;
                                </td>
                                <td style="width: 3%" align="left">
                                    <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        CssClass="txtInput" Font-Size="11px" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged"
                                        Width="120px">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%" align="right">
                                    Vessel :&nbsp;&nbsp;
                                </td>
                                <td style="width: 4%">
                                    <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="true" CssClass="txtInput"
                                        Font-Size="11px" Width="120px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%" align="right">
                                    Rank :&nbsp;&nbsp;
                                </td>
                                <td>
                                    <CustomFilter:ucfDropdown ID="ucf_DDLRank" Width="160" Height="200" UseInHeader="false"
                                        OnApplySearch="BindJobDoneSummary" runat="server" />
                                </td>
                                <td style="width: 35%" align="center">
                                    <asp:RadioButtonList ID="optRptType" RepeatDirection="Horizontal" runat="server"
                                        OnSelectedIndexChanged="optRptType_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Rank - wise&nbsp;&nbsp;" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Vessel - wise"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 6%" align="center">
                                    <asp:ImageButton ID="btnRetrieve" runat="server" Height="25px" OnClick="btnRetrieve_Click"
                                        ImageUrl="~/Images/SearchButton.png" ToolTip="Search" />
                                </td>
                                <td style="width: 6%" align="center">
                                    <asp:ImageButton ID="btnClearFilter" runat="server" Height="25px" OnClick="btnClearFilter_Click"
                                        ImageUrl="~/Images/filter-delete-icon.png" ToolTip="Clear Filter" />
                                </td>
                                <td style="width: 6%" align="center">
                                    <asp:ImageButton ID="btnExport" ImageUrl="~/Images/XLS.jpg" Height="25px" OnClick="btnExport_Click"
                                        OnClientClick="return OnExportToExcel();" runat="server" ToolTip="Export to Excel" />
                                </td>
                                <td style="width: 6%" align="center">
                                    <asp:ImageButton ID="BtnSendEmail" ImageUrl="~/Images/Send-RFQ.png" Height="25px"
                                        runat="server" OnClick="BtnSendEmail_Click" OnClientClick="return OnSendEmail();"
                                        ToolTip="Send Email To Ship" />
                                </td>
                                <td style="width: 10%">
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="border: 1px solid #cccccc; padding: 2px; margin-top: 2px">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div style="overflow-x: hidden; overflow-y: hidden; border: 0px solid gray;">
                            <asp:GridView ID="gvJobDoneSummary" runat="server" EmptyDataText="NO RECORDS FOUND"
                                CellPadding="4" CellSpacing="0" AutoGenerateColumns="False" 
                                Width="100%" GridLines="Both">
                                 <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                 <RowStyle CssClass="RowStyle-css" />
                                 <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />

                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel">
                                        <HeaderTemplate>
                                            Vessel
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Staff Code">
                                        <HeaderTemplate>
                                            Staff Code &nbsp;&nbsp;
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStaffCode" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Staff_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="55px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Staff Name">
                                        <HeaderTemplate>
                                            Staff Name &nbsp;&nbsp;
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStaffName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Staff_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rank Short Name">
                                        <HeaderTemplate>
                                            Rank &nbsp;&nbsp;
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRankShortName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Rank_Short_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Total Job">
                                        <HeaderTemplate>
                                            Total Jobs
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalJob" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JobCount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Done">
                                        <HeaderTemplate>
                                            Jobs Updated
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobDone" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JOB_DONE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Not Done">
                                        <HeaderTemplate>
                                            Jobs Not Updated
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobNotDone" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JobNotDone") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="65px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="50" OnBindDataItem="BindJobDoneSummary" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
