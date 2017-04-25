 


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreArrivalDetails.aspx.cs" Inherits="Operations_PreArrivalDetails"   EnableEventValidation="false" %>

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

    <script type="text/javascript">
        function OpenAttachment(InspectionDetailId, Vessel_ID, Office_ID, RiskType, RiskName) {

            var pagesrc = "PreArrivalAttachments.aspx?PreArrivalID=" + InspectionDetailId + "&Vessel_ID=" + Vessel_ID + "&Office_ID=" + Office_ID + "&RiskType=" + RiskType + "&RiskName=" + RiskName;

            document.getElementById("ifAttach").src = pagesrc;
            showModal('divAttach');


        }
        function OpenIncident(InspectionDetailId, Vessel_ID, Office_ID, RiskType, RiskName, IncidentYN) {
            RiskName = RiskName.replace("#", "$@$"); 
           
            if (IncidentYN == 'No')
                return;
            var pagesrc = "PreArrivalIncidents.aspx?PreArrivalID=" + InspectionDetailId + "&Vessel_ID=" + Vessel_ID + "&Office_ID=" + Office_ID + "&RiskType=" + RiskType + "&RiskName=" + RiskName;
            
            document.getElementById("ifInc").src = pagesrc;
            showModal('divInc');


        }


        var lastExecutor = null;


        function asyncGet_PreArrivalAttachments(PreArrivalId, Vessel_ID, evt, objthis, isclicked, pageheader) {


            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_PreArrivalAttachments', false, { "PreArrivalId": PreArrivalId, "Vessel_ID": Vessel_ID}, onSuccess_asyncGet_Portage_Bill_Attachments, Onfail, new Array(evt, objthis, isclicked, pageheader));

            lastExecutor = service.get_executor();

        }

        function onSuccess_asyncGet_Portage_Bill_Attachments(retVal, Args) {

            js_ShowToolTip_Fixed(retVal, Args[0], Args[1], Args[3]);
        }
        </script>
