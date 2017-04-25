<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanySeniorityReward.aspx.cs" Inherits="Crew_Libraries_CompanySeniorityReward"  Title="Seniority Reward"  MasterPageFile="~/Site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
        Seniority Reward Year
    </div>
   <asp:UpdateProgress ID="UpdateProgress1"  runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                 
                <img src="../Images/loaderbar.gif"alt="Please Wait" />
            </div>
        </ProgressTemplate>
 </asp:UpdateProgress>
 
    <div id="dvPageContent" style="height: 620px; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto;"  runat="server">
        
        <table style="width: 40%;">
            <tr>
                <td style="width: 40%; vertical-align: top;">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Seniority Reward Year:</legend>
                        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <table cellpadding="0" cellspacing="5" style="width: 100%;">
                                        <tr>                                          
                                           <td style="text-align: right">
                                                <asp:LinkButton ID="lnkAddNewSeniorityYear" OnClick="AddNewSeniorityYear"  runat="server" CssClass="linkbtn">Add New Seniority Reward Year</asp:LinkButton>
                                            </td>
                                        </tr>
                                       
                                    </table>
                                    <asp:UpdatePanel ID="UpdatePanel_Grid" runat="server"  UpdateMode="Conditional" >
                                        <ContentTemplate>
                                        <asp:GridView ID="gvSeniorityYear" runat="server" AutoGenerateColumns="False"
                                            OnRowDeleting="gvSeniorityYear_RowDeleting" 
                                            DataKeyNames="Id" EmptyDataText="No Record Found"
                                            AllowSorting="True" CaptionAlign="Bottom" CellPadding="4" 
                                            GridLines="None" Width="100%" CssClass="gridmain-css">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <Columns>
                                            
                                            <asp:TemplateField HeaderText="Seniority Reward Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSeniorityYear" runat="server" Text='<%#Eval("SeniorityYear")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"  Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
                                                        CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                        AlternateText="Delete"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </td>
            </tr>
        </table>

         <div id="dvAddNewSeniorityYear" title="Add New Seniority Year" class="modal-popup-container"
               style="width: 550px; left: 40%; top: 30%;">    
               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                   <div class="modal-popup-content">
                <div class="error-message" onclick="javascript:this.style.display='none';">
                    <asp:Label ID="lblMsg" runat="server" Visible="false" Text="" ></asp:Label>
                </div>
                <table border="0" style="width: 100%;" cellpadding="10">
                        <tr>
                            <td style="font-size: 11px; font-weight: bold">
                                Seniority Year:
                            </td>
                            <td>
                                <asp:TextBox ID="txtSeniorityYear" Width="250px" runat="server" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSeniorityYearName" runat="server"
                                ValidationGroup="Validate" Display="None" ErrorMessage="SeniorityYear Name is mandatory!"
                                ControlToValidate="txtSeniorityYear" InitialValue=""></asp:RequiredFieldValidator>
                                <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rfvSeniorityYearName"
                                            runat="server">
                                    </tlk4:ValidatorCalloutExtender>
                            </td>
                        </tr>
                         <tr>
                            <td colspan="2" style="font-size: 11px; text-align: center;">
                                <asp:Button ID="btnSaveAndClose" runat="server" CssClass="button-css" 
                                    OnClick="btnSaveAndClose_Click" Text="Save" ValidationGroup="Validate" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btncancel" runat="server" CssClass="button-css" 
                                    OnClientClick="hideModal('dvAddNewContract');" Text="Close" />
                            </td>
                        </tr>
                </table>                
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
