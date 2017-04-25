<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemSepcificallyView.aspx.cs" Inherits="Technical_INV_ItemSepcificallyView" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Item Specifically</title>
    <style type="text/css">


.RadGrid_WebBlue
{
	border:1px solid #898d8f;
	border-top:0;
}

.RadGrid_WebBlue
{
	font:11px arial,tahoma,sans-serif;
}

.RadGrid_WebBlue
{
	background:#bbc1c9;
	color:#000;
	
	scrollbar-face-color:#bbc1c9; 
	scrollbar-highlight-color:#bbc1c9; 
	scrollbar-shadow-color:#bbc1c9; 
	scrollbar-3dlight-color:#d2d6db; 
	scrollbar-arrow-color:#333; 
	scrollbar-track-color:#d2d6db;
	scrollbar-darkshadow-color:#d2d6db; 
}

.RadGrid_WebBlue
{
	border:1px solid #898d8f;
	border-top:0;
}

.RadGrid_WebBlue
{
	font:11px arial,tahoma,sans-serif;
}

.RadGrid_WebBlue
{
	background:#bbc1c9;
	color:#000;
	
	scrollbar-face-color:#bbc1c9; 
	scrollbar-highlight-color:#bbc1c9; 
	scrollbar-shadow-color:#bbc1c9; 
	scrollbar-3dlight-color:#d2d6db; 
	scrollbar-arrow-color:#333; 
	scrollbar-track-color:#d2d6db;
	scrollbar-darkshadow-color:#d2d6db; 
}

.MasterTable_WebBlue
{
	border-collapse:separate !important;
}

.MasterTable_WebBlue
{
	font:11px arial,tahoma,sans-serif;
}

.MasterTable_WebBlue
{
	border-collapse:separate !important;
}

.MasterTable_WebBlue
{
	font:11px arial,tahoma,sans-serif;
}

.GridHeader_WebBlue
{
	color:#fff;
	text-decoration:none;
}

.GridHeader_WebBlue
{
	border-top:1px solid #d8dce0;
	border-bottom:1px solid #90979e;
	padding:5px 3px 5px 2px;
	background:#bbc6d2 url('mvwres://Telerik.Web.UI, Version=2008.1.415.20, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.WebBlue.Grid.sprite.gif') 0 0 repeat-x;
	text-align:left;
}

.GridHeader_WebBlue
{
	color:#fff;
	text-decoration:none;
}

.GridHeader_WebBlue
{
	border-top:1px solid #d8dce0;
	border-bottom:1px solid #90979e;
	padding:5px 3px 5px 2px;
	background:#bbc6d2 url('mvwres://Telerik.Web.UI, Version=2008.1.415.20, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.WebBlue.Grid.sprite.gif') 0 0 repeat-x;
	text-align:left;
}


.GridFooter_WebBlue
{
	background:#e0e3e6;
	color:#333;
}


.GridFooter_WebBlue
{
	background:#e0e3e6;
	color:#333;
}

.GridPager_WebBlue
{
	background:#d2d6db;
	line-height:24px;
}

.GridPager_WebBlue
{
	background:#d2d6db;
	line-height:24px;
}

.PagerLeft_WebBlue
{
	float:left;
}

.PagerLeft_WebBlue
{
	float:left;
}

.PagerRight_WebBlue
{
	float:right;
}

.PagerRight_WebBlue
{
	float:right;
}

.GridRow_WebBlue
{
	background:#f0f2f4;
}

.GridRow_WebBlue
{
	background:#f0f2f4;
}

.GridAltRow_WebBlue
{
	background:#fff;
}

.GridAltRow_WebBlue
{
	background:#fff;
}

        .style4
        {
            height: 11px;
        }
        .style9
        {
            width: 1px;
            height: 17px;
        }

        .style12
        {
            width: 133px;
            height: 17px;
        }
        .style16
        {
            width: 88px;
            height: 17px;
        }
        .style17
        {
            width: 4px;
            height: 17px;
        }
        .style18
        {
            width: 131px;
            height: 17px;
        }
        .style19
        {
            width: 88px;
            height: 11px;
        }
        .style20
        {
            width: 1px;
            height: 11px;
        }
        .style21
        {
            width: 131px;
            height: 11px;
        }

    </style>
