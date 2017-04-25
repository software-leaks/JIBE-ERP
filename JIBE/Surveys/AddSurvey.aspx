<%@ Page Title="Add Survey Details" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddSurvey.aspx.cs" Inherits="Surveys_AddSurvey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        body
        {
            color: black;
            font-family: Tahoma;
            font-size: 11px;
        }
        select
        {
            font-size: 12px;
            height: 21px;
        }
        
        #page-content a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 0px;
        }
        #page-content a:visited
        {
            color: black;
            text-decoration: none;
        }
        #page-content a:hover
        {
            color: blue;
            text-decoration: none;
        }
        .pager
        {
            font-size: 12px;
        }
        .pager span
        {
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
            color: Gray;
        }
        .pager a
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:visited
        {
            color: blue;
            background-color: white;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:hover
        {
            color: blue;
            background-color: #efefef;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 2px 0px 2px;
            font-weight: bold;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
        .data
        {
            background-color: #efefef;
            border: 1px solid #cccccc;
            height: 18px;
        }
    </style>
    <script type="text/javascript">
        function OpenFollowupDiv() {
            document.getElementById("dvAddFollowUp").style.display = "block";
            return false;
        }
        function CloseFollowupDiv() {
            var dvAddFollowUp = document.getElementById("dvAddFollowUp");
            dvAddFollowUp.style.display = 'none';

        }    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: bottom;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
        padding: 2px; background-color: #F6CEE3; text-align: center; font-size: 14px;
        font-weight: bold;">
        <div>
            Add Survey Details</div>
    </div>
    <div id="page-content" style="overflow: auto; border: 1px solid #cccccc; border-top: 0px;
        padding: 5px;">
        <asp:UpdatePanel ID="UpdatePanelFilter" runat="server">
            <ContentTemplate>
                <table cellpadding="0" cellspacing="0" style="width: 100%;" class="gradiant-css-blue">
                    <tr>
                        <td style="font-size: 22px;">
                           Add Survey Details
                        </td>
                        <td>
                            <asp:Button ID="btnMakeAsNA" runat="server" Height="40px" Text="Make Survey/Certificate as NA" />
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                </table>
                <table  cellpadding="0" cellspacing="0"  style="width: 100%">
                    <tr style="background-color:#C6D9F1">
                        <td>
                            <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                <tr>
                                    <td style="width:100px">
                                        Vessel:
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblVesselName" runat="server"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:100px">
                                        Category:
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblCategoryName" runat="server"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:100px">
                                        Survey:
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblCertificateName" runat="server"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:100px">
                                        Remarks:
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblCertificateRemarks" runat="server"/>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width:100px">
                                        Make/Model:
                                    </td>
                                    <td class="data">
                                        <asp:Label ID="lblMakeModel" runat="server" />
                                    </td>
                                </tr>
                                
                            </table>
                        </td>
                    </tr>
                    <tr  style="background-color:#ffffff">
                        <td>
                            <table border="0">
                                <tr>
                                    <td style="width: 120px">
                                        Date of Issue
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateOfIssue" runat="server" CssClass="required"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        Remarks
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;">
                                        Date of Expiry
                                    </td>
                                    <td style="vertical-align:top;">
                                        <asp:TextBox ID="txtDateOfExpiry" runat="server" CssClass="required"></asp:TextBox>
                                    </td>
                                    <td rowspan="2" colspan="2">
                                        <asp:TextBox ID="txtRemarks" runat="server" Width="300px" Height="80px" CssClass="required"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="vertical-align:top;text-align:right">
                                        <asp:CheckBox ID="chkNoExpiryDate" runat="server" Text="No Expiry Date?" />
                                    </td>                                    
                                </tr>                                
                                <tr>
                                    <td>
                                        Followup Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFollowupDate" runat="server" CssClass="required"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Followup Details:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtDetails" runat="server" Width="400px" Height="80px" CssClass="required"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                   
                </table>
                </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

