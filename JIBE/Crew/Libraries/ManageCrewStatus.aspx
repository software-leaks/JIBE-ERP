<%@ Page Title="Status Mapping" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="ManageCrewStatus.aspx.cs" Inherits="Crew_Libraries__ManageCrewStatus" %>

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
    <style type="text/css">
        .InvisibleCol
        {
            display: none;
        }
        </style>
    <script language="javascript" type="text/javascript">

        function Divaddnewlink() {
            document.getElementById("divadd").style.display = "block";
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 70%;
            height: 100%;">
            <div class="page-title">
                Status&nbsp; Mapping
            </div>
            <div style="height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Status :&nbsp;
                                    </td>
                                    <td style="text-align: left" class="style1">
                                     <%--   <asp:DropDownList ID="ddlStatusSearch" Width="25%" CssClass="txtInput" runat="server">
                                            <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Deduction" Value="19"></asp:ListItem>
                                            <asp:ListItem Text="Earning" Value="18"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                         <asp:TextBox ID="txtfilter" runat="server" Width="25%"  CssClass="txtInput" ></asp:TextBox>
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
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click" Visible="false"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div style="width: 100%; height: 400px; overflow: scroll">
                                <asp:GridView ID="gvStatusStructure" runat="server" EmptyDataText="NO RECORDS FOUND" 
                                    AutoGenerateColumns="False" DataKeyNames="id"
                                    CellPadding="1" CellSpacing="0" Width="100%" GridLines="both" OnRowCommand="gvStatusStructure_RowCommand"
                                    OnSorting="gvStatusStructure_Sorting" AllowSorting="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Status">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblNameHeader" runat="server" ForeColor="Black">Status&nbsp;</asp:Label>
                                                <img id="Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("M_status")%>' Style="color: Black"
                                                    CommandArgument='<%#Eval("id")%>' OnCommand="onUpdate"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="110px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>                            
                                        <asp:TemplateField HeaderText="Sub Status">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSubStatuts" runat="server" ForeColor="Black">Sub Status &nbsp;</asp:Label>
                                                <img id="PayBleAtDetails" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCalcStatus" runat="server" Text='<%#Eval("Calculated_Status")%>'
                                                    Style="color: Black" CommandArgument='<%#Eval("id")%>' OnCommand="onUpdate"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joining Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJoiningType" runat="server" Text='<%# Eval("Joining_Type")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
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
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[id]")+";"+Container.DataItemIndex%>'
                                                                ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px">
                                                            </asp:ImageButton>
                                                        </td>
                                                     <%--   <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("id")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>--%>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                               <%-- <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="100" OnBindDataItem="BindStatusStructure" Visible="false" />--%>
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 30%;">
                            <table cellpadding="2" cellspacing="2"  width="100%"  style="padding: 10px 10px 10px 10px">
                                <tr>
                                    <td align="right" style="width: 20%">
                                        Status :&nbsp;&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlMainStatus" Width="70%" CssClass="txtInput" runat="server" AutoPostBack="true"
                                            AppendDataBoundItems="true" 
                                            onselectedindexchanged="ddlMainStatus_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>                     
                                <tr>
                                    <td align="right" valign="top" style="width: 25%">
                                        Sub Status :&nbsp;&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left" valign="top">
                                        <div id="div1" style="height:150px; Width:70%; overflow-y:scroll;">                                     
                                        <asp:CheckBoxList ID="chkCalc_Status" Width="100%" CssClass="txtInput" runat="server" AppendDataBoundItems="true" Enabled="false" >
                                        </asp:CheckBoxList>
                                         </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        Joining Type :&nbsp;&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                <div id="div2" style="height:150px;  Width:70%; overflow-y:scroll;">          
                                        <asp:CheckBoxList ID="chkJoiningType" Width="100%" CssClass="txtInput" runat="server" AppendDataBoundItems="true">
                                        </asp:CheckBoxList>
                                        </div>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; padding: 5px 5px 5px 5px;
                                        border-color: Silver; border-width: 1px">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                       <%-- <asp:TextBox ID="txtCode" runat="server" Visible="false" Width="1px"></asp:TextBox>--%>
                                        <%--OnClientClick="return validation();"--%>
                                      <%--  <asp:TextBox ID="txtParentCode" runat="server" Visible="false" Width="1px"></asp:TextBox>--%>
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
