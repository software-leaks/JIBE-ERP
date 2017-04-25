<%@ Page Title="Pre-Joining Checklist" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="PreJoiningChecklist.aspx.cs" Inherits="Crew_PreJoiningChecklist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function PrintDiv(dvID) {

            var a = window.open('', '', 'left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
            a.document.write(document.getElementById(dvID).innerHTML);
            a.document.close();
            a.focus();
            a.print();
            a.close();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="pageTitle" style="background-color: gray; color: White; font-size: 12px;
                text-align: center; padding: 2px; font-weight: bold;">
                <asp:Label ID="lblPageTitle" runat="server" Text="JOINING CHECKLIST (AGENT)"></asp:Label>
            </div>
            <div class="error-message">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>
            <div id="page-content" style="border: 1px solid gray; z-index: -2; overflow: auto;
                text-align: center;">
                <asp:HiddenField ID="hdnCrewID" runat="server" Value="0" />
                <div>
                </div>
                <center>
                    <div style="text-align: left; width: 90%;">
                        <table cellspacing="0" cellpadding="4" rules="rows" style="background-color: White;
                            border-color: #336666; border: 1px solid gray; width: 100%; border-collapse: collapse;
                            margin: 5px;">
                            <tr style="color: White; background-color: #336666; font-weight: bold;">
                                <td style="text-align: center; height: 30px; font-weight: bold; font-size: 14px;">
                                    <span id="pgHeader">CHECK LIST</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <u>SECTION B</u><br />
                                    INSTRUCTIONS FOR COMPLETION: All questions to be answered. Questions that are not
                                    applicable are to be answered as NA. If a question is answered in the negative,
                                    a reason is to be given.
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlViewCheckList" runat="server" Visible="false">
                                        <table id="tblCheckList" cellspacing="1" cellpadding="4" style="border-color: #efefef; border-collapse: collapse;border-width:1px;"
                                            border="1">
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    QUESTION
                                                </td>
                                                <td>
                                                    YES/NO
                                                </td>
                                                <td>
                                                    COMMENTS
                                                </td>
                                                <td>
                                                    REMARKS
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    1.
                                                </td>
                                                <td>
                                                    HAS THE SEAFARER FILLED OUT FORM HR-001 (OFFICERS/CREW LICENSES/CERTIFICATES UPDATE)
                                                    AND CROSS CHECKED AGAINST STCW78/95 REQUIREMENTS?
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAns1" Text="Yes" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRemarks1" Text="Yes" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    2.
                                                </td>
                                                <td>
                                                    DOES THE SEAFARER HAVE THE NECESSARY EXPERIENCE AND TRAINING TO SAFELY SERVE UPON
                                                    THE ASSIGNED VESSEL?
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAns2" Text="Yes" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRemarks2" Text="Yes" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    3.
                                                </td>
                                                <td>
                                                    HAS THE SEAFARER PASSED A MEDICAL EXAMINATION FOR FITNESS, HEARING AND EYESIGHT
                                                    WITHIN LAST YEAR? (FORM HR-011 COMPLETED) DID EXAMINATION INCL. DRUG & ALCOHOL TESTING?
                                                    DID EXAMINATION INCL. HIV TESTING (AIDS)? ORIGINAL TO BE HANDED OVER TO MASTER.
                                                    COPY TO BE SENT TO OFFICE.
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAns3" Text="Yes" />
                                                </td>
                                                <td>
                                                    IF NO, IT MUST BE DONE.IF YES, DATE
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRemarks3" Text="Yes" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    4.
                                                </td>
                                                <td>
                                                    HAS THE MASTER/OFFICER READ THE FLEET STANDING INSTRUCTIONS MANUAL?
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAns4" Text="Yes" />
                                                </td>
                                                <td>
                                                    IF YES, MASTER/OFFICER TO SIGN
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRemarks4" Text="Yes" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    5.
                                                </td>
                                                <td>
                                                    HAS THE MASTER/OFFICER READ RELEVANT SECTIONS OF THE QUALITY POLICY/PROCEDURES MANUALS?
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAns5" Text="Yes" />
                                                </td>
                                                <td>
                                                    IF YES, MASTER/OFFICER TO SIGN
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRemarks5" Text="Yes" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    6.
                                                </td>
                                                <td>
                                                    HAS THE MASTER/OFFICER READ THE CIRCULAR AND INCIDENT FILES?
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAns6" Text="Yes" />
                                                </td>
                                                <td>
                                                    IF YES, MASTER/OFFICER TO SIGN
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRemarks6" Text="Yes" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    7.
                                                </td>
                                                <td>
                                                    HAS THE MANAGEMENT’S QUALITY & SAFETY POLICY BEEN READ BY THE MASTER/OFFICER AND
                                                    EXPLAINED TO THE CREW MEMBER?
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAns7" Text="Yes" />
                                                </td>
                                                <td>
                                                    IF YES, MASTER/OFFICER TO SIGN
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRemarks7" Text="Yes" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    8.
                                                </td>
                                                <td>
                                                    HAS THE SEAFARER SIGNED AN AGREEMENT OF EMPLOYMENT (HR-010)?
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAns8" Text="Yes" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRemarks8" Text="Yes" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    9.
                                                </td>
                                                <td>
                                                    HAS THE SEAFARER SIGNED A LETTER OF UNDERTAKING (HR-008) AND OIL POLLUTION DECLARATION
                                                    OF UNDERTAKING (HR-009)?
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAns9" Text="Yes" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRemarks9" Text="Yes" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    10.
                                                </td>
                                                <td>
                                                    HAS THE SEAFARER BEEN BRIEFED ON THE OWNERS STRICT DRUG AND ALCOHOL POLICY AND SIGNED
                                                    THE DRUG AND ALCOHOL ABUSE STATEMENT (HR-007)?
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAns10" Text="Yes" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRemarks10" Text="Yes" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    11.
                                                </td>
                                                <td>
                                                    DO FITTERS, BOSUNS AND COOKS FULLY UNDERSTAND THAT OVERTIME IS CONSOLIDATED INTO
                                                    THEIR SALARY?
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAns11" Text="Yes" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRemarks11" Text="Yes" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    12.
                                                </td>
                                                <td>
                                                    HAS OFFICER OR RATING SUFFICIENT WORKING KNOWLEDGE OF ENGLISH TO EFFECTIVELY UNDERSTAND
                                                    ORDERS AND COMMUNICATE IN HIS CAPACITY? (HR-003 COMPLETED)
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAns12" Text="Yes" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRemarks12" Text="Yes" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    13.
                                                </td>
                                                <td>
                                                    HAS OFFICER OR RATING ADEQUATE KNOWLEDGE OF MARPOL REQUIREMENTS? (HR-002 COMPLETED)
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblAns13" Text="Yes" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRemarks13" Text="Yes" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlEditCheckList" runat="server">
                                        <table cellspacing="1" cellpadding="4" style="border-color: #efefef; border-collapse: collapse;"
                                            border="1">
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    QUESTION
                                                </td>
                                                <td>
                                                    YES/NO
                                                </td>
                                                <td>
                                                    COMMENTS
                                                </td>
                                                <td>
                                                    REMARKS
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    1.
                                                </td>
                                                <td>
                                                    HAS THE SEAFARER FILLED OUT FORM HR-001 (OFFICERS/CREW LICENSES/CERTIFICATES UPDATE)
                                                    AND CROSS CHECKED AGAINST STCW78/95 REQUIREMENTS?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList runat="server" ID="rdoQ1">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtRemark1" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    2.
                                                </td>
                                                <td>
                                                    DOES THE SEAFARER HAVE THE NECESSARY EXPERIENCE AND TRAINING TO SAFELY SERVE UPON
                                                    THE ASSIGNED VESSEL?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList runat="server" ID="rdoQ2">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtRemark2" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    3.
                                                </td>
                                                <td>
                                                    HAS THE SEAFARER PASSED A MEDICAL EXAMINATION FOR FITNESS, HEARING AND EYESIGHT
                                                    WITHIN LAST YEAR? (FORM HR-011 COMPLETED) DID EXAMINATION INCL. DRUG & ALCOHOL TESTING?
                                                    DID EXAMINATION INCL. HIV TESTING (AIDS)? ORIGINAL TO BE HANDED OVER TO MASTER.
                                                    COPY TO BE SENT TO OFFICE.
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList runat="server" ID="rdoQ3">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    IF NO, IT MUST BE DONE.IF YES, DATE
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtRemark3" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    4.
                                                </td>
                                                <td>
                                                    HAS THE MASTER/OFFICER READ THE FLEET STANDING INSTRUCTIONS MANUAL?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList runat="server" ID="rdoQ4">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    IF YES, MASTER/OFFICER TO SIGN
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtRemark4" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    5.
                                                </td>
                                                <td>
                                                    HAS THE MASTER/OFFICER READ RELEVANT SECTIONS OF THE QUALITY POLICY/PROCEDURES MANUALS?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList runat="server" ID="rdoQ5">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    IF YES, MASTER/OFFICER TO SIGN
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtRemark5" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    6.
                                                </td>
                                                <td>
                                                    HAS THE MASTER/OFFICER READ THE CIRCULAR AND INCIDENT FILES?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList runat="server" ID="rdoQ6">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    IF YES, MASTER/OFFICER TO SIGN
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtRemark6" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    7.
                                                </td>
                                                <td>
                                                    HAS THE MANAGEMENT’S QUALITY & SAFETY POLICY BEEN READ BY THE MASTER/OFFICER AND
                                                    EXPLAINED TO THE CREW MEMBER?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList runat="server" ID="rdoQ7">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    IF YES, MASTER/OFFICER TO SIGN
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtRemark7" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    8.
                                                </td>
                                                <td>
                                                    HAS THE SEAFARER SIGNED AN AGREEMENT OF EMPLOYMENT (HR-010)?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList runat="server" ID="rdoQ8">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtRemark8" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    9.
                                                </td>
                                                <td>
                                                    HAS THE SEAFARER SIGNED A LETTER OF UNDERTAKING (HR-008) AND OIL POLLUTION DECLARATION
                                                    OF UNDERTAKING (HR-009)?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList runat="server" ID="rdoQ9">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtRemark9" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    10.
                                                </td>
                                                <td>
                                                    HAS THE SEAFARER BEEN BRIEFED ON THE OWNERS STRICT DRUG AND ALCOHOL POLICY AND SIGNED
                                                    THE DRUG AND ALCOHOL ABUSE STATEMENT (HR-007)?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList runat="server" ID="rdoQ10">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtRemark10" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    11.
                                                </td>
                                                <td>
                                                    DO FITTERS, BOSUNS AND COOKS FULLY UNDERSTAND THAT OVERTIME IS CONSOLIDATED INTO
                                                    THEIR SALARY?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList runat="server" ID="rdoQ11">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtRemark11" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    12.
                                                </td>
                                                <td>
                                                    HAS OFFICER OR RATING SUFFICIENT WORKING KNOWLEDGE OF ENGLISH TO EFFECTIVELY UNDERSTAND
                                                    ORDERS AND COMMUNICATE IN HIS CAPACITY? (HR-003 COMPLETED)
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList runat="server" ID="rdoQ12">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtRemark12" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    13.
                                                </td>
                                                <td>
                                                    HAS OFFICER OR RATING ADEQUATE KNOWLEDGE OF MARPOL REQUIREMENTS? (HR-002 COMPLETED)
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList runat="server" ID="rdoQ13">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtRemark13" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <div class="NoPrint">
                                        <style type="text/css" media="print">
                                            .NoPrint
                                            {
                                                display: none;
                                                
                                            }
                                            #pgHeader {color:Black;}
                                            #tblCheckList {border-width:1px;}
                                        </style>
                                        <asp:Button ID="btnSave" Text=" Save " runat="server" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" Text=" Close " runat="server" OnClientClick="window.close();" />
                                        <asp:Button ID="btnPrint" Text=" Print " runat="server" OnClick="btnPrint_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </center>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
