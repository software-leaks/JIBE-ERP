<%@ Page Title="Evaluation Index" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EvaluationIndex.aspx.cs" Inherits="CrewEvaluation_EvaluationIndex" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <style type="text/css">
        .header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
            
        }
        h4
        {
            font-size: 1.2em;
            color: #ffffff;
            font-weight: bold;
            margin: 0 0 0 5px;
        }
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        .watermarked
        {
            color: #cccccc;
        }
        
        .linkbtn a
        {
            color: Black;
        }
        
        .linkbtn
        {
            color: black;
            background-color: white;
            text-decoration: none;
            border-left: 1px solid #cccccc;
            padding-left: 10px;
            border-top: 1px solid #cccccc;
            padding-top: 5px;
            border-right: 1px solid #cccccc;
            padding-right: 10px;
            border-bottom: 1px solid #cccccc;
            padding-bottom: 3px;
            background-color: #F1F8E0;
        }
        input
        {
            font-family: Tahoma;
            font-size: 12px;
        }
    </style>
    <style id="TooltipStyle" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: Tahoma;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .grid-row
        {
            padding: 6px;
        }
        .grid-col-fixed
        {
            border: 1px solid #cccccc;
        }
        .grid-col
        {
            border: 1px solid #cccccc;
        }
        .gradiant-css-browne
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#81F79F',EndColorStr='#088A4B');
            background: -webkit-gradient(linear, left top, left bottom, from(#81F79F), to(#088A4B));
            background: -moz-linear-gradient(top,  #81F79F,  #088A4B);
            color: Black;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
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
            $('#dvAddNewEvaluation').draggable();
        });       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div id="pageTitle" class="gradiant-css-blue" style="font-size: 12px; border: 1px solid #CEE3F6;
        text-align: center; padding: 3px; font-weight: bold;">
        <asp:Label ID="lblPageTitle" runat="server" Text="Crew Evaluation Question Bank"></asp:Label>
    </div>
    <div id="page-content" style="height: 620px; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto;">
        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
            <ContentTemplate>
                <div style="text-align: center">
                    <table border="0">
                        <tr>
                            <td style="text-align: left">
                                Search:
                            </td>
                            <td>
                                <asp:TextBox ID="txtfilter" runat="server" CssClass="textbox-css" AutoPostBack="true"
                                    OnTextChanged="txtfilter_TextChanged"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                    WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%" border="0">
                        <tr>
                            <td style="text-align: right">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="linkbtn" OnClientClick="showDiv('dvAddNewEvaluation');return false;">Create New Evaluation</asp:LinkButton>
                            </td>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView_Evaluation" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        CaptionAlign="Bottom" CellPadding="4" DataKeyNames="Evaluation_ID" EmptyDataText="No Record Found"
                                        ForeColor="#333333" GridLines="None" OnRowCancelingEdit="GridView_Evaluation_RowCancelEdit"
                                         OnRowDeleting="GridView_Evaluation_RowDeleting"
                                        OnRowEditing="GridView_Evaluation_RowEditing" OnRowUpdating="GridView_Evaluation_RowUpdating"
                                        OnSorted="GridView_Evaluation_Sorted" OnSorting="GridView_Evaluation_Sorting"
                                        Width="100%">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" SortExpression="Evaluation_ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("Evaluation_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Evaluation Name" SortExpression="Evaluation_Name"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEvaluation_Name" runat="server" Text='<%#Eval("Evaluation_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEvaluation_Name" runat="server" Font-Size="11px" MaxLength="1000"
                                                        Width="300px" Text='<%#Bind("Evaluation_Name")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="No of Criteria" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCriteria_Count" runat="server" Text='<%#Eval("Criteria_Count")%>'></asp:Label>
                                                </ItemTemplate>                                               
                                                <ItemStyle HorizontalAlign="Center"/>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Criteria" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCriteria" runat="server"></asp:Label>
                                                    <asp:Button ID="btnSelectCriteria" runat="server" Text="Add/Remove Criteria" CommandArgument='<%#Eval("Evaluation_ID") %>'
                                                        OnClick="btnSelectCriteria_Click" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Created By" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreated_By" runat="server" Text='<%#Eval("Created_By_Name")%>'></asp:Label>
                                                </ItemTemplate>                                               
                                                <ItemStyle HorizontalAlign="Center"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date Created" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate_Of_Creation" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation"))) %>'></asp:Label>
                                                </ItemTemplate>                                               
                                                <ItemStyle HorizontalAlign="Center"/>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="50px">
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnAccept" runat="server" AlternateText="Update" CausesValidation="False"
                                                        CommandName="Update" ImageUrl="~/images/accept.png" />
                                                    <asp:ImageButton ID="ImgBtnCancel" runat="server" AlternateText="Cancel" CausesValidation="False"
                                                        CommandName="Cancel" ImageUrl="~/images/reject.png" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton2" runat="server" AlternateText="Edit" CausesValidation="False"
                                                        CommandName="Edit" ImageUrl="~/images/edit.gif" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1del" runat="server" AlternateText="Delete" CausesValidation="False"
                                                        CommandName="Delete" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to  delete ?')" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#58FA82" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="dvAddNewEvaluation" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
            border-style: solid; border-width: 1px; position: absolute; left: 40%; top: 15%;
            z-index: 1; color: black">
            <div class="header">
                <div style="right: 0px; position: absolute; cursor: pointer;">
                    <img src="../Images/Close.gif" onclick="closeDiv('dvAddNewEvaluation');" alt="Close" />
                </div>
                <h4>
                    Add New Evaluation</h4>
            </div>
            <div class="content">
                <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; background-color: White; cursor: default;">
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Evaluation Name:
                                </td>
                                <td style="border-width: 1px">
                                    <asp:TextBox ID="txtEvaluation_Name" CssClass="textbox-css" Width="400px" runat="server"
                                        MaxLength="1000"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveAndAdd" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnsave_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndClose" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndClose_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDiv('dvAddNewEvaluation')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>

