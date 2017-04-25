<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTP_Quotation_Items_PopUp.aspx.cs"
    Inherits="Purchase_CTP_Quotation_ItemsPopup" %>

<%@ Register Src="../UserControl/uc_Purc_Ctp_AddItem.ascx" TagName="uc_Purc_Ctp_AddItem"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="script" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <uc1:uc_Purc_Ctp_AddItem ID="uc_Purc_Ctp_AddItemDetails" runat="server" /> 
            
            </ContentTemplate>
           
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
