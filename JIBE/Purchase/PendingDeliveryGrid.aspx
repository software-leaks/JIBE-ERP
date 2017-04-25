<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PendingDeliveryGrid.aspx.cs"
    Inherits="Technical_INV_PendingDeliveryGrid" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControl/ucReqsncancelLog.ascx" TagName="ucReqsncancelLog"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucPurcReqsnHold_UnHold.ascx" TagName="ucPurcReqsnHold_UnHold"
    TagPrefix="Hold" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucPurc_Rollback_Reqsn.ascx" TagName="ucPurc_Rollback_Reqsn"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
     <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Purc_Get_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Get_Remarks_All.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Ins_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ml
        {
            margin-left: 80px;
        }
        .mr
        {
            margin-right: 80px;
        }
        body 
        {
            background-color: White;
        } 
    </style>
    <script language="javascript" type="text/jscript">

        function CloseDiv() {
            var control = document.getElementById("divOnHold");
            control.style.visibility = "hidden";
        }

        function CloseDiv2() {
            var control = document.getElementById("divReqStages");
            control.style.visibility = "hidden";
            var control1 = document.getElementById("txtReason");
            control1.value = "";
        }

        function OnCancelConfirmation() {
            var bln = confirm('Do you want to cancel PO')
            return bln;

        }
        function OnCancelSupplierConfirmation() {
            var Result = confirm("Please Confirm to supplier this order will be Canceled or Not. If You have Confirm Then Continue...?")
            return Result;


        }

        function CheckFileAndOpen(path) {

            if (path != "") {

                window.open(path);
                return false;
            }
        }
        var ReqCode = "", vesselcode = "", ordercode = "", Document_Code = "";
        $(document).ready(function () {
            $("body").on("click", ".btnCancelPO_Click", function () {
                ReqCode = "&RFQCODE=" + $(this).attr("reqcode");
                vesselcode = "&Vessel_Code=" + $(this).attr("vesselcode");
                ordercode = "&ORDER_CODE=" + $(this).attr("ordercode");
                Document_Code = '&Document_Code=' + $(this).attr("Document_Code");
            });
        });


        function GeneratePDf() {
            $("#Iframe").attr("src", $("#hdnHost").val() + 'purchase/CancelPOPreview.aspx?' + ReqCode + vesselcode + ordercode + Document_Code);
        }
    </script>
