<%@ Page Title="PO List" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="POList.aspx.cs" Inherits="POList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        
        .linkbtn
        {
            border-right: wheat 1px solid;
            border-top: wheat 1px solid;
            font-weight: bold;
            border-left: wheat 1px solid;
            color: White;
            border-bottom: wheat 1px solid;
            background-color: white;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function Divaddnewlink() {
            document.getElementById("divadd").style.display = "block";
        }



        function validation() {

            if (document.getElementById("ctl00_MainContent_txtPoList").value == "") {
                alert("Please enter Attach Prefix.");
                document.getElementById("ctl00_MainContent_txtPoList").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtFileSize").value == "") {
                alert("Please Enter File Size.");
                document.getElementById("ctl00_MainContent_txtFileSize").focus();
                return false;
            }

            return true;


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
                PO&nbsp; List</div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdPoList" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 8%">
                                        PO Number:&nbsp;
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
                                                ImageUrl="~/Images/SearchButton.png" Style="width: 23px" />
                                        </td>
                                        <td align="center" style="width: 5%">
                                            <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                                ImageUrl="~/Images/Refresh-icon.png" />
                                        </td>
                                        <td align="center" style="width: 5%">
                                            <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New PO Number"
                                                OnClick="ImgAdd_Click" ImageUrl="~/Images/Add-icon.png" />
                                        </td>
                                        <td style="width: 5%">
                                           <%-- <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                                ImageUrl="~/Images/Exptoexcel.png" />--%>
                                        </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div style="width: 100%; height: 600px; overflow: scroll">
                                <asp:GridView ID="gvPOList" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvPOList_RowDataBound" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both" OnSorting="gvPOList_Sorting" AllowSorting="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="PoList">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="txtPONo" runat="server" CommandName="Sort" CommandArgument="ID"
                                                    ForeColor="Black">PO Number&nbsp;</asp:LinkButton>
                                                <%--<img id="PoList" runat="server" visible="false" />--%>
                                            </HeaderTemplate>
                                                <ItemTemplate>
                                  <%--  <asp:LinkButton ID="lblPO_Number" runat="server" CommandName="OpenItems" CommandArgument='<%#Eval("[PO_Number]")%>' OnCommand="onUpdate"
                                        Target="_blank" Text='<%# Eval("PO_Number")%>' CssClass="staffInfo pin-it"></asp:LinkButton>--%>

                                          <asp:LinkButton ID="lblPO_Number" runat="server" OnCommand="onUpdate"  CommandName="OpenItems" 
                                                              Text='<%# Eval("PO_Number")%>'  Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")+","+ Eval("PO_Number")%>' ForeColor="Blue"
                                                                ToolTip="View Items" ></asp:LinkButton>


                                    <asp:Label ID="lblX" runat="server"></asp:Label>
                                     <%--NavigateUrl='<%# "PoListItems.aspx?ID=" + Eval("ID")%>'--%>
                                </ItemTemplate>
                                <ItemStyle Width="60px" HorizontalAlign="Left" />                             
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPO_Amount" runat="server" Text='<%# Eval("PO_Amount")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfav" runat="server" Text='<%# Bind("Order_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplier" runat="server" Text='<%# Eval("Supplier_name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vessel ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
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
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")+","+ Eval("PO_Number")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>                                                
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="15" OnBindDataItem="BindPOList" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                <asp:HiddenField ID="HiddenFieldItemID" runat="server" EnableViewState="False" />
                                <asp:HiddenField ID="HiddenFieldID" runat="server" EnableViewState="False" />   
                                <asp:HiddenField ID="HiddenField_PONo" runat="server" EnableViewState="False" />                              
                            </div>
                            <br />
                        </div>             
                    </ContentTemplate>                  
                </asp:UpdatePanel>
            </div>
        </div>
            <asp:UpdatePanel  ID="UpdatePanel1" runat="server" >
                                           <ContentTemplate>
                 <div id="divadd" title="<%= OperationMode %>" style="display:none; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 30%">
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
                                    <td align="right" style="width: 30%">
                                        PO Number&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPoNo" MaxLength="10" CssClass="txtInput" Width="90%" runat="server"></asp:TextBox>
                                     <%--     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPoNo"
                                            ValidationExpression="\d+" Display="Dynamic" EnableClientScript="true" ErrorMessage="Please enter numeric values only"
                                            ValidationGroup="ValidationCheck" runat="server" />--%>
                                            <asp:RequiredFieldValidator ID="rfv1"  ValidationGroup="ValidationCheck" runat="server" ControlToValidate="txtPoNo"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 20%">
                                        PO Amount :&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPOAmt" MaxLength="12" CssClass="txtInput" Width="90%" runat="server"></asp:TextBox>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPOAmt"
                                            ValidationExpression="\d+" Display="Dynamic" EnableClientScript="true" ErrorMessage="Please enter numeric values only"
                                            ValidationGroup="ValidationCheck" runat="server" />--%>                                           
                                             <asp:RegularExpressionValidator ID="NumericOnlyValidator" runat="server" Display="Dynamic"  ValidationGroup="ValidationCheck"
                                             ControlToValidate="txtPOAmt" ErrorMessage="Enter a valid Amount" ValidationExpression="^\d+([,\.]\d{1,2})?$"></asp:RegularExpressionValidator>
                                              <asp:RequiredFieldValidator ID="rfv2"  ValidationGroup="ValidationCheck" runat="server" ControlToValidate="txtPOAmt"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Vessel :
                                    </td>
                                    <td align="right" style="color: #FF0000; width: 1%">
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlVessel" runat="server" Width="90%">
                                        </asp:DropDownList>                                     
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Supplier :
                                    </td>
                                    <td align="right" style="color: #FF0000; width: 1%">
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSupplier" runat="server" Width="90%">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Order Date &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="dtOrderDate" runat="server" Width="90%" MaxLength="100" ></asp:TextBox>
                                        <cc1:CalendarExtender TargetControlID="dtOrderDate" ID="caltxOrderDate" Format="dd-MM-yyyy"
                                            runat="server">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; background-color: none;">
                                        <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Save" ValidationGroup="ValidationCheck" />
                                       <%-- <asp:TextBox ID="txtPoListID" runat="server" Visible="false" Width="1px"></asp:TextBox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 0px solid #cccccc;
                                            background-color: #FDFDFD">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="3" style="color: #FF0000; font-size: small;">
                                        * Mandatory fields
                                    </td>
                                </tr>
                            </table>
                     
                        </div>

                         <div id="divItems"  title="<%= OperationMode %>" style="display:none; font-family: Tahoma; 
                            text-align: center; font-size: 12px; color: Black; width: 60%">
                            <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table>
                            <tr>
                          <td style="width: 15%"> 
                          
                           </td>
                            <td align="right" style="width: 15%">
                           <asp:Label ID="lblPONO" runat="server" Font-Bold="True"> </asp:Label>
                         
                            </td>
                            
                               <td align="center" style="width: 5%">
                                            <asp:ImageButton ID="btnItemRefresh" runat="server" ToolTip="Refresh" OnClick="btnItemRefresh_Click" 
                                                ImageUrl="~/Images/Refresh-icon.png" />
                                        </td>
                                        <td align="center" style="width: 5%">
                                            <asp:ImageButton ID="btnItemAdd" runat="server" ToolTip="Add New Item"
                                                OnClick="btnItemAdd_Click" ImageUrl="~/Images/Add-icon.png" />
                                        </td>
                                              <td style="width: 5%">
                                           <%-- <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                                ImageUrl="~/Images/Exptoexcel.png" />--%>
                                        </td>
                            </tr>
                            </table>
                             </div>
                       <center>
                       <asp:Panel ID="pnlItems" runat="server" width="40%" BorderColor="#e1e1e1" BorderWidth="1" Visible="false" >
                               <table cellpadding="2" cellspacing="2" width="100%"  >
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 30%">
                                            Item Description :&nbsp;
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                          
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtIem_Desc" runat="server" 
                                                MaxLength="200" Width="90%"></asp:TextBox>
                                       <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="txtIem_Desc"  ValidationGroup="ValidationCheckItem" ></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 20%">
                                            Item Price :&nbsp;
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtItemPrice" runat="server" CssClass="txtInput" 
                                                MaxLength="12" Width="90%"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                ControlToValidate="txtItemPrice" Display="Dynamic" 
                                                ErrorMessage="Enter a valid Amount" ValidationExpression="^\d+([,\.]\d{1,2})?$" 
                                                ValidationGroup="ValidationCheckItem"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ControlToValidate="txtItemPrice" ValidationGroup="ValidationCheckItem"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Item Quantity :
                                        </td>
                                           <td align="right" style="color: #FF0000; width: 1%">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtItem_Qtn" runat="server" CssClass="txtInput" MaxLength="8" Width="90%">
                                        </asp:TextBox>
                                            <asp:RegularExpressionValidator ID="rexv" runat="server" 
                                                ControlToValidate="txtItem_Qtn" Display="Dynamic" EnableClientScript="true" 
                                                ErrorMessage="Please enter numeric values only" ValidationExpression="\d+" 
                                                ValidationGroup="ValidationCheckItem" />
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="txtItemPrice" ValidationGroup="ValidationCheckItem"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Item Unit&nbsp;:&nbsp;
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtItem_unit" runat="server" MaxLength="100" Width="90%"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                Format="dd-MM-yyyy" TargetControlID="dtOrderDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" 
                                            style="font-size: 11px; text-align: center; background-color: none;">
                                            <asp:Button ID="btnsaveItem" runat="server" OnClick="btnsaveItem_Click" 
                                                Text="Save" ValidationGroup="ValidationCheckItem" />
                                            <%-- <asp:TextBox ID="txtPoListID" runat="server" Visible="false" Width="1px"></asp:TextBox>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                             <td align="right" colspan="3" style="color: #FF0000; font-size: small;">
                                            * Mandatory fields
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="3" style="color: #FF0000; font-size: small;">
                                            <%--* Mandatory fields--%>
                                        </td>
                                    </tr>
                                </table>       
                                </asp:Panel>
                                <div style="height:5px;"> </div>
                                <div style="width: 100%; height: 500px; overflow: scroll">
                               <asp:GridView ID="gvPoItems" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                  DataKeyNames="Item_ID" CellPadding="1" CellSpacing="0" 
                                    Width="80%" GridLines="both"  AllowSorting="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item Description">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblItemDec" runat="server" CommandName="Sort" CommandArgument="ID"
                                                    ForeColor="Black">Item Description &nbsp;</asp:Label>
                                                <%--<img id="PoList" runat="server" visible="false" />--%>
                                            </HeaderTemplate>
                                                <ItemTemplate>
                                    <asp:Label ID="lblPO_Number" runat="server" 
                                         Text='<%# Eval("Item_Description")%>' CssClass="staffInfo pin-it"></asp:Label>                                   
                                </ItemTemplate>
                                <ItemStyle Width="60px" HorizontalAlign="Left" />                                
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Price ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem_Amount" runat="server" Text='<%# Eval("Item_price")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Qtn">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem_Qtn" runat="server" Text='<%# Bind("Item_Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItem_unit" runat="server" Text='<%# Eval("Item_unit")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
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
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate" CommandName="EditItems"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Item_ID]")+","+ Eval("Item_Description")+","+ Eval("Item_price")+","+ Eval("Item_Quantity")+","+ Eval("Item_unit")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete" CommandName="ItemsDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[Item_ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>                                                     
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>  
                                </div>                         
                         
                            </center>                     
                     
                        </div>
                 
                            </ContentTemplate>           
                </asp:UpdatePanel>

    </center>
</asp:Content>
