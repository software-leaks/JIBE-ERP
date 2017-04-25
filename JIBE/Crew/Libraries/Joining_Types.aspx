<%@ Page Title="Joining Type" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Joining_Types.aspx.cs" Inherits="Joining_Types" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
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

        function validation() {
            
            if (document.getElementById("ctl00_MainContent_txtJoiningType").value.trim() == "") {
                alert("Please enter joining type.");
                document.getElementById("ctl00_MainContent_txtJoiningType").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtJoiningCode").value.trim() == "") {
                alert("Please enter joining code.");
                document.getElementById("ctl00_MainContent_txtJoiningCode").focus();
                return false;
            }

            var flag = false;
            if (document.getElementById("ctl00_MainContent_chkPBillConsidered").checked == true) {

                var gridView = document.getElementById('<%= GVOffPBill.ClientID %>');
                var checkBoxes = gridView.getElementsByTagName("input");
                for (var i = 0; i < checkBoxes.length; i++) {
                    if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                        flag = true
                    }
                }

                if (flag == false) {
                    alert("Please select a salary component if considering portage bill.");
                    return false;
                }
            }
        
            return true;
        }
        function CheckPBill(Ctrl) {
           
            if (document.getElementById("ctl00_MainContent_chkPBillConsidered").checked == true && document.getElementById("ctl00_MainContent_chkVessselPBill_Considered").checked == true) {
                alert("Either office or vessel portage bill can only be considered.");
                document.getElementById("ctl00_MainContent_" + Ctrl).checked = false;

            }
            else if(Ctrl == "chkPBillConsidered")
                $('[id$=dvPBill]').toggle();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 
 <asp:UpdateProgress ID="UpdateProgress1"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

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
       
        <table width="100%">
        <tr>
        <td style="width:50%">
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
          <div class="page-title">
             Joining Type
          </div>
           <asp:Panel ID="pnlJoininigType" runat="server" DefaultButton="btnFilter">
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Joining Type / Code :&nbsp;
                                    </td>
                                    <td align="left" style="width: 30%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" TabIndex="0" OnClick="btnFilterJoining_Click"
                                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Joining Type" OnClick="ImgAdd_Click"
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
                                <asp:GridView ID="gvJoiningType" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" OnRowDataBound="gvJoiningType_RowDataBound" DataKeyNames="ID"
                                    CellPadding="0" CellSpacing="2" Width="100%" GridLines="both" OnSorting="gvJoiningType_Sorting"
                                    AllowSorting="true" CssClass="gridmain-css">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Joining Type">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblReasonHeader" runat="server" CommandName="Sort" CommandArgument="Joining_Type"
                                                    ForeColor="Black">Joining Type&nbsp;</asp:LinkButton>
                                                <img id="Joining_Type" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblReason" runat="server" Text='<%#Eval("Joining_Type")%>' Style="color: Black"
                                                    CommandArgument='<%#Eval("ID")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Joining Code">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblJCodeHeader" runat="server" CommandName="Sort" CommandArgument="JCode"
                                                    ForeColor="Black">Joining Code&nbsp;</asp:LinkButton>
                                                <img id="JCode" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblJCode" runat="server" Text='<%#Eval("JCode")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:CheckBoxField DataField="SeniorityConsidered" HeaderText="Seniority Considered" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" />
                                        <asp:CheckBoxField DataField="VesselPortageBillConsidered" HeaderText="Vessel PBill Considered" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" />
                                        <asp:CheckBoxField DataField="OfficePortageBillConsidered" HeaderText="Office PBill Considered" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" />
                                        <asp:CheckBoxField DataField="ServiceConsidered" HeaderText="Service Considered" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" />
                                         <asp:CheckBoxField DataField="OperatorExpConsidered" HeaderText="Operator Exp Considered" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" />
                                        <asp:CheckBoxField  DataField="WatchKeepingConsidered" HeaderText="Watch Keeping Considered" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" />
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
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_LIB_JOININGTYPES&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindJoiningType" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 30%">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Joining Type &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:TextBox ID="txtJoiningType" CssClass="txtInput" MaxLength="400" Width="90%"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Joining Code &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:TextBox ID="txtJoiningCode" CssClass="txtInput" MaxLength="40" Width="90%"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" style="width: 15%">
                                        Seniority Considered &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                       
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:CheckBox ID="chkSeniorityConsidered" runat="server"  > </asp:CheckBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" style="width: 15%">
                                        Service Considered &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                       
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:CheckBox ID="chkServiceConsidered" runat="server" > </asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Vessel Portage Bill Considered &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                       
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:CheckBox ID="chkVessselPBill_Considered" runat="server" onchange="javascript:CheckPBill('chkVessselPBill_Considered');" > </asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Office Portage Bill Considered &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                       
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:CheckBox ID="chkPBillConsidered" runat="server"  onchange="javascript:CheckPBill('chkPBillConsidered');" > </asp:CheckBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" style="width: 15%">
                                        Operator Exp Considered &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                       
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:CheckBox ID="chkOperatorExp" runat="server" > </asp:CheckBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" style="width: 15%">
                                        Watch Keeping Considered &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                       
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:CheckBox ID="chkWatchKeeping" runat="server" > </asp:CheckBox>
                                    </td>
                                </tr>
                                <tr><td colspan="3">
                                <div id="dvPBill" style=" display:none;">
                                <asp:GridView ID="GVOffPBill" runat="server" 
                                    AutoGenerateColumns="False" DataKeyNames="Code"
                                    CellPadding="0" CellSpacing="2" Width="100%" GridLines="both" 
                                    CssClass="gridmain-css">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Salary Component">
                                            <ItemTemplate>
                                                <%--<asp:HiddenField ID="hdnCode" runat="server" Value='<%# Eval("Code")%>' />--%>
                                                <asp:Label ID="ilbSalComponent" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Consider">
                                            <ItemTemplate>
                                                 <asp:CheckBox ID="chkPbillAssigned" runat="server" 
                                                                Checked='<%#Eval("ISPBillConsidered").ToString() == "1" ? true : false %>' />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>
                                </div>
                                </td></tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validation();"
                                            OnClick="btnsave_Click" />
                                        <asp:TextBox ID="txtJoiningTypeID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 0px solid #cccccc;
                                            background-color: #FDFDFD">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
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
            </asp:Panel>
        </div>
        </td>
        <td style="width:50%" valign="top">
         <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 500px;
            height: 100%;">
          <div class="page-title">
             Permanent Status
          </div>
           <div style="height: 355px;  color: Black;">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <center>
        <asp:Panel ID="Panel1" runat="server" DefaultButton="ImgbtnFilterBranch">
                                    <table width="100%" cellpadding="4" cellspacing="4">
                                        <tr>
                                            <td style="width:20%; color: Black;">
                                                <b>Permanent Status:</b>&nbsp;<asp:Label ID="lblPermanent" runat="server" />
                                            </td>
                                          
                                            <td align="left" style="width: 30%">
                                                <asp:TextBox ID="txtPermanentFilter" runat="server" Width="90%"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                    TargetControlID="txtPermanentFilter" WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                            </td>
                                            <td align="center" style="width: 1%">
                                                <asp:ImageButton ID="ImgbtnFilterBranch" runat="server" TabIndex="0" OnClick="btnFilter_Click"
                                                    ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                            </td>
                                            <td align="center" style="width: 1%">
                                                <asp:ImageButton ID="ImgbtnRefreshBranch" runat="server" OnClick="ImgbtnRefreshBranch_Click"
                                                    ToolTip="Refresh" ImageUrl="~/Images/Refresh-icon.png" />
                                            </td>
                                            <td align="center" style="width: 1%">
                                                <asp:Image ImageUrl="~/Images/Add-icon.png" ID="ImgbtnAddBranch" 
                                                    runat="server" Style="cursor: pointer;"
                                                    ToolTip="Add New Status" ClientIDMode="Static" 
                                                     />
                                            </td>
                                          
                                        </tr>
                                    </table>
                                </asp:Panel>
        <asp:GridView ID="gvPermanentStatus" ClientIDMode="Static" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False"  
                                    CellPadding="0" CellSpacing="2"  GridLines="both" 
                                    AllowSorting="true" CssClass="gridmain-css" PageSize="1">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                      <asp:TemplateField HeaderText="Status" ItemStyle-Width="400px">
                                      <ItemTemplate>
                                      <asp:Label ID="lblStatus" Width="50px" Text='<%#Eval("Status")%>' rel='<%#Eval("ID").ToString() %>'
                                                    runat="server"  Style="margin: 5px; color: Black;" CssClass="lblStatus" />
                                                     <asp:HiddenField ClientIDMode="Static" ID="hdnPermanentID" runat="server" Value="0" />
                                      </ItemTemplate>
                                      </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="150px">
                                          <ItemTemplate>
                                                <asp:Image ID="Edit" CssClass="editBranch" ImageUrl="~/Images/Edit.gif" ForeColor="Black"
                                                    ToolTip="Edit" runat="server" rel='<%#Eval("ID").ToString() %>' Height="16px"
                                                    Width="16px" Visible='<%# uaEditFlag %>' Style="cursor: pointer;" />

                                                <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" Visible='<%# uaDeleteFlage %>'
                                                    OnCommand="onDeleteStatus" OnClientClick="return confirm('Are you sure, you want to delete?')"
                                                    CommandArgument='<%#Eval("[ID]")%>' ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                    Height="16px" Width="16px"></asp:ImageButton>
                                              <asp:Image ID="imgRecordInfo2" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                    Height="16px" Style="vertical-align: top; cursor: pointer;" Width="16px" runat="server"
                                                    onclick='<%# "Get_Record_Information(&#39;CRW_LIB_CD_PermanentStatus&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                   <uc1:ucCustomPager ID="ucCustomPagerPermanent" runat="server" PageSize="30" OnBindDataItem="BindJoiningType" />
                                 <div id="divAddStatus" title='Add/Edit Permanent Status' style="display: none; font-family: Tahoma;
                                text-align: left; font-size: 12px; color: Black; width: 30%">
                                <table width="100%" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td align="right" style="width: 20%">
                                           Permanent Status&nbsp;:&nbsp;
                                        </td>
                                       <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                          <td align="left">
                                            <asp:TextBox ID="txtPermanent" ClientIDMode="Static" runat="server"></asp:TextBox>
                                               <asp:HiddenField ClientIDMode="Static" ID="hdnStatusID" runat="server" Value="0" />
                                        </td>
                                    </tr>
                                  
                                    <tr>
                                        <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                            <asp:Button ID="btnSavePermanent" Width="75px" runat="server" Text="Save" 
                                                ClientIDMode="Static" onclick="btnSavePermanent_Click"
                                                />
                                            <input type="button" name="Cancel" value="Cancel" id="btnCancel" style="width: 75px;" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                                </center>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                                </div>
        </td>
        </tr>
        
        </table>
    
    </center>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", "#divadd_dvModalPopupCloseButton", function () {
                $("#divadd input[type='checkbox']").prop("checked", false);
            });

            $("body").on("click", ".editBranch", function () {
                var ID = $(this).attr("rel");
                $("#hdnStatusID").val($(this).attr("rel"));
                $("#txtPermanent").val($("#gvPermanentStatus .lblStatus[rel='" + ID + "']").text());
                showModal('divAddStatus', false);
                $("#divAddStatus_dvModalPopupTitle").text("Add/Edit Status");
            });

            $("body").on("click", "#ImgbtnAddBranch", function () {
                if (this.id == "ImgbtnAddBranch") {
                    $("#hdnStatusID").val(0);
                    $("#txtPermanent").val('');
                    showModal('divAddStatus', false);
                    $("#divAddStatus_dvModalPopupTitle").text("Add/Edit Status");
                }
                else {
                    $("#hdnStatusID").val(parseInt($(this).attr("rel")));
                    $("#txtPermanent").val($(this).text());
                    showModal('divAddStatus', false);
                    $("#divAddStatus_dvModalPopupTitle").text("Add/Edit Status");
                }
            });

            $("body").on("click", "#btnCancel", function () {
                $("#divAddStatus_dvModalPopupCloseButton").click();
            });

            $("body").on("click", "#btnSavePermanent", function () {
                if ($.trim($("#txtPermanent").val()) == "") {
                    $("#txtPermanent").val('');
                    $("#txtPermanent").focus();
                    alert("Please enter Status");
                    return false;
                }
            });

        });
    </script>
</asp:Content>
