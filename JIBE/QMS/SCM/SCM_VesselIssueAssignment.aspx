<%@ Page Title="Vessel Issue Assignment" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SCM_VesselIssueAssignment.aspx.cs" Inherits="QMS_SCM_SCM_VesselIssueAssignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Import Namespace="SMS.Business.QMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function ValidationOnUpdateResponse() {

            var Year = document.getElementById("ctl00_MainContent_ddlYear").value;
            var Month = document.getElementById("ctl00_MainContent_ddlMonth").value;


            if (Year == "0") {
                alert('Year is required.')
                return false;
            }


            if (Month == "0") {
                alert('Month is required.')
                return false;
            }

            return true;
        }


       
     
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Vessel Issue Assignment
    </div>
 <asp:UpdateProgress ID="upUpdateProgress" runat="server">
                <ProgressTemplate>
                    <div id="blur-on-updateprogress">
                        &nbsp;</div>
                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                        color: black">
                        <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
    <center>
        <div style="font-family: Tahoma; font-size: 12px; width: 100%; height: 850px">
       
            <div style="border: 1px solid gray;">
                <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table cellpadding="1" cellspacing="1" width="100%" style="color: Black;">
                            <tr>
                                <td style="width: 6%" align="right">
                                    Fleet : &nbsp; &nbsp;
                                </td>
                                <td style="width: 5%" align="left">
                                    <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged" Width="120px" Height="20px"
                                        Font-Size="11px" BackColor="#FFFFCC">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 6%" align="right">
                                    Month : &nbsp;
                                </td>
                                <td align="left" style="width: 10%">
                                    <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="true" Width="80px"
                                        Height="20px" Font-Size="11px" BackColor="#FFFFCC">
                                        <asp:ListItem Selected="True" Value="0" Text="--SELECT ALL--"></asp:ListItem>
                                        <asp:ListItem Value="01" Text="January"></asp:ListItem>
                                        <asp:ListItem Value="02" Text="February"></asp:ListItem>
                                        <asp:ListItem Value="03" Text="March"></asp:ListItem>
                                        <asp:ListItem Value="04" Text="April"></asp:ListItem>
                                        <asp:ListItem Value="05" Text="May"></asp:ListItem>
                                        <asp:ListItem Value="06" Text="June"></asp:ListItem>
                                        <asp:ListItem Value="07" Text="July"></asp:ListItem>
                                        <asp:ListItem Value="08" Text="August"></asp:ListItem>
                                        <asp:ListItem Value="09" Text="September"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 6%">
                                    Dept : &nbsp; &nbsp;
                                </td>
                                <td align="left" style="width: 6%">
                                    <asp:DropDownList ID="DDLOfficeDept" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="DDLOfficeDept_SelectedIndexChanged" Width="120px" Height="20px"
                                        Font-Size="11px" BackColor="#FFFFCC">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    <div style="border: 1px solid gray; width: 280px">
                                        <asp:RadioButtonList ID="optAssignDept" runat="server" Font-Size="11px" RepeatDirection="Horizontal"
                                            Width="280px" BackColor="#FFFFCC">
                                            <asp:ListItem Selected="True" Text="ALL" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Assign Dept." Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Not Assign Dept." Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </td>
                                <td align="right" style="width: 6%">
                                    <asp:Button ID="btnRetrieve" runat="server" Font-Size="11px" Height="22px" OnClick="btnRetrieve_Click"
                                        Text="Search" Width="80px" />
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnRqstUptResponse" runat="server" Font-Size="11px" Height="20px"
                                        Text="Rqst. to Upd. Reps" Width="110px" OnClientClick="return ValidationOnUpdateResponse();"
                                        OnClick="btnRqstUptResponse_Click" />
                                </td>
                                <td align="center" style="width: 5%">
                                    &nbsp;
                                </td>
                                <td align="center" style="width: 5%">
                                    <asp:ImageButton ID="btnExport" runat="server" Height="22px" ImageUrl="~/Images/XLS.jpg"
                                        OnClick="btnExport_Click" ToolTip="Export to Excel" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Vessel : &nbsp; &nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                        Width="120px" Height="20px" Font-Size="11px" BackColor="#FFFFCC">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    Year : &nbsp; &nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="true" Width="80px"
                                        Height="20px" Font-Size="11px" BackColor="#FFFFCC">
                                        <asp:ListItem Selected="True" Value="0" Text="--ALL--"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    Search By : &nbsp; &nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSearchBy" BackColor="#FFFFCC" Width="116px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <div style="border: 1px solid gray; width: 280px">
                                        <asp:RadioButtonList ID="optSMSReview" runat="server" Font-Size="11px" RepeatDirection="Horizontal"
                                            Width="280px" BackColor="#FFFFCC">
                                            <asp:ListItem Selected="True" Text="ALL" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="SMS Review" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Non SMS Review" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnClearFilter" runat="server" Font-Size="11px" Height="20px" OnClick="btnClearFilter_Click"
                                        Text="Clear Filters" Width="80px" />
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnUnMarkSMSReview" runat="server" Font-Size="11px" Height="22px"
                                        OnClick="btnUnMarkSMSReview_Click" Text="Save SMS Review" Width="110px" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkCheckAll" runat="server" OnClick="lnkCheckAll_Click">Select All</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkUnChekAll" runat="server" OnClick="lnkUnChekAll_Click">De Select All</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="border: 1px solid gray; margin-top: 2px; height: 720px;">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div style="overflow-x: hidden; border: 0px solid gray; width: 100%">
                            <asp:GridView ID="gvIssueAssign" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" OnRowDataBound="gvIssueAssign_RowDataBound" 
                                Width="100%" AllowSorting="True" OnSorting="gvIssueAssign_Sorting" DataKeyNames="ResponseID"
                                OnRowCreated="gvIssueAssign_RowCreated" OnSelectedIndexChanging="gvIssueAssign_SelectedIndexChanging"
                                CellPadding="2" CellSpacing="2">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblVesseleNameHeader" runat="server" ForeColor="Black" CommandArgument="Vessel_Name"
                                                CommandName="Sort">Vessel&nbsp;</asp:LinkButton>
                                            <img id="Vessel_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblResponseID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ResponseID") %>'></asp:Label>
                                            <asp:Label ID="lblTabIssueID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.TabIssueID") %>'></asp:Label>
                                            <asp:Label ID="lblVesselID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_ID") %>'></asp:Label>
                                            <asp:Label ID="lblVessel" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                                            <asp:Label ID="lblVslIssueTooltip" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.TooltipVesselIssue") %>'></asp:Label>
                                            <asp:Label ID="lblOfficeResponseTooltip" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.TooltipOfficeResponse") %>'></asp:Label>
                                            <asp:Label ID="lblDeptID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DEPT_ID") %>'></asp:Label>
                                            <asp:Label ID="lblReleaseFlag" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RELEASE_FLAG") %>'></asp:Label>
                                            <asp:Label ID="lblLink" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LINK") %>'></asp:Label>
                                            <asp:Label ID="lblSmsReviewFlag" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SMS_NEXT_REVIEW") %>'></asp:Label>
                                            <asp:Label ID="lblCreatedByID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CreatedByID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Month">
                                        <HeaderTemplate>
                                            Month
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMonth" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Month") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year">
                                        <HeaderTemplate>
                                            Year
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblYear" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Year") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Assgn Dept.">
                                        <HeaderTemplate>
                                            Assgn Dept.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeptName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Department") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SCM Tab">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblSCMTabHeader" runat="server" ForeColor="Black" CommandArgument="TAB_NAME"
                                                CommandName="Sort">SCM Tab&nbsp;</asp:LinkButton>
                                            <img id="TAB_NAME" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSCMTab" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.TAB_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <HeaderTemplate>
                                            Code
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.Doc_Ref_No")%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="left" Width="130px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vessel Issue">
                                        <HeaderTemplate>
                                            Vessel Issue
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselIssue" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.VesselIssueFullDetails") %>'></asp:Label>
                                            <asp:Label ID="lblVesselIssueFullDetails" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.VesselIssueFullDetails") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="left" Width="400px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created By">
                                        <HeaderTemplate>
                                            Created By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="cursor: pointer">
                                                <asp:Label ID="lblCreatedBy" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Issue_Created_By") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Assign Department" >
                                      <%--  <HeaderTemplate   >
                                        Assign Department
                                            <br />
                                         &nbsp;&nbsp;  C &nbsp;&nbsp;&nbsp;&nbsp; I&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; O&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; S&nbsp;&nbsp;&nbsp;
                                           &nbsp;&nbsp; P&nbsp;&nbsp;&nbsp;&nbsp; T&nbsp;&nbsp;&nbsp;
                                        </HeaderTemplate>--%>
                                        <ItemTemplate>                                        
                                            <asp:RadioButtonList ID="optDepartment" DataSource='<%# BLL_SCM_Report.SCMReportGetOfficeRepresentativeDept()%>'
                                                DataValueField="DEPT_ID" DataTextField="VALUE" RepeatDirection="Horizontal"
                                                AppendDataBoundItems="true" runat="server" OnSelectedIndexChanged="optDepartment_OnSelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:RadioButtonList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SMS Review">
                                        <HeaderTemplate>
                                            SMS Review
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkSMSReview" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindSCMIssueAssignmentSearch" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
