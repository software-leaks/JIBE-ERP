

<%@ Page Language="C#" AutoEventWireup="true" Title="KPI List" MasterPageFile="~/Site.master"  CodeFile="KPI_List.aspx.cs"
    Inherits="KPI_List" %>
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


        function OpenDemo() {
            var url = "../Images/TMSA_KPI_Demo.png"
            OpenPopupWindow('KPIDetail', 'KPI Details', url, 'popup', 500, 1050, null, null, false, false, true, null);
        }


        function OpenScreen2(KPI_ID) {
            var url = "../KPI/TMSA_Configure_KPI.aspx?KPI_ID=" + KPI_ID
            //OpenPopupWindow('KPIDetail', 'KPI Details', url, 'popup', 670, 1100, null, null, false, false, true, null);
            window.open(url, "_blank");
        }
            function OpenScreen(KPI_ID) {
                var url = "../KPI/TMSA_KPI_Details.aspx?PI_ID=" + KPI_ID
                window.open(url, "_blank");
            }

            function validatePI() {
                if (document.getElementById("ddlPIList").value == "0") {
                    alert("Please select a PI to add.");
                    return false;
                }
                return true;
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
    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">
        <center>
 
            <table width="100%" cellpadding="2" cellspacing="0" >
                            <tr>
                    <td align="center" colspan="6">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            <asp:Literal ID="ltPageHeader" Text ="KPI List" runat ="server" ></asp:Literal>
                        </div>
                    </td>
                </tr>

                <tr>
                        <td align="right" style="font-weight:bold" width="15%">
                             KPI  Name/Code :
                                        </td>
                        <td align="left" valign="top" width="10%">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="txtInput" Width="200px"></asp:TextBox>
                                     
                        </td>
                        <td align="right" style="font-weight:bold">
                        Category : 
                        </td>
                            <td align="left" valign="top" width="10%">
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="200px">
                                </asp:DropDownList>
                                     
                        </td>
                        <td align="left" valign="top" width="5%">
                            <asp:ImageButton ID="imgsearch" runat="server" 
                                ImageUrl="~/Images/SearchButton.png" 
                                ToolTip="Search" onclick="imgsearch_Click" />
                        </td>
                                                                        
                            <td align="left" valign="top" width="5%">
                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                ImageUrl="~/Images/Refresh-icon.png" 
                                ToolTip="Refresh" onclick="ImageButton1_Click"  />
                            &nbsp;
                                <asp:ImageButton ID="ibtnAdd" runat="server" ToolTip="Add New KPI" OnClientClick='OpenScreen2(0);return false;'
                            ImageUrl="~/Images/Add-icon.png"  />
                            &nbsp;
                              <asp:ImageButton ID="btnExport" runat="server" ToolTip="Export to Excel" 
                                ImageUrl="~/Images/Excel-icon.png" Visible="false" onclick="btnExport_Click" />&nbsp;  
                            </td>                               

                </tr>
                <tr>
                <td  colspan="6">

                        <asp:GridView ID="gvKPIList" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                CellPadding="1" CellSpacing="0" Width="100%"  DataKeyNames="KPI_ID"
                                CssClass="gridmain-css" AllowSorting="true" >
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                            <PagerStyle CssClass="PMSPagerStyle-css" />
                            <SelectedRowStyle BackColor="#FFFFCC" />
                                <Columns>

                                <asp:BoundField HeaderText="KPI Code" DataField="Code" ItemStyle-Width="5%"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="KPI Name" DataField="Name"  ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"  ItemStyle-Wrap="true" />
                                <asp:BoundField HeaderText=" Interval" DataField="Interval"  ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="Description" DataField="Description" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="true" />
                                 <asp:BoundField HeaderText="Status" DataField="KPI_Status" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                   
 

                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" >
                                        <HeaderTemplate>
                                         Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>

                                                        <asp:ImageButton ID="ImgView" runat="server" Text="Update" OnClientClick='<%#"OpenScreen2((&#39;"+ Eval("KPI_ID") +"&#39;));return false;"%>'
                                                            CommandName="Select" ForeColor="Black" ToolTip="KPI Details" ImageUrl="~/Images/asl_view.png"
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



         <tr style="visibility:hidden">


                <td width="15%" align="right">
                    <asp:Literal ID="ltItemName" runat="server" Text="KPI Name :"></asp:Literal>
                </td>
                    <td align="right" class="style1" style="color: #FF0000; width:2% ">
                        *
                    </td>
                    <td width="15%" align="left">
                    <asp:TextBox ID="txtPIName" runat="server" MaxLength="200" Width="200px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPIName" runat="server" 
                    ErrorMessage="KPI name is mandatory." ControlToValidate="txtPIName" ValidationGroup="vgSubmit"
                    ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                

                <td width="15%" align="right">
                    <asp:Literal ID="ltItemCode" runat="server" Text="KPI Code :"></asp:Literal>
                </td>
                    <td align="right" class="style1" style="color: #FF0000; width:2% ">
              &nbsp;
                    </td>
                    <td width="15%" align="left">
                    <asp:TextBox ID="txtPICode" runat="server" MaxLength="50"></asp:TextBox>

                    </td>
                    </tr>

                <tr style="visibility:hidden">
                <td width="23%" align="right">
                    <asp:Literal ID="ltUnit" runat="server" Text="KPI Defination :"></asp:Literal>
                </td>
                  <td align="right" class="style1" style="color: #FF0000; width:2% ">
       
                    </td>
                    <td width="25%" align="left">
                       <asp:TextBox ID="txtItemDescription" runat="server" Width="80%" Height="100px" TextMode="MultiLine"  MaxLength="1000" ></asp:TextBox>

                    </td>
                    <td width="23%" align="right">
                        <asp:Literal ID="ltPIContext" runat="server" Text="PI List :"></asp:Literal>
                    </td>
                      <td align="right" class="style1" style="color: #FF0000; width:2% ">
       
                        </td>
                        <td width="25%" align="left">
                           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div>
                        <asp:DropDownList ID="ddlPIList" Visible="false" runat="server" Width="200px" CssClass="txtInput">
                        </asp:DropDownList>
                          <asp:Button ID="btnAddPI" runat="server" Text="Add PI" OnClientClick="return validatePI();"
                                        onclick="btnAddPI_Click" />
         
                            <br />
                                             
                                <div id="dvPI" runat="server" style="float: left; text-align: left; width: 400px; height: 60px;
                                    border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                    background-color: #ffffff;">
                                    <asp:CheckBoxList ID="chkPI"  RepeatLayout="Flow" RepeatDirection="Horizontal"
                                        runat="server">
                                    </asp:CheckBoxList>
                                </div>
                                                   
                        </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                        </td>
                    </tr>

               <tr style="visibility:hidden">
                <td width="23%" align="right">
                    <asp:Literal ID="Literal1" runat="server" Text="KPI Interval :"></asp:Literal>
                </td>
                   <td align="right" class="style1" style="color: #FF0000; width:2% ">
                        *
                    </td>
                    <td width="25%" align="left">

                         <asp:DropDownList ID="ddlIntervalUnit" Visible="false"  runat="server" Width="100px">
                        <asp:ListItem Text="Daily" value="Daily"></asp:ListItem>
                        <asp:ListItem Text="Monthly" value="Monthly"></asp:ListItem>
                         <asp:ListItem Text="Quarterly" value="Quarterly"></asp:ListItem>
                          <asp:ListItem Text="Half Yearly" value="Half Yearly"></asp:ListItem>
                           <asp:ListItem Text="Yearly" value="Yearly"></asp:ListItem>
                        </asp:DropDownList>

                    </td>

                    </tr>

                    <tr style="visibility:hidden">
                    <td colspan="6" align = "center" style="color: #FF0000;" >
                    
                    <asp:Literal ID="ltmessage" runat = "server"> </asp:Literal>
                    </td>
                    </tr>
                    <tr style="visibility:hidden">
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

</center>
</div>
</center>
</asp:Content>