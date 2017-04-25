<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCrewMedicalHistory.aspx.cs"
    Inherits="Crew_CrewMedicalHistory" %>
 <%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="tlk4" %>
<%@ Register Src="../UserControl/ucUploadOpsWorklistAttachment.ascx" TagName="ctlUploadAttachment"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/MedHistory_DataHandler.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            color: #333;
            font-family: Tahoma;
        }
    </style>
    <script type="text/javascript">
        function uploadComplete() { }
        function validation() {

            if (document.getElementById("ctl00_MainContent_txtExp_Date").value == "") {
                alert("Enter Date");
                document.getElementById("ctl00_MainContent_txtExp_Date").focus();
                return false;
            }

//            hideModal('dvPopupCostItem');
            return true;


        }

        function uploadComplete(sender, args) {

        var errText = args.get_postedUrl();
        if (!errText) return; // only process if populated

        var errinfo = Sys.Serialization.JavaScriptSerializer.deserialize(errText);

        if (errinfo && errinfo.id && errinfo.message) {
            var idEl = document.getElementById('errnbr');
            var msgEl = document.getElementById('errmsg');
            if (idEl && msgEl) {
                idEl.innerHTML = errinfo.id;
                msgEl.innerHTML = errinfo.message;
            }
        }
    }
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode != 46 && (charCode < 48 || charCode > 57)))
            return false;
        return true;
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="HiddenField_SelectedFiles" runat="server"/>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnCaseID" runat="server" />
            <asp:HiddenField ID="hdnVesselId" runat="server" />
            <asp:HiddenField ID="hdnOfficeId" runat="server" />

            <div class="error-message" onclick="javascript:this.style.display='none';">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
            <asp:Panel ID="pnlAddMedHistory" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:MultiView ID="MultiView_MedHistory" runat="server">
                                <asp:View ID="Edit" runat="server">
                                    <div class="crewquery-section-header">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    Crew Medical History
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:LinkButton ID="lnkSaveDetails" Text="Save" runat="server" ForeColor="Blue" Font-Bold="false"
                                                        Font-Underline="false" OnClick="lnkSaveDetails_Click"> </asp:LinkButton>
                                                    &nbsp;|&nbsp;
                                                    <asp:LinkButton ID="lnkCancelEditDetails" Text="Cancel" runat="server" ForeColor="Blue"
                                                        Font-Bold="false" Font-Underline="false" OnClick="lnkCancelEditDetails_Click"> </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <table class="crewquery-table" style="border-collapse: separate">
                                        <tr>
                                            <td style="width: 120px">
                                                Date
                                            </td>
                                            <td style="width: 200px" class="crewquery-data-field">
                                                <asp:TextBox ID="txtDate_Of_Creation" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                <tlk4:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDate_Of_Creation">
                                                </tlk4:CalendarExtender>
                                            </td>
                                            <td style="width: 50px">
                                            </td>
                                            <td style="width: 100px">
                                                Status
                                            </td>
                                            <td style="width: 200px" class="crewquery-data-field">
                                                <asp:DropDownList ID="ddlStatus" runat="server" Width="200px">
                                                    <asp:ListItem Text="Open" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Closed" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 120px">
                                                To Date
                                            </td>
                                            <td style="width: 200px" class="crewquery-data-field">
                                                <asp:TextBox ID="txtToDate" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                <tlk4:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate">
                                                </tlk4:CalendarExtender>
                                            </td>

                                           
                                            <td style="width: 50px">
                                            </td>
                                            <td>
                                                Type
                                            </td>
                                            <td style="width: 200px" class="crewquery-data-field">
                                                <asp:DropDownList ID="ddlType" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                             <td>
                                                Staff Code
                                            </td>
                                            <td style="width: 200px" class="crewquery-data-field">
                                                <asp:HyperLink ID="lnkStaff_Code" runat="server" Target="_blank"></asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Staff Name
                                            </td>
                                            <td colspan="4" class="crewquery-data-field">
                                                <asp:Label ID="lblStaff_FullName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Voyage
                                            </td>
                                            <td colspan="4" class="crewquery-data-field">
                                                <asp:DropDownList ID="ddlVoyages" runat="server" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; min-height: 50px; vertical-align: top">
                                                Details
                                            </td>
                                            <td colspan="4" class="crewquery-data-field" style="min-height: 50px; vertical-align: top">
                                                <asp:TextBox TextMode="MultiLine" ID="txtDetails" runat="server" Width="99%" Height="80px" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="View" runat="server">
                                    <div class="crewquery-section-header">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    Crew Medical History
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:LinkButton ID="lnkEditDetails" Text="Edit" runat="server" ForeColor="Blue" Font-Bold="false"
                                                        Font-Underline="false" OnClick="lnkEditDetails_Click"> </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <table class="crewquery-table" style="border-collapse: separate">
                                        <tr>
                                            <td style="width: 120px">
                                                Date
                                            </td>
                                            <td style="width: 200px" class="crewquery-data-field">
                                                <asp:Label ID="lblDate_Of_Creation" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 50px">
                                            </td>
                                            <td style="width: 100px">
                                                Status
                                            </td>
                                            <td style="width: 200px" class="crewquery-data-field">
                                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 120px">
                                                To Date
                                            </td>
                                            <td style="width: 200px" class="crewquery-data-field">
                                                 <asp:Label ID="lblToDate" runat="server"></asp:Label>
                                            </td>

                                           
                                            <td style="width: 50px">
                                            </td>
                                            <td>
                                                Type
                                            </td>
                                            <td style="width: 200px" class="crewquery-data-field">
                                                <asp:Label ID="lblType" runat="server"></asp:Label>
                                            </td>
                                        </tr>


                                       <tr>
                                            <td>
                                                Staff Code
                                            </td>
                                            <td style="width: 200px" class="crewquery-data-field">
                                                <asp:HyperLink ID="lnkStaff_Code_View" runat="server" Target="_blank"></asp:HyperLink>
                                            </td>
                                            <td style="width: 50px">
                                            </td>
                                           <%-- <td>
                                                Type
                                            </td>
                                            <td style="width: 200px" class="crewquery-data-field">
                                                <asp:Label ID="lblType" runat="server"></asp:Label>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td>
                                                Staff Name
                                            </td>
                                            <td colspan="4" class="crewquery-data-field">
                                                <asp:Label ID="lblStaff_FullName_View" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Voyage
                                            </td>
                                            <td colspan="4" class="crewquery-data-field">
                                                <asp:Label ID="lblVoyageName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; min-height: 50px; vertical-align: top">
                                                Details
                                            </td>
                                            <td colspan="4" class="crewquery-data-field" style="min-height: 50px; vertical-align: top">
                                                <asp:Label ID="lblDetails" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:View>
                            </asp:MultiView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlCostItems" runat="server">
                                <div class="crewquery-section-header">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                Medical Cost Items
                                            </td>
                                            <td style="text-align: right">
                                                <asp:LinkButton ID="lnkAddCostItem" Text="Add Cost Item " runat="server" ForeColor="Blue"
                                                    Font-Bold="false" Font-Underline="false" OnClick="lnkAddCostItem_Click"> </asp:LinkButton>

                                                    <%--<asp:LinkButton ID="LinkButton1" Text="Add Cost Item " runat="server" ForeColor="Blue"
                                                    Font-Bold="false" Font-Underline="false" OnClientClick="showModal('dvPopupCostItem');return false;"> </asp:LinkButton>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <asp:Repeater ID="rptCostItems" runat="server" OnItemDataBound="rptCostItems_ItemDataBound"
                                    OnItemCommand="rptCostItems_ItemCommand">
                                    <HeaderTemplate>
                                        <table class="crewquery-table">
                                            <tr class="crewquery-table-header">
                                                <td>
                                                    Date
                                                </td>
                                                <td>
                                                    Item Description
                                                </td>
                                                <td>
                                                    Type of Expense
                                                </td>
                                                <td>
                                                    Local Currency
                                                </td>
                                                <td>
                                                    Local Amount
                                                </td>
                                                <td>
                                                    In USD
                                                </td>
                                                <td style="text-align: center">
                                                    Attachments
                                                </td>
                                                <td>
                                                    Delete
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="crewquery-table-row">
                                            <td>
                                                <asp:Label ID="lblDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("On_Date"))) %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExp_Type" runat="server" Text='<%#Eval("COST_TYPE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExp_Curr" runat="server" Text='<%#Eval("Currency_Code")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExp_Curr_Amt" runat="server" Text='<%#Eval("Local_Amount")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUSD_AMT" runat="server" Text='<%#Eval("USD_Amount")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/attach-icon.png"
                                                    AlternateText='<%#Eval("Attach_Count")%>' Visible='<%#Eval("Attach_Count").ToString() =="0"?false:true%>'
                                                    OnClientClick='<%# "showCostItemAttachments(event," + Eval("ID").ToString() + "," + Eval("Case_ID").ToString()+ "," + Eval("Vessel_ID").ToString()+ "," + Eval("Office_ID").ToString() +"); return false;"%>'>
                                                </asp:ImageButton>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                                                    CausesValidation="False" CommandArgument='<%#Eval("ID")%>' CommandName="Delete"
                                                    OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                                                    AlternateText="Delete"></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="crewquery-table-alt-row">
                                            <td>
                                                <asp:Label ID="lblDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("On_Date"))) %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExp_Type" runat="server" Text='<%#Eval("COST_TYPE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExp_Curr" runat="server" Text='<%#Eval("Currency_Code")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExp_Curr_Amt" runat="server" Text='<%#Eval("Local_Amount")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUSD_AMT" runat="server" Text='<%#Eval("USD_Amount")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/attach-icon.png"
                                                    AlternateText='<%#Eval("Attach_Count")%>' Visible='<%#Eval("Attach_Count").ToString() =="0"?false:true%>'
                                                    OnClientClick='<%# "showCostItemAttachments(event," + Eval("ID").ToString() + "," + Eval("Case_ID").ToString()+ "," + Eval("Vessel_ID").ToString()+ "," + Eval("Office_ID").ToString() + "); return false;"%>'>
                                                </asp:ImageButton>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                                                    CausesValidation="False" CommandArgument='<%#Eval("ID")%>' CommandName="Delete"
                                                    OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                                                    AlternateText="Delete"></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        <tr class="crewquery-table-footer-row">
                                            <td colspan="4">
                                                Total
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTotal" runat="server" Text='0.00'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTotalUSD" runat="server" Text='0.00'></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlFollowups" runat="server">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 60%; vertical-align: top;">
                                            <div class="crewquery-section-header">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            Followups
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:LinkButton ID="lnkAddFollowUp" Text="Add Followup " runat="server" ForeColor="Blue"
                                                                Font-Bold="false" Font-Underline="false" OnClientClick="showModal('dvPopupFollowup');return false;"> </asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:Repeater ID="rptFollowUps" runat="server">
                                                <HeaderTemplate>
                                                    <table class="crewquery-table">
                                                        <tr class="crewquery-table-header">
                                                            <td style="width: 100px">
                                                                Date
                                                            </td>
                                                            <td style="width: 150px">
                                                                From
                                                            </td>
                                                            <td>
                                                                Message
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="crewquery-table-row">
                                                        <td>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation"))) %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblFrom" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMsg" runat="server" Text='<%#Eval("Followup_Text")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="crewquery-table-alt-row">
                                                        <td>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation"))) %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblFrom" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMsg" runat="server" Text='<%#Eval("Followup_Text")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td style="width: 40%; vertical-align: top;">
                                            <div class="crewquery-section-header">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            Attachments
                                                        </td>
                                                        <td style="text-align: right">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:Repeater ID="rptAttachments" runat="server" OnItemCommand="rptAttachments_ItemCommand">
                                                <HeaderTemplate>
                                                    <table class="crewquery-table">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="crewquery-table-row">
                                                        <td>
                                                            <asp:Label ID="lblDt" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation"))) %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="lnkAttach" runat="server" Text='<%#Eval("attachment_name")%>'
                                                                NavigateUrl='<%# "../Uploads/MedHistory/" + Eval("attachment_path")%>' Target="_blank"></asp:HyperLink>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSize" runat="server" Text='<%#Eval("Attachment_Size")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                                                                CausesValidation="False" CommandArgument='<%#Eval("ID")%>' CommandName="Delete"
                                                                OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                                                                AlternateText="Delete"></asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="crewquery-table-alt-row">
                                                        <td>
                                                            <asp:Label ID="lblDt" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation"))) %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="lnkAttach" runat="server" Text='<%#Eval("attachment_name")%>'
                                                                NavigateUrl='<%# "../Uploads/MedHistory/" + Eval("attachment_path")%>' Target="_blank"></asp:HyperLink>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSize" runat="server" Text='<%#Eval("Attachment_Size")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                                                                CausesValidation="False" CommandArgument='<%#Eval("ID")%>' CommandName="Delete"
                                                                OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                                                                AlternateText="Delete"></asp:ImageButton>
                                                        </td>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <asp:Panel ID="pnlFileUpload" runat="server">
                                                <asp:FileUpload ID="UploadAttachments" runat="server" Height="24px" />
                                                <asp:Button ID="btnUploadAttachments" runat="server" Text="Upload" Font-Names="Tahoma"
                                                    OnClick="btnUploadAttachments_Click" />
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnSave" runat="server" Text="Save and Proceed" BorderStyle="Solid"
                                BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5"
                                Width="160px" OnClick="btnSave_Click" ClientIDMode="Static" />
                            <asp:Button ID="btnClose" runat="server" Text="Close" BorderStyle="Solid" BorderColor="#0489B1"
                                BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" Width="160px"
                                OnClientClick="parent.hideModal('dvPopupFrame');return false;"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div id="dvPopupFollowup" class="draggable" style="display: none; background-color: #CBE1EF;
                border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
                left: 0.5%; top: 15%; width: 400px; z-index: 1; color: black" title='Add FollowUp'>
                <div class="content">
                    <table>
                        <tr>
                            <td>
                                Followup
                            </td>
                            <td>
                                <asp:TextBox ID="txtFollowUp" runat="server" TextMode="MultiLine" Width="300px" Height="80px">                                
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right">
                                <asp:Button ID="btnSaveFollowUp" Text="Save" runat="server" BorderStyle="Solid" BorderColor="#0489B1"
                                    BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" Width="160px"
                                    OnClick="btnSaveFollowUp_Click" OnClientClick="hideModal('dvPopup');" />
                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" BorderStyle="Solid" BorderColor="#0489B1"
                                    BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" Width="60px"
                                    OnClientClick="hideModal('dvPopup');" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUploadAttachments" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel_CostItem" runat="server"  UpdateMode="Conditional">
    <ContentTemplate>
    <div id="dvPopupCostItem" class="draggable" style="display: none; background-color: #CBE1EF;
                border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
                left: 0.5%; top: 15%; width: 600px; z-index: 1; color: black" title='Add Cost Item'>
                <div class="content" style="padding: 10px">
                    <table style="width: 100%">
                        <tr>
                            <td colspan="2" style="text-align: center" class="crewquery-section-header">
                                New Cost Item
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 160px">
                                Date<span style="color:red"> *</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExp_Date" runat="server"></asp:TextBox>
                                <tlk4:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtExp_Date">
                                </tlk4:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Item Description<span style="color:red" >*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="300px" Height="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr> 
                            <td>
                                Type of Expense<span style="color:red"> *</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlExpType" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Local Currency<span style="color:red"> *</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLocalCurr" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Local Amount<span style="color:red"> *</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLocalAmt" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                In USD
                            </td>
                            <td>
                                <asp:TextBox ID="txtUSDAmt" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align:top">
                                Attachments <br /><font size=1><b>(Click UPLOAD button before you Save the cost item)</b></font>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                                        <tlk4:AjaxFileUpload ID="CostItemUploader" runat="server" OnClientUploadComplete="uploadComplete" Font-Size="11px" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right">
                                <asp:Button ID="btnSaveCostItem" Text="Save" runat="server" BorderStyle="Solid" BorderColor="#0489B1"
                                    BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" Width="160px"
                                    OnClick="btnSaveCostItem_Click" OnClientClick="return validation();"/>
                                <asp:Button ID="btnCancelCostItem" Text="Cancel" runat="server" BorderStyle="Solid"
                                    BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5"
                                    Width="60px" OnClientClick="hideModal('dvPopupCostItem');" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
<script type="text/javascript">
    var strDateFormat = '<%=DFormat %>';

    $(document).ready(function () {
        $("body").on("click", "#btnSave", function () {

            if ($("#txtDate_Of_Creation").length > 0) {
                var date1 = document.getElementById("txtDate_Of_Creation").value;
                if ($.trim($("#txtDate_Of_Creation").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid Date<%=TodayDateFormat %>");
                        $("#txtDate_Of_Creation").focus();
                        return false;
                    }
                }

            }
            if ($("#txtToDate").length > 0) {
                var date1 = document.getElementById("txtToDate").value;
                if ($.trim($("#txtToDate").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid To Date<%=TodayDateFormat %>");
                        return false;
                    }
                }
            }
        });
    });
    
</script>
</html>
