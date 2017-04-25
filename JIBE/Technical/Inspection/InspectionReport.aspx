<%@ Page Title="Inspection Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="InspectionReport.aspx.cs" Inherits="Technical_Worklist_InspectionReport"
    EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register TagPrefix="eo" Namespace="EO.Web" Assembly="EO.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.alerts.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="http://www.google.com/jsapi" type="text/javascript"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi?autoload={'modules':[{'name':'visualization','version':'1.1','packages':['bar']}]}"></script>
    <link href="../../Styles/jquery.alerts.css" rel="stylesheet" />
    <script type="text/javascript">

  

     google.load('visualization', '1.0', { 'packages': ['corechart'] });

        
     function HtmlPrint(divID) 
     {
          document.getElementById($('[id$=txtCompanyName]').attr('id')).style.backgroundColor="white";
          document.getElementById($('[id$=txtReportNo]').attr('id')).style.backgroundColor="white";

          PrintDiv(divID);

           document.getElementById($('[id$=txtCompanyName]').attr('id')).style.backgroundColor="#FFFF99";
          document.getElementById($('[id$=txtReportNo]').attr('id')).style.backgroundColor="#FFFF99";
     }
    function GetExecutiveSummary() {

        try {
            $('#dvExeSummary').load("ExecutiveSummary.aspx?InspID=" + <%= Request.QueryString["InspID"].ToString() %> + "&rnd=" + Math.random() + ' #dvExeSummary',fnExeSummaryLoadComplete);
            //checkForMyAction('lblWebPartEvaluationBelow60', retval);
        }
        catch (ex) { }

    }

    function fnExeSummaryLoadComplete()
    {
        ReplaceExeSummaryText();
    }
        function ReplaceExeSummaryText()
    {
             
            
            var txt=document.getElementsByClassName("rptSummary");
             
            for(var j=0;j<txt.length;j++)
            {
                 
                $(txt[j]).replaceWith(  $(txt[j]).val() );
                  
                txt=document.getElementsByClassName("rptSummary");
                   
                if (txt.length == 0) {
                    break;
                }
                else {
                    j = -1;
                }

            }

        return false;
            
    }  
    function GetCategoryRating() {

        try {
                $('#dvCategoryRating').load("CategoryRating.aspx?InspID=" + <%= Request.QueryString["InspID"].ToString() %> +"&ScheduleID="+  <%=Request.QueryString["ScheduleID"].ToString()  %> +"&VesselID"+  <%=Request.QueryString["VesselID"].ToString()  %> +"&ReportType=" + <%= Request.QueryString["ReportType"].ToString() %> +"&rnd=" + Math.random() + ' #dvCategory');
               
        }
        catch (ex) { }

    }
      
        
     


        function GetSubCategoryRating(SystemCode) {

        try {

                var RptType=<%= Request.QueryString["ReportType"] %>;
            if(RptType=="O")
            {
                 
                    var div = document.createElement("div");
                    var br = document.createElement("br");
                    div.id="dvSubCatRating"+SystemCode;

                    div.style.pageBreakInside="avoid";
                    //div.style.height = "100%";
                    var container = document.getElementById("dvSubCatRating");
                    container.appendChild(div);
                    container.appendChild(br);
             
         
                $('#dvSubCatRating'+SystemCode).load("CategoryRating.aspx?InspID=" + <%= Request.QueryString["InspID"].ToString() %>+"&ScheduleID="+  <%=Request.QueryString["ScheduleID"].ToString()  %> + "&SystemCode=" +SystemCode +"&VesselID="+ <%=Request.QueryString["VesselID"].ToString()  %>+"&rnd=" + Math.random()  +  ' #dvSubCategoryInner', fnSubCategoryLoadComplete);
                   
            }
            else
            {
                $('#dvSCR').hide();  
            }
              
            
        }
        catch (ex) { }

    }
    function ReplaceTextRemark()
    {
           
            var txt1=document.getElementsByClassName("MultiLineTextbox");
            for(var i=0;i<txt1.length;i++)
            {
                $(txt1[i]).replaceWith(  $(txt1[i]).val() );

                txt1=document.getElementsByClassName("MultiLineTextbox");
                if (txt1.length == 0) {
                    break;
                }
                else {
                    i = -1;
                }
                 
            }    


        return false;
            
    }
    function fnSubCategoryLoadComplete()
    {
        ReplaceTextRemark();
        ReplaceDropDownWithLabel();
        ReplaceExeSummaryText();
    }
        function ReplaceDropDownWithLabel()
    {
                    var ddl = $('select');   
            for(var k=0; k<ddl.length;k++ ){
                    
                $(ddl[k]).replaceWith(  $(ddl[k]).children("option").filter(":selected").text().replace('--SELECT--','') );




            }

        return false;
            
    }
    function GetConditionReportOT3() {

        try {
                

                    $('#dvConditionReport').load("InspectionWorklistReport.aspx?InspID=" + <%= Request.QueryString["InspID"].ToString() %>+ "&ReportType=C"  +"&rnd=" + Math.random() + ' #dvWorklist');

 
        }
        catch (ex) { }

    }

      function GetDefectNonOT3() {

        try {
                

                    $('#dvDefect').load("InspectionWorklistReport.aspx?InspID=" + <%= Request.QueryString["InspID"].ToString() %>+ "&ReportType=O"  +"&rnd=" + Math.random() + ' #dvWorklist');

                
         
             
        }
        catch (ex) { }

    }

    function ShowModalPopup(divId)
    {
        showModal(divId)
        return false;
    }


    function drawChartSummaryRating(dataValues) {

        var RptType=<%= Convert.ToString(Request.QueryString["ReportType"]) %>;
        if(RptType=="O")
        {  
            __isResponse = 1;
     
            var data = new google.visualization.DataTable();
            data.addColumn('number', 'This Report');
            data.addColumn('number', 'Last Report');
            data.addColumn('string', 'Description');

         
            var LastReport,ThisReport;
            for (var i = 0; i < dataValues.length; i++) {
            if ($.trim(dataValues[i].LastReport)=="")
            {
                LastReport=0
            }
            else
            {
                LastReport=$.trim(dataValues[i].LastReport);
            }
                if ($.trim(dataValues[i].ThisReport)=="")
            {
                ThisReport=0
            }
            else
            {
                ThisReport=$.trim(dataValues[i].ThisReport);
            }
          
                    data.addRow([parseFloat(ThisReport),parseFloat(LastReport),dataValues[i].Description]);
            

            }
            
            var view = new google.visualization.DataView(data);
            view.setColumns([2, 1,{ calc: "stringify",
                                sourceColumn: 1,
                                type: "string",
                                role: "annotation"
                            }  , 0, 
                                { calc: "stringify",
                                sourceColumn: 0,
                                type: "string",
                                role: "annotation"
                            }  
                      
                                ]);

             var options = { 'title': 'Category Rating',
                hAxis: { textStyle: { fontName: 'Tahoma', fontSize: 9 },
                         slantedText: false, 
                         slantedTextAngle:90,
                         viewWindowMode:'explicit',
                         viewWindow:{max:10, min:0 },
                         gridlines: { count: 6 }
                        },
              
                annotations: { textStyle: { fontName: 'Tahoma',fontSize: 9 } },
               
                legend:{left:-50,position: 'bottom', textStyle: { fontName: 'Tahoma',fontSize: 9 }},
                chartArea: {left:200,top:30, width:"50%",height:"70%"},
                bar: {groupWidth: '80%'} ,
                vAxis: { title: "", format: "", textStyle: {fontName: 'Tahoma',fontSize: 9}
            }
          

            };

             var chart_div = document.getElementById('dvCategoryRatingChart');

            var chart = new google.visualization.BarChart(document.getElementById('dvCategoryRatingChart'));


             google.visualization.events.addListener(chart, 'ready', function () {
                chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
                console.log(chart_div.innerHTML);
              });

            chart.draw(view, options);

        }
        else if(RptType=="C")
        {
                                __isResponse = 1;
     
                var data = new google.visualization.DataTable();
                data.addColumn('number', 'This Report');
                data.addColumn('string', 'Description');
                var ThisReport;
                for (var i = 0; i < dataValues.length; i++) {
                
                    if ($.trim(dataValues[i].ThisReport)=="")
                {
                    ThisReport=0
                }
                else
                {
                    ThisReport=$.trim(dataValues[i].ThisReport);
                }
          
                        data.addRow([parseFloat(ThisReport),dataValues[i].Description]);
            

                }
                 
                var view = new google.visualization.DataView(data);
                view.setColumns([1, 0, 
                                    { calc: "stringify",
                                    sourceColumn: 0,
                                    type: "string",
                                    role: "annotation"
                                }  
                      
                                    ]);

                var options = { 'title': 'Category Rating',
                hAxis: { textStyle: { fontName: 'Tahoma', fontSize: 9 },
                         slantedText: false, 
                         slantedTextAngle:90,
                         viewWindowMode:'explicit',
                         viewWindow:{max:10, min:0 },
                         gridlines: { count: 6 }
                        },
              
                annotations: { textStyle: { fontName: 'Tahoma',fontSize: 9 } },
               
                legend:{position: 'bottom', textStyle: { fontName: 'Tahoma',fontSize: 9 }},
                chartArea: {left:200,top:30, width:"50%",height:"100%"},
                 bar: {groupWidth: '80%'} ,
                vAxis: { title: "", format: "", textStyle: {fontName: 'Tahoma',fontSize: 9}
            }
          

            };

             var chart_div = document.getElementById('dvCategoryRatingChart');

            var chart = new google.visualization.BarChart(document.getElementById('dvCategoryRatingChart'));


             google.visualization.events.addListener(chart, 'ready', function () {
                chart_div.innerHTML = '<img src="' + chart.getImageURI() + '">';
                console.log(chart_div.innerHTML);
              });

            chart.draw(view, options);
        }
    }

    function DisplayCompanyNameSave()
    {
        document.getElementById($('[id$=BtnUpdateCompany]').attr('id')).style.visibility="visible";
            document.getElementById($('[id$=BtnCancelUpdCompany]').attr('id')).style.visibility="visible";
    }
    function DisplayReportNOSave()
    {
        document.getElementById($('[id$=BtnUpdateReportNo]').attr('id')).style.visibility="visible";
        document.getElementById($('[id$=BtnCancelUpdRptNo]').attr('id')).style.visibility="visible";
    }

    function hideSaveComp()
    {
        document.getElementById($('[id$=BtnUpdateCompany]').attr('id')).style.visibility="hidden";
        document.getElementById($('[id$=BtnCancelUpdCompany]').attr('id')).style.visibility="hidden";
        return false;
    }
    function hideSaveRptNo()
    {
        document.getElementById($('[id$=BtnUpdateReportNo]').attr('id')).style.visibility="hidden";
        document.getElementById($('[id$=BtnCancelUpdRptNo]').attr('id')).style.visibility="hidden";
            return false;
    }

    
    function GetDivDataOT2() {

        document.getElementById($('[id$=lblHead]').attr('id')).textContent ="SHIP'S ATTENDANCE REPORT-OT02";
        document.getElementById($('[id$=txtCompanyName]').attr('id')).style.backgroundColor="#fff";
        document.getElementById($('[id$=txtReportNo]').attr('id')).style.backgroundColor="#fff";
        document.getElementById($('[id$=dvOT3]').attr('id')).style.display='none';
        document.getElementById($('[id$=dvNonOT3]').attr('id')).style.display='none';

        var str = document.getElementById('dvReport').innerHTML;
        document.getElementById($('[id$=hdnContent]').attr('id')).value = str;
        //        __dopostback("BtnPrintPDF", "onclick")
        __doPostBack("<%=BtnPrintPDFOT2.UniqueID %>", "onclick");
         document.getElementById($('[id$=lblHead]').attr('id')).textContent ="SHIP'S ATTENDANCE REPORT";
        document.getElementById($('[id$=txtCompanyName]').attr('id')).style.backgroundColor="#FFFF99";
        document.getElementById($('[id$=txtReportNo]').attr('id')).style.backgroundColor="#FFFF99";
        
    }
      function GetDivDataOT2C() {

         document.getElementById($('[id$=lblHead]').attr('id')).textContent ="SHIP'S ATTENDANCE REPORT-OT02 WITH WORKLIST-OT03";
        document.getElementById($('[id$=txtCompanyName]').attr('id')).style.backgroundColor="#fff";
        document.getElementById($('[id$=txtReportNo]').attr('id')).style.backgroundColor="#fff";
        document.getElementById($('[id$=dvOT3]').attr('id')).style.display='inline';
        document.getElementById($('[id$=dvNonOT3]').attr('id')).style.display='none';
        var str = document.getElementById('dvReport').innerHTML;
        document.getElementById($('[id$=hdnContent]').attr('id')).value = str;
        //        __dopostback("BtnPrintPDF", "onclick")
        __doPostBack("<%=BtnPrintPdfOT2C.UniqueID %>", "onclick");
         document.getElementById($('[id$=lblHead]').attr('id')).textContent ="SHIP'S ATTENDANCE REPORT";
        document.getElementById($('[id$=txtCompanyName]').attr('id')).style.backgroundColor="#FFFF99";
        document.getElementById($('[id$=txtReportNo]').attr('id')).style.backgroundColor="#FFFF99";
        
    }
      function GetDivDataOT3() {

        
        document.getElementById($('[id$=txtCompanyName]').attr('id')).style.backgroundColor="#fff";
        document.getElementById($('[id$=txtReportNo]').attr('id')).style.backgroundColor="#fff";
        document.getElementById($('[id$=dvOT3]').attr('id')).style.display='inline';
        document.getElementById($('[id$=dvOT3CompanyLogo]').attr('id')).style.display='inline';
        document.getElementById($('[id$=dvOT03Cover]').attr('id')).style.display='inline';

        var str = document.getElementById('dvOT3').innerHTML;
        document.getElementById($('[id$=hdnContent]').attr('id')).value = str;
        document.getElementById($('[id$=dvOT3CompanyLogo]').attr('id')).style.display='none';
        document.getElementById($('[id$=dvOT03Cover]').attr('id')).style.display='none';
      
        __doPostBack("<%=BtnPrintPdfOT3.UniqueID %>", "onclick");
        
        document.getElementById($('[id$=txtCompanyName]').attr('id')).style.backgroundColor="#FFFF99";
        document.getElementById($('[id$=txtReportNo]').attr('id')).style.backgroundColor="#FFFF99";
        
    }
      function GetDivDataNonOT3() {

  
        document.getElementById($('[id$=txtCompanyName]').attr('id')).style.backgroundColor="#fff";
        document.getElementById($('[id$=txtReportNo]').attr('id')).style.backgroundColor="#fff";
        document.getElementById($('[id$=dvNonOT3]').attr('id')).style.display='inline';
        document.getElementById($('[id$=dvNonOT3CompanyLogo]').attr('id')).style.display='inline';
         document.getElementById($('[id$=dvNonOT03Cover]').attr('id')).style.display='inline';

        var str = document.getElementById('dvNonOT3').innerHTML;
        document.getElementById($('[id$=hdnContent]').attr('id')).value = str;
        document.getElementById($('[id$=dvNonOT3CompanyLogo]').attr('id')).style.display='none';
        document.getElementById($('[id$=dvNonOT03Cover]').attr('id')).style.display='none';
       
        __doPostBack("<%=BtnPrintPdfNonOT3.UniqueID %>", "onclick");

        document.getElementById($('[id$=txtCompanyName]').attr('id')).style.backgroundColor="#FFFF99";
        document.getElementById($('[id$=txtReportNo]').attr('id')).style.backgroundColor="#FFFF99";
        
    }
    </script>
    <style type="text/css">
        .report
        {
            background-color: Yellow;
        }
        
        
        .rptSummary
        {
            border: 0px none White;
            color: Black;
            font-size: 12px;
            font-family: Tahoma;
        }
        .Rating-FooterStyle-css
        {
            /* background: url(../../Images/gridheaderbg-silver-image.png) left -0px repeat-x;*/
            color: #333333;
            background-color: #fff;
            font-weight: bold;
            font-size: 11px;
            border: 1px solid #000;
        }
        .Rating-FooterStyle-css td
        {
            border: 1px solid #000;
            border-collapse: collapse;
        }
        .Rating-HeaderStyle-css
        {
            /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #333333;
            font-size: 11px;
            padding: 5px;
            text-align: left;
            vertical-align: middle;
            background-color: #CCCCCC;
            border: 1px solid #000;
            border-collapse: collapse;
        }
        .Rating-HeaderStyle-css th
        {
            border: 1px solid #000;
            border-collapse: collapse;
        }
        .PaddingCellCss
        {
            white-space: normal;
            max-width: 175px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; white-space: nowrap;">
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
        <div id="page-header" class="page-title">
            <b>Inspection Report</b>
            <%--  <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick=" return ReplaceDropDownWithLabel();" />--%>
        </div>
        <br />
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <b style="float: right; margin-top: -12px; margin-right: 38px;">&nbsp;&nbsp; &nbsp;&nbsp;
                        <asp:Button ID="BtnPrintPDFOT2" runat="server" Height="25px" Text="OT02" Font-Bold="true"
                            Font-Size="12px" Font-Names="Tahoma" BackColor="#5CD6FF" BorderColor="White"
                            ForeColor="Black" BorderWidth="2px" OnClick="BtnPrintPDF_Click" OnClientClick="GetDivDataOT2()" />
                        &nbsp;
                        <asp:Button ID="BtnPrintPdfOT2C" runat="server" Height="25px" Text="OT02+OT03" Font-Bold="true"
                            Font-Size="12px" Font-Names="Tahoma" BackColor="#5CD6FF" BorderColor="White"
                            ForeColor="Black" BorderWidth="2px" OnClick="BtnPrintPDF_Click" OnClientClick="GetDivDataOT2C()" />
                        &nbsp;
                        <asp:Button ID="BtnPrintPdfOT3" runat="server" Height="25px" Text="OT03" Font-Bold="true"
                            Font-Size="12px" Font-Names="Tahoma" BackColor="#5CD6FF" BorderColor="White"
                            ForeColor="Black" BorderWidth="2px" OnClick="BtnPrintPDF_Click" OnClientClick="GetDivDataOT3()" />
                        &nbsp;
                        <asp:Button ID="BtnPrintPdfNonOT3" runat="server" Height="25px" Text="Non-OT03" Font-Bold="true"
                            Font-Size="12px" Font-Names="Tahoma" BackColor="#5CD6FF" BorderColor="White"
                            ForeColor="Black" BorderWidth="2px" OnClick="BtnPrintPDF_Click" OnClientClick="GetDivDataNonOT3()" />
                    </b><b style="float: right; margin-top: -12px;">
                        <asp:ImageButton ID="BtnPrint" runat="server" OnClientClick="return HtmlPrint('dvReport');"
                            ImageUrl="~/Images/Printer.png" Height="25px" ToolTip="Print"></asp:ImageButton>
                    </b>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="dvReport" style="width: 98%; margin-left: 10px;">
                    <style type="text/css">
                        .report
                        {
                            background-color: Yellow;
                        }
                        
                        
                        .rptSummary
                        {
                            border: 0px none White;
                            color: Gray;
                        }
                        .Rating-FooterStyle-css
                        {
                            /* background: url(../../Images/gridheaderbg-silver-image.png) left -0px repeat-x;*/
                            color: #333333;
                            background-color: #fff;
                            font-weight: bold;
                            font-size: 11px;
                            border: 1px solid #000;
                        }
                        .Rating-FooterStyle-css td
                        {
                            border: 1px solid #000;
                            border-collapse: collapse;
                        }
                        .Rating-HeaderStyle-css
                        {
                            /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
                            color: #333333;
                            font-size: 11px;
                            padding: 5px;
                            text-align: left;
                            vertical-align: middle;
                            background-color: #CCCCCC;
                            border: 1px solid #000;
                            border-collapse: collapse;
                        }
                        .Rating-HeaderStyle-css th
                        {
                            border: 1px solid #000;
                            border-collapse: collapse;
                        }
                        .PaddingCellCss
                        {
                            white-space: normal;
                            max-width: 175px;
                        }
                    </style>
                    <div id="dvReportCover" style="border: 1px solid black; width: 98%;">
                        <div id="dvCompanyLogo">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Image ID="imgLogo" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="dvReportHead" style="text-align: center; font-weight: bold; font-size: 16px;
                            text-transform: capitalize;">
                            <table width="100%">
                                <tr>
                                    <td style="width: 100%;" align="center">
                                        <asp:Label ID="lblHead" runat="server" Text=" SHIP'S ATTENDANCE REPORT " ForeColor="#333333"
                                            Font-Bold="true" Font-Size="16px" Font-Names="Tahoma"></asp:Label>
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="90%" style="margin-left: 30px;">
                            <%--                     <tr>
                                <td>
                                    <asp:TextBox ID="txtCompanyName" runat="server" BorderColor="#FF3399" Width="200px"
                                        Font-Bold="true" Font-Size="16px"></asp:TextBox>
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lblHeader" runat="server" Text=" SHIP'S ATTENDANCE REPORT "  Font-Bold="true" Font-Size="14px"></asp:Label>
                                   
                                       
                                                                  
                                    <hr />
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="width: 100%; vertical-align: left;" colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCompanyName" runat="server" Width="200px" Font-Bold="true" Font-Names="Tahoma"
                                                Font-Size="24px" onchange="DisplayCompanyNameSave()" BackColor="#FFFF99" BorderWidth="0px" ForeColor="#333333"></asp:TextBox>
                                            &nbsp;
                                            <asp:Button ID="BtnUpdateCompany" runat="server" Text="Save" OnClick="BtnUpdateCompany_Click" />
                                            &nbsp;
                                            <asp:Button ID="BtnCancelUpdCompany" runat="server" Text="Cancel" OnClientClick="hideSaveComp()"
                                                OnClick="BtnCancelUpdCompany_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Lalel1" runat="server" Font-Bold="true" ForeColor="#333333" Font-Names="Tahoma"
                                        Font-Size="12px" Text="Ship"></asp:Label>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="lblShipName" runat="server" Font-Bold="true" ForeColor="#333333" Font-Names="Tahoma"
                                        Font-Size="12px" Text=""></asp:Label>
                                    <%-- <asp:TextBox ID="txtShipName" runat="server" BorderColor="#FF3399" 
                                            Width="200px"  Font-Bold="true"></asp:TextBox>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="#333333" Font-Names="Tahoma"
                                        Font-Size="12px" Text="Report No"></asp:Label>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <%-- <asp:Label ID="lblReportNo" runat="server" Font-Bold="true" Text=""></asp:Label>--%>
                                            <asp:TextBox ID="txtReportNo" runat="server" BackColor="#FFFF99" Width="200px" Font-Bold="true"
                                                Font-Names="Tahoma" Font-Size="12px" onchange="DisplayReportNOSave()" BorderWidth="0px" ForeColor="#333333"></asp:TextBox>
                                            &nbsp;
                                            <asp:Button ID="BtnUpdateReportNo" runat="server" Text="Save" OnClick="BtnUpdateReportNo_Click" />
                                            &nbsp;
                                            <asp:Button ID="BtnCancelUpdRptNo" runat="server" Text="Cancel" OnClientClick="hideSaveRptNo()"
                                                OnClick="BtnCancelUpdRptNo_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="#333333"
                                        Font-Size="12px" Text="Attended By"></asp:Label>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="lblAttendendBy" runat="server" Font-Bold="true" Font-Names="Tahoma"
                                        ForeColor="#333333" Font-Size="12px" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="#333333"
                                        Font-Size="12px" Text="Rank"></asp:Label>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="lblRank" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="#333333"
                                        Font-Size="12px" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;">
                                    <asp:Label ID="Label6" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="#333333"
                                        Font-Size="12px" Text="Date of Visit"></asp:Label>
                                </td>
                                <td style="vertical-align: top;">
                                    :
                                </td>
                                <td>
                                    <table width="200px">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="#333333"
                                                    Font-Size="12px" Text="From"></asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFrom" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="#333333"
                                                    Font-Size="12px" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="#333333"
                                                    Font-Size="12px" Text="To"></asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTo" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="#333333"
                                                    Font-Size="12px" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="#333333"
                                                    Font-Size="12px" Text="Duration"></asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDur" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="#333333"
                                                    Font-Size="12px" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="#333333"
                                        Font-Size="12px" Text="Port/s"></asp:Label>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="lblPorts" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="#333333"
                                        Font-Size="12px" Text=""></asp:Label>
                                </td>
                            </tr>
                            <%-- <tr>
                                <td colspan="3" align="center">
                                   
                                </td>
                            </tr>--%>
                        </table>
                        <hr />
                        <div id="dvVessImage" align="center" style="padding: 10px;">
                            <asp:Image ID="imgVesselPhoto" runat="server" Height="270px" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div id="dvExSummary" style="width: 98%; page-break-before: always; page-break-after: always;">
                        <div style="text-align: center; font-size: 14px; font-family: Tahoma; font-weight: bold;
                            color: Black;">
                            <b>EXECUTIVE SUMMARY </b>
                        </div>
                        <hr />
                        <br />
                        <div id="dvExeSummary">
                        </div>
                    </div>
                    <br />
                    <div id="dvCatRating" style="width: 98%; page-break-inside: avoid; page-break-after: always;"
                        align="left">
                        <div style="text-align: center; font-size: 14px; font-family: Tahoma; font-weight: bold;
                            color: Black;">
                            <b>CATEGORY RATING</b>
                        </div>
                        <hr />
                        <br />
                        <div id="dvCategoryRating">
                        </div>
                    </div>
                    <br />
                    <div id="dvCatRatingChart" style="width: 80%;  page-break-inside: avoid;
                        page-break-after: always;" align="left">
                        <div id="dvCategoryRatingChart" style="width: 80%; height: 700px;" align="left">
                            

                            
                        </div>
                          <%--<div id="dvRatingLegend"  align="center" style="vertical-align:top;width: 80%;">
                            <asp:Label ID="lblLegend" runat="server" Text="Label" Font-Bold="True" 
                                Font-Names="Tahoma" Font-Size="10px" ForeColor="#333333"></asp:Label>
                        </div>--%>
                        <br />
                      <div id="dvRatingLegend"  align="center">
                            <asp:GridView ID="grdRatingLegend" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="black" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" 
                                Font-Names="Tahoma" Font-Size="10px" 
                                 >
                                <Columns>
                                    <asp:BoundField HeaderText="From" DataField="FromRating" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="50px">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="To" DataField="ToRating" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Rating" DataField="Rating" ItemStyle-Width="100px">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="RatingColor" DataField="RatingColor" ItemStyle-Width="100px" >
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>

                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor=" #333333" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                        </div>
                        <br />
                    </div>
                    <div id="dvSCR" style="width: 98%; page-break-inside: avoid; page-break-after: always;">
                        <div style="text-align: center; font-size: 14px; font-family: Tahoma; font-weight: bold;
                            color: Black;">
                            <b>SUB-CATEGORY RATING</b>
                        </div>
                        <hr />
                        <br />
                        <div id="dvSubCatRating">
                        </div>
                    </div>
                    <div id="dvOT3" style="width: 98%; page-break-inside: avoid; page-break-after: always;">
                        <div id="dvOT3CompanyLogo" style="display: none;">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Image ID="imgOT3Logo" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="text-align: center; font-size: 14px; font-family: Tahoma; font-weight: bold;
                            color: Black;">
                            <b>Worklist/OT03</b>
                        </div>
                        <hr />
                        <br />
                        <div id="dvOT03Cover" style="display: none;">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCWlShip1" runat="server" Font-Bold="true" ForeColor="Black" Font-Names="Tahoma"
                                            Font-Size="12px" Text="Ship"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCWlShip" runat="server" Font-Bold="true" ForeColor="Black" Font-Names="Tahoma"
                                            Font-Size="12px" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:Label ID="lblCWLDov" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="Black"
                                            Font-Size="12px" Text="Date Of Visit"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top;">
                                        :
                                    </td>
                                    <td>
                                        <table width="200px">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCwlFrom1" runat="server" Font-Bold="true" Font-Names="Tahoma"
                                                        ForeColor="Black" Font-Size="12px" Text="From"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCwlFrom" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="Black"
                                                        Font-Size="12px" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCWlTo1" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="Black"
                                                        Font-Size="12px" Text="To"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCWlTo" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="Black"
                                                        Font-Size="12px" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCwlDur1" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="Black"
                                                        Font-Size="12px" Text="Duration"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCwlDur" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="Black"
                                                        Font-Size="12px" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCWlPorts1" runat="server" Font-Bold="true" Font-Names="Tahoma"
                                            ForeColor="Black" Font-Size="12px" Text="Port/s"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCWlPorts" runat="server" Font-Bold="true" Font-Names="Tahoma"
                                            ForeColor="Black" Font-Size="12px" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="dvConditionReport">
                        </div>
                    </div>
                    <br />
                    <div id="dvNonOT3" style="width: 98%; page-break-inside: avoid; page-break-after: always;">
                        <div id="dvNonOT3CompanyLogo" style="display: none;">
                            <table style="width: 100%;">
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Image ID="imgNonOT3Logo" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="text-align: center; font-size: 14px; font-family: Tahoma; font-weight: bold;
                            color: Black;">
                            <b>Non-OT03</b>
                        </div>
                        <hr />
                        <br />
                        <div id="dvNonOT03Cover" style="display: none;">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWLShip1" runat="server" Font-Bold="true" ForeColor="Black" Font-Names="Tahoma"
                                            Font-Size="12px" Text="Ship"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblWLShip" runat="server" Font-Bold="true" ForeColor="Black" Font-Names="Tahoma"
                                            Font-Size="12px" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:Label ID="lblWLDov" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="Black"
                                            Font-Size="12px" Text="Date Of Visit"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top;">
                                        :
                                    </td>
                                    <td>
                                        <table width="200px">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblWLFrom1" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="Black"
                                                        Font-Size="12px" Text="From"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblWLFrom" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="Black"
                                                        Font-Size="12px" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblWlTo1" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="Black"
                                                        Font-Size="12px" Text="To"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblWlTo" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="Black"
                                                        Font-Size="12px" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblWlDur1" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="Black"
                                                        Font-Size="12px" Text="Duration"></asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblWlDur" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="Black"
                                                        Font-Size="12px" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblWLPorts1" runat="server" Font-Bold="true" Font-Names="Tahoma"
                                            ForeColor="Black" Font-Size="12px" Text="Port/s"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblWLPorts" runat="server" Font-Bold="true" Font-Names="Tahoma" ForeColor="Black"
                                            Font-Size="12px" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="dvDefect">
                        </div>
                    </div>
                    <br />
                    <%--  <div id="dvCatWLJobs" style="width: 98%; page-break-inside: avoid;">
                        <div style="text-align: center; font-size: 14px; font-family: Tahoma; font-weight: bold;
                            color: Black;">
                            <b>CATEGORY WISE WORKLIST REPORT</b>
                        </div>
                        <hr />
                        <br />
                           
                        <br />
                        <div id="dvCatWiseJobs" runat="server">
                        </div>
                    </div>--%>
                </div>
                <asp:HiddenField ID="hdnRating" runat="server" />
                <br />
                </b>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hdnContent" runat="server" />
        <div id="dvConfig" title="Report Configuration" style="display: none; width: 600px;
            height: 600px;">
          <%--  <asp:UpdatePanel ID="updReportConfig" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <br />
                    <p align="center">
                        <asp:Button ID="BtnSave" runat="server" Text="Save & Close" OnClick="BtnSave_Click" />
                        &nbsp;
                        <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" />
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
        <div id="dvConfirmBox" style="display: none;">
            <table>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="BtnYes" runat="server" Text="Yes" OnClientClick="return BtnYesClick();" />
                    </td>
                    <td align="left">
                        <asp:Button ID="BtnNo" runat="server" Text="No" OnClientClick="return BtnNoClick();" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
