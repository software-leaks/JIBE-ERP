<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddQuotation.aspx.cs" Inherits="Travel_AddQuotation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Quotation</title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/EGSoft.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            color: Black;
            font-family: Tahoma;
            font-size: 11px;
            background-color: White;
        }
        
        
        
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 0px 2px 0px 3px;
            height: 20px;
            vertical-align: middle;
        }
        
        textarea
        {
            font-family: Tahoma;
            background-color: #FFFF99;
        }
        select
        {
            font-family: Tahoma;
            background-color: #FFFF99;
        }
        input[type=text]
        {
            font-family: Tahoma;
            background-color: #FFFF99;
        }
        .overlay-div-css
        {
            width: 100%;
            background-color: black;
            moz-opacity: 0.5;
            khtml-opacity: .5;
            opacity: .5;
            filter: alpha(opacity=50);
            z-index: 999;
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
            display: none;
        }
    </style>
  
</head>
<body>
    <form id="frmAddQuotation" runat="server">
    <script type="text/javascript">


        function ShowInsertDataFromAgents() {
            document.getElementById('dvFlightInfoFromOtherAgency').style.display = 'block';
            document.getElementById('Overlay-div').style.display = 'block';
            return false;

        }

        function HideInsertDataFromAgents() {

            document.getElementById('dvFlightInfoFromOtherAgency').style.display = 'none';
            document.getElementById('Overlay-div').style.display = 'none';
        }


        function AssignArrivalDate(DeptDT_id) {

            var DeptDT_Val = document.getElementById(DeptDT_id.id).value;
            var ArrDT_ID = DeptDT_id.id.replace('txtDeparureDate', 'txtArrivalDate');
            document.getElementById(ArrDT_ID).value = DeptDT_Val;


        }


    </script>
    <asp:ScriptManager ID="_sm" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress" style="z-index: 9999">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div style="margin: 3px">
        <table border="1" style="border-collapse: collapse" width="100%" cellpadding="7">
            <tr>
                <td style="width: 80px; font-weight: bold; font-size: 12px; color: Black">
                    Currency:
                </td>
                <td align="left">
                    <asp:DropDownList ID="cmbCurrency" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <span style="background-color: Yellow; height: 25px; padding: 2px 10px 2px 10px">
                        <asp:Label ID="lblSeamanStatus" Font-Bold="true" Font-Size="14px" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr style="padding-bottom: 10px">
                <td colspan="3" align="left">
                    <input type="button" value="Insert Amadeus data" style="font-size: 12px; font-weight: bold;
                        color: Blue;" onclick="ShowInsertDataFromAgents()" />
                </td>
            </tr>
        </table>
    </div>
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; background-color: White; padding: 0px; overflow: auto;
        width: 99%">
        <asp:UpdatePanel ID="_upd" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label ID="lblerrorMsg" runat="server" ForeColor="Red" Font-Size="12px"></asp:Label>
                    <asp:GridView ID="GrdFlight" Width="98%" AutoGenerateColumns="False" runat="server"
                        BackColor="LightGray" CellSpacing="1" CellPadding="3" GridLines="None" ShowFooter="true"
                        DataKeyNames="ID" OnRowDeleting="GrdFlight_RowDeleting" OnRowDataBound="GrdFlight_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Flight">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFlight" runat="server" MaxLength="100" Text='<%#Eval("Flight") %>'
                                        Width="70px" BackColor="#FFFF99"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtFlight" ValidationGroup="SaveColse"
                                        Display="None" ControlToValidate="txtFlight" runat="server" ErrorMessage="Please enter flight "></asp:RequiredFieldValidator>
                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtFlight"
                                        CompletionSetCount="10" MinimumPrefixLength="2" ServiceMethod="GetAirlineList"
                                        ContextKey="" CompletionInterval="200" ServicePath="~/TravelService.asmx" runat="server">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:ImageButton ID="cmdAddFlight" runat="server" ImageUrl="~/Travel/images/AddFlight.gif"
                                        BorderWidth="0px" AlternateText="Add new Flight" OnClick="cmdAddFlight_Click" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFrom" runat="server" Text='<%#Eval("From") %>' Width="50px" BackColor="#FFFF99"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtFrom" ValidationGroup="SaveColse"
                                        Display="None" ControlToValidate="txtFrom" runat="server" ErrorMessage="Please enter from"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:AutoCompleteExtender ID="ACEFrom" TargetControlID="txtFrom" CompletionSetCount="10"
                                        MinimumPrefixLength="2" ServiceMethod="GetAirportList" ContextKey="" CompletionInterval="200"
                                        ServicePath="~/TravelService.asmx" runat="server" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTo" runat="server" Text='<%#Eval("To") %>' Width="50px" BackColor="#FFFF99"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtTo" ValidationGroup="SaveColse"
                                        Display="None" ControlToValidate="txtTo" runat="server" ErrorMessage="Please enter to"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:AutoCompleteExtender ID="ACETo" TargetControlID="txtTo" CompletionSetCount="10"
                                        MinimumPrefixLength="2" ServiceMethod="GetAirportList" ContextKey="" CompletionInterval="200"
                                        ServicePath="~/TravelService.asmx" runat="server" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                    </ajaxToolkit:AutoCompleteExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Departure Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDeparureDate" runat="server" Text='<%#Eval("DepartureDate") %>'
                                        BackColor="#FFFF99" Width="80px" onchange="AssignArrivalDate(this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtDeparureDate" ValidationGroup="SaveColse"
                                        Display="None" ControlToValidate="txtDeparureDate" runat="server" ErrorMessage="Please enter Deparure Date"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:CalendarExtender ID="calDep" TargetControlID="txtDeparureDate" runat="server"
                                        Format="dd-MMM-yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:DropDownList ID="cmbDepHours" DataTextField="HrText" DataValueField="HrValue"
                                        runat="server" DataSourceID="ObjectDataSourceHour">
                                    </asp:DropDownList>
                                    <%-- <asp:RequiredFieldValidator ID="DeparureDatecmbDepHours" InitialValue="0" ValidationGroup="SaveColse"
                                        Display="None" ControlToValidate="cmbDepHours" runat="server" ErrorMessage="Please enter Deparure Date hour"></asp:RequiredFieldValidator>--%>
                                    <asp:DropDownList ID="cmbDepMins" DataSourceID="ObjectDataSourceMinute" DataTextField="MnText"
                                        DataValueField="MnValue" AppendDataBoundItems="true" runat="server" />
                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1cmbDepMins" InitialValue="0"
                                        ValidationGroup="SaveColse" Display="None" ControlToValidate="cmbDepMins" runat="server"
                                        ErrorMessage="Please enter Deparure Date minute"></asp:RequiredFieldValidator>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Arrival Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtArrivalDate" runat="server" Width="80px" Text='<%#Eval("ArrivalDate")%>'
                                        BackColor="#FFFF99"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtArrivalDate" ValidationGroup="SaveColse"
                                        Display="None" ControlToValidate="txtArrivalDate" runat="server" ErrorMessage="Please enter Arrival Date"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:CalendarExtender ID="calArr" TargetControlID="txtArrivalDate" runat="server"
                                        Format="dd-MMM-yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:DropDownList ID="cmbArrHours" DataSourceID="ObjectDataSourceHour" DataTextField="HrText"
                                        DataValueField="HrValue" runat="server" />
                                    <asp:DropDownList ID="cmbArrMins" DataSourceID="ObjectDataSourceMinute" DataTextField="MnText"
                                        DataValueField="MnValue" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Locator">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAirlineLocator" runat="server" MaxLength="6" Width="80px" Text='<%#Eval("Locator") %>'
                                        BackColor="#FFFF99"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtAirlineLocator" ValidationGroup="SaveColse"
                                        Display="None" ControlToValidate="txtAirlineLocator" runat="server" ErrorMessage="Please enter Locator"></asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Travel Class">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbTravelClass" runat="server" Text='<%#Eval("TravelClass") %>'
                                        Width="80px">
                                        <asp:ListItem Text="Economy" Value="Economy"></asp:ListItem>
                                        <asp:ListItem Text="Business" Value="Business"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FlightStatus">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbFlightStatus" Text='<%#Eval("FlightStatus") %>' runat="server"
                                        Width="90px">
                                        <asp:ListItem Text="Confirmed" Value="Confirm" />
                                        <asp:ListItem Text="Waitlisted" Value="Waitlist" />
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFlightRemark" runat="server" Font-Size="12px" Font-Names="Tahoma"
                                        Text='<%#Eval("Remarks")%>' TextMode="MultiLine" Width="200px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField DeleteImageUrl="~/Travel/Images/Delete.gif" ShowDeleteButton="true"
                                ButtonType="Image" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <RowStyle CssClass="RowStyle-css" BackColor="WhiteSmoke" />
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <FooterStyle BackColor="WhiteSmoke" />
                        <EmptyDataTemplate>
                            <span>No Flight Information Found</span>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSourceHour" TypeName="SMS.Business.TRAV.BLL_TRV_QuoteRequest"
                        SelectMethod="AddHour" runat="server"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSourceMinute" TypeName="SMS.Business.TRAV.BLL_TRV_QuoteRequest"
                        SelectMethod="AddMinute" runat="server"></asp:ObjectDataSource>
                </div>
                <br />
                <center>
                    <%--   <div style="border: 1px solid Gray;width:800px">--%>
                    <table id="tblQuote" cellpadding="2px" cellspacing="1px;" style="border: 1px solid gray;
                        font-size: 11px" width="700px">
                        <tr class="header" style="color: #FFF;">
                            <td align="left" style="width: 8%">
                                GDS-Locator
                            </td>
                            <td align="left" style="width: 18%">
                                Deadline &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Hours&nbsp;&nbsp;&nbsp;
                                &nbsp;Mins
                            </td>
                            <td align="left" style="width: 6%">
                                <b>Per Pax Fare</b>
                            </td>
                            <td align="left" style="width: 6%">
                                Tax
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtGDSLocator" runat="server" ForeColor="Blue" Width="120px" MaxLength="6"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvGDSLocator" runat="server" ErrorMessage="missing"
                                    ForeColor="Red" ControlToValidate="txtGDSLocator" ValidationGroup="SaveColse"></asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtTicketDeadline" runat="server" Width="100px" ForeColor="Blue"></asp:TextBox>
                                <asp:DropDownList ID="cmbHours" runat="server" />
                                <asp:DropDownList ID="cmbMins" runat="server" />
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorcmbHours" InitialValue="0"
                                    Display="None" runat="server" ErrorMessage="Please enter hour" ForeColor="Red"
                                    ControlToValidate="cmbHours" ValidationGroup="SaveColse">
                                </asp:RequiredFieldValidator>--%>
                                <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidatorcmbMins" runat="server" Display="None"
                                    InitialValue="0" ErrorMessage="Please enter minute" ForeColor="Red" ControlToValidate="cmbMins"
                                    ValidationGroup="SaveColse">
                                </asp:RequiredFieldValidator>--%>
                                <asp:RequiredFieldValidator ID="rfvDeadline" runat="server" ErrorMessage="missing"
                                    ForeColor="Red" ControlToValidate="txtTicketDeadline" ValidationGroup="SaveColse">
                                </asp:RequiredFieldValidator>
                                <ajaxToolkit:CalendarExtender ID="txtCal" TargetControlID="txtTicketDeadline" runat="server"
                                    Format="dd-MMM-yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtFare" runat="server" onchange="checkNumber(id)" Style="text-align: right"
                                    ForeColor="Blue" Width="60px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFare" runat="server" ErrorMessage="missing" ForeColor="Red"
                                    ControlToValidate="txtFare" ValidationGroup="SaveColse"></asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtTax" runat="server" onchange="checkNumber(id)" Style="text-align: right"
                                    ForeColor="Blue" Width="60px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTax" runat="server" ErrorMessage="missing" ForeColor="Red"
                                    ControlToValidate="txtTax" ValidationGroup="SaveColse"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div id="dvFlightInfoFromOtherAgency" style="display: none; position: absolute; left: 5%;
                        top: 10%; z-index: 10000; border: 2px solid maroon; background-color: White;
                        padding: 10px">
                        <table id="Table1" cellpadding="2px" cellspacing="1px;" width="90%">
                            <tr>
                                <td>
                                    <b style="color: Navy">Please paste data from Amadeus below .</b> &nbsp;
                                    <asp:Button ID="btnFillFlightDataForAmadeus" Text="Tabulate data" OnClick="btnFillFlightDataForAmadeus_Click"
                                        runat="server" />
                                    &nbsp;<input type="button" value="Close" onclick="HideInsertDataFromAgents();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtAmadeusContent" runat="server" Height="150px" Width="700px" BackColor="White"
                                        TextMode="MultiLine"></asp:TextBox><br />
                                    <span style="background-color: #FFFF4D; padding: 2px; color: Blue">----Examples----<br />
                                        <br />
                                        LO3828&nbsp;Q&nbsp;08FEB&nbsp;3&nbsp;GDNWAW HK1&nbsp;&nbsp;0535&nbsp;0630&nbsp;&nbsp;08FEB&nbsp;&nbsp;E&nbsp;&nbsp;LO/42YCR7<br />
                                        RO&nbsp;383&nbsp;W&nbsp;30MAR&nbsp;5&nbsp;OTPCDG&nbsp;HK1&nbsp;&nbsp;1300&nbsp;1510&nbsp;&nbsp;30MAR&nbsp;&nbsp;E&nbsp;&nbsp;RO/QTJYP<br />
                                        RO&nbsp;384&nbsp;W&nbsp;02APR&nbsp;1&nbsp;CDGOTP&nbsp;HK1&nbsp;&nbsp;1620&nbsp;2010&nbsp;&nbsp;02APR&nbsp;&nbsp;E&nbsp;&nbsp;RO/QTJYP
                                    </span>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--   </div>--%>
                    <div id="Overlay-div" class="overlay-div-css">
                        &nbsp;</div>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <center>
            <table width="100%" cellspacing="5">
                <tr>
                    <td valign="top" style="">
                        &nbsp;<br />
                        <br />
                        <table cellspacing="3">
                            <tr>
                                <td style="width: 140px" class="tdh">
                                    Baggage allowances:
                                </td>
                                <td style="width: 60px" class="tdd">
                                    <asp:TextBox ID="txtbaggageallowances" onchange="checkNumber(id)" Style="text-align: right"
                                        Width="80px" ForeColor="Blue" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdh" style="width: 120px">
                                    Date-Change penalty:
                                </td>
                                <td class="tdd">
                                    <asp:TextBox ID="txtDateChangeAllow" onchange="checkNumber(id)" Style="text-align: right"
                                        Width="80px" ForeColor="Blue" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdh" style="width: 120px">
                                    Cancellation Charges:
                                </td>
                                <td class="tdd">
                                    <asp:TextBox ID="txtCancellationCharge" onchange="checkNumber(id)" Style="text-align: right"
                                        Width="80px" ForeColor="Blue" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="left">
                        <table>
                            <tr>
                                <td align="left" class="tdd" style="padding-left: 10px">
                                    <b>Remark:</b><br />
                                    <br />
                                    <asp:TextBox ID="txtPNRText" runat="server" Width="600px" TextMode="MultiLine" Font-Size="12px"
                                        Font-Names="Tahoma" Height="120"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Button ID="cmdSave" runat="server" ValidationGroup="SaveColse" Text="Save & Close"
                Height="35px" OnClick="cmdSave_Click"  />
            <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="SaveColse" ShowSummary="false"
                ShowMessageBox="true" DisplayMode="List" runat="server" />
        </center>
    </div>

    <div>
      
    </div>
    </form>
</body>
</html>
