<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentGroup.aspx.cs" Inherits="DMS_DocumentGroup"  MasterPageFile="~/Site.master" Title="Document Groups" %>

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
        #dvAddNewGroup
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
        function showDiv(dv) {
            
            document.getElementById(dv).style.display = "block";
        }
        function closeDiv(dv) {
            document.getElementById(dv).style.display = "None";
        }
        function showDiv(dv) {
          
           document.getElementById('dvAddNewGroup').title = "Add New Group";
            showModal(dv);
        }
        function closeDiv(dv) {
            hideModal(dv);
        }
        $(document).ready(function () {
            $('#dvAddNewGroup').draggable();
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
        Document Groups
    </div>
   <asp:UpdateProgress ID="UpdateProgress1"  runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                 
                <img src="../Images/loaderbar.gif"alt="Please Wait" />
            </div>
        </ProgressTemplate>
 </asp:UpdateProgress>
 
    <div id="dvPageContent" style="height: 620px; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto;"  runat="server">
        <table style="width: 70%;">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Groups:</legend>
                        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <table cellpadding="0" cellspacing="5" style="width: 100%;">
                                        <tr>
                                            <td style="text-align: left">
                                                Filter:&nbsp;<asp:TextBox ID="txtfilter" runat="server" AutoPostBack="true" OnTextChanged="txtfilter_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                                    WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                            </td>
                                            <td style="text-align: right">
                                                <asp:LinkButton ID="lnkAddNewGroup" OnClientClick="javascript:showDiv('dvAddNewGroup');return false;"
                                                    runat="server" CssClass="linkbtn">Add New Group</asp:LinkButton>
                                            </td>
                                        </tr>
                                       
                                    </table>
                                    <asp:GridView ID="gvDocumentGroup" runat="server" AutoGenerateColumns="False" OnRowUpdating="gvDocumentGroup_RowUpdating"
                                        OnRowDeleting="gvDocumentGroup_RowDeleting" OnRowEditing="gvDocumentGroup_RowEditing"
                                        OnRowCancelingEdit="gvDocumentGroup_RowCancelEdit" DataKeyNames="GROUPID" EmptyDataText="No Record Found"
                                        AllowSorting="True" CaptionAlign="Bottom" CellPadding="4" 
                                        GridLines="None" Width="100%" CssClass="gridmain-css">
                                       <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                       <RowStyle CssClass="RowStyle-css" />
                                       <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Group Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGroup_Name" runat="server" Text='<%#Eval("GroupName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtGroup_Name" Font-Size="11px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("GroupName")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="50px">
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
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
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

         <div id="dvAddNewGroup" title="Add New Category" class="modal-popup-container"
               style="width: 550px; left: 40%; top: 30%;">    
           <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                    <ContentTemplate>
                       <table border="0" style="width: 100%;" cellpadding="10">
                            <tr>
                                <td style="font-size: 11px; font-weight: bold">
                                    Group Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGroupName" Width="250px" runat="server" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvGroupName" runat="server"
                                    ValidationGroup="Validate" Display="None" ErrorMessage="Group Name is mandatory!"
                                    ControlToValidate="txtGroupName" InitialValue=""></asp:RequiredFieldValidator>
                                    <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rfvGroupName"
                                                runat="server">
                                     </tlk4:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveAndAdd" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnsave_Click"  ValidationGroup="Validate"  />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndClose" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndClose_Click"  ValidationGroup="Validate"  />&nbsp;&nbsp;
                                    <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDiv('dvAddNewGroup')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
