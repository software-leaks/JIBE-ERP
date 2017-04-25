<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RequisitionHoldUnHoldHistory.aspx.cs"
    Inherits="RequisitionHoldUnHoldHistory" Title="View Requisition On Hold History" %>
     <%@ Register Src="~/UserControl/ucCustomPager.ascx"TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
 
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />

</asp:Content>
 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server"> 
  <div class="page-title">
         View Requisition On Hold History
    </div>
  <asp:UpdateProgress ID="UpdateProgress1"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                 
                   <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
    <asp:UpdatePanel ID="updpanal" runat="server">
        <ContentTemplate>
            <center>
                <div style="color: Black">
                 
                    <div id="dvpage-content" class="page-content-main" style="padding: 10px">
                        <div style="padding-top: 2px; padding-bottom: 5px; width: 100%">
                            <table cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td align="right" style="width: 8%;">
                                        Fleet :&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <b>
                                            <asp:DropDownList ID="ddlFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True" 
                                                Font-Size="12px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged" Width="119px">
                                                <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                            </asp:DropDownList>
                                        </b>
                                    </td>
                                    <td align="right" style="width: 10%">
                                        Stage :&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlLinetype" runat="server" AppendDataBoundItems="True" Font-Size="12px"
                                            Width="119px">
                                            <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 7%">
                                        OnHold :&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 8%">
                                        <asp:DropDownList ID="cmbhold" runat="server" AppendDataBoundItems="True" Font-Size="12px" 
                                            Width="119px">
                                            <asp:ListItem Text="--SELECT ALL--" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="OnHold" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Unhold" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 10%">
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click"  
                                            Text="Search" CssClass="button-css" Width="60%" style="margin-left: 0px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Vessel Name :&nbsp;&nbsp;
                                    </td>
                                    <td align="right" style="width: 10%;">
                                        <b>
                                            <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="True" Font-Size="12px"
                                                Width="119px">
                                                <asp:ListItem Selected="True" Value="0">--ALL--</asp:ListItem>
                                            </asp:DropDownList>
                                        </b>
                                    </td>
                                    <td align="right">
                                        Reqsn. Code :&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 15%;">
                                        <b>
                                            <asp:TextBox ID="txtReqCode" runat="server" Style="margin-left: 0px" Width="115px"></asp:TextBox>
                                        </b>
                                    </td>
                                    <td align="right" style="width: 6%;">
                                        User Name :&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtUsername" runat="server" Width="115px"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        Document Code :&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 50px;">
                                        <asp:TextBox ID="txtdocument" runat="server" Width="115px"></asp:TextBox>
                                    </td>
                                    <td align="left" >
                                        <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click"  CssClass="button-css"
                                           Width="60%" Text="Clear Filters"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="overflow-x: hidden; overflow-y: hidden; border: 1px solid #cccccc;">
                            <asp:GridView ID="rgdonholdgrd" runat="server" EmptyDataText="NO RECORDS FOUND" CellPadding="4"
                                CellSpacing="0" AutoGenerateColumns="False" 
                                Width="100%" GridLines="Both"
                                AllowSorting="true" OnSorting="rgdonholdgrd_Sorting">
                              
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVessel_Name" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUser_name" runat="server" Text='<%#Eval("User_name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reqsn. Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequisition_Code" runat="server" Text='<%#Eval("Requisition_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stage">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLine_Type" runat="server" Text='<%#Eval("Line_Type")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocument_Code" runat="server" Text='<%#Eval("Document_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="onHold">
                                        <ItemTemplate>
                                            <asp:Label ID="lblonHold" runat="server" Text='<%#Eval("onHold")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOnHoldDate" runat="server" Text='<%#Eval("OnHoldDate")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="400px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                     <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindViewReqistiononHold" />
                  <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" /> 
                        </div>
                    </div>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
