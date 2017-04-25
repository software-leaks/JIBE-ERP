<%@ Page Title="Interview Admin" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="InterviewAdmin.aspx.cs" Inherits="Crew_InterviewAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        body, html
        {
            overflow-x: hidden;
        }
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
            $('[id$=txtInterview_Name]').val('');
            $('[id$=ddlRank]').val('0');
            $('[id$=ddlInterviewType]').val('0');
            $('[id$=ddlInterviewNames]').val('0');
            document.getElementById(dv).style.display = "block";
        }
        function closeDiv(dv) {
            document.getElementById(dv).style.display = "None";
        }
        $(document).ready(function () {
            $('.draggable').draggable();
        });       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   
   <div class="page-title">
        Questionnaire Administration
    </div>

    
 <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

    <div id="page-content" style="height: 620px; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto;">
        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
            <ContentTemplate>
                <div style="text-align: center">
                    <table border="0">
                        <tr>
                            <td>
                                Rank
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRank1" runat="server" Width="115px" OnSelectedIndexChanged="ddlRank_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%" border="0">
                        <tr>
                            <td style="text-align: right">
                                <asp:LinkButton ID="lnkAddNewInterview" runat="server" CssClass="linkbtn" OnClientClick="showDiv('dvAddNewInterview');return false;">Create New Questionnaire</asp:LinkButton>
                            </td>
                            <tr>
                                <td style="padding-top: 10px">
                                    <asp:GridView ID="GridView_Interview" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        CaptionAlign="Bottom" CellPadding="4" DataKeyNames="ID,RankID" EmptyDataText="No Record Found"
                                        GridLines="None" OnRowCancelingEdit="GridView_Interview_RowCancelEdit"
                                        OnRowDataBound="GridView_Interview_RowDataBound" OnRowDeleting="GridView_Interview_RowDeleting"
                                        OnRowEditing="GridView_Interview_RowEditing" OnRowUpdating="GridView_Interview_RowUpdating"
                                        OnSorted="GridView_Interview_Sorted" OnSorting="GridView_Interview_Sorting" Width="100%">
                                       <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                       <RowStyle CssClass="RowStyle-css" />
                                       <HeaderStyle CssClass="HeaderStyle-css" Height="35px"/>
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" HeaderStyle-ForeColor="Black" SortExpression="ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID"  runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Questionnaire Name" HeaderStyle-ForeColor="Black"  SortExpression="Interview_Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInterview_Name" runat="server" Text='<%#Eval("Interview_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtInterview_Name" runat="server" Font-Size="11px" MaxLength="1000"
                                                        Width="300px" Text='<%#Bind("Interview_Name")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" SortExpression="InterviewType" HeaderStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInterviewType" runat="server" Text='<%#Eval("InterviewType")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rank" HeaderStyle-ForeColor="Black" SortExpression="Rank_Short_Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRankName" runat="server" Text='<%#Eval("Rank_Short_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of Criteria" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCriteria_Count" runat="server" Text='<%#Eval("Criteria_Count")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Criteria" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCriteria" runat="server"></asp:Label>
                                                    <asp:Button ID="btnSelectCriteria" runat="server" Text="Add/Remove Criteria" CommandArgument='<%#Eval("ID") %>'
                                                        OnClick="btnSelectCriteria_Click" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
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
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                        Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_LIB_Interview&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>'
                                                        AlternateText="info" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#58FA82" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />                                 
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
        <div id="dvAddNewInterview" class="draggable" style="display: none; background-color: #CBE1EF;
            border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
            left: 40%; top: 15%; z-index: 1; color: black">
            <div class="header">
                <div style="right: 0px; position: absolute; cursor: pointer;">
                    <img src="../Images/Close.gif" onclick="closeDiv('dvAddNewInterview');" alt="Close" />
                </div>
                <h4>
                    Add New Questionnaire</h4>
            </div>
            <div class="content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; background-color: White; cursor: default;">
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Questionnaire Name:
                                </td>
                                <td style="border-width: 1px">
                                    <asp:TextBox ID="txtInterview_Name" CssClass="textbox-css" Width="400px" runat="server"
                                        MaxLength="1000"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Rank:
                                </td>
                                <td style="border-width: 1px">
                                    <asp:DropDownList ID="ddlRank" runat="server" DataTextField="Rank_Short_Name" DataValueField="id"
                                        AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Questionnaire Type:
                                </td>
                                <td style="border-width: 1px">
                                   <asp:DropDownList ID="ddlInterviewType" runat="server" Width="154px" CssClass="control-edit">
                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Interview" Value="Interview"></asp:ListItem>
                                        <asp:ListItem Text="Briefing" Value="Briefing"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; font-weight: bold">
                                    Copy From:
                                </td>
                                <td style="border-width: 1px">
                                    <asp:DropDownList ID="ddlInterviewNames" runat="server" DataTextField="Interview_Name"
                                        DataValueField="id" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveInterview" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnSaveInterview_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndCloseInterview" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndCloseInterview_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="Button3" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDiv('dvAddNewInterview')" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
