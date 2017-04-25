<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditInterviewSchedule.aspx.cs"
    Inherits="Crew_EditInterviewSchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/CrewDetails_DataHandler.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scr1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlEdit_InterviewPlanning" runat="server" Visible="true">
                    <asp:FormView ID="frmInterviewDetails" runat="server" OnItemUpdating="frmInterviewDetails_Updating">
                        <EditItemTemplate>
                            <table style="width: 100%" cellpadding="4">
                                
                                <tr>
                                    <td>
                                        Interview Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPlanDate" runat="server" Width="150px" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("InterviewPlanDate"))) %>' ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPlanDate"  >
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Interview Time
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPlanH" runat="server" Width="50px" AppendDataBoundItems="true"
                                            Text='<%# Bind("InterviewPlanH")%>'>
                                            <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12" Text="12"></asp:ListItem>
                                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                        </asp:DropDownList>
                                        H &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlPlanM" runat="server" Width="50px" AppendDataBoundItems="true"
                                            >
                                            <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                            <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                        </asp:DropDownList>
                                        M
                                    </td>
                                </tr>
                                <tr>
                                    <td>Time Zone</td>
                                    <td><asp:DropDownList ID="ddlTimeZone" runat="server" Width="250px"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>
                                        Interviewer
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPlanInterviewer" runat="server" DataSourceID="ObjectDataSource4"
                                             DataTextField="USER_NAME" DataValueField="USERID"
                                            Width="154px" AppendDataBoundItems="True" AutoPostBack="True">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="Get_UserList"
                                            TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="0" Name="CompanyID" SessionField="UserCompanyID"
                                                    Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Interview Rank
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlInterviewRank" runat="server" DataSourceID="objdsInterviewRank" Text='<%#Bind("InterviewRank") %>'
                                            DataTextField="Rank_Short_Name" DataValueField="id" Width="154px" AppendDataBoundItems="true"  AutoPostBack="true" OnSelectedIndexChanged="ddlInterviewRank_SelectedIndexChanged">
                                         </asp:DropDownList>
                                        <asp:ObjectDataSource ID="objdsInterviewRank" runat="server" SelectMethod="Get_RankList"
                                            TypeName="SMS.Business.Crew.BLL_Crew_Admin"></asp:ObjectDataSource>
                                    </td>
                                </tr>
                                 <tr>
                                    <td >
                                       <asp:Label ID="lblInterviewSheet" Text="Interview Sheet" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlInterviewSheet" runat="server"  Width="180px" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <asp:Button ID="UpdateButton" CommandName="Update" ValidationGroup="ValidEntry" runat="server"
                                            Text=" Update " BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                            Height="24px" Width="100px" BackColor="#81DAF5" />
                                        <asp:Button ID="CancelUpdate" OnClientClick="parent.hideModal('dvPopupFrame'); return false;" runat="server" CausesValidation="false"
                                            Text=" Cancel " BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                            Height="24px" Width="100px" BackColor="#81DAF5" />
                                    </td>
                                </tr>
                                
                            </table>
                        </EditItemTemplate>
                    </asp:FormView>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
