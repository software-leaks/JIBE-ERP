<%@ Page Title="NOQuotation Comparision " Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="NOQuatationEvalution.aspx.cs" Inherits="Purchase_Quotation_Evaluation_Gridview" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Import Namespace="System.Web.Services" %>
<%@ Register Src="../UserControl/ucPurcQuotationApproval.ascx" TagName="ucApprovalUser"
    TagPrefix="ucUser" %>
<asp:Content ID="header" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/boxover.js"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Get_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Purc_Get_Remarks_All.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Ins_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Quotation_Evaluation.js?v=9" type="text/javascript"></script>
    <link href="../Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.alerts.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
       
        .target
        {
            width: 80px;
            text-align: center;
            border: 2px solid #666666;
            padding: 5px;
            background-color: #00FFFF;
            height: 45px;
            display: block;
            float: left;
        }
        
        .amount-css
        {
            text-align: right;
            padding-right: 5px;
        }
        
        .text-css
        {
            text-align: left;
        }
        
        
        div#divInfoQTN
        {
            width: 1210px;
            max-height: 400px;
            overflow: scroll;
            position: relative;
        }
        
        div#divInfoQTN th
        {
            top: 0px;
            position: relative;
        }
        .gtdth
        {
            z-index: 0;
            background-color: #F2F2F2;
            position: relative;
            cursor: default;
            left: 0px;
            border-collapse: collapse;
            padding-left: 3px;
            white-space: nowrap;
            color: Black;
            font-size: 11px;
            border: 0px solid white;
        }
        .hd
        {
            background-image: url(../images/suppliergridbk.png); /*background-color: #00868B;*/
            position: relative;
            cursor: default;
             /*z-index: 200;*/
             /*ABOVE WAS COMMENTED FOR JIT 11321 & 11868/*/
            z-index: 5;
            left: 0px;
            color: Black;
            font-size: 11px;
            border-collapse: collapse;
            border: 0px solid white;
            padding-left: 3px;
            white-space: nowrap;
        }
        .NewItem
        {
            background-color: Yellow;
        }
        
        .tdTooltip
        {
            border-bottom: 1px solid gray;
            width: 250px;
        }
       
        
    </style>
    <script type = "text/javascript">
        function Check_Click(objRef) {
       

            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                row.style.backgroundColor = "aqua";
            }
            else {
                if (row.rowIndex % 2 == 0) {
                    row.style.backgroundColor = "#F6F6F6";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }

            var GridView = row.parentNode;
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                var headerCheckBox = inputList[0];
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }
        function checkAll(objRef) {
             var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        if (row.rowIndex % 2 == 0) {
                            row.style.backgroundColor = "#F6F6F6";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
</script>

  </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
<asp:UpdatePanel runat="server" ID="UpdatePanel1"> 
<ContentTemplate>
<div id="divMain" style="width: 100%; border: 1px solid #cccccc; background-color: #fff;">

<table style="width: 100%;">
    
<tr align="left" style="border: 12px solid #cccccc;height:80px">
<td>
<div id="ActionDiv" style="width: 100%">
<table id="ActionTable" cellpadding="0">
<tr>
<td><asp:Button ID="btn_SaveQuot" Width="150px" runat="server" Text="Save Quotation Evaluation"  OnClientClick="return UpdateEvalution();" OnClick="btnSaveEvaln_Click" /></td>
<td><asp:Button ID="btn_addattach" Width="150px" runat="server" Text="Add Attachment" OnClientClick="javascript:document.getElementById('dvpurcAttachments').style.display = 'block';return false;"/></td>
<td><asp:Button ID="btn_ViewHistory" Width="150px" runat="server" Text="View Catalogue History" OnClientClick="return ItemsHistoryShow()" /></td>
<td><asp:Button ID="btn_Currency" Width="150px" runat="server" Text="Currency :USD" /></td>
</tr>
<tr>
<td><asp:Button ID="btn_approve" Width="150px" runat="server" Text="Approve PO" OnClick="ApprovePO_Onclick" /></td>
<td><asp:Button ID="btn_GetRemarks" Width="150px" runat="server" Text="Remarks" /></td>
<td><asp:Button ID="btn_approve_hstry" Width="150px" runat="server" Text="Track Approval History" OnClientClick="return divHistoryShow()" /></td>
</tr>
</table>
</div>
</td>
</tr>
<%---------------%>

<tr >
 
    <td  >
    <div id="DivEvaluation" style="width: 100%;background-color:Silver">
    <table id="table_evaldata" style="width: 100%;">

    <tr style="border: 12px solid #F6F6F6;height:60px"> 

                        <td Width="25%" valign="top">
                            <br>
                           
                            <b>Vessel:</b>
                            <asp:Label ID="lbl_Vessel" runat="server" Height="16px" Width="150px"></asp:Label>
                            
    </td> 
<td  Width="25%">
<b>Receival Date:</b><asp:Label ID="lbl_ReceivalDate" runat="server" ></asp:Label>
   <br />
<br />
    <b>Requested Delivery Date:</b><asp:Label ID="lbl_Req_Del_Date" runat="server"></asp:Label>
    
</td>

                        <td  Width="25%">
<b>Function:</b><asp:Label ID="lbl_Function" runat="server" ></asp:Label>
<br />
<br />
<b>Catalogue/System:</b><asp:Label ID="lbl_Cat_Sys" runat="server" ></asp:Label>
</td>

                        <td  Width="25%">
<b>Account Type:</b><asp:Label ID="lbl_AccountType" runat="server" ></asp:Label>
<br />
<br />
<b>Maker:</b><asp:Label ID="lbl_Maker" runat="server" ></asp:Label>
</td>
       </tr>
       </table>
      </div>
      </td>
      
</tr>

<%---------------%>

<tr>
<td>
<br />
<br />
<div class="page-title" align="center" ><asp:Label ID="Label1" style="font-weight:bold;color:Black;background-color:" Text="Requisition Summary" runat="server"></asp:Label></div>

<div width ="100%">
<table >
<tr class="HeaderStyle-css-teal" style="border-bottom-color:Black"><td ><b>Supplier Name</b></td>  <td><b>requisition Number</b></td>  <td><b>Selected Items Amount</b></td>  <td><b>Selected Items Discount</b></td>  <td><b>Selected Items VAT</b></td>  <td><b>Selected Items Withhold Tax</b></td>  <td><b>Total Amount</b></td>  <td><b>Advance Payment</b></td>  <td><b>Rework to Purchaser</b></td>   <td><b>Quotation Attachment</b></td> </tr>

<tr style="background-color:#F6F6F6;border-bottom-color:Black;border-bottom-style:solid"><td>
<asp:Label ID="lbl_supplier" runat="server" ></asp:Label></td>  
<td><asp:Label ID="lbl_reqsnNo" runat="server"></asp:Label></td> 
<td  align="right"><asp:Label ID="lbl_ItemAmount" runat="server" ></asp:Label></td> 
<td  align="right"><asp:Label ID="lbl_Discount" runat="server"></asp:Label></td> 
<td align="right"><asp:Label ID="lbl_Vat" runat="server" ></asp:Label></td> 
<td align="right"><asp:Label ID="lbl_withholdTax" runat="server"></asp:Label></td> 
<td align="right"><asp:Label ID="lbl_TotalAmount" runat="server" ></asp:Label></td> 
<td align="right"><asp:TextBox ID="lbl_Advance" runat="server" ></asp:TextBox></td> 
<td align="center"> <asp:ImageButton ID="HL_Rework" runat="server" NavigateUrl="#" Style="cursor: pointer" ImageUrl="~/Images/processing-time-icon.png"
                                                            Text="RW"  OnClick="onRework"  CommandArgument='<%#Eval("Supplier")%>' 
                                                                            Font-Size="10px" ToolTip="Send for Re-quotaion." ></asp:ImageButton>
                                                   </td> 
<td align="center"><asp:ImageButton ID="ImgAttachment" Height="20px" Width="20px" ImageUrl="../Images/attachment.png" runat="server" style="border: 0px solid white" /></td>

</tr>
</table>
</div>


</td>
</tr>
<%---------------%>

<tr>

<td>
    <br/>
    <div ID="div_ReqsnDtl" Width="100%">
        <div class="page-title">
            <asp:Label ID="Label2" runat="server" 
                style="font-weight:bold;color:Black;background-color:" 
                Text="Requisition Detail"></asp:Label>
        </div>
        <asp:UpdatePanel ID="GridPanal" runat="server">
            <ContentTemplate>
                <asp:GridView ID="Grid_RqsnDtl" runat="server" AutoGenerateColumns="false" 
                    BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" CellPadding="2" 
                    CellSpacing="0" ClientIDMode="Static" DataKeyNames="" 
                    EmptyDataText="No Record Found!" Width="100%">
                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <RowStyle CssClass="RowStyle-css" />
                    <HeaderStyle CssClass="HeaderStyle-css" />
                    <EmptyDataRowStyle Font-Bold="true" Font-Size="12px" ForeColor="Red" 
                        HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="Chk_Header" runat="server" OnClick="checkAll(this);" 
                                    Text="NO" TextAlign="Left" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="Chk_Item" runat="server" OnClick="Check_Click(this);" 
                                    TextAlign="Left" />
                                <asp:HiddenField ID="HD_ITEMREF" runat="server" 
                                    Value='<%# Eval("ITEM_REF_CODE")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Draw Number
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_DrawNo" runat="server" Text='<%# Eval("Drawing_Number")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Part Number
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_PartNo" runat="server" Text='<%# Eval("Part_Number")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Item Name
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_ItemName" runat="server" 
                                    Text='<%# Eval("Short_Description")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Unit
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_Unit" runat="server" Text='<%# Eval("Unit_and_Packings")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                            <HeaderTemplate>
                                Historic Low Price
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_HistoryPrice" runat="server" Text="History Price"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                            <HeaderTemplate>
                                Last Order
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_LastOrder" runat="server" Text="Last Order"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                            <HeaderTemplate>
                                ROB
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_ROB" runat="server" Text='<%# Eval("ROB_QTY")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Quantity
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_Qnty" runat="server" Text='<%# Eval("REQUESTED_QTY")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                            <HeaderTemplate>
                                Price Per Unit
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_PPU" runat="server" Text='<%# Eval("ORDER_RATE")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                            <HeaderTemplate>
                                Discount Amount
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_Discount" runat="server" Text='<%# Eval("DISCOUNT")%>' ToolTip="This is the discount percentage the supplier has provided.&#013; It is already calculated in the evaluated sums"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                            <HeaderTemplate>
                                VAT Amount
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_VAT" runat="server" Text='<%# Eval("VAT")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                            <HeaderTemplate>
                                Total Item Price
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbl_TotalPrice" runat="server" Text='<%# Eval("Total_Amount")%>'> </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br/>
    
   
</td>
</tr>
<tr>
<td>
<asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
</td>
</tr>
</table>

</div>
 <%--<asp:HiddenField ID="hdfDocumentCode" runat="server" />--%>
 <asp:HiddenField ID="hdfUserID"  runat="server" />
  <asp:HiddenField ID="lblITEMSYSTEMCODE" runat="server" />
  <asp:HiddenField ID="lblVesselCode" runat="server" />
 <input id="hdfDocumentCode" type="hidden" />
 <asp:HiddenField id="hdfMaxQuotedAmount"  runat="server"  />
 <asp:HiddenField id="hdfOrderAmounts"  runat="server"  />
 <asp:HiddenField id="hdfSupplierBeingApproved"  runat="server"  />



 <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnApprove" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div id="dvSendForApproval" style="position: fixed; left: 30%; top: 15%; padding: 10px 0px 10px 0px;
                        border: 1px solid gray; z-index: 300" runat="server" class="popup-css" visible="false">
                        <ucUser:ucApprovalUser ID="ucApprovalUser1" OnstsSaved="OnStsSaved" runat="server" />
                    </div>
                    <div id="dvSendTosuppdt" style="position: fixed; left: 30%; top: 15%; padding: 10px 0px 10px 0px;
                        width: 600px; border: 1px solid gray; z-index: 300" runat="server" class="popup-css"
                        visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 100%; text-align: left">
                                    <asp:GridView ID="gvQuotationList" runat="server" Font-Size="11px" Width="100%" 
                                        AutoGenerateColumns="False" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Qtn Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblqtn" runat="server" Text='<%# Bind("QUOTATION_CODE") %>' BackColor='<%#Eval("active_PO").ToString()=="1"?System.Drawing.Color.Silver:System.Drawing.Color.Transparent %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupplier" runat="server" Text='<%# Bind("Full_NAME") %>' BackColor='<%#Eval("active_PO").ToString()=="1"?System.Drawing.Color.Silver:System.Drawing.Color.Transparent %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Port">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPort" runat="server" Text='<%# Bind("PORT_NAME") %>' BackColor='<%#Eval("active_PO").ToString()=="1"?System.Drawing.Color.Silver:System.Drawing.Color.Transparent %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="active_PO" Visible="False" />
                                        </Columns>
                                        <HeaderStyle ForeColor="White" BackColor="#5D7B9D" />
                                    </asp:GridView>
                                    <span style="font-size: 11px; font-weight: bold; font-family: Verdana">Select BGT Code:</span>
                                    <asp:DropDownList ID="ddlBGTCodeToSuppdt" runat="server" Style="font-size: small"
                                        Width="60%">
                                    </asp:DropDownList>
                                    <hr />
                                    <ucUser:ucApprovalUser ID="ucApprovalUserToSuppdt" OnstsSaved="OnStsSavedSentToApprover"
                                        runat="server" />
                                    <asp:HiddenField ID="HiddenFieldSuppdtRemark" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
<div id="dvpurcAttachments" style="border: 1px solid gray; z-index: 502; color: Black;
        padding: 5px; width: 600px; display: none; position: fixed; left: 20%; top: 100px;
        text-align: right" class="popup-css">
        <img onclick="javascript:document.getElementById('dvpurcAttachments').style.display = 'none';"
            alt="close" src="../Images/Close.gif" />
        <table width="100%" style="text-align: left">
            <asp:Repeater ID="rpAttachment" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Eval("SlNo") %>.
                        </td>
                        <td>
                            <asp:HyperLink ID="lnkAtt" Target="_search" Font-Size="11px" Font-Names="verdana"
                                runat="server" NavigateUrl='<%# Eval("File_Path")%>'> <%# Eval("File_Name")%>  </asp:HyperLink>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div id="divHistory" class="popup-css" style="display: none; left: 35%; top: 25%;
        position: fixed; padding-top: 10px; padding-left: 30px; padding-right: 30px;
        padding-bottom: 30px; color: White; text-align: right; max-height: 400px; max-width: 700px;
        overflow: auto; border: 1px solid black; z-index: 456">
        <img id="imgdivclose" onclick="DivHistoryClose()" alt="close" title="close" src="../Images/Close.gif" />
        <asp:GridView ID="gvApprovalHistory" AutoGenerateColumns="true" EmptyDataText="no record found"
            CellPadding="4" OnRowCreated="gvApprovalHistory_RowCreated" runat="server" OnDataBound="gvApprovalHistory_DataBound">
            <HeaderStyle CssClass="HeaderStyle-css" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
            <RowStyle HorizontalAlign="Center" CssClass="RowStyle-css" />
        </asp:GridView>
    </div>
    <div id="divApprove" style="width:700px; display: none; z-index: 300; color: black"
                title="Assign budget code and Approve">
                <asp:UpdatePanel ID="updBGT" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table cellspacing="0" style="height: 100px; width: 100%; padding: 10px">
                            <tr>
                                <td style="border-bottom: 1px solid gray">
                                    <table style="width: 100%; height: 58px;" cellpadding="1" cellspacing="0">
                                        <tr>
                                            <td align="right" style="font-size: small">
                                                Requisition Type:&nbsp;
                                            </td>
                                            <td style="width: 300px" align="left">
                                                <asp:DropDownList ID="ddlReqsnType" runat="server" DataSourceID="objsrcReqsnType"
                                                    AutoPostBack="true" DataTextField="Description" DataValueField="code" Width="400px"
                                                    OnSelectedIndexChanged="ddlReqsnType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="objsrcReqsnType" runat="server" SelectMethod="Get_ReqsnType"
                                                    TypeName="ClsBLLTechnical.TechnicalBAL"></asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="font-size: small">
                                                Budget code:<span style="color: Red">*</span>
                                            </td>
                                            <td style="width: 400px" align="left">
                                                <asp:DropDownList ID="ddlBudgetCode" runat="server" Style="font-size: small" Width="400px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlBudgetCode" Display="Dynamic"
                                                    ControlToValidate="ddlBudgetCode" InitialValue="0" ValidationGroup="finalapprl"
                                                    runat="server" ErrorMessage="Please select budget code."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="font-size: small">
                                                Approver's Remarks: <span style="color: Red">*</span>
                                            </td>
                                            <td align="left" style="width:400px">
                                                <asp:TextBox ID="txtComment" runat="server" Height="40px" TextMode="MultiLine" Width="400px"
                                                    Style="font-size: small"> </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtComment" ValidationGroup="finalapprl"
                                                    ControlToValidate="txtComment" Display="Dynamic" runat="server" ErrorMessage="Please enter comment."></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" style="padding: 10px 0px 10px 0px">
                                                <asp:Button ID="btnApprove" runat="server" ValidationGroup="finalapprl" Text="Approve"
                                                    Height="35px" Width="100px" OnClick="btnApprove_Click" OnClientClick="UpdateEvalution();"
                                                    Style="font-size: small" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td align="center" colspan="2" style="padding: 10px 0px 10px 0px">
                                                <asp:Button ID="btnRequestAmount" runat="server"   Text="Request For Budget Limit Increase"
                                                    Height="35px" Width="250px" 
                                                    Style="font-size: small" onclick="btnRequestAmount_Click" />
                                            </td>
                                        </tr>
                                          <tr>
                                            <td align="center" colspan="2" style="padding: 10px 0px 10px 0px">
                                               <asp:Label ID="lblBudgetMsg" runat="server" ForeColor="Red" Text="" ></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                     <asp:HiddenField ID="hdnBudgetCode" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
    <div id="dvReworktoSuppler" class="popup-css" style="left: 35%; top: 25%; position: fixed;
                padding-top: 10px; padding-left: 30px; padding-right: 30px; padding-bottom: 30px;
                color: White; text-align: right; height: 100px; width: 400px; border: 1px solid black;
                z-index: 500; text-align: left; color: Black; display: none;">
                <spam style="font-weight: bold; font-size: 11px; color: Black; padding-bottom: 10px"> Remark on rework: </spam>
                <asp:TextBox ID="txtReworkToSupplier" TextMode="MultiLine" runat="server" Height="60px"
                    Width="400px"></asp:TextBox>
                <asp:Button ID="btnReworkToSupplier" runat="server" Text="OK" Height="30px" Width="80px"
                    OnClick="btnReworkToSupplier_Click" OnClientClick="javascript:return confirm('are you sure to send for rework !')" />
                <input id="btncancelRWKSUPP" style="height: 30px" value="Cancel" type="button" onclick="DivReworkSuppClose()" />
                <asp:HiddenField ID="hdfSuppCode" runat="server" />
                <asp:HiddenField ID="hdfQTNCode" runat="server" />
            </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</center>
</asp:Content>
