<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TMSA_Configure_KPI.aspx.cs" Inherits="TMSA_KPI_TMSA_Configure_KPI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="tlk" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.droppable.js" type="text/javascript"></script>
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <style type="text/css">
        .page
        {
            width: 1440px;
        }
        body
        {
            color: black;
            font-family: Tahoma;
            font-size: 11px;
        }
        select
        {
            height: 21px;
            font-family: Tahoma;
            font-size: 11px;
        }
        input
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        textarea
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        .link
        {
            text-decoration: none;
            text-transform: capitalize;
        }
        .roundedBox
        {
            border-radius: 5px;
            border: 2px solid white;
            background-color: #DBDFD5;
            text-align: center;
            font-size: 14px;
            color: #555;
            margin: 2px;
            padding: 2px;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
        .data
        {
            border: 1px solid #efefef;
            background-color: #F5F6CE;
        }
        .row-header
        {
            background-color: #66CCFF;
            font-weight: bold;
        }
        .style1
        {
            height: 286px;
        }
        .style2
        {
            height: 23px;
        }
        .style3
        {
            width: 1%;
            height: 23px;
        }
    </style>
    <style type="text/css">
        .hide
        {
            display: none;
        }
        .show
        {
            display: block;
        }
        .bold
        {
            font-weight: bold;
        }
        .dvDrag
        {
        }
        .formula
        {
            border: 1px solid gray;
            background-color: Silver;
            width: 99%;
            margin-left: 0.5%;
            margin-bottom: 0.5%;
            height: 50px;
        }
        .hidebr br
        {
            display: none;
        }
        .formula li
        {
            display: inline;
            list-style-type: none;
        }
    </style>
    <script type="text/javascript">

        function OpenGoal() {
            var KPID = '<%= hdnKPIID.Value %>';
            var e = document.getElementById("ctl00_MainContent_ddlKPIApplicableFor");   //Pass selected option from "Applicable For" dropdown
            var KPI_ApplicableFor = e.options[e.selectedIndex].value;
            var url = "../KPI/KPI_Goal.aspx?KPI_ID=" + KPID+'&AppFor='+KPI_ApplicableFor;
           

            OpenPopupWindow('KPIGOAL', 'KPI Goals', url, 'popup', 800, 500, null, null, false, false, true, null);
        }


        function OpenQueryBuilder() {
            var url = "../PI/querybuilder.aspx?Type=TMSA_KPI_Daemon_SP";
            window.open(url, "_blank");
        }

        $(document).ready(function () {
            var MaxLength = 250;
            $('#txtKPIDesc').keypress(function (e) {
                if ($(this).val().length >= MaxLength) {
                    e.preventDefault();
                }
            });
        });


        $(function () {
            var items;
            var shouldCancel = false;
            $(".dvDrag").draggable({
                containment: $('.formula'),
                axis: "x",
                stack: ".dvDrag",
                revert: function () {
                    if (shouldCancel) {
                        shouldCancel = false;
                        return true;
                    } else {
                        return false;
                    }
                }
            });
            $('.dvDrag').droppable({
                tolerance: "touch",
                over: function () {
                    shouldCancel = true;
                },
                drop: function () {
                    shouldCancel = true;
                }
            });
            $(".formula").droppable({
                drop: function (event, ui) {
                    items = [ui.draggable];
                    shouldCancel = false;
                }
            });
        });

        function Position() {
            var position = "";
            $('.dvDrag').each(function (i, el) {
                // x = $(el).html();
                //alert($(el).position().left);
                position = position + $(el).position().left + '+' + $(el).attr('id') + ',';
                // alert(position);
            });
            $("[id$=hdnPosition]").val(position);
            //alert($("[id$=hdnPosition]").val());
            //alert(ui.draggable.attr('id'));.attr('id') 
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="width: 100%">
        <div style="border: 1px solid #cccccc" class="page-title">
            <asp:Literal ID="ltPageHeader" Text="Create/Edit KPI" runat="server"></asp:Literal>
        </div>
        <div align="center">
            <table>
                <tr>
                    <td align="left" style="border: 1px solid #aabbdd;" valign="top">
                        <table border="0" cellpadding="2" cellspacing="1" width="100%">
                            <tr class="row-header">
                                <td colspan="2">
                                    <asp:Label ID="lblCode1" runat="server" Text="KPI Code :"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="lblCode3" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px" align="right">
                                    KPI Name :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="left">
                                    *
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKPIName" runat="server" MaxLength="150" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtKPIName" runat="server"
                                        ControlToValidate="txtKPIName" Display="None" ErrorMessage="KPI Name is required."
                                        InitialValue="" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <tlk:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtKPIName" TargetControlID="RequiredFieldValidatortxtKPIName"
                                        runat="server">
                                    </tlk:ValidatorCalloutExtender>
                                </td>
                                <td align="center">
                                    <asp:Button ID="Button1" runat="server" Text="Goal" OnClick="Button1_Click" />
                                    &nbsp;
                                    <asp:ImageButton ID="imgQBuilder" ImageUrl="../../images/wizard/database-process-icon.png"
                                        ToolTip="Query Builder" runat="server" OnClientClick="OpenQueryBuilder();" Height="20px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    KPI Description :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="left">
                                    *
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtKPIDesc" runat="server" TextMode="MultiLine" Height="100px" Width="500px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtKPIDesc" runat="server"
                                        ControlToValidate="txtKPIDesc" Display="None" ErrorMessage="KPI description is required."
                                        InitialValue="" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rgDescription" ValidationGroup="Save" ControlToValidate="txtKPIDesc"
                                        ErrorMessage="Description can't exceed 500 characters" ValidationExpression="^[\s\S]{0,500}$"
                                        runat="server" Display="None" SetFocusOnError="true" />
                                    <tlk:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtKPIDesc" TargetControlID="RequiredFieldValidatortxtKPIDesc"
                                        runat="server">
                                    </tlk:ValidatorCalloutExtender>
                                    <tlk:ValidatorCalloutExtender ID="ValidatorCalloutExtenderKPIDesc" TargetControlID="rgDescription"
                                        runat="server">
                                    </tlk:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Time Interval :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="left">
                                    *
                                </td>
                                <td colspan="1">
                                    <asp:DropDownList ID="ddlInterval" runat="server" Width="156px" AutoPostBack="true" OnSelectedIndexChanged="ddlInterval_SelectedIndexChanged" CssClass="control-edit">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlInterval" runat="server"
                                        ValidationGroup="Save" Display="None" ErrorMessage="Please select Interval !"
                                        ControlToValidate="ddlInterval" InitialValue="0"></asp:RequiredFieldValidator>
                                    <tlk:ValidatorCalloutExtender ID="ValidatorCalloutExtenderddlInterval" TargetControlID="RequiredFieldValidatorddlInterval"
                                        runat="server">
                                    </tlk:ValidatorCalloutExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTimePeriod" runat="server" Visible="false" Width="150px" Height="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Vessel/Fleet Measurements :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="left">
                                    &nbsp;
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtMeasure" runat="server" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblListSource" runat="server" Text="Data Source:"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlListSource" runat="server" Width="250px" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Status :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="left">
                                    *
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="156px" CssClass="control-edit">
                                        <asp:ListItem Text="-Select-" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlStatus" runat="server" ValidationGroup="Save"
                                        Display="None" ErrorMessage="Please select Status !" ControlToValidate="ddlStatus"
                                        InitialValue="2"></asp:RequiredFieldValidator>
                                    <tlk:ValidatorCalloutExtender ID="ValidatorCalloutExtenderddlStatus" TargetControlID="RequiredFieldValidatorddlStatus"
                                        runat="server">
                                    </tlk:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblFormula1" runat="server" Text="Formula :"></asp:Label>
                                </td>
                                <td style="color: #FF0000; width: 1%" align="left">
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lblFormula3" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Category :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="left">
                                    *
                                </td>
                                <td>
                                    <div>
                                        <asp:DropDownList ID="ddlCategory" runat="server" Width="156px" CssClass="control-edit">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Save"
                                            Display="None" ErrorMessage="Category required !" ControlToValidate="ddlCategory"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                        <tlk:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1"
                                            runat="server">
                                        </tlk:ValidatorCalloutExtender>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    URL :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="left">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtURL" Width="300px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Applicable For :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="left">
                                    *
                                </td>
                                <td>
                                    <div>
                                        <asp:DropDownList ID="ddlKPIApplicableFor" runat="server" Width="156px" CssClass="control-edit">
                                            <asp:ListItem Text="Fleet" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Vessel" Value="1" Selected=True></asp:ListItem>
                                            <asp:ListItem Text="Vessel Type" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Company" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <center>
                                        <%--<asp:Button ID="Button1" runat="server" Text="Goal" OnClick="Button1_Click" />--%>
                                    </center>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%">
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr style="background-color: #66CCFF">
                <td colspan="4">
                    <div>
                        <div>
                            <table border="0" width="100%">
                                <tr>
                                    <td colspan="12" class="bold" align="left">
                                        Create KPI Formula
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        PI :
                                    </td>
                                    <td style="width: 470px">
                                        <asp:DropDownList ID="ddlPIList" runat="server" Width="460px" CssClass="control-edit">
                                        </asp:DropDownList>
                                        <%--                                     <asp:RequiredFieldValidator ID="rfvPIlist" runat="server" ValidationGroup="Save"
                                        Display="None" ErrorMessage="Please select PI !" ControlToValidate="ddlPIList"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                            <tlk:ValidatorCalloutExtender ID="vePI" TargetControlID="rfvPIlist"
                                                runat="server">
                                            </tlk:ValidatorCalloutExtender>--%>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnAddPI" runat="server" src="../../Images/Add-icon.png"
                                            OnClientClick="Position();" OnClick="PI_Click" />
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 60px">
                                        Operator :
                                    </td>
                                    <td style="width: 110px">
                                        <asp:DropDownList ID="ddlOperator" runat="server" Width="100px" CssClass="control-edit">
                                            <asp:ListItem Text="-select-" Value=""></asp:ListItem>
                                            <asp:ListItem Text="+" Value="+"></asp:ListItem>
                                            <asp:ListItem Text="-" Value="-"></asp:ListItem>
                                            <asp:ListItem Text="*" Value="*"></asp:ListItem>
                                            <asp:ListItem Text="^" Value="^"></asp:ListItem>
                                            <asp:ListItem Text="/" Value="/"></asp:ListItem>
                                            <asp:ListItem Text="(" Value="("></asp:ListItem>
                                            <asp:ListItem Text=")" Value=")"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnAddOperator" runat="server" src="../../Images/Add-icon.png"
                                            OnClientClick="Position();" OnClick="Operator_Click" />
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 100px">
                                        Numerical Value :
                                    </td>
                                    <td style="width: 150px">
                                        <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
                                        <asp:CompareValidator ID="ValidatortxtNumber" runat="server" Type="Integer" Operator="DataTypeCheck"
                                            Display="None" InitialValue="" ControlToValidate="txtNumber" ErrorMessage="Please enter a valid number."
                                            ValidationGroup="saveFZ">
                                        </asp:CompareValidator>
                                        <tlk:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtNumber" TargetControlID="ValidatortxtNumber"
                                            runat="server">
                                        </tlk:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnAddNumber" runat="server" src="../../Images/Add-icon.png"
                                            OnClientClick="Position();" OnClick="Number_Click" ValidationGroup="saveFZ" />
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="formula">
                            <asp:DataList ID="dtlFormula" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table"
                                CellSpacing="5">
                                <ItemTemplate>
                                    <div id='<%# Eval("sno") %>' class="dvDrag" style="background-color: #C3EBFF; border-radius: 5px;
                                        padding: 5px; border: 1px solid #ACC9C9; width: auto">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFormula" runat="server" Text='<%# Eval("value") %>'></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:ImageButton ID="imgbtnDeleteTask" runat="server" OnCommand="imgbtnDeleteTask_Click"
                                                        OnClientClick="Position();" CommandArgument='<%# Eval("sno") %>' AlternateText="delete"
                                                        ImageAlign="AbsMiddle" ImageUrl="~/Images/Delete.png" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <%--<div class="formula">
                            <ul>
                                <asp:Repeater ID="dtlFormula" runat="server">
                                    <ItemTemplate>
                                        <li>
                                        <div id='<%# Eval("sno") %>' class="dvDrag" style="background-color: #C3EBFF; border-radius: 5px;
                                                padding: 5px; border: 1px solid #ACC9C9; width: auto">
                                            <asp:Label ID="lblFormula" runat="server" Text='<%# Eval("value") %>'></asp:Label></div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                            </div>--%>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right" style="padding: 10px">
                    <center>
                        <asp:DataList ID="DataList1" runat="server" RepeatDirection="Vertical" RepeatLayout="Table"
                            GridLines="Both">
                            <ItemTemplate>
                                <table border="0" cellpadding="5">
                                    <tr>
                                        <td style="border-right-style: solid; border-right-width: thin;">
                                            <asp:Label ID="lblPIID" runat="server" Text='<%# Eval("value") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPIText" runat="server" Text='<%# Eval("PIName") %>'></asp:Label>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <center>
                        <asp:Button ID="txtSave" runat="server" Text="Save" OnClick="txtSave_Click" ValidationGroup="Save"
                            OnClientClick="Position();" />
                        <asp:HiddenField ID="hdnKPIID" runat="server" />
                        <asp:HiddenField ID="hdnPosition" runat="server" />
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Italic="true" Font-Size="11px"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
