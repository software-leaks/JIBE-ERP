<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_MedHistory.aspx.cs"
    Inherits="Crew_CrewDetails_MedHistory" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
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
            <div id="dvMedicalHistory" style="margin-top: 2px;">
                <asp:HiddenField ID="HiddenField_CrewID" runat="server" />
                <table style="width: 100%;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                        </td>
                        <td style="width: 120px; text-align: right;">
                            <asp:ImageButton runat="server" ID="imgAdd_MedHistory" ImageUrl="~/Images/AddMedHistory.png"
                                OnClientClick="Add_MedHistory_Details($('[id$=HiddenField_CrewID]').val());return false;" />
                        </td>
                        <td style="width: 30px; text-align: right;">
                            <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Images/reload.png"
                                OnClientClick="Get_Crew_MedicalHistory($('[id$=HiddenField_CrewID]').val());return false;" />
                        </td>
                    </tr>
                </table>
                <div class="error-message" onclick="javascript:this.style.display='none';">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <asp:Panel ID="pnlMedicalHistoryIndex" runat="server">
                    <asp:GridView ID="GridView_MedicalHistory" runat="server" AutoGenerateColumns="False"
                        CellPadding="3" CellSpacing="0" Width="100%" EmptyDataText="No Record Found"
                        CaptionAlign="Bottom" GridLines="None" DataKeyNames="ID" AllowPaging="false"
                        AllowSorting="true" OnSorting="GridView_MedicalHistory_Sorting" CssClass="GridView-css">
                        <Columns>
                           <%-- <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Date Raised" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate_Raised" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("case_Date"))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Date " HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblToDate" runat="server"  Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("CASE_TO_DATE"))) %>' ></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblVessel_Short_Name" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'
                                        crewid='<%# Eval("CrewID")%>' CssClass="sailingInfo"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblQueryType" runat="server" Text='<%# Eval("Case_Type")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Details" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblQuery_Detail" runat="server" onclick='<%# "Show_MedHistory_Details(" + Eval("CrewID").ToString() + ","+ Eval("ID").ToString()+ ","+ Eval("VESSEL_ID").ToString()+ ","+ Eval("Office_ID").ToString() +")" %>'
                                        Target="_blank" Text='<%# Eval("Case_Detail")%>' ForeColor="Blue"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblQueryStatus" runat="server" Text='<%# Eval("CASE_STATUS")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" Font-Size="10px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                        Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_DTL_CREW_MED_HISTORY&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>'
                                        AlternateText="info" />
                                </ItemTemplate>
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
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
