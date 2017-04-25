<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" EnableViewState="true"
    CodeFile="SelectSuppliers.aspx.cs" Inherits="Technical_INV_SelectSuppliers" Title="Send RFQ"
    EnableEventValidation="false" %>
 <%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="RJS" Namespace="RJS.Web.WebControl" Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" %>
<%@ Register Src="../UserControl/ucPurcAttachment.ascx" TagName="ucPurcAttachment"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ctlPortList.ascx" TagName="ctlPortList" TagPrefix="uc2" %>
<asp:Content ID="contenthead" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/messages.js" type="text/javascript"></script>
    <link href="../Styles/messages.css" rel="stylesheet" type="text/css" />
   <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
  
   <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
   <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
     <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <style type="text/css">
        .fileBrowser
        {
            width: 370px;
        }
        .style1
        {
            width: 147px;
        }
        .style2
        {
            width: 118px;
        }
        .style3
        {
            width: 69px;
        }
        .style4
        {
            background-color: #C0C0C0;
        }
        .btnCSS
        {
            height: 26px;
        }
    </style>
    <script language="javascript" type="text/javascript">

         function ShowDeliveryInstruction() {

            var ev = window.event;
            var txtid = ev.srcElement;


            txtid.title = txtid.value;

        }

        function DisplayDelivery() {
            document.getElementById("dvDelivery").style.display = "block";
            return false;
        }

        function CloseDelivery() {
            document.getElementById("dvDelivery").style.display = "none";
            return false;
        }


        function Validation() {
            var grid = $find("<%= grvSupplier.ClientID %>");
            var masterTable = grid.get_masterTableView();
            var cell;
            var blnChk = false;
            var cmbSuppText = masterTable._element.rows[0].cells[0].innerText;
            var strQuotDate = document.getElementById("ctl00_MainContent_txtfrom").value;


            if (cmbSuppText == "0") {
                alert("Please select the supplier.")
                return false;
            }

            if (strQuotDate != "") {
                var currDate = new Date();
                var strCurrentDt = currDate.format("dd-MM-yyyy");
                var dt1 = parseInt(strCurrentDt.substring(0, 2), 10);
                var mon1 = parseInt(strCurrentDt.substring(3, 5), 10);
                var yr1 = parseInt(strCurrentDt.substring(6, 10), 10);
                var dt2 = parseInt(strQuotDate.substring(0, 2), 10);
                var mon2 = parseInt(strQuotDate.substring(3, 5), 10);
                var yr2 = parseInt(strQuotDate.substring(6, 10), 10);
                var CurrentDt = new Date(yr1, mon1, dt1);

                var QuotDate = new Date(yr2, mon2, dt2);

                if (QuotDate < CurrentDt) {
                    alert("Quotation due date can not be less or Equal then current date.")
                    return false;
                }
                return true;

            }
        }


      

      
    </script>
    <script language="javascript" type="text/javascript">
        function OpenScreen(ID, Job_ID) {
           
            var ReqCode = document.getElementById("ctl00_MainContent_txtReqCode").value;
            var VesselID = document.getElementById("ctl00_MainContent_txtVessselCode").value;
            var url = 'ReqsnAttachment.aspx?Requisition_code=' + ReqCode + '&Vessel_ID=' + VesselID;
            OpenPopupWindowBtnID('ReqsnAttachment_ID', 'Requisition Attachments', url, 'popup', 600, 800, null, null, false, false, true, false, null);
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
         <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <center>
        <table align="center" width="100%">
            <tr>
                <td style="background-color: #808080; color: #FFFFFF;">
                    <b>Send RFQ</b>
                </td>
            </tr>
        </table>
        <table align="center" width="100%" cellpadding="1" cellspacing="1" style="background-color: #f4ffff;
            color: Black; border-right: 1px solid gray; border-top: 1px solid gray">
            <tr>
                <td class="tdh">
                    Vessel :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblVessel" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Received Date :
                </td>
                <td class="tdd" colspan="3">
                    <asp:Label ID="lblToDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdh">
                    Catalogue :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblCatalog" runat="server"></asp:Label>
                </td>
                <td class="tdh">
                    Requisition Number :
                </td>
                <td class="tdd">
                    <asp:HyperLink ID="lblReqNo" Target="_blank" runat="server"> </asp:HyperLink>
                </td>
                <td class="tdh">
                    Total Items :
                </td>
                <td class="tdd">
                    <asp:Label ID="lblTotalItem" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table cellpadding="3" cellspacing="0" align="center" style="width: 100%; background-color: #87AFC6;
            color: Black">
            <tr>
                <td style="text-align: left;" valign="middle">
                    <div style="float: left">
                        <asp:TextBox ID="txtcode" runat="server" Height="12px" Visible="false" Width="1px"
                            MaxLength="50"></asp:TextBox>
                        &nbsp;<b>Supplier Name: </b>
                        <asp:TextBox ID="txtSupname" runat="server" Width="140px"></asp:TextBox>
                        &nbsp;<b>City: </b>
                        <asp:TextBox ID="txtCity" runat="server" Width="140px"></asp:TextBox>
                        &nbsp;<b>Country: </b>
                        <asp:DropDownList ID="ddlCountry" runat="server" Width="140px">
                        </asp:DropDownList>
                        &nbsp;<b>Category</b>
                        <asp:DropDownList ID="ddlSuppcategory" runat="server" Width="140px">
                        </asp:DropDownList>
                    </div>
                    <div style="float: left">
                        <asp:UpdatePanel ID="updSuppFilter" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                &nbsp;<asp:Button ID="btnimg" runat="server" Text="Search" OnClick="btnimg_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
                <td align="left">
                </td>
                <td style="font-size: x-small;">
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 100%; border: 1px solid gray;
            background-color: #f4ffff;">
            <tr>
                <td align="center">
                    <asp:UpdatePanel ID="SuppSupplierList" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnimg" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <table style="width: 100%">
                               <%-- <tr>
                                    <td style="width: 100%; text-align: right; font-size: 9px">
                                        <asp:DataPager ID="DataPagerProducts" runat="server" PagedControlID="lstsupplier"
                                            PageSize="20" OnPreRender="DataPagerProducts_PreRender">
                                            <Fields>
                                                <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False" />
                                                <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False" />
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td align="center">
                                        <div style="width: 100%; height: 230px; margin-left: 0px; overflow-y: auto; overflow-x: hidden;
                                            text-align: right">
                                            <asp:ListView ID="lstsupplier" cellpadding="1" cellspacing="0" runat="server">
                                                <LayoutTemplate>
                                                    <table id="itemPlaceholderContainer" border="0" width="100%" style="background-color: #616D7E">
                                                        <tr style="background-color: #616D7E" align="Left">
                                                            <th style="width: 20%" align="center">
                                                                <b style="color: White">Select Supplier</b>
                                                            </th>
                                                            <th style="width: 40%" align="Left">
                                                                <b style="color: White">Supplier Name</b>
                                                            </th>
                                                            <th style="width: 20%" align="Left">
                                                                <b style="color: White">City</b>
                                                            </th>
                                                            <th style="width: 20%" align="Left">
                                                                <b style="color: White">Country</b>
                                                            </th>
                                                            <tr runat="server" id="itemPlaceholder" align="Left">
                                                            </tr>
                                                        </tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <table width="100%" cellpadding="1" cellspacing="0" style="border-bottom: 1px solid #FAFAFA">
                                                        <tr style="background-color: #EFEFEF" align="Left">
                                                            <td width="20%" style="text-align: center">
                                                                <asp:UpdatePanel ID="updbtnSelect" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:Button ID="btnSelect" runat="server" Text="Add" Height="20px" OnClick="btnSelect_Click"
                                                                            CommandArgument='<%# Eval("SUPPLIER") + "," + Eval("[Supplier_Property]") %>' ToolTip='<%#Eval("SUPPLIER_NAME")%>'
                                                                            ValidationGroup='<%#Eval("COUNTRY")%>' Font-Size="10px" />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td align="left" width="40%">
                                                                <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SUPPLIER_NAME") %>' />
                                                            </td>
                                                            <td align="left" width="20%">
                                                                <asp:Label ID="lblCITY" runat="server" Text='<%# Eval("CITY") %>' />
                                                            </td>
                                                            <td align="left" width="20%">
                                                                <asp:Label ID="lblSupplierCountry" runat="server" Text='<%# Eval("COUNTRY") %>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:ListView>
                                             <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="filterlistbox" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 100%; border: 1px solid gray;
            background-color: #f4ffff;">
            <tr align="left" style="background-color: #f4ffff">
                <td>
                    <asp:Label ID="lblquotationDate" runat="server" Text="Quotation Due Date :" Style="color: #000000;
                        font-weight: 700"></asp:Label>
                    <asp:TextBox ID="txtfrom" runat="server" ViewStateMode="Enabled" Style="font-size: small;
                        background-color: #FFFF99; text-align: left;"></asp:TextBox>
                    <cc1:CalendarExtender ID="calOrderReadines" Format="dd-MM-yyyy" TargetControlID="txtfrom"
                        runat="server">
                    </cc1:CalendarExtender>
                    <%--                    <RJS:PopCalendar ID="frcal" Control="txtfrom" runat="server" />--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDataErr" runat="server" Text="Label" Style="color: #FF3300"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%; border: 1px solid gray; background-color: #f4ffff;">
                <td align="left">
                    <asp:UpdatePanel ID="updgrvSupplier" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnDELSave" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <div style="height: 230px; overflow-y: auto; overflow-x: hidden; width: 1210px">
                          
                                <telerik:RadGrid ID="grvSupplier" runat="server" AllowAutomaticInserts="True" AllowPaging="False"
                                    CellSpacing="0" CellPadding="0" ItemStyle-Height="12px" HeaderStyle-Height="25px" OnItemDataBound="grvSupplier_ItemDataBound"
                                    AlternatingItemStyle-BackColor="#CEE3F6" GridLines="None" Skin="Office2007" Width="100%"
                                    HeaderStyle-HorizontalAlign="Center"
                                    AutoGenerateColumns="False">
                                    <MasterTableView DataKeyNames="Supplier_Property">
                                        <RowIndicatorColumn Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Resizable="False" Visible="False">
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="SUPPLIER" HeaderText="Supp Code" UniqueName="SUPPLIER">
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left" Width="5%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SUPPLIER_NAME" HeaderText="Name" UniqueName="SUPPLIER_NAME">
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left" Width="20%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Date" HeaderText="Date" UniqueName="Date" Visible="true"
                                                HeaderStyle-Width="0">
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left" Width="6%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="COUNTRY" HeaderText="Country" UniqueName="COUNTRY"
                                                Visible="true" HeaderStyle-Width="0">
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left" Width="6%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Delivery Port" HeaderStyle-Width="115px"
                                                ItemStyle-Width="115px" UniqueName="DELIVERY_PORT">
                                                <ItemTemplate>
                                                    <uc2:ctlPortList ID="DDLPort" SelectedValue='<%#Bind("DELIVERY_PORT")%>' runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left" Width="10%" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Delivery Date" HeaderStyle-Width="130px"
                                                ItemStyle-Width="130px" UniqueName="DELIVERY_DATE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDeliveryDate" Width="80px" Font-Size="10px" Height="12px" EnableViewState="true"
                                                        runat="server" Text='<%#Bind("DELIVERY_DATE")%>'></asp:TextBox>
                                                    <cc1:CalendarExtender ID="caltxtDeliveryDate" Format="dd-MM-yyyy" TargetControlID="txtDeliveryDate"
                                                        runat="server">
                                                    </cc1:CalendarExtender>
                                                    <%-- <RJS:PopCalendar ID="calDeliveryDT" runat="server" Separator="/" From-Control="txtDeliveryDate"
                                                        Format="ddmmyyyy" ViewStateMode="Enabled" Control="txtDeliveryDate" /> --%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left" Width="8%" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Delivery Instructions" UniqueName="Delivery_Instruction">
                                                <HeaderTemplate>
                                                    <asp:Literal ID="ltrdel" Text="Delivery Remarks" runat="server"></asp:Literal>&nbsp;
                                                    <asp:Button ID="lbtnDLVINS" runat="server" Font-Size="9px" OnClientClick="return DisplayDelivery() "
                                                        Text="For All"></asp:Button>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDeliveryInstruction" Width="100%" Font-Size="10px" Height="12px"
                                                        onmouseover="JavaScript:return ShowDeliveryInstruction()" TextMode="MultiLine"
                                                        runat="server" Text='<%#Bind("Delivery_Instructions")%>'></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Send RFQ." Display="false" UniqueName="chkExportToExcel">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkExportToExcel" Checked="true" Height="12px" runat="server" Font-Size="Smaller" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnDelete" runat="server" Visible='<%#  Convert.ToString(Eval("Requisition_Code")) == "New" ? true : false %>'
                                                        AlternateText="delete" ImageUrl="~/Images/Close.gif" OnClick="imgbtnDelete_Click" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Select Items">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnSelectItems" runat="server" CommandArgument='<%#Eval("RowID").ToString()+":"+Eval("SUPPLIER").ToString()%>'
                                                        ToolTip='<%#Eval("SUPPLIER_NAME") %>' ImageUrl="~/Purchase/Image/SelectItem.png"
                                                        OnClick="btnSelectItems_Click" Text="Select Items" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </telerik:GridTemplateColumn>
                                            <%--<telerik:GridTemplateColumn UniqueName="sendMail" HeaderText="Send Mail" Visible="false">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="imgbtnsendMail" Target="_blank" Height="12px" Width="12px" NavigateUrl='<%# "~/Crew/EmailEditor.aspx?ID="+Convert.ToString(Eval("MailID"))+@"&FILEPATH=uploads\Purchase&ReSend=1"  %>'
                                                        Visible='<%#  Convert.ToString(Eval("Requisition_Code")) == "New" ? false : true %>'
                                                        runat="server" ToolTip='<%#Eval("MailStatus")%>' ImageUrl="~/Purchase/Image/view.gif" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </telerik:GridTemplateColumn>--%>
                                            <telerik:GridTemplateColumn UniqueName="sendMail" HeaderText="RFQ type" Visible="true">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="optGRowRFQType" runat="server" RepeatDirection="Horizontal"
                                                        BorderStyle="None" BorderColor="Transparent" AutoPostBack="false" OnSelectedIndexChanged="optRFQType_SelectedIndexChanged"
                                                        Style="font-size: x-small; height: 16px">
                                                        <asp:ListItem Text="Web " Value="2" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Excel" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Send RFQ">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnWebRFQ" runat="server" Text="Excel" CommandArgument='<%#Eval("SUPPLIER")%>'
                                                        ToolTip='<%#Eval("SUPPLIER_NAME")%>' ImageUrl="~/Purchase/Image/webRFQ.png" OnClick="btnWebRFQ_Click"
                                                        Visible='<%#  Convert.ToString(Eval("Requisition_Code")) == "New" ? false : true %>' />
                                                    <img id="Img1" runat="server" src="~/Purchase/Image/spacer.png" width="3" />
                                                    <asp:ImageButton ID="btnExportEcelRFQ" runat="server" Text="Web" CommandArgument='<%#Eval("SUPPLIER")%>'
                                                        ToolTip='<%#Eval("SUPPLIER_NAME")%>' ImageUrl="~/Purchase/Image/Excel-2010.png"
                                                        OnClick="btnExcelRFQ_Click" Visible='<%#  Convert.ToString(Eval("Requisition_Code")) == "New" ? false : true %>' />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                             <telerik:GridTemplateColumn UniqueName="SupplierProperty" HeaderText="Vat" Visible="true">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlProperty" runat="server">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem></asp:DropDownList>
                                                    <asp:Label ID="lblSupplierProperty" runat="server" Text='<%#Bind("Supplier_Property")%>' Visible="false" ></asp:Label>
                                                    <asp:Label ID="lblApplicable" runat="server" Text='<%#Bind("Applicable_Flag")%>' Visible="false" ></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="left" />
                                                <ItemStyle HorizontalAlign="left" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Requisition_Code" HeaderText="Requisition_Code"
                                                UniqueName="Requisition_Code" Visible="true" HeaderStyle-Width="0" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="dport" DataField="DELIVERY_PORT" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="RowID" DataField="RowID" Display="false">
                                            </telerik:GridBoundColumn>

                                              <telerik:GridBoundColumn UniqueName="QUOTATION_CODE" DataField="QUOTATION_CODE" Display="false">
                                            </telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn UniqueName="SupplierProperty" DataField="Supplier_Property" Display="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Applicable_Flag" DataField="Applicable_Flag" Display="false">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <PopUpSettings ScrollBars="Horizontal" />
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling AllowScroll="false" UseStaticHeaders="false" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                            <asp:ObjectDataSource ID="ObjSelectDeliveryPort" runat="server" TypeName="SMS.Business.PURC.BLL_PURC_Purchase"
                                SelectMethod="getDeliveryPort"></asp:ObjectDataSource>
                                <br />
                                <asp:Label ID="lblrfqmessage" runat="server" ForeColor="Red" runat="server" ></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #f4ffff;
            border: 1px solid gray">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #f4ffff;
                        text-align: left; margin-top: 5px">
                        <tr>
                            <td style="font-size: small; width: 10%; margin-top: 7px; color: Black">
                                RFQ Remarks :
                            </td>
                            <td align="left" style="width: 30%; vertical-align: top; margin-top: 7px">
                                <asp:TextBox ID="txtRFQRemarks" runat="server" TextMode="MultiLine" Height="45px"
                                    Width="97%"></asp:TextBox>
                            </td>
                            <td style="width: 50%; text-align: left; vertical-align: middle; margin-top: 7px">
                                <asp:Button ID="btnSendToSupplier" Height="45px" runat="server" Text="Send to selected suppliers"
                                    OnClick="btnSendToSupplier_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnSendToNonApprovedSupp" Height="45px" Text="Export RFQ for non-ASL"
                                    OnClick="btnSendToNonApprovedSupp_Click" runat="server" />
                            </td>
                            <td style="width: 10%; vertical-align: top; margin-top: 7px">
                           
                            <asp:ImageButton ID="lbtnAttachments" runat="server" ImageUrl="../Images/AddAttachment.png" OnClientClick='OpenScreen(null,null);return false;'/>
                            
                            <%--<a href="#" onclick="ShowReqsnAttachment(Requisition_code=" +   + "&Vessel_ID=" + Eval("Vessel_Code").ToString()+"&#39;);">Add Attachment</a>--%>
                              <%-- <asp:HyperLink ID="lbtnAttachments"  Text="Add Attachment" Target="_blank" runat="server"/>--%>
                            </td><td style=" display:none;"> <asp:TextBox ID="txtReqCode" runat="server" Text="" Width="1px"  ></asp:TextBox>
                            <asp:TextBox ID="txtVessselCode" runat="server" Text="" Width="1px"  ></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #f4ffff;">
            <tr align="center">
                <td style="width: 20px">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #FF3300" Width="333px" Height="16px"></asp:Label>
                </td>
                <td>
                    <asp:HyperLink ID="LinkFileLoc1" runat="server"> </asp:HyperLink>
                    <asp:LinkButton ID="LinkFileLoc" runat="server" Visible="False">LinkButton</asp:LinkButton>
                </td>
                <td>
                    <asp:HiddenField ID="hiddenChkCount" runat="server" Value="0" />
                </td>
                <td>
                    <asp:HiddenField ID="HddEmailCc" runat="server" Value="0" />
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="updDelv" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="dvDelivery" style="display: none; position: fixed; left: 50%; top: 38%;
                    background-color: Silver; text-align: right; padding: 5px 5px 5px 5px">
                    <table style="vertical-align: top; color: Black; width: 580px" border="1">
                        <tr>
                            <td style="text-align: left; vertical-align: top">
                                Delivery Port:<br />
                                <asp:DropDownList ID="DDLPortAll" runat="server" AppendDataBoundItems="true" Height="20px"
                                    DataSourceID="ObjSelectDeliveryPort" DataTextField="Port_Name" DataValueField="Id"
                                    Style="font-size: 10px" Width="144px">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; vertical-align: top; width: 140px">
                                Delivery Date:<br />
                                <asp:TextBox ID="txtdeldate" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdeldate" Format="dd/MM/yyyy"
                                    runat="server">
                                </cc1:CalendarExtender>
                            </td>
                            <td style="text-align: left; vertical-align: top">
                                Delivery Instructions:
                                <asp:TextBox ID="txtdelivery" TextMode="MultiLine" Width="300px" Height="60px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="right">
                                <asp:Button ID="btnDELSave" runat="server" Text="OK" OnClick="btnDELSave_Click" />
                                <asp:Button ID="btnDELcancel" runat="server" OnClientClick="return CloseDelivery()"
                                    Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updReqsnitems" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="dvreqsnItems" style="position: fixed; z-index: 50; left: 20%; top: 20%;
                    width: 1000px; border: 1px solid black; padding: 15px;" class="popup-css" runat="server">
                    <table width="100%" style="text-align: center">
                        <tr>
                            <td style="padding-bottom: 5px">
                                <asp:Label ID="lblSupplierName" runat="server" Style="font-size: 11px; font-weight: bold;
                                    color: Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="max-height: 300px; overflow: auto;">
                                    <asp:GridView ID="gvReqsnItems" AutoGenerateColumns="False" GridLines="None" Width="99%"
                                        BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px" CellPadding="2" runat="server"
                                        OnRowDataBound="gvReqsnItems_RowDataBound">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkitems" runat="server" Checked="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Subsystem_Description" HeaderText="Sub Catalogue" ItemStyle-Width="7%"
                                                HeaderStyle-Width="7%">
                                                <HeaderStyle Width="7%" />
                                                <ItemStyle Width="7%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="ItemID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemRefCode" runat="server" Text='<%# Bind("ItemID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SrNo" HeaderText="Sr.No.">
                                                <ItemStyle Width="7%" HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Drawing_Number" HeaderText="Drawing No" Visible="true">
                                                <ItemStyle Width="7%" HorizontalAlign="Center" />
                                                <HeaderStyle Width="7%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Part_Number" HeaderText="Part No" Visible="true">
                                                <ItemStyle Width="7%" HorizontalAlign="Center" />
                                                <HeaderStyle Width="7%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Description" Visible="true">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lblItemDesc" Width="100%" runat="server" Target="_blank" Text='<%#Eval("ItemDesc")%>'
                                                        NavigateUrl='<%# "~/Purchase/Item_History.aspx?vessel_code="+ Request.QueryString["Vessel_Code"].ToString()+"&item_ref_code="+Eval("ItemID").ToString() %>'>  </asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemUnit" HeaderText="Unit">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ROB_QTY" HeaderText="ROB" Visible="true">
                                                <ItemStyle Width="5%" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ReqestedQty" HeaderText="Requested Qty" Visible="true">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ItemComments" HeaderText="Comments" Visible="true" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="15%" />
                                            </asp:BoundField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblshortDesc" Text='<%#Eval("ItemDesc")%>' ToolTip='<%#Eval("Vessel_ID")%>'
                                                        runat="server"></asp:Label>
                                                    <asp:Label ID="lblLongDesc" Text='<%#Eval("Long_Description")%>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblComments" Text='<%#Eval("ItemComments")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnselectedItemsSave" runat="server" Height="30px" Width="80px" Text="Ok"
                                    OnClick="btnselectedItemsSave_Click" />
                                <asp:Button ID="btnselectedItemsClose" runat="server" Text="Close" Height="30px"
                                    Width="80px" OnClick="btnselectedItemsClose_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hdfSuppRowID" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
