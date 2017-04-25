<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMSAssignLocationCatalogue.aspx.cs"
    Inherits="PMSAssignLocationCatalogue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ctlFunctionList.ascx" TagName="ctlFunctionList" TagPrefix="ucFunction" %>
<%@ Register Src="~/UserControl/ctlVesselLocationList.ascx" TagName="ctlVesselLocationList"
    TagPrefix="ucVesslLocation" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
</head>
<body>
    <script language="javascript" type="text/javascript">
    </script>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="pnlCatalogue">
        <ContentTemplate>
            <center>
                <div id="divAddLocation" style="border: 1px solid gray; background-color: #E0E0E0;
                    font-family: Tahoma; font-size:11px; position: absolute; left: 35%; top: 10%; color: black; height: 433px;
                    width: 580px;" class="popup-css">
                    <center>
                        <table cellpadding="1" cellspacing="1" width="95%" style="position: relative;">
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td align="right">
                                                Search:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSearchLocation" Width="340px" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:ImageButton ID="imgLocationSearch" ImageUrl="~/Purchase/Image/preview.gif" runat="server"
                                                    OnClick="imgLocationSearch_Click" />
                                            </td>
                                            <td style="width: 50%">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="max-height: 350px; overflow: auto; background-color: #006699; z-index: 3;">
                                        <asp:GridView ID="gvLocation" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                            OnRowDataBound="gvLocation_RowDataBound" Width="100%" OnSelectedIndexChanging="gvLocation_SelectedIndexChanging"
                                            OnRowCommand="gvLocation_RowCommand" AllowSorting="true" OnSorting="gvLocation_Sorting"
                                            DataKeyNames="LocationID">
                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID">
                                                    <HeaderTemplate>
                                                        Select
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDivAssingLoc" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID">
                                                    <HeaderTemplate>
                                                        ID
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDivLocationCode" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LocationID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Short Code">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblDivShortCodeHeader" runat="server" ForeColor="White">Short Code&nbsp;</asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDivShortCode" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LocationShortCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Location Name">
                                                    <HeaderTemplate>
                                                        Location Name
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDivLocationName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LocationName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindCatalogueAssignLocation" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnDivSave" runat="server" Text="Save" OnClick="btnDivSave_click" />
                                    <input type="button" name="btnCancel" style="font-size: small" onclick="JavaScript:CloseDiv();"
                                        value="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </center>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
