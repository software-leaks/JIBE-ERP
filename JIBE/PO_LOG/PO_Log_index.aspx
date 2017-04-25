<%@ Page Title="PO LOG" Language="C#" MasterPageFile="~/Site.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="PO_Log_index.aspx.cs" Inherits="PO_LOG_PO_Log_index" %>

    <%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <style>
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        
        .page
        {
            width: 100%;
        }
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
        .HeaderStyle-center
        {
            background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #333333;
            font-size: 11px;
            padding: 5px;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #959EAF;
            border-collapse: collapse;
        }
        .style2
        {
            width: 10%;
            height: 24px;
        }
        .style4
        {
            width: 20%;
            height: 24px;
        }
        .style5
        {
            width: 10%;
            height: 55px;
        }
        .style6
        {
            width: 198px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function CreatePOLOG(SUPPLY_ID) {
            if (window.confirm('Confirm create PO?')) {
                var POID = document.getElementById("ctl00_MainContent_ddlPOType").selectedIndex;
                var POType = document.getElementById("ctl00_MainContent_ddlPOType").options[POID].value;
                //                var url = "../PO_LOG/PO_Log.aspx?SUPPLY_ID=" + SUPPLY_ID + '&POType=' + POType
                //                window.open(url, "_blank");
                document.getElementById('ctl00_MainContent_iFrame1').src = '../PO_LOG/PO_Log.aspx?SUPPLY_ID=' + SUPPLY_ID + '&POType=' + POType;
            }
            else {
            }
        }
        function OpenPOLOG(SUPPLY_ID) {
            var POID = document.getElementById("ctl00_MainContent_ddlPOType").selectedIndex;
            var POType = document.getElementById("ctl00_MainContent_ddlPOType").options[POID].value;
            //            var url = "../PO_LOG/PO_Log.aspx?SUPPLY_ID=" + SUPPLY_ID + '&POType=' + POType
            //            window.open(url, "_blank");
            document.getElementById('ctl00_MainContent_iFrame1').src = '../PO_LOG/PO_Log.aspx?SUPPLY_ID=' + SUPPLY_ID + '&POType=' + POType;
        }
        function OpenInvoice(SUPPLY_ID) {
            var POID = document.getElementById("ctl00_MainContent_ddlPOType").selectedIndex;
            var POType = document.getElementById("ctl00_MainContent_ddlPOType").options[POID].value;
            //            var url = "../PO_LOG/PO_Log_Invoice_Listing.aspx?SUPPLY_ID=" + SUPPLY_ID + '&POType=' + POType
            //            window.open(url, "_blank");
            document.getElementById('ctl00_MainContent_iFrame1').src = '../PO_LOG/PO_Log_Invoice_Listing.aspx?SUPPLY_ID=' + SUPPLY_ID + '&POType=' + POType;
        }
        function OpenDelivery(SUPPLY_ID) {
            var POID = document.getElementById("ctl00_MainContent_ddlPOType").selectedIndex;
            var POType = document.getElementById("ctl00_MainContent_ddlPOType").options[POID].value;
            //            var url = "../PO_LOG/PO_Log_Delivery_Listing.aspx?SUPPLY_ID=" + SUPPLY_ID + '&POType=' + POType
            //            window.open(url, "_blank");
            document.getElementById('ctl00_MainContent_iFrame1').src = '../PO_LOG/PO_Log_Delivery_Listing.aspx?SUPPLY_ID=' + SUPPLY_ID + '&POType=' + POType;
        }
        function OpenScreen(SUPPLY_ID, Job_ID) {
            var POID = document.getElementById("ctl00_MainContent_ddlPOType").selectedIndex;
            var POType = document.getElementById("ctl00_MainContent_ddlPOType").options[POID].value;

            var url = 'PO_Log.aspx?SUPPLY_ID=' + SUPPLY_ID + '&POType=' + POType;
            OpenPopupWindowBtnID('PO_LOG', 'PO Register', url, 'popup', 900, 1210, null, null, false, false, true, null, 'ctl00_MainContent_btnGet');
        }

    
        function myFunction() {

            var size = {
                width: window.innerWidth || document.body.clientWidth,
                height: window.innerHeight || document.body.clientHeight
            }
            var w = (size.width);
            var y = (size.height);


            document.getElementById('iFrame1').setAttribute("style", "width:" + w * 0.65 + "px");
            document.getElementById('iFrame1').style.overflow = scroll;
            
           

        }
    </script>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <progresstemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress1" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </progresstemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
            <contenttemplate>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-title" class="page-title">
                PO LOG
            </div>
            <div style="overflow: auto; color: Black; margin-bottom: 20px;">
                <table width="100%">
                    <tr>
                       
                        <td align="left" valign="top" style="vertical-align: top; background-color: #f8f8f8; width:30%; padding: 5;
                            border: 1px solid #c3c3c3">
                            <div style="background-color: #f8f8f8; text-align: left; width: 35%;
                                border: 1px solid inset;">
                                <%--<asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                    <contenttemplate>--%>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <div style="padding-top: 5px;border: 1px padding-bottom: 5px;" align="left">
                                                <table width="100%" cellpadding="2" cellspacing="2" align="left">
                                                    <tr>
                                                        <td colspan="5" align="right" class="style5">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="left">
                                                                        PO Type &nbsp;:&nbsp;
                                                                        <asp:DropDownList ID="ddlPOType" runat="server" CssClass="txtInput" Width="250px">
                                                                        </asp:DropDownList>
                                                                        &nbsp;&nbsp;&nbsp;
                                                                        <asp:RequiredFieldValidator ID="rfvPOType" runat="server" Display="None" InitialValue="0"
                                                                            ErrorMessage="Please select PO Type." ControlToValidate="ddlPOType" ValidationGroup="vgSubmit"
                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                   <asp:ImageButton ID="btnNewPO" runat="server" ToolTip="Create New PO" OnClientClick="CreatePOLOG(0);return false;"
                                                                          ImageUrl="~/Images/Add-icon.png" />
                                                                      <%--  <asp:Button ID="btnNewPO" runat="server" OnClientClick="CreatePOLOG(0);return false;"
                                                                            ValidationGroup="vgSubmit" Text="Create New PO" />--%>
                                                                        <%--OnClientClick='OpenScreen(null,null);return false;'--%>
                                                                        <%--OnClientClick='<%#"OpenScreen(&#39;" + Eval("[SUPPLY_ID]") +"&#39;);return false;"%>'--%>
                                                                        <%--OnClientClick='<%#"OpenPOLOG(&#39;" + Eval("[SUPPLY_ID]") +"&#39;);return false;"%>'--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </ContentTemplate></asp:UpdatePanel>
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="margin-right:5px;" >
                                                            Vessel :
                                                        </td>
                                                        <td align="left" style="margin-right:5px;" class="style6" >
                                                            <asp:DropDownList ID="ddlVessel" runat="server" CssClass="txtInput" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right" >
                                                           
                                                        </td>
                                                        <td align="left" class="style4">
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%" align="right">
                                                            Supplier Type :
                                                        </td>
                                                        <td align="left" class="style6" >
                                                            <asp:DropDownList ID="ddlSupplierType" runat="server" CssClass="txtInput" AutoPostBack="true"
                                                                Width="150px" OnSelectedIndexChanged="ddlSupplierType_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right">
                                                              Supplier :
                                                        </td>
                                                        <td colspan="3" style="align:left;">
                                                          <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="txtInput" Width="250px">
                                                            </asp:DropDownList>
                                                           
                                                        </td>
                                                    </tr>
                                                      <tr>
                                                        <td style="width: 10%" align="right">
                                                            Account Type :
                                                        </td>
                                                        <td align="left" class="style6" >
                                                             <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="txtInput" Width="150px">
                                                            </asp:DropDownList>
                                                           
                                                           
                                                        </td>
                                                        <td align="right">
                                                               Account Classification:
                                                        </td>
                                                        <td colspan="3" style="align:left;">
                                                         <asp:DropDownList ID="ddlAccClassification" runat="server" CssClass="txtInput" Width="250px">
                                                            </asp:DropDownList>
                                                           
                                                        </td>
                                                    </tr>
                                                   
                                                    <tr>
                                                        <td style="width: 10%" align="right">
                                                            Type :
                                                        </td>
                                                        <td colspan="4" style="width: 10%" align="left">
                                                            <div style="float: left; text-align: left;  height: 70px; overflow-x: hidden;
                                                                border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                                background-color: #ffffff;">
                                                                <asp:CheckBoxList ID="chkType" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                            <%--   <asp:CheckBox ID="chkAgent" runat="server" Text="Agent" />
                                        <asp:CheckBox ID="chkSupplier" runat="server" Text="Supplier" />
                                        <asp:CheckBox ID="chkMaker" runat="server" Text="Maker" />--%>
                                          <%--/asp:Label>OnClientClick='<%#"OpenScreen2((&#39;" + Eval("[Vessel_ID]") +"&#39;),(&#39;"+ Eval("[Port_Call_ID]") + "&#39;));return false;"%>,(&#39;"+ Eval("[Req_Type]") + "&#39;))--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 15%" align="right">
                                                            Search :
                                                        </td>
                                                        <td colspan="2" align="left">
                                                            <asp:TextBox ID="txtfilter" runat="server" Width="300px" Height="18px" CssClass="txtInput"></asp:TextBox>
                                                        </td>
                                                       <td align="left" >
                                                      <asp:CheckBox ID="chkClosePO" runat="server" Text="Include Closed PO" />
                                                       </td>
                                                        <td>
                                                               <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                                                WatermarkText="Type to Search" WatermarkCssClass="watermarked" />&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td colspan="6" align="right" valign="top" >
                                                     <asp:ImageButton ID="btnGet" runat="server" TabIndex="0" OnClick="btnGet_Click"
                                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />&nbsp;&nbsp;
                                             <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />&nbsp;&nbsp;
                                              <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                                    <%--    <asp:Button ID="btnGet" runat="server" OnClick="btnGet_Click" Text="Search" Width="100px" />--%>
                                                            <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="vgSubmit" />
                                                    </td>
                                                    </tr>
                                                </table>
                                            </div>
                                          
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="divPendingPO" visible="false" runat="server" style="height: 250px; overflow-y: scroll;
                                                max-height: 250px">
                                               <hr />
                                                <table width="100%" cellpadding="2" cellspacing="2" align="left">
                                                    <tr>
                                                        <td align="left" style="width: 8%; color: red;">
                                                            Purchase orders pending your approval or by your direct reports. Click on the row
                                                            to see the PO.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td align="left" style="width: 8%;">
                                                      <telerik:RadGrid ID="gvPendingPO" runat="server" AllowAutomaticInserts="True"
                                                    GridLines="None" ShowFooter="true" ViewStateMode="Enabled" Skin="Office2007" 
                                                    Style="margin-left: 0px" Width="100%" AutoGenerateColumns="False" AllowMultiRowSelection="True"
                                                    PageSize="100" TabIndex="6" HeaderStyle-HorizontalAlign="Center" 
                                                                AlternatingItemStyle-BackColor="#CEE3F6" 
                                                                onselectedindexchanged="gvPendingPO_SelectedIndexChanged">
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                                                    </ClientSettings>
                                                    <MasterTableView DataKeyNames="SUPPLY_ID">
                                                        <RowIndicatorColumn Visible="true">
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                        </RowIndicatorColumn>
                                                        <Columns>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Vessel" Visible="true"
                                                                HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderCode" runat="server" Text='<%#Eval("Vessel_Display_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Supplier Name" Visible="true"
                                                                HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVesselType" runat="server" Text='<%#Eval("Supplier_Display_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText=" Account Classification" Visible="true"
                                                                HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInvoice_Type" runat="server" Text='<%#Eval("Acct")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Amount"
                                                                Visible="true" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReference" runat="server" Text='<%#Eval("TotalAmount")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Created By" Visible="true"
                                                                HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInvoice_Date" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Action By" Visible="true"
                                                                HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCur" runat="server" Text='<%#Eval("Action_By")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                                    </td>
                                                    </tr>
                                                </table>
                                               
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                <hr />
                                                  <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                               <table width="80%" cellpadding="2" cellspacing="2" align="left">
                                                    <tr>
                                                    <td align="left" style="width: 8%;">
                                               
                                              <asp:GridView ID="gvPO" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                                DataKeyNames="ID" CellPadding="0" CellSpacing="0" Width="80%" GridLines="both"
                                                OnSorting="gvPO_Sorting" CssClass="gridmain-css" AllowSorting="true" OnRowDataBound="gvPO_RowDataBound" 
                                                                    onselectedindexchanged="gvPO_SelectedIndexChanged">
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                            <RowStyle CssClass="RowStyle-css" />
                                                            <HeaderStyle CssClass="HeaderStyle-css"  />
                                                            <PagerStyle CssClass="PMSPagerStyle-css" />
                                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Date">
                                                <HeaderTemplate>
                                                    Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server"  Text='<%#Eval("Created_Date","{0:dd-MMM-yyyy}")%>' Width="70px" ></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center"   CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                           
                                            <asp:TemplateField  HeaderText="Office Reference">
                                                <HeaderTemplate>
                                                    Office Reference
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                          
                                                       <asp:Label ID="lblOffice" CommandArgument='<%#Eval("[ID]")%>' runat="server" Width="130px" 
                                                Text='<%#Eval("Office_Ref_Code") %>'  ></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center"  Width="140px"  CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField >
                                                <ItemTemplate>
                                                    <asp:Image ID="ImgStatus" runat="server" Width="12px" Height="12px" />
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="12px"  CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" >
                                                <ItemTemplate>
                                                <table>
                                                <tr>
                                                <td width="20%" align="left" >
                                                   <asp:Button ID="btnSType" runat="server" Text='<%#Eval("SType")%>' Enabled="false"    Width="20px" Height="20px" BackColor="YelloW"  BorderStyle="Dashed" BorderColor="Black" />   
                                                   </td>
                                          
                                                   <td align="left" ><asp:Label ID="lblSupplier" runat="server" Text='<%#Eval("Supplier_Name")%>' Width="150px" ></asp:Label></td>
                                                 </tr>  </table>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="170px"  CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cur">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrency" runat="server"  Text='<%# Eval("Line_Currency")%>'  ></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px"  CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPo_Amount" runat="server" Text='<%#Eval("TotalAmount")%>' ></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="20px"  CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoice" runat="server" Text='<%#Eval("Invoice_Amount")%>' ></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Right"  Width="20px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true"  HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Count">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCount" runat="server" Text='<%#Eval("Invoice_Count")%>' ></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Right" Width="20px"  CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true"  HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderTemplate>
                                                    Action
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table cellpadding="2" cellspacing="2">
                                                        <tr>
                                                       <%-- <td>
                                                         <asp:ImageButton ID="ImgView" runat="server" CommandArgument='<%#Eval("[ID]")%>'  OnClick="ImageView_Click" OnClientClick='<%#"OpenPOLOG(&#39;" + Eval("[SUPPLY_ID]") +"&#39;);return true;"%>'
                                                        ForeColor="Black" ToolTip="View" Width="15px" Height="15px" 
                                                        ImageUrl="~/Images/asl_view.png"></asp:ImageButton>
                                                        </td>--%>
                                                            <td>
                                                                <asp:Button ID="btnInvoice" runat="server" Text="I"  Width="20px" Height="20px" Visible="false" OnClick="ButtonView_Click" CommandArgument='<%#Eval("SUPPLY_ID") + ";" +Eval("SType")%>'   />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnDelivery" runat="server" Text="D"   Width="20px" Height="20px" Visible="false"  OnClick="ButtonView_Click" CommandArgument='<%#Eval("SUPPLY_ID") + ";" +Eval("SType")%>'  />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px"   CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                 <HeaderStyle Wrap="true"  HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindGrid" />
                                            <asp:HiddenField ID="HdnId" runat="server" EnableViewState="False" />
                                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                             </td>
                                                    </tr>
                                                </table>
                                           </ContentTemplate></asp:UpdatePanel>
                     
                          
                             
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td align="left" style="vertical-align: top; border: 1px solid #c3c3c3; width: 70%;">
                            <div style="background-color: #f8f8f8; text-align: left; width: 100%; z-index: 1;
                                border: 1px solid inset;">
                                   <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                                       <iframe id="iFrame1" runat="server" width="100%" height="1200px"></iframe>
                                     </ContentTemplate>  </asp:UpdatePanel>
                            
                            </div>
                        </td>
                        
                    </tr>
                </table>
            </div>
        </div>
         </contenttemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>