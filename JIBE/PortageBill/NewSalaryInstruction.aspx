<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewSalaryInstruction.aspx.cs"
    Inherits="PortageBill_NewSalaryInstruction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        select
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        input
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scr" runat="server" />
    <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
    <div class="error-message" onclick="javascript:this.style.display='none';">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </div>
    <asp:Panel ID="pnlViewInstructions" runat="server">
        <asp:GridView ID="GridView_SalaryInstructions" runat="server" AllowSorting="True"
            AutoGenerateColumns="false" BorderStyle="Solid" ForeColor="#333333" GridLines="Both"
            Width="100%" BorderColor="#CCCCCC" CssClass="grid">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblVessel" runat="server" Text='<%#Eval("Vessel_Short_Name")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblRank_Short_Name" runat="server" Text='<%#Eval("Rank_Short_Name")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PB Date" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblPBDate" runat="server" Text='<%#Eval("PBill_Date","{0:dd/MM/yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created By" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created Date" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblDate_Of_Creation" runat="server" Text='<%#Eval("Date_Of_Creation","{0:dd/MM/yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Earning/Deduction" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblSalaryType" runat="server" Text='<%#Eval("SalaryType")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblSalaryName" runat="server" Text='<%#Eval("SalaryName")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="lblamount" runat="server" Text='<%#Eval("amount","{0:00.00}")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("remarks")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#58FA82" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF6FC" ForeColor="#333333" CssClass="grid-row" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="pnlAddNew" runat="server">
    <div style="position: relative; text-align: left">
        <table>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:RadioButtonList ID="rdoEarndeduction" runat="server" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="rdoEarndeduction_SelectedIndexChanged" Width="306px">
                        <asp:ListItem Text="Earning" Value="18" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Deduction" Value="19"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="width: 150px">
                    PortageBill date
                </td>
                <td>
                    <asp:TextBox ID="txtPBDate" runat="server" Width="150px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtPBDate"
                        Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Type
                </td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server" Width="156px">
                        <asp:ListItem Text="Office Instructions" Value="40"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <%--<tr>
                <td>
                    Fleet
                </td>
                <td>
                    <asp:DropDownList ID="ddlFleet" runat="server" Width="156px">
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td>
                    Vessel
                </td>
                <td>
                    <asp:DropDownList ID="ddlVessel" runat="server" Width="156px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Amount(USD)
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Remarks
                </td>
                <td>
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="300px" Height="60px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right">
                    <asp:Button ID="btnSave" runat="server" Text=" Save " OnClick="btnSave_Click" />
                    <input type="button" value="Cancel" onclick="window.parent.hideModal('dvPopupFrame');" />
                </td>
            </tr>
        </table>
    </div>
    </asp:Panel>
    
    </form>
</body>
</html>
