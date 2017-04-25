<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CP_Charter_Party_List.aspx.cs"
    Inherits="CP_Charter_Party_List" Title="Charter Party List" EnableEventValidation="false" %>

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
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/CP_Functions_Common.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/example/scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function showDvRemarks() {
            showModal('dvRemarks', true, closeDvRemarks);
        }

        function closeDvRemarks() {

            hideModal('dvAddFolder');
        }
    


        function Validation() {

            if (document.getElementById("ctl00_MainContent_txtAirPortName").value == "") {
                alert("Please enter airport name.");
                document.getElementById("ctl00_MainContent_txtAirPortName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtElevation").value != "") {
                if (isNaN(document.getElementById("ctl00_MainContent_txtElevation").value)) {
                    alert("Elevation allows numeric value only.");
                    document.getElementById("ctl00_MainContent_txtElevation").focus();
                    return false;
                }
            }

            return true;
        }

        function OpenCPDetails(CPID) {
            var url = "../CharterParty/CP_Charter_Party_Details.aspx?CPID=" + CPID
            window.open(url, "_blank");
        }

        function OpenCPOpening() {

           window.open('../CharterParty/CP_Openings.aspx','_blank');
        }

        function OpenScreen1(ID, Job_ID) {

            var url = 'CP_Redelivery_Notices.aspx?Supp_ID=' + ID;
            OpenPopupWindowBtnID('Redelivery_Notices', 'Redelivery Notices', url, 'popup', 300, 1000, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }
        function OpenScreen(ID, Job_ID) {

            var url = 'CP_Chater_Opening.aspx?ID=' + ID + '&Supp_ID=' + Job_ID;
            OpenPopupWindowBtnID('Chater_Opening', 'Chater Opening', url, 'popup', 800, 1030, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }

        function OpenOutstanding(ID) {

            var url = 'CP_Overdue_BreakDown.aspx?ID=' + ID;
            OpenPopupWindow('Chater_Overdue', 'Chater Overdue', url, 'popup', 400, 600, null, null, false, false, true, null);
        }
        function ShowModalPopup() {
              js_ShowToolTip(Remarks, event, this);
        }
</script>
<style type="text/css">  

             .ModalBackground  
            {  
                  background-color: Gray;  
                  filter: alpha(opacity=60);  
                  opacity: 0.6;  
                  z-index: 10000;  
            }  

            .ModalPopup  
            {  
                  background-color:White;  
                  border-width:3px;  
                  border-style:solid;  
                  border-color:Gray;  
                  padding:5px;  
                  width: 350px; 
                  height:210px; 

            }  


        body, html
        {
            overflow-x: hidden;
        }
         
         .page

        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }

  .page
        {
            width: 100%;
           
        }

        .AlternatingCharterRowStyle-css
    {
        /*EDF1F8*/
        background-color:#E5E5E5;
        color: #333333;
        font-size: 11px;
    }
</style>  

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-title" class="page-title">
                Charter Party List
            </div>
            <div style="color: Black;">
                <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; border: 1px padding-bottom: 5px;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td style="width: 10%" align="right">
                                        Vessel :
                                    </td>
                                    <td style="width: 20%" align="left">
                                        <asp:DropDownList ID="ddlVessel" runat="server" Width="150px" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" CssClass="txtInput"></asp:DropDownList>
                                    </td>
                                    <td style="width: 10%" align="right">
                                        Charterer:
                                    </td>
                                    <td style="width: 20%" align="left">
                                        <asp:DropDownList ID="ddlSupplierList" runat="server" CssClass="txtInput"  Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 5%" align="right">
                                        <asp:ImageButton ID="btnSearch" runat="server" ToolTip="Search" ImageUrl="~/Images/SearchButton.png" Text="Search" 
                                            onclick="btnSearch_Click"  />
                                    </td>
                                    <td style="width:5%" align="left">
                                      <asp:ImageButton ID="btnNew" runat="server" Text="Create New" ToolTip="Create New" OnClientClick="OpenCPDetails(0);return false;" ImageUrl="~/Images/Add-icon.png" />
                                      <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Refresh-icon.png"
                                            OnClick="btnRefresh_Click" ToolTip="Refresh" />
                                    </td>
                                      <td style="width: 10%" align="left">
                                       &nbsp;
                                    </td>
                                      <td style="width: 5%" align="left">
                                         &nbsp;
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                        CP Status :
                                    </td>
                                    <td style="width: 40%" colspan="5" align="left">
                                        <asp:CheckBoxList ID="chkCPStatus" RepeatDirection="Horizontal" runat="server"/>
                                     </td>
                                </tr>
                            </table>

                        </div>
                        <div id="divCPList" runat="server" style=" overflow-y: scroll">
                            <asp:GridView ID="gvCPList" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                DataKeyNames="Charter_Party_Id" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                CssClass="gridmain-css" AllowSorting="true" OnRowDataBound="gvCPList_Rowdatabound" OnRowCommand="gvCPList_RowCommand">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingCharterRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign = "Center">
                                        <HeaderTemplate >
                                          Vessel <hr /> Charterer
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:Label ID="lblVessel" runat="server" ForeColor="Green" Text='<%#Eval("Vessel_Name")%>' Height="10px"></asp:Label>
                                         <hr />
                                         <asp:Label ID="lblCharter" runat="server" ForeColor="Blue" Text='<%#Eval("Charterer_Short_Name")%>' Height="10px"></asp:Label>                                           
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField >
                                         <HeaderTemplate >
                                          Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:Label ID="lbldocuments" runat="server" Font-Bold="true" BackColor="Yellow" ForeColor="Red" Text='Documents!' Visible="false" Height="10px"></asp:Label>
                                     
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("CP_Status")%>' Height="10px"></asp:Label>
                                             
                                             
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField >
                                        <HeaderTemplate>
                                           Delivery<hr /> Redelivery

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDelivery" runat="server" Text='<%#Eval("Delivery_Date")%>' Height="10px"></asp:Label>
                                            <hr />
                                             <asp:Label ID="lblRedeliveryDate" runat="server" Text='<%#Eval("ReDelivery_Date")%>' Height="10px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                             Add. Comm.
                                             <hr />
                                             Brok. Comm.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblComission" runat="server" ForeColor="Green" Text='<%#Eval("Address_Comm")%>' Height="10px"></asp:Label>
                                            <hr />
                                            <asp:Label ID="lblAddress" runat="server" ForeColor="Blue" Text='<%#Eval("Brokerage_Comm")%>' Height="10px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hire Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPBY" runat="server" Text='<%#Eval("Hire_Rate")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="6%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField >
                                        <HeaderTemplate>
                                          Prev. Hire <hr /> Due
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblHireDueDate" runat="server" Text='<%#Eval("Previous_Hire_Due_Date")%>' Height="10px"></asp:Label>
                                            <hr />
                                              <asp:Label ID="lblDueAmt" runat="server" Text='<%#Eval("Previous_Hire_Due_Amount","{0:0,0}")%>' Height="10px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true"  HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField >
                                        <HeaderTemplate>
                                            Last Remitt.<hr/>Recvd.(US$)
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceipt" runat="server" Text='<%#Eval("Last_Receipt")%>' Height="10px"></asp:Label>
                                            <hr />
                                            <asp:Label ID="lblAmountReceived" runat="server" Text='<%#Eval("Amount_Received","{0:0,0}")%>' Height="10px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="7%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="7%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField >
                                        <HeaderTemplate>
                                               Next Hire<hr/>Due <font Color="blue"><i>(Est)</i></font>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNextHire" runat="server" Text='<%#Eval("Next_Hire_Due_Date")%>'  Height="10px"></asp:Label>
                                            <hr />
                                             <asp:Label ID="lblNextHireDue" runat="server" Text='<%#Eval("Next_Hire_Due_Amount")%>' Height="10px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="7%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField >
                                        <HeaderTemplate>
                                            Unmatched
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnmatched" runat="server" Height="10px" Text='<%#Eval("Available","{0:0,0}")%>'   ></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Height="10px" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                       <asp:TemplateField >
                                        <HeaderTemplate>
                                           OverDue Amt.(US$)
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOverdue" runat="server" Text='<%#Eval("OverDue_Amount","{0:0,0}")%>' Height="10px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="8%"  CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true"  HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                        <asp:TemplateField >
                                        <HeaderTemplate>
                                         Hire Upto Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:Label ID="lblUptoDate" runat="server" ></asp:Label>
                                              </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="5%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                       <asp:TemplateField >
                                        <HeaderTemplate>
                                         Overdue Breakdown
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Image ID="ImgRemark" runat="server"  OnClick='<%#"OpenOutstanding("+ Eval("Charter_Party_ID").ToString()+")" %>'   Height="16px" Width="16px"
                                             onmouseover='<%#"GetOverdueBreakdown("+Eval("Charter_Party_ID").ToString() +",null,event,this,(&#39;Vessel:"+ Eval("[Vessel_Name]") + "&#39;));"%>'
                                                            onmouseout="CloseRemarkToolTip();" title="Overdue Breakdown" 
                                               ImageUrl="~/Images/info2.png" />
                                              </ItemTemplate>

                                              
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="5%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField >
                                        <HeaderTemplate>
                                          General Remarks
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:ImageButton ID="ibtnRemark" runat="server" CommandArgument='<%#Eval("Charter_Party_ID") %>'
                                                                    Visible="true" ImageUrl="~/images/remark_new.gif" OnCommand="ibtnRemark_click"
                                                                ToolTip="Remarks ">
                                        </asp:ImageButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="5%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                       <asp:TemplateField >
                                        <HeaderTemplate>
                                         Redelivery Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRedelivery" runat="server" Text='<%#Eval("Estimated_ReDelivery_Notice")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="8%" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField >
                                        <HeaderTemplate>
                                         Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ibtnDelete" runat="server" CommandArgument='<%#Eval("Charter_Party_ID") %>' Visible="false"
                                                                   ImageUrl="~/images/delete.png" OnCommand="ibtnDelete_click"
                                                                    OnClientClick="return confirm('Are you sure want to delete?')" ToolTip="Delete Charterer">
                                                                </asp:ImageButton>
                                                        <asp:ImageButton ID="ImgView" runat="server" Text="Update"  OnClientClick='<%#"OpenCPDetails("+ Eval("Charter_Party_ID") +");return false;"%>'  
                                                           ForeColor="Black" ToolTip="Details" ImageUrl="~/Images/asl_view.png"
                                                            Height="16px"></asp:ImageButton>

                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="7%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                          <HeaderStyle Wrap="true" Width="7%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="30" OnBindDataItem="BindGrid" />
                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                        </div>

                        <asp:Button ID="btnFake" runat="server" style="display:none" />
            
                          <cc1:ModalPopupExtender ID="mpeBreakdown" BehaviorID="mpe"  runat="server" CancelControlID="btnCancel"  TargetControlID="btnFake" PopupControlID="pnlPopUp"  BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
    
                        <asp:Panel runat="server" ID="pnlPopUp" CssClass="ModalPopup" style="display:none" >
                        <asp:UpdatePanel ID="updWindow" runat="server">
                        <ContentTemplate>
                        <table>
                        <tr>

                        <asp:ImageButton ID="btnCancel" runat="server" OnClientClick="return HideModalPopup();" ImageUrl="~/Images/xf_close_icon.png"/>
                        </tr>
                        <tr>
                        <td>
                         <asp:Label ID="lblBreakdown" runat="server"></asp:Label>
                        <div id="content">
                        </div>
                        </td>
                        </tr>
                        
                        </table>
                        </ContentTemplate>
                           </asp:UpdatePanel>
                        </asp:Panel>

                    </ContentTemplate>
                   <Triggers>

                        <asp:PostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
           


        </div>
        <div id="dvRemarks" style="display: none; background-color: #CBE1EF; border-color: #5C87B2; overflow: auto;
        border-style: solid; border-width: 1px; width: 600px; position: absolute; left: 40%;
        top: 15%; z-index: 1; color: black" class="ui-widget-content" title="Remarks">    
        <div class="content">
        <asp:UpdatePanel ID="updRemarks" runat="server">
        <ContentTemplate>
        <asp:GridView ID="gvRemarks" runat="server" AutoGenerateColumns="False" EmptyDataText="No Remarks found."
                                GridLines="Both" Width="100%"  >
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Created By">
                                           <ItemTemplate>
                                            <asp:Label ID="lblCreatedBy" runat="server" 
                                                Text='<%# Eval("CreatedBy") %>' 
                                                Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" ForeColor="Blue" HorizontalAlign="Center" Width="15%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Remarks/Queries">
                                    <ItemTemplate>
                                         <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("Remarks") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Wrap="true" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Attention To">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActionBy" runat="server" 
                                                Text='<%# Eval("ActionBy") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="15%" 
                                            Wrap="true" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>
        </div>

    </div>
    
            <div id="dvOverdueRemark"style="border: 1px solid gray; color: Black; position: relative;">
    </div>
    </center>
</asp:Content>
