<%@ Page Title="Cash Advance Limit" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CashAdvanceLimit.aspx.cs" Inherits="PortageBill_CashAdvanceLimit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
     <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/crew_interview_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    function DecimalsOnly(e) {
        var evt = (e) ? e : window.event;
        var key = (evt.keyCode) ? evt.keyCode : evt.which;
        if (key != null) {
            key = parseInt(key, 10);
            if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                if (!IsUserFriendlyChar(key, "Decimals")) {
                    return false;
                }
            }
            else {
                if (evt.shiftKey) {
                    return false;
                }
            }
        }
        return true;
    }

    function IsUserFriendlyChar(val, step) {
        // Backspace, Tab, Enter, Insert, and Delete
        if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
            return true;
        }
        // Ctrl, Alt, CapsLock, Home, End, and Arrows
        if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
            return true;
        }
        if (step == "Decimals") {
            if (val == 190 || val == 110) {
                return true;
            }
        }
        // The rest
        return false;
    }
    function showDiv(dv) {
        document.getElementById(dv).style.display = "block";
    }
    function closeDiv(dv) {
        document.getElementById(dv).style.display = "None";
    }
    function showDiv(dv) {
        document.getElementById('dvAddNew').title = "Add Cash Advance Limit"
        showModal(dv);
    }
    function closeDiv(dv) {
        hideModal(dv);
    }
    $(document).ready(function () {
        $('#dvAddNew').draggable();
    });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 <div class="page-title">
           Cash Advance Limit
    </div>
    <asp:UpdatePanel ID="upd" runat="server" ClientIDMode="Static">
        <ContentTemplate>
            <table border="0" style="width: 100%;" cellpadding="1">
                <tr>
                    <td style="text-align: right" colspan="2">
                        <asp:LinkButton ID="lnkAddNew" OnClientClick="javascript:showDiv('dvAddNew');return false;"
                            runat="server" CssClass="linkbtn">Add Cash Advance Limit</asp:LinkButton>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView_CashLimit" runat="server" AutoGenerateColumns="False" 
                                        OnRowDataBound="GridView_CashLimit_RowDataBound"
                                        OnRowUpdating="GridView_CashLimit_RowUpdating"
                                        OnRowDeleting="GridView_CashLimit_RowDeleting" 
                                        OnRowEditing="GridView_CashLimit_RowEditing"
                                        OnRowCancelingEdit="GridView_CashLimit_RowCancelEdit" DataKeyNames="ID" EmptyDataText="No Record Found"
                                        AllowSorting="True" CaptionAlign="Bottom" CellPadding="4" 
                                        GridLines="None" Width="100%" CssClass="gridmain-css">
                                       <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                       <RowStyle CssClass="RowStyle-css"  HorizontalAlign="Center"/>
                                       <HeaderStyle CssClass="HeaderStyle-css" Height="25px" HorizontalAlign="Center" VerticalAlign="Middle"/>
                                       
                                       <Columns>
                                            <asp:TemplateField HeaderText="Rank Category" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnID" runat="server" ClientIDMode="Static" Value='<%#Eval("ID") %>'/>

                                                    <asp:Label ID="lblRankCatName" runat="server" Text='<%#Eval("Category_Name")%>'  ClientIDMode="Static"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                <%--<asp:Label ID="lblRankCatID" runat="server" Text='<%#Eval("Rank_Category_ID")%>'  ClientIDMode="Static"></asp:Label>--%>
                                                    <asp:DropDownList ID="ddlRankCat" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                                </EditItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Percentage">
                                            <ItemTemplate>
                                            <asp:Label ID="lblPercentage" ClientIDMode="Static" runat="server" Text='<%#Eval("Percentage")%>'> </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                             <asp:TextBox ID="txtPercentage" Font-Size="11px" runat="server" ClientIDMode="Static"
                                                        Text='<%#Bind("Percentage")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                            </asp:TemplateField>
                                              
                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="50px">
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnAccept" ToolTip="Update" runat="server" ImageUrl="~/images/accept.png"
                                                        CausesValidation="False" CommandName="Update" AlternateText="Update"></asp:ImageButton>
                                                    <asp:ImageButton ID="ImgBtnCancel" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                                        CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                                        CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
                                                        CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                        AlternateText="Delete"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvAddNew" title="Add New Cash Limit" class="modal-popup-container" style="width: 550px;
        left: 40%; top: 30%;">
        <div class="modal-popup-content">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <table border="0" style="width: 100%;" cellpadding="10">
                        <tr>
                            <td style="font-size: 11px; text-align: left; font-weight: bold">
                                Rank Category:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRnkCat" ClientIDMode="Static" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; text-align: left; font-weight: bold">
                                % of Salary
                            </td>
                            <td>
                                <asp:TextBox ID="txtPercent" Width="250px" runat="server" ClientIDMode="Static"
                                    onkeydown="javascript:return DecimalsOnly(event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 11px; text-align: center;">
                                <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="button-css" runat="server"
                                    Text="Save And Add New" OnClick="btnSave_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnSaveAndClose" ClientIDMode="Static" CssClass="button-css"
                                    runat="server" Text="Save And Close" OnClick="btnSaveAndClose_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnClose" ClientIDMode="Static" CssClass="button-css" runat="server"
                                    Text="Close" OnClick="btnClose_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
              
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
