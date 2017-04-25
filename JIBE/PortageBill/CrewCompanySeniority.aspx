<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewCompanySeniority.aspx.cs"
    Inherits="PortageBill_CrewCompanySeniority" MasterPageFile="~/Site.master" Title="Crew Seniority" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .HeaderStyle-css-center
        {
            text-align: center;
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
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="dvFilter" style="border: 1px solid #cccccc; margin: 2px;">
                    <table width="100%" border="0">
                        <tr>
                            <td align="left" style="width: 70px">
                                Joining Rank
                            </td>
                            <td align="left" style="width: 200px">
                                <asp:DropDownList ID="ddlRank" runat="server" Style="width: 194px">
                                </asp:DropDownList>
                            </td>
                            <td align="left" style="width: 70px">
                                Status
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStaus" runat="server" Width="156px">
                                    <asp:ListItem Value="">ALL</asp:ListItem>
                                    <asp:ListItem Value="CANDIDATE">Candidate</asp:ListItem>
                                    <asp:ListItem Value="STAND BY">Stand By</asp:ListItem>
                                    <asp:ListItem Value="ONBOARD" Selected="True">Onboard</asp:ListItem>
                                    <asp:ListItem Value="ON-LEAVE">On-Leave</asp:ListItem>
                                    <asp:ListItem Value="INACTIVE">Inactive</asp:ListItem>
                                    <asp:ListItem Value="PLANNED">Planned</asp:ListItem>
                                    <asp:ListItem Value="NTBR">NTBR</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" style="width: 100px">
                                Seniority Year
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCompanySeniorityFilter" runat="server" Width="156px">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">> 1</asp:ListItem>
                                    <asp:ListItem Value="5">> 5</asp:ListItem>
                                    <asp:ListItem Value="10">> 10</asp:ListItem>
                                    <asp:ListItem Value="20">> 20</asp:ListItem>
                                    <asp:ListItem Value="30">> 30</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Search:
                            </td>
                            <td>
                                <asp:TextBox ID="txtSearchText" runat="server" Width="110px"></asp:TextBox>
                            </td>
                            <td align="left" style="width: 100px">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" CssClass="btnCSS"
                                    OnClick="btnSearch_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="dvMain" style="border: 1px solid #cccccc; margin: 2px;">
                    <div class="error-message" onclick="javascript:this.style.display='none';">
                        <asp:Label ID="lblmsg" runat="server" BackColor="Yellow"></asp:Label>
                    </div>
                    <asp:GridView ID="gvSeniorityRecords" DataKeyNames="ID,CompanySeniorityYear,CompanySeniorityDays,RankSeniorityYear,RankSeniorityDays"
                        runat="server" AutoGenerateColumns="False" CellPadding="4" AllowPaging="false"
                        Width="100%" ShowFooter="true" OnRowCreated="GridView1_RowCreated" EmptyDataText="No Record Found"
                        CaptionAlign="Bottom" GridLines="None" OnRowDataBound="gvSeniorityRecords_RowDataBound"
                        OnPageIndexChanging="gvSeniorityRecords_PageIndexChanging" ForeColor="#333333"
                        OnRowEditing="gvSeniorityRecords_RowEditing" OnRowUpdating="gvSeniorityRecords_RowUpdating"
                        OnRowCancelingEdit="gvSeniorityRecords_RowCancelingEdit" CssClass="GridView-css">
                        <Columns>
                            <asp:TemplateField HeaderText="S/C" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblSC" runat="server" NavigateUrl='<%# "../Crew/CrewDetails.aspx?ID=" + Eval("CrewID").ToString() + "&V=" +  Eval("VoyageID").ToString() %>'
                                        CssClass="staffInfo" Target="_blank" Text='<%# Eval("STAFF_CODE")%>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Staff Name" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Staff_fullName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Current Rank" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Name")%>'></asp:Label>
                                    <asp:Label ID="lblRankID" runat="server" Text='<%# Eval("RankID")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblSeconityRankID" runat="server" Text='<%# Eval("RankSeniorityID")%>'
                                        Visible="false"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnVessel_ID" runat="server" Value='<%# Eval("Vessel_ID")%>' />
                                    <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Seniority Year" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanySeniorityYear" runat="server" Text='<%# Eval("CompanySeniorityYear")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCompanySeniorityYear" runat="server" Text='<%# Bind("CompanySeniorityYear")%>'
                                        Enabled='<%#  (Eval("CompanySeniorityYear").ToString() != "" || Eval("CompanySeniorityDays").ToString() != "" ) ? false : true %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Seniority Days" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanySeniorityDays" runat="server" Text='<%# Eval("CompanySeniorityDays")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCompanySeniorityDays" runat="server" Text='<%# Bind("CompanySeniorityDays")%>'
                                        Enabled='<%#  (Eval("CompanySeniorityYear").ToString() != "" || Eval("CompanySeniorityDays").ToString() != "" ) ? false : true %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company Effective Date" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyEffectiveDate" runat="server" Text='<%# Eval("CompanyEffective_date")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCompanyEffectiveDate" CssClass="txtCompanyEffectiveDate" runat="server"
                                        Text='<%# Bind("CompanyEffective_date")%>' Enabled='<%#  (Eval("CompanySeniorityYear").ToString() != "" || Eval("CompanySeniorityDays").ToString() != "" ) ? false : true %>'
                                        Width="80px"></asp:TextBox>
                                    <tlk4:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCompanyEffectiveDate"
                                        Format="dd/MM/yyyy">
                                    </tlk4:CalendarExtender>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Seniority Rank" SortExpression="Rank_Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblRank_Name" runat="server" Text='<%# Bind("SeniorityRankName")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlSeniorityRank" runat="server" DataSourceID="DataSource_Rank"
                                        DataTextField="Rank_Short_Name" DataValueField="id" Enabled='<%#  (Eval("RankSeniorityYear").ToString() != "" || Eval("RankSeniorityDays").ToString() != "" ) ? false : true %>'>
                                        <asp:ListItem Selected="True" Value="0">-- SELECT --</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="DataSource_Rank" runat="server" SelectMethod="Get_RankList"
                                        TypeName="SMS.Business.Crew.BLL_Crew_Admin"></asp:ObjectDataSource>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank Seniority Year" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRankSeniorityYear" runat="server" Text='<%# Eval("RankSeniorityYear")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRankSeniorityYear" runat="server" Text='<%# Bind("RankSeniorityYear")%>'
                                        Enabled='<%#  (Eval("RankSeniorityYear").ToString() != "" || Eval("RankSeniorityDays").ToString() != "" ) ? false : true %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank Seniority Days" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRankSeniorityDays" runat="server" Text='<%# Eval("RankSeniorityDays")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRankSeniorityDays" runat="server" Text='<%# Bind("RankSeniorityDays")%>'
                                        Enabled='<%#  (Eval("RankSeniorityYear").ToString() != "" || Eval("RankSeniorityDays").ToString() != "" ) ? false : true %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank Effective Date" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRankEffectiveDate" runat="server" Text='<%# Eval("RankEffective_date")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRankEffectiveDate" CssClass="txtRankEffectiveDate" runat="server"
                                        Text='<%# Bind("RankEffective_date")%>' Enabled='<%#  (Eval("RankSeniorityYear").ToString() != "" || Eval("RankSeniorityDays").ToString() != "" ) ? false : true %>'
                                        Width="80px"></asp:TextBox>
                                    <tlk4:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtRankEffectiveDate"
                                        Format="dd/MM/yyyy">
                                    </tlk4:CalendarExtender>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:ImageButton ID="Save" ClientIDMode="Static" runat="server" ImageUrl="~/images/accept.png"
                                        CausesValidation="True" CommandName="Update" AlternateText="Update"></asp:ImageButton>
                                    &nbsp;<asp:ImageButton ID="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                        CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="Edit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                        CommandName="Edit" AlternateText="Edit" Visible='<%# ( (Eval("CompanySeniorityYear").ToString() == "" && Eval("CompanySeniorityDays").ToString() == "" ) || Eval("RankSeniorityYear").ToString() == "" || Eval("RankSeniorityDays").ToString() == "" )? true : false %>'>
                                    </asp:ImageButton>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", "#Save", function () {
                if ($.trim($(".txtRankEffectiveDate").val()) != "") {
                    if (IsInvalidDate($.trim($(".txtRankEffectiveDate").val()), '<%=UDFLib.GetDateFormat()%>')) {
                        {
                            alert("Enter valid Rank Effective Date<%=UDFLib.DateFormatMessage() %>");
                            $(".txtRankEffectiveDate").focus();
                            return false;
                        }
                    }
                }

                if ($.trim($(".txtCompanyEffectiveDate").val()) != "") {
                    if (IsInvalidDate($.trim($(".txtCompanyEffectiveDate").val()), '<%=UDFLib.GetDateFormat()%>')) {
                        {
                            alert("Enter valid Company Effective Date<%=UDFLib.DateFormatMessage() %>");
                            $(".txtCompanyEffectiveDate").focus();
                            return false;
                        }
                    }
                }
            });
        });
    </script>
</asp:Content>
