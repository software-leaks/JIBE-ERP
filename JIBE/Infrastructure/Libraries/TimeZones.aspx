<%@ Page Title="Time Zones" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TimeZones.aspx.cs" Inherits="TimeZones" %>

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
    
    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("ctl00_MainContent_txtTimeZone").value == "") 
            {
                alert("Please enter Time Zone Name.");
                document.getElementById("ctl00_MainContent_txtTimeZone").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_txtBaseUtc").value.trim() == "") 
            {
                alert("Please enter Base UTC Offset.");
                document.getElementById("ctl00_MainContent_txtBaseUtc").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_txtBaseUtc").value.trim() != "") {

                if (CheckTimezoneFormat() == false) {
                   // document.getElementById("ctl00_MainContent_txtBaseUtc").value = '';
                    document.getElementById("ctl00_MainContent_txtBaseUtc").focus();
                    return false;

                }
                else 
                {
                    return true;
                }
            }
            return true;
        }
       
        function Validate_Digit(evt) {
            try {
                Browser = navigator.appName
                Net = Browser.indexOf("Netscape")
                Micro = Browser.indexOf("Microsoft")
                
                var nbr = (window.event) ? event.keyCode : evt.which;
                if (!((nbr >= 48 && nbr <= 57 || nbr == 58 || nbr == 43 || nbr == 45))) 
                {
                    if (Net >= 0) {
                        if (nbr != 8) {
                            evt.preventDefault();
                        }
                    }
                    else if (Micro >= 0) {
                        window.event.keyCode = 0;
                    }
                }
            }
            catch (err) {
                alert(err.description)
            }
        }
       
     
    </script>
    <script type="text/javascript">
            function CheckTimezoneFormat() {
            var chkzone = document.getElementById("ctl00_MainContent_txtBaseUtc").value; //"+05:30:00"
            if (chkzone.match(/^(?:Z|[+-]?(?:2[0-3]|[01][0-9]):[0-5][0-9]:[0-5][0-9])$/))//(chkzone.match(/^(Z|[+-](?:2[0-3]|[01]?[0-9])(?::?(?::[0-23]?[0-9]))?)$/)) 
            {
                return true;
            }
            else 
            {
                alert("Base UTC Offset is incorrect. Please enter in range between +/-00:00:00 and +/-23:59:59");
                return false;

            }
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
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
             <div class="page-title">
            Time Zones
           </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdTimeZone" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 8%">
                                       Time Zone:&nbsp;
                                    </td>
                                    <td align="left" style="width: 30%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="100%" ></asp:TextBox>
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
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New TimeZone" OnClick="ImgAdd_Click"
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
                                <asp:GridView ID="gvTimeZone" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvTimeZone_RowDataBound" DataKeyNames="ID" CellPadding="1"
                                    CellSpacing="0" Width="100%" GridLines="both" OnSorting="gvTimeZone_Sorting"
                                    AllowSorting="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                   <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblTimeZoneHeader" runat="server" CommandName="Sort" CommandArgument="ID"
                                                    ForeColor="Black">ID&nbsp;</asp:LinkButton>
                                                <img id="ID" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblTimeZoneID" runat="server" Text='<%#Eval("ID")%>'
                                                    Style="color: Black" CommandArgument='<%#Eval("ID")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px"  CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Display Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblDisplayName" runat="server" CommandName="Sort" CommandArgument="DisplayName"
                                                    ForeColor="Black">Time Zone&nbsp;</asp:LinkButton>
                                                <img id="DisplayName" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblDisplayName" runat="server" Text='<%#Eval("DisplayName")%>'
                                                    Style="color: Black" CommandArgument='<%#Eval("ID")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Base UTC Offset">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbUtc" runat="server" Text='<%#Eval("BaseUtcOffSet")%>'
                                                    Style="color: Black" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Default TimeZone">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblDefaultTimeZone" runat="server" CommandName="Sort" CommandArgument="DefaultTimeZone"
                                                    ForeColor="Black">Default Time Zone&nbsp;</asp:LinkButton>
                                                <img id="DefaultTimeZone" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDefaultTimeZone" runat="server" Text='<%#Eval("DefaultTimeZone")%>'
                                                    Style="color: Black" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
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
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>                                                        
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="Load_TimeZones" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display:none ; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 30%">
                            <table width="100%" cellpadding="2" cellspacing="2">

                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <tr>
                                        <td align="left" style="width: 30%">
                                            Time Zone&nbsp;:&nbsp;
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtTimeZone" runat="server" CssClass="txtInput" Width="80%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 30%">
                                            Base UTC Offset&nbsp;:&nbsp;<br />
                                            <asp:Label ID="lblhdrBase" runat="server" Text="[+-hh:mm:ss]"></asp:Label>
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtBaseUtc" runat="server" CssClass="txtInput"  MaxLength=9   Width="80%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 30%">
                                            Default Time Zone&nbsp;:&nbsp;
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                            
                                        </td>
                                        <td align="left">
                                            <asp:CheckBox ID="chkDefaultTimeZone" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="font-size: 11px; text-align: center;">
                                            <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" 
                                                OnClientClick="return validation();" Text="Save" />
                                            <asp:TextBox ID="txtTimeZoneID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="3" style="color: #FF0000; font-size: small;">
                                            * Mandatory fields
                                        </td>
                                    </tr>
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
</asp:Content>
