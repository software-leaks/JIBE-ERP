<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PendingPOGrid.aspx.cs" Inherits="Technical_INV_PendingPOGrid" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"    TagPrefix="ucDDL" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucReqsncancelLog.ascx" TagName="ucReqsncancelLog"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucPurcReqsnHold_UnHold.ascx" TagName="ucPurcReqsnHold_UnHold"
    TagPrefix="Hold" %>
<%@ Register Src="../UserControl/ucPurc_Rollback_Reqsn.ascx" TagName="ucPurc_Rollback_Reqsn"
    TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page </title>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
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
    <script language="javascript" type="text/javascript">

        function Open_RaisePO(url, hold) {

            if (hold == "False") {
                window.open(url,"name", "");
            }
            else
                alert("This requisition has been marked as OnHold.");
        }

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
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table cellpadding="0" cellspacing="0" width="100%" style="font-size: 11px">
         <tr>
                                                <td align="right" valign="middle" width="50%" style="padding:0px 20px 0px 0px">
                                                    Delivery Port
                                                </td>
                                                <td align="left" width="50%" style="height: 30px; width: 170px;">
                                                    <ucDDL:ucCustomDropDownList ID="DDLPort" Height="150" runat="server"  OnApplySearch="DDLPort_SelectedIndexChanged" UseInHeader="false"/>
                                                </td>
                                               
                                               
                                            </tr>
        </table>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center">
                        <telerik:RadGrid ID="rgdPO" runat="server" Width="100%" ShowStatusBar="True" HeaderStyle-HorizontalAlign="Center"
                            AlternatingItemStyle-BackColor="#CEE3F6" Skin="Office2007" AutoGenerateColumns="False"
                            AllowSorting="True" AllowPaging="false" GridLines="None" OnItemDataBound="rgdPO_ItemDataBound"
                            OnDataBound="rgdPO_DataBound" OnNeedDataSource="rgd_NeedDataSource" OnSortCommand="rgdPO_SortCommand">
                            <PagerStyle Mode="NextPrevAndNumeric" CssClass="Pager" Font-Size="12px" Font-Names="verdana"
                                HorizontalAlign="Right"></PagerStyle>
                            <MasterTableView DataKeyNames="REQUISITION_CODE,document_code,Vessel_Code" Width="100%"
                                AllowPaging="false">
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
                                    <telerik:GridTemplateColumn SortExpression="REQUISITION_CODE" AllowFiltering="true"
                                        HeaderText="Requisition Number" UniqueName="REQUISITION_CODE">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgPriority" runat="server" ToolTip="Urgent" ImageUrl="~/Images/exclamation.gif"
                                                Height="12px"></asp:ImageButton>
                                                 <asp:HyperLink ID="hlinkReq" runat="server" Target="_blank"
                                            NavigateUrl='<%#String.Format("RequisitionSummary.aspx?REQUISITION_CODE={0}&Document_Code={1}&Vessel_Code={2}&Dept_Code={3}&hold={4}", Eval("REQUISITION_CODE"), Eval("document_code"),Eval("Vessel_Code"),Eval("code"),Eval("OnHold"))%>'
                                            Text='<%#Eval("REQUISITION_CODE") %>'> </asp:HyperLink>
                                            <asp:Label ID="lblCriticalFlag" runat="server" Text='<%#Eval("Critical_Flag") %>'  style="display:none"></asp:Label>
                                          <%--  <a href="RequisitionSummary.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")+"&Dept_Code="+Eval("code")+"&"+Eval("OnHold")%>"
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "REQUISITION_CODE")%></a>--%>
                                        </ItemTemplate>
                                        <ItemStyle />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn SortExpression="Name_Dept" HeaderText="Department /Function" DataField="Name_Dept"
                                        AllowSorting="true" UniqueName="Name_Dept">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn UniqueName="SYSTEM_Description" SortExpression="SYSTEM_Description"
                                        DataField="SYSTEM_Description" HeaderText="Catalogue/System">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
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
                                    <telerik:GridBoundColumn SortExpression="Lead_Time" HeaderText="Lead Time" DataField="Lead_Time"
                                        AllowSorting="false" UniqueName="Lead_Time">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridTemplateColumn  AllowFiltering="true"
                                        HeaderText="Supplier" UniqueName="SHORT_NAME">
                                        <ItemTemplate>
                                          <%# DataBinder.Eval(Container.DataItem, "SHORT_NAME")%></a>
                                        </ItemTemplate>
                                        <ItemStyle />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left"  Width="15%"/>
                                    </telerik:GridTemplateColumn>       
                                    <telerik:GridTemplateColumn  AllowFiltering="true"
                                        HeaderText="Requested Supply Date" UniqueName="DELIVERY_DATE">
                                        <ItemTemplate>
                                          <%# DataBinder.Eval(Container.DataItem, "DELIVERY_DATE")%></a>
                                        </ItemTemplate>
                                        <ItemStyle />
                                        <HeaderStyle HorizontalAlign="Left"  Width="10%"/>
                                        <ItemStyle HorizontalAlign="Left"  Width="5%"/>
                                    </telerik:GridTemplateColumn>    
                                    <telerik:GridTemplateColumn  AllowFiltering="true"
                                        HeaderText="Delivery Port" UniqueName="PORT_NAME">
                                        <ItemTemplate>
                                          <%# DataBinder.Eval(Container.DataItem, "PORT_NAME")%></a>
                                        </ItemTemplate>
                                        <ItemStyle />
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        <ItemStyle HorizontalAlign="Left"  Width="5%"/>
                                    </telerik:GridTemplateColumn>                                
                                    <telerik:GridBoundColumn SortExpression="URGENCY_CODE" HeaderText="Urgency" DataField="URGENCY_CODE"
                                        AllowSorting="false" UniqueName="URGENCY_CODE" Visible="false">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Actions">
                                        <HeaderTemplate>
                                        <HeaderStyle width="200px" HorizontalAlign="Left"/>
                                            <table  width="200px">
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
                                            <table cellpadding="1" cellspacing="0">
                                                <tr>
                                                    <td style="border-color: transparent">
                                                       
                                                        <a  <%# "style='"+ (objUA.Edit != 0?"display:block":"display:none")+"'"  %> onclick="Open_RaisePO('ApprovedPurchaseOrder.aspx?Requisitioncode=<%# Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")+"&hold="+Eval("OnHold")+"&Type=RPO','"+Eval("OnHold")+"')"%>"
                                                            target="_blank">
                                                            <img src="Image/view.gif" style="border: 0px" title="Raise PO" alt="raise po" />
                                                        </a>

                                                    </td>
                                                    <td style="border-color: transparent">
                                                     <a style="border: 0px solid white" href='<%#"QuotationEvalRpt.aspx?Requisitioncode=" + Eval("REQUISITION_CODE").ToString() + "&Document_Code=" + Eval("document_code").ToString() + "&Vessel_Code=" + Eval("Vessel_Code").ToString() %>'
                                                          target="_blank">
                                                       <%-- <a style="border: 0px solid white" href='<%#"Quotation_Evaluation_Report.aspx?Requisitioncode=" + Eval("REQUISITION_CODE").ToString() + "&Document_Code=" + Eval("document_code").ToString() + "&Vessel_Code=" + Eval("Vessel_Code").ToString() %>'
                                                            target="_blank">--%>
                                                            <img src="../Images/compare.gif" style="border: 0px solid white; height: 18px; width: 18px"
                                                                title="View Evaluation" onmouseover="DisplayActionInHeader('View Evaluation' ,'rgdPO')" />
                                                        </a>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <a href="QuotationSummary.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("Document_Code") +"&Vessel_Code="+Eval("Vessel_Code")+"&QUOTATION_CODE=0"%>"
                                                            target="_blank" onmouseover="DisplayActionInHeader('View Quotations' ,'rgdDeliveryStatus')">
                                                            <img src="../Images/QtnsSummary.png" style="border: 0px" title="View Quotations" />
                                                        </a>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgAttachment" Height="20px" Width="20px" ImageUrl="../Images/attachment.png" runat="server" style="border: 0px solid white"
                                                        
                                                        onclick='<%#"ShowReqsnAttachment(&#39;Requisition_code=" + Eval("REQUISITION_CODE").ToString()+ "&Vessel_ID=" + Eval("Vessel_Code").ToString()+"&#39;)"%>' >
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <img id="imgbtnPurchaserRemark" src="../Images/remark_new.gif" onclick='<%# "GetRemarkAll("+Eval("document_code").ToString()+","+Session["userid"].ToString()+",null,event)" %>'
                                                            onmouseover='<%#"GetRemarkToolTip("+Eval("document_code").ToString() +",null,event);DisplayActionInHeader(\"All Remarks; Click to add new\",\"rgdPO\");" %>'
                                                            onmouseout="CloseRemarkToolTip();" alt="remark" />
                                                    </td>
                                                    <td style="border-color: transparent; width: 20px">
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="BtnCancelReqStage" runat="server" Text="Select" OnCommand="OnCancelReq"
                                                            CommandName="OnCancelReq"  CommandArgument='<%#Eval("REQUISITION_CODE")+","+Eval("document_code") +","+Eval("Vessel_Code")+","+Eval("code")+","+Eval("OnHold")+","+Eval("QUOTATION_CODE")%>'
                                                            ForeColor="Black" ToolTip="Rollback" ImageUrl="~/purchase/Image/Cancel_New.gif"
                                                            Width="16px" Height="16px" onmouseover="DisplayActionInHeader('Rollback' ,'rgdPO')"
                                                            Visible='<%# objUA.Edit != 0?true:false%>' CssClass="btnCancelPO_Click" Document_Code='<%#Eval("document_code")%>'
                                                            reqcode='<%#Eval("REQUISITION_CODE") %>' Quotation_code='<%#Eval("Quotation_code") %>'
                                                            vesselcode='<%#Eval("Vessel_Code") %>' ordercode='<%#Eval("ORDER_CODE") %>'></asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="btnOnHold" runat="server" Text="Hold" ImageUrl="~/purchase/Image/OnHold.png"
                                                            Width="16px" Height="16px" OnClick="btnOnHold_Click" OnCommand="OnHold" CommandName="OnHold"
                                                            CommandArgument='<%#Eval("REQUISITION_CODE")+","+Eval("document_code") +","+Eval("Vessel_Code")+","+Eval("code")+","+Eval("OnHold")%>'
                                                            onmouseover="DisplayActionInHeader('Put on Hold' ,'rgdPO')" Visible='<%# objUA.Edit != 0?true:false  %>' />
                                                    </td>
                                                    <td style="border-color: transparent; display:none">
                                                        <asp:ImageButton ID="ImgAddItm" runat="server" Text="Select" OnCommand="onAddNewItem"
                                                            CommandName="onAddNewItem" CommandArgument='<%#Eval("REQUISITION_CODE")+"&VCode="+Eval("Vessel_Code")+ "&sOrderCode="+Eval("ORDER_CODE")+"&ReqStage=APR"%>'
                                                            ForeColor="Black" ToolTip="Add Items from Office side" ImageUrl="~/purchase/Image/briefcase-add.gif"
                                                            Width="16px" Height="16px" onmouseover="DisplayActionInHeader('Add Items from Office side' ,'rgdPO')"
                                                            Visible="false" ></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn SortExpression="Attach_Status" Visible="false" HeaderText="Attach_Status"
                                        DataField="Attach_Status" UniqueName="Attach_Status">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="document_code" Visible="false" HeaderText="Document Code"
                                        DataField="document_code" UniqueName="document_code">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="Vessel_Code" Visible="false" HeaderText="Vessel_Code"
                                        DataField="Vessel_Code" UniqueName="Vessel_Code">
                                        <HeaderStyle Width="0px" />
                                        <ItemStyle Width="0px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="code" Visible="false" HeaderText="code"
                                        DataField="code" UniqueName="code">
                                        <HeaderStyle Width="0px" />
                                        <ItemStyle Width="0px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn SortExpression="OnHold" Visible="false" HeaderText="OnHold"
                                        DataField="OnHold" UniqueName="OnHold">
                                        <HeaderStyle />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings>
                                    <PopUpSettings ScrollBars="None"></PopUpSettings>
                                </EditFormSettings>
                            </MasterTableView>
                            <ClientSettings>
                                <Scrolling AllowScroll="false" UseStaticHeaders="false" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td align="center">
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
                top: 22%; z-index: 2; color: black; height: auto; width: 495px;" class="popup-css"
                runat="server">
              
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
      <div id="divBuyerRemark" style="border: 1px solid gray; z-index: 501; color: Black;
        display: none; position: fixed; left: 400px; top: 100px" class="Tooltip-css">
    </div>
    <div style="display:none">
    <iframe id="Iframe" clientidmode="Static" runat="server"></iframe>
    </div>
    </form>
</body>
</html>
