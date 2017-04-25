<%@ Page Title="View Wages" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="View_Wages.aspx.cs" Inherits="PortageBill_View_Wages" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
    
    <div id="pageTitle">
        <asp:Label ID="lblPageTitle" runat="server" Text="Crew Details"></asp:Label>
    </div>
    <div id="page-content" style="height: 620px; border: 1px solid gray; z-index: -2;
        overflow: auto; padding: 2px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblstaffdtl" runat="server" Font-Names="Tahoma" Font-Bold="true"
                    ForeColor="Navy" Font-Size="11px"></asp:Label><br />
                <asp:GridView ID="GridViewViewwages" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1"
                    Width="300px" DataKeyNames="id,entrytypecode" OnDataBound="GridViewViewwages_DataBound"
                    OnRowEditing="GridViewViewwages_RowEditing">
                    <Columns>
                        <asp:BoundField DataField="Entry_type" ReadOnly="true" ItemStyle-HorizontalAlign="Left"
                            HeaderText="Entry Type" />
                        <asp:TemplateField HeaderText="Salary Type">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DDLSalaryType" runat="server" EnableViewState="true" DataSourceID="ObjectDataSourcePayableAt"
                                    DataTextField="name" DataValueField="code" Font-Size="Smaller" Width="120px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("salary_type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payable At">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlpayableat" runat="server" DataSourceID="ObjectDataSourcePayableAt"
                                    DataTextField="name" DataValueField="code" Font-Size="Smaller" Width="120px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("PAYABLE_AT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("amount") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("amount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Currency Type">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DDLCurrencyType" runat="server" DataSourceID="ObjectDataSourceCurrencyType"
                                    DataTextField="Currency_Type" DataValueField="Currency_Code" Font-Size="Smaller"
                                    Width="70px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Currency_Type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rule Values">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Rule_Values") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Rule_Values") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rule Type">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlruletype" runat="server" DataSourceID="ObjectDataSourceRuleType"
                                    DataTextField="Rule_Type" Font-Size="Smaller" Width="70px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("Rule_Type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parantcode">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DDLEntryType_Rpce" runat="server" DataSourceID="ObjectDataSourceSalaryType"
                                    DataTextField="name" DataValueField="code" Font-Size="Smaller" Width="90px" AutoPostBack="false">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("parantcode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="Update"></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="Edit"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="RowStyle-css" />
                    <HeaderStyle CssClass="HeaderStyle-css" />
                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" BackColor="White" />
                    <RowStyle CssClass="RowStyle-css" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OnSelecting="ObjectDataSource1_Selecting"
            SelectMethod="SP_ACC_GetCrewWages" TypeName="Eern_Deduction" OnUpdating="ObjectDataSource1_Updating"
            UpdateMethod="Ins_CrewWages" OnUpdated="ObjectDataSource1_Updated">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="Vessel_Code" QueryStringField="vcodeviewwages" />
                <asp:Parameter Name="voyid" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceSalaryType" runat="server" SelectMethod="Get_CrewWages"
            TypeName="Eern_Deduction">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="vcode" QueryStringField="vcodeviewwages" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourcePayableAt" runat="server" SelectMethod="Crew_Salary_Type"
            TypeName="BALPortageBillNameSpace.PortageBillBAL"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceCurrencyType" runat="server" SelectMethod="GC_Company_Currency"
            TypeName="BALPortageBillNameSpace.PortageBillBAL"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceEntryType" runat="server" SelectMethod="Crew_EntryType"
            TypeName="BALPortageBillNameSpace.PortageBillBAL">
            <SelectParameters>
                <asp:SessionParameter Name="indvslcode" SessionField="indvslcode" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceEntryType_deduction" runat="server" SelectMethod="Crew_EntryType_deduction"
            TypeName="BALPortageBillNameSpace.PortageBillBAL">
            <SelectParameters>
                <asp:SessionParameter Name="indvslcode" SessionField="indvslcode" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceRuleType" runat="server" SelectMethod="Rule_Type"
            TypeName="BALPortageBillNameSpace.PortageBillBAL"></asp:ObjectDataSource>
    </div>
</asp:Content>
