<%@ Page Title="Allotment" Language="C#" AutoEventWireup="true" CodeFile="NewAllotment.aspx.cs"
    Inherits="PortageBill_NewAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add New Allotment</title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <script src="../Scripts/drag.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/StaffInfo.js" type="text/javascript"></script>
</head>
<body style="font-family: Tahoma; font-size: 11px">
    <form id="frmNewPax" runat="server">
    <asp:ScriptManager ID="smp1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">
                <div class="error-message" onclick="javascript:this.style.display='none';">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
                <asp:Panel ID="pnlAllotment" runat="server">
                    <table>
                        <tr>
                            <td>
                                Search Staff:
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearchText" runat="server" OnTextChanged="txtSearchText_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td style="width: 100px">
                            </td>
                            <td>
                                PortageBill Date:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMonth" runat="server">
                                    <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlYear" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvSelectedCrew" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#336666" BorderStyle="Double" BorderWidth="0px" CellPadding="2"
                        AllowPaging="false" PageSize="15" GridLines="Horizontal" Width="100%" DataKeyNames="ID"
                        AllowSorting="true" Font-Size="11px" CssClass="GridCSS" OnRowDataBound="gvSelectedCrew_RowDataBound"
                        OnRowCommand="gvSelectedCrew_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="S/Code" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnCrewID" runat="server" Value='<%# Eval("ID")%>' />
                                    <a href='../Crew/CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                        <%# Eval("STAFF_CODE")%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSTAFFNAME" runat="server" Text='<%# Eval("staff_name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Account" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlBankAcc" runat="server" DataTextField="Acc_NO" DataValueField="ID"
                                        AppendDataBoundItems="false" Width="150px">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Voyage" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%# Bindvoyage_name(Convert.ToString(Eval("voyage_name")))%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server" Width="80px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Is Second Allotment ?" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:CheckBox ID="chkIsSpecial" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnSelect" runat="server" CommandName="ADDSTAFF" CommandArgument='<%#Eval("ID").ToString()+","+Eval("Voyage_ID").ToString()+","+Eval("Vessel_ID").ToString() %>'
                                        Text="Add" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoRec" runat="server" Text="No staff found"></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <PagerStyle CssClass="PagerStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <EditRowStyle CssClass="EditRowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                    </asp:GridView>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
