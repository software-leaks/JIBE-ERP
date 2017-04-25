<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Delivered_Requisition.aspx.cs"
    Inherits="Purchase_Delivered_Requisition" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"    TagPrefix="ucDDL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Purc_Get_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Get_Remarks_All.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Ins_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>        
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
     <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <title>Delivered requisition</title>
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
<body style="padding: 0px; margin: 0px;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- <div id="divInfoQTN" onscroll="checkAvailableWidth()">--%>
             <table cellpadding="0" cellspacing="0" width="100%" style="font-size: 11px">
         <tr>
                                                <td align="center" valign="middle"  style="width: 125px;padding:0px 0px 0px 10px">
                                                   Supplier
                                                </td>
                                                <td align="left" style="height: 30px; width: 170px;">
                                                   <ucDDL:ucCustomDropDownList ID="ddlSupplier" runat="server" UseInHeader="false" OnApplySearch="ddlSupplier_SelectedIndexChanged" />
                                                </td>
                                               <td align="center" valign="middle"  style="width: 125px;padding:0px 0px 0px 10px">
                                                    Delivery Number
                                                </td>
                                                <td align="left" style="height: 30px; width: 170px;">
                                                   <ucDDL:ucCustomDropDownList ID="DDLPort" Height="150" runat="server"  OnApplySearch="DDLPort_SelectedIndexChanged" UseInHeader="false"/>
                                                </td>
                                               
                                            </tr>
        </table>
            <table id="tblGrid" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center">
                        <uc2:ucCustomDropDownList ID="ucCustomDropDownListstatus" OnApplySearch="BindData"
                            runat="server" />
                        <telerik:RadGrid ID="rgdAllReqStatus" runat="server" AlternatingItemStyle-BackColor="#CEE3F6"
                            Skin="Office2007" AutoGenerateColumns="False" AllowSorting="True" Width="100%"

                            AllowPaging="false" EnableAjaxSkinRendering="true" GridLines="None" 
                            OnItemDataBound="rgdAllReqStatus_ItemDataBound"
                            OnSortCommand="rgdAllReqStatus_SortCommand"
                            PageSize="20" OnNeedDataSource="rgd_NeedDataSource">
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
                                            <a href="POPreview.aspx?RFQCODE=<%# Eval("REQUISITION_CODE") +"&Vessel_Code="+ Eval("Vessel_CODE")+"&Order_Code="+ Eval("ORDER_CODE") %> "
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "ORDER_CODE")%></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn HeaderText="Delivery Number">
                                        <HeaderTemplate>
                                            <asp:Image ID="imgStatusFilter" AlternateText="Filter" ImageUrl="../Images/filter-grid.png"
                                                Height="16px" ImageAlign="Middle" onclick="ShowCustomFilterUserControl(event,'ucCustomDropDownListstatus')"
                                                Style="cursor: pointer" runat="server" /> &nbsp;Delivery Number
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <a  onmouseover='<%# Eval("qtydiff").ToString()=="0.00"?"":"js_ShowToolTip(&#39;The delivered quantity for item has been found to be different from ordered quantity .&#39;,event,this)"%>' 
                                                href='DeliveryOrderSummary.aspx?REQUISITION_CODE=<%# Eval("REQUISITION_CODE") +"&Vessel_Code="+ Eval("Vessel_CODE")+"&document_code="+ Eval("Document_Code") +"&DELIVERY_CODE="+ Eval("DELIVERY_CODE")%> '
                                                target="_blank" style='<%# Eval("qtydiff").ToString()=="0.00"?"": "color:red;font-weight:bold"%>' >
                                                <%# DataBinder.Eval(Container.DataItem, "DELIVERY_CODE")%></a>
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
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="Document_Date" HeaderText="Delivery Date"
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
                                            <asp:ImageButton ID="ImgSelect" runat="server" CommandName="Select" CommandArgument='<%#Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")%>'
                                                ForeColor="Black" ToolTip="View for next process to selected requistion" ImageUrl="~/purchase/Image/view.gif"
                                                Width="12px" Height="12px"></asp:ImageButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Send PO" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgSendPO" runat="server" CommandName="onSendPO" CommandArgument='<%#Eval("REQUISITION_CODE")+"," +Eval("document_code")+ "," + Eval("Vessel_Code")%>'
                                                ForeColor="Black" ImageUrl="~/purchase/Image/HandleHand.png" Width="12px" Height="12px">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn SortExpression="URGENCY_CODE" HeaderText="Urgency" DataField="URGENCY_CODE"
                                        AllowSorting="false" UniqueName="URGENCY_CODE" Visible="false">
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
                                    <telerik:GridBoundColumn SortExpression="REQUISITION_CODE" Visible="false" HeaderText="Requisition"
                                        DataField="REQUISITION_CODE" UniqueName="REQUISITION_CODE">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="Vessel_Code" Visible="false" HeaderText="Vessel_Code"
                                        DataField="Vessel_Code" UniqueName="Vessel_Code">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="code" Visible="false" DataField="code" UniqueName="code">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="ORDER_SUPPLIER" Visible="false" DataField="ORDER_SUPPLIER"
                                        UniqueName="ORDER_SUPPLIER">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Actions">
                                        <HeaderTemplate>
                                         <HeaderStyle width="200px" HorizontalAlign="Left"/>
                                            <table width="200px">
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
                                            <table cellpadding="1" cellspacing="0">
                                                <tr>
                                                    <td style="border-color: transparent">
                                                        <a  id="A1" runat="server" visible='<%# Eval("ORDER_CODE").ToString().Trim()==""?false:true %>' 
                                                            style="border: 0px solid white" href='<%#"QuotationEvalRpt.aspx?Requisitioncode=" + Eval("REQUISITION_CODE").ToString() + "&Document_Code=" + Eval("document_code").ToString() + "&Vessel_Code=" + Eval("Vessel_Code").ToString() %>'
                                                          target="_blank">
                                                       <%-- <a id="A1" runat="server" visible='<%# Eval("ORDER_CODE").ToString().Trim()==""?false:true %>'
                                                            style="border: 0px solid white" href='<%#"Quotation_Evaluation_Report.aspx?Requisitioncode=" + Eval("REQUISITION_CODE").ToString() + "&Document_Code=" + Eval("document_code").ToString() + "&Vessel_Code=" + Eval("Vessel_Code").ToString() %>'
                                                            target="_blank">--%>
                                                            <img src="../Images/compare.gif" style="border: 0px solid white; height: 18px; width: 18px"
                                                                title="View Evaluation" onmouseover="DisplayActionInHeader('View Evaluation' ,'rgdDeliveryStatus')" />
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
                                                           <asp:ImageButton ID="ImgAttachment"  runat="server"  Height="20px" Width="20px" 
                                                            ImageUrl="../Images/attachment.png" style="border: 0px solid white"  
                                                            
                                                            onclick='<%#"ShowReqsnAttachment(&#39;Requisition_code=" + Eval("REQUISITION_CODE").ToString()+ "&Vessel_ID=" + Eval("Vessel_Code").ToString()+"&#39;)"%>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <img id="imgbtnPurchaserRemark" src="../Images/remark_new.gif" onclick='<%# "GetRemarkAll("+Eval("document_code").ToString()+","+Session["userid"].ToString()+",null,event)" %>'
                                                            onmouseover='<%#"GetRemarkToolTip("+Eval("document_code").ToString() +",null,event);DisplayActionInHeader(\"All Remarks; Click to add new\",\"rgdAllReqStatus\");" %>'
                                                            onmouseout="CloseRemarkToolTip();" title="All Remarks; Click to add new" />
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
                                <Scrolling AllowScroll="false" UseStaticHeaders="true" SaveScrollPosition="true" />
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
            <%--  </div>--%>
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
    </form>
</body>
</html>
