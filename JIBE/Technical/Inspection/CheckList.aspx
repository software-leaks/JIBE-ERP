<%@ Page Title="Inspection Checklist" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CheckList.aspx.cs" Inherits="Technical_Inspection_CheckList"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/uc_INSP_Add_Questions.ascx" TagName="ucCustomPager"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--<script src="../../Scripts/PMS_Manage_System.js" type="text/javascript"></script>--%>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/CheckList.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <%--  <link href="../../Scripts/JsTree/themes/default/style.min.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JsTree/libs/jquery.js" type="text/javascript"></script>
    <script src="../../Scripts/JsTree/jstree.min.js" type="text/javascript"></script>--%>
    <%--<script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>--%>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {

            var hdnfeild = $('[id$=hdnQuerystring]');
            var dv = $('[id$=dvAddNew]')
            var val = document.getElementById(hdnfeild.attr('id')).value;
            if (val != "") {
                GetCheckList(val);
                //document.getElementById('dvAddNew').style.display = 'NONE';
                document.getElementById(dv.attr('id')).style.display = 'NONE';
                //var hdnfeild = $('[id$=hdnQuerystring]')
                // var divaddnew = document.getElementById(hdnfeild.attr('id')).value;
            }
            else {
                AddNewClick();
            }


        });

        function getScheduleList() {

            __doPostBack("<%=btnGetSchedule.UniqueID %>", "onclick");

        }

        // This function will not allow user to enter special character except ? and .
        function blockSpecialChar(e) {
            var k = e.keyCode == 0 ? e.charCode : e.keyCode;
            return (k == 63 || (k > 64 && k < 91) || k == 46 || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57) || k == 32 || k == 44 || k == 45 || k == 13);
            return k;
        }


        


    </script>
    <style type="text/css">
        .insp-Chk-Checklist
        {
            color: Black;
            font-size: 14px;
            float: left;
            min-width: 200px;
            border: 0px solid gray;
            background-color: #efefef;
            padding: 5px;
            font-weight: bold;
        }
        .insp-Chk-Group
        {
            background-color: Yellow;
            color: Black;
            font-size: 14px;
            border: 1px solid gray;
            background-color: #efefef;
            padding: 5px;
            font-weight: bold;
            cursor: pointer;
        }
        .insp-IndexNo
        {
        }
        /*padding:3px;*/
        /*.insp-Chk-Group .insp-Chk-Group{margin-left:20px;}*/
        .insp-Chk-Location
        {
            border: 1px solid gray;
            background-color: Gray;
            color: Black;
            font-size: 12px;
            padding: 5px;
            font-weight: bold;
            cursor: pointer;
        }
        /*padding:3px; margin-left:20px;*/
        .insp-Chk-Location-Text
        {
            border: 2px solid black;
            color: Black;
            font-size: 12px;
            padding: 5px;
            font-weight: bold;
            cursor: pointer;
        }
        /*padding:3px; margin-left:20px;*/
        
        .insp-Chk-Item-Container
        {
            color: Black;
            font-size: 10px;
            cursor: pointer;
        }
        /*padding:3px;margin-left:40px;*/
        .insp-Item-options
        {
            /*margin-left:20px;*/
        }
        .insp-Item-Pos1
        {
            margin-left: 10px;
        }
        .insp-Item-Pos2
        {
            margin-left: 50px;
        }
        .insp-Item-Pos3
        {
            margin-left: 100px;
        }
        .insp-Item-Pos4
        {
            margin-left: 150px;
        }
        .insp-Item-Pos5
        {
            margin-left: 200px;
        }
        .insp-Item-Pos6
        {
            margin-left: 250px;
        }
        .insp-Item-Pos7
        {
            margin-left: 300px;
            float: left;
        }
        
        
        .insp-img-UP
        {
            display: block;
            float: right;
            width: 16px;
            height: 16px;
            background: url('../../Images/Actions-go-up-icon.png') 0 -16px;
        }
        .insp-img-DN
        {
            display: block;
            float: right;
            width: 16px;
            height: 16px;
            background: url('../../Images/Actions-go-down-icon.png') 0 -16px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <div class="page-title">
            Inspection Checklist
        </div>
        <div id="dvEditPublish" style="margin-left: 430px; padding-bottom: 10px; padding-top: 10px;
            display: none;">
            <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                <ContentTemplate>
                    <asp:Button ID="btnEdit" Width="100px" runat="server" Text="Edit" OnClientClick="return EditClick();" />
                    <asp:Button ID="btnPublish" Width="100px" runat="server" Text="Publish" OnClientClick="return UpdateStatus();" />
                    <asp:Button ID="btnUpdate" Width="100px" runat="server" Text="Update" OnClientClick="return UpdateDivClick();" />
                    <asp:Button ID="btnGetSchedule" Width="100px" runat="server" Text="Update" Visible="false"
                        OnClick="btnGetSchedule_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <div id="dvUP3">
                    <div id="dvShowUpdates" style="margin-top: 0px; padding-left: 30%; display: none;">
                        <asp:GridView ID="grvChecklist" runat="server" HeaderStyle-Font-Size="14px" HeaderStyle-BackColor="#838389"
                            HeaderStyle-ForeColor="black" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"
                            EmptyDataText="No records Found !!">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRow" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Schedule ID" ItemStyle-Width="150" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSCHID" runat="server" Text='<%# Eval("ScheduleID") %>' Font-Size="14px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Checklist ID" ItemStyle-Width="150" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCHKID" runat="server" Text='<%# Eval("Checklist_ID") %>' Font-Size="14px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Schedule_Desc" HeaderText="Schedule Description" ItemStyle-Width="150"
                                    Visible="true">
                                    <ItemStyle Width="150px" Font-Size="14px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CheckList_Name" HeaderText="CheckList Name" ItemStyle-Width="150"
                                    Visible="true">
                                    <ItemStyle Width="150px" Font-Size="14px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Start_Date" HeaderText="Start Date" ItemStyle-Width="150"
                                    Visible="true">
                                    <ItemStyle Width="150px" Font-Size="14px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle BackColor="#DEE1E2" ForeColor="Black" />
                        </asp:GridView>
                        <br />
                        <div id="dvUpdateButton" style="margin-left: 20%; padding-top: 5px; padding-bottom: 5px;
                            display: none">
                            <asp:Button ID="btnUpdateShedules" Width="100px" runat="server" Text="Save" OnClick="btnUpdateShedules_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            <span id="SPremarks" style="font-size: 14px; color: Red">&nbsp;</span>
        </div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="float: left; border: 1px solid gray; border-radius: 5px; width: 60%;
            min-width: 250px; max-height: 800px; overflow: hidden;">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <div id="FunctionalTree" style="min-width: 250px; height: 700px; max-height: 760px;
                        overflow: scroll; padding-top: 3px">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <div style="float: right; width: 38.6%;">
                    <div id="dvAddNew" runat="server" style="display: block">
                        <asp:Button ID="btnNew" Width="80px" runat="server" Text="Add New" OnClientClick="return AddNewClick();" />
                    </div>
                    <div id="dvCheckList" style="display: none">
                        <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                            <legend>Checklist Details</legend>
                            <table>
                                <tr>
                                    <td>
                                        Checklist name :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChecklistName" Width="150px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Vessel type :
                                    </td>
                                    <%-- <td style="padding-left: 6px;">--%>
                                    <td>
                                        <asp:DropDownList ID="ddlvesselType" Width="150px" runat="server">
                                        </asp:DropDownList>
                                        <%--<asp:TextBox ID="txtVesselType" runat="server"></asp:TextBox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Check list type :
                                    </td>
                                    <%--<td style="padding-left: 6px;">--%>
                                    <td>
                                        <asp:DropDownList ID="ddlchecklistType" Width="150px" runat="server">
                                        </asp:DropDownList>
                                        <%-- <asp:TextBox ID="txtCheclistType" runat="server"></asp:TextBox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Location rating :
                                    </td>
                                    <%-- <td style="padding-left: 6px;">--%>
                                    <td>
                                        <asp:DropDownList ID="ddlGrades" Width="150px" runat="server">
                                        </asp:DropDownList>
                                        <%-- <asp:TextBox ID="txtlocationGrading" runat="server"></asp:TextBox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hdnfldChecklstID" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSave" Width="80px" runat="server" Text="Save" OnClientClick="return InsertCheckList();" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                    <div id="dvGroupUpdate" style="display: none">
                        <%--================================--%>
                        <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                            <legend>Edit Group:</legend>
                            <table>
                                <tr>
                                    <td style="width: 95px;">
                                        Group name :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEditGroupName" Width="150px" runat="server" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:HiddenField ID="hdnfldGroupID" runat="server" />
                                    </td>
                                    <td>
                                        <div id="dvDelGroup" style="width: 80px; float: left">
                                            <asp:Button ID="btnDeleteGroup" runat="server" Width="80px" Text="Delete" OnClientClick="return DeleteGroup();">
                                            </asp:Button>
                                        </div>
                                        <asp:Button ID="btnGroupUdate" runat="server" Width="80px" Text="Update" OnClientClick="return UpdateGroup();">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                    <div id="dvGroup" style="display: none">
                        <%--================================--%>
                        <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                            <legend>Add Group:</legend>
                            <table>
                                <tr>
                                    <td style="width: 95px;">
                                        Group name :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGroupName" Width="150px" runat="server" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--<asp:HiddenField ID="hdnfldGroupID" runat="server" />--%>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAddGroup" runat="server" Width="80px" Text="Add" OnClientClick="return InsertGroup();">
                                        </asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                    <div id="dvLocation" style="display: none">
                        <%--================================--%>
                        <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                            <legend>
                                <asp:Label ID="lblLocationCaption" runat="server" Text="Add Location:"></asp:Label>
                            </legend>
                            <table>
                                <tr>
                                    <td style="width: 95px;">
                                        Location name:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLocationName" Width="150px" runat="server" MaxLength="80"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Select location:
                                    </td>
                                    <td style="padding-left: 2px;">
                                        <div id="dvLocationDDL" style="width: 150px;">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:HiddenField ID="hdnfldLocationID" runat="server" />
                                    </td>
                                    <td>
                                        <div id="dvbtnDelLoc" style="width: 80px; float: left;">
                                            <asp:Button ID="btnDeleteLoc" runat="server" Width="80px" Text="Delete" OnClientClick="return DeleteLocation();" />
                                        </div>
                                        <asp:Button ID="btnAddLocation" runat="server" Width="80px" Text="Add" OnClientClick="return InsertLocation();" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                    <br />
                    <div id="dvItemsFilter" style="display: none">
                        <%--================================--%>
                        <table style="width: 98%">
                            <tr>
                                <td style="width: 90%">
                                    <asp:TextBox ID="txtfilter" runat="server" Width="98%"></asp:TextBox>
                                    <%-- <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />--%>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnFilter" runat="server" OnClientClick="return filter();" ToolTip="Search"
                                        ImageUrl="~/Images/SearchButton.png" />
                                </td>
                                <td>
                                    <%-- <asp:ImageButton ID="imgbtnAddnew" runat="server" OnClientClick="return showModal('dvAddQuestions'),false;"
                                        ToolTip="Add new Question" ImageUrl="~/Images/Add-icon.png" />--%>
                                    <asp:ImageButton ID="imgbtnAddnew" runat="server" OnClientClick="return AddNewQuestionModal(),false;"
                                        ToolTip="Add new Question" ImageUrl="~/Images/Add-icon.png" />
                                </td>
                            </tr>
                        </table>
                        <div id="dvCKItemsQB" style="width: 99%; min-width: 250px; max-height: 500px; overflow-y: scroll;">
                        </div>
                    </div>
                    <br />
                    <div id="dvSaveItems" style="display: none; margin-left: 105px;">
                        <asp:Button ID="btnSaveItems" runat="server" Width="80px" Text="Save" OnClientClick="return ItemSave();" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanelnew" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="dvAddQuestions" style="display: none;" title="Add New Question">
                    <%-- <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30"  />--%>
                    <div class="modal-popup-content">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <table border="0" style="width: 100%;" cellpadding="10">
                                    <tr>
                                        <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                            Question:
                                        </td>
                                        <td style="font-size: 11px; text-align: left; border-width: 1px; color: Red; font-weight: bold">
                                            *
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCriteria" Width="400px" Height="100px" runat="server" MaxLength="1000" onkeypress="return blockSpecialChar(event)"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                            Category:
                                        </td>
                                        <td style="font-size: 11px; text-align: left; border-width: 1px; color: Red; font-weight: bold">
                                            *
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCatName" Width="400px" runat="server" DataTextField="Category_Name"
                                                DataValueField="ID" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                            Grading type:
                                        </td>
                                        <td style="font-size: 11px; text-align: left; border-width: 1px; color: Red; font-weight: bold">
                                            *
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlGradingType" Width="400px" DataTextField="Grade_Name" DataValueField="ID"
                                                runat="server" onchange="return BindGradeOptions();">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top; border-width: 1px; font-weight: bold;">
                                            Gradings
                                        </td>
                                        <td style="font-size: 11px; text-align: left; border-width: 1px; color: Red; font-weight: bold">
                                            &nbsp;
                                        </td>
                                        <td style="vertical-align: top; border-width: 1px">
                                            <%--<asp:RadioButtonList ID="rdoGradings" runat="server" DataTextField="OptionText" DataValueField="OptionValue"
                                        Enabled="false">
                                    </asp:RadioButtonList>--%>
                                            <div id="rdoGradings">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="font-size: 11px; text-align: center; border-width: 1px">
                                            <%-- <asp:Button ID="btnSaveAndAdd" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnsave_Click" />&nbsp;&nbsp;--%>
                                            <asp:Button ID="btnSaveAndClose" CssClass="button-css" runat="server" Text="Save"
                                                OnClientClick="return SaveChecklistQuestion();" />&nbsp;&nbsp;
                                            <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClientClick="return closeAddQuestion();" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSaveAndClose" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hdnfldUserID" runat="server" />
        <asp:HiddenField ID="hdnfldVesselType" runat="server" />
        <asp:HiddenField ID="hdnQuerystring" runat="server" />
        <asp:HiddenField ID="hdnParentID" runat="server" />
        <asp:HiddenField ID="hdnStatus" runat="server" />
        <asp:HiddenField ID="hdnCHecklistVersion" runat="server" />
        <asp:HiddenField ID="hdnQuerystringPID" runat="server" />
        <asp:HiddenField ID="hdnVesselID" runat="server" />
    </div>
</asp:Content>
