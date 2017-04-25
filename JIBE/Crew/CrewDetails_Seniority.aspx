<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_Seniority.aspx.cs"
    Inherits="Crew_CrewDetails_Seniority" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <div id="dvCrewSeniority">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlCompanySeniority" runat="server" Width="100%">
            <div style="text-align: right">
                <asp:HyperLink ID="lnkCompanySeniorityReward" runat="server" Target="_blank" Style="font-family: Tahoma ,Tahoma, Sans-Serif,vrdana;
                    font-size: 12px;" />
            </div>
            <fieldset style="text-align: left; margin: 0px; padding: 2px; font-family: Tahoma ,Tahoma, Sans-Serif,vrdana;
                font-size: 12px;">
                <legend><b>Company Seniority :</b> </legend>
                <table class="dataTable">
                    <tr>
                        <td>
                            Company Seniority :
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompanySeniorityYear" runat="server" Width="30px"></asp:TextBox>
                            Year
                            <asp:TextBox ID="txtCompanySeniorityDays" runat="server" Width="30px"></asp:TextBox>
                            Days
                            <asp:CompareValidator runat="server" ID="CompareValidator1" ErrorMessage="Enter only numeric value"
                                Operator="DataTypeCheck" Display="None" ValidationGroup="validate1" ControlToValidate="txtCompanySeniorityDays"
                                Type="Integer"></asp:CompareValidator>
                            <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="CompareValidator1"
                                runat="server">
                            </tlk4:ValidatorCalloutExtender>
                            <asp:CompareValidator runat="server" ID="CompareValidator2" ErrorMessage="Enter only numeric value"
                                Operator="DataTypeCheck" Display="None" ValidationGroup="validate1" ControlToValidate="txtCompanySeniorityYear"
                                Type="Integer"></asp:CompareValidator>
                            <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="CompareValidator2"
                                runat="server">
                            </tlk4:ValidatorCalloutExtender>
                        </td>
                        <td>
                            <asp:Button ID="btnEditCompanySeniority" runat="server" Text="Edit" OnClick="btnEditCompanySeniority_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnReverseCompanySeniority" runat="server" Text="Reverse" OnClick="btnReverseCompanySeniority_Click" />
                        </td>
                    </tr>
                    <tr runat="server" id="trCompanyEffectiveDate">
                        <td>
                            Effective Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompanyEffectiveDate"  CssClass="control-edit required" runat="server" Width="150px"></asp:TextBox>
                            <tlk4:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCompanyEffectiveDate">
                            </tlk4:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvCompanyDate" runat="server" ValidationGroup="validate1"
                                Display="None" ErrorMessage="Date is mandatory!" ControlToValidate="txtCompanyEffectiveDate"
                                InitialValue=""></asp:RequiredFieldValidator>
                            <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" TargetControlID="rfvCompanyDate"
                                runat="server">
                            </tlk4:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr runat="server" id="trCompanySeniorityRemarks">
                        <td>
                            Remarks
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompanySeniorityRemarks" TextMode="MultiLine" Width="350px" Height="50px"
                                runat="server" CssClass="control-edit required uppercase"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="validate1"
                                Display="None" ErrorMessage="Remarks is mandatory!" ControlToValidate="txtCompanySeniorityRemarks"
                                InitialValue=""></asp:RequiredFieldValidator>
                            <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1"
                                runat="server">
                            </tlk4:ValidatorCalloutExtender>
                        </td>
                        <td>
                            <asp:Button ID="btnSaveCompanySeniority" ClientIDMode="Static" runat="server" Text="Save" OnClick="btnSaveCompanySeniority_Click"
                                ValidationGroup="validate1" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancelCompanySeniority" runat="server" Text="Cancel" OnClick="btnCancelCompanySeniority_Click" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvCompanySeniority" runat="server" AllowSorting="False" AutoGenerateColumns="false"
                    GridLines="None" CellPadding="3" CellSpacing="1" Width="100%" CssClass="GridView-css">
                    <Columns>
                        <asp:TemplateField HeaderText="Seniority Year">
                            <ItemTemplate>
                                <asp:Label ID="lblSeniorityYear" runat="server" Text='<%# Eval("SeniorityYear") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="100px" CssClass="grid-col-fixed" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seniority Days">
                            <ItemTemplate>
                                <asp:Label ID="lblSeniorityDays" runat="server" Text='<%# Eval("SeniorityDays") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="100px" CssClass="grid-col-fixed" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Effective Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("EffectiveDate"))) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="LEFT" CssClass="grid-col-fixed" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="HeaderStyle-css" />
                    <PagerStyle CssClass="PagerStyle-css" />
                    <RowStyle CssClass="RowStyle-css" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                </asp:GridView>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnlRankSeniority" runat="server" Width="100%">
            <fieldset style="text-align: left; margin: 0px; padding: 2px; font-family: Tahoma ,Tahoma, Sans-Serif,vrdana;
                font-size: 12px;">
                <legend><b>Rank Seniority : </b></legend>
                <table  class="dataTable">
                    <tr>
                        <td>
                            Rank :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRank" runat="server" Width="115px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnEditRankSeniority" runat="server" Text="Edit" OnClick="btnEditRankSeniority_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnReverseRankSeniority" runat="server" Text="Reverse" OnClick="btnReverseRankSeniority_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Rank Seniority :
                        </td>
                        <td>
                            <asp:TextBox ID="txtRankSeniorityYear" runat="server" Width="30px"></asp:TextBox>Year
                            <asp:TextBox ID="txtRankSeniorityDays" runat="server" Width="30px"></asp:TextBox>Days
                            <asp:CompareValidator runat="server" ID="CompareValidator3" ErrorMessage="Enter only numeric value"
                                Operator="DataTypeCheck" Display="None" ValidationGroup="validate" ControlToValidate="txtRankSeniorityYear"
                                Type="Integer"></asp:CompareValidator>
                            <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="CompareValidator3"
                                runat="server">
                            </tlk4:ValidatorCalloutExtender>
                            <asp:CompareValidator runat="server" ID="CompareValidator4" ErrorMessage="Enter only numeric value"
                                Operator="DataTypeCheck" Display="None" ValidationGroup="validate" ControlToValidate="txtRankSeniorityDays"
                                Type="Integer"></asp:CompareValidator>
                            <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="CompareValidator4"
                                runat="server">
                            </tlk4:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr runat="server" id="trRankEffectiveDate">
                        <td>
                            Effective Date :
                        </td>
                        <td>
                            <asp:TextBox ID="txtRankEffectiveDate" CssClass="control-edit required" runat="server" Width="150px"></asp:TextBox>
                            <tlk4:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtRankEffectiveDate">
                            </tlk4:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfvRankDate" runat="server" ValidationGroup="validate"
                                Display="None" ErrorMessage="Date is mandatory!" ControlToValidate="txtRankEffectiveDate"
                                InitialValue=""></asp:RequiredFieldValidator>
                            <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="rfvRankDate"
                                runat="server">
                            </tlk4:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr runat="server" id="trRankSeniorityRemarks">
                        <td>
                            Remarks
                        </td>
                        <td>
                            <asp:TextBox ID="txtRankSeniorityRemarks" TextMode="MultiLine" Width="350px" Height="50px"
                                runat="server" CssClass="control-edit required uppercase"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRemarks" runat="server" ValidationGroup="validate"
                                Display="None" ErrorMessage="Remarks is mandatory!" ControlToValidate="txtRankSeniorityRemarks"
                                InitialValue=""></asp:RequiredFieldValidator>
                            <tlk4:ValidatorCalloutExtender ID="v1" TargetControlID="rfvRemarks" runat="server">
                            </tlk4:ValidatorCalloutExtender>
                        </td>
                        <td>
                            <asp:Button ID="btnSaveRankSeniority" runat="server" Text="Save" OnClick="btnSaveRankSeniority_Click"
                                ValidationGroup="validate" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancelRankSeniority" runat="server" Text="Cancel" OnClick="btnCancelRankSeniority_Click" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvRankSeniority" runat="server" AllowSorting="False" AutoGenerateColumns="false"
                    GridLines="None" CellPadding="3" CellSpacing="1" Width="100%" CssClass="GridView-css">
                    <Columns>
                        <asp:TemplateField HeaderText="Vessel">
                            <ItemTemplate>
                                <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Short_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rank">
                            <ItemTemplate>
                                <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seniority Year">
                            <ItemTemplate>
                                <asp:Label ID="lblSeniorityYear" runat="server" Text='<%# Eval("SeniorityYear") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seniority Days">
                            <ItemTemplate>
                                <asp:Label ID="lblSeniorityDays" runat="server" Text='<%# Eval("SeniorityDays") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Effective Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("EffectiveDate"))) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="LEFT" CssClass="grid-col-fixed" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="HeaderStyle-css" />
                    <PagerStyle CssClass="PagerStyle-css" />
                    <RowStyle CssClass="RowStyle-css" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                </asp:GridView>
            </fieldset>
        </asp:Panel>
    </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", "#btnSaveCompanySeniority", function () {
                if ($.trim($("#txtCompanyEffectiveDate").val())=="") {
                    alert("Enter Effective Date");
                    $("#txtCompanyEffectiveDate").focus();
                    return false;
                }
                if (IsInvalidDate($.trim($("#txtCompanyEffectiveDate").val()), window.parent.$("#hdnDateFromatMasterPage").val())) {
                    alert("Enter valid Effective Date<%= UDFLib.DateFormatMessage() %>");
                    $("#txtCompanyEffectiveDate").focus();
                    return false;
                }
            });

            $("body").on("click", "#btnSaveRankSeniority", function () {
                if ($.trim($("#txtRankEffectiveDate").val()) == "") {
                    alert("Enter Effective Date");
                    $("#txtRankEffectiveDate").focus();
                    return false;
                }
                if (IsInvalidDate($.trim($("#txtRankEffectiveDate").val()), window.parent.$("#hdnDateFromatMasterPage").val())) {
                    alert("Enter valid Effective Date<%= UDFLib.DateFormatMessage() %>");
                    $("#txtRankEffectiveDate").focus();
                    return false;
                }
            });
        });
    </script>
</body>
</html>
