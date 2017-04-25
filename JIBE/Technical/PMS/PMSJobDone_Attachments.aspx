<%@ Page Title="Job Attachments" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PMSJobDone_Attachments.aspx.cs" Inherits="PMSJobDone_Attachments" %>

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
            $('#myGallery2').galleryView({ panel_width: 1000, panel_height: 800 });

            $('.rotate-image').rotate({ bind: { click: function () { var value = 0; if (isNaN($(this).attr('angle'))) $(this).attr('angle', '90'); else value = eval($(this).attr('angle')); value += 90; $(this).attr('angle', value); $(this).rotate({ animateTo: value }) } } });

            $("#showPopUp").click(function () {
                showModal('dvGalerryPopUp');
            });


        });

             
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="font-family: Tahoma; font-size: 12px; border: 1px solid gray; height: 850px;
        width: 100%; vertical-align: middle;">
        <div style="background-color: #5588BB; color: #FFFFCC; text-align: center; height: 20px;">
            <b>Job Attachments</b>
        </div>
        <div>
            <table cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td align="right" style="width: 1%; color: Black;">
                        Vessel :&nbsp;&nbsp;
                    </td>
                    <td align="left" style="width: 10%">
                        <asp:TextBox ID="txtVessel" Width="20%" ReadOnly="true" runat="server" CssClass="txtReadOnly"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="color: Black;">
                        Job Title :&nbsp;&nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtJobTitle" Width="99%" ReadOnly="true" runat="server" CssClass="txtReadOnly"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="color: Black;">
                        Job Description :&nbsp;&nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtJobDesc" TextMode="MultiLine" Width="99%" ReadOnly="true" runat="server"
                            Height="35px" CssClass="txtReadOnly"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="color: Black;">
                        Frequency :&nbsp;&nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtFrequency" Width="10%" ReadOnly="true" runat="server" CssClass="txtReadOnly"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="color: Black;">
                        Date Done :&nbsp;&nbsp;
                    </td>
                    <td align="left" style="width: 10%">
                        <asp:TextBox ID="txtDateDone" Width="10%" ReadOnly="true" runat="server" CssClass="txtReadOnly"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="color: Black;">
                        R Hrs. Done :&nbsp;&nbsp;
                    </td>
                    <td align="left" style="width: 10%">
                        <asp:TextBox ID="txtRHoursDone" Width="10%" ReadOnly="true" runat="server" CssClass="txtReadOnly"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="color: Black;">
                        Remark :&nbsp;&nbsp;
                    </td>
                    <td align="left" style="width: 10%">
                        <asp:TextBox ID="txtRemark" TextMode="MultiLine" Width="99%" ReadOnly="true" runat="server"
                            Height="35px" CssClass="txtReadOnly"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div style="border: 0px solid gray; margin-top: 5px; margin-left: 2px; font-size: 12px;
            background-color: #ffffff;">
            <table style="width: 100%;" cellpadding="2" cellspacing="0">
                <tr>
                    <td colspan="2" align="center" style="padding: 10px">
                    <div  runat="server" id="tdg"> 
                    <table cellpadding="1" cellspacing="0" width="100%">
                            <tr class="row-header" style="color: #FFFFCC; background-color: #0B4C5F;">
                                <td style="font-weight: bold; color: White;" align="left">
                                    Image Attachments:
                                </td>
                                <td style="font-weight: bold; color: #FFFFCC;" align="right">
                                    <div id="showPopUp" style="cursor: pointer;">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/SearchButton.png" Height="16px" />
                                        Larger View
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <div id="webpage">
                                        <div id="retina">
                                        </div>
                                        <%-- <asp:Label ID="lblFileName" runat="server" Text="" Visible="false"></asp:Label>--%>
                                        <ul id="myGallery">
                                            <asp:ListView ID="ListView1" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Image ID="imgAttachment" ImageUrl='<%# "../../Uploads/PmsJobs/" + Eval("Image_Path") %>'
                                                            data-description='<% #Eval("Created_By_Name") %>' ToolTip='<% #Eval("ATTACHMENT_NAME") %>'
                                                            runat="server" MaxHeight="400" CssClass="rotate-image" />
                                                    </li>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </ul>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:HiddenField ID="hidenTotalrecords" runat="server" />
                                    <asp:HiddenField ID="HCurrentIndex" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div><div style="max-height: 150px; min-height: 0px; overflow-x: auto; overflow-y: auto;">
                                        <asp:Repeater runat="server" ID="rptDrillImages">
                                            <HeaderTemplate>
                                                <table style="width: 100%" cellpadding="1" cellspacing="0">
                                                    <tr style="color: Black; background-color: #0B4C5F">
                                                        <td colspan="5" style="font-weight: bold; color: White;" align="left">
                                                            Other Attachments :
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="color: Black">
                                                    <td style="width: 16px">
                                                        <asp:Image ID="imgDocIcon" ImageUrl="~/Images/DocTree/TXT.gif" runat="server" Height="15px" />
                                                    </td>
                                                    <td style="padding-left: 2px; width: 10px; text-align: left;">
                                                        <asp:Label ID="lblDateCr" runat="server" Text='<%#Eval("DATE_OF_CREATION") %>'></asp:Label>
                                                    </td>
                                                    <td style="padding-left: 2px; width: 150px; text-align: left;">
                                                        <asp:HyperLink ID="lnkAttachment" runat="server" Text='<%#Eval("ATTACHMENT_NAME") %>'
                                                            NavigateUrl='<%# "../../Uploads/PmsJobs/" + Eval("PhotoUrl") %>' Target="_blank"></asp:HyperLink>
                                                    </td>
                                                    <td style="padding-left: 2px; width: 60px; text-align: right;">
                                                        <asp:Label ID="lblSize" runat="server" Text='<%# Eval("FileSize","{0:0.00}").ToString() + " KB" %>'></asp:Label>
                                                    </td>
                                                    <td style="padding-left: 5px; width: 200px; text-align: right;">
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr style="color: Black; background-color: #E0ECF8">
                                                    <td style="width: 16px">
                                                        <asp:Image ID="imgDocIcon" ImageUrl="~/Images/DocTree/TXT.gif" runat="server" Height="18px" />
                                                    </td>
                                                    <td style="padding-left: 2px; width: 10px; text-align: left;">
                                                        <asp:Label ID="lblDateCr" runat="server" Text='<%#Eval("DATE_OF_CREATION") %>'></asp:Label>
                                                    </td>
                                                    <td style="padding-left: 2px; width: 150px; text-align: left;">
                                                        <asp:HyperLink ID="lnkAttachment" runat="server" Text='<%#Eval("ATTACHMENT_NAME") %>'
                                                            NavigateUrl='<%# "../../Uploads/PmsJobs/" + Eval("PhotoUrl") %>' Target="_blank"></asp:HyperLink>
                                                    </td>
                                                    <td style="padding-left: 2px; width: 60px; text-align: right;">
                                                        <asp:Label ID="lblSize" runat="server" Text='<%# Eval("FileSize","{0:0.00}").ToString() + " KB" %>'></asp:Label>
                                                    </td>
                                                    <td style="padding-left: 5px; width: 200px; text-align: right;">
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                            <FooterTemplate>
                                                </table></FooterTemplate>
                                        </asp:Repeater>
                                    </div></div>
                        
                    </td>
                </tr>
            </table>
            <%--<table style="width: 100%">
                <tr>
                    <td style='width: 300px; border: 1px solid #cccccc; background-color: white; vertical-align: top;'>
                        <asp:UpdatePanel ID="UpdatePanel_Left" runat="server">
                            <ContentTemplate>
                                <div>
                                    
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
            </table>--%>
        </div>
    </div>
    <div id="dvGalerryPopUp" style="display: none">
        <ul id="myGallery2">
            <asp:ListView ID="ListView2" runat="server">
                <ItemTemplate>
                    <li>
                        <asp:Image ID="imgAttachment" ImageUrl='<%# "../../Uploads/PmsJobs/" + Eval("Image_Path") %>'
                            data-description='<% #Eval("Created_By_Name") %>' ToolTip='<% #Eval("ATTACHMENT_NAME") %>'
                            runat="server" MaxHeight="650" />
                    </li>
                </ItemTemplate>
            </asp:ListView>
        </ul>
    </div>
</asp:Content>
