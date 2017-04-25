<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Crew_ToolTip.aspx.cs" Inherits="Crew_Crew_ToolTip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div  id="MainDiv" class="tooltip-content" style="font-family:Tahoma;font-size:11px;">
        <div style="float: right; background-color: Yellow;">
            <asp:ImageButton ID="imgCardStatus" ImageUrl="" runat="server" ImageAlign="AbsMiddle" Visible="false" />
            <asp:Label ID="lblCardStatus" runat="server"></asp:Label>
        </div>

        <table>
            <tr>
                <td style="font-weight: bold">
                    Last Vessel:
                                  
                    <asp:Label ID="lblLastVessel" runat="server"></asp:Label>&nbsp; &nbsp;
                    Signed ON:
                    <asp:Label ID="lblLastSignedOn" runat="server"></asp:Label>&nbsp; &nbsp;
                    Signed OFF:
                    <asp:Label ID="lblLastSignedOff" runat="server"></asp:Label>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td style="font-weight: bold">
                    Last 2 remarks:
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView_CrewRemarks" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="ID" AllowPaging="True" PageSize="15" AllowSorting="True" BackColor="White"
                        BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="None"
                        CellSpacing="1" Width="100%" Font-Size="11px">
                        <Columns>
                            <asp:TemplateField HeaderText="Date">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_of_creation"))) %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_of_creation"))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Posted By">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Feedback/Note">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Remark").ToString().Replace("\n","<br>") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Remark") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a href='../Uploads/CrewDocuments/<%# Eval("AttachmentPath") %>' target="_blank">
                                        <img src='../images/<%# Eval("AttachmentIcon") %>' style="border: 0" /></a>
                                </ItemTemplate>
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                        <AlternatingRowStyle BackColor="#eFeFeF" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#594B9C" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#33276A" />
                    </asp:GridView>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td style="font-weight: bold">
                    Complaints escalated by the staff:
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <asp:Repeater ID="rptMyComplaints" runat="server">
                            <HeaderTemplate>
                                <table style="width: 100%; border-collapse: collapse" border="1" cellpadding="2"
                                    cellspacing="0">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 100px">
                                        Complaint
                                    </td>
                                    <td colspan="6">
                                        <asp:Label ID="lblJOB_DESCRIPTION" runat="server" Text='<%# Bind("JOB_DESCRIPTION") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Vessel
                                    </td>
                                    <td colspan="6">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("vessel_name") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status
                                    </td>
                                    <td colspan="6">
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr style="background-color: #627AA8; color: Aqua; font-weight: bold; text-align: center;">
                                    <td>
                                        <asp:Label ID="lbl1" runat="server" Text="Escalated On"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="lbl2" runat="server" Text="Escalated By"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="lbl3" runat="server" Text="Escalated To"></asp:Label>
                                    </td>
                                </tr>
                                <asp:Repeater runat="server" ID="rptLog" DataSource='<%# ((System.Data.DataRowView) Container.DataItem).Row.GetChildRows("MyLog") %>'>
                                    <ItemTemplate>
                                        <tr class='<%# (((System.Data.DataRow)Container.DataItem)["Escalated_By"].ToString()==GetCrewID().ToString())?"required":"not-selected"%>'
                                            style="text-align: center;">
                                            <td>
                                                <%# ((System.Data.DataRow)Container.DataItem)["ESCALATED_ON"]%>
                                            </td>
                                            <td>
                                                <a href='CrewDetails.aspx?ID=<%# ((System.Data.DataRow) Container.DataItem)["Escalated_By"] %>'
                                                    target="_blank">
                                                    <%# ((System.Data.DataRow)Container.DataItem)["Escalated_By_Staff_Code"]%></a>
                                            </td>
                                            <td>
                                                <%# ((System.Data.DataRow)Container.DataItem)["Escalated_By_Rank"]%>
                                            </td>
                                            <td style="text-align: left;">
                                                <%# ((System.Data.DataRow)Container.DataItem)["Escalated_by_Name"]%>
                                            </td>
                                            <td>
                                                <a href='CrewDetails.aspx?ID=<%# ((System.Data.DataRow) Container.DataItem)["Escalated_To"] %>'
                                                    target="_blank">
                                                    <%# ((System.Data.DataRow)Container.DataItem)["Escalated_To_Staff_Code"]%></a>
                                            </td>
                                            <td>
                                                <%# ((System.Data.DataRow)Container.DataItem)["Escalated_To_Rank"]%>
                                            </td>
                                            <td style="text-align: left;">
                                                <%# ((System.Data.DataRow)Container.DataItem)["Escalated_To_Name"]%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
