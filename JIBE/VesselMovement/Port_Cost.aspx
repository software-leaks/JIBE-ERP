<%@ Page Title="Port Cost" Language="C#" AutoEventWireup="true" CodeFile="Port_Cost.aspx.cs" MasterPageFile="~/Site.master" Inherits="VesselMovement_Port_Cost" %>

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
  <%--  <script src="../Scripts/VesselMovement.js" type="text/javascript"></script>--%>
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


        function Onfail(retval) {
            alert(retval._message);


        }


        function asyncBind_PortCost_M() {

            var PortID = document.getElementById('ddlPortCost').value;
            //            var AsOFDate = document.getElementById('txtAsofDate').value;
            //            var CrewStatus = document.getElementById('ddlCrewStatus').value;
            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncBind_PortCostList', false, { "PortID": PortID }, onSucc_LoadFunction, Onfail, new Array('dvPortCost', 'lblWebPortCost'));

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
                            Port Cost
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center" width="100%">
                        <div style="height: 100%; background-color: White;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table style="margin-top: 10px;" width="100%">
                                        <tr>
                                            <td valign="top" align="right" style="font-weight:bold" width="15%">
                                                Port Name :
                                            </td>
                                            <td valign="top" align="left" width="15%">
                                                <asp:DropDownList ID="ddlPortCost" runat="server" Width="150px" CssClass="txtInput">
                                                </asp:DropDownList>
                                            </td>

                                                    <td align="right" style="font-weight:bold" valign="top" width="15%">
                                                        Vessel Name :
                                                    </td>
                                                    <td align="left" valign="top" width="15%">
                                                        <asp:DropDownList ID="ddlvessel" runat="server" CssClass="txtInput" 
                                                            Width="150px">
                                                        </asp:DropDownList>
                                                    </td>
                                                                    <td align="right" style="font-weight:bold" width="15%">
                                                                        Start Date :
                                                                    </td>
                                                                    <td align="left" valign="top" width="10%">
                                                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtInput" Width="100px" onkeydown="return false;"></asp:TextBox>
                                                                        <ajaxToolkit:CalendarExtender ID="CalStartDate" runat="server" 
                                                                             TargetControlID="txtStartDate">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                    </td>
                                                                        <td align="right" style="font-weight:bold" width="15%">
                                                                            End Date :
                                                                        </td>
                                                                        <td align="left" valign="top" width="10%">
                                                                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtInput" Width="100px" onkeydown="return false;"></asp:TextBox>
                                                                            <ajaxToolkit:CalendarExtender ID="CalEndDate" runat="server" 
                                                                                 TargetControlID="txtEndDate">
                                                                            </ajaxToolkit:CalendarExtender>
                                                                             <%-- <asp:CompareValidator ID="dateValidator" 
                                                                              ControlToValidate="txtEndDate"
                                                                              Text="To date should be greater than From date"
                                                                              Operator="GreaterThanEqual"
                                                                              Type="Date" 
                                                                              runat="server" />--%>
                                                                        </td>
                                                                        <td align="left" valign="top" width="5%">
                                                                            <asp:ImageButton ID="ImgPortCost" runat="server" 
                                                                                ImageUrl="~/Images/SearchButton.png" OnClick="ImgPortCost_Click" 
                                                                                ToolTip="Search" OnClientClick="return ValidateText();" />
                                                                        </td>
                                                                        

                                        </tr>
                                            <tr>
                                            <td valign="top" align="right" style="font-weight:bold" width="15%">
                                                
                                            </td>
                                            <td valign="top" align="left" width="15%">
                                              
                                            </td>

                                                    <td align="right" style="font-weight:bold" valign="top" width="15%">
                                                       
                                                    </td>
                                                    <td align="left" valign="top" width="15%">
                                                     
                                                    </td>
                                                                    <td align="right" style="font-weight:bold" width="15%">
                                                                      
                                                                    </td>
                                                                    <td align="left" valign="top" width="10%">
                                                                      
                                                                    </td>
                                                                        <td align="right" style="font-weight:bold" width="15%">
                                                                            
                                                                        </td>
                                                                        <td align="left" valign="top" width="10%">
                                                                            
<%--                                                                              <asp:CompareValidator ID="dateValidator" 
                                                                              ControlToValidate="txtEndDate" ControlToCompare="txtStartDate"
                                                                              Text="To date should be greater than From date"
                                                                              Operator="GreaterThanEqual"
                                                                              Type="Date" 
                                                                              runat="server" CultureInvariantValues="false" />--%>

                                                                              <asp:CustomValidator runat="server" ID="datesValidator" OnServerValidate="DatesValidator_Validate" ErrorMessage="End Date should be greater than  or equal to Start Date"></asp:CustomValidator>
                                                                        </td>
                                                                        <td align="left" valign="top" width="5%">
                                                                            
                                                                        </td>
                                                                        

                                        </tr>
                                    </table>
                                    <table style="margin-top: 25px;" width="100%">
                                        <tr>
                                            <td>
                                                <div style="background: #cccccc;  padding: 3px; font-weight: 600">
                                                    Vessel Calls</div>
                                            </td>
                                            <td>
                                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                                    DA Items</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:GridView ID="gvPortCost" runat="server" EmptyDataText="NO RECORDS FOUND" ShowHeaderWhenEmpty="True"
                                                    AutoGenerateColumns="False" DataKeyNames="DA_ID" CellPadding="1" Width="100%"
                                                    OnRowDataBound="gvPortCost_RowDataBound">
                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Vessel">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPVessel_Name" runat="server" Text='<%# Eval("Vessel_Code")%>'></asp:Label></ItemTemplate>
                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="15%" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
<%--                                                        <asp:TemplateField HeaderText="Port Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPPORT_NAME" runat="server" Text='<%# Eval("PORT_NAME")%>'></asp:Label></ItemTemplate>
                                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Arrival">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPArrival" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Arrival"))) %>'></asp:Label></ItemTemplate>
                                                                
                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="12%" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Departure">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPDeparture" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Departure"))) %>'></asp:Label></ItemTemplate>
                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="12%" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Agent">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAgent" Visible="true" runat="server"  Text='<%# Eval("Agent_Name")%>'></asp:Label></ItemTemplate>
                                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="30%" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DA Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatus" Visible="true" runat="server"  Text='<%# Eval("DA_Status")%>'></asp:Label></ItemTemplate>
                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="15%" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DA Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" Visible="true" runat="server"  Text='<%#Eval("DA_Amount")%>'></asp:Label></ItemTemplate>
                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <HeaderTemplate>
                                                                Action
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table cellpadding="2" cellspacing="2">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="lbtnDAEdit" runat="server" CommandArgument='<%#Eval("[DA_ID]") + "," + Eval("[Agent_Code]") %>'
                                                                                OnCommand="lbtnDAEdit_Click" ImageUrl="~/Images/asl_view.png" Text="Edit">
                                                                            </asp:ImageButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5%" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                            <td valign="top" align="left" width="40%">
                                                <asp:GridView ID="gvDAItem" runat="server" EmptyDataText="NO RECORDS FOUND" ShowHeaderWhenEmpty="True" ShowFooter="true"
                                                    AutoGenerateColumns="False" DataKeyNames="DA_Item_ID" CellPadding="1" Width="100%" OnRowDataBound="gvDAItem_RowDataBound">
                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                    <FooterStyle CssClass="FooterStyle-css" /> 
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItem_Category" runat="server" Text='<%# Eval("Item_Category")%>'></asp:Label></ItemTemplate>
                                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="10%" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Proforma Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPerfome_Desc" runat="server" Text='<%# Eval("Proforma_Desc")%>'></asp:Label></ItemTemplate>
                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="25%" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                           <FooterTemplate>
                                                            <asp:Label ID="lbtotalProformaText" Text="Proforma Total :"  runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle  Wrap="True" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPamount" runat="server" Text='<%# Eval("Pamount")%>'></asp:Label></ItemTemplate>
                                                            <ItemStyle Wrap="True" HorizontalAlign="Center"  Width="15%"   CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="True"  HorizontalAlign="Center" />
                                                             <FooterStyle  Wrap="True" HorizontalAlign="Center" />
                                                            <FooterTemplate>
                                                            <asp:Label ID="lbtotalProformaAmt" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Final Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFinal_Desc" Visible="true" runat="server"  Text='<%# Eval("Final_Desc")%>'></asp:Label></ItemTemplate>
                                                            <ItemStyle Wrap="True" HorizontalAlign="Left" Width="20%" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                          <FooterTemplate>
                                                            <asp:Label ID="lbtotalFinalText" Text="Final Total :"  runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle  Wrap="True" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFAmount" Visible="true" runat="server" Text='<%# Eval("FAmount")%>'></asp:Label></ItemTemplate>
                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="15%" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbtotalFinalAmt" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                             <FooterStyle  Wrap="True" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Agent's Tariff">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" Visible="true" runat="server"  Text='<%# Eval("Item_Desc")%>'></asp:Label></ItemTemplate>
                                                            <ItemStyle Wrap="True" HorizontalAlign="Center" Width="15%" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="ImgPortCost" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
        </center>
    </div>
</asp:Content>