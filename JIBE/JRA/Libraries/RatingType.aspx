<%@ Page Title="Risk Ratings" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RatingType.aspx.cs" Inherits="JRA_Libraries_RatingType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
           body, html
        {
            overflow-x: hidden;
        }
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
        function DecimalsOnly(e) {
            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    if (!IsUserFriendlyChar(key, "Decimals")) {
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        }

        function IsUserFriendlyChar(val, step) {
            // Backspace, Tab, Enter, Insert, and Delete
            if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
                return true;
            }
            // Ctrl, Alt, CapsLock, Home, End, and Arrows
            if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
                return true;
            }
            if (step == "Decimals") {
                if (val == 190 || val == 110) {
                    return true;
                }
            }
            // The rest
            return false;
        }
        function showDiv(dv) 
        {
            document.getElementById(dv).style.display = "block";
        }
        function closeDiv(dv) {

            
            document.getElementById(dv).style.display = "None";

        }
        function showDiv(dv) {
            document.getElementById('dvAddNewRiskType').title = "Risk Ratings"
            //            document.getElementById('dvAddNewGrade').title = "Add New Grade"
            showModal(dv);
        }
        function closeDiv(dv) {
            hideModal(dv);
        }
        $(document).ready(function () {
            $('#dvAddNewRiskType').draggable();
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:UpdatePanel ID="upd" runat="server" ClientIDMode="Static">
<ContentTemplate>
  <div class="page-title">
       Risk Ratings
    </div>
<table border="0" style="width: 100%;" cellpadding="1">
                        <tr>
                        <td style="text-align: right" colspan="2">
                                                <asp:LinkButton ID="lnkAddNewRating" OnClientClick="javascript:showDiv('dvAddNewRiskType');return false;"
                                                    runat="server" CssClass="linkbtn">Add New Rating</asp:LinkButton>
                                            </td>
                        </tr>
                         
                           
                        </table>
                     <asp:GridView ID="GridView_Ratings" runat="server" AutoGenerateColumns="False" 
                                        OnRowDataBound="GridView_Ratings_RowDataBound"
                                        OnRowUpdating="GridView_Ratings_RowUpdating"
                                        OnRowDeleting="GridView_Ratings_RowDeleting" 
                                        OnRowEditing="GridView_Ratings_RowEditing"
                                        OnRowCancelingEdit="GridView_Ratings_RowCancelEdit" DataKeyNames="Rating_ID" EmptyDataText="No Record Found"
                                        AllowSorting="True" CaptionAlign="Bottom" CellPadding="4" 
                                        GridLines="None" Width="100%" CssClass="gridmain-css">
                                       <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                       <RowStyle CssClass="RowStyle-css"  HorizontalAlign="Left"/>
                                       <HeaderStyle CssClass="HeaderStyle-css" Height="25px" HorizontalAlign="Left" VerticalAlign="Middle"/>
                                       
                                       <Columns>
                                            <asp:TemplateField HeaderText="Rating" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                            <asp:Label ID="lblRisk" ClientIDMode="Static" runat="server" Text='<%#Eval("Rating_VALUE")%>'> </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                             <asp:TextBox ID="txtRisk" Font-Size="11px" runat="server" ClientIDMode="Static"
                                                        Text='<%#Bind("Rating_VALUE")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Risk Type" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnRating_ID" runat="server" ClientIDMode="Static" Value='<%#Eval("Rating_ID") %>'/>
                                                    
                                                    <asp:Label ID="lblRiskType" runat="server" Text='<%#Eval("Type_Display_Text")%>'  ClientIDMode="Static"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                <asp:HiddenField ID="hdnRisk_TYPE"  runat="server"  ClientIDMode="Static"  Value='<%#Eval("Risk_TYPE") %>' />
                                                    <asp:DropDownList ID="ddlDisplayText" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlDisplayText_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:HiddenField ID="hdnSelectedValue"  runat="server"  ClientIDMode="Static"  />
                                                </EditItemTemplate>
                                                
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Color" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                            <asp:Label ID="lblColor" ClientIDMode="Static" runat="server" Text='<%#Eval("Type_Color")%>'> </asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
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
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
                                                        CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                        AlternateText="Delete"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
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
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
<div id="dvAddNewRiskType" title="Add New RatingType" class="modal-popup-container"
               style="width: 500px; left: 40%; top: 30%;">    
           <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                    <ContentTemplate>
                        <table border="0" style="width: 100%;" cellpadding="1">
                           <tr>
                                <td style="font-size: 11px;vertical-align:top; font-weight: bold";">
                                    Risk Type
                                </td>
                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">*</td>
                                <td>
                                    <asp:DropDownList ID="ddlRiskType" Width="70px"  runat="server" ClientIDMode="Static"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                               <td style="font-size: 11px;vertical-align:top; font-weight: bold">
                                    Rating Value
                                </td>
                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">*</td>
                                <td>
                                    <asp:TextBox ID="txtRatingVal"  runat="server" ClientIDMode="Static" ></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table align="center">
                         <tr>
                                <td  style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveAndAdd" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnsave_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndClose" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndClose_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnClose" CssClass="button-css" runat="server" Text="Close" OnClick="btnClose_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                   
                </asp:UpdatePanel>
            </div>
        </div>
</asp:Content>

