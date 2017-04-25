<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ALLRequisitioinStatusGrid.aspx.cs"
    ValidateRequest="false" EnableEventValidation="false" Inherits="Technical_INV_ALLRequisitioinStatusGrid" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Purc_Get_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Get_Remarks_All.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Ins_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js?v=1" type="text/javascript"></script>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js?v=1" type="text/javascript"></script>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <title>ALL Requisition Status</title>
    <style type="text/css">
        div#divInfoQTN
        {
            width: 1210px;
            max-height: 400px;
            overflow: scroll;
            position: relative;
        }
        
        div#divInfoQTN th
        {
            top: expression(document.getElementById("divInfoQTN").scrollTop-1); /* left: expression(parentNode.parentNode.parentNode.parentNode.scrollLeft);*/
            position: relative;
        }
        
        .Pager a
        {
            font-size: 12px;
            padding-left: 10px;
            letter-spacing: 10px;
        }
        body 
        {
            background-color: White;
        } 
    </style>
    <script type="text/javascript">

        function checkAvailableWidth() {

            var container = document.getElementById("divInfoQTN");

            $('#divInfoQTN th').css('top', document.getElementById("divInfoQTN").scrollTop - 1 + 'px')



        }

        function CheckFileAndOpen(path) {

            if (path != "") {

                window.open(path);
                return false;
            }
        }
    </script>
