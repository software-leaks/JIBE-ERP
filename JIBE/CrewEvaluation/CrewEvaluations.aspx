<%@ Page Title="Crew Evaluations" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CrewEvaluations.aspx.cs" Inherits="CrewEvaluation_CrewEvaluations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tools.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/AsyncResponse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>

    <style id="TooltipStyle" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: Tahoma;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .grid td
        {
            border: 1px solid #cccccc;
        }
        .grid-row
        {
            padding: 6px;
        }
        .grid-col-fixed
        {
            border: 1px solid #cccccc;
        }
        .grid-col
        {
            border: 1px solid #cccccc;
        }
        .gradiant-css-browne
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#81F79F',EndColorStr='#088A4B');
            background: -webkit-gradient(linear, left top, left bottom, from(#81F79F), to(#088A4B));
            background: -moz-linear-gradient(top,  #81F79F,  #088A4B);
            color: Black;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
        .Pending
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
            font-weight: bold;
            border-left: 1px solid #cccccc;
        }
        .Fixed
        {
            position: relative;
            border-bottom: 1px solid #cccccc;
        }
        .Current
        {
            background-color: Aqua;
        }
    </style>
    <script type="text/javascript">

        function showDialog(PageURL) {
            document.getElementById("ifrmFeedbackRequest").src = PageURL;
            showModal('dvCrewEvalFeedbackReq', true);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    <div id="pageTitle" class="gradiant-css-blue" style="font-size: 12px; border: 1px solid #CEE3F6;
        text-align: center; padding: 3px; font-weight: bold;">
        <asp:Label ID="lblPageTitle" runat="server" Text="Crew Evaluations"></asp:Label>
    </div>
    <div id="page-content" style="height: 620px; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto; padding: 5px;">
        <div style="border: 1px solid #B6DAFD; background-color: white; padding: 3px; margin-bottom: 5px;">
            <table style="border: 1px solid #B6DAFD; background-color: #E8F3FE; width: 100%;">
                <tr>
                    <td style="width: 80px; text-align: left">
                        Staff Code
                    </td>
                    <td style="width: 80px; font-weight: bold; text-align: left">
                        <asp:Label ID="lblStaffCode" runat="server"></asp:Label>
                    </td>
                    <td style="width: 80px; text-align: left">
                        Staff Name
                    </td>
                    <td style="width: 250px; font-weight: bold; text-align: left">
                        <asp:Label ID="lblStaffName" runat="server"></asp:Label>
                    </td>
                    <td style="width: 50px; text-align: left">
                        Rank
                    </td>
                    <td style="font-weight: bold; text-align: left">
                        <asp:Label ID="lblRank" runat="server"></asp:Label>
                        <asp:HiddenField ID="hdnCrewrank" runat="server" />
                    </td>
                    <td style="width: 50px; text-align: left">
                        Sign-On
                    </td>
                    <td style="font-weight: bold; text-align: left">
                        <asp:Label ID="lblSignOn" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        Sign-Off/COC
                    </td>
                    <td style="font-weight: bold; text-align: left">
                        <asp:Label ID="lblCOC" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="GridView_Evaluation" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                        BorderStyle="Solid" ForeColor="#333333" GridLines="Both" Width="100%" BorderColor="#CCCCCC"
                        CssClass="grid" OnRowDataBound="GridView_Evaluation_RowDataBound">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Evaluation Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblEvaluation_Name" runat="server" Text='<%#Eval("Evaluation_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="300px" CssClass="grid-col-fixed" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblRank_Short_Name" runat="server" Text='<%#Eval("Rank_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" CssClass="grid-col-fixed" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblVessel_Short_Name" runat="server" Text='<%#Eval("Vessel_Short_Name")%>'
                                        class='vesselinfo' vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Width="100px" CssClass="grid-col-fixed" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Evaluator" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblEvaluator" runat="server" Text='<%#Eval("Evaluator")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="80px" CssClass="grid-col-fixed" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Marks" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal_Marks" runat="server" Text='<%#Eval("Total_Marks")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Marks" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblMarks" runat="server" Text='<%#Eval("Marks")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="(%) Marks" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblAvgMarks" runat="server" Text='<%#Eval("AvgMarks","{0:00.00}").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblEType" runat="server" Text='<%# Eval("ID").ToString()=="0"? "Un-Planned":"Planned" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblDueDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DueDate"))) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" CssClass="grid-col-fixed" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Evaluation" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPending" Text="Pending For Master" Visible='<%#  !Convert.ToBoolean(Eval("Status").ToString()=="2" ? 1 : Eval("Status")) %>'
                                        runat="server"></asp:Label>
                                    <asp:HyperLink ID="BtnPendingEvaluation" runat="server" Target="_blank" Text='<%# Eval("First_Name") %>'
                                        Visible='<%#  Convert.ToBoolean(Eval("Status").ToString()=="2" ? 1 : 0) %>' CssClass="linkImageBtn linkImageBtnDone"></asp:HyperLink>
                                    <asp:HyperLink ID="btnViewEvaluation" runat="server" Target="_blank" Text="View Evaluation"
                                        Visible='<%# Convert.ToBoolean(Eval("Status").ToString()=="2" ? 0 : Eval("Status")) %>'
                                        CssClass="linkImageBtn linkImageBtnDone"></asp:HyperLink>

                                   <asp:ImageButton runat="server" ID="lnkAddFeedBk" Text="Add Feedback"   ImageUrl="../Images/Plus.gif" Width="14px" Height="14px" Visible='<%# Convert.ToBoolean(Eval("Status").ToString()=="2" ? 0 : Eval("Status")) %>'  ToolTip='Add Feedback'/>
                                   <asp:ImageButton runat="server" ID="lnkReqFeedBk" Text="Request Feedback"   ImageUrl='<%# Eval("FeedbackCount").ToString()=="0" ? "../Images/feedback1.png" : "../Images/FeedbackRed.png" %>'  Width="15px" Height="15px" Visible='<%# Convert.ToBoolean(Eval("Status").ToString()=="2" ? 0 : Eval("Status")) %>'  ToolTip='Request Feedback'/>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250px" CssClass="grid-col-fixed" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#58FA82" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF6FC" ForeColor="#333333" CssClass="grid-row" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvCrewEvalFeedbackReq" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 0.5%; top: 15%; width: 1000px; z-index: 1; color: black" title=''>
        <div class="content">
            <iframe id="ifrmFeedbackRequest" src="" frameborder="0" height="550px;" width="1000px"></iframe>
        </div>
    </div>
</asp:Content>
