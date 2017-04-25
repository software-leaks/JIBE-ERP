<%@ Page Language="C#" MasterPageFile="~/WebQtn/Webquotation.master" AutoEventWireup="true"
    CodeFile="WebQuotationDetails.aspx.cs" Inherits="WebQuotation_WebQuotationDetails"
    Title="Quotation Details" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="RJS" Namespace="RJS.Web.WebControl" Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <link href="styles/premiere_blue/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            font-size: small;
            font-weight: bold;
        }
        body
        {
            font-size: 11px;
            font-family: Arial;
        }
        input
        {
            font-size: 11px;
            font-family: Arial;
        }
        select
        {
            font-size: 11px;
            font-family: Arial;
        }
        #blur-on-updateprogress
        {
            width: 100%;
            background-color: black;
            moz-opacity: 0.5;
            khtml-opacity: .5;
            opacity: .5;
            filter: alpha(opacity=50);
            z-index: 700;
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function RefreshThispage() {
            alert('this');
            location.reload(true);
        }

        function fixform() {
            if (opener.document.getElementById("aspnetForm").target != "_blank")
                return;
            opener.document.getElementById("aspnetForm").target = "";
            opener.document.getElementById("aspnetForm").action = opener.location.href;
        }



        var lo;
        function selMe(src) {
            try {
                var o;
                var p;
                if (src) {
                    o = document.getElementById(src);
                }
                else {
                    o = window.event.srcElement;
                }
                p = o.parentElement.parentElement;
                p.className = 'ih';
                if (lo) {
                    if (lo.id != p.id) {
                        lo.className = '';
                        lo = p;
                    }
                }
                else {
                    lo = p;
                }
            } catch (ex) {
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function ValidationOnSearch() {
            var DDLVessel = document.getElementById("ctl00_ContentPlaceHolder1_DDLVessel").value;
            var DDLStatus = document.getElementById("ctl00_ContentPlaceHolder1_DDLStatus").value;


            return true;
        }


        function onConfirmPO() {
            var msgRetval = confirm("Do you want to accept this PO?");
            return msgRetval;
        }

    </script>
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 20%; z-index: 710;
                    color: black; background-color: Yellow; width: 200px; height: 30px; font-size: 12px;
                    text-align: left; padding-left: 10px; vertical-align: baseline">
                    Please wait. Loading......
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="updWebQtn" runat="server">
            <ContentTemplate>
                <table align="center" cellpadding="0" cellspacing="0" style="width: 100%; border-left: 1px solid gray;
                    border-right: 1px solid gray; border-top: 1px solid gray">
                    <tr align="center">
                        <td colspan="3" style="background-color: #778899; font-weight: bold; font-size: 14px;
                            width: 100%; padding-bottom: 7px; padding-top: 7px; color: White">
                            Web Quotations
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 10px; width: 100%; padding-bottom: 10px; background-color: #FAFAD2">
                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td align="center" style="width: 40%">
                                        <asp:Label ID="lblInboxMsg" runat="server" Style="color: Red; font-size: small; font-weight: 700;"></asp:Label>
                                    </td>
                                    <td align="center" style="font-size: 12px; width: 40%">
                                        <asp:LinkButton ID="lbtnContractListAlert" runat="server" Font-Bold="true" ForeColor="Red"
                                            OnClick="lbtnContractListAlert_Click"></asp:LinkButton>
                                    </td>
                                    <td align="right" style="width: 20%; font-size: 11px">
                                        <a href="Supplier_HelpFile.pdf" target="_blank">How to submit your quotation ? </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #F5F5F5;
                    color: Black; font-size: 12px; border-left: 1px solid gray; border-right: 1px solid gray;
                    border-top: 1px solid gray">
                    <tr align="left">
                        <td style="text-align: right;">
                            Vessel :&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="DDLVessel" AppendDataBoundItems="true" runat="server" Style="width: 150px">
                                <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;">
                            Reqsn / Order No :&nbsp;
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtReqNo" runat="server"></asp:TextBox>
                        </td>
                        <td rowspan="3" align="center">
                            <div class="ob_iLboIC ob_iLboIC_L" style="width: 392px; height: 110px; visibility: visible;
                                background-image: url(image/navmenubg.png); background-repeat: no-repeat;">
                                <div class="ob_iLboICH">
                                    <div class="ob_iLboICHCL">
                                    </div>
                                    <div class="ob_iLboICHCM">
                                    </div>
                                    <div class="ob_iLboICHCR">
                                    </div>
                                </div>
                                <div class="ob_iLboICB">
                                    <div class="ob_iLboICBL">
                                        <div class="ob_iLboICBLI">
                                        </div>
                                    </div>
                                    <ul class="ob_iLboICBC" style="height: 100px; min-height: 90px; float: left; width: 183px;
                                        margin-left: 6px;">
                                        <li id="li1"><b>
                                            <asp:LinkButton ID="lnkMenu1" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                font-size: 10px" CommandArgument="P-S">New or Saved</asp:LinkButton></b><i>P-S</i></li>
                                        <li id="li2"><b>
                                            <asp:LinkButton ID="lnkMenu2" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                font-size: 10px" CommandArgument="F">Finalised or Declined </asp:LinkButton></b><i>Finalised
                                                    </i></li>
                                        <li id="li4"><b>
                                            <asp:LinkButton ID="lnkMenu4" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                font-size: 10px" CommandArgument="A">PO Received</asp:LinkButton></b><i>PO Received
                                                    </i></li>
                                        <li id="li5"><b>
                                            <asp:LinkButton ID="lnkMenu6" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                font-size: 10px" CommandArgument="C">PO Confirmed</asp:LinkButton></b><i>PO Confirmed</i></li>
                                        <li id="li3"><b>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                font-size: 10px" CommandArgument="N">PO Not Confirmed</asp:LinkButton></b><i>PO Not
                                                    Confirmed</i></li>
                                    </ul>
                                    <ul class="ob_iLboICBC" style="height: 110px; min-height: 90px; float: left; width: 183px;
                                        margin-left: 2px; padding-left: 6px;">
                                        <li id="li8"><b>
                                            <asp:LinkButton ID="lnkMenu8" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                font-size: 10px" CommandArgument="Q">Quotation Not Approved</asp:LinkButton></b><i>Quotation
                                                    Not Approved</i></li>
                                        <li id="li9"><b>
                                            <asp:LinkButton ID="lnkMenu9" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                font-size: 10px" CommandArgument="R">Received for Rework</asp:LinkButton></b><i>Received
                                                    for Rework</i></li>
                                        <li id="li6"><b>
                                            <asp:LinkButton ID="lbtnDelivered" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                font-size: 10px" CommandArgument="H">OnHold</asp:LinkButton></b><i>OnHold</i></li>
                                        <li id="li10"><b>
                                            <asp:LinkButton ID="lnkMenu10" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                font-size: 10px" CommandArgument="CN">Cancel</asp:LinkButton></b><i>Cancel</i></li>
                                        <li id="li11"><b>
                                            <asp:LinkButton ID="lnkMenu11" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                font-size: 10px" CommandArgument="0">ALL</asp:LinkButton></b><i>ALL</i></li>
                                    </ul>
                                    <div class="ob_iLboICBR">
                                        <div class="ob_iLboICBRI">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdfSelectedStage" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="LinkUrl" runat="server" />
                            <asp:HiddenField ID="hdfSelectedStageValue" runat="server" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr align="left">
                        <td style="text-align: right;">
                            From Date :&nbsp;
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtfrom" runat="server" Style="width: 125px;"></asp:TextBox>
                            <RJS:PopCalendar ID="frcal" Control="txtfrom" runat="server" />
                        </td>
                        <td style="text-align: right;">
                            To Date :&nbsp;
                        </td>
                        <td align="left" class="style1">
                            <asp:TextBox ID="txtto" runat="server" Style="width: 125px;"></asp:TextBox>
                            <RJS:PopCalendar ID="tocal" Control="txtto" runat="server" />
                        </td>
                        <td>
                            <asp:LinkButton ID="lbtnInv" runat="server" Text="Invoice payment status" Style="font-weight: bold;"
                                Font-Size="11px" Font-Names="verdana" OnClick="lbtnInv_Click" ForeColor="Blue"></asp:LinkButton></td>
                    </tr>
                    <tr align="left">
                        <td align="right" style="padding-right: 45px" colspan="4">
                            <asp:Button ID="btnSearch" runat="server" Height="25px" Width="80px" Text="Retrieve"
                                OnClientClick="return ValidationOnSearch();" OnClick="btnSearch_Click" />
                        </td>
                        <td style="width: 160px">
                            <asp:LinkButton ID="lbtnViewContractList" runat="server" Text="View Contract List"
                                Font-Size="11px" Font-Names="verdana" ForeColor="Blue" Font-Bold="true" OnClick="lbtnContractListAlert_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" width="100%" style="background-color: #F5F5F5;
                    color: Black; font-size: 12px; border: 1px solid gray">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr align="center">
                                    <td>
                                        <telerik:RadGrid ID="rgdWebQuoDetails" runat="server" AllowAutomaticInserts="True"
                                            GridLines="None" Height="500px" Skin="WebBlue" Width="100%" AutoGenerateColumns="False"
                                            Font-Names="Arial" ShowFooter="false">
                                            <MasterTableView>
                                                <RowIndicatorColumn Visible="False">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Resizable="False" Visible="False">
                                                    <HeaderStyle />
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn HeaderText="Quot. No." UniqueName="QUOTATION_CODE" DataField="QUOTATION_CODE"
                                                        Visible="true">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="REQUISITION_CODE" UniqueName="REQUISITION_CODE"
                                                        HeaderText="Requisition No" Visible="true">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ORDER_CODE" UniqueName="ORDER_CODE" HeaderText="Order No"
                                                        Visible="true">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="document_code" UniqueName="document_code" HeaderText="document_code"
                                                        Visible="false">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Vessel_Code" UniqueName="Vessel_Code" HeaderText="Vessel_Code"
                                                        Visible="true" Display="false">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Vessel_Name" UniqueName="Vessel_Name" HeaderText="Vessel Name"
                                                        Visible="true">
                                                        <HeaderStyle HorizontalAlign="Left" Width="60px" />
                                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="code" UniqueName="code" HeaderText="Dept Code"
                                                        Visible="true" Display="false">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Name_Dept" UniqueName="Name_Dept" HeaderText="Department"
                                                        Visible="true">
                                                        <HeaderStyle HorizontalAlign="Left" Width="80px" />
                                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="SentDate" AllowSorting="true" SortExpression="SentDate"
                                                        HeaderText="Qtn Recvd Date" UniqueName="SentDate">
                                                        <HeaderStyle HorizontalAlign="Left" Width="70px" />
                                                        <ItemStyle HorizontalAlign="Left" Width="70px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Quotation_Due_Date" UniqueName="Quotation_Due_Date"
                                                        HeaderText="Quot. Due Date" Visible="true" Display="true">
                                                        <HeaderStyle HorizontalAlign="Left" Width="70px" />
                                                        <ItemStyle HorizontalAlign="Left" Width="70px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TOTAL_ITEMS" UniqueName="TOTAL_ITEMS" HeaderText="Total Item"
                                                        Visible="true">
                                                        <HeaderStyle HorizontalAlign="Left" Width="70px" />
                                                        <ItemStyle Width="70px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Line_type" UniqueName="Line_type" HeaderText="Line_type"
                                                        Visible="false">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="QUOTATION_SUPPLIER" UniqueName="QUOTATION_SUPPLIER"
                                                        HeaderText="Supplier" Visible="true" Display="false">
                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                        <ItemStyle Width="100px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Quotation_Status" UniqueName="Quotation_Status"
                                                        HeaderText="Quot. Status" Visible="true">
                                                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="View">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="ImgSelect" runat="server" Text="View" NavigateUrl='<%#"QoutationEntry.aspx?Requisitioncode="+Eval("REQUISITION_CODE")+"&Document_Code="+ Eval("document_code")+"&Vessel_Code="+Eval("Vessel_Code")+"&Quotation_code="+Eval("QUOTATION_CODE")+"&Quotation_Status="+Eval("Quotation_Status")+"&Dept_Code="+Eval("code")+"&Quotation_Due_Date="+Eval("Quotation_Due_Date")%>'
                                                                ForeColor="Black" ToolTip="View to Quotation Details" ImageUrl="Image/view.gif"
                                                                Target="_blank">
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="PO Preview">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="btnPreviewPO" runat="server" Text="PO Preview" ForeColor="Navy"
                                                                NavigateUrl='<%#"~/purchase/POPreview.aspx?RFQCODE="+Eval("REQUISITION_CODE")+"&Document_Code="+ Eval("document_code")+"&Vessel_Code="+Eval("Vessel_Code")+"&Quotation_code="+Eval("QUOTATION_CODE")+"&Quotation_Status="+Eval("Quotation_Status")+"&Dept_Code="+Eval("code")+"&Quotation_Due_Date="+Eval("Quotation_Due_Date")%>'
                                                                ToolTip="View to PO Preview" Font-Size="X-Small" Target="_blank" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                        <ItemStyle Width="80px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Accept PO" UniqueName="AcceptPO_ID">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnPOStatus" runat="server" OnCommand="onPOConfirm" CommandName="onPOConfirm"
                                                                CommandArgument='<%#Eval("REQUISITION_CODE")+","+ Eval("document_code")+","+Eval("Vessel_Code")+","+Eval("QUOTATION_CODE")+","+Eval("Quotation_Status")+","+Eval("code")+","+Eval("Quotation_Due_Date")%>'
                                                                ToolTip="Marks PO as Accept" Text="Accept PO" OnClientClick="return onConfirmPO();"
                                                                Font-Size="X-Small" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                        <ItemStyle Width="80px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Attachment" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="imgAttachment" runat="server" Text="View" NavigateUrl='<%#"QoutationEntry.aspx?Requisitioncode="+Eval("REQUISITION_CODE")+"&Document_Code="+ Eval("document_code")+"&Vessel_Code="+Eval("Vessel_Code")+"&Quotation_code="+Eval("QUOTATION_CODE")+"&Quotation_Status="+Eval("Quotation_Status")+"&Dept_Code="+Eval("code")+"&Quotation_Due_Date="+Eval("Quotation_Due_Date")%>'
                                                                ForeColor="Black" ToolTip="View to Quotation Details" ImageUrl="Image/attach.gif"
                                                                Target="_blank" Visible='<%# Convert.ToString(Eval("Attach_Status"))=="0" ? Convert.ToBoolean("false") : Convert.ToBoolean("true")%>'>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="Attach" HeaderText="Attachment" UniqueName="column"
                                                        Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <PopUpSettings ScrollBars="Horizontal" />
                                                </EditFormSettings>
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Scrolling AllowScroll="true" UseStaticHeaders="false" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                        <uc1:ucCustomPager ID="ucCustomPager1" OnBindDataItem="BindGriddataitem" PageSize="50"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
    <asp:ObjectDataSource ID="ObjSelectVessel" runat="server" TypeName="BLLQuotation.clsQuotationBLL.GetVessel"
        SelectMethod="GetVessel"></asp:ObjectDataSource>
</asp:Content>
