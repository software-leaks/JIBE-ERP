<%@ Page Title="Lashing Gear Inventory Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LashingGearInventoryReport.aspx.cs" Inherits="eForms_eFormTempletes_LashingGearInventoryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
 <script src="../JS/common.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script> 
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../../Scripts/iframe.js" type="text/javascript"></script>   
 <style type="text/css">
        .eform-vertical-text
        {
            font: bold 14px verdana;
            font-weight: normal;
            writing-mode: tb-rl;
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=2);
        }
     .style3
     {
         width: 3px;
     }
     .style4
     {
         width: 436px;
         height: 20px;
     }
     .style5
     {
         width: 272px;
         height: 20px;
     }
     .style7
     {
         width: 104px;
     }
     .style10
     {
         width: 436px;
         height: 17px;
     }
     .style11
     {
         width: 272px;
         height: 17px;
     }
     .style16
     {
         width: 436px;
     }
     .style17
     {
         width: 1%;
     }
     .style19
     {
         width: 160px;
         height: 17px;
     }
     .style20
     {
         width: 160px;
         height: 20px;
     }
     .style21
     {
         height: 17px;
     }
     .style22
     {
         height: 20px;
     }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanelport" runat="server">
    <Triggers>
                          <asp:PostBackTrigger ControlID="ImgExpExcel" />                   
                         </Triggers>
                <ContentTemplate>
 <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr class="eform-report-header">
                <td style="width: 33%;">
                </td>
                <td style="width: 34%; text-align: center;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Lashing Gear Inventory Report"></asp:Label>
                </td>
               
                  <td width="10%">
                                                        &nbsp;&nbsp;
                                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to Excel" OnClick="ImgExpExcel_Click"
                                                            Width="16px" ImageUrl="~/Images/Excel-icon.png" />
                                                        &nbsp;&nbsp;
                                                        <img src="../../Images/Printer.png" style="cursor: hand;" alt="Print" onclick="PrintDiv('dvPageContent')" />
                                                        &nbsp;&nbsp;
                                                    </td>
            </tr>
        </table>
         </ContentTemplate>
  </asp:UpdatePanel>
 
 
  
    <div id="dvPageContent"  class="page-content-main">
     <div  style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px; width: 1200px;
            color: Black; text-align: left; background-color: #fff;">
               <table>
                <tr>
                    <td style="width: 80px">
                        Vessel Name:
                    </td>
                    <td style="width: 200px" class="eform-field-data" align="left">
                        <asp:Label ID="lblVesselName" runat="server"></asp:Label>
                    </td>
                    <td style="width: 80px">
                        Report Date:
                    </td>
                    <td class="eform-field-data" align="left">
                        <asp:Label ID="lblReportDate" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
           
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                       
                        <ContentTemplate>
                        
           
                        <asp:GridView ID="GridView_LashingInventoryRPT" DataKeyNames="ID" runat="server"
                            AutoGenerateColumns="False" OnRowDataBound="GridView_LashingInventoryRPT_RowDataBound"
                            CellPadding="7" AllowPaging="false" Width="100%" ShowFooter="false" EmptyDataText="No Record Found"
                            CaptionAlign="Bottom" GridLines="Both">
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                            <Columns>
                             <asp:TemplateField HeaderText="Sr.No">
                                   
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblNo" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ROWNUM") %>'></asp:Label>--%>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                   <ItemStyle HorizontalAlign="Center" />
                              </asp:TemplateField>
                          <asp:TemplateField HeaderText="Item Description">
                                    <ItemTemplate>
                                        <%# Eval("Item_Description")%>
                                    </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                     <asp:TemplateField HeaderText="Model">
                                    <ItemTemplate>
                                        <%# Eval("Model_No")%>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening R.O.B.">
                                    <ItemTemplate>
                                        <%# Eval("OpeningROB")%>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Owner">
                                    <ItemTemplate>
                                        <%# Eval("UR_Owner_No")%>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Charterer">
                                    <ItemTemplate>
                                        <%# Eval("UR_Charterer_No")%>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Owner">
                                    <ItemTemplate>
                                        <%# Eval("OP_Owner_No")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Charterer">
                                    <ItemTemplate>
                                        <%# Eval("OP_Charterer_No")%>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Units Refurbished/Repaired">
                                    <ItemTemplate>
                                        <%# Eval("UNIT_Repaired_No")%>
                                    </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Units Claimed from Stevedores">
                                    <ItemTemplate>
                                        <%# Eval("Unit_Claimed_No")%>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closing R.O.B.">
                                    <ItemTemplate>
                                        <%# Eval("ClosingROB")%>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requirement per Cargo Securing Manual">
                                    <ItemTemplate>
                                        <%# Eval("Carg_Securing_Mannual_No")%>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                           
                                 <asp:TemplateField HeaderText="Supply Required">
                                    <ItemTemplate>
                                        <%# Eval("SupplyReq")%>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" HorizontalAlign="Center"/>
                            <PagerStyle CssClass="PagerStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                            <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                        </asp:GridView>
                        <tr>
                    <td>

                        <table cellpadding="1" cellspacing="1"  style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;">
                      
                 
                            <tr>
                                <td colspan="2">
                                   <%-- * For results on Lashing Gear Inventory Report&nbsp; :--%>
                                </td>
                            </tr>
                          
                            <tr>
                                
                              <td style="padding-left:0px;font-family:Times New Roman;font-size:100%;width:50%;">
                                   
                                   Note:
                                </td>
                                <td class="style7">
                                  
                                    
                                <td>
                                </td>
                                <tr>
                               <td style="padding-left:0px;font-size:85%;width:50%;">
                                   1.Complete Inventory of lashing gear items (used or unused)</td>
                                </tr>
                            </tr>
                         
                             <tr>
                              <td style="padding-left:0px;font-size:85%;width:50%;">
                                  2.This form is to be made ship specific and kept consistent.</td>
                            </tr>
                         
                            <tr>
                                <td style="padding-left:0px;font-size:85%;width:50%;">
                                 3.Item no.6 can not checked because operation in cargo hold.</td>
                                </tr>
                         
                            <tr>
                                <td class="style10" align="right">
                                 
                                <td class="style11" align="right">
                                    </td>
                                </td>
                                <td class="style21">
                                    &nbsp;</td>
                                <td class="style19" align="right">
                                    Master's Signature:
                                </td>
                                 <td width="200px" align="left">
                                <%--  <asp:Label ID="lblmasterDate" runat="server"></asp:Label>--%>
                                     <asp:Label ID="lblMasterSignature" runat="server"></asp:Label>
                                 </td>
                            </tr>
                            <tr>
                                <td class="style4"align="right">
                                  
                                <td class="style5"align="right">
                           
                                    </td>
                                </td>
                                <td class="style22">
                                    </td>
                                <td class="style20" align="right">
                                    Chief Officer's Signature: 
                                </td>
                                 <td  width="200px" align="left" class="style22">
                                 <asp:Label ID="lblchiefofficer" runat="server"></asp:Label>
                                 </td>
                            </tr>
                            <table>
                                <tr>
                                    <td align="left" class="style17">
                                        Remarks:</td>
                                    <td align="left" width="50%">
                                        <asp:TextBox ID="txtremarks" Height="50px" runat="server" ReadOnly="true" TextMode="MultiLine" 
                                            Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                             
                             
                                    
                                   
                                
                               
                        
                            
                        </table>
                     
                    </td>
                </tr>
                        </ContentTemplate>
                         </asp:UpdatePanel>
                            
                 
            </div>
    </div>
</asp:Content>

