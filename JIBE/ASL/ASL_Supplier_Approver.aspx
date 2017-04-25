<%@ Page Title="Supplier Approver" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ASL_Supplier_Approver.aspx.cs" Inherits="ASL_ASL_Supplier_Approver" %>

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
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function validation() {
            if (document.getElementById("ctl00_MainContent_ddlApproverType").value == "0") {
                alert("Approver Type is mandatory field.");
                document.getElementById("ctl00_MainContent_ddlApproverType").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_ddlUserName").value == "0") {
                alert("User Name is mandatory field.");
                document.getElementById("ctl00_MainContent_ddlUserName").focus();
                return false;
            }
            

            return true;
        }
        function OpenScreen(ID, Eval_ID) {
            var url = 'ASL_CR_Approver.aspx?Supp_ID=' + ID + '&Eval_ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_CR_Approver', 'Supplier Change Request Approver', url, 'popup', 700, 600, null, null, false, false, true, null);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server" defaultbutton="btnFilter">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <progresstemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </progresstemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
            <div class="page-title">
                Supplier Approver
            </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <contenttemplate>
                        <div style="padding-top: 2px; padding-bottom: 10px; height: 70px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 20%">
                                       User Name :&nbsp;
                                    </td>
                                    <td align="left" style="width: 30%">
                                       <asp:TextBox ID="txtSearch" runat="server" MaxLength="50" Width="200px" CssClass="txtInput" onkeydown = "return (event.keyCode!=13);"></asp:TextBox>
                                      <%-- <asp:DropDownList ID="ddlFilter" runat="server" Width="100%" CssClass="txtInput"> 
                                           </asp:DropDownList>--%>
                    
                                    </td>
                                    <td align="right" style="width: 20%">
                                      Group Name :&nbsp;
                                    </td>
                                    <td align="left" style="width: 30%">
                                       
                                      <asp:DropDownList ID="ddlFilter" runat="server" Width="200px" CssClass="txtInput"> 
                                           </asp:DropDownList>
                    
                                    </td>
                                    
                                    
                                </tr>
                                <tr>
                                    <td align="right" style="width: 20%">
                                       Approver Type :&nbsp; </td>
                                    <td align="left" style="width: 30%">
                                          <asp:DropDownList ID="ddlFilterApproverType" runat="server" Width="200px" 
                                              CssClass="txtInput"> 
                                              <asp:ListItem Value="0" >--All--</asp:ListItem>
                                              <asp:ListItem Value="Evaluation" >Evaluation</asp:ListItem>
                                              <asp:ListItem Value="ChangeRequest">Change Request</asp:ListItem>
                                           </asp:DropDownList></td>
                                    <td align="right" style="width: 15%">
                                        &nbsp;</td>
                                    <td align="left" style="width: 30%">
                                       <asp:Button ID="btnGroup" runat="server" Text="ASL Column Group Relationship"  OnClientClick='OpenScreen(null,null);return false;'
                                             /></td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" TabIndex="0" OnClick="btnFilter_Click"
                                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" /></td>
                                    <td align="center" style="width: 5%">
                                         <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" /></td>
                                    <td align="center" style="width: 5%">
                                       <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Supplier Approver" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" /></td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to Excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" /></td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvSupplierApprover" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" OnRowDataBound="gvSupplierApprover_RowDataBound" DataKeyNames="ID"
                                    CellPadding="0" CellSpacing="2" Width="100%" GridLines="both" OnSorting="gvSupplierApprover_Sorting"
                                    AllowSorting="true" CssClass="gridmain-css">
                                       <HeaderStyle CssClass="HeaderStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                    <Columns>
                                        <asp:TemplateField Visible="false" HeaderText="Approver ID">
                                            <HeaderTemplate>
                                                 <asp:LinkButton ID="lblApprover" runat="server" CommandName="Sort" CommandArgument="Srno"
                                                    ForeColor="Black">Srno&nbsp;</asp:LinkButton>
                                                <img id="imgApprover" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                              <asp:Label ID="lblID" runat="server" Text='<%#Eval("Srno")%>' Style="color: Black"></asp:Label>
                                              
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <HeaderTemplate>
                                               <asp:LinkButton ID="lblUserApprover" runat="server" CommandName="Sort" CommandArgument="User_Name"
                                                    ForeColor="Black">Approver Name&nbsp;</asp:LinkButton>
                                                <img id="imgUserApprover" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUser_Name" runat="server" Text='<%#Eval("User_Name")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <HeaderTemplate>
                                              Approver Type
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblApprover_Type" runat="server" Text='<%#Eval("Approver_Type")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="110px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <HeaderTemplate>
                                               Change Request Group Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGroup_Name" runat="server" Text='<%#Eval("CR_Group_Name")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="140px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Super Approver">
                                            <HeaderTemplate>Approver
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                             <asp:Label ID="lblApprover" runat="server"  Text='<%#Eval("Approver")%>' ></asp:Label>
                                                <%--<asp:CheckBox ID="lblchk" runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("Approver")) %>'  />--%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Super Approver">
                                            <HeaderTemplate>Final Approver
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                             <asp:Label ID="lblFinalApprover" runat="server"  Text='<%#Eval("Final_Approver")%>' ></asp:Label>
                                                <%--<asp:CheckBox ID="lblchk" runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("Final_Approver")) %>'  /> Visible='<%# uaEditFlag %>'Visible='<%# uaDeleteFlage %>' --%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="3" cellspacing="3">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                               CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                 OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                    
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem=" BindSupplierApprover" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display:none; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 40%">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                               <tr>
                                    <td align="right" style="width: 15%">
                                       Approver Type &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 25%">
                                          <asp:DropDownList ID="ddlApproverType" runat="server" Width="50%" 
                                              CssClass="txtInput" AutoPostBack="true"
                                              onselectedindexchanged="ddlApproverType_SelectedIndexChanged"> 
                                              <asp:ListItem Value="0" >--SELECT--</asp:ListItem>
                                              <asp:ListItem Value="Evaluation" >Evaluation</asp:ListItem>
                                              <asp:ListItem Value="ChangeRequest">Change Request</asp:ListItem>
                                           </asp:DropDownList>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                       User Name &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 25%">
                                          <asp:DropDownList ID="ddlUserName" runat="server" Width="50%" CssClass="txtInput"> 
                                           </asp:DropDownList>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                       CR Group Name &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        
                                    </td>
                                    <td align="left" style="width: 25%">
                                          <asp:DropDownList ID="ddlGroupName" runat="server" Width="50%" CssClass="txtInput"> 
                                           </asp:DropDownList>
                                       
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" style="width: 15%">
                                       Approver  &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left" style="width: 25%">
                                          <asp:CheckBox ID="chkApprover" runat="server" Text="" />
                                       
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" style="width: 15%">
                                       Final Approver  &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        
                                    </td>
                                    <td align="left" style="width: 25%">
                                          <asp:CheckBox ID="chkFinalApprover" runat="server" Text="" />
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validation();"
                                            OnClick="btnsave_Click" />
                                        <asp:TextBox ID="txtApproverID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 0px solid #cccccc;
                                            background-color: #FDFDFD">
                                        </div>
                                    </td>
                                </tr>
                                  <tr>
                                    <td colspan="3" align="center" style="color: #FF0000; font-size: small;">
                                       <asp:Label ID="lblmsg" ForeColor="Red" runat="server"></asp:Label>
                                    </td>
                                </tr
                                <tr>
                                </tr>
                                <tr>
                                    <td align="right" colspan="3" style="color: #FF0000; font-size: small;">
                                        * Mandatory fields
                                    </td>
                                </tr>
                            </table>
                        </div>
                          <div id="divReport" title="<%= OperationMode %>" style="display:none; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 50%">
                                <asp:GridView ID="gvGroupColumn" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False"  DataKeyNames="ID"
                                    CellPadding="0" CellSpacing="2" Width="100%" GridLines="both" 
                                    AllowSorting="true" CssClass="gridmain-css">
                                       <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                    <Columns>
                                        <asp:TemplateField Visible="false" HeaderText="Approver ID">
                                            <ItemTemplate>
                                              <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Column Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblColumn_Name" runat="server" Text='<%#Eval("Field_Display_Name")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <HeaderTemplate>
                                              Group Description
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGroup_Description" runat="server" Text='<%#Eval("Group_Description")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="110px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="User Name">
                                            <HeaderTemplate>
                                              Approver Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGroup_Description" runat="server" Text='<%#Eval("Approver_Name")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="110px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                              
                            </div>
                          
                    </contenttemplate>
                    <triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
