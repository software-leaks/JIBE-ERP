<%@ Page Title="Group Setting" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ASL_Supplier_Group.aspx.cs" Inherits="ASL_ASL_Group_Setting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
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
   
      <script type="text/javascript" >
          function OpenScreen(ID, Eval_ID) {
              var url = 'ASL_Supplier_Group_Entry.aspx?Supp_ID=' + ID + '&Eval_ID=' + Eval_ID;
              OpenPopupWindowBtnID('Supplier_Group_Entry', 'Supplier Group Entry', url, 'popup', 700, 600, null, null, false, false, true, null, 'ctl00_MainContent_btnFilter');
          }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
            <div class="page-title">
                Group Setting
            </div>
            <div style="height: 100%; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 15%">
                                        
                                    </td>
                                    <td align="left" style="width: 30%">
                                      
                                    </td>
                                    <td align="center" style="width: 5%">
                                       
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" Visible="false" TabIndex="0" OnClick="btnFilter_Click"
                                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server"  ToolTip="Add New Group" OnClientClick='OpenScreen(null,null);return false;'
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td style="width: 5%">
                                       
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvSupplierGroup" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" OnRowDataBound="gvSupplierScope_RowDataBound" DataKeyNames="ORDINAL_POSITION"
                                    CellPadding="0" CellSpacing="2" Width="100%" GridLines="both" OnSorting="gvSupplierScope_Sorting"
                                    AllowSorting="true" CssClass="gridmain-css">
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Fields Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesc" runat="server" Text='<%#Eval("COLUMN_Desc")%>' Style="color: Black"></asp:Label>
                                                <asp:Label ID="lblFieldsName" runat="server" Visible="False" Text='<%#Eval("COLUMN_NAME")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Group Name">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlGroup" runat="server" Width="400px"  ></asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50%" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                          <table width="100%" cellpadding="4" cellspacing="4">
                                <tr><td><asp:Button ID="btnSubmit" runat="server" Width="200px" Text="Save" 
                                        onclick="btnSubmit_Click" /></td> </tr></table>
                        </div>
                     
                    </ContentTemplate>
                    
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
