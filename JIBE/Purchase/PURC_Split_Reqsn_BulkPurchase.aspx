<%@ Page Title="Split Order" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PURC_Split_Reqsn_BulkPurchase.aspx.cs" Inherits="Purchase_PURC_Split_Reqsn_BulkPurchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <style type="text/css">
        .txtord
        {
            font-size: 11px;
            width: 50px;
            border: 1px solid #E7E7E7;
            border-collapse: collapse;
            text-align: right;
        }
        
        .remainingQty
        {
            background-color: #FFE0BA;
        }
        .remainingPrice
        {
            background-color: #FFE0BA;
        }
    </style>
    <script type="text/javascript" language="javascript">
        var lastfixedColumnID = 7;
        var remainingQtyColumnID = 4;
        var remainingPriceColumnID = 7;
        var totalQtyColumnID = 3;
        var unitPriceColumnID = 5;

        var Old_Value_AllotedQty = 0;

        function Store_Old_Value(oldvalueid) {
            Old_Value_AllotedQty = parseFloat(document.getElementById(oldvalueid).value).toString() == 'NaN' ? 0 : parseFloat(document.getElementById(oldvalueid).value);

        }

        function calculatePriceAndQty(totalQty, unitPrice, lblqty, lblprice, txtAssignedQtyID) {

            var prefixid = lblqty.split('_qty')[0];

            var griditems = document.getElementById("ctl00_MainContent_gvItemsSplit");
            var totalAssignedqty = 0;
            for (var i = lastfixedColumnID + 1; i <= griditems.rows[0].cells.length; i++) {
                var locAssdQty = document.getElementById(prefixid + "_" + i.toString()).value;
                if (parseFloat(locAssdQty) > 0)
                    totalAssignedqty += parseFloat(locAssdQty);

            }

            var CalculatedRemainingQty = 0;
            if (Old_Value_AllotedQty > 0)
                CalculatedRemainingQty = parseFloat((totalAssignedqty - parseFloat(document.getElementById(txtAssignedQtyID).value)) + Old_Value_AllotedQty) + parseFloat(document.getElementById(lblqty).innerHTML);
            else
                CalculatedRemainingQty = parseFloat(document.getElementById(lblqty).innerHTML) + (totalAssignedqty - parseFloat(document.getElementById(txtAssignedQtyID).value));


            if (CalculatedRemainingQty >= totalAssignedqty) {
                document.getElementById(lblqty).innerHTML = (CalculatedRemainingQty - totalAssignedqty).toFixed(2);
                document.getElementById(lblprice).innerHTML = ((CalculatedRemainingQty - totalAssignedqty) * unitPrice).toFixed(2);
            }
            else {
                document.getElementById(txtAssignedQtyID).value = Old_Value_AllotedQty;
                alert('Remaining qty is ' + document.getElementById(lblqty).innerHTML + ' . You can not assign !');

            }

        }

        function calculatePriceAndQty_Onload() {

            var griditems = document.getElementById("ctl00_MainContent_gvItemsSplit");
            var totalAssignedqty = 0;

            for (r = 2; r <= griditems.rows.length; r++) {
                totalAssignedqty = 0;
                var prefixid = (r < 10) ? "ctl00_MainContent_gvItemsSplit_ctl0" + r.toString() : "ctl00_MainContent_gvItemsSplit_ctl" + r.toString();
                for (var i = lastfixedColumnID + 1; i <= griditems.rows[0].cells.length; i++) {

                    var locAssdQty = document.getElementById(prefixid + "_" + i.toString()).value;
                    if (parseFloat(locAssdQty) > 0)
                        totalAssignedqty += parseFloat(locAssdQty);

                }
                var lblqty = prefixid + "_qty" + remainingQtyColumnID.toString();
                var lblprice = prefixid + "_price" + remainingPriceColumnID.toString();

                var totalQtyid = prefixid + "_totqty" + totalQtyColumnID.toString();
                var unitPriceid = prefixid + "_unitprice" + unitPriceColumnID.toString();

                var totalQty = document.getElementById(totalQtyid).innerHTML;
                var unitPrice = document.getElementById(unitPriceid).innerHTML;

                if (parseFloat(unitPrice.toString()) > 0) {

                    document.getElementById(lblqty).innerHTML = (totalQty - totalAssignedqty).toFixed(2);
                    document.getElementById(lblprice).innerHTML = ((totalQty - totalAssignedqty) * unitPrice).toFixed(2);
                }
            }

        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 45%; top: 40%; z-index: 2;
                z-index: 1200; color: black">
                <div style="background-color: Yellow; color: Black; font-size: 12px; font-family: Verdana;
                    border-radius: 5px; text-align: center; padding: 15px">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" /><br />
                    Please wait...working on your request..
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <table align="center" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="background-color: #808080; font-size: small; color: #FFFFFF; text-align: center;
                font-weight: bold">
                Split Bulk Purchase Order
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="updBulkPurchase" runat="server">
        <ContentTemplate>
            <table align="center" width="100%" cellpadding="1" cellspacing="0" style="background-color: #f4ffff;
                color: Black; border-collapse: collapse">
                <tr>
                    <td class="tdh">
                        Vessel :
                    </td>
                    <td class="tdd">
                        <asp:Label ID="lblVessel" runat="server"></asp:Label>
                    </td>
                    <td class="tdh">
                        Received Date :
                    </td>
                    <td class="tdd" colspan="3">
                        <asp:Label ID="lblToDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdh">
                        Catalogue :
                    </td>
                    <td class="tdd">
                        <asp:Label ID="lblCatalog" runat="server"></asp:Label>
                    </td>
                    <td class="tdh">
                        Requisition Number :
                    </td>
                    <td class="tdd">
                        <asp:HyperLink ID="lblReqNo" Target="_blank" runat="server"> </asp:HyperLink>
                    </td>
                    <td class="tdh">
                        Total Items :
                    </td>
                    <td class="tdd">
                        <asp:Label ID="lblTotalItem" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%" style="text-align: left; border-collapse: collapse; color: Black">
                <tr>
                    <td style="border: 1px solid gray">
                        <span style="font-weight: bold;">Select vessels:</span>
                        <br />
                        <br />
                        <asp:CheckBoxList ID="chkVesselList" AutoPostBack="false" RepeatDirection="Horizontal"
                            runat="server" DataTextField="Vessel_Short_Name" DataValueField="Vessel_id" Font-Size="11px"
                            ForeColor="Black">
                        </asp:CheckBoxList>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid gray">
                        <table width="100%">
                            <tr>
                                <td style="width: 60%; text-align: left; padding-left: 40px">
                                    <asp:Button ID="btnSplitItems" Text="Split Order " runat="server" OnClick="btnSplitItems_Click" />
                                </td>
                                <td style="width: 40%">
                                    <%--<asp:Button ID="btnLoadDraftOrder" Text="Load Draft" runat="server" OnClick="btnLoadDraftOrder_Click" />--%>
                                    <asp:Button ID="btnSaveAsDraft" Text="Save as Draft" runat="server" OnClick="btnSaveAsDraft_Click" />
                                    <asp:Button ID="btnSaveFinalize" runat="server" Text="Save and send PO to vessel(s)"
                                        OnClick="btnSaveFinalize_Click" />
                                    &nbsp;&nbsp;
                                    <asp:HyperLink ID="hlkViewFinalizedOrders" runat="server" Text="View split history"
                                        Target="_blank"></asp:HyperLink>
                                    <br />
                                    <asp:Label ID="lblmsg" runat="server" Font-Italic="true" Font-Names="verdana" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid gray">
                        <div style="max-width: 1200px; overflow-x: scroll">
                            <asp:GridView ID="gvItemsSplit" GridLines="None" AutoGenerateColumns="true" CellPadding="5"
                                runat="server" OnDataBound="gvItemsSplit_DataBound">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" Font-Size="11px" Font-Names="verdana"
                                    BackColor="White" HorizontalAlign="Right" />
                                <RowStyle CssClass="RowStyle-css" Font-Size="11px" Font-Names="verdana" HorizontalAlign="Right"
                                    BorderStyle="Solid" BorderColor="#cccccc" BorderWidth="1px" />
                                <HeaderStyle CssClass="HeaderStyle-css-teal" BorderStyle="Solid" BorderColor="#cccccc"
                                    BorderWidth="1px" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid gray">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
