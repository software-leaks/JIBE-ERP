<%@ Page Title="Port Call Notification" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" EnableEventValidation="false"
    CodeFile="Port_Call_Notification.aspx.cs" Inherits="VesselMovement_Port_Call_Notification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        //         function asyncBind_NotiFicationReport(NotificationID, NotificationName) {

        //             var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncBind_NotificationReport', false, { "NotificationID": NotificationID, "NotificationName": NotificationName }, onSucc_LoadFunction, Onfail, new Array('dvPortCallNotification', 'ltlNotification'));

        //             lastExecutorMinMaxQty_M = service.get_executor();

        //         }

        //                  function onSucc_LoadFunction(retval, prm) {
        //                      try {
        //                          document.getElementById(prm[0]).innerHTML = retval;

        //                          checkForMyAction(prm[1], retval);
        //                      }
        //                      catch (ex)
        //                     { }
        //                  }



        function OpenScreen(ID) {
            var url = 'PortCallNotification_Entry.aspx?ID=' + ID;
            OpenPopupWindowBtnID('PortCallNotification', 'Vessel Movement', url, 'popup', 650, 1000, null, null, false, false, true, null, 'ctl00_MainContent_btnRefresh');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div class="page-title">
                Port Call Notification
            </div>
            <div style="width: 100%; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 15%">
                                        <asp:Button ID="btnBackToVessel" Text="Back To Vessel" Visible="false" runat="server"
                                            OnClick="btnBackToVessel_Click" />
                                    </td>
                                    <td align="left" style="width: 30%">
                                        &nbsp;
                                    </td>
                                    <td align="center" style="width: 5%">
                                        &nbsp;
                                    </td>
                                    <td align="right" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" ToolTip="Refresh" ImageUrl="~/Images/Refresh-icon.png"
                                            OnClick="btnRefresh_Click" />&nbsp;
                                    </td>
                                    <td align="left" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" Visible="false" ToolTip="Add New Notification"
                                            OnClientClick='OpenScreen(0); return false;' ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnRefresh" />
                    </Triggers>
                </asp:UpdatePanel>
                <div style="margin-left: auto; margin-right: auto; text-align: center;">
                    <div>
                        <asp:GridView ID="gvNotification" runat="server" EmptyDataText="NO RECORDS FOUND"
                            AutoGenerateColumns="False" OnRowDataBound="gvNotification_RowDataBound" DataKeyNames="Notification_ID"
                            CellPadding="0" CellSpacing="2" Width="100%" GridLines="both" OnSorting="gvNotification_Sorting"
                            AllowSorting="true" CssClass="gridmain-css" OnRowCreated="gvNotification_RowCreated">
                            <HeaderStyle CssClass="HeaderStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID">
                                    <HeaderTemplate>
                                        Seq. No
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Button ID="lblID" runat="server" Text='<%#Eval("ROWNUM")%>' width="40px"
                                            OnCommand="onView" CommandArgument='<%#Eval("[Notification_ID]")%>' Style="color: Black">
                                        </asp:Button>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="5%" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notification Name">
                                    <HeaderTemplate>
                                        Notification Name
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Notification_Name")%>' Style="color: Black"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="20%" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField >
                                    <HeaderTemplate >
                                        <table class="HeaderStyle-css" width="100%" border="0">
                                        <tr>
                                        <td align="center" colspan="3">Arrival Period</td>
                                        </tr>
                                        <tr>
                                        <td  colspan="3" height="2px">
                                        <hr />
                                        </td>
                                        </tr>
                                               <tr>
                                                <td align="right" width="45%">
                                                    FROM 
                                                </td>
                                                <td  width="10%" align="center">
                                                 |
                                                </td>
                                                <td align="left"  width="45%">
                                                    TO
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFromDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Start_Date")))%>'></asp:Label>
                                                   
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;|&nbsp;&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblToDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("End_Date")))%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="25%" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                
              <%--                  <asp:TemplateField HeaderText="From">
                                    <HeaderTemplate>
                                        From
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                           <asp:Label ID="lblFromDate1" runat="server" Text='<%# Eval("Start_Date","{0:dd MMM yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center"   />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="To">
                                    <HeaderTemplate>
                                        To
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                           <asp:Label ID="lblToDate1" runat="server" Text='<%# Eval("End_Date","{0:dd MMM yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center"  />
                                </asp:TemplateField>--%>



                                <asp:TemplateField HeaderText="Status">
                                    <HeaderTemplate>
                                        Status
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Notification_Status")%>'
                                            Style="color: Black"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="10%" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notify User">
                                    <HeaderTemplate>
                                        Notify User
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserList" runat="server" Text='<%# Bind("UserList") %>' />
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30%" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <HeaderTemplate>
                                        Action
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table cellpadding="2" cellspacing="2">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="lblIDI" runat="server" Text='<%#Eval("ROWNUM")%>' ToolTip="View Details"
                                                        Height="16px" OnCommand="onView" ImageUrl="~/Images/view13.png" CommandArgument='<%#Eval("[Notification_ID]")%>'
                                                        Style="color: Black"></asp:ImageButton>
                                                    <asp:ImageButton ID="ImgEdit" runat="server" Text="Edit" Visible='<%# uaEditFlag %>'
                                                        OnClientClick='<%#"OpenScreen((&#39;" + Eval("[Notification_ID]") +"&#39;));return false;"%>'
                                                        ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px">
                                                    </asp:ImageButton>
                                                    <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                        Visible='<%# uaDeleteFlag %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                        CommandArgument='<%#Eval("[Notification_ID]")%>' ForeColor="Black" ToolTip="Delete"
                                                        ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem=" BindNotificationList" />
                        <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                    </div>
                    <div>
                        &nbsp;</div>
                    <br />
                </div>
            </div>
            <div>
                <table title="Port Call Notification" width="100%">
                    <tr>
                        <td>
                            <div style="border: 1px solid #cccccc;">
                                <asp:GridView ID="gvNotificationReport" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AllowPaging="true" Visible="false" AutoGenerateColumns="False" CellPadding="0"
                                    CellSpacing="2" Width="100%" GridLines="both" PageSize="10" AllowSorting="true"
                                    CssClass="gridmain-css" OnPageIndexChanging="gvNotificationReport_PageIndexChanging">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                    <Columns>
                                        <asp:BoundField DataField="Vessel_Name" HeaderText="Vessel Name" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PORT_NAME" HeaderText="Port Name" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Arrival" HeaderText="Arrival" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Departure" HeaderText="Departure" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Country_Name" HeaderText="Country" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </center>
</asp:Content>
