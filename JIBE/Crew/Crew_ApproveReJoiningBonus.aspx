<%@ Page Title="Approve Re-Joining Bonus" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Crew_ApproveReJoiningBonus.aspx.cs" Inherits="Crew_Crew_ApproveReJoiningBonus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script type="text/javascript">
        function SetApprovalStatus(status) {
       
            if (status == 'APPROVED')
                document.getElementById("ctl00_MainContent_hdnStatus").value = 1;
            else if (status == 'REJECTED')
                document.getElementById("ctl00_MainContent_hdnStatus").value = 2;

            return true;
        }

        var lastExecutor = null;
        function ShowRJBFormula(Desc, evt, objthis) {

            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_RJBFormula', false, { "Description": Desc }, onSuccessASync_Get_AmountFormula, Onfail, new Array(evt, objthis));

            lastExecutor = service.get_executor();
        }
        function onSuccessASync_Get_AmountFormula(retVal, eventArgs) {
            js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
        }
        function Onfail(msg) {

            // alert(msg._message);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 33%;">
                </td>
                <td style="width: 34%; text-align: center; font-weight: bold;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Re-Joining Bonus"></asp:Label>
                </td>
                <td style="width: 33%; text-align: right;">
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
        <ContentTemplate>
            <div id="dvPageContent" class="page-content-main">
                <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                    color: Black; text-align: left; background-color: #fff;">
                    <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
                    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="BtnSearch">
                        <table border="0" cellpadding="2" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td>
                                    Rank
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRank" runat="server" Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td style="font-weight: bold; width:120px">
                                    Readiness date:
                                </td>
                                <td>
                                    From
                                    <asp:TextBox ID="txtSearchJoinFromDate" runat="server" Width="140px" 
                                        ClientIDMode="Static"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtSearchJoinFromDate">
                                    </ajaxToolkit:CalendarExtender> 
                                    To
                                    <asp:TextBox ID="txtSearchJoinToDate" runat="server" Width="140px" 
                                        ClientIDMode="Static"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSearchJoinToDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>                               
                                <td>
                                    Search
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchText" runat="server" Width="150px"></asp:TextBox>
                                </td>
                                <td> 
                                    <asp:Button ID="BtnSearch" runat="server" Width="75px" OnClick="BtnSearch_Click"
                                        Text="Search" CssClass="btnCSS" ClientIDMode="Static" /></td>
                            </tr>
                            <tr>
                                <td>
                                    Status
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStaus" runat="server" Width="150px">
                                        <asp:ListItem Value="0" Selected="True">ALL</asp:ListItem>
                                        <asp:ListItem Value="1">APPROVED</asp:ListItem>
                                        <asp:ListItem Value="2">REJECTED</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                               
                                 <td style="font-weight: bold; width:120px">
                                    Expected date:
                                </td>
                                <td>
                                    From
                                    <asp:TextBox ID="txtExpectedFromDate" runat="server" Width="140px" 
                                        ClientIDMode="Static"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtExpectedFromDate">
                                    </ajaxToolkit:CalendarExtender> 
                                    To
                                    <asp:TextBox ID="txtExpectedToDate" runat="server" Width="140px" 
                                        ClientIDMode="Static"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtExpectedToDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>


                                <td>
                                    Current Vessel
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCurrentVessel" runat="server" Width="150px">
                                    </asp:DropDownList>
                                </td>



                                <%--<td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    (Staff Code/Staff Name)
                                </td>--%>
                                <td><asp:Button ID="btnClearFilter" runat="server" Font-Names="Arial" Text="Clear Filter"
                                        CssClass="btnNewAddCSS" OnClick="btnClearFilter_Click" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <%--  <asp:UpdatePanel ID="UpdatePanel_Grid" runat="server">
            <ContentTemplate>--%>
                <div id="grid-container" style="margin-top: 2px">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3"
                        CellSpacing="0" Width="100%" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                        GridLines="None" DataKeyNames="ID" AllowPaging="false" AllowSorting="true" OnSorting="GridView1_Sorting"
                        CssClass="GridView-css" OnRowDataBound="GridView1_RowDataBound" ShowHeaderWhenEmpty="true">
                        <Columns>
                            <asp:TemplateField HeaderText="Staff Code" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CrewId")%>'
                                        Target="_blank" Text='<%# Eval("Staff_Code")%>' CssClass="staffInfo pin-it"></asp:HyperLink>
                                    <asp:Label ID="lblX" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="60px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Crew_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Vessel" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastVessel" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sign Off Dt" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSign_On_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Sign_Off_Date"))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Readiness Dt" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblReadiness_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Readiness_Date"))) %>' ></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Current Vessel" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrentVessel" runat="server" Text='<%# Eval("CurrVessel_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Expected Date Of Joining" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpectedDateOfJoining" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("CurrVessel_Joining_Date"))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Contract Duration (Days)">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastContractDuration" runat="server" Text='<%# Eval("LastContractDuration")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vacation Time (Days)" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblVacationDuration" runat="server" Text='<%# Eval("VacationDuration")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Calculated RJB Amount" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("CalculatedRJBAmount")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Entitled For RJB" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsEntitled" runat="server" Text='<%# Eval("IsEntitled")%>' ></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblApproved" runat="server" Text='<%# Eval("IsApproved")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Saved RJB Amount" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSavedAmount" runat="server" Text='<%# Eval("SavedRJBAmount")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                        CommandArgument='<%#Eval("[ID]") + "," + Eval("[CrewId]")%>' ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif"
                                        Height="16px"></asp:ImageButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="30px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                        <PagerStyle CssClass="PagerStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                    </asp:GridView>
                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindRJBGrid" />
                </div>
                <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
            </div>
            <div id="dvExport" title="Re-Joining Bonus" class="draggable" style="display: none;
                min-width: 500px; position: absolute; padding: 0px; text-align: left;">
                <div class="page-title" style="text-align: center; font-weight: bold; padding-top: 1%;
                    padding-bottom: 1%;">
                    Rejoining Bonus Details</div>
                <table border="0" style="width: 85%; padding: 1%">
                    <tr>
                        <td align="left" style="font-weight: bold">
                            Name
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-weight: bold">
                            Rank
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:Label ID="lblRank" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-weight: bold">
                            Last Vessel
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:Label ID="lblVessel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-weight: bold">
                            Sign Off Date
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:Label ID="lblSignOffDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-weight: bold">
                            Readiness Date
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:Label ID="lblReadinessDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-weight: bold">
                            Last Contract Duration(Days)
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:Label ID="lblLastContractDuration" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-weight: bold">
                            Vacation Duration(Days)
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:Label ID="lblVacationDuration" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-weight: bold">
                            Calculated Amount 
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:Label ID="lblRJBAmount" runat="server"></asp:Label>&nbsp;<asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                Height="16px" Width="16px" runat="server" onmouseover='ShowRJBFormula("RJBAmount_Calculation",event,this);'
                                AlternateText="info" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-weight: bold">
                            Actual Amount
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:TextBox ID="txtActAmount" runat="server" Width="110px"></asp:TextBox>
                            <asp:CompareValidator ID="ValidatortxtActAmount" runat="server" Type="Currency" Operator="DataTypeCheck"
                                Display="None" InitialValue="" ControlToValidate="txtActAmount" ErrorMessage="Please enter a valid Amount."
                                ValidationGroup="saveFZ">
                            </asp:CompareValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtActAmount" TargetControlID="ValidatortxtActAmount"
                                runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        <asp:LinkButton ID="lnkResetAmount" runat="server" CssClass="inline-edit" ForeColor="Blue" Font-Italic="true" Font-Underline="true"
                                    Text="Make Actual amount same as Calculated" OnClick="chkAmt_CheckedChanged" 
                                    ></asp:LinkButton>
                            <%--<asp:CheckBox ID="chkAmt" runat="server" Text="Same As Calculated Amount" AutoPostBack="true"
                                OnCheckedChanged="chkAmt_CheckedChanged" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr style="margin: 1%">
                        <td align="left" style="font-weight: bold">
                            Is Entitled
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:Label ID="lblEntitled" runat="server"></asp:Label>&nbsp;<asp:Image ID="Image1" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                Height="16px" Width="16px" runat="server"  onmouseover='ShowRJBFormula("RJBEligiblity_Calculation",event,this);'
                                AlternateText="info" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-weight: bold">
                            Is Approved
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:Label ID="lblApproved" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="font-weight: bold">
                            Remark
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemark" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:HiddenField ID="hdnCrewID" runat="server" />
                            <asp:HiddenField ID="hdnStatus" runat="server" />
                            <asp:HiddenField ID="hdnID" runat="server" />
                            <asp:HiddenField ID="objEdit" runat="server" />
                            <asp:HiddenField ID="objAdmin" runat="server" />
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" Visible="false" OnClientClick="SetApprovalStatus('APPROVED');"
                                OnClick="btnStatus_OnClick" ValidationGroup="saveFZ" />
                            <%--</td>
                <td>--%>
                            <asp:Button ID="btnReject" runat="server" Text="Reject" Visible="false" OnClientClick="SetApprovalStatus('REJECTED');"
                                OnClick="btnStatus_OnClick" ValidationGroup="saveFZ" />
                            <%--</td>
                <td>--%>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="false"
                                ValidationGroup="saveFZ" />
                            <asp:Button ID="btnRework" runat="server" Text="Rework" OnClick="btnRework_Click"
                                Visible="false" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="javascript:hideModal('dvExport');" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblmsg" runat="server" Font-Italic="true" ForeColor="Red" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        var strDateFormat = '<%=DFormat %>';
         $(document).ready(function () {
            $("body").on("click", "#BtnSearch", function () {

                if ($("#txtSearchJoinFromDate").length > 0) {
                    var date1 = document.getElementById("txtSearchJoinFromDate").value;
                    if ($.trim($("#txtSearchJoinFromDate").val()) != "") {
                        if (IsInvalidDate(date1, strDateFormat)) {
                            alert("Enter valid Readiness from date<%=TodayDateFormat %> ");
                            return false;
                        }
                    }

                }
                if ($("#txtSearchJoinToDate").length > 0) {
                    var date1 = document.getElementById("txtSearchJoinToDate").value;
                    if ($.trim($("#txtSearchJoinToDate").val()) != "") {
                        if (IsInvalidDate(date1, strDateFormat)) {
                            alert("Enter valid Readiness To date<%=TodayDateFormat %> ");
                            return false;
                        }
                    }

                }
                if ($("#txtExpectedFromDate").length > 0) {
                    var date1 = document.getElementById("txtExpectedFromDate").value;
                    if ($.trim($("#txtExpectedFromDate").val()) != "") {
                        if (IsInvalidDate(date1, strDateFormat)) {
                            alert("Enter valid Expected From date<%=TodayDateFormat %> ");
                            return false;
                        }
                    }

                }
                if ($("#txtExpectedToDate").length > 0) {
                    var date1 = document.getElementById("txtExpectedToDate").value;
                    if ($.trim($("#txtExpectedToDate").val()) != "") {
                        if (IsInvalidDate(date1, strDateFormat)) {
                            alert("Enter valid Expected To date<%=TodayDateFormat %> ");
                            return false;
                        }
                    }

                }

            });
        });
    
</script>
</asp:Content>
