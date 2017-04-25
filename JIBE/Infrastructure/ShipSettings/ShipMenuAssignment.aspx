<%@ Page Title="Ship Menu Assignment" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ShipMenuAssignment.aspx.cs" Inherits="Infrastructure_ShipSettings_ShipMenuAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui.min.js"></script>
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
    <script language="javascript" type="text/javascript">

        function ValidatonOnCopyAccess() {

//            if (document.getElementById("ctl00_MainContent_DDLFromVessel").value == "0") {
//                alert("Please select from vessel.");
//                document.getElementById("ctl00_MainContent_DDLFromVessel").focus();
//                return false;
//            }

//            if (document.getElementById("ctl00_MainContent_DDLVessel").value == "0") {
//                alert("Please select vessel.");
//                document.getElementById("ctl00_MainContent_DDLVessel").focus();
//                return false;
//            }

            if (document.getElementById("ctl00_MainContent_lstRankList").value == "0") {
                alert("Please select rank.");
                document.getElementById("ctl00_MainContent_lstRankList").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_DDLFromRank").value == "0") {
                alert("Please select from rank.");
                document.getElementById("ctl00_MainContent_DDLFromRank").focus();
                return false;
            }

            return true;
        }


        function ValidationOnSave() {

//            if (document.getElementById("ctl00_MainContent_DDLVessel").value == "0") {
//                alert("Please select from vessel.");
//                document.getElementById("ctl00_MainContent_DDLVessel").focus();
//                return false;
//            }


            if (document.getElementById("ctl00_MainContent_lstRankList").value == "") {
                alert("Please select rank from list.");
                document.getElementById("ctl00_MainContent_lstRankList").focus();
                return false;
            }

            return true;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
          OnBoard Menu Assignment
    </div>
<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                 
                   <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%" cellspacing="0">
           
                <tr style="border: 1px solid #0489B1; background-color: #E6E6E6;">
                    <td style="background-color: White;visibility:hidden">
                        Vessel :&nbsp;
                    </td>
                    <td style="background-color: White;visibility:hidden">
                        <asp:DropDownList ID="DDLVessel" runat="server" AppendDataBoundItems="true" Font-Size="11px" Enabled ="false"
                            Width="120px">
                        </asp:DropDownList>
                    </td>
                    <td align="center" style="background-color:#A9F5D0">
                        Copy Access From -
                    </td>
                    <td align="right" style="visibility:hidden">
                        Vessel :&nbsp;
                    </td>
                    <td style="visibility:hidden">
                        <asp:DropDownList ID="DDLFromVessel" runat="server" Width="150px" AutoPostBack="false" Enabled ="false"
                            CssClass="txtInput" DataTextField="Vessel_ID" DataValueField="Vessel_Name">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        Rank :&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLFromRank" runat="server" Width="100px" AutoPostBack="false"
                            CssClass="txtInput" DataTextField="USERNAME" DataValueField="USERID">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAppendMode" runat="server" Width="150px" AutoPostBack="false"
                            CssClass="txtInput">
                            <asp:ListItem Value="0" Text="Remove Existing"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Retain Existing"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCopyMenu" runat="server" Width="250px" AutoPostBack="false"
                            CssClass="txtInput">
                            <asp:ListItem Value="0" Text="All Menu"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Selected Module/Sub-Module" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnCopy" runat="server" Text="Copy Access" OnClick="btnCopy_Click"
                            OnClientClick="return ValidatonOnCopyAccess();" CssClass="button-css" Visible="false" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table style="width: 100%; margin-top: 5px;">
                <tr>
                    <td style="vertical-align: top; border: 1px solid #cccccc; width: 15%;">
                        <asp:ListBox ID="lstRankList" runat="server" Height="600px" Width="100%" AutoPostBack="true"
                            CssClass="listbox" SelectionMode="Single" OnSelectedIndexChanged="lstRankList_SelectedIndexChanged">
                        </asp:ListBox>
                    </td>
                    <td style="vertical-align: top; border: 1px solid #cccccc; width: 20%;">
                        <div style="height: 600px; overflow: auto;">
                            <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" NodeIndent="15"
                                Width="100%" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" BorderColor="#cccccc">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px"
                                    NodeSpacing="0px" VerticalPadding="2px" />
                                <ParentNodeStyle Font-Bold="False" />
                                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                    VerticalPadding="0px" CssClass="SelectedNodeStyle" />
                            </asp:TreeView>
                        </div>
                    </td>
                    <td style="vertical-align: top; text-align: right;">
                        <asp:GridView ID="gvMenu" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            EmptyDataText="NO RECORDS FOUND!" PageSize="20" DataKeyNames="Menu_ID" Width="100%"
                            OnDataBound="gvMenu_DataBound" OnPageIndexChanging="gvMenu_PageIndexChanging"
                            CellPadding="4" ForeColor="#333333" GridLines="None">
                            <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                            <RowStyle Font-Size="12px" CssClass="PMSGridRowStyle-css" />
                            <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                            <SelectedRowStyle BackColor="#D8F6CE" />
                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" ForeColor="white" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Screen">
                                    <ItemTemplate>
                                        <asp:Label ID="lblScreenName" runat="server" Text='<%#Eval("Screen_Name") %>'></asp:Label>
                                        <asp:Label ID="lblScreenID" Visible="false" runat="server" Text='<%#Eval("Screen_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                    <ItemStyle Wrap="true" HorizontalAlign="left" Width="400px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assembly">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssembly" runat="server" Text='<%#Eval("Assembly_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                    <ItemStyle Wrap="true" HorizontalAlign="left" Width="300px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Menu">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkMenu" runat="server" Checked='<%# Convert.ToBoolean(Eval("Access_Menu")) %>'
                                            ForeColor="white" />
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="300px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkView" runat="server" Checked='<%# Convert.ToBoolean(Eval("Access_View")) %>'
                                            ForeColor="white" />
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Add">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAdd" runat="server" Checked='<%# Convert.ToBoolean(Eval("Access_Add")) %>'
                                            ForeColor="white" />
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEdit" runat="server" Checked='<%# Convert.ToBoolean(Eval("Access_Edit")) %>'
                                            ForeColor="white" />
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDelete" runat="server" Checked='<%# Convert.ToBoolean(Eval("Access_Delete")) %>'
                                            ForeColor="white" />
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkApprove" runat="server" Checked='<%# Convert.ToBoolean(Eval("Access_Approve")) %>'
                                            ForeColor="white" />
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div style="border: 0px solid #cccccc; background-color: #E6E6E6; padding: 2px 2px 2px 2px;">
                            <asp:Button ID="btnResetMenu" runat="server" Text="Remove Access" OnClick="btnResetMenu_Click"
                                CssClass="button-css" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" OnClientClick="return ValidationOnSave();"
                                CssClass="button-css" />
                        </div>
                        <div>
                            <asp:Label ID="lblMessage" runat="server" Style="color: #0000FF"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
