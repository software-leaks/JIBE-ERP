<%@ Page Title="OnBoard Staff Limit" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="OnBoard_Staff_Limit.aspx.cs" Inherits="Crew_Libraries_OnBoard_Staff_Limit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("ctl00_MainContent_ddlVessel").value == "0") {
                alert("Please select vessel name.");
                document.getElementById("ctl00_MainContent_ddlVessel").focus();
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    

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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 80%;
            height: 100%;">
             <div class="page-title">
             On Board Staff Limit
            </div>
            <div style="color: Black;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 10px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table cellspacing="1" cellspacing="1" width="100%">
                                <tr>
                                    <td align="right" style="width:10%">
                                        Fleet : &nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width:15%">
                                        <asp:DropDownList ID="ddlFleet" CssClass="txtInput" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                            Style="width: 200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                     <td align="right" style="width:15%">
                                        Vessel Name : &nbsp;&nbsp;
                                    </td>
                                 <td align="left" style="width:15%">
                                        <asp:DropDownList ID="ddlVessel" CssClass="txtInput" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"
                                            Style="width: 200px">
                                        </asp:DropDownList>
                                    </td>

                                    <td style="width:40%">
                                    
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <asp:GridView ID="gvRankLimit" DataKeyNames="Rank_ID" runat="server" AutoGenerateColumns="False"
                                CellPadding="2" AllowPaging="False" PageSize="20" Width="100%" ShowFooter="false"
                                EmptyDataText="No Record Found" CaptionAlign="Bottom" GridLines="None" ForeColor="#333333"
                                CssClass="gridmain-css">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />

                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S/N" HeaderStyle-HorizontalAlign="Left" Visible="false" >
                                        <ItemTemplate>
                                            <%# ((GridViewRow)Container).RowIndex + 1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rank ID" HeaderStyle-HorizontalAlign="Left" Visible="false" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRankID" runat="server" Text='<%# Eval("Rank_ID")%>' />                                            
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rank Name" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRankName" runat="server" Text='<%# Eval("Rank_Name")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rank Short Name" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRankShortName" runat="server" Text='<%# Eval("Rank_Short_Name")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Maximum Limit" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMaxLimit" Width="40px" CssClass="txtInput" runat="server" Text='<%# Eval("MaxLimit")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Minimum Limit" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMinLimit" Width="40px" CssClass="txtInput" runat="server" Text='<%# Eval("MinLimit")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div style="text-align: center">
                            <asp:Button ID="btnSave" Text="Save" runat="server" OnClientClick="return validation();"
                                OnClick="btnSave_Click" /></div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
