<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PendingPOConfirmGrid.aspx.cs"
    Inherits="Technical_INV_PendingPOConfirmGrid" Title="PendingPOConfirm" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControl/ucReqsncancelLog.ascx" TagName="ucReqsncancelLog"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucPurcReqsnHold_UnHold.ascx" TagName="ucPurcReqsnHold_UnHold"
    TagPrefix="Hold" %>
<%@ Register Src="../UserControl/ucPurc_Rollback_Reqsn.ascx" TagName="ucPurc_Rollback_Reqsn"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Purc_Get_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Get_Remarks_All.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Ins_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <title>Pending PO-Confirm</title>
    <script language="javascript" type="text/javascript">

        function CloseDiv1() {
            var control = document.getElementById("divOnHold");
            control.style.visibility = "hidden";

        }

        function CloseDiv() {
            var control = document.getElementById("divReqStages");
            control.style.visibility = "hidden";



            var control1 = document.getElementById("txtReason");
            control1.value = "";
        }
        function CheckFileAndOpen(path) {

            if (path != "") {

                window.open(path);
                return false;
            }
        }
        var ReqCode = "", vesselcode = "", ordercode = "", Document_Code = "";
        $(document).ready(function () 
        {
            $("body").on("click", ".btnCancelPO_Click", function () {
                ReqCode = "&RFQCODE=" + $(this).attr("reqcode");
                vesselcode = "&Vessel_Code=" + $(this).attr("vesselcode");
                ordercode = "&ORDER_CODE=" + $(this).attr("ordercode");
                Document_Code = '&Document_Code=' + $(this).attr("Document_Code");


            });
        });


        function GeneratePDf() {
            $("#Iframe").attr("src", $("#hdnHost").val()+'purchase/CancelPOPreview.aspx?' + ReqCode + vesselcode + ordercode + Document_Code);
        }
    </script>
    <style type="text/css">
        body
        {
            background-color: White;
        }
    </style>
