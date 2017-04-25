<%@ Page Title="Question Bank" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Criteria.aspx.cs" Inherits="CrewEvaluation_Criteria" %>

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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
        Crew Evaluation Question Bank
    </div>
     <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
   
    <div id="page-content" style="height: 620px; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto;">
        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
            <ContentTemplate>
                <div style="text-align: center">
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
                            <td style="text-align: right">
                                <asp:DropDownList ID="ddlCategory" runat="server" DataValueField="ID" DataTextField="Category_Name"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: right">
                                <asp:LinkButton ID="lnkAddNewQuestion" runat="server" CssClass="linkbtn" OnClientClick="showDiv('dvAddNewCriteria');return false;">Add New Question</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView_Criteria" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CaptionAlign="Bottom" CellPadding="4" DataKeyNames="Criteria_ID" EmptyDataText="No Record Found"
                                    GridLines="None" OnRowCancelingEdit="GridView_Criteria_RowCancelEdit"
                                    OnRowDataBound="GridView_Criteria_RowDataBound" OnRowDeleting="GridView_Criteria_RowDeleting"
                                    OnRowEditing="GridView_Criteria_RowEditing" OnRowUpdating="GridView_Criteria_RowUpdating"
                                    OnSorted="GridView_Criteria_Sorted" OnSorting="GridView_Criteria_Sorting" Width="100%" BorderStyle="Solid" BorderColor="#cccccc" CssClass="gridmain-css" BorderWidth="1px">
                                  <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                  <RowStyle CssClass="RowStyle-css" />
                                  <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" HeaderStyle-ForeColor="Black"  SortExpression="Criteria_ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID"  runat="server" Text='<%#Eval("Criteria_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="40px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Question" HeaderStyle-ForeColor="Black" SortExpression="Criteria">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCriteria" runat="server" Text='<%#Eval("Criteria")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCriteria" runat="server" Font-Size="11px" MaxLength="1000" TextMode="MultiLine"
                                                    Width="350px" Height="60px" Text='<%#Bind("Criteria")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="400px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category Name" HeaderStyle-ForeColor="Black" SortExpression="Category_Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory_Name" runat="server" Text='<%#Eval("Category_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlCategory_Name" runat="server" DataTextField="Category_Name" DataValueField="ID" />

                                                <%--<asp:TextBox ID="txtCriteria1" runat="server" Font-Size="11px" MaxLength="1000" TextMode="MultiLine"
                                                    Width="350px" Height="60px" Text='<%#Bind("Category_ID")%>'></asp:TextBox>--%>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Criteria Options">
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="rdoOptions" runat="server" DataTextField="OptionText" DataValueField="OptionValue"
                                                    RepeatDirection="Horizontal">
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlGradingType" runat="server" DataSourceID="objDS_GradingType"
                                                    DataTextField="Grade_Name" DataValueField="ID" MaxLength="50" 
                                                    Text='<%#Bind("Grading_Type")%>' />
                                                <asp:ObjectDataSource ID="objDS_GradingType" runat="server" SelectMethod="Get_GradingList"
                                                    TypeName="SMS.Business.Crew.BLL_Crew_Evaluation"></asp:ObjectDataSource>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="50px">
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="ImgBtnAccept" ToolTip="Update" runat="server" AlternateText="Update" CausesValidation="False"
                                                    CommandName="Update" ImageUrl="~/images/accept.png"  />
                                                <asp:ImageButton ID="ImgBtnCancel" ToolTip="Cancel" runat="server" AlternateText="Cancel" CausesValidation="False"
                                                    CommandName="Cancel" ImageUrl="~/images/reject.png" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" AlternateText="Edit" CausesValidation="False"
                                                    CommandName="Edit" ImageUrl="~/images/edit.gif"  Visible='<%# Convert.ToBoolean(Eval("Visibility")) %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" AlternateText="Delete" CausesValidation="False"
                                                    CommandName="Delete" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to  delete ?')" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#58FA82" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />                                
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
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
