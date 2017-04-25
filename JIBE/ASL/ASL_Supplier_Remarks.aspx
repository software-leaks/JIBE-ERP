<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_Supplier_Remarks.aspx.cs"
    ValidateRequest="false" Inherits="ASL_ASL_Supplier_Remarks" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supplier Remarks</title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ajax__htmleditor_editor_bottomtoolbar
        {
            display: none;
        }
        .cke_show_borders body
        {
            background: #FFFFCC;
            color: black;
        }
    </style>
     <script language="javascript" type="text/javascript">

         function validation() {
             if (document.getElementById("txtRemarks").value == "") {
                 alert("Remarks is mandatory field.");
                 document.getElementById("txtRemarks").focus();
                 return false;
             }
            
             return true;
         }
    </script>
     <script type="text/javascript">
         /*The Following Code added To adjust height of the popup when open after entering search criteria.*/
         $(document).ready(function () {
             window.parent.$("#ASL_Evalution").css("height", (parseInt($("#pnlRemarks").height()) + 50) + "px");
             window.parent.$(".xfCon").css("height", (parseInt($("#pnlRemarks").height()) + 50) + "px").css("top", "50px");
         });
    </script>
</head>
<body style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 99%;">
<table  style="border: 0px solid #cccccc; font-family: Tahoma; overflow: Auto; font-size: 12px; width: 100%;">
                                 <tr><td>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlRemarks" runat="server" Visible="true">
       <asp:UpdatePanel ID="UpdatePanelRemarks" runat="server">
                <contenttemplate>
                <div id="Div2" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
                color: Black; height: 100%;">
               
                        <div id="Div1" class="page-title">
                            Supplier Remarks
                        </div>
                    
                 <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td valign="top">
                     
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                   <asp:TextBox ID="txtRemarks" runat="server" MaxLength="2000" TextMode="MultiLine" 
                                        Width="400px" Height="100px" ></asp:TextBox>  
                                        <%-- <CKEditor:CKEditorControl ID="txtRemarks"  Height="90px" Width="500px"   CssClass="cke_show_borders" runat="server"></CKEditor:CKEditorControl>--%>
                                    <%-- <CKEditor:CKEditorControl ID="txtRemarks"  Height="100px" Width="550px" Toolbar="Basic"  CssClass="cke_show_borders" runat="server"></CKEditor:CKEditorControl> ValidationGroup="vgSubmit"
                                     
                                      <asp:textarea class="ckeditor" id="txtRemarks" name="message"></textarea>--%>
                                    <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" Display="None" ErrorMessage="Remarks is mandatory field."
                                        ControlToValidate="txtRemarks" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnRemarks" Text="Add Remarks"  ValidationGroup="vgSubmit"  runat="server"
                                        OnClick="btnRemarks_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUpdate" Text="Update Remarks"  ValidationGroup="vgSubmit"  Visible="false"
                                        runat="server" OnClick="btnUpdate_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="vgSubmit" />
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                    <td colspan="5" align="left" valign="top">
                        <table>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkChange" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="Image1" ImageUrl="~/Images/Pencil_Edit.png" ToolTip="Amendments" Width="20px"
                                        Height="20px" runat="server" />
                                    Changes to Supplier's Business, Organizational structure or registered data.
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkGeneral" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="Image2" Width="20px" Height="20px" ToolTip="General" ImageUrl="~/Images/Blue_Square_Flag.jpg"
                                        runat="server" />General comments relating to the supplier.
                                </td>
                            </tr>
                            <tr id="Tr1" runat="server" visible="false">
                                <td>
                                    <asp:CheckBox ID="chkGood" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="Image5" Width="20px" Height="20px" ToolTip="Green Card" ImageUrl="~/Images/Orange_Square_Flag.jpg"
                                        runat="server" />Good performance.
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkWarning" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="Image3" Width="20px" Height="20px" ToolTip="Yellow Card" ImageUrl="~/Images/Orange_Square_Flag.jpg"
                                        runat="server" />Warnings for weak perfomances and mistakes, poor response and
                                    negative attitudes.
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkRed" runat="server" />
                                </td>
                                <td>
                                    <asp:Image ID="Image4" Width="20px" Height="20px" ToolTip="Red Card" ImageUrl="~/Images/Red_Square_Flag.jpg"
                                        runat="server" />Serious breach of Company procedures, dishonesty, lack of integrity.
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
               
                    <td colspan="6">
                    
                        <div style="height: 500px; background-color: White; overflow-y: scroll; max-height: 500px">
                            <asp:GridView ID="gvRemarks" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                DataKeyNames="ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                AllowSorting="true" OnRowDataBound="gvRemarks_RowDataBound">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Entry Date">
                                        <HeaderTemplate>
                                            Entry Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEntryDate" runat="server" Text='<%#Eval("CreatedDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Created By">
                                        <HeaderTemplate>
                                            Created By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("CREATED_BY")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks Type">
                                        <HeaderTemplate>
                                            Remarks Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table>
                                                <td>
                                                    <asp:ImageButton ID="ImgAmend" Width="20px" Height="20px" Visible="false" runat="server"
                                                        Text="Update" ForeColor="Black" ToolTip="Amendments" ImageUrl="~/Images/Pencil_Edit.png">
                                                    </asp:ImageButton>
                                                    <asp:ImageButton ID="imgGeneral" Width="20px" Height="20px" Visible="false" runat="server"
                                                        Text="Update" ForeColor="Black" ToolTip="General" ImageUrl="~/Images/Blue_Square_Flag.jpg">
                                                    </asp:ImageButton>
                                                    <asp:ImageButton ID="imgWarning" Width="20px" Height="20px" Visible="false" runat="server"
                                                        Text="Update" ForeColor="Black" ToolTip="Yellow Card" ImageUrl="~/Images/Orange_Square_Flag.jpg">
                                                    </asp:ImageButton>
                                                    <asp:ImageButton ID="imageRed" Width="20px" Height="20px" runat="server" Text="Update"
                                                        ForeColor="Black" ToolTip="Red Card" Visible="false" ImageUrl="~/Images/Red_Square_Flag.jpg">
                                                    </asp:ImageButton>
                                                </td>
                                            </table>
                                            <%-- <asp:Label ID="lblRemarksType" runat="server" Text='<%#Eval("REMARKS_TYPE")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <HeaderTemplate>
                                            Remarks
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("REMRAKS")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="lbtnEdit" runat="server" CommandArgument='<%#Eval("[ID]")  %>'
                                                            Visible='<%# uaEditFlag %>' ImageUrl="~/images/edit.gif" OnCommand="lbtnEdit_Click"
                                                            Text="Edit" ToolTip="Edit"></asp:ImageButton>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="lbtnDelete" runat="server" CommandArgument='<%#Eval("[ID]")  %>'
                                                            Visible='<%# uaDeleteFlage %>' ImageUrl="~/images/delete.png" OnCommand="lbtnDelete_Click"
                                                            OnClientClick="return confirm('Are you sure want to delete?')" ToolTip="Delete">
                                                        </asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindRemarksGrid" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                        </div>
                     
                    </td>
                    
                </tr>
            </table>
            </div>
            </contenttemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    </form>
    </td></tr></table>
</body>
</html>
