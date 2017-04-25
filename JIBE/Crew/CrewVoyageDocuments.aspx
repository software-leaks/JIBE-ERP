<%@ Page Title="Crew Voyage Documents" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CrewVoyageDocuments.aspx.cs" Inherits="Crew_CrewVoyageDocuments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
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
            font-family: arial;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .dvhdr1
        {
            font-family: Tahoma;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
            width: 200px;
            color: Black; /*background: #F5D0A9;*/
            border: 1px solid #F1C15F;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .dvbdy1
        {
            background: #FFFFFF;
            font-family: arial;
            font-size: 11px; /*border-left: 2px solid #3B0B0B;
            border-right: 2px solid #3B0B0B;
            border-bottom: 2px solid #3B0B0B;*/
            padding: 5px;
            width: 200px;
            background-color: #E0F8E0;
            border: 1px solid #F1C15F;
        }
        p
        {
            margin-top: 20px;
        }
        h1
        {
            font-size: 13px;
        }
        .dogvdvhdr
        {
            width: 300;
            background: #C4D5E3;
            border: 1px solid #C4D5E3;
            font-weight: bold;
            padding: 10px;
        }
        .dogvdvbdy
        {
            width: 300;
            background: #FFFFFF;
            border-left: 1px solid #C4D5E3;
            border-right: 1px solid #C4D5E3;
            border-bottom: 1px solid #C4D5E3;
            padding: 10px;
        }
        .pgdiv
        {
            width: 320;
            height: 250;
            background: #E9EFF4;
            border: 1px solid #C4D5E3;
            padding: 10px;
            margin-bottom: 20;
            font-family: arial;
            font-size: 12px;
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
    </style>
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
        function MandatoryCheck() {
            if ($("[id$='txtIssueDate']").val() == "") {
                alert('Issue date is mandatory');
                return false;
            }
            return DateValidation();
        }
        function DateValidation() {
            var v = $("[id$='txtIssueDate']").val();
            var v1 = $("[id$='txtExpDate']").val();
            var Issuedatearray = v.split("/");
            var newIssuedate = Issuedatearray[1] + '/' + Issuedatearray[0] + '/' + Issuedatearray[2];

            var Expirydatearray = v1.split("/");
            var newExpirydate = Expirydatearray[1] + '/' + Expirydatearray[0] + '/' + Expirydatearray[2];

            if (new Date(newIssuedate) > new Date(newExpirydate)) {
                alert('Issue date cannot be greater than Expiry Date');
                return false;
            }
            return true;
        }    
    </script>
    <style type="text/css">
        .noborder
        {
            border: 0;
        }
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div id="page-content" style="z-index: -2; overflow: auto; text-align: center;">
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
            <img src="../Images/Printer.png" title="*Print*" style="cursor: pointer;" alt=""
                onclick="PrintDiv('page-content')" />
        </div>
        <div class="page-title">
            <asp:Label ID="lblPageTitle" runat="server" Text="Voyage - Document check list"></asp:Label>
        </div>
        <%--   <div class="error-message">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>--%>
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <asp:HiddenField ID="hdnCrewID" runat="server" Value="0" />
        <div>
        </div>
        <center>
            <div style="text-align: left; border: 1px solid gray;">
                <table cellspacing="0" cellpadding="2" rules="rows" style="background-color: White;
                    width: 100%;">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Crew Name:
                                    </td>
                                    <td colspan="3" align="right">
                                        <asp:Label ID="lblCurrentRank" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                        &nbsp; - &nbsp;
                                        <asp:Label ID="lblStaffCode" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                        &nbsp; - &nbsp;
                                        <asp:Label ID="lblCrewName" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Checklist applicable for Vessel:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblVessel" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Nationality:<asp:Label ID="lblNationality" runat="server" ForeColor="Black" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 700px">
                                        <asp:Label runat="server" Text='* Mandatory Document Required for SignOn' ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ObjectDataSource ID="ObjDataSourceDDLJoinRank" runat="server" SelectMethod="Get_RankList"
                                TypeName="SMS.Business.Crew.BLL_Crew_Admin"></asp:ObjectDataSource>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                AllowSorting="True" DataKeyNames="DocTypeID" CellPadding="4" ForeColor="#333333"
                                GridLines="None" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelEdit"
                                OnRowUpdating="GridView1_RowUpdating" OnRowDataBound="GridView1_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAstrick" runat="server" Width="5px" Text='*' Visible='<%# Eval("isDocCheckList").ToString() == "1" ? true : false %>'
                                                ForeColor="Red"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("DocTypeName") %>'></asp:Label>                                            
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("DocTypeName") %>'></asp:Label>
                                            <asp:FileUpload ID="docUploader" runat="server" />
                                             <asp:HiddenField runat="server" ID="hdnScannedMand" Value='<%# Eval("isScannedDocMandatory") %>' /> 
                                        </EditItemTemplate>
                                        <ControlStyle Width="250px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Legend" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLegend" runat="server" Text='<%#Eval("Legend") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Y/N/NA" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYN" runat="server" Text='<%#Eval("AnswerYNText") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlYN" runat="server" Text='<%#Eval("AnswerYN").ToString()  == "-1" ? "0" : Eval("AnswerYN") %>'
                                                ValidationGroup="ValidateDoc" Enabled="false" Visible='<%# Eval("VoyageSpecific").ToString() == "0" ? false : true %>'>
                                                <asp:ListItem Text="NA" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRankName" runat="server" Text='<%#Eval("Rank_Short_Name") %>' Visible='<%# Eval("VoyageSpecific").ToString() == "0" ? false : true %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddRank" runat="server" DataSourceID="ObjDataSourceDDLJoinRank"
                                                Text='<%# Bind("RankID") %>' DataTextField="Rank_short_Name" DataValueField="ID"
                                                Width="70px" AppendDataBoundItems="true" ValidationGroup="ValidateDoc" Visible='<%# Eval("VoyageSpecific").ToString() == "0" ? false : true %>'>
                                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Remark")%>' Visible='<%# Eval("VoyageSpecific").ToString() == "0" ? false : true %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRemark" runat="server" Text='<%#Bind("Remark")%>' Visible='<%# Eval("VoyageSpecific").ToString() == "0" ? false : true %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deck" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeck" runat="server" Text='<%#Eval("Deck") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Engine" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEngine" runat="server" Text='<%#Eval("Engine") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Doc.No" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocNo" runat="server" Text='<%#Eval("DocNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDocNo" runat="server" Text='<%#Bind("DocNo") %>' Width="70px"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Date" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssueDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("IssueDate"))) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="80px" />
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtIssueDate" Width="50px" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("IssueDate"))) %>' onchange="DateValidation()" ></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtIssueDate" Format='<%# Convert.ToString(Session["User_DateFormat"])%>' >
                                            </ajaxToolkit:CalendarExtender>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Exp. Date" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("ExpiryDate"))) %>' ></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle Width="80px" />
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtExpDate" runat="server" Width="50px" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("ExpiryDate"))) %>'
                                                onchange="DateValidation()"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calExpDate" runat="server" TargetControlID="txtExpDate"
                                                Format='<%# Convert.ToString(Session["User_DateFormat"])%>'>
                                            </ajaxToolkit:CalendarExtender>
                                        </EditItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Place" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssuePlace" runat="server" Text='<%#Eval("IssuePlace") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtIssuePlace" Width="60px" runat="server" Text='<%#Bind("IssuePlace") %>'></asp:TextBox>
                                            <asp:Label ID="lblVoyageSpecific" runat="server" Text='<%#Bind("VoyageSpecific") %>'
                                                Visible="false"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Country" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCountry" runat="server" Text='<%#Eval("CountryOfIssue") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlCountry" runat="server" Width="100px">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hdnId" Value='<%# Eval("CountryOfIssue") %>' />
                                            <asp:HiddenField runat="server" ID="hdnCountryId" Value='<%# Eval("CountryId") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="ImgAttachment" runat="server" ImageUrl="~/images/Attach.png" Height="16px"
                                                Width="16px" BorderStyle="None" CssClass="noborder" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemStyle CssClass="NoPrint" />
                                        <HeaderStyle CssClass="NoPrint" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="LinkButton1" runat="server" ImageUrl="~/images/accept.png" Width="16px"
                                                CommandName="Update" AlternateText="Update" CausesValidation="true" ><%--OnClientClick="return MandatoryCheck();"--%>
                                            </asp:ImageButton>
                                            <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/reject.png" Width="16px"
                                                CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/DocumentUpLoad.png"
                                                Height="16px" Width="47px" CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                            <asp:Image ID="imgInfo" runat="server" Visible="false" ImageUrl="~/Images/exclamation.gif" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="NoPrint" />
                                        <HeaderStyle CssClass="NoPrint" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#58FAAC" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </div>
</asp:Content>
