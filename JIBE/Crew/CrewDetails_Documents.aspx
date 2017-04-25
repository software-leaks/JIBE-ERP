<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_Documents.aspx.cs"
    Inherits="Crew_CrewDetails_Documents" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        function DateValidation() {
            if (document.getElementById('txtDocIssueDate').value != "" && document.getElementById('txtDocExpiryDate').value != "") {
                var d1 = new Date(document.getElementById('txtDocIssueDate').value);
                var d2 = new Date(document.getElementById('txtDocExpiryDate').value);
                if (d1 > d2)
                    alert('Issue date cannot be greater than Expiry Date');
            }

        }
    </script>
    <script type="text/javascript">

        function FreezeGridView() {
            var ScrollHeight = 380;
            //    window.onload = function FreezeGridView() {
            var grid = document.getElementById("GridView_Documents");
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();
            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }
            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];
            for (var i = 0; i < cells.length; i++) {
                var width;
                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }
                cells[i].style.width = parseInt(width - 3) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 415px;
        }
        
        .TreeNd:hover
        {
            background-color: #99FF66;
            color: #6666AA !important;
        }
        .HeaderStyle-css th a
        {
            text-decoration: none;
        }
        .noRecordFound
        {
            height: 25px;
            text-align: center;
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <div id="dvCrewDocuments" style="height: 434px">
        <asp:UpdatePanel ID="UpdatePanel_Documents" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnDocVoyageID" runat="server" Value="0" />
                <div class="error-message" onclick="javascript:this.style.display='none';">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <div id="fragment-4-tool" style="text-align: right;">
                    <img src="../Images/OpenInDMS.png" alt="" onclick="javascript:window.open('../DMS/Default.aspx?ID=<%=GetCrewID() %>', '_blank');return false;" />
                    <asp:ImageButton runat="server" ID="ImgAddDocument" ImageUrl="~/Images/AddPerpetualDoc.png"
                        OnClick="ImgAddDocument_Click" />
                    <asp:ImageButton runat="server" ID="ImgReloadDocuments" ImageUrl="~/Images/reload.png"
                        OnClick="ImgReloadDocuments_Click" />
                </div>
                <asp:Panel ID="pnlDocuments" runat="server" CssClass="class-doc-list" Visible="true">
                    <table style="width: 100%; height: 408px">
                        <tr>
                            <td style="vertical-align: top; width: 25%;">
                                <div class="HeaderStyle-css" style="height: 23px; padding-top: 1px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtSearchDoc" runat="server" Width="135" OnTextChanged="txtSearchDoc_TextChanged"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton runat="server" ID="ImgClearSearch" OnClick="ImgClearSearch_Click"
                                                    ImageUrl="~/Images/Refresh-icon.png" Style="display: none;" />
                                                <asp:ImageButton runat="server" ID="ImgSearchDoc" OnClick="ImgSearchDoc_Click" ImageUrl="~/Images/searchbutton.png" />
                                            </td>
                                            <td align="right" class="style1">
                                                <asp:CheckBox ID="chkArchive" runat="server" Text="Show archived documents" OnCheckedChanged="chkArchive_CheckedChanged"
                                                    AutoPostBack="true" Font-Size="10px" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="margin-top: 1px; padding: 2px; overflow: auto; height: 365px;">
                                    <asp:TreeView ID="BrowseTreeView" runat="server" Style="margin-top: 0px; margin-right: 1px"
                                        BorderColor="#F3F1CD" Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"
                                        ForeColor="Black" BackColor="#ffffff" ImageSet="XPFileExplorer" NodeIndent="10"
                                        AutoGenerateDataBindings="False" OnSelectedNodeChanged="BrowseTreeView_SelectedNodeChanged">
                                        <ParentNodeStyle Font-Bold="False" />
                                        <HoverNodeStyle Font-Underline="False" ForeColor="#6666AA" BackColor="#99FF66" />
                                        <SelectedNodeStyle Font-Underline="False" ForeColor="#6666AA" BackColor="#99FF66" />
                                        <NodeStyle Font-Names="Tahoma" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="1px"
                                            VerticalPadding="1px" CssClass="TreeNd" />
                                    </asp:TreeView>
                                </div>
                            </td>
                            <td style="vertical-align: top; width: 75%;">
                                <asp:Panel ID="pnlView_Documents" runat="server" CssClass="class-doc-list" Visible="true">
                                    <asp:GridView ID="GridView_Documents" runat="server" CellPadding="2" CellSpacing="1"
                                        GridLines="None" Width="100%" AllowSorting="True" AutoGenerateColumns="False"
                                        DataKeyNames="DocID" OnRowDeleting="GridView_Documents_RowDeleting" CssClass="GridView-css"
                                        BorderWidth="1" OnSorting="GridView_Documents_Sorting" EmptyDataText="No Record Found"
                                        EmptyDataRowStyle-CssClass="noRecordFound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Document Type" SortExpression="DocTypeName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocTypeName" Text='<%#Eval("DocTypeName") %>' runat="server" Style="padding: 2px 5px 2px"
                                                        BackColor='<%# Eval("ExpiryValidation").ToString() == "1" ? System.Drawing.Color.FromArgb(243, 87, 82) : System.Drawing.Color.Transparent %>'
                                                        ForeColor='<%# Eval("ExpiryValidation").ToString() == "1" ? System.Drawing.Color.White : System.Drawing.Color.Black %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Group" SortExpression="GroupName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGroupName" Text='<%#Eval("GroupName") %>' runat="server" ForeColor="black"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Document No" SortExpression="DocNo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocNo" Text='<%#Eval("DocNo") %>' runat="server" ForeColor="black"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Issue Date" SortExpression="DateOfIssue">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDtOfIssue" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DateOfIssue"))) %>'
                                                        runat="server" ForeColor="black"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Expiry Date" SortExpression="DateOfExpiry">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExpiryDate" Style="padding: 2px 5px 2px" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DateOfExpiry"))) %>'
                                                        runat="server" BackColor='<%# Eval("ExpiryValidation").ToString() == "1" ? System.Drawing.Color.FromArgb(243, 87, 82) : System.Drawing.Color.Transparent %>'
                                                        ForeColor='<%# Eval("ExpiryValidation").ToString() == "1" ? System.Drawing.Color.White : System.Drawing.Color.Black %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Voyage" SortExpression="voyage_Name">
                                                <ItemTemplate>
                                                    <%# BindVoyageField(Convert.ToString(Eval("voyage_Name")))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <img id="ImageButton1" src="../images/search.png" onclick="javascript:window.open('../DMS/Default.aspx?ID=<%=GetCrewID()%>&DocID=<%#Eval("DocID") %>','_blank')"
                                                        alt="View in DMS" />
                                                    <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                                                        CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                                                        AlternateText="Delete"></asp:ImageButton>
                                                    <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                        Style="margin-top: -7px;" Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;DMS_DTL_DOCUMENT&#39;,&#39;DocID="+Eval("DocID").ToString()+"&#39;,event,this);" %>'
                                                        AlternateText="info" />
                                                </ItemTemplate>
                                                <ItemStyle Width="60" />
                                                <HeaderStyle     Width="60" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="HeaderStyle-css" ForeColor="black" />
                                        <PagerStyle CssClass="PagerStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" ForeColor="#3498DB" />
                                        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" ForeColor="#3498DB" />
                                        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" ForeColor="#3498DB" />
                                        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" ForeColor="#3498DB" />
                                    </asp:GridView>
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
                                </asp:Panel>
                                <asp:Panel ID="pnlDoc_Attributes" runat="server" CssClass="class-doc-list" Visible="false">
                                    <table>
                                        <tr>
                                            <td style="width: 150px">
                                                Document Name
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDocumentName" runat="server" Width="250px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                                Document No.
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDocNo" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                                Place Of Issue
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDocIssuePlace" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                                Country Of Issue
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCountry" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                                Issue Date
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDocIssueDate" runat="server" onchange="DateValidation()" ClientIDMode="Static"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtDocIssueDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                                Expiry Date
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDocExpiryDate" runat="server" onchange="DateValidation()" ClientIDMode="Static"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtDocExpiryDate">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="rptDocAttributes" runat="server" DataSourceID="ObjectDataSource_DocAttributeValue"
                                            OnItemDataBound="rptDocAttributes_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblAttributeName" runat="server" Text='<%# Eval("ATTRIBUTENAME") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="HiddenField_ID" runat="server" Value='<%# Eval("ID") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="HiddenField_Type" runat="server" Value='<%# Eval("AttributeDataType") %>'>
                                                        </asp:HiddenField>
                                                        <asp:HiddenField ID="HiddenField_AttributeID" runat="server" Value='<%# Eval("AttributeID") %>'>
                                                        </asp:HiddenField>
                                                        <asp:HiddenField ID="HiddenField_ListSource" runat="server" Value='<%# Eval("ListSource") %>'>
                                                        </asp:HiddenField>
                                                        <asp:TextBox ID="txtAttributeValue" Text='<%#Eval("AttributeValue_String") %>' runat="server"
                                                            AutoPostBack="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td style="width: 150px">
                                                Document Type
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDocType" runat="server" Width="250px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlDocType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                                Replace Existing
                                            </td>
                                            <td colspan="2">
                                                <asp:Button ID="btnSaveDocType" Text="Save" runat="server" OnClick="btnSaveDocType_Click"
                                                    ClientIDMode="Static" />
                                                <asp:Button ID="btnSaveAndReplaceDocType" Text="Save and Replace Existing" runat="server"
                                                    OnClientClick="return confirm('Are you sure, you want to replace existing document for the current voyage with this document?')"
                                                    OnClick="btnSaveAndReplaceDocType_Click" ClientIDMode="Static" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                            </td>
                                            <td colspan="2" style="text-align: left">
                                                <asp:HyperLink ID="lnkCrewDocument" runat="server" NavigateUrl="" Target="_blank"><img src="../Images/ViewDocument.png"  alt="View Document in DMS" style="border:0"/></asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
<script type="text/javascript">
    var strDateFormat = '<%=DFormat %>';
    var CurrentDateFormatMessage = '<%= UDFLib.DateFormatMessage() %>';
    $(document).ready(function () {

        FreezeGridView();

        $("body").on("click", "#btnSaveDocType,#btnSaveAndReplaceDocType", function () {
            var msg = "";
            if ($("#txtDocIssueDate").length > 0) {
                var date1 = document.getElementById("txtDocIssueDate").value;
                if ($.trim($("#txtDocIssueDate").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        msg = "Enter valid Issue Date" + CurrentDateFormatMessage + "\n";
                    }
                }
            }
            if ($("#txtDocExpiryDate").length > 0) {
                var date1 = document.getElementById("txtDocExpiryDate").value;
                if ($.trim($("#txtDocExpiryDate").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        msg += "Enter valid Expiry Date" + CurrentDateFormatMessage;
                    }
                }
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
        });
    });    
</script>
</html>
