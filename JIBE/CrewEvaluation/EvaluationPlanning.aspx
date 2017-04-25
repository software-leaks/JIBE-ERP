<%@ Page Title="Evaluation Planning" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="EvaluationPlanning.aspx.cs" Inherits="CrewEvaluation_EvaluationPlanning" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/crew_interview_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
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
        .watermarked
        {
            color: #cccccc;
        }
        
        .linkbtn a
        {
            color: Black;
        }
        
        .linkbtn
        {
            color: black;
            background-color: white;
            text-decoration: none;
            border-left: 1px solid #cccccc;
            padding-left: 10px;
            border-top: 1px solid #cccccc;
            padding-top: 5px;
            border-right: 1px solid #cccccc;
            padding-right: 10px;
            border-bottom: 1px solid #cccccc;
            padding-bottom: 3px;
            background-color: #F1F8E0;
        }
        input
        {
            font-family: Tahoma;
            font-size: 12px;
        }
    </style>
    <style id="TooltipStyle" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: Tahoma;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .grid-row
        {
            padding: 6px;
        }
        .grid-col-fixed
        {
            border: 1px solid #cccccc;
        }
        .grid-col
        {
            border: 1px solid #cccccc;
        }
        .gradiant-css-browne
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#81F79F',EndColorStr='#088A4B');
            background: -webkit-gradient(linear, left top, left bottom, from(#81F79F), to(#088A4B));
            background: -moz-linear-gradient(top,  #81F79F,  #088A4B);
            color: Black;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function showDiv(dv) {
            document.getElementById(dv).style.display = "block";
        }
        function showDiv(dv) {
            document.getElementById('dvAddNewEvaluation').title = "Add New Evaluation"
            showModal(dv);
        }
        function closeDiv(dv) {
            hideModal(dv);
        }
        $(document).ready(function () {
            $('.draggable').draggable();
        });

        function ValidateEvaluationLocation() {

            var chkOff = document.getElementById('ctl00_MainContent_chkOffice');
            var txtNotifyDays = document.getElementById('ctl00_MainContent_txtNotify');
            var ddlEvaluator = document.getElementById('ctl00_MainContent_ddlEvaluator');
            if (ddlEvaluator != null) {
                if (ddlEvaluator.value == '-1') {

                    alert("Select Valid Evaluator");

                    return false;

                }
            }
          
                if (chkOff.checked == true) {

                    if (txtNotifyDays != null) {
                        if (txtNotifyDays.value == '') {
                            alert("Enter numeric value for Notify Before");

                            return false;

                        }
                        else if (isNaN(txtNotifyDays.value)) {

                            alert("Enter numeric value for Notify Before");

                            return false;
                        }
                    }
                }
            
        }       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
       Evaluation Planning
    </div>
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


    <div id="page-content" style="height: 620px; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto;">
        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
            <ContentTemplate>
                <div style="text-align: center">
                    <table border="0">
                        <tr>
                            <td style="text-align: left">
                                Search:
                            </td>
                            <td>
                                <asp:TextBox ID="txtfilter" runat="server" CssClass="textbox-css" AutoPostBack="true"
                                    OnTextChanged="txtfilter_TextChanged"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                    WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%" border="0">
                        <tr>
                            <td style="text-align: right">
                                <asp:LinkButton ID="lnkAddNewEvaluation" runat="server" CssClass="linkbtn" OnClientClick="showDiv('dvAddNewEvaluation');return false;">Create New Evaluation</asp:LinkButton>
                            </td>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView_Evaluation" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        CaptionAlign="Bottom" CellPadding="4" DataKeyNames="Evaluation_ID" EmptyDataText="No Record Found"
                                        ForeColor="#333333" GridLines="None" OnRowCancelingEdit="GridView_Evaluation_RowCancelEdit"
                                        OnRowDataBound="GridView_Evaluation_RowDataBound" OnRowDeleting="GridView_Evaluation_RowDeleting"
                                        OnRowEditing="GridView_Evaluation_RowEditing" OnRowUpdating="GridView_Evaluation_RowUpdating"
                                        OnSorted="GridView_Evaluation_Sorted" OnSorting="GridView_Evaluation_Sorting"
                                        Width="100%" CssClass="gridmain-css">
                                       <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                       <RowStyle CssClass="RowStyle-css" />
                                       <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" SortExpression="Evaluation_ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("Evaluation_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Evaluation Name" SortExpression="Evaluation_Name"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEvaluation_Name" runat="server" Text='<%#Eval("Evaluation_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEvaluation_Name" runat="server" Font-Size="11px" MaxLength="1000"
                                                        Width="300px" Text='<%#Bind("Evaluation_Name")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of Criteria" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCriteria_Count" runat="server" Text='<%#Eval("Criteria_Count")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Criteria" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCriteria" runat="server"></asp:Label>
                                                    <asp:Button ID="btnSelectCriteria" runat="server" Text="Add/Remove Criteria" CommandArgument='<%#Eval("Evaluation_ID") %>'
                                                        OnClick="btnSelectCriteria_Click" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Scheduling" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnScheduling" runat="server" CommandArgument='<%#Eval("Evaluation_ID")%>'
                                                        Text="Evaluation Scheduling" OnClick="btnScheduling_Click" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="50px">
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnAccept" ToolTip="Update" runat="server" AlternateText="Update" CausesValidation="False"
                                                        CommandName="Update" ImageUrl="~/images/accept.png" />
                                                    <asp:ImageButton ID="ImgBtnCancel" ToolTip="Cancel" runat="server" AlternateText="Cancel" CausesValidation="False"
                                                        CommandName="Cancel" ImageUrl="~/images/reject.png" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" AlternateText="Edit" CausesValidation="False"
                                                        CommandName="Edit" ImageUrl="~/images/edit.gif" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" AlternateText="Delete" CausesValidation="False"
                                                        CommandName="Delete" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to  delete ?')" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#58FA82" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvEvaluationScheduling" class="draggable" style="display: none; background-color: #CBE1EF;
            border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
            left: 30%; top: 10%; width: 600px; z-index: 1; color: black">
            <div class="header">
                <h4>
                    Evaluation Scheduling
                </h4>
            </div>
            <div class="content">
                <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; background-color: White; cursor: default;">
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: left; font-weight: bold">
                                    Evaluation Name:
                                    <asp:DropDownList ID="ddlEvaluations" runat="server" DataSourceID="objDS_EvaluationList"
                                        DataTextField="Evaluation_Name" DataValueField="Evaluation_ID" Width="300px">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="objDS_EvaluationList" runat="server" SelectMethod="Get_Evaluations"
                                        TypeName="SMS.Business.Crew.BLL_Crew_Evaluation"></asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Already Selected Ranks
                                </td>
                                <td style="font-size: 11px; text-align: left; font-weight: bold; vertical-align: top;">
                                    Frequency
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #cccccc; vertical-align: top;">
                                    <div style="overflow: auto; height: 300px; background-color: #">
                                        <asp:GridView ID="GridView_Selected" runat="server" DataTextField="Rank_Name" OnSelectedIndexChanged="RankList_SelectedIndexChanged"
                                            AutoGenerateColumns="false" DataKeyNames="ID" GridLines="Horizontal" ShowHeader="false"
                                            OnRowDeleting="GridView_Selected_RowDeleting" Width="300px">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgSelect" runat="server" ImageUrl="~/images/ok-icon.png" CausesValidation="False"
                                                            CommandName="Select" AlternateText="Select"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="False" CommandName="Select"
                                                            AlternateText="Select"><%#Eval("Rank_Name") %></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="LinkButton1del" runat="server" AlternateText="Delete" CausesValidation="False"
                                                            CommandName="Delete" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to  delete the scheduling ?')" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="#F5D0A9" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                            <SortedAscendingHeaderStyle BackColor="#487575" />
                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                            <SortedDescendingHeaderStyle BackColor="#275353" />
                                        </asp:GridView>
                                    </div>
                                    <div style="font-size: 11px; text-align: left; font-weight: bold; padding: 5px; background-color: #efdfef;
                                        margin-top: 10px;">
                                        Ranks not yet selected
                                    </div>
                                    <div style="overflow: auto; height: 300px;">
                                        <asp:GridView ID="GridView_NotSelected" runat="server" DataTextField="Rank_Name"
                                            OnSelectedIndexChanged="RankList_SelectedIndexChanged" AutoGenerateColumns="false"
                                            DataKeyNames="ID" GridLines="Horizontal" ShowHeader="false" Width="300px">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgSelect" runat="server" ImageUrl="~/images/select1.gif" CausesValidation="False"
                                                            CommandName="Select" AlternateText="Select"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="False" CommandName="Select"
                                                            AlternateText="Select"><%#Eval("Rank_Name") %></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                            <SortedAscendingHeaderStyle BackColor="#487575" />
                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                            <SortedDescendingHeaderStyle BackColor="#275353" />
                                        </asp:GridView>
                                    </div>
                                </td>
                                <td style="border: 1px solid #cccccc; vertical-align: top; height: 500px;">
                                    <div style="overflow: auto; padding: 10px;">
                                        <div style="font-size: 11px; text-align: left; font-weight: bold; vertical-align: top;
                                            background-color: #00FFBF; padding: 2px;">
                                            Recurssive on selected month(s)
                                        </div>
                                        <asp:CheckBoxList ID="lstSelectedMonth" CssClass="textbox-css" runat="server" RepeatColumns="3"
                                            RepeatDirection="Horizontal" CellPadding="2">
                                            <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                        </asp:CheckBoxList>
                                        <div style="font-size: 11px; text-align: left; font-weight: bold; vertical-align: top;
                                            margin-top: 10px; background-color: #00FFBF; padding: 2px;">
                                            Based on the Rule:
                                        </div>
                                        <asp:CheckBoxList ID="lstSelectedRules" CssClass="textbox-css" runat="server" RepeatDirection="Vertical"
                                            CellPadding="2">
                                            <asp:ListItem Text="15 Days after Joining" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="1 Month after Joining" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="3 Months after Joining" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="6 Months after Joining" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="End of Contract" Value="3"></asp:ListItem>
                                        </asp:CheckBoxList>

                                         <div style="font-size: 11px; text-align: left; font-weight: bold; vertical-align: top;
                                            margin-top: 10px; background-color: #00FFBF; padding: 2px;">
                                            Evaluation Location:
                                        </div>
                                        <div style="margin-top: 5px;">
                                            <asp:CheckBox ID="chkOffice" runat="server" Text="Office" CssClass="textbox-css"
                                                OnCheckedChanged="chkOffice_CheckedChanged" AutoPostBack="True" />
                                        </div>
                                        <asp:Label ID="lblEvaluator" runat="server" Text="Evaluator:" CssClass="textbox-css" Visible="false"></asp:Label><asp:Label id="lbleman" runat="server" style="color:Red;" >*</asp:Label>
                                        <div style=" margin-top: 5px;">
                                           <asp:DropDownList ID="ddlEvaluator" runat="server" Width="150px" CssClass="textbox-css"
                                                Visible="false">
                                            </asp:DropDownList>
                                        </div>
                                      
                                       <asp:Label ID="lblNotify" runat="server" Text="Notify Before:" CssClass="textbox-css" Visible="false"></asp:Label><asp:Label id="lblnman" runat="server" style="color:Red;" >*</asp:Label>
                                        <div style="margin-top: 5px;">
                                          <asp:TextBox ID="txtNotify" runat="server" Visible="false" ></asp:TextBox>  <asp:Label ID="lblDays" runat="server" Text="(days)" CssClass="textbox-css" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveSchedule" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnSaveSchedule_Click" OnClientClick="return ValidateEvaluationLocation();" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndCloseSchedule" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndCloseSchedule_Click" OnClientClick="return ValidateEvaluationLocation();" />&nbsp;&nbsp;
                                    <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDiv('dvEvaluationScheduling')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="dvAddNewEvaluation" title="Add New Evaluation" class="modal-popup-container"
               style="width: 550px; left: 40%; top: 30%;">

           
            <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; background-color: White; cursor: default;">
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Evaluation Name:
                                </td>
                                <td style="border-width: 1px">
                                    <asp:TextBox ID="txtEvaluation_Name" CssClass="textbox-css" Width="400px" runat="server"
                                        MaxLength="1000"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveEvaluation" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnSaveEvaluation_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndCloseEvaluation" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndCloseEvaluation_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="Button3" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDiv('dvAddNewEvaluation')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div
    </div>
</asp:Content>
