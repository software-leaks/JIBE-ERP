<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewSettings.aspx.cs" Inherits="Infrastructure_Libraries_CrewInterviewSettings"
    MasterPageFile="~/Site.master" Title="Crew Settings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <center style="height: 850px">
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: auto;
            height: 100%;">
            <%--<div class="page-title">
                 Interview Setting
          </div>--%>
            <div style="margin-left: auto; height: auto; width: auto; margin-right: auto; text-align: center;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table width="40%">
                            <tr valign="top">
                                <td>
                                    <fieldset style="text-align: left; margin: 0px; padding: 2px; width: 250px">
                                        <legend>Interview Setting</legend>
                                        <table cellpadding="5" cellspacing="1" width="100%">
                                            <tr>
                                                <td align="left">
                                                    Interview Mandatory
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkInterview_Mandatory" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    Check Rejected Interview Before Approval
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkRejectedCheck" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnSave" runat="server" Text=" Save " OnClick="btnSave_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                                <td>
                                    <fieldset style="text-align: left; margin: 0px; padding: 2px; width: 270px">
                                        <legend>Wages Setting</legend>
                                        <table cellpadding="5" cellspacing="1" width="100%">
                                            <tr>
                                                <td align="left">
                                                    Rank
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkRank" runat="server" Checked="true" Enabled="false"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    Vessel Flag
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkVesselFlag" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    Nationality
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkNationality" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    Rank Scale
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkRankScale" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnSaveWages" runat="server" Text=" Save " OnClick="btnSaveWages_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <fieldset style="text-align: left; margin: 0px; padding: 2px; width: 250px">
                                        <legend>Reference Check Setting</legend>
                                        <table cellpadding="5" cellspacing="1" width="100%">
                                            <tr>
                                                <td>
                                                    <div style="width: 100%; height: 560px; overflow: auto;">
                                                        <asp:GridView ID="gvRankList" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                                                            AllowSorting="True" CaptionAlign="Bottom" CellPadding="4" Font-Size="14px" GridLines="None"
                                                            Width="100%" CssClass="gridmain-css" Height="600px">
                                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                            <RowStyle CssClass="RowStyle-css" />
                                                            <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select Rank">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelected" Checked='<%# Eval("Mandatory").ToString()=="1"?true:false%>'
                                                                            runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ranks">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRankId" runat="server" Text='<%# Eval("Id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle CssClass="crew-interview-grid-alternaterow" />
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnSaveRank" runat="server" Text=" Save " OnClick="btnSaveRank_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                                <td valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <fieldset style="text-align: left; margin: 0px; padding: 2px; width: 270px;">
                                                    <legend>Document Setting</legend>
                                                    <table cellpadding="0" cellspacing="5" width="100%">
                                                        <tr>
                                                            <td align="left" style="width: 350px">
                                                                Rank
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkRank1" runat="server" Checked="true" Enabled="false"></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="border: 5px">
                                                            <td align="left" style="width: 350px">
                                                                Consider
                                                            </td>
                                                            <td align="left">
                                                                <asp:RadioButtonList ID="rdbConsider" CssClass="txtInput" Width="145px" runat="server"
                                                                    Style="margin-left: -5px;" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="Vessel">Vessel</asp:ListItem>
                                                                    <asp:ListItem Value="VesselFlag">Vessel Flag</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 350px">
                                                                STCW Deck
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkSTCWDeck" runat="server"></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 350px">
                                                                STCW Engine
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkSTCWEngine" runat="server"></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="center">
                                                                <asp:Button ID="btnSaveDocument" runat="server" Text=" Save " OnClick="btnSaveDocument_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <fieldset style="text-align: left; margin: 0px; padding: 2px; width: 270px;">
                                                    <legend>Seniority Reset</legend>
                                                    <table cellpadding="5" cellspacing="1" width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                Reset Seniority Automatic
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkSeniorityReset" runat="server" AutoPostBack="true" OnCheckedChanged="chkSeniorityReset_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Reset Time Duration(Year)
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlSeniorityYear" runat="server" Style="width: 90px">
                                                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                                    <asp:ListItem Value="2">2</asp:ListItem>
                                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                                    <asp:ListItem Value="4">4</asp:ListItem>
                                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                                    <asp:ListItem Value="6">6</asp:ListItem>
                                                                    <asp:ListItem Value="7">7</asp:ListItem>
                                                                    <asp:ListItem Value="8">8</asp:ListItem>
                                                                    <asp:ListItem Value="9">9</asp:ListItem>
                                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="center">
                                                                <asp:Button ID="btnSaveSeniorityResetYear" runat="server" Text=" Save " OnClick="btnSaveSeniorityResetYear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <fieldset style="text-align: left; margin: 0px; padding: 2px; width: 270px;">
                                                    <legend>Mandatory Settings</legend>
                                                    <table cellpadding="5" cellspacing="1" width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                NOK
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkNOK" runat="server"></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Crew Photograph
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkCrewPhoto" runat="server"></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Bank Account Details
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkBankAccDetails" runat="server"></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Seniority
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkSeniority" runat="server"></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Leave Wages Withhold
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkLeaveWithhold" runat="server"></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                Default Office User Rank
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlRank" runat="server" DataTextField="Rank_Short_Name" DataValueField="id"
                                                                    Width="100px" AppendDataBoundItems="true" CssClass="control-edit required">
                                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="center">
                                                                <asp:Button ID="btnSaveMandatorySettings" runat="server" Text=" Save " OnClick="btnSaveMandatorySettings_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <fieldset style="text-align: left; margin: 0px; padding: 2px; width: 270px;">
                                                    <legend>Evaluation Setting</legend>
                                                    <table cellpadding="5" cellspacing="1" width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                Evaluated signature mandatory
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkEvalSign" runat="server"></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td colspan="2" align="center">
                                                                <asp:Button ID="btnSaveEvaluatedSignature" runat="server" Text=" Save " OnClick="btnSaveEvaluatedSignature_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
