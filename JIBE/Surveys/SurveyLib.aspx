<%@ Page Title="Survey Library" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SurveyLib.aspx.cs" Inherits="Surveys_SurveyLib" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <style type="text/css">
        .header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
          
        }
        h4
        {
            font-size: 1.2em;
            color: #ffffff;
            font-weight: bold;
            margin: 0 0 0 5px;
        }
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        .watermarked
        {
            color: #cccccc;
        }
        
        .linkbtn a
        {
            color: Black;
        }
        
        .linkbtn
        {
            color: black;
            background-color: white;
            text-decoration: none;
            border-left: 1px solid #cccccc;
            padding-left: 10px;
            border-top: 1px solid #cccccc;
            padding-top: 5px;
            border-right: 1px solid #cccccc;
            padding-right: 10px;
            border-bottom: 1px solid #cccccc;
            padding-bottom: 3px;
            background-color: #F1F8E0;
        }
        #dvAddNewCategory
        {
            cursor: move;
        }
        #dvAddNewCertificate
        {
            cursor: move;
        }
        #dvAddNewType
        {
            cursor: move;
        }
    </style>
    <style id="TooltipStyle" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: Tahoma;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .grid-row
        {
            padding: 6px;
        }
        .grid-col-fixed
        {
            border: 1px solid #cccccc;
        }
        .grid-col
        {
            border: 1px solid #cccccc;
        }
        .gradiant-css-browne
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#81F79F',EndColorStr='#088A4B');
            background: -webkit-gradient(linear, left top, left bottom, from(#81F79F), to(#088A4B));
            background: -moz-linear-gradient(top,  #81F79F,  #088A4B);
            color: Black;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function showDivAddNewCategory() {
            document.getElementById("dvAddNewCategory").style.display = "block";
        }
        function closeDivAddNewCategory() {
            document.getElementById("dvAddNewCategory").style.display = "None";
        }

        function showDivAddNewCertificate() {
            document.getElementById("dvAddNewCertificate").style.display = "block";
        }
        function closeDivAddNewCertificate() {
            document.getElementById("dvAddNewCertificate").style.display = "None";
        }

        function showDivAddNewType() {
            document.getElementById("dvAddNewType").style.display = "block";
        }
        function closeDivAddNewType() {
            document.getElementById("dvAddNewType").style.display = "None";
        }
        $(document).ready(function () {
            $('#dvAddNewCategory').draggable();
        });
        $(document).ready(function () {
            $('#dvAddNewCertificate').draggable();
        });
        $(document).ready(function () {
            $('#dvAddNewType').draggable();
        }); 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="pageTitle" class="gradiant-css-blue" style="font-size: 12px; border: 1px solid #CEE3F6;
        text-align: center; padding: 3px; font-weight: bold;">
        <asp:Label ID="lblPageTitle" runat="server" Text="Survey Library"></asp:Label>
    </div>
    <div id="page-content" style="height: 620px; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto; padding: 5px">
        <fieldset style="text-align: left; margin: 0px; padding: 2px;">
            <legend>Survey Category:</legend>
            <asp:UpdatePanel ID="UpdatePanelCategory" runat="server">
                <ContentTemplate>
                    <div style="text-align: center">
                        <table cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td style="text-align: left">
                                    Filter:&nbsp;<asp:TextBox ID="txtCategory" runat="server" CssClass="textbox-css"
                                        AutoPostBack="true" OnTextChanged="txtCategory_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtCategory"
                                        WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                </td>
                                <td style="text-align: right">
                                    <asp:LinkButton ID="lnkAddNewCategory" OnClientClick="javascript:showDivAddNewCategory();return false;"
                                        runat="server" CssClass="linkbtn">Add New Category</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView_Category" runat="server" AutoGenerateColumns="False" OnRowUpdating="GridView_Category_RowUpdating"
                            OnRowDeleting="GridView_Category_RowDeleting" OnRowEditing="GridView_Category_RowEditing"
                            OnRowCancelingEdit="GridView_Category_RowCancelEdit" DataKeyNames="ID" EmptyDataText="No Record Found"
                            AllowSorting="True" CaptionAlign="Bottom" CellPadding="4" ForeColor="#333333"
                            GridLines="None" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Category Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory_Name" runat="server" Text='<%#Eval("Survey_Category")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCategory_Name" Font-Size="11px" MaxLength="50" runat="server"
                                            Text='<%#Bind("Survey_Category")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="50px">
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="ImgBtnAccept" runat="server" ImageUrl="~/images/accept.png"
                                            CausesValidation="False" CommandName="Update" AlternateText="Update"></asp:ImageButton>
                                        <asp:ImageButton ID="ImgBtnCancel" runat="server" ImageUrl="~/images/reject.png"
                                            CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                            CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                                            CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                            AlternateText="Delete"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
        <fieldset style="text-align: left; margin: 0px; padding: 2px;">
            <legend>Survey Certificate:</legend>
            <asp:UpdatePanel ID="UpdatePanelCertificate" runat="server">
                <ContentTemplate>
                    <div style="text-align: center">
                        <table cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td style="text-align: left;width:100px;">
                                    Category
                                </td>
                                <td style="text-align: left;width:300px;">
                                    <asp:DropDownList ID="ddlCategoryFilter" Font-Size="11px" runat="server" DataSourceID="ObjectDataSource1" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCategoryFilter_SelectedIndexChanged" AutoPostBack="true"
                                        DataTextField="Survey_Category" DataValueField="ID">
                                        <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get_Survay_CategoryList"
                                        TypeName="SMS.Business.Technical.BLL_Tec_Survey"></asp:ObjectDataSource>
                                </td>
                                <td style="text-align: left">
                                    Filter:&nbsp;<asp:TextBox ID="txtCertificate" runat="server" CssClass="textbox-css"
                                        AutoPostBack="true" OnTextChanged="txtCertificate_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                        TargetControlID="txtCertificate" WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                </td>
                                <td style="text-align: right">
                                    <asp:LinkButton ID="lnkAddNewCertificate" OnClientClick="javascript:showDivAddNewCertificate();return false;"
                                        runat="server" CssClass="linkbtn">Add New Certificate</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GridView_Certificate" runat="server" AutoGenerateColumns="False"
                            OnRowUpdating="GridView_Certificate_RowUpdating" OnRowDeleting="GridView_Certificate_RowDeleting"
                            OnRowEditing="GridView_Certificate_RowEditing" OnRowCancelingEdit="GridView_Certificate_RowCancelEdit"
                            DataKeyNames="Surv_ID" EmptyDataText="No Record Found" AllowSorting="True" CaptionAlign="Bottom"
                            CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Certificate Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCertificate_Name" runat="server" Text='<%#Eval("Survey_Cert_Name1")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCertificate_Name" Font-Size="11px" MaxLength="50" runat="server"
                                            Text='<%#Bind("Survey_Cert_Name1")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Certificate Remarks"  HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSurvey_Cert_remarks" runat="server" Text='<%#Eval("Survey_Cert_remarks")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSurvey_Cert_remarks" Font-Size="11px" MaxLength="50" runat="server"
                                            Text='<%#Bind("Survey_Cert_remarks")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Term"  HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTerm" runat="server" Text='<%#Eval("Term")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTerm" Font-Size="11px" MaxLength="50" runat="server" Width="50px"
                                            Text='<%#Bind("Term")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category"  HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory_Name" runat="server" Text='<%#Eval("Survey_Category")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlSurvey_Category" Font-Size="11px" runat="server" Text='<%# Bind("Survey_Category_ID") %>'
                                            DataSourceID="ObjectDataSource1" DataTextField="Survey_Category" DataValueField="ID">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="50px">
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="ImgBtnAccept" runat="server" ImageUrl="~/images/accept.png"
                                            CausesValidation="False" CommandName="Update" AlternateText="Update"></asp:ImageButton>
                                        <asp:ImageButton ID="ImgBtnCancel" runat="server" ImageUrl="~/images/reject.png"
                                            CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                            CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                                            CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                            AlternateText="Delete"></asp:ImageButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
    <div id="dvAddNewCategory" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
        border-style: solid; border-width: 1px; width: 450px; position: absolute; left: 40%;
        top: 20%; z-index: 1; color: black">
        <div class="header">
            <h4>
                Add New Category</h4>
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                <ContentTemplate>
                    <table border="0" style="width: 100%;" class="content">
                        <tr>
                            <td style="font-size: 11px; font-weight: bold">
                                Category Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCatName" CssClass="textbox-css" Width="250px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 11px; text-align: center;">
                                <asp:Button ID="btnSaveAndAddCat" CssClass="button-css" runat="server" Text="Save And Add New"
                                    OnClick="btnSaveAndAddCat_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnSaveAndCloseCat" CssClass="button-css" runat="server" Text="Save And Close"
                                    OnClick="btnSaveAndCloseCat_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDivAddNewCategory()" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="dvAddNewCertificate" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
        border-style: solid; border-width: 1px; width: 450px; position: absolute; left: 40%;
        top: 20%; z-index: 1; color: black">
        <div class="header">
            <h4>
                Add New Certificate</h4>
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table border="0" style="width: 100%;" class="content">
                        <tr>
                            <td style="font-size: 11px; font-weight: bold">
                                Certificate Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCertificateName" CssClass="textbox-css" Width="250px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; font-weight: bold">
                                Category Name:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSurvey_Category" Font-Size="11px" runat="server" Width="250px"
                                    DataSourceID="ObjectDataSource1" DataTextField="Survey_Category" DataValueField="ID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; font-weight: bold">
                                Term:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTerm" CssClass="textbox-css" Width="250px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; font-weight: bold">
                                Certificate Remark:
                            </td>
                            <td>
                                <asp:TextBox ID="txtSurvey_Cert_remarks" CssClass="textbox-css" Width="250px" runat="server"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 11px; text-align: center;">
                                <asp:Button ID="btnSaveAndAddCertificate" CssClass="button-css" runat="server" Text="Save And Add New"
                                    OnClick="btnSaveAndAddCertificate_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnSaveAndCloseCertificate" CssClass="button-css" runat="server"
                                    Text="Save And Close" OnClick="btnSaveAndCloseCertificate_Click" />&nbsp;&nbsp;
                                <asp:Button ID="Button3" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDivAddNewCertificate()" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
