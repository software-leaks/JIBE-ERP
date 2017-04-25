<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_Voyages.aspx.cs"
    Inherits="Crew_CrewDetails_Voyages" %>

<%@ Register Src="~/UserControl/ctlPortList.ascx" TagName="PortList" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ctlAirPortList.ascx" TagName="AirPortList" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showNVessel() {
            showModal("divVesselType");
        }
        function hideVessel() {
            hideModal("divVesselType");
        }
        function showNationalityApproval() {
            //$('[id$=txtAppRequest]').val("Request of approval for more than 2 staffs of the same rank/category.");
            showModal("dvNationalityApproval");
        }
        function DocOpen(docpath) {
            window.open(docpath);
        }

        function MandatoryCheck() {
            if ($("[id$='txtIssueDate']").val() == "") {
                alert('Issue date is mandatory');
                return false;
            }
            return DateValidation();
        }
        function DateValidation() {
            var v = $("[id$='txtJoiningDate']").val();
            var v1 = $("[id$='txtSignOnDate']").val();
            v = ConvertDt_oldFormat(v, strDateFormat);
            if (v1 != "") {
                v1 = ConvertDt_oldFormat(v1, strDateFormat);
            }
            var Today = new Date();
            var JoiningdateArray = v.split("/");
            var newJoiningdate = JoiningdateArray[1] + '/' + JoiningdateArray[0] + '/' + JoiningdateArray[2];

            var SignondateArray = v1.split("/");
            var Signondate = SignondateArray[1] + '/' + SignondateArray[0] + '/' + SignondateArray[2];

            if (new Date(newJoiningdate) > new Date(Signondate)) {
                alert('Sign-On Date should be greater than Contract Date');
                document.getElementById('txtSignOnDate').value = '';
                return false;
            }
           
            if (new Date(Signondate) >= new Date(Today)) {
                alert('Sign-On Date cannot be future date..!');
                document.getElementById('txtSignOnDate').value = '';
                return false;
            }
            return DateValidationEOC;
        }

        function DateValidationSignOff() {
          //  var v = $("[id$='txtJoiningDate']").val();
            var v1 = $("[id$='txtSignOnDate']").val();
            var v2 = $("[id$='txtSignOffDate']").val();

            v1 = ConvertDt_oldFormat(v1, strDateFormat);
            v2 = ConvertDt_oldFormat(v2, strDateFormat);

            var SignondateArray = v1.split("/");
            var Signondate = SignondateArray[1] + '/' + SignondateArray[0] + '/' + SignondateArray[2];

            var SignOffdateArray = v2.split("/");
            var Signoffdate = SignOffdateArray[1] + '/' + SignOffdateArray[0] + '/' + SignOffdateArray[2];

            if (new Date(Signondate) > new Date(Signoffdate)) {
                alert('Sign-Off Date cannot be less than Sign-On Date ');
                document.getElementById('txtSignOffDate').value = '';
                return false;
            }
            return DateValidation();
        }

        function DateValidationEOC() {
            var v = $("[id$='txtSignOnDate']").val();
            var v1 = $("[id$='txtCOCDate']").val();

            v = ConvertDt_oldFormat(v, strDateFormat);
            v1 = ConvertDt_oldFormat(v1, strDateFormat);

            var SignOndateArray = v.split("/");
            var SignOndate = SignOndateArray[1] + '/' + SignOndateArray[0] + '/' + SignOndateArray[2];

            var EOCdateArray = v1.split("/");
            var EOCdate = EOCdateArray[1] + '/' + EOCdateArray[0] + '/' + EOCdateArray[2];

            if (new Date(EOCdate) < new Date(SignOndate)) {
                alert('EOC Date should be greater than Sign-On Date');
                document.getElementById('txtCOCDate').value = '';
                return false;
            }
            return true;
        }       
 
    </script>
    <style type="text/css">
        .GridView-css
        {
            margin-top: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <div id="dvCrewVoyagesGrid">
        <asp:UpdatePanel ID="UpdatePanel_Msg" runat="server">
            <ContentTemplate>
                <div class="error-message">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Panel ID="pnlView_Voyages" runat="server" Visible="false">
            <table style="width: 100%" cellspacing="0" cellpadding="0">
                <tr>               
                    <td style="text-align: right">
                        <div id="dvVoyageInfo" style="background-color: Yellow; font-weight: bold; text-align: center;
                            width: 200px; float: right;">
                        </div>
                         <asp:hiddenfield id="hdfAttachmentPath" runat="server"/>
                         <asp:HyperLink ID="hlnkAttachmentPath" runat="server" Text="Payroll" Visible="false" ToolTip="Click to open in new window" Target ="_NewWindow"  ></asp:HyperLink>
                        </td>
                    <td style="width: 120px; text-align: center;">
                        <asp:ImageButton runat="server" ID="ImgAddVoyage" ImageUrl="~/Images/AddVoyage1.png"
                            OnClientClick="AndVoyage($('[id$=HiddenField_CrewID]').val());return false;" />
                    </td>
                    <td style="width: 20px; text-align: right;">
                        <asp:ImageButton ToolTip="Refresh" runat="server" ID="ImgReloadVoyage" ImageUrl="~/Images/reload.png"
                            OnClientClick="GetCrewVoyages($('[id$=HiddenField_CrewID]').val());return false;" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView_Voyages" runat="server" DataKeyNames="ID" AllowSorting="False"
                AutoGenerateColumns="false" GridLines="None" CellPadding="3" CellSpacing="1"
                Width="100%" CssClass="GridView-css" OnRowDataBound="GridView_Voyages_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Vessel" SortExpression="VESSEL_SHORT_NAME">
                        <ItemTemplate>
                            <asp:Label ID="lblVesselName" runat="server" Text='<%# Eval("VESSEL_SHORT_NAME") %>'
                                class='vesselinfo' vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Joining Type" SortExpression="JoiningTypeName">
                        <ItemTemplate>
                            <asp:Label ID="lblJoiningTypeName" runat="server" Text='<%# Eval("JoiningTypeName") %>'
                                ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Joining Rank" SortExpression="Rank_Short_Name">
                        <ItemTemplate>
                            <asp:Label ID="lblJoiningRank" runat="server" Text='<%# Eval("Rank_Short_Name") %>'></asp:Label>
                            <asp:ImageButton ID="imgVerify" runat="server" Visible="false" ImageUrl="~/Images/Select.png"
                                OnClientClick='<%# "ViewPopup_ApproveRank(" + Eval("ID").ToString() + "," + Eval("Joining_Rank").ToString() + ",&#39;" + UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Joining_Date"))) + "&#39;,this);return false;" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contract Date" SortExpression="Joining_Date">
                        <ItemTemplate>
                            <asp:Label ID="lblJoiningDate" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Joining_Date"))) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sign On Date" SortExpression="Sign_On_Date">
                        <ItemTemplate>
                            <asp:Label ID="lblSignOnDate" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Sign_On_Date"))) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sign On Port" SortExpression="Joining_Port">
                        <ItemTemplate>
                            <asp:Label ID="lblJoiningPort" runat="server" Text='<%# Eval("PORT_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EOC Date" SortExpression="COCDate">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkCOCDate" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("COCDate"))) %>'
                                OnClientClick='<%#"EditEOC(" + Eval("ID").ToString()+ "," + Eval("CrewID").ToString() +"); return false;" %>'
                                Visible='<%# (Eval("OfficePortageBillConsidered").ToString() == "True" || Eval("VesselPortageBillConsidered").ToString() == "True" ) ? true : false %>' ></asp:LinkButton>
                            &nbsp;&nbsp;
                            <asp:Image ID="ImgCOCModified" runat="server" ImageUrl="~/images/red-dot.png" CausesValidation="False"
                                Height="16px" AlternateText='<%# Eval("COCRemark")%>' CssClass="imgCOC"></asp:Image>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sign Off Date" SortExpression="Sign_Off_Date">
                        <ItemTemplate>
                            <asp:Label ID="lblSignOffDate" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Sign_Off_Date"))) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DOA Home Port">
                        <ItemTemplate>
                            <asp:Label ID="lblDOA_Home" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DOA_HomePort"))) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sign Off Port" SortExpression="Sign_Off_Port">
                        <ItemTemplate>
                            <asp:Label ID="lblSignOffPort" runat="server" Text='<%# Eval("Sign_Off_Port_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MPA Ref">
                        <ItemTemplate>
                            <asp:Label ID="lblMPA_REF" runat="server" Text='<%# Eval("MPA_REF") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sign Off Reason">
                        <ItemTemplate>
                            <asp:Label ID="lblSignOffReason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Co./Rank Sr.Year" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSeniority" runat="server" Text='<%# ((Eval("CompanySeniorityYear").ToString()=="" ? "-" : Eval("CompanySeniorityYear")) + "/" + (Eval("RankSeniorityYear").ToString()=="" ? "-" : Eval("RankSeniorityYear"))) %>' CssClass="linkbtn"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:Image ID="imgCrewTransferPromotion" runat="server" ImageUrl="~/Images/Transfer.png"
                                Height="20px" CssClass="transfer-link" BorderStyle="None" ImageAlign="AbsMiddle" Visible='<%# Eval("VesselPortageBillConsidered").ToString() == "True" ? true : false %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:Image ID="imgNewTravelRequest" runat="server" ImageUrl="~/Images/flightButton.png"
                                Height="20px" CssClass="travel-link" BorderStyle="None" ImageAlign="AbsMiddle" Visible='<%# Eval("VesselPortageBillConsidered").ToString() == "True" ? true : false %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:Image ID="imgContractDownload" Height="18px" runat="server" ImageUrl="~/Images/pdf_download.png"
                                CausesValidation="False" CssClass="download-contract" AlternateText="Contract" Visible="false"
                                ImageAlign="AbsMiddle" ></asp:Image>
                            <asp:Image ID="imgViewContract" runat="server" ImageUrl="~/Images/printer.png" BorderStyle="None"
                                CssClass="print-link" ImageAlign="AbsMiddle" Visible='<%# Eval("VesselPortageBillConsidered").ToString() == "True" ? true : false %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:Image ID="imgViewWages" runat="server" ImageUrl="~/Images/dollar.png" BorderStyle="None"
                                Height="20px" CssClass="wages-link" ImageAlign="AbsMiddle" Visible='<%#  ( (Eval("OfficePortageBillConsidered").ToString() == "True" || Eval("VesselPortageBillConsidered").ToString() == "True" ) &&  Eval("ServiceConsidered").ToString() == "True" ) ? true : false %>' />
                            <asp:Image ID="imgSideLetter" runat="server" ImageUrl="~/Images/sideletter.png" BorderStyle="None"
                                Height="20px" CssClass="wages-link" ImageAlign="AbsMiddle" />
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <img id="imgEventON" class="event-link" src='<%#(Eval("Event_Status_ON").ToString()=="1")?"../Images/E_ON_OPEN.png":"../Images/E_ON_CLOSED.png"%>'
                                            onclick='<%# "showEvent("+Eval("EventID_ON").ToString()+","+Eval("CrewID").ToString()+")" %>'
                                            alt="Event" style='<%# Eval("VesselPortageBillConsidered").ToString() == "True" ? (Eval("EventID_ON").ToString()=="0"? "visibility:visible;display:none" : "visibility:visible;display:block") : "visibility:hidden" %>'
                                            />
                                    </td>
                                    <td>
                                        <img id="imgEventOFF" class="event-link" src='<%#(Eval("Event_Status_OFF").ToString()=="1")?"../Images/E_OFF_OPEN.png":"../Images/E_OFF_CLOSED.png"%>'
                                            onclick='<%# "showEvent("+Eval("EventID_OFF").ToString()+","+Eval("CrewID").ToString()+")" %>'
                                            alt="Event" style='<%#  Eval("VesselPortageBillConsidered").ToString() == "True" ? (Eval("EventID_OFF").ToString()=="0"? "visibility:visible;display:none" : "visibility:visible;display:block") : "visibility:hidden" %>' />
                                   </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Documents">
                        <ItemTemplate>
                            <a href='CrewVoyageDocuments.aspx?VoyID=<%#Eval("ID")%>&CrewID=<%#Eval("CrewID")%>'
                                target="_blank">
                                <asp:Image ID="lnkUpload" runat="server" ImageUrl="~/Images/DocumentUpLoad.png" BorderStyle="None"  Visible='<%# Eval("VesselPortageBillConsidered").ToString() == "True" ? true : false %>'
                                    CssClass="doc-link" ImageAlign="AbsMiddle"></asp:Image></a> <a href='CrewVoyageDocuments.aspx?VoyID=<%#Eval("ID")%>&CrewID=<%#Eval("CrewID")%>'
                                        target="_blank">
                                        <asp:Image ID="imgPendingCount" runat="server" ImageUrl="~/Images/folder-error.png"
                                            BorderStyle="None" Height="18px" ImageAlign="AbsMiddle"  /></a>
                        </ItemTemplate>                      
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                           <ItemTemplate>
                            <a href='CrewVerifiedDocuments.aspx?VoyID=<%#Eval("ID")%>&CrewID=<%#Eval("CrewID")%>'
                                target="_blank">
                                <asp:Image ID="lnkVerified" runat="server" ImageUrl="~/Images/checked.gif" BorderStyle="None"  Visible='<%# Eval("VerifiedDocumentCount").ToString() == "0" ? false : true %>'
                                    CssClass="doc-link" ImageAlign="AbsMiddle" ToolTip="View Verified Documents"></asp:Image></a> <a href='CrewVerifiedDocuments.aspx?VoyID=<%#Eval("ID")%>&CrewID=<%#Eval("CrewID")%>'
                                        target="_blank">    
                            </ItemTemplate>
                         </asp:TemplateField>                 
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="lnkEditVoyage" ToolTip ="Edit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                AlternateText="Edit" OnClientClick='<%#"EditVoyage(" + Eval("CrewID").ToString()+ "," + Eval("ID").ToString() +"); return false;" %>'
                                Visible='<%# Eval("PortageBillFinalized").ToString() == "0" ? true : false %>'>
                            </asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>                  

                    <asp:TemplateField ShowHeader="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="lnkDeleteVoyage" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png" 
                                CausesValidation="False" OnClientClick='<%#"DeleteVoyage(" + Eval("CrewID").ToString()+ "," + Eval("ID").ToString() + "," + GetSessionUserID().ToString() + "); return false;" %>'
                                AlternateText="Delete"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="imgRecordInfo" ToolTip="Info" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_DTL_CREW_VOYAGES&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>'
                                AlternateText="info"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="HeaderStyle-css" />
                <PagerStyle CssClass="PagerStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <EditRowStyle CssClass="EditRowStyle-css" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="pnlAdd_Voyages" runat="server" Visible="false">
            <asp:UpdatePanel ID="UpdatePanel_AddEditVoyage" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="HiddenField_OldContractDate" runat="server" />
                    <asp:HiddenField ID="HiddenField_NewContractDate" runat="server" />
                    <fieldset>
                        <legend>Service/Status Detail :</legend>
                        <table border="0" cellpadding="3" cellspacing="0" width="100%" class="dataTable">
                            <tr>
                                <td style="width: 50%; vertical-align: top;">
                                    <table border="0" cellpadding="3" cellspacing="0" width="100%">
                                        <tr class="highlight_jtype">
                                            <td>
                                                Joining Type:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlJoinType" runat="server" Width="156px" DataTextField="Joining_Type" 
                                                    DataValueField="ID" AutoPostBack="true"
                                                    onselectedindexchanged="ddlJoinType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%">
                                                Fleet
                                            </td>
                                            <td style="width: 70%">
                                                <asp:DropDownList ID="ddlFleet" runat="server" Width="156px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Vessel Name:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlVesselList" runat="server" Width="156px" DataTextField="Vessel_Name"
                                                    OnSelectedIndexChanged="ddlVesselList_SelectedIndexChanged" AutoPostBack="true"
                                                    DataValueField="Vessel_ID">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td>
                                                Contract Template:
                                            </td>
                                            <td><%-- OnSelectedIndexChanged="ddlContract_SelectedIndexChanged"--%>
                                                <asp:DropDownList ID="ddlContract" runat="server" Width="156px" DataTextField="Contract_Name"
                                                    AutoPostBack="true"
                                                    DataValueField="ContractId">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td>
                                                Joining Rank:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDLJoiningRank" runat="server" Width="156px" DataSourceID="ObjDataSourceDDLJoinRank"
                                                    AutoPostBack="true" DataTextField="Rank_Short_Name" DataValueField="ID" AppendDataBoundItems="true"
                                                    OnSelectedIndexChanged="DDLJoiningRank_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CompareValidator ID="CompareValidatorport" ValidationGroup="vggrid2" ControlToValidate="DDLJoiningRank"
                                                    Operator="NotEqual" ValueToCompare="select" Type="String" runat="server" ErrorMessage="Please select rank"></asp:CompareValidator>
                                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="CompareValidatorport"
                                                    runat="server">
                                                </cc1:ValidatorCalloutExtender>
                                                <asp:ObjectDataSource ID="ObjDataSourceDDLJoinRank" runat="server" SelectMethod="Get_RankList"
                                                    TypeName="SMS.Business.Crew.BLL_Crew_Admin"></asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                        <tr id="trRankScale" runat="server">
                                            <td>
                                                Rank Scale:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRankScale" runat="server" Width="156px" ></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Contract Date:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtJoiningDate" runat="server" Width="150px" AutoPostBack="true" onchange="DateValidation()"
                                                    OnTextChanged="txtJoiningDate_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtJoiningDate"></ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Sign-On Date:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSignOnDate" runat="server" Width="150px"  onchange="DateValidation()"
                                                    AutoPostBack="true" ontextchanged="txtSignOnDate_TextChanged" ></asp:TextBox><asp:Image
                                                    ID="imgSignOn" runat="server" ImageUrl="~/Images/folder-error.png" BorderStyle="None"
                                                    Height="18px" ImageAlign="AbsMiddle" Visible="false" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtSignOnDate"></ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Joining Port:
                                            </td>
                                            <td>
                                                <asp:Panel id="pnlJoiningPort" runat="server">
                                                    <uc:PortList ID="ctlJoiningPort" runat="server" Width="130px" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                EOC Date:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCOCDate" runat="server" Width="150px"  onchange="DateValidationEOC()"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCOCDate"></ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%; vertical-align: top;">
                                <asp:Panel ID="pnlSignOff" runat="server">
                                <table border="0" cellpadding="3" cellspacing="0" width="100%" >
                                        <tr>
                                            <td>
                                                Sign-Off Date:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSignOffDate" runat="server" Width="150px" AutoPostBack="true" ontextchanged="txtSignOffDate_TextChanged"  onchange="DateValidationSignOff()" ></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSignOffDate"></ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Sign-Off Port:
                                            </td>
                                            <td>
                                                <asp:Panel id="pnlSignOffPort" runat="server">
                                                    <uc:PortList ID="ctlSignOffPort" runat="server" Width="130px" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Sign-Off Reason:
                                            </td>
                                            <td>
                                               
                                                   
                                                <asp:DropDownList ID="ddlSignOffReason" runat="server"  Width="156px">
                                                    
                                                </asp:DropDownList>
                                           </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                MPA Ref:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMPARef" runat="server" Width="150px" AutoPostBack="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                DOA Home Port:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDOAHomePort" runat="server" Width="150px" AutoPostBack="true"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtDOAHomePort"></ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnSaveVoyage" runat="server" Text=" Save " OnClick="btnSaveVoyage_Click" OnClientClick="DateValidationSignOff()" > 
                                    </asp:Button>
                                    <asp:Button ID="btnCloseVoyage" runat="server" Text="Cancel" OnClientClick="parent.hideModal('dvPopupFrame'); return false;">
                                    </asp:Button>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
                         

    </div>
     <div ID="divVesselType" runat="server" title="Confirmation Required" style="width: 500px; font-size: 12px; display: none" >
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <img src="../Images/alert.jpg" />
                            <asp:Label ID="lblConfirmationTitle" runat="server" style="font-weight: bold;" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <asp:RadioButtonList ID="rdbVesselTypeAssignmentList" runat="server" RepeatDirection="Vertical">
                                <asp:ListItem Value="1" Selected="True">Assign and add the vessel type to the crew member vessel type list</asp:ListItem>
                                <asp:ListItem Value="0">Assign without adding the vessel type</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Button Text="Cancel"  runat="server" OnClientClick="return hideVessel();"/>
                            <asp:Button ID="btnAssignVesselType" Text="Assign"  runat="server" OnClick="btnAssignVesselType_Click"/>
                        </td>
                    </tr>                
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvNationalityApproval" title="Approval Details" style="width: 500px; font-size: 12px;
        display: none">
        <asp:UpdatePanel ID="UpdatePanel_NationalityApproval" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:HiddenField ID="hdnAppVesselID" runat="server" />
                <asp:HiddenField ID="hdnAppCrewID" runat="server" />
                <asp:HiddenField ID="hdnAppJoiningRankID" runat="server" />
                <asp:HiddenField ID="hdnAppCurrentRankID" runat="server" />
                <asp:HiddenField ID="hdnAppEventID" runat="server" />
                <asp:HiddenField ID="hdnAppSOffCrewID" runat="server" />
                <table style="width: 100%;" cellpadding="5">
                    <tr>
                        <td colspan="2">
                            The ON-SIGNER can not join this vessel as there are already two or more staffs of
                            the same nationality has been joined the vessel
                            <br />
                            <br />
                            Please take approval if you still want him to join the vessel
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            Vessel
                        </td>
                        <td>
                            <asp:Label ID="lblAppVessel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            Joining Rank
                        </td>
                        <td>
                            <asp:Label ID="lblAppRank" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%">
                            Request Details
                        </td>
                        <td>
                            <asp:TextBox ID="txtAppRequest" runat="server" TextMode="MultiLine" Width="200" Height="80"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right">
                            <asp:Button ID="btnNationalityApproval" Text="Send for Approval" runat="server" OnClick="btnNationalityApproval_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
