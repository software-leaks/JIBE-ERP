<%@ Page Title="Bunker Sample Lab" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="BunkerSampleLab.aspx.cs" Inherits="Operations_BunkerSampleLab" %>

<%@ Register Src="~/UserControl/ctlPortList.ascx" TagName="PortList" TagPrefix="uc" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link type="text/css" href="../../styles/ui-lightness/jquery-ui-1.8.14.custom.css"
        rel="stylesheet" />
    <script type="text/javascript" src="../../scripts/jquery-1.5.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery-ui-1.8.14.custom.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.draggable').draggable();
        });

        $(function () {
            // Dialog			
            $('#dialog').dialog({
                autoOpen: false,
                modal: true,
                width: 600,
                height: 300,
                buttons: {
                    "Close": function () {
                        $(this).dialog("close");
                    },
                    "Reload": function () {
                        var url = "BunkerAttachments.aspx?id=" + $('#dialog').attr('alt') + "&rnd=" + Math.random();

                        $("#dialog").dialog({ title: 'Loading Data ...' });

                        $.get(url, function (data) {
                            $('#dialog').html(data);
                            $("#dialog").dialog({ title: 'Attachments' });
                        });
                    }
                }
            });

            //hover states on the static widgets
            $('#dialog_link, ul#icons li').hover(
					function () { $(this).addClass('ui-state-hover'); },
					function () { $(this).removeClass('ui-state-hover'); }
				);

        });

