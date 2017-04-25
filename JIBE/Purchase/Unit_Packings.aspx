<%@ Page Title="Unit Packings" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Unit_Packings.aspx.cs" Inherits="Purchase_Unit_Packings" %>

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

        function runScript(e) {
            if (e.keyCode == 13) {
                document.getElementById("ctl00_MainContent_btnSave").OnClick;
                return false;
            }
        }

        function Validation() {

            if (document.getElementById("ctl00_MainContent_txtMainPack").value.trim() == "") {
                alert("Please enter main pack.");
                document.getElementById("ctl00_MainContent_txtMainPack").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_txtAbreviation").value.trim() == "") {
                alert("Please enter Abbreviation.");
                document.getElementById("ctl00_MainContent_txtAbreviation").focus();
                return false;
            }

            return true;
        } 

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 
    <center>
        <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
              <div class="page-title">
                Unit Packings   
              </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr align="left">
                                    <td align="right" style="width: 25%">
                                        Main Pack / Abbreviation :&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSearch" runat="server"  Width="100%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtSearch"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
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
                                            Text="Add Location" ToolTip="Add New Unit Packing" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <asp:GridView ID="rgdUnitPakings" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" OnRowDataBound="rgdUnitPakings_RowDataBound" Width="100%"
                                GridLines="Both" AllowSorting="true" OnSorting="rgdUnitPakings_Sorting" CellPadding="1"
                                CellSpacing="0">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Main Pack" Visible="true">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblMAIN_PACKHeader" runat="server" CommandName="Sort" CommandArgument="MAIN_PACK"
                                                ForeColor="Black">Main Pack&nbsp;</asp:LinkButton>
                                            <img id="MAIN_PACK" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblMAIN_PACK" Visible="true" runat="server" Style="color: Black"
                                                Text='<%# DataBinder.Eval(Container,"DataItem.ABREVIATION") %>' CommandArgument='<%#Eval("[ID]")%>'
                                                OnCommand="onUpdate"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Abreviation">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblAbreviationHeader" runat="server" CommandName="Sort" CommandArgument="ABREVIATION"
                                                ForeColor="Black">Abbreviation&nbsp;</asp:LinkButton>
                                            <img id="ABREVIATION" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            
                                            <asp:Label ID="lblAbreviation" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.MAIN_PACK") %>'></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
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
                                                            Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                            ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                            Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                            CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
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
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindrgdUnitPakings" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" onkeypress="return runScript(event)" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 20%;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="2" cellspacing="2">
                                            
                                             <tr>
                                                <td align="right">
                                                    Main Pack &nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">
                                                    *
                                                </td>
                                                <td align="left">
                                                     <asp:TextBox ID="txtMainPack" runat="server" Width="90%" MaxLength="6" CssClass="txtInput"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="right" style="width: 25%">
                                                   Abbreviation &nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">
                                                    *
                                                </td>
                                                <td align="left">
                                                  
                                                     <asp:TextBox ID="txtAbreviation" runat="server" Width="90%" MaxLength="16" CssClass="txtInput"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtUnitPakingID" Width="16px" Visible="false" runat="server"></asp:TextBox>
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" Height="24px" OnClick="btnSave_Click"
                                                        OnClientClick="return Validation();" Style="font-size: small" Width="60px" />
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
                        <asp:PostBackTrigger ControlID="ImgExpExcel"/>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
