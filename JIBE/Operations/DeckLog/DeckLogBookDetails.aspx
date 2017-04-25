<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="DeckLogBookDetails.aspx.cs"
    Inherits="DeckLogBookDetails" Title="Deck Log Book Details" EnableEventValidation="false"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    title
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/Deck_Engine_LogBook.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(ReplacetxtBox);     
        function LogBookThresholdSettingPage() {
            var LogBookID = document.getElementById('<%=hdnLogBookID.ClientID%>').value;
            var Vessel_ID = document.getElementById('<%=hdnVesselID.ClientID%>').value;

            var url = 'DeckLogBookThreshold.aspx?LogBookID=' + LogBookID + '&Vessel_ID=' + Vessel_ID;
            // OpenPopupWindow('LogBookThresholdSettings', 'ThresholdSettings', url, 'popup', 180, 700, null, null, true, false, true, Page_Closed);
            window.open(url, "test", "", "");
        }

        function ReplacetxtBox() {


            var txt = document.getElementsByClassName("txtBox");

            for (var j = 0; j < txt.length; j++) {

                $(txt[j]).replaceWith($(txt[j]).val());

                txt = document.getElementsByClassName("txtBox");

                if (txt.length == 0) {
                    break;
                }
                else {
                    j = -1;
                }

            }

            return false;

        }  

        function WaterInHoldThresholdSettingPage() {

            var LogBookID = document.getElementById('<%=hdnLogBookID.ClientID%>').value;
            var Vessel_ID = document.getElementById('<%=hdnVesselID.ClientID%>').value;

            var url = 'DeckLogWaterInHoldThreshold.aspx?LogBookID=' + LogBookID + '&Vessel_ID=' + Vessel_ID;
            OpenPopupWindow('WaterInHoldThresholdSettings', 'ThresholdSettings', url, 'popup', 180, 400, null, null, true, false, true, Page_Closed);

        }


        function WaterInTankThresholdSettingPage() {

            var LogBookID = document.getElementById('<%=hdnLogBookID.ClientID%>').value;
            var Vessel_ID = document.getElementById('<%=hdnVesselID.ClientID%>').value;

            var url = 'DeckLogWaterInTankThreshold.aspx?LogBookID=' + LogBookID + '&Vessel_ID=' + Vessel_ID;
            OpenPopupWindow('WaterInTankThresholdSettings', 'ThresholdSettings', url, 'popup', 180, 400, null, null, true, false, true, Page_Closed);

        }


        function OpenIncidentParticipantPage(Incident_ID, Vessel_ID) {
            var url = 'DeckLogBookParticipant.aspx?Incident_ID=' + Incident_ID + '&Vessel_ID=' + Vessel_ID;

            OpenPopupWindow('IncidentParticipant', 'IncidentParticipant', url, 'popup', 400, 630, null, null, true, false, true, Page_Closed);

        }

        function OpenIncidentParticipantAttachmentPage(Incident_ID, Vessel_ID) {

            var url = 'DeckLogBookIncidentAttachment.aspx?Incident_ID=' + Incident_ID + '&Vessel_ID=' + Vessel_ID;
            OpenPopupWindow('IncidentParticipantAtt', 'IncidentParticipantAtt', url, 'popup', 750, 900, null, null, true, false, true, Page_Closed);

        }

        function Page_Closed() {
            return true;
        }

        function GetDivData() {

            var cell = document.getElementsByClassName('CellClass1');
            for (var i = 0; i < cell.length; i++) {

                cell[i].className = "CellClass0";

                if (cell.length >= 0) {
                    var cell = document.getElementsByClassName('CellClass1');
                    i = -1;
                }
                
            }
            var img = document.getElementsByClassName('transactLog');

            for (var i = 0; i < img.length; i++) {

                img[i].src = document.getElementById($('[id$=hdnBaseURL]').attr('id')).value + "Uploads/CrewImages/" + img[i].nameProp;



            }
            $("a").contents().unwrap()
            document.getElementById($('[id$=lblHead]').attr('id')).textContent = "CHIEF OFFICER'S LOG BOOK"
            var str = document.getElementById('dvPageContent').innerHTML;
            document.getElementById($('[id$=lblHead]').attr('id')).textContent = "";
            document.getElementById($('[id$=hdnContent]').attr('id')).value = str;
            //        __dopostback("BtnPrintPDF", "onclick")
            __doPostBack("<%=BtnPrintPDF.UniqueID %>", "onclick");


        }


        $(document).ready(function () {

            var wh = 'ID=<%=Request.QueryString["DeckLogBookID"]%> and Vessel_ID=<%=Request.QueryString["Vessel_ID"]%>';

            Get_Record_Information_Details('OPS_Lib_DeckLog_Book', wh);

            Get_Deck_Record_Information_Details('OPS_Lib_DeckLog_Book', wh);

        });
    </script>
    <style type="text/css">
        body, html
        {
            overflow-x: hidden;
        }
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid black;
        }
        .doublescroll
        {
            overflow: hidden;
            overflow-x: scroll;
        }
        .doublescroll p
        {
            margin: 0;
            padding: 1em;
            white-space: nowrap;
        }
        .CellClass1
        {
            background-color: #FA5858;
            color: White;
            border: 1px solid black;
            border-right: 1px solid black;
        }
        .CellClass0
        {
            border: 1px solid black;
            border-right: 1px solid black;
        }
        
        .CellClassChangeFlage1
        {
            background-color: Yellow;
            color: Black;
            border: 1px solid black;
            border-right: 1px solid #ccccccblack;
        }
        
        .CellClassChangeFlage0
        {
            border: 1px solid black;
            border-right: 1px solid black;
        }
        
        .HeaderCellColor
        {
            background-color: #3399CC;
            color: White;
        }
        
        .FieldDottedLine
        {
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            border-bottom: 1px dashed;
        }
        .CreateHtmlTableFromDataTable-table
        {
            background-color: #FFFFFF;
            border: 0px solid black;
        }
        
        .CreateHtmlTableFromDataTable-PageHeader
        {
            background-color: #F6B680;
        }
        
        .CreateHtmlTableFromDataTable-DataHedaer
        {
            background-color: #CCCCCC;
            border: 1px solid black;
            text-align: center;
        }
        .CreateHtmlTableFromDataTable-Data
        {
            border: 1px solid black;
        }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            function DoubleScroll(element) {
                var scrollbar = document.createElement('div');
                scrollbar.appendChild(document.createElement('div'));
                scrollbar.style.overflow = 'auto';
                scrollbar.style.overflowY = 'hidden';
                scrollbar.firstChild.style.width = element.scrollWidth + 20 + 'px';
                scrollbar.firstChild.style.paddingTop = '1px';
                scrollbar.firstChild.appendChild(document.createTextNode('\xA0'));
                scrollbar.onscroll = function () {
                    element.scrollLeft = scrollbar.scrollLeft;
                };
                element.onscroll = function () {
                    scrollbar.scrollLeft = element.scrollLeft;
                };
                element.parentNode.insertBefore(scrollbar, element);
            }
            DoubleScroll(document.getElementById('dvPageContent'));
            //        var conts = document.querySelectorAll(".doubleScroll");
            //        for (var i = 0; i < conts.length; i++)
            //            DoubleScroll(conts[i]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
         
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/1.png" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div  style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">

            <div id="page-header" class="page-title">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                       
                        <td align="center">
                            <b>&nbsp;CHIEF OFFICER&#39;S LOG BOOK</b>
                        </td>
                        <td  align="right" >
                          <a href="Logbook_Scales_Description.pdf" target="_blank" >Scale</a>
                         </td>
                    </tr>
                </table>
                <div style="visibility: hidden">
                    <asp:HiddenField ID="hdnVesselID" runat="server" />
                    <asp:HiddenField ID="hdnLogBookID" runat="server" />
                    <asp:Button ID="btnHiddenSubmit" runat="server" Text="btnHiddenSubmit" OnClick="btnHiddenSubmit_Click" />
                </div>
            </div>
            <div style="float:right;width:100%; " align="right">
            <table>
                <tr>
        
                          <td class="CellClass0">
                                            <input id="btnLogBookThreshold" type="button" value="Threshold - Log Book" style="border: 1px solid white;
                                                cursor: pointer; background-color: Silver;  width: 145px" onclick="LogBookThresholdSettingPage()" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="BtnPrintPDF" runat="server" OnClientClick="GetDivData();"
                                            ImageUrl="~/Images/PDF-icon.png"  Height="20px" ToolTip="Print" 
                                            OnClick="BtnPrintPDF_Click"></asp:ImageButton>
                                        </td>
                                    </tr>

                                    </table>
            </div>
        
        <br />
            <div style="text-align: left;  width:100%;" id="dvPageContent" >
          
            <style type="text/css">
        body, html
        {
            overflow-x: hidden;
        }
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid black;
        }
        .doublescroll
        {
            overflow: hidden;
            overflow-x: scroll;
        }
        .doublescroll p
        {
            margin: 0;
            padding: 1em;
            white-space: nowrap;
        }
        .CellClass1
        {
            background-color: #FA5858;
            color: White;
            border: 1px solid black;
            border-right: 1px solid black;
        }
        .CellClass0
        {
            border: 1px solid black;
            border-right: 1px solid black;
        }
        
        .CellClassChangeFlage1
        {
            background-color: Yellow;
            color: Black;
            border: 1px solid black;
            border-right: 1px solid black;
        }
        
        .CellClassChangeFlage0
        {
            border: 1px solid black;
            border-right: 1px solid black;
        }
        
        .HeaderCellColor
        {
            background-color: #3399CC;
            color: White;
        }
        
        .FieldDottedLine
        {
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            border-bottom: 1px dashed;
        }
        .CreateHtmlTableFromDataTable-table
        {
            background-color: #FFFFFF;
            border: 0px solid black;
        }
        
        .CreateHtmlTableFromDataTable-PageHeader
        {
            background-color: #F6B680;
        }
        
        .CreateHtmlTableFromDataTable-DataHedaer
        {
            background-color: #CCCCCC;
            border: 1px solid black;
            text-align: center;
        }
        .CreateHtmlTableFromDataTable-Data
        {
            border: 1px solid black;
        }
    </style>
        <div style="text-align:center;">
             <asp:Label ID="lblHead" runat="server" Text="" Font-Size="32px" Font-Names="Tahoma"></asp:Label>
             </div>
                <table cellspacing="0" cellpadding="0" rules="all" width="100%" style="border: 1px solid black; ">
                    <tr>
                        <td class="CellClass0">
                            <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px;font-family: Tahoma; font-size: 12px;">
                                <table cellspacing="1" cellpadding="0" rules="all" width="100%" style="border: 0px solid black;
                                    background-color: White;">
                                
                                    <tr>
                                        <td width="6%;" align="right" style="border-right: 1px solid white">
                                            <b>M. V :&nbsp;&nbsp;</b>
                                        </td>
                                        <td align="left" style="border-right: 1px solid white">
                                            <asp:Label ID="lblVessel" CssClass="FieldDottedLine" Width="150px" runat="server"> </asp:Label>
                                        </td>
                                        <td width="6%;" align="right" style="border-right: 1px solid white">
                                            <b>FROM :&nbsp;&nbsp;</b>
                                        </td>
                                        <td align="left" style="border-right: 1px solid white">
                                            <asp:Label ID="lblFrom" Width="150px" CssClass="FieldDottedLine" runat="server"> </asp:Label>
                                        </td>
                                        <td width="6%;" align="right" style="border-right: 1px solid white">
                                            <b>Towards :&nbsp;</b>
                                        </td>
                                        <td align="left" style="border-right: 1px solid #cccccc">
                                            <asp:Label ID="lblTo" Width="150px" CssClass="FieldDottedLine" runat="server"> </asp:Label>
                                        </td>
                                        <td align="right" >
                                            <b> Report Date : &nbsp;</b>
                                        </td>
                                        <td align="left" >
                                            <asp:Label ID="lblReportDate" runat="server"></asp:Label>
                                        </td>
                                      
                                    <%--    <td align="left" width="7%;" class="CellClass0">
                                            <input id="btnWanterInHoldThreshold" type="button" value="Threshold - Water In Hold"
                                                style="border: 1px solid white; cursor: pointer; background-color: Silver; height: 18px;
                                                width: 160px" onclick="WaterInHoldThresholdSettingPage()" />
                                        </td>
                                        <td align="left" width="7%;" class="CellClass0">
                                            <input id="btnWanterInTankThreshold" type="button" value="Threshold - Water In Tank"
                                                style="border: 1px solid white; cursor: pointer; background-color: Silver; height: 18px;
                                                width: 160px" onclick="WaterInTankThresholdSettingPage()" />
                                        </td>
                                        <td>
                                        </td>--%>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" valign="top">
                            <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px;font-family: Tahoma; font-size: 12px;">
                                <asp:Repeater ID="rpDeckLogBook01" runat="server">
                                    <HeaderTemplate>
                                        <table cellspacing="1" cellpadding="1" rules="all" style="border: 1px solid black"
                                            width="100%">
                                            <tr align="center">
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Hours
                                                </td>
                                                <td colspan="4" class="HeaderCellColor">
                                                    COURSES
                                                </td>
                                                <td colspan="2" class="HeaderCellColor">
                                                    ERROR
                                                </td>
                                                <td colspan="2" class="HeaderCellColor">
                                                    WINDS
                                                </td>
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Sea
                                                </td>
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Sky
                                                </td>
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Visibility
                                                </td>
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Barometer
                                                </td>
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Air Dry
                                                </td>
                                                  <td rowspan="2" class="HeaderCellColor">
                                                    Air Wet
                                                </td>
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Sea Temp
                                                </td>
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Log Reading
                                                </td>
                                                <td align="left" rowspan="2" class="HeaderCellColor">
                                                    REMARKS, Etc. (Note Carefully When Boats Are Exercised)
                                                </td>
                                                 <td align="left" rowspan="2" class="HeaderCellColor"  width="200px">
                                                    Last Updated
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td class="HeaderCellColor">
                                                    True
                                                </td>
                                                <td class="HeaderCellColor">
                                                    Gyro
                                                </td>
                                                <td class="HeaderCellColor">
                                                    Standard
                                                </td>
                                                <td class="HeaderCellColor">
                                                    Steering
                                                </td>
                                                <td class="HeaderCellColor">
                                                    Gyro
                                                </td>
                                                <td class="HeaderCellColor">
                                                    Standard
                                                </td>
                                                <td class="HeaderCellColor">
                                                    Direction
                                                </td>
                                                <td class="HeaderCellColor">
                                                    Force
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr align="center">
                                            <td align="left" style="height: 16px;" class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Log_Hours_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Log_Hours")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_True_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_True")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_Gyro_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_Gyro&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_Gyro")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_Standard_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_Steering&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_Standard")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_Steering_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_Steering&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_Steering")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Error_Gyro_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Error_Gyro&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Error_Gyro_Color").ToString() + "  CellClassChangeFlage" + DataBinder.Eval(Container.DataItem,"Error_Gyro_Change_Flag").ToString() %>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Error_Gyro")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Error_Standard_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Error_Standard_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Error_Standard")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Winds_Direction_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Winds_Direction_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Winds_Direction")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Winds_Force_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Winds_Force_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Winds_Force")%>
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Sea_Color").ToString()%>' <%# DataBinder.Eval(Container.DataItem,"Sea_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Sea")%>
                                            </td>
                                            <td style="height: 19px;" class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Sky_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Sky")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Visibility_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Visibility_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Visibility")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Barometer_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Barometer_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Barometer")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"AirTemp_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"AirTemp_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "AirTemp")%>
                                            </td>

                                            <td style="height: 19px;">
                                                <%# DataBinder.Eval(Container.DataItem, "AirTemp_Wet")%>
                                            </td>

                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"SeaTemp_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"SeaTemp_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "SeaTemp")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"LogReading_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class="CellClass0">
                                                <%# DataBinder.Eval(Container.DataItem, "LogReading")%>
                                            </td>
                                            <td align="left" style="height: 19px;" class="CellClass0">
                                                <%# DataBinder.Eval(Container.DataItem, "Remark")%>
                                            </td>
                                              <td align="left" style="height: 19px;" class="CellClass0" style="white-space:nowrap">
                                                <%# DataBinder.Eval(Container.DataItem, "LastUp")%>
                                            </td>
                                         
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr style="background-color: #efefcf; border: 1px solid black" align="center">
                                            <td align="left" style="height: 16px;" class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Log_Hours_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Log_Hours")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_True_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_True")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_Gyro_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_Gyro&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_Gyro")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_Standard_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_Steering&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_Standard")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_Steering_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_Steering&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_Steering")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Error_Gyro_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Error_Gyro&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Error_Gyro_Color").ToString() + "  CellClassChangeFlage" + DataBinder.Eval(Container.DataItem,"Error_Gyro_Change_Flag").ToString() %>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Error_Gyro")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Error_Standard_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Error_Standard_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Error_Standard")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Winds_Direction_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Winds_Direction_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Winds_Direction")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Winds_Force_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Winds_Force_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Winds_Force")%>
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Sea_Color").ToString()%>' <%# DataBinder.Eval(Container.DataItem,"Sea_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Sea")%>
                                            </td>
                                            <td style="height: 19px;" class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Sky_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Sky")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Visibility_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Visibility_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Visibility")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Barometer_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Barometer_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Barometer")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"AirTemp_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"AirTemp_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "AirTemp")%>
                                            </td>
                                            <td style="height: 19px;">
                                                <%# DataBinder.Eval(Container.DataItem, "AirTemp_Wet")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"SeaTemp_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"SeaTemp_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "SeaTemp")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"LogReading_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class="CellClass0">
                                                <%# DataBinder.Eval(Container.DataItem, "LogReading")%>
                                            </td>
                                            <td align="left" style="height: 19px;" class="CellClass0">
                                                <%# DataBinder.Eval(Container.DataItem, "Remark")%>
                                            </td>
                                            <td align="left" style="height: 19px;" class="CellClass0"  style="white-space:nowrap">
                                                <%# DataBinder.Eval(Container.DataItem, "LastUp")%>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" style="border-top: 1px solid black;">
                            <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px; font-family: Tahoma; font-size: 12px;">
                                <table cellspacing="2" cellpadding="1" rules="all" width="100%" style="border-top: 2px solid black;
                                    border-left: 2px solid black; border-right: 2px solid black; border-bottom: 2px solid black;">
                                    <tr align="center">
                                        <td style="border-right: 2px solid black; border-left: 2px solid black; border-bottom: 2px solid black;">
                                        </td>
                                        <td colspan="3" align="right" style="border-right: 1px solid black; border-bottom: 2px solid black;">
                                            <b>CURRENT SET & DRIFT :</b>
                                        </td>
                                        <td colspan="7" align="left" style="border-right: 1px solid black; border-bottom: 2px solid black;
                                            padding: 5px">
                                            <asp:Label ID="lblCurrentSetDrift" runat="server" Text="Label" Width="200px" CssClass="FieldDottedLine"></asp:Label>
                                        </td>
                                        <td align="right" style="border-right: 2px solid white; border-bottom: 2px solid black;">
                                            <b>Total Engine Reves. :</b>
                                        </td>
                                        <td align="left" style="border-right: 2px solid white; border-bottom: 2px solid black;">
                                            <asp:Label ID="lblTotalEngineRevs" runat="server" Text="Label" CssClass="FieldDottedLine"
                                                Width="100px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td rowspan="2" align="center" style="width: 5%; border-right: 2px solid black; border-bottom: 2px solid white;">
                                            True Course Made to Noon
                                        </td>
                                        <td colspan="2" align="center" style="width: 10%; font-weight: bold; font-size: x-small;
                                            border-right: 2px solid black; border-bottom: 2px solid black;">
                                            Distance in Nautical Miles
                                        </td>
                                        <td colspan="2" align="center" style="width: 10%; font-weight: bold; font-size: x-small;
                                            border-right: 2px solid black; border-bottom: 2px solid black;">
                                            Latitude
                                        </td>
                                        <td colspan="2" align="center" style="width: 10%; font-weight: bold; font-size: x-small;
                                            border-right: 2px solid black; border-bottom: 2px solid black;">
                                            Longitude
                                        </td>
                                        <td colspan="2" align="center" style="width: 10%; font-weight: bold; font-size: x-small;
                                            border-right: 2px solid black; border-bottom: 2px solid black;">
                                            Steaming time
                                        </td>
                                        <td colspan="2" align="center" style="width: 10%; font-weight: bold; font-size: x-small;
                                            border-right: 2px solid black; border-bottom: 2px solid black;">
                                            Speed
                                        </td>
                                        <td align="right" style="border-right: 1px solid white; border-bottom: 1px solid white;">
                                            Distance per Log :
                                        </td>
                                        <td align="left" style="border-bottom: 1px solid white;">
                                            <asp:Label ID="lblDistperLog" runat="server" Text="Label" CssClass="FieldDottedLine"
                                                Width="100px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td align="right" style="width: 5%; border-bottom: 1px solid white; border-right: 1px solid white;">
                                            Days Run :
                                        </td>
                                        <td align="left" style="width: 5%; border-right: 2px solid black; border-bottom: 1px solid white;">
                                            <asp:Label ID="lblDaysRun" runat="server" Text="Label" CssClass="FieldDottedLine"
                                                Width="50px"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 5%; border-bottom: 1px solid white; border-right: 1px solid white;">
                                            By Ac&#39;nt :
                                        </td>
                                        <td align="left" style="width: 5%; border-right: 2px solid black; border-bottom: 1px solid white;">
                                            <asp:Label ID="lbllati_Acnt" runat="server" Text="Label" CssClass="FieldDottedLine"
                                                Width="50px"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 5%; border-bottom: 1px solid white; border-right: 1px solid white">
                                            By Ac&#39;nt :
                                        </td>
                                        <td align="left" style="width: 5%; border-right: 2px solid black; border-bottom: 1px solid white;">
                                            <asp:Label ID="lblLong_Acnt" runat="server" Text="Label" CssClass="FieldDottedLine"
                                                Width="50px"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 5%; border-bottom: 1px solid white; border-right: 1px solid white">
                                            This Day :
                                        </td>
                                        <td align="left" style="width: 5%; border-right: 2px solid black; border-bottom: 1px solid white">
                                            <asp:Label ID="lblSteeringDay" runat="server" Text="Label" CssClass="FieldDottedLine"
                                                Width="50px"></asp:Label>
                                        </td>
                                        <td align="right" style="border-bottom: 1px solid white; border-right: 1px solid white;">
                                            Average :
                                        </td>
                                        <td align="left" style="border-right: 2px solid black; border-bottom: 1px solid white">
                                            <asp:Label ID="lblSpeedAvg" runat="server" Text="Label" CssClass="FieldDottedLine"
                                                Width="50px"></asp:Label>
                                        </td>
                                        <td align="right" style="border-bottom: 1px solid white; border-right: 1px solid white;">
                                            Error per cent :
                                        </td>
                                        <td align="left" style="border-bottom: 1px solid white">
                                            <asp:Label ID="lblErrPercent" runat="server" Text="Label" CssClass="FieldDottedLine"
                                                Width="100px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center" style="border: 1px solid black;">
                                        <td style="border-right: 2px solid black; padding: 4px">
                                            <asp:Label ID="lbltrueCourseNoon" runat="server" Text="Label" CssClass="FieldDottedLine"
                                                Width="50px"></asp:Label>
                                        </td>
                                        <td align="right" style="border-right: 1px solid white;">
                                            Total :
                                        </td>
                                        <td align="left" style="border-right: 2px solid black;">
                                            <asp:Label ID="lblTotal" runat="server" Text="Label" CssClass="FieldDottedLine" Width="50px"></asp:Label>
                                        </td>
                                        <td align="right" style="border-right: 1px solid white;">
                                            Obs :
                                        </td>
                                        <td align="left" style="border-right: 2px solid black;">
                                            <asp:Label ID="lbllati_Obs" runat="server" Text="Label" CssClass="FieldDottedLine"
                                                Width="50px"></asp:Label>
                                        </td>
                                        <td align="right" style="border-right: 1px solid white;">
                                            Obs :
                                        </td>
                                        <td align="left" style="border-right: 2px solid black;">
                                            <asp:Label ID="lblLong_Obs" runat="server" Text="Label" CssClass="FieldDottedLine"
                                                Width="50px"></asp:Label>
                                        </td>
                                        <td align="right" style="border-right: 1px solid white;">
                                            Total :
                                        </td>
                                        <td align="left" style="border-right: 2px solid black;">
                                            <asp:Label ID="lblSteeringDayToal" runat="server" Text="Label" CssClass="FieldDottedLine"
                                                Width="50px"></asp:Label>
                                        </td>
                                        <td align="right" style="border-right: 1px solid white;">
                                            Total Average :
                                        </td>
                                        <td align="left" style="border-right: 2px solid black;">
                                            <asp:Label ID="lblSpeedTotalAvg" runat="server" Text="Label" CssClass="FieldDottedLine"
                                                Width="50px"></asp:Label>
                                        </td>
                                        <td align="right" style="border-right: 1px solid white;">
                                            Magnetic variation :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblMagneticVariation" runat="server" Text="Label" CssClass="FieldDottedLine"
                                                Width="100px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" valign="top" class="CellClass0">
                            <div style="padding-top: 0px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px;font-family: Tahoma; font-size: 12px;">
                                <asp:Repeater ID="rpDeckLogBook02" runat="server">
                                    <HeaderTemplate>
                                        <table cellspacing="1" cellpadding="1" rules="all" style="border: 1px solid black"
                                            width="100%">
                                            <tr align="center">
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Hours
                                                </td>
                                                <td colspan="4" class="HeaderCellColor">
                                                    COURSES
                                                </td>
                                                <td colspan="2" class="HeaderCellColor">
                                                    ERROR
                                                </td>
                                                <td colspan="2" class="HeaderCellColor">
                                                    WINDS
                                                </td>
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Sea
                                                </td>
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Sky
                                                </td>
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Visibility
                                                </td>
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Barometer
                                                </td>
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Air Dry
                                                </td>
                                                  <td rowspan="2" class="HeaderCellColor">
                                                    Air Wet
                                                </td>
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Sea Temp
                                                </td>
                                                <td rowspan="2" class="HeaderCellColor">
                                                    Log Reading
                                                </td>
                                                <td align="left" rowspan="2" class="HeaderCellColor">
                                                    REMARKS, Etc. (Note Carefully When Boats Are Exercised)
                                                </td>
                                                 <td align="left" rowspan="2" class="HeaderCellColor" width="200px">
                                                    Last Updated
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td class="HeaderCellColor">
                                                    True
                                                </td>
                                                <td class="HeaderCellColor">
                                                    Gyro
                                                </td>
                                                <td class="HeaderCellColor">
                                                    Standard
                                                </td>
                                                <td class="HeaderCellColor">
                                                    Steering
                                                </td>
                                                <td class="HeaderCellColor">
                                                    Gyro
                                                </td>
                                                <td class="HeaderCellColor">
                                                    Standard
                                                </td>
                                                <td class="HeaderCellColor">
                                                    Direction
                                                </td>
                                                <td class="HeaderCellColor">
                                                    Force
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr align="center">
                                            <td align="left" style="height: 16px;" class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Log_Hours_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Log_Hours")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_True_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_True")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_Gyro_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_Gyro&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_Gyro")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_Standard_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_Steering&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_Standard")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_Steering_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_Steering&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_Steering")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Error_Gyro_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Error_Gyro&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Error_Gyro_Color").ToString() + "  CellClassChangeFlage" + DataBinder.Eval(Container.DataItem,"Error_Gyro_Change_Flag").ToString() %>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Error_Gyro")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Error_Standard_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Error_Standard_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Error_Standard")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Winds_Direction_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Winds_Direction_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Winds_Direction")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Winds_Force_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Winds_Force_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Winds_Force")%>
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Sea_Color").ToString()%>' <%# DataBinder.Eval(Container.DataItem,"Sea_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Sea")%>
                                            </td>
                                            <td style="height: 19px;" class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Sky_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Sky")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Visibility_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Visibility_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Visibility")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Barometer_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Barometer_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Barometer")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"AirTemp_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"AirTemp_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "AirTemp")%>
                                            </td>

                                            <td style="height: 19px;">
                                                <%# DataBinder.Eval(Container.DataItem, "AirTemp_Wet")%>
                                            </td>

                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"SeaTemp_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"SeaTemp_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "SeaTemp")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"LogReading_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class="CellClass0">
                                                <%# DataBinder.Eval(Container.DataItem, "LogReading")%>
                                            </td>
                                            <td align="left" style="height: 19px;" class="CellClass0">
                                                <%# DataBinder.Eval(Container.DataItem, "Remark")%>
                                            </td>
                                              <td align="left" style="height: 19px;" class="CellClass0"  style="white-space:nowrap">
                                                <%# DataBinder.Eval(Container.DataItem, "LastUp")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr style="background-color: #efefcf; border: 1px solid black;" align="center">
                                            <td align="left" style="height: 16px;" class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Log_Hours_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Log_Hours")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_True_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_True")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_Gyro_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_Gyro&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_Gyro")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_Standard_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_Steering&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_Standard")%>
                                            </td>
                                            <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Course_Steering_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_Steering&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Course_Steering")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Error_Gyro_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Error_Gyro&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Error_Gyro_Color").ToString() + "  CellClassChangeFlage" + DataBinder.Eval(Container.DataItem,"Error_Gyro_Change_Flag").ToString() %>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Error_Gyro")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Error_Standard_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Error_Standard_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Error_Standard")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Winds_Direction_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Winds_Direction_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Winds_Direction")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Winds_Force_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Winds_Force_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Winds_Force")%>
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Sea_Color").ToString()%>' <%# DataBinder.Eval(Container.DataItem,"Sea_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Sea")%>
                                            </td>
                                            <td style="height: 19px;" class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Sky_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>>
                                                <%# DataBinder.Eval(Container.DataItem, "Sky")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Visibility_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Visibility_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Visibility")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"Barometer_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Barometer_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "Barometer")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"AirTemp_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"AirTemp_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "AirTemp")%>
                                            </td>
                                            <td style="height: 19px;">
                                                <%# DataBinder.Eval(Container.DataItem, "AirTemp_Wet")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"SeaTemp_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"SeaTemp_Color").ToString()%>'>
                                                <%# DataBinder.Eval(Container.DataItem, "SeaTemp")%>
                                            </td>
                                            <td style="height: 19px;" <%# DataBinder.Eval(Container.DataItem,"LogReading_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_DeckLog_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_Hours_ID").ToString()+"&#39;,&#39;Course_True&#39;,event,this)":"" %>
                                                class="CellClass0">
                                                <%# DataBinder.Eval(Container.DataItem, "LogReading")%>
                                            </td>
                                            <td align="left" style="height: 19px;" class="CellClass0">
                                                <%# DataBinder.Eval(Container.DataItem, "Remark")%>
                                            </td>
                                               <td align="left" style="height: 19px;" class="CellClass0"  style="white-space:nowrap">
                                                <%# DataBinder.Eval(Container.DataItem, "LastUp")%>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" valign="top" class="CellClass0">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td valign="top" style="width: 25%">
                                        <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 0px;font-family: Tahoma; font-size: 12px;">
                                            <asp:Repeater ID="rpWheelLookOut" runat="server">
                                                <HeaderTemplate>
                                                    <table cellspacing="1" cellpadding="1" rules="all" style="border: 1px solid black"
                                                        width="100%">
                                                        <tr>
                                                            <td align="center" colspan="6" class="HeaderCellColor">
                                                                WHEEL AND LOOK-OUT
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="HeaderCellColor">
                                                                Watch
                                                            </td>
                                                            <td align="center" colspan="2" class="HeaderCellColor">
                                                                Wheel
                                                            </td>
                                                            <td align="center" colspan="3" class="HeaderCellColor">
                                                                Look-out
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr align="center">
                                                        <td align="left" style="height: 19px; width: 10%;" class="CellClass0">
                                                            <%# DataBinder.Eval(Container.DataItem, "Log_WATCH")%>
                                                        </td>
                                                        <td class="CellClass0 CellClassChangeFlage<%#DataBinder.Eval(Container.DataItem,"Wheel_Col_1_Change_Flag").ToString() %>"
                                                            <%# DataBinder.Eval(Container.DataItem,"Wheel_Col_1_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WheelLookOut_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_WATCH_ID").ToString()+"&#39;,&#39;Wheel_Col_1&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Wheel_Col_1")  %>
                                                        </td>
                                                        <td class="CellClass0 CellClassChangeFlage<%#DataBinder.Eval(Container.DataItem,"Wheel_Col_2_Change_Flag").ToString() %>"
                                                            <%# DataBinder.Eval(Container.DataItem,"Wheel_Col_2_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WheelLookOut_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_WATCH_ID").ToString()+"&#39;,&#39;Wheel_Col_2&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Wheel_Col_2")%>
                                                        </td>
                                                        <td class="CellClass0 CellClassChangeFlage<%#DataBinder.Eval(Container.DataItem,"Look_Out_Col_1_Change_Flag").ToString() %>"
                                                            <%# DataBinder.Eval(Container.DataItem,"Look_Out_Col_1_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WheelLookOut_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_WATCH_ID").ToString()+"&#39;,&#39;Look_Out_Col_1&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Look_Out_Col_1")%>
                                                        </td>
                                                        <td class="CellClass0 CellClassChangeFlage<%#DataBinder.Eval(Container.DataItem,"Look_Out_Col_2_Change_Flag").ToString() %>"
                                                            <%# DataBinder.Eval(Container.DataItem,"Look_Out_Col_2_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WheelLookOut_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_WATCH_ID").ToString()+"&#39;,&#39;Look_Out_Col_2&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Look_Out_Col_2")%>
                                                        </td>
                                                        <td class="CellClass0 CellClassChangeFlage<%#DataBinder.Eval(Container.DataItem,"Look_Out_Col_3_Change_Flag").ToString() %>"
                                                            <%# DataBinder.Eval(Container.DataItem,"Look_Out_Col_3_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WheelLookOut_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_WATCH_ID").ToString()+"&#39;,&#39;Look_Out_Col_3&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Look_Out_Col_3")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr style="background-color: #efefcf; border: 1px solid black;" align="center">
                                                        <td align="left" style="height: 19px; width: 10%;" class="CellClass0">
                                                            <%# DataBinder.Eval(Container.DataItem, "Log_WATCH")%>
                                                        </td>
                                                        <td class="CellClass0 CellClassChangeFlage<%#DataBinder.Eval(Container.DataItem,"Wheel_Col_1_Change_Flag").ToString() %>"
                                                            <%# DataBinder.Eval(Container.DataItem,"Wheel_Col_1_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WheelLookOut_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_WATCH_ID").ToString()+"&#39;,&#39;Wheel_Col_1&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Wheel_Col_1")  %>
                                                        </td>
                                                        <td class="CellClass0 CellClassChangeFlage<%#DataBinder.Eval(Container.DataItem,"Wheel_Col_2_Change_Flag").ToString() %>"
                                                            <%# DataBinder.Eval(Container.DataItem,"Wheel_Col_2_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WheelLookOut_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_WATCH_ID").ToString()+"&#39;,&#39;Wheel_Col_2&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Wheel_Col_2")%>
                                                        </td>
                                                        <td class="CellClass0 CellClassChangeFlage<%#DataBinder.Eval(Container.DataItem,"Look_Out_Col_1_Change_Flag").ToString() %>"
                                                            <%# DataBinder.Eval(Container.DataItem,"Look_Out_Col_1_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WheelLookOut_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_WATCH_ID").ToString()+"&#39;,&#39;Look_Out_Col_1&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Look_Out_Col_1")%>
                                                        </td>
                                                        <td class="CellClass0 CellClassChangeFlage<%#DataBinder.Eval(Container.DataItem,"Look_Out_Col_2_Change_Flag").ToString() %>"
                                                            <%# DataBinder.Eval(Container.DataItem,"Look_Out_Col_2_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WheelLookOut_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_WATCH_ID").ToString()+"&#39;,&#39;Look_Out_Col_2&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Look_Out_Col_2")%>
                                                        </td>
                                                        <td class="CellClass0 CellClassChangeFlage<%#DataBinder.Eval(Container.DataItem,"Look_Out_Col_3_Change_Flag").ToString() %>"
                                                            <%# DataBinder.Eval(Container.DataItem,"Look_Out_Col_3_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WheelLookOut_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Log_WATCH_ID").ToString()+"&#39;,&#39;Look_Out_Col_3&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Look_Out_Col_3")%>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </td>
                                    <td valign="top" style="width: 25%">
                                        <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 0px; padding-right: 0px;font-family: Tahoma; font-size: 12px;">
                                            <asp:Repeater ID="rpWaterInHold" runat="server">
                                                <HeaderTemplate>
                                                    <table cellspacing="1" cellpadding="1" rules="all" style="border: 1px solid black"
                                                        width="100%">
                                                        <tr>
                                                            <td align="center" colspan="4" class="HeaderCellColor">
                                                                WATER IN HOLD
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="HeaderCellColor">
                                                            </td>
                                                            <td align="center" class="HeaderCellColor">
                                                                Sounding(cms)
                                                            </td>
                                                            <td align="center" class="HeaderCellColor">
                                                                Capacity(CuM)
                                                            </td>
                                                            <td align="center" class="HeaderCellColor">
                                                                Capacity100(CuM)
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr align="center">
                                                        <td align="left" style="height: 19px; width: 20%;" class="CellClass0">
                                                            <%# DataBinder.Eval(Container.DataItem, "Hold_Tank_Name")%>
                                                        </td>
                                                        <td class='CellClass0'
                                                            <%# DataBinder.Eval(Container.DataItem,"Water_In_Hold_Sounding_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WaterInHold_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Hold_Tank_ID").ToString()+"&#39;,&#39;Water_In_Hold_Sounding&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Water_In_Hold_Sounding")%>
                                                        </td>
                                                           <td class="CellClass0" + <%#  "CellClassChangeFlage" + DataBinder.Eval(Container.DataItem,"Water_In_Hold_Capacity_Change_Flag").ToString()%>  <%# DataBinder.Eval(Container.DataItem,"Water_In_Hold_Capacity_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WaterInHold_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Hold_Tank_ID").ToString()+"&#39;,&#39;Water_In_Hold_Capacity&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Water_In_Hold_Capacity")%>
                                                        </td>   
                                                         <td class="CellClass0">
                                                            <%# DataBinder.Eval(Container.DataItem, "Water_In_Hold_Capacity100")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr style="background-color: #efefcf; border: 1px solid black;" align="center">
                                                        <td align="left" style="height: 19px; width: 20%;" class="CellClass0">
                                                            <%# DataBinder.Eval(Container.DataItem, "Hold_Tank_Name")%>
                                                        </td>
                                                        <td class='CellClass0'
                                                            <%# DataBinder.Eval(Container.DataItem,"Water_In_Hold_Sounding_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WaterInHold_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Hold_Tank_ID").ToString()+"&#39;,&#39;Water_In_Hold_Sounding&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Water_In_Hold_Sounding")%>
                                                        </td>
                                                            <td class="CellClass0" + <%#  "CellClassChangeFlage" + DataBinder.Eval(Container.DataItem,"Water_In_Hold_Capacity_Change_Flag").ToString()%>  <%# DataBinder.Eval(Container.DataItem,"Water_In_Hold_Capacity_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WaterInHold_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Hold_Tank_ID").ToString()+"&#39;,&#39;Water_In_Hold_Capacity&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Water_In_Hold_Capacity")%>
                                                        </td>  
                                                          <td class="CellClass0">
                                                            <%# DataBinder.Eval(Container.DataItem, "Water_In_Hold_Capacity100")%>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </td>
                                    <td valign="top" style="width: 25%">
                                        <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 0px; padding-right: 0px; font-family: Tahoma; font-size: 12px;">
                                            <asp:Repeater ID="rpWaterInTank" runat="server">
                                                <HeaderTemplate>
                                                    <table cellspacing="1" cellpadding="1" rules="all" style="border: 1px solid black"
                                                        width="100%">
                                                        <tr>
                                                            <td align="center" colspan="4" class="HeaderCellColor">
                                                                WATER IN TANK
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="HeaderCellColor">
                                                            </td>
                                                            <td align="center" class="HeaderCellColor">
                                                                Sounding(cms)
                                                            </td>
                                                            <td align="center" class="HeaderCellColor">
                                                                Capacity(CuM)
                                                            </td>
                                                            <td align="center" class="HeaderCellColor">
                                                                Capacity100(CuM)
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr align="center">
                                                        <td align="left" style="height: 19px; width: 20%;" class="CellClass0">
                                                            <%# DataBinder.Eval(Container.DataItem, "Hold_Tank_Name")%>
                                                        </td>
                                                        <td class='CellClass0'
                                                            <%# DataBinder.Eval(Container.DataItem,"Water_In_Tank_Sounding_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WaterInTank_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Hold_Tank_ID").ToString()+"&#39;,&#39;Water_In_Tank_Sounding&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Water_In_Tank_Sounding")%>
                                                        </td>
                                                        <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Water_In_Tank_Capacity_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WaterInTank_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Hold_Tank_ID").ToString()+"&#39;,&#39;Water_In_Tank_Capacity&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Water_In_Tank_Capacity")%>
                                                        </td>
                                                         <td class="CellClass0">
                                                            <%# DataBinder.Eval(Container.DataItem, "Water_In_Tank_Capacity100")%>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr style="background-color: #efefcf; border: 1px solid black;" align="center">
                                                        <td align="left" style="height: 19px; width: 20%;" class="CellClass0">
                                                            <%# DataBinder.Eval(Container.DataItem, "Hold_Tank_Name")%>
                                                        </td>
                                                        <td class='CellClass0'
                                                            <%# DataBinder.Eval(Container.DataItem,"Water_In_Tank_Sounding_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WaterInTank_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Hold_Tank_ID").ToString()+"&#39;,&#39;Water_In_Tank_Sounding&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Water_In_Tank_Sounding")%>
                                                        </td>
                                                        <td class="CellClass0" <%# DataBinder.Eval(Container.DataItem,"Water_In_Tank_Capacity_Change_Flag").ToString()=="1"?"onmouseover=ASync_Get_WaterInTank_Value_Chages(&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Hold_Tank_ID").ToString()+"&#39;,&#39;Water_In_Tank_Capacity&#39;,event,this)":"" %>>
                                                            <%# DataBinder.Eval(Container.DataItem, "Water_In_Tank_Capacity")%>
                                                        </td>
                                                         <td class="CellClass0">
                                                            <%# DataBinder.Eval(Container.DataItem, "Water_In_Tank_Capacity100")%>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </td>
                                    <td valign="top" style="width: 25%">
                                        <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px; font-family: Tahoma; font-size: 12px;
                                            height: 150px">
                                            <table cellpadding="0" cellspacing="1" width="100%">
                                                <tr>
                                                    <td colspan="2" align="center" style="border: 1px solid black;" class="HeaderCellColor">
                                                        REGULATION LIGHTS EXHIBITED
                                                    </td>
                                                </tr>
                                                <tr style="height: 15px">
                                                    <td colspan="2" style="border-left: 1px solid black; border-right: 1px solid black">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-left: 1px solid black">
                                                        From midnight till
                                                    </td>
                                                    <td style="border-right: 1px solid black">
                                                        <asp:Label ID="lblFromMidnight" CssClass="FieldDottedLine" Text="0600 Hrs." runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="height: 15px">
                                                    <td colspan="2" style="border-left: 1px solid black; border-right: 1px solid black">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-left: 1px solid black">
                                                        <asp:Label ID="lblTillMidnight" CssClass="FieldDottedLine" Text="1900 Hrs." runat="server"></asp:Label>
                                                    </td>
                                                    <td style="border-right: 1px solid black">
                                                        till midnight.
                                                    </td>
                                                </tr>
                                                <tr style="height: 15px">
                                                    <td colspan="2" align="center" style="border-left: 1px solid black; border-right: 1px solid black;"
                                                        class="HeaderCellColor">
                                                        Sick List
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                       <%-- <asp:TextBox ID="txtSickList" runat="server" Height="80px" Width="445px" ReadOnly="true"
                                                            Visible="true" TextMode="MultiLine"></asp:TextBox>--%>
                                                            <asp:Label ID="txtSickList" runat="server" Text="Label"  Width="445px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                    <td align="left" valign="top" colspan="2" style="border: 1px solid #cccccc;">
                            <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 0px; padding-right: 0px;
                                 height:auto;">
                                <table cellspacing="1" cellpadding="1" rules="all" style="border: 1px solid #cccccc"
                                    width="100%">
                                    <tr>
                                        <td align="center" colspan="6" style="width: 30%; border: 1px solid #cccccc;">
                                            Additional remarks for the day :
                                        </td>
                                        <td style="width: 70%;height:100%; white-space:normal;border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; background-color: #efefcf">
                                            <asp:TextBox ID="txtAdditionalRemark" runat="server" Width="100%" ReadOnly="true"
                                                Style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; background-color: #efefcf"
                                                Visible="true" TextMode="MultiLine" CssClass="txtBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" colspan="2" style="border: 1px solid #cccccc;">
                            <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 0px; padding-right: 0px;
                                overflow-x: hidden; overflow-y: scroll; height: 100px">
                                <asp:Repeater ID="rpIncidentReport" runat="server">
                                    <HeaderTemplate>
                                        <table cellspacing="1" cellpadding="1" rules="all" style="border: 1px solid black"
                                            width="100%">
                                            <tr>
                                                <td align="center" colspan="6" class="HeaderCellColor">
                                                    Incident
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" class="HeaderCellColor">
                                                    Date
                                                </td>
                                                <td align="center" class="HeaderCellColor">
                                                    Type
                                                </td>
                                                <td align="center" class="HeaderCellColor">
                                                    Details
                                                </td>
                                                <td align="center" class="HeaderCellColor">
                                                    Action Taken
                                                </td>
                                                <td align="center" class="HeaderCellColor">
                                                    Participant
                                                </td>
                                                <td align="center" class="HeaderCellColor">
                                                    Att.
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr align="center">
                                            <td align="left" style="height: 19px; width: 15%;" class="CellClass0">
                                                <%# DataBinder.Eval(Container.DataItem, "INCIDENT_DATE")%>
                                            </td>
                                            <td align="left" style="height: 19px; width: 15%;" class="CellClass0">
                                                <%# DataBinder.Eval(Container.DataItem, "INCIDENT_TYPE")%>
                                            </td>
                                            <td align="left" style="height: 19px; width: 15%;" class="CellClass0">
                                                <asp:Label ID="lblDtlIncident" Text='<%#Convert.ToString(Eval("DETAILS_OF_INCIDENT")).Length>25?Convert.ToString(Eval("DETAILS_OF_INCIDENT")).Substring(0,25)+"..." : Convert.ToString(Eval("DETAILS_OF_INCIDENT")) %>'
                                                    onmousemove='<%# "js_ShowToolTip(&#39;"+ Convert.ToString(Eval("DETAILS_OF_INCIDENT")) +"&#39;,event,this)" %>'
                                                    runat="server"></asp:Label>
                                            </td>
                                            <td align="left" style="height: 19px; width: 15%;" class="CellClass0">
                                                <asp:Label ID="lblDtlActionTaken" Text='<%#Convert.ToString(Eval("ACTION_TAKEN")).Length>25?Convert.ToString(Eval("ACTION_TAKEN")).Substring(0,25)+"..." : Convert.ToString(Eval("ACTION_TAKEN")) %>'
                                                    onmousemove='<%# "js_ShowToolTip(&#39;"+ Convert.ToString(Eval("DETAILS_OF_INCIDENT")) +"&#39;,event,this)" %>'
                                                    runat="server"></asp:Label>
                                            </td>
                                            <td align="center" style="height: 19px; width: 15%;" class="CellClass0">
                                                <asp:Image ID="imgParticipant" ImageUrl="~/Images/Users.gif" onclick='<%#"OpenIncidentParticipantPage("+  DataBinder.Eval(Container.DataItem, "ID") + "," + DataBinder.Eval(Container.DataItem, "Vessel_ID") +");"%>'
                                                    Visible='<%#Convert.ToString(Eval("ParticipantFlag"))=="1"?true : false %>' runat="server" />
                                            </td>
                                            <td align="center" style="height: 19px; width: 15%;" class="CellClass0">
                                                <asp:Image ID="imgAtt" ImageUrl="~/Images/attach-icon.png" onclick='<%#"OpenIncidentParticipantAttachmentPage("+  DataBinder.Eval(Container.DataItem, "ID") + "," + DataBinder.Eval(Container.DataItem, "Vessel_ID") +");"%>'
                                                    Visible='<%#Convert.ToString(Eval("AttFlag"))=="1"?true : false %>' runat="server" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr style="background-color: #efefcf; border: 1px solid black;" align="center">
                                            <td align="left" style="height: 19px; width: 15%;" class="CellClass0">
                                                <%# DataBinder.Eval(Container.DataItem, "INCIDENT_DATE")%>
                                            </td>
                                            <td align="left" style="height: 19px; width: 15%;" class="CellClass0">
                                                <%# DataBinder.Eval(Container.DataItem, "INCIDENT_TYPE")%>
                                            </td>
                                            <td align="left" style="height: 19px; width: 15%;" class="CellClass0">
                                                <asp:Label ID="lblDtlIncident" Text='<%#Convert.ToString(Eval("DETAILS_OF_INCIDENT")).Length>25?Convert.ToString(Eval("DETAILS_OF_INCIDENT")).Substring(0,25)+"..." : Convert.ToString(Eval("DETAILS_OF_INCIDENT")) %>'
                                                    onmousemove='<%# "js_ShowToolTip(&#39;"+ Convert.ToString(Eval("DETAILS_OF_INCIDENT")) +"&#39;,event,this)" %>'
                                                    runat="server"></asp:Label>
                                            </td>
                                            <td align="left" style="height: 19px; width: 15%;" class="CellClass0">
                                                <asp:Label ID="lblDtlActionTaken" Text='<%#Convert.ToString(Eval("ACTION_TAKEN")).Length>25?Convert.ToString(Eval("ACTION_TAKEN")).Substring(0,25)+"..." : Convert.ToString(Eval("ACTION_TAKEN")) %>'
                                                    onmousemove='<%# "js_ShowToolTip(&#39;"+ Convert.ToString(Eval("DETAILS_OF_INCIDENT")) +"&#39;,event,this)" %>'
                                                    runat="server"></asp:Label>
                                            </td>
                                            <td align="center" style="height: 19px; width: 15%;" class="CellClass0">
                                                <asp:Image ID="imgParticipant" ImageUrl="~/Images/Users.gif" onclick='<%#"OpenIncidentParticipantPage("+  DataBinder.Eval(Container.DataItem, "ID") + "," + DataBinder.Eval(Container.DataItem, "Vessel_ID") +");"%>'
                                                    Visible='<%#Convert.ToString(Eval("ParticipantFlag"))=="1"?true : false %>' runat="server" />
                                            </td>
                                            <td align="center" style="height: 19px; width: 15%;" class="CellClass0">
                                                <asp:Image ID="imgAtt" ImageUrl="~/Images/attach-icon.png" onclick='<%#"OpenIncidentParticipantAttachmentPage("+  DataBinder.Eval(Container.DataItem, "ID") + "," + DataBinder.Eval(Container.DataItem, "Vessel_ID") +");"%>'
                                                    Visible='<%#Convert.ToString(Eval("AttFlag"))=="1"?true : false %>' runat="server" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px;
                                background: url(../../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                                background-color: #F6CEE3; font-family: Tahoma; font-size: 12px;">
                                <table cellspacing="0" cellpadding="1" rules="all" style="background-color: White;"
                                    width="100%">
                                    <tr>
                                       <%-- <td align="right" style="width: 5%; border-left: 1px solid white;">
                                            Master :&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 2%; border-left: 1px solid white;">
                                            <asp:Image ID="imgMaster" runat="server" Height="30px" CssClass="transactLog" />
                                        </td>
                                        <td align="left" style="width: 10%; border-left: 1px solid white;">
                                            <asp:HyperLink ID="lnkMaster" CssClass="FieldDottedLine link" runat="server" ForeColor="Blue"></asp:HyperLink>
                                        </td>
                                        
                                        <td align="right" style="width: 5%; border-left: 1px solid white;">
                                            Chief officer :&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 2%; border-left: 1px solid white;">
                                            <asp:Image ID="imgChiefOfficer" runat="server" Height="30px" CssClass="transactLog" />
                                        </td>
                                        <td align="left" style="width: 10%; border-left: 1px solid white;">
                                            <asp:HyperLink ID="lnkChiefOfficer" CssClass="FieldDottedLine link" runat="server"
                                                ForeColor="Blue"></asp:HyperLink>
                                        </td>--%>
                                      <%--  <td align="right" style="width: 5%; border-left: 1px solid white;">
                                           Created by :&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 2%; border-left: 1px solid white;">
                                            <asp:Image ID="imgCreated" runat="server" Height="30px" CssClass="transactLog" />
                                        </td>
                                        <td align="left" style="width: 10%; border-left: 1px solid white;">
                                            <asp:HyperLink ID="lnkCreated" CssClass="FieldDottedLine link" runat="server"
                                                ForeColor="Blue"></asp:HyperLink>
                                        </td>
                                        <td style="width: 2%; border-left: 1px solid white;">
                                           &nbsp; &nbsp; &nbsp; &nbsp;
                                        </td>
                                        <td align="right" style="width: 5%; border-left: 1px solid white;">
                                           Modified by :&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 2%; border-left: 1px solid white;">
                                            <asp:Image ID="imgModif" runat="server" Height="30px" CssClass="transactLog"  />
                                        </td>
                                        <td align="left" style="width: 10%; border-left: 1px solid white;">
                                            <asp:HyperLink ID="lnkModif" CssClass="FieldDottedLine link" runat="server"
                                                ForeColor="Blue"></asp:HyperLink>
                                        </td>--%>

                                          <td style="width:50%">
                                                               <div id="dvDeckRecordInformation" style="text-align: left; width: 100%;">
                                        
                                                                </div>
                                                        </td>
                                                     
                                                        <td style="width:50%">
                                                        <div id="dvRecordInformation" style="text-align: left; width: 100%;">
                                        
                                                        </div>
                                        
                                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hdnBaseURL" runat="server"></asp:HiddenField>
              <asp:HiddenField ID="hdnContent" runat="server" />
           
        </div>
    </center>
</asp:Content>
