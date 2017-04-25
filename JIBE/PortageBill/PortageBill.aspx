<%@ Page Title="Portage Bill" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PortageBill.aspx.cs" Inherits="PortageBill_PortageBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min1.9.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <style type="text/css">
        .PbillHeaderStyle-css-fixed
        {
            background-color: #F5F5F5;
            font-size: 11px;
            color: #333333;
        }
        .GHeaderStyle-css
{
    
    background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
    color: #333333;
    font-size: 11px;
    padding: 5px;
    text-align: left;
    vertical-align: middle;
    border: 1px solid #959EAF;
    border-collapse: collapse;
  
}
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=GridViewPB.ClientID%>').gridviewScroll({
                height: 600,
                width: 1200,
                freezesize: 6
            });
        }

        function PbillConfirm() {

            var con = confirm('You have chosen to FINALIZE office portage bill.You will not be able to modify this if you continue. Do you want to continue');
            if (con)
                return true;
            else 
             return false;

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: hidden;">
        <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UPDMain" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExportExcel"/>
            </Triggers>
            <ContentTemplate>
                <div id="page-title" style="margin: 2px; border: 1px solid #cccccc; height: 20px;
                    vertical-align: bottom; background: url(../Images/bg.png) left -10px repeat-x;
                    color: Black; text-align: left; padding: 2px; background-color: #F6CEE3;">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 33%;">
                            </td>
                            <td style="width: 33%; text-align: center; font-weight: bold;">
                                <asp:Label ID="lblPageTitle" runat="server" Text="Portage Bill"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td style="width: 33%; text-align: right; padding-right: 25px">
                                <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/Images/Excel-icon.png"
                                    Height="18px" OnClick="btnExportExcel_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="text-align:right;margin: 1px;padding:3px; border: 1px solid #cccccc;">
                    <asp:Button ID="btnGeneratePortageBill" runat="server" Text="Generate Portage bill" Font-Size="14px" Height="30px" style="margin-right:20px"
                        OnClick="btnGeneratePortageBill_Click" Visible="false" />
                    <asp:Button ID="btnFinalizePortageBill" runat="server" Text="Finalize Office Portage bill" Font-Size="14px" Height="30px"
                        OnClick="btnFinalizePortageBill_Click" OnClientClick="return PbillConfirm();"
                        Visible="false" />
                </div>
                <div id="gv">
                    <asp:GridView ID="GridViewPB" runat="server" ForeColor="Black" ShowFooter="True" EmptyDataText="no crew found." ShowHeaderWhenEmpty="true"
                        CssClass="GridView-css" AutoGenerateColumns="true" GridLines="None" CellSpacing="0"
                        OnDataBound="GridViewPB_DataBound" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                        BorderWidth="1px" CellPadding="4" OnRowDataBound="GridViewPB_RowDataBound">
                        <HeaderStyle CssClass="GHeaderStyle-css" />
                        <RowStyle CssClass="RowStyle-css" HorizontalAlign="Center" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                    </asp:GridView>                


                    <br />
                    <br />
                       <asp:GridView ID="GridView1" runat="server" ForeColor="Black" ShowFooter="True" EmptyDataText="no crew found." ShowHeaderWhenEmpty="true" Visible="false"
                        CssClass="GridView-css" AutoGenerateColumns="true" GridLines="None" CellSpacing="0"
                        OnDataBound="GridView1_DataBound" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                        BorderWidth="1px" CellPadding="4" OnRowDataBound="GridView1_RowDataBound">
                        <HeaderStyle CssClass="GHeaderStyle-css" />
                        <RowStyle CssClass="RowStyle-css" HorizontalAlign="Center" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                    </asp:GridView>  
                </div>



                 
            </ContentTemplate>
        </asp:UpdatePanel>        
    </div>
</asp:Content>
