

<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" Title="PI List" MasterPageFile="~/Site.master"  CodeFile="PI_List.aspx.cs"
    Inherits="PI_List" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script type="text/javascript">
        function numbersonly(e) {
            var keycode = e.charCode ? e.charCode : e.keyCode;
            if (!(keycode == 46 || keycode == 8 ||keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57)))
                {
                    return false
                    }
            return true;
            }

//            function OpenDemo() {
//                var url = "../../Images/TMSA_PI_Demo.png"
//                OpenPopupWindow('PIDetail', 'PI Details', url, 'popup', 700, 800, null, null, false, false, true, null);
//            }

            function OpenScreen(PI_ID) {
                var url = "PI_Details.aspx?PI_ID=" + PI_ID ;
                window.open(url, "_blank");
            }
            function OpenEntryScreen(ID,UOM,Intr) {

                var url = 'PI_Entry.aspx?ID=' + ID + '&UOM=' + UOM + '&Intr=' + Intr;
                OpenPopupWindowBtnID('PIEntry', 'PI Entry', url, 'popup', 700, 800, null, null, false, false, true, null, 'ctl00_MainContent_imgsearh');
            }

            function OpenQueryBuilder() {
                var url = "../PI/querybuilder.aspx?Type=TMSA_Daemon_SP";
                window.open(url, "_blank");
            }

    </script>

         <style type="text/css">

         .Rowstyle

        {

            background-color: #D4C9F5;
            border: 1px solid #496077;
        }


   </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

        <center>
    <div id="dvContent" style="overflow:scroll; text-align: center; border: 1px solid #5588BB;">
        <center>
        <div width="50%">
            <table width="100%" cellpadding="2" cellspacing="0" >
                            <tr>
                    <td align="center" colspan="6">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltPageHeader" Text ="PI List" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>

                                <tr>
                    <td align="right" style="font-weight:bold" width="15%">
                                        PI  Name/Code :
                                    </td>
                                    <td align="left" valign="top" width="10%">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txtInput" Width="200px"></asp:TextBox>
                                     
                                    </td>
                                    <td style="font-weight:bold" align="right"  width="10%">
                                    Category :
                                    </td>
                                     <td align="left" valign="top" width="10%">
                                         <asp:DropDownList ID="ddlCategory" runat="server">
                                         </asp:DropDownList>
                                     
                                    </td>
                                    <td align="left" valign="top" width="10%">
                                        <asp:ImageButton ID="imgsearh" runat="server" 
                                            ImageUrl="~/Images/SearchButton.png" 
                                            ToolTip="Search" onclick="imgsearh_Click" />&nbsp;
                                          <asp:ImageButton ID="ImageButton1" runat="server" 
                                ImageUrl="~/Images/Refresh-icon.png" 
                                ToolTip="Refresh" onclick="ImageButton1_Click"  />
                                    </td>
                                         <td align="left">
                                         
                                              <asp:ImageButton ID="ibtnAdd" runat="server" ToolTip="Add New PI" OnClientClick='OpenEntryScreen(0);return false;'
                                            ImageUrl="~/Images/Add-icon.png"  />  &nbsp;
                                             <asp:ImageButton ID="btnExport" runat="server" ToolTip="Export to Excel" 
                                ImageUrl="~/Images/Excel-icon.png" Visible="false" onclick="btnExport_Click" />&nbsp;

                                     <asp:ImageButton ID ="imgQBuilder" ImageUrl="../../images/wizard/database-process-icon.png" ToolTip="Query Builder" runat="server" OnClientClick="OpenQueryBuilder();" height="20px" />
                                    
                                    </td>
                                 

                   </tr>

                <tr>

                <td  colspan="6">

                        <asp:GridView ID="gvPIList" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                CellPadding="1" CellSpacing="0" Width="100%"  DataKeyNames="PI_ID"
                                AllowSorting="true" >
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                   <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>

                                <asp:BoundField HeaderText="PI Code" DataField="Code" ItemStyle-Width="5%" ItemStyle-CssClass="PMSGridItemStyle-css" />
                                <asp:BoundField HeaderText="PI Name" DataField="Name"  ItemStyle-Width="10%" ItemStyle-CssClass="PMSGridItemStyle-css"/>
                                <asp:BoundField HeaderText="Interval" DataField="Interval"  ItemStyle-Width="5%"  ItemStyle-CssClass="PMSGridItemStyle-css"/>
                                <asp:BoundField HeaderText="Description" DataField="Description" ItemStyle-Width="15%"  ItemStyle-Wrap="true"  ItemStyle-CssClass="PMSGridItemStyle-css"/>
                                <asp:BoundField HeaderText="Unit" DataField="UOM"  ItemStyle-Width="5%" ItemStyle-CssClass="PMSGridItemStyle-css" />
                                 <asp:BoundField HeaderText="Data Source" DataField="Datasource"  ItemStyle-Width="10%" ItemStyle-CssClass="PMSGridItemStyle-css" />
                                  <asp:BoundField HeaderText="Status" DataField="Status"  ItemStyle-Width="5%" ItemStyle-CssClass="PMSGridItemStyle-css" />
                                   <asp:TemplateField >
                                        <HeaderTemplate>
                                         Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                         <asp:ImageButton ID="imgUpdate" style="border: 0; width: 14px; height: 14px" Text="Update" ToolTip="Update Definition"  OnClientClick='<%#"OpenEntryScreen((&#39;"+ Eval("PI_ID") +"&#39;),(&#39;"+ Eval("[UOM]") + "&#39;),(&#39;"+ Eval("[Interval]") + "&#39;));return false;"%>'  ForeColor="Black"
                                                                ImageUrl="../../Images/edit.gif" runat="server" />&nbsp;
                                                               <asp:ImageButton ID="ImgView" runat="server" Text="Details" OnClientClick='<%#"OpenScreen((&#39;"+ Eval("PI_ID") +"&#39;));return false;"%>'
                                                            CommandName="Select" ForeColor="Black" ToolTip="PI Values" ImageUrl="~/Images/asl_view.png"
                                                            Height="16px"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="10" OnBindDataItem="BindGrid" />
                            

                </td>
     
       </tr>

       <tr>
       <td>  <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" /></td>
       </tr>
          <tr  style="visibility:hidden">


                <td width="23%" align="right">
                    <asp:Literal ID="ltItemName" runat="server" Text="PI Name :"></asp:Literal>
                </td>
                    <td align="right" class="style1" style="color: #FF0000; width:2% ">
                        *
                    </td>
                    <td width="25%" align="left">
                    <asp:TextBox ID="txtPIName" runat="server" MaxLength="200" Width="200px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPIName" runat="server" 
                    ErrorMessage="PI name is mandatory." ControlToValidate="txtPIName" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                

                <td width="23%" align="right">
                    <asp:Literal ID="ltItemCode" runat="server" Text="PI Code :"></asp:Literal>
                </td>
                    <td align="right" class="style1" style="color: #FF0000; width:2% ">
              &nbsp;
                    </td>
                    <td width="25%" align="left">
                    <asp:TextBox ID="txtPICode" runat="server" MaxLength="50"></asp:TextBox>

                    </td>
                    </tr>

                <tr  style="visibility:hidden">
                <td width="23%" align="right">
                    <asp:Literal ID="ltUnit" runat="server" Text="PI Description :"></asp:Literal>
                </td>
                  <td align="right" class="style1" style="color: #FF0000; width:2% ">
       
                    </td>
                    <td width="25%" align="left">
                       <asp:TextBox ID="txtItemDescription" runat="server" Width="80%" TextMode="MultiLine"  MaxLength="1000" ></asp:TextBox>

                    </td>
                    <td width="23%" align="right">
                        <asp:Literal ID="ltPIContext" runat="server" Text="PI Context :"></asp:Literal>
                    </td>
                      <td align="right" class="style1" style="color: #FF0000; width:2% ">
       
                        </td>
                        <td width="25%" align="left">
                           <asp:TextBox ID="txtContext" runat="server" Width="80%" TextMode="MultiLine"  MaxLength="1000" ></asp:TextBox>


                        </td>
                    </tr>

               <tr  style="visibility:hidden">
                <td width="23%" align="right">
                    <asp:Literal ID="Literal1" runat="server" Text="PI Interval :"></asp:Literal>
                </td>
                   <td align="right" class="style1" style="color: #FF0000; width:2% ">
                        *
                    </td>
                    <td width="25%" align="left">

                         <asp:DropDownList ID="ddlIntervalUnit"  Visible ="false" runat="server" Width="100px">
                        <asp:ListItem Text="Daily" value="Daily"></asp:ListItem>
                        <asp:ListItem Text="Monthly" value="Monthly"></asp:ListItem>
                         <asp:ListItem Text="Quarterly" value="Quarterly"></asp:ListItem>
                          <asp:ListItem Text="Half Yearly" value="Half Yearly"></asp:ListItem>
                           <asp:ListItem Text="Yearly" value="Yearly"></asp:ListItem>
                        </asp:DropDownList>

                    </td>

                    </tr>

                    <tr  style="visibility:hidden">
                    <td colspan="6" align = "center" style="color: #FF0000;" >
                    
                    <asp:Literal ID="ltmessage" runat = "server"> </asp:Literal>
                    </td>
                    </tr>

                    <tr  style="visibility:hidden">
                    <td colspan="6" align="center">
                    &nbsp;
                     <asp:ValidationSummary ID="vsDelivery" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"  runat ="server" ValidationGroup = "vgSubmit" />

                       <asp:Button ID="btnSave" runat="server" Width ="100px" ValidationGroup="vgSubmit" Font-Bold="true"
                                Text="Save" onclick="btnSave_Click" />&nbsp;
                        
                        <asp:Button ID="btnClear" runat="server" Text="Clear Selection" 
                                onclick="btnClear_Click" />

                        </td>
                               
                </tr>
</table>
</div>
</center>
</div>
</center>
</asp:Content>