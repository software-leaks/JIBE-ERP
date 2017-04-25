<%@ Page Title="SupplierList" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SupplierList.aspx.cs" Inherits="SupplierList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
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
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function validationOnSave() {
            

            if (document.getElementById("ctl00_MainContent_txtMakerName_AV").value == "") {
                alert("Please enter supplier name.");
                document.getElementById("ctl00_MainContent_txtMakerName_AV").focus();
                return false;
            }
            if (document.getElementById("txtShortName").value == "") {
                alert("Please enter supplier short name.");
                document.getElementById("txtShortName").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_txtAddress_AV").value == "") {
                alert("Please enter address.");
                document.getElementById("ctl00_MainContent_txtAddress_AV").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_ddlCountry_AV").value == "0") {
                alert("Please select country.");
                document.getElementById("ctl00_MainContent_ddlCountry_AV").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtEmail_AV").value == "") {
                alert("Please enter email Id.");
                document.getElementById("ctl00_MainContent_txtEmail_AV").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_ddlSupplierType").value == "0") {
                alert("Please select supplier type.");
                document.getElementById("ctl00_MainContent_ddlSupplierType").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_ddlCurrency").value == "0") {
                alert("Please select supplier currency.");
                document.getElementById("ctl00_MainContent_ddlCurrency").focus();
                return false;
            }
            if (Validate() == false) 
            {
                return false;
            }
            return true;

        }
        var atLeast = 1
        function Validate() {
            var CHK = document.getElementById("<%=cblSupplier.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (atLeast > counter) {
                alert("Please select atleast " + atLeast + " Supplier scope");
                return false;
            }
            else 
            {
                return true;
            }
            
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
          Supplier
    </div>
    <center>
            
          <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                 <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
      
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                
                <div style="height: 650px; color: Black;">
                    <asp:UpdatePanel ID="updMaker" runat="server">
                        <ContentTemplate>
                            <div style="padding-top: 5px; padding-bottom: 5px; height: 40px;">
                                <table width="100%" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td align="right" style="width: 30%">
                                            Code / Name / Country / Address &nbsp;:&nbsp;
                                        </td>
                                        <td align="left" style="width: 40%">
                                            <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                                WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                        </td>
                                        <td align="center" style="width: 30px">
                                            <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                                ImageUrl="~/Images/SearchButton.png" />
                                        </td>
                                        <td align="center" style="width: 30px">
                                            <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                                ImageUrl="~/Images/Refresh-icon.png" />
                                        </td>
                                        <td align="left" style="width: 30px">
                                            <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Supplier" OnClick="lnkAddNew_Click"
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
                                    <asp:GridView ID="gvSuppliers" runat="server" EmptyDataText="NO RECORDS FOUND" ShowHeaderWhenEmpty="true"
                                        AutoGenerateColumns="False" OnRowDataBound="gvSuppliers_RowDataBound" DataKeyNames="SUPPLIER_ID"
                                        CellPadding="1" CellSpacing="0" Width="100%" GridLines="both" OnSorting="gvSuppliers_Sorting"
                                        AllowSorting="true" CssClass="gridmain-css">
                                         <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                        <PagerStyle CssClass="PMSPagerStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Supplier_Code">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblSupplierCodeHeader" runat="server" CommandName="Sort" CommandArgument="Supplier_Code"
                                                        ForeColor="Black">Code&nbsp;</asp:LinkButton>
                                                    <img id="Supplier_Code" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupplierCode" runat="server" Text='<%# Eval("Supplier_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblSupplierNameHeader" runat="server" CommandName="Sort" CommandArgument="Supplier_Name"
                                                        ForeColor="Black">Name&nbsp;</asp:LinkButton>
                                                    <img id="Supplier_Name" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblSupplierName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Supplier_Name") %>'
                                                        Style="color: Black" CommandArgument='<%#Eval("[SUPPLIER_ID]")%>' OnCommand="onUpdate"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Currency">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("Supplier_Currency")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Country">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblCountryHeader" runat="server" CommandName="Sort" CommandArgument="Country"
                                                        ForeColor="Black">Country&nbsp;</asp:LinkButton>
                                                    <img id="Country" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRY")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="180px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EMAIL")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDRESS_1")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("PHONE")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fax">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfax" runat="server" Text='<%# Eval("FAX")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Telex">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTELEX" runat="server" Text='<%# Eval("TELEX")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
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
                                                                <asp:ImageButton ID="lbtnEdit" runat="server" CommandArgument='<%#Eval("SUPPLIER_ID") %>'
                                                                    Visible='<%# uaEditFlag %>' ImageUrl="~/images/edit.gif" OnCommand="onUpdate"
                                                                    Text="Edit"></asp:ImageButton>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="lbtnDelete" runat="server" CommandArgument='<%#Eval("SUPPLIER_ID") %>'
                                                                    Visible='<%# uaDeleteFlage %>' ImageUrl="~/images/delete.png" OnCommand="lbtnDelete_Click"
                                                                    OnClientClick="return confirm('Are you sure want to delete?')" ToolTip="Delete">
                                                                </asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <uc1:ucCustomPager ID="ucCustomPagerItems" PageSize="30" runat="server" OnBindDataItem="BindMakerGrid" />
                                    <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                </div>
                                 <br />
                            </div>
                            <div id="divadd" title="<%= OperationMode %>" style=" display:none;  border: 1px solid Gray;
                                font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 40%;">
                                <table width="98%" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td align="right">
                                            Supplier Name &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtMakerName_AV" runat="server" Width="200px" CssClass="txtInput"
                                                MaxLength="150" Text=""></asp:TextBox>
                                        </td>
                                        <td align="right">
                                           Short Name &nbsp;&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">*</td>
                                         <td align="left">
                                            <asp:TextBox ID="txtShortName" runat="server" ClientIDMode="Static" Width="200px" CssClass="txtInput"
                                                MaxLength="150" Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Address 1 &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtAddress_AV" runat="server" Width="200px" Height="70px" TextMode="MultiLine"
                                                MaxLength="2000" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Address 2 &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                           
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtAddress2_AV" runat="server" Width="200px" Height="70px" TextMode="MultiLine"
                                                MaxLength="2000" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Supplier Code &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                     
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtMakerCode_AV" Enabled="false" runat="server" MaxLength="50" CssClass="txtReadOnly"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Country &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlCountry_AV" runat="server" Width="140px" CssClass="txtInput"
                                                AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            E-mail-1 &nbsp;:&nbsp;
                                        </td>
                                         <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEmail_AV" MaxLength="250" Width="200px" runat="server" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            E-mail-2 &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEmail2_AV" MaxLength="250" Width="200px" runat="server" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Phone 1 &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPhone_AV" MaxLength="50" Width="200px" runat="server" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Phone 2 &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPhone2_AV" MaxLength="50" Width="200px" runat="server" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Fax 1 &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFax_AV" runat="server" Width="200px" MaxLength="50" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Fax 2 &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFax2_AV" runat="server" Width="200px" MaxLength="50" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Telex 1 &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtTelex_AV" runat="server" CssClass="txtInput" MaxLength="50" Width="200px"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Telex 2 &nbsp;:&nbsp;
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtTelex2_AV" runat="server" CssClass="txtInput" MaxLength="50"
                                                Width="200px"></asp:TextBox>
                                        </td>
                                        <tr>
                                            <td align="right">
                                               Supplier Currency &nbsp;:&nbsp;
                                            </td>
                                             <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                            <td align="left">
                                            <asp:DropDownList ID="ddlCurrency" runat="server" Width="102%" CssClass="txtInput">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtCreationDate_AV" runat="server" Visible ="false" Width="0px"  CssClass="txtReadOnly" Enabled="false"></asp:TextBox>
                                            </td>
                                        <td align="right">
                                           Supplier Type &nbsp;:&nbsp;
                                        </td>
                                         <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                            <td>
                                            <asp:DropDownList ID="ddlSupplierType" runat ="server" >
                                            <asp:ListItem Value="0" Text="Select Supplier Type" Selected ="True"> </asp:ListItem>
                                            <asp:ListItem Value="A" Text="Agent"> </asp:ListItem>
                                            <asp:ListItem Value="S" Text="Supplier"> </asp:ListItem>
                                            <asp:ListItem Value="S" Text="Travel Agent"> </asp:ListItem>                                            
                                            </asp:DropDownList>
                                            </td>
                                        </tr>
                                         <tr>
                                           
                                        <td align="right" valign ="top">
                                           Supplier Scope &nbsp;:&nbsp;
                                        </td>
                                         <td style="color: #FF0000; width: 1%" align="right" valign ="top">
                                            *
                                        </td>
                                            <td colspan="4" >
                                            <asp:CheckBoxList ID="cblSupplier" CellPadding ="8" RepeatColumns="8" runat ="server" Height="60px">
                                            <asp:ListItem Value ="TRVL" Text="TRVL"></asp:ListItem>
                                            <asp:ListItem Value ="PROV" Text="PROV"></asp:ListItem>
                                            <asp:ListItem Value ="BOND" Text="BOND"></asp:ListItem>
                                            <asp:ListItem Value ="AGCY" Text="AGCY"></asp:ListItem>
                                            <asp:ListItem Value ="SAFE" Text="SAFE"></asp:ListItem>
                                            <asp:ListItem Value ="AUDT" Text="AUDT"></asp:ListItem>
                                            <asp:ListItem Value ="BNKR" Text="BNKR"></asp:ListItem>
                                            <asp:ListItem Value ="CHEM" Text="CHEM"></asp:ListItem>
                                            <asp:ListItem Value ="BRKG" Text="BRKG"></asp:ListItem>
                                            <asp:ListItem Value ="CHRT" Text="CHRT"></asp:ListItem>
                                            <asp:ListItem Value ="CLAS" Text="CLAS"></asp:ListItem>
                                            <asp:ListItem Value ="COMM" Text="COMM"></asp:ListItem>
                                            <asp:ListItem Value ="CTMR" Text="CTMR"></asp:ListItem>
                                            <asp:ListItem Value ="LOGS" Text="LOGS"></asp:ListItem>
                                            <asp:ListItem Value ="CWLO" Text="CWLO"></asp:ListItem>
                                            <asp:ListItem Value ="DSPR" Text="DSPR"></asp:ListItem>
                                            <asp:ListItem Value ="DSTR" Text="DSTR"></asp:ListItem>
                                            <asp:ListItem Value ="FUEL" Text="FUEL"></asp:ListItem>
                                            <asp:ListItem Value ="INSU" Text="INSU"></asp:ListItem>                                                                                      
                                            </asp:CheckBoxList>
                                            </td>
                                           
                                        </tr>

                                        <tr>
                                            <td colspan="6" style="font-size: 11px; text-align: center; border-style: solid;
                                                border-color: Silver; border-width: 1px">
                                                <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" OnClientClick="return validationOnSave();"
                                                    Text="Save" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="6" style="color: #FF0000; font-size: small;">
                                                * Mandatory fields
                                            </td>
                                        </tr>
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
