<%@ Page Title="Create new contract" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CTP_New_Contract.aspx.cs" Inherits="Purchase_CTP_New_Contract" %>

<%@ Register Src="../UserControl/uc_Purc_Ctp_AddItem.ascx" TagName="uc_Purc_Ctp_AddItem"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/uc_Purc_Ctp_Send_RFQ.ascx" TagName="uc_Purc_Ctp_Send_RFQ"
    TagPrefix="uc2" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="HeadContent" runat="Server">
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
            border: 1px solid #81DAF5;
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
        }
        .tdd
        {
            text-align: left;
            padding: 3px 2px 3px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="cntMain" ContentPlaceHolderID="MainContent" runat="Server">

  <div class="page-title">
       New Contract
    
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
    <asp:UpdatePanel ID="updmaincontent" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" style="color: Black; border-collapse: collapse" cellpadding="0"
                cellspacing="0">
                <tr>
                    <td>
                        <asp:MultiView ID="mlvCTP" runat="server" ActiveViewIndex="0">
                            <asp:View ID="viewNewContract" runat="server">
                                
                                <asp:UpdatePanel ID="updnewContract" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <table class="tbl-content" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="tdh" style="width: 10%">
                                                    Department Type :
                                                </td>
                                                <td class="tdd">
                                                    <asp:RadioButtonList ID="optList" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                        OnSelectedIndexChanged="optList_SelectedIndexChanged" ForeColor="Black" TabIndex="2">
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdh">
                                                    Department :
                                                </td>
                                                <td class="tdd">
                                                    <asp:DropDownList ID="cmbDept" runat="server" AutoPostBack="true" Width="160px" OnSelectedIndexChanged="cmbDept_OnSelectedIndexChanged"
                                                        Font-Size="12px" TabIndex="3" AppendDataBoundItems="True">
                                                        <asp:ListItem Selected="True" Value="0">--SELECT --</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorcmbDept" runat="server" InitialValue="0"
                                                        ErrorMessage="Please select department" Display="None" ControlToValidate="cmbDept"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtendercmbDept" TargetControlID="RequiredFieldValidatorcmbDept"
                                                        runat="server">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdh">
                                                    Catalogue :
                                                </td>
                                                <td class="tdd">
                                                    <asp:DropDownList ID="ddlCatalogue" runat="server" AutoPostBack="false" Width="160px"
                                                        Font-Size="12px" TabIndex="3" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlCatalogue_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlCatalogue" ControlToValidate="ddlCatalogue"
                                                        InitialValue="0" runat="server" ErrorMessage="Please select catalogue" Display="None"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtenderddlCatalogue" TargetControlID="RequiredFieldValidatorddlCatalogue"
                                                        runat="server">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <table class="tbl-footer" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="tbl-footer-td">
                                            <asp:Button ID="btnNext_1" OnCommand="btnNext_Prev_Click" CommandName="1" Text="Next"
                                                ToolTip="Add Item" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="viewAddItems" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <div style="font-size: 14px; background-color: #5588BB; color: White; text-align: center;
                                                padding: 2px 0px 2px 0px; margin: 0px 0px 0px 0px;">
                                                <b>Add item </b>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <table class="tbl-content">
                                    <tr>
                                        <td>
                                            <uc1:uc_Purc_Ctp_AddItem ID="uc_Purc_Ctp_Add_Item" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table class="tbl-footer">
                                    <tr>
                                        <td class="tbl-footer-td">
                                            <asp:Button ID="btnPrev_0" OnCommand="btnNext_Prev_Click" CommandName="0" Text="Previous"
                                                ToolTip="new contract" runat="server" />
                                            <asp:Button ID="btnNext_2" OnCommand="btnNext_Prev_Click" CommandName="2" ToolTip="Send to supplier"
                                                Text="Next" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="viewSuppliers" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <div style="font-size: 14px; background-color: #5588BB; color: White; text-align: center;
                                                padding: 2px 0px 2px 0px; margin: 0px 0px 0px 0px;">
                                                <b>Select Supplier </b>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
            <table class="tbl-content">
                <tr>
                    <td style="width: 100%">
                        <asp:UpdatePanel ID="updPurc_Ctp_Send_RFQSupp" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <uc2:uc_Purc_Ctp_Send_RFQ ID="uc_Purc_Ctp_Send_RFQSupp" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <table class="tbl-footer">
                <tr>
                    <td class="tbl-footer-td">
                        <asp:Button ID="btnPrev_1" OnCommand="btnNext_Prev_Click" CommandName="1" Text="Previous"
                            ToolTip="add item" runat="server" />
                    </td>
                </tr>
            </table>
            </asp:View> </asp:MultiView> </td> </tr> </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
