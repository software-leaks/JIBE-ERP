<%@ Page Title="A/E Monthly Running Hrs Report" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="DrugAndAlcoholPolicyDeclaration.aspx.cs" Inherits="DrugAndAlcoholPolicyDeclaration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" class="page-title">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr class="eform-report-header">
                <td style="width: 33%;">
                </td>
                <td style="width: 34%; text-align: center;">
                    <asp:Label ID="lblPageTitle" runat="server" Text="DRUG AND ALCOHOL POLICY DECLARATION"></asp:Label>
                </td>
                <td style="width: 33%; text-align: right;">
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
                                <table class="eform-report-table" cellpadding="2" border="1">
                                 <tr>
                                        <td colspan="3">
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            I SHALL ABIDE BY THE COMPANY'S DRUG AND ALCOHOL POLICY, PROCEDURES AND PRACTICES.
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width:20%">
                                            Rank/Name
                                        </td>
                                        <td style="width:5%">
                                            :
                                        </td>
                                        <td class="eform-field-data" style="width:75%">
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Crew_Full_NAME")%>'></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            Vessel Name
                                        </td>
                                        <td style="width: 15px">
                                            :
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Date signed on
                                        </td>
                                        <td style="width: 15px">
                                            :
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="lblVessel" runat="server" Text='<%#Eval("Date_signed_on")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Mouthpiece issue to seafarer
                                        </td>
                                        <td style="width: 15px">
                                            :
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Mouthpiece_issue")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Name
                                        </td>
                                        <td style="width: 15px">
                                            :
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label15" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Date
                                        </td>
                                        <td style="width: 15px">
                                            :
                                        </td>
                                        <td class="eform-field-data">
                                            <asp:Label ID="Label16" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Node
                                        </td>
                                        <td style="width: 15px">
                                            :
                                        </td>
                                        <td class="eform-field-data">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            1) All staff shall read, understand and support the objectives of the DNA policy.
                                            <br>
                                            </br> 2) The Master shall ensure that this Drug and Policy Declaration is completed
                                            every time a seafarer joins the ship. 
                                            <br> </br> 3) File a copy of this signed statement
                                            onboard. Hand the original to the seafarer.<br></br> 4) Mouthpiece issued to a seafarer
                                            for their own personal use only and seafarer is responsible for safe keeping of
                                            the item during His/her tenure onboard the ship.
                                        </td>
                            </td>
                        </tr>
                    </table>
                    </td> </tr> </table>
                </ItemTemplate>
            </asp:FormView>
        </div>
    </div>
</asp:Content>
