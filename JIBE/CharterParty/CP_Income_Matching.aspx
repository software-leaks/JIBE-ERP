<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="CP_Income_Matching.aspx.cs"
    Inherits="CP_Income_Matching" %>

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
            OpenPopupWindowBtnID('CP', 'Billing Item Entry', url, 'popup', 600, 800, null, null, false, false, true, null, 'btnRefresh');
        }

        function RadioCheck(rb) {
            var gv = document.getElementById("<%=gvMatching.ClientID%>");
            var rbs = gv.getElementsByTagName("input");

            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }

        }

        function RadioTransactionCheck(rb) {
            var gv = document.getElementById("<%=gvTransaction.ClientID%>");
            var rbs = gv.getElementsByTagName("input");

            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }

        function CheckSelection() {

            var CountMatch = 0
            var CountTran = 0

            var gv = document.getElementById("<%=gvMatching.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked) {
                        CountMatch = 1;
                    }
                }
            }
            var gv2 = document.getElementById("<%=gvTransaction.ClientID%>");
            var rbs2 = gv2.getElementsByTagName("input");
            for (var i = 0; i < rbs2.length; i++) {
                if (rbs2[i].type == "radio") {
                    if (rbs2[i].checked) {
                        CountTran = 1;
                    }
                }
            }



            if( CountMatch == 0)
                 {
                    alert("Please select an invoice!");
                    return false;
                 }
                 else if(CountTran == 0)
                       {
                    alert("Please select a received transaction!");
                    return false;
                }
                 else if (document.getElementById("txtMatchAmt").value == "") {
                alert("Please add an amount!");
                return false;
            }
                 else 
                    return true; 
        }
    </script>
