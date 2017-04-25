<%@ Page Title="Agency Fee" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DisbFeeLib.aspx.cs" Inherits="CrewDisbursement_DisbFeeLib" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <%--    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>--%>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/gridviewScroll.min.js"></script>
    <script type="text/javascript">


        var nVer = navigator.appVersion;
        var nAgt = navigator.userAgent;
        var browserName = navigator.appName;
        var fullVersion = '' + parseFloat(navigator.appVersion);
        var majorVersion = parseInt(navigator.appVersion, 10);
        var nameOffset, verOffset, ix;

        
        ///////////////////////////



        //        function selectdate() {
        //            $('input[type=date]').each(
        //            function () {

        //                $(this).datepicker({ dateFormat: 'dd/mm/yy' });
        //              });
        //        }
        function aftercall() {


            gridScrollAgency();
            gridScrollProc();
            $('#ctl00_MainContent_GridView_AgencyFeePagerBottom').hide();
            $('#ctl00_MainContent_GridView_ProcessingFeePagerBottom').hide();
            //     $('#ctl00_MainContent_GridView_AgencyFeeVerticalBar').css('left': '10px');


        }

        $(document).ready(function () {
            aftercall();
        });

        function gridScrollAgency() {
            $('#<%=GridView_AgencyFee.ClientID%>').gridviewScroll({
                width: 1385,
                height: 600
            });
        }
        function gridScrollProc() {
            $('#<%=GridView_ProcessingFee.ClientID%>').gridviewScroll({
                width: 1385,
                height: 600
            });
        }
    
    </script>
    <style type="text/css">
        .fixedHeader
        {
            position: absolute;
            background-color: #5588bb;
            font-weight: bold;
            font-size: 12px;
            color: White;
            font-family: Tahoma;
            border-spacing: 12px;
            border: 1px solid #efefef;
        }
        table tbody tr th
        {
            border: 1px solid black;
            min-width: 20px;
        }
        #ctl00_MainContent_GridView_AgencyFee td
        {
            border-right: 0.5px dashed grey;
            text-align: center;
        }
        #ctl00_MainContent_GridView_ProcessingFee td
        {
            border-right: 0.5px dashed grey;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: bottom;
        background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
        padding: 2px; background-color: #F6CEE3; text-align: center; font-weight: bold;">
        <div>
            Manning Office Fee
        </div>
    </div>
    <div id="Div1" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 5px;">
        <div id="page-content">
            <asp:HiddenField ID="hfdID" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel_View" runat="server">
                <ContentTemplate>
                    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
                        <ProgressTemplate>
                            <div id="divProgress" style="background-color: transparent; position: absolute; left: 49%;
                                top: 20px; z-index: 2; color: black">
                                <img src="../images/loaderbar.gif" alt="Please Wait" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <div class="error-message" onclick="javascript:this.style.display='none';">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                    </div>
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server">Manning Office: Agency Fee</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </legend>
                        <div style="max-height: 709px; overflow: auto; width: 100%;">
                            <asp:GridView ID="GridView_AgencyFee" runat="server" PageSize="15" CellPadding="1"
                                EmptyDataText="No matching found!" AllowSorting="false" AllowPaging="false" CssClass="grd"
                                ForeColor="#333333" GridLines="None" Width="100%" DataKeyNames="ID" OnRowCreated="GridView_AgencyFee_RowCreated">
                                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle BackColor="#E2E2E2" BorderColor="Black" Wrap="true" />
                                <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                                <EditRowStyle VerticalAlign="Top" BackColor="#efefef" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                            CommandArgument='<%#Eval("[ID]")+ ";" +Eval("Name")%>' ForeColor="Black" ToolTip="Edit"
                                                            ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />
                        <b>
                            <asp:Label ID="Label3" runat="server">Update History</asp:Label>
                        </b>
                        <div style="max-height: 400px; overflow: auto; width: 100%">
                            <asp:GridView ID="gv_AgencyFeeHistory" runat="server" AutoGenerateColumns="False"
                                CellPadding="1" CssClass="grd" ForeColor="#333333" GridLines="None" PageSize="15"
                                Width="100%">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <EditRowStyle BackColor="#efefef" VerticalAlign="Top" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Updated By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("UpdatedBy_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Updated On">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Updated_On"))) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </fieldset>
                    <br />
                    <div>
                        <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                            <legend>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server">Manning Office: Processing Fee</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </legend>
                            <div style="max-height: 709px; overflow: auto; width: 100%;">
                                <asp:GridView ID="GridView_ProcessingFee" runat="server" AllowPaging="false" AllowSorting="false"
                                    CellPadding="1" CssClass="grd" EmptyDataText="No matching found!" ForeColor="#333333"
                                    GridLines="None" OnRowCreated="GridView_ProcessingFee_RowCreated" PageSize="15"
                                    Width="100%">
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Wrap="true" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#efefef" VerticalAlign="Top" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdateProc" runat="server" CommandArgument='<%#Eval("[ID]")+ ";" +Eval("Name")%>'
                                                                ForeColor="Black" Height="16px" ImageUrl="~/Images/Edit.gif" OnCommand="onUpdate_Proc"
                                                                Text="Update" ToolTip="Edit" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="40px"
                                                Wrap="true" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                            <b>
                                <asp:Label ID="Label4" runat="server">Update History</asp:Label>
                            </b>
                            <div style="max-height: 400px; overflow: auto; width: 100%">
                                <asp:GridView ID="gv_ProcessingFeeHistory" runat="server" AutoGenerateColumns="False"
                                    CellPadding="1" CssClass="grd" ForeColor="#333333" GridLines="None" PageSize="15"
                                    Width="100%">
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#efefef" VerticalAlign="Top" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Updated By">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("UpdatedBy_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Updated On">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Updated_On"))) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </div>
                    <div id="divadd" style="display: none; font-family: Tahoma; text-align: left; font-size: 12px;
                        color: Black; width: 35%" title="<%= OperationMode %>">
                        <table cellpadding="2" cellspacing="2" width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlTextBoxes" runat="server">
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnGet" runat="server" OnClick="GetTextBoxValues" Text="Save" />
                                </td>
                                <td align="center" style="width: 500px; color: #FF0000; font-size: small;">
                                    * Mandatory fields
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="right" colspan="3" style="color: #FF0000; font-size: small;">
                                    <asp:Label ID="lblError" runat="server" Text="" Width="300"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
