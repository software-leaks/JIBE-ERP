<%@ Page Title="FMS Office Approval" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FMSScheduleFileApproval.aspx.cs" Inherits="FMS_FMSScheduleFileApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">


        function validation() {

            if (document.getElementById("ctl00_MainContent_txtRemark").value == "") {
                alert("Please enter remark.");
                document.getElementById("ctl00_MainContent_txtRemark").focus();
                return false;
            }

            return true;
        }

        function DocOpen(filename) {

            var filepath = "../FMS/";
            //alert(filepath + filename);
            window.open(filepath + filename);
        }
        function RefreshPage() {
            window.location.reload()
        }

    </script>
    <style type="text/css">
      .page
        {
           height:100%;
        }
     
     
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        FMS Office Approval
    </div>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;
        height: 100%;">
        <div style="height: 650px; width: 100%; color: Black;">
            <asp:UpdatePanel ID="UpdUserType" runat="server">
                <ContentTemplate>
                    <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                        <table width="100%" cellpadding="4" cellspacing="4">
                            <tr>
                                <td align="right" style="width: 25%">
                                    Search by File Name :&nbsp;
                                </td>
                                <td align="left" style="width: 20%">
                                    <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                        WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                </td>
                                <td align="right" style="width: 10%">
                                    My Approvals :&nbsp;
                                </td>
                                <td align="left">
                                    <asp:CheckBox ID="chkMyApproval" runat="server" CssClass="bckColor" Checked="true" />
                                </td>
                                <td align="left" style="width: 20%">
                                    <asp:RadioButtonList ID="optApprove" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Text="Pending Approval" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Approved"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="center" style="width: 5%">
                                    <asp:ImageButton ID="btnFilter" runat="server" TabIndex="0" OnClick="btnFilter_Click"
                                        ToolTip="Search" ImageUrl="~/Images/SearchButton.png" Style="height: 21px" />
                                </td>
                                <td align="center" style="width: 5%">
                                    <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                        ImageUrl="~/Images/Refresh-icon.png" />
                                </td>
                                <td style="width: 5%">
                                    <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                        ImageUrl="~/Images/Exptoexcel.png" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div >
                        <%--<div style="text-align: right; width: 100%">
                                <asp:Button ID="btnApprove" Text="Approve File(s)" Height="30px" runat="server" Width="250px"
                                    OnClick="btnApprove_Click" />
                            </div>--%>
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grdApprovalList" runat="server" EmptyDataText="NO RECORDS FOUND"
                                        AutoGenerateColumns="False" OnRowDataBound="grdApprovalList_RowDataBound" DataKeyNames="Approval_Level,FilePath,ApprovarID"
                                        CellPadding="2" Width="100%" OnSorting="grdApprovalList_Sorting" AllowSorting="True"
                                        OnRowCommand="grdApprovalList_RowCommand">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                        <PagerStyle CssClass="PMSPagerStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Vessel">
                                                <HeaderTemplate>
                                                   File Name
                                                    <img id="ID" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblLogFileID" runat="server" Text='<%#Eval("FileName")%>' Style="color: Black"></asp:LinkButton>
                                                    <asp:Label ID="lblFileID" runat="server" Text='<%#Eval("FileID")%>' Visible="false"
                                                        Style="color: Black"></asp:Label>
                                                    <asp:Label ID="lblFilePath" runat="server" Text='<%#Eval("FilePath")%>' Visible="false"
                                                        Style="color: Black"></asp:Label>
                                                    <asp:Label ID="lblVersion" runat="server" Text='<%#Eval("Version")%>' Visible="false"
                                                        Style="color: Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Schedule Date" Visible="true">
                                                <HeaderTemplate>
                                                    Schedule Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                
                                                        <asp:Label ID="lblSchDate" runat="server" Text=' <%#Eval("Schedule_Date")%>' Visible="true"
                                                                Style="color: Black"></asp:Label>
                                                
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Completion Date" Visible="true">
                                                <HeaderTemplate>
                                                    Completion Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                 <asp:Label ID="lblCompDate" runat="server" Text=' <%#Eval("Completion_Date")%>' Visible="true"
                                                        Style="color: Black"></asp:Label>
                                                  
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Version">
                                                <HeaderTemplate>
                                                    Version
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("Version")%>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vessel Name" Visible="true">
                                                <HeaderTemplate>
                                                    Vessel Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approver Name">
                                                <HeaderTemplate>
                                                    Approver Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("ApproverName")%>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approver Date">
                                                <HeaderTemplate>
                                                    Approved Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("Date_Of_Approval")%>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approve/Rework">
                                                <ItemTemplate>
                                                    <asp:Button ID="BtnApprove" Text="A" Enabled='<%#Convert.ToString(Eval("Approval_Status"))=="0"?true : false %>'
                                                        runat="server" CommandName="Approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        ToolTip="Approve" />
                                                    <asp:Button ID="BtnRework" Text="R" Enabled='<%#Convert.ToString(Eval("Approval_Status"))=="0"?true : false %>'
                                                        runat="server" CommandName="Rework" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        ToolTip="Rework" />
                                                    <asp:Image ID="Image1" ImageUrl="~/Images/Ok-icon.png" Height="16px" Visible='<%#Convert.ToString(Eval("Approval_Status"))=="1"?true : false %>'
                                                        runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status ID" Visible="false">
                                                <HeaderTemplate>
                                                    Status ID
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatusID" runat="server" Text='<%#Eval("Status_ID") %>'></asp:Label>
                                                    <asp:Label ID="lblOfficeID" runat="server" Text='<%#Eval("Office_ID") %>'></asp:Label>
                                                    <asp:Label ID="lblVesselID" runat="server" Text='<%#Eval("Vessel_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approval Level" Visible="false">
                                                <HeaderTemplate>
                                                    Approval Level
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApprovalLevel" runat="server" Text='<%#Eval("Approval_Level") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindGrid" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <br />
                        <br />
                        <br />
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grdApprovedList" runat="server" EmptyDataText="NO RECORDS FOUND"
                                        AutoGenerateColumns="False" DataKeyNames="Approval_Level,FilePath,ApprovarID"
                                        CellPadding="2" Width="100%" OnSorting="grdApprovedList_Sorting" 
                                        AllowSorting="True" onrowdatabound="grdApprovedList_RowDataBound">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                        <PagerStyle CssClass="PMSPagerStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Vessel">
                                                <HeaderTemplate>
                                                   <%-- <asp:LinkButton ID="lblLogFileIDHeader" runat="server" CommandName="Sort" CommandArgument="ID"
                                                        ForeColor="Black">File Name&nbsp;</asp:LinkButton>--%>
                                                        File Name
                                                    <img id="ID" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblLogFileID" runat="server" Text='<%#Eval("FileName")%>' Style="color: Black"></asp:LinkButton>
                                                    <asp:Label ID="lblFileID" runat="server" Text='<%#Eval("FileID")%>' Visible="false"
                                                        Style="color: Black"></asp:Label>
                                                    <asp:Label ID="lblFilePath" runat="server" Text='<%#Eval("FilePath")%>' Visible="false"
                                                        Style="color: Black"></asp:Label>
                                                    <asp:Label ID="lblVersion" runat="server" Text='<%#Eval("Version")%>' Visible="false"
                                                        Style="color: Black"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Schedule Date" Visible="true">
                                                <HeaderTemplate>
                                                    Schedule Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                  <asp:Label ID="lblSchDate" runat="server" Text=' <%#Eval("Schedule_Date")%>' Visible="true"
                                                        Style="color: Black"></asp:Label>
                                                   
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Completion Date" Visible="true">
                                                <HeaderTemplate>
                                                    Completion Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompDate" runat="server" Text=' <%#Eval("Completion_Date")%>' Visible="true"
                                                        Style="color: Black"></asp:Label> 
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Version">
                                                <HeaderTemplate>
                                                    Version
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("Version")%>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vessel Name" Visible="true">
                                                <HeaderTemplate>
                                                    Vessel Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approver Name">
                                                <HeaderTemplate>
                                                    Approver Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("ApproverName")%>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approver Date">
                                                <HeaderTemplate>
                                                    Approved Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#Eval("Date_Of_Approval")%>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approve/Rework">
                                                <ItemTemplate>
                                                    <%--<asp:CheckBox ID="chkStatus" Enabled='<%#Convert.ToString(Eval("Approval_Status"))=="0"?true : false %>'
                                                        Visible='<%#Convert.ToString(Eval("Approval_Status"))=="0"?true : false %>' runat="server" />--%>
                                                    <asp:Image ID="Image1" ImageUrl="~/Images/Ok-icon.png" Height="16px" Visible='<%#Convert.ToString(Eval("Approval_Status"))=="1"?true : false %>'
                                                        runat="server" />
                                                     
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status ID" Visible="false">
                                                <HeaderTemplate>
                                                    Status ID
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatusID" runat="server" Text='<%#Eval("Status_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approval Level" Visible="false">
                                                <HeaderTemplate>
                                                    Approval Level
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApprovalLevel" runat="server" Text='<%#Eval("Approval_Level") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <uc1:ucCustomPager ID="ucCustomPagerApprovedList" runat="server" PageSize="30" OnBindDataItem="ApprovedBindGrid" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <br />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ImgExpExcel" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div id="dvRemark" title="Add Remark" style="width: 300px; display: none;">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtRemark" runat="server" Width="95%" Height="200px" CssClass="txt"
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSaveRemark" runat="server" Text="Save" OnClick="btnSaveRemark_Click">
                                            </asp:Button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="dvRRemark" title="Add Remark" style="width: 300px; display: none;">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtRRemark" runat="server" Width="95%" Height="200px" CssClass="txt"
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSaveRRemark" runat="server" Text="Save" OnClick="btnSaveRRemark_Click">
                                            </asp:Button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
    </div>
</asp:Content>
