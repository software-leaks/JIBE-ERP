﻿<%@ Page Title="Data Migration Error list" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="DataMigrationIndex.aspx.cs"
    Inherits="DataMigrationIndex" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <div style="width: 1000px; color: Black;">
            <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                <ContentTemplate>
                    <div style="border: 1px solid #cccccc" class="page-title">
                       Data Migration Error List
                    </div>
                    <div id="dvpage-content" class="page-content-main" style="padding: 1px">
                        <table cellpadding="2" cellspacing="0" width="100%">
                            <tr>
                                <td align="right">
                                    Fleet :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDLFleet" runat="server" CssClass="txtInput" Width="150px"
                                        AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">SELECT ALL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td  align="right">Vessel :</td>
                                <td align="left">  
                                    <asp:DropDownList ID="ddlvessel" runat="server" AutoPostBack="true" 
                                        CssClass="txtInput" OnSelectedIndexChanged="ddlvessel_SelectedIndexChanged" 
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Button ID="btnRetrieve" runat="server" Font-Size="11px" Height="22px" 
                                        OnClick="btnRetrieve_Click" Text="Retrieve" Width="80px" />
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnClearFilter" runat="server" Font-Size="11px" Height="20px" 
                                        OnClick="btnClearFilter_Click" Text="Clear Filters" Width="80px" />
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td style="width: 10%" align="center">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    &nbsp;</td>
                                  <td  align="right">&nbsp;</td>
                                <td align="left"> 
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                     &nbsp;</td>
                            </tr>
                        </table>
                        <br />
                        <div style="border: 0px solid gray; margin-top: 0px; cursor: pointer; height: 700px;">
                            <asp:GridView ID="gvDmErrorList" runat="server" EmptyDataText="No record found !"
                                AutoGenerateColumns="False" Width="100%" CssClass="GridView-css" GridLines="None"
                                CellPadding="4" AllowSorting="True" Style="margin-right: 0px; cursor: pointer;"
                                OnSorting="gvDmErrorList_Sorting" OnRowDataBound="gvDmErrorList_RowDataBound"
                                OnRowCreated="gvDmErrorList_RowCreated" OnSelectedIndexChanging="gvDmErrorList_SelectedIndexChanging">
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
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblDateFromHeader" runat="server" ForeColor="Black" CommandArgument="Date_of_creation"
                                                CommandName="Sort"> Date&nbsp;</asp:LinkButton>
                                            <img id="Date_of_creation" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblREST_HOURS_DATE" runat="server" Text='<%# Eval("Date_of_creation", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblStaff_CodeHeader" runat="server" ForeColor="Black" CommandArgument="TABLE_Name"
                                                CommandName="Sort">TABLE Name</asp:LinkButton>
                                            <img id="TABLE_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStaff_Code" runat="server" Text='<%# Eval("TABLE_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                           
                                        <asp:TemplateField HeaderText="Rank">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblStaff_RankHeader" runat="server" ForeColor="Black" CommandArgument="KeyValues"
                                                CommandName="Sort">KeyValues</asp:LinkButton>
                                            <img id="KeyValues" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStaff_Rank" runat="server" Text='<%# Eval("KeyValues")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ship's Clocked Hours">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSHIPS_CLOCKED_HOURSHeader" runat="server">Error Message</asp:Label>                                          
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSHIPS_CLOCKED_HOURS" runat="server" Text='<%# Eval("ErrorMessage")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="350px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>                                        
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem=" BindGrid" PageSize ="20" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </center>
</asp:Content>
