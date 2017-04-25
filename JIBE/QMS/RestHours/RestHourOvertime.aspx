<%@ Page Title="Overtime Details" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="RestHourOvertime.aspx.cs"
    Inherits="RestHourOvertime" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
<%--<script src="../../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">                
              <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

    <center>
        <div style="width: 1000px; color: Black;">
            <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
              <Triggers>
                    <asp:PostBackTrigger ControlID="ImgExpExcel"/>                   
                    </Triggers>
                <ContentTemplate>
                    <div style="border: 1px solid #cccccc" class="page-title">
                       Overtime Details
                    </div>
                    <div id="dvpage-content" class="page-content-main" style="padding: 1px">
                        <table cellpadding="2" cellspacing="0" width="100%">
                            <tr>
                                <td align="right">
                                    Fleet :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDLFleet" runat="server"  Width="150px"
                                        AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">SELECT ALL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                              
                                <td align="right">
                                    From Date :<asp:Label ID="Label2" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtfrom" AutoPostBack="true"  runat="server"
                                        OnTextChanged="txtfrom_TextChanged"></asp:TextBox>
                                    <cc1:CalendarExtender ID="calfrom" Format="dd-MM-yyyy" TargetControlID="txtfrom"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                    <asp:Button ID="btnRetrieve" runat="server" Font-Size="11px" Height="22px" OnClick="btnRetrieve_Click"
                                        Text="Search" Width="80px" />
                                </td>
                                <td style="width: 10%" align="center">
                              
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Vessel :<asp:Label ID="lbl1" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>&nbsp;&nbsp; 
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlvessel"  Width="150px" runat="server"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlvessel_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                  
                                <td align="right">
                                    To Date :<asp:Label ID="Label1" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtto" AutoPostBack="true" runat="server" OnTextChanged="txtto_TextChanged"></asp:TextBox>
                                    <cc1:CalendarExtender ID="calto" Format="dd-MM-yyyy" TargetControlID="txtto" runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                    <asp:Button ID="btnClearFilter" runat="server" Font-Size="11px" Height="20px" OnClick="btnClearFilter_Click"
                                        Text="Clear Filters" Width="80px" />
                                </td>
                                <td>
                                     <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to Excel"  OnClick="btnExport_Click"
                                                                    Width="16px" ImageUrl="~/Images/Excel-icon.png" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div style="border: 0px solid gray; margin-top: 0px;   height: 700px;">
                            <asp:GridView ID="gvDeckLogBook" runat="server" EmptyDataText="No record found !"
                                AutoGenerateColumns="False" Width="100%" CssClass="gridmain-css" GridLines="None"
                                CellPadding="4" AllowSorting="True" Style="margin-right: 0px;"
                                OnSorting="gvDeckLogBook_Sorting" OnRowDataBound="gvDeckLogBook_RowDataBound"
                                OnRowCreated="gvDeckLogBook_RowCreated"  >
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel Name">
                                        <HeaderTemplate>
                                        
                                           <asp:LinkButton ID="lblVesseleNameHeader" runat="server" ForeColor="Black" CommandArgument="Vessel_Name"
                                                CommandName="Sort">Vessel&nbsp;</asp:LinkButton>
                                            <img id="Vessel_Name" runat="server" visible="false" /> 
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVSLName" Text='<%#Eval("Vessel_Name")%>' runat="server" class='vesselinfo'
                                                vid='<%# Eval("Vessel_ID")%>' vname='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                            <asp:Label ID="lblDeckLogBookID" Visible="false" runat="server" Text='<%# Eval("ROWNUMBER")%>'></asp:Label>
                                            <asp:Label ID="lblVesselID" Visible="false" runat="server" Text='<%# Eval("Vessel_ID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Code">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblStaff_CodeHeader" runat="server" ForeColor="Black" CommandArgument="Staff_Code"
                                                CommandName="Sort">Staff Code</asp:LinkButton>
                                            <img id="Staff_Code" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:HyperLink ID="lblStaff_Code" runat="server" Text='<%# Eval("Staff_Code") %>' NavigateUrl='<%# "~/Crew/CrewDetails.aspx?ID="+Eval("CREWID").ToString() %>' Target="_blank" CssClass="staffInfo" ></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Crew Name">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblStaff_NameHeader" runat="server" ForeColor="Black" CommandArgument="Staff_Name"
                                                CommandName="Sort">Crew Name</asp:LinkButton>
                                            <img id="Staff_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStaff_Name" runat="server" Text='<%# Eval("Staff_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rank">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblStaff_RankHeader" runat="server" ForeColor="Black" CommandArgument="Staff_Rank"
                                                CommandName="Sort">Rank</asp:LinkButton>
                                            <img id="Staff_Rank" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStaff_Rank" runat="server" Text='<%# Eval("Staff_rank_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ship's Clocked Hours">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSHIPS_CLOCKED_HOURSHeader" runat="server">Ship's Clocked Hours</asp:Label>                                          
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSHIPS_CLOCKED_HOURS" runat="server" Text='<%# Eval("Total_SHIPS_CLOCKED_HOURS")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Working Hours">
                                        <HeaderTemplate>
                                             <asp:Label ID="lblWORKING_HOURSHeader" runat="server">Work Hours</asp:Label>           
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblWORKING_HOURS" runat="server" Text='<%# Eval("Total_WORKING_HOURS")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rest Hours">
                                        <HeaderTemplate>
                                           <asp:Label ID="lblREST_HOURSHeader" runat="server">Rest Hours</asp:Label>      
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblREST_HOURS" runat="server" Text='<%# Eval("Total_REST_HOURS")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Rest Hours">
                                        <HeaderTemplate>
                                           <asp:Label ID="lblTotal_OVERTIMEHeader" runat="server">Overtime (Hours)</asp:Label>      
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal_OVERTIME" runat="server" Text='<%# Eval("Total_OVERTIME")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                  
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem=" BindGrid" PageSize ="20" />
                        </div>
                    </div>
                </ContentTemplate>
                   <Triggers>
            <asp:PostBackTrigger ControlID="ImgExpExcel" />
        </Triggers>
            </asp:UpdatePanel>
        </div>
    </center>
</asp:Content>
