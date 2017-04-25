<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SCM_Report_Details.aspx.cs"
    Inherits="SCM_Report_Details" Title="Safety Committee Meeting Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
        <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
   <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function DocOpen(filename) {

            var filepath = "../../uploads/SCM/";
            //alert(filepath + filename);
            window.open(filepath + filename);
        }


        function showdetails(path) {
            window.open(path);
            return false;
        }

    </script>
    <style type="text/css">
        .page
        {
            width: 88%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <div style="font-family: Tahoma; font-size: 11px; width: 95%; height: 100%; border: 0px solid Gray;
            margin-top: 1px">
            <div class="page-title">
                            Safety Committee Meeting Details / System Review Minutes Details  
            </div>
            <div style="border: 1px solid gray;">
                <table cellpadding="1" cellspacing="2" width="100%" style="color: Black;">
                    <tr>
                        <td style="width: 4%" align="right">
                            Vessel : &nbsp; &nbsp;
                        </td>
                        <td style="width: 11%" align="left">
                            <asp:TextBox ID="txtVessel" ReadOnly="true" CssClass="txtReadOnly" Font-Size="11px"
                                Width="120px" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 10%; vertical-align: top" align="right" rowspan="5">
                            <table>
                                <tr>
                                    <td style="vertical-align: top">
                                        <asp:Label ID="lblComment" runat="server" Text="Comment : &nbsp; &nbsp;"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Height="100" Width="300PX"></asp:TextBox>
                                    </td>
                                    <td style="vertical-align: top">
                                        <asp:Button ID="btnVerify" Text="Verify Report" runat="server" OnClick="btnVerify_Click"
                                            Style="margin-left: 5px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Meeting Date : &nbsp; &nbsp;
                        </td>
                        <td align="left" style="width: 7%">
                            <asp:TextBox ID="txtMeetingDate" runat="server" EnableViewState="true" Font-Size="11px"
                                CssClass="txtReadOnly" ReadOnly="true" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Vessel Position : &nbsp; &nbsp;
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtVesselPosition" ReadOnly="true" runat="server" Height="30px"
                                CssClass="txtReadOnly" Font-Size="12px" Font-Names="Tahoma" Width="300" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblVerifiedByH" runat="server" Text="Verified By : &nbsp; &nbsp;"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="lblVerifiedBy" ReadOnly="true" CssClass="txtReadOnly" Font-Size="11px"
                                Width="120px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblVerificationDateH" runat="server" Style="text-align: right" Text="Verification Date : &nbsp; &nbsp;"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="lblVerificationDate" ReadOnly="true" CssClass="txtReadOnly" Font-Size="11px"
                                Width="120px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="border: 1px solid gray; margin-top: 1px; height: 800px">
                <br />
                <cc1:TabContainer ID="TabSCM" runat="server" ActiveTabIndex="0" OnActiveTabChanged="TabSCM_ActiveTabChanged" style="text-align:left"
                    AutoPostBack="true">
                    <cc1:TabPanel runat="server" HeaderText="&nbsp; Attendee &nbsp;" ID="TabPanel1" TabIndex="7">
                                <ContentTemplate>
                                    
                                        <table Width="100%">
                                            <tr>
                                                <td align="right">
                                                    <%--<asp:ImageButton ID="btnExport" runat="server" Height="22px" ImageUrl="~/Images/XLS.jpg"
                                                     OnClick="btnExport_Click" ToolTip="Export to Excel" />
                                                      <img src="../../Images/Printer.png" title="*Print*" style="cursor: pointer;" alt="Print" onclick="PrintDiv('grid-container')" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                <div id="grid-container" style="border: 0px solid Gray; margin-top: 0px">
                                                <asp:GridView ID="gvAbsn" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                                    Width="100%" GridLines="Both" AllowSorting="true" AllowPaging="false" PageSize="25"
                                                    PagerStyle-BackColor="#CCCCCC">
                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                    <RowStyle Font-Size="12px" CssClass="HeaderStyle-css" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                    <Columns>
                                                        <asp:BoundField DataField="STAFF_CODE" HeaderText="Staff Code" HeaderStyle-HorizontalAlign="left"
                                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="120px" ItemStyle-Wrap="true"
                                                            ItemStyle-CssClass="PMSGridItemStyle-css" Visible="false" />
                                                             <asp:TemplateField HeaderText="Staff Code">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hlProgramReport" Text='<%#Eval("STAFF_CODE")%>' CssClass="staffInfo"
                                                                    runat="server" NavigateUrl='<%#   "~/Crew/CrewDetails.aspx?ID="+Eval("CrewId").ToString() %>'
                                                                    Target="_blank"></asp:HyperLink>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="PMSGridItemStyle-css" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Staff Name">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hlProgramReport" Text='<%# Eval("Rank_Short_Name").ToString()+" - "+Eval("Staff_Name").ToString() %>'
                                                                    runat="server"
                                                                    Target="_blank"></asp:HyperLink>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="PMSGridItemStyle-css" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="REASON" HeaderText="Reason" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="800px"
                                                            ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" ItemStyle-CssClass="PMSGridItemStyle-css" />
                                                        <asp:BoundField DataField="Present" HeaderText="Present" HeaderStyle-HorizontalAlign="left"
                                                            ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" ItemStyle-CssClass="PMSGridItemStyle-css" />
                                                    </Columns>
                                                </asp:GridView>
                                                </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                             
                    <cc1:TabPanel runat="server" HeaderText="&nbsp; NCRs Not Closed &nbsp;" ID="Cncd"
                        TabIndex="8">
                        <ContentTemplate>
                            <div style="border: 0px solid Gray; margin-top: 0px">
                                <asp:GridView ID="gvCncd" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    Width="100%" GridLines="Both" AllowSorting="true" AllowPaging="true" PageSize="25"
                                    PagerStyle-BackColor="#CCCCCC" OnPageIndexChanging="gvCncd_PageIndexChanging">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:BoundField DataField="JOB_CODE" HeaderText="Job Code" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="80px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <asp:BoundField DataField="REASON_NCR_NOT_DISPOSED" HeaderText="Reason NCR Not Closed"
                                            HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="&nbsp;Injuries Details, if any&nbsp;" ID="Injd"
                        TabIndex="0">
                        <ContentTemplate>
                            <div style="border: 0px solid Gray; margin-top: 0px">
                                <asp:GridView ID="gvInjd" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    Width="100%" GridLines="Both" AllowSorting="true" AllowPaging="true" PageSize="20"
                                    PagerStyle-BackColor="#CCCCCC">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:BoundField DataField="INJURY_DATE" HeaderText="Date" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="60px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="300px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <asp:BoundField DataField="FOLLOWUP_ACTION" HeaderText="Followup Action" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="300px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <div>
                                <asp:FormView ID="gvInjdLst" runat="server">
                                    <ItemTemplate>
                                        <table style="font-size: 11px" width="100%" border="1px" cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="center" style="background-color: #5588BB; color: #FFFFFF">
                                                    <b>Date of Last Injury </b>
                                                </td>
                                                <td align="center" style="background-color: #5588BB; color: #FFFFFF">
                                                    <b>Injury free days</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="background-color: #FFFFCC; color: Black">
                                                    Lost Work Case
                                                </td>
                                                <td align="center"> 
                                                    <%#Eval("DTOFLASTINJURYLWC")%>
                                                </td>
                                                <td align="center"> 
                                                    <%#Eval("INJURYFREEDAYSLWC")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="background-color: #FFFFCC; color: Black">
                                                    Restricted Work Case
                                                </td>
                                                <td align="center">
                                                    <%#Eval("DTOFLASTINJURYRWC")%>
                                                </td>
                                                <td align="center">
                                                    <%#Eval("INJURYFREEDAYSRWC")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="background-color: #FFFFCC; color: Black">
                                                    Medical Treatment Case
                                                </td>
                                                <td align="center">
                                                    <%#Eval("DTOFLASTINJURYMTC")%>
                                                </td>
                                                <td align="center">
                                                    <%#Eval("INJURYFREEDAYSMTC")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:FormView>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="&nbsp; Drills &nbsp;" ID="Emdrl" TabIndex="1">
                                <ContentTemplate>
                                    <div style="border: 0px solid Gray; margin-top: 0px">
                                        <asp:GridView ID="gvEmdrl" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                            Width="100%" GridLines="Both" AllowSorting="true" OnRowDataBound="gvEmdrl_RowDataBound"
                                            AllowPaging="true" PageSize="25" PagerStyle-BackColor="#CCCCCC" OnPageIndexChanging="gvEmdrl_PageIndexChanging">
                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                            <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                            <Columns>
                                                <asp:BoundField DataField="DRILL_DATE" HeaderText="Drill Date" HeaderStyle-HorizontalAlign="left"
                                                    ItemStyle-HorizontalAlign="left" ItemStyle-Width="100px" ItemStyle-Wrap="true"
                                                    ItemStyle-CssClass="PMSGridItemStyle-css" />
                                                <asp:BoundField DataField="Scheduled_Date" HeaderText="Scheduled Date" HeaderStyle-HorizontalAlign="left"
                                                    ItemStyle-HorizontalAlign="left" ItemStyle-Width="100px" ItemStyle-Wrap="true"
                                                    ItemStyle-CssClass="PMSGridItemStyle-css" />
                                                <%-- <asp:BoundField DataField="DRILLTYPE" HeaderText="Dril Type" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />--%>
                                                <asp:TemplateField HeaderText="Drill">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlProgramReport" Text='<%# Eval("DRILLTYPE") %>' runat="server"
                                                            Enabled='<%# (Eval("TRAINING_ID").ToString().Trim().Length==0 || Eval("PROGRAM_ID").ToString()=="0") ?false:true %>'
                                                            NavigateUrl='<%# Eval("TRAINING_ID").ToString().Trim().Length!=0?("~/LMS/LMS_Program_Details_Report.aspx?Program_ID="+Eval("PROGRAM_ID").ToString()+ "&SCHEDULE_ID="+Eval("TRAINING_ID").ToString() + "&Vessel_Short_Name="+Eval("Vessel_Short_Name").ToString()+"&Office_Id="+Eval("Office_Id").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()):"#" %>'
                                                            Target="_blank"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="PMSGridItemStyle-css" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="IMPROVEMENTSSUGGESTED" HeaderText="Improvement suggested"
                                                    HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true"
                                                    ItemStyle-CssClass="PMSGridItemStyle-css" />
                                                <asp:TemplateField HeaderText="Attachment" Visible="false">
                                                    <HeaderTemplate>
                                                        Attachment
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFilePathIfSingle" Visible="false" runat="server" Text='<%# Eval("FilePathIfSingle") %>'></asp:Label>
                                                        <asp:ImageButton ID="ImgSCMAtt" runat="server" Height="16px" Visible='<%# Eval("AttCount").ToString() =="0" ? false : false%>'
                                                            ForeColor="Black" ImageUrl="~/Images/attach-icon.png"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="false" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div>
                                        <table width="100%">
                                            <tr>
                                                <td style="background-color: Red; color: #FFFFFF" align="left">
                                                    Drills not carried out
                                                </td>
                                                <td style="background-color: #FFFFCC; color: black" align="left">
                                                    (To let SQA department know the reason for not carrying out the due drills)
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="border: 0px solid Gray; margin-top: 0px">
                                        <asp:GridView ID="gvEmdrlntdone" runat="server" EmptyDataText="NO RECORDS FOUND"
                                            AutoGenerateColumns="False" Width="100%" GridLines="Both" AllowSorting="true"
                                            AllowPaging="true" PageSize="25" PagerStyle-BackColor="#CCCCCC" OnPageIndexChanging="gvEmdrlntdone_PageIndexChanging">
                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                            <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                            <Columns>
                                                <asp:BoundField DataField="Scheduled_Date" HeaderText="Scheduled Date" HeaderStyle-HorizontalAlign="left"
                                                    ItemStyle-HorizontalAlign="left" ItemStyle-Width="100px" ItemStyle-Wrap="true"
                                                    ItemStyle-CssClass="PMSGridItemStyle-css" />
                                                <asp:BoundField DataField="DRILLTYPE" HeaderText="Drill Type" HeaderStyle-HorizontalAlign="left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px" ItemStyle-Wrap="true"
                                                    ItemStyle-CssClass="PMSGridItemStyle-css" />
                                                <asp:BoundField DataField="REASONFORNOTCARRYINGOUT" HeaderText="Reason for not carrying out drill"
                                                    HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true"
                                                    ItemStyle-CssClass="PMSGridItemStyle-css" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="&nbsp; Safety Video Screening &nbsp;" ID="Sfvs"
                        TabIndex="2">
                        <ContentTemplate>
                            <div style="border: 0px solid Gray; margin-top: 0px">
                                <asp:GridView ID="gvSfvs" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    Width="100%" GridLines="Both" AllowSorting="true" OnRowDataBound="gvSfvs_RowDataBound"
                                    AllowPaging="true" PageSize="30" PagerStyle-BackColor="#CCCCCC" OnPageIndexChanging="gvSfvs_PageIndexChanging">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:BoundField DataField="DISPLAYED_ON" HeaderText="Displayed On" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="80px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <asp:BoundField DataField="Scheduled_Date" HeaderText="Scheduled Date" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="80px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <%--   <asp:BoundField DataField="TITLE" HeaderText="Title" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" ItemStyle-CssClass="PMSGridItemStyle-css" />--%>
                                        <asp:TemplateField HeaderText="TITLE">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlProgramReport" Text='<%# Eval("TITLE") %>' runat="server" Enabled='<%# Eval("TRAINING_ID").ToString().Trim().Length==0?false:true %>'
                                                    NavigateUrl='<%# Eval("TRAINING_ID").ToString().Trim().Length!=0?("~/LMS/LMS_Program_Details_Report.aspx?Program_ID="+Eval("PROGRAM_ID").ToString()+ "&SCHEDULE_ID="+Eval("TRAINING_ID").ToString() + "&Vessel_Short_Name="+Eval("Vessel_Short_Name").ToString()+"&Office_Id="+Eval("Office_Id").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()):"#" %>'
                                                    Target="_blank"   ></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="PMSGridItemStyle-css" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ATTENDANCE_DECK" HeaderText="Deck" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <asp:BoundField DataField="ATTENDANCE_ENGINE" HeaderText="Engine" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <asp:BoundField DataField="ATTENDANCE_CATERING" HeaderText="Catering" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                      
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="&nbsp; Safe Working Practice Training &nbsp;"
                        ID="Swpt" TabIndex="3">
                        <ContentTemplate>
                            <div style="border: 0px solid Gray; margin-top: 0px">
                                <asp:GridView ID="gvSwpt" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    Width="100%" GridLines="Both" AllowSorting="true" OnRowDataBound="gvSwpt_RowDataBound"
                                    AllowPaging="true" PageSize="30" PagerStyle-BackColor="#CCCCCC" OnPageIndexChanging="gvSwpt_PageIndexChanging">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:BoundField DataField="TRAINING_DATE" HeaderText="Training Date" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="80px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <asp:BoundField DataField="Scheduled_Date" HeaderText="Scheduled Date" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="80px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <%--     <asp:BoundField DataField="TOPIC" HeaderText="Topic" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="350px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />--%>
                                        <asp:TemplateField HeaderText="TITLE">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlProgramReport" Text='<%# Eval("TOPIC") %>' runat="server" Enabled='<%# Eval("TRAINING_ID").ToString().Trim().Length==0?false:true %>'
                                                    NavigateUrl='<%# Eval("TRAINING_ID").ToString().Trim().Length!=0?("~/LMS/LMS_Program_Details_Report.aspx?Program_ID="+Eval("PROGRAM_ID").ToString()+ "&SCHEDULE_ID="+Eval("TRAINING_ID").ToString() + "&Vessel_Short_Name="+Eval("Vessel_Short_Name").ToString()+"&Office_Id="+Eval("Office_Id").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()):"#" %>'
                                                    Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                             <ItemStyle CssClass="PMSGridItemStyle-css" />
                                        </asp:TemplateField>
                                     <%--   <asp:BoundField DataField="TRAINERNAME" HeaderText="Trainer Name" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="120px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />--%>
                                        <asp:BoundField DataField="ATTENDANCE_DECK" HeaderText="Deck" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <asp:BoundField DataField="ATTENDANCE_ENGINE" HeaderText="Engine" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <asp:BoundField DataField="ATTENDANCE_CATERING" HeaderText="Catering" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="&nbsp; Other Training &nbsp;" ID="Ortg"
                        TabIndex="4">
                        <ContentTemplate>
                            <div style="border: 0px solid Gray; margin-top: 0px">
                                <asp:GridView ID="gvOrtg" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    Width="100%" GridLines="Both" AllowSorting="true" OnRowDataBound="gvOrtg_RowDataBound"
                                    AllowPaging="true" PageSize="30" PagerStyle-BackColor="#CCCCCC" OnPageIndexChanging="gvOrtg_PageIndexChanging">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:BoundField DataField="TRAINING_DATE" HeaderText="Training Date" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="80px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <asp:BoundField DataField="Scheduled_Date" HeaderText="Scheduled Date" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="80px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <%--                                        <asp:BoundField DataField="TOPIC" HeaderText="Topic" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="350px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />--%>
                                        <asp:TemplateField HeaderText="TITLE">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlProgramReport" Text='<%# Eval("TOPIC") %>' runat="server" Enabled='<%# Eval("TRAINING_ID").ToString().Trim().Length==0?false:true %>'
                                                    NavigateUrl='<%# Eval("TRAINING_ID").ToString().Trim().Length!=0?("~/LMS/LMS_Program_Details_Report.aspx?Program_ID="+Eval("PROGRAM_ID").ToString()+ "&SCHEDULE_ID="+Eval("TRAINING_ID").ToString() + "&Vessel_Short_Name="+Eval("Vessel_Short_Name").ToString()+"&Office_Id="+Eval("Office_Id").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()):"#" %>'
                                                    Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                             <ItemStyle CssClass="PMSGridItemStyle-css" />
                                        </asp:TemplateField>
                                   <%--     <asp:BoundField DataField="TRAINERNAME" HeaderText="Trainer Name" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Width="120px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />--%>
                                        <asp:BoundField DataField="ATTENDANCE_DECK" HeaderText="Deck" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <asp:BoundField DataField="ATTENDANCE_ENGINE" HeaderText="Engine" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <asp:BoundField DataField="ATTENDANCE_CATERING" HeaderText="Catering" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" ItemStyle-Wrap="true"
                                            ItemStyle-CssClass="PMSGridItemStyle-css" />
                                       
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="&nbsp;Meeting Minutes&nbsp;" ID="Metm" TabIndex="6">
                        <ContentTemplate>
                            <div style="border: 0px solid Gray; margin-top: 0px">
                                <asp:GridView ID="gvMetm" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    Width="100%" GridLines="Both" AllowSorting="true" AllowPaging="true" PageSize="25"
                                    PagerStyle-BackColor="#CCCCCC" OnPageIndexChanging="gvMetm_PageIndexChanging">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:BoundField DataField="MEETINGMINUTES" HeaderText="Meeting Minutes" HeaderStyle-HorizontalAlign="left"  HtmlEncode="false" 
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" ItemStyle-CssClass="PMSGridItemStyle-css" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                     <cc1:TabPanel runat="server" HeaderText="&nbsp;Attachments&nbsp;" ID="Attachments" TabIndex="9">
                        <ContentTemplate>
                            <div style="border: 0px solid Gray; margin-top: 0px">
                                <asp:GridView ID="gvAttachments"  runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    Width="100%" GridLines="Both" AllowSorting="true" AllowPaging="true" PageSize="25"
                                    PagerStyle-BackColor="#CCCCCC" OnPageIndexChanging="gvAttachments_PageIndexChanging">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:BoundField DataField="ATTACHMENT_NAME" HeaderText="File Name" HeaderStyle-HorizontalAlign="center"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" ItemStyle-CssClass="PMSGridItemStyle-css" />
                                        <asp:TemplateField HeaderText="Attachment">
                                            <HeaderTemplate>
                                                Attachment
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkAttachment" runat="server" ImageUrl="~/Images/Attach.png" NavigateUrl='<%# "~/Uploads/SCM/" + Eval("ATTACHMENT_PATH").ToString()%>'
                                                    Target="_blank" />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="false" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                  <%--  <cc1:TabPanel runat="server" HeaderText="&nbsp;Attachments&nbsp;" ID="" Height="100%"
                        TabIndex="9">
                        <ContentTemplate>
                             <div style="border: 0px solid Gray; margin-top: 0px">
                                
                             </div>
                        </ContentTemplate>
                    </cc1:TabPanel>--%>
                    <cc1:TabPanel runat="server" HeaderText="&nbsp;Environmental Training" ID="Environmental"
                        TabIndex="12">
                        <ContentTemplate>
                            <div style="border: 0px solid Gray; margin-top: 10px">
                                <asp:GridView ID="gvEnvironmental" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" Width="100%" GridLines="Both" AllowSorting="true"
                                    PagerStyle-BackColor="#CCCCCC" AllowPaging="true" PageSize="25">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:BoundField DataField="Question" HeaderText="Feedback Question" HeaderStyle-HorizontalAlign="center"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" ItemStyle-CssClass="PMSGridItemStyle-css"
                                            ItemStyle-Width="500px" />
                                        <asp:BoundField DataField="Answer" HeaderText="Answer" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" ItemStyle-CssClass="PMSGridItemStyle-css"
                                            ItemStyle-Width="50px" />
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <br />
                            </div>
                            <div style="border: 0px solid Gray; margin-top: 10px; text-align: left;">
                                &nbsp;&nbsp; <span><strong>Attachments</strong></span>
                                <asp:DataList ID="dlEnvironmentalAttachments" runat="server" RepeatDirection="Horizontal"  EmptyDataText="NO RECORDS FOUND"
                                    CellSpacing="15" RepeatColumns="3" RepeatLayout="Table">
                                  
                                    <ItemTemplate>
                                        <div>
                                            <span>
                                                <asp:HyperLink ID="lnkAttachment" runat="server" NavigateUrl='<%# "~/Uploads/SCM/" + Eval("Attached_File_Path").ToString()%>'
                                                    Target="_blank" Text='<%# Eval("ATTACHED_FILE_NAME")%>' /></span>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="&nbsp;Health Nutrition and Hygiene Training"
                        ID="HealthNutritionHygiene" TabIndex="13">
                        <ContentTemplate>
                            <div style="border: 0px solid Gray; margin-top: 10px">
                                <asp:GridView ID="gvHealthNutritionHygiene" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" Width="100%" GridLines="Both" AllowSorting="true"
                                    PagerStyle-BackColor="#CCCCCC" AllowPaging="true" PageSize="25">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle Font-Size="12px" CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:BoundField DataField="Question" HeaderText="Feedback Question" HeaderStyle-HorizontalAlign="center"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" ItemStyle-CssClass="PMSGridItemStyle-css"
                                            ItemStyle-Width="500px" />
                                        <asp:BoundField DataField="Answer" HeaderText="Answer" HeaderStyle-HorizontalAlign="left"
                                            ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true" ItemStyle-CssClass="PMSGridItemStyle-css"
                                            ItemStyle-Width="50px" />
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <br />
                            </div>
                            <div style="border: 0px solid Gray; margin-top: 10px; text-align: left;">
                                &nbsp;&nbsp;<span><strong>Attachments</strong></span>
                                <asp:DataList ID="dtHealthAttachments" runat="server" RepeatDirection="Horizontal"
                                    CellSpacing="15" RepeatColumns="3" RepeatLayout="Table">
                                    <ItemTemplate>
                                        <div>
                                            <span>
                                                <asp:HyperLink ID="lnkAttachment" runat="server" NavigateUrl='<%# "~/Uploads/SCM/" + Eval("Attached_File_Path").ToString()%>'
                                                    Target="_blank" Text='<%# Eval("ATTACHED_FILE_NAME")%>' /></span>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </div>
        </div>
    </center>
</asp:Content>
