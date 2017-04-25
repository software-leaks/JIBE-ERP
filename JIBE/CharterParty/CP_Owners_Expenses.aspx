<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="CP_Owners_Expenses.aspx.cs"
    Inherits="CP_Owners_Expenses" %>

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
    <script type="text/javascript">


        function OpenScreen(Inv_ID, Supply_ID) {
            var url = '../po_Log/Compare_PO_Invoice.asp?Invoice_ID=' + Inv_ID + '&Supply_ID=' + Supply_ID;
            window.open(url, "_blank");
        }
    
    </script>
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
                            <asp:Literal ID="ltPageHeader" Text ="Owner Expenses" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>
                    <tr>
                    <td colspan="7" align = "center" style="color: #FF0000;" >
                    
                    <asp:Button ID="btnHideShow" runat="server" Text="Show All" 
                            onclick="btnHideShow_Click"/>
                    <asp:Literal ID="ltmessage" runat = "server"> </asp:Literal>
                    </td>
                    </tr>
                    <tr>
                         <td colspan="7" align="center">
                            <asp:GridView ID="gvOwnerExpenses" runat="server" AutoGenerateColumns="False" 
                                GridLines="Both" Width="98%" DataKeyNames="Invoice_ID" 
                                 OnPageIndexChanging="gvOwnerExpenses_PageIndexChanging" 
                                 onrowdatabound="gvOwnerExpenses_RowDataBound" >
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
                                        <ItemStyle CssClass="PMSGridItemStyle-css" ForeColor="Blue" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="PO Reference">
                                    <ItemTemplate>
                                         <asp:Label ID="lblAmt" Text='<%# Eval("PO_Ref") %>'  runat="server" ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Wrap="true" Width="10%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Inv. Ref">
                                    <ItemTemplate>
                                         <asp:Label ID="lblInvRef" Text='<%# Eval("Invoice_Reference") %>'  runat="server" ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Wrap="true" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cur">
                                    <ItemTemplate>
                                         <asp:Label ID="lblInvCur" Text='<%# Eval("Invoice_Currency") %>'  runat="server" ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Wrap="true" Width="5%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Invoice Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvAmount" runat="server" Text='<%# Eval("Invoice_Amount") %>' 
                                                Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                     <asp:Label ID="lblInvStatus" Text='<%# Eval("Invoice_Status") %>' runat="server" Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Allocated">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAllocated" runat="server" Text='<%# Eval("Allocated_Amount") %>'  Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Right" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Available">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAvailable" runat="server" Text='<%# Eval("Available_Amount") %>'  Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Right" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>

  
                                     <asp:TemplateField HeaderText="Journal Ref.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJournalRef" runat="server" Text='<%# Eval("Hire_Matching_Journal_ID") %>'  Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:HiddenField ID ="hdnapprover_UserID" Value='<%# Eval("Approver_UserID") %>'  runat="server" />
                                            <asp:HiddenField ID ="hdnIsApprover" Value='<%# Eval("IsApprover") %>'  runat="server" />
                                            <asp:HiddenField ID ="hdnJournalId" Value='<%# Eval("Journal_ID") %>'  runat="server" />
                                           <asp:ImageButton ID="ibtnUnmatch" style="border: 0; width: 14px; height: 14px" Text="Unmatch" Visible="false" OnCommand="ibtnUnmatch_Click"
                                            CommandArgument='<%#Eval("[Invoice_ID]")%>' ForeColor="Black" ToolTip="Unmatch"
                                            ImageUrl="../Images/tasklist.gif" runat="server" />&nbsp;
                                            <asp:ImageButton ID="ibtnApprove" runat="server" OnCommand="ibtnApprove_Click" Visible="false"
                                            CommandArgument='<%#Eval("[Invoice_ID]")%>' ImageUrl="~/Images/asl_eval.png" ToolTip="Approve Invoice"  OnClientClick="javascript:return confirm('Confirm approval?')" />&nbsp;

                                             <asp:ImageButton ID="ibtnUnApprove" runat="server" OnCommand="ibtnUnApprove_Click" Visible="false"
                                            CommandArgument='<%#Eval("[Invoice_ID]")%>' ImageUrl="~/Images/docreject.png" ToolTip="Un-Approve" OnClientClick="javascript:return confirm('Confirm Un approve?')" />

                                              <asp:ImageButton ID="ibtnCompare" runat="server" Visible="false"
                                            ImageUrl="~/Images/compare.gif" ToolTip="Compare Invoice"  OnClientClick='<%#"OpenScreen((&#39;" + Eval("[Invoice_ID]") +"&#39;),(&#39;"+ Eval("[Supply_ID]") + "&#39;));return false;"%>' />

                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
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