<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_Services.aspx.cs"
    Inherits="Crew_CrewDetail_Services" %>

<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr15" runat="server">
    </asp:ScriptManager>
    <div id="dvCrewOtherServiceGrid">
        <asp:UpdatePanel ID="UpdatePanel_Msg" runat="server">
            <ContentTemplate>
                <div class="error-message">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Panel ID="pnlView_Voyages" runat="server" Visible="false">
            <table style="width: 100%" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: right">
                        <div id="dvVoyageInfo" style="background-color: Yellow; font-weight: bold; text-align: center;
                            width: 200px; float: right;">
                        </div>
                    </td>
                    <td style="width: 120px; text-align: right;">
                        <asp:ImageButton runat="server" ID="ImgAddService" ImageUrl="~/Images/AddService.png"
                            OnClientClick="AddOtherServices($('[id$=HiddenField_CrewID]').val());return false;" />
                    </td>
                    <td style="width: 20px; text-align: right;">
                        <asp:ImageButton ToolTip="Refresh" runat="server" ID="ImgReloadVoyage" ImageUrl="~/Images/reload.png"
                            OnClientClick="GetOtherServices($('[id$=HiddenField_CrewID]').val());return false;" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView_Voyages" runat="server" DataKeyNames="ID" AllowSorting="False"
                AutoGenerateColumns="false" GridLines="None" CellPadding="3" CellSpacing="1"
                Width="100%" CssClass="GridView-css">
                <Columns>
                    <asp:TemplateField HeaderText="Joining Rank" SortExpression="Rank_Short_Name" >
                        <ItemTemplate>
                            <asp:Label ID="lblJoiningRank" runat="server" Text='<%# Eval("Rank_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date From" SortExpression="Date_From" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblDateFrom" runat="server" Text='<%# Eval("Date_From","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date To" SortExpression="Sign_To" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblDateTo" runat="server" Text='<%# Eval("Date_To","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Service Type" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblServiceType" runat="server" Text='<%# Eval("Service_Type") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="lnkEditVoyage" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif"
                                CausesValidation="False" AlternateText="Edit" OnClientClick='<%#"EditOtherServices(" + Eval("CrewID").ToString()+ "," + Eval("ID").ToString() +"); return false;" %>'>
                            </asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="lnkDeleteVoyage" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
                                CausesValidation="False" OnClientClick='<%#"DeleteOtherServices(" + Eval("CrewID").ToString()+ "," + Eval("ID").ToString() + "," + GetSessionUserID().ToString() + "); return false;" %>'
                                AlternateText="Delete"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Image ID="imgRecordInfo" ToolTip="Info" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_DTL_OtherServices&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>'
                                AlternateText="info" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="HeaderStyle-css" />
                <PagerStyle CssClass="PagerStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <EditRowStyle CssClass="EditRowStyle-css" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="pnlAdd_Voyages" runat="server" Visible="false" >
          <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>--%>
                <fieldset style="text-align: left; margin: 0px; padding: 10px; margin: 10px; font-family: Tahoma; font-size: 12px;color: Black; ">
                        <legend>ADD Service:</legend>
                    <table border="0" cellpadding="3" cellspacing="0" width="100%">
                        <tr class="highlight_jtype">
                            <td style=" width:25%">
                                Service Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlServiceType" runat="server" Width="156px" DataTextField="Service_Type"
                                    DataValueField="SCode">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlServiceType" runat="server"
                                    ValidationGroup="saveFZ" Display="None" ErrorMessage="Please select Service Type !"
                                    ControlToValidate="ddlServiceType" InitialValue="0"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidatorddlServiceType"
                                    runat="server">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Joining Rank:
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLJoiningRank" runat="server" Width="156px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDDLJoiningRank" runat="server"
                                    ValidationGroup="saveFZ" Display="None" ErrorMessage="Please select Joining Rank !"
                                    ControlToValidate="DDLJoiningRank" InitialValue="0"></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidatorDDLJoiningRank"
                                    runat="server">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <%-- <asp:ObjectDataSource ID="ObjDataSourceDDLJoinRank" runat="server" SelectMethod="Get_RankList"
                                TypeName="SMS.Business.Crew.BLL_Crew_Admin"></asp:ObjectDataSource>DataSourceID="ObjDataSourceDDLJoinRank"DataTextField="Rank_Short_Name" DataValueField="ID" AppendDataBoundItems="true"--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Date From:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDateFrom" runat="server" Width="150px" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDateFrom"
                                    Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtDateFrom" runat="server"
                                    ValidationGroup="saveFZ" Display="None" ErrorMessage="Please enter Date From !"
                                    ControlToValidate="txtDateFrom" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="dateValidatortxtDateFrom" runat="server" Type="Date" Operator="DataTypeCheck"
                                    Display="None" InitialValue="" ControlToValidate="txtDateFrom" ErrorMessage="Please enter a valid date."
                                    ValidationGroup="saveFZ">
                                </asp:CompareValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtDateFrom" TargetControlID="RequiredFieldValidatortxtDateFrom"
                                    runat="server">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtenderDVtxtDateFrom"
                                    TargetControlID="dateValidatortxtDateFrom" runat="server">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Date To:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDateTo" runat="server" Width="150px" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtDateTo"
                                    Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtDateTo" runat="server" ValidationGroup="saveFZ"
                                    Display="None" ErrorMessage="Please enter Date To !" ControlToValidate="txtDateTo"
                                    InitialValue=""></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidatortxtDateTo" runat="server" Type="Date" Operator="DataTypeCheck"
                                    Display="None" InitialValue="" ControlToValidate="txtDateTo" ErrorMessage="Please enter a valid date."
                                    ValidationGroup="saveFZ">
                                </asp:CompareValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidatortxtDateTo"
                                    runat="server">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="CompareValidatortxtDateTo"
                                    runat="server">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remarks:
                            </td>
                            <td>
                               <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Width="150px" Height="50px"
                                            runat="server" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr><td colspan="2"></td></tr>
                        <tr>
                            <td colspan="2" style=" padding-left:20%">
                      
                                <asp:HiddenField ID="hdnServiceID" runat="server" />
                                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" ValidationGroup="saveFZ" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClientClick="parent.hideModal('dvPopupFrame'); return false;" />
                            </td>
                        </tr>
                    </table>
                     </fieldset>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
