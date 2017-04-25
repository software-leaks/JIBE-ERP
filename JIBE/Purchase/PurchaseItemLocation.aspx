<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" ValidateRequest="false"
    EnableEventValidation="false" CodeFile="PurchaseItemLocation.aspx.cs" Title=" "
    Inherits="PurchaseItemLocation" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function Divaddnewlink() {
            document.getElementById("divadd").style.display = "block";
        }

        function validation() {

            if (document.getElementById("ctl00_MainContent_txtLocation").value == "") {
                alert("Please enter Location.");
                document.getElementById("ctl00_MainContent_txtLocation").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_ddlAddLocation").value == "0") {
                alert("Please select parent.");
                document.getElementById("ctl00_MainContent_ddlAddLocation").focus();
                return false;
            }

            return true;


        }

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <div style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;">
            <div style="border: 0px solid  #cccccc; padding: 0px; background-color: #5588BB;
                color: #FFFFFF; text-align: center;">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 95%">
                            <b>Item Location</b>
                        </td>
                        <td style="width: 5%">
                        </td>
                    </tr>
                </table>
            </div>
            <div style="border: 1px solid #cccccc; height: 40px; vertical-align: middle; padding-top: 10px">
                <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table cellpadding="1" cellspacing="1" width="100%" style="color: Black;">
                            <tr>
                                <td align="right" style="width: 15%">
                                    Location :&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlPLocation" runat="server" AppendDataBoundItems="true" Width="120px"
                                        Height="20px" Font-Size="11px" CssClass="txtInput" AutoPostBack="True" OnSelectedIndexChanged="ddlPLocation_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 15%">
                                    Search :&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSearchBy" Width="180px" runat="server" CssClass="txtInput" AutoPostBack="True"
                                        OnTextChanged="txtSearchBy_TextChanged"></asp:TextBox>
                                </td>
                                <td align="center" style="width: 10%">
                                    <asp:Button ID="btnClear" runat="server" Height="22px" OnClick="btnClear_Click" Text="Clear Filter"
                                        Width="80px" Font-Size="11px" />
                                </td>
                                <td align="center" style="width: 5%">
                                    <asp:ImageButton ID="btnExport" ImageUrl="~/Images/XLS.jpg" Height="25px" runat="server"
                                        ToolTip="Export to Excel" OnClick="btnExport_Click" />
                                </td>
                                <td align="center" style="width: 5%">
                                    <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Item Location" OnClick="ImgAdd_Click"
                                        ImageUrl="~/Images/Add-icon.png" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="border: 1px solid #cccccc; margin-top: 2px; cursor: pointer; ">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div style="overflow: auto; border: 0px solid gray; width: 100%">
                            <div>
                                <asp:GridView ID="gvLocation" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvLocation_RowDataBound" Width="100%" GridLines="Both" AllowSorting="false"
                                    OnSorting="gvLocation_Sorting">
                                    <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                    <RowStyle Font-Size="12px" CssClass="PMSGridRowStyle-css" />
                                    <AlternatingRowStyle Font-Size="12px" CssClass="PMSGridAlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Rank Name">
                                            <HeaderTemplate>
                                                Parent Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("Parent_Name")%>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                            <ItemStyle Wrap="true" HorizontalAlign="left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rank Name">
                                            <HeaderTemplate>
                                                Location Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("Location_Name")%>
                                                <asp:Label ID="lblLocationID" Visible="false" runat="server" Text='<%# Eval("Location_ID") %>'></asp:Label>
                                                <asp:Label ID="lblParentID" Visible="false" runat="server" Text='<%# Eval("Parent_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                            <ItemStyle Wrap="true" HorizontalAlign="left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="1" cellspacing="0">
                                                    <tr align="center">
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                CommandArgument='<%#Eval("[Location_ID]")%>' ForeColor="Black" ToolTip="Edit"
                                                                ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td style="border-color: transparent">
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                OnClientClick="return confirm('Are you sure want to delete this record?')" CommandArgument='<%#Eval("[Location_ID]") %>'
                                                                ForeColor="Black" ToolTip="Remove Mandatory Read" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;PURC_LIB_ITEM_LOCATION&#39;,&#39;Location_ID="+Eval("Location_ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" Width="120px" />
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="20" OnBindDataItem="BindLocation" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 30%;">
                            <table cellpadding="2" cellspacing="2" width="100%" style="padding-top: 10px;">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Parent Location :&nbsp;&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlAddLocation" runat="server" AppendDataBoundItems="true"
                                            Width="120px" Height="20px" Font-Size="11px" CssClass="txtInput">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Location Name
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtLocation" Width="40%" MaxLength="5" CssClass="txtInput" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; border-style: solid;
                                        padding: 5px 5px 5px 5px; border-color: Silver; border-width: 1px; background-color: #d8d8d8;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validation();"
                                            OnClick="btnSave_Click" />
                                        <asp:TextBox ID="txtLocationID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 0px solid #cccccc;
                                            background-color: #FDFDFD">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                        * Mandatory fields
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
