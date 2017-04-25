<%@ Page Title="Deck Log Book Index" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="RestHourExceptionReports.aspx.cs"
    Inherits="RestHourExceptionReports" %>

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
    <script src="../../Scripts/common_functions.js" type="text/javascript"></script>
<%--<script src="../../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <div style="width: 1000px; color: Black;">
            <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                <ContentTemplate>
                    <div style="border: 1px solid #cccccc" class="page-title">
                        Rest Hours Exception Reports</div>
                    <div id="dvpage-content" class="page-content-main" style="padding: 1px">
                        <table cellpadding="2" cellspacing="0" width="100%">
                           
                            <tr>
                                <td align="left">
                                  &nbsp;&nbsp;&nbsp;&nbsp;  For Month :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                <asp:TextBox ID="txtMonth" runat ="server" ></asp:TextBox>
                                  </td>
                                  <td  align="right"></td>
                                <td align="left"> 
                                    &nbsp;</td>
                                     <td align="left">
                                    &nbsp;</td>
                                <td align="right">
                                    Print Date
                                </td>
                               
                                <td>
                                    &nbsp;</td>
                                <td>
                                     <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to Excel" 
                                                                    Width="16px" ImageUrl="~/Images/Excel-icon.png" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div style="border: 0px solid gray; margin-top: 0px; cursor: pointer; height: 700px;">
                            <asp:GridView ID="gvDeckLogBook" runat="server" EmptyDataText="No record found !"
                                AutoGenerateColumns="False" Width="100%" CssClass="GridView-css" GridLines="None"
                                CellPadding="4" AllowSorting="True" Style="margin-right: 0px; cursor: pointer;"
                                OnSorting="gvDeckLogBook_Sorting" OnRowDataBound="gvDeckLogBook_RowDataBound"
                                OnRowCreated="gvDeckLogBook_RowCreated">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
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
                                            <asp:Label ID="lblDeckLogBookID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                            <asp:Label ID="lblVesselID" Visible="false" runat="server" Text='<%# Eval("Vessel_ID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblDateFromHeader" runat="server" ForeColor="Black" CommandArgument="REST_HOURS_DATE"
                                                CommandName="Sort"> Date&nbsp;</asp:LinkButton>
                                            <img id="REST_HOURS_DATE" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblREST_HOURS_DATE" runat="server" Text='<%# Eval("REST_HOURS_DATE", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblStaff_CodeHeader" runat="server" ForeColor="Black" CommandArgument="Staff_Code"
                                                CommandName="Sort">Code</asp:LinkButton>
                                            <img id="Staff_Code" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStaff_Code" runat="server" Text='<%# Eval("Staff_Code")%>' CssClass="staffInfo"></asp:Label>
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
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rank">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblStaff_RankHeader" runat="server" ForeColor="Black" CommandArgument="Staff_Rank"
                                                CommandName="Sort">Rank</asp:LinkButton>
                                            <img id="Staff_Rank" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStaff_Rank" runat="server" Text='<%# Eval("Staff_Rank")%>'></asp:Label>
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
                                            <asp:Label ID="lblSHIPS_CLOCKED_HOURS" runat="server" Text='<%# Eval("SHIPS_CLOCKED_HOURS")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Working Hours">
                                        <HeaderTemplate>
                                             <asp:Label ID="lblWORKING_HOURSHeader" runat="server">Working Hours</asp:Label>           
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblWORKING_HOURS" runat="server" Text='<%# Eval("WORKING_HOURS")%>'></asp:Label>
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
                                            <asp:Label ID="lblREST_HOURS" runat="server" Text='<%# Eval("REST_HOURS")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Verifier's Comments">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSeafarer_Remarks" runat="server" Text='<%# Eval("Seafarer_Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>                                   
                                </Columns>
                            </asp:GridView>                           
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </center>
</asp:Content>
