<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_Feedback.aspx.cs"
    Inherits="Crew_CrewDetails_Feedback" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <div id="dvCrewFeedbackGrid">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlViewFeedbacks" runat="server" Visible="false">
            <asp:GridView ID="GridView_CrewRemarks" runat="server" AllowSorting="False" AutoGenerateColumns="false"
                GridLines="None" CellPadding="3" CellSpacing="1" Width="100%" CssClass="GridView-css">
                <Columns>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date_of_creation"))) %>
                        </ItemTemplate>
                        <ItemStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vessel">
                        <ItemTemplate>
                            <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("VESSEL_NAME") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Posted By">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Feedback">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Remark").ToString().Replace("\n","<br>") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkAttachment" runat="server" ImageUrl="~/Images/Attach.png" NavigateUrl='<%# "~/Uploads/CrewDocuments/" + Eval("AttachmentPath").ToString()%>'
                                Target="_blank" Visible='<%#Eval("AttachmentPath").ToString()==""?false:true%>' />
                        </ItemTemplate>
                        <ItemStyle Width="10px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="HeaderStyle-css" />
                <PagerStyle CssClass="PagerStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="pnlAddFeedback" runat="server" Visible="false">
            <div id="dvPopupAddFeedback">
                <table style="background-color: white; font-family: Tahoma ,Tahoma, Sans-Serif,vrdana; font-size: 12px; ">
                    <tr>
                        <td>
                            Feedback/Note
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCrewRemarks" runat="server" TextMode="MultiLine" Height="200px"
                                Width="450px">
                            </asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvCrewRemarks" runat="server"
                                    ValidationGroup="V1" Display="None" ErrorMessage="Remarks is mandatory!"
                                    ControlToValidate="txtCrewRemarks" InitialValue=""></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="v1"
                                    TargetControlID="rfvCrewRemarks" runat="server">
                                </ajaxToolkit:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>Select Voyage: <asp:DropDownList ID="ddlVoyages" runat="server" 
                                                            DataValueField="ID" Width="250px">                                                            
                                                        </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:FileUpload ID="CrewRemarks_FileUploader" runat="server" Width="400px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnSaveRemarks" runat="server" Text="Save" OnClick="btnSaveRemarks_Click" ValidationGroup="V1">
                            </asp:Button>
                            <asp:Button ID="btnSaveAndCloseRemarks" runat="server" Text="Save & Close" OnClick="btnSaveAndCloseRemarks_Click">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
