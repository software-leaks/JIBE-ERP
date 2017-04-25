<%@ Page Title="Tanker Inspection" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="TankInspection.aspx.cs" Inherits="eForms_eFormTempletes_TankInspection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../JS/common.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../../Scripts/iframe.js" type="text/javascript"></script>
    <style type="text/css">
        .eform-vertical-text
        {
            font: bold 14px verdana;
            font-weight: normal;
            writing-mode: tb-rl;
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=2);
        }
        .style3
        {
            width: 3px;
        }
        .style4
        {
            width: 436px;
            height: 20px;
        }
        .style5
        {
            width: 272px;
            height: 20px;
        }
        .style7
        {
            width: 104px;
        }
        .style10
        {
            width: 436px;
            height: 17px;
        }
        .style11
        {
            width: 272px;
            height: 17px;
        }
        .style16
        {
            width: 436px;
        }
        .style17
        {
            width: 1%;
        }
        .style19
        {
            width: 160px;
            height: 17px;
        }
        .style20
        {
            width: 160px;
            height: 20px;
        }
        .style21
        {
            height: 17px;
        }
        .style22
        {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelport" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="ImgExpExcel" />
        </Triggers>
        <ContentTemplate>
            <div id="page-title" class="page-title">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" height="50px">
                    <tr class="eform-report-header">
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 34%; text-align: center;">
                            <asp:Label ID="lblPageTitle" runat="server" Text="Tanker"></asp:Label>
                        </td>
                        <td width="10%">
                            &nbsp;&nbsp;
                            <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to Excel" OnClick="ImgExpExcel_Click"
                                Width="16px" ImageUrl="~/Images/Excel-icon.png" />
                            &nbsp;&nbsp;
                            <img src="../../Images/Printer.png" style="cursor: hand;" alt="Print" onclick="PrintDiv('dvPageContent')" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr class="eform-report-header">
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 34%; text-align: center;">
                            <asp:Label ID="Label1" runat="server" Text="Test / Inspection  Record (MONTHLY)"></asp:Label>
                        </td>
                        <td width="10%">
                    </tr>
                </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvPageContent" class="page-content-main">
        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            width: 1200px; margin-top: 30px; color: Black; text-align: left; background-color: #fff;">
            <table>
                <tr>
                    <td style="width: 80px">
                        Vessel Name:
                    </td>
                    <td style="width: 200px" class="eform-field-data" align="left">
                        <asp:Label ID="lblVesselName" runat="server"></asp:Label>
                    </td>
                    <td style="width: 800px">
                    </td>
                    <td style="width: 80px" align="right">
                        Year:
                    </td>
                    <td class="eform-field-data" align="left" style="width: 80px">
                        <asp:Label ID="lblYear" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView_Tanker" DataKeyNames="ID" runat="server"
                        AutoGenerateColumns="False" OnRowDataBound="GridView_Tanker_RowDataBound"
                        CellPadding="7" AllowPaging="false" Width="100%" ShowFooter="false" EmptyDataText="No Record Found"
                        CaptionAlign="Bottom" GridLines="Both">
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblNo" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ROWNUM") %>'></asp:Label>--%>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eqipment">
                                <ItemTemplate>
                                    <%# Eval("Eqipment_Name")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <%# Eval("TankerDate1", "{0:dd/MM/yyyy}")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <%# Eval("TankerDate2", "{0:dd/MM/yyyy}")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <%# Eval("TankerDate3", "{0:dd/MM/yyyy}")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <%# Eval("TankerDate4", "{0:dd/MM/yyyy}")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <%# Eval("TankerDate5", "{0:dd/MM/yyyy}")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" HorizontalAlign="Center" />
                        <PagerStyle CssClass="PagerStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                    </asp:GridView>
                    <tr>
                        <td>
                            <table cellpadding="1" cellspacing="1" style="border: 1px solid #cccccc; font-family: Tahoma;
                                font-size: 12px; width: 100%;">
                                <tr>
                                    <td colspan="2">
                                        <%-- * For results on Lashing Gear Inventory Report&nbsp; :--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 0px; font-family: Times New Roman; font-size: 100%; width: 50%;">
                                        Note:
                                    </td>
                                    <td class="style7">
                                        <td>
                                        </td>
                                        <tr>
                                            <td style="padding-left: 0px; font-size: 85%; width: 50%;">
                                                1.Vessel should add other ship specific available equipment in the empty columns and refer to equipment manual for frequency of check and act accordingly
                                            </td>
                                        </tr>
                                </tr>
                                <tr>
                                    <td style="padding-left: 0px; font-size: 85%; width: 50%;">
                                        2.Insert N.A. for equipment not available on board.
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 0px; font-size: 85%; width: 50%;">
                                        3.This is a six-monthly form per annum. File this form in the Tanker file.
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="padding-left: 0px; font-size: 85%; width: 50%;color:Red;">
                                       <u> 4.This is the minimum frequency. In addition, Cargo Pump emergency stop shall be carried out as and when required</u>
                                    </td>
                                </tr>
                                              <tr>
                                <td>
                                  <table>
                                    <tr>
                                        <td align="left" class="style17" valign="top">
                                            Remarks:
                                        </td>
                                        <td align="left" width="50%">
                                            <asp:TextBox ID="txtremarks" Height="50px" runat="server" ReadOnly="true" TextMode="MultiLine"
                                                Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                </td>
                                </tr>
                           
                <tr>
                        <td colspan="6">
                            <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px;
                                background: url(../../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                                background-color: #F6CEE3; font-family: Tahoma; font-size: 12px;">
                                <table cellspacing="0" cellpadding="1" rules="all" style="background-color: White;"
                                    width="100%">
                                    <tr>
                                    <td align="right" style="width:15%; border-left: 1px solid black;">
                                            Carried out by Chief Officer:&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 2%; border-left: 1px solid black;">
                                            <asp:Image ID="imgChiefOfficer" runat="server" Height="30px" CssClass="transactLog" />
                                        </td>
                                        <td align="left" style="width: 20%; border-left: 1px solid black;">
                                            <asp:HyperLink ID="lnkChiefOfficer" CssClass="FieldDottedLine link" runat="server"
                                                ForeColor="Blue"></asp:HyperLink>
                                        </td>
                                        <td align="right" style="width: 15%; border-left: 1px solid black;">
                                            Verified by Master: :&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 2%; border-left: 1px solid black;">
                                            <asp:Image ID="imgMaster" runat="server" Height="30px" CssClass="transactLog" />
                                        </td>
                                        <td align="left" style="width: 20%; border-left: 1px solid black;">
                                            <asp:HyperLink ID="lnkMaster" CssClass="FieldDottedLine link" runat="server" ForeColor="Blue"></asp:HyperLink>
                                        </td>
                                        
                                        
                                    </tr>
                                </table>
                            </div>
                                  <asp:HiddenField ID="hdnBaseURL" runat="server"></asp:HiddenField>
              <asp:HiddenField ID="hdnContent" runat="server" />
                        </td>
                    </tr>
                  
                              
                            </table>
                        </td>
                    </tr>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