//        function showAttachments(ID, Type) {
//            var url = "BunkerAttachments.aspx?id=" + ID + "&Type=" + Type + "&rnd=" + Math.random();
        function showAttachments(ID, Vessel_Id, Type) {
            var url = "BunkerAttachments.aspx?id=" + ID + "&Vessel_Id=" + Vessel_Id + "&Type=" + Type + "&rnd=" + Math.random();
            //            //-- show dialog --
            //            $('#dialog').dialog('open');

            //            //-- load event data --
            //            $.get(url, function (data) {
            //                $('#dialog').html(data);
            //            });

            //            //-- remember event id --
            //            $('#dialog').attr('alt', ID + "&Type=" + Type);

            showDiv('dvAttachments', url)
        }
    </script>
    <script language="javascript" type="text/javascript">
        function showDiv(dv, src) {
            if (src)
            { document.getElementById("frmAttachments").src = src; }

            document.getElementById(dv).style.display = "block";
        }
        function closeDiv(dv) {
            document.getElementById(dv).style.display = "None";
        }
        function reloadFrame(fr) {
            document.getElementById(fr).src = document.getElementById(fr).src + "&rnd=" + Math.random;
        }

    </script>
    <style type="text/css">
        #dialog
        {
            font-family: Verdana;
            font-size: 10px;
        }
        .popup-header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
            background-color: #5C87B2;
        }
        h4
        {
            font-size: 1.2em;
            color: #ffffff;
            font-weight: bold;
            margin: 0 0 0 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="page-title" style="border: 1px solid #cccccc; height: 20px; vertical-align: bottom;
        background: url(../../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
        padding: 2px; background-color: #F6CEE3; text-align: center; font-weight: bold;">
        <div style="float: right; display: none;">
            <asp:ImageButton ID="ImageButton1" ImageUrl="../../Images/Excel-icon.png" Height="20px"
                runat="server" OnClick="ImgExportToExcel_Click" AlternateText="Print" /></div>
        <div>
            Bunker Sample Lab</div>
    </div>
    <div id="dvMain" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; padding: 2px;">
        <div id="dvFilter">
            <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnSearch">
                        <table border="0" cellpadding="0" cellspacing="4" style="width: 100%;">
                            <tr>
                                <td>
                                    Fleet
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFleet" runat="server" Width="156px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Status
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="156px">
                                        <asp:ListItem Text="-SELECT ALL-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Within Spec" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Out-Of-Spec" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Bunker Date From:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDateFrom" runat="server" Width="80px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDateFrom"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: right">
                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                        Width="80px" CssClass="btnCSS" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Vessel
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlVessel" runat="server" Width="156px" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <%--<asp:DropDownList ID="ddlBunkerSupplier" runat="server" Width="156px" AppendDataBoundItems="true"
                                        DataTextField="SUPPLIER_NAME" DataValueField="ID">
                                        <asp:ListItem Text="-SELECT ALL-" Value="0"></asp:ListItem>
                                    </asp:DropDownList>--%>
                                </td>
                                <td>
                                    To:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDateTo" runat="server" Width="80px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateTo"
                                        Format="dd/MM/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                    <%--<asp:DropDownList ID="ddlLabList" runat="server" Width="200px" AppendDataBoundItems="true"
                                        DataTextField="SUPPLIER_NAME" DataValueField="ID">
                                        <asp:ListItem Text="-SELECT ALL-" Value="0"></asp:ListItem>
                                    </asp:DropDownList>--%>
                                </td>
                                <td style="text-align: right">
                                    <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" Text="Clear Filter"
                                        Width="80px" CssClass="btnCSS" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="grid-container" style="margin-top: 5px; border: 1px solid #cccccc; padding: 2px;">
            <asp:UpdatePanel ID="UpdatePanel_Bunker" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView_Bunker" runat="server" PageSize="15" CellPadding="1" EmptyDataText="No record found!"
                        AllowSorting="false" AllowPaging="false" AutoGenerateColumns="False" ShowFooter="false"
                        Width="100%" CssClass="grd" ForeColor="#333333" GridLines="None" DataKeyNames="ID,Vessel_ID"
                        OnRowDataBound="GridView_Bunker_RowDataBound" OnRowCommand="GridView_Bunker_RowCommand"
                        OnRowCancelingEdit="GridView_Bunker_RowCancelEdit" OnRowEditing="GridView_Bunker_RowEditing"
                        OnRowUpdating="GridView_Bunker_RowUpdating">
                        <FooterStyle BackColor="#FFF8C6" ForeColor="#333333" Font-Bold="true" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                        <EditRowStyle VerticalAlign="Top" BackColor="#efefef" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblVsl" runat="server" Text='<%# Eval("Vessel_Code")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="60px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Grade" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("Grade")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="60px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bunkering Date" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Bunkering_Date","{0:dd/MM/yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Bunkering_Date","{0:dd/MM/yyyy}")%>'></asp:Label>
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Port" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblPort" runat="server" Text='<%# Eval("Port_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="150px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lab" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblLab" runat="server" Text='<%# Eval("Lab_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:CheckBoxField DataField="SampleReceived_ByLab" HeaderText="Sample Received"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" />
                            <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="106px" Text='<%# Bind("StatusID") %>'>
                                        <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Within Spec" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Out-Of-Spec" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Sample Received" HeaderStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSampleReceived" runat="server" Text='<%# Eval("SampleReceived_ByLab").ToString()=="1"?"YES":"NO"%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rdoSampleReceived" runat="server" Width="106px" RepeatDirection="Horizontal" Text='<%# Bind("SampleReceived_ByLab") %>'>                                        
                                        <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Report" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAddReport" runat="server" Text="Upload" Visible='<%# !Convert.ToBoolean(Eval("ReportCount")) %>'
                                      OnClientClick='<%# "showAttachments(" + Eval("ID").ToString() + "," + Eval("Vessel_ID").ToString()  + ",1)" %>'>
                                    </asp:LinkButton>
                                    <asp:ImageButton ID="ImgReport" runat="server" ImageUrl="~/images/Attach.png" Visible='<%# Convert.ToBoolean(Eval("ReportCount")) %>'
                                        OnClientClick='<%# "showAttachments(" + Eval("ID").ToString() + "," + Eval("Vessel_ID").ToString()  + ",1)" %>'></asp:ImageButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BDN & Others" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAddAttachment" runat="server" Text="Upload" Visible='<%# !Convert.ToBoolean(Eval("AttachmentCount")) %>'
                                       OnClientClick='<%# "showAttachments(" + Eval("ID").ToString() + "," + Eval("Vessel_ID").ToString()  + ",2)" %>'>
                                    </asp:LinkButton>
                                    <asp:ImageButton ID="ImgAttachments" runat="server" ImageUrl="~/images/Attach.png"
                                        Visible='<%# Convert.ToBoolean(Eval("AttachmentCount")) %>' OnClientClick='<%# "showAttachments(" + Eval("ID").ToString() + "," + Eval("Vessel_ID").ToString()  + ",2)" %>'>
                                    </asp:ImageButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <%--                            <asp:TemplateField HeaderText="Sample Received" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgMailAck" runat="server" AlternateText="Acknowledge"
                                        CommandName="SendMailAcknowledge" CommandArgument='<%# Eval("ID")%>' CausesValidation="False"
                                        ImageUrl="~/images/EMail.png" />
                                    <asp:Label ID="lblSampleReceived" runat="server" Text='<%# Eval("SampleReceived_ByLab").ToString()=="1"?"YES":"NO"%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkSampleReceived" runat="server" Checked='<%# Eval("SampleReceived_ByLab").ToString()=="1"?true:false%>' />
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="30px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgEdit" runat="server" AlternateText="Edit" CausesValidation="False"
                                        CommandName="Edit" ImageUrl="~/images/edit.gif" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="ImgBtnAccept" runat="server" AlternateText="Update" CausesValidation="False"
                                        CommandName="Update" ImageUrl="~/images/accept.png" />
                                    <asp:ImageButton ID="ImgBtnCancel" runat="server" AlternateText="Cancel" CausesValidation="False"
                                        CommandName="Cancel" ImageUrl="~/images/reject.png" />
                                </EditItemTemplate>
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <div style="margin: 2px; vertical-align: bottom; padding: 2px; background: url(../Images/bg.png) left -10px repeat-x;
                        color: Black; text-align: left; background-color: #F6CEE3;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <uc1:ucCustomPager ID="ucCustomPager" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Records" PageSize="30"
                                        OnBindDataItem="Load_BunkerSampleAnalysis" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="dialog" title="Attachments">
        Loading Data ...
    </div>
    <div id="dvAttachments" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 40%; top: 15%; width: 600px; z-index: 1; color: black">
        <div class="popup-header">
            <div style="right: 0px; position: absolute; cursor: hand;">
                <img src="../../Images/Reload.gif" onclick="reloadFrame('frmAttachments');" alt="Reload" />
                <img src="../../Images/Close.gif" onclick="closeDiv('dvAttachments');" alt="Close" />
            </div>
            <h4>
                Bunker Analysis Attachments</h4>
        </div>
        <div class="popup-content">
            <iframe id="frmAttachments" src="" frameborder="0" style="height: 350px; width: 100%">
            </iframe>
        </div>
    </div>
</asp:Content>
