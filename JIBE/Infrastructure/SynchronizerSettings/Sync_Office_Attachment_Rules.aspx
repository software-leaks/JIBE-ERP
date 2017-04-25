<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Sync_Office_Attachment_Rules.aspx.cs"
    Inherits="SynchronizerSettings_Import_From_Vessels_Attachment_Rules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script type="text/javascript">

        function EditRules(Rule_ID, Prefix, Path) {

            document.getElementById('<%=hdfRule_ID.ClientID%>').value = Rule_ID
            document.getElementById('<%=txtAttachPreFix.ClientID%>').value = Prefix;
            document.getElementById('<%=txtAttachPath.ClientID%>').value = Path;

            showModal('dvAttachRule');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Office Attachment Rules
    </div>
    <div class="page-content">
        <table width="100%">
            <tr>
                <td align="center">
                    <table>
                        <tr>
                            <td align="left">
                                <asp:Image ID="AddRules" runat="server" onclick="EditRules('','','')" ImageUrl="~/Images/AddNew.png" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvAttachmentRule" runat="server" AutoGenerateColumns="false" CellPadding="5"
                                    Width="500px" CellSpacing="0" GridLines="None" EmptyDataText="no record found !"
                                    CssClass="gridmain-css" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:BoundField DataField="Attach_Prefix" HeaderText="Attachment PreFix" ItemStyle-Width="150px" />
                                        <asp:BoundField HeaderText="Attachment Path" DataField="Attach_Path" ItemStyle-Width="300px" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Image ID="imgEdit" runat="server" onclick='<%# "EditRules("+Eval("Rule_ID").ToString()+",&#39;"+Eval("Attach_Prefix").ToString()+"&#39;,&#39;"+Eval("Attach_Path").ToString()+"&#39;);" %>'
                                                    ImageUrl="~/Images/edit.gif" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("Rule_ID").ToString() %>'
                                                    ImageUrl="~/Images/delete.gif" OnClick="btnDelete_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                        <td align="left">

                        </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="dvAttachRule" style="display: none; height: 150px; width: 500px; text-align: center">
            <asp:HiddenField ID="hdfRule_ID" runat="server" />
            <table cellpadding="5" cellspacing="0" border="1" style="border-collapse: collapse"
                width="100%">
                <tr>
                    <td>
                        Attach Prefix
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAttachPreFix" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Attach path
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtAttachPath" Width="400px" Columns="300" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSaveRule" runat="server" Text="Save" Height="35px" Width="100px"
                            Font-Size="14px" OnClick="btnSaveRule_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
