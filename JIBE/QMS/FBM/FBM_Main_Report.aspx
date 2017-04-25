<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" ValidateRequest="false"
    EnableEventValidation="false" CodeFile="FBM_Main_Report.aspx.cs" Title="Fleet Broadcast Messages"
    Inherits="QMS_FBM_FBM_Main_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function DocOpen(filename) {

            var filepath = "../../uploads/fbm/";
            //alert(filepath + filename);
            window.open(filepath + filename);
        }


        function showdetails(path) {
            window.open(path);
            return false;
        }

        function OpenFBMReadPage(FBM_ID) {

            var url = 'FBM_Read_Report.aspx?FBM_ID=' + FBM_ID;
            OpenPopupWindow('FBM_Read_Report', 'FBM Read Report', url, 'popup', 500, 900, null, null, true, false, true, Page_Closed);

        }


        function Page_Closed() {
            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <center>
  <div class="page-title">
         Fleet Broadcast Messages
    </div>
<asp:UpdateProgress ID="UpdateProgress1"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                 
                   <img src="../../Images/loader.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
  
        <div style="font-family: Tahoma; font-size: 12px; width: 100%; height: 100%">
          
            <div style="border: 1px solid gray;">
                <asp:UpdatePanel ID="UpdPnlFilter" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table cellpadding="1" cellspacing="1" width="100%" style="color: Black;">
                            <tr>
                                <td style="width: 6%" align="right">
                                    Dept : &nbsp;
                                </td>
                                <td style="width: 5%" align="left">
                                    <asp:DropDownList ID="DDLOfficeDept" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="DDLOfficeDept_SelectedIndexChanged" Width="120px" Height="20px"
                                        Font-Size="11px">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%" align="right">
                                    Primary Cate. : &nbsp;
                                </td>
                                <td align="left" style="width: 14%">
                                    <asp:DropDownList ID="DDLPrimaryCategory" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" OnSelectedIndexChanged="DDLPrimaryCategory_SelectedIndexChanged"
                                        Width="160px" Height="20px" Font-Size="11px">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    <div>
                                        <asp:RadioButtonList ID="optMsgStatus" runat="server" Font-Size="11px" AutoPostBack="true"
                                            RepeatDirection="Horizontal" Width="130px" OnSelectedIndexChanged="optMsgStatus_SelectedIndexChanged">
                                            <asp:ListItem Text="Active" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="In Active" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </td>
                                <td>
                                </td>
                                <td align="center" colspan="2">
                                    <div>
                                        <asp:RadioButtonList ID="optForUser" runat="server" Font-Size="11px" AutoPostBack="true"
                                            RepeatDirection="Horizontal" Width="220px" OnSelectedIndexChanged="optForUser_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Text="ALL" Value="ALL"></asp:ListItem>
                                            <asp:ListItem Text="Company" Value="COMPANY"></asp:ListItem>
                                            <asp:ListItem Text="Office" Value="OFFICE"></asp:ListItem>
                                            <asp:ListItem Text="Ships" Value="SHIP"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </td>
                                <td align="right" style="width: 5%">
                                    From : &nbsp;
                                </td>
                                <td align="left" style="width: 10%">
                                    <asp:TextBox ID="txtFromDate" runat="server" EnableViewState="true" Font-Size="11px"
                                        Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                        TargetControlID="txtFromDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td align="center" style="width: 6%">
                                    <asp:Button ID="btnRetrieve" runat="server" Font-Size="11px" Height="22px" OnClick="btnRetrieve_Click"
                                        Text="Search" Width="60px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Year :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="true" Width="120px"
                                        AutoPostBack="true" Height="20px" Font-Size="11px" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                        
                                        <asp:ListItem Selected="True" Value="0" Text="--ALL--"></asp:ListItem>                                      
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    Secondry Cate. : &nbsp;
                                </td>
                                <td align="left" style="width: 14%">
                                    <asp:DropDownList ID="DDLSecondryCategory" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" Width="160px" Height="20px" Font-Size="11px" OnSelectedIndexChanged="DDLSecondryCategory_SelectedIndexChanged">
                                        
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                </td>
                                <td align="left" colspan="2">
                                    <div>
                                        <asp:RadioButtonList ID="optMsgType" runat="server" Font-Size="11px" AutoPostBack="true"
                                            RepeatDirection="Horizontal" Width="210px" OnSelectedIndexChanged="optMsgType_SelectedIndexChanged">
                                            <asp:ListItem Text="Draft" Selected="True" Value="DRAFT"></asp:ListItem>
                                            <asp:ListItem Text="Pending Approval" Value="PENDINGAPPROVAL"></asp:ListItem>
                                            <asp:ListItem Text="Sent" Value="SENT"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </td>
                                <td align="right" style="width: 10%">
                                    Subject/Body :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSearch" runat="server" Width="140px"></asp:TextBox>
                                </td>
                                <td align="right">
                                    To : &nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtToDate" runat="server" EnableViewState="true" Font-Size="11px"
                                        Width="100px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                        TargetControlID="txtToDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td align="center">
                                    <div style="border: 0px solid gray;">
                                        <asp:HyperLink ID="ImgFBMNewMsg" runat="server" ForeColor="Black" Height="23px" Target="_blank"
                                            onclick="javascript:window.open('FBM_Main_Report_Details.aspx')" ImageUrl="~/Images/FBMNewMsg.png"></asp:HyperLink>
                                    </div>
                                    <div>
                                        New FBM</div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="border: 1px solid gray; margin-top: 2px; cursor: pointer; height: 650px;">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div style="overflow-x: hidden; border: 0px solid gray; width: 100%">
                            <asp:GridView ID="gvFBMReport" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                OnRowDataBound="gvFBMReport_RowDataBound" Width="100%" GridLines="Both" AllowSorting="true"
                                OnSorting="gvFBMReport_Sorting" DataKeyNames="ID" OnRowCreated="gvFBMReport_RowCreated"
                                CellPadding="5" OnSelectedIndexChanging="gvFBMReport_SelectedIndexChanging" OnSelectedIndexChanged="gvFBMReport_SelectedIndexChanged">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgPriority" Visible="false" runat="server" ToolTip="Urgent"
                                                ImageUrl="~/Images/exclamation.gif" Height="14px"></asp:ImageButton>
                                            <asp:Label ID="lblPriority" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.URGENT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="5px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date Sent">
                                        <HeaderTemplate>
                                            Date Sent
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateSent" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DATE_SENT","{0:dd-MM-yyyy}") %>'></asp:Label>
                                            <asp:Label ID="lblFBMID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'></asp:Label>
                                            <asp:Label ID="lblBody" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.BODY") %>'></asp:Label>
                                            <asp:Label ID="lblAttachment" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ATTCHMENTS") %>'></asp:Label>
                                            <asp:Label ID="lblFilePathIfSingle" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FilePathIfSingle") %>'></asp:Label>
                                            <asp:Label ID="lblUserID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CREATED_BY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" FBM No.">
                                        <HeaderTemplate>
                                            FBM No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFBMNumber" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FBM_NUMBER") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="left" Width="140px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <HeaderTemplate>
                                            Category
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrimaryCategory" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PRIMARY_CATEGORY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject">
                                        <HeaderTemplate>
                                            Subject
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubject" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SUBJECT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="left" Width="450px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Body">
                                        <HeaderTemplate>
                                            Body
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgMsgBody" runat="server" Height="16px" ForeColor="Black" ImageUrl="~/Images/FBMMsgBody.png">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Att.">
                                        <HeaderTemplate>
                                            Att.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgFBMAtt" runat="server" Height="16px" ForeColor="Black" ImageUrl="~/Images/attach-icon.png">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active">
                                        <HeaderTemplate>
                                            Active
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblActive" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ACTIVE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Att.">
                                        <HeaderTemplate>
                                            Read Info.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Image ID="imgReadInfo" ImageUrl="~/Images/Users.gif" Height="16px" onclick='<%#"OpenFBMReadPage("+  DataBinder.Eval(Container.DataItem, "ID") +");"%>'
                                                Visible='<%#Convert.ToString(Eval("FBM_READ_FLAG"))=="1"?true : false %>' ToolTip="Click to view FBM Read Report"
                                                runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div style="height:30px;">
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindFBMReport" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
