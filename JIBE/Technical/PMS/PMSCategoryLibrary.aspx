<%@ Page Title="PMS Category Library" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PMSCategoryLibrary.aspx.cs" Inherits="Technical_PMS_PMSCategoryLibrary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

<link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">

    function validationAddCategory() {
        //                document.getElementById("ctl00_MainContent_ddlCatalogFunction_txtSelectedFunction").value = "";

        return true;

    }

    function validationCategory() {

        var Type = document.getElementById($('[id$=ddlType]').attr('id')).value;
        var CategoryName = document.getElementById($('[id$=txtCatName]').attr('id')).value.trim();







        if (Type == "--SELECT--") {
            alert("Select Category Type.");
            document.getElementById($('[id$=ddlType]').attr('id')).focus();
            return false;
        }
        if (CategoryName == "") {
            alert("Category name is required.");
            document.getElementById($('[id$=txtCatName]').attr('id')).focus();
            return false;
        }

  

        return true;

    }
        
        </script>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; height: 100%;">
        <div id="page-header" class="page-title">
            <b>PMS Category Library</b>
        </div>
          <div id="Div1" style="background-color:Yellow; " align="center" >
              <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible=false></asp:Label>
        </div>
        <table style="width: 100%;">
        <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="50%">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblCatName" runat="server" Text="Category Name :"></asp:Label>  
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCatName" runat="server" CssClass="txtInput"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                       <asp:Label ID="lblCatType" runat="server" Text=" Category Type :"></asp:Label>  
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="txtInput">
                                            <asp:ListItem Text="--SELECT--" Value="--SELECT--" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Primary" Value="Primary"></asp:ListItem>
                                            <asp:ListItem Text="Secondary" Value="Secondary"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="left">
                                        <asp:Button ID="btnCategorySave" Text="Save" runat="server" Width="70px" OnClientClick="return validationCategory()"
                                            OnClick="btnCategorySave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="updCat" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdCategories" CellPadding="8" runat="server" ShowFooter="False"
                                AutoGenerateColumns="false" BorderWidth="1px" Width="100%">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" Font-Size="12px" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        <HeaderTemplate>
                                            Category Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCategory" CommandName="Select" ToolTip="Select" runat="server"
                                                ForeColor="GrayText" Font-Bold="true" CssClass="LinkButton" Text='<%# Eval("Category_Name") %>'
                                                CommandArgument='<%# Eval("ID") %>' OnCommand="lnkCategory_Click"></asp:LinkButton>
                                            <asp:HiddenField ID="hdnActiveStatus" runat="server" Value='<%# Eval("Active_Status") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Category Type" DataField="Category_Type" ItemStyle-Width="80px" />
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="0">
                                                <tr>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgEditCategory" runat="server" ForeColor="Black" ToolTip="Edit"
                                                            ImageUrl="~/purchase/Image/Edit.gif" OnCommand="ImgEditCategory_Click" CommandArgument='<%# Eval("ID") %>'
                                                            Width="16px" Height="16px" Visible='<%# Eval("Active_Status").ToString()=="1"?true:false %>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgCategoryDelete" runat="server" ForeColor="Black" ToolTip="Delete"
                                                            ImageUrl="~/purchase/Image/Delete.gif" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                            OnCommand="ImgCategoryDelete_Click" CommandArgument='<%# Eval("ID") %>' Width="16px"
                                                            Height="16px" Visible='<%# Eval("Active_Status").ToString()=="1"?true:false %>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgCategoryRestore" runat="server" ForeColor="Black" ToolTip="Restore"
                                                            ImageUrl="~/purchase/Image/restore.png" OnClientClick="return confirm('Are you sure, you want to  restore ?')"
                                                            OnCommand="ImgCategoryRestore_Click" CommandArgument='<%# Eval("ID") %>' Width="16px"
                                                            Height="16px" Visible='<%# Eval("Active_Status").ToString()=="1"?false:true %>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Records Found
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            
        </table>
    </div>
</asp:Content>
