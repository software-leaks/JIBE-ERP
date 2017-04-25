<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SlopChestDetails.aspx.cs" Inherits="Purchase_SlopChestDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
   <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;">
            <div class="page-title">
                Slop Chest
            </div>
            <div style="width: 100%; height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePaneCompany" runat="server">
                    <ContentTemplate>
                      <%--  <div class="subHeader" style="display: none; position: relative; right: 0px">
                            <asp:Button ID="btnHiddenSubmit" runat="server" Text="btnHiddenSubmit" OnClick="btnHiddenSubmit_Click" />
                        </div>--%>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 70px; width: 100%">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                   
                                    <td align="right" style="width: 10%">
                                        Vessel  :&nbsp;
                                    </td>
                                    <td style="width: 15%">
                                        <asp:DropDownList ID="ddlVesselFilter" AppendDataBoundItems="true" runat="server"
                                            Width="100%" AutoPostBack="True" >
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        <%--<asp:ImageButton ID="imbAddMaker" ImageUrl="~/Images/AddMaker.png" Height="12px"
                                            ToolTip="Add Company Type" runat="server" OnClientClick="return OnAddMaker();"
                                            Visible="false" />--%>
                                    </td>
                                    <td align="right" style="width: 10%">
                                        Year :&nbsp;
                                    </td>
                                    <td style="width: 15%">
                                        <asp:DropDownList ID="ddlYearFilter" AppendDataBoundItems="true" runat="server"
                                            Width="200px" AutoPostBack="True" >
                                        </asp:DropDownList>
                                    </td>
                                     <td align="right" style="width: 15%">
                                        Month :&nbsp;
                                    </td>
                                    <td align="left" style="width: 15%">
                                      <asp:DropDownList ID="ddlMonth" AppendDataBoundItems="true" runat="server"
                                            Width="200px" AutoPostBack="True" >
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <%--<asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Refresh-icon.png"
                                            OnClick="btnRefresh_Click" ToolTip="Refresh" />--%>
                                            <asp:Button ID="btnRefresh" runat="server"  OnClick="btnRefresh_Click" ToolTip="Refresh" Text="Clear Filter"/>
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <%--<asp:ImageButton ID="ImgAdd" runat="server" ImageUrl="~/Images/Add-icon.png" OnClick="ImgAdd_Click"
                                            ToolTip="Add New Checklist" />--%>
                                            <%--<asp:Button  ID="ImgAdd" runat="server" OnClick="ImgAdd_Click"
                                            ToolTip="Add New Checklist" Text="Add"/>--%>
                                    </td>
                                    <td style="width: 30px">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" Visible="False" />
                                    </td>
                                </tr>
                                
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="GridViewSlopchest" runat="server" CellPadding="1" OnRowDataBound="GridViewSlopchest_RowDataBound"
                                    OnSorting="GridViewSlopchest_Sorting" EmptyDataText="NO RECORDS FOUND!" AllowSorting="True"
                                    AutoGenerateColumns="False" Width="100%" DataKeyNames="Slopchest_ID">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>                                     
                                   <%--     <asp:TemplateField HeaderText="Vessel Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblNameHeader" runat="server" CommandName="Sort" CommandArgument="CheckList_Name"
                                                    ForeColor="Black">Vessel&nbsp;</asp:LinkButton>
                                                <img id="Vessel_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                              <asp:Label ID="lblVessel_Name" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="220px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSlopchest_Date" runat="server" Text='<%#Eval("Slopchest_Date")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Crew">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCrew" runat="server" Text='<%#Eval("Crew")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem" runat="server" Width="200px" Text='<%# Bind("RefCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="280px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrice" runat="server" Width="200px" Text='<%# Bind("Price") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="280px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantity" runat="server" Width="200px" Text='<%# Bind("Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="280px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Final Amt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFinalAmount" runat="server" Width="200px" Text='<%# Bind("FinaAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="280px" CssClass="PMSGridItemStyle-css">
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
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;PURC_DTL_Slopchest&#39;,&#39;Slopchest_ID="+Eval("Slopchest_ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                        <td>
                                                          </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindCompany" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="dvIframe" style="display: none; width: 600px;" title=''>
            <iframe id="Iframe" src="" frameborder="0" style="height: 295px; width: 100%"></iframe>
        </div>
    </center>
</asp:Content>

