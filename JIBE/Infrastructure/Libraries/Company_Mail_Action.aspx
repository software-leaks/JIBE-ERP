<%@ Page Title="Company Action Mail" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Company_Mail_Action.aspx.cs" Inherits="Infrastructure_Libraries_Company_Mail_Action" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        
        .linkbtn
        {
            border-right: wheat 1px solid;
            border-top: wheat 1px solid;
            font-weight: bold;
            border-left: wheat 1px solid;
            color: White;
            border-bottom: wheat 1px solid;
            background-color: white;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function Divaddnewlink() {
            document.getElementById("divadd").style.display = "block";
        }



        function validation() {

//            if (document.getElementById("ctl00_MainContent_ddlUserName").value == "0") {
//                alert("Please select User Name.");
//                document.getElementById("ctl00_MainContent_ddlUserName").focus();
//                return false;
//            }
            return true;


        }


       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
            <div class="page-title">
                Company Action Mail
            </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                    <ContentTemplate>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvComMailAction" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" OnRowDataBound="gvComMailAction_RowDataBound" DataKeyNames="ID"
                                    CellPadding="1" CellSpacing="0" Width="100%" GridLines="both" OnSorting="gvComMailAction_Sorting"
                                    AllowSorting="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code" SortExpression="Action Type">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblAction_Type" runat="server" CommandName="Sort" CommandArgument="Action_Type"
                                                    ForeColor="Black">Action Type</asp:LinkButton>
                                                <img id="Action_Type" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblActionType" Visible="true" runat="server" CommandArgument='<%#Eval("[ID]")%>'
                                                    Text='<%# DataBinder.Eval(Container,"DataItem.Action_Type") %>' Style="color: Black"
                                                    OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subject" SortExpression="Subject">
                                            <HeaderTemplate>
                                                Subject
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("Subject")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mail To" SortExpression="Mail To">
                                            <HeaderTemplate>
                                                Mail To
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMail_To" runat="server" Text='<%#Eval("Mail_To")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mail CC" SortExpression="Mail CC">
                                            <HeaderTemplate>
                                                Mail CC
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMail_CC" runat="server" Text='<%#Eval("Mail_CC")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Body" SortExpression="Body">
                                            <HeaderTemplate>
                                                Body
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBody" runat="server" Text='<%#Eval("Body")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
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
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindCompany_MailAction" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 30%;">
                            <table width="98%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Action Type&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtActionType" runat="server" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Subject&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSubject" runat="server" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Mail To&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMailTo" runat="server" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Mail Cc&nbsp;:&nbsp;
                                    </td>
                                    <td align="right" style="color: #FF0000; width: 1%">
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMailCc" runat="server" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                       Body&nbsp;:&nbsp;
                                    </td>
                                    <td align="right" style="color: #FF0000; width: 1%">
                                    </td>
                                    <td align="left">
                                      
                                        <asp:TextBox  ID="txtemailbody" MaxLength="4000"  Width="300px" CssClass="cke_show_borders" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; border-style: solid;
                                        border-color: Silver; border-width: 1px; background-color: #d8d8d8;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validation();"
                                            OnClick="btnsave_Click" />
                                        <asp:TextBox ID="txtMailID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td colspan="3" align="center" style="color: #FF0000; font-size: small;">
                                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                        * Mandatory fields
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                  
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
