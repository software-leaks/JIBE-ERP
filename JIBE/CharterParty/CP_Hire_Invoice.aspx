<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="CP_Hire_Invoice.aspx.cs"
    Inherits="CP_Hire_Invoice" %>

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
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>

         <script language="javascript" type="text/javascript">
             function OpenScreen(ID, Inv_ID) {
                 var CPID = <%=CPID %>
                 var url = 'CP_Hire_Invoice_Entry.aspx?CPID=' + CPID + '&Inv_ID=' + Inv_ID;
                 OpenPopupWindowBtnID('CP', 'Hire Invoice Detail', url, 'popup', 650, 1000, null, null, false, false, true, null, 'btnSearch');
             }
            function OpenInvPrep(ID) {
                 var CPID = <%=CPID %>
                 var url = 'CP_Hire_Invoice_Prep.aspx?CPID=' + CPID ;
                 OpenPopupWindowBtnID('CP', 'Invoice Preparation', url, 'popup', 600, 1200, null, null, false, false, true, null, 'btnSearch');
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
                    <td align="center" colspan="5">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltPageHeader" Text ="Hire Invoice" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                <td width="10%" align="right">
                 <asp:Literal ID="ltInvoiceStatus" Text="Invoice Status :" runat="server"></asp:Literal>
                </td>
                <td align="left" width="40%" >
                <asp:UpdatePanel ID="updStatus" runat="server">
                    <ContentTemplate>
                  <asp:CheckBoxList ID="chkStatusList"  RepeatDirection="Horizontal" runat="server" AutoPostBack="true"
                        onselectedindexchanged="chkStatusList_SelectedIndexChanged"></asp:CheckBoxList>   
                        </ContentTemplate>
                        </asp:UpdatePanel>  
                 </td>
                    <td width="10%" align="right"> 
                        <asp:Literal ID="ltPayment" Text="Payment :" runat="server"></asp:Literal>
                    </td>
                    <td width="25%" align="left">
                    <asp:UpdatePanel ID="updatesearch" runat="server">
                    <ContentTemplate>
                    
                    <asp:CheckBoxList ID="chkPaymentType"  RepeatDirection="Horizontal" runat="server" AutoPostBack="true"
                            onselectedindexchanged="chkPaymentType_SelectedIndexChanged">
                    
                    <asp:ListItem Text="Outstanding" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Not Due" Value="2" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Matched" Value="3"></asp:ListItem>
                    </asp:CheckBoxList> 
                    </ContentTemplate>
                    </asp:UpdatePanel>  
                        </td>
                        <td align="left" width="15%" >
                           <asp:ImageButton ID="btnSearch" runat="server" ToolTip="Search" 
                                ImageUrl="~/Images/SearchButton.png" onclick="btnSearch_Click" />&nbsp;
                           <asp:ImageButton ID="ibtnAdd" runat="server" ToolTip="Add New Invoice" OnClientClick='OpenScreen(0,0);return false;'
                                            ImageUrl="~/Images/Add-icon.png"  />  
<%--                           <asp:ImageButton ID="imgInvPrep" runat="server" ToolTip="Invoice Preparation" OnClientClick='OpenInvPrep(0);return false;'
                        ImageUrl="~/Images/issue-list.gif"  />    --%>                
                        </td>
                    </tr>
                    <tr>
                    <td colspan="5" align = "center" style="color: #FF0000;" >
                    
                    <asp:Literal ID="ltmessage" runat = "server"> </asp:Literal>
                    </td>
                    </tr>
                    <tr>
                         <td colspan="5" align="center">
                            <asp:GridView ID="gvHireInvoices" runat="server" AutoGenerateColumns="False" 
                                GridLines="Both" Width="98%" DataKeyNames="ID" >
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Created On">
                                           <ItemTemplate>
                                            <asp:Label ID="lblCreatedOn" runat="server" 
                                                Text='<%# Eval("Date_Of_Creation") %>' 
                                                Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" ForeColor="Blue" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Ref. No">
                                    <ItemTemplate>
                                         <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("Hire_Invoice_No") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="left" Wrap="true" Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Due Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDueDate" runat="server" 
                                                Text='<%# Eval("Due_Date") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Period From">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPeriodfrom" runat="server" 
                                                Text='<%# Eval("PeriodFrom") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Period To">
                                        <ItemTemplate>
                                            <asp:Label ID="lblperiodTo" runat="server" 
                                                Text='<%# Eval("PeriodTo") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Invoice Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBilledAmt" runat="server" 
                                                Text='<%# Eval("Billed_Amount") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Right" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Received Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecvAmt" runat="server" 
                                                Text='<%# Eval("Received_Amount") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Right" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Outstanding">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOustanding" runat="server" 
                                                Text='<%# Eval("DUE") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Right" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" 
                                                Text='<%# Eval("Status") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate >

                                        <asp:ImageButton ID="ibtnView" style="border: 0; width: 14px; height: 14px" ToolTip="View"  OnClientClick='<%#"OpenScreen((&#39;" + Eval("[Charter_Party_ID]") +"&#39;),(&#39;"+ Eval("[ID]") + "&#39;));return false;"%>'
                                         ForeColor="Black"       ImageUrl="~/Images/asl_view.png" runat="server" /> &nbsp;
<%--         
                                         <asp:ImageButton ID="ImageButton1" style="border: 0; width: 14px; height: 14px" ToolTip="Invoice Preparation"  OnClientClick='<%#"OpenInvPrep((&#39;" + Eval("[Charter_ID]") +"&#39;),(&#39;"+ Eval("[Hire_Invoice_Id]") + "&#39;));return false;"%>'
                                         ForeColor="Black" ImageUrl="~/Images/YellowCard.png" runat="server" />--%>

                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" Wrap="False" />
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