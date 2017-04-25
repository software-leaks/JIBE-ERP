<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewCompanySeniorityReward.aspx.cs" Inherits="Crew_CrewCompanySeniorityReward"   EnableViewState="true" MasterPageFile="~/Site.master"   Title="Company Seniority Reward"  %>

<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <!--Javascript Files-->
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
        Crew Company Seniority Reward
    </div>
 <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">
     
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="dvFilter" style="border: 1px solid #cccccc; margin: 2px;">
                    <table width="100%" border="0">
                        <tr>                            
                            <td align="left"  style="width:200px">
                                Seniority Year :
                                <asp:DropDownList ID="ddlSeniorityYear" runat="server" Width="100px" >
                                </asp:DropDownList>
                            </td>
                            <td  align="left"  style="width:90px">Reward Status :</td>
                            <td align="left"  style="width:250px"> 
                                <asp:RadioButtonList ID="rblRewardStatus" runat="server"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                                    <asp:ListItem Value="Rewarded">Rewarded</asp:ListItem>
                                    <asp:ListItem Value="Pending" >Pending</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="left"  style="width:200px">
                                Search:
                               <asp:TextBox ID="txtSearchText" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td align="left"  style="width:200px">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" width= "90px" CssClass="btnCSS" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnClearFilter" runat="server" Text="Clear Filter" width= "90px" CssClass="btnCSS" OnClick="btnClearFilter_Click" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="dvMain" style="border: 1px solid #cccccc; margin: 2px;">
                    <asp:GridView ID="gvSeniorityRecords" DataKeyNames="SeniorityYear,CrewID,Rank_Short_Name,RankId,CrewSeniorityRewardId" runat="server" AutoGenerateColumns="False"
                        CellPadding="4" AllowPaging="false" Width="100%" ShowFooter="true"  OnRowEditing="GridView_RowEditing"
                        EmptyDataText="No Record Found" CaptionAlign="Bottom" GridLines="None"
                        OnPageIndexChanging="gvSeniorityRecords_PageIndexChanging" ForeColor="#333333"
                       >
                        <Columns>
                            
                            <asp:TemplateField HeaderText="S/C" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblSC" runat="server" NavigateUrl='<%# "../Crew/CrewDetails.aspx?ID=" + Eval("CrewID").ToString()  %>' CssClass="staffInfo"
                                        Target="_blank" Text='<%# Eval("STAFF_CODE")%>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Staff Name" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Staff_fullName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Seniority Year" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeniorityYear" runat="server" Text='<%# Eval("SeniorityYear")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Seniority Days" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeniorityDays" runat="server" Text='<%# Eval("SeniorityDays")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblEffectiveDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Effective_date"))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rewarded On" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRewardedOn" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("RewardedON"))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Reward Status" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                 <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Eval("RewardStatus")%>' CausesValidation="False"
                                            CommandName="Edit"  />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                        <PagerStyle CssClass="PagerStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                    </asp:GridView>
                    <uc1:ucCustomPager ID="ucCustomPager" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Staff"
                                    AlwaysGetRecordsCount="true" OnBindDataItem="Load_SeniorityRecords" />
                </div>
             </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:UpdatePanel ID="UpdatePnl" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="divSeniorityReward" style="font-family: Tahoma; color: black; display: none;">
                    <center>
                        <div class="error-message" onclick="javascript:this.style.display='none';">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                        <div style="font-family: Tahoma; font-size: 12px; border: 1px solid Gray; width: auto">
                            <div style="padding: 0px; padding: 2px; border-top: 0; background-color: #5588BB;
                                color: #FFFFFF; text-align: center;">
                                <b>Seniority Reward</b>
                            </div>
                     </center>
                     <table>    
                        <tr>
                            <td>
                                   Name :
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" Enabled="false" Width="450px"> </asp:TextBox>
                                <asp:TextBox ID="txtCrewId" runat="server" Visible="false" Width="450px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                   Rank : 
                            </td>
                            <td>
                                <asp:TextBox ID="txtRank" runat="server" Enabled="false" Width="450px"></asp:TextBox>
                                <asp:TextBox ID="txtRankId" runat="server" Visible="false" Width="450px"></asp:TextBox>
                            </td>
                        </tr>   
                        <tr>
                            <td>
                                   Seniority Year :
                            </td>
                            <td>
                                <asp:TextBox ID="txtSeniorityYear" runat="server" Enabled="false"
                                    Width="450px">
                                </asp:TextBox>
                            </td>
                        </tr>                       
                        <tr>
                            <td>
                                   Remarks :
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="200px"
                                    Width="450px">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ValidationGroup="validate1" Display="None" ErrorMessage="Remarks is mandatory!"
                                            ControlToValidate="txtRemarks" InitialValue=""></asp:RequiredFieldValidator>
                                <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender1"
                                    TargetControlID="RequiredFieldValidator1" runat="server">
                                </tlk4:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 Attachment :
                            </td>
                            <td>
                             <asp:HyperLink ID="lnkAttachment" runat="server"  Target="_blank"  />
                             <tlk4:AjaxFileUpload ID="AttachmentUploader" runat="server" Font-Size="11px" ThrobberID="myThrobber" Enabled="false" OnUploadComplete="AjaxFileUpload1_OnUploadComplete" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align ="center" >
                                </hr>
                                <div style="background-color: #F0F0F0">
                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" Text="Save" runat="server"   ValidationGroup="validate1" />
                                </div>
                            </td>
                        </tr>
                     </table>
                 </div>
                </ContentTemplate>
 </asp:UpdatePanel>  
 
</asp:Content>