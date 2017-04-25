<%@ Page Title="NA Survey List" Language="C#" MasterPageFile="~/Surveys/SurveyMaster.master"
    AutoEventWireup="true" CodeFile="NASurveyList.aspx.cs" Inherits="Surveys_MakeAsNA" %>

<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <style type="text/css">
        .tdpager {
    padding: 2px 0px 2px 0px;
    text-align: left;
    border: 1px solid #A9D0F5;
}
        body
        {
            color: black;
            font-family: Tahoma;
            font-size: 11px;
        }
        select
        {
            font-family: Tahoma;
            font-size: 11px;            
        }
        input
        {
            font-family: Tahoma;
            font-size: 11px;            
        }
        
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        #page-content a:link
        {
            color: Black;
            background-color: transparent;
            text-decoration: none;
            border: 0px;
        }
        #page-content a:visited
        {
            color: Black;
            text-decoration: none;
        }
        #page-content a:hover
        {
            color: Black;
            text-decoration: none;
        }
        
        .pager {
    text-align: center;
    text-decoration: none;
    font-size: 12px;
    position: relative;
    font-family: Tahoma;
} 
        
        .pager span
        {
          background-color: #99ccff;
  border: 1px solid #58ACFA;
  padding: 2px 5px 2px 5px;
        }
        .pager a
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:visited
        {
            color: Black;
            background-color: white;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:hover
        {
            color: Black;
            background-color: #efefef;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .taskpane
        {
            background-image: url(../images/taskpane.png);
            background-repeat: no-repeat;
            background-position: -2px -2px;
        }
        .Legend{color:Black;font-weight:bold;text-align:left;;padding-bottom:2px;}
       
        .Overdue{background-color:Red;color:Yellow;}
        .Due0-30{background-color:Orange; color:Black;}
        .Due30-90{background-color:Yellow; color:Black;}
        .Done30{background-color:Green;color:White;}
        
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="AccessMsgDiv" runat="server" style="color: Red; font-size: 14px; text-align: center;">
        You don't have sufficient privilege to access the requested page.</div>
    <div id="MainDiv" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="upUpdateProgress" runat="server">
                    <ProgressTemplate>
                        <div id="divProgress" style="background-color: transparent; position: absolute; left: 49%;
                            top: 20px; z-index: 2; color: black">
                            <img src="../images/loaderbar.gif" alt="Please Wait" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div id="page-content" style="overflow: auto; border-top: 0px">
                    <div style="text-align: right">
                        <asp:RadioButtonList ID="rdoVerified" runat="server" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="rdoVerified_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Verified" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Not Verified" Value="0" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <table width="100%">
                        <tr>
                            <td width="100%">
                                <div style="width: 100%; overflow: auto; text-align: left;">
                                    <asp:GridView ID="grdSurveylist" runat="server" AutoGenerateColumns="false" BorderStyle="Solid"
                                        EnableModelValidation="True" AllowSorting="true" Width="100%" GridLines="None"
                                        AllowPaging="true" OnSorting="grdSurveylist_Sorting" OnRowCommand="grdSurveylist_RowCommand"
                                        CssClass="gridmain-css" OnPageIndexChanging="grdSurveylist_PageIndexChanging">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                        <PagerStyle CssClass="Gridpager" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="100px" HeaderText="Vessel" SortExpression="Vessel_Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-Width="200px" HeaderText="Survey/Certificate Category" SortExpression="Survey_Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSurvey_Category" runat="server" Text='<%#Eval("Survey_Category") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Survey/Certificate Name" SortExpression="Survey_Cert_Name"
                                                ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSurvey_Cert_Name" runat="server" Text='<%#Eval("Survey_Cert_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="N/A Marked By" SortExpression="NA_Marked_By_Name"
                                                ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNA_Marked_By" runat="server" Text='<%#Eval("NA_Marked_By_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="100px" HeaderText="Mark as Active">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnMarkAsActive" runat="server" Text="Mark as Active" BorderStyle="Solid"
                                                        BorderColor="#cccccc" OnClientClick="return confirm('Are you sure, you want to mark the survey as ACTIVE?')"
                                                        BorderWidth="1px" Font-Names="Tahoma" Height="21px" CommandName="MarkasActive"
                                                        CommandArgument='<%#Eval("Vessel_ID").ToString() + "," + Eval("Surv_Vessel_ID").ToString()%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Verify" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnVerify" runat="server" Text="Verify" BorderStyle="Solid" BorderColor="#cccccc"
                                                        OnClientClick="return confirm('Are you sure, you want to mark the survey as VERIFIED?')"
                                                        BorderWidth="1px" Font-Names="Tahoma" Height="21px" CommandName="VERIFY" CommandArgument='<%#Eval("Vessel_ID").ToString() + "," + Eval("Surv_Vessel_ID").ToString()%>'
                                                        Visible='<%#Eval("Verified").ToString()=="0"?true:false %>' />
                                                    <asp:Button ID="btnUnVerify" runat="server" Text="Undo-Verify" BorderStyle="Solid"
                                                        BorderColor="#cccccc" OnClientClick="return confirm('Are you sure, you want to mark the survey as NOT-VERIFIED?')"
                                                        BorderWidth="1px" Font-Names="Tahoma" Height="21px" CommandName="UNDO_VERIFY"
                                                        CommandArgument='<%#Eval("Vessel_ID").ToString() + "," + Eval("Surv_Vessel_ID").ToString()%>'
                                                        Visible='<%#Eval("Verified").ToString()=="1"?true:false %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Verified/Undo By" SortExpression="Verified_By_Name"
                                                ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVerified_By_Name" runat="server" Text='<%#Eval("Verified_By_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date Verified/Undo" SortExpression="Verified_Date"
                                                ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVerified_Date" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Verified_Date"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <label id="Label1" runat="server">
                                                No survey found !!</label>
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#aabbdd" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="20" OnBindDataItem="SortGridView" />
                                    <table width="100%" id="tblPager">
                                        <tr>
                                            <td class="tdpager" style="background-color: #F6CEE3; background: url(/JIBE/Images/bg.png) left -20px repeat-x;
                                                color: Black; text-align: left;">
                                                <table id="tbl_paging" class="pager" style="float: left;">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <span>1</span>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <asp:DropDownList ID="ddlPageSize" runat="server" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                                                    AutoPostBack="true" Style="font-size: 12px;">
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="20" Value="20" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                </asp:DropDownList>
                                                [&nbsp;Total Pages&nbsp;:&nbsp;
                                                <asp:Label ID="lblPageStatus" runat="server" Text="0"></asp:Label>&nbsp;&nbsp; Records
                                                found&nbsp;:&nbsp;<asp:Label ID="lblRecordCount" runat="server" Text="0"></asp:Label>
                                                &nbsp;]
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function Pageing() {
            $("#tbl_paging").html($(".Gridpager table").html());
            $(".Gridpager").remove();
        }

    </script>
</asp:Content>
