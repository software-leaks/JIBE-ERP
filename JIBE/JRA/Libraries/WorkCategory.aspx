<%@ Page Title="Work Category" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="WorkCategory.aspx.cs" Inherits="JRA_Libraries_WorkCategory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
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
        function showDiv(dv) {
            document.getElementById(dv).style.display = "block";
        }
        function closeDiv(dv) {
            document.getElementById(dv).style.display = "None";
        }
        function showDiv(dv) {
            document.getElementById('dvAddNewCategory').title = "Add New Category"
            showModal(dv);
        }
        function closeDiv(dv) {
            hideModal(dv);
        }
        $(document).ready(function () {
            $('#dvAddNewCategory').draggable();
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
        Work Category
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
 
    <div id="page-content" style=" border: 1px solid #CEE3F6; z-index:-2;
        margin-top: -1px; overflow: auto;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Work Category:</legend>
                                <asp:UpdatePanel ID="updCate" runat="server" >
                                <ContentTemplate>
                                <div style="text-align: center">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                            <td style="text-align: left">
                                                <table>
                                                <tr>
                                                <td>Filter:&nbsp;<asp:TextBox ID="txtfilter"  runat="server" AutoPostBack="true" OnTextChanged="txtfilter_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2"  runat="server" TargetControlID="txtfilter"
                                                    WatermarkText="Category to Search" WatermarkCssClass="watermarked" /></td>
                                                <td><asp:DropDownList ID="ddlFiter" runat="server" ></asp:DropDownList></td>
                                                <td><asp:ImageButton ID="btnFilter" runat="server" Height="23"  
                                                        ToolTip="Search"  ImageUrl="~/Images/SearchButton.png" 
                                                        OnClick="btnFilter_Click" /></td>
                                                </tr>
                                                </table>
                                                
                                            </td>
                                            <td style="text-align: right">
                                                <asp:LinkButton ID="lnkAddNewCategory" OnClientClick="javascript:showDiv('dvAddNewCategory');return false;"
                                                    runat="server" CssClass="linkbtn">Add New Category</asp:LinkButton><br /><br />
                                                    <p  style="color:#034af3">Sub Category Code should contains '.' along with Parent Category code</p>
                                                    <p style="color:#034af3"> e.g. If parent Category is 100 then Sub category should be like 100.1,100.2 etc</p>
                                                   
                                            </td>
                                        </tr>

                                    </table>
                                
                                    <asp:GridView ID="GridView_Category" runat="server" AutoGenerateColumns="False" 
                                        OnRowDataBound="GridView_Category_RowDataBound" 
                                        OnRowUpdating="GridView_Category_RowUpdating" ClientIDMode="Static"
                                        OnRowDeleting="GridView_Category_RowDeleting" 
                                        OnRowEditing="GridView_Category_RowEditing" 
                                        OnRowCancelingEdit="GridView_Category_RowCancelEdit" DataKeyNames="Work_Categ_ID" EmptyDataText="No Record Found"
                                        AllowSorting="True" CaptionAlign="Bottom" CellPadding="4" 
                                        GridLines="None" Width="100%" CssClass="gridmain-css">
                                       <AlternatingRowStyle CssClass="AlternatingRowStyle-css"  />
                                       <RowStyle CssClass="RowStyle-css" Font-Size="10px"/>
                                       <HeaderStyle CssClass="HeaderStyle-css" Height="35px" Font-Size="12px" HorizontalAlign="Left"  />
                                        <Columns>
                                          
                                        <asp:TemplateField HeaderText="Category Code" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                 <asp:Label ID="lblWork_Categ_Value" runat="server"  Text='<%#Eval("Work_Categ_Value") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtWork_Categ_Value" runat="server"  Text='<%#Eval("Work_Categ_Value") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Parent" HeaderText="Parent Work Category" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                                            <asp:TemplateField HeaderText="Category Name"  HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory_Name" runat="server"  ClientIDMode="Static" Text='<%#Eval("Work_Category_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                  <asp:HiddenField ID="hdnWork_Categ_ID" ClientIDMode="Static" runat="server" Value='<%#Eval("Work_Categ_ID") %>'/>
                                                    <asp:HiddenField ID="hdnParent_Work_Categ_ID"  ClientIDMode="Static" runat="server"   Value='<%#Eval("Parent_Work_Categ_ID") %>' />
                                                    <asp:TextBox ID="txtCategory_Name" Font-Size="11px" runat="server"
                                                        Text='<%#Bind("Work_Category_Name")%>' ClientIDMode="Static"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Apporval Configuration" Visible="false">
                                                <ItemTemplate> 
                                                    <asp:Hyperlink ID="hdnAppConfg" runat="server" Text="Apporval Configuration"   NavigateUrl='<%# "~/JRA/Libraries/RiskAssessmentUserLevelConfig.aspx?Work_Categ_ID="+Eval("Work_Categ_ID").ToString()+"&Work_Category_Name="+Eval("Work_Category_Name").ToString() %>' Target="_blank"></asp:Hyperlink>
                                                </ItemTemplate> 
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
                                    
                                       <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="10   " OnBindDataItem="Search_WorkCategory" />
                                </div>
                             
                                </ContentTemplate></asp:UpdatePanel>
                    </fieldset>
                </td>
                
            </tr>
            
        </table>
        <div id="dvAddNewCategory" title="Add New Category" class="modal-popup-container"
               style="width: 500px; left: 40%; top: 30%;">    
           <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                    <ContentTemplate>
                       <table border="0" style="width: 100%;" cellpadding="1">
                            <tr>
                                <td style="font-size: 11px;vertical-align:top; font-weight: bold";">
                                    Category Code
                                </td>
                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">*</td>
                                <td>
                                    <asp:TextBox ID="txtCatVal" Width="70px"  runat="server"  onkeydown="javascript:return DecimalsOnly(event);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                               <td style="font-size: 11px;vertical-align:top; font-weight: bold">
                                    Category Name 
                                </td>
                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">*</td>
                                <td>
                                    <asp:TextBox ID="txtCatName"  runat="server" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                            <td style="font-size: 11px;vertical-align:top; font-weight: bold";">
                                    Parent Category
                                </td>
                                <td></td>
                                <td>
                                    <asp:DropDownList ID="ddlParentCat" Width="80px"  runat="server"  AutoPostBack="false" OnSelectedIndexChanged="ddlParentCat_SelectedIndexChanged"></asp:DropDownList>
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
                                    <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClick="btncancel_Click"/>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                   
                </asp:UpdatePanel>
            </div>
        </div>
        

  
    
    </div>
</asp:Content>

