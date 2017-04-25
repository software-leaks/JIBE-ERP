<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTP_SendRFQ_PopUp.aspx.cs"
    Inherits="Purchase_CTP_SendRFQ_Popup" %>

<%@ Register Src="../UserControl/uc_Purc_Ctp_Send_RFQ.ascx" TagName="uc_Purc_Ctp_Send_RFQ"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="scrmrfq" runat="server"></asp:ScriptManager>
     <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
        <asp:UpdatePanel ID="upd_SendRFQ" runat="server">
            <ContentTemplate>
                <uc1:uc_Purc_Ctp_Send_RFQ ID="uc_Purc_Ctp_Send_RFQMore" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
