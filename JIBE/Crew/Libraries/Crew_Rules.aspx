<%@ Page Title="Crew Rule" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Crew_Rules.aspx.cs" Inherits="Crew_Rules" %>

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
    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("ctl00_MainContent_txtRule").value.trim() == "") {
                alert("Please enter Rule.");
                document.getElementById("ctl00_MainContent_txtRule").focus();
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
    Crew Rules    
    </div>
    <center>    
 <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                   <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdRule" runat="server">
            <ContentTemplate>
                <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                    height: 100%;">
                    <table cellpadding="1" cellspacing="2" width="100%">
                        
                        <tr>
                            <td>
                                <div style="padding-top: 5px; padding-bottom: 5px; height: 30px; width: 100%;background-color:#F2F2F2">
                                    <table width="100%" cellpadding="4" cellspacing="4">
                                        <tr>
                                            <td align="right" style="width: 10%">
                                                Rules:&nbsp;
                                            </td>
                                            <td align="left" style="width: 30%">
                                                <asp:TextBox ID="txtfilter" runat="server" Width="100%" CssClass="txtInput"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                                    WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                            </td>
                                            <td align="center" style="width: 5%">
                                                <asp:ImageButton ID="btnFilter" runat="server" TabIndex="0" OnClick="btnFilter_Click"
                                                    ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                            </td>
                                            <td align="center" style="width: 5%">
                                                <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                                    ImageUrl="~/Images/Refresh-icon.png" />
                                            </td>
                                            <td align="center" style="width: 5%">
                                                <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Rule" OnClick="ImgAdd_Click"
                                                    ImageUrl="~/Images/Add-icon.png" />
                                            </td>
                                            <td style="width: 5%">
                                                <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                                    ImageUrl="~/Images/Exptoexcel.png" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td  style="font-size: 15px;color:Black;background-color:#F2F2F2;font-family: Verdana;">
                             
                                Rule Assignment to Vessel
                      
                            </td>
                            <td  style="font-size: 15px;color:Black;background-color:#F2F2F2;font-family: Verdana;">
                            
                                Rule Assignment to Rank
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="height: 650px; width: 400px; color: Black;">
                                    <div style="margin-left: auto; margin-right: auto; text-align: left;">
                                        <div style="height: 600px; width: 400px; color: Black; overflow: scroll">
                                            <asp:GridView ID="gvCrewRules" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                                OnRowDataBound="gvCrewRules_RowDataBound" DataKeyNames="RULE_ID" CellPadding="1"
                                                CellSpacing="0" Width="100%" GridLines="both" OnSorting="gvCrewRules_Sorting"
                                                AllowSorting="true">
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                               <RowStyle CssClass="RowStyle-css" />
                                               <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                                <SelectedRowStyle BackColor="#FFFFCC" />
                                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Rule ID">
                                                        <ItemTemplate>
                                                            <%# Eval("RULE_ID")%>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lblRuleNameHeader" runat="server" CommandName="Sort" CommandArgument="Description"
                                                                ForeColor="Black">Crew Rules&nbsp;</asp:LinkButton>
                                                            <img id="Description" runat="server" visible="false" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblDescription" runat="server" Text='<%#Eval("Description")%>'
                                                                Style="color: Black" CommandArgument='<%#Eval("RULE_ID")%>' CommandName="Select"
                                                                OnCommand="onSelect"></asp:LinkButton>
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
                                                            <table cellpadding="2" cellspacing="2">
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                            Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[RULE_ID]")%>' ForeColor="Black"
                                                                            ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                            Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                            CommandArgument='<%#Eval("[RULE_ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                            Height="16px"></asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindCrewRule" />
                                        </div>
                                    </div>
                                    <div id="divadd" title="<%= OperationMode %>" style="display: none; font-family: Tahoma;
                                        text-align: left; font-size: 12px; color: Black; width: 25%">
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
                                                <td align="right" style="width: 5%">
                                                    Rule &nbsp;:&nbsp;
                                                </td>
                                                <td style="color: #FF0000; width: 1%" align="right">
                                                    *
                                                </td>
                                                <td align="left" style="width: 25%">
                                                    <asp:TextBox ID="txtRule" TextMode="MultiLine" Height="80" CssClass="txtInput" MaxLength="100"
                                                        Width="90%" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="font-size: 11px; text-align: center;">
                                                    <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validation();"
                                                        OnClick="btnsave_Click" />
                                                    <asp:TextBox ID="txtRuleID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                                    * Mandatory fields
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align: top">
                                <div style="height: 600px; width: 400px; color: Black; overflow: scroll">
                                    <asp:GridView ID="gvVesselRule" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                        CellPadding="1" CellSpacing="0" Width="100%" GridLines="both" AllowSorting="true">
                                       <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                       <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <PagerStyle CssClass="PMSPagerStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Vessel Name">
                                                <ItemTemplate>
                                                    <%# Eval("Vessel_Name")%>
                                                    <asp:Label ID="lblVesselID" Visible="false" runat="server" Text='<%# Eval("Vessel_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Short Name">
                                                <ItemTemplate>
                                                    <%# Eval("Vessel_Short_Name")%>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rule Apply">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkVesselFlag" runat="server" Checked='<%#Eval("RuleApplayFlag").ToString() == "1" ? true : false %>' />
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                            <td style="vertical-align: top">
                                <div style="height: 600px; width: 400px; color: Black; overflow: scroll">
                                    <asp:GridView ID="gvRankRules" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                        CellPadding="1" CellSpacing="0" Width="100%" GridLines="both" AllowSorting="true">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <PagerStyle CssClass="PMSPagerStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Rank Name">
                                                <ItemTemplate>
                                                    <%# Eval("Rank_Name")%>
                                                    <asp:Label ID="lblRankID" runat="server" Visible="false" Text='<%# Eval("RankID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Short Name">
                                                <ItemTemplate>
                                                    <%# Eval("Rank_Short_Name")%>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rule Apply">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRankFlag" runat="server" Checked='<%#Eval("RuleApplayFlag").ToString() == "1" ? true : false %>' />
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="color:Blue;font-style:normal;font-family:Verdana;font-size:smaller;">
                              Click on rules to View / Assignment crew rule to Vessel and Rank.
                            </td>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnSaveAssignment" runat="server" Text="Save Assignment" Width="220"
                                    Height="30px" OnClick="btnSaveAssignment_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ImgExpExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </center>
</asp:Content>