</head>
<body>
    <form id="form2" runat="server">
    <div style="position: absolute; left: 0%; top: 0%;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
         <table cellpadding="0" cellspacing="0" style="font-family:Verdana"><tr><td align="center" style="font-size:small; background-color:#808080; color:#FFFFFF"> 
             OFFERED QUALITY DETAILS </td></tr>
         <tr><td> 
             <table cellpadding="0" cellspacing="0"  align="left" style="border: 1px solid #FFFFFF; left:2%; right:2%; width:100%; background-color: #999999;
                        height: 50px;">
                        <tr align="left" style="background-color: #C0C0C0">
                            <td align="left" style="font-size: small; color: #333333; font-weight: 700; " 
                                class="style16">
                                Requisition No
                            </td>
                           <td align="left" 
                                style="font-size: small; color: #333333; font-weight: bold; " 
                                class="style17">
                            :
                            </td> 
                            <td style="font-size: small; color: #333333; " class="style12">
                                <b>
                                    <asp:Label ID="lblReqNo" runat="server"></asp:Label>
                                </b>
                            </td>
                       <%-- </tr>
                        <tr align="left" style="background-color: #C0C0C0">--%>
                            <td style="font-size: small;  color: #333333; " 
                                class="style16">
                                <b>Vessel </b>
                            </td>
                          <td align="left" 
                                
                                
                                style="font-size: small;  color: #333333; font-weight: bold; " 
                                class="style17">
                            :
                            </td>
                            <td style="font-size: small;  color: #333333;
                                " class="style18">
                                <b><span style="font-weight: normal">
                                    <asp:Label ID="lblVessel" runat="server" Style="font-weight: 700"></asp:Label>
                                </span></b>
                            </td>
                       <%-- </tr>
                        <tr align="left" style="background-color: #C0C0C0">--%>
                            <td style="font-size: small; background-color: #C0C0C0; color: #333333;" 
                                class="style16">
                                <b style="color: #333333">Catalogue </b>
                            </td>
                            <td align="left" 
                                
                                
                                style="font-size: small; background-color: #C0C0C0; color: #333333; font-weight: bold; " 
                                class="style17">
                            :
                            </td>
                            <td style="font-size: small; background-color: #C0C0C0; color: #333333;
                                " class="style18">
                                <b><span style="font-weight: normal">
                                    <asp:Label ID="lblCatalog" runat="server" Style="font-weight: 700"></asp:Label>
                                </span></b>
                            </td>
                        </tr>
                        <tr align="left" style="background-color: #CCCCCC">
                            <td style="font-size: small;  color: #333333;" 
                                class="style16">
                                <b style="color: #333333">Total Item </b>
                            </td>
                       <td align="left" 
                                
                                style="font-size: small; color: #333333; font-weight: bold; " 
                                class="style17">
                            :
                            </td>
                            <td style="font-size: small;  color: #333333;
                                " class="style18">
                                <b><span style="font-weight: normal">
                                    <asp:Label ID="lblTotalItem" runat="server" Style="font-weight: 700"></asp:Label>
                                </span></b>
                            </td>
                       <%-- </tr>
                        
                        <tr align="left" style="background-color: #C0C0C0">--%>
                            <td style="font-size: small;  color: #333333;" 
                                class="style16">
                           <b style="color: #333333"> Req. Date </b>
                            </td>
                            <td style="font-size: small;  color: #333333;
                                " class="style9"> :
                            </td>
                             <td style="font-size: small;  color: #333333;
                                " class="style18">
                               <b><span style="font-weight: normal">
                                <asp:Label ID="lblReqDate" runat="server" Style="font-weight: 700"></asp:Label>
                                </span></b>
                            </td>
                      <%--  </tr>
                        
                        <tr align="left" style="background-color: #C0C0C0">--%>
                            <td style="font-size: small;   color: #333333;" class="style16">
                                <b style="color: #333333">RFQ Date </b>
                            </td>
                        <td align="left" 
                                
                                style="font-size: small; color: #333333; font-weight: bold; " 
                                class="style17">
                            :
                            </td>
                            <td style="font-size: small;  color: #333333;
                                " class="style18">
                                <b><span style="font-weight: normal">
                                    <asp:Label ID="lblToDate" runat="server" Style="font-weight: 700"></asp:Label>
                                </span></b>
                            </td>
                        </tr>
                        <tr align="left" style="background-color: #C0C0C0">
                            <td style="font-size: small; color: #333333;" 
                                class="style19">
                                <b style="color: #333333">Qtn. Due Date</b></td>
                            <td style="font-size: small;  color: #333333;
                                " class="style20">
                                :</td>
                             <td style="font-size: small;  color: #333333;
                                " class="style21">
                                <b><span style="font-weight: normal">
                                    <asp:Label ID="lblQuotDueDate" runat="server" Style="font-weight: 700"></asp:Label>
                                </span></b>
                            </td>
                            <td colspan ="6" class="style4"></td>
                        </tr>
                        
                    </table>
         </td></tr><tr><td>
             <telerik:RadGrid ID="rgdItmSpecView" 
    runat="server" AllowAutomaticInserts="True"
                                        AllowPaging="True" GridLines="None" 
    Skin="WebBlue" Width="900px"  
                                        
    OnNeedDataSource="rgdItmSpecView_NeedDataSource" PageSize="500" 
                                        AutoGenerateColumns="False">
                 <MasterTableView>
                     <RowIndicatorColumn Visible="False">
                         <HeaderStyle HorizontalAlign="Center" Width="20px" />
                     </RowIndicatorColumn>
                     <ExpandCollapseColumn Resizable="False" Visible="False">
                         <HeaderStyle Width="20px" />
                     </ExpandCollapseColumn>
                     <Columns>
                         <telerik:GridBoundColumn DataField="ITEM_REF_CODE" HeaderText="ID" UniqueName="ITEM_REF_CODE"
                                                    Display="false" AllowFiltering="false">
                         </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="ITEM_SERIAL_NO" HeaderText="Req SN" UniqueName="ITEM_SERIAL_NO"
                                                    Visible="True" AllowFiltering="false">
                             <ItemStyle   HorizontalAlign="Right"   />
                         </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="QUOTATION_CODE" HeaderText="Quatation No." UniqueName="QUOTATION_CODE"
                                                     Display="false"  AllowFiltering="false" >
                             <ItemStyle   Width="100px" />
                         </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="Drawing_Number" HeaderText="Drawing No." UniqueName="Drawing_Number"
                                                    Visible="True" AllowFiltering="false">
                         </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="Part_Number" HeaderText="Part No." UniqueName="Part_Number"
                                                    Visible="True" AllowFiltering="false">
                         </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="ITEM_SHORT_DESC" HeaderText="Item Name" UniqueName="ITEM_SHORT_DESC"
                                                    Visible="True" AllowFiltering="false">
                             <ItemStyle Width="200px" />
                         </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="Unit_and_Packings" HeaderText="Unit" UniqueName="Unit_and_Packings"
                                                    Visible="True" AllowFiltering="false">
                             <ItemStyle Width="80px" HorizontalAlign="Center"   />
                         </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="ROB_QTY" HeaderText="ROB" UniqueName="ROB_QTY"
                                                    Visible="False" AllowFiltering="false">
                             <ItemStyle  BackColor="#ffe1e1"  Width="80px"  HorizontalAlign="Right" />
                         </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="QUOTED_QTY" HeaderText="Reqst Qty" UniqueName="QUOTED_QTY"
                                                    Visible="False" AllowFiltering="false">
                             <ItemStyle Width="80px"  HorizontalAlign="Right" />
                         </telerik:GridBoundColumn>
                         <%-- <telerik:GridBoundColumn DataField="QUOTED_QTY" HeaderText="Order Qty" UniqueName="Order_qty"
                                                    Visible="True" AllowFiltering="false">
                                                    <ItemStyle  BackColor="#ffe1e1"  Width="80px"  />
                                                </telerik:GridBoundColumn>--%>
                       <%--  <telerik:GridTemplateColumn AllowFiltering="false" Visible="False" 
                             HeaderText="Order Qty" UniqueName="Order_qty1">
                             <ItemTemplate>
                                 <asp:TextBox ID="txtORqty" runat ="server" Text='<%#Eval("QUOTED_QTY")%>' Width ="40px" Font-Size="XX-Small"  ></asp:TextBox>
                             </ItemTemplate>
                         </telerik:GridTemplateColumn>--%>
                        <%-- <telerik:GridBoundColumn HeaderText="SupplierName" UniqueName="column">
                         </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn HeaderText="Item Type" UniqueName="column1">
                         </telerik:GridBoundColumn>--%>
                        <%-- <telerik:GridTemplateColumn>
                         <HeaderTemplate>
                         <TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" border="1">
                            <TR>
                             <TD colspan="2" align="center"><b>Address</b></TD>
                            </TR>
                            <TR>
                             <TD width="50%"><b>City</b></TD>
                             <TD width="50%"><b>Postal code</b></TD>
                            </TR>
                           </TABLE>
                          </HeaderTemplate>
                          <ItemTemplate>
                           <TABLE id="Table2" cellSpacing="1" cellPadding="1" width="300" border="1">
                            <TR>
                             <TD width="50%"><%# DataBinder.Eval(Container.DataItem, "City") %></TD>
                             <TD width="50%"><%# DataBinder.Eval(Container.DataItem, "PostalCode") %></TD>
                            </TR>
                           </TABLE>
                          </ItemTemplate>

                         </telerik:GridTemplateColumn>--%>
                     </Columns>
                     <EditFormSettings>
                         <PopUpSettings ScrollBars="None" />
                     </EditFormSettings>
                 </MasterTableView>
             </telerik:RadGrid>
             </td></tr> </table>
         </ContentTemplate>
     </asp:UpdatePanel>
      </div>
    </form>
   
    
</body>
</html>
