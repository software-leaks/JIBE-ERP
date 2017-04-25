<%@ Page Title="Worklist Access" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="WorkList_Access.aspx.cs" Inherits="Infrastructure_Libraries_WorkList_Access" %>

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

            if (document.getElementById("ctl00_MainContent_ddlUserName").value == "0") {
                alert("Please select User Name.");
                document.getElementById("ctl00_MainContent_ddlUserName").focus();
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
                Worklist Access
            </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 15%">
                                        User Name :&nbsp;
                                    </td>
                                    <td align="left" style="width: 30%">
                                     <%--   <asp:DropDownList ID="ddlUser_Name" runat="server" Width="100%" CssClass="txtInput"
                                            AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="--ALL--"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                         <asp:TextBox ID="txtSearch" runat="server" Width="100%" CssClass="txtInput"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 15%">
                                        Action Type :&nbsp;
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:DropDownList ID="ddlAction_Type" runat="server" Width="100%" CssClass="txtInput"
                                            AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="--ALL--"></asp:ListItem>
                                            <asp:ListItem Value="REWORK" Text="REWORK"></asp:ListItem>
                                            <asp:ListItem Value="CLOSE" Text="CLOSE"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="right" style="width: 15%">
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
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Worklist Access" OnClick="ImgAdd_Click"
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
                                <asp:GridView ID="gvWorkListAccess" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" OnRowDataBound="gvWorkListAccess_RowDataBound" DataKeyNames="ID"
                                    CellPadding="1" CellSpacing="0" Width="100%" GridLines="both" OnSorting="gvWorkListAccess_Sorting"
                                    AllowSorting="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code" SortExpression="User Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblUser_Name" runat="server" CommandName="Sort" CommandArgument="User_Name"
                                                    ForeColor="Black">User Name</asp:LinkButton>
                                                <img id="User_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <%-- <ItemTemplate>
                                                <asp:LinkButton ID="lblUser_ID" runat="server" CommandArgument='<%#Eval("User_Name")%>'
                                                    Text='<%# DataBinder.Eval(Container,"DataItem.User_Name") %>' Style="color: Black"
                                                    OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>--%>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblUser_ID" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.User_Name") %>'
                                                    Style="color: Black" CommandArgument='<%#Eval("[ID]")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description" SortExpression="Action Type">
                                            <HeaderTemplate>
                                                Action Type
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrencyDesc" runat="server" Text='<%#Eval("Action_Type")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                            <asp:TemplateField  >
                                            <HeaderTemplate>
                                                Assigned Job Types
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblJobTypes" runat="server" Text='<%#Eval("Access_Details")%>'></asp:Label>
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
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <%-- <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;LIB_CURRENCY&#39;,&#39;Currency_ID="+Eval("Currency_ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>--%>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindWorkList_Access" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 30%;">
                            <table width="98%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        User Name&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlUserName" CssClass="txtInput" runat="server" AppendDataBoundItems="true"
                                            Width="102%">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Action Type&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    *
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlActionType" CssClass="txtInput" runat="server" AppendDataBoundItems="true"
                                            Width="102%">
                                            <asp:ListItem Value="REWORK" Text="REWORK"></asp:ListItem>
                                            <asp:ListItem Value="CLOSE" Text="CLOSE"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Job Type&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    *
                                    </td>
                                    <td align="left">
                                        <asp:CheckBoxList ID="chkList" runat="server" RepeatDirection="Horizontal">
                                           
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; border-style: solid;
                                        border-color: Silver; border-width: 1px; background-color: #d8d8d8;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validation();"
                                            OnClick="btnsave_Click" />
                                        <asp:TextBox ID="txtAccessID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center" style="color: #FF0000; font-size: small;">
                                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
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
