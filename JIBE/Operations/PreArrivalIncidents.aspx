 
 


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreArrivalIncidents.aspx.cs"
    Inherits="Operations_PreArrivalIncidents" Title="Pre Arrival Incidents" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script type="text/javascript">



        function DocOpen(docpath) {
            window.open(docpath);
        }

        function previewDocument(docPath) {
            document.getElementById("ifrmDocPreview").src = docPath;
        }

        function getImageopen(str) {
            window.open(str, "file", "menubar=0,resizable=0,width=750,height=550,resizeable=yes");
        }




        function fn_OnClose(sender, arg) {

            $('[id$=btnLoadFiles]').trigger('click');
            //__doPostBack('ctl00_MainContent_btnLoadFiles', true);            
        }


    </script>
    <style type="text/css">
        .tdh
        {
            text-align: right;
            padding: 1px 3px 1px 3px;
            width: 250px;
        }
        .tdd
        {
            text-align: left;
            padding: 1px 3px 1px 3px;
            width: 150px;
        }
    </style>
    <title></title>
</head>
<body style="background-color: White; font-size: 11px; font-family: Tahoma;">
    <form id="form2" runat="server">
    <div>
        <asp:ScriptManager ID="scpMgr" runat="server">
        </asp:ScriptManager>
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
        <div style="text-align: left; padding: 10px;white-space:nowrap">
            <asp:Label ID="lblInspectionDetailId" runat="server" Style="font-size: 12px; font-weight: bold;
                display: none; color: Navy" />
            <table>
                <tr>
                    <td style="font-size: 12px; font-weight: bold;">
                        Risk Type:-
                    </td>
                    <td>
                        <asp:Label ID="lblRiskType" runat="server" Style="font-size: 12px; font-weight: bold;
                            color: Navy" />
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td style="font-size: 12px; font-weight: bold;">
                        Risk Name:-
                    </td>
                    <td>
                        <asp:Label ID="lblRiskName" runat="server" Style="font-size: 12px; font-weight: bold;
                            color: Navy" />
                    </td>
            </table>
        </div>
        <asp:UpdatePanel ID="updGridAttach" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div style="padding-top: 2px; padding-bottom: 2px; width: 100%">
                  
                    <table cellpadding="2" cellspacing="0" style="width: 100%; border: 1px solid #cccccc;
                        margin-top: 2px; height: 400px; vertical-align: top">
                        <tr>
                            <td style="width: 60%; text-align: left; vertical-align: top;">
                                <asp:GridView ID="gvInspectionAttachment" runat="server" AutoGenerateColumns="False"
                                    EmptyDataText="No attachment found." CellPadding="2" Width="100%" GridLines="None"
                                    CssClass="GridView-css" Style="padding-left: 5px">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    

                                     <Columns>
                                        <asp:TemplateField HeaderText="Incident name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                &nbsp;&nbsp;&nbsp;<a href='../Technical/worklist/ViewJob.aspx?OFFID=<%#Eval("WL_OFFICE_ID") %>&WLID=<%#Eval("Worklist_ID") %>&VID=<%#Eval("VESSEL_ID") %>'
														target="_blank" style="cursor: hand; color: Blue; text-decoration: none;"><asp:Label ID="lblIncidentName" runat="server" Text='<%# Eval("IncidentName").ToString() %>'></asp:Label></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>        
                                        
                                         <asp:TemplateField HeaderText="Incident Date" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                            <asp:Label ID="lblIncidentDate" runat="server" Text='<%# Convert.ToDateTime(Eval("IncidentDate")).ToString("dd/MMM/yyyy") %>'></asp:Label> 
                                            </ItemTemplate>
                                        </asp:TemplateField>                                     
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #FF3300; font-size: small;"
                    Width="624px" Height="16px"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
