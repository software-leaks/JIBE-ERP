<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_RefererDetails.aspx.cs"
    Inherits="Crew_CrewDetails_RefererDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <div>
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlView_RefererDetails" runat="server" Visible="false">
            <fieldset style="text-align: left; margin: 0px; padding: 2px; font-size: 11px;">
                <legend>Referrer Details :
                    <asp:LinkButton ID="lnkRefererDetails" runat="server" OnClick="lnkEditRefererDetails_Click"
                        Visible="true" CssClass="inline-edit" ForeColor="Blue"><font color=blue>[Add New Referrer Details]</font></asp:LinkButton></legend>
                <asp:GridView ID="gdRefererDetails" runat="server" DataKeyNames="ID,CrewID" AllowSorting="true"
                    CellPadding="3" GridLines="None" CellSpacing="1" Width="100%" Font-Size="11px"
                    AutoGenerateColumns="False" DataSourceID="objDS_ReferenceDetail" CssClass="GridView-css"
                    OnRowDataBound="gdRefererDetails_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label ID="lblPERSON_QUIERED_TITLE" runat="server" Text='<%# Bind("PERSON_QUIERED_TITLE")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPERSON_QUIERED_TITLE" runat="server" Text='<%# Bind("PERSON_QUIERED_TITLE")%>'
                                    Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTITLE" runat="server" ValidationGroup="noofdays"
                                    Display="None" ErrorMessage="Title is mandatory!" ControlToValidate="txtPERSON_QUIERED_TITLE"
                                    InitialValue=""></asp:RequiredFieldValidator>
                                <tlk4:ValidatorCalloutExtender ID="v1" TargetControlID="rfvTITLE" runat="server">
                                </tlk4:ValidatorCalloutExtender>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblREFERER_NAME" runat="server" Text='<%# Bind("REFERER_NAME")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtREFERER_NAME" runat="server" Text='<%# Bind("REFERER_NAME")%>'
                                    Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvName" runat="server" ValidationGroup="noofdays"
                                    Display="None" ErrorMessage="Name is mandatory!" ControlToValidate="txtREFERER_NAME"
                                    InitialValue=""></asp:RequiredFieldValidator>
                                <tlk4:ValidatorCalloutExtender ID="v2" TargetControlID="rfvName" runat="server">
                                </tlk4:ValidatorCalloutExtender>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mobile">
                            <ItemTemplate>
                                <asp:Label ID="lblREFERER_MOBILE" runat="server" Text='<%# Bind("REFERER_MOBILE")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtREFERER_MOBILE" runat="server" Text='<%# Bind("REFERER_MOBILE")%>'
                                    Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ValidationGroup="noofdays"
                                    Display="None" ErrorMessage="Mobile number is mandatory!" ControlToValidate="txtREFERER_MOBILE"
                                    InitialValue=""></asp:RequiredFieldValidator>
                                <tlk4:ValidatorCalloutExtender ID="v3" TargetControlID="rfvMobile" runat="server">
                                </tlk4:ValidatorCalloutExtender>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lblREFERENCE_DATE" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("REFERENCE_DATE"))) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtREFERENCE_DATE" CssClass="txtReferrerDate" runat="server" Text='<%#Bind("REFERENCE_DATE")%>'
                                    Width="100px"></asp:TextBox>
                                <tlk4:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtREFERENCE_DATE"
                                    Format='<%# Convert.ToString(Session["User_DateFormat"]) %>'>
                                </tlk4:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" ValidationGroup="noofdays"
                                    Display="None" ErrorMessage="Date is mandatory!" ControlToValidate="txtREFERENCE_DATE"
                                    InitialValue=""></asp:RequiredFieldValidator>
                                <tlk4:ValidatorCalloutExtender ID="v4" TargetControlID="rfvDate" runat="server">
                                </tlk4:ValidatorCalloutExtender>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Queried By">
                            <ItemTemplate>
                                <asp:Label ID="lblPERSON_QUIERED_NAME" runat="server" Text='<%# Bind("PERSON_QUIERED_NAME")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPERSON_QUIERED_NAME" runat="server" Text='<%# Bind("PERSON_QUIERED_NAME")%>'
                                    Width="120px" Enabled="false"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:ImageButton ID="LinkButton1" CssClass="lnkbtnUpdateReferrer" runat="server"
                                    ImageUrl="~/images/accept.png" CausesValidation="True" CommandName="Update" AlternateText="Update"
                                    ValidationGroup="noofdays"></asp:ImageButton>
                                <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/reject.png" CausesValidation="False"
                                    CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                    CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                                    CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                                    AlternateText="Delete"></asp:ImageButton>
                            </ItemTemplate>
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
        <asp:Panel ID="pnlAdd_RefererDetails" runat="server" Visible="false" Width="300px">
            <fieldset style="text-align: left; margin: 0px; padding: 2px; font-size: 11px; width=300px">
                <legend>Referrer Details :</legend>
                <table>
                    <tr>
                        <td>
                            Reference Check Date :
                        </td>
                        <td>
                            <div style="position: relative; z-index: 100;">
                                <asp:TextBox ID="txtReferenceDate" runat="server" Width="150px" CssClass="control-edit required"
                                    ClientIDMode="Static"></asp:TextBox>
                                <tlk4:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtReferenceDate">
                                </tlk4:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvtxtReferenceDate" runat="server" ValidationGroup="saveFZD"
                                    Display="None" ErrorMessage="Reference Check Date is mandatory" ControlToValidate="txtReferenceDate"
                                    InitialValue=""></asp:RequiredFieldValidator>
                                <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rfvtxtReferenceDate"
                                    runat="server">
                                </tlk4:ValidatorCalloutExtender>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Referrer Title :
                        </td>
                        <td class="data">
                            <asp:TextBox ID="txtPersonQuieredTitle" runat="server" CssClass="control-edit required uppercase"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvQuieredTitle" runat="server" ValidationGroup="saveFZD"
                                Display="None" ErrorMessage="Referrer Title is mandatory!" ControlToValidate="txtPersonQuieredTitle"
                                InitialValue=""></asp:RequiredFieldValidator>
                            <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="rfvQuieredTitle"
                                runat="server">
                            </tlk4:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Referrer Name :
                        </td>
                        <td class="data">
                            <asp:TextBox ID="txtRefererName" runat="server" CssClass="control-edit required uppercase"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtRefererName" runat="server" ValidationGroup="saveFZD"
                                Display="None" ErrorMessage="Referrer Name is mandatory" ControlToValidate="txtRefererName"
                                InitialValue=""></asp:RequiredFieldValidator>
                            <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtdtlEmailAdd" TargetControlID="rfvtxtRefererName"
                                runat="server">
                            </tlk4:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Referrer Mobile :
                        </td>
                        <td class="data">
                            <asp:TextBox ID="txtRefererPhoneNo" runat="server" CssClass="control-edit required uppercase"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRefererPhoneNo" runat="server" ValidationGroup="saveFZD"
                                Display="None" ErrorMessage="Mobile Number is mandatory" ControlToValidate="txtRefererPhoneNo"
                                InitialValue=""></asp:RequiredFieldValidator>
                            <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="rfvRefererPhoneNo"
                                runat="server">
                            </tlk4:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Queried By :
                        </td>
                        <td class="data">
                            <asp:TextBox ID="txtPersonQuieredName" runat="server" CssClass="control-edit required uppercase"
                                Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnRefererDetailSave" runat="server" Text="Save Referrer Details"
                                OnClick="btnSaveRefererDetail_Click" ValidationGroup="saveFZD" ClientIDMode="Static" />
                            <asp:Button ID="btnRefererDetailCancel" runat="server" Text="Cancel" OnClick="btnCancelRefererDetail_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
    <asp:ObjectDataSource ID="objDS_ReferenceDetail" runat="server" SelectMethod="Get_Crew_Referer_Details"
        TypeName="SMS.Business.Crew.BLL_Crew_CrewDetails" DeleteMethod="DEL_CrewReferenceDetail"
        UpdateMethod="Save_Crew_Reference_Details" OnDeleted="gdRefererDetails_Deleted"
        OnUpdating="ObjectDataSource2_Updating">
        <DeleteParameters>
            <asp:Parameter Name="CrewID" Type="Int32" />
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:SessionParameter Name="Deleted_By" SessionField="userid" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="" Name="CrewID" QueryStringField="CrewID"
                Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="CrewID" Type="Int32" />
            <asp:Parameter Name="REFERER_NAME" Type="String" />
            <asp:Parameter Name="REFERER_MOBILE" Type="String" />
            <asp:Parameter Name="REFERENCE_DATE" Type="String" />
            <asp:Parameter Name="PERSON_QUIERED_NAME" Type="String" />
            <%--<asp:SessionParameter Name="QUIERED_BY" SessionField="userid" Type="Int32" />--%>
            <asp:Parameter Name="PERSON_QUIERED_TITLE" Type="String" />
            <asp:SessionParameter Name="User_ID" SessionField="userid" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    </form>
</body>
<script type="text/javascript">
    var strDateFormat = '<%=DFormat %>';

    $(document).ready(function () {
        $("body").on("click", "#btnRefererDetailSave", function () {

            if ($("#txtReferenceDate").length > 0) {
                var date1 = document.getElementById("txtReferenceDate").value;
                if ($.trim($("#txtReferenceDate").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid Reference Check Date<%= UDFLib.DateFormatMessage() %>");
                        $("#txtReferenceDate").focus();
                        return false;
                    }
                }

            }
        });

        $("body").on("click", ".lnkbtnUpdateReferrer", function () {
            if ($.trim($(".txtReferrerDate").val()) != "") {
                if (IsInvalidDate($.trim($(".txtReferrerDate").val()), strDateFormat)) {
                    alert("Enter valid Date<%= UDFLib.DateFormatMessage() %>");
                    $(".txtReferrerDate").focus();
                    return false;
                }
            }
        });
    });
</script>
</html>
