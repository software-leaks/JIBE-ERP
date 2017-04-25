<%@ Page Title="Vessel General Parameters" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LibVesselGeneralParameters.aspx.cs" Inherits="Operations_LibVesselGeneralParameters" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Validation() {
            if (document.getElementById($('[id$=ddlVessel]').attr('id')).value == "0") {
                alert("Please select a vessel.");
                document.getElementById($('[id$=ddlVessel]').attr('id')).focus();
                return false;
            }
            if (document.getElementById($('[id$=txtPropellorPitch]').attr('id')).value == "") {
                alert("Please enter a value for Propellor Pitch for the vessel.");
                document.getElementById($('[id$=txtPropellorPitch]').attr('id')).focus();
                return false;
            }
            if (document.getElementById($('[id$=txtbxQmcr]').attr('id')).value == "") {
                alert("Please enter a value for Qmcr for the vessel.");
                document.getElementById($('[id$=txtbxQmcr]').attr('id')).focus();
                return false;
            }
            if (document.getElementById($('[id$=txtbxRPM_Max]').attr('id')).value == "") {
                alert("Please enter a value for RPM Max for the vessel.");
                document.getElementById($('[id$=txtbxRPM_Max]').attr('id')).focus();
                return false;
            }
            if (document.getElementById($('[id$=txtMCR_Power]').attr('id')).value == "") {
                alert("Please enter a value for M.E.Rated Power for the vessel.");
                document.getElementById($('[id$=txtMCR_Power]').attr('id')).focus();
                return false;
            }
            if (document.getElementById($('[id$=txtMCR_Power_Engine]').attr('id')).value == "") {
                alert("Please enter a value for MCR Power for Engine Curve for the vessel.");
                document.getElementById($('[id$=txtMCR_Power_Engine]').attr('id')).focus();
                return false;
            }
            if (document.getElementById($('[id$=txtSFOC]').attr('id')).value == "") {
                alert("Please enter a value for M.E. SFOC for the vessel.");
                document.getElementById($('[id$=txtSFOC]').attr('id')).focus();
                return false;
            }
            if (document.getElementById($('[id$=txtCylOil]').attr('id')).value == "") {
                alert("Please enter a value for Fuel dependent/RPM Dependent for the vessel.");
                document.getElementById($('[id$=txtCylOil]').attr('id')).focus();
                return false;
            }
            return true;
        }
        function IsOneDecimalPoint(evt) {
            var controlid = $(evt.target)[0].id;

            var keyCode = (evt.which) ? evt.which : evt.keyCode;

            var ret = ((keyCode >= 48 && keyCode <= 57) || keyCode == 46 || keyCode == 8 || keyCode == 9 || keyCode == 8 || keyCode == 39 || keyCode == 37);
            if (ret == false) {
                alert("Only Numeric value allowed.");
            }
            if (ret == true) {

                var textboxvalue = document.getElementById(controlid).value;

                if (keyCode == 46) {
                    textboxvalue = textboxvalue + ".";
                    var parts = textboxvalue.split('.');

                    if (parts.length > 2)
                        ret = false;
                }

            }
            return ret;
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 1090px;
            height: 100%;">
            <div class="page-title">
                Vessel General Parameters
            </div>
            <div style="height: 650px; width: 1090px; color: Black;">
                <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                    <table width="100%" cellpadding="0" cellspacing="4">
                        <tr>
                            <td align="right" style="width: 12%">
                                Fleet :&nbsp;
                            </td>
                            <td align="left" style="width: 30%">
                                <asp:DropDownList ID="ddlFleet" runat="server" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td align="right" style="width: 12%">
                                Vessel :&nbsp;
                            </td>
                            <td align="left" style="width: 20%">
                                <asp:DropDownList ID="ddlVesselList" runat="server" OnSelectedIndexChanged="ddlVesselList_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                            <table align="center" cellpadding="2" cellspacing="1"><tr><td> <asp:ImageButton ID="btnFilter" runat="server" ToolTip="Search" ImageUrl="~/Images/SearchButton.png"
                                    OnClick="btnFilter_Click" /></td>
                                    <td align="center">
                                <asp:ImageButton ID="btnRefresh" runat="server" ToolTip="Refresh" ImageUrl="~/Images/Refresh-icon.png"
                                    OnClick="btnRefresh_Click" />
                            </td>
                            <td align="center">
                                <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add Vessel General Parameter" ImageUrl="~/Images/Add-icon.png"
                                    OnClick="ImgAdd_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" ImageUrl="~/Images/Exptoexcel.png"
                                    OnClick="ImgExpExcel_Click" Style="height: 22px" />
                            </td>
                                    </tr></table>

                               
                            </td>

                            
                        </tr>
                        <tr>
                            <td colspan="8">
                                <div style="padding-left: 20px;width:965px">
                                    <asp:GridView ID="gvVesselGeneralParameters" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="GeneralParameterID" EmptyDataText="No Record Found" AllowSorting="True"
                                        CaptionAlign="Bottom" CellPadding="4" Font-Size="14px" GridLines="None" Width="953px"
                                        CssClass="gridmain-css">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("GeneralParameterID")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVesselID" runat="server" Text='<%# Eval("VesselID")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vessel Name" HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    Vessel Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtVesselName" Font-Size="12px" MaxLength="50" Width="400px" runat="server"
                                                        Text='<%#Bind("VesselName")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Propellor Pitch" HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    Propellor Pitch
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPropellorPitch" runat="server" Text='<%#Eval("Propeller_Pitch")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPropellorPitch" Font-Size="12px" MaxLength="50" Width="400px"
                                                        runat="server" Text='<%#Bind("Propeller_Pitch")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qmcr" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    Qmcr
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQmcr" runat="server" Text='<%#Eval("Qmcr")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtQmcr" Font-Size="12px" MaxLength="50" Width="400px" runat="server"
                                                        Text='<%#Bind("Qmcr")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RPM Max" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    RPM Max
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRPM_Max" runat="server" Text='<%#Eval("RPM_Max")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtQmcr" Font-Size="12px" MaxLength="50" Width="400px" runat="server"
                                                        Text='<%#Bind("RPM_Max")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="M.E.Rated Power" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false">
                                                <HeaderTemplate>
                                                   M.E.Rated Power
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmcr_power" runat="server" Text='<%#Eval("Abs_eng_rat_power")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtmcr_power" Font-Size="12px" MaxLength="50" Width="400px" runat="server"
                                                        Text='<%#Bind("Abs_eng_rat_power")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="MCR Power For Engine Curve" HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false">
                                                <HeaderTemplate>
                                                   MCR Power For Engine Curve
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEng_rat_power" runat="server" Text='<%#Eval("Eng_rat_power")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEng_rat_power" Font-Size="12px" MaxLength="50" Width="400px" runat="server"
                                                        Text='<%#Bind("Eng_rat_power")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="M.E SFOC" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    M.E SFOC
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSFOC" runat="server" Text='<%#Eval("SFOC")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSFOC" Font-Size="12px" MaxLength="50" Width="400px" runat="server"
                                                        Text='<%#Bind("SFOC")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fuel Dependent/RPM Dependent" HeaderStyle-HorizontalAlign="Left"  HeaderStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    Fuel Dependent/RPM Dependent
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCyl_oil_calc" runat="server" Text='<%#Eval("Cyl_oil_calc_mothod")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCyl_oil_calc" Font-Size="12px" MaxLength="50" Width="400px" runat="server"
                                                        Text='<%#Bind("Cyl_oil_calc_mothod")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    Action
                                                </HeaderTemplate>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="btnAccept" runat="server" AlternateText="Update" CausesValidation="False"
                                                        CommandName="Update" ImageUrl="~/images/accept.png" />
                                                    <asp:ImageButton ID="btnReject" runat="server" AlternateText="Cancel" CausesValidation="False"
                                                        CommandName="Cancel" ImageUrl="~/images/reject.png" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit" CausesValidation="False"
                                                        OnCommand="onUpdate" CommandArgument='<%#Eval("[GeneralParameterID]")%>' ImageUrl="~/images/edit.gif" /><%--Visible='<%# uaEditFlag %>'--%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left" ShowHeader="False">
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete" CausesValidation="False"
                                                        CommandArgument='<%#Eval("[GeneralParameterID]")%>' OnCommand="onDelete" ImageUrl="~/images/delete.png"
                                                        OnClientClick="return confirm('Are you sure, you want to delete the record?')" />
                                                    <%--Visible='<%# uaDeleteFlage %>'--%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle CssClass="crew-interview-grid-alternaterow" />
                                        <EditRowStyle CssClass="crew-interview-grid-editrow" />
                                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle CssClass="crew-interview-grid-row" />
                                        <SortedAscendingCellStyle CssClass="crew-interview-grid-col-sorted-asc" />
                                        <SortedAscendingHeaderStyle CssClass="crew-interview-grid-header-sorted-asc" />
                                        <SortedDescendingCellStyle CssClass="crew-interview-grid-col-sorted-desc" />
                                        <SortedDescendingHeaderStyle CssClass="crew-interview-grid-header-sorted-desc" />
                                    </asp:GridView>
                                    <div style="width:99%">
                                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="20" OnBindDataItem="BindVesselParameters"/>
                                    </div>
                                    <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divadd" title="Add Vessel Parameters" style="display:none ; font-family: Tahoma;
                    text-align: left; font-size: 12px; color: Black; width: 300px; height:290px;">
                    <table width="100%" cellpadding="2" cellspacing="2">
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 150px">
                                Vessel
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlVessel" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 150px;text-align:left">
                                Propellor Pitch
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPropellorPitch" CssClass="txtInput" Width="150px" runat="server"
                                    onkeypress="return IsOneDecimalPoint(event);" MaxLength="8"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 150px">
                                Qmcr
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtbxQmcr" CssClass="txtInput" Width="150px" runat="server" MaxLength="8"
                                    onkeypress="return IsOneDecimalPoint(event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 150px">
                                RPM Max
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtbxRPM_Max" CssClass="txtInput" Width="150px" runat="server" MaxLength="8"
                                    onkeypress="return IsOneDecimalPoint(event);"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td align="left" style="width: 150px;text-align:left">
                                 M.E.Rated Power
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtMCR_Power" CssClass="txtInput" Width="150px" runat="server" MaxLength="8" 
                                    onkeypress="return IsOneDecimalPoint(event);"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td align="left" style="width: 150px">
                                MCR Power For Engine Curve
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtMCR_Power_Engine" CssClass="txtInput" Width="150px" runat="server" MaxLength="8"
                                    onkeypress="return IsOneDecimalPoint(event);"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td align="left" style="width: 150px">
                                M.E SFOC
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSFOC" CssClass="txtInput" Width="150px" runat="server" MaxLength="8"
                                    onkeypress="return IsOneDecimalPoint(event);"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td align="left" style="width: 150px">
                               Fuel Dependent/RPM Dependent
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCylOil" CssClass="txtInput" Width="150px" runat="server" MaxLength="8"
                                    onkeypress="return IsOneDecimalPoint(event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="3">
                                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return Validation();" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </center>
</asp:Content>
