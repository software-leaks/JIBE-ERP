<%@ Page Title="Provision Type" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Provision_Type.aspx.cs" Inherits="Purchase_Provision_Type" %>

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
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
 
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    function ValidationOnModifiyDepartment() {
        return true;
    }
    function ValidateOnSave() {
        if ($("#ddlSubSystem").val() == '0' || $("#ddlSubSystem").val() == '--SELECT--') {
            alert("Select SubSystem");
            $("#ddlSubSystem").focus();
            return false;
        }
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <center>
        <div style="font-family: Tahoma; font-size: 12px; width: 65%; height: 100%">
            <div class="page-title">
               Provision Type
            </div>
      
            <div style="border: 1px solid #cccccc; height: 650px;">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table align="right"><tr><td align="right" style="width: 5%;vertical-align:middle">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New SubSystem Provision Type" 
                                            ImageUrl="~/Images/Add-icon.png" onclick="ImgAdd_Click" />
                                    </td></tr></table><br />&nbsp;
                        <div>
                            <asp:GridView ID="gvProvision_Type" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="NO RECORDS FOUND" DataKeyNames="ProviID"
                                AutoGenerateColumns="False" 
                                Width="100%" GridLines="Both" AllowSorting="true"
                                CellPadding="2" CellSpacing="2" OnRowCancelingEdit="gvProvision_Type_RowCancelingEdit"
                                OnRowEditing="gvProvision_Type_RowEditing" OnRowUpdating="gvProvision_Type_RowUpdating"
                                 OnRowCommand="gvProvision_Type_RowCommand" OnRowDeleting="gvProvision_Type_RowDeleting">
                                 <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="25px"/>
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px"/>

                                <Columns>
                                    <asp:TemplateField HeaderText="SubSystem Description ">
                                        <HeaderTemplate>
                                            Sub System
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:Label ID="lblProviID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'></asp:Label>
                                            <asp:Label ID="lblSubsystem_Code" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Subsystem_Code") %>'></asp:Label>
                                            <asp:Label ID="lblSubsystem_Description" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Subsystem_Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Provision Type">
                                        <HeaderTemplate>
                                           Provision Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblProvisionType" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PROVISION_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                          <asp:DropDownList ID="ddlProvisionType" runat="server" Width="98%" Text='<%# DataBinder.Eval(Container,"DataItem.PROVISION_TYPE") %>'
                                                MaxLength="255">
                                             <asp:ListItem Value="FRESH">FRESH</asp:ListItem> 
                                             <asp:ListItem Value="DRY">DRY</asp:ListItem>   </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" AlternateText="Edit" ImageUrl="~/Images/edit.gif"
                                                CommandName="EDIT" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdEdit"
                                                Visible='<%# objUA.Edit ==0 ? false : true%>' ToolTip="Edit"></asp:ImageButton>
                                            <img id="Img1" runat="server" alt="" src="~/Images/transp.gif" width="3" />
                                            <asp:ImageButton runat="server" AlternateText="Delete" ImageUrl="~/Images/Delete.png"
                                                CommandName="DELETE" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdDelete" OnClientClick="return confirm('Are you sure want to Delete?')"
                                                Visible='<%# objUA.Delete == 0 ? false : true%>' ToolTip="Delete"></asp:ImageButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton runat="server" AlternateText="Save" ImageUrl="~/Images/Save.png"
                                                CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdSave"
                                                ToolTip="Save"></asp:ImageButton>
                                            <img id="Img2" runat="server" alt="" src="~/Images/transp.gif" width="3" />
                                            <asp:ImageButton runat="server" AlternateText="Cancel" ImageUrl="~/Images/Delete.png"
                                                CommandName="Cancel" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdCancel"
                                                ToolTip="Cancel"></asp:ImageButton>
                                        </EditItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div id="divAddItem" title ="<%=OperationMode%>" style="display:none; border: 1px solid Black;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 400px;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                            <tr><td align="center"><asp:Label ID="lblError" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label></td>
                            </tr>
                            <tr>
                            <td><table width="100%">
                            <tr><td>SubSystem :</td><td style="color:Red;">*</td><td><asp:DropDownList Width="150px" ID="ddlSubSystem" runat="server" ClientIDMode="Static"></asp:DropDownList></td></tr>
                            <tr><td>Provision Type :</td><td style="color:Red"></td><td><asp:DropDownList Width="150px" ID="ddlProviType" runat="server" ClientIDMode="Static"><asp:ListItem Value="FRESH">FRESH</asp:ListItem> 
                                             <asp:ListItem Value="DRY">DRY</asp:ListItem></asp:DropDownList></td></tr>
                            <tr><td align="center" colspan="3"><asp:Button  ID="btnSave" ClientIDMode="Static" OnClientClick="javascript:return ValidateOnSave();"
                                    runat="server" Text="Save" onclick="btnSave_Click"/></td></tr>
                            </table></td>
                            </tr>
                             <tr>
                                    <td style="color: #FF0000; font-size: small;" align="center">
                                        * Indicates as mandatory fields
                                    </td>
                                </tr>
                            </table>
                            
                        </div>
                        <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>