</head>
<body style="padding: 0px; margin: 0px;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center">
                        <telerik:RadGrid ID="rgdPending" runat="server" Width="100%" ShowStatusBar="True"
                            AlternatingItemStyle-BackColor="#CEE3F6" Skin="Office2007" AutoGenerateColumns="False"
                            AllowSorting="True" OnNeedDataSource="RadGrid1_NeedDataSource" GridLines="None"
                            OnItemDataBound="rgdPending_ItemDataBound" OnDataBound="rgdPending_DataBound">
                            <PagerStyle Mode="NextPrevAndNumeric" CssClass="Pager" Font-Size="12px" Font-Names="verdana"
                                HorizontalAlign="Right"></PagerStyle>
                            <MasterTableView DataKeyNames="REQUISITION_CODE,document_code,Vessel_Code" AllowMultiColumnSorting="True"
                                Width="100%" AllowPaging="false">
                                <Columns>
                                    <telerik:GridTemplateColumn SortExpression="Vessel_name" HeaderText="Vessel" DataField="Vessel_name"
                                        UniqueName="Vessel_name">
                                        <ItemTemplate>
                                            <a href='<%# "../crew/CrewList_Print.aspx?vid="+Eval("Vessel_Code").ToString() %>'
                                                target="_blank">
                                                <%#Eval("Vessel_name")%></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn SortExpression="Name_Dept" HeaderText="Department" DataField="Name_Dept"
                                        AllowSorting="true" UniqueName="Name_Dept">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Requisition Number" UniqueName="REQUISITION_CODE"
                                        SortExpression="REQUISITION_CODE">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgPriority" runat="server" ToolTip="Urgent" ImageUrl="~/Images/exclamation.gif"
                                                Height="12px"></asp:ImageButton>
                                            <a href="RequisitionSummary.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("Document_CODE") +"&Vessel_Code="+Eval("Vessel_Code")%>"
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "REQUISITION_CODE")%></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Order No." SortExpression="ORDER_CODE">
                                        <ItemTemplate>
                                            <a href="POPreview.aspx?RFQCODE=<%# Eval("REQUISITION_CODE") +"&Vessel_Code="+ Eval("Vessel_CODE")+"&Order_Code="+ Eval("ORDER_CODE") %> "
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "ORDER_CODE")%></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Supplier">
                                        <ItemTemplate>
                                            <a href="ViewSupplierDetails.aspx?SupplierCode=<%# Eval("SUPPLIER") %> " target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "SHORT_NAME")%></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn SortExpression="requestion_Date" HeaderText="Sent Date"
                                        AllowSorting="true" DataField="requestion_Date" UniqueName="requestion_Date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="SYSTEM_Description" SortExpression="SYSTEM_Description"
                                        DataField="SYSTEM_Description" HeaderText="Catalogue Name">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="TOTAL_ITEMS" HeaderText="Items" DataField="TOTAL_ITEMS"
                                        AllowSorting="true" UniqueName="TOTAL_ITEMS">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="Server" Text='<%# Eval("DeliveryStage") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Delivery Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveryType" runat="Server" ForeColor='<%# Convert.ToString(Eval("DeliveryType"))!="Full"?System.Drawing.Color.Red:System.Drawing.Color.Transparent %>'
                                                Text='<%# Eval("DeliveryType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn SortExpression="URGENCY_CODE" HeaderText="Urgency" DataField="URGENCY_CODE"
                                        AllowSorting="false" UniqueName="URGENCY_CODE" Visible="false">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="REQUISITION_CODE" Visible="false" HeaderText="Requisition"
                                        DataField="REQUISITION_CODE" UniqueName="REQUISITION_CODE">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="Attach_Status" Visible="false" HeaderText="Attach_Status"
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
                                    <telerik:GridBoundColumn SortExpression="ORDER_CODE" HeaderText="Order No." Visible="false"
                                        DataField="ORDER_CODE" UniqueName="ORDER_CODE">
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
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Actions">
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td align="center" style="width: 25%">
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
                                            <table cellpadding="2" cellspacing="0">
                                                <tr>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgSelect" runat="server" Text="Select" OnCommand="onSelect" 
                                                            CommandName="Select" CommandArgument='<%#Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")+"&Dept_Code="+Eval("code")+"&"+Eval("OnHold")+"&intCatalog="+Eval("SYSTEM_CODE")%>'
                                                            ForeColor="Black" ToolTip="Update Delivery" onmouseover="DisplayActionInHeader('Update Delivery' ,'rgdPending')"
                                                            ImageUrl="~/purchase/Image/view.gif" Visible='<%# objUA.Edit != 0?true:false  %>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <a style="border: 0px solid white" href='<%#"Quotation_Evaluation_Report.aspx?Requisitioncode=" + Eval("REQUISITION_CODE").ToString() + "&Document_Code=" + Eval("document_code").ToString() + "&Vessel_Code=" + Eval("Vessel_Code").ToString() %>'
                                                            target="_blank">
                                                            <img src="../Images/compare.gif" style="border: 0px solid white; height: 18px; width: 18px"
                                                                title="View Evaluation" onmouseover="DisplayActionInHeader('View Evalution' ,'rgdPending')" />
                                                        </a>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <a href="QuotationSummary.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("Document_Code") +"&Vessel_Code="+Eval("Vessel_Code")+"&QUOTATION_CODE=0"%>"
                                                            target="_blank" onmouseover="DisplayActionInHeader('View Quotations' ,'rgdDeliveryStatus')">
                                                            <img src="../Images/QtnsSummary.png" style="border: 0px" title="View Quotations" />
                                                        </a>
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <img id="imgbtnPurchaserRemark" src="../Images/remark_new.gif" onclick='<%# "GetRemarkAll("+Eval("document_code").ToString()+","+Session["userid"].ToString()+",null,event)" %>'
                                                            onmouseover='<%#"GetRemarkToolTip("+Eval("document_code").ToString() +",null,event);DisplayActionInHeader(\"All Remarks; Click to add new\",\"rgdPending\");" %>'
                                                            onmouseout="CloseRemarkToolTip();" title="All Remarks; Click to add new" />
                                                    </td>
                                                    <td style="border-color: transparent">
                                                       <asp:ImageButton ID="ImgAttachment" runat="server" Height="20px" Width="20px" ImageUrl="../Images/attachment.png"  style="border: 0px solid white"   
                                                       onclick='<%#"ShowReqsnAttachment(&#39;Requisition_code=" + Eval("REQUISITION_CODE").ToString()+ "&Vessel_ID=" + Eval("Vessel_Code").ToString()+"&#39;)"%>'>
                                                        </asp:ImageButton>
                                                        
                                                    </td>
                                                    <td style="border-color: transparent; display:none" >
                                                        <asp:ImageButton ID="ImgAddItm" runat="server" Text="Select" OnCommand="onAddNewItem"
                                                            CommandName="onAddNewItem" CommandArgument='<%#Eval("REQUISITION_CODE")+"&VCode="+Eval("Vessel_Code")+"&ReqStage=APR"%>'
                                                            ForeColor="Black" ToolTip="Add Items from Office side" ImageUrl="~/purchase/Image/briefcase-add.gif"
                                                            Width="16px" Height="16px" onmouseover="DisplayActionInHeader('Add Items from Office side' ,'rgdPending')"
                                                            Visible="false"></asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="BtnCancelReqStage" runat="server" Text="Select" OnCommand="OnCancelReq"
                                                            AlternateText='<%#Eval("ORDER_CODE")+"~"+Eval("SHORT_NAME")%>' CommandName="OnCancelReq"
                                                            CommandArgument='<%#Eval("REQUISITION_CODE")+","+Eval("document_code") +","+Eval("Vessel_Code")+","+Eval("code")+","+Eval("OnHold")+","+Eval("QUOTATION_CODE")%>'
                                                            ForeColor="Black" ToolTip="Rollback" ImageUrl="~/purchase/Image/Cancel_New.gif"
                                                            Width="16px" Height="16px" onmouseover="DisplayActionInHeader('Rollback' ,'rgdPending')"
                                                            Visible='<%# objUA.Edit != 0?true:false  %>'
                                                            CssClass="btnCancelPO_Click" Document_Code='<%#Eval("document_code")%>'
                                                            reqcode='<%#Eval("REQUISITION_CODE") %>' Quotation_code='<%#Eval("Quotation_code") %>'
                                                            vesselcode='<%#Eval("Vessel_Code") %>' ordercode='<%#Eval("ORDER_CODE") %>'

                                                            ></asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="btnOnHold" runat="server" Width="16px" Height="16px" Text="Hold"
                                                            ImageUrl="~/purchase/Image/OnHold.png" OnClick="btnOnHold_Click" OnCommand="OnHold"
                                                            CommandName="OnHold" CommandArgument='<%#Eval("REQUISITION_CODE")+","+Eval("document_code") +","+Eval("Vessel_Code")+","+Eval("code")+","+Eval("OnHold")%>'
                                                            onmouseover="DisplayActionInHeader('Put on Hold' ,'rgdPending')" Visible='<%# objUA.Edit != 0?true:false  %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings>
                                    <PopUpSettings ScrollBars="None"></PopUpSettings>
                                </EditFormSettings>
                            </MasterTableView>
                            <ClientSettings>
                                <Scrolling AllowScroll="false" UseStaticHeaders="false" />
                            </ClientSettings>
                        </telerik:RadGrid>
                        <uc1:ucCustomPager ID="ucCustomPagerDeliveryStatus" OnBindDataItem="BindRequisitionGrid"
                            PageSize="15" runat="server" />
                    </td>
                </tr>
            </table>
            <div id="divOnHold" style="border: 1px solid Black; position: absolute; left: 35%;
                top: 30%; z-index: 2; color: black;" runat="server">
                <Hold:ucPurcReqsnHold_UnHold ID="HoldUnHold" runat="server" OnCancelClick="btndivCancel_Click"
                    OnSaveClick="btndivSave_Click" />
            </div>
            <asp:HiddenField ID="HiddenArgument" runat="server" Value="" />
            <div id="divReqStages" style="border: 1px solid Black; position: absolute; left: 17%;
                top: 1%; z-index: 2; color: black; height: auto; width: 495px;" runat="server"
                class="popup-css">
                <uc2:ucPurc_Rollback_Reqsn ID="ucPurc_Rollback_Reqsn1" OnSave="btndivReqprioOK_Click"
                    runat="server" />
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
