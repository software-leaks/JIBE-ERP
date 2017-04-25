<%@ Page Title="Document Type" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Info_Folder.aspx.cs" Inherits="Info_Folder" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
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
        function showDivAddFolder() {
            showModal('dvAddFolder', true, dvAddFolder_onClose);
        }
        function dvAddFolder_onClose() {

        }

        function showDivAddAttribute() {

            showModal('dvAddAttribute', true, dvAddAttribute_onClose);
        }
        function dvAddAttribute_onClose() {

        }

        function closeDivAddDocType() {

            hideModal('dvAddFolder');
        }

        function closeDivAddAttribute() {

            hideModal('dvAddAttribute');
        }

        function OpenQueryBuilder() {
            var url = "querybuilder.aspx";
            window.open(url, "_blank");
        }

        function OpenUserRights(ID, Name,DeptId) {
            var url = 'User_Rights.aspx?i=' + ID + '&ii=' + Name + '&iii=' + DeptId;
            OpenPopupWindowBtnID('UserRights', 'Infobase: User Rights', url, 'popup', 800, 1200, null, null, false, false, true, null, 'ctl00_MainContent_btnRefresh');
        }

        function ValidateList() 
        {
            var rad = document.getElementById('<%=rdoLstAttributeDataType.ClientID %>');
            var radio = rad.getElementsByTagName("input");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked && radio[i].value == "LIST")
                {
                    if (document.getElementById("ctl00_MainContent_ddlListSource").value == "0") 
                    {
                        alert("Please select a list source !");
                        return false;

                    }

                }
                }
                return true;
      }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <div class="page-title">
      Document Folders
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



     <div id="dvAddFolder" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
        border-style: solid; border-width: 1px; width: 450px; position: absolute; left: 40%;
        top: 15%; z-index: 1; color: black" class="ui-widget-content" title="Add New Document Type">    
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="border-style: solid; border-color: Silver; border-width: 1px; width: 100%;">
                    <tr>
                    
                        <td style="font-size: 12px; text-align: right; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold">
                        Department :
                        </td>
                        <td align="left">
                        <asp:DropDownList ID="ddlAddDepartment" runat="server" Width="156px" CssClass="control-edit required">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlAddDepartment" ErrorMessage="Department Required!" Display="None"  InitialValue="0" ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
                        </td>
                    
                    </tr>
                       
                        <tr>
                             <td style="font-size: 12px; text-align: right; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold">
                                Folder Name:
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px">
                                <asp:TextBox ID="txtFolderName"  Width="200px" MaxLength="200" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFolder" runat="server" ControlToValidate="txtFolderName" ErrorMessage="Folder Name Required!" Display="None"   ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td  style="font-size: 12px; text-align: right; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold">
                            Source Table :

                            </td>
                            <td  align="left">
                            <asp:DropDownList ID="ddlTableList" Width="200px" MaxLength="200" runat="server"></asp:DropDownList>
                              <asp:RequiredFieldValidator ID="rfvTable" runat="server" ControlToValidate="ddlTableList" ErrorMessage="Source Table Required!" Display="None"  InitialValue="0" ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
                            </td>
                        
                        </tr>
                        <tr>
                        <td colspan="2" style="font-style:italic;color:Red" align="right">
                        **Source table & link source  must be linked .
                        
                         </td>
                        </tr>
                        <tr>
                          <td style="font-size: 12px; text-align: right; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold">
                                Link Display Name:
                            </td>
                        <td style="border-style: solid; border-color: Silver; border-width: 1px">
                        
                           <asp:TextBox ID="txtLinkDisplay"  Width="200px" MaxLength="100" runat="server"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="rfvLinkDisplay" runat="server" ControlToValidate="txtLinkDisplay" ErrorMessage="Link display name Required!" Display="None"   ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
  
                        </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; text-align: right; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold">
                                <asp:Label ID="lblSource" runat="server" Text="Link Source:" ></asp:Label>
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px">
                                <asp:DropDownList ID="ddlListSource1" runat="server"  onselectedindexchanged="ddlListSource1_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="rfvListSource" runat="server" ControlToValidate="ddlListSource1" ErrorMessage="List Source Required!" Display="None"  InitialValue="0" ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                        <td style="border-style: solid; border-color: Silver; border-width: 1px">
                        <asp:Label ID="lblLinkValue" runat="server" Text="Value Field:" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlList1Value" runat="server" Visible="false">
                                </asp:DropDownList>
                            </td>
                         <td style="border-style: solid; border-color: Silver; border-width: 1px">
                         <asp:Label ID="lblLinkText" runat="server" Text="Text Field:" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlList1Text" runat="server" Visible="false">
                                </asp:DropDownList>
                            </td>
                        </tr>


                        <tr>
                            <td colspan="2" style="font-size: 11px; text-align: center; border-style: solid;
                                border-color: Silver; border-width: 1px">
                                <asp:Button ID="btnSaveFolder" runat="server" Text="Save"  ValidationGroup="ValidateSave"
                                    OnClick="btnSaveFolder_Click" />
                                <asp:Button ID="btnCancel" CssClass="button-css" runat="server" Text="Cancel" OnClientClick="closeDivAddDocType()" />
                                <asp:ValidationSummary ID="VSummary" runat="Server" ShowMessageBox="true" ShowSummary="false"  DisplayMode="List"  ValidationGroup="ValidateSave" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="dvAddAttribute" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
        border-style: solid; border-width: 1px; width: 600px; position: absolute; left: 40%;
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
                                <asp:TextBox ID="txtAttribute"  runat="server"></asp:TextBox>
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
                                <asp:DropDownList ID="ddlListSource" runat="server"  onselectedindexchanged="ddlListSource_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                        <td style="border-style: solid; border-color: Silver; border-width: 1px">
                        <asp:Label ID="lblData" runat="server" Text="Value Field:" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlValuefield" runat="server" Visible="false">
                                </asp:DropDownList>
                            </td>
                         <td style="border-style: solid; border-color: Silver; border-width: 1px">
                         <asp:Label ID="lblText" runat="server" Text="Text Field:" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlTextfield" runat="server" Visible="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                            <tr>   
                            <td style="font-size: 11px; text-align: left; border-style: solid; border-color: Silver;
                                border-width: 1px; font-weight: bold">
                                Is Required:
                            </td>
                            <td style="border-style: solid; border-color: Silver; border-width: 1px">
                                <asp:CheckBox ID="chkIsRequired"  runat="server" />
                                   
                            </td>
                        
                        
                        
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 11px; text-align: center; border-style: solid;
                                border-color: Silver; border-width: 1px">
                                <asp:Button ID="btnSaveAttribute" CssClass="button-css" runat="server" OnClientClick ="return ValidateList();" Text="Save" OnClick="btnSaveAttribute_Click" />
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
                        <tr>
                            <td colspan="2">
                                <table style="width: 100%">
                                 <tr>
                                    <td width="10%" align="right">
                                        Department :
                                    </td>
                                    <td width="20%" align="left">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="156px" CssClass="control-edit required"  AutoPostBack="true" >
                                        </asp:DropDownList>
                                    </td>
                                        <td style="width:10%" align="right">
                                            Search:
                                        </td>
                                        <td style="width:10%" align="left">
                                            <asp:TextBox ID="txtSearchText" runat="server" MaxLength="20" AutoPostBack="true"></asp:TextBox>
                                        </td>
                                    <td style="width: 10%" align="left">
                                        <asp:ImageButton ID="btnSearch" runat="server" ToolTip="Search"  CausesValidation="false"
                                            ImageUrl="~/Images/SearchButton.png" Text="Search" OnClick="btnSearch_Click" />
                                &nbsp;
                                      <asp:ImageButton ID="btnNew" runat="server" Text="Create New Folder" 
                                            ToolTip="Create New Folder" OnClientClick="showDivAddFolder();" 
                                            ImageUrl="~/Images/Add-icon.png"/>
                                      &nbsp;
                                      <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Refresh-icon.png" CausesValidation="false"
                                           ToolTip="Refresh" OnClick="btnRefresh_Click" />
                                    </td>
                                    <td>
                                     <img src="../images/wizard/database-process-icon.png" style="vertical-align: bottom" alt="Query Builder" OnClick="OpenQueryBuilder();" height="20px" />
                                    
                                    </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 60%; text-align: right">
                                <asp:GridView ID="GridViewDocType" runat="server" AutoGenerateColumns="False" GridLines="Both"
                                    DataKeyNames="ID" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                                    PageSize="20" CellPadding="2"  Width="100%" 
                                    OnSelectedIndexChanged="GridViewDocType_SelectedIndexChanged" CssClass="gridmain-css"  >
                                   <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                   <RowStyle CssClass="RowStyle-css" />
                                  <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />

                                    <Columns>
                                         <asp:TemplateField  Visible="false" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocTypeID" runat="server" Text='<%# Eval("FOLDER_ID")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField  HeaderText="S.No" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNO" runat="server" Text='<%# Eval("ROWNUM")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Folder" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                             <asp:Label ID="lblFolderName" runat="server" Text='<%#Eval("FOLDER_NAME")%>'></asp:Label>

                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" Width="15%"/>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Department" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                             <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("DeptName")%>'></asp:Label>

                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left"  Width="15%" />
                                        </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Created By" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                             <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>

                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" width="15%"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Rights" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>

                                        <asp:ImageButton ID="ibtnUserrights" runat="server" ImageUrl="../Images/Users.gif"
                                         OnClientClick='<%#"OpenUserRights((&#39;"+ Eval("ID") +"&#39;),(&#39;"+ Eval("FOLDER_NAME") + "&#39;),(&#39;"+ Eval("Department_ID") + "&#39;));return false;"%>'
                                         />
                                        
                                        </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Access" ItemStyle-HorizontalAlign="Left" ShowHeader="False">
                                            <ItemTemplate>
                                             <asp:Label ID="lblAccess" runat="server" Text="Access To"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Attribute">
                                        
                                        <ItemTemplate>
                                           <asp:ImageButton ID="ibtnAttribute" style="border: 0; width: 14px; height: 14px"   OnCommand="ibtnAttribute_Click"
                                            CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Attributes" CausesValidation="false"
                                            ImageUrl="../Images/AdhocDrill.png" runat="server" />&nbsp;
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="10%"/>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left" ShowHeader="False">
                                            <ItemTemplate>
                                            <asp:ImageButton ID="ibtnEdit" runat="server" OnCommand="ibtnEdit_Click" CommandArgument='<%#Eval("ID")%>' 
                                            ToolTip="Edit Folder"  ImageUrl="~/Images/Edit.gif"/>
                                                <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete" CausesValidation="False" 
                                                 OnCommand="ibtnDeleteFolder_Click" CommandArgument='<%#Eval("ID")%>' 
                                                 ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure, you want to delete the record?')" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#efefef" />
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPager1" runat="server" PageSize="10" OnBindDataItem="BindGrid" />
                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                            </td>
                            <td style="vertical-align: top; text-align: left; width: 40%;">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="font-weight: bold;">
                                            Selected Attributes:
                                        </td>
                                    </tr>
                                    <tr>
                                    <td align="right">
                                       <asp:ImageButton ID="ibtnAddAttribute" runat="server" Text="Create New Attribute" Width="20px" Height="20px" Visible="false" ToolTip="Add New Attribute"
                                        OnClientClick="showDivAddAttribute();" ImageUrl="~/Images/AddMaker.gif" />

                                    </td>

                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridViewAttributes" runat="server" AutoGenerateColumns="False"
                                             EmptyDataText="No un-selected attributes" DataKeyNames="Attribute_ID,Folder_Id" 
                                             onrowediting="EditAttribute" onrowupdating="UpdateAttribute"  onrowcancelingedit="CancelEdit"

                                                CaptionAlign="Bottom" CellPadding="4" GridLines="Horizontal"
                                                Width="100%"   BorderStyle="Double" BorderWidth="3px" CssClass="gridmain-css">
                                              <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                              <RowStyle CssClass="RowStyle-css" />
                                              <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                                <Columns>
                                                <asp:TemplateField HeaderText="Attribute" ShowHeader="False" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAttributeName" runat="server" Text='<%#Eval("AttributeName") %>'></asp:Label>
                                                            
                                                         </ItemTemplate>
                                                         <EditItemTemplate>
                                                         
                                                         <asp:TextBox ID="txtAttributName" Text='<%#Eval("AttributeName") %>' runat="server" MaxLength="100"></asp:TextBox>
                                                         <asp:RequiredFieldValidator ID="rfvAttributeName" runat="server" ValidationGroup="vdUpdateAttribute" ControlToValidate="txtAttributName" ErrorMessage="Name Required!" Display="None"></asp:RequiredFieldValidator>
                                                         </EditItemTemplate>
                                                         
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Attribute Type"  ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAttributeType" runat="server" Text='<%#Eval("AttributeDataType") %>'></asp:Label>
                                                            
                                                         </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>


                                                     <asp:TemplateField HeaderText="Source"  ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblListsource" runat="server" Text='<%#Eval("ListSource") %>'></asp:Label>
                                                            
                                                         </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    
                                                   <asp:TemplateField HeaderText="Required"  ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIsRequired" runat="server" Text='<%#Eval("StrIsRequired") %>'></asp:Label>
                                                            
                                                         </ItemTemplate>
                                                         <EditItemTemplate>
                                                         
                                                         <asp:CheckBox ID="chkRequired" Checked='<%#Eval("IsRequired") %>' runat="server" />
                                                         </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Edit" ShowHeader="False" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEdit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                                              CommandName="Edit"
                                                                 AlternateText="Edit"></asp:ImageButton>
                                                        </ItemTemplate>
                                                            <EditItemTemplate>
                                                            <asp:ImageButton ID="LinkButton1" runat="server" ImageUrl="~/images/accept.png" ValidationGroup="vdUpdateAttribute"
                                                                CommandName="Update"  AlternateText="Update"></asp:ImageButton>
                                                            <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/reject.png" CausesValidation="False"
                                                                CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                                                <asp:ValidationSummary ID="vsUpdate" runat="server" ValidationGroup="vdUpdateAttribute" ShowSummary="false" ShowMessageBox="true" DisplayMode="List" />
                                                        </EditItemTemplate>

                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibtnDeleteAttribute" runat="server" ImageUrl="~/images/delete.png" CausesValidation="False"
                                                             OnCommand="ibtnDeleteAttribute_Click" CommandArgument='<%#Eval("[Attribute_ID]")%>' 
                                                                 OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                                                                AlternateText="Delete"></asp:ImageButton>
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

                                </table>
                            </td>
                        </tr>
                    </table>
                </div>

     
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
