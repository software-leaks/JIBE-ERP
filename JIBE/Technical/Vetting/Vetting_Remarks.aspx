<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vetting_Remarks.aspx.cs"
    Inherits="Technical_Vetting_Vetting_Remarks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <style>
        body
        {
            color: black;
            font-family: Tahoma;
            font-size: 12px;
            background-color: White;
        }
        lbl
        {
            white-space: normal;
        }
        
        .CurHeaderStyle-css
        {
            background: url(../../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #333333;
            font-size: 11px;
            padding-top: 5px;
            padding-bottom: 5px;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #959EAF;
            border-collapse: collapse;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updGridAttach" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="width: 100%;">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblRemarks" runat="server" Text="Remark: "></asp:Label><b style="color:Red">*</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRemarks" runat="server" Height="50px" TextMode="MultiLine" 
                                Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    <td>
                    </td>
                        <td>
                            <asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click" />
                        </td>
                       
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:UpdatePanel ID="UpdPnlGrid" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div style="width: 810px; height: 180px; overflow-y: scroll;">
                                        <asp:GridView ID="gvRemarks" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="false"
                                            CellPadding="2" ShowHeaderWhenEmpty="true" OnRowDataBound="gvRemarks_RowDataBound"
                                            Width="100%" CssClass="gridmain-css" GridLines="None">
                                            <HeaderStyle CssClass="CurHeaderStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" Height="20px" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Date
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemarkDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Date_Of_Creation") %>'
                                                            CssClass="lbl"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Remark By
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemarkBy" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.User_name") %>'
                                                            CssClass="lbl"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Remarks
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemark" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Remark") %>'
                                                            CssClass="lbl"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="70%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
