<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CP_Hire_Remittance.aspx.cs" Inherits="CharterParty_CP_Hire_Remittance" EnableEventValidation="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>


     <script type="text/javascript">
         var previousRow;
         function ChangeRowColor(row) {
             if (previousRow == row)
                 return;
             else if (previousRow != null)
           
                 document.getElementById(previousRow).style.backgroundColor = "#ffffff";


             document.getElementById(row).style.backgroundColor = "#ABCDEF";
         
             previousRow = row;
         }

         function isNumberKey(evt) {
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
                 return false;

             return true;
         }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltPageHeader" Text ="Charter Hire Remittance Receipts" runat ="server" ></asp:Literal>
                        </div>
                           <asp:UpdatePanel ID="UpdatePanelRemarks" runat="server">
                                        <ContentTemplate>
             <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
        <table width="100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td>
                    <center>
                    <font size="2">
                   
                    This module is created for the update of Charterers remittances which will be matched against Hire Invoices that has been billed.<br />
                          The Accounts department shall be responsible for the update of remittance entries into the database.<br />
                         All entries made cannot be amended and incase of any errors, please email Chartering Dept for the required changes.
                        </ul>
                        </font>
                        </center>
                    </td>
                    </tr>
                    </table>
                    <table align="center">
                   <tr>
                   <td colspan="4" width="100%">
                   <center>
                   <table>
                    <tr>
                    <td>Vessel Name</td>
                    <td>
                        <asp:DropDownList ID="ddlVessel" runat="server">
                        </asp:DropDownList>
                    </td>
                   
                    <td>Charterer</td>
                    <td>
                        <asp:DropDownList ID="ddlCharterer" runat="server">
                        </asp:DropDownList>
                    </td>
                  
                    <td>CP Status</td>
                    <td>
                        <asp:DropDownList ID="ddlCPStatus" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                    <asp:Button ID="btnFilter" Text="Search" runat="server" onclick="btnFilter_Click" />
                    </td>
                    </tr>
                   </table>
                   </center>
                   </td>
                   </tr>
                    </table>
                    </div>
                    <div id="divRemittance">
                         <asp:GridView ID="gvSupplier" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                CellPadding="0" CellSpacing="0" AllowPaging="true"
                                Width="100%" GridLines="both"  CssClass="gridmain-css"
                                AllowSorting="true" DataKeyNames= "Charter_ID"
                             onpageindexchanging="gvSupplier_PageIndexChanging" 
                             onselectedindexchanged="gvSupplier_SelectedIndexChanged" 
                             onrowcommand="gvSupplier_RowCommand" onrowdatabound="gvSupplier_RowDataBound"
                             >
                           
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        ID
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:LinkButton ID="lblCharter"  runat="server" CommandArgument="Charter_ID" CommandName="select"  Text='<%# Eval("Charter_ID")%>' OnClick="lblCharter_Click" ></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        Vessel Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblVessel" runat="server"  Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                                        <asp:Label ID="lblVesselID" runat="server"  Text='<%# Eval("Vessel_ID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                       Charterer Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblCht" runat="server"  Text='<%# Eval("Charterer_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        CP Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblCPDate" runat="server"  Text='<%# Eval("CP_Date","{0:dd/MM/yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        CP Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblStatus" runat="server"  Text='<%# Eval("CP_status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        Delivery
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblDDate" runat="server"  Text='<%# Eval("Delivery_Date","{0:dd/MM/yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        ReDelivery
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblRDate" runat="server"  Text='<%# Eval("Redelivery_Date","{0:dd/MM/yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        Address Comm
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblAcomm" runat="server"  Text='<%# Eval("Address_Comm")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        Brokerage Comm
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblBComm" runat="server"  Text='<%# Eval("Brokerage_Comm")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        Hire Rate
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblHire" runat="server"  Text='<%# Eval("Hire_Rate")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        Last Remittance Received
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblReceipt" runat="server"  Text='<%# Eval("Last_Receipt","{0:dd/MM/yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        Amount Received
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblAmount" runat="server"  Text='<%# Eval("Amount_Received")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        Unmatched Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblOverDue" runat="server"  Text='<%# Eval("OverDue_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        Available  Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblAvailable" runat="server"  Text='<%# Eval("Available")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    </Columns>
                                    </asp:GridView>
                    </div>
                    <div id="divAdd" align="center" runat="server" visible="false">
                   <table>
                    <tr>
                    <td>Amount</td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" ControlToValidate="txtAmount" runat="server" ErrorMessage="Amount is required" ValidationGroup="valid"></asp:RequiredFieldValidator>
                    </td>
                   
                    <td>Received On</td>
                    <td>
                        <asp:TextBox ID="txtReceivedDate" runat="server" Width="120px"  onkeypress="return isNumberKey(event)" CssClass="txtInput" ValidationGroup="valid"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalStartDate" runat="server" TargetControlID="txtReceivedDate"
                                                    Format="dd-MM-yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="txtReceivedDate" ErrorMessage="Received Date is Required" ValidationGroup="valid" ></asp:RequiredFieldValidator>
                    </td>
                  
                    <td>Remarks</td>
                    <td>
                         <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  Display="None"  ControlToValidate="txtRemarks" ErrorMessage="Remark is required" ValidationGroup="valid"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    <asp:Button ID="btnRemittance" Text="Add Remittance" runat="server" ValidationGroup="valid"
                            onclick="btnRemittance_Click" /><asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List"  ShowMessageBox="true"  ShowSummary="false" ValidationGroup="valid" runat="server" />
                    </td>
                    </tr>
                   </table>
                    </div>
                    <div id="divGridRemarks" runat="server" visible="false">
                    <asp:GridView ID="gridRemarks" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                              CellPadding="0" CellSpacing="0" DataKeyNames="Remittance_ID"
                                Width="100%" GridLines="both"  CssClass="gridmain-css"
                                AllowSorting="true" onrowcancelingedit="gridRemarks_RowCancelingEdit" 
                            onrowcommand="gridRemarks_RowCommand" onrowediting="gridRemarks_RowEditing" 
                            onrowupdating="gridRemarks_RowUpdating"    >
                           
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        RID
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblRemID" Text='<%# Eval("Remittance_ID")%>' runat="server" ></asp:Label>
                                        </ItemTemplate>
                                     
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                       Amount Received
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblAmntRcv" Text='<%# Eval("Amount_Received")%>' runat="server" ></asp:Label>
                                        </ItemTemplate>
                                           <EditItemTemplate>
                                          <asp:TextBox ID="lblAmntRcv" Text='<%# Eval("Amount_Received")%>' onkeypress="return isNumberKey(event)" runat="server" ></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        Received Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblRIDate" Text='<%# Eval("Received_Date")%>' runat="server" ></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                           <asp:TextBox ID="lblRIDate" Text='<%# Eval("Received_Date")%>' runat="server" ></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalStartDate2" runat="server" TargetControlID="lblRIDate"
                                                    Format="dd-MM-yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                        </EditItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                       Journal ID
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblJID" Text='<%# Eval("Journal_Id")%>' runat="server" ></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                       Allocated Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblAllAmount" Text='<%# Eval("Allocated_Amount")%>' runat="server" ></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                       Amount Available
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblAvAmount" Text='<%# Eval("Amount_Available")%>' runat="server" ></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Right" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <HeaderTemplate>
                                        <table>
                                        <tr>
                                        <td>Remarks</td>
                                         <td> <asp:Button ID="btnUpdate" runat="server" Text="Update" /></td>
                                        </tr>
                                        </table>
                                       
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                       <asp:Label ID="lblRemRemarks" Text='<%# Eval("Remittance_Remarks")%>' runat="server" ></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                           <asp:TextBox ID="lblRemRemarks" Text='<%# Eval("Remittance_Remarks")%>' runat="server" ></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Type">

                              
                                                          <EditItemTemplate>
                                                            <asp:ImageButton runat="server" AlternateText="Save" ImageUrl="~/Images/Save.png"  
                                                                CommandName="UPDATE" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdSave" ValidationGroup="ValidateTemplate"
                                                                ToolTip="Save"></asp:ImageButton><img id="Img2" runat="server" alt="" src="~/Images/transp.gif"
                                                                    width="3" /><asp:ImageButton runat="server" AlternateText="Cancel" ImageUrl="~/Images/cancel.gif" 
                                                                        CommandName="Cancel" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdCancel" 
                                                                        ToolTip="Cancel"></asp:ImageButton></EditItemTemplate>
                                        <HeaderTemplate>
                                                            Action
                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                       <asp:ImageButton runat="server" AlternateText="Edit" ImageUrl="~/Images/edit.gif" 
                                                                CommandName="EDIT" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdEdit"
                                                                 ToolTip="Edit"></asp:ImageButton>
                                                                
                                                                    </ItemTemplate>
                                                                  
                                                                   

                                                       <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>                
                                    </Columns>
                                    </asp:GridView>




                    </div>
                    </ContentTemplate></asp:UpdatePanel>
</asp:Content>

