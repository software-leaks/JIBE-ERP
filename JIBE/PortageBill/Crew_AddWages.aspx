<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Crew_AddWages.aspx.cs"
    Inherits="Account_Portage_Bill_Crew_AddWages" Title="Add Wages" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
     <link href="../Styles/Common.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
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
    <script type="text/javascript">
        function ShowUpdateSeniority(VoyID) {
            showModal('dvUpdateSeniority');
        }
        var lastExecutor_WebServiceProxy = null;
        function ShowReJoining_History(ID, evt, objthis) {

            if (lastExecutor_WebServiceProxy != null)
                lastExecutor_WebServiceProxy.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_RJBDetails', false, { "ID": ID }, ShowReJoining_History_onSuccess, ShowReJoining_History_onFail, new Array(evt, objthis));

            lastExecutor_WebServiceProxy = service.get_executor();
            //showModal('dvRJB');
        }
        function ShowReJoining_History_onSuccess(retval, eventArgs) {

            js_ShowToolTip(retval, eventArgs[0], eventArgs[1]);
        }
        function ShowReJoining_History_onFail(err_) {
            alert(err_._message);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
 
  <div class="page-title">
           Crew Wages
    </div>
    <div id="Div1" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 5px;">
        <div id="page-content">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="error-message" onclick="javascript:this.style.display='none';">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                    </div>
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>
                            <asp:Label ID="Label1" runat="server">Salary Details for Crew</asp:Label>
                        </legend>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="padding-right: 10px;">
                                    Staff Code:
                                </td>
                                <td style="padding-right: 40px;" class="data">
                                    <asp:Label ID="lblStaffCode" runat="server" Font-Bold="true" ForeColor="Navy" Font-Size="11px"></asp:Label>
                                </td>
                                <td style="padding-right: 10px;">
                                    Staff Name:
                                </td>
                                <td style="padding-right: 40px;" class="data">
                                    <asp:Label ID="lblStaffName" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div id="dvGrid_Voyages" style="margin-top: 5px">
                            <asp:GridView ID="GridView_Voyages" runat="server" DataKeyNames="ID" CellPadding="4"
                                CssClass="GridView-css" GridLines="None" Width="100%" Font-Size="11px" AutoGenerateColumns="False"
                                DataSourceID="objDS_CrewVoyages" AllowSorting="false" ForeColor="#333333"  OnRowDataBound="GridView_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel" SortExpression="VESSEL_SHORT_NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselName" runat="server" Text='<%# Eval("VESSEL_SHORT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Joining Rank" SortExpression="Rank_Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRankId" runat="server" Text='<%# Eval("Joining_Rank") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblJoiningRank" runat="server" Text='<%# Eval("Rank_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Joining Date" SortExpression="Joining_Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJoiningDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Joining_Date"))) %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sign On Date" SortExpression="Sign_On_Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSignOnDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Sign_On_Date"))) %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sign On Port" SortExpression="Joining_Port">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJoiningPort" runat="server" Text='<%# Eval("PORT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="COC Date" SortExpression="COCDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCOCDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("COCDate"))) %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sign Off Date" SortExpression="Sign_Off_Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSignOffDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Sign_Off_Date"))) %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sign Off Port" SortExpression="Sign_Off_Port">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSignOffPort" runat="server" Text='<%# Eval("Sign_Off_Port_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sign Off Reason">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSignOffReason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RankSeniorityYear" HeaderText="Seniority Year" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <a href='../Crew/CrewContract.aspx?CrewID=<%#Eval("CrewID")%>&VoyID=<%#Eval("ID")%>'
                                                target="_blank">Print Contract</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle CssClass="RowStyle-css" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                        <asp:ObjectDataSource ID="objDS_CrewVoyages" runat="server" SelectMethod="Get_CrewVoyages"
                            TypeName="SMS.Business.Crew.BLL_Crew_CrewDetails">
                            <SelectParameters>
                                <asp:QueryStringParameter DefaultValue="" Name="ID" QueryStringField="CrewID" Type="Int32" />
                                <asp:QueryStringParameter DefaultValue="" Name="VoyageID" QueryStringField="VoyID"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </fieldset>
                    <asp:Panel ID="pnlViewWages" runat="server">
                        <fieldset style="text-align: left; margin-top: 5px; padding: 2px;">
                            <legend>
                                <asp:Label ID="Label3" runat="server">View Wages:</asp:Label>
                            </legend>
                            <table cellpadding="3" cellspacing="1">
                                <asp:Repeater runat="server" ID="rpt1" OnItemDataBound="rpt1_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                Salary effective from
                                            </td>
                                            <td>
                                                <%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("effective_date")))%>
                                            </td>
                                            <td >
                                                <asp:Label ID="lblRankScale" runat="server"  Text="Rank Scale" />

                                            </td>
                                            <td >
                                                <asp:Textbox ID="txtRankScale" runat="server"  Text='<%#Eval("RankScaleName")%>' Enabled="false" />
                                                
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
                            <table>
                                <tr style="font-size: 14px; color: Black; background-color: Yellow;">
                                    <td style="width: 120px">
                                        Side Letter
                                    </td>
                                    <td style="width: 130px">
                                    </td>
                                    <td style="width: 70px">
                                    </td>
                                    <td style="width: 50px">
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSideLetterAmount" runat="server" Text="0.00" Width="70px" CssClass="numeric-edit"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlEditWages" runat="server">
                                <div style="text-align: center">
                                    <asp:Button ID="btnEditWages" runat="server" Text="Edit Wages" OnClick="btnEditWages_Click" />
                                    <asp:Button ID="btnAddWages" runat="server" Text="Revise Wages" OnClick="btnAddWages_Click" />
                                </div>
                            </asp:Panel>
                        </fieldset>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel_EditWages" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlAddWages" runat="server" Visible="false">
                        <fieldset style="text-align: left; margin-top: 5px; padding: 2px;">
                            <legend>
                                <asp:Label ID="lblAddEditWages" runat="server" Text="Add Wages:"></asp:Label>
                            </legend>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPrevEffectiveDate" runat="server" Text="Previous Effective Date:"
                                            Visible="false" Style="padding-right: 10px; font-weight: bold;"></asp:Label>
                                    </td>
                                    <td style="padding-right: 10px;">
                                        Currency:
                                    </td>
                                    <td style="padding-right: 40px;" class="data">
                                        <asp:DropDownList ID="ddlCurrencyType" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRankScale" runat="server" Text="Rank Scale:"></asp:Label>
                                        <asp:DropDownList ID="ddlRankScale" runat="server"  Width="100px" OnSelectedIndexChanged="ddlRankScale_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="padding-right: 10px;">
                                        <asp:Label ID="lblincris" runat="server" Text="Is this an increment?"></asp:Label>
                                        <asp:CheckBox ID="chkbinc" runat="server" AutoPostBack="true" />
                                    </td>
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
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hdWageContractId" runat="server" />
                                        <asp:GridView ID="GridViewaddwages" DataKeyNames="code" CssClass="GridView-css" runat="server"
                                            AutoGenerateColumns="False" EmptyDataText="No Record Found" CellPadding="4" ForeColor="#333333"
                                            GridLines="None" OnRowDataBound="GridViewaddwages_RowDataBound">
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Earning/Deduction">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltype" runat="server" Text='<%#Eval("EarningDeduction") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblentry_type" runat="server" Text='<%#Eval("name") %>'></asp:Label>
                                                        <asp:Label ID="lblKey_Value" runat="server" Text='<%#Eval("Key_Value") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Salary Type" ItemStyle-Width="180px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList ID="SalaryTypes" runat="server" DataSourceID="ObjectDataSourcePayableAt"
                                                            DataTextField="name" DataValueField="code" Font-Size="11px" RepeatDirection="Horizontal"
                                                            SelectedValue='<%# Eval("DFSalary_Type") %>' Enabled ="false">
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
                                                            CssClass="numeric-edit" EnableViewState="true"  ToolTip='<%# Convert.ToInt32(Eval("OnBoardEntry"))==1?"Amount will be added on vessel":"" %>'></asp:TextBox>
                                                        <asp:LinkButton ID="lnkUpdateSeniority" runat="server" Visible="false" Text="Edit"></asp:LinkButton>
                                                        <asp:Image ID="imgInfo" runat="server" Visible="false" ImageUrl="~/Images/RecordInformation.png" Height="16px" Width="16px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                        <table style="font-size: 14px; color: Black; background-color: Yellow;">
                                            <tr>
                                                <td style="width: 110px">
                                                    Earning
                                                </td>
                                                <td style="width: 150px">
                                                    <asp:Label ID="lblSideLetter" runat="server" Text="Side Letter"></asp:Label>
                                                </td>
                                                <td style="width: 190px">
                                                    MOC
                                                </td>
                                                <td style="width: 170px">
                                                    EOC
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSideLetterAmount" runat="server" Text="0.00" Width="70px" CssClass="numeric-edit"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnsave" runat="server" ValidationGroup="aa" Text="Save" OnClick="btnsave_Click" />
                                        <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                    <div style="color: Black">
                        BOC = Begining of Contract, MOC = Monthly, EOC = End of Contract</div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnEditWages" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnAddWages" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
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
    </div>
    <asp:Panel ID="pnlContract_" runat="server" Visible="false">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:Repeater runat="server" ID="rptContractWages" OnItemCreated="rptContractWages_ItemCreated">
                    <ItemTemplate>
                        <tr style="background-color: #CEF6F5; color: Black">
                            <td>
                                <%#Eval("EarningDeduction")%>
                            </td>
                            <td>
                                <%#Eval("Name") %>
                            </td>
                            <td style="text-align: right">
                                USD
                            </td>
                            <td style="text-align: right">
                                <%# Eval("Amount", "{0:00.00}")%>
                            </td>
                            <td>
                                &nbsp;&nbsp;<%#Eval("PayableAt").ToString().Replace("BOC", "One-Time").Replace("MOC", "Per Month").Replace("EOC", "One-Time")%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr style="background-color: #CEF6F5; color: Black; font-weight: bold;">
                            <td colspan="2">
                                Total
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lblCurrency" runat="server" Text="USD"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lblTotalAmt" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;&nbsp;<asp:Label ID="lblPerMonth" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <div id="dvUpdateSeniority" style="display: none;" title="Select New Seniority">
        <asp:UpdatePanel ID="UpdatePanel_UpdateSeniority" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width:100%">
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdnCurrentSeniority" runat="server" />
                            <asp:HiddenField ID="hdnSelectedSeniority" runat="server" />
                            <asp:GridView ID="gvNewSeniority" runat="server" DataKeyNames="SeniorityYear" AutoGenerateColumns="False" CellPadding="4"
                                OnSelectedIndexChanged="gvNewSeniority_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="SeniorityYear" HeaderText="Seniority Year" ItemStyle-Width="150px"  ItemStyle-HorizontalAlign="Center"/>
                                    <asp:BoundField DataField="SeniorityAmount" HeaderText="Amount" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right"/>
                                    <asp:TemplateField ShowHeader="False" HeaderText="Select"  ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkSelect" runat="server" ImageUrl="~/images/select1.gif" CausesValidation="False"
                                                CommandName="Select" AlternateText="Select"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label runat="server" ID="lblMsg" Text="Seniority rates are not set for this rank"></asp:Label>
                                </EmptyDataTemplate>
                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="White" ForeColor="#333333"  />
                                <SelectedRowStyle BackColor="#F7BE81" Font-Bold="True" ForeColor="Black" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#487575" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#275353" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>Remarks
                            <asp:TextBox ID="txtRankSeniorityRemarks" TextMode="MultiLine" Width="350px" Height="50px"
                                                            runat="server" CssClass="control-edit required uppercase"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRemarks" runat="server"
                                        ValidationGroup="validate" Display="None" ErrorMessage="Remarks is mandatory!"
                                        ControlToValidate="txtRankSeniorityRemarks" InitialValue=""></asp:RequiredFieldValidator>
                                    <tlk4:ValidatorCalloutExtender ID="v1"
                                        TargetControlID="rfvRemarks" runat="server">
                                    </tlk4:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnUpdateSeniority" runat="server" Text="Save" OnClick="btnUpdateSeniority_Click"  ValidationGroup="validate"/>
                            <asp:Button ID="btnCancelSeniorityUpdate" runat="server" Text="Cancel" OnClick="btnCancelSeniorityUpdate_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvRJB" style="display: none;" title="Rejoining Bonus Details">
    
    </div>
</asp:Content>
