<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Wage_Contract.aspx.cs"
    Inherits="Account_Portage_Bill_Wage_Contract" Title="Rank-Wage Assignment" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/CrewIndex_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/InterviewSchedule_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>
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
            cursor: hand;
        }
        .ans2
        {
            background-color: #FFBF00;
            border: 1px solid black;
            text-align: center;
            cursor: hand;
        }
        .ans3
        {
            background-color: #FFBF00;
            border: 1px solid black;
            text-align: center;
            cursor: hand;
        }
        .ans4
        {
            background-color: Red;
            border: 1px solid black;
            color: White;
            text-align: center;
            cursor: hand;
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
        .style1
        {
            width: 141px;
        }
        .GridView-css
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Rank-Wage Assignment
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="Div1" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 5px;">
        <div id="page-content">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="error-message" onclick="javascript:this.style.display='none';">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                    </div>
                    <fieldset style="text-align: left; margin-top: 5px; padding: 2px;">
                        <legend>
                            <asp:Label ID="lblAddEditWages" runat="server" Text="Rank - Wage Assignment:"></asp:Label>
                        </legend>
                        <table style="width: 100%">
                            <tr>
                                <td style="vertical-align: top; width: 220px; color: Black;">
                                    <div runat="server" id="divVesselFlag" style="vertical-align: top;">
                                        <asp:Label ID="lblflag" Text="Vessel Flag:" runat="server"></asp:Label>
                                        <asp:ListBox ID="lstFlags" runat="server" Width="220px" AutoPostBack="true" OnSelectedIndexChanged="ddlRank_SelectedIndexChanged">
                                        </asp:ListBox>
                                        <br />
                                    </div>
                                    <div runat="server" id="divRank" style="vertical-align: top;">
                                        <asp:Label ID="Label1" Text="Ranks:" runat="server"></asp:Label>
                                        <asp:ListBox ID="ddlRank" runat="server" Width="220px" Height="660px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlRank_SelectedIndexChanged"></asp:ListBox>
                                    </div>
                                </td>
                                <%-- VASU ADDED START--%>
                                <td style="vertical-align: top; width: 250px; color: Black;" id="tdAddGroup" runat="server">
                                    <asp:Button ID="btnAddCountry" runat="server" OnClick="btnAddCountry_Click" Text="Add Group" />
                                    <asp:GridView ID="gvNationalityGroup" runat="server" AutoGenerateColumns="False"
                                        CaptionAlign="Bottom" CellPadding="4" CssClass="GridView-css" DataKeyNames="NationalityGroup_Id"
                                        EmptyDataText="No Record Found" GridLines="None" ShowHeaderWhenEmpty="true" Width="240px">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Nationality Group">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblNationalityGroup" runat="server" CommandArgument='<%# Container.DataItemIndex +","+Eval("NationalityGroup_Id")  %>'
                                                        Text='<%# Eval("NationalityGroup_Name") %>' OnCommand="GroupSelected"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit" CausesValidation="False"
                                                        CommandArgument='<%# Eval("NationalityGroup_Id") + "," + Eval("NationalityGroup_Name")%>'
                                                        OnCommand="EditNationality" ImageUrl="~/images/edit.gif" />
                                                    <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete" CausesValidation="False"
                                                        OnCommand="DeleteNationality" CommandArgument='<%# Eval("NationalityGroup_Id")%>'
                                                        ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to delete the record?')" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
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
                                </td>
                                <td style="vertical-align: top; width: 220px; color: Black;" id="tdNationality" runat="server">
                                    <asp:Button ID="btnCopyWages" runat="server" Text="Copy Wages For Other Nationality"
                                        OnClick="btnCopyWages_Click" Visible="false" Width="220px" />
                                    <asp:Label ID="lblNationalityHeader" runat="server" Text="Nationality:"></asp:Label>
                                    <asp:ListBox ID="lblNationality" runat="server" Width="180px" Height="670px" Enabled="false">
                                    </asp:ListBox>
                                </td>
                                <td style="vertical-align: top" id="tdDetails" runat="server">
                                    <div runat="server" id="divRankScale">
                                        Rank Scale :
                                        <asp:DropDownList ID="ddlRankScale" runat="server" Width="156px" OnSelectedIndexChanged="ddlRankScale_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <asp:Panel ID="pnlAddWages" runat="server" Visible="false">
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPrevEffectiveDate" runat="server" Text="Previous Effective Date:"
                                                        Visible="false" Style="padding-right: 10px; font-weight: bold;"></asp:Label>
                                                </td>
                                                <div runat="server" id="div2">
                                                    <td style="padding-right: 10px;">
                                                        Currency:
                                                    </td>
                                                    <td style="padding-right: 40px;" class="data">
                                                        <asp:DropDownList ID="ddlCurrencyType" runat="server" Enabled="false">
                                                        </asp:DropDownList>
                                                    </td>
                                                </div>
                                                <td style="padding-right: 40px;" class="data">
                                                    <asp:Label ID="lbleffdt" runat="server" Text="Effective Date:"></asp:Label>
                                                    <asp:TextBox ID="txteffdt" CssClass="textbox-css" runat="server"></asp:TextBox>
                                                    <tlk4:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txteffdt"
                                                        Format="dd/MM/yyyy">
                                                    </tlk4:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="aa" runat="server"
                                                        ControlToValidate="txteffdt" ErrorMessage="Please Enter Effective Date !!"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:GridView ID="GridViewaddwages" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                        DataKeyNames="WageContractID,Salary_Code" EmptyDataText="No Record Found" ForeColor="#333333"
                                                        GridLines="None">
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                                        <AlternatingRowStyle BackColor="White" CssClass="AlternatingRowStyle-css" ForeColor="#284775" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#F7F6F3" CssClass="RowStyle-css" ForeColor="#333333" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Earning/Deduction">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltype" runat="server" Text='<%#Eval("EarningDeduction") %>'></asp:Label>
                                                                    <asp:Label ID="lblVesselSpecific" runat="server" Text='<%#Eval("Vessel_Specific") %>'
                                                                        Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblentry_type" runat="server" Text='<%#Eval("name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Salary Type" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="180px">
                                                                <ItemTemplate>
                                                                    <asp:RadioButtonList ID="SalaryTypes" runat="server" DataSourceID="ObjectDataSourcePayableAt"
                                                                        DataTextField="name" DataValueField="code" Font-Size="11px" RepeatDirection="Horizontal"
                                                                        SelectedValue='<%# Eval("Salary_Type") %>' Enabled="false">
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payble At" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                                                                <ItemTemplate>
                                                                    <asp:RadioButtonList ID="PayableAT" runat="server" DataSourceID="ObjectDataSourcePayableAt"
                                                                        DataTextField="name" Enabled='<%# Eval("Vessel_Specific").ToString() == "True" ? false:true  %>'
                                                                        DataValueField="code" Font-Size="11px" RepeatDirection="Horizontal" SelectedValue='<%# Eval("Payable_At") %>'>
                                                                    </asp:RadioButtonList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CheckBoxField DataField="Vessel_Specific" HeaderText="Vessel Specific" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="numeric-edit" Visible='<%# Eval("Vessel_Specific").ToString() == "True" ? false:true  %>'
                                                                        EnableViewState="true" Font-Size="11px" Text='<%# String.Format("{0:0.00}", Eval("Amount"))%>'
                                                                        Width="40px"></asp:TextBox>
                                                                    <asp:LinkButton ID="lbtnEdit" runat="server" Text="Edit" Visible='<%# Convert.ToBoolean(Eval("Vessel_Specific"))  %>'
                                                                        CommandArgument='<%#Eval("Name")+ "," + Eval("Salary_Type")+ "," + Eval("Payable_At")+","+ Eval("Salary_Code")+","+ Eval("WageContractID") +","+  ((GridViewRow)Container).RowIndex + ",Edit" %>'
                                                                        OnCommand="EditAmt" />
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
                                                <tr>
                                                    <td colspan="4" style="text-align: right">
                                                        <asp:Button ID="btnsave" ClientIDMode="Static" runat="server" OnClick="btnsave_Click"
                                                            Text="Save" ValidationGroup="aa" />
                                                        <asp:Button ID="btncancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
                                                    </td>
                                                </tr>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlViewWages" runat="server" Visible="false">
                                        <fieldset style="text-align: left; margin-top: 5px; padding: 2px;">
                                            <legend>
                                                <asp:Label ID="Label3" runat="server">Existing Wage Assignments:</asp:Label>
                                            </legend>
                                            <div style="text-align: right">
                                                <asp:Button ID="btnAddWages" runat="server" Text="Revise Rank Wage Components" OnClick="btnAddWages_Click" />
                                            </div>
                                            <table cellpadding="3" cellspacing="1" style="width: 100%">
                                                <asp:Repeater runat="server" ID="rpt1" OnItemDataBound="rpt1_ItemDataBound" OnItemCommand="rpt1_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="5" style="font-weight: bold; color: Black">
                                                                Wages Effective From: &nbsp;&nbsp;<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("effective_date"))) %>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkEditWage" runat="server" Text="Edit" ForeColor="Blue" CommandName="Edit"
                                                                    CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr class="header">
                                                            <td colspan="6">
                                                                Created By: &nbsp;&nbsp;<%#Eval("Created_By") %>&nbsp;on&nbsp;
                                                                <%# UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("Date_Of_Creation"))) %><br />
                                                                Modified By: &nbsp;&nbsp;<%#Eval("Modified_By") %>&nbsp;on&nbsp;
                                                                <%# UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("Date_Of_Modification")))%>
                                                            </td>
                                                        </tr>
                                                        <tr style="background-color: #D8D8D8; color: Black; font-weight: bold;">
                                                            <td style="width: 50px">
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
                                                                Vessel Specific
                                                            </td>
                                                            <td style="text-align: right">
                                                                Amount
                                                            </td>
                                                        </tr>
                                                        <asp:Repeater runat="server" ID="rpt2" OnItemDataBound="rpt2_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr style="background-color: #CEF6F5; color: Black">
                                                                    <td>
                                                                        <asp:Label ID="lblEarningDeduction" runat="server" Text='<%#Eval("EarningDeduction") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("Name") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("Salary_Type_Name")%>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("Payable_At_Name")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkVesselSpecific" runat="server" Checked='<%#Eval("Vessel_Specific").ToString() == "True" ? true:false %>'
                                                                            Enabled="false" />
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("Amount","{0:0.00}")%>' Visible='<%# Eval("Vessel_Specific").ToString() == "True" ? false:true  %>'></asp:Label><%--((DataRowView)Container).RowIndex--%>
                                                                        <asp:LinkButton ID="lbtnEdit" runat="server" Text="View" Visible='<%# Convert.ToBoolean(Eval("Vessel_Specific"))  %>'
                                                                            CommandArgument='<%#Eval("Name")+ "," + Eval("Salary_Type")+ "," + Eval("Payable_At")+","+ Eval("Salary_Code")+","+ Eval("WageContractID") +","+ (Container.ItemIndex) +",View" %>'
                                                                            OnCommand="EditAmt" />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <%-- <FooterTemplate>
                                                                <tr style="background-color: #CEF6F5; color: Black; font-weight: bold;">
                                                                    <td colspan="5">
                                                                        Total
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <asp:Label ID="lblTotalAmt" runat="server" Text='<%# Eval("Amount","{0:0.00}")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </FooterTemplate>--%>
                                                        </asp:Repeater>
                                                        <tr>
                                                            <td colspan="6">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </fieldset>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <div style="color: Black">
                        BOC = Begining of Contract, MOC = Monthly, EOC = End of Contract, HRC = Hourly Rate</div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="upUpdateProgress" runat="server">
                <ProgressTemplate>
                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                        color: black">
                        <img src="../Images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
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
        </div>
        <asp:UpdatePanel ID="pnlAddCountries" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="divCountryList" style="font-family: Tahoma; color: black; display: none;">
                    <center>
                        <fieldset style="text-align: left; margin-top: 5px; padding: 2px;">
                            <legend>
                                <asp:Label ID="Label4" runat="server" Text="Nationality Group:"></asp:Label>
                            </legend>
                            <table>
                                <tr>
                                    <td>
                                        Name:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGroupName" runat="server" Width="200px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="vgAddCountry"
                                            runat="server" ControlToValidate="txtGroupName" ErrorMessage="Group name is mandatory !!"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSaveNationalityGroup" runat="server" Text="Save" OnClick="btnSaveNationalityGroup_OnClick"
                                            ValidationGroup="vgAddCountry" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div style="height: 550px; overflow: auto;">
                                            <%-- <asp:CheckBoxList ID="CheckBoxList1" runat="server"  DataTextField="COUNTRY"
                                            DataValueField="ID" >
                                        </asp:CheckBoxList>--%>
                                            <asp:CheckBoxList ID="chkCountryList" runat="server" DataTextField="COUNTRY" DataValueField="Country_ID"
                                                AutoPostBack="false">
                                            </asp:CheckBoxList>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </center>
                    <%-- <asp:Button ID="btnSelectCountry" runat="server" Text="Add Selected Countries" OnClick="btnAddSelectedCountries" />--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="pnlAddAmt" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="divVesselAllowance" style="font-family: Tahoma; color: black; display: none;">
                    <center>
                        <fieldset style="text-align: left; margin-top: 5px; padding: 2px;">
                            <legend>
                                <asp:Label ID="lblAllowances" runat="server" Text=""></asp:Label>
                            </legend>
                            <table>
                                <tr>
                                    <td colspan="6">
                                        <div class="error-message" onclick="javascript:this.style.display='none';">
                                            <asp:Label ID="lblVesselAllowanceMessage" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Vessel Type:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlVesselType" runat="server" Width="200px" OnSelectedIndexChanged="ddlVesselType_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator InitialValue="-1" ID="Req_ID" Display="Dynamic" ValidationGroup="V_AddAmt"
                                            runat="server" ControlToValidate="ddlVesselType" ErrorMessage="Please Select Vessel Type"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        Amount:
                                        <asp:TextBox ID="txtVesselAmount" runat="server" CssClass="numeric-edit" EnableViewState="true"
                                            Font-Size="11px" Text='0.00' Width="70px">
                                        </asp:TextBox>
                                        <asp:CompareValidator ID="ValidatortxtAmount1" runat="server" Type="Currency" Operator="DataTypeCheck"
                                            Display="None" InitialValue="" ControlToValidate="txtVesselAmount" ErrorMessage="Please enter a valid amount."
                                            ValidationGroup="V_Amt">
                                        </asp:CompareValidator>
                                        <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtAmount" TargetControlID="ValidatortxtAmount1"
                                            runat="server">
                                        </tlk4:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnPopulateAllowance" runat="server" Text="Populate Amount" OnClick="btnPopulateAllowance_OnClick"
                                            ValidationGroup="V_Amt" />
                                        <asp:Button ID="btnSaveAmt" runat="server" Text="Save" OnClick="btnSaveAmt_OnClick" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Salary Type:
                                    </td>
                                    <td colspan="3">
                                        <asp:RadioButtonList ID="SalaryTypeList" runat="server" DataSourceID="ObjectDataSourcePayableAt"
                                            DataTextField="name" DataValueField="code" Font-Size="11px" RepeatDirection="Horizontal"
                                            Enabled="false">
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Payable At:
                                    </td>
                                    <td colspan="3">
                                        <asp:RadioButtonList ID="PayableATList" runat="server" DataSourceID="ObjectDataSourcePayableAt"
                                            DataTextField="name" DataValueField="code" Font-Size="11px" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div style="height: 550px; overflow: auto;">
                                            <asp:GridView ID="gvAllowance" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                                                DataKeyNames="Vessel_ID" CaptionAlign="Bottom" CellPadding="4" GridLines="None"
                                                Width="100%" CssClass="gridmain-css">
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Vessel Type" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVesselTypeName" runat="server" Text='<%# Eval("Vessel_Type_Name") %>'
                                                                Enabled="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vessel" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVesselName" runat="server" Text='<%# Eval("Vessel_Name") %>' Enabled="false" />
                                                            <asp:Label ID="lblVesselId" runat="server" Text='<%# Eval("Vessel_ID") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtVesselAmount" runat="server" Text='<%# String.Format("{0:0.00}", Eval("Amount"))%>'
                                                                Width="50px"></asp:TextBox>
                                                            <asp:CompareValidator ID="ValidatortxtAmount" runat="server" Type="Currency" Operator="DataTypeCheck"
                                                                Display="None" InitialValue="" ControlToValidate="txtVesselAmount" ErrorMessage="Please enter a valid amount."
                                                                ValidationGroup="V_AddAmt">
                                                            </asp:CompareValidator>
                                                            <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtRJBAmount" TargetControlID="ValidatortxtAmount"
                                                                runat="server">
                                                            </tlk4:ValidatorCalloutExtender>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </center>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--   -----%>
        <asp:UpdatePanel ID="pnlAddWagesForCountries" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="divCountryList1" style="font-family: Tahoma; color: black; display: none;">
                    <center>
                        <div style="padding: 0px; padding: 2px; border-top: 0; background-color: #5588BB;
                            color: #FFFFFF; text-align: center;">
                            <b>Countries</b>
                        </div>
                    </center>
                    <table>
                        <tr>
                            <td style="padding-right: 10px;">
                                Copy Wages From :
                            </td>
                            <td>
                                <b>
                                    <asp:Label ID="lblCountryName" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px;">
                                Currency:
                            </td>
                            <td style="padding-right: 40px;" class="data">
                                <asp:DropDownList ID="ddlCurrencyType1" runat="server" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px;">
                                Effective Date:
                            </td>
                            <td style="padding-right: 40px;" class="data">
                                <asp:TextBox ID="txteffdt1" CssClass="textbox-css" runat="server"></asp:TextBox>
                                <tlk4:CalendarExtender ID="CalendarExtender51" runat="server" TargetControlID="txteffdt1"
                                    Format="dd/MM/yyyy">
                                </tlk4:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="vgAddWages"
                                    runat="server" ControlToValidate="txteffdt1" ErrorMessage="Please Enter Effective Date !!"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnAddWagesForSelectedCountries" runat="server" Text="Add Wages For Selected Countries"
                                    ValidationGroup="vgAddWages" OnClick="btnAddWagesForSelectedCountries_OnClick" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script type="text/javascript">
            $(document).ready(function () {
                $("body").on("click", "#btnsave", function () {
                    if ($.trim($("#<%=txteffdt.ClientID %>").val()) == "") {
                        alert("Enter Effective Date");
                        $("#<%=txteffdt.ClientID %>").focus();
                        return false;
                    }
                    if (IsInvalidDate($.trim($("#<%=txteffdt.ClientID %>").val()), '<%= UDFLib.GetDateFormat() %>')) {
                        alert("Enter valid Effective Date<%=UDFLib.DateFormatMessage() %>");
                        $("#<%=txteffdt.ClientID %>").focus();
                        return false;
                    }
                });
            });
        </script>
</asp:Content>
