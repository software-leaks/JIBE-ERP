<%@ Page Title="Voyage Number" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="VoyageNoLibrary.aspx.cs" Inherits="Operations_VoyageNoLibrary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
 <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .page
        {
            width: 1200px;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
         .LinkButton
        {
            text-decoration:none;
            
           
        }
        .LinkButton:hover
        {
            text-decoration:underline;
        }
        .txtInput
        {}
        .style1
        {
            width: 495px;
        }
        </style>

        <script type="text/javascript" language="javascript">

            function validationAddRating() {
                //                document.getElementById("ctl00_MainContent_ddlCatalogFunction_txtSelectedFunction").value = "";

                return true;

            }

            function validationRating() {

                var Color = document.getElementById("ctl00_MainContent_ddlColor").value;
                var Value = document.getElementById("ctl00_MainContent_txtValue").value.trim();
                var Rating = document.getElementById("ctl00_MainContent_txtRating").value.trim();
                if (Value != "") {
                    if (isNaN(Value)) {
                        document.getElementById("ctl00_MainContent_txtValue").focus();
                        alert('Rating Value accept only numeric value.')
                        return false;
                    }
                }

                if (Rating == "") {
                    alert("Rating is required.");
                    document.getElementById("ctl00_MainContent_txtRating").focus();
                    return false;
                }
                if (Value == "") {
                    alert("Rating Value is required.");
                    document.getElementById("ctl00_MainContent_txtValue").focus();
                    return false;
                }

                if (Color == "--SELECT--") {
                    alert("Select Rating Color.");
                    document.getElementById("ctl00_MainContent_ddlColor").focus();
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
                <b>Add/Edit Voyage Number</b>
            </div>
<table style="width:100%;">

<tr>
<td >

        <asp:UpdatePanel ID="updCat" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

               <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 8%">
                                        Voyage Number:&nbsp;
                                    </td>
                                    <td align="left" style="width: 10%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                <%--    </td>
                                        <td align="left" style="width: 12%">
                                        <asp:DropDownList ID="DDLVesselType" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </td>--%>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Voyage Number" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td runat="server" id="tdV_No" style="width: 5%" align="right">
                                       
                                        Voyage Number :
                                       
                                    </td>
                                         <td style="width: 5%">
                                       
                                             <asp:TextBox ID="txtValue" runat="server" CssClass="txtInput" Width="158px"></asp:TextBox>
                                               <asp:RequiredFieldValidator ID="rqfAuthority" runat="server" ValidationGroup ="save" ControlToValidate="txtValue" ErrorMessage="*" /> 
                          <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtValue" ID="RegularExpressionValidator1" ValidationExpression = "^[\s\S]{0,50}$" runat="server" 
                          ErrorMessage="Maximum 50 characters allowed."  ValidationGroup ="save"></asp:RegularExpressionValidator>
                                       
                                    </td>
                                         <td style="width: 5%">
                                       
                                             <asp:Button ID="btnVoyageIdSave" runat="server" OnClick="btnVoyageIdSave_Click" Text="Save" ValidationGroup="save" 
                                                 Width="70px" />
                                       
                                    </td>
                                </tr>
                            </table>
                        </div>
                <asp:GridView ID="grdVoyageId" CellPadding="8" runat="server"
                    ShowFooter="False" AutoGenerateColumns="false" BorderWidth="1px" 
                    Width="100%" onrowdatabound="grdVoyageId_RowDataBound"  >
                    <HeaderStyle CssClass="HeaderStyle-css" />
                    <RowStyle CssClass="RowStyle-css" Font-Size="12px" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <SelectedRowStyle BackColor="#FFFFCC" />
                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                    <Columns>				

                                <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                            <HeaderTemplate>
                            Voyage Number
                            </HeaderTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblVoyageId" runat="server" ForeColor="GrayText" Font-Bold="true"  Text='<%# Bind("Voyage_Number") %>' ></asp:Label>	
                                <asp:HiddenField ID="hdnActiveStatus" runat="server" Value='<%# Bind("Active_Status") %>' />						
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ItemStyle-css" />
                        </asp:TemplateField>

                        <%--<asp:BoundField HeaderText="Voyage Id" DataField="Voyage_ID" ItemStyle-Width="80px"  />--%>
                        <asp:BoundField HeaderText="Status" DataField="Active_Status" ItemStyle-Width="100px" />
                       
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                            <ItemTemplate>
                                <table cellpadding="2" cellspacing="0">
                                    <tr>
                                    <td style="border-color: transparent">
                                            <asp:ImageButton ID="ImgEdit" runat="server" ForeColor="Black" ToolTip="Edit"
                                                ImageUrl="~/purchase/Image/Edit.gif" OnCommand="ImgEdit_Click" CommandArgument='<%# Bind("ID") %>' Width="16px"
                                                Height="16px"></asp:ImageButton>
                                        </td>
                                        <td style="border-color: transparent">
                                            <asp:ImageButton ID="ImgVoyageDelete" runat="server" ForeColor="Black" ToolTip="Delete"
                                                ImageUrl="~/purchase/Image/Delete.gif" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                OnCommand="ImgVoyageDelete_Click" CommandArgument='<%# Bind("ID") %>' Width="16px"
                                                Height="16px"></asp:ImageButton>
                                        </td>
                                        <td style="border-color: transparent">
                                            <asp:ImageButton ID="ImgVoyageRestore" runat="server" ForeColor="Black" ToolTip="Restore"
                                                ImageUrl="~/purchase/Image/restore.png"  OnClientClick="return confirm('Are you sure, you want to  restore ?')" OnCommand="ImgVoyageRestore_Click" CommandArgument='<%# Bind("ID") %>'
                                                Width="16px" Height="16px"></asp:ImageButton>
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
                 <uc1:uccustompager ID="ucCustomPagerItems" runat="server" 
          PageSize="30" OnBindDataItem="BindVoyageNumber" />
          <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                
            </ContentTemplate>
        </asp:UpdatePanel>   
</td>
</tr>
<tr>
<td>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td align="right">
                        &nbsp;&nbsp;
                    </td>
                    <td  align="left">
                        &nbsp;</td>
               
                    <td align="right">
                        &nbsp;</td>
                    <td align="left" class="style1">
                      
                    </td>                
                    <td align="right">
                        &nbsp;</td>
                    <td  align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6"  align="center">
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</td>

</tr>
</table>
</div>


</asp:Content>

