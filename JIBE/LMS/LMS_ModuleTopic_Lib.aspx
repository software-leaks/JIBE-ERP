<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LMS_ModuleTopic_Lib.aspx.cs" Inherits="LMS_LMS_ModuleTopic_Lib" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
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
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        //        function Divaddnewlink() {
        //            document.getElementById("divadd").style.display = "block";
        //        }
        //        function hidePopup() {
        //            document.getElementById("divadd").style.display = "none";
        //        }
        //        $(document).ready(function () {
        //            $(".draggable").draggable();
        //        });
        //        function DivShow(mode) {
        //            if (mode == 'Topic') {
        //                document.getElementById("dvTopic").style.display = "block";
        //                document.getElementById("dvModule").style.display = "none";
        //            } else {
        //                document.getElementById("dvModule").style.display = "block";
        //                document.getElementById("dvTopic").style.display = "none";
        //            }
        //        }
        function validation() {
            if (document.getElementById("ctl00_MainContent_hdnMode").value == "Topic") {
                if (document.getElementById("ctl00_MainContent_txtName").value == "") {
                    alert("Please enter Topic name.");
                    document.getElementById("ctl00_MainContent_txtName").focus();
                    return false;
                }

                if (document.getElementById("ctl00_MainContent_ddlModule").value == "0") {
                    alert("Please select Module.");
                    document.getElementById("ctl00_MainContent_ddlModule").focus();
                    return false;
                }
            } else {
                if (document.getElementById("ctl00_MainContent_txtName").value == "") {
                    alert("Please enter Module name.");
                    document.getElementById("ctl00_MainContent_txtName").focus();
                    return false;
                }
            }
            return true;
        }    
    </script>
    <style type="text/css">
        .style1
        {
            width: 17%;
        }
        .style2
        {
            width: 5%;
        }
        .bckColor
        {
            margin-left: 48px;
        }
    </style>
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
                Add/Edit Module And Topic
            </div>
            <div style="width: 100%; height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePaneCompany" runat="server">
                    <ContentTemplate>
                        <%--<div style="padding-top: 5px; padding-bottom: 5px; height: 70px; width: 100%">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Name :&nbsp;
                                    </td>
                                    <td align="left" class="style1">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="80%"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="right" class="style2">
                                        <asp:Label ID="lblfltModule" Text="Module :" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 15%">
                                        <asp:DropDownList ID="ddlfltModule" AppendDataBoundItems="true" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        <asp:RadioButtonList ID="rdoStatus" runat="server" CssClass="bckColor" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rdoStatus_SelectedIndexChanged" AutoPostBack="true" BorderColor="Gray"
                                            BorderStyle="Solid" BorderWidth="1px">
                                            <asp:ListItem id="rdoBtnModule" runat="server" Value="2" Text="Module" />
                                            <asp:ListItem id="rdoBtnTopic" runat="server" Value="1" Text="Topic" Selected="True" />
                                        </asp:RadioButtonList>
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
                                            ToolTip="Add New" />
                                    </td>
                                    <td style="width: 30px">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>--%>
                        <div id="dvTopic" style="margin-left: auto; margin-right: auto; text-align: center;">
                            <table width="100%">
                                <tr>
                                    <td style="padding-left: 13%; font-weight: bold;">
                                        <u>Module List</u>
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="ImgAdd" runat="server" ImageUrl="~/Images/Add-icon.png" OnClick="btnAddModule_Click"
                                            ToolTip="Add New Module" />
                                    </td>
                                    <td>
                                    </td>
                                    <td style="padding-left: 16%; font-weight: bold;">
                                        <u>Topic List</u>
                                    </td>
                                    <td align="right">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Add-icon.png"
                                            OnClick="btnAddTopic_Click" ToolTip="Add New Topic" />
                                        <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Refresh-icon.png"
                                            OnClick="btnRefresh_Click" ToolTip="Refresh Topic" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="top" style="width: 45%">
                                        <div style="width: 100%">
                                            <asp:GridView ID="grdModule" runat="server" CellPadding="1" EmptyDataText="NO RECORDS FOUND!"
                                                AutoGenerateColumns="False" Width="100%" GridLines="Both" DataKeyNames="Module_ID">
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                                <SelectedRowStyle BackColor="#FFFFCC" />
                                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Module_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Module Name">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblName" Style="color: Black" runat="server" Text='<%#Eval("Module_Description")%>'
                                                                CommandArgument='<%#Eval("Module_ID")%>' OnCommand="onUpdateModule"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="140px" CssClass="PMSGridItemStyle-css">
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
                                                                        <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdateModule"
                                                                            Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Module_ID]")%>' ForeColor="Black"
                                                                            ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDeleteModule"
                                                                            Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                            CommandArgument='<%#Eval("[Module_ID]")%>' ForeColor="Black" ToolTip="Delete"
                                                                            ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                            Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;LMS_DTL_FAQModule&#39;,&#39;Module_ID="+Eval("Module_ID").ToString()+"&#39;,event,this)" %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lblModuleTopic" Style="color: Blue" runat="server" Text="View Topics"
                                                                            CommandArgument='<%#Eval("Module_ID")%>' OnCommand="onFilter"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="30" OnBindDataItem="BindModuleGrid" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                                        </div>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2" valign="top" style="width: 55%">
                                        <div style="width: 100%; vertical-align: top">
                                            <asp:GridView ID="grdTopic" runat="server" CellPadding="1" EmptyDataText="NO RECORDS FOUND!"
                                                AutoGenerateColumns="False" Width="100%" GridLines="Both" DataKeyNames="Topic_ID"
                                                ShowHeaderWhenEmpty="true">
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                                <SelectedRowStyle BackColor="#FFFFCC" />
                                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTopicID" runat="server" Text='<%#Eval("Topic_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Module Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblModuleName" runat="server" Text='<%# Bind("Module_Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="130px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Topic Name">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblTopicName" Style="color: Black" runat="server" Text='<%#Eval("Description")%>'
                                                                CommandArgument='<%#Eval("Topic_ID")%>' OnCommand="onUpdateTopic"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
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
                                                                        <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdateTopic"
                                                                            Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Topic_ID]")%>' ForeColor="Black"
                                                                            ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDeleteTopic"
                                                                            Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                            CommandArgument='<%#Eval("[Topic_ID]")%>' ForeColor="Black" ToolTip="Delete"
                                                                            ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                            Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;LMS_DTL_FAQTopic&#39;,&#39;Topic_ID="+Eval("Topic_ID").ToString()+"&#39;,event,this)" %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindTopicGrid" />
                                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                            <asp:HiddenField ID="hdnModuleID" runat="server" EnableViewState="False" />
                                        </div>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td>
                                        <asp:Button ID="btnAddModule" runat="server" Text="Add Module" OnClick="btnAddModule_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAddTopic" runat="server" Text="Add Topic" OnClick="btnAddTopic_Click" />
                                    </td>
                                </tr>--%>
                            </table>
                            <br />
                        </div>
                        <%--<div id="dvModule" style="margin-left: auto; margin-right: auto; text-align: center;">
                            
                            <br />
                        </div>--%>
                        <div id="divadd" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 20%;">
                            <center>
                                <table width="98%" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <asp:Label ID="lblName" runat="server"></asp:Label>
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="right" style="width: 25%">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="txtInput" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <asp:Label ID="lblModule" Text="Module :" runat="server"></asp:Label>
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            <asp:Label ID="td_Relation" Text="*" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 25%">
                                            <asp:DropDownList ID="ddlModule" runat="server" Width="102%" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="font-size: 11px; text-align: center; border-style: solid;
                                            padding: 5px 0px 5px 0px; border-color: Silver; border-width: 1px; background-color: #d8d8d8;">
                                            <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return  validation();"
                                                OnClick="btnsave_Click" />
                                            <asp:TextBox ID="txtID" runat="server" Visible="false" Width="1%"></asp:TextBox>
                                            <asp:HiddenField ID="hdnMode" runat="server" />
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td colspan="6">
                                            <div id="dvRecordInformation" style="text-align: left; width: 100%; border: 0px solid #cccccc;
                                                background-color: #FDFDFD">
                                            </div>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                            * Mandatory fields
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                    </ContentTemplate>
                    <%--<Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>--%>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