</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
    height: 100%;">
    <form id="form1" runat="server">
    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
            <table width="100%" cellpadding="2" cellspacing="0" >
                <tr>
                    <td align="center" colspan="3">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltPageHeader" Text ="Income Matching" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>

                <td align="center" width ="50%">
                   <asp:Button ID="btnHideShow" runat="server" ToolTip="Hide Show"
                   Text="Show All" onclick="btnHideShow_Click"  />&nbsp;&nbsp;
                   <asp:Label ID="ltMessageNote" runat="server" Text="Only approved invoices can be processed." ForeColor="red"></asp:Label>
                </td>
               <td align="left">
               <asp:TextBox ID="txtMatchAmt" runat="server" Width="100px" MaxLength="15" ></asp:TextBox>&nbsp;
               <asp:Button ID ="btnMatch" OnClientClick="return CheckSelection();" 
                       ValidationGroup="vgMatch" runat="server" Width="100px" Text="Match" 
                       onclick="btnMatch_Click" />&nbsp;
                     <asp:Label ID="lblError" ForeColor="Red" Visible="false" runat="server"></asp:Label>
                     <asp:RegularExpressionValidator ID="rgvPricePerUnit" runat="server" 
                     ErrorMessage="Match amount not valid!"
                        ValidationGroup="vgMatch" ControlToValidate="txtMatchAmt"
                        ForeColor="Red" ValidationExpression="^[0-9.-]+$"> </asp:RegularExpressionValidator>
    
                </td>
                </tr>
                    <tr>
                    
                    <td valign="top">
                    <div class="freezing" style="width:100%">
                        <asp:gridview ID="gvMatching" runat="server" AllowAutomaticInserts="True" 
                            GridLines="None"  Width="100%"
                                                    ShowFooter="false" ViewStateMode="Enabled" 
                            Skin="Office2007" Style="margin-left: 0px" OnRowDataBound="gvMatching_RowDataBound"
                                                     AutoGenerateColumns="false" AllowPaging="true"
                                                   AllowMultiRowSelection="True" PageSize="10"  HeaderStyle-HorizontalAlign="Center"
                                                    AlternatingItemStyle-BackColor="#CEE3F6" 
                            onpageindexchanging="gvMatching_PageIndexChanging">
                                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css"  HorizontalAlign="Center" />
                                
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                
                                <asp:TemplateField  HeaderText="Inv Date"
                                            Visible="true" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblInvDate" runat="server" Text='<%# Eval("Inv_Date")%>' ></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle Width="15%" Wrap="true" />
                                  </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Due Date"
                                            Visible="true" >
                                            <ItemTemplate>
                                             <asp:Label ID="lbldueDate" runat="server" Text='<%# Eval("Due_Date")%>' ></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle Width="15%" Wrap="true" />
                                  </asp:TemplateField>
                                  <asp:TemplateField  HeaderText="Invoice No"
                                            Visible="true" >
                                            <ItemTemplate>
                                              <asp:Label ID="Inv_No" runat="server" Text='<%# Eval("Hire_Invoice_No")%>' ></asp:Label>
                                              <asp:HiddenField ID="hdnOustStandingRemarks" runat="server" Value='<%# Eval("OutStanding_Remarks")%>' />
                                            </ItemTemplate>
                                              <ItemStyle Width="20%" Wrap="true" />
                                  </asp:TemplateField>
                                  <asp:TemplateField  HeaderText="Inv Amt(USD)"
                                            Visible="true" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblBilledAmt" runat="server" Text='<%# Eval("Billed_Amount")%>' ></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle Width="15%" Wrap="true" />
                                  </asp:TemplateField>
                                  <asp:TemplateField  HeaderText="Matched"
                                            Visible="true" >
                                            <ItemTemplate>
                                             <asp:Label ID="lblAllocatedAmt" runat="server" Text='<%# Eval("Allocated_Amount")%>' ></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle Width="12%" Wrap="true" />
                                  </asp:TemplateField>
                                  <asp:TemplateField  HeaderText="Out Standing"
                                            Visible="true" >
                                            <ItemTemplate>
                                             <asp:Label ID="lblOustandingAmt" runat="server" Text='<%# Eval("OutStanding_Amount")%>' ></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle Width="15%" Wrap="true" />
                                  </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Action"
                                            Visible="true" >
                                <ItemTemplate>
                                        <asp:ImageButton ID="ibtnView" style="border: 0; width: 14px; height: 14px"  OnClick="ibtnView_Click"
                                        ForeColor="Black"  ImageUrl="~/Images/asl_view.png" runat="server" />&nbsp;
                                <asp:HiddenField ID="hdnHireInvId"  runat="server" Value='<%# Eval("ID")%>' />
                                 <asp:HiddenField ID="hdnInvoiceRef"  runat="server" Value='<%# Eval("Hire_Invoice_ID")%>' />
                                <asp:RadioButton ID="rdInv" OnCheckedChanged="rdInv_CheckChanged"  onclick= "RadioCheck(this);" runat="server" ToolTip="Select To Match" />
                                </ItemTemplate>
                                  <ItemStyle Width="13%" Wrap="true" />
                                </asp:TemplateField>

                                </Columns>
                        </asp:gridview>
                     </div>
                 
                    </td>

                    <td width="50%" valign="top">
                       <div class="freezing" style="width:100%">
                        <asp:gridview ID="gvTransaction" runat="server" AllowAutomaticInserts="True" 
                               GridLines="None"  Width="100%"
                                                    ShowFooter="false" ViewStateMode="Enabled" 
                               Skin="Office2007" Style="margin-left: 0px"
                                                     AutoGenerateColumns="false" AllowPaging="true"
                                                   AllowMultiRowSelection="True" PageSize="10"  HeaderStyle-HorizontalAlign="Center"
                                                    AlternatingItemStyle-BackColor="#CEE3F6" OnRowDataBound="gvTransaction_RowDataBound"
                               onpageindexchanging="gvTransaction_PageIndexChanging">
                                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css"  HorizontalAlign="Center" />
                                
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                  <asp:TemplateField  HeaderText="Action"
                                            Visible="true" >
                                <ItemTemplate>
                                     <asp:RadioButton ID="rdInv"  onclick= "RadioTransactionCheck(this);" OnCheckedChanged="rdTran_CheckChanged" runat="server" ToolTip="Select To Match" />&nbsp;
                                        <asp:ImageButton ID="ibtnViewRemittance" style="border: 0; width: 14px; height: 14px" OnClick="ibtnViewRemittance_Click" 
                                        ForeColor="Black"  ImageUrl="~/Images/asl_view.png" runat="server" />
                                <asp:HiddenField ID="hdnRemittanceId"  runat="server" Value='<%# Eval("Remittance_ID")%>' />
                                <asp:HiddenField ID="hdnType"  runat="server" Value='<%# Eval("Type")%>' />
                                </ItemTemplate>
                                <ItemStyle Width="8%" Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Transaction"
                                            Visible="true" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblInvDate" runat="server" Text='<%# Eval("Trans")%>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="12%" Wrap="true" />
                                  </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Dated"
                                            Visible="true" >
                                            <ItemTemplate>
                                             <asp:Label ID="lbldueDate" runat="server" Text='<%# Eval("Received_Date")%>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="12%" Wrap="true" />
                                  </asp:TemplateField>
                                  <asp:TemplateField  HeaderText="Reference"
                                            Visible="true" >
                                            <ItemTemplate>
                                              <asp:Label ID="Inv_No" runat="server" Text='<%# Eval("Remittance_ID")%>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" Wrap="true" />
                                  </asp:TemplateField>
                                  <asp:TemplateField  HeaderText="Amount(USD)"
                                            Visible="true" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblBilledAmt" runat="server" Text='<%# Eval("Amount_Received")%>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" Wrap="true" />
                                  </asp:TemplateField>
                                  <asp:TemplateField  HeaderText="Matched"
                                            Visible="true" >
                                            <ItemTemplate>
                                             <asp:Label ID="lblAllocatedAmt" runat="server" Text='<%# Eval("Allocated_Amount")%>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" Wrap="true" />
                                  </asp:TemplateField>
                                  <asp:TemplateField  HeaderText="Available"
                                            Visible="true" >
                                            <ItemTemplate>
                                             <asp:Label ID="lblOustandingAmt" runat="server" Text='<%# Eval("Available_Amount")%>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" Wrap="true" />
                                  </asp:TemplateField>
                                  <asp:TemplateField  HeaderText="Remarks"
                                            Visible="true" >
                                            <ItemTemplate>
                                             <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remittance_Remarks")%>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" Wrap="true" />
                                  </asp:TemplateField>
                                      </Columns>
                           </asp:gridview>
                          
                     </div>
                 
                    </td>
                    </tr>
                    <tr>
                    <td colspan="2">     
                    <table width="100%">
                    
                    <tr>
                    <td width="30%" align="left" style="font-weight:bold">
                     <asp:Literal ID="ltInvoiceItems" Text="Invoice Items" runat="server"></asp:Literal>
                    </td>
                    <td width="50%" style="font-weight:bold" align="left">
                        <asp:Literal ID="ltHeader" Text="Offset Records" runat="server"></asp:Literal>
                    </td>
                    <td width="20%" align="left" style="color:Blue">
                    <asp:Literal ID="ltOutstandingRemarks" runat="server"></asp:Literal>
                    </td>
                    </tr>
                     <tr>
                    <td width="30%" align="left" valign="top">

                        <asp:gridview ID="gvInvoiceItems" runat="server" AllowAutomaticInserts="True" 
                            GridLines="None"  Width="100%" OnPageIndexChanging="gvInvoiceItems_PageIndexChanging"
                                                    ShowFooter="false" ViewStateMode="Enabled" 
                            Skin="Office2007" Style="margin-left: 0px"
                                                     AutoGenerateColumns="false" AllowPaging="true"
                                                   AllowMultiRowSelection="True" PageSize="10"  HeaderStyle-HorizontalAlign="Center"
                                                    AlternatingItemStyle-BackColor="#CEE3F6" >
                                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css"  HorizontalAlign="Center" />
                                
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                
                                <asp:TemplateField  HeaderText="Invoice Items"
                                            Visible="true" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblInvDate" runat="server" Text='<%# Eval("Item_Group") + " - " + Eval("Item_Name")%>' ></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle Width="70%" Wrap="true" HorizontalAlign="left"/>
                                  </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Amount"
                                            Visible="true" >
                                            <ItemTemplate>
                                             <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("Item_Amount")%>' ></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle Width="30%" Wrap="true" />
                                  </asp:TemplateField>
                                </Columns>
                        </asp:gridview>
                    </td>&nbsp;
                    <td width="50%"  align="left" valign="top">
                        
                        <asp:gridview ID="gvOffset" runat="server"  AllowAutomaticInserts="True" 
                            GridLines="None"  Width="100%"
                            ShowFooter="false" ViewStateMode="Enabled"  EmptyDataText="No offset record found."
                            Skin="Office2007" Style="margin-left: 0px"
                            AutoGenerateColumns="false" AllowPaging="true"
                            AllowMultiRowSelection="True" PageSize="10"  HeaderStyle-HorizontalAlign="Center"
                            AlternatingItemStyle-BackColor="#CEE3F6" >
                            <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                            <RowStyle CssClass="PMSGridRowStyle-css"  HorizontalAlign="Center" />
                                
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                
                                <asp:TemplateField  HeaderText="Hire ref"
                                            Visible="true" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblInvNo" runat="server" Text='<%# Eval("Hire_Invoice_No")%>' ></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle Width="30%" Wrap="true" />
                                  </asp:TemplateField>

                                    <asp:TemplateField  HeaderText="Curr"
                                            Visible="true" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblCurr" runat="server" Text='<%# Eval("OffSet_Currency")%>' ></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle Width="10%" Wrap="true" />
                                  </asp:TemplateField>

                                <asp:TemplateField  HeaderText="Offset Amt"
                                            Visible="true" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("OffSet_Amount")%>' ></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle Width="20%" Wrap="true" />
                                  </asp:TemplateField>
                                  <asp:TemplateField  HeaderText="Type"
                                            Visible="true" >
                                            <ItemTemplate>
                                            <asp:Label ID="lbltype" runat="server" Text='<%# Eval("OffSet_Type")%>' ></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle Width="20%" Wrap="true" />
                                  </asp:TemplateField>
                                   <asp:TemplateField  HeaderText="Reference"
                                            Visible="true" >
                                            <ItemTemplate>
                                            <asp:Label ID="lblref" runat="server" Text='<%# Eval("Ref")%>' ></asp:Label>
                                            </ItemTemplate>
                                              <ItemStyle Width="20%" Wrap="true" />
                                  </asp:TemplateField>

                                </Columns>
                        </asp:gridview>

                    </td>&nbsp;
                    <td width="15%"  align="left" valign="top">
                    <asp:TextBox ID="txtOutStandingRemarks" Width="80%" Height="80px" runat="server"  TextMode="MultiLine"></asp:TextBox>
                   <br />

                    &nbsp;<asp:Button ID="btnSubmit" Text="Submit Remark" 
                            runat="server" onclick="btnSubmit_Click"/>
                    </td>
                    </tr>

                    </table>
                                   
                    </td>
                    </tr>

</table>
</center>
</div>
</form>
</body>
</html>