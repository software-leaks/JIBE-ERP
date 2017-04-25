<%@ Page Title="Inspector Assignment" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InspectorAssignment.aspx.cs" Inherits="Infrastructure_Libraries_InspectorAssignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function Divaddnewlink() {
            document.getElementById("divadd").style.display = "block";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <center>
     
       <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                 <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
               <div class="page-title">
           Inspector Assignment
          </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="Updpnl" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                    
                        <table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 30px">
                            <tr style="background-color: #E6EFFA;">
                                <td align="left" style="width: 15%; background-color: #E6EFFA; font-weight: 700;">
                                    Office User:
                                </td>
                                <td align="left" style="width: 30%">
                                    <asp:TextBox ID="txtUserListSearch" CssClass="txtInput" Width="220px" runat="server" BorderWidth="1" BorderStyle=Solid Height="20px"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE1" runat="server" TargetControlID="txtUserListSearch"
                                        WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                </td>
                                <td style="width: 4%">
                                    <asp:ImageButton ID="btnUserFilter" runat="server" OnClick="btnUserFilter_Click"
                                        ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                </td>
                                <td style="width: 4%">
                                    <asp:ImageButton ID="btnUserRefresh" runat="server" OnClick="btnUserRefresh_Click"
                                        ToolTip="Refresh" ImageUrl="~/Images/Refresh-icon.png" />
                                </td>
                                <td style="background-color: #FFFFFF">
                                </td>
                                <td style="width: 10%; background-color: #E6EFFA; font-weight: 700;">
                                    Inspector :
                                </td>
                                <td align="left" style="width: 30%">
                                    <asp:TextBox ID="txtSuperUserListSearch" CssClass="txtInput" Width="220px" runat="server" BorderWidth="1" BorderStyle=Solid Height="20px"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtSuperUserListSearch"
                                        WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                </td>
                                <td style="width: 4%">
                                    <asp:ImageButton ID="btnSuperUserFilter" runat="server" ImageUrl="~/Images/SearchButton.png"
                                        ToolTip="Search" OnClick="btnSuperUserFilter_Click" />
                                </td>
                                <td style="width: 4%">
                                    <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Refresh-icon.png"
                                        ToolTip="Refresh" OnClick="btnSuperRefresh_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div style="overflow-x: hidden; overflow-y: scroll; border: 1px solid #cccccc; text-align: left;
                                        width: 100%; height: 550px">
                                        <asp:CheckBoxList ID="chkLstUser" runat="server" RepeatLayout="Table" Width="100%">
                                        </asp:CheckBoxList>
                                    </div>
                                </td>
                                <td align="center">
                                    <div>
                                        <asp:Button ID="btnMoveRight" runat="server" Height="28px" OnClick="btnMoveRight_Click" 
                                            CssClass="button-css" Style="color: Blue; text-align: left;padding:5px 5px 5px 5px;cursor:pointer" Text="  &gt;&gt;  "
                                            ToolTip="Add user in inspector list." />
                                    </div>
                                    <div style="padding:5px 5px 5px 5px;cursor:pointer">
                                        <asp:Button ID="btnMoveLeft" runat="server" Height="28px" OnClick="btnMoveLeft_Click" 
                                            CssClass="button-css" Style="color: Blue; text-align: left;padding:5px 5px 5px 5px;cursor:pointer" Text="  &lt;&lt;  "
                                            ToolTip="Remove user from inspector list" />
                                    </div>
                                </td>
                                <td colspan="4">
                                    <div style="overflow-x: hidden; overflow-y: scroll; border: 1px solid #cccccc; text-align: left;
                                        width: 100%; height: 550px;">
                                        <asp:CheckBoxList ID="chkLstSelectedUser" runat="server" Width="100%">
                                        </asp:CheckBoxList>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>

