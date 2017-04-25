<%@ Page Title="Survey Certificate Authority" Language="C#" MasterPageFile="~/Surveys/SurveyMaster.master"
    AutoEventWireup="true" CodeFile="SurveyCertificateAuthority.aspx.cs" Inherits="SurveyCertificateAuthority" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
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
        .textbox-css1
        {
            font-family: Tahoma;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function showDivAddNewCategory() {
            document.getElementById("dvAddNewCategory").style.display = "block";
        }
        function closeDivAddNewCategory() {
            document.getElementById("dvAddNewCategory").style.display = "None";
        }

        function showDivAddNewCertificate() {
            document.getElementById("dvAddNewCertificate").style.display = "block";
        }
        function closeDivAddNewCertificate() {
            document.getElementById("dvAddNewCertificate").style.display = "None";
        }

        function showDivAddNewType() {
            document.getElementById("dvAddNewType").style.display = "block";
        }
        function closeDivAddNewType() {
            document.getElementById("dvAddNewType").style.display = "None";
        }
        $(document).ready(function () {
            $('#dvAddNewCategory').draggable();
        });
        $(document).ready(function () {
            $('#dvAddNewCertificate').draggable();
        });
        $(document).ready(function () {
            $('#dvAddNewType').draggable();
        });


        function validate() {
            if ($.trim($("input[type='text'][id$='txtAuthority_Name']").val()) == "") {
                $("input[type='text'][id$='txtAuthority_Name']").focus();
                alert("Enter Authority Name");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <div id="AccessMsgDiv" runat="server" style="color: Red; font-size: 14px; text-align: center;">
            You don't have sufficient privilege to access the requested page.</div>
        <div id="MainDiv" runat="server">
            <div style="font-family: Tahoma; font-size: 12px; width: 800px; height: 100%;">
                <asp:UpdatePanel ID="UpdatePanelCategory" runat="server">
                    <ContentTemplate>
                        <div style="text-align: center">
                            <asp:Panel runat="server" DefaultButton="btnFilter">
                                <table cellpadding="4" cellspacing="4" style="width: 60%;">
                                    <tr>
                                        <td style="text-align: left">
                                            Authority Name:&nbsp;<asp:TextBox ID="txtFltrAuthority" Width="250px" runat="server"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtFltrAuthority"
                                                WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                        </td>
                                        <td align="center" style="width: 5%">
                                            <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                                ImageUrl="~/Images/SearchButton.png" />
                                        </td>
                                        <td align="center" style="width: 5%">
                                            <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                                ImageUrl="~/Images/Refresh-icon.png" />
                                        </td>
                                        <td align="center" style="width: 5%">
                                            <asp:ImageButton ID="lnkAddNewAuthority" OnClientClick="showDivAddNewCategory();return false;"
                                                runat="server" ToolTip="Add New Certificate Authority" ImageUrl="~/Images/Add-icon.png" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                <asp:GridView ID="GridView_Authority" runat="server" AutoGenerateColumns="False"
                                    OnRowUpdating="GridView_Authority_RowUpdating" OnRowDeleting="GridView_Authority_RowDeleting"
                                    OnRowEditing="GridView_Authority_RowEditing" OnRowCancelingEdit="GridView_Authority_RowCancelEdit"
                                    DataKeyNames="ID" EmptyDataText="NO RECORDS FOUND" AllowSorting="True" CaptionAlign="Bottom"
                                    CellPadding="4" GridLines="None" Width="100%" CssClass="gridmain-css" OnSorting="GridView_Authority_Sorting"
                                    OnRowDataBound="GridView_Authority_OnRowDataBound">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblAuthority_NameHeader" runat="server" CommandName="Sort" CommandArgument="Authority"
                                                    ForeColor="Black">Authority Name</asp:LinkButton>
                                                <img id="Authority" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAuthority_Name" runat="server" Text='<%#Eval("Authority")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAuthority_Name" Font-Size="11px" MaxLength="50" runat="server"
                                                    Text='<%#Bind("Authority")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False" HeaderStyle-Width="100px">
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="ImgBtnAccept" runat="server" ImageUrl="~/images/accept.png"
                                                    CausesValidation="False" CommandName="Update" OnClientClick="return validate();"
                                                    ToolTip="Save" AlternateText="Update"></asp:ImageButton>
                                                <asp:ImageButton ID="ImgBtnCancel" runat="server" ImageUrl="~/images/reject.png"
                                                    CausesValidation="False" CommandName="Cancel" ToolTip="Cancel" AlternateText="Cancel">
                                                </asp:ImageButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                                    Visible='<%# uaEditFlag %>' CommandName="Edit" ToolTip="Update Authority" AlternateText="Edit">
                                                </asp:ImageButton>
                                                <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                                                    Visible='<%# uaDeleteFlage %>' CausesValidation="False" CommandName="Delete"
                                                    ToolTip="Delete Authority" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                    AlternateText="Delete"></asp:ImageButton>
                                                <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                    Height="16px" title="Information" Style="vertical-align: top; cursor: pointer;"
                                                    Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;LIB_Survey_CertificateAuthority&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="Load_AuthorityList" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:Panel ID="pnladd" runat="server" DefaultButton="btnSaveAndClose">
                <div id="dvAddNewCategory" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
                    border-style: solid; border-width: 1px; width: 450px; position: absolute; left: 40%;
                    top: 20%; z-index: 1; color: black">
                    <div class="header">
                        <h4>
                            Add New Authority</h4>
                    </div>
                    <div class="content">
                        <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                            <ContentTemplate>
                                <table border="0" style="width: 100%;" class="content">
                                    <tr>
                                        <td style="font-size: 11px; font-weight: bold">
                                            Authority Name:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAuthority" CssClass="textbox-css" Width="250px" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rqfAuthority" runat="server" ValidationGroup="save"
                                                ControlToValidate="txtAuthority" ErrorMessage="*" />
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtAuthority"
                                                ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{0,100}$" runat="server"
                                                ErrorMessage="Maximum 100 characters allowed." ValidationGroup="save"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="font-size: 11px; text-align: center;">
                                            <asp:Button ID="btnSaveAndAdd" CssClass="button-css" runat="server" Text="Save And Add New"
                                                ValidationGroup="save" OnClick="btnSaveAndAdd_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="btnSaveAndClose" CssClass="button-css" runat="server" Text="Save And Close"
                                                OnClick="btnSaveAndClose_Click" ValidationGroup="save" />&nbsp;&nbsp;
                                            <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </center>
</asp:Content>
