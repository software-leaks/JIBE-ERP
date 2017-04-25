<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_CrewQueries.aspx.cs"
    Inherits="Crew_CrewDetails_CrewQueries" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="jQUpload/Scripts/jquery-1.2.6.min.js"></script>
    <script type="text/javascript" src="jQUpload/Scripts/jquery.ajax_upload.0.6.min.js"></script>
      <script type="text/javascript" src="../Scripts/Common_Functions.js"></script>
    <script type="text/javascript" src="../Scripts/CrewSailingInfo.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel_Grid" runat="server">
        <ContentTemplate>
            <div id="dvCrewQueries" style="margin-top: 2px;">
                <asp:HiddenField ID="HiddenField_CrewID" runat="server" />
                <table style="width: 100%;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                        </td>
                        <td style="width: 120px; text-align: right;">
                            
                        </td>
                        <td style="width: 20px; text-align: right;">
                            <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Images/reload.png"
                                OnClientClick="GetCrewQueries($('[id$=HiddenField_CrewID]').val());return false;" />
                        </td>
                    </tr>
                </table>
                <div class="error-message" onclick="javascript:this.style.display='none';">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <asp:GridView ID="GridView_CrewQueries" runat="server" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="0" Width="100%" EmptyDataText="No Record Found"
                    CaptionAlign="Bottom" GridLines="None" DataKeyNames="ID" AllowPaging="false"
                    AllowSorting="true" OnSorting="GridView_CrewQueries_Sorting" CssClass="GridView-css">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Raised" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblDate_Raised" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation"))) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblVessel_Short_Name" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <%--<asp:TemplateField HeaderText="Staff Code" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                                    Target="_blank" Text='<%# Eval("STAFF_CODE")%>' CssClass="pin-it"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle Width="60px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:HyperLink ID="lblSTAFF_NAME" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                                    Target="_blank" Text='<%# Eval("Staff_FullName")%>' ForeColor="Black"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'
                                    crewid='<%# Eval("CrewID")%>' CssClass="sailingInfo"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblQueryType" runat="server" Text='<%# Eval("Query_Type")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Details" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="lblQuery_Detail" runat="server" NavigateUrl='<%# "/"+System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString()+"/crewquery/CrewQuery_Details.aspx?ID=" + Eval("ID").ToString() + "&VID=" + Eval("Vessel_ID").ToString()+ "&CrewID=" + Eval("CrewID").ToString() %>'
                                    Target="_blank" Text='<%# Eval("Query_Detail")%>' CssClass="pin-it"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle Width="350px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblQueryStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" Font-Size="10px" HorizontalAlign="Center" />
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
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
