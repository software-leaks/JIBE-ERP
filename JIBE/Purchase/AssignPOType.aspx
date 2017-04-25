<%@ Page Title="Assign PO Type" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AssignPOType.aspx.cs" Inherits="Purchase_AssignPOType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function validation() {

//            var dcode = document.getElementById("ctl00_MainContent_txtCode").value.trim();
//            var dName = document.getElementById("ctl00_MainContent_TxtDept").value.trim();
//            var ftype = document.getElementById("ctl00_MainContent_cmbFType").value;
//            var AcClassiCode = document.getElementById("ctl00_MainContent_ddlAcClassification").value;


//            if (dName == "" || dName == null) {
//                alert("Please enter department name.");
//                document.getElementById("ctl00_MainContent_TxtDept").focus();
//                return false;
//            }

//            if (dcode == "" || dcode == null) {
//                alert("Please enter department code.");
//                document.getElementById("ctl00_MainContent_txtCode").focus();
//                return false;
//            }

//            if (ftype == "0" || ftype == null) {
//                alert("Select form type to proceed.");
//                document.getElementById("ctl00_MainContent_cmbFType").focus();
//                return false;
//            }

//            if (AcClassiCode == "0" || AcClassiCode == null) {
//                alert("Select A/c Classification code to proceed.");
//                document.getElementById("ctl00_MainContent_ddlAcClassification").focus();
//                return false;
//            }

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
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
            <div class="page-title">
            Req Type and PO Type
            </div> 
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr align="left">
                                    <td align="right" style="width: 20%">
                                        Req Type :&nbsp;&nbsp;
                                    </td>
                                    <td align="left">
                                          <asp:DropDownList ID="ddlFilterType" runat="server"  Width="100%">
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
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Department" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" Text="Add Department" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="overflow-x: hidden; overflow-y: none; height: 600px; width: 800px;">
                            <asp:GridView ID="rgdDept" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                OnRowDataBound="rgdDept_RowDataBound" Width="100%" GridLines="Both" AllowSorting="true" DataKeyNames="ID"
                                OnSorting="rgdDept_Sorting" CellPadding="1" CellSpacing="0" OnRowCommand="rgdDept_RowCommand"
                                PageSize="20">
                                  <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                  <RowStyle CssClass="RowStyle-css" />
                                  <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                 <asp:TemplateField HeaderText="Name">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblReq_Code" runat="server" CommandName="Sort" CommandArgument="Req_Code"
                                                ForeColor="Black">Name&nbsp;</asp:LinkButton>
                                            <img id="Req_Code" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblReq_Name" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Req_Name") %>'
                                                Style="color: Black" CommandArgument='<%#Eval("[Req_Code]")%>' OnCommand="onUpdate"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Code">
                                        <HeaderTemplate>
                                            Budget Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblBudget_Name" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Budget_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="130px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                         <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Code">
                                        <HeaderTemplate>
                                            PO Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPO_Type" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PO_Type") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                         <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Form Type">
                                        <HeaderTemplate>
                                          Account Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccount_Type" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Account_Type") %>'></asp:Label>
                                        </ItemTemplate>
                                      <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                         <HeaderStyle Wrap="true" HorizontalAlign="Center" />
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
                                                    <td>
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                            Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                            CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
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
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindrgdDept" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 450px;">
                            <table width="98%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Req Type &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                         <asp:DropDownList ID="ddlReqType" runat="server" Width="97%" AppendDataBoundItems="True"
                                            CssClass="txtInput">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="ReqAdd" runat="server" Display="None" InitialValue="0" ErrorMessage="Req Type is mandatory field."
                                    ControlToValidate="ddlReqType" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                       Budget Code&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                         <asp:DropDownList ID="ddlBudgetCode" runat="server" Width="97%" AppendDataBoundItems="True"
                                            CssClass="txtInput">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" Display="None" ErrorMessage="Budget Code is mandatory field."
                                    ControlToValidate="ddlBudgetCode" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        PO Type&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlPOType" runat="server" Width="97%" AppendDataBoundItems="True"
                                            CssClass="txtInput">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" Display="None" ErrorMessage="PO Type is mandatory field."
                                    ControlToValidate="ddlPOType" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Account Type&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlAccType" runat="server" Width="97%" AppendDataBoundItems="true"
                                            CssClass="txtInput">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0" Display="None" ErrorMessage="Account Type is mandatory field."
                                    ControlToValidate="ddlAccType" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="center" colspan="3">
                                        <asp:Button ID="ImgSave" Style="font-size: small" runat="server" 
                                            OnClick="Save_Click" Text="Save" />
                                        <asp:TextBox ID="txtBudgetID" runat="server" Visible="false" Width="1px"></asp:TextBox>
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

