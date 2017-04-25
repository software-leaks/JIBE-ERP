<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Attachment.aspx.cs" Inherits="Attachment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Attach Files</title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <link href="../Styles/EGSoft.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/drag.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmAttachment" runat="server">
    <script type="text/javascript">
        function Validate() {


            var ddlAttachmentType = document.getElementById("<%=ddlAttachmentType.ClientID%>");

            var SelectedAttacType = ddlAttachmentType.options[ddlAttachmentType.selectedIndex].value;

            if (SelectedAttacType == '') {
                alert('Please select attachment type !')
                return false;

            }


            if (document.getElementById("<%=txtInvNo.ClientID%>").value.trim() == "" || document.getElementById("<%=txtInvAmount.ClientID%>").value.trim() == "" || document.getElementById("<%=txtInvDueDate.ClientID%>").value.trim() == "") {
                alert('Please enter invoice number/amount/due date');
                return false;
            }


        }

        function CkeckForInvoce() {



        }
    </script>
    <asp:ScriptManager ID="__sm1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <div style="font-family: Tahoma; font-size: 12px; border: 1px solid gray; height: 570px">
        <center>
            <div style="border: 1px solid  #5588BB; padding: 2px; background-color: #5588BB;
                color: #FFFFFF; text-align: center;">
                <b>View/Add &nbsp;&nbsp; Attachments </b>
            </div>
            <table width="100%" style="text-align: center" cellpadding="5">
                <tr>
                    <td align="right">
                        Attachment Type :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAttachmentType" runat="server" AppendDataBoundItems="false"
                            onChange="CkeckForInvoce()" AutoPostBack="true" OnSelectedIndexChanged="ddlAttachmentType_SelectedIndexChanged">
                            <asp:ListItem Text="ALL" Value=""></asp:ListItem>
                            <asp:ListItem Text="General Documents" Value="DOCUMENT"> </asp:ListItem>
                            <asp:ListItem Text="E-Ticket" Value="ETICKET"></asp:ListItem>
                            <asp:ListItem Text="Invoice" Value="INVOICE"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <br />
         
            <div id="dvInvoice" runat="server">
                <table cellpadding="0px" cellspacing="0px" style="width: 600px; border: 1px solid gray;
                    background-color: #ffffff;">
                    <caption style="background-color: Gray; color: White;">
                        <b>Upload Invoice</b>
                    </caption>
                    <tr>
                        <td style="text-align: left;">
                            Invoice No.
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvNo" runat="server" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            Amount
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvAmount" runat="server" onchange="return IsNumeric(id);" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            Currency
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbCurrency" Width="155px" runat="server">
                                <asp:ListItem Text="-select" Value=""></asp:ListItem>
                                <asp:ListItem Text="USD" Value="USD"></asp:ListItem>
                                <asp:ListItem Text="SGD" Value="SGD"></asp:ListItem>
                                <asp:ListItem Text="GBP" Value="GBP"></asp:ListItem>
                                <asp:ListItem Text="INR" Value="INR"></asp:ListItem>
                                <asp:ListItem Text="THB" Value="THB"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            Due Date
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvDueDate" runat="server" Width="150px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CEDueDate" runat="server" TargetControlID="txtInvDueDate"
                                Format="dd-MMM-yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left;">
                            Remarks
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtInvoiceRemarks" runat="server" TextMode="MultiLine" Width="460px"
                                Font-Size="12px" Font-Names="Tohama" Height="30px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:Label ID="lblhdrAttach" runat="server" Text="Attach File :&nbsp;&nbsp;&nbsp;&nbsp"></asp:Label>
                <asp:FileUpload ID="flName" Width="580px" runat="server" />
                &nbsp;&nbsp;
                <asp:Button ID="cmdUpload" runat="server" Text="Upload" OnClick="cmdUpload_Click"
                    OnClientClick="return Validate()" />
            </div>
            <br />
            <div id="dvFiles">
                <asp:GridView ID="grdFiles" runat="server" Caption="" DataKeyNames="id" Width="98%"
                    DataSourceID="objAttachment" AutoGenerateColumns="false" OnRowCommand="grdFiles_onRowCommand"
                    OnRowDataBound="grdFiles_onRowDataBound">
                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <RowStyle CssClass="RowStyle-css" />
                    <HeaderStyle CssClass="HeaderStyle-css" />
                    <Columns>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attachment_Name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Document Type">
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attachment_Type")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference No">
                            <ItemTemplate>
                                <asp:Label ID="lblRefNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Ref_Number")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField Text="Download" DataNavigateUrlFields="Attachment_path" DataNavigateUrlFormatString="downloadfile.aspx?filename={0}"
                            HeaderText="Download" Target="_blank" />
                        <asp:CommandField ShowDeleteButton="true" ControlStyle-BorderWidth="0px" ButtonType="Image"
                            DeleteImageUrl="images/delete.gif" />
                    </Columns>
                </asp:GridView>
            </div>
        </center>
    </div>
    <asp:ObjectDataSource ID="objAttachment" runat="server" TypeName="SMS.Business.TRAV.BLL_TRV_Attachment"
        SelectMethod="GetAttachments" DeleteMethod="DeleteAttachment">
        <SelectParameters>
            <asp:Parameter Name="requestid" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="Attach_Type" Type="String" DefaultValue="" />
            <asp:Parameter Name="AgentID" Type="Int32" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="userid" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
