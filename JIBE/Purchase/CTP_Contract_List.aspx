<%@ Page Title="List of Contract" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CTP_Contract_List.aspx.cs" Inherits="Purchase_CTP_Contract_List" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/uc_SupplierList.ascx" TagName="uc_SupplierList"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/uc_Purc_DepartmentList.ascx" TagName="uc_Purc_DepartmentList"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControl/ctlPortList.ascx" TagName="ctlPortList" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="ucpager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            width: 16%;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 0px 2px 0px 3px;
            height: 20px;
            vertical-align: middle;
            border-right: 1px solid gray;
        }
    </style>
    <script type="text/javascript">
        function ShowdvImportQtn(id, evt) {

            var QuotationID = document.getElementById(id).alt;
            var hdfQuotationID = document.getElementById("<%=hdf_QuotationID.ClientID%>");
            hdfQuotationID.value = QuotationID;

            var divunit = document.getElementById("dvImportQtn");
            divunit.style.display = "block";
            divunit.style.left = (evt.clientX - 420) + "px";
            divunit.style.top = (evt.clientY + 7) + "px";

        }

        function DisplayActionInHeader(ctlToolTips, evt, objthis) {

            if (ctlToolTips) {

                var divMsg_Obj = document.getElementById("__divMsg");

                if (divMsg_Obj == null) {

                    var __divMsg = document.createElement("div");
                    __divMsg.setAttribute("id", "__divMsg");
                    __divMsg.setAttribute("style", "background-color:yellow;z-index:1000;display:block;position:absolute;color:black;padding:3px;border: 1px solid gray; font-size:11px");
                    __divMsg.style.left = (evt.clientX - 1) + "px";
                    __divMsg.style.top = (evt.clientY - 28) + "px";
                    __divMsg.innerHTML = ctlToolTips;
                    document.body.appendChild(__divMsg);

                }
                else {

                    divMsg_Obj.style.display = "block";
                    divMsg_Obj.innerHTML = ctlToolTips;
                    divMsg_Obj.style.left = (evt.clientX - 1) + "px";
                    divMsg_Obj.style.top = (evt.clientY - 28) + "px";

                }


                document.getElementById(objthis.id).setAttribute("onmouseout", "HideActionInHeader()");
            }

        }

        function HideActionInHeader() {
            document.getElementById("__divMsg").style.display = "none";
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
      List Of Contracts  
    </div>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updCTPMain" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <table width="100%" cellpadding="1" cellspacing="0" style="border-collapse: collapse;
                color: Black; font-size: 11px; font-family: Verdana; border: 1px solid gray">
                <tr>
                    <td class="tdh">
                        Supplier :
                    </td>
                    <td class="tdd">
                        <asp:UpdatePanel ID="upd_uc_SupplierListCTP" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lblsupp" runat="server">
                                    <uc1:uc_SupplierList ID="uc_SupplierListCTP" Width="200px" runat="server" />
                                </asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="tdh">
                        Department :
                    </td>
                    <td class="tdd">
                        <asp:UpdatePanel ID="upd_uc_Purc_DepartmentListctp" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <uc2:uc_Purc_DepartmentList ID="uc_Purc_DepartmentListctp" Width="150px" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td rowspan="2" valign="top" style="border: 1px solid gray; border-top: 1px solid gray">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="2" style="border-bottom: 1px solid gray;">
                                    Effective Date :
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 3px; text-align: right; font-weight: bold">
                                    From :
                                </td>
                                <td style="padding-top: 3px; text-align: left">
                                    <asp:TextBox ID="txtEffdtFrom" Width="75px" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendartxtEffdtFrom" TargetControlID="txtEffdtFrom" Format="dd/MM/yyyy"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 3px; text-align: right; font-weight: bold">
                                    To :
                                </td>
                                <td style="padding-right: 3px; padding-top: 3px; text-align: left">
                                    <asp:TextBox ID="txtEffdtTo" Width="75px" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendartxtEffdtTo" Format="dd/MM/yyyy" TargetControlID="txtEffdtTo"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td rowspan="2" class="tdd" align="left">
                        <asp:RadioButtonList ID="chkContractStatus" Width="80px" runat="server" RepeatDirection="Vertical"
                            ValidationGroup="aa">
                            <asp:ListItem Text="Current" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="History" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="left" rowspan="2" class="tdd">
                        Status:
                        <br />
                        <asp:RadioButtonList ID="chkQtnStatus" runat="server" RepeatDirection="Vertical"
                            RepeatColumns="2" RepeatLayout="Table" CellPadding="1" Width="200px">
                            <asp:ListItem Text="Sent" Value="S"></asp:ListItem>
                            <asp:ListItem Text="Not sent" Value="NS"></asp:ListItem>
                            <asp:ListItem Text="in proccess" Value="SD"></asp:ListItem>
                            <asp:ListItem Text="Finalized" Value="FZ"></asp:ListItem>
                            <asp:ListItem Text="Reworked" Value="RW"></asp:ListItem>
                            <asp:ListItem Text="Approved" Value="AP"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td rowspan="2" style="text-align: center; border-right: 1px solid gray" class="tdh">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Size="12px" Height="30px"
                            OnClick="btnSearch_Click" Width="80px" /><br />
                        <asp:Button ID="btnClearFilter" runat="server" Text="Clear Filter" Font-Size="12px"
                            Height="30px" OnClick="btnClearFilter_Click" Width="80px" />
                    </td>
                    <td class="tdh">
                        <a href="CTP_New_Contract.aspx" id="hlnkCreateNewContract" runat="server" target="_blank">
                            New Contract</a>
                    </td>
                </tr>
                <tr>
                    <td class="tdh">
                        Port :
                    </td>
                    <td class="tdd">
                        <asp:UpdatePanel ID="udp_ctlPortListctp" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <uc3:ctlPortList ID="ctlPortListctp" Width="200px" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="tdh">
                        Item Desc /code :
                    </td>
                    <td class="tdd">
                        <asp:TextBox ID="txtItemSearch" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdh" style="text-align: center">
                        <asp:Button ID="btnCompare" runat="server" Font-Size="11px" Text="Compare" OnClick="btnCompare_Click"
                           
                            Width="80px" />
                        <br />
                        <asp:Button ID="btnExport" ToolTip="Export to excel" Text="Export" Font-Size="11px"
                            OnClick="btnExport_Click" runat="server" Width="80px" />
                    </td>
                </tr>
            </table>
            <table width="100%" style="border: 1px solid gray; border-top: 0px solid gray">
                <tr>
                    <td>
                        <asp:GridView ID="gvContractList" runat="server" AutoGenerateColumns="false" DataKeyNames="Quotation_ID"
                            Width="100%" EmptyDataText="No record found !" EmptyDataRowStyle-ForeColor="Maroon"
                            BackColor="#D8D8D8" OnRowCreated="gvContractList_RowCreated" CellPadding="3"
                            ShowFooter="true" CssClass="gridmain-css" GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="QTN_Contract_Code" HeaderText="Contract Code" />
                                <asp:BoundField HeaderText="Effective Date" DataField="Effective_Date" />
                                <asp:BoundField HeaderText="Supplier" DataField="Full_NAME" />
                                <asp:BoundField HeaderText="Port" DataField="PORT_NAME" />
                                <asp:BoundField HeaderText="Department" DataField="Dept_Name" />
                                <asp:BoundField HeaderText="Catalogue" DataField="System_Description" />
                                <asp:BoundField HeaderText="Approved" DataField="APPROVED_ITEM_COUNT" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="InProcess" DataField="NOT_APPROVED_ITEM_COUNT" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Approved By" DataField="First_Name" />
                                <asp:BoundField HeaderText="Approved On" DataField="Approved_Date" />
                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSts" Width="100%" Height="100%" Text='<%#Eval("QTN_STS") %>' BackColor='<%#Eval("QTN_STS").ToString()=="Expired"?System.Drawing.Color.Red:System.Drawing.Color.Transparent %>'
                                            ForeColor='<%#Eval("QTN_STS").ToString()=="Expired"?System.Drawing.Color.White:System.Drawing.Color.Black %>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCompare" Visible='<%# ((string.IsNullOrWhiteSpace(Convert.ToString(Session["SUPPCODE"]))==true) && (Eval("Quotation_Status").ToString()=="FZ"  || Eval("Quotation_Status").ToString()=="AP") )?true:false%>'
                                            runat="server" ToolTip='<%#Eval("quotation_id")+"_"+Eval("System_Code") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <table cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 50px; border: 0; padding-right: 2px">
                                                    <asp:HyperLink ID="hlnkViewDetails" runat="server" NavigateUrl='<%# "ctp_contract_details.aspx?Quotation_ID="+Eval("Quotation_ID")+"&supplier_code="+Convert.ToString(Session["SUPPCODE"])+"&Contract_ID="+Eval("Contract_ID") %>'
                                                        onmouseover="DisplayActionInHeader('View Contract details' ,event,this)" Target="_blank"
                                                        ForeColor="Blue" Height="16px" Text="View"></asp:HyperLink>&nbsp;
                                                </td>
                                                <td style="width: 50px; border: 0; padding-right: 2px">
                                                    <asp:HyperLink ID="hlnkSendRFQ" NavigateUrl="#" BorderWidth="0px" BorderColor="Transparent"
                                                        Height="16px" ForeColor="Blue" ImageUrl="~/Purchase/Image/SendRFQ.png" Visible='<%# string.IsNullOrWhiteSpace(Convert.ToString(Session["SUPPCODE"])) == true && objUA.Edit!=0 ?true:false%>'
                                                        runat="server" onclick='<%#"OpenPopupWindowBtnID(&#39;POP__IDRFQ&#39;, &#39;Send RFQ&#39;, &#39;CTP_SendRFQ_PopUp.aspx?Contract_ID="+Eval("Contract_ID").ToString()+"&#39;,&#39;popup&#39;,600,1000,250,100,true,true,true,false,&#39;"+btnSearch.ClientID+"&#39;); return false;" %>'
                                                        onmouseover="DisplayActionInHeader('Send new RFQ' ,event,this)" Text="Send RFQ" />&nbsp;
                                                </td>
                                                <td style="width: 50px; border: 0; padding-right: 2px">
                                                    <asp:ImageButton ID="imgbtnImportQtn" ImageAlign="Baseline" runat="server" Text="Excel"
                                                        ImageUrl="~/Purchase/Image/import.png" Height="16px" AlternateText='<%#Eval("Quotation_ID")%>'
                                                        Visible='<%#( Eval("Quotation_ID").ToString() == "0" || Eval("Quotation_Status").ToString()=="FZ"  || Eval("Quotation_Status").ToString()=="AP"  || string.IsNullOrWhiteSpace(Convert.ToString(Session["SUPPCODE"]))==false )  || objUA.Edit==0 ?false:true%>'
                                                        onmouseover="DisplayActionInHeader('Import quotation' ,event,this)" OnClientClick="javascript:ShowdvImportQtn(id,event);return false;" />&nbsp;
                                                </td>
                                                <td style="width: 50px; border: 0; padding-right: 2px">
                                                    <asp:ImageButton ID="btnWebRFQ" ImageAlign="Baseline" runat="server" Text="Excel"
                                                        CommandArgument='<%#Eval("Quotation_ID")%>' Height="16px" onmouseover="DisplayActionInHeader('Re send Web RFQ' ,event,this)"
                                                        ImageUrl="~/Purchase/Image/webRFQ.png" OnClick="btnWebRFQ_Click" Visible='<%#( Eval("Quotation_ID").ToString() == "0"  || string.IsNullOrWhiteSpace(Convert.ToString(Session["SUPPCODE"])) ==false)  || objUA.Edit==0 ?false:true%>' />&nbsp;
                                                </td>
                                                <td style="width: 50px; border: 0; padding: 0px 3px 0px 3px">
                                                    <asp:ImageButton ID="btnExportEcelRFQ" ImageAlign="Baseline" runat="server" Height="16px"
                                                        Text="exl" CommandArgument='<%#Eval("Quotation_ID")%>' onmouseover="DisplayActionInHeader('Re send excel RFQ' ,event,this)"
                                                        ImageUrl="~/Purchase/Image/Excel-2010.png" OnClick="btnExportEcelRFQ_Click" Visible='<%#( Eval("Quotation_ID").ToString() == "0" || string.IsNullOrWhiteSpace(Convert.ToString(Session["SUPPCODE"]))==false) || objUA.Edit==0 ?false:true%>' />&nbsp;
                                                </td>
                                                <td style="width: 50px; border: 0; padding: 0px 3px 0px 3px">
                                                    <asp:HyperLink ID="hlnkCopyContract" runat="server" NavigateUrl='<%# "CTP_Copy_Contract.aspx?Quotation_ID="+Eval("Quotation_ID")+"&supplier_code="+Convert.ToString(Session["SUPPCODE"])+"&Contract_ID="+Eval("Contract_ID") %>'
                                                        onmouseover="DisplayActionInHeader('Create contract for other department/catalogue based on this contract' ,event,this)"
                                                        Visible='<%#( string.IsNullOrWhiteSpace(Convert.ToString(Session["SUPPCODE"])) ==false || Eval("Quotation_Status").ToString()!="AP") || objUA.Edit==0 ?false:true%>'
                                                        Target="_blank" ForeColor="Blue" Height="16px" ImageUrl="~/images/Copy_Contract.png"
                                                        Text="t"></asp:HyperLink>&nbsp;
                                                </td>
                                                <td style="width: 50px; border: 0; padding-left: 4px">
                                                    <asp:ImageButton ID="imgDeleteCTP" ImageAlign="Baseline" runat="server" OnClientClick="var a =confirm('are you sure to delete ?'); if(a) return true;else return false;"
                                                        Height="16px" Text="Delete" CommandArgument='<%#Eval("Quotation_ID").ToString()+&#39;,&#39;+Eval("Contract_ID").ToString()%>'
                                                        onmouseover="DisplayActionInHeader('Delete this contract' ,event,this)" ImageUrl="~/Images/Delete.png"
                                                        OnClick="imgDeleteCTP_Click" Visible='<%#(string.IsNullOrWhiteSpace(Convert.ToString(Session["SUPPCODE"]))==false) || objUA.Delete==0 ?false:true%>' />&nbsp;
                                                </td>
                                                <td style="border: 0; padding-left: 4px">
                                                    <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                        Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;PURC_DTL_CTP_Quotation&#39;,&#39; Quotation_ID="+Eval("Quotation_ID")+" and Contract_ID="+Eval("Contract_ID")+"&#39;,event,this)" %>'
                                                        AlternateText="info" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                        </asp:GridView>
                        <ucpager:ucCustomPager ID="ucCustomPagerctp" OnBindDataItem="BindDataItems" AlwaysGetRecordsCount="true"
                            runat="server" />
                        <asp:Label ID="lblmsg" ForeColor="Red" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdf_QuotationID" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvImportQtn" style="display: none; position: fixed; left: 25%; top: 21%;
        padding: 30px; border: 1px solid gray; z-index: 310; background-color: Teal">
        <asp:FileUpload ID="FileUpload1" runat="server" Height="24px" Style="font-size: small" />
        <asp:Button ID="btnUpload" runat="server" Text="Upload" Style="font-size: small"
            OnClick="btnUpload_Click" />
        <input type="button" value="Close" onclick="javascript:document.getElementById('dvImportQtn').style.display = 'none';return false;" />
    </div>
</asp:Content>
