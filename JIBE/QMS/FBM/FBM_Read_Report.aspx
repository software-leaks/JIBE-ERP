<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FBM_Read_Report.aspx.cs"
    Inherits="QMS_FBM_FBM_Read_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
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
            <b>FBM Read Report</b>
        </div>
    </div>
    <div style="border: 1px solid #CCCCCC">
        <table cellpadding="2" cellspacing="2">
            <tr>
                <td align="right" style="width: 8%">
                    Fleet :&nbsp;
                </td>
                <td align="left">
                    <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                        OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged" Width="120px" Height="20px"
                        Font-Size="11px" CssClass="txtInput">
                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" style="width: 12%">
                    FBM List :&nbsp;
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlFbmList" runat="server" AppendDataBoundItems="true" Width="224px"
                        Height="20px" Font-Size="11px" CssClass="txtInput">
                    </asp:DropDownList>
                </td>
                <td align="right" style="width: 8%">
                    Rank :&nbsp;
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlRank" runat="server" AppendDataBoundItems="true" Width="120px"
                        Height="20px" Font-Size="11px" CssClass="txtInput">
                    </asp:DropDownList>
                </td>
                <td align="center" style="width: 10%">
                    <asp:Button ID="btnRetrieve" runat="server" Height="22px" OnClick="btnRetrieve_Click"
                        Text="Retrieve" Width="80px" Font-Size="11px" />
                </td>
                <td align="center" style="width: 5%">
                    <asp:ImageButton ID="btnExport" ImageUrl="~/Images/XLS.jpg" Height="25px" OnClick="btnExport_Click"
                        runat="server" ToolTip="Export to Excel" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    Vessel :&nbsp;
                </td>
                <td align="left">
                    <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="true" Width="120px"
                        Height="20px" Font-Size="11px" CssClass="txtInput">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Search :&nbsp;
                </td>
                <td align="left">
                    <asp:TextBox ID="txtSearchBy" Width="220px" runat="server" CssClass="txtInput"></asp:TextBox>
                </td>
                <td colspan="2" align="center">
                    <asp:RadioButtonList ID="rdoFBMReadStatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Read" Value="1" Selected="True" />
                        <asp:ListItem Text="Not yet read" Value="0" />
                    </asp:RadioButtonList>
                </td>
                <td align="center">
                    <asp:Button ID="btnClearFilter" runat="server" Height="22px" OnClick="btnClearFilter_Click"
                        Font-Size="11px" Text="Clear Filters" Width="80px" />
                </td>
                <td></td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="overflow-x: hidden; overflow-y: scroll; border: 1px solid #cccccc; height: 350px">
                <asp:GridView ID="gvFbmRead" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                    OnRowDataBound="gvFbmRead_RowDataBound" Width="100%" GridLines="Both" AllowSorting="true"
                    OnSorting="gvFbmRead_Sorting" OnRowCreated="gvFbmRead_RowCreated" CellSpacing="2"
                    CellPadding="2">
                    <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                    <RowStyle CssClass="PMSGridRowStyle-css" />
                    <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                    <SelectedRowStyle BackColor="#FFFFCC" />
                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                    <Columns>
                        <asp:TemplateField HeaderText="Vessel">
                            <HeaderTemplate>
                                <asp:LinkButton ID="lblVesselHeader" runat="server" ForeColor="White" CommandArgument="Vessel_Name"
                                    CommandName="Sort">Vessel&nbsp;</asp:LinkButton>
                                <img id="Drawing_Number" runat="server" visible="false" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblVessel" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridHeaderStyle-css" />
                            <ItemStyle Wrap="true" HorizontalAlign="left" Width="120px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rank">
                            <HeaderTemplate>
                                Rank
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRankShortName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Rank_Short_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridHeaderStyle-css" />
                            <ItemStyle Wrap="true" HorizontalAlign="left" Width="80px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Staff Code">
                            <HeaderTemplate>
                                Staff Code
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container,"DataItem.Staff_Code") %>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridHeaderStyle-css" />
                            <ItemStyle Wrap="true" HorizontalAlign="left" Width="100px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <HeaderTemplate>
                                Name
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.Staff_FullName")%>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridHeaderStyle-css" />
                            <ItemStyle Wrap="true" HorizontalAlign="left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" FBM Read on">
                            <HeaderTemplate>
                                FBM Read on
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container,"DataItem.FBM_DATE_READ") %>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridHeaderStyle-css" />
                            <ItemStyle Wrap="False" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindFbmReadSearch" />
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
