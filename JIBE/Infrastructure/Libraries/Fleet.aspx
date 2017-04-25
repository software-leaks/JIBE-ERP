<%@ Page Title="Fleet" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Fleet.aspx.cs" Inherits="Fleet" %>

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
    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("ctl00_MainContent_ddlFleet_Vessel_Manager").value == "0") {
                alert("Please select vessel manager.");
                document.getElementById("ctl00_MainContent_ddlFleet_Vessel_Manager").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtFleetName").value.trim() == "") {
                alert("Fleet name is a mandatory field.");
                document.getElementById("ctl00_MainContent_txtFleetName").focus();
                return false;
            }

            return true;
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <center>
    
          <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                  <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 85%;
            height: 100%;">
           <div class="page-title">
             Fleet
          </div>
            <div style="height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 20%">
                                        Name &nbsp;:&nbsp;
                                    </td>
                                    <td style="text-align: left; width: 25%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="right" style="width: 15%">
                                        Vessel Manager&nbsp;:&nbsp;
                                    </td>
                                    <td style="width: 20%">
                                        <asp:DropDownList ID="ddlFleetVslMgrFilter" Width="100%" runat="server"/>
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
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Fleet" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvFleet" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvFleet_RowDataBound" DataKeyNames="FLEETCODE" CellPadding="1"
                                    CellSpacing="0" Width="100%" GridLines="both" OnSorting="gvFleet_Sorting" AllowSorting="true">
                                   <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                  <RowStyle CssClass="RowStyle-css" />
                                  <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                   <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblFleetCodeHeader" runat="server" CommandName="Sort" CommandArgument="FLEETCODE"
                                                    ForeColor="Black">Code&nbsp;</asp:LinkButton>
                                                <img id="FLEETCODE" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblFleetCode" runat="server" Text='<%#Eval("FLEETCODE")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblFleetNameHeader" runat="server" CommandName="Sort" CommandArgument="NAME"
                                                    ForeColor="Black">NAME&nbsp;</asp:LinkButton>
                                                <img id="NAME" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblFleetName" Style="color: Black" runat="server" Text='<%#Eval("NAME")%>'
                                                    CommandArgument='<%#Eval("FLEETCODE")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Suptd. Email">
                                            <HeaderTemplate>
                                                Superintendent Email
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSuptdEmail" runat="server" Text='<%#Eval("Super_MailID")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tech. Team Email">
                                            <HeaderTemplate>
                                                Technical Assistance Email
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTechTeamEmail" runat="server" Text='<%#Eval("TechTeam_MailID")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VesselManager">
                                            <HeaderTemplate>
                                                Vessel Manager
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVesselManager" runat="server" Text='<%#Eval("VesselManager")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[FLEETCODE]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[FLEETCODE]")%>' ForeColor="Black" ToolTip="Delete"
                                                                ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;LIB_FLEETS&#39;,&#39;FleetCode="+Eval("FLEETCODE").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindFleet" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 30%;">
                            <div class="content">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <table width="98%" cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Vessel Manager &nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlFleet_Vessel_Manager" Width="82%" runat="server" CssClass="txtInput" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 25%">
                                                    Fleet Name &nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtFleetName" runat="server" Width="80%" MaxLength="100" CssClass="txtInput"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Suptd. Email &nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSuptdEmail" runat="server" Width="80%" CssClass="txtInput"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    Tech. Team Email &nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTechTeamEmail" runat="server" Width="80%" CssClass="txtInput"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="center" style="font-size: 11px; border-width: 1px">
                                                    <asp:Button ID="btnSaveFleet" runat="server" Text="Save" OnClientClick="return validation();"
                                                        OnClick="btnSaveFleet_Click" />&nbsp;&nbsp;
                                                    <asp:TextBox ID="txtFleetID" runat="server" Visible="false"></asp:TextBox>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
