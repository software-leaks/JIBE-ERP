<%@ Page Title="Oil Majors Rules Library" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="OilMajorsRules.aspx.cs" Inherits="Crew_Libraries_OilMajorsRules" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .gridmain-css tr
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
            <div class="page-title">
                Oil Majors Rules Library
            </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFilter">
                                <table width="100%" cellpadding="4" cellspacing="4">
                                    <tr>
                                        <td align="right" style="width: 15%">
                                            Rule :&nbsp;
                                        </td>
                                        <td align="left" style="width: 30%">
                                            <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                                WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                        </td>
                                        <td align="center" style="width: 5%">
                                            <asp:ImageButton ID="btnFilter" ClientIDMode="Static" runat="server" TabIndex="0"
                                                OnClick="btnFilter_Click" ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                        </td>
                                        <td align="center" style="width: 5%">
                                            <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                                ImageUrl="~/Images/Refresh-icon.png" />
                                        </td>
                                        <td align="center" style="width: 5%">
                                            <asp:Image ImageUrl="~/Images/Add-icon.png" ID="ImgAdd" runat="server" Style="cursor: pointer;"
                                                ToolTip="Add new rule" ClientIDMode="Static" />
                                        </td>
                                        <td style="width: 5%">
                                            <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                                ImageUrl="~/Images/Exptoexcel.png" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvOilMajorsRules" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" OnRowDataBound="gvOilMajorsRules_RowDataBound" DataKeyNames="ID"
                                    CellPadding="0" CellSpacing="2" Width="100%" GridLines="both" OnSorting="gvOilMajorsRules_Sorting"
                                    AllowSorting="true" CssClass="gridmain-css">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblReasonHeader" Style="margin-left: 2px;" runat="server" CommandName="Sort"
                                                    CommandArgument="Rule_Name" ForeColor="Black">Rule</asp:LinkButton>
                                                <img id="Rule_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOilMajorName" Text='<%#Eval("Rule_Name")%>' rel='<%#Eval("ID")%>'
                                                    runat="server" Style="margin-left: 5px; color: Black;" />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Image ID="Edit" CssClass="edit" ImageUrl="~/Images/Edit.gif" ForeColor="Black"
                                                    ToolTip="Edit" runat="server" Height="16px" Width="16px" rel='<%#Eval("ID")%>'
                                                    Visible='<%# uaEditFlag %>' Style="cursor: pointer;" />
                                                <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" Visible='<%# uaDeleteFlage %>'
                                                    OnCommand="onDelete" OnClientClick="return confirm('Are you sure, you want to delete?')"
                                                    CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                    Height="16px" Width="16px"></asp:ImageButton>
                                                <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                    Height="16px" Width="16px" Style="vertical-align: top; cursor: pointer;" runat="server"
                                                    onclick='<%# "Get_Record_Information(&#39;CRW_LIB_CM_OilMajorsRules&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindOilMajorsRules" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title='<%=OperationMode %>' style="display: none; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 30%">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Rule&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:TextBox ID="txtOilMajorName" ClientIDMode="Static" CssClass="txtInput" MaxLength="200"
                                            Width="90%" runat="server"></asp:TextBox>
                                        <asp:HiddenField ClientIDMode="Static" ID="hdnOilMajorID" runat="server" Value="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" ClientIDMode="Static" OnClick="btnsave_OnClick" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", ".edit", function () {
                var Rel = parseInt($(this).attr("rel"));
                $("#hdnOilMajorID").val(parseInt($(this).attr("rel")));
                $("#txtOilMajorName").val($("span[rel='" + Rel + "']").text());
                showModal('divadd', false);
                $("#divadd_dvModalPopupTitle").text("Edit Rule");
            });

            $("body").on("click", "#ImgAdd", function () {
                if (this.id == "ImgAdd") {
                    $("#hdnOilMajorID").val(0);
                    $("#txtOilMajorName").val('');
                    //Select first item
                    showModal('divadd', false);
                    $("#divadd_dvModalPopupTitle").text("Add New Rule");
                }
                else {
                    $("#hdnOilMajorID").val(parseInt($(this).attr("rel")));
                    $("#txtOilMajorName").val($(this).text());
                    showModal('divadd', false);
                    $("#divadd_dvModalPopupTitle").text("Edit Rule");
                }

            });
            $("body").on("click", "#btnFilter", function () {
                if ($.trim($("#ctl00_MainContent_txtfilter").val()) == "Type to Search") {
                    $("#ctl00_MainContent_txtfilter").focus();
                    alert("Please type to search");
                    return false;
                }
            });

            $("body").on("click", "#btnsave", function () {
                if ($.trim($("#txtOilMajorName").val()) == "") {
                    $("#txtOilMajorName").val($.trim($("#txtOilMajorName").val()));
                    $("#txtOilMajorName").focus();
                    alert("Please enter rule");
                    return false;
                }
            });
        });
    </script>
</asp:Content>
