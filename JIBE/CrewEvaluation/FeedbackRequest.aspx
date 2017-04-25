<%@ Page Language="C#" Title="Crew Evaluation Feedback Request" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="FeedbackRequest.aspx.cs" Inherits="CrewEvaluation_FeedbackRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            width: 451px;
        }
        .style5
        {
            width: 73px;
        }
        .style6
        {
            width: 133px;
        }
        .style7
        {
            width: 208px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgrFeedback" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">

        function OpenFeedback() {
            var btnID = '<%=this.Request.QueryString["btnID"]%>';
            if (btnID == 'lnkReqFeedBk') {
                document.getElementById("dvCrewEvaluationFeedbackRequest").style.display = 'block';
                document.getElementById("dvAddFeedback").style.display = 'none';

            } else {
                document.getElementById("dvCrewEvaluationFeedbackRequest").style.display = 'none';
                document.getElementById("dvAddFeedback").style.display = 'block';
            } return false;
        }

        function Validate() {
            var Comment = document.getElementById("txtFeedback").value;
            if (Comment == "") {
                alert("Enter Comment");
                document.getElementById("txtFeedback").focus();
                return false;
            }
            else if (Comment.indexOf("&#60;") > -1 && Comment.indexOf("&#62;") > -1) {
                alert("Invalid characters in comments");
                document.getElementById("txtFeedback").focus();
                return false;
            }
            else if (Comment.indexOf("<") > -1 && Comment.indexOf(">") > -1) {
                alert("Invalid characters < and > in comments");
                document.getElementById("txtFeedback").focus();
                return false;
            }

            if (document.getElementById("ddlFeedbackCategory_Add").value == "0") {
                alert("Select Feedback Action");
                document.getElementById("ddlFeedbackCategory_Add").focus();
                return false;
            }
        }


    </script>
    <div id="dvCrewEvaluationFeedbackRequest" style="width: 100%; background-color: White;
        height: 550px">
        <table style="width: 100%">
            <tr style="background-color: White; border-color: Silver; border-style: solid">
                <td align="center" style="font-size: 12x; border: 1px solid #CEE3F6; text-align: center;
                    padding: 3px; font-weight: bold; color: Black">
                    Crew Evaluation Feedback Request
                </td>
            </tr>
            <tr>
                <td style="font-size: small; font-weight: bold; background-color: White">
                    Pending Feedback Request:<br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView_CrewEvaluationFeedbackRequest" runat="server" AllowSorting="false"
                        Width="100%" AutoGenerateColumns="False" CaptionAlign="Bottom" CellPadding="4"
                        DataKeyNames="ID" EmptyDataText="No Record Found" ForeColor="#333333" GridLines="None"
                        OnRowDataBound="GridView_AssignedCriteria_RowDataBound" BorderStyle="Solid" BorderColor="#cccccc"
                        BorderWidth="1px" CssClass="GridView-css">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requested By">
                                <ItemTemplate>
                                    <asp:Label ID="lblRequestedBy" runat="server" Text='<%#Eval("createdBy")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" CssClass="PMSGridItemStyle-css" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requested On Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblRequestedOn" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("creationDate"))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" CssClass="PMSGridItemStyle-css" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requestor's Comment">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtComment" runat="server" Width="240px" Height="45px" TextMode="MultiLine"
                                        Text='<%#Eval("Req_Remark")%>' ReadOnly="true"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" CssClass="PMSGridItemStyle-css" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDueDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("dueDate"))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" CssClass="PMSGridItemStyle-css" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Requested To">
                                <ItemTemplate>
                                    <asp:Label ID="lblRequestedTo" runat="server" Text='<%#Eval("requestedFrom")%>'></asp:Label>
                                    <asp:HiddenField ID="hdnReqestedFrom" runat="server" Value='<%#Eval("Requested_From")%>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" CssClass="PMSGridItemStyle-css" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Feedback Action">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlFeedbackCategory_Feedback" runat="server" DataSourceID='ObjectDataSource1'
                                        DataTextField="CATEGORY" DataValueField="ID">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get_FeedbackCategories"
                                        TypeName="SMS.Business.Crew.BLL_Crew_Evaluation"></asp:ObjectDataSource>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" CssClass="PMSGridItemStyle-css" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Feedback">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRemark" runat="server" Width="240px" Height="45px" TextMode="MultiLine"
                                        Text='<%#Eval("Remark")%>' Enabled='<%#Eval("Staff_RemarkID").ToString()== "" ? true : false %>'></asp:TextBox>
                                    <asp:HiddenField ID="hdnStaff_RemarkID" runat="server" Value='<%#Eval("Staff_RemarkID")%>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" CssClass="PMSGridItemStyle-css" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="ImgSaveEvalFeedback" ImageUrl="~/images/Save.png"
                                        Visible='<%#Eval("Staff_RemarkID").ToString()!= "" ? false : true %>' CommandArgument='<%#Eval("[ID]") + "," +((GridViewRow)Container).RowIndex%>'
                                        OnCommand="onSave" Height="18px" ToolTip="Send Feedback" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" Width="20px" CssClass="PMSGridItemStyle-css" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                        <RowStyle CssClass="PMSGridRowStyle-css" />
                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="PMSGridSelectedRowStyle-css" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="font-size: small; font-weight: bold; background-color: White">
                    New Feedback Request:<br />
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border: 1px solid #B6DAFD; background-color: #E8F3FE; margin-bottom: 5px;
                        margin-top: 5px; width: 100%; height: 100px">
                        <tr>
                            <td align="right" class="style6">
                                Feedback From:
                            </td>
                            <td align="left" class="style7">
                                <asp:DropDownList ID="ddlFeedbackFrom" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td rowspan="2" align="right" class="style5">
                                Comment :
                            </td>
                            <td rowspan="2" class="style2">
                                <asp:TextBox ID="txtReqComment" runat="server" Width="410px" TextMode="MultiLine"
                                    Height="80px" Style="margin-top: 14px"></asp:TextBox>
                            </td>
                            <td align="left" rowspan="2">
                                <asp:Button ID="btnSendForFeedback" runat="server" Text="Send" OnClick="btnSendForFeedback_Click"
                                    ValidationGroup="saveFZ" ToolTip="Request Feedback" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style6">
                                Due Date:
                            </td>
                            <td align="left" class="style7">
                                <asp:TextBox ID="txtDueDate" runat="server"></asp:TextBox>
                                <tlk4:CalendarExtender ID="CalendarExtendertxtDueDate" runat="server" TargetControlID="txtDueDate">
                                </tlk4:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Italic="true" Font-Size="Small"></asp:Label>
                                <%--<asp:CompareValidator ID="dateValidatortxtDueDate" runat="server" Type="Date" Operator="DataTypeCheck"
                                    InitialValue="" ControlToValidate="txtDueDate" ErrorMessage="**Please enter a valid date." 
                                    Font-Italic="true" Font-Size="Small" ValidationGroup="saveFZ" >
                                </asp:CompareValidator>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvAddFeedback" style="width: 100%; background-color: White; height: 550px;
        display: none">
        <div align="center" style="font-size: 12x; border: 1px solid #CEE3F6; text-align: center;
            padding: 3px; font-weight: bold; color: Black">
            Add Feedback
        </div>
        <table style="border: 1px solid #B6DAFD; background-color: #E8F3FE; margin-bottom: 5px;
            margin-top: 5px; width: 100%; height: 100px">
            <tr>
                <td align="right" class="style6">
                    Feedback From:
                </td>
                <td align="left" class="style7">
                    <asp:Label ID="lblName" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <td rowspan="2" align="right" class="style5">
                    Comment :
                </td>
                <td rowspan="2" class="style2">
                    <asp:TextBox ID="txtFeedback" runat="server" Width="410px" TextMode="MultiLine" Height="80px"
                        Style="margin-top: 14px"></asp:TextBox>
                </td>
                <td rowspan="2" align="right" class="style5">
                    Feedback Action :
                </td>
                <td align="left" rowspan="2">
                    <asp:DropDownList ID="ddlFeedbackCategory_Add" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="left" rowspan="2">
                    <asp:Button ID="btnAddFeedback" runat="server" Text="Save" OnClientClick="return Validate();"
                        OnClick="btnAddFeedback_Click" ValidationGroup="saveFZ" ToolTip="Add Feedback" />
                </td>
            </tr>
            <tr>
                <td align="right" class="style6">
                    Date:
                </td>
                <td align="left" class="style7">
                    <asp:Label ID="lblDate" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="lblerrmsg" runat="server" ForeColor="Red" Font-Italic="true" Font-Size="Small"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
