<%@ Page Title="Crew Query Details" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CrewQuery_Details.aspx.cs" Inherits="CrewQuery_CrewQuery_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/CrewQuery_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
<%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
    <style type="text/css">
        body
        {
            color: #333;
        }

    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:FormView ID="frmDetails" runat="server" Width="100%">
                <ItemTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td>
                               <div class="page-title">
                                   Crew Query
                             </div>
                               
                                <table class="crewquery-table" style="border-collapse: separate">
                                    <tr>
                                        <td style="width: 120px">
                                            Date
                                        </td>
                                        <td style="width: 200px" class="crewquery-data-field">
                                            <asp:Label ID="txtDate_Of_Creation" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation")))%>'></asp:Label>
                                            
                                        </td>
                                        <td style="width: 50px">
                                        </td>
                                        <td style="width: 100px">
                                            Status
                                        </td>
                                        <td style="width: 200px" class="crewquery-data-field">
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Staff Code
                                        </td>
                                        <td style="width: 200px" class="crewquery-data-field">
                                            <asp:HyperLink ID="lnlStaff_Code" runat="server" Text='<%#Eval("Staff_Code")%>' NavigateUrl='<%# "../Crew/CrewDetails.aspx?ID=" + Eval("CrewID")%>' CssClass="staffInfo"
                                                Target="_blank"></asp:HyperLink>
                                        </td>
                                        <td style="width: 50px">
                                        </td>
                                        <td>
                                            Query Type
                                        </td>
                                        <td style="width: 200px" class="crewquery-data-field">
                                            <asp:Label ID="lblQType" runat="server" Text='<%#Eval("QUERY_TYPE")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Staff Name
                                        </td>
                                        <td colspan="4" class="crewquery-data-field">
                                            <asp:Label ID="lblStaff_FullName" runat="server" Text='<%#Eval("Staff_FullName")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Selected Course
                                        </td>
                                        <td colspan="4" class="crewquery-data-field">
                                            <asp:Label ID="lblCourse" runat="server" Text='<%#Eval("COURSE_NAME")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold" style="min-height: 50px; vertical-align: top">
                                            Details
                                        </td>
                                        <td colspan="4" class="crewquery-data-field" style="min-height: 50px; vertical-align: top">
                                            <asp:Label ID="lblDetails" runat="server" Text='<%#Eval("Query_Detail")%>' />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlClaims" runat="server" Visible='<%#Eval("QUERY_TYPE_ID").ToString() =="2"?true:false%>'>
                                    <div class="crewquery-section-header">
                                        Claim Details</div>
                                    <asp:Repeater ID="rptClaims" runat="server" OnItemDataBound="rptClaims_ItemDataBound"
                                        OnItemCommand="rptClaims_ItemCommand">
                                        <HeaderTemplate>
                                            <table class="crewquery-table">
                                                <tr class="crewquery-table-header">
                                                    <td>
                                                        Vessel
                                                    </td>
                                                    <td>
                                                        Purpose
                                                    </td>
                                                    <td>
                                                        Date
                                                    </td>
                                                    <td>
                                                        Description
                                                    </td>
                                                    <td>
                                                        Trans From
                                                    </td>
                                                    <td>
                                                        Trans To
                                                    </td>
                                                    <td>
                                                        Supporting
                                                    </td>
                                                    <td>
                                                        Rqstd USD
                                                    </td>
                                                    <td>
                                                        Approved USD
                                                    </td>
                                                    <td style="text-align: center">
                                                        Attachments
                                                    </td>
                                                    <td style="text-align: center">
                                                        Approval Status
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="crewquery-table-row">
                                                <td>
                                                    <asp:Label ID="lblVessel" runat="server" Text='<%#Eval("Vessel_Short_Name")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPurpose" runat="server" Text='<%#Eval("Purpose")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("On_Date")))%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Trans_From")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("Trans_To")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSupporting" runat="server" Text='<%#Eval("Supporting_Verified")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRequestedUSD" runat="server" Text='<%#Eval("Requested_US_Amount")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtApprovedUSD" runat="server" Enabled='<%#Eval("Claim_Status").ToString() =="0"?true:false%>'
                                                        Text='<%#Eval("Claim_Status").ToString() =="0"? Eval("Requested_US_Amount").ToString(): Eval("Approved_US_Amount").ToString()%>'
                                                        CssClass="crewquery-data-field" Width="60px" BorderColor="#888888">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/attach-icon.png"
                                                        AlternateText='<%#Eval("Attach_Count")%>' Visible='<%#Eval("Attach_Count").ToString() =="0"?false:true%>' OnClientClick='<%# "showClaimAttachments(event," + Eval("Vessel_ID").ToString() + "," + Eval("Query_ID").ToString() + "," + Eval("ID").ToString() + "); return false;"%>' >
                                                    </asp:ImageButton>

                                                    <%--<asp:ImageButton ID="imgAttach" runat="server" ImageUrl="~/Images/attach-icon.png"
                                                        AlternateText='<%#Eval("Attach_Count")%>' CommandName="imgClaimAttachments_Click"
                                                        CommandArgument='<%#Eval("ID")%>' Visible='<%#Eval("Attach_Count").ToString() =="0"?false:true%>'>
                                                    </asp:ImageButton>--%>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblApproved" runat="server" Text='<%#Eval("ApprovalStatus")%>' Visible='<%#Eval("Claim_Status").ToString() =="0"?false:true%>'></asp:Label>
                                                    <asp:CheckBox ID="chkApproval" runat="server" Visible='<%#Eval("Claim_Status").ToString() =="0"?true:false%>' />
                                                    <asp:HiddenField ID="hdnClaimID" runat="server" Value='<%#Eval("ID")%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="crewquery-table-alt-row">
                                                <td>
                                                    <asp:Label ID="lblVessel" runat="server" Text='<%#Eval("Vessel_Short_Name")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPurpose" runat="server" Text='<%#Eval("Purpose")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("On_Date")))%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Trans_From")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("Trans_To")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSupporting" runat="server" Text='<%#Eval("Supporting_Verified")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRequestedUSD" runat="server" Text='<%#Eval("Requested_US_Amount")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtApprovedUSD" runat="server" Enabled='<%#Eval("Claim_Status").ToString() =="0"?true:false%>'
                                                        Text='<%#Eval("Claim_Status").ToString() =="0"? Eval("Requested_US_Amount").ToString(): Eval("Approved_US_Amount").ToString()%>'
                                                        CssClass="crewquery-data-field" Width="60px" BorderColor="#888888">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/attach-icon.png"
                                                        AlternateText='<%#Eval("Attach_Count")%>' Visible='<%#Eval("Attach_Count").ToString() =="0"?false:true%>' OnClientClick='<%# "showClaimAttachments(event," + Eval("Vessel_ID").ToString() + "," + Eval("Query_ID").ToString() + "," + Eval("ID").ToString() + "); return false;"%>' >
                                                    </asp:ImageButton>

                                                    <%--<asp:ImageButton ID="imgAttach" runat="server" ImageUrl="~/Images/attach-icon.png"
                                                        AlternateText='<%#Eval("Attach_Count")%>' CommandName="imgClaimAttachments_Click"
                                                        CommandArgument='<%#Eval("ID")%>' Visible='<%#Eval("Attach_Count").ToString() =="0"?false:true%>'>
                                                    </asp:ImageButton>--%>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblApproved" runat="server" Text='<%#Eval("ApprovalStatus")%>' Visible='<%#Eval("Claim_Status").ToString() =="0"?false:true%>'></asp:Label>
                                                    <asp:CheckBox ID="chkApproval" runat="server" Visible='<%#Eval("Claim_Status").ToString() =="0"?true:false%>' />
                                                    <asp:HiddenField ID="hdnClaimID" runat="server" Value='<%#Eval("ID")%>' />
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                            <tr class="crewquery-table-footer-row">
                                                <td colspan="7">
                                                    Total
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTotalRequestedUSD" runat="server" Text='0.00'></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="lblTotalApprovedUSD" runat="server" Text='0.00'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Button ID="btnApproveClaims" runat="server" Text="Approve" OnClick="btnApproveClaims_Click"
                                                        OnClientClick="return confirm('Are you sure, you want to APPROVE the claim(s)?')" />
                                                    <asp:Button ID="btnRejectClaims" runat="server" Text="Reject" OnClick="btnRejectClaims_Click"
                                                        OnClientClick="return confirm('Are you sure, you want to REJECT the claim(s)?')" />
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
                                                                Font-Bold="false" Font-Underline="false" OnClientClick="showModal('dvPopup');return false;"> </asp:LinkButton>
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
                                                            <asp:Label ID="lblDate" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation")))%>'></asp:Label>
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
                                                            <asp:Label ID="lblDate" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation")))%>'></asp:Label>
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
                                                Attachments</div>
                                            <asp:Repeater ID="rptAttachments" runat="server">
                                                <HeaderTemplate>
                                                    <table class="crewquery-table">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="crewquery-table-row">
                                                        <td>
                                                            <asp:HyperLink ID="lnkAttach" runat="server" Text='<%#Eval("attachment_name")%>'
                                                                NavigateUrl='<%# "../Uploads/CrewQuery/" + Eval("attachment_path")%>' Target="_blank"></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="crewquery-table-alt-row">
                                                        <td>
                                                            <asp:HyperLink ID="lnkAttach" runat="server" Text='<%#Eval("attachment_name")%>'
                                                                NavigateUrl='<%# "../Uploads/CrewQuery/" + Eval("attachment_path")%>' Target="_blank"></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
            <div id="dvPopup" class="draggable" style="display: none; background-color: #CBE1EF;
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
                                <asp:Button ID="btnSaveFollowUp" Text="Save" runat="server" OnClick="btnSaveFollowUp_Click"
                                    OnClientClick="hideModal('dvPopup');" />
                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClientClick="hideModal('dvPopup');" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
