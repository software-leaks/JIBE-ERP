<%@ Page Title="Exchange Rate" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ExchangeRate.aspx.cs" Inherits="ExchangeRate" %>

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
    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("ctl00_MainContent_ddlCurrency").value == "0") {
                alert("Please select currency.");
                document.getElementById("ctl00_MainContent_ddlCurrency").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtExchangeRate").value == "") {
                alert("Please enter Exchange rate.");
                document.getElementById("ctl00_MainContent_txtExchangeRate").focus();
                return false;
            }


            if (document.getElementById("ctl00_MainContent_dtpCurrentDate").value == "") {
                alert("Please enter date.");
                document.getElementById("ctl00_MainContent_dtpCurrentDate").focus();
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
               Exchange Rate
            </div>
          
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 10%">
                                        Currency Code:&nbsp;
                                    </td>
                                    <td align="left" style="width: 30%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="100%" CssClass="txtInput"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked"   />
                                    </td>
                                    <td align="right" style="width: 15%">
                                        <asp:RadioButton ID="rbtCurrent" runat="server" Text="Current" Checked="True" 
                                            Font-Bold="True" GroupName="CurrentHistory" />
                                        <asp:RadioButton ID="rbtHistory" runat="server" Text="History" Font-Bold="True" 
                                            GroupName="CurrentHistory" />
                                      &nbsp;&nbsp;
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
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Exchange Rate" OnClick="ImgAdd_Click"
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
                                <asp:GridView ID="gvExchRate" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvExchRate_RowDataBound" CellPadding="1"
                                    CellSpacing="0" Width="100%" GridLines="both" OnSorting="gvExchRate_Sorting"
                                    AllowSorting="true" style="margin-right: 1px">
                                     <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="FlagName">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblCurr_codeHeader" runat="server" CommandName="Sort" CommandArgument="Curr_code"
                                                    ForeColor="Black">Currency Code&nbsp;</asp:LinkButton>
                                                <img id="CURR_CODE" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblCurr_code" runat="server" Text='<%#Eval("Curr_code")%>' Style="color: Black"
                                                    CommandArgument='<%#Eval("Curr_code") + ","+ Eval("Date") %>' OnCommand="onUpdate"></asp:LinkButton>
                                                   
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                      <%-- <asp:TemplateField HeaderText="FlagName">
                                     <HeaderTemplate>
                                        <asp:LinkButton ID="lblDeptNameHeader" runat="server" CommandName="Sort" CommandArgument="Curr_code"
                                             ForeColor="Black">Curr Code&nbsp;</asp:LinkButton>
                                        <img id="Curr_code" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurr_code" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Curr_code") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                     <HeaderStyle Wrap="true" HorizontalAlign="Left" />--%>
                               <%-- </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Exch.Rate">
                                            <HeaderTemplate>
                                             Exchange rate (vs USD)
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblExch_rate" runat="server" Text='<%#Eval("Exch_rate")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <HeaderTemplate>
                                             Date
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                      <%--  <asp:TemplateField HeaderText="BASE CURRECY">
                                            <HeaderTemplate>
                                             Base Currency
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBaseCurrecy" runat="server" Text='<%#Eval("BASE_CURRECY")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("Curr_code") + ","+ Eval("Date") %>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("Curr_code") +","+ Eval("Date") %>' ForeColor="Black" ToolTip="Delete"
                                                                ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                        </td>
                                                       <td>
                                                          <%--  <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server"  />--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindExchRate" />
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
                                    <td align="right" style="width: 15%">
                                       Currency &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 40%">
                                        <asp:DropDownList ID="ddlCurrency" Width="82%" runat="server" CssClass="txtInput" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                       Exchange Rate &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtExchangeRate" CssClass="txtInput" Width="80%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                       Date &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="dtpCurrentDate" runat="server" Width="80%" MaxLength="100" CssClass="txtInput"></asp:TextBox>
                                            <cc1:CalendarExtender   TargetControlID="dtpCurrentDate" ID="caldtpCurrentDate" Format="dd-MM-yyyy"
                                                runat="server">
                                            </cc1:CalendarExtender>
                                    </td>
                                </tr>

                                 <tr>
                                    <td align="right">
                                       Base Currency &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    
                                    </td>
                                    <td align="left">
                                          <asp:TextBox ID="txtBaseCurr" ReadOnly="true" CssClass="txtReadOnly" Width="80%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validation();"
                                            OnClick="btnsave_Click" />
                                        <asp:TextBox ID="txtCurrency" runat="server" Visible="false" Width="1px"></asp:TextBox>
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
