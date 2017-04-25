<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="RequisitionPreview.aspx.cs"
    EnableEventValidation="false" Inherits="Purchase_RequisitionPreview" Title="Requisition Preview" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="RJS" Namespace="RJS.Web.WebControl" Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" %>
<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<asp:Content ID="conhead" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function ShowAlertForDelete() {
            var retTrueFalse = confirm("Do you want to delete this Requisition?");
            if (retTrueFalse == true) {
                return true;
            }
            else {
                return false;
            }
        }
        function Validate_supp() {
            var txt_SuppDate = document.getElementById("ctl00_MainContent_txt_SuppDate").value;
            var txt_Discount = document.getElementById("ctl00_MainContent_txt_Discount").value;
            var txt_Advance = document.getElementById("ctl00_MainContent_txt_Advance").value;
            if (txt_SuppDate == "") {
                alert("Please Select The Supply Date"); return false;
            }
            if (txt_Discount == "" || txt_Discount > 100) {
                alert("Discount Rate Should Be Between 1-100 Percentage"); return false;
            }
            if (txt_Advance == "" || txt_Advance > 100) {
                alert("Advance Rate Should Be Between 1-100 Percentage"); return false;
            }
        }

        function ValidationOnFinalize() {
            return Validate_supp();
            var txtReqComment = document.getElementById("ctl00_MainContent_txtReqComment").value;
            var DDLPort = document.getElementById("ctl00_MainContent_DDLPort").value;
            var strDeliveryDate = document.getElementById("ctl00_MainContent_txtfrom").value;
            var flag;
            if (txtReqComment == "") {
                alert("Reason for Requisition is a mandatory field.")
                return false;
            }

            if (DDLPort == "0") {
                var Result = confirm("If you know Please select DeliveryPort ?")
                if (Result == true) {
                    return false;
                }

            }
            else {

                if (DDLPort != "0") {
                    if (strDeliveryDate == "") {
                        alert("Select the Supply Date.")
                        return false;
                    }
                    else {
                        var currDate = new Date();
                        var strCurrentDt = currDate.format("dd-MM-yyyy");
                        //       var strCurrentDt = currDate.getDate()+ "-" + (currDate.getMonth()+1) + "-" + currDate.getYear();

                        var dt1 = parseInt(strCurrentDt.substring(0, 2), 10);
                        var mon1 = parseInt(strCurrentDt.substring(3, 5), 10);
                        var yr1 = parseInt(strCurrentDt.substring(6, 10), 10);

                        var dt2 = parseInt(strDeliveryDate.substring(0, 2), 10);
                        var mon2 = parseInt(strDeliveryDate.substring(3, 5), 10);
                        var yr2 = parseInt(strDeliveryDate.substring(6, 10), 10);


                        var CurrentDt = new Date(yr1, mon1, dt1);
                        var DeliveryDate = new Date(yr2, mon2, dt2);
                        if (DeliveryDate != 'Invalid Date') {
                            if (DeliveryDate <= CurrentDt) {
                                alert("Supply date can not be less or Equal then current date.")
                                return false;
                            }
                        }
                        else {
                            alert('Invalid Supply Date.');
                            return false;
                        }

                    }
                }
            }
            return true;
        }


        function ValidationOnSavecomment() {
            var txtReqComment = document.getElementById("ctl00_MainContent_txtReqComment").value;

            if (txtReqComment == "") {
                alert("Reason for Requisition is a mandatory field.")
                return false;
            }
            return true;
        }


        function CompareDates() {
            var str1 = document.getElementById("Fromdate").value;
            var str2 = document.getElementById("Todate").value;
            var dt1 = parseInt(str1.substring(0, 2), 10);
            var mon1 = parseInt(str1.substring(3, 5), 10);
            var yr1 = parseInt(str1.substring(6, 10), 10);
            var dt2 = parseInt(str2.substring(0, 2), 10);
            var mon2 = parseInt(str2.substring(3, 5), 10);
            var yr2 = parseInt(str2.substring(6, 10), 10);
            var date1 = new Date(yr1, mon1, dt1);
            var date2 = new Date(yr2, mon2, dt2);

            if (date2 < date1) {
                alert("To date cannot be greater than from date");
                return false;
            }
            else {
                alert("Submitting ...");
                document.form1.submit();
            }
        }



        function printContent() {
            var printIframe = document.createElement("IFRAME");
            document.body.appendChild(printIframe);
            var printDocument = printIframe.contentWindow.document;
            printDocument.designMode = "on";
            printDocument.open();
            printDocument.write("<html><head></head><body>" + PreviewerDiv.innerHTML + "</body></html>");
            printIframe.style.position = "absolute";
            printIframe.style.top = "-1000px";
            printIframe.style.left = "-1000px";

            if (document.all) {
                printDocument.execCommand("Print", null, false);
            }
            else {
                printIframe.contentWindow.print();
            }
        }



        var isChoice = 0;

        function callAlert(Msg, Title) {
            txt = Msg;
            caption = Title;
            vbMsg(txt, caption)
            alert(isChoice);
        }

        function validatesave() {

            var str = document.getElementById('ctl00_MainContent_stssave').value;
            if (str == "1") {
                window.close();
            }
            else {

                //                var answer = confirm('Do you want to save ?')
                var answer = confirm('Do you want to close the window ?')
                if (answer)
                    return true;
                else
                    return false;

            }

        }
        function fn_OnClose() {
            $('[id$=btnLoadFiles]').trigger('click');
        }
        function OpenAssignWorkList() {

            DocCode = '<%=Request["Document_Code"]%>';
            vid = '<%=Request["Vessel_Code"] %>';
            offid = 0;


            var URL = "AssignWorklist.aspx?DocumentCode=" + DocCode + "&VID=" + vid + "&OFFID=" + offid + "&Mode=ADD&rnd=" + Math.random();
            OpenPopupWindowBtnID('Assign_Worklist', 'Assign Worklist for Requisition', URL, 'popup', document.body.offsetHeight / 1.1, document.body.offsetWidth / 1.9, null, null, false, false, true, null, 'ctl00_MainContent_pnlViewCrewInvolve');
        }
        function getid(btn) {
            var cmdvalue = btn[0].getAttribute('commandArgument');
            var hdn = document.getElementById('ctl00_MainContent_hdnid');
            hdn.value = cmdvalue;

        }
        function OpenQuestionAnswers(DocumentCode) {
            $('#dvQuestions').attr("Title", "Add/View Question");
            $('#dvQuestions').css({ "width": "900px", "height": "800px", "text-allign": "center" });
            $('#dvQuestions').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });
            var URL = "View_QuestionAnswers.aspx?DocumentCode=" + DocumentCode + "&Mode=ADD&rnd=" + Math.random();
            document.getElementById("frPopupFrame1").src = URL;
            showModal('dvQuestions', true);
        }

        function validateQuestions() {
            var flag = true;
            var dropdowns = new Array(); //Create array to hold all the dropdown lists.
            var texss = new Array();
            var gridview = document.getElementById('grdQuestion');  //GridView1 is the id of ur gridview.
            dropdowns = gridview.getElementsByTagName('select'); //Get all dropdown lists contained in grdQuestion.
            texss = gridview.getElementsByTagName('input');

            for (var i = 0; i < dropdowns.length; i++) {
                if (dropdowns.item(i).value == '--SELECT--') //If dropdown has no selected value
                {
                    flag = false;
                    break; //break the loop as there is no need to check further.
                }
                for (var j = 0; j < texss.length; j++) {
                    if (texss.item(i).type == "text") {
                        if (texss.item(i).value == "") {
                            flag = false;
                            break; //break the loop as there is no need to check further.
                        }
                    }
                }

            }
            if (!flag) {
                alert('All answers are mandatory');
            }
            return flag;
        }



        //-----------------Supplier Select Change
        function Supplier_SelectChange(row) {
            var hdn = row.id.toString().replace('BtnSuppSelect', 'HD_status')
            var SuppStatus = document.getElementById(hdn).value
           var tt = document.getElementById(row.id);
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            var grid = document.getElementById("<%=GV_SupplierList.ClientID%>");

            Row = grid.rows[rowIndex];
            td = Row.cells[0];

            document.getElementById("<%=HD_SelectedSupplier.ClientID%>").value = td.innerText
            for (i = 0; i < grid.rows.length; i++) {
                grid.rows[i].style.backgroundColor = 'White';
            }


            if (SuppStatus == "Approved") {
                grid.rows[rowIndex].style.backgroundColor = 'SkyBlue';

            }
            else { alert("The supplier you have selected is not approved. Please approve it in the ASL before proceeding") }
            return false;
            
        }
        //-----------------Supplier Select Change

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
            width: 98%;
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
        .tdh
        {
            text-align: right;
            padding: 1px 3px 1px 3px;
        }
        .tdd
        {
            text-align: left;
            padding: 1px 3px 1px 3px;
        }
        .btn
        {
            -moz-border-radius: 20px;
            -webkit-border-radius: 20px;
            -khtml-border-radius: 20px;
            border-radius: 20px;
            width: 150px;
            height: 30px;
            background-color: #ccffff;
        }
        .cellbordr td
        {
            border: 1px solid #cccccc;
        }
        .grdhead
        {
            background-color: Silver; color: Black; border-collapse: collapse;
             border: 1px; border-bottom-style: solid; border-bottom-color: Gray;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
    <div style="font-size: 14px; background-color: #5588BB; color: White; text-align: center;
                padding: 2px 0px 2px 0px; margin: 0px 0px 0px 0px;">
                <b>&nbsp;Requisition Preview</b>
            </div>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            
            <div id="childdiv">
                <asp:UpdatePanel ID="update1" runat="server">
                    <ContentTemplate>
                        <table align="center" cellpadding="0" style="width: 99%; border-color: Black; border-bottom-width: 1px">
                            <tr id="tblrw">
                                <td align="center" colspan="999" style="width: 100%;">
                                    <asp:Repeater ID="rptMain" runat="server" OnItemDataBound="OnItemDataBound">
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <tr align="center">
                                                    <td align="center" style="width: 5%; " class="grdhead">
                                                        <b>Part Number</b>
                                                    </td>
                                                    <td align="center" style="width: 7%;" class="grdhead">
                                                        <b>Name</b>
                                                    </td>
                                                    <td align="center" style="width: 25%;" class="grdhead">
                                                        <b>Description</b>
                                                    </td>
                                                    <td align="center" style="width: 3%;" class="grdhead">
                                                        <b>Unit</b>
                                                    </td>
                                                    <td align="center" style="width: 5%;" class="grdhead">
                                                        <b>Requested Qty</b>
                                                    </td>
                                                    <td align="center" style="width: 4%;" class="grdhead">
                                                        <b>Discount
                                                    </td>
                                                    <td align="center" style="width: 10%;" class="grdhead">
                                                        <b>VAT
                                                    </td>
                                                    <td align="center" style="width: 10%;" class="grdhead">
                                                        <b>WithHold
                                                    </td>
                                                    <td align="center" style="width: 5%;" class="grdhead">
                                                        <b>Price Per Unit</b>
                                                        <asp:Button ID="BtnCurrency" runat="server" Text="USD" Font-Size="Small" OnClick="BtnCurrency_Click" />
                                                        <asp:DropDownList ID="ddlCurrency" runat="server" Font-Size="XX-Small" Visible="false"
                                                            AutoPostBack="true" OnSelectedIndexChanged="CurrencyChange">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 5%;" class="grdhead">
                                                        <b>Total Price</b>
                                                    </td>
                                                    <td align="center" style="width: 13%;" class="grdhead">
                                                        <b>Comments</b>
                                                    </td>
                                                    <td align="center" style="width: 3%;" class="grdhead">
                                                        <b>Action</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%" align="center">
                                                <tr>
                                                    <td colspan="999">
                                                        <asp:Label ID="lblSubCatlog" runat="server" Width="100%" BackColor="SkyBlue" Text='<%# Eval("SubCatalog") %>' /><br />
                                                        <asp:Panel ID="pnlsubcatlog" runat="server" Width="100%">
                                                            <asp:Repeater ID="rptChild" runat="server">
                                                                <ItemTemplate>
                                                                    <table class="ChildGrid" cellspacing="0" rules="all" border="1" width="100%">
                                                                        <tr>
                                                                            <td style="width: 5%;">
                                                                                <asp:Label ID="lblPartno" runat="server" Text='<%# Eval("Part_Number") %>' />
                                                                            </td>
                                                                            <td style="width: 7%">
                                                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("VesselName") %>' />
                                                                            </td>
                                                                            <td style="width: 25%">
                                                                                <asp:Label ID="lbldesc" runat="server" Text='<%# Eval("ItemDesc") %>' />
                                                                            </td>
                                                                            <td style="width: 3%">
                                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("ItemUint") %>' />
                                                                            </td>
                                                                            <td style="width: 5%">
                                                                                <asp:TextBox ID="txtQnty" runat="server" Text='<%# Eval("ReqestedQty") %>' Enabled="false"
                                                                                    Width="98%" OnTextChanged="Calculate" CommandArgument='<%#Eval("ItemID")%>' AutoPostBack="true" Style="text-align: right"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 4%">
                                                                                <asp:TextBox ID="txt_Discount" runat="server" Text='<%# Eval("ORDER_DISCOUNT") %>'
                                                                                    Enabled="false" Width="98%" OnTextChanged="Calculate"  AutoPostBack="true" Style="text-align: right"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 10%" align="right">
                                                                                <asp:DropDownList ID="ddl_ItemVAT" runat="server" Width="98%">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="width: 10%" align="right">
                                                                                <asp:DropDownList ID="ddl_item_Withhold" runat="server" Width="98%">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="width: 5%" align="right">
                                                                                <asp:TextBox ID="txt_PPU" runat="server" Style="text-align: right" Text='<%# Eval("ORDER_RATE") %>'
                                                                                    Enabled="false" Width="98%" OnTextChanged="Calculate" AutoPostBack="true"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 5%">
                                                                                <asp:TextBox ID="txtTP" runat="server" Style="text-align: right" Text='<%# Eval("ORDER_PRICE") %>'
                                                                                    Enabled="false" Width="98%"></asp:TextBox>
                                                                            </td>
                                                                            <td style="width: 13%">
                                                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("ItemComments") %>' />
                                                                            </td>
                                                                            <td style="width: 3%;" align="center">
                                                                                <asp:ImageButton ID="btnupdate" runat="server" CommandArgument='<%#Eval("ItemID")%>'
                                                                                    Text='Update' ForeColor="Black" ToolTip="Update" ImageUrl="~/images/accept.png"
                                                                                    Visible="false" OnClick="UpdateItemClick" Height="16px"></asp:ImageButton>
                                                                                <asp:ImageButton ID="ImgBtnCancel" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                                                                    Visible="false" CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"
                                                                                    OnClick="CancelClick"></asp:ImageButton>
                                                                                <asp:ImageButton ID="ImgUpdate" runat="server" CommandArgument='<%#Eval("ItemID")%>'
                                                                                    Text='Update' ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif" OnClick="updateClick"
                                                                                    Height="16px"></asp:ImageButton>&nbsp
                                                                                <asp:ImageButton ID="ImgDelete" runat="server" OnClick="deleteClick" Text="Delete"
                                                                                    ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px">
                                                                                </asp:ImageButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </asp:Panel>
                                                        <asp:HiddenField ID="hfCatlogId" runat="server" Value='<%# Eval("SubCatalogID") %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server" ID="update2">
                <ContentTemplate>
                <table id="tbl" runat="server" class="cellbordr" align="center" cellpadding="0" style="width: 99%;">
                    <tr>
                        <td width="30%" valign="top">
                            <div style="overflow: scroll; width: 100%" align="center">
                                <div class="page-title">
                                    <asp:Label ID="Label121" Style="font-weight: bold; color: Black; background-color: "
                                        Text="Purchase Questionnaire" runat="server"></asp:Label>
                                </div>
                                <asp:GridView ID="grdQuestion" runat="server" ClientIDMode="Static" EmptyDataText="No Record Found!"
                                    Width="100%" AutoGenerateColumns="false" BorderStyle="Solid" BorderColor="Gray"
                                    BorderWidth="1px" CellPadding="2" CellSpacing="0" OnRowDataBound="grdQuestion_RowDataBound">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Questions">
                                            <ItemStyle Width="7%" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="50%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestID" Style="display: none" ClientIDMode="Static" runat="server"
                                                    Text='<%#Eval("Question_ID") %>'></asp:Label>
                                                <asp:Label ID="lblQuestion" ClientIDMode="Static" runat="server" Text='<%#Eval("Question") %>'></asp:Label>
                                                <asp:Label ID="lblGradeType" Style="display: none" ClientIDMode="Static" runat="server"
                                                    Text='<%#Eval("Grade_Type") %>'></asp:Label>
                                                <asp:Label ID="lblOBJE" ClientIDMode="Static" runat="server" Style="display: none"
                                                    Text='<%#Eval("OBJE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Answers">
                                            <ItemStyle Width="7%" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Width="50%" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDescriptive" ClientIDMode="Static" runat="server" TextMode="MultiLine"
                                                    Text='<%#Eval("Remark")%>'> </asp:TextBox>
                                                <asp:DropDownList ID="ddlAnswers" ClientIDMode="Static" runat="server">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblAns" Style="display: none" runat="server" ClientIDMode="Static"
                                                    Text='<%#Eval("Option_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                        <td width="30%" valign="top">
                            <div class="page-title">
                                <asp:Label ID="Label5" Style="font-weight: bold; color: Black; background-color: "
                                    Text="Select Supplier" runat="server"></asp:Label>
                            </div>
                            <div id="supplierdiv" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txt_searchCodeName" runat="server" Width="98%"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                TargetControlID="txt_searchCodeName" WatermarkText="Supplier's - Code / Name / ShortName / Scope"
                                                WatermarkCssClass="watermarked" />
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton src="../Images/SearchButton.png" Style="border-width: 0px; height: 21px;"
                                                ID="searchimg" runat="server" OnClick="searchimg_Click" />
                                        </td>
                                    </tr>
                                    <tr style="height: 100px">
                                        <td colspan="2" align="center">
                                            <div style="height: 150px; overflow: scroll">
                                                <asp:UpdatePanel ID="UPDateSuppselect" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GV_SupplierList" runat="server" Width="98%" AutoGenerateColumns="false">
                                                            <SelectedRowStyle BackColor="LightCyan" ForeColor="DarkBlue" Font-Bold="true" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Supplier Code" DataField="Supplier_Code" />
                                                                <asp:BoundField HeaderText="Supplier Name" DataField="Supplier_Name" />
                                                                <asp:BoundField HeaderText="Supplier Type" DataField="Supplier_Type" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <%--<asp:CheckBox ID="rbtn_suppselect" runat="server" AutoPostBack="true" OnCheckedChanged="selected_suppchange" />--%>
                                                                        <asp:Button ID="BtnSuppSelect" runat="server" Text="Select" OnClientClick="Supplier_SelectChange(this);return false;" />
                                                                        <asp:HiddenField ID="HD_status" runat="server" Value='<%#Eval("Supp_Status")%>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers >
                                                    <asp:AsyncPostBackTrigger ControlID="btnCancel" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                               
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            With Hold Tax
                                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*" />
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlWithHoldTax" runat="server" Width="100%">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            VAT Rate
                                            <asp:Label ID="Label11" runat="server" ForeColor="Red" Text="*" />
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlVAt" runat="server" Width="100%">
                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Supplier Port
                                            <asp:Label ID="Label12" runat="server" ForeColor="Red" Text="*" />
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlSupp_Port" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Supply Date
                                            <asp:Label ID="Label13" runat="server" ForeColor="Red" Text="*" />
                                        </td>
                                        <td colspan="2">
                                           <%-- <div id="Div1" style="position: relative">--%>
                                                <asp:TextBox ID="txt_SuppDate" runat="server" Width="99%"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CE_SuppDate" runat="server" TargetControlID="txt_SuppDate" 
                                                    Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            <%--</div>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Discount Rate
                                            <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="*" />
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txt_Discount" runat="server" Width="99%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Advance Payment
                                            <asp:Label ID="Label15" runat="server" ForeColor="Red" Text="*" />
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txt_Advance" runat="server" Width="99%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td width="20%" valign="top" align="center">
                            <div class="page-title">
                                <asp:Label ID="Label3" Style="font-weight: bold; color: Black; background-color: "
                                    Text="Assign Worklist jobs" runat="server"></asp:Label>
                                <br />
                                <br />
                            </div>
                            <br />
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlViewCrewInvolve" runat="server">
                                        <asp:Button ID="BtnJob" runat="server" Text=" Worklist Jobs" OnClientClick="OpenAssignWorkList()" />
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td width="20%" valign="top" align="center">
                            <asp:UpdatePanel ID="updAttach" runat="server">
                                <ContentTemplate>
                                    <div class="page-title">
                                        <asp:Label ID="Label4" Style="font-weight: bold; color: Black; background-color: "
                                            Text="Attachments List" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                    </div>
                                    <br />
                                    <br />
                                    <div style="overflow: auto; height: 150px; text-align: left">
                                        <div>
                                            <asp:GridView ID="gvAttachment" AutoGenerateColumns="false" runat="server">
                                                <HeaderStyle ForeColor="White" BackColor="#5D7B9D" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnDelete" ImageUrl="~/Purchase/Image/Close.gif" runat="server"
                                                                OnClick="imgbtnDelete_Click" CommandArgument='<%#Eval("id")+","+Eval("File_Path")+","+Eval("Office_ID")+","+Eval("Vessel_Code") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="File Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="440px">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkAtt" Target="_blank" runat="server" Text='<%# Eval("File_Name")%>'
                                                                NavigateUrl='<%#"../Uploads/Purchase/"+System.IO.Path.GetFileName(Convert.ToString(Eval("File_Path")))%>'> </asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div align="center">
                                            <asp:Button ID="btnid" runat="server" Text="Add Attachment" OnClientClick="showModal('dvPopupAddAttachment',true,fn_OnClose);" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                </ContentTemplate>
                <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                </Triggers>--%>
                </asp:UpdatePanel>
            </div>
            <br />
            <br />
            <div >
                <table cellpadding="1" cellspacing="0" border="0" style="width: 90%;" >
                    <tr>
                        <td style="text-align: left;" colspan="2">
                            <table cellpadding="2" cellspacing="10" align="center" width="100%">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Style="font-size: small"
                                            OnClientClick="return validatesave();" Width="150px" OnClick="btnCancel_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnEdit" runat="server" Width="150px" Text="Edit" Style="font-size: small" OnClick="btnEdit_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Width="150px" Text="Save" Style="font-size: small" OnClientClick="return Validate_supp()"
                                            OnClick="btnSave_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDelete" runat="server" Width="150px" Text="Delete" Style="font-size: small"
                                            OnClientClick="return ShowAlertForDelete();" OnClick="btnDelete_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnFinalize" runat="server" Text="Finalize" Width="150px" ValidationGroup="suppValidate"
                                            OnClientClick="return ValidationOnFinalize();" OnClick="btnFinalize_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnLoadFiles" OnClick="btnLoadFiles_Click" runat="server" Width="150px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <input id="stssave" type="hidden" runat="server" />
        </div>
    </center>
    <div id="dvQuestions" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 0.5%; top: 15%; width: 1000px; height: 900px; z-index: 1; color: black"
        title=''>
        <div class="content" style="width: 900px; height: 900px;">
            <iframe id="frPopupFrame1" src="" frameborder="0" height="100%" width="100%"></iframe>
        </div>
    </div>
    <asp:HiddenField ID="hdnid" runat="server" />
    <asp:HiddenField ID="HDNCurrency" runat="server" />
    <asp:HiddenField ID="HDNPreviousCurr" runat="server" />
    <asp:HiddenField ID="itemid" runat="server" />
    <asp:HiddenField ID="HD_TP" runat="server" />
    <asp:HiddenField ID="HD_SelectedSupplier" runat="server" />
    <div id="dvPopupAddAttachment" title="Add Attachments" style="display: none; width: 500px;
        height: 250px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="Label2" Text="Select file" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
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
</asp:Content>
