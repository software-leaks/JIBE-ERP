<%@ Page Title="A/E Monthly Running Hrs Report" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="SM001EnclosedSpaceEntry.aspx.cs" Inherits="SM001EnclosedSpaceEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr class="eform-report-header">
                <td style="width: 85%; text-align: center;" rowspan="2" colspan="3">
                    <asp:Label ID="lblPageTitle" runat="server" Text="DRUG AND ALCOHOL POLICY DECLARATION"></asp:Label>
                </td>
                <td style="width: 15%; text-align: right;">
                </td>
            </tr>
            <tr class="eform-report-header">
                <td style="width: 10%; text-align: Center;">
                    Distribution
                </td>
            </tr>
            <tr class="eform-report-header">
                <td style="width: 85%; text-align: center;" colspan="3">
                    <asp:Label ID="Label5" runat="server" Text="ENCLOSED SPACE ENTRY PERMIT"></asp:Label>
                </td>
                <td style="width: 15%; text-align: right;">
                    <asp:CheckBox ID="chkIsDistibution" runat="server" />
                </td>
            </tr>
            <tr class="eform-report-header">
                <td style="width: 10%; text-align: right;">
                    Vessel :
                </td>
                <td style="width: 40%; text-align: left;">
                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 35%; text-align: right;">
                    Reference Number :
                </td>
                <td style="width: 15%; text-align: left;">
                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvPageContent" class="page-content-main">
        <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
            color: Black; text-align: left; background-color: #fff;">
            <asp:FormView ID="frmMain" runat="Server" Width="100%">
                <ItemTemplate>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 100%; text-align: left; vertical-align: top;">
                                <table class="eform-report-table" cellpadding="2" border="1" width="100%">
                                    <tr>
                                        <td colspan="8" style="text-align: Center;">
                                            This permit relates to entry into any enclosed space and shall be completed by the
                                            Master or responsible officer and by the person entering the space or authorized
                                            team leader.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            GENERAL
                                        </td>
                                    </tr>
                                    <tr>
                                        <tr>
                                            <td>
                                                Location / Name of Enclosed Space
                                            </td>
                                            <td style="width: 15px">
                                                :
                                            </td>
                                            <td class="eform-field-data" colspan="6">
                                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Reason for entry
                                            </td>
                                            <td style="width: 15px">
                                                :
                                            </td>
                                            <td class="eform-field-data" colspan="6">
                                                <asp:Label ID="lblVessel" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td rowspan="2">
                                                This permit is Valid
                                            </td>
                                            <td style="width: 15px" rowspan="2">
                                                :
                                            </td>
                                            <td class="eform-field-data">
                                                From :
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                            </td>
                                            <td>
                                                Hrs
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                            </td>
                                            <td>
                                                Date
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="eform-field-data">
                                                To :
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                            </td>
                                            <td>
                                                Hrs
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                            </td>
                                            <td>
                                                Date
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: left; vertical-align: top;">
                                <table class="eform-report-table" cellpadding="2" border="1" width="100%">
                                    <tr>
                                        <td colspan="7" style="text-align: Center;">
                                            Section 1
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="text-align: Center;">
                                            PRE-ENTRY PREPARATIONS
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="text-align: Center;">
                                            (This section to be completed by Master, C/E, C/O, 2/E)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                        </td>
                                        <td style="width: 60px">
                                            Yes
                                        </td>
                                        <td style="width: 60px">
                                            No
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            Has the space been thoroughly ventilated?
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox1" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            Has the space been segregated by blanking off or isolating all connecting pipelines
                                            or
                                            <br></br>
                                            valves and electrical power/equipment?
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox2" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox3" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            Has the space been cleaned where necessary?
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox4" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox5" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            Has the space been tested and found safe for entry?
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox6" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox7" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            Pre-entry atmosphere tests : (See Note 2)
                                        </td>
                                        <td style="width: 60px">
                                            BY :
                                        </td>
                                        <td class="eform-field-data" colspan="2">
                                            <asp:Label ID="Label12" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Oxygen
                                        </td>
                                        <td style="width: 60px">
                                            <asp:Label ID="Label11" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                        <td class="eform-field-data">
                                            % Vol. (21%)
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Hydrocarbon
                                        </td>
                                        <td style="width: 60px">
                                            <asp:Label ID="Label13" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                        <td class="eform-field-data">
                                            % LFL (Less than 1%)
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Toxic Gases
                                        </td>
                                        <td style="width: 60px">
                                            <asp:Label ID="Label14" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                        <td class="eform-field-data">
                                            PPM (Specify gas & PEL) (See Note 3)
                                        </td>
                                        <td>
                                            Time
                                        </td>
                                        <td class="eform-field-data" colspan="2">
                                            <asp:Label ID="Label15" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            1.
                                        </td>
                                        <td colspan="4">
                                            Have arrangements been made for frequent atmosphere checks to be made while the
                                            spaces is occupied and after work breaks?
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox8" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox9" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2.
                                        </td>
                                        <td colspan="4">
                                            Have arrangements been made for the space to be continuously ventilated throughout
                                            the period of occupations and during work breaks?
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox10" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox11" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3.
                                        </td>
                                        <td colspan="4">
                                            Are Access and illumination adequate?
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox12" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox13" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4.
                                        </td>
                                        <td colspan="4">
                                            Is rescue and resuscitation equipment available for immediate use by the entrance
                                            to the space?
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox14" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox15" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            5.
                                        </td>
                                        <td colspan="4">
                                            Has a responsible person been designated to be in constant attendance at the entrance
                                            to the space?
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox16" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox17" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            6.
                                        </td>
                                        <td colspan="4">
                                            Has the Officer of the watch (bridge, engine room, cargo control room) been advised
                                            of the planned entry?
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox18" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox19" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            7.
                                        </td>
                                        <td colspan="4">
                                            Has a system of communication between all parties been tested and emergency signals
                                            agreed?
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox20" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox21" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            8.
                                        </td>
                                        <td colspan="4">
                                            Are emergency and evacuation procedures established and understood by all personnel
                                            involved with the enclosed space entry?
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox22" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox23" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            9.
                                        </td>
                                        <td colspan="4">
                                            Is all equipment used in good working condition and inspected prior to entry? (For
                                            Breathing apparatus checks refer Section – 3)
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox24" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox25" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            10.
                                        </td>
                                        <td colspan="4">
                                            Are personnel properly clothed and equipped?
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox26" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox27" Text="" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: left; vertical-align: top;">
                                <table class="eform-report-table" cellpadding="2" border="1" width="100%">
                                    <tr>
                                        <td colspan="7" style="text-align: Center;">
                                            Section 2
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="text-align: Center;">
                                            PRE-ENTRY CHECKS
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="text-align: Center;">
                                            (This section to be checked by the person entering the space or authorized team
                                            leader)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                        </td>
                                        <td style="width: 60px">
                                            Yes
                                        </td>
                                        <td style="width: 60px">
                                            No
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            1.
                                        </td>
                                        <td colspan="4">
                                            I have received instructions or permission from the master or nominated responsible
                                            person to enter the enclosed space.
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox36" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox37" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2.
                                        </td>
                                        <td colspan="4">
                                            Section 1 of this permit has been satisfactorily completed by the master or nominated
                                            responsible person.
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox38" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox39" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3.
                                        </td>
                                        <td colspan="4">
                                            I have agreed and understand the communication procedures.
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox40" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox41" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4.
                                        </td>
                                        <td colspan="2">
                                            I have agreed upon a reporting interval of .
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            minutes.
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox42" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox43" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            5.
                                        </td>
                                        <td colspan="4">
                                            Emergency and evacuation procedures have been agreed and are understood.
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox44" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox45" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            6.
                                        </td>
                                        <td colspan="4">
                                            I am aware that the space must be vacated immediately In the event of ventilation
                                            failure or if atmosphere tests change from agreed safe criteria.
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox46" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox47" Text="" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: left; vertical-align: top;">
                                <table class="eform-report-table" cellpadding="2" border="1" width="100%">
                                    <tr>
                                        <td colspan="5" style="text-align: Center;">
                                            Section 3
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" style="text-align: Center;">
                                            PRE-ENTRY CHECKS FOR BREATHING APPARATUS AND OTHER RELATED EQUIPMENT
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" style="text-align: Center;">
                                            (This section to be checked and confirmed jointly by the Master / C/E / C/O or 2/E
                                            and the person who is to enter the space)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 60px">
                                        </td>
                                        <td colspan="2">
                                        </td>
                                        <td style="width: 60px">
                                            Yes
                                        </td>
                                        <td style="width: 60px">
                                            No
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 60px">
                                            1.
                                        </td>
                                        <td colspan="2">
                                            Has the space been thoroughly ventilated?
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox28" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox29" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2.
                                        </td>
                                        <td colspan="2">
                                            The breathing apparatus has been tested as follows:
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox30" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox31" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            gauge and capacity of air supply
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="Label18" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            low pressure audible alarm
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="Label19" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            face mask – under positive pressure and not leaking
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="Label20" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 60px">
                                            3.
                                        </td>
                                        <td colspan="2">
                                            The means of communication has been tested and emergency signals agreed
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox34" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox35" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4.
                                        </td>
                                        <td colspan="2">
                                            Rescue harnesses and, where required, lifelines have been checked and kept ready
                                            for immediate use
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox32" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox33" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" style="text-align: Center;">
                                            Signed upon completion of sections 1, 2 and 3 by:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                        <td style="width: 60px">
                                            Date
                                        </td>
                                        <td style="width: 60px">
                                            Time
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Master or C/E
                                        </td>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label22" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label23" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            C/O or 2/E supervising entry
                                        </td>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label24" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label25" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Person entering the space or authorized team leader
                                        </td>
                                        <td>
                                            <asp:Label ID="Label21" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label26" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label27" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: left; vertical-align: top;" width="100%">
                                <asp:Repeater ID="rpEngine1" runat="server">
                                    <HeaderTemplate>
                                        <table class="eform-report-table" cellpadding="2" border="1">
                                            <tr>
                                                <td colspan="3" style="text-align: Center;">
                                                    Section 4
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="text-align: Center;">
                                                    PERSONNEL ENTRY
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="text-align: Center;">
                                                    (This section To be completed by C/O or 2/E supervising entry)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="width: 60px">
                                                    Time In
                                                </td>
                                                <td style="width: 60px">
                                                    Time Out
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label52" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                            </td>
                                            <td style="width: 60px">
                                                <asp:Label ID="Label54" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                            </td>
                                            <td style="width: 60px">
                                                <asp:Label ID="Label55" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: left; vertical-align: top;">
                                <table class="eform-report-table" cellpadding="2" border="1" width="100%">
                                    <tr>
                                        <td colspan="4" style="text-align: Center;">
                                            Section 5
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: Center;">
                                            COMPLETION OF JOB
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: Center;">
                                            (This section to be completed by C/O or 2/E supervising entry)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                        </td>
                                        <td style="width: 60px">
                                            Date
                                        </td>
                                        <td style="width: 60px">
                                            Time
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Job completed
                                        </td>
                                        <td>
                                            <asp:Label ID="Label44" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label45" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Space secured against entry
                                        </td>
                                        <td>
                                            <asp:Label ID="Label47" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label48" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            The officer of the watch has been duly informed
                                        </td>
                                        <td>
                                            <asp:Label ID="Label50" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label51" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: Center;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: Center;">
                                            COMPLETION OF JOB
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            C/O or 2/E supervising entry
                                        </td>
                                        <td>
                                            <asp:Label ID="Label28" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                        <td style="width: 60px">
                                            Date
                                        </td>
                                        <td style="width: 60px">
                                            Time
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Master must be informed immediately on start and on completion of job.
                                        </td>
                                        <td>
                                            <asp:Label ID="Label29" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label30" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: left; vertical-align: top;">
                                <table class="eform-report-table" cellpadding="2" border="1" width="100%">
                                    <tr>
                                        <td colspan="7" style="text-align: Center;">
                                            Section 2
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="text-align: Center;">
                                            PRE-ENTRY CHECKS
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="text-align: Center;">
                                            (This section to be checked by the person entering the space or authorized team
                                            leader)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                        </td>
                                        <td style="width: 60px">
                                            Yes
                                        </td>
                                        <td style="width: 60px">
                                            No
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            1.
                                        </td>
                                        <td colspan="4">
                                            I have received instructions or permission from the master or nominated responsible
                                            person to enter the enclosed space.
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox48" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox49" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2.
                                        </td>
                                        <td colspan="4">
                                            Section 1 of this permit has been satisfactorily completed by the master or nominated
                                            responsible person.
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox50" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox51" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3.
                                        </td>
                                        <td colspan="4">
                                            I have agreed and understand the communication procedures.
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox52" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox53" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4.
                                        </td>
                                        <td colspan="2">
                                            I have agreed upon a reporting interval of .
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            minutes.
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox54" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox55" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            5.
                                        </td>
                                        <td colspan="4">
                                            Emergency and evacuation procedures have been agreed and are understood.
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox56" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox57" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            6.
                                        </td>
                                        <td colspan="4">
                                            I am aware that the space must be vacated immediately In the event of ventilation
                                            failure or if atmosphere tests change from agreed safe criteria.
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox58" runat="server" Text="" />
                                        </td>
                                        <td style="width: 60px">
                                            <asp:CheckBox ID="CheckBox59" Text="" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: left; vertical-align: top;">
                                <table class="eform-report-table" cellpadding="2" border="1" width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: Center;">
                                            THIS PERMIT IS RENDERED IN VALID SHOULD VENTILATION OF THE SPACE STOP OR IF ANY
                                            OF THE CONDITIONS NOTED IN THE CHECKLIST CHANGE.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: left;">
                                            Notes:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            1.
                                        </td>
                                        <td>
                                            The permit shall contain a clear indication as to its maximum period of validity,
                                            which in any event, should not exceed 12 hours.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2.
                                        </td>
                                        <td>
                                            In order to obtain a representative cross-section of the space’s atmosphere, samples
                                            shall be taken from several levels and through as many openings as possible. Ventilation
                                            shall be stopped for about 10 minutes before the pre-entry atmosphere tests are
                                            taken.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3.
                                        </td>
                                        <td>
                                            3. Tests for specific toxic contaminants, such as benzene or hydrogen sulphide,
                                            shall be undertaken depending on the nature of the previous contents of the space.
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: left; vertical-align: top;">
                                <table class="eform-report-table" cellpadding="2" border="1" width="100%">
                                    <tr>
                                        <td colspan="2" style="text-align: left;">
                                            ADDITIONAL NOTES:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30px">
                                        </td>
                                        <td>
                                            PERMIT IS TO BE POSTED AT THE ENTRANCE OF THE SPACE TO BE ENTERED, BEFORE ENTRY
                                            IS MADE.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30px">
                                        </td>
                                        <td>
                                            IF THERE IS MORE THAN ONE ENTRANCE TO THE SPACE, WHICH CAN BE USED FOR ACCESS, THEN
                                            A COPY TO BE POSTED AT EACH ENTRANCE.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30px">
                                        </td>
                                        <td>
                                            IF THE ENTRANCES ARE NOT WITHIN SIGHT AND CONTROL OF THE ‘RESPONSIBLE PERSON AT
                                            ENTRANCE’, THEN ADDITIONAL ‘RESPONSIBLE PERSONS’ ARE TO BE DESIGNATED AS NECESSARY.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30px">
                                        </td>
                                        <td>
                                            ALL ENCLOSED SPACE ENTRIES ARE TO BE STRICTLY CONTROLLED, IN FULL ACCORDANCE WITH
                                            THE REQUIREMENTS OF SAFETY MANUAL 202, “ENCLOSED SPACE ENTRY PROCEDURES”, AND WITH
                                            DUE REGARD TO THE PROCEDURES GIVEN IN ECM 205, FOR RESCUE FROM HOLD / TANK / ENGINE
                                            ROOM.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30px">
                                        </td>
                                        <td>
                                            BECAUSE A SPACE HAS BEEN CERTIFIED SAFE, IT DOES NOT MEAN IT WILL REMAIN SO, PARTICULARLY
                                            IF HOT WORK IS BEING CARRIED OUT.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30px">
                                        </td>
                                        <td>
                                            THE ATMOSPHERIC CONDITION OF A SPACE SHOULD BE MONITORED THROUGHOUT THE ENTRY, TO
                                            ENSURE SAFETY OF THE PERSONNEL AND THE OPERATION.
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
        </div>
    </div>
</asp:Content>
