<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CompanyNK.aspx.cs"
    Inherits="CompanyNK" Title="Company" %>

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

//            if (document.getElementById("ctl00_MainContent_ddlCompanyType").value == "0") {
//                alert("Please select company type.");
//                document.getElementById("ctl00_MainContent_ddlCompanyType").focus();
//                return false;
//            }

//            if (document.getElementById("ctl00_MainContent_txtCompCode").value.trim() == "") {
//                alert("Please enter company code.");
//                document.getElementById("ctl00_MainContent_txtCompCode").focus();
//                return false;
//            }
//            if (document.getElementById("ctl00_MainContent_txtCompCode").value != "") {

//                if (isNaN(document.getElementById("ctl00_MainContent_txtCompCode").value)) {
//                    alert("This field is allow only numeric value");
//                    document.getElementById("ctl00_MainContent_txtCompCode").focus()
//                    return false;
//                }

//            }

            if (document.getElementById("ctl00_MainContent_txtCompName").value.trim() == "") {
                alert("Please enter company name.");
                document.getElementById("ctl00_MainContent_txtCompName").focus();
                return false;
            }

            //            if (document.getElementById("ctl00_MainContent_txtDtIncorp").value.trim() == "") {
            //                alert("Please enter Date of Incorp.");
            //                document.getElementById("ctl00_MainContent_txtDtIncorp").focus();
            //                return false;
            //            }
            //            if (document.getElementById("ctl00_MainContent_txtShortName").value.trim() == "") {
            //                alert("Please enter company short name.");
            //                document.getElementById("ctl00_MainContent_txtShortName").focus();
            //                return false;
            //            }


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
                Company
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
                                <td align="right">
                                        Country :&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCountryFilter" AppendDataBoundItems="true" runat="server"
                                            Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" style="width: 15%">
                                        Code/name/desc/Reg No. :&nbsp;
                                    </td>
                                    <td align="left" style="width: 15%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="80%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="right" style="width: 10%;display:none;">
                                        Company Type :&nbsp;
                                    </td>
                                    <td style="width: 15%;display:none; ">
                                        <asp:DropDownList ID="ddlCompanyTypeFilter" AppendDataBoundItems="true" runat="server"
                                            Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" style="display:none;">
                                        <asp:ImageButton ID="imbAddMaker" ImageUrl="~/Images/AddMaker.png" Height="12px"
                                            ToolTip="Add Company Type" runat="server" OnClientClick="return OnAddMaker();" />
                                    </td>
                                    <td align="right" style="width: 10%;display:none;">
                                        Country Incorp. :&nbsp;
                                    </td>
                                    <td style="width: 15%;display:none;">
                                        <asp:DropDownList ID="ddlCountryIncorpFilter" AppendDataBoundItems="true" runat="server"
                                            Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Refresh-icon.png"
                                            OnClick="btnRefresh_Click" ToolTip="Refresh" />
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ImageUrl="~/Images/Add-icon.png" OnClick="ImgAdd_Click"
                                            ToolTip="Add New Company" />
                                    </td>
                                    <td style="width: 30px">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="display:none;">
                                        Currency :&nbsp;
                                    </td>
                                    <td align="left" style="display:none;">
                                        <asp:DropDownList ID="ddlCurrencyFilter" AppendDataBoundItems="true" runat="server"
                                            Width="50%">
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
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="GridViewCompany" runat="server" CellPadding="1" OnRowDataBound="GridViewCompany_RowDataBound"
                                    OnSorting="GridViewCompany_Sorting" EmptyDataText="NO RECORDS FOUND!" AllowSorting="True"
                                    AutoGenerateColumns="False" Width="100%" GridLines="Both" DataKeyNames="ID">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblCodeHeader" runat="server" CommandName="Sort" CommandArgument="Company_Code"
                                                    ForeColor="Black">Code&nbsp;</asp:LinkButton>
                                                <img id="Company_Code" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Company_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    <%--    <asp:TemplateField HeaderText="Company Type">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblCompany_TypeHeader" runat="server" CommandName="Sort" CommandArgument="Company_Type"
                                                    ForeColor="Black">Company Type&nbsp;</asp:LinkButton>
                                                <img id="Company_Type" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompany_Type" runat="server" Text='<%# Bind("Company_Type") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblNameHeader" runat="server" CommandName="Sort" CommandArgument="Company_Name"
                                                    ForeColor="Black">Name&nbsp;</asp:LinkButton>
                                                <img id="Company_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblName" Style="color: Black" runat="server" Text='<%#Eval("Company_Name")%>'
                                                    CommandArgument='<%#Eval("ID")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="220px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                      <%--  <asp:TemplateField HeaderText="Short Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShort_Name" runat="server" Text='<%#Eval("Short_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Reg No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegNo" runat="server" Width="80px" Text='<%# Bind("Reg_Number") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                       <%-- <asp:TemplateField HeaderText="Date of Incorp">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIncorp" runat="server" Text='<%# Eval("Date_Of_Incorp","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : Eval("Date_Of_Incorp","{0:d/MM/yy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Country of Incorp.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountryIncorp" runat="server" Text='<%# Bind("COUNTRY_INCORP") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>--%>
                                       <%-- <asp:TemplateField HeaderText="Curr.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBase_Curr" runat="server" Text='<%# Bind("Currency_code") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Address"><%-- Visible="false"--%>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Text='<%# Bind("Address") %>'
                                                    Width="120px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" runat="server" Text='<%# Bind("COUNTRY_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone1") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                     <%--   <td>
                                                            <asp:ImageButton ID="ImgVerified" runat="server" Text="Verified" OnCommand="onVerified"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                                ToolTip="Verify " ImageUrl="~/Images/Allot-Flag-Completed.PNG" Height="16px">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUnverified" runat="server" Text="Verified" OnCommand="onVerified"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                                ToolTip="Verify" ImageUrl="~/Images/Allot-Flag-Active.PNG" Height="16px"></asp:ImageButton>
                                                        </td>--%>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;LIB_COMPANY&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
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
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 45%;">
                            <center>
                                <table width="70%" cellpadding="0" cellspacing="10">
                                    <tr style="display: none;">
                                        <td align="right" style="width: 15%">
                                            Company Type : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="right" style="width: 25%">
                                            <asp:DropDownList ID="ddlCompanyType" runat="server" CssClass="txtInput">
                                                <%--onchange="CompanyTypeChanged(this.options[this.selectedIndex].value)"--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right" style="width: 20%">
                                            Relationship with Parent : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            <asp:Label ID="td_Relation" Text="*" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 25%">
                                            <asp:DropDownList ID="ddlRelation" runat="server" Width="102%" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="display: none;">
                                        <td align="right">
                                            Parent Company : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            <asp:Label ID="td_ParentCompany" Text="*" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlParentCompany" runat="server" Width="102%" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            Company Code : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="right">
                                            <asp:TextBox ID="txtCompCode" runat="server" CssClass="txtInput" Width="100%" Text="123"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Company Name : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtCompName" runat="server" MaxLength="50" Width="300px" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td align="right" style="display: none;">
                                            Company Short Name : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; display: none; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left" style="display: none;">
                                            <asp:TextBox ID="txtShortName" runat="server" MaxLength="10" CssClass="txtInput"
                                                Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="display: none;">
                                        <td align="right">
                                            Registration No. : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtRegNo" runat="server" MaxLength="250" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Date of Incorp. : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDtIncorp" runat="server" CssClass="txtInput" Width="100%">26/01/2015</asp:TextBox>
                                            <cc1:CalendarExtender ID="calDtIncorp" runat="server" Enabled="True" TargetControlID="txtDtIncorp"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr style="display: none;">
                                        <td align="right">
                                            Country of Incorp. : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <%--<asp:TextBox ID="txtCountryIncorp"  runat="server"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlCountryIncorp" runat="server" Width="102%" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            Currency : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlCurrency" runat="server" Width="102%" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            Address : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtAddrerss" TextMode="MultiLine" MaxLength="250" Rows="3" Width="298px"
                                                CssClass="txtInput" runat="server"></asp:TextBox>
                                        </td>
                                        <%-- <td align="right" valign="top">
                                Country : &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right">
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="ddlAddressCountry" runat="server" CssClass="txtInput" Width="102%">
                                </asp:DropDownList>
                            </td>--%>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Email : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="250" CssClass="txtInput" Width="300px"></asp:TextBox>
                                        </td>
                                        <td align="right" style="display: none;">
                                            Email 2 : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; display: none; width: 1%" align="right">
                                        </td>
                                        <td align="left" style="display: none;">
                                            <asp:TextBox ID="txtEmail2" runat="server" MaxLength="250" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Phone : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPhone" runat="server" MaxLength="50" CssClass="txtInput" Width="300px"></asp:TextBox>
                                        </td>
                                        <td align="right" style="display: none;">
                                            Phone 2 : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; display: none; width: 1%" align="right">
                                        </td>
                                        <td align="left" style="display: none;">
                                            <asp:TextBox ID="txtPhone2" runat="server" MaxLength="100" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Fax : &nbsp;
                                        </td>
                                        <td style="color: White; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFax1" runat="server" MaxLength="50" CssClass="txtInput" Width="300px"></asp:TextBox>
                                        </td>
                                        <td align="right" style="display: none;">
                                            Fax 2 : &nbsp;
                                        </td>
                                        <td style="color: White; display: none; width: 1%" align="right">
                                        </td>
                                        <td align="left" style="display: none;">
                                            <asp:TextBox ID="txtFax2" runat="server" MaxLength="50" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            Country : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlAddressCountry" runat="server" CssClass="txtInput" Width="304px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="font-size: 11px; text-align: center; padding: 5px 0px 5px 0px;">
                                            <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return  validation();"
                                                OnClick="btnsave_Click" />
                                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false" Width="1%"></asp:TextBox>
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
                            </center>
                        </div>
                        <div id="divVerify" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 45%;">
                            <center>
                                <table width="98%" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td align="right" style="width: 15%">
                                            Company Type : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="right" style="width: 25%">
                                            <asp:DropDownList ID="ddlCompanyType1" runat="server" Width="102%" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right" style="width: 20%">
                                            Relationship with Parent : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            <asp:Label ID="Label1" Text="*" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 25%">
                                            <asp:DropDownList ID="ddlRelation1" runat="server" Width="102%" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Parent Company : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            <asp:Label ID="Label2" Text="*" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlParentCompany1" runat="server" Width="102%" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            Company Code : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="right">
                                            <asp:TextBox ID="txtCompCode1" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Company Name : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtCompName1" runat="server" MaxLength="50" CssClass="txtInput"
                                                Width="100%"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Company Short Name : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtShortName1" runat="server" MaxLength="10" CssClass="txtInput"
                                                Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Registration No. : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtRegNo1" runat="server" MaxLength="250" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Date of Incorp. : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDtIncorp1" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtDtIncorp"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Country of Incorp. : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <%--<asp:TextBox ID="txtCountryIncorp"  runat="server"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlCountryIncorp1" runat="server" Width="102%" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            Currency : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlCurrency1" runat="server" Width="102%" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            Address : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtAddrerss1" TextMode="MultiLine" MaxLength="250" Rows="3" Width="100%"
                                                CssClass="txtInput" runat="server"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="top">
                                            Country : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlAddressCountry1" runat="server" CssClass="txtInput" Width="102%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Email 1 : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEmail1" runat="server" MaxLength="250" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Email 2 : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEmail21" runat="server" MaxLength="250" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Phone 1 : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPhone1" runat="server" MaxLength="50" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Phone 2 : &nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPhone21" runat="server" MaxLength="100" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Fax 1 : &nbsp;
                                        </td>
                                        <td style="color: White; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFax11" runat="server" MaxLength="50" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Fax 2 : &nbsp;
                                        </td>
                                        <td style="color: White; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFax21" runat="server" MaxLength="50" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:CheckBox ID="chkVerify" runat="server" Text="Verify" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="font-size: 11px; text-align: center; border-style: solid;
                                            padding: 5px 0px 5px 0px; border-color: Silver; border-width: 1px; background-color: #d8d8d8;">
                                            <asp:Button ID="btnVerify" runat="server" Text="Verify" OnClick="btnVerify_Click" /><%--OnClientClick="return  validation();"--%>
                                            <asp:TextBox ID="TextBox13" runat="server" Visible="false" Width="1%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <div id="Div2" style="text-align: left; width: 100%; border: 0px solid #cccccc; background-color: #FDFDFD">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="right" style="color: #FF0000; font-size: small;">
                                            * Mandatory fields
                                        </td>
                                    </tr>
                                </table>
                            </center>
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
