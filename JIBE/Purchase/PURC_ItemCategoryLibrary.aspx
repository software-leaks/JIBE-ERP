<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.master"  CodeFile="PURC_ItemCategoryLibrary.aspx.cs" Inherits="Purchase_PURC_ItemCategoryLibrary" Title="ItemCategory Library"%>

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
    <script language="javascript" type="text/javascript">

        function Isvalidate() {

            var TxtCatName = document.getElementById("ctl00_MainContent_TxtCatName").value;
            var ddlcatType = document.getElementById("ctl00_MainContent_ddlcatType").value;
            var TxtShrtName = document.getElementById("ctl00_MainContent_TxtShrtName").value;


            if (TxtCatName == "" || TxtCatName == null) {
                alert("Please enter Functions name.");
                document.getElementById("ctl00_MainContent_TxtCatName").focus();
                return false;
            }

            if (ddlcatType == "0") {
                alert("Please Select the Category Type.");
                document.getElementById("ctl00_MainContent_ddlcatType").focus();
                return false;
            }

            if (TxtShrtName == "" || TxtShrtName == null) {
                alert("Please enter Functions Short Name.");
                document.getElementById("ctl00_MainContent_TxtShrtName").focus();
                return false;
            }
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
                Item Category</div> 
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr align="left">
                                    <td align="right" style="width: 20%">
                                       Item Category Name :&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSearchName"  runat="server" Width="100%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtSearchName"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="right" style="width: 20%">
                                        <%--Requisition Type :&nbsp;&nbsp;--%>
                                    </td>
                                    <td align="left">
                                       <%-- <asp:DropDownList ID="ddlCategoryType" runat="server"  Width="100%">
                                         <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="ItemCategory"></asp:ListItem>
                                            <asp:ListItem Text="UrgencyLevel"></asp:ListItem>
                                        </asp:DropDownList>--%>
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
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Department" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" Text="Add Department" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <asp:GridView ID="rgdItmCat" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                OnRowDataBound="rgdItmCat_RowDataBound" Width="100%" GridLines="Both" AllowSorting="true"
                                OnSorting="rgdItmCat_Sorting" CellPadding="1" CellSpacing="0" 
                                PageSize="20">
                                  <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                  <RowStyle CssClass="RowStyle-css" />
                                  <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                   
                                    <asp:TemplateField HeaderText="Name">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblNameHeader" runat="server" CommandName="Sort" CommandArgument="Category_Name"
                                                ForeColor="Black" >Category Name</asp:LinkButton>
                                            <img id="Name_Dept" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Category_Name") %>'
                                                Style="color: Black" CommandArgument='<%#Eval("[ID]")%>' Enabled="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderText="Form Type">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblForm_TypeHeader" runat="server" CommandName="Sort" CommandArgument="Category_Type"
                                                ForeColor="Black">Category Type</asp:LinkButton>
                                            <img id="Form_Type" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblForm_Type" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Category_Type") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnClick="onUpdate"
                                                            CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                            ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnClick="onDelete"
                                                             OnClientClick=" return confirm('Are you sure want to delete?')"
                                                            CommandArgument='<%#Eval("[ID]")+","+Eval("[IsInUse]") %>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
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
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindrgdItmCat" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 450px;">
                            <table width="98%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Category Name &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TxtCatName" runat="server" Width="95%" CssClass="txtInput" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" style="width: 30%">
                                        Category Short Code &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TxtShrtName" runat="server" Width="95%" CssClass="txtInput" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Category Type&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        
                                        <asp:DropDownList ID="ddlcatType" runat="server" Width="97%" AppendDataBoundItems="True"
                                            CssClass="txtInput">
                                            <asp:ListItem Text="SELECT" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="ItemCategory" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="UrgencyLevel" Value="2"></asp:ListItem>
                                          
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                
                                
                                <tr>
                                    <td>
                                    </td>
                                    <td align="center" colspan="3">
                                        <asp:Button ID="ImgSave" Style="font-size: small" runat="server" OnClientClick="return Isvalidate();"
                                            OnClick="Save_Click" Text="Save" />
                                        <asp:TextBox ID="txtDeptID" runat="server" Visible="false" Width="1px"></asp:TextBox>
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

