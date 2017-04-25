<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="FMSSchedule.aspx.cs"
    Inherits="FMS_FMSSchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html>
<head id="Head1" runat="server">
    <title></title>
<link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
     <script src="../Scripts/Common_Functions.js" type="text/javascript"></script> 
    <link href="../Styles/jqueryFileTree.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jqueryFileTree.js" type="text/javascript"></script>
  <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .awesome
        {
            border: 1px solid #2980B9;
            display: inline-block;
            cursor: pointer;
            background: #3498DB;
            color: #FFF;
            font-size: 14px;
            padding: 6px 8px;
            text-decoration: none;
            text-shadow: 0px 1px 0px #2980B9;
            margin-right: 5px;
            margin-bottom: 5px;
            border-radius: 0px;
        }
    </style>
    <script type="text/javascript">

        function confirmSaveSchedule() {

            try {
                var rb = document.getElementById($('[id$=rdoFrequency]').attr('id'));
                var inputs = rb.getElementsByTagName('input');
                var flag = false;
                var selected;
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].checked) {
                        selected = inputs[i];
                        flag = true;
                        break;
                    }
                }
                if (selected.value == "NA") {
                    var res = confirm("Choosing None will remove for this form schedule from this vessel.");                 
                    if (document.getElementById($('[id$=txtRemark]').attr('id')).value.trim() == "") {
                        alert("Remark is mandatory for None type frequency.");
                        return false;
                    }

                }
                if (selected.value == "Monthwise") {
                    var chkEOM = document.getElementById($('[id$=chkEOM]').attr('id'));
                    var txtDD = document.getElementById($('[id$=txtDueDay]').attr('id'));
                    var dotPos = txtDD.value.indexOf(".");
                    if (chkEOM.checked == false) {
                       
                        if (txtDD.value == "") {

                            alert("Enter Due Date");
                            return false;
                        }
                        else {

                            if (isNaN(txtDD.value)) {

                                alert("Due Day accepts only numeric value");
                                return false;

                            }
                            else if (parseInt(txtDD.value) > 28) {

                                alert("Due Day should be less than or equal to 28");
                                return false;
                            }
                            else if (parseInt(txtDD.value) < 1) {

                                alert("Due Day should not be less than 1");
                                return false;
                            }
                            else if (parseInt(dotPos) > -1) {

                                alert("Decimal values not allowed for Due Day");
                                return false;

                            }

                            var startDate = document.getElementById($('[id$=txtStartDate]').attr('id')).value;
                            var endDate = document.getElementById($('[id$=txtEndDate]').attr('id')).value;

                            var res = startDate.split("/");




                            if (parseInt(txtDD.value) < parseInt(res[0]))
                            {
                                alert("Please select valid Due Day");
                                return false;
                            }
                        }

                    }
                }
            }
            catch (ex) {
                alert(ex);
                return false;
            }

            return res;
        }
        function confirmSaveVesselAssignment() {

            var tr = document.getElementById($('[id$=trvFile]').attr('id'));
            var trCls = document.getElementsByClassName("TreeChk");
            var chk = tr.childNodes[1].childNodes[1].childNodes[1].childNodes[0];
            var FileName = tr.childNodes[1].childNodes[1].childNodes[1].childNodes[1];
            if (confirm("The un-assigned forms will removed completely from the chosen vessels and the assigned forms will be synchronized to the chosen vessels.") == false) {
                document.getElementById($('[id$=hdfAlertFlag]').attr('id')).value = "0";
               
            }
            else {


                document.getElementById($('[id$=hdfAlertFlag]').attr('id')).value = "1";

            }
            return true;

        }
        function trvFileNodeCheckedChange(chk) {

            var chk1 = chk;


            var hdn = document.getElementById($('[id$=hdnDocID]').attr('id'));

            var docID = hdn.value.toString().split(",");

            if (chk.checked == true) {
                hdn.value = hdn.value + "," + chk.id;
            }
            else {

                var index = docID.indexOf(chk.id);               
                docID.splice(index, 1);
                hdn.value = docID.toString();
            }



        }
        $(document).ready(function () {

            LoadTree();

        });
        function LoadTree() {

            $('.fileTreeDemo').fileTree({
                root: 'DOCUMENTS',
                script: 'FMSSchTree.aspx',
                multiFolder: true

            },
                function (id, path, type) {
                    if (type == 1) {
                        //Folder node click
                      
                    }
                    else {
                        //File node click                        
                      
                    }
                }
            );
            }

            function RemarkMandetory() {

            }


    </script>
