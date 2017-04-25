<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Log_Payment_Entry.aspx.cs"
    Inherits="PO_LOG_PO_Log_Payment_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/swfobject.js" type="text/javascript"></script>
    <link href="../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <%--<link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />--%>
    <script language="javascript" type="text/javascript">
        function refreshAndClose() {
            window.parent.ReloadParent_ByButtonID();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Sct1" runat="server">
    </asp:ScriptManager>
    <center>
        <asp:UpdatePanel ID="upd1" runat="server">
            <ContentTemplate>
                <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                    height: 100%;">
                    <div id="page-title" class="page-title">
                        Payment Details
                    </div>
                    <table width="100%" cellpadding="2" cellspacing="2">
                        <tr>
                            <td align="right" style="width: 100%">
                                <asp:GridView ID="gvPaymentDetails" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" DataKeyNames="ID,Invoice_ID" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both" AllowSorting="true" OnRowDataBound="gvPaymentDetails_RowDataBound">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Type">
                                            <HeaderTemplate>
                                                PO Type
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server" Text='<%#Eval("Req_Type")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Owner">
                                            <HeaderTemplate>
                                                Owner Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOwner" runat="server" Text='<%#Eval("Owner_Short_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                PO And INVOICE
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPO_Code" runat="server" Text='<%#Eval("Office_Ref_Code")%>'></asp:Label>
                                                <asp:Label ID="Lbl4" runat="server" Text="|"></asp:Label>
                                                <asp:Label ID="lblInvoice_Code" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                Payment Due Date
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPayment_Due_Date" runat="server" Text='<%#Eval("Payment_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                Payment Approved By
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPayment_Approved_By" runat="server" Text='<%#Eval("Payment_Approved_By")%>'></asp:Label>
                                                <asp:Label ID="lbler" runat="server" Text="|"></asp:Label>
                                                <asp:Label ID="lblPayment_Approved_Date" runat="server" Text='<%#Eval("Payment_Approved_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                Invoice Value
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                Cur
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCur" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                Urgency
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUrgency" runat="server" Text='<%#Eval("Urgency")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                Payment Status
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPayment_Status" runat="server" Text='<%#Eval("Payment_Status")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgView" runat="server" OnClientClick='<%#"OpenScreen(&#39;" + Eval("[ID]") +"&#39;);return false;"%>'
                                                                CommandName="Select" ForeColor="Black" ToolTip="View" ImageUrl="~/Images/asl_view.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" style="width: 100%">
                                <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td align="left" valign="top">
                                                <asp:Button ID="btnlink" runat="server" Text="Link To selected Payment" />
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:Button ID="btnNew" runat="server" Text="Create New Payment" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" style="width: 50%;">
                                                <asp:GridView ID="gvlinkPayment" runat="server" EmptyDataText="NO RECORDS FOUND"
                                                    AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                                                    Width="100%" GridLines="both" AllowSorting="true">
                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Type">
                                                            <HeaderTemplate>
                                                                No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblType" runat="server" Text='<%#Eval("No")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Owner">
                                                            <HeaderTemplate>
                                                                Owner
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOwner" runat="server" Text='<%#Eval("Owner")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <HeaderTemplate>
                                                                PO Code
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPO_Code" runat="server" Text='<%#Eval("PO_Code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <HeaderTemplate>
                                                                Due Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayment_Due_Date" runat="server" Text='<%#Eval("Payment_Due_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <HeaderTemplate>
                                                                Invoice Ref
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayment_Approved_By" runat="server" Text='<%#Eval("Payment_Approved_By")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <HeaderTemplate>
                                                                Invoice Value
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("Invoice_Value")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <HeaderTemplate>
                                                                Cur
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCur" runat="server" Text='<%#Eval("Cur")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <HeaderTemplate>
                                                                Urgency
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUrgency" runat="server" Text='<%#Eval("Urgency")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:GridView ID="gvNewPayment" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                                    DataKeyNames="ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                                    AllowSorting="true">
                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Type">
                                                            <HeaderTemplate>
                                                                No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblType" runat="server" Text='<%#Eval("No")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <HeaderTemplate>
                                                                Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOwner" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cur">
                                                            <HeaderTemplate>
                                                                Cur
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPO_Code" runat="server" Text='<%#Eval("Cur")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <HeaderTemplate>
                                                                Count
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayment_Due_Date" runat="server" Text='<%#Eval("Count")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value Date">
                                                            <HeaderTemplate>
                                                                Value Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayment_Approved_By" runat="server" Text='<%#Eval("Payment_Approved_By")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mode">
                                                            <HeaderTemplate>
                                                                Mode
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("Invoice_Value")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Right" Width="80px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <HeaderTemplate>
                                                                Account Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCur" runat="server" Text='<%#Eval("Cur")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <HeaderTemplate>
                                                                Status
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUrgency" runat="server" Text='<%#Eval("Urgency")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                                    height: 100%;">
                                    <table style="width: 80%;">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblPaymentID" runat="server" CssClass="txtInput" Text=""></asp:Label><asp:Label
                                                    ID="lblPaymentYear" runat="server" CssClass="txtInput" Text=""></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:Label ID="lblSupplierName" CssClass="txtInput" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Payment Amount :
                                            </td>
                                            <td align="left">
                                                <asp:Label ID="lblValue" CssClass="txtInput" Width="200px" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td align="right">
                                                Bank Reference :
                                            </td>
                                            <td align="left">
                                            <asp:Label ID="lblBankRef" runat="server" CssClass="txtInput" Text="" Width="200px"></asp:Label>
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Payment Date :
                                            </td>
                                            <td align="left">
                                                 <asp:TextBox ID="txtPayDate" runat="server" CssClass="txtInput" Width="200px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtPayDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                                    TargetControlID="txtPayDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td align="right">
                                                Payment Mode :
                                            </td>
                                            <td align="left">
                                             <asp:DropDownList ID="ddlPayMode" Width="200px" CssClass="txtInput" runat="server">
                                                </asp:DropDownList>
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Source Account :
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:DropDownList ID="ddlAccount" Width="400px" CssClass="txtInput" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Bank Amount :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtBankAmt" CssClass="txtInput" Width="200px" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                Bank Charges :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtBankCharge" CssClass="txtInput" Width="200px" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Payment Mode :
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:RadioButtonList ID="rdbPaymode" RepeatDirection="Horizontal" CssClass="txtInput"
                                                    runat="server">
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Remarks :
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Width="400px" CssClass="txtInput"
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Journal
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:Label ID="lblJournal" runat="server" Width="400px" CssClass="txtInput" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4">
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update Payment" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4">
                                                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" CssClass="txtInput" Text=""
                                                    Width="400px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;">
                    <table>
                        <tr>
                            <td>
                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                    Invoice Details
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                    <asp:GridView ID="gvApprovedInvoice" runat="server" EmptyDataText="NO RECORDS FOUND"
                                        AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                                        Width="100%" GridLines="both" CssClass="gridmain-css" AllowSorting="true">
                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Vessel">
                                                <HeaderTemplate>
                                                    No.
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPStatus" runat="server" Text='<%#Eval("Srno")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Name">
                                                <HeaderTemplate>
                                                    Supplier Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupplier" runat="server" Text='<%#Eval("Supplier_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO Code">
                                                <HeaderTemplate>
                                                    PO Code
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCode" runat="server" Text='<%#Eval("PO_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO Value">
                                                <HeaderTemplate>
                                                    PO Value
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPO_Amount" runat="server" Text='<%#Eval("PO_Amount")%>'></asp:Label>
                                                    <asp:Label ID="lblPo_Currency" runat="server" Text='<%#Eval("PO_Currency")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Ref">
                                                <HeaderTemplate>
                                                    Invoice Ref
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoice_Ref" runat="server" Text='<%#Eval("Invoice_Reference")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Date">
                                                <HeaderTemplate>
                                                    Invoice Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoice_Date" runat="server" Text='<%#Eval("Invoice_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Verified">
                                                <HeaderTemplate>
                                                    Verified By
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVerified_By" runat="server" Text='<%#Eval("Verified_By")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Type Status">
                                                <HeaderTemplate>
                                                    Invoice Status
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoice_Status" runat="server" Text='<%#Eval("Invoice_Status")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Verified">
                                                <HeaderTemplate>
                                                    Approved By
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVerified_By" runat="server" Text='<%#Eval("Approved_By")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Date">
                                                <HeaderTemplate>
                                                    Approved Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoice_Date" runat="server" Text='<%#Eval("Approved_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice Value">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblInvoice_Amount" runat="server" CommandName="Sort" CommandArgument="Invoice_Amount"
                                                        ForeColor="Black">Invoice Value</asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoice_Value" runat="server" Text='<%#Eval("Invoice_Amount")%>'></asp:Label>
                                                    <asp:Label ID="lblInvoice_Currency" runat="server" Text='<%#Eval("Invoice_Currency")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Confirmed Delivered">
                                                <HeaderTemplate>
                                                    Invoice Due Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDelivered" runat="server" Text='<%#Eval("Invoice_Due_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment Due Date">
                                                <HeaderTemplate>
                                                    Urgency
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPaymentDate" runat="server" Text='<%#Eval("Urgency")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderTemplate>
                                                    Action
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnCompare" runat="server" Text="Compare" OnClientClick='<%#"OpenScreen((&#39;" + Eval("[ID]") +"&#39;),(&#39;"+ Eval("[Invoice_ID]") + "&#39;));return false;"%>' />
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" Width="30px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="30" OnBindDataItem="BindSupplierDetails" />
                                    <asp:HiddenField ID="HiddenField2" runat="server" EnableViewState="False" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;">
                    <table>
                        <tr>
                            <td>
                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                    Supplier Details
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Supplier Name :
                            </td>
                            <td>
                                <asp:TextBox ID="txtSupplierName" runat="server" Enabled="false" TextMode="MultiLine" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                Payment Terms :
                            </td>
                            <td>
                                <asp:TextBox ID="txtPaymentTerms" runat="server" Enabled="false" TextMode="MultiLine" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address :
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress" runat="server" Enabled="false" TextMode="MultiLine" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                Payment Instructions :
                            </td>
                            <td>
                                <asp:TextBox ID="txtPayment" runat="server" Enabled="false" TextMode="MultiLine" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Country/City :
                            </td>
                            <td>
                                <asp:TextBox ID="txtCity" runat="server" Enabled="false" TextMode="MultiLine" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                Payment Notifications :
                            </td>
                            <td>
                                <asp:TextBox ID="txtpaymentnotification" runat="server" Enabled="false" TextMode="MultiLine" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Email :
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Enabled="false" TextMode="MultiLine" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                Phone :
                            </td>
                            <td>
                                <asp:TextBox ID="txtPhone" runat="server" Enabled="false" TextMode="MultiLine" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                Fax :
                            </td>
                            <td>
                                <asp:TextBox ID="txtFax" runat="server" Enabled="false" TextMode="MultiLine" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="display: none;">
                    <asp:TextBox ID="txtSupplierCode" runat="server" Width="1px"></asp:TextBox>
                    <asp:TextBox ID="txtPayMode" runat="server" Width="1px"></asp:TextBox>
                    <asp:TextBox ID="txtInvoiceCode" runat="server" Width="1px"></asp:TextBox>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
    </form>
</body>
</html>
