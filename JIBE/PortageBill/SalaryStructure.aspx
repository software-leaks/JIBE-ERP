<%@ Page Title="Salary Structure" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SalaryStructure.aspx.cs" Inherits="SalaryStructure" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="tlk4" %>--%>
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
    <style type="text/css">
        .InvisibleCol
        {
            display: none;
        }
        .style1
        {
            width: 498px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function Divaddnewlink() {
            document.getElementById("divadd").style.display = "block";
        }

        function validation() {



            var e = document.getElementById('<%=DDLParentList.ClientID %>');
            //var strUser = e.options[e.selectedIndex].value;
            if (e.selectedIndex <= 0) {
                alert("Parent type is required field!");
                e.focus();
                return false;
            }

            var parentype = $('#<%=DDLParentList.ClientID%> :selected').text().trim();


            if (document.getElementById("ctl00_MainContent_txtName").value.trim() == "") {
                alert("Please enter name.");
                document.getElementById("ctl00_MainContent_txtName").focus();
                return false;
            }





            if (document.getElementById("ctl00_MainContent_txtName").value.trim() == "") {
                alert("Please enter name.");
                document.getElementById("ctl00_MainContent_txtName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtDescription").value.trim() == "") {
                alert("Please enter description.");
                document.getElementById("ctl00_MainContent_txtDescription").focus();
                return false;
            }

            var itemname = document.getElementById("ctl00_MainContent_txtName").value.trim()
            var table = document.getElementById('<%=gvSalaryStructure.ClientID%>');
            var hdrindex = document.getElementById('<%=hdfRowIndex.ClientID%>');
            for (var i = 1, row; row = table.rows[i]; i++) {
                if (row.cells[1].innerText.trim() == parentype) {
                    if (row.cells[2].innerText.trim() == itemname && hdrindex.value != (i - 1)) {
                        alert("Item Name already exists in the database!");
                        return false;
                    }
                }
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 70%;
            height: 100%;">
            <div class="page-title">
                Salary Structure
            </div>
            <div style="height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Parent Type :&nbsp;
                                    </td>
                                    <td style="text-align: left" class="style1">
                                        <asp:DropDownList ID="ddlParetType" Width="25%" CssClass="txtInput" runat="server">
                                            <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Deduction" Value="19"></asp:ListItem>
                                            <asp:ListItem Text="Earning" Value="18"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />--%>
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
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New" OnClick="ImgAdd_Click"
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
                                <asp:GridView ID="gvSalaryStructure" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" OnRowDataBound="gvSalaryStructure_RowDataBound" DataKeyNames="Code"
                                    CellPadding="1" CellSpacing="0" Width="100%" GridLines="both" OnRowCommand="gvSalaryStructure_RowCommand"
                                    OnSorting="gvSalaryStructure_Sorting" AllowSorting="true">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Order" ShowHeader="False">
                                            <ItemTemplate>
                                                <table cellpadding="0" cellspacing="0" style="margin-right: 5px">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgBtnMoveUp" runat="server" ImageUrl="~/images/Arrow2 - Up.png"
                                                                ToolTip="Cleck to set order" CommandName="MoveUp" CommandArgument='<%#Eval("Code") +"," + Eval("Parent_Code") %>'
                                                                AlternateText="Up"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;<asp:ImageButton ID="ImgBtnMoveDown" runat="server" ImageUrl="~/images/Arrow2 - Down.png"
                                                                ToolTip="Cleck to set order" CommandName="MoveDown" CommandArgument='<%#Eval("Code") +"," + Eval("Parent_Code") %>'
                                                                AlternateText="Down"></asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" Width="20px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sort Order">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSortOrder" runat="server" Text='<%# Eval("Sort_Order")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Parent Type">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblParenttypeHeader" runat="server" ForeColor="Black">Parent Type&nbsp;</asp:Label>
                                                <img id="Component_Type" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblParenttypee" runat="server" Text='<%#Eval("Component_Type")%>'
                                                    Style="color: Black" CommandArgument='<%#Eval("Code")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblNameHeader" runat="server" ForeColor="Black">Name&nbsp;</asp:Label>
                                                <img id="Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblName" runat="server" Text='<%#Eval("Name")%>' Style="color: Black"
                                                    CommandArgument='<%#Eval("Code")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="110px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="LblManager" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salary Type">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSalaryTypetHeader" runat="server" ForeColor="Black">Salary Type&nbsp;</asp:Label>
                                                <img id="SalaryTypeDetails" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblSalaryType" runat="server" Text='<%#Eval("SalaryTypeDetails")%>'
                                                    Style="color: Black" CommandArgument='<%#Eval("Code")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payable At">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblPayableAtHeader" runat="server" ForeColor="Black">Payable At&nbsp;</asp:Label>
                                                <img id="PayBleAtDetails" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblpaybleat" runat="server" Text='<%#Eval("PayBleAtDetails")%>'
                                                    Style="color: Black" CommandArgument='<%#Eval("Code")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Key Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKey_Value" runat="server" Text='<%# Eval("Key_Value")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vessel Specific">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVessel_Specific" runat="server" Text='<%# Eval("Vessel_Specific").ToString() == "0" ? "No":"Yes"  %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Auto Populate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAuto_Populate" runat="server" Text='<%# Eval("Auto_Populate").ToString() == "0" ? "No":"Yes"  %>'></asp:Label>
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
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Code]")+";"+Container.DataItemIndex%>'
                                                                ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[Code]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
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
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="100" OnBindDataItem="BindSalaryStructure" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 20%;">
                            <table cellpadding="2" cellspacing="2" width="100%" style="padding: 10px 10px 10px 10px">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Parent Type :&nbsp;&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="DDLParentList" Width="101%" CssClass="txtInput" runat="server"
                                            AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Name :&nbsp;&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                       <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtName"
                                                ValidationExpression="^[a-zA-Z0-9\s-_]+$" ErrorMessage="no special character" Display="None"></asp:RegularExpressionValidator>
                                            <tlk4:ValidatorCalloutExtender ID="v1" TargetControlID="RegularExpressionValidator1" runat="server">
                                            </tlk4:ValidatorCalloutExtender>
                                        <asp:HiddenField ID="hdfRowIndex" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Description :&nbsp;&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDescription" Width="99%" MaxLength="100" CssClass="txtInput"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Sort Order :&nbsp;&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">                                       
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSortOrder" Width="99%" MaxLength="100" CssClass="txtInput"
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Key Value :&nbsp;&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlkeyvalue" Width="101%" CssClass="txtInput" runat="server"
                                            AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Salary Type :&nbsp;&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlSalaryType" Width="101%" CssClass="txtInput" runat="server"
                                            AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                    <tr>
                                    <td align="right">
                                       Payble At :&nbsp;&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlPaybleAt" Width="101%" CssClass="txtInput" runat="server"
                                            AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Vessel Specific :&nbsp;&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkVesselSpecific" Width="10%" CssClass="txtInput" runat="server"
                                            Checked='<%# Eval("Vessel_Specific").ToString() == "0" ? true:false %>' AppendDataBoundItems="true">
                                        </asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Auto Populate :&nbsp;&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkAutoPopulate" Width="10%" CssClass="txtInput" runat="server"
                                            Checked='<%# Eval("Auto_Populate").ToString() == "0" ? true:false %>' AppendDataBoundItems="true">
                                        </asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center; padding: 5px 5px 5px 5px;
                                        border-color: Silver; border-width: 1px">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                        <asp:TextBox ID="txtCode" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                        <%--OnClientClick="return validation();"--%>
                                        <asp:TextBox ID="txtParentCode" runat="server" Visible="false" Width="1px"></asp:TextBox>
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
