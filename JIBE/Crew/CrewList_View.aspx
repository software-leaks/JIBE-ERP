<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewList_View.aspx.cs" Inherits="Crew_CrewList_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%--<script src="../Scripts/jquery.min.js" type="text/javascript"></script>--%>
<script src="../Scripts/Common_functions.js" type="text/javascript"></script>
<script src="../Scripts/StaffInfo.js" type="text/javascript"></script>
    <title>Crew List</title>
    <style type="text/css">
        body
        {
            font-family: Tahoma;
            font-size: 12px;
        }
    </style>
    <script type="text/javascript">
        function PrintDiv(dvID) {
            var a = window.open('', '', 'left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
            a.document.write(document.getElementById(dvID).innerHTML);
            a.document.close();
            a.focus();
            a.print();
            a.close();
            return false;
        }
    </script>
</head>
<body style="padding: 10; margin: 10;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="font-family: Tahoma; font-size: 14px; margin-bottom: 15px; font-weight: bold;
                width: 100%;">
                <tr>
                    <td>
                        Vessel:
                        <asp:Label ID="lblVesselName" runat="server"></asp:Label>
                        
                    </td>
                    <td style="text-align: right">
                        <img src="../Images/Printer.png" title="*Print*" style="cursor: pointer;" alt="" onclick="PrintDiv('page-content')" />
                        &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <div id="page-content">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                    Width="100%" EmptyDataText="No Record Found" CaptionAlign="Bottom" GridLines="Horizontal"
                    DataKeyNames="ID" Font-Size="12px" AllowSorting="false" CssClass="Grid_CSS">
                    <Columns>
                        <asp:TemplateField HeaderText="Staff Code" SortExpression="STAFF_CODE" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>                                                                    
                                    <asp:Label ID="lblStaffCode" runat="server" Text='<%# Eval("staff_Code")%>' class="staffInfo"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rank" SortExpression="Rank_Name" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Name")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" SortExpression="Staff_FullName" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblSTAFF_NAME" runat="server" Text='<%# Eval("Staff_FullName")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DOB" SortExpression="STAFF_BIRTH_DATE" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSTAFF_BIRTH_DATE" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("STAFF_BIRTH_DATE"))) %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nationality" SortExpression="ISO_Code" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSTAFF_NATIONALITY" runat="server" Text='<%# Eval("ISO_Code")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="D.O.J" SortExpression="sign_on_date" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblsign_on_date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("sign_on_date"))) %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EOC Date" SortExpression="Est_Sing_Off_Date" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblEst_Sing_Off_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Est_Sing_Off_Date"))) %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Passport No" SortExpression="Passport_Number">
                            <ItemTemplate>
                                <asp:Label ID="lblPassport_Number" runat="server" Text='<%# Eval("Passport_Number")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px"  HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PP. Expiry Date" SortExpression="Passport_Expiry_Date">
                            <ItemTemplate>
                                <asp:Label ID="lblPassport_Expiry_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Passport_Expiry_Date"))) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px"  HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                    <PagerStyle Font-Size="Larger" CssClass="pager" BackColor="#336666" ForeColor="White"
                        HorizontalAlign="Center" />
                    <RowStyle CssClass="GridRow_CSS" BackColor="White" ForeColor="#333333" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
