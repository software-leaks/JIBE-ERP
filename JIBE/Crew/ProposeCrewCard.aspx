<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProposeCrewCard.aspx.cs"
    Inherits="Crew_ProposeCrewCard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
    </style>
    <script type="text/javascript">
        function changeAttStatus(cardid, atttype, status, userid) {
            var iStatus = 0;

            if (status == true)
                iStatus = 1;

            Async_setCrewCard_AttachmentStatus(cardid, atttype, iStatus, userid);
        }

        function Async_setCrewCard_AttachmentStatus(cardid, atttype, status, userid) {
            var url = "../webservice.asmx/setCrewCard_AttachmentStatus";
            var params = 'cardid=' + cardid + '&atttype=' + atttype + '&status=' + status + '&userid=' + userid;

            var obj = new AsyncResponse(url, params, response_setCrewCard_AttachmentStatus);
            obj.getResponse();
        }
        function response_setCrewCard_AttachmentStatus(retval) {
            if (retval.indexOf('Working') >= 0) { return; }
            try {
                retval = clearXMLTags(retval);
                if (retval.indexOf('ERROR:', 0) >= 0) {
                    alert(retval);
                    return;
                }

                if (retval == 1) {
                    alert('Attachment (N/A) status updated.');
                }
                else
                    alert(retval);
            }
            catch (ex) { alert(ex.message); }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function showDiv(dv, cardid) {
            document.getElementById(dv).style.display = "block";
            $('[id$=hdnCardID]').val(cardid);
            //alert($('[id$=hdnCardID]').val());
        }
        function closeDiv(dv) {
            document.getElementById(dv).style.display = "None";
        }
            
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scr" runat="server">
    </asp:ScriptManager>
    <%--    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>--%>
    <asp:HiddenField ID="hdnCardID" runat="server" />
    <div class="error-message" onclick="javascript:this.style.display='none';">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </div>
    <div>
        <asp:Panel ID="pnlRemarks" runat="server" Visible="false">
            <div style="background-color: #E6F8E0; border: 1px solid gray; text-align: left;
                margin-top: 10px; padding: 2px; font-weight: bold;">
                <center>
                    Add Remarks</center>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtAddRemarks" runat="server" TextMode="MultiLine" Height="100px"
                                Width="99%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnSaveRemarks" runat="server" Text="Save" OnClick="btnSaveRemarks_Click">
                            </asp:Button>
                            <%--<input type="button" value="Close" onclick="window.parent.closeDiv('dvProposeYellowCard');" />--%>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <div>
            <asp:Button ID="lnkAddRemarks" Text="Add Remarks" runat="server" OnClick="lnkAddRemarks_Click" />
        </div>
        <asp:Panel ID="pnlCardDetails" runat="server">
            <div id="dvAddAttachment" style="background-color: #EFF8FB; border: 1px solid #cccccc;
                padding: 4px; margin: 2px; display: none;">
                <table style="background-color: white; border: 1px solid #cccccc; border-collapse: collapse;
                    width: 100%;" border="1" cellpadding="2">
                    <tr>
                        <td>
                            Letter of Warning
                        </td>
                        <td>
                            <asp:FileUpload ID="Add_FileUpload_LW" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            LogBook Entry
                        </td>
                        <td>
                            <asp:FileUpload ID="Add_FileUpload_LB" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Other Attachment
                        </td>
                        <td>
                            <asp:FileUpload ID="Add_FileUpload_P" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right">
                            <asp:LinkButton ID="lnkAddAttachment" Text="Upload" runat="server" OnClick="lnkAddAttachment_Click" />
                            <a href='#' onclick="closeDiv('dvAddAttachment');">Close</a>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="background-color: #E6F8E0; border: 1px solid gray; text-align: left;
                padding: 2px; font-weight: normal;">
                <asp:Repeater ID="rptCardDetails" runat="server">
                    <ItemTemplate>
                        <div style="background-color: #E6F8E0; border: 1px solid gray; text-align: center;
                            padding: 2px; font-weight: bold;">
                            <asp:ImageButton ID="imgCardStatus" ImageUrl='<%# "~/Images/" + Eval("cardtype").ToString().Replace(" ","") + "_" + Eval("cardstatus").ToString() + ".png" %>'
                                runat="server" ImageAlign="AbsMiddle" />
                            <asp:Label ID="lblCardStatus" runat="server" Text='<%# Eval("cardtype").ToString() + " " + Eval("cardstatus").ToString()%>'></asp:Label>
                        </div>
                        <table style="background-color: White; border-collapse: collapse; width: 100%;" border="1"
                            cellpadding="2px" cellspacing="0">
                            <tr>
                                <td style="width: 100px">
                                    Proposed By
                                </td>
                                <td style="width: 175px">
                                    <%#Eval("ProposedBy") %>
                                </td>
                                <td style="width: 100px">
                                    Approved By
                                </td>
                                <td style="width: 175px">
                                    <%#Eval("ApprovedBy") %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date
                                </td>
                                <td>
                                  <%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation")))%>  
                                </td>
                                <td>
                                    Date
                                </td>
                                <td>
                                  <%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Approval_Date")))%>     
                                </td>
                            </tr>
                            <tr style="font-weight: bold; background-color: #EFF8FB;">
                                <td colspan="2">
                                    Proposer remarks:
                                </td>
                                <td colspan="2">
                                    Approver Remarks:
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 4px;">
                                    <%#Eval("ProposedRemarks")%>
                                </td>
                                <td colspan="2" style="padding: 4px;">
                                    <%#Eval("Approver_Remarks")%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr style="font-weight: bold; background-color: #EFF8FB;">
                                <td style="vertical-align: top; text-align: center;">
                                    Attachments:<br />
                                    <a href='#' onclick='showDiv("dvAddAttachment",<%#Eval("CardID")%>)'>Add</a>
                                </td>
                                <td colspan="3">
                                    <input id="chkLB_NA" type="checkbox" <%# (Eval("LogBookEntry").ToString()=="1")?"checked=checked":"" %>
                                        onclick='changeAttStatus(<%#Eval("CardID") %>,"LB",this.checked,<%=Session["UserID"] %>)' />LogBook
                                    Entry N/A
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="3">
                                    <asp:Repeater runat="server" ID="rptAttachments" DataSource='<%# ((System.Data.DataRowView) Container.DataItem).Row.GetChildRows("CardAttachments") %>'>
                                        <HeaderTemplate>
                                            <table style='width: 100%;'>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="background-color: #E0F8F7; color: Black;">
                                                <td>
                                                    <%# ((System.Data.DataRow)Container.DataItem)["AttachmentType"]%>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <%# "<a href='../Uploads/CrewDocuments/" + ((System.Data.DataRow)Container.DataItem)["ATTACHMENT_PATH"].ToString() + "' target='_blank'>"%>
                                                    <%# ((System.Data.DataRow)Container.DataItem)["ATTACHMENT_NAME"].ToString() + "</a>" %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table></FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlCardEntry" runat="server">
            <div style="background-color: white; border: 1px solid gray; text-align: left; padding: 2px;
                font-weight: bold; margin-top: 5px;">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            Propose Card
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCrewCardType" runat="server">
                                <asp:ListItem Text="Yellow Card" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Red Card" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="100px" Width="99%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Letter of Warning
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload_LW" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            LogBook Entry
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload_LB" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Other Attachment
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload_P" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnSaveAndApprove" runat="server" Text="Save & Approve" OnClick="btnSaveAndApprove_Click">
                            </asp:Button>
                            <asp:Button ID="btnProposeCard" runat="server" Text="Save" OnClick="btnProposeCard_Click">
                            </asp:Button>
                            <%--<input type="button" value="Close" onclick="window.parent.closeDiv('dvProposeYellowCard');" />--%>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlCardApprove" runat="server" Visible="false">
            <div style="background-color: #E6F8E0; border: 1px solid gray; text-align: left;
                margin-top: 10px; padding: 2px; font-weight: bold;">
                <center>
                    Approval</center>
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtApprovalRemark" runat="server" TextMode="MultiLine" Height="100px"
                                Width="99%">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Letter of Warning
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload_A_LW" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            LogBook Entry
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload_A_LB" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Other Attachment
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload_A" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click">
                            </asp:Button>
                            <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click">
                            </asp:Button>
                           <%-- <input type="button" value="Close" onclick="parent.hideModal('dvProposeYellowCard');" />--%>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
