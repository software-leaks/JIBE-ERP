<%@ Page Title="Crew Trainers" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Trainers.aspx.cs" Inherits="Trainers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 
    <center>
    
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

        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
            <div class="page-title">
              Crew Trainers
            </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="pnlEditTrainingType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td>
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <table border="1" style="width: 100%; border-collapse: collapse;">
                                <tr>
                                    <td style="width:45%">
                                        <table border="0" >
                                            <tr style="background-color: #d8d8d8">
                                                <td style="width:150px;text-align:right">
                                                    Company Name:
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="lstCompany" runat="server" AutoPostBack="true" DataSourceID="ObjectDataSource_CompanyList"
                                                        OnDataBound="lstCompany_DataBound" DataTextField="company_name" DataValueField="id"
                                                        Width="256px">
                                                    </asp:DropDownList>
                                                    <asp:ObjectDataSource ID="ObjectDataSource_CompanyList" runat="server" SelectMethod="Get_CompanyListMini"
                                                        TypeName="SMS.Business.Infrastructure.BLL_Infra_Company">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="UserCompany" SessionField="UserCompanyID" ConvertEmptyStringToNull="true"
                                                                DefaultValue="0" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #d8d8d8">
                                                <td style="width:150px;text-align:right">
                                                    Search:
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtSearchUser" runat="server" Width="250px" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; text-align: left" colspan="2">
                                                    <asp:ListBox ID="lstUserList" runat="server" Height="400px" Width="360px" AutoPostBack="true"
                                                        SelectionMode="Single" DataSourceID="ObjectDataSource_Users" DataTextField="USERNAME"
                                                        DataValueField="USERID"></asp:ListBox>
                                                    <asp:ObjectDataSource ID="ObjectDataSource_Users" runat="server" SelectMethod="Get_UserList"
                                                        TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="lstCompany" DefaultValue="0" Name="CompanyID" PropertyName="SelectedValue"
                                                                Type="Int32" />
                                                            <asp:ControlParameter ControlID="txtSearchUser" DefaultValue="" Name="FilterText"
                                                                PropertyName="Text" Type="String" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="vertical-align: middle;width:10%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add User to Trainer List" OnClick="ImgAdd_Click" 
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td style="vertical-align: top">
                                        <asp:GridView ID="gvTrainers" runat="server" EmptyDataText="NO RECORDS FOUND"
                                            AutoGenerateColumns="False" DataKeyNames="LibUserID" CellPadding="1" CellSpacing="0"
                                            Width="100%" GridLines="both" AllowSorting="true">
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                            <PagerStyle CssClass="PMSPagerStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="List of Trainers" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTrainingType" runat="server" Text='<%#Eval("TrainerName")%>' Style="color: Black"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <HeaderTemplate>
                                                        Action
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                            Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                            CommandArgument='<%#Eval("[LibUserID]")%>' ForeColor="Black" ToolTip="Delete"
                                                            ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
