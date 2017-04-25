<%@ Page Title="Split Order Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PURC_Split_Reqsn_BulkPurchase_Report.aspx.cs" Inherits="Purchase_PURC_Split_Reqsn_BulkPurchase_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />

    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .txtord
        {
            font-size: 11px;
            width: 50px;
            text-align: center;
           
        }
        
        .remainingQty
        {
            background-color: #FFE0BA;
        }
        .remainingPrice
        {
            background-color: #FFE0BA;
        }
        
        .HeaderStyle-css-bulkreport
        {
            background: url(../Images/gridheaderbg-image.png) left -5px repeat-x;
            color: #333333;
            background-color: #ADD8E6;
            font-size: 11px;
            padding: 2px;
            text-align: center;
            border: 1px solid #cccccc;
            border-collapse: collapse;
        }
    </style>
    <script type="text/javascript" language="javascript">
        var lastfixedColumnID = 7;
        var remainingQtyColumnID = 4;
        var remainingPriceColumnID = 7;
        var totalQtyColumnID = 3;
        var unitPriceColumnID = 5;


        function calculatePriceAndQty_Onload() {
            var griditems = document.getElementById("ctl00_MainContent_gvItemsSplit");
            var totalAssignedqty = 0;

            var cellscount = lastfixedColumnID + (griditems.rows[0].cells.length - lastfixedColumnID) * 2;
            for (r = 2; r <= griditems.rows.length - 1; r++) {
                totalAssignedqty = 0;
                var prefixid = (r < 10) ? "ctl00_MainContent_gvItemsSplit_ctl0" + r.toString() : "ctl00_MainContent_gvItemsSplit_ctl" + r.toString();
                for (var i = lastfixedColumnID + 1; i <= cellscount; i = i + 2) {

                    var locAssdQty = document.getElementById(prefixid + "_" + i.toString()).innerHTML;
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
    <table align="center" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="background-color: #808080; font-size: small; color: #FFFFFF; text-align: center;
                font-weight: bold">
                Bulk Purchase Order Details
            </td>
        </tr>
    </table>
    <table style="border: 1px solid #cccccc;" width="100%" >
        <tr>
            <td>
                <table align="center" width="1200px" cellpadding="2" cellspacing="0" style="background-color: #f4ffff;
                    color: Black;border-collapse:collapse">
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
            </td>
        </tr>
        <tr>
            <td>
                <div style="max-width:1200px; overflow:scroll">
                    <asp:GridView ID="gvItemsSplit" GridLines="None" AutoGenerateColumns="true" CellPadding="8"
                        runat="server" OnDataBound="gvItemsSplit_DataBound" OnRowCreated="gvItemsSplit_RowCreated">
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" Font-Size="11px" Font-Names="verdana"
                            BackColor="White" HorizontalAlign="Center" />
                        <RowStyle CssClass="RowStyle-css" Font-Size="11px" Font-Names="verdana" HorizontalAlign="Center"
                            BorderStyle="Solid" BorderColor="#cccccc" BorderWidth="1px" />
                        <HeaderStyle CssClass="HeaderStyle-css-bulkreport" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
  
    </table>
</asp:Content>
