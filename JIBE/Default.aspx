<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <center>
        <div class="errormsg">
            <asp:Label ID="lblMsg" runat="server"></asp:Label></div>
        <asp:Panel ID="pnlCalendar" runat="server" Visible="false">
            
        </asp:Panel>
        <asp:Panel ID="pnlCompInfo" runat="server">
            <div id="dvMain" style="text-align: center;">
            <a href="" target="" >  </a>
                <div id="dvContent" style="height: 400px; border: 1px solid #aaeeee; padding: 10px;">
                    <h2>
                        Welcome to JiBE.
                    </h2>
                    <p>
                    Please contact your admin!
                        <%--To learn more about us visit our website <a href="http://www.jibe.com.sg/"
                            title="JiBE">JiBE</a>.--%>
                    </p>
                </div>
            </div>
        </asp:Panel>
    </center>
</asp:Content>
