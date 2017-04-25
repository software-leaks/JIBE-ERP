<%@ Page Title="Inspection Rating Library" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InspectionRatingLibrary.aspx.cs" Inherits="Technical_Worklist_InspectionRatingLibrary" %>

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
				<b>Inspection Rating Library</b>
			</div>
<table style="width:100%;">

<tr>
<td >

        <asp:UpdatePanel ID="updCat" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="grdRating" CellPadding="8" runat="server"
                    ShowFooter="False" AutoGenerateColumns="false" BorderWidth="1px" 
                    Width="100%" onrowdatabound="grdRating_RowDataBound" >
                    <HeaderStyle CssClass="HeaderStyle-css" />
                    <RowStyle CssClass="RowStyle-css" Font-Size="12px" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                    <SelectedRowStyle BackColor="#FFFFCC" />
                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                    <Columns>
                       


                         <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                            <HeaderTemplate>
                            Rating Value
                            </HeaderTemplate>
                            <ItemTemplate>
                               <asp:LinkButton ID="lnkRating" CommandName="Select" ToolTip="Select" runat="server" ForeColor="GrayText" Font-Bold="true" CssClass="LinkButton" Text='<%# Bind("RatingValue") %>' CommandArgument='<%# Bind("ID") %>' OnCommand="lnkRating_Click"></asp:LinkButton>
                                <asp:HiddenField ID="hdnActiveStatus" runat="server" Value='<%# Bind("Active_Status") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ItemStyle-css" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Rating" DataField="Rating" ItemStyle-Width="80px" />
                        <asp:BoundField HeaderText="Rating Color" DataField="RatingColor" ItemStyle-Width="100px" />
                       
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                            <ItemTemplate>
                                <table cellpadding="2" cellspacing="0">
                                    <tr>
                                    <td style="border-color: transparent">
                                            <asp:ImageButton ID="ImgEditRating" runat="server" ForeColor="Black" ToolTip="Edit"
                                                ImageUrl="~/purchase/Image/Edit.gif" OnCommand="ImgEditRating_Click" CommandArgument='<%# Bind("ID") %>' Width="16px"
                                                Height="16px"></asp:ImageButton>
                                        </td>
                                        <td style="border-color: transparent">
                                            <asp:ImageButton ID="ImgRatingDelete" runat="server" ForeColor="Black" ToolTip="Delete"
                                                ImageUrl="~/purchase/Image/Delete.gif" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                OnCommand="ImgRatingDelete_Click" CommandArgument='<%# Bind("ID") %>' Width="16px"
                                                Height="16px"></asp:ImageButton>
                                        </td>
                                        <td style="border-color: transparent">
                                            <asp:ImageButton ID="ImgRatingRestore" runat="server" ForeColor="Black" ToolTip="Restore"
                                                ImageUrl="~/purchase/Image/restore.png"  OnClientClick="return confirm('Are you sure, you want to  restore ?')" OnCommand="ImgRatingRestore_Click" CommandArgument='<%# Bind("ID") %>'
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
                        Rating : &nbsp;
                    </td>
                    <td  align="left">
                        <asp:TextBox ID="txtRating" runat="server" CssClass="txtInput"></asp:TextBox>
                    </td>
               
                    <td align="right">
                        Value : &nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtValue" runat="server" CssClass="txtInput"></asp:TextBox>
                    </td>
                
                    <td align="right">
                        Rating Color : &nbsp;
                    </td>
                    <td  align="left">
                        <asp:DropDownList ID="ddlColor" runat="server" CssClass="txtInput">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="6"  align="center">
                        <asp:Button ID="btnRatingAdd" Text="Add New" runat="server" Width="70px" OnClientClick="return validationAddRating()"
                            OnClick="btnRatingAdd_Click" />
                        <asp:Button ID="btnRatingSave" Text="Save" runat="server" Width="70px" OnClientClick="return validationRating()"
                            OnClick="btnRatingSave_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</td>

</tr>
</table>
</div>


</asp:Content>

