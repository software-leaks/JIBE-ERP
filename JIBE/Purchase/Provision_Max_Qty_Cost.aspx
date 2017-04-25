<%@ Page Title="Provision's Approval Limit" Language="C#" MasterPageFile="~/Site.master" 
    AutoEventWireup="true" CodeFile="Provision_Max_Qty_Cost.aspx.cs" Inherits="Purchase_Provision_Max_Qty_Cost" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="ucpager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <style type="text/css">
        .tdhFilter
        {
            font-size: 12px;
            text-align: right;
            vertical-align: middle;
            font-weight: normal;
            width: 100px;
            color: Black;
        }
        .tddFilter
        {
            font-size: 12px;
            text-align: left;
            vertical-align: middle;
        }
        
        .maxlimit-css-number
        {
            cursor: pointer;
            text-align: center;
            padding: 2px;
        }
        .HeaderStyle-css-bulkreport
        {
            background: url(../Images/gridheaderbg-image.png) left -5px repeat-x;
            color: #333333;
            background-color: #ADD8E6;
            font-size: 11px;
            padding: 2px 15px 2px 15px;
            text-align: center;
            border: 1px solid #cccccc;
            border-collapse: collapse;
        }
    </style>
    <script type="text/javascript">


        var prv_qty_id, prv_cost_id, prv_vessel_id, prv_item_ref_code, prv_userid;

        function EditCostAmount(Vessel_id, item_ref_code, lblid, user_id, item_name, evt, objthis) {

            SetBackgroundColor_selecteddRow("transparent");

            prv_item_ref_code = item_ref_code;
            prv_vessel_id = Vessel_id;
            prv_userid = user_id;

            var lblQtyId = "";
            var lblCostid = "";
            var arrid = lblid.split("_");
            if ((arrid[arrid.length - 1] % 2) == 0) {
                prv_qty_id = lblid;
                var colnumber = parseInt(arrid[arrid.length - 1]) + 1;
                prv_cost_id = lblid.replace('_' + arrid[arrid.length - 1].toString(), '_' + colnumber.toString())

            }
            else {
                prv_cost_id = lblid;
                var colnumber = parseInt(arrid[arrid.length - 1]) - 1;
                prv_qty_id = lblid.replace('_' + arrid[arrid.length - 1].toString(), '_' + colnumber.toString());

            }

            document.getElementById('txtNewMaxQty').value = parseFloat(document.getElementById(prv_qty_id).innerHTML).toString() != 'NaN' ? document.getElementById(prv_qty_id).innerHTML : '';
            document.getElementById('txtNewMaxCost').value = parseFloat(document.getElementById(prv_cost_id).innerHTML).toString() != 'NaN' ? document.getElementById(prv_cost_id).innerHTML : '';
            document.getElementById('lblitemshortdescpt').innerHTML = item_name;

            SetBackgroundColor_selecteddRow("orange");

            document.getElementById('dvChangeMinmaxQty_M').style.display = "block";
            SetPosition_Relative(evt, 'dvChangeMinmaxQty_M');

            document.getElementById('txtNewMaxQty').focus();

            //$("#ctl00_MainContent_gvItemsSplit").scrollableTable({ type: "th" }); 
        }


        function Onfail(retval) {
            alert(retval._message);


        }


        ///////////////////////////////

        var lastExecutorMinMaxQty_M = null;

        function SetBackgroundColor_selecteddRow(color) {

            if (prv_qty_id) {
                document.getElementById(prv_qty_id).style.background = color;
                document.getElementById(prv_cost_id).style.background = color;

            }

        }



        function async_Upd_Provisions_Approval_Limit() {

            if (lastExecutorMinMaxQty_M != null)
                lastExecutorMinMaxQty_M.abort();

            var newtxtNewMaxQty = document.getElementById('txtNewMaxQty').value;
            var newtxtNewMaxCost = document.getElementById('txtNewMaxCost').value;

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'async_Upd_Provisions_Approval_Limit', false, { "Item_Ref_Code": prv_item_ref_code, "Max_Qty": newtxtNewMaxQty, "Max_Cost": newtxtNewMaxCost, "UserID": prv_userid, "Vessel_ID": prv_vessel_id }, onSuccasync_Upd_Provisions_Approval_Limit, Onfail, new Array(newtxtNewMaxQty, newtxtNewMaxCost));

            lastExecutorMinMaxQty_M = service.get_executor();

        }

        function onSuccasync_Upd_Provisions_Approval_Limit(retVal, Args) {



            if (retVal == "1") {
                document.getElementById('dvChangeMinmaxQty_M').style.display = "none";
                SetBackgroundColor_selecteddRow("transparent");

                document.getElementById(prv_qty_id).innerHTML = Args[0];
                document.getElementById(prv_cost_id).innerHTML = Args[1];

            }

            else {
                alert('Error occured during update !');
            }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="page-title">
        Set approval limit for Provision
    </div>
    <div class="page-content">
        <asp:UpdatePanel ID="updFilter" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
            <ContentTemplate>
                <asp:Label ID="lblmsg" runat="server" Font-Italic="true" Font-Size="10" Font-Names="verdana"
                    BackColor="Yellow" ForeColor="Red"></asp:Label>
                <table width="100%">
                    <tr>
                        <td style="width: 10%; vertical-align: top">
                            <asp:UpdatePanel ID="updfleet" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table cellspacing="0" width="100%" cellpadding="5" style="border: 1px solid  #cccccc;
                                        border-collapse: collapse;">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; text-align: left; width: 120px">
                                                <asp:CheckBox ID="chkVesselListSelectAll" Font-Size="11px" ForeColor="Black" runat="server"
                                                    Text="Select All" OnCheckedChanged="chkVesselListSelectAll_CheckedChanged" AutoPostBack="true" />
                                                <br />
                                                <asp:CheckBoxList ID="chkVesselList" AutoPostBack="false" RepeatDirection="Vertical"
                                                    runat="server" DataTextField="Vessel_Short_Name" DataValueField="Vessel_id" Font-Size="11px"
                                                    ForeColor="Black">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 90%; vertical-align: top">
                            <table cellspacing="0" width="100%" cellpadding="5" style="border: 1px solid  #cccccc;
                                border-collapse: collapse;">
                                <tr>
                                    <td class="tdhFilter" style="width: 100px">
                                        Sub Catalogue :
                                    </td>
                                    <td class="tddFilter">
                                        <asp:DropDownList ID="ddlSubCatalogue" runat="server" Width="160px" Font-Size="12px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tdhFilter">
                                        Item :
                                    </td>
                                    <td class="tddFilter">
                                        <asp:TextBox ID="txtSearchItems" Width="150px" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="padding: 0px 10px 0px 10px; text-align: center">
                                        <asp:CheckBox ID="chkMaxQty" runat="server" Text="Qty > 0" />
                                    </td>
                                    <td align="left" valign="bottom" class="tddFilter">
                                        <asp:Button ID="btnSearch" runat="server" ToolTip="Search" Width="100px" Text="Search" OnClick="btnSplitItems_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnViewAssignment" runat="server" Text="Copy To Vessels" OnClick="btnViewAssignment_Click" />
                                        &nbsp;
                                        <asp:ImageButton ID="btnExport" ToolTip="Export To Excel" ImageUrl="~/Images/XLS.jpg" Height="25px" OnClick="btnExport_Click"
                                            ImageAlign="AbsBottom" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; border-top: 1px solid #cccccc" colspan="6">
                                        <div style="max-width: 1000px; overflow-x: scroll; min-height: 400px">
                                            <asp:GridView ID="gvItemsSplit" GridLines="None" AutoGenerateColumns="true" CellPadding="5"
                                                ShowHeaderWhenEmpty="true" EmptyDataText=" no record found !" EmptyDataRowStyle-BackColor="LightYellow"
                                                runat="server" OnDataBound="gvItemsSplit_DataBound" OnRowCreated="gvItemsSplit_RowCreated">
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" Font-Size="11px" Font-Names="verdana"
                                                    BackColor="White" HorizontalAlign="Left" />
                                                <RowStyle CssClass="RowStyle-css" Font-Size="11px" Font-Names="verdana" HorizontalAlign="Left"
                                                    BorderStyle="Solid" BorderColor="#cccccc" BorderWidth="1px" />
                                                <HeaderStyle CssClass="HeaderStyle-css-teal" BorderStyle="Solid" BorderColor="#cccccc"
                                                    BorderWidth="1px" />
                                            </asp:GridView>
                                        </div>
                                        <ucpager:ucCustomPager ID="CustomPager" OnBindDataItem="BindSplittedItems" AlwaysGetRecordsCount="true"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="dvCopyfromVessels" title='Copy from vessel' style="display: none; width: 300px;
                    max-height: 400px">
                    <table style="width: 100%; border: 1px solid;">
                        <tr>
                            <td style="background-color: #C6E4EE; font-weight: bold; text-align: center; padding-left: 20px;
                                color: black">
                                Select Vessel
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;padding-left:20px">
                                <div style="max-height: 200px; overflow-y: scroll">
                                    <asp:UpdatePanel ID="updselectall" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" OnCheckedChanged="chkSelectAll_CheckedChanged"
                                                AutoPostBack="true" />
                                            <br />
                                            <asp:CheckBoxList ID="chkNotAssignedVessels" runat="server" Width="120px" RepeatDirection="Vertical">
                                            </asp:CheckBoxList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="background-color: #C6E4EE; text-align: center; padding: 5px">
                                <asp:Button ID="btnAssign" runat="server" Text="Save" OnClick="btnAssign_Click" />
                                <br />
                                <asp:Label ID="lblErrorMessage" runat="server" Font-Italic="true" Font-Size="10"
                                    Font-Names="verdana" BackColor="Yellow" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvChangeMinmaxQty_M" style="display: none; border: 4px solid orange; background-color: #FFFFFF;
            position: absolute; z-index: 100; border-radius: 3px; width: 185px">
            <table cellpadding="2" cellspacing="0" style="margin: 3px; width: 180px; border-collapse: collapse">
                <tr>
                    <td colspan="2" style="text-align: left; font-size: 10px; font-family: Tahoma; padding: 2px;
                        background-color: #F5F5F5; color: #191970">
                        <span id="lblitemshortdescpt"></span>
                    </td>
                </tr>
                <tr>
                    <td class="tdhFilter">
                        Max Qty :
                    </td>
                    <td class="tddFilter">
                        <input type="text" id="txtNewMaxQty" style="width: 85px; height: 16px; background-color: #FFFFFF;
                            border: 1px solid #D3D3D3" maxlength="10" />
                    </td>
                </tr>
                <tr>
                    <td class="tdhFilter">
                        Max unit cost :
                    </td>
                    <td class="tddFilter">
                        <input type="text" id="txtNewMaxCost" style="width: 85px; height: 16px; background-color: #FFFFFF;
                            border: 1px solid #D3D3D3" maxlength="10" />
                    </td>
                </tr>
                <tr>
                    <td valign="bottom" colspan="2" style="text-align: right; padding-right: 12px; padding-top: 3px">
                        <img src="../Images/save.gif" onclick="async_Upd_Provisions_Approval_Limit()" title="Save"
                            alt="save" />
                        &nbsp;
                        <img src="../Images/close.gif" title="Cancel" onclick=" SetBackgroundColor_selecteddRow('transparent');document.getElementById('dvChangeMinmaxQty_M').style.display = 'none';"
                            alt="close" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
