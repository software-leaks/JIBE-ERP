<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PassageInfo.aspx.cs" EnableEventValidation="false"
    MasterPageFile="~/Site.master" Inherits="PassageInfo" %>

<%@ Register Src="~/UserControl/GoogleMapForASPNet.ascx" TagName="GoogleMapForASPNet"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <style type="text/css">
        .latlontxt
        {
            height: 12px;
            width: 110px;
            font: menu;
            font-size: xx-small;
        }
    </style>
    <style>
        .mybox
        {
            width: 45px;
            height: 12px;
            font: menu;
            font-size: xx-small;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            &nbsp;
            <asp:Label ID="LabelCompany" runat="server" Text="Company:" Font-Bold="true" Font-Names="Arial"></asp:Label>
            <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"
                Font-Size="XX-Small">
            </asp:DropDownList>
            <asp:Label ID="LabelFleet" runat="server" Text="Fleet:" Font-Bold="true" Font-Names="Arial"></asp:Label>
            <asp:DropDownList ID="ddlTechmanager" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTechmanager_SelectedIndexChanged"
                Font-Size="XX-Small">
            </asp:DropDownList>
            Vessel
            <asp:DropDownList ID="ddl_shipname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_shipname_SelectedIndexChanged"
                Font-Size="XX-Small">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp; From Date
            <asp:DropDownList ID="ddl_frmdate" runat="server" Font-Size="XX-Small">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; To Date
            <asp:DropDownList ID="ddl_todate" runat="server" Font-Size="XX-Small">
            </asp:DropDownList>
            &nbsp;&nbsp;
            <asp:Button ID="btn_route" runat="server" Text="Retrieve" OnClick="btn_route_Click"
                Visible="False" />
            &nbsp;
            <asp:LinkButton ID="lbtn_daily" runat="server" OnClick="lbtn_daily_Click">View Daily Position List</asp:LinkButton>
        </ContentTemplate>
    </asp:UpdatePanel>
    Lat,Lon<input id="txthtmltemp" type="text" class="latlontxt" />
    &nbsp;Local Time<input id="txttime" type="text" class="mybox" />
    &nbsp;GMT<input id="txtgmt" type="text" class="mybox" />
    &nbsp;
    <input id="chck_showclouds" type="checkbox" checked="checked" />Clouds
    <input id="show_daylight" type="checkbox" checked="checked" />Day/Night
    <div>
        <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" OnLoad="GoogleMapForASPNet1_Load" />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Route" />
        <br />
        <br />
        <div style="text-align: left;">
            <asp:TextBox ID="htxt_sat_nor_hybrid" runat="server" Height="16px" Width="100px"
                BorderStyle="None" Enabled="false"></asp:TextBox>
        </div>
    </div>
</asp:Content>
