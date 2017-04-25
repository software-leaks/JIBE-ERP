<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNewPax.aspx.cs" Inherits="AddNewPax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Pax</title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <script src="../Scripts/drag.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Validate() {
            if (!confirm("sure to add this pax"))
                return false;
        }
    </script>
</head>
<body style="font-family: Tahoma; font-size: 11px">
    <form id="frmNewPax" runat="server">
    <asp:ScriptManager ID="smp1" runat="server">
    </asp:ScriptManager>    
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="cmdGet">
                <table cellpadding="1" cellspacing="1" style="text-align: left; width: 100%; background-color: GhostWhite;">
                    <caption style="background-color: #32426F; font-weight: bold; color: White;">
                        Search - Staff code/Name/Rank
                    </caption>
                    <tr style="font-weight: bold;">
                        <td>
                            <span>Search</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearch" runat="server" Text="" Width="260px" Height="15px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="cmdGet" runat="server" Width="100px" Text="Go->" OnClick="cmdGet_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <asp:GridView ID="GrdCrew" Width="100%" AutoGenerateColumns="false"
                OnRowCommand="GrdCrew_RowCommand" runat="server">
                <HeaderStyle HorizontalAlign="Left" BackColor="Gray" ForeColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Code">
                        <ItemTemplate>
                            <asp:Label ID="lblCode" onclick="alert('fine');" runat="server" Text='<%#Eval("Staff_Code") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Staff_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rank">
                        <ItemTemplate>
                            <asp:Label ID="lblRank" runat="server" Text='<%#Eval("Rank_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Passport No">
                        <ItemTemplate>
                            <asp:Label ID="lblPPNo" runat="server" Text='<%#Eval("Passport_Number") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expiry Date">
                        <ItemTemplate>
                            <asp:Label ID="lblPPExpiry" runat="server" Text='<%#Eval("Passport_Expiry_Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PlaceOf Issue">
                        <ItemTemplate>
                            <asp:Label ID="lblPPIssue" runat="server" Text='<%#Eval("Passport_PlaceOf_Issue") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nationality">
                        <ItemTemplate>
                            <asp:Label ID="lblNationality" runat="server" Text='<%#Eval("Staff_Nationality") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DOB">
                        <ItemTemplate>
                            <asp:Label ID="lblDOB" runat="server" Text='<%#Eval("Staff_Birth_Date", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Add">
                        <ItemTemplate>
                            <asp:Button ID="cmdAddPax" CommandName="AddPax" OnClientClick="return Validate();"
                                CommandArgument='<%#Eval("id") %>' runat="server"
                                Text="Add" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
