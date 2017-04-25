<%@ Page Title="Inspection Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="InspectionReportChecklist.aspx.cs" Inherits="InspectionReport_Checklist"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.alerts.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="http://www.google.com/jsapi" type="text/javascript"></script>
    <link href="../../Styles/jquery.alerts.css" rel="stylesheet" />
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
    </style>
    <script type="text/javascript" language="javascript">

  

         google.load('visualization', '1.0', { 'packages': ['corechart'] });

 
       function BtnYesClick()    
       {
        hideModal("dvConfirmBox");
          $('#dvWorklist').load("InspectionWorklistReport.aspx?InspID=" + <%= Request.QueryString["InspID"].ToString() %>+ "&ReportType=" + <%= Request.QueryString["ReportType"].ToString() %> +"&rnd=" + Math.random() + ' #dvWorklist');
        
           ReplaceExeSummaryText();
          
           return false;
       }
       function BtnNoClick()    
       {
          hideModal("dvConfirmBox");
         $("#dvWL").hide();
           ReplaceExeSummaryText();
          
           return false;
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
         
        function ReplaceTextRemark()
        {
           
             var txt1=document.getElementsByClassName("input");
             for(var i=0;i<txt1.length;i++)
             {
                  $(txt1[i]).replaceWith(  $(txt1[i]).val() );

                    txt1=document.getElementsByClassName("input");
                    if (txt1.length == 0) {
                        break;
                    }
                    else {
                        i = -1;
                    }
                 
             }    

  
           
           return false;
            
        }
    
        function ReplaceDropDownWithLabel()
        {
                      var ddl = $('select');   
             for(var k=0; k<ddl.length;k++ ){
                    
                    $(ddl[k]).replaceWith(  $(ddl[k]).children("option").filter(":selected").text().replace('--SELECT--','') );




             }

           return false;
            
        }

        function ShowModalPopup(divId)
        {
            showModal(divId)
            return false;
        }
     
            function fnSubCategoryLoadComplete()
            {
                ReplaceDropDownWithLabel();
                ReplaceTextRemark();
                ReplaceExeSummaryText();
               
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
                    LastReport=$.trim(Math.round(dataValues[i].LastReport));
                }
                 if ($.trim(dataValues[i].ThisReport)=="")
                {
                    ThisReport=0
                }
                else
                {
                    ThisReport=$.trim(Math.round(dataValues[i].ThisReport));
                }
          
                        data.addRow([parseInt(ThisReport),parseInt(LastReport),dataValues[i].Description]);
            

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

                var options = { 'title': '',
                    hAxis: { textStyle: {

                        fontSize: 10

                    }, slantedText: true, slantedTextAngle: 30
                    },
                    vAxis: { minValue: 0 }
                };

                var chart = new google.visualization.ColumnChart(document.getElementById('dvCategoryRatingChart'));

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
                    ThisReport=$.trim(Math.round(dataValues[i].ThisReport));
                }
          
                        data.addRow([parseInt(ThisReport),dataValues[i].Description]);
            

                }

                var view = new google.visualization.DataView(data);
                view.setColumns([1, 0, 
                                    { calc: "stringify",
                                   sourceColumn: 0,
                                   type: "string",
                                   role: "annotation"
                               }  
                      
                                   ]);

                var options = { 'title': '',
                    hAxis: { textStyle: {

                        fontSize: 10

                    }, slantedText: true, slantedTextAngle: 30
                    },
                    vAxis: { minValue: 0 }
                };

                var chart = new google.visualization.ColumnChart(document.getElementById('dvCategoryRatingChart'));

                chart.draw(view, options);    
      }
    }


    function GetDivData() {

  

        var str = document.getElementById('dvReport').innerHTML;
        document.getElementById($('[id$=hdnContent]').attr('id')).value = str;

        __doPostBack("<%=BtnPrintPDF.UniqueID %>", "onclick");


        
    }

    </script>
    <style type="text/css">
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
        
        .insp-Chk-Checklist-td
        {
            color: Black;
            font-size: 14px;
            min-width: 300px;
            border: 0px solid gray;
            background-color: #efefef;
            padding: 5px;
            padding-left: 10px;
            font-weight: bold;
            height: 22px;
            margin-left: 10px;
        }
        .insp-Chk-Checklist
        {
            color: Black;
            font-size: 14px;
            float: left;
            min-width: 300px;
            border: 0px solid gray;
            font-weight: bold;
        }
        .insp-Chk-Group
        {
            background-color: Yellow;
            color: Black;
            font-size: 14px;
            border: 1px solid gray;
            background-color: #efefef;
            padding: 5px;
            font-weight: bold;
            cursor: default;
        }
        
        
        .insp-Chk-Location
        {
            border: 1px solid gray;
            background-color: Gray;
            color: Black;
            font-size: 12px;
            padding: 5px;
            font-weight: bold;
            cursor: default;
        }
        
        .insp-Chk-Location-Text
        {
            border: 2px solid black;
            color: Black;
            font-size: 12px;
            padding: 5px;
            font-weight: bold;
            cursor: default;
        }
        
        
        .insp-Chk-Item-Container
        {
            color: Black;
            font-size: 11px;
            cursor: pointer;
        }
        
        
        .insp-Item-Pos1
        {
            margin-left: 10px;
        }
        .insp-Item-Pos2
        {
            margin-left: 10px;
        }
        .insp-Item-Pos3
        {
            margin-left: 10px;
        }
        .insp-Item-Pos4
        {
            margin-left: 10px;
        }
        .insp-Item-Pos5
        {
            margin-left: 10px;
        }
        .insp-Item-Pos6
        {
            margin-left: 10px;
        }
        .insp-Item-Pos7
        {
            margin-left: 10px;
            float: left;
        }
        
        
        .insp-OptionText
        {
            border: 1px solid black;
            background-color: transparent;
            color: black;
            font-size: 10px;
            padding: 3px;
            font-weight: bold;
            float: right;
        }
        .insp-OptionText1
        {
            border: 1px solid gray;
            background-color: transparent;
            color: Black;
            font-size: 10px;
            padding: 3px;
            margin: 2px;
            font-weight: normal;
        }
        
        
        
        .Addshedule
        {
            overflow: hidden;
            display: inline-block;
            float: right;
            padding-right: 7px;
            padding-top: 0px;
        }
        
        .rating
        {
            overflow: hidden;
            display: inline-block; /*padding-left:700px;
          border: 1px solid red;*/
            float: right;
        }
        .rating-input
        {
            position: absolute;
            left: 0;
            top: -50px;
        }
        .rating-star
        {
            display: block;
            float: right;
            width: 16px;
            height: 16px;
            background: url('../../Images/star.png') 0 -16px; /* background: url('http://kubyshkin.ru/samples/star-rating/star.png') 0 -16px;*/
        }
        .rating-star:hover, .rating-star:hover ~ .rating-star, .rating-input:checked ~ .rating-star
        {
            background-position: 0 0;
        }
        
        .badge
        {
            color: white;
        }
        
        .badge2
        {
            color: white;
            float: left;
        }
        
        a:link
        {
            color: white;
        }
        
        a:visited
        {
            color: white;
        }
        
        .badge1
        {
            overflow: hidden;
            display: inline-block;
            float: right;
            background: green;
            color: white;
            width: 19px;
            height: 19px;
            text-align: center;
            text-decoration: none;
            line-height: 17px;
            border-radius: 50%;
            box-shadow: 0 0 1px #333;
            padding-right: 0px;
        }
        .badge1[data-badge]:after
        {
            content: attr(data-badge);
            position: absolute;
            top: -12px;
            right: -10px;
            font-size: 1.0em;
            background: green;
            color: white;
            width: 25px;
            height: 25px;
            text-align: center;
            text-decoration: none;
            line-height: 25px;
            border-radius: 50%;
            box-shadow: 0 0 1px #333;
        }
        
        .insp-table
        {
            float: right;
            margin-top: -4px;
        }
        
        /* Just for the demo */
        body
        {
            margin: 20px;
        }
        .tblheader
        {
            width: 100px;
        }
        .tbldata
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2;">
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
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <b style="float: right; margin-top: -14px;">&nbsp;<asp:ImageButton ID="BtnPrint"
                        runat="server" OnClientClick="return PrintDiv('dvReport');" ImageUrl="~/Images/Printer.png"
                        Width="20px" Height="20px" ToolTip="Print"></asp:ImageButton>
                    </b><b style="float: right; margin-top: -14px; margin-right: 10px;">&nbsp;&nbsp; &nbsp;&nbsp;
                        <asp:ImageButton ID="BtnPrintPDF" runat="server" ImageUrl="~/Images/PDF-icon.png"
                            Width="20px" Height="20px" ToolTip="Export PDF" OnClick="BtnPrintPDF_Click" OnClientClick="GetDivData()">
                        </asp:ImageButton>
                    </b><b style="float: right; margin-top: -14px; margin-right: 10px;">
                        <asp:ImageButton ID="BtnConfig" runat="server" ImageUrl="~/Images/Add-icon.png" Width="20px"
                            Height="20px" ToolTip="Set Report Configuration" OnClick="BtnConfig_Click" Visible="False">
                        </asp:ImageButton>
                    </b>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="dvReport" style="width: 100%;">
                    <style type="text/css">
                        .insp-Chk-Checklist-td
                        {
                            color: Black;
                            font-size: 14px;
                            min-width: 300px;
                            border: 0px solid gray;
                            background-color: #efefef;
                            padding: 5px;
                            padding-left: 10px;
                            font-weight: bold;
                            height: 22px;
                            margin-left: 10px;
                        }
                        .insp-Chk-Checklist
                        {
                            color: Black;
                            font-size: 14px;
                            float: left;
                            min-width: 300px;
                            border: 0px solid gray;
                            font-weight: bold;
                        }
                        .insp-Chk-Group
                        {
                            background-color: Yellow;
                            color: Black;
                            font-size: 14px;
                            border: 1px solid gray;
                            background-color: #efefef;
                            padding: 5px;
                            font-weight: bold;
                            cursor: default;
                        }
                        
                        
                        .insp-Chk-Location
                        {
                            border: 1px solid gray;
                            background-color: Gray;
                            color: Black;
                            font-size: 12px;
                            padding: 5px;
                            font-weight: bold;
                            cursor: default;
                        }
                        
                        .insp-Chk-Location-Text
                        {
                            border: 2px solid black;
                            color: Black;
                            font-size: 12px;
                            padding: 5px;
                            font-weight: bold;
                            cursor: default;
                        }
                        
                        
                        .insp-Chk-Item-Container
                        {
                            color: Black;
                            font-size: 11px;
                            cursor: default;
                        }
                        
                        
                        .insp-Item-Pos1
                        {
                            margin-left: 10px;
                        }
                        .insp-Item-Pos2
                        {
                            margin-left: 10px;
                        }
                        .insp-Item-Pos3
                        {
                            margin-left: 10px;
                        }
                        .insp-Item-Pos4
                        {
                            margin-left: 10px;
                        }
                        .insp-Item-Pos5
                        {
                            margin-left: 10px;
                        }
                        .insp-Item-Pos6
                        {
                            margin-left: 10px;
                        }
                        .insp-Item-Pos7
                        {
                            margin-left: 10px;
                            float: left;
                        }
                        
                        
                        .insp-OptionText
                        {
                            border: 1px solid black;
                            background-color: transparent;
                            color: black;
                            font-size: 10px;
                            padding: 3px;
                            font-weight: bold;
                            float: right;
                        }
                        .insp-OptionText1
                        {
                            border: 1px solid gray;
                            background-color: transparent;
                            color: Black;
                            font-size: 10px;
                            padding: 3px;
                            margin: 2px;
                            font-weight: normal;
                        }
                        
                        .CreateHtmlTableFromDataTable-table
                        {
                            background-color: #FFFFFF;
                            width: 100%;
                        }
                        
                        .CreateHtmlTableFromDataTable-PageHeader
                        {
                            border: 1px solid #B1B1B1;
                            background-color: #F6B680;
                        }
                        
                        .CreateHtmlTableFromDataTable-DataHedaer
                        {
                            background-color: #F2F2F2;
                            border: 1px solid #B1B1B1;
                            font-size: 11px;
                            text-align: center;
                        }
                        .CreateHtmlTableFromDataTable-Data
                        {
                            border: 1px solid #B1B1B1;
                            font-size: 11px;
                        }
                    </style>
                    <div id="dvCatWLJobs" style="border: 1px solid black; width: 98%; page-break-inside: avoid;">
                        <div id="dvReportCover" style="border-bottom: 1px solid black; width: 100%; page-break-after: avoid;">
                            <table width="100%">
                                <tr>
                                    <td style="width: 80px">
                                        <asp:Label ID="Label8" runat="server" Font-Bold="true" Text="Vessel Name"></asp:Label>
                                    </td>
                                    <td class="tbldataSeprt">
                                        :
                                    </td>
                                    <td class="tbldata">
                                        <asp:Label ID="lblShipName" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    </td>
                                    <td class="tblheader">
                                        <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="Inspector Name"></asp:Label>
                                    </td>
                                    <td class="tbldataSeprt">
                                        :
                                    </td>
                                    <td class="tbldata">
                                        <asp:Label ID="lblAttendendBy" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    </td>
                                    <td style="width: 50px;">
                                        <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="PORT/S"></asp:Label>
                                    </td>
                                    <td class="tbldataSeprt">
                                        :
                                    </td>
                                    <td class="tbldata">
                                        <asp:Label ID="lblPorts" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    </td>
                                    <td class="tblheader">
                                        <asp:Label ID="Label15" runat="server" Font-Bold="true" Text="Inspection Date"></asp:Label>
                                    </td>
                                    <td class="tbldataSeprt">
                                        :
                                    </td>
                                    <td class="tbldata">
                                        <asp:Label ID="lblFrom" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div style="text-align: center; border-top: 1px solid black;">
                            <b>CATEGORY WISE WORKLIST REPORT</b>
                        </div>
                        <hr />
                        <br />
                        <div id="dvCatWiseJobs" runat="server" style="width: 100%;">
                        </div>
                    </div>
                    <br />
                </div>
                <asp:HiddenField ID="hdnRating" runat="server" />
                </b>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvConfig" title="Report Configuration" style="display: none; width: 600px;
            height: 600px;">
            <asp:UpdatePanel ID="updReportConfig" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdReportConfig" GridLines="Vertical" CellPadding="4" runat="server"
                        ShowFooter="True" AutoGenerateColumns="false" BorderWidth="1px" Width="100%">
                        <HeaderStyle CssClass="Rating-HeaderStyle-css" />
                        <FooterStyle CssClass="Rating-FooterStyle-css" />
                        <RowStyle CssClass="RowStyle-css" BorderStyle="Solid" BorderColor="#000" BorderWidth="1px" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                        <Columns>
                            <asp:BoundField HeaderText="Key No" DataField="KeyNo" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="50px" />
                            <asp:BoundField HeaderText="Description" DataField="KeyDescription" ItemStyle-Width="300px"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Value" DataField="KeyValue" ItemStyle-Width="50px" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Is Active
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Convert.ToBoolean(Eval("Active_Status")) %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No Records Found
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <br />
                    <p align="center">
                        <asp:Button ID="BtnSave" runat="server" Text="Save & Close" OnClick="BtnSave_Click" />
                        &nbsp;
                        <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" />
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
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
            <asp:HiddenField ID="hdnContent" runat="server" />
        </div>
    </div>
</asp:Content>
