<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="FileIndex.aspx.cs" Inherits="QMSDB_FileIndex" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="JS/common.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../Scripts/iframe.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">


        function OpenProcedureReadPage(ProcedureId, VesselID)
         {

            var url = 'ProcedureReadReport.aspx?FBM_ID=' + ProcedureId;
            OpenPopupWindow('ProcedureReadReport', 'Procedure Read Report', url, 'popup', 500, 900, null, null, true, false, true, Page_Closed);

        }

        function Page_Closed() {
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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

    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
        height: 100%;">
        <div id="page-header" class="page-title">
            <b>Procedures List </b>
        </div>
        <div style="height: 650px; color: Black;">
            <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                <ContentTemplate>
                    <div style="margin: 1px; padding: 1px; border: 1px solid #cccccc;">
                        <table width="100%" cellpadding="1" cellspacing="0">
                            <tr>
                                <td width="100%" valign="top" style="border: 1px solid gray; color: Black">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table width="100%" cellpadding="3" cellspacing="0">
                                                <tr>
                                                    <td width="20%" align="right" valign="top">
                                                        Fleet :
                                                    </td>
                                                    <td width="25%" valign="top" align="left">
                                                        <uc1:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                                            AutoPostBack="true" Height="150" Width="160" />
                                                    </td>
                                                    <td style="vertical-align: top;" align="right">
                                                        Department :
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <asp:DropDownList ID="lstDept" runat="server" Width="230px" AutoPostBack="true" SelectionMode="Multiple"
                                                            OnSelectedIndexChanged="lstDept_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="10%">
                                                        &nbsp;&nbsp;
                                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to Excel" OnClick="ImgExpExcel_Click"
                                                            Width="16px" ImageUrl="~/Images/Excel-icon.png" />
                                                        &nbsp;&nbsp;
                                                        <img src="../Images/Printer.png" title="*Print*" style="cursor: hand;" alt="Print" onclick="PrintDiv('divGrid')" />
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%" align="right" valign="top">
                                                        Vessel :
                                                    </td>
                                                    <td width="25%" valign="top" align="left">
                                                        <uc1:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                                                            AutoPostBack="true" Height="200" Width="160" />
                                                    </td>
                                                    <td style="vertical-align: top;" align="right" width="120px">
                                                        Users :
                                                    </td>
                                                    <td style="vertical-align: top">
                                                        <asp:DropDownList ID="lstUser" runat="server" Width="230px" AutoPostBack="true" DataTextField="UserName"
                                                            DataValueField="UserID" OnSelectedIndexChanged="lstUser_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnSearch" Text="Search" runat="server" ToolTip="Search" OnClick="btnSearch_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle;" align="right">
                                                        Procedure/Folder :
                                                    </td>
                                                    <td style="vertical-align: top; padding-left: 2px">
                                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="input" Width="150px" Height="18px"
                                                            OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </td>
                                                    <td colspan="3">
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divGrid" style="position: relative; z-index: 0; width: 100%;">
                        <asp:GridView ID="gvProcedureList" runat="server" EmptyDataText="NO RECORDS FOUND"
                            AutoGenerateColumns="False" OnRowDataBound="gvProcedureList_RowDataBound" DataKeyNames="PROCEDURE_ID"
                            CellPadding="3" GridLines="None" CellSpacing="0" Width="100%" OnSorting="gvProcedureList_Sorting"
                            AllowSorting="true" Font-Size="11px" CssClass="GridView-css">
                            <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                            <PagerStyle CssClass="PagerStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <Columns>
                                <asp:TemplateField HeaderText="Vessel Code">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblVesseleNameHeader" runat="server" ForeColor="Black" CommandArgument="Vessel_Name"
                                            CommandName="Sort">Vessel&nbsp;</asp:LinkButton>
                                        <img id="Vessel_Name" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVesselCode" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Folder Name">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbtProFolderName" runat="server" CommandArgument="Folder_Name"
                                            ForeColor="Black" CommandName="Sort">Folder Name &nbsp;</asp:LinkButton>
                                        <img id="Folder_Name" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProNmae" runat="server" Text='<%#Eval("Folder_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Procedure Code">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbtProCodeHeader" runat="server" CommandArgument="PROCEDURE_CODE"
                                            ForeColor="Black" CommandName="Sort"> Procedure Code</asp:LinkButton>
                                        <img id="PROCEDURE_CODE" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblProCode" runat="server" Text='<%#Eval("PROCEDURE_CODE")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Procedure Name">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbtProNameHeader" runat="server" CommandArgument="PROCEDURES_NAME"
                                            ForeColor="Black" CommandName="Sort">Procedure Name</asp:LinkButton>
                                        <img id="PROCEDURES_NAME" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbtProlName" runat="server" Text='<%#Eval("PROCEDURES_NAME")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Access User">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lbtAccessUserHeader" runat="server" CommandArgument="User_name"
                                            ForeColor="Black" CommandName="Sort">Access User</asp:LinkButton>
                                        <img id="User_name" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUser" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.User_name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbtDepartmentHeader" runat="server">Department</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepartment" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Department")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Publish Date">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbtPublishDateHeader" runat="server">Publish Date</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPublishDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PUBLISHED_DATE","{0:dd/MMM/yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Publish Version">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblpublishVersionHeader" runat="server">Publish Version</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPublishVersion" runat="server" Text='<%#Eval("PUBLISH_VERSION")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Send To">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbtsendToHeader" runat="server">Publish By</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblsendTo" runat="server" Text='<%#Eval("PUBLISHED_BY")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Send By">
                                    <HeaderTemplate>
                                        <asp:Label ID="lbtSentByHeader" runat="server">Created By</asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSentBy" runat="server" Text='<%#Eval("CREATED_USER")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Send By">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblCreatedDateHeader" runat="server" CommandArgument="CREATED_DATE"
                                            ForeColor="Black" CommandName="Sort">Created Date</asp:LinkButton>
                                        <img id="CREATED_DATE" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CREATED_DATE","{0:dd/MMM/yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" Height="22px">
                                    </HeaderStyle>
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblAction" runat="server">Action</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table cellpadding="1" cellspacing="0">
                                            <tr align="center">
                                                <td>
                                                    <%-- <asp:ImageButton ID="imgEditDocument" runat="server" Text="Select" Height="16px"
                                                        Width="16px" CommandArgument='<%#Eval("PROCEDURE_ID")%>' ForeColor="Black" ToolTip="Edit Document"
                                                        ImageUrl="~/QMSDB/images/edit.gif" />
                                                    --%>
                                                    <asp:Image ID="imgReadInfo" ImageUrl="~/Images/Users.gif" Height="16px" onclick='<%#"OpenProcedureReadPage("+  DataBinder.Eval(Container.DataItem, "PROCEDURE_ID") +","+  DataBinder.Eval(Container.DataItem, "VESSEL_ID") +");"%>'
                                                        Visible='<%#Convert.ToString(Eval("FBM_READ_FLAG"))=="1"?true : false %>' ToolTip="Click to view FBM Read Report" runat="server" />
                                                </td>
                                                <td style="border-color: transparent; width: 10px">
                                                </td>
                                                <td style="border-color: transparent; width: 10px">
                                                    <asp:ImageButton ID="imgViewDocument" runat="server" Height="16px" ToolTip="View Document"
                                                        CommandArgument='<%#Eval("PROCEDURE_ID")%>' ForeColor="Black" ImageUrl="~/QMSDB/images/document_view.png" />
                                                </td>
                                                <td style="border-color: transparent; width: 10px">
                                                </td>
                                                <td style="border-color: transparent">
                                                    <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                        Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;QMS_DB_LIB_PROCEDURES&#39;,&#39; PROCEDURE_ID="+Eval("PROCEDURE_ID").ToString()+"&#39;,event,this)" %>'
                                                        AlternateText="info" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindPublishDoc" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
