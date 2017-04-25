<%@ Page Title="Add Note / Observation" Language="C#" AutoEventWireup="true" CodeFile="Vetting_AddObservationNotes.aspx.cs"
    Inherits="Technical_Vetting_Vetting_AddObservationNotes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <style type="text/css">
        .ihyperlink img
        {
            height: 16px;
        }
        body
        {
            font-family: Tahoma;
            font-size: 12px;
            margin: 0;
            padding: 0;
            background-color: #F2F2F2;
        }
        .img
        {
            margin-right: 10px;
            margin-top: 5px;
        }
        
        .badge
        {
            background-image: url(../../Images/VET_Error.png);
            width: 33px;
            height: 28px;
            background-repeat: no-repeat;
            display: block;
            position: relative;
        }
        .badge div
        {
            background: red none repeat scroll 0 0;
            border-radius: 50px;
            bottom: 0;
            color: #fff;
            display: inline;
            float: right;
            font-size: 8px;
            height: 12px;
            padding: 3px;
            position: absolute;
            right: 0;
            text-align: center;
            width: 12px;
        }
        .badge div a
        {
            text-decoration: none;
        }
        .txt
        {
            margin-left: 3px;
        }
    </style>
    <script language="javascript" type="text/javascript">



        function ValidateObs() {

            var ddlSection = document.getElementById($('[id$=ddlSection]').attr('id')).value;
            if (ddlSection == "") {

                ddlSection = "0";
            }
            if (ddlSection == "0") {
                alert("Select the Section.");
                $("#" + $('[id$=ddlSection]').attr('id')).focus();
                return false;
            }

            var ddlQuestion = document.getElementById($('[id$=ddlQuestion]').attr('id')).value;
            if (ddlQuestion == "") {

                ddlQuestion = "0";
            }
            if (ddlQuestion == "0") {
                alert("Select the Question.");
                $("#" + $('[id$=ddlQuestion]').attr('id')).focus();
                return false;
            }

            var txtObsDescription = document.getElementById($('[id$=txtObsDescription]').attr('id')).value;
            if (txtObsDescription == "") {
                alert("Enter Observation Description.");
                $("#" + $('[id$=txtObsDescription]').attr('id')).focus();
                return false;
            }


            return true;
        }

        function PopupAddResponse(Observation_ID) {
            document.getElementById('IframeAddResponse').src = "VET_AddResponse.aspx?Observation_ID=" + document.getElementById($('[id$=hdnQryStrObservationId]').attr('id')).value;
            $("#dvPopupAddResponse").prop('title', 'Add Response');
            showModal('dvPopupAddResponse', true, saveCloseReponseChild);
        }

        function saveCloseReponseChild() {

            hideModal('dvPopupAddResponse');
            document.getElementById($('[id$=BtnResHidden]').attr('id')).click();
        }
        function saveCloseReponseAttachChild() {

            hideModal('dvPopupAddResponseAttachment');
            document.getElementById($('[id$=BtnResAttHidden]').attr('id')).click();
        }
        function PopupAddResponseAttachment(Response_ID) {
            document.getElementById('IframeAddResponseAttachment').src = "VET_ResponseAttachment.aspx?Response_ID=" + Response_ID;

            $("#dvPopupAddResponseAttachment").prop('title', 'Response Attachment');
            showModal('dvPopupAddResponseAttachment');
            return false;

        }

        function PopupAssignJob(Observation_ID, Vessel_ID) {
            document.getElementById('IframeAssignJob').src = "Vetting_AssignJobs.aspx?Observation_ID=" + Observation_ID + "&Vessel_ID=" + Vessel_ID;

            $("#dvPopupAssignJob").prop('title', 'Assign Job');
            showModal('dvPopupAssignJob');
            return false;

        }
        function saveCloseChild() {

            hideModal('dvPopupAssignJob');
            document.getElementById($('[id$=btnJobHidden]').attr('id')).click();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="font-family: Tahoma; padding: 5px; padding-right: 5px; font-size: 12px;">
        <asp:UpdatePanel ID="UpdPnlAddObservation_Naote" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td align="left" style="width: 12%; height:35px;">
                            <asp:Label ID="lblSection" runat="server" Text="Section:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" style="width: 25%;">
                            <asp:Label ID="lblSectionMandatry" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                            <asp:DropDownList ID="ddlSection" runat="server" Width="160" CssClass="txt" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 10%;">
                            <asp:Label ID="lblCategory" runat="server" Text="Category:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlCategory" runat="server" Width="160">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style=" height:35px;">
                            <asp:Label ID="lblQuestionNo" runat="server" Text="Question:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblQuestionMandatry" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                        <asp:DropDownList ID="ddlQuestion" runat="server" Width="160" OnSelectedIndexChanged="ddlQuestion_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <div class="badge" runat="server" id="dvbadge">
                                            <div>
                                                <asp:HyperLink ID="lnkRelatedObs" runat="server" ForeColor="White" Height="12px"
                                                    Target="_blank" ToolTip="Observations"></asp:HyperLink>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblRiskLevel" runat="server" Text="Risk Level:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:DropDownList ID="ddlRiskLevel" runat="server" Width="160">
                                <asp:ListItem Selected="True" Value="0">-Select-</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style=" height:35px;">
                            <asp:Label ID="lblType" runat="server" Text="Type:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlType" runat="server" Width="160" Style="margin-left: 12px">
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblStatus" runat="server" Text="Status:" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="160">
                                <asp:ListItem Value="1" Selected="True">Open</asp:ListItem>
                                <asp:ListItem Value="2">Closed</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style=" height:35px;">
                            <asp:Label ID="Label2" runat="server" Text="Question Description:" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="3">
                            &nbsp; <asp:Label ID="lblQuestion" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="5" style=" height:35px;">
                            <asp:Label ID="lblObservation" runat="server" Text="Observation:" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; vertical-align: middle; padding: 20px; vertical-align: middle;
                            background-color: White;" colspan="5">
                            <asp:TextBox Width="97%" ID="txtObsDescription" Enabled="false" runat="server" TextMode="MultiLine"
                                Height="60px"></asp:TextBox>
                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" CommandName="EditButton"
                                ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px" OnClick="ImgUpdate_Click">
                            </asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4" style="padding-top:19px;">
                            <asp:Label ID="lblResponse" runat="server" Text="Reponses:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
               
                            <asp:Button ID="BtnResHidden" runat="server" Style="visibility: hidden" OnClick="BtnResHidden_Click" />                   
                            <asp:Button  ID="ImgAddResponse" runat="server" Text="Add Response"  style="float:right; font-size:11px;" OnClientClick="return PopupAddResponse();"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="padding: 20px; background-color: White; vertical-align: middle;">
                            <div style="overflow-y: auto; height: 150px;">
                                <asp:GridView ID="gvResponse" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found."
                                    CellPadding="2" Width="100%" GridLines="None" CssClass="GridView-css" Style="padding-left: 5px;"
                                    OnRowDataBound="gvResponse_RowDataBound" ShowHeaderWhenEmpty="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCDate" runat="server" Text='<%# Eval("Date_Of_creation") %>'></asp:Label>
                                                <asp:Label ID="lblRespID" runat="server" Visible="false" Text='<%# Eval("Response_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                            <HeaderStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Created By" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("Created_By") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                            <HeaderStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Response" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblResponse" runat="server" Text='<%# Eval("Response") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="75%" />
                                            <HeaderStyle Width="75%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgAttatch" runat="server" Text="Attachments" ForeColor="Black"
                                                    ToolTip="Attachments" ImageUrl="~/Images/attach-icon.png" Height="16px" OnClick='<%# "PopupAddResponseAttachment(&#39;"+Eval("Response_ID").ToString()+"&#39;)" %>'>
                                                </asp:ImageButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" Width="5%" />
                                            <HeaderStyle HorizontalAlign="center" Width="5%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblRelatedJobs" runat="server" Text="Related Jobs:" Font-Bold="true" style="padding-bottom:15px;"></asp:Label>
                        </td>
                        <td colspan="2" align="right">
                            <asp:ImageButton ID="ImglnkJob" runat="server" ImageAlign="Right" ForeColor="Black"
                                ToolTip="Assign Job" ImageUrl="~/Images/VET_AssignJob.png" Height="14px" CssClass="img">
                            </asp:ImageButton>
                            <asp:Button ID="btnJobHidden" runat="server" Style="visibility: hidden" OnClick="btnJobHidden_Click" />
                        </td>
                        <td style="width: 1%">                       
                            <asp:Button  ID="ImgAddNewJob" runat="server" ToolTip="Add New Job"  Text="Add New Job"
                                style="float:right; font-size:11px;" onclick="ImgAddNewJob_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="padding: 20px; background-color: White; vertical-align: middle;">
                            <div style="overflow-y: auto; height: 150px;">
                                <asp:GridView ID="gvRelatedJob" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record found."
                                    CellPadding="2" Width="100%" GridLines="None" CssClass="GridView-css" Style="padding-left: 5px;"
                                    OnRowDataBound="gvRelatedJob_RowDataBound" ShowHeaderWhenEmpty="true" OnRowCommand="gvRelatedJob_RowCommand">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkVerJobsCnt" runat="server" Text='<%# Eval("Job_card_No") %>'
                                                    ToolTip="View Job" Target="_blank"></asp:HyperLink>
                                                <asp:HiddenField ID="hdnWLID" runat="server" Value='<%# Eval("Job_History_ID") %>' />
                                                <asp:HiddenField ID="hdnOFFID" runat="server" Value='<%# Eval("Office_ID") %>' />
                                                <asp:HiddenField ID="hdnVID" runat="server" Value='<%# Eval("Vessel_ID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                            <HeaderStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job Description " HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJobDescription" runat="server" Text='<%# Eval("Job_Long_Description") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60%" />
                                            <HeaderStyle Width="60%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("WL_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date Raised" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDateRaised" runat="server" Text='<%# Eval("DATE_RAISED") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                            <HeaderStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Job_Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImglnkJob" runat="server" Text="Jobs" ForeColor="Black" ToolTip="Unlink Jobs"
                                                    ImageUrl="~/Images/VET_Unlink.png" Height="16px" OnClientClick="return confirm('Are you sure you want to unlink this job from the observation');"
                                                    CommandArgument='<%# Eval("Observation_Job_ID") %>'></asp:ImageButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%" colspan="2">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70px" OnClick="btnCancel_Click" />
                        </td>
                        <td style="width: 50%;padding-right:1px;" align="right" colspan="3">
                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="70px" OnClick="btnSave_Click"
                                OnClientClick="return ValidateObs(); " />
                            &nbsp;
                            <asp:Button ID="BtnSaveClose" runat="server" Text="Save and Close" Width="100px"
                                OnClick="btnSaveClose_Click" OnClientClick="return ValidateObs(); " />
                            <asp:HiddenField ID="hdnQryStrObservationId" runat="server" />
                            <asp:HiddenField ID="hdnObservationId" runat="server" />
                        </td>
                       
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvPopupAddResponse" style="display: none; width: 500px;" title="Add Response">
        <iframe id="IframeAddResponse" src="" frameborder="0" style="height: 300px; width: 500px;">
        </iframe>
    </div>
    <div id="dvPopupAddResponseAttachment" style="display: none; width: 500px;" title="Response Attachment">
        <iframe id="IframeAddResponseAttachment" src="" frameborder="0" style="height: 300px;
            width: 500px;"></iframe>
    </div>
    <div id="dvPopupAssignJob" style="display: none; width: 1200px;" title="Assign Job">
        <iframe id="IframeAssignJob" src="" frameborder="0" style="height: 500px; width: 1200px;">
        </iframe>
    </div>
    </form>
</body>
</html>
