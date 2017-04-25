<%@ Page Title="Functions Library" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PMSFunctionsLibraryPage.aspx.cs" Inherits="Technical_PMS_PMSFunctionsLibraryPage" %>

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

//        function validation() {

//            if (document.getElementById("ctl00_MainContent_txtFunction").value == "") {
//                alert("Please Enter Function Name.");
//                document.getElementById("ctl00_MainContent_txtFunction").focus();
//                return false;
//            }
//            if (document.getElementById("ctl00_MainContent_txtShortCode").value == "") {
//                alert("Please Enter Short Code.");
//                document.getElementById("ctl00_MainContent_txtShortCode").focus();
//                return false;
//            }
//            return true;
//        }

        function specialcharecter() {
            
            if (document.getElementById($('[id$=txtFunction]').attr('id')) != null) {
                var iChars = "!`@#$%^&*()+=-[]\\\';,./{}|\":<>?~_";
                var data = document.getElementById($('[id$=txtFunction]').attr('id')).value;
               
                for (var i = 0; i < iChars.length; i++) {
                    if (data.indexOf(iChars.charAt(i))>= 0) {
                        alert("Function name with special characters is not allowed.");
                        document.getElementById($('[id$=txtFunction]').attr('id')).value = "";

                        return false;
                    }
                }
            }

           if (document.getElementById($('[id$=txtShortCode]').attr('id')) != null) {
                var iChars = "!`@#$%^&*()+=-[]\\\';,./{}|\":<>?~_";
                var data = document.getElementById($('[id$=txtShortCode]').attr('id')).value;

                for (var i = 0; i < iChars.length; i++) {
                    if (data.indexOf(iChars.charAt(i))>= 0) {
                        alert("Short code with special characters is not allowed.");
                        document.getElementById($('[id$=txtShortCode]').attr('id')).value = "";

                        return false;
                    }
                }
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
                Functions Library
            </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlsearch" runat="server" DefaultButton="btnFilter">
                            <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                                <table width="100%" cellpadding="4" cellspacing="4">
                                    <tr>
                                        <td align="right" style="width: 15%">
                                            Function Name :
                                        </td>
                                        <td align="left" style="width: 25%">
                                            <asp:TextBox ID="txtSearch" runat="server" Width="100%" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td align="right" style="width: 10%">
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
                                            <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add function" OnClick="ImgAdd_Click"
                                                ImageUrl="~/Images/Add-icon.png" />
                                        </td>
                                        <td style="width: 5%">
                                            <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                                ImageUrl="~/Images/Exptoexcel.png" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvPMSFunctionLib" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Function Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFuntion" runat="server" Text='<%#Eval("Description") %>'>'></asp:Label>
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
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Code]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Deleting this function will delete all the information stored in it including systems and jobs from all vessels.\r\nAre you sure you wish to continue?')"
                                                                CommandArgument='<%#Eval("[Code]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;PURC_LIB_SYSTEM_PARAMETERS&#39;,&#39;Code="+Eval("Code").ToString()+"&#39;,event,this)" %>'>
                                                            </asp:Image>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign = "Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="PMS_Get_FunctionBySearch" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        
                            <asp:Label ID="lblDelete" runat="server" Text=""></asp:Label>
                        <asp:Panel ID="pnladd" runat="server" DefaultButton="btnsave">
                            <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                                font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 30%;">
                                <table width="98%" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td align="right" style="width: 30%">
                                            Function Name :
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFunction" runat="server" Width="100%" CssClass="txtInput" MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 30%">
                                            Short Code :
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtShortCode" runat="server" Width="100%" CssClass="txtInput" MaxLength="4"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="font-size: 11px; text-align: center; border-style: solid;
                                            border-color: Silver; border-width: 1px; background-color: #d8d8d8;">
                                            <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return specialcharecter();"
                                                OnClick="btnsave_Click" />
                                            <asp:TextBox ID="txtID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center" style="color: #FF0000; font-size: small;">
                                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                            * Mandatory field
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
