<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMSJob_Crew_Involved.aspx.cs"
    Inherits="PMSJob_Crew_Involved" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
       <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>    
    <script src="../../Scripts/StaffInfo.js" type="text/javascript"></script>
<%--<script src="../../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
   <%-- <script src="../../Scripts/uploadify/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
    <script src="../../Scripts/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
    <script src="../../Scripts/uploadify/swfobject.js" type="text/javascript"></script>
    <link href="../../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />--%>
    <script language="javascript" type="text/javascript">

        function AddNewMaintenanceFeedback(crewid, vid, jid,jhistoryid, wlid, offid, voygeid) {

            $('#dvPopupFrame').attr("Title", "Add New Maintenance Feedback");
            $('#dvPopupFrame').css({ "width": "500px", "height": "400px", "text-allign": "center" });
            $('#frPopupFrame').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });

            var URL = "../../Crew/CrewDetails_MaintenanceFeedback.aspx?CrewID=" + crewid + "&VID=" + vid + "&WLID=" + wlid + "&OFFID=" + offid + "&JID=" + jid + "&JHID=" + jhistoryid + "&voygeid=" + voygeid + "&Mode=ADD&rnd=" + Math.random();
            //var URL = "../../Technical/PMS/Test_Maintaince.aspx?CrewID=" + crewid + "&VID=" + vid + "&WLID=" + wlid + "&OFFID=" + offid + "&JID=" + jid + "&JHID=" + jhistoryid + "&voygeid=" + voygeid + "&Mode=ADD&rnd=" + Math.random();

            document.getElementById("frPopupFrame").src = URL;
            showModal('dvPopupFrame', true);

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div id="dvWorkListCrewInvolved" style="font-family: Tahoma; font-size: 12px;">
            <div>
                <table width="100%" cellpadding="1" cellspacing="1">
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td align="right">
                            Search :&nbsp
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCrewInvolveSearch" CssClass="txtInput" runat="server"></asp:TextBox>
                        </td>
                        <td align="right">
                            Rank :&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlCrewInvloveRank" CssClass="txtInput" runat="server" Width="220px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearchCrewInvolve" runat="server" Text="Search" OnClick="btnSearchCrewInvolve_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <div style="max-height: 500px; min-height: 500px; overflow-x: hidden; overflow-y: scroll">
                                <asp:GridView ID="grdCrewListToAdd" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    CellSpacing="1" EmptyDataText="No Record Found" GridLines="both" Width="100%"
                                    AllowSorting="false">
                                    <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                    <RowStyle Font-Size="11px" CssClass="PMSGridRowStyle-css" />
                                    <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Staff Code" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblStaff_NAME" runat="server" NavigateUrl='<%# "../../Crew/CrewDetails.aspx?ID=" + Eval("CrewID")%>' CssClass="staffInfo pin-it"
                                                    Target="_blank" Text='<%# Eval("Staff_Code")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Staff Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStaff_FullName" runat="server" Text='<%#Eval("Staff_FullName").ToString()%>'></asp:Label>
                                                <asp:Label ID="lblVOYAGE_ID" Visible="false" runat="server" Text='<%#Eval("VOYAGE_ID").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rank">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRank" runat="server" Text='<%#Eval("Rank_Short_Name").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCrewStatus" runat="server" Text='<%#Eval("CrewStatus").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Feed back">
                                            <HeaderTemplate>
                                                Feed back
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Button ID="btnFeedback" Text="FeedBack" runat="server" OnClientClick='<%# "AddNewMaintenanceFeedback(0,"+ Eval("VESSEL_ID").ToString() +","+ JOB_ID +","+ JOB_HISTORY_ID +",0,0,"+ Eval("VOYAGE_ID").ToString() + ");return false;" %>' />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                  
                </table>
            </div>
            
        </div>
    </center>
    <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute; text-align:center; Font-family: Tahoma; font-size: 12px;
        left: 0.5%; top: 15%; width: 900px; z-index: 1; color: black" title=''>
        <div class="content">
            <iframe id="frPopupFrame" src="" frameborder="0" height="100%" width="100%"></iframe>
        </div>
    </div>
    
    </form>
</body>
</html>
