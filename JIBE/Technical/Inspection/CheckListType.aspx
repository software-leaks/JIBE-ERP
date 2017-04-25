<%@ Page Title="Checklist Type Library" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CheckListType.aspx.cs" Inherits="Technical_Inspection_CheckListType"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/crew_interview_css.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        /* body, html
        {
            overflow-x: hidden;
        }*/
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
        #dvAddNewGrade
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
        function showDiv(dv) {
            document.getElementById(dv).style.display = "block";
        }
        function closeDiv(dv) {
            document.getElementById(dv).style.display = "None";
            __doPostBack("<%=btnCloseGrade.UniqueID %>", "onclick");
        }
        function showDiv(dv) {
            //            document.getElementById('dvAddNewCategory').title = "Add New Category"
            document.getElementById('dvAddNewGrade').title = "Add New Grade"
            document.getElementById('dvAddNewType').title = "Add New Checklist Type"
            showModal(dv, true, fnCloseNewGrade);
        }
        function closeDiv(dv) {
            hideModal(dv);
            __doPostBack("<%=btnCloseGrade.UniqueID %>", "onclick");
        }
        // debugger;
        function Addnewgrade() {

            //            document.getElementById($('[id$=ddlMin]').attr('id')).value = 0;
            //            document.getElementById($('[id$=ddlMax]').attr('id')).value = 0;
            //            document.getElementById($('[id$=ddlDivisions]').attr('id')).value = 0;

            if (document.getElementById($('[id$=dvrpt]').attr('id')) != null) {
                document.getElementById($('[id$=dvrpt]').attr('id')).style.display = "none";
            }

            showDiv('dvAddNewGrade');


            return false;
        }

        $(document).ready(function () {
            $('#dvAddNewCategory').draggable();
            $('#dvAddNewGrade').draggable();
            $('#dvAddNewType').draggable();
        });


        function fnCloseNewGrade() {

            __doPostBack("<%=btnCloseGrade.UniqueID %>", "onclick");
        }

        function ValidateType() {

            var type = document.getElementById($('[id$=txtEvaluationType]').attr('id')).value;
            if (type.length <= 0) {
                alert('Checklist type cannot be blank.');
                return false;
            }
            else if (type.length > 50) {
                alert('Checklist type length exceeds 50 character.\n It should be less than 50 character.');
                return false;
            }
            else {
                return true;
            }

        }
        function ValidateGrade() {

            var type = document.getElementById($('[id$=txtGrade]').attr('id')).value;
            if (type.length <= 0) {
                alert('Grade name cannot be blank.');
                return false;
            }

            else {
                return true;
            }

        }

        function ValidateCategory() {
            var category = document.getElementById($('[id$=txtCategory]').attr('id')).value;
            if (category.trim().length <= 0) {
                alert('Category can not be blank.');
                return false;
            }
            else {
                return true;
            }
        }
        function RemoveSpecialChar(txtName) {
            txtName.value = txtName.value.replace(/[^a-zA-Z0-9-'& ]/g, '');

        }
        function fnAllowNumeric() {
            if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 8 && event.keyCode != 46 && event.keyCode > 31) {
                event.keyCode = 0;
                alert("Please enter only numeric value.");
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Checklist Type
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="page-content" style="height: 900px; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Checklist Type:</legend>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:LinkButton ID="lnkAddNewType" OnClientClick="javascript:showDiv('dvAddNewType');return true;"
                                                    runat="server" CssClass="linkbtn" OnClick="lnkAddNewType_Click">Add New Checklist Type</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="GridView_EvaluationType" runat="server" AutoGenerateColumns="False"
                                        OnRowUpdating="GridView_EvaluationType_RowUpdating" OnRowDeleting="GridView_EvaluationType_RowDeleting"
                                        OnRowEditing="GridView_EvaluationType_RowEditing" OnRowCancelingEdit="GridView_EvaluationType_RowCancelEdit"
                                        DataKeyNames="ID" EmptyDataText="No Record Found" AllowSorting="True" CaptionAlign="Bottom"
                                        CellPadding="4" GridLines="None" Width="100%" CssClass="grd">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Checklist Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEvaluation_Type" runat="server" Text='<%#Eval("Category_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEvaluation_Type" Font-Size="11px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("Category_Name")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="50px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnAccept" ToolTip="Update" runat="server" ImageUrl="~/images/accept.png"
                                                        CausesValidation="False" CommandName="Update" AlternateText="Update"></asp:ImageButton>
                                                    <asp:ImageButton ID="ImgBtnCancel" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                                        CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif"
                                                        CausesValidation="False" CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
                                                        CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this?')"
                                                        AlternateText="Delete"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
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
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Grading Type:</legend>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:LinkButton ID="lnkAddNewGrade" OnClientClick="Addnewgrade();" runat="server"
                                                    CssClass="linkbtn" OnClick="lnkAddNewGrade_Click">Add New Grade</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="GridView_Grading" runat="server" AutoGenerateColumns="False" OnRowUpdating="GridView_Grading_RowUpdating"
                                                    OnRowDeleting="GridView_Grading_RowDeleting" OnRowEditing="GridView_Grading_RowEditing"
                                                    OnRowCancelingEdit="GridView_Grading_RowCancelEdit" OnRowDataBound="GridView_Grading_RowDataBound"
                                                    DataKeyNames="ID" EmptyDataText="No Record Found" AllowSorting="True" CaptionAlign="Bottom"
                                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Grade Name" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrade_Name" runat="server" Text='<%#Eval("Grade_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtGrade_Name" Font-Size="11px" MaxLength="50" runat="server" Text='<%#Bind("Grade_Name")%>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Grade Type" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrade_Type" runat="server" Text='<%#Eval("Grade_Type_Text")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:RadioButtonList ID="rdoGradeType" runat="server" Text='<%#Bind("Grade_Type")%>'
                                                                    RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoGradeType_SelectedIndexChanged"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="Objective Type" Value="1" Selected="True"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Min" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMin" runat="server" Text='<%#Eval("Min")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlMin" Font-Size="11px" runat="server" Text='<%#Bind("Min")%>'
                                                                    Enabled="false" Width="50">
                                                                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Max" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMax" runat="server" Text='<%#Eval("Max")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlMax" Font-Size="11px" runat="server" Text='<%#Bind("Max")%>'
                                                                    Width="50" Enabled="false">
                                                                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="No of Options" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDivisions" runat="server" Text='<%#Eval("Divisions")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlDivisions" Font-Size="11px" MaxLength="10" runat="server"
                                                                    Text='<%#Bind("Divisions")%>' Width="50" Enabled="false">
                                                                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Criteria Options" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rdoOptions" runat="server" RepeatDirection="Horizontal"
                                                                    DataTextField="OptionText" DataValueField="OptionValue">
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-HorizontalAlign="Center">
                                                            <EditItemTemplate>
                                                                <asp:ImageButton ID="ImgBtnAccept" ToolTip="Update" runat="server" ImageUrl="~/images/accept.png"
                                                                    CausesValidation="False" CommandName="Update" AlternateText="Update"></asp:ImageButton>
                                                                <asp:ImageButton ID="ImgBtnCancel" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                                                    CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif"
                                                                    CausesValidation="False" CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
                                                                    CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this?')"
                                                                    AlternateText="Delete"></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EditRowStyle BackColor="#58FA82" />
                                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Category:</legend>
                        <asp:UpdatePanel ID="UpdatePanelCategory" runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:LinkButton ID="lnkAddNewCategory" OnClientClick="javascript:showDiv('dvAddNewCategory');return true;"
                                                    runat="server" CssClass="linkbtn" OnClick="lnkAddNewCategory_Click">Add New Category</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="gvCategory" runat="server" AutoGenerateColumns="False" OnRowUpdating="gvCategory_RowUpdating"
                                        OnRowDeleting="gvCategory_RowDeleting" OnRowEditing="gvCategory_RowEditing" OnRowCancelingEdit="gvCategory_RowCancelEdit"
                                        DataKeyNames="Category_ID" EmptyDataText="No Record Found" AllowSorting="True"
                                        CaptionAlign="Bottom" CellPadding="4" GridLines="None" Width="100%" CssClass="grd">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Category_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCategory" Font-Size="11px" MaxLength="50" runat="server" Text='<%#Bind("Category_Name")%>'
                                                        onkeyup="javascript:RemoveSpecialChar(this);"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="50px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnAccept" ToolTip="Update" runat="server" ImageUrl="~/images/accept.png"
                                                        CausesValidation="False" CommandName="Update" AlternateText="Update"></asp:ImageButton>
                                                    <asp:ImageButton ID="ImgBtnCancel" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                                        CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif"
                                                        CausesValidation="False" CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
                                                        CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this?')"
                                                        AlternateText="Delete"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
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
                </td>
            </tr>
        </table>
        <div id="dvAddNewType" title="Add New Checklist Type" class="modal-popup-container"
            style="width: 550px; left: 40%; top: 30%;">
            <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <table border="0" style="width: 100%;" cellpadding="10">
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Checklist Type: <b style="color: Red">*</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEvaluationType" Width="250px" runat="server" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveEvaType" CssClass="button-css" runat="server" Text="Save and Add New"
                                        OnClick="btnSaveEvaType_Click" OnClientClick="return ValidateType();" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndCloseEvaType" CssClass="button-css" runat="server" Text="Save and Close"
                                        OnClick="btnSaveAndCloseEvaType_Click" OnClientClick="return ValidateType();" />&nbsp;&nbsp;
                                    <asp:Button ID="Button3" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDiv('dvAddNewType')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="dvAddNewGrade" title="Add New Grade" class="modal-popup-container" style="width: 550px;
            left: 40%; top: 30%;">
            <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table border="0" style="width: 100%;" cellpadding="10">
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Grade Name: <b style="color: Red">*</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGrade" Width="250px" runat="server" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Grade Type:
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdoGradeType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rdoGradeType_SelectedIndexChanged">
                                        <asp:ListItem Text="Objective Type" Value="1" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr id="tr1" runat="server">
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Min. Point:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMin" runat="server" Width="50" AutoPostBack="true" OnSelectedIndexChanged="ddlDivisions_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="tr2" runat="server">
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Max. Point:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMax" runat="server" Width="50" AutoPostBack="true" OnSelectedIndexChanged="ddlDivisions_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="tr3" runat="server">
                                <td style="font-size: 11px; text-align: left; font-weight: bold; vertical-align: top;">
                                    <asp:Label ID="lblCaption" runat="server" Text="No. of Options"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDivisions" MaxLength="10" runat="server" AutoPostBack="true"
                                        Text='<%#Bind("Divisions")%>' Width="100" OnSelectedIndexChanged="ddlDivisions_SelectedIndexChanged">
                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                    </asp:DropDownList>
                                    <div id="dvrpt" runat="server">
                                        <asp:Repeater ID="rptOptions" runat="server">
                                            <HeaderTemplate>
                                                <table cellspacing="1" cellpadding="0" style="border: 1px solid gray">
                                                    <tr style="background-color: #01DFA5">
                                                        <td>
                                                            <asp:Label ID="lblValue" runat="server" Text="Value"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txtOptionText" runat="server" Text="Text"></asp:Label>
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtValue" runat="server" Text='<%#Eval("OptionValue")%>' Width="50px" onkeypress="return fnAllowNumeric()"
                                                            BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtText" runat="server" Text='<%#Eval("OptionText")%>' Width="200px"
                                                            BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Panel ID="pnlSubjective" runat="server" Visible="false">
                                        <asp:TextBox ID="txtSubjectiveText" runat="server" TextMode="MultiLine" Width="250px"
                                            Height="60px" ReadOnly="true"></asp:TextBox>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSaveGrade" CssClass="button-css" runat="server" Text="Save and Add New"
                                                OnClick="btnSaveGrade_Click" OnClientClick="return ValidateGrade()" />&nbsp;&nbsp;
                                            <asp:Button ID="btnSaveAndCloseGrade" CssClass="button-css" runat="server" Text="Save and Close"
                                                OnClick="btnSaveAndCloseGrade_Click" OnClientClick="return ValidateGrade()" />&nbsp;&nbsp;
                                            <asp:Button ID="btnCloseGrade" CssClass="button-css" runat="server" Text="Close"
                                                OnClientClick="closeDiv('dvAddNewGrade')" OnClick="btnCloseGrade_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="dvAddNewCategory" title="Add New Category" class="modal-popup-container"
            style="width: 550px; left: 40%; top: 30%;">
            <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <table border="0" style="width: 100%;" cellpadding="10">
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Category: <b style="color: Red">*</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCategory" Width="250px" runat="server" MaxLength="50" onkeyup="javascript:RemoveSpecialChar(this);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveCategory" CssClass="button-css" runat="server" Text="Save and Add New"
                                        OnClick="btnSaveCategory_Click" OnClientClick="return ValidateCategory();" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveandCloseCategory" CssClass="button-css" runat="server" Text="Save and Close"
                                        OnClick="btnSaveandCloseCategory_Click" OnClientClick="return ValidateCategory();" />&nbsp;&nbsp;
                                    <asp:Button ID="tbnCloseCategory" CssClass="button-css" runat="server" Text="Close"
                                        OnClientClick="closeDiv('dvAddNewCategory')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
