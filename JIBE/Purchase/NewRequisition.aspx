<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewRequisition.aspx.cs" Inherits="Technical_INV_NewRequisition" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControl/ucPurcCancelReqsn.ascx" TagName="ucPurcCancelReqsn"
    TagPrefix="uc1" %>
    <%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Src="../UserControl/ucPurcReqsnHold_UnHold.ascx" TagName="ucPurcReqsnHold_UnHold"
    TagPrefix="Hold" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Purc_Get_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Get_Remarks_All.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Ins_Remarks.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>

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
    <script type="text/javascript">

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
    <asp:UpdatePanel ID="updad" runat="server">
    <ContentTemplate>
    <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 702;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <%--<table cellpadding="1" cellspacing="1" width="100%" style="font-size: 11px">
         <tr>
                                                <td align="center" valign="middle" width="120px">
                                                    Account Code :
                                                </td>
                                                <td align="left" style="height: 30px; width: 170px">
                                                    <ucDDL:ucCustomDropDownList ID="ddlAccClassification" runat="server" 
                                                        UseInHeader="false" />
                                                </td>
                                                 <td align="left" style="width: 125px;padding:0px 0px 0px 10px">
                                                    Urgency Level:
                                                </td>
                                                <td align="center" valign="middle" style="height: 30px; width: 170px">
                                                   <asp:DropDownList ID="ddlUrgencyLvl" runat="server"  Width="150px"></asp:DropDownList>
                                                </td>
                                                <td align="center" valign="middle" style="width: 125px;">
                                                    Requisition Status :
                                                </td>
                                                <td align="left" style="height: 30px; width: 170px">
                                                    <ucDDL:ucCustomDropDownList ID="ddlReqsnStatus" runat="server" 
                                                        UseInHeader="false" />
                                                </td>
                                            </tr>
        </table>--%>
            <table cellpadding="0" cellspacing="0" width="100%">
           
                <tr>
                    <td align="center">
                        <telerik:RadGrid ID="rgdNewREQ" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                            AllowPaging="false" OnItemDataBound="rgdNewREQ_ItemDataBound" OnDataBound="rgdNewREQ_DataBound"
                            MasterTableView-ItemStyle-Height="12px" OnNeedDataSource="rgd_NeedDataSource" OnSortCommand="rgdNewREQ_SortCommand">
                            <PagerStyle Mode="NextPrevAndNumeric" CssClass="Pager" Font-Size="12px" Font-Names="verdana"
                                HorizontalAlign="Right"></PagerStyle>
                            <MasterTableView DataKeyNames="REQUISITION_CODE,document_code,Vessel_Code" AllowMultiColumnSorting="True"
                                Width="100%" AllowPaging="false" ItemStyle-Height="15px" HeaderStyle-Height="40px">
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
                                    <telerik:GridTemplateColumn SortExpression="REQUISITION_CODE" AllowFiltering="false"
                                        HeaderText="Requisition Number">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgPriority" runat="server"  Height="12px"></asp:ImageButton>
                                            <asp:HyperLink ID="hlinkReq" runat="server" Target="_blank"
                                            NavigateUrl='<%#String.Format("RequisitionSummary.aspx?REQUISITION_CODE={0}&Document_Code={1}&Vessel_Code={2}&Dept_Code={3}&hold={4}", Eval("REQUISITION_CODE"), Eval("document_code"),Eval("Vessel_Code"),Eval("code"),Eval("OnHold"))%>'
                                            Text='<%#Eval("REQUISITION_CODE") %>'> </asp:HyperLink>
                                           <asp:Label ID="lblCriticalFlag" runat="server" Text='<%#Eval("Critical_Flag") %>'  style="display:none"></asp:Label>
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
                                    <telerik:GridTemplateColumn SortExpression="Subsystem_Description" AllowFiltering="false"
                                        HeaderText="Sub-Catalogue/Sub-System" DataField="Subsystem_Description">
                                        <ItemTemplate>
                                        <asp:Label ID="lblsubsystem" runat="server" Text="Subsystem_Description"></asp:Label>
                                        </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn SortExpression="REQUISITION_CODE" HeaderText="Requisition"
                                        DataField="REQUISITION_CODE" UniqueName="REQUISITION_CODE" Visible="false">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Receival Date" AllowSorting="true" SortExpression="Document_Date"
                                        DataField="requestion_Date" UniqueName="requestion_Date">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="TOTAL_ITEMS" HeaderText="Items" DataField="TOTAL_ITEMS"
                                        AllowSorting="false" UniqueName="TOTAL_ITEMS" Visible="false">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="URGENCY_CODE" HeaderText="Urgency" DataField="URGENCY_CODE"
                                        AllowSorting="false" UniqueName="URGENCY_CODE" Display="false" Visible="false">
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
                                        Aggregate="Count" FooterText="Total items found:" DataField="OnHold" UniqueName="OnHold">
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
                                                        <asp:ImageButton ID="ImgSelect" runat="server" Text="Select" OnCommand="onSelect"
                                                            Visible='<%# objUA.Edit != 0?true:false  %>' CommandName="Select" Height="12px"
                                                            CommandArgument='<%#Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")+"&Dept_Code="+Eval("code")+"&"+Eval("OnHold")%>'
                                                            ForeColor="Black" ToolTip="Send RFQ" ImageUrl="~/Purchase/Image/SendRFQ.png"
                                                            onmouseover="DisplayActionInHeader('Send RFQ','rgdNewREQ')"></asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                       <asp:ImageButton ID="ImgAttachment"  runat="server"  Height="20px" Width="20px" ImageUrl="../Images/attachment.png" 
                                                        style="border: 0px solid white"  
                                                            onclick='<%#"ShowReqsnAttachment(&#39;Requisition_code=" + Eval("REQUISITION_CODE").ToString()+ "&Vessel_ID=" + Eval("Vessel_Code").ToString()+"&#39;)"%>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <img id="imgbtnPurchaserRemark" src="../Images/remark_new.gif" onclick='<%# "GetRemarkAll("+Eval("document_code").ToString()+","+Session["userid"].ToString()+",null,event)" %>'
                                                            onmouseover='<%#"GetRemarkToolTip("+Eval("document_code").ToString() +",null,event);DisplayActionInHeader(\"All Remarks; Click to add new\",\"rgdNewREQ\");" %>'
                                                            onmouseout="CloseRemarkToolTip();" title="All Remarks; Click to add new" />
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgAddItm" runat="server" Text="Select" OnCommand="onAddNewItem"
                                                            Visible='<%# objUA.Edit != 0?true:false  %>' CommandName="onAddNewItem" CommandArgument='<%#Eval("REQUISITION_CODE")+"&VCode="+Eval("Vessel_Code")+"&ReqStage=NRQ"%>'
                                                            ForeColor="Black" ToolTip="Add Items from Office side" ImageUrl="~/purchase/Image/briefcase-add.gif"
                                                            Width="14px" Height="12px" onmouseover="DisplayActionInHeader('Add Items from Office side','rgdNewREQ')">
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="btnOnHold" runat="server" Width="14px" Height="12px" Text="Hold"
                                                            Visible='<%# objUA.Edit != 0?true:false  %>' ImageUrl="~/purchase/Image/OnHold.gif"
                                                            OnClick="btnOnHold_Click" OnCommand="OnHold" CommandName="OnHold" onmouseover="DisplayActionInHeader('Put on Hold','rgdNewREQ')"
                                                            CommandArgument='<%#Eval("REQUISITION_CODE")+","+Eval("document_code") +","+Eval("Vessel_Code")+","+Eval("code")+","+Eval("OnHold")%>' />
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="imgbtnCancel" Visible="false" AlternateText="cancelReqsn" OnClick="imgbtnCancel_Click"
                                                            ImageUrl="~/Images/Close.gif" onmouseover="DisplayActionInHeader('Cancel Requisition','rgdNewREQ')"
                                                            ToolTip="Cancel Requisition" runat="server" CommandArgument='<%#Eval("REQUISITION_CODE") +","+Eval("document_code")+","+Eval("Vessel_Code") %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings>
                                    <PopUpSettings ScrollBars="Vertical"></PopUpSettings>
                                </EditFormSettings>
                            </MasterTableView>
                            <ClientSettings>
                                <Scrolling AllowScroll="false" SaveScrollPosition="true" />
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
            <div id="divOnHold" style="border: 1px solid Black; position: fixed; left: 35%; top: 30%;
                color: black;" runat="server">
                <Hold:ucPurcReqsnHold_UnHold ID="HoldUnHold" runat="server" OnCancelClick="btndivCancel_Click"
                    OnSaveClick="btndivSave_Click" />
            </div>
            <div id="dvCancelReq" style="display: block; position: fixed; left: 15%; top: 20%"
                runat="server">
                <uc1:ucPurcCancelReqsn ID="ucPurcCancelReqsnNew" runat="server" />
            </div>
            <asp:HiddenField ID="HiddenArgument" runat="server" Value="" />
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
                                <textarea id="txtRemark1" cols="40" rows="5" style="width: 490px; height: 60px"></textarea>
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
