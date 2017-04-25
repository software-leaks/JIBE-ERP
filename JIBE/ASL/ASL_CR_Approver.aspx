<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_CR_Approver.aspx.cs" Inherits="ASL_ASL_CR_Approver" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Supplier Change Request Approver</title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ajax__htmleditor_editor_bottomtoolbar
        {
            display: none;
        }
        .cke_show_borders body
        {
            background: #FFFFCC;
            color: black;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function validation() {
            if (document.getElementById("txtRemarks").value == "") {
                alert("Remarks is mandatory field.");
                document.getElementById("txtRemarks").focus();
                return false;
            }

            return true;
        }
    </script>
     <script type="text/javascript">
         $(document).ready(function () {
             window.parent.$("#ASL_CR_Approver").css("height", (parseInt($("#pnlRemarks").height()) + 50) + "px");
             window.parent.$(".xfCon").css("height", (parseInt($("#pnlRemarks").height()) + 50) + "px").css("top", "50px");
         });
    </script>
</head>
<body style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 99%;">
<table  style="border: 0px solid #cccccc; font-family: Tahoma; overflow: Auto; font-size: 12px; width: 100%;">
                                 <tr><td>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <div class="error-message" onclick="javascript:this.style.display='none';">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
        <asp:Panel ID="pnlRemarks" runat="server" Visible="true">
         <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
       <asp:UpdatePanel ID="UpdatePanelRemarks" runat="server">
                <contenttemplate>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td align="left"  style="background: #cccccc; width: 100%">
                        <div id="Div1" class="page-title">
                           Supplier Change Request Approver
                        </div>
                    </td>
                </tr>
               
                <tr>
                    <td>
                        <div style="background-color: White;">
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
                                        <asp:TemplateField  HeaderText="Srno">
                                            <ItemTemplate>
                                              <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="15px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fields Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblColumn_Name" runat="server" Text='<%#Eval("Field_Display_Name")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="140px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <HeaderTemplate>
                                              Group Name
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
                                                <asp:Label ID="lblApprover_Name" runat="server" Text='<%#Eval("Approver_Name")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <HeaderTemplate>
                                             Final Approver Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblFinal_Approver_Name" runat="server" Text='<%#Eval("Final_Approver_Name")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                        </div>
                     
                    </td>
                    
                </tr>
            </table>
            </contenttemplate>
            </asp:UpdatePanel>
            </div>
        </asp:Panel>
    </div>
    </form> </td></tr></table>
</body>
</html>

