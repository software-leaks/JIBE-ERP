<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PendingReqGrid.aspx.cs" Inherits="Technical_INV_PendingReqGrid" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControl/ucReqsncancelLog.ascx" TagName="ucReqsncancelLog"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucPurcCancelReqsn.ascx" TagName="ucPurcCancelReqsn"
    TagPrefix="CanReq" %>
<%@ Register Src="../UserControl/ucPurcReqsnHold_UnHold.ascx" TagName="ucPurcReqsnHold_UnHold"
    TagPrefix="Hold" %>
<%@ Register Src="../UserControl/ucPurcQuotationApproval.ascx" TagName="ucApprovalUser"
    TagPrefix="ucUser" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

   
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Purc_Get_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Get_Remarks_All.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Ins_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    
    <title>RFQ/Quotation</title>
    <script language="javascript" type="text/javascript">

        function CloseDiv() {
            var control = document.getElementById("divOnHold");
            control.style.visibility = "hidden";
        }

        function CloseDiv1() {
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

        function GetRemark(doccode) {

            var ev = window.event;

            Async_getPurchaseRemarks(doccode, '301');
            document.getElementById("dvPurchaseRemark").style.display = "block";
            document.getElementById("dvPurchaseRemark").style.left = (ev.x - 270) + "px";
            document.getElementById("dvPurchaseRemark").style.top = ev.y + "px";
        }

        function CloseRemark() {
            document.getElementById("dvPurchaseRemark").style.display = "none";
        }    
        

    
    </script>
    <style type="text/css">
        .ml
        {
            margin-left: 80px;
        }
        .mr
        {
            margin-right: 80px;
        }
        
        .tdTooltip
        {
            border-bottom: 1px solid gray;
            width: 250px;
        }
        .tdHtip
        {
            font-weight: bold;
            text-align: right;
            font-size: 11px;
            font-family: Verdana;
            vertical-align: top;
        }
        .tdDtip
        {
            text-align: left;
            padding-left: 3px;
            font-size: 11px;
            font-family: Verdana;
            vertical-align: top;
        }
        body 
        {
            background-color: White;
        } 
    </style>
</head>
<body style="padding: 0px; margin: 0px; background-color: transparent">
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
    <div style="text-align: center;" class="main">
        <asp:UpdatePanel ID="UpdatePanel1" RenderMode="Inline" runat="server">
            <ContentTemplate>
            <table cellpadding="1" cellspacing="1" width="100%" style="font-size: 11px">
         <tr>         
                                                <td align="right" valign="middle" width="170px" style="padding:0px 20px 0px 0px">
                                                   Minimum Quotation No Received:
                                                </td>
                                                <td align="left" style="height: 30px; width: 150px">
                                                    <asp:DropDownList ID="ddlMinQuot" runat="server" 
                                                        onselectedindexchanged="ddlMinQuot_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </td>                                                                                         
                                            </tr>
        </table>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center">
                            <telerik:RadGrid ID="rgdPending" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                OnItemDataBound="rgdPending_ItemDataBound" OnNeedDataSource="rgd_NeedDataSource"  OnSortCommand="rgdPending_SortCommand">
                                <FooterStyle Font-Bold="True" Font-Size="X-Small" ForeColor="#996633" />
                                <PagerStyle Mode="NextPrevAndNumeric" CssClass="Pager" Font-Size="12px" Font-Names="verdana"
                                    HorizontalAlign="Right"></PagerStyle>
                                <ClientSettings>
                                    <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                                    <Resizing EnableRealTimeResize="true" />
                                <Scrolling UseStaticHeaders="false" AllowScroll="false" /><Resizing EnableRealTimeResize="true" /><Scrolling UseStaticHeaders="false" AllowScroll="false" /><Resizing EnableRealTimeResize="true" /><Scrolling UseStaticHeaders="false" AllowScroll="false" /><Resizing EnableRealTimeResize="true" /></ClientSettings>
                                <MasterTableView DataKeyNames="REQUISITION_CODE,document_code,Vessel_Code" Width="100%"
                                    AllowPaging="false">
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <Columns>
                                        <telerik:GridTemplateColumn SortExpression="Vessel_name" FilterListOptions="AllowAllFilters"
                                            HeaderText="Vessel" DataField="Vessel_name" UniqueName="Vessel_name">
                                            <ItemTemplate>
                                                <a href='<%# "../crew/CrewList_Print.aspx?vid="+Eval("Vessel_Code").ToString() %>'
                                                    target="_blank">
                                                    <%#Eval("Vessel_name")%></a>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn SortExpression="REQUISITION_CODE" AllowFiltering="true"  HeaderText="Requisition Number">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgPriority" runat="server"
                                                    Height="12px"></asp:ImageButton>
                                                     <asp:HyperLink ID="hlinkReq" runat="server" Target="_blank"
                                                    NavigateUrl='<%#String.Format("RequisitionSummary.aspx?REQUISITION_CODE={0}&Document_Code={1}&Vessel_Code={2}&Dept_Code={3}&hold={4}", Eval("REQUISITION_CODE"), Eval("document_code"),Eval("Vessel_Code"),Eval("code"),Eval("OnHold"))%>'
                                                    Text='<%#Eval("REQUISITION_CODE") %>'> </asp:HyperLink>
                                                    <asp:Label ID="lblCriticalFlag" runat="server" Text='<%#Eval("Critical_Flag") %>'  style="display:none"></asp:Label>
                                                <%--<a href="RequisitionSummary.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")+"&Dept_Code="+Eval("code")+"&hold="+Eval("OnHold")%>"
                                                    target="_blank">
                                                    <%# DataBinder.Eval(Container.DataItem, "REQUISITION_CODE")%></a>--%>
                                            </ItemTemplate>
                                            <ItemStyle />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                      
                                        <telerik:GridBoundColumn SortExpression="REQUISITION_CODE" HeaderText="Requisition Number"
                                            DataField="REQUISITION_CODE" UniqueName="REQUISITION_CODE" Visible="false">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
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
                                        <telerik:GridBoundColumn  SortExpression="DOCUMENT_DATE" HeaderText="Receival Date"
                                            AllowSorting="true" DataField="requestion_Date" UniqueName="requestion_Date">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn SortExpression="TOTAL_ITEMS" HeaderText="Items" DataField="TOTAL_ITEMS"
                                            AllowSorting="true" UniqueName="TOTAL_ITEMS">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Qtn Code" Display="false" AllowFiltering="false">
                                            <ItemTemplate>
                                                <a href="QuotationSummary.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")+"&Dept_Code="+Eval("code")+"&"+Eval("OnHold")+"&QUOTATION_CODE="+Eval("QUOTATION_CODE")%>"
                                                    target="_blank">
                                                    <%# DataBinder.Eval(Container.DataItem, "QUOTATION_CODE")%></a>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn SortExpression="RFQSend" HeaderText="RFQs" DataField="RFQSend"
                                            AllowSorting="true" UniqueName="RFQSend">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn SortExpression="QuotReceived" HeaderText="Qtns" DataField="QuotReceived"
                                            UniqueName="QuotReceived">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnQtnRcvd" Text='<%#Eval("QuotReceived") %>' OnClick="lbtnQtnRcvd_Click"
                                                    CommandArgument='<%#Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")+"&QUOTATION_CODE="+Eval("QUOTATION_CODE")+"&"+Eval("OnHold")%>'
                                                    runat="server"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn UniqueName="InProcess" DataField="Sent_Reqsn_Status" HeaderStyle-Width="60px"
                                            ItemStyle-Width="60px" SortExpression="Sent_Reqsn_Status" ItemStyle-HorizontalAlign="Center"
                                            HeaderText="Pending">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="QuotDeclined" DataField="QuotDeclined" SortExpression="QuotDeclined"
                                            ItemStyle-HorizontalAlign="Center" HeaderText="Declined">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn SortExpression="URGENCY_CODE" HeaderText="Urgency" DataField="URGENCY_CODE"
                                            UniqueName="URGENCY_CODE" AllowSorting="false" Visible="false">
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
                                        <telerik:GridBoundColumn SortExpression="QUOTATION_CODE" Display="false" HeaderText="QUOTATION_CODE"
                                            DataField="QUOTATION_CODE" UniqueName="QUOTATION_CODE">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn Display="false" HeaderText="MinQuotReceived"
                                            DataField="is_minquot_received" UniqueName="is_minquot_received">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Actions">
                                            <HeaderTemplate>
                                                <table>
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
                                                <table cellpadding="2" cellspacing="0">
                                                    <tr>
                                                    <td style="border-color: transparent">
                                                     <img alt="BuyerRemark" id="imgBuyerRemark" src="../Images/remark_new.gif" onclick='<%# "GetBuyerRemark("+Eval("document_code").ToString()+","+Session["userid"].ToString()+",null,event)" %>'
                                                                title="Buyer Remarks; Click to add new"/>
                                                    </td>
                                                        <td style="border-color: transparent">
                                                            <asp:ImageButton ID="ImgSelect" runat="server" Text="Select" OnCommand="onSendRFQ"
                                                                Height="16px" CommandName="onSendRFQ" CommandArgument='<%#Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")+"&Dept_Code="+Eval("code")+"&"+Eval("OnHold")%>'
                                                                ForeColor="Black" ToolTip="Select Suppliers" ImageUrl="~/purchase/Image/SendRFQ.png"
                                                                onmouseover="DisplayActionInHeader('Send RFQ' ,'rgdPending')" OnClientClick='<%# "Async_Get_Reqsn_Validity(&#39;"+Eval("REQUISITION_CODE").ToString()+"&#39;)" %>'
                                                                Visible='<%# objUA.Edit != 0?true:false  %>'></asp:ImageButton>
                                                        </td>
                                                        <td style="border-color: transparent">
                                                            <asp:ImageButton ID="ImgUpd" runat="server" Text="Select" OnCommand="onSelect" Height="16px"
                                                                CommandArgument='<%#Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")+"&Dept_Code="+Eval("code")+"&"+Eval("OnHold")%>'
                                                                ForeColor="Black" ToolTip="Import Excel RFQ" onmouseover="DisplayActionInHeader('Import Excel RFQ' ,'rgdPending')"
                                                                ImageUrl="~/purchase/Image/Excel-2010.png" Visible='<%# objUA.Edit != 0?true:false  %>'>
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td style="border-color: transparent">
                                                            <asp:LinkButton ID="btnSenttosupdt" runat="server" OnClick="btnSenttosupdt_Click"
                                                                Text="SendToSupdt" CommandArgument='<%#Eval("ID")+","+Eval("Office_ID") +","+Eval("REQUISITION_CODE")+","+Eval("document_code")+","+Eval("Vessel_Code")%>'
                                                                Height="12px" Font-Size="11px" ToolTip="Send to Suptd for Approval" onmouseover="DisplayActionInHeader('Send to Suptd for Approval' ,'rgdPending')"
                                                                Visible='<%# objUA.Edit != 0?true:false  %>' />
                                                        </td>
                                                        <td style="border-color: transparent; width: 15px">
                                                        </td>
                                                        <td style="border-color: transparent">
                                                             <asp:ImageButton ID="ImgAttachment" Height="20px" Width="20px" ImageUrl="../Images/attachment.png" runat="server" style="border: 0px solid white"
                                                               onclick='<%#"ShowReqsnAttachment(&#39;Requisition_code=" + Eval("REQUISITION_CODE").ToString()+ "&Vessel_ID=" + Eval("Vessel_Code").ToString()+"&#39;)"%>' >
                                                        </asp:ImageButton>
                                                        </td>
                                                        <td style="border-color: transparent">
                                                            <img id="imgbtnPurchaserRemark" src="../Images/remark_new.gif" onclick='<%# "GetRemarkAll("+Eval("document_code").ToString()+","+Session["userid"].ToString()+",null,event)" %>'
                                                                onmouseover='<%#"GetRemarkToolTip("+Eval("document_code").ToString() +",null,event);DisplayActionInHeader(\"All Remarks; Click to add new\",\"rgdPending\");" %>'
                                                                onmouseout="CloseRemarkToolTip();" title="All Remarks; Click to add new" />
                                                       </td>
                                                        <td style="border-color: transparent">
                                                            <asp:ImageButton ID="ImgAddItm" runat="server" Text="Select" OnCommand="onAddNewItem"
                                                                CommandName="onAddNewItem" CommandArgument='<%#Eval("REQUISITION_CODE")+"&VCode="+Eval("Vessel_Code")+"&ReqStage=RFQ"%>'
                                                                ForeColor="Black" ToolTip="Add Items from Office side" ImageUrl="~/purchase/Image/briefcase-add.gif"
                                                                Width="16px" Height="16px" onmouseover="DisplayActionInHeader('Add Items from Office side' ,'rgdPending')"
                                                                Visible='<%# objUA.Edit != 0?true:false  %>'></asp:ImageButton>
                                                        </td>
                                                        <td style="border-color: transparent; width: 15px">
                                                        </td>
                                                        <td style="border-color: transparent">
                                                            <asp:ImageButton ID="BtnCancelReqStage" runat="server" Visible="false" Text="Select"
                                                                OnCommand="OnCancelReq" CommandName="OnCancelReq" CommandArgument='<%#Eval("REQUISITION_CODE")+","+Eval("document_code") +","+Eval("Vessel_Code")+","+Eval("code")+","+Eval("OnHold")%>'
                                                                ForeColor="Black" ToolTip="Roll Back" ImageUrl="~/purchase/Image/Cancel_New.gif"
                                                                Width="16px" Height="16px" onmouseover="DisplayActionInHeader('Roll Back' ,'rgdPending')">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td style="border-color: transparent">
                                                            <asp:ImageButton ID="btnOnHold" runat="server" Width="16px" Height="16px" Text="Hold"
                                                                ImageUrl="~/purchase/Image/OnHold.png" OnClick="btnOnHold_Click" OnCommand="OnHold"
                                                                CommandName="OnHold" onmouseover="DisplayActionInHeader('Put on Hold' ,'rgdPending')"
                                                                CommandArgument='<%#Eval("REQUISITION_CODE")+","+Eval("document_code") +","+Eval("Vessel_Code")+","+Eval("code")+","+Eval("OnHold")%>'
                                                                Visible='<%# objUA.Edit != 0?true:false  %>' />
                                                            <asp:ImageButton ID="btnOnRelease" runat="server" Width="16px" Height="16px" Text="Hold"
                                                                ImageUrl="~/purchase/Image/release.png" OnClick="btnOnHold_Click" OnCommand="OnHold"
                                                                CommandName="OnHold" onmouseover="DisplayActionInHeader('Cancel Hold' ,'rgdPending')"
                                                                CommandArgument='<%#Eval("REQUISITION_CODE")+","+Eval("document_code") +","+Eval("Vessel_Code")+","+Eval("code")+","+Eval("OnHold")%>'
                                                                Visible='<%# objUA.Edit != 0?true:false  %>' />
                                                        </td>
                                                        <td style="border-color: transparent">
                                                            <asp:ImageButton ID="imgbtnCancel" Visible="false" AlternateText="Cancel Reqsn" OnClick="imgbtnCancel_Click"
                                                                ImageUrl="~/Images/Close.gif" runat="server" CommandArgument='<%#Eval("REQUISITION_CODE") +","+Eval("document_code")+","+Eval("Vessel_Code") %>'
                                                                onmouseover="DisplayActionInHeader('Cancel Requisition' ,'rgdPending')" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Width="25%" VerticalAlign="Middle" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="rework" DataField="rework" Display="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrework" runat="server" Text='<%#Eval("rework") %>' ToolTip='<%#Eval("Remarktopurc") %>'> </asp:Label></ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <PopUpSettings ScrollBars="None"></PopUpSettings>
                                    </EditFormSettings>
                                </MasterTableView>
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
                    top: 22%; z-index: 2; color: black; width: 495px;" class="popup-css" runat="server">
                    <table style="width: 100%; font-family: Tahoma" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="vertical-align: top; border-bottom: 1px solid gray; padding-bottom: 5px">
                                <table style="width: 495px" cellpadding="0" cellspacing="0">
                                    <tr align="center">
                                        <td style="font-size: 12px; width: 100%; padding: 5px 0px 5px 0px; border-bottom: 1px solid gray;"
                                            class="popup-css" colspan="2">
                                            <asp:Label ID="Label1" runat="server" Text="Roll Back" Style="color: Black; font-weight: 700;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%; font-size: 12px; padding: 2px 0px 2px 0px; text-align: right">
                                            Requisition No.:
                                        </td>
                                        <td style="width: 50%; font-size: 12px; text-align: left">
                                            <asp:Label ID="lblReqsnNoRollBack" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                <table style="width: 495px" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="font-size: 12px; text-align: right; padding-right: 5px">
                                            Roll Back Stages :
                                        </td>
                                        <td align="left">
                                            <asp:ListBox ID="DDLReqStages" runat="server" Style="font-size: small" Width="223px">
                                            </asp:ListBox>
                                        </td>
                                        <td style="color: #FF0000; font-size: small;" align="left">
                                            *
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="height: 3px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 12px; text-align: right; padding-right: 5px">
                                            Reason:
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtReason" runat="server" Height="57px" Style="font-size: small"
                                                TextMode="MultiLine" Width="98%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 5px">
                                <table style="width: 495px" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btndivReqprioOK" runat="server" Text="Ok" Font-Size="12px" Height="30px"
                                                Width="100px" OnClick="btndivReqprioOK_Click" />
                                            &nbsp;
                                            <asp:Button ID="btndivReqPrioCancel" runat="server" Text="Cancel" Font-Size="12px"
                                                Height="30px" Width="100px" OnClick="btndivReqPrioCancel_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="color: #FF0000; font-size: small;" align="left" colspan="2">
                                            <div style="max-height: 80px; overflow: auto">
                                                <uc1:ucReqsncancelLog ID="ucReqsncancelLog1" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvCancelReq" style="display: block; position: fixed; left: 15%; top: 20%"
                    runat="server">
                    <CanReq:ucPurcCancelReqsn ID="ucPurcCancelReqsnNew" runat="server" />
                </div>
                <div id="dvSendForApproval" style="position: fixed; left: 30%; top: 5%; padding: 10px 0px 10px 0px;
                    width: 520px; border: 1px solid black" runat="server" class="popup-css" visible="false">
                    <table width="100%">
                        <tr>
                            <td style="width: 100%; text-align: left">
                                <asp:GridView ID="gvQuotationList" runat="server" Font-Size="11px" Width="100%" AutoGenerateColumns="False">
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
                                <hr />
                                <span style="font-size: 11px; font-weight: bold; font-family: Verdana">Select BGT Code:</span>
                                <asp:DropDownList ID="ddlBudgetCode" runat="server" Style="font-size: small" Width="70%">
                                </asp:DropDownList>
                                <br />
                                <asp:Button ID="btnRequestAmount" runat="server" Visible="false" 
                                    Text="Request Budget Amount " onclick="btnRequestAmount_Click" />
                                <hr />
                                <ucUser:ucApprovalUser ID="ucApprovalUser1" OnstsSaved="OnStsSaved" runat="server" />
                                <asp:HiddenField ID="HiddenFieldSuppdtRemark" runat="server" />
                                <br />
                                <asp:Label ID="lblBudgetMsg" runat="server" ForeColor="Red" Text="" ></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvPruchremarkMain" style="display: none; position: fixed; left: 30%; top: 10%;
            padding: 10px" class="popup-css">
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
        <div id="dvBuyerRemark" style="border: 1px solid gray; z-index: 501; color: Black;
            display: none; position: fixed; left: 400px; top: 100px" class="Tooltip-css">
        </div>
        <div id="dvBuyerRemarks" style="border: 1px solid gray; z-index: 501; color: Black;
            display: none; position: fixed; left: 400px; top: 100px" class="Tooltip-css">
        </div>
        <div id="dvBuyerremarkMain" style="display: none; position: fixed; left: 30%; top: 10%;
            padding: 10px" class="popup-css">
            <table>
                <tr>
                    <td>
                        <div id="dvShowBuyerRemark" style="position: relative">
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
                                    <textarea id="Textarea1" cols="40" rows="5" style="width: 490px; height: 60px"></textarea>
                                    <%--<input id="txtRemark" type="text" style="width: 490px; height: 60px" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <input id="Button1" onclick="SaveBuyerRemarks();" type="button" style="height: 30px;
                                        width: 80px" value="Save" />&nbsp;&nbsp
                                    <input id="Button2" onclick="CloseRemarkAll();" type="button" value="Close"
                                        style="height: 30px; width: 80px" />
                                    <input id="hdnDocCode" type="hidden" />
                                </td>
                            </tr>
                        </table>
                        <input id="hdnBuyerID" type="hidden" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
