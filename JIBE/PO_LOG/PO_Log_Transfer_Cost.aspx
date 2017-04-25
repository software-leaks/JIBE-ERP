<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Log_Transfer_Cost.aspx.cs"
    Inherits="PO_LOG_PO_Log_Transfer_Cost" Title="Transfer Cost" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">


        function DocOpen(docpath) {
            //window.open(docpath);
        }

        function previewDocument(docPath) {
            document.getElementById("ifrmDocPreview").src = docPath;
        }
       
      
    </script>
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
        .style7
        {
            width: 30%;
        }
        .style8
        {
            width: 16%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="page-title" class="page-title">
        Transfer Cost
    </div>
    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
        height: 100%;">
        <div>
            <table width="100%">
                <tr>
                    <td align="right" class="style8">
                        Vessel Name :
                    </td>
                    <td width="1px">
                        <asp:Label ID="Label1" ForeColor="Red" Text="*" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left;" class="style7">
                        <asp:DropDownList ID="ddlVessel" runat="server" Width="200px" CssClass="txtInput">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvVessel" runat="server" Display="None" InitialValue="0"
                            ErrorMessage="Vessel is mandatory field." ControlToValidate="ddlVessel" ValidationGroup="vgSubmit"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align: Right;" class="style1">
                        Owner Name:
                    </td>
                    <td width="1px">
                        <asp:Label ID="Label3" ForeColor="Red" Text="*" runat="server"></asp:Label>
                    </td>
                    <td align="left" colspan="4" class="style1">
                        <asp:DropDownList ID="ddlOwnerCode" runat="server" Width="200px" BackColor="#F7F8E0">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="Owner" runat="server" Display="None" InitialValue="0"
                            ErrorMessage="Owner is mandatory field." ControlToValidate="ddlOwnerCode" ValidationGroup="vgSubmit"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right" class="style8">
                        PO List :
                    </td>
                    <td width="1px">
                        <asp:Label ID="Label4" ForeColor="Red" Text="*" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left;" class="style7">
                        <asp:DropDownList ID="ddlPOType" runat="server" Width="200px" CssClass="txtInput">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="POList" runat="server" Display="None" InitialValue="0"
                            ErrorMessage="PO is mandatory field." ControlToValidate="ddlPOType" ValidationGroup="vgSubmit"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td  style="text-align: Right;" class="style1">
                        Transfer Amount:
                    </td>
                    <td width="1px">
                        <asp:Label ID="Label5" ForeColor="Red" Text="*" runat="server"></asp:Label>
                    </td>
                    <td align="left" class="style7">
                        <asp:TextBox ID="txtAmount" runat="server" MaxLength="255" Width="200px" BackColor="#F7F8E0"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="Amount" runat="server" Display="None" ErrorMessage="Amount is mandatory field."
                            ControlToValidate="txtAmount" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td rowspan="2" align="right" class="style8">
                        Remarks :
                    </td>
                    <td rowspan="2" width="1px">
                    </td>
                    <td rowspan="2" colspan="7" style="text-align: left;" class="style7">
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="50px" Width="95%"
                            Style="resize: none;" CssClass="txtInput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td  style="text-align: Right;" class="style8">
                        Budget Code:
                    </td>
                    <td width="1px">
                        <asp:Label ID="Label6" ForeColor="Red" Text="*" runat="server"></asp:Label>
                    </td>
                    <td  align="left"  class="style1">
                        <asp:DropDownList ID="ddlAccClassification" runat="server" Width="200px" BackColor="#F7F8E0">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="Budget" runat="server" Display="None" InitialValue="0"
                            ErrorMessage="Budget Code is mandatory field." ControlToValidate="ddlAccClassification"
                            ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                   
                </tr>
                <tr>
                    <td style="width: 100%" colspan="12" align="center" >
                    <asp:RegularExpressionValidator ID="revNumber" runat="server" ControlToValidate="txtAmount"
                            ValidationGroup="vgSubmit" Display="None" ErrorMessage="Number should be money type(100 or 100.0)"
                            ValidationExpression="^\d+(\.\d\d)?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="12" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="70px"
                            ValidationGroup="vgSubmit" />
                        &nbsp;
                        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" Width="80px" OnClick="btnConfirm_Click"
                            ValidationGroup="vgSubmit" Visible="False" />&nbsp;
                        <asp:Button ID="btnUnconfirm" Width="80px" runat="server" Text="Unconfirm" OnClick="btnUnconfirm_Click"
                            ValidationGroup="vgSubmit" Visible="False" />&nbsp;
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" Width="80px" OnClick="btnApprove_Click"
                            ValidationGroup="vgSubmit" Visible="False" />&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                            Width="80px" Visible="False" />&nbsp;
                        <asp:Button ID="btnExit" runat="server" Text="Exit" Width="70px" ForeColor="Red"
                            CausesValidation="false" OnClick="btnExit_Click" />
                        
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 100%" colspan="12" align="center" >
                        <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="vgSubmit" />
                    </td>
                </tr>
            </table>
            <td valign="top" class="style7">
                <div runat="server" id="divGrid" visible="true" style="border: 1px solid #cccccc;
                    width: 100%; height: 200px; max-height: 200px">
                    <div style="width: 100%; overflow-y: scroll; height: 100px;">
                        <asp:GridView ID="gvTransferCost" runat="server" AutoGenerateColumns="False" GridLines="Both"
                            PagerStyle-Mode="NextPrevAndNumeric" Width="100%">
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                            <SelectedRowStyle BackColor="#FFFFCC" />
                            <EmptyDataRowStyle Font-Bold="true" Font-Size="12px" ForeColor="Red" HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="10px">
                                    <HeaderTemplate>
                                        Transfer Id
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Transfer_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10px">
                                    <HeaderTemplate>
                                        Transfer Status
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Transfer_Status")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10px">
                                    <HeaderTemplate>
                                        Transfer Amount
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Transfer_Amount","{0:n}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Right" Width="60px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10px">
                                    <HeaderTemplate>
                                        Parent Supply Id
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Parent_Supply_Id")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10px">
                                    <HeaderTemplate>
                                        Parent Invoice
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Parent_Invoice_Id")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10px">
                                    <HeaderTemplate>
                                        Exchange_Rate
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Exchange_Rate")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Right" Width="60px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div align="left">
                        <table>
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                </td>
                                <td>
                                    <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                                </td>
                                <td>
                                    <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:HiddenField ID="Parent_invoice_id" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="Parent_Supply_id" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="hiddenTransfer" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="hiddenTransferStatus" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenAmount" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="hiddenClassification" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <div style="height: 20px; width: 100%">
            </div>
            <div id="divAttachment" visible="true" runat="server" style="border: 1px solid #cccccc;
                font-family: Tahoma; font-size: 12px; width: 100%; height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <contenttemplate>
                        <table width="100%">
                        <tr>
                        <td align="center" style="width: 100%;"  >
                                    <label style="display: inline-block; width: 20%; text-align: center">
                                    </label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <asp:Button ID="btnView" runat="server" Text="Upload" OnClick="btnView_Click" CausesValidation="false" />
                                </td>
                        </tr>
                            <tr>
                                
                                <td style="width: 100%">
                                    <asp:GridView ID="gvReqsnAttachment" runat="server" AutoGenerateColumns="False"
                                        CellPadding="2" Width="100%" GridLines="None" CssClass="GridView-css" OnRowDataBound="gvReqsnAttachment_RowDataBound">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Attachment name" DataField="Attachment_Name" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField HeaderText="Uploaded By" DataField="Uploaded_By" />
                                            <asp:BoundField HeaderText="Uploaded On" DataField="Uploaded_On" ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" width="100%" style="border: none">
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="ImgView" runat="server" OnCommand="ImgView_Click" Style="width: 15px; 
                                                                    height: 15px" CommandArgument='<%#Eval("[File_Path]")%>' ForeColor="Black" ToolTip="View"
                                                                    ImageUrl="~/Images/asl_view.png" CausesValidation="false"></asp:ImageButton>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="imgDownload" runat="server" OnCommand="ImgDownload_Click" Style="width: 15px;
                                                                    height: 15px" CommandArgument='<%#Eval("[File_Path]")%>' ForeColor="Black" ToolTip="Download"
                                                                    ImageUrl="~/Images/Download-icon.png"></asp:ImageButton>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="imgbtnDelete" ImageUrl="~/Images/delete.png" runat="server"
                                                                    OnClientClick="javascript:var a =confirm('Are you sure to delete this file ?'); if(a) return true; else return false;"
                                                                    OnClick="imgbtnDelete_Click" CommandArgument='<%#Eval("id")+","+Eval("Link_ID")+","+Eval("File_Path") %>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </contenttemplate>
                    <triggers>
                        <asp:PostBackTrigger ControlID="btnView" />
                     
                    </triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
        height: 100%;">
        <table width="100%">
            <tr>
                <td style="width: 50%" valign="top">
                    <iframe id="iFrame1" runat="server" src="PO_Log_Preview.aspx?Supply_ID=<%= Parent_Supply_id.Value %>"
                        width="100%" height="500"></iframe>
                </td>
                <td style="width: 50%" valign="top">
                    <iframe id="ifrmDocPreview" runat="server" width="100%" height="100%"></iframe>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
