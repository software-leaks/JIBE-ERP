<%@ Page Title="Engine Log Book Index" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="ERLogIndex.aspx.cs" Inherits="Technical_ERLog_ERLogIndex" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <style type="text/css">
        .AnomalyCell
        {
            background-color: #FA5858;
            color: White;
        }
        .NoAnomaly
        {
            background-color: #31bc1a;
            color: Black;
        }
    </style>
    <script type="text/javascript">
        function showDialog(url) {
            window.open(url);
        }
        function showEmailNotifiers() {
            document.getElementById("dvEmailNotifiers").style.display = 'block';
        }
        function hideEmailNotifiers() {
            document.getElementById("dvEmailNotifiers").style.display = 'none';
        }
        function showFollowups(V, W, O) {

            var evt = window.event || O; // this assign evt with the event object
            var src = evt.srcElement; // this assign current with the event target
            var pos = 0;
            var width = 0;
            var x = 0;
            var y = 0;
            if (src == null) {
                src = evt.target;
                x = evt.x;
                y = evt.y;

            }
            else {
                pos = $(src).offset();
                width = $(src).width();
                x = pos.left;
                y = pos.top;
            }
            // var src = window.event.srcElement;


            var url = 'ErLogTask_Followups.aspx?LID=' + W + '&VID=' + V;

            $('#iframeFollowups').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });
            $('#iframeFollowups').attr("src", url);
            $('#dialog').show();
            $("#dialog").css({ "left": (x - 600) + "px", "top": y + "px", "width": 600 });


        }
        function showDiv(dv) {
            if (dv) {
                $('#' + dv).show();
            }
        }
        function closeDiv(dv) {
            if (dv) {
                $('#' + dv).hide();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2;">
        <center>
            <asp:UpdateProgress ID="upUpdateProgress" runat="server">
                <ProgressTemplate>
                    <div id="blur-on-updateprogress">
                        &nbsp;</div>
                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                        color: black">
                        <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <div id="page-header" class="page-title">
                    <b>Engine Room Log Book Index </b>
                </div>
                <div style="color: Black;">
                    <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                        <ContentTemplate>
                            <div style="margin: 1px; padding: 1px; border: 1px solid #cccccc;">
                                <table width="100%" cellpadding="1" cellspacing="0">
                                    <tr>
                                        <td width="100%" valign="top" style="border: 1px solid gray; color: Black">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table width="100%" cellpadding="3" cellspacing="0">
                                                        <tr>
                                                            <td width="20%" align="right" valign="top">
                                                                Fleet :
                                                            </td>
                                                            <td width="25%" valign="top" align="left">
                                                                <asp:DropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged" AutoPostBack="true"
                                                                     Width="160" />
                                                            </td>
                                                            <td width="20%" align="right" valign="top">
                                                                Date From :
                                                            </td>
                                                            <td width="25%" valign="top" align="left">
                                                                <asp:TextBox ID="txtFromDate" CssClass="input" runat="server" Width="120px"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td width="10%">
                                                                &nbsp;&nbsp;
                                                                <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to Excel" OnClick="ImgExpExcel_Click"
                                                                    Width="16px" ImageUrl="~/Images/Excel-icon.png" />
                                                                &nbsp;&nbsp;
                                                                <img src="../../Images/Printer.png" title="*Print*" style="cursor: hand;" alt="Print" onclick="PrintDiv('divGrid')" />
                                                                &nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%" align="right" valign="top">
                                                                Vessel :
                                                            </td>
                                                            <td width="25%" valign="top" align="left">
                                                                <asp:DropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged" AutoPostBack="true"  
                                                                      Width="160" />
                                                            </td>
                                                            <td width="20%" align="right" valign="top">
                                                                Date To :
                                                            </td>
                                                            <td width="25%" valign="top" align="left">
                                                                <asp:TextBox ID="txtToDate" CssClass="input" runat="server" Width="120px"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="calToDate" runat="server" Enabled="True" TargetControlID="txtToDate"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnSearch" Text="Search" runat="server" Width="90px" ToolTip="Search"
                                                                    OnClick="btnSearch_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divGrid" >
                                <asp:GridView ID="gvERLogIndex" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvERLogIndex_RowDataBound" DataKeyNames="LOG_ID" CellPadding="3"
                                    GridLines="None" CellSpacing="0" Width="100%" OnSorting="gvERLogIndex_Sorting"
                                    OnRowCommand="gvERLogIndex_RowCommand" AllowSorting="true" Font-Size="12px" CssClass="GridView-css">
                                    <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                    <PagerStyle CssClass="PagerStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="9">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgBtn" ImageUrl="~/Images/plus.gif" CommandName="Expand"
                                                    CommandArgument='<% #Eval("Vessel_ID").ToString()+";"+Eval("LOG_DATE").ToString()  %>'
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vessel Name">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbtVesslNameHeader" runat="server">Vessel Name</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("VESSEL_NAME")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="130px"></ItemStyle>
                                            <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblEngineLogDateHeader" runat="server">Date</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEngineLogDate" runat="server" style="cursor:pointer" Text='<%# DataBinder.Eval(Container,"DataItem.LOG_DATE","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="125px"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voyage Number" Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblVoyageNumberHeader" runat="server">Voyage Number</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtVoyageNumberr" runat="server" CommandArgument='<%#Eval("LOG_ID") + ","+ Eval("VESSEL_ID")%>'
                                                    CommandName="ViewRequest" Text='<%#Eval("VOYAGE_NUM") %>' Style="color: Black"></asp:LinkButton>
                                                <asp:HiddenField ID="hdfAnomalyValue" runat="server" Value='<%#Eval("Anomaly_Value")%>' />
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vessel">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblFromPortHeader" runat="server">From</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblFromPort" runat="server" Text='<%#  DataBinder.Eval(Container,"DataItem.FROMPORT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="200"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To Port">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbtToPortHeader" runat="server">To</asp:Label>
                                                <img id="TOPORT" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lbtToPort" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.TOPORT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="200"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblCERemarks" runat="server">CE Remarks</asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblCERemarks" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CE_Shor_Remarks") %>'></asp:Label>
                                                <asp:Label ID="lblCEFullRemarks" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CE_REMARKS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Justify"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="FollowUps">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgRemarks" runat="server" ImageUrl='~/Images/remark.gif' CssClass="job-remarks"
                                                       >
                                                    </asp:Image>
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" />
                                            </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblActionHeader" runat="server">
                                                    Action
                                                </asp:Label>
                                            </HeaderTemplate>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="../../Images/RecordInformation.png"
                                                    Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;TEC_ERL_DTL_ERLOGDETAILS&#39;,&#39; LOG_ID="+Eval("LOG_ID").ToString()+" and Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,event,this)" %>'
                                                    AlternateText="info" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField >
                                            <ItemTemplate>
                                                <tr>
                                                </tr>
                                                <td colspan="8">
                                                    <%--   <asp:Panel ID="objPHLHanomalies" runat="server" Visible="false">--%>
                                                    <asp:UpdatePanel runat="server" ID="objPHLHanomalies" Visible="false">
                                                        <ContentTemplate>
                                                       
                                                            <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="true">
                                                                <asp:GridView ID="grdLHanomalies" GridLines="None" AutoGenerateColumns="false" runat="server" style="width:30%;margin:0px 0px 0px 100px"  OnRowDataBound="grdLHanomalies_RowDataBound"
                                                                    CssClass="GridView-css">
                                                                    <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                                                    <PagerStyle CssClass="PagerStyle-css" />
                                                                    <RowStyle CssClass="RowStyle-css" />
                                                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Center"  >
                                                                 
                                                                            <ItemTemplate>
                                                                            &nbsp;&nbsp;
                                                                                <asp:Label ID="lblLogWatchId" runat="server" Text='<%#Eval("ID")+"."%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                             <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="20px"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Log Watch"  HeaderStyle-HorizontalAlign="Center">
                                                                          
                                                                            <ItemTemplate  >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                      <asp:LinkButton ID="lnkbGoToDetails" runat="server" >
                                                                           
                                                                          
                                                                           <asp:Label ID="lblLogWatch" runat="server" Text='<%#Eval("LOG_WATCH")%>' ></asp:Label>                                                                               
                                                                          </asp:LinkButton>
                                                                             
                                                                            </ItemTemplate>
                                                                            <ItemStyle Wrap="true" HorizontalAlign="Justify" Width="60px"></ItemStyle>
                                                                        </asp:TemplateField>
                                              
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:PlaceHolder>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <%--   </asp:Panel>--%>
                                                    <%--    <asp:PlaceHolder ID="objPHLHanomalies" runat="server" Visible="true">
                                                            <asp:UpdatePanel runat="server" ID="ChildControl">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="grdLHanomalies" GridLines="None" AutoGenerateColumns="false" runat="server"
                                                                        CssClass="mGrid">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Sr.No">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblLogWatchIdHeader" runat="server">Sr.No</asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLogWatchId" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Log Watch">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblLogWatchHeader" runat="server">LOG_WATCh</asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLogWatch" runat="server" Text='<%#Eval("LOG_WATCH")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Anomaly">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblAnomalyHeader" runat="server">Anomaly</asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Image ID="anoIcon" runat="server" ImageUrl='<%# Eval("Anomaly_Value").ToString()=="0"?"~/Images/correct_icon.png":"~/Images/incorrect_icon.png"  %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </asp:PlaceHolder>--%>
                                                </td>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div style="margin-top: 2px; border: 1px solid #cccccc; vertical-align: bottom; padding: 2px;
                                background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindIndex" />
                                            <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                         <Triggers>
            <asp:PostBackTrigger ControlID="ImgExpExcel" />
        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </center>
    </div>
        <div id="dialog" title="Follow-ups" style="top: 0px; left: 0px; width: 600px; display: none;
        position: absolute;">
       
        <iframe id="iframeFollowups" style="width: 100%; height: 100%; border: 0px;"></iframe>
    </div>
</asp:Content>
