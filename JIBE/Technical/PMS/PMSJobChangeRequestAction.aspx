<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMSJobChangeRequestAction.aspx.cs"
    Inherits="Technical_PMS_PMSJobChangeRequestAction"  EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Approve / Reject -JOB Change Request</title>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <script language="javascript" type="text/javascript">
        function CloseDiv() {
            var control = document.getElementById("divAddLocation");
            control.style.visibility = "hidden";
        }

        function validationReject() {

            var CRActionedRemarks = document.getElementById("txtCRActionedRemarks").value;

            if (CRActionedRemarks == "") {
                alert("Actioned Remarks is required.");
                return false;
            }

            return true;
        }

        function validationApprove() {
            var CRJobtitle = document.getElementById("txtCRJobtitle").value;
            var CRActionedRemarks = document.getElementById("txtCRActionedRemarks").value;
            var CRFrequency = document.getElementById("txtCRFrequency").value;
            var hdfAddNewFlag = document.getElementById("hdfAddNewFlag").value;

            if (CRJobtitle == "") {
                alert("Job Title is required.");
                return false;
            }

            if (CRFrequency != "") {
                if (isNaN(CRFrequency)) {
                    alert('Frequency is accept ony numeric value.')
                    return false;
                }
            }

            if (CRActionedRemarks == "") {
                alert("Actioned Remarks is required.");
                return false;
            }

            return true;
        }

    </script>
    <form id="frmJobChangeRequestAction" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="pnlCatalogue">
        <ContentTemplate>
            <center>
                <div style="font-family: Tahoma; font-size: 12px; margin: 5px">
                    <table cellpadding="1" cellspacing="1" width="100%">
                        
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                               <div style="width:100%;border:1px solid #cccccc;">
                                    <table cellpadding="1" cellspacing="2" width="100%" >
                                        <tr style="background-color:Green;font-size:medium;color:White">
                                            <td align="center" colspan="4">
                                             Original Job</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Job Title :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtJobtitle"  runat="server" MaxLength="254"   ReadOnly="true" CssClass="txtReadOnly"
                                                    Width="98%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Job Description :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:TextBox ID="txtjobDescription" ReadOnly="true" runat="server" Font-Names="Tahoma"  CssClass="txtReadOnly"
                                                    Font-Size="9.5pt" Height="60px" MaxLength="2000" TextMode="MultiLine" Width="97%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="">
                                                Frequency :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" style="">
                                                <asp:TextBox ID="txtFrequency" MaxLength="5" ReadOnly="true" Width="80%" runat="server"  CssClass="txtReadOnly"></asp:TextBox>
                                            </td>
                                            <td colspan="2" align="left">
                                                <asp:RadioButtonList ID="optCMS" runat="server" RepeatDirection="Horizontal" CssClass="txtReadOnly" Enabled="false" Width="160px">
                                                    <asp:ListItem Text="CMS" Value="1"></asp:ListItem>
                                                    <asp:ListItem Selected="True" Text="Non CMS" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="">
                                                Frequency Type :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" valign="middle" rowspan="2">
                                                <asp:ListBox ID="lstFrequency" runat="server" Height="60px" Width="100%"  CssClass="txtReadOnly"  Enabled="false"></asp:ListBox>
                                            </td>
                                            <td colspan="2" align="left">
                                                <asp:RadioButtonList ID="optCritical" runat="server" RepeatDirection="Horizontal" CssClass="txtReadOnly" Enabled="false" Width="160px">
                                                    <asp:ListItem Text="Critical" Value="1"></asp:ListItem>
                                                    <asp:ListItem Selected="True" Text="Non Critical" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="">
                                                &nbsp;
                                            </td>
                                            <td align="left" colspan="2" style="" valign="middle">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="vertical-align: top;">
                                                Department :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" style="vertical-align: top;">
                                                <asp:ListBox ID="lstDepartment" runat="server" Height="60px" Width="100%" CssClass="txtReadOnly"  Enabled="false"></asp:ListBox>
                                            </td>
                                            <td align="right" style="vertical-align: top;">
                                                Rank :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" style="vertical-align: top;">
                                                <asp:DropDownList ID="ddlRank" runat="server" Width="80%" CssClass="txtReadOnly" Enabled="false" >
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Machinery :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:Label ID="lblMachinery" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Sub System :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:Label ID="lblSubsystem" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Job Code :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:Label ID="lblJobCode" runat="server"></asp:Label>
                                            </td>
                                        
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="width: 2%;">
                            </td>
                            <td style="width: 50%; vertical-align: top">
                                 <div style="width:100%;border:1px solid #cccccc;">
                                    <table cellpadding="1" cellspacing="2" width="100%">
                                        <tr>
                                            <td align="right" style="background-color:Red;font-size:medium;color:White">
                                                &nbsp;Request For :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="3" align="left" style="width: 6%;">
                                            <asp:Label ID="lblRequestFor" Font-Bold="true" runat="server" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Job Title :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtCRJobtitle" runat="server" MaxLength="254" Width="98%" CssClass="txtInput"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Job Description :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:TextBox ID="txtCRjobDescription" Font-Names="Tahoma" Font-Size="9.5pt" TextMode="MultiLine" CssClass="txtInput"
                                                    runat="server" MaxLength="2000" Width="98%" Height="60px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Frequency :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" style="">
                                                <asp:TextBox ID="txtCRFrequency" MaxLength="5" Width="80%" runat="server" CssClass="txtInput"></asp:TextBox>
                                            </td>
                                            <td colspan="2" align="left">
                                                <asp:RadioButtonList ID="optCRCMS" runat="server" RepeatDirection="Horizontal" CssClass="txtInput" Width="160px">
                                                    <asp:ListItem Text="CMS" Value="1"></asp:ListItem>
                                                    <asp:ListItem Selected="True" Text="Non CMS" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Frequency Type :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" rowspan="2">
                                                <asp:ListBox ID="lstCRFrequency" runat="server" Height="60px" Width="100%" CssClass="txtInput"></asp:ListBox>
                                            </td>
                                            <td colspan="2" align="left">
                                                <asp:RadioButtonList ID="optCRCritical" runat="server" RepeatDirection="Horizontal" CssClass="txtInput" Width="160px">
                                                    <asp:ListItem Text="Critical" Value="1"></asp:ListItem>
                                                    <asp:ListItem Selected="True" Text="Non Critical" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td align="left" colspan="2" valign="middle">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="">
                                                Department :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" style="">
                                                <asp:ListBox ID="lstCRDepartment" runat="server" Height="60px" Width="100%" CssClass="txtInput"></asp:ListBox>
                                            </td>
                                            <td align="right" style="vertical-align: top;">
                                                Rank:&nbsp;&nbsp;
                                            </td>
                                            <td align="left" style="vertical-align: top;">
                                                <asp:DropDownList ID="ddlCRRank" runat="server" Width="98%" CssClass="txtInput">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="">
                                                Change Reason :&nbsp;&nbsp;
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtCRChangeReason" Font-Names="Tahoma" Font-Size="9.5pt" TextMode="MultiLine" CssClass="txtReadOnly"
                                                    ReadOnly="true" runat="server" MaxLength="2000" Width="98%" Height="40px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="">
                                                Actioned Remarks : <span style="color:Red">*</span>
                                            </td>
                                            <td align="left" colspan="3" style="">
                                                <asp:TextBox ID="txtCRActionedRemarks" Font-Names="Tahoma" Font-Size="9.5pt" TextMode="MultiLine" CssClass="txtInput"
                                                    runat="server" MaxLength="2000" Width="98%" Height="40px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Actioned by :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="3" align="left" style="">
                                                <asp:Label ID="lblActionedBy" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Request by :&nbsp;&nbsp;
                                            </td>
                                            <td colspan="3" align="left" style="">
                                                <asp:Label ID="lblRequestedBy" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="">
                                                <asp:HiddenField ID="hdfAddNewFlag" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnDivApprove" runat="server" OnClick="btnDivApprove_Click" OnClientClick="return validationApprove();"
                                                    Text="Approved" Width="100px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnDivReject" runat="server" OnClick="btnDivReject_Click" OnClientClick="return validationReject();"
                                                    Text="Reject" Width="100px" />
                                            </td>
                                            <td>
                                                <input type="button" name="btnCancel" style="font-size: 12px; width: 100px" value="Close"
                                                    onclick="javascript:parent.ReloadParent_ByButtonID();">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
