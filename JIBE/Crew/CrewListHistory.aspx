<%@ Page Language="C#" Title="Crew List History" AutoEventWireup="true" CodeFile="CrewListHistory.aspx.cs"
    MasterPageFile="~/Site.master" Inherits="Crew_CrewListHistory" EnableEventValidation="false"
    ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
<%--    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>--%>
<script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
    <style type="text/css">
        body
        {
            font-family: Tahoma;
            font-size: 12px;
        }
        #pageTitle
        {
            background-color: gray;
            color: White;
            font-size: 12px;
            text-align: center;
            padding: 2px;
            font-weight: bold;
        }
        .header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
            background-color: #3498DB;/*Changed from  #5C87B2 to  #3498DB*/
        }
        h4
        {
            font-size: 1.2em;
            color: #ffffff;
            font-weight: bold;
            margin: 0 0 0 5px;
        }
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        #page-content a:link
        {
            color: Blue;
            background-color: transparent;
            text-decoration: none;
            border: 0px;
        }
        #page-content a:visited
        {
            color: Blue;
            text-decoration: none;
        }
        #page-content a:hover
        {
            color: Black;
            text-decoration: none;
        }
        
        .pager span
        {
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:visited
        {
            color: blue;
            background-color: white;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:hover
        {
            color: blue;
            background-color: #efefef;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 2px 0px 2px;
        }
        .taskpane
        {
            background-image: url(../images/taskpane.png);
            background-repeat: no-repeat;
            background-position: -2px -2px;
        }
        .tooltip
        {
            display: none;
            background: transparent url(../Images/black_arrow.png);
            font-size: 12px;
            height: 70px;
            width: 160px;
            padding: 25px;
            color: #fff;
        }
        .interview-schedule-table
        {
            padding: 0;
            border-collapse: collapse;
        }
        .interview-schedule-table div
        {
            border: 0px solid gray;
            height: 16px;
            width: 16px;
            margin-top: 2px;
            background: url(../Images/Interview_1.png) no-repeat;
        }
        .Grid_CSS
        {
            cursor: pointer;
        }
        GridRow_CSS
        {
            cursor: pointer;
        }
        .CrewStatus_Current
        {
            background-color: #aabbdd;
        }
        .CrewStatus_SigningOff
        {
            background-color: #F3F781;
        }
        .CrewStatus_SignedOff
        {
            background-color: #F5A9A9;
        }
        .CrewStatus_Assigned
        {
            background-color: #BBB6FF;
        }
        .CrewStatus_Planned
        {
            background-color: #F781F3;
        }
        .CrewStatus_Pending
        {
            background-color: #81BEF7;
        }
        .CrewStatus_Inactive
        {
            background-color: #848484;
            color: #E6E6E6;
        }
        .CrewStatus_NoVoyage
        {
            background-color: #A9F5D0;
        }
        .hide
        {
            display: none;
        }
        .show
        {
            display: block;
        }
        #dialog
        {
            position: absolute;
            background-color: #BCF5A9;
            border: 2px solid orange;
        }
    </style>
    <style type="text/css" media="print">
        .NoPrint
        {
            display: none;
        }
    </style>
    <script id="DocumentReady" type="text/javascript">
        $(document).ready(function () {
            var strDateFormat = "<%= DateFormat %>";

            $("#tabs").tabs();
            $(".hover-parent").mouseenter(function (event) {
                var CrewID = $(this).attr("id");
                getDialogContent(CrewID, event);
            })
            .mouseout(function () {
                $('#dialog').hide();
            });

            $("body").on("click", "#btnSearch", function () {
                var Msg = "";
                if ($.trim($("#txtSearchJoinFromDate").val()) != "") {
                    if (IsInvalidDate($("#txtSearchJoinFromDate").val(), strDateFormat)) {
                        Msg += "Enter Valid Onboard From Date<%=TodayDateFormat %>\n";
                    }
                }
                if ($.trim($("#txtSearchJoinToDate").val()) != "") {
                    if (IsInvalidDate($("#txtSearchJoinToDate").val(), strDateFormat)) {
                        Msg += "Enter Valid Onboard To Date<%=TodayDateFormat %>\n";
                    }
                }
                if (($.trim($("#txtSearchJoinFromDate").val()) != "") && ($.trim($("#txtSearchJoinToDate").val()) != "")) {
                    if (DateAsFormat($.trim($("#txtSearchJoinFromDate").val()), strDateFormat) > DateAsFormat($.trim($("#txtSearchJoinToDate").val()), strDateFormat)) {
                        Msg += " Onboard From date cannot be greater than to date\n";
                    }
                }
                if (Msg != "") {
                    alert(Msg);
                    return false;
                }
            });

        });


        function Regsisterfunction() {
            $(".hover-parent").mouseenter(function (event) {
                var CrewID = $(this).attr("id");
                getDialogContent(CrewID, event);
            })
            .mouseout(function () {
                $('#dialog').hide();
            });
        }

        function PrintDiv(dvID) {
            var a = window.open('', '', 'left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
            a.document.write(document.getElementById(dvID).innerHTML);
            a.document.close();
            a.focus();
            a.print();
            a.close();
            return false;
        }


        function getDialogContent(CrewID, event) {
            var src = event.target;
            var pos = $(src).offset();
            var width = $(src).width();
            $('#dialog').load('Crew_ToolTip.aspx?CrewID=' + CrewID + ' #MainDiv');
            $('#dialog').show();


            if (pos.left < 1000)
                $("#dialog").css({ "left": (pos.left + width) + "px", "top": pos.top + "px", "width": 600 });
            else
                $("#dialog").css({ "left": (pos.left - 600) + "px", "top": pos.top + "px", "width": 600 });
        };


        function getHtmlContentsForPhotoToolTip_result(htmlresult) {
            $('#dialog').html(htmlresult);
            $('#dialog').show();
        }

  
        
    </script>
    <script language="javascript" type="text/javascript">
        function showDiv(dv, id) {
            if (id)
            { document.getElementById("frmCrewCard").src = 'ProposeCrewCard.aspx?CrewID=' + id; }

            document.getElementById(dv).style.display = "block";
        }
        function closeDiv(dv) {
            document.getElementById(dv).style.display = "None";
        }
        function reloadFrame(fr) {
            document.getElementById(fr).src = document.getElementById(fr).src + "&rnd=" + Math.random;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server" >
    <div class="page-title">
        Crew History
    </div>
    <asp:ScriptManagerProxy ID="smp1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/WebService.asmx" />
        </Services>
    </asp:ScriptManagerProxy>
   <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
    <div style="font-family: Tahoma; font-size: 12px; border: 1px solid gray; height: 1200px;
        vertical-align: middle;">
 
        <div style="font-family: Tahoma; font-size: 12px;">
            <table cellpadding="2" cellspacing="2">
                <tr>
                    <td>
                        Vessel : &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="true" Font-Size="11px"
                            Width="120px" Height="20px" BackColor="#FFFFCC">
                        </asp:DropDownList>
                    </td>
                     <td>
                        Nationality
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCountry" runat="server" Width="156px">
                        </asp:DropDownList>
                    </td>
                    <td>
                                    Rank
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRank" runat="server" Width="115px" >
                                    </asp:DropDownList>
                   </td>
                    <td>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="Search"  OnClick="btnSearch_Click" ClientIDMode="Static" />
                    </td>

                </tr>
                <tr>
                <td>
                    Onboard From
                </td>
                <td>
                    <asp:TextBox ID="txtSearchJoinFromDate" runat="server" Width="100px" ClientIDMode="Static"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtSearchJoinFromDate" >
                    </ajaxToolkit:CalendarExtender>
                </td>
                    <td>
                    Onboard To
                </td>
                <td>
                    <asp:TextBox ID="txtSearchJoinToDate" runat="server" Width="100px"  ClientIDMode="Static"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSearchJoinToDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td></td><td></td><td></td>
                <td><asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" Text="Clear Filter" /></td>
                </tr>
            </table>
        </div>
        <br />
        <div style="font-family: Tahoma; font-size: 12px;">
            <asp:UpdatePanel ID="UpdTabCrwHistory" runat="server">
                <ContentTemplate><%--Height="850px"--%>
                    <cc1:TabContainer ID="TabCrwHistory" runat="server" ActiveTabIndex="1" 
                        OnClientClick="hideDiv();">
                        <cc1:TabPanel runat="server" HeaderText="&nbsp; Crew List View &nbsp;" ID="Injd"
                            TabIndex="0">
                            <ContentTemplate>
                                <div style="color: Black; overflow: hidden">
                                    <asp:GridView ID="gvCrwHistroy" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        CellSpacing="1" EmptyDataText="No Record Found" GridLines="both" DataKeyNames="ID"
                                        Width="100%" AllowSorting="false"  AllowPaging="false">
                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                        <RowStyle Font-Size="11px" CssClass="PMSGridRowStyle-css" />
                                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                        <PagerStyle CssClass="PMSPagerStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Staff Code">
                                                <ItemTemplate>
                                                    <a href='CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                                        <%# Eval("staff_Code")%></a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSTAFF_NAME" runat="server" Text='<%# Eval("Staff_FullName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="200px" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rank">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSTAFF_NATIONALITY" runat="server" Text='<%# Eval("ISO_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DOB">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSTAFF_BIRTH_DATE" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("STAFF_BIRTH_DATE"))) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="80px" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OnBD">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblONBD" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sign On Dt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJoining_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("SIGN_ON_DATE"))) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="80px" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>                                           
                                            <asp:TemplateField HeaderText="Sign Off Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Sing_Off_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Sign_Off_Date"))) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="80px" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="COC Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEst_Sing_Off_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Est_Sing_Off_Date"))) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="80px" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sign Off Port">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Sing_Off_Port" runat="server" Text='<%# Eval("SignOffPort")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="80px" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Salary">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalary" runat="server" Text='<%# Eval("Salary")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Manning Office">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblManning" runat="server" Text='<%# Eval("MANNING_OFFICE")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="130px" HorizontalAlign="Left" Wrap="true" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="CrewStatus">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCrewStatus" runat="server" Text='<%# Eval("CrewStatus")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="70px" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                                        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                                        background-color: #F6CEE3;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                 <td style="width: 60%">
                                                     <uc1:ucCustomPager ID="ucCustomPager_CrewList" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Staff" PageSize="30"
                                                        AlwaysGetRecordsCount="true" OnBindDataItem="BindCrewHistoryGrid" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            
</ContentTemplate>
                        



</cc1:TabPanel>
                        <cc1:TabPanel runat="server" HeaderText="&nbsp; Crew Photo View &nbsp;" ID="TabPanel1"
                            TabIndex="1">
                            <ContentTemplate>
                                <asp:Repeater runat="server" ID="rpt1" OnItemDataBound="rpt1_ItemDataBound"><ItemTemplate>
                                        <div style="float: left; border: 0px solid gray; width: 100%; margin-top: 10px; font-size: 10px;
                                            background-color: #ffffff;">
                                            <asp:Repeater runat="server" ID="rpt2" OnItemDataBound="rpt2_ItemDataBound">
                                                <ItemTemplate>
                                                    <div style="float: left; padding: 2px; border: 2px solid #aabbdd; height: 190px;
                                                        overflow: hidden; text-align: center; margin: 4px 0px 4px 4px; background-color: #cfdfef;">
                                                        <div id="dvCrewCard" style="position: absolute">
                                                            <asp:ImageButton runat="server" ID="ImgCard" ImageUrl="" Visible="false" />
                                                        </div>
                                                        <div>
                                                            <table style="width: 120px">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <img src="../Uploads/CrewImages/<%# Eval("PhotoUrl")%>" alt="" style="border: 0;
                                                                            width: 90px;" id='<%# Eval("ID")%>' class="hover-parent" onclick="javascript:window.open('CrewDetails.aspx?ID=<%# Eval("ID")%>')" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="font-weight: bold; text-align: left;">
                                                                        <%# Eval("Rank_Short_Name")%>
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: right;">
                                                                        <%# Eval("Staff_Code")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="text-align: left; font-size: 10px;">
                                                                        <%# Eval("Staff_FullName")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="font-weight: bold; text-align: left;">
                                                                        Nationality
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: right;">
                                                                        <%# Eval("Nationality")%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    
</ItemTemplate>
</asp:Repeater>




                               <div style="vertical-align: bottom; text-align: left;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                                        <tr>
                                                <td style="width: 60%">
                                                    <uc1:ucCustomPager ID="ucCustomPager1" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Staff" PageSize="30"
                                                    AlwaysGetRecordsCount="true" OnBindDataItem="BindCrewHistoryGrid" />




                                            </td>
                                        </tr>
                                    </table>
                                </div>                                 
                            
</ContentTemplate>
                        



</cc1:TabPanel>
                    </cc1:TabContainer>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="dialog" title="Staff Info" style="top: 0px; left: 0px; display: none;">
        Loading Data ...
    div>
  

</asp:Content>
