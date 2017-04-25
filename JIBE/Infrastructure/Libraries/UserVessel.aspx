<%@ Page Title="Vessel Assignment" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="UserVessel.aspx.cs" Inherits="Infrastructure_Libraries_UserVessel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
            padding: 2px;
            font-weight: bold;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
            padding: 2px;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        //Stop Form Submission of Enter Key Press
        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopRKey;
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
       User Vessel Assignment
    </div>
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">
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
      
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <div id="Div1" style="margin: -3px 2px 2px 2px; border: 1px solid #cccccc; padding: 5px;">
                    <table border="0" cellpadding="0" cellspacing="5" width="100%">
                        <tr>
                            <td style="width: 20%;" class="gradiant-css-orange">
                                Select User
                            </td>
                            <td style="width: 20%;" class="gradiant-css-orange">
                                Select Vessel
                            </td>
                            <td style="width: 80px;">
                            </td>
                            <td class="gradiant-css-orange">
                                Assignments
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Search:
                                <asp:TextBox ID="txtSearchUser" runat="server" AutoPostBack="true" 
                                    ontextchanged="txtSearchUser_TextChanged" onkeydown = "return (event.keyCode!=13);" ></asp:TextBox>
                            </td>
                            <td>
                                Fleet:<asp:DropDownList ID="ddlFleet" runat="server" Width="156px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 80px;">
                            </td>
                            <td>
                            Vessel:<asp:DropDownList ID="ddlVessel" runat="server" Width="156px" AutoPostBack="true">
                                </asp:DropDownList>&nbsp;&nbsp;User Name:<asp:TextBox ID="txtUserName" 
                                    runat="server" AutoPostBack="true"  onkeydown = "return (event.keyCode!=13);"
                                    ontextchanged="txtUserName_TextChanged" ></asp:TextBox>
                                <%--<asp:Button ID="btnClearFilter" runat="server" Text="Clear Filter" OnClick="btnClearFilter_Click"/>--%>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:ListBox ID="lstUserList" runat="server" Height="300px" Width="300px" DataSourceID="ObjectDataSource_Users"
                                    DataTextField="USERNAME" DataValueField="USERID"></asp:ListBox>
                                <asp:ObjectDataSource ID="ObjectDataSource_Users" runat="server" SelectMethod="Get_UserList"
                                    TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                                    <SelectParameters>
                                        <asp:SessionParameter DefaultValue="1" Name="CompanyID" SessionField="UserCompanyID"  Type="Int32" />
                                        <asp:ControlParameter ControlID="txtSearchUser" DefaultValue="" Name="FilterText"
                                            PropertyName="Text" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                            <td style="vertical-align: top;">
                                <asp:ListBox ID="lstVessel" runat="server" Height="300px" Width="200px" SelectionMode="Multiple"
                                    DataTextField="Vessel_Name" DataValueField="Vessel_ID" AutoPostBack="false">
                                </asp:ListBox>
                            </td>
                            <td style="width: 80px;vertical-align:top;">
                                <asp:Button ID="btnAssign" runat="server" Text="Assign" OnClick="btnAssign_Click"    />
                            </td>
                            <td style="vertical-align: top;">
                                <div style="height:600px;overflow:auto;">
                                <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" Width="100%"
                                    AutoGenerateColumns="False" DataKeyNames="ID" AllowSorting="True"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                    GridLines="Horizontal" CssClass="gridmain-css" >
                                  <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                 <RowStyle CssClass="RowStyle-css" />
                                 <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vessel" HeaderStyle-ForeColor="Black"  SortExpression="vessel_short_name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVessel" runat="server" Text='<%#Eval("vessel_short_name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name" HeaderStyle-ForeColor="Black" SortExpression="User_Name" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUser_Name" runat="server" Text='<%#Eval("User_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department" HeaderStyle-ForeColor="Black" SortExpression="Department" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                                                            CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                                                            AlternateText="Delete"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                    </Columns>
                                   <%--  <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                   <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />--%>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get_User_Vessel_Assignment"
                                    TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials" DeleteMethod="DELETE_User_Vessel_Assignment">
                                    <DeleteParameters>
                                        <asp:Parameter Name="ID" Type="Int32" />
                                        <asp:SessionParameter Name="Deleted_By" SessionField="UserID" Type="Int32" />
                                    </DeleteParameters>
                                    <SelectParameters>
                                        <asp:ControlParameter Name="Vessel_ID" Type="Int32" ControlID="ddlVessel" PropertyName="SelectedValue" />
                                        <asp:ControlParameter Name="User_Name" Type="String" ControlID="txtUserName" PropertyName="Text" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
