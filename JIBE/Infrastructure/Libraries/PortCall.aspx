<%@ Page Title="Port Call" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PortCall.aspx.cs" Inherits="Infrastructure_Libraries_PortCall" %>

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

            if (document.getElementById("ctl00_MainContent_ddlVessel").value == "0") {
                alert("Please enter Vessel");
                document.getElementById("ctl00_MainContent_ddlVessel").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_DDLPort").value == "0") {
                alert("Please enter port.");
                document.getElementById("ctl00_MainContent_DDLPort").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_dtpArrival").value == "") {
                alert("Please Enter Arrival Date.");
                document.getElementById("ctl00_MainContent_dtpArrival").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_dtpBerthing").value == "") {
                alert("Please Enter Berthing Date.");
                document.getElementById("ctl00_MainContent_dtpBerthing").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_dtpDeparture").value == "") {
                alert("Please Enter Departure Date.");
                document.getElementById("ctl00_MainContent_dtpDeparture").focus();
                return false;
            }

            return true;

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
          Port Call
    </div>
    <center>
        <div style="height: 830px; color: Black;">
        
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
                            <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; text-align: center">
                                <table width="100%" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td align="right" style="width: 30%">
                                           Port Name &nbsp;:&nbsp;
                                        </td>
                                        <td align="left" style="width: 40%">
                                            <asp:TextBox ID="txtfilter" runat="server" Width="100%" CssClass="txtInput"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                                WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                        </td>
                                        <td align="right" style="width: 8%">
                                            Vessel :&nbsp;
                                        </td>
                                        <td align="left" style="width: 12%">
                                            <asp:DropDownList ID="DDLVesselFilter" runat="server" Width="120px" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>

                                        <td align="right" style="width: 8%">
                                            Port :&nbsp;
                                        </td>
                                        <td align="left" style="width: 12%">
                                            <asp:DropDownList ID="DDLPortFilter" runat="server" Width="120px" CssClass="txtInput">
                                            </asp:DropDownList>
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
                                            <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Port Call" OnClick="lnkAddNew_Click"
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
                                    <asp:GridView ID="gvPortCall" runat="server" EmptyDataText="NO RECORDS FOUND" ShowHeaderWhenEmpty="true"
                                        AutoGenerateColumns="False" OnRowDataBound="gvPortCall_RowDataBound" DataKeyNames="Port_Call_ID"
                                        CellPadding="1" CellSpacing="0" Width="100%" GridLines="both" OnSorting="gvPortCall_Sorting"
                                        AllowSorting="true">
                                         <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                       <RowStyle CssClass="RowStyle-css" />
                                      <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                        <PagerStyle CssClass="PMSPagerStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Vessel_Name">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblVessel_NameHeader" runat="server" CommandName="Sort" CommandArgument="Vessel_Name"
                                                        ForeColor="Black">Vessel_Name&nbsp;</asp:LinkButton>
                                                    <img id="Vessel_Name" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVessel_Name" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Port_Name">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblPort_NameHeader" runat="server" CommandName="Sort" CommandArgument="Port_Name"
                                                        ForeColor="Black">Port Name&nbsp;</asp:LinkButton>
                                                    <img id="Port_Name" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblPort_Name" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Port_Name") %>'
                                                        Style="color: Black" CommandArgument='<%#Eval("[Port_Call_ID]") + "," + Eval("[Vessel_ID]") %>' OnCommand="onUpdate"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Arrival">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblArrival" runat="server" Text='<%# Eval("Arrival","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Berthing">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBerthing" runat="server" Text='<%# Eval("Berthing","{0:dd/MM/yyyy}")%>' ></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="180px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Departure">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeparture" runat="server" Text='<%# Eval("Departure","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPort_Remarks" runat="server" Text='<%# Eval("Port_Remarks")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="400px" CssClass="PMSGridItemStyle-css">
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
                                                                <asp:ImageButton ID="lbtnEdit" runat="server" CommandArgument='<%#Eval("[Port_Call_ID]") + "," + Eval("[Vessel_ID]") %>'
                                                                    Visible='<%# uaEditFlag %>' ImageUrl="~/images/edit.gif" OnCommand="onUpdate"
                                                                    Text="Edit"></asp:ImageButton>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="lbtnDelete" runat="server" CommandArgument='<%#Eval("[Port_Call_ID]") + "," + Eval("[Vessel_ID]") %>'
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
                                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindPortCall" />
                                    <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                </div>
                            </div>
                            <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                                font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 30%;">
                                <table width="98%" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td align="right">
                                            Vessel &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlVessel" runat="server" Width="120px" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Port &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DDLPort" runat="server" Width="120px" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Arrival &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="dtpArrival" runat="server" Width="80%" MaxLength="100" CssClass="txtInput"></asp:TextBox>
                                            <cc1:CalendarExtender TargetControlID="dtpArrival" ID="caltxtArrival" Format="dd-MM-yyyy"
                                                runat="server">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Berthing &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="dtpBerthing" runat="server" Width="80%" MaxLength="100" CssClass="txtInput"></asp:TextBox>
                                            <cc1:CalendarExtender TargetControlID="dtpBerthing" ID="caltxtBerthingDate" Format="dd-MM-yyyy"
                                                runat="server">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Departure &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="dtpDeparture" runat="server" Width="80%" MaxLength="100" CssClass="txtInput"></asp:TextBox>
                                            <cc1:CalendarExtender TargetControlID="dtpDeparture" ID="caltxtDepartureDate" Format="dd-MM-yyyy"
                                                runat="server">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td align="right">
                                            IsWarRisk &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                          <asp:CheckBox ID="chkWarRisk" runat="server" Text="" />
                                        </td>
                                    </tr>
                                      <tr>
                                        <td align="right">
                                            IsShipCrane Req &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                           <asp:CheckBox ID="chkShipCrane" runat="server" Text="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Ramark &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPortRemark" runat="server" Width="80%" TextMode="MultiLine" Height="120px"
                                                MaxLength="100" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="font-size: 11px; text-align: center; border-style: solid;
                                            border-color: Silver; border-width: 1px">
                                            <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" OnClientClick="return validationOnSave();"
                                                Text="Save" />
                                              <asp:TextBox ID="txtPortCallID" runat="server" Visible="false"></asp:TextBox>
                                              <asp:TextBox ID="txtVesselCode" runat="server" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="6" style="color: #FF0000; font-size: small;">
                                            * Mandatory fields
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="ImgExpExcel" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </center>
</asp:Content>
