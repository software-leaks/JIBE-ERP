<%@ Page Title="Interview Sheet" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CrewInterview.aspx.cs" Inherits="Crew_CrewInterview" %>

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
    <script src="../Scripts/CrewDetails_DataHandler.js" type="text/javascript"></script>
    <script type="text/javascript">
                
        function initScript() {
            $("#dvToggle").click(function () {
                $('#dvInterviewSheet').toggleClass("interview-answer-sheet-fixed");
                $('#dvInterviewSheet th').css('top', 0);
                var htm = $(this).html() == "Collapse" ? "Expand" : "Collapse";
                $(this).html(htm);
            });
            CalculateTotal();
        }

        function checkAvailableWidth() {
            $('#dvInterviewSheet th').css('top', document.getElementById("dvInterviewSheet").scrollTop - 2 + 'px');
        }
        function setDotColor(color_) {

            document.getElementById("tdDot").style.color = "white";
            if (color_ == "red")
                document.getElementById("tdDot").style.backgroundImage = "url(../Images/red-dot.png)";
            else if (color_ == "green")
                document.getElementById("tdDot").style.backgroundImage = "url(../Images/green-dot.png)";
            else {
                document.getElementById("tdDot").style.backgroundImage = "url(../Images/gray-dot.png)";
                document.getElementById("tdDot").style.color = "blue";
            }
        }
        function rdoOptions_Changed(obj) {
            CalculateTotal();
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

            CalculateTotal();                
        }

        function CalculateTotal() {
            var MaxTotal = 0;
            var UserTotal = 0;
            var Avg = 0;
            var AvgOut5 = 0;
            var tempVal;
            

            $('.interview-question-box input[type=checkbox]').each(function () {

                var i = 0;
                var chkID = this.id;
                var hdnQuestion = this.id.replace('chkNA', 'hdnQuestion');
                var rdoList = $(this.id.replace('chkNA', 'rdoOptions'));
                var options = $('table#' + rdoList.selector).find('input:radio');
            

                $(this).parent().parent().removeClass("crew-interview-grid-na-row");
                $(this).parent().parent().removeClass("crew-interview-grid-selected-row");

                if ($(this).attr('checked') != 'checked') {

                    for (i = 0; i < options.length; i++) {
                        if (options[i].checked == true) {
                            tempVal = options[i].value.split(',');
                            UserTotal += eval(tempVal[1]);
                            $(this).parent().parent().addClass("crew-interview-grid-selected-row");
                            break;
                        }
                    }
                    var iMax = document.getElementById(hdnQuestion).value;
                    MaxTotal += eval(iMax);
                }
                else {
                    for (i = 0; i < options.length; i++) {
                        options[i].checked = false;
                        $(options[i]).attr("disabled", true);
                    }
                    $(this).parent().parent().addClass("crew-interview-grid-na-row");

                }
            });

            
            if (MaxTotal > 0) {
                Avg = parseFloat(UserTotal / MaxTotal * 100).toFixed(1);
                AvgOut5 = parseFloat((UserTotal / MaxTotal * 5)).toFixed(1);
                UserTotal = parseFloat(UserTotal).toFixed(1);

                if (parseFloat((UserTotal / MaxTotal * 5)) > 2)
                    setDotColor('green');
                else
                    setDotColor('red');
            }
            else
                setDotColor('red');

            $('[id$=lblMaxMarks]').html(MaxTotal);
            $('[id$=lblUserMarks_P]').html(Avg);
            $('[id$=lblUserMarks]').html(UserTotal);
            $('[id$=lblOutOf5]').html(AvgOut5);
                        
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
        <asp:Panel ID="pnlInterviewPlanning" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="error-message">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                    <asp:HiddenField ID="hdnInterviewID" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnCrewID" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnUserType" runat="server" Value="" />
                    <div style="text-align: center">
                        <fieldset style="text-align: left; margin: 0px; padding: 2px; width: 60%; text-align: left;">
                            <legend>Interview Planning:</legend>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        Crew Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPlanCrewName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Interview Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPlanDate" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPlanDate"
                                            Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Interview Time
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPlanH" runat="server" Width="50px" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12" Text="12"></asp:ListItem>
                                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                        </asp:DropDownList>
                                        H &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlPlanM" runat="server" Width="50px" AppendDataBoundItems="true">
                                            <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                            <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                        </asp:DropDownList>
                                        M
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Rank
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPlanRank" runat="server" DataTextField="Rank_Short_Name"
                                            DataValueField="id" Width="154px" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Interviewer
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPlanInterviewer" runat="server" DataSourceID="ObjectDataSource_UserList"
                                            DataTextField="USER_NAME" DataValueField="USERID" Width="154px" AppendDataBoundItems="True"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlPlanInterviewer_SelectedIndexChanged">
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
                                </tr>
                                <tr>
                                    <td>
                                        Position
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPlanInterviewerPosition" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <asp:Button ID="btnSavePlanning" runat="server" Text="Save" OnClick="btnSavePlanning_Click" />
                                        <asp:Button ID="btnCancelPlanning" runat="server" Text="Cancel" OnClick="btnCancelPlanning_Click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel ID="pnlEdit_InterviewResult" runat="server">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" cellpadding="4" rules="rows" style="background-color: White;
                        width: 100%;">
                        <tr style="color: White; background-color: #336666; font-weight: bold;">
                            <td style="text-align: center; height: 30px; font-weight: bold; font-size: 14px;">
                                FOR OFFICE USE ONLY (MANING AGENT / HEAD OFFICE)
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            Interview Date
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtInterviewDate" runat="server" Width="200px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtInterviewDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td>
                                            Interviewer's Name
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlUserList" runat="server" DataSourceID="ObjectDataSource_UserList"
                                                DataTextField="USER_NAME" DataValueField="USERID" Width="154px" AppendDataBoundItems="True"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td rowspan="3">
                                            <table style="border: 1px solid gray; width: 100%; background-color: #66DDFF;" cellpadding="3">
                                                <tr>
                                                    <td style="text-align: left; color: #888;">
                                                        Planned Interviewer:
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
                                                <tr>
                                                    <td style="text-align: right;" colspan="2">
                                                        <asp:HyperLink ID="lnkEditSchedule" Target="_blank" runat="server" CssClass="linkImageBtn">Re-Schedule Interview</asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--<td>
                                            <asp:TextBox ID="txtPosition" runat="server"></asp:TextBox>
                                        </td>--%>
                                    </tr>
                                    <tr>
                                        <td>
                                            Name of person Interviewed
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPersonInterviewed" runat="server" Width="200px"></asp:TextBox>
                                            <asp:HyperLink ID="lnkOpenProfile" Target="_blank" runat="server">View Profile</asp:HyperLink>
                                        </td>
                                        <td style="text-align: left; color: #888;">
                                            Interview Date:
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblInterviewDate" runat="server"></asp:Label>
                                        </td>
                                        <%--<td style="text-align: left; color: #888;">
                                            Planned By:
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblPlannedBy" runat="server"></asp:Label>
                                        </td>--%>
                                    </tr>
                                    <tr>
                                        <td>
                                            Staff Code
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtStaffCode" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            Interview Rank
                                        </td>
                                        <td>
                                             <asp:TextBox ID="txtInterviewRank" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                                             <asp:DropDownList ID="ddlRank" runat="server" DataTextField="Rank_Short_Name" DataValueField="id"
                                                Width="204px" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlRank_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div class="section-title-broad-lightyellow" style="margin-top: 5px; font-size: 18px;">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblInterviewSheet_Name" runat="server"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    Total Marks:<asp:Label ID="lblMaxMarks" runat="server" CssClass="linkbtn"></asp:Label>
                                    &nbsp;&nbsp;User Marks:<asp:Label ID="lblUserMarks" runat="server" CssClass="linkbtn"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;Marks (%):<asp:Label ID="lblUserMarks_P" runat="server" CssClass="linkbtn"></asp:Label>
                                </td>
                                <td style="width: 100px">
                                    <div id="dvToggle" class="linkbtn">
                                        Expand</div>
                                </td>
                            </tr>
                            <%--<tr>
                                <td colspan="3" align="right">
                                    <asp:CheckBox ID="chkSelectAllNA" runat="server" Text="Select All"  AutoPostBack="true" OnCheckedChanged="chkSelectAllNA_CheckedChanged" > </asp:CheckBox>
                                </td>
                            </tr>--%>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div id="dvInterviewSheet" style="margin-top: 2px;" onscroll="checkAvailableWidth()" class="interview-answer-sheet-fixed">
                         <asp:GridView ID="GridView_AssignedCriteria" runat="server" AllowSorting="false"
                            AutoGenerateColumns="False" CaptionAlign="Bottom" CellPadding="4" DataKeyNames="QID"
                            EmptyDataText="No Record Found" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView_AssignedCriteria_RowDataBound"
                            BorderStyle="Solid" BorderColor="#cccccc" BorderWidth="1px" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="Black" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No." ItemStyle-CssClass="interview-question-box"  HeaderStyle-BackColor="#5D7B9D">
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
                                    <HeaderTemplate >
                                         <asp:CheckBox ID="chkSelectAllNA" runat="server" Text="Select All NA"  onclick="CheckAllNA(this);" > </asp:CheckBox>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkNA" runat="server" AutoPostBack="false" onclick="chkNA_Changed(this)"
                                            Checked='<%#Eval("NotApplicable").ToString()=="1"?true:false%>' />
                                    </ItemTemplate>
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
                    <div style="text-align: left;">
                        <table style="width: 100%; background-color: #F5F6CE; border-collapse: collapse;
                            border-color: #efefef" border="1">
                            <tr>
                                <td colspan="3" style="color: Black; background-color: #cfcfff; vertical-align: top;">
                                    Recomendations:
                                </td>
                            </tr>
                            <tr>
                                <td style="color: Black; width: 300px;">
                                    Recomended for:
                                </td>
                                <td colspan="2" style="color: Black; text-align: left;">
                                    <table style="width: 100%" border="1">
                                        <tr>
                                            <td>
                                                Your Recomendation:
                                            </td>
                                            <td style="font-size: 14px;">
                                                <asp:RadioButtonList ID="rdoSelected" runat="server" RepeatDirection="Horizontal"
                                                    CellPadding="10" CellSpacing="5">
                                                    <asp:ListItem Value="1"><font color="Green"><b>Approved</b></font></asp:ListItem>
                                                    <asp:ListItem Value="0"><font color="Red"><b>Rejected</b></font></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td style="width: 40%; text-align: right; color: #555">
                                                Marks out of 5 (higher the better):
                                            </td>
                                            <td>
                                                <div id="tdDot" class="gray" style="width: 30px; height: 30px; text-align: center;
                                                    background-repeat: no-repeat; font-weight: bold; color: Blue; padding-top: 10px;">
                                                    <asp:Label ID="lblOutOf5" runat="server">0.00</asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; vertical-align: top;">
                                    <table border="0">
                                        <tr>
                                            <td>
                                                <div style="height: 100px; width: 160px; overflow: auto; background-color: White;
                                                    border: 1px solid gray;">
                                                    <asp:CheckBoxList ID="lstVessels" runat="server" DataTextField="vessel_name" DataValueField="vessel_id"
                                                        CellPadding="0" CellSpacing="0" SelectionMode="Multiple" Font-Names="Tahoma"
                                                        Font-Size="11px">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                            <td>
                                                <div style="height: 100px; width: 160px; overflow: auto; border: 1px solid gray;">
                                                    <asp:CheckBoxList ID="chkTradingArea" runat="server" RepeatDirection="Vertical">
                                                        <asp:ListItem Text="US Trades" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Europe Trades" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Intra-Asia" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Piracy" Value="4"></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="text-align: left; vertical-align: top;">
                                    <asp:TextBox ID="txtResultText" runat="server" TextMode="MultiLine" Width="600px"
                                        Height="100px"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtResultText"
                                        WatermarkText="Write your comment here !!" WatermarkCssClass="watermarked" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSaveInterviewResult" Text=" Save " runat="server" OnClick="btnSaveInterviewResult_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text=" Close " OnClientClick="window.close()" />
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: center">
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 0.5%; top: 15%; width: 600px; height: 400px; z-index: 1; color: black"
        title=''>
        <div class="content">
            <iframe id="frPopupFrame" src="" frameborder="0" height="100%" width="100%"></iframe>
        </div>
    </div>
</asp:Content>
