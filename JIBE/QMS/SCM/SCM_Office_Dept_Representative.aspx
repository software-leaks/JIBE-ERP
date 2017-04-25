<%@ Page Title="Master’s Review Response Representative" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SCM_Office_Dept_Representative.aspx.cs" Inherits="QMS_SCM_SCM_Office_Dept_Representative" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">


        function ValidationOnModifiyDepartment() {

            var Department = document.getElementById("ctl00_MainContent_DivResponseDDLDeptpartment").value;

            if (Department == "0") {
                alert('Department is required.')
                return false;
            }

            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 <asp:UpdateProgress ID="upUpdateProgress" runat="server">
                <ProgressTemplate>
                    <div id="blur-on-updateprogress">
                        &nbsp;</div>
                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                        color: black">
                        <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
    <center>
    
        <div style="font-family: Tahoma; font-size: 12px; width: 65%; height: 100%">
           <div class="page-title">
              Master’s Review Response Representative
         </div>
            <div style="border: 1px solid #cccccc;height: 650px;">
                <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div>
                            <asp:GridView ID="gvOffRespresentative" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" OnRowDataBound="gvOffRespresentative_RowDataBound"
                                Width="100%" GridLines="Both" AllowSorting="true" OnSorting="gvOffRespresentative_Sorting"
                                OnRowCreated="gvOffRespresentative_RowCreated" OnSelectedIndexChanging="gvOffRespresentative_SelectedIndexChanging"
                                CellPadding="2" CellSpacing="2" OnRowCancelingEdit="gvOffRespresentative_RowCancelingEdit"
                                OnRowEditing="gvOffRespresentative_RowEditing" OnRowUpdating="gvOffRespresentative_RowUpdating"
                                OnRowDeleting="gvOffRespresentative_RowDeleting" OnRowCommand="gvOffRespresentative_RowCommand">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Department">
                                        <HeaderTemplate>
                                            Department
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeptID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DEPT_ID") %>'></asp:Label>
                                            <asp:Label ID="lblScmResID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Scm_Res_ID") %>'></asp:Label>
                                            <asp:Label ID="lblDeptName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Dept_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Representative Email">
                                        <HeaderTemplate>
                                            Representative Email
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblResEmail" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Scm_Res_email") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtResEmailEdit" runat="server" Width="98%" Text='<%# DataBinder.Eval(Container,"DataItem.Scm_Res_email") %>'
                                                MaxLength="255"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Representative Email">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" AlternateText="Edit" ImageUrl="~/Images/edit.gif"
                                                CommandName="EDIT" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdEdit"
                                                Visible='<%# objUA.Edit ==0 ? false : true%>' ToolTip="Edit"></asp:ImageButton>
                                            <img id="Img1" runat="server" alt="" src="~/Images/transp.gif" width="3" />
                                            <asp:ImageButton runat="server" AlternateText="Delete" ImageUrl="~/Images/Delete.png"
                                                CommandName="DELETE" CommandArgument="<%# Container.DataItemIndex %>" ID="cmdDelete"
                                                Visible='<%# objUA.Delete == 0 ? false : true%>' ToolTip="Delete Email"></asp:ImageButton>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
