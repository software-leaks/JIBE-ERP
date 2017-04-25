<%@ Page Title="Crew Medical History Viewer" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="CrewMedicalHistoryViewer.aspx.cs"
    Inherits="CrewMedicalHistoryViewer" %>

<%@ Register Src="../UserControl/uc_SupplierList.ascx" TagName="uc_SupplierList"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="ucpager" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/CrewDetails_DataHandler.js" type="text/javascript"></script>
    <%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            width: 120px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 0px 2px 0px 3px;
            height: 20px;
            vertical-align: middle;
        }
    </style>
    <style type="text/css">
        .ob_iLboICBC li
        {
            float: left;
            width: 150px;
            font-size: 11px;
        }
        /* For IE6 */* HTML .ob_iLboICBC li
        {
            -width: 135px;
        }
        * HTML .ob_iLboICBC li b
        {
            width: 135px;
            overflow: hidden;
        }
        .style1
        {
            width: 100%;
        }
    </style>
    <script type="text/javascript">

        var lo;
        function selMe(src) {
            try {
                var o;
                var p;
                if (src) {
                    o = document.getElementById(src);
                }
                else {
                    o = window.event.srcElement;
                }
                p = o.parentElement.parentElement;
                p.className = 'ih';
                if (lo) {
                    if (lo.id != p.id) {
                        lo.className = '';
                        lo = p;
                    }
                }
                else {
                    lo = p;
                }
            } catch (ex) {
            }
        }

       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Crew Medical History Viewer
    </div>
 <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 0px; color: #282829">
        <input type="hidden" id="hdf_Log_ID" />
        <input type="hidden" id="hdf_User_ID" runat="server" />
        <asp:UpdatePanel ID="updLPOMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlmain" Width="100%" runat="server" BorderStyle="None" DefaultButton="btnSearchPO">
                    <table width="100%" style="padding-bottom: 5px; padding-top: 5px">
                        <tr>
                            <td align="right">
                                Fleet :
                            </td>
                            <td align="left">
                                <ucDDL:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                    Height="150" Width="160" />
                            </td>
                            <td align="right">
                                Rank :
                            </td>
                            <td align="left">
                                <ucDDL:ucCustomDropDownList ID="DDLRank" runat="server" UseInHeader="false" OnApplySearch="DDLRank_SelectedIndexChanged"
                                    Height="200" Width="160" />
                            </td>
                            <td align="right">
                                Manning Office :
                            </td>
                            <td align="left">
                                <ucDDL:ucCustomDropDownList ID="DDLManningOffice" runat="server" UseInHeader="false"
                                    OnApplySearch="DDLManningOffice_SelectedIndexChanged" Height="200" Width="160" />
                            </td>
                            <td align="right">
                                From Date :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCommentFromDate" runat="server" Width="100px" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtCommentFromDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td style="width: 130px; padding-left: 10px">
                                <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                    ImageUrl="~/Images/Exptoexcel.png" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Vessel :
                            </td>
                            <td align="left">
                                <ucDDL:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                                    Height="200" Width="160" />
                            </td>
                            <td align="right">
                                Nationality :
                            </td>
                            <td align="left">
                                <ucDDL:ucCustomDropDownList ID="DDLNationality" runat="server" UseInHeader="false"
                                    OnApplySearch="DDLNationality_SelectedIndexChanged" Height="200" Width="160" />
                            </td>
                            <td align="right">
                                Status :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DDLStatus"  Width="160px" runat="server">
                                    <asp:ListItem Value="0" Text="--ALL--"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Open"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Close"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                To Date :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCommentToDate" runat="server" Width="100px" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calTo" runat="server" TargetControlID="txtCommentToDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td align="left" style="width: 15%">
                                <asp:Button ID="btnSearchPO" Text="Search" Height="24px" runat="server" OnClick="btnSearchPO_Click" />
                                &nbsp;
                                <asp:Button ID="btnClearFilter" Text="Clear Filter" Height="24px" runat="server"
                                    OnClick="btnClearFilter_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Search By :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSearch" Width="160px" runat="server" ></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtSearch"
                                    WatermarkText="Search to Staff Code/Name" WatermarkCssClass="watermarked" />
                            </td>
                            <td>
                            Crew Status
                            </td>
                            <td>
                            <asp:DropDownList ID="DDLCrewStatus"  Width="160px" runat="server">
                                    <asp:ListItem Value="0" Text="--ALL--"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="ONBOARD"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="NTBR"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td align="left">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="txtSelMenu" runat="server"></asp:HiddenField>
                    <table width="100%" style="border-top: 1px solid #cccccc" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: right; padding-right: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvCrewFeedback" runat="server" AutoGenerateColumns="false" Width="100%"
                                    EmptyDataText="No record found !" EmptyDataRowStyle-ForeColor="Maroon" CssClass="gridmain-css"
                                    BackColor="#D8D8D8" CellPadding="5" GridLines="None" AllowSorting="True" OnSorting="gvCrewFeedback_Sorting"
                                    OnRowDataBound="gvCrewFeedback_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Staff Code">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblStaffCodeHeader" runat="server" ForeColor="Black" CommandArgument="Staff_Code"
                                                    CommandName="Sort">Staff Code&nbsp;</asp:LinkButton>
                                                <img id="Staff_Code" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblStaffCode" runat="server" NavigateUrl='<%# "CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                                                    Target="_blank" Text='<%# Eval("Staff_Code")%>' CssClass="staffInfo pin-it"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblStaffNameHeader" runat="server" ForeColor="Black" CommandArgument="Staff_Name"
                                                    CommandName="Sort">Name&nbsp;</asp:LinkButton>
                                                <img id="Staff_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblStaffName" Text='<%#Eval("Staff_Name")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rank">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblRankHeader" runat="server" ForeColor="Black" CommandArgument="Rank_Name"
                                                    CommandName="Sort">Rank&nbsp;</asp:LinkButton>
                                                <img id="Rank" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRank" Text='<%#Eval("Rank_Name")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVessel_Short_Name" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQueryType" runat="server" Text='<%# Eval("Case_Type")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reported On" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReportedOn" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Reported_On"))) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Details" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblQuery_Detail" runat="server" onclick='<%# "Show_MedHistory_Details(" + Eval("CrewID").ToString() + ","+ Eval("ID").ToString() + ","+ Eval("Vessel_ID").ToString()+ ","+ Eval("Office_ID").ToString() +")" %>'
                                                    Target="_blank" Text='<%# Eval("Case_Detail")%>' Style="cursor: pointer" ForeColor="Blue"></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQueryStatus" runat="server" Text='<%# Eval("CASE_STATUS")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" Font-Size="10px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:HyperLink ID="lnkAttachment" runat="server" ImageUrl="~/Images/Attach.png" Target="_blank"
                                                                Visible='<%#Eval("Attach_Count").ToString()=="0"?false:true%>' />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgAdd" runat="server" Text="AddMedicalHistory" CommandArgument='<%#Eval("[CrewID]")%>'
                                                                Visible='<%# uaAddFlag %>' ForeColor="Black" ToolTip="Add Medical History" ImageUrl="~/Images/Add.gif"
                                                                OnClick='<%# "Add_MedHistory_Details(" + Eval("CrewID").ToString() + ","+ Eval("ID").ToString() + ","+ Eval("Vessel_ID").ToString()+ ","+ Eval("Office_ID").ToString() +")" %>'
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                </asp:GridView>
                                <ucpager:ucCustomPager ID="ucCustomPager" OnBindDataItem="BindGrid" AlwaysGetRecordsCount="true"
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ImgExpExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 0.5%; top: 15%; width: 900px; z-index: 1; color: black" title=''>
        <div class="content">
            <iframe id="frPopupFrame" src="" frameborder="0" height="100%" width="100%"></iframe>
        </div>
    </div>
</asp:Content>
