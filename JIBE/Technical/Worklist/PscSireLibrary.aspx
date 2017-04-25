<%@ Page Title="PSC/SIRE Library" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PscSireLibrary.aspx.cs" Inherits="Technical_Worklist_PscSireLibrary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">

    function validationAddPSCSIRE() {
        //                document.getElementById("ctl00_MainContent_ddlCatalogFunction_txtSelectedFunction").value = "";

        return true;

    }

    function validationPSCSIRE() {

      
        var PSCSIRECode = document.getElementById($('[id$=txtPSCSIRECode]').attr('id')).value.trim();








        if (PSCSIRECode == "") {
            alert("PSC/SIRE Code is required.");
            document.getElementById($('[id$=txtPSCSIRECode]').attr('id')).focus();
            return false;
        }



        return true;

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
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; height: 100%;">
        <div id="page-header" class="page-title">
            <b>PSC/SIRE Library</b>
        </div>
            <div id="Div1" style="background-color:Yellow; " align="center" >
              <asp:Label ID="lblError" runat="server"  ForeColor="Red" Visible="false"></asp:Label>
        </div>
        <table style="width: 100%;">
        <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="50%">
                                <tr>
                                    <td align="right">
                                       <asp:Label ID="lblPSCSIRE" runat="server" Text=" PSC/SIRE Code: "></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPSCSIRECode" runat="server" CssClass="txtInput"></asp:TextBox>
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td colspan="6" align="left">
                                        <asp:Button ID="btnPSCSIRESave" Text="Save" runat="server" Width="70px" OnClientClick="return validationPSCSIRE()"
                                            OnClick="btnPSCSIRESave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="updCat" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdPSCSIRE" CellPadding="8" runat="server" ShowFooter="False"
                                AutoGenerateColumns="false" BorderWidth="1px" Width="100%">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" Font-Size="12px" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        <HeaderTemplate>
                                            PSC/SIRE Code
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCategory" CommandName="Select" ToolTip="Select" runat="server"
                                                ForeColor="GrayText" Font-Bold="true" CssClass="LinkButton" Text='<%# Eval("PSC_SIRE") %>'
                                                CommandArgument='<%# Eval("ID") %>' OnCommand="lnkCategory_Click"></asp:LinkButton>
                                            <asp:HiddenField ID="hdnActiveStatus" runat="server" Value='<%# Eval("Active_Status") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ItemStyle-css" />
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="0">
                                                <tr>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgEditPSCSIRE" runat="server" ForeColor="Black" ToolTip="Edit"
                                                            ImageUrl="~/purchase/Image/Edit.gif" OnCommand="ImgEditPSCSIRE_Click" CommandArgument='<%# Eval("ID") %>'
                                                            Width="16px" Height="16px" Visible='<%# Eval("Active_Status").ToString()=="1"?true:false %>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgPSCSIREDelete" runat="server" ForeColor="Black" ToolTip="Delete"
                                                            ImageUrl="~/purchase/Image/Delete.gif" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                            OnCommand="ImgPSCSIREDelete_Click" CommandArgument='<%# Eval("ID") %>' Width="16px"
                                                            Height="16px" Visible='<%# Eval("Active_Status").ToString()=="1"?true:false %>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgPSCSIRERestore" runat="server" ForeColor="Black" ToolTip="Restore"
                                                            ImageUrl="~/purchase/Image/restore.png" OnClientClick="return confirm('Are you sure, you want to  restore ?')"
                                                            OnCommand="ImgPSCSIRERestore_Click" CommandArgument='<%# Eval("ID") %>' Width="16px"
                                                            Height="16px" Visible='<%# Eval("Active_Status").ToString()=="1"?false:true %>'>
                                                        </asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Records Found
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            
        </table>
    </div>





</asp:Content>

