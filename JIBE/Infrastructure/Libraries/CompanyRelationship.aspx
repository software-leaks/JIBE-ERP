<%@ Page Title="Company Relationship" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CompanyRelationship.aspx.cs" Inherits="Infrastructure_Libraries_CompanyRelationship" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

 <script language="javascript" type="text/javascript">



</script>

    <style type="text/css">
    .lstCompany
    {
        border:0px;
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 
  <div class="page-title">
       Company Relationship
    </div>
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
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div style="color:Black">
        <table style="border:1px solid gray;width:100%">

        <tr>
                <td align="left" style="font-size:12px;font-weight:bold;height:45px" colspan="2">
                    Company Relation Type &nbsp;&nbsp;

                    <asp:DropDownList ID="ddCompRelationshipType" Width="280" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddCompRelationshipType_SelectedIndexChanged" ></asp:DropDownList>
                </td>
              
            </tr>
            <tr class="gradiant-css-orange">
                <td align="left" style="font-size:13px;font-weight:bold" class="gradiant-css-orange">
                    Parent Company
                </td>
                <td align="left" style="font-size:13px;font-weight:bold"  class="gradiant-css-orange">
                    Child Company
                </td>
           <%--     <td align="left" style="font-size:14px;font-weight:bold"  class="gradiant-css-orange">
                    Relationship - 2 : Vessel Manager
                </td>--%>
            </tr>
            <tr>
                <td align="left" style="border:1px solid gray;vertical-align:top;">
                    <asp:ListBox ID="lstCompany" runat="server" OnSelectedIndexChanged="lstCompany_SelectedIndexChanged" Height="400px"  CssClass="lstCompany"
                        AutoPostBack="true">
                    </asp:ListBox>
                </td>
                <td align="left" style="border:1px solid gray;vertical-align:top;">
                    <asp:CheckBoxList ID="lstCompany1" runat="server"  OnSelectedIndexChanged="lstCompany1_SelectedIndexChanged" 
                        AutoPostBack="false">
                        
                    </asp:CheckBoxList>
                </td>
              <%--  <td align="left" style="border:1px solid gray;vertical-align:top;">
                    <asp:CheckBoxList ID="lstCompany2" runat="server"  OnSelectedIndexChanged="lstCompany2_SelectedIndexChanged" 
                        AutoPostBack="true">
                    </asp:CheckBoxList>
                </td>--%>
            </tr>

            <tr>
            
            <td colspan="2" align="center">
            
            <asp:Button ID="btnSave" Text ="Save" runat="server" onclick="btnSave_Click" Width="121px" />

            </td>
            </tr>
        </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
