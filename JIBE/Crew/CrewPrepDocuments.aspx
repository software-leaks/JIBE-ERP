<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewPrepDocuments.aspx.cs"
    Inherits="Crew_CrewPrepDocuments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
        body
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        .FixedHeader
        {
            position: absolute;
            font-weight: bold;
        }
        .FixedHeader
        {
            background-color: #5d7b9d !important;
        }
        #GridView1 tr:hover
        {
            background-color: #feecec !important;
        }
    </style>
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        function BindHeight() {
            parent.$("#frPopupFrame").css("height", parseInt($("#page-content").height()) + 40);
            parent.$("#dvPopupFrame").css("height", parseInt($("#page-content").height()) + 40);
        }

        $(document).ready(function () {
            BindHeight();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="page-content">
        <div class="error-message">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <asp:HiddenField ID="hdnCrewID" runat="server" Value="0" />
        <div>
        </div>
        <div class="NoPrint" style="left: 97%; position: absolute; text-align: right; top: 10px;">
            <style type="text/css" media="print">
                .NoPrint
                {
                    display: none;
                }
                #pgHeader
                {
                    color: Black;
                }
                #tblCheckList
                {
                    border-width: 1px;
                }
            </style>
            <img src="../Images/Printer.png" style="cursor: pointer;" title="*Print*" onclick="PrintDiv1('Doc-page-content')" />
        </div>
        <center>
            <table cellspacing="0" cellpadding="0" rules="rows" class="dataTable" style="width: 100%;">
                <tr class="NoPrint">
                    <td>
                        <table width="65%">
                            <tr>
                                <td>
                                    Crew Name
                                </td>
                                <td style="width: 200px">
                                    <asp:Label ID="lblCrewName" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                </td>
                                <td>
                                    Rank
                                </td>
                                <td>
                                    <asp:Label ID="lblRank" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Document Group
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGroup" runat="server" Width="156px" CssClass="control-edit"
                                        AutoPostBack="true" OnSelectedIndexChanged="LoadDocumentList">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Search Document
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchText" runat="server" ForeColor="Black" Font-Bold="true"
                                        OnTextChanged="txtSearchText_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="Doc-page-content">
                        <table width="100%" style="border-collapse: collapse;" id="tblDocument">
                            <tr style="background-color: #5d7b9d !important; color: White; height: 25px;">
                                <th align="left" scope="col" style="width: 205px;">
                                    Document
                                </th>
                                <th align="left" scope="col" style="width: 70px;">
                                    Group
                                </th>
                                <th align="left" scope="col" style="width: 155px;">
                                    Doc.No
                                </th>
                                <th align="left" scope="col" style="width: 90px;">
                                    Issue Date
                                </th>
                                <th align="left" scope="col" style="width: 71px;">
                                    Expiry Date
                                </th>
                                <th align="left" scope="col" style="width: 92px;">
                                    Issue Place
                                </th>
                                <th align="left" scope="col" style="width: 90px;">
                                    Issue Country
                                </th>
                                <th class="NoPrint" align="center" scope="col" style="width: 16px;">
                                    &nbsp;
                                </th>
                                <th class="NoPrint" scope="col" style="width: 90px;">
                                    &nbsp;
                                </th>
                            </tr>
                        </table>
                        <div id="Div1" style="z-index: -2; overflow: auto; text-align: left; max-height: 470px">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                HeaderStyle-CssClass="FixedHeader" AllowSorting="True" DataKeyNames="DocTypeID"
                                CellPadding="4" ForeColor="#333333" GridLines="None" OnRowEditing="GridView1_RowEditing"
                                OnRowDataBound="GridView1_RowDataBound" ShowHeader="false" EmptyDataText="No Record Found"
                                EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Document" HeaderStyle-Width="300px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("DocTypeName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="220px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocGroupName" runat="server" Text='<%#Eval("GroupName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="70px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Doc.No" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocNo" runat="server" Text='<%#Eval("DocNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="155px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Date" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssueDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DateOfIssue"))) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expiry Date" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DateOfExpiry"))) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="71px"></HeaderStyle>
                                        <ItemStyle Width="71px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Place" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssuePlace" runat="server" Text='<%#Eval("PlaceOfIssue") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="92px"></HeaderStyle>
                                        <ItemStyle Width="92px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Country" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCountry" runat="server" Text='<%#Eval("Country_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Width="90px"></HeaderStyle>
                                        <ItemStyle Width="90px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false" HeaderStyle-Width="16px">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="ImgAttachment" runat="server" ImageUrl="~/images/Attach.png" Height="16px"
                                                Width="16px" BorderStyle="None" CssClass="noborder" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemStyle CssClass="NoPrint" Width="16" />
                                        <HeaderStyle CssClass="NoPrint" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False" HeaderStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/DocumentUpLoad.png"
                                                Height="16px" Width="47px" CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="NoPrint" Width="70px" />
                                        <HeaderStyle CssClass="NoPrint" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#58FAAC" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr style="height: 10px;" class="NoPrint">
                    <td>
                    </td>
                </tr>
                <tr id="trEdit" runat="server" visible="false" class="NoPrint">
                    <td>
                        <table width="100%">
                            <tr style="background-color: #5D7B9D !important; font-weight: bold; text-align: left;
                                height: 20px; color: white">
                                <td>
                                    Document
                                </td>
                                <td>
                                    Group
                                </td>
                                <td>
                                    Doc.No
                                </td>
                                <td>
                                    Issue Date
                                </td>
                                <td>
                                    Expiry Date
                                </td>
                                <td>
                                    Issue Place
                                </td>
                                <td colspan="2">
                                    Issue Country
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <asp:Label ID="lblDocName" runat="server"></asp:Label>
                                    <asp:Label ID="lblDocTypeId" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr style="background-color: #58FAAC; text-align: left; color: black">
                                <td>
                                    <asp:FileUpload ID="docUploader" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDocGroupName" runat="server" Width="120px" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDocNo" runat="server" Width="90px" Text='<%#Bind("DocNo") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIssueDate" runat="server" Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtIssueDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExpDate" runat="server" Width="110px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calExpDate" runat="server" TargetControlID="txtExpDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIssuePlace" runat="server" Width="110px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCountry" runat="server" Width="110px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/images/accept.png" Width="13px"
                                        AlternateText="Update" ToolTip="Save" CausesValidation="True" OnClick="btnSave_Click">
                                    </asp:ImageButton>
                                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/images/reject.png" Width="12px"
                                        OnClick="btnCancel_Click" AlternateText="Cancel" ToolTip="Cancel"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    <script type="text/javascript">

        function PrintDiv1(dvID) {
            $("#Div1").removeAttr("style");
            $(".NoPrint").css("display", "none");
            var a = window.open('', '', 'left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
            a.document.write(document.getElementById("page-content").innerHTML);
            a.document.close();
            a.focus();
            a.print();
            a.close();
            $(".NoPrint").css("display", "");
            $("#Div1").attr("style", "z-index: -2;overflow: auto;text-align: left;height: 400px;");
            return false;
        }

        $(document).ready(function () {

            var strDateFormat = "<%= DateFormat %>";
            var CurrentDateFormat = '<%= UDFLib.DateFormatMessage() %>';

            $("body").on("click", "#btnSave", function () {
                return MandatoryCheck();
            });



            function MandatoryCheck() {
                var Msg = "";
                if ($("[id$='txtIssueDate']").val() == "") {
                    alert('Issue date is mandatory');
                    return false;
                }
                if (IsInvalidDate($("#txtIssueDate").val(), strDateFormat)) {
                    Msg += "Enter valid Issue Date" + CurrentDateFormat + "\n";
                }
                if ($("[id$='txtExpDate']").val() != "") {
                    if (IsInvalidDate($("#txtExpDate").val(), strDateFormat)) {
                        Msg += "Enter valid Expiry Date" + CurrentDateFormat + "\n";
                    }
                }
                if ($("#txtIssueDate").length > 0 && $("#txtExpDate").length > 0) {
                    if (DateAsFormat(document.getElementById("txtIssueDate").value, strDateFormat) > DateAsFormat(document.getElementById("txtExpDate").value, strDateFormat)) {
                        Msg += "Issue Date should be less than Expiry Date\n";
                    }
                }
                if (Msg != "") {
                    alert(Msg);
                    return false;
                }
                else
                    return true;
            }
        });
    </script>
    </form>
</body>
</html>
