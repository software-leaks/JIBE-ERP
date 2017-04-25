<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportJobList.aspx.cs" Inherits="Technical_Reports_ReportJobList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Job List Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color: #efefef; border: 1px solid #ffffff; font-family: Tahoma;">
        <div style="background-color: #ffffff; margin: 5px; border: 1px solid #ffffff;">
            <div style="background-color: #ffffff; margin: 5px;">
                <div style="background-color: #efefef; text-align: right; font-size: 11px; padding: 2px;">
                    <asp:Label ID="lblDt" runat="server"></asp:Label></div>
                <div style="background-color: #cccccc; padding: 2px; font-size: 18px;">
                    Job List Report</div>
                <div style="margin-top: 2px; font-size: 12px;">
                    <asp:GridView ID="grdJoblist" runat="server" BackColor="White" BorderColor="#000000"
                        AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        EnableModelValidation="True" AllowSorting="false" Width="100%" GridLines="Both"
                        RowStyle-BorderStyle="Solid" RowStyle-BorderWidth="1px">
                        <AlternatingRowStyle BackColor="#DDeeEE" />
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:Image ID="imgAnyChanges" runat="server" Height="16px" Width="16px" ImageUrl='~/Images/exclamation.gif'
                                        Visible='<%#Eval("MODIFIED").ToString()=="1"?true:false %>' ToolTip="Modified in last 3 days." />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="60px" HeaderText="Vessel" SortExpression="VESSEL_CODE">
                                <ItemTemplate>
                                    <asp:Label ID="lblVesselShortName" runat="server" Text='<%#Eval("VESSEL_SHORT_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="60px" HeaderText="Code" SortExpression="WORKLIST_ID">
                                <ItemTemplate>
                                    <asp:Label ID="lbljobcodegriditem" runat="server" Text='<%#Eval("WORKLIST_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job Description" SortExpression="JOB_DESCRIPTION"
                                ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="jd" runat="server" Text='<%#Eval("JOB_DESCRIPTION").ToString()%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" HeaderText="Assignor" SortExpression="AssignorName">
                                <ItemTemplate>
                                    <asp:Label ID="lblgrdAssignor" runat="server" Text='<%#Eval("AssignorName") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" HeaderText="PIC" SortExpression="PIC">
                                <ItemTemplate>
                                    <asp:Label ID="lblPIC" runat="server" Text='<%#Eval("USER_NAME") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" HeaderText="Date Raised" SortExpression="DATE_RAISED">
                                <ItemTemplate>
                                    <asp:Label ID="lblgrdRaisedDate" runat="server" Text='<%# Eval("DATE_RAISED","{0:d/MM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_RAISED","{0:d/MM/yy}")  %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" HeaderText="Office Dept" SortExpression="INOFFICE_DEPT">
                                <ItemTemplate>
                                    <asp:Label ID="lblgrdofficeDept" runat="server" Text='<%#Eval("INOFFICE_DEPT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" HeaderText="Vessel Dept" SortExpression="ONSHIP_DEPT">
                                <ItemTemplate>
                                    <asp:Label ID="lblgrdVesselDept" runat="server" Text='<%#Eval("ONSHIP_DEPT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" HeaderText="Expected Completion" SortExpression="DATE_ESTMTD_CMPLTN">
                                <ItemTemplate>
                                    <asp:Label ID="lblExptCompl" runat="server" Text='<%# Eval("DATE_ESTMTD_CMPLTN","{0:d/MM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_ESTMTD_CMPLTN","{0:d/MM/yy}") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" HeaderText="Completed" SortExpression="DATE_COMPLETED">
                                <ItemTemplate>
                                    <asp:Label ID="lblgrdCompletedOn" runat="server" Text='<%# Eval("DATE_COMPLETED","{0:d/MM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_COMPLETED","{0:d/MM/yy}") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" HeaderText="NCR" SortExpression="NCR">
                                <ItemTemplate>
                                    <asp:Label ID="lblgrdNCR" runat="server" Text='<%#Eval("NCR").ToString()=="-1"?Eval("NCR_NUM").ToString()+ "/" + Eval("NCR_YEAR").ToString():"NO" %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <label id="Label1" runat="server">
                                No jobs found !!</label>
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="#aabbdd" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
