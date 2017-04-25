<%@ Page Title="Items Inventory" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PURC_Items_Inventory.aspx.cs" Inherits="Purchase_PURC_Items_Inventory" %>

<%@ Register TagPrefix="RJS" Namespace="RJS.Web.WebControl" Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .tbl
        {
            border: 1px solid gray;
            height: 90px;
        }
        
        .tdh
        {
            text-align: right;
            padding: 1px 2px 1px 0px;
        }
        .tdd
        {
            text-align: left;
            padding: 1px 2px 1px 0px;
        }
        
        .css-MinQty
        {
            background-color: Red;
            color: White;
        }
        .css-MaxQty
        {
            background-color: Green;
            color: White;
        }
    </style>
    <script type="text/javascript">
        var lastExecutor = null;
        function asyncGet_Inventory_UpdatedBy(ID, Office_ID, Vessel_ID, evt, objthis) {

            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Inventory_UpdatedBy', false, { "ID": ID, "Office_ID": Office_ID, "Vessel_ID": Vessel_ID }, onSuccess_Get_Inventory_UpdatedBy, Onfail, new Array(evt, objthis));

            lastExecutor = service.get_executor();

        }

        function onSuccess_Get_Inventory_UpdatedBy(retVal, Args) {
            js_ShowToolTip_Fixed(retVal, Args[0], Args[1]);
        }

        function Onfail() {
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
       Inventory Items
    </div>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 40%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
      
            <table id="main" cellpadding="0" cellspacing="0" width="100%" style="background-color: #f4ffff;color: Black; vertical-align: top">
                <tr>
                    <td>
                        <table align="center" cellpadding="0" cellspacing="0" class="tbl" style="width: 100%">
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="tdh">
                                    Fleet :
                                </td>
                                <td class="tdd">
                                    <asp:DropDownList ID="DDLFleet" runat="server" Font-Size="12px" Width="110px" AppendDataBoundItems="True"
                                        AutoPostBack="True" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdh">
                                    Vessel :
                                </td>
                                <td class="tdd">
                                    <asp:DropDownList ID="DDLVessel" Font-Size="12px" runat="server" Width="110px" AppendDataBoundItems="True"
                                        TabIndex="1">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 2px">
                        &nbsp
                    </td>
                    <td>
                        <table class="tbl" style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="tdd">
                                    Department :
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="optList" runat="server" Width="200px" RepeatDirection="Horizontal"
                                        AutoPostBack="True" OnSelectedIndexChanged="optList_SelectedIndexChanged" ForeColor="Black"
                                        TabIndex="2">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdh">
                                    Department :
                                </td>
                                <td class="tdd">
                                    <asp:DropDownList ID="cmbDept" runat="server" AutoPostBack="true" Width="160px" OnSelectedIndexChanged="cmbDept_OnSelectedIndexChanged"
                                        Font-Size="12px" TabIndex="3" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdh">
                                    Catalogue :
                                </td>
                                <td class="tdd">
                                    <asp:DropDownList ID="ddlCatalogue" runat="server" AutoPostBack="true" Width="160px"
                                        OnSelectedIndexChanged="ddlCatalogue_OnSelectedIndexChanged" Font-Size="12px"
                                        TabIndex="3" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdh">
                                    Sub Catalogue :
                                </td>
                                <td class="tdd">
                                    <asp:DropDownList ID="ddlSubCatalogue" runat="server" Width="160px" Font-Size="12px"
                                        TabIndex="3" AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 2px">
                        &nbsp
                    </td>
                    <td>
                        <table class="tbl" style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="tdh">
                                    Draw No.
                                </td>
                                <td class="tdd">
                                    <asp:TextBox ID="txtDrawno" runat="server" Width="140px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdh">
                                    <asp:Label ID="lblSrchPartNo" runat="server" Text="Part No : "></asp:Label>
                                </td>
                                <td class="tdd">
                                    <asp:TextBox ID="txtSrchPartNo" runat="server" Width="140px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdh">
                                    <asp:Label ID="lblSrchDesc" runat="server" Text="Description :"></asp:Label>
                                </td>
                                <td class="tdd">
                                    <asp:TextBox ID="txtSrchDesc" runat="server" Width="140px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 2px">
                        &nbsp
                    </td>
                    <td>
                        <table class="tbl" style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="2" valign="baseline">
                                    Dates :
                                </td>
                            </tr>
                            <tr>
                                <td class="tdd" colspan="2">
                                    <asp:RadioButton ID="rbtnLatest" runat="server" GroupName="ltl" Checked="true" Text="Latest" />
                                    &nbsp; &nbsp;
                                    <asp:RadioButton ID="rbtnCustom" runat="server" GroupName="ltl" Text="Custom" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdh">
                                    From :
                                </td>
                                <td class="tdd">
                                    <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                                    <RJS:PopCalendar ID="calFrom" runat="server" Control="txtFrom" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdh">
                                    To :
                                </td>
                                <td class="tdd">
                                    <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                                    <RJS:PopCalendar ID="calTo" runat="server" Control="txtTo" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 2px">
                        &nbsp
                    </td>
                    <td>
                        <table class="tbl" style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    Min-Max Qty
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkInventory_Qty_Less" Text="Less than Min Qty" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkInventory_Qty_Greater" Text="Greater than Max Qty" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 2px">
                        &nbsp
                    </td>
                    <td>
                        <table class="tbl" style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="2">
                                    Items :
                                </td>
                            </tr>
                            <tr>
                                <td class="tdd">
                                    <asp:RadioButton ID="rbtnItemAll" GroupName="itm" runat="server" Checked="true" Text="Show All" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdd">
                                    <asp:RadioButton ID="rbtnRob" GroupName="itm" runat="server" Text="ROB > 0" />
                                </td>
                            </tr>
                             <tr>
                                <td class="tdd">
                                  <%--  <asp:RadioButton ID="rbCritical" GroupName="itm" runat="server" Text="Critical" />--%>
                                    <asp:CheckBox ID="chkCritcal" runat="server"  Text="Critical"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 2px">
                        &nbsp
                    </td>
                    <td align="center">
                        <table class="tbl" style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center">
                                    <asp:ImageButton ID="btnExport" ToolTip="Export To Excel" ImageUrl="~/Images/Exptoexcel.png" Height="25px" OnClick="btnExport_Click"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnSearch" runat="server" Height="24px" ToolTip="Search" Text="Search"
                                        OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="updINVItems" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="gvInventoryItems" AutoGenerateColumns="false" runat="server" Width="100%"
                            CssClass="gridmain-css" CellPadding="4" CellSpacing="0" GridLines="None" OnRowDataBound="gvInventoryItems_RowDataBound">
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                            <Columns>
                                <asp:BoundField DataField="Vessel_Name" HeaderText="Vessel" />
                                <asp:BoundField DataField="System_Description" ItemStyle-Width="150px" HeaderText="Catalogue" />
                                <asp:BoundField DataField="Subsystem_Description" ItemStyle-Width="150px" HeaderText="Sub Catalogue" />
                                <asp:BoundField DataField="Drawing_Number" HeaderText="Draw. No." />
                                <asp:BoundField DataField="Part_Number" HeaderText="Part Number" />
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" Style="cursor: default" Text='<%#Convert.ToString(Eval("Short_Description")).Length>50?Convert.ToString(Eval("Short_Description")).Substring(0,49)+"..." : Convert.ToString(Eval("Short_Description")) %>'
                                            onmousemove='<%# "js_ShowToolTip(&#39;<div style=width:100%;height:100%;margin:5px;background-color:transparent><b>Short Desc :</b>"+Convert.ToString(Eval("Short_Description"))+"<br><b>Long Desc :</b>"+Convert.ToString(Eval("Long_Description"))+"</div>&#39;,event,this)" %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="" HeaderText="" />
                                <asp:BoundField DataField="Unit_and_Packings" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Unit" />
                                <asp:TemplateField HeaderText="ROB" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblROB" runat="server" Style="cursor: default" Text='<%#Eval("Inventory_Qty")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Date_Of_Creatation" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="Update Date" />
                                <asp:BoundField DataField="Requisition_Code" HeaderText="Reqsn. No." />
                                <asp:BoundField DataField="REQUESTED_QTY" ItemStyle-HorizontalAlign="Center" HeaderText="Req Qty." />
                                <asp:TemplateField HeaderText="Adr">
                                    <ItemTemplate>
                                        <asp:Image ID="imgaddress" ImageUrl="~/Images/Item-Location-icon.png" runat="server" ImageAlign="Bottom" Visible='<%# Eval("LOCATION").ToString().Length > 0?true:false %>' ToolTip="click to view item address"
                                            onclick='<%# "js_ShowToolTip_Fixed(&#39;<div style=padding:10px >"+Eval("Location").ToString()+"</div>&#39;,event,this)" %>'
                                            Height="16px" />
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Image ID="ImgUpdatedBy" ImageUrl="~/Images/Users.gif" runat="server" ImageAlign="Bottom" ToolTip="click to view record log"
                                            onclick='<%# "asyncGet_Inventory_UpdatedBy("+Eval("ID").ToString()+","+ Eval("Office_ID").ToString()+","+Eval("Vessel_ID").ToString()+",event,this)" %>'
                                            Height="16px" />
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <uc1:ucCustomPager ID="ucCustomPagerItems" PageSize="20" CurrentPageIndex="1" OnBindDataItem="BindItems"
                            runat="server" />
                    </td>
                </tr>
            </table>
            <table align="left" width="100%">
                <tr>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>
