<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Remarks.aspx.cs" Inherits="Remarks" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Remarks</title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <script src="../Scripts/drag.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmRemarks" runat="server">
    <div style="font-family: Tahoma; font-size: 12px; border: 1px solid gray; height: 570px">
        <center>
            <div style="border: 1px solid  #5588BB; padding: 2px; background-color: #5588BB;
                color: #FFFFFF; text-align: center;">
                <b>View/Add &nbsp;&nbsp; Remarks </b>
            </div>
            <div style="background-color: #C6D9F1; height: 40px; vertical-align: top; font-size: x-large;
                font-family: Tahoma;">
                <table cellpadding="0" cellspacing="0" width="99%">
                    <tr>
                        <td align="left">
                            Remarks for Request&nbsp;-&nbsp;<asp:Literal ID="ltRequestid" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <input type="button" id="cmd" value="ADD REMARKS" onclick="dvNewRemark.style.display='inline';" />
                        </td>
                    </tr>
                </table>
                <hr />
            </div>
            <br />
            <div id='dvNewRemark' class="drag" style="display: none; border: 1px solid gray;
                position: absolute; z-index: 1; top: 75px; left: 15px; background-color: LightGrey;
                width: 97%; color: black; text-align: center;">
                <div class="dvpopup" style="width: 99%; margin: 5px;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" MaxLength="200"
                                    Width="98%" Height="120px">        
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="cmdSaveRemark" runat="server" Text="Save Remarks" OnClick="cmdSaveRemark_Click" />
                                <input type="button" id="cmdClose" value="Close" onclick="dvNewRemark.style.display='none';" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <asp:GridView ID="grdRemarks" runat="server" Width="98%" DataSourceID="objRemarks"
                AutoGenerateColumns="false">
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <HeaderStyle CssClass="HeaderStyle-css" />
                <Columns>
                    <asp:TemplateField HeaderText="Remark Date">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarkDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "remarkDate")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark By">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarkBy" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "remark_By")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Remark")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <center>
                        <span>No remarks !!!</span>
                    </center>
                </EmptyDataTemplate>
            </asp:GridView>
        </center>
    </div>
    <asp:ObjectDataSource ID="objRemarks" runat="server" TypeName="SMS.Business.TRAV.BLL_TRV_TravelRequest"
        SelectMethod="GetRemarks">
        <SelectParameters>
            <asp:Parameter Name="requestid" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="agentid" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </form>
</body>
</html>
