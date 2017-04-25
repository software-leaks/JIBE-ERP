<%@ Page Title="Drill Type" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SCM_DrillTypes.aspx.cs" Inherits="QMS_SCM_SCM_DrillTypes" %>

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
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
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

            if (document.getElementById("ctl00_MainContent_ddlVessel").value == "0") {
                alert("Please select Vessel Name.");
                document.getElementById("ctl00_MainContent_ddlVessel").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_ddlDrillName").value == "0") {
                alert("Please select Drill Name.");
                document.getElementById("ctl00_MainContent_ddlDrillName").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_ddlFrequency").value == "0") {
                alert("Please select Frequency.");
                document.getElementById("ctl00_MainContent_ddlFrequency").focus();
                return false;
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
               Drill Type
             </div>
            <div style="height: 650px; width: 800px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 15%">
                                       Vessel Name :&nbsp;
                                    </td>
                                    <td align="left" style="width: 30%">
                                         <asp:DropDownList ID="ddlVessel_Name" runat="server" Width="100%" CssClass="txtInput"
                                            AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="--ALL--"></asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td align="right" style="width: 15%">
                                        Drill Name :&nbsp;
                                    </td>
                                    <td align="left" style="width: 25%">
                                        <asp:DropDownList ID="ddlDrill_Name" runat="server" Width="100%" CssClass="txtInput"
                                            AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="--ALL--"></asp:ListItem>
                                            <asp:ListItem Value="Abandon Ship Drill" Text="Abandon Ship Drill"></asp:ListItem>
                                            <asp:ListItem Value="Lifeboat Launching into water" Text="Lifeboat Launching into water"></asp:ListItem>
                                             <asp:ListItem Value="Oil Pollution Prevention" Text="Oil Pollution Prevention"></asp:ListItem>
                                              <asp:ListItem Value="Fire Drill" Text="Fire Drill"></asp:ListItem>
                                              <asp:ListItem Value="Enclosed Space Entry" Text="Enclosed Space Entry"></asp:ListItem>
                                              <asp:ListItem Value="Emergency Steering Gear Drill" Text="Emergency Steering Gear Drill"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                       

                                    </td>
                                    <td align="right" style="width: 15%">
                                      
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
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Currency" OnClick="ImgAdd_Click"
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
                                <asp:GridView ID="gvWorkListAccess" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" OnRowDataBound="gvWorkListAccess_RowDataBound" DataKeyNames="ID"
                                    CellPadding="1" CellSpacing="0" Width="100%" GridLines="both" OnSorting="gvWorkListAccess_Sorting"
                                    AllowSorting="true">
                                  <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                  <RowStyle CssClass="RowStyle-css" />
                                  <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vessel Name" SortExpression="Vessel Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblVessel_Name" runat="server" CommandName="Sort" CommandArgument="Vessel_Name"
                                                    ForeColor="Black">Vessel Name</asp:LinkButton>
                                                <img id="Vessel_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblVesselName" runat="server" CommandArgument='<%#Eval("Vessel_Name")%>'
                                                    Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>' Style="color: Black"
                                                    OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Drill Name" SortExpression="DRILL_NAME">
                                            <HeaderTemplate>
                                               Drill Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDRILL_NAME" runat="server" Text='<%#Eval("DRILL_NAME")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Frequency" SortExpression="FREQUENCY">
                                            <HeaderTemplate>
                                               Frequency
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblFrequency" runat="server" Text='<%#Eval("FREQUENCY")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
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
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete"
                                                                ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                        </td>
                                                       <%-- <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;LIB_CURRENCY&#39;,&#39;Currency_ID="+Eval("Currency_ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>--%>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindDrillType" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 30%;">
                            <table width="98%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Vessel Name&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                          <asp:DropDownList ID="ddlVessel" CssClass="txtInput" runat="server" AppendDataBoundItems="true"
                                            Width="102%">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Drill Name&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                   *
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlDrillName" CssClass="txtInput" runat="server" AppendDataBoundItems="true"
                                            Width="102%">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Value="Abandon Ship Drill" Text="Abandon Ship Drill"></asp:ListItem>
                                            <asp:ListItem Value="Lifeboat Launching into water" Text="Lifeboat Launching into water"></asp:ListItem>
                                             <asp:ListItem Value="Oil Pollution Prevention" Text="Oil Pollution Prevention"></asp:ListItem>
                                              <asp:ListItem Value="Fire Drill" Text="Fire Drill"></asp:ListItem>
                                              <asp:ListItem Value="Enclosed Space Entry" Text="Enclosed Space Entry"></asp:ListItem>
                                              <asp:ListItem Value="Emergency Steering Gear Drill" Text="Emergency Steering Gear Drill"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                             <tr>
                                    <td align="right">
                                        Frequency&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                   *
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlFrequency" CssClass="txtInput" runat="server" AppendDataBoundItems="true"
                                            Width="102%">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                            <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                            <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; border-style: solid;
                                        border-color: Silver; border-width: 1px;background-color:#d8d8d8;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validation();"
                                            OnClick="btnsave_Click" />
                                        <asp:TextBox ID="txtDrillID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                    </td>
                                </tr>
                               <tr>
                                    <td colspan="3" align="center" style="color: #FF0000; font-size: small;">
                                       <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
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

