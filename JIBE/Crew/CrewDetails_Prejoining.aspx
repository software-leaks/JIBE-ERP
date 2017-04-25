<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_Prejoining.aspx.cs"
    Inherits="Crew_CrewDetails_Prejoining" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/jsAsyncPager.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        #dvAddPreJoiningExp tr
        {
            height: 28px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", "#chkCurrentOperator", function () {
                if ($(this).is(":checked")) {
                    $('#txtPJVessel').val('');
                    $('#txtCompanyName').val('');
                    $('#ddlVessel').show();
                    $('#tdVessel').show();
                    $('#ddlVessel').val('0');
                    $('#ddlPJVesselType').val('0');
                    $('#ddlPJVesselType').prop('disabled', true);
                    $('#txtPJVessel').prop('disabled', true);
                    $('#txtCompanyName').prop('disabled', true);
                    $('#txtCompanyName').val($("#hdnVesselManager").val());
                }
                else {
                    $('#ddlVessel').hide();
                    $('#tdVessel').hide();
                    $('#ddlPJVesselType').prop('disabled', false);
                    $('#txtPJVessel').prop('disabled', false);
                    $('#txtCompanyName').prop('disabled', false);
                    $('#txtCompanyName').val('');
                }
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div id="dvPrejoiningExp">
                <div class="error-message" onclick="javascript:this.style.display='none';">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <div id="dvPreviousContacts">
                    <asp:Panel ID="pnlView_PreviousContacts" runat="server" Visible="false">
                        <fieldset id="flstPreviousOperator" runat="server" style="background-color: #effbef;
                            padding: 2px;">
                            <legend>Previous Operator's Contacts :
                                <asp:LinkButton ID="lnkPreviousContacts" runat="server" CssClass="inline-edit" OnClick="lnkEditPreviousContacts_Click"><font color=blue>[Edit]</font></asp:LinkButton></legend>
                            <table class="dataTable" border="0" style="color: #696969" width="45%">
                                <tr>
                                    <td width="1px">
                                        1.
                                    </td>
                                    <td width="35%">
                                        Worked with Multinational Crew:
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblMultinationalcrew" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        Nationalities:
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblNationalities" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        2.
                                    </td>
                                    <td colspan="2">
                                        Previous Operator's Contacts:
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:GridView ID="GridView_PreviousContacts" runat="server" AutoGenerateColumns="False"
                                            GridLines="None" CellPadding="3" CellSpacing="1" Width="100%" EmptyDataText=""
                                            CaptionAlign="Bottom" DataKeyNames="ID" AllowPaging="True" PageSize="10" Font-Size="11px"
                                            AllowSorting="True" OnRowDataBound="GridView_PreviousContacts_RowDataBound" CssClass="GridView-css">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PIC">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPIC" runat="server" Text='<%# Eval("pic")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Telephone">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTele" runat="server" Text='<%# Eval("Telephone")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fax">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFax" runat="server" Text='<%# Eval("Fax")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="E-Mail">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFax" runat="server" Text='<%# Eval("Email")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" HorizontalAlign="Left" />
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
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="pnlEdit_PreviousContacts" runat="server">
                        <div id="dvEdpreviousContact" title="Previous Operator's Contacts :" style="display: none;
                            border: 1px solid Gray; text-align: left; font-size: 12px; color: Black; width: 90%;">
                            <div class="error-message" onclick="javascript:this.style.display='none';">
                                <asp:Label ID="lblMsgEditPreContact" runat="server"></asp:Label>
                            </div>
                            <table>
                                <tr>
                                    <td>
                                        1.
                                    </td>
                                    <td>
                                        Worked with Multinational Crew
                                    </td>
                                    <td colspan="4" class="data">
                                        <asp:RadioButtonList ID="rdoMultiNationalCrew" runat="server" RepeatDirection="Horizontal"
                                            CssClass="control-edit">
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        If Yes, State Nationalities
                                    </td>
                                    <td colspan="4" class="data">
                                        <asp:TextBox ID="txtNationalities" runat="server" Width="400px" CssClass="control-edit uppercase"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        2.
                                    </td>
                                    <td colspan="5">
                                        Previous Operator's Contacts:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        Operator's Name/Company Name
                                    </td>
                                    <td>
                                        PIC
                                    </td>
                                    <td>
                                        Telephone
                                    </td>
                                    <td>
                                        Fax
                                    </td>
                                    <td>
                                        E-mail
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        a.
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14a_name" runat="server" Width="190px" CssClass="control-edit"
                                            onkeypress="return UCase(event,this)"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14a_pic" runat="server" Width="150px" CssClass="control-edit"
                                            onkeypress="return UCase(event,this)"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14a_tel" runat="server" Width="120px" CssClass="control-edit"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14a_fax" runat="server" Width="120px" CssClass="control-edit"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14a_email" runat="server" CssClass="control-edit" Width="150px"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14a_ID" runat="server" CssClass="control-edit" Width="150px"
                                            Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        b.
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14b_name" runat="server" Width="190px" CssClass="control-edit"
                                            onkeypress="return UCase(event,this)"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14b_pic" runat="server" Width="150px" CssClass="control-edit"
                                            onkeypress="return UCase(event,this)"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14b_tel" runat="server" Width="120px" CssClass="control-edit"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14b_fax" runat="server" Width="120px" CssClass="control-edit"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14b_email" runat="server" Width="150px" CssClass="control-edit"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14b_ID" runat="server" CssClass="control-edit" Width="150px"
                                            Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        c.
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14c_name" runat="server" Width="190px" CssClass="control-edit"
                                            onkeypress="return UCase(event,this)"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14c_pic" runat="server" Width="150px" CssClass="control-edit"
                                            onkeypress="return UCase(event,this)"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14c_tel" runat="server" Width="120px" CssClass="control-edit"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14c_fax" runat="server" Width="120px" CssClass="control-edit"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14c_email" runat="server" Width="150px" CssClass="control-edit"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14c_ID" runat="server" CssClass="control-edit" Width="150px"
                                            Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        d.
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14d_name" runat="server" Width="190px" CssClass="control-edit"
                                            onkeypress="return UCase(event,this)"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14d_pic" runat="server" Width="150px" CssClass="control-edit"
                                            onkeypress="return UCase(event,this)"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14d_tel" runat="server" Width="120px" CssClass="control-edit"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14d_fax" runat="server" Width="120px" CssClass="control-edit"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14d_email" runat="server" Width="150px" CssClass="control-edit"></asp:TextBox>
                                    </td>
                                    <td class="data">
                                        <asp:TextBox ID="txt14d_ID" runat="server" CssClass="control-edit" Width="150px"
                                            Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Button ID="btnSavePreviousContacts" runat="server" Text="Save Previous Contacts"
                                            OnClick="btnSavePreviousContacts_Click" />
                                        <asp:Button ID="btnCancelPreviousContacts" runat="server" Text="Cancel Previous Contacts"
                                            OnClick="btnCancelPreviousContacts_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </div>
                <div id="dvPrejoining">
                    <asp:Panel ID="pnlView_PreJoining" runat="server" Visible="false">
                        <fieldset id="flstPreJoiningExpInterview" runat="server" style="background-color: #effbef;
                            padding: 2px;">
                            <legend>Pre-Joining Experiences (From Interview Sheet): </legend>
                            <asp:Label ID="lblPreJoiningExperiences" runat="server" />
                        </fieldset>
                        <fieldset id="flstPreJoiningExp" runat="server" style="background-color: #effbef;
                            padding: 2px;">
                            <legend>Pre-Joining Experiences: </legend>
                            <div id="fragment-1-tool" style="text-align: right;">
                                <asp:ImageButton runat="server" Style="margin: -5px 2px 5px 0;" ID="ImgAdd_PreJoiningExp"
                                    ImageUrl="~/Images/btnPreJoiningExp.png" OnClick="ImgAdd_PreJoiningExp_Click" />
                            </div>
                            <div id="dvGrid_PreJoiningExp">
                                <asp:GridView ID="GridView_PreJoiningExp" runat="server" DataKeyNames="ID" AllowSorting="true"
                                    CellPadding="3" GridLines="None" CellSpacing="1" Width="100%" Font-Size="11px"
                                    AutoGenerateColumns="False" DataSourceID="objDS_PreJoiningExp" CssClass="GridView-css">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vessel Name" SortExpression="Vessel_Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPreJoiningExpId" runat="server" Text='<%# Bind("ID")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblVessel_Name" runat="server" Text='<%# Bind("Vessel_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtVessel_Name" runat="server" Text='<%# Bind("Vessel_Name")%>'
                                                    Width="100px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtVessel_Name" runat="server"
                                                    ValidationGroup="noofdays" Display="None" ErrorMessage="Vessel Name cannot be null!"
                                                    ControlToValidate="txtVessel_Name" InitialValue=""></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtVessel_Name"
                                                    TargetControlID="RequiredFieldValidatortxtVessel_Name" runat="server">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Flag" SortExpression="Flag">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFlag" runat="server" Text='<%# Bind("FLAG")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtFlag" runat="server" Text='<%# Bind("FLAG")%>' Width="40px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtFlag" runat="server" ValidationGroup="noofdays"
                                                    Display="None" ErrorMessage="Flag cannot be null!" ControlToValidate="txtFlag"
                                                    InitialValue=""></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtFlag" TargetControlID="RequiredFieldValidatortxtFlag"
                                                    runat="server">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vessel Type" SortExpression="Vessel_Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVessel_Type" runat="server" Text='<%# Bind("Vessel_Type")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlPJVesselType" DataSourceID="ObjectDataSource_VesselTypes"
                                                    DataTextField="VesselTypes" DataValueField="VesselTypes" runat="server" Width="206px"
                                                    Text='<%# Bind("Vessel_Type")%>'>
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="ObjectDataSource_VesselTypes" runat="server" SelectMethod="Get_VesselTypeList"
                                                    TypeName="SMS.Business.Infrastructure.BLL_Infra_VesselLib"></asp:ObjectDataSource>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DWT" SortExpression="DWT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDWT" runat="server" Text='<%# Bind("DWT")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDWT" runat="server" Text='<%# Bind("DWT")%>' Width="40px"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GRT" SortExpression="GRT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGRT" runat="server" Text='<%# Bind("GRT")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtGRT" runat="server" Text='<%# Bind("GRT")%>' Width="40px"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidatortxtGRT" runat="server" Operator="DataTypeCheck"
                                                    ValidationGroup="noofdays" Display="None" InitialValue="" Type="Integer" ControlToValidate="txtGRT"
                                                    ErrorMessage="GRT value should be numeric" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtGRT" TargetControlID="CompareValidatortxtGRT"
                                                    runat="server">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M/E Make/Model" SortExpression="ME_MakeModel">
                                            <ItemTemplate>
                                                <asp:Label ID="lblME_MakeModel" runat="server" Text='<%# Bind("ME_MakeModel")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtME_MakeModel" runat="server" Text='<%# Bind("ME_MakeModel")%>'
                                                    Width="40px"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="M/E BHP" SortExpression="ME_BHP">
                                            <ItemTemplate>
                                                <asp:Label ID="lblME_BHP" runat="server" Text='<%# Bind("ME_BHP")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtME_BHP" runat="server" Text='<%# Bind("ME_BHP")%>' Width="40px"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidatortxtME_BHP" runat="server" Operator="DataTypeCheck"
                                                    ValidationGroup="noofdays" Display="None" InitialValue="" Type="Integer" ControlToValidate="txtME_BHP"
                                                    ErrorMessage="BHP value should be numeric" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtME_BHP" TargetControlID="CompareValidatortxtME_BHP"
                                                    runat="server">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rank" SortExpression="Rank_Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRank_Name" runat="server" Text='<%# Bind("Rank_Short_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlRank" runat="server" DataSourceID="DataSource_Rank" DataTextField="Rank_Short_Name"
                                                    DataValueField="id" Text='<%# Bind("Rank")%>'>
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="DataSource_Rank" runat="server" SelectMethod="Get_RankList"
                                                    TypeName="SMS.Business.Crew.BLL_Crew_Admin"></asp:ObjectDataSource>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From" SortExpression="Date_From">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate_From" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_From"))) %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDate_From" runat="server" Text='<%# Bind("Date_From","{0:dd/MM/yyyy}")%>'
                                                    Width="80px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDate_From"
                                                    Format='<%#Convert.ToString(Session["User_DateFormat"]) %>'>
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtDate_From" runat="server"
                                                    ValidationGroup="noofdays" Display="None" ErrorMessage="From Date cannot be null!"
                                                    ControlToValidate="txtDate_From" InitialValue=""></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtDate_From" TargetControlID="RequiredFieldValidatortxtDate_From"
                                                    runat="server">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:CompareValidator ID="CompareValidatortxtDate_From" runat="server" Type="Date"
                                                    Operator="DataTypeCheck" Display="None" InitialValue="" ControlToValidate="txtDate_From"
                                                    ErrorMessage="Please enter a valid date." ValidationGroup="noofdays">
                                                </asp:CompareValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="CompareValidatortxtDate_From"
                                                    runat="server">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To" SortExpression="Date_To">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate_To" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_To"))) %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDate_To" runat="server" Text='<%# Bind("Date_To","{0:dd/MM/yyyy}")%>'
                                                    Width="80px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtDate_To"
                                                    Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtDate_To" runat="server"
                                                    ValidationGroup="noofdays" Display="None" ErrorMessage="To Date cannot be null!"
                                                    ControlToValidate="txtDate_To" InitialValue=""></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtDate_To" TargetControlID="RequiredFieldValidatortxtDate_To"
                                                    runat="server">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                <asp:CompareValidator ID="CompareValidatortxtDate_To" runat="server" Type="Date"
                                                    Operator="DataTypeCheck" Display="None" InitialValue="" ControlToValidate="txtDate_To"
                                                    ErrorMessage="Please enter a valid date." ValidationGroup="noofdays">
                                                </asp:CompareValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="CompareValidatortxtDate_To"
                                                    runat="server">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month/Days">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDAYS" runat="server" Text='<%# Eval("MONTHS") + " M / " + Eval("DAYS") + " D"%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Name" SortExpression="COMPANYNAME">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCOMPANYNAME" runat="server" Text='<%# Bind("COMPANYNAME")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCOMPANYNAME" runat="server" Text='<%# Bind("COMPANYNAME")%>'
                                                    Width="100px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtCOMPANYNAME" runat="server"
                                                    ValidationGroup="noofdays" Display="None" ErrorMessage="Company Name cannot be null!"
                                                    ControlToValidate="txtCOMPANYNAME" InitialValue=""></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtCOMPANYNAME"
                                                    TargetControlID="RequiredFieldValidatortxtCOMPANYNAME" runat="server">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="LinkButton1" ToolTip="Update" runat="server" ImageUrl="~/images/accept.png"
                                                    CausesValidation="True" CommandName="Update" AlternateText="Update" ValidationGroup="noofdays">
                                                </asp:ImageButton>
                                                <asp:ImageButton ID="LinkButton2" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                                    CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif"
                                                    CommandArgument='<%#Eval("[ID]")%>' CausesValidation="False" OnCommand="EditPreJoining"
                                                    AlternateText="Edit"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
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
                            </div>
                            <asp:ObjectDataSource ID="objDS_PreJoiningExp" runat="server" SelectMethod="Get_CrewPreJoiningExp"
                                TypeName="SMS.Business.Crew.BLL_Crew_CrewDetails" OnUpdating="Validate_Prejoining"
                                DeleteMethod="DEL_CrewPreJoiningExp" InsertMethod="INS_CrewPreJoiningExp" UpdateMethod="UPDATE_CrewPreJoiningExp">
                                <DeleteParameters>
                                    <asp:Parameter Name="ID" Type="Int32" />
                                    <asp:SessionParameter Name="Deleted_By" SessionField="userid" Type="Int32" />
                                </DeleteParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="Vessel_Name" Type="String" />
                                    <asp:Parameter Name="Flag" Type="String" />
                                    <asp:Parameter Name="Vessel_Type" Type="String" />
                                    <asp:Parameter Name="DWT" Type="String" />
                                    <asp:Parameter Name="GRT" Type="String" />
                                    <asp:Parameter Name="CompanyName" Type="String" />
                                    <asp:Parameter Name="Rank" Type="Int32" />
                                    <asp:Parameter Name="Date_From" Type="String" />
                                    <asp:Parameter Name="Date_To" Type="String" />
                                    <asp:SessionParameter Name="Created_By" SessionField="userid" Type="Int32" />
                                    <asp:Parameter Name="ME_MakeModel" Type="String" />
                                    <asp:Parameter Name="ME_BHP" Type="Int32" />
                                </InsertParameters>
                                <SelectParameters>
                                    <asp:QueryStringParameter DefaultValue="" Name="ID" QueryStringField="ID" Type="Int32" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="ID" Type="Int32" />
                                    <asp:Parameter Name="Vessel_Name" Type="String" />
                                    <asp:Parameter Name="Flag" Type="String" />
                                    <asp:Parameter Name="Vessel_Type" Type="String" />
                                    <asp:Parameter Name="DWT" Type="String" />
                                    <asp:Parameter Name="GRT" Type="String" />
                                    <asp:Parameter Name="CompanyName" Type="String" />
                                    <asp:Parameter Name="Rank" Type="Int32" />
                                    <asp:Parameter Name="Date_From" Type="String" />
                                    <asp:Parameter Name="Date_To" Type="String" />
                                    <asp:SessionParameter Name="Modified_By" SessionField="userid" Type="Int32" />
                                    <asp:Parameter Name="ME_MakeModel" Type="String" />
                                    <asp:Parameter Name="ME_BHP" Type="Int32" />
                                </UpdateParameters>
                            </asp:ObjectDataSource>
                        </fieldset>
                        <div class="error-message1" onclick="javascript:this.style.display='none';">
                            <asp:Label ID="Label1" runat="server" Font-Italic="true" ForeColor="Red"></asp:Label>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlAdd_PreJoining" runat="server">
                        <div id="dvAddPreJoiningExp" title="Pre-Joining Experience" style="display: none;
                            border: 1px solid Gray; font-family: Tahoma; text-align: left; font-size: 12px;
                            color: Black; width: 60%;">
                            <div class="error-message" onclick="javascript:this.style.display='none';">
                                <asp:Label ID="lblMsgPreJoining" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnVesselManager" runat="server" Value="0" />
                            </div>
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td>
                                        Company Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCompanyName" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="width: 20px;">
                                    </td>
                                    <td>
                                        Vessel Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPJVessel" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkCurrentOperator" runat="server" Text="Current Operator" />
                                    </td>
                                    <%--OnCheckedChanged="chkCurrentOperator_CheckedChanged" AutoPostBack="true" --%>
                                    <td>
                                    </td>
                                    <td runat="server" id="tdVessel">
                                        <span>Vessel</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlVessel" Width="206" runat="server" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Flag
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPJFlag" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="width: 20px;">
                                    </td>
                                    <td>
                                        Vessel Type
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPJVesselType" runat="server" Width="206px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        DWT
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPJDWT" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="width: 20px;">
                                    </td>
                                    <td>
                                        Rank
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddPJRank" runat="server" DataSourceID="ObjDataSourceDDLJoinRank"
                                            DataTextField="Rank_Short_Name" DataValueField="ID" Width="206px">
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="ObjDataSourceDDLJoinRank" runat="server" SelectMethod="Get_RankList"
                                            TypeName="SMS.Business.Crew.BLL_Crew_Admin"></asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        GRT
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGRT" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="width: 20px;">
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        M/E Make/Model
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMEModel" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="width: 20px;">
                                    </td>
                                    <td>
                                        M/E BHP
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMEBHP" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Period of Service From
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPJDateFrom" runat="server" Width="200px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPJDateFrom">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td style="width: 20px;">
                                    </td>
                                    <td>
                                        Period of Service To
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPJDateTo" runat="server" Width="200px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtPJDateTo">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="center">
                                        <asp:Button ID="btnSavePreJoiningExp" runat="server" Text=" Save " OnClick="btnSavePreJoiningExp_Click">
                                        </asp:Button>
                                        <asp:Button ID="btnClosePreJoining" runat="server" Text="Cancel" OnClick="btnClosePreJoining_Click">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </div>
                <div id="dvReferalDetails" title="Referal Details">
                    <iframe id="IReferalDetails" src="CrewDetails_RefererDetails.aspx?CrewID=<%=GetCrewID() %>&Mode=1"
                        frameborder="0" style="width: 100%; height: 400px"></iframe>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
   <%-- <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", "#<%=btnSavePreJoiningExp.ClientID %>", function () {
                var MSG = "";
                var strDateFormat = '<%=DateFormat %>';

                if ($.trim($("#<%=txtPJDateFrom.ClientID %>").val()) == "") {
                    MSG = "Enter Period of Service From Date\n";
                } else if (IsInvalidDate($.trim($("#<%=txtPJDateFrom.ClientID %>").val()), strDateFormat)) {
                    MSG += "Enter valid Period of Service From Date" + '<%=TodayDateFormat %>' + "\n";
                }

                if ($.trim($("#<%=txtPJDateTo.ClientID %>").val()) == "") {
                    MSG += "Enter Period of Service To Date\n";
                }
                else if (IsInvalidDate($.trim($("#<%=txtPJDateTo.ClientID %>").val()), strDateFormat)) {
                    MSG += "Enter valid Period of Service To Date" + '<%=TodayDateFormat %>' + "\n";
                }

                if ($.trim($("#<%=txtPJDateFrom.ClientID %>").val()) != "" && $.trim($("#<%=txtPJDateTo.ClientID %>").val())!="") {
                    if (DateAsFormat($.trim($("#<%=txtPJDateFrom.ClientID %>").val()), strDateFormat) > DateAsFormat($.trim($("#<%=txtPJDateTo.ClientID %>").val()), strDateFormat)) {
                        MSG += "To Date can not be less than From date";
                    }
                }

                if (MSG != "") {
                    alert(MSG)
                    return false;
                }
            });
        });
    </script>--%>
</body>
</html>
