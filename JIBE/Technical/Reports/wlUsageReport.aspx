<%@ Page Title="Worklist Usage Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="wlUsageReport.aspx.cs" Inherits="Technical_Reports_wlUsageReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .SelectedRow
        {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-header" class="page-title">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            <b>Worklist Usage Report</b>
                        </td>
                    </tr>
                </table> 
    </div>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div style="text-align: center">
                <div style="height: 630px; overflow: auto; text-align: left;">
                    <table style="padding: 2px;" cellpadding="0" cellspacing="0" width="99%">
                        <tr style="background-color: #aabbdd; color: Black; font-weight: bold; border: 1px solid #aabbdd;">
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%">
                                            User Login Count
                                        </td>
                                        <td>
                                            From Date:<asp:TextBox ID="txtStartDate" runat="server" Width="100px" AutoPostBack="true"
                                                OnTextChanged="txtStartDate_TextChanged"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtStartDate"
                                                Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td>
                                            To Date:<asp:TextBox ID="txtEndDate" runat="server" Width="100px" 
                                                AutoPostBack="true" ontextchanged="txtEndDate_TextChanged"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calTo" runat="server" TargetControlID="txtEndDate"
                                                Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #aabbdd;">
                                <asp:GridView ID="GridViewUserSession" runat="server" DataSourceID="ObjectDataSource4"
                                    CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                    <EmptyDataTemplate>
                                        No logins found for this period !!
                                    </EmptyDataTemplate>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Left" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <%--<asp:GridView ID="GridViewUserSession" runat="server" DataSourceID="ObjectDataSource4"
                                    Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
                                    AllowSorting="true">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="User Name" SortExpression="UserName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Login Count" SortExpression="Count" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSessionCount" runat="server" Text='<%#Eval("Count") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="No record found !!"></asp:Label>
                                    </EmptyDataTemplate>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>--%>
                                <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="Get_UserSessionLog"
                                    TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="SessionUserID" SessionField="userid" Type="Int32" />
                                        <asp:ControlParameter ControlID="txtStartDate" Name="StartDate" PropertyName="Text"
                                            Type="String" />
                                        <asp:ControlParameter ControlID="txtEndDate" Name="EndDate" PropertyName="Text" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="background-color: #aabbdd; color: Black; font-weight: bold; border: 1px solid #aabbdd;">
                            <td>
                                Number of jobs completed
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #aabbdd;">
                                <asp:GridView ID="GridViewJobsCompleted" runat="server" DataSourceID="ObjectDataSource_JobsCompleted"
                                    Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCreated="GridViewJobsCompleted_RowCreated">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                    <EmptyDataTemplate>
                                        No job is marked as complete during the selected period !!
                                    </EmptyDataTemplate>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <%--
                                <asp:GridView ID="GridView_PrevWk1" runat="server" 
                                    Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                    
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                </td>
                                <td>
                                <asp:GridView ID="GridView_PrevWk2" runat="server"
                                    Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                    
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                --%>
                                <asp:ObjectDataSource ID="ObjectDataSource_JobsCompleted" runat="server" SelectMethod="Get_CompletedJobs"
                                    TypeName="SMS.Business.Technical.BLL_Tec_Worklist">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtStartDate" Name="StartDate" PropertyName="Text"
                                            Type="String" />
                                        <asp:ControlParameter ControlID="txtEndDate" Name="EndDate" PropertyName="Text" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <%--<table style="background-color:Yellow;width:100%">
                                    <tr>
                                        <td>
                                            Total jobs completed in period
                                        </td>
                                        <td>
                                            (<asp:Label ID="lblPrevWk1St" runat="server"></asp:Label>
                                            -
                                            <asp:Label ID="lblPrevWk1End" runat="server"></asp:Label>)
                                            =
                                            <asp:Label ID="lblPrevWk1Total" runat="server"></asp:Label>
                                        </td>
                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                        <td>
                                            Total jobs completed in period
                                        </td>
                                        <td>
                                            (<asp:Label ID="lblPrevWk2St" runat="server"></asp:Label>
                                            -
                                            <asp:Label ID="lblPrevWk2End" runat="server"></asp:Label>)
                                            =
                                            <asp:Label ID="lblPrevWk2Total" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="background-color: #aabbdd; color: Black; font-weight: bold; border: 1px solid #aabbdd;">
                            <td>
                                Number of jobs presently incomplete for each Vessel department-wise
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #aabbdd;">
                                <asp:GridView ID="GridViewJobsIncomplete" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                                    CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCreated="GridViewJobsIncomplete_RowCreated">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get_IncompleteJobs"
                                    TypeName="SMS.Business.Technical.BLL_Tec_Worklist"></asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="background-color: #aabbdd; color: Black; font-weight: bold; border: 1px solid #aabbdd;">
                            <td>
                                Number of jobs presently incomplete for each Vessel department-wise, with NO followup
                                from Office
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #aabbdd;">
                                <asp:GridView ID="GridViewJobsNoFollowup" runat="server" DataSourceID="ObjectDataSource3" Width="100%"
                                    CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCreated="GridViewJobsNoFollowup_RowCreated">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="Get_IncompleteJobs_NoFollowUp"
                                    TypeName="SMS.Business.Technical.BLL_Tec_Worklist"></asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="background-color: #aabbdd; color: Black; font-weight: bold;">
                            <td>
                                Number of followups done by each user in last 30 days
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #aabbdd;">
                                <asp:GridView ID="GridViewJobsWithFollowup" runat="server" DataSourceID="ObjectDataSource2" Width="100%"
                                    CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCreated="GridViewJobsWithFollowup_RowCreated">
                                    
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" HorizontalAlign="Center"/>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="Get_IncompleteJobs_WithFollowUp"
                                    TypeName="SMS.Business.Technical.BLL_Tec_Worklist"></asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
