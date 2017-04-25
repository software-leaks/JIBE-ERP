<%@ Page Title="Copy Run Hour" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CopyRunHour.aspx.cs" Inherits="Technical_PMS_CopyRunHour" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <link href="../../Scripts/JsTree/themes/default/style.min.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JsTree/libs/jquery.js" type="text/javascript"></script>
    <script src="../../Scripts/JsTree/jstree.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min1.11.0.js" type="text/javascript"></script>
    <link href="../../Styles/jquery-ui1.11.0.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/CopyRunHourSettingsToEquipment.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .page
        {
            min-width: 400px;
            width: 90%;
        }
        .cleartd
        {
            width: 10px;
        }
    </style>
    <%--<script type="text/javascript">
        var isloaded = false;
        var id = 0;
        var vessel_id = 0;
        function ddlVessel_selectionChange() {
            document.getElementById('hdfSourceID').value = "0";
            bindTreeSource();
            var elem = document.getElementById("dvSource");
            if (vessel_id > 0)
                elem.style.display = "block";
            else
                elem.style.display = "none";

            HideDestination();
        }
        function SuccessMessage() {
            alert("Equipment run hour settings saved successfully.");
        }
        function HideDestination() {
            var elem = document.getElementById("dvDestination");
            elem.style.display = "none";
        }

        function DisplayPage() {
            var elem = document.getElementById("dvSource");
            if (vessel_id > 0)
                elem.style.display = "block";
            else
                elem.style.display = "none";

            var elem2 = document.getElementById("dvDestination");
            var sourceid = document.getElementById('hdfSourceID').value;

            if (vessel_id > 0)
                elem2.style.display = "block";
            else
                elem2.style.display = "none";
        }
        function DisplayOnlySource() {
            var elem = document.getElementById("dvSource");
            elem.style.display = "block";

            var elem2 = document.getElementById("dvDestination");
            var sourceid = document.getElementById('hdfSourceID').value;
            elem2.style.display = "none";

        }
        function DestroyTree() {
            if (isloaded == true) {
                $("#FunctionalTreeSource").jstree("destroy");
                isloaded = true;
            }
        }

        function bindTreeSource() {

            id = 0;

            vessel_id = $('[id$=ddlVessel]').val();
            if (isloaded == true) {
                $("#FunctionalTreeSource").jstree("destroy");
                isloaded = true;
            }
            $("#FunctionalTreeSource").jstree({
                'core': {

                    'data': {
                        'type': "POST",
                        "async": "true",
                        'contentType': "application/json; charset=utf-8",
                        'url': "../../JIBEWebService.asmx/PMS_Get_SourceSystemSubsystemFunction_Tree",
                        'data': "{}",
                        'dataType': 'JSON',
                        'data': function () {
                            return '{"id":' + id + ',"vesselid":' + vessel_id + '}'
                        },
                        'success': function (retvel) {
                            isloaded = true;
                            return retvel.d;
                        }

                    }
                }
            });
            var tree = $("#FunctionalTreeSource");
            tree.bind("loaded.jstree", function (event, data) {
                if (document.getElementById('hdfSourceID').value != "")
                    id = document.getElementById('hdfSourceID').value;
                else
                    id = 0;

                if (document.getElementById('hdfSourceID').value > 0) {

                    tree.jstree('select_node', document.getElementById('hdfSourceID').value);
                }

            });
            $("#FunctionalTreeSource").bind('select_node.jstree', function (e) {
                id = $("#FunctionalTreeSource").jstree('get_selected').toString();

                if ($('#FunctionalTreeSource').jstree(true).get_parent(id) == '#') {
                    document.getElementById('hdf2').value = "System";
                }
                else {
                    document.getElementById('hdf2').value = "SubSystem";
                }
                if (id > 0 && vessel_id > 0) {
                    document.getElementById('hdfSourceID').value = id;

                }
            });
            isloaded = true;
        }


        function bindBasedOnQueryStringTreeSource() {

            if (document.getElementById('hdfSourceID').value != "")
                id = document.getElementById('hdfSourceID').value;
            else
                id = 0;
            vessel_id = $('[id$=ddlVessel]').val();
            if (isloaded == true) {
                $("#FunctionalTreeSource").jstree("destroy");
                isloaded = true;
            }
            $("#FunctionalTreeSource").jstree({
                'core': {

                    'data': {
                        'type': "POST",
                        "async": "true",
                        'contentType': "application/json; charset=utf-8",
                        'url': "../../JIBEWebService.asmx/PMS_Get_SourceSystemSubsystemFunction_Tree",
                        'data': "{}",
                        'dataType': 'JSON',
                        'data': function () {
                            return '{"id":' + id + ',"vesselid":' + vessel_id + '}'
                        },
                        'success': function (retvel) {
                            isloaded = true;
                            return retvel.d;
                        }

                    }
                }
            });
            var tree = $("#FunctionalTreeSource");
            tree.bind("loaded.jstree", function (event, data) {

                if (document.getElementById('hdfSourceID').value > 0) {

                    tree.jstree('select_node', document.getElementById('hdfSourceID').value);
                }


                document.getElementById('hdfSourceID').value = parents;

            });
            $("#FunctionalTreeSource").bind('select_node.jstree', function (e) {
                id = $("#FunctionalTreeSource").jstree('get_selected').toString();


                if ($("#FunctionalTreeSource").is_leaf(e) == true)
                    document.getElementById('hdf2').value = id;

                if (id > 0 && vessel_id > 0) {
                    document.getElementById('hdfSourceID').value = id;


                }
            });
            isloaded = true;
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="background-color: transparent; position: absolute; left: 49%;
                top: 20px; z-index: 2; color: black">
                <img src="../../images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="page-title">
        Copy Run Hour
    </div>
    <div id="dvPageContent" style="margin-top: -2px; border: 1px solid #cccccc; vertical-align: bottom;
        padding: 4px; color: Black; text-align: left; background-color: #fff; height: 715px;">
        <div id="dvDefaultFilter">
            <table border="0" cellpadding="1" cellspacing="1" style="width: 99.5%;">
                <tr>
                    <td valign="top" style="border: 1px solid #aabbdd;">
                        <table border="0" cellpadding="4" cellspacing="1">
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblFleet" Text="Fleet:" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 200px">
                                    <asp:DropDownList ID="ddlFleet" runat="server" Width="150px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblVesselSearch" Text="Vessel:" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 200px">
                                    <asp:DropDownList ID="ddlVessel" runat="server" Width="150px" AutoPostBack="false"
                                        onChange="ddlVessel_selectionChange()" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <center>
                                        <asp:Label ID="lblSystemSubsystem" runat="server" Text="System/Subsystem:" Font-Bold="true"></asp:Label>&nbsp;&nbsp;<asp:Label
                                            ID="lblSourceName" runat="server"></asp:Label>
                                    </center>
                                </td>
                                <td style="width: 20px">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:HiddenField ID="hdfSourceID" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdf2" runat="server" ClientIDMode="Static" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hdfQueryStringEquipmentID" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="hdfQueryStringEquipmentType" runat="server" Value="0" ClientIDMode="Static" />
        <div id="innerData">
            <div style="float: left; width: 49%; display: none;" id="dvSource">
                <center>
                    <table style="width: 99%; border: 1px solid black;">
                        <tr>
                            <td style="background-color: #BEBEBE; height: 30px;">
                                <center>
                                    <span style="font-weight: bold; font-size: 14px; color: Black;">Source</span></center>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="FunctionalTreeSource" style="height: 590px; width: 97%; overflow: auto;
                                    padding: 10px 10px 10px 10px;">
                                </div>
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="float: left; width: 51%; display: none;" id="dvDestination">
                        <center>
                            <table style="width: 99%; border: 1px solid black;">
                                <tr>
                                    <td style="background-color: #BEBEBE; height: 30px;">
                                        <table style="width: 99%;">
                                            <tr>
                                                <td width="60%">
                                                    <center>
                                                        <span style="font-weight: bold; font-size: 14px; color: Black;">Destination</span></center>
                                                </td>
                                                <td width="39%">
                                                    <div style="text-align: center;">
                                                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:Button
                                                            ID="btnFreeTextSearch" runat="server" Text="Search" OnClick="btnFreeTextSearch_Click" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="height: 608px; overflow: auto;">
                                            <asp:GridView ID="gvDestination" runat="server" AutoGenerateColumns="false" DataKeyNames="ID,SystemCode,SourceLinked,LinkedID"
                                                Width="99%" CssClass="gridmain-css" CellPadding="4" CellSpacing="0" GridLines="None"
                                                OnRowDataBound="gvDestination_RowDataBound">
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="checkRow" runat="server" Checked='<%#  Eval("IsLinked") %>'/>
                                                            <asp:Label ID="lblSystemID" runat="server" Text='<%#  Eval("ID") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSystemID" runat="server" Text='<%#  Eval("ID") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSystemDescription" runat="server" Text='<%#  Eval("SystemDescription") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Level">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLevel" runat="server" Text='<%#  Eval("Title") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <div style="padding-left: 765px;">
                            <table>
                                <tr>
                                    <td>
                                        <div style="display: none;">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                ClientIDMode="Static" /></div>
                                        <asp:HiddenField ID="hdfSearchClick" runat="server" ClientIDMode="Static" Value="0" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
