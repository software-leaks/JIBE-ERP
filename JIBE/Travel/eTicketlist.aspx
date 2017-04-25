<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eTicketlist.aspx.cs" Inherits="Travel_eTicketlist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E-Ticket List</title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <script src="../Scripts/drag.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmETicket" runat="server">
    <center>
        <h2>
            E-Ticket List</h2>
    </center>
    <div>
        <br />
        <asp:GridView ID="grdTickets" Width="350px" runat="server" AutoGenerateColumns="False">
            <HeaderStyle HorizontalAlign="Left" BackColor="Gray" Font-Bold="true" ForeColor="White" />
            <RowStyle CssClass="grid-row-data" BackColor="White" />
            <SelectedRowStyle BackColor="LightGray" ForeColor="Blue" />
            <Columns>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="E-Ticket No.">
                    <ItemTemplate>
                        <asp:Label ID="lblTicketNo" runat="server" Text='<%#Eval("eTicketNumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
