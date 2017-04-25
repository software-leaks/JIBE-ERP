<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupdtInspReport.aspx.cs"
    Inherits="Technical_Worklist_SupdtInspReport" Title="Inspection Worklist Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: right">
        <button onclick="PrintFunc()">
            Print</button>
        <style>
            @media print
            {
                .tr
                {
                    background-color: #1a4567 !important;
                    -webkit-print-color-adjust: exact;
                }
            }
            @media print
            {
                .th
                {
                    color: white !important;
                }
            }
        </style>
        <script type="text/javascript">
            function PrintFunc() {

                var divElements = document.getElementById('<%=dvinspectionReport.ClientID%>').innerHTML;
                //Get the HTML of whole page
                var oldPage = document.body.innerHTML;

                //Reset the page's HTML with div's HTML only
                document.body.innerHTML =
              "<html><head><title></title>"+
                "        <style>"+
           " @media print"+
            "{"+
                "tr.vendorListHeading"+
                "{"+
                    "background-color: #1a4567 !important;"+
                    "-webkit-print-color-adjust: exact;"+
                "}"+
            "}"+
            "@media print"+
            "{"+
                ".vendorListHeading th"+
                "{"+
                    "color: white !important;"+
                "}"+
            "}"+
        "</style>"+ 
              
              "</head><body>" +
              divElements + "</body>";

                //Print Page
                window.print();

                //Restore orignal HTML
                document.body.innerHTML = oldPage;
            }</script>
    </div>
    <div id="dvinspectionReport" runat="server">
    </div>
    </form>
</body>
</html>
