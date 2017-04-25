<%@ Page Title="Module Templates" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Infra_ModuleTemplates.aspx.cs" Inherits="Infra_ModuleTemplates" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link type="text/css" href="../styles/ui-lightness/jquery-ui-1.8.14.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="../scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery-ui-1.8.14.custom.min.js"></script>
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function load_Contract() {
            var flagid = window.event.srcElement.id;
            var url = "ContractPreview.aspx?flag=" + flagid + "&rnd=" + Math.random();
            $.get(url, function (data) {
                $('#dvPreview').html(data);
            });
        }
    </script>
    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("ctl00_MainContent_txtModule").value.trim() == "") {
                alert("Please enter Module Name.");
                document.getElementById("ctl00_MainContent_txtModule").focus();
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        <table style="height: auto">
            <td>
                <td align="center" style="width: 30%">
                    <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                </td>
                <td align="center" style="width: 40%">
                    Module Template Editor
                </td>
                <td align="center" style="width: 50%">
                    <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Module" ImageUrl="~/Images/Add-icon.png"
                        OnClick="ImgAdd_Click" />
                </td>
            </td>
        </table>
    </div>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanelSearchCrew" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="vertical-align: top; width: 200px;">
                        <table cellspacing="1" cellpadding="1" style="border: 1px solid #610B5E; width: 100%;">
                            <tr>
                                <th style="background-color: #336666; color: White;" colspan="2">
                                    Module Types
                                </th>
                            </tr>
                            <asp:Repeater runat="server" ID="rpt1">
                                <ItemTemplate>
                                    <tr style="background-color: White; color: Black; font-weight: bold;">
                                        <td>
                                            <div id='<%# Eval("ModuleTypeName")%>'>
                                                <%# Eval("ModuleTypeName")%></div>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("ModuleTypeId").ToString() + "," + Eval("TemplateId").ToString() %>'
                                                OnClick="lnkEdit_Click" Text='<%# Eval("TemplateId").ToString() == "0" ? "ADD" : "EDIT"%>'></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <br />
                        <br />
                        <table cellspacing="1" cellpadding="1" style="border: 1px solid #610B5E; width: 100%;">
                        </table>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 25%">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Module Name &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:TextBox ID="txtModule" CssClass="txtInput" MaxLength="100" Width="90%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsaveModule_Click"
                                            OnClientClick="return validation();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                        * Mandatory fields
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td style="vertical-align: top">
                        <table cellspacing="1" cellpadding="1" style="border: 1px solid #610B5E; width: 100%;">
                            <tr>
                                <th style="background-color: #336666; color: White; font-size: 14px;">
                                    Template Editor
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <div style=" font-family:Tahoma; color:Black; padding: 2px;" class="gradiant-css-orange">
                                        <asp:Panel ID="pnlModuleTemplate" runat="server" Visible="false">                                           
                                            Module Template Name:
                                            <asp:HiddenField ID="hdnModuleTypeID" runat="server" />
                                            <asp:TextBox ID="txtTemplateName" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:Button ID="btnSaveTemplate" runat="server" Text="Save" OnClick="btnSaveTemplate_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                            Module Name<asp:TextBox ID="txtModuleName" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:ImageButton  ID="ImageButton1" runat="server" Text="EDit" ToolTip="Edit Module" ImageUrl="~/Images/edit.gif" OnClick="btnModuleEdit_Click" Visible=true />
                                            <asp:ImageButton  ID="btnModuleEdit" runat="server" Text="Save" ImageUrl="~/Images/Save.png" OnClick="btnModuleEditSave_Click" Visible=false />                                          
                                            <asp:ImageButton ID="ImgDelete" runat="server" ToolTip="Delete Module" ImageUrl="~/Images/delete.png"
                                                OnClientClick="return confirm('Are you sure want to delete?')" OnClick="ImgDelete_Click" Height="20" />
                                            <br />
                                            <asp:Button Text="Email Header" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
                                                BackColor="#cccccc" OnClick="Tab2_Click" />
                                            <asp:Button Text="Email Body" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server" Visible=false 
                                                BackColor="#cccccc" OnClick="Tab1_Click" />
                                            <asp:Button Text="Email Footer" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
                                                BackColor="#cccccc" OnClick="Tab3_Click" />
                                        </asp:Panel>
                                       
                                        
                                        <asp:MultiView ID="MainView" runat="server">
                                            <asp:View ID="View1" runat="server">
                                                <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                                    <tr>
                                                        <td>
                                                            <h3>
                                                                <span style="color: Green">Edit Body </span>
                                                            </h3>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:View>
                                            <asp:View ID="View2" runat="server">
                                                <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                                    <tr>
                                                        <td>
                                                            <h3>
                                                                <span style="color: Green">Edit Header </span>
                                                            </h3>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:View>
                                            <asp:View ID="View3" runat="server">
                                                <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                                    <tr>
                                                        <td>
                                                            <h3>
                                                                <span style="color: Green">Edit Footer </span>
                                                            </h3>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:View>
                                        </asp:MultiView>
                                    </div>
                                    <CKEditor:CKEditorControl ID="txtTemplateBody" runat="server"></CKEditor:CKEditorControl>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
