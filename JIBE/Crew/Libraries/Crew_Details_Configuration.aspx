<%@ Page Title="Crew Details Configuration" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Crew_Details_Configuration.aspx.cs" Inherits="Crew_Crew_Details_Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function checkMutuallyExclusive(target1, target2) {

            if (document.getElementById(target1).checked == false) {
                document.getElementById(target1).checked = true;
                document.getElementById(target2).checked = false;
            }
            else if (document.getElementById(target2).checked == false) {
                document.getElementById(target2).checked = true;
                document.getElementById(target1).checked = false;
            }
            else if (document.getElementById(target1).checked == true) {
                document.getElementById(target1).checked = false;
                document.getElementById(target2).checked = true;
            }
            else if (document.getElementById(target2).checked == true) {
                document.getElementById(target2).checked = false;
                document.getElementById(target1).checked = true;
            }
        }
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(" Changes to the Crew Details will affect workflows and the hiring process. Are you sure you want to continue?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
        height: 100%;">
        <div class="page-title" style="margin-bottom: 10px;">
            Crew Details Configuration
        </div>
        <div align="center">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3"
                CellSpacing="0" Width="60%" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                GridLines="None" DataKeyNames="ID" AllowPaging="false" CssClass="gridmain-css"
                OnRowDataBound="GridView1_RowDataBound">
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                <PagerStyle CssClass="PMSPagerStyle-css" />
                <SelectedRowStyle BackColor="#FFFFCC" />
                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                <Columns>
                    <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblKey" runat="server" Text='<%# Eval("KEY")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Field" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="71%">
                        <ItemTemplate>
                            <asp:Label ID="lblDisplayName" Width="150px" runat="server" Text='<%# Eval("DisplayName")%>'></asp:Label>
                            <asp:TextBox ID="txtDisplay" Width="520px" MaxLength="100" runat="server" Visible="false"
                                Text='<%# Eval("DisplayName")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Display" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkDisplay" runat="server" Checked='<%# Convert.ToBoolean(Eval("Display")) %>' />
                            <asp:RadioButton ID="rdbDisplay" runat="server" AutoPostBack="true" OnCheckedChanged="rdbDisplay_checked"
                                Visible="false" Checked='<%# Convert.ToBoolean(Eval("Display")) %>' />
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confidential" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkConfidential" runat="server" Checked='<%# Convert.ToBoolean(Eval("Confidential")) %>' />
                            <asp:RadioButton ID="rdbConf" runat="server" Visible="false" AutoPostBack="true"
                                OnCheckedChanged="rdbConf_checked" Checked='<%# Convert.ToBoolean(Eval("Confidential")) %>' />
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblEdit" runat="server" Text='<%# Eval("IsEditable")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgEdit" runat="server" ImageUrl="~/Images/edit.gif" OnClick="ImgEdit_Click"
                                ToolTip="Edit" />
                            <asp:ImageButton ID="ImgSave" Visible="false" OnClick="ImgSave_Click" runat="server"
                                ImageUrl="~/Images/save.gif" ToolTip="Save" />
                            <asp:ImageButton ID="ImgCancel" Visible="false" OnClick="ImgCancel_Click" runat="server"
                                ImageUrl="~/Images/delete.gif" ToolTip="Cancel" />
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <br />
            <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" OnClientClick="Confirm()" />
        </div>
    </div>
</asp:Content>
