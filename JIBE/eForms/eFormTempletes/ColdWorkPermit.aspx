<%@ Page Title="eForms: Cold Work Permit" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ColdWorkPermit.aspx.cs" Inherits="ColdWorkPermit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .eform-vertical-text
        {
            font: bold 14px verdana;
            font-weight: normal;
            writing-mode: tb-rl;
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=2);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr class="eform-report-header">
                <td style="width: 33%;">
                </td>
                <td style="width: 34%; text-align: center;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="Cold Work Permit"></asp:Label>
                </td>
                <td style="width: 33%; text-align: right;">
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            color: Black; text-align: left; background-color: #fff;">
            <table style="width: 100%">
                <tr>
                    <td colspan="6">
                        GENERAL
                    </td>
                </tr>
                <tr>
                    <td style="width: 200px">
                        Vessel Name:
                    </td>
                    <td style="width: 200px" class="eform-field-data">
                        <asp:Label ID="lblVesselName" runat="server"></asp:Label>
                    </td>
                    <td style="width: 200px">
                        Report Date:
                    </td>
                    <td class="eform-field-data">
                        <asp:Label ID="lblReportDate" runat="server"></asp:Label>
                    </td>
                    <td>
                        Distribution:
                    </td>
                    <td>
                        <asp:RadioButton ID="optDistribution" Text="SHIP" runat="server" />
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>
                        This permit is Valid From
                    </td>
                    <td>
                        <asp:TextBox ID="txtFromHrs" runat="server"></asp:TextBox>
                        &nbsp;Hrs
                    </td>
                    <td>
                        <asp:TextBox ID="txtFromDate" runat="server" />
                    </td>
                    <td>
                        To :
                    </td>
                    <td>
                        <asp:TextBox ID="txtToHrs" runat="server"></asp:TextBox>
                        &nbsp;Hrs
                    </td>
                    <td>
                        <asp:TextBox ID="txtToDate" runat="server" />
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>
                        Location of cold work
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtLocation" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Has an Enclosed Space Entry Permit been issued?
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoEnclosedEntryYes" runat="server" GroupName="EnclosedEntry" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoEnclosedEntryNo" runat="server" GroupName="EnclosedEntry" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Description of work to be done
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtDescWorkDone" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Personnel carrying out work(Name)
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtPersonnelCarryingWork" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Personnel carrying out work(Name)
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtResponsiblePerson" runat="server" />
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td colspan="4">
                        <b>Preparation And Checks To Be Carried Out By officier In Charge Of Work To Be Performed</b>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <b>YES</b>
                    </td>
                    <td>
                        <b>NO</b>
                    </td>
                </tr>
                <tr>
                    <td rowspan="6" valign="top">
                        <b>1.1</b>
                    </td>
                    <td colspan="3">
                        The equipment/pipeline has been prepared as follows
                    </td>
                </tr>
                <tr>
                    <td>
                        Vented to atmosphere
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoVentedtoatmosphereYes" GroupName="Ventedtoatmosphere" runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoVentedtoatmosphereNo" GroupName="Ventedtoatmosphere" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Drained
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoDrainedYes" GroupName="Drained" runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoDrainedNo" GroupName="Drained" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Washed
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoWashedYes" GroupName="Washed" runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoWashedNo" GroupName="Washed" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Purged
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoPurgedYes" GroupName="Purged" runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoPurgedNo" GroupName="Purged" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Other
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td rowspan="5" valign="top">
                        <b>1.2</b>
                    </td>
                    <td colspan="3">
                        The equipment/pipeline has been isolated as follows:
                    </td>
                </tr>
                <tr>
                    <td>
                        Lines Spaded
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoLinesSpadedYes" GroupName="LinesSpaded" runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoLinesSpadedNo" GroupName="LinesSpaded" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Lines Disconnected
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoLinesDisconnectedYes" GroupName="LinesDisconnected" runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoLinesDisconnectedNo" GroupName="LinesDisconnected" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Valves Closed
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoValvesClosedYes" GroupName="ValvesClosed" runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoValvesClosedNo" GroupName="ValvesClosed" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Other
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td rowspan="6" valign="top">
                        <b>1.3</b>
                    </td>
                    <td colspan="3">
                        Is equipment free from
                    </td>
                </tr>
                <tr>
                    <td>
                        Oil
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoOilYes" GroupName="Oil1" runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoOilNo" GroupName="Oil1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Gas
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoGasYes" GroupName="Gas" runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoGasNo" GroupName="Gas" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        H2S
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoH2SYes" GroupName="H2S" runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoH2SNo" GroupName="H2S" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Steam
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoSteamYes" GroupName="Steam" runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoSteamNo" GroupName="Steam" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Pressure
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoPressureYes" GroupName="Pressure" runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoPressureNo" GroupName="Pressure" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>1.4</b>
                    </td>
                    <td>
                        Is surrounding area free from hazards?
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>1.5</b>
                    </td>
                    <td>
                        If work is to be performed on electrical equipment has that equipment been isolated
                        and an electrical isolation certificate issued?
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoisolationYes" GroupName="Isolation" runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoisolationNo" GroupName="Isolation" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td colspan="4">
                        <b>Has the following been clearly explained to the persons carrying out the work.</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>2.1</b>
                    </td>
                    <td>
                        The following personal protection must be worn
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtProtectionWorn" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>2.2</b>
                    </td>
                    <td>
                        Equipment / Pipeline contained following material in service
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtMaterialService" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>2.3</b>
                    </td>
                    <td>
                        Equipment expected to contain the following hazardous material when open
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtHazardousmaterial" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>2.4</b>
                    </td>
                    <td>
                        Special conditions / Precautions
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtSpecialPrecautions" runat="server" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <b>In the circumstances noted it is considered safe to proceed with this work.</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        Master :
                    </td>
                    <td>
                    </td>
                    <td>
                        Ch.Engg/Ch/Off/2nd Engr.
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td rowspan="2">
                        Authorized Officer in charge &amp; Signature
                    </td>
                    <td>
                    </td>
                    <td>
                        Date :
                    </td>
                    <td>
                    <asp:TextBox ID="txtAuthorizedDate" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        Time :
                    </td>
                    <td>
                     <asp:TextBox ID="txtAuthorizedTime" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <b>To be retained by responsible officer and returned to Master for filing, completion
                            of the work. </b>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <b>Notes: This permit should be used for but not be limited to the following cold work
                            : </b>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <b>1. Blanking / de-blanking. </b>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <b>2. Disconnecting and connecting pipe work. </b>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <b>3. Removing and fitting of valves, blanks, spades or blinds. </b>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <b>4. Work on pumps etc. </b>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <b>5. Clean up (oil spills). </b>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        This checklist shall be maintained as hard copy
                    </td>
                </tr>
            </table>
         
        </div>
    </div>
</asp:Content>
