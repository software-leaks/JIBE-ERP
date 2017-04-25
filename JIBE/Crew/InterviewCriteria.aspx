<%@ Page Title="Select Interview Criteria" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="InterviewCriteria.aspx.cs" Inherits="Crew_InterviewCriteria" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/crew_interview_css.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function showDivAddNewCriteria() {
            showModal('dvAddNewCriteria');
        }
        function closeDivAddNewCriteria() {
            hideModal('dvAddNewCriteria');
        }
        function showDivAddNewGrade() {
            showModal('dvAddNewGrade');
        }
        function closeDivAddNewGrade() {
            hideModal('dvAddNewGrade');
        }
        $(document).ready(function () {
            $('#dvAddNewCriteria').draggable();
        });       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<div class="page-title">
             Select Criteria for Interview
            </div>
 <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
             
 
    <div id="page-content" class="page-content-div">
        <div class="section-title-broad-lightyellow">
            <table style="width: 100%">
                <tr>
                    <td>
                        Assigned Criteria List
                    </td>
                    <td style="text-align: right">
                        <asp:LinkButton ID="lnkAddNewQuestion" runat="server" CssClass="linkbtn" OnClientClick="showDivAddNewCriteria();return false;">Add New Question</asp:LinkButton>
                        <asp:LinkButton ID="lnkGoBackToInterview" runat="server" CssClass="linkbtn" OnClick="lnkGoBackToInterview_Click">Go Back to Interview Admin</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-top: 10px; max-height: 400px; overflow: auto;">
            <asp:UpdatePanel ID="Update1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView_AssignedCriteria" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CaptionAlign="Bottom" CellPadding="4" DataKeyNames="QID,RankID" EmptyDataText="No Record Found"
                        ForeColor="#333333" GridLines="None" OnRowDataBound="GridView_AssignedCriteria_RowDataBound"
                        OnSorted="GridView_AssignedCriteria_Sorted" OnSorting="GridView_AssignedCriteria_Sorting"
                        OnRowCommand="GridView_AssignedCriteria_RowCommand" Width="100%">
                        <Columns>
                            
                            <asp:TemplateField HeaderText="Question" SortExpression="Question" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <table style="width: 100%" cellpadding="4px">
                                        <tr>
                                            <td style="width: 150px">
                                                Q.No.
                                            </td>
                                            <td>
                                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("QID")%>'></asp:Label><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Category:
                                            </td>
                                            <td style="font-weight: bold">
                                                <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Category_Name")%>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Criteria:
                                            </td>
                                            <td style="min-height: 50px; background-color: White; border: 1px solid #cccccc;
                                                vertical-align: top;">
                                                <asp:Label ID="lblCriteria" runat="server" Text='<%#Eval("Question").ToString().Replace("\n","<br>")%>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Criteria Options:
                                            </td>
                                            <td style="font-weight: bold">
                                                <asp:RadioButtonList ID="rdoOptions" runat="server" DataTextField="OptionText" DataValueField="OptionValue"
                                                    RepeatDirection="Horizontal">
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Answer:
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("answer").ToString().Replace("\n","<br>")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remove" ShowHeader="False" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="LinkButton1del" runat="server" AlternateText="Remove" CausesValidation="False"
                                        CommandName="RemoveCriteria" CommandArgument='<%#Eval("QID")%>' ImageUrl="~/images/reject.png"
                                        OnClientClick="return confirm('Are you sure, you want to  remove the criteria from the interview ?')" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order" ShowHeader="False">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="ImgBtnMoveUp" runat="server" ImageUrl="~/images/Arrow2 - Up.png"
                                                    CausesValidation="False" CommandName="MoveUp" CommandArgument='<%#Eval("QID")%>'
                                                    AlternateText="Up"></asp:ImageButton>
                                            </td>
                                            <td>
                                                &nbsp;&nbsp;<asp:ImageButton ID="ImgBtnMoveDown" runat="server" ImageUrl="~/images/Arrow2 - Down.png"
                                                    CausesValidation="False" CommandName="MoveDown" CommandArgument='<%#Eval("QID")%>'
                                                    AlternateText="Down"></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle CssClass="crew-interview-grid-alternaterow" BackColor="#EFF2FB" />
                        <EditRowStyle CssClass="crew-interview-grid-editrow" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle CssClass="crew-interview-grid-header" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle CssClass="crew-interview-grid-row" />
                        <SortedAscendingCellStyle CssClass="crew-interview-grid-col-sorted-asc" />
                        <SortedAscendingHeaderStyle CssClass="crew-interview-grid-header-sorted-asc" />
                        <SortedDescendingCellStyle CssClass="crew-interview-grid-col-sorted-desc" />
                        <SortedDescendingHeaderStyle CssClass="crew-interview-grid-header-sorted-desc" />
                        <SelectedRowStyle CssClass="crew-interview-grid-selected-row" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="section-title-broad-lightyellow" style="margin-top: 20px;">
            <table style="width: 100%">
                <tr>
                    <td>
                        Un-Assigned Criteria List
                    </td>
                    <td style="text-align: right">
                        <asp:LinkButton ID="LinkButton4" runat="server" CssClass="linkbtn" OnClick="lnkAddToInterview_Click"
                            Text="Add to Interview"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-top: 10px; max-height: 400px; overflow: auto;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table border="0">
                        <tr>
                            <td style="text-align: left">
                                Category:
                            </td>
                            <td style="width:430x">
                                <asp:DropDownList ID="ddlCategoryFilter" runat="server" DataValueField="ID" DataTextField="Category_Name"
                                                                Width="406px" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoryFilter_SelectedIndexChanged">
                                                            </asp:DropDownList>
                            </td>
                            <td style="text-align: left">
                                Search:
                            </td>
                            <td>
                                <asp:TextBox ID="txtfilter" runat="server" CssClass="textbox-css" AutoPostBack="true" Width="200px"
                                    OnTextChanged="txtfilter_TextChanged"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                    WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="GridView_UnAssignedCriteria" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" CaptionAlign="Bottom" CellPadding="4" DataKeyNames="ID"
                        EmptyDataText="No Record Found" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView_UnAssignedCriteria_RowDataBound"
                        OnSorted="GridView_UnAssignedCriteria_Sorted" OnSorting="GridView_UnAssignedCriteria_Sorting"
                        Width="100%">
                        <Columns>
                            
                            <asp:TemplateField HeaderText="Question" SortExpression="Question" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <table style="width: 100%" cellpadding="4px">
                                        <tr>
                                            <td style="width: 150px">
                                                Q.No.
                                            </td>
                                            <td>
                                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>'></asp:Label><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Category:
                                            </td>
                                            <td style="font-weight: bold">
                                                <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Category_Name")%>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Criteria:
                                            </td>
                                            <td style="min-height: 50px; background-color: White; border: 1px solid #cccccc;
                                                vertical-align: top;">
                                                <asp:Label ID="lblCriteria" runat="server" Text='<%#Eval("Question").ToString().Replace("\n","<br>")%>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Criteria Options:
                                            </td>
                                            <td style="font-weight: bold">
                                                <asp:RadioButtonList ID="rdoOptions" runat="server" DataTextField="OptionText" DataValueField="OptionValue"
                                                    RepeatDirection="Horizontal">
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Answer:
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("answer").ToString().Replace("\n","<br>")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" CausesValidation="False" CommandName="Select" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle CssClass="crew-interview-grid-alternaterow" BackColor="#EFF2FB" />
                        <EditRowStyle CssClass="crew-interview-grid-editrow" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle CssClass="crew-interview-grid-header" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle CssClass="crew-interview-grid-row" />
                        <SortedAscendingCellStyle CssClass="crew-interview-grid-col-sorted-asc" />
                        <SortedAscendingHeaderStyle CssClass="crew-interview-grid-header-sorted-asc" />
                        <SortedDescendingCellStyle CssClass="crew-interview-grid-col-sorted-desc" />
                        <SortedDescendingHeaderStyle CssClass="crew-interview-grid-header-sorted-desc" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="dvAddNewCriteria" title="Add New Question" class="modal-popup-container"
            style="width: 60%; left: 20%; top: 10%;">
            <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Question:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCriteria" CssClass="textbox-css" Width="99%" Height="100px" runat="server"
                                        MaxLength="4000" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Appropriate Answer:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAnswer" CssClass="textbox-css" Width="99%" Height="100px" runat="server"
                                        MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Category:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCategory" runat="server" DataValueField="ID" DataTextField="Category_Name"
                                        Width="406px" AutoPostBack="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Question Type:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlType" runat="server" Width="406px" Font-Size="Small" ForeColor="#333333"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Objective Type" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Subjective Type" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlType"
                                        ToolTip="Please Select Type" ValidationGroup="group" InitialValue="-Select-"
                                        runat="server" ErrorMessage="Please Select Type">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Grading Type:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGradingType" Width="406px" DataTextField="Grade_Name" DataValueField="ID"
                                        runat="server" OnSelectedIndexChanged="ddlGradingType_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlGradingType"
                                        ToolTip="Please Select Type" ValidationGroup="group" InitialValue="-Select-"
                                        runat="server" ErrorMessage="Please Grading Type">*</asp:RequiredFieldValidator>
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
                                    <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDivAddNewCriteria(); return false;" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
