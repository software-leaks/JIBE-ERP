<%@ Page Title="Copy PMS data" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Copy_PMS_Data.aspx.cs" Inherits="Technical_Copy_PMS_Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Copy PMS Data
    </div>
    <div class="page-content">
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
        <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>
                <table cellpadding="3">
                    <tr>
                        <td style="background-color: #80DFFF; font-weight: bold">
                            Copy PMS data from
                        </td>
                        <td>
                        </td>
                        <td style="background-color: #80DFFF; font-weight: bold">
                            Copy PMS data to
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="lstCopyFromVessel" runat="server" DataTextField="Vessel_name" DataValueField="Vessel_id"
                                Height="600px" Width="400px" SelectionMode="Single"></asp:ListBox>
                        </td>
                        <td>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                        <td>
                            <asp:ListBox ID="lstCopyToVessel" runat="server" DataTextField="Vessel_name" DataValueField="Vessel_id"
                                Height="600px" Width="400px" SelectionMode="Single"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnCopyData" runat="server" Text="Copy PMS data" OnClick="btnCopyData_Click"
                                OnClientClick="javascript:var a =confirm('Are you sure to copy data ?'); if(a) return true; else return false;" />
                            <br />
                            <asp:Label ID="lblmsg" runat="server" Font-Size="14px" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
