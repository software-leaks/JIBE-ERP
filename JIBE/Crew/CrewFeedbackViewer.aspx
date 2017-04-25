<%@ Page Title="Crew Feedback Viewer" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="CrewFeedbackViewer.aspx.cs"
    Inherits="CrewFeedbackViewer" %>

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
       Crew Feedback Viewer
    </div>
      <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
  
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 0px; color: #282829">
        <input type="hidden" id="hdf_Log_ID" />
        <input type="hidden" id="hdf_User_ID" runat="server" />
      <%--  <asp:UpdatePanel ID="updLPOMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
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
                                <asp:TextBox ID="txtCommentFromDate" runat="server" Width="100px"></asp:TextBox>
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
                                Commented By :
                            </td>
                            <td align="left">
                                <ucDDL:ucCustomDropDownList ID="DDLCommentedBy" runat="server" UseInHeader="false"
                                    OnApplySearch="DDLCommentedBy_SelectedIndexChanged" Height="200" Width="160" />
                            </td>
                            <td align="right">
                                To Date :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCommentToDate" runat="server" Width="100px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calTo" runat="server" TargetControlID="txtCommentToDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td align="left" style="width: 15%">
                                <asp:Button ID="btnSearchPO" Text="Search" Height="24px" ToolTip="Search" runat="server" OnClick="btnSearchPO_Click" />
                                &nbsp;
                                <asp:Button ID="btnClearFilter" Text="Clear Filter" ToolTip="Clear Filter" Height="24px" runat="server"
                                    OnClick="btnClearFilter_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Search By :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSearch" Width="160px" runat="server"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtSearch"
                                    WatermarkText="Search to Staff Code/Name" WatermarkCssClass="watermarked" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td align="right">
                                Status :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DDLStatus" Width="160px" runat="server">
                                    <asp:ListItem Value="0" Text="--ALL--"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="ONBOARD"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="NTBR"></asp:ListItem>
                                </asp:DropDownList>
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
                                    BackColor="#D8D8D8" CellPadding="5" GridLines="None" AllowSorting="True"
                                    OnSorting="gvCrewFeedback_Sorting" OnRowDataBound="gvCrewFeedback_RowDataBound" >
                            
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
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comment Date">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblCommentDateHeader" runat="server" ForeColor="Black" CommandArgument="Comment_Date"
                                                    CommandName="Sort">Comment Date&nbsp;</asp:LinkButton>
                                                <img id="Comment_Date" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCommentDate" Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("Comment_Date"))) %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comment By">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblCommentByHeader" runat="server" ForeColor="Black" CommandArgument="Comment_By"
                                                    CommandName="Sort">Comment By&nbsp;</asp:LinkButton>
                                                <img id="Comment_By" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblComment_By" Text='<%#Eval("Comment_By")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comment Vessel">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblCommentVesselHeader" runat="server" ForeColor="Black" CommandArgument="Comment_Vessel"
                                                    CommandName="Sort">Comment Vessel&nbsp;</asp:LinkButton>
                                                <img id="Comment_Vessel" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblComment_Vessel" Text='<%#Eval("Comment_Vessel")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblComment" Text='<%#Eval("Remark")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="400px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:HyperLink ID="lnkAttachment" ToolTip="Attachment" runat="server" ImageUrl="~/Images/Attach.png" NavigateUrl='<%# "~/Uploads/CrewDocuments/" + Eval("AttachmentPath").ToString()%>'
                                                                Target="_blank" Visible='<%#Eval("AttachmentPath").ToString()==""?false:true%>' />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgAdd" runat="server" Text="AddComment" OnCommand="OnAddComment"
                                                                CommandArgument='<%#Eval("[CrewID]")%>' ForeColor="Black" ToolTip="Add" ImageUrl="~/Images/Add.gif"
                                                                Height="16px"></asp:ImageButton>
                                                            <%--    Visible='<%# uaEditFlag %>'--%>
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
                                
            
       

        <div id="dvPopupAddFeedback" style="display: none; font-family: Tahoma; text-align: left;
            font-size: 12px; color: Black;" >
            <table style="background-color: white;">
                <tr>
                    <td>
                        Feedback/Note
                    </td>
                </tr>
                <tr>
                    <td>
                     <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                 <asp:TextBox ID="txtCrewRemarks" runat="server" TextMode="MultiLine" Height="200px" Width="450px"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvCrewRemarks" runat="server"
                                    ValidationGroup="V1" Display="None" ErrorMessage="Remarks is mandatory!"
                                    ControlToValidate="txtCrewRemarks" InitialValue=""></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="v1"
                                    TargetControlID="rfvCrewRemarks" runat="server">
                                </ajaxToolkit:ValidatorCalloutExtender>
                        
                            </ContentTemplate>
                        </asp:UpdatePanel>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        Select Voyage:
                        <asp:UpdatePanel ID="updDDlVoyages" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlVoyages" runat="server" DataTextField="voyage_name" DataValueField="ID"
                                    Width="215px">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:FileUpload ID="CrewRemarks_FileUploader" runat="server" Width="400px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="btnSaveRemarks" runat="server" Text="Save" ValidationGroup="V1" OnClick="btnSaveRemarks_Click">
                        </asp:Button>
                        <asp:Button ID="btnSaveAndCloseRemarks" runat="server" Text="Save & Close" ValidationGroup="V1" OnClick="btnSaveAndCloseRemarks_Click">
                        </asp:Button>
                    </td>
                </tr>
                <tr >
                    <td align="left">
                        <div class="error-message" onclick="javascript:this.style.display='none';">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
      <%--  </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ImgExpExcel" />
            </Triggers>
       </asp:UpdatePanel>--%>
    </div>
</asp:Content>
