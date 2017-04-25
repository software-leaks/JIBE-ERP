<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_INSP_Add_Questions.ascx.cs" Inherits="UserControl_uc_INSP_Add_Questions" %>
<script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
   <asp:Panel ID="Panel1" runat="server">
              <div id="dvAddNewCriteria" title=" Add New Question" class="modal-popup-container"
               style="width: 550px; left: 40%; top: 30%;">
  
               <div class="modal-popup-content">
                <asp:UpdatePanel ID="UpdatePanelnew" runat="server"   UpdateMode="Conditional" >
                    <ContentTemplate>
                       <table border="0" style="width: 100%;" cellpadding="10">
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Question:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCriteria" CssClass="textbox-css" Width="400px" Height="100px"
                                        runat="server" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Checklist Type Name:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCatName" CssClass="textbox-css" Width="400px" runat="server"
                                        DataTextField="Category_Name" DataValueField="ID" />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; text-align: left; border-width: 1px; font-weight: bold">
                                    Grading Type:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGradingType" Width="400px" DataTextField="Grade_Name" DataValueField="ID"
                                        runat="server" 
                                        OnSelectedIndexChanged="ddlGradingType_SelectedIndexChanged" onchange="return BindGradeOptions();">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; border-width: 1px">
                                    Gradings
                                </td>
                                <td style="vertical-align: top; border-width: 1px">
                                    <%--<asp:RadioButtonList ID="rdoGradings" runat="server" DataTextField="OptionText" DataValueField="OptionValue"
                                        Enabled="false">
                                    </asp:RadioButtonList>--%>
                                    <div id="rdoGradings"></div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center; border-width: 1px">
                                    <asp:Button ID="btnSaveAndAdd" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnsave_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndClose" CssClass="button-css" runat="server" Text="Save And Close" 
                                        OnClick="btnSaveAndClose_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClientClick="return closeAddQuestion();" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                     <Triggers>
      <asp:AsyncPostbackTrigger ControlID="btnSaveAndClose"  />
   </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        </asp:Panel>