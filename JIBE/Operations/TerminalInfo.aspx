<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TerminalInfo.aspx.cs" Inherits="Operations_TerminalInfo" %>

<%@ Register Src="../UserControl/Rating.ascx" TagName="ucRating"
    TagPrefix="uc1" %>

   <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
     <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
</head>
  <body>
<form id="form1" runat="server">
  
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
                
    <style type="text/css">
        .leafTR
        {
            border-bottom-style: solid;
            border-bottom-color: White;
            border-bottom-width: 1px;
            height: 20px;
        }
        .leafTD-header-center
        {
            width: 220px;
            height: 20px;
            padding: 0px 0px 0px 10px;
            text-align: center;
            font-size: 13px;            
            background-color: #99ccff;
            font-weight: bold; 
        }
        .leafTD-header
        {
            width: 300px;
            height: 20px;
            padding: 0px 0px 0px 10px;
            text-align: left;
            font-size: 13px;
        }
        .leafTD-data
        {
            width: 900px;
            height: 20px;
            padding: 0px 0px 0px 3px;
            background-color: #cce499;
            text-align: left;
        }
        .leafTD-data-center
        {
            width: 122px;
            height: 20px;
            padding: 0px 0px 0px 2px;
            background-color: #cce499;
            text-align: center;
        }
        .leafTD-header-center-small
        {
            width: 122px;
            height: 20px;
            padding: 0px 0px 0px 10px;
            text-align: center;
            font-size: 13px;
            background-color: #99ccff;
            font-weight: bold; 
        }
    </style>
    <div id="dvArrivalReport">
        <table border="1">
            <tr> 
                <td align="right" colspan="3">
                    <asp:Button   runat="server" ID="ImgViewAttachments" OnClick="btnViewAttachments_Click"   Text="View Attachment"  />
                </td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header-center' >Equipment</td>  
                <td class='leafTD-header-center-small'>Rating </td>  
                <td class='leafTD-header-center'>Comments </td> 
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' style="width:120px">Condition of Bollards</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="CondOfBollards_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblCondOfBollards_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' style="width:120px">Condition of apron/fenders/dock</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="CondOfApron_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblCondOfApron_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' style="width:120px">Condition of Shore Cranes</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="CondOfShoreCranes_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblCondOfShoreCranes_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' style="width:120px">Berth Lighting</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="BerthLighting_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblBerthLighting_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' style="width:120px">Tugs Performance</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="TugsPerformance_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblTugsPerformance_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' style="width:120px">Condition of Tugs Equipment</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="CondOfTugsEqu_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblCondOfTugsEqu_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header-center'>Shore Personnel</td>  
                <td class='leafTD-header-center-small'>Rating </td>  
                <td class='leafTD-header-center'>Comments </td> 
            </tr>

                <tr class='leafTR'>
                <td class='leafTD-header' >Pre-transfer conference</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="PreTransConf_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblPreTransConf_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Safety Awareness and PPE</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="SafetyAware_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblSafetyAware_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >English Skills</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="EngSkill_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblEngSkill_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Accessibility</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="Access_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblAccess_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Courtesy</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="Courtesy_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblCourtesy_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Emergency Preparedness</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="EmerPreparedness_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblEmerPreparedness_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Efficiency of Mooring Gang</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="EffOfMooringGang_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblEffOfMooringGang_Comm" runat="server"></asp:Label></td>
            </tr>
                <tr class='leafTR'>
                <td class='leafTD-header-center'>Agent</td>  
                <td class='leafTD-header-center-small'>Rating </td>  
                <td class='leafTD-header-center'>Comments </td> 
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Company Name</td>
                <td class='leafTD-data' colspan="2"><asp:Label ID="lblCompanyName" runat="server"></asp:Label></td>
            </tr>

            <tr class='leafTR'>
                <td class='leafTD-header' >Efficiency</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="Efficiency_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblEfficiency_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Communication</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="Communication_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblCommunication_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Crew Handling</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="CrewHandling_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblCrewHandling_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Documentation</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="Documentation_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblDocumentation_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Cost Efficiency</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="CostEfficiency_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblCostEfficiency_Comm" runat="server"></asp:Label></td>
            </tr>


                <tr class='leafTR'>
                <td class='leafTD-header-center'>Boat Service</td>  
                <td class='leafTD-header-center-small'>Rating </td>  
                <td class='leafTD-header-center'>Comments </td> 
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Boat Company Name</td>
                <td class='leafTD-data' colspan="2"><asp:Label ID="lblBoatCompanyName" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Condition of Boats</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="CondOfBoats_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblCondOfBoats_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Safety Awareness</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="SafetyAwareness_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblSafetyAwareness_Comm" runat="server"></asp:Label></td>
            </tr>
                                    

                <tr class='leafTR'>
                <td class='leafTD-header-center'>Miscellanceous</td>  
                <td class='leafTD-header-center-small'>Rating </td>  
                <td class='leafTD-header-center'>Comments </td> 
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Surveyor Company Name</td>
                <td class='leafTD-data' colspan="2"><asp:Label ID="lblSurveyorCompanyName" runat="server"></asp:Label></td>
            </tr>

            <tr class='leafTR'>
                <td class='leafTD-header' >Surveyor Safety Awareness</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="SurveyorSafetyAware_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblSurveyorSafetyAware_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Accessibility</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="Accessibility_Rat" runat="server"  /></td>
                <td class='leafTD-data'><asp:Label ID="lblAccessibility_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr class='leafTR'>
                <td class='leafTD-header' >Security</td>
                <td class="leafTD-data-center"> <uc1:ucRating ID="Security_Rat" runat="server" /></td>
                <td class='leafTD-data'><asp:Label ID="lblSecurity_Comm" runat="server"></asp:Label></td>
            </tr>
            <tr  class='leafTR'>
                <td class='leafTD-header' colspan="3" style="background-color: #99ccff;">Other Remarks</td>
            </tr>
            <tr  class='leafTR'>
                <td style="width: 100%; height: 120px; background-color: #cce499" colspan="3" >
                <asp:TextBox ID="txtRemarks" runat="server" BackColor="#cce499" BorderStyle="None"
                                                            ForeColor="Black" Height="100%" Text='<%#Eval("Remarks")%>' TextMode="MultiLine"
                                                            Width="100%" Enabled="false"> </asp:TextBox>
                </td>
            </tr>
        </table>
     </div>
     <asp:UpdatePanel ID="UpdatePnl1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                 <div id="divViewAttachments1" style="font-family: Tahoma; color: black; display: none; width:500px; height: 300px;">
                    <center>
                             <div style="padding: 0px; padding: 2px; border-top: 0; background-color: #5588BB;
                                color: #FFFFFF; text-align: center;">
                                <b>View Attachments</b>
                            </div>
                     </center>
                     <asp:GridView ID="grdPTR_Attachment" runat="server" AllowSorting="False" AutoGenerateColumns="false"
                        GridLines="None" CellPadding="3" CellSpacing="1" Width="100%" CssClass="GridView-css">
                        <Columns>
                            <asp:TemplateField HeaderText="Attachment Name">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("File_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkAttachment" runat="server" ImageUrl="~/Images/Attach.png" NavigateUrl='<%# "~/Uploads/PTR/" + Eval("File_Path").ToString()%>'
                                        Target="_blank" Visible='<%#Eval("File_Path").ToString()==""?false:true%>' />
                                </ItemTemplate>
                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle-css" />
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
</form>
</body>
</html>