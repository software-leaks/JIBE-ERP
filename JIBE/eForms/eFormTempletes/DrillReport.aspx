<%@ Page Title="Drill Activity Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DrillReport.aspx.cs" Inherits="eForms_eFormTempletes_DrillReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script src="../JS/common.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script> 
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <script src="../../Scripts/iframe.js" type="text/javascript"></script>  
    <script>
        function PrintReportPreview() {
            var Vessel_ID = document.getElementById('<%=hdnVesselId.ClientID%>').value;
            var Schedule_Id = document.getElementById('<%=hdnSchId.ClientID%>').value;
            var Office_Id = document.getElementById('<%=hdnOfficeID.ClientID%>').value;

            var url = 'DrillReportViewer.aspx?Vessel_ID=' + Vessel_ID + '&Schedule_Id=' + Schedule_Id + '&Office_Id=' + Office_Id; 
            window.open(url, "_blank", "", "");
        }

    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

  
 <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr class="eform-report-header">
                <td style="width: 33%;">
                </td>
                <td style="width: 34%; text-align: center;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Drill Report"></asp:Label>
                </td>
                <td style="width: 33%; text-align: right;">
                </td>
            </tr>
        </table>
    </div>
    <%--<asp:UpdateProgress ID="upUpdateProgress" runat="server">
                <ProgressTemplate>
                    <div id="blur-on-updateprogress">
                        &nbsp;</div>
                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                        color: black">
                        <img src="../Images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
            <div id="page-content" style="height: 900; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto;">
        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            color: Black; text-align: left; background-color: #fff;">
    <table>
        <tr>
           <td style="width: 100px">
           Training/Drill Name
               </td>
              <td style="width: 200px" class="eform-field-data" align="left">
                        <asp:Label ID="lblDrillName" runat="server"></asp:Label>
                    </td>
              <td style="width: 80px">
                        Report Date:
                    </td>
            <td class="eform-field-data" align="left">
                        <asp:Label ID="lblReportDate" runat="server"></asp:Label>
                    </td>
                       <td style="width: 200px"   align="right">
                       <input type="button" ID="btnReportPreview" runat="server" Value="Print Report" 
                               OnClick="PrintReportPreview()" />
                     </td>
        </tr>
        <tr> <td style="width: 80px">
                        Vessel Name:
                    </td>
           <td style="width: 200px" class="eform-field-data" align="left">
                        <asp:Label ID="lblVesselName" runat="server"></asp:Label>
                    </td>
         <td style="width: 80px">
                       Location:
                    </td>
             <td style="width: 200px" class="eform-field-data" align="left">
                        <asp:Label ID="lblLocation" runat="server"></asp:Label>
                    </td>
                  
        </tr>
    </table>
    </div>
        <table style="width: 100%;">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Drill Activity:</legend>
                        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    
                                    <asp:GridView ID="grd_Drill_Description" DataKeyNames="ID" runat="server"
                            AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" CssClass="GridView-css"
                            CellPadding="7"  AllowPaging="false" Width="100%" ShowFooter="false" EmptyDataText="No Record Found!"
                            CaptionAlign="Bottom" GridLines="Both">
                         <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#FFFFCC" />
                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Hours" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="10%" />
                                    <ItemTemplate>
                                        <%# Eval("Drill_Hours")%>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Minutes" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="10%" />
                                    <ItemTemplate>
                                        <%# Eval("Drill_Min")%>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                                   <HeaderStyle Width="90%" />
                                    <ItemTemplate>
                                        <%# Eval("Description")%>
                                    </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Left" />
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
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </td>
                
            </tr>
            <tr>
                <td colspan="2">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Question Answers:</legend>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="3">
                                 <asp:GridView ID="grd_Details_Answer" DataKeyNames="ID" runat="server"
                            AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" CssClass="GridView-css"
                            CellPadding="7" AllowPaging="false" Width="100%" ShowFooter="false" EmptyDataText="No Record Found!"
                            CaptionAlign="Bottom" GridLines="Both">
                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Question" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="50%" />
                                    <ItemTemplate>
                                        <%# Eval("Question")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Answer" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="20%" />
                                    <ItemTemplate>
                                    <asp:Label ID="lblanswer" runat="server" Text='<%# Bind("Ans") %>' Width="200px"   Height="24px"></asp:Label>
                                     
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="30%" />
                                    <ItemTemplate>
                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>' Width="200px"  style="word-wrap: break-word;"   Height="24px"></asp:Label>
                                     
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Wrap="true" />
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
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </td>
            </tr>


            <tr>
                <td colspan="2">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Attachments:</legend>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                 <asp:GridView ID="gvTrainingItems" AutoGenerateColumns="false" runat="server" Width="100%"
                                CssClass="gridmain-css" CellPadding="4" CellSpacing="0" EmptyDataText="No Records Found!"
                                GridLines="None">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Name" ItemStyle-Width="250px" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                          <asp:Image ImageUrl='<%# "../../Images/DocTree/"+(  (System.IO.File.Exists(Server.MapPath(@"~/Images/DocTree/"+System.IO.Path.GetExtension(Eval("Doc_Path").ToString()).Replace(".", "")+".png"))?(System.IO.Path.GetExtension(Eval("Doc_Path").ToString()).Replace(".", "")+".png"):"noneimg.png")  ) %>' ID="imgFile" runat="server"  />
                                            <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("Doc_Name") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     
                                    
                                 
                                    <asp:TemplateField HeaderText="Attachment" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlAttachmentDetails" Text='<%# Eval("Doc_Name")  %>'
                                                runat="server" NavigateUrl='<%#    System.IO.File.Exists(Server.MapPath("~/Uploads/TrainingItems/"+Eval("Doc_Path").ToString()))?"~/Uploads/TrainingItems/"+Eval("Doc_Path").ToString():"~/FileNotFound.aspx"     %>'
                                                Target="_blank"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     
                                </Columns>
                            </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </td>
            </tr>
        </table>
        
        <asp:HiddenField ID="hdnVesselId" runat="server" />
                    <asp:HiddenField ID="hdnSchId" runat="server" />
                       <asp:HiddenField ID="hdnOfficeID" runat="server" />
        
    </div>
  
</asp:Content>

