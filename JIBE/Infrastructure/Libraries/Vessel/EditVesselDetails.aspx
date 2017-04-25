<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EditVesselDetails.aspx.cs"
    Inherits="EditVesselDetails" Title="Edit vessel details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .txtbox-css
        {
            width: 90px;
        }
        .dropdown-css
        {
            width: 95px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    

    <center>
            <div id="pageTitle" style="background-color: #15317E; color: White; font-size: 12px;
                text-align: center; padding: 2px; font-weight: bold;">
                 <asp:Label ID="lblShipName" runat="server" Font-Bold="true" Text='<%#Bind("Vessel_Short_Name") %>'></asp:Label>
            </div>        
           
        <div style="text-align: left">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
                <ContentTemplate>
                    <script type="text/javascript">
                        function ViewPopupId(querystring) {
                            javascript: window.open("mailto:" + querystring + "?subject=xx");
                        }
                        
                    </script>
                    <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                        border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                        <tr>
                            <td>
                                <asp:Image ID="ShipImg" runat="server" Height="193px" Width="400px" ImageUrl='<%# Bind("Vessel_Image") %>' /><br />
                                <br />
                                Upload New Image
                                <asp:FileUpload ID="ShipImageUpload" runat="server" />
                            </td>
                            <td>
                                <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                                    border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label76" runat="server" Text="Ex-Name"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtExName" runat="server" CssClass="txtbox-css" Text='<%#Bind("Vessel_Ex_Name1")%>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExName">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label8" runat="server" Text="Vessel Code :"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtVeseelCode" runat="server" CssClass="txtbox-css" Text='<%#Bind("Vessel_Short_Name")%>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtVeseelCode">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label2" runat="server" Text="A/C Code:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtAccCode" runat="server" CssClass="txtbox-css" Text='<%#Bind("Vessel_Code")%>'
                                                ReadOnly="True"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAccCode">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAccCode"
                                                ErrorMessage="A/C Code must be numeric" ValidationExpression="\d{0,15}">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblowner" runat="server" Text="Owner:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtVesselOwner" runat="server" TextMode="MultiLine" CssClass="txtbox-css"
                                                Text='<%#Bind("Vessel_Owner")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtVesselOwner">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lbloperator" runat="server" Text="Operator:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtOperator" runat="server" TextMode="MultiLine" CssClass="txtbox-css"
                                                Text='<%#Bind("Vessel_Operator")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtOperator">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label6" runat="server" Text="Flag:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DDLFlag" CssClass="dropdown-css" runat="server">
                                                <asp:ListItem Text="--Select--">
                                        
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DDLFlag">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label10" runat="server" Text="Call-sign:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtCallSign" runat="server" CssClass="txtbox-css" Text='<%# Bind("Vessel_Call_sign") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCallSign">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                                                ControlToValidate="txtCallSign" ErrorMessage="Call-Sign should not be special charecters"
                                                ValidationExpression="^[0-9a-zA-Z]+$">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label7" runat="server" Text="IMO No:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtIMO" runat="server" CssClass="txtbox-css" Text='<%# Bind("Vessel_IMO_No") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtIMO">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                                                ControlToValidate="txtIMO" ErrorMessage="A/C Code must be numeric" ValidationExpression="\d{0,15}">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="OffcialNo"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtOffcialNo" runat="server" CssClass="txtbox-css" Text='<%#Bind("Vessel_Official_No")%>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtOffcialNo">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server"
                                                ControlToValidate="txtOffcialNo" ErrorMessage="A/C Code must be numeric" ValidationExpression="\d{0,15}">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label14" runat="server" Text="Class:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DDLClass" CssClass="dropdown-css" runat="server">
                                                <asp:ListItem Text="--Select--" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="DDLClass">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label16" runat="server" Text="ClassNo:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtClassNo" runat="server" CssClass="txtbox-css" Text='<%# Bind("Vessel_Class_No") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtClassNo">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label18" runat="server" Text="Serv Speed:"></asp:Label>
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:TextBox ID="txtServSpeed" runat="server" CssClass="txtbox-css" Text='<%# Bind("Vessel_Serv_Speed") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtServSpeed">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtServSpeed"
                                                ErrorMessage="Serv Speed Must be numeric" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                                    border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label13" runat="server" Text="Type "></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DDLVesselType" runat="server" CssClass="dropdown-css">
                                                <asp:ListItem Selected="True" Text="--Select--"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="DDLVesselType">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label33" runat="server" Text="Size"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DDLTankSize" runat="server" CssClass="dropdown-css">
                                                <asp:ListItem Selected="True" Text="--Select--"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="DDLTankSize">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label17" runat="server" Text="Dt Keel laid" CssClass="txtbox-css"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtKeellaiddt" runat="server" Text='<%# Bind("Vessel_keel_laid_Date","{0:dd/MM/yyyy}") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtKeellaiddt">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtKeellaiddt"
                                                ErrorMessage="Date Must be MM/DD/YYYY" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label19" runat="server" Text="Date delivered" CssClass="txtbox-css"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDlvrydt" runat="server" Text='<%# Bind("Vessel_Delvry_Date","{0:dd/MM/yyyy}") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtDlvrydt">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDlvrydt"
                                                ErrorMessage="Date Must be MM/DD/YYYY" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label70" runat="server" Text="Yard"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtyard" runat="server" Text='<%# Bind("Vessel_Yard") %>' CssClass="txtbox-css"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtyard">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label71" runat="server" Text="Hull No"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtHullNo" runat="server" Text='<%# Bind("Vessel_Hull_No") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtHullNo">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtHullNo"
                                                ErrorMessage="Hull NO must be numeric" ValidationExpression="\d{0,15}">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label35" runat="server" Text="Hull Type"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;<asp:DropDownList ID="DDLHullType" runat="server">
                                                <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="DDLHullType">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label72" runat="server" Text="Length(OA)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLength" runat="server" Text='<%# Bind("Vessel_Length_OA") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtLength">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server"
                                                ControlToValidate="txtLength" ErrorMessage="Length Must be numeric" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label73" runat="server" Text="Depth"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDepth" runat="server" Text='<%# Bind("Vessel_Depth") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtDepth">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server"
                                                ControlToValidate="txtDepth" ErrorMessage="Depth Must be numeric" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label74" runat="server" Text="Length(BP)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBpLength" runat="server" Text='<%# Bind("Vessel_Length_BP") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtBpLength">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server"
                                                ControlToValidate="txtBpLength" ErrorMessage="Length(BP)  Must be numeric" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label127" runat="server" Text="Breadth(moulded)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBreadth" runat="server" Text='<%# Bind("Vessel_Breadth") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtBreadth">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server"
                                                ControlToValidate="txtBreadth" ErrorMessage="Breadth Must be numeric" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label75" runat="server" Text="Mast Top from Keel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMastTopfrmKeel" runat="server" Text='<%# Bind("Vessel_Mast_Top_Keel") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtMastTopfrmKeel">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server"
                                                ControlToValidate="txtMastTopfrmKeel" ErrorMessage="Mast Top from Keel Must be numeric"
                                                ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                                    border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label77" runat="server" Text=" "></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label78" runat="server" Font-Bold="true" Text="Telex"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Font-Bold="true" Text="Telephone"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label21" runat="server" Font-Bold="true" Text="Fax"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label22" runat="server" Font-Bold="true" Text="Data"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label79" runat="server" Text="Inmarsat-A:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox5" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox6" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label3" runat="server" Text="Inmarsat-B:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox7" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox8" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox9" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox10" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label4" runat="server" Text="Inmarsat-M:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox11" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox12" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox13" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox14" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label81" runat="server" Text="Inmarsat-A:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox15" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox16" runat="server" CssClass="txtbox-css"></asp:TextBox>&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox17" runat="server" CssClass="txtbox-css"></asp:TextBox>&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox18" runat="server" CssClass="txtbox-css"></asp:TextBox>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label82" runat="server" Text="Email-Adds"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtemail" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label83" runat="server" Text="MMIS No"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtMMSI">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtMMSI"
                                                ErrorMessage="MMSI Must be 9 digits number" ValidationExpression="\d{9}">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMMSI" runat="server" CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Image ID="TankImg" runat="server" Height="127px" Width="400px" /><br />
                                <br />
                                Upload New Image
                                <asp:FileUpload ID="TankImageUpload" runat="server" />
                            </td>
                            <td style="vertical-align: top;">
                                <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                                    border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td align="left" colspan="2">
                                            <asp:Label ID="Label84" runat="server" Text="Cargo Tk capacity-98%(including Slops)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCargoTkCap" runat="server" Text='<%# Bind("Vessel_Cargo_Tk_Cap") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtCargoTkCap">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtCargoTkCap"
                                                ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td align="left">
                                            <b>cub.m</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            <asp:Label ID="Label86" runat="server" Text="Slop Tank cap-98%"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSloptkCap" runat="server" Text='<%# Bind("Vessel_Slop_Tk_Cap") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtSloptkCap">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtSloptkCap"
                                                ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td align="left">
                                            <b>cub.m</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            <asp:Label ID="Label88" runat="server" Text="Ballast Tk cap-100%"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBallastTkCap" runat="server" Text='<%# Bind("Vessel_Ballast_Tk_Cap") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtBallastTkCap">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtBallastTkCap"
                                                ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td align="left">
                                            <b>cub.m</b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align:top">
                                <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                                    border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label106" runat="server" Text=" "></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="SalutationLabel" runat="server" Font-Bold="true" Text="International"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label107" runat="server" Font-Bold="true" Text="Suez"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label34" runat="server" Font-Bold="true" Text="Panama"></asp:Label>
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label108" runat="server" Text="GRT"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txgrttInter" runat="server" Text='<%# Bind("Grt_International") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txgrttInter">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                ControlToValidate="txgrttInter" ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtGrtSuez" runat="server" Text='<%# Bind("Grt_Suez") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtGrtSuez">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                                ControlToValidate="txtGrtSuez" ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtGrtpanama" runat="server" Text='<%# Bind("Grt_Panama") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtGrtpanama">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                ControlToValidate="txtGrtpanama" ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <b>MT</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label112" runat="server" Text="NRT"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNrtInter" runat="server" Text='<%# Bind("NRT_International") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtNrtInter">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                                                ControlToValidate="txtNrtInter" ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNrtSuez" runat="server" Text='<%# Bind("NRT_Suez") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txtNrtSuez">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                                                ControlToValidate="txtNrtSuez" ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNrtPanama" runat="server" Text='<%# Bind("NRT_Panama") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtNrtPanama">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                                ControlToValidate="txtNrtPanama" ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <b>MT</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label116" runat="server" Text="LWT"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLwtInter" runat="server" Text='<%# Bind("LWT_International") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtLwtInter">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server"
                                                ControlToValidate="txtLwtInter" ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtLwtSuez" runat="server" Text='<%# Bind("LWT_Suez") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txtLwtSuez">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator26" runat="server"
                                                ControlToValidate="txtLwtSuez" ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLwtPanama" runat="server" Text='<%# Bind("LWT_Panama") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtLwtPanama">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server"
                                                ControlToValidate="txtLwtPanama" ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <b>MT</b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                                    border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label90" runat="server" Text=" "></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label91" runat="server" Font-Bold="true" Text="Tropical"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label92" runat="server" Font-Bold="true" Text="Summer"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label93" runat="server" Font-Bold="true" Text="Winter"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label37" runat="server" Font-Bold="true" Text="Ballast"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label94" runat="server" Text="DWT"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDw_Trop" runat="server" Text='<%# Bind("Dwt_Tropical") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtDw_Trop">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator28" runat="server"
                                                ControlToValidate="txtDw_Trop" ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDw_Summ" runat="server" Text='<%# Bind("Dwt_Summer") %>' CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_Dw_wint" runat="server" Text='<%# Bind("Dwt_winter") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="txt_Dw_wint">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDw_Ballast" runat="server" Text='<%# Bind("Dwt_Ballast") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="txtDw_Ballast">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <b>MT</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label98" runat="server" Text="Displ"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDisp_Trop" runat="server" Text='<%# Bind("Disp_Tropical") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txtDisp_Trop">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator29" runat="server"
                                                ControlToValidate="txtDisp_Trop" ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDisp_summ" runat="server" Text='<%# Bind("Disp_Summer") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="txtDisp_summ">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDisp_Winter" runat="server" Text='<%# Bind("Disp_winter") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="txtDisp_Winter">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDisp_Ballast" runat="server" Text='<%# Bind("Disp_Ballasr") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ControlToValidate="txtDisp_Ballast">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <b>MT</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label102" runat="server" Text="Draft"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtdrft_trop" runat="server" Text='<%# Bind("Draft_Tropical") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" ControlToValidate="txtdrft_trop">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server"
                                                ControlToValidate="txtdrft_trop" ErrorMessage="Must be digits" ValidationExpression="^[0-9]+\.?[0-9]*$">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtdrft_summ" runat="server" Text='<%# Bind("Draft_Summer") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" ControlToValidate="txtdrft_summ">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtdrft_wint" runat="server" Text='<%# Bind("Draft_winter") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" ControlToValidate="txtdrft_wint">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtdrft_Ballast" runat="server" Text='<%# Bind("Draft_Ballast") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" ControlToValidate="txtdrft_Ballast">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <b>mtr</b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <b><i>Machinery particulars</i></b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                                    border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label1" runat="server" Text="Main Engine"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtME" runat="server" Text='<%# Bind("Vessel_MainEngine") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ControlToValidate="txtME">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label120" runat="server" Text="Aux.Boiler"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAuxBoil" runat="server" Text='<%# Bind("Vessel_aux_Boiler") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ControlToValidate="txtAuxBoil">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label121" runat="server" Text="MCR"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMCR" runat="server" Text='<%# Bind("Vessel_ME_MCR") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" ControlToValidate="txtMCR">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label38" runat="server" Text="Capacity@Wrkg prs x Nos"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAuxKw" runat="server" Text='<%# Bind("Vessel_ABLR_Cap") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" ControlToValidate="txtAuxKw">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label123" runat="server" Text="NCR"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNCR" runat="server" Text='<%# Bind("Vessel_ME_NCR") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ControlToValidate="txtNCR">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label39" runat="server" Text="COPs-Capacity x Nos"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCopCap" runat="server" Text='<%# Bind("Vessel_Cops_Cap") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" ControlToValidate="txtCopCap">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label125" runat="server" Text="Aux Engine"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAunEng" runat="server" Text='<%# Bind("Aux_Engine") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="txtAunEng">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label41" runat="server" Text="Deck Machinery"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDLDeckMech" runat="server" CssClass="dropdown-css">
                                                <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ControlToValidate="DDLDeckMech">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label47" runat="server" Text="KW x Nos"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAuxEngKw" runat="server" Text='<%# Bind("Vessel_AE_KW") %>' CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="txtAuxEngKw">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label49" runat="server" Font-Bold="true" Text="Last"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label50" runat="server" Font-Bold="true" Text="Next"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label53" runat="server" Font-Bold="true" Text="Latest"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label51" runat="server" Text="Turbine Generator"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtturbEng" runat="server" Text='<%# Bind("Vessel_turb_Genrt") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ControlToValidate="txtturbEng">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label52" runat="server"  Text="Dry dock"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDrtLast" runat="server" Text='<%# Bind("Dry_Dock_Last","{0:dd/MM/yyyy}") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ControlToValidate="txtDrtLast">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator31" runat="server"
                                                ControlToValidate="txtKeellaiddt" ErrorMessage="Date Must be MM/DD/YYYY" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDryNext" runat="server" Text='<%# Bind("Dry_Dock_Next","{0:dd/MM/yyyy}") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDryLatest" runat="server" Text='<%# Bind("Dry_Dock_Latest","{0:dd/MM/yyyy}") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label55" runat="server" Text="KW x Nos"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtturbengKw" runat="server" Text='<%# Bind("Vessel_TG_KW") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ControlToValidate="txtturbengKw">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label59" runat="server" Text="Special survey"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSplLast" runat="server" Text='<%# Bind("Spl_Svry_Last","{0:dd/MM/yyyy}") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="txtSplLast">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator33" runat="server"
                                                ControlToValidate="txtSplLast" ErrorMessage="Date Must be MM/DD/YYYY" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSplNext" runat="server" Text='<%# Bind("Spl_Svry_Next","{0:dd/MM/yyyy}") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSplLatest" runat="server" Text='<%# Bind("Spl_Svry_Latest","{0:dd/MM/yyyy}") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="Label66" runat="server"  Text="Tailshaft survey"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTailLast" runat="server" Text='<%# Bind("Tailshft_Svry_Last","{0:dd/MM/yyyy}") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server" ControlToValidate="txtTailLast">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator35" runat="server"
                                                ControlToValidate="txtTailLast" ErrorMessage="Date Must be MM/DD/YYYY" ValidationExpression="(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d">*</asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTailNext" runat="server" Text='<%# Bind("Tailshft_Svry_Next","{0:dd/MM/yyyy}") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTailLatest" runat="server" Text='<%# Bind("Tailshft_Svry_Latest","{0:dd/MM/yyyy}") %>'
                                                CssClass="txtbox-css"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="text-align:center">
                            <td colspan="2">
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </center>
</asp:Content>
