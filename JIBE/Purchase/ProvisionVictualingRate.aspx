<%@ Page Title="Victualing Rate" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProvisionVictualingRate.aspx.cs" Inherits="Purchase_ProvisionVictualingRate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function validationOnAdd() {

            //        var cmbparentType =document.getElementById("ctl00_MainContent_cmbParent").value;
            var TxtVictualingRate = document.getElementById("ctl00_MainContent_txtVictualingRate").value;
            var TxtFromDate = document.getElementById("ctl00_MainContent_txtFromDate").value;
            //var txtToDate = document.getElementById("ctl00_MainContent_txtToDate").value;
            var txtVessel = document.getElementById("ctl00_MainContent_ddlVessel").value;

            if (txtVessel == "--Select--") {
                alert("Please Select Vessel");
                document.getElementById("ctl00_MainContent_ddlVessel").focus();
                return false;
            }
            if (TxtFromDate == "") {
                alert("Please enter date from");
                document.getElementById("ctl00_MainContent_txtFromDate").focus();
                return false;
            }
            //            if (txtToDate == "") {
            //                alert("Please enter date to.");
            //                document.getElementById("ctl00_MainContent_txtToDate").focus();
            //                return false;
            //            }
            if (TxtVictualingRate == "") {
                alert("Please enter victualing rate.");
                document.getElementById("ctl00_MainContent_txtVictualingRate").focus();
                return false;
            }
            if (TxtVictualingRate != "") {
                if (parseFloat(TxtVictualingRate) == 0) {
                    alert("Victualling Rate Can not be Zero.");
                    document.getElementById("ctl00_MainContent_txtVictualingRate").focus();
                    return false;

                }
            }

            return true;
        }

        function NumbersOnly(e) {
            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    if (!IsUserFriendlyChar(key, "Numbers")) {
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        }

        //-------------------------------------------
        // Function to only allow decimal data entry
        //-------------------------------------------
        function DecimalsOnly(e) {
            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    if (!IsUserFriendlyChar(key, "Decimals")) {
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        }

        function IsUserFriendlyChar(val, step) {
            // Backspace, Tab, Enter, Insert, and Delete
            if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
                return true;
            }
            // Ctrl, Alt, CapsLock, Home, End, and Arrows
            if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
                return true;
            }
            if (step == "Decimals") {
                if (val == 190 || val == 110) {
                    return true;
                }
            }
            // The rest
            return false;
        }
        function Validate(event) {
            var regex = new RegExp("^/[0-9|\b|/?=.]/+$");
            var key = String.fromCharCode(event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                return false;
            }
        }

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<center>
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 1000px;
            height: 100%;">
            <div style="font-size: 24px; background-color: #5588BB; width: 1000px; color: White;
                text-align: center;">
                <b>Victualing Rate </b>
            </div>
            <div style="height: 650px; width: 1000px; color: Black;">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr align="left">
                                    <td width="10%" align="right" valign="top">
                                        Fleet :
                                    </td>
                                    <td width="15%" valign="top" align="left">
                                        <uc1:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                            Height="150" Width="160" />
                                    </td>
                                    <td width="10%" align="right" valign="top">
                                        Vessel :
                                    </td>
                                    <td width="15%" valign="top" align="left">
                                        <uc1:ucCustomDropDownList ID="DDLVesselF" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                                            Height="200" Width="160" />
                                    </td>
                                    <td align="right" style="width: 10%">
                                        <%-- Month/Year :&nbsp;--%>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txtInput" Visible="false" Width="100%"></asp:TextBox>
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" Height="23" OnClick="btnFilter_Click"
                                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" OnClick="ImgAdd_Click" ImageUrl="~/Images/Add-icon.png"
                                            Text="Add Victualing Rate" ToolTip="Add New Victualing Rate" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="overflow-x: hidden; overflow-y: none; height: 600px; width: 1000px;">
                            <asp:GridView ID="rgdVictulingRate" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" OnRowDataBound="rgdVictulingRate_RowDataBound" Width="100%"
                                GridLines="Both" AllowSorting="true" OnSorting="rgdVictulingRate_Sorting" CellPadding="1"
                                CellSpacing="0">
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle Font-Size="12px" CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Vessel
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Victualing Rate
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container,"DataItem.Victualing_Rate") %>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Effective Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.Victualing_From_Date")%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                            CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif"
                                                            Height="16px"></asp:ImageButton>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                            Visible="false" OnClientClick="return confirm('Are you sure want to delete?')"
                                                            CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                            Height="16px"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindrgdVictulingRate" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" />
                            <asp:HiddenField ID="HiddenItemID" runat="server" />
                        </div>
                        <div id="divaddLocation" title="<%= OperationMode %>" style="display: none; border: 1px solid Black;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 400px;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                <td align="center">
                                <asp:Label ID="lblError" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td align="right" style="width: 25%">
                                                    Vessel &nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlVessel" runat="server" Width="70%" AppendDataBoundItems="True"
                                                        CssClass="txtReadOnly">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Effective Date :
                                                </td>
                                                <td style="color: #FF0000; font-size: small; width: 1%" align="left">
                                                    *
                                                </td>
                                                
                                                    <td valign="top" align="left">
                                                        <asp:TextBox ID="txtFromDate" CssClass="input" runat="server" Width="120px"  onkeypress="return Validate(event);"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                                            Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                
                                            </tr>
                                           
                                            <tr>
                                                <td align="right">
                                                    Victualing Rate
                                                </td>
                                                 <td style="color: #FF0000; font-size: small; width: 1%" align="left">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtVictualingRate" runat="server" Width="120px" CssClass="txtInput"  AutoCompleteType="Disabled"  onkeydown="javascript:return DecimalsOnly(event);" ></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td align="center">
                                                    <asp:TextBox ID="txtCode" Width="16px" Visible="false" runat="server"></asp:TextBox>
                                                    <asp:Button ID="btnSaveLocation" runat="server" Text="Save" Height="24px" OnClick="DivbtnSave_Click"
                                                        OnClientClick="return validationOnAdd();" Style="font-size: small" Width="60px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="color: #FF0000; font-size: small;" align="center">
                                        * Indicates as mandatory fields
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>

