<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CP_Notices.aspx.cs"
    Inherits="CP_Notices" Title="Redelivery Notices" EnableEventValidation="false" %>

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

        function OpenOpenings(VID) {
            var url = '../CharterParty/CP_Openings.aspx?vid=' + VID;
            window.open(url, "_blank");
        }
        function OpenScreen1(ID, Job_ID) {

            var url = 'CP_Redelivery_Notices.aspx?Supp_ID=' + ID;
            OpenPopupWindowBtnID('Redelivery_Notices', 'Redelivery Notices', url, 'popup', 300, 1000, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
        }
        function OpenScreen(ID, Job_ID) {

            var url = 'CP_Chater_Opening.aspx?ID=' + ID + '&Supp_ID=' + Job_ID;
            OpenPopupWindowBtnID('Chater_Opening', 'Chater Opening', url, 'popup', 800, 1030, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
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
       <div id="page-title" class="page-title">
                Redelivery Notices
            </div>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;">
            <table width="100%">
            <tr>
           
            
                    <td width="70%" align="left">
                    &nbsp;
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
                            <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                            ImageUrl="~/Images/Exptoexcel.png" />
                        </td>
                    </tr>
                       <tr>
                 <td colspan="4">


            <div style="color: Black;height:500px" >

                        <div id="divCPList" runat="server" style=" overflow-y: scroll;width:100%">
                            <asp:GridView ID="gvNotices" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                DataKeyNames="Vessel_Id" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                CssClass="gridmain-css" AllowSorting="true" OnRowDataBound="gvNotices_Rowdatabound" >
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="DarkGray"/>
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign = "Center">
                                        <HeaderTemplate >
                                         Vessel
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:Label ID="lblVessel" runat="server" ForeColor="Green" Text='<%#Eval("Vessel_Name")%>'></asp:Label>   
                                                                           
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderStyle-HorizontalAlign = "Center">
                                        <HeaderTemplate >
                                        Charterer
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                         <asp:Label ID="lblCharter" runat="server" ForeColor="Blue" Text='<%#Eval("Charterer")%>'></asp:Label>                                           
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="center" />
                                    </asp:TemplateField>


                                  <asp:TemplateField >
                                        <HeaderTemplate>
                                        <table>
                                        
                                        <tr><td colspan="2">
                                         Redelivery Period
                                        </td> 
                                        </tr>
                                        </hr>
                                           <tr>
                                         <td>From
                                         </td>
                                         <td>
                                         To
                                         </td>
                                         </tr>
                                        </table>
                                          

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <table>
                                        <tr>
                                        <td>
                                           <asp:Label ID="lblRedeliveryDate" runat="server" Text='<%#Eval("Redelivery_Min_Date")%>'></asp:Label> &nbsp;-&nbsp
                                        </td>
                                        <td>
                                           <asp:Label ID="lblRedelivery" runat="server" Text='<%#Eval("Redelivery_Max_Date")%>'></asp:Label>
                                        </td>
                                        </tr>
                                        </table>

                                       </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                       <asp:TemplateField >
                                        <HeaderTemplate>
                                         Notice Days
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoticeDays" runat="server" Text='<%#Eval("Notice_Days")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="8%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                  <asp:TemplateField >
                                        <HeaderTemplate>
                                         <table>
                                         <tr>
                                         <td colspan="2">
                                         Notice Period
                                         </td>
                                         </tr>
                                          <tr>
                                         <td>From
                                         </td>
                                         
                                         <td>
                                         To
                                         </td>
                                         </tr>
                                         </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                          <table>
                                        <tr id="minmaxnotice" runat="server">
                                        <td>
                                           <asp:Label ID="lblMinNotice" runat="server" Text='<%#Eval("MinReDeliveryNotice")%>'></asp:Label>
                                           &nbsp;-&nbsp
                                        </td>
                                        <td>
                                           <asp:Label ID="lblMaxNotice" runat="server" Text='<%#Eval("MaxReDeliveryNotice")%>'></asp:Label>
                                        </td>
                                     </tr>

                                        </table>

                                            
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" Width="10%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField >
                                         <HeaderTemplate >
                                          Earliest  <br/> Delivery
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <asp:Label ID="lblEarliestDelivery" runat="server" Text='<%#Eval("EarliestRedelivery")%>'></asp:Label>
                                             
                                             
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                      <asp:TemplateField >
                                         <HeaderTemplate >
                                          Redelivery <br/>Date
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <asp:Label ID="lblEstimatedDelivery" runat="server" Text='<%#Eval("Est_ReDelivery")%>'></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="5%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    


                                     <asp:TemplateField >
                                         <HeaderTemplate >
                                          Status
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                             
                                             
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField >
                                        <HeaderTemplate>
                                         Openings
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnOpenings" runat="server" Text="Openings" Font-Bold="true"  OnClick="ibtnOpenings_Click"
                                                        
                                                            ForeColor="Blue" ToolTip="View Openings" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="5" OnBindDataItem="BindGrid" />
                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                        </div>



            </div>
           
           </td>
           </tr>
           </table>

        </div>
        <div style="color: Black;height:500px;width:100%">
        
        <table width="100%">
        <tr>
        <td width="60%">
                     <asp:GridView ID="gvOpenings" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False" Visible="false"
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
                                        <asp:HiddenField ID="hdnVesselId" Value='<%#Eval("Vessel_Id")%>' runat="server" />
                                        <asp:HiddenField ID="hdnOpeningId" Value='<%#Eval("Opening_Id")%>' runat="server" />
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImgView" runat="server" Text="Update" OnClick="ImgView_click"
                                                             ForeColor="Black" ToolTip="Opening history" ImageUrl="~/Images/asl_view.png"
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
                            <uc1:ucCustomPager ID="ucCustomPager2" runat="server" PageSize="5" Visible="false" OnBindDataItem="BindGrid" />
                            <asp:HiddenField ID="HiddenField2" runat="server" EnableViewState="False" />
        
        </td>
        <td width="40%" valign="top">
        
                <asp:GridView ID="gvOpeningHistory" runat="server"  Visible="false"
                    EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False" AllowPaging="true" PageSize="10"
                                CellPadding="1" CellSpacing="0" Width="100%" GridLines="both" 
                                CssClass="gridmain-css" AllowSorting="true" 
                    onpageindexchanging="gvOpeningHistory_PageIndexChanging"  >
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>

                                <asp:BoundField HeaderText="Status" DataField="Status" ItemStyle-Width="8%" />
                                <asp:BoundField HeaderText="Opening" DataField="Opening"  ItemStyle-Width="8%"/>
                                <asp:BoundField HeaderText="Contact" DataField="Contact_Name"  ItemStyle-Width="10%"/>
                                <asp:BoundField HeaderText="Remark(Progress)" DataField="Progress_Remarks"  ItemStyle-Width="30%"/>
                                <asp:BoundField HeaderText="Updated On" DataField="Remarks_Date" ItemStyle-Width="10%"/>
                                <asp:BoundField HeaderText="Updated By" DataField="Updated_By" ItemStyle-Width="10%"/>
                                </Columns>
                                </asp:GridView>
        
        </td>
        </tr>
        </table>

        
        </div>
    </center>
</asp:Content>
