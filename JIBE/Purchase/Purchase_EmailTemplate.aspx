<%@ Page Title="Email Template" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Purchase_EmailTemplate.aspx.cs"
 validateRequest="false" Inherits="Purchase_Purchase_EmailTemplate" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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

    <script type="text/javascript" src="../ckeditor/ckeditor.js"></script>
<script type="text/javascript" src="../ckeditor/adapters/jquery.js"></script>


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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <center>
       
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

        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 850px;
            height: 100%;">
              <div class="page-title">
             Email Template
           </div>
            <div style="height: 650px; width: 850px; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 8%">
                                        Email Template:&nbsp;
                                    </td>
                                    <td align="left" style="width: 30%">
                                       <asp:DropDownList ID="ddlEmailfilter" runat="server" Width="100%"></asp:DropDownList>
                                      
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add  EmailTemplate" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvEmailTemplate" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvEmailTemplate_RowDataBound" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both" OnSorting="gvEmailTemplate_Sorting" AllowSorting="true">
                                     <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                     <RowStyle CssClass="RowStyle-css" />
                                     <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="EmailStatus">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblUserTypeHeader" runat="server" CommandName="Sort" CommandArgument="Email_Type"
                                                    ForeColor="Black">Email Type&nbsp;</asp:LinkButton>
                                                <img id="UserType" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            <asp:Label ID="lblEmail_Type" runat="server" Text='<%#Eval("Email_Type")%>'></asp:Label>
                                                                                          
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left"  Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true"  Width="50px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Email Body">
                                            <HeaderTemplate>
                                                Email Body
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblbody" runat="server" Text='<%#Eval("Email_Body")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left"  Width="300px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true"  Width="300px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EmailSubject">
                                            <HeaderTemplate>
                                              Email Subject
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblemailsubject" runat="server" Text='<%#Eval("Email_Subject")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" Width="70px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                         <%--   <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;ASL_LIB_SUPPLIER_EMAIL&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                             <HeaderStyle Wrap="true" Width="10px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindGrid" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
     <%--   <div id="divadd" title="<%= OperationMode %>" style="display: none; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 50%">
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
                                    <td align="right" style="width: 20%">
                                      Email Type&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlEmailType" runat="server" CssClass="txtInput" Width="400px">
                                                </asp:DropDownList>--%>
                                                 <%--<asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" Display="None" ErrorMessage="Email Status is mandatory field."
                                                            ControlToValidate="ddlEmailType" InitialValue="0" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>--%>
<%--                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 20%">
                                      Email Subject&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtemailsubject" MaxLength="500" TextMode="MultiLine" CssClass="txtInput" Width="90%" runat="server"></asp:TextBox>--%>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ErrorMessage="Email Subject is mandatory field."
                                                            ControlToValidate="txtemailsubject"  ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>--%>
<%--                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 20%">
                                      Email Body&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                   
                                   <CKEditor:CKEditorControl ID="txtemailbody"  Height="100px" Width="90%" CssClass="cke_show_borders" runat="server"></CKEditor:CKEditorControl>--%>
                                         <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ErrorMessage="Email Body is mandatory field." ControlToValidate="txtemailbody"  ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>   --%>
                                 <%--   </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center;background-color:#d8d8d8;">--%>
                                       <%-- <asp:Button ID="btnsave" runat="server" Text="Save"  ValidationGroup="vgSubmit" OnClick="btnsave_Click" />--%>
                                   <%--     <asp:Button ID="btnsave" runat="server" Text="Save"  onclick="btnsave_Click" OnClientClick="return validation();" />
                                        <asp:TextBox ID="txtUserTypeID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center;">--%>
                                       <%-- <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="Fields marked with * are required"></asp:Label>--%>
                                       <%-- <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="vgSubmit" />--%>
                                <%--    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 0px solid #cccccc;
                                            background-color: #FDFDFD">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                        * Mandatory fields
                                    </td>
                                </tr>
                            </table>
                        </div>--%>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>


