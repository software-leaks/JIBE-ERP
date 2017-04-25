<%@ Page Title="Interview Library" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="InterviewLibrary.aspx.cs" Inherits="Crew_InterviewLibrary" %>

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
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function showDiv(dv) {
            document.getElementById('dvAddNewGrade').title = "Add New Grade"
            document.getElementById('dvAddNewCategory').title = "Add New Category"
            showModal(dv);
        }
        function closeDiv(dv) {
            hideModal(dv);
        }

        $(document).ready(function () {
            $('#dvAddNewCategory').draggable();
            $('#dvAddNewGrade').draggable();
            $('#dvAddNewType').draggable();
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <div class="page-title">
         Interview Library
    </div>
 
     <asp:UpdateProgress ID="upUpdateProgress" runat="server">
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
        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
            <ContentTemplate>
                <div class="section-title-broad-lightyellow">
                    Grades
                </div>
                <div style="padding: 20px;">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: right">
                                <asp:Button ID="lnkAddNewGrade" OnClientClick="javascript:showDiv('dvAddNewGrade');return false;"
                                    runat="server" Text="Add New Grade" CssClass="linkbtn"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView_Grading" runat="server" AutoGenerateColumns="False" OnRowUpdating="GridView_Grading_RowUpdating"
                                    OnRowDeleting="GridView_Grading_RowDeleting" OnRowEditing="GridView_Grading_RowEditing"
                                    OnRowCancelingEdit="GridView_Grading_RowCancelEdit" OnRowDataBound="GridView_Grading_RowDataBound"
                                    DataKeyNames="ID" EmptyDataText="No Record Found" AllowSorting="True" CaptionAlign="Bottom"
                                    CellPadding="4" GridLines="None" Width="100%" CssClass="gridmain-css">
                                 <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Grade Name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrade_Name" runat="server" Text='<%#Eval("Grade_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtGrade_Name" Font-Size="11px" MaxLength="50" runat="server" Text='<%#Bind("Grade_Name")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grade Type" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrade_Type" runat="server" Text='<%#Eval("Grade_Type_Text")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:RadioButtonList ID="rdoGradeType" runat="server" Text='<%#Bind("Grade_Type")%>'
                                                    RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoGradeType_SelectedIndexChanged">
                                                    <asp:ListItem Text="Objective Type" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Subjective Type" Value="2"></asp:ListItem>
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
                                        <asp:TemplateField HeaderText="Grade Options" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="rdoOptions" runat="server" RepeatDirection="Horizontal"
                                                    DataTextField="OptionText" DataValueField="OptionValue">
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="ImgBtnAccept" ToolTip="Update" runat="server" ImageUrl="~/images/accept.png"
                                                    CausesValidation="False" CommandName="Update" AlternateText="Update"></asp:ImageButton>
                                                <asp:ImageButton ID="ImgBtnCancel" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                                    CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                                    CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                                                    CausesValidation="False" CommandName="Delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                    AlternateText="Delete"></asp:ImageButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ToolTip="Info" ImageUrl="~/Images/RecordInformation.png"
                                                    Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_LIB_INTERVIEWCRITERIAGRADING&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>'
                                                    AlternateText="info" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="crew-interview-grid-alternaterow" />
                                    <EditRowStyle CssClass="crew-interview-grid-editrow" />
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                               <%--     <HeaderStyle CssClass="crew-interview-grid-header" />--%>
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle CssClass="crew-interview-grid-row" />
                                    <SortedAscendingCellStyle CssClass="crew-interview-grid-col-sorted-asc" />
                                    <SortedAscendingHeaderStyle CssClass="crew-interview-grid-header-sorted-asc" />
                                    <SortedDescendingCellStyle CssClass="crew-interview-grid-col-sorted-desc" />
                                    <SortedDescendingHeaderStyle CssClass="crew-interview-grid-header-sorted-desc" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="section-title-broad-lightyellow">
                    Question Category
                </div>
                <div style="padding: 20px;">
                    <table>
                        <tr>
                            <td style="text-align: left">
                                Filter:&nbsp;<asp:TextBox ID="txtfilter" runat="server" AutoPostBack="true" OnTextChanged="txtfilter_TextChanged"
                                    BorderStyle="Solid" Height="20px" BorderWidth="1px"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                    WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                            </td>
                            <td style="text-align: right">
                                <div style="margin-bottom: 10px; vertical-align: bottom;">
                                    <%--<asp:ImageButton ID="lnkAddNewCategory" ImageUrl="../Images/Plus2.png" OnClientClick="showDiv('dvAddNewCategory');return false;" runat="server" ImageAlign="Middle" />--%>
                                    <asp:Button ID="lnkAddNewCategory" OnClientClick="javascript:showDiv('dvAddNewCategory');return false;"
                                        Text="Add New Category" runat="server" CssClass="linkbtn"></asp:Button>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="GridView_Category" runat="server" AutoGenerateColumns="False" OnRowUpdating="GridView_Category_RowUpdating"
                                    OnRowDeleting="GridView_Category_RowDeleting" OnRowEditing="GridView_Category_RowEditing"
                                    OnRowCancelingEdit="GridView_Category_RowCancelEdit" DataKeyNames="ID" EmptyDataText="No Record Found"
                                    AllowSorting="True" CaptionAlign="Bottom" CellPadding="4" ShowHeader="false"
                                    Font-Size="14px" GridLines="None" Width="600px" CssClass="gridmain-css">
                                  <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                  <RowStyle CssClass="RowStyle-css" />
                                  <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Category Name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory_Name" runat="server" Text='<%#Eval("Category_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCategory_Name" Font-Size="12px" MaxLength="50" Width="400px"
                                                    runat="server" Text='<%#Bind("Category_Name")%>'></asp:TextBox>
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
                                                <asp:ImageButton ID="LinkButton2" runat="server" ToolTip="Edit" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                                    CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                                                    CausesValidation="False" CommandName="Delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                    AlternateText="Delete"></asp:ImageButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ToolTip="Info" ImageUrl="~/Images/RecordInformation.png"
                                                    Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_LIB_INTERVIEWQUESTIONCATEGORY&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>'
                                                    AlternateText="info" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="crew-interview-grid-alternaterow" />
                                    <EditRowStyle CssClass="crew-interview-grid-editrow" />
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle CssClass="crew-interview-grid-row" />
                                    <SortedAscendingCellStyle CssClass="crew-interview-grid-col-sorted-asc" />
                                    <SortedAscendingHeaderStyle CssClass="crew-interview-grid-header-sorted-asc" />
                                    <SortedDescendingCellStyle CssClass="crew-interview-grid-col-sorted-desc" />
                                    <SortedDescendingHeaderStyle CssClass="crew-interview-grid-header-sorted-desc" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvAddNewCategory" title="Add New Category" class="modal-popup-container"
            style="width: 450px; left: 40%; top: 30%;">
            <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                    <ContentTemplate>
                        <table border="0" style="width: 100%;" cellpadding="10">
                            <tr>
                                <td style="font-size: 11px; font-weight: bold">
                                    Category Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCatName" Width="250px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveAndAdd" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnsave_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndClose" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndClose_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDiv('dvAddNewCategory')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="dvAddNewGrade" title="Add New Grade" class="modal-popup-container" style="width: 450px;
            left: 40%; top: 30%;">
            <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%;" cellpadding="10">
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Grade Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGrade" Width="250px" runat="server"></asp:TextBox>
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
                                        <asp:ListItem Text="Subjective Type" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Min Point:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMin" Font-Size="11px" runat="server" Width="50" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlDivisions_SelectedIndexChanged">
                                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Max Point:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMax" Font-Size="11px" runat="server" Width="50" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlDivisions_SelectedIndexChanged">
                                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold; vertical-align: top;">
                                    <asp:Label ID="lblCaption" runat="server" Text="No of Options"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDivisions" Font-Size="11px" MaxLength="10" runat="server"
                                        AutoPostBack="true" Text='<%#Bind("Divisions")%>' Width="100" OnSelectedIndexChanged="ddlDivisions_SelectedIndexChanged">
                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                    </asp:DropDownList>
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
                                                    <asp:TextBox ID="txtValue" runat="server" Text='<%#Eval("OptionValue")%>' Width="50px"
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
                                    <asp:Button ID="btnSaveGrade" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnSaveGrade_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndCloseGrade" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndCloseGrade_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCloseGrade" CssClass="button-css" runat="server" Text="Close"
                                        OnClientClick="closeDiv('dvAddNewGrade')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