</head>
<body style="padding: 0px; margin: 0px;">
    <form id="form1" runat="server">
    <asp:HiddenField  ID="hdnHost" runat="server" ClientIDMode="Static"/>    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center">
                        <telerik:RadGrid ID="rgdPOConfirm" runat="server" Width="100%" HeaderStyle-Height="30px"
                            ShowStatusBar="True" HeaderStyle-HorizontalAlign="Center" AlternatingItemStyle-BackColor="#CEE3F6"
                            Skin="Office2007" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false"
                            GridLines="None" OnItemDataBound="rgdPOConfirm_ItemDataBound" OnDataBound="rgdPOConfirm_DataBound"
                            OnSortCommand="rgdPOConfirm_SortCommand" OnNeedDataSource="rgd_NeedDataSource">
                            <PagerStyle Mode="NextPrevAndNumeric" CssClass="Pager" Font-Size="12px" Font-Names="verdana"
                                HorizontalAlign="Right"></PagerStyle>
                            <MasterTableView DataKeyNames="REQUISITION_CODE,document_code,Vessel_Code" AllowMultiColumnSorting="True"
                                Width="100%" AllowPaging="false">
                                <Columns>
                                    <telerik:GridTemplateColumn SortExpression="Vessel_name" HeaderText="Vessel" DataField="Vessel_name"
                                        UniqueName="Vessel_name">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <a href='<%# "../crew/CrewList_Print.aspx?vid="+Eval("Vessel_Code").ToString() %>'
                                                target="_blank">
                                                <%#Eval("Vessel_name")%></a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Requisition Number" SortExpression="REQUISITION_CODE">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgPriority" runat="server"
                                                Height="12px"></asp:ImageButton>
                                                 <asp:HyperLink ID="hlinkReq" runat="server" Target="_blank"
                                            NavigateUrl='<%#String.Format("RequisitionSummary.aspx?REQUISITION_CODE={0}&Document_Code={1}&Vessel_Code={2}&Dept_Code={3}&hold={4}", Eval("REQUISITION_CODE"), Eval("document_code"),Eval("Vessel_Code"),Eval("code"),Eval("OnHold"))%>'
                                            Text='<%#Eval("REQUISITION_CODE") %>'> </asp:HyperLink>
                                            <asp:Label ID="lblCriticalFlag" runat="server" Text='<%#Eval("Critical_Flag") %>'  style="display:none"></asp:Label>
                                          <%--  <a href="RequisitionSummary.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("Document_CODE") +"&Vessel_Code="+Eval("Vessel_Code")%>"
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "REQUISITION_CODE")%></a>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </telerik:GridTemplateColumn>
                                       <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Po Number" SortExpression="ORDER_CODE">
                                        <ItemTemplate>
                                            <a href="POPreview.aspx?RFQCODE=<%# Eval("REQUISITION_CODE") +"&Vessel_Code="+ Eval("Vessel_CODE")+"&Order_Code="+ Eval("ORDER_CODE") %> "
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "ORDER_CODE")%></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn SortExpression="Name_Dept" HeaderText="Department/Function" DataField="Name_Dept"
                                        AllowSorting="true" UniqueName="Name_Dept">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="SUPPLIER" HeaderText="Supp Code" AllowSorting="true"
                                        DataField="SUPPLIER" UniqueName="SupplierCode" Display="false">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn UniqueName="SYSTEM_Description" SortExpression="SYSTEM_Description"
                                        DataField="SYSTEM_Description" HeaderText="Catalogue /System">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Supplier">
                                        <ItemTemplate>
                                            <a href="ViewSupplierDetails.aspx?SupplierCode=<%# Eval("SUPPLIER") %> " target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "SHORT_NAME")%></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn SortExpression="Document_Date" HeaderText="PO Issue Date" AllowSorting="true"
                                        DataField="requestion_Date" UniqueName="requestion_Date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="TOTAL_ITEMS" HeaderText="Items" DataField="TOTAL_ITEMS"
                                        AllowSorting="true" UniqueName="TOTAL_ITEMS">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="Lead_Time" HeaderText="Lead Time(In days)"
                                        DataField="Lead_Time" AllowSorting="false" UniqueName="Lead_Time" Visible="false">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="URGENCY_CODE" HeaderText="Urgency" DataField="URGENCY_CODE"
                                        AllowSorting="false" UniqueName="URGENCY_CODE" Visible="false">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Order Raise" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgSelect" runat="server" Text="Select" OnCommand="onSelect"
                                                CommandName="Select" CommandArgument='<%#Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")%>'
                                                ForeColor="Black" ToolTip="View for next process to selected requistion" ImageUrl="~/purchase/Image/view.gif">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Send PO" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgSendPO" runat="server" Text="Send PO" OnCommand="onSendPO"
                                                CommandName="onSendPO" CommandArgument='<%#Eval("REQUISITION_CODE")+"," +Eval("document_code")+ "," + Eval("Vessel_Code")%>'
                                                ForeColor="Black" ImageUrl="~/purchase/Image/HandleHand.png" Width="12px" Height="12px">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn SortExpression="Attach_Status" Visible="False" HeaderText="Attach_Status"
                                        DataField="Attach_Status" UniqueName="Attach_Status">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="document_code" Visible="false" HeaderText="Document Code"
                                        DataField="document_code" UniqueName="document_code">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="Vessel_Code" Visible="false" HeaderText="Vessel_Code"
                                        DataField="Vessel_Code" UniqueName="Vessel_Code">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="code" Visible="false" HeaderText="code"
                                        DataField="code" UniqueName="code">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="OnHold" Visible="false" HeaderText="OnHold"
                                        DataField="OnHold" UniqueName="OnHold">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Actions">
                                        <HeaderTemplate>
                                            <headerstyle width="200px" horizontalalign="Left" />
                                            <table width="200px">
                                                <tr>
                                                    <td align="center" style="width: 15%">
                                                        Actions
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <span id="lblActionDisplayText" style="height: 15px; width: 200px; color: Red"></span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="btnConfirm" runat="server" AlternateText="Confirm" ImageUrl="~/Purchase/Image/ApprovePO.gif"
                                                            OnClientClick="return confirm('Are you sure,you want to Confirm this PO?')" OnCommand="OnConfirm"
                                                            CommandName="OnConfirm" CommandArgument='<%#Eval("REQUISITION_CODE")+","+Eval("document_code") +","+Eval("Vessel_Code")+","+Eval("supplier")+","+Eval("Order_Code")+","+Eval("QUOTATION_CODE")%>'
                                                            onmouseover="DisplayActionInHeader('Confirm PO' ,'rgdPOConfirm')" Visible='<%# objUA.Edit != 0?true:false  %>' />
                                                    </td>
                                                    <td>
                                                        <a style="border: 0px solid white" href='<%#"QuotationEvalRpt.aspx?Requisitioncode=" + Eval("REQUISITION_CODE").ToString() + "&Document_Code=" + Eval("document_code").ToString() + "&Vessel_Code=" + Eval("Vessel_Code").ToString() %>'
                                                            target="_blank">
                                                            <%-- <a style="border: 0px solid white" href='<%#"Quotation_Evaluation_Report.aspx?Requisitioncode=" + Eval("REQUISITION_CODE").ToString() + "&Document_Code=" + Eval("document_code").ToString() + "&Vessel_Code=" + Eval("Vessel_Code").ToString() %>'
                                                            target="_blank">--%>
                                                            <img src="../Images/compare.gif" style="border: 0px solid white; height: 17px; width: 17px"
                                                                alt="View Evaluation" onmouseover="DisplayActionInHeader('View Evaluation' ,'rgdPOConfirm')" />
                                                        </a>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <a href="QuotationSummary.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("Document_Code") +"&Vessel_Code="+Eval("Vessel_Code")+"&QUOTATION_CODE=0"%>"
                                                            target="_blank" onmouseover="DisplayActionInHeader('View Quotations' ,'rgdDeliveryStatus')">
                                                            <img src="../Images/QtnsSummary.png" style="border: 0px" title="View Quotations" />
                                                        </a>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgAttachment" Height="20px" Width="20px" ImageUrl="../Images/attachment.png"
                                                            runat="server" Style="border: 0px solid white" OnClick='<%#"ShowReqsnAttachment(&#39;Requisition_code=" + Eval("REQUISITION_CODE").ToString()+ "&Vessel_ID=" + Eval("Vessel_Code").ToString()+"&#39;)"%>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <img id="imgbtnPurchaserRemark" src="../Images/remark_new.gif" onclick='<%# "GetRemarkAll("+Eval("document_code").ToString()+","+Session["userid"].ToString()+",null,event)" %>'
                                                            onmouseover='<%#"GetRemarkToolTip("+Eval("document_code").ToString() +",null,event);DisplayActionInHeader(\"All Remarks; Click to add new\",\"rgdPOConfirm\");" %>'
                                                            onmouseout="CloseRemarkToolTip();" title="All Remarks; Click to add new" />
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgbtnReSendPO" runat="server" AlternateText="ReSend PO" ImageUrl="~/Images/ResendPo.png"
                                                            OnCommand="ImgbtnReSendPO_Click" CommandArgument='<%#Eval("REQUISITION_CODE")+","+Eval("document_code") +","+Eval("Vessel_Code")+","+Eval("supplier")+","+Eval("Order_Code")+","+Eval("QUOTATION_CODE")%>'
                                                            onmouseover="DisplayActionInHeader('Resend PO to supplier' ,'rgdPOConfirm')"
                                                            Visible='<%# objUA.Edit != 0?true:false  %>' />
                                                    </td>
                                                    <td style="border-color: transparent; display: none; visibility: hidden">
                                                        <asp:ImageButton ID="ImgAddItm" runat="server" Text="Select" OnCommand="onAddNewItem"
                                                            CommandName="onAddNewItem" CommandArgument='<%#Eval("REQUISITION_CODE")+"&VCode="+Eval("Vessel_Code")+"&ReqStage=ORD"+"&sOrderCode="+Eval("Order_Code")%>'
                                                            ForeColor="Black" ToolTip="Add Items from Office side" ImageUrl="~/purchase/Image/briefcase-add.gif"
                                                            Width="16px" Height="16px" onmouseover="DisplayActionInHeader('Add Items from Office side' ,'rgdPOConfirm')"
                                                            Visible="false"></asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="BtnCancelReqStage" runat="server" AlternateText='<%#Eval("ORDER_CODE")+"~"+Eval("SHORT_NAME")%>'
                                                            OnCommand="OnCancelReq" CommandName="OnCancelReq" CommandArgument='<%#Eval("REQUISITION_CODE")+","+Eval("document_code") +","+Eval("Vessel_Code")+","+Eval("code")+","+Eval("OnHold")+","+Eval("QUOTATION_CODE")%>'
                                                            ForeColor="Black" ToolTip="Rollback"  ImageUrl="~/purchase/Image/Cancel_New.gif"
                                                            Width="16px" Height="16px" onmouseover="DisplayActionInHeader('Rollback' ,'rgdPOConfirm')"
                                                            Visible='<%# objUA.Edit != 0?true:false  %>'
                                                            CssClass="btnCancelPO_Click"
                                                            Document_Code='<%#Eval("document_code")%>'
                                                            reqcode='<%#Eval("REQUISITION_CODE") %>' Quotation_code='<%#Eval("Quotation_code") %>'
                                                            vesselcode='<%#Eval("Vessel_Code") %>' ordercode='<%#Eval("ORDER_CODE") %>'
                                                         ></asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="btnCancelPO" runat="server" AlternateText="Cancel PO" ImageUrl="~/Purchase/Image/Close.gif"
                                                            OnClick="btnCancelPO_Click"  CssClass="btnCancelPO_Click" CommandArgument='<%#Eval("Order_Code")+","+Eval("OnHold")%>'
                                                            ToolTip='Cancel PO' onmouseover="DisplayActionInHeader('Cancel PO' ,'rgdPOConfirm')"
                                                            Visible='<%# objUA.Edit != 0?true:false  %>' Document_Code='<%#Eval("document_code")%>'
                                                            reqcode='<%#Eval("REQUISITION_CODE") %>' Quotation_code='<%#Eval("Quotation_code") %>'
                                                            vesselcode='<%#Eval("Vessel_Code") %>' ordercode='<%#Eval("ORDER_CODE") %>' />
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="btnOnHold" runat="server" ImageUrl="~/purchase/Image/OnHold.png"
                                                            Width="16px" Height="16px" Text="Hold" OnClick="btnOnHold_Click" OnCommand="OnHold"
                                                            CommandName="OnHold" onmouseover="DisplayActionInHeader('Hold/UnHold' ,'rgdPOConfirm')"
                                                            CommandArgument='<%#Eval("REQUISITION_CODE")+","+Eval("document_code") +","+Eval("Vessel_Code")+","+Eval("code")+","+Eval("OnHold")%>'
                                                            Visible='<%# objUA.Edit != 0?true:false  %>' />
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="btnCancelPOItems" runat="server" AlternateText="Cancel item(s)"
                                                            CssClass="btnCancelPO_Click"
                                                            Document_Code='<%#Eval("document_code")%>'
                                                            reqcode='<%#Eval("REQUISITION_CODE") %>' Quotation_code='<%#Eval("Quotation_code") %>'
                                                            vesselcode='<%#Eval("Vessel_Code") %>' ordercode='<%#Eval("ORDER_CODE") %>'
                                                            ImageUrl="~/Purchase/Image/list_delete.png" OnClick="btnCancelPOItems_Click"
                                                            CommandArgument='<%#Eval("Order_Code")%>' ToolTip='Cancel Items in PO' onmouseover="DisplayActionInHeader('Cancel Items in PO' ,'rgdPOConfirm')"
                                                            Visible='<%# objUA.Edit != 0?true:false  %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings>
                                    <PopUpSettings ScrollBars="None"></PopUpSettings>
                                </EditFormSettings>
                            </MasterTableView>
                            <ClientSettings>
                                <Scrolling AllowScroll="false" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindData" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HiddenArgument" runat="server" Value="" />
            <div id="divOnHold" style="border: 1px solid Black; position: absolute; left: 35%;
                top: 30%; z-index: 2; color: black;" runat="server">
                <Hold:ucPurcReqsnHold_UnHold ID="HoldUnHold" runat="server" OnCancelClick="btndivCancel_Click"
                    OnSaveClick="btndivSave_Click" />
            </div>
            <div id="divReqStages" style="border: 1px solid Black; position: absolute; left: 17%;
                top: 1%; z-index: 2; color: black; height: auto; width: 495px;" runat="server"
                class="popup-css">
                <uc2:ucPurc_Rollback_Reqsn ID="ucPurc_Rollback_Reqsn1" OnSave="btndivReqprioOK_Click"
                    runat="server" />
            </div>
            <div id="dvCancelPO" runat="server" style="border: 1px solid Black; position: absolute;
                left: 35%; top: 30%; z-index: 2; color: black;">
                <table class="popup-css">
                    <tr>
                        <td style="text-align: center; font-weight: bold; font-size: 12px" colspan="2">
                            Cancel PO(<asp:Label ID="lblCancelPO" ForeColor="Red" runat="server"></asp:Label>)
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; vertical-align: middle; font-size: 12px">
                            Remark:
                        </td>
                        <td style="text-align: center; vertical-align: top">
                            <asp:TextBox ID="txtRemarkPO" runat="server" Width="400px" Height="60px" TextMode="MultiLine"
                                ValidationGroup="remarkPO"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPO" runat="server" ValidationGroup="remarkPO"
                                Display="None" ControlToValidate="txtRemarkPO" ErrorMessage="Please enter remark "></asp:RequiredFieldValidator>
                            <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtenderPO" TargetControlID="RequiredFieldValidatorPO"
                                runat="server">
                            </tlk4:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="2">
                            <asp:Button ID="btnCancelPOSave" runat="server" ValidationGroup="remarkPO" OnClick="btnCancelPOSave_Click"
                                Text="Ok" Width="100px" Height="30px" ClientIDMode="Static" OnClientClick="javascript:return confirm('Are you sure, you want to Cancel PO?')" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnCancelPOCancel" runat="server" OnClick="btnCancelPOCancel_Click"
                                Text="Cancel" Width="100px" Height="30px" />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="HiddenOrderCode" runat="server" />
                <asp:HiddenField ID="hdfDocumentcode" runat="server" />
            </div>
            <div id="dvcancelPOItems" runat="server" style="border: 1px solid Black; position: absolute;
                max-height: 300px; width: 460px; overflow: auto; text-align: center; left: 35%;
                top: 25%; z-index: 2; color: black;" class="popup-css">
                <table width="100%">
                    <tr>
                        <td style="text-align: center; font-size: 12px; font-weight: bold" colspan="2">
                            Cancel PO Items(<asp:Label ID="lblCancelPOItems" ForeColor="Red" runat="server"></asp:Label>)
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="2">
                            <asp:GridView ID="gvPOItems" AutoGenerateColumns="false" EmptyDataText="no item found !"
                                Width="100%" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="11px"
                                        HeaderStyle-BackColor="Teal" HeaderStyle-Font-Size="12px" HeaderStyle-Font-Bold="true"
                                        HeaderStyle-ForeColor="White">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCencel" AutoPostBack="false" ForeColor="Black" Text='<%#Eval("itemname") %>'
                                                ValidationGroup="POItems" ToolTip='<%#Eval("itemid") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 12px">
                            Remark:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemarkPOItems" Width="380px" Height="50px" TextMode="MultiLine"
                                ValidationGroup="POItems" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnCancellPOItemsSave" runat="server" OnClientClick="javascript:return confirm('Are you sure, to cancel selected items?')"
                                ValidationGroup="POItems" OnClick="btnCancellPOItemsSave_Click" Text="Cancel the selected items"
                                Height="30px" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnCancelPOItemsCancel" runat="server" OnClick="btnCancelPOItemsCancel_Click"
                                Text="Cancel" Width="100px" Height="30px" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvPruchremarkMain" style="display: none; position: fixed; left: 30%; top: 10%;
        border: 1px solid gray; padding: 10px" class="popup-css">
        <table>
            <tr>
                <td>
                    <div id="dvShowPurchaserRemark" style="position: relative">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="font-size: 12px; font-family: Verdana; font-weight: bold">
                                Remark:
                            </td>
                            <td>
                                <textarea id="txtRemark" cols="40" rows="5" style="width: 490px; height: 60px"></textarea>
                                <%--<input id="txtRemark" type="text" style="width: 490px; height: 60px" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <input id="btnSaveRemark" onclick="SavePurcReamrk();" type="button" style="height: 30px;
                                    width: 80px" value="Save" />&nbsp;&nbsp
                                <input id="btnCancelRemark" onclick="CloseRemarkAll();" type="button" value="Close"
                                    style="height: 30px; width: 80px" value="Save" />
                                <input id="hdfUserID" type="hidden" />
                            </td>
                        </tr>
                    </table>
                    <input id="hdfDocumentCode" type="hidden" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPurchaseRemark" style="border: 1px solid gray; z-index: 501; color: Black;
        display: none; position: fixed; left: 400px; top: 100px" class="Tooltip-css">
    </div>
    <div style="display:none">
    <iframe id="Iframe" clientidmode="Static" runat="server"></iframe>
    </div>
    </form>
</body>
</html>