</head>
<body style="padding: 0px; margin: 0px;background-color:transparent">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- <div id="divInfoQTN" onscroll="checkAvailableWidth()">--%>
            <table style="text-align: right; font-size: 11px" width="100%">
                <tr>
                    <td>
                        Requisition Status : &nbsp
                        <asp:DropDownList ID="ddlReqsnStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReqsnStatus_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="0" Selected="True"> </asp:ListItem>
                            <asp:ListItem Text="New Requisition" Value="NRQ"> </asp:ListItem>
                            <asp:ListItem Text="RFQ / Quotation" Value="RFQ"> </asp:ListItem>
                            <asp:ListItem Text="Quotation Approval" Value="QEV"> </asp:ListItem>
                            <asp:ListItem Text="Raise PO" Value="RPO"> </asp:ListItem>
                            <asp:ListItem Text="Supplier Confirmation" Value="SCN"> </asp:ListItem>
                            <asp:ListItem Text="Update Delivery" Value="UPD"> </asp:ListItem>
                            <asp:ListItem Text="Delivered" Value="DLV"> </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table id="tblGrid" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center">
                        <telerik:RadGrid ID="rgdAllReqStatus" runat="server" AlternatingItemStyle-BackColor="#CEE3F6"
                            Skin="Office2007" AutoGenerateColumns="False" AllowSorting="True" Width="100%"
                            AllowPaging="false" EnableAjaxSkinRendering="true" OnNeedDataSource="rgdAllReqStatus_NeedDataSource"
                            GridLines="None" OnItemDataBound="rgdAllReqStatus_ItemDataBound"  OnSortCommand="rgdAllReqStatus_SortCommand">
                            <PagerStyle Mode="NextPrevAndNumeric" CssClass="Pager" Font-Size="12px" Font-Names="verdana"
                                HorizontalAlign="Right"></PagerStyle>
                            <MasterTableView DataKeyNames="REQUISITION_CODE,document_code,Vessel_Code" Width="100%"
                                AllowPaging="false">
                                <Columns>
                                    <telerik:GridTemplateColumn SortExpression="Vessel_name" HeaderText="Vessel" DataField="Vessel_name"
                                        UniqueName="Vessel_name">
                                        <ItemTemplate>
                                            <a href='<%# "../crew/CrewList_Print.aspx?vid="+Eval("Vessel_Code").ToString() %>'
                                                target="_blank">
                                                <%#Eval("Vessel_name")%>
                                            </a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
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
                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="PO Number" SortExpression="ORDER_CODE">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkOrderNo" runat="server" Target="_blank"><%# DataBinder.Eval(Container.DataItem, "ORDER_CODE")%></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridBoundColumn SortExpression="Name_Dept" HeaderText="Department/Function" DataField="Name_Dept"
                                        AllowSorting="true" UniqueName="Name_Dept">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="SYSTEM_Description" SortExpression="SYSTEM_Description"
                                        DataField="SYSTEM_Description" HeaderText="Catalogue/System">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="SHORT_NAME" HeaderText="Supplier" AllowSorting="true"
                                        DataField="SHORT_NAME" UniqueName="SHORT_NAME">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="DOCUMENT_DATE"  HeaderText="Receival Date"
                                        AllowSorting="true" DataField="requestion_Date" UniqueName="requestion_Date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="TOTAL_ITEMS" HeaderText="Items" DataField="TOTAL_ITEMS"
                                        AllowSorting="true" UniqueName="TOTAL_ITEMS">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Order Raise" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgSelect" runat="server" Text="Select" OnCommand="onSelect"
                                                CommandName="Select" CommandArgument='<%#Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")%>'
                                                ForeColor="Black" ToolTip="View for next process to selected requistion" ImageUrl="~/purchase/Image/view.gif"
                                                Width="12px" Height="12px"></asp:ImageButton>
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
                                    <telerik:GridTemplateColumn DataField="status" HeaderText="Status" UniqueName="Status">
                                        <ItemTemplate>
                                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td style="text-align: left; border: 0px solid white">
                                                        <asp:Label ID="lblclremark" Text='<%#Eval("clRemark") %>' Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblStatus" Text='<%#Eval("status")%>' runat="server"> </asp:Label>
                                                    </td>
                                                    <td style="text-align: right; border: 0px solid white">
                                                        <asp:Image ID="imgStatus" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
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
                                    <telerik:GridBoundColumn SortExpression="code" Visible="false" DataField="code" UniqueName="code">
                                        <HeaderStyle HorizontalAlign="Left" Width="0px" />
                                        <ItemStyle Width="0px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="ORDER_SUPPLIER" Visible="false" DataField="ORDER_SUPPLIER"
                                        UniqueName="ORDER_SUPPLIER">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Actions">
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td align="center" style="width: 20%">
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
                                            <table cellpadding="1" cellspacing="1" width="100px">
                                                <tr>
                                                    <td style="border-color: transparent; width: 20px">
                                                     <a id="A1" runat="server" visible='<%# Eval("ORDER_CODE").ToString().Trim()==""?false:true %>'
                                                        style="border: 0px solid white" href='<%#"QuotationEvalRpt.aspx?Requisitioncode=" + Eval("REQUISITION_CODE").ToString() + "&Document_Code=" + Eval("document_code").ToString() + "&Vessel_Code=" + Eval("Vessel_Code").ToString() %>'
                                                        target="_blank">
                                                        <%--<a id="A1" runat="server" visible='<%# Eval("ORDER_CODE").ToString().Trim()==""?false:true %>'
                                                            style="border: 0px solid white" href='<%#"Quotation_Evaluation_Report.aspx?Requisitioncode=" + Eval("REQUISITION_CODE").ToString() + "&Document_Code=" + Eval("document_code").ToString() + "&Vessel_Code=" + Eval("Vessel_Code").ToString() %>'
                                                            target="_blank">--%>
                                                            <img src="../Images/compare.gif" style="border: 0px solid white; height: 18px; width: 18px"
                                                                title="View Evaluation" onmouseover="DisplayActionInHeader('View Evaluation' ,'rgdAllReqStatus')" />
                                                        </a>
                                                    </td>
                                                    <td style="border-color: transparent;width: 20px">
                                                        <a href="QuotationSummary.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("Document_Code") +"&Vessel_Code="+Eval("Vessel_Code")+"&QUOTATION_CODE=0"%>"
                                                            target="_blank" onmouseover="DisplayActionInHeader('View Quotations' ,'rgdAllReqStatus')">
                                                            <img src="../Images/QtnsSummary.png" style="border: 0px" title="View Quotations" />
                                                        </a>
                                                    </td>
                                                    <td style="border-color: transparent;width: 20px">
                                                         <asp:ImageButton ID="ImgAttachment" Height="20px" Width="20px" ImageUrl="../Images/attachment.png" runat="server" style="border: 0px solid white" 
                                                         onclick='<%#"ShowReqsnAttachment(&#39;Requisition_code=" + Eval("REQUISITION_CODE").ToString()+ "&Vessel_ID=" + Eval("Vessel_Code").ToString()+"&#39;)"%>'/>
                                                    </td>
                                                    <td style="border-color: transparent;width: 20px">
                                                        <img id="imgbtnPurchaserRemark" src="../Images/remark_new.gif" onclick='<%# "GetRemarkAll("+Eval("document_code").ToString()+","+Session["userid"].ToString()+",null,event)" %>'
                                                            onmouseover='<%#"GetRemarkToolTip("+Eval("document_code").ToString() +",null,event);DisplayActionInHeader(\"All Remarks; Click to add new\",\"rgdAllReqStatus\");" %>'
                                                            onmouseout="CloseRemarkToolTip();" title="All Remarks; Click to add new" />
                                                    </td>
                                                    <td style="border-color: transparent; text-align: right;width: 20px">
                                                        <asp:ImageButton ID="btnSendPOToVessel" runat="server" Font-Size="10px" ImageAlign="Middle"
                                                            ImageUrl="~/Images/syncPO.png" OnClick="btnSendPOToVessel_Click" CommandArgument='<%# Eval("ORDER_CODE")%>'
                                                            onmouseover="DisplayActionInHeader('Re-send PO to vessel' ,'rgdAllReqStatus')"
                                                            Visible='<%# Eval("ORDER_CODE").ToString().Trim()!=""?true:false %>'
                                                            Enabled="true" />
                                                    </td>
                                                    <td style="border-color: transparent; text-align: right;background-color:transparent;width: 20px">
                                                        <asp:HyperLink ID="hlnkViewProcessingTime" runat="server" NavigateUrl="#" Style="cursor: pointer" ImageUrl="~/Images/processing-time-icon.png"
                                                            Text="PT" onmouseover='<%#"Get_Reqsn_ProcessingTime_By_Reqsn(&#39;"+Eval("REQUISITION_CODE").ToString()+"&#39;,event,this,0);" %>'  onclick='<%#"Get_Reqsn_ProcessingTime_By_Reqsn(&#39;"+Eval("REQUISITION_CODE").ToString()+"&#39;,event,this,1,&#39;Processing Time(in days)&#39;);" %>'></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" VerticalAlign="Middle" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings>
                                    <PopUpSettings ScrollBars="None"></PopUpSettings>
                                </EditFormSettings>
                            </MasterTableView>
                            <ClientSettings>
                                <Scrolling AllowScroll="false" UseStaticHeaders="true" SaveScrollPosition="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                        <uc1:ucCustomPager ID="ucCustomPagerAllStatus" OnBindDataItem="BindALLRequistionsDetails"
                            runat="server" />
                    </td>
                </tr>
            </table>
            <%--  </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
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
    </form>
</body>
</html>
