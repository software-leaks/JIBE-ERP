<%@ Page Title="Vessel Manager Registration" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SurveyDashboard.aspx.cs" Inherits="SurveyDashboard" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link type="text/css" href="Styles/ui-lightness/jquery-ui-1.8.14.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="scripts/jquery.min.js"></script>
    <script type="text/javascript" src="scripts/jquery-ui-1.8.14.custom.min.js"></script>
    <style type="text/css">
    .txtInput {min-height:25px;font-size:14px}
    </style>
    <script type="text/javascript">

        function load_Contract() {
            var flagid = window.event.srcElement.id;
            var url = "ContractPreview.aspx?flag=" + flagid + "&rnd=" + Math.random();
            //alert(url);
            $.get(url, function (data) {
                $('#dvPreview').html(data);
            });

        }

        function validation() {

            if (document.getElementById("ctl00_MainContent_ddlCompanyType").value == "0") {
                alert("Please select company type.");
                document.getElementById("ctl00_MainContent_ddlCompanyType").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtCompCode").value.trim() == "") {
                alert("Please enter company code.");
                document.getElementById("ctl00_MainContent_txtCompCode").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_txtCompCode").value != "") {

                if (isNaN(document.getElementById("ctl00_MainContent_txtCompCode").value)) {
                    alert("This field is allow only numeric value");
                    document.getElementById("ctl00_MainContent_txtCompCode").focus()
                    return false;
                }

            }

            if (document.getElementById("ctl00_MainContent_txtCompName").value.trim() == "") {
                alert("Please enter company name.");
                document.getElementById("ctl00_MainContent_txtCompName").focus();
                return false;
            }

//            if (document.getElementById("ctl00_MainContent_txtDtIncorp").value.trim() == "") {
//                alert("Please enter Date of Incorp.");
//                document.getElementById("ctl00_MainContent_txtDtIncorp").focus();
//                return false;
//            }
//            if (document.getElementById("ctl00_MainContent_txtShortName").value.trim() == "") {
//                alert("Please enter company short name.");
//                document.getElementById("ctl00_MainContent_txtShortName").focus();
//                return false;
//            }


            return true;
        }       
    </script>
    <style type="text/css">
        .style1
        {
            height: 17px;
        }
        .style2
        {
            width: 15%;
            height: 34px;
        }
        .style3
        {
            width: 1%;
            height: 34px;
        }
        .style4
        {
            width: 25%;
            height: 34px;
        }
        .style5
        {
            width: 20%;
            height: 34px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<div class="page-title">
         Contract Template Editor 
    </div>--%>
    <div class="page-title">
        Register Vessel Manager
    </div>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="/Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanelSearchCrew" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                  <%--  <td style="vertical-align: top; width: 200px;">
                        <table cellspacing="1" cellpadding="1" style="border: 1px solid #610B5E; width: 100%;">
                            <tr>
                              
                                <th style="background-color: #336666; color: White;" colspan="2">
                                    Menu
                                </th>
                            </tr>
                            <tr style="background-color: White; color: Black; font-weight: bold;">
                                <td>
                                    <asp:LinkButton ID="hlSendInvitation" runat="server" Text="Register VesselManager" 
                                        onclick="hlSendInvitation_Click" ></asp:LinkButton>
                                </td>
                            </tr>
                            <tr style="background-color: White; color: Black; font-weight: bold;">
                                <td class="style1">
                                       <asp:HyperLink ID="hlRegisterdVessel" runat="server" Text="Registered Vessel" Target="_blank"  NavigateUrl="~/Infrastructure/Libraries/SearchVessel.aspx"></asp:HyperLink>
                                </td>
                            </tr>    
                             <tr style="background-color: White; color: Black; font-weight: bold;">
                                <td class="style1">
                                  <asp:HyperLink ID="hlVesselGA" runat="server" Text="Vessel GA" Target="_blank" NavigateUrl="~/Infrastructure/Libraries/VesselGA.aspx"></asp:HyperLink>
                                </td>
                            </tr>  
                            <tr style="background-color: White; color: Black; font-weight: bold;">
                                <td class="style1">
                                   <asp:HyperLink ID="hlChecklistType" runat="server" Text="Add Checklist Type" Target="_blank" NavigateUrl="~/Technical/Inspection/EvaluationLibrary.aspx"></asp:HyperLink>
                                </td>
                            </tr>    
                             <tr style="background-color: White; color: Black; font-weight: bold;">
                                <td class="style1">
                                     <asp:HyperLink ID="hlCriteria" runat="server" Text="Add Criteria" Target="_blank" NavigateUrl="~/Technical/Inspection/Criteria.aspx"></asp:HyperLink>
                                     </td>
                            </tr>        
                            <tr style="background-color: White; color: Black; font-weight: bold;">
                                <td class="style1">
                                         <asp:HyperLink ID="hlChecklistIndex" runat="server" Text="Checklist" Target="_blank" NavigateUrl="~/Technical/Inspection/ChecklistIndex.aspx"></asp:HyperLink>
                                </td>
                            </tr>    
                             <tr style="background-color: White; color: Black; font-weight: bold;">
                                <td class="style1">
                                       <asp:HyperLink ID="hlAddInspector" runat="server" Text="Add Inspector" Target="_blank" NavigateUrl="~/infrastructure/libraries/userList.aspx"></asp:HyperLink>
                                </td>
                            </tr>    
                                <tr style="background-color: White; color: Black; font-weight: bold;">
                                <td class="style1">
                                    <asp:HyperLink ID="hlSheduleInspection" runat="server" Text="Shedule Inspection" Target="_blank" NavigateUrl="~/Technical/Worklist/SuperintendentInspection.aspx"></asp:HyperLink>
                                </td>
                            </tr>    
                        </table>
                        <br />
                        <br />
                     
                    </td>--%>
                    <td style="vertical-align: top">
                        <table cellspacing="1" cellpadding="1" style="border: 0px solid #610B5E; width: 100%;" id="dvAddtd" runat="server" >
                            <tr>
                                <%-- <th style="background-color: #336666; color: White; font-size: 14px;">
                                    Template Editor
                                </th>--%>
                               <%-- <th style="background-color: #336666; color: White; font-size: 14px;">
                                    Admin Panel
                                </th>--%>
                            </tr>
                            <tr>
                                <td >
                                    <%--<div style="padding: 2px;" class="gradiant-css-orange">
                                        <asp:Panel ID="pnlContractTemplate" runat="server" Visible = "false">
                                            Vessel Flag:
                                            <asp:HiddenField ID="hdnVessel_Flag" runat="server" />
                                            <asp:TextBox ID="txtTemplateName" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:Button ID="btnSaveTemplate" runat="server" Text="Save" OnClick="btnSaveTemplate_Click" />
                                        </asp:Panel>
                                        <asp:Panel ID="pnlSideletter" runat="server" Visible="false">
                                            Side Letter:
                                            <asp:HiddenField ID="hdnSideletterID" runat="server" />
                                            <asp:TextBox ID="txtSideletter" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:Button ID="btnSideletter" runat="server" Text="Save" OnClick="btnSaveSideletterTemplate_Click" />
                                        </asp:Panel>
                                    </div>--%>
                                    <%-- <iframe id="TestIFRM" height="700px" width="1000px" ></iframe>--%>
                                    <%--  <CKEditor:CKEditorControl ID="txtTemplateBody" runat="server"></CKEditor:CKEditorControl>--%>
                                    
                                    <%--title="<%= OperationMode %>"--%>
                                    <div id="divadd" runat="server"  style="display: block;
                border: 1px solid Gray; font-family: Tahoma; text-align: left; font-size: 12px;
                color: Black; width: 68%; margin-left:135px"  >
                
                    <table width="70%" cellpadding="0" cellspacing="10" >
                        <tr style="display:none;" >
                            <td align="right" style="width: 15%">
                                Company Type : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="right" style="width: 25%">
                                <asp:DropDownList ID="ddlCompanyType" runat="server" CssClass="txtInput">
                                    <%--onchange="CompanyTypeChanged(this.options[this.selectedIndex].value)"--%>
                                </asp:DropDownList>
                            </td>
                            <td align="right" style="width: 20%">
                                Relationship with Parent : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                <asp:Label ID="td_Relation" Text="*" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                            <td align="right" style="width: 25%">
                                <asp:DropDownList ID="ddlRelation" runat="server" Width="102%" CssClass="txtInput">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td align="right">
                                Parent Company : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                <asp:Label ID="td_ParentCompany" Text="*" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlParentCompany" runat="server" Width="102%" CssClass="txtInput">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Company Code : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="right">
                                <asp:TextBox ID="txtCompCode" runat="server" CssClass="txtInput" Width="100%">123</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Company Name : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtCompName" runat="server" MaxLength="50"  Width="300px" CssClass="txtInput"></asp:TextBox>
                            </td>
                            <td align="right" style="display:none;">
                                Company Short Name : &nbsp;
                            </td>
                            <td style="color: #FF0000; display:none; width: 1%" align="right">
                                *
                            </td>
                            <td align="left" style="display:none;">
                                <asp:TextBox ID="txtShortName" runat="server" MaxLength="10" CssClass="txtInput"
                                    Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr  style="display:none;">
                            <td align="right">
                                Registration No. : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRegNo" runat="server" MaxLength="250" CssClass="txtInput" Width="100%"></asp:TextBox>
                            </td>
                            <td align="right">
                                Date of Incorp. : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDtIncorp" runat="server" CssClass="txtInput" Width="100%">26/01/2015</asp:TextBox>
                                <cc1:CalendarExtender ID="calDtIncorp" runat="server" Enabled="True" TargetControlID="txtDtIncorp"
                                    Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr style="display:none;">
                            <td align="right">
                                Country of Incorp. : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <%--<asp:TextBox ID="txtCountryIncorp"  runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlCountryIncorp" runat="server" Width="102%" CssClass="txtInput">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Currency : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCurrency" runat="server" Width="102%" CssClass="txtInput">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Address : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left" >
                                <asp:TextBox ID="txtAddrerss" TextMode="MultiLine" MaxLength="250" Rows="3" Width="298px"
                                    CssClass="txtInput"  runat="server"></asp:TextBox>
                            </td>
                           <%-- <td align="right" valign="top">
                                Country : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="ddlAddressCountry" runat="server" CssClass="txtInput" Width="102%">
                                </asp:DropDownList>
                            </td>--%>
                        </tr>
                        <tr>
                            <td align="right">
                                Email : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="250" CssClass="txtInput" Width="300px"></asp:TextBox>
                            </td>
                            <td align="right" style="display:none;">
                                Email 2 : &nbsp;
                            </td>
                            <td style="color: #FF0000; display:none; width: 1%" align="right">
                            </td>
                            <td align="left" style="display:none;">
                                <asp:TextBox ID="txtEmail2" runat="server" MaxLength="250" CssClass="txtInput" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Phone : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPhone" runat="server" MaxLength="50" CssClass="txtInput" Width="300px"></asp:TextBox>
                            </td>
                            <td align="right" style="display:none;">
                                Phone 2 : &nbsp;
                            </td>
                            <td style="color: #FF0000; display:none; width: 1%" align="right">
                            </td>
                            <td align="left" style="display:none;">
                                <asp:TextBox ID="txtPhone2" runat="server" MaxLength="100" CssClass="txtInput" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Fax : &nbsp;
                            </td>
                            <td style="color: White; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFax1" runat="server" MaxLength="50" CssClass="txtInput" Width="300px"></asp:TextBox>
                            </td>
                            <td align="right" style="display:none;">
                                Fax 2 : &nbsp;
                            </td>
                            <td style="color: White; display:none; width: 1%" align="right">
                            </td>
                            <td align="left" style="display:none;">
                                <asp:TextBox ID="txtFax2" runat="server" MaxLength="50" CssClass="txtInput" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                         <td align="right" valign="top">
                                Country : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left" valign="top" >
                                <asp:DropDownList ID="ddlAddressCountry" runat="server" CssClass="txtInput" Width="304px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="font-size: 11px; text-align: center; 
                                padding: 5px 0px 5px 0px; ">
                                <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return  validation();"
                                    OnClick="btnsave_Click" />
                                <asp:TextBox ID="txtCompanyID" runat="server" Visible="false" Width="1%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 0px solid #cccccc;
                                    background-color: #FDFDFD">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="right" style="color: #FF0000; font-size: small;">
                                * Mandatory fields
                            </td>
                        </tr>
                    </table>
                
            </div>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
         
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
