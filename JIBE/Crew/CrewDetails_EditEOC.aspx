<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_EditEOC.aspx.cs"
    Inherits="Crew_CrewDetails_EditEOC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/common_functions.js" type="text/javascript"></script>
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <div id="dvEditEOC">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <table>
            <tr>
                <td colspan="2" style="text-align: center; color: Black; font-weight: bold; padding-bottom: 10px">
                    Modify EOC Date
                </td>
            </tr>
            <tr>
                <td>
                    EOC Date:
                </td>
                <td>
                    <asp:TextBox ID="txtNewCOCDate" runat="server" Width="100px" ClientIDMode="Static"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender14" runat="server" TargetControlID="txtNewCOCDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Reason:
                </td>
                <td style="text-align: center">
                    <asp:TextBox ID="txtCOCRemark" runat="server" TextMode="MultiLine" Width="300px"
                        Height="160px" MaxLength="1000"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnSaveEOCEdit" runat="server" Text="Save" OnClick="btnSaveEOCEdit_Click"
                        ClientIDMode="Static" />
                    <asp:Button ID="btnSaveAndCloseEOCEdit" runat="server" Text="Save & Close" OnClick="btnSaveAndCloseEOCEdit_Click"
                        ClientIDMode="Static" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script type="text/javascript">
    var strDateFormat = '<%=DFormat %>';
    $(document).ready(function () {
        $("body").on("click", "#btnSaveEOCEdit,#btnSaveAndCloseEOCEdit", function () {
            if ($("#txtNewCOCDate").length > 0) {
                if ($.trim($("#txtNewCOCDate").val()) == "") {
                    $("#txtNewCOCDate").focus();
                    alert("Enter EOC Date");
                    return false;
                }
                var date1 = document.getElementById("txtNewCOCDate").value;
                if ($.trim($("#txtNewCOCDate").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid EOC date<%=UDFLib.DateFormatMessage() %>");
                        $("#txtNewCOCDate").focus();
                        return false;
                    }
                }

            }
        });
    });
    
</script>
</html>
