<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttachJob.aspx.cs" Inherits="Technical_Worklist_AttachJob" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <style type="text/css">
        .divgrid
        {
            height: 300px; /*width: 370px;*/
            width: 100%;
        }
        
        .divgrid table
        {
            width: 350px;
        }
        
        .divgrid table th
        {
            background-color: #E2E2E2;
            color: black;
        }
        
        .MarginLeftClass
        {
            /* margin-left:24px;*/
        }
        
        .IE
        {
            margin-left: 4px;
        }
        .Firefox
        {
            margin-left: 10px !important;
        }
        
        .Chrome
        {
            margin-left: 10px !important;
        }
        .emptyTemplateShtyle
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="frmAttacheJOB" runat="server">
    <script type="text/javascript" type="text/javascript">



        function showDialog(url) {
            window.open(url);
        }
        function AddDefect(LocationID, InspID, VesselID) {

            document.getElementById('IframeJob').src = 'AddConditionReport.aspx?LocationID=' + LocationID + '&InspID=' + InspID + '&VesselID=' + VesselID;

            showModal('dvJob');
            $("#dvJob").prop('title', 'Defect/Condition Report');
            return false;
        }

        function Confirm() {

            // alert(document.getElementById("ctl00_MainContent_hdnDirtyCounter").value);
            if (parseInt(document.getElementById("ctl00_MainContent_hdnDirtyCounter").value) > 0) {
                if (confirm("Do you want to save changes?")) {
                    document.getElementById("ctl00_MainContent_hdnConfirm").value = "Yes";
                } else {

                    document.getElementById("ctl00_MainContent_hdnConfirm").value = "No";
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
                var headerCheckBox = document.getElementById($('[id$=checkAll]').attr('id')); //inputList[0];

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

        function GetRating(event, th) {

            var rowIndex = th.offsetParent.parentNode.rowIndex;
            var cellIndex = th.offsetParent.cellIndex;
            var ddlRating = $('[id$=grdSubCatRating]').find(th).val();
            //      alert(ddlRating);
            var grd = document.getElementById($('[id$=grdSubCatRating]').attr('id'));

            var lblRate = $(th).closest("tr").find('[id$=lblSubCatRating]');
            var lblRateControl = document.getElementById(lblRate.attr('id'));
            //      alert(lblRate.attr('id'));
            //    alert(lblRate.attr('text'));

            var Rate = ddlRating.toString();
            //   alert(Rate.split('_')[0]);
            lblRateControl.innerText = Rate.split('_')[0];
            lblRateControl.textContent = Rate.split('_')[0];
            //  alert(lblRateControl.innerText)
            //  lblRateControl.innerText = ddlRating.toString().;
            //lblRate.attr('text', ddlRating);
            //  alert(lblRateControl.innerText);



        }

        function checkAll(objRef) {
            //var GridView = objRef.parentNode.parentNode.parentNode;
            var GridView = document.getElementById($('[id$=grdJoblist]').attr('id'));
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (inputList[i].disabled != true) {
                        if (objRef.checked) {
                            //If the header checkbox is checked
                            //check all checkboxes
                            //and highlight all rows
                            if (inputList[i].disabled == false) {
                                inputList[i].checked = true;
                            }
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
        }


        $(document).ready(function () {


        }

);

        function GridWorkFlow() {
            try {

                var isOpera;
                var isFirefox;
                var isSafari;
                var isChrome;
                var isIE;

                // GridWorkFlow();
                isOpera = !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
                // Opera 8.0+ (UA detection to detect Blink/v8-powered Opera)
                isFirefox = typeof InstallTrigger !== 'undefined'; // Firefox 1.0+
                isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
                // At least Safari 3+: "[object HTMLElementConstructor]"
                isChrome = !!window.chrome && !isOpera; // Chrome 1+
                isIE = /*@cc_on!@*/false || !!document.documentMode; // At least IE6



                $(".divgrid").each(function () {

                    var grid = $(this).find("table")[0];

                    var ScrollHeight = $(this).height();

                    var gridWidth = $(this).width() - 20;

                    var headerCellWidths = new Array();

                    for (var i = 0; i < grid.getElementsByTagName('TH').length; i++) {

                        headerCellWidths[i] = grid.getElementsByTagName('TH')[i].offsetWidth;

                    }

                    grid.parentNode.appendChild(document.createElement('div'));

                    var parentDiv = grid.parentNode; var table = document.createElement('table');

                    for (i = 0; i < grid.attributes.length; i++) {

                        if (grid.attributes[i].specified && grid.attributes[i].name != 'id') {

                            table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);

                        }

                    }

                    table.style.cssText = grid.style.cssText;

                    table.style.width = gridWidth + 'px';

                    table.appendChild(document.createElement('tbody'));

                    table.getElementsByTagName('tbody')[0].appendChild(grid.getElementsByTagName('TR')[0]);

                    var cells = table.getElementsByTagName('TH');

                    var gridRow = grid.getElementsByTagName('TR')[0];

                    for (var i = 0; i < cells.length; i++) {

                        var width; if (headerCellWidths[i] > gridRow.getElementsByTagName('TD')[i].offsetWidth) {

                            width = headerCellWidths[i];

                        } else {

                            width = gridRow.getElementsByTagName('TD')[i].offsetWidth;

                        } cells[i].style.width = parseInt(width - 3) + 'px';

                        gridRow.getElementsByTagName('TD')[i].style.width = parseInt(width - 3) + 'px';

                    }

                    var gridHeight = grid.offsetHeight;

                    if (gridHeight < ScrollHeight)

                        ScrollHeight = gridHeight;

                    parentDiv.removeChild(grid);

                    var dummyHeader = document.createElement('div');

                    dummyHeader.appendChild(table); parentDiv.appendChild(dummyHeader);

                    var scrollableDiv = document.createElement('div');

                    if (parseInt(gridHeight) > ScrollHeight) {

                        gridWidth = parseInt(gridWidth) + 17;

                    }

                    //scrollableDiv.style.cssText = 'overflow:auto;height:' + ScrollHeight + 'px;width:' + gridWidth + 'px';
                    scrollableDiv.style.cssText = 'overflow:auto;height:300px;width:' + gridWidth + 'px';

                    scrollableDiv.appendChild(grid);

                    parentDiv.appendChild(scrollableDiv);

                });

                //$("checkAll").attr("margin-left", "5px");
                // document.getElementById($('[id$=checkAll]').attr('id')).style.marginLeft = "5px";
                if (isIE == true)
                    $(".MarginLeftClass").addClass("IE");

                if (isChrome == true)
                    $(".MarginLeftClass").addClass("Chrome");

                if (isFirefox == true)
                    $(".MarginLeftClass").addClass("Firefox");


                document.getElementById("tblPager").style.width = "98.5%";
            }

            catch (err) { }
        }

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanelport" runat="server">
        <ContentTemplate>
            <div id="dvWorklist" style="display: block; height: 480px; overflow: hidden;">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div id="dx" style="display: none">
                        </div>
                        <div id="dy" style="display: none">
                        </div>
                        <script type="text/javascript">
                            $(document).on("mousemove", function (event) {

                                $("#dx").text(event.pageX);
                                $("#dy").text(event.pageY);

                            });


                            function showFollowups(V, W, O, M) {

                                var evt = window.event || M; // this assign evt with the event object
                                var src = evt.srcElement; // this assign current with the event target
                                var pos = 0;
                                var width = 0;
                                var x = 0;
                                var y = 0;
                                var min = 0;
                                if (src == null) {
                                    src = evt.target;
                                    x = evt.x;
                                    y = evt.y;
                                    min = 210;
                                }
                                else {
                                    pos = $(src).offset();
                                    width = $(src).width();
                                    x = pos.left;
                                    y = pos.top;
                                    min = 120;
                                }
                                // var src = window.event.srcElement;
                                x = $("#dx").text();
                                y = $("#dy").text();


                                var url = 'Task_Followups.aspx?WLID=' + W + '&VID=' + V + '&OFFID=' + O;

                                $('#iframeFollowups').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });
                                $('#iframeFollowups').attr("src", url);
                                $('#dialog').show();
                                $("#dialog").css({ "left": (x - 600) + "px", "top": (y - min) + "px", "width": 600 });


                            }
                        </script>
                        <div id="dialog" title="Follow-ups" style="top: 0px; left: 0px; width: 600px; display: none;
                            position: absolute;">
                            <iframe id="iframeFollowups" style="width: 100%; height: 100%; border: 0px;"></iframe>
                        </div>
                        <input type="hidden" runat="server" id="hdnFlagCheck" value="false" />
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 100%">
                                    <div style="width: 100%; text-align: left; height: 400px;">
                                        <div id="Div2" style="border: 1px solid #aabbdd; background-color: #efefef; padding: 8px;
                                            margin-top: 5px; margin-bottom: 2px;">
                                            <div>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="font-weight: bold; padding-right: 20px; font-size: 14px; font-family: Calibri;">
                                                            Total Jobs:&nbsp;&nbsp;<asp:Label ID="lblRecordCount" runat="server" Text="0"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <div style="height: 6px; width: 6px; background-color: transparent;" title="Priority: URGENT">
                                                            </div>
                                                        </td>
                                                        <td style="font-weight: bold; padding-right: 20px; font-size: 14px; font-family: Calibri;">
                                                            JOB Status Filter:&nbsp;
                                                        </td>
                                                        <td style="font-weight: bold; padding-right: 20px; font-size: 14px; font-family: Calibri;">
                                                            <asp:RadioButtonList ID="rblJobStaus" runat="server" RepeatDirection="Horizontal"
                                                                TextAlign="Right" CellPadding="1" CellSpacing="0" AutoPostBack="True" OnSelectedIndexChanged="Filter_Changed">
                                                                <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                                                <asp:ListItem Value="PENDING" Text="Pending" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="COMPLETED" Text="Completed"></asp:ListItem>
                                                                <asp:ListItem Value="REWORKED" Text="Re-worked"></asp:ListItem>
                                                                <asp:ListItem Value="CLOSED" Text="Verified"></asp:ListItem>
                                                                <asp:ListItem Value="OVERDUEPENDING" Text="Overdue"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:TextBox ID="txtDescription" runat="server" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <table border="0" cellpadding="1" cellspacing="1" width="100%" style="margin-top: 10px">
                                                                <tr>
                                                                    <td style="width: 150px; text-align: center;">
                                                                        <asp:ImageButton ID="ImgBtnSearch" ImageUrl="~/Images/SearchAndReload.png" 
                                                                            runat="server" onclick="ImgBtnSearch_Click" />
                                                                        <asp:ImageButton ID="ImgBtnClearFilter" ImageUrl="~/Images/ClearFilter.png" 
                                                                            runat="server" onclick="ImgBtnClearFilter_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="divgrid">
                                            <asp:GridView ID="grdJoblist" runat="server" AutoGenerateColumns="false" CellPadding="4"
                                                ShowHeaderWhenEmpty="true" DataKeyNames="WORKLIST_ID,VESSEL_ID,OFFICE_ID" EnableModelValidation="True"
                                                AllowSorting="false" Width="100%" GridLines="None" OnRowCommand="grdJoblist_RowCommand"
                                                OnRowDataBound="grdJoblist_RowDataBound" OnSorting="grdJoblist_Sorting" AllowPaging="false">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Width="10px" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgAnyChanges" runat="server" Height="16px" Width="16px" ImageUrl="~/Images/exclamation.gif"
                                                                Visible='<%#Eval("MODIFIED").ToString()=="1"?true:false %>' ToolTip="Modified in last 3 days." />
                                                            <%--<div style="height: 6px; width: 6px; background-color: <%#Eval("WL_PRIORITY_COLOR").ToString()%>"
                                                            title="Priority: <%#Eval("PRIORITY").ToString()%>">
                                                        </div>--%>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="0px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="Priority">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPriority" runat="server" Text='<%#Eval("PRIORITY") %>' Style="white-space: nowrap"></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="0px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="Vessel" SortExpression="Vessel_Short_Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVesselShortName" runat="server" Text='<%#Eval("Vessel_Name") %>'
                                                                Style="white-space: nowrap"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Code" SortExpression="WORKLIST_ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljobcodegriditem" runat="server" Text='<%#Eval("WLID_DISPLAY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Description" SortExpression="JOB_DESCRIPTION"
                                                        ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <a href='../Worklist/ViewJob.aspx?OFFID=<%#Eval("OFFICE_ID") %>&WLID=<%#Eval("WORKLIST_ID") %>&VID=<%#Eval("VESSEL_ID") %>'
                                                                target="_blank" style="cursor: hand; color: Blue; text-decoration: none;">
                                                                <asp:Label ID="jd" runat="server" ToolTip='<%#Eval("JOB_DESCRIPTION")%>' Text='<%#Eval("JOB_DESCRIPTION").ToString().Length > 80 ?  Eval("JOB_DESCRIPTION").ToString().Substring(0, 80) + "..." : Eval("JOB_DESCRIPTION").ToString() %>'></asp:Label></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="Assignor" SortExpression="AssignorName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdAssignor" runat="server" Text='<%#Eval("AssignorName") %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="PIC" SortExpression="PIC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPIC" runat="server" Text='<%#Eval("USER_NAME") %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="Date Raised" SortExpression="DATE_RAISED">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdRaisedDate" runat="server" Text='<%# Eval("DATE_RAISED","{0:dd/MMM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_RAISED","{0:d/MMM/yy}")  %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="Office Dept" SortExpression="INOFFICE_DEPT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdofficeDept" runat="server" Text='<%#Eval("INOFFICE_DEPT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="Vessel Dept" SortExpression="ONSHIP_DEPT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdVesselDept" runat="server" Text='<%#Eval("ONSHIP_DEPT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Expected Compln" SortExpression="DATE_ESTMTD_CMPLTN">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDATE_ESTMTD_CMPLTN" runat="server" Text='<%# Eval("DATE_ESTMTD_CMPLTN","{0:dd/MMM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_ESTMTD_CMPLTN","{0:d/MMM/yy}") %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Completed" SortExpression="DATE_COMPLETED">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdCompletedOn" runat="server" Text='<%# Eval("DATE_COMPLETED","{0:dd/MMM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_COMPLETED","{0:d/MMM/yy}") %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="NCR" SortExpression="NCR" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdNCR" runat="server" Text='<%#Eval("NCR").ToString()=="0"?"":"YES" %>'></asp:Label></ItemTemplate>
                                                        <ControlStyle Width="30px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Job Type" SortExpression="Worklist_Type_Display" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdJobType" runat="server" Text='<%#Eval("Worklist_Type_Display")%>'></asp:Label></ItemTemplate>
                                                        <ControlStyle Width="30px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderText="Att.">
                                                        <ItemTemplate>
                                                            <asp:Image ID="ImgAttachment" runat="server" ImageUrl="~/Images/attach.png" AlternateText="Attachment" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="WORKLIST_STATUS" HeaderText="Status" />
                                                    <%--  <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                    HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgFlagOFF" runat="server" ImageUrl="~/Images/Flag_Off.png"
                                                            Visible='<%# !Convert.ToBoolean(Eval("FLAG_Tech_Meeting")) %>' CommandName="FlagJobForMeeting"
                                                            CommandArgument='<%#Eval("OFFICE_ID").ToString()+ "," + Eval("WORKLIST_ID").ToString() + "," + Eval("VESSEL_ID").ToString()+ ",1"%>' />
                                                        <asp:ImageButton ID="imgFlagON" runat="server" ImageUrl="~/Images/Flag_ON.png" Visible='<%# Convert.ToBoolean(Eval("FLAG_Tech_Meeting")) %>'
                                                            CommandName="FlagJobForMeeting" CommandArgument='<%#Eval("OFFICE_ID").ToString()+ "," + Eval("WORKLIST_ID").ToString() + "," + Eval("VESSEL_ID").ToString()+ ",0"%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" CssClass="MarginLeftClass" runat="server" onclick="checkAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="checkRow" runat="server" onclick="Check_Click(this)" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <label id="Label1" runat="server">
                                                        No jobs found !!</label>
                                                </EmptyDataTemplate>
                                                <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" Font-Bold="true" Font-Size="14px"
                                                    Font-Names="Calibri" />
                                                <PagerStyle CssClass="PagerStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" Font-Size="14px" Font-Names="Calibri" />
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                                                <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                                                <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                                                <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                                                <EmptyDataRowStyle CssClass="emptyTemplateShtyle" />
                                            </asp:GridView>
                                            <auc:CustomPager ID="ucCustomPagerctp" OnBindDataItem="Search_Worklist" AlwaysGetRecordsCount="true"
                                                PageSize="10" RecordCountCaption="Total Jobs" runat="server" />
                                        </div>
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: right">
                            <asp:Button ID="btnAssign" runat="server" Text="Assign" Style="margin-top: 10px;
                                width: 100px" OnClick="btnAssign_Click" Visible="false" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAssignandClose" runat="server" Text="Save" Style="margin-top: 10px;
                                width: 200px; height: 25px;" OnClick="btnAssignandClose_Click" />
                            <asp:Button ID="btnGenerateReport" runat="server" Text="Generate Report" Style="margin-top: 10px;
                                width: 150px; margin-left: 10px; margin-right: 10px" OnClick="btnGenerateReport_Click"
                                Visible="false" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<div id="dvPopupAddAttachment" title="Add Attachments" style="display: none; width: 500px;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                            <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                                Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                MaximumNumberOfFiles="10" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>--%>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnOPMode" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="hdnfldUserID" runat="server" />
    <asp:HiddenField ID="hdnfldVesselType" runat="server" />
    <asp:HiddenField ID="hdnQuerystring" runat="server" />
    <asp:HiddenField ID="hdnQuerystringInspID" runat="server" />
    <asp:HiddenField ID="hdnParentID" runat="server" />
    <asp:HiddenField ID="hdnVesselID" runat="server" />
    <asp:HiddenField ID="hdnLocationID" runat="server" />
    <asp:HiddenField ID="hdnLocationNodeID" runat="server" />
    </form>
</body>
</html>
