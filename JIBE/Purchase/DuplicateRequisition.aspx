<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="DuplicateRequisition.aspx.cs" Inherits="Purchase_DuplicateRequisition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
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
    <style type="text/css">
        .tbl
        {
            border: 1px solid gray;
            height: 90px;
        }
        
        .view-header
        {
            font-size: 14px;
            font-family: Calibri;
            font-weight: bold;
            text-align: left;
            width: 100%;
            color: Black;
            background-color: #81DAF5;
            border-collapse: collapse;
            padding: 2px 0px 2px 3px;
        }
        
        .tbl-content
        {
            border: 1px solid #81DAF5;
            width: 100%;
            border-collapse: collapse;
        }
        
        .tbl-footer
        {
            border-bottom: 1px solid #81DAF5;
            border-left: 1px solid #81DAF5;
            border-right: 1px solid #81DAF5;
            width: 100%;
            border-collapse: collapse;
            padding: 2px 2px 2px 2px;
        }
        
        .tbl-footer-td
        {
            width: 100%;
            padding: 2px 2px 2px 2px;
            text-align: left;
            background-color: #81DAF5;
        }
        .tdh
        {
            text-align: right;
            padding: 3px 2px 3px 0px;
        }
        .tdd
        {
            text-align: left;
            padding: 3px 2px 3px 0px;
        }
    </style>
    <script type="text/javascript">
        /*The Following Code added To adjust height of the popup when open after entering search criteria.*/
        $(document).ready(function () {
            window.parent.$("#Add_Item").css("height", (parseInt($("#pnlEvaluation").height()) + 50) + "px");
            window.parent.$(".xfCon").css("height", (parseInt($("#pnlEvaluation").height()) + 50) + "px").css("top", "50px");
        });
    </script>
    
</head>
<body style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 99%;">
    <table style="border: 0px solid #cccccc; font-family: Tahoma; overflow: Auto; font-size: 12px;
        width: 100%;">
        <tr>
            <td>
                <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptM" runat="server">
                </asp:ScriptManager>
                <center>
                    <div class="error-message" onclick="javascript:this.style.display='none';">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlEvaluation" runat="server" Visible="true">
                        <div id="divAddSpare" style="width: 100%; height:100%; border: 1px solid #cccccc; background-color: #fff;">
                            <div id="Div2" class="page-title">
                                <asp:Label ID="lblHeader" runat="server" Text="Duplicate Requisition" ForeColor="#000099"></asp:Label>
                            </div>
                            <table cellpadding="1" cellspacing="1" width="100%">
                                <tr id="trCatalogue" runat="server">
                                    <td colspan="2" align="center">
                                        You have chosen to duplicate an existing requisition.<br />
                                         Fill the details below and click 'Create a New Requisition'.
                                    </td>
                                   
                                </tr>
                                  <tr style="height: 10px">
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                               <tr style="height: 30px">
                                    <td align="right" style="width: 30%">
                                       Vessel :
                                    </td>
                                  
                                    <td align="left" style="width: 70%">
                                         <asp:DropDownList ID="ddlVessel"  runat="server" Width="200px">
                                        </asp:DropDownList>
                                          <asp:RequiredFieldValidator ID="ReqPOType" runat="server" InitialValue="0" Display="None"
                                                        ErrorMessage="Vessel is mandatory field." ControlToValidate="ddlVessel" ValidationGroup="vgSubmit"
                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                 <tr style="height: 30px">
                                    <td align="right" style="width: 30%;">
                                        Delivery Date :
                                    </td>
                                 
                                    <td align="left" style="width: 70%;">
                                         <asp:TextBox ID="txtDeliveryDate" onchange="return CheckValidation()" runat="server" Width="200px"></asp:TextBox>
                                                    <cc1:CalendarExtender TargetControlID="txtDeliveryDate" ID="caltxtDeliveryDate" runat="server">
                                                    </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr style="height: 30px">
                                    <td align="right" style="width: 30%;">
                                       Delivery Port :
                                    </td>
                                  
                                    <td align="left" style="width: 70%;">
                                        <asp:DropDownList ID="ddlDeliveryPort" runat="server"   Width="200px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqDeliveryPort" runat="server" InitialValue="0" Enabled = "false"
                                                        Display="None" ErrorMessage="Delivery Port is mandatory field." ControlToValidate="ddlDeliveryPort"
                                                        ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="height: 30px">
                                    <td align="right" style="width: 30%;">
                                       Include Quotations :
                                        
                                    </td>
                                   
                                    <td align="left" style="width: 70%;">
                                         <asp:DropDownList ID="ddlIncludeQtn" runat="server"   Width="200px">
                                         <asp:ListItem Value="Yes" >Yes</asp:ListItem>
                                         <asp:ListItem Value="No" >No</asp:ListItem>
                                                    </asp:DropDownList>
                                    </td>
                                </tr>
                            
                                  <tr style="height: 25px">
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            
                                  <tr style="height: 30px">
                                    <td colspan="2" align="center">
                                     
                                        <asp:Button ID="btnSave" Text="Create New Requisition" runat="server" 
                                            Width="170px" ValidationGroup="vgSubmit" onclick="btnSave_Click"/>
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" Width="70px" OnClick="btnCancel_Click" />
                                       
                                    </td>
                                </tr>
                                  <tr style="height: 20px">
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblItemError" Text="" runat="server" Visible="false" ForeColor="Red"
                                            Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                  <tr style="height: 10px">
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hdnReqsnCode" runat="server" />
                                        <asp:HiddenField ID="hdnDocumentCode" runat="server" />
                                    </td>
                                    <td>
                                        <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="vgSubmit" />
                                    </td>

                                </tr>
                            </table>
                        </div>
       
        
     

                    </asp:Panel>
                    </ContentTemplate>
                     
                    </asp:UpdatePanel>
                </center>
                </form>
            </td>
        </tr>
    </table>
</body>
</html>
