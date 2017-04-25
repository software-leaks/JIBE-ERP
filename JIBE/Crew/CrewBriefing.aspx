<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewBriefing.aspx.cs" Inherits="Crew_CrewBriefing"  MasterPageFile="~/Site.master"  Title="Briefing Sheet"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/crew_interview_css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function initScript() {
            $("#dvToggle").click(function () {
                $('#dvInterviewSheet').toggleClass("interview-answer-sheet-fixed");
                $('#dvInterviewSheet th').css('top', 0);
                var htm = $(this).html() == "Collapse" ? "Expand" : "Collapse";
                $(this).html(htm);
            });
           
        }

        function checkAvailableWidth() {
            $('#dvInterviewSheet th').css('top', document.getElementById("dvInterviewSheet").scrollTop - 2 + 'px');
        }
        function CheckAllNA(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=GridView_AssignedCriteria.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                chkNA_Changed(GridVwHeaderChckbox.rows[i].cells[3].getElementsByTagName("INPUT")[0]);
            }
        }
        function chkNA_Changed(obj) {
            var rdo = $(obj.id.replace('chkNA', 'rdoOptions'));
            var options = $('table#' + rdo.selector).find('input:radio');

            var i = 0;
            if (obj.checked == true) {
                for (i = 0; i < options.length; i++) {
                    options[i].checked = false;
                    $(options[i]).attr("disabled", true);
                }
            }
            else {
                for (i = 0; i < options.length; i++) {
                    $(options[i]).removeAttr("disabled");
                }
            }

        }



    </script>
    <style type="text/css">
        
        div#dvInterviewSheet th
        {
            top: 0px;
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
      <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
      </asp:UpdateProgress>
      <div id="page-content" class="page-content-div">
      <asp:Panel ID="pnlEdit_InterviewResult" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="error-message">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                    <asp:HiddenField ID="hdnInterviewID" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnCrewID" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnUserType" runat="server" Value="" />
                 
              
        
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        <ContentTemplate>
                    <table cellspacing="0" cellpadding="4" rules="rows" style="background-color: White;
                        width: 100%;">
                         <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            Briefing Date
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtInterviewDate" runat="server" Width="200px"></asp:TextBox>
                                            <tlk4:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtInterviewDate">
                                            </tlk4:CalendarExtender>
                                        </td>
                                        <td>
                                            Briefer's Name
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlUserList" runat="server" DataSourceID="ObjectDataSource_UserList"
                                                DataTextField="USER_NAME" DataValueField="USERID" Width="154px" AppendDataBoundItems="True"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="ObjectDataSource_UserList" runat="server" SelectMethod="Get_UserList"
                                            TypeName="SMS.Business.Infrastructure.BLL_Infra_UserCredentials">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="0" Name="CompanyID" SessionField="UserCompanyID"
                                                    Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        </td>
                                        <td rowspan="3">
                                            <table style="border: 1px solid gray; width: 100%;" cellpadding="3">
                                                <tr>
                                                    <td style="text-align: left; color: #888;">
                                                        Planned Briefer:
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblPlannedInterviewer" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; color: #888;">
                                                        Planned Date:
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblPlannedDate" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; color: #888;">
                                                        Time Zone:
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblPlannedTimeZone" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; color: #888;">
                                                        Planned By:
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblPlannedBy" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                     </tr>
                                    <tr>
                                        <td>
                                            Name of person briefed
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPersonInterviewed" runat="server" Width="200px"></asp:TextBox>
                                            <asp:HyperLink ID="lnkOpenProfile" Target="_blank" runat="server">View Profile</asp:HyperLink>
                                        </td>
                                         <td>
                                           Rank
                                        </td>
                                        <td>
                                             <asp:DropDownList ID="ddlRank" runat="server" DataTextField="Rank_Short_Name" DataValueField="id"
                                                Width="204px" >
                                                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                      </tr>
                                    <tr>
                                        <td>
                                            Staff Code
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtStaffCode" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                                        </td>
                                       
                                     </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            <ContentTemplate>
                    <div id="dvInterviewSheet" style="margin-top: 2px; max-height: 625px; overflow: scroll; position: relative;" onscroll="checkAvailableWidth()"  >
                        <asp:GridView ID="GridView_AssignedCriteria" runat="server" AllowSorting="false"
                            AutoGenerateColumns="False" CaptionAlign="Bottom" CellPadding="4" DataKeyNames="QID"
                            EmptyDataText="No Record Found" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView_AssignedCriteria_RowDataBound"
                            BorderStyle="Solid" BorderColor="#cccccc" BorderWidth="1px" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="Black" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No." ItemStyle-CssClass="interview-question-box" HeaderStyle-BackColor="#5D7B9D">
                                    <ItemTemplate>
                                        <%# ((GridViewRow)Container).RowIndex + 1%>
                                        <asp:HiddenField ID="hdnQuestion" runat="server" Value='<%#Eval("Max").ToString()%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Topic" ItemStyle-CssClass="interview-question-box" HeaderStyle-BackColor="#5D7B9D">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory_Name" runat="server" Text='<%#Eval("Category_Name")%>'
                                            Font-Bold="true"></asp:Label><br />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Question" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="interview-question-box" HeaderStyle-BackColor="#5D7B9D">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCriteria" runat="server" Text='<%#Eval("Question").ToString().Replace("\n","<br>")%>'
                                            CssClass='<%#Eval("Mandatory").ToString()=="1"?"highlight":""%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="N/A" ItemStyle-CssClass="interview-question-box" HeaderStyle-BackColor="#5D7B9D">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkNA" runat="server" AutoPostBack="false" onclick="chkNA_Changed(this)"
                                            Checked='<%#Eval("NotApplicable").ToString()=="1"?true:false%>' />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                         <asp:CheckBox ID="chkSelectAllNA" runat="server" Text="Select All NA"  onclick="CheckAllNA(this);" > </asp:CheckBox>
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Answer Reference" HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#5D7B9D"
                                    ItemStyle-CssClass="interview-answer-box">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAnswer" runat="server" Text='<%#Eval("Answer").ToString().Replace("\n","<br>")%>'
                                            CssClass='<%#Eval("Mandatory").ToString()=="1"?"highlight":""%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grading" ItemStyle-CssClass="interview-question-box" HeaderStyle-BackColor="#5D7B9D">
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="rdoOptions" name="rdoOptions" runat="server" DataTextField="OptionText" DataValueField="ID_Value" 
                                            AutoPostBack="false" onclick="rdoOptions_Changed(this)" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList>
                                        <asp:TextBox ID="txtAnswer" runat="server" Width="90%" TextMode="MultiLine" Visible="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks" ItemStyle-CssClass="interview-question-box" HeaderStyle-BackColor="#5D7B9D">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" runat="server" Width="200px" Height="80px" TextMode="MultiLine"
                                            AutoPostBack="false" Text='<%#Eval("UserRemark")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="200px" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="White" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="Black" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#333333" CssClass="grid-row" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                                    <ContentTemplate>
                        <div style="text-align: center">
                          <asp:Button ID="btnSaveInterviewResult" Text=" Save " runat="server" OnClick="btnSaveInterviewResult_Click" />
                          <asp:Button ID="btnCancel" runat="server" Text=" Close " OnClientClick="window.close()" />
                          <asp:HiddenField ID="hdnInterviewType" runat="server" />
                        </div>
                </ContentTemplate>
                        </asp:UpdatePanel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>
