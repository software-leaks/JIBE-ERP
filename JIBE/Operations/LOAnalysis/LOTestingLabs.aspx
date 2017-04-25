<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LOTestingLabs.aspx.cs"
    Inherits="LOTestingLabs" Title="Lube Oil Testing Labs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        
        .linkbtn
        {
            border-right: wheat 1px solid;
            border-top: wheat 1px solid;
            font-weight: bold;
            border-left: wheat 1px solid;
            color: White;
            border-bottom: wheat 1px solid;
            background-color: white;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('.draggable').draggable();
        });

        $(document).keydown(function (e) {
            // ESCAPE key pressed 
            if (e.keyCode == 27) {
                if (isModalOpen == 1) {
                    hideModal(ModalPopUpID);
                }
            }
        });

        function Divaddnewlink() {
            document.getElementById("divadd").style.display = "block";
        }
        function validation() {

            if (document.getElementById("ctl00_MainContent_txtLabName").value == "") {
                alert("Please Enter Lab Name.");
                document.getElementById("ctl00_MainContent_txtLabName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_ddlAddCountry").value == "0") {
                alert("Please select country.");
                document.getElementById("ctl00_MainContent_ddlAddCountry").focus();
                return false;
            }

            return true;


        }


       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>     
 <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">                
              <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
             <div class="page-title">
                    Lube Oil Testing Labs
    
            </div>
            <div style="height: 650px;  color: Black;">
                <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 8%">
                                        Code/Desc :&nbsp;
                                    </td>
                                    <td align="left" style="width: 30%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="100%"  AutoPostBack="true"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="right" style="width: 8%">
                                        Country :&nbsp;
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:DropDownList ID="ddlSearchCountry" runat="server" Width="100%" 
                                            AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="--ALL--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Lab" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="GridView_Labs" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" OnRowDataBound="GridView_Labs_RowDataBound" DataKeyNames="ID"
                                    CellPadding="1" CellSpacing="0" Width="100%" GridLines="both" OnSorting="GridView_Labs_Sorting"
                                    AllowSorting="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Lab Name" SortExpression="Lab_Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkLabNameH" runat="server" CommandName="Sort" CommandArgument="Lab_Name"
                                                    ForeColor="Black">Lab Name&nbsp;</asp:LinkButton>
                                                <asp:Image ID="imgLab_Name" runat="server" Visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkLabName" runat="server" CommandArgument='<%#Eval("ID")%>'
                                                    Text='<%# Eval("Lab_Name") %>' Style="color: Black" OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="240px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Country" SortExpression="Country_Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkCountryH" runat="server" CommandName="Sort" CommandArgument="Country_Name"
                                                    ForeColor="Black">Country&nbsp;</asp:LinkButton>
                                                <asp:Image ID="imgCountry_Name" runat="server" Visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry_Name" runat="server" Text='<%#Eval("Country_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" SortExpression="Address">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkAddressH" runat="server" CommandName="Sort" CommandArgument="Address"
                                                    ForeColor="Black">Address&nbsp;</asp:LinkButton>
                                                <asp:Image ID="imgAddress" runat="server" Visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone" SortExpression="Phone">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkPhoreH" runat="server" CommandName="Sort" CommandArgument="Phone"
                                                    ForeColor="Black">Phone&nbsp;</asp:LinkButton>
                                                <asp:Image ID="imgPhone" runat="server" Visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMail" SortExpression="EMail">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkEMailH" runat="server" CommandName="Sort" CommandArgument="EMail"
                                                    ForeColor="Black">EMail&nbsp;</asp:LinkButton>
                                                <asp:Image ID="imgEMail" runat="server" Visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEMail" runat="server" Text='<%# Eval("EMail") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
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
                                                                CommandArgument='<%#Eval("ID")%>' ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                OnClientClick="return confirm('Are you sure want to delete?')" CommandArgument='<%#Eval("ID")%>'
                                                                ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px">
                                                            </asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="Bind_LOTestingLabs" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 450px;"
                            class="draggable">
                            <table width="98%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Lab Name&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtLabName" CssClass="txtInput" Width="100%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Address.&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAddress" TextMode="MultiLine" Rows="2" Width="100%" CssClass="txtInput"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        EMail.&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtEmail" TextMode="MultiLine" Rows="2" Width="100%" CssClass="txtInput"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Phone.&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPhone" TextMode="MultiLine" Rows="2" Width="100%" CssClass="txtInput"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Country&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlAddCountry" CssClass="txtInput" runat="server" AppendDataBoundItems="true"
                                            Width="100%">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; border-style: solid;
                                        border-color: Silver; border-width: 1px">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return validation();"
                                            OnClick="btnSave_Click" />
                                        <asp:HiddenField ID="Selected_ID" runat="server" />
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
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
