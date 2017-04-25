<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" EnableEventValidation="false"  CodeFile="Daily_Message_Queue.aspx.cs" Inherits="ODM_Daily_Message_Queue" %>

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
              var url = 'ODM_Entry.aspx?ID=' + ID;
              OpenPopupWindowBtnID('ODM', 'ODM Entry', url, 'popup', 650, 1000, null, null, false, false, true, null, 'ctl00_MainContent_btnRefresh');
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width:80%;
            height: 100%;">
          <div class="page-title">
            Office Daily Message
          </div>
            <div style="height: 100%; width: 100%; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">

                    <ContentTemplate>
                        <div style=" height: 10%; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 15%">
                                    <asp:Button ID="btnHistory" Text="History" runat = "server" 
                                            onclick="btnHistory_Click" />
                                    </td>
                                    <td align="left" style="width: 30%">

                                        <asp:Button ID="btnAttachments" Text="Attachments" runat = "server" 
                                            onclick="btnAttachments_Click" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" onclick="btnRefresh_Click" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Message" OnClientClick='OpenScreen(0); return false;'
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        </ContentTemplate>
                         <Triggers>
                        <asp:PostBackTrigger ControlID="btnRefresh" />
                    </Triggers>
                </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdateGV"  runat = "server">
                        <ContentTemplate>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvODM" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" OnRowDataBound="gvODM_RowDataBound" DataKeyNames="GroupId"
                                    CellPadding="0" CellSpacing="2" Width="100%" GridLines="both" OnSorting="gvODM_Sorting"
                                    AllowSorting="true" CssClass="gridmain-css">
                                       <HeaderStyle CssClass="HeaderStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                    <Columns>
                                         <asp:TemplateField HeaderText="From">
                                            <HeaderTemplate>FROM
                                              
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("DEPARTMENT")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Subject">
                                            <HeaderTemplate>
                                            SUBJECT
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                  <asp:Label ID="lblToDate" runat="server" Text='<%# Eval("ODM_SUBJECT")%>'></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="25%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Attachment">
                                            <HeaderTemplate>
                                                <asp:Image ID="ImgAttach" runat="server" ImageUrl="~/Images/Attachment.png" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                               <asp:Label ID="lblAttachment" runat="server" Text='<%# Eval("NoOfAttachments")%>'  />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Send To">
                                            <HeaderTemplate>Send To
                                             
                                         
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserList" runat="server" Text='<%# Eval("VesselList")%>'  />
                                                
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30%" CssClass="PMSGridItemStyle-css">
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
                                                               <asp:ImageButton ID="ImgEdit" runat="server" 
                                                                OnClientClick='<%#"OpenScreen((&#39;" + Eval("[GroupID]") +"&#39;));return false;"%>'
                                                                 ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/asl_view.png"
                                                                Height="16px"></asp:ImageButton>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem=" BindODMQueue" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>


