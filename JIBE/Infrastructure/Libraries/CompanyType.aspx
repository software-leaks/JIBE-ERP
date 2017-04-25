<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyType.aspx.cs" Inherits="CompanyType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function validationOnSave() {

            if (document.getElementById("txtCompanyTypeName").value == "") {
                alert("Please enter company type name.");
                document.getElementById("txtCompanyTypeName").focus();
                return false;
            }

            if (document.getElementById("txtCompanyTypeDesc").value == "") {
                alert("Please enter company type description.");
                document.getElementById("txtCompanyTypeDesc").focus();
                return false;
            }

            return true;
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 60%;">
            <div style="font-size: 12px; background-color: #5588BB; color: White; text-align: center;">
                <b>Add/View : Company Type </b>
            </div>
            <div style="height: 650px; width: 100%; color: Black;">
                <asp:UpdatePanel ID="UpdCompanyType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <table width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="right" style="width: 10%">
                                        Name &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCompanyTypeName" CssClass="txtInput" Width="90%" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 6%">
                                        Desc&nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right">
                                        *
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCompanyTypeDesc" CssClass="txtInput" Width="90%" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="center" style="color: #FF0000; width: 10%">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClientClick="return validationOnSave();"
                                            Width="60px" CommandArgument="Save" OnClick="btnsave_Click" />
                                    </td>
                                    <td align="center" style="color: #FF0000; width: 10%">
                                        <asp:Button ID="btnSaveNClose" runat="server" Text="Save & Close" OnClientClick="return validationOnSave();"
                                            Width="80px" CommandArgument="SaveNClose" OnClick="btnSaveNClose_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvCompanyType" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    AutoGenerateColumns="False" OnRowDataBound="gvCompanyType_RowDataBound" DataKeyNames="ID"
                                    CellPadding="1" CellSpacing="0" Width="100%" GridLines="both" OnSorting="gvCompanyType_Sorting"
                                    AllowSorting="true">
                                    <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                    <RowStyle Font-Size="12px" CssClass="PMSGridRowStyle-css" />
                                    <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="CompanyType">
                                            <HeaderTemplate>
                                                Company Type Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompanyType" runat="server" Text='<%#Eval("Company_Type")%>' Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CompanyType">
                                            <HeaderTemplate>
                                                Company Type Desc.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompanyTypeDesc" runat="server" Text='<%#Eval("Company_Type_Desc")%>'
                                                    Style="color: Black"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                OnClientClick="return confirm('Are you sure want to delete?')" CommandArgument='<%#Eval("[ID]")%>'
                                                                ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px">
                                                            </asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindCompanyType" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
    </form>
</body>
</html>
