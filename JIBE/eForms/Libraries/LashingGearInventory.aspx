<%@ Page Title="Lashing Gear Inventory" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LashingGearInventory.aspx.cs" Inherits="eForms_Libraries_LashingGearInventory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("ctl00_MainContent_DDLVessel").value == "0") {
                alert("Please select vessel.");
                document.getElementById("ctl00_MainContent_DDLVessel").focus();
                return false;
            }



            if (document.getElementById("ctl00_MainContent_txtItemDes").value.trim() == "") {
                alert("Please enter Item Description.");
                document.getElementById("ctl00_MainContent_txtItemDes").focus();
                return false;
            }


            if (document.getElementById("ctl00_MainContent_txtitemModel").value.trim() == "") {
                alert("Please Enter Item Model Number.");
                document.getElementById("ctl00_MainContent_txtitemModel").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtcoargoMno").value.trim() == "") {
                alert("Please Enter Cargo Mannual Number.");
                document.getElementById("ctl00_MainContent_txtcoargoMno").focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   
    <center>        
        <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                   <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 900px;
            height: 100%;">
          <div class="page-title">
           LASHING GEAR INVENTORY
          </div>
            <div style="height: 650px; width: 900px; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 8%">
                                        Vessel :&nbsp;
                                    </td>
                                    <td align="left" style="width: 15%">
                                        <asp:DropDownList ID="ddlVessel_Filter" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 15%">
                                        Item Description :&nbsp;
                                    </td>
                                    <td align="left" style="width: 15%">
                                         <asp:TextBox ID="txtItemDescription" runat="server" Width="113%"></asp:TextBox>
                                         <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtItemDescription"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="right" style="width: 15%">
                                        Item Model :&nbsp;
                                    </td>
                                    <td align="left" style="width: 20%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="100%" CssClass="txtInput"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>

                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" TabIndex="0" OnClick="btnFilter_Click"
                                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvLoCategory" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvLoCategory_RowDataBound" DataKeyNames="ID" CellPadding="2"
                                    CellSpacing="0" Width="100%" GridLines="both" OnSorting="gvLoCategory_Sorting"
                                    AllowSorting="true">
                                   <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                   <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                   <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vessel">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblVesselNameHeader" runat="server" CommandName="Sort" CommandArgument="VESSEL_NAME"
                                                    ForeColor="Black">Vessel&nbsp;</asp:LinkButton>
                                                <img id="Reason" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblVesselName" runat="server" Text='<%#Eval("VESSEL_NAME")%>'
                                                    Style="color: Black" CommandArgument='<%#Eval("ID") + "," + Eval("Vessel_ID")%>'
                                                    OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Description">
                                            <HeaderTemplate>
                                               Item Description
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemDescription" runat="server" Text='<%#Eval("Item_Description")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Model">
                                            <HeaderTemplate>
                                                Item Model
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblModelNo" runat="server" Text='<%#Eval("Model_No")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cargo Manual Non">
                                            <HeaderTemplate>
                                                Cargo Manual No
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblHoldTankName" runat="server" Text='<%#Eval("Carg_Securing_Mannual_No")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         

                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("ID") + "," + Eval("Vessel_ID")%>'
                                                                ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("ID") + "," + Eval("Vessel_ID")%>' ForeColor="Black"
                                                                ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindLashingGearInventory" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 25%">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Vessel Name &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:DropDownList ID="DDLVessel" runat="server" Width="204px" CssClass="txtInput">
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None" 
                                                                    InitialValue="0" ErrorMessage="Vessel Name is mandatory field." ControlToValidate="DDLVessel"
                                                                    ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Item Description &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:TextBox ID="txtItemDes" CssClass="txtInput" MaxLength="100"  Width="200px"
                                            runat="server"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                                                                     ErrorMessage="Item Description is mandatory field." ControlToValidate="txtItemDes"
                                                                    ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Item Model &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 25%">
                                         <asp:TextBox ID="txtitemModel" CssClass="txtInput" MaxLength="100"  Width="200px"
                                          runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                                                     ErrorMessage="Item Model is mandatory field." ControlToValidate="txtitemModel"
                                                                    ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Cargo Mannual No &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 25%">
                                         <asp:TextBox ID="txtcoargoMno" CssClass="txtInput" MaxLength="100"  Width="200px"
                                          runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegPicPhone1" runat="server" ErrorMessage="Cargo Manual No. is not valid"
                                                                    Display="None" ValidationGroup="vgSubmit" ControlToValidate="txtcoargoMno" ForeColor="Red"
                                                                    ValidationExpression="^[ 0-9,()+-]+$"> </asp:RegularExpressionValidator>
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                                                     ErrorMessage="Cargo Mannual No is mandatory field." ControlToValidate="txtcoargoMno"
                                                                    ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" ValidationGroup="vgSubmit"
                                            OnClick="btnsave_Click" />
                                        <asp:TextBox ID="txtLashingGearInventoryID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                        * Mandatory fields
                                          <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="vgSubmit" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel"/>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
