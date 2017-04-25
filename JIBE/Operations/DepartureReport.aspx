<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DepartureReport.aspx.cs"
    Inherits="Operation_DepartureReport" %>

<%@ Register Src="../UserControl/ctlRecordNavigation.ascx" TagName="ctlRecordNavigation"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            var wh = 'PKID=<%=Request.QueryString["id"]%>';
            Get_Record_Information_Details('OPS_Telegrams', wh);

        });

    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContent" runat="server">
    <div id="pageTitle">
        <asp:Label ID="lblPageTitle" runat="server" Text="Departure Report"></asp:Label>
    </div>
    <div id="page-content" style="color: #333333; border: 1px solid gray; z-index: -2;
        width: 100%">
        <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="updmain" runat="server">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="text-align: left; font-weight: bold;" >
                            Vessel Name:&nbsp;<asp:Label ID="lblVessel" runat="server" ForeColor="Blue" Font-Size="14px"></asp:Label>
                            &nbsp; &nbsp; &nbsp;|&nbsp; &nbsp; &nbsp;
                            <asp:LinkButton ID="lbtncrwlist" runat="server" Text="Crew List" OnClick="lbtncrwlist_Click"></asp:LinkButton>
                        </td>
                        <td align="right" style="padding-right: 17px">
                            <uc1:ctlRecordNavigation ID="ctlRecordNavigationReport" OnNavigateRow="BindReport"
                                runat="server" />
                        </td>
                    </tr>
                    <caption>
                        &nbsp;<tr>
                            <td colspan="2">
                                <div ID="dvDepartureReport">
                                    <style type="text/css">

                                    .leafTR
                                    {
                                        border-bottom-style: solid;
                                        border-bottom-color: White;
                                        border-bottom-width: 1px;
                                    }
                                    .leafTD-header
                                    {
                                        width: 150px;
                                        height: 20px;
                                        padding: 0px 0px 0px 10px;
                                        text-align: left;
                                    }
                                    .leafTD-data
                                    {
                                        width: 140px;
                                        height: 20px;
                                        padding: 0px 0px 0px 0px;
                                        background-color: #cce499;
                                        text-align: left;
                                    }
                                    #pageTitle
                                    {
                                        background-color: gray;
                                        color: White;
                                        font-size: 12px;
                                        text-align: center;
                                        padding: 2px;
                                        font-weight: bold;
                                    }
                                </style>
                                 <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="text-align: left; font-weight: bold;" >
                                            IMO No.:&nbsp;<asp:Label ID="lblIMONO" runat="server" ForeColor="Blue" Font-Size="14px"></asp:Label>
                                            &nbsp;|&nbsp;
                                            Call Sign:&nbsp;<asp:Label ID="lblCallSign" runat="server" ForeColor="Blue" Font-Size="14px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:FormView ID="fvdepature" runat="server" OnDataBound="fvdepature_DataBound" 
                                        Width="100%">
                                        <ItemTemplate>
                                        
                                            <table width="100%">
                                                <tr valign="top">
                                                    <td style="border: solid 1px gray">
                                                        <table>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Date:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("TelegramDate")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Voyage Number:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("VOYAGE")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Departure frm Port:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("PortName")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    UTC / GMT:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("UTC_TYPE")%>&nbsp; <%#Eval("UTC_hr")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Dep Draft:
                                                                </td>
                                                                <td style="background-color: #cce499">
                                                                    <table>
                                                                        <tr style="border-bottom: solid 1px white">
                                                                            <td style="width: 50px; height: 25px; background-color: #cce499">
                                                                                Fwd(mtrs)
                                                                            </td>
                                                                            <td style="width: 50px; height: 25px; background-color: #cce499">
                                                                                Mid(mtrs)
                                                                            </td>
                                                                            <td style="width: 50px; height: 25px; background-color: #cce499">
                                                                                Aft(mtrs)
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 50px; height: 25px; background-color: #cce499; border-right: solid 1px white">
                                                                                <%#Eval("FwdDraft")%>
                                                                            </td>
                                                                            <td style="width: 50px; height: 25px; background-color: #cce499; border-right: solid 1px white">
                                                                                <%#Eval("MIDDRAFT")%>
                                                                            </td>
                                                                            <td style="width: 50px; height: 25px; background-color: #cce499;">
                                                                                <%#Eval("AftDraft")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Arr Draft:
                                                                </td>
                                                                <td style="background-color: #cce499">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 55px; height: 25px; background-color: #cce499; border-right: solid 1px white">
                                                                                <%#Eval("FwdEstArrDraft")%>
                                                                            </td>
                                                                            <td style="width: 55px; height: 25px; background-color: #cce499; border-right: solid 1px white">
                                                                            </td>
                                                                            <td style="width: 50px; height: 25px; background-color: #cce499;">
                                                                                <%#Eval("AftEstArrDraft")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    GM:
                                                                </td>
                                                                <td class="leafTD-data" >
                                                                    <%#Eval("gm")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    &nbsp;mtrs
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Displacement:
                                                                </td>
                                                                <td class="leafTD-data" >
                                                                    <%#Eval("Displacement")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    &nbsp;MT
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Deadweight:
                                                                </td>
                                                                <td class="leafTD-data" >
                                                                    <%#Eval("dwt")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    &nbsp;MT
                                                                </td>
                                                            </tr>
                                                              <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Cargo Weight:
                                                                </td>
                                                                <td class="leafTD-data" >
                                                                    <%#Eval("Cargo_Weight")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    &nbsp;MT
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Next Port:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("Next_Port")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                             <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                   Total distance to go:
                                                                </td>
                                                                <td class="leafTD-data" >
                                                                    <%#Eval("PassDist")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    &nbsp;N.mile
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Estimated speed:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("Est_Speed")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    ETA Next Port @ Est.speed:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("ETANextPort")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td>
                                                                </td>
                                                                <td style="text-align: right; padding-left: 6px">
                                                                    Frame
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Shearing Force:
                                                                </td>
                                                                <td colspan="1">
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 50%">
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td class="leafTD-data">
                                                                                            <%#Eval("Shearing_Force")%>
                                                                                        </td>
                                                                                        <td style="width: 20px; height: 20px">
                                                                                            %
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td style="width: 50%; border-left: 1px solid gray; background-color: #cce499">
                                                                                <%#Eval("Frame_SF")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Bending Moment:
                                                                </td>
                                                                <td colspan="1">
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 50%">
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td class="leafTD-data">
                                                                                            <%#Eval("Bending_Movement")%>
                                                                                        </td>
                                                                                        <td style="width: 20px; height: 20px">
                                                                                            %
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td style="width: 50%; border-left: 1px solid gray; background-color: #cce499">
                                                                                <%#Eval("Frame_BM")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Tortional Moment:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("Tortional_Movement")%>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    %
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td colspan="2" style="background-color: #99ccff; text-align: left">
                                                                    Tugs usage number:
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-data" colspan="2"  style="height: 100px; vertical-align: top"> 
                                                                        <asp:TextBox ID="TextBox1" runat="server" BackColor="#cce499" 
                                                                        BorderStyle="None" ForeColor="Black" Height="100%" MaxLength="400" 
                                                                        Text='<%#Eval("TUG_USED")%>' TextMode="MultiLine" Width="100%"> </asp:TextBox>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 5px">
                                                    </td>
                                                    <td style="border: solid 1px gray" valign="top">
                                                        <table>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                </td>
                                                                <td style="width: 50px; height: 20px; background-color: #99ccff; text-align: center">
                                                                    Bunkers
                                                                    <br />
                                                                    Received
                                                                </td>
                                                                <td style="width: 50px; height: 20px; background-color: #99ccff; text-align: center">
                                                                    Consump in port
                                                                </td>
                                                                <td style="width: 90px; height: 20px; background-color: #99ccff; text-align: center">
                                                                    ROB
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Heavy Oil:
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <%#Eval("RcvdFO")%>
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <%#Eval("ME_HOcons")%>
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <asp:Label ID="lblHO" runat="server" Height="100%" Text='<%#Eval("HO_ROB")%>' 
                                                                        Width="100%"></asp:Label>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    &nbsp;MT
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Diesel Oil:
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <%#Eval("RcvdDO")%>
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <%#Eval("ME_DOcons")%>
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <asp:Label ID="lblDO" runat="server" Height="100%" Text='<%#Eval("DO_ROB")%>' 
                                                                        Width="100%"></asp:Label>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    &nbsp;MT
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    MECC:
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <%#Eval("RcvdMECC")%>
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <%#Eval("MECC_Cons")%>
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <asp:Label ID="lblMECC" runat="server" Text='<%#Eval("MECC_ROB")%>'></asp:Label>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    &nbsp;Ltrs
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    MECYL:
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <%#Eval("RcvdMECYL")%>
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <%#Eval("MECYL_Cons")%>
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <asp:Label ID="lblMECYL" runat="server" Height="100%" 
                                                                        Text='<%#Eval("MECYL_ROB")%>' Width="100%"></asp:Label>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    &nbsp;Ltrs
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    AECC:
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <%#Eval("RcvdAECC")%>
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <%#Eval("AECC_Cons")%>
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <asp:Label ID="lblAECC" runat="server" Height="100%" 
                                                                        Text='<%#Eval("AECC_ROB")%>' Width="100%"></asp:Label>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    &nbsp;Ltrs
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Fresh Water:
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <%#Eval("RcvdFW")%>
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <%#Eval("FW_cons")%>
                                                                </td>
                                                                <td class="leafTD-data" style="width: 50px; text-align: right">
                                                                    <asp:Label ID="lblFW" runat="server" Height="100%" Text='<%#Eval("FW_ROB")%>' 
                                                                        Width="100%"></asp:Label>
                                                                </td>
                                                                <td style="width: 20px; height: 20px">
                                                                    &nbsp;MT
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <table cellpadding="4" 
                                                                        style="width: 100%; border: 1px solid gray; background-color: #efefef;">
                                                                        <tr style="background-color: #99ccff; font-weight: bold">
                                                                            <td colspan="2">
                                                                                Bunker Samples offlanding status:
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left">
                                                                                Heavy Oil
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label1" runat="server" Height="100%" 
                                                                                    Text='<%#Eval("FO_Offlanded")%>' Width="100%"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left">
                                                                                Diesel Oil
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label2" runat="server" Height="100%" 
                                                                                    Text='<%#Eval("GO_Offlanded")%>' Width="100%"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left">
                                                                                Lube Oil
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label4" runat="server" Height="100%" 
                                                                                    Text='<%#Eval("LO_Offlanded")%>' Width="100%"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2" style="text-align: left;">
                                                                                <asp:Repeater ID="rptLOSamples" runat="server">
                                                                                    <HeaderTemplate>
                                                                                        <table border="1" cellpadding="3" cellspacing="2" 
                                                                                            style="width: 90%; border-collapse: collapse; margin-left: 20px;">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Category
                                                                                                </td>
                                                                                                <td>
                                                                                                    Remarks
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td style="background-color: White;">
                                                                                                <%#Eval("Category_Name")%>
                                                                                            </td>
                                                                                            <td style="background-color: White;">
                                                                                                <%#Eval("Remarks")%>
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
                                                                            <td style="text-align: left">
                                                                                Offland report created?
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label3" runat="server" Height="100%" 
                                                                                    Text='<%#Eval("Offland_Report")%>' Width="100%"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                     </table>
                                                                </td>
                                                            </tr>
                                                                <td colspan="5">
                                                                    <table cellpadding="4" 
                                                                        style="width: 100%; border: 1px solid gray; background-color: #efefef;">
                                                                        <tr style="background-color: #99ccff; font-weight: bold">
                                                                            <td colspan="2">
                                                                                Date And Time:
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left">
                                                                                Commen. Load/disc
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label5" runat="server" Height="100%" 
                                                                                    Text='<%#Eval("Commen_Load_disc")%>' Width="100%"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left">
                                                                                Compl. Load/disc
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label6" runat="server" Height="100%" 
                                                                                    Text='<%#Eval("Compl_Load_disc")%>' Width="100%"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left">
                                                                                Pilot Away
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label7" runat="server" Height="100%" 
                                                                                    Text='<%#Eval("Pilot_Away")%>' Width="100%"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left">
                                                                                Anchored/Drift
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label8" runat="server" Height="100%" 
                                                                                    Text='<%#Eval("Anchored_Drift")%>' Width="100%"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    Steaming hrs
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label14" runat="server" Height="100%" 
                                                                        Text='<%#Eval("Steaming_hrs")%>' Width="100%"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    Total hours in a day
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label9" runat="server" Height="100%" 
                                                                        Text='<%#Eval("Total_Hrs_In_Day")%>' Width="100%"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    Ttl steam hrs since STBY
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label10" runat="server" Height="100%" 
                                                                        Text='<%#Eval("Total_Steam_Hrs_Since_STBY")%>' Width="100%"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    Steaming Dist till COP
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" Height="100%" 
                                                                        Text='<%#Eval("Steaming_Dist_Till_COP")%>' Width="100%"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    Wind Force
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" Height="100%" 
                                                                        Text='<%#Eval("Wind_Force")%>' Width="100%"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    Wave height/dir.
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label13" runat="server" Height="100%" 
                                                                        Text='<%#Eval("Wind_Direction")%>' Width="100%"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 5px">
                                                    </td>
                                                    <td style="border: solid 1px gray">
                                                        <table>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Charter Name:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("Charter_Name")%>
                                                                </td>
                                                                <td style="width: 75px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    SBE:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("strSBE")%>
                                                                </td>
                                                                <td style="width: 75px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    PilotOnbd:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("PilotOnbd")%>
                                                                </td>
                                                                <td style="width: 75px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Unberthed/All cast off:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("Unberthed")%>
                                                                </td>
                                                                <td style="width: 75px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    COP/SSP:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("SSP")%>
                                                                </td>
                                                                <td style="width: 75px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Type Of Cargo:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("Type_Of_Cargo")%>
                                                                </td>
                                                                <td style="width: 75px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    <%--Num. of Cntainrs Loaded:--%>Qty. Cargo/Cntnrs Loaded:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("Cntainr_Loaded")%>
                                                                </td>
                                                                <td style="width: 75px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    <%--Num. of Cntainrs Discharged:--%>Qty. Cargo/Cntnrs Dischrgd:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("Cntainr_Discharged")%>
                                                                </td>
                                                                <td style="width: 75px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Num. of All Containers Obd:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("CntainrOnbd")%>
                                                                </td>
                                                                <td style="width: 75px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Num. of Reefers Obd:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("Reefer_num")%>
                                                                </td>
                                                                <td style="width: 75px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header">
                                                                    Num. of Containers damaged during Port stay:
                                                                </td>
                                                                <td class="leafTD-data">
                                                                    <%#Eval("CntainrDmgdInPort")%>
                                                                </td>
                                                                <td style="width: 75px; height: 20px">
                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header" style="vertical-align: top">
                                                                    Reason for damage to Containers:
                                                                </td>
                                                                <td class="leafTD-data" colspan="2" style="height: 100px; vertical-align: top">
                                                                   

                                                                      <asp:TextBox ID="TextBox4" runat="server" BackColor="#cce499" 
                                                                        BorderStyle="None" ForeColor="Black" Height="100%" MaxLength="400" 
                                                                        Text='<%#Eval("CntainrDmgdInPort_Remarks")%>' TextMode="MultiLine" Width="100%"> </asp:TextBox>
                                                                </td>
                                                             </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header" style="vertical-align: top">
                                                                    Port Activites-Details:
                                                                </td>
                                                                <td class="leafTD-data" colspan="2" style="height: 100px; vertical-align: top">
                                                                   
                                                                        <asp:TextBox ID="TextBox3" runat="server" BackColor="#cce499" 
                                                                        BorderStyle="None" ForeColor="Black" Height="100%" MaxLength="400" 
                                                                        Text='<%#Eval("PortAct")%>' TextMode="MultiLine" Width="100%"> </asp:TextBox>

                                                                </td>
                                                            </tr>
                                                            <tr class="leafTR">
                                                                <td class="leafTD-header" style="vertical-align: top">
                                                                    Passage Route/via which rout or passage and why:
                                                                </td>
                                                                <td class="leafTD-data" colspan="2" style="height: 100px; vertical-align: top">
                                                                    
                                                                      <asp:TextBox ID="TextBox2" runat="server" BackColor="#cce499" 
                                                                        BorderStyle="None" ForeColor="Black" Height="100%" MaxLength="400" 
                                                                        Text='<%#Eval("PassRoute")%>' TextMode="MultiLine" Width="100%"> </asp:TextBox>
                                                                </td>
                                                             </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="border: solid 1px gray">
                                                        <table>
                                                            <tr>
                                                                <td class="leafTD-header" style="vertical-align: top">
                                                                    Remark:
                                                                </td>
                                                                <td style="width: 100%; height: 120px; background-color: #cce499">
                                                                    <asp:TextBox ID="txtremark" runat="server" BackColor="#cce499" 
                                                                        BorderStyle="None" ForeColor="Black" Height="100%" MaxLength="400" 
                                                                        Text='<%#Eval("Remarks")%>' TextMode="MultiLine" Width="100%"> </asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:FormView>
                                </div>
                            </td>
                        </tr>
                    </caption>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 1px solid gray;
            background-color: #FDFDFD">
        </div>
    </div>
</asp:Content>
