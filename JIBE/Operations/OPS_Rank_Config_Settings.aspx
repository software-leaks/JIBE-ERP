<%@ Page Title="Report-Rank Config." Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OPS_Rank_Config_Settings.aspx.cs" Inherits="Operations_OPS_Rank_Config_Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../styles/Purchase.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script type="text/javascript" language="javascript">
    $('#btnAddRank').click(function () {
        if ($("#ddlMasterRank").val() != '-Select-' && $("#ddlCERank").val() != '-Select-') {
            return true;
        }
        else 
        {
            if ($("#ddlMasterRank").val() == '-Select-') {
                alert('Select Master Rank');
                $("#ddlMasterRank").focus();
            }
            if ($("#ddlCERank").val() == '-Select-') {
                alert('Select Chief Engineer Rank');
                $("#ddlCERank").focus();
            }
        }
    });
</script>
<center>
   <div class="page-title">
Rank Configuration for Voyage Report            </div>
<br />
<div style="height: 350px; width: 900px; color: Black;">
<table width="70%" align="center">

<tr>
<td align="center">
<fieldset style="text-align: left; margin:0px; padding:0px; width: 100%;">
                                        <legend style="color:Black">Assign Rank to Voyage Report Creation</legend>
                                        <asp:UpdatePanel ID="upd" runat="server">
                                        <ContentTemplate>
                                        <table align="center" cellpadding="5" cellspacing="1" width="100%">
                                            <tr>
                                                <td width="60px">
                                                    Master  &nbsp;<span style="color:Red">*</span>
                                                </td>
                                                <td width="200px">
                                                    <asp:DropDownList ID="ddlMasterRank"  ClientIDMode="Static"  Width="100%" runat="server" 
                                                        CssClass="control-edit required" >
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="99px">
                                                    Chief Engineer &nbsp;<span style="color:Red">*</span>
                                                </td>
                                                <td width="200px">
                                                    <asp:DropDownList ID="ddlCERank"  ClientIDMode="Static"  Width="100%" runat="server" 
                                                        CssClass="control-edit required" >
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center">
                                                    <asp:Button ID="btnAddRank" ClientIDMode="Static" runat="server" Text="Save" 
                                                        ToolTip="Save" OnClick="btnAddRank_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </fieldset></td>
</tr>
</table>
</div>
</center>
</asp:Content>

