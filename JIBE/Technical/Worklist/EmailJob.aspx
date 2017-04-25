<%@ Page Title="Email Job" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="EmailJob.aspx.cs" Inherits="Technical_Worklist_EmailJob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".draggable").draggable();

            //var options = { serviceUrl: '../../UserControl/ADAutoCompleteHandler.ashx' };
            //$('.autocomplete').autocomplete(options);
            //$('[id$=txtTo]').autocomplete(options);

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
    
    <div id="content">
        <div id="mailHeader">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%; background-color: #E6E6E6; padding: 5px; border: 1px solid #dcdcdc;">
                        <tr>
                            <td rowspan="3">
                                <asp:ImageButton ID="ImgBtnSend" runat="server" ImageUrl="~/Images/sendmail.png"
                                    OnClick="ImgBtnSend_Click" />
                            </td>
                            <td rowspan="3">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Discardmail.png"
                                    OnClick="ImgBtnSend_Click" />
                            </td>
                            <td style="width: 70px">
                                <div id="lblTo" style="cursor: hand;" runat="server" onclick="javascript:$('#dvSelectAddress').show();">
                                    To ...</div>
                            </td>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtTo" runat="server" Style="width: 99%"  />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                CC
                            </td>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtCC" runat="server" Style="width: 99%"  />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Subject
                            </td>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtSubject" runat="server" Style="width: 99%" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:TextBox ID="txtMailBody" runat="server" Style="width: 99%" TextMode="MultiLine"
                                    Height="350px" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="dvSelectAddress" class="draggable" style="display: none; background-color: White;
        position: absolute; left: 350px; top: 60px; width: 350px; z-index: 1; padding: 2px;
        border: 1px solid #aabbdd;">
        <asp:UpdatePanel ID="UpdateAddress" runat="server">
            <ContentTemplate>
                <table style="border: 1px solid #aabbdd; padding: 2px;" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #aabbdd;">
                        <td>
                            Select User
                        </td>
                        <td style="text-align: right">
                            <div style="font-size: 11px; font-weight: bold; cursor: hand; padding: 2px; border: 1px solid outset;
                                text-align: center; width: 16px;" onclick="javascript:$('#dvSelectAddress').hide('slow');">
                                X</div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ListBox ID="lstUsers" runat="server" DataSourceID="ObjectDataSource_UserList"
                                DataTextField="USER_NAME" DataValueField="USERID" AppendDataBoundItems="True"
                                SelectionMode="Multiple" AutoPostBack="True" Height="200px" Width="99%">
                                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                            </asp:ListBox>
                            <asp:ObjectDataSource ID="ObjectDataSource_UserList" runat="server" SelectMethod="Get_UserList"
                                TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                                <SelectParameters>
                                    <asp:SessionParameter DefaultValue="0" Name="CompanyID" SessionField="UserCompanyID"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAddTo" runat="server" Text="To &gt;&gt;" OnClick="btnAddTo_Click" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSelectedIDsTo" runat="server" Width="270px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCC" runat="server" Text="CC &gt;&gt;" OnClick="btnCC_Click" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSelectedIDsCC" runat="server" Width="270px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_Click" Width="60px" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

