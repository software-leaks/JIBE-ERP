<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ChecklistIndex.aspx.cs"
    Inherits="ChecklistIndex" Title="Inspection Checklist Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui.min.js"></script>
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
        function Divaddnewlink() {
            document.getElementById("divadd").style.display = "block";
        }
        function hidePopup() {
            document.getElementById("divadd").style.display = "none";
        }
        $(document).ready(function () {
            $(".draggable").draggable();
        });

        function OnAddMaker() {
            document.getElementById('Iframe').src = '../../Infrastructure/Libraries/CompanyType.aspx';
            showModal('dvIframe');
            return false;
        }

        function validation() {

            if (document.getElementById("ctl00_MainContent_ddlCompanyType").value == "0") {
                alert("Please select company type.");
                document.getElementById("ctl00_MainContent_ddlCompanyType").focus();
                return false;
            }


            if (document.getElementById("ctl00_MainContent_HiddenFlag").value != "Edit") {

                if (document.getElementById("ctl00_MainContent_ddlRelation").value == "0") {
                    alert("Please select relation with parent.");
                    document.getElementById("ctl00_MainContent_ddlRelation").focus();
                    return false;
                }
                if (document.getElementById("ctl00_MainContent_ddlParentCompany").value == "0") {
                    alert("Please select parent company.");
                    document.getElementById("ctl00_MainContent_ddlParentCompany").focus();
                    return false;
                }

            }

            if (document.getElementById("ctl00_MainContent_txtCompCode").value.trim() == "") {
                alert("Please enter company code.");
                document.getElementById("ctl00_MainContent_txtCompCode").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_txtCompCode").value != "") {

                if (isNaN(document.getElementById("ctl00_MainContent_txtCompCode").value)) {
                    alert("This field is allow only numeric value");
                    document.getElementById("ctl00_MainContent_txtCompCode").focus()
                    return false;
                }

            }

            if (document.getElementById("ctl00_MainContent_txtCompName").value.trim() == "") {
                alert("Please enter company name.");
                document.getElementById("ctl00_MainContent_txtCompName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtDtIncorp").value.trim() == "") {
                alert("Please enter Date of Incorp.");
                document.getElementById("ctl00_MainContent_txtDtIncorp").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_txtShortName").value.trim() == "") {
                alert("Please enter company short name.");
                document.getElementById("ctl00_MainContent_txtShortName").focus();
                return false;
            }


            return true;
        }





        function RefreshMakerFromChild() {

            document.getElementById("ctl00_MainContent_btnHiddenSubmit").click();
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;">
            <div class="page-title">
                Inspection Checklist Index
            </div>
            <div style="width: 100%; height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePaneCompany" runat="server">
                    <ContentTemplate>
                        <div class="subHeader" style="display: none; position: relative; right: 0px">
                            <asp:Button ID="btnHiddenSubmit" runat="server" Text="btnHiddenSubmit" OnClick="btnHiddenSubmit_Click" />
                        </div>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 70px; width: 100%">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                   
                                    <td align="right" style="width: 10%">
                                        Vessel Type :&nbsp;
                                    </td>
                                    <td style="width: 15%">
                                        <asp:DropDownList ID="ddlVesselTypeFilter" AppendDataBoundItems="true" runat="server"
                                            Width="100%" AutoPostBack="True" 
                                            onselectedindexchanged="ddlVesselTypeFilter_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        <asp:ImageButton ID="imbAddMaker" ImageUrl="~/Images/AddMaker.png" Height="12px"
                                            ToolTip="Add Company Type" runat="server" OnClientClick="return OnAddMaker();"
                                            Visible="false" />
                                    </td>
                                    <td align="right" style="width: 10%">
                                        Checklist Type :&nbsp;
                                    </td>
                                    <td style="width: 15%">
                                        <asp:DropDownList ID="ddlCheklistFilter" AppendDataBoundItems="true" runat="server"
                                            Width="200px" AutoPostBack="True" 
                                            onselectedindexchanged="ddlCheklistFilter_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                     <td align="right" style="width: 15%">
                                        CheckList Name :&nbsp;
                                    </td>
                                    <td align="left" style="width: 15%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="80%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <%--<asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Refresh-icon.png"
                                            OnClick="btnRefresh_Click" ToolTip="Refresh" />--%>
                                            <asp:Button ID="btnRefresh" runat="server"  OnClick="btnRefresh_Click" ToolTip="Refresh" Text="Clear Filter"/>
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <%--<asp:ImageButton ID="ImgAdd" runat="server" ImageUrl="~/Images/Add-icon.png" OnClick="ImgAdd_Click"
                                            ToolTip="Add New Checklist" />--%>
                                            <asp:Button  ID="ImgAdd" runat="server" OnClick="ImgAdd_Click"
                                            ToolTip="Add New Checklist" Text="Add"/>
                                    </td>
                                    <td style="width: 30px">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" Visible="False" />
                                    </td>
                                </tr>
                                
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="GridViewCompany" runat="server" CellPadding="1" OnRowDataBound="GridViewCompany_RowDataBound"
                                    OnSorting="GridViewCompany_Sorting" EmptyDataText="NO RECORDS FOUND!" AllowSorting="True"
                                    AutoGenerateColumns="False" Width="100%" DataKeyNames="ID">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>                                     
                                        <asp:TemplateField HeaderText="CheckList Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblNameHeader" runat="server" CommandName="Sort" CommandArgument="CheckList_Name"
                                                    ForeColor="Black">Name&nbsp;</asp:LinkButton>
                                                <img id="CheckList_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%--  <asp:LinkButton ID="lblName" Style="color: Black" runat="server" Text='<%#Eval("CheckList_Name")%>'
                                                    CommandArgument='<%#Eval("ID")%>' OnCommand="onUpdate"></asp:LinkButton>--%>
                                                <asp:HyperLink ID="lblName" Style="color: Black" runat="server" Text='<%#Eval("CheckList_Name")%>' Target="_blank"
                                                    NavigateUrl='<%#"CheckList.aspx?CHKID="+Eval("ID")+"&Status="+Eval("Status")+"&ParentID="+Eval("Parent_ID")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="220px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vessel Types">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShort_Name" runat="server" Text='<%#Eval("VesselTypes")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Checklist Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegNo" runat="server" Width="200px" Text='<%# Bind("Category_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="280px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="Version">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVersion" runat="server" Width="100px" Text='<%# Eval("Version") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" 
                                                CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Width="100px" Text='<%# Eval("Status").ToString() == "1"?"Published":"Draft" %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" 
                                                 CssClass="PMSGridItemStyle-css">
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

                                                        <asp:HyperLink ID="lblName" Style="color: Black" runat="server"  Target="_blank"
                                                    NavigateUrl='<%#"CheckList.aspx?CHKID="+Eval("ID")+"&Status="+Eval("Status")+"&ParentID="+Eval("Parent_ID")%>'>  
                                                    <asp:Image ID="ImgUpdate" runat="server" ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px" Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"/>
                                                    </asp:HyperLink>

                                                           <%-- <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>--%>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                          </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindCompany" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="dvIframe" style="display: none; width: 600px;" title=''>
            <iframe id="Iframe" src="" frameborder="0" style="height: 295px; width: 100%"></iframe>
        </div>
    </center>
</asp:Content>
