<%@ Page Title="Crew Matrix" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CrewMatrixNew.aspx.cs" Inherits="Crew_CrewMatrix" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <style type="text/css">
        .minAgg
        {
            background: #c1f0f6 none repeat scroll 0 0 !important;
            font-weight: bold;
        }
        .HeaderStyle
        {
            background-color: #cecece !important;
            border: 1px solid #959eaf;
            border-collapse: collapse;
            color: #333333;
            vertical-align: middle;
        }
        #tblCrewMatrix tr
        {
            text-align: center;
            height: 30px;
        }
        .error
        {
            color: Red;
        }
        .divflag
        {
            height: 23px;
            margin: -3px 0px 0px -3px;
            position: absolute;
            width: 22px !important;
        }
        .Event
        {
            background-image: url("../Images/GreenFlag.png");
        }
        .Simulated
        {
            background-image: url("../Images/YellowFlag.png");
        }
        .DifferentRank
        {
            background-image: url("../Images/RedFlag.png");
        }
        
        .CurrentStatusdiv
        {
            margin-left: 23px;
        }
        
        .divFlag
        {
            float: left;
            margin-right: 10px;
        }
        #tblCrewMatrix span
        {
            cursor: pointer;
        }
        #tblCrewMatrixAddtionalRules tr
        {
            height: 30px;
        }
        #tblCrewMatrix tr:hover
        {
            background-color: #feecec;
        }
        #tblCrewMatrixAddtionalRules tr:hover
        {
            background-color: #feecec;
        }
        .remarks
        {
            color: Black;
            padding-bottom: 14px;
        }
        .spnRemarks
        {
            font-weight: bold;
            text-decoration: underline;
        }
        .page-title
        {
            height: 18px !important;
        }
        .btnSimulate
        {
            cursor: pointer;
            margin: 3px 10px 0 30px;
        }
        .btnAssign
        {
            cursor: pointer;
        }
        #tblCrewMatrixAddtionalRules td
        {
            border: 1px solid #959eaf;
        }
        .MinAgg
        {
            cursor: default !important;
        }
        
        .ajax__calendar_invalid .ajax__calendar_year
        {
            color: Red !important;
        }
        .ajax__calendar_invalid .ajax__calendar_month
        {
            color: Red !important;
        }
        .ajax__calendar_invalid .ajax__calendar_day
        {
            color: Red !important;
        }
        .remarks p
        {
            margin: 0 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" class="page-title">
        Crew Matrix
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div style="margin-top: 2px; margin-bottom: 15px; vertical-align: bottom; padding: 2px;
            color: Black; text-align: left; background-color: #fff;">
            <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="ImgExpExcel" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="hdnVesselName" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnVesselID" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnDateFormat" ClientIDMode="Static" runat="server" Value="dd/MM/yyyy" />
                    <asp:HiddenField ID="hdnTodayDate" ClientIDMode="Static" runat="server" />
                    <div id="blur-on-updateprogress" class="divProgress" style="display: none;">
                        <img src="<%=Host %>Images/loaderbar.gif" style="position: absolute; left: 49%; top: 30px;
                            z-index: 2; color: black;" />
                    </div>
                    <table border="0" style="width: 100%; text-align: center">
                        <tr>
                            <td align="right" style="width: 220px">
                                <b>Vessel:</b>
                            </td>
                            <td align="left" style="width: 100px">
                                <asp:DropDownList ID="ddlVessel" runat="server" Width="150px" OnSelectedIndexChanged="ddlVessel_OnSelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td align="right" style="width: 100px">
                                <b>Type Of Vessel:</b>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTankerType" runat="server" Text="-"></asp:Label>
                                <asp:HiddenField ID="hdnTankerType" runat="server" Value="" />
                            </td>
                            <td colspan="5" width="520px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 160px">
                                <b>Oil Major:</b>
                            </td>
                            <td align="left" style="width: 160px">
                                <asp:DropDownList ID="drpOilMajors" ClientIDMode="Static" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td align="right" style="width: 120px">
                                <b>Vessel Event:</b>
                            </td>
                            <td align="left" style="width: 160px">
                                <asp:DropDownList Width="160" runat="server" ID="drpCrewEvent" Enabled="false" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 85px">
                                -OR-
                            </td>
                            <td align="right" style="width: 85px">
                                <b>Date:</b>
                            </td>
                            <td align="left" width="300px">
                                <asp:TextBox runat="server" ID="lblDate" Width="100px" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="lblDate">
                                </ajaxToolkit:CalendarExtender>
                                <input type="button" id="spnToday" value="Today" style="width: 77px" />
                            </td>
                            <td align="center" style="text-align: right;">
                                <img src="../Images/SearchButton.png" style="cursor: pointer; margin-right: 8px;"
                                    alt="Filter" id="btnFilter" title="Search" />
                                <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                    ImageUrl="~/Images/Exptoexcel.png" Style="display: none;" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
            <div id="gridOfficer" style="margin-top: 10px" runat="server">
                <table width="100%" id="tblGrid" runat="server">
                    <tr class="tdflagDescrption" style="display: none; height: 25px">
                        <td>
                            <div class="divFlag">
                                <div class="divflag Event">
                                </div>
                                <div class=" CurrentStatusdiv">
                                    <%=EventCrew%></div>
                            </div>
                            <div class="divFlag">
                                <div class="divflag Simulated">
                                </div>
                                <div class="CurrentStatusdiv">
                                    <%=SimulatedCrew%></div>
                            </div>
                            <div class="divFlag">
                                <div class="divflag DifferentRank">
                                </div>
                                <div class="CurrentStatusdiv">
                                    <%=DifferentRank%></div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" id="tdCrewMatrixGrid" runat="server" clientidmode="Static">
                            <asp:Literal ID="ltrCrewMatrixGrid" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hdnActualCrewID" Value="" runat="server" ClientIDMode="Static" />
        </div>
        <div id="dvPopupFrame" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
            border-style: solid; border-width: 1px; position: absolute; left: 0.5%; top: 15%;
            width: 1000; z-index: 1; color: black; height: 750px" title=''>
            <div class="content" id="divContentdvPopupFrame">
                <iframe id="frPopupFrame" src="" frameborder="0" height="100%" width="100%"></iframe>
            </div>
        </div>
        <div id="dvPopupFrame1" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
            border-style: solid; border-width: 1px; position: absolute; left: 0.5%; top: 15%;
            width: 500px; height: 230px; z-index: 1; color: black" title=''>
            <div class="content" style="height: 350px">
                <iframe id="frPopupFrame1" src="" frameborder="0" height="100%" width="100%"></iframe>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var __app_name = location.pathname.split('/')[1];
            $("body").on("click", "#drpCrewEvent", function () {
                if ($("#drpCrewEvent").val() == 0) {
                    $("#<%=lblDate.ClientID %>").val($("#hdnTodayDate").val());
                }
                else {
                    $("#ctl00_MainContent_lblDate").val('');
                }
            });

            ///Reset to today's date
            $("body").on("click", "#spnToday", function () {
                $("#<%=lblDate.ClientID %>").val($("#hdnTodayDate").val());
                $("#drpCrewEvent").val(0);
                $("#btnFilter").click();
                return false;
            });

            ///vessel dorp down change today date 
            $("body").on("change", "#ctl00_MainContent_ddlVessel", function () {
                $("#<%=lblDate.ClientID %>").val($("#hdnTodayDate").val());
                $(".tdflagDescrption").fadeOut();
                $("#tdCrewMatrixGrid").html('');
            });

            ///Reset Crew event dropdowm on date change
            $("body").on("change", "#ctl00_MainContent_lblDate", function () {
                $("#drpCrewEvent").val(0);
            });


            ///Filter record
            $("body").on("click", "#btnFilter", function () {
                if ($("#ctl00_MainContent_ddlVessel").val() == "0") {
                    alert("Please select Vessel");
                    $("#ctl00_MainContent_ddlVessel").focus();
                    return false;
                }

                if ($("#drpOilMajors").val() == "0") {
                    alert("Please select Oil Major");
                    $("#drpOilMajors").focus();
                    return false;
                }

                if ($("#drpCrewEvent").val() == "0" && $("#ctl00_MainContent_lblDate").val() == "") {
                    alert("Please select Vessel Event or Date");
                    $("#ctl00_MainContent_lblDate").focus();
                    return false;
                }

                if ($("#<%=lblDate.ClientID %>").val() != "") {
                  if(IsInvalidDate($("#<%=lblDate.ClientID %>").val(),$("#hdnDateFormat").val()))
                    {
                      alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                      $("#<%=lblDate.ClientID %>").focus();
                      return false;
                    }
                 }

                if ($("#ctl00_MainContent_lblDate").val() != "") {
                    
                    /// Previous date check 
                    if (new DateAsFormat($.trim($("#ctl00_MainContent_lblDate").val()),$("#hdnDateFormat").val()).getDateOnly() < new Date().getDateOnly()) {
                        alert("Do not enter past date");
                        $("#ctl00_MainContent_lblDate").focus();
                        return false;
                    }

                    /// Check if future date is manully entered
                    var numberOfDaysToAdd = <%=strFutureDateRes %>;
                    if (parseInt(numberOfDaysToAdd)>0) {
                    var someDate = new Date().getDateOnly();
                    someDate.setDate(someDate.getDate() + numberOfDaysToAdd);
                    if (new DateAsFormat($.trim($("#ctl00_MainContent_lblDate").val()),$("#hdnDateFormat").val()).getDateOnly()>someDate) {
                        alert("You cannot enter future date");
                        $("#ctl00_MainContent_lblDate").focus();
                        return false;
                    }
                    }
                    
                }
               
                BindCrewGrid(0, 0, 0, 0, 0, false);
                $("#ctl00_MainContent_ImgExpExcel").show();
            });

            $("body").on("mouseover", ".spnStaffName", function () {
                showCrewInfo($(this).attr("rel"));
            });

            $("body").on("mouseout", ".spnStaffName", function () {
                $("#__divMsgTooltip").fadeOut();
            });


            ///Crew info popup
            function showCrewInfo(StaffCode) {
                var scode_ = StaffCode;
                if (scode_ > 0) {
                    if (lastExecutor_WebServiceProxy != null)
                        lastExecutor_WebServiceProxy.abort();
                          var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
                    var service = Sys.Net.WebServiceProxy.invoke("/" + __app_name + '/JibeWebService.asmx', 'Get_StaffInfo', false, { "StaffCode": scode_, "DateFormat": DateFormat.toString()  }, Get_StaffInfo_onSuccess, Get_StaffInfo_onFail);
                    lastExecutor_WebServiceProxy = service.get_executor();
                }
            }

            function Get_StaffInfo_onSuccess(retval) {
                try {
                    js_ShowToolTip(retval, evt, objthis)
                }
                catch (ex) { }
            }

            $("body").on("mouseover", ".tdTooltip", function () {
                var splitCrew=$(this).attr("rel").split(", ");
                var nottankertype=$(this).attr("nottankertype");

                var html="";
                if (nottankertype=="1") {
                    html= splitCrew;
                }
                else
                {
                    for (var i = 0; i < splitCrew.length; i++) {
                      if (i==0) {
                         html +="<table class='GridView-css' style='100%; border-collapse: collapse;' border='1'><tr class='HeaderStyle'><th>&nbsp;&nbsp;Rank&nbsp;&nbsp;</th><th>&nbsp;&nbsp;Staff Code&nbsp;&nbsp;</th><th>&nbsp;&nbsp;Name&nbsp;&nbsp;</th></tr>";
                       }
                         html +="<tr><td>&nbsp;&nbsp;"+splitCrew[i].split(":")[0]+"&nbsp;&nbsp;</td><td>&nbsp;&nbsp;"+splitCrew[i].split(":")[2]+"&nbsp;&nbsp;</td><td>&nbsp;&nbsp;"+splitCrew[i].split(":")[1]+"&nbsp;&nbsp;</td></tr>";
                       if (i==splitCrew.length) {
                        html +="</table>";
                       }
                    }
                }

                js_ShowToolTip(html, evt, objthis)
            });

            $("body").on("mouseout", ".tdTooltip", function () {
                $("#__divMsgTooltip").hide();
                $("#__divMsgTooltip").html('');
            });

            //call Simulate popup
            $("body").on("click", ".btnSimulate", function () {
                var RankId = $(this).attr("rankid");
                var CurrentCrewID = $(this).attr("crewid");
                var Nationality = $(this).attr("nationality");
                var ActualCrewID = $(this).attr("actualcrewid");
                var VesselId = $(this).attr("VesselId");
                Simulate(CurrentCrewID, RankId, Nationality,VesselId);
                
                $("#hdnActualCrewID").val(ActualCrewID);
                return false;
            });

            $("body").on("click", "#closePopupbutton", function () {
                $("#hdnActualCrewID").val('');
                $("#frPopupFrame").attr("src", "");
            });

            function Simulate(CrewId, RankId, CountryId,VesselId) {
                $('#dvPopupFrame').attr("Title", "Crew Matrix Simulation");
                $('#dvPopupFrame').css({ "width": "1100px", "text-allign": "center" });
                $('#dvPopupFrame').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });
                var URL = "CrewMatrix_Simulation.aspx?CrewId=" + CrewId + "&CountryId=" + CountryId + "&RankId=" + RankId + "&vesselid=" +VesselId + "&rnd=" + Math.random(); 
                document.getElementById("frPopupFrame").src = URL;
                showModal('dvPopupFrame', false);
            }

            //call Assignment popup
            $("body").on("click", ".btnAssign", function () {
                var OffSignerCrewId = $(this).attr("OffSignerCrewId");
                var OnSignerCrewId = $(this).attr("OnSignerCrewId");
                var rankid = $(this).attr("rankid");

                var VesselID = $(this).attr("vesselid");
                if (CheckAssign(OnSignerCrewId, OffSignerCrewId, rankid,VesselID)) {
                    Assignment(OnSignerCrewId, OffSignerCrewId, VesselID, rankid);
                }
                return false;
            });


            ///Check onsigner current assignments
            function CheckAssign(OnSignnerCrewId, OffSignnerCrewId, RankId,VesselID) {
                var options = {
                    url: 'CrewMatrix_Assignment.aspx?Method=CheckAssign&OnSignnerCrewId=' + OnSignnerCrewId + '&OffSignnerCrewId=' + OffSignnerCrewId + "&RankId=" + RankId+ "&VesselId=" +VesselID,
                    dataType: 'html',
                    type: 'POST',
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        res = response;
                    }
                }
                $.ajax(options);

                if (res == -5) {
                    alert('This crew is already onboard, you can plan a transfer for this crew');
                    return false;
                }
                else
                    return true;
            }

            function Assignment(OnSignerCrewId, OffSignerCrewId, VesselID, RankId) {
                ShowLoader();
                $('#dvPopupFrame1').attr("Title", "Crew Matrix Assignment");
                $('#dvPopupFrame1').css({ "width": "700px", "height": "230px", "text-allign": "center" });
                $('#dvPopupFrame1').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });
                var URL = "CrewMatrix_Assignment.aspx?OnSignnerCrewId=" + OnSignerCrewId + "&OffSignnerCrewId=" + OffSignerCrewId + "&VesselID=" + VesselID + "&RankId=" + RankId;
                document.getElementById("frPopupFrame1").src = URL;
                showModal('dvPopupFrame1', false);
                HideLoader();
            }


            //Select only one crew from multiple crew list of same rank
            $("body").on("click", ".chkMultiCrew", function () {
                var CrewID = $(this).attr("crewid");
                var chk = $(this).prop("checked");

                //Send request for min aggregated crew
                BindCrewGrid(0, 0, 0, 0, CrewID, chk);
            });


             $("body").on("change", "#ctl00_MainContent_ddlVessel", function () {
                if ($("#ctl00_MainContent_ddlVessel :selected").val()!="0") 
                   $("#ctl00_MainContent_lblTankerType").text($("#ctl00_MainContent_ddlVessel :selected").attr("vesseltype"));
                else
                   $("#ctl00_MainContent_lblTankerType").text("-");
             });

        });

        ///Crew matrix grid
        function BindCrewGrid(JoiningRank, OffSignerCrewId, OnSignerCrewId, ActualRank, MinAggCrewId, MinAggCrewChkd) {
            try {
                ShowLoader();
                var VesselId = "&VesselId=" + $("#ctl00_MainContent_ddlVessel").val();
                var OilMajorID = "&OilMajorID=" + $("#drpOilMajors").val();
                var CrewEventID = "&CrewEventID=" + $("#drpCrewEvent").val();
                var flrDate = "&flrDate=" + $("#ctl00_MainContent_lblDate").val();
                JoiningRank = "&JoiningRank=" + JoiningRank;
                OffSignerCrewId = "&OldCrewId=" + OffSignerCrewId;
                OnSignerCrewId = "&SimulatedCrewId=" + OnSignerCrewId;
                var ActualCrewId = "&ActualCrewId=" + $("#hdnActualCrewID").val();
                var ActualRank = "&ActualRank=" + ActualRank;
                var TankerType = "&TankerType=" +$("#ctl00_MainContent_hdnTankerType").val();

                //for mini Aggrated calculations
                if (MinAggCrewId == undefined)
                    MinAggCrewId = 0
                MinAggCrewId = "&MinAggCrewId=" + MinAggCrewId;

                if (MinAggCrewChkd == undefined)
                    MinAggCrewChkd = false;
                MinAggCrewChkd = "&MinAggCrewChkd=" + MinAggCrewChkd;

                //time span for new data
                var d = new Date();
                var timespan = d.getTime();

                var options = {
                    url: 'CrewMatrixNew.aspx?' + JoiningRank + OffSignerCrewId + OnSignerCrewId + VesselId + OilMajorID + CrewEventID + flrDate + ActualCrewId + ActualRank + MinAggCrewId + MinAggCrewChkd +TankerType +"&t="+timespan,
                    dataType: 'html',
                    type: 'GET',
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        if (response != '') {
                            if(response=="LOGOUT")
                            {
                             window.location.href = $(location).attr("href");
                            }
                            else
                            {
                            $(".tdflagDescrption").show();
                            $("#tdCrewMatrixGrid").show();
                            $("#tdCrewMatrixGrid").html(response);
                            }
                        }
                        else {
                            $(".tdflagDescrption").hide();
                        }
                       HideLoader();
                    },
                     error: function(){
                           HideLoader();
                    }
                }
                $.ajax(options);

            } catch (e) {
                $(".tdflagDescrption").hide();

            }
        }

        function ShowLoader() {
        //document.getElementById("divProgress").style.display = 'block';
        $(".divProgress").show();
        }

        function HideLoader() {
         //document.getElementById("divProgress").style.display = 'none';
         $(".divProgress").hide();
        }
    </script>
</asp:Content>
