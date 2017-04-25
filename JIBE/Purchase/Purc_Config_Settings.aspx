<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Purc_Config_Settings.aspx.cs" 
    Title="Purchase Configuration Settings" Inherits="Purc_Config_Settings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
      <style type="text/css">
        body, html
        {
            overflow-x: hidden;
        }
        
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        
        .page
        {
            width: 99%;
        }
        td.blog_content div
        {
            width: 100%;
            text-align: left;
            padding: 2px;
        }
        
        .HeaderStyle-center
        {
            background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #333333;
            font-size: 11px;
            padding: 5px;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #959EAF;
            border-collapse: collapse;
        }
        .divclass
        {
            width: 100%;
            background-color: #BDBDBD;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
            color: White;
        }
        .divclass1
        {
            background-color: #fff;
            border: 0.5px solid #496077;
        }
        .checkbxinput
        {
            color: White;
            background-color: green;
            width: 50px;
            height: 20px;
        }
        .tdh
        {
            text-align: right;
            padding: 1px 3px 1px 3px;
        }
        .tdd
        {
            text-align: left;
            padding: 1px 3px 1px 3px;
        }
        .btn
        {
            -moz-border-radius: 20px;
            -webkit-border-radius: 20px;
            -khtml-border-radius: 20px;
            border-radius: 20px;
            width: 150px;
            height: 30px;
            background-color: #ccffff;
        }
        .cellbordr td
        {
            border: 1px solid #cccccc;
        }
    </style>
    <style type="text/css">
        
        .Initial
        {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left;
            background: url("../Images/InitialImage.png") no-repeat right top;
            color: Black;
            font-weight: bold;
        }
        .Initial:hover
        {
            color: White;
            background: url("../Images/SelectedButton.png") no-repeat right top;
        }
        .Clicked
        {
            float: left;
            display: block;
            background: url("../Images/SelectedButton.png") no-repeat right top;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: White;
        }
        #Menudiv
        {
            background-color: lightblue;
            height: 20%;
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="ConfigSetting" runat="server">
        <ContentTemplate>
            <div class="page-content" style="font-family: Tahoma; font-size: 12px">
                <ajaxToolkit:TabContainer ID="tbCntr" runat="server" Width="100%" 
                    ActiveTabIndex="4">
                    <%--TAB 1--%>
                    <ajaxToolkit:TabPanel ID="tbAutRqsn" runat="server" Font-Names="Tahoma">
                        <HeaderTemplate>
                            Automatic Requisition
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table width="550px">
                                <tr>
                                    <td align="left" style="color: Black">
                                        Create Automatic Requisition for Critical Spare items
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkAutoReq" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="color: Black">
                                        Required Supplier Confirmation for Delivery Update
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkSuppConf" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <%--TAB 2--%>
                    <ajaxToolkit:TabPanel ID="tbNewRnk" runat="server">
                        <HeaderTemplate>
                            Capture Rank to New Requisition
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table cellpadding="5" cellspacing="1" width="550px">
                                <tr>
                                    <td width="50px">
                                        Rank
                                    </td>
                                    <td width="200px">
                                        <asp:DropDownList ID="ddlPurc_CrewRank" Width="100%" runat="server" CssClass="control-edit required">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="300px" align="left">
                                        <asp:Button ID="btnPurcAddRank" runat="server" Text="Add Rank" ToolTip="Add Rank"
                                            OnClick="btnPurcAddRank_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div style="overflow-y: scroll; height: 350px">
                                            <asp:GridView ID="grdPurc_Crew_Rank" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                CellSpacing="1" EmptyDataText="No Record Found!" Width="100%" >
                                                <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                                <PagerStyle CssClass="PagerStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="True" Font-Size="12px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Ranks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRankId" runat="server" Text='<%# Eval("RankID")%>' Style="display: none"></asp:Label>
                                                            <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="True" HorizontalAlign="Left" Width="240px" />
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemStyle Width="20px" HorizontalAlign="Center" Wrap="True" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ImageUrl="~/Images/delete.png" ID="btnDelete" Text="Delete" OnClick="onDelete"
                                                                runat="server" CommandName="Delete" OnClientClick="return confirm('Do you want to delete?');"
                                                                CommandArgument='<%#Eval("RankID")%>' ToolTip="Delete" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <%--TAB 3--%>
                    <ajaxToolkit:TabPanel ID="tbConfig_Set" runat="server" Visible="false">
                        <HeaderTemplate>
                            Purchase Configuration Settings
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div>
                                <table cellpadding="2" cellspacing="2" width="100%" height="50%">
                                    <tr>
                                        <td align="right">
                                            PO Type
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="LBpotype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="potype_change"
                                                Width="450px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Requisition Type
                                        </td>
                                        <td align="Left" style="color: #FF0000; width: 1%">
                                            *
                                        </td>
                                        <td align="Left">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <biv>
                        <div style="float: left; text-align: left; width: 550px; height: 60px; overflow-x: hidden;
                                                            border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                            background-color: #ffffff;">
                            <asp:CheckBoxList ID="LBReqsntype" runat="server" Height="60px" AutoPostBack="true" 
                                                               RepeatColumns="8">
                            </asp:CheckBoxList>
                        </div>
                    </biv>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Supplier Type
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                            *
                                        </td>
                                        <td align="Left" style="height: 10px">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <biv>
                        <div style="float: left; text-align: left; width: 550px; height: 60px; overflow-x: hidden;
                                                            border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                            background-color: #ffffff;">
                            <asp:CheckBoxList ID="LbSupplierType" runat="server" Height="60px" AutoPostBack="true" OnSelectedIndexChanged="SupplierTypeChange"
                                                            RepeatColumns="8">
                            </asp:CheckBoxList>
                        </div>
                    </biv>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <table cellpadding="2" cellspacing="2" width="100%" height="50%">
                                    <tr>
                                        <td align="right">
                                            Owner
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RBtn_Owner" runat="server" />
                                        </td>
                                        <td align="right" class="style1">
                                            Delivery Port
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="Rbtn_DeliveryPort" runat="server" />
                                        </td>
                                        <td align="right">
                                            Delivery Port Date
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RBtn_Delivery_Port_date" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Vessle Movement Date
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RBtn_VesselMovement_Date" runat="server" />
                                        </td>
                                        <td align="right" class="style1">
                                            Quotation Required
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RBtn_Quote_required" runat="server" AutoPostBack="True" OnCheckedChanged="Quote_required_onChanged" />
                                        </td>
                                        <td align="right">
                                            No Of Quotaion
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_QuoteNo" runat="server" Enabled="False" Max="99" Min="1" Text="0"
                                                Width="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Item Category
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RBtn_Item_Category" runat="server" />
                                        </td>
                                        <td align="right" class="style1">
                                            Vessel Processing PO
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RBtn_Vessel_Processing_PO" runat="server" />
                                        </td>
                                        <td align="right">
                                            Enable Adding Free Text Item
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RBtn_Enable_Free_text" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Send a Copy to Vessel
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RBtn_Copy_to_Vessel" runat="server" />
                                        </td>
                                        <td align="right" class="style1">
                                            Supplier PO Confirmation
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RBtn_Sup_Po_Confirmation" runat="server" />
                                        </td>
                                        <td align="right">
                                            Delivery Confirmation On Board
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RBtn_Delivery_Confirm_Onbor" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Vessel Delivery Confirmation
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="Rbtn_Vessel_Delivery_Confirm" runat="server" />
                                        </td>
                                        <td align="right" class="style1">
                                            Withholding Tax
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RBtn_Withhold_tax" runat="server" />
                                        </td>
                                        <td align="right">
                                            VAT Configuration by Purchaser
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RBtn_Vat_Config_Purc" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Verification Required
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RBtn_require_verify" runat="server" />
                                        </td>
                                        <td align="right" class="style1">
                                            Auto PO Closing
                                        </td>
                                        <td align="right" style="color: #FF0000; width: 1%">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_AutoPoClosing" runat="server" Max="99" Min="1" Text="0" Width="80px"></asp:TextBox>
                                        </td>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="submit" runat="server" align="center" OnClick="submit_Click" Text="Save" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <%--TAB 4--%>
                    <ajaxToolkit:TabPanel ID="TbMandat_Set" runat="server">
                        <HeaderTemplate>
                            Purchase Mandatory Settings
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div align="center">
                                <table>
                                    <tr>
                                        <td>
                                            <div style="text-align: right;">
                                                <asp:CheckBoxList ID="CBLMandatoryFields" runat="server" TextAlign="Left" CellPadding="5"
                                                    CellSpacing="5" Height="60px">
                                                </asp:CheckBoxList>
                                                <asp:Button ID="btnUpdate" runat="server" align="center" Text="Update" OnClick="Update_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <%--TAB 5--%>
                     <ajaxToolkit:TabPanel ID="TbConfigSetting" runat="server" >
                    <HeaderTemplate>
                             Purchase Configuration Settings
                     </HeaderTemplate>
                      <ContentTemplate>
                            <div align="center" style="overflow:scroll">
                               <asp:GridView ID="gvPOConfig" runat="server" AutoGenerateColumns="False" EmptyDataText="No attachment found."
                                    CellPadding="2" Width="100%" GridLines="None" CssClass="GridView-css" OnRowCancelingEdit="grd_RowDeleting">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="True" 
                                        Font-Size="12px" />
                                    <Columns>

                                     <asp:TemplateField>
                                    <HeaderTemplate > PO Type</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:LinkButton  runat="server" ID="lnkBtn_potype" Text='<%#Eval("PO_Types") %>' CommandArgument='<%#Eval("PO_Type") %>' Font-Underline="false" OnClick="lnkReqsnClick" Enabled="false" ></asp:LinkButton>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Requsition Type</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:LinkButton  runat="server" ID="lnkBtn_Reqsn" Text='<%#Eval("Requisition") %>' CommandArgument='<%#Eval("RequisitionValue") %>' Font-Underline="false" OnClick="lnkReqsnClick"  Visible='<%# Eval("Requisition").ToString()!="-"  %>'></asp:LinkButton>
                                    <asp:ImageButton runat="server" ID="ImgBtn_Reqsn" Text='<%#Eval("Requisition") %>' CommandArgument='<%#Eval("RequisitionValue") %>' ImageUrl="~/Images/add.gif" OnClick="imgReqsnClick" Visible='<%# Eval("Requisition").ToString()=="-" %>' ></asp:ImageButton>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField>
                                    <HeaderTemplate > Supplier Type</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkBtn_Supplier" Text='<%#Eval("Supplier") %>' CommandArgument='<%#Eval("SupplierValue") %>' Font-Underline="false" Visible='<%# Eval("Supplier").ToString()!="-" %>' OnClick="lnkSuppClick"></asp:LinkButton>
                                    <asp:ImageButton runat="server" ID="ImgBtn_supp" Text='<%#Eval("Supplier") %>' CommandArgument='<%#Eval("SupplierValue") %>' ImageUrl="~/Images/add.gif" OnClick="imgSuppClick" Visible='<%# Eval("Supplier").ToString()=="-" %>' ></asp:ImageButton>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Owner</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_Owner" Checked='<%# Eval("Auto_Owner_Selection") %>' Enabled="false" ></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField>
                                    <HeaderTemplate > Delivery Port</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_DelvryPort" Checked='<%# Eval("Delivery_Port") %>' Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Delivery Date</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_DelvryDate" Checked='<%# Eval("Delivery_Date") %>' Enabled="false" ></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > vessel movement Date</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_VslMov_Date" Checked='<%# Eval("Vessel_Movement_Date")  %>' Enabled="false" ></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Item Category</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_ItmCat" Checked='<%# Eval("Item_Category") %>' Enabled="false" ></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Quotation Required</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_QtnReq" Checked='<%# Eval("Direct_Quotation")  %>' Enabled="false" OnCheckedChanged="OnQuoteRequireChange" AutoPostBack="true"></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Minimum quotations number</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txt_QtnNo" Text='<%# Eval("Min_QTN_Required") %>' Width="98%" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Vessel Processing PO</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_Vsl_PurcPO" Checked='<%# Eval("Vessel_Processing_PO") %>' Enabled="false" ></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Free Text Items </HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_Free_txt" Checked='<%# Eval("Free_Text_Items_addition") %>' Enabled="false" ></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Copy to vessel</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_Cpy_Vsl" Checked='<%# Eval("Copy_To_Vessel") %>' Enabled="false" ></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Supplier PO Confirmation</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_Suply_POConfirm" Checked='<%# Eval("Supplier_PO_Confirmation") %>' Enabled="false" ></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Delivery Conformation OnBd</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_DelConfirmBD" Checked='<%# Eval("Vessel_Delivery_Confirmation")  %>' Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Office Delivery Conformation</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_Off_DelConfirm" Checked='<%# Eval("Office_Delivery_Confirmation")  %>' Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Witholding Tax</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_WitHold" Checked='<%# Eval("Witholding_Tax") %>' Enabled="false" ></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > VAT Configuration by purchaser</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_VAT_Conf_Purchsr" Checked='<%# Eval("VAT_Config_By_Purchaser")  %>' Enabled="false" ></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Verification Reqiuired</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkBox_Verfication_Req" Checked='<%# Eval("Verification_Required")  %>' Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                    <HeaderTemplate > Auto Closing of PO  </HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txt_ClosngNo_PO" Text='<%# Eval("Auto_PO_Closing") %>' Width="98%" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                   
                                   <asp:TemplateField>
                                    <HeaderTemplate > Action  </HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:ImageButton ID="btnupdate" runat="server" CommandArgument='<%#Eval("PO_Type")%>'
                                                                                    Text='Update' ForeColor="Black" ToolTip="Update" ImageUrl="~/images/accept.png"
                                                                                    Visible="false"  Height="16px" OnClick="SaveClick"></asp:ImageButton>
                                                                                <asp:ImageButton ID="ImgBtnCancel" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                                                                    Visible="false" CausesValidation="False" CommandName="Cancel" AlternateText="Cancel" OnClick="Cancel_Click"
                                                                                   ></asp:ImageButton>
                                                                                <asp:ImageButton ID="ImgUpdate" runat="server" CommandArgument='<%#Eval("PO_Type")%>'
                                                                                    Text='Update' ForeColor="Black" ToolTip="Edit" ImageUrl="~/Images/Edit.gif" OnClick="updateClick"
                                                                                    Height="16px"></asp:ImageButton>&nbsp
                                    </ItemTemplate>
                                    </asp:TemplateField>


                                    </Columns>
                              </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    
                    <%--TAB 5--%>


                </ajaxToolkit:TabContainer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="dvPopupreqsn" title="Add requisition Type" style="display: none; width: 500px;
        height: 100px">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <div style="float: left; text-align: left; width: 100%;  overflow-x: hidden;
                                                            border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                            background-color: #ffffff;">
                            <asp:CheckBoxList ID="ChkBxList_ReqsnType" runat="server" Width="100%"  AutoPostBack="true" 
                                                               RepeatColumns="8">
                            </asp:CheckBoxList>
                        </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left"><asp:Button Id="Btn_Req_Save" runat="server" Text="Save" OnClick="BtnReq_Save" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

     <div id="dvPopupSupp" title="All Supplier Type" style="display: none; width: 500px;
        height: 100px">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <div style="float: left; text-align: left; width: 100%;  overflow-x: hidden;
                                                            border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                            background-color: #ffffff;">
                            <asp:CheckBoxList ID="ChkBxList_SuppType" runat="server" Width="100%"  AutoPostBack="true" 
                                                               RepeatColumns="8">
                            </asp:CheckBoxList>
                        </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left"><asp:Button Id="btn_save" runat="server" Text="Save" OnClick="onSave" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:Label ID="rowclicked" runat="server" Visible="false"/>
</asp:Content>
