<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="RiskAssessmentUserLevelConfig.aspx.cs"
    Title="Approval Configuration" Inherits="JRA_Libraries_RiskAssessmentUserLevelConfig"
      %>

<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .LinkButton
        {
            text-decoration: none;
            color: Black;
        }
        
        .row-header
        {
            background-color: #aabbdd;
            font-weight: bold;
        }
        .roundedBox
        {
            border-radius: 5px;
            border: 2px solid white;
            background-color: #DBDFD5;
            text-align: center;
            font-size: 14px;
            color: #555;
            margin: 2px;
            padding: 2px;
        }
    </style>
    <script type="text/javascript">
        function SetWorkCategID(val,tes) {

            var hfWCHidden = document.getElementById('<%= hfWC.ClientID %>');
            hfWCHidden.value = val.split(";")[0];

            var hfType = document.getElementById('<%= hfType.ClientID %>');
            if (val.split(";")[1] == "") {
                hfType.value = "1";
              
            }
            else {
                hfType.value = val.split(";")[1];
            }

            var hfIDName = document.getElementById('<%= hfIDName.ClientID %>');
            hfIDName.value = tes;

             
          
        }
        function GetRadioButtonListSelectedValue(radioButtonList) {

            var hfType = document.getElementById('<%= hfType.ClientID %>');
            hfType.value = $('input[type="radio"]:checked').val();

            var hfTempType = document.getElementById('<%= hfTempType.ClientID %>');
            hfTempType.value = $('input[type="radio"]:checked').val();
            
        }
        
        function SetSaveRank() {

            var hfSaveRank = document.getElementById('<%= hfSaveRank.ClientID %>');
            hfSaveRank.value = "1";
        }
        
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Risk Approval Configuration
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <div align="center">
                    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="12px"
                        ForeColor="Red" Visible="False"></asp:Label>
                    <br />
                    <div class="roundedBox">
                        <table style="text-align: left; width: 100%">
                            <tr>
                                <td style="width: 110px">
                                     Work Category:
                                </td>
                                <td style="width: 190px">
                                    <asp:DropDownList ID="ddlParentWorkCateg" Width="98%" runat="server" AutoPostBack="true"
                                        onchange="SetWorkCategID(this.options[this.selectedIndex].value,this.options[this.selectedIndex].text);">
                                    </asp:DropDownList>
                                </td>
                                <asp:HiddenField ID="hfWC" runat="server" />
                                <asp:HiddenField ID="hfType" runat="server" />
                                  <asp:HiddenField ID="hfSaveRank" runat="server" />
                                      <asp:HiddenField ID="hfTempType" runat="server" />
                                        <asp:HiddenField ID="hfIDName" runat="server" />
                                <td style="width: 170px">
                                    &nbsp;&nbsp;&nbsp;Copy To Work Category:
                                </td>
                                <td style="text-align: left; width: 110px">
                                    <ucDDL:ucCustomDropDownList ID="ddlCTWorkCategory" runat="server" UseInHeader="false"
                                        Height="150" Width="100" Style="float: left;" />
                                </td>
                                <td style="text-align: left">
                                    <asp:Button ID="btnCopyTo" runat="server" Text="Copy Approval Validation" OnClick="btnCopyTo_Click" />
                                </td>
                            </tr>
                        </table>
                        <table style="text-align: left; width: 100%">
                            <tr>
                                <td style="width: 170px">
                               <asp:Label ID="lblWC" runat="server"></asp:Label>
                                </td>
                                <td style="width: 110px">
                                    Approval Type
                                </td>
                                <td  style="text-align: left;text-align:left;width: 140px">
                                    <asp:RadioButtonList ID="rdblstAppType" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="true" onchange="GetRadioButtonListSelectedValue(this);" OnSelectedIndexChanged="rdblstAppType_SelectedIndexChanged" >
                                        <asp:ListItem Text="Office" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Vessel" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:Panel ID="pnlVessel" runat="server"> 
                    <div id="diVPopupRank" title="Rank List">
                        <table>
                            
                            <tr>
                                <td colspan="3">
                                    <div id="Div2" title="Approver List" style="">
                                        <asp:GridView ID="gvRankList" runat="server" Width="300px" AutoGenerateColumns="false"
                                            GridLines="None" BorderStyle="Solid" BorderWidth="1px" BorderColor="#df5015">
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" Font-Size="14px" />
                                            <HeaderStyle CssClass="HeaderStyle-css" Height="35px" Font-Size="12px" />
                                            <PagerStyle CssClass="PMSPagerStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="Rank_Short_Name" HeaderText="Rank" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkRank" runat="server" Enabled='<%# EnableCheckboxRank(Eval("ID").ToString()) %>'
                                                            Checked='<%# SelectRank(Eval("ID").ToString()) %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                                 <td valign="top">
                             <asp:Button ID="btnSaveRank" Text="Save Rank Approval" runat="server" OnClick="btnSaveRank_Click" OnClientClick="SetSaveRank()" />
                            </td>
                            </tr>
                           
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlOffice" runat="server">
                    <div style="width: 100%;">
                        <table style="width: 100%;">
                            <tr>
                                <td align="left">
                                    <asp:Button ID="btnAddLevel" runat="server" Text="Add Level" OnClick="btnAddLevel_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border: 1px solid black;">
                                    <asp:GridView ID="grdLevel" runat="server" DataKeyNames="Approval_Level" Width="100%"
                                        AutoGenerateColumns="false" GridLines="None">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" Font-Size="14px" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" Font-Size="14px" />
                                        <PagerStyle CssClass="PMSPagerStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="14px" />
                                        <Columns>
                                            <asp:BoundField DataField="UserID" HeaderText="ID" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="40px" />
                                            <asp:BoundField DataField="UserName" HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div id="dvAppLevelUser" title="Approver List" style="display: none; font-family: Tahoma;
                        font-size: 10px;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSearch" runat="server" Text="Search Text" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div id="dvUser" title="Approver List" style="height: 300px; overflow-y: scroll;">
                                        <asp:GridView ID="grdUser" runat="server" Width="300px" AutoGenerateColumns="false"
                                            GridLines="None" BorderStyle="Solid" BorderWidth="1px" BorderColor="#df5015">
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" Font-Size="14px" />
                                            <HeaderStyle CssClass="HeaderStyle-css" Height="35px" Font-Size="12px" />
                                            <PagerStyle CssClass="PMSPagerStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                            <Columns>
                                                <asp:BoundField DataField="UserID" HeaderText="User ID" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="UserName" HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkUser" runat="server" Enabled='<%# EnableCheckbox(Eval("UserID").ToString()) %>'
                                                            Checked='<%# SelectCheckbox(Eval("UserID").ToString()) %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
