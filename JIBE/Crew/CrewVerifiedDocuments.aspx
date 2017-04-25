<%@ Page Title="Crew Verified Documents" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CrewVerifiedDocuments.aspx.cs" Inherits="Crew_CrewVerifiedDocuments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <style id="TooltipStyle" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: arial;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .dvhdr1
        {
            font-family: Tahoma;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
            width: 200px;
            color: Black; /*background: #F5D0A9;*/
            border: 1px solid #F1C15F;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .dvbdy1
        {
            background: #FFFFFF;
            font-family: arial;
            font-size: 11px; /*border-left: 2px solid #3B0B0B;
            border-right: 2px solid #3B0B0B;
            border-bottom: 2px solid #3B0B0B;*/
            padding: 5px;
            width: 200px;
            background-color: #E0F8E0;
            border: 1px solid #F1C15F;
        }
        p
        {
            margin-top: 20px;
        }
        h1
        {
            font-size: 13px;
        }
        .dogvdvhdr
        {
            width: 300;
            background: #C4D5E3;
            border: 1px solid #C4D5E3;
            font-weight: bold;
            padding: 10px;
        }
        .dogvdvbdy
        {
            width: 300;
            background: #FFFFFF;
            border-left: 1px solid #C4D5E3;
            border-right: 1px solid #C4D5E3;
            border-bottom: 1px solid #C4D5E3;
            padding: 10px;
        }
        .pgdiv
        {
            width: 320;
            height: 250;
            background: #E9EFF4;
            border: 1px solid #C4D5E3;
            padding: 10px;
            margin-bottom: 20;
            font-family: arial;
            font-size: 12px;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
    </style>
    <script type="text/javascript">
        function PrintDiv(dvID) {

            var a = window.open('', '', 'left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
            a.document.write(document.getElementById(dvID).innerHTML);
            a.document.close();
            a.focus();
            a.print();
            a.close();
            return false;
        }
    </script>
    <style type="text/css">
        
          .error-message
               {
                font-size: 12px;
                font-weight: bold;
                color: Red;
                text-align: center;
                background-color: Yellow;
               }  
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div id="page-content" style="z-index: -2; overflow: auto; text-align: center;">
        <div class="NoPrint" style="text-align: right">
            <style type="text/css" media="print">
            
                .NoPrint
                {
                    display: none;
                }
                #pgHeader
                {
                    color: Black;
                }
                #tblCheckList
                {
                    border-width: 1px;
                }
         
            </style>
            <img src="../Images/Printer.png" title="*Print*" style="cursor: pointer;" alt="" onclick="PrintDiv('page-content')" />
        </div>
      <div class="page-title">
            <asp:Label ID="lblPageTitle" runat="server" Text="Verified - Document list"></asp:Label>
        </div>
     <%--   <div class="error-message">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>--%>
          <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <asp:HiddenField ID="hdnCrewID" runat="server" Value="0" />
        <div>
        </div>
        <center>
            <div style="text-align: left; border: 1px solid gray;">
                <table cellspacing="0" cellpadding="2" rules="rows" style="background-color: White;
                    width: 100%;">
                    <tr>
                        <td>
                             <table>
                                <tr>
                                    <td>
                                        Crew Name:
                                    </td>
                                    
                                    <td colspan="2" align="right">
                                        <asp:Label ID="lblCurrentRank" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label> &nbsp; - &nbsp;
                                        <asp:Label ID="lblStaffCode" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label> &nbsp; - &nbsp;
                                        <asp:Label ID="lblCrewName" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                  <td>
                                        Checklist applicable for Vessel:
                                    </td>   
                                       <td><asp:Label ID="lblVessel" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                      &nbsp;
                                    </td>                                 
                                    <td>
                                         &nbsp;</td>
                                   
                                    <td> 
                                        Nationality:<asp:Label ID="lblNationality" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                    </td>                                  
                                    
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ObjectDataSource ID="ObjDataSourceDDLJoinRank" runat="server" SelectMethod="Get_RankList"
                                TypeName="SMS.Business.Crew.BLL_Crew_Admin"></asp:ObjectDataSource>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                AllowSorting="True" CellPadding="4" ForeColor="#333333"
                                GridLines="None">
                                   <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                               <%--<AlternatingRowStyle CssClass="AlternatingRowStyle-css" />--%>
                           <%--     <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Document">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("DocName") %>'></asp:Label>
                                        </ItemTemplate>                                   
                                        <ControlStyle Width="250px"/>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Doc.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocNo" runat="server" Text='<%#Eval("DocNo") %>'></asp:Label>
                                        </ItemTemplate>                                      
                                    </asp:TemplateField>                                                        
                                    <asp:TemplateField HeaderText="Exp. Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("ExpiryDate"))) %>'></asp:Label>
                                        </ItemTemplate>                                    
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Place">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssuePlace" runat="server" Text='<%#Eval("PlaceOfIssue") %>'></asp:Label>
                                        </ItemTemplate>                                      
                                    </asp:TemplateField> 
                                      <asp:TemplateField HeaderText="Issue Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRankName" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DateOfIssue"))) %>'></asp:Label>
                                        </ItemTemplate>                                       
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Verified By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVerifiedBy" runat="server" Text='<%#Eval("VerifiedBy")%>'></asp:Label>
                                        </ItemTemplate>                                     
                                    </asp:TemplateField>                                
                                  
                                     <asp:TemplateField HeaderText="Verified Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRankName" runat="server"  Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("VerifiedDT"))) %>' ></asp:Label>
                                        </ItemTemplate>                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Remark")%>'></asp:Label>
                                        </ItemTemplate>                                     
                                    </asp:TemplateField>
                                </Columns>
                                  <EditRowStyle BackColor="#58FAAC" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>                        
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
