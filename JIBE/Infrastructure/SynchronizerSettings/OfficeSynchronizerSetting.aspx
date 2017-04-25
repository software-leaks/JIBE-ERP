<%@ Page Title="Synchronizer Settings" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="OfficeSynchronizerSetting.aspx.cs" Inherits="Infrastructure_OfficeSync_OfficeSynchronizerSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script type="text/javascript">

        function EditSettings(Setting_ID, Setting_Key, Setting_Value) {

            document.getElementById('<%=hdfSetting_ID.ClientID%>').value = Setting_ID
            document.getElementById('<%=txtKey.ClientID%>').value = Setting_Key;
            document.getElementById('<%=txtValue.ClientID%>').value = Setting_Value;

            showModal('dvSetting');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Office Synchronizer Setting
    </div>
    <div class="page-content">
        <table width="100%">
            <tr>
                <td align="center">
                    <table>
                        <tr>
                            <td align="left">
                                <asp:Image ID="AddSettings" runat="server" onclick="EditSettings('','','')" ImageUrl="~/Images/AddNew.png" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvSetting" runat="server" AutoGenerateColumns="false" CellPadding="5"
                                    Width="500px" CellSpacing="0" GridLines="None" EmptyDataText="no record found !"
                                    CssClass="gridmain-css" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:BoundField DataField="Setting_Key" HeaderText="Key" ItemStyle-Width="150px" />
                                        <asp:BoundField HeaderText="Value" DataField="Setting_Value" ItemStyle-Width="300px" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Image ID="imgEdit" runat="server" onclick='<%# "EditSettings("+Eval("Setting_ID").ToString()+",&#39;"+Eval("Setting_Key").ToString()+"&#39;,&#39;"+Eval("Setting_Value").ToString()+"&#39;);" %>'
                                                    ImageUrl="~/Images/edit.gif" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("Setting_ID").ToString() %>'
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
                                <auc:CustomPager ID="CustomPagerSetting" OnBindDataItem="BindItems" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <div id="dvSetting" style="display: none; height: 150px; width: 500px; text-align: center">
            <asp:HiddenField ID="hdfSetting_ID" runat="server" />
            <table cellpadding="5" cellspacing="0" border="1" style="border-collapse: collapse"
                width="100%">
                <tr>
                    <td>
                        Setting Key
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtKey" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Setting Value
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtValue" Width="400px" Columns="300" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSaveSetting" runat="server" Text="Save" Height="35px" Width="100px"
                            Font-Size="14px" OnClick="btnSaveSetting_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
