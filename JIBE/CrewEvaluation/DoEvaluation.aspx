<%@ Page Title="Perform Evaluation" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="DoEvaluation.aspx.cs" Inherits="CrewEvaluation_DoEvaluation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        historyBox
        {
            background-color: #efefef;
            border: 1px solid inset;
        }
        .tbl-common-Css
        {
            width: 100%;
            border: 1 px solid gray;
            border-collapse: collapse;
        }
        .hdr-common-Css
        {
            background-color: #006699;
            font-weight: bold;
            font-size: 10px;
            color: White;
            font-family: Tahoma;
            border-spacing: 1px;
            font-family: Verdana;
            border-top: 1px solid gray;
            border-bottom: 1px solid gray;
        }
        .row-common-Css
        {
            font-size: 10px;
            background-color: transparent;
            color: Black;
            text-align: left;
            font-family: Verdana;
            border-bottom: 1px solid #cccccc;
            padding-left: 5px;
            border-left: 0px solid white;
            border-right: 0px solid white;
        }
    </style>
    <style type="text/css">
        .header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
        }
        h4
        {
            font-size: 1.2em;
            color: #ffffff;
            font-weight: bold;
            margin: 0 0 0 5px;
        }
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        .watermarked
        {
            color: #cccccc;
        }
        
        .linkbtn a
        {
            color: Black;
        }
        
        .linkbtn
        {
            color: black;
            background-color: white;
            text-decoration: none;
            border-left: 1px solid #cccccc;
            padding-left: 10px;
            border-top: 1px solid #cccccc;
            padding-top: 5px;
            border-right: 1px solid #cccccc;
            padding-right: 10px;
            border-bottom: 1px solid #cccccc;
            padding-bottom: 3px;
            background-color: #F1F8E0;
        }
        input[checked=checked]
        {
            background-color: yellow;
        }
    </style>
    <style id="TooltipStyle" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: Tahoma;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .grid-row
        {
            border: 1px solid #cccccc;
        }
        .grid-col-fixed
        {
            border: 1px solid #cccccc;
        }
        .grid-col
        {
            border: 1px solid #cccccc;
        }
        .gradiant-css-browne
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#81F79F',EndColorStr='#088A4B');
            background: -webkit-gradient(linear, left top, left bottom, from(#81F79F), to(#088A4B));
            background: -moz-linear-gradient(top,  #81F79F,  #088A4B);
            color: Black;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
        
        p
        {
            line-height:1.6em !important;
            margin:2px !important;
            }
       
      
      
    </style>
   <%-- <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();

            if (dd < 10) {
                dd = '0' + dd
            }

            if (mm < 10) {
                mm = '0' + mm
            }

            today = mm + '/' + dd + '/' + yyyy;
            var wh = '<%=Request.QueryString["CrewID"]%>'
            Get_Crew_Information(wh, today);
            //
        });
       
    
    </script>--%>
    <script language="javascript" type="text/javascript">

        function showDivAddNewCriteria() {
            document.getElementById("dvAddNewCriteria").style.display = "block";
        }
        function closeDivAddNewCriteria() {
            document.getElementById("dvAddNewCriteria").style.display = "None";
        }
        $(document).ready(function () {
            var highlightChecked = function () {
                $("#tbl_Result input[type=radio]").next("label").css({ "backgroundColor": "transparent" });
                $("#tbl_Result input:checked").next("label").css({ "backgroundColor": "yellow" });               

            };
            highlightChecked();
            $("input[type=radio]").on("click", highlightChecked);
        });

        function rdoOptions_Changed(obj) {
            var hdnRemark = $(obj.id.replace('rdoOptions', 'hdnRemark'));
            var txtRemark = $(obj.id.replace('rdoOptions', 'txtRemarks'));

            document.getElementById(txtRemark.selector).style.backgroundColor = "white";

            var res = document.getElementById(hdnRemark.selector).value.split(";");
            var options = $('table#' + $(obj.id).selector).find('input:radio');


            for (i = 0; i < options.length; i++) {
                if (options[i].checked == true) {
                    for (j = 0; j < res.length; j++) {
                        if (res[j] == options[i].value) {
                            document.getElementById(txtRemark.selector).style.backgroundColor = "yellow";
                            $('[id$=lblMsg]')[0].innerText = "Remark highlighted in Yellow is mandatory!";
                            break;
                        }
                    }
                }
            }
        }
        function Validate() {
            var grid = document.getElementById("<%=GridView_AssignedCriteria.ClientID%>");
            var rbs = grid.getElementsByTagName("textarea");


            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].style.backgroundColor == "yellow" && rbs[i].value.trim() == "") {
                    $('[id$=lblMsg]')[0].innerText = "Remark highlighted in Yellow is mandatory!";
                    alert('Remark highlighted in Yellow is mandatory!');
                    return false;
                }
            }
        }
        function chkNA_Checked() {
            var grid = document.getElementById("<%=GridView_AssignedCriteria.ClientID%>");
            var chkList = grid.getElementsByTagName("input");
            for (var i = 0; i < chkList.length; i++) {
                if (chkList[i].type == 'checkbox') {
                    chkNA_Changed(chkList[i]);
                }
            }
        }
        function chkNA_Changed(obj) {
            var rdo = $(obj.id.replace('chkNA', 'rdoOptions'));
            var options = $('table#' + rdo.selector).find('input:radio');
            var remark = $(obj.id.replace('chkNA', 'txtRemarks'));
            var i = 0;
            if (obj.checked == true) {
                for (i = 0; i < options.length; i++) {
                    options[i].checked = false;
                    $(options[i]).attr("disabled", true);
                    document.getElementById(remark.selector).style.backgroundColor = "white";
                }
            }
            else {
                for (i = 0; i < options.length; i++) {
                    $(options[i]).removeAttr("disabled");
                }
            }
        }

        var lastExecutor = null;
        function showFeedbackHistory() {
            if (document.getElementById("dvFeedback").style.display == "none") {

                document.getElementById("ctl00_MainContent_lnkHide").style.display = 'block';
                document.getElementById("ctl00_MainContent_lnkShow").style.display = 'none';
                AsyncFeedbackHistory();
            }
            else {
                $('[id$=dvFeedback]').hide('slow');
                document.getElementById("ctl00_MainContent_lnkHide").style.display = 'none';
                document.getElementById("ctl00_MainContent_lnkShow").style.display = 'block';
            }
        }

        function AsyncFeedbackHistory() {

            document.getElementById("ctl00_MainContent_lnkHide").style.display = 'block';
            document.getElementById("ctl00_MainContent_lnkShow").style.display = 'none';
            var CrewEvaluation_ID = '<%=this.Request.QueryString["DtlID"]%>';
            var Vessel_ID = $('[id$=hdnVessel_ID]').val();
            var Office_ID = $('[id$=hdnOffice_ID]').val();

            if (CrewEvaluation_ID != "" && Vessel_ID != "" && Office_ID != "") {
                if (lastExecutor != null)
                    lastExecutor.abort();

                var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGetCrewEvaluation_FeedbackCompleted', false, { "CrewEvaluation_ID": CrewEvaluation_ID, "Vessel_ID": Vessel_ID, "Office_ID": Office_ID }, onSuccessShowFeedbackHistory, Onfail, new Array('t'));

                lastExecutor = service.get_executor();
            }
        }

        function onSuccessShowFeedbackHistory(retVal1, eventArgs) {

            document.getElementById("dvFeedbackHistory").innerHTML = retVal1;
            $('#dvFeedback').show('slow');
        }
        function Onfail(msg) {

           // alert(msg._message);
        }
        function ChangeBtnColor(btnColor) {

            if (btnColor == "") {
                document.getElementById("ctl00_MainContent_lnkReqFeedBk").style.backgroundColor = "white";
            } else {
                document.getElementById("ctl00_MainContent_lnkReqFeedBk").style.backgroundColor = "yellow";
            }

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
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="pageTitle" class="gradiant-css-blue" style="font-size: 12px; border: 1px solid #CEE3F6;
         padding: 3px; font-weight: bold;">
         <table width="100%">
         <tr>
         <td style="text-align:right;width:35%;"></td>
         <td style="text-align:center;width:30%;"> <asp:Label ID="lblPageTitle" runat="server" Text="Perform Crew Evaluation" ></asp:Label></td>
         <td style="text-align:right;width:35%;">  <asp:Label ID="LblDigitalSign" runat="server" ></asp:Label></td>
         </tr>
         </table>
       
      
    </div>
    <div id="page-content" style="border: 1px solid #CEE3F6; z-index: -2; margin-top: -1px;
        overflow: auto;">
        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
            <ContentTemplate>
                <div style="text-align: center; padding: 2px;">
                    <table style="border: 1px solid #B6DAFD; background-color: #E8F3FE; margin-bottom: 5px;
                        width: 100%;">
                        <tr>
                            <td style="width: 80px; text-align: left">
                                Staff Code
                            </td>
                            <td style="width: 150px; font-weight: bold; text-align: center; background-color: White;
                                border: 1px solid #B6DAFD;">
                                <asp:HyperLink ID="lnkStaffCode" runat="server" Target="_blank"></asp:HyperLink>
                            </td>
                            <td style="width: 120px; text-align: left">
                                Staff Name
                            </td>
                            <td style="width: 200px; font-weight: bold; text-align: center; background-color: White;
                                border: 1px solid #B6DAFD;">
                                <asp:Label ID="lblStaffName" runat="server"></asp:Label>
                            </td>
                            <td style="width: 100px; text-align: left">
                                Rank
                            </td>
                            <td style="width: 100px; font-weight: bold; text-align: center; background-color: White;
                                border: 1px solid #B6DAFD;">
                                <asp:Label ID="lblRank" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnCrewrank" runat="server" />
                            </td>
                            <td style="width: 100px; text-align: left">
                                COC
                            </td>
                            <td style="width: 100px; font-weight: bold; text-align: center; background-color: White;
                                border: 1px solid #B6DAFD;">
                                <asp:Label ID="lblCOC" runat="server"></asp:Label>
                            </td>
                            <tr>
                                <td style="width: 80px; text-align: left">
                                    Evaluator
                                </td>
                                <td style="width: 150px; font-weight: bold; text-align: center; background-color: White;
                                    border: 1px solid #B6DAFD;">
                                    <asp:HyperLink ID="lnkEvaluator" runat="server" Target="_blank"></asp:HyperLink>
                                </td>
                                <td style="width: 120px; text-align: left">
                                    Evaluation Name
                                </td>
                                <td style="font-weight: bold; width: 200px; background-color: White; border: 1px solid #B6DAFD;">
                                    <asp:Label ID="lblEvalName" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px; text-align: left">
                                    Scheduled Month
                                </td>
                                <td style="font-weight: bold; width: 100px; background-color: White; border: 1px solid #B6DAFD;">
                                    <asp:Label ID="lblMonth" runat="server"></asp:Label>
                                </td>
                                <td style="width: 100px; text-align: left">
                                    Evaluation Date
                                </td>
                                <td style="width: 100px; font-weight: bold; text-align: center; background-color: White;
                                border: 1px solid #B6DAFD;">
                                    <asp:TextBox ID="txtEvaDate" runat="server" style="width: 100px; font-weight: bold; text-align: center; background-color: White;border: 1px solid #ffffff;color:#696969;" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calEvaDate" runat="server" TargetControlID="txtEvaDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                    </table>
                    <fieldset style="text-align: left; margin: 0px; padding: 2px">
                        <legend>
                            <asp:Label ID="lblStaffRank" runat="server" BackColor="Yellow">Evaluation Feedback</asp:Label>
                            <asp:LinkButton ID="lnkReqFeedBk" runat="server" CssClass="inline-edit" OnClick="btnReqFeedBk_Click">[Request Feedback]</asp:LinkButton>
                            <asp:LinkButton ID="lnkAddFeedBk" runat="server" CssClass="inline-edit" OnClick="btnReqFeedBk_Click">[Add Feedback]</asp:LinkButton>
                            <asp:LinkButton ID="lnkHide" runat="server" CssClass="inline-edit" OnClientClick="showFeedbackHistory();return false;">[Hide]</asp:LinkButton>
                            <asp:LinkButton ID="lnkShow" runat="server" CssClass="inline-edit" OnClientClick="showFeedbackHistory();return false;">[Show]</asp:LinkButton>
                        </legend>
                        <div id="dvFeedback" class="historyBox" style="display: block; border: 1px solid inset;
                            background-color: #efddef; padding: 2px; text-align: left;">
                            <div id="dvFeedbackHistory" style="width: 100%;">
                            </div>
                        </div>
                    </fieldset>
                    <table style="width: 100%" id="tbl_Result">
                        <tr>
                            <td>
                                <asp:GridView ID="GridView_AssignedCriteria" runat="server" AllowSorting="false" Width="100%"
                                    AutoGenerateColumns="False" CaptionAlign="Bottom" CellPadding="4" DataKeyNames="Criteria_ID"
                                    EmptyDataText="No Record Found" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView_AssignedCriteria_RowDataBound"
                                    BorderStyle="Solid" BorderColor="#cccccc" BorderWidth="1px">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <%# ((GridViewRow)Container).RowIndex + 1%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="40px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Question">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCriteria" runat="server" Text='<%#Eval("Criteria")%>'></asp:Label>
                                                <asp:HiddenField ID="hdnCriteria_ID" runat="server" Value='<%#Eval("Criteria_ID")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="300px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="N/A">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkNA" Checked='<%#Eval("Not_Applicable")%>' onclick="chkNA_Changed(this)"
                                                    runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select Option">
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="rdoOptions" runat="server" DataTextField="OptionText" DataValueField="ID" 
                                                    RepeatDirection="Horizontal" onclick="rdoOptions_Changed(this)">
                                                </asp:RadioButtonList>
                                                <asp:HiddenField ID="hdnRemark" runat="server" />
                                                <asp:TextBox ID="txtAnswer" runat="server" Width="90%" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="700px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemarks" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#58FA82" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF6FC" ForeColor="#333333" CssClass="grid-row" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblMsg" runat="server" Font-Size="Smaller" Font-Italic="true" ForeColor="Red"></asp:Label>
                                <asp:Label ID="txtVerificationComment" runat="server" Text=" Verification Comment :  "
                                    Style="vertical-align: top; font-weight: bold; width: 480px" />&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Height="50px" Width="480px"
                                    Style="vertical-align: top;"></asp:TextBox>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnSaveFollowUpAndClose" Text="Verify" runat="server" Style="vertical-align: top;
                                    font-weight: bold" OnClick="brnVerifySave" />
                                &nbsp;&nbsp; &nbsp;&nbsp;
                                <asp:Button ID="btnSaveEvaluation" runat="server" Text=" Save " OnClientClick="return Validate();"
                                    OnClick="btnSaveEvaluation_Click" Style="vertical-align: top; font-weight: bold" />
                                <asp:HiddenField ID="hdnOffice_ID" runat="server" />
                                <asp:HiddenField ID="hdnVessel_ID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <div style="border: 1px solid #e0e0e0; padding:5px; background-color:#f9f9f9;" id="dvEvalutionFooter" runat="server" visible="false">
                                 
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvCrewEvalFeedbackReq" style="display: none; width: 1000px;" title="<%= dvTitle %>">
        <asp:UpdatePanel ID="UpdatePanel_Frame" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <iframe id="ifrmFeedbackRequest" style="height: 550px; width: 1000px; border: 0px;
                    padding: 0; margin: 0;" frameborder="0" runat="server"></iframe>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
