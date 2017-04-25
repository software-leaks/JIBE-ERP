<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_AuditTrail.aspx.cs" Inherits="ASL_ASL_AuditTrail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
         $(document).ready(function () {
             window.parent.$("#ASL_AuditTrail").css("height", (parseInt($("#pnl").height()) + 80) + "px");
             window.parent.$(".xfCon").css("height", (parseInt($("#pnl").height()) + 80) + "px").css("top", "50px");
         });
    </script>
</head>
<body style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 99%;">
<table  style="border: 0px solid #cccccc; font-family: Tahoma; overflow: Auto; font-size: 12px; width: 100%;">
                                 <tr><td>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-title" class="page-title">
               Transaction History
            </div>
              <asp:UpdatePanel ID="paneltable" runat="server">
                              <contenttemplate>
            <asp:Panel Visible="true" ID="pnl"  runat="server">
            <table width="100%" cellpadding="2" cellspacing="2">
                 <tr>
               
               <td colspan="4" align="left">  <div  style="font-size: 15px; font-family: Tahoma;">
               Supplier Code -  <asp:Label ID="lblSupplierCode" runat="server" Width="100px"  Text=""></asp:Label>&nbsp;&nbsp;
                Supplier Name -  <asp:Label ID="lblSuppliername" runat="server" Width="400px"  Text=""></asp:Label>
                 </div>
                </td></tr>
                <tr>
                    <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                        <div style="height: 600px; background-color: White; overflow-y: scroll; max-height: 600px">
                            <asp:GridView ID="gvlog" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                DataKeyNames="ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                AllowSorting="true">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAction" runat="server" Text='<%#Eval("Created_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Action By">
                                        <HeaderTemplate>
                                            Action By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUser" runat="server" Text='<%#Eval("User_name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="10%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAction" runat="server" Text='<%#Eval("Action")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="15%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <HeaderTemplate>
                                            Description
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Width="100%"  Style="word-wrap: normal; word-break: break-all;" Text='<%#Eval("Description")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="65%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                   
                                   
                                </Columns>
                            </asp:GridView>
                            <uc1:uccustompager id="ucCustomPager1" runat="server" onbinddataitem="BindGrid" />
                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                        </div>
                    </td>
                </tr>
              
            </table>
            </asp:Panel>
            </contenttemplate></asp:UpdatePanel>
        </div>
        <div style="display:none;" >
         <asp:TextBox ID="txtRemarksID" runat="server"  Width="1px"></asp:TextBox>
          <asp:TextBox ID="txtSupplierCode" runat="server"  Width="1px"></asp:TextBox>
        </div>
    </center>
  
    </form>
    </td></tr></table>
</body>
</html>
