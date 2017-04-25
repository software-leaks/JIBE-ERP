<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="ASL_CR.aspx.cs" Inherits="ASL_ASL_CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Change Request</title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function OpenScreen(ID, Eval_ID) {
            var url = 'ASL_CR_Approver.aspx?Supp_ID=' + ID + '&Eval_ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_CR_Approver', 'Supplier Change Request Approver', url, 'popup', document.body.offsetHeight / 1.8, document.body.offsetWidth / 1.3, null, null, false, false, true, null);
        }

        $(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        });
        function validation() {
            if (Page_ClientValidate('vgSubmit')) {

                var objCompanyResgName = document.getElementById('<%= txtCompanyResgName.ClientID %>');
                if (objCompanyResgName != null) {
                    var SCode = document.getElementById('txtCompanyResgName').value;
                    var TCode = document.getElementById('txtCNameReason').value;

                    var myElement = document.getElementById('lblRegname1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Company Registered Name reason is mandatory field.");
                                document.getElementById("txtCNameReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("Company Registered Name Current Value and New Value can't be same.");
                            document.getElementById("txtCNameReason").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("Company Registered Name is mandatory field.");
                            document.getElementById("txtCompanyResgName").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("Company Registered Name Current Value and New Value can't be same.");
                            document.getElementById("txtTaxNumber").focus();
                            return false;
                        }
                    }
                }
                var objTaxAccNumber = document.getElementById('<%= txtTaxNumber.ClientID %>');
                if (objTaxAccNumber != null) {
                    var SCode = document.getElementById('txtTaxNumber').value;
                    var TCode = document.getElementById('txtTaxNumberReason').value;

                    var myElement = document.getElementById('lblTaxNumber1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Tax Account Number reason is mandatory field.");
                                document.getElementById("txtTaxNumberReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("Tax Account Number Current Value and New Value can't be same.");
                            document.getElementById("txtTaxNumberReason").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("Tax Account Number is mandatory field.");
                            document.getElementById("txtTaxNumber").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("Tax Account Number Current Value and New Value can't be same.");
                            document.getElementById("txtTaxNumber").focus();
                            return false;

                        }
                    }
                }
                var objSuppAddress = document.getElementById('<%= txtSuppAddress.ClientID %>');
                if (objSuppAddress != null) {
                    var SCode = document.getElementById('txtSuppAddress').value;
                    var TCode = document.getElementById('txtSuppAddressReason').value;

                    var myElement = document.getElementById('lblAddress1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Registered Address reason is mandatory field.");
                                document.getElementById("txtSuppAddressReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("Registered Address Current Value and New Value can't be same.");
                            document.getElementById("txtSuppAddress").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("Registered Address is mandatory field.");
                            document.getElementById("txtSuppAddress").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("Registered Address Current Value and New Value can't be same.");
                            document.getElementById("txtSuppAddress").focus();
                            return false;
                        }
                    }
                }
                var objtxtCity = document.getElementById('<%= txtCity.ClientID %>');
                if (objtxtCity != null) {
                    var SCode = document.getElementById('txtCity').value;
                    var TCode = document.getElementById('txtCityReason').value;

                    var myElement = document.getElementById('lblCity1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("City of Address reason is mandatory field.");
                                document.getElementById("txtCityReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("City of Address Current Value and New Value can't be same.");
                            document.getElementById("txtCity").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("City of Address is mandatory field.");
                            document.getElementById("txtCity").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("City of Address Current Value and New Value can't be same.");
                            document.getElementById("txtCity").focus();
                            return false;
                        }
                    }

                }
                var objEmail = document.getElementById('<%= txtEmail.ClientID %>');
                if (objEmail != null) {
                    var SCode = document.getElementById('txtEmail').value;
                    var TCode = document.getElementById('txtEmailReason').value;

                    var myElement = document.getElementById('lblEmail1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Email reason is mandatory field.");
                                document.getElementById("txtEmailReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("Email Current Value and New Value can't be same.");
                            document.getElementById("txtEmail").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("Email is mandatory field.");
                            document.getElementById("txtEmail").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("Email Current Value and New Value can't be same.");
                            document.getElementById("txtEmail").focus();
                            return false;
                        }
                    }
                }
                var objPhone = document.getElementById('<%= txtPhone.ClientID %>');
                if (objPhone != null) {
                    var SCode = document.getElementById('txtPhone').value;
                    var TCode = document.getElementById('txtPhoneReason').value;

                    var myElement = document.getElementById('lblPhone1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Phone reason is mandatory field.");
                                document.getElementById("txtPhoneReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("Phone Current Value and New Value can't be same.");
                            document.getElementById("txtPhone").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("Phone is mandatory field.");
                            document.getElementById("txtPhone").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("Phone Current Value and New Value can't be same.");
                            document.getElementById("txtPhone").focus();
                            return false;
                        }

                    }
                }
                var objFax = document.getElementById('<%= txtFax.ClientID %>');
                if (objFax != null) {
                    var SCode = document.getElementById('txtFax').value;
                    var TCode = document.getElementById('txtFaxReason').value;

                    var myElement = document.getElementById('lblFax1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Fax reason is mandatory field.");
                                document.getElementById("txtFaxReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("Fax Current Value and New Value can't be same.");
                            document.getElementById("txtFax").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("Fax is mandatory field.");
                            document.getElementById("txtFax").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("Fax Current Value and New Value can't be same.");
                            document.getElementById("txtFax").focus();
                            return false;
                        }
                    }
                }

                var objtxtPICName = document.getElementById('<%= txtPICName.ClientID %>');
                if (objtxtPICName != null) {
                    var SCode = document.getElementById('txtPICName').value;
                    var TCode = document.getElementById('txtPICNameReason').value;

                    var myElement = document.getElementById('lblPICName1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("PIC Name reason is mandatory field.");
                                document.getElementById("txtPICNameReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("PICName Current Value and New Value can't be same.");
                            document.getElementById("txtPICName").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("PIC Name is mandatory field.");
                            document.getElementById("txtPICName").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("PICName Current Value and New Value can't be same.");
                            document.getElementById("txtPICName").focus();
                            return false;
                        }
                    }
                }

                var objPICEmail = document.getElementById('<%= txtPICEmail.ClientID %>');
                if (objPICEmail != null) {
                    var SCode = document.getElementById('txtPICEmail').value;
                    var TCode = document.getElementById('txtPICEmailReason').value;

                    var myElement = document.getElementById('lblPICEmail1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("PIC Email reason is mandatory field.");
                                document.getElementById("txtPICEmailReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("PIC Email Current Value and New Value can't be same.");
                            document.getElementById("txtPICEmail").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("PIC Email is mandatory field.");
                            document.getElementById("txtPICEmail").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("PIC Email Current Value and New Value can't be same.");
                            document.getElementById("txtPICEmail").focus();
                            return false;
                        }
                    }
                }

                var objPICPhone = document.getElementById('<%= txtPICPhone.ClientID %>');
                if (objPICPhone != null) {
                    var SCode = document.getElementById('txtPICPhone').value;
                    var TCode = document.getElementById('txtPICPhoneReason').value;

                    var myElement = document.getElementById('lblPICPhone1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("PIC Phone reason is mandatory field.");
                                document.getElementById("txtPICPhoneReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("PIC Phone Current Value and New Value can't be same.");
                            document.getElementById("txtPICPhone").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("PIC Phone is mandatory field.");
                            document.getElementById("txtPICPhone").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("PIC Phone Current Value and New Value can't be same.");
                            document.getElementById("txtPICPhone").focus();
                            return false;
                        }
                    }
                }

                //var objPICPhone = document.getElementById('<%= txtPICName2.ClientID %>');
                var objtxtPICName2 = document.getElementById('<%= txtPICName2.ClientID %>');
                if (objtxtPICName2 != null) {
                    var SCode = document.getElementById('txtPICName2').value;
                    var TCode = document.getElementById('txtPICName2Reason').value;

                    var myElement = document.getElementById('lblPICName21');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("2nd PIC Name reason is mandatory field.");
                                document.getElementById("txtPICName2Reason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("2nd PIC Name Current Value and New Value can't be same.");
                            document.getElementById("txtPICName2").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("2nd PIC Name is mandatory field.");
                            document.getElementById("txtPICName2").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("2nd PIC Name Current Value and New Value can't be same.");
                            document.getElementById("txtPICName2").focus();
                            return false;
                        }
                    }
                }
                var objPICEmail2 = document.getElementById('<%= txtPICEmail2.ClientID %>');
                if (objPICEmail2 != null) {
                    var SCode = document.getElementById('txtPICEmail2').value;
                    var TCode = document.getElementById('txtPICEmail2Reason').value;

                    var myElement = document.getElementById('lblPICEmail21');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("2nd PIC Email reason is mandatory field.");
                                document.getElementById("txtPICEmail2Reason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("2nd PIC Email Current Value and New Value can't be same.");
                            document.getElementById("txtPICEmail2").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("2nd PIC Email is mandatory field.");
                            document.getElementById("txtPICEmail2").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("2nd PIC Email Current Value and New Value can't be same.");
                            document.getElementById("txtPICEmail2").focus();
                            return false;
                        }
                    }
                }
                var objPICPhone2 = document.getElementById('<%= txtPICPhone2.ClientID %>');
                if (objPICPhone2 != null) {
                    var SCode = document.getElementById('txtPICPhone2').value;
                    var TCode = document.getElementById('txtPICPhone2Reason').value;

                    var myElement = document.getElementById('lblPICPhone21');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("2ndPIC Phone reason is mandatory field.");
                                document.getElementById("txtPICPhone2Reason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("2nd PIC Phone Current Value and New Value can't be same.");
                            document.getElementById("txtPICPhone2").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("2nd PIC Phone is mandatory field.");
                            document.getElementById("txtPICPhone2").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("2nd PIC Phone Current Value and New Value can't be same.");
                            document.getElementById("txtPICPhone2").focus();
                            return false;
                        }
                    }
                }

                var objtxtPayment = document.getElementById('<%= txtPayment.ClientID %>');
                if (objtxtPayment != null) {
                    var SCode = document.getElementById('txtPayment').value;
                    var TCode = document.getElementById('txtBankReason').value;

                    var myElement = document.getElementById('lblPayment1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Payment Instructions reason is mandatory field.");
                                document.getElementById("txtBankReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("Payment Instructions Current Value and New Value can't be same.");
                            document.getElementById("txtPayment").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("Payment Instructions is mandatory field.");
                            document.getElementById("txtPayment").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("Payment Instructions Current Value and New Value can't be same.");
                            document.getElementById("txtPayment").focus();
                            return false;
                        }
                    }
                }

                var objtxtShortName = document.getElementById('<%= txtShortName.ClientID %>');
                if (objtxtShortName != null) {
                    var SCode = document.getElementById('txtShortName').value;
                    var TCode = document.getElementById('txtShortNameReason').value;

                    var myElement = document.getElementById('lblShortname1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Supplier Short Name reason is mandatory field.");
                                document.getElementById("txtShortNameReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("Supplier Short Name Current Value and New Value can't be same.");
                            document.getElementById("txtShortName").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("Supplier Short Name is mandatory field.");
                            document.getElementById("txtShortName").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("Supplier Short Name Current Value and New Value can't be same.");
                            document.getElementById("txtShortName").focus();
                            return false;
                        }
                    }
                }

                var objtxtPaymentEmail = document.getElementById('<%= txtPaymentEmail.ClientID %>');
                if (objtxtPaymentEmail != null) {
                    var SCode = document.getElementById('txtPaymentEmail').value;
                    var TCode = document.getElementById('txtPaymentEmailReason').value;

                    var myElement = document.getElementById('lblPaymentEmail1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Payment Notifications reason is mandatory field.");
                                document.getElementById("txtPaymentEmailReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("Payment Notifications Current Value and New Value can't be same.");
                            document.getElementById("txtPaymentEmail").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("Payment Notifications is mandatory field.");
                            document.getElementById("txtPaymentEmail").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("Payment Notifications Current Value and New Value can't be same.");
                            document.getElementById("txtPaymentEmail").focus();
                            return false;
                        }
                    }
                }

                var objtxtTerms = document.getElementById('<%= txtTerms.ClientID %>');
                if (objtxtTerms != null) {
                    var SCode = document.getElementById('txtTerms').value;
                    var TCode = document.getElementById('txtTermsReason').value;

                    var myElement = document.getElementById('lblTerms1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Terms reason is mandatory field.");
                                document.getElementById("txtTermsReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("Terms Current Value and New Value can't be same.");
                            document.getElementById("txtTerms").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("Terms is mandatory field.");
                            document.getElementById("txtTerms").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("Terms Current Value and New Value can't be same.");
                            document.getElementById("txtTerms").focus();
                            return false;
                        }
                    }
                }
                var objTaxRate = document.getElementById('<%= txtTaxRate.ClientID %>');
                if (objTaxRate != null) {
                    var SCode = document.getElementById('txtTaxRate').value;
                    var TCode = document.getElementById('txtTaxRateReason').value;

                    var myElement = document.getElementById('lblTaxRate1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Tax Rate reason is mandatory field.");
                                document.getElementById("txtTaxRateReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("Tax Rate Current Value and New Value can't be same.");
                            document.getElementById("txtTaxRate").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("Tax Rate is mandatory field.");
                            document.getElementById("txtTaxRate").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("Tax Rate Current Value and New Value can't be same.");
                            document.getElementById("txtTaxRate").focus();
                            return false;
                        }
                    }
                }
                var objbiz = document.getElementById('<%= txtbiz.ClientID %>');
                if (objbiz != null) {
                    var SCode = document.getElementById('txtbiz').value;
                    var TCode = document.getElementById('txtbizReason').value;

                    var myElement = document.getElementById('lblBiz1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Biz Incorporation reason is mandatory field.");
                                document.getElementById("txtbizReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("Biz Incorporation Current Value and New Value can't be same.");
                            document.getElementById("txtbiz").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("Biz Incorporation is mandatory field.");
                            document.getElementById("txtbiz").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("Biz Incorporation Current Value and New Value can't be same.");
                            document.getElementById("txtbiz").focus();
                            return false;
                        }
                    }
                }

                var objtxtSupplierDesc = document.getElementById('<%= txtSupplierDesc.ClientID %>');
                if (objtxtSupplierDesc != null) {
                    var SCode = document.getElementById('txtSupplierDesc').value;
                    var TCode = document.getElementById('txtSupplierDescReason').value;

                    var myElement = document.getElementById('lblSupplierDesc1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Supplier Description reason is mandatory field.");
                                document.getElementById("txtSupplierDescReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("Supplier Description Current Value and New Value can't be same.");
                            document.getElementById("txtSupplierDesc").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("Supplier Description is mandatory field.");
                            document.getElementById("txtSupplierDesc").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("Supplier Description Current Value and New Value can't be same.");
                            document.getElementById("txtSupplierDesc").focus();
                            return false;
                        }
                    }
                }
                var objReg_No = document.getElementById('<%= txtCompany_Reg_No.ClientID %>');
                if (objReg_No != null) {
                    var SCode = document.getElementById('txtCompany_Reg_No').value;
                    var TCode = document.getElementById('txtCompany_Reg_NoReason').value;

                    var myElement = document.getElementById('lblCompany_Reg_No1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Company Reg. No  reason is mandatory field.");
                                document.getElementById("txtCompany_Reg_NoReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("Company Reg. No Current Value and New Value can't be same.");
                            document.getElementById("txtCompany_Reg_NoReason").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("Company Reg. No is mandatory field.");
                            document.getElementById("txtCompany_Reg_No").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("Company Reg. No Current Value and New Value can't be same.");
                            document.getElementById("txtCompany_Reg_No").focus();
                            return false;

                        }
                    }
                }
                var objReg_No = document.getElementById('<%= txtGST_Reg_No.ClientID %>');
                if (objReg_No != null) {
                    var SCode = document.getElementById('txtGST_Reg_No').value;
                    var TCode = document.getElementById('txtGST_Reg_NoReason').value;

                    var myElement = document.getElementById('lblGST_Reg_No1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("GST Reg. No  reason is mandatory field.");
                                document.getElementById("txtGST_Reg_NoReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("GST Reg. No Current Value and New Value can't be same.");
                            document.getElementById("txtGST_Reg_NoReason").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("GST Reg. No is mandatory field.");
                            document.getElementById("txtGST_Reg_No").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("GST Reg. No Current Value and New Value can't be same.");
                            document.getElementById("txtGST_Reg_No").focus();
                            return false;

                        }
                    }
                }
                var objReg_No = document.getElementById('<%= txtWithhold_Tax_Rate.ClientID %>');
                if (objReg_No != null) {
                    var SCode = document.getElementById('txtWithhold_Tax_Rate').value;
                    var TCode = document.getElementById('txtWithhold_Tax_RateReason').value;

                    var myElement = document.getElementById('lblWithhold_Tax_Rate1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("WithHolding Tax Rate reason is mandatory field.");
                                document.getElementById("txtWithhold_Tax_RateReason").focus();
                                return false;
                            }
                        }
                        else if (Scode == lblCode) {
                            alert("WithHolding Tax Rate Current Value and New Value can't be same.");
                            document.getElementById("txtWithhold_Tax_RateReason").focus();
                            return false;
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "") {
                            alert("WithHolding Tax Rate is mandatory field.");
                            document.getElementById("txtWithhold_Tax_Rate").focus();
                            return false;
                        }
                        else if (Scode == lblCode) {
                            alert("WithHolding Tax Rate Current Value and New Value can't be same.");
                            document.getElementById("txtWithhold_Tax_Rate").focus();
                            return false;

                        }
                    }
                }
                var objTaxRate = document.getElementById('<%= ddlType.ClientID %>');

                var ddlReport = document.getElementById("<%=ddlType.ClientID%>");
                if (ddlReport != null) {
                    var SCode = ddlReport.options[ddlReport.selectedIndex].text;
                    //var SCode = document.getElementById('ddlType').value;
                    var TCode = document.getElementById('txtTypeReason').value;

                    var myElement = document.getElementById('lblType1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "No Change") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Supplier type reason is mandatory field.");
                                document.getElementById("txtTypeReason").focus();
                                return false;
                            }
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "No Change") {
                            alert("Supplier Type is mandatory field.");
                            //document.getElementById("txtCompanyResgName").focus();
                            return false;
                        }

                    }
                }

                //             var objTaxRate = document.getElementById('<%= txtTaxRate.ClientID %>');
                //            if (objTaxRate != null) {
                var ddlReport = document.getElementById("<%=ddlSubType.ClientID%>");
                if (ddlReport != null) {
                    var SCode = ddlReport.options[ddlReport.selectedIndex].text;

                    var TCode = document.getElementById('txtSubTypeReason').value;

                    var myElement = document.getElementById('lblSubType1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "No Change") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("CounterParty Type reason is mandatory field.");
                                document.getElementById("txtSubTypeReason").focus();
                                return false;
                            }
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "No Change") {

                            alert("CounterParty type is mandatory field.");
                            //document.getElementById("txtCompanyResgName").focus();
                            return false;

                        }
                    }
                }

                var ddlReport = document.getElementById("<%=ddlCountry.ClientID%>");
                if (ddlReport != null) {
                    var SCode = ddlReport.options[ddlReport.selectedIndex].text;
                    var TCode = document.getElementById('txtCountryReason').value;
                    var myElement = document.getElementById('lblCountry1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "No Change") {
                        if (Text != lblCode) {
                            if (TCode == "") {
                                alert("Country reason is mandatory field.");
                                document.getElementById("txtCountryReason").focus();
                                return false;
                            }
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "No Change") {

                            alert("Country is mandatory field.");
                            //document.getElementById("txtCompanyResgName").focus();
                            return false;

                        }
                    }
                }

                var CurrencySelect = document.getElementById("<%=ddlCurrency.ClientID%>");
                if (CurrencySelect != null) {
                    var SCode = CurrencySelect.options[CurrencySelect.selectedIndex].text;
                    var TCode = document.getElementById('txtCurrencyReason').value;
                    var myElement = document.getElementById('lblCurrency1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "No Change") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Currency reason is mandatory field.");
                                document.getElementById("txtCurrencyReason").focus();
                                return false;
                            }
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "No Change") {

                            alert("Currency is mandatory field.");
                            //document.getElementById("txtCompanyResgName").focus();
                            return false;

                        }
                    }
                }
                //             var objTaxRate = document.getElementById('<%= txtTaxRate.ClientID %>');
                //            if (objTaxRate != null) {
                var OwnerShipSelect = document.getElementById("<%=ddlownerShip.ClientID%>");
                if (OwnerShipSelect != null) {
                    var SCode = OwnerShipSelect.options[OwnerShipSelect.selectedIndex].text;
                    //var SCode = document.getElementById('ddlownerShip').value;
                    var TCode = document.getElementById('txtownerShipReason').value;
                    //var lblCode = document.getElementById('<%=lblOwnerShip1.ClientID%>').innerText;
                    var myElement = document.getElementById('lblOwnerShip1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "No Change") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("OwnerShip reason is mandatory field.");
                                document.getElementById("txtownerShipReason").focus();
                                return false;
                            }
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "No Change") {

                            alert("OwnerShip is mandatory field.");
                            //document.getElementById("txtCompanyResgName").focus();
                            return false;

                        }
                    }
                }
                //             var objTaxRate = document.getElementById('<%= txtTaxRate.ClientID %>');
                //            if (objTaxRate != null) {
                var IntervalSelect = document.getElementById("<%=ddlPaymentInterval.ClientID%>");
                if (IntervalSelect != null) {
                    var SCode = IntervalSelect.options[IntervalSelect.selectedIndex].text;
                    var TCode = document.getElementById('txtPaymentIntervalReason').value;
                    var myElement = document.getElementById('lblPaymenyInterval1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "No Change") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Payment Interval reason is mandatory field.");
                                document.getElementById("txtPaymentIntervalReason").focus();
                                return false;
                            }
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "No Change") {

                            alert("Payment Interval is mandatory field.");
                            //document.getElementById("txtCompanyResgName").focus();
                            return false;

                        }
                    }
                }
                //             var objTaxRate = document.getElementById('<%= txtTaxRate.ClientID %>');
                //            if (objTaxRate != null) {
                var PaymentSelect = document.getElementById("<%=ddlPaymentTerms.ClientID%>");
                if (PaymentSelect != null) {
                    var SCode = PaymentSelect.options[PaymentSelect.selectedIndex].text;
                    var TCode = document.getElementById('txtPaymentTermsReason').value;
                    var myElement = document.getElementById('lblPaymentTerms1');
                    var lblCode = (myElement.innerText || myElement.textContent);
                    var Scode = SCode.trim()
                    var TCode = TCode.trim()
                    if (Scode != "No Change") {
                        if (Scode != lblCode) {
                            if (TCode == "") {
                                alert("Payment Terms reason is mandatory field.");
                                document.getElementById("txtPaymentTermsReason").focus();
                                return false;
                            }
                        }
                    }
                    if (TCode != "") {
                        if (Scode == "No Change") {

                            alert("Payment Terms is mandatory field.");
                            //document.getElementById("txtCompanyResgName").focus();
                            return false;

                        }
                    }
                }
                //             var objTaxRate = document.getElementById('<%= txtTaxRate.ClientID %>');
                //            if (objTaxRate != null) {
                var RB1 = document.getElementById("<%=rdbInvoiceStatus.ClientID%>");
                if (RB1 != null) {
                    var radio = RB1.getElementsByTagName("input");
                    //var label = RB1.getElementsByTagName("label");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) {
                            var TCode = document.getElementById('txtInvoiceStatusReason').value;
                            var myElement = document.getElementById('lblInvoiceStatus1');
                            var myText = (myElement.innerText || myElement.textContent);
                            var TCode = TCode.trim()
                            if (radio[i].value != myText) {
                                if (TCode == "") {
                                    alert("Enable Invoice Status Reason is mandatory field.");
                                    document.getElementById("txtInvoiceStatusReason").focus();
                                    return false;
                                }
                            }
                            if (TCode != "") {
                                if (radio[i].value == myText) {
                                    alert("Enable Invoice Status Current value and New value should not be same.");
                                    document.getElementById("txtInvoiceStatusReason").focus();
                                    return false;
                                }
                            }
                        }
                    }
                }
                //             var objTaxRate = document.getElementById('<%= txtTaxRate.ClientID %>');
                //            if (objTaxRate != null) {
                var RB1 = document.getElementById("<%=rdbdirectinvoice.ClientID%>");
                if (RB1 != null) {
                    var radio = RB1.getElementsByTagName("input");
                    //var label = RB1.getElementsByTagName("label");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) {
                            var TCode = document.getElementById('txtdirectinvoiceReason').value;
                            var myElement = document.getElementById('lbldirectinvoice1');
                            var myText = (myElement.innerText || myElement.textContent);
                            var TCode = TCode.trim()
                            if (radio[i].value != myText) {
                                if (TCode == "") {
                                    alert("Direct Invoice Upload Reason is mandatory field.");
                                    document.getElementById("txtdirectinvoiceReason").focus();
                                    return false;
                                }
                            }
                            if (TCode != "") {
                                if (radio[i].value == myText) {
                                    alert("Direct Invoice Upload Current value and New value should not be same.");
                                    document.getElementById("txtdirectinvoiceReason").focus();
                                    return false;
                                }
                            }

                        }
                    }
                }
                var RB1 = document.getElementById("<%=rdbPaymentPriority.ClientID%>");
                if (RB1 != null) {
                    var radio = RB1.getElementsByTagName("input");
                    //var label = RB1.getElementsByTagName("label");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) {
                            var TCode = document.getElementById('txtPaymentPriorityReason').value;
                            var myElement = document.getElementById('lblPaymenypriority1');
                            var myText = (myElement.innerText || myElement.textContent);
                            var TCode = TCode.trim()
                            if (radio[i].value != myText) {
                                if (TCode == "") {
                                    alert("Payment Priority Reason is mandatory field.");
                                    document.getElementById("txtPaymentPriorityReason").focus();
                                    return false;
                                }
                            }
                            if (TCode != "") {
                                if (radio[i].value == myText) {
                                    alert("Payment Priority Current value and New value should not be same.");
                                    document.getElementById("txtPaymentPriorityReason").focus();
                                    return false;
                                }
                            }

                        }
                    }
                }

                var RB1 = document.getElementById("<%=rdbPaymentHistory.ClientID%>");
                if (RB1 != null) {
                    var radio = RB1.getElementsByTagName("input");
                    //var label = RB1.getElementsByTagName("label");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) {
                            var TCode = document.getElementById('txtPaymentHistoryReason').value;
                            var myElement = document.getElementById('lblPaymentHistory1');
                            var myText = (myElement.innerText || myElement.textContent);
                            var TCode = TCode.trim()
                            if (radio[i].value != myText) {
                                if (TCode == "") {
                                    alert("Payment History View Reason is mandatory field.");
                                    document.getElementById("txtPaymentHistoryReason").focus();
                                    return false;
                                }
                            }
                            if (TCode != "") {
                                if (radio[i].value == myText) {
                                    alert("Payment History View Current value and New value should not be same.");
                                    document.getElementById("txtPaymentHistoryReason").focus();
                                    return false;
                                }
                            }
                        }
                    }
                }
                var RB1 = document.getElementById("<%=rdbAutoSendPO.ClientID%>");
                if (RB1 != null) {
                    var radio = RB1.getElementsByTagName("input");
                    //var label = RB1.getElementsByTagName("label");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) {
                            var TCode = document.getElementById('txtAutoSendPO').value;
                            var myElement = document.getElementById('lblAutoSendPO1');
                            var myText = (myElement.innerText || myElement.textContent);
                            var TCode = TCode.trim()
                            if (radio[i].value != myText) {
                                if (TCode == "") {
                                    alert("Auto Send PO Reason is mandatory field.");
                                    document.getElementById("txtAutoSendPO").focus();
                                    return false;
                                }
                            }
                            if (TCode != "") {
                                if (radio[i].value == myText) {
                                    alert("Auto Send PO Current value and New value should not be same.");
                                    document.getElementById("txtAutoSendPO").focus();
                                    return false;
                                }
                            }
                        }
                    }
                }
                var ChklistScope = document.getElementById("<%=chkScope.ClientID%>");
                var str = "";
                var str1 = "";
                if (ChklistScope != null) {
                    var Scopecheckbox = ChklistScope.getElementsByTagName("input");
                    var label = ChklistScope.getElementsByTagName("label");
                    for (var i = 0; i < Scopecheckbox.length; i++) {
                        if (Scopecheckbox[i].checked) {
                            str = label[i].innerHTML + ',' + str;
                        }
                    }
                    str1 = str.substring(0, str.length - 1);
                }

                var TCode = document.getElementById('txtScopeReason').value;
                //var lblCode = document.getElementById('<%=lblScope1.ClientID%>').innerText;
                var myElement = document.getElementById('lblScope1');
                var lblCode = (myElement.innerText || myElement.textContent);

                var TCode = TCode.trim()
                if (str1 != "") {
                    if (str1 != lblCode) {
                        if (TCode == "") {
                            alert("Scope Name Reason is mandatory field.");
                            document.getElementById("txtScopeReason").focus();
                            return false;
                        }
                    }
                }
                if (TCode != "") {
                    if (str1 == "") {
                        alert("Scope Name  is mandatory field.");
                        document.getElementById("txtScopeReason").focus();
                        return false;
                    }
                    else if (str1 == lblCode) {
                        alert("Scope Name Current value and New value should not be same.");
                        document.getElementById("txtScopeReason").focus();
                        return false;
                    }

                }

                var ChklistPort = document.getElementById("<%=chkPort.ClientID%>");
                var str = "";
                var str1 = "";
                if (ChklistPort != null) {
                    var Portcheckbox = ChklistPort.getElementsByTagName("input");
                    var label = ChklistPort.getElementsByTagName("label");
                    for (var i = 0; i < Portcheckbox.length; i++) {
                        if (Portcheckbox[i].checked) {
                            str = label[i].innerHTML + ',' + str;
                        }
                    }
                    str1 = str.substring(0, str.length - 1);
                }

                var TCode = document.getElementById('txtPortReason').value;
                var myElement = document.getElementById('lblPort1');
                var lblCode = (myElement.innerText || myElement.textContent);
                var TCode = TCode.trim()
                if (str1 != "") {
                    if (str1 != lblCode) {
                        if (TCode == "") {
                            alert("Port Name Reason is mandatory field.");
                            document.getElementById("txtPortReason").focus();
                            return false;
                        }
                    }
                }
                if (TCode != "") {
                    if (str1 == "") {
                        alert("Port Name is mandatory field.");
                        document.getElementById("txtPortReason").focus();
                        return false;
                    }
                    else if (str1 == lblCode) {
                        alert("Port Name Current value and New value should not be same.");
                        document.getElementById("txtPortReason").focus();
                        return false;
                    }

                }

                var ChklistProperties = document.getElementById("<%=gvProperties.ClientID%>");
                var str = "";
                var str1 = "";
                if (ChklistProperties != null) {
                    var Propertiesheckbox = ChklistProperties.getElementsByTagName("input");
                    var label = ChklistProperties.getElementsByTagName("label");
                    for (var i = 0; i < Propertiesheckbox.length; i++) {
                        if (Propertiesheckbox[i].checked) {
                            str = label[i].innerHTML + ',' + str;
                        }
                    }
                    str1 = str.substring(0, str.length - 1);
                }

                var TCode = document.getElementById('txtSupplier_PropertiesReason').value;
                //var lblCode = document.getElementById('<%=lblScope1.ClientID%>').innerText;
                var myElement = document.getElementById('lblSupplier_Properties1');
                var lblCode = (myElement.innerText || myElement.textContent);

                var TCode = TCode.trim()
                if (str1 != "") {
                    if (str1 != lblCode) {
                        if (TCode == "") {
                            alert("Supplier Property Reason is mandatory field.");
                            document.getElementById("txtSupplier_PropertiesReason").focus();
                            return false;
                        }
                    }
                }
                if (TCode != "") {
                    if (str1 == "") {
                        alert("Supplier Property  is mandatory field.");
                        document.getElementById("txtSupplier_PropertiesReason").focus();
                        return false;
                    }
                    else if (str1 == lblCode) {
                        alert("Supplier Property Current value and New value should not be same.");
                        document.getElementById("txtSupplier_PropertiesReason").focus();
                        return false;
                    }

                }

                return true
            }
            return false;
        }

    </script>
    <style type="text/css">
        div
        {
            margin: 5px;
        }
        
        #tblChangeRequest
        {
            font-family: Arial, Verdana, Sans-Serif;
            font-size: 12px;
        }
        
        #tblChangeRequest td
        {
            text-align: left;
            border: solid 1px #cccccc;
        }
        #tblChangeRequest1
        {
            font-family: Arial, Verdana, Sans-Serif;
            font-size: 12px;
        }
        
        #tblChangeRequest1 td
        {
            text-align: left;
            border: solid 1px #cccccc;
        }
    </style>
    <script type="text/javascript">
        function myFunction() {

            var size = {
                width: window.innerWidth || document.body.clientWidth,
                height: window.innerHeight || document.body.clientHeight
            }
            var w = (size.width);
            var y = (size.height);

            var body = document.body,
             html = document.documentElement;

            var height = Math.max(body.scrollHeight, body.offsetHeight,
                       html.clientHeight, html.scrollHeight, html.offsetHeight);
            document.getElementById('blur-on-updateprogress').setAttribute("style", "height:" + height + "px");
            //            document.getElementById('iFrame1').setAttribute("style", "width:" + w * 0.65 + "px");
            //            document.getElementById('iFrame1').style.overflow = scroll;
            //alert(height);


        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            window.parent.$("#ASL_CR").css("height", (parseInt($("#pnlChangeRequest").height()) + 50) + "px");
            window.parent.$(".xfCon").css("height", (parseInt($("#pnlChangeRequest").height()) + 50) + "px").css("top", "50px");
        });
    </script>
