<%@ Page Title="Rank Configuration" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CrewRankConfiguration.aspx.cs" Inherits="Crew_CrewMR_HO_Checklist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">
  <div class="page-title">
            Rank Configuration
          </div>
<div align="center">
<table>
<tr>
<td>
<fieldset>
<legend>Master's Review</legend>
<table>
<tr>

<td></td>
<td>
<div style="height:600px;overflow:auto;">
   <asp:CheckBoxList ID="chkRank1" runat="server" RepeatDirection="Vertical" Width="300px"  ></asp:CheckBoxList>
   </div>
    </td>
</tr>
</table>
</fieldset>
</td>
<td>
<fieldset>
<legend>Handover CheckList</legend>
<table>
<tr>

<td>
</td>
<td>
<div style="height:600px;overflow:auto;">
<asp:CheckBoxList ID="chkRank2" runat="server"  Width="300px"></asp:CheckBoxList>
    </div>
    </td>
</tr>
</table>
</fieldset>
</td>
</tr>
</table>



<asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
</div>
</div>
</asp:Content>

