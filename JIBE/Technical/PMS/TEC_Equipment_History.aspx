<%@ Page Title="Equipment statistics" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="TEC_Equipment_History.aspx.cs" Inherits="Technical_PMS_TEC_Equipment_History_" %>

<%@ Register Src="~/UserControl/ucAsyncPager.ascx" TagName="ucAsyncPager" TagPrefix="auc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Scripts/JsTree/themes/default/style.min.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JsTree/libs/jquery.js" type="text/javascript"></script>
    <script src="../../Scripts/JsTree/jstree.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min1.11.0.js" type="text/javascript"></script>
    <link href="../../Styles/jquery-ui1.11.0.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/TEC_Equipment_History.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/jsasyncpager.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .page
        {
            min-width: 600px;
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="page-title" style="margin-bottom: 3px;">
        Equipment statistics
    </div>
    <div style="height: 100%;">
        <div style="float: left; border: 1px solid gray; border-radius: 5px; width: 20%;
            min-width: 250px; max-height: 800px; overflow: hidden;">
            <div style="padding: 3px; background-color: #DCDCDC">
                <asp:RadioButtonList ID="ddlEquipmentType" runat="server" RepeatDirection="Horizontal"
                    CellPadding="4" CellSpacing="0" onchange="ddlEquipmentType_selectinChanged(this)"
                    Font-Size="11px" AutoPostBack="false" ForeColor="Navy">
                    <asp:ListItem Text="All Equipment" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Active Equipment" Selected="True" Value="AC"></asp:ListItem>
                    <asp:ListItem Text="Spare Equipment" Value="SP"></asp:ListItem>
                </asp:RadioButtonList>
                <div style="padding: 5px 0px 8px 5px; color: Navy;">
                    <asp:CheckBox ID="cbxSafetyAlarm" runat="server" Text="Safety Alarm" ClientIDMode="Static"
                        onChange="cbxSafetyAlarm_selectionChange();" />&nbsp;&nbsp;<asp:CheckBox ID="cbxCalibration"
                            runat="server" Text="Calibration" ClientIDMode="Static" onChange="cbxCalibration_selectionChange();" />&nbsp;&nbsp;<asp:CheckBox
                                ID="cbxCritical" runat="server" Text="Critical" ClientIDMode="Static" onChange="cbxCritical_selectionChange();" />
                </div>
                <asp:DropDownList ID="DDlVessel_List" runat="server" Font-Size="11px" AutoPostBack="false"
                    Width="200px" DataTextField="Vessel_Name" DataValueField="Vessel_ID" onChange="DDlVessel_selectionChange()">
                </asp:DropDownList>
            </div>
            <div id="FunctionalTree" style="min-width: 250px; height: 710px; max-height: 710px;
                overflow: scroll; padding-top: 3px">
            </div>
        </div>
        <div style="float: right; width: 79.6%;">
            <div id="dvMachineryDetails" style="height: 100px; overflow-y: scroll; border: 1px solid gray;
                border-radius: 5px; padding: 5px">
            </div>
            <div id="tabs" style="height: 680px; border: 1px solid gray">
                <ul>
                    <li><a href="#plannedjob"><span>Planned jobs </span></a></li>
                    <li><a href="#unplannedjob"><span>UnPlanned jobs</span></a></li>
                    <li><a href="#runhour"><span>Run Hour</span></a></li>
                    <li><a href="#requisition"><span>Requisition</span></a></li>
                   <%-- <li><a href="#oilcons"><span>Oil Consumption</span></a></li>--%>
                    <li><a href="#sparecons"><span>Spare Consumption</span></a></li>
                    <li><a href="#storecons"><span>Store Consumption</span></a></li>
                    <li><a href="#eqpreplacement"><span>Equipment Replacement</span></a></li>
                    <li><a href="#joblibrary"><span>Job Library</span></a></li>
                    <li><a href="#spareconstab"><span>Spare Items</span></a></li>
                </ul>
                <div id="plannedjob" style="padding: 2px; display: block;">
                </div>
                <div id="unplannedjob" style="padding: 2px; display: block;">
                </div>
                <div id="runhour" style="padding: 2px; display: block">
                </div>
                <div id="requisition" style="padding: 2px; display: block">
                </div>
                <div id="oilcons" style="padding: 2px; display: block">
                </div>
                <div id="sparecons" style="padding: 2px; display: block">
                </div>
                <div id="storecons" style="padding: 2px; display: block">
                </div>
                <div id="eqpreplacement" style="padding: 2px; display: block">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: left; margin: 10px; color: Black">
                                <asp:HyperLink ID="hlnkReplaceEquipment" Text="Replace this equipment" NavigateUrl="#"
                                    onclick="ShowEquipmentReplacement()" runat="server" BorderColor="#70DBFF" BorderWidth="3px"
                                    ForeColor="Black" BackColor="#70DBFF" Visible="false"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="dataEqpreplacement">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="joblibrary" style="padding: 2px; display: block; height: 560px; overflow-y: scroll;">
                    loading....
                </div>
                <div id="spareconstab" style="display: none;">
                    <div id="sparecontabhead" style="display: none;">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="rdItemStatus" runat="server" RepeatDirection="Horizontal"
                                    onclick="return onFilterSpare(this)" AutoPostBack="True">
                                    <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Deleted" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <br />
                    <div id="spareitems" style="padding: 2px; display: none; height: 530px; overflow-y: scroll;">
                        loading....
                    </div>
                    <br />
                    <div id="sparepager" style="display: none;">
                        <auc:ucAsyncPager ID="ucAsyncPager2" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="dvEQPReplacement" style="display: none" title="Equipment Replacement">
        <iframe id="iframeEQPReplacement" width="1000" height="400" src=""></iframe>
    </div>
    <script type="text/javascript">
        $(document).ready(initTab);      
       
    </script>
</asp:Content>
