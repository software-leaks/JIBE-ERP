﻿<%@ Page Title="Approval Group" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false" AutoEventWireup="true"
    CodeFile="ApprovalGroup.aspx.cs" Inherits="ApprovalGroup" %>

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
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("ctl00_MainContent_txtApprovalGroup").value == "") {
                alert("Please enter group.");
                document.getElementById("ctl00_MainContent_txtApprovalGroup").focus();
                return false;
            }

        
            return true;
        }
        function OpenScreen(ID, Job_ID) {

            var url = 'Approval_Group_Department.aspx?ID=' + ID + '&Supp_ID=' + Job_ID;
            OpenPopupWindowBtnID('Approval_Group', 'Approval Group', url, 'popup', 520, 600, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 900px;
            height: 100%;"> 
             <div class="page-title">
               Approval Group
         </div>   
            <div style="height: 650px; width: 900px; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 12%">
                                        User :&nbsp;
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
                                      <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Approval Group"  OnClientClick="OpenScreen(0, 0)"
                                            ImageUrl="~/Images/Add-icon.png" />
                                        <%--<asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Approval Group" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" />--%>
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
                                <asp:GridView ID="gvApprovalGroup" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvApprovalGroup_RowDataBound" DataKeyNames="Group_ID" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both" OnSorting="gvApprovalGroup_Sorting" AllowSorting="true">
                                   <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                   <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                         
                                        <asp:TemplateField HeaderText="Approval Group">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblGroupNameHeader" runat="server" CommandName="Sort" CommandArgument="UserName"
                                                    ForeColor="Black">Approval Group&nbsp;</asp:LinkButton>
                                                <img id="UserType" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblUserName" runat="server" Text='<%#Eval("Group_Name")%>' Style="color: Black"
                                                    CommandArgument='<%#Eval("Group_ID")%>' OnCommand="onUpdate"></asp:LinkButton>
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
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update"  OnClientClick='<%#"OpenScreen(&#39;"+ Eval("Group_ID") +"&#39;);return false;"%>'
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Group_ID]")%>'  ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[Group_ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
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
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindApprovalGroup" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
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
                                    <td align="right" style="width: 20%">
                                        Group&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                          <asp:TextBox ID="txtApprovalGroup" CssClass="txtInput" Width="60%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validation();"
                                            OnClick="btnsave_Click" />
                                        <asp:TextBox ID="txtGroupID" runat="server" Visible="false" Width="1px"></asp:TextBox>
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
