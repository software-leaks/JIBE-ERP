<%@ Page Title="System Variable" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="System_Variable.aspx.cs" Inherits="ASL_Libraries_System_Variable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
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

        function Validation() {

//            if (document.getElementById("ctl00_MainContent_txtAirPortName").value.trim() == "") {
//                alert("Please enter airport name.");
//                document.getElementById("ctl00_MainContent_txtAirPortName").focus();
//                return false;
//            }

//            if (document.getElementById("ctl00_MainContent_txtElevation").value != "") {
//                if (isNaN(document.getElementById("ctl00_MainContent_txtElevation").value)) {
//                    alert("Elevation allows numeric value only.");
//                    document.getElementById("ctl00_MainContent_txtElevation").focus();
//                    return false;
//                }
//            }

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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;
            height: 100%;">
            <div class="page-title">
                System Variable
            </div>
            <div style="height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px;">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td style="width: 15%" align="right">
                                        Variable Type :&nbsp;
                                    </td>
                                    <td style="width: 20%" align="left">
                                        <asp:DropDownList ID="ddlVariableTypeFilter" Width="98%" runat="server" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 8%" align="right">
                                        Variable Name :&nbsp;
                                    </td>
                                    <td style="width: 20%" align="left">
                                       <asp:TextBox ID="txtNameFilter" runat="server" Width="98%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtNameFilter"
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
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New System Variable" OnClick="ImgAdd_Click"
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
                                <asp:GridView ID="gvVariable" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvVariable_RowDataBound" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both" OnSorting="gvVariable_Sorting" AllowSorting="true"
                                    CssClass="gridmain-css">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Variable Type">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblVariableType" runat="server" CommandName="Sort" CommandArgument="Variable_Type"
                                                    ForeColor="Black">Variable Type</asp:LinkButton>
                                               
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVType" runat="server" Text='<%#Eval("Variable_Type")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Variable Name">
                                            <HeaderTemplate>
                                              <asp:LinkButton ID="lblVariableName" runat="server" CommandName="Sort" CommandArgument="Variable_Name"
                                                    ForeColor="Black">Variable Name</asp:LinkButton>
                                               
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVName" runat="server" Text='<%#Eval("Variable_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Variable Code">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblVCode" runat="server" CommandName="Sort" CommandArgument="Variable_Code"
                                                    ForeColor="Black">Variable Code&nbsp;</asp:LinkButton>
                                              
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVcode" runat="server" Text='<%#Eval("Variable_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Variable Value">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkValue" runat="server" CommandName="Sort" CommandArgument="VARIABLE_VALUE"
                                                    ForeColor="Black">Variable Value&nbsp;</asp:LinkButton>
                                              
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%#Eval("VARIABLE_VALUE")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Color Code">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lnkColorCode" runat="server" CommandName="Sort" CommandArgument="COLOR_CODE"
                                                    ForeColor="Black">Color Code&nbsp;</asp:LinkButton>
                                              
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblColorcode" runat="server" Text='<%#Eval("COLOR_CODE")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
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
                                                        <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;SYS_VARIABLES&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindSysVariableGrid" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 45%;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td colspan="6">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Variable Type &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                       <asp:Label ID="lblType" runat="server" ForeColor="Red" Text="*" ></asp:Label>
                                    </td>
                                    <td align="left" style="width: 34%">
                                          <asp:DropDownList ID="ddlVariableType" Width="98%" runat="server" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="ReqVariableType" runat="server" Display="None" InitialValue="0"
                                                    ErrorMessage=" Variable Type is mandatory field." ControlToValidate="ddlVariableType" ValidationGroup="vgSubmit"
                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right" style="width: 15%">
                                         Variable Name &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                     <asp:Label ID="lblName" runat="server" ForeColor="Red" Text="*" ></asp:Label>
                                    </td>
                                    <td align="left" style="width: 34%">
                                        <asp:TextBox ID="txtName" CssClass="txtInput" Width="95%" MaxLength="30" runat="server">
                                        </asp:TextBox>
                                         <asp:RequiredFieldValidator ID="ReqVariableName" runat="server" Display="None" 
                                                    ErrorMessage="Variable Name is mandatory field." ControlToValidate="txtName" ValidationGroup="vgSubmit"
                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Variable code &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    <asp:Label ID="lblCode" runat="server" ForeColor="Red" Text="*" ></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCode" CssClass="txtInput" Width="95%" MaxLength="10" runat="server">
                                        </asp:TextBox>
                                         <asp:RequiredFieldValidator ID="ReqVariableCode" runat="server" Display="None" 
                                                    ErrorMessage="Variable code is mandatory field." ControlToValidate="txtCode" ValidationGroup="vgSubmit"
                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right">
                                       VARIABLE_VALUE &nbsp;:&nbsp;
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtValue" CssClass="txtInput" Width="95%" MaxLength="10" runat="server">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        COLOR_CODE &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtColorCode" CssClass="txtInput" Width="95%" MaxLength="10" runat="server">
                                        </asp:TextBox>
                                       
                                    </td>
                                    <td align="right">
                                      
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="font-size: 11px; text-align: center; border-style: solid;
                                        border-color: Silver; border-width: 1px">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" ValidationGroup="vgSubmit" />
                                        <asp:TextBox ID="txtVariableID" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="6" style="font-size: 14px; color:Red; text-align: center;">
                                       <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 0px solid #cccccc;
                                            background-color: #FDFDFD">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="right" style="color: #FF0000; font-size: small;">
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
