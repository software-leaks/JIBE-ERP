<%@ Page Title="Logistic PO " Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="LOG_Logistics_PO_List.aspx.cs" Inherits="Purchase_LOG_Logistics_PO_List" %>

<%@ Register Src="../UserControl/uc_SupplierList.ascx" TagName="uc_SupplierList"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="ucpager" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/Purc_Logistic.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="styles/premiere_blue/style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/CustomAsyncDropDown.js" type="text/javascript"></script>
    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            width: 120px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 0px 2px 0px 3px;
            height: 20px;
            vertical-align: middle;
        }
    </style>
    <style type="text/css">
        .ob_iLboICBC li
        {
            float: left;
            width: 150px;
            font-size: 11px;
        }
        /* For IE6 */* HTML .ob_iLboICBC li
        {
            -width: 135px;
        }
        * HTML .ob_iLboICBC li b
        {
            width: 135px;
            overflow: hidden;
        }
        .style1
        {
            width: 100%;
        }
    </style>
    <script type="text/javascript">

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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
              Logistic PO
    
             </div>
    <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
          
 
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 0px; color: #282829">
        <input type="hidden" id="hdf_Log_ID" />
        <input type="hidden" id="hdf_User_ID" runat="server" />
        <asp:UpdatePanel ID="updLPOMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlmain" Width="100%" runat="server" BorderStyle="None" DefaultButton="btnSearchPO">
                    <table width="100%" style="padding-bottom: 5px; padding-top: 5px">
                        <tr>
                            <td class="tdh">
                                Fleet :
                            </td>
                            <td class="tdd">
                                <ucDDL:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                    Height="150" Width="160" />
                            </td>
                            <td class="tdh">
                                Supplier :
                            </td>
                            <td class="tdd">
                                <auc:CustomAsyncDropDownList ID="uc_SupplierList1" runat="server" 
                                    Width="160" Height="250" />
                            </td>
                            <td class="tdd" style="width: 130px; padding-left: 10px">
                                <asp:CheckBox ID="chkShowActive" Checked="true" Text="Show Active" Visible="false"
                                    runat="server" />
                            </td>
                            <td rowspan="4">
                                <div class="ob_iLboIC ob_iLboIC_L" style="width: 392px; height: 60px; visibility: visible;
                                    background-image: url(image/navmenubg.png); background-repeat: no-repeat; border-bottom: 1px solid gray;
                                    border-radius: 5px">
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
                                                    font-size: 10px" CommandArgument="NLP">New Request </asp:LinkButton></b></li>
                                            <li id="li2"><b>
                                                <asp:LinkButton ID="lnkMenu2" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                    font-size: 10px" CommandArgument="APR">Pending Approval </asp:LinkButton></b></li>
                                            <li id="li8"><b>
                                                <asp:LinkButton ID="lnkMenu8" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                    font-size: 10px" CommandArgument="RPO">Raise PO </asp:LinkButton></b></li>
                                        </ul>
                                        <ul class="ob_iLboICBC" style="height: 110px; min-height: 90px; float: left; width: 183px;
                                            margin-left: 2px; padding-left: 6px;">
                                            <li id="li9"><b>
                                                <asp:LinkButton ID="lnkMenu9" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                    font-size: 10px" CommandArgument="POI">PO Issued</asp:LinkButton></b></li>
                                            <li id="li4"><b>
                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                    font-size: 10px" CommandArgument="DEL">Deleted</asp:LinkButton></b></li>
                                            <li id="li3"><b>
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                    font-size: 10px" CommandArgument="ALL">All Status</asp:LinkButton></b></li>
                                        </ul>
                                        <div class="ob_iLboICBR">
                                            <div class="ob_iLboICBRI">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdh">
                                Vessel :
                            </td>
                            <td class="tdd">
                                <ucDDL:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                                    Height="200" Width="160" />
                            </td>
                            <td class="tdh">
                                Logistic PO No. :
                            </td>
                            <td class="tdd">
                                <asp:TextBox ID="txtPoNumber" Width="160px" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdd" style="width: 15%">
                                <asp:Button ID="btnSearchPO" Text="Search" Height="24px" runat="server" OnClick="btnSearchPO_Click" />
                                &nbsp;
                                <asp:Button ID="btnClearFilter" Text="Clear Filter" Height="24px" runat="server"
                                    OnClick="btnClearFilter_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="txtSelMenu" runat="server"></asp:HiddenField>
                    <table width="100%" style="border-top: 1px solid #cccccc" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: right; padding-right: 10px">
                                <asp:CheckBox ID="chkShowAll" Checked="true" runat="server" AutoPostBack="true" Text="Show All"
                                    OnCheckedChanged="chkShowAll_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvOrderList" runat="server" AutoGenerateColumns="false" Width="100%"
                                    EmptyDataText="No record found !" EmptyDataRowStyle-ForeColor="Maroon" CssClass="gridmain-css"
                                    BackColor="#D8D8D8" CellPadding="5" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Logistic ID" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkPOID" Style="cursor: pointer" ForeColor="Blue" runat="server"
                                                    Text='<%#Eval("LOG_ID") %>' onclick='<%# "OpenPopupWindowBtnID(&#39;POP__Logistic_PO_Details&#39;, &#39;Logistic PO Details&#39;, &#39;LOG_Logistic_PO_Details.aspx?LOG_ID=" + Eval("LOG_ID").ToString() + "&IsApproved="+Eval("approved").ToString()+ "&IsApproving="+Eval("IsApproving").ToString()+   "&#39;,&#39;popup&#39;,800,920,null,null,false,false,true,false,&#39;"+btnSearchPO.ClientID+"&#39;);" %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Issued_Supplier" HeaderText="Issued Supplier" />
                                        <asp:BoundField DataField="Full_NAME" HeaderText="Agent/Forward Name" />
                                        <asp:BoundField DataField="Date_Of_Created" HeaderText="Created On" />
                                        <asp:BoundField DataField="PORT_NAME" HeaderText="Port Name" />
                                        <asp:BoundField DataField="hub" HeaderText="Hub Name" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <img id="imgbtnPurchaserRemark" runat="server" src="../Images/remark_new.gif" onclick='<%#"ASync_Get_Log_Remark(&#39;"+Eval("LOG_ID").ToString()+"&#39;,event,this,&#39;1&#39;);js_HideTooltip();" %>'
                                                    onmouseover='<%#"ASync_Get_Log_Remark(&#39;"+Eval("LOG_ID").ToString()+"&#39;,event,this,&#39;0&#39;)" %>'
                                                    title="All Remarks; Click to add new" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="First_Name" HeaderText="Pending With" />
                                        <asp:TemplateField HeaderText="Approval" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkApprovePO" Font-Names="verdana" Style="cursor: pointer" ForeColor="Blue"
                                                    Visible='<%# (Eval("approved").ToString()=="0"  || Eval("approved").ToString()=="2") && objUA.Approve != 0 && Eval("First_Name").ToString()!="" &&  Eval("active_status").ToString()=="1" ?true:false %>'
                                                    runat="server" Text="Approve" onclick='<%# "OpenPopupWindowBtnID(&#39;POP__Logistic_PO_Details&#39;, &#39;Logistic PO Details&#39;, &#39;LOG_Logistic_PO_Details.aspx?LOG_ID=" + Eval("LOG_ID").ToString() + "&IsApproving=1 &#39;,&#39;popup&#39;,800,920,null,null,false,false,true,false,&#39;"+btnSearchPO.ClientID+"&#39;);" %>'></asp:HyperLink>
                                                <asp:HyperLink ID="hlnkTrackApproval" Font-Names="verdana" Style="cursor: pointer"
                                                    ForeColor="Blue" Visible='<%# Eval("approved").ToString()=="0" || Eval("approved").ToString()=="2"  ||  Eval("active_status").ToString()=="0" ?false:true %>'
                                                    runat="server" Text="Track Approval" onmouseover='<%#"ASync_Get_Approval(&#39;"+Eval("LOG_ID").ToString()+"&#39;,event,this)" %>'></asp:HyperLink>
                                                <asp:HyperLink ID="hlnkPOIDAppr" Style="cursor: pointer" runat="server" ForeColor='<%# Eval("APPROVED").ToString()=="2"?System.Drawing.Color.Red:System.Drawing.Color.Blue %>'
                                                    onmouseover='<%#Eval("APPROVED").ToString()=="2"? "ASync_Get_Log_Remark(&#39;"+Eval("LOG_ID").ToString()+"&#39;,event,this,&#39;0&#39;,6)":"#" %>'
                                                    Text="Send For Approval" Visible='<%# Eval("First_Name").ToString()=="" && objUA.Approve != 0  &&  Eval("active_status").ToString()=="1" ?true:false %>'
                                                    onclick='<%# "OpenPopupWindowBtnID(&#39;POP__Logistic_PO_Details&#39;, &#39;Logistic PO Details&#39;, &#39;LOG_Logistic_PO_Details.aspx?LOG_ID=" + Eval("LOG_ID").ToString() + "&IsApproved="+Eval("approved").ToString()+"&#39;,&#39;popup&#39;,800,920,null,null,false,false,true,false,&#39;"+btnSearchPO.ClientID+"&#39;);" %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkPOCode" Style="cursor: pointer" ForeColor="Blue" runat="server"
                                                    Target="_blank" Text="Raise PO " NavigateUrl='<%# "~/Purchase/LOG_Raise_LogisticPO.aspx?LOG_ID="+ Eval("LOG_ID").ToString()%>'
                                                    Visible='<%# (Eval("approved").ToString()=="0"  || Eval("approved").ToString()=="2") || objUA.Approve == 0 || Eval("NoOfPONotRaised").ToString()=="0" ||  Eval("active_status").ToString()=="0" || Convert.ToString(ViewState["LOG_STATUS"])!="RAISEPO" ?false:true %>'></asp:HyperLink>
                                                <asp:Label ID="lblpostatus" runat="server" ForeColor="Black" Font-Bold="true" Text='<%# int.Parse(Eval("NoOfPORaised").ToString())>0?"PO Issued":"" %>'
                                                    Visible='<%# Convert.ToString(ViewState["LOG_STATUS"])=="RAISEPO" ?true:false %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="border: 0px">
                                                            <asp:Label ID="lblLPOSts" runat="server" Text='<%# Eval("LPOStatus") %>' Visible='<%# Convert.ToString(ViewState["LOG_STATUS"])=="0"?true:false %>'></asp:Label>
                                                        </td>
                                                        <td style="padding-left: 15px; border: 0px">
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;PURC_LIB_LOG_PO&#39;,&#39; LOG_ID="+Eval("LOG_ID")+"&#39;,event,this)" %>'
                                                                AlternateText="info" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                </asp:GridView>
                                <ucpager:ucCustomPager ID="ucCustomPagerPO" OnBindDataItem="BindDataItems" AlwaysGetRecordsCount="true"
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvInsRemark" style="width: 400px; display: none; position: absolute; border: 2px solid black;
            background-color: #F0F8FF;">
            <table width="100%">
                <tr>
                    <td colspan="2" style="width: 399px; border-bottom: 1px solid gray">
                        <div id="dvShowremark" style="width: 396px; position: relative; max-height: 150px;
                            overflow-y: auto; overflow-x: hidden">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        Remark :
                    </td>
                    <td>
                        <textarea id="txtNewRemark" rows="5" cols="35"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="2">
                        <input type="button" id="btnSaveRemark" value="Save" onclick="ASync_Ins_Log_Remark(event,this)" />
                        &nbsp;
                        <input type="button" id="btnCloseRemark" value="Close" onclick="javascript:document.getElementById('dvInsRemark').style.display = 'none';" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
