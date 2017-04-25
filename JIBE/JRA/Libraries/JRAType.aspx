<%@ Page Title="JRA Types" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="JRAType.aspx.cs" Inherits="JRA_Libraries_JRAType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
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
        .Mandatory
        {
           color: #FF0000; font-size: small; width: 1%"
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
    function showDiv(dv) {
        document.getElementById(dv).style.display = "block";
    }
    function closeDiv(dv) {
        document.getElementById(dv).style.display = "None";
    }
    function showDiv(dv) {
        document.getElementById('dvAddNewType').title = "Add New Type"
        showModal(dv);
    }
    function closeDiv(dv) {
        hideModal(dv);
    }
    $(document).ready(function () 
    {
        $('#dvAddNewType').draggable();
    });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="page-title">
       JRA Types
    </div>
<table style="width: 100%;">
<tr>
                <td colspan="2">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>JRA Type:</legend>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <table style="width: 100%">
                                        <tr>
                                         <td style="text-align: left">
                                                <table>
                                                <tr>
                                                <td>Filter:&nbsp;<asp:TextBox ID="txtfilter" runat="server" AutoPostBack="true" OnTextChanged="txtfilter_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                                    WatermarkText="Display Text/Description" WatermarkCssClass="watermarked" /></td>
                                                <td><asp:DropDownList ID="ddlFiter" runat="server" ClientIDMode="Static">  
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Consequences" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="Likelihood" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Risk" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Severity" Value="1"></asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td><asp:ImageButton ID="btnFilter" runat="server" Height="23" 
                                                        ToolTip="Search" ImageUrl="~/Images/SearchButton.png" 
                                                        onclick="btnFilter_Click" /></td>
                                                </tr>
                                                </table>
                                                
                                            </td>
                                            <td style="text-align: right">
                                                <asp:LinkButton ID="lnkAddNewType" OnClientClick="javascript:showDiv('dvAddNewType');return false;"
                                                    runat="server" CssClass="linkbtn">Add New Type</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView  ID="GridView_Type" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView_Type_RowDataBound"
                                        OnRowUpdating="GridView_Type_RowUpdating" OnRowDeleting="GridView_Type_RowDeleting"
                                        OnRowEditing="GridView_Type_RowEditing" OnRowCancelingEdit="GridView_Type_RowCancelEdit"
                                        DataKeyNames="Type_ID" EmptyDataText="No Record Found" AllowSorting="True" CaptionAlign="Bottom"
                                        CellPadding="4"  GridLines="None" Width="100%" CssClass="grd" >
                                      <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                      <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left"/>
                                      <HeaderStyle CssClass="HeaderStyle-css" Height="25px" HorizontalAlign="Left" VerticalAlign="Middle"/>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Type"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnType_ID"  runat="server"  ClientIDMode="Static"  Value='<%#Eval("Type_ID") %>' />
                                                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlType" Font-Size="11px" ClientIDMode="Static"  runat="server">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                          <asp:ListItem Text="Consequences" Value="4"></asp:ListItem>
                                                          <asp:ListItem Text="Likelihood" Value="2"></asp:ListItem>
                                                          <asp:ListItem Text="Risk" Value="3"></asp:ListItem>
                                                          <asp:ListItem Text="Severity" Value="1"></asp:ListItem>
                                                         </asp:DropDownList>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Type Value" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType_Value" runat="server" Text='<%#Eval("Type_Value")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtType_Value" Font-Size="11px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("Type_Value")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Display Text" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType_Display_Text" runat="server" Text='<%#Eval("Type_Display_Text")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtType_Display_Text" Font-Size="11px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("Type_Display_Text")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType_Description" runat="server" Text='<%#Eval("Type_Description")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtType_Desc" Font-Size="11px" MaxLength="2000"  Width="450px" runat="server"
                                                        Text='<%#Bind("Type_Description")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                 <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Color" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType_Color" runat="server" Text='<%#Eval("Type_Color")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlGrdType_Color"  ClientIDMode="Static" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGrdType_Color_SelectedIndexChanged"></asp:DropDownList>
                                                    <%--<asp:TextBox ID="txtType_Color" Font-Size="11px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("Type_Color")%>'></asp:TextBox>--%>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
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
                                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="10" OnBindDataItem="Search_Type" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </td>
            </tr></table>
<div id="dvAddNewType" title="Add New Type" class="modal-popup-container"
               style="width: 550px; left: 40%; top: 30%;"><div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                          <table border="0" style="width: 100%;" cellpadding="10">
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Type:
                                </td>
                                <td class="Mandatory" align="left">*</td>
                                <td>
                                    <asp:DropDownList ID="ddlType" ClientIDMode="Static" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Consequences" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Likelihood" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Risk" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Severity" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Value
                                </td>
                                <td align="left"><asp:Label id="tdvalue" runat="server" CssClass="Mandatory"  Text="*"></asp:Label> </td>
                                <td>
                                    <asp:TextBox ID="txtTypeValue" Width="250px" runat="server" ClientIDMode="Static" onkeydown="javascript:return DecimalsOnly(event);"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                   Description
                                </td>
                                <td align="left"><asp:Label id="tdDesc" runat="server" CssClass="Mandatory" Text="*"></asp:Label> </td>
                                <td>
                                    <asp:TextBox ID="txtDesc" Width="250px" ClientIDMode="Static" runat="server" TextMode="MultiLine" Height="150px" ></asp:TextBox>
                                   
                                </td>
                            </tr>
                            <tr><td style="font-size: 11px; text-align: left; font-weight: bold">Display Text</td>
                            <td  class="Mandatory" align="left"><asp:Label id="tdDisText" runat="server" Text="*" ></asp:Label></td><td><asp:TextBox ID="txtDisplayText" ClientIDMode="Static" runat="server"></asp:TextBox></td></tr>
                            <tr>
                            <td style="font-size: 11px; text-align: left; font-weight: bold">
                                List Of Colors
                            </td>
                            <td align="left"><asp:Label id="tdcolor" runat="server" CssClass="Mandatory" Text="*"> </asp:Label></td>
                            <td>
                            <table>
                            <tr>
                            <td><asp:DropDownList ID="ddlColors" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlColors_SelectedIndexChanged"></asp:DropDownList></td>
                            <td><div id="divColor" runat="server" clientidmode=Static></div></td>
                            </tr></table>
                            
                            </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveType" ClientIDMode="Static" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnSaveType_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndCloseType" ClientIDMode="Static" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndCloseType_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnClose" ClientIDMode="Static" CssClass="button-css" runat="server" Text="Close" OnClick="btnClose_Click"/>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlType" />
                    <asp:AsyncPostBackTrigger ControlID="ddlColors" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
</asp:Content>

