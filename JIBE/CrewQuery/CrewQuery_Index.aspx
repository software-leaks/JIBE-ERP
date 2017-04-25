<%@ Page Title="Crew Queries" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CrewQuery_Index.aspx.cs" Inherits="CrewQuery_CrewQuery_Index" %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <%--<script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
    <script src="../Scripts/CrewQuery_DataHandler.js" type="text/javascript"></script>
    <script type="text/javascript">
        function initScripts() {
           //$('.sailingInfo').SailingInfo();
            $('.vesselinfo').InfoBox();
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" class="page-title">
        Crew Queries
    </div>
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
    <div id="dvPageContent" class="page-content-main">
        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            color: Black; text-align: left; background-color: #fff;">
            <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="HiddenField_Vessel_ID" runat="server" />
                    <asp:HiddenField ID="HiddenField_QueryID" runat="server"/>

                    <table style="width: 100%">
                        <tr>
                            <td style="width: 100px; text-align: right">
                                Fleet :
                            </td>
                            <td style="width: 200px; text-align: left">
                                <asp:DropDownList ID="ddlFleet" runat="server" Width="156px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 100px; text-align: right">
                                Query Type :
                            </td>
                            <td style="width: 200px; text-align: left">
                                <asp:DropDownList ID="ddlQueryType" runat="server" OnSelectedIndexChanged="ddlQueryType_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td colspan="2" style="text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; text-align: right">
                                Vessel :
                            </td>
                            <td style="width: 200px; text-align: left">
                                <asp:DropDownList ID="ddlVessel" runat="server" Width="156px" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 100px; text-align: right">
                                Status :
                            </td>
                            <td style="width: 200px; text-align: left">
                                <asp:RadioButtonList ID="rdoStatus" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoStatus_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="ALL" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Completed" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 100px; text-align: right">
                                Staff Code :
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtFilterText" runat="server" OnTextChanged="txtFilterText_OnTextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="grid-container" style="margin-top: 2px; border: 1px solid #efefef">
            <asp:UpdatePanel ID="UpdatePanel_Grid" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView_CrewQueries" runat="server" AutoGenerateColumns="False"
                        CellPadding="3" CellSpacing="0" Width="100%" EmptyDataText="No Record Found"
                        CaptionAlign="Bottom" GridLines="None" DataKeyNames="ID" OnRowDataBound="GridView_CrewQueries_RowDataBound"
                        AllowPaging="false" AllowSorting="true" OnSorting="GridView_CrewQueries_Sorting"
                        CssClass="GridView-css">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblONBD" runat="server" Text='<%# Eval("Vessel_Short_Name")%>' class='vesselinfo'
                                        vid='<%# Eval("Vessel_ID")%>' vname='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date Raised" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate_Raised" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation"))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Staff Code" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblstaff_Code" runat="server" NavigateUrl='<%# "/"+System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString()+"/crew/CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                                        Target="_blank" Text='<%# Eval("STAFF_CODE")%>' CssClass="staffInfo pin-it"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Width="60px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblSTAFF_NAME" runat="server" NavigateUrl='<%# "/"+System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString()+"/crew/CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                                        Target="_blank" Text='<%# Eval("Staff_FullName")%>' ForeColor="Black"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblQueryType" runat="server" Text='<%# Eval("Query_Type")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Details" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblQuery_Detail" runat="server" NavigateUrl='<%# "CrewQuery_Details.aspx?ID=" + Eval("ID").ToString() + "&VID=" + Eval("Vessel_ID").ToString()+ "&CrewID=" + Eval("CrewID").ToString() %>'
                                        Target="_blank" Text='<%# Eval("Query_Detail")%>' CssClass="pin-it"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Width="350px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Followup/Attachments">
                                <ItemTemplate>
                                    <asp:ImageButton ID="lnkAddFollowUp" ImageUrl="~/Images/AddAttchment.png" runat="server" OnClientClick='<%# "AddFollowUp(" + Eval("Vessel_ID").ToString() + "," + Eval("ID").ToString() + ");return false;" %>'
                                     onmouseover='<%# "showQueryFollowups(event, " + Eval("Vessel_ID").ToString() + "," + Eval("ID").ToString() + ");return false;" %>' onmouseout="$('#dialog').hide();"> </asp:ImageButton>
                                    
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/attach-icon.png"
                                        AlternateText='<%#Eval("Attach_Count")%>' Visible='<%#Eval("Attach_Count").ToString() =="0"?false:true%>'
                                        OnClientClick='<%# "showQueryAttachments(event," + Eval("Vessel_ID").ToString() + "," + Eval("ID").ToString() + "); return false;"%>'>
                                    </asp:ImageButton>
                                </ItemTemplate>
                                <ItemStyle Width="80px" Font-Size="10px" HorizontalAlign="Center" />
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
                    <uc1:ucCustomPager ID="ucCustomPager_CrewQueries" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Records"
                        AlwaysGetRecordsCount="true" OnBindDataItem="Load_Crew_Queries" />
                    <div id="dvPopup" class="draggable" style="display: none; background-color: #CBE1EF;
                        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
                        left: 0.5%; top: 15%; width: 400px; z-index: 1; color: black" title='Add FollowUp'>
                        <div class="content">
                            <table>
                                <tr>
                                    <td>
                                        Followup
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFollowUp" runat="server" TextMode="MultiLine" Width="300px" Height="80px">                                
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: right">
                                        <asp:Button ID="btnSaveFollowUp" Text="Save" runat="server" OnClick="btnSaveFollowUp_Click"
                                            OnClientClick="hideModal('dvPopup');" />
                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClientClick="hideModal('dvPopup');" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="dialog" title="Follow-ups" style="top: 0px; left: 0px; width: 600px; display: none;
        position: absolute;">
        <iframe id="iframeFollowups" style="width: 100%; height: 100%; border: 0px;"></iframe>
    </div>
</asp:Content>
