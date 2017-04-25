<%@ Page Title="Port Call History" Language="C#" AutoEventWireup="true" CodeFile="PortCallHistory.aspx.cs"  MasterPageFile="~/Site.master"
    Inherits="VesselMovement_PortCallHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script type="text/javascript">

        var strDateFormat = '<%#DateFormat %>';



        function ValidateText() {
           
            var txtStartDate = $('#<%=txtStartDate.ClientID %>').val();

            var txtEndDate = $('#<%=txtEndDate.ClientID %>').val();


            if (IsInvalidDate(txtStartDate, strDateFormat)) {
                alert("Please Enter Valid Start Date.");
                document.getElementById("txtStartDate").focus();
                return false;
            }

            if (IsInvalidDate(txtEndDate, strDateFormat)) {
                alert("Please Enter Valid End Date.");
                document.getElementById("txtEndDate").focus();
                return false;
            }


        }

        function asyncBind_PortCallHistory_M() {

            var PortID = document.getElementById('ddlportfilter').value;
            var VesselID = document.getElementById('ddlvessel').value;
            var StartDate = document.getElementById('txtStartDate').value;
            var EndDate = document.getElementById('txtEndDate').value;
            var rdborder = "asc"; //document.getElementById('rdborder').value;
            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncBind_PortCallHistory', false, { "PortID": PortID, "Vessel_ID": VesselID, "StartDate": StartDate, "EndDate": EndDate, "rdborder": rdborder }, onSucc_LoadFunction, Onfail, new Array('dvPortCallHistory', 'lblWebPortCallHistory'));

            //lastExecutorMinMaxQty_M = service.get_executor();

        }

        function onSucc_LoadFunction(retval, prm) {
            try {
                document.getElementById(prm[0]).innerHTML = retval;

                checkForMyAction(prm[1], retval);
            }
            catch (ex)
            { }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
        <center>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td align="center">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            Port Call History
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <div style="background-color: White; 
                            max-height: 100%;">
                            <asp:UpdatePanel ID="Update1" runat="server">
                                <ContentTemplate>
                                    <table style="margin-top: 10px;">
                                        <tr>
                                            <td valign="top" align="right">
                                                Port Name :
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:DropDownList ID="ddlportfilter" runat="server" Width="200px" CssClass="txtInput">
                                                </asp:DropDownList>
                                            </td>
                                            <td valign="top" align="right">
                                                Vessel Name :
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:DropDownList ID="ddlvessel" runat="server" Width="200px" CssClass="txtInput">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                Start Date :
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtStartDate" runat="server" Width="120px"  onkeypress="return isNumberKey(event)" CssClass="txtInput"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalStartDate" runat="server" TargetControlID="txtStartDate">
                                             
                                                </ajaxToolkit:CalendarExtender>
<%--                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtStartDate" runat="server"  Display="None" ErrorMessage="Start date is blank" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtStartDate" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$"
                                                         ErrorMessage="Start Date : Invalid date format." Display="None" ValidationGroup="Group1" />--%>
                                            </td>
                                            <td align="right">
                                                End Date :
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtEndDate" runat="server" Width="120px" onkeypress="return isNumberKey(event)" CssClass="txtInput"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalEndDate" runat="server" TargetControlID="txtEndDate">
                                                   

                                                </ajaxToolkit:CalendarExtender>
<%--                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEndDate" runat="server"  Display="None" ErrorMessage="End date is blank" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator1"  Display="None" runat="server" ControlToValidate="txtEndDate" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$"
                                                         ErrorMessage="End Date : Invalid date format." ValidationGroup="Group1" />--%>
                                            </td>
                                            <td valign="top" align="left">
                                                <%--  <asp:ImageButton ID="btnportfilter" runat="server" OnClick="btnportfilter_Click"
                                                                    ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />--%>
                                                <asp:ImageButton ID="btnportfilter" runat="server" ToolTip="Search" 
                                                    ImageUrl="~/Images/SearchButton.png" OnClientClick="return ValidateText();" onclick="btnportfilter_Click"   />
<%--                                                <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Group1"  />--%>
                            <%--<asp:CompareValidator ID="CompareValidator1"  runat="server" 
                                            ControlToValidate = "txtEndDate" ControlToCompare = "txtStartDate" Operator ="GreaterThanEqual" Type = "Date"
                                            ErrorMessage="End date must be more than Start date." Display="None" ValidationGroup="Group1" ></asp:CompareValidator>--%>
                                             <asp:CustomValidator runat="server" ID="datesValidator"  
                                                      ErrorMessage="End Date should be greater than  or equal to Start Date" 
                                                     OnServerValidate="DatesValidator_Validate"></asp:CustomValidator>
                                            </td>

                                        </tr>
                                        <tr id="TR1" runat="server" visible="false">
                                            <td valign="top" align="right">
                                                Sort Arrival in :
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:RadioButtonList ID="rdborder" Width="200px" CssClass="txtInput" RepeatDirection="Horizontal"
                                                    runat="server">
                                                    <asp:ListItem Value="desc" Selected="True" Text="Descending"></asp:ListItem>
                                                    <asp:ListItem Value="asc" Text="Ascending"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td valign="top" align="right">
                                            </td>
                                            <td valign="top" align="left">
                                            </td>
                                            <td valign="top" align="right">
                                            </td>
                                            <td valign="top" align="left">
                                            </td>
                                            <td valign="top" align="left">
                                            
                                         <%--   <asp:CompareValidator ID="dateValidator" 
                                              ControlToValidate="txtEndDate"
                                              Text="To date should be greater than From date"
                                              Operator="GreaterThanEqual"
                                              Type="Date" 
                                              runat="server" ValidationGroup="Group1"  />--%>
                                             
                                              
                                            </td>
                                          <td valign="top" align="right">
                                            
                                              
                                              
                                            </td>
                                              <td valign="top" align="right">
                                            

                                              
                                            </td>
                                           
                                        </tr>
                                    </table>
                                </ContentTemplate>
                        <Triggers>
                        <asp:PostBackTrigger ControlID="btnportfilter" />
                    </Triggers>
                            </asp:UpdatePanel>
                            <table width="80%">
                                <tr>
                                    <td align ="center">
                                        <asp:Label ID="lblWebPortCallHistory" Title="Port Call History" runat="server" Width="100%"> <div style=" border: 1px solid #cccccc;">
                                             <div id="dvPortCallHistory">
                                             </div>
                                        </div>
                                        </asp:Label>
                                         <asp:GridView ID="gvPortCallHistory" runat="server" EmptyDataText="NO RECORDS FOUND" AllowPaging="true" PageSize="50"
                                                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1"
                                                                Width="100%" OnPageIndexChanging="gvPortCallHistory_PageIndexChanging">
                                                                <HeaderStyle CssClass="HeaderStyle-css" />
                                                                <RowStyle CssClass="RowStyle-css" />
                                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Vessel Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVessel_Name" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label></ItemTemplate>
                                                                        <ItemStyle Wrap="True" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Port Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPORT_NAME" Visible="true" runat="server" Width="250px" Text='<%# Eval("PORT_NAME")%>'></asp:Label></ItemTemplate>
                                                                        <ItemStyle Wrap="True" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Arrival">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblArrival" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Arrival")))%>'></asp:Label></ItemTemplate>
                                                                            
                                                                        <ItemStyle Wrap="True" HorizontalAlign="Center" Width="200px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Departure">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDeparture" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Departure")))%>'></asp:Label></ItemTemplate>
                                                                        <ItemStyle Wrap="True" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Port Remarks">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPort_Remarks" Visible="true" runat="server" Width="250px" Text='<%# Eval("Port_Remarks")%>'></asp:Label></ItemTemplate>
                                                                        <ItemStyle Wrap="True" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ShipCraneReq">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblShipCraneReq" Visible="true" runat="server" Width="40px" Text='<%# Eval("ShipCraneReq")%>'></asp:Label></ItemTemplate>
                                                                        <ItemStyle Wrap="True" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </center>
    </div>
</asp:Content>