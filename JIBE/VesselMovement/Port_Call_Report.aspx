<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Port_Call_Report.aspx.cs"
    Inherits="VesselMovement_Port_Call_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <a target="_parent" /><title>Port Call Report</title>
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
    <style type="text/css">
        .style1
        {
            width: 8%;
            height: 22px;
        }
        .style2
        {
            width: 10%;
            height: 22px;
        }
    </style>
</head>
<body>
    <script language="javascript" type="text/javascript">

        function refreshAndClose() {
            window.parent.ReloadParent_ByButtonID();
            window.close();
        }
        function validation() {


            if (document.getElementById("DDLPort").value == "0") {
                alert("Please Select port.");
                document.getElementById("DDLPort").focus();
                return false;
            }
        }

        function ValidateText() {
            var strDateFormat = "<%= DateFormat %>";
            if (IsInvalidDate($("#txtActivateDate").val(), strDateFormat)) {
                alert("Please Enter Valid Activity Date.");
                document.getElementById("txtActivateDate").focus();
                return false;
            }

        }

        function ValidateText1() {
            var strDateFormat = "<%= DateFormat %>";

            if (IsInvalidDate($("#txtCheckDate").val(), strDateFormat)) {
                alert("Please Enter Valid  Check Date.");
                document.getElementById("txtCheckDate").focus();
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
                    <td colspan="3" align="center">
                        <div id="page-title" class="page-title">
                            Port Call Details
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblPortCode" runat="server" ForeColor="Blue" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                    <asp:Label ID="Label1" runat="server" Text=":"></asp:Label>&nbsp;
                                </td>
                                <td style="text-align: Right;">
                                    <asp:Label ID="lblVesselname" runat="server" ForeColor="Blue" Font-Bold="true" Text=""></asp:Label>&nbsp;
                                    <asp:Label ID="Label2" runat="server" Text=":"></asp:Label>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblPortName" runat="server" Font-Bold="true" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Visible="false" Text="Link To Port Library :"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLPort" runat="server" Visible="false" Width="200px" CssClass="txtInput">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnLinkPort" Visible="false" runat="server" Text="Link to Port" OnClientClick="return validation();"
                                        OnClick="btnLinkPort_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td valign="top" align="left" width="55%">
                                    <table style="text-align: center; border: 1px solid #cccccc; font-family: Tahoma;
                                        font-size: 12px;">
                                        <tr>
                                            <td align="right" style="width: 30%">
                                                <asp:Label ID="lblPortCallStatus" Text="Port Call Status :" Visible="false" runat="server"></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:CheckBox ID="chkomit" Visible="false" runat="server" Enabled="false" Text="Omitted" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 30%">
                                                Port Remarks:
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="400px" CssClass="txtInput"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 30%">
                                                Charterer:
                                            </td>
                                            <td align="left" colspan="3" style="width: 10%">
                                                <asp:DropDownList ID="ddlCharter" runat="server" Width="200px" CssClass="txtInput">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblCharterer" runat="server" Visible="false" BackColor="#DCDCDC"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 15%">
                                                Charterer's Agent :
                                            </td>
                                            <td align="left" style="width: 5%">
                                                <asp:DropDownList ID="ddlCharterAgent" runat="server" Width="200px" CssClass="txtInput">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblCharterAgent" runat="server" Visible="false" BackColor="#DCDCDC"></asp:Label>
                                            </td>
                                            <td align="left" colspan="2" style="width: 10%">
                                                <asp:Button ID="btnCharter" runat="server" Text="Add" Width="80px" OnClick="btnCharter_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="btnCSupplier" runat="server" Text="Manage Contacts" Width="110px"
                                                    OnClick="btnCSupplier_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 15%">
                                                Address :
                                            </td>
                                            <td align="left" colspan="3" style="8background-color: #DCDCDC">
                                                <asp:Label ID="txtAddress" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 15%">
                                                Country :
                                            </td>
                                            <td align="left" colspan="1" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtCountry" runat="server" Width="200px"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 10%">
                                                City :
                                            </td>
                                            <td align="left" style="width: 8%; background-color: #DCDCDC">
                                                <asp:Label ID="txtCity" runat="server" Width="200px" Enabled="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 15%">
                                                Phone :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtPhone" runat="server" Width="200px" Enabled="false"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 10%">
                                                Fax :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtFax" runat="server" Width="200px" Enabled="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 15%">
                                                Company Email :
                                            </td>
                                            <td align="left" colspan="3" style="background-color: #DCDCDC">
                                                <asp:Label ID="txtEmail" runat="server" Enabled="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="4" style="width: 10%">
                                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 15%">
                                                PIC :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtPICName" runat="server" Width="200px" Enabled="false"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 8%">
                                                2nd PIC :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtPICName2" runat="server" Width="200px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 15%">
                                                Email :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtPICEmail" runat="server" Width="200px" Enabled="false"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 8%">
                                                Email2 :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtPICEmail2" runat="server" Enabled="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 10%">
                                                Phone :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtPICPhone" runat="server" Width="200px" Enabled="false"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 8%">
                                                Phone2 :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtPICPhone2" runat="server" Width="200px" Enabled="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 8%">
                                                Owner's Agent :
                                            </td>
                                            <td align="left" style="width: 5%">
                                                <asp:DropDownList ID="ddlOwnerAgent" runat="server" Width="200px" CssClass="txtInput">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblOwnerAgent" runat="server" Visible="false" BackColor="#DCDCDC"></asp:Label>
                                            </td>
                                            <td align="left" colspan="2" style="width: 59%">
                                                <asp:Button ID="btnOwner" runat="server" Text="Add" Width="80px" OnClick="btnOwner_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="btnOSupplier" runat="server" Text="Manage Contacts" Width="110px"
                                                    OnClick="btnOSupplier_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 8%">
                                                Address :
                                            </td>
                                            <td align="left" colspan="3" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtOAddress" runat="server" TextMode="MultiLine" Width="400px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style1">
                                                Country :
                                            </td>
                                            <td align="left" colspan="1" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtOCountry" runat="server" Width="200px"></asp:Label>
                                            </td>
                                            <td align="right">
                                                City :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtOCity" runat="server" Width="200px" Enabled="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 8%">
                                                Phone :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtOPhone" runat="server" Width="200px"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 10%">
                                                Fax :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtOFax" runat="server" Width="200px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 8%">
                                                Company Email :
                                            </td>
                                            <td align="left" colspan="3" style="background-color: #DCDCDC">
                                                <asp:Label ID="txtOEmail" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="4" style="width: 10%; background-color: #DCDCDC">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 8%">
                                                PIC :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtOPICName" runat="server" Width="200px"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 8%">
                                                &nbsp;2nd PIC :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtOPICName2" runat="server" Width="200px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 8%">
                                                &nbsp;Email :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtOPICEmail" runat="server" Width="200px"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 59%">
                                                Email :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtOPICEmail2" runat="server" Width="200px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 8%">
                                                Phone :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtOPICPhone" runat="server" Width="200px"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 59%">
                                                Phone :
                                            </td>
                                            <td align="left" style="width: 10%; background-color: #DCDCDC">
                                                <asp:Label ID="txtOPICPhone2" runat="server" Width="200px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <div style="background: #cccccc; text-align: left; padding: 3px; font-weight: 600">
                                                    <asp:Label ID="lbl1" runat="server" Text=" Crew Change Events during Port Stay for vessel."
                                                        Visible="false"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table width="98%">
                                                    <tr>
                                                        <td>
                                                            <div style="background: #cccccc; padding: 3px">
                                                                <asp:Label ID="lblOn" runat="server" Text="Sign On Open Event" Visible="false"></asp:Label>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
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
                                                                            <itemtemplate>
                                                                                <a href='../Crew/CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                                                                    <%# Eval("staff_Code")%></a>
                                                                            </itemtemplate>
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
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="background: #cccccc; padding: 3px">
                                                                <asp:Label ID="lblOff" runat="server" Text="Sign Off Open Event." Visible="false"></asp:Label>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
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
                                                                            <itemtemplate>
                                                                                <a href='../Crew/CrewDetails.aspx?ID=<%# Eval("ID")%>' target="_blank" class="staffInfo">
                                                                                    <%# Eval("staff_Code")%></a>
                                                                            </itemtemplate>
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
                                </td>
                                <td valign="top" align="left" width="45%">
                                    <table style="text-align: center; border: 1px solid #cccccc; font-family: Tahoma;
                                        font-size: 11px;">
                                        <tr>
                                            <td colspan="4">
                                                <asp:GridView ID="gvDARecord" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    OnRowDataBound="gvDARecord_RowDataBound" GridLines="Both">
                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="false" Font-Size="12px" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="DA Code" DataField="Agent_Code" ItemStyle-Width="15%"
                                                            HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField HeaderText="Currency" DataField="DA_Currency" ItemStyle-Width="15%"
                                                            HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField HeaderText="Agent" DataField="Agent_Name" ItemStyle-Width="50%" HeaderStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField HeaderText="Status" DataField="DA_Status" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr id="rwAct" runat="server">
                                            <td colspan="4">
                                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                                    Additional Jobs orders not included into proforma.</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 40%">
                                                <asp:Literal ID="ltactivity" Text="  Activity Date :" runat="server"></asp:Literal>
                                            </td>
                                            <td align="left" style="width: 10%">
                                                <asp:TextBox ID="txtActivateDate" runat="server" Width="120px" BackColor="#FFFFCC"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtActivateDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td align="right" style="width: 25%">
                                                <asp:Literal ID="ltCost" Text=" Cost :" runat="server"></asp:Literal>
                                            </td>
                                            <td align="left" colspan="2" style="width: 10%">
                                                <asp:TextBox ID="txtCost" runat="server" Width="120px" BackColor="#FFFFCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 25%">
                                                <asp:Literal ID="ltDes" Text=" Description :" runat="server"></asp:Literal>
                                            </td>
                                            <td align="left" colspan="2" style="width: 10%">
                                                <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="300px" BackColor="#FFFFCC"></asp:TextBox>
                                            </td>
                                            <td align="left" style="width: 10%">
                                                <asp:Button ID="btnActivity" runat="server" Visible="false" Text="Add Activity" OnClick="btnActivity_Click"  OnClientClick="return ValidateText();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="5">
                                                <asp:GridView ID="gvActivity" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                                    DataKeyNames="ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                                    AllowSorting="true">
                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Supplier_Name">
                                                            <HeaderTemplate>
                                                                Requestor
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequestor" runat="server" Text='<%#Eval("Created_By")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Propose By">
                                                            <HeaderTemplate>
                                                                Activity Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDateActivity" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Activity_Date")))%>'></asp:Label>
                                                                
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Verified By">
                                                            <HeaderTemplate>
                                                                Description
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Request_Description")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Created Date">
                                                            <HeaderTemplate>
                                                                Request_Type
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequest_Type" runat="server" Text='<%#Eval("Request_Type")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Est Cost">
                                                            <HeaderTemplate>
                                                                Est Cost
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEstimated_Cost" runat="server" Text='<%#Eval("Estimated_Cost")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <HeaderTemplate>
                                                                Approval
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApproval" runat="server" Text='<%#Eval("Approved_By")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <HeaderTemplate>
                                                                Action
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table cellpadding="2" cellspacing="2">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="lbtnEdit" runat="server" Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Port_Activity_ID]")%>'
                                                                                OnCommand="lbtnEdit_Click" ImageUrl="~/images/edit.gif" Text="Edit"></asp:ImageButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="lbtnDelete" runat="server" CommandArgument='<%#Eval("[Port_Activity_ID]")  %>'
                                                                                Visible='<%# uaDeleteFlag %>' OnCommand="lbtnDelete_Click" ImageUrl="~/images/delete.png"
                                                                                OnClientClick="return confirm('Are you sure want to delete?')" ToolTip="Delete">
                                                                            </asp:ImageButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr id="rwTask" runat="server">
                                            <td colspan="4">
                                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                                    Agency Jobs orders to be included into proforma.</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 10%">
                                                <asp:Literal ID="ltCheckDate" Text="Check Date :" runat="server"></asp:Literal>
                                            </td>
                                            <td align="left" colspan="2" style="width: 10%">
                                                <asp:TextBox ID="txtCheckDate" runat="server" Width="120px" BackColor="#FFFFCC"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCheckDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 10%">
                                                <asp:Literal ID="ltPlanDes" Text=" Description :" runat="server"></asp:Literal>
                                            </td>
                                            <td align="left" colspan="2" style="width: 10%">
                                                <asp:TextBox ID="txtPlanDesc" runat="server" TextMode="MultiLine" Width="300px" BackColor="#FFFFCC"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnTask" runat="server" Text="Add Plan" OnClick="btnTask_Click" OnClientClick="return ValidateText1();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:GridView ID="gvtask" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                                    DataKeyNames="ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                                    AllowSorting="true">
                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Supplier_Name">
                                                            <HeaderTemplate>
                                                                Created_By
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTCreated_By" runat="server" Text='<%#Eval("Created_By")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Propose By">
                                                            <HeaderTemplate>
                                                                Activity_Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblActivity_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Activity_Date")))%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Verified By">
                                                            <HeaderTemplate>
                                                                Description
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTDescription" runat="server" Text='<%#Eval("Request_Description")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Est Cost" Visible="false">
                                                            <HeaderTemplate>
                                                                Est Cost
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEstimated_Cost" runat="server" Text='<%#Eval("Estimated_Cost")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Created Date">
                                                            <HeaderTemplate>
                                                                Request_Type
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTRequest_Type" runat="server" Text='<%#Eval("Request_Type")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <HeaderTemplate>
                                                                Action
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table cellpadding="2" cellspacing="2">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="lbtnTEdit" runat="server" CommandArgument='<%#Eval("[Port_Activity_ID]")  %>'
                                                                                Visible='<%# uaEditFlag %>' OnCommand="lbtnTEdit_Click" ImageUrl="~/images/edit.gif"
                                                                                Text="Edit"></asp:ImageButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="lbtnTDelete" runat="server" CommandArgument='<%#Eval("[Port_Activity_ID]")  %>'
                                                                                Visible='<%# uaDeleteFlag %>' OnCommand="lbtnTDelete_Click" ImageUrl="~/images/delete.png"
                                                                                OnClientClick="return confirm('Are you sure want to delete?')" ToolTip="Delete">
                                                                            </asp:ImageButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <div style="background: #cccccc; padding: 3px; font-weight: 600">
                                                    Purchase Orders Connected to this Port.</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:GridView ID="gvPurchase" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                                    CellPadding="1" CellSpacing="0" Width="100%" GridLines="both" AllowSorting="true">
                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Supplier">
                                                            <HeaderTemplate>
                                                                Supplier
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSupplierName" runat="server" Text='<%#Eval("Supplier")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO / Reference">
                                                            <HeaderTemplate>
                                                                PO / Ref.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDateOfCreatation" runat="server" Text='<%# Eval("Supplier_Quotation_Reference")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type">
                                                            <HeaderTemplate>
                                                                Type
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVerified_By" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Account">
                                                            <HeaderTemplate>
                                                                Acc. Type / Classification
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVerified_By" runat="server" Text='<%#Eval("Account")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <HeaderTemplate>
                                                                Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPropose_Status" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <HeaderTemplate>
                                                                Currency
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCurrency" runat="server" Text='<%#Eval("Currency")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                            </ItemStyle>
                                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <HeaderTemplate>
                                                                Status
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPropose_Status" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
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
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <asp:Button ID="btnExit" Width="100px" runat="server" OnClientClick="refreshAndClose();"
                            Text="Exit" Visible='<%#uaEditFlag%>' />
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
