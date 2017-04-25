﻿<%@ Page Title="Survey Document Type" Language="C#" MasterPageFile="~/Surveys/SurveyMaster.master"
    AutoEventWireup="true" CodeFile="SurveyDocumentType.aspx.cs" Inherits="Surveys_SurveyDocumentType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function validation() {

            if (document.getElementById("ctl00_MainContent_txtDocumentType").value.trim() == "") {
                alert("Please enter Document Type.");
                document.getElementById("ctl00_MainContent_txtDocumentType").focus();
                return false;
            }

            return true;
        }
    </script>
    <style type="text/css">
        .RowStyle-css{min-height:25px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <div id="AccessMsgDiv" runat="server" style="color: Red; font-size: 14px; text-align: center;">
            You don't have sufficient privilege to access the requested page.</div>
        <div id="MainDiv" runat="server">
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
            <div style="font-family: Tahoma; font-size: 12px; width: 800px; height: 100%;">
                <div style="height: 650px; width: 100%; color: Black;">
                    <asp:UpdatePanel ID="UpdUserType" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlsearch" runat="server" DefaultButton="btnFilter">
                                <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                                    <table width="100%" cellpadding="4" cellspacing="4">
                                        <tr>
                                            <td align="right" style="width: 10%">
                                                Document Type:&nbsp;
                                            </td>
                                            <td align="left" style="width: 30%">
                                                <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
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
                                                <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Survey Document Type"
                                                    OnClick="ImgAdd_Click" ImageUrl="~/Images/Add-icon.png" />
                                            </td>
                                            <td style="width: 5%">
                                                <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                                    ImageUrl="~/Images/Exptoexcel.png" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>

                            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                    <asp:GridView ID="GridView_DocumentType" runat="server" EmptyDataText="NO RECORDS FOUND"
                                        AutoGenerateColumns="False" OnRowDataBound="GridView_DocumentType_RowDataBound"
                                        DataKeyNames="ID" CellPadding="1" CellSpacing="0" Width="100%" 
                                        OnSorting="GridView_DocumentType_Sorting" AllowSorting="true">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                        <PagerStyle CssClass="PMSPagerStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Document Type" HeaderStyle-HorizontalAlign="left">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblDocumentType_NameHeader" runat="server" CommandName="Sort"
                                                        CommandArgument="DocumentType" ForeColor="Black">Document Type</asp:LinkButton>
                                                        <img id="DocumentType" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblDocumentType_Name" runat="server" CommandArgument='<%#Eval("ID")%>'
                                                        Text='<%#Eval("DocumentType")%>' OnCommand="onUpdate" Style="color: Black;margin-left:5px;"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    Action
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                        Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                        ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                    <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                        Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure you want to delete?')"
                                                        CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                        Height="16px"></asp:ImageButton>
                                                    <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                        Height="16px" title="Information" Style="vertical-align: top; cursor: pointer;"
                                                        Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;LIB_SurveyDocumentType&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindSurveyDocType" />
                                    <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <asp:Panel ID="pnladd" runat="server" DefaultButton="btnsave">
                                <div id="divadd" title="<%= OperationMode %>" style="display: none; font-family: Tahoma;
                                    text-align: left; font-size: 12px; color: Black; width: 31%">
                                    <table width="100%" cellpadding="2" cellspacing="2">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 10%">
                                                Document Type:<asp:Label runat="server" ID="lblstar" ForeColor="Red">*</asp:Label>
                                            </td>
                                            <td align="left" style="width: 30%">
                                                <asp:TextBox ID="txtDocumentType" CssClass="txtInput" MaxLength="255" Width="190px"
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="font-size: 11px; text-align: center;">
                                                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" OnClientClick="return validation();" />
                                                <asp:TextBox ID="txtDocTypeID" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                                * Mandatory fields
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="ImgExpExcel" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </center>
</asp:Content>
