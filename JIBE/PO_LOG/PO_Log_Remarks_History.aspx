<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Log_Remarks_History.aspx.cs"
    Inherits="PO_LOG_PO_Log_Remarks_History" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
 <link href="../Purchase/styles/premiere_blue/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/POLOG_Common_Function.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <%--<script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>--%>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function Onfail(retval) {
            alert(retval._message);
        }
        function asyncBind_PortCost_M() {
            var Invoice_ID = document.getElementById('txtInvoiceID').value;
            var UserID = document.getElementById('txtInvoiceCode').value;
            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'async_Get_POLOG_Remarks', false, { "Invoice_ID": Invoice_ID, "RemarkType": UserID }, onSucc_LoadFunction, Onfail, new Array('dvRemarks', 'lblPologRemarks'));
        }

        function onSucc_LoadFunction(retval, prm) {
            try {
                document.getElementById(prm[0]).innerHTML = retval;
                checkForMyAction(prm[1], retval);
            }
            catch (ex)
            { }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-title" class="page-title">
                Remarks History
            </div>
            <table width="100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                         <div style="height: 400px; background-color: White; overflow-y: scroll; max-height: 400px">
                            <asp:GridView ID="gvRemarkslog" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                DataKeyNames="ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                AllowSorting="true" onrowdatabound="gvRemarkslog_RowDataBound">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Created By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("CREATED_BY")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                           Created Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEntryDate" runat="server" Text='<%#Eval("Created_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <HeaderTemplate>
                                            Remarks
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                             <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="500px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                   
                                   
                                </Columns>
                            </asp:GridView>
                            <uc1:uccustompager id="ucCustomPager1" runat="server" onbinddataitem="BindRemarks" />
                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div style="display: none;">
            <asp:TextBox ID="txtRemarksID" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txtInvoiceID" runat="server" Width="1px"></asp:TextBox>
            <asp:TextBox ID="txtInvoiceCode" runat="server" Width="1px"></asp:TextBox>
        </div>
    </center>
    </form>
</body>
</html>
