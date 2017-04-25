<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPurcCancelReqsn.ascx.cs"
    Inherits="UserControl_ucPurcCancelReqsn" %>
<link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    function CheckRemark() {


        alert("A Cancelled requisition will not be active for futher action  Are you sure to cancel ?");
    }
</script>
<table style="text-align: center; padding: 5px 5px 5px 5px; border: 1px solid black"
    class="popup-css" cellpadding="0" cellspacing="0">
    <tr>
        <td colspan="2" style="text-align: center; font-size: 14px; font-weight: bold">
            Cancel Requisition
        </td>
    </tr>
    <tr>
        <td>
            POs:
        </td>
        <td>
            <asp:DataList ID="dlistPONumber" runat="server" Width="100%" Style="border: 1px solid gray;
                border-collapse: collapse" RepeatDirection="Horizontal" RepeatLayout="Table">
                <HeaderTemplate>
                    <tr>
                        <td style="font-weight: bold; border: 1px solid gray">
                            Order Code
                        </td>
                        <td style="font-weight: bold; border: 1px solid gray">
                            Supplier Name
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="padding-right: 10px;">
                            <a href="POPreview.aspx?RFQCODE=<%# Eval("REQUISITION_CODE") +"&Vessel_Code="+ Eval("Vessel_CODE")+"&Order_Code="+ Eval("ORDER_CODE") %> "
                                target="_blank" style="color: Red">
                                <%# Eval("ORDER_CODE")%></a>
                        </td>
                        <td style="">
                            <%#Eval("SHORT_NAME")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </td>
    </tr>
    <tr>
        <td>
            Remark:
        </td>
        <td>
            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Height="80px" ValidationGroup="save"
                Width="450px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorRemark" ControlToValidate="txtRemark"
                runat="server" ValidationGroup="save" Display="None" ErrorMessage="Please enter Remark"></asp:RequiredFieldValidator>
            <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidatorRemark"
                runat="server">
            </tlk4:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnSave" runat="server" Text="OK" Width="100px" Height="30px" ValidationGroup="save"
                OnClick="btnSave_Click" />
            &nbsp;
            <asp:Button ID="btnCancel" runat="server" Width="100px" Height="30px" Text="Cancel" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="HiddenField_ReqsnNumber" runat="server" />
            <asp:HiddenField ID="HiddenField_DocCode" runat="server" />
            <asp:HiddenField ID="HiddenField_VesselCode" runat="server" />
            <asp:HiddenField ID="HiddenField3" runat="server" />
        </td>
    </tr>
</table>
