<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportJobProgress.aspx.cs"
    Inherits="ReportJobProgress" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Jobs Progress</title>
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../../Scripts/ModalPopUp.js"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <style type="text/css">
        @media print
        {
            body
            {
                color: black;
                font-family: Tahoma;
                font-size: 14px;
            }
            .header
            {
                display: none;
            }
            .printable
            {
                display: block;
                border: 0;
            }
            .printable table
            {
                display: block;
                border: 0;
            }
            .non-printable
            {
                display: none;
            }
            #pageTitle
            {
                border: 0;
            }
        }
    </style>
    <style type="text/css">
        body
        {
            color: black;
            font-family: Tahoma;
        }
        select
        {
            font-size: 10px;
            height: 21px;
        }
        a
        {
            text-decoration: none;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $(".draggable").draggable();

        });

        function OpenFollowupDiv() {
            showModal('dvAddFollowUp');
            return false;
        }
        function CloseFollowupDiv() {
            hideModal('dvAddFollowUp');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="background-color: #efefef; border: 1px solid #ffffff; font-family: Tahoma;">
                <div style="background-color: #ffffff; margin: 5px; border: 1px solid #ffffff;">
                    <div style="background-color: #ffffff; margin: 5px;">
                        <div style="background-color: #efefef; text-align: right; font-size: 11px; padding: 2px;">
                            <asp:Label ID="lblDt" runat="server"></asp:Label></div>
                        <div style="background-color: #cccccc; padding: 2px; font-size: 18px;">
                            Jobs Progress - Report</div>
                        <div style="margin-top: 2px; font-size: 12px;">
                            <asp:Repeater ID="rpt1" runat="server" OnItemDataBound="rpt1_ItemDataBound" OnItemCommand="rpt1_ItemCommand">
                                <HeaderTemplate>
                                    <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                                        border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                        <tr style="background-color: #efefef">
                                            <td>
                                                <b>Vessel</b>
                                            </td>
                                            <td>
                                                <b>Code</b>
                                            </td>
                                            <td>
                                                <b>Assignor</b>
                                            </td>
                                            <td>
                                                <b>Assign Date</b>
                                            </td>
                                            <td>
                                                <b>Job Description</b>
                                            </td>
                                            <td>
                                                <b>Expected Completion</b>
                                            </td>
                                            <td>
                                                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                    <tr style="background-color: #efefef">
                                                        <td colspan="3">
                                                            <b>Job Progress</b>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #aabbdd">
                                                        <td style="width: 80px; text-align: left;">
                                                            <b>Date</b>
                                                        </td>
                                                        <td style="width: 80px; text-align: left;">
                                                            <b>Followup By</b>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <b>Message</b>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: #ffffff; vertical-align: top;">
                                        <td style="width: 50px;">
                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Vessel_Short_Name")%>'></asp:Label>
                                        </td>
                                        <td style="width: 50px;">
                                            <asp:Label ID="lblJobID" runat="server" Text='<%#Eval( "worklist_id")%>'></asp:Label>
                                        </td>
                                        <td style="width: 80px;">
                                            <asp:Label ID="lblAssignor" runat="server" Text='<%# Eval( "AssignorName").ToString()%>'></asp:Label>
                                        </td>
                                        <td style="width: 80px;">
                                            <asp:Label ID="lblAssignDt" runat="server" Text='<%#Eval("DATE_RAISED","{0:d/MM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_RAISED","{0:d/MM/yy}")   %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblJobDescription" runat="server" Text='<%# Eval( "job_description").ToString().Replace("\n","<br>")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblExptCompl" runat="server" Text='<%# Eval("DATE_ESTMTD_CMPLTN","{0:d/MM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_ESTMTD_CMPLTN","{0:d/MM/yy}") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: right; vertical-align: top;">
                                            <asp:Repeater ID="rpt2" runat="server" OnItemDataBound="rpt2_ItemDataBound">
                                                <HeaderTemplate>
                                                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr style="vertical-align: top; background-color: #efefef;">
                                                        <td style="width: 80px; text-align: left;">
                                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("DATE_CREATED","{0:d/MM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_CREATED","{0:d/MM/yy}")   %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 80px; text-align: left;">
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("USER_NAME") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("FOLLOWUP") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr style="vertical-align: top; background-color: #ffffff;">
                                                        <td style="width: 80px; text-align: left;">
                                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("DATE_CREATED","{0:d/MM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_CREATED","{0:d/MM/yy}")   %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 80px; text-align: left;">
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("USER_NAME") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("FOLLOWUP") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <div style="border: 1px solid #cccccc; background-color: #00FFFF; text-align: center;
                                                margin-top: 1px;" class="non-printable">
                                                <asp:LinkButton ID="lnkAddFollowUp" runat="server" Text="Add FollowUp" CommandName="Add_FollowUp"
                                                    CssClass="linkbutton" CommandArgument='<%# Eval("VESSEL_ID").ToString() + "," + Eval("WORKLIST_ID").ToString() + "," + Eval("OFFICE_ID").ToString()%>'>
                                                </asp:LinkButton>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlAddFollowUp" runat="server">
        <div id="dvAddFollowUp" class="draggable" style="display: none; background-color: #E0E0E0;
            border: 2px solid gray; width: 500px; position: absolute; left: 33%; top: 10%;
            z-index: 2; color: black">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnOfficeID" runat="server" />
                    <asp:HiddenField ID="hdnWorklistlID" runat="server" />
                    <asp:HiddenField ID="hdnVesselID" runat="server" />
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="3" style="text-align: center; font-weight: bold; border-style: solid;
                                border-color: Silver; background-color: Gray; padding: 2px;">
                                New Followup
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                            </td>
                            <td>
                                <asp:TextBox ID="txtFollowupDate" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                <asp:Button ID="btnSaveFollowUpAndClose" Text="Save And Close" runat="server" OnClientClick="JavaScript:CloseFollowupDiv();"
                                    OnClick="btnSaveFollowUpAndClose_OnClick" />
                                <input type="button" id="Button1" value="Close" onclick="CloseFollowupDiv()" />
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" style="background-color: #aabbee; padding: 2px; text-align: center;">
                                &nbsp;&nbsp;Message:&nbsp;&nbsp;
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" colspan="3" style="border: 1px solid inset; background-color: #aabbee;
                                padding-top: 5px; padding-bottom: 5px; text-align: center;">
                                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Height="200px" Width="480px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
<%
    if (Request.QueryString["Export"] != null)
    {
%>
<style type="text/css">
    .non-printable
            {
                display: none;
            }
</style>
<%
    }
     %>