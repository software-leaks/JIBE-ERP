<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortCallTemplate.aspx.cs" Inherits="VesselMovement_PortCallTemplate" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Template</title>
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
   
   <script language="javascript" type="text/javascript">
       function refreshAndClose() {
           window.parent.ReloadParent_ByButtonID();
           window.close();
       }
    
    </script>
     <script type = "text/javascript">
         function Confirm() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Do you want to delete?")) {
                 confirm_value.value = "Yes";
             } else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
         }
    </script>
</head>
<body>

    <form id="form1" runat="server">

    <asp:ScriptManager ID="Script1" runat="server">
    </asp:ScriptManager>
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
        <center>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td  align="center">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltHeader" runat="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                    <div style=" background-color: White; overflow-x: hidden; overflow-y: Auto;">
                         <asp:GridView ID="gvTemplate" runat="server" EmptyDataText="NO RECORDS FOUND" ShowHeaderWhenEmpty="True" Width="100%"
                                                AutoGenerateColumns="False" DataKeyNames="ID" 
                             CellPadding="1" AllowSorting="True"
                                                OnRowCancelingEdit="gvTemplate_RowCancelingEdit" OnRowEditing="gvTemplate_RowEditing"
                                                OnRowUpdating="gvTemplate_RowUpdating" 
                             OnRowCommand="gvTemplate_RowCommand" onrowdeleting="gvTemplate_RowDeleting">
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="From Port">
                                                        <ItemTemplate>
                                                            <asp:Label ID="FromportName" runat="server" Text='<%# Eval("From_Port")%>'></asp:Label></ItemTemplate>
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                        <ItemStyle Wrap="True" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <EditItemTemplate >
                                                             <asp:Label ID="FromPort" runat="server"  Text='<%# Eval("From_Port")%>'></asp:Label></ItemTemplate>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sea Time (DD/HHMM)">
                                                        <EditItemTemplate>
                                                        <asp:HiddenField ID="txtSeaTime" runat="server"  Value='<%# Eval("SeaTime")%>'  ></asp:HiddenField>
                                                                <table>
                                                                <tr>
                                                                <td><asp:DropDownList runat="server" ID="ddlDD" ></asp:DropDownList></td>
                                                                <td>      <asp:DropDownList runat="server" ID="ddlHH" ></asp:DropDownList> </td>
                                                                <td>  <asp:DropDownList runat="server" ID="ddlMM" ></asp:DropDownList></td>
                                                                </tr>
                                                                </table>
                                                   
                                                            <%--<asp:TextBox ID="txtSeaTime" runat="server" Width="80%" Text='<%# Eval("SeaTime")%>'
                                                                MaxLength="255"></asp:TextBox>
                                                                
                                                                <asp:RegularExpressionValidator ID="revSeaTime" ControlToValidate="txtSeaTime"  Text="*" ValidationExpression="^[0-9][0-9](/)(0[0-9]|1[0-9]|2[0-3])[0-5][0-9]$" ErrorMessage="Please Enter Correct seatime format(day/hhmm)." ValidationGroup="ValidateTemplate" runat="server"></asp:RegularExpressionValidator>--%>
                                                                </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSeaTime" Visible="true" runat="server" Width="80px" Text='<%# Eval("SeaTime")%>'></asp:Label></ItemTemplate>
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                        <ItemStyle Wrap="True" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Port Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="InPortName" runat="server" Text='<%# Eval("To_Port")%>'></asp:Label></ItemTemplate>
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                        <ItemStyle Wrap="True" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <EditItemTemplate >
                                                             <asp:Label ID="ToPort" runat="server"  Text='<%# Eval("To_Port")%>'></asp:Label></ItemTemplate>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="InPort Time (DD/HHMM)">
                                                        <EditItemTemplate>
                                                            <%--<asp:TextBox ID="txtInportTime" runat="server"  Width="80%" Text='<%# Eval("InPortTime")%>'></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="revInportTime" ControlToValidate="txtInportTime" ValidationExpression="^[0-9][0-9](/)(0[0-9]|1[0-9]|2[0-3])[0-5][0-9]$"
                                                            
                                                            Text="*" ErrorMessage="Please Enter Correct Inport format(day/hhmm)." ValidationGroup="ValidateTemplate" runat="server"></asp:RegularExpressionValidator>--%>
                                                              <asp:HiddenField ID="txtInportTime" runat="server"  Value='<%# Eval("InPortTime")%>'  ></asp:HiddenField>
                                                                <table>
                                                                <tr>
                                                                <td><asp:DropDownList runat="server" ID="ddlDDI" ></asp:DropDownList></td>
                                                                <td>      <asp:DropDownList runat="server" ID="ddlHHI" ></asp:DropDownList> </td>
                                                                <td>  <asp:DropDownList runat="server" ID="ddlMMI" ></asp:DropDownList></td>
                                                                </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPortTime" Visible="true" runat="server" Width="80px" Text='<%# Eval("InPortTime")%>'></asp:Label></ItemTemplate>
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                        <ItemStyle Wrap="True" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Charterers Agent">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlCAgent" runat="server" Width="280px" DataValueField="Charter_ID"
                                                                DataTextField="CharterName" />
                                                            <asp:Label ID="lblc" runat="server" Visible="false" Width="1px" Text='<%# Eval("Charter_ID")%>'></asp:Label></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblChartName" Visible="true" runat="server" Width="300px" Text='<%# Eval("CharterName")%>'></asp:Label></ItemTemplate>
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                        <ItemStyle Wrap="True" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Owners Agent">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlOAgent" runat="server" DataValueField="Owners_ID" DataTextField="OwnerName"
                                                                Width="280px" />
                                                            <asp:Label ID="lblo" runat="server" Visible="false"  Width="300px" Text='<%# Eval("Owners_ID")%>'></asp:Label></EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblownersName" Visible="true" runat="server" Width="300px" Text='<%# Eval("OwnerName")%>'></asp:Label></ItemTemplate>
                                                        <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                        <ItemStyle Wrap="True" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Representative Email">
                                                    
                                                        <EditItemTemplate>
                                                            <asp:ImageButton runat="server" AlternateText="Save" ImageUrl="~/Images/Save.png"   Visible='<%# uaEditFlag %>'
                                                                CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdSave" ValidationGroup="ValidateTemplate"
                                                                ToolTip="Save"></asp:ImageButton><img id="Img2" runat="server" alt="" src="~/Images/transp.gif"
                                                                    width="3" /><asp:ImageButton runat="server" AlternateText="Cancel" ImageUrl="~/Images/CloseDiv.png" Visible='<%# uaDeleteFlage %>'
                                                                        CommandName="Cancel" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdCancel" 
                                                                        ToolTip="Cancel"></asp:ImageButton></EditItemTemplate>
                                                        <HeaderTemplate>
                                                            Action</HeaderTemplate>
                                                        <ItemTemplate>
                                                        <table>  
                                                        <tr> 
                                                        <td>  <asp:ImageButton runat="server" AlternateText="Edit" ImageUrl="~/Images/edit.gif" Visible='<%# uaEditFlag %>'
                                                                CommandName="EDIT" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdEdit"
                                                                 ToolTip="Edit"></asp:ImageButton><img
                                                                    id="Img1" runat="server" alt="" src="~/Images/transp.gif" width="3" />
                                                                  </td><td>
                                                                     <asp:ImageButton runat="server" AlternateText="Delete" ImageUrl="~/Images/Delete.png" Visible="false"  CommandName="Delete"
                                                               CommandArgument="<%# Container.DataItemIndex %>" ID="cmdDelete" OnClientClick="Confirm()"
                                                                 ToolTip="Delete"></asp:ImageButton><img
                                                                    id="ImgDelete" runat="server" alt="" src="~/Images/transp.gif" width="3" />
                                                                    </td>  </tr> </table>
                                                                    </ItemTemplate>
                                                                  
                                                                   

                                                        <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                <HeaderStyle CssClass="HeaderStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                            </asp:GridView>
                                            
                                            <asp:ValidationSummary ID="vdTemplate" ValidationGroup="ValidateTemplate" DisplayMode="List" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
                                            </div>
                    </td>
                </tr>
                <tr>
                <td align="center">
                <asp:Button ID="btnExit" Width="100px" Text="Exit" runat="server" 
                        OnClientClick="refreshAndClose();"  />
                </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
