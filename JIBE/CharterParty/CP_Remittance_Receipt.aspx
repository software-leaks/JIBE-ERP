<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="CP_Remittance_Receipt.aspx.cs"
    Inherits="CP_Remittance_Receipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;
    height: 100%;">
    <form id="form1" runat="server">
    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
            <table width="100%" cellpadding="2" cellspacing="0" >
                <tr>
                    <td align="center" colspan="7">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltPageHeader" Text ="Remittance Receipt" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>
                    <tr>
                    <td colspan="7" align = "center" style="color: #FF0000;" >
                    
                    <asp:Literal ID="ltmessage" runat = "server"> </asp:Literal>
                    </td>
                    </tr>
                    <tr>
                         <td colspan="7" align="center">
                            <asp:GridView ID="gvRemittance" runat="server" AutoGenerateColumns="False" 
                                 AllowPaging="true" PageSize="10"
                                GridLines="Both" Width="98%" DataKeyNames="Remittance_ID" 
                                 onrowdatabound="gvRemittance_RowDataBound" 
                                 onpageindexchanging="gvRemittance_PageIndexChanging">
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Received Date">
                                           <ItemTemplate>
                                            <asp:Label ID="lblReceivedDt" runat="server" 
                                                Text='<%# Eval("Received_Date") %>' 
                                                Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" ForeColor="Blue" HorizontalAlign="Center" Width="15%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Amount(USD)">
                                    <ItemTemplate>
                                         <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("Amount_Received") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Wrap="true" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Allocated">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAllocated" runat="server" 
                                                Text='<%# Eval("Allocated_Amount") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Available">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAvailable" runat="server" 
                                                Text='<%# Eval("Available") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Remittance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemittance" runat="server" 
                                                Text='<%# Eval("Remittance_Remarks") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="left" Width="20%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Update">
                                        <ItemTemplate >

                                                  <table cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td width="3%">
                                                            <asp:Literal ID="ltUnmatched" Text="Unmatched" Visible = "false" runat="server"></asp:Literal>
                                                            <asp:ImageButton ID="ibtnMarkRead" style="border: 0; width: 14px; height: 14px" Text="Unmatch" OnCommand="ibtnMarkRead_Click"
                                                              CommandArgument='<%#Eval("[Remittance_ID]")%>' ForeColor="Black" ToolTip="Update Unmatched"  OnClientClick="return confirm('Are you sure want to unmatch?')"
                                                                ImageUrl="../Images/tasklist.gif" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>


                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="False" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>

</table>
</center>
</div>
</form>
</body>
</html>