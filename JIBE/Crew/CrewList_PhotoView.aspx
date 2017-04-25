<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewList_PhotoView.aspx.cs"
    Inherits="CrewList_PhotoView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crew List</title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>    
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <script src="../Scripts/StaffInfo.js" type="text/javascript"></script>
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
            $("#tabs").tabs();

            $(".hover-parent").mouseenter(function (event) {
                var CrewID = $(this).attr("id");
                getDialogContent(CrewID, event);

            })
            .mouseout(function () {
                $('#dialog').hide();
            });

        });

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

            $.get('Crew_ToolTip.aspx?CrewID=' + CrewID, function (data) {
                $('#dialog').html(data);
                $('#dialog').show();
                if (pos.left < 1000)
                    $("#dialog").css({ "left": (pos.left + width) + "px", "top": pos.top + "px", "width": 600 });
                else
                    $("#dialog").css({ "left": (pos.left - 600) + "px", "top": pos.top + "px", "width": 600 });

            });
        };
        
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
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="tabs" style="margin-top: 5px;">
        <div style="float: right; padding: 5px;">
            <img src="../Images/Printer.png" style="cursor: pointer;" title="*Print*" alt="" onclick="PrintDiv('fragment-1')" /></div>
        <ul class="NoPrint">
            <li><a href="#fragment-0">
                <img src="../Images/User2.png" alt="Icon View" style="border: 0; height: 15px; vertical-align: baseline" /><span>
                    Photo View</span></a></li>
            <li><a href="#fragment-1">
                <img src="../Images/task-list.gif" alt="Icon View" style="border: 0; height: 15px;
                    vertical-align: baseline" /><span> List View</span></a></li>
                    <li style="font-weight:bold">
                    <asp:Literal ID ="ltCrewCount" runat="server" ></asp:Literal>
                    </li>
        </ul>

        <div id="fragment-0" style="padding: 0px; display: block;">
            <div style="float: left; border: 1px solid gray; margin-top: 5px; padding: 10px;width:99%;
                background-color: #aabbdd;">
                <%--<div id="wall" style="height: 100%">
                    <!-- 3D Wall Goes Here -->
                </div>--%>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Repeater runat="server" ID="rpt1" OnItemDataBound="rpt1_ItemDataBound">
                            <ItemTemplate>
                                <div style="float: left; border: 1px solid gray; width: 100%; margin-top: 10px; font-size: 10px;
                                    background-color: #ffffff;">
                                    <asp:Repeater runat="server" ID="rpt2" OnItemDataBound="rpt2_ItemDataBound">
                                        <ItemTemplate>
                                            <div style="float: left; padding: 2px; border: 2px solid #aabbdd; height: 180px;
                                                overflow: hidden; text-align: center; margin: 4px 0px 4px 4px; background-color: #cfdfef;">
                                                <div id="dvCrewCard" style="position:absolute"><asp:ImageButton runat="server" ID="ImgCard" ImageUrl="" Visible="false" /></div>
                                                <div>
                                                    <table style="width: 120px">
                                                        <tr>
                                                            <td colspan="2">
                                                                <img src="../Uploads/CrewImages/<%# Eval("PhotoUrl")%>" alt="" style="border: 0;
                                                                    width: 90px;" id='<%# Eval("ID")%>' class="hover-parent" onclick="javascript:window.open('CrewDetails.aspx?ID=<%# Eval("ID")%>')"/>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="fragment-1" style="padding: 2px; display: block">
            <div style="float: left; border: 1px solid gray; margin-top: 5px; padding: 5px; width: 99%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlVessel" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        Vessel
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div id="page-content">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                                Width="100%" EmptyDataText="No Record Found" CaptionAlign="Bottom" GridLines="Horizontal"
                                DataKeyNames="ID" OnRowDataBound="GridView1_RowDataBound" Font-Size="12px" AllowSorting="false"
                                CssClass="Grid_CSS">
                                <Columns>
                                    
                                    <asp:TemplateField HeaderText="Staff Code" SortExpression="STAFF_CODE" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <a href='CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                                <%# Eval("staff_Code")%></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="Staff_FullName" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTAFF_NAME" runat="server" Text='<%# Eval("Staff_FullName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rank" SortExpression="Rank_Name" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nation" SortExpression="ISO_Code" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTAFF_NATIONALITY" runat="server" Text='<%# Eval("ISO_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DOB" SortExpression="STAFF_BIRTH_DATE" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTAFF_BIRTH_DATE" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("STAFF_BIRTH_DATE"))) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OnBD" SortExpression="Vessel_Short_Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblONBD" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sign On Dt" SortExpression="Joining_Date" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJoining_Date" runat="server"  Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("SIGN_ON_DATE"))) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="COC Date" SortExpression="Est_Sing_Off_Date" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEst_Sing_Off_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Est_Sing_Off_Date"))) %>' ></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salary" SortExpression="Salary" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSalary" runat="server" Text='<%# Eval("Salary")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manning Office" SortExpression="MANNING_OFFICE" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblManning" runat="server" Text='<%# Eval("MANNING_OFFICE")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="130px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" SortExpression="CrewStatus">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCrewStatus" runat="server" Text='<%# Eval("CrewStatus")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="70px" Font-Size="10px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#487575" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#275353" />
                                <PagerStyle Font-Size="Larger" CssClass="pager" BackColor="#336666" ForeColor="White"
                                    HorizontalAlign="Center" />
                                <RowStyle CssClass="GridRow_CSS" BackColor="White" ForeColor="#333333" />
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="dialog" title="Staff Info" style="top: 0px; left: 0px; display: none;">
        Loading Data ...
    </div>
    </form>
</body>
</html>
