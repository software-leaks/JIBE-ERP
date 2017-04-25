<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.master" CodeFile="ODM_History.aspx.cs"
    Inherits="ODM_ODM_History" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <title>ODM History</title>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
          <script language="javascript" type="text/javascript">

              function Validate() {
                  var sText = document.getElementById("ctl00_MainContent_txtSeachText");
                 if (sText.value != "" && sText.value.length < 4) {
                      alert("Search text must be minimum 4 characters.");
                      sText.focus();
                      return false;

                  }

                  return true;

              }
    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
            <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
        <center>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td align="center">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            ODM History
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <div style="background-color: White; 
                            max-height: 100%;">

                                    <table style="margin-top: 10px;">
                                        <tr>
                                            <td valign="top" align="right">
                                                Vessel Name :
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:DropDownList ID="ddlvessel" runat="server" Width="200px" CssClass="txtInput">
                                                </asp:DropDownList>
                                            </td>
                                              <td valign="top" align="right">
                                               Search Text :
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtSeachText" ValidationGroup="ValidateSearch"  MaxLength="30" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                Start Date :
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtStartDate" runat="server" Width="120px" CssClass="txtInput"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalStartDate" runat="server" TargetControlID="txtStartDate"
                                                    Format="dd-MMM-yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td align="right">
                                                End Date :
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:TextBox ID="txtEndDate" runat="server" Width="120px" CssClass="txtInput"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalEndDate" runat="server" TargetControlID="txtEndDate"
                                                    Format="dd-MMM-yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td valign="top" align="left" >
                                                <%--  <asp:ImageButton ID="btnportfilter" runat="server" OnClick="btnportfilter_Click"
                                                                    ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />--%>
                                                <asp:Button ID="btnportfilter" runat="server" Font-Bold="true" ToolTip="Search" 
                                                    ValidationGroup="ValidateSearch" OnClientClick="return Validate();"
                                                    Text="Search" onclick="btnportfilter_Click1"  />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td valign="top" align="left">
                                            <asp:Button ID="btnODMMain" Text="ODM Main" runat="server" 
                                                    onclick="btnODMMain_Click" />
                                            </td>
                                        </tr>
                                        <tr id="TR1" runat="server">
                                            <td valign="top" align="right">
                                                Departments :
                                            </td>
                                            <td valign="top" align="left" colspan="6">
                                                <asp:CheckBoxList ID="chkListDepartment"  CssClass="txtInput" RepeatDirection="Horizontal"
                                                    runat="server"></asp:CheckBoxList>

                                               
                                            </td>


                                        </tr>
                                    </table>
                                    <table style="width:100%">
                                    <tr>
                            <td style="width:60%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td align="center" valign="top">
                                       <%-- <asp:Label ID="lblWebPortCallHistory" Title="Port Call History" runat="server" Width="100%">
                                        <div style=" border: 1px solid #cccccc;">
                                             <div id="dvPortCallHistory">
                                             </div>
                                        </div>
                                        </asp:Label>--%>
                                        <asp:GridView ID="gvODMHistory" runat="server" EmptyDataText="NO RECORDS FOUND" 
                                            OnRowDataBound="gvODMHistory_RowDataBound" OnSorting="gvODMHistory_Sorting"
                                                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
                                            DataKeyNames="GroupId" CellPadding="1"
                                                                Width="100%" onrowcommand="gvODMHistory_RowCommand">
                                                                <HeaderStyle CssClass="HeaderStyle-css" />
                                                                <RowStyle CssClass="RowStyle-css" />
                                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SENT DATE">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSentDate" runat="server" Text='<%# Eval("SENT_DATE")%>'></asp:Label></ItemTemplate>
                                                                        <ItemStyle Wrap="True" HorizontalAlign="Left" Width="20%" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="VESSEL">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVessel" Visible="true" runat="server"></asp:Label></ItemTemplate>
                                                                        <ItemStyle Wrap="True" HorizontalAlign="Center" Width="40%" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Department">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Deapartment_Name")%>'></asp:Label></ItemTemplate>
                                                                        <ItemStyle Wrap="True" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Subject">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("ODM_SUBJECT")%>'></asp:Label></ItemTemplate>
                                                                        <ItemStyle Wrap="True" HorizontalAlign="Center" Width="20%" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Attachment">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAttachmentCount" Visible="true" runat="server" Text='<%# Eval("Attachment_Count")%>'></asp:Label></ItemTemplate>
                                                                        <ItemStyle Wrap="True" HorizontalAlign="Left" Width="7%" CssClass="PMSGridItemStyle-css">
                                                                        </ItemStyle>
                                                                        <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                    
                                                                    <ItemTemplate>
                                                                          <asp:ImageButton ID="SelectButton" ImageUrl="~/Images/asl_view.png" ToolTip="View"  CommandArgument='<%#Eval("[GroupId]") %>' CommandName="Select" runat="server" />
                                                                    </ItemTemplate>
                                                                        <ItemStyle Wrap="True" HorizontalAlign="Left" Width="2%" CssClass="PMSGridItemStyle-css" />

                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                    </td>

                                </tr>
                                <tr>
                                <td >
                                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindPortODMHistory" />
                                </td>
                                </tr>
                                <tr>
                                <td>
                                  <asp:Label ID="lblRecordCount" Visible="true" runat="server" Width="250px" ></asp:Label>
                                </td>
                                </tr>
                            </table>

                                    </td>
                                    <td style="width:40%; height:650px" valign="top">
                                    <iframe id="iFrame" runat="server" width ="100%" height="400px"> </iframe>
                                    </td>
                                    </tr>
                                    </table>
  

                        </div>
                    </td>
                </tr>
            </table>
        </center>
           </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>