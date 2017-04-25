<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="SubmitQuote.aspx.cs"
    Inherits="SubmitQuote" EnableEventValidation="false" Title="Submit Quotation" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <title>Submit Quote</title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <link href="../Styles/EGSoft.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/drag.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.alerts.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.draggable').draggable();
        });

        function getFlightDetail() {
            var obj = getOBJ("<%=grdFlights.ClientID%>");

            getOBJ("<%=txtFlight.ClientID%>").value = "";
            getOBJ("<%=txtFrom.ClientID%>").value = "";
            getOBJ("<%=txtTo.ClientID%>").value = "";
            getOBJ("<%=txtDeparureDate.ClientID%>").value = "";
            getOBJ("<%=txtArrivalDate.ClientID%>").value = "";
            getOBJ("<%=cmbTravelClass.ClientID%>").value = "";
            getOBJ("<%=cmbFlightStatus.ClientID%>").value = "";
            getOBJ("<%=txtAirlineLocator.ClientID%>").value = "";

            getOBJ("<%=cmbDepHours.ClientID%>").value = "";
            getOBJ("<%=cmbDepMins.ClientID%>").value = "";
            getOBJ("<%=cmbArrHours.ClientID%>").value = "";
            getOBJ("<%=cmbArrMins.ClientID%>").value = "";

            getOBJ("<%=txtFrom.ClientID%>").value = obj.rows[1].cells[0].innerText.trim();
            getOBJ("<%=txtTo.ClientID%>").value = obj.rows[1].cells[1].innerText.trim();
            getOBJ("<%=txtDeparureDate.ClientID%>").value = obj.rows[1].cells[2].innerText.trim();
            getOBJ("<%=txtArrivalDate.ClientID%>").value = obj.rows[1].cells[2].innerText.trim();



        }

        function newQuote(ReqID) {



            if (getOBJ("<%=cmbCurrency.ClientID%>").value.trim() == "")
            { alert("Please select currency"); return false; }

            //            var odiv = document.getElementById("blur-on-updateprogress");
            //            odiv.style.display = "block";

            //            var url = 'AddQuotation.aspx?requestid=<%=Request.QueryString["RequestID"]%>&currency=' + getOBJ("<%=cmbCurrency.ClientID%>").value.trim();
            //            OpenPopupWindow('NewQuotation', 'New Quotation', url, 'popup', 550, 1200, null, null, true, false, true, newQuote_Closed);


        }

        function newQuote_Closed() {

            document.getElementById("ctl00_MainContent_btnHiddenSubmit").click();
            //            window.location.reload();
            //            return true;
        }

        function openAttachments(ReqID) {
            var url = 'Attachment.aspx?atttype=DOCUMENT&requestid=' + ReqID;

            document.getElementById("iFrmPopup").src = url;
            showModal('dvPopUp', true, Attachment_Closed);

            //            var url = 'Attachment.aspx?atttype=DOCUMENT&requestid=' + ReqID;
            //            OpenPopupWindow('AttachmentWID', 'Attachments', url, 'popup', 550, 900, null, null, true, false, true, Attachment_Closed);
        }

        function Attachment_Closed() {
            return true;
        }

        function openETicket(ReqID, flightID) {
            var url = 'IssueTicket.aspx?requestid=' + ReqID + '&flightID=' + flightID;
            OpenPopupWindow('eTicket', 'Upload e-Ticket', url, 'popup', 550, 900, null, null, true, false, true, eTicket_Close);
        }

        function eTicket_Close() {
            return true;
        }

        function AddFlight(obj, id) {
            getFlightDetail();
            getOBJ("<%=cmdSaveFlight.ClientID%>").style.display = "block";
            getOBJ("<%=cmdUpdateFlight.ClientID%>").style.display = "none";
            getOBJ("<%=cmbTravelClass.ClientID%>").value = "Economy";
            getOBJ("<%=cmbFlightStatus.ClientID%>").value = "Confirm";
            getOBJ("hdQuoteID").value = id;
            //            $("#dvQuote").show();

            showModal('dvQuote');

        }

        function EditFlight(obj, id) {
            var tr = obj.parentElement.parentElement;

            getOBJ("<%=cmdSaveFlight.ClientID%>").style.display = "none";
            getOBJ("<%=cmdUpdateFlight.ClientID%>").style.display = "block";

            getOBJ("<%=txtFlight.ClientID%>").value = tr.cells[1].innerText.trim();
            getOBJ("<%=txtFrom.ClientID%>").value = tr.cells[2].innerText.trim();
            getOBJ("<%=txtTo.ClientID%>").value = tr.cells[3].innerText.trim();
            getOBJ("<%=txtDeparureDate.ClientID%>").value = tr.cells[4].innerText.trim();
            getOBJ("<%=cmbDepHours.ClientID%>").value = tr.cells[5].innerText.trim();
            getOBJ("<%=cmbDepMins.ClientID%>").value = tr.cells[6].innerText.trim();
            getOBJ("<%=txtArrivalDate.ClientID%>").value = tr.cells[7].innerText.trim();
            getOBJ("<%=cmbArrHours.ClientID%>").value = tr.cells[8].innerText.trim();
            getOBJ("<%=cmbArrMins.ClientID%>").value = tr.cells[9].innerText.trim();
            getOBJ("<%=cmbTravelClass.ClientID%>").value = tr.cells[10].innerText.trim();
            getOBJ("<%=cmbFlightStatus.ClientID%>").value = tr.cells[11].innerText.trim();
            getOBJ("<%=txtAirlineLocator.ClientID%>").value = tr.cells[12].innerText.trim();

            getOBJ("<%=txtFlightRemark.ClientID%>").value = tr.cells[13].all[0].value;

            getOBJ("hdFlightID").value = id;
            //            $("#dvQuote").show();

            showModal('dvQuote');

        }

  function ConfirmDeleteQuote()
        {
            var con=confirm('This quote will be deleted permanently . Do you want to continue with deletion ?') ;
            if(con) 
            return true ;
           else
           return false;
        }

        function EditQuotation(obj, id) {
    
            var RequestID=<%=Request.QueryString["RequestID"]%>;

            var url = 'AddQuotation.aspx?requestid=' +RequestID + "&QuoteID=" + id.toString() + "&IsSeaman=" +getOBJ("<%=hdfIsSeaman.ClientID%>").value; 
            OpenPopupWindow('AddQuotation', 'Add Quotation', url ,  'popup', 650, 1000, null, null, true, false, true, newQuote_Closed);
        }

        function OnSubmitQuotation(id) {


            $.alerts.okButton = " Yes ";
            $.alerts.cancelButton = " No ";

          
            var strMsg = "You are Submitting this quote  !" + "\n\n"
                             + "Once submitted, you will not be able to modify same." + "\n"
                             + "Do you want to continue ?";

            var aa = jConfirm(strMsg, ' Confirmation Required !', function (r) {

                if (r) {

                    var postBackstr = "__doPostBack('" + id.replace(/_/gi, '$') + "','" + id.replace(/_/gi, '$') + "_Click')";
                    window.setTimeout(postBackstr, 0, 'JavaScript');
                    return true;


                }
                else {
                    return false;
                }
            }

            );

            return false;
        }


 

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<div id="blur-on-updateprogress">
        &nbsp;</div>--%>
    <div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: bottom;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
        padding: 0px; background-color: #F6CEE3; text-align: center; font-weight: bold">
        <div>
            <asp:Label ID="lblPageTitle" runat="server" Text="Submit Quotation&nbsp;-&nbsp;"></asp:Label><asp:Literal
                ID="ltRequest" runat="server" Text=""></asp:Literal>;&nbsp;&nbsp;&nbsp;Quotation
            Due Date :&nbsp;&nbsp;&nbsp;
            <asp:Literal ID="ltQuotationDate" runat="server" Text=""></asp:Literal>
            &nbsp;&nbsp; <span style="background-color: Yellow; height: 25px; padding: 2px 10px 2px 10px">
                <asp:Label ID="lblSeamanStatus" runat="server"></asp:Label>
            </span>
            <asp:HiddenField ID="hdfIsSeaman" runat="server" />
        </div>
    </div>
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 0px; min-height: 620px; overflow: auto;">
        <asp:ScriptManagerProxy ID="SMRFQ" runat="server" />
        <input type="hidden" id="hdQuoteID" name="hdQuoteID" />
        <input type="hidden" id="hdFlightID" name="hdFlightID" />
        <table width='100%'>
            <tr>
                <td align="left">
                    <asp:Repeater ID="rptRequest" DataMember="Request" runat="server" OnItemDataBound="rptRequest_OnItemDataBound">
                        <HeaderTemplate>
                            <table cellpadding="1px" cellspacing="1px" style="width: 45%; font-size: x-small;"
                                border="0">
                                <tr class="grid-row-header">
                                    <td style="width: 50px;">
                                        Req ID
                                    </td>
                                    <td style="width: 100px;">
                                        Vessel
                                    </td>
                                    <td style="width: 100px;">
                                        Vessel Flag
                                    </td>
                                    <%--  <td style="width: 160px;">
                            Route
                        </td>
                        <td style="width: 90px;">
                            Departure Date
                        </td>--%>
                                    <td style="width: 120px;">
                                        Travel Type
                                    </td>
                                    <%--   <td style="width: 90px;">
                            Quote Due Date
                        </td>--%>
                                    <%--  <td style="width: 50px;">
                            No.Of Pax
                        </td>--%>
                                    <%--  <td style="width: 140px;">
                            Requested By
                        </td>--%>
                                    <%--   <td style="width: 50px;">
                            Status
                        </td>--%>
                                    <%--   <td style="width: 50px;">
                            Attach
                        </td>--%>
                                    <td>
                                        Remarks
                                    </td>
                                    <td style="width: 120px;">
                                        Sea man?
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="grid-row-data">
                                <td>
                                    <%#Eval( "id")%>
                                </td>
                                <td>
                                    <%#Eval("vessel_name")%>
                                </td>
                                <td>
                                    <%#Eval("VslFlag")%>
                                </td>
                                <%--    <td>
                        <%#Eval("FlightRoute")%>
                    </td>
                    <td align="center">
                        <%#Eval("departureDate","{0:dd/MM/yyyy}")%> &nbsp; <%#Eval("preferredDepartureTime")%> 
                    </td>--%>
                                <td>
                                    <%#Eval("classOfTravel")%>
                                </td>
                                <%--    <td align="center">
                        <%#Eval("QuoteDueDate", "{0:dd/MM/yyyy}")%>
                    </td>--%>
                                <%--    <td>
                        <%#Eval("PaxCount")%>
                    </td>--%>
                                <%--   <td>
                        <%#Eval("created_by")%>
                    </td>--%>
                                <%--   <td>
                        <asp:Label ID="lblCurrentStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "currentstatus")%>'></asp:Label>
                    </td>--%>
                                <%--     <td style="text-align: center;">
                        <img src="images/attach-icon.gif" class="clickable" alt="Attach documents" onclick='openAttachments(<%#DataBinder.Eval(Container.DataItem, "id")%>, "DOCUMENT")' />
                    </td>--%>
                                <td style="text-align: center;">
                                    <asp:Image ID="imgRemark" runat="server" ImageUrl="images/remarks.gif" />
                                </td>
                                <td style="text-align: center;">
                                    <%#Eval("isSeaman")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
                <td align="left">
                    <table border="1" style="border-collapse: collapse">
                        <tr>
                            <th colspan="2" style="background-color: #125699; color: White">
                                Requestor Details
                            </th>
                        </tr>
                        <tr>
                            <td align="right" style="padding-right: 4px; font-weight: bold">
                                Name :
                            </td>
                            <td align="left" style="padding-left: 4px">
                                <asp:Label ID="lblRequestorName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-right: 4px; font-weight: bold">
                                E-Mail :
                            </td>
                            <td align="left" style="padding-left: 4px">
                                <asp:Label ID="lblRequestorEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-right: 4px; font-weight: bold">
                                Mobile :
                            </td>
                            <td align="left" style="padding-left: 4px">
                                <asp:Label ID="lblRequestorMobile" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <span style="font-weight: 700; text-decoration: underline; background-color: #FFFFCC">
            Pax Details </span>
        <br />
        <asp:Repeater ID="rptPax" runat="server">
            <HeaderTemplate>
                <table cellpadding="1px" cellspacing="1px" style="width: 100%;" border="0">
                    <tr class="grid-row-header">
                        <td align="left">
                            <span>Code</span>
                        </td>
                        <td align="left">
                            <span>Pax Name</span>
                        </td>
                        <td>
                            <span>Gender</span>
                        </td>
                        <td align="left">
                            Passport No
                        </td>
                        <td align="left">
                            Passport Expiry
                        </td>
                        <td align="left">
                            Place Of Issue
                        </td>
                        <td align="left">
                            Nationality
                        </td>
                        <td align="left">
                            <span>Rank</span>
                        </td>
                        <td align="left">
                            <span>DOB</span>
                        </td>
                        <td align="left">
                            <span>Place Of Birth</span>
                        </td>
                        <td align="left">
                            Passport
                        </td>
                        <td align="left">
                            SeamenBook
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class='grid-row-data'>
                    <td align="left">
                        <%# Eval("staff_code")%>
                    </td>
                    <td align="left">
                        <%# "<b style='text-decoration:underline;color:#12124E'>" + Eval("StaffFullName").ToString().Split(' ')[0] + "</b>" + Eval("StaffFullName").ToString().Substring(Eval("StaffFullName").ToString().Split(' ')[0].Length)%>
                    </td>
                    <td>
                        <%# Eval("Staff_Gender")%>
                    </td>
                    <td align="left">
                        <%# Eval("Passport_Number")%>
                    </td>
                    <td align="left">
                        <%# Eval("Passport_Expiry_Date")%>
                    </td>
                    <td align="left">
                        <%#Eval("Passport_PlaceOf_Issue")%>
                    </td>
                    <td align="left">
                        <%# Eval("Staff_Nationality")%>
                    </td>
                    <td align="left">
                        <%# Eval("CurrentRank")%>
                    </td>
                    <td align="left">
                        <%# Eval("Staff_Birth_Date")%>
                    </td>
                    <td align="left">
                        <%#Eval("Staff_Born_Place")%>
                    </td>
                    <td align="center">
                        <asp:HyperLink ID="hlnkPassport" ImageUrl="~/Images/passport_trv.gif" runat="server"  
                        NavigateUrl='<%#      System.IO.File.Exists(Server.MapPath("~/Uploads/CrewDocuments/"+Eval("PassportFileName").ToString()))?"~/Uploads/CrewDocuments/"+Eval("PassportFileName").ToString():"~/FileNotFound.aspx"     %>'
                            Target="_blank"  />
                    </td>
                    <td align="center">
                        <asp:HyperLink ID="hlnkSeamenBook" ImageUrl="~/Images/Seamenbook_trv.png" runat="server" 
                        NavigateUrl='<%#      System.IO.File.Exists(Server.MapPath("~/Uploads/CrewDocuments/"+Eval("SeamanBookFileName").ToString()))?"~/Uploads/CrewDocuments/"+Eval("SeamanBookFileName").ToString():"~/FileNotFound.aspx"     %>'
                            Target="_blank"  />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <br />
        <span style="font-weight: 700; text-decoration: underline; background-color: #FFFFCC">
            Routes Details </span>
        <br />
        <asp:GridView ID="grdFlights" Width="800px" runat="server" AutoGenerateColumns="False"
            DataSourceID="objRequestFlights">
            <Columns>
                <asp:TemplateField HeaderText="From">
                    <ItemTemplate>
                        <asp:Label ID="lblFrom" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "travelOrigin")%>'></asp:Label><br />
                        <asp:Label ID="lblOriginAirport" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OriginAirport")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To">
                    <ItemTemplate>
                        <asp:Label ID="lblTo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "travelDestination")%>'></asp:Label><br />
                        <asp:Label ID="lblDestinationAirport" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DestinationAirport")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Departure">
                    <ItemTemplate>
                        <asp:Label ID="lblDep" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DepartureDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Return/Arrival">
                    <ItemTemplate>
                        <asp:Label ID="lblRetArr" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "returnDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pref. Airline">
                    <ItemTemplate>
                        <asp:Label ID="lblPAir" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PreferredAirline")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle HorizontalAlign="Left" CssClass="grid-row-header" />
            <RowStyle CssClass="grid-row-data" BackColor="White" />
        </asp:GridView>
        <br />
        <br />
        <table>
            <tr>
                <td style="display: none">
                    <b>Currency</b>&nbsp;<asp:DropDownList ID="cmbCurrency" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <%--     <input type="button" id="cmdAddMoreOption" onclick="newQuote()" style="margin: 10px;
                        color: Red; font-weight: bold;" value="Add New Quotation" />--%>
                    <asp:Button ID="btnAddQuotation" runat="server" Text="Add New Quotation" ForeColor="Red"
                        Font-Bold="true" OnClientClick="return newQuote();" OnClick="btnAddQuotation_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:UpdatePanel ID="updQuotes" runat="server">
            <ContentTemplate>
                <div id="dvNewQuotation" class="draggable" style="display: none; position: absolute;
                    z-index: 100; top: 200px; left: 200px; background-color: #f8f8f8; width: 40%;
                    color: black;">
                    <div style="border: 1px solid  #5588BB; padding: 2px; background-color: #5588BB;
                        color: #FFFFFF; text-align: center;">
                        <b>Edit Quotation </b>
                    </div>
                    <div class="dvpopup" style="width: 98%; margin: 5px; height: 200px; border: 1px solid Gray;">
                        <table id="tblQuote" cellpadding="2px" cellspacing="1px;" width="100%">
                            <tr class="header" style="color: #FFF;">
                                <td align="left" style="width: 8%">
                                    GDS-Locator<span style="color: Red;">&nbsp;*&nbsp;</span>
                                </td>
                                <td align="left" style="width: 12%">
                                    Deadline<span style="color: Red;">&nbsp;*&nbsp;</span>&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp;Hours&nbsp;&nbsp;&nbsp;Mins
                                </td>
                                <td align="left" style="width: 6%">
                                    Fare<span style="color: Red;">&nbsp;*&nbsp;</span>
                                </td>
                                <td align="left" style="width: 6%">
                                    Tax<span style="color: Red;">&nbsp;*&nbsp;</span>
                                </td>
                                <td align="left" style="width: 6%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtGDSLocator" runat="server" ForeColor="Blue" Width="120px" MaxLength="6"></asp:TextBox>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtTicketDeadline" runat="server" Width="100px" ForeColor="Blue"></asp:TextBox>
                                    <asp:DropDownList ID="cmbHours" runat="server" />
                                    <asp:DropDownList ID="cmbMins" runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="txtCal" TargetControlID="txtTicketDeadline" runat="server"
                                        Format="dd-MMM-yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFare" runat="server" onblur="return IsNumeric(this.value)" ForeColor="Blue"
                                        Width="60px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTax" runat="server" onblur="return IsNumeric(this.value)" ForeColor="Blue"
                                        Width="60px"></asp:TextBox>
                                </td>
                                <td style="text-align: center;">
                                    <asp:Button ID="cmdUpdateQuote" runat="server" UseSubmitBehavior="false" ToolTip="Update Quote"
                                        Text="Update" OnClick="cmdUpdateQuote_Click" />
                                    <asp:ImageButton ID="cmdUpdateQuote1" Visible="false" ImageUrl="images/Save.png"
                                        BorderWidth="0px" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <hr />
                        <table id="Table1" cellpadding="2px" cellspacing="1px;" width="100%">
                            <tr>
                                <td align="right" style="width: 18%; vertical-align: top;">
                                    Remark: &nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPNRText" runat="server" Width="99%" TextMode="MultiLine" Font-Size="12px"
                                        Font-Names="Tohama" Height="120"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <center>
                            <asp:Button ID="cmdSave" runat="server" Text="Add New Quotation" Width="120px" OnClick="cmdSave_Click" />
                        </center>
                    </div>
                </div>
                <div id='dvQuote' class="draggable" style="display: none; border: 1px solid gray;
                    position: absolute; z-index: 100; top: 200px; left: 200px; height: 280px; width: 850px;
                    color: black;">
                    <div class="dvpopup" style="width: 98%; margin: 5px; border: 1px solid Gray;">
                        <div style="border: 1px solid  #5588BB; padding: 2px; background-color: #5588BB;
                            color: #FFFFFF; text-align: center;">
                            <b>Add Flight </b>
                        </div>
                        <div>
                            <table cellpadding="2" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td align="right">
                                        From<span style="color: Red;">&nbsp;*&nbsp;</span> &nbsp;&nbsp; :&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFrom" runat="server" Width="200px"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="ACEFrom" TargetControlID="txtFrom" CompletionSetCount="10"
                                            MinimumPrefixLength="2" ServiceMethod="GetAirportList" ContextKey="" CompletionInterval="200"
                                            ServicePath="~/TravelService.asmx" runat="server" EnableCaching="true" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                    <td align="right">
                                        To<span style="color: Red;">&nbsp;*&nbsp;</span>&nbsp;&nbsp; :&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTo" runat="server" Width="200px"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="ACETo" TargetControlID="txtTo" CompletionSetCount="10"
                                            MinimumPrefixLength="2" ServiceMethod="GetAirportList" ContextKey="" CompletionInterval="200"
                                            ServicePath="~/TravelService.asmx" runat="server" EnableCaching="true" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            ShowOnlyCurrentWordInCompletionListItem="true">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Departure Date<span style="color: Red;">&nbsp;*&nbsp;</span>&nbsp;&nbsp; :&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDeparureDate" runat="server" Width="106px"></asp:TextBox>
                                        <asp:DropDownList ID="cmbDepHours" runat="server" />
                                        &nbsp
                                        <asp:DropDownList ID="cmbDepMins" runat="server" />
                                        <ajaxToolkit:CalendarExtender ID="calDep" TargetControlID="txtDeparureDate" runat="server"
                                            Format="dd-MMM-yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td align="right">
                                        Arrival Date &nbsp;&nbsp; :&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtArrivalDate" runat="server" Width="106px"></asp:TextBox>
                                        <asp:DropDownList ID="cmbArrHours" runat="server" />
                                        &nbsp;
                                        <asp:DropDownList ID="cmbArrMins" runat="server" />
                                        <ajaxToolkit:CalendarExtender ID="calArr" TargetControlID="txtArrivalDate" runat="server"
                                            Format="dd-MMM-yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Flight<span style="color: Red;">&nbsp;*&nbsp;</span>&nbsp;&nbsp; :&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFlight" runat="server" MaxLength="100" Width="200px"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtFlight"
                                            CompletionSetCount="10" MinimumPrefixLength="2" ServiceMethod="GetAirlineList"
                                            ContextKey="" CompletionInterval="200" ServicePath="~/TravelService.asmx" runat="server">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                    <td align="right">
                                        Locator<span style="color: Red;">&nbsp;*&nbsp;</span>&nbsp;&nbsp; :&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAirlineLocator" runat="server" MaxLength="6" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Class&nbsp;&nbsp; :&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbTravelClass" runat="server" Width="205px">
                                            <asp:ListItem Text="Economy" Value="Economy"></asp:ListItem>
                                            <asp:ListItem Text="Business" Value="Business"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        Status<span style="color: Red;">&nbsp;*&nbsp;</span>&nbsp;&nbsp; :&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbFlightStatus" runat="server" Width="205px">
                                            <asp:ListItem Text="Confirmed" Value="Confirm" />
                                            <asp:ListItem Text="Waitlisted" Value="Waitlist" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="vertical-align: top;">
                                        Remarks&nbsp;&nbsp; :&nbsp;&nbsp;
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtFlightRemark" runat="server" TextMode="MultiLine" Height="60px"
                                            Width="90%" Font-Size="12px" Font-Names="Tahoma"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="background-color: #f8f8f8;">
                            <hr />
                            <center>
                                <asp:Button ID="cmdSaveFlight" runat="server" Text="Save Flight" OnClick="cmdSaveFlight_Click" />&nbsp;&nbsp;
                                <asp:Button ID="cmdUpdateFlight" runat="server" Text="Update Flight" OnClick="cmdUpdateFlight_Click" />
                            </center>
                        </div>
                    </div>
                </div>
                <asp:Repeater ID="rptParent" DataMember="Quotation" runat="server" OnItemCommand="rptParent_ItemCommand"
                    OnItemDataBound="rptParent_OnItemDataBound">
                    <HeaderTemplate>
                        <table cellpadding="3px" cellspacing="0px" style="width: 100%; border: 1px solid gray;">
                            <tr class="header" style="color: #FFF; font-weight: bold">
                                <td style="width: 8%">
                                    GDS Locator
                                </td>
                                <td align="center" style="width: 9%">
                                    Ticketing Deadline
                                </td>
                                <td align="left" style="width: 8%">
                                    Hrs
                                </td>
                                <td align="left" style="width: 8%">
                                    Min
                                </td>
                                <td style="width: 8%;">
                                    Fare
                                </td>
                                <td style="width: 8%;">
                                    Tax
                                </td>
                                <td style="width: 8%">
                                    Total
                                </td>
                                <td style="width: 10%;">
                                    Currency
                                </td>
                                <td style="width: 8%;">
                                    Baggage
                                </td>
                                <td style="width: 8%;">
                                    Date Change
                                </td>
                                <td style="width: 8%;">
                                    Cancellation
                                </td>
                                  <td style="width:40px">
                                Remark / Quote date
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td  style="width: 25%;">
                                    &nbsp;
                                </td>
                              
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="background-color: #f8f8f8; color: blue;">
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "GDSLocator")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "TicketingDeadline")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "TimeHours")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "TimeMins")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "Fare")%>
                            </td>
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "Tax")%>
                            </td>
                            <td style="font-weight: bold;">
                                <%#DataBinder.Eval(Container.DataItem, "TotalFare")%>
                            </td>
                            <td style="font-weight: bold; text-align: center">
                                <%#Eval("Currency")%>
                            </td>
                            <td>
                                <%#Eval("Baggage_Charge")%>
                            </td>
                            <td>
                                <%#Eval("Date_Change_Charge")%>
                            </td>
                            <td>
                                <%#Eval("Cancellation_Charge")%>
                            </td>
                                <td style="text-align:center">
                               <img src="images/Trv-Remark-Evaluation.png"   class="clickable" alt="View/Add Remarks" title="cssbody=[dvbdy1] cssheader=[dvhdr1] header=[  <%# "Quoted on : "+   Convert.ToString(Eval("quotedate"))+"<br>"+ DataBinder.Eval(Container.DataItem, "Remarks").ToString()%> ]"
                                         <%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Remarks")).Trim()=="" && Convert.ToString(Eval("quotedate"))=="" ?"style='display:none'":"" %>   />
       
                            </td>
                            <td>
                                <asp:HiddenField ID="hdPNR" runat="server" Value='<%#Eval("Remarks")%>' />
                                <asp:ImageButton ID="DeleteQuote" runat="server" ImageAlign="AbsBottom" ImageUrl="~/Travel/images/delete.gif"
                                    Visible='<%# String.IsNullOrEmpty(Eval("quotedate").ToString()) && objUA.Edit != 0?true:false %>'
                                    CommandName="DELETEQUOTE" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "id")%>'
                                    OnClientClick=" return ConfirmDeleteQuote()" />
                            </td>
                            <td valign="bottom">
                                <img src="images/EditQuote.gif" class="clickable" title="Edit Quotation" alt="Edit Quotation"
                                    onclick='EditQuotation(this, <%#DataBinder.Eval(Container.DataItem, "id")%>);'
                                    style='display: <%#String.IsNullOrEmpty(Eval("quotedate").ToString()) && objUA.Edit != 0?"block":"none"%>;
                                    cursor: pointer;' />
                            </td>
                            <td >
                                <asp:Button ID="cmdSendQuote" CssClass="clickable" ForeColor="Navy" runat="server"
                                    OnClientClick="return OnSubmitQuotation(id);" CommandName="SENDQUOTE" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "id")%>'
                                    Text="Submit Quote" Visible='<%# objUA.Edit != 0?true:false  %>' />
                            </td>
                        
                        </tr>
                        <tr style="background-color: #556699; color: #FFF;">
                            <td style="background-color: #f8f8f8;">
                                &nbsp;
                            </td>
                            <td>
                                Flight
                            </td>
                            <td>
                                From
                            </td>
                            <td>
                                To
                            </td>
                            <td align="left" style="width: 5%">
                                Departure
                            </td>
                            <td align="left" style="width: 2%">
                                Hrs
                            </td>
                            <td align="left" style="width: 5%">
                                Min
                            </td>
                            <td align="left" style="width: 5%">
                                Arrival
                            </td>
                            <td align="left" style="width: 2%">
                                Hrs
                            </td>
                            <td align="left" style="width: 5%">
                                Min
                            </td>
                            <td>
                                Class
                            </td>
                            <td>
                                Status
                            </td>
                            <td>
                                Locator
                            </td>
                            <td>
                                Remarks
                            </td>
                            <td>
                                Action
                            </td>
                        </tr>
                        <asp:Repeater ID="rptChild" runat="server" DataSource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("QuotationFlight") %>'
                            OnItemCommand="rptChild_ItemCommand" OnItemDataBound="rptChild_OnItemDataBound">
                            <ItemTemplate>
                                <tr style="text-align: left;" id='child<%#((System.Data.DataRow)Container.DataItem)["quoteid"]%>'>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <%# ((System.Data.DataRow)Container.DataItem)["flightNo"]%>
                                    </td>
                                    <td>
                                        <%# ((System.Data.DataRow)Container.DataItem)["TravelFrom"]%>
                                    </td>
                                    <td>
                                        <%# ((System.Data.DataRow)Container.DataItem)["TravelTo"]%>
                                    </td>
                                    <td>
                                        <%# ((System.Data.DataRow)Container.DataItem)["DepartureDate"]%>
                                    </td>
                                    <td>
                                        <%# ((System.Data.DataRow)Container.DataItem)["TimeDepHours"]%>
                                    </td>
                                    <td>
                                        <%# ((System.Data.DataRow)Container.DataItem)["TimeDepMins"]%>
                                    </td>
                                    <td>
                                        <%# ((System.Data.DataRow)Container.DataItem)["ArrivalDate"]%>
                                    </td>
                                    <td>
                                        <%# ((System.Data.DataRow)Container.DataItem)["TimeArrHours"]%>
                                    </td>
                                    <td>
                                        <%# ((System.Data.DataRow)Container.DataItem)["TimeArrMins"]%>
                                    </td>
                                    <td>
                                        <%# ((System.Data.DataRow)Container.DataItem)["TravelClass"]%>
                                    </td>
                                    <td>
                                        <%# ((System.Data.DataRow)Container.DataItem)["FlightStatus"]%>
                                    </td>
                                    <td>
                                        <%# ((System.Data.DataRow)Container.DataItem)["AirlineLocator"]%>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdflightRemark" runat="server" Value='<%# ((System.Data.DataRow)Container.DataItem)["flightRemarks"]%>' />
                                        <asp:Image ID="imgflightRemark" runat="server" AlternateText="" ImageUrl="~/Travel/images/remarks.gif"
                                            ImageAlign="AbsMiddle" />
                                    </td>
                                    <td>
                                        <%if (CurrentStatus == "APPROVED")
                                          {%>
                                        <img src="images/e-ticket.gif" alt="Upload e-Ticket" class="clickable" onclick='openETicket(<%=RequestID%>, <%# ((System.Data.DataRow)Container.DataItem)["id"]%>);' />
                                        <%} %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="objRequestFlights" runat="server" SelectMethod="GetFlightByRequestID"
            TypeName="SMS.Business.TRAV.BLL_TRV_TravelRequest">
            <SelectParameters>
                <asp:Parameter Name="RequestID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <div class="subHeader" style="display: none; position: relative; right: 0px">
        <asp:Button ID="btnHiddenSubmit" runat="server" Text="btnHiddenSubmit" OnClick="btnHiddenSubmit_Click" />
    </div>
    <div id="dvPopUp" style="display: none; width: 1200px;" title=''>
        <iframe id="iFrmPopup" src="" frameborder="0" style="width: 100%; height: 600px">
        </iframe>
    </div>
    <asp:HiddenField ID="hdf_CompanyName" runat="server" />
</asp:Content>
