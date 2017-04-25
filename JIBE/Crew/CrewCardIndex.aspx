<%@ Page Title="Crew Card Index" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CrewCardIndex.aspx.cs" Inherits="Crew_CrewCardIndex" %>
    <%@ Register Src="~/UserControl/ucCustomPager.ascx"TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
    <style type="text/css">
        .YELLOWCARD_PROPOSED
        {
            background-color: Yellow;
            color: Black;
            font-weight: bold;
        }
        .YELLOWCARD_ISSUED
        {
            background-color: Yellow;
            color: Red;
            font-weight: bold;
        }
        .REDCARD_PROPOSED
        {
            background-color: Red;
            color: White;
            font-weight: bold;
        }
        .REDCARD_ISSUED
        {
            background-color: Red;
            color: Yellow;
            font-weight: bold;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function showDiv(dv, id) {
            if (id)
            { document.getElementById("frmCrewCard").src = 'ProposeCrewCard.aspx?CrewID=' + id; }

            document.getElementById(dv).style.display = "block";
        }
        function closeDiv(dv) {
            document.getElementById(dv).style.display = "None";
        }
        $(function () {
            $('.draggable').draggable();
        });
        $(document).ready(function () {
            $('#frmCrewCard').load(function () {
                this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px';
            });
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

    <div class="page-title">
        <div>
            Crew Yellow Card/Red Card Index</div>
    </div>
    <div class="page-content-main">
        <div style="border: 1px solid #cccccc; padding: 5px;margin-bottom:2px;">
            <asp:UpdatePanel ID="Update_Filter" runat="server" UpdateMode="Conditional">
              <Triggers>
                    <asp:PostBackTrigger ControlID="btnExport" />                   
             </Triggers>
                <ContentTemplate>
                    <table style="color: Black;">
                        <tr>
                            <td style="width: 60px;">
                                Fleet
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Load_CrewCards">
                                </asp:DropDownList>
                            </td>
                            <td style="padding-left: 20px">
                                Vessel
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Load_CrewCards">
                                </asp:DropDownList>
                            </td>
                            <td>
                                Nationality
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCountry" runat="server" Width="156px"  AutoPostBack="true"
                                    onselectedindexchanged="ddlCountry_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="padding-left: 50px">
                                Approval Status
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Load_CrewCards">
                                    <asp:ListItem Text="-SELECT ALL-" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Proposed" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Issued" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Rejected" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Search
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearchText" runat="server" AutoPostBack="true"
                                    OnTextChanged="Load_CrewCards"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server"  Text="Search" CssClass="btnCSS" OnClick="Load_CrewCards">
                                </asp:Button>
                            </td>
                            <td style="text-align: right">
                                    <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" Text="Clear Filter"
                                        CssClass="btnCSS" />
                                </td>
                                 <td align="left">
                                        <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btncss" 
                                            OnClick="btnExport_Click" />
                                 </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
       
        <asp:UpdatePanel ID="Update_Grid" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3"
                    Width="100%" CellSpacing="1" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                    GridLines="None" DataKeyNames="ID" AllowPaging="True" AllowSorting="false" PageSize="30"
                    CssClass="GridView-css" OnPageIndexChanging="GridView1_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Vessel">
                            <ItemTemplate>
                                <asp:Label ID="lblONBD" runat="server" Text='<%# Eval("Vessel_Short_Name")%>' class='vesselinfo'
                                    vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Staff Code" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CrewID")%>'                                  
                                    Target="_blank" Text='<%# Eval("STAFF_CODE")%>' CssClass="staffInfo pin-it"></asp:HyperLink>
                                <asp:Label ID="lblX" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="60px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:HyperLink ID="lblSTAFF_NAME" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                                    Target="_blank" Text='<%# Eval("Staff_FullName")%>' ForeColor="Black"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_short_Name")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nation" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSTAFF_NATIONALITY" runat="server" Text='<%# Eval("ISO_Code")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Proposed By" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblProposedBy" runat="server" Text='<%# Eval("ProposedBy")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Propose Date" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblDate_Of_Creation" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation"))) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Proposed Remarks" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblProposedRemarks" runat="server" Text='<%# Eval("ProposedRemarks")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Size="10px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <div class='<%# Eval("CardType").ToString().Replace(" ","") + "_" +  Eval("ApprovalStatus").ToString()%>'
                                    onclick="showDiv('dvProposeYellowCard',<%# Eval("CrewID")%>);return false;">
                                    <asp:Label ID="lblCardStatus" runat="server" Text='<%# Eval("CardType").ToString() + " " +  Eval("ApprovalStatus").ToString()%>'></asp:Label>
                                </div>
                                <%--<asp:Label ID="lblApprovalStatus" runat="server" Text='<%# Eval("CardType").ToString() + " " +  Eval("ApprovalStatus").ToString()%>' CssClass='<%# Eval("CardType").ToString().Replace(" ","") + "_" +  Eval("ApprovalStatus").ToString()%>'></asp:Label>--%>
                            </ItemTemplate>
                            <ItemStyle Width="150px" Font-Size="10px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="HeaderStyle-css" />
                    <PagerStyle CssClass="PagerStyle-css" />
                    <RowStyle CssClass="RowStyle-css" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                </asp:GridView>
               <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindCrewCardIndex" />
               <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" /> 
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvProposeYellowCard" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 40%; top: 15%; width: 600px; z-index: 1; color: black">
        <div class="header">
            <div style="right: 0px; position: absolute; cursor: pointer;">
                <img src="../Images/Close.gif" onclick="closeDiv('dvProposeYellowCard');" alt="Close" />
            </div>
            <h4>
                Propose Yellow/Red Card</h4>
        </div>
        <div class="content">
            <iframe id="frmCrewCard" src="" frameborder="0" height="450px" width="100%"></iframe>
        </div>
    </div>
</asp:Content>
