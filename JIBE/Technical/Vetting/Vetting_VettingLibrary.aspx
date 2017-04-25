<%@ Page Title="Vetting Library" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Vetting_VettingLibrary.aspx.cs" Inherits="Technical_Vetting_Vetting_VettingLibrary" %>

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        
          function ActiveTab() {
              var ID = $(".ajax__tab_active").attr("id").replace("ctl00_MainContent_tbCntr_", "");
            switch (ID) {
                case "tbVetType_tab":
                    $("#IframeVetType").attr("src", "Vetting_VettingType.aspx");
                    $("#IframeVesselSetting").attr("src", "");
                    $("#IframeVetAttachment").attr("src", "");
                    $("#IframeExtInspctor").attr("src", "");
                    $("#IframeObsCategory").attr("src", "");
                    break;
                case "tabVesselSetting_tab":
                    $("#IframeVetType").attr("src", "");
                    $("#IframeVesselSetting").attr("src", "Vetting_Vessel_Setting.aspx");
                    $("#IframeVetAttachment").attr("src", "");
                    $("#IframeExtInspctor").attr("src", "");
                    $("#IframeObsCategory").attr("src", "");
                    break;
                case "tbVetAtt_tab":
                    $("#IframeVetType").attr("src", "");
                    $("#IframeVesselSetting").attr("src", "");
                    $("#IframeVetAttachment").attr("src", "Vetting_CreateNewVettingAttachmentType.aspx");
                    $("#IframeExtInspctor").attr("src", "");
                    $("#IframeObsCategory").attr("src", "");
                    break;
                case "tbExtInspctor_tab":
                    $("#IframeVetType").attr("src", "");
                    $("#IframeVesselSetting").attr("src", "");
                    $("#IframeVetAttachment").attr("src", "");
                    $("#IframeExtInspctor").attr("src", "Vetting_ExternalInspector.aspx");
                    $("#IframeObsCategory").attr("src", "");
                    break;
                case "tbObsCategory_tab":
                    $("#IframeVetType").attr("src", "");
                    $("#IframeVesselSetting").attr("src", "");
                    $("#IframeVetAttachment").attr("src", "");
                    $("#IframeExtInspctor").attr("src", "");
                    $("#IframeObsCategory").attr("src", "Vetting_ObservationCategories.aspx");                   
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
            border-bottom: 1px solid #c6c6c6;
            border-left: 1px solid #c6c6c6;
            border-right: 1px solid #c6c6c6;
        }
    </style>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
    <div class="page-title">
      Vetting Library
    </div>
   <br />
        <ajaxToolkit:TabContainer ID="tbCntr" runat="server" Width="100%" ActiveTabIndex="0">
           
            <ajaxToolkit:TabPanel TabIndex="0" ID="tbVetType" runat="server" Font-Names="Tahoma">
                <HeaderTemplate>
                    Technical Settings
                </HeaderTemplate>
                <ContentTemplate>
                 <iframe id="IframeVetType" width="100%" height="700px" frameborder="0">
                    </iframe>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
             <ajaxToolkit:TabPanel  TabIndex="1" ID="tabVesselSetting" runat="server" Font-Names="Tahoma">
                <HeaderTemplate>
                   Vessel-Vetting relationship
                </HeaderTemplate>
                <ContentTemplate>
                 <iframe id="IframeVesselSetting" width="100%" height="700px" frameborder="0">
                    </iframe>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
             <ajaxToolkit:TabPanel TabIndex="2" ID="tbVetAtt" runat="server" Font-Names="Tahoma">
                <HeaderTemplate>
                    Attachment Type
                </HeaderTemplate>
                <ContentTemplate>                   
             
                    <iframe id="IframeVetAttachment" width="100%" height="700px" frameborder="0">
                    </iframe>            
                 
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
             <ajaxToolkit:TabPanel TabIndex="3" ID="tbExtInspctor" runat="server" Font-Names="Tahoma">
                <HeaderTemplate>
                   External Inspector
                </HeaderTemplate>
                <ContentTemplate>
                 <iframe id="IframeExtInspctor" width="100%" height="700px" frameborder="0">
                    </iframe>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
             <ajaxToolkit:TabPanel TabIndex="4" ID="tbObsCategory" runat="server" Font-Names="Tahoma">
                <HeaderTemplate>
                    Observation Categories
                </HeaderTemplate>
                <ContentTemplate>
                 <iframe id="IframeObsCategory" width="100%" height="700px" frameborder="0">
                    </iframe>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    
</asp:Content>
