<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CancelPOPreview.aspx.cs"
    Inherits="Purchase_CancelPOPreview" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="eo" Namespace="EO.Web" Assembly="EO.Web" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.8.2.js"></script>
     <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Purc_Get_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Get_Remarks_All.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Ins_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script type="text/javascript">
        function SavePDf() 
        {
            $("#hdnReport").val($("#dvReport").html().toString());
            $("#btnrp").click();
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnrp" runat="server" OnClick="btnrp_Click" />
    <div id="dvReport" runat="server" style="width: 98%; margin-left: 10px;">
    <style type="text/css">
    .colheader
    {
        background-color:#C1BCC5;
    }
    .subheader
    {
        background-color:#EEEEE;
    }
    </style>
        <div style="width: 100%">
            <asp:Panel ID="pnl1" runat="server">
                <table width="100%">
                 <tr>
                <td valign="top" style="width:20%;" align="left">
                    <asp:Image ID="Imgheader" runat="server"/>
                </td>
                <td align="left" style="width:80%;">
                    <asp:Label ID="lblCName" Font-Bold="true" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="lblCAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
                </table>
                <hr style="border-style:solid;border-width:1px;border-color:#000000" />
                
                <table width="100%">
                    <tr>
                        <td valign="top" align="center">
                            <asp:Label ID="lblHeader" Style="font-size: 22px;font-weight:bold; text-align: Center;" runat="server"
                                Text="Purchase Order"></asp:Label>
                        </td>
                    </tr>
                </table>
                <hr style="border-style:solid;border-width:1px;border-color:#000000" />
                <table width="100%">
                    <tr>
                        <td style="width: 50%">
                            <b>ISSUED TO:</b>
                        </td>
                        <td style="width: 50%">
                            <b>ISSUED BY:</b>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <font style="font-size: 12px;">
                                <asp:Label ID="lblIssueToName" runat="server" Text=""></asp:Label>
                            </font>
                            <br />
                            
                        </td>
                        <td align="left" valign="top">
                            <font style="font-size: 12px;">
                                <asp:Label ID="lblIssueByname" runat="server"></asp:Label>
                            </font>
                            <br />
                            <font style="font-size: 12px;">
                                <asp:Label ID="lblIssueByAddress" Width="190px" runat="server"></asp:Label></font>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                        </td>
                        <td align="left" valign="top">
                            PIC : <font style="font-size: 12px;">
                                <asp:Label ID="lblPICName" runat="server"  style="vertical-align:bottom"></asp:Label>
                            </font>&nbsp;&nbsp;
                            Mobile : <font style="font-size: 12px;">
                                <asp:Label ID="lblPICMobile" runat="server"  style="vertical-align:bottom"></asp:Label></font>
                        </td>
                    </tr>
                </table>
                <hr style="border-style:solid;border-width:1px;border-color:#000000" />
                    <table width="100%">
                        <tr>
                            <td valign="top" width="50%">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" style="width: 400px;">
                                            <b>ORDER PARTICULARS:</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="height: 10PX;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 250px; font-size: 12px;">
                                            <b>VESSEL NAME</b>
                                        </td>
                                        <td align="left" style="width: 250px; font-size: 12px;">
                                            <asp:Label ID="lblVesselName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 250px; font-size: 12px;">
                                            <b>REQUISITION NUMBER</b>
                                        </td>
                                        <td align="left" style="width: 250px; font-size: 12px;">
                                            <asp:Label ID="lblReqsnNo" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 150px; font-size: 12px;">
                                            <b>PO NUMBER</b>
                                        </td>
                                        <td align="left" style="width: 250px; font-size: 12px;">
                                            <asp:Label ID="lblPOCode" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 150px; font-size: 12px;">
                                            <b>PO ISSUANCE DATE</b>
                                        </td>
                                        <td align="left" style="width: 250px; font-size: 12px;">
                                            <asp:Label ID="lblPODate" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 150px; font-size: 12px;">
                                            <b>PO CURRENCY</b>
                                        </td>
                                        <td align="left" style="width: 250px; font-size: 12px;">
                                            <asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 150px; font-size: 12px;">
                                            <b>SUPPLIER REFERENCE</b>
                                        </td>
                                        <td align="left" style="width: 250px; font-size: 12px;">
                                            <asp:Label ID="lblSupplierRef" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 150px; font-size: 12px;">
                                            <b>TYPE OF EXPENSES</b>
                                        </td>
                                        <td align="left" style="width: 250px; font-size: 12px;">
                                            <asp:Label ID="lblTypeExp" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 150px; font-size: 12px;">
                                            <b>DELIVERY PORT</b>
                                        </td>
                                        <td align="left" style="width: 250px; font-size: 12px;">
                                            <asp:Label ID="lblDeliveryPort" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 150px; font-size: 12px;">
                                            <b>VESSEL ETA</b>
                                        </td>
                                        <td align="left" style="width: 250px; font-size: 12px;">
                                            <asp:Label ID="lblVesselETA" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="left" style="width: 150px; font-size: 12px;">
                                            <b>VESSEL ETD</b>
                                        </td>
                                        <td align="left" style="width: 250px; font-size: 12px;">
                                            <asp:Label ID="lblVesselETD" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 160px; font-size: 12px;">
                                            <b>MARKING ON PACKAGING</b>
                                        </td>
                                        <td align="left" style="width: 250px; font-size: 12px;">
                                            <asp:Label ID="lblmARKING" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 160px; font-size: 12px;">
                                            <b>DELIVERY INSTRUCTIONS</b>
                                        </td>
                                        <td align="left" style="width: 250px; font-size: 12px;">
                                            <asp:Label ID="lbldeliveryInst" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" width="50%">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 400px;">
                                            <b>AGENT DETAILS:</b>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td style="height: 10PX;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <asp:Label ID="lblAgentDtl" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <hr style="border-style:solid;border-width:1px;border-color:#000000" />
                        <table width="100%">
                            <tr>
                                <td valign="top" width="50%">
                                    <table width="50%">
                                        <tr>
                                            <td colspan="2" style="width: 400px;">
                                                <b>SUPPLIER QUOTATION:</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Delivery Term :</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldvTerm" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Origin :</b>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOrigin" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <hr style="border-style:solid;border-width:1px;border-color:#000000" />
                        <table width="100%">
                            <tr>
                                <td>
                                    <b>DETAILS OF PURCHASE:</b>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5PX;">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvReqsnItems" runat="server" AutoGenerateColumns="False" BorderColor="Gray"
                                        BorderStyle="Solid" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="ID"
                                        GridLines="Both" Width="100%" >
                                        <HeaderStyle CssClass="colheader" />
                                        <Columns>
                                            <asp:BoundField DataField="Subsystem_Description" HeaderStyle-Width="7%" HeaderText="Sub Catalogue" 
                                                ItemStyle-Width="7%" Visible="false" />
                                            <asp:BoundField DataField="ID" HeaderText="ItemID" Visible="false" />
                                            <asp:BoundField DataField="ID" HeaderText="Sr.No.">
                                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Description" Visible="true">
                                                <ItemTemplate>
                                                <asp:Label ID="lblItemDesc" runat="server" Text='<%#Eval("[Short_Description]")%>'></asp:Label>
                                                   <%--<asp:HyperLink ID="lblItemDesc" runat="server" NavigateUrl='<%# "~/Purchase/Item_History.aspx?vessel_code="+ Request.QueryString["Vessel_Code"].ToString()+"&item_ref_code="+Eval("ItemID").ToString() %>'
                                                        Target="_blank" Text='<%#Eval("ItemDesc")%>' Width="100%">  </asp:HyperLink>--%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Part_Number" HeaderText="Part No" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Drawing_Number" HeaderText="Drawing No" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unit_and_Packings" HeaderText="Unit">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REQUESTED_QTY" HeaderText="Reqst Qty" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QUOTED_RATE" HeaderText="Unit Price" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QUOTED_DISCOUNT" HeaderText="Discount" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Total_Price" HeaderText="Total" Visible="true">
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <br />
                         <table width="50%" align="right" cellpadding="5" cellspacing="2">
                                        <tr>
                                            <td width="25%" align="right">
                                                Total Price :
                                            </td>
                                            <td width="25%" align="right">
                                                <asp:Label ID="lblTotalPrice" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="right">
                                                Discount on Total Price :
                                            </td>
                                            <td width="25%" align="right">
                                                <asp:Label ID="lblDiscTotalPrice" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="right">
                                                Vat/GST
                                            </td>
                                            <td width="25%" align="right">
                                                <asp:Label ID="lblVat" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="right">
                                                Trucking/Freight Cost
                                            </td>
                                            <td width="25%" align="right">
                                                <asp:Label ID="lblFrieght" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="right">
                                                Pkg and handling cost
                                            </td>
                                            <td width="25%" align="right">
                                                <asp:Label ID="lblPkgHandling" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="right">
                                                Truck Cost
                                            </td>
                                            <td width="25%" align="right">
                                                <asp:Label ID="lblTruck" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="right">
                                                Barge/Workboat Cost
                                            </td>
                                            <td width="25%" align="right">
                                                <asp:Label ID="lblBargeCost" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="right">
                                                Other Cost
                                            </td>
                                            <td width="25%" align="right">
                                                <asp:Label ID="lblOtherCost" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="25%" align="right">
                                                Net Amount in
                                                <asp:Label ID="lblQuCurrency" runat="server" style="font-weight:bold"></asp:Label>
                                            </td>
                                            <td width="25%"  align="right">
                                                <asp:Label ID="lblNet" runat="server" style="font-weight:bold"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                 
                        </asp:Panel>
                        <br />
                        
                                    
                       
        </div>
        <table width="100%">
             <tr>
                <td style="height: 5PX;">
                </td>
            </tr>
             <tr>
                <td valign="top" style="text-align: left;">
                  <asp:Label ID="lblPOLegalTerm" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnHost"  ClientIDMode="Static"  runat="server" />
    <asp:HiddenField ID="hdnReport" ClientIDMode="Static" runat="server" />
 
    </form>
</body>
</html>
