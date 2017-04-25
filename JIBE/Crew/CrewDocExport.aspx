<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="CrewDocExport.aspx.cs"
    Inherits="Crew_CrewDocExport" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
    <script id="DocumentReady" type="text/javascript">
        function loadDiv() {

            $('#dvdialog').load('CrewProfile.aspx?P=1&ID=' + '<%= this.Request.QueryString["CrewID"]%>' + "&rnd=" + Math.random() + ' #dvPageContent');

        }
        function GetDivElement() {
            //debugger;
            //var str =  document.getElementById('dvdialog').innerHTML;
            //var res = str.replace(/"/g, "'");
            //document.getElementById('hdnCrewDetails').value = res;
            //document.getElementById('dvdialog').innerHTML = document.getElementById('dvdialog').innerHTML;
            //$("#hdnCrewDetails").val = $("#dvdialog").innerHTML;
            if (document.getElementById('hdnCrewDetails').value.trim() == "")
                document.getElementById('hdnCrewDetails').value = document.getElementById('dvdialog').innerHTML;
        }
    </script>
    <style type="text/css">
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
        body
        {
            font-family: Tahoma;
            font-size: 11px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="dvdialog" class="cls" style="display: none">
    </div>
    <div id="page-content" style="z-index: -2; overflow: auto; text-align: center; height: 100%">
        <div id="pageTitle" style="background-color: gray; color: White; font-size: 12px;
            text-align: center; padding: 2px; font-weight: bold;">
            <asp:Label ID="lblPageTitle" runat="server" Text="Crew Documents"></asp:Label>
        </div>
        <div class="error-message">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <asp:HiddenField ID="hdnCrewID" runat="server" Value="0" />
        <div>
        </div>
        <center>
            <div style="text-align: left; border: 1px solid gray;">
                <table cellspacing="0" cellpadding="2" rules="rows" style="background-color: White;
                    width: 100%;">
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        Crew Name:
                                    </td>
                                    <td style="width: 300px">
                                        <asp:Label ID="lblCrewName" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Rank:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRank" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="font-weight: bold">
                                        Search Document:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSearchText" runat="server" ForeColor="Black" Font-Bold="true"
                                            OnTextChanged="txtSearchText_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="DownloadFiles"
                                OnClientClick="GetDivElement();" />
                        </td>
                        <td align="right">
                            <asp:CheckBox ID="chkAllDocs" OnCheckedChanged="chk_ALLDocs_CheckedChanged" Text="Select ALL"
                                runat="server" AutoPostBack="true" ForeColor="Black" Font-Bold="true" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="grdCrewDoc" runat="server" AutoGenerateColumns="False" Width="100%"
                                AllowSorting="True" DataKeyNames="DocTypeID" CellPadding="4" ForeColor="#333333"
                                GridLines="None" OnRowDataBound="grdCrewDoc_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="DocTypeName" HeaderText="Document" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="DocNo" HeaderText="Doc.No" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField HeaderText="Issue Date" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssueDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DateOfIssue"))) %>' ></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expiry Date" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpiryDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DateOfExpiry"))) %>' ></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PlaceOfIssue" HeaderText="Issue Place" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                            <asp:Label ID="lblFilePath" runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <asp:HiddenField ID="hdnCrewRank" runat="server" />
                            <asp:HiddenField ID="hdnStaffCode" runat="server" />
                            <asp:HiddenField ID="hdnFirstName" runat="server" />
                            <asp:HiddenField ID="hdnCrewDetails" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </div>
    </form>
</body>
</html>
