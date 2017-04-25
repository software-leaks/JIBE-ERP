<%@ Page Title="Copy Contract" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CTP_Copy_Contract.aspx.cs" Inherits="Purchase_CTP_Copy_Contract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        body
        {
            color: Black;
        }
        .view-header
        {
            font-size: 14px;
            font-family: Calibri;
            font-weight: bold;
            text-align: left;
            width: 100%;
            color: Black;
            background-color: #81DAF5;
            border-collapse: collapse;
            padding: 2px 0px 2px 3px;
        }
        
        .tbl-content
        {
            border: 0px solid #81DAF5;
            width: 100%;
            border-collapse: collapse;
        }
        
        .tbl-footer
        {
            border-bottom: 1px solid #81DAF5;
            border-left: 1px solid #81DAF5;
            border-right: 1px solid #81DAF5;
            width: 100%;
            border-collapse: collapse;
            padding: 2px 2px 2px 2px;
        }
        
        .tbl-footer-td
        {
            width: 100%;
            padding: 2px 2px 2px 2px;
            text-align: left;
            background-color: #81DAF5;
        }
        .tdh
        {
            text-align: right;
            padding: 3px 2px 3px 0px;
            font-weight: bold;
        }
        .tdd
        {
            text-align: left;
            padding: 3px 2px 3px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Create contract for other department based on existing contract
    </div>
    <div class="page-content">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <table width="100%" style="border: 1px solid #F4F4F4"  cellspacing="0">
            <tr>
                <td style="width: 44%; border-right: 1px solid #F4F4F4">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdh">
                                Supplier :
                            </td>
                            <td class="tdd">
                                <asp:Label ID="lblSupplierName" runat="server"></asp:Label>
                            </td>
                            <td class="tdh">
                                Department :
                            </td>
                            <td class="tdd">
                                <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdh">
                                Port :
                            </td>
                            <td class="tdd">
                                <asp:Label ID="lblPort" runat="server"></asp:Label>
                            </td>
                            <td class="tdh">
                                Catalogue :
                            </td>
                            <td class="tdd">
                                <asp:Label ID="lblCatalogue" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 40%; border-right: 1px solid #F4F4F4; vertical-align: top">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdh">
                                Supplier Ref. :
                            </td>
                            <td class="tdd">
                                <asp:Label ID="lblSupplierRef" runat="server"></asp:Label>
                            </td>
                            <td class="tdh">
                                Company Ref. :
                            </td>
                            <td class="tdd">
                                <asp:Label ID="lblSeachangeRef" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdh">
                                Effective Date :
                            </td>
                            <td class="tdd">
                                <asp:Label ID="lblEffectiveDT" runat="server"></asp:Label>
                            </td>
                            <td class="tdh">
                                Approved By :
                            </td>
                            <td class="tdd">
                                <asp:Label ID="lblApprovedBy" Width="70px" runat="server"></asp:Label><asp:Image
                                    ID="imgApprovedByRmk" ImageUrl="~/Purchase/Image/view1.gif" Height="10px" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="updnewContract" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <table class="tbl-content" cellpadding="4" cellspacing="0">
                    <tr>
                        <td class="tdh" style="width: 10%;text-align:left;padding:10px 0px 10px 0px" >
                            Department Type :
                        </td>
                        <td class="tdd">
                            <asp:RadioButtonList ID="optList" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                OnSelectedIndexChanged="optList_SelectedIndexChanged" ForeColor="Black" TabIndex="2">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <table class="tbl-content" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="tdh" style="width: 25%; text-align: left; background-color: #F4F4F4">
                            Department :
                        </td>
                        <td class="tdh" style="width: 25%; text-align: left; background-color: #F4F4F4">
                            Catalogue :
                        </td>
                        <td class="tdh" style="width: 10%; text-align: left; background-color: #F4F4F4">
                        </td>
                        <td class="tdh" style="width: 30%; text-align: left; background-color: #F4F4F4">
                            Selected Departments/Catalogues
                        </td>
                        <td class="tdh" style="width: 30%; text-align: left; background-color: #F4F4F4">
                            Remark
                        </td>
                    </tr>
                    <tr>
                        <td class="tdd">
                            <asp:ListBox ID="cmbDept" runat="server" AutoPostBack="true" Width="200px" Height="400px"
                                OnSelectedIndexChanged="cmbDept_OnSelectedIndexChanged" Font-Size="12px" TabIndex="3"
                                AppendDataBoundItems="True">
                                <asp:ListItem Selected="True" Value="0">--SELECT --</asp:ListItem>
                            </asp:ListBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorcmbDept" runat="server" InitialValue="0"
                                ErrorMessage="Please select department" Display="None" ControlToValidate="cmbDept"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtendercmbDept" TargetControlID="RequiredFieldValidatorcmbDept"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td class="tdd">
                            <asp:ListBox ID="ddlCatalogue" runat="server" AutoPostBack="false" Width="250px"
                                Height="400px" Font-Size="12px" TabIndex="3" AppendDataBoundItems="True" ValidationGroup="selectcat">
                                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                            </asp:ListBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlCatalogue" ControlToValidate="ddlCatalogue"
                                ValidationGroup="selectcat" InitialValue="0" runat="server" ErrorMessage="Please select catalogue"
                                Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtenderddlCatalogue" TargetControlID="RequiredFieldValidatorddlCatalogue"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td class="tdd" style="text-align: center; width: 100px">
                            <asp:Button ID="btnAssign" runat="server" Text="Assign" ValidationGroup="selectcat"
                                OnClick="btnAssign_Click" /><br />
                            <br />
                            <asp:Button ID="btnDeSelect" runat="server" Text="Deselect" OnClick="btnDeSelect_Click" />
                        </td>
                        <td class="tdd">
                            <asp:ListBox ID="lstSelectedItems" runat="server" AutoPostBack="false" Width="320px"
                                Height="400px" Font-Size="12px" TabIndex="3" ValidationGroup="createcontract">
                            </asp:ListBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="lstSelectedItems"
                                ValidationGroup="createcontract" InitialValue="" runat="server" ErrorMessage="Please add items to this list"
                                Display="None"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td class="tdd" style="vertical-align: top">
                            <asp:TextBox ID="txtCopyRemark" runat="server" TextMode="MultiLine" Height="100px"
                                Width="210px"></asp:TextBox><br />
                            <br />
                            <asp:Button ID="btnCreateContract" runat="server" Height="30px" Text="Create contract for selected catalogues"
                                OnClick="btnCreateContract_Click" ValidationGroup="createcontract" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