</head>
<body style="background-color: White;">
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
      <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left:50%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div>
    </div>
    <div align="center">
        <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
    </div>
    <div id="dvInspectionScheduling" runat="server" style="width:98%; height: 200px;"
        title="">
        <asp:UpdatePanel ID="UpdatePanel_Frequency" runat="server">
            <ContentTemplate>
                <br />
                <table style="width: 100%; margin: 0px 10px 0px 10px">
               
                    <tr>
                        <td style="width: 20%;">
                            <div style="overflow: auto; ">
                                <table width="100%" cellspacing="5">
                                    <tr>
                                        <td style="vertical-align: top; background-color: #f8f8f8; width: 100%; padding: 5;
                                            border: 1px solid #c3c3c3">
                                            <div style="background-color: #f8f8f8; text-align: left; height: 597px; width: 250px;
                                                overflow: auto; z-index: 1; border: 1px solid inset;">
                                                <div class="pull-right">
                                                </div>
                                                <table>
                                                    <tr>
                                                        <td style="vertical-align: middle; font-size: 12px; color:black;">
                                                            <div style=" vertical-align: middle;">
                                                                <img src="../images/doctree/network.gif" /> DOCUMENTS
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnRefresh" runat="server" Text="Refresh" ImageUrl="~/Images/refresh.png"
                                                                Height="20px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="margin-left:15px;">
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:TreeView ID="trvFile" runat="server" ImageSet="Arrows" NodeIndent="15" ForeColor="Black"
                                                                Width="100%" BorderColor="#cccccc" ShowCheckBoxes="All" 
                                                                EnableClientScript="true" >
                                                                <HoverNodeStyle Font-Underline="True" ForeColor="#000" />
                                                                <NodeStyle Font-Names="Tahoma" Font-Size="12px" ForeColor="Black" HorizontalPadding="2px"
                                                                    NodeSpacing="0px" VerticalPadding="2px" />
                                                                <ParentNodeStyle Font-Bold="False" />
                                                                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                                                    VerticalPadding="0px" CssClass="SelectedNodeStyle" />
                                                            </asp:TreeView>
                                                        
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <input id="hidden_SelectedNodeURL" type="hidden" runat="server" />
                                <input id="hdnForCheckINSelectedNode" type="hidden" runat="server" />
                            </div>
                        </td>
                        <td style="width: 40%; vertical-align: top;">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <span style="font-weight: bold; font-size: 12px; color:Black; ">Fleet:-</span> &nbsp;
                                        <br />
                                        <asp:DropDownList ID="ddlFleet" runat="server" UseInHeader="false" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                            AutoPostBack="true" Width="130" Style="margin-left: 0px" />
                                        <br />
                                        <br />
                                        <span style="font-weight: bold; font-size: 12px;color:Black; ">Vessel:-</span>
                                       
                                        <div style="float:right;  margin-bottom:4px;margin-right:-12px;">
                                        
                                                <asp:Button ID="BtnVesselAssign" runat="server" Text="Save Assignment" Style="margin-right: 10px"
                                                OnClientClick="return confirmSaveVesselAssignment();" OnClick="BtnVesselAssign_Click"
                                                CssClass="awesome" />
                                      
                                        </div>
                                        <br />
                                        <br />
                                        
                                        <div style="width: 100%; height: 510px; overflow-y: scroll; border: 1px solid black;">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvVesselSch" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                                        DataKeyNames="Vessel_ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                                        AllowSorting="true" CssClass="gridmain-css" 
                                                        onrowcommand="gvVesselSch_RowCommand" >
                                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                        <RowStyle CssClass="RowStyle-css"  Font-Size="12px"/>
                                                        <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                                        <PagerStyle CssClass="PMSPagerStyle-css" />
                                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Vessel">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnVesselID" runat="server" value='<%#Eval("Vessel_ID")%>'/>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Schedule Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSchType" runat="server" Text='<%#Eval("Schedule_Type")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Frequency" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFreq" runat="server" Text='<%#Eval("Frequency")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="130px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Assign">
                                                            <HeaderTemplate>
                                                            Assign
                                                            <br />
                                                                <asp:CheckBox ID="chkAssignAllVessel" runat="server" 
                                                                    Style="font-size: 12px; color: Black;" OnCheckedChanged="chkAssignAllVessel_CheckedChanged"
                                                                    AutoPostBack="true" />
                                                            </HeaderTemplate>
                                                                <ItemTemplate>
                                                                      <asp:CheckBox ID="chkVesselAssign" runat="server" Checked='<%#Convert.ToString(Eval("VesselAssign"))=="1"?true : false %>' ToolTip="Assign To Vessel" />
                                                                      <asp:HiddenField runat="server" ID="hdnAssignedCheck" Value='<%#Convert.ToString(Eval("VesselAssign")) %>' />
                                                                 </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                            </asp:TemplateField>

                                                               <asp:TemplateField HeaderText="Schedule">
                                                               <HeaderTemplate>
                                                               Schedule
                                                               <br />
                                                                   <asp:CheckBox ID="chkSchAllVessel" runat="server" 
                                                                       Style="font-size: 12px; color: Black;" OnCheckedChanged="chkSchAllVessel_CheckedChanged" ToolTip="Schedule to Vessel"
                                                                       AutoPostBack="true" />
                                                               </HeaderTemplate>
                                                                <ItemTemplate>
                                                                      <asp:CheckBox ID="chkSch" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgEditSchedule" runat="server" Text="Attachment" 
                                                                        CommandArgument='<%#Eval("[Vessel_ID]")+","+Eval("[Schedule_ID]")+","+Eval("[Document_ID]")+","+((GridViewRow) Container).RowIndex%>' ForeColor="Black"
                                                                        ToolTip='<%#Eval("[Schedule_Type]")%>' ImageUrl="~/Images/edit.gif" Height="16px">
                                                                    </asp:ImageButton>
                                                                
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                                </ItemStyle>
                                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                                     <asp:HiddenField ID="hdfAlertFlag" runat="server" />   
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                </tr>
                               
                            </table> 
                        </td>
                        <td style=" width: 40%; margin-top:-240px;">
                          
                      
                            <asp:Panel ID="pnlShedule" runat="server" Visible="false">
                                   <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
                                   <ContentTemplate>
                               
                              <br />
                                <table style="margin-top: -182px; width: 100%; ">
                                    <tr>
                                        <td style="float:right; margin-top:10px;margin-right:-3px;"   >
                                            <asp:Button ID="btnSaveInspectinAndClose" runat="server" Text="Save Schedule"  OnClientClick="return confirmSaveSchedule();" OnClick="btnSaveAndClose_Click"
                                                CssClass="awesome" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="tblSchedule" style="border: 1px solid black; width:500px;color: black;">
                                                <tr>
                                                    <td style="vertical-align: top; width: 150px; border: 1px solid black;">
                                                        <span style="font-weight: bold; font-size: 12px;">Frequency</span>
                                                        <br />
                                                        <br />
                                                        <asp:RadioButtonList ID="rdoFrequency" runat="server" OnSelectedIndexChanged="rdoFrequency_SelectedIndexChanged"
                                                            AutoPostBack="true" Font-Size="12px" ForeColor="black">
                                                            <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
                                                            <asp:ListItem Text="Monthly" Value="Monthwise"></asp:ListItem>
                                                            <asp:ListItem Text="One Time" Value="Onetime"></asp:ListItem>
                                                            <asp:ListItem Text="Repeat Interval" Value="Duration"></asp:ListItem>
                                                            <asp:ListItem Text="None" Value="NA" ></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td style="vertical-align: top; width:400px;height:200px; border: 1px solid black;">
                                                        <div style="margin-top: 2px; font-size: 12px;" id="dvRange" runat="server">
                                                            Begin:
                                                            <asp:TextBox ID="txtStartDate" runat="server" Width="120px" CssClass="txtInput"></asp:TextBox>&nbsp;
                                                            <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtStartDate" 
                                                                Format="dd/MMM/yy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            End:
                                                            <asp:TextBox ID="txtEndDate" runat="server" Width="120px" CssClass="txtInput"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="calEndDate" runat="server" TargetControlID="txtEndDate" 
                                                                Format="dd/MMM/yy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </div>
                                                        <div style="font-size: 12px;">
                                                            <asp:Panel ID="pnlOneTime" runat="server" Visible="true">
                                                                <table style="font-size: 12px; color: black; ">
                                                                    <tr>
                                                                        <td>
                                                                            Schedule Date : <span style="color: #FF0000">*</span>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtOneTime" runat="server"></asp:TextBox>&nbsp;&nbsp;
                                                                            <ajaxToolkit:CalendarExtender ID="calEtxtOneTime" runat="server" TargetControlID="txtOneTime"
                                                                                Format="dd/MMM/yy">
                                                                            </ajaxToolkit:CalendarExtender>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                   
                                                                </table>
                                                            </asp:Panel>
                                                            <br />
                                                            <asp:Panel ID="pnlWeekly" runat="server" Visible="false">
                                                                Reoccur every <span style="color: #FF0000">*</span>
                                                                <asp:TextBox ID="txtWeek" runat="server" Width="40px" Text="1" CssClass="txtInput"></asp:TextBox>&nbsp;&nbsp;weeks
                                                                on:
                                                                <asp:CheckBoxList runat="server" ID="chkWeekDays" RepeatDirection="Horizontal" RepeatColumns="3"
                                                                    Font-Size="12px" ForeColor="Black">
                                                                    <asp:ListItem Text="Sunday" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Monday" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Tuesday" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="Wednesday" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="Thursday" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="Friday" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="Saturday" Value="7"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlMonthWise" runat="server" Visible="false">
                                                                <table style="font-size: 12px; color: black;">
                                                                    <tr>
                                                                    <td colspan="2">
                                                                        <asp:CheckBox ID="chkEOM" runat="server" Text="End Of Month" Checked="true"  
                                                                            Font-Size="12px" oncheckedchanged="chkEOM_CheckedChanged" 
                                                                            AutoPostBack="True"/>
                                                                    </td>
                                                                 
                                                                    </tr>
                                                                    <tr>
                                                                  
                                                                        <td  >
                                                                           &nbsp;<asp:Label runat="server" ID="lblDD" Text="Due Day :" ></asp:Label>   <span style="color: #FF0000">*</span>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtDueDay" runat="server" Enabled="false"></asp:TextBox>
                                                                        </td>
                                                                       
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:CheckBoxList runat="server" ID="chkMonthWise" RepeatDirection="Vertical" RepeatColumns="3"
                                                                                Font-Size="12px" ForeColor="Black">
                                                                                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                                                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                                                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                                                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                                                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                                                            </asp:CheckBoxList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlStartMonth" runat="server" Visible="false">
                                                                <h4>
                                                                    Start of Every Month</h4>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlEndMonth" runat="server" Visible="false">
                                                                <h4>
                                                                    End of Every month</h4>
                                                            </asp:Panel>
                                                            <br />
                                                            <asp:Panel ID="pnlDuration" runat="server" Visible="false">
                                                                Repeat Every&nbsp;&nbsp;&nbsp;
                                                                <asp:DropDownList runat="server" ID="ddlDuration" Font-Size="12px" ForeColor="Black">
                                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                    <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                    <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                    <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                                    <asp:ListItem Text="32" Value="32"></asp:ListItem>
                                                                    <asp:ListItem Text="33" Value="33"></asp:ListItem>
                                                                    <asp:ListItem Text="34" Value="34"></asp:ListItem>
                                                                    <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                                                    <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                                                    <asp:ListItem Text="37" Value="37"></asp:ListItem>
                                                                    <asp:ListItem Text="38" Value="38"></asp:ListItem>
                                                                    <asp:ListItem Text="39" Value="39"></asp:ListItem>
                                                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                                    <asp:ListItem Text="41" Value="41"></asp:ListItem>
                                                                    <asp:ListItem Text="42" Value="42"></asp:ListItem>
                                                                    <asp:ListItem Text="43" Value="43"></asp:ListItem>
                                                                    <asp:ListItem Text="44" Value="44"></asp:ListItem>
                                                                    <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                                                    <asp:ListItem Text="46" Value="46"></asp:ListItem>
                                                                    <asp:ListItem Text="47" Value="47"></asp:ListItem>
                                                                    <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                                                    <asp:ListItem Text="49" Value="49"></asp:ListItem>
                                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                                    <asp:ListItem Text="51" Value="51"></asp:ListItem>
                                                                    <asp:ListItem Text="52" Value="52"></asp:ListItem>
                                                                    <asp:ListItem Text="53" Value="53"></asp:ListItem>
                                                                    <asp:ListItem Text="54" Value="54"></asp:ListItem>
                                                                    <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                                                    <asp:ListItem Text="56" Value="56"></asp:ListItem>
                                                                    <asp:ListItem Text="57" Value="57"></asp:ListItem>
                                                                    <asp:ListItem Text="58" Value="58"></asp:ListItem>
                                                                    <asp:ListItem Text="59" Value="59"></asp:ListItem>
                                                                    <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                                                    <asp:ListItem Text="61" Value="61"></asp:ListItem>
                                                                    <asp:ListItem Text="62" Value="62"></asp:ListItem>
                                                                    <asp:ListItem Text="63" Value="63"></asp:ListItem>
                                                                    <asp:ListItem Text="64" Value="64"></asp:ListItem>
                                                                    <asp:ListItem Text="65" Value="65"></asp:ListItem>
                                                                    <asp:ListItem Text="66" Value="66"></asp:ListItem>
                                                                    <asp:ListItem Text="67" Value="67"></asp:ListItem>
                                                                    <asp:ListItem Text="68" Value="68"></asp:ListItem>
                                                                    <asp:ListItem Text="69" Value="69"></asp:ListItem>
                                                                    <asp:ListItem Text="70" Value="70"></asp:ListItem>
                                                                    <asp:ListItem Text="71" Value="71"></asp:ListItem>
                                                                    <asp:ListItem Text="72" Value="72"></asp:ListItem>
                                                                    <asp:ListItem Text="73" Value="73"></asp:ListItem>
                                                                    <asp:ListItem Text="74" Value="74"></asp:ListItem>
                                                                    <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                                                    <asp:ListItem Text="76" Value="76"></asp:ListItem>
                                                                    <asp:ListItem Text="77" Value="77"></asp:ListItem>
                                                                    <asp:ListItem Text="78" Value="78"></asp:ListItem>
                                                                    <asp:ListItem Text="79" Value="79"></asp:ListItem>
                                                                    <asp:ListItem Text="80" Value="80"></asp:ListItem>
                                                                    <asp:ListItem Text="81" Value="81"></asp:ListItem>
                                                                    <asp:ListItem Text="82" Value="82"></asp:ListItem>
                                                                    <asp:ListItem Text="83" Value="83"></asp:ListItem>
                                                                    <asp:ListItem Text="84" Value="84"></asp:ListItem>
                                                                    <asp:ListItem Text="85" Value="85"></asp:ListItem>
                                                                    <asp:ListItem Text="86" Value="86"></asp:ListItem>
                                                                    <asp:ListItem Text="87" Value="87"></asp:ListItem>
                                                                    <asp:ListItem Text="88" Value="88"></asp:ListItem>
                                                                    <asp:ListItem Text="89" Value="89"></asp:ListItem>
                                                                    <asp:ListItem Text="90" Value="90"></asp:ListItem>
                                                                    <asp:ListItem Text="91" Value="91"></asp:ListItem>
                                                                    <asp:ListItem Text="92" Value="92"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                &nbsp; Days
                                                            </asp:Panel>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border: 1px solid black;"  align="right">
                                                    <table>
                                                    <tr>
                                                    <td style="font-size: 12px; ">
                                                    <asp:Label runat="server" ID="lblrmk" Text="Remarks :"></asp:Label>
                                                    </td>
                                                    <td style="font-size: 12px;color:Red; ">
                                                    <asp:Label runat="server" ID="lblMandetory" Text="*" Visible="false"></asp:Label>
                                                    </td>
                                                    </tr>
                                                    </table>
                                                        
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="6" Width="100%" CssClass="txtInput"
                                                            Style="width: 100%;"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                
                                   </ContentTemplate>
                               </asp:UpdatePanel>

                             </asp:Panel>
                          
                        </td>
                    </tr>
                  
                </table>
                </td> </tr> </table>
               
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnDocID" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
