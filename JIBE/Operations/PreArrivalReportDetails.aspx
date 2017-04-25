<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreArrivalReportDetails.aspx.cs" Inherits="Operations_PreArrivalReportDetails" MasterPageFile="~/Site.master"  Title ="Pre Arrival Report"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
   <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        var lastExecutor = null;


        function asyncGet_PreArrivalAttachments(PreArrivalId, Vessel_ID, evt, objthis, isclicked, pageheader) {


            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_PreArrivalAttachments', false, { "PreArrivalId": PreArrivalId, "Vessel_ID": Vessel_ID }, onSuccess_asyncGet_Portage_Bill_Attachments, Onfail, new Array(evt, objthis, isclicked, pageheader));

            lastExecutor = service.get_executor();

        }

        function onSuccess_asyncGet_Portage_Bill_Attachments(retVal, Args) {

            js_ShowToolTip_Fixed(retVal, Args[0], Args[1], Args[3]);
        }</script>
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
            height: 20px;
            padding: 0px 0px 0px 10px;
            text-align: left;
            font-weight: bold;
        }
        .leafTD-data
        {
            width: 140px;
            height: 20px;
            padding: 0px 0px 0px 0px;
            background-color: #cce499;
            text-align: left;
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
</asp:Content>

<asp:Content ID="contentmain" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-title">
          Pre Arrival Report
    </div>
    <div>
         <asp:FormView ID="fvPortInfoReport" FooterStyle-ForeColor="Black" runat="server" Width="100%">
            <ItemTemplate >
                <table>
                    <tr>
                        <td class='leafTD-header'>Report Date </td>  <td class='leafTD-data'><%#Eval("ReportDate")%></td>
                        <td class='leafTD-header'>Vessel </td>  <td class='leafTD-data'><%#Eval("VesselName")%></td>
                        <td class='leafTD-header'>Port </td>  <td class='leafTD-data'><%#Eval("Port_Name")%></td>
                    </tr>
                    <tr>
                        <td class='leafTD-header'>ETA</td>  <td class='leafTD-data'><%#Eval("ETA")%></td>
                        <td class='leafTD-header'>ETD </td>  <td class='leafTD-data'><%#Eval("ETD")%></td>
                        
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
    </div>
    <div>
         <asp:GridView  runat="server" ID= "GridView1" AutoGenerateColumns="False"  EmptyDataText="No record found !" Width="100%"
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
                <asp:BoundField DataField="ReportedIncidence" HeaderText="Reported Incidence" />
                <asp:TemplateField HeaderText="Att.">
                    <HeaderTemplate>
                        Picture
                    </HeaderTemplate>
                    <ItemTemplate>
                          <%-- <asp:HyperLink ID="lnkAttachmentNav" runat="server" ImageUrl="~/Images/Attach.png" NavigateUrl='<%# "~/Uploads/PNT/" + Eval("Attachment_Path").ToString()%>'
                                Target="_blank" Visible='<%#Eval("Attachment_Path").ToString()==""?false:true%>' />--%>
                                            <asp:HyperLink ID="hlnkPB" runat="server" NavigateUrl="#" Style="cursor: pointer"
                                        ImageUrl="~/Images/Attach.png"
                                        Text="PT" onclick='<%#"asyncGet_PreArrivalAttachments(&#39;"+Eval("PreArrivalDTLId").ToString()+"&#39;,&#39;"+Eval("Vessel_ID").ToString()+"&#39;,event,this,1,&#39;Attachments&#39;);" %>' Visible='<%#Eval("AttachmentYN").ToString()=="No"?false:true%>'></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" >
                    </ItemStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="Cost" HeaderText="Cost to owner/P&I (Average)" />
                <asp:BoundField DataField="Severity" HeaderText="Severity" />                                
            </Columns>
         </asp:GridView>
         <br />
         <asp:GridView  runat="server" ID= "GridView2" AutoGenerateColumns="False"  EmptyDataText="No record found !" Width="100%"
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
                <asp:BoundField DataField="ReportedIncidence" HeaderText="Reported Incidence" />
                <asp:TemplateField HeaderText="Att.">
                    <HeaderTemplate>
                        Picture
                    </HeaderTemplate>
                    <ItemTemplate>
                      <%--  <asp:HyperLink ID="lnkAttachmentCoun" runat="server" ImageUrl="~/Images/Attach.png" NavigateUrl='<%# "~/Uploads/PNT/" + Eval("Attachment_Path").ToString()%>'
                                Target="_blank" Visible='<%#Eval("Attachment_Path").ToString()==""?false:true%>' />--%>
                                            <asp:HyperLink ID="hlnkPB" runat="server" NavigateUrl="#" Style="cursor: pointer"
                                        ImageUrl="~/Images/Attach.png"
                                        Text="PT" onclick='<%#"asyncGet_PreArrivalAttachments(&#39;"+Eval("PreArrivalDTLId").ToString()+"&#39;,&#39;"+Eval("Vessel_ID").ToString()+"&#39;,event,this,1,&#39;Attachments&#39;);" %>' Visible='<%#Eval("AttachmentYN").ToString()=="No"?false:true%>'></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" >
                    </ItemStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="Cost" HeaderText="Cost to owner/P&I (Average)" />
                <asp:BoundField DataField="Severity" HeaderText="Severity" />
                                
            </Columns>
         </asp:GridView>
         <br />
         <asp:GridView  runat="server" ID= "GridView3" AutoGenerateColumns="False"  EmptyDataText="No record found !" Width="100%"
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
                <asp:BoundField DataField="RiskType" HeaderText="Code Type" />
                <asp:BoundField DataField="ReportedIncidence" HeaderText="Issue" />
                <asp:BoundField DataField="Cost" HeaderText="Cost to owner" />
                <asp:BoundField DataField="Severity" HeaderText="Severity" />                                
            </Columns>
         </asp:GridView>

         <br />
         <asp:Label ID="lblGridHeader" runat="server"></asp:Label>
         <asp:GridView  runat="server" ID= "GridView4" AutoGenerateColumns="False"  EmptyDataText="No record found !" Width="100%"
            AllowPaging="false" CellPadding="4" AllowSorting="True"  
            GridLines="Horizontal" >
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <RowStyle CssClass="RowStyle-css" />
                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                    <PagerSettings Mode="NumericFirstLast" />                        
                <PagerStyle CssClass="PagerStyle-css" />
                <PagerStyle Font-Size="Large" CssClass="pager" />
            <Columns>
                <asp:BoundField DataField="IncidentDate" HeaderText="Date"  />        
                <asp:BoundField DataField="VesselName" HeaderText="Vessel Name"  />
                <asp:BoundField DataField="Vessel_Size" HeaderText="Size" />
                <asp:BoundField DataField="IncidentName" HeaderText="Incident" />                         
            </Columns>
         </asp:GridView>

         <br />
         <asp:Label ID="Label1" runat="server" Text="Other Vessels Reported: "></asp:Label>
         <asp:GridView  runat="server" ID= "GridView5" AutoGenerateColumns="False"  EmptyDataText="No record found !" Width="100%"
            AllowPaging="false" CellPadding="4" AllowSorting="True"  
            GridLines="Horizontal" >
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <RowStyle CssClass="RowStyle-css" />
                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                    <PagerSettings Mode="NumericFirstLast" />                        
                <PagerStyle CssClass="PagerStyle-css" />
                <PagerStyle Font-Size="Large" CssClass="pager" />
             <Columns>
                <asp:BoundField DataField="IncidentDate" HeaderText="Date"  />        
                <asp:BoundField DataField="VesselName" HeaderText="Vessel Name"  />
                <asp:BoundField DataField="Vessel_Size" HeaderText="Size" />
                <asp:BoundField DataField="IncidentName" HeaderText="Incident" />                         
            </Columns>
         </asp:GridView>
    </div>
</asp:Content>