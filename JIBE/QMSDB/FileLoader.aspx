<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileLoader.aspx.cs" Inherits="FileLoader" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document</title>
    <script src="JS/common.js" type="text/javascript"></script>
    <link href="../Styles/Main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/AsyncResponse.js"></script>
    <script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="JS/Watermark.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        historyBox
        {
            background-color: #efefef;
            border: 1px solid inset;
        }
        cssmarging
        {
            margin: 1.5em;
        }
    </style>
    <style type="text/css">
        @media all
        {
            .pagebreak
            {
                display: none;
            }
        }
        @media print
        {
            .pagebreak
            {
                display: block;
            }
            .pagebreak div.header
            {
                display: block;
                page-break-before: always;
            }
        }
    </style>
    <script type="text/javascript">
        function OpenDoc(url) {
            document.getElementById("ifrmDocument").src = url;
        }

        function OpenCheckOutDoc(url) {
            var checkStatus = confirm("You are about to Check Out the document.\n\nPlease save the document to your local system,edit and Check In from the same location.\n\nDo you want to continue?");
            if (checkStatus) {

                window.open(url, "Test", "", "");
            }
        }

        function PintProcedure(url) {
            window.open(url, "Test", "", "");
        }

        function ViewHistory(docId) {

            Async_getHistory(docId);
            //document.getElementById("dvHistory").style.display = 'block';
            $('#dvHistory').show('slow');
        }


        function closeHistory() {
            document.getElementById("dvHistory").style.display = 'none';
        }

        function Async_getHistory(docId) {
            var url = "docHistory.aspx?FileID=" + docId;
            var params = "";

            obj = new AsyncResponse(url, params, response_getHistory)
            obj.getResponse();
        }
        function response_getHistory(retval) {
            var ar, arS
            if (retval.indexOf('Working') >= 0) { return; }
            try {

                retval = clearXMLTags(retval);

                var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
                xmlDoc.async = "false";
                xmlDoc.loadXML(retval);

                var frm = xmlDoc.getElementsByTagName("form")[0];
                dv = frm.childNodes[0];

                frm.removeChild(dv);

                var xmlOut = "";
                var x = frm.childNodes;
                for (var i = 0; i < x.length; i++) {
                    xmlOut += x[i].xml;
                }
                document.getElementById("dvDocHistory").innerHTML = xmlOut;
            }
            catch (ex) { alert(ex.message) }
        }

        function clearXMLTags(retval) {
            try {
                retval = retval.replace('<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">', '');
            }
            catch (ex) { }
            return retval;
        }

        function print() {
            var doc = document.getElementById("lblDetails").innerHTML;
            var header = document.getElementById("lblHeader").innerHTML;
            //            //            doc.print();
            //          doc window.print();

            childWindow = window.open('', 'childWindow', 'location=yes, menubar=yes, toolbar=yes');
            childWindow.document.open();
            childWindow.document.write('<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:w="urn:schemas-microsoft-com:office:word"');
            childWindow.document.write('<body lang=EN-US style="tab-interval:.5in"> <table border="0" width="100%"> ');
            childWindow.document.write('<thead> <tr> <th style="width:100%">');
            childWindow.document.write(header);
            childWindow.document.write('</th> </tr></thead>');
            childWindow.document.write('<tbody lang=EN-US style="tab-interval:.5in>"  <tr>  <td width="100%"> ');
            childWindow.document.write(doc);
            childWindow.document.write('</td> </tr></tbody> </table>');
            childWindow.document.write('</body></html>');
            //          childWindow.screen(570);
            childWindow.print();
            childWindow.document.close();
            childWindow.close();

        }

        //        function print() {
        //            try {
        //                var selNodeUrl = document.getElementById("lblDocName").innerHTML;
        //                if (selNodeUrl == undefined || selNodeUrl == "") {
        //                    myMessage("No Document selected for printing!")
        //                }
        //                else {
        //                    if (selNodeUrl.indexOf('_blank') > 0) { myMessage("Please select a file"); return false; }
        //                    if (selNodeUrl.indexOf(".pdf") > 0) {
        //                        myMessage("Please use print button available inside pdf document.")
        //                    }
        //                    else {
        //                        //  frames[0].focus();
        //                        //  frames[0].print();
        //                        window.frames['ifrmDocument'].focus();
        //                        window.frames['ifrmDocument'].print();

        //                    }
        //                }
        //            }
        //            catch (ex)
        //        { alert(ex) }
        //        }

        //        function loader ()
        //        {
        //            WaterMarker.AddWaterMark(document.getElementById("FirstName"), "First Name");
        //            dojo.config.parseOnLoad = true;
        //            dojo.addOnLoad(loader);
        //          
        //        }

       

    
    </script>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
    <div id="divMessage" align="center">
        <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
    </div>
    <div id="dvMain">
        <div style="overflow: hidden; background-color: #cfcfff; margin-bottom: 1px; border: 1px solid outset;"
            id="Div2">
            <table id="docProperties" style="width: 100%;">
                <tr>
                    <td style="width: 100px">
                        <span style="color: #666"><b>File Name :</b></span>
                    </td>
                    <td>
                        <span style="color: #666">
                            <asp:Label ID="lblDocName" runat="server" Text=""></asp:Label></span>
                    </td>
                    <td style="width: 100px">
                        <span style="color: #666"><b>Version :</b></span>
                    </td>
                    <td>
                        <span style="color: #666">
                            <asp:Label ID="lblCurrentVersion" runat="server" Text=""></asp:Label></span>
                    </td>
                    <td style="text-align: right;" rowspan="2">
                        <img src="images/print.gif" alt="Print" onclick="print()" style="cursor: hand;" />&nbsp;
                        <%--  <img src="images/print.gif" alt="Print"  onclick="PintProcedure('PrintProcedure.aspx?CheckOut=1&PROCEDURE_ID=<%=GetDocID()%>')" style="cursor: hand;" />&nbsp;--%>
                        <img src="images/checkout.png" alt="Check Out" onmouseover="Check Out" width="20px"
                            height="20px" onclick="OpenCheckOutDoc('QMSDBProcedureBuilder.aspx?CheckOut=1&PROCEDURE_ID=<%=GetDocID()%>')"
                            style="cursor: hand;" />&nbsp;
                        <%--   <img src="images/checkin.png" alt="Check In" onclick="OpenDoc('CheckInForm.aspx?FileID=<%=GetDocID()%>');"
                            style="cursor: hand;" />&nbsp;--%>
                        <img src="images/history.png" alt="View History" onclick="ViewHistory('<%=GetDocID()%>')"
                            style="cursor: hand;" />&nbsp;
                        <%--   <img src="images/getLatest.png" alt="Get Latest File" onclick="OpenDoc('GetLatest.aspx?FileID=<%=GetDocID()%>')"
                            style="cursor: hand;" />--%>
                        <asp:Label ID="lblProcedureId" runat="server" Width="20"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="color: #666"><b>Status :</b></span>
                    </td>
                    <td>
                        <span style="color: #666">
                            <asp:Label ID="lblProcedureStatus" runat="server" Text=""></asp:Label></span>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvFrame" style="overflow: scroll; background-color: #cfcfff; border: 2px solid #cfcfff;
            padding: 1px;">
            <div id="dvHistory" class="historyBox" style="display: none; border: 1px solid inset;
                background-color: #efddef; padding: 2px; text-align: left;">
                <div id="dvDocHistory" style="width: 100%;">
                </div>
                <table cellspacing="0" cellpadding="0" style="width: 100%;">
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <img src="images/close.gif" alt="Close" onclick="closeHistory();" style="cursor: hand;" />
                        </td>
                    </tr>
                </table>
            </div>
            <%-- <div id="Divheader" runat ="server" style="background-color: White;">
             
            </div>--%>
            <div id="dvResult" style="background-color: White; min-height: 500px" class="ui-layout-east" >
                <asp:Label ID="lblHeader" runat="server" CssClass="cssmarging"></asp:Label></br>
                <asp:Label ID="lblDetails" runat="server" CssClass="cssmarging"></asp:Label>
            </div>
            <%--  <asp:Label ID="lblDetails" runat="server" Visible="false"></asp:Label>--%>
            <CKEditor:CKEditorControl ID="txtProcedureDetails" CssClass="cke_show_borders text-editor"
                ReadOnly="true" Width="100%" runat="server" Height="520" AutoGrowMaxHeight="1500"
                Visible="false"></CKEditor:CKEditorControl>
        </div>
    </div>
    </form>
</body>
</html>