</head>
<body style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 99%;">
<table  style="border: 0px solid #cccccc; font-family: Tahoma; overflow: Auto; font-size: 12px; width: 100%;">
                                 <tr><td>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptM" runat="server">
    </asp:ScriptManager>
   
    
    <div class="error-message" onclick="javascript:this.style.display='none';">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    <center>
    <asp:Panel ID="pnlChangeRequest" runat="server" Visible="true">
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
        <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
            color: Black; height: 100%;">
            <asp:UpdatePanel ID="upd1" runat="server">
                <ContentTemplate>
                    <center>
                        <div id="page-title" class="page-title">
                            Change Request
                        </div>
                        <table width="100%">
                            <tr id="trSubmitted" runat="server">
                                <td align="right" style="color: #FF0000;">
                                    <asp:Button ID="btnGroup" runat="server" Text="ASL Column Group Relationship" OnClientClick='OpenScreen(null,null);return false;' />&nbsp;&nbsp;&nbsp;&nbsp;
                                    Supplier Code &nbsp;:&nbsp;<asp:Label ID="lblSupplierCode" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                    Submitted By &nbsp;:&nbsp;
                                    <asp:Label ID="lblSubmittedBY" runat="server" Text=""></asp:Label>&nbsp;&nbsp; Submitted
                                    Date &nbsp;:&nbsp;<asp:Label ID="lblSubmitteddate" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                        <table border="1" width="100%" cellpadding="5" cellspacing="0">
                            <tr>
                                <td align="center">
                                    <strong>Data Fields</strong>
                                </td>
                                <td align="center">
                                    <strong>Current Value</strong>
                                </td>
                                <td align="center">
                                    <strong>New Value</strong>
                                </td>
                                <td align="center">
                                    <strong>Reason For Change</strong>
                                </td>
                                <td align="center" id="tdHeader" runat="server" visible="false">
                                    <strong>Select</strong>
                                </td>
                            </tr>
                            <tr id="trRegname" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblRegname" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblRegname1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtCompanyResgName" runat="server" MaxLength="500" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtCNameReason" runat="server" MaxLength="500" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkCname" visible="false" runat="server">
                                    <asp:CheckBox ID="chkCname" runat="server" />
                                    <asp:HiddenField ID="hdnCName" runat="server" />
                                </td>
                            </tr>
                            <tr id="trType" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblType" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblType1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtTypeReason" runat="server" MaxLength="500" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkType" visible="false" runat="server">
                                    <asp:CheckBox ID="chkType" runat="server" />
                                    <asp:HiddenField ID="hdnType" runat="server" />
                                </td>
                            </tr>
                            <tr id="trTaxNumber" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblTaxNumber" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblTaxNumber1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtTaxNumber" runat="server" MaxLength="500" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtTaxNumberReason" runat="server" MaxLength="500" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdTaxNumber" visible="false" runat="server">
                                    <asp:CheckBox ID="chkTaxNumber" runat="server" />
                                    <asp:HiddenField ID="hdnTaxNumber" runat="server" />
                                </td>
                            </tr>
                            <tr id="trSubType" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblSubType" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblSubType1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:DropDownList ID="ddlSubType" runat="server" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtSubTypeReason" runat="server" MaxLength="500" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkSubType" visible="false" runat="server">
                                    <asp:CheckBox ID="chkSubType" runat="server" />
                                    <asp:HiddenField ID="hdnSubType" runat="server" />
                                </td>
                            </tr>
                            <tr id="trAddress" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblAddress1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtSuppAddress" TextMode="MultiLine" MaxLength="1000" Height="40px"
                                        Width="300px" runat="server" CssClass="txtInput"> </asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtSuppAddressReason" TextMode="MultiLine" runat="server" MaxLength="500"
                                        Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkAddress" visible="false" runat="server">
                                    <asp:CheckBox ID="chkAddress" runat="server" />
                                    <asp:HiddenField ID="hdnSuppAddress" runat="server" />
                                </td>
                            </tr>
                            <tr id="trCountry" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblCountry" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblCountry1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtCountryReason" runat="server" MaxLength="500" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkCountry" visible="false" runat="server">
                                    <asp:CheckBox ID="chkCountry" runat="server" />
                                    <asp:HiddenField ID="hdnCountry" runat="server" />
                                </td>
                            </tr>
                            <tr id="trCity" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblCity1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtCity" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtCityReason" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkCity" visible="false" runat="server">
                                    <asp:CheckBox ID="chkCity" runat="server" />
                                    <asp:HiddenField ID="hdnCity" runat="server" />
                                </td>
                            </tr>
                            <tr id="trEmail" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblEmail1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="255" Width="300px" ValidationGroup="vgSubmit" CssClass="txtInput"></asp:TextBox>
                                   
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtEmailReason" runat="server" MaxLength="500" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkEmail" visible="false" runat="server">
                                    <asp:CheckBox ID="chkEmail" runat="server" />
                                    <asp:HiddenField ID="hdnEmail" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPhone" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPhone1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPhone" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                  
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPhoneReason" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPhone" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPhone" runat="server" />
                                    <asp:HiddenField ID="hdnPhone" runat="server" />
                                </td>
                            </tr>
                            <tr id="trFax" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblFax" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblFax1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtFax" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                 
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtFaxReason" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkFax" visible="false" runat="server">
                                    <asp:CheckBox ID="chkFax" runat="server" />
                                    <asp:HiddenField ID="hdnFax" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPICName" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPICName" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPICName1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICName" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICNameReason" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPICName" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPICName" runat="server" />
                                    <asp:HiddenField ID="hdnPICName" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPICEmail" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPICEmail" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPICEmail1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICEmail" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                   
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICEmailReason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPICEmail" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPICEmail" runat="server" />
                                    <asp:HiddenField ID="hdnPICEmail" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPICPhone1" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPICPhone" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPICPhone1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICPhone" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                  
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICPhoneReason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPICPhone" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPICPhone" runat="server" />
                                    <asp:HiddenField ID="hdnPICPhone" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPICName2" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPICName2" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPICName21" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICName2" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICName2Reason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPICName2" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPICName2" runat="server" />
                                    <asp:HiddenField ID="hdnPICName2" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPICEmail2" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPICEmail2" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPICEmail21" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICEmail2" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                  
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICEmail2Reason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPICEmail2" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPICEmail2" runat="server" />
                                    <asp:HiddenField ID="hdnPICEmail2" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPICPhone2" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPICPhone2" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPICPhone21" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICPhone2" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                   
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPICPhone2Reason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPICPhone2" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPICPhone2" runat="server" />
                                    <asp:HiddenField ID="hdnPICPhone2" runat="server" />
                                </td>
                            </tr>
                            <tr id="trShortname" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblShortname" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblShortname1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtShortName" MaxLength="255" Width="300px" runat="server" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtShortNameReason" MaxLength="255" Width="300px" runat="server"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkShortName" visible="false" runat="server">
                                    <asp:CheckBox ID="chkShortName" runat="server" />
                                    <asp:HiddenField ID="hdnShortName" runat="server" />
                                </td>
                            </tr>
                            <tr id="trScope" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblScope" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblScope1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" valign="top" style="width: 300px">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <biv style="float: left; text-align: left; width: 300px;">
                                                <asp:DropDownList ID="ddlScope" runat="server" Width="300px" CssClass="txtInput">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnScopeAdd" runat="server" Text="Add Scope" OnClick="btnAdd_Click" />
                                                <br />
                                               <div style="float: left; text-align: left; width: 300px; height: 80px; overflow-x: hidden;
                                                            border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                            background-color: #ffffff;">
                                                            <asp:CheckBoxList ID="chkScope"  RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                                runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                </biv>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtScopeReason" runat="server" MaxLength="1000" Width="300px" CssClass="txtInput"
                                        Height="130px" TextMode="MultiLine"> </asp:TextBox>
                                </td>
                                <td align="left" id="tdchkScope" visible="false" runat="server">
                                    <asp:CheckBox ID="chkScope1" runat="server" />
                                    <asp:HiddenField ID="hdnScope" runat="server" />
                                    <asp:HiddenField ID="hdnAddScope" runat="server" />
                                </td>
                            </tr>
                            <tr id="trSupplierDesc" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblSupplierDesc" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblSupplierDesc1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtSupplierDesc" MaxLength="2000" Height="90px" Width="300px" TextMode="MultiLine"
                                        runat="server" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtSupplierDescReason" MaxLength="255" Height="90px" TextMode="MultiLine"
                                        Width="300px" runat="server" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkSupplierDesc" visible="false" runat="server">
                                    <asp:CheckBox ID="chkSupplierDesc" runat="server" />
                                    <asp:HiddenField ID="hdnSupplierDesc" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPort" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPort" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPort1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" valign="top" style="width: 300px">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div style="float: left; text-align: left; width: 300px;">
                                                <asp:DropDownList ID="ddlPort" runat="server" Width="300px" CssClass="txtInput">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnPortAdd" runat="server" Text="Add Port" OnClick="btnPortAdd_Click" />
                                                <br />
                                                <div style="float: left; text-align: left; width: 300px; height: 80px; overflow-x: hidden;
                                                    border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                    background-color: #ffffff;">
                                                    <asp:CheckBoxList ID="chkPort" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPortReason" runat="server" MaxLength="1000" Width="300px" CssClass="txtInput"
                                        Height="130px" TextMode="MultiLine"> </asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPort" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPort1" runat="server" />
                                    <asp:HiddenField ID="hdnPort" runat="server" />
                                    <asp:HiddenField ID="hdnAddPort" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPayment" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPayment" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPayment1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPayment" runat="server" MaxLength="1000" Width="300px" CssClass="txtInput"
                                        Height="130px" TextMode="MultiLine"> </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBankReason" runat="server" MaxLength="1000" Width="300px" CssClass="txtInput"
                                        Height="130px" TextMode="MultiLine"> </asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPayment" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPayment" runat="server" />
                                    <asp:HiddenField ID="hdnPayment" runat="server" />
                                </td>
                            </tr>
                            <tr id="trPaymentEmail" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPaymentEmail" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPaymentEmail1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPaymentEmail" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPaymentEmailReason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPaymentEmail" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPaymentEmail" runat="server" />
                                    <asp:HiddenField ID="hdnPaymentEmail" runat="server" />
                                </td>
                            </tr>
                            <tr id="trCurrency" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblCurrency1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtCurrencyReason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkCurrency" visible="false" runat="server">
                                    <asp:CheckBox ID="chkCurrency" runat="server" />
                                    <asp:HiddenField ID="hdnCurrency" runat="server" />
                                </td>
                            </tr>
                            <tr id="trTerms" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblTerms" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblTerms1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtTerms" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtTermsReason" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkTerms" visible="false" runat="server">
                                    <asp:CheckBox ID="chkTerms" runat="server" />
                                    <asp:HiddenField ID="hdnTerms" runat="server" />
                                </td>
                            </tr>
                            <tr id="trTaxRate" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblTaxRate" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblTaxRate1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtTaxRate" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                  
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtTaxRateReason" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkTaxRate" visible="false" runat="server">
                                    <asp:CheckBox ID="chkTaxRate" runat="server" />
                                    <asp:HiddenField ID="hdnTaxRate" runat="server" />
                                </td>
                            </tr>
                            <tr id="trOwnerShip" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblOwnerShip" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblOwnerShip1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:DropDownList ID="ddlownerShip" runat="server" CssClass="txtInput" Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtownerShipReason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkOwnership" visible="false" runat="server">
                                    <asp:CheckBox ID="chkOwnership" runat="server" />
                                    <asp:HiddenField ID="hdnownerShip" runat="server" />
                                </td>
                            </tr>
                            <tr id="trbiz" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblBiz" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblBiz1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtbiz" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                   
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtbizReason" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkBiz" visible="false" runat="server">
                                    <asp:CheckBox ID="chkBiz" runat="server" />
                                    <asp:HiddenField ID="hdnBiz" runat="server" />
                                </td>
                            </tr>
                            <tr id="trInvoiceStatus" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblInvoiceStatus" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblInvoiceStatus1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:RadioButtonList ID="rdbInvoiceStatus" CssClass="txtInput" Width="200px" runat="server"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtInvoiceStatusReason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkInvoiceStatus" visible="false" runat="server">
                                    <asp:CheckBox ID="chkInvoiceStatus" runat="server" />
                                    <asp:HiddenField ID="hdnInvoiceStatus" runat="server" />
                                </td>
                            </tr>
                            <tr id="trdirectinvoice" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lbldirectinvoice" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lbldirectinvoice1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:RadioButtonList ID="rdbdirectinvoice" CssClass="txtInput" Width="200px" runat="server"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtdirectinvoiceReason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkDirectInvoice" visible="false" runat="server">
                                    <asp:CheckBox ID="chkDirectInvoice" runat="server" />
                                    <asp:HiddenField ID="hdndirectinvoice" runat="server" />
                                </td>
                            </tr>
                            <tr id="TRPaymenyInterval" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPaymenyInterval" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPaymenyInterval1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:DropDownList ID="ddlPaymentInterval" CssClass="txtInput" Width="300px" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPaymentIntervalReason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPaymentInterval" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPaymentInterval" runat="server" />
                                    <asp:HiddenField ID="hdnPaymentInterval" runat="server" />
                                </td>
                            </tr>
                            <tr id="TRPaymenypriority" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPaymenypriority" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPaymenypriority1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:RadioButtonList ID="rdbPaymentPriority" CssClass="txtInput" Width="300px" runat="server"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Normal">Normal</asp:ListItem>
                                        <asp:ListItem Value="Immediate">Immediate</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPaymentPriorityReason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPaymentPriority" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPaymentPriority" runat="server" />
                                    <asp:HiddenField ID="hdnPaymentPriority" runat="server" />
                                </td>
                            </tr>
                            <tr id="TRPaymentTerms" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPaymentTerms" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPaymentTerms1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:DropDownList ID="ddlPaymentTerms" CssClass="txtInput" Width="300px" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPaymentTermsReason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPaymentTerms" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPaymentTerms" runat="server" />
                                    <asp:HiddenField ID="hdnPaymentTerms" runat="server" />
                                </td>
                            </tr>
                            <tr id="TRPaymentHistory" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblPaymentHistory" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblPaymentHistory1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:RadioButtonList ID="rdbPaymentHistory" CssClass="txtInput" Width="200px" runat="server"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtPaymentHistoryReason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdchkPaymentHistory" visible="false" runat="server">
                                    <asp:CheckBox ID="chkPaymentHistory" runat="server" />
                                    <asp:HiddenField ID="hdnPaymentHistory" runat="server" />
                                </td>
                            </tr>
                            <tr id="TRAutoSendPO" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblAutoSendPO" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblAutoSendPO1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:RadioButtonList ID="rdbAutoSendPO" CssClass="txtInput" Width="200px" runat="server"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtAutoSendPO" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdAutoSendPO" visible="false" runat="server">
                                    <asp:CheckBox ID="chkAutoSendPO" runat="server" />
                                    <asp:HiddenField ID="hdnAutoSendPO" runat="server" />
                                </td>
                            </tr>
                            <tr id="trCompany_Reg_No" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblCompany_Reg_No" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblCompany_Reg_No1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtCompany_Reg_No" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtCompany_Reg_NoReason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdCompany_Reg_No" visible="false" runat="server">
                                    <asp:CheckBox ID="chkCompany_Reg_No" runat="server" />
                                    <asp:HiddenField ID="hdnCompany_Reg_No" runat="server" />
                                </td>
                            </tr>
                            <tr id="trGST_Reg_No" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblGST_Reg_No" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblGST_Reg_No1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtGST_Reg_No" runat="server" MaxLength="255" Width="300px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtGST_Reg_NoReason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdGST_Reg_No" visible="false" runat="server">
                                    <asp:CheckBox ID="chkGST_Reg_No" runat="server" />
                                    <asp:HiddenField ID="hdnGST_Reg_No" runat="server" />
                                </td>
                            </tr>
                            <tr id="trWithhold_Tax_Rate" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblWithhold_Tax_Rate" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblWithhold_Tax_Rate1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtWithhold_Tax_Rate" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtWithhold_Tax_RateReason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdWithhold_Tax_Rate" visible="false" runat="server">
                                    <asp:CheckBox ID="chkWithhold_Tax_Rate" runat="server" />
                                    <asp:HiddenField ID="hdnWithhold_Tax_Rate" runat="server" />
                                </td>
                            </tr>
                            <tr id="trSupplier_Properties" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblSupplier_Properties" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblSupplier_Properties1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                     <div style="float: left; text-align: left; Width:300px; height: 40px; overflow-x: hidden;
                                                                                border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                                                background-color: #ffffff;">
                                                                                <asp:CheckBoxList ID="gvProperties" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                                                                </asp:CheckBoxList>
                                                                            </div>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtSupplier_PropertiesReason" runat="server" TextMode="MultiLine" MaxLength="255" Width="300px" Height="40px" CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdSupplier_Properties" visible="false" runat="server">
                                    <asp:CheckBox ID="chkSupplier_Properties" runat="server" />
                                    <asp:HiddenField ID="hdnSupplier_Properties" runat="server" />
                                </td>
                            </tr>
                             <tr id="trClientCode" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblClientCode" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblClientCode1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtClientCode" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtClientCode_Reason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdClientCode" visible="false" runat="server">
                                    <asp:CheckBox ID="chkClientCode" runat="server" />
                                    <asp:HiddenField ID="hdnClientCode" runat="server" />
                                </td>
                            </tr>
                             <tr id="trSupplierShortCode" visible="false" runat="server">
                                <td align="right" style="width: 200px">
                                    <asp:Label ID="lblSupplierShortCode" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:Label ID="lblSupplierShortCode1" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtSupplierShortCode" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 300px">
                                    <asp:TextBox ID="txtSupplierShortCode_Reason" runat="server" MaxLength="255" Width="300px"
                                        CssClass="txtInput"></asp:TextBox>
                                </td>
                                <td align="left" id="tdSupplierShortCode" visible="false" runat="server">
                                    <asp:CheckBox ID="chkSupplierShortCode" runat="server" />
                                    <asp:HiddenField ID="hdnSupplierShortCode" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </center>
                    <div>
                        <center>
                            <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td align="center" style="background-color: #DDDDDD">
                                        <asp:Button ID="btnSaveDraft" runat="server" Width="100px" Text="Save Draft" ValidationGroup="vgSubmit"
                                            OnClick="btnSaveDraft_Click" OnClientClick="return validation();" />&nbsp;&nbsp;
                                        <%-- <asp:Button ID="btnRecallDraft" runat="server" Width="100px" Text="Recall Draft"
                                            ValidationGroup="vgSubmit" OnClick="btnRecallDraft_Click" />&nbsp;&nbsp;
                                      <asp:Button ID="btnSubmitRequest" runat="server" Width="100px" Text="Submit Request"
                                            ValidationGroup="vgSubmit" OnClick="btnSubmitRequest_Click" OnClientClick="return validation();" />&nbsp;&nbsp;--%>
                                        <%-- <asp:Button ID="btnRecallRequest" runat="server"  Width="100px" Enabled="false" Text="Recall Request"
                                            OnClick="btnRecallRequest_Click" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnApprove" Visible="false" runat="server" Width="200px" OnClientClick="return validation();" Text="Approve Selected Changes"
                                            OnClick="btnApprove_Click" />&nbsp;&nbsp;
                                             <asp:Button ID="btnRecallApprove" runat="server"  Width="150px" Enabled="false" Text="Recall Approved Request"
                                            OnClick="btnRecallRequest_Click" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnFinalApprove" Visible="false" runat="server" Width="200px" OnClientClick="return validation();" 
                                            Text="Final Approve Selected Changes" onclick="btnFinalApprove_Click"
                                             />   
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnReject" Visible="false" runat="server" Width="200px" Text="Reject Selected Changes"
                                            OnClick="btnReject_Click" />--%>
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="vgSubmit" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
            <asp:HiddenField ID="hdnCRID" runat="server" />
        </div>
        <div>
        <asp:RegularExpressionValidator ID="regNoteEmail" Display="None" runat="server" ValidationGroup="vgSubmit"
                            ErrorMessage="Company EmailID is not Valid" ControlToValidate="txtEmail"
                            ValidationExpression="^([a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])?\.)+[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])[;,|]?)+$"
                            ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>

           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="None" runat="server" ValidationGroup="vgSubmit"
                            ErrorMessage="PIC EmailID is not Valid" ControlToValidate="txtPICEmail"
                            ValidationExpression="^([a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])?\.)+[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])[;,|]?)+$"
                            ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
           <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="None" runat="server" ValidationGroup="vgSubmit"
                            ErrorMessage="2nd PIC EmailID is not Valid" ControlToValidate="txtPICEmail2"
                            ValidationExpression="^([a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])?\.)+[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])[;,|]?)+$"
                            ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>

              <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="None" runat="server" ValidationGroup="vgSubmit"
                            ErrorMessage="Payment Notifications EmailID is not Valid" ControlToValidate="txtPaymentEmail"
                            ValidationExpression="^([a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9A-Z!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])?\.)+[a-z0-9A-Z](?:[a-z0-9A-Z-]*[a-z0-9A-Z])[;,|]?)+$"
                            ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="None" runat="server" ValidationGroup="vgSubmit"
                            ErrorMessage="Company Phone is not Valid" ControlToValidate="txtPhone"
                            ValidationExpression="^[ 0-9,()+-]+$"
                            ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="None" runat="server" ValidationGroup="vgSubmit"
                            ErrorMessage="Company Fax is not Valid" ControlToValidate="txtFax"
                            ValidationExpression="^[ 0-9,()+-]+$"
                            ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="None" runat="server" ValidationGroup="vgSubmit"
                            ErrorMessage="PIC Phone is not Valid" ControlToValidate="txtPICPhone"
                            ValidationExpression="^[ 0-9,()+-]+$"
                            ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator7" Display="None" runat="server" ValidationGroup="vgSubmit"
                            ErrorMessage="2nd PIC Phone is not Valid" ControlToValidate="txtPICPhone2"
                            ValidationExpression="^[ 0-9,()+-]+$"
                            ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator> 
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator8" Display="None" runat="server" ValidationGroup="vgSubmit"
                            ErrorMessage="Biz Incorporation is not Valid" ControlToValidate="txtbiz"
                            ValidationExpression="^[ 0-9,()+-]+$"
                            ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" Display="None" runat="server" ValidationGroup="vgSubmit"
                            ErrorMessage="Tax Rate is not Valid" ControlToValidate="txtTaxRate"
                            ValidationExpression="^[0-9.]+$"
                            ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator10" Display="None" runat="server" ValidationGroup="vgSubmit"
                            ErrorMessage="Withhold Tax Rate is not Valid" ControlToValidate="txtWithhold_Tax_Rate"
                            ValidationExpression="^[0-9.]+$"
                            ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
        </div>
    </asp:Panel>
    </center>
    </form>
    </td></tr></table>
</body>
</html>