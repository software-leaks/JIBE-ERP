<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Quotation_Additional_Charge.aspx.cs"
    Inherits="Travel_Quotation_Additional_Charge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript"">
        function CalculateTotal() {
        }
    </script>
    <style type="text/css">
    body
    {
        font-family:Tahoma;
        font-size:12px;
    }
    input
    {
          font-family:Tahoma;
        font-size:12px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scm1" runat="server" ></asp:ScriptManager>
   
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 0px; color: #282829; width: 900px">
        <table width="100%" style="padding-bottom: 5px; padding-top: 5px">
        <tr>
        <td style="background-color:#FFFF80">
        For clear understanding <br /> 
        1. All charges should be PER PERSON.<br />
        2. All figures in USD  <br/>
        </td>
        </tr>
            <tr>
                <td style="border-top: 1px solid #C9C9CF; border-bottom: 1px solid #C9C9CF; padding-bottom: 5px;
                    padding-top: 5px; width: 100%">
                    <asp:UpdatePanel ID="upditemlist" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found !"
                                EmptyDataRowStyle-ForeColor="Maroon" CellSpacing="1" BackColor="Gray" CellPadding="1"
                                GridLines="None" ShowFooter="true" RowStyle-VerticalAlign="Top" RowStyle-HorizontalAlign="Left" Font-Names="Tahoma">
                                <HeaderStyle BackColor="SkyBlue" ForeColor="Black" Height="30px" Font-Size="12px"
                                  Font-Bold="true" />
                                <RowStyle BackColor="WhiteSmoke" Font-Size="11px"  />
                                <FooterStyle BackColor="SkyBlue" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Description">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdfID" runat="server" Value='<%#Eval("ID")%>' />
                                            <asp:TextBox ID="txtItem" runat="server" Height="30px" TextMode="MultiLine" Width="400px" Font-Names="Tahoma"
                                                Text='<%#Eval("Item_Name") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btnAddNewItem" Text="Add new item" BackColor="#0066cc" BorderStyle="None"
                                                ForeColor="WhiteSmoke" runat="server" OnClick="btnAddNewItem_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmount" runat="server"  Height="30px"  Width="120px" Style="text-align: right"  Font-Names="Tahoma"
                                                onchange="checkNumber(id);CalculateTotal()" Text='<%#Eval("amount") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalAmount" runat="server" Font-Bold="true" Font-Size="11px" ForeColor="Blue"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemark" runat="server" Width="300px" TextMode="MultiLine" Height="30px"  Font-Names="Tahoma"
                                                Text='<%#Eval("remark") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDeleteitem" Width="12" Height="12" runat="server" AlternateText="Delete"
                                                OnClientClick="javascript:var con=confirm('Are you sure to delete this item ?'); if(con)return true;else return false;"
                                                OnClick="imgbtnDeleteitem_Click" ImageUrl="~/Images/Delete.png" CommandArgument='<%#Eval("id")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSaveItemDetails" OnClick="btnSaveItemDetails_Click" runat="server"
                        Text="Save Items" Height="30px" Width="100px" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
