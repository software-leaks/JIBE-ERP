<%@ Page Title="KPI Library" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="KPILibrary.aspx.cs" Inherits="KPILibrary" %>

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
        .style1
        {
            height: 28px;
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
            document.getElementById('dvAddNewCategory').title = "Add New Category"
            document.getElementById('dvAddNewGrade').title = "Add New Interval"
            document.getElementById('dvAddNewType').title = "Add New Units"
            showModal(dv);
        }
        function closeDiv(dv) {

            document.getElementById("ctl00_MainContent_txtIntervals").value = "";
            document.getElementById("ctl00_MainContent_txtCatName").value = "";
            document.getElementById("ctl00_MainContent_txtUnits").value = "";
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
        KPI Libraries
    </div>
  <asp:UpdateProgress ID="UpdateProgress1"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                 
                   <img src="../../Images/loaderbar.gif"alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
 
    <div id="page-content" style="height: 620px; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Category:</legend>
                        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                          <td style="text-align: left" class="style1">
                                                Filter:&nbsp;<asp:TextBox ID="txtfilter" runat="server" AutoPostBack="true" OnTextChanged="txtfilter_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                                    WatermarkText="Type to Search Category" WatermarkCssClass="watermarked" />
                                            </td>
                                            <td style="text-align: right" class="style1">
                                                <asp:LinkButton ID="lnkAddNewCategory" OnClientClick="javascript:showDiv('dvAddNewCategory');return false;"
                                                    runat="server" CssClass="linkbtn">Add New Category</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="GridView_Category" runat="server" AutoGenerateColumns="False" OnRowUpdating="GridView_Category_RowUpdating"
                                        OnRowDeleting="GridView_Category_RowDeleting" OnRowEditing="GridView_Category_RowEditing" 
                                        OnRowCancelingEdit="GridView_Category_RowCancelEdit" DataKeyNames="ID" EmptyDataText="No Record Found"
                                        AllowSorting="True" CaptionAlign="Bottom" CellPadding="4" 
                                        GridLines="None" Width="100%" CssClass="gridmain-css">
                                       <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                       <RowStyle CssClass="RowStyle-css" />
                                       <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Category Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory_Name" runat="server" Text='<%#Eval("Category_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCategory_Name" Font-Size="11px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("Category_Name")%>'></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="rfvCategory" ControlToValidate="txtCategory_Name" Display="None"  ErrorMessage="Category Name required!"
                                                     ValidationGroup="vgSubmittCategory" runat="server"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="60%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" >
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnAccept" ToolTip="Update" ValidationGroup="vgSubmittCategory"  runat="server" ImageUrl="~/images/accept.png"
                                                        CausesValidation="true" CommandName="Update" AlternateText="Update"></asp:ImageButton>
                                                         <asp:ValidationSummary ID="vsSubmittCategory" runat="server" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" ValidationGroup="vgSubmittCategory" />

                                                    <asp:ImageButton ID="ImgBtnCancel" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                                        CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                                        CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete"  >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
                                                        CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                        AlternateText="Delete"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20%" />
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
                <td style="width: 50%; vertical-align: top;">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Units:</legend>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                 <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                        <td style="text-align: left" class="style1">
                                                Filter:&nbsp;<asp:TextBox ID="txtUnitSearch" runat="server" AutoPostBack="true" OnTextChanged="txtUnitSearch_TextChanged"></asp:TextBox> &nbsp; <%--<asp:Button ID="btnUnitSearch" runat="server" Text="Filter" OnClick="btnunitSearch_click" />--%>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtUnitSearch"
                                                    WatermarkText="Type to Search Unit" WatermarkCssClass="watermarked" />
                                            </td>
                                            <td style="text-align: right" class="style1">
                                                <asp:LinkButton ID="lnkAddNewType" OnClientClick="javascript:showDiv('dvAddNewType');return false;"
                                                    runat="server" CssClass="linkbtn">Add New Units</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="GridViewUnits" runat="server" AutoGenerateColumns="False" 
                                         OnRowEditing="GridViewUnits_RowEditing"
                                        OnRowCancelingEdit="GridViewUnits_RowCancelingEdit" 
                                        DataKeyNames="ID" EmptyDataText="No Record Found"
                                        AllowSorting="True" CaptionAlign="Bottom" CellPadding="4" 
                                        GridLines="None" Width="100%" CssClass="gridmain-css" 
                                        onrowupdating="GridViewUnits_RowUpdating" 
                                        onrowdeleting="GridViewUnits_RowDeleting">
                                       <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                       <RowStyle CssClass="RowStyle-css" />
                                       <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Unit Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnit_Name" runat="server" Text='<%#Eval("Unit_Name")%>'></asp:Label>
                                                     
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCategory_Name" Font-Size="11px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("Unit_Name")%>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvUnitName" ControlToValidate="txtCategory_Name" Display="None"  ErrorMessage="Unit Name required!"
                                                     ValidationGroup="vgSubmittUnit" runat="server"></asp:RequiredFieldValidator>

                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="50px">
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnAccept1" ToolTip="Update" runat="server" ImageUrl="~/images/accept.png"
                                                        ValidationGroup="vgSubmittUnit" CommandName="Update" AlternateText="Update"></asp:ImageButton>
                                                    <asp:ImageButton ID="ImgBtnCancel1" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                                        CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                                         <asp:ValidationSummary ID="vsSubmittUnit" runat="server" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" ValidationGroup="vgSubmittUnit" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                                        CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1del1" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
                                                        CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
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
                          <%--  <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnUnitSearch"  EventName="Click"/>
                            
                            </Triggers>--%>
                        </asp:UpdatePanel>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Intervals:</legend>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <table style="width: 100%">
                                        <tr>
                                         <td style="text-align: left" class="style1">
                                                Filter:&nbsp;<asp:TextBox ID="txtIntervalSearch" runat="server" AutoPostBack="true" OnTextChanged="txtInterval_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtIntervalSearch"
                                                    WatermarkText="Type to Search Interval" WatermarkCssClass="watermarked" />
                                            </td>
                                           
                                        
                                            <td style="text-align: right">
                                                <asp:LinkButton ID="lnkAddNewGrade" OnClientClick="javascript:showDiv('dvAddNewGrade');return false;"
                                                    runat="server" CssClass="linkbtn">Add New Intervals</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="GridViewIntervals" runat="server" AutoGenerateColumns="False" 
                                         OnRowEditing="GridViewIntervals_RowEditing"
                                        OnRowCancelingEdit="GridViewIntervals_RowCancelingEdit" 
                                        DataKeyNames="ID" EmptyDataText="No Record Found"
                                        AllowSorting="True" CaptionAlign="Bottom" CellPadding="4" 
                                        GridLines="None" Width="100%" CssClass="gridmain-css" 
                                        onrowupdating="GridViewIntervals_RowUpdating" 
                                        onrowdeleting="GridViewIntervals_RowDeleting">
                                       <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                       <RowStyle CssClass="RowStyle-css" />
                                       <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Interval Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInterval_Name" runat="server" Text='<%#Eval("Interval_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtInterval_Name" Font-Size="11px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("Interval_Name")%>'></asp:TextBox>
                                                         <asp:RequiredFieldValidator ID="rfvInterval" ControlToValidate="txtInterval_Name" Display="None"  ErrorMessage="Interval Name required!"
                                                     ValidationGroup="vgSubmittInterval" runat="server"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="50px">
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnAccept1" ToolTip="Update" runat="server" ImageUrl="~/images/accept.png"
                                                        CausesValidation="true" CommandName="Update"  ValidationGroup="vgSubmittInterval" AlternateText="Update"></asp:ImageButton>
                                                    <asp:ValidationSummary ID="vsSubmittInterval" runat="server" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" ValidationGroup="vgSubmittInterval" />

                                                    <asp:ImageButton ID="ImgBtnCancel1" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                                        CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                                        CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1del1" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
                                                        CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
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
        <div id="dvAddNewCategory" title="Add New Category" class="modal-popup-container"
               style="width: 550px; left: 40%; top: 30%;">    
           <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                    <ContentTemplate>
                       <table border="0" style="width: 100%;" cellpadding="10">
                            <tr>
                                <td style="font-size: 11px; font-weight: bold">
                                    Category Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCatName" Width="200px" MaxLength="50" runat="server"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="rfvCategory" ControlToValidate="txtCatName" Display="None"  ErrorMessage="Category Name required!"
                                                     ValidationGroup="vgSubmittCategoryEntry" runat="server"></asp:RequiredFieldValidator>
                                                      <asp:ValidationSummary ID="vsSubmittInterval" runat="server" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" ValidationGroup="vgSubmittCategoryEntry" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveAndAdd" CssClass="button-css" runat="server" ValidationGroup="vgSubmittCategoryEntry" Text="Save And Add New"
                                        OnClick="btnsave_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndClose" CssClass="button-css" runat="server" Text="Save And Close" ValidationGroup ="vgSubmittCategoryEntry"
                                        OnClick="btnSaveAndClose_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDiv('dvAddNewCategory')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        

  <div id="dvAddNewType" title="Add New Unit" class="modal-popup-container"
               style="width: 550px; left: 40%; top: 30%;">

           
           <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                          <table border="0" style="width: 100%;" cellpadding="10">
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Unit Type:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUnits" Width="200px" MaxLength="50" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUnits" Display="None"  ErrorMessage="Unit Name required!"
                                ValidationGroup="vgSubmittUnitEntry" runat="server"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" ValidationGroup="vgSubmittUnitEntry" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveUnits" CssClass="button-css" runat="server" Text="Save And Add New" ValidationGroup="vgSubmittUnitEntry" OnClick="btnsaveUnits_Click"
                                        />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndCloseUnits" CssClass="button-css" runat="server" Text="Save And Close" ValidationGroup="vgSubmittUnitEntry" OnClick="btnSaveAndCloseUnits_Click"
                                       />&nbsp;&nbsp;
                                    <asp:Button ID="Button3" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDiv('dvAddNewType')" />
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
                                    Interval Type:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIntervals" Width="200px" MaxLength="50" runat="server"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtIntervals" Display="None"  ErrorMessage="Interval Name required!"
                                ValidationGroup="vgSubmittIntervalEntry" runat="server"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" ValidationGroup="vgSubmittIntervalEntry" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="Button1" CssClass="button-css" runat="server" Text="Save And Add New"  ValidationGroup="vgSubmittIntervalEntry" OnClick="btnsaveIntervals_Click"
                                        />&nbsp;&nbsp;
                                    <asp:Button ID="Button2" CssClass="button-css" ValidationGroup="vgSubmittIntervalEntry" runat="server" Text="Save And Close" OnClick="btnSaveAndCloseIntervals_Click"
                                       />&nbsp;&nbsp;
                                    <asp:Button ID="Button4" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDiv('dvAddNewGrade')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
