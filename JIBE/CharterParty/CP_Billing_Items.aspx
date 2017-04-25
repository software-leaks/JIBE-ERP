<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="CP_Billing_Items.aspx.cs"
    Inherits="CP_Billing_Items" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
         <script language="javascript" type="text/javascript">
        function numbersonly(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode
            if (unicode != 8) { //if the key isn't the backspace key (which we should allow)
                if (unicode < 48 || unicode > 57) //if not a number
                    return false //disable key press
            }
        }
        function OpenScreen(ID) {
            var url = 'CP_Billing_Item_Entry.aspx?ItemId=' + ID;
            OpenPopupWindowBtnID('CP', 'Billing Items', url, 'popup', 800, 900, null, null, false, false, true, null, 'btnRefresh');
        }
    </script>
</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 98%;
    height: 100%;">
    <form id="form1" runat="server">
    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
            <table width="100%" cellpadding="2" cellspacing="0" >
                <tr>
                    <td align="center" colspan="2">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltPageHeader" Text ="Billing Items" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                <td width="70%">
                </td>
                <td align="center" width ="30%">
                   <asp:ImageButton ID="btnRefresh" runat="server" ToolTip="Refresh"
                    ImageUrl="~/Images/Refresh-icon.png" onclick="btnRefresh_Click" />&nbsp;&nbsp;
                    <asp:ImageButton ID="ibtnAdd" runat="server" ToolTip="Add/Update Item" OnClientClick='OpenScreen(0);return false;'
                    ImageUrl="~/Images/edit.png"  />
                </td>
                </tr>
                    <tr>
                    
                    <td colspan="2" width="80%">
                    <div class="freezing" style="width:100%">
                        <asp:gridview ID="rgdItems" runat="server" AllowAutomaticInserts="True" Width="98%" GridLines="Both"
                                                    ShowFooter="false" ViewStateMode="Enabled" Skin="Office2007" Style="margin-left: 0px"
                                                     AutoGenerateColumns="true"
                                                     OnRowDataBound="rgdItems_ItemDataBound" 
                                                    AllowMultiRowSelection="True" PageSize="10"  HeaderStyle-HorizontalAlign="Center"
                                                    AlternatingItemStyle-BackColor="#CEE3F6">
                                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css"  HorizontalAlign="Center" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                   <asp:TemplateField  HeaderText="Action"
                                                                Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgDelete" runat="server" OnCommand="onDelete" OnClientClick="return confirm('Are you sure want to delete?')"
                                                                        CommandArgument='<%#Eval("Billing_Item_ID")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                        Height="16px"></asp:ImageButton> &nbsp;
                                                             <asp:ImageButton runat="server" AlternateText="Edit" ImageUrl="~/Images/edit.gif" Visible='<%# uaEditFlag %>'
                                                                ID="cmdEdit"  OnClientClick='<%#"OpenScreen((&#39;" + Eval("Billing_Item_ID") +"&#39;));return false;"%>'  ToolTip="Edit"></asp:ImageButton>
                                                                </ItemTemplate>
                                                              </asp:TemplateField>
                                                        </Columns>
                                               </asp:gridview>
                     </div>
                 
                    </td>
                    </tr>

</table>
</center>
</div>
</form>
</body>
</html>