<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG_Logistic_PO_Details.aspx.cs"
    EnableEventValidation="false" Inherits="Purchase_LOG_Logistic_PO_Details" %>

<%@ Register Src="../UserControl/ctlPortList.ascx" TagName="ctlPortList" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/uc_SupplierList.ascx" TagName="uc_SupplierList"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <%--   <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>--%>
      <script src="../Scripts/Purc_Logistic.js" type="text/javascript"></script>
    <script src="../Scripts/CustomAsyncDropDown.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <title>Logistic PO Details</title>
    <style type="text/css">
        body
        {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            background-color: White;
        }
        
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 3px 3px 3px 0px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
            border-color: #C9C9CF;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 3px 2px 3px 3px;
            height: 20px;
            vertical-align: middle;
            border-color: #C9C9CF;
        }
        
        input
        {
            font-family: verdana;
        }
    </style>
    <script type="text/javascript">



        function CalculateTotal() {

            var griditems = document.getElementById('gvItemList');
            var griditems_ctlID = 'gvItemList'
            var Exchrate = document.getElementById('hdfExchrate').value;

            var Tot_Amount = 0;
            var k = 0;
            for (var j = 2; j < griditems.rows.length; j++) {

                var amt_id = "";
                amt_id = (j < 10) ? griditems_ctlID + "_ctl0" + j.toString() + "_txtAmount" : griditems_ctlID + "_ctl" + j.toString() + "_txtAmount";
                var rowamt = parseFloat(document.getElementById(amt_id).value);
                if (rowamt > 0)
                    Tot_Amount = Tot_Amount + rowamt;

                k = j;
            }
            if (k != 0) {
                document.getElementById(griditems_ctlID + "_ctl0" + (k + 1).toString() + "_lblTotalAmount").innerHTML = "Total : " + Tot_Amount.toFixed(2);
                document.getElementById(griditems_ctlID + "_ctl0" + (k + 1).toString() + "_lblTotalAmount_USD").innerHTML = "&nbsp;&nbsp[USD  " + (Tot_Amount * Exchrate).toFixed(2) + "]";
                document.getElementById('hdf_TotalAmount_USD').value = (Tot_Amount * Exchrate).toFixed(2);
            }
        }

        function checkNumber_local(thisid) {

            var obj = document.getElementById(thisid.id);
            if (isNaN(obj.value) || obj.value == "") {
                obj.value = 0;
                alert("Only number allowed !");
            }

        }

        function fn_OnClose() {
            $('[id$=btnLoadFiles]').trigger('click');
            //__doPostBack('ctl00_MainContent_btnLoadFiles', true);            
        }
      
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptmngPO" runat="server">
    </asp:ScriptManager>
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 0px; color: #282829; width: 890px;
        padding: 10px">
        <table width="100%" style="padding-bottom: 5px; padding-top: 5px; border-collapse: collapse;
            border-color: #C9C9CF">
            <tr>
                <td class="tdh">
                    Logistic ID :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblLpoCode" runat="server"></asp:Label>
                </td>
                <td style="width: 35%; text-align: right; padding-right: 15px">
                    <a id="imgbtnPurchaserRemark" runat="server" href="#">View Remarks </a>
                </td>
                <td style="width: 25%; text-align: right">
                    <asp:Button ID="btnDeleteLO" runat="server" ForeColor="Maroon" Text="Delete this Logistic PO"
                        OnClientClick="showModal('dvDeleteLPO');return false;" />
                </td>
                <td style="width: 25%; text-align: right">
                    <asp:Button ID="btnShowCnacelLPO" runat="server" Text="Cancel LPO" ForeColor="Maroon"
                        OnClientClick="showModal('dvCancellpo');return false;" />
                </td>
            </tr>
        </table>
        <table border="1" width="100%" style="border-top: 1px solid #C9C9CF; border-bottom: 1px solid #C9C9CF;
            padding-bottom: 5px; padding-top: 5px; border-collapse: collapse; border-color: #C9C9CF;">
            <tr>
                <td class="tdh">
                    PO Type :
                </td>
                <td class="tdd" colspan="3">
                    <asp:RadioButtonList ID="rbtnlistPOType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Consolidated PO" Value="CPO"></asp:ListItem>
                        <asp:ListItem Text="Single PO" Value="SPO"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="tdh">
                    Cost Type :
                </td>
                <td class="tdd">
                    <asp:RadioButtonList ID="rbtnlistCostType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Actual Cost" Value="ACT"></asp:ListItem>
                        <asp:ListItem Text="Estimated" Value="ECT"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="tdh">
                    Hub :
                </td>
                <td class="tdd">
                    <asp:DropDownList ID="ddlHub" Width="200px" runat="server" AutoPostBack="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdh">
                    Supplier :
                </td>
                <td class="tdd">
                    <asp:UpdatePanel ID="updsupplier" runat="server">
                        <ContentTemplate>
                            <auc:CustomAsyncDropDownList ID="uc_SupplierList1" runat="server" Width="160" Height="200" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="tdh">
                    Currency :
                </td>
                <td class="tdd">
                    <asp:UpdatePanel ID="updDDlCurrency" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="DDLCurrency" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="DDLCurrency_SelectedIndexChanged" ValidationGroup="ddlcurr">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDDLCurrency" Display="Dynamic"
                                ValidationGroup="vldamt" runat="server" ControlToValidate="DDLCurrency" ErrorMessage="Please select currency ! "
                                InitialValue="0"></asp:RequiredFieldValidator>
                            <asp:HiddenField ID="hdfExchrate" Value="1" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="tdh">
                    Port :
                </td>
                <td class="tdd">
                    <auc:CustomAsyncDropDownList ID="ctlPortList1" runat="server" Width="160" Height="200" />
                </td>
                <td class="tdh">
                    Agent/Forwd :
                </td>
                <td class="tdd">
                    <asp:DropDownList ID="ddlAgentFord" Width="200px" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table width="100%" style="border-collapse: collapse; border-color: #C9C9CF" border="1">
            <tr>
                <td style="padding: 5px 0px 5px 0px">
                    <asp:DataList ID="dlReqsnPOs" runat="server" BackColor="Transparent" ForeColor="Black"
                        RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Table">
                        <HeaderStyle ForeColor="Black" Font-Size="11px" Font-Names="verdana" Font-Bold="true" />
                        <HeaderTemplate>
                            Reqsn PO List :
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a href="POPreview.aspx?RFQCODE=<%# Eval("REQUISITION_CODE") +"&Vessel_Code="+ Eval("Vessel_CODE")+"&Order_Code="+ Eval("ORDER_CODE") %> "
                                target="_blank">
                                <%# DataBinder.Eval(Container.DataItem, "ORDER_CODE")%></a>
                            <asp:ImageButton ID="imgbtnDelete" Width="12" Height="12" runat="server" AlternateText="Delete"
                                Visible='<%# objUA.Delete!=0?true:false %>' OnClientClick="javascript:var con=confirm('Are you sure to delete this PO ?'); if(con)return true;else return false;"
                                OnClick="imgbtnDelete_Click" ImageUrl="~/Images/Delete.png" CommandArgument='<%#Eval("ID")%>' />
                        </ItemTemplate>
                        <SeparatorTemplate>
                            &nbsp;&nbsp; &nbsp;
                        </SeparatorTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td style="border-top: 1px solid #C9C9CF; border-bottom: 1px solid #C9C9CF; padding-bottom: 5px;
                    padding-top: 5px; width: 100%">
                    <asp:UpdatePanel ID="upditemlist" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found !"
                                Width="885px" EmptyDataRowStyle-ForeColor="Maroon" CellSpacing="1" BackColor="Gray"
                                CellPadding="1" GridLines="None" ShowFooter="true" RowStyle-VerticalAlign="Top"
                                RowStyle-HorizontalAlign="Left" OnRowDataBound="gvItemList_RowDataBound">
                                <HeaderStyle BackColor="Wheat" ForeColor="Black" Font-Size="12px" Font-Names="verdana"
                                    Font-Bold="true" />
                                <RowStyle BackColor="WhiteSmoke" Font-Size="11px" Font-Names="verdana" />
                                <FooterStyle BackColor="Wheat" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlpovessels" DataTextField="vessel_name" DataValueField="vessel_id"
                                                AppendDataBoundItems="true" DataSourceID="objvesselpo" runat="server">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <br />
                                            <a href="LOG_PO_Preview.aspx?ORDER_CODE=<%# Eval("Item_PO")%>&LOG_ID=<%# Request.QueryString["LOG_ID"] %>"
                                                target="_blank">
                                                <%# Eval("Item_PO")%>
                                            </a>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlpovessels" ValidationGroup="vldamt"
                                                InitialValue="0" runat="server" ControlToValidate="ddlpovessels" Display="Dynamic"
                                                ErrorMessage="Please select vessel !"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btnAddNewItem" Text="Add new item" BackColor="#0066cc" BorderStyle="None"
                                                ForeColor="WhiteSmoke" runat="server" OnClick="btnAddNewItem_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Description">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdfID" runat="server" Value='<%#Eval("item_ID")%>' />
                                            <asp:TextBox ID="txtItem" runat="server" Height="100px" TextMode="MultiLine" Width="400px"
                                                onkeypress="return textBoxCheckMaxLength(this,1000);" Text='<%#Eval("item") %>'></asp:TextBox>
                                            <br />
                                            <asp:Label ID="lblitemmaxlength" runat="server" Font-Size="7" ForeColor="#BA00BA"
                                                Font-Names="verdana" Text="Note: maximum length of item description is 1000 characters. "></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtItem" Display="Dynamic"
                                                ValidationGroup="vldamt" runat="server" ControlToValidate="txtItem" ErrorMessage="Please enter Item details ! "></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-VerticalAlign="Middle">
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAmount" runat="server" Width="120px" Style="text-align: right"
                                                Height="40px" onchange="checkNumber_local(this);CalculateTotal()" Text='<%#Eval("amount") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtAmount" Display="Dynamic"
                                                ValidationGroup="vldamt" runat="server" ControlToValidate="txtAmount" ErrorMessage="Please enter amount ! "></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalAmount" runat="server" Font-Bold="true" Font-Size="11px" ForeColor="Blue"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemark" runat="server" Width="200px" TextMode="MultiLine" Height="40px"
                                                onkeypress="return textBoxCheckMaxLength(this,950);" Text='<%#Eval("remark") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalAmount_USD" runat="server" Font-Bold="true" Font-Size="11px"
                                                ForeColor="Blue"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDeleteitem" Width="12" Height="12" runat="server" AlternateText="Delete"
                                                Visible='<%# objUA.Delete!=0?true:false %>' OnClientClick="javascript:var con=confirm('Are you sure to delete this item ?'); if(con)return true;else return false;"
                                                OnClick="imgbtnDeleteitem_Click" ImageUrl="~/Images/Delete.png" CommandArgument='<%#Eval("Item_ID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="objvesselpo" runat="server" TypeName="SMS.Business.PURC.BLL_PURC_LOG"
                                SelectMethod="Get_VesselInLogisticPO">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="LOG_ID" QueryStringField="LOG_ID" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:HiddenField ID="hdf_TotalAmount_USD" Value="0" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr style="border-top: 1px solid #C9C9CF; border-bottom: 1px solid #C9C9CF; padding-bottom: 5px;
                padding-top: 5px; width: 100%">
                <td style="text-align: center; padding: 10px">
                    <asp:Button ID="btnSavePODetails" Text="Save PO Details" runat="server" Height="35px"
                        ValidationGroup="vldamt" Font-Names="verdana" OnClick="btnSavePODetails_Click" />
                    &nbsp;
                    <asp:Button ID="btnSendForApproval" Text="Send For Approval" runat="server" Height="35px"
                        Font-Names="verdana" OnClientClick="if (!Page_ClientValidate('vldamt')) return false; showModal('divSendForApproval'); return false;" />
                    <br />
                    <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="11px"></asp:Label>
                    <asp:Panel ID="pnlApprove" runat="server" Width="100%">
                        <table>
                            <tr>
                                <td class="tdh">
                                    Remark :
                                </td>
                                <td class="tdd">
                                    <asp:TextBox ID="txtApproverRemark" runat="server" Width="250px" TextMode="MultiLine"
                                        Height="35px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtApproverRemark" ControlToValidate="txtApproverRemark"
                                        ValidationGroup="apprrmk" runat="server" ErrorMessage="Please enter Remark !"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td class="tdd">
                                    <asp:Button ID="btnApprove" Text="Approve" Font-Names="verdana" Height="35px" runat="server"
                                        ValidationGroup="apprrmk" Width="80px" OnClick="btnApprove_Click" />
                                </td>
                                <td class="tdd">
                                    <asp:Button ID="btnReworkToPurchaser" Text="Rework to Purchaser" Font-Names="verdana"
                                        ValidationGroup="apprrmk" Height="35px" runat="server" OnClick="btnReworkToPurchaser_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:UpdatePanel ID="updAttach" runat="server" RenderMode="Block">
                        <ContentTemplate>
                            <table width="100%" style="text-align: left">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnLoadFiles" OnClick="btnLoadFiles_Click" runat="server" />
                                        <asp:ImageButton ID="imgAttach" runat="server" ImageUrl="../Images/AddAttachment.png" OnClientClick="showModal('dvPopupAddAttachment',true,fn_OnClose);" />
                                                                              
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvAttachment" AutoGenerateColumns="false" runat="server" OnDataBound="gvAttachment_DataBound"
                                            ShowHeader="false" DataKeyNames="id" GridLines="None" CellPadding="3">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnDelete" ImageUrl="~/Purchase/Image/Close.gif" runat="server"
                                                            ImageAlign="Bottom" Visible="false" OnClick="imgbtnDeleteAttachment_Click" CommandArgument='<%#Eval("id")+","+Eval("File_Path") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkAtt" Target="_blank" runat="server" NavigateUrl='<%# Eval("File_Path")%>'
                                                            Text='<%# Eval("File_Name")%>'>
                                                        </asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lbllblexpand" Font-Bold="true" ForeColor="IndianRed" runat="server"></asp:Label>
                    <tlk4:CollapsiblePanelExtender ID="CollapsiblePanelpnldeletedpo" TargetControlID="pnldeletedpo"
                        CollapsedSize="0" Collapsed="true" AutoCollapse="False" AutoExpand="False" CollapseControlID="lbllblexpand"
                        ExpandControlID="lbllblexpand" CollapsedText="Click to view deleted PO(s) ..."
                        ExpandedText="Hide Deleted PO(s) Details" TextLabelID="lbllblexpand" ExpandDirection="Vertical"
                        runat="server">
                    </tlk4:CollapsiblePanelExtender>
                    <asp:Panel ID="pnldeletedpo" runat="server" ScrollBars="Vertical" Height="90px">
                        <asp:GridView ID="gvDeletedPOs" AutoGenerateColumns="false" runat="server" RowStyle-Wrap="true"
                            Width="99%" GridLines="None" CellPadding="2">
                            <Columns>
                                <asp:BoundField DataField="ORDER_CODE" HeaderText="Order Code" ItemStyle-Width="170px" />
                                <asp:BoundField DataField="ITEM_SHORT_DESC" HeaderText="Item" />
                                <asp:BoundField DataField="Currency" HeaderText="Currency" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="ORDER_RATE" HeaderText="Amount" ItemStyle-Width="100px" />
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <div id="divSendForApproval" title="Send for Approval" style="width: 500px; display: none">
            <table cellpadding="2" width="500px" cellspacing="0">
                <tr>
                    <td style="width: 23%; text-align: right; font-size: 11px; font-weight: bold; border-top: 1px solid gray;
                        padding-top: 8px; font-family: Verdana">
                        Select Approver :
                    </td>
                    <td style="text-align: left; width: 77%; border-top: 1px solid gray; padding-top: 8px">
                        <asp:ListBox ID="lstUserList" runat="server" DataTextField="UserName" DataValueField="UserID"
                            Height="150px" ValidationGroup="apr" Width="99%" Font-Size="12px" Font-Names="verdana">
                        </asp:ListBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="apr"
                            runat="server" ControlToValidate="lstUserList" InitialValue="0" ErrorMessage="Please select approver !"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 23%; text-align: right; font-size: 11px; font-family: Verdana;
                        font-weight: bold">
                        Remark :
                    </td>
                    <td style="width: 77%">
                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="99%" Height="60px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; padding-top: 5px">
                        <asp:Button ID="Button1" runat="server" Height="30px" Text="Send For Approval" ValidationGroup="apr"
                            OnClick="btnSendForApproval_Click" />
                        <asp:Button ID="btnSendForApprovalCancel" Height="30px" runat="server" oclick="hideModal('divSendForApproval');return false;"
                            Text="Close" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvDeleteLPO" style="background-color: #F0FFFF; width: 320px; height: auto;
            display: none; border: 2px solid gray">
            <table width="99%">
                <tr>
                    <td class="tdh">
                        Remark :
                    </td>
                    <td class="tdd">
                        <asp:TextBox ID="txtremarkDeleteLPO" runat="server" Width="220px" TextMode="MultiLine"
                            Height="70px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDeleteLPO" Display="Dynamic"
                            ValidationGroup="DLPO" runat="server" ControlToValidate="txtremarkDeleteLPO"
                            ErrorMessage="Please enter remark !"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnDeleteLPO" runat="server" Text="Delete" ValidationGroup="DLPO"
                            OnClick="btnDeleteLPO_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvCancellpo" style="background-color: #F0FFFF; width: 320px; height: auto;
            display: none; border: 2px solid gray">
            <table width="99%">
                <tr>
                    <td class="tdh">
                        Remark :
                    </td>
                    <td class="tdd">
                        <asp:TextBox ID="txtCancelLPORemark" runat="server" Width="220px" TextMode="MultiLine"
                            Height="70px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtCancelLPORemark" Display="Dynamic" ValidationGroup="CLPO"
                            runat="server" ControlToValidate="txtCancelLPORemark" ErrorMessage="Please enter remark !"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnCancelLPO" runat="server" Text="Cancel LPO" OnClick="btnCnacelLPO_Click"
                            ValidationGroup="CLPO" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 1px solid gray;
            background-color: #FDFDFD">
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
    </div>
    </form>
</body>
</html>
