<%@ Page Title="FMS File Approval" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FMSFilesApproval.aspx.cs" Inherits="FMSFilesApproval" %>

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
    <script  type="text/javascript">


        function OnApprove() {

            if (document.getElementById($('[id$=txtRemark]').attr('id')).value == "") {
                alert("Please enter remark.");
                document.getElementById($('[id$=txtRemark]').attr('id')).focus();
                return false;
            }

            return true;
        }

        function DocOpen(filename) {

            var filepath = "../FMS/";
            //alert(filepath + filename);
            window.open(filepath + filename);
        }

        function OnReject() {

            if (document.getElementById($('[id$=txtRRemark]').attr('id')).value == "") {
                alert("Please enter remark.");
                document.getElementById($('[id$=txtRRemark]').attr('id')).focus();
                return false;
            }

            return true;
        }

    </script>

     <style type="text/css">
      .page
        {
           height:100%;
           width:1400px;
        }
     
     
     </style>
       
       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
          FMS File Approval Status
    </div>
    <center>
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
                                    <td align="right" style="width: 15%">
                                        Search by File Name :&nbsp;
                                    </td>
                                    <td align="left" style="width: 20%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                      <td align="right" style="width: 8%">
                                        My Approvals :&nbsp;
                                    </td>
                                    <td align="left" style="width: 5%">
                                         <asp:CheckBox ID="chkMyApproval" runat="server" CssClass="bckColor" Checked="true" />
                                                                                 
                                    </td>
                                    <td align="left" style="width: 20%">
                                        <asp:RadioButtonList ID="optApprove" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Text="Pending Approval" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Approved"></asp:ListItem>
                                            <asp:ListItem Value="-1" Text="Rejected"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" TabIndex="0" OnClick="btnFilter_Click"
                                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
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
                     <div style="margin-left: auto; margin-right: auto; text-align: center;">
                          <%--  <div style="text-align: right; width: 100%">
                                <asp:Button ID="btnApprove" Text="Approve File(s)" Height="30px" runat="server" Width="150px"
                                    OnClick="btnApprove_Click" />
                                    &nbsp;&nbsp;
                                   <asp:Button ID="btnReject" Text="Reject File(s)" Height="30px" runat="server" Width="150px"
                                    OnClick="btnReject_Click" />
                            </div>--%>
                            <div>
                                <asp:GridView ID="gvFMSFile" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvFMSFile_RowDataBound" DataKeyNames="ParentID,LevelID,FilePath,ApproverID" CellPadding="2" CellSpacing="0" 
                                    Width="100%" GridLines="both" OnSorting="gvFMSFile_Sorting" AllowSorting="true"     OnRowCommand="gvFMSFile_RowCommand">
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


                                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lnkLogFileID" runat="server" Text='<%#Eval("LogFileID")%>' Style="color: Black"  CommandName="DownladFile"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  Visible="false"></asp:LinkButton>
                                                        <asp:HyperLink ID="lnkLogFileID1" runat="server" Target="_blank" Visible="false" Style="color: Black"></asp:HyperLink>
                                                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' Visible="false" Style="color: Black"></asp:Label>
                                                        <asp:Label ID="lblFilePath" runat="server" Text='<%#Eval("FilePath")%>' Visible="false"  Style="color: Black"></asp:Label>

                                                        <asp:Label ID="lblVersion" runat="server" Text='<%#Eval("Version")%>' Visible="false" Style="color: Black"></asp:Label>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="lnkLogFileID" />
                                                    </Triggers>
                                                    </asp:UpdatePanel>


                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="File Path" Visible="true">
                                            <HeaderTemplate>
                                                File Path
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("FilePath")%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Created Date">
                                            <HeaderTemplate>
                                                Created Date
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("LogDate")%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
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
                                                Approved/Rejected Date
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("Date_Of_Approval")%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Created Date">
                                            <HeaderTemplate>
                                              Remark
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("Remark")%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approve/Reject">
                                            <ItemTemplate>
                                            <%--    <asp:CheckBox ID="chkStatus" Enabled= '<%#Convert.ToString(Eval("Approval_Status"))=="0"?true : false %>'
                                                     Visible='<%#Convert.ToString(Eval("Approval_Status"))=="0"?true : false %>' runat="server" />--%>
                                                <asp:Image ImageUrl="~/Images/Ok-icon.png" Height="16px"  Visible='<%#Convert.ToString(Eval("Approval_Status"))=="1"?true : false %>'
                                                    runat="server" />
                                                      <asp:Image ID="Image1" ImageUrl="~/Images/DeleteRed.png" Height="16px"  Visible='<%#Convert.ToString(Eval("Approval_Status"))=="-1"?true : false %>'
                                                    runat="server" />
                                                  <asp:Button ID="BtnApprove" Text="A" Enabled='<%#Convert.ToString(Eval("Approval_Status"))=="0"?true : false %>'
                                                        runat="server" CommandName="Approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        ToolTip="Approve" Visible='<%#Convert.ToString(Eval("Approval_Status"))=="1"|| Convert.ToString(Eval("Approval_Status"))=="-1"?false : true %>' />
                                                    <asp:Button ID="BtnReject" Text="R" Enabled='<%#Convert.ToString(Eval("Approval_Status"))=="0"?true : false  %>'
                                                        runat="server" CommandName="Reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        ToolTip="Reject" Visible='<%#Convert.ToString(Eval("Approval_Status"))=="1" || Convert.ToString(Eval("Approval_Status"))=="-1" ?false : true %>' />
                                            </ItemTemplate> 
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindGrid" />
                             
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
                                            <asp:Button ID="btnSaveRemark" runat="server" Text="Save" OnClick="btnSaveRemark_Click" OnClientClick="return OnApprove();">
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
                                            <asp:Button ID="btnSaveRRemark" runat="server" Text="Save" OnClick="btnSaveRRemark_Click" OnClientClick="return OnReject();">
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
    </center>
</asp:Content>
