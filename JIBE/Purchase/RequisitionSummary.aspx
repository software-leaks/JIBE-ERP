<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RequisitionSummary.aspx.cs"
    Inherits="Technical_INV_RequisitionSummary" Title="Requisition Details" %>

<%--<%@ Register Src="../UserControl/ucPurcAttachment.ascx" TagName="ucPurcAttachment"
    TagPrefix="uc1" %>--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControl/ucPurcCancelReqsn.ascx" TagName="ucPurcCancelReqsn"
    TagPrefix="CanReq" %>
<%@ Register Src="../UserControl/ucPurcReqsnHold_UnHold.ascx" TagName="ucPurcReqsnHold_UnHold"
    TagPrefix="Hold" %>
<asp:Content ID="HeadCont" runat="server" ContentPlaceHolderID="HeadContent">
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
        function DisplayTypeLog() {

            $('#dvreqsType').attr("Title", "Reqsn type change log");
            showModal('dvreqsType', true);
            return false;
        }
        function fn_OnClose() {
            $('[id$=btnLoadFiles]').trigger('click');
            //__doPostBack('ctl00_MainContent_btnLoadFiles', true);            
        }
        function OpenQuestionAnswers(DocCode) {

            $('#dvQuestions').attr("Title", "Add/View Question");
            $('#dvQuestions').css({ "width": "900px", "height": "800px", "text-allign": "center" });
            $('#dvQuestions').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });
            var URL = "View_QuestionAnswers.aspx?DocumentCode=" + DocCode + "&Mode=View&rnd=" + Math.random();
            document.getElementById("frPopupFrame").src = URL;
            showModal('dvQuestions', true);
        }

        function OpenAssignWorkList(vid, DocCode, offid) {
            $('#dvWorklist').attr("Title", "Add/View Worklist");
            $('#dvWorklist').css({ "width": "900px", "height": "800px", "text-allign": "center" });
            $('#dvWorklist').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });
            var URL = "AssignWorklist.aspx?DocumentCode=" + DocCode + "&VID=" + vid + "&OFFID=" + offid + "&Mode=View&rnd=" + Math.random();
            document.getElementById("frPopupFrame1").src = URL;
            showModal('dvWorklist', true);
        }
        function CheckUnCheckAll(chk) {
            $('#<%=gvReqsnItems.ClientID %>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });
        }
        function DisplayAccountType() {

            $('#dvAccountType').attr("Title", "Change Account Type:");
            showModal('dvAccountType', true);
            return false;
        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows

                        inputList[i].checked = true;
                        chkNA_Changed(inputList[i]);

                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original

                        inputList[i].checked = false;
                        chkNA_Changed(inputList[i]);

                    }
                }
            }
        }



        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;


            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }
        var chkArray = [];
        var unchekArray = [];
        var chekList = "";
        var unchekList = "";

        function chkNA_Changed(obj) {

            var remark = $(obj.id.replace('chkSupplier', 'hdnref'));
            var i = 0;
            if (obj.checked == true) {
                var index = chkArray.indexOf(document.getElementById(remark.selector).value);
                if (index >= 0) {
                    chkArray.splice(index, 1);
                }
                chkArray.push(document.getElementById(remark.selector).value);
            }
            else {
                var index2 = unchekArray.indexOf(document.getElementById(remark.selector).value);
                if (index2 >= 0) {
                    unchekArray.splice(index2, 1);
                }
                unchekArray.push(document.getElementById(remark.selector).value);
            }
            for (var r = 0; r < chkArray.length; r++) {
                chekList = chekList + "," + chkArray[r];

            }
            for (var j = 0; j < unchekArray.length; j++) {
                unchekList = unchekList + "," + unchekArray[j];

            }
            document.getElementById('ctl00_MainContent_hdnSelectedCde').value = chekList;
            document.getElementById('ctl00_MainContent_hdnNotselect').value = unchekList;
        }
     
    </script>
    <style type="text/css">
        .fieldset-css
        {
            margin: 0px 0px 0px 0px;
            padding: 10px 0px 10px 0px;
            border: 1px solid #CEE3F6;
        }
        
        .legend-css
        {
            margin: 0px 0px 0px 0px;
            padding: 2px 0px 2px 0px;
            font-size: 12px;
            font-weight: bold;
            font-family: Verdana;
            background-color: #CEE3F6; /*filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#0B615E',EndColorStr='#088A85');*/
            color: #2E2E2E;
            width: 150px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function OpenScreen(ID, Job_ID) {
            var ReqCode = document.getElementById("ctl00_MainContent_txtReqCode").value;
            var VesselID = document.getElementById("ctl00_MainContent_txtVessselCode").value;
            var Catalogue_Code = document.getElementById("ctl00_MainContent_txtCatalogueCode").value;
            var url = 'MachineryDetails.aspx?Requisition_code=' + ReqCode + '&Vessel_ID=' + VesselID + '&Catalogue_Code=' + Catalogue_Code;
            OpenPopupWindowBtnID('MachineryDetails_ID', 'Machinery Details', url, 'popup', 420, 1000, null, null, false, false, true, null, null);
        }
//        function OpenDuplicateScreen(ID, Job_ID) {
//            var ReqCode = document.getElementById("ctl00_MainContent_txtReqCode").value;
//            var VesselID = document.getElementById("ctl00_MainContent_txtVessselCode").value;
//            var DocCode = document.getElementById("ctl00_MainContent_txtDocumentCode").value;
//            var url = 'DuplicateRequisition.aspx?Requisition_code=' + ReqCode + '&Vessel_ID=' + VesselID + '&Document_Code=' + DocCode;
//            OpenPopupWindowBtnID('DuplicateReqisition', 'Duplicate Requisition', url, 'popup', 420, 500, null, null, false, false, true, null, null);
//        }
    </script>
    <style type="text/css">
        body, html
        {
            overflow-x: hidden;
        }
        
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        
        .page
        {
            width: 100%;
        }
        td.blog_content div
        {
            width: 100%;
            text-align: left;
            padding: 2px;
        }
        
        .HeaderStyle-center
        {
            background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #333333;
            font-size: 11px;
            padding: 5px;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #959EAF;
            border-collapse: collapse;
        }
        .divclass
        {
            width: 100%;
            background-color: #BDBDBD;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
            color: White;
        }
        .divclass1
        {
            background-color: #fff;
            border: 0.5px solid #496077;
        }
        .checkbxinput
        {
            color: White;
            background-color: green;
            width: 50px;
            height: 20px;
        }
        .tbl
        {
            border: 1px solid gray;
            height: 50px;
        }
        
        .view-header
        {
            font-size: 14px;
            font-family: Calibri;
            font-weight: bold;
            text-align: left;
            width: 100%;
            color: Black;
            background-color: #81DAF5;
            border-collapse: collapse;
            padding: 2px 0px 2px 3px;
        }
        
        .tbl-content
        {
            border: 1px solid #81DAF5;
            width: 100%;
            border-collapse: collapse;
        }
        
        .tbl-footer
        {
            border-bottom: 1px solid #81DAF5;
            border-left: 1px solid #81DAF5;
            border-right: 1px solid #81DAF5;
            width: 100%;
            border-collapse: collapse;
            padding: 2px 2px 2px 2px;
        }
        
        .tbl-footer-td
        {
            width: 100%;
            padding: 2px 2px 2px 2px;
            text-align: left;
            background-color: #81DAF5;
        }
        .tdh
        {
            text-align: left;
            padding: 0px 0px 0px 0px;
        }
        .tdd
        {
            text-align: left;
            padding: 0px 0px 0px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
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
    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
        height: 100%;">
        <div class="page-title">
            Requisition Details
        </div>
        <center>
            <asp:UpdatePanel ID="updReqsn" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="imgbtnExportReqsnItems" />
                </Triggers>
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td valign="middle" style="width: 15%">
                                <asp:Button ID="btnSendRFQ" Enabled="false" Text="Select Supplier" runat="server"
                                    OnClick="btnSendRFQ_Click" />
                            </td>
                            <td valign="middle" style="width: 15%">
                                <asp:Button ID="btnBulkPurchase" runat="server" OnClick="btnBulkPurchase_Click" Text="Distribute Between Vessels" />
                            </td>
                            <td valign="middle" style="width: 15%">
                                <asp:Button ID="btnHold" runat="server" OnClick="btnHold_Click" Text="Hold Requisition" />
                            </td>
                            <td valign="middle" style="width: 15%">
                                <asp:Button ID="btnCancel" Text="Cancel Requisition" Enabled="false" runat="server"
                                    OnClick="btnCancel_Click" />
                            </td>
                            <td valign="middle" style="width: 15%">
                                <asp:CheckBox ID="chkSendTovessel" Text="Send a copy to vessel" runat="server" 
                                    oncheckedchanged="chkSendTovessel_CheckedChanged" />
                                <asp:Button ID="btnSendToVessel" runat="server" Text="Send" Visible="false" OnClick="btnSendToVessel_Click" />
                            </td>
                            <td valign="middle" style="width: 15%">
                                <asp:Button ID="btnCloseRequisition" OnClick="btnCloseRequisition_Click" Text="Close Requisition"
                                    Enabled="false" Visible="false" runat="server" />
                            </td>
                            <td valign="middle" align="right" style="width: 15%">
                                <asp:ImageButton ID="imgbtnExportReqsnItems" ToolTip="Export Requisition Item" ImageUrl="~/Purchase/Image/Excel.gif"
                                    OnClick="imgbtnExportReqsnItems_Click" runat="server" />
                            </td>
                           
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="1" width="100%" style="background-color: #f4ffff;
                        color: Black">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="tbl" style="width: 100%">
                                    <tr>
                                        <td class="tdh">
                                            Requisition No:&nbsp; &nbsp;
                                            <asp:Label ID="lblReqNo" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdh">
                                            Requisition Raise date:&nbsp; &nbsp;<asp:Label ID="lblToDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table align="center" cellpadding="0" cellspacing="0" class="tbl" style="width: 100%">
                                    <tr>
                                        <td class="tdh">
                                            Delivery Port:&nbsp; &nbsp;
                                            <asp:Label ID="lblDeliveryPort" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdh">
                                            Delivery Date:&nbsp; &nbsp;<asp:Label ID="lblDeliveryDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table align="center" cellpadding="0" cellspacing="0" class="tbl" style="width: 100%">
                                    <tr>
                                        <td class="tdh">
                                            Function/Department:&nbsp; &nbsp;<asp:Label ID="lblFunction" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdh">
                                            System/Catalogue:&nbsp; &nbsp;<asp:Label ID="lblCatalog" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table align="center" cellpadding="0" cellspacing="0" class="tbl" style="width: 100%">
                                    <tr>
                                        <td class="tdh">
                                            Requisition Type:&nbsp; &nbsp;
                                            <asp:Label ID="lblReqsntype" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdh">
                                            Reason For Requistion:&nbsp; &nbsp;<asp:Label ID="lblReason" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="display: none;">
                                <asp:TextBox ID="txtReqCode" runat="server" Text="" Width="1px"></asp:TextBox>
                                <asp:TextBox ID="txtVessselCode" runat="server" Text="" Width="1px"></asp:TextBox>
                                <asp:TextBox ID="txtCatalogueCode" runat="server" Text="" Width="1px"></asp:TextBox>
                                <asp:TextBox ID="txtDocumentCode" runat="server" Text="" Width="1px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" style="background-color: #CEE3F6; height: 20px; font-size: small;
                                font-weight: 700; color: #333333;">
                                Items in Requisition
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:GridView ID="gvReqsnItems" AutoGenerateColumns="False" GridLines="None" Width="100%"
                                    BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px" CellPadding="2" CellSpacing="0"
                                    runat="server" OnRowDataBound="gvReqsnItems_RowDataBound" DataKeyNames="ItemID">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <Columns>
                                        <asp:BoundField DataField="Subsystem_Description" HeaderText="Sub Catalogue" ItemStyle-Width="7%"
                                            HeaderStyle-Width="7%" />
                                        <asp:BoundField DataField="ItemID" HeaderText="ItemID" Visible="false"></asp:BoundField>
                                        <asp:BoundField DataField="SrNo" HeaderText="Sr.No.">
                                            <ItemStyle Width="4%" HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Drawing_Number" HeaderText="Drawing No" Visible="true">
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Part_Number" HeaderText="Part No" Visible="true">
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            <HeaderStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Description" Visible="true">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblItemDesc" Width="100%" runat="server" Target="_blank" Text='<%#Eval("ItemDesc")%>'
                                                    NavigateUrl='<%# "~/Purchase/Item_History.aspx?vessel_code="+ Request.QueryString["Vessel_Code"].ToString()+"&item_ref_code="+Eval("ItemID").ToString() %>'>  </asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            <HeaderStyle Width="20%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ItemUnit" HeaderText="Unit">
                                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ROB_QTY" HeaderText="ROB" Visible="true">
                                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ReqestedQty" HeaderText="Requested Qty" Visible="true">
                                            <ItemStyle Width="7%" HorizontalAlign="Right" />
                                            <HeaderStyle Width="7%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ItemComments" HeaderText="Comments" Visible="true">
                                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            <HeaderStyle Width="20%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblshortDesc" Text='<%#Eval("ItemDesc")%>' ToolTip='<%#Eval("link")%>'
                                                    runat="server"></asp:Label>
                                                <asp:Label ID="lblLongDesc" Text='<%#Eval("Long_Description")%>' runat="server"></asp:Label>
                                                <asp:Label ID="lblComments" Text='<%#Eval("ItemComments")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="Chkall" ClientIDMode="Static" Text="Show Remarks to Supplier" Checked='<%#Convert.ToBoolean(Eval("IsShow"))%>'
                                                    runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSupplier" onclick="Check_Click(this);chkNA_Changed(this);" Checked='<%#Convert.ToBoolean(Eval("IsShow"))%>'
                                                    runat="server" />
                                                <asp:Label ID="hdnItemIDd" Style="display: none" runat="server" Text='<%#Eval("ItemID") %>' />
                                                <asp:HiddenField ID="hdnref" runat="server" Value='<%#Eval("ItemID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="7%" HorizontalAlign="Left" />
                                            <HeaderStyle Width="7%" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnSaveSupplierSetting" runat="server" Style="font-weight: bold"
                                    Text="Save Remarks Settings" align="right" ClientIDMode="Static" OnClick="btnSaveSupplierSetting_Click" />
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" style="background-color: #CEE3F6; height: 20px; font-size: small;
                                font-weight: 700; color: #333333;">
                                Purchase Questionnaire
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <div style="overflow: scroll">
                                    <asp:GridView ID="grdQuestion" runat="server" ClientIDMode="Static" HorizontalAlign="Left"
                                        Width="100%" AutoGenerateColumns="false" EmptyDataText="No Record Found!" BorderStyle="Solid"
                                        BorderColor="Gray" BorderWidth="1px" CellPadding="2" CellSpacing="0">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:BoundField DataField="SrNo" HeaderText="Sr.No.">
                                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="4%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Questions">
                                                <ItemStyle Width="7%" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="50%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuestID" Style="display: none" ClientIDMode="Static" runat="server"
                                                        Text='<%#Eval("Question_ID") %>'></asp:Label>
                                                    <asp:Label ID="lblQuestion" ClientIDMode="Static" runat="server" Text='<%#Eval("Question") %>'></asp:Label>
                                                    <asp:Label ID="lblGradeType" Style="display: none" ClientIDMode="Static" runat="server"
                                                        Text='<%#Eval("Grade_Type") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Answers">
                                                <ItemStyle Width="7%" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="50%" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDescriptive" Width="500px" ClientIDMode="Static" Visible="false"
                                                        runat="server" TextMode="MultiLine" Text='<%#Eval("Remark")%>'> </asp:TextBox>
                                                    <asp:Label ID="lblAns" Style="display: none" runat="server" ClientIDMode="Static"
                                                        Text='<%#Eval("Option_ID") %>'></asp:Label>
                                                    <asp:Label ID="lblAnsLabel" Font-Size="Small" runat="server" ClientIDMode="Static"
                                                        Text='<%#Eval("OptionText") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" width="100%" style="vertical-align: top; text-align: left">
                      <tr>
                            <td align="center" style="background-color: #CEE3F6; height: 20px; font-size: small;
                                font-weight: 700; color: #333333;">
                                Attached Files 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <asp:Repeater ID="rpAttachment" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("SlNo") %>.
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="lnkAtt" Target="_blank" runat="server" NavigateUrl='<%#"../Uploads/Purchase/"+System.IO.Path.GetFileName(Convert.ToString(Eval("File_Path")))%>'> <%# Eval("File_Name")%>  </asp:HyperLink>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align:right">
                                <asp:UpdatePanel ID="updAttach" runat="server">
                                    <ContentTemplate>
                                        <div>
                                            <%-- <uc1:ucPurcAttachment ID="ucPurcAttachment1" OnUploadCompleted="LoadFiles" runat="server" />--%>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../Images/AddAttachment.png"
                                                OnClientClick="showModal('dvPopupAddAttachment',true,fn_OnClose);" />
                                            <asp:Button ID="btnLoadFiles" OnClick="btnLoadFiles_Click" runat="server" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="2" cellspacing="1" width="100%" style="color: Black; border-color: #0000FF">
                        <tr>
                             <td align="center" style="background-color: #CEE3F6; height: 20px; font-size: small;
                                font-weight: 700; color: #333333;">
                                Order Number:
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-bottom: 5px; padding-top: 5px">
                                <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table">
                                    <ItemTemplate>
                                        <td style="padding-right: 10px">
                                            <a href="POPreview.aspx?RFQCODE=<%# Eval("REQUISITION_CODE") +"&Vessel_Code="+ Eval("Vessel_CODE")+"&Order_Code="+ Eval("ORDER_CODE") %> "
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "ORDER_CODE")%></a>
                                        </td>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td align="center" style="background-color: #CEE3F6; height: 20px; font-size: small;
                                font-weight: 700; color: #333333;">
                                Assigned Worklist Jobs
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <div>
                                    <asp:GridView ID="grdWorklistInvolved" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" CellSpacing="1" EmptyDataText="No Record Found!" GridLines="both"
                                        Width="100%" AllowSorting="false">
                                        <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                        <PagerStyle CssClass="PagerStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Woklist ID" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="7%" HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="50%" />
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lblWORKLIST_ID" runat="server" NavigateUrl='<%#"../Technical/Worklist/ViewJob.aspx?WLID=" + Eval("WORKLIST_ID") +"&OFFID="+Eval("OFFICE_ID")+"&VID="+Eval("VESSEL_ID")+"&Mode=View"%>'
                                                        Target="_blank" Text='<%# Eval("WLID_DISPLAY")%>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Job Description">
                                                <ItemStyle Width="7%" HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="50%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJobDescription" runat="server" Text='<%#Eval("LONG_JOB_DESCRIPTION").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Action">
                          <ItemTemplate>
                             <asp:ImageButton ImageUrl="~/Images/Delete-icon.png" ID="btnDelete" Text="Delete" runat="server"  CommandArgument='<%#Eval("ID")%>' CommandName="Delete" OnClick="onDelete"  Enabled='<%#Convert.ToString(ViewState["del"])=="1"?true:false %>' ToolTip="Delete" />
                           </ItemTemplate>
                           <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css" />
                           <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                          </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td valign="middle" style="width: 15%">
                                <asp:Button ID="btnReqsnLog" Text="View Requisition Log" runat="server" OnClick="btnSendRFQ_Click" />
                            </td>
                            <td valign="middle" style="width: 15%">
                                <asp:Button ID="lbtnAllremarks" runat="server" Text="All Remark" />
                            </td>
                            <td valign="middle" style="width: 15%">
                                <asp:Button ID="btnAccountType" runat="server" OnClientClick='DisplayAccountType(null,null);return false;'
                                    Text="Account Type" />
                            </td>
                            <td valign="middle" style="width: 15%">
                                <asp:Button ID="btnVessel" Text="Vessel/Machinery/Maker" runat="server" OnClientClick='OpenScreen(null,null);return false;' />
                            </td>
                        </tr>
                    </table>
                    <table id="CrwDetails" runat="server" visible="false" cellpadding="0" cellspacing="0"
                        style="width: 100%">
                        <tr>
                            <td align="left">
                                <asp:Panel ID="Panel5" runat="server" Style="font-size: 11px">
                                    <fieldset class="fieldset-css">
                                        <legend class="legend-css">Crew List :</legend>
                                        <asp:Repeater runat="server" ID="rptCrew">
                                            <ItemTemplate>
                                                <div style="float: left; padding: 2px; border: 1px solid #aabbdd; width: 100px; height: 150px;
                                                    overflow: hidden; text-align: center; margin: 2px 0px 2px 2px; background-color: #cfdfef;"
                                                    onclick="javascript:window.open('../crew/CrewDetails.aspx?ID=<%# Eval("ID")%>')">
                                                    <img src="../Uploads/CrewImages/<%# Eval("PhotoUrl")%>" alt="" style="border: 0;
                                                        width: 90px;" />
                                                    <div style="text-align: left; margin-left: 5px; font-weight: bold;">
                                                        <%# Eval("Rank_Short_Name")%>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("Staff_Code")%></div>
                                                    <div style="text-align: left; margin-left: 5px; letter-spacing: 1px;" title="<%# Eval("Staff_FullName")%>">
                                                        <%# ((System.Data.DataRowView)Container.DataItem)["Staff_FullName"]%></div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="background-color: #CEE3F6; font-weight: 700; color: #333333;
                                padding-bottom: 2px; padding-top: 2px">
                                Order Number:
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-bottom: 5px; padding-top: 5px">
                                <asp:DataList ID="dlistPONumber" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table">
                                    <ItemTemplate>
                                        <td style="padding-right: 10px">
                                            <a href="POPreview.aspx?RFQCODE=<%# Eval("REQUISITION_CODE") +"&Vessel_Code="+ Eval("Vessel_CODE")+"&Order_Code="+ Eval("ORDER_CODE") %> "
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "ORDER_CODE")%></a>
                                        </td>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                    <div id="dvCancelReq" style="display: block; position: fixed; left: 15%; top: 20%"
                        runat="server">
                        <CanReq:ucPurcCancelReqsn ID="ucPurcCancelReqsnNew" runat="server" />
                    </div>
                    <div id="divOnHold" style="display: block; border: 1px solid Black; position: absolute;
                        left: 35%; top: 30%; z-index: 2; color: black;" runat="server">
                        <Hold:ucPurcReqsnHold_UnHold ID="HoldUnHold" runat="server" OnCancelClick="btndivCancel_Click"
                            OnSaveClick="btndivSave_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="updReqLog" runat="server">
                <ContentTemplate>
                    <div id="dvreqsType" title="Reqsn type change log" style="display: none; width: 500px;
                        text-align: left; vertical-align: top; overflow: auto">
                        <div style="width: 100%; overflow: scroll">
                            <table>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvReqsnTypeLog" AutoGenerateColumns="true" Width="498px" EmptyDataText="No record found !"
                                            runat="server">
                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="dvPruchremarkMain" style="display: none; position: absolute; border: 1px solid gray;
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
            <div id="dvPurchaseRemark" style="border: 1px solid gray; z-index: 501; color: Black;
                display: none; position: absolute;" class="Tooltip-css">
            </div>
            <div id="dvPopupAddAttachment" title="Add Attachments" style="display: none; width: 500px;">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left">
                                    <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                                    <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                                        Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                        MaximumNumberOfFiles="10" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="dvQuestions" class="draggable" style="display: none; background-color: #CBE1EF;
                border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
                left: 0.5%; top: 15%; width: 1000px; height: 900px; z-index: 1; color: black"
                title=''>
                <div class="content" style="width: 900px; height: 900px;">
                    <iframe id="frPopupFrame" src="" frameborder="0" height="100%" width="100%"></iframe>
                </div>
            </div>
            <div id="dvWorklist" class="draggable" style="display: none; background-color: #CBE1EF;
                border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
                left: 0.5%; top: 15%; width: 1000px; height: 900px; z-index: 1; color: black"
                title=''>
                <div class="content" style="width: 900px; height: 900px;">
                    <iframe id="frPopupFrame1" src="" frameborder="0" height="100%" width="100%"></iframe>
                </div>
            </div>
            <asp:UpdatePanel ID="upddvAccountType" runat="server">
                <ContentTemplate>
                    <div id="dvAccountType" style="display: none; position: absolute; border: 1px solid gray;
                        padding: 10px" class="popup-css">
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    Select Type:
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:DropDownList ID="ddlAccountType" runat="server" Width="270px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Reason:
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="txtTypeRemark" runat="server" Height="50px" TextMode="MultiLine"
                                        Width="270px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnAccountTypeSave" runat="server" Height="25px" OnClick="btnAccountTypeSave_Click"
                                        Text="Save" ValidationGroup="type" Width="62px" />
                                </td>
                            </tr>
                        </table>
                        <input id="hdnAccountType" type="hidden" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hdnSelectedCde" runat="server" Value="" />
            <asp:HiddenField ID="hdnNotselect" runat="server" Value="" />
        </center>
    </div>
</asp:Content>
