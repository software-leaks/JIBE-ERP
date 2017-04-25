<%@ Page Title="Hazard Template" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="HazardTemplate.aspx.cs" Inherits="JRA_Libraries_HazardTemplate" %>

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

            if (document.getElementById('<%= txtHazardDesc.ClientID %>').value.trim() == "") {
                alert("Hazard Description is mandatory.");
                document.getElementById('<%= txtHazardDesc.ClientID %>').focus();
                return false;
            }

            var result = document.getElementById('<%= ddlWorkCategory.ClientID %>').value;
            if (result <= 0) {
                alert("Work Category is mandatory.");
                document.getElementById('<%= ddlWorkCategory.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= txtControlMeasure.ClientID %>').value.trim() == "") {
                alert("Control Measure is mandatory.");
                document.getElementById('<%= txtControlMeasure.ClientID %>').focus();
                return false;
            }

            var result1 = document.getElementById('<%= ddlSeverity.ClientID %>').value;
            if (result1 <= 0) {
                alert("Severity is mandatory.");
                document.getElementById('<%= ddlSeverity.ClientID %>').focus();
                return false;
            }

            var result2 = document.getElementById('<%= ddlLikelihood.ClientID %>').value;
            if (result2 <= 0) {
                alert("Likelihood is mandatory.");
                document.getElementById('<%= ddlLikelihood.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= txtInitiakRisk.ClientID %>').value.trim() == "") {
                alert("Severity and Likelihood are mandatory for Initial Risk.");
                return false;
            }





            //            if (document.getElementById("ctl00_MainContent_txtElevation").value != "") {
            //                if (isNaN(document.getElementById("ctl00_MainContent_txtElevation").value)) {
            //                    alert("Elevation allows numeric value only.");
            //                    document.getElementById("ctl00_MainContent_txtElevation").focus();
            //                    return false;
            //                }
            //            }

            return true;
        }
        function SetHzID(val) {

          
            var hfHzID = document.getElementById('<%= hfHzID.ClientID %>');
            hfHzID.value = val;

            $("#btnHdnOpenClick").click();
            return false;

        }
        function SetDelHzID(val) {
           
            var hfHzID = document.getElementById('<%= hfHzID.ClientID %>');
            hfHzID.value = val;
            var hfAns = document.getElementById('<%= hfAns.ClientID %>');
            hfAns.value = confirm('Are you sure want to delete?');

            $("#btnHdnDelClick").click();
            return false;
            
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
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;
            height: 100%;">
            <div class="page-title">
                Hazard Template
            </div>
            <div style="height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px;">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td style="width: 15%" align="right">
                                        Parent Work Category :&nbsp;
                                    </td>
                                    <td style="width: 20%" align="left">
                                        <asp:DropDownList ID="ddlParentWorkCateg" Width="98%" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlParentWorkCateg_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 8%" align="right">
                                        Work Category :&nbsp;
                                    </td>
                                    <td style="width: 20%" align="left">
                                        <asp:DropDownList ID="ddlChildWorkCateg" Width="98%" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" Style="height: 22px" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Hazard Template" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" Style="width: 24px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:HiddenField ID="hfHzID" runat="server" />
                                  <asp:HiddenField ID="hfAns" runat="server" />
                                <asp:GridView ID="gvHazard" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvHazard_RowDataBound" DataKeyNames="Hazard_ID" CellPadding="1"
                                    CellSpacing="0" Width="100%" GridLines="both" OnSorting="gvHazard_Sorting" AllowSorting="true"
                                    CssClass="gridmain-css">
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                     <%--   <asp:TemplateField>
                                            <HeaderTemplate>
                                                Work Category
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblWC" runat="server" Text='<%#Eval("Work_Category_Name") %>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css"
                                                VerticalAlign="Top"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblHazard_descriptionHead" runat="server" CommandName="Sort"
                                                    CommandArgument="Hazard_description" ForeColor="Black">Hazard Description&nbsp;</asp:LinkButton>
                                                <img id="Hazard_description" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblHazard_descriptionView" runat="server"  
                                                    Text='<%#Eval("Hazard_Description") %>'  ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css"
                                                VerticalAlign="Top"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblControl_MeasureView" runat="server" CommandName="Sort" CommandArgument="Control_Measure"
                                                    ForeColor="Black">Control Measure&nbsp;</asp:LinkButton>
                                                <img id="Control_Measure" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblControl_MeasureView" runat="server" Text='<%#Eval("Control_Measure")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css"
                                                VerticalAlign="Top"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblSeverityHEader" runat="server" CommandName="Sort" CommandArgument="Severity"
                                                    ForeColor="Black">Severity&nbsp;</asp:LinkButton>
                                                <img id="Severity" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeverityView" runat="server" Text='<%#Eval("Severity")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css"
                                                VerticalAlign="Top"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblCountryHeader" runat="server" CommandName="Sort" CommandArgument="Likelihood"
                                                    ForeColor="Black">Likelihood&nbsp;</asp:LinkButton>
                                                <img id="Likelihood" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLikelihoodView" runat="server" Text='<%# Eval("Likelihood") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css"
                                                VerticalAlign="Top"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblInitial_Risk_HEader" runat="server" CommandName="Sort" CommandArgument="Initial_Risk"
                                                    ForeColor="Black">Initial Risk&nbsp;</asp:LinkButton>
                                                <img id="Initial_Risk" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInitial_Risk_View" runat="server" Text='<%#Eval("Initial_Risk")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css"
                                                VerticalAlign="Top"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblAdditional_Control_MeasuresHeader" runat="server" CommandName="Sort"
                                                    CommandArgument="Additional_Control_Measures" ForeColor="Black">Additional Control Measures&nbsp;</asp:LinkButton>
                                                <img id="Additional_Control_Measures" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdditional_Control_MeasuresView" runat="server" Text='<%# Eval("Additional_Control_Measures") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css"
                                                VerticalAlign="Top"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblAdditional_Modified_RiskHeader" runat="server" CommandName="Sort"
                                                    CommandArgument="Modified_Risk" ForeColor="Black">Modified Risk&nbsp;</asp:LinkButton>
                                                <img id="Modified_Risk" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblModified_Risk" runat="server" Text='<%# Eval("Modified_Risk") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css"
                                                VerticalAlign="Top"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update"  
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Hazard_ID]")%>' ForeColor="Black"
                                                                OnClientClick=<%# string.Format("SetHzID('{0}');return false;", Eval("Hazard_ID")) %> ToolTip="Edit"
                                                                ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick=<%# string.Format("SetDelHzID('{0}');return false;", Eval("Hazard_ID")) %>
                                                                CommandArgument='<%#Eval("[Hazard_ID]")%>' ForeColor="Black" ToolTip="Delete"
                                                                ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;JRA_LIB_HAZARD_TEMPLATE&#39;,&#39;Hazard_ID="+Eval("Hazard_ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css"
                                                VerticalAlign="Top"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindHazardTemplateGrid" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>

                        <asp:UpdatePanel ID="updPopup" runat="server">
                        <ContentTemplate>
                             <div style="display:none"><asp:Button ID="btnHdnOpenClick" runat="server"  OnClick="btnUpd_OnClick" ClientIDMode="Static"/>
                             <asp:Button ID="btnHdnDelClick" runat="server"  OnClick="btnDel_OnClick" ClientIDMode="Static"/></div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 30%;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 10%" valign="top">
                                        Work Category &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right" valign="top">
                                        *
                                    </td>
                                    <td align="left" style="width: 30%" valign="top">
                                        <asp:DropDownList ID="ddlWorkCategory" CssClass="txtInput" Width="97%" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 10%" valign="top">
                                        Hazard Description &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right" valign="top">
                                        *
                                    </td>
                                    <td align="left" style="width: 30%">
                                        <asp:TextBox ID="txtHazardDesc" CssClass="txtInput" Width="95%" MaxLength="1000"
                                            runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        Control Measure &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right" valign="top">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtControlMeasure" CssClass="txtInput" Width="95%" MaxLength="1000"
                                            runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" valign="top">
                                        Severity &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right" valign="top">
                                        *
                                    </td>
                                    <td valign="top">
                                        <asp:DropDownList ID="ddlSeverity" CssClass="txtInput" Width="97%" runat="server"
                                            OnSelectedIndexChanged="ddlSeverity_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        Likelihood &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right" valign="top">
                                        *
                                    </td>
                                    <td valign="top">
                                        <asp:DropDownList ID="ddlLikelihood" CssClass="txtInput" Width="97%" runat="server"
                                            OnSelectedIndexChanged="ddlLikelihood_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Initial Risk &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right" valign="top">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtInitiakRisk" CssClass="txtInput" Width="95%" MaxLength="10" runat="server" Enabled="false"
                                            ReadOnly="true">
                                        </asp:TextBox>
                                        <asp:TextBox ID="txtInitiakRiskValue" CssClass="txtInput" Width="95%" MaxLength="10"
                                            runat="server" Style="display: none">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        Additional Control Measure &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAdditionalCntrolMeasure" CssClass="txtInput" Width="95%" MaxLength="1000"
                                            runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top">
                                        Modified Risk &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td valign="top">
                                        <asp:DropDownList ID="ddlModifiedRisk" CssClass="txtInput" Width="97%" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="font-size: 11px; text-align: center; border-style: solid;
                                        border-color: Silver; border-width: 1px">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" OnClientClick="return Validation();" />
                                        <asp:TextBox ID="txtHazardID" runat="server" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 0px solid #cccccc;
                                            background-color: #FDFDFD">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="right" style="color: #FF0000; font-size: small;">
                                        * Mandatory fields
                                    </td>
                                </tr>
                            </table>
                        </div>
                        </ContentTemplate>
                   
                        </asp:UpdatePanel>
                        

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
