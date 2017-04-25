<%@ Page Title="Purchase Questionnaire" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Purchase_Questionnaire.aspx.cs" Inherits="Purchase_Purchase_Questionnaire" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/crew_interview_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
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
        #dvAddNewCriteria{cursor:move;}
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
        function closeDiv(dv) 
        {
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
            $('#dvAddNewGrade').draggable();
        });
        function ValidateGrading() 
        {
            if ($("#<%=txtGrade.ClientID%>").val().trim() == '') 
            {
                alert('Enter Grade Name');
                $("#<%=txtGrade.ClientID%>").focus();
                return false;
            }

            if ($('#<%= rdoGradeType.ClientID %> input:checked').val() == "1") {

                if ($("#<%=ddlDivisions.ClientID%>").val() == "0") 
                {
                    alert('Select Options');
                    $("#<%=ddlDivisions.ClientID%>").focus();
                    return false;
                }
                
                
            }
        }
        function ValidateQuestion() 
        {
            if ($("#<%=txtQuestion.ClientID%>").val().trim() == '') {
                alert('Enter Question');
                $("#<%=txtQuestion.ClientID%>").focus()
                return false;
            }
            if ($("#<%=ddlCatName.ClientID%>").val() == '0') {
                alert('Select Department');
                $("#<%=ddlCatName.ClientID%>").focus();
                return false;
            }
            if ($("#<%=ddlGradingType.ClientID%>").val() == '0') {
                alert('Select Grading Type');
                $("#<%=ddlGradingType.ClientID%>").focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Purchase Questionnaire
    </div>
     <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
   
    <div id="page-content" style="height: 620px; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto;">
        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
            <ContentTemplate>
                <div style="text-align: center">
                    <table border="0" cellpadding="1" cellspacing="5" width="25%">
                        <tr>
                            <td style="text-align: left">
                               <b> Search: </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfilter" runat="server" CssClass="textbox-css" AutoPostBack="true"
                                    OnTextChanged="txtfilter_TextChanged"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                    WatermarkText="Questions to Search" WatermarkCssClass="watermarked" />
                            </td>
                            <td>
                               <b> Department: </b>
                            </td>
                            <td style="text-align: right">
                                <asp:DropDownList ID="ddlCategory" runat="server"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    
                    <table style="width: 100%" cellpadding="1" cellspacing="0">
                        <tr>
                            <td style="text-align: right">
                                <asp:LinkButton ID="lnkAddNewQuestion" runat="server" CssClass="linkbtn" OnClientClick="showDiv('dvAddNewCriteria');return false;">Add New Question</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                             <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Questions:</legend>
                                <asp:GridView ID="grdQuestion" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    CaptionAlign="Bottom" CellPadding="4" DataKeyNames="Question_ID" EmptyDataText="No Record Found"
                                                    ForeColor="#333333" GridLines="None" OnRowCancelingEdit="grdQuestion_RowCancelingEdit"
                                                    OnRowDataBound="grdQuestion_RowDataBound" OnRowDeleting="grdQuestion_RowDeleting"
                                                    OnRowEditing="grdQuestion_RowEditing" OnRowUpdating="grdQuestion_RowUpdating"
                                                    Width="100%" BorderStyle="Solid" BorderColor="#cccccc" BorderWidth="1px">
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("Question_ID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="40px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Question">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("Question")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtgrdQuestion" runat="server" Font-Size="11px" MaxLength="1000"
                                                                    TextMode="MultiLine" Width="350px" Height="60px" Text='<%#Bind("Question")%>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="400px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Department" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                                            </ItemTemplate>
                                                             <EditItemTemplate>
                                                                <asp:HiddenField ID="hdnGridType" runat="server" ClientIDMode="Static"  Value='<%# Bind("Type_ID")%>' />
                                                                <asp:DropDownList ID="ddlGridType" runat="server" DataTextField="Description" DataValueField="Code"
                                                                    MaxLength="50" DataSourceID="ObjectDataSource2" Text='<%#Bind("Type_ID")%>' />
                                                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="Get_GradingType"
                                                                    TypeName="SMS.Business.PURC.BLL_PURC_Common"></asp:ObjectDataSource>
                                                            </EditItemTemplate>
                                                           
                                                            <ItemStyle HorizontalAlign="Left"/>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Grade Options" >
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rdoOptions" runat="server" DataTextField="optiontext" DataValueField="optionvalue"
                                                                    RepeatDirection="Horizontal">
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                             <asp:HiddenField ID="hdnGradingTypeID" runat="server" ClientIDMode="Static"  Value='<%#Bind("Grading_Type")%>' />
                                                              <asp:DropDownList ID="ddlGradingType" runat="server" DataTextField="Grade_Name" DataValueField="ID"
                                                                    MaxLength="50" DataSourceID="ObjectDataSource1" Text='<%#Bind("Grading_Type")%>' />
                                                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get_GradingList"
                                                                    TypeName="SMS.Business.PURC.BLL_PURC_Common"></asp:ObjectDataSource>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" Visible="false" ShowHeader="False" HeaderStyle-Width="50px">
                                                            <EditItemTemplate>
                                                                <asp:ImageButton ID="ImgBtnAccept" runat="server" AlternateText="Update" CausesValidation="False"
                                                                    CommandName="Update" ImageUrl="~/images/accept.png" />
                                                                <asp:ImageButton ID="ImgBtnCancel" runat="server" AlternateText="Cancel" CausesValidation="False"
                                                                    CommandName="Cancel" ImageUrl="~/images/reject.png" />
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="LinkButton2" runat="server" AlternateText="Edit" CausesValidation="False"
                                                                    CommandName="Edit" Visible="false"  ImageUrl="~/images/edit.gif" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQUESTION_IS_USED" runat="server" style="display:none"  Text='<%#Eval("QUESTION_IS_USED")%>'></asp:Label>
                                                                <asp:ImageButton ID="LinkButton1del" runat="server" AlternateText="Delete" CausesValidation="False"
                                                                    CommandName="Delete" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to  delete ?')" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
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
                                                </fieldset>
                            </td>

                        </tr>
                        <tr>
                        <td colspan="2">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Grading Types:</legend>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: right">
                                                  <asp:LinkButton ID="lnkAddNewGrade" ClientIDMode="Static" runat="server" CssClass="linkbtn"
                                                    OnClientClick="javascript:showDiv('dvAddNewGrade');return false;">Add New Grade</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdGrading" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                                                    AllowSorting="True" CaptionAlign="Bottom" ClientIDMode="Static" CellPadding="4"
                                                    ForeColor="#333333" GridLines="None" Width="100%" OnRowCancelingEdit="grdGrading_RowCancelingEdit"
                                                    OnRowDataBound="grdGrading_RowDataBound" OnRowDeleting="grdGrading_RowDeleting"
                                                    OnRowEditing="grdGrading_RowEditing" OnRowUpdating="grdGrading_RowUpdating" DataKeyNames="ID">
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Grading" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrade_Name" runat="server" ClientIDMode="Static" Text='<%#Eval("Grade_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtGrade_Name" Font-Size="11px" MaxLength="50" runat="server" ClientIDMode="Static"
                                                                    Text='<%#Bind("Grade_Name")%>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Grade Type" HeaderStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGrade_Type" runat="server" ClientIDMode="Static" Text='<%#Eval("Grade_Type_Text")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:RadioButtonList ID="rdoGradeType" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal"
                                                                    Text='<%#Bind("Grade_Type")%>'>
                                                                    <asp:ListItem Text="Objective Type" Value="1" Selected="True"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Min" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMin" runat="server" ClientIDMode="Static" Text='<%#Eval("Min")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlMin" Font-Size="11px" runat="server" ClientIDMode="Static"
                                                                    Text='<%#Bind("Min")%>' Enabled="false" Width="50">
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
                                                        <asp:TemplateField HeaderText="Max" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMax" runat="server" ClientIDMode="Static" Text='<%#Eval("Max")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlMax" Font-Size="11px" runat="server" ClientIDMode="Static"
                                                                    Text='<%#Bind("Max")%>' Width="50" Enabled="false">
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
                                                        <asp:TemplateField HeaderText="No of Options" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDivisions" runat="server" ClientIDMode="Static" Text='<%#Eval("Divisions")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlDivisions" Font-Size="11px" MaxLength="10" ClientIDMode="Static"
                                                                    runat="server" Width="50" Enabled="false" Text='<%#Bind("Divisions")%>'>
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
                                                        <asp:TemplateField  HeaderText="Grade Type">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblAnsType" runat="server" ClientIDMode="Static" Text='<%#Eval("Grade_Type_Text") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Grade Options" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rdoOptions" ClientIDMode="Static" runat="server" RepeatDirection="Horizontal" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                  <%--<asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                                            <EditItemTemplate>
                                                                <asp:ImageButton ID="imgbtnAccept" runat="server" ImageUrl="~/images/accept.png"
                                                                    ClientIDMode="Static" CausesValidation="False" CommandName="Update" AlternateText="Update">
                                                                </asp:ImageButton>
                                                                <asp:ImageButton ID="imgbtnCancel" runat="server" ImageUrl="~/images/reject.png"
                                                                    ClientIDMode="Static" CausesValidation="False" CommandName="Cancel" AlternateText="Cancel">
                                                                </asp:ImageButton>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="lnkEditQuest" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                                                    CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGradeIsUsed" Text='<%#Eval("GRADE_IS_USED")%>' ClientIDMode="Static" runat="server" style="display:none"></asp:Label>
                                                                <asp:ImageButton ID="LinkButton1del" runat="server" AlternateText="Delete" CausesValidation="False"
                                                                    CommandName="Delete" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to  delete ?')" />
                                                                <%-- <asp:ImageButton ID="lnkDeleteQuest" runat="server" ClientIDMode="Static" CausesValidation="False" ImageUrl="~/images/delete.png"
                                                                    CausesValidation="False" CommandName="Delete" OnClientClick="return
    confirm('Are you sure, you want to delete ?')" AlternateText="Delete"></asp:ImageButton>--%>
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
                                <td style="font-size: 11px; text-align: left; border-width: 1px; color:Red; font-weight: bold">
                                    *
                                </td>
                                <td>
                                    <asp:TextBox ID="txtQuestion" CssClass="textbox-css" Width="400px" Height="100px"
                                        runat="server" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Department:
                                </td>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; color:Red; font-weight: bold">
                                    *
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCatName" CssClass="textbox-css" Width="400px" runat="server"/>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Grading Type:
                                </td>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; color:Red; font-weight: bold">
                                    *
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGradingType" Width="400px" DataTextField="Description" DataValueField="Code"
                                        runat="server" OnSelectedIndexChanged="ddlGradingType_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr runat="server" id="trGradings">
                                <td style="vertical-align: top; border-width: 1px; font-weight: bold">
                                    Gradings
                                </td>
                                 <td style="font-size: 11px; text-align: left; border-width: 1px; color:Red; font-weight: bold">
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
                                        OnClick="btnSaveAndAdd_Click" OnClientClick="return ValidateQuestion();" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndClose" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndClose_Click"  OnClientClick="return ValidateQuestion();"/>&nbsp;&nbsp;
                                    <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDiv('dvAddNewCriteria')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        
        <div id="dvAddNewGrade" title="Add New Grade" class="modal-popup-container"
               style="width: 550px; left: 40%; top: 30%;">
            
           
           <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table border="0" style="width: 100%;" cellpadding="10">
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                   Grade:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGrade" Width="250px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Grading Type:
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdoGradeType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rdoGradeType_SelectedIndexChanged">
                                        <asp:ListItem Text="Objective Type" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Subjective Type" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr id="tr1" runat="server">
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Min Point:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMin" Font-Size="11px" runat="server" Width="50" AutoPostBack="true" OnSelectedIndexChanged="ddlDivisions_SelectedIndexChanged">
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
                            <tr id="tr2" runat="server">
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Max Point:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMax" Font-Size="11px" runat="server" Width="50" AutoPostBack="true" OnSelectedIndexChanged="ddlDivisions_SelectedIndexChanged">
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
                            <tr id="tr3" runat="server">
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
                                    <asp:Button ID="btnSaveGrade" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnSaveGrade_Click" OnClientClick="return ValidateGrading()" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndCloseGrade" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndCloseGrade_Click" OnClientClick="return ValidateGrading()" />&nbsp;&nbsp;
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

