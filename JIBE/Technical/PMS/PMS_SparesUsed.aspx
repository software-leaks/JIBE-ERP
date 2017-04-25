<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMS_SparesUsed.aspx.cs" Inherits="PMS_SparesUsed" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
</head>
<body style="font-family: Tahoma; font-size: 12px;">
    <script language="javascript" type="text/javascript">


    </script>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div style="font-family: Tahoma; font-size: 12px;" class="popup-css">
        <div style="background-color: #5588BB; color: White; vertical-align: middle; text-align: center;
            height: 20px">
            <b>Spares Items Used</b>
        </div>
    </div>
    <div>
        <table cellpadding="2" cellspacing="2">
            <tr>
                <td>
                    Search By - Drawing No/Part No/Item Name :
                </td>
                <td>
                    <asp:TextBox ID="txtSearchtext" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
                <td align="center">
                    <asp:Button ID="btnRetrieve" runat="server" Height="22px" OnClick="btnRetrieve_Click"
                        Text="Retrieve" Width="80px" Font-Size="11px" />
                </td>
                <td align="center">
                    <asp:Button ID="btnClearFilter" runat="server" Height="22px" OnClick="btnClearFilter_Click"
                        Font-Size="11px" Text="Clear Filters" Width="80px" />
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="border: 0px solid gray">
                <asp:GridView ID="gvSpareItemUsed" runat="server" EmptyDataText="NO RECORDS FOUND"
                    AutoGenerateColumns="False" OnRowDataBound="gvSpareItemUsed_RowDataBound" Width="100%"
                    GridLines="Both" AllowSorting="true" OnSorting="gvSpareItemUsed_Sorting" DataKeyNames="JOB_ID"
                    OnRowCreated="gvSpareItemUsed_RowCreated" CellSpacing="2" CellPadding="2">
                    <HeaderStyle CssClass="PMSGridHeaderStyle-css"/>
                    <RowStyle CssClass="PMSGridRowStyle-css"/>
                    <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                    <SelectedRowStyle BackColor="#FFFFCC" />
                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                    <Columns>
                        <asp:TemplateField HeaderText="SubSystem">
                            <HeaderTemplate>
                                <asp:Label ID="lblSubSystemHeader" runat="server" ForeColor="Black">SubSystem&nbsp;</asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.Subsystem_Description")%>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="160px">
                            </ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DrawingNumber">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lblDrawingNumberHeader" runat="server" ForeColor="Black" CommandArgument="Drawing_Number"
                                    CommandName="Sort">Drawing No.&nbsp;</asp:LinkButton>
                                <img id="Drawing_Number" runat="server" visible="false" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDrawingNumber" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Drawing_Number") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="120px">
                            </ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PartNumber">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lblPartNumberHeader" runat="server" ForeColor="Black" CommandArgument="Part_Number"
                                    CommandName="Sort"> Part No.&nbsp;</asp:LinkButton>
                                <img id="Part_Number" runat="server" visible="false" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPartNumber" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Part_Number") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="120px">
                            </ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lblItemNameHeader" runat="server" ForeColor="Black" CommandArgument="Short_Description"
                                    CommandName="Sort">Item Name&nbsp;</asp:LinkButton>
                                <img id="Short_Description" runat="server" visible="false" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblItemName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Short_Description") %>'></asp:Label>
                                <asp:Label ID="lblFullDesc" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Long_Description") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="200px">
                            </ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Unit
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.Unit_and_Packings")%>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px">
                            </ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Old ROB
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container,"DataItem.Old_ROB") %>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="110px">
                            </ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Used Qty.
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.Used_Qty")%>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="110px">
                            </ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                New ROB
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container,"DataItem.New_ROB")  %>
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="110px">
                            </ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <HeaderTemplate>
                                Remarks
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblItemUsedRemaks" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Su_remarks") %>'></asp:Label>
                                <asp:Label ID="lblItemUsedFullRemaks" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Su_Full_remarks") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindSparesItemUsed" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnRetrieve" />
                <asp:PostBackTrigger ControlID="btnClearFilter" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
