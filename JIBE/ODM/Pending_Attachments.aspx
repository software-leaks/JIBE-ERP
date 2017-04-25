<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.master" CodeFile="Pending_Attachments.aspx.cs"
    Inherits="ODM_Pending_Attachments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <title>ODM Attachments</title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
        <script language="javascript" type="text/javascript">

        function DocOpen(filename) {
             window.open('..' + filename);
        }
       </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
            <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
        <center>
            <table width="60%" cellpadding="2" cellspacing="0" >
                <tr>
                    <td align="center">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            ODM Attachments
                        </div>
                    </td>
                </tr>

                <tr>
                    <td valign="top" align="left">
                    <asp:Button ID="btnODMMain" Text="ODM Main" runat="server" 
                            onclick="btnODMMain_Click" />
                    </td>
                </tr>
                <tr>

                <td align="center" >
                           <asp:GridView ID="gvAttachment" runat="server" AutoGenerateColumns="False"
                                GridLines="Both" 
                                Width="100%">
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Attachments">
                                        <HeaderTemplate>
                                            Attachement 
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblfileName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ATTACHMENT_NAME") %>'
                                                Visible="true"></asp:Label>
                                       </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="left" Wrap="true" Width="30%"/>
                                    </asp:TemplateField>
<%--                                    <asp:TemplateField>

                                    <HeaderTemplate>Path </HeaderTemplate>
                                    <ItemTemplate>
                                         <asp:Label ID="lblfilePath" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ATTACHMENT_PATH") %>'
                                                Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="left" Wrap="true" Width="40%" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField>
                                    <HeaderTemplate >Size(KB)
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblFileId" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Size") %>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Wrap="true" Width="15%"/>
                                    </asp:TemplateField>


                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <img style="border: 0; width: 14px; height: 14px" alt="Open in new window" onclick="DocOpen('<%# Eval("ATTACHMENT_PATH")%>')"
                                                src="../Images/Download-icon.png" title="Click to View file" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="5%"
                                            Wrap="False" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                
                
                </td>
                </tr>




            </table>
        </center>
           </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>