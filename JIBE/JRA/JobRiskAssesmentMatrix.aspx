<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="JobRiskAssesmentMatrix.aspx.cs"
    Inherits="JRA_JobRiskAssesmentMatrix" Title="Job Risk Assessment Matrix" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .page
        {
            min-width: 400px;
            width: 99%;
            height: 910px;
        }
        .cleartd
        {
            width: 10px;
        }
    </style>
    <script type="text/javascript">
        function btnprint_onclick() {

            var a = window.open('', '', 'left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
            a.document.write(document.getElementById('innerData').innerHTML);
            a.document.close();
            a.focus();
            a.print();
            a.close();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Job Risk Assessment Matrix
    </div>
    <div id="dvPageContent" style="margin-top: -2px; border: 1px solid #cccccc; vertical-align: bottom;
        padding: 4px; color: Black; text-align: left; background-color: #fff;">
        <div style="text-align: right; width: 94%;">
            <asp:ImageButton ID="ImageButton1" src="../Images/printer.gif" Height="30px" OnClientClick="btnprint_onclick()"
                runat="server" AlternateText="Print" /></div>
        <br />
        <%--<asp:GridView ID="gvJRA" runat="server" CellPadding="1" ForeColor="#333333" GridLines="Both"
                        BorderColor="#738DA5" CellSpacing="1" Width="84%" OnRowDataBound="gvJRA_RowDataBound"
                        AutoGenerateColumns="true" ShowHeader="true" OnRowCreated="gvJRA_RowCreated"
                        Visible="false">
                        <EditRowStyle BackColor="#999999" Wrap="true" Font-Size="X-Small" />
                        <HeaderStyle BackColor="#DBDBDB" Font-Bold="False" ForeColor="Black" Wrap="true"
                            Font-Size="X-Small" />
                    </asp:GridView>--%>
        <div id="innerData" runat="server" clientidmode="Static">
        </div>
    </div>
</asp:Content>
