<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CP_Openings.aspx.cs"
    Inherits="CP_Openings" Title="Charterer Openings" EnableEventValidation="false" %>

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
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/example/scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

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

        function OpenScreen(ID,Vessel_ID) {

            var url = 'CP_Opening_Entry.aspx?ID=' + ID + ' &VID=' + Vessel_ID;
            OpenPopupWindowBtnID('Chater_Opening', 'Charter Opening', url, 'popup', 800, 1030, null, null, false, false, true, null, 'ctl00_MainContent_btnRefresh');
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
                Charterer Openings
            </div>
            <div style="color: Black;">
                    <table width="100%">
                    <tr>
                <td width="10%" align="right">
                 <asp:Literal ID="ltOpeningStatus" Text="Opening Status :" runat="server"></asp:Literal>
                </td>

                    <td width="60%" align="left">
                     <asp:UpdatePanel ID="updatesearch" runat="server">
                    <ContentTemplate>
                    <asp:CheckBoxList ID="chkOpeningStatus"  RepeatDirection="Horizontal" runat="server" AutoPostBack="true">
                    
                    <asp:ListItem Text="Enquiry" Value="Enquiry"></asp:ListItem>
                    <asp:ListItem Text="Talking Stage" Value="Response" ></asp:ListItem>
                    <asp:ListItem Text="Working Firm" Value="Negotiation"></asp:ListItem>
                    <asp:ListItem Text="Approved" Value="Approval"></asp:ListItem>
                    <asp:ListItem Text="Fixed" Value="Fixed"></asp:ListItem>
                    <asp:ListItem Text="Suspend" Value="Suspend"></asp:ListItem>
                    <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>

                    </asp:CheckBoxList> 
   
                   </ContentTemplate>
                    </asp:UpdatePanel>
                        </td>
                     <td width="10%" align="right">
                         <asp:Literal ID="ltVessel" Text="Vessel :" runat="server"></asp:Literal>
                        </td>
                        <td width="10%">
                        <asp:DropDownList ID="ddlvessel"  runat="server"></asp:DropDownList>
                        </td>
                        <td align="left" width="10%" >
                           <asp:ImageButton ID="btnSearch" runat="server" ToolTip="Search" 
                                ImageUrl="~/Images/SearchButton.png" onclick="btnSearch_Click" />&nbsp;
                            <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Refresh-icon.png"
                                            OnClick="btnRefresh_Click" ToolTip="Refresh" />&nbsp;
                           <asp:ImageButton ID="ibtnAdd" runat="server" ToolTip="Add New Opening" OnClientClick='OpenScreen(0,0);return false;'
                                            ImageUrl="~/Images/Add-icon.png"  />  
                  
                        </td>
                    </tr>
                    <tr>
                    <td colspan="5">
                        <div id="divCPList" runat="server" style=" overflow-y: scroll">
                            <asp:GridView ID="gvOpenings" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                CssClass="gridmain-css" AllowSorting="true" >
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>

                                <asp:BoundField HeaderText="Status" DataField="Status" ItemStyle-Width="8%"  HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="Vessel" DataField="Vessel_Name"  ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="Opening" DataField="Opening"  ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="Charterer" DataField="Charterer_Name" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Broker" DataField="Broker_Name"  ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="Contact" DataField="Contact_Name"  ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="Remark(Progress)" DataField="Progress_Remarks"  ItemStyle-Width="30%" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="Updated On" DataField="Remarks_Date" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="Updated By" DataField="Updated_By" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>




                                    <asp:TemplateField >
                                        <HeaderTemplate>
                                         Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImgView" runat="server" Text="Update" OnClientClick='<%#"OpenScreen((&#39;"+ Eval("Opening_ID") +"&#39;),(&#39;"+ Eval("Vessel_ID") + "&#39;));return false;"%>'
                                                            CommandName="Select" ForeColor="Black" ToolTip="Update Opening" ImageUrl="~/Images/asl_view.png"
                                                            Height="16px"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="30" OnBindDataItem="BindGrid" />
                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                        </div>
                        </td>
                    </tr>
                   </table>
                        <asp:Button ID="btnFake" runat="server" style="display:none" />
            
                          <cc1:ModalPopupExtender ID="mpeBreakdown"  runat="server" CancelControlID="btnCancel"  TargetControlID="btnFake" PopupControlID="pnlPopUp"  BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
    
                        <asp:Panel runat="server" ID="pnlPopUp" CssClass="ModalPopup" style="display:none" >
                        <asp:UpdatePanel ID="updWindow" runat="server">
                        <ContentTemplate>
                        <table>
                        <tr>

                        <asp:ImageButton ID="btnCancel" runat="server"  ImageUrl="~/Images/xf_close_icon.png"/>
                        </tr>
                        <tr>
                        <td>
                         <asp:Literal ID="ltBreakdown" runat="server"></asp:Literal>
                        
                        </td>
                        </tr>
                        
                        </table>
                        </ContentTemplate>
                           </asp:UpdatePanel>
                        </asp:Panel>

            </div>
           


        </div>
    </center>
</asp:Content>
