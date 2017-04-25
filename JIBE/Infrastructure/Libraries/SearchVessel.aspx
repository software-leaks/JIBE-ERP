<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="~/Infrastructure/Libraries/SearchVessel.aspx.cs" Inherits="SearchVessel" Title="Search Vessel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager"
    TagPrefix="uc1" %>
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
     <style type="text/css">
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }    
         .style1
         {
             width: 13%;
         }
         .style2
         {
             width: 283px;
         }
         .style3
         {
             width: 163px;
         }
    </style>

    <script language="javascript" type="text/javascript">

        function showDiv(dv) {
            document.getElementById(dv).style.display = "block";
        }

        function closeDiv(dv) {
            document.getElementById(dv).style.display = "None";
        }
        $(document).ready(function () {
            $('#dvAddNewVessel').draggable();
            $('#dvAddNewFleet').draggable();
        });


        function validationOnVesselSave() {

            if (document.getElementById("ctl00_MainContent_ddlVesselManager").value == "0") {
                alert("Please select vessel manager.");
                document.getElementById("ctl00_MainContent_ddlVesselManager").focus();
                return false;
            }

//            if (document.getElementById("ctl00_MainContent_ddlFleet_AddVessel").value == "0") {
//                alert("Please select fleet.");
//                document.getElementById("ctl00_MainContent_ddlFleet_AddVessel").focus();
//                return false;
//            }

            if (document.getElementById("ctl00_MainContent_ddlVesselFlage_AddVessel").value == "0") {
                alert("Please select  vessel flag.");
                document.getElementById("ctl00_MainContent_ddlVesselFlage_AddVessel").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_ddlvessel_AddType").value == "0") {
                alert("Please select  vessel Type.");
                document.getElementById("ctl00_MainContent_ddlVesselFlage_AddVessel").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtIMONo").value.trim() == "") {
                alert("Please enter IMONo.");
                document.getElementById("ctl00_MainContent_txtIMONo").focus();
                return false;
            }

          
            if (document.getElementById("ctl00_MainContent_txtVessel").value.trim() == "") {
                alert("Please enter vessel name.");
                document.getElementById("ctl00_MainContent_txtVessel").focus();
                return false;
            }

            
            if (document.getElementById("ctl00_MainContent_txtVesselShortName").value.trim() == "") {
                alert("Please enter vessel short name.");
                document.getElementById("ctl00_MainContent_txtVesselShortName").focus();
                return false;
            }

//            if (document.getElementById("ctl00_MainContent_txtMMSI").value.trim() == "") {
//                alert("Please enter minimum CTM.");
//                document.getElementById("ctl00_MainContent_txtMMSI").focus();
//                return false;
//            }

          
            return true;
        }


        function DocOpen(filename){
            var conn = '<%=ConfigurationManager.AppSettings["APP_NAME"].ToString() %>'
            var filepath = "/" + conn +"/uploads/MEPowerCurve/";
            // alert(filepath + filename);
            window.open(filepath + filename);
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
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;
            height: 100%;">
            <div class="page-title">
              Vessel
           </div>
            <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
            <div style="height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div  style="padding-top: 5px; padding-bottom: 5px; height: 70px;">
                            <table width="100%" cellpadding="2" cellspacing="1">
                                <tr>
                                  <td align="right" class="style1">
                                        Code/Name/Short Name :&nbsp;
                                    </td>
                                    <td align="left" class="style3" >
                                        <asp:TextBox ID="txtfilter" runat="server" Width="80%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="right" style="width: 18%">
                                        Vessel Manager :&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlFilterVesselManager" Width="100%" runat="server" AppendDataBoundItems="True"
                                            AutoPostBack="true"  OnSelectedIndexChanged="ddlFilterVesselManager_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                     
                                    <%--<td align="right">
                                        Fleet :&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="DDLFleet" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged">
                                         
                                        </asp:DropDownList>
                                    </td>--%>
                                    <td align="right" style="width: 8%">
                                        Vessel :&nbsp;
                                    </td>
                                    <td align="left" style="width: 12%">
                                        <asp:DropDownList ID="DDLVessel" runat="server" Width="120px">
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
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="ImgAddVessel" runat="server" ToolTip="Add New Vessel" OnClick="ImgAddVessel_Click"
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td style="width: 30px">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                                <tr>
                                 
                                    <td align="right" style="width: 8%">
                                        Vessel Flag :&nbsp;
                                    </td>
                                    <td align="left" class="style3">
                                        <asp:DropDownList ID="DDLVesselFlag" Width="100%" runat="server" AutoPostBack="True">
                                           
                                        </asp:DropDownList>
                                    </td>
                                  <td align="right" class="style1">
                                    Vessel Type :&nbsp;
                                    </td>
                                      <td align="left" class="style2">
                                       <asp:DropDownList ID="ddlvesselType" Width="120px" runat="server" AutoPostBack="True">
                                           
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="VesselGridView" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnRowDataBound="VesselGridView_RowDataBound"
                                    DataKeyNames="Vessel_ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                    OnSorting="VesselGridView_Sorting" AllowSorting="true">
                                   <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                   <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="LblVesselNameHeader" runat="server" CommandName="Sort" CommandArgument="Vessel_Name"
                                                    ForeColor="Black">Name&nbsp;</asp:LinkButton>
                                                <img id="Vessel_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LblVesselName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'
                                                    Style="color: Black" CommandArgument='<%#Eval("[Vessel_ID]")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Short Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="LblShortNameHeader" runat="server" CommandName="Sort" CommandArgument="Vessel_Short_Name"
                                                    ForeColor="Black">Short Name&nbsp;</asp:LinkButton>
                                                <img id="Vessel_Short_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LblShortName" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="E-Mail ID">
                                            <ItemTemplate>
                                                <asp:Label ID="LblVessel_EMail" runat="server" Text='<%# Eval("Vessel_EMail")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fleet">
                                            <ItemTemplate>
                                                <asp:Label ID="LblFleet" runat="server" Text='<%# Eval("FleetName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vessel Manager">
                                            <ItemTemplate>
                                                <asp:Label ID="LblManager" runat="server" Text='<%# Eval("VesselManager")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="180px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vessel Flag">
                                            <ItemTemplate>
                                                <asp:Label ID="LblFlag_Name" runat="server" Text='<%# Eval("Flag_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Vessel Type">
                                            <ItemTemplate>
                                                <asp:Label ID="LblVessel_Type" runat="server" Text='<%# Eval("VesselTypes")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Min CTM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMinCTM" runat="server" Text='<%# Eval("Min_CTM")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Call Sign">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCall_Sign" runat="server" Text='<%# Eval("Vessel_Call_sign")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="IMO No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIMONo" runat="server" Text='<%# Eval("Vessel_IMO_No")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sync Flag">
                                            <ItemTemplate>
                                                <%--  <asp:Label ID="lblSyncFlag" runat="server" Text='<%# Eval("Flag_Name")%>'></asp:Label>--%>
                                                <asp:CheckBox ID="chkSyncFlag" runat="server" Checked='<%#Eval("ODM_ENABLED").ToString() == "-1" ? true : false %>' Enabled="false" />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pow">
                                            <HeaderTemplate>
                                                ME PwrCurve Att.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgMEPCMAtt" runat="server" Visible='<%#Eval("ME_Power_Curve_Att").ToString() == "1" ? true : false %>'
                                                    Height="16px" ForeColor="Black" ImageUrl="~/Images/attach-icon.png"></asp:ImageButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
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
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Vessel_ID]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[Vessel_ID]")%>' ForeColor="Black" ToolTip="Delete"
                                                                ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;LIB_VESSELS&#39;,&#39;Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindVesselGrid" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                        </div>

                        <div id="dvAddNewVessel" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
        font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 35%;">
        <center>
            <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                <ContentTemplate>
                    <table width="98%" cellpadding="2" cellspacing="2">
                        <tr>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 25%">
                                Vessel Manager &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlVesselManager" Width="82%" runat="server" OnSelectedIndexChanged="ddlVesselManager_SelectedIndexChanged"
                                    AutoPostBack="true" CssClass="txtInput" />
                            </td>
                        </tr>
                     <%--   <tr>
                            <td align="right">
                                Fleet &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlFleet_AddVessel" Width="82%" runat="server" CssClass="txtInput" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="right">
                                Vessel Flag &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlVesselFlage_AddVessel" Width="82%" runat="server" AppendDataBoundItems="true"
                                    CssClass="txtInput" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Vessel Type &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlvessel_AddType" Width="82%" runat="server" AppendDataBoundItems="true"
                                    CssClass="txtInput" />
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                                IMO Number &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtIMONo" runat="server" Width="80%" MaxLength="50" CssClass="txtInput" style="width:80%;"></asp:TextBox>
                                <%--<asp:ImageButton ID="ImageButton2" runat="server"  ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />--%>
                            </td>
                        
                        </tr>
                          <tr>
                            <td align="right">
                               Year Built &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtYearBuilt" runat="server" Width="80%" MaxLength="100" CssClass="txtInput"></asp:TextBox>
                                <cc1:CalendarExtender TargetControlID="txtYearBuilt" ID="caltxtYearBuilt" Format="dd-MM-yyyy"
                                    runat="server">
                                </cc1:CalendarExtender>
                            </td>
                        
                        </tr>
                        <tr>
                            <td align="right">
                                Vessel Name &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtVessel" runat="server" Width="80%" MaxLength="50" CssClass="txtInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Short Name &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                *
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtVesselShortName" runat="server" Width="80%" MaxLength="50" CssClass="txtInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                E-Mail &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtEmailID" runat="server" Width="80%" MaxLength="250" CssClass="txtInput"></asp:TextBox>
                            </td>
                        </tr>
                             <tr>
                            <td align="right">
                                Length &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLength" runat="server" Width="80%" MaxLength="50" CssClass="txtInput"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td align="right">
                                MMSI &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtMMSI" runat="server" Width="80%" MaxLength="250" CssClass="txtInput"></asp:TextBox>
                            </td>
                        </tr>
                          <tr>
                            <td align="right">
                                CallSign &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCallSign" runat="server" Width="80%" MaxLength="50" CssClass="txtInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Takeover Date &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                
                            </td>
                            <td align="left">
                                <asp:TextBox ID="dtTakeoverDate" runat="server" Width="80%" MaxLength="100" CssClass="txtInput"></asp:TextBox>
                                <cc1:CalendarExtender TargetControlID="dtTakeoverDate" ID="caltxtTakeoverDate" Format="dd-MM-yyyy"
                                    runat="server">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Handover Date &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:TextBox ID="dtHandoverDate" runat="server" Width="80%" MaxLength="100" CssClass="txtInput"></asp:TextBox>
                                <cc1:CalendarExtender TargetControlID="dtHandoverDate" ID="caltxtHandoverDate" Format="dd-MM-yyyy"
                                    runat="server">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Minimum CTM &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                                
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtMinimumCTM" Width="50%" runat="server" CssClass="txtInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Sync Enable &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="chkSyncEnable" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="border-top: 1px solid gray; padding-top: 10px;" align="left">
                                Speed Power Curve File
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left">
                                <asp:HyperLink ID="lnkAttachment" runat="server" Target="_blank"> </asp:HyperLink>
                                <asp:ImageButton ID="ImgTempAttDelete" runat="server" Height="14px" ForeColor="Black"
                                    ToolTip="Delete attached file" ImageUrl="~/Images/Delete.png" OnClick="ImgTempAttDelete_click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="3" style="border-bottom: 1px solid gray; padding-bottom: 15px">
                                <asp:FileUpload ID="FileUploader" Style="width: 90%; height: 18px; background-color: #F2F2F2;
                                    border: 1px solid #cccccc; font-size: 12px; cursor: pointer" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="font-size: 11px; border-width: 0px; background-color: #d8d8d8;"
                                align="center">
                                <asp:Button ID="btnSaveAndAdd" runat="server" Text="Save" OnClientClick="return validationOnVesselSave();"
                                    OnClick="btnsave_Click" />
                                <asp:TextBox ID="txtVesselID" runat="server" Visible="false"></asp:TextBox>
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
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSaveAndAdd" />
                </Triggers>
            </asp:UpdatePanel>
        </center>
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
