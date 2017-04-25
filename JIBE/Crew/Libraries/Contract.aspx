<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contract.aspx.cs" Inherits="Crew_Libraries_Contract"  Title="Contract"  MasterPageFile="~/Site.master" %>

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
        Contract Type
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
        
        <table style="width: 70%;">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Contract Type:</legend>
                        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <table cellpadding="0" cellspacing="5" style="width: 100%;">
                                        <tr>                                          
                                           <td style="text-align: right">
                                                <asp:LinkButton ID="lnkAddNewContract" OnClick="AddNewContract"  runat="server" CssClass="linkbtn">Add New Contract</asp:LinkButton>
                                            </td>
                                        </tr>
                                       
                                    </table>
                                    <asp:UpdatePanel ID="UpdatePanel_Grid" runat="server"  UpdateMode="Conditional" >
                                        <ContentTemplate>
                                        <asp:HiddenField ID="hdContractId" runat="server" />
                                        <asp:GridView ID="gvContract" runat="server" AutoGenerateColumns="False"
                                            OnRowDeleting="gvContract_RowDeleting" 
                                            DataKeyNames="ContractId" EmptyDataText="No Record Found"
                                            AllowSorting="True" CaptionAlign="Bottom" CellPadding="4" 
                                            GridLines="None" Width="100%" CssClass="gridmain-css">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <Columns>
                                            
                                            <asp:TemplateField HeaderText="Contract Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRankScaleName" runat="server" Text='<%#Eval("Contract_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblContractId" runat="server" Text='<%#Eval("ContractId")%>' Visible="false" ></asp:Label>
                                                    <asp:TextBox ID="txtContract_Name" Font-Size="11px" MaxLength="50" runat="server" Text='<%#Bind("Contract_Name")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"  Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Countries" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                     <asp:LinkButton ID="lnkNationality" runat="server" Text='<%# Bind("CountryList") %>' CausesValidation="False"  OnCommand="SelectNationality" CommandArgument='<%# Eval("ContractId")%>' ></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"  Width="150px"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vessel Flags" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                     <asp:LinkButton ID="lnkVesselFlag" runat="server" Text='<%# Bind("VesselFlagList") %>' CausesValidation="False"  OnCommand="SelectVesselFlags" CommandArgument='<%# Eval("ContractId")%>' ></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"  Width="150px"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit" CausesValidation="False" OnCommand="EditContract" CommandArgument='<%# Eval("ContractId")%>'
                                                    ImageUrl="~/images/edit.gif" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
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

         <div id="dvAddNewContract" title="Add New Contract" class="modal-popup-container"
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
                            Contract Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractName" Width="250px" runat="server" Text=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvContractName" runat="server"
                            ValidationGroup="Validate" Display="None" ErrorMessage="Contract Name is mandatory!"
                            ControlToValidate="txtContractName" InitialValue=""></asp:RequiredFieldValidator>
                            <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rfvContractName"
                                        runat="server">
                                </tlk4:ValidatorCalloutExtender>
                        </td>
                        </tr>
                        <tr>
                         <td style="font-size: 11px; font-weight: bold">
                            Selection Type:
                         </td>
                         <td>
                            <asp:RadioButtonList ID="rdbContractType" CssClass="txtInput" Width="200px" runat="server"  AutoPostBack="true"
                                RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbContractType_SelectedIndexChanged">
                                <asp:ListItem Value="Nationality">Nationality</asp:ListItem>
                                <asp:ListItem Value="VesselFlag">Vessel Flag</asp:ListItem>
                            </asp:RadioButtonList>
                         </td>
                        </tr>
                        <tr>
                        <td style="font-size: 11px; font-weight: bold">
                            Countries:
                        </td>
                        <td style="font-size: 11px; font-weight: bold">
                            Vessel Flags
                        </td>
                        <tr>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px">
                                <div style="height:580px; overflow:auto;">
                                    <asp:CheckBoxList ID="chkCountryList1" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px">
                                <div style="height:580px; overflow:auto;">
                                    <asp:CheckBoxList ID="chkVesselFlagList1" runat="server">
                                    </asp:CheckBoxList>
                                </div>
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
                        </tr>
                        
                </table>                
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:UpdatePanel ID="pnlCountries" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="divCountryList" style="font-family: Tahoma; color: black; display: none;">
                    <center>
                     <div style="padding: 0px; padding: 2px; border-top: 0; background-color: #5588BB; color: #FFFFFF; text-align: center;"> <b>Countries</b>
                     </div>
                 </center>
                 <asp:Button ID="btnSelectCountry" runat="server" Text="Save Selection"  OnClick="SaveSelectedCountries"/>
                    <div  style="height:580px; overflow:auto;" >
                        <asp:CheckBoxList ID="chkCountryList" runat="server"  > </asp:CheckBoxList>
                    </div>
                </div>
            </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="pnlVesselFlags" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="divVesselFlagList" style="font-family: Tahoma; color: black; display: none;">
                    <center>
                     <div style="padding: 0px; padding: 2px; border-top: 0; background-color: #5588BB; color: #FFFFFF; text-align: center;"> <b>Vessel Flags</b> </div>
                 </center>
                 <asp:Button ID="btnSelectVesselFlags" runat="server" Text="Save Selection"  OnClick="SaveSelectedVesselFlags"/>
                    <div  style="height:580px; overflow:auto;" >
                        <asp:CheckBoxList ID="chkVesselFlagList" runat="server"  > </asp:CheckBoxList>
                    </div>
                </div>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
