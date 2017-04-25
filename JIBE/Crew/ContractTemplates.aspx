<%@ Page Title="View Contract Templates" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="ContractTemplates.aspx.cs" Inherits="Crew_ContractTemplates" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link type="text/css" href="../styles/ui-lightness/jquery-ui-1.8.14.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="../scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery-ui-1.8.14.custom.min.js"></script>
    <script type="text/javascript">

        function load_Contract() {
            var flagid = window.event.srcElement.id;
            var url = "ContractPreview.aspx?flag=" + flagid + "&rnd=" + Math.random();
            $.get(url, function (data) {
                $('#dvPreview').html(data);
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
         Contract Template Editor 
    </div>
 <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
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
                                    Contract Types
                                </th>
                            </tr>
                            <asp:Repeater runat="server" ID="rpt1" >
                                <ItemTemplate>
                                    <tr style="background-color: White; color: Black; font-weight: bold;">
                                        <td>
                                            <div id='<%# Eval("Contract_Name")%>'> <%# Eval("Contract_Name")%></div>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("ContractId")%>'
                                                OnClick="lnkEdit_Click" Text='<%# Eval("TemplateId").ToString() == "0" ? "ADD" : "EDIT"%>'></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <br />
                        <br />
                        <table cellspacing="1" cellpadding="1" style="border: 1px solid #610B5E; width: 100%;">
                            <tr>
                                <th style="background-color: #336666; color: White;" colspan="2">
                                    Side Letters
                                </th>
                            </tr>
                            <asp:Repeater runat="server" ID="rptSideLetters">
                                <ItemTemplate>
                                    <tr style="background-color: White; color: Black; font-weight: bold;">
                                        <td>
                                            <%# Eval("Template_Name")%>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("ID").ToString() + "," + Eval("Template_Name").ToString() %>' OnClick="lnkEditSideLetter_Click">Edit</asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
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
                                    <div style="padding: 2px;" class="gradiant-css-orange">
                                        <asp:Panel ID="pnlContractTemplate" runat="server" Visible = "false">
                                            Contract Template Name:
                                            <asp:HiddenField ID="hdnVessel_Flag" runat="server" />
                                            <asp:HiddenField ID="hdnContractId" runat="server" />
                                            <asp:TextBox ID="txtTemplateName" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:Button ID="btnSaveTemplate" runat="server" Text="Save" OnClick="btnSaveTemplate_Click" />
                                        </asp:Panel>
                                        <asp:Panel ID="pnlSideletter" runat="server" Visible="false">
                                            Side Letter:
                                            <asp:HiddenField ID="hdnSideletterID" runat="server" />
                                            <asp:TextBox ID="txtSideletter" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:Button ID="btnSideletter" runat="server" Text="Save" OnClick="btnSaveSideletterTemplate_Click" />
                                        </asp:Panel>
                                    </div>
                                    <CKEditor:CKEditorControl ID="txtTemplateBody" runat="server"></CKEditor:CKEditorControl>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <%--            <asp:Panel ID="pnlWages" runat="server" Visible="false">
                <asp:Repeater runat="server" ID="rpt2"><ItemTemplate><tr><td><%#Eval("EarningDeduction")%></td><td><%#Eval("Name") %></td><td><%#Eval("Salary_Type").ToString().Replace("BOC", "One-Time").Replace("MOC", "Per Month").Replace("EOC", "One-Time")%></td></tr></ItemTemplate>
                </asp:Repeater>
            </asp:Panel>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
