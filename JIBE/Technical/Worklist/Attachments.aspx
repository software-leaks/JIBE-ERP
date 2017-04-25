<%@ Page Title="Attachments" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Attachments.aspx.cs" Inherits="Worklist_Attachments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
  
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />



    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="../../Styles/jquery.galleryview-3.0-dev.css" />
    <script type="text/javascript" src="../../Scripts/jQueryRotate.2.2.js"></script>
    <!-- Second, add the Timer and Easing plugins -->
    <script type="text/javascript" src="../../Scripts/jquery.timers-1.2.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.galleryview-3.0-dev.js"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $(".draggable").draggable();

            $('#myGallery').galleryView({ panel_width: 800, panel_height: 380 });
            $('#myGallery2').galleryView({ panel_width: 900, panel_height: 800 });

            $('.rotate-image').rotate({ bind: { click: function () { var value = 0; if (isNaN($(this).attr('angle'))) $(this).attr('angle', '90'); else value = eval($(this).attr('angle')); value += 90; $(this).attr('angle', value); $(this).rotate({ animateTo: value }) } } });

            $("#showPopUp").click(function () {
                showModal('dvGalerryPopUp');
            });


        });

             
    </script>
    <script type="text/javascript">
        function onItemSelected(item) {
            if (item != null) {
                var t = document.getElementById("titleField");
                if (t)
                    t.innerHTML = 'Title';
            }
        }

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="dvAgreement" style="border: 1px solid #B6DAFD; background-color: #E8F3FE;
        font-family: Arial; color: Black;">
        <table style="width: 100%">
            <tr>
                <td style="width: 20%; border: 1px solid #B6DAFD; background-color: white; vertical-align: top;">
                    <asp:UpdatePanel ID="UpdatePanel_Left" runat="server">
                        <ContentTemplate>
                            <div>
                                <div style="padding: 5px;">
                                    <asp:Label ID="lblJob" runat="server" Font-Bold="true" />
                                </div>
                                <asp:Repeater runat="server" ID="rpt1" OnItemCommand="rpt1_ItemCommand" OnItemDataBound="rpt1_ItemDataBound">
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
                                                <asp:Label ID="lblDateCr" runat="server" Text='<%#Eval("Date_Of_Creation") %>'></asp:Label>
                                            </td>
                                            <td style="padding-left: 2px; width: 150px; text-align: left;">
                                                <asp:LinkButton ID="lnkAttachment" runat="server" Text='<%#Eval("Attach_Name") %>'
                                                    CommandName="ViewDocument" CommandArgument='<%#Eval("Attach_Path") %>'></asp:LinkButton>
                                            </td>
                                            <td style="padding-left: 5px; width: 60px; text-align: right;">
                                                <asp:Label ID="lblSize" runat="server" Text='<%# Eval("FileSize","{0:0.00}").ToString() + " KB" %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr style="color: Black; background-color: #81F7D8">
                                            <td style="width: 20px">
                                                <asp:Image ID="imgDocIcon" ImageUrl="~/Images/DocTree/TXT.gif" runat="server" Height="18px" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDateCr" runat="server" Text='<%#Eval("Date_Of_Creation") %>'></asp:Label>
                                            </td>
                                            <td style="padding-left: 2px; width: 150px; text-align: left;">
                                                <asp:LinkButton ID="lnkAttachment" runat="server" Text='<%#Eval("Attach_Name") %>'
                                                    CommandName="ViewDocument" CommandArgument='<%#Eval("Attach_Path") %>'></asp:LinkButton>
                                            </td>
                                            <td style="padding-left: 5px; width: 80px; text-align: right;">
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
                <td  align="left" style='width: 50%;border: 1px solid #B6DAFD; background-color: white; vertical-align: top;'>
                    <div id="dvGalerryPopUp" style="width:100%;vertical-align:top;">
                        <ul id="myGallery2">
                            <asp:ListView ID="ListView2" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <asp:Image ID="imgAttachment" ImageUrl='<%# "../../Uploads/Technical/" + Eval("Image_Path") %>'
                                            data-description='<% #Eval("Created_By_Name") %>' ToolTip='<% #Eval("Attach_Name") %>'
                                            runat="server" MaxHeight="650" />
                                    </li>
                                </ItemTemplate>
                            </asp:ListView>
                        </ul>
                    </div>
                    <asp:HiddenField ID="hidenTotalrecords" runat="server" />
                    <asp:HiddenField ID="HCurrentIndex" runat="server" />
                    <%--<asp:UpdatePanel ID="UpdatePanel_Frame" runat="server">
                        <ContentTemplate>
                            <iframe id="frmContract" src="" runat="server" style="width: 100%; height: 600px;
                                border: 0;"></iframe>
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
