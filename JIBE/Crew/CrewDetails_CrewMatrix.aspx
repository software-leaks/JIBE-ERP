<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_CrewMatrix.aspx.cs"
    Inherits="Crew_CrewDetails_CrewMatrix" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        #Panel1 tr
        {
            height: 25px;
        }
        #tblCM select
        {
            height: auto !important;
        }
        #btnSavePreJoiningExp
        {
            margin: 10px 0 0 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <div>
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
    </div>
    <div id="divMain" runat="server">
        <asp:HiddenField runat="server" ID="hdnFileSize" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlCrewMatrixMsg" runat="server" Visible="false" BackColor="Yellow"
                    ForeColor="Red" Font-Bold="true" HorizontalAlign="Center">
                    You don't have sufficient previlege to access the requested information.
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server">
                    <div style="text-align: left; margin: 0px; padding: 2px; font-size: 12px; font-family: Tahoma ,Tahoma, Sans-Serif,vrdana;">
                        <table width="47%" cellpadding="2"  style="margin: 5px 0px 5px 10px;" id="tblCM">
                            <tr>
                                <td width="190px">
                                    Certificate of competency
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCertOfComp" ClientIDMode="Static" runat="server" Width="156px"
                                        CssClass="control-edit">
                                    </asp:DropDownList>
                                    <asp:Image ID="imgRecordInfoCC" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                        Height="16px" Width="16px" runat="server" onclick="Get_Record_Info('CertificateCompetency',event,this)" />
                                </td>
                                <td width="250px">
                                    <span style="font-size: 11px; color: Red;" id="spnExpiryDate" runat="server" visible="true">
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Issuing country
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlCountry" ClientIDMode="Static" runat="server" Width="156px"
                                        CssClass="control-edit" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Administration acceptance
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlAdminAccept" runat="server" Width="156px" CssClass="control-edit"
                                        ClientIDMode="Static">
                                        <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        <asp:ListItem Text="Applied for" Value="Applied for"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Image ID="imgRecordInfoAA" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                        Height="16px" Width="16px" runat="server" onclick="Get_Record_Info('AdministrationAcceptance',event,this)" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tanker certification
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="2">
                                    <div class="chklstTankerCertification">
                                        <asp:CheckBoxList RepeatDirection="Horizontal" ClientIDMode="Static" runat="server"
                                            ID="chklstTankerCertification">
                                        </asp:CheckBoxList>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="width: 100px; overflow: auto;">
                                    STCW V Para
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlSTCW" runat="server" ClientIDMode="Static" Width="156px"
                                        CssClass="control-edit">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Radio qualification
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlRadioQual" runat="server" ClientIDMode="Static" Width="156px"
                                        CssClass="control-edit">
                                        <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Image ID="imgRecordInfoRadio" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                        Height="16px" Width="16px" runat="server" onclick="Get_Record_Info('RadioQualification',event,this)" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    English proficiency
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlEnglishProf" ClientIDMode="Static" runat="server" Width="156px"
                                        CssClass="control-edit">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="height: 10px;">
                                <td colspan="3">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Years with operator
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="2">
                                    <asp:Literal ID="ltrYearsWithOpeartor" Text="0" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Years in rank
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="2">
                                    <asp:Literal ID="ltrYearsinRank" Text="0" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Years on this type of tanker
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="2">
                                    <asp:Literal ID="ltrYearsthisTypeTanker" Text="0" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Years on all types of tanker
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="2">
                                    <asp:Literal ID="ltrYearsAllTypeTanker" Text="0" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Months on vessel this tour of duty
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="2">
                                    <asp:Literal ID="ltrTourOfDuty" Text="0" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Watchkeeping years(years watch)
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="2">
                                    <asp:Literal ID="ltrWatchkeepingYears" Text="0" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="btnSave" runat="server" Text=" Save " OnClick="btnSave_Click"></asp:Button>
                        <asp:Button ID="btnResetCrewMatrix" runat="server" Text="  Reset  " ClientIDMode="Static"
                            OnClick="btnReset_Click" OnClientClick="return CheckReset();"></asp:Button>
                        <span style="color: Gray;">Reset will restore all the fields to their calculated or
                            default values</span>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            
            if ($("#ddlCertOfComp").val() == "") {
                $("#spnExpiryDate").text("");
            }
            else
            {
                $("#spnExpiryDate").text("");
                if ($("#ddlCertOfComp option:selected").attr("dateofexpiry")!=undefined) {
                var date = $("#ddlCertOfComp option:selected").attr("dateofexpiry").substring(0, $("#ddlCertOfComp option:selected").attr("dateofexpiry").indexOf(" "));
                var dateofexpiry = new Date(parseInt(date.split('/')[2]), parseInt(date.split('/')[1]) - 1, parseInt(date.split('/')[0]));
                var convertDate =  $("#ddlCertOfComp option:selected").attr("convertdate");
                var today = new Date();
                if (dateofexpiry < today) {
                    $("#spnExpiryDate").text("Document has been expired on " + convertDate);
                }
            }
            }

            $("#ddlCountry").prop("disabled", "disabled");

            $("body").on("click", "#dvPopupAddAttachment_dvModalPopupHeader", function () {
                $("#dvPopupAddAttachment").hide();
                $("#overlay").hide();
            });
            $("body").on("change", "#ddlCertOfComp", function () {
                var countryofissue = $("#ddlCertOfComp option:selected").attr("countryofissue");
                $("#ddlCountry").val(countryofissue);
            });

            $("body").on("change", "#ddlEnglishProf", function () {
                if ($("#ddlEnglishProf").val() != "0")
                    window.parent.$("#ctl00_MainContent_lblEngProficiency").text($("#ddlEnglishProf").val());
                else
                    window.parent.$("#ctl00_MainContent_lblEngProficiency").text('');
            });


            $("body").on("change", "#ddlCertOfComp", function () {
                $("#spnExpiryDate").text("");
                if ($("#ddlCertOfComp option:selected").attr("dateofexpiry")!=undefined) {
                var date = $("#ddlCertOfComp option:selected").attr("dateofexpiry").substring(0, $("#ddlCertOfComp option:selected").attr("dateofexpiry").indexOf(" "));
                var dateofexpiry = new Date(parseInt(date.split('/')[2]), parseInt(date.split('/')[1]) - 1, parseInt(date.split('/')[0]));
                 var convertDate =  $("#ddlCertOfComp option:selected").attr("convertdate");
                var today = new Date();
                if (dateofexpiry < today) {
                    $("#spnExpiryDate").text("Document has been expired on " + convertDate);
                    }
                }                 
            });
        });


        function CheckReset() {
            if (!confirm("Are you sure, you want to reset")) {
                return false;
            }
        }

        function RedirectToLogin(URL) {
            window.parent.location.href = URL;
        }

        var lastExecutor = null;
        var e = null;
        var o = null;

        function Get_Record_Info(DocType, event, objthis) {
            e = event;
            o = objthis;
            var DocType1 = DocType.toString();
            if (lastExecutor != null)
                lastExecutor.abort();
            var service = Sys.Net.WebServiceProxy.invoke('/' + __app_name + '/JibeWebService.asmx', 'asyncGet_Record_Info', false, { "CrewId": <%= CrewID %>, "DocType": DocType1, "DateFormat": '<%= UDFLib.GetDateFormat() %>' }, OnSuccGet_Record_Info, Onfail, new Array(event, objthis));
            lastExecutor = service.get_executor();
        }

        function OnSuccGet_Record_Info(retval, prm) {
            try {
                js_ShowToolTip_Fixed(retval, e, o);
            }
            catch (ex)
            { }
        }

        function Onfail(retval) {
        }

    </script>
    </form>
</body>
</html>
