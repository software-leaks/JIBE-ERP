<%@ Page Title="Select Criteria" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SelectCriteria.aspx.cs" Inherits="CrewEvaluation_SelectCriteria" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
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
            color: Black;
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
            border: 1px solid #cccccc;
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
        function showDivAddNewCriteria() {
            document.getElementById("dvAddNewCriteria").style.display = "block";
        }
        function closeDivAddNewCriteria() {
            document.getElementById("dvAddNewCriteria").style.display = "None";
        }
        $(document).ready(function () {
            $('#dvAddNewCriteria').draggable();
        });       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                        color: black">
                        <img src="../Images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="pageTitle" class="gradiant-css-blue" style="font-size: 12px; border: 1px solid #CEE3F6;
        text-align: center; padding: 3px; font-weight: bold;">
        <asp:Label ID="lblPageTitle" runat="server" Text="Select Criteria for Evaluation"></asp:Label>
    </div>
    <div id="page-content" style="border: 1px solid #CEE3F6; z-index: -2; margin-top: -1px;
        overflow: auto;">
        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
            <ContentTemplate>
                <div style="text-align: center; padding: 2px;">
                    <asp:Panel ID="pnlAssignedCriteria" runat="server">
                        <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                            <legend>Assigned Criteria List:</legend>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="linkbtn" OnClientClick="showDivAddNewCriteria();return false;">Add New Question</asp:LinkButton>
                                        <asp:LinkButton ID="lnkGoBackToEvaluation" runat="server" CssClass="linkbtn" OnClick="lnkGoBackToEvaluation_Click">Go Back to Evaluation</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="GridView_AssignedCriteria" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            CaptionAlign="Bottom" CellPadding="4" DataKeyNames="Criteria_ID" EmptyDataText="No Record Found"
                                            ForeColor="#333333" GridLines="None" OnRowDataBound="GridView_AssignedCriteria_RowDataBound"
                                            OnRowDeleting="GridView_AssignedCriteria_RowDeleting" OnSorted="GridView_AssignedCriteria_Sorted"
                                            OnSorting="GridView_AssignedCriteria_Sorting" OnRowCommand="GridView_AssignedCriteria_RowCommand"
                                            Width="100%" BorderStyle="Solid" BorderColor="#cccccc" BorderWidth="1px">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" SortExpression="Criteria_ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("Criteria_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Question" SortExpression="Criteria">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCriteria" runat="server" Text='<%#Eval("Criteria")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category Name" SortExpression="Category_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCategory_Name" runat="server" Text='<%#Eval("Category_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mandatory Remark Options">
                                                    <ItemTemplate>
                                                        <asp:CheckBoxList ID="chkOptions" runat="server" DataTextField="OptionText" DataValueField="OptionValue"
                                                            RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                       <%-- <asp:RadioButtonList ID="rdoOptions" runat="server" DataTextField="OptionText" DataValueField="OptionValue"
                                                            RepeatDirection="Horizontal">
                                                        </asp:RadioButtonList>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="LinkButton1del" runat="server" AlternateText="Delete" CausesValidation="False"
                                                            CommandName="Delete" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to  remove the criteria from the evaluation ?')" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="ImgBtnMoveUp" runat="server" ImageUrl="~/images/Arrow2 - Up.png"
                                                                        CausesValidation="False" CommandName="MoveUp" CommandArgument='<%#Eval("Criteria_ID")%>'
                                                                        AlternateText="Up"></asp:ImageButton>
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp;<asp:ImageButton ID="ImgBtnMoveDown" runat="server" ImageUrl="~/images/Arrow2 - Down.png"
                                                                        CausesValidation="False" CommandName="MoveDown" CommandArgument='<%#Eval("Criteria_ID")%>'
                                                                        AlternateText="Down"></asp:ImageButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#58FA82" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF6FC" ForeColor="#333333" CssClass="grid-row" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="pnlUnAssignedCriteria" runat="server">
                        <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                            <legend>Un-Assigned Criteria List:</legend>
                            <table border="0">
                                <tr>
                                    <td style="text-align: left">
                                        Search:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfilter" runat="server" CssClass="textbox-css" AutoPostBack="true"
                                            OnTextChanged="txtfilter_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td>
                                        Category
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCategory" runat="server" DataValueField="ID" DataTextField="Category_Name"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView_UnAssignedCriteria" runat="server" AllowSorting="True"
                                            AutoGenerateColumns="False" CaptionAlign="Bottom" CellPadding="4" DataKeyNames="Criteria_ID"
                                            EmptyDataText="No Record Found" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView_UnAssignedCriteria_RowDataBound"
                                            OnSorted="GridView_UnAssignedCriteria_Sorted" OnSorting="GridView_UnAssignedCriteria_Sorting"
                                            Width="100%" BorderStyle="Solid" BorderColor="#cccccc" BorderWidth="1px" SkinID="Office2007">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" SortExpression="Criteria_ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("Criteria_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Question" SortExpression="Criteria">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCriteria" runat="server" Text='<%#Eval("Criteria")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category Name" SortExpression="Category_Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCategory_Name" runat="server" Text='<%#Eval("Category_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Select Mandatory Remark Options">
                                                    <ItemTemplate>
                                                        <asp:CheckBoxList ID="chkOptions" runat="server" DataTextField="OptionText" DataValueField="OptionValue"
                                                            RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                        <%--<asp:RadioButtonList ID="rdoOptions" runat="server" DataTextField="OptionText" DataValueField="OptionValue"
                                                            RepeatDirection="Horizontal">
                                                        </asp:RadioButtonList>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Select" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" CausesValidation="False" CommandName="Select" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#58FA82" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF6FC" ForeColor="#333333" CssClass="grid-row" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:LinkButton ID="LinkButton4" runat="server" CssClass="linkbtn" OnClick="lnkAddToEvaluation_Click">Add to Evaluation</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvAddNewCriteria" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
            border-style: solid; border-width: 1px; position: absolute; left: 40%; top: 20%;
            z-index: 1; color: black">
            <div class="header">
                <h4>
                    Add New Question</h4>
            </div>
            <div class="content">
                <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                    <ContentTemplate>
                        <table style="border-style: none; border-color: Silver; border-width: 1px; width: 100%;
                            background-color: #F3F3FD;">
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Question:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCriteria" CssClass="textbox-css" Width="400px" Height="100px"
                                        runat="server" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Category Name:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCatName" CssClass="textbox-css" Width="400px" runat="server"
                                        DataTextField="Category_Name" DataValueField="ID" />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Grading Type:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGradingType" Width="400px" DataTextField="Grade_Name" DataValueField="ID"
                                        runat="server" OnSelectedIndexChanged="ddlGradingType_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; border-width: 1px">
                                    Gradings
                                </td>
                                <td style="vertical-align: top; border-width: 1px">
                                    <asp:RadioButtonList ID="rdoGradings" runat="server" DataTextField="OptionText" DataValueField="OptionValue"
                                        Enabled="false">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center; border-width: 1px">
                                    <asp:Button ID="btnSaveAndAdd" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnsave_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndClose" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndClose_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDivAddNewCriteria()" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
