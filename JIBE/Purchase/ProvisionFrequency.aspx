<%@ Page Title="Frequency" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProvisionFrequency.aspx.cs" Inherits="Purchase_ProvisionFrequency" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function validationOnAdd() {

            //  var cmbparentType =document.getElementById("ctl00_MainContent_cmbParent").value;
            var txtduration = document.getElementById("ctl00_MainContent_txtDuration").value;

            if (txtduration == "") {
                alert("Please enter Supply Period (in Days).");
                document.getElementById("ctl00_MainContent_txtDuration").focus();
                return false;
            }

            return true;
        }

        function DecimalsOnly(e) {
            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    if (!IsUserFriendlyChar(key, "Decimals")) {
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        }

        function IsUserFriendlyChar(val, step) {
            // Backspace, Tab, Enter, Insert, and Delete
            if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
                return true;
            }
            // Ctrl, Alt, CapsLock, Home, End, and Arrows
            if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
                return true;
            }
            if (step == "Decimals") {
                if (val == 190 || val == 110) {
                    return true;
                }
            }
            // The rest
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
            <div style="font-size: 24px; background-color: #5588BB; width: 800px; color: White;
                text-align: center;">
                <b>Frequency </b>
            </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr align="left">
                                    <td align="right" style="width: 25%">
                                        Frequency :&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" Height="23" OnClick="btnFilter_Click"
                                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" OnClick="ImgAdd_Click" ImageUrl="~/Images/Add-icon.png"
                                            Text="Add Frequency" ToolTip="Add New Frequency" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="overflow-x: hidden; overflow-y: none; height: 600px; width: 800px;">
                            <asp:GridView ID="rgdFrequency" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                OnRowDataBound="rgdFrequency_RowDataBound" Width="100%" GridLines="None" AllowSorting="false"
                                OnSorting="rgdFrequency_Sorting" CellPadding="4" CellSpacing="0">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <PagerStyle CssClass="PagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:BoundField DataField="VESSEL_NAME" HeaderText="Vessel" />
                                    <asp:BoundField DataField="PROVISION_TYPE" HeaderText="Provision Type" />
                                    <asp:BoundField DataField="SUPPLY_PERIOD" HeaderText="Supply Period (in Days)" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                            CommandArgument='<%#Eval("ID")%>' ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif"
                                                            Height="16px"></asp:ImageButton>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                            OnClientClick="return confirm('Are you sure want to delete?')" Visible="false"
                                                            CommandArgument='<%#Eval("ID")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                            Height="16px"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindrgdFrequency" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" />
                            <asp:HiddenField ID="HiddenItemID" runat="server" />
                        </div>
                        <div id="divaddLocation" title="<%= OperationMode %>" style="display: none; border: 1px solid Black;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 400px;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblError" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td align="right" style="width: 40%">
                                                    Vessel &nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlVessel" runat="server" Width="100%" AppendDataBoundItems="True"
                                                        CssClass="txtReadOnly">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Provision Type
                                                </td>
                                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlProvisionType" runat="server" Width="100%" CssClass="txtReadOnly">
                                                        <asp:ListItem Text="Dry" Value="DRY">
                                                        </asp:ListItem>
                                                        <asp:ListItem Text="Fresh" Value="FRESH"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Supply Period (in Days)
                                                </td>
                                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDuration" runat="server" Width="100%" CssClass="txtInput" onkeydown="javascript:return DecimalsOnly(event);"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td align="center">
                                                    <asp:TextBox ID="txtCode" Width="16px" Visible="false" runat="server"></asp:TextBox>
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" Height="24px" OnClick="DivbtnSave_Click"
                                                        OnClientClick="return validationOnAdd();" Style="font-size: small" Width="60px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="color: #FF0000; font-size: small;" align="right">
                                        * Indicates as mandatory fields
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>

