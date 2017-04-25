<%@ Page Title="Email Editor" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="EmailEditor.aspx.cs" Inherits="Crew_EmailEditor" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="../UserControl/ucEmailAttachment.ascx" TagName="ucEmailAttachment"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var isMailSent = 0;



        window.onbeforeunload = bunload;

        function bunload() {
            var dontleave = "";
            if (isMailSent == 0)
                return dontleave = " This mail has not been sent !";
            else
                window.close();

        }

        $(document).ready(function () {

            $(".draggable").draggable();

            //var options = { serviceUrl: '../../UserControl/ADAutoCompleteHandler.ashx' };
            //$('.autocomplete').autocomplete(options);
            //$('[id$=txtTo]').autocomplete(options);

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
    <div id="content">
        <div id="mailHeader">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table border="0" style="width: 100%; background-color: #E6E6E6; padding: 5px; border: 1px solid #dcdcdc;">
                        <tr>
                            <td rowspan="3" style="width: 50px">
                                <asp:ImageButton ID="ImgBtnSend" runat="server" ImageUrl="~/Images/sendmail.png"
                                    OnClick="ImgBtnSend_Click" />
                            </td>
                            <td rowspan="3">
                                <asp:ImageButton ID="ImgBtnDiscard" runat="server" ImageUrl="~/Images/Discardmail.png"
                                    OnClick="ImgBtnDiscard_Click" />
                            </td>
                            <td style="width: 70px">
                                <div id="lblTo" style="cursor: pointer;" runat="server" onclick="javascript:$('#dvSelectAddress').show();">
                                    To ...</div>
                            </td>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtTo" runat="server" Style="width: 99%" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                CC
                            </td>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtCC" runat="server" Style="width: 99%" />
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
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="margin-top: 3px; text-align: left; vertical-align: top; border: 1px solid #dcdcdc;
                background-color: #E6E6E6;">
                <asp:UpdatePanel ID="updAttach" UpdateMode="Always" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtTo" EventName="TextChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 200px;">
                                    <uc1:ucEmailAttachment ID="ucEmailAttachment1" OnUploadCompleted="LoadFiles" runat="server" />
                                </td>
                                <td>
                                    <asp:DataList ID="dlstAttachment" runat="server" BackColor="Transparent" ForeColor="Black"
                                        RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Table">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hplinkFileName" runat="server" NavigateUrl='<%# "../"+Eval("ATTACHMENT_PATH").ToString().Replace(@"\\server01\","").Replace(@"\", "/") %>'
                                                Target="_blank" Text='<%#Eval("ATTACHMENT_NAME")%>'></asp:HyperLink>
                                            <asp:ImageButton ID="imgbtnDelete" Width="12" Height="12" runat="server" AlternateText="Delete"
                                                OnClick="imgbtnDelete_Click" ImageUrl="~/Images/Delete.png" CommandArgument='<%#Eval("ID")+","+ Eval("ATTACHMENT_PATH") %>' />
                                        </ItemTemplate>
                                        <SeparatorTemplate>
                                            &nbsp;;&nbsp;
                                        </SeparatorTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="margin-top: 5px; height: 550px;">
                <CKEditor:CKEditorControl ID="txtMailBody" runat="server"></CKEditor:CKEditorControl>
            </div>
        </div>
    </div>
    <div id="dvSelectAddress" class="draggable" style="display: none; background-color: White;
        position: absolute; left: 350px; top: 200px; z-index: 1; padding: 2px; border: 1px solid #aabbdd;">
        <asp:UpdatePanel ID="UpdateAddress" runat="server">
            <ContentTemplate>
                <table style="border: 1px solid #aabbdd; padding: 2px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="4" align="right" style="background-color: #B5CDE1">
                            <div style="font-size: 11px; font-weight: bold; cursor: pointer; padding: 2px; border: 1px solid outset;
                                text-align: right; width: 16px; color: Red" onclick="javascript:$('#dvSelectAddress').hide('slow');">
                                X</div>
                        </td>
                    </tr>
                    <tr style="background-color: #aabbdd; color: Black">
                        <td colspan="2">
                            Select User
                        </td>
                        <td colspan="2">
                            Select Vessel
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ListBox ID="lstUsers" runat="server" DataSourceID="ObjectDataSource_UserList"
                                DataTextField="USER_NAME" DataValueField="USERID" AppendDataBoundItems="True"
                                SelectionMode="Multiple" AutoPostBack="false" Height="200px" Width="99%">
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
                        <td>
                            <asp:ListBox ID="lstVessel" runat="server" DataTextField="Vessel_Name" DataValueField="Vessel_ID"
                                AppendDataBoundItems="True" SelectionMode="Multiple" AutoPostBack="false" Height="200px"
                                Width="99%">
                                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                            </asp:ListBox>
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
                                        <asp:TextBox ID="txtSelectedIDsTo" Visible="false" runat="server" Width="270px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCC" runat="server" Text="CC &gt;&gt;" OnClick="btnCC_Click" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSelectedIDsCC" runat="server" Visible="false" Width="270px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAddVslTo" CommandArgument="TO" runat="server" Text="To &gt;&gt;"
                                            OnClick="btnAddVslTo_Click" />
                                    </td>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAddvslCc" CommandArgument="CC" runat="server" Text="CC &gt;&gt;"
                                                OnClick="btnAddVslTo_Click" />
                                        </td>
                                    </tr>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnOK" runat="server" Visible="false" Text="OK" OnClientClick="javascript:$('#dvSelectAddress').hide('slow');return false;"
                                Width="60px" />
                            <asp:Button ID="btnCancel" runat="server" Visible="false" Text="Cancel" OnClientClick="javascript:$('#dvSelectAddress').hide('slow'); return false;" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
