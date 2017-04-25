<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMSMachineryChangeRequestAction.aspx.cs"
    Inherits=" PMSMachineryChangeRequestAction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/ctlVesselLocationList.ascx" TagName="ctlVesselLocationList"
    TagPrefix="ucVesslLocation" %>
<%@ Register Src="../../UserControl/ctlFunctionList.ascx" TagName="ctlFunctionList"
    TagPrefix="ucFunction" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Approve / Reject - Change Request</title>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <script language="javascript" type="text/javascript">

        function CloseDiv() {
            var control = document.getElementById("divAddLocation");
            control.style.visibility = "hidden";
        }

        function validationApprove() {



            var txtCrName = document.getElementById("txtCrName").value;
            var CatalogueDepartment = document.getElementById("ddlCrDepartment").value;
            var Account = document.getElementById("ddlAccountCode").value;
            var Maker = document.getElementById("ddlCatalogMaker").value;
            var CrSetsInstalled = document.getElementById("txtCrSetsInstalled").value;
            var CrFunction = document.getElementById("ddlFunction").value;

            var CrRemark = document.getElementById("txtCrRemark").value;
            

            var hdfAddNewFlag = document.getElementById("hdfAddNewFlag").value;


            if (txtCrName == "") {
                alert('Machinery name is required.')
                document.getElementById("txtCrName").focus();
                return false;
            }

            if (CrSetsInstalled != "") {
                if (isNaN(CrSetsInstalled)) {
                    alert('Set installed is accept ony numeric value.')
                    document.getElementById("txtCrSetsInstalled").focus();
                    return false;
                }
            }

            if (CatalogueDepartment == "0") {
                alert("Catalogue Department is required.");
                document.getElementById("ddlCrDepartment").focus();
                return false;
            }

            if (Account == "0") {
                alert("Account Code is required.");
                document.getElementById("ddlAccountCode").focus();
                return false;
            }



            if (hdfAddNewFlag == "ADDNEW") {
                if (Maker == "0") {
                    alert("Maker is required.");
                    document.getElementById("ddlCatalogMaker").focus();
                    return false;
                }
            }


            if (CrFunction == "0") {
                alert("Function is required.");
                document.getElementById("ddlFunction").focus();
                return false;
            }

            if (CrRemark == "") {
                alert("Remark is required.")
                document.getElementById("txtCrRemark").focus();
                return false;
            }

            return true;
        }


        function OnAddNewLocation() {

            if (document.getElementById('dvAddNewLocation').style.display == 'block') {
                document.getElementById('dvAddNewLocation').style.display = 'none';

            }
            else {
                document.getElementById('dvAddNewLocation').style.display = 'block';
                document.getElementById('txtLoc_ShortCode').focus();
            }

            return false;

        }

        function validationReject() 
        {

            var txtCrName = document.getElementById("txtCrName").value;
            var CatalogueDepartment = document.getElementById("ddlCrDepartment").value;
            var Account = document.getElementById("ddlAccountCode").value;
            var Maker = document.getElementById("ddlCatalogMaker").value;
            var CrSetsInstalled = document.getElementById("txtCrSetsInstalled").value;
            var CrFunction = document.getElementById("ddlFunction").value;

            var CrRemark = document.getElementById("txtCrRemark").value;


            var hdfAddNewFlag = document.getElementById("hdfAddNewFlag").value;


            if (txtCrName == "") {
                alert('Machinery name is required.')
                document.getElementById("txtCrName").focus();
                return false;
            }

            if (CrSetsInstalled != "") {
                if (isNaN(CrSetsInstalled)) {
                    alert('Set installed is accept ony numeric value.')
                    document.getElementById("txtCrSetsInstalled").focus();
                    return false;
                }
            }

            if (CatalogueDepartment == "0") {
                alert("Catalogue Department is required.");
                document.getElementById("ddlCrDepartment").focus();
                return false;
            }

            if (Account == "0") {
                alert("Account Code is required.");
                document.getElementById("ddlAccountCode").focus();
                return false;
            }



            if (hdfAddNewFlag == "ADDNEW") {
                if (Maker == "0") {
                    alert("Maker is required.");
                    document.getElementById("ddlCatalogMaker").focus();
                    return false;
                }
            }


            if (CrFunction == "0") {
                alert("Function is required.");
                document.getElementById("ddlFunction").focus();
                return false;
            }

            if (CrRemark == "") {
                alert("Remark is required.")
                document.getElementById("txtCrRemark").focus();
                return false;
            }

            return true;
        }

    </script>
    <form id="frmCatalogue" runat="server" style="vertical-align: top" defaultbutton="btnDivApprove"
    defaultfocus="btnRetrieve">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updCatalogue">
        <ContentTemplate>
            <center>
                <div style="font-family: Tahoma; font-size: 12px; width: 100%">
                    <table cellpadding="0" cellspacing="0" style="position: relative;" width="100%">
                        <tr>
                            <td align="center" style="vertical-align: top;">
                                <div style="height: 350px; border: 1px solid #cccccc; vertical-align: top; padding: 0px;">
                                    <table cellspacing="1" cellpadding="1" width="100%" style="vertical-align: top;">
                                        <tr>
                                            <td align="center" colspan="2" style="background-color: Green; color: White">
                                                <b>Original</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 15%">
                                                Name :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" style="width: 50%">
                                                <asp:TextBox ID="txtName" runat="server" Width="280px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Particular :&nbsp;&nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtParticular" runat="server" Height="50px" TextMode="MultiLine"
                                                    Width="280px" ReadOnly="true" CssClass="txtReadOnly" Font-Names="Tahoma" Font-Size="10pt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Sets Ins. :&nbsp;&nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSetsInstalled" runat="server" Width="48px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Maker :&nbsp;&nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlMaker" Width="284px" ReadOnly="true" CssClass="txtReadOnly"
                                                    runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Model :&nbsp;&nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtModel" runat="server" Width="280px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Sr. No. :&nbsp;&nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSerialNo" runat="server" Width="280px" ReadOnly="true" CssClass="txtReadOnly"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Dept. :&nbsp;&nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlDepartment" Width="284px" runat="server" ReadOnly="true"
                                                    CssClass="txtReadOnly">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td align="center" style="vertical-align: top;">
                                <div style="height: 500px; border: 1px solid #cccccc;">
                                    <table cellspacing="1" cellpadding="1" width="100%">
                                        <tr>
                                            <td align="right" style="width: 25%; background-color: Red;">
                                                <b>Request For :</b>&nbsp;&nbsp;
                                            </td>
                                            <td colspan="2" align="left" style="width: 50%">
                                                <asp:Label ID="lblReqstFor" runat="server" />
                                            </td>
                                            <td colspan="1" align="center" style="width: 15%">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 15%">
                                                Name :&nbsp;&nbsp;
                                            </td>
                                            <td style="color: Red">
                                                *
                                            </td>
                                            <td align="left" style="width: 50%">
                                                <asp:TextBox ID="txtCrName" runat="server" CssClass="txtInput" Width="280px"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 15%">
                                                A/c Code : &nbsp;
                                            </td>
                                            <td style="color: Red">
                                                *
                                            </td>
                                            <td align="left" style="width: 50%">
                                                <asp:DropDownList ID="ddlAccountCode" runat="server" CssClass="txtInput" Width="280px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Particular :&nbsp;&nbsp;
                                            </td>
                                            <td></td>
                                            <td align="left" style="width: 50%">
                                                <asp:TextBox ID="txtCrParticular" runat="server" CssClass="txtInput" Font-Names="Tahoma"
                                                    Font-Size="10pt" Height="50px" TextMode="MultiLine" Width="280px"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 15%">
                                            </td>
                                            <td align="left" style="width: 50%">
                                                &nbsp;
                                            </td>
                                            <td align="left" style="width: 5%" valign="middle">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Sets Ins. :&nbsp;&nbsp;
                                            </td>
                                            <td></td>
                                            <td align="left" style="margin-left: 160px">
                                                <asp:TextBox ID="txtCrSetsInstalled" runat="server" CssClass="txtInput" Width="48px"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 25%">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                               
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Maker :&nbsp;&nbsp;
                                            </td>
                                            <td></td>
                                            <td align="right" style="text-align: left">
                                                <asp:TextBox ID="txtCrMakerDetials" runat="server" CssClass="txtReadOnly" Font-Names="Tahoma"
                                                    Font-Size="10pt" ReadOnly="true" Width="280px"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 25%">
                                                Maker :&nbsp;&nbsp;
                                            </td>
                                            <td style="color: Red">
                                                *
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCatalogMaker" runat="server" CssClass="txtInput" Width="280px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Model :&nbsp;&nbsp;
                                            </td>
                                            <td></td>
                                            <td align="right" style="text-align: left">
                                                <asp:TextBox ID="txtCrModel" runat="server" CssClass="txtInput" Width="280px"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                Functions :&nbsp;&nbsp;
                                            </td>
                                            <td style="color: Red">
                                                *
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlFunction" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                    CssClass="txtInput" Width="280px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Sr. No.:&nbsp;&nbsp;
                                            </td>
                                            <td></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCrSerialNo" runat="server" CssClass="txtInput" Width="280px"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td align="left" style="width: 50%">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Dept.:&nbsp;&nbsp;
                                            </td>
                                            <td></td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlCrDepartment" Width="284px" runat="server" Enabled="false">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Dept.Change Remark :&nbsp;&nbsp;
                                            </td>
                                            <td></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCrDepartChangeRemark" runat="server" Height="50px" TextMode="MultiLine"
                                                    Width="280px" Font-Names="Tahoma" Font-Size="10pt" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 15%">
                                            </td>
                                            <td align="left" style="width: 50%">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Reason :&nbsp;&nbsp;
                                            </td>
                                            <td></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCrReason" runat="server" Height="50px" TextMode="MultiLine" Width="280px"
                                                    Font-Names="Tahoma" Font-Size="10pt" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 15%">
                                            </td>
                                            <td align="left" style="width: 50%">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Requested By :&nbsp;&nbsp;
                                            </td>
                                            <td></td>
                                            <td colspan="3" style="text-align: left">
                                                <asp:Label ID="lblRequestedBy" runat="server" Style="text-align: left"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Actioned By :&nbsp;&nbsp;
                                            </td>
                                            <td></td>
                                            <td colspan="3" style="text-align: left">
                                                <asp:Label ID="lblActionedBy" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Remark :&nbsp;&nbsp;
                                            </td>
                                            <td style="color:Red">
                                            *
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCrRemark" runat="server" Font-Names="Tahoma" CssClass="txtInput"
                                                    Font-Size="10pt" Height="50px" TextMode="MultiLine" Width="280px"></asp:TextBox>
                                                 <asp:RequiredFieldValidator runat="server" id="reqName" controltovalidate="txtCrRemark" errormessage="Please enter Remark!" />
                                            </td>
                                            <td align="right" style="width: 15%">
                                                &nbsp;
                                            </td>
                                            <td align="left" style="width: 50%">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr style="background-color: #cccccc">
                                            <td align="right">
                                                <asp:HiddenField ID="hdfAddNewFlag" runat="server" />
                                            </td>
                                            <td align="center" colspan="3">
                                                <asp:Button ID="btnDivApprove" runat="server" OnClick="btnDivApprove_Click" OnClientClick="return validationApprove();"
                                                    Text="Approve" Width="80px" />&nbsp;&nbsp;
                                                <asp:Button ID="btnDivReject" runat="server" OnClick="btnDivReject_Click"  OnClientClick="return validationReject();"  
                                                  Text="Reject"   Width="80px" />&nbsp;&nbsp;
                                                   <input type="button" name="btnCancel" style="font-size: 12px; width: 100px" value="Close"
                                                    onclick="javascript:parent.ReloadParent_ByButtonID();">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdAddLocation">
        <ContentTemplate>
            <div id="divAddLocation" title="Assign Location" style="font-family: Tahoma; font-size: 12px;
                display: none; border: 1px solid Gray; height: 500px; width: 580px;">
                <center>
                    <table cellpadding="1" cellspacing="1" width="95%" style="position: relative;">
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            Search:
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtSearchLocation" onFocus="javascript:this.select()" CssClass="txtInput"
                                                Width="340px" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="left" style="width: 10%;">
                                            <asp:ImageButton ID="imgLocationSearch" ImageUrl="~/Purchase/Image/preview.gif" runat="server"
                                                OnClick="imgLocationSearch_Click" />
                                        </td>
                                        <td align="right" style="width: 35%;">
                                            Add Location
                                        </td>
                                        <td align="right" style="width: 5%;">
                                            <asp:ImageButton ID="imgAddNewLocation" runat="server" OnClientClick="return OnAddNewLocation();"
                                                Height="15px" ToolTip="Add New Location" ImageUrl="~/Images/AddMaker.png" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="dvAddNewLocation" style="display: none; border: 1px solid #cccccc; background-color: #E0F2F7;
                                    height: 60px;">
                                    <table cellpadding="2" cellspacing="2">
                                        <tr>
                                            <td align="right" style="width: 20%">
                                                Short Code :&nbsp;
                                            </td>
                                            <td style="width: 1%; color: Red;">
                                                *
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLoc_ShortCode" Width="260px" runat="server" CssClass="txtInput"></asp:TextBox>
                                            </td>
                                            <td align="right" style="color: red">
                                                * Mandatory fields.
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 20%">
                                                Location Name :&nbsp;
                                            </td>
                                            <td style="width: 1%; color: Red;">
                                                *
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLoc_Description" Width="260px" runat="server" CssClass="txtInput"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnDivLocationSave" runat="server" Text="Save Location" OnClientClick="return OnSaveLocation();"
                                                    OnClick="btnDivLocationSave_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="max-height: 350px; overflow: auto; background-color: #006699; z-index: 3;
                                    margin-left: 40px;">
                                    <asp:GridView ID="gvLocation" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                        OnRowDataBound="gvLocation_RowDataBound" Width="100%" OnSelectedIndexChanging="gvLocation_SelectedIndexChanging"
                                        OnRowCommand="gvLocation_RowCommand" AllowSorting="true" OnSorting="gvLocation_Sorting"
                                        DataKeyNames="LocationID">
                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                        <RowStyle CssClass="PMSGridRowStyle-css" />
                                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID">
                                                <HeaderTemplate>
                                                    ID
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDivLocationCode" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LocationID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Short Code">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblDivShortCodeHeader" runat="server" ForeColor="White">Short Code&nbsp;</asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDivShortCode" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LocationShortCode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location Name">
                                                <HeaderTemplate>
                                                    Location Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDivLocationName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LocationName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Machinery">
                                                <HeaderTemplate>
                                                    Machinery Assigned
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDivMachinery" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.System_Description") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID">
                                                <HeaderTemplate>
                                                    Select
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDivAssingLoc" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem, "LocAssignFlag").ToString() =="1"? true : false %>'
                                                        Enabled='<%#DataBinder.Eval(Container.DataItem, "LocAssignFlag").ToString() =="1"? false : true %>' />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="PMSGridItemStyle-css"></ItemStyle>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Spare">
                                                    <HeaderTemplate>
                                                        Is Spare Equipment
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkIsSpare" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem, "Category_Code").ToString() =="SP"? true : false %>'
                                                             />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <uc1:ucCustomPager ID="ucDivCustomPager" runat="server" OnBindDataItem="BindCatalogueAssignLocation" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="background-color: #d8d8d8;">
                                <asp:Button ID="btnDivSave" runat="server" Text="Save" OnClick="btnDivSave_click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnDivCancel" runat="server" Text="Cancel" OnClientClick="hideModal('divAddLocation');return false;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblErrMsg" ForeColor="Blue" runat="server" Width="200px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
