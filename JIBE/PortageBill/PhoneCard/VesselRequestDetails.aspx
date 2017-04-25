<%@ Page Title="CARD REQUEST DETAILS" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="VesselRequestDetails.aspx.cs" Inherits="PortageBill_PhoneCard_VesselRequestDetails" %>

<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .noline
        {
            color: Lime;
            text-decoration: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2;">
        <center>
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
            <asp:Button ID="cmdHiddenSubmit" runat="server" Text="cmdHiddenSubmit" OnClick="cmdHiddenSubmit_Click" />
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <div id="page-header" class="page-title">
                    <b>CARD REQUEST DETAILS</b>
                </div>
                <div style="height: 650px; color: Black;">
                    <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                        <ContentTemplate>
                            <div style="margin: 1px; padding: 1px; border: 1px solid #cccccc;">
                                <table width="100%" cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td align="right">
                                            Request Date :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblRequestDate" runat="server" Width="90px" CssClass="input"></asp:Label>
                                        </td>
                                        <td align="right">
                                            Request Number :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblRequestNumber" runat="server" Width="120px" CssClass="input"></asp:Label>
                                        </td>
                                        <td align="right">
                                            Vessel Name :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblVesselname" runat="server" Width="120px" CssClass="input"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Protage Bill date :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblPortageBillDate" runat="server" Width="90px" CssClass="input"></asp:Label>
                                        </td>
                                        <td align="right">
                                            Total request :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblTotalRequest" runat="server" Width="120px" CssClass="input"></asp:Label>
                                        </td>
                                        <td align="right">
                                            Status :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblStatus" runat="server" Width="120px" CssClass="input"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divGrid" style="position: relative; z-index: 0; width: 100%;">
                                <asp:GridView ID="gvPhoneCardRequest" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" OnRowDataBound="gvPhoneCardRequest_RowDataBound"
                                    DataKeyNames="id" CellPadding="3" CellSpacing="0" Width="100%" OnSorting="gvPhoneCardRequest_Sorting"
                                    AllowSorting="true" Font-Size="11px" CssClass="GridView-css" GridLines="None"
                                    OnSelectedIndexChanged="gvPhoneCardRequest_SelectedIndexChanged">
                                    <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                    <PagerStyle CssClass="PagerStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lbtRequestDateHeader" runat="server" CommandName="Sort" CommandArgument="DATE_OF_CREATION"
                                                    ForeColor="Black" CssClass="noline">Request Date</asp:LinkButton>
                                                <img id="DATE_OF_CREATION" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DATE_OF_CREATION")))%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lbtVesslNameHeader" runat="server" CommandName="Sort" CommandArgument="VESSEL_NAME"
                                                    ForeColor="Black" CssClass="noline">Vessel Name </asp:LinkButton>
                                                <img id="VESSEL_NAME" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("VESSEL_NAME")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblReqNumberrHeader" runat="server" CommandName="Sort" CommandArgument="STAFF_NAME"
                                                    ForeColor="Black" CssClass="noline">Crew Name</asp:LinkButton>
                                                <img id="STAFF_NAME" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbtnRequestNumber" runat="server" Text='<%#Eval("STAFF_NAME") %>'></asp:Label>
                                                <asp:Label ID="lblItemId" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblTotalCardRequestHeader" runat="server" CommandName="Sort"
                                                    CommandArgument="CARD_UNITS" ForeColor="Black" CssClass="noline">Card Unit</asp:LinkButton>
                                                <img id="CARD_UNITS" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalcardRequest" runat="server" Text='<%#Eval("CARD_UNITS")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblReqStatusHeader" runat="server" CommandName="Sort" CommandArgument="CARD_NUM"
                                                    ForeColor="Black" CssClass="noline">Card Number</asp:LinkButton>
                                                <img id="CARD_NUM" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblReqStatus" runat="server" Text='<%#Eval("CARD_NUM")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblActionHeader" runat="server">
                                                    Action
                                                </asp:Label>
                                            </HeaderTemplate>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" AlternateText="Save" ImageUrl="~/Images/upload.png"
                                                    CommandName="UploadCard" Visible='<%#Eval("VISIBLE_STATUS").ToString() != "CLOSE"?true:false %>'
                                                    CommandArgument='<%#Eval("ID")%>' ID="cmdUpload" OnCommand="onUpdate" ToolTip="Upload Card">
                                                </asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindViews" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </center>
    </div>
    <div id="divUploadCard" title="Assign Card" style="display: none; border: 1px solid Gray;
        font-family: Tahoma; text-align: left; font-size: 11px; background-color: White;
        color: Black; width: 500px;">
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="popup-content">
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <table width="100%" cellpadding="5" cellspacing="0">
                            <tr>
                                <td colspan="4" style="text-align: center; color: Black; font-weight: bold;">
                                    Assign Card
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Crew Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCrewName" runat="server" Width="210px" ReadOnly="true" CssClass="input"></asp:TextBox>
                                </td>
                                <td>
                                    Card Unit
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRequestUnit" runat="server" Width="60px" CssClass="input" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Card Number
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCard" runat="server" CssClass="input" ForeColor="Black"
                                        Font-Size="11px" Width="120px" DataSourceID="objCard" DataTextField="Card_Num"
                                        DataValueField="ID" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="objCard" runat="server" TypeName="SMS.Business.PortageBill.BLL_PB_PhoneCard"
                                        SelectMethod="PhoneCard_Kitty_GetCard"></asp:ObjectDataSource>
                                </td>
                                <td>
                                    Card Unit
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="server" Width="60px" CssClass="input" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <asp:Button ID="btnSaveEventEdit" runat="server" Text="Save" OnClick="btnSaveEventEdit_Click" />
                                    <asp:Button ID="btnCloseEventEdit" runat="server" Text="Close" OnClick="btnCloseEventEdit_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%--  <asp:HiddenField ID="hdnEditEventID" runat="server" />
        <table width="100%" cellpadding="1" cellspacing="0">
         <tr>
                    <td align="left">
                        <iframe runat="server" id="ifUploadCard" src="" scrolling="auto" style="min-height: 100px;
                            width: 100%;"></iframe>
                    </td>
                </tr>      
        </table>--%>
    </div>
</asp:Content>
