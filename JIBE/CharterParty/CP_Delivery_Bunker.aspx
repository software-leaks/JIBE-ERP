<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="CP_Delivery_Bunker.aspx.cs"
    Inherits="CP_Delivery_Bunker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;
    height: 100%;">
    <form id="form1" runat="server">
    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
            <table width="100%" cellpadding="2" cellspacing="0" >
                <tr>
                    <td align="center" colspan="5">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltPageHeader" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                <td width="20%">
                </td>
                <td width="23%" align="right">
                    <asp:Literal ID="ltFuel" runat="server" Text="Fuel Type :"></asp:Literal>
                </td>
                    <td align="right" class="style1" style="color: #FF0000; width:2% ">
                        *
                    </td>
                    <td width="25%" align="left">
                        <asp:DropDownList ID="ddlFuelType" Width="150px" runat="server">
                        </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvFuelType" runat="server" Display="None" InitialValue="0"
                    ErrorMessage="Fuel Type is mandatory." ControlToValidate="ddlFuelType" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                        <td width="20%">  &nbsp;
                        </td>
                    </tr>
                <tr>
                <td width="20%">
                </td>
                <td width="23%" align="right">
                    <asp:Literal ID="ltUnit" runat="server" Text="Unit :"></asp:Literal>
                </td>
                  <td align="right" class="style1" style="color: #FF0000; width:2% ">
                        *
                    </td>
                    <td width="25%" align="left">
                        <asp:TextBox ID="txtUnit" runat="server" Text="0.0000"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" 
                    ErrorMessage="Unit is mandatory."  ControlToValidate="txtUnit" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegInvoiceValue" runat="server" ErrorMessage="Unit value is not valid"
                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtUnit"
                                    ForeColor="Red" ValidationExpression="^[0-9.-]+$">
                                </asp:RegularExpressionValidator>
                    </td>
                        <td width="20%" align="left">  &nbsp; MT
                        </td>
                    </tr>

               <tr>
                <td width="20%">
                </td>
                <td width="23%" align="right">
                    <asp:Literal ID="Literal1" runat="server" Text="Price/Unit :"></asp:Literal>
                </td>
                   <td align="right" class="style1" style="color: #FF0000; width:2% ">
                        *
                    </td>
                    <td width="25%" align="left">
                        <asp:TextBox ID="txtPricePerUnit" runat="server" Text="0.0000"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  Display="None"
                    ErrorMessage="Unit price is mandatory." ControlToValidate="txtPricePerUnit" ValidationGroup="vgSubmit" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rgvPricePerUnit" runat="server" ErrorMessage="Price/Unit value is not valid."
                Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtPricePerUnit"
                ForeColor="Red" ValidationExpression="^[0-9.-]+$">
            </asp:RegularExpressionValidator>
                    </td>
                        <td width="20%" align="left">  &nbsp; USD/MT
                        </td>
                    </tr>

                    <tr>
                    <td colspan="5" align = "center" style="color: #FF0000;" >
                    
                    <asp:Literal ID="ltmessage" runat = "server"> </asp:Literal>
                    </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">

                        </td>
                        <td colspan="3" align="center">
                       <asp:Button ID="btnSave" runat="server" Width ="100px" ValidationGroup="vgSubmit"
                                Text="Save" onclick="btnSave_Click" />&nbsp;
                        <asp:Button ID="btnSaveClose" runat="server" ValidationGroup="vgSubmit" Visible="false"
                                Text="Save & Close" onclick="btnSaveClose_Click" />
                     <asp:ValidationSummary ID="vsDelivery" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"  runat ="server" ValidationGroup = "vgSubmit" />
                        </td>
                    </tr>
                    <tr>
                         <td colspan="5" align="center">
                            <asp:GridView ID="gvDeliveryBunker" runat="server" AutoGenerateColumns="False" 
                                GridLines="Both" Width="98%" DataKeyNames="Delivery_Bunker_ID">
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Fuel">
                                           <ItemTemplate>
                                            <asp:Label ID="lblfileName" runat="server" 
                                                Text='<%# DataBinder.Eval(Container,"DataItem.Fuel_Name") %>' 
                                                Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="30%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Amount(MT)">
                                    <ItemTemplate>
                                         <asp:Label ID="lblAmt" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Fuel_Amt") %>'
                                                ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Wrap="true" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price/Unit(USD/MT)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileId" runat="server" 
                                                Text='<%# DataBinder.Eval(Container,"DataItem.Unit_Price") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="30%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate >

                                                  <table cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td>
                                                            <asp:ImageButton ID="ImageButton1" style="border: 0; width: 14px; height: 14px" Text="Update" OnCommand="onUpdate"
                                                             Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Delivery_Bunker_ID]")%>' ForeColor="Black"
                                                                ImageUrl="../Images/edit.gif" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="lbtnDelete" runat="server" CommandArgument='<%#Eval("Delivery_Bunker_ID") %>'
                                                                    Visible='<%# uaDeleteFlage %>' ImageUrl="~/images/delete.png" OnCommand="lbtnDelete_Click"
                                                                    OnClientClick="return confirm('Are you sure want to delete?')" ToolTip="Delete">
                                                                </asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                    </table>


                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="5%" 
                                            Wrap="False" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>

</table>
</center>
</div>
</form>
</body>
</html>