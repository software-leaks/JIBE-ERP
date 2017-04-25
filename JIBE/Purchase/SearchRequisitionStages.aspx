<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SearchRequisitionStages.aspx.cs"
    Inherits="Technical_INV_SearchRequisitionStages" Title="View Requisition Summary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="RJS" Namespace="RJS.Web.WebControl" Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" %>
<asp:Content ID="conhead" ContentPlaceHolderID="HeadContent" runat="server">

 <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Purc_Get_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Get_Remarks_All.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Ins_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript">
        function DisplayExport() {
            document.getElementById("divExport").style.display = "block";
            return false;
        }

        function CloseExport() {
            document.getElementById("divExport").style.display = "none";
            return false;
        }

    </script>
    <style type="text/css">
        table.ReqsnHead
        {
            border-top: 1px solid #E6E6E6;
            border-right: 1px solid #E6E6E6;
            background-color: #f4ffff;
        }
        table.ReqsnHead th
        {
            border-width: 1px;
            border-style: solid;
            border-color: #E6E6E6;
        }
        table.ReqsnHead td
        {
            border-bottom: 1px solid #E6E6E6;
            border-left: 1px solid #E6E6E6;
            background-color: #f4ffff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        View Requisition Summary
    </div>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <asp:UpdatePanel ID="updpanal" runat="server">
        <ContentTemplate>
            <center>
                <div style="color: Black">
                    <div id="dvpage-content" class="page-content-main" style="padding: 10px">
                        <div style="padding-top: 2px; padding-bottom: 2px; width: 100%">
                            <table cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 8%; text-align: right;">
                                        Fleet :&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 116px;">
                                        <b>
                                            <asp:UpdatePanel ID="updFleet" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                        Font-Size="12px" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged" Width="117px">
                                                        <asp:ListItem Selected="True" Value="0">--ALL--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </b>
                                    </td>
                                    <td align="center" colspan="2">
                                        <asp:UpdatePanel ID="updDept"  ClientIDMode="Static" runat="server">
                                        <ContentTemplate>
                                        <asp:RadioButtonList ID="optList" runat="server" AutoPostBack="True" Font-Size="11px"
                                            OnSelectedIndexChanged="optList_SelectedIndexChanged" RepeatDirection="Horizontal"
                                            Width="250px">
                                        </asp:RadioButtonList>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="right" style="width: 10%">
                                        Time Lapse :&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 15%;">
                                        <asp:TextBox ID="txtTimeLapse" runat="server" Style="font-size: small" Width="113px"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 10%">
                                        From Date :&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 15%;">
                                        <asp:TextBox ID="txtSearchJoinFromDate" runat="server" Width="113px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtSearchJoinFromDate"
                                            Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td rowspan="3">
                                        <asp:Button ID="btnRetrieve" runat="server" OnClick="btnRetrieve_Click" CssClass="btncss"
                                            Width="100%" Text="Search" />
                                        <hr style="border: 1px" />
                                        <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CssClass="btncss"
                                            Text="Clear Filters" />
                                            <hr style="border: 1px" />
                                            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btncss" Width="100%"
                                            OnClick="btnExport_Click" />
                                          
                                        
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td style="width: 8%; text-align: right;">
                                        Vessel :&nbsp;&nbsp;
                                    </td>
                                    <td style="text-align: left;">
                                        <b>
                                            <asp:UpdatePanel ID="updVessel" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="DDLFleet" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="True" Font-Size="12px"
                                                        Width="117px">
                                                        <asp:ListItem Selected="True" Value="0">--ALL--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </b>
                                    </td>
                                    <td align="right" style="width: 10%">
                                        Department :&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cmbDept" runat="server" AppendDataBoundItems="True" Font-Size="12px"
                                            OnSelectedIndexChanged="cmbDept_OnSelectedIndexChanged" Width="117px">
                                            <asp:ListItem Selected="True" Value="0">--ALL--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 10%">
                                        Show all On Hold :&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlHoldUnhold" runat="server" AppendDataBoundItems="True" AutoPostBack="false"
                                            Font-Size="12px" OnSelectedIndexChanged="cmbUrgency_SelectedIndexChanged" Width="117px"
                                            Style="text-align: left">
                                            <asp:ListItem Selected="True" Value="0">--ALL--</asp:ListItem>
                                            <asp:ListItem Value="1">Un Hold</asp:ListItem>
                                            <asp:ListItem Value="2">Hold</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 10%">
                                        To Date :&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 15%;">
                                        <asp:TextBox ID="txtSearchJoinToDate" runat="server" Width="113px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSearchJoinToDate"
                                            Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr align="right">
                                    <td style="width: 8%; text-align: right;">
                                        Req No :&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 80px; font-size: small; color: #000000; text-align: left;">
                                        <asp:TextBox ID="txtReqNo" runat="server" Style="font-size: small" Text="" Width="113px"></asp:TextBox>
                                    </td>

                                     <td style="width: 8%; text-align: right;">
                                        PO No :&nbsp;&nbsp;
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtPoNo" runat="server" Style="font-size: small" Text="" Width="113px"></asp:TextBox>
                                    </td>
                                   
                                    <%--<td align="right" style="width: 10%">
                                        Qtn Ref :&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 15%;">
                                        <asp:TextBox ID="txtQutRef" runat="server" Style="font-size: small; text-align: left;"
                                            Width="113px"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 10%">
                                        Delivery Type :&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 15%;">
                                        <asp:TextBox ID="txtDeliveryType" Text="" runat="server" Style="font-size: small"
                                            Width="113px"></asp:TextBox>
                                    </td>--%>
                                    <td align="right" style="width: 10%">
                                        Urgency Of Req :&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cmbUrgency" runat="server" AppendDataBoundItems="True" AutoPostBack="false"
                                            Font-Size="12px" OnSelectedIndexChanged="cmbUrgency_SelectedIndexChanged" Width="117px">
                                            <asp:ListItem Selected="True" Value="0">--ALL--</asp:ListItem>
                                            <asp:ListItem Value="N">Normal</asp:ListItem>
                                            <asp:ListItem Value="U">Urgent</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>


                                    
                                </tr>
                                <tr>
                                     
                                  <%--  <td align="right" style="width: 10%">
                                        DO No :&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 15%;">
                                        <asp:TextBox ID="txtDoNo" runat="server" Style="font-size: small" Text="" Width="113px"></asp:TextBox>
                                    </td>--%>
                                    <td align="right" style="width: 10%; font-size: small; color: #000000;">
                                    </td>
                                    <td style="width: 3px;">
                                        <asp:TextBox ID="txtInvoiceNo" runat="server" Text="" Visible="false" Style="font-size: small;
                                            text-align: left;" Width="117px"></asp:TextBox>
                                    </td>
                                    <%--<td align="right" style="width: 10%">
                                        Delivery Status :&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 15%;">
                                        <asp:TextBox ID="txtDeliveryStatus" Text="" runat="server" Style="font-size: small"
                                            Width="113px"></asp:TextBox>
                                    </td>--%>
                                </tr>
                            </table>
                        </div>
                        <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExport" />
                                <asp:AsyncPostBackTrigger ControlID="btnRetrieve" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:GridView ID="rgdReqAllStage" runat="server" EmptyDataText="NO RECORDS FOUND" 
                                    CellPadding="4" CellSpacing="0" AutoGenerateColumns="False" OnRowDataBound="rgdReqAllStage_RowDataBound"
                                    Width="100%" GridLines="None" AllowSorting="true" CssClass="PMSGridItemStyle-css" >
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    
                                    <Columns>
                                        <asp:TemplateField HeaderText="View Audit" >
                                            <ItemTemplate>
                                                <a href="Audittrailsummery.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")+"&QUOTATION_CODE="+Eval("QUOTATION_CODE")%>"
                                                    target="_blank" title="View Audit Summary">
                                                    <img style="border: 0; width: 15px; height: 12px" src="Image/AuditTrails.gif" alt="View Audit Summary" /></a>
                                            </ItemTemplate>
                                            <ItemStyle Width="10px" />
                                            <HeaderStyle Wrap="true" Width="10px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vessel Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVessel_Name" runat="server" Text='<%#Eval("Vessel_Short_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Reqcode" runat="server" Text='<%#Eval("REQUISITION_CODE")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Eval("requestion_Date")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Urgency">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Urgency" runat="server" Text='<%#Eval("URGENCY")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RFQ Sent">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRFQSend" runat="server"></asp:Label>
                                                <asp:Label ID="lblInfo" runat="server" Text='<%#Eval("Document_Code")%>'  ToolTip='<%#Eval("Document_Code")+","+Eval("Vessel_Code") %>'
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qtn Rcvd">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuotReceived" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Evaluation Date">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "EVALUATION_DATE")%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Number">
                                            <ItemTemplate>
                                                <a href="POPreview.aspx?RFQCODE=<%# Eval("REQUISITION_CODE") +"&Vessel_Code="+ Eval("Vessel_Code")+"&Order_Code="+ Eval("ORDER_CODE") %> "
                                                    target="_blank">
                                                    <%# DataBinder.Eval(Container.DataItem, "ORDER_CODE")%></a>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "ORDER_DATE")%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" >
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sent to supp.">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "SentToSupp")%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="conf by supp.">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "ConfBySupp")%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" >
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status.">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Status")%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" >
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DO Number">
                                            <ItemTemplate>
                                                <a href='DeliveryOrderSummary.aspx?REQUISITION_CODE=<%# Eval("REQUISITION_CODE") +"&Vessel_Code="+ Eval("Vessel_CODE")+"&document_code="+ Eval("Document_Code") +"&DELIVERY_CODE="+ Eval("DELIVERY_CODE")%> '
                                                    target="_blank">
                                                    <%# DataBinder.Eval(Container.DataItem, "DELIVERY_CODE")%></a>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" >
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date.">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "DELIVERY_DATE")%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" >
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Time Lapsed.">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TimeLaps")%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="On Hold">
                                            <ItemTemplate>
                                                <%#Eval("OnHold")%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="15" OnBindDataItem="BindSearchRequistionstages" />
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
