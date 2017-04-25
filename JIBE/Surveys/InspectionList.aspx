<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InspectionList.aspx.cs" Inherits="Surveys_InspectionList"
    ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("body").on("click", "#<%=btnSearchInspection.ClientID %>", function () {
                if ($.trim($("#<%=txtInspectionFromDate.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtInspectionFromDate.ClientID %>").val()), '<%= UDFLib.GetDateFormat()  %>')) {
                        alert("Enter valid From Date<%=UDFLib.DateFormatMessage()%>");
                        $("#<%=txtInspectionFromDate.ClientID %>").focus();
                        return false;
                    }
                }
                if ($.trim($("#<%=txtInspectionToDate.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtInspectionToDate.ClientID %>").val()), '<%= UDFLib.GetDateFormat()  %>')) {
                        alert("Enter valid To Date<%=UDFLib.DateFormatMessage()%>");
                        $("#<%=txtInspectionToDate.ClientID %>").focus();
                        return false;
                    }
                }
            });

            $("body").on("click", "#<%=btnCancel.ClientID %>", function () {
                window.parent.$("#closePopupbutton").click();
                return false;
            });
        });
    </script>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divInspectionList" style="width: 100%; font-family: Tahoma; font-size: 12px;">
        <table width="100%" border="0" style="margin: 10px 0px 10px 0px;">
            <tr>
                <td align="left">
                    Status :
                </td>
                <td valign="top" align="left">
                    <asp:DropDownList ID="ddlStatus" runat="server" UseInHeader="false" Width="120px">
                        <asp:ListItem Value="0" Text="-SELECT-"></asp:ListItem>
                        <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem>
                        <asp:ListItem Value="Overdue" Text="Overdue"></asp:ListItem>
                        <asp:ListItem Value="Completed" Text="Completed"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left">
                    Port:
                </td>
                <td style="vertical-align: top;">
                    <asp:DropDownList ID="DDLPort" runat="server" UseInHeader="false"  AutoPostBack="false" />
                </td>
                <td align="left">
                    Inspection From :
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtInspectionFromDate" runat="server" Width="70px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtInspectionFromDate"
                        Format="dd/MMM/yy">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td align="left">
                    Inspection To :
                </td>
                 <td  valign="top" align="left">
                    <asp:TextBox ID="txtInspectionToDate" runat="server" Width="70px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" Enabled="True" TargetControlID="txtInspectionToDate"
                        Format="dd/MMM/yy">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    <asp:Button ID="btnSearchInspection" runat="server" Text="Search" OnClick="btnSearchInspection_Click">
                    </asp:Button>
                    <asp:Button ID="btnCancel" ClientIDMode="Static" runat="server" Text="Cancel"></asp:Button>
                </td>
            </tr>
        </table>
        <div style="max-height: 434px; overflow-y: scroll;">
            <asp:GridView ID="gvInspectionSchedule" runat="server" EmptyDataText="No Records Found !!"
                AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" DataKeyNames="InspectionDetailId,Schedule_date"
                CellPadding="1" GridLines="None" Width="100%" CssClass="gridmain-css" ShowHeader="true">
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                <Columns>
                    <asp:TemplateField HeaderText="Inspection Date">
                        <ItemTemplate>
                            <%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Schedule_date"))) %>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Justify" CssClass="PaddingCellCss" Width="110px">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Left" CssClass="PaddingCellHCss" Width="110px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Port
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPort" runat="server" Text='<%#Eval("Port")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Justify" CssClass="PaddingCellCss" Width="120px">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PaddingCellHCss" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Inspector
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblActualInspectorName" runat="server" Text='<%#Eval("InspectorName")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Justify" CssClass="PaddingCellCss" Width="145px">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Center" Width="145px" CssClass="PaddingCellHCss" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            InspectionType
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblInspectionType" runat="server" Text='<%# Eval("InspectionTypeName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Justify" CssClass="PaddingCellCss" Width="145px">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Center" Width="145px" CssClass="PaddingCellHCss" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Status
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="90px" CssClass="PaddingCellCss">
                        </ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Center" Width="90px" CssClass="PaddingCellHCss" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnSelectEvaluation" runat="server" Text="Select" OnCommand="btnSelectEvaluation_Click"
                                CommandArgument='<%#Eval("[Schedule_date]") + "," + Eval("[InspectionDetailId]") %>'>
                            </asp:Button>
                        </ItemTemplate>
                        <ItemStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellCss"  Width="90px" ></ItemStyle>
                        <HeaderStyle Wrap="true" HorizontalAlign="Center" CssClass="PaddingCellHCss" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <label id="Label1" runat="server">
                        NO RECORDS FOUND</label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#aabbdd" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                <PagerStyle Font-Size="16px" CssClass="pager" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
