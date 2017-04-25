<%@ Page Title="Alarm Effect" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PMS_AlarmEffect.aspx.cs" Inherits="Technical_PMS_PMS_AlarmEffect" %>

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
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        
        .linkbtn
        {
            border-right: wheat 1px solid;
            border-top: wheat 1px solid;
            font-weight: bold;
            border-left: wheat 1px solid;
            color: White;
            border-bottom: wheat 1px solid;
            background-color: white;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function Divaddnewlink() {
            document.getElementById("divadd").style.display = "block";
        }



        function validation() {

            if (document.getElementById("ctl00_MainContent_txtModalEffect").value.trim() == "") {
                alert("Please enter a effect name.");
                document.getElementById("ctl00_MainContent_txtModalEffect").focus();
                return false;
            }
            return true;
        }


       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
            <div class="page-title">
                PMS Alarm Effect
            </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlsearch" runat="server" DefaultButton="btnFilter">
                            <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                                <table width="100%" cellpadding="1" cellspacing="4">
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            Effect :&nbsp;
                                        </td>
                                        <td align="left" style="width: 30%">
                                            <asp:TextBox ID="txtEffect" runat="server" Width="100%"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtEffect"
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
                                            <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Effect" OnClick="ImgAdd_Click"
                                                ImageUrl="~/Images/Add-icon.png" />
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
                            <div>
                                <asp:GridView ID="GridViewEffect" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" DataKeyNames="EffectID" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Effect" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50%">
                                            <HeaderTemplate>
                                                <div style="padding-left: 30px;">
                                                    <asp:Label ID="lblCurrencyCodeHeader" runat="server" CommandArgument="EffectID" ForeColor="Black">Effect</asp:Label></div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="padding-left: 20px;">
                                                    <asp:LinkButton ID="lblUnitName" runat="server" CommandArgument='<%#Eval("EffectID")%>'
                                                        Text='<%# DataBinder.Eval(Container,"DataItem.EffectName") %>' Style="color: Black"
                                                        OnCommand="onUpdate"></asp:LinkButton></div>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[EffectID]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[EffectID]")%>' ForeColor="Black" ToolTip="Delete"
                                                                ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;TEC_LIB_ALARM_EFFECT&#39;,&#39;EffectID="+Eval("EffectID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindEffect" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <asp:Panel ID="pnladd" runat="server" DefaultButton="btnsave">
                            <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                                font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 30%;">
                                <table width="98%" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td align="right" style="width: 30%">
                                            Effect&nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtModalEffect" CssClass="txtInput" Width="98%" runat="server" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="font-size: 11px; text-align: center; border-style: solid;
                                            border-color: Silver; border-width: 1px; background-color: #d8d8d8;">
                                            <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validation();"
                                                OnClick="btnsave_Click" />
                                            <asp:TextBox ID="txtEffectID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 0px solid #cccccc;
                                                background-color: #FDFDFD">
                                            </div>
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
    </center>
</asp:Content>
