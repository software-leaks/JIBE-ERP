<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CrewSeniority.aspx.cs"
    EnableEventValidation="false" Title="Crew Seniority" Inherits="Account_Portage_Bill_CrewSeniority" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.alerts.js" type="text/javascript"></script>
    <style type="text/css">
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
    </style>
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
            color: blue;
            background-color: transparent;
            text-decoration: none;
            border: 0px;
        }
        #page-content a:visited
        {
            color: black;
            text-decoration: none;
        }
        #page-content a:hover
        {
            color: blue;
            text-decoration: none;
        }
        
        .pager span
        {
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
            background: url(../Images/bg.png) left -1672px repeat-x;
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
            padding: 0px 3px 0px 3px;
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
            height: 90px;
            width: 180px;
            padding: 15px;
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
        .CrewStatus_NTBR
        {
            background-color: RED;
            color: Yellow;
        }
        .CrewStatus_NTBR_Row
        {
            color: Red;
        }
        .CrewStatus_UNFIT
        {
            background-color: RED;
            color: Yellow;
        }
        .CrewStatus_UNFIT_Row
        {
            color: Red;
        }
        .imgCOC
        {
            vertical-align: middle;
        }
        .CrewStatus_Rejected
        {
            background-color: RED;
            color: Yellow;
        }
        input
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        select
        {
            height: 21px;
            font-family: Tahoma;
            font-size: 11px;
        }
    </style>
    <style id="TooltipStyle" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: arial;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .dvhdr1
        {
            font-family: Tahoma;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
            width: 200px;
            color: Black; /*background: #F5D0A9;*/
            border: 1px solid #F1C15F;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .dvbdy1
        {
            background: #FFFFFF;
            font-family: arial;
            font-size: 11px; /*border-left: 2px solid #3B0B0B;
            border-right: 2px solid #3B0B0B;
            border-bottom: 2px solid #3B0B0B;*/
            padding: 5px;
            width: 200px;
            background-color: #E0F8E0;
            border: 1px solid #F1C15F;
        }
        p
        {
            margin-top: 20px;
        }
        h1
        {
            font-size: 13px;
        }
        .dogvdvhdr
        {
            width: 300;
            background: #C4D5E3;
            border: 1px solid #C4D5E3;
            font-weight: bold;
            padding: 10px;
        }
        .dogvdvbdy
        {
            width: 300;
            background: #FFFFFF;
            border-left: 1px solid #C4D5E3;
            border-right: 1px solid #C4D5E3;
            border-bottom: 1px solid #C4D5E3;
            padding: 10px;
        }
        .pgdiv
        {
            width: 320;
            height: 250;
            background: #E9EFF4;
            border: 1px solid #C4D5E3;
            padding: 10px;
            margin-bottom: 20;
            font-family: arial;
            font-size: 12px;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
    </style>
    <style type="text/css">
        .tdh
        {
            font-size: 12px;
            text-align: right;
            padding: 2px 3px 2px 0px;
            vertical-align: middle;
            font-weight: bold;
            width: 100px;
            border: 1px solid #DADADA;
        }
        .tdd
        {
            font-size: 12px;
            text-align: left;
            padding: 2px 2px 2px 3px;
            vertical-align: middle;
            border: 1px solid #DADADA;
        }
        
        .CreateHtmlTableFromDataTable-Data-Attachment
        {
            border: 0;
        }
        .CreateHtmlTableFromDataTable-Data-Attachment td
        {
            border: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
        Crew Joining Seniority
    </div>
 <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">
     
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="dvFilter" style="border: 1px solid #cccccc; margin: 2px;">
                    <table width="100%" border="0">
                        <tr>
                            <td align="right" style="width:100px">
                                Fleet
                            </td>
                            <td align="left"  style="width:200px">
                                <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                    Style="width: 200px">
                                </asp:DropDownList>
                            </td>
                            <td align="right"  style="width:100px">
                                Joining Rank
                            </td>
                            <td align="left"  style="width:200px">
                                <asp:DropDownList ID="ddlRank" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                    Style="width: 194px">
                                </asp:DropDownList>
                            </td>
                            <td align="right"  style="width:200px">
                                
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td align="right">
                                Vessel
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"
                                    Style="width: 200px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Joining Month
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"  style="width:90px">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                    <asp:ListItem Value="9">Sept</asp:ListItem>
                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"  style="width:100px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:TextBox ID="txtSearch" runat="server" Style="width: 100px" />
                             </td>
                            <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" width= "100px" CssClass="btnCSS" OnClick="btnSearch_Click" />
                            </td>
                         
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="dvMain" style="border: 1px solid #cccccc; margin: 2px;">
                    <asp:GridView ID="gvSeniorityRecords" DataKeyNames="VoyageID" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" AllowPaging="false" Width="100%" ShowFooter="true"
                        EmptyDataText="No Record Found" CaptionAlign="Bottom" GridLines="None" OnRowDataBound="gvSeniorityRecords_RowDataBound"
                        OnPageIndexChanging="gvSeniorityRecords_PageIndexChanging" ForeColor="#333333"
                        OnRowEditing="gvSeniorityRecords_RowEditing" OnRowUpdating="gvSeniorityRecords_RowUpdating"
                        OnRowCancelingEdit="gvSeniorityRecords_RowCancelingEdit">
                        <Columns>
                            <asp:TemplateField HeaderText="S/N" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnVessel_ID" runat="server" Value='<%# Eval("Vessel_ID")%>' />
                                    <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S/C" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblSC" runat="server" NavigateUrl='<%# "../Crew/CrewDetails.aspx?ID=" + Eval("CrewID").ToString() + "&V=" +  Eval("VoyageID").ToString() %>'
                                        CssClass="staffInfo" Target="_blank" Text='<%# Eval("STAFF_CODE")%>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Staff Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Staff_fullName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Joining Date" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblJoining_Date" runat="server" Text='<%# Eval("Joining_Date","{0:dd/MM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sign-On Date" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSign_On_Date" runat="server" Text='<%# Eval("Sign_On_Date","{0:dd/MM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sign-Off Date" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSign_Off_Date" runat="server" Text='<%# Eval("Sign_Off_Date","{0:dd/MM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Days OnBD" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblDays_OnBD" runat="server" Text='<%# Eval("Days_OnBD")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Seniority Year" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeniorityYear" runat="server" Text='<%# Eval("SeniorityYear")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlSeniorityDays" Text='<%# Bind("SeniorityYear")%>' runat="server">
                                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Seniority Days" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeniorityDays" runat="server" Text='<%# Eval("SeniorityDays")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effective date" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblEffective_date" runat="server" Text='<%# Eval("Effective_date","{0:dd/MM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Seniority Amount" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeniority_Amount" runat="server" Text='<%# Eval("Seniority_Amount")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:ImageButton ID="Save" runat="server" ImageUrl="~/images/accept.png" CausesValidation="True"
                                        CommandName="Update" AlternateText="Update" ValidationGroup="noofdays"></asp:ImageButton>
                                    &nbsp;<asp:ImageButton ID="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                        CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="Edit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                        CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                        <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png" Style="cursor: hand;vertical-align: baseline;"
											Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_DTL_Crew_Seniority&#39;,&#39;Id="+Eval("Id").ToString()+"&#39;,event,this)" %>'
											AlternateText="info" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                        <PagerStyle CssClass="PagerStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                    </asp:GridView>
                    <uc1:ucCustomPager ID="ucCustomPager" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Staff"
                                    AlwaysGetRecordsCount="true" OnBindDataItem="Load_SeniorityRecords" />
                </div>
                
                <div id="Div1" style="border: 1px solid #cccccc; margin: 2px; text-align: right;">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="12px"
                        BackColor="Yellow" Font-Italic="true" Width="400px"></asp:Label></div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
