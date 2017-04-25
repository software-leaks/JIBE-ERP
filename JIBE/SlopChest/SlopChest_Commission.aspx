<%@ Page Title="SlopChest Commision" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SlopChest_Commission.aspx.cs" Inherits="Purchase_SlopChest_Commission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
<link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
 <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
 <script type="text/javascript" language="javascript">
     function DecimalsOnly(e) {
         var evt = (e) ? e : window.event;
         var key = (evt.keyCode) ? evt.keyCode : evt.which;
         if (key != null) {
             key = parseInt(key, 10);
             if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                 if (!IsUserFriendlyChar(key, "Decimals")) {
                     return false;
                 }
             }
             else {
                 if (evt.shiftKey) {
                     return false;
                 }
             }
         }
         return true;
     }

     function IsUserFriendlyChar(val, step) {
         // Backspace, Tab, Enter, Insert, and Delete
         if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
             return true;
         }
         // Ctrl, Alt, CapsLock, Home, End, and Arrows
         if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
             return true;
         }
         if (step == "Decimals") {
             if (val == 190 || val == 110) {
                 return true;
             }
         }
         // The rest
         return false;
     }
  

     function ValidateTextBox() {
         var count = 0;
         $('#txtCommision').each(function (index, item) {
             if ($(this).val() != "") {
                 count = 1;
             }
         }, 0);

         if (count == 0) {
             alert("Enter atleast one");
             return false;
         }
         else {
             return true; 
        }
     }
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<center>
 <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 1000px;
            height: 100%;">
            <div style="font-size: 20px; background-color: #5588BB; width: 1000px; color: White;
                text-align: center;">
                <b>SlopChest Commision </b>
            </div>
            <br />
<asp:GridView ID="grdSlopChestCommision" Width="100%" ClientIDMode="Static" runat="server" AutoGenerateColumns="false" CellPadding="2" CellSpacing="2">
<HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <PagerStyle CssClass="PagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" /><Columns>
<asp:TemplateField HeaderText="SlopChest Items" HeaderStyle-HorizontalAlign="Left">
<ItemTemplate>
<asp:Label ID="lblItemID" runat="server" ClientIDMode="Static" Text='<%#Eval("Item_Ref_Code")%>'  style="display:none"></asp:Label>
<asp:Label ID="lblItem" runat="server" ClientIDMode="Static" Text='<%#Eval("Short_Description") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Commision" ItemStyle-HorizontalAlign="Center">
<ItemTemplate>
<asp:TextBox ID="txtCommision"  runat="server" ClientIDMode="Static" onkeydown="javascript:return DecimalsOnly(event);"></asp:TextBox>
</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</div>
<br />
<table><tr><td><asp:Button ID="btnSave" Text="Save" runat="server" ClientIDMode="Static" 
        onclick="btnSave_Click" OnClientClick ="javascript:return ValidateTextBox();" /> </td> 
    <td><asp:Button ID="btnCancel" Text="Cancel" runat="server" ClientIDMode="Static" 
        onclick="btnCancel_Click" /> </td></tr></table>


</center>
</asp:Content>

