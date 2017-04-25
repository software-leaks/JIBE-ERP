<%@ Page Title="DMS" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="DMS_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
    <script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify/swfobject.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../Styles/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
<%--    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>--%>
<script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
    
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#tabs").tabs();

            var CrewID = $('[id$=HiddenField_CrewID]').val();
            var CurrentUserID = $('[id$=HiddenField_UserID]').val();
            var strDocumentUploadpath = $('[id$=HiddenField_DocumentUploadPath]').val();

            crewDocumentUploadSettings(CrewID, strDocumentUploadpath, CurrentUserID);
            initAutoComplete('txtAttributeValue', 'COUNTRY');
        });
        function crewDocumentUploadSettings(CrewID_, strUploadpath_, UserID_) {
            $('#crewDocumentInput').uploadify({
                'uploader': '../scripts/uploadify/uploadify.swf',
                'script': '../UserControl/CrewDocumentUploader.ashx',
                'scriptData': { 'id': CrewID_, 'userid': UserID_, 'uploadpath': strUploadpath_ },
                'multi': true,
                'auto': true,
                'buttonText': 'Browse Files ...',
                'folder': '/Uploads/CrewDocuments',
                'cancelImg': '../scripts/uploadify/img/cancel.png',
                'onCancel': function (event, queueID, fileObj, data) { $('#dvPersonalDetails').show(); },
                'onAllComplete': function (event, queueID, fileObj, response, data) { $('#dvCrewDocumentUploader').hide(); $('[id$=ImgClearSearch]').trigger('click'); }
            });
        }
        function showDialog(dialog_) {
            $(dialog_).show('slow');
        };
        function showDivAddDocument() {
            document.getElementById("dvAddDocument").style.display = "block";
        }
        function closeDivAddDocument() {
            
            document.getElementById("dvAddDocument").style.display = "none";
        }
        function previewDocument(docPath) {
            //alert(docPath);
            document.getElementById("ifrmDocPreview").src = docPath;
        }
        function toggleLeftPanel(e_) {
            if (e_ == 1) {
                $('#tblSearch').hide();
                $('#tdLeftPanel').hide('slow');
                $('#dvLeftPanelExpand').show();
                $('#tdBottomPanel').hide('slow');

                setTimeout(document.getElementById('dvPreview').style.height = '600px', 3000);

            }
            else {
                $('#dvLeftPanelExpand').hide();
                $('#tblSearch').show();
                $('#tdLeftPanel').show('slow');
                $('#tdBottomPanel').show('slow');
                setTimeout(document.getElementById('dvPreview').style.height = '440px', 3000);
            }
        }
        function clearSearch() {
            $('[id$=txtSearchDoc]').val('');
        }

        
    </script>
    <script type="text/javascript">
        function initAutoComplete(controlid_, list_) {
            var options = { serviceUrl: '../UserControl/AutoComplete_Handler.ashx', params: { list: list_} };
            //$('.autocomplete').autocomplete(options);
            //$('[id$=' + controlid_ + ']').autocomplete(options);
            $('#' + controlid_).autocomplete(options);

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
    <div id="dvpageTitle" class="page-title">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="text-align: left; width: 33%">
                    <asp:Label ID="lblCrewName" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPageTitle" runat="server" Text="Document Library"></asp:Label>
                </td>
                <td style="color: Black; text-align: left; width: 33%">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="dvpage-content" class="page-content-main">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td id="tdLeftPanel" style="padding: 2px; vertical-align: top; width: 20%;">
                    <div id="dvLeftPanel">
                        <div id="tabs" style="margin-top: 0px; width: 250px; font-size: 10px;
                            display: block;">
                            <ul>
                                <li><a href="#fragment-0"><span>Documents</span></a></li>
                                <li><a href="#fragment-1"><span>Crew List</span></a></li>
                            </ul>
                            <div id="fragment-0" style="padding: 0px; display: block">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div style="border: 1px solid #BDBDBD; background-color: #F5EFFB; font-size: 10px;">
                                            <table id="tblSearch" style="width: 100%">
                                                <tr>
                                                    <td style="width: 100px;">
                                                        <asp:TextBox ID="txtSearchDoc" runat="server" Width="100" OnTextChanged="txtSearchDoc_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="ImgClearSearch" OnClick="ImgClearSearch_Click"
                                                            ImageUrl="~/Images/clear.gif" />
                                                        <asp:ImageButton runat="server" ID="ImgSearchDoc" OnClick="ImgSearchDoc_Click" ImageUrl="~/Images/search.gif" />
                                                    </td>
                                                    <td>
                                                        <%--<asp:ImageButton ID="imgUpload" ImageUrl="~/Images/DocumentUpload.png" runat="server"
                                                            Visible="false" OnClientClick="showDialog('#dvCrewDocumentUploader')" AlternateText="Upload Document" />--%>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <img src="../Images/Arrow2LeftGray.png" alt="dock/undock" style="cursor: hand" onclick="toggleLeftPanel(1);" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="border: 1px solid #BDBDBD; background-color: #F5EFFB; margin-top: 1px;
                                            padding: 2px;">
                                            <div id="docTreeContainer" style="background-color: #ffffff; padding: 2px; overflow: auto;
                                                 width: 240px;">
                                                <asp:TreeView ID="BrowseTreeView" runat="server" Style="margin-right: 1px" BorderColor="#F3F1CD"
                                                    Font-Bold="False" Font-Names="Tahoma" Font-Size="11px" ForeColor="Black" ImageSet="XPFileExplorer"
                                                    NodeIndent="15" AutoGenerateDataBindings="False" OnSelectedNodeChanged="BrowseTreeView_SelectedNodeChanged">
                                                    <ParentNodeStyle Font-Bold="False" />
                                                    <HoverNodeStyle Font-Underline="False" ForeColor="#6666AA" BackColor="#99FF66" />
                                                    <SelectedNodeStyle Font-Underline="False" ForeColor="#6666AA" BackColor="#99FF66" />
                                                    <NodeStyle Font-Names="Tahoma" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="1px"
                                                        VerticalPadding="1px" />
                                                </asp:TreeView>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div id="fragment-1" style="padding: 0px; display: block">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div style="border: 1px solid #BDBDBD; background-color: #F5EFFB; font-size: 10px;
                                            padding: 2px;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        Code
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtStaffCode" runat="server" Width="50px" AutoPostBack="true"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        Name
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtStaffName" runat="server" Width="50px" AutoPostBack="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div id="Div1" style="background-color: #ffffff; padding: 2px; overflow: auto; width: 240px;">
                                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get_Crewlist_ForDMS"
                                                    TypeName="SMS.Business.Crew.BLL_Crew_CrewDetails">
                                                    <SelectParameters>
                                                        <asp:QueryStringParameter Name="CrewID" QueryStringField="ID"  Type="Int32" DefaultValue="0" />                                                        
                                                        <asp:ControlParameter ControlID="txtStaffCode" Name="Staff_Code" PropertyName="Text" Type="String" />
                                                        <asp:ControlParameter ControlID="txtStaffName" Name="Staff_Name" PropertyName="Text" Type="String" />
                                                        <asp:SessionParameter Name="UserCompanyID" SessionField="UserCompanyID" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                    CellPadding="2" Width="100%"
                                                    EmptyDataText="No Record Found" CaptionAlign="Bottom" GridLines="Horizontal"
                                                    DataKeyNames="ID" AllowPaging="True" PageSize="15" Font-Size="11px" AllowSorting="True"
                                                    DataSourceID="ObjectDataSource1" ForeColor="Black" OnRowDataBound="GridView1_RowDataBound" CssClass="gridmain-css">
                                                      <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                      <RowStyle CssClass="RowStyle-css" />
                                                      <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSTAFF_CODE" runat="server" Text='<%# Eval("staff_Code")%>' CssClass="staffInfo"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="50px" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name" ItemStyle-CssClass="crewlink">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSTAFF_NAME" runat="server" Text='<%# Eval("staff_name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="130px" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rank">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#BFCFDF" Font-Bold="True" ForeColor="Black" />
                                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                                    <PagerStyle Font-Size="Larger" CssClass="pager" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </td>
                <td style="vertical-align: top; width: 0%">
                    <div id="dvLeftPanelExpand" style="display: none">
                        <img src="../Images/Arrow2RightGray.png" alt="dock/undock" style="cursor: hand" onclick="toggleLeftPanel(0);" /></div>
                </td>
                <td style="padding: 2px; vertical-align: top;">
                    <div id="dvPreview" style="border: 1px solid #BDBDBD; background-color: #ffffff;
                        font-size: 10px; height: 500px; overflow: auto;">
                        <div id="dvCrewDocumentUploader" style="text-align: right; display: none; float: left;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="HiddenField_DocumentUploadPath" runat="server" />
                                        <asp:HiddenField ID="HiddenField_CrewID" runat="server" />
                                        <asp:HiddenField ID="HiddenField_UserID" runat="server" />
                                        <input id="crewDocumentInput" name="crewDocumentInput" type="file" />
                                    </td>
                                    <td style="vertical-align: top">
                                        <img src="../scripts/uploadify/cancel.png" alt="Close" onclick="javascript: $('#dvCrewDocumentUploader').hide();" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <iframe id="ifrmDocPreview" src="" width="99%" height="99%" marginheight="0px" frameborder="0">
                        </iframe>
                    </div>
                </td>
            </tr>
            <tr>
                <td id="tdBottomPanel" colspan="3" style="padding: 1px; vertical-align: top; font-size: 11px;">
                    <div id="dvBottomPanel" style="height: 168px; overflow: hidden; border: 1px solid inset;
                        background-color: #efefef;">
                        <table cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td style="vertical-align: top; width: 60%;">
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:ObjectDataSource ID="ObjectDataSource_DocumentList" runat="server" 
                                                    TypeName="SMS.Business.Crew.BLL_Crew_CrewDetails"
                                                    DeleteMethod="DEL_Crew_DocumentByDocID"
                                                    SelectMethod="Get_CrewDocumentList"                                                     
                                                    UpdateMethod="UPDATE_CrewDocument">
                                                    <DeleteParameters>
                                                        <asp:Parameter Name="DocID" Type="Int32" />
                                                    </DeleteParameters>
                                                    <SelectParameters>
                                                        <asp:QueryStringParameter Name="CrewID" QueryStringField="ID" Type="Int32" DefaultValue="0" />
                                                        <asp:SessionParameter Name="UserCompanyID" SessionField="UserCompanyID" Type="Int32" />
                                                        <asp:ControlParameter ControlID="txtSearchDoc" DefaultValue="" Name="FilterString"
                                                            PropertyName="Text" Type="String" />
                                                        <asp:Parameter Name="DocTypeName" Type="String" DefaultValue="" />
                                                        <asp:Parameter Name="DocName" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <UpdateParameters>
                                                        <asp:Parameter Name="DocID" Type="Int32" />
                                                        <asp:Parameter Name="DocName" Type="String" />
                                                        <asp:Parameter Name="DocNo" Type="String" />
                                                        <asp:Parameter Name="SizeByte" Type="Int32" />
                                                        <asp:Parameter Name="DocTypeID" Type="Int32" />
                                                        <asp:Parameter Name="Modified_By" Type="Int32" />
                                                    </UpdateParameters>
                                                </asp:ObjectDataSource>
                                                <asp:GridView ID="GridView_Documents" runat="server" BackColor="White" BorderColor="White"
                                                   CellPadding="1" GridLines="None" CellSpacing="1"
                                                    AllowSorting="True" AllowPaging="true" PageSize="6" AutoGenerateColumns="False"
                                                    DataKeyNames="DocID" Width="100%" 
                                                    DataSourceID="ObjectDataSource_DocumentList"
                                                    OnRowUpdated="GridView_Documents_RowUpdated" 
                                                    OnRowDeleted="GridView_Documents_RowDeleted"
                                                    OnRowUpdating="GridView_Documents_RowUpdating" CssClass="RowStyle-css">
                                                  <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                  <RowStyle CssClass="RowStyle-css" />
                                                 <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Doc Type" SortExpression="DocNo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocTypeName" Text='<%#Eval("DocTypeName") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlEditDocType" runat="server" Text='<%# Bind("DocTypeID") %>'
                                                                    DataSourceID="ObjectDataSource1" DataTextField="DocTypeName" DataValueField="DocTypeID"
                                                                    AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get_DocTypeList"
                                                                    TypeName="SMS.Business.DMS.BLL_DMS_Admin"></asp:ObjectDataSource>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Name" SortExpression="DocName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocName" Text='<%#Eval("DocName") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtDocName" Text='<%#Bind("DocName") %>' runat="server" Width="80px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ver" SortExpression="Version">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVersion" Text='<%#Eval("Version") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Uploaded On" SortExpression="Date_Of_Creation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDtCreated" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_Of_Creation"))) %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Uploaded By" SortExpression="Created_By_Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedBy" Text='<%#Eval("Created_By_Name") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ShowHeader="False">                                                            
                                                            <ItemTemplate>
                                                                <img id="ImageButton1" src="../images/search.png" onclick="previewDocument('../Uploads/CrewDocuments/<%#Eval("DocFileName") %>')"
                                                                    alt="View in DMS" style="height: 15px; width: 15px;" />                                                                
                                                                <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                                                                    CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                                                                    AlternateText="Delete" Width="15px" Height="15px"></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />                                                
                                                    <PagerStyle Font-Size="11px" CssClass="pager" />
                                                    <RowStyle BackColor="#E6E6E6" ForeColor="Black" />
                                                    <AlternatingRowStyle BackColor="#FBEFF5" ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#33276A" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                                <td style="vertical-align: top; width: 40%;">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:ObjectDataSource ID="ObjectDataSource_DocAttributeValue" runat="server" SelectMethod="Get_DocAttributeValueByDocID"
                                                TypeName="SMS.Business.Crew.BLL_Crew_CrewDetails" UpdateMethod="UPDATE_DocumentAttributeValues">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="BrowseTreeView" Name="DocID" PropertyName="Target"
                                                        Type="Int32" DefaultValue="0" />
                                                </SelectParameters>
                                                <UpdateParameters>
                                                    <asp:Parameter Name="ID" Type="Int32" />
                                                    <asp:Parameter Name="AttributeValue_String" Type="String" />
                                                    <asp:Parameter Name="Modified_By" Type="Int32" />
                                                </UpdateParameters>
                                            </asp:ObjectDataSource>
                                            <asp:GridView ID="GridView_DocAttributes" runat="server" 
                                               CellPadding="2" Width="100%" AllowSorting="True"
                                                AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ObjectDataSource_DocAttributeValue"
                                                OnDataBound="GridView_DocAttributes_DataBound" OnRowDataBound="GridView_DocAttributes_RowDataBound" CssClass="gridmain-css">
                                               <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                               <RowStyle CssClass="RowStyle-css" />
                                               <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Attribute" SortExpression="ATTRIBUTENAME" ItemStyle-BorderColor="#dfdfdf"
                                                        ItemStyle-BorderStyle="Dotted" ItemStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAttributeName" runat="server" Text='<%# Eval("ATTRIBUTENAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle BorderColor="#DFDFDF" BorderStyle="Dotted" Width="150px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value" ItemStyle-BackColor="White" ItemStyle-BorderColor="#dfdfdf"
                                                        ItemStyle-BorderStyle="Dotted">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="HiddenField_ID" runat="server" Value='<%# Eval("ID") %>'></asp:HiddenField>
                                                            <asp:HiddenField ID="HiddenField_Type" runat="server" Value='<%# Eval("AttributeDataType") %>'>
                                                            </asp:HiddenField>
                                                            <asp:HiddenField ID="HiddenField_AttributeID" runat="server" Value='<%# Eval("AttributeID") %>'>
                                                            </asp:HiddenField>
                                                            <asp:HiddenField ID="HiddenField_ListSource" runat="server" Value='<%# Eval("ListSource") %>'>
                                                            </asp:HiddenField>
                                                            <asp:TextBox ID="txtAttributeValue" Text='<%#Eval("AttributeValue_String") %>' runat="server"
                                                                OnTextChanged="txtAttributeValue_TextChanged" AutoPostBack="true" CssClass="AttributeEditBox"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle BackColor="White" BorderColor="#DFDFDF" BorderStyle="Dotted" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField ItemStyle-BorderColor="#dfdfdf" ItemStyle-BorderStyle="Dotted"
                                                        ItemStyle-Width="50px">
                                                        <EditItemTemplate>
                                                            <asp:ImageButton ID="LinkButton1" runat="server" ImageUrl="~/images/accept.png" CausesValidation="True"
                                                                CommandName="Update" AlternateText="Update" ValidationGroup="noofdays"></asp:ImageButton>
                                                            &nbsp;<asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/reject.png"
                                                                CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                                                CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                        <ItemStyle BorderColor="#DFDFDF" BorderStyle="Dotted" Width="50px" />
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                             <%--   <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />--%>
                                                <RowStyle BackColor="#E6E6E6" ForeColor="Black" />
                                                <AlternatingRowStyle BackColor="#FBEFF5" ForeColor="Black" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#33276A" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
