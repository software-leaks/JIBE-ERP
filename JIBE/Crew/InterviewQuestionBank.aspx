<%@ Page Title="Interview Question Bank" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="InterviewQuestionBank.aspx.cs" Inherits="Crew_InterviewQuestionBank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/Thesaurus.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.selected-text-sharer.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.highlight.js" type="text/javascript"></script>
    <link href="../Styles/crew_interview_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
      <style type="text/css">
         body, html
        {
            overflow-x: hidden;
        }
     </style>
    <script language="javascript" type="text/javascript">
        function showDivAddNewCriteria() {
            document.getElementById('dvAddNewCriteria').title = "Add New Question"
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
            //$("body span").highlight(["interview", "ship"], { element: 'a', className: 'jQueryLink' });
            //$("body span a.jQueryLink").attr({ href: 'javascript:alert()' });
        });       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
      <div class="page-title">
        Interview Question Bank
    </div>

    
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

    <div id="page-content" class="page-content-div">
        <div class="section-title-broad-lightyellow">
            Question Bank
        </div>
        <div id="dvQuestionBank" style="padding-top: 20px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table border="0" style="width: 100%">
                        <tr>
                            <td style="text-align: left">
                                Category:
                            </td>
                            <td style="width: 430px">
                                <asp:DropDownList ID="ddlCategoryFilter" runat="server" DataValueField="ID" DataTextField="Category_Name"
                                    Width="406px" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoryFilter_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left">
                                Search:
                            </td>
                            <td>
                                <asp:TextBox ID="txtfilter" runat="server" CssClass="textbox-css" AutoPostBack="true"
                                    OnTextChanged="txtfilter_TextChanged"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                <asp:LinkButton ID="lnkAddNewQuestion" runat="server" CssClass="linkbtn" OnClientClick="showDivAddNewCriteria();return false;">Add New Question</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <div id="dvInterviewSheet" style="margin-top: 2px;" class="interview-questionbank-fixed">
                                    <asp:GridView ID="GridView_Criteria" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        CaptionAlign="Bottom" CellPadding="4" DataKeyNames="ID" EmptyDataText="No Record Found"
                                        GridLines="None" OnRowCancelingEdit="GridView_Criteria_RowCancelEdit"
                                        OnRowDataBound="GridView_Criteria_RowDataBound" OnRowDeleting="GridView_Criteria_RowDeleting"
                                        OnRowEditing="GridView_Criteria_RowEditing" OnRowUpdating="GridView_Criteria_RowUpdating"
                                        OnSorted="GridView_Criteria_Sorted" OnSorting="GridView_Criteria_Sorting" Width="100%"
                                        CssClass="gridmain-css">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                       <RowStyle CssClass="RowStyle-css" />
                                       <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Question" HeaderStyle-ForeColor="Black" SortExpression="ID" HeaderStyle-HorizontalAlign="Left">
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
                                                                Answer Reference:
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("answer").ToString().Replace("\n","<br>")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <table style="width: 100%" cellpadding="4px">
                                                        <tr>
                                                            <td style="width: 150px">
                                                                Q.No.
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblID1" runat="server" Text='<%#Eval("ID")%>'></asp:Label><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Category:
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlCategory" runat="server" DataValueField="ID" DataTextField="Category_Name"
                                                                    Width="406px" AutoPostBack="false">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Criteria:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCriteria" runat="server" Font-Size="11px" MaxLength="1000" TextMode="MultiLine"
                                                                    Width="100%" Height="60px" Text='<%#Bind("Question")%>'></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Criteria Options:
                                                            </td>
                                                            <td style="font-weight: bold">
                                                                <asp:DropDownList ID="ddlGradingType" runat="server" DataSourceID="objDS_GradingType"
                                                                    DataTextField="Grade_Name" DataValueField="ID" Text='<%#Bind("Grading_Type")%>' />
                                                                <asp:ObjectDataSource ID="objDS_GradingType" runat="server" SelectMethod="Get_GradingList"
                                                                    TypeName="SMS.Business.Crew.BLL_Crew_Interview"></asp:ObjectDataSource>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Answer:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAnswer" runat="server" Font-Size="11px" MaxLength="1000" TextMode="MultiLine"
                                                                    Width="100%" Height="60px" Text='<%#Bind("Answer")%>'></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="50px">
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnAccept" runat="server" ToolTip="Update" AlternateText="Update" CausesValidation="False"
                                                        CommandName="Update" ImageUrl="~/images/accept.png" />
                                                    <asp:ImageButton ID="ImgBtnCancel" runat="server" AlternateText="Cancel" ToolTip="Cancel" CausesValidation="False"
                                                        CommandName="Cancel" ImageUrl="~/images/reject.png" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton2" runat="server" ToolTip="Edit" AlternateText="Edit" CausesValidation="False"
                                                        CommandName="Edit" ImageUrl="~/images/edit.gif" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" AlternateText="Delete" CausesValidation="False"
                                                        CommandName="Delete" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to  delete ?')" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Image ID="imgRecordInfo" ToolTip="Info" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                        Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_LIB_INTERVIEWQUESTIONS&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>'
                                                        AlternateText="info" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle CssClass="crew-interview-grid-alternaterow" BackColor="#EFF2FB" />
                                        <EditRowStyle CssClass="crew-interview-grid-editrow" />
                                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle CssClass="crew-interview-grid-row" />
                                        <SortedAscendingCellStyle CssClass="crew-interview-grid-col-sorted-asc" />
                                        <SortedAscendingHeaderStyle CssClass="crew-interview-grid-header-sorted-asc" />
                                        <SortedDescendingCellStyle CssClass="crew-interview-grid-col-sorted-desc" />
                                        <SortedDescendingHeaderStyle CssClass="crew-interview-grid-header-sorted-desc" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
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
                                    <asp:TextBox ID="txtCriteria" CssClass="textbox-css" Width="99%" Height="300px" runat="server"
                                        MaxLength="4000" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Answer Reference:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAnswer" CssClass="textbox-css" Width="99%" Height="200px" runat="server"
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
                                        AutoPostBack="false">
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
                                        RepeatDirection="Horizontal" Enabled="false">
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
