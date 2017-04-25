<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Port.aspx.cs"
    Inherits="Port" Title="Port" %>

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
    <script language="javascript" type="text/javascript">

        function Validation() {

            if (document.getElementById("ctl00_MainContent_txtPortName").value.trim() == "") {
                alert("Please enter port name.");
                document.getElementById("ctl00_MainContent_txtPortName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_ddlPortCountry").value == "0") {
                alert("Please select the country.");
                document.getElementById("ctl00_MainContent_ddlPortCountry").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtLatDeg").value.trim() == "") {
                alert("Latitude is a madatory field.");
                document.getElementById("ctl00_MainContent_txtLatDeg").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtLatDeg").value != "") {
                if (isNaN(document.getElementById("ctl00_MainContent_txtLatDeg").value)) {
                    alert("Latitude allows numeric value only.");
                    document.getElementById("ctl00_MainContent_txtLatDeg").focus();
                    return false;
                }
            }

            if (document.getElementById("ctl00_MainContent_txtLatMin").value.trim() == "") {
                alert("Latitude degree is a mandatory field.");
                document.getElementById("ctl00_MainContent_txtLatMin").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtLatMin").value != "") {
                if (isNaN(document.getElementById("ctl00_MainContent_txtLatMin").value)) {
                    alert("Latitude degree allows numeric value only.");
                    document.getElementById("ctl00_MainContent_txtLatMin").focus();
                    return false;
                }
            }

            if (document.getElementById("ctl00_MainContent_txtLonDeg").value.trim() == "") {
                alert("Longitudes is a mandatory field.");
                document.getElementById("ctl00_MainContent_txtLonDeg").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtLonDeg").value != "") {
                if (isNaN(document.getElementById("ctl00_MainContent_txtLonDeg").value)) {
                    alert("Longitudes allows numeric value only.");
                    document.getElementById("ctl00_MainContent_txtLonDeg").focus();
                    return false;
                }
            }

            if (document.getElementById("ctl00_MainContent_txtLonMin").value.trim() == "") {

                alert("Longitudes degree is a mandatory field.");
                document.getElementById("ctl00_MainContent_txtLonMin").focus();
                return false;

            }

            if (document.getElementById("ctl00_MainContent_txtLonMin").value != "") {
                if (isNaN(document.getElementById("ctl00_MainContent_txtLonMin").value)) {
                    alert("Latitude allows numeric value only.");
                    document.getElementById("ctl00_MainContent_txtLonMin").focus();
                    return false;
                }
            }

            if (document.getElementById("ctl00_MainContent_txtUTC").value != "") {
                if (isNaN(document.getElementById("ctl00_MainContent_txtUTC").value)) {
                    alert("UTC allows numeric value only.");
                    document.getElementById("ctl00_MainContent_txtUTC").focus();
                    document.getElementById("ctl00_MainContent_txtUTC").select();
                    return false;
                }
            }



            return true;
        }

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
              <div class="page-title">
              Port
             </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td style="width: 15%" align="right">
                                        Port Name :&nbsp;
                                    </td>
                                    <td style="width: 20%" align="left">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="98%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td style="width: 8%" align="right">
                                        Country :&nbsp;
                                    </td>
                                    <td style="width: 20%" align="left">
                                        <asp:DropDownList ID="ddlPortCountryFilter" Width="98%" runat="server" AppendDataBoundItems="true">
                                           
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Port" OnClick="ImgAdd_Click"
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
                                <asp:GridView ID="GridViewPort" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="GridViewPort_RowDataBound" DataKeyNames="PORT_ID" CellPadding="1"
                                    CellSpacing="0" Width="100%" GridLines="both" OnSorting="GridViewPort_Sorting"
                                    AllowSorting="true">
                                   <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                   <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Port Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblPortCodeHeader" runat="server" CommandName="Sort" CommandArgument="PORT_NAME"
                                                    ForeColor="Black">Port Name&nbsp;</asp:LinkButton>
                                                <img id="PORT_NAME" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblPortCode" runat="server" CommandArgument='<%#Eval("PORT_ID")%>'
                                                    Text='<%#Eval("PORT_NAME") %>' Style="color: Black" OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblPortCountryHeader" runat="server" CommandName="Sort" CommandArgument="COUNTRY_NAME"
                                                    ForeColor="Black">Country&nbsp;</asp:LinkButton>
                                                <img id="COUNTRY_NAME" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPortCountry" runat="server" Text='<%#Eval("COUNTRY_NAME")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="130px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BP Code">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblBpCodeHeader" runat="server" CommandName="Sort" CommandArgument="BP_CODE"
                                                    ForeColor="Black">BP Code&nbsp;</asp:LinkButton>
                                                <img id="BP_CODE" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBpCode" runat="server" Text='<%# Eval("BP_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lattitude">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPort_Lat" runat="server" Width="60px" Text='<%# Eval("PORT_LAT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Longitude">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPORT_LON" runat="server" Width="60px" Text='<%# Eval("PORT_LON") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ocean">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOcean" runat="server" Text='<%# Eval("OCEAN") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UTC">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUTC" runat="server" Text='<%# Eval("UTC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="War Risk">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWarRisk" runat="server" Text='<%# Eval("WarRisk") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
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
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[PORT_ID]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[PORT_ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;Lib_Ports&#39;,&#39;PORT_ID="+Eval("PORT_ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindPortGrid" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 30%;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="right" style="width: 10%">
                                        Port Name &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:TextBox ID="txtPortName" runat="server" Width="99%" MaxLength="50" CssClass="txtInput"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        BP Code &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBPCode" CssClass="txtInput" Width="99%" MaxLength="10" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Country &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlPortCountry" CssClass="txtInput" Width="99%" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                   <tr>
                                    <td align="right">
                                        War Risk &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                       &nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkWarRisk" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Latitude &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <table cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtLatDeg" CssClass="txtInput" MaxLength="3" Width="30px" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLatMin" CssClass="txtInput" MaxLength="2" Width="30px" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoNorthSouth" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Value="N">North</asp:ListItem>
                                                        <asp:ListItem Value="S">South</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Longitudes&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <table cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtLonDeg" CssClass="txtInput" MaxLength="3" Width="30px" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLonMin" CssClass="txtInput" MaxLength="2" Width="30px" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdoEastWest" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Value="E">East</asp:ListItem>
                                                        <asp:ListItem Value="W">West</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Ocean&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtOcean" CssClass="txtInput" Width="80%" MaxLength="2" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        UTC&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtUTC" CssClass="txtInput" Width="80%" MaxLength="9" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; border-style: solid;
                                        border-color: Silver; border-width: 1px;background-color:#d8d8d8;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" OnClientClick="return Validation();" />
                                        <asp:TextBox ID="txtPortID" runat="server" Visible="false"></asp:TextBox>
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
                                    <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                        * Mandatory fields
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
