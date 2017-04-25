<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocPublishPendingList.aspx.cs" Inherits="DocPublishPendingList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <script src="JS/common.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
    <div style="border: 1px solid outset; background-color: #B0C4DE; margin-top: 5px;
        padding: 2px; font-weight: bold;font-size: 12px;">
        Pending Approval List
    </div>
      <div id="divGrid" style="position: relative; z-index: 0; width: 100%;">
        <asp:GridView ID="gvPendingPublishDoc" runat="server" EmptyDataText="NO RECORDS FOUND"
            AutoGenerateColumns="False" OnRowDataBound="gvPendingPublishDoc_RowDataBound"
            DataKeyNames="PROCEDURE_ID" CellPadding="3" GridLines="None" CellSpacing="0" Width="100%"
            OnSorting="gvPendingPublishDoc_Sorting" AllowSorting="true" Font-Size="11px"
            CssClass="GridView-css">
            <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
            <PagerStyle CssClass="PagerStyle-css" />
            <RowStyle CssClass="RowStyle-css" />
            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
            <Columns>
                <asp:TemplateField HeaderText="Procedure Code">
                    <HeaderTemplate>
                        <asp:Label ID="lbtProCodeHeader" runat="server">Procedure Code</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbtProCode" runat="server" Text='<%#Eval("PROCEDURE_CODE")%>'></asp:Label>                        
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Procedure Name">
                    <HeaderTemplate>
                        <asp:Label ID="lbtProNameHeader" runat="server">Procedure Name</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbtProlName" runat="server" Text='<%#Eval("PROCEDURES_NAME")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Publish Date">
                    <HeaderTemplate>
                        <asp:Label ID="lbtPublishDateHeader" runat="server">Publish Date</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPublishDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CREATED_DATE","{0:dd/MMM/yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Check Out Date">
                    <HeaderTemplate>
                        <asp:Label ID="lbtCheckOutHeader" runat="server">Check Out Date</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCheckOutDate" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.MODIFIED_DATE","{0:dd/MMM/yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Send By">
                    <HeaderTemplate>
                        <asp:Label ID="lbtSentByHeader" runat="server">Sent By</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSentBy" runat="server" Text='<%#Eval("SENT_BY_USERNAME")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Send To">
                    <HeaderTemplate>
                        <asp:Label ID="lbtsendToHeader" runat="server">Sent To</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblsendTo" runat="server" Text='<%#Eval("USERNAME")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                    <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Publish Version">
                    <HeaderTemplate>
                        <asp:Label ID="lblpublishVersionHeader" runat="server">Publish Version</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPublishVersion" runat="server" Text='<%#Eval("PUBLISH_VERSION")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Check Out Version">
                    <HeaderTemplate>
                        <asp:Label ID="lblRunningNumberHeader" runat="server">Running Version</asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRunningNumber" runat="server" Text='<%#Eval("CHANGE_VERSION")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="120px" Height ="22px" ></HeaderStyle>
                    <HeaderTemplate>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblAction" runat="server">Action</asp:Label>
                                </td>
                            </tr>                           
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table cellpadding="1" cellspacing="0">
                            <tr align="center">
                                <td>
                                    <asp:ImageButton ID="imgEditDocument" runat="server" Text="Edit" Height="16px" Width="16px"
                                        CommandArgument='<%#Eval("PROCEDURE_ID")%>' ForeColor="Black" ToolTip="Edit Procedure"
                                        ImageUrl="~/QMSDB/images/edit.gif"/>
                                </td>
                                  <td style="border-color: transparent; width: 10px">
                                </td>
                                <td style="border-color: transparent; width: 10px">
                                  <asp:ImageButton ID="imgViewDocument" runat="server" Height="16px"  ToolTip ="View Procedure" CommandArgument='<%#Eval("PROCEDURE_ID")%>'
                                        ForeColor="Black" ImageUrl="~/QMSDB/images/document_view.png" />
                                </td>
                                <td style="border-color: transparent; width: 10px">
                                </td>
                                <td style="border-color: transparent">
                                    <asp:ImageButton ID="imgCompareDocument" runat="server" Text="Compare" 
                                        Height="16px" CommandArgument='<%#Eval("PROCEDURE_ID")%>' ForeColor="Black" ToolTip="Compare Procedure"
                                        ImageUrl="~/QMSDB/images/Doc_Compare.png" >
                                    </asp:ImageButton>
                                </td>
                              
                                 <td style="border-color: transparent; width: 10px">
                                </td>
                                <td style="border-color: transparent">
                                    <asp:ImageButton ID="imgdelete" runat="server" Text="Select" CommandName="DeleteProcedure"
                                        Height="16px" CommandArgument='<%#Eval("PROCEDURE_ID")%>' ForeColor="Black" ToolTip="Delete Procedure"  OnClientClick="var c= confirm('Are you sure to publish this procedure ?'); if(c) return true ; else return false"
                                        ImageUrl="~/Images/Delete-icon.png" >
                                    </asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                    </ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30"  OnBindDataItem="BindPendingPublishDoc" />
                    <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
