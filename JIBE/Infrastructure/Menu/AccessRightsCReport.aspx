<%@ Page Title=" Access Rights Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="AccessRightsCReport.aspx.cs" Inherits="Infrastructure_Menu_AccessRightsCReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function getOperatingSystem() {
            var OSName = "";
            var Browser = "";

            if (navigator.appVersion.indexOf("Win") != -1) OSName = "Windows";
            if (navigator.appVersion.indexOf("Mac") != -1) OSName = "MacOS";
            if (navigator.appVersion.indexOf("X11") != -1) OSName = "UNIX";
            if (navigator.appVersion.indexOf("Linux") != -1) OSName = "Linux";

            if (OSName == "MacOS") {
                document.getElementById($('[id$=btnExport]').attr('id')).style.display = "none";

            }

            if (OSName == "Windows") {

                document.getElementById($('[id$=btnMacExport]').attr('id')).style.display = "none";

            }

        }
    </script>
   <style type="text/css">
       .page
       {
           width:100%;
       }
       
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="page-title">
        Access Rights Report
    </div>
    <div class="page-content" style="font-family: Tahoma; font-size: 12px">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnRetrieve">
            <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div style="color: Black;  padding: 5px;">
                        <table cellpadding="2" cellspacing="3" width="100%" style="color: Black; ">
                            <tr>
                                <td align="left" style="width:7%">
                                    <asp:Label ID="lblCompany" runat="server" Text="Company :"></asp:Label>
                                </td>
                                <td align="left" style="width:15%">
                                    <CustomFilter:ucfDropdown ID="DDLCompany" runat="server" Height="200" UseInHeader="false"
                                        Width="160" OnApplySearch="BindDepartment_UserList"/>
                                </td>
                                <td align="left" style="width:7%">
                                    <asp:Label ID="lblMenu" runat="server" Text="Menu :"></asp:Label>
                                </td>
                                <td align="left" style="width:15%">
                                    <CustomFilter:ucfDropdown ID="DDLMenu" runat="server" Height="200" UseInHeader="false"
                                        Width="160" OnApplySearch="BindSubMenu_PageList" />
                                </td>
                                <td  align="right">
                                <table >
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnRetrieve" runat="server" Height="24px" 
                                                ImageAlign="AbsBottom" ImageUrl="~/Images/SearchButton.png" 
                                                onclick="btnRetrieve_Click" ToolTip="Search" />
                                        
                                            <asp:ImageButton ID="btnClearFilter" runat="server" Height="23px" 
                                                ImageUrl="~/Images/filter-delete-icon.png" onclick="btnClearFilter_Click" 
                                                ToolTip="Clear Filter" />
                                        
                                            <asp:ImageButton ID="btnExport" runat="server" CommandArgument="ExportFrom_IE" 
                                                Height="25px" ImageUrl="~/Images/XLS.jpg" OnClick="btnExport_Click" 
                                                ToolTip="Export to Excel" />
                                       
                                            <asp:ImageButton ID="btnMacExport" runat="server" 
                                                CommandArgument="ExportFrom_MAC" Height="25px" 
                                                ImageUrl="~/Images/Export-mac.png" OnClick="btnExport_Click" 
                                                Style="display: none;" ToolTip="Export to Excel unformatted" />
                                        </td>
                                    </tr>
                                </table>
                                </td>

                            </tr>
                            <tr>
                                <td align="left" style="width:7%">
                                    <asp:Label ID="lblDepartment" runat="server" Text="Department :"></asp:Label>
                                </td>
                                <td align="left" style="width:15%">
                                    <CustomFilter:ucfDropdown ID="DDLDepartment" runat="server" Height="200" UseInHeader="false"
                                        Width="160" OnApplySearch="INF_Get_UserList"/>
                                </td>
                                <td align="left" style="width:7%">
                                    <asp:Label ID="lblSubMenu" runat="server" Text="Sub-Menu :"></asp:Label>
                                </td>
                                <td align="left" style="width:15%">
                                    <CustomFilter:ucfDropdown ID="DDLSubMenu" runat="server" Height="200" UseInHeader="false"
                                        Width="160" OnApplySearch="INF_Get_PageListBySubMenu"/>
                                </td>
                                
                            </tr>
                            <tr>
                                <td align="left" style="width:7%">
                                    <asp:Label ID="lblUser" runat="server" Text="User :"></asp:Label>
                                </td>
                                <td align="left" style="width:15%">
                                    <CustomFilter:ucfDropdown ID="DDLUser" runat="server" Height="200" UseInHeader="false"
                                        Width="160" />
                                </td>
                                <td align="left" style="width:7%">
                                    <asp:Label ID="lblPage" runat="server" Text="Page :"></asp:Label>
                                </td>
                                <td align="left" style="width:15%">
                                    <CustomFilter:ucfDropdown ID="DDLPageName" runat="server" Height="200" UseInHeader="false"
                                        Width="160" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="width: 100%">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div>
                            <asp:GridView ID="gvAccessRightsReport" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" CellPadding="2" ShowHeaderWhenEmpty="true" Width="100%"
                                AllowSorting="true" CssClass="gridmain-css" GridLines="None" 
                                onrowdatabound="gvAccessRightsReport_RowDataBound" 
                                onsorting="gvAccessRightsReport_Sorting">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css"  Height="30px"/>
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="User ID">
                                        <HeaderStyle Wrap="true" Width="80px" HorizontalAlign="Left"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("userid") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true"  Width="80px" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                      
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Full Name">
                                     <HeaderTemplate>
                                            <asp:LinkButton ID="lblSortName" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                CommandArgument="Full_Name" ForeColor="Blue">Full Name</asp:LinkButton>
                                            <img id="Full_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <HeaderStyle Wrap="true"  Width="120px" HorizontalAlign="Left"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFullName" runat="server" Text='<%#Eval("Full_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" Width="120px" HorizontalAlign="Left"  CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Department">
                                     <HeaderTemplate>
                                            <asp:LinkButton ID="lblSortDept" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                CommandArgument="Department" ForeColor="Blue">Department</asp:LinkButton>
                                            <img id="Department" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <HeaderStyle Wrap="true"  Width="100px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true"  Width="100px" HorizontalAlign="Left"  CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company">
                                     <HeaderTemplate>
                                            <asp:LinkButton ID="lblSortCompny" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                CommandArgument="Company_Name" ForeColor="Blue">Company</asp:LinkButton>
                                            <img id="Company_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <HeaderStyle Wrap="true"  Width="100px" HorizontalAlign="Left"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompany" runat="server" Text='<%#Eval("Company_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true"  Width="100px" HorizontalAlign="Left"  CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Last Login">
                                        <HeaderStyle  Wrap="true"  Width="120px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLastLogin" runat="server" Text='<%#Eval("Last_Login") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true"  Width="120px" HorizontalAlign="Left"  CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Station IP">
                                        <HeaderStyle Wrap="true"  Width="80px" HorizontalAlign="Left"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStationIP"   runat="server" Text='<%#Eval("Station_IP") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px"  CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                       
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Menu">
                                         <HeaderTemplate>
                                            <asp:LinkButton ID="lblSortMenu" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                CommandArgument="Menu" ForeColor="Blue">Menu</asp:LinkButton>
                                            <img id="Menu" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <HeaderStyle Wrap="true"  Width="100px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMenu" runat="server" Text='<%#Eval("Menu") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true"  Width="100px" HorizontalAlign="Left"  CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Menu">
                                     <HeaderTemplate>
                                            <asp:LinkButton ID="lblSortSubMenu" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                CommandArgument="Sub_Menu" ForeColor="Blue">Sub Menu</asp:LinkButton>
                                            <img id="Sub_Menu" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <HeaderStyle Wrap="true"  Width="100px" HorizontalAlign="Left"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubMenu" runat="server" Text='<%#Eval("Sub_Menu") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true"  Width="100px" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Page Name">
                                     <HeaderTemplate>
                                            <asp:LinkButton ID="lblSortPageName" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                CommandArgument="Page_Name" ForeColor="Blue">Page Name</asp:LinkButton>
                                            <img id="Page_Name" runat="server" visible="false"  />
                                        </HeaderTemplate>
                                        <HeaderStyle Wrap="true" Width="200px" HorizontalAlign="Left"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPageName"  Width="200px" runat="server" Text='<%#Eval("Page_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Page Description">
                                        <HeaderStyle Wrap="true" Width="200px" HorizontalAlign="Left"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPageDescription" runat="server" Text='<%#Eval("Page_Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Access to">
                                        <HeaderStyle Wrap="true" Width="300px" HorizontalAlign="Left"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccessTo" runat="server" Text='<%#Eval("Access_to") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left"  CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Access Description">
                                        <HeaderStyle Wrap="true" Width="350px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccessDescription" runat="server" Text='<%#Eval("Access_Desp") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="350px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Last Changed">
                                      <HeaderTemplate>
                                            <asp:LinkButton ID="lblSortLastChanged" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                CommandArgument="Last_changed" ForeColor="Blue">Last Changed</asp:LinkButton>
                                            <img id="Last_changed" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="140px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLastChanged" runat="server" Text='<%#Eval("Last_changed") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="140px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Changed by">
                                      <HeaderTemplate>
                                            <asp:LinkButton ID="lblSortChangedBy" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                CommandArgument="Changed_by" ForeColor="Blue">Changed by</asp:LinkButton>
                                            <img id="Changed_by" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <HeaderStyle  Wrap="true" HorizontalAlign="Left" Width="100px"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblChangedBy" runat="server" Text='<%#Eval("Changed_by") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left"  Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                       
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div style="margin-top: 2px; border: 0px solid #cccccc; vertical-align: bottom; padding: 2px;
                            color: Black; text-align: left;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="50" OnBindDataItem="Bind_AccessRightsReport" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                        <asp:PostBackTrigger ControlID="btnMacExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
