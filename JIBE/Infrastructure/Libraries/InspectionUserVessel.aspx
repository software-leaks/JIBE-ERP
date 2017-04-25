<%@ Page Title="Inspection" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="InspectionUserVessel.aspx.cs" Inherits="Infrastructure_InspectionUserVessel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">
        <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="upUpdateProgress" runat="server">
                    <ProgressTemplate>
                        <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                            color: black">
                            <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="page-title" style="margin: 2px; border: 1px solid #cccccc; height: 20px;
            vertical-align: bottom; background: url(../../Images/bg.png) left -10px repeat-x;
            color: Black; text-align: left; padding: 2px; background-color: #F6CEE3;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 33%;">
                    </td>
                    <td style="width: 33%; text-align: center; font-weight: bold;">
                        <asp:Label ID="lblPageTitle" runat="server" Text="User Vessel Assignment"></asp:Label>
                    </td>
                    <td style="width: 33%; text-align: right;">
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <div id="Div1" style="margin: -3px 2px 2px 2px; border: 1px solid #cccccc; padding: 5px;">
                    <table border="0" cellpadding="0" cellspacing="5" width="100%">
                        <tr>
                            <td style="width: 20%;" class="gradiant-css-orange">
                            </td>
                            <td style="width: 20%;" class="gradiant-css-orange">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Search User:
                                <asp:TextBox ID="txtSearchUser" runat="server" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnAssign" runat="server" Text="Save vessel assignment" OnClick="btnAssign_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:ListBox ID="lstUserList" runat="server" Height="600px" Width="300px" DataSourceID="ObjectDataSource_Users"
                                    AutoPostBack="true" DataTextField="USERNAME" DataValueField="USERID" OnSelectedIndexChanged="lstUserList_SelectedIndexChanged">
                                </asp:ListBox>
                                <asp:ObjectDataSource ID="ObjectDataSource_Users" runat="server" SelectMethod="Get_UserList"
                                    TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="CompanyID" SessionField="USERCOMPANYID" Type="Int32" />
                                        <asp:ControlParameter ControlID="txtSearchUser" DefaultValue="" Name="FilterText"
                                            PropertyName="Text" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                            <td style="vertical-align: top;">
                            <table>
                            <tr>
                            <td style="vertical-align:top">Fleet:&nbsp;&nbsp;<asp:DropDownList ID="ddlFleet" 
                                    runat="server" UseInHeader="false"  
											AutoPostBack="true" Width="130" Style="margin-left: 0px" 
                                    onselectedindexchanged="ddlFleet_SelectedIndexChanged" /></td>
                            </tr>
                            <tr>
                           <td> <asp:CheckBox ID="chkSelectAll" runat="server" 
                                   oncheckedchanged="chkSelectAll_CheckedChanged" AutoPostBack="True" />SELECT ALL</td> 
                            </tr>
                             <tr>
                            <td> <%-- <asp:ListBox ID="lstVessel" runat="server" Height="600px" Width="200px" SelectionMode="Multiple"
                                    DataTextField="Vessel_Name" DataValueField="Vessel_ID" AutoPostBack="false">
                                </asp:ListBox>--%>
                               <div style="overflow-y: scroll; WIDTH:220px; HEIGHT:600px; border:1px solid gray; ">
                               
                                <asp:CheckBoxList ID="lstVessel" runat="server" Width="200px" SelectionMode="Multiple"
                                    DataTextField="Vessel_Name" DataValueField="Vessel_ID" AutoPostBack="false" 
                                       BorderColor="Gray" BorderWidth="0px" BorderStyle="None" 
                                      >
                                </asp:CheckBoxList>
                                </div>
                                
                                </td>
                            </tr>
                            </table>
                         
                              
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
