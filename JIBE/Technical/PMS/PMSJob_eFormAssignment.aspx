<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMSJob_eFormAssignment.aspx.cs"
    Inherits="Technical_PMS_PMSJob_eFormAssignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Child screen assignment</title>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <script language="javascript" type="text/javascript">
        function OnbtnRetrieve() {

            var SearchJobID = document.getElementById("txtSearchJobID").value;
            if (SearchJobID != "") {
                if (isNaN(SearchJobID)) {
                    alert('Job Id is accept ony numeric value.')
                    return false;
                }

            }
            return true;
        }
    </script>
    <form id="form1" runat="server">
    <div style="font-family: Tahoma; font-size: 12px; border: 1px solid #cccccc; padding: 0px;
        margin-top: 0px;">
        <center>
            <table cellpadding="1" cellspacing="1" width="95%" style="position: relative;">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    Search:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSearch" onFocus="javascript:this.select()" CssClass="txtInput"
                                        Width="340px" runat="server"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 10%;">
                                    <asp:ImageButton ID="imgScreenSearch" ImageUrl="~/Purchase/Image/preview.gif" runat="server"
                                        OnClick="imgScreenSearch_Click" />
                                </td>
                                <td align="right" style="width: 35%;">
                                    &nbsp;
                                </td>
                                <td align="right" style="width: 5%;">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="max-height: 400px; width: 100%; overflow: auto;">
                            <asp:GridView ID="gvAssigneForm" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" OnRowDataBound="gvAssigneForm_RowDataBound" Width="100%"
                                OnSelectedIndexChanging="gvAssigneForm_SelectedIndexChanging" OnRowCommand="gvAssigneForm_RowCommand"
                                AllowSorting="true" OnSorting="gvAssigneForm_Sorting">
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel">
                                        <HeaderTemplate>
                                            Vessel
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.Vessel_Name")%>
                                            <asp:Label ID="lblForm_ID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Form_ID")%>'></asp:Label>
                                            <asp:Label ID="lblJob_ID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Job_ID")%>'></asp:Label>
                                            <asp:Label ID="lbl_ID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'></asp:Label>
                                            
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location Name">
                                        <HeaderTemplate>
                                            eForm Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.Form_Display_Name")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Select
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           <%-- <asp:CheckBox ID="chkAssingeForm" runat="server" />--%>
                                           <asp:CheckBox ID="chkAssingeForm" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem, "AssignFlag").ToString() =="1"? true : false %>'/> 
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPager" runat="server" OnBindDataItem="BindeFormAssign" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="background-color: #d8d8d8;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Height="28px" Width="150px"
                            OnClick="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblErrMsg" ForeColor="Blue" runat="server" Width="200px"></asp:Label>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
