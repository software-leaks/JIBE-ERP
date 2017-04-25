<%@ Page Title="Crew Missing Data Report" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CrewMissingData.aspx.cs" Inherits="Crew_CrewMissingData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
    <div class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 33%;">
                </td>
                <td style="width: 33%; text-align: center; font-weight: bold;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Missing Data Report"></asp:Label>
                </td>
                <td style="width: 33%; text-align: right;">
                </td>
            </tr>
        </table>
    </div>
    <div class="page-content-main">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div id="dvMain" style="border: 1px solid #cccccc; margin: 2px;">
            <table cellspacing="5" style="width: 100%">
                <tr>
                    <td class="th">
                        <b>JOINING DATE updated but SIGN-ON DATE is not updated</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" CellPadding="4" CellSpacing="1" GridLines="None"
                            Width="100%" AutoGenerateColumns="False" CssClass="GridView-css" OnRowDataBound="GridView1_RowDataBound" >
                            <Columns>
                                <asp:TemplateField HeaderText="Vessel">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Short_Name")%>' class='vesselinfo'
                                            vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="St/Code">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CrewID")%>' CssClass="staffInfo"
                                            Target="_blank" Text='<%# Eval("STAFF_CODE")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Staff_Code") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Joining_Date" HeaderText="Joining Date" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Staff_Surname" HeaderText="Staff Surname" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Staff_Name" HeaderText="Staff Name" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Rank_Short_Name" HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="VoyageID" HeaderText="Voyage ID" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Sign_On_Date" HeaderText="Sign-On Date" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Sign_Off_Date" HeaderText="Sign-Off Date" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
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
                    </td>
                </tr>
                <tr>
                    <td class="th">
                        <b>JOINING DATE updated but SALARY not updated</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridView2" runat="server" CellPadding="4" CellSpacing="1" GridLines="None"
                            Width="100%" AutoGenerateColumns="False" CssClass="GridView-css" OnRowDataBound="GridView2_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Vessel_Short_Name" HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="St/Code" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CrewID")%>' CssClass="staffInfo"
                                            Target="_blank" Text='<%# Eval("STAFF_CODE")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Staff_Code") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Staff_Surname" HeaderText="Staff Surname" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Staff_Name" HeaderText="Staff Name" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Rank_Short_Name" HeaderText="Rank" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Joining_Date" HeaderText="Joining Date" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="ManningOffice" HeaderText="Manning Office" HeaderStyle-HorizontalAlign="Left" />
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
                    </td>
                </tr>
                <tr>
                    <td class="th">
                        <b>CREW JOINED, BUT PARTICULARS NOT UPDATED - SUMMARY</b> &nbsp;&nbsp;&nbsp;&nbsp;
                        (<asp:HyperLink ID="lnkViewDetails" runat="server" NavigateUrl="CrewMissingDataDetails.aspx?VM=1"
                            Text="View Details" Target="_blank"></asp:HyperLink>)
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridView3" runat="server" CellPadding="4" CellSpacing="1" GridLines="None"
                            ShowFooter="true" Width="100%" AutoGenerateColumns="False" OnRowDataBound="GridView3_RowDataBound"  CssClass="GridView-css">
                            <Columns>
                                <%--<asp:BoundField DataField="Company_Name" HeaderText="Manning Office" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Manning Office">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Col0" runat="server" NavigateUrl='<%# "CrewMissingDataDetails.aspx?C=&MOID=" + Eval("ManningOfficeID")%>'
                                            Target="_blank" Text='<%# Eval("Company_Name") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Total0" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OnBd Count">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Col1" runat="server" NavigateUrl='<%# "CrewMissingDataDetails.aspx?C=&MOID=" + Eval("ManningOfficeID")%>'
                                            Target="_blank" Text='<%# Eval("OnBD").ToString()=="0"?"":Eval("OnBD").ToString() %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Total1" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Particulars">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Col2" runat="server" NavigateUrl='<%# "CrewMissingDataDetails.aspx?C=PART&&MOID=" + Eval("ManningOfficeID")%>'
                                            Target="_blank" Text='<%# Eval("Particulars").ToString()=="0"?"":Eval("Particulars").ToString() %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Total2" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Passport">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Col3" runat="server" NavigateUrl='<%# "CrewMissingDataDetails.aspx?C=PASS&MOID=" + Eval("ManningOfficeID")%>'
                                            Target="_blank" Text='<%# Eval("PP").ToString()=="0"?"":Eval("PP").ToString() %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Total3" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Seaman Book">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Col4" runat="server" NavigateUrl='<%# "CrewMissingDataDetails.aspx?C=SEBK&MOID=" + Eval("ManningOfficeID")%>'
                                            Target="_blank" Text='<%# Eval("[S/B]").ToString()=="0"?"":Eval("[S/B]").ToString() %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Total4" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Photo">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Col5" runat="server" NavigateUrl='<%# "CrewMissingDataDetails.aspx?C=PHOT&MOID=" + Eval("ManningOfficeID")%>'
                                            Target="_blank" Text='<%# Eval("Photo").ToString()=="0"?"":Eval("Photo").ToString() %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Total5" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pre-Joining">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Col6" runat="server" NavigateUrl='<%# "CrewMissingDataDetails.aspx?C=PREJ&MOID=" + Eval("ManningOfficeID")%>'
                                            Target="_blank" Text='<%# Eval("PreJoining").ToString()=="0"?"":Eval("PreJoining").ToString() %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Total6" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank ACC">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Col7" runat="server" NavigateUrl='<%# "CrewMissingDataDetails.aspx?C=BANK&MOID=" + Eval("ManningOfficeID")%>'
                                            Target="_blank" Text='<%# Eval("[BankA/C]").ToString()=="0"?"":Eval("[BankA/C]").ToString() %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Total7" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NOK Details">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Col8" runat="server" NavigateUrl='<%# "CrewMissingDataDetails.aspx?C=NOKD&MOID=" + Eval("ManningOfficeID")%>'
                                            Target="_blank" Text='<%# Eval("NOK").ToString()=="0"?"":Eval("NOK").ToString() %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Total8" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Uniform">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Col9" runat="server" NavigateUrl='<%# "CrewMissingDataDetails.aspx?C=UNIF&MOID=" + Eval("ManningOfficeID")%>'
                                            Target="_blank" Text='<%# Eval("uniform").ToString()=="0"?"":Eval("uniform").ToString() %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Total9" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Height/Weight/Waist">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Col10" runat="server" NavigateUrl='<%# "CrewMissingDataDetails.aspx?C=HWWD&MOID=" + Eval("ManningOfficeID")%>'
                                            Target="_blank" Text='<%# Eval("[H/W/WST]").ToString()=="0"?"":Eval("[H/W/WST]").ToString() %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Total10" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voyage Document">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Col11" runat="server" NavigateUrl='<%# "CrewMissingDataDetails.aspx?C=VOYD&MOID=" + Eval("ManningOfficeID")%>'
                                            Target="_blank" Text='<%# Eval("VoyageDocMissing").ToString()=="0"?"":Eval("VoyageDocMissing").ToString() %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Total11" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Perpetual Document">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="Col12" runat="server" NavigateUrl='<%# "CrewMissingDataDetails.aspx?C=PREP&MOID=" + Eval("ManningOfficeID")%>'
                                            Target="_blank" Text='<%# Eval("PerpetualDocMissing").ToString()=="0"?"":Eval("PerpetualDocMissing").ToString() %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Total12" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="HeaderStyle-css" />
                            <PagerStyle CssClass="PagerStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                            <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
