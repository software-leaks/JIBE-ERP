<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PURC_Add_Item.aspx.cs" Inherits="Purchase_PURC_Add_Item" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script language="javascript" type="text/javascript">

        function ShowUploader() {
            
            if (document.getElementById($('[id$=hdnItemID]').attr('id')).value == "") {
                alert('Save Item Before adding attachment');
                //onSaveSpare();
                return false;
            }
            showModal('dvPopupAddAttachment', false, fn_OnClose);
            return false;
        }
       
        function ShowDetailsUploader() {
            if (document.getElementById($('[id$=hdnItemID]').attr('id')).value == "") {
                alert('Save Item Before adding attachment');
                //onSaveSpare();
                return false;
            }
            showModal('dvPopupAddAttachment', false, fn_OnClose);
            return false;

        }
        function fn_OnClose() {
            $('[id$=btnLoadFiles]').trigger('click');
            //__doPostBack('ctl00_MainContent_btnLoadFiles', true);            
        }
       
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
                        <div id="divAddSpare" style="width: 100%; border: 1px solid #cccccc; background-color: #fff;">
                            <div id="Div2" class="page-title">
                                <asp:Label ID="lblHeader" runat="server" Text="Add an Item To Catalogue" ForeColor="#000099"></asp:Label>
                            </div>
                            <table cellpadding="1" cellspacing="1" width="100%">
                                <tr id="trCatalogue" runat="server">
                                    <td align="right" style="width: 15%">
                                        Catalogue
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblCatalogue" runat="server" ForeColor="#000099"></asp:Label>
                                    </td>
                                </tr>
                               <tr id="trSubCatalogue" runat="server">
                                    <td align="right" style="width: 15%">
                                        Sub Catalogue
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblSubCatalogue" runat="server" ForeColor="#000099"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Account Classification
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblAccClassification" runat="server" ForeColor="#000099"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Drawing number
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtItemDrawingNumber" MaxLength="30" Width="200px" runat="server"
                                            CssClass="txtInput"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Part number
                                        <asp:Label ID="Label1" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtItemPartNumber" MaxLength="25" Width="200px" CssClass="txtInput"
                                            runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ReqRemarks" runat="server" Display="None" ErrorMessage=" Part number is mandatory field."
                                            ControlToValidate="txtItemPartNumber" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Name
                                        <asp:Label ID="Label2" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtItemName" Width="450px" MaxLength="200" CssClass="txtInput" runat="server"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" Display="None" ErrorMessage="Name is mandatory field."
                                                                    ControlToValidate="txtItemName" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Description
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td rowspan="2" align="left">
                                        <asp:TextBox ID="txtItemDescription" TextMode="MultiLine" runat="server" CssClass="txtInput"
                                            MaxLength="2000" Width="450px" Height="50px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="height: 5px">
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Unit
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlUnit" CssClass="txtInput" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Min qty
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMinQty" Width="450px" MaxLength="200" CssClass="txtInput" runat="server"></asp:TextBox>
                                         <asp:RegularExpressionValidator ID="RegTaxRate" runat="server" ErrorMessage="Min Qty is not valid"
                                                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtMinQty" ForeColor="Red"
                                                                    ValidationExpression="^[0-9]+$"> </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Max qty
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMaxQty" Width="450px" MaxLength="200" CssClass="txtInput" runat="server"></asp:TextBox>
                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Min Qty is not valid"
                                                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtMaxQty" ForeColor="Red"
                                                                    ValidationExpression="^[0-9]+$"> </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Item category
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlItemCategory" CssClass="txtInput" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                  <tr style="height: 5px"  id="trCritical" runat="server" >
                                    <td align="right">
                                        Critical
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCritical" CssClass="txtInput" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                  <tr style="height: 5px">
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Attachment
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                    <asp:UpdatePanel ID="updAttach" runat="server" RenderMode="Block">
                        <ContentTemplate>
                                        <table style="width: 100px" align="left">
                                            <tr>
                                                <td style="width: 15px;">
                                                    <asp:HyperLink ID="lnkImageUploadName" Width="15px" runat="server" Target="_blank"
                                                        ImageUrl="~/Images/attachment.png"> </asp:HyperLink>
                                                </td>
                                                <td style="width: 60px;">
                                                   <asp:ImageButton ID="imgAttach" runat="server" 
                                                        ImageUrl="../Images/AddAttachment.png" onclick="imgAttach_Click"  />
                                                     
                                                   
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr style="height: 5px">
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Product details attachment
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td align="left">
                                     <asp:UpdatePanel ID="UpdatePanel5" runat="server" RenderMode="Block">
                        <ContentTemplate>
                                        <table style="width: 100px" align="left">
                                            <tr>
                                                <td style="width: 15px;">
                                                    <asp:HyperLink ID="lnkProductDetailUploadName" Width="15px" runat="server" Target="_blank"
                                                        ImageUrl="~/Images/attachment.png"> </asp:HyperLink>
                                                </td>
                                                <td style="width: 60px;">
                                                <asp:ImageButton ID="btnProductAttImage" runat="server" 
                                                        ImageUrl="../Images/AddAttachment.png" onclick="btnProductAttImage_Click"   />
                                               
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                        </ContentTemplate>
                                       
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                              
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSpareCreatedBy" runat="server" ForeColor="#000099"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSpareModifiedBy" runat="server" ForeColor="#000099"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lblSpareDeletedBy" runat="server" ForeColor="#000099"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <%-- <asp:Button ID="btnAddNewItem" Text="Add New" runat="server" Width="70px" OnClientClick="return onAddSpare();"/>--%>
                                        <%--OnClick="btnAddNewItem_Click" --%>
                                        <asp:Button ID="btnSaveItem" Text="Add Item" runat="server" Width="70px" ValidationGroup="vgSubmit"
                                            OnClick="btnSaveItem_Click" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" Width="70px" OnClick="btnCancel_Click" />
                                        <%--OnClick="btnSaveItem_Click"--%>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblItemError" Text="" runat="server" Visible="false" ForeColor="Red"
                                            Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hdfItemOperationMode" runat="server" />
                                         <asp:HiddenField ID="hdnImageURL" runat="server" />
                                           <asp:HiddenField ID="hdnProductURL" runat="server" />
                                            <asp:HiddenField ID="hdnItemID" runat="server" />
                                             <asp:HiddenField ID="hdnImageType" runat="server" />
                                    </td>
                                    <td>
                                        <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="vgSubmit" />
                                    </td>
                                     <td>
                                      <asp:UpdatePanel ID="UpdatePanel12" runat="server" RenderMode="Block">
                        <ContentTemplate>
                                       <div style="display:none">
                                        <asp:Button ID="btnLoadFiles" OnClick="btnLoadFiles_Click"   runat="server" />
                                       <asp:Button ID="lblImageType" Text=""   runat="server" />
                                       </div>
                                       </ContentTemplate></asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </div>
        <div id="dvPopupAddAttachment" title="Add Attachments" style="display: none; width: 500px;">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: left">
                             
                                <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                                <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                                    Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                    MaximumNumberOfFiles="10" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
     

                    </asp:Panel>
                    </ContentTemplate>
                      <Triggers>
                        <asp:PostBackTrigger ControlID="btnProductAttImage" />
                        <asp:PostBackTrigger ControlID="imgAttach" />
                    </Triggers>
                    </asp:UpdatePanel>
                </center>
                </form>
            </td>
        </tr>
    </table>
</body>
</html>
