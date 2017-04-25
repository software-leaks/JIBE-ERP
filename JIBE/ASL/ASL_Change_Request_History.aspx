<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_Change_Request_History.aspx.cs" Inherits="ASL_ASL_Change_Request_History" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>Change Request History</title>
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
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            window.parent.$("#ASL_Evalution").css("height", (parseInt($("#pnl").height()) + 50) + "px");
            window.parent.$(".xfCon").css("height", (parseInt($("#pnl").height()) + 50) + "px").css("top", "50px");
        });
    </script>
</head>
<body style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 99%;">
<table  style="border: 0px solid #cccccc; font-family: Tahoma; overflow: Auto; font-size: 12px; width: 100%;">
                                 <tr><td>
    <form id="form1" runat="server">
  <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
        color: Black; height: 100%;">
         <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnl" runat="server" Visible="true">
        
           
                    <div id="Div1" class="page-title">
                        Change Request History
                    </div>
                
            <table width="100%" cellpadding="2" cellspacing="0">
            <tr>
                <td valign="top">
                   
                              <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <asp:GridView ID="gvSupplier" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                DataKeyNames="Supplier_Code" CellPadding="1" CellSpacing="0"
                                 GridLines="both"  CssClass="gridmain-css"
                                AllowSorting="true">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Created">
                                        <HeaderTemplate>
                                           Created
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server"  Text='<%#Eval("Proposed_By_Name")%>'></asp:Label>
                                       
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Field name">
                                        <HeaderTemplate>
                                            Field Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblField" runat="server" Text='<%#Eval("Field_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="9%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Old Value">
                                        <HeaderTemplate>
                                            Old Value
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOld" runat="server"  Text='<%#Eval("Old_Value")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="16%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="New Value">
                                        <HeaderTemplate>
                                            New Value
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNew" runat="server" Text='<%#Eval("New_Value")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="16%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Reason For Change">
                                        <HeaderTemplate>
                                            Reason For Change
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblReason" runat="server" Text='<%#Eval("Reason_For_Change")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="16%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>   
                                  <asp:TemplateField HeaderText="Status">
                                        <HeaderTemplate>
                                            Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="5%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Verified">
                                        <HeaderTemplate>
                                           Submitted By
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                        <asp:Label ID="lblVerified" runat="server"  Text='<%#Eval("Verify_By_Name")%>'></asp:Label>
                                         
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>  
                                     <asp:TemplateField HeaderText="Effected">
                                        <HeaderTemplate>
                                           Approved By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:Label ID="lblEffected" runat="server"  Text='<%#Eval("Approve_By_Name")%>'></asp:Label>
                                      
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>  
                                     <asp:TemplateField HeaderText="Effected">
                                        <HeaderTemplate>
                                            Final Approved By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:Label ID="lblEffected" runat="server"  Text='<%#Eval("FinalApprove_By_Name")%>'></asp:Label>
                                      
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="8%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>  
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindGrid" />
                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                        </div>  
                          
                </td>
            </tr>
        </table>
        </asp:Panel>
    </div>
    </form>
    </td></tr></table>
</body>
</html>
