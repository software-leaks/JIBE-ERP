<%@ Page Title="Document Type" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="DocTypeLib.aspx.cs" Inherits="DMS_Admin_DocTypeLib" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <style type="text/css">
        #pageTitle
        {
            background-color: gray;
            color: White;
            font-size: 12px;
            text-align: center;
            padding: 2px;
            font-weight: bold;
        }
        .header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
          
        }
        h4
        {
            font-size: 1.2em;
            color: #ffffff;
            font-weight: bold;
            margin: 0 0 0 5px;
        }
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
        .linkbtn
        {
            color: black;
            font-size: 14px;
            text-align: center;
            background-color: white;
            text-decoration: none;
            border-left: 1px solid #cccccc;
            padding-left: 10px;
            border-top: 1px solid #cccccc;
            padding-top: 5px;
            border-right: 1px solid #cccccc;
            padding-right: 10px;
            border-bottom: 1px solid #cccccc;
            padding-bottom: 3px;
            background-color: #F1F8E0;
            white-space: nowrap;
        }
        .dvhdr1
        {
            font-family: Tahoma;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
            width: 200px;
            color: Black; /*background: #F5D0A9;*/
            border: 1px solid #F1C15F;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background:#F1C15F;
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .dvbdy1
        {
            background: #FFFFFF;
            font-family: arial;
            font-size: 11px; /*border-left: 2px solid #3B0B0B;             border-right: 2px solid #3B0B0B;             border-bottom: 2px solid #3B0B0B;*/
            padding: 5px;
            width: 200px;
            background-color: #E0F8E0;
            border: 1px solid #F1C15F;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function showDivAddDocType() {
            //document.getElementById("dvAddDocType").style.display = "block";
            showModal('dvAddDocType', true, dvAddDocType_onClose);
        }
        function dvAddDocType_onClose() {

        }

        function showDivAddAttribute() {
            //document.getElementById("dvAddAttribute").style.display = "block";
            showModal('dvAddAttribute', true, dvAddAttribute_onClose);
        }
        function dvAddAttribute_onClose() {

        }

        function closeDivAddDocType() {
            //document.getElementById("dvAddDocType").style.display = "none";
            hideModal('dvAddDocType');
        }

        function closeDivAddAttribute() {
            //document.getElementById("dvAddAttribute").style.display = "none";
            hideModal('dvAddDocType');
        }

        //        $(document).ready(function () {
        //            $("#dvAddDocType").draggable();
        //            $("#dvAddAttribute").draggable();
        //        });

        function chk_Changed(obj) {
            alert(1);
             alert(obj);
             var rdo = $(obj.id.replace('chkVoyage', 'CheckBox'));
//            var options = $('table#' + rdo.selector).find('input:radio');

//            var i = 0;
             if (obj.checked == true) {
                 var options = $('table#' + rdo).find('input:CheckBox');
                 alert('inside');
                 alert(options);
//                for (i = 0; i < options.length; i++) {
//                    options[i].checked = false;
//                    $(options[i]).attr("disabled", true);
 //               }
            }
//            else {
//                for (i = 0; i < options.length; i++) {
//                    $(options[i]).removeAttr("disabled");
//                }
//            }
           
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <div class="page-title">
       Document Type
    </div>
   <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
  
    <asp:ObjectDataSource ID="ObjectDataSource_DocTypes" runat="server" DeleteMethod="DeleteDocType" 
        UpdateMethod="EditDocType" TypeName="SMS.Business.DMS.BLL_DMS_Admin" SelectMethod="Get_DocTypeList" OnUpdating="ObjectDataSource1_Updating">
        <UpdateParameters>
            <asp:Parameter Name="DocTypeID" Type="Int32" />
            <asp:Parameter Name="DocTypeName" Type="String" />
            <asp:Parameter Name="Legend" Type="String" />
            <asp:Parameter Name="Deck" Type="String" />
            <asp:Parameter Name="Engine" Type="String" />
            <asp:Parameter Name="AlertDays" Type="Int32" />
            <asp:Parameter Name="isDocCheckList" Type="Int32" />
            <asp:Parameter Name="Voyage" Type="Int32" />
            <asp:Parameter Name="Vessel_Flag" Type="Int32" />
            <asp:Parameter Name="isExpiryMandatory" Type="Int32" />
             <asp:Parameter Name="VesselFlagList"  />
             <asp:SessionParameter Name="Modified_By" SessionField="userid" Type="Int32" />
        </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Name="DocTypeID" Type="Int32" />
            <asp:SessionParameter Name="Deleted_By" SessionField="userid" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter Name="Vessel_Flag" ControlID="rdoSearchVesselFlag" PropertyName="SelectedValue"
                DefaultValue="0" Type="Int32" />
            <asp:ControlParameter Name="SearchText" ControlID="txtSearchText" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter Name="RankID" ControlID="ddlRank" PropertyName="SelectedValue" DefaultValue="0" 
                Type="Int32" />
            <asp:ControlParameter ControlID="GridViewDocType" Name="DocTypeID" PropertyName="SelectedValue"
                                                        Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" TypeName="SMS.Business.DMS.BLL_DMS_Admin"
        SelectMethod="Get_UnAssignedAttributesToTypeID" DeleteMethod="DeleteDocAttribute"
        InsertMethod="InsertDocAttribute" UpdateMethod="EditDocAttribute">
        <DeleteParameters>
            <asp:Parameter Name="AttributeID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="AttributeName" Type="String" />
            <asp:Parameter Name="AttributeDataType" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="AttributeID" Type="Int32" />
            <asp:Parameter Name="AttributeName" Type="String" />
            <asp:Parameter Name="AttributeDataType" Type="String" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="GridViewDocType" Name="DocTypeID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="dsVesselFlag" runat="server" TypeName="SMS.Business.Infrastructure.BLL_Infra_VesselLib"
        SelectMethod="Get_VesselFlagList">
        <SelectParameters>
            <asp:SessionParameter Name="UserCompanyID" SessionField="USERCOMPANYID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

     <div id="dvAddDocType" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
        border-style: solid; border-width: 1px; width: 450px; position: absolute; left: 40%;
        top: 15%; z-index: 1; color: black" class="ui-widget-content" title="Add New Document Type">
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="border-style: solid; border-color: Silver; border-width: 1px; width: 100%;">
                        <tr>
                            <td style="font-size: 12px; text-align: left; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold">
                                Doc Type Name:
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px">
                                <asp:TextBox ID="txtDocType" CssClass="textbox-css" Width="200px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; text-align: left; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold">
                                Alert before expiry (Days):
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px">
                                <asp:TextBox ID="txtAlertDays" Text="30" CssClass="textbox-css" Width="200px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; text-align: left; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold">
                                Required for SignOn:
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px">
                                <asp:CheckBox ID="chkCheckList" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; text-align: left; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold">
                                Voyage Specific:
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px">
                             <asp:CheckBox ID="chkVoyageDoc" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; text-align: left; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold">
                                Vessel Flag:
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px">                     
                                <asp:CheckBoxList ID="chkVesselFlagList" runat="server"  SelectionMode="Multiple" Font-Names="Tahoma"  DataTextField="Flag_Name" DataValueField="Vessel_Flag"  DataSourceID="dsVesselFlag"
                                            Font-Size="11px">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; text-align: left; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold">
                                Expiry Date Mandatory :
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px">
                                <asp:CheckBox ID="chkExpiryMandatory" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 11px; text-align: center; border-style: solid;
                                border-color: Silver; border-width: 1px">
                                <asp:Button ID="btnSaveDocType" runat="server" Text="Save" CausesValidation="false"
                                    OnClick="btnSaveDocType_Click" />
                                <asp:Button ID="btnCancel" CssClass="button-css" runat="server" Text="Cancel" OnClientClick="closeDivAddDocType()" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="dvAddAttribute" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
        border-style: solid; border-width: 1px; width: 450px; position: absolute; left: 40%;
        top: 15%; z-index: 1; color: black" class="ui-widget-content" title="Add New Attribute">
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table style="border-style: solid; border-color: Silver; border-width: 1px; width: 100%;">
                        <tr>
                            <td style="font-size: 11px; text-align: left; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold">
                                Attribute Name:
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px">
                                <asp:TextBox ID="txtAttribute" CssClass="textbox-css" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field !!"
                                    ControlToValidate="txtAttribute"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; text-align: left; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold; vertical-align: top;">
                                Attribute Type:
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px">
                                <asp:RadioButtonList ID="rdoLstAttributeDataType" runat="server" Width="156px" AutoPostBack="true"
                                    OnSelectedIndexChanged="rdoLstAttributeDataType_SelectedIndexChanged">
                                    <asp:ListItem Value="DATETIME" Text="DateTime"></asp:ListItem>
                                    <asp:ListItem Value="NUMERIC" Text="Numeric"></asp:ListItem>
                                    <asp:ListItem Value="LIST" Text="Selection List"></asp:ListItem>
                                    <asp:ListItem Value="TEXTAREA" Text="String(Multiline)"></asp:ListItem>
                                    <asp:ListItem Value="TEXTBOX" Text="String(Single Line)" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="YESNO" Text="Yes/No"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; text-align: left; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold">
                                <asp:Label ID="lblListSource" runat="server" Text="List Source:" Visible="false"></asp:Label>
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px">
                                <asp:DropDownList ID="ddlListSource" runat="server" Visible="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 11px; text-align: center; border-style: solid;
                                border-color: Silver; border-width: 1px">
                                <asp:Button ID="btnSaveAttribute" CssClass="button-css" runat="server" Text="Save" OnClick="btnSaveAttribute_Click" />
                                <asp:Button ID="Button2" CssClass="button-css" runat="server" Text="Cancel" OnClientClick="closeDivAddAttribute()" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="page-content" style="border: 1px solid gray; z-index: -2; overflow: auto;">
        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
            <ContentTemplate>
                <div class="error-message">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
                <div style="margin-left: auto; margin-right: auto; text-align: center;">
                    <table style="width: 100%">
                        <tr style="background-color: #e0e0e0">
                            <td style="width: 50%">
                                <a style="font-weight: bold; color: black;" href="javascript:showDivAddDocType()">Add
                                    New Document Type</a>
                            </td>
                            <td>
                                <a style="font-weight: bold; color: black;" href="javascript:showDivAddAttribute()">
                                    Create New Attribute</a>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table style="width: 100%">
                                 <tr>
                                    <td>
                                        Rank
                                    </td>
                                    <td class="data">
                                        <asp:DropDownList ID="ddlRank" runat="server" Width="156px" CssClass="control-edit required"  AutoPostBack="true" OnSelectedIndexChanged="ddlRank_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px">
                                            Search:
                                        </td>
                                        <td style="width: 150px">
                                            <asp:TextBox ID="txtSearchText" runat="server" AutoPostBack="true"></asp:TextBox>
                                        </td>
                                        <td style="width: 80px">
                                            Vessel Flag:
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rdoSearchVesselFlag" runat="server" DataSourceID="dsVesselFlag"
                                                RepeatDirection="Horizontal" DataTextField="Flag_Name" DataValueField="Vessel_Flag"
                                                AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Text="-All-" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 60%; text-align: right">
                                <asp:GridView ID="GridViewDocType" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource_DocTypes"
                                    DataKeyNames="DocTypeID" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                                    PageSize="20" CellPadding="2"  GridLines="None" Width="100%" 
                                    OnSelectedIndexChanged="GridViewDocType_SelectedIndexChanged" CssClass="gridmain-css" OnRowDataBound="GridViewDocType_RowDataBound" >
                                   <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                   <RowStyle CssClass="RowStyle-css" />
                                  <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />

                                    <Columns>
                                         <asp:TemplateField  >
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocTypeID" runat="server" Text='<%# Eval("DocTypeID")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td colspan="4" style="font-size: 12px; font-weight: bold;">
                                                            <asp:Label ID="lblDocTypeName" runat="server" Text='<%#Eval("DocTypeName")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 11px; color: #555;">
                                                        <td style="width: 20px">
                                                        </td>
                                                        <td>
                                                            LEGEND:<asp:Label ID="Label1" runat="server" Text='<%#Eval("Legend")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            Regn STCW’95 :
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Deck")%>'></asp:Label>
                                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Engine")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txtDocTypeName" Font-Size="11px" Width="200px" MaxLength="50" runat="server"
                                                                Text='<%#Bind("DocTypeName")%>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20px">
                                                        </td>
                                                        <td>
                                                            LEGEND:
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txtLegend" Font-Size="11px" Width="200px" MaxLength="50" runat="server"
                                                                Text='<%#Bind("Legend")%>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20px">
                                                        </td>
                                                        <td>
                                                            Regn STCW’95/Deck:
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txtDeck" Font-Size="11px" Width="200px" MaxLength="50" runat="server"
                                                                Text='<%#Bind("Deck")%>'></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="width: 20px">
                                                        </td>
                                                        <td>
                                                            Regn STCW’95/Engine:
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txtEngine" Font-Size="11px" Width="200px" MaxLength="50" runat="server"
                                                                Text='<%#Bind("Engine")%>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr><td>
                                                        </td>
                                                        <td style="vertical-align: top">
                                                            Vessel Flag:
                                                        </td>
                                                        <td colspan="2">                     
                                                            <asp:CheckBoxList ID="chkVesselFlagList" runat="server"  SelectionMode="Multiple" Font-Names="Tahoma"  DataTextField="Flag_Name" DataValueField="Vessel_Flag"  DataSourceID="dsVesselFlag"
                                                                        Font-Size="11px" >
                                                            </asp:CheckBoxList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Alert" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAlertDays" runat="server" Text='<%#Eval("AlertDays")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAlertDays" Font-Size="11px" Width="30px" MaxLength="50" runat="server"
                                                    Text='<%#Bind("AlertDays")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:CheckBoxField DataField="isDocCheckList" HeaderText="Required for SignOn" ItemStyle-HorizontalAlign="Center" />
                                         <asp:CheckBoxField DataField="Voyage" HeaderText="Voyage Specific"  ItemStyle-HorizontalAlign="Center" />
                                       
                                        <asp:CheckBoxField DataField="isExpiryMandatory" HeaderText="Expiry Mandatory" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Vessel Flag" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                 <asp:Label ID="lblVesselFlag" runat="server"  CssClass="linkbtn" Visible="false" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="btnAccept" runat="server" AlternateText="Update" CausesValidation="False"
                                                    CommandName="Update" ImageUrl="~/images/accept.png" />
                                                <asp:ImageButton ID="btnReject" runat="server" AlternateText="Cancel" CausesValidation="False"
                                                    CommandName="Cancel" ImageUrl="~/images/reject.png" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit" CausesValidation="False"
                                                    CommandName="Edit" ImageUrl="~/images/edit.gif" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left" ShowHeader="False">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete" CausesValidation="False" 
                                                    CommandName="Delete" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to delete the record?')" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:CommandField HeaderText="Attributes" ShowHeader="True" ShowSelectButton="True"
                                            SelectText="Attributes" />
                                    </Columns>
                                    <EditRowStyle BackColor="#efefef" />
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                 <%--   <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />--%>
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                            <td style="vertical-align: top; text-align: left; width: 40%;">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="font-weight: bold;">
                                            Selected Attributes:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" DeleteMethod="Remove_AttributeFromDocType"
                                                TypeName="SMS.Business.DMS.BLL_DMS_Admin" SelectMethod="Get_AssignedAttributesToTypeID"
                                                InsertMethod="Add_AttributeToDocType" OnDeleted="ObjectDataSource2_Deleted">
                                                <InsertParameters>
                                                    <asp:Parameter Name="ID" Type="Int32" />
                                                    <asp:Parameter Name="DocTypeID" Type="Int32" />
                                                    <asp:Parameter Name="AttributeID" Type="Int32" />
                                                </InsertParameters>
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="GridViewDocType" Name="DocTypeID" PropertyName="SelectedValue"
                                                        Type="Int32" />
                                                </SelectParameters>
                                                <DeleteParameters>
                                                    <asp:Parameter Name="ID" Type="Int32" />
                                                </DeleteParameters>
                                            </asp:ObjectDataSource>
                                            <asp:GridView ID="GridViewDocAttributeLinking" runat="server" AutoGenerateColumns="False"
                                                DataSourceID="ObjectDataSource2" EmptyDataText="No selected attributes" CaptionAlign="Bottom"
                                                DataKeyNames="ID" CellPadding="4" GridLines="Horizontal" Width="100%" 
                                                 BorderStyle="Double" BorderWidth="3px" CssClass="gridmain-css">
                                              <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                              <RowStyle CssClass="RowStyle-css" />
                                              <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                                <Columns>
                                                    <asp:BoundField DataField="AttributeName" HeaderText="Attribute Name" />
                                                    <asp:BoundField DataField="AttributeDataType" HeaderText="Attribute Type" />
                                                    <asp:TemplateField HeaderText="Required" ShowHeader="False" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkIsRequired" runat="server" AutoPostBack="true" Text='<%#Eval("ID") %>'
                                                                OnCheckedChanged="chkIsRequired_CheckedChanged" Width="10px" ForeColor="White"
                                                                Checked='<%#Eval("ISREQUIRED").ToString()=="1"?true:false %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Alert Days" ShowHeader="False" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAlertDays" runat="server" Text='<%#Eval("AlertDays") %>' AutoPostBack="true"
                                                                OnTextChanged="txtAlertDays_TextChanged" Width="50px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remove" ShowHeader="False" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkDelete" runat="server" ImageUrl="~/images/delete.png" CausesValidation="False"
                                                                CommandName="Delete" AlternateText="Remove"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                  
                                                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="White" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#487575" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#275353" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; color: Red;">
                                            Unselected Attributes:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridViewAttributes" runat="server" AutoGenerateColumns="False"
                                                DataSourceID="ObjectDataSource3" EmptyDataText="No un-selected attributes" DataKeyNames="AttributeID"
                                                CaptionAlign="Bottom" CellPadding="4" GridLines="Horizontal" OnSelectedIndexChanged="GridViewAttributes_SelectedIndexChanged"
                                                Width="100%"   BorderStyle="Double" BorderWidth="3px" CssClass="gridmain-css">
                                              <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                              <RowStyle CssClass="RowStyle-css" />
                                              <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                                <Columns>
                                                    <asp:BoundField DataField="AttributeName" HeaderText="Attribute Name" SortExpression="AttributeName" />
                                                    <asp:BoundField DataField="AttributeDataType" HeaderText="Attribute Type" SortExpression="AttributeDataType" />
                                                    <asp:TemplateField HeaderText="Edit" ShowHeader="False" ItemStyle-HorizontalAlign="Left">
                                                        <EditItemTemplate>
                                                            <asp:ImageButton ID="LinkButton1" runat="server" ImageUrl="~/images/accept.png" CausesValidation="False"
                                                                CommandName="Update" AlternateText="Update"></asp:ImageButton>
                                                            <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/reject.png" CausesValidation="False"
                                                                CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEdit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                                                CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkDelete" runat="server" ImageUrl="~/images/delete.png" CausesValidation="False"
                                                                CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                                                                AlternateText="Delete"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add" ShowHeader="False" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkSelect" runat="server" ImageUrl="~/images/add.gif" CausesValidation="False"
                                                                CommandName="Select" AlternateText="Add Attribute"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                    
                                                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="White" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#487575" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#275353" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; color: Red;">
                                            Mandatory for Ranks:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList ID="chkRankList" runat="server" RepeatColumns="4">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr style="background-color: #cfcfcf">
                                        <td>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select/DeSelect All" OnCheckedChanged="chkSelectAll_CheckedChanged"
                                                AutoPostBack="true" />
                                            <asp:Button ID="btnSaveMandatoryRank" runat="server" Text="Save List" CausesValidation="false"
                                                OnClick="btnSaveMandatoryRank_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
