<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reqsn_Vic_Meat_Percent.aspx.cs" Inherits="Purchase_Reqsn_Vic_Meat_Percent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>victualling rate/Meat Qtry/percentage of items </title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <style type="text/css">
        .ReqsnProvTitle
        {
            background-color: #5CD6FF;
            font-size: 12px;
            padding: 6px;
            text-align: left;
            font-weight: bold;
            margin-bottom: 40px;
            color: Black;
        }
        
        .HeaderStyle-css-center
        {
            text-align: center;
        }
        
        .tdh
        {
            font-family:Tahoma;
            font-size:11px;
            color:Black;
            font-weight:bold;
            padding:5px;
        }
        
         .tdd
        {
            font-family:Tahoma;
            font-size:11px;
            color:Black;
            padding:5px;
            
        }
        
    </style>
</head>
<body style="background-color: White">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td class="ReqsnProvTitle">
                    Meat Quantity - KG
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="tdh">
                                Max Limit :
                            </td>
                            <td class="tdd">
                                <asp:Label ID="lblMeatMaxLimit" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdh">
                                Current :
                            </td>
                            <td class="tdd">
                                <asp:Label ID="lblMeatCurrentQty" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="ReqsnProvTitle">
                    Percentage of requested items in each sub-catalogue
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvPercentage" runat="server" AutoGenerateColumns="false" Width="50%"
                        EmptyDataText="No record found !" EmptyDataRowStyle-ForeColor="Maroon" CssClass="gridmain-css"
                        BackColor="#D8D8D8" CellPadding="1" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="SUBSYSTEM_DESCRIPTION" HeaderText="Sub-Catalogue" />
                            <asp:BoundField DataField="PERCENT_SUBSYSTEM" HeaderText="Percentage" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="ReqsnProvTitle">
                    Victualling Rate - USD
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvVictuallingRate" runat="server" AutoGenerateColumns="false" Width="100%"
                        EmptyDataText="No record found !" EmptyDataRowStyle-ForeColor="Maroon" CssClass="gridmain-css"
                        BackColor="#D8D8D8" CellPadding="3" GridLines="None" OnRowCreated="gvVictuallingRate_RowCreated">
                        <Columns>
                            <asp:BoundField DataField="LAST_DRY_DELIVERY_DATE" HeaderText="Last Supply Date"  DataFormatString="{0:d}"/>
                            <asp:BoundField DataField="CREDIT_DEBIT_DRY_DAYS" HeaderText="Credit/debit Days" />
                            <asp:BoundField DataField="CREDIT_DEBIT_DRY_AMOUNT" HeaderText="Credit/debit Amount" />
                            <asp:BoundField DataField="ORDER_DRY_AMOUNT" HeaderText="Current Amount" />
                            <asp:BoundField DataField="LAST_FRESH_DELIVERY_DATE" HeaderText="Last Supply Date" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="CREDIT_DEBIT_FRESH_DAYS" HeaderText="Credit/debit Days" />
                            <asp:BoundField DataField="CREDIT_DEBIT_FRESH_AMOUNT" HeaderText="Credit/debit Amount" />
                            <asp:BoundField DataField="ORDER_FRESH_AMOUNT" HeaderText="Current Amount" />
                            <asp:BoundField DataField="CREW_COUNT" HeaderText="Crew OB" />
                            <asp:BoundField DataField="ALLOWANCE_STOCK_AMOUNT" HeaderText="5days stock" />
                            <asp:BoundField DataField="EXTRA_MEAL_AMOUNT" HeaderText="Extra Meal Amount" />
                            <asp:BoundField DataField="ORDER_VICTULING_RATE" HeaderText="Victualling Rate" ItemStyle-Font-Bold="true"
                                ItemStyle-ForeColor="Blue" />
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <RowStyle CssClass="RowStyle-css" HorizontalAlign="Center" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
