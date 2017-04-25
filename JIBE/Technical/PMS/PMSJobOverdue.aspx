<%@ Page Title="PMS Jobs Overdue" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PMSJobOverdue.aspx.cs" Inherits="PMSJobOverdue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/ucCustomDropDownList.ascx" TagName="ucfDropdown"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomStringFilter.ascx" TagName="ucfString"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomNumberFilter.ascx" TagName="ucfNumber"
    TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ucCustomDateFilter.ascx" TagName="ucfDate" TagPrefix="CustomFilter" %>
<%@ Register Src="../../UserControl/ctlVesselLocationList.ascx" TagName="ctlVesselLocationList"
    TagPrefix="ucVesslLocation" %>
    
 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>

    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">
        function OnbtnRetrieve() {

            var SearchJobID = document.getElementById("ctl00_MainContent_txtSearchJobID").value;
            if (SearchJobID != "") {
                if (isNaN(SearchJobID)) {
                    alert('Job Id is accept ony numeric value.')
                    return false;
                }

            }
            return true;
        }

        function CloseDiv() {

            var control = document.getElementById("ctl00_MainContent_updpnlDivSuptdResponse");
            control.style.visibility = "hidden";
        }


        function ValidationOnSave() {

            var CompletionDate = document.getElementById("ctl00_MainContent_txtDivModifiedCompletionDate").value;
            var SuptdResponse = document.getElementById("ctl00_MainContent_txtDivSuptdResponse").value;

            if (CompletionDate == "") {
                alert('Modified Completion Date is required.')
                return false;
            }

            if (SuptdResponse == "") {
                alert('Suptd. Remark is required.')
                return false;
            }

            return true;
        }

        function CallPrint(gridview)
         {
             var Var1 = '<%=ConfigurationManager.AppSettings["APP_NAME"].ToString() %>'
             WinPrint = window.open('/' + Var1 + '/PrintGrid.aspx?gv=' + gridview, 'Print', 'letf=0,top=0,width=1024px,height=768px,toolbar=0,scrollbars=0,status=0');
            WinPrint.focus();
            //setTimeout('WinPrint.print();', 4000);
            //setTimeout('WinPrint.close();', 4000);
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
        <div style="font-family: Tahoma; font-size: 12px">
            <div style="border: 1px solid  #5588BB; padding: 0px; background-color: #5588BB;
                color: #FFFFFF; text-align: center;">
                <table>
                    <tr>
                        <td align="center" style="width: 95%">
                            <b>PMS Overdue Jobs</b>
                        </td>
                        <td align="right" style="width: 5%">
                            <asp:HiddenField ID="hfAppName" runat ="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="border: 1px solid #cccccc; height: 50px; padding-top: 15px;">
                <%--<asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>--%>
                <table cellpadding="0" cellspacing="1" width="100%" style="color: Black;">
                    <tr>
                        <td style="width: 5%" align="right">
                            Fleet :&nbsp;&nbsp;
                        </td>
                        <td style="width: 4%" align="left">
                            <asp:DropDownList ID="DDLFleet" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                CssClass="txtInput" Height="20px" Font-Size="11px" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged"
                                Width="120px">
                                <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 8%" align="right">
                            Department : &nbsp;&nbsp;
                        </td>
                        <td align="left" style="width: 15%">
                            <asp:DropDownList ID="DDLJobDepartment" runat="server" AppendDataBoundItems="true"
                                CssClass="txtInput" Font-Size="11px" Width="150px" Height="20px">
                                <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="center" style="width: 18%">
                            <div style="border: 1px solid #cccccc">
                                <asp:RadioButtonList ID="optStatus" runat="server" Font-Size="11px" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="ALL&nbsp;&nbsp;" Value="0"></asp:ListItem>
                                    <asp:ListItem Selected="True" Text="Pending" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="Responded" Value="R"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                        <td style="width: 7%" align="right">
                            Month :&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="true" Width="85px"
                                CssClass="txtInput" Height="20px" Font-Size="11px">
                                <asp:ListItem Selected="True" Value="0" Text="--ALL--"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            Year :&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="true" Width="85px"
                                CssClass="txtInput" Height="20px" Font-Size="11px">
                                <asp:ListItem Selected="True" Value="0" Text="--ALL--"></asp:ListItem>
                                <asp:ListItem Value="2011" Text="2011"></asp:ListItem>
                                <asp:ListItem Value="2012" Text="2012"></asp:ListItem>
                                <asp:ListItem Value="2013" Text="2013"></asp:ListItem>
                                <asp:ListItem Value="2014" Text="2014"></asp:ListItem>
                                <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                                <asp:ListItem Value="2016" Text="2016"></asp:ListItem>
                                <asp:ListItem Value="2017" Text="2017"></asp:ListItem>
                                <asp:ListItem Value="2018" Text="2018"></asp:ListItem>
                                <asp:ListItem Value="2019" Text="2019"></asp:ListItem>
                                <asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                                <asp:ListItem Value="2021" Text="2021"></asp:ListItem>
                                <asp:ListItem Value="2022" Text="2022"></asp:ListItem>
                                <asp:ListItem Value="2023" Text="2023"></asp:ListItem>
                                <asp:ListItem Value="2024" Text="2024"></asp:ListItem>
                                <asp:ListItem Value="2025" Text="2025"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 6%" align="center">
                            <asp:ImageButton ID="btnRetrieve" runat="server" Height="25px" OnClick="btnRetrieve_Click"
                                ImageUrl="~/Images/SearchButton.png" ToolTip="Search" />
                        </td>
                        <td style="width: 6%" align="center">
                            <asp:ImageButton ID="btnClearFilter" runat="server" Height="25px" OnClick="btnClearFilter_Click"
                                ImageUrl="~/Images/filter-delete-icon.png" ToolTip="Clear Filter" />
                        </td>
                        <td style="width: 6%" align="center">
                            <asp:ImageButton ID="btnExport" ImageUrl="~/Images/XLS.jpg" Height="25px" OnClick="btnExport_Click"
                                runat="server" ToolTip="Export to Excel" />
                        </td>
                    </tr>
                </table>
                <%--  </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <div style="margin-top: 2px; border: 1px solid gray; padding: 2px;">
                <div style="overflow-x: hidden; overflow-y: scroll; height: 350px; border: 1px solid gray">
                    <%--<asp:UpdatePanel ID="UpdPnlOverDueGrid" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>--%>
                    <asp:GridView ID="gvOverDueJobs" runat="server" EmptyDataText="NO RECORDS FOUND"
                        AutoGenerateColumns="False" OnRowDataBound="gvOverDueJobs_RowDataBound" Width="100%"
                        GridLines="Both" AllowSorting="true" OnSorting="gvOverDueJobs_Sorting" DataKeyNames="JOB_ID"
                        OnSelectedIndexChanging="gvOverDueJobs_SelectedIndexChanging">
                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                        <RowStyle CssClass="PMSGridRowStyle-css" />
                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Vessel">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lblVesseleNameHeader" runat="server" ForeColor="White" CommandArgument="Vessel_Name"
                                        CommandName="Sort">Vsl.&nbsp;</asp:LinkButton>
                                    <img id="Vessel_Name" runat="server" visible="false" />
                                    <asp:Image ID="imgVesselFilter" AlternateText="Filter" ImageUrl="~/Images/filter-grid.png"
                                        Height="16px" ImageAlign="Middle" onclick="ShowCustomFilterUserControl(event,'ucf_DDLVessel')"
                                        Style="cursor: pointer" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblVesselName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                                    <asp:Label ID="lblVesselCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_ID") %>'></asp:Label>
                                    <asp:Label ID="lblLocationID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LocationID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Year">
                                <HeaderTemplate>
                                    Year
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblJoYear" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Overdue_Year") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Month">
                                <HeaderTemplate>
                                    Mon.
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblJoMonth" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Overdue_Month") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location">
                                <HeaderTemplate>
                                    <asp:Label ID="lblLocationHeader" runat="server" ForeColor="White">Location&nbsp;</asp:Label>
                                    <asp:Image ID="imgLocationFilter" AlternateText="Filter" ImageUrl="~/Images/filter-grid.png"
                                        Height="16px" ImageAlign="Middle" onclick="ShowCustomFilterUserControl(event,'ucf_DLLLocation')"
                                        Style="cursor: pointer" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Location") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="200px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SubSystem">
                                <HeaderTemplate>
                                    <asp:Label ID="lblSubSystemHeader" runat="server" ForeColor="White">SubSystem&nbsp;</asp:Label>
                                    <asp:Image ID="imgSubcatalogueFilter" AlternateText="Filter" ImageUrl="~/Images/filter-grid.png"
                                        Height="16px" ImageAlign="Middle" onclick="ShowCustomFilterUserControl(event,'ucf_DDLSubCatalogue')"
                                        Style="cursor: pointer" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSubSystem" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SubSystem") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="160px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job Code">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lblJobCodeHeader" runat="server" ForeColor="White" CommandArgument="JOB_ID"
                                        CommandName="Sort">Job Code&nbsp;</asp:LinkButton>
                                    <img id="JOB_ID" runat="server" visible="false" />
                                    <asp:Image ID="imgJobIDFilter" AlternateText="Filter" ImageUrl="~/Images/filter-grid.png"
                                        Height="16px" ImageAlign="Middle" onclick="ShowCustomFilterUserControl(event,'ucf_txtSearchJobID')"
                                        Style="cursor: pointer" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblJobCode" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JOB_ID") %>'></asp:LinkButton>
                                    <asp:Label ID="lblOverDueID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OverDue_ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job Title">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lblJobTitleHeader" runat="server" ForeColor="White" CommandArgument="JOB_TITLE"
                                        CommandName="Sort">Job Title&nbsp;</asp:LinkButton>
                                    <img id="JOB_TITLE" runat="server" visible="false" />
                                    <asp:Image ID="imgJobtitleFilter" AlternateText="Filter" ImageUrl="~/Images/filter-grid.png"
                                        Height="16px" ImageAlign="Middle" onclick="ShowCustomFilterUserControl(event,'ucf_txtSearchJobTitle')"
                                        Style="cursor: pointer" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="cursor: pointer">
                                        <asp:Label ID="lblJobTitle" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JOB_TITLE") %>'></asp:Label>
                                    </div>
                                    <asp:Label ID="lblJobDescription" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Job_Description") %>'></asp:Label>
                                    <asp:Label ID="lblOverDueFlage" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OVERDUEFLAGE") %>'></asp:Label>
                                    <asp:Label ID="lblNext30dayFlage" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NEXT30DAYSFLAGE") %>'></asp:Label>
                                    <asp:Label ID="lblMachineryDeatils" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.MachineryDeatils") %>'></asp:Label>
                                    <asp:Label ID="lblModifiedCompletionDateFlage" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.MODIFIEDCOMPLETIONDATEFLAG") %>'></asp:Label>
                                    <asp:Label ID="lblOverDueReason" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Overdue_Reason") %>'></asp:Label>
                                    <asp:Label ID="lblSuptdResponse" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Suptd_Response") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Frequency">
                                <HeaderTemplate>
                                    Freq.
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFrequency" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FREQUENCY") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Frequency Name">
                                <HeaderTemplate>
                                    Freq. Type
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFrequencyName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Frequency_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Done">
                                <HeaderTemplate>
                                    Date
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLastDone" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LAST_DONE","{0:dd/MM/yy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rhrs Done">
                                <HeaderTemplate>
                                    R.Hrs.
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRHrsdone" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RHRS_DONE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Next Due">
                                <HeaderTemplate>
                                    Next Due
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNextDue" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DATE_NEXT_DUE","{0:dd/MM/yy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CMS">
                                <HeaderTemplate>
                                    CMS
                                    <asp:Image ID="imgCMSFilter" AlternateText="Filter" ImageUrl="~/Images/filter-grid.png"
                                        Height="16px" ImageAlign="Middle" onclick="ShowCustomFilterUserControl(event,'ucf_optCMS')"
                                        Style="cursor: pointer" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCms" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CMS") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Critical">
                                <HeaderTemplate>
                                    Critical
                                    <asp:Image ID="imgCriticalFilter" AlternateText="Filter" ImageUrl="~/Images/filter-grid.png"
                                        Height="16px" ImageAlign="Middle" onclick="ShowCustomFilterUserControl(event,'ucf_optCritical')"
                                        Style="cursor: pointer" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCritical" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Critical") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="M/C Rhrs">
                                <HeaderTemplate>
                                    #
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMachineryRHrs" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.MachineryRHrs") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="M/C Rhrs Read On">
                                <HeaderTemplate>
                                    Read On
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMachineryRHrsReadOn" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RHrsReadOn","{0:dd/MM/yy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vessel Recommended completion Date">
                                <HeaderTemplate>
                                    Vessel Recommended completion Date
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTentativecompletionDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Tentative_Completion_Date","{0:dd/MM/yy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank">
                                <HeaderTemplate>
                                    Responded by
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRespondedby" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Actioned_By") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Verified On">
                                <HeaderTemplate>
                                    Approved completion Date
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblModifiedcompletionDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Modified_Completion_Date","{0:dd/MM/yy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblAction" runat="server">Responde</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <span id="lblActionDisplayText" style="height: 15px; width: 100px; color: #FFFF00">
                                                </span>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table cellpadding="1" cellspacing="0">
                                        <tr align="center">
                                            <td style="border-color: transparent">
                                                <asp:ImageButton ID="ImgResponded" runat="server" Text="Select" OnCommand="ImgResponse_Click"
                                                    Height="16px" CommandArgument='<%#Eval("OverDue_ID") + "," + Eval("Job_id")+","+ Eval("Vessel_ID") +","+ Eval("MODIFIEDCOMPLETIONDATEFLAG") %>'
                                                    ForeColor="Black" ToolTip="View Responded" ImageUrl="~/Purchase/Image/Responded.png"
                                                    onmouseover="DisplayActionInHeader('View Responded','gvOverDueJobs')"></asp:ImageButton>
                                                <asp:ImageButton ID="ImgPendingResponse" runat="server" Text="Select" OnCommand="ImgResponse_Click"
                                                    Height="16px" CommandArgument='<%#Eval("OverDue_ID") + "," + Eval("Job_id")+","+ Eval("Vessel_ID") +","+ Eval("MODIFIEDCOMPLETIONDATEFLAG") %>'
                                                    ForeColor="Black" ToolTip="Response" ImageUrl="~/Purchase/Image/Response.png"
                                                    onmouseover="DisplayActionInHeader('Response','gvOverDueJobs')"></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="200px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindOverDueJobs" />
                    <%-- </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnExport" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                </div>
                <%--    <asp:UpdatePanel ID="UpdPnlJobOverdueHistoryGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>--%>
                <div style="border: 0px solid gray; padding: 4px; font-weight: bold; font-size: 12px;
                    margin-top: 5px; color: Black;">
                    Job OverDue History
                </div>
                <div style="overflow-x: hidden; overflow-y: scroll; height: 200px; border: 1px solid gray">
                    <asp:GridView ID="gvOverDueJobHistory" runat="server" EmptyDataText="NO RECORDS FOUND"
                        AutoGenerateColumns="False" OnRowDataBound="gvOverDueJobHistory_RowDataBound"
                        Width="100%" BorderStyle="None" AllowSorting="true" BorderWidth="0" BorderColor="Gray"
                        GridLines="Vertical" OnSorting="gvOverDueJobHistory_Sorting" DataKeyNames="JOB_ID">
                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                        <RowStyle CssClass="PMSGridRowStyle-css" />
                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Vessel">
                                <HeaderTemplate>
                                    Vessel
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblVesselName_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                                    <asp:Label ID="lblVesselCode_OH" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_ID") %>'></asp:Label>
                                    <asp:Label ID="lblLocationID_OH" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LocationID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location">
                                <HeaderTemplate>
                                    <asp:Label ID="lblLocationHeader_OH" runat="server" ForeColor="White">Location&nbsp;</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Location") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="200px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SubSystem">
                                <HeaderTemplate>
                                    <asp:Label ID="lblSubSystemHeader_OH" runat="server" ForeColor="White">SubSystem&nbsp;</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSubSystem_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SubSystem") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="PMSGridItemStyle-css" />
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="160px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job Code">
                                <HeaderTemplate>
                                    Job Code
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblJobCode_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JOB_ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job Title">
                                <HeaderTemplate>
                                    Job Title
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblJobTitle_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JOB_TITLE") %>'></asp:Label>
                                    <asp:Label ID="lblJobDescription_OH" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Job_Description") %>'></asp:Label>
                                    <asp:Label ID="lblOverDueFlage_OH" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OVERDUEFLAGE") %>'></asp:Label>
                                    <asp:Label ID="lblNext30dayFlage_OH" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NEXT30DAYSFLAGE") %>'></asp:Label>
                                    <asp:Label ID="lblMachineryDeatils_OH" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.MachineryDeatils") %>'></asp:Label>
                                    <asp:Label ID="lblOverDueReason_OH" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Overdue_Reason") %>'></asp:Label>
                                    <asp:Label ID="lblSuptdResponse_OH" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Suptd_Response") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Frequency">
                                <HeaderTemplate>
                                    Freq.
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFrequency_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FREQUENCY") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Frequency Name">
                                <HeaderTemplate>
                                    Freq. Type
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFrequencyName_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Frequency_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Done">
                                <HeaderTemplate>
                                    Last Done
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLastDone_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LAST_DONE","{0:dd/MM/yy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rhrs">
                                <HeaderTemplate>
                                    Rhrs
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRHrsdone_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RHRS_DONE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Next Due">
                                <HeaderTemplate>
                                    Next Due
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNextDue_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DATE_NEXT_DUE","{0:dd/MM/yy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CMS" Visible="false">
                                <HeaderTemplate>
                                    CMS
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCms_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CMS") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Critical" Visible="false">
                                <HeaderTemplate>
                                    Critical
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCritical_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Critical") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tentative Completion Date">
                                <HeaderTemplate>
                                    Vessel Recommended Completion Date
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTentativecompletionDate_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Tentative_Completion_Date","{0:dd/MM/yy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank">
                                <HeaderTemplate>
                                    Responded by
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRespondedby_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Actioned_By") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Verified On">
                                <HeaderTemplate>
                                    Approved Completion Date
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblModifiedcompletionDate_OH" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Modified_Completion_Date","{0:dd/MM/yy}") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle BorderColor="#efefef" CssClass="PMSGridItemStyle-css" />
                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <%-- </ContentTemplate>--%>
                <%--   </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdPnlJobHistoryGrid" UpdateMode="Conditional" runat="server">--%>
                <%-- <contenttemplate>--%>
                <div style="border: 0px solid gray; padding: 4px; font-weight: bold; font-size: 12px;
                    margin-top: 5px; color: Black;">
                    Job History
                </div>
                <div style="overflow-x: hidden; overflow-y: scroll; height: 200px; border: 1px solid gray">
                    <asp:GridView ID="gvJobHistory" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                        OnRowDataBound="gvJobHistory_RowDataBound" Width="100%" BorderStyle="None" AllowSorting="true"
                        BorderWidth="0" BorderColor="Gray" GridLines="Vertical" OnSorting="gvJobHistory_Sorting"
                        DataKeyNames="JOB_ID">
                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                        <RowStyle CssClass="PMSGridRowStyle-css" />
                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Vessel">
                                <HeaderTemplate>
                                    Vessel
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblVesselName_H" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                                    <asp:Label ID="lblVesselCode_H" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_ID") %>'></asp:Label>
                                    <asp:Label ID="lblLocationID_H" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LocationID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location">
                                <HeaderTemplate>
                                    <asp:Label ID="lblLocationHeader_H" runat="server" ForeColor="White">Location&nbsp;</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation_H" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Location") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="200px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SubSystem">
                                <HeaderTemplate>
                                    <asp:Label ID="lblSubSystemHeader_H" runat="server" ForeColor="White">SubSystem&nbsp;</asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSubSystem_H" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SubSystem") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="PMSGridItemStyle-css" />
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="160px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job Code">
                                <HeaderTemplate>
                                    Job Code
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblJobCode_H" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JOB_ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job Title">
                                <HeaderTemplate>
                                    Job Title
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblJobTitle_H" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.JOB_TITLE") %>'></asp:Label>
                                    <asp:Label ID="lblJobDescription_H" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Job_Description") %>'></asp:Label>
                                    <asp:Label ID="lblOverDueFlage_H" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OVERDUEFLAGE") %>'></asp:Label>
                                    <asp:Label ID="lblNext30dayFlage_H" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NEXT30DAYSFLAGE") %>'></asp:Label>
                                    <asp:Label ID="lblMachineryDeatils_H" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.MachineryDeatils") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Frequency">
                                <HeaderTemplate>
                                    Freq.
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFrequency_H" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FREQUENCY") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Frequency Name">
                                <HeaderTemplate>
                                    Freq. Type
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFrequencyName_H" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Frequency_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Done">
                                <HeaderTemplate>
                                    Last Done
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLastDone_H" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LAST_DONE","{0:dd-MM-yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rhrs">
                                <HeaderTemplate>
                                    Rhrs
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRHrsdone_H" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RHRS_DONE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Next Due">
                                <HeaderTemplate>
                                    Next Due
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNextDue_H" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DATE_NEXT_DUE","{0:dd-MM-yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CMS">
                                <HeaderTemplate>
                                    CMS
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCms_H" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CMS") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Critical">
                                <HeaderTemplate>
                                    Critical
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCritical_H" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Critical") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department">
                                <HeaderTemplate>
                                    Department
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDepartment" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Department") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank">
                                <HeaderTemplate>
                                    Rank
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRank" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RankName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <HeaderTemplate>
                                    Remarks
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Remarks") %>'></asp:Label>
                                    <asp:Label ID="lblFullRemarks" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.FullRemarks") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle BorderColor="#efefef" CssClass="PMSGridItemStyle-css" />
                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblAction" runat="server">Action</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <span id="lblActionDisplayText" style="height: 15px; width: 200px; color: Red"></span>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table cellpadding="1" cellspacing="0">
                                        <tr align="center">
                                            <td>
                                                <asp:ImageButton ID="ImgSpareUsed" runat="server" Text="Select" Height="16px" Width="16px"
                                                    CommandArgument='<%#Eval("JOB_ID")%>' ForeColor="Black" ToolTip="Spare Used"
                                                    ImageUrl="~/Purchase/Image/ToolBox.png" Visible='<%#DataBinder.Eval(Container.DataItem, "Spare_used_flag").ToString() =="N"? false : true %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="200px" CssClass="PMSGridItemStyle-css">
                                </ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <%--  </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <div id="dvCustumFilterJobOverdue">
                <CustomFilter:ucfDropdown ID="ucf_DLLLocation" Width="250" Height="200" UseJavaScriptForControlAction="false"
                    OnApplySearch="BindOverDueJobs" runat="server" />
                <CustomFilter:ucfDropdown ID="ucf_optCMS" Width="150" Height="60" UseJavaScriptForControlAction="false"
                    OnApplySearch="BindOverDueJobs" runat="server" />
                <CustomFilter:ucfDropdown ID="ucf_DDLVessel" Width="200" Height="200" UseJavaScriptForControlAction="false"
                    OnApplySearch="BindOverDueJobs" runat="server" />
                <CustomFilter:ucfDropdown ID="ucf_DDLSubCatalogue" Width="400" Height="200" UseJavaScriptForControlAction="false"
                    OnApplySearch="BindOverDueJobs" runat="server" />
                <CustomFilter:ucfDropdown ID="ucf_DDLRank" Width="150" Height="200" UseJavaScriptForControlAction="false"
                    OnApplySearch="BindOverDueJobs" runat="server" />
                <CustomFilter:ucfDropdown ID="ucf_optCritical" Width="150" Height="60" UseJavaScriptForControlAction="false"
                    OnApplySearch="BindOverDueJobs" runat="server" />
                <CustomFilter:ucfNumber ID="ucf_txtSearchJobID" Width="200" usejavascriptforcontrolaction="false"
                    OnApplySearch="BindOverDueJobs" runat="server" />
                <CustomFilter:ucfString ID="ucf_txtSearchJobTitle" Width="200" usejavascriptforcontrolaction="false"
                    OnApplySearch="BindOverDueJobs" runat="server" />
            </div>
        </div>
        <asp:UpdatePanel ID="updpnlDivSuptdResponse" UpdateMode="Conditional" Visible="false"
            runat="server">
            <ContentTemplate>
                <asp:UpdatePanel ID="DivSuptdResponse" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <cc1:DragPanelExtender ID="DragPanelExtender1" TargetControlID="PanelContentUserMenu"
                            DragHandleID="PanelDrag" runat="server">
                        </cc1:DragPanelExtender>
                        <asp:Panel ID="PanelContentUserMenu" CssClass="popup-css" Style="position: absolute;
                            left: 40%; top: 20%; z-index: 10; text-align: center; padding: 0px 0px 10px 0px"
                            runat="server" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px">
                            <asp:Panel ID="PanelDrag" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px"
                                BackColor="Azure" Font-Bold="true" Font-Size="11px" Font-Names="verdana" Style="padding: 3px 0px 3px 0px"
                                runat="server">
                                Job Overdue Response
                            </asp:Panel>
                            <div style="max-height: 400px; overflow: auto; width: 550px; padding: 5px;">
                                <center>
                                    <table cellpadding="1" cellspacing="1" width="100%">
                                        <tr>
                                            <td align="right" style="width: 30%">
                                                Job Title :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:TextBox ID="txtDivJobTitle" Width="350px" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                OverDue Reason :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:TextBox ID="txtDivOverDueReason" runat="server" MaxLength="4000" ReadOnly="true"
                                                    TextMode="MultiLine" Font-Names="Tahoma" Font-Size="9.5pt" Height="50px" Width="350px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Vessel Recommended completion Date :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtDivTentativeCompletionDate" runat="server" MaxLength="4000" ReadOnly="true"
                                                    Width="120px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <hr />
                                    <table>
                                        <tr>
                                            <td colspan="2" align="left" style="color: Blue; font-size: 14px; text-decoration: underline">
                                                Office Response
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 30%">
                                                Approved Completion Date :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtDivModifiedCompletionDate" runat="server" MaxLength="4000" Width="120px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CaltxtDivModifiedDate" Format="dd-MM-yyyy" TargetControlID="txtDivModifiedCompletionDate"
                                                    runat="server">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                Suptd Remark :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:TextBox ID="txtDivSuptdResponse" runat="server" MaxLength="4000" TextMode="MultiLine"
                                                    Font-Names="Tahoma" Font-Size="9.5pt" Height="50px" Width="350px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                            </div>
                            <br />
                            <asp:Button ID="btnDivSave" runat="server" Text="Save" OnClientClick="return ValidationOnSave();"
                                OnClick="btnDivSave_Click" />
                            <asp:Button ID="btnDivClose" runat="server" Text="Cancel" OnClick="btnDivClose_Click" />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvJobsDetails" style="display: none; width: 830px;" title=''>
            <iframe id="iFrmJobsDetails" src="" frameborder="0" style="height: 540px; width: 100%">
            </iframe>
        </div>
    </center>
</asp:Content>
