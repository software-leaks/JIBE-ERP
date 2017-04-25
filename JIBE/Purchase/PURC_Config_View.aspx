<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PURC_Config_View.aspx.cs" Inherits="Purchase_PURC_Config_View" EnableEventValidation="true" EnableViewState="true" %>

<%@ Register Src="../UserControl/ctlPortList.ascx" TagName="ctlPortList" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/ctlCityList.ascx" TagName="ctlCityList" TagPrefix="ucCity" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="RJS" Namespace="RJS.Web.WebControl" Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>    
<%@ Register Src="../UserControl/ucPurcReqsnHold_UnHold.ascx" TagName="ucPurcReqsnHold_UnHold"
    TagPrefix="Hold" %>
<%@ Register Src="../UserControl/ucPurc_Rollback_Reqsn.ascx" TagName="ucPurc_Rollback_Reqsn"
    TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

 <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="styles/premiere_blue/style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
   
    <script language="javascript" type="text/javascript">

        function isvalid() {
                   var CHK_Rqsn = document.getElementById("<%=LBReqsntype.ClientID%>");
                   var CHK_supp = document.getElementById("<%=LbSupplierType.ClientID%>");


                   var checkbox = CHKCHK_Rqsn.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (atLeast > counter) {
                alert("Please select atleast 1 Supplier scope");
                return false;
            }
            else {
                return true;
            }

            var checkbox = CHK_supp.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (atLeast > counter) {
                alert("Please select atleast 1 Supplier scope");
                return false;
            }
            else {
                return true;
            }

        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 142px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
    <div class="page-title">
                <b>Purchase Process Configuration </b>
       
        
            </div>
    <div id="config_div"  style="display:inherit; 
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: auto; height:auto">
                            <%-- <div align="center">--%>
                            <asp:ObjectDataSource ID="objsrcReqsnType" SelectMethod="Get_ReqsnType" TypeName="ClsBLLTechnical.TechnicalBAL"
                                runat="server"></asp:ObjectDataSource>
                              </br>
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="right">
                                        PO Type
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                       
                                        <asp:DropDownList ID="LBpotype" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="LBpotype" 
                            InitialValue="0" ErrorMessage="Please select the POType"></asp:RequiredFieldValidator>

                                    </td>  
                                     
                                </tr>
                                <tr>
                                    <td align="right">
                                        Requisition Type
                                    </td>

                                    <td style="color: #FF0000; width: 1%" align="Left">
                                        *
                                    </td>
                                    <td colspan="4" align="Left">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <biv>
                                                                                        
                                                        <div style="float: left; text-align: left; width: 450px; height: 60px; overflow-x: hidden;
                                                            border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                            background-color: #ffffff;">
                                                             <asp:CheckBoxList ID="LBReqsntype" runat="server" RepeatColumns="5"   Height="60px">
                                
                                        </asp:CheckBoxList>
                                                        </div>
                                                   
                                                </biv>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                               

                                    </td>
                                    </tr><tr>
                                    <td align="right">
                                        Supplier Type
                                    </td colspan="4" >
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td  style="height:10px" align="Left">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <biv>
                                                                                        
                                                        <div style="float: left; text-align: left; width: 450px; height: 60px; overflow-x: hidden;
                                                            border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                            background-color: #ffffff;">
                                                            <asp:CheckBoxList ID="LbSupplierType" RepeatColumns="5"  runat="server"   Height="60px">
                                        </asp:CheckBoxList>
                                                        </div>
                                                   
                                                </biv>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                       
                                    </td>
                                    </tr>
                                    <table width="98%" cellpadding="2" cellspacing="2" align="left">
                                    <br/ >
                                    
                                    <table>
                                <tr>
                                    <%--<td align="right">Issued By Company</td><td style="color: #FF0000; width: 1%" align="right">*</td> 
                                                 <td>
                                                <asp:ListBox ID="LBCompany_Issue" runat="server" SelectionMode="Multiple" onclick="ListBoxClient_SelectionChanged(this, event,3);" Width="150px" Height="100"></asp:ListBox>
                                                <asp:DropDownList ID="LBCompany_Issue" runat="server"></asp:DropDownList>
                                                  </td> --%>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Owner
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Owner" runat="server" Width="80px">
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" class="style1">
                                        Delivery Port
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Delivery_Port" runat="server" Width="80px">
                                          
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        Delivery Port Date
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dd_Delivery_Port_date" runat="server" Width="80px">
                                           
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Vessle Movement Date
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Vessel_movement_Date" runat="server" Width="80px">
                                          
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" class="style1">
                                        Quotation Required
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Quote_required" runat="server" OnSelectedIndexChanged="ddl_Quote_required_SelectedIndexChanged" Width="80px"
                                            AutoPostBack="true">
                                            
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        No Of Quotaion
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_QuoteNo" Text="0" Max="99" Min="1" runat="server"  
                                            Enabled="false" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Item Category
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Item_Category" runat="server" Width="80px">
                                          
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" class="style1">
                                        Vessel Processing PO
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Vessel_Processing_PO" runat="server" Width="80px">
                                          
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        Enable Adding Free Text Item
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Enable_Free_text" runat="server" Width="80px">
                                          
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="right">
                                        Send a Copy to Vessel
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_copy_to_vessel" runat="server" Width="80px">
                                          
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" class="style1">
                                        Supplier PO Confirmation
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Sup_Po_Confirmation" runat="server" Width="80px">
                                           
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        Delivery Confirmation On Board
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Delivery_Confirm_Onbor" runat="server" Width="80px">
                                           
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Vessel Delivery Confirmation
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Vessel_Delivery_Confirm" runat="server" Width="80px">
                                          
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" class="style1">
                                        Withholding TAx
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Withhold_tax" runat="server" Width="80px">
                                          
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        VAT Configuration by Purchaser
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Vat_Config_Purc" runat="server" Width="80px">
                                           
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                   
                                    <td align="right">
                                        Verification Required
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_require_verify" runat="server" Width="80px">
                                            
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" class="style1">
                                        Auto PO Closing
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Auto_POClosing" runat="server" Width="80px">
                                           
                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        </br>
                         <asp:Button ID="submit" runat="server" Text="Submit" OnClick="submit_Click"  /></br></br>
    </div>
    </center>
    </asp:Content>