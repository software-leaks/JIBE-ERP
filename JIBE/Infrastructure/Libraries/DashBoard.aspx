<%@ Page Title="Dashboard Snippets" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DashBoard.aspx.cs" Inherits="Infrastructure_Libraries_DashBoard" %>

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
    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("ctl00_MainContent_txtSnippetID").value == "") {
                alert("Please enter snippet ID.");
                document.getElementById("ctl00_MainContent_txtSnippetID").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtSnippetName").value == "") {
                alert("Please enter name.");
                document.getElementById("ctl00_MainContent_txtSnippetName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtSnippetFunctionName").value == "") {
                alert("Please enter function name.");
                document.getElementById("ctl00_MainContent_txtSnippetFunctionName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_ddlDepartment_Add").value == "0") {
                alert("Please select the department.");
                document.getElementById("ctl00_MainContent_ddlDepartment_Add").focus();
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
      <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                   <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 90%;
            height: 100%;">
             <div class="page-title">
               Dash Board Snippet
            </div>
         
            <div style="height: 650px; width: 100%; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td align="right" style="width: 30%">
                                        Search By Snippet ID/ Name / function :&nbsp;
                                    </td>
                                    <td align="left" style="width: 20%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="100%" CssClass="txtInput"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="right" style="width: 15%">
                                        Department&nbsp;:&nbsp;
                                    </td>
                                    <td style="width: 15%">
                                        <asp:DropDownList ID="ddl_Department_Filter" Width="100%" runat="server" CssClass="txtInput" />
                                    </td>
                                    <td style="width: 15%">
                                        <asp:CheckBox ID="chkAutoRefresh_Filter" Text="Auto Refresh" runat="server" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" TabIndex="0" OnClick="btnFilter_Click"
                                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add" OnClick="ImgAdd_Click"
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvSnippet" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvSnippet_RowDataBound" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both" OnSorting="gvSnippet_Sorting" AllowSorting="true">
                                   <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                   <RowStyle CssClass="RowStyle-css" />
                                   <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Snippet_ID">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblSnippet_IDHeader" runat="server" CommandName="Sort" CommandArgument="Snippet_ID"
                                                    ForeColor="Black">Snippet ID&nbsp;</asp:LinkButton>
                                                <img id="Snippet_ID" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblSnippet_ID" runat="server" Text='<%#Eval("Snippet_ID")%>'
                                                    Style="color: Black" CommandArgument='<%#Eval("ID")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Snippet">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblSnippet_NameHeader" runat="server" CommandName="Sort" CommandArgument="Snippet_Name"
                                                    ForeColor="Black">Snippet&nbsp;</asp:LinkButton>
                                                <img id="Snippet_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSnippet_Name" runat="server" Text='<%#Eval("Snippet_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Function">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblSnippet_Function_NameHeader" runat="server" CommandName="Sort"
                                                    CommandArgument="Snippet_Function_Name" ForeColor="Black">Function&nbsp;</asp:LinkButton>
                                                <img id="Snippet_Function_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSnippet_Function_Name" runat="server" Text='<%#Eval("Snippet_Function_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblDepartment_NameHeader" runat="server" CommandName="Sort" CommandArgument="Department"
                                                    ForeColor="Black">Department&nbsp;</asp:LinkButton>
                                                <img id="Department" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepartment_Name" runat="server" Text='<%#Eval("Department")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Departmentcolor">
                                            <HeaderTemplate>
                                                Department color
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepartment_Color" runat="server" Text='<%#Eval("Department_Color")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Auto_Refresh">
                                            <HeaderTemplate>
                                                Auto Refresh
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAuto_Refresh" runat="server" Text='<%#Eval("Auto_Refresh")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10px" CssClass="PMSGridItemStyle-css">
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
                                                                CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;INF_LIB_Dash_Board_Snippet&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindDashBoardSnippet" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 35%">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Snippet ID &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 40%">
                                        <asp:TextBox ID="txtSnippetID" CssClass="txtInput" Width="90%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Name &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 40%">
                                        <asp:TextBox ID="txtSnippetName" MaxLength="50" CssClass="txtInput" Width="90%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Function Name &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 40%">
                                        <asp:TextBox ID="txtSnippetFunctionName" CssClass="txtInput" Width="90%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Department&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left" style="width: 40%">
                                        <asp:DropDownList ID="ddlDepartment_Add" Width="92%" runat="server" CssClass="txtInput" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Department Color &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlDepartmentColor" Width="92%" runat="server" CssClass="txtInput">
                                           <%-- <asp:ListItem Text="PartStyle-css-white" Value="PartStyle-css-white" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="PartStyle-css-olive" Value="PartStyle-css-olive"></asp:ListItem>--%>
                                            <asp:ListItem Text="redDiv" Value="redDiv"></asp:ListItem>
                                              <asp:ListItem Text="blackDiv" Value="blackDiv"></asp:ListItem>
                                               <asp:ListItem Text="purpleDiv" Value="purpleDiv"></asp:ListItem>
                                                <asp:ListItem Text="maroonDiv" Value="maroonDiv"></asp:ListItem>
                                                 <asp:ListItem Text="orangeDiv" Value="orangeDiv"></asp:ListItem>
                                                  <asp:ListItem Text="greenDiv" Value="greenDiv"></asp:ListItem>
                                                   <asp:ListItem Text="lightBlueDiv" Value="lightBlueDiv"></asp:ListItem>
                                                    <asp:ListItem Text="yellowDiv" Value="yellowDiv"></asp:ListItem>
                                                     <asp:ListItem Text="darkBlueDiv" Value="darkBlueDiv"></asp:ListItem>
                                                      <asp:ListItem Text="ThemeDiv" Value="ThemeDiv"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="left" colspan="2">
                                        &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkAutoRefresh_Add" Text="Auto Refresh" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="font-size: 11px; text-align: center;background-color:#d8d8d8">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validation();"
                                            OnClick="btnsave_Click" />
                                        <asp:TextBox ID="txtID" runat="server" Visible="false" Width="1px"></asp:TextBox>
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
