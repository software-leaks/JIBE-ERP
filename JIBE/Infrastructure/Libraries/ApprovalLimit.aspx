<%@ Page Title="Approval Limit" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ApprovalLimit.aspx.cs" Inherits="ApprovalLimit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList" TagPrefix="ucDDL" %>
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
     <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>


     <link href="../../styles/premiere_blue/style.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
   

    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("ctl00_MainContent_DDLGroup").value == "0") {
                alert("Group is mandatory.");
                document.getElementById("ctl00_MainContent_DDLGroup").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_ddlType").value == "0") {
                alert("Type is mandatory.");
                document.getElementById("ctl00_MainContent_ddlType").focus();
                return false;
            }
            return true;
        }
    </script>
      <style type="text/css">
        table.ReqsnHead
        {
            border-top: 1px solid #E6E6E6;
            border-right: 1px solid #E6E6E6;
            background-color: #f4ffff;
        }
        table.ReqsnHead th
        {
            border-width: 1px;
            border-style: solid;
            border-color: #E6E6E6;
        }
        table.ReqsnHead td
        {
            border-bottom: 1px solid #E6E6E6;
            border-left: 1px solid #E6E6E6;
            background-color: #f4ffff;
        }
    </style>
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
              Approval Limit
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
                                    <td align="right" style="width: 8%">
                                        Group
                                    </td>
                                    <td align="left" style="width: 30%">
                                        <asp:DropDownList ID="DDLGroupFilter" runat="server" Width="120px" CssClass="txtInput">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 8%">
                                        User
                                    </td>
                                    <td align="left" style="width: 30%">
                                        <asp:DropDownList ID="DDLUserFilter" runat="server" Width="120px">
                                        </asp:DropDownList>
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
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Approval Limit" OnClick="ImgAdd_Click"
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
                                <asp:GridView ID="gvUserType" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvUserType_RowDataBound" CellPadding="1" CellSpacing="0" DataKeyNames="ID"
                                    Width="100%" GridLines="both" OnSorting="gvUserType_Sorting" AllowSorting="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                   <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Group Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGroupName" runat="server" Text='<%#Eval("Group_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblGroupNameHeader" runat="server" CommandName="Sort" CommandArgument="Group_Name"
                                                    ForeColor="Black">Type&nbsp;</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblApprovalType" runat="server" Text='<%#Eval("Approval_Type")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblUserNameHeader" runat="server" CommandName="Sort" CommandArgument="UserName"
                                                    ForeColor="Black">User Name&nbsp;</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblUserName" runat="server" Text='<%#Eval("UserName")%>' Style="color: Black"
                                                    CommandArgument='<%#Eval("ID")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approval Amount">
                                             <ItemTemplate>
                                                <asp:Label ID="lblMAX_APPROVAL_LIMIT" runat="server" Text='<%#Eval("MAX_APPROVAL_LIMIT")%>'></asp:Label>
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
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]") + "," +Eval("[Group_ID]") + "," +Eval("[Approval_TypeKey]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[ID]") + "," + Eval("[Group_ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
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
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindUserApproval" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd"  title="Approver Limit"  style="display: none; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 30%; height:600px">
                            <table width="100%" cellpadding="2" cellspacing="2">
                          
                                <tr>
                                    <td style="width:10%" >
                                        Group&nbsp;*&nbsp;:
                                    </td>
                                     <td  >
                                        <asp:DropDownList ID="DDLGroup" runat="server" Width="50%" CssClass="txtInput"  AutoPostBack="true" onselectedindexchanged="DDLGroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="width:10%" >
                                        Type &nbsp;*&nbsp;:
                                    </td>
                                    <td >
                                        <asp:DropDownList ID="ddlType" runat="server" Width="50%" CssClass="txtInput"  AutoPostBack="true" OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr>
                                   <td colspan="2">
                                        <div style="width:80%; height:400px; overflow:auto;" >
                                        <asp:GridView ID="gvUserApprovalList" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                                            AllowSorting="True" CaptionAlign="Bottom" CellPadding="4"
                                            Font-Size="14px" GridLines="None" Width="100%" CssClass="gridmain-css" Height="400px" OnRowDataBound="gvUserApprovalList_RowDataBound">
                                          <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                          <RowStyle CssClass="RowStyle-css" />
                                          <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                            <Columns>
                                                <asp:TemplateField  HeaderText="Select User"  >
                                                      <ItemTemplate>
                                                        <asp:CheckBox  ID="chkSelected" Checked='<%# Eval("UserSelected").ToString()=="1"?true:false%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                             
                                                <asp:TemplateField  HeaderText="User" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserId" runat="server" Text='<%# Eval("USERID")%>' Visible="false" ></asp:Label>
                                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("USERNAME")%>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField   >
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblAmount" Text="Approval Amount" runat="server"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:textbox id="txtAmount" runat="server"  Text='<%# Eval("MAX_APPROVAL_LIMIT")%>' ></asp:textbox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle CssClass="crew-interview-grid-alternaterow" />
                                            <EditRowStyle CssClass="crew-interview-grid-editrow" />
                                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle CssClass="crew-interview-grid-row" />
                                            <SortedAscendingCellStyle CssClass="crew-interview-grid-col-sorted-asc" />
                                            <SortedAscendingHeaderStyle CssClass="crew-interview-grid-header-sorted-asc" />
                                            <SortedDescendingCellStyle CssClass="crew-interview-grid-col-sorted-desc" />
                                            <SortedDescendingHeaderStyle CssClass="crew-interview-grid-header-sorted-desc" />
                                        </asp:GridView>
                                        </div>
                                   </td>
                                </tr>
                                 <tr>
                                    <td  colspan="2" style="font-size: 11px; text-align: center; ">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validation();"
                                            OnClick="btnsave_Click" />
                                        <asp:TextBox ID="txtApprovarID" runat="server" Visible="false" Width="1px"></asp:TextBox>
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
