<%@ Page Title="Survey and Certificate Settings" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="SurveyandCertificateSettings.aspx.cs" Inherits="Surveys_SurveyandCertificateSettings" %>

<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ajax__tab_tab
        {
            min-width: 110px;
        }
        .ajax__tab_body
        {
            border: 0 !important;
            min-height: 550px;
        }
        .ajax__tab_xp .ajax__tab_body
        {
            padding: 0px !important;
        }
        .ajax__tab_tab
        {
            color: #696969 !important;
            min-width: 130px;
        }
        .ajax__tab_active
        {
            font-weight: bold;
        }
        iframe
        {
            border: 1px solid #c6c6c6;
        }
    </style>
    <script type="text/javascript">

        function ActiveTab() {
            var ID = $(".ajax__tab_active").attr("id").replace("ctl00_MainContent_TabPanel_", "");
            switch (ID) {
                case "tabSurveyCertificate_tab":
                    $("#iframe0").attr("src", "SurveyCertificateLib.aspx");
                    $("#iframe1").attr("src", "");
                    $("#iframe2").attr("src", "");
                    $("#iframe3").attr("src", "");
                    $("#iframe4").attr("src", "");
                    $("#iframe5").attr("src", "");
                    break;
                case "tabSurveyCategory_tab":
                    $("#iframe0").attr("src", "");
                    $("#iframe1").attr("src", "SurveyCategoryLib.aspx");
                    $("#iframe2").attr("src", "");
                    $("#iframe3").attr("src", "");
                    $("#iframe4").attr("src", "");
                    $("#iframe5").attr("src", "");
                    break;
                case "tabCertificateAuthority_tab":
                    $("#iframe0").attr("src", "");
                    $("#iframe1").attr("src", "");
                    $("#iframe2").attr("src", "SurveyCertificateAuthority.aspx");
                    $("#iframe3").attr("src", "");
                    $("#iframe4").attr("src", "");
                    $("#iframe5").attr("src", "");
                    break;
                case "tabSurveyDocType_tab":
                    $("#iframe0").attr("src", "");
                    $("#iframe1").attr("src", "");
                    $("#iframe2").attr("src", "");
                    $("#iframe3").attr("src", "SurveyDocumentType.aspx");
                    $("#iframe4").attr("src", "");
                    $("#iframe5").attr("src", "");
                    break;
                case "tabNASurveyandCertificates_tab":
                    $("#iframe0").attr("src", "");
                    $("#iframe1").attr("src", "");
                    $("#iframe2").attr("src", "");
                    $("#iframe3").attr("src", "");
                    $("#iframe4").attr("src", "NASurveyList.aspx?vid=0&s_v_id=0&cat_id=0&page=setting");
                    $("#iframe5").attr("src", "");
                    break;
                case "tabSurveyvesselassignment_tab":
                    $("#iframe0").attr("src", "");
                    $("#iframe1").attr("src", "");
                    $("#iframe2").attr("src", "");
                    $("#iframe3").attr("src", "");
                    $("#iframe4").attr("src", "");
                    $("#iframe5").attr("src", "Survey_Vessel_Assign.aspx");
                    break;
                default:
            }
        }

        $(window).load(function () {
            ActiveTab();
            $("body").on("click", ".ajax__tab_active", function () {
                ActiveTab();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Survey and Certificate Settings
    </div>
    <br />
    <ajaxToolkit:TabContainer ID="TabPanel" ActiveTabIndex="0" runat="server">
        <ajaxToolkit:TabPanel TabIndex="0" ID="tabSurveyCertificate" runat="server">
            <HeaderTemplate>
                Survey Certificate
            </HeaderTemplate>
            <ContentTemplate>
                <iframe id="iframe0" height="1000px" width="100%"></iframe>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel TabIndex="1" ID="tabSurveyCategory" runat="server">
            <HeaderTemplate>
                Survey Category
            </HeaderTemplate>
            <ContentTemplate>
                <iframe id="iframe1" height="1000px" width="100%"></iframe>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel TabIndex="2" ID="tabCertificateAuthority" runat="server">
            <HeaderTemplate>
                Certificate Authority
            </HeaderTemplate>
            <ContentTemplate>
                <iframe id="iframe2" height="1000px" width="100%"></iframe>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel TabIndex="3" ID="tabSurveyDocType" runat="server">
            <HeaderTemplate>
                Survey Doc Type
            </HeaderTemplate>
            <ContentTemplate>
                <iframe id="iframe3" height="1000px" width="100%"></iframe>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel TabIndex="4" ID="tabNASurveyandCertificates" runat="server">
            <HeaderTemplate>
                N/A Survey and Certificates
            </HeaderTemplate>
            <ContentTemplate>
                <iframe id="iframe4" height="1000px" width="100%"></iframe>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel TabIndex="5" ID="tabSurveyvesselassignment" runat="server">
            <HeaderTemplate>
                Survey vessel assignment
            </HeaderTemplate>
            <ContentTemplate>
                <iframe id="iframe5" height="1000px" width="100%"></iframe>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>
