<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_MaintenanceFeedback.aspx.cs"
    Inherits="Crew_CrewDetails_MaintenanceFeedback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
       <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
</head>
<body style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;
    height: 100%;">
    <form id="form1" runat="server">
     <center>
    <div id="dvCrewFeedbackGrid" style="font-family: Tahoma; font-size: 12px;">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlViewFeedbacks" runat="server" Visible="false">
            <asp:GridView ID="GridView_CrewRemarks" runat="server" AllowSorting="False" AutoGenerateColumns="false"
                GridLines="None" CellPadding="3" CellSpacing="1" Width="100%" CssClass="GridView-css" EmptyDataText="No Record Found!!">
                <Columns>
                    
                    <asp:TemplateField HeaderText="Vessel">
                        <ItemTemplate>
                            <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Short_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type(PMS/WL)">
                        <ItemTemplate>
                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("JOB_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Job Code">
                        <ItemTemplate>
                           <asp:HyperLink ID="lnkJobCode" runat="server" Text='<%# Eval("JOB_Code") %>'
                                Target="_blank" NavigateUrl='<%#Eval("Job_Type").ToString()=="PMS JOB"?"../Technical/PMS/PMSJobIndividualDetails.aspx?JobID="+Eval("PMS_JobID").ToString()+"&JobHistoryID="+Eval("PMS_JobHistoryID").ToString()+"&VID="+Eval("Vessel_ID").ToString()+"&Qflag=H":"../Technical/WorkList/ViewJob.aspx?WLID="+Eval("Worklist_ID").ToString()+"&VID="+Eval("Vessel_ID").ToString()+"&OFFID="+Eval("WL_Office_ID").ToString()%>' />
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Created By">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("User_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Created">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_of_creation"))) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Feedback">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Remark").ToString().Replace("\n","<br>") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Att.">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkAttachment" runat="server" ImageUrl="~/Images/Attach.png" NavigateUrl='<%# "~/Uploads/CrewDocuments/" + Eval("AttachmentPath").ToString()%>'
                                Target="_blank" Visible='<%#Eval("AttachmentPath").ToString()==""?false:true%>' />
                        </ItemTemplate>
                        <ItemStyle Width="10px" HorizontalAlign="Center" />
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
        </asp:Panel>
        <asp:Panel ID="pnlAddFeedback" runat="server" Visible="false">
            <div id="dvPopupAddFeedback">
                <table width="100%" cellpadding="1" cellspacing="1">
                    <tr>
                        <td>
                            Feedback/Note
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCrewRemarks" runat="server" TextMode="MultiLine" Height="200px"
                                Width="450px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:FileUpload ID="CrewRemarks_FileUploader" runat="server" Width="400px" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnSaveRemarks" runat="server" Text="Save" OnClick="btnSaveRemarks_Click">
                            </asp:Button>
                            <asp:Button ID="btnSaveAndCloseRemarks" runat="server" Text="Save & Close" OnClick="btnSaveAndCloseRemarks_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div></center>
    </form>
</body>
</html>
