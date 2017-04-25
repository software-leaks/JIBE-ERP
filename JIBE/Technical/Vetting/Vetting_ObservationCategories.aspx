<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="Vetting_ObservationCategories.aspx.cs" Inherits="Technical_Vetting_Vetting_ObservationCategories" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>

    <script language="javascript" type="text/javascript">
        function showAddCategory() {
            $("#divCategory1").prop('title', 'Add Category');
            showModal('divCategory1');
            $('#txtCategoryName').val(' ');
            $("#<%= hdnCatid.ClientID%>").val('');
            return false;
        }

        function ValidateCategory() {       
            var CategoryName = document.getElementById($('[id$=txtCategoryName]').attr('id')).value.trim();
            if (CategoryName == "") {
                alert('Enter category name');
                return false;
            }

        }
        function SetTitleonEdit() {
            $("#divCategory1").prop('title', 'Edit Category');
        }

        function SetTitleonAdd() {
            $("#divCategory1").prop('title', 'Add Category');
        }

        function onEditClick(CategoryID, CategoryName) {
            $("#divCategory1").prop('title', 'Edit Category');
            showModal('divCategory1');
            document.getElementById($('[id$=hdnCatid]').attr('id')).value = CategoryID;
            document.getElementById($('[id$=txtCategoryName]').attr('id')).value = CategoryName;

            return true;
        }
    </script>
    <style type="text/css">
       
        .gridmain-css tr
        {
            height: 30px;
        }
        .gridmain-css tr:hover
        {
            background-color: #feecec;
        }
        #cke_show_borders p
        {
            margin: 8px 8px 8px 8px !important;
        }
       body {  
   
        font-family: Tahoma;
        font-size: 12px;
        margin: 0;
        padding: 0;
       }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <div id="divLoggout" runat="server" style="color: red; font-size: 14px; text-align: center;">
                Session expired!! Please log out and login again
            </div>
       <div align="center" id="MainContent" runat="server">
     <div id="AccessMsgDiv" runat="server" style="color: Red; font-size: 14px; text-align: center;">
            You don't have sufficient privilege to access the requested page.</div>
            <div id="MainDiv" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 950px">
                    <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFilter">
                        <table cellpadding="2" cellspacing="4" style="float: left;" width="100%">
                            <tr>
                                <td align="right" style="width: 10%">
                                    Category Name :&nbsp;
                                </td>
                                <td align="left" style="width:40%">
                                    <asp:TextBox ID="txtfilter" runat="server" Width="100%" Height="18px"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                        WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                </td>
                                <td align="center" style="width: 1%">
                                    <asp:ImageButton ID="btnFilter" runat="server" TabIndex="0" OnClick="btnFilter_Click"
                                        ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                </td>
                                <td align="center" style="width: 1%">
                                    <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                        ImageUrl="~/Images/Refresh-icon.png" />
                                </td>
                                    <td align="center" style="width: 1%">
                                    <asp:ImageButton ImageUrl="~/Images/Add-icon.png" ClientIDMode="Static" ID="ImgAdd" Height="22px"
                                        runat="server" Style="cursor: pointer;" ToolTip="Add new category" OnClientClick="return showAddCategory();" />
                                </td>
                                <td style="width: 1%">
                                    <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                        ImageUrl="~/Images/Exptoexcel.png" />
                                </td>
                            
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div align="center" style="width: 950px">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvCategory" runat="server" EmptyDataText="NO RECORDS FOUND" CellPadding="0" ShowHeaderWhenEmpty="true" 
                                CellSpacing="2" Width="100%" AllowSorting="true" CssClass="gridmain-css" AutoGenerateColumns="false"
                                OnSorting="gvCategory_Sorting" OnRowDataBound="gvCategory_RowDataBound">
                                <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Category Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryId" runat="server" Text='<%#Eval("OBSCategory_ID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category Name">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblReasonHeader" Style="margin-left: 2px;text-decoration:none; " runat="server" CommandName="Sort"
                                                CommandArgument="OBSCategory_Name" ForeColor="Black">Category Name</asp:LinkButton>
                                            <img id="OBSCategory_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryName" runat="server" Text='<%#Eval("OBSCategory_Name")%>'></asp:Label>
                                            <asp:Label ID="lblCategoryIdEdit" runat="server" Text='<%#Eval("OBSCategory_ID")%>'
                                                Style="display: none"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="20%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:ImageButton ID="Edit" CssClass="edit" ImageUrl="~/Images/Edit.gif" ForeColor="Black"
                                                            ToolTip="Edit" runat="server" Height="16px" Width="16px" Style="cursor: pointer;" Visible='<%# uaEditFlag %>'/>                                           
                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                OnClientClick="return confirm('Are you sure, you want to delete?')" CommandArgument='<%#Eval("[OBSCategory_ID]")%>'
                                                ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px" Width="16px" Visible='<%# uaDeleteFlage %>'></asp:ImageButton>
                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                Height="16px" Style="vertical-align: top; cursor: pointer;" Width="16px" runat="server"
                                                onclick='<%# "Get_Record_Information(&#39;VET_LIB_ObservationCategories&#39;,&#39;OBSCategory_ID="+Eval("OBSCategory_ID").ToString()+"&#39;,event,this)" %>' />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="2%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="10" OnBindDataItem="BindGrid" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ImgExpExcel" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <div id="divCategory1" style="display: none; font-family: Tahoma; text-align: left;
            font-size: 12px; color: Black; width: 380px; height: 130px">
            <asp:UpdatePanel runat="server" ID="upoilmajor">
                <ContentTemplate>
                    <table cellpadding="2" cellspacing="2" style="padding-top: 20px;">
                        <tr>
                            <td align="left" style="width: 100px; padding-left: 20px;">
                                Category Name&nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCategoryName" ClientIDMode="Static" CssClass="txtInput" MaxLength="200"
                                    Width="200px" runat="server"></asp:TextBox>
                                   
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" style="font-size: 11px; text-align: center;padding-top: 30px;">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidateCategory();" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hdnCatid" runat="server"></asp:HiddenField>
        </div>
    </div>
    </div>
</form>
</body>
</html>

