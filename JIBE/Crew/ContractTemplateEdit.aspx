<%@ Page Title="Edit Contract Template" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ContractTemplateEdit.aspx.cs" Inherits="ContractTemplateEdit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="height: 650px;">
        <div style="border: 1px solid #cccccc; padding: 5px;margin-bottom:2px;">
            <div style="padding: 2px;" class="gradiant-css-orange">
               Vessel Flag: <asp:TextBox ID="txtTemplateName" runat="server" Enabled="false"></asp:TextBox> 
               <asp:Button ID="btnSaveTemplate" runat="server" Text="Save" OnClick="btnSaveTemplate_Click" />
            </div>
        </div>
        <CKEditor:CKEditorControl ID="txtTemplateBody" runat="server"></CKEditor:CKEditorControl>
    </div>
</asp:Content>
