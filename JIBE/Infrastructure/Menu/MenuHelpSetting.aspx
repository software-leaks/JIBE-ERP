 <%@ Page  Title="Menu Help Setting" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MenuHelpSetting.aspx.cs" Inherits="Infrastructure_Menu_MenuHelpSetting" %>
 

<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
           body, html
        {
            overflow-x: hidden;
        }
         
         .page

        {
             
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }

 
         </style>
    <style type="text/css">
        .listbox
        {
            border: 0px;
        }
        .SelectedNodeStyle
        {
            background: url(../../Images/bg.png) left -1672px repeat-x;
        }
        .pager span
        {
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
            background: url(../../Images/bg.png) left -1672px repeat-x;
            font-size: 14px;
        }
        .pager a
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:visited
        {
            color: blue;
            background-color: white;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:hover
        {
            color: blue;
            background-color: #efefef;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%" cellspacing="4">
                
                
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table style="width: 100%; margin-top: 5px;">
                <tr>
                   
                    <td style="vertical-align: top; border: 1px solid #cccccc; width: 25%;">
                        <div style="height: 600px; overflow: auto;">
                            <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                                Width="100%" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" BorderColor="#cccccc">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px"
                                    NodeSpacing="0px" VerticalPadding="2px" />
                                <ParentNodeStyle Font-Bold="False" />
                                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                    VerticalPadding="0px" CssClass="SelectedNodeStyle" />
                            </asp:TreeView>
                        </div>
                    </td>
                    <td style="vertical-align: top; border: 1px solid #cccccc;">
                         <div id="dvHelpVideo" class="build-Section" style="margin: 10px 10px 0px 10px;">
                          <fieldset>
    <legend>Help Videos:</legend>
  
 

                <div class="build-header">
                    <b></b>
                </div>
                <asp:UpdatePanel ID="updHelpVideo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlHelpVideo" runat="server">
                            <table width="100%" style="margin: 10px;white-space:nowrap">
                            <tr>
                                    <td style="width: 80px">
                                        Help Video Name :
                                    </td>
                                    <td style="width: 200px; text-align: center">
                                        <asp:DropDownList ID="ddlHelpVideo" runat="server" Width="150px" ></asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnAddHelpVideo" Text="Add Help Video" runat="server" Width="150px"
                                            onclick="btnAddHelpVideo_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:DataList ID="dtlHelpVideo" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                            RepeatLayout="Table" CellSpacing="5">
                                            <ItemTemplate>
                                                <div style="background-color: #C3EBFF; border-radius: 5px; padding: 5px; border: 1px solid #ACC9C9">
                                                    <table style="width:100%">
                                                        <tr>
                                                            <td>
                                                                <asp:HyperLink ID="hlnHelpVideoID" ForeColor="Black" runat="server" Text='<%# Eval("ITEM_NAME") %>'
                                                                    NavigateUrl='<%# "~/Uploads/TrainingItems/" +  Eval("ITEM_PATH") %>' Target="_blank" ></asp:HyperLink>
                                                            </td>
                                                            <td style="padding: 0px 5px 0px 5px">
                                                                |
                                                            </td>
                                                            <td align="right">
                                                                <asp:ImageButton ID="imgbtnDeleteHelpVideo" runat="server" OnCommand="imgbtnDeleteHelpVideo_Click"
                                                                    CommandArgument='<%# Eval("HELP_ID") %>' AlternateText="delete" ImageAlign="AbsMiddle"
                                                                    ImageUrl="~/Images/Delete.png" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                               <%-- <tr>
                                    <td>
                                        <asp:GridView ID="grdHelpVideoList" runat="server" AutoGenerateColumns="False" DataKeyNames="HelpVideo_ID"
                                            CellPadding="1" CellSpacing="0" Width="99%" GridLines="both" CssClass="GridView-css">
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <HeaderStyle CssClass="HeaderStyle-css" BackColor="LightBlue" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="HelpVideo ID">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlnkView" runat="server" NavigateUrl='<%# "~/CRM/HelpVideo/AsyncHelpVideoList.aspx?HelpVideo_ID="+Eval("HelpVideo_ID").ToString() %>'
                                                            Target="_blank" Text='<%# Eval("HelpVideo_ID") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Module">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProject_Name" runat="server" Text='<%#Eval("Module_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHelpVideo_Name" runat="server" Text='<%#Eval("HelpVideo_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHelpVideo_Desc" runat="server" Text='<%#Eval("HelpVideo_Desc")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Priority">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPriority" runat="server" Text='<%#Eval("Priority")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PIC">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPIC" runat="server" Text='<%#Eval("HelpVideo_PIC")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actual Dates" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActualDates" runat="server" Text='<%#Eval("Actual_Dates")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <HeaderTemplate>
                                                        Action
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table cellpadding="2" cellspacing="2">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                        OnClientClick="return confirm('Are you sure want to delete?')" ForeColor="Black"
                                                                        CommandArgument='<%#Eval("[HelpVideo_ID]")%>' ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                        Height="16px"></asp:ImageButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>--%>
                                
                            </table>
                           
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
            <div id="dvHelpFAQ" class="build-Section" style="margin: 10px 10px 0px 10px;display:none">
             <fieldset>
 
                    <legend>Help FAQs</legend>
               
                <asp:UpdatePanel ID="updHelpFAQ" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlHelpFAQ" runat="server">
                            <table width="100%" style="margin: 10px;white-space:nowrap">
                            <tr>
                                    <td style="width: 80px">
                                        Help FAQ :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 200px; text-align: center">
                                        <asp:DropDownList ID="ddlHelpFAQ" runat="server" Width="150px" ></asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnAddHelpFAQ" Text="Add Help FAQ" runat="server" OnClick="btnAddHelpFAQ_Click"  Width="150px"
                                             />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:DataList ID="dtlHelpFAQ" runat="server" RepeatColumns="15" RepeatDirection="Horizontal" 
                                            RepeatLayout="Table" CellSpacing="5">
                                            <ItemTemplate>
                                                <div style="background-color: #C3EBFF; border-radius: 5px; padding: 5px; border: 1px solid #ACC9C9">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:HyperLink ID="hlnHelpFAQID" ForeColor="Black" runat="server" Text='<%# Eval("Question") %>'
                                                                    NavigateUrl='<%# "~/LMS/LMS_FAQ_Builder.aspx?FAQ_ID=" +  Eval("FAQ_ID") %>' Target="_blank" ></asp:HyperLink>
                                                            </td>
                                                            <td style="padding: 0px 5px 0px 5px">
                                                                |
                                                            </td>
                                                            <td align="center">
                                                                <asp:ImageButton ID="imgbtnDeleteHelpFAQ" runat="server" OnCommand="imgbtnDeleteHelpFAQ_Click"
                                                                    CommandArgument='<%# Eval("HELP_ID") %>' AlternateText="delete" ImageAlign="AbsMiddle"
                                                                    ImageUrl="~/Images/Delete.png" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                               <%-- <tr>
                                    <td>
                                        <asp:GridView ID="grdHelpFAQList" runat="server" AutoGenerateColumns="False" DataKeyNames="HelpFAQ_ID"
                                            CellPadding="1" CellSpacing="0" Width="99%" GridLines="both" CssClass="GridView-css">
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <HeaderStyle CssClass="HeaderStyle-css" BackColor="LightBlue" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="HelpFAQ ID">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlnkView" runat="server" NavigateUrl='<%# "~/CRM/HelpFAQ/AsyncHelpFAQList.aspx?HelpFAQ_ID="+Eval("HelpFAQ_ID").ToString() %>'
                                                            Target="_blank" Text='<%# Eval("HelpFAQ_ID") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Module">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProject_Name" runat="server" Text='<%#Eval("Module_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHelpFAQ_Name" runat="server" Text='<%#Eval("HelpFAQ_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHelpFAQ_Desc" runat="server" Text='<%#Eval("HelpFAQ_Desc")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Priority">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPriority" runat="server" Text='<%#Eval("Priority")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PIC">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPIC" runat="server" Text='<%#Eval("HelpFAQ_PIC")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actual Dates" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActualDates" runat="server" Text='<%#Eval("Actual_Dates")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <HeaderTemplate>
                                                        Action
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table cellpadding="2" cellspacing="2">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                        OnClientClick="return confirm('Are you sure want to delete?')" ForeColor="Black"
                                                                        CommandArgument='<%#Eval("[HelpFAQ_ID]")%>' ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                        Height="16px"></asp:ImageButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>--%>
                                
                            </table>
                           
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                 </fieldset>
            </div>

                      
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>