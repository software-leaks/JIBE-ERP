<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false"  CodeFile="CP_Remarks.aspx.cs"
    Inherits="CP_Remarks" %>

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
                    <td align="center" colspan="7">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltPageHeader" Text ="Query/Remarks" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                <td width="10%" align="right">
                 <asp:Literal ID="ltAttentionTo" Text="Attention To :" runat="server"></asp:Literal>
                </td>
                <td align="right" class="style1" style="color: #FF0000; width:1% ">
                       &nbsp;
                 </td>
                    <td width="15%" align="left">
                        <asp:DropDownList ID="ddlUser" Width="150px" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="10%" align="right"> 
                        <asp:Literal ID="ltRemarks" Text="Remarks :" runat="server"></asp:Literal>
                    </td>
                    <td align="right" class="style1" style="color: #FF0000; width:1% ">
                      *
                     </td>
                    <td width="40%" align="left">
                    <asp:TextBox ID="txtRemarks" runat ="server" TextMode="MultiLine" Width="300px"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rqRemarks" runat="server" Display="None"
                    ErrorMessage="Remark is mandatory." ControlToValidate="txtRemarks" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td align="left" class="style1" style="color: #FF0000; width:23% ">
                                               <asp:Button ID="btnSave" runat="server" Width ="100px" ValidationGroup="vgSubmit"
                                Text="Add Remarks" onclick="btnSave_Click" />&nbsp;
                     <asp:ValidationSummary ID="vsRemarks" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"  runat ="server" ValidationGroup = "vgSubmit" />
                        </td>
                    </tr>

                    <tr>
                    <td colspan="7" align = "center" style="color: #FF0000;" >
                    
                    <asp:Literal ID="ltmessage" runat = "server"> </asp:Literal>
                    </td>
                    </tr>
                    <tr>
                         <td colspan="7" align="center">
                            <asp:GridView ID="gvRemarks" runat="server" AutoGenerateColumns="False" 
                                GridLines="Both" Width="98%" DataKeyNames="Remarks_ID" 
                                 onrowdatabound="gvRemarks_RowDataBound">
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" 
                                    HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Created By">
                                           <ItemTemplate>
                                            <asp:Label ID="lblCreatedBy" runat="server" 
                                                Text='<%# Eval("CreatedBy") %>' 
                                                Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" ForeColor="Blue" HorizontalAlign="Center" Width="15%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Remarks/Queries">
                                    <ItemTemplate>
                                         <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("Remarks") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Wrap="true" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Attention To">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActionBy" runat="server" 
                                                Text='<%# Eval("ActionBy") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="15%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" 
                                                Text='<%# Eval("Status") %>' Visible="true"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate >

                                                  <table cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td width="3%">
                                                            <asp:ImageButton ID="ibtnMarkRead" style="border: 0; width: 14px; height: 14px" Text="Mark read" OnCommand="ibtnMarkRead_Click"
                                                              CommandArgument='<%#Eval("[Remarks_ID]")%>' ForeColor="Black" Visible="false" ToolTip="Update Read"
                                                                ImageUrl="../Images/tasklist.gif" runat="server" />
                                                            </td>
                                                              <td  width="3%">
                                                                <asp:ImageButton ID="ibtnClose" runat="server" CommandArgument='<%#Eval("Remarks_ID") %>' Visible="false"
                                                                    ImageUrl="~/images/close-Dash.png" OnCommand="ibtnClose_Click"
                                                                    OnClientClick="return confirm('Are you sure want to Close?')" ToolTip="Close">
                                                                </asp:ImageButton>
                                                            </td>
                                                            <td  width="4%">
                                                                <asp:ImageButton ID="ibtnDelete" runat="server" CommandArgument='<%#Eval("Remarks_ID") %>'
                                                                    Visible='<%# uaDeleteFlage %>' ImageUrl="~/images/delete.png" OnCommand="ibtnDelete_Click"
                                                                    OnClientClick="return confirm('Are you sure want to delete?')" ToolTip="Delete">
                                                                </asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                    </table>


                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="10%" 
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