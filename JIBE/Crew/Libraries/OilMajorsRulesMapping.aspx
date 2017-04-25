<%@ Page Title="Oil Majors Rules Mapping" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="OilMajorsRulesMapping.aspx.cs" Inherits="Crew_Libraries_OilMajorsRulesMapping" %>

<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .required
        {
            background-color: #f2f5a9;
            border: 1px solid #dcdcdc;
            height: 18px;
        }
        .control-edit
        {
            font-family: Tahoma;
            font-size: 12px;
            padding: 0;
            width: 150px;
        }
        .RowStyle-css, .AlternatingRowStyle-css
        {
            height: 30px;
        }
        .even
        {
            background-color: #f6f6f6;
        }
        #chklstOilMajors tr:hover
        {
            background-color: #feecec !important;
        }
        #ctl00_MainContent_TabContainer1_TabPanel1_chkOilMajors tr:hover
        {
            background-color: #feecec !important;
        }
        #chklstRanks tr:hover
        {
            background-color: #feecec !important;
        }
        .gridmain-css tr:hover
        {
            background-color: #feecec !important;
        }
        #tblOilMajorsValues tr
        {
            height: 25px;
        }
        #tblOilMajorsValues tr:hover
        {
            background-color: #feecec;
        }
        .HeaderStyle-css tr:hover
        {
            background-color: none;
        }
        .NoRecordFound
        {
            color: Red;
            font-weight: bold;
            background-color: #feecec;
            text-align: center;
            font-size: 11px;
        }
        
        #ctl00_MainContent_TabContainer1_TabPanel1_chkOilMajors tr:nth-child(even)
        {
            background-color: #f6f6f6;
        }
        #chklstRanks tr:nth-child(even)
        {
            background-color: #f6f6f6;
        }
        #chklstOilMajors tr:nth-child(even)
        {
            background-color: #f6f6f6;
        }
        #bluronupdateprogress
        {
            background-color: black;
            height: 100%;
            left: 0;
            opacity: 0.5;
            position: absolute;
            top: 0;
            width: 100%;
            z-index: 700;
        }
        tr.GridPager td
        {
            background-color: #F6CEE3;
            background: url(/JIBE/Images/bg.png) left -20px repeat-x;
            color: Black;
            padding: 2px 0px 2px 0px;
            text-align: left;
        }
        
        .GridPager a, .GridPager span
        {
            text-align: center;
            margin: 2px;
            padding: 2px 5px 2px 5px;
            text-decoration: none;
            font-size: 12px;
            position: relative;
            font-family: Verdana;
        }
        
        .GridPager a
        {
            text-align: center;
            margin: 2px;
            padding: 2px 5px 2px 5px;
            text-decoration: none;
            font-size: 12px;
            position: relative;
            font-family: Verdana;
        }
        
        .GridPager span
        {
            background-color: #ccdef4;
            color: #000;
            border: 1px solid #969696;
            height: 20px;
        }
    </style>
    <script type="text/javascript">
        
        //Additional Rule JSON 
        var AdditionalRule = <%=JSONString %>;

        ///Validate Additional Rules
        function Validate() {
            
            if ($("#<%=chkOilMajors.ClientID %> input[type='checkbox']:checked").length==0) {
                alert("Please select atleast one oil major");
                return false;
            }

            var Result= false;
            for (var i = 0; i < AdditionalRule.length; i++) {
              var Rule = AdditionalRule[i]; 
                if (Rule["ID"]==$(".gridRules :selected").attr("value").split('_')[0]) {
                    switch (Rule["RuleName"].toLowerCase()) {
                       case "additionalrule1":
                            Result = AddtionalRule1();
                         break;
                         case "additionalrule2":
                            Result =  AddtionalRule2();
                         break;
                         case "additionalrule3":
                            Result =  AddtionalRule3();
                         break;
                          case "additionalrule4":
                            Result =  AddtionalRule4();
                         break;
                          case "additionalrule5":
                            Result =  AddtionalRule5();
                         break;
                         case "additionalrule6":
                            Result =  AddtionalRule6();
                         break;
                         case "additionalrule7":
                            Result =  AddtionalRule7();
                         break;
                         case "additionalrule8":
                            Result =  AddtionalRule8();
                         break;
                         case "additionalrule9":
                            Result =  AddtionalRule9();
                         break;
                         case "additionalrule10":
                            Result =  AddtionalRule10();
                         break;
                         case "additionalrule11":
                            Result =  AddtionalRule11();
                         break;
                         case "additionalrule12":
                            Result =  AddtionalRule12();
                         break;
                         case "additionalrule13":
                            Result =  AddtionalRule13();
                         break;
                         case "additionalrule14":
                            Result =  AddtionalRule14();
                         break;
                         case "additionalrule15":
                            Result =  AddtionalRule15();
                         break;
                         case "additionalrule16":
                            Result =  AddtionalRule16();
                         break;
                         case "additionalrule17":
                            Result =  AddtionalRule17();
                         break;
                         case "additionalrule18":
                            Result =  AddtionalRule18();
                         break;
                         case "additionalrule19":
                            Result =  AddtionalRule19();
                         break;
                    } 
                }
            } 
           
            if(Result)
             return true;
            else
             return false;
        }


        /// Additional Rule1 :- Minimum N1 days between date of joinings for N2 and N3
        function AddtionalRule1()
        {
            var ddl1 = document.getElementById("<%=ddlAdditionalRule1Rank1.ClientID %>");
            var ddl2 = document.getElementById("<%=ddlAdditionalRule1Rank2.ClientID %>");
            
            if ($.trim($("#<%=txtAdditionalRule1Days.ClientID %>").val())=="") {
                alert("Please enter value for N1");
                $("#<%=txtAdditionalRule1Days.ClientID %>").focus();
                return false;
            }             
            if (IsCheckNumeric($("#<%=txtAdditionalRule1Days.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule1Days.ClientID %>").focus();
                return false;
            }
            if (ddl1.value == "0" && ddl2.value != "0") {
                alert("Please select an option for N2");
                return false;
            }

            else if (ddl1.value != "0" && ddl2.value == "0") {
                alert("Please select an option for N3");
                return false;
            }
            else if (ddl1.value == "0" && ddl2.value == "0") {
                alert("Please select an option for N2 & N3");
                return false;
            }
            if (ddl1.value == ddl2.value) {
                alert("N2 & N3 options cannot be same");
                return false;
            }
            return true;
        }

         /// Additional Rule2 :- Minimum N1 Aggreated years on all types of tankers for all Deck officers
        function AddtionalRule2()
        {
           if ($.trim($("#<%=txtAdditionalRule2.ClientID %>").val())=="") {
                alert("Please enter valur for N1");
                $("#<%=txtAdditionalRule2.ClientID %>").focus();
                return false;
            }             
            if (IsCheckNumeric($("#<%=txtAdditionalRule2.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule2.ClientID %>").focus();
                return false;
            }
            else
              return true;
        }

         /// Additional Rule3 :- N1 English proficiency must be good
        function AddtionalRule3()
        {
            if ($("#<%=chkAdditionalRule3Ranks.ClientID %> input[type='checkbox']:checked").length==0) {
              alert("Please select atleast one rank");
              return false;
            }
            return true;
        }

         /// Additional Rule4 :- Senior deck officers(N1) must have aggregate N2 years
        function AddtionalRule4()
        {
            if ($("#<%=chkAdditionalRule4Ranks.ClientID %> input[type='checkbox']:checked").length==0) {
              alert("Please select atleast one rank");
              return false;
            }
            if ($.trim($("#<%=txtAdditionalRule4Years.ClientID %>").val())=="") {
                alert("Please enter value for N2");
                $("#<%=txtAdditionalRule4Years.ClientID %>").focus();
                return false;
            }             
            if (IsCheckNumeric($("#<%=txtAdditionalRule4Years.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule4Years.ClientID %>").focus();
                return false;
            }
            else
              return true;
        }

        /// Additional Rule5 :- Senior engineering officers(N1) must have aggregate N2 years
        function AddtionalRule5()
        {
            if ($("#<%=chkAdditionalRule5Ranks.ClientID %> input[type='checkbox']:checked").length==0) {
              alert("Please select atleast one rank");
              return false;
            }
            if ($.trim($("#<%=txtAdditionalRule5Years.ClientID %>").val())=="") {
                alert("Please enter value for N2");
                $("#<%=txtAdditionalRule5Years.ClientID %>").focus();
                return false;
            }             
            if (IsCheckNumeric($("#<%=txtAdditionalRule5Years.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule5Years.ClientID %>").focus();
                return false;
            }
            else
              return true;
        }

        /// Additional Rule6 :- Combined  aggregated for all deck and engineering officers(N1) shall not be less than N2 years
        function AddtionalRule6()
        {
            if ($("#<%=chkAdditionalRule6Ranks.ClientID %> input[type='checkbox']:checked").length==0) {
              alert("Please select atleast one rank");
              return false;
            }
            if ($.trim($("#<%=txtAdditionalRule6Years.ClientID %>").val())=="") {
                alert("Please enter value for N2");
                $("#<%=txtAdditionalRule6Years.ClientID %>").focus();
                return false;
            }             
            if (IsCheckNumeric($("#<%=txtAdditionalRule6Years.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule6Years.ClientID %>").focus();
                return false;
            }
            else
              return true;
        }

        /// Additional Rule7 :- If the N1 has less than N2 year, the N3 shall not be less than N4 year in rank
        function AddtionalRule7()
        {
            if ($("#<%=ddlAdditionalRule7Rank1.ClientID %>").val()=="0") {
              alert("Please select rank for N1");
              $("#<%=ddlAdditionalRule7Rank1.ClientID %>").focus();
              return false;
            }
            if($.trim($("#<%=txtAdditionalRule7Year1.ClientID %>").val())=="")
            {
               alert("Please enter value for N2");
               $("#<%=txtAdditionalRule7Year1.ClientID %>").focus();
               return false;
            }      
            if (IsCheckNumeric($("#<%=txtAdditionalRule7Year1.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule7Year1.ClientID %>").focus();
                return false;
            }

            if ($("#<%=ddlAdditionalRule7Rank2.ClientID %>").val()=="0") {
              alert("Please select rank for N3");
              $("#<%=ddlAdditionalRule7Rank2.ClientID %>").focus();
              return false;
            }  

            if($("#<%=ddlAdditionalRule7Rank1.ClientID %>").val()==$("#<%=ddlAdditionalRule7Rank2.ClientID %>").val())
            {
              alert("N1 and N3 ranks cannot be same");
              $("#<%=ddlAdditionalRule7Rank2.ClientID %>").focus();
              return false;
            }      

            if($.trim($("#<%=txtAdditionalRule7Year2.ClientID %>").val())=="")
            {
               alert("Please enter value for N4");
               $("#<%=txtAdditionalRule7Year2.ClientID %>").focus();
               return false;
            }      
            if (IsCheckNumeric($("#<%=txtAdditionalRule7Year2.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule7Year2.ClientID %>").focus();
                return false;
            }
            else
              return true;
        }

        /// Additional Rule8 :- If the N1 has less than N2 year, the N3 shall not be less than N4 year in rank
        function AddtionalRule8()
        {
            if ($("#<%=ddlAdditionalRule8Rank1.ClientID %>").val()=="0") {
              alert("Please select rank for N1");
              $("#<%=ddlAdditionalRule8Rank1.ClientID %>").focus();
              return false;
            }
            if($.trim($("#<%=txtAdditionalRule8Year1.ClientID %>").val())=="")
            {
               alert("Please enter value for N2");
               $("#<%=txtAdditionalRule8Year1.ClientID %>").focus();
               return false;
            }      
            if (IsCheckNumeric($("#<%=txtAdditionalRule8Year1.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule8Year1.ClientID %>").focus();
                return false;
            }
            
            if($("#<%=ddlAdditionalRule8Rank1.ClientID %>").val()==$("#<%=ddlAdditionalRule8Rank2.ClientID %>").val())
            {
              alert("N1 and N3 ranks cannot be same");
              $("#<%=ddlAdditionalRule8Rank2.ClientID %>").focus();
              return false;
            }    

            if ($("#<%=ddlAdditionalRule8Rank2.ClientID %>").val()=="0") {
              alert("Please select rank for N3");
              $("#<%=ddlAdditionalRule8Rank2.ClientID %>").focus();
              return false;
            }  
            if($.trim($("#<%=txtAdditionalRule8Year2.ClientID %>").val())=="")
            {
               alert("Please enter value for N4");
               $("#<%=txtAdditionalRule8Year2.ClientID %>").focus();
               return false;
            }      
            if (IsCheckNumeric($("#<%=txtAdditionalRule8Year2.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule8Year2.ClientID %>").focus();
                return false;
            }
            else
              return true;
        }

        
        /// Additional Rule9 :- Deck and Engine senior officers (combined)(N1) shall not have less than N2 years of experience in Years with Operator
        function AddtionalRule9()
        {
            if ($("#<%=chkAdditionalRule9Ranks.ClientID %> input[type='checkbox']:checked").length==0) {
              alert("Please select atleast one rank");
              return false;
            }
            if ($.trim($("#<%=txtAdditionalRule9Years.ClientID %>").val())=="") {
                alert("Please enter value for N2");
                $("#<%=txtAdditionalRule9Years.ClientID %>").focus();
                return false;
            }             
            if (IsCheckNumeric($("#<%=txtAdditionalRule9Years.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule9Years.ClientID %>").focus();
                return false;
            }
            else
              return true;
        }

         /// Additional Rule10 :- Deck and Engine 2nd and 3rd  officers (combined)(N1) shall not have less than N2 year of experience in Years with Operator
        function AddtionalRule10()
        {
            if ($("#<%=chkAdditionalRule10Ranks.ClientID %> input[type='checkbox']:checked").length==0) {
              alert("Please select atleast one rank");
              $("#<%=chkAdditionalRule10Ranks.ClientID %>").focus();
              return false;
            }
            if ($.trim($("#<%=txtAdditionalRule10Years.ClientID %>").val())=="") {
                alert("Please enter value for N2");
                $("#<%=txtAdditionalRule10Years.ClientID %>").focus();
                return false;
            }             
            if (IsCheckNumeric($("#<%=txtAdditionalRule10Years.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule10Years.ClientID %>").focus();
                return false;
            }
            else
              return true;
        }

         /// Additional Rule 11 :- Deck and Engine senior officers (combined)(N1) shall not have less than N2 years of experience in Years with Rank
        function AddtionalRule11()
        {
            if ($("#<%=chkAdditionalRule11Ranks.ClientID %> input[type='checkbox']:checked").length==0) {
              alert("Please select atleast one rank");
              $("#<%=chkAdditionalRule11Ranks.ClientID %>").focus();
              return false;
            }
            if ($.trim($("#<%=txtAdditionalRule11Years.ClientID %>").val())=="") {
                alert("Please enter value for N2");
                $("#<%=txtAdditionalRule11Years.ClientID %>").focus();
                return false;
            }             
            if (IsCheckNumeric($("#<%=txtAdditionalRule11Years.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule11Years.ClientID %>").focus();
                return false;
            }
            else
              return true;
        }


        /// Additional Rule 12 :- Deck and Engine 2nd and 3rd  officers (combined)(N1) shall not have less than N2 year of experience in Years with Rank
        function AddtionalRule12()
        {
            if ($("#<%=chkAdditionalRule12Ranks.ClientID %> input[type='checkbox']:checked").length==0) {
              alert("Please select atleast one rank");
              $("#<%=chkAdditionalRule12Ranks.ClientID %>").focus();
              return false;
            }
            if ($.trim($("#<%=txtAdditionalRule12Years.ClientID %>").val())=="") {
                alert("Please enter value for N2");
                $("#<%=txtAdditionalRule12Years.ClientID %>").focus();
                return false;
            }             
            if (IsCheckNumeric($("#<%=txtAdditionalRule12Years.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule12Years.ClientID %>").focus();
                return false;
            }
            else
              return true;
        }

         /// Additional Rule 13 :-Deck and Engine senior officers (combined)(N1) shall not have less than N2 years of experience in Years on This Type of Tanker
        function AddtionalRule13()
        {
            if ($("#<%=chkAdditionalRule13Ranks.ClientID %> input[type='checkbox']:checked").length==0) {
              alert("Please select atleast one rank");
              $("#<%=chkAdditionalRule13Ranks.ClientID %>").focus();
              return false;
            }
            if ($.trim($("#<%=txtAdditionalRule13Years.ClientID %>").val())=="") {
                alert("Please enter value for N2");
                $("#<%=txtAdditionalRule13Years.ClientID %>").focus();
                return false;
            }             
            if (IsCheckNumeric($("#<%=txtAdditionalRule13Years.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule13Years.ClientID %>").focus();
                return false;
            }
            else
              return true;
        }

         /// Additional Rule14 :- N1 and N2 should not board at the same time
        function AddtionalRule14()
        {
            if ($("#<%=ddlAdditionalRule14Rank1.ClientID %>").val()=="0") {
              alert("Please select rank for N1");
              $("#<%=ddlAdditionalRule14Rank1.ClientID %>").focus();
              return false;
            }
            if ($("#<%=ddlAdditionalRule14Rank2.ClientID %>").val()=="0") {
              alert("Please select rank for N2");
              $("#<%=ddlAdditionalRule14Rank2.ClientID %>").focus();
              return false;
            }
            if ($("#<%=ddlAdditionalRule14Rank1.ClientID %>").val()== $("#<%=ddlAdditionalRule14Rank2.ClientID %>").val()) {
               alert("N1 and N2 ranks cannot be same");
               $("#<%=ddlAdditionalRule14Rank2.ClientID %>").focus();
              return false;
            }
            return true;
        }

        /// Additional Rule15 :- N1 and N2 should not board at the same time
        function AddtionalRule15()
        {
            if ($("#<%=ddlAdditionalRule15Rank1.ClientID %>").val()=="0") {
              alert("Please select rank for N1");
              $("#<%=ddlAdditionalRule15Rank1.ClientID %>").focus();
              return false;
            }
            if ($("#<%=ddlAdditionalRule15Rank2.ClientID %>").val()=="0") {
              alert("Please select rank for N2");
              $("#<%=ddlAdditionalRule15Rank2.ClientID %>").focus();
              return false;
            }
            if ($("#<%=ddlAdditionalRule15Rank1.ClientID %>").val()== $("#<%=ddlAdditionalRule15Rank2.ClientID %>").val()) {
               alert("N1 and N2 ranks cannot be same");
               $("#<%=ddlAdditionalRule15Rank2.ClientID %>").focus();
              return false;
            }
            return true;
        }

         /// Additional Rule16 :- A minimum of N1 days shall lapse between replacement of the N2 and N3
        function AddtionalRule16()
        {   
            if ($.trim($("#<%=txtAdditionalRule16Days.ClientID %>").val())=="") {
                alert("Please enter value for N1");
                $("#<%=txtAdditionalRule16Days.ClientID %>").focus();
                return false;
            }             
            if (IsCheckNumeric($("#<%=txtAdditionalRule16Days.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule16Days.ClientID %>").focus();
                return false;
            }
            if ($("#<%=ddlAdditionalRule16Rank1.ClientID %>").val()=="0") {
              alert("Please select rank for N2");
              $("#<%=ddlAdditionalRule16Rank1.ClientID %>").focus();
              return false;
            }
            if ($("#<%=ddlAdditionalRule16Rank2.ClientID %>").val()=="0") {
              alert("Please select rank for N3");
              $("#<%=ddlAdditionalRule16Rank2.ClientID %>").focus();
              return false;
            }
            if ($("#<%=ddlAdditionalRule16Rank1.ClientID %>").val()== $("#<%=ddlAdditionalRule16Rank2.ClientID %>").val()) {
               alert("N2 and N3 ranks cannot be same");
               $("#<%=ddlAdditionalRule16Rank2.ClientID %>").focus();
              return false;
            }
            return true;
        }

         /// Additional Rule17 :- A minimum of N1 days shall lapse between replacement of the N2 and N3
        function AddtionalRule17()
        {   
            if ($.trim($("#<%=txtAdditionalRule17Days.ClientID %>").val())=="") {
                alert("Please enter value for N1");
                $("#<%=txtAdditionalRule17Days.ClientID %>").focus();
                return false;
            }             
            if (IsCheckNumeric($("#<%=txtAdditionalRule17Days.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule17Days.ClientID %>").focus();
                return false;
            }
            if ($("#<%=ddlAdditionalRule17Rank1.ClientID %>").val()=="0") {
              alert("Please select rank for N2");
              $("#<%=ddlAdditionalRule17Rank1.ClientID %>").focus();
              return false;
            }
            if ($("#<%=ddlAdditionalRule17Rank2.ClientID %>").val()=="0") {
              alert("Please select rank for N3");
              $("#<%=ddlAdditionalRule17Rank2.ClientID %>").focus();
              return false;
            }
            if ($("#<%=ddlAdditionalRule17Rank1.ClientID %>").val()== $("#<%=ddlAdditionalRule17Rank2.ClientID %>").val()) {
               alert("N2 and N3 ranks cannot be same");
               $("#<%=ddlAdditionalRule17Rank2.ClientID %>").focus();
              return false;
            }
            return true;
        }

        /// Additional Rule18 :- A minimum of N1 days shall lapse between replacement of the N2 and N3
        function AddtionalRule18()
        {   
            if ($.trim($("#<%=txtAdditionalRule18Days.ClientID %>").val())=="") {
                alert("Please enter value for N1");
                $("#<%=txtAdditionalRule18Days.ClientID %>").focus();
                return false;
            }             
            if (IsCheckNumeric($("#<%=txtAdditionalRule18Days.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule18Days.ClientID %>").focus();
                return false;
            }
            if ($("#<%=ddlAdditionalRule18Rank1.ClientID %>").val()=="0") {
              alert("Please select rank for N2");
              $("#<%=ddlAdditionalRule18Rank1.ClientID %>").focus();
              return false;
            }
            if ($("#<%=ddlAdditionalRule18Rank2.ClientID %>").val()=="0") {
              alert("Please select rank for N3");
              $("#<%=ddlAdditionalRule18Rank2.ClientID %>").focus();
              return false;
            }
            if ($("#<%=ddlAdditionalRule18Rank1.ClientID %>").val()== $("#<%=ddlAdditionalRule18Rank2.ClientID %>").val()) {
               alert("N2 and N3 ranks cannot be same");
               $("#<%=ddlAdditionalRule18Rank2.ClientID %>").focus();
              return false;
            }
            return true;
        }

        /// Additional Rule19 :- A minimum of N1 days shall lapse between replacement of the N2 and N3
        function AddtionalRule19()
        {   
            if ($.trim($("#<%=txtAdditionalRule19Days.ClientID %>").val())=="") {
                alert("Please enter value for N1");
                $("#<%=txtAdditionalRule19Days.ClientID %>").focus();
                return false;
            }             
            if (IsCheckNumeric($("#<%=txtAdditionalRule19Days.ClientID %>").val())) {
                alert("Please enter numeric value only");
                $("#<%=txtAdditionalRule19Days.ClientID %>").focus();
                return false;
            }
            if ($("#<%=ddlAdditionalRule19Rank1.ClientID %>").val()=="0") {
              alert("Please select rank for N2");
              $("#<%=ddlAdditionalRule19Rank1.ClientID %>").focus();
              return false;
            }
            if ($("#<%=ddlAdditionalRule19Rank2.ClientID %>").val()=="0") {
              alert("Please select rank for N3");
              $("#<%=ddlAdditionalRule19Rank2.ClientID %>").focus();
              return false;
            }
            if ($("#<%=ddlAdditionalRule19Rank1.ClientID %>").val()== $("#<%=ddlAdditionalRule19Rank2.ClientID %>").val()) {
               alert("N2 and N3 ranks cannot be same");
               $("#<%=ddlAdditionalRule19Rank2.ClientID %>").focus();
              return false;
            }
            return true;
        }



        ///Check whether value is numeric or not
        function IsCheckNumeric(chkval)
        {
          if (isNaN(chkval)) 
              return true;
          else
              return false;  
        } 

        function CheckAll(event) {
           $("#<%=chkAdditionalRule3Ranks.ClientID %> input[type='checkbox']").prop("checked",event.checked);
        }

        function UnCheckAll() {
            var intIndex = 0;
            var flag = 0;
            var rowCount = document.getElementById('<%=chkAdditionalRule3Ranks.ClientID %>').getElementsByTagName("input").length;
            for (i = 0; i < rowCount; i++) {
                if (document.getElementById("<%=chkAdditionalRule3Ranks.ClientID %>" + "_" + i)) {
                    if (document.getElementById("<%=chkAdditionalRule3Ranks.ClientID %>" + "_" + i).checked == true) {
                        flag = 1;
                    }
                    else {
                        flag = 0;
                        break;
                    }
                }
            }

        }
    </script>
    <style type="text/css">
        .ajax__tab_tab
        {
            width: 140px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <div class="page-title" style="margin-bottom: 10px;">
            Oil Majors Rules Mapping
        </div>
        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server">
            <ajaxToolkit:TabPanel runat="server" ID="rt1" HeaderText="Rule Mapping" ToolTip="Oil major rules mapping">
                <ContentTemplate>
                    <center>
                        <div style="min-height: 600px; width: 1050px; color: Black; margin-bottom: 25px;">
                            <asp:UpdateProgress ID="bluronupdateprogress" ClientIDMode="Static" runat="server"
                                AssociatedUpdatePanelID="UpdRulesMappings">
                                <ProgressTemplate>
                                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                                        color: black">
                                        <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdRulesMappings" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table width="100%" cellpadding="4" cellspacing="4">
                                        <tr>
                                            <td align="right" style="width: 15%">
                                                Oil Major :&nbsp;
                                            </td>
                                            <td align="left" style="width: 30%">
                                                <asp:DropDownList runat="server" ID="drpOilMajorsFilter" ClientIDMode="Static" AutoPostBack="true"
                                                    OnSelectedIndexChanged="drpOilMajorsFilter_OnSelectedIndexChanged" Style="min-width: 135px;
                                                    max-width: 250px;">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 5%">
                                                <input id="btnCreateNewRule" type="submit" value="Create New Rule" runat="server"
                                                    clientidmode="Static" visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                        <asp:Repeater runat="server" ID="rptOilMajorRuleGroup" OnItemDataBound="rptOilMajorRuleGroup_OnItemDataBound">
                                            <HeaderTemplate>
                                                <table width="100%" class="gridmain-css" border="1" style="margin: 5px;">
                                                    <tr class="HeaderStyle-css" style="height: 35px; text-align: center;">
                                                        <th>
                                                        </th>
                                                        <th>
                                                            Value
                                                        </th>
                                                        <th>
                                                            Oil Major
                                                        </th>
                                                        <th>
                                                            Last Updated
                                                        </th>
                                                        <th width="8%">
                                                            Action
                                                        </th>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr id="trGroupHeading" runat="server" style="background: #9dc9eb !important; height: 25px;">
                                                    <th style="text-align: left;" colspan="5">
                                                        <asp:Label ID="lblRuleGroup" rel='<%#Eval("ID") %>' Text='<%#Eval("Group_Name")%>'
                                                            runat="server" Style="margin-left: 3px;" title="Rule Group" />
                                                    </th>
                                                </tr>
                                                <asp:Repeater runat="server" ID="rptRules" OnItemDataBound="rptRules_OnItemDataBound">
                                                    <ItemTemplate>
                                                        <tr style="height: 28px;" id="trRules" runat="server">
                                                            <td style="text-align: left;">
                                                                <asp:Label Style="margin-left: 15px;" ID="lblRule" rel='<%#Eval("OmRuleId") %>' Text='<%#Eval("Rules")%>'
                                                                    runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label Text='<%#Eval("Value") %>' ID="lblValue" ClientIDMode="Static" rel='<%#Eval("ID")%>'
                                                                    runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblOilMajors" runat="server" />
                                                            </td>
                                                            <td>
                                                                <%# Convert.ToString(Eval("LastUpdate"))!=""? UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("LastUpdate"))) :"" %>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="Edit" rel='<%#Eval("OilMajorId")%>' RuleGroupId='<%#Eval("RuleGroupId") %>'
                                                                    OMRuleId='<%#Eval("OMRuleId") %>' CssClass="edit" ImageUrl="~/Images/Edit.gif"
                                                                    ToolTip="Edit Rule" runat="server" Height="16px" Width="16px" Style="vertical-align: middle;
                                                                    cursor: pointer;" Visible='<%# editAccess%>' OnCommand='Edit_OnEdit' CommandArgument='<%#Eval("[ID]")%>' />
                                                                <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure, you want to delete?')"
                                                                    CommandArgument='<%#Eval("[ID]")%>' ToolTip="Delete Rule" Height="16px" Width="16px"
                                                                    Style="vertical-align: middle;" ImageUrl="~/Images/delete.png" OnCommand="ImgDelete_onDelete"
                                                                    Visible='<%# deleteAccess %>' rel='<%#Eval("OilMajorId")%>'></asp:ImageButton>
                                                                <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ToolTip="Information" ImageUrl="~/Images/RecordInformation.png"
                                                                    Height="16px" Width="16px" runat="server" Style="vertical-align: middle; cursor: pointer;" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <table cellpadding="0" border="1" style="width: 100%;" id="tblNoRecord" runat="Server"
                                            visible="false" rules="all" class="gridmain-css">
                                            <tbody>
                                                <tr class="NoRecordFound">
                                                    <td>
                                                        No Records found
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div id="divadd" title='<%=OperationMode %>' style="display: none; font-family: Tahoma;
                                        text-align: left; font-size: 12px; color: Black; width: 30%" runat="server" clientidmode="Static">
                                        <table width="100%" cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td style="width: 5%">
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Rule&nbsp;:&nbsp;
                                                </td>
                                                <td style="width: 1%" align="right">
                                                </td>
                                                <td align="left" style="width: 25%">
                                                    <asp:DropDownList runat="server" ID="drpOilMajorRules" ClientIDMode="Static" Style="width: auto;">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList runat="server" ID="drpOilMajorsGroup" ClientIDMode="Static" Style="width: auto;">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Ranks<span class="ui-state-error-text">*</span>&nbsp;:&nbsp;
                                                </td>
                                                <td style="width: 1%" align="right">
                                                </td>
                                                <td align="left" style="width: 25%">
                                                    <div style="overflow-y: scroll; max-width: 350px; border: 1px solid #c6c6c6; max-height: 250px;">
                                                        <asp:CheckBoxList runat="server" ID="chklstRanks" ClientIDMode="Static" Width="100%">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="trRuleMappingValue">
                                                <td align="right">
                                                    Value&nbsp;:&nbsp;
                                                </td>
                                                <td style="width: 1%;" align="right">
                                                </td>
                                                <td align="left" style="width: 25%">
                                                    <asp:TextBox runat="server" CssClass="control-edit required" ID="txtRuleMappingValue"
                                                        ClientIDMode="Static" Width="80px" MaxLength="7" onkeyup="checkForSecondDecimal(this,event)" />
                                                    <cc1:FilteredTextBoxExtender runat="server" ID="fltrtxtRuleMappingValue" TargetControlID="txtRuleMappingValue"
                                                        FilterType="Numbers, Custom" ValidChars=".">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:Button Text="Same for all Oil Majors" ID="chkSameValueOilMajors" ClientIDMode="Static"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    Oil Majors<span class="ui-state-error-text">*</span>&nbsp;:&nbsp;
                                                </td>
                                                <td style="width: 1%" align="right">
                                                </td>
                                                <td align="left" style="width: 25%">
                                                    <asp:CheckBox Text="Select all" ID="chkSelectAllOilMajors" ClientIDMode="Static"
                                                        runat="server" />
                                                    <div style="overflow-y: scroll; display: none; width: 400px; border: 1px solid #c6c6c6;
                                                        max-height: 300px;">
                                                        <asp:CheckBoxList runat="server" ID="chklstOilMajors" ClientIDMode="Static" Width="100%">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                    <div style="overflow-y: scroll; max-width: 350px; border: 1px solid #c6c6c6; max-height: 400px;">
                                                        <asp:Repeater runat="server" ID="rptOilMajorsValue">
                                                            <HeaderTemplate>
                                                                <table border="0" width="100%" cellpadding="0" cellspacing="0" id="tblOilMajorsValues">
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkOilMajors" rel='<%# Eval("ID") %>' Text='<%# Eval("Oil_Major_Name") %>'
                                                                            runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        Value:
                                                                        <asp:TextBox runat="server" rel='<%# Eval("ID") %>' ID="txtOilMajorsValue" Width="80px"
                                                                            MaxLength="7" CssClass="control-edit required" onkeyup="checkForSecondDecimal(this,event)" />
                                                                        <cc1:FilteredTextBoxExtender runat="server" ID="fltrtxtRuleMappingValue" TargetControlID="txtOilMajorsValue"
                                                                            FilterType="Numbers, Custom" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <tr class="even">
                                                                    <td>
                                                                        <asp:CheckBox ID="chkOilMajors" rel='<%# Eval("ID") %>' Text='<%# Eval("Oil_Major_Name") %>'
                                                                            runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        Value:
                                                                        <asp:TextBox runat="server" rel='<%# Eval("ID") %>' ID="txtOilMajorsValue" Width="80px"
                                                                            MaxLength="7" CssClass="control-edit required" onkeyup="checkForSecondDecimal(this,event)" />
                                                                        <cc1:FilteredTextBoxExtender runat="server" ID="fltrtxtRuleMappingValue" TargetControlID="txtOilMajorsValue"
                                                                            FilterType="Numbers, Custom" ValidChars=".">
                                                                        </cc1:FilteredTextBoxExtender>
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
                                                <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" ClientIDMode="Static" OnClick="btnSave_OnClick" />
                                                    <asp:Button ID="btnClose" runat="server" Text="Close" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hdnRuleMappingID" runat="server" Value="0" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hdnOilMajors" runat="server" Value="" ClientIDMode="Static" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </center>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" ID="TabPanel1" HeaderText="Additional Rule Mapping"
                ToolTip="Oil majors additional rules">
                <ContentTemplate>
                    <div style="min-height: 600px; color: Black; margin-bottom: 25px;">
                        <center>
                            <table width="80%" style="min-width: 200px; margin-bottom: 8px;" cellspacing="5">
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Oil Major :&nbsp;
                                    </td>
                                    <td align="left" style="width: 1%">
                                        <asp:DropDownList runat="server" ID="ddlOilMajors" Width="135px" ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 10%">
                                        Rule List :&nbsp;
                                    </td>
                                    <td align="left" style="width: 5%">
                                        <asp:DropDownList ID="ddlRulesList" Style="max-width: 345px" runat="server" ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" style="width: 3%; text-align: right;">
                                        <asp:ImageButton ID="btnSearch" runat="server" ToolTip="Search" AlternateText="Search"
                                            OnClick="btnSearch_click" ImageUrl="~/Images/SearchButton.png" ClientIDMode="Static" />
                                    </td>
                                    <td style="width: 3%;">
                                        <asp:ImageButton ID="btnClearAdditionalRules" ToolTip="Clear" runat="server" AlternateText="Clear"
                                            OnClick="btnClearAdditionalRules_click" ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td style="vertical-align: top;">
                                        <asp:Button ID="btnCreateRuleAssign" runat="server" Text="Assign New Rule" OnClick="btnCreateAssign_click"
                                            Visible="false" />
                                    </td>
                                </tr>
                            </table>
                            <asp:UpdateProgress ID="UpdateProgress1" ClientIDMode="Static" runat="server" AssociatedUpdatePanelID="upAdditionalRule">
                                <ProgressTemplate>
                                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                                        color: black">
                                        <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel runat="server" ID="upAdditionalRule">
                                <ContentTemplate>
                                    <asp:GridView ID="gridOilMajorAssign" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                        EmptyDataRowStyle-CssClass="NoRecordFound" runat="server" AllowPaging="false"
                                        OnPageIndexChanging="gridOilMajorAssign_OnPageIndexChanging" GridLines="Both"
                                        OnRowDataBound="gridOilMajorAssign_RowDataBound" HeaderStyle-CssClass="HeaderStyle-css"
                                        HeaderStyle-Height="35px" Font-Size="11px" ShowHeaderWhenEmpty="false" RowStyle-CssClass="RowStyle-css"
                                        CssClass="gridmain-css" Width="98%" AlternatingRowStyle-CssClass="AlternatingRowStyle-css">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Oil Major">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOMN" runat="server" Style="margin: 5px;" Text='<% # Eval("Oil_Major_Name") %>'></asp:Label>
                                                    <asp:Label ID="lblOID" runat="server" Visible="false" Text='<% # Eval("OID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Rule">
                                                <ItemTemplate>
                                                    &nbsp;<asp:Label ID="lblRules" runat="server" Text='<%# BindRuleText(Convert.ToString(Eval("Rules")))%>'></asp:Label>
                                                    <asp:Label ID="lblRuleId" runat="server" Visible="false" Text='<% # Eval("RuleId") %>'></asp:Label>
                                                    <asp:Label ID="lblParam" runat="server" Visible="false" Text='<% # Eval("Params") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdnRuleName" runat="server" Value='<% # Eval("RuleName") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="500px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="N1">
                                                <ItemTemplate>
                                                    <asp:Label ID="N1" runat="server" Text='<%# Eval("N1") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="N2">
                                                <ItemTemplate>
                                                    <asp:Label ID="N2" runat="server" Text='<% #  Eval("N2") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="N3">
                                                <ItemTemplate>
                                                    <asp:Label ID="N3" runat="server" Text='<% # Eval("N3") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="N4">
                                                <ItemTemplate>
                                                    <asp:Label ID="N4" runat="server" Text='<% # Eval("N4") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox Enabled='<%# editAccess%>' ID="chkIsactive" Checked='<%# (Eval("IsActive").ToString() == "1" ? true : false) %>'
                                                        runat="server" OnCheckedChanged="chkIsactive_OnCheckedChanged" AutoPostBack="true"
                                                        rel='<%# Eval("ParentId") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lblEdit" ImageUrl="~/Images/Edit.gif" runat="server" ToolTip="Edit"
                                                        OnClick="lblEdit_Click" Visible='<%# editAccess%>' Style="border-width: 0; cursor: pointer;
                                                        height: 16px; vertical-align: middle; width: 16px;" rel='<%# Eval("ParentId") %>' />
                                                    <asp:ImageButton ID="lblDelete" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure, you want to delete?')"
                                                        ToolTip="Delete" ImageUrl="~/Images/delete.png" Visible='<%# deleteAccess%>'
                                                        OnClick="lblDelete_Click" Style="border-width: 0; cursor: pointer; height: 16px;
                                                        vertical-align: middle; width: 16px;" rel='<%# Eval("ParentId") %>'></asp:ImageButton>
                                                    <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                        Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_LIB_CM_AdditionalRuleMapping&#39;,&#39;OilMajorID="+Eval("OID").ToString()+" AND RuleId="+Eval("RuleId").ToString()+"&#39;,event,this)" %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="75px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle Wrap="False" CssClass="GridPager" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:GridView>
                                    <asp:HiddenField ID="hdnParentID" ClientIDMode="Static" runat="server" Value="0" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnClearAdditionalRules" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </center>
                    </div>
                    <asp:UpdateProgress ID="UpdateProgress2" ClientIDMode="Static" runat="server" AssociatedUpdatePanelID="updivAddvewRule">
                        <ProgressTemplate>
                            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                                color: black">
                                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updivAddvewRule">
                        <ContentTemplate>
                            <div id="divParam" title="Assign/Unassign Additional Rule" style="display: none">
                                <table width="100%" cellpadding="5">
                                    <tr>
                                        <td valign="top">
                                            Oil Majors:
                                        </td>
                                        <td valign="top">
                                            <div style="overflow-y: scroll; width: 400px; border: 1px solid #c6c6c6; max-height: 250px;">
                                                <asp:CheckBoxList runat="server" ID="chkOilMajors" Width="100%">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            Rule:
                                        </td>
                                        <td>
                                            <asp:DropDownList Style="max-width: 400px" ID="gridRules" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="gridRules_OnselectionIndexChanged" CssClass="gridRules">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule1" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table cellspacing="0" cellpadding="5">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule1Days" MaxLength="4" runat="server" Width="35px"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                            FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtAdditionalRule1Days">
                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                        days
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule1Rank1" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N3
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule1Rank2" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule2" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table cellpadding="5" cellspacing="0" width="25%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule2" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule3" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <div style="overflow-y: scroll; border: 1px solid #c6c6c6; max-height: 250px;">
                                                            <asp:CheckBoxList runat="server" ID="chkAdditionalRule3Ranks">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule4" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <div style="overflow-y: scroll; border: 1px solid #c6c6c6; max-height: 250px;">
                                                            <asp:CheckBoxList runat="server" ID="chkAdditionalRule4Ranks">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule4Years" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                                        years
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule5" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <div style="overflow-y: scroll; border: 1px solid #c6c6c6; max-height: 250px;">
                                                            <asp:CheckBoxList runat="server" ID="chkAdditionalRule5Ranks">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule5Years" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                                        years
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule6" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <div style="overflow-y: scroll; border: 1px solid #c6c6c6; max-height: 250px;">
                                                            <asp:CheckBoxList runat="server" ID="chkAdditionalRule6Ranks">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule6Years" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                                        years
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule7" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule7Rank1" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule7Year1" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                                        years
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N3
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule7Rank2" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N4
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule7Year2" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                                        years
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule8" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule8Rank1" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule8Year1" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                                        years
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N3
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule8Rank2" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N4
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule8Year2" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                                        years
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule9" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <div style="overflow-y: scroll; border: 1px solid #c6c6c6; max-height: 250px;">
                                                            <asp:CheckBoxList runat="server" ID="chkAdditionalRule9Ranks">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule9Years" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                                        years
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule10" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <div style="overflow-y: scroll; border: 1px solid #c6c6c6; max-height: 250px;">
                                                            <asp:CheckBoxList runat="server" ID="chkAdditionalRule10Ranks">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule10Years" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                                        years
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule11" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <div style="overflow-y: scroll; border: 1px solid #c6c6c6; max-height: 250px;">
                                                            <asp:CheckBoxList runat="server" ID="chkAdditionalRule11Ranks">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule11Years" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                                        years
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule12" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <div style="overflow-y: scroll; border: 1px solid #c6c6c6; max-height: 250px;">
                                                            <asp:CheckBoxList runat="server" ID="chkAdditionalRule12Ranks">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule12Years" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                                        years
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule13" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <div style="overflow-y: scroll; border: 1px solid #c6c6c6; max-height: 250px;">
                                                            <asp:CheckBoxList runat="server" ID="chkAdditionalRule13Ranks">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule13Years" MaxLength="4" runat="server" Width="50px"></asp:TextBox>
                                                        years
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule14" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule14Rank1" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule14Rank2" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule15" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule15Rank1" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule15Rank2" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule16" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule16Days" MaxLength="4" runat="server" Width="35px"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                            FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtAdditionalRule16Days">
                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                        days
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule16Rank1" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N3
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule16Rank2" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule17" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule17Days" MaxLength="4" runat="server" Width="35px"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                            FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtAdditionalRule17Days">
                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                        days
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule17Rank1" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N3
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule17Rank2" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule18" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule18Days" MaxLength="4" runat="server" Width="35px"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                            FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtAdditionalRule18Days">
                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                        days
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule18Rank1" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N3
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule18Rank2" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trAddtionalRule19" runat="server" visible="false">
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="5" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        N1
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAdditionalRule19Days" MaxLength="4" runat="server" Width="35px"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                            FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtAdditionalRule19Days">
                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                        days
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N2
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule19Rank1" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        N3
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlAdditionalRule19Rank2" Width="200px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Active:
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkActive" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnSaveRules" Text="Save" runat="server" OnClick="btnsaveRules_Click"
                                                OnClientClick="return Validate();" Visible="false" Width="75px" />
                                            <asp:Button ID="btnAdditionalCancel" ClientIDMode="Static" Text="Cancel" runat="server"
                                                Width="75px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnCreateRuleAssign" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="gridRules" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            $("body").on("click", "#<%= btnClearAdditionalRules.ClientID%>", function () {
                $("#ddlOilMajors").val('0');
                $("#ddlRulesList").val('0');
            });

            $("body").on("click", "#btnClose,#closePopupbutton", function () {
                ///Uncheck all checkbox inside New rule popup
                $("#divadd input[type='checkbox']").prop("checked", false);
                $("#trRuleMappingValue").show();
                $("#txtRuleMappingValue").val('');
                $("#hdnRuleMappingID").val('0');
                $("#tblOilMajorsValues input[type='text']").val('');
                $("#chkSameValueOilMajors").prop("checked", false);
                hideModal('divadd');
                //Enable DropDown
                $("#drpOilMajorsGroup").prop("disabled", false);
                $("#drpOilMajorRules").prop("disabled", false);
                return false;
            });

            ///Select all Oil Majors
            $("body").on("click", "#chkSelectAllOilMajors", function () {
                $("#tblOilMajorsValues input[type='checkbox']").prop("checked", $(this)[0].checked);
            });


            $("body").on("click", "#btnCreateNewRule", function () {
                $("#divadd").attr("title", "Add New Rule");
                $("#drpOilMajorsGroup").prop("disabled", false);
                $("#drpOilMajorRules").prop("disabled", false);

                /// Uncheck All Oil Majors and rankId when rule group is changed
                $("#chklstOilMajors input[type='checkbox']:checked").prop("checked", false);
                $("#chklstRanks input[type='checkbox']:checked").prop("checked", false);

                $("#txtRuleMappingValue").val('');
                $("#tblOilMajorsValues input[type='text']").val('');
                $("#chkSameValueOilMajors").prop("checked", false);
                $("#hdnRuleMappingID").val('0');
                showModal('divadd', true);
                return false;
            });


            $("body").on("click", "#btnCreateRuleAssign", function () {
                showModal('divParam', true);
                return false;
            });

            //check for Rule group type 
            $("body").on("change", "#drpOilMajorsGroup", function () {

                //check for option type rule starts here
                $("#trRuleMappingValue").show();
                if ($("#drpOilMajorsGroup option:selected").attr("Type") == "Options")
                    $("#trRuleMappingValue").hide();
                //check for option type rule starts here

                /// Uncheck All Oil Majors and rankId when rule group is changed
                $("#chklstOilMajors input[type='checkbox']:checked").prop("checked", false);
                $("#chklstRanks input[type='checkbox']:checked").prop("checked", false);

            });


            //check for oil major selected or not
            $("body").on("click", "#btnFilter", function () {
                if ($("#drpOilMajorsFilter").val() == "0") {
                    alert("Please select oil major");
                    return false;
                }
            });

            $("body").on("click", "#btnSave", function () {

                ///Check aleast one rank is selected
                if ($("#chklstRanks input[Type='checkbox']:checked").length == 0) {
                    alert("Please select atleast one rank");
                    return false;
                }

                ///Check aleast one oil major is selected
                if ($("#tblOilMajorsValues input[Type='checkbox']:checked").length == 0) {
                    alert("Please select atleast one oil major");
                    return false;
                }

                if ($.trim($("#drpOilMajorRules :selected").text().toLowerCase()) == 'minimum aggregated') {
                    if ($("#chklstRanks input[type='checkbox']:checked").length == 1) {
                        $(this).prop("checked", false);
                        alert("Minimum two ranks should be selected for minimum aggregated rule");
                        return false;
                    }
                }

                var Return = false;
                var OilMajors = $("#tblOilMajorsValues input[type='checkbox']:checked");
                OilMajors.each(function (index) {
                    var oilMajorName = $(this).next().text();
                    var oilMajorId = $(this).parent("span")[0].attributes["rel"].value;
                    if ($("input[type='text'][rel='" + oilMajorId + "']").val() == "" && Return == false) {
                        $("input[type='text'][rel='" + oilMajorId + "']").focus();
                        alert("Please enter value for " + oilMajorName);
                        Return = true;
                    }
                });
                if (Return) {
                    return false;
                }

                $("#drpOilMajorsGroup").prop("disabled", false);
                $("#drpOilMajorRules").prop("disabled", false);
            });

            ///value same for all oil majors 
            $("body").on("click", "#chkSameValueOilMajors", function () {
                if ($("#tblOilMajorsValues input[type='checkbox']:checked").length == 0) {
                    alert("Please select atleast one oil major");
                    return false;
                }
                if ($.trim($("#txtRuleMappingValue").val()) == "") {
                    $("#txtRuleMappingValue").focus();
                    alert("Please enter value");
                    return false;
                    $(this).prop("checked", false);
                    $("#txtRuleMappingValue").focus();
                }

                var CheckedOilMajors = $("#tblOilMajorsValues input[type='checkbox']:checked");
                CheckedOilMajors.each(function (index) {
                    var ID = $(this)[0].id.replace("ctl00_MainContent_TabContainer1_rt1_rptOilMajorsValue_", "").replace("_chkOilMajors", "");
                    $("#ctl00_MainContent_TabContainer1_rt1_rptOilMajorsValue_" + ID + "_txtOilMajorsValue").val($("#txtRuleMappingValue").val())
                });

                $("#txtRuleMappingValue").val('');
                $(this).prop("checked", false);
                return false;
            });

            ///Clear value field if oil major is unchecked
            $("body").on("click", "#tblOilMajorsValues input[type='checkbox']", function () {
                if (!$(this).prop("checked")) {
                    var ID = $(this)[0].id.replace("ctl00_MainContent_TabContainer1_rt1_rptOilMajorsValue_", "").replace("_chkOilMajors", "");
                    $("#ctl00_MainContent_TabContainer1_rt1_rptOilMajorsValue_" + ID + "_txtOilMajorsValue").val('');
                }
            });


            ///Only one rank has to be selected for minimum rule
            $("body").on("click", "#chklstRanks input[type='checkbox']", function () {
                if ($.trim($("#drpOilMajorRules :selected").text().toLowerCase()) == 'minimum') {
                    if ($("#chklstRanks input[type='checkbox']:checked").length > 1) {
                        $(this).prop("checked", false);
                        alert("Only one rank can be selected for the minimum type of rule");
                        return false;
                    }
                }
            });

            ///Clear the rank selections 
            $("body").on("change", "#drpOilMajorRules", function () {
                $("#chklstRanks [type='checkbox']").prop("checked", false);
                $("#tblOilMajorsValues [type='checkbox']").prop("checked", false);
                $("#tblOilMajorsValues input[type='text']").val('');
            });

            $("body").on("click", "#btnAdditionalCancel", function () {
                $("#divParam_dvModalPopupCloseButton #closePopupbutton").click();
                return false;
            });
        });


        function checkForSecondDecimal(sender, e) {
            var val = sender.value;
            var re = /^([0-9]+[\.]?[0-9]?[0-9]?|[0-9]+)$/g;
            var re1 = /^([0-9]+[\.]?[0-9]?[0-9]?|[0-9]+)/g;
            if (re.test(val)) {
                //do something here

            } else {
                val = re1.exec(val);
                if (val) {
                    sender.value = val[0];
                } else {
                    sender.value = "";
                }
            }
        }
    </script>
</asp:Content>
