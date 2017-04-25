<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_NewEvaluation.aspx.cs"
    Inherits="Crew_CrewDetails_NewEvaluation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <div class="error-message" onclick="javascript:this.style.display='none';">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    <asp:Panel ID="pnlUnplannedEval" runat="server" Visible="false" Height="300px">
        <div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellpadding="10">
                        <tr>
                            <td colspan="3" style="text-align: left; font-family: Tahoma ,Tahoma, Sans-Serif,vrdana;
                                font-size: 16px; padding-bottom: 5px; font-weight: bold">
                                Select Evaluation Sheet
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; margin: 0px; padding: 2px; font-family: Tahoma ,Tahoma, Sans-Serif,vrdana;
                                font-size: 12px;">
                                Evaluation
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlEvaluations" runat="server" DataTextField="Evaluation_Name"
                                    DataValueField="Evaluation_ID" Width="300px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; margin: 0px; padding: 2px; font-family: Tahoma ,Tahoma, Sans-Serif,vrdana;
                                font-size: 12px;">
                                Due date
                            </td>
                            <td>
                                <asp:TextBox ID="txtEvaDueDate" runat="server" Width="95" ClientIDMode="Static"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calEvaDate" runat="server" TargetControlID="txtEvaDueDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td>
                                <asp:Button ID="btnUnplannedEvaluation" runat="server" OnClick="btnUnplannedEvaluation_Click"
                                    Text="Perform UnPlanned Evaluation in Office" Height="30px" ClientIDMode="Static" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="font-weight: bold; font-size: small; padding-bottom: 0px;">
                                Vessel Evaluator's Rank:
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="padding-top: 0px;">
                                <asp:GridView ID="GridView_Evaluation" runat="server" BorderStyle="Solid" ForeColor="#333333"
                                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" GridLines="None" BorderColor="#CCCCCC"
                                    CssClass="grid">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEvaluator_Rank" runat="server" Text='<%#Eval("Rank_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" Width="200px" CssClass="grid-col-fixed" Font-Size="X-Small" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="Small" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnPlanEvaluationForMaster" runat="server" OnClick="btnPlanEvaluationForMaster_Click"
                                    Text="Save this Evaluation for Vessel" Height="30px" ClientIDMode="Static" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    </form>
</body>
<script type="text/javascript">
    var strDateFormat = '<%=DFormat %>';

    $(document).ready(function () {
        $("body").on("click", "#btnPlanEvaluationForMaster,#btnUnplannedEvaluation", function () {
            if ($("#txtEvaDueDate").length > 0) {
                var date1 = document.getElementById("txtEvaDueDate").value;
                if ($.trim($("#txtEvaDueDate").val()) == "") {
                    alert("Enter Due date");
                    $("#txtEvaDueDate").focus();
                    return false;
                }
                if ($.trim($("#txtEvaDueDate").val()) != "") {
                    if (IsInvalidDate(date1, strDateFormat)) {
                        alert("Enter valid Due date<%= UDFLib.DateFormatMessage() %>");
                        $("#txtEvaDueDate").focus();
                        return false;
                    }
                }
            }
        });
    });
    
</script>
</html>
