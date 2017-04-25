<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PendingRequisitionPO.aspx.cs" Inherits="PendingRequisitionPO"
    Title="Pending Requisitions" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <script type="text/javascript">
   function GetRadioButtonValue(id)
        {
            var radio = document.getElementsByName(id);
            for (var j = 0; j < radio.length; j++)
            {
                if (radio[j].checked)
                    alert(radio[j].value);
            }
        }

    function PendingRequisitionPool()
    {
        javascript:window.open("PendingRequisitionDetails.aspx");
    }
    
    
    function PendingReqSendRFQ()
    {
        javascript:window.open("PendingRequisitionDetails.aspx?OptCode=0");
    }
    
    function PendingReqImportQuotation()
    {
        javascript:window.open("PendingRequisitionDetails.aspx?OptCode=1");
    }
    
    function PendingReqEvalution()
    {
        javascript:window.open("PendingRequisitionDetails.aspx?OptCode=2");
    }
    
    function PendingReqRaisePO()
    {
        javascript:window.open("PendingRequisitionDetails.aspx?OptCode=3");
    }
   
    function PendingReqPOConfirm()
    {
        javascript:window.open("PendingRequisitionDetails.aspx?OptCode=4");
    }
    
     function PendingReqUpdateDelivers()
    {
        javascript:window.open("PendingRequisitionDetails.aspx?OptCode=5");
    }
    </script>
    <script type="text/javascript"> function fixform() { if (opener.document.getElementById("aspnetForm").target != "_blank") return; opener.document.getElementById("aspnetForm").target = ""; opener.document.getElementById("aspnetForm").action = opener.location.href; }</script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>
                <table align="center" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="background-color: #808080; font-size: small; color: #FFFFFF;">
                            <b>Pending Requisition Details</b>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" align="center" style="height: 25px; width: 930px;
                    background-color: #C0C0C0;">
                    <tr>
                        <td width="100" align="left" style="background-color: #CCCCCC">
                        </td>
                        <td align="left" style="width: 77px; font-size: small; color: #333333; background-color: #CCCCCC;">
                            <b>Fleet : </b>
                        </td>
                        <td style="width: 116px; background-color: #CCCCCC;" align="left">
                            <b>
                                <asp:DropDownList ID="DDLFleet" runat="server" Width="109px" Font-Size="XX-Small"
                                    AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="0">-Select--</asp:ListItem>
                                </asp:DropDownList>
                            </b>
                        </td>
                        <td width="100" align="left" style="background-color: #CCCCCC">
                        </td>
                        <td width="100" align="left" style="background-color: #CCCCCC">
                        </td>
                        <td width="100" align="left" style="background-color: #CCCCCC">
                        </td>
                        <td width="100" align="left" style="background-color: #CCCCCC">
                        </td>
                        <td width="100" align="left" style="background-color: #CCCCCC">
                        </td>
                    </tr>
                    <tr>
                        <td width="100" align="left">
                        </td>
                        <td align="left" style="width: 45px; font-size: small; color: #333333;">
                            <b>Vessel :</b>
                        </td>
                        <td width="100" align="left">
                            <b>
                                <asp:DropDownList ID="DDLVessel" runat="server" Width="109px" Font-Size="XX-Small"
                                    AppendDataBoundItems="True">
                                    <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                            </b>
                        </td>
                        <td width="100" align="left">
                        </td>
                        <td width="100" align="left">
                        </td>
                        <td width="100" align="left">
                        </td>
                        <td width="100" align="left">
                        </td>
                        <td width="100" align="left">
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="height: 12px; width: 109px; background-color: #CCCCCC;">
                            <asp:RadioButtonList ID="optList" runat="server" Width="100px" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="optList_SelectedIndexChanged" Font-Size="XX-Small"
                                ForeColor="White" Style="color: #333333">
                                <asp:ListItem Text="Store" Selected="True"> 
                                </asp:ListItem>
                                <asp:ListItem Text="Spares"> </asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="width: 77px; font-size: small; color: #333333; background-color: #CCCCCC;"
                            align="left">
                            <b>Department :</b>
                        </td>
                        <td style="border: thin none #FFFFFF; background-color: #CCCCCC;" align="left">
                            <asp:DropDownList ID="cmbDept" runat="server" AutoPostBack="true" AppendDataBoundItems="True"
                                Width="109px" Font-Size="XX-Small" OnSelectedIndexChanged="cmbDept_OnSelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 80px; font-size: small; color: #FFFFFF; background-color: #CCCCCC;"
                            align="left">
                            <b style="color: #333333">Catalogue :&nbsp;&nbsp; </b>
                        </td>
                        <td style="border: thin none #FFFFFF; background-color: #CCCCCC; width: 110px;" align="left">
                            <asp:DropDownList ID="cmbCatalog" runat="server" AutoPostBack="true" AppendDataBoundItems="True"
                                Width="110px" Font-Size="XX-Small" OnSelectedIndexChanged="cmbCatalog_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="100" align="left" style="background-color: #CCCCCC">
                        </td>
                        <td width="100" align="left" style="background-color: #CCCCCC">
                        </td>
                        <td width="100" align="left" style="background-color: #CCCCCC">
                            <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Style="font-size: small"
                                Text="Refresh" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" align="center" style="width: 930px; background-color: #999999;">
                    <tr align="left">
                        <td style="background-color: #C0C0C0">
                            <asp:RadioButtonList ID="optRequiPendingType" runat="server" Width="800px" RepeatDirection="Horizontal"
                                AutoPostBack="True"  OnSelectedIndexChanged="optRequiPendingType_SelectedIndexChanged"
                                Font-Size="XX-Small" ForeColor="White" Style="font-weight: 700; color: #333333;">
                                <asp:ListItem Text="Send RFQ" Value="1" ></asp:ListItem>
                                <asp:ListItem Text="Upload Quotation" Value="2"> </asp:ListItem>
                                <asp:ListItem Text="Quotation Evaluation" Value="3"> </asp:ListItem>
                                <asp:ListItem Text="Raise Purchased Order " Value="4"> </asp:ListItem>
                                <asp:ListItem Text="Update Delivery" Value="5"> </asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="1" align="center" style="width: 900px; background-color: #CCCCCC;">
                    <tr align="center">
                        <td>
                            <div runat="server" style="height: 291px; overflow: scroll; margin-left: 0px; width: 930px;"
                                id="MainDiv">
                                <telerik:RadGrid ID="rgdPending" runat="server" Width="900px" ShowStatusBar="True"
                                    Skin="WebBlue" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True"
                                    OnDetailTableDataBind="RadGrid1_DetailTableDataBind" OnNeedDataSource="RadGrid1_NeedDataSource"
                                    GridLines="None">
                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                    <MasterTableView DataKeyNames="REQUISITION_CODE,document_code,Vessel_Code" AllowMultiColumnSorting="True"
                                        AllowPaging="False">
                                        <DetailTables>
                                            <telerik:GridTableView DataKeyNames="SUPPLIER" Name="Items" Width="100%">
                                                <RowIndicatorColumn Visible="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Visible="False" Resizable="False">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                 <Columns>
                       <telerik:GridBoundColumn  UniqueName ="QUOTATION_CODE"   DataField="QUOTATION_CODE" HeaderText="Quotation Code" Display="false"  >
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn UniqueName="SUPPLIER" DataField="SUPPLIER" HeaderText="Supplier Code">
                        </telerik:GridBoundColumn>
                 
                     <telerik:GridBoundColumn UniqueName="SHORT_NAME" DataField="SHORT_NAME" 
                           HeaderText="Supplier Name">
                       </telerik:GridBoundColumn>
                       
                       <telerik:GridBoundColumn UniqueName="Total_Item" DataField="Total_Item" 
                           HeaderText="Selected Items">
                       </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Currency" DataField="Currency" 
                           HeaderText="Currency">
                       </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn UniqueName="Total_Amount_Local" DataField="Total_Amount_Local" 
                           HeaderText="Total Amount">
                       </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn UniqueName="Total_Amount_Uds" DataField="Total_Amount_Uds" 
                           HeaderText="Total in USD">
                       </telerik:GridBoundColumn>
                     
                       <telerik:GridBoundColumn DataField="send_date" UniqueName="send_date" HeaderText="Approved Date">
                       </telerik:GridBoundColumn>
                       
                        <telerik:GridBoundColumn DataField="APPROVED_Status" UniqueName="APPROVED_Status" HeaderText="Approve Status">
                       </telerik:GridBoundColumn>
                       
                     <%-- <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="Select PO" AllowFiltering="false"  ItemStyle-HorizontalAlign="Left"  >
                        <ItemTemplate>
                                <asp:CheckBox ID="chkSendOrder" Checked ="false"  runat="server"  Font-Size="Smaller" />                                
                        </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </telerik:GridTemplateColumn>--%>
                    
                   <%--  <telerik:GridTemplateColumn HeaderText="Approve PO" UniqueName="ApprovePO">
                        <ItemTemplate>
                           <asp:ImageButton runat="server" ID="ImgApprove" OnCommand="onSelectApprovePO" CommandName="ApprovePO" OnClientClick="aspnetForm.target ='_blank'" CommandArgument='<%#Eval("QUOTATION_CODE") +","+ Eval("SUPPLIER")+","+Eval("APPROVED_Status") %>' ImageUrl="~/Technical/INV/Image/ApprovePO.gif">
                           </asp:ImageButton>
                            </ItemTemplate>
                       </telerik:GridTemplateColumn>--%>
                    
                     <%--  <telerik:GridTemplateColumn HeaderText="Preview" UniqueName="Preview">
                        <ItemTemplate>
                           <asp:ImageButton runat="server" ID="ImgPreviewBtn" OnCommand="onSelectAttachment" CommandName="Preview" OnClientClick="aspnetForm.target ='_blank'" CommandArgument='<%#Eval("QUOTATION_CODE") +","+ Eval("SUPPLIER") %>' ImageUrl="~/images/preview.gif">
                           </asp:ImageButton>
                            </ItemTemplate>
                       </telerik:GridTemplateColumn>--%>
                     
                 </Columns>
                                                <EditFormSettings>
                                                    <PopUpSettings ScrollBars="None"></PopUpSettings>
                                                </EditFormSettings>
                                            </telerik:GridTableView>
                                        </DetailTables>
                                        <RowIndicatorColumn Visible="False">
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Resizable="False">
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn SortExpression="REQUISITION_CODE" HeaderText="Requisition"
                                                DataField="REQUISITION_CODE" UniqueName="REQUISITION_CODE">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn SortExpression="requestion_Date" HeaderText="Sent Date"
                                                DataField="requestion_Date" UniqueName="requestion_Date">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn SortExpression="Name_Dept" HeaderText="Dept Name" DataField="Name_Dept"
                                                UniqueName="Name_Dept">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn SortExpression="TOTAL_ITEMS" HeaderText="Total Items" DataField="TOTAL_ITEMS"
                                                UniqueName="TOTAL_ITEMS">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgSelect" runat="server" Text="Select" OnCommand="onSelect"
                                                        CommandName="Select" CommandArgument='<%#Eval("REQUISITION_CODE")+"&Document_Code="+Eval("document_code") +"&Vessel_Code="+Eval("Vessel_Code")%>'
                                                        ForeColor="Black" OnClientClick="aspnetForm.target ='_blank';" ToolTip="View for next process to selected requistion"
                                                        ImageUrl="~/Technical/INV/Image/view.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="60px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Attachments" UniqueName="Attachments">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgAttachment" runat="server" Text="Select" OnCommand="onSelectAttachment"
                                                        CommandName="Select" CommandArgument='<%#Eval("[REQUISITION_CODE]")%>' ForeColor="Black"
                                                        ToolTip="Attachments" ImageUrl="~/Technical/INV/Image/attach1.gif" Width="16px"
                                                        Height="16px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="60px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn SortExpression="Attach_Status" Visible="false" HeaderText="Attach_Status"
                                                DataField="Attach_Status" UniqueName="Attach_Status">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn SortExpression="document_code" Visible="false" HeaderText="Document Code"
                                                DataField="document_code" UniqueName="document_code">
                                                <HeaderStyle HorizontalAlign="Left" Width="0px" />
                                                <ItemStyle Width="0px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn SortExpression="Vessel_Code" Visible="false" HeaderText="Vessel_Code"
                                                DataField="Vessel_Code" UniqueName="Vessel_Code">
                                                <HeaderStyle HorizontalAlign="Left" Width="0px" />
                                                <ItemStyle Width="0px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn SortExpression="code" Visible="false" HeaderText="code" DataField="code"
                                                UniqueName="code">
                                                <HeaderStyle HorizontalAlign="Left" Width="0px" />
                                                <ItemStyle Width="0px" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <PopUpSettings ScrollBars="None"></PopUpSettings>
                                        </EditFormSettings>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
