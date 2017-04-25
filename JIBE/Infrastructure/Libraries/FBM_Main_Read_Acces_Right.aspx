<%@ Page Title="Read Access Right" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FBM_Main_Read_Acces_Right.aspx.cs" Inherits="QMS_FBM_FBM_Main_Read_Acces_Right" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server" >
    
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
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function validation() {

            if (document.getElementById("ctl00_MainContent_ddlRank").value.trim() == "0") {
                alert("Please Select Rank.");
                document.getElementById("ctl00_MainContent_ddlRank").focus();
                return false;
            }

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
                    <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
            
  
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
            
            <div style="height: 650px; width: 800px; color: Black;">
            <div class="page-title">
                      FBM READ ACCESS RIGHT
    
            </div>
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 10%">
                                         Search Text :&nbsp;
                                    </td>
                                    <td align="left" style="width: 30%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                      <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                                    WatermarkText="Type rank short name / rank name to search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" TabIndex="0" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Rank" OnClick="ImgAdd_Click"
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
                                <asp:GridView ID="gvRankCategory" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvRankCategory_RowDataBound" DataKeyNames="ID" CellPadding="1"
                                    CellSpacing="0" Width="100%" GridLines="both" OnSorting="gvRankCategory_Sorting"
                                    AllowSorting="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Rank Short Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblCategoryNameHeader" runat="server" CommandName="Sort" CommandArgument="RANK_ID"
                                                    ForeColor="Black">Rank Short Name&nbsp;</asp:LinkButton>
                                                <img id="RANK_ID" runat="server" visible="false"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblCategoryName" runat="server" Text='<%#Eval("Rank_Short_Name")%>' Style="color: Black"
                                                    CommandArgument='<%#Eval("ID")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Rank Name">
                                            <ItemTemplate>
                                               <%#Eval("Rank_Name")%>
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
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate" Visible ='<%# uaEditFlag %>'
                                                                CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete" Visible ='<%# uaDeleteFlage %>'
                                                                OnClientClick="return confirm('Are you sure want to delete?')" CommandArgument='<%#Eval("[ID]")%>'
                                                                ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px">
                                                            </asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindReadAccessRight" />
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
                                    <td align="right" style="width:15%">
                                        Select Rank  &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width:25%">
                                         <asp:DropDownList ID="ddlRank" AppendDataBoundItems="true" runat="server"
                                            Width="50%">
                                        </asp:DropDownList>
                                        <%--<asp:TextBox ID="txtRankcategory" CssClass="textbox-css" onkeypress="return isNumberKey('evt')" MaxLength="6" Width="90%" runat="server"></asp:TextBox>--%>
                                    </td>
                                </tr>
                             
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validation();"
                                            OnClick="btnsave_Click" CausesValidation="true" />
                                        <asp:TextBox ID="txtRankcategoryID" runat="server" Visible="false" Width="1px"></asp:TextBox>
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


