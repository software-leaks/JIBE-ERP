<%@ Page Title="Locations" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Location.aspx.cs" Inherits="Location" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        function validationOnAdd() {

            //        var cmbparentType =document.getElementById("ctl00_MainContent_cmbParent").value;
            var TxtShotCode = document.getElementById("ctl00_MainContent_TxtShotCode").value;
            var txtShortDesc = document.getElementById("ctl00_MainContent_txtShortDesc").value;
            var Parent = document.getElementById("ctl00_MainContent_cmbParent").value;
            if (Parent == "--Select--") {
                alert("Please Select Parent");
                document.getElementById("ctl00_MainContent_cmbParent").focus();
                return false;
            }
            if (TxtShotCode == "") {
                alert("Please enter Short Code.");
                document.getElementById("ctl00_MainContent_TxtShotCode").focus();
                return false;
            }
            if (txtShortDesc == "") {
                alert("Please enter Short Description.");
                document.getElementById("ctl00_MainContent_txtShortDesc").focus();
                return false;
            }

            if (document.getElementById('ctl00_MainContent_dvLoca') != null) {
                if (document.getElementById('ctl00_MainContent_txtNoLoc').value == "" || document.getElementById('ctl00_MainContent_txtNoLoc').value == "0") {
                    document.getElementById('ctl00_MainContent_txtNoLoc').focus();
                    alert("Number Of Location is a mandatory field and it cannot be blank or 0.");
                    return false;
                }

                if (/^\d+$/.test(parseInt(document.getElementById('ctl00_MainContent_txtNoLoc').value))) {
                } else {
                    document.getElementById('ctl00_MainContent_txtNoLoc').focus();
                    alert("Number Of Location is an whole number.");
                    return false;
                }
            }
            return true;
        } 

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
            <div class="page-title">
                Locations
            </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr align="left">
                                    <td align="right" style="width: 25%">
                                        Short Code / Desc. :&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSearch" runat="server" Width="100%"></asp:TextBox>
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
                                            Text="Add Location" ToolTip="Add New Location" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <asp:GridView ID="rgdLocation" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                OnRowDataBound="rgdLocation_RowDataBound" Width="100%" GridLines="Both" AllowSorting="true"
                                OnSorting="rgdLocation_Sorting" CellPadding="1" CellSpacing="0">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Code" Visible="false">
                                        <HeaderTemplate>
                                            Code
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ParentType" Visible="false">
                                        <HeaderTemplate>
                                            Parent Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblParent_Type" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Parent_Type") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Short_code">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblShort_codeHeader" runat="server" CommandName="Sort" CommandArgument="Short_code"
                                                ForeColor="Black">Short Code&nbsp;</asp:LinkButton>
                                            <img id="Short_code" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblShort_code" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Short_code") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css" />
                                        <HeaderStyle HorizontalAlign="Left" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblDescriptionHeader" runat="server" CommandName="Sort" CommandArgument="Description"
                                                ForeColor="Black">Description&nbsp;</asp:LinkButton>
                                            <img id="Description" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblDescription" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Description") %>'
                                                Style="color: Black" CommandArgument='<%#Eval("[code]")%>' OnCommand="onUpdate"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="left" Width="100px" CssClass="PMSGridItemStyle-css" />
                                        <HeaderStyle HorizontalAlign="Left" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Long_Discription">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblLong_DiscriptionHeader" runat="server" CommandName="Sort"
                                                CommandArgument="Long_Discription" ForeColor="Black">Long Description.&nbsp;</asp:LinkButton>
                                            <img id="Long_Discription" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLong_Discription" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Long_Discription") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="left" Width="100px" CssClass="PMSGridItemStyle-css" />
                                        <HeaderStyle HorizontalAlign="left" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                            Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[code]")%>' ForeColor="Black"
                                                            ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                            Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                            CommandArgument='<%#Eval("[code]")+ ", " +Eval("Parent_Type")%>' ForeColor="Black"
                                                            ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindrgdLocation" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" />
                        </div>
                        <div id="divaddLocation" title="<%= OperationMode %>" style="display: none; border: 1px solid Black;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 500px;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td align="right" style="width: 25%">
                                                    Parent Type &nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtParentType" runat="server" Visible="false" Width="5px" CssClass="txtReadOnly"></asp:TextBox>
                                                    <asp:DropDownList ID="cmbParent" runat="server" Width="102%" AppendDataBoundItems="True"
                                                        CssClass="txtReadOnly">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Short Code &nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TxtShotCode" runat="server" Width="100%" MaxLength="15" CssClass="txtInput"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Short Description &nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShortDesc" runat="server" Width="100%" Height="50px" TextMode="MultiLine"
                                                        CssClass="txtInput"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Long Description &nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLongDesc" runat="server" Width="100%" Height="50px" TextMode="MultiLine"
                                                        CssClass="txtInput"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <div id="dvLoca" runat="server">
                                                    <td align="right">
                                                        No. Of Location &nbsp;:&nbsp;
                                                    </td>
                                                    <td style="color: #FF0000; font-size: small; width: 1%" align="left">
                                                        *
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtNoLoc" runat="server" Width="40%" MaxLength="15" CssClass="txtInput"
                                                            Text="1"></asp:TextBox>
                                                    </td>
                                                </div>
                                            </tr>
                                            <tr>
                                            <td align="right">
                                            Vessel Type &nbsp;:&nbsp;
                                            </td>
                                            <td style="color: #FF0000; width: 1%" align="right">
                                             
                                            </td>
                                            <td align="left">
                                            <asp:DropDownList ID="ddlvessel_AddType" Width="82%" runat="server" AppendDataBoundItems="true"
                                            CssClass="txtInput" />
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
                                                    <asp:Button ID="btnSaveLocation" runat="server" Text="Save" Height="24px" OnClick="DivbtnSave_Click"
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
