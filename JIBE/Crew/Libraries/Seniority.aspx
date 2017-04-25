<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Seniority.aspx.cs" Inherits="Crew_Libraries_Seniority"
    MasterPageFile="~/Site.master" Title="Configure Seniority" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/crew_interview_css.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Configure Seniority
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="dvPageContent" style="height: 620px; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto;"  runat="Server">
        <asp:UpdatePanel ID="UpdatePanel_Grid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <table>
                    <tr class="gradiant-css-orange" style="font-size:14px; font-weight:bold">
                        <td style=" width:400px">
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
                        <div style="max-height: 500px; vertical-align:top; overflow: auto;">
                            <asp:GridView ID="gvSenority" runat="server" AutoGenerateColumns="False" OnRowUpdating="gvSenority_RowUpdating"
                                EmptyDataText="No Record Found" DataKeyNames="RankId" OnRowEditing="gvSenority_RowEditing"
                                OnRowCancelingEdit="gvSenority_RowCancelEdit" AllowSorting="True" CaptionAlign="Bottom"
                                CellPadding="4" GridLines="None" Width="100%" CssClass="gridmain-css">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Rank">
                                        <ItemTemplate>
                                                <asp:LinkButton ID="lblRankName" runat="server" Text='<%# Eval("Rank_Name") %>' CommandArgument='<%#  DataBinder.Eval(Container,"DataItem.RankId") + "," + DataBinder.Eval(Container,"DataItem.Rank_Name") + "," 
                                                + DataBinder.Eval(Container,"DataItem.CompanySeniorityConsidered")+ "," + DataBinder.Eval(Container,"DataItem.RankSeniorityConsidered") + "," + Container.DataItemIndex  %>'
                                            OnCommand="RankSelected" ></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                    </asp:TemplateField>
                                   
                                    <asp:CheckBoxField DataField="RankSeniorityConsidered" HeaderText="Rank Seniority"
                                        ItemStyle-HorizontalAlign="Center" /> 
                                    <asp:CheckBoxField DataField="CompanySeniorityConsidered" HeaderText="Company Seniority"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="btnAccept" runat="server" AlternateText="Update" 
                                                CommandName="Update" ImageUrl="~/images/accept.png"  ValidationGroup="saveFZ"/>
                                            <asp:ImageButton ID="btnReject" runat="server" AlternateText="Cancel" CausesValidation="False"
                                                CommandName="Cancel" ImageUrl="~/images/reject.png" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit" CausesValidation="False"
                                                CommandName="Edit" ImageUrl="~/images/edit.gif" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        </td>
                        <td style="vertical-align: top">
                                <div style="max-height: 500px; overflow: auto;">
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
                                                <asp:Label ID="lblLastUpdated" runat="server" Text='<%#Eval("Modified_By").ToString() + " : " + UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Modification"))) %>'></asp:Label>
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
                                </div>
                                <div style="margin-top:10px; text-align:right;">
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                                    <asp:Button ID="btnSave" runat="server" Text="Save"  OnClick="btnSave_Click"  ValidationGroup="Validate"/>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                            </td>

                        <td style="vertical-align: top">
                                <div style=" text-align:right"> 
                                <asp:Button ID="btnAddCompanySeniority" runat="server" Text="Add" OnClick="btnAddCompanySeniority_Click"
                                         />
                                </div>
                                <div style="max-height: 500px; overflow: auto;">
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
                                                    ValidationGroup="Validate1" Display="None" ErrorMessage="Seniority Amount is mandatory"
                                                    ControlToValidate="txtAmount" InitialValue=""></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="DataTypeCheck" Type="Double"
                                                    ValidationGroup="Validate1" Display="None" ControlToValidate="txtAmount" ErrorMessage="Seniority Amount must be an integer." />

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
                                                <asp:Label ID="lblLastUpdated" runat="server" Text='<%#Eval("Modified_By").ToString() + " : " + UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Modification"))) %>'></asp:Label>
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
                                </div>
                                <div style="margin-top:10px; text-align:right;">
                                    <asp:Label ID="lblMsg1" runat="server" ForeColor="Red"></asp:Label>
                                    <asp:Button ID="btnUpdateCompanySeniorityRates" runat="server" Text="Save"   ValidationGroup="Validate1" OnClick="btnUpdateCompanySeniorityRates_Click" />
                                    <asp:Button ID="btnCancelCompanySeniorityRates" runat="server" Text="Cancel" OnClick="btnCancelCompanySeniorityRates_Click" />
                                </div>
                            </td>
                    </tr>
                    </table>
                    <div id="dvAddNewCompanySeniority" title="Add New Company Seniority"  
               style="width: 550px; left: 40%; top: 30%;  display: none;">    
             
                <table border="0" style="width: 100%;" cellpadding="10">
                        <tr>
                            <td style="font-size: 11px; font-weight: bold">
                                Seniority Year:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCompanySeniorityYear" Width="250px" runat="server" Text=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCompanySeniorityYear" runat="server"
                                    ValidationGroup="Validate2" Display="None" ErrorMessage="Seniority Year is mandatory"
                                    ControlToValidate="txtCompanySeniorityYear" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="DataTypeCheck" Type="Integer"
                                    ValidationGroup="Validate2" Display="None" ControlToValidate="txtCompanySeniorityYear" ErrorMessage="Seniority Year must be an integer." />
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
                                    ValidationGroup="Validate2" Display="None" ErrorMessage="Seniority Amount is mandatory"
                                    ControlToValidate="txtCompanySeniorityAmount" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="DataTypeCheck" Type="Double"
                                    ValidationGroup="Validate2" Display="None" ControlToValidate="txtCompanySeniorityAmount" ErrorMessage="Seniority Amount must be an integer." />

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
                                    OnClick="btnSaveAndClose_Click" Text="Save" ValidationGroup="Validate2" />
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
</asp:Content>
