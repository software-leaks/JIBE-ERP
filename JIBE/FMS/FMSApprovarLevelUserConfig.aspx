<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FMSApprovarLevelUserConfig.aspx.cs"
    Inherits="FMS_FMSSchFileApprovarConfig" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title></title>
    <script src="JS/common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .LinkButton
        {
            text-decoration: underline;
            color: Black;
        }
        
        .awesome
        {
            border: 1px solid #2980B9;
            display: inline-block;
            cursor: pointer;
            background: #3498DB;
            color: #FFF;
            font-size: 14px;
            padding: 6px 8px;
            text-decoration: none;
            text-shadow: 0px 1px 0px #2980B9;
            margin-right: 5px;
            margin-bottom: 5px;
            border-radius: 0px;
        }
    </style>
    <script type="text/javascript">


        function AddApprovar() {



            showModal('dvAppLevelUser');

            return false;
        }

        function validateEvent(e) {
            if (e.keyCode == 13) {

                return false;
            }
        }


    </script>
</head>
<body style="background: white;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 35%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <div align="center">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="12px"
                        ForeColor="Red" Visible="False"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="height: 400px;">
            <table style="width: 100%">
                <tr>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnAddLevel" runat="server" CssClass="awesome" Text="Add Level" OnClick="btnAddLevel_Click"
                                    TabIndex="1000" />
                                &nbsp;
                                <asp:Button ID="btnRefresh" runat="server" CssClass="awesome" Text="Refresh" OnClick="btnRefresh_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="border: 1px solid black; width: 250px; vertical-align: top;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdLevel" runat="server" DataKeyNames="Approval_Level" Width="100%"
                                    AutoGenerateColumns="false" GridLines="None" OnRowDataBound="grdLevel_RowDataBound">
                                    <%--<AlternatingRowStyle CssClass="AlternatingRowStyle-css" />--%>
                                    <RowStyle CssClass="RowStyle-css" Font-Size="11px" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" Font-Size="11px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="14px" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Level
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnklevel" runat="server" Text='<%#"L-"+Eval("Approval_Level")%>'
                                                    CssClass="LinkButton" ForeColor="Black" Font-Bold="true" OnClick="lnklevel_click"
                                                    CommandArgument='<%#Eval("Approval_Level")+","+((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 50%;" align="right">
                                                            <asp:ImageButton ID="ImgSelectLevel" runat="server" Text="Add " OnCommand="onLevelClick"
                                                                CommandArgument='<%#Eval("Approval_Level")+","+((GridViewRow) Container).RowIndex %>'
                                                                ForeColor="Black" ToolTip="View" ImageUrl="~/Images/Arrow2Right.png" Height="16px">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td style="width: 50%;">
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete Level" OnCommand="onDeleteLevelClick"
                                                                CommandArgument='<%#Eval("Approval_Level")%>' Approval_Level='<%#Eval("Approval_Level")%>'
                                                                ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/Delete.png" Height="16px"
                                                                CssClass="btnDelete"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;FMS_DTL_FileApprovalLevels&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>'>
                                                            </asp:Image>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="border: 1px solid black; vertical-align: top; width: 250px;">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdLevelUser" runat="server" DataKeyNames="Approval_Level" Width="100%"
                                    AutoGenerateColumns="false" GridLines="None">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" Font-Size="11px" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" Font-Size="11px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="14px" />
                                    <Columns>
                                      
                                                    <asp:BoundField DataField="UserID" HeaderText="ID" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="40px" Visible="false">
                                                        <headerstyle horizontalalign="Left" />
                                                        <itemstyle width="40px" />
                                                    </asp:BoundField>
                                           
                                                    <asp:BoundField DataField="UserName" HeaderText="User Name" HeaderStyle-HorizontalAlign="Left">
                                                        <headerstyle horizontalalign="Left" />
                                                    </asp:BoundField>
                                              
                                                
                                              <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
                                         <ItemTemplate>
                                            
                                                    <asp:Image ID="imgApproverRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                        Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;FMS_DTL_FileApprovar&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>'>
                                                    </asp:Image>
                                         </ItemTemplate>
                                         </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div id="dvAppLevelUser" title="Approver List" style="display: none; font-family: Tahoma;
            font-size: 10px; width: 320px;">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td style="width: 100px;" align="right">
                                <asp:Label ID="lblSearch" runat="server" Text="Search Text" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                            <td style="width: 160px;">
                                <asp:TextBox ID="txtSearch" ClientIDMode="Static" runat="server" onkeypress="return validateEvent(event)"></asp:TextBox>
                            </td>
                            <td style="width: 60px;" align="left">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div id="dvUser" title="Approver List" style="height: 250px; width: 310px; overflow-y: scroll;">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        
                                        <ContentTemplate>

                                        <%--<asp:Button ID="btnTemp" runat="server" Visible="false" OnClick="onchkUser_Check_Changed" />--%>

                                            <asp:GridView ID="grdUser" runat="server" Width="290px" AutoGenerateColumns="false"
                                                GridLines="None" BorderStyle="Solid" BorderWidth="1px" BorderColor="#df5015"
                                                DataKeyNames="UserID">
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" Font-Size="12px" />
                                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" Font-Size="12px" />
                                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                                <SelectedRowStyle BackColor="#FFFFCC" />
                                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                <Columns>
                                                    <asp:BoundField DataField="UserID" HeaderText="User ID" HeaderStyle-HorizontalAlign="Left"
                                                        Visible="false" />
                                                    <asp:BoundField DataField="UserName" HeaderText="User Name" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkUser" runat="server" Enabled='<%# EnableCheckbox(Eval("UserID").ToString()) %>'
                                                                Checked='<%# SelectCheckbox(Eval("UserID").ToString()) %>' OnCheckedChanged="onchkUser_CheckChanged" AutoPostBack="true"
                                                                 />
                                                               <%--      <asp:CheckBox ID="chkUser" runat="server" Enabled='<%# EnableCheckbox(Eval("UserID").ToString()) %>'
                                                                Checked='<%# SelectCheckbox(Eval("UserID").ToString()) %>'  onchange="AsyncCall();"
                                                                 />--%>
                                                                 <asp:HiddenField ID="hdnCheckboxValues" runat="server" Value='<%# SelectCheckbox(Eval("UserID").ToString()) == true ? 1 : 0  %>' />
                                                             
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            
                                        </ContentTemplate>
                                      
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", ".btnDelete", function () {
                if (confirm("Are you sure you want to delete the level?")) {
                    var approval_level = $(this).attr("approval_level");

                    if (approval_level == "1") {
                        if (confirm("This form has only one level. If you continue , this form will not required any approval.") == false) {
                            return false;
                        }
                    }

                }
                else {
                    return false;
                }
            });
        });
    </script>
</body>
</html>
<%--</asp:Content>--%>