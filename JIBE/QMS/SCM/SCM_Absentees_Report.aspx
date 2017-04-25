 
<%@ Page Title="SCM - Absentees Report" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="SCM_Absentees_Report.aspx.cs" Inherits="QMS_SCM_SCM_Absentees_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/Wizard.js" type="text/javascript"></script>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.highlight.js" type="text/javascript"></script>
<%--    <script src="../../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; white-space: nowrap;">
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; height: 100%;">
            <div id="page-header" class="page-title">
                <b>Absentees Report</b>
            </div>
            <div style="color: Black;">
                <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                    <ContentTemplate>
                        <div style="margin: 1px; padding: 1px; border: 1px solid #cccccc;">
                            <table width="100%" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td width="100%" valign="top" style="border: 1px solid gray; color: Black">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table width="100%" cellpadding="3" cellspacing="0">
                                                    <tr>
                                                        <td width="20%" align="right" valign="top">
                                                            Fleet :
                                                        </td>
                                                        <td width="25%" valign="top" align="left">
                                                            <asp:DropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged"
                                                                AutoPostBack="true" Width="160" />
                                                        </td>
                                                        <td width="20%" align="right" valign="top">
                                                            Date To :
                                                        </td>
                                                        <td width="25%" valign="top" align="left">
                                                            <asp:TextBox ID="txtToDate" CssClass="input" runat="server" Width="120px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="calToDate" runat="server" Enabled="True" TargetControlID="txtToDate"
                                                                Format="dd/MM/yyyy">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        
                                                        <td>
                                                            <asp:Button ID="btnSearch" Text="Retrieve" runat="server" Width="150px" Height="30px"
                                                                OnClick="btnSearch_Click" />
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                        <td width="20%" align="right" valign="top">
                                                            Vessel :
                                                        </td>
                                                        <td width="25%" valign="top" align="left">
                                                            <asp:DropDownList ID="DDLVessel" runat="server" UseInHeader="false" Width="160" />
                                                        </td>
                                                      
                                                         
                                                        <td width="20%" align="right" valign="top">
                                                           
                                                        </td>
                                                        <td width="25%" valign="top" align="left">
                                                           
                                                        </td>
                                                       
                                                        <td>
                                                            <asp:Button ID="btnClear" Text="Clear" runat="server" Width="150px" ToolTip="Clear Selection"
                                                                Height="30px" OnClick="btnClear_Click" />
                                                        </td>
                                                        <td align="center">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:GridView ID="gvAbsentees" runat="server" EmptyDataText="No Records Found !!"
                            AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"  
                            CellPadding="3" GridLines="None" CellSpacing="0" Width="100%" AllowSorting="True"
                             Font-Size="12px" CssClass="GridView-css" onrowdatabound="gvAbsentees_RowDataBound" 
                            >
                            <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" Wrap="false" />
                            <PagerStyle CssClass="PagerStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <%--<AlternatingRowStyle CssClass="AlternatingRowStyle-css" Wrap="true" />--%>
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" Wrap="true" />
                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Vessel Name">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbtVesslNameHeader" runat="server" 
                                            >Vessel Name</asp:Label>
                                        <img id="Vessel_Name" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name")%>' style="margin-left:10px"></asp:Label>
                                    </ItemTemplate>
                               
                                      <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="125px" CssClass="PaddingCellCss">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                </asp:TemplateField>
                                 <asp:TemplateField Visible="false">
                                    <HeaderTemplate>
                                    <asp:Label ID="lblStaffCodeHead" runat="server" Text='Staff Code' ></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStaffCode" runat="server" Text='<%#Eval("Staff_Code")%>' CssClass="staffInfo"></asp:Label>
                                    </ItemTemplate>
                                     <ItemStyle Wrap="true" HorizontalAlign="Center" Width="125px" CssClass="PaddingCellCss">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Staff Name" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderTemplate>
                                        <asp:Label ID="lblS" runat="server" Text='Staff Name'></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      <asp:HyperLink ID="hlProgramReport" Text='<%# Eval("RankName").ToString()+" - "+ Eval("Staff_code").ToString()+" - "+ Eval("Staff_Name").ToString() %>' runat="server"  NavigateUrl='<%#   "~/Crew/CrewDetails.aspx?ID="+Eval("CrewId").ToString()  %>' style="margin-left:10px"
                                                Target="_blank"></asp:HyperLink> 
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="125px" CssClass="PaddingCellCss">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                </asp:TemplateField>
                                 
                               
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblMonth0H" runat="server"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth0" runat="server" Visible='<%#  !Convert.ToBoolean(Eval("Month0V").ToString()) %>' Text='N/A'></asp:Label>
                                        <asp:Image ID="imgMonth0" runat="server" Visible='<%# Convert.ToBoolean(Eval("Month0V").ToString()) %>' ImageUrl="~/Images/incorrect_icon.png" />
                                    </ItemTemplate>
                                    
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="125px" CssClass="PaddingCellCss">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                </asp:TemplateField>
                   

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblMonth1H" runat="server"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth1" runat="server" Visible='<%#!Convert.ToBoolean(Eval("Month1V").ToString())%>' Text='N/A'></asp:Label>
                                        <asp:Image ID="imgMonth1" runat="server" Visible='<%#Convert.ToBoolean(Eval("Month1V").ToString()) %>' ImageUrl="~/Images/incorrect_icon.png" />
                                    </ItemTemplate>
                                    
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="125px" CssClass="PaddingCellCss">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                </asp:TemplateField>


                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblMonth2H" runat="server"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth2" runat="server" Visible='<%#!Convert.ToBoolean(Eval("Month2V").ToString()) %>' Text='N/A'></asp:Label>
                                        <asp:Image ID="imgMonth2" runat="server" Visible='<%#Convert.ToBoolean(Eval("Month2V").ToString()) %>' ImageUrl="~/Images/incorrect_icon.png" />
                                    </ItemTemplate>
                                    
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="125px" CssClass="PaddingCellCss">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                       
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
   
   
</asp:Content>
