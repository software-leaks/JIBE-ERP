<%@ Page Title="Crew Approval" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CrewApproval.aspx.cs" Inherits="Crew_CrewApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList" TagPrefix="ucDDL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../styles/premiere_blue/style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
   
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page-title">
                <asp:Label ID="lblPageTitle" runat="server" Text="Crew Approval"></asp:Label>
            </div>
            <div class="page-content-main">
                <div class="error-message" style="text-align:center;color:Red;">
                    <asp:Label ID="lblMessage"  style="color:Red;" runat="server"></asp:Label>
                </div>
                <center>
                    <div style="text-align: left; width: 70%;">
                        <table cellpadding="4" cellspacing="4" style="border: 1px solid #dcdcdc; margin: 5px;
                            border-collapse: collapse; width: 100%">
                            <tr style="background-color: #cfdfef">
                                <td>
                                    Candidate
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCrewName" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    Manning Office
                                </td>
                                <td>
                                    <asp:TextBox ID="txtManningOffice" runat="server" Enabled="false" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="background-color: #cfdfef">
                                <td>
                                    Approver
                                </td>
                                <td>
                                    <asp:TextBox ID="txtApprover" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    Approval date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtApprovalDate" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="background-color: #cfdfef">
                                <td>
                                    Applied Rank
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAppliedRank" runat="server" Enabled="false" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    Approved Rank
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlApprovedRank" runat="server" DataSourceID="ObjectDataSource3"
                                        DataTextField="Rank_Short_Name" DataValueField="id" Width="154px" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="Get_RankList"
                                        TypeName="SMS.Business.Crew.BLL_Crew_Admin"></asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr style="background-color: #cfdfef">
                            <td >
                                Vessel Type :
                            </td>
                            <td align="left">
                                <ucDDL:ucCustomDropDownList ID="ddlVesselType" runat="server" UseInHeader="False" 
                                    Height="150" Width="160" />
                            </td>
                            <td></td><td></td>
                            </tr>
                       
                            <tr>
                                <td>
                                    Selected Status
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdoSelected" runat="server" RepeatDirection="Horizontal"
                                        CellPadding="10">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtOther" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Remark
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtResultText" runat="server" TextMode="MultiLine" Width="350px"
                                        Height="50px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Button ID="btnSaveInterviewResult" Text=" Save " runat="server" OnClick="btnSaveInterviewResult_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text=" Close " OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </center>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
