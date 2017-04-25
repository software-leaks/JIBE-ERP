<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PMSJobIndividualDetails.aspx.cs"
    Inherits="Technical_PMS_PMSJobIndividualDetails" Title="Job Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .txtbox txtBoxCss
        {
            background-color: #EEEEEE;
            border: 1px solid #D6D5B6;
            font-family: Tahoma;
            font-size: 11px;
            color: #333333;
            height: inherit;
        }
    </style>
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

            $('#myGallery').galleryView({ panel_width: 800, panel_height: 250 });
            $('#myGallery2').galleryView({ panel_width: 1000, panel_height: 700 });

            $('.rotate-image').rotate({ bind: { click: function () { var value = 0; if (isNaN($(this).attr('angle'))) $(this).attr('angle', '90'); else value = eval($(this).attr('angle')); value += 90; $(this).attr('angle', value); $(this).rotate({ animateTo: value }) } } });

            $("#showPopUp").click(function () {
                showModal('dvGalerryPopUp');
            });


        });



        function OpenWorkListCrewInvolved(vid, jobid, jobhistoryid) {

            $('#dvPopupFrame').attr("Title", "Add maintenance feedback for this job");
            $('#dvPopupFrame').css({ "width": "700px", "height": "600px", "text-allign": "center" });
            $('#frPopupFrame').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });

            var URL = "../../Technical/PMS/PMSJob_Crew_Involved.aspx?VID=" + vid + "&JobID=" + jobid + "&JobHistoryID=" + jobhistoryid + "&Mode=ADD&rnd=" + Math.random();

            document.getElementById("frPopupFrame").src = URL;
            showModal('dvPopupFrame', true);
        }
             
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <div style="font-family: Tahoma; font-size: 12px; color: Black; border: 1px solid #cccccc;
            width: 850px; height: 750;">
            <div style="padding: 0px; padding: 2px; border-top: 0; background-color: #5588BB;
                color: #FFFFFF; text-align: center; font-weight: 700;">
                <b>Job Details</b>
            </div>
            <div>
                <table cellpadding="2" cellspacing="1" width="100%">
                    <tr>
                        <td align="right" style="font-weight: 700; font-size: 14px; width: 10%">
                            &nbsp; Job Code : &nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtJobCode" runat="server" Width="80px" Style="font-weight: 700;
                                font-size: 14px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%" align="right">
                            Vessel : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtVessel" runat="server" Width="460px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td style="width: 15%" align="right">
                            Department : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDepartment" runat="server" Width="120px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%"  align="right">
                            Function : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFun" runat="server" Width="460px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td align="right">
                            Rank : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRank" runat="server" Width="120px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%" align="right">
                           System Location : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtLocation" runat="server" Width="460px" BackColor="#FAEDED" ReadOnly="true"
                                CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td align="right">
                            CMS : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtcms" runat="server" Width="120px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%" align="right">
                            Sub System Location : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSubsystemLoc" runat="server" Width="460px" BackColor="#FAEDED" ReadOnly="true"
                                CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td align="right">
                            Critical : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCritical" runat="server" Width="120px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Job Title : &nbsp;
                        </td>
                        <td align="left" style="vertical-align: top" rowspan="2">
                            <asp:TextBox ID="txtJobTitle" runat="server" Width="460px" TextMode="MultiLine" Height="40px"
                                Font-Names="Tahoma" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td align="right" style="vertical-align: top">
                            Frequency : &nbsp;
                        </td>
                        <td align="left" style="vertical-align: top">
                            <asp:TextBox ID="txtFrequencyType" runat="server" Width="120px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFrequencyName" runat="server" Width="120px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Job Desc. : &nbsp;
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtJobDescription" TextMode="MultiLine" Height="40px" runat="server"
                                Width="98%" Font-Names="Tahoma" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <hr />
                <table cellpadding="2" cellspacing="1" width="100%">
                    <tr>
                        <td style="width: 18%" align="right">
                            Date originally due : &nbsp;
                        </td>
                        <td style="width: 8%" align="left">
                            <asp:TextBox ID="txtDateOriginallyDue" runat="server" Width="110px" BackColor="#ED1C24"
                                ForeColor="White" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td align="right" style="width: 12%">
                            Date Next due : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDateNextDue" runat="server" Width="110px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td style="width: 12%" align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Date Job Done : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDateJobDone" runat="server" Width="110px" BackColor="#F7F8E0"
                                ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td align="right">
                            History : &nbsp;
                        </td>
                        <td colspan="3" rowspan="2" align="left">
                            <asp:TextBox ID="txtJobRemark" TextMode="MultiLine" runat="server" Width="98%" Height="45px"
                                BackColor="#F7F8E0" Font-Names="Tahoma" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Rhrs when job done : &nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRhrsWhenJobDone" runat="server" Width="110px" BackColor="#F7F8E0"
                                ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 12%" align="right">
                            Created By : &nbsp;
                        </td>
                        <td align="left" colspan="2">
                            <asp:LinkButton ID="lbnCreatedBy" runat="server" OnClick="lbnCreatedBy_Click"></asp:LinkButton>
                            &nbsp;
                        </td>
                        <td align="right">
                            Verified By : &nbsp;
                        </td>
                        <td align="left" colspan="2">
                            <asp:LinkButton ID="lbnVerifiedBy" runat="server" OnClick="lbnVerifiedBy_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Created On : &nbsp;
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtCreatedOn" runat="server" Width="150px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                            &nbsp;
                        </td>
                        <td align="right">
                            Verified On : &nbsp;
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtVerifiedOn" runat="server" Width="150px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trMaintenanceLink" runat="server">
                    <td align="left">
                     <asp:LinkButton ID="lbnRequisition" runat="server" OnClick="lbnRequisition_Click"></asp:LinkButton>
                    </td>
                        <td align="right" colspan="5" style="background-color: #dddddd">
                            <a href="#" id="hplViewCrewInvolve" style="cursor: pointer" onclick="OpenWorkListCrewInvolved(<%=VID%>,<%=JID%>,<%=JHID%>);return false;">
                                Add maintenance feedback </a>
                        </td>
                    </tr>
                </table>
            </div>
            <hr />
            <table style="width: 100%;" cellpadding="2" cellspacing="0">
                <tr>
                    <td colspan="2" align="center" runat="server" id="tdg">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr class="row-header" style="color: #FFFFCC; background-color: #5588BB;">
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="max-height: 150px; min-height: 0px; overflow-x: auto; overflow-y: auto;">
                            <asp:Repeater runat="server" ID="rptDrillImages">
                                <HeaderTemplate>
                                    <table style="width: 100%" cellpadding="1" cellspacing="0">
                                        <tr style="color: Black; background-color: #5588BB">
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
                        </div>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <div style="max-height: 150px; min-height: 0px; overflow-x: auto; overflow-y: auto;">
                            <asp:Repeater runat="server" ID="gvPMSJobAttachment">
                                <HeaderTemplate>
                                    <table style="width: 100%" cellpadding="1" cellspacing="0">
                                        <tr style="color: Black; background-color: #5588BB">
                                            <td colspan="5" style="font-weight: bold; color: White;" align="left">
                                             Job Instructions checklist
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
                                                NavigateUrl='<%# "../../Uploads/PmsJobs/" + Eval("ATTACHMENT_PATH") %>' Target="_blank"></asp:HyperLink>
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
                                                NavigateUrl='<%# "../../Uploads/PmsJobs/" + Eval("ATTACHMENT_PATH") %>' Target="_blank"></asp:HyperLink>
                                        </td>
                                       
                                    </tr>
                                </AlternatingItemTemplate>
                                <FooterTemplate>
                                    </table></FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </td>
                </tr>
            </table>
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
        <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
            border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
            left: 0.5%; top: 15%; width: 900px; z-index: 1; color: black" title=''>
            <div class="content">
                <iframe id="frPopupFrame" src="" frameborder="0" height="100%" width="100%"></iframe>
            </div>
        </div>
    </center>
</asp:Content>