</head>
<body>
    <form id="form2" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager> 
    <div id="dvPreArrivalDetails">
     
     
     <asp:UpdatePanel ID="updPreArrivalDetails" runat="server" >
     <ContentTemplate>
      <asp:GridView  runat="server" ID= "dgNav" AutoGenerateColumns="False"  EmptyDataText="No record found !" Width="100%"
            AllowPaging="false" CellPadding="4" AllowSorting="True"  OnRowDataBound="gv_RowDataBound"
            GridLines="Horizontal" >
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <RowStyle CssClass="RowStyle-css" />
                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                    <PagerSettings Mode="NumericFirstLast" />                        
                <PagerStyle CssClass="PagerStyle-css" />
                <PagerStyle Font-Size="Large" CssClass="pager" />
            <Columns>
                <asp:BoundField DataField="DetailType" HeaderText="Nav Hazard/General risk"  />        
                <asp:BoundField DataField="Date" HeaderText="Date"  />
                <asp:BoundField DataField="RiskType" HeaderText="Risk Type" />
                <asp:BoundField DataField="Location" HeaderText="Location" />
                <asp:TemplateField>
                    <HeaderTemplate>
                        Reported Incidence
                    </HeaderTemplate>
                    <ItemTemplate>
                      <asp:HyperLink ID="lblIncident" runat="server" Text='<%# Eval("IncidentYN").ToString() %>'
                                            OnClick='<%# "OpenIncident(&#39;"+Eval("PKID").ToString()+"&#39;,&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("Office_ID").ToString()+"&#39;,&#39;Nav Hazards&#39;,&#39;"+Eval("DetailType").ToString()+"&#39;,&#39;"+Eval("IncidentYN").ToString()+"&#39;)" %>'  NavigateUrl="#" Enabled='<%#Eval("IncidentYN").ToString()=="No"?false:true%>'/>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" >
                    </ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Att.">
                    <HeaderTemplate>
                        Picture
                    </HeaderTemplate>
                    <ItemTemplate>
                       

                                <image id="imgAttach" runat="server" src="../Images/attachment.png" style="cursor: pointer"
                                            onclick='<%# "OpenAttachment(&#39;"+Eval("PKID").ToString()+"&#39;,&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("Office_ID").ToString()+"&#39;,&#39;Nav Hazards&#39;,&#39;"+Eval("DetailType").ToString()+"&#39;)" %>' Visible='<%#Eval("AttachmentYN").ToString()=="None"?false:true%>'/>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" >
                    </ItemStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="Cost" HeaderText="Cost to owner/P&I (Average)" />
                <asp:BoundField DataField="Severity" HeaderText="Severity" />                                
            </Columns>
         </asp:GridView>
         
         <br />
         <asp:GridView  runat="server" ID= "dgCounter" AutoGenerateColumns="False"  EmptyDataText="No record found !" Width="100%"
            AllowPaging="false" CellPadding="4" AllowSorting="True"  OnRowDataBound="gv_RowDataBound"
            GridLines="Horizontal" >
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <RowStyle CssClass="RowStyle-css" />
                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                    <PagerSettings Mode="NumericFirstLast" />                        
                <PagerStyle CssClass="PagerStyle-css" />
                <PagerStyle Font-Size="Large" CssClass="pager" />
            <Columns>
                <asp:BoundField DataField="DetailType" HeaderText="Counter Party Risks"  />        
                <asp:BoundField DataField="Date" HeaderText="Date"  />
                <asp:BoundField DataField="RiskType" HeaderText="Risk Type" />
                <asp:BoundField DataField="Location" HeaderText="Location" />
                   <asp:TemplateField>
                    <HeaderTemplate>
                        Reported Incidence
                    </HeaderTemplate>
                    <ItemTemplate>
                      <asp:HyperLink ID="lblIncident" runat="server" Text='<%# Eval("IncidentYN").ToString() %>'
                                            OnClick='<%# "OpenIncident(&#39;"+Eval("PKID").ToString()+"&#39;,&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("Office_ID").ToString()+"&#39;,&#39;"+Eval("SubType").ToString()+"&#39;,&#39;"+Eval("DetailType").ToString()+"&#39;,&#39;"+Eval("IncidentYN").ToString()+"&#39;)" %>'  NavigateUrl="#" Enabled='<%#Eval("IncidentYN").ToString()=="No"?false:true%>'/>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" >
                    </ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Att.">
                    <HeaderTemplate>
                        Picture
                    </HeaderTemplate>
                    <ItemTemplate>
                      <image id="imgAttach" runat="server" src="../Images/attachment.png" style="cursor: pointer"
                                            onclick='<%# "OpenAttachment(&#39;"+Eval("PKID").ToString()+"&#39;,&#39;"+Eval("Vessel_ID").ToString()+"&#39;,&#39;"+Eval("Office_ID").ToString()+"&#39;,&#39;"+Eval("SubType").ToString()+"&#39;,&#39;"+Eval("DetailType").ToString()+"&#39;)" %>' Visible='<%#Eval("AttachmentYN").ToString()=="None"?false:true%>'/>


                                       
                    </ItemTemplate>
                    <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" >
                    </ItemStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="Cost" HeaderText="Cost to owner/P&I (Average)" />
                <asp:BoundField DataField="Severity" HeaderText="Severity" />
                                
            </Columns>
         </asp:GridView>
         <br />
         <asp:GridView  runat="server" ID= "dgPsc" AutoGenerateColumns="False"  EmptyDataText="No record found !" Width="100%"
            AllowPaging="false" CellPadding="4" AllowSorting="True"  OnRowDataBound="GridView3_RowDataBound"
            GridLines="Horizontal" >
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <RowStyle CssClass="RowStyle-css" />
                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                    <PagerSettings Mode="NumericFirstLast" />                        
                <PagerStyle CssClass="PagerStyle-css" />
                <PagerStyle Font-Size="Large" CssClass="pager" />
            <Columns>
                <asp:BoundField DataField="DetailType" HeaderText="PSC"  />        
                <asp:BoundField DataField="Date" HeaderText="Date"  />
                <asp:BoundField DataField="CodeType" HeaderText="Code Type" />
                <asp:BoundField DataField="Issue" HeaderText="Issue" />
                <asp:BoundField DataField="Cost" HeaderText="Cost to owner" />
                <asp:BoundField DataField="Severity" HeaderText="Severity" />                                
            </Columns>
         </asp:GridView>

        

     </ContentTemplate>
     </asp:UpdatePanel>


        








          <div id="divAttach" style="display: none; width:400px;height:300px"  title="Pre Arrival Attachments">
        <iframe id="ifAttach" src="" style=" width:400px;height:300px "></iframe>
    </div>
      <div id="divInc" style="display: none; width:400px;height:300px"  title="Pre Arrival Incidents">
        <iframe id="ifInc" src="" style=" width:400px;height:300px "></iframe>
    </div>
     </div>

</form>
</body>
</html>