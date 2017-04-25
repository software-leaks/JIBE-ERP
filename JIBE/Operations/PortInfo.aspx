<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortInfo.aspx.cs" Inherits="Operations_PortInfo"   EnableEventValidation="false" %>

<%@ Register Src="../UserControl/ctlRecordNavigation.ascx" TagName="ctlRecordNavigation"
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

     <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
     <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/CrewIndex_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/InterviewSchedule_DataHandler.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>


</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager> 
    <div id="dvPortReport">
    <style type="text/css">
            .leafTR
            {
                border-bottom-style: solid;
                border-bottom-color: White;
                border-bottom-width: 1px;
            }
            .leafTD-header
            {
                width: 120px;
                height: 18px;
                padding: 0px 0px 0px 0px;
                text-align: left;
                font-size: 13px;
            }
            .leafTD-data
            {
                width: 40px;
                height: 20px;
                padding: 0px 0px 0px 0px;
                background-color: #cce499;
                text-align: left;
                font-size: 13px;
            }
            .leafTD-data-left
            {
                width: 40px;
                height: 20px;
                padding: 0px 0px 0px 0px;
                background-color: #cce499;
                text-align: center;
                font-size: 13px;
            }    
            #pageTitle
            {
                background-color: gray;
                color: White;
                font-size: 12px;
                text-align: center;
                padding: 2px;
                font-weight: bold;
            }
        </style>
        <script type="text/javascript">
            function openFile(filepath) {
                window.open(filepath, "_blank");
            }

    </script>
        <table >
        <tr>
        <td >
        <asp:FormView ID="fvPortInfoReport" FooterStyle-ForeColor="Black" runat="server">
                <ItemTemplate >
                    <table >
                 
                        <tr>
                            <td class='leafTD-header'>Terminal Name </td>  <td class='leafTD-data'><%#Eval("TerminalName")%></td>
                            <td class='leafTD-header'>Berth </td>  <td class='leafTD-data'><%#Eval("Berth")%></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                         
                        </tr>
                        <tr>
                            <td class='leafTD-header'>Terminal Operator</td>  <td class='leafTD-data'><%#Eval("TerminalOperator")%></td>
                            </tr>
                        <tr>
                            <td class='leafTD-header'>Berth Type </td>  <td class='leafTD-data'><%#Eval("BerthType")%></td>
                            <td class='leafTD-header'>Quick Release Shore Bollards </td>  <td class='leafTD-data'><%#Eval("QuickRelease")%></td>
                            <td class='leafTD-header'>Ship Crane </td>  <td class='leafTD-data'><%#Eval("ShipCrane")%></td>
                        </tr>

                            <tr>
                            <td class='leafTD-header'>Pilot VHF Ch. </td>  <td class='leafTD-data'><%#Eval("PilotVHF")%></td>
                            <td class='leafTD-header'>No.Of Pilots(Arrival) </td>  <td class='leafTD-data'><%#Eval("PilotArrNo")%></td>
                            <td class='leafTD-header'>Shore Crane </td>  <td class='leafTD-data'><%#Eval("ShoreCrane")%></td>
                        </tr>

                            <tr>
                            <td class='leafTD-header'>Berth Length(m) </td>  <td class='leafTD-data'><%#Eval("BerthLength")%></td>
                            <td class='leafTD-header'>No.Of Pilots(Departure) </td>  <td class='leafTD-data'><%#Eval("PilotDepNo")%></td>
                            <td class='leafTD-header'>Tidal Restriction </td>  <td class='leafTD-data'><%#Eval("TidalRest")%></td>
                        </tr>


                            <tr>
                            <td class='leafTD-header'>Max.LOA(m) </td>  <td class='leafTD-data'><%#Eval("MaxLOA")%></td>
                            <td class='leafTD-header'>Depth Alongside(m) </td>  <td class='leafTD-data'><%#Eval("DepthAlongSide")%></td>
                            <td class='leafTD-header'>Max. Draft </td>  <td class='leafTD-data'><%#Eval("MaxDraft")%></td>
                        </tr>

                            <tr>
                            <td class='leafTD-header'>Max.DWT </td>  <td class='leafTD-data'><%#Eval("MaxDWT")%></td>
                            <td class='leafTD-header'>Max.Beam(m) </td>  <td class='leafTD-data'><%#Eval("MaxBeam")%></td>
                            <td class='leafTD-header'>Loading </td>  <td class='leafTD-data'><%#Eval("Loading")%></td>
                        </tr>

                            <tr>
                            <td class='leafTD-header'>Water Density </td>  <td class='leafTD-data'><%#Eval("WaterDensity")%></td>
                            <td class='leafTD-header'>Max.Displ.(mt) </td>  <td class='leafTD-data'><%#Eval("MaxDispl")%></td>
                            <td class='leafTD-header'>Discharging </td>  <td class='leafTD-data'><%#Eval("Discharging")%></td>
                        </tr>

                            <tr>
                            <td></td><td></td>
                            <td class='leafTD-header'>Max.Air Draft.(m) </td>  <td class='leafTD-data'><%#Eval("MaxAirDraft")%></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td  style="background-color: #99ccff; text-align: center; font-weight: bold; " colspan="8">Master's Comments (Port Channel / Fairway Depth / Tidal Restriction) </td>  
                        </tr>
                        <tr>
                            <td class='leafTD-data' colspan="8"><asp:TextBox ID="txtComments" Height="60px" Width="800px" runat="server" Text='<%#Eval("Comments")%>' TextMode="MultiLine" BorderStyle="None" ForeColor="Black" BackColor="#cce499" Enabled="false"> </asp:TextBox></td> 
                        </tr>
                        <tr>
                            <td  style="background-color: #99ccff; text-align: center; font-weight: bold; padding: 5px 0px 5px 5px" colspan="8">Berthing Details </td>  
                        </tr>

                        <tr>
                            <td class='leafTD-header'>Side Alongside </td>  <td class='leafTD-data'><%#Eval("SideAlongside")%></td>
                            <td class='leafTD-header'>Gangway Used </td>  <td class='leafTD-data'><%#Eval("GangwayUsed")%></td>
                            <td class='leafTD-header'>Berthing No.Tugs </td>  <td class='leafTD-data'><%#Eval("BerthTugsNo")%></td>
                        </tr>

                        <tr>
                            <td class='leafTD-header'>Night Berthing </td>  <td class='leafTD-data'><%#Eval("NightBerthing")%></td>
                            <td class='leafTD-header'>Towage Line </td>  <td class='leafTD-data'><%#Eval("TowageLine")%></td>
                            <td class='leafTD-header'>Unberthing No.Tugs </td>  <td class='leafTD-data'><%#Eval("UnberthTugsNo")%></td>
                        </tr>

                        <tr>
                            <td class='leafTD-header'  colspan="2">No. of Mooring Lines FWD </td> 
                            <td class='leafTD-header'>Head </td>  <td class='leafTD-data'><%#Eval("HeadFWD")%></td>
                            <td class='leafTD-header'>Breast </td>  <td class='leafTD-data'><%#Eval("BreastFWD")%></td>
                            <td class='leafTD-header'>Spring </td>  <td class='leafTD-data'><%#Eval("SpringFWD")%></td>
                        </tr>

                        <tr>
                            <td class='leafTD-header' colspan="2">No. of Mooring Lines AFT </td> 
                            <td class='leafTD-header'>Stern </td>  <td class='leafTD-data'><%#Eval("SternAFT")%></td>
                            <td class='leafTD-header'>Breast </td>  <td class='leafTD-data'><%#Eval("BreastAFT")%></td>
                            <td class='leafTD-header'>Spring </td>  <td class='leafTD-data'><%#Eval("SpringAFT")%></td>
                        </tr>
                            <tr>
                            <td  style="background-color: #99ccff; text-align: center; font-weight: bold; padding: 5px 0px 5px 5px"  colspan="8">Services and Facilities Available </td>  
                        </tr>
                        <tr>
                            <td class='leafTD-header'>Bunkers </td>  <td class='leafTD-data'><%#Eval("Bunkers")%></td>
                            <td class='leafTD-header'>Bunker Type </td>  <td class='leafTD-data'><%#Eval("BunkerType")%></td>
                        </tr>
                        <tr>
                            <td class='leafTD-header'>Fresh water </td>  <td class='leafTD-data'><%#Eval("FreshWater")%></td>
                            <td class='leafTD-header'>Lube Oil </td>  <td class='leafTD-data'><%#Eval("LubeOil")%></td>
                            <td class='leafTD-header'>Sludge Disposal </td>  <td class='leafTD-data'><%#Eval("SludgeDisposal")%></td>
                        </tr>
                        <tr>
                            <td class='leafTD-header'>Crew Change </td>  <td class='leafTD-data'><%#Eval("CrewChange")%></td>
                            <td class='leafTD-header'>Provisions </td>  <td class='leafTD-data'><%#Eval("Provisions")%></td>
                            <td class='leafTD-header'>Garbage Disposal </td>  <td class='leafTD-data'><%#Eval("GarbageDisposal")%></td>
                        </tr>
                        <tr>
                            <td style="background-color: #99ccff; text-align: center; font-weight: bold; padding: 5px 0px 5px 5px"  colspan="8">Cargo Arrangements</td>  
                        </tr>
                        <tr>
                            <td class='leafTD-header'>No.Of Shore Crane</td>  <td class='leafTD-data'><%#Eval("ShoreCraneNo")%></td>
                            <td class='leafTD-header'>No. Of Ship Crane</td>  <td class='leafTD-data'><%#Eval("ShipCraneNo")%></td>
                            <td class='leafTD-header'>Total No.Cont.Operated</td>  <td class='leafTD-data'><%#Eval("ContOperNo")%></td>
                            <td class='leafTD-header'>Shore Staff Onboard</td>  <td class='leafTD-data'><%#Eval("ShoreStaffOBD")%></td>
                        </tr>

                        <tr style="background-color: #99ccff; text-align: center; font-weight: bold; padding: 5px 0px 5px 5px">
                            <td class='leafTD-header'>Container Operated </td>  
                            <td class='leafTD-header'  >20 Feet </td>  
                            <td class='leafTD-header' >40 Feet </td> 
                            <td class='leafTD-header'>45 Feet </td>  
                                <td class='leafTD-header'>DG</td>  
                            <td class='leafTD-header'>Reefer</td>  
                            <td class='leafTD-header'>Flatrack</td> 
                            <td class='leafTD-header'>BB cgo</td>  
                        </tr>

                        <tr>
                            <td class='leafTD-header' style="background-color: #99ccff; text-align: center; font-weight: bold; padding: 5px 0px 5px 5px">Loaded </td>  
                            <td class='leafTD-data'><%#Eval("Loaded20ft")%></td>  
                            <td class='leafTD-data'><%#Eval("Loaded40ft")%></td> 
                            <td class='leafTD-data'><%#Eval("Loaded45ft")%></td>  
                            <td class='leafTD-data'><%#Eval("LoadedDG")%></td>  
                            <td class='leafTD-data'><%#Eval("LoadedReefer")%></td>  
                            <td class='leafTD-data'><%#Eval("LoadedFlatrack")%></td> 
                            <td class='leafTD-data'><%#Eval("LoadedBB_CGO")%></td>  
                        </tr>

                        <tr>
                            <td class='leafTD-header' style="background-color: #99ccff; text-align: center; font-weight: bold; padding: 5px 0px 5px 5px">Discharged </td>  
                            <td class='leafTD-data'><%#Eval("Discharged20ft")%></td>  
                            <td class='leafTD-data'><%#Eval("Discharged40ft")%></td> 
                            <td class='leafTD-data'><%#Eval("Discharged45ft")%></td>  
                            <td class='leafTD-data'><%#Eval("DischargedDG")%></td>  
                            <td class='leafTD-data'><%#Eval("DischargedReefer")%></td>  
                            <td class='leafTD-data'><%#Eval("DischargedFlatrack")%></td> 
                            <td class='leafTD-data'><%#Eval("DischargedBB_CGO")%></td>  
                        </tr>

                        <tr>
                            <td class='leafTD-header' style="background-color: #99ccff; text-align: center; font-weight: bold; padding: 5px 0px 5px 5px">Shifted </td>  
                            <td class='leafTD-data'><%#Eval("Shifted20ft")%></td>  
                            <td class='leafTD-data'><%#Eval("Shifted40ft")%></td> 
                            <td class='leafTD-data'><%#Eval("Shifted45ft")%></td>  
                            <td class='leafTD-data'><%#Eval("ShiftedDG")%></td>  
                            <td class='leafTD-data'><%#Eval("ShiftedReefer")%></td>  
                            <td class='leafTD-data'><%#Eval("ShiftedFlatrack")%></td> 
                            <td class='leafTD-data'><%#Eval("ShiftedBB_CGO")%></td>  
                        </tr>

                        <tr>
                             <td colspan="8" style="background-color: #99ccff; text-align: center; font-weight: bold; padding: 5px 0px 5px 5px">Other Port Information</td>
                        </tr>
                            <tr>
                              <td class='leafTD-data' colspan="8">  
                                    <asp:TextBox ID="txtAdditionComments" Height="60px" Width="800px" runat="server" Text='<%#Eval("AdditionalComments")%>' TextMode="MultiLine" BorderStyle="None" ForeColor="Black" BackColor="#cce499" Enabled="false"> </asp:TextBox>
                            </td>
                        </tr>
                       
                    </table>
                </ItemTemplate>
            </asp:FormView>
        </td>
      <td style="vertical-align:top;width:270px;overflow:auto">
                            <div style="height: 760px; width: 270px; overflow: Auto">
             <asp:DataList ID="imgListRight" runat="server" AllowSorting="False" AutoGenerateColumns="false"
                        RepeatDirection="Vertical" RepeatColumns="1" GridLines="None" CellPadding="3"
                        CellSpacing="1" Width="250px" CssClass="GridView-css">
                        <ItemTemplate>
                            <asp:Image ID="lnkAttachment" runat="server" Text='<%# Eval("File_Name") %>' 
                            onclick='<%#"openFile(&#39;"+"../Uploads/PTR/"+Eval("File_Path").ToString()+"&#39;);"  %>' 
                            style="cursor:pointer"
                                NavigateUrl='<%# "~/Uploads/PTR/" + Eval("File_Path").ToString()%>' Target="_blank" ImageUrl='<%# GetImage("~/Uploads/PTR/" + Eval("File_Path").ToString()) %>'
                                Visible='<%#Eval("File_Path").ToString()==""?false:true%>'  Width='240px' />
                        </ItemTemplate>
                        <ItemStyle Width="10px" HorizontalAlign="Center" Wrap="false" VerticalAlign="Middle" />
                    </asp:DataList>
        </div>
                           
       </td>
        </tr>
         <tr>
                <td  style="background-color: #99ccff; text-align: center; font-weight: bold; padding: 5px 0px 5px 5px">Pilot Details</td>
        </tr>
         <tr>
            <td>
                <asp:GridView ID="gvPilotInfo" runat="server" EmptyDataText="Pilot Information Not Updated !" Width="100%"
                    AutoGenerateColumns="False"  DataKeyNames="ID,PortInfoId,Vessel_Id"
                    AllowPaging="false" CellPadding="4" AllowSorting="True"
                    GridLines="Horizontal" >
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                <Columns>
                    <asp:TemplateField HeaderText="Pilot Name">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("PILOT_NAME") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="200px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />
                    <asp:TemplateField ShowHeader="False" ItemStyle-Width="20px">
                        <HeaderTemplate>
                                    View Details 
                        </HeaderTemplate>
                        <ItemTemplate>
                                <asp:ImageButton ID="LinkButton2" runat="server" AlternateText="Edit" CausesValidation="False" OnCommand="onView" 
                                    ImageUrl="~/images/FBMMsgBody.png"   CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") + "," + DataBinder.Eval(Container,"DataItem.Vessel_Id")  %>' />
                        </ItemTemplate>
                </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </td>
        </tr>
        </table>
            
            </div>
            <asp:UpdatePanel ID="UpdatePnl" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                 <div id="divViewAttachments" style="font-family: Tahoma; color: black; display: none; width:500px; height: 300px;">
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


                 <div id="divPilotInfo" style="font-family: Tahoma; color: black; display: none; width:700px;height :auto">
                   <center>
                        <div class="error-message" onclick="javascript:this.style.display='none';">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                        <div style="font-family: Tahoma; font-size: 12px; border: 1px solid Gray; width: auto">
                            <div style="padding: 0px; padding: 2px; border-top: 0; background-color: #5588BB;
                                color: #FFFFFF; text-align: center;">
                                <b>Pilot Information</b>
                            </div>
                     </center>
                     <table>
                        <tr>
                            <td class='leafTD-header' >
                                    Pilot Name : <asp:Label ID="lblPilotName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                     <asp:GridView ID="dgPilotDetails" runat="server" EmptyDataText="Pilot Information Not Updated !" Width="700px"
                                            AutoGenerateColumns="False" DataKeyNames="Grade_Type" 
                                            AllowPaging="false" CellPadding="4" AllowSorting="True"
                                            GridLines="Horizontal"  OnRowDataBound="dgPilotDetails_RowDataBound">
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                              <Columns>
                                                        <asp:BoundField DataField="CATEGORY_NAME" HeaderText="Category"  />
                                                        <asp:TemplateField HeaderText="Ratings" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rdoOptions" runat="server" RepeatDirection="Horizontal"
                                                                    DataTextField="OptionText" DataValueField="ID">
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                         <asp:BoundField DataField="COMMENTS" HeaderText="Comments" />
                                            </Columns>
                                     </asp:GridView>
                            </td>
                           
                        </tr>
                        <tr>
                            <td class='leafTD-header'>
                                   Remarks : <asp:Label ID="lblPilotremarks" runat="server"></asp:Label>
                            </td>
                        </tr>
                     
                     </table>
                 </div>
               

</form>
</body>
</html>