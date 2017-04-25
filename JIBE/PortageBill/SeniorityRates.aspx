<%@ Page Title="Seniority Rates" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SeniorityRates.aspx.cs" Inherits="PortageBill_SeniorityRates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
     <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Rank Wise Seniority Amounts
    </div>
 <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

   
    <div id="Div1" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 5px;">
        <div id="page-content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr class="gradiant-css-orange" style="font-size:14px; font-weight:bold">
                            <td>
                                Select Rank
                            </td>
                            <td>
                                Edit Rank Seniority Rates for: <asp:Label ID="lblSelectedRank" runat="server"></asp:Label> <br />
                                Copy from: <asp:DropDownList ID="ddlRank" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlRank_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td>
                                Edit Company Seniority Rates for: <asp:Label ID="lblSelectedRank1" runat="server"></asp:Label> <br />
                                Copy from: <asp:DropDownList ID="ddlRank1" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlRank1_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="lstRank" runat="server" Width="300px" Height="500px" AutoPostBack="true"
                                    OnSelectedIndexChanged="lstRank_SelectedIndexChanged"></asp:ListBox>
                                
                            </td>
                            <td style="vertical-align: top">
                                <asp:GridView ID="gvNewSeniority" runat="server" DataKeyNames="SeniorityYear" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Seniority Year" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeniorityYear" runat="server" Text='<%#Eval("SeniorityYear") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Seniority Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAmount" runat="server" Text='<%#Eval("SeniorityAmount") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSeniorityAmount" runat="server"
                                                    ValidationGroup="Validate" Display="None" ErrorMessage="Seniority Amount is mandatory"
                                                    ControlToValidate="txtAmount" InitialValue=""></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="DataTypeCheck" Type="Double"
                                                    ValidationGroup="Validate" Display="None" ControlToValidate="txtAmount" ErrorMessage="Seniority Amount must be an integer." />

                                                <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="rfvSeniorityAmount"
                                                    runat="server">
                                                </tlk4:ValidatorCalloutExtender>
                                                <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="CompareValidator2"
                                                    runat="server">
                                                </tlk4:ValidatorCalloutExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Updated">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLastUpdated" runat="server" Text='<%#Eval("Modified_By").ToString() + " : " + Eval("Date_Of_Modification").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                    <PagerStyle CssClass="PagerStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                                    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                                </asp:GridView>

                                <div style="margin-top:10px; text-align:right;">
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                                    <asp:Button ID="btnSave" runat="server" Text="Save"  OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                            </td>

                            <td style="vertical-align: top">
                                <div style=" text-align:right">
                                <asp:Button ID="btnAddCompanySeniority" runat="server" Text="Add"  OnClick="btnAddCompanySeniority_Click" AccessKey />
                                </div>
                                <asp:GridView ID="gvCompanySeniority" runat="server" DataKeyNames="CompanyYear" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Seniority Year" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeniorityYear" runat="server" Text='<%#Eval("CompanyYear") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Seniority Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAmount" runat="server" Text='<%#Eval("SeniorityAmount") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSeniorityAmount" runat="server"
                                                    ValidationGroup="Validate" Display="None" ErrorMessage="Seniority Amount is mandatory"
                                                    ControlToValidate="txtAmount" InitialValue=""></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="DataTypeCheck" Type="Double"
                                                    ValidationGroup="Validate" Display="None" ControlToValidate="txtAmount" ErrorMessage="Seniority Amount must be an integer." />

                                                <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="rfvSeniorityAmount"
                                                    runat="server">
                                                </tlk4:ValidatorCalloutExtender>
                                                <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="CompareValidator2"
                                                    runat="server">
                                                </tlk4:ValidatorCalloutExtender>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Updated">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLastUpdated" runat="server" Text='<%#Eval("Modified_By").ToString() + " : " + Eval("Date_Of_Modification").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                    <PagerStyle CssClass="PagerStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                                    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                                </asp:GridView>

                                <div style="margin-top:10px; text-align:right;">
                                    <asp:Label ID="lblMsg1" runat="server" ForeColor="Red"></asp:Label>
                                    <asp:Button ID="btnUpdateCompanySeniorityRates" runat="server" Text="Save"  OnClick="btnUpdateCompanySeniorityRates_Click" />
                                    <asp:Button ID="btnCancelCompanySeniorityRates" runat="server" Text="Cancel" OnClick="btnCancelCompanySeniorityRates_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

              <div id="dvAddNewCompanySeniority" title="Add New Company Seniority" class="modal-popup-container"
               style="width: 550px; left: 40%; top: 30%;">    
               <asp:UpdatePanel ID="pnlCompanySeniority" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                   <div class="modal-popup-content">
                <div class="error-message" onclick="javascript:this.style.display='none';">
                    <asp:Label ID="Label2" runat="server" Visible="false" Text="" ></asp:Label>
                </div>
                <table border="0" style="width: 100%;" cellpadding="10">
                        <tr>
                            <td style="font-size: 11px; font-weight: bold">
                                Seniority Year:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCompanySeniorityYear" Width="250px" runat="server" Text=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCompanySeniorityYear" runat="server"
                                    ValidationGroup="Validate" Display="None" ErrorMessage="Seniority Year is mandatory"
                                    ControlToValidate="txtCompanySeniorityYear" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="DataTypeCheck" Type="Integer"
                                    ValidationGroup="Validate" Display="None" ControlToValidate="txtCompanySeniorityYear" ErrorMessage="Seniority Year must be an integer." />
                                <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rfvCompanySeniorityYear"
                                    runat="server">
                                </tlk4:ValidatorCalloutExtender>
                                <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="CompareValidator1"
                                    runat="server">
                                </tlk4:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                             <td style="font-size: 11px; font-weight: bold">
                                Seniority Amount:
                             </td>
                             <td>
                                 <asp:TextBox ID="txtCompanySeniorityAmount" Width="250px" runat="server" Text=""></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvCompanySeniorityAmount" runat="server"
                                    ValidationGroup="Validate" Display="None" ErrorMessage="Seniority Amount is mandatory"
                                    ControlToValidate="txtCompanySeniorityAmount" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="DataTypeCheck" Type="Double"
                                    ValidationGroup="Validate" Display="None" ControlToValidate="txtCompanySeniorityAmount" ErrorMessage="Seniority Amount must be an integer." />

                                <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="rfvCompanySeniorityAmount"
                                    runat="server">
                                </tlk4:ValidatorCalloutExtender>
                                <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="CompareValidator2"
                                    runat="server">
                                </tlk4:ValidatorCalloutExtender>
                             </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 11px; text-align: center;">
                                <asp:Button ID="btnSaveAndClose" runat="server" CssClass="button-css" 
                                    OnClick="btnSaveAndClose_Click" Text="Save" ValidationGroup="Validate" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnCancelCompanySeniority" runat="server" CssClass="button-css" 
                                    OnClientClick="hideModal('dvAddNewCompanySeniority');" Text="Close" />
                            </td>
                        </tr>
                        
                </table>                
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        </div>
    </div>
</asp:Content>
