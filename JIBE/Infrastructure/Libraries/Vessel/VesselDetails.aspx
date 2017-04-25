<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="VesselDetails.aspx.cs"
    Inherits="VesselDetails" Title="Vessel Particulars" %>
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
        <div id="page-header">
            Vessel Particulars for
            <asp:Label ID="lblShipName" runat="server" Text='<%#Bind("Vessel_Short_Name") %>'></asp:Label><br />
        </div>
        <div style="text-align: left">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <script type="text/javascript">
                        function ViewPopupId(querystring) {
                            javascript: window.open("mailto:" + querystring + "?subject=xx");

                        }
    
                    </script>
                    <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                        border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                        <tr>
                            <td style="width: 70%; height: 359px; border-right: solid; border-right-color: Gray;
                                border-right-width: 1px" align="left" valign="top">
                                <asp:Image ID="ShipImg" runat="server" Height="193px" Width="457px" ImageUrl='<%# Bind("Vessel_Image") %>' /><br />
                                <br />
                                <asp:FormView ID="FormView3" runat="server" DataKeyNames="Vessel_Code" Height="133px"
                                    Width="100%">
                                    <ItemTemplate>
                                        <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                        border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td align="left" style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label1"  runat="server" Text="Type "></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 19px;">
                                                    <asp:Label ID="SalutationLabel" runat="server" Text='<%# Bind("Vessel_Type") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label33"  runat="server" Text="Size"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label34" runat="server" Text='<%# Bind("Vessel_Size") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%; height: 20px;">
                                                    <asp:Label ID="Label8"  runat="server" Text="Dt Keel laid"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 25px;">
                                                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Bind("Vessel_keel_laid_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 20px;">
                                                    <asp:Label ID="Label2"  runat="server" Text="Date delivered"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 25px;">
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("Vessel_Delvry_Date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%; height: 21px">
                                                    <asp:Label ID="Label3"  runat="server" Text="Yard"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 21px" colspan="3">
                                                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Bind("Vessel_Yard") %>'></asp:Label>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label4"  runat="server" Text="Hull No"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Job_TitleLabel" runat="server" Text='<%# Bind("Vessel_Hull_No") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label35"  runat="server" Text="Hull Type"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label36" runat="server" Text='<%# Bind("Vessel_Hull_Type") %>'></asp:Label>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%">
                                                    <asp:Label ID="Label6"  runat="server" Text="Length(OA)"></asp:Label>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:Label ID="EmployerLabel" runat="server" Text='<%# Bind("Vessel_Length_OA") %>'></asp:Label><b>
                                                        mtr</b>
                                                </td>
                                                <td style="width: 25%" align="left">
                                                    <asp:Label ID="Label10"  runat="server" Text="Depth (moulded)"></asp:Label>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("Vessel_Depth") %>'></asp:Label><b>
                                                        mtr</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Label7"  runat="server" Text="Length(BP)"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Dept_CodeLabel" runat="server" Text='<%# Bind("Vessel_Length_BP") %>'></asp:Label><b>
                                                        mtr</b>
                                                </td>
                                                <td style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Label12"  runat="server" Text="Breadth (moulded)"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("Vessel_Breadth") %>'></asp:Label><b>
                                                        mtr</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%; height: 23px;">
                                                </td>
                                                <td style="width: 25%; height: 23px;">
                                                </td>
                                                <td style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Label16"  runat="server" Text="Mast-top from keel"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Label17" runat="server" Text='<%# Bind("Vessel_Mast_Top_Keel") %>'></asp:Label><b>
                                                        mtr</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:FormView>
                            </td>
                            <td align="left" style="width: 80%; height: 359px;" valign="top">
                                <asp:FormView ID="FormView1" runat="server" DataKeyNames="Vessel_Code" Height="133px"
                                    Width="473px">
                                    <ItemTemplate>
                                        <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                        border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td align="left" style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label1"  runat="server" Text="Ex-Name"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 19px;" colspan="3">
                                                    <asp:Label ID="SalutationLabel" runat="server" Text='<%# Bind("Vessel_Ex_Name1") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 20%; height: 20px;">
                                                    <asp:Label ID="Label8"  runat="server" Text="Vessel Code :"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 20px;">
                                                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Bind("Vessel_Short_Name") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 20%; height: 20px;">
                                                    <asp:Label ID="Label2"  runat="server" Text="A/C Code:"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 20px;">
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("Vessel_Code") %>'></asp:Label>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%; height: 21px">
                                                    <asp:Label ID="Label3"  runat="server" Text="Owner:"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 21px" colspan="3">
                                                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Bind("vessel_Owner") %>'></asp:Label>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label4"  runat="server" Text="Operator:"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 19px;" colspan="3">
                                                    <asp:Label ID="Job_TitleLabel" runat="server" Text='<%# Bind("Vessel_Operator") %>'></asp:Label>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%">
                                                    <asp:Label ID="Label6"  runat="server" Text="Flag:"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%">
                                                    <asp:Label ID="EmployerLabel" runat="server" Text='<%# Bind("Vessel_Flag") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%">
                                                    <asp:Label ID="Label10"  runat="server" Text="Call-sign:"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%">
                                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("Vessel_Call_sign") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Label7"  runat="server" Text="IMO No:"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Dept_CodeLabel" runat="server" Text='<%# Bind("Vessel_IMO_No") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Label12"  runat="server" Text="Offcial-No"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("Vessel_Official_No") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Label14" runat="server" Text="Class:"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Label15" runat="server" Text='<%# Bind("Vessel_Class") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Label16"  runat="server" Text="Class-No:"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Label17" runat="server" Text='<%# Bind("Vessel_Class_No") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%; height: 23px;">
                                                    <asp:Label ID="Label18"  runat="server" Text="Serv Speed:"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 23px;" colspan="3">
                                                    <asp:Label ID="Label19" runat="server" Text='<%# Bind("Vessel_Serv_Speed") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:FormView>
                                &nbsp;<br />
                                <asp:Table ID="Table1" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="1px">
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                        border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                        <tr>
                            <td style="width: 641px; height: 83px; border-right: solid; border-right-color: Gray;
                                border-right-width: 1px" align="left" valign="top">
                                <asp:Image ID="TankImg" runat="server" Height="127px" Width="459px" />
                            </td>
                            <td style="height: 83px">
                                <asp:FormView ID="FormView5" runat="server" DataKeyNames="Vessel_Code" Height="106px"
                                    Width="461px">
                                    <ItemTemplate>
                                        <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                        border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td align="left" style="width: 25%; height: 19px;" colspan="2">
                                                    <asp:Label ID="Label1"  runat="server" Text="Cargo Tk capacity-98%(including Slops)"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;" colspan="2">
                                                    <asp:Label ID="SalutationLabel" runat="server" Text='<%# Bind("Vessel_Cargo_Tk_Cap") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 19px;">
                                                </td>
                                                <td align="left">
                                                    <b>cub.m</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%; height: 20px;" colspan="2">
                                                    <asp:Label ID="Label8"  runat="server" Text="Slop Tank cap-98%"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 20px;" colspan="2">
                                                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Bind("Vessel_Slop_Tk_Cap") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 19px;">
                                                </td>
                                                <td align="left">
                                                    <b>cub.m</b>
                                                </td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 25%; height: 21px" colspan="2">
                                                    <asp:Label ID="Label3"  runat="server" Text="Ballast Tk cap-100%"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 21px" colspan="2">
                                                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Bind("Vessel_Ballast_Tk_Cap") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 19px;">
                                                </td>
                                                <td align="left">
                                                    <b>cub.m</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:FormView>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 641px; height: 108px">
                                <asp:FormView ID="FormView4" runat="server" DataKeyNames="Vessel_Code" Height="89px"
                                    Width="461px">
                                    <ItemTemplate>
                                        <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                        border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td align="left" style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
                                                </td>
                                                <td style="width: 20%; height: 19px;">
                                                    <asp:Label ID="SalutationLabel" runat="server" Text="Tropical"></asp:Label>
                                                </td>
                                                <td style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label33" runat="server" Text="Summer"></asp:Label>
                                                </td>
                                                <td style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label34" runat="server" Text="Winter"></asp:Label>
                                                </td>
                                                <td style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label37" runat="server" Text="Ballast"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 20%; height: 20px;">
                                                    <asp:Label ID="Label8"  runat="server" Text="DWT"></asp:Label>
                                                </td>
                                                <td style="width: 20%; height: 20px;">
                                                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Bind("Dwt_Tropical") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 20%; height: 20px;">
                                                    <asp:Label ID="Label38" runat="server" Text='<%# Bind("Dwt_Summer") %>'></asp:Label>
                                                </td>
                                                <td style="width: 20%; height: 20px;">
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("Dwt_winter") %>'></asp:Label>
                                                </td>
                                                </td>
                                                <td style="width: 20%; height: 20px;">
                                                    <asp:Label ID="Label42" runat="server" Text='<%# Bind("Dwt_Ballast") %>'></asp:Label>
                                                </td>
                                                </td>
                                                <td style="width: 20%; height: 20px;">
                                                    <b>MT</b>
                                                </td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 20%; height: 21px">
                                                    <asp:Label ID="Label3"  runat="server" Text="Displ"></asp:Label>
                                                </td>
                                                <td style="width: 20%; height: 21px">
                                                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Bind("Disp_Tropical") %>'></asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="Label39" runat="server" Text='<%# Bind("Disp_Summer") %>'></asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                    <asp:Label ID="Label40" runat="server" Text='<%# Bind("Disp_winter") %>'></asp:Label>
                                                </td>
                                                <td style="width: 20%; height: 20px;">
                                                    <asp:Label ID="Label43" runat="server" Text='<%# Bind("Disp_Ballasr") %>'></asp:Label>
                                                </td>
                                                </td>
                                                <td style="width: 20%; height: 20px;">
                                                    <b>MT</b>
                                                </td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label4"  runat="server" Text="Draft"></asp:Label>
                                                </td>
                                                <td style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Job_TitleLabel" runat="server" Text='<%# Bind("Draft_Tropical") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label41" runat="server" Text='<%# Bind("Draft_Summer") %>'></asp:Label>
                                                </td>
                                                <td style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label36" runat="server" Text='<%# Bind("Draft_winter") %>'></asp:Label>
                                                </td>
                                                <td style="width: 20%; height: 20px;">
                                                    <asp:Label ID="Label44" runat="server" Text='<%# Bind("Draft_Ballast") %>'></asp:Label>
                                                </td>
                                                </td>
                                                <td style="width: 20%; height: 20px;">
                                                    <b>mtr</b>
                                                </td>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:FormView>
                            </td>
                            <td style="width: 641px; height: 108px">
                                <asp:FormView ID="FormView6" runat="server" DataKeyNames="Vessel_Code" Height="89px"
                                    Width="461px">
                                    <ItemTemplate>
                                        <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                        border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td align="left" style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="SalutationLabel"  runat="server" Text="International"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label33"  runat="server" Text="Suez"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label34"  runat="server" Text="Panama"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 19px;">
                                                    <b>MT</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 20%; height: 20px;">
                                                    <asp:Label ID="Label8"  runat="server" Text="GRT"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 20px;">
                                                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Bind("Grt_International") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 20px;">
                                                    <asp:Label ID="Label38" runat="server" Text='<%# Bind("Grt_Suez") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 20px;">
                                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("Grt_Panama") %>'></asp:Label>
                                                </td>
                                                </td>
                                                <td style="width: 25%; height: 20px;">
                                                    <b>MT</b>
                                                </td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 20%; height: 21px">
                                                    <asp:Label ID="Label3"  runat="server" Text="NRT"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 21px">
                                                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Bind("NRT_International") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:Label ID="Label39" runat="server" Text='<%# Bind("NRT_Suez") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:Label ID="Label40" runat="server" Text='<%# Bind("NRT_Panama") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 20px;">
                                                    <b>MT</b>
                                                </td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label4"  runat="server" Text="LWT"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Job_TitleLabel" runat="server" Text='<%# Bind("LWT_International") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label41" runat="server" Text='<%# Bind("LWT_Suez") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label36" runat="server" Text='<%# Bind("LWT_Panama") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 20px;">
                                                    <b>MT</b>
                                                </td>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:FormView>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                        border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                        <tr>
                            <td colspan="2">
                                <b><i>Machinery particulars</i></b>
                                <br />
                                <asp:FormView ID="FormView7" runat="server" DataKeyNames="Vessel_Code" Height="89px"
                                    Width="100%">
                                    <ItemTemplate>
                                        <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                        border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td align="left" style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label1" runat="server"  Text="Main Engine"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label45" runat="server" Text='<%# Bind("Vessel_MainEngine") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label33" runat="server"  Text="Aux.Boiler"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label46" runat="server" Text='<%# Bind("Vessel_aux_Boiler") %>'></asp:Label>
                                                    <tr>
                                                        <td align="left" style="width: 20%; height: 20px;">
                                                            <asp:Label ID="Label8" runat="server"  Text="MCR"></asp:Label>
                                                        </td>
                                                        <td style="width: 25%; height: 20px;">
                                                            <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Bind("Vessel_ME_MCR") %>'></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 25%; height: 20px;">
                                                            <asp:Label ID="Label38" runat="server"  Text="Capacity@Wrkg prs x Nos"></asp:Label>
                                                        </td>
                                                        <td style="width: 25%; height: 20px;">
                                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("Vessel_ABLR_Cap") %>'></asp:Label>
                                                        </td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 20%; height: 21px">
                                                    <asp:Label ID="Label3" runat="server"  Text="NCR"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 21px">
                                                    <asp:Label ID="LastNameLabel" runat="server" Text='<%# Bind("Vessel_ME_NCR") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:Label ID="Label39" runat="server"  Text="COPs-Capacity x Nos"></asp:Label>
                                                </td>
                                                <td style="width: 25%">
                                                    <asp:Label ID="Label40" runat="server" Text='<%# Bind("Vessel_Cops_Cap") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label4" runat="server"  Text="Aux Engine"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Job_TitleLabel" runat="server" Text='<%# Bind("Aux_Engine") %>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label41" runat="server"  Text="Deck Machinery"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label36" runat="server" Text='<%# Bind("Vessel_Deck_Mache") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label47" runat="server"  Text="KW x Nos"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label48" runat="server" Text='<%# Bind("Vessel_AE_KW") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label49"  runat="server" Text="Last"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label50"  runat="server" Text="Next"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label53"  runat="server" Text="Latest"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label51"  runat="server" Text="Turbine Generator"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label63" runat="server" Text='<%# Bind("Vessel_turb_Genrt") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label52"  runat="server" Text="Dry dock"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label54" runat="server" Text='<%# Bind("Dry_Dock_Last","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label57" runat="server" Text='<%# Bind("Dry_Dock_Next","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label58" runat="server" Text='<%# Bind("Dry_Dock_Latest","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label55" runat="server"  Text="KW x Nos"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label56" runat="server" Text='<%# Bind("Vessel_TG_KW") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label59"  runat="server" Text="Special survey"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label60" runat="server" Text='<%# Bind("Spl_Svry_Last","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label61" runat="server" Text='<%# Bind("Spl_Svry_Next","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label62" runat="server" Text='<%# Bind("Spl_Svry_Latest","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 20%; height: 19px;">
                                                    <asp:Label ID="Label64" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label65" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label66"  runat="server" Text="Tailshaft survey"></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label67" runat="server" Text='<%# Bind("Tailshft_Svry_Last","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label68" runat="server" Text='<%# Bind("Tailshft_Svry_Next","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                                <td style="width: 25%; height: 19px;">
                                                    <asp:Label ID="Label69" runat="server" Text='<%# Bind("Tailshft_Svry_Latest","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:FormView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </center>
    <asp:Button ID="Button1" runat="server" Text="Print" OnClientClick="javascript:window.print();" />
</asp:Content>
