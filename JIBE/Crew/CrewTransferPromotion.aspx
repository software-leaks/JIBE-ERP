<%@ Page Title="Crew Transfer/Promotion Planning" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CrewTransferPromotion.aspx.cs" Inherits="Crew_CrewTransferPromotion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ctlPortList.ascx" TagName="PortList" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        #pageTitle
        {
            background-color: gray;
            color: White;
            font-size: 12px;
            text-align: center;
            padding: 2px;
            font-weight: bold;
        }
        #page-content
        {
            font-size: 12px;
        }
        .header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
        }
        h4
        {
            font-size: 1.2em;
            color: #ffffff;
            font-weight: bold;
            margin: 0 0 0 5px;
        }
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        .dataTable
        {
            background-color: #ffffff;
            border: 1px solid #efefef;
            border-collapse: collapse;
            color: Teal;
        }
        .dataTable td
        {
            background-color: #ffffff;
            border: 1px solid #efefef;
            border-collapse: collapse;
            padding: 0px;
        }
        .data
        {
            vertical-align: top;
            background-color: #ffffff;
            color: Black;
        }
        .inline-edit
        {
            font-size: 10px;
            text-decoration: none;
            font-weight: normal;
            color: Blue;
        }
        .up
        {
            background-image: url(../Images/up.png);
            background-repeat: no-repeat;
        }
        .down
        {
            background-image: url(../Images/down.png);
            background-repeat: no-repeat;
        }
        .class-doc-list
        {
            font-size: 11px;
        }
        .class-doc-edit
        {
            font-size: 11px;
        }
        .control-edit
        {
            font-family: Tahoma;
            font-size: 12px;
            padding: 0px;
            width: 150px;
        }
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
        .tooltip
        {
            display: none;
            background: transparent url(../Images/black_arrow.png);
            font-size: 12px;
            height: 70px;
            width: 160px;
            padding: 25px;
            color: #fff;
        }
        .user-answer-table
        {
            font-size: 10px;
            border: 1px solid gray;
        }
        .user-answer-table td
        {
            border: 1px solid #dfdfdf;
            white-space: nowrap;
        }
        .interviewer-table
        {
            border: 1px solid gray;
            background-color: #bfcfdf;
            width: 100%;
        }
        .interviewer-table td
        {
            background-color: #bfcfdf;
        }
        .interview-result-table
        {
            font-size: 10px;
            border: 1px solid gray;
        }
        
        .interview-result-table td
        {
            vertical-align: top;
        }
        .Question
        {
            width: 80%;
        }
        .ans1
        {
            background-color: Green;
            border: 1px solid black;
            color: White;
            text-align: center;
            cursor: pointer;
        }
        .ans2
        {
            background-color: #FFBF00;
            border: 1px solid black;
            text-align: center;
            cursor: pointer;
        }
        .ans3
        {
            background-color: #FFBF00;
            border: 1px solid black;
            text-align: center;
            cursor: pointer;
        }
        .ans4
        {
            background-color: Red;
            border: 1px solid black;
            color: White;
            text-align: center;
            cursor: pointer;
        }
        .AttributeEditBox
        {
            border: 0;
            width: 100%;
        }
        .required
        {
            background-color: #F2F5A9;
            border: 1px solid #dcdcdc;
            height: 18px;
        }
    </style>
    <style type="text/css">
        .numeric-edit
        {
            text-align: right;
        }
    </style>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", "#<%=btnSaveVoyage.ClientID %>", function () {
                if ($.trim($("#<%=txtSignOffDate.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtSignOffDate.ClientID %>").val()), '<%=DateFormat %>')) {
                        alert("Enter valid Sign Off Date<%=TodayDateFormat %>");
                        $("#<%=txtSignOffDate.ClientID %>").focus();
                        return false;
                    }
                }

                if ($.trim($("#<%=txtJoiningDate.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtJoiningDate.ClientID %>").val()), '<%=DateFormat %>')) {
                        alert("Enter valid Contract Date<%=TodayDateFormat %>");
                        $("#<%=txtJoiningDate.ClientID %>").focus();
                        return false;
                    }
                }

                if ($.trim($("#<%=txtSignOnDate.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtSignOnDate.ClientID %>").val()), '<%=DateFormat %>')) {
                        alert("Enter valid Sign On Date<%=TodayDateFormat %>");
                        $("#<%=txtSignOnDate.ClientID %>").focus();
                        return false;
                    }
                }

                if ($.trim($("#<%=txtCOCDate.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtCOCDate.ClientID %>").val()), '<%=DateFormat %>')) {
                        alert("Enter valid EOC Date <%=TodayDateFormat %>");
                        $("#<%=txtCOCDate.ClientID %>").focus();
                        return false;
                    }
                }

                if ($.trim($("#<%=txteffdt.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txteffdt.ClientID %>").val()), '<%=DateFormat %>')) {
                        alert("Enter valid Effective Date <%=TodayDateFormat %>");
                        $("#<%=txteffdt.ClientID %>").focus();
                        return false;
                    }
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: bottom;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
        padding: 2px; background-color: #F6CEE3; text-align: center;">
        <div>
            Crew Transfer/Promotion Planning</div>
    </div>
    <div id="Div1" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 5px;">
        <div id="page-content" style="min-height: 400px; overflow: auto;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="error-message" onclick="javascript:this.style.display='none';">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                    <div style="border: 1px solid #B6DAFD; background-color: white; padding: 3px; margin-bottom: 5px;">
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rdoOptions" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="true" OnSelectedIndexChanged="rdoOptions_SelectedIndexChanged">
                                        <asp:ListItem Text="Transfer" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Promotion" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Transfer with Promotion" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="border: 1px solid #B6DAFD; background-color: white; padding: 2px; margin-bottom: 5px;">
                        <div style="background-color: #B6DAFD; padding: 2px; margin-bottom: 5px; color: Black;
                            font-size: 14px;">
                            Staff Details:</div>
                        <div style="border: 1px solid #B6DAFD; background-color: #E8F3FE; padding: 2px; margin-bottom: 5px;">
                            <table>
                                <tr>
                                    <td style="width: 60px; text-align: left">
                                        Staff Code:
                                    </td>
                                    <td style="width: 70px; font-weight: bold; text-align: left">
                                        <asp:Label ID="lblStaffCode" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 80px; text-align: left">
                                        Contract Date:
                                    </td>
                                    <td style="width: 100px; font-weight: bold; text-align: left">
                                        <asp:Label ID="lblContractDate" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 70px; text-align: left">
                                        Staff Name:
                                    </td>
                                    <td style="width: 250px; font-weight: bold; text-align: left">
                                        <asp:Label ID="lblStaffName" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 40px; text-align: left">
                                        Rank:
                                    </td>
                                    <td style="font-weight: bold; text-align: left; width: 50px;">
                                        <asp:Label ID="lblRank" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnCrewrank" runat="server" />
                                    </td>
                                    <td>
                                    </td>
                                    <td style="width: 50px; text-align: left">
                                        Sign-On:
                                    </td>
                                    <td style="font-weight: bold; text-align: left; width: 100px;">
                                        <asp:Label ID="lblSignOn" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align: left; width: 80px;">
                                        Sign-Off/EOC:
                                    </td>
                                    <td style="font-weight: bold; text-align: left">
                                        <asp:Label ID="lblCOC" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div style="border: 1px solid #B6DAFD; background-color: white; padding: 2px; margin-bottom: 5px;">
                        <asp:Panel ID="pnlViewTransfer" runat="server" Visible="false">
                            <div style="background-color: #B6DAFD; padding: 2px; margin-bottom: 5px;">
                                Transfer/Promotion Log:</div>
                            <asp:GridView ID="GridView1" DataKeyNames="Transfer_ID" runat="server" AutoGenerateColumns="False"
                                EmptyDataText="No Record Found" CellPadding="4" ForeColor="#333333" GridLines="None"
                                OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
                                Width="100%">
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle CssClass="HeaderStyle-css" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" BackColor="White" ForeColor="#284775" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle CssClass="RowStyle-css" BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sign-Off Date" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSign_Off_date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Sign_Off_date"))) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sign-Off Vessel" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromVess" runat="server" Text='<%#Eval("FromVessel") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sign-Off Rank" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromRank" runat="server" Text='<%#Eval("FromRank") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sign-Off Port" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSign_Off_Port" runat="server" Text='<%#Eval("Sign_Off_Port") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSign" runat="server" Text=' ---> '></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sign-On Date" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSign_On_date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Sign_On_date"))) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sign-On Vessel" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToVess" runat="server" Text='<%#Eval("ToVessel") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sign-On Rank" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToRank" runat="server" Text='<%#Eval("ToRank") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sign-On Port" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSign_On_Port" runat="server" Text='<%#Eval("Sign_On_Port") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transfer/Promotion" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransferType" runat="server" Text='<%#Eval("TransferType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Transfer_Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkDelete" runat="server" ImageUrl="~/images/delete.png" CausesValidation="False"
                                                CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                                                AlternateText="Delete"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Panel ID="pnlTransfer" runat="server" Visible="false">
                            <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                                <legend>New Voyage Detail :</legend>
                                <table>
                                    <tr>
                                        <td style="vertical-align: top">
                                            <table border="0" cellpadding="2" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        Fleet
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlFleet" runat="server" Width="156px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Vessel Name:
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlVessel" runat="server" Width="156px" DataTextField="Vessel_Name"
                                                            DataValueField="Vessel_ID" AutoPostBack="true" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Contract Template:
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlContract" runat="server" Width="156px" DataTextField="Contract_Name"
                                                            DataValueField="ContractId">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Joining Rank:
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlJoiningRank" runat="server" Width="156px" AutoPostBack="true"
                                                            DataTextField="Rank_Short_Name" DataValueField="ID" OnSelectedIndexChanged="ddlJoiningRank_SelectedIndexChanged">
                                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:CompareValidator ID="CompareValidatorport" ValidationGroup="vggrid2" ControlToValidate="ddlJoiningRank"
                                                            Operator="NotEqual" ValueToCompare="select" Type="String" runat="server" ErrorMessage="Please select rank"></asp:CompareValidator>
                                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="CompareValidatorport"
                                                            runat="server">
                                                        </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                                <%-- <tr id="trRankScale" runat="server">
                                                    <td>
                                                        Rank Scale:
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlRankScale" runat="server" Width="156px"  AutoPostBack="true"  OnSelectedIndexChanged="ddlRankScale_SelectedIndexChanged" ></asp:DropDownList>
                                                    </td>
                                                </tr>--%>
                                    </tr>
                                </table>
                                </td>
                                <td style="vertical-align: top; width: 300px;">
                                    <table>
                                        <tr>
                                            <td align="left">
                                                Sign Off Date:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSignOffDate" runat="server" Width="150px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtSignOffDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Sign-Off Port:
                                            </td>
                                            <td>
                                                <uc:PortList ID="ctlSignOffPort" runat="server" Width="130px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr id="trRankScale" runat="server">
                                            <td>
                                                Rank Scale:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRankScale" runat="server" Width="156px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlRankScale_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top">
                                    <table>
                                        <tr>
                                            <td align="left">
                                                Contract Date:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtJoiningDate" runat="server" Width="150px" AutoPostBack="true"
                                                    OnTextChanged="txtJoiningDate_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtJoiningDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Sign On Date:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSignOnDate" runat="server" Width="150px" AutoPostBack="true"
                                                    OnTextChanged="txtSignOnDate_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender15" runat="server" TargetControlID="txtSignOnDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Joining Port:
                                            </td>
                                            <td>
                                                <uc:PortList ID="ctlJoiningPort" runat="server" Width="130px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                EOC Date:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCOCDate" runat="server" Width="150px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCOCDate"
                                                    Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                </tr> </table>
                            </fieldset>
                        </asp:Panel>
                        <asp:Panel ID="pnlPromotion" runat="server" Visible="false">
                            <table style="border: 1px solid #B6DAFD; width: 100%; margin-top: 4px;" cellpadding="4">
                                <tr style="background-color: #E8F3FE;">
                                    <td style="width: 40%; font-weight: bold; color: Black;">
                                        Current Wages
                                    </td>
                                    <td style="width: 60%; font-weight: bold; color: Black;">
                                        Wages after Promotion
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">
                                        <table style="width: 100%">
                                            <asp:Repeater runat="server" ID="rpt1" OnItemDataBound="rpt1_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            Salary effective from
                                                        </td>
                                                        <td>
                                                            <%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("effective_date")))%>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #D8D8D8; color: Black; font-weight: bold;">
                                                        <td>
                                                            Earning/Deduction
                                                        </td>
                                                        <td>
                                                            Name
                                                        </td>
                                                        <td>
                                                            Salary Type
                                                        </td>
                                                        <td>
                                                            Payable At
                                                        </td>
                                                        <td>
                                                            Amount
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater runat="server" ID="rpt2" OnItemDataBound="rpt2_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr style="background-color: #CEF6F5; color: Black">
                                                                <td>
                                                                    <%--<%#Eval("EarningDeduction") %>--%>
                                                                    <asp:Label ID="lblEarningDeduction" runat="server" Text='<%# Eval("EarningDeduction")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("Name") %>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("Salary_Type")%>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("PayableAt")%>
                                                                </td>
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("Amount","{0:0.00}")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <tr style="background-color: #CEF6F5; color: Black; font-weight: bold;">
                                                                <td colspan="4">
                                                                    Total
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblTotalAmt" runat="server" Text='<%# Eval("Amount","{0:0.00}")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </td>
                                    <td style="vertical-align: top">
                                        <asp:Label ID="lbleffdt" runat="server" Text="Effective Date:"></asp:Label>
                                        <asp:TextBox ID="txteffdt" CssClass="textbox-css" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txteffdt">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:HiddenField ID="hdWageContractId" runat="server" />
                                        <asp:GridView ID="GridViewaddwages" DataKeyNames="code" runat="server" AutoGenerateColumns="False"
                                            EmptyDataText="No Record Found" CellPadding="4" ForeColor="#333333" GridLines="None"
                                            OnRowDataBound="GridViewaddwages_RowDataBound">
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle CssClass="HeaderStyle-css" Font-Bold="True" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" BackColor="White" ForeColor="#284775" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle CssClass="RowStyle-css" BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Earning/Deduction">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltype" runat="server" Text='<%#Eval("EarningDeduction") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblentry_type" runat="server" Text='<%#Eval("name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Salary Type" ItemStyle-Width="180px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList ID="SalaryTypes" runat="server" DataSourceID="ObjectDataSourcePayableAt"
                                                            DataTextField="name" DataValueField="code" Font-Size="11px" RepeatDirection="Horizontal"
                                                            SelectedValue='<%# Eval("DFSalary_Type") %>' Enabled="false">
                                                        </asp:RadioButtonList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Payble At" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList ID="PayableAT" runat="server" DataSourceID="ObjectDataSourcePayableAt"
                                                            DataTextField="name" DataValueField="code" Font-Size="11px" RepeatDirection="Horizontal"
                                                            SelectedValue='<%# Eval("DFPayableAT") %>'>
                                                        </asp:RadioButtonList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" runat="server" Font-Size="11px" Width="70px" Text='<%# String.Format("{0:0.00}", Eval("Amount"))%>'
                                                            EnableViewState="true" CssClass='<%#Eval("Amount").ToString()%>' OnTextChanged="amount_text_changed"
                                                            AutoPostBack="true" Enabled='<%# Convert.ToInt32(Eval("OnBoardEntry"))==1?false:true %>'
                                                            ToolTip='<%# Convert.ToInt32(Eval("OnBoardEntry"))==1?"Amount will be added on vessel":"" %>'></asp:TextBox>
                                                        <asp:HiddenField ID="hdnAmount" runat="server" Value="0"></asp:HiddenField>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlSave" runat="server" Visible="false">
                            <div style="text-align: center">
                                <asp:Button ID="btnSaveVoyage" runat="server" Text=" Save " OnClick="btnSaveVoyage_Click">
                                </asp:Button>
                                <asp:Button ID="btnCloseVoyage" runat="server" Text=" Cancel " OnClientClick="window.close();return false;">
                                </asp:Button>
                            </div>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSourceSalaryType" runat="server" SelectMethod="Get_CrewWages"
        TypeName="SMS.Business.PortageBill.BLL_PortageBill">
        <SelectParameters>
            <asp:QueryStringParameter Name="VoyID" QueryStringField="VoyID" />
            <asp:QueryStringParameter Name="CrewID" QueryStringField="CrewID" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourcePayableAt" runat="server" SelectMethod="Get_Salary_Types"
        TypeName="SMS.Business.PortageBill.BLL_PortageBill"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceEntryType" runat="server" SelectMethod="Get_Wages_EntryType"
        TypeName="SMS.Business.PortageBill.BLL_PortageBill"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceEntryType_deduction" runat="server" SelectMethod="Get_Wages_EntryType_Deduction"
        TypeName="SMS.Business.PortageBill.BLL_PortageBill">
        <SelectParameters>
            <asp:SessionParameter Name="indvslcode" SessionField="indvslcode" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceRuleType" runat="server" SelectMethod="Get_Rule_Type"
        TypeName="SMS.Business.PortageBill.BLL_PortageBill"></asp:ObjectDataSource>
</asp:Content>
