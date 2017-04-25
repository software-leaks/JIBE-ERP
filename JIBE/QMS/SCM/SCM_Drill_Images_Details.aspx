<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SCM_Drill_Images_Details.aspx.cs" Inherits="QMS_SCM_SCM_Drill_Images_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
     <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="font-family: Tahoma; font-size: 12px; border: 1px solid gray; height: 800px;
        width: 100%; vertical-align: middle;">
        <div style="background-color: #006699; color: #FFFFCC; text-align: center; height: 20px;">
            <b>Emergency Drill images</b>
        </div>
        <div>
            <table cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td align="right" style="color:Black;">
                        Vessel :&nbsp;&nbsp;
                    </td>
                    <td align="left" style="width: 4%">
                        <asp:TextBox ID="txtVessel" Width="99%" ReadOnly="true" runat="server" CssClass="txtReadOnly"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 2%;color:Black;">
                        Drill Date :&nbsp;&nbsp;
                    </td>
                    <td align="left" style="width: 3%">
                        <asp:TextBox ID="txtDrillDate" Width="99%" ReadOnly="true" runat="server" CssClass="txtReadOnly"></asp:TextBox>
                    </td>
                    <td style="width: 2%;color:Black;" align="right">
                        Drill Type :&nbsp;&nbsp;
                    </td>
                    <td align="left" style="width: 5%">
                        <asp:TextBox ID="txtDrillType" ReadOnly="true" Width="98%" runat="server" CssClass="txtReadOnly"></asp:TextBox>
                    </td>
                    <td style="width:10%">
                    
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 5%;color:Black;">
                        Improvement Suggested :&nbsp;&nbsp;
                    </td>
                    <td align="left" colspan="6">
                        <asp:TextBox ID="txtImproSugg" ReadOnly="true" runat="server" Width="61%" CssClass="txtReadOnly"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left; border: 0px solid gray; width: 99%; margin-top: 10px; margin-left: 2px;
            font-size: 10px; background-color: #ffffff;">
            <table style="width: 100%">
                <tr>
                    <td style='width: 300px; border: 1px solid #cccccc; background-color: white; vertical-align: top;'>
                        <asp:UpdatePanel ID="UpdatePanel_Left" runat="server">
                            <ContentTemplate>
                                <div>
                                    <asp:Repeater runat="server" ID="rptDrillImages" OnItemCommand="rptDrillImages_ItemCommand"
                                        OnItemDataBound="rptDrillImages_ItemDataBound">
                                        <HeaderTemplate>
                                            <table style="width: 100%" cellpadding="2" cellspacing="0">
                                                <tr style="color: Black; background-color: #0B4C5F">
                                                    <td colspan="4" style="font-weight: bold; color: White;">
                                                        Attachments:
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="color: Black">
                                                <td style="width: 20px">
                                                    <asp:Image ID="imgDocIcon" ImageUrl="~/Images/DocTree/TXT.gif" runat="server" Height="18px" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDateCr" runat="server" Text='<%#Eval("DATE_OF_CREATION") %>'></asp:Label>
                                                </td>
                                                <td style="padding-left: 2px; width: 150px; text-align: left;">
                                                    <asp:LinkButton ID="lnkAttachment" runat="server" Text='<%#Eval("ATTACHMENT_NAME") %>'
                                                        CommandName="ViewDocument" CommandArgument='<%#Eval("PhotoUrl") %>'></asp:LinkButton>
                                                </td>
                                                <td style="padding-left: 5px; width: 60px; text-align: right;">
                                                    <asp:Label ID="lblSize" runat="server" Text='<%# Eval("FileSize","{0:0.00}").ToString() + " KB" %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr style="color: Black; background-color: #E0ECF8">
                                                <td style="width: 20px">
                                                    <asp:Image ID="imgDocIcon" ImageUrl="~/Images/DocTree/TXT.gif" runat="server" Height="18px" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDateCr" runat="server" Text='<%#Eval("DATE_OF_CREATION") %>'></asp:Label>
                                                </td>
                                                <td style="padding-left: 2px; width: 150px; text-align: left;">
                                                    <asp:LinkButton ID="lnkAttachment" runat="server" Text='<%#Eval("ATTACHMENT_NAME") %>'
                                                        CommandName="ViewDocument" CommandArgument='<%#Eval("PhotoUrl") %>'></asp:LinkButton>
                                                </td>
                                                <td style="padding-left: 5px; width: 60px; text-align: right;">
                                                    <asp:Label ID="lblSize" runat="server" Text='<%# Eval("FileSize","{0:0.00}").ToString() + " KB" %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                            </table></FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style='border: 1px solid #cccccc; background-color: white; vertical-align: top;'>
                        <asp:UpdatePanel ID="UpdatePanel_Frame" runat="server">
                            <ContentTemplate>
                                <iframe id="frmContract" src="" runat="server" style="width: 100%; height: 600px;
                                    border: 0;"></iframe>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
