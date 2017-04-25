<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Schools.aspx.cs" Inherits="Crew_Schools" %>

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
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .ajax__tab_tab
        {
            min-width: 110px;
        }
        .ajax__tab_body
        {
            min-height: 600px;
        }
        .gridmain-css tr
        {
            height: 25px;
        }
        .gridmain-css tr:hover
        {
            background-color: #FEECEC;
        }
        .page-title
        {
            line-height: 18px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        School
    </div>
    <center>
        <asp:UpdatePanel ID="UpdUserType" runat="server">
            <ContentTemplate>
                <div style="width: 800px; text-align: center;">
                    <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                        <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFilter">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 10%">
                                        School :&nbsp;
                                    </td>
                                    <td align="left" style="width: 30%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="center" style="width: 1%">
                                        <asp:ImageButton ID="btnFilter" runat="server" TabIndex="0" OnClick="btnFilter_Click"
                                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 1%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 1%">
                                        <asp:Image ImageUrl="~/Images/Add-icon.png" ID="ImgAdd" runat="server" Style="cursor: pointer;"
                                            ToolTip="Add New School" ClientIDMode="Static" />
                                    </td>
                                    <td style="width: 3%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div style="margin-left: auto; margin-right: auto; text-align: center;">
                        <div>
                            <asp:GridView ID="gvSchool" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                OnRowDataBound="gvSchool_RowDataBound" DataKeyNames="ID" CellPadding="0" CellSpacing="2"
                                Width="100%" GridLines="both" OnSorting="gvSchool_Sorting" AllowSorting="true"
                                CssClass="gridmain-css" PageSize="20">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="School">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblReasonHeader" Style="margin-left: 2px;" runat="server" CommandName="Sort"
                                                CommandArgument="School" ForeColor="Black">School</asp:LinkButton>
                                            <img id="School" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSchool" Text='<%#Eval("School")%>' rel='<%#Eval("ID").ToString() %>'
                                                runat="server" Style="margin: 5px; color: Black;" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="20%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="60%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Image ID="Edit" CssClass="edit" ImageUrl="~/Images/Edit.gif" ForeColor="Black"
                                                ToolTip="Edit" runat="server" rel='<%#Eval("ID").ToString() %>' Height="16px"
                                                Width="16px" Visible='<%# uaEditFlag %>' Style="cursor: pointer;" />
                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" Visible='<%# uaDeleteFlage %>'
                                                OnCommand="onDelete" OnClientClick="return confirm('Are you sure, you want to delete?')"
                                                CommandArgument='<%#Eval("[ID]")%>' ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                Height="16px" Width="16px"></asp:ImageButton>
                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                Height="16px" Style="vertical-align: top; cursor: pointer;" Width="16px" runat="server"
                                                onclick='<%# "Get_Record_Information(&#39;CRW_LIB_CD_School&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindSchool" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                        </div>
                        <br />
                    </div>
                    <div id="divadd" title='Add/Edit Race' style="display: none; font-family: Tahoma;
                        text-align: left; font-size: 12px; color: Black; width: 30%">
                        <table width="100%" cellpadding="2" cellspacing="2">
                            <tr>
                                <td align="right" style="width: 20%">
                                    School&nbsp;:&nbsp;
                                </td>
                                <td style="color: #FF0000; width: 1%" align="right">
                                    *
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSchool" ClientIDMode="Static" CssClass="txtInput" MaxLength="200"
                                        Width="250px" runat="server"></asp:TextBox>
                                    <asp:HiddenField ClientIDMode="Static" ID="hdnSchoolID" runat="server" Value="0" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                    <asp:Button ID="btnsave" Width="75px" runat="server" Text="Save" ClientIDMode="Static"
                                        OnClick="btnsave_OnClick" />
                                    <input type="button" name="Cancel" value="Cancel" id="btnCancel" style="width: 75px;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ImgExpExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </center>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", ".edit", function () {
                var Rel = parseInt($(this).attr("rel"));
                $("#hdnSchoolID").val(parseInt($(this).attr("rel")));
                $("#txtSchool").val($("span[rel='" + Rel + "']").text());
                showModal('divadd', false);
                $("#divadd_dvModalPopupTitle").text("Add/Edit School");
            });

            $("body").on("click", "#ImgAdd", function () {
                if (this.id == "ImgAdd") {
                    $("#hdnSchoolID").val(0);
                    $("#txtSchool").val('');
                    showModal('divadd', false);
                    $("#divadd_dvModalPopupTitle").text("Add/Edit School");
                }
                else {
                    $("#hdnSchoolID").val(parseInt($(this).attr("rel")));
                    $("#txtSchool").val($(this).text());
                    showModal('divadd', false);
                    $("#divadd_dvModalPopupTitle").text("Add/Edit School");
                }
            });

            $("body").on("click", "#btnsave", function () {
                if ($.trim($("#txtSchool").val()) == "") {
                    $("#txtSchool").val('');
                    $("#txtSchool").focus();
                    alert("Please enter School");
                    return false;
                }
            });

            $("body").on("click", "#btnCancel", function () {
                $("#closePopupbutton").click();
            });
        });
    </script>
</asp:Content>
