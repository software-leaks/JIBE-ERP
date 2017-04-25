<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortCallCrewList.aspx.cs"
    Inherits="VesselMovement_PortCallCrewList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crew List</title>
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
     <script type="text/javascript">
         function Onfail(retval) {
             alert(retval._message);


         }

         function ValidateText() {

             var strDateFormat = "<%= DateFormat %>";
             if (IsInvalidDate($("#txtAsofDate").val(), strDateFormat)) {
                 alert("Please Enter Valid Crew list As of Date.");
                 document.getElementById("txtAsofDate").focus();
                 return false;
             }

         }


         function asyncBind_CrewList_M() {

             var VesselID = document.getElementById('ddlCrewVessel').value;
             var AsOFDate = document.getElementById('txtAsofDate').value;
             var CrewStatus = document.getElementById('ddlCrewStatus').value;
             var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncBind_CrewList', false, { "Vessel_ID": VesselID, "AsofDate": AsOFDate, "Status": CrewStatus }, onSucc_LoadFunction, Onfail, new Array('dvCrewList', 'lblWebCrewList'));

             //lastExecutorMinMaxQty_M = service.get_executor();

         }

//         function onSucc_LoadFunction(retval, prm) {
//             try {
//                 document.getElementById(prm[0]).innerHTML = retval;

//                 checkForMyAction(prm[1], retval);
//             }
//             catch (ex)
//            { }
//         }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Script1" runat="server">
    </asp:ScriptManager>
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
        <center>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td align="center">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            Crew List
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                    <asp:UpdatePanel ID="updCrewList" runat="server">
                    <ContentTemplate>
                        <div style="background-color: White;   max-height: 100%;">
                             <asp:UpdatePanel ID="Update1" runat="server">
                            <ContentTemplate>
                            <table style="margin-top: 10px;">
                                <tr>
                                    <td valign="top">
                                        Vessel :
                                        <asp:DropDownList ID="ddlCrewVessel" runat="server" Width="200px" CssClass="txtInput">
                                        </asp:DropDownList>
                                    </td>
                                    <td valign="top">
                                        Crew list As of Date :
                                        <asp:TextBox ID="txtAsofDate" runat="server" Width="120px" BackColor="#FFFFCC"></asp:TextBox><ajaxToolkit:CalendarExtender
                                            ID="calAsOfDate" runat="server" TargetControlID="txtAsofDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td valign="top">
                                        Status : &nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlCrewStatus" runat="server" BackColor="#FFFFCC">
                                            <asp:ListItem Value="0" Text="ALL"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Signed-Off"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Current" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td valign="top">
                                        <asp:ImageButton ID="imgCrewFilter" runat="server" ToolTip="Search" ImageUrl="~/Images/SearchButton.png"
                                            OnClick="imgCrewFilter_Click" OnClientClick="return ValidateText();" />
                                            <%--<asp:ImageButton ID="imgCrewFilter" runat="server" ToolTip="Search" ImageUrl="~/Images/SearchButton.png"
                                                    OnClientClick="asyncBind_CrewList_M()" />--%>
                                    </td>
                                </tr>
                            </table></ContentTemplate>
                            </asp:UpdatePanel>
                            <table width="98%">
                                <tr>
                                    <td align="center" > <asp:Label ID="lblWebCrewList" Title="Crew List" runat="server" Width="100%">
                                        <div style=" border: 1px solid #cccccc;">
                                             <div id="dvCrewList">
                                             </div>
                                        </div>
                                        </asp:Label>
                                        <asp:GridView ID="gvCrewList" runat="server" EmptyDataText="NO RECORDS FOUND" ShowHeaderWhenEmpty="True"
                                            AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" Width="100%">
                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Staff Code">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lblSTAFF_CODE" NavigateUrl='<%#"../Crew/CrewDetails.aspx?ID="+ Eval("ID")%>' runat="server" Text='<%# Eval("Staff_Code")%>' CssClass="staffInfo"></asp:HyperLink></ItemTemplate>
                                                    <ItemStyle Wrap="True" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Staff Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTAFF_NAME" Visible="true" runat="server" Width="350px" Text='<%# Eval("Staff_FullName")%>'></asp:Label></ItemTemplate>
                                                    <ItemStyle Wrap="True" HorizontalAlign="Left" Width="350px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rank Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRank_Name" runat="server" Text='<%# Eval("Rank_Name")%>'></asp:Label></ItemTemplate>
                                                    <ItemStyle Wrap="True" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nationality">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTAFF_NATIONALITY" runat="server" Text='<%# Eval("Nationality")%>'></asp:Label></ItemTemplate>
                                                    <ItemStyle Wrap="True" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sign On Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSIGN_ON_DATE" Visible="true" runat="server" Width="150px" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Joining_Date")))%>'></asp:Label></ItemTemplate>
                                                    <ItemStyle Wrap="True" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sign On Port">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSIGN_ON_PORT" Visible="true" runat="server" Width="150px" Text='<%# Eval("JoiningPort")%>'></asp:Label></ItemTemplate>
                                                    <ItemStyle Wrap="True" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Est Sign Off">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEst_Sing_Off_Date" Visible="true" runat="server" Width="150px" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Est_Sing_Off_Date")))%>'></asp:Label></ItemTemplate>
                                                    <ItemStyle Wrap="True" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                           </ContentTemplate>
                         </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
                <tr>
                <td><asp:Button ID="btnExit" runat="server"  Visible="false" Text="Exit" OnClientClick="javaScript:CloseThisWindow();" />
                </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
