<%@ Page Language="C#" MasterPageFile="~/WebQtn/Webquotation.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="QoutationEntry.aspx.cs" Inherits="WebQuotation_QoutationEntry"
    Title="Quotation Entry" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UserControl/ctlPortList.ascx" TagName="ctlPortList" TagPrefix="uc1" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="JS/boxover.js" type="text/javascript"></script>
    <script src="JS/messages.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="CSS/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/CustomAsyncDropDown.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <style type="text/css">
        #main
        {
        }
        .style1
        {
            width: 126px;
        }
        .style2
        {
            color: #FF0000;
        }
        .style3
        {
            color: #FF0000;
            font-weight: normal;
        }
         .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }    
        body
        {
            font-size: 12px;
            font-family: Tahoma;
        }
        input
        {
            font-size: 11px;
            font-family: Tahoma;
        }
        select
        {
            font-size: 11px;
            font-family: Tahoma;
        }
        
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            border-bottom: 1px solid gray;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
            background-color: #F2F2F2;
            color: #424242;
            width:148px;
        }
        .tdd
        {
            font-size: 12px;
            text-align: left;
            padding: 0px 0px 0px 3px; /*border-left: 1px solid gray;*/
            border-bottom: 1px solid gray;
            height: 20px;
            vertical-align: middle;
            background-color: #F2F2F2;
            color: #1C1C1C;
            width:200px;
        }
        .popup-css
        {
            background-image: -ms-linear-gradient(bottom, #A6DEFF 0%, #18D9EF 100%);
            background: -webkit-gradient(linear, left top, left bottom, from(#e9eff2), to(#8fb9cc));
            background: -moz-linear-gradient(top,  #e9eff2,  #8fb9cc);
            color: Black;
            border: 1px solid gray;
            background-color: #8fb9cc;
        }
        
        .bgc
        {
            background-color: #F2F5A9;
            border: 1px solid #A4A4A4;
            padding: 2px;
        }
        
    </style>
    <script language="javascript" type="text/javascript">

        var Glob_Item_Ref_code = "";
        var Glob_Quotation_Code = "";
        var lastExecutor = null;
        var imgremarkThis = null;

        function ASync_Get_Item_Remark(Item_Ref_code, Quotation_Code, evt, objthis, isClicked) {

            if (isClicked.toString() == "1") {
                document.getElementById("DivRemarks").style.display = "block";
                Glob_Item_Ref_code = Item_Ref_code;
                Glob_Quotation_Code = Quotation_Code;
                imgremarkThis = objthis;
            }

            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../WebService.asmx', 'asyncGetSupplRemarks', false, { "Quotation_Code": Quotation_Code, "Item_Ref_Code": Item_Ref_code }, onSuccessASync_Get_Item_Remark, Onfail, new Array(evt, objthis, isClicked));
            lastExecutor = service.get_executor();

        }


        function onSuccessASync_Get_Item_Remark(retVal, eventArgs) {

            if (eventArgs[2].toString() == "1") {
                document.getElementById('txtRemarks').value = retVal;



            }
            else {
                js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
            }
        }

        function ASync_Ins_Item_Remark() {



            var Remark = document.getElementById('txtRemarks').value
            document.getElementById("DivRemarks").style.display = "none";

            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../WebService.asmx', 'asyncUpdateSupplRemarks', false, { "Quotation_Code": Glob_Quotation_Code, "Item_Ref_Code": Glob_Item_Ref_code, "Remark": Remark }, onSuccessASync_Ins_Item_Remark, Onfail);
            lastExecutor = service.get_executor();

        }


        function onSuccessASync_Ins_Item_Remark() {
            if (document.getElementById('txtRemarks').value != "")
                imgremarkThis.src = "Image/view1.gif"
            else
                imgremarkThis.src = "Image/remark_new.gif"

            document.getElementById("DivRemarks").style.display = "none";

        }

        //////////////////////////////////////////

        function Onfail(retval) {


        }


        var PreItemID = "0";

        function showMakers() {
            document.getElementById("tblMachmaker").style.display = "block"; return false;
        }

        function DisplayItem(ID) {

            var item = ID.split("btn");
            item = item[0] + "dvitemtype";

            document.getElementById(item).style.display = "block";
            if (PreItemID != "0" && PreItemID != item) {
                document.getElementById(PreItemID).style.display = "none";
            }
            PreItemID = item;
            return false;

        }


        function txtvatChanged() {

            inlineMsg('ctl00_ContentPlaceHolder1_txtVat', '<strong>Message</strong><br />These goods are ordered as stores in transit for delivery <br >to an ocean vessel and as such are zero rated for tax purposes', 8);

        }

        function txtPkgHandlingChanged() {

            inlineMsg('ctl00_ContentPlaceHolder1_txtPkgHandling', '<strong>Message</strong><br />For better chance to get this order,we prefer a  <br >waiver on Packing and Handling charges.', 7);

        }

        function LeadTimeCloseDiv() {
            var control = document.getElementById("ctl00_ContentPlaceHolder1_divLeadTime");
            //control.style.visibility = "hidden";
            control.style.visibility = "display";

        }
        function CloseDiv() {
            lastExecutor.abort();
            document.getElementById("DivRemarks").style.display = "none";
            return false;


        }




        function onSubmit() {
            var msgRetval = confirm("Once Submitted, you can not edit this quotation. Do you want to continue ?");
            return msgRetval;
        }

        function ondeclined() {

            var rem = document.getElementById('ctl00_ContentPlaceHolder1_txtDeclinetoQuoteRemark').value;
            if (rem.toString().trim().length == 0) {
                alert('Please enter remark !');
                return false;
            }
            else {
                var msgRetval = confirm("Once declined , you can not edit this quotation. Do you want to continue ?");
                return msgRetval;
            }
        }


        function showdeclinetoquote() {
            try {
                var a = showModal('dvDeclineToQuote');
            }
            catch (e)
           { }

            return false;
        }

        function ValidationOnSave() {

            var DDLCurrency = document.getElementById("ctl00_ContentPlaceHolder1_DDLCurrency").value;
            var txtExchangeRate = document.getElementById("ctl00_ContentPlaceHolder1_HiddenField2").value;
            // var txtAddCharge = document.getElementById("ctl00_ContentPlaceHolder1_txtAddCharge").value;
            // var txtSurcharge = document.getElementById("ctl00_ContentPlaceHolder1_txtSurcharge").value;
            var txtVat = document.getElementById("ctl00_ContentPlaceHolder1_txtVat").value;
            // var txtRebate = document.getElementById("ctl00_ContentPlaceHolder1_txtRebate").value;
            var txtQtnRef = document.getElementById("ctl00_ContentPlaceHolder1_txtQuppQtnRef").value;
            var txtreason = document.getElementById("ctl00_ContentPlaceHolder1_txtreason").value;
            var txtTransCost = document.getElementById("ctl00_ContentPlaceHolder1_txtTransCost").value;
            var txtPkgHandling = document.getElementById("ctl00_ContentPlaceHolder1_txtPkgHandling").value;
            if (DDLCurrency == "0") {
                alert("Please select Currency");
                return false;
            }



            if (txtVat != "") {
                if (isNaN(txtVat)) {
                    alert("Vat field allow only Numeric.");
                    return false;
                }

            }


            if (txtQtnRef == "") {
                alert("Please provide supplier quotation refernce");
                return false;
            }



            if (txtTransCost != "") {
                if (isNaN(txtTransCost)) {
                    alert("Trucking/freight Cost  field allow only Numeric.");
                    return false;
                }

            }
            else {
                alert("Trucking/freight Cost is a mandatory field.");
                return false;
            }

            if (txtPkgHandling != "") {
                if (isNaN(txtPkgHandling)) {
                    alert("Pkg/Handling  field allow only Numeric.");
                    return false;
                }

            }
            else {
                alert("Pkg/Handling is a mandatory field.");
                return false;
            }


            //if (txtTransCost != "0.00" && txtPkgHandling != "0.00") 
            if (Number(txtTransCost) >= 0 || Number(txtPkgHandling) >= 0) {
                if (txtreason == "") {
                    alert("Please provide reason for transportation/Packaging charges ");
                    return false;
                }
            }

            return true;
        }


        function Claculate(obj_id) {


            var DDLCurrency = document.getElementById("ctl00_ContentPlaceHolder1_DDLCurrency").value;
            var txtExchangeRate = document.getElementById("ctl00_ContentPlaceHolder1_HiddenField2").value;
            var sQty = "";

            var txtUnitprice_obj = "";
            var txtDiscount_obj = "";
            var txtTotalPrice_obj = "";



            if (obj_id.indexOf("_DDL") == -1) {
                txtUnitprice_obj = document.getElementById(obj_id.split("_txt")[0].toString() + "_txtRate");
                txtDiscount_obj = document.getElementById(obj_id.split("_txt")[0].toString() + "_txtDiscount");
                txtTotalPrice_obj = document.getElementById(obj_id.split("_txt")[0].toString() + "_txtTotalRate");
                sQty = document.getElementById(obj_id.split("_txt")[0].toString() + "_lblQyotedQty").innerHTML;
            }
            else {

                txtUnitprice_obj = document.getElementById(obj_id.split("_DDL")[0].toString() + "_txtRate");
                txtDiscount_obj = document.getElementById(obj_id.split("_DDL")[0].toString() + "_txtDiscount");
                txtTotalPrice_obj = document.getElementById(obj_id.split("_DDL")[0].toString() + "_txtTotalRate");
                sQty = document.getElementById(obj_id.split("_txt")[0].toString() + "_lblQyotedQty").innerHTML;

            }



            var txtUnitPrice = txtUnitprice_obj;
            var txtDiscount = txtDiscount_obj;
            var txtTotal = txtTotalPrice_obj;


            if (DDLCurrency == "0") {
                alert("Please select Currency");
                return false;
            }
            if (txtExchangeRate.value == "") {
                alert("Please enter the exchange rate.");
                return false;
            }
            else {
                var iQty = (sQty == "") ? 0 : sQty;
                var iUP = (txtUnitPrice.value == "") ? 0 : txtUnitPrice.value;
                var iDis = (txtDiscount.value == "") ? 0 : txtDiscount.value;
                var iTot = 0;

                if (iDis > 0)
                    iTot = (iQty * iUP) - (iDis / 100) * (iQty * iUP);
                else
                    iTot = (iQty * iUP);

                txtTotal.value = iTot.toFixed(2);

                //  txtUnitPrice.style.backgroundColor =(txtUnitPrice.value > 0) ? "White" : "Aqua";


                CalculateTotal();

            }
        }

        function CalculateTotal() {

            var strCommonPart = "ctl00_ContentPlaceHolder1_rgdQuoEntry_ctl00_ctl";
            var txtTotalDiscount = document.getElementById("ctl00_ContentPlaceHolder1_txtTotalDiscount").value;
            // var txtSurcharge = document.getElementById("ContentPlaceHolder1_txtSurcharge").value;
            var txtSurcharge = 0;
            var txtVat = document.getElementById("ctl00_ContentPlaceHolder1_txtVat").value;

            var TranpChrges = document.getElementById("ctl00_ContentPlaceHolder1_txtTransCost").value;

            var PkgCharges = document.getElementById("ctl00_ContentPlaceHolder1_txtPkgHandling").value;
            var TruckCharge = document.getElementById("ctl00_ContentPlaceHolder1_txtTruckCharge").value;
            var OtherChange = document.getElementById("ctl00_ContentPlaceHolder1_txtOtherChange").value;
            var Barge = document.getElementById("ctl00_ContentPlaceHolder1_txtBarge").value;

            var txtAddCharge = 0;

            var txtRebate = 0;



            // var countRow = '<%= iCountRows  %>';

            var countRow = document.getElementById("ctl00_ContentPlaceHolder1_HiddenField1").value;
            var strSubTotal = 0;
            var strTotal = 0;

            var j = 4;
            for (var i = 0; i < countRow; i++) {
                var strFinalID = "";
                var strtxtRate = "";
                var txtofferedqty = "";
                var txtDiscount = "";
                var totalRow = 0;

                if (j <= 8) {
                    strFinalID = strCommonPart + "0" + j + "_txtTotalRate";
                    strtxtRate = strCommonPart + "0" + j + "_txtRate";
                    txtofferedqty = strCommonPart + "0" + j + "_lblQyotedQty";
                    txtDiscount = strCommonPart + "0" + j + "_txtDiscount";

                }
                else {
                    strFinalID = strCommonPart + j + "_txtTotalRate";
                    strtxtRate = strCommonPart + j + "_txtRate";
                    txtofferedqty = strCommonPart + j + "_lblQyotedQty";
                    txtDiscount = strCommonPart + j + "_txtDiscount";
                }
                j = j + 2;
                totalRow = ((document.getElementById(txtofferedqty).innerHTML * document.getElementById(strtxtRate).value) - ((document.getElementById(txtofferedqty).innerHTML * document.getElementById(strtxtRate).value) * document.getElementById(txtDiscount).value / 100));
                document.getElementById(strFinalID).value = parseFloat(totalRow).toFixed(2);
                strSubTotal = strSubTotal + totalRow;
                if (eval(document.getElementById(strtxtRate).value) <= 0) {
                    document.getElementById(strtxtRate).style.backgroundColor = "Aqua";
                }
                else {
                    //document.getElementById(strtxtRate).style.backgroundColor = "White";
                }
            }


            var decDiscount = 0;
            //Discount calculation only on item's 
            var decAfterDiscount = strSubTotal;
            if (txtTotalDiscount != 0 && txtTotalDiscount != "") {
                decDiscount = (txtTotalDiscount * strSubTotal) / 100;
                decAfterDiscount = strSubTotal - eval(decDiscount);
            }


            //Add Vat
            var decVat = 0;
            var decAfterVat = decAfterDiscount;
            if (txtVat != 0 && txtVat != "") {
                var decVat = (txtVat * decAfterDiscount) / 100;
                decAfterVat = decAfterDiscount + eval(decVat);
            }



            //Add additional Charge
            var decAfterAddCharge = decAfterVat;
            if (txtAddCharge != 0 && txtAddCharge != "") {
                var decAddCharge = (txtAddCharge * decAfterVat) / 100;
                decAfterAddCharge = decAfterAddCharge + eval(decAddCharge);
            }
            if (TranpChrges != "0.00" && TranpChrges != "") {
                decAfterAddCharge = decAfterAddCharge + eval(TranpChrges);
            }
            if (PkgCharges != "0.00" && PkgCharges != "") {
                decAfterAddCharge = decAfterAddCharge + eval(PkgCharges);
            }

            if (TruckCharge != "0.00" && TruckCharge != "") {
                decAfterAddCharge = decAfterAddCharge + eval(TruckCharge);
            }

            if (OtherChange != "0.00" && OtherChange != "") {
                decAfterAddCharge = decAfterAddCharge + eval(OtherChange);
            }

            if (Barge != "0.00" && Barge != "") {
                decAfterAddCharge = decAfterAddCharge + eval(Barge);
            }




            document.getElementById("ctl00_ContentPlaceHolder1_lblFreightFinal").innerHTML = TranpChrges;
            document.getElementById("ctl00_ContentPlaceHolder1_lblOtherFinal").innerHTML = OtherChange;
            document.getElementById("ctl00_ContentPlaceHolder1_lblBargeFinal").innerHTML = Barge;
            document.getElementById("ctl00_ContentPlaceHolder1_lblVatFinal").innerHTML = parseFloat(decVat).toFixed(2);
            document.getElementById("ctl00_ContentPlaceHolder1_lblpkgFinal").innerHTML = PkgCharges;
            document.getElementById("ctl00_ContentPlaceHolder1_lblDiscountFinal").innerHTML = parseFloat(decDiscount).toFixed(4);
            document.getElementById("ctl00_ContentPlaceHolder1_lblTruckFinal").innerHTML = TruckCharge;


            document.getElementById("ctl00_ContentPlaceHolder1_lblAmount").innerHTML = parseFloat(decAfterAddCharge).toFixed(2);
            document.getElementById("ctl00_ContentPlaceHolder1_HiddenField3").value = parseFloat(decAfterAddCharge).toFixed(2);


        }



        function ChangetextColor(txtUnitPrice) {
            if (txtUnitPrice = "") {
                var txtUnitPrice = document.getElementById("ctl00_ContentPlaceHolder1_txtUnitPrice").value;
            }


        }
        function Count_Words() {

            var tbox_input = document.getElementById("ctl00_ContentPlaceHolder1_txtRemarks").value;
            var no_words = tbox_input.value.length;
            // alert("Text box has " + no_words.length + " words");
            document.getElementById("ctl00_ContentPlaceHolder1_lblCount").value = no_words;
            return false;
        }

        function textCounter() {
            // 
            var field = document.getElementById("ctl00_ContentPlaceHolder1_txtRequi").value.length;
            var cntfield = "ctl00_ContentPlaceHolder1_lblCnt";
            var maxlimit = 256;
            if (field > maxlimit)
            // if too long...trim it!
            {
                field.value = field.value.substring(0, maxlimit);
            }
            // otherwise, update 'characters left' counter
            else {
                document.getElementById("ctl00_ContentPlaceHolder1_lblCnt").innerHTML = maxlimit - field;
            }
        }

        function MaskMoney(evt) {

            if (!(evt.keyCode == 9 || evt.keyCode == 190 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 37 || evt.keyCode == 39 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105))) {
                return false;
            }
            var parts = evt.srcElement.value.split(',');

            if (parts.length > 2) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
            if (parts.length == 2 && parts[1].length >= 2) return false;
        }
    </script>
      <script language="javascript" type="text/javascript">
          function OpenScreen(ID, Job_ID) {
              var ReqCode = document.getElementById("ctl00_ContentPlaceHolder1_txtReqCode").value;
              var VesselID = document.getElementById("ctl00_ContentPlaceHolder1_txtVessselCode").value;
              var Catalogue_Code = document.getElementById("ctl00_ContentPlaceHolder1_txtCatalogueCode").value;
              var url = '../Purchase/MachineryDetails.aspx?Requisition_code=' + ReqCode + '&Vessel_ID=' + VesselID + '&Catalogue_Code=' + Catalogue_Code;
              OpenPopupWindowBtnID('MachineryDetails_ID', 'Machinery Details', url, 'popup', 420, 1000, null, null, false, false, true, null, null);
          }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <eo:ASPXToPDF runat="server" ID="ASPXToPDF1">
    </eo:ASPXToPDF>
      <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    <center>
        <asp:HyperLink ID="hylContractSts" Target="_blank" runat="server"></asp:HyperLink>
        <table align="center" cellpadding="0" cellspacing="0" style="width: 90%">
            <tr align="center">
                <%--      <td style="background-color: #084B8A; font-weight: bold; height: 22px; width:10%;  color: #FFFFFF;
                    font-size: 13px">
                </td>--%>
                <td align="left" style="background-color: #084B8A; font-weight: bold; height: 22px;
                    color: #FFFFFF; font-size: 13px">
                    <asp:Button ID="btnExporttoPDF" OnClick="btnExporttoPDF_Click" runat="server" Text="Export To PDF" />
                </td>
                <td style="background-color: #084B8A; font-weight: bold; height: 22px; color: #FFFFFF;
                    font-size: 13px">
                    Web Quotation Entry
                </td>
                <td style="background-color: #084B8A; font-weight: bold; height: 22px; width: 30%;
                    color: #FFFFFF; font-size: 13px">
                    <asp:FileUpload ID="FileRFQUpload" runat="server" Height="24px" Style="font-size: small" />
                    <asp:Button ID="btnRFQUpload" runat="server" Text="Upload" Style="font-size: small"
                        OnClick="btnRFQUpload_Click" />
                </td>
                <td style="background-color: #084B8A; font-weight: bold; height: 22px; width: 10%;
                    color: #FFFFFF; font-size: 13px">
                    <asp:Button ID="Button2" OnClick="btnExportQtnToExcel_Click" runat="server" Text="Export Quotation to Excel" />
                </td>
            </tr>
        </table>
        <table cellpadding="1" cellspacing="0" style="width: 90%; border: 1px solid gray;
            background-color: #F2F2F2; padding-bottom: 5px">
            <tr>
                <td class="tdh">
                    Requisition No :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblReqNo" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Vessel :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblVessel" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Department :
                </td>
                <td class="tdd" colspan="2">
                    <asp:Label ID="lblDept" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdh">
                    Catalogue :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblCatalog" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Quot. Due Date :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblToDate" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Total Items :
                </td>
                <td class="tdd" colspan="2">
                    <asp:Label ID="lblTotalItems" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdh">
                    Delivery Date :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblDelDT" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Delivery Port :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblDeLPort" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Priority :
                </td>
                <td class="tdd" colspan="2">
                    <asp:Label ID="lblReqType" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdh">
                    Purchaser Name :<br>
                    Contact No. :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblPurchaserName" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="lblPurchaserNum" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Delivery Remarks :
                </td>
                <td class="tdd">
                    <asp:TextBox ID="txtdelremark" BackColor="Transparent" Font-Size="10px" BorderStyle="None"
                        BorderColor="Transparent" BorderWidth="0px" TextMode="MultiLine" Width="100%"
                        Height="40" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <td class="tdh">
                    Purchaser Remarks :
                </td>
                <td class="tdd" colspan="2">
                    <asp:TextBox ID="txtPurchaserRemarks" BackColor="Transparent" BorderStyle="None"
                        Font-Size="10px" runat="server" Height="40px" ReadOnly="True" TextMode="MultiLine"
                        Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr align="right" style="color: #000000; ">
                <td>
                    Currency :<span class="style2">*</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="DDLCurrency" AppendDataBoundItems="true" runat="server" Width="110px"
                        CssClass="bgc">
                    </asp:DropDownList>
                    
                </td>
                <td style="color: #000000; " align="right">
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    Barge/Workboat Charge :
                </td>
                <td style="" align="left">
                    <asp:TextBox ID="txtBarge" Width="90px" Style="text-align: right" Text="0.00" CssClass="bgc"
                        onKeydown="JavaScript:return MaskMoney(event);" onblur="JavaScript:CalculateTotal();"
                        runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtExchangeRate" Width="0px" runat="server" Style="text-align: right"
                        ReadOnly="True" Visible="false"></asp:TextBox>
                </td>
                <td style="color: #000000; " align="right">
                    Discount On Quot.(%) :
                </td>
                <td align="left">
                    <asp:TextBox ID="txtTotalDiscount" Width="95%" runat="server" CssClass="bgc" Style="text-align: right"
                        onKeydown="JavaScript:return MaskMoney(event);" onblur="JavaScript:CalculateTotal();"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="color: #000000" align="right">
                    Truck Charge :
                </td>
                <td style="" align="left">
                    <asp:TextBox ID="txtTruckCharge" Style="text-align: right" Text="0.00" CssClass="bgc"
                        onKeydown="JavaScript:return MaskMoney(event);" onblur="JavaScript:CalculateTotal();"
                        runat="server"></asp:TextBox>
                </td>
                <td style="color: #000000;" align="right">
                    Vat/GST(%) :
                </td>
                <td style="" align="left">
                    <asp:TextBox ID="txtVat" runat="server" Width="92px" Style="text-align: right" onclick="txtvatChanged()"
                        onKeydown="JavaScript:return MaskMoney(event);" CssClass="bgc" onblur="JavaScript:CalculateTotal();"></asp:TextBox>
                </td>
                <td style="color: #000000; " align="right">
                    Supplier Qtn. Ref :<span class="style2">*</span>
                </td>
                <td style="color: #000000;" align="left" colspan="2">
                    <asp:TextBox ID="txtQuppQtnRef" runat="server" Width="95%"  CssClass="bgc"   OnTextChanged="txtQuppQtnRef_TextChanged" MaxLength="250" ></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td style="color: #000000; " align="right">
                    Freight Cost :<span class="style2">*</span>
                </td>
                <td style="" align="left">
                    <asp:TextBox ID="txtTransCost" runat="server" CssClass="bgc" Text="0.00" onKeydown="JavaScript:return MaskMoney(event);"
                        Style="text-align: right" onblur="JavaScript:CalculateTotal();"></asp:TextBox>
                    
                </td>
                <td style="color: #000000;" align="right">
                    &nbsp;Pkg and handling Charges :<span class="style2">*</span>
                </td>
                <td style="" align="left">
                    <asp:TextBox ID="txtPkgHandling" runat="server" CssClass="bgc" Width="92px" onclick="txtPkgHandlingChanged()"
                        onKeydown="JavaScript:return MaskMoney(event);" Style="text-align: right" onblur="JavaScript:CalculateTotal();"></asp:TextBox>
                    
                </td>
                <td style="color: #000000; " align="right" valign="middle">
                    Reasons for trans/Pkg charges :<span class="style2">*</span>
                </td>
                <td style="color: #000000; " align="left">
                    <asp:TextBox ID="txtreason" runat="server" CssClass="bgc" TextMode="MultiLine" Height="45px"
                        ValidationGroup="pkch" Width="95%"></asp:TextBox>
                    
                    <asp:RequiredFieldValidator ValidationGroup="pkch" ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtreason" Display="Dynamic" ErrorMessage="Please enter reason for trans/Pkg charges "></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="color: #000000" align="right">
                    Other Charge :
                </td>
                <td style="" align="left">
                    <asp:TextBox ID="txtOtherChange" CssClass="bgc" Style="text-align: right" Text="0.00"
                        onKeydown="JavaScript:return MaskMoney(event);" onblur="JavaScript:CalculateTotal();"
                        runat="server"></asp:TextBox>
                </td>
                <td style="color: #000000;" align="left">
                    Reason for Other Charges :
                </td>
                <td colspan="3" style="" align="left">
                    <asp:TextBox ID="txtReasonOthers" CssClass="bgc" Width="99%" runat="server" TextMode="MultiLine"
                        Height="35px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table style="width: 90%; border-left: 1px solid gray; border-right: 1px solid gray;
            background-color: #F2F2F2; text-align: left">
            <tr>
                <td>
                    <table cellpadding="1" cellspacing="0">
                        <tr>
                            <td width="100%">
                            <asp:Button ID="Button1" runat="server" Text="Click to view vessel/machinery/maker/SubCatalogue details"
                                    ForeColor="Blue" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px" BackColor="Silver"
                                    OnClientClick="OpenScreen(null,null);return false;" />
                               <%-- <asp:Button ID="btnMakerDetails" runat="server" Text="Click to view vessel/machinery/maker details"
                                    ForeColor="Blue" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px" BackColor="Silver"
                                    OnClientClick="document.getElementById('tblMachmaker').style.display = 'block'; return false" />--%>
                            </td>
                           <td style=" display:none;">
                            <asp:TextBox ID="txtReqCode" runat="server" Text="" Width="1px"  ></asp:TextBox>
                            <asp:TextBox ID="txtVessselCode" runat="server" Text="" Width="1px"  ></asp:TextBox>
                            <asp:TextBox ID="txtCatalogueCode" runat="server" Text="" Width="1px"  ></asp:TextBox>
                        </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    <table cellpadding="3" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <span style="color: #424242; font-weight: bold">Attachments : </span>&nbsp;
                                <asp:Panel ID="PanelAttach" runat="server" ScrollBars="Vertical">
                                    <asp:Repeater ID="rpAttachment" runat="server" OnItemDataBound="rpAttachment_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkAtt" runat="server" Target="_blank" NavigateUrl='<%# Eval("File_Path")%>'><%# Eval("SlNo") %>.
                                            <%# Eval("File_Name")%>
                                            </asp:HyperLink>&nbsp;; &nbsp;
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                                <%# Eval("SlNo") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; vertical-align: bottom" class="bgc">
                                <span style="color: #424242; font-weight: bold">Upload: </span>&nbsp;
                                <asp:FileUpload ID="FileUploadAttch" runat="server" Height="26px" />
                                <asp:Button ID="btnUpload" runat="server" Height="26px" OnClick="btnUpload_Click"
                                    Text="Upload" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <%--  <asp:UpdatePanel ID="UpdatePanel1"   runat="server">
            <ContentTemplate>--%>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 90%; border-left: 1px solid gray;
            border-right: 1px solid gray;">
            <tr>
                <td style="text-align: right">
                    <asp:Button ID="btnLeadTime" OnClick="btnLeadTime_Click" runat="server" Text="Add Lead Time For All" />
                </td>
            </tr>
            <tr align="left">
                <td style="width: 90%">
                    Note:The Items having unit price are belongs to Contract .<br />
                    <telerik:RadGrid ID="rgdQuoEntry" runat="server" AllowAutomaticInserts="True" GridLines="None"
                        Skin="WebBlue" Width="100%" AutoGenerateColumns="False" OnDataBound="rgdQuoEntry_DataBound">
                        <MasterTableView TableLayout="Auto" Width="100%">
                            <RowIndicatorColumn Visible="False">
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn Resizable="False" Visible="False">
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="Item S.No." UniqueName="ID">
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    <ItemStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ITEM_REF_CODE" HeaderText="Item ID" UniqueName="ITEM_REF_CODE"
                                    Visible="False">
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    <ItemStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Drawing_Number" HeaderText="Drawing No." UniqueName="Drawing_Number">
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    <ItemStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Part_Number" HeaderText="Part No." UniqueName="Part_Number">
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    <ItemStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ITEM_SYSTEM_CODE" HeaderText="ITEM_SYSTEM_CODE"
                                    UniqueName="ITEM_SYSTEM_CODE" Visible="False">
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    <ItemStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ITEM_SUBSYSTEM_CODE" HeaderText="ITEM_SUBSYSTEM_CODE"
                                    UniqueName="ITEM_SUBSYSTEM_CODE" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="Short_Description" HeaderText="Description"
                                    UniqueName="Short_Description">
                                    <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                    <ItemStyle Width="15%" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <table cellpadding="0" cellspacing="0" border="0" align="left">
                                            <tr>
                                                <td style="border: 0px solid transparent"  align="left">
                                                    <asp:Image ID="imgItemDetails" ImageAlign="Bottom" Visible="true" ImageUrl="~/images/ItemDetails_Supplier.png"
                                                        ToolTip="Click to view item comment" runat="server" />
                                                </td>
                                                <td style="border: 0px solid transparent"  align="left">
                                                    <asp:Label ID="lblitem" runat="server" Text='<%#Eval("Short_Description") %>'></asp:Label>
                                                    <asp:Label ID="lbllongDesc" Visible="false" runat="server" Text='<%#Eval("Long_Description") %>' ToolTip='<%#Eval("ITEM_COMMENT")%>'></asp:Label>
                                                    <asp:Label ID="lblVesselID" runat="server" Text='<%#Eval("Vessel_ID") %>' Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                              <%--<telerik:GridBoundColumn DataField="Long_Description" HeaderText="Long Description"
                                     UniqueName="Long_Description">
                                     <HeaderStyle Width="1%" Wrap="true" />
                                     <ItemStyle Width="1%" Wrap="true"/>
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridTemplateColumn HeaderText="Long Description" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                <ItemTemplate>
                                <asp:Label ID="lblLongDesc11" Text='<%#Eval("Long_Description")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Unit_and_Packings" HeaderText="Unit" UniqueName="Unit_and_Packings">
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    <ItemStyle Width="5%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="REQUESTED_QTY" HeaderText="Reqst Qty." UniqueName="REQUESTED_QTY">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQyotedQty" runat="server" Text='<%#Eval("REQUESTED_QTY")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Offered Qty" Display="false" UniqueName="Offered Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtofferedqty" Width="40px" Height="12px" Enabled="false" runat="server"
                                            Style="text-align: right" onKeydown="JavaScript:return MaskMoney(event);" onblur="JavaScript:Claculate(id);"
                                            Text='<%# Bind("OFFERED_QTY")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Unit Price" UniqueName="QUOTED_RATE">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="updUnitPrice" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtRate" Width="98%" Height="12px" Text='<%# Bind("QUOTED_RATE")%>'
                                                    BackColor='<%#Eval("CTP_PRICE").ToString()=="1"?System.Drawing.Color.PaleVioletRed : System.Drawing.Color.Aqua%>'
                                                    runat="server" Style="text-align: right" onKeydown="JavaScript:return MaskMoney(event);"
                                                    onblur="JavaScript:Claculate(id);"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%"></HeaderStyle>
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Discount" UniqueName="QUOTED_DISCOUNT">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDiscount" MaxLength="5" Height="12px" onKeydown="JavaScript:return MaskMoney(event);"
                                            onblur="JavaScript:Claculate(id);" Width="40px" Text='<%# Bind("QUOTED_DISCOUNT")%>'
                                            runat="server" Style="text-align: right"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%"></HeaderStyle>
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Item Type" UniqueName="Type">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="updItemtype" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DDLItemType" runat="server" Width="99%" AppendDataBoundItems="True"
                                                    AutoPostBack="true" OnSelectedIndexChanged="DDLItemType_SelectedIndexChanged"
                                                    Font-Size="11px">
                                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                    <HeaderStyle Width="8%"></HeaderStyle>
                                    <ItemStyle Width="8%" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Lead Time&lt;br&gt;(In days)" UniqueName="TemplateColumn1">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLeadTime" EnableViewState="true" Style="text-align: right" Width="40px"
                                            Height="12px" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("Lead_Time")%>'
                                            runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%"></HeaderStyle>
                                    <ItemStyle Width="5%" HorizontalAlign="Right" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Total Amt." UniqueName="TotalAmt">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotalRate" runat="server" Enabled="false" EnableViewState="true"
                                            Height="12px" Style="text-align: right" Text='<%# Bind("QUOTED_Price")%>' Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="8%"></HeaderStyle>
                                    <ItemStyle Width="8%" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="MoreOption">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="udpMoreoption" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Button ID="btnMoreItemType" runat="server" CommandArgument='<%#Eval("ITEM_REF_CODE")%>'
                                                    ToolTip='<%#Eval("Short_Description")%>' OnClick="btnMoreItemType_Click" Text="Option" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Width="100px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Supplier Remarks" UniqueName="QUOTATION_REMARKS"
                                    AllowFiltering="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgComments" runat="server" ImageUrl='<%#Eval("QUOTATION_REMARKS").ToString()==""?"~/WebQtn/Image/remark_new.gif":"~/WebQtn/Image/view1.gif" %>'
                                            OnClick='<%# "ASync_Get_Item_Remark(&#39;"+Eval("ITEM_REF_CODE").ToString()+"&#39;,&#39;"+Eval("QUOTATION_CODE").ToString()+"&#39;,event,this,&#39;1&#39;);return false;" %>'
                                            onmouseover='<%# "ASync_Get_Item_Remark(&#39;"+Eval("ITEM_REF_CODE").ToString()+"&#39;,&#39;"+Eval("QUOTATION_CODE").ToString()+"&#39;,event,this,&#39;0&#39;);return false;" %>'
                                            Height="14px" Width="14px" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Item_Type" Display="false" UniqueName="Item_Type">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings>
                                <PopUpSettings ScrollBars="None" />
                            </EditFormSettings>
                        </MasterTableView>
                        <HeaderStyle Font-Size="XX-Small" />
                        <ClientSettings>
                            <Scrolling AllowScroll="false" UseStaticHeaders="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HiddenField3" runat="server" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <table cellpadding="0" cellspacing="0" style="width: 90%; background-color: #f4ffff;
            border-left: 1px solid gray; border-right: 1px solid gray; text-align: right">
            <tr align="right">
                <td style="text-align: right; width: 100%">
                    <asp:Label ID="lblTotalAmountUSD" Visible="false" runat="server" ForeColor="Blue"
                        Style="text-align: right"></asp:Label>
                    <table cellpadding="2" cellspacing="0" style="text-align: right; color: Black; width: 100%">
                        <tr>
                            <td>
                                Truck Charge :
                            </td>
                            <td style="width: 80px">
                                <asp:Label ID="lblTruckFinal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Freight Cost :
                            </td>
                            <td>
                                <asp:Label ID="lblFreightFinal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Other Charge :
                            </td>
                            <td>
                                <asp:Label ID="lblOtherFinal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Barge/Workboat Charge :
                            </td>
                            <td>
                                <asp:Label ID="lblBargeFinal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Vat/GST :
                            </td>
                            <td>
                                <asp:Label ID="lblVatFinal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Pkg and handing Charges :
                            </td>
                            <td>
                                <asp:Label ID="lblpkgFinal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Discount on Quot. :
                            </td>
                            <td>
                                <asp:Label ID="lblDiscountFinal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; color: Blue; font-size: 11px">
                                Total Amount(Local Currency) :
                            </td>
                            <td>
                                <asp:Label ID="lblAmount" runat="server" Font-Size="11px" ForeColor="Blue" Style="text-align: right;
                                    font-weight: bold"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Italic="true" Font-Size="11px"
                        Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 90%; background-color: #f4ffff;
            border-left: 1px solid gray; border-right: 1px solid gray;">
            <tr>
                <td style="border: 1px solid white" align="left" valign="middle" class="style1">
                    Delivery Terms:
                </td>
                <td style="width: 500px;" align="left">
                    <asp:TextBox ID="txtRequi" Enabled="True" runat="server" Width="350px" Height="44px"
                        TextMode="MultiLine"></asp:TextBox>
                    &nbsp;
                    <asp:UpdatePanel ID="updDelterm" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlDelvTerm" DataTextField="Short_Code" DataValueField="CODE"
                                AutoPostBack="true" DataSourceID="ObjectDataSourceDelvTerm" Width="200px" Height="25px"
                                runat="server" OnSelectedIndexChanged="ddlDelvTerm_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;
                            <br />
                            <asp:Label ID="lblMsgonddlDelvTerm" runat="server" Font-Size="9px" Font-Names="Verdana"
                                ForeColor="Maroon"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="">
                    <asp:Button ID="btnDeclinQuote" runat="server" Height="35px" Text="Decline to quote"
                        Enabled="false" Width="100px" OnClientClick="return showdeclinetoquote();" />
                    <asp:Button ID="btnSave" runat="server" Height="35px" Text="Save Quotation" Width="100px"
                        ValidationGroup="pkch" OnClick="btnSave_Click" OnClientClick="return ValidationOnSave();"
                        Enabled="False" />
                    <asp:Button ID="btnSubmit" runat="server" Height="35px" Text="Submit"
                        ValidationGroup="pkch" OnClick="btnSubmit_Click" OnClientClick="return    ValidationOnSave();  onSubmit();"
                        Enabled="False" />
                    <asp:Button ID="btnClose" Height="35px" runat="server" OnClick="btnClose_Click" Text="Close"
                        Width="80px" />
                    <br />
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 90%; background-color: #f4ffff;
            border-left: 1px solid gray; border-right: 1px solid gray;">
            <tr>
                <td style="text-align: left; width: 12%">
                    Origin:
                </td>
                <td align="left" style="width: 88%; padding: 4px 0px 0px 0px">
                    <auc:CustomAsyncDropDownList ID="ctlPortList1" runat="server" Width="200" Height="200" />
                </td>
            </tr>
            <tr>
                <td style="background-color: #C0C0C0">
                </td>
            </tr>
        </table>
        <table style="width: 90%; background-color: #f4ffff; border-left: 1px solid gray;
            border-right: 1px solid gray; border-bottom: 1px solid gray">
            <tr>
                <td style="width: 125px" align="left" valign="middle">
                    Legal Terms:
                </td>
                <td>
                    <asp:TextBox ID="lblLegal" TextMode="MultiLine" ReadOnly="true" Width="100%" BorderStyle="None"
                        Height="60px" runat="server"> </asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="popup-css" id="DivRemarks" style="border: 1px solid Black; position: fixed;
            left: 45%; top: 25%; z-index: 2; color: black; width: 375px; padding: 10px; display: none">
            <center>
                <table style="height: 50px; width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="border-bottom: 1px solid gray">
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr align="center">
                                    <td style="background-color: #808080; font-size: small;">
                                        <asp:Label ID="lblUrgencyTitle" Width="152px" runat="server" Text="Remark" Style="color: #FFFFFF;
                                            font-weight: 700; font-size: small;"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 16px; background-color: #808080;">
                                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/Close.gif" runat="server" OnClientClick="return CloseDiv();"
                                            Style="font-size: xx-small" Width="12px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <textarea id="txtRemarks" cols="40" rows="5"></textarea>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right">
                                        <input type="button" onclick="ASync_Ins_Item_Remark();return false;" value="Save" />
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btndivCancel" runat="server" Text="Cancel" Height="24px" Font-Size="Small"
                                            OnClientClick="return CloseDiv();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
        <div id="divLeadTime" runat="server" style="border: 1px solid Black; background-color: #E0E0E0;
            position: absolute; left: 50%; top: 37%; z-index: 2; color: black; height: 40;
            width: 300" visible="false">
            <table width="100%">
                <tr>
                    <td>
                        Enter Lead Time(in day(s))
                    </td>
                    <td>
                        <asp:TextBox ID="txtLeadTimeEnt" runat="server" Text="0" Width="60" onKeydown="JavaScript:return MaskMoney(event);"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnLeadTimeOk" OnClick="btnLeadTimeOk_Click" runat="server" Text="Ok" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:ObjectDataSource ID="ObjectDataSourceDelvTerm" TypeName="BLLQuotation.BLL_PURC_Purchase"
            SelectMethod="GET_SystemParameters" runat="server">
            <SelectParameters>
                <asp:Parameter Name="Code" DefaultValue="288" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:UpdatePanel ID="upditemtypeSave" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="dvitemtype" runat="server" class="popup-css" style="height: 200px; width: 330px;
                    position: fixed; text-align: left; top: 35%; left: 50%; float: right; display: block;
                    padding: 10px 10px 10px 10px" runat="server" visible="false">
                    <asp:Label ID="lblItemName" runat="server" Text=""></asp:Label>
                    <br />
                    <br />
                    <asp:DataList ID="dlItemType" runat="server">
                        <ItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%; text-align: right; font-weight: bold; font-size: 11px; border: 0px solid white">
                                        <asp:Label ID="lblItemType" runat="server" Text='<%#Eval("Description")%>' ToolTip='<%#Eval("Code") %>'> </asp:Label>
                                    </td>
                                    <td style="width: 50%; text-align: center; font-size: 11px; border: 0px solid white">
                                        <asp:TextBox ID="txtQuotedprice" runat="server" Text='<%#Eval("Quoted_Rate")%>' ToolTip='<%#Eval("id")%>'></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 100%; text-align: center; border: 0px solid white;">
                                <asp:Button ID="btnSaveItem" runat="server" Font-Size="12px" Height="35px" OnClick="btnSaveItem_Click"
                                    Text="Save" Width="80px" />
                                &nbsp;
                                <asp:Button ID="btnCancel" runat="server" Font-Size="12px" Height="35px" Text="Cancel"
                                    Width="80px" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--        </ContentTemplate>
        </asp:UpdatePanel>--%>
        <div id="tblMachmaker" style="position: absolute; left: 25%; top: 20%; width: 900px;
            color: Black; padding: 10px; border: 1px solid gray; text-align: right; display: none"
            class="popup-css">
            <img alt="" onclick="document.getElementById('tblMachmaker').style.display = 'none';"
                src="Image/Close.gif" />
            <table cellpadding="1" cellspacing="0" border="1" align="left" width="100%" style="border-collapse: collapse;
                border: 1px solid gray;">
                <tr>
                    <td style="width: 100%" colspan="6">
                        <table width="100%" border="1" cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                            <tr>
                                <td colspan="6" style="text-align: left; font-weight: bold; font-size: 11px; color: Black;
                                    background-color: #D8D8D8">
                                    Vessel Details:
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td style="width: 8%; font-size: 11px; font-weight: bold">
                                    Vessel Type:
                                </td>
                                <td style="width: 25%; font-size: 11px;">
                                    <asp:Label ID="lblVesselType" runat="server"></asp:Label>
                                </td>
                                <td style="width: 10%; font-size: 11px; font-weight: bold">
                                    Vessel Hull No:
                                </td>
                                <td style="width: 18%; font-size: 11px;">
                                    <asp:Label ID="lblVesselHullNo" runat="server"></asp:Label>
                                </td>
                                <td style="width: 8%; font-size: 11px; font-weight: bold">
                                    Built Yard:
                                </td>
                                <td style="width: 40%; font-size: 11px;">
                                    <asp:Label ID="lblVesselYard" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td style="width: 8%; font-size: 11px; font-weight: bold">
                                    Built Year:
                                </td>
                                <td style="width: 25%; font-size: 11px;">
                                    <asp:Label ID="lblVesselDelvDate" runat="server"></asp:Label>
                                </td>
                                <td style="width: 10%; font-size: 11px; font-weight: bold">
                                    IMO No:
                                </td>
                                <td style="width: 18%; font-size: 11px;">
                                    <asp:Label ID="lblIMOno" runat="server"></asp:Label>
                                </td>
                                <td style="width: 8%; font-size: 11px; font-weight: bold">
                                    Vessel Ex-Name(s):
                                </td>
                                <td style="width: 40%; font-size: 11px;">
                                    <asp:Label ID="lblVesselExName1" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: left; font-weight: bold; font-size: 11px; color: Black;
                        background-color: #D8D8D8">
                        Machinery/Maker Details &nbsp;( Machinery name :
                        <asp:Label ID="lblmachinaryname" runat="server"></asp:Label>):
                    </td>
                </tr>
                <tr align="left" valign="top">
                    <td style="width: 8%; font-size: 11px;">
                        <b>M/c Sr. No.: </b>
                    </td>
                    <td style="width: 25%; font-size: 11px;">
                        <asp:Label ID="lblMachinesrno" runat="server"></asp:Label>
                    </td>
                    <td style="width: 8%; font-size: 11px;">
                        <b>Particulars: </b>
                    </td>
                    <td style="width: 30%; font-size: 11px;">
                        <asp:Label ID="lblParticulars" Height="60px" Width="99%" runat="server"></asp:Label>
                    </td>
                    <td style="width: 8%; font-size: 11px;">
                        <b>Model:</b>
                    </td>
                    <td style="width: 30%; font-size: 11px;">
                        <asp:Label ID="lblModel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold; font-size: 11px; text-align: left">
                        Set Installed :
                    </td>
                    <td colspan="5">
                        <asp:Label ID="lblSetInstalled" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr align="left" valign="top">
                    <td style="width: 8%; font-size: 11px;">
                        <b>Maker Name: </b>
                    </td>
                    <td style="width: 25%; font-size: 11px;">
                        <asp:Label ID="lblMakerName" runat="server"></asp:Label>
                    </td>
                    <td style="width: 8%; font-size: 11px;">
                        <b>Maker Contact: </b>
                    </td>
                    <td style="width: 30%; font-size: 11px;">
                        <asp:Label ID="lblMakerContact" Height="60px" runat="server"></asp:Label>
                    </td>
                    <td style="width: 8%; font-size: 11px;">
                        <b style="">Phone: </b>
                    </td>
                    <td style="width: 30%; font-size: 11px;">
                        <asp:Label ID="lblMakerPh" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr align="left" valign="top">
                    <td style="width: 8%; font-size: 11px;">
                        <b style="">Email: </b>
                    </td>
                    <td style="width: 25%; font-size: 11px;">
                        <asp:Label ID="lblMakerEmail" runat="server"></asp:Label>
                    </td>
                    <td style="width: 8%; font-size: 11px;">
                        <b>City: </b>
                    </td>
                    <td style="width: 30%; font-size: 11px;">
                        <asp:Label ID="lblMakerCity" runat="server"></asp:Label>
                    </td>
                    <td style="width: 8%; font-size: 11px; font-family: Tahoma">
                        <b>Address: </b>
                    </td>
                    <td style="font-size: 11px; width: 30%" colspan="3">
                        <asp:Label ID="txtAddress" runat="server" Width="100%" Height="60px"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div id='dvDeclineToQuote' style="display: none; width: 400px" title="Decline to quote">
            <table>
                <tr>
                    <td>
                        Remark
                    </td>
                    <td>
                        <asp:TextBox ID="txtDeclinetoQuoteRemark" runat="server" Height="50px" Width="300px"
                            TextMode="MultiLine" ValidationGroup="vldeclinetoquote"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btndeclinetoquote" OnClick="btnDeclinQuote_Click" runat="server"
                            OnClientClick="return ondeclined();" ValidationGroup="vldeclinetoquote" Text="Decline to Quote" />
                        <input type="button" value="Close" onclick="hideModal('dvDeclineToQuote')" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hdnLink" runat="server" />
    </center>

</asp:Content>
