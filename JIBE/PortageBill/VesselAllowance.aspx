<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselAllowance.aspx.cs" Inherits="PortageBill_VesselAllowance"  MasterPageFile="~/Site.master"  Title="Vessel Allowance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">   
<link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>   
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Vessel Allowance
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


     <div id="Div1" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 5px;">
        <div id="page-content">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="error-message" onclick="javascript:this.style.display='none';">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                    </div>
                    <fieldset style="text-align: left; margin-top: 5px; padding: 2px;">
                        <legend>
                            <asp:Label ID="lblVesselAllowance" runat="server" Text="Vessel Allowance:"></asp:Label>
                        </legend>
                        <table style="width: 100%">
                            <tr>
                                <td style="vertical-align: top;" >
                                    <table>
                                        <tr>
                                            <td> 
                                                <asp:Label ID="lblVessels" Text="Vessels:" runat="server" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 220px; color: Black;">                                   
                                                <asp:ListBox ID="lstVesselList" runat="server" Width="220px" 
                                                    AutoPostBack="true" OnSelectedIndexChanged="lstVesselList_SelectedIndexChanged" 
                                                    Height="500px" >
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top;">
                                    <table>
                                        <tr>
                                            <td> 
                                                <asp:Label ID="Label1" Text="Nationality Group:" runat="server" ></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnAddNationalityGroup" runat="server" Text="Add Nationality" OnClick="btnAddNationalityGroup_OnClick" Enabled="false" />
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td  colspan="2" style="width: 220px; ">    
                                            <asp:GridView ID="gvNationalityGroup" runat="server" AutoGenerateColumns="False" 
                                                EmptyDataText="No Record Found" DataKeyNames="NationalityGroupId" 
                                                    CaptionAlign="Bottom" ShowHeaderWhenEmpty="true"
                                                CellPadding="4" GridLines="None" Width="300px" CssClass="GridView-css">
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nationality Group">
                                                        <ItemTemplate>
                                                                <asp:LinkButton ID="lblNationalityGroup" runat="server" Text='<%# Eval("NationalityGroupName") %>' CommandArgument='<%#  Container.DataItemIndex +","+Eval("NationalityGroupId")  %>'
                                                                    OnCommand="GroupSelected"  ></asp:LinkButton> 
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">                                       
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit" CausesValidation="False" OnCommand="AddNationality"
                                                                CommandArgument='<%# Eval("NationalityGroupId") + "," + Eval("NationalityGroupName")%>' ImageUrl="~/images/edit.gif" />
                                                            <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete" CausesValidation="False" OnCommand="DeleteNationality"
                                                                CommandArgument='<%# Eval("NationalityGroupId")%>' ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to delete the record?')" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="100px"/>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="HeaderStyle-css" />
                                                <PagerStyle CssClass="PagerStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <EditRowStyle CssClass="EditRowStyle-css" />
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                                                <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                                                <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                                                <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                                            </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top;">
                                    <table>
                                        <tr>
                                            <td> 
                                                <asp:Label ID="Label2" Text="Countries:" runat="server" ></asp:Label>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 220px; color: Black;">                                   
                                                <asp:ListBox ID="lstCountryList1" runat="server" Width="220px" Height="500px"  Enabled="false">
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top;">                                    
                                    <table width="350px">
                                        <tr>
                                            <td> 
                                                <asp:Label ID="Label3" Text="Allowance:" runat="server" ></asp:Label>                                              
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px;">
                                                Effective Date:
                                                <asp:TextBox ID="txteffdt" CssClass="textbox-css" runat="server"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender51" runat="server" TargetControlID="txteffdt"
                                                    Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="vgAddWages" runat="server"
                                                    ControlToValidate="txteffdt" ErrorMessage="Effective date is mandatory!!"></asp:RequiredFieldValidator>
                                            </td>
                                            <td valign="top">
                                                <asp:Button ID="btnSaveAllowance" runat="server" Text="Save Allowance" ValidationGroup="vgAddWages" OnClick="btnSaveAllowance_OnClick" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="vertical-align: top; color: Black;">         
                                                <div style="max-height: 450px; overflow: auto;"> 
                                                <asp:GridView ID="gvVesselAllowance" runat="server" ShowHeaderWhenEmpty="true"
                                                    AutoGenerateColumns="False" EmptyDataText="No Record Found" CellPadding="4" 
                                                    GridLines="None" Width="100%"  CssClass="GridView-css">
                                                 
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Rank">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRankName" runat="server" Text='<%#Eval("Rank_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblRankId" runat="server" Text='<%#Eval("RankId") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmount" runat="server" Font-Size="11px" Width="70px" Text='<%# String.Format("{0:0.00}", Eval("Amount"))%>'
                                                                    CssClass="numeric-edit" EnableViewState="true"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <%-- <asp:TemplateField HeaderText="Last Updated">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLastUpdated" runat="server" Text='<%#Eval("Modified_By").ToString() + " : " + Eval("Date_Of_Modification").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    </Columns>           
                                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                                    <PagerStyle CssClass="PagerStyle-css" />
                                                    <RowStyle CssClass="RowStyle-css" />
                                                    <EditRowStyle CssClass="EditRowStyle-css" />
                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                    <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                                                    <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                                                    <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                                                    <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />                                        
                                                </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>                   
                                </td>
                            </tr>
                            
                        </table>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel ID="pnlNationalityGroup" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="divNationalityGroup" style="font-family: Tahoma; color: black; display: none; ">
                    <center>
                     <fieldset style="text-align: left; margin-top: 5px; padding: 2px; ">
                        <legend>
                            <asp:Label ID="Label4" runat="server" Text="Nationality Group:"></asp:Label>
                        </legend>
                        <table>
                            <tr>
                                <td>Name:</td>
                                <td>
                                    <asp:TextBox ID="txtGroupName" runat="server" Width="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="vgAddCountry" runat="server"
                                        ControlToValidate="txtGroupName" ErrorMessage="Group name is mandatory !!"></asp:RequiredFieldValidator>
                                </td>
                                <td><asp:Button ID="btnSaveNationalityGroup" runat="server" Text="Save" OnClick="btnSaveNationalityGroup_OnClick"  ValidationGroup="vgAddCountry"/></td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div  style="height:580px; overflow:auto;" >
                                        <asp:CheckBoxList ID="chkCountryList" runat="server"  DataTextField="COUNTRY"
                                            DataValueField="ID" >
                                        </asp:CheckBoxList>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                 </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
