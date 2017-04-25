 
<%@ Page Title="Risk Assessment Details" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" 
    CodeFile="RiskAssessmentDetails.aspx.cs" Inherits="JRA_RiskAssessmentDetails" %>

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
    <script language="javascript" type="text/javascript">

        function Validation() {

            if (document.getElementById('<%= txtHazardDesc.ClientID %>').value.trim() == "") {
                alert("Hazard Description is mandatory.");
                document.getElementById('<%= txtHazardDesc.ClientID %>').focus();
                return false;
            }

            var result = document.getElementById('<%= ddlWorkCategory.ClientID %>').value;
            if (result <= 0) {
                alert("Work Category is mandatory.");
                document.getElementById('<%= ddlWorkCategory.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= txtControlMeasure.ClientID %>').value.trim() == "") {
                alert("Control Measure is mandatory.");
                document.getElementById('<%= txtControlMeasure.ClientID %>').focus();
                return false;
            }

            var result1 = document.getElementById('<%= ddlSeverity.ClientID %>').value;
            if (result1 <= 0) {
                alert("Severity is mandatory.");
                document.getElementById('<%= ddlSeverity.ClientID %>').focus();
                return false;
            }

            var result2 = document.getElementById('<%= ddlLikelihood.ClientID %>').value;
            if (result2 <= 0) {
                alert("Likelihood is mandatory.");
                document.getElementById('<%= ddlLikelihood.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= txtInitiakRisk.ClientID %>').value.trim() == "") {
                alert("Severity and Likelihood are mandatory for Initial Risk.");
                return false;
            }

           



            //            if (document.getElementById("ctl00_MainContent_txtElevation").value != "") {
            //                if (isNaN(document.getElementById("ctl00_MainContent_txtElevation").value)) {
            //                    alert("Elevation allows numeric value only.");
            //                    document.getElementById("ctl00_MainContent_txtElevation").focus();
            //                    return false;
            //                }
            //            }

            return true;
        }

        function ValidationFW() {

            if (document.getElementById('<%= txtFWRemark.ClientID %>').value.trim() == "") {
                alert("Follow-up Remark is mandatory.");
                document.getElementById('<%= txtFWRemark.ClientID %>').focus();
                return false;
            }
            return true;
        }

        function OpenFollowupDiv() {
            //document.getElementById("dvAddFollowUp").style.display = "block";
            document.getElementById('<%= txtHazardDesc.ClientID %>').value = "";
            showModal('divFollow');
        }
        function ValidationRApr() {

            if (document.getElementById('<%= txtRemark.ClientID %>').value.trim() == "") {
                alert("Remark is mandatory.");
                document.getElementById('<%= txtRemark.ClientID %>').focus();
                return false;
            }
            return true;
        }
        function PrintElem(elem) {
           
            Popup($(elem).html());
        }

        function Popup(data) {
            var mywindow = window.open('', 'Risk Assess,emt Details', 'height=842,width=595');
            mywindow.document.write('<html><head><title>my div</title>');
            /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
            mywindow.document.write('</head><body >');
            mywindow.document.write(data);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10

            mywindow.print();
            mywindow.close();

            return true;
        }
    </script>
    <style>
     .row-header
        {
            background-color: #aabbdd;
            font-weight: bold;
        }
         .roundedBox
        {
            border-radius: 5px;
            border: 2px solid white;
            background-color: #DBDFD5;
            text-align: center;
            font-size: 14px;
            color: #555;
            margin: 2px;
            padding: 2px;
        }
        .Page
        {
            height:2000px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <center>    
 <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

        <div id="printDiv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;">
             <div class="page-title">
                Assessment Details
            </div>
            <div style="color: Black;">
                <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px;" class="roundedBox">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td style="" align="right">
                                       Vessel Name :&nbsp;
                                    </td>
                                    <td style="" align="left">
                                        <asp:Label ID="txtVesselName"   runat="server" ReadOnly="true" style="font-weight:bold;color:Blue"> 
                                        </asp:Label>
                                    </td>
                                  
                                       <td style="white-space:nowrap" align="right">
                                       Current Assessment Date :&nbsp;
                                    </td>
                                    <td style="" align="left">
                                        <asp:Label ID="txtCurrentAssessmentDate" runat="server" ReadOnly="true" style="font-weight:bold;color:Blue"> 
                                        </asp:Label>
                                    </td>
                                   <td style="white-space:nowrap" align="right">
                                       Status :&nbsp;
                                    </td>
                                    <td style="" align="left">
                                        <asp:Label ID="lblApprovalStatus"  runat="server" ReadOnly="true" style="font-weight:bold;color:Blue"> 
                                        </asp:Label>
                                    </td>
                                   
                                  
                                    <td align="left" style="">
                                       <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"  style="display:none"
                                            ImageUrl="~/Images/Exptoexcel.png"   />
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Hazard" OnClick="ImgAdd_Click" Visible="false" style="margin-left:15px"
                                            ImageUrl="~/Images/Add-icon.png" />
                                            <asp:ImageButton ID="ImgPrint" runat="server" ToolTip="Print"  OnClientClick="PrintElem('#printDiv')"   style="margin-left:15px"
                                            ImageUrl="~/Images/Printer.png" /> 
                                       
                                    </td>
                                </tr>
                                <tr>
                                  <td style="white-space:nowrap" align="right">
                                        Job Assessed :&nbsp;
                                    </td>
                                    <td style="" align="left">
                                        <asp:Label ID="txtJobASsessed"  runat="server" ReadOnly="true" style="font-weight:bold;color:Blue"> 
                                        </asp:Label>
                                    </td>
                                    <td style="white-space:nowrap" align="right">
                                       Last Assessment Date :&nbsp;
                                    </td>
                                    <td style="" align="left">
                                        <asp:Label ID="txtLastAssessmentDate"  runat="server" ReadOnly="true" style="font-weight:bold;color:Blue"> 
                                        </asp:Label>
                                    </td>
                                    <td>
                                    </td><td></td>
                                       <td align="left"   >
                                        <asp:ImageButton ID="imgBtnApproveReject" runat="server"    ToolTip="Approve/Reject Job Assessment" Visible="false" Text="Verify Risk Assessment"
                                            style="height: 22px"  ImageUrl="~/Images/btnVerifyAssessment.png"
                                             onclick="imgBtnApproveReject_Click" />
                                    </td>
                                   <%-- <td style="" align="right"  >
                                       Risk Assessment No.:&nbsp;
                                    </td>
                                    <td style="" align="left"  >
                                        <asp:Label ID="txtAssessmentNo" runat="server" ReadOnly="true" style="font-weight:bold;color:Blue"> 
                                        </asp:Label>
                                    </td>--%>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvAssessmentDetails" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvAssessmentDetails_RowDataBound" DataKeyNames="Assessment_Dtl_ID,Vessel_ID,Office_ID,Initial_Risk_Color,Modified_Risk_Color" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both" OnSorting="gvAssessmentDetails_Sorting" AllowSorting="true" CssClass="gridmain-css">
                                  
                                 <RowStyle CssClass="RowStyle-css" />
                                 <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField  >
                                            <HeaderTemplate>
                                                <asp:Label ID="lblHazard_descriptionHead" runat="server"  
                                                    ForeColor="Black">Hazard Description&nbsp;</asp:Label>
                                             
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblHazard_descriptionView" runat="server" CommandArgument='<%#Eval("Assessment_Dtl_ID").ToString()+";"+Eval("Office_ID")%>'
                                                    Text='<%#Eval("Hazard_Description") %>' Style="color: Black" OnCommand="onUpdate" Enabled='<%# uaEditFlag %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css" VerticalAlign="Top">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  >
                                            <HeaderTemplate>
                                                <asp:Label ID="lblControl_MeasureView" runat="server"  
                                                    ForeColor="Black">Control Measure&nbsp;</asp:Label>
                                              
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblControl_MeasureView" runat="server" Text='<%#Eval("Control_Measure")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="90px" CssClass="PMSGridItemStyle-css" VerticalAlign="Top">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  >
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSeverityHEader" runat="server" 
                                                    ForeColor="Black">Severity&nbsp;</asp:Label>
                                              
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeverityView" runat="server" Text='<%#Eval("Severity")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css" VerticalAlign="Top">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  >
                                            <HeaderTemplate>
                                                <asp:Label ID="lblCountryHeader" runat="server" 
                                                    ForeColor="Black">Likelihood&nbsp;</asp:Label>
                                                <img id="Likelihood" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLikelihoodView" runat="server" Text='<%# Eval("Likelihood") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css" VerticalAlign="Top">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  >
                                            <HeaderTemplate>
                                                <asp:Label ID="lblInitial_Risk_HEader" runat="server" 
                                                    ForeColor="Black">Initial Risk&nbsp;</asp:Label>
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInitial_Risk_View" runat="server" Text='<%#Eval("Initial_Risk")%>'  ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css" VerticalAlign="Top">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  >
                                            <HeaderTemplate>
                                                <asp:Label ID="lblAdditional_Control_MeasuresHeader" runat="server" 
                                                    ForeColor="Black">Additional Control Measures&nbsp;</asp:Label>
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdditional_Control_MeasuresView" runat="server" Text='<%# Eval("Additional_Control_Measures") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css" VerticalAlign="Top">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  >
                                         <HeaderTemplate>
                                                <asp:Label ID="lblAdditional_Modified_RiskHeader" runat="server" 
                                                    ForeColor="Black">Modified Risk&nbsp;</asp:Label>
                                               
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblModified_Risk" runat="server" Text='<%# Eval("Modified_Risk") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css" VerticalAlign="Top">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  >
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("Assessment_Dtl_ID").ToString()+";"+Eval("Office_ID")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# (Eval("Hazard_ID").ToString().Trim().Length==0&&uaDeleteFlage==true)?true:false %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("Assessment_Dtl_ID").ToString()+";"+Eval("Office_ID")%>'  ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                     <%--   <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;JRA_DTL_ASSESSMENT_DETAILS&#39;,&#39;Hazard_ID="+Eval("Hazard_ID").ToString()+" and Vessel_ID="+Eval("Vessel_ID").ToString()+" and Office_ID="+Eval("Office_ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>--%>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css" VerticalAlign="Top">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindAssessmentDetails"  Visible="false"/>
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                   <asp:HiddenField ID="hfAssessment_ID" runat="server"  />
                                   <asp:HiddenField ID="hfVessel_ID"     runat="server" />
                                    <asp:HiddenField ID="hfTblMode"     runat="server" />
                                      <asp:HiddenField ID="hfWork_Categ_ID"     runat="server" />
                                      <asp:HiddenField ID="hfAssessment_Status"     runat="server" />
                            </div>
                            <br />
                              <div id="dvFollowUp_Attachments">
                <table cellpadding="0" cellspacing="5" width="100%">
                    <tr>
                        <td style="border: 1px solid #aabbdd; width: 50%; vertical-align: top;">
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr class="row-header" >
                                    <td style="font-weight: bold;">
                                       Follow-up Remarks
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="ImgBtnAddFollowup" runat="server" ImageUrl="~/Images/AddFollowup.png"
                                            OnClientClick="OpenFollowupDiv();return false;" CssClass="non-printable" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div style="max-height: 350px; min-height: 250px; overflow: auto">
                                            <asp:GridView ID="grdFollowUps" runat="server" BackColor="White" BorderColor="#999999"
                                                AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                                EnableModelValidation="True" GridLines="Vertical" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" SortExpression="Date_Of_Creation" ItemStyle-Width="10%"
                                                        ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate_Of_CreationAC" runat="server" Text='<%#Eval("Date_Of_Creation","{0:dd/MM/yyyy HH:mm}").ToString() == "01/01/1900" ? "" : Eval("Date_Of_Creation","{0:dd/MMM/yy HH:mm}")   %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Created By" SortExpression="LOGIN_NAME" ItemStyle-Width="25%"
                                                        ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lblLOGIN_NAME" runat="server" NavigateUrl='<%# "../Crew/CrewDetails.aspx?ID=" + Eval("CrewID")%>' Enabled='<%# Eval("CrewID").ToString()==""?false:true %>'
                                                                Target="_blank" Text='<%# Eval("Created_By_Name")%>' CssClass="pin-it"></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Followup" SortExpression="FOLLOWUP" ItemStyle-Width="75%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFOLLOWUP" runat="server" Text='<%#Eval("Remarks").ToString().Replace("\n","<br>") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="ldl1" runat="server" Text="No followups !!"></asp:Label>
                                                </EmptyDataTemplate>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                            </table>
                        </td>
                    <td style="border: 1px solid #aabbdd; width: 50%; vertical-align: top;">
                            <table style="width: 100%" cellpadding="0" cellspacing="0">
                                <tr class="row-header" style="height: 24px">
                                    <td>
                                        Action Log:
                                    </td>
                                   
                                </tr>
                                <tr >
                                    <td  >
                                       
                                            <asp:GridView ID="grdActionLog" runat="server" AutoGenerateColumns="false" 
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                                                CellPadding="3" EnableModelValidation="True" GridLines="Vertical" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center" 
                                                        ItemStyle-Width="10%" SortExpression="Date_Of_Creation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate_Of_CreationAC" runat="server" 
                                                                Text='<%#Eval("Date_Of_Creation","{0:dd/MM/yyyy HH:mm}").ToString() == "01/01/1900" ? "" : Eval("Date_Of_Creation","{0:dd/MMM/yy HH:mm}")   %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action By" ItemStyle-HorizontalAlign="Center" 
                                                        ItemStyle-Width="25%" SortExpression="LOGIN_NAME">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lblLOGIN_NAMEAC" runat="server" CssClass="pin-it"  Enabled='<%# Eval("CrewID").ToString()==""?false:true %>'
                                                                NavigateUrl='<%# "../Crew/CrewDetails.aspx?ID=" + Eval("CrewID")%>' 
                                                                Target="_blank" Text='<%# Eval("Created_By_Name")%>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="15%" 
                                                        SortExpression="FOLLOWUP">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFOLLOWUPAC" runat="server" 
                                                                Text='<%#Eval("Action").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark" ItemStyle-Width="75%" 
                                                        SortExpression="FOLLOWUP">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFOLLOWUPAC" runat="server" 
                                                                Text='<%#Eval("Remark").ToString().Replace("\n","<br>") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="ldl1" runat="server" Text="No followups !!"></asp:Label>
                                                </EmptyDataTemplate>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        
                                    </td>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="hidenTotalrecords" runat="server" />
                                            <asp:HiddenField ID="HCurrentIndex" runat="server" />
                                        </td>
                                    </tr>
                                </tr>
                                 
                            </table>
                        </td>
                                
                    </tr>
                </table>
            </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 30%;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%" valign="top" >
                                        Work Category &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right" valign="top">
                                        *
                                    </td>
                                    <td align="left" style="width: 34%" valign="top" >
                                         <asp:DropDownList ID="ddlWorkCategory" CssClass="txtInput" Width="97%" runat="server" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    </tr><tr>
                                    <td align="right" style="width: 15%" valign="top"   >
                                        Hazard Description &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right" valign="top"   >   *
                                    </td>
                                    <td align="left" style="width: 34%">
                                        <asp:TextBox ID="txtHazardDesc" CssClass="txtInput" Width="95%" MaxLength="2000" 
                                            runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                       Control Measure &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right" valign="top"   >   *
                                    </td>
                                    <td align="left">
                                <asp:TextBox ID="txtControlMeasure" CssClass="txtInput" Width="95%" MaxLength="2000" 
                                            runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td> </tr><tr>
                                    <td align="right" valign="top" valign="top"   >   
                                        Severity &nbsp;:&nbsp;
                                    </td>
                                     <td style="color: #FF0000; width: 1%" align="right" valign="top"   >   *
                                    </td>
                                    <td valign="top">
                                          <asp:DropDownList ID="ddlSeverity" CssClass="txtInput" Width="97%" 
                                              runat="server" onselectedindexchanged="ddlSeverity_SelectedIndexChanged"  AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        Likelihood &nbsp;:&nbsp;
                                    </td>
                                      <td style="color: #FF0000; width: 1%" align="right" valign="top"   >   *
                                    </td>
                                    <td valign="top">
                                          <asp:DropDownList ID="ddlLikelihood" CssClass="txtInput" Width="97%" 
                                              runat="server" onselectedindexchanged="ddlLikelihood_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td> </tr><tr>
                                     <td align="right">
                                         Initial Risk &nbsp;:&nbsp;
                                    </td>
                                     <td style="color: #FF0000; width: 1%" align="right" valign="top"   >   *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtInitiakRisk" CssClass="txtInput" Width="95%" MaxLength="10" runat="server" ReadOnly="true" Enabled="false">
                                        </asp:TextBox>
                                         <asp:TextBox ID="txtInitiakRiskValue" CssClass="txtInput" Width="95%" MaxLength="10" runat="server" style="display:none">
                                        </asp:TextBox>
                                             <asp:TextBox ID="txtInitialRiskColor" CssClass="txtInput" Width="95%" MaxLength="10" runat="server" style="display:none">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                   <td align="right" valign="top">
                                      Additional Control Measure &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                <asp:TextBox ID="txtAdditionalCntrolMeasure" CssClass="txtInput" Width="95%" MaxLength="2000" 
                                            runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                   </tr><tr>
                                    <td align="right" valign="top">
                                        Modified Risk &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                   <td valign="top">
                                          <asp:DropDownList ID="ddlModifiedRisk" CssClass="txtInput" Width="97%" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                             
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; border-style: solid;
                                        border-color: Silver; border-width: 1px">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" OnClientClick="return Validation();" />
                                        <asp:TextBox ID="txtHazardID" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 0px solid #cccccc;
                                            background-color: #FDFDFD">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                        * Mandatory fields
                                    </td>
                                </tr>
                            </table>
                        </div>
                         <div id="divFollow" title="Follow-Up Remarks" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 40%;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                              <tr>
                              <td align="right" style="width: 6%" valign="top"   >
                                        Remark &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right" valign="top"   >   *
                                    </td>
                                    <td align="left" style="width: 34%">
                                        <asp:TextBox ID="txtFWRemark" CssClass="txtInput" Width="95%" MaxLength="1000" 
                                            runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                              </tr>
                                <tr>
                                    <td colspan="6" style="font-size: 11px; text-align: center; border-style: solid;
                                        border-color: Silver; border-width: 1px">
                                        <asp:Button ID="btnSaveFollowup" runat="server" Text="Save" 
                                            OnClientClick="return ValidationFW();" onclick="btnSaveFollowup_Click"   />
                     
                                        
                                           
                                    </td>
                                </tr>
                                
                               
                            </table>
                        </div>
                        <div id="divApv" title="Approve/Rework Assessment" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 40%;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                              <tr>
                              <td align="right" style="width: 6%" valign="top"   >
                                        Remark &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right" valign="top"   >   *
                                    </td>
                                    <td align="left" style="width: 34%">
                                        <asp:TextBox ID="txtRemark" CssClass="txtInput" Width="95%" MaxLength="1000" 
                                            runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                              </tr>
                                <tr>
                                    <td colspan="6" style="font-size: 11px; text-align: center; border-style: solid;
                                        border-color: Silver; border-width: 1px">
                                        <asp:Button ID="bntApprove" runat="server" Text="Approve" 
                                            OnClientClick="return ValidationRApr();" onclick="bntApprove_Click" />
                                        <asp:Button ID="btnRework" runat="server" Text="Rework"  
                                            OnClientClick="return ValidationRApr();" style="margin-left:24px" 
                                            onclick="btnRework_Click" />
                                        
                                    </td>
                                </tr>
                                
                               
                            </table>
                        </div>
                        </div>
                      
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
