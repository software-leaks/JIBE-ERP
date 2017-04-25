<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Port_Call_CrewOnOff.aspx.cs"
    Inherits="VesselMovement_Port_Call_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <a target="_parent" />
    <title>Port Call Report</title>
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
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/StaffInfo.js" type="text/javascript"></script>

</head>
<body >
 <script language="javascript" type="text/javascript">

     function refreshAndClose() {
         window.parent.ReloadParent_ByButtonID();
         window.close();
     }
     function validation() 
        {


                if (document.getElementById("DDLPort").value == "0")
                 {
                    alert("Please Select port.");
                    document.getElementById("DDLPort").focus();
                    return false;
                }
     }
    </script>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Script1" runat="server">
    </asp:ScriptManager>
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; 
        color: Black; height: 100%;">
               <center>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td align="center">
                        <div id="page-title" class="page-title">
                           Sign On/Off Event Details
                        </div>
                    </td>
                </tr>
               
                <tr>
                    <td  align="center">
                        <table width="100%">
                            <tr>
                               
                                    
                                            <td style="width:50%; vertical-align:top">
                                                            <div style="background: #cccccc;">
                                                              <center>
                                                        <asp:Label ID="lblOn" runat="server" Text="Sign On Open Event" ></asp:Label>
                                                          </center>
                                                        </div>
                                                        <asp:GridView ID="gvCrewOn" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                                                DataKeyNames="ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                                                AllowSorting="true">
                                                                <HeaderStyle CssClass="HeaderStyle-css" />
                                                                <RowStyle CssClass="RowStyle-css" />
                                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="No.">
                                                                        <HeaderTemplate>
                                                                            No.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblONNo" runat="server" Text='<%#Eval("No")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="10%" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <HeaderTemplate>
                                                                            Date
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOnDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Event_Date")))%>'></asp:Label>
                                                                            
                                                                        </ItemTemplate>
                                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Port Name">
                                                                        <HeaderTemplate>
                                                                            Port Name
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOnPort" runat="server" Text='<%#Eval("PORT_NAME")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SCode">
                                                                        <HeaderTemplate>
                                                                            SCode
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                              
                                                                                <a href='../Crew/CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                                                                    <%# Eval("staff_Code")%></a>
                                                                          
                                                                          </ItemTemplate>
                                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Rank">
                                                                        <HeaderTemplate>
                                                                            Rank
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOnRank" runat="server" Text='<%#Eval("Rank_Name")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Crew">
                                                                        <HeaderTemplate>
                                                                            Crew
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOnCrew" runat="server" Text='<%#Eval("Staff_Name")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                           
                                                        </td>
                                                         <td style="width:50%;  vertical-align:top">
                                <div style="background: #cccccc;"  >
                                <center>
                                                        <asp:Label ID="lblOff" runat="server" Text="Sign Off Open Event" ></asp:Label>
                                                        </center>
                                                        </div>
   
                                                         <asp:GridView ID="gvCrewOff" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                                                DataKeyNames="ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                                                AllowSorting="true">
                                                                <HeaderStyle CssClass="HeaderStyle-css" />
                                                                <RowStyle CssClass="RowStyle-css" />
                                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="No.">
                                                                        <HeaderTemplate>
                                                                            No.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOff" runat="server" Text='<%#Eval("No")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="10%" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <HeaderTemplate>
                                                                            Date
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Event_Date")))%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Port Name">
                                                                        <HeaderTemplate>
                                                                            Port Name
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPort" runat="server" Text='<%#Eval("PORT_NAME")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SCode">
                                                                        <HeaderTemplate>
                                                                            SCode
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                           
                                                                                <a href='../Crew/CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                                                                    <%# Eval("staff_Code")%></a>
                                                                            
                                                                        </ItemTemplate>
                                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Rank">
                                                                        <HeaderTemplate>
                                                                            Rank
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRank" runat="server" Text='<%#Eval("Rank_Name")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Crew">
                                                                        <HeaderTemplate>
                                                                            Crew
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCrew" runat="server" Text='<%#Eval("Staff_Name")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                           
                                                        </td>
                                                        
                                                        </tr>
                                                        
                                                </table>
                                            </td>
                                        </tr>
                                   
               
            </table>
        </center>
    </div>
    </form>
</body>
</html>
