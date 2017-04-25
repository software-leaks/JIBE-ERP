<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMS_CopyItems.aspx.cs" Inherits="Technical_PMS_PMS_CopyItems" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <link href="../../Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.alerts.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
</head>
<body >
    <script language="javascript" type="text/javascript">

        function ValidationOnOverWriteAppend(id, action) {

            var FromVessel = document.getElementById("DDLFromVessel").value;
            var FromSystem = document.getElementById("DDLFromSystem").value;
            var FromSubsystem = document.getElementById("DDLFromSubsystem").value;

            var FromDepartment = document.getElementById("DDLFromDept").value;
            var ToVessel = document.getElementById("DDLToVessel").value;
            var ToSystem = document.getElementById("DDLToSystem").value;
            var ToSubsystem = document.getElementById("DDLToSubsystem").value;

            var ToDepartment = document.getElementById("DDLToDept").value;


            if (FromDepartment == "0") {
                alert("Please select From Department."); return false;
            }

            if (ToDepartment == "0") {
                alert("Please select To Department."); return false;
            }

            if (FromSystem == "0") {
                alert("Please select From System."); return false;
            }

            if (ToSystem == "0") {
                alert("Please select to System."); return false;
            }

            if (FromSubsystem == "0") {
                alert("Please select From Subsytem"); return false;

            }

            if (ToSubsystem == "0") {
                alert("Please select To Subsytem"); return false;

            }

            if (FromSubsystem == ToSubsystem) {
                alert("From SubSystem and To SubSystem can not be same."); return false;
            }

            $.alerts.okButton = " Yes ";
            $.alerts.cancelButton = " No ";

            var strMsg = "Jobs will be Move \n"
                             + "from : \n"
                             + "Vessel     : " + document.getElementById("DDLFromVessel").options[document.getElementById("DDLFromVessel").selectedIndex].text + "\n"
                             + "System   : " + document.getElementById("DDLFromSystem").options[document.getElementById("DDLFromSystem").selectedIndex].text + "\n"
                             + "Subsystem : " + document.getElementById("DDLFromSubsystem").options[document.getElementById("DDLFromSubsystem").selectedIndex].text + "\n\n"
                             + "To : \n"
                             + "Vessel     : " + document.getElementById("DDLToVessel").options[document.getElementById("DDLToVessel").selectedIndex].text + "\n"
                             + "System   : " + document.getElementById("DDLToSystem").options[document.getElementById("DDLToSystem").selectedIndex].text + "\n"
                             + "Subsystem : " + document.getElementById("DDLToSubsystem").options[document.getElementById("DDLToSubsystem").selectedIndex].text + "\n\n"
                             + "Are you sure want to continue ?";

            var aa = jConfirm(strMsg, ' Confirmation Required !', function (r) {

                if (r) {

                    var postBackstr = "__doPostBack('" + id.replace(/_/gi, '$') + "','" + id.replace(/_/gi, '$') + "_Click')";
                    window.setTimeout(postBackstr, 0, 'JavaScript');
                    return true;


                }
                else {
                    return false;
                }
            }

            );

            return false;
        }

    </script>
    <form id="frmMoveJobs" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div style="font-family: Tahoma; font-size: 12px;" >
       
            <table cellpadding="1" cellspacing="1" style="border: 1px solid gray; width:960px;">
                <tr>
                    <td colspan="5">
                        <div style="background-color: #5588BB; color: #FFFF66; vertical-align: middle; text-align: center;
                            height: 20px">
                            <b>Copy Items</b></div>
                    </td>
                </tr>
                <tr>
                    <td valign="top" >
                        <asp:UpdatePanel runat="server" ID="UpdPnlFrom" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="border: 1px solid gray; height: 170px">
                                    <table cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td align="center" colspan="3">
                                                From
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Vessel :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="DDLFromVessel" runat="server" Enabled="false" AppendDataBoundItems="true"
                                                    Width="260px">
                                                    <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Department/Functions :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="DDLFromfunc" runat="server" AppendDataBoundItems="true" Width="260px"
                                                    OnSelectedIndexChanged="DDLFromFunc_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Machinery :&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFromMachinery" Width="232px" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgFromMachinerySearch" ImageUrl="~/Purchase/Image/preview.gif"
                                                    runat="server" OnClick="imgFromMachinerySearch_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                System / Catalogue&nbsp;&nbsp; :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="DDLFromSystem" runat="server" AppendDataBoundItems="true" Width="260px"
                                                    OnSelectedIndexChanged="DDLFromSystem_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Subsystem / Subcatalogue :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="DDLFromSubsystem" runat="server" AppendDataBoundItems="true"
                                                    Width="260px" AutoPostBack="True" OnSelectedIndexChanged="DDLFromSubsystem_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Total Jobs Count :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="lblFromJobCount" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 10px" >
                        <table>
                            <tr>
                                <td style="padding-top: 42px" >
                                    <asp:UpdatePanel runat="server" ID="updSelectToSystem" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:ImageButton ID="btnSelectSystem" ImageUrl="../../Images/panel-next-big.png" Width="20px"
                                                runat="server" OnClick="btnSelectSystem_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <asp:UpdatePanel runat="server" ID="UpdPnlTo" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="border: 1px solid gray; height: 170px">
                                    <table cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td align="center" colspan="3">
                                                To
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Vessel :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="DDLToVessel" runat="server" Enabled="false" AppendDataBoundItems="true"
                                                    Width="260px">
                                                    <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Department/Functions:&nbsp;&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="DDLTofunc" runat="server" AppendDataBoundItems="true" Width="260px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DDLToFunc_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Machinery :&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtToMachinery" Width="232px" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:ImageButton ID="imgToMachinerySearch" ImageUrl="~/Purchase/Image/preview.gif"
                                                    runat="server" OnClick="imgToMachinerySearch_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                System / Catalogue :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="DDLToSystem" runat="server" AppendDataBoundItems="true" Width="260px"
                                                    OnSelectedIndexChanged="DDLToSystem_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Subsystem / SubCatalogue :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="DDLToSubsystem" runat="server" AppendDataBoundItems="true"
                                                    Width="260px" AutoPostBack="True" OnSelectedIndexChanged="DDLToSubsystem_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Total Jobs Count :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="lblToJobCount" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center" style="background-color: #CCCCCC;padding:10px" >
                        <%-- <asp:Button ID="btnMoveJob" Text="Move Job" runat="server" OnClientClick="return ValidationOnOverWriteAppend(id,'Append');"
                            OnClick="btnMoveJob_Click" />--%>
                       <%-- <asp:Button ID="btnMoveItems" Text="Move Items" runat="server" OnClientClick="return ValidationOnOverWriteAppend(id,'Append');"
                            OnClick="btnMoveItems_Click" />--%>
                            <asp:Button ID="BtnOverwrite" Text="Owerwrite" runat="server" OnClientClick="return ValidationOnOverWriteAppend(id,'Append');"
                            OnClick="btnOW_Click" />
                            <asp:Button ID="BtnAppend" Text="Append" runat="server" OnClientClick="return ValidationOnAppend(id,'Append');"
                            OnClick="btnApnd_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
      
    </div>
    </form>
</body>
</html>
