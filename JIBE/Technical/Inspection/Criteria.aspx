<%@ Page Title="Question Bank" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Criteria.aspx.cs" Inherits="CrewEvaluation_Criteria" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
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
        #dvAddNewCriteria
        {
            cursor: move;
        }
        .TextCSsShtyle
        {
            width: 125px;
        }
        .HeadetTHStyle
        {
            text-align: center;
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
        }
        function showDiv(dv) {
            document.getElementById('dvAddNewCriteria').title = "Add New Question"
            showModal(dv);
        }
        function closeDiv(dv) {
            hideModal(dv);
        }
        $(document).ready(function () {
            $('#dvAddNewCriteria').draggable();
        });

        // This function will not allow user to enter special character except '?' and '.'
        function blockSpecialChar(e) {
            var k = e.keyCode == 0 ? e.charCode : e.keyCode;
            return (k == 63 || (k > 64 && k < 91) || k == 46 || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57) || k == 32 || k == 44 || k == 45 || k == 13);
            return k;
        }

        //This is validation function to restrict special characters on save
        function specialcharecter() {
            //            var iChars = "!`@#$%^&*()+=-[]\\\';,/{}|\":<>~_";
            var iChars = "!`@#$%^&*()+=[]\\\';/{}|\":<>~_";
            var data = document.getElementById($('[id$=txtCriteria]').attr('id')).value;
            for (var i = 0; i < data.length; i++) {
                if (iChars.indexOf(data.charAt(i)) != -1) {
                    alert("Special characters are not allowed except (?,space,comma and (-)).");
                    //document.getElementById($('[id$=txtCriteria]').attr('id')).value = "";
                    return false;
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Question Bank
    </div>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="page-content" style="border: 1px solid #CEE3F6; z-index: -2; margin-top: -1px;
        overflow: auto;">
        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
            <ContentTemplate>
                <div style="text-align: center">
                    <table border="0">
                        <tr>
                            <td style="text-align: left">
                                Search:
                            </td>
                            <td>
                                <asp:TextBox ID="txtfilter" runat="server" CssClass="textbox-css TextCSsShtyle" AutoPostBack="true"
                                    OnTextChanged="txtfilter_TextChanged"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                    WatermarkText="Type to Search" WatermarkCssClass="watermarked TextCSsShtyle" />
                            </td>
                            <td>
                                <%--Checklist Type--%>
                                Category
                            </td>
                            <td style="text-align: right">
                                <%--<asp:DropDownList ID="ddlCategory" runat="server" DataValueField="ID" DataTextField="Category_Name"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                </asp:DropDownList>--%>
                                <asp:DropDownList ID="ddlCategory" runat="server" DataValueField="ID" DataTextField="Category_Name"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: right">
                                <asp:LinkButton ID="lnkAddNewQuestion" runat="server" CssClass="linkbtn" OnClientClick="showDiv('dvAddNewCriteria');return true;"
                                    OnClick="lnkAddNewQuestion_Click">Add New Question</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView_Criteria" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CaptionAlign="Bottom" CellPadding="4" DataKeyNames="Criteria_ID" EmptyDataText="No Record Found"
                                    GridLines="None" OnRowCancelingEdit="GridView_Criteria_RowCancelEdit" OnRowDataBound="GridView_Criteria_RowDataBound"
                                    OnRowDeleting="GridView_Criteria_RowDeleting" OnRowEditing="GridView_Criteria_RowEditing"
                                    OnRowUpdating="GridView_Criteria_RowUpdating" OnSorted="GridView_Criteria_Sorted"
                                    OnSorting="GridView_Criteria_Sorting" Width="100%" BorderStyle="Solid" BorderColor="#cccccc"
                                    CssClass="gridmain-css" BorderWidth="1px">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" HeaderStyle-ForeColor="Black" SortExpression="Criteria_ID"
                                            Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("Criteria_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle ForeColor="Black" />
                                            <ItemStyle HorizontalAlign="Left" Width="40px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Question" HeaderStyle-ForeColor="Black" SortExpression="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCriteria" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCriteria" runat="server" Font-Size="11px" MaxLength="1000" TextMode="MultiLine"
                                                    onkeypress="return blockSpecialChar(event)" Width="350px" Height="60px" Text='<%#Bind("Description")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle ForeColor="Black" />
                                            <ItemStyle HorizontalAlign="Left" Width="400px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category" HeaderStyle-ForeColor="Black" SortExpression="Category_Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory_Name" runat="server" Text='<%#Eval("Category_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlCategory_Name" runat="server" DataTextField="Category_Name"
                                                    DataValueField="ID" DataSourceID="objDS_CatName" SelectedValue='<%#Bind("ID") %>' />
                                                <asp:ObjectDataSource ID="objDS_CatName" runat="server" SelectMethod="Get_Search_CheckListCategory"
                                                    TypeName="SMS.Business.Inspection.BLL_INSP_Checklist" OldValuesParameterFormatString="original_{0}">
                                                    <SelectParameters>
                                                        <asp:Parameter Name="searchtext" Type="String" DefaultValue="" ConvertEmptyStringToNull="true" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </EditItemTemplate>
                                            <HeaderStyle ForeColor="Black" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Criteria Options">
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="rdoOptions" runat="server" DataTextField="OptionText" DataValueField="OptionValue"
                                                    RepeatDirection="Horizontal">
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlGradingType" runat="server" DataTextField="Grade_Name" DataValueField="ID"
                                                    MaxLength="50" DataSourceID="objDS_GradingType" SelectedValue='<%# Bind("Grading_Type") %>' />
                                                <asp:ObjectDataSource ID="objDS_GradingType" runat="server" SelectMethod="Get_Grades"
                                                    TypeName="SMS.Business.Inspection.BLL_INSP_Checklist" OldValuesParameterFormatString="original_{0}">
                                                    <SelectParameters>
                                                        <asp:Parameter Name="CheckList_ID" Type="Int32" DefaultValue="" ConvertEmptyStringToNull="true" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="50px">
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="ImgBtnAccept" ToolTip="Update" runat="server" AlternateText="Update"
                                                    CausesValidation="False" CommandName="Update" ImageUrl="~/images/accept.png" />
                                                <asp:ImageButton ID="ImgBtnCancel" ToolTip="Cancel" runat="server" AlternateText="Cancel"
                                                    CausesValidation="False" CommandName="Cancel" ImageUrl="~/images/reject.png" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" AlternateText="Edit"
                                                    CausesValidation="False" CommandName="Edit" ImageUrl="~/images/edit.gif" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" AlternateText="Delete"
                                                    CausesValidation="False" CommandName="Delete" ImageUrl="~/images/delete.png"
                                                    OnClientClick="return confirm('Are you sure, you want to  delete ?')" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" CssClass="HeadetTHStyle" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#58FA82" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="Bind_QuestionBank" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvAddNewCriteria" title=" Add New Question" class="modal-popup-container"
            style="width: 550px; left: 40%; top: 30%;">
            <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                    <ContentTemplate>
                        <table border="0" style="width: 100%;" cellpadding="10">
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Question:
                                </td>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; color: Red; font-weight: bold">
                                    *
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCriteria" Width="400px" Height="100px" runat="server" MaxLength="1000"
                                        onkeypress="return blockSpecialChar(event)" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Category:
                                </td>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; color: Red; font-weight: bold">
                                    *
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCatName" Width="400px" runat="server" DataTextField="Category_Name"
                                        DataValueField="ID" />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Grading type:
                                </td>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; color: Red; font-weight: bold">
                                    *
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGradingType" Width="400px" DataTextField="Grade_Name" DataValueField="ID"
                                        runat="server" OnSelectedIndexChanged="ddlGradingType_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr runat="server" id="trGradings">
                                <td style="vertical-align: top; border-width: 1px; font-weight: bold; font-size: 11px;">
                                    Gradings
                                </td>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; color: Red; font-weight: bold">
                                    &nbsp;
                                </td>
                                <td style="vertical-align: top; border-width: 1px">
                                    <asp:RadioButtonList ID="rdoGradings" runat="server" DataTextField="OptionText" DataValueField="OptionValue"
                                        Enabled="false">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="font-size: 11px; text-align: center; border-width: 1px">
                                    <asp:Button ID="btnSaveAndAdd" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnsave_Click" OnClientClick="return specialcharecter();" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndClose" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndClose_Click" OnClientClick="return specialcharecter();" />&nbsp;&nbsp;
                                    <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDiv('dvAddNewCriteria')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
