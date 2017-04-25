<%@ Page Title="Observation Index" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Vetting_ObservationIndex.aspx.cs" Inherits="Technical_Vetting_Vetting_ObservationIndex"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
     <style type="text/css">
        .hide
        {
            display: none;
        }
       
          .imgReport img
        {
           
             Height:18px;
             width:20px;          
           
        }
    </style>
    <script language="javascript" type="text/javascript">

        
        $(document).ready(function () {
            $("body").on("click", "#<%=btnRetrieveData.ClientID%>", function () {
                
                var MSG = "";
                if ($.trim($("#<%=txtLObsFromDate.ClientID%>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtLObsFromDate.ClientID%>").val()), '<%= UDFLib.GetDateFormat()  %>')) {
                        MSG = "Enter valid Between Date<%=UDFLib.DateFormatMessage()%>\n";
                    }
                }
                if ($.trim($("#<%=txtLObsToDate.ClientID%>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtLObsToDate.ClientID%>").val()), '<%= UDFLib.GetDateFormat()  %>')) {
                        MSG += "Enter valid And Date<%=UDFLib.DateFormatMessage()%>";
                    }
                }
                if (MSG != "") {
                    alert(MSG);
                    return false;
                }
            });
        });

             
        function CloseNote_Observation() {
            hideModal('dvAddNote_Observation')
            document.getElementById($('[id$=btnRetrieveData]').attr('id')).click();
        }
        function getOperatingSystem() {
            var OSName = "";
            var Browser = "";

            if (navigator.appVersion.indexOf("Win") != -1) OSName = "Windows";
            if (navigator.appVersion.indexOf("Mac") != -1) OSName = "MacOS";
            if (navigator.appVersion.indexOf("X11") != -1) OSName = "UNIX";
            if (navigator.appVersion.indexOf("Linux") != -1) OSName = "Linux";

            if (OSName == "MacOS") {
               
                document.getElementById($('[id$=btnObsExport]').attr('id')).style.display = "none";
            }

            if (OSName == "Windows") {               
                
                document.getElementById($('[id$=btnObsMacExport]').attr('id')).style.display = "none";
            }

        }
        //----Observation Index tab------------
        function toggleAdvSearch(obj) {

            if ($(obj).text() == "Open Advance Filter") {
                $(obj).text("Close Advance Filter");
                $("#dvAdvanceFilter").show();
            }
            else {
                $(obj).text("Open Advance Filter");
                $("#dvAdvanceFilter").hide();
            }

            if ($("#<%= hfAdv.ClientID %>").val() == "c") {
                $("#<%= hfAdv.ClientID %>").val('o');
            }
            else {
                $("#<%= hfAdv.ClientID %>").val('c');
            }
        }
        function toggleSerachPostBack() {
            if ($("#<%= hfAdv.ClientID %>").val() == "o") {
                $("#dvAdvanceFilter").show();
                $("#advText").text("Close Advance Filter");
            }
        }

        function toggleOnSearchClearFilter(obj, objval) {

            if (objval == 'o') {
                $(obj).text("Close Advance Filter");
                $("#dvAdvanceFilter").show();
            }
            else {
                $("#advText").text("Open Advance Filter");
                $("#dvAdvanceFilter").hide();
            }

        }
        

        //------------------------------Observation Index : Related Jobs tooltip--------------------

        var RelatedJobs = null;
        function BindObsIndxRelatedJobs(Question_ID, Observation_ID, ev, objthis) {

            if (RelatedJobs != null)
                RelatedJobs.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'VET_Get_ObsIndxRelatedJobsTooltip', false, { "Question_ID": Question_ID, "Observation_ID": Observation_ID }, onSuccessBindObservationIndx, OnfailObsIndx, new Array(ev, objthis));
            RelatedJobs = service.get_executor();
        }

        //------------------------------Observation Index : Response tooltip--------------------

        var Response = null;
        function BindObsIndxResponse(Observation_ID, ev, objthis) {

            if (Response != null)
                Response.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'VET_Get_ObsResponseTooltip', false, { "Observation_ID": Observation_ID }, onSuccessBindResponse, OnfailBindResponse, new Array(ev, objthis));
            Response = service.get_executor();
        }

        

        //----------------OnSuccess & onFail common function for all observation index tooltip---------------
        function onSuccessBindObservationIndx(retVal, ev) {


            js_ShowToolTip_Fixed(retVal, ev[0], ev[1], "Observations");
        }
       
        function OnfailObsIndx(result) {
//            var res = result.d;
//            alert(res);
        }

        //----------------OnSuccess & onFail common function for response tooltip---------------
        function onSuccessBindResponse(retVal, ev) {


            js_ShowToolTip(retVal, ev[0], ev[1]);
        }

        function OnfailBindResponse(result) {
//            var res = result.d;
//            alert(res);
        }
        function CloseNote_Observation() {
            hideModal('dvAddNote_Observation')
            document.getElementById($('[id$=btnRetrieveData]').attr('id')).click();
        }
        
    </script>
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
       Observation Index
    </div>
    <div class="page-content" style="font-family: Tahoma; font-size: 12px">
  
                    <asp:Panel ID="Panel2" runat="server" DefaultButton="btnRetrieveData">
                        <asp:UpdatePanel ID="UpdPnlFilterObs" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                            <div   style="color:black; background:#f2f2f2; padding:5px;">
                                <table cellpadding="2" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="left" colspan="1">
                                            <asp:Label ID="lblQuestionnaire" runat="server" Text="Questionnaire :"></asp:Label>
                                        </td>
                                        <td align="left" colspan="2">
                                          <CustomFilter:ucfDropdown ID="DDLQuestionnaire" runat="server" Height="200" UseInHeader="false" OnApplySearch="DDLQuestionnaireApplySearch"
                                                Width="160" />                                            
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:Label ID="lblSection" runat="server" Text="Section :"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <CustomFilter:ucfDropdown ID="DDLSection" runat="server" Height="200" UseInHeader="false"
                                                Width="160" OnApplySearch="VET_Get_QuestionNoByQuestionnireId" />
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:Label ID="lblQuestion" runat="server" Text="Question :"></asp:Label>
                                        </td>
                                        <td align="left" colspan="6">
                                            <CustomFilter:ucfDropdown ID="DDLQuestion" runat="server" Height="200" UseInHeader="false"
                                                Width="160" />
                                        </td>
                                        <td align="left" colspan="7">
                                            <asp:Label ID="lblType" runat="server" Text="Type :"></asp:Label>
                                        </td>
                                        <td align="left" colspan="8">
                                            <asp:RadioButtonList ID="rbtnType" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                                                <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Observations" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Notes" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td align="right" colspan="9" >
                                         <asp:ImageButton ID="btnRetrieveData" runat="server" Height="24px" ImageAlign="AbsBottom"
                                                ImageUrl="~/Images/SearchButton.png" OnClick="btnRetrieveData_Click" ToolTip="Search"  style="margin-bottom:2px"/>&nbsp;
                                            <asp:ImageButton ID="btnClearAllFilter" runat="server" Height="22px" ImageUrl="~/Images/filter-delete-icon.png"
                                                OnClick="btnClearAllFilter_Click" ToolTip="Clear Filter" style="margin-bottom:2px"/>&nbsp;                                       
                                            <asp:ImageButton ID="btnObsExport" runat="server" CommandArgument="ExportFrom_IE"
                                                Height="25px" ImageUrl="~/Images/XLS.jpg" OnClick="btnObsExport_Click" ToolTip="Export to Excel" style="margin-bottom:2px" />
                                            <asp:ImageButton ID="btnObsMacExport" runat="server" CommandArgument="ExportFrom_MAC"
                                                Height="25px" ImageUrl="~/Images/Export-mac.png" OnClick="btnObsExport_Click"
                                                Style="display: none; margin-bottom:2px" ToolTip="Export to Excel unformatted" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="1">
                                            <asp:Label ID="lblFleet" runat="server" Text="Fleet :"></asp:Label>
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                Height="18px" Width="160" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:Label ID="lblVesselList" runat="server" Text="Vessel :"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <CustomFilter:ucfDropdown ID="DDLVesselObs" runat="server" Height="200" UseInHeader="false"
                                                Width="160" />
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:Label ID="lblOilMajor" runat="server" Text="Oil Major :"></asp:Label>
                                        </td>
                                        <td align="left" colspan="6">
                                            <CustomFilter:ucfDropdown ID="DDLOilMajorObs" runat="server" Height="200" UseInHeader="false"
                                                Width="160" />
                                        </td>
                                        <td align="left" colspan="7">
                                            <asp:Label ID="lblInspectorName" runat="server" Text="Inspector :"></asp:Label>
                                        </td>
                                        <td align="left" colspan="8">
                                            <CustomFilter:ucfDropdown ID="DDLInspectorObs" runat="server" Height="200" UseInHeader="false"
                                                Width="160" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="1">
                                            <asp:Label ID="lblCategories" runat="server" Text="Categories :"></asp:Label>
                                        </td>
                                        <td align="left" colspan="2">
                                            <CustomFilter:ucfDropdown ID="DDLCategories" runat="server" Height="200" UseInHeader="false"
                                                Width="160" />
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:Label ID="lblRiskLevel" runat="server" Text="Risk Level :"></asp:Label>
                                        </td>
                                        <td align="left" colspan="4">
                                            <CustomFilter:ucfDropdown ID="DDLRiskLevel" runat="server" Height="200" UseInHeader="false"
                                                Width="160" />
                                        </td>
                                        <td align="left" colspan="5">
                                            <asp:Label ID="lblObservationVessel" runat="server" Text="Observation/Vessel :"></asp:Label>
                                        </td>
                                        <td align="left" colspan="6">
                                            <asp:TextBox ID="txtObservationVessel" runat="server" Width="157"></asp:TextBox>
                                        </td>
                                        <td align="left" colspan="7">
                                         <asp:Label ID="lblUserVesselAssigment" runat="server"  Text="By Vessel assignment :"></asp:Label>
                                        </td>
                                        <td align="left" colspan="8">
                                        <asp:UpdatePanel ID="UpdatePanel3"  runat="server">
                                        <ContentTemplate>
                                        <asp:CheckBox ID="chkVesselAssign" RepeatDirection="Horizontal" AutoPostBack="true" Checked="true"  runat="server" OnCheckedChanged="chkVesselAssign_CheckedChanged"></asp:CheckBox>  
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                        </td>
                                        <td align="right" colspan="9">
                                            <a id="advText"
                                                    href="#" onclick="toggleAdvSearch(this)">Open Advance Filter</a>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                        <asp:UpdatePanel ID="UpdAdvFltrObs" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hfAdv" runat="server" Value="c" />
                                <div id="dvAdvanceFilter" align="left" class="hide">
                                    <table border="0" cellpadding="1" cellspacing="1" style="width: 250px; background-color: #efefef;">
                                        <td valign="top" style="border: 1px solid #aabbdd;">
                                            <table border="0" cellpadding="2" cellspacing="1" width="250px">
                                                <tr style="background-color: #aabbdd;">
                                                    <td style="text-align: left;" colspan="2">
                                                        <asp:Label ID="lblObsDate" runat="server" Text="Observation Date " Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        Between:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtLObsFromDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Width="120px"></asp:TextBox><ajaxToolkit:CalendarExtender ID="cexLObsFromDate" runat="server"
                                                                Format="dd-MM-yyyy" TargetControlID="txtLObsFromDate">
                                                            </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        And:
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtLObsToDate" runat="server" CssClass="txtInput" EnableViewState="true"
                                                            Width="120px"></asp:TextBox><ajaxToolkit:CalendarExtender ID="cexLObsToDate" runat="server"
                                                                Format="dd-MM-yyyy" TargetControlID="txtLObsToDate">
                                                            </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div style="margin-top: 15px">
                            <asp:UpdatePanel ID="UpdPnlGridObs" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <asp:GridView ID="gvObservationIndex" runat="server" EmptyDataText="NO RECORDS FOUND"
                                            AutoGenerateColumns="False" CellPadding="2" ShowHeaderWhenEmpty="true" OnRowDataBound="gvObservationIndex_RowDataBound"
                                            Width="100%" AllowSorting="true" OnSorting="gvObservationIndex_Sorting" CssClass="gridmain-css"
                                            GridLines="None">
                                            <HeaderStyle CssClass="HeaderStyle-css" Height="30px"/>
                                            <RowStyle CssClass="RowStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Type">
                                                <HeaderStyle  Width="80px"/>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("Observation_Type") %>'></asp:Label>
                                                        <asp:Label ID="lblQuestionnire_ID" Visible="false" runat="server" Text='<%#Eval("Questionnaire_ID") %>'></asp:Label>
                                                        <asp:Label ID="lblQuestion_ID" runat="server" Visible="false" Text='<%#Eval("Question_ID") %>'></asp:Label>
                                                        <asp:Label ID="lblObservation_ID" runat="server" Visible="false" Text='<%#Eval("Observation_ID") %>'></asp:Label>
                                                        <asp:Label ID="lblVettingID" runat="server" Visible="false" Text='<%#Eval("Vetting_ID") %>'></asp:Label>
                                                        <asp:Label ID="lblVetTypeID" runat="server" Visible="false" Text='<%#Eval("Vetting_Type_ID") %>'></asp:Label>
                                                        <asp:Label ID="lblVeseelID" runat="server" Visible="false" Text='<%#Eval("Vessel_ID") %>'></asp:Label>
                                                        <asp:Label ID="lblVetType" runat="server" Visible="false" Text='<%#Eval("Vetting_Type_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section">
                                                <HeaderStyle  Width="80px"/>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSection" runat="server" Text='<%#Eval("Section_No") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Question">
                                                <HeaderStyle  Width="250px"/>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("Question") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Observation/Note">
                                                <HeaderStyle  Width="200px"/>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblObsNote" runat="server" Text='<%#Eval("Observation_Note") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category">
                                                <HeaderStyle  Width="150px"/>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Category_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Risk Level">
                                                <HeaderStyle  Width="80px"/>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRiskLevel" runat="server" Text='<%#Eval("Risk_Level") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vessel">
                                                <HeaderStyle  Width="150px"/>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVessel" runat="server" Text='<%#Eval("Vessel") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                <HeaderStyle  Width="150px"/>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVettingDate" runat="server" Text='<%#Eval("Observation_Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inspector">
                                                <HeaderStyle  Width="200px"/>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInspector" runat="server" Text='<%#Eval("Inspector_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Related Jobs">
                                                <HeaderStyle  Width="100px"/>
                                                    <ItemTemplate>
                                                         <u style="color:#1d60ff; cursor:pointer"><asp:HyperLink runat="server" ID="hplnkRelatedJobs" Visible='<%# Eval("Related_Jobs").ToString()!="0"?true:false %>'
                                                            Target="_blank" OnClick='<%#"BindObsIndxRelatedJobs(&#39;"+Eval("Question_ID").ToString()+"&#39;,&#39;"+Eval("Observation_ID").ToString()+"&#39;,event,this);" %>'></asp:HyperLink></u>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Responses">
                                                <HeaderStyle  Width="100px"/>
                                                    <ItemTemplate>
                                                     
                                                       <u style="color:#1d60ff; cursor:pointer"><asp:HyperLink runat="server" ID="hplnkResponses" Visible='<%# Eval("Responses").ToString()!="0"?true:false %>'
                                                            Target="_blank" OnClick='<%#"BindObsIndxResponse(&#39;"+Eval("Observation_ID").ToString()+"&#39;,event,this);" %>'></asp:HyperLink></u>
                                                        <asp:HiddenField ID="hdnObCount"  runat="server" />
                                                        <asp:HiddenField ID="hdnFleetCode"  runat="server" Value='<%#Eval("FleetCode") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle  Width="100px"/>
                                                    <ItemTemplate>
                                                        <table cellpadding="1" cellspacing="0">
                                                            <tr align="center">
                                                                <td style="border-color: transparent; width: 20px">                                                                 
                                                                      <asp:ImageButton ID="ImgDetails" runat="server" Text="Details" ForeColor="Black"
                                                                        ToolTip="Details" ImageUrl="~/Images/Details-icon.png" CssClass="imgReport" Height="16px">
                                                                    </asp:ImageButton>
                                                                </td>
                                                                <td style="border-color: transparent; width: 20px ">
                                                                   <asp:HyperLink ID="ImgReport" runat="server" Text="Report" ForeColor="Black" ToolTip="Report"
                                                                        Height="16px" ImageUrl="~/Images/Vet_Report_Icon.png" CssClass="imgReport" NavigateUrl='<%#"Vetting_Reports.aspx?Vetting_ID="+Eval("Vetting_ID")+"" %>'
                                                                        Target="_blank"></asp:HyperLink>
                                                                </td>
                                                                <td style="border-color: transparent; width: 20px">
                                                        <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                            Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;VET_DTL_Observation&#39;,&#39;Observation_ID="+Eval("Observation_ID").ToString()+"&#39;,event,this)" %>'>
                                                        </asp:Image>
                                                    </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div style="margin-top: 2px; border: 0px solid #cccccc; vertical-align: bottom; padding: 2px;
                                        color: Black; text-align: left;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="width: 100%">
                                                    <uc1:ucCustomPager ID="ucCustomPagerItemsObs" runat="server" PageSize="50" OnBindDataItem="Bind_ObservationIndex"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnObsExport" />
                                    <asp:PostBackTrigger ControlID="btnObsMacExport" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
               
    
    </div>
      <div id="dvAddNote_Observation" style="display: none; width: 1300px;" title="Add Note / Observation">
        <iframe id="IframeAddNote_Observation" src="" frameborder="0" style="height: 785px;
            width: 1300px;"></iframe>
    </div>
  
</asp:Content>
