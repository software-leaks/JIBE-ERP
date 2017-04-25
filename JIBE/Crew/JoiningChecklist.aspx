<%@ Page Title="Joining Checklist (Agent)" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="JoiningChecklist.aspx.cs" Inherits="Crew_JoiningChecklist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function PrintDiv(dvID) {

            var a = window.open('', '', 'left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
            a.document.write(document.getElementById(dvID).innerHTML);
            a.document.close();
            a.focus();
            a.print();
            a.close();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="error-message">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </div>
    <div id="page-content" style="text-align: center;">
        <asp:HiddenField ID="hdnCrewID" runat="server" Value="0" />
        <div class="NoPrint" style="text-align: right">
            <style type="text/css" media="print">
                .NoPrint
                {
                    display: none;
                }
                #pgHeader
                {
                    color: Black;
                }
                #tblCheckList
                {
                    border-width: 1px;
                }
            </style>
            <img src="../Images/Printer.png" title="*Print*" style="cursor: pointer;" alt="" onclick="PrintDiv('page-content')" />
        </div>
        <center>
            <div style="text-align: left;">
                <table cellspacing="0" cellpadding="4" rules="rows" style="background-color: White;
                    border-color: #336666; border: 1px solid gray; border-collapse: collapse;">
                    <tr style="color: White; background-color: #336666; font-weight: bold;">
                        <td style="text-align: center; height: 30px; font-weight: bold; font-size: 14px;">
                            <span id="pgHeader">JOINING CHECKLIST (AGENT)</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <u>SECTION B</u><br />
                            INSTRUCTIONS FOR COMPLETION: All questions to be answered. Questions that are not
                            applicable are to be answered as NA. If a question is answered in the negative,
                            a reason is to be given.
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource_CheckList"
                                AutoGenerateColumns="False" Width="100%" AllowSorting="True" DataKeyNames="QuestionID"
                                CellPadding="4" ForeColor="#333333" GridLines="Both">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Nr.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuestionID" runat="server" Text='<%#Eval("QuestionID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Question">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("Question") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="YES/NO">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYN" runat="server" Text='<%#Eval("AnswerText") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:RadioButtonList ID="rdoYN" runat="server" Text='<%#Bind("Answer") %>'>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Comment") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Remark").ToString().Replace("\n","</br>")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Text='<%#Bind("Remark")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="LinkButton1" ToolTip="Update" runat="server" ImageUrl="~/images/accept.png" Width="16px"
                                                CommandName="Update" AlternateText="Update"></asp:ImageButton>
                                            <asp:ImageButton ID="LinkButton2" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png" Width="16px"
                                                CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif" Width="16px"
                                                CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="NoPrint" />
                                        <HeaderStyle CssClass="NoPrint" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#58FAAC" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="pgHeader" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ObjectDataSource_CheckList" runat="server" SelectMethod="Get_CrewJoiningChecklist"
                                TypeName="SMS.Business.Crew.BLL_Crew_CrewDetails" UpdateMethod="UPDATE_CrewJoiningChecklist">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="CrewID" QueryStringField="CrewID" Type="Int32" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:QueryStringParameter Name="CrewID" QueryStringField="CrewID" Type="Int32" />
                                    <asp:Parameter Name="QuestionID" Type="Int32" />
                                    <asp:Parameter Name="Answer" Type="Int32" />
                                    <asp:Parameter Name="Remark" Type="String" />
                                    <asp:SessionParameter Name="Modified_By" Type="Int32" SessionField="userid" />
                                </UpdateParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </div>
</asp:Content>
