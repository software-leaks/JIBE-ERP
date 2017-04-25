<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PendingEvalGrid.aspx.cs"
    Inherits="Technical_INV_PendingEvalGrid" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucReqsncancelLog.ascx" TagName="ucReqsncancelLog"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucPurcCancelReqsn.ascx" TagName="ucPurcCancelReqsn"
    TagPrefix="CanReq" %>
<%@ Register Src="../UserControl/ucPurcReqsnHold_UnHold.ascx" TagName="ucPurcReqsnHold_UnHold"
    TagPrefix="Hold" %>
<%@ Register Src="../UserControl/ucPurc_Rollback_Reqsn.ascx" TagName="ucPurc_Rollback_Reqsn"
    TagPrefix="uc2" %>
 <%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList" TagPrefix="ucDDL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
   <%-- <script src="../Scripts/boxover.js" type="text/javascript"></script>--%>
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

    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function CloseDiv() {
            var control = document.getElementById("divReqStages");
            control.style.visibility = "hidden";
        }

        function CloseDivHold() {
            var control = document.getElementById("divOnHold");
            control.style.display = "none";
            return false;
        }

        function DisplayDivHold() {
            var control = document.getElementById("divOnHold");
            control.style.display = "Block";
        }

        function CheckFileAndOpen(path) {

            if (path != "") {

                window.open(path);
                return false;
            }



        }

    </script>
    <style>
        body 
        {
            background-color: White;
        } 
    </style>
</head>
<body style="padding: 0px; margin: 0px;background-color:transparent">
    <form id="form1" runat="server">
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
        <ContentTemplate>
        <table align="center" cellpadding="0" cellspacing="0" width="50%" style="font-size: 11px">
         <tr>
                                                <td align="left" valign="middle"  style="width: 125px;padding:0px 0px 0px 10px">
                                                    Pending With :
                                                </td>
                                                <td align="left" style="height: 30px; width: 170px;">
                                                
                                                    <ucDDL:ucCustomDropDownList ID="cmdApprover" runat="server" UseInHeader="false" OnApplySearch="cmdApprover_SelectedIndexChanged"/>
                                                </td>
                                                 <td align="left" style="width: 125px;padding:0px 0px 0px 10px">
                                                   Quotation Received:
                                                </td>
                                                <td align="left" valign="middle" style="height: 30px; width: 170px">
                                                    <ucDDL:ucCustomDropDownList ID="ddlSupplier" runat="server" UseInHeader="false" OnApplySearch="ddlSupplier_SelectedIndexChanged"
                                Height="150" Width="160" />
                                                </td>
                                               
                                            </tr>
        </table>

            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 100%; text-align: right">
                        <asp:CheckBox ID="chkAllApprovals" runat="server" Font-Size="12px" AutoPostBack="true"
                            OnCheckedChanged="chkAllApprovals_CheckedChanged" Text="Show All" />
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="rgdEval" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                            SelectedItemStyle-BackColor="Gray" AllowPaging="false" OnItemDataBound="rgdEval_ItemDataBound"
                            OnNeedDataSource="rgd_NeedDataSource" OnSortCommand="rgdEval_SortCommand">
                            <PagerStyle Mode="NextPrevAndNumeric" CssClass="Pager" Font-Size="12px" Font-Names="verdana"
                                HorizontalAlign="Right"></PagerStyle>
                            <MasterTableView AllowMultiColumnSorting="True" Width="100%" AllowPaging="false">
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
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Requisition Number" SortExpression="REQUISITION_CODE">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgPriority" runat="server" ToolTip="Urgent" ImageUrl="~/Images/exclamation.gif"
                                                Height="12px"></asp:ImageButton>
                                            <asp:HyperLink ID="hlinkReq" runat="server" Target="_blank"
                                            NavigateUrl='<%#String.Format("RequisitionSummary.aspx?REQUISITION_CODE={0}&Document_Code={1}&Vessel_Code={2}&Dept_Code={3}&hold={4}", Eval("REQUISITION_CODE"), Eval("document_code"),Eval("Vessel_Code"),Eval("code"),Eval("OnHold"))%>'
                                            Text='<%#Eval("REQUISITION_CODE") %>'> </asp:HyperLink>
                                            <asp:Label ID="lblCriticalFlag" runat="server" Text='<%#Eval("Critical_Flag") %>'  style="display:none"></asp:Label>
                                        <%--    <a href="RequisitionSummary.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")+"&Dept_Code="+Eval("code")+"&"+Eval("OnHold")%>"
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "REQUISITION_CODE")%></a>--%>
                                        </ItemTemplate>
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
                                    <telerik:GridBoundColumn SortExpression="Document_Date" HeaderText="Receival Date"
                                        AllowSorting="true" DataField="requestion_Date" UniqueName="requestion_Date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    
                                    <telerik:GridBoundColumn SortExpression="TOTAL_ITEMS" HeaderText="Items" DataField="TOTAL_ITEMS"
                                        AllowSorting="true" UniqueName="TOTAL_ITEMS">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Qtn.Code" Visible="false">
                                        <ItemTemplate>
                                            <a href="QuotationSummary.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")+"&Dept_Code="+Eval("code")+"&"+Eval("OnHold")+"&QUOTATION_CODE="+Eval("QUOTATION_CODE")%>"
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "QUOTATION_CODE")%></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn SortExpression="RFQSend" Visible="true" HeaderText="RFQs"
                                        AllowSorting="true" DataField="RFQSend" UniqueName="RFQSend">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="QuotReceived" Visible="true" HeaderText="Quotations"
                                        AllowSorting="true" DataField="QuotReceived" UniqueName="QuotReceived">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="Lead_Time" Visible="false" HeaderText="Lead Time(In days)"
                                        AllowSorting="false" DataField="Lead_Time" UniqueName="Lead_Time">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="URGENCY_CODE" HeaderText="Urgency" DataField="URGENCY_CODE"
                                        AllowSorting="false" UniqueName="URGENCY_CODE" Visible="false">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PendApprover" UniqueName="PendApprover" HeaderText=" Pending With">
                                        <ItemStyle BackColor="Wheat" />
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
                                    <telerik:GridBoundColumn SortExpression="QUOTATION_CODE" Visible="false" HeaderText="QUOTATION_CODE"
                                        DataField="QUOTATION_CODE" UniqueName="QUOTATION_CODE">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="OnHold" Visible="false" HeaderText="OnHold"
                                        DataField="OnHold" UniqueName="OnHold">
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
                                         <HeaderStyle width="200px" HorizontalAlign="Left"/>
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
                                            <table cellpadding="2" cellspacing="0">
                                                <tr>
                                                    <td style="border-color: transparent">
                                                        <asp:Button ID="ImgSelect" runat="server" Text="Evaluate" OnCommand="onSelect" Font-Size="12px"
                                                            Height="21px" CommandArgument='<%#Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")+"&QUOTATION_CODE="+Eval("QUOTATION_CODE")+"&"+Eval("OnHold")%>'
                                                            ForeColor="DarkGreen" ToolTip="Qtn Comparison" onmouseover="DisplayActionInHeader('Qtn Comparison' ,'rgdEval')">
                                                        </asp:Button>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                      <asp:ImageButton ID="ImgAttachment" Height="20px" Width="20px" ImageUrl="../Images/attachment.png" runat="server" 
                                                      style="border: 0px solid white"
                                                      onclick='<%#"ShowReqsnAttachment(&#39;Requisition_code=" + Eval("REQUISITION_CODE").ToString()+ "&Vessel_ID=" + Eval("Vessel_Code").ToString()+"&#39;)"%>' >
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <img id="imgbtnPurchaserRemark" src="../Images/remark_new.gif" onclick='<%# "GetRemarkAll("+Eval("document_code").ToString()+","+Session["userid"].ToString()+",null,event)" %>'
                                                            onmouseover='<%#"GetRemarkToolTip("+Eval("document_code").ToString() +",null,event);DisplayActionInHeader(\"All Remarks; Click to add new\",\"rgdEval\");" %>'
                                                            onmouseout="CloseRemarkToolTip();" title="All Remarks; Click to add new" />
                                                                    
                                                                    
                                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgAddItm" runat="server" Text="Select" OnCommand="onAddNewItem"
                                                            CommandName="onAddNewItem" CommandArgument='<%#Eval("REQUISITION_CODE")+"&VCode="+Eval("Vessel_Code")+"&ReqStage=RFQ"%>'
                                                            ForeColor="Black" ToolTip="Add Items from Office side" ImageUrl="~/purchase/Image/briefcase-add.gif"
                                                            Width="16px" Height="16px" onmouseover="DisplayActionInHeader('Add Items from Office side' ,'rgdEval')" Visible='<%# objUA.Edit != 0?true:false  %>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="BtnCancelReqStage" runat="server" Text="Select" OnCommand="OnCancelReq"
                                                            CommandName="OnCancelReq" CommandArgument='<%#Eval("REQUISITION_CODE")+","+Eval("document_code") +","+Eval("Vessel_Code")+","+Eval("code")+","+Eval("OnHold")%>'
                                                            ForeColor="Black" ToolTip="Roll Back" ImageUrl="~/purchase/Image/Cancel_New.gif"
                                                            Width="16px" Height="16px" onmouseover="DisplayActionInHeader('Roll Back' ,'rgdEval')" Visible='<%# objUA.Edit != 0?true:false  %>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="btnOnHold" runat="server" Width="16px" Height="16px" Text="Hold"
                                                            ImageUrl="~/purchase/Image/OnHold.png" OnClick="btnOnHold_Click" OnCommand="OnHold"
                                                            CommandName="OnHold" CommandArgument='<%#Eval("REQUISITION_CODE")+","+Eval("document_code") +","+Eval("Vessel_Code")+","+Eval("code")+","+Eval("OnHold")%>'
                                                            onmouseover="DisplayActionInHeader('Put On Hold' ,'rgdEval')"  Visible='<%# objUA.Edit != 0?true:false  %>'/>
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
                            <ClientSettings EnableRowHoverStyle="false">
                                <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                                <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                                <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                                <Scrolling UseStaticHeaders="false" AllowScroll="false" />
                            <Scrolling UseStaticHeaders="false" AllowScroll="false" /><Scrolling UseStaticHeaders="false" AllowScroll="false" /><Scrolling UseStaticHeaders="false" AllowScroll="false" /><Scrolling UseStaticHeaders="false" AllowScroll="false" /><Scrolling UseStaticHeaders="false" AllowScroll="false" /><Scrolling UseStaticHeaders="false" AllowScroll="false" /><Scrolling UseStaticHeaders="false" AllowScroll="false" /><Scrolling UseStaticHeaders="false" AllowScroll="false" /><Scrolling UseStaticHeaders="false" AllowScroll="false" /><Scrolling UseStaticHeaders="false" AllowScroll="false" /><Scrolling UseStaticHeaders="false" AllowScroll="false" /><Scrolling UseStaticHeaders="false" AllowScroll="false" /></ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindData" />
                    </td>
                </tr>
            </table>
            <div id="divOnHold" style="border: 1px solid Black; position: absolute; left: 35%;
                top: 30%; z-index: 2; color: black;" runat="server">
                <Hold:ucPurcReqsnHold_UnHold ID="HoldUnHold" runat="server" OnCancelClick="btndivCancel_Click"
                    OnSaveClick="btndivSave_Click" />
            </div>
            <div id="divReqStages" style="border: 1px solid Black; position: absolute; left: 17%;
                top: 22%; z-index: 2; color: black; height: auto; width: 495px;display:block;" 
                class="popup-css" runat="server">
                <%--<table style="height: auto; width: 495px" cellpadding="0" cellspacing="0">
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
                        <td style="padding-top: 5px">
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDDLReqStages" ValidationGroup="rollbk"
                                            runat="server" ControlToValidate="DDLReqStages" ErrorMessage="Please select stage" Display="None"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtenderDDLReqStages" TargetControlID="RequiredFieldValidatorDDLReqStages" runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                        *
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="height: 3px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 12px; text-align: right; padding-right: 5px">
                                        Reason:
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtReason" runat="server" Height="57px" Style="font-size: small"
                                            TextMode="MultiLine" Width="98%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtReason" ControlToValidate="txtReason"
                                            ValidationGroup="rollbk" runat="server" Display="None" 
                                             ErrorMessage="Please enter reason."></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtenderReason"  TargetControlID="RequiredFieldValidatortxtReason" runat="server">
                                        </cc1:ValidatorCalloutExtender>
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
                                        <asp:Button ID="btndivReqprioOK" runat="server" Text="Ok" Font-Size="small" Width="100px"
                                            ValidationGroup="rollbk" Height="30px" OnClick="btndivReqprioOK_Click" />
                                        &nbsp;
                                        <asp:Button ID="btndivReqPrioCancel" runat="server" Width="100" Height="30px" Text="Cancel"
                                            Font-Size="small" OnClick="btndivReqPrioCancel_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="color: #FF0000; font-size: small;" align="left">
                                        <div style="max-height: 80px; overflow: auto">
                                            <uc1:ucReqsncancelLog ID="ucReqsncancelLog1" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>--%>
                <uc2:ucPurc_Rollback_Reqsn ID="ucPurc_Rollback_Reqsn1" OnSave="btndivReqprioOK_Click"
                    runat="server" />
            </div>
            <asp:HiddenField ID="HiddenArgument" runat="server" />
            <div id="dvCancelReq" style="display: block; position: fixed; left: 15%; top: 20%"
                runat="server">
                <CanReq:ucPurcCancelReqsn ID="ucPurcCancelReqsnNew" runat="server" />
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
    </form>
</body>
</html>
