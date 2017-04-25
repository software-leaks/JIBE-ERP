<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAsyncPager.ascx.cs" Inherits="UserControl_ucAsyncPager" %>
<style type="text/css">
    .Paging-Custom
    {
        text-align: center;
        margin: 2px;
        padding: 2px 5px 2px 5px;
        text-decoration: none;
        font-size: 12px;
        position: relative;
        font-family: Verdana;
    }
    .Paging-Selected
    {
        background-color: #99ccff;
        border: 1px solid #58ACFA;
    }
    
    .tdpager
    {
        padding: 2px 0px 2px 0px;
        text-align: left;
        border: 1px solid #A9D0F5;
    }
    .Paging-Custom:hover
    {
        background-color: #CECEF6;
        cursor:default;
    }
</style>
<table id="tblPager" align="left" width="100%"  cellpadding="0" cellspacing="0">
   
    <tr>
    <td></td>
        <td class="tdpager" style="background-color: #F6CEE3; background: url(../Images/bg.png) left -10px repeat-x;
            color: Black;white-space:nowrap">
            <asp:ImageButton ID="first" runat="server"  ImageAlign="AbsMiddle" style="display:inline"
                ImageUrl="~/Images/Movefirst.png" Height="20px" CausesValidation="false"></asp:ImageButton>
            <asp:ImageButton ID="prev" runat="server"  ImageAlign="AbsMiddle" style="display:inline"
                ImageUrl="~/Images/Moveprev.png" Height="20px" CausesValidation="false"></asp:ImageButton>
            <asp:Label ID="lblPaging" runat="server"></asp:Label>
            <asp:ImageButton ID="next" runat="server"  CausesValidation="false" style="display:inline"
                ImageAlign="AbsMiddle" ImageUrl="~/Images/Movenext.png" Height="20px"></asp:ImageButton>
            <asp:ImageButton ID="last" runat="server"  CausesValidation="false" style="display:inline"
                ImageAlign="AbsMiddle" ImageUrl="~/Images/Movelast.png" Height="20px"></asp:ImageButton>
            <asp:DropDownList ID="ddlPageSize" runat="server"  Font-Size="12px"
               >
                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                <asp:ListItem Text="30" Value="30"> </asp:ListItem>
                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                <asp:ListItem Text="100" Value="100"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblTotalPages" ForeColor="Black" Font-Size="11px" Font-Names="verdana"
                runat="server"></asp:Label>
        </td>
    </tr>
</table>
<div style="display:none">
    <asp:HiddenField ID="hdfCurrentPageSection" Value="1" runat="server" />
    <asp:HiddenField ID="hdfcountTotalRec" Value="0" runat="server" />
    <asp:HiddenField ID="hdfPageSize" Value="15" runat="server" />
    <asp:HiddenField ID="hdfPageIndex" Value="1" runat="server" />
    <asp:HiddenField ID="hdfisCountRecord" Value="1" runat="server" />
    <asp:HiddenField ID="hdfBindMethodname"  runat="server" />
   
</div>
