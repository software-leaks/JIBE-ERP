<%@ Page Title="ToolBox Meeting" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ToolboxMeeting.aspx.cs" Inherits="eForms_eFormTempletes_ToolboxMeeting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div class="page-title">
                <asp:Label ID="lblPageTitle" runat="server" Text="ToolBox Meeting"></asp:Label>
            </div>
            <div style="width: 100%; color: Black;">
                <table width="100%">
                    <tr>
                        <td style="width: 80px">
                            Vessel Name:
                        </td>
                        <td style="width: 200px" class="eform-field-data" align="left">
                            <asp:Label ID="lblVesselName" runat="server"></asp:Label>
                        </td>
                        <td style="width: 80px">
                            Report Date:
                        </td>
                        <td class="eform-field-data" align="left">
                            <asp:Label ID="lblReportDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td>
                                    <div>
                                        <asp:Repeater runat="server" ID="rpt1" OnItemDataBound="rpt1_ItemDataBound">
                                            <ItemTemplate>
                                                <table border="1" width="100%" cellpadding="5" cellspacing="0">
                                                    <tr>
                                                        <td width="20%" colspan="6" align="left" style="font-weight: bold">
                                                            <asp:Label ID="lblGroup_Desc" runat="server" Text='<%# Eval("TopicType")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #DADEE5; color: #333333; font-size: 11px; padding: 5px;
                                                        text-align: left; vertical-align: middle; border: 1px solid #959EAF;">
                                                        <td align="center" style="width: 6%;">
                                                            <strong>Sr no.</strong>
                                                        </td>
                                                        <td align="center" style="width: 36%;">
                                                            <strong>Activity</strong>
                                                        </td>
                                                        <td align="center" style="width: 16%;">
                                                            <strong>Environment</strong>
                                                        </td>
                                                        <td align="center" style="width: 16%;">
                                                            <strong>JHA</strong>
                                                        </td>
                                                        <td align="center" style="width: 16%;">
                                                            <strong>WP</strong>
                                                        </td>
                                                        <td align="center" style="width: 10%;">
                                                            <strong>Assessment Name</strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <%-- <td valign="top">--%>
                                                        <asp:Repeater runat="server" ID="rpt2">
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td width="6%" valign="top" style="color: Black;" align="center">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Rownum")%>'></asp:Label>
                                                                    </td>
                                                                    <td width="36%" valign="top" style="color: Black;" align="center">
                                                                        <asp:Label ID="lblFieldsName" runat="server" Visible="false" Text='<%# Eval("Activity")%>'></asp:Label>
                                                                        <asp:Label ID="lblFieldDesc" runat="server" Text='<%# Eval("Activity")%>'></asp:Label>
                                                                    </td>
                                                                    <td width="18%" valign="top" style="color: Black; height: 15px" align="center">
                                                                        <asp:Label ID="lblCurrentValue" runat="server" Text='<%# Eval("Environment")%>'></asp:Label>
                                                                    </td>
                                                                    <td width="18%" valign="top" style="color: Black; height: 15px" align="center">
                                                                        <asp:Label ID="lblNewValue" runat="server" Text='<%# Eval("JHA")%>'></asp:Label>
                                                                    </td>
                                                                    <td width="18%" valign="top" style="color: Black; height: 15px" align="center">
                                                                        <asp:Label ID="lblReason_For_Change" runat="server" Text='<%# Eval("WP")%>'></asp:Label>
                                                                    </td>
                                                                    <td width="10%" valign="top" style="color: Black; height: 15px" align="center">
                                                                        <asp:HyperLink ID="lblWork_Categ_NameView" runat="server" Text='<%#Eval("Assessment_Name")%>'
                                                                            NavigateUrl='<%# "~/JRA/RiskAssessmentDetails.aspx?Assessment_ID="+Eval("Assessment_ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString() %>'
                                                                            Target="_blank"></asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                                <%-- </tr>
                                                                            </table>--%>
                                                                <%--     
                                                                    <asp:Literal ID="litRowEnd" runat="server"></asp:Literal>--%>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                        <%-- </td>--%>
                                                    </tr>
                                                    <br />
                                                    <br />
                                                </table>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <%-- Label used for showing Error Message --%>
                                                <asp:Label ID="lblErrorMsg" runat="server" CssClass="errMsg" Text="NO RECORDS FOUND"
                                                    Visible="false">
                                                </asp:Label>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="1" cellspacing="1" style="border: 1px solid #cccccc; font-family: Tahoma;
                                        font-size: 12px; width: 100%;">
                                        <tr>
                                            <td style="width: 10%;" align="left">
                                                <strong>Signature :</strong>
                                            </td>
                                            <td style="width: 10%;" align="right">
                                                Master's Signature :
                                            </td>
                                            <td style="width: 15%;" align="left">
                                                <asp:Label ID="lblMasterSignature" Visible="false" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 10%;" align="right">
                                                Chief Officer :
                                            </td>
                                            <td style="width: 15%;" align="left">
                                                <asp:Label ID="lblchiefofficer" Visible="false" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 10%;" align="right">
                                                Chief Engineer :
                                            </td>
                                            <td style="width: 15%;" align="left">
                                                <asp:Label ID="lblchiefEngg" Visible="false" runat="server"></asp:Label>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="width: 100%;">
                                                <br />
                                                <strong>Environment :</strong> if any threat to environment,list additional precautions;
                                                <strong>JHA :</strong> Job hazard analysis;<strong>WP :</strong> Work Permit;<strong>RA
                                                    :</strong> Rist Assessment
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="1" cellspacing="1" style="border: 1px solid #cccccc; font-family: Tahoma;
                                        font-size: 12px; width: 100%;">
                                        <tr>
                                            <td style="width: 100%;">
                                                <asp:Image ImageUrl="~/Images/ToolBoxMeatingGuide.JPG" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
