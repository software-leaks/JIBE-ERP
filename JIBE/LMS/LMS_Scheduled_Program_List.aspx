<%@ Page Title="Scheduled Training/Drill  List" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="LMS_Scheduled_Program_List.aspx.cs" Inherits="LMS_Scheduled_Program_List" %>

<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="auc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            width: 120px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
            white-space: nowrap;
            max-width: 100px;
        }
        .th
        {
            white-space: nowrap;
            max-width: 100px;
        }
        .td
        {
            white-space: nowrap;
            max-width: 100px;
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
            //$('#dvItemList').animate({opacity: 0.2}, 1000);
        }
        function closeDiv(dv) {
            if (dv) {
                $('#' + dv).hide();
            }
            //$('#dvItemList').animate({ opacity: 1 }, 1000);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Scheduled Training/Drill List
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
                        <td class="tdh">
                            Fleet :
                        </td>
                        <td class="tdd">
                            <ucDDL:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                Height="150" Width="160" />
                        </td>
                        <td class="tdh" style="width: 200px">
                            Training/Drill Name / Description :
                        </td>
                        <td class="tdd" style="text-align: left">
                            <asp:TextBox ID="txtProgramName" runat="server" Width="160"></asp:TextBox>
                        </td>
                        <td class="tdh">
                            Category :
                        </td>
                        <td class="tdd">
                            <asp:DropDownList ID="ddlProgramCategory" runat="server" Font-Size="11px" Height="20px"
                                Visible="true" Width="160px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                Width="80px" CssClass="btnCSS" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdh">
                            Vessel :
                        </td>
                        <td class="tdd">
                            <ucDDL:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                                Height="200" Width="160" />
                        </td>
                        <td class="tdh">
                            Due Date From :
                        </td>
                        <td class="tdd">
                            <asp:TextBox ID="txtDuedateFrom" runat="server" CssClass="control-edit valid_regex_alphanumeric"
                                Width="160"></asp:TextBox>
                            <tlk4:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtDuedateFrom"
                                Format="dd/MM/yyyy">
                            </tlk4:CalendarExtender>
                        </td>
                        <td class="tdh">
                            Due Date To :
                        </td>
                        <td class="tdd">
                            <asp:TextBox ID="txtDuedateTo" runat="server" CssClass="control-edit valid_regex_alphanumeric"
                                Width="155"></asp:TextBox>
                            <tlk4:CalendarExtender ID="calTo" runat="server" TargetControlID="txtDuedateTo" Format="dd/MM/yyyy">
                            </tlk4:CalendarExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnClearFilter" runat="server" OnClick="btnClearFilter_Click" Text="Clear Filter"
                                Width="80px" CssClass="btnCSS" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="vertical-align: top;">
                            <asp:GridView ID="gvProgram_ListDetails" AutoGenerateColumns="false" runat="server"
                                OnSorting="gvProgram_ListDetails_Sorting" Width="100%" CssClass="gridmain-css"
                                AllowSorting="true" CellPadding="4" CellSpacing="0" GridLines="None" OnRowDataBound="gvProgram_ListDetails_RowDataBound"
                                EmptyDataText="No Data Found!">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" Wrap="false" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField SortExpression="">
                                        <ItemTemplate>
                                            <asp:Image ID="imgAdhoc" ImageUrl="~/Images/AdhocDrill.png" runat="server" Visible='<%#Eval("Office_Id").ToString()=="0"?true:false %>'
                                                ToolTip="Ad-hoc Training" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblVessel_NameHead" runat="server" CommandName="Sort" CommandArgument="Vessel_Name"
                                                ForeColor="Black">Vessel_Name&nbsp;</asp:LinkButton>
                                            <img id="Vessel_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVessel_NameView" runat="server" Text='<%#Eval("Vessel_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Training/Drill Name">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblPROGRAM_NameHead" runat="server" CommandName="Sort" CommandArgument="PROGRAM_Name"
                                                ForeColor="Black">PROGRAM_Name&nbsp;</asp:LinkButton>
                                            <img id="PROGRAM_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlProgramDetails" Text='<%#Eval("PROGRAM_Name")%>' runat="server"
                                                NavigateUrl='<%# "~/LMS/LMS_Program_Details.aspx?Program_ID="+Eval("Program_ID").ToString()%>'
                                                Target="_blank"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Training/Drill Description" ItemStyle-Width="400px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProgramDescCon" Text='<%#Eval("PROGRAM_DESCRIPTION").ToString().Length>=100?Eval("PROGRAM_DESCRIPTION").ToString().Substring(0, 98)+".....":Eval("PROGRAM_DESCRIPTION").ToString()  %>'
                                                runat="server"  ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblSCHEDULED_DATEHead" runat="server" CommandName="Sort" CommandArgument="SCHEDULED_DATE"
                                                ForeColor="Black">Schedule Date&nbsp;</asp:LinkButton>
                                            <img id="SCHEDULED_DATE" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSCHEDULED_DATEView" runat="server" Text='<%#Eval("SCHEDULED_DATE", "{0:dd/MMM/yyyy}") %>'> </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblTRG_END_DATEHead" runat="server" CommandName="Sort" CommandArgument="TRG_END_DATE"
                                                ForeColor="Black">Done Date&nbsp;</asp:LinkButton>
                                            <img id="TRG_END_DATE" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTRG_END_DATEView" runat="server" Text='<%#Eval("TRG_END_DATE", "{0:dd/MMM/yyyy}") %>'> </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Duration" HeaderText="Duration" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Attaindees" HeaderText="Attendees" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlProgramReport" Text='View Detail' runat="server" NavigateUrl='<%# "~/LMS/LMS_Program_Details_Report.aspx?Program_ID="+Eval("Program_ID").ToString()+ "&SCHEDULE_ID="+Eval("SCHEDULE_ID").ToString() + "&Vessel_Short_Name="+Eval("Vessel_Name").ToString()+"&Office_Id="+Eval("Office_Id").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()%>'
                                                Target="_blank"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <auc:CustomPager ID="ucCustomPagerProgram_List" OnBindDataItem="BindProgramItemInGrid"
                                AlwaysGetRecordsCount="true" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="divMessage" align="center">
            <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
        </div>
        <br />
    </div>
</asp:Content>
