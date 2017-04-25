<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_BankAcc.aspx.cs"
    Inherits="Crew_CrewDetails_BankAcc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="HiddenField_CrewID" runat="server" />
    <asp:HiddenField ID="HiddenField_AccID" runat="server" />
    <div id="dvCrewBankAccGrid">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlViewAccounts" runat="server" Visible="false">
            <div style="margin:5px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table class="dataTable" id="tblDisplayMode" runat="server">
                            <tr>
                                <td>
                                    Account used for Allotment:&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label Width="250px" ID="lblAllotment" runat="server" class="data"></asp:Label>
                                </td>
                                <td>
                                    <asp:ImageButton ID="lnkEditAllotment" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif"
                                        CausesValidation="False" AlternateText="Edit"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                        <table class="dataTable" width="100%" id="tblEditMode" runat="server" visible="false">
                            <tr>
                                <td width="42%">
                                    Account used for Allotment :
                                </td>
                                <td style="color: Black;">
                                    <asp:RadioButtonList ID="rdoOptAllotment" runat="server" RepeatDirection="Vertical">
                                        <asp:ListItem id="rdoMO" runat="server" Value="MO" Text="Use only MO Bank Account" />
                                        <asp:ListItem id="rdoCRW" runat="server" Value="CRW" Text="Use only Crew Bank Account" />
                                        <asp:ListItem id="rdoBoth" runat="server" Value="BOTH" Text="Use Any Bank Account"
                                            Selected="True" />
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnSaveAllotment" runat="server" Text="Save" OnClick="btnSaveAllotment_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:GridView ID="GridView_BankAccounts" runat="server" AllowSorting="False" AutoGenerateColumns="false"
                GridLines="None" CellPadding="3" CellSpacing="1" Width="100%" CssClass="GridView-css">
                <Columns>
                    <asp:BoundField DataField="beneficiary" HeaderText="Account Name(Beneficiary)" ItemStyle-Width="150px" />
                    <asp:BoundField DataField="Acc_NO" HeaderText="Account No" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name" ItemStyle-Width="100px" />
                    <asp:TemplateField HeaderText="Bank Address">
                        <ItemTemplate>
                            <asp:Label ID="lblBank_Address" Text='<%#Eval("Bank_Address") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SwiftCode" HeaderText="SWIFT Code" ItemStyle-Width="150px" />
                    <asp:BoundField DataField="BANK_CODE" HeaderText="Bank Code" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="BRANCH_CODE" HeaderText="Branch Code" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="CURRENCY_CODE" HeaderText="Currency" ItemStyle-Width="80px" />
                    <asp:CheckBoxField DataField="MOAccount" HeaderText="MO Bank Account" Text="" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="70px" ReadOnly="true" />
                    <asp:CheckBoxField DataField="Default_Acc" HeaderText="Default" Text="" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="70px" />
                    <asp:CheckBoxField DataField="Verified" HeaderText="Verified" Text="" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="70px" ReadOnly="true" />
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif"
                                CausesValidation="False" AlternateText="Edit" OnClientClick='<%#"EditCrewBankAcc(" + Eval("ID").ToString()+ "," + Eval("CrewID").ToString() + "," + Session["UserID"].ToString() +"); return false;" %>'>
                            </asp:ImageButton>
                        </ItemTemplate>
                        <ItemStyle Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
                                CausesValidation="False" OnClientClick='<%#"DeleteCrewBankAcc(" + Eval("ID").ToString()+ "," + Eval("CrewID").ToString() + "," + Session["UserID"].ToString() +"); return false;" %>'
                                AlternateText="Delete"></asp:ImageButton>
                        </ItemTemplate>
                        <ItemStyle Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="15px">
                        <ItemTemplate>
                            <asp:Image ID="imgRecordInfo" ToolTip="Info" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_DTL_BANKACCOUNTS&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>'
                                AlternateText="info" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="HeaderStyle-css" />
                <PagerStyle CssClass="PagerStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="pnlEditAccount" runat="server" Visible="false">
            <asp:DetailsView ID="DetailsView_NewAcc" Width="80%" CssClass="dataTable" runat="server"
                AutoGenerateRows="False" DataKeyNames="id" CellPadding="4" GridLines="None" Font-Names="Tahoma"
                Font-Size="12px" BackColor="White" OnItemUpdating="DetailsView_NewAcc_ItemUpdating"
                OnItemInserting="DetailsView_NewAcc_ItemInserting" OnItemInserted="DetailsView_NewAcc_ItemInserted">
                <Fields>
                    <asp:TemplateField HeaderText="Use MO Bank Account:">
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkMOAcc" runat="server" Checked='<%# Bind("MOAccSelected") %>' />
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:CheckBox ID="chkMOAcc" runat="server" Checked='<%# Bind("MOAccSelected") %>'
                                AutoPostBack="true" OnCheckedChanged="chkMOAcc_CheckedChanged" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO Bank Account:">
                        <EditItemTemplate>
                            <asp:ObjectDataSource ID="ObjectDataSource_MOAccount" runat="server" SelectMethod="Get_MOBankAccount"
                                TypeName="SMS.Business.Crew.BLL_Crew_Admin">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="CrewID" QueryStringField="CrewID" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:DropDownList ID="ddlMO_Account" EnableViewState="true" runat="server" Width="306px"  Text='<%# Bind("MOBank_ID") %>'	
                                DataSourceID="ObjectDataSource_MOAccount" DataTextField="MO_Account" AppendDataBoundItems="true"
                                DataValueField="ID">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                         
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:ObjectDataSource ID="ObjectDataSource_MOAccount" runat="server" SelectMethod="Get_MOBankAccount"
                                TypeName="SMS.Business.Crew.BLL_Crew_Admin">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="CrewID" QueryStringField="CrewID" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:DropDownList ID="ddlMO_Account" EnableViewState="true" runat="server" Text='<%# Bind("MOBank_ID") %>'
                                Width="306px" DataSourceID="ObjectDataSource_MOAccount" DataTextField="MO_Account"
                                AppendDataBoundItems="true" DataValueField="ID" Enabled="false" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlMO_Account_SelectedIndexChanged">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                      
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Account Number:">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtacc" runat="server" Text='<%# Bind("Acc_NO") %>' Width="300px"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtacc" EnableViewState="false" runat="server" Text='<%# Bind("Acc_NO") %>'
                                Width="300px"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank Name:">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtbname" runat="server" Text='<%# Bind("Bank_Name") %>' Width="300px"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtbname" EnableViewState="false" runat="server" Text='<%# Bind("Bank_Name") %>'
                                Width="300px"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SWIFT Code:">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSwiftCode" runat="server" Text='<%# Bind("SwiftCode") %>' Width="300px"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtSwiftCode" EnableViewState="false" runat="server" Text='<%# Bind("SwiftCode") %>'
                                Width="300px"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank Address:">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtbadd" runat="server" Text='<%# Bind("Bank_Address") %>' Width="300px"
                                TextMode="MultiLine" Height="80px"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtbadd" EnableViewState="false" runat="server" TextMode="MultiLine"
                                Text='<%# Bind("Bank_Address") %>' Width="300px" Height="80px"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Account Name(Beneficiary)">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtBeneficiary" runat="server" Text='<%# Bind("Beneficiary") %>'
                                Width="300px" TextMode="MultiLine" Height="80px"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtBeneficiary" EnableViewState="false" runat="server" Text='<%# Bind("Beneficiary") %>'
                                Width="300px" TextMode="MultiLine" Height="80px"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Verified:">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("Verified") %>' Enabled="false" />
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("Verified") %>' Enabled="false" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Make Default">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Default_Acc") %>' />
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Default_Acc") %>' />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-ForeColor="Black">
                        <ItemTemplate>
                            These three fields are required for bank accounts in Singapore
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank Code">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtBank_Code" EnableViewState="false" runat="server" Text='<%# Bind("BANK_CODE") %>'
                                Width="300px"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtBank_Code" EnableViewState="false" runat="server" Text='<%# Bind("BANK_CODE") %>'
                                Width="300px"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branch Code">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtBRANCH_CODE" EnableViewState="false" runat="server" Text='<%# Bind("BRANCH_CODE") %>'
                                Width="300px"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="txtBRANCH_CODE" EnableViewState="false" runat="server" Text='<%# Bind("BRANCH_CODE") %>'
                                Width="300px"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Currency">
                        <EditItemTemplate>
                            <asp:ObjectDataSource ID="ObjectDataSource_Curr" runat="server" SelectMethod="Get_CurrencyList"
                                TypeName="SMS.Business.Infrastructure.BLL_Infra_Currency"></asp:ObjectDataSource>
                            <asp:DropDownList ID="ddlACCOUNT_CURR" EnableViewState="true" runat="server" Text='<%# Bind("ACCOUNT_CURR") %>'
                                Width="306px" DataSourceID="ObjectDataSource_Curr" DataTextField="Currency_Code"
                                AppendDataBoundItems="true" DataValueField="Currency_ID">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:ObjectDataSource ID="ObjectDataSource_Curr" runat="server" SelectMethod="Get_CurrencyList"
                                TypeName="SMS.Business.Infrastructure.BLL_Infra_Currency"></asp:ObjectDataSource>
                            <asp:DropDownList ID="ddlACCOUNT_CURR" EnableViewState="true" runat="server" Text='<%# Bind("ACCOUNT_CURR") %>'
                                Width="306px" DataSourceID="ObjectDataSource_Curr" DataTextField="Currency_Code"
                                AppendDataBoundItems="true" DataValueField="Currency_ID">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="Edit"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update" CommandArgument="Save"
                                Text="Save" CssClass="linkbtn"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Update"
                                CommandArgument="SaveAndClose" Text="Save & Close" CssClass="linkbtn"></asp:LinkButton>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:Button ID="imgbtnBankAccSave" runat="server" Text="Save" CommandName="Insert" />
                            <asp:Button ID="imgbtnBankAccCancel" runat="server" Text="Save & Close" CommandName="Insert"
                                CommandArgument="SaveAndClose" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                </Fields>
            </asp:DetailsView>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
