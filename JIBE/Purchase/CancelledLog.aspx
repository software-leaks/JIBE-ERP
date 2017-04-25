<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CancelledLog.aspx.cs" Inherits="Purchase_CancelledLog"
    EnableEventValidation="false" %>
    <%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <style type="text/css">
        body 
        {
            background-color: White;
        } 
    </style>
</head>
<body style="padding: 0px; margin: 0px;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="mg1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <telerik:RadGrid ID="rgdCancelledLog" runat="server" Width="100%" HeaderStyle-Height="30px"
                    ShowStatusBar="True" HeaderStyle-HorizontalAlign="Center" AlternatingItemStyle-BackColor="#CEE3F6"
                    Skin="Office2007" AutoGenerateColumns="False" AllowSorting="true"  
                     GridLines="None"
                    OnItemDataBound="rgdCancelledLog_ItemDataBound" OnNeedDataSource="rgd_NeedDataSource" 
                    OnSortCommand="rgdCancelledLog_SortCommand">
                    <MasterTableView DataKeyNames="REQUISITION_CODE,document_code,Vessel_Code" AllowMultiColumnSorting="True"
                        Width="100%" AllowPaging="false">
                        <Columns>
                            <telerik:GridTemplateColumn SortExpression="Vessel_name" HeaderText="Vessel" DataField="Vessel_name"
                                UniqueName="Vessel_name">
                                <ItemTemplate>
                                    <a href="../Crew/CrewList.aspx" target="_blank">
                                        <%#Eval("Vessel_name")%></a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Requisition" SortExpression="REQUISITION_CODE">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgPriority" runat="server" ToolTip="Urgent" ImageUrl="~/Images/exclamation.gif"
                                        Height="12px"></asp:ImageButton>
                                    <a href="CancelledReqsnSummary.aspx?<%#"REQUISITION_CODE="+Eval("REQUISITION_CODE")+"&Document_Code="+Eval("Document_CODE") +"&Vessel_Code="+Eval("Vessel_Code")%>"
                                        target="_blank">
                                        <%# DataBinder.Eval(Container.DataItem, "REQUISITION_CODE")%></a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Order No." SortExpression="ORDER_CODE">
                                <ItemTemplate>
                                <asp:HyperLink ID="lnkCancelPO" Target="_blank" runat="server" File_Path='<%#Eval("File_Path")%>'><%# DataBinder.Eval(Container.DataItem, "ORDER_CODE")%></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn SortExpression="Name_Dept" HeaderText="Department/Function" DataField="Name_Dept"
                                AllowSorting="true" UniqueName="Name_Dept">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="SUPPLIER" HeaderText="Supplier" AllowSorting="true"
                                DataField="SUPPLIER" UniqueName="SupplierCode" Display="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SYSTEM_Description" SortExpression="SYSTEM_Description"
                                DataField="SYSTEM_Description" HeaderText="Catalogue/System">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Supplier">
                                <ItemTemplate>
                                    <a href="ViewSupplierDetails.aspx?SupplierCode=<%# Eval("SUPPLIER") %> " target="_blank">
                                        <%# DataBinder.Eval(Container.DataItem, "SHORT_NAME")%></a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridBoundColumn SortExpression="Document_Date" HeaderText="Receival Date"
                                AllowSorting="true" DataField="requestion_Date" UniqueName="requestion_Date">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="TOTAL_ITEMS" HeaderText="Items" DataField="TOTAL_ITEMS"
                                AllowSorting="true" UniqueName="TOTAL_ITEMS">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="Lead_Time" HeaderText="Lead Time" DataField="Lead_Time"
                                AllowSorting="false" UniqueName="Lead_Time">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="URGENCY_CODE" HeaderText="Urgency" DataField="URGENCY_CODE"
                                AllowSorting="false" UniqueName="URGENCY_CODE" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="CalcelationType" HeaderText="Actions">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="text-align: left; border: 0px solid white">
                                                <asp:Label ID="lblclremark" Text='<%#Eval("clRemark") %>' Visible="false" runat="server"></asp:Label>
                                                <asp:Label ID="lblStatus" Text='<%# (Eval("ORDER_CODE")=="" ||Eval("ORDER_CODE")=="NULL")  ?"Cancelled-Reqsn" :"Cancelled-PO" %>'
                                                    runat="server"> </asp:Label>
                                            </td>
                                            <td style="text-align: right; border: 0px solid white">
                                                <asp:Image ID="imgStatus" runat="server" ImageUrl="~/Purchase/Image/view1.gif" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="activatePOReqsn">
                                <ItemTemplate>
                                    <asp:Button ID="btnActivatePO" Font-Size="10px" runat="server" Text="Activate PO"
                                        OnClick="btnActivatePO_Click" CommandArgument='<%#Eval("ORDER_CODE")%>' Visible='<%# (Eval("ORDER_CODE")=="" ||Eval("ORDER_CODE")=="NULL") ?false :true %>'
                                        Enabled='<%# (Eval("ORDER_CODE")=="" ||Eval("ORDER_CODE")=="NULL") ?false :true %>' />
                                    <asp:Button ID="btnActivateReqsn" runat="server" Text="Activate Reqsn" OnClick="btnActivateReqsn_Click"
                                        CommandArgument='<%#Eval("Document_CODE")%>' Font-Size="10px" Visible='<%# (Eval("ORDER_CODE")=="" ||Eval("ORDER_CODE")=="NULL")  ?true :false %>'
                                        Enabled='<%# (Eval("ORDER_CODE")=="" ||Eval("ORDER_CODE")=="NULL") ?false :true %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <EditFormSettings>
                            <PopUpSettings ScrollBars="None"></PopUpSettings>
                        </EditFormSettings>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="false" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
        <tr>
        <td>
        
         <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindData" />
        </td>
        </tr>
    </table>
    </form>
</body>
</html>
