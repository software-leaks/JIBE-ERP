<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ASLIndexscreen.aspx.cs" Inherits="Technical_INV_ASLIndexscreen" Title="Approved Supplier List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <table align="center" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="font-size: small; color: Black;" align="center">
                    <b style="font-size: small">Approved Suppliers List</b>
                    <br />
                </td>
                <td align="right" style="font-size: small; color: #FFFFFF;">
                    <asp:ImageButton ID="image" runat="server" ImageUrl="~/images/arrowbac.gif" 
                        OnClientClick="Javascript:history.go(-1)" Visible="False" />
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td valign="top" align="left" width="33%">
                            <table width="100%">
                                <tr>
                                    <td valign="top" align="left">
                                        <b>Supplier Code / Name: </b>
                                    </td>
                                    <td valign="top" align="left">
                                        <asp:TextBox ID="txtsuppliercode" runat="server"  
                                            Width="147px" BackColor="White"></asp:TextBox>
                                            <asp:ImageButton ID="imgsuppliercode" runat="server" 
                                            onclick="imgsuppliercode_Click"  ImageUrl="~/images/Search-2-jpg.png" AlternateText="Search" style="height:20px;width:20px;border-width:0px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="left">
                                        <b>Supplier Category: </b>
                                    </td>
                                    <td valign="top" align="left">
                                        <asp:DropDownList ID="ddlsuppliercategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlsuppliercategory_SelectedIndexChanged"
                                            Width="150px" BackColor="White">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td valign="top" align="left">
                                        <b >Supplier Type: </b>
                                    </td>
                                    <td valign="top" align="left">
                                        <asp:DropDownList ID="ddlSupplierType" runat="server" AutoPostBack="True" BackColor="White"
                                            OnSelectedIndexChanged="ddlSupplierType_SelectedIndexChanged" 
                                            Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>--%>
                            </table>
                        </td>
                        <td valign="top" align="left" width="33%">
                            <table width="100%">
                                <tr>
                                    <td valign="top" align="left" style="height: 26px">
                                        <b >Supplier Country: </b>
                                    </td>
                                    <td valign="top" align="left" style="height: 26px">
                                        <asp:DropDownList ID="ddlSupplierCountry" runat="server" AutoPostBack="True" BackColor="White"
                                            OnSelectedIndexChanged="ddlSupplierHQ_SelectedIndexChanged" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="left" style="height: 26px">
                                        <b >Supplier Status:</b>
                                    </td>
                                    <td valign="top" align="left" style="height: 26px">
                                        <asp:DropDownList ID="ddlSupplierStatus" runat="server" AutoPostBack="True" BackColor="White"
                                            OnSelectedIndexChanged="ddlSupplierStatus_SelectedIndexChanged" 
                                            Width="150px">
                                            
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td valign="top" align="left" colspan="2">
                                        <asp:Button ID="btnAddnewsupplier" runat="server" Text="Add New Supplier" onclick="btnAddnewsupplier_Click" 
                                             />
                                        <asp:Button ID="btnclear" runat="server" Text="Clear Filter" OnClick="btnclear_Click" />
                                    </td>
                                </tr>--%>
                            </table>
                        </td>
                        <td valign="top" align="left" width="33%">
                         <%--  <asp:Panel ID ="PnlScope" runat ="server" Visible="true"  >
                            <table width="100%" BORDER="1" RULES="none" FRAME="box">
                                <tr>
                                    <td valign="top" align="left">
                                        <b >Supplier Scope: </b>
                                    </td>
                                    <td valign="top" align="left">
                                        <asp:DropDownList ID="ddlSupplierScope" runat="server" AutoPostBack="True" BackColor="White"
                                            OnSelectedIndexChanged="ddlSupplierScope_SelectedIndexChanged" 
                                            Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="left">
                                        <b>Supply Port: </b>
                                    </td>
                                    <td valign="top" align="left" rowspan="2">
                                        <asp:DropDownList ID="ddlSupplyPort" runat="server" AutoPostBack="True" BackColor="White"
                                            Width="150px" OnSelectedIndexChanged="ddlSupplyPort_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>--%>
                             <asp:Button ID="Button1" runat="server" Text="Add New Supplier" onclick="btnAddnewsupplier_Click" 
                                             />
                                        <asp:Button ID="Button2" runat="server" Text="Clear Filter" OnClick="btnclear_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                           <%-- <div style="overflow-y: auto; overflow-x: hidden; height: 350px;">--%>
                                <asp:GridView ID="grdsupplier" runat="server" AutoGenerateColumns="false" Width="99%"
                                    AllowSorting="true" OnSorting="grdsupplier_Sorting"  
                                AllowPaging="true" PageSize="13"
                                    DataKeyNames="SUPPLIER" 
                                onpageindexchanging="grdsupplier_PageIndexChanging">
                                    <HeaderStyle BackColor="SkyBlue" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Supplier Code" SortExpression="SUPPLIER">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierCodegrd" runat="server" Text='<%#Eval("SUPPLIER") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" SortExpression="SHORT_NAME">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNamegrd" runat="server" Text='<%#Eval("SHORT_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Category" SortExpression="FLDCATEGORYNAME">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierTypegrd" runat="server" Text='<%#Eval("FLDCATEGORYNAME") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country" SortExpression="COUNTRY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountrygrd" runat="server" Text='<%#Eval("COUNTRY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Status" SortExpression="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierStatusgrd" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgedit" runat="server" ImageUrl="~/images/edit.gif" 
                                                    CommandName="EditSupplier" OnCommand="imgedit_Command" CommandArgument='<%#Eval("SUPPLIER") %>'    /> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Detail">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgdetails" runat="server" ImageUrl="~/images/preview.gif" OnCommand="Details" CommandName="OnDetails"  
                                                    onclick="imgdetails_Click" CommandArgument='<%#Eval("SUPPLIER") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            <%--</div>--%>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <b >Supplier Count :</b>
                            <asp:Label ID="lblcount" runat="server" BackColor="Yellow"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
