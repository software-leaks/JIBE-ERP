<%@ Page Title="Survey Vessel Assignment" Language="C#" MasterPageFile="SurveyMaster.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="Survey_Vessel_Assign.aspx.cs"
    Inherits="Surveys_Survey_Vessel_Assign" %>

<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
        }
        h4
        {
            font-size: 12px;
            color: #000;
            margin: 5px 0px 0px 5px;
        }
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        .watermarked
        {
            color: #cccccc;
        }
        
        .linkbtn a
        {
            color: Black;
        }
        
        .linkbtn
        {
            color: black;
            background-color: white;
            text-decoration: none;
            border-left: 1px solid #cccccc;
            padding-left: 10px;
            border-top: 1px solid #cccccc;
            padding-top: 5px;
            border-right: 1px solid #cccccc;
            padding-right: 10px;
            border-bottom: 1px solid #cccccc;
            padding-bottom: 3px;
            background-color: #F1F8E0;
        }
        #dvAddNewCategory
        {
            cursor: move;
        }
        #dvAddNewCertificate
        {
            cursor: move;
        }
        #dvAddNewType
        {
            cursor: move;
        }
    </style>
    <style id="TooltipStyle" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: Tahoma;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .grid-row
        {
            padding: 6px;
        }
        .grid-col-fixed
        {
            border: 1px solid #cccccc;
        }
        .grid-col
        {
            border: 1px solid #cccccc;
        }
        .gradiant-css-browne
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#81F79F',EndColorStr='#088A4B');
            background: -webkit-gradient(linear, left top, left bottom, from(#81F79F), to(#088A4B));
            background: -moz-linear-gradient(top,  #81F79F,  #088A4B);
            color: Black;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function ShowDiv_dvAssignSurvey() {
            document.getElementById("dvAssignSurvey").style.display = "block";
        }
        function CloseDiv_dvAssignSurvey() {
            document.getElementById("dvAssignSurvey").style.display = "None";
        }

        $(document).ready(function () {
            $('#dvAssignSurvey').draggable();
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="AccessMsgDiv" runat="server" style="color: Red; font-size: 14px; text-align: center;">
        You don't have sufficient privilege to access the requested page.</div>
    <div id="MainDiv" runat="server">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div id="page-content" style="overflow: auto;">
            <asp:UpdatePanel ID="UpdatePanelCategory" runat="server">
                <ContentTemplate>
                    <div style="text-align: center; margin-bottom: 15px; font-size: 12px;">
                        <table style="width: 90%;">
                            <tr>
                                <td>
                                    Fleet
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                        Width="156px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Vessel
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"
                                        Width="156px">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right;">
                                    Main Category:
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlMainCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMainCategory_SelectedIndexChanged"
                                        Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: left; width: 100px;">
                                    Sub Category
                                </td>
                                <td style="text-align: left; width: 300px;">
                                    <asp:DropDownList ID="ddlCategoryFilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoryFilter_SelectedIndexChanged"
                                        Width="300px">
                                    </asp:DropDownList>
                                </td>
                                <td align="center" style="width: 5%">
                                    <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                        ImageUrl="~/Images/Refresh-icon.png" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <asp:GridView ID="GridView_Certificate" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="Surv_ID" EmptyDataText="No Record Found" AllowSorting="True" CaptionAlign="Bottom"
                            CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" CssClass="gridmain-css"
                            OnSorting="GridView_Certificate_Sorting" OnRowDataBound="GridView_Certificate_OnRowDataBound">
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                            <Columns>
                                <asp:TemplateField HeaderText="S/N" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                        <%# ((GridViewRow)Container).RowIndex + 1%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Main Category" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMainCategory_Name" runat="server" Text='<%#Eval("Survey_MainCategory")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Category" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory_Name" runat="server" Text='<%#Eval("Survey_Category")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Certificate Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCertificate_Name" runat="server" Text='<%#Eval("Survey_Cert_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCertificate_Name" Font-Size="11px" MaxLength="50" runat="server"
                                            Text='<%#Bind("Survey_Cert_Name")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Equipment Type" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEquipmentType" runat="server" Text='<%#Eval("EquipmentType")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEquipmentType" Font-Size="11px" MaxLength="50" runat="server"
                                            Text='<%#Bind("EquipmentType")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Issuing Authority" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssuingAuth" runat="server" Text='<%#Eval("IssuingAuth")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="IssuingAuth" Font-Size="11px" MaxLength="50" runat="server" Text='<%#Bind("IssuingAuth")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Assign" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Image ID="imgAssign" runat="server" AlternateText="Assigned" Visible='<%# Convert.ToBoolean(Eval("Assigned")) %>'
                                            ImageUrl="~/images/Select.png" />
                                        <asp:Button ID="btnAssign" runat="server" Text=" Assign " BorderStyle="Solid" BorderColor="#cccccc"
                                            BorderWidth="1px" Font-Names="Tahoma" Height="21px" Visible='<%# !Convert.ToBoolean(Eval("Assigned")) %>'
                                            CommandArgument='<%#Eval("Surv_ID")%>' OnClick="btnAssign_Click" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Make Active" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Image ID="imgActive" runat="server" AlternateText="Active" Visible='<%# Convert.ToBoolean(Eval("ACTIVE")) %>'
                                            ImageUrl="~/images/Select.png" />
                                        <asp:Button ID="btnActive" runat="server" Text="Make as Active" BorderStyle="Solid"
                                            BorderColor="#cccccc" OnClientClick="return confirm('Are you sure, you want to mark the survey as ACTIVE?')"
                                            BorderWidth="1px" Font-Names="Tahoma" Height="21px" Visible='<%# (!Convert.ToBoolean(Eval("ACTIVE")) && Convert.ToBoolean(Eval("Assigned"))) %>'
                                            CommandArgument='<%#Eval("Surv_Vessel_ID")%>' OnClick="btnActive_Click" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                            <%-- <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />--%>
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="20" OnBindDataItem="Load_CertificateList" />
                    </div>
                    <asp:Panel ID="pnlAssign" runat="server" Visible="false">
                        <div id="dvAssignSurvey" style="display: block; background-color: #CBE1EF; border-color: #5C87B2;
                            border-style: solid; border-width: 1px; width: 450px; position: absolute; left: 40%;
                            top: 20%; z-index: 1; color: black">
                            <div class="header">
                                <h4>
                                    Assign Survey to Vessel</h4>
                            </div>
                            <div class="content">
                                <asp:Panel ID="pnlAssignSurvey" DefaultButton="btnSaveAssignment" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                                        <ContentTemplate>
                                            <asp:HiddenField ID="hdnSurv_ID" runat="server" />
                                            <table border="0" style="width: 100%;" class="content">
                                                <tr>
                                                    <td style="font-size: 11px; font-weight: bold">
                                                        Equipment Type:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEquipmentType" CssClass="textbox-css" Width="250px" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="trAuthority" runat="server">
                                                    <td style="font-size: 11px; font-weight: bold">
                                                        Issuing Authority:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtIssuingAuthority" CssClass="textbox-css" Width="250px" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="font-size: 11px; text-align: center;">
                                                        <asp:Button ID="btnSaveAssignment" CssClass="button-css" runat="server" Text="Save Assignment"
                                                            OnClick="btnSaveAssignment_Click" />&nbsp;&nbsp;
                                                        <asp:Button ID="btnClose" CssClass="button-css" runat="server" Text="Close" OnClick="btnClose_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
