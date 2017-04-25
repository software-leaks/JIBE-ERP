<%@ Page Title="Evaluation Schedule" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="EvaluationSchedules.aspx.cs" Inherits="CrewEvaluation_EvaluationSchedules" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //FindAndReplace();
        });

        function FindAndReplace(Find_ , Replace_) {
            $("*").each(function () {
                if ($(this).children().length == 0) {
                    $(this).text($(this).text().replace(Find_, Replace_));
                }
            });
        }      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
    <div class="page-title">
        <asp:Label ID="lblPageTitle" runat="server" Text="Crew Evaluation Schedule"></asp:Label>
    </div>
    <div class="page-content-main">
        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
            <ContentTemplate>
                <div class="page-content-filter" style="text-align: center; border: 1px solid #cccccc;">
                    <table border="0">
                        <tr>
                            <td>
                                Fleet
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFleet" runat="server" Width="156px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Vessel
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVessel" runat="server" Width="156px" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Rank
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRank" runat="server" DataValueField="id" DataTextField="Rank_Short_Name"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlRank_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left">
                                Search:
                            </td>
                            <td>
                                <asp:TextBox ID="txtfilter" runat="server" AutoPostBack="true"
                                    OnTextChanged="txtfilter_TextChanged"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                    WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                            </td>
                            <td style="width: 200px">
                                <asp:Button ID="btnClear" OnClick="btnClearFilter_Click" runat="server" Text="Clear Filter"
                                    CssClass="button-css"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="text-align: center" class="page-content">
                    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="font-size: 14px; font-weight: bold; text-align: left;">
                                            <asp:Label ID="lblSelection" runat="server"></asp:Label>
                                        </td>
                                        <td runat="server" id="tdNavigator">
                                            <div style="text-align: right;" >
                                                <asp:HiddenField ID="hdnStartMonth" runat="server" Value="" />
                                                <asp:Button ID="MovePrev" runat="server" Text=" < " OnClick="MovePrev_Click" />
                                                <asp:Button ID="MoveCurrent" runat="server" Text=" Current " OnClick="MoveCurrent_Click" />
                                                <asp:Button ID="MoveNext" runat="server" Text=" > " OnClick="MoveNext_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView_Evaluation" runat="server" AllowSorting="false" AutoGenerateColumns="true"
                                    CaptionAlign="Bottom" CellPadding="4" DataKeyNames="ID" EmptyDataText="No Record Found"
                                    BorderStyle="Solid" ForeColor="#333333" GridLines="Horizontal" OnSorted="GridView_Evaluation_Sorted"
                                    OnSorting="GridView_Evaluation_Sorting" OnRowDataBound="GridView_Evaluation_RowDataBound"
                                    Width="100%" BorderColor="#CCCCCC" CssClass="grid">
                                   <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                   <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <Columns>
                                    </Columns>
                                    <EditRowStyle BackColor="#58FA82" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF6FC" ForeColor="#333333" CssClass="grid-col" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
