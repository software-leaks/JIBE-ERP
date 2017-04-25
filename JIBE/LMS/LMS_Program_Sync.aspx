<%@ Page Title="'Training/Drill To Sync" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LMS_Program_Sync.aspx.cs" Inherits="LMS_Program_Sync" %>

<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="auc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            width: 120px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 0px 2px 0px 3px;
            height: 20px;
            vertical-align: middle;
        }
        
        .tdrow
        {
            font-size: 11px;
            text-align: left;
            padding: 2px;
            vertical-align: top;
            line-height: 16px;
        }
        .btnCSS
        {
            height: 26px;
        }
    </style>
    <script type="text/javascript">

        function fn_OnClose() {

        }
        function showDiv(dv) {
            if (dv) {
                $('#' + dv).show();
            }

        }
        function closeDiv(dv) {
            if (dv) {
                $('#' + dv).hide();
            }

        }

        function Validation_Delete(program_status) {

            if (program_status.toString().length > 0) {
                alert('Program already has scheduled, so you can not delete program');
                return false;
            }

            var con = confirm('Are you sure want to delete this record?');
            if (con) {
                return true;
            }
            else
                return false;
        }


        function Check_NoOfCHAPTER_Exist_In_Program(Total_Chapter) {

            if (Total_Chapter == 0) {
                alert('Program does not have any chapter, so you can not schedule program.');
                return false;
            }
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
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original

                        inputList[i].checked = false;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Training/Drill List To be Synced
    </div>
    <div class="page-content-main">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 40%; top: 30px; z-index: 2;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="updMain" runat="server">
            <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnDownloadSelected" EventName="Click" />
            </Triggers>--%>
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td class="tdh" style="text-align: right">
                            Sync Date:
                        </td>
                        <td class="tdd" style="text-align: left">
                            <asp:DropDownList   DataTextField="SYNC_DATE" DataValueField="SYNC_DATE" 
                                ID="ddlSyncDate" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddlSyncDate_SelectedIndexChanged"></asp:DropDownList>
                          
                            &nbsp;&nbsp; <span style="text-align: left">Show History:
                                <asp:CheckBox ID="chkHistory" runat="server" OnCheckedChanged="chkHistory_CheckedChanged"
                                    AutoPostBack="true" />
                            </span>&nbsp;
                            <%--    <asp:Button ID="btnClearFilter" runat="server" OnClick="btnClearFilter_Click" Text="Clear Filter"
                                Width="80px" CssClass="btnCSS" />--%>
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnDownloadSelected" runat="server" OnClick="btnDownloadSelected_Click"
                                Text="Download Selected" Width="150px" CssClass="btnCSS" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="vertical-align: top;">
                            <asp:GridView ID="gvProgram_ListDetails" AutoGenerateColumns="false" runat="server"
                                DataKeyNames="Vessel_ID,ProgramsIDS" Width="100%" CssClass="gridmain-css" CellPadding="4"
                                CellSpacing="0" GridLines="None" OnRowDataBound="gvProgram_ListDetails_RowDataBound">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="checkRow" runat="server" onclick="Check_Click(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Vessel_Name" ItemStyle-HorizontalAlign="left" ItemStyle-Width="800px"
                                        HeaderText="Vessel Name" />
                                    <asp:BoundField DataField="Programs" ItemStyle-HorizontalAlign="left" ItemStyle-Width="800px"
                                        HeaderText="Training/Drill" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtProgramIDS" runat="server" Style="display: none" Text='<%#Eval("ProgramsIDS") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <auc:CustomPager ID="ucCustomPagerProgram_List" OnBindDataItem="BindPrograms" AlwaysGetRecordsCount="true"
                                runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="divMessage" align="center">
            <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
        </div>
        <br />
    </div>
    <div id="dvTrainingScheduling" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 30%; top: 10%; width: 600px; z-index: 1; color: black">
        <div class="header">
            <div style="right: 0px; position: absolute; cursor: pointer;">
                <img src="../Images/Close.gif" onclick="closeDiv('dvTrainingScheduling');" alt="Close" />
            </div>
            <h4>
                Training Scheduling</h4>
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                <ContentTemplate>
                    <table style="width: 100%; background-color: White; cursor: default;">
                        <tr>
                            <td colspan="2" style="font-size: 11px; text-align: left; font-weight: bold">
                                Training/Drill Name :
                                <asp:Label ID="lblProgramName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; text-align: left; font-weight: bold">
                                Vessel List
                            </td>
                            <td style="font-size: 11px; text-align: left; font-weight: bold; vertical-align: top;">
                                Frequency
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
