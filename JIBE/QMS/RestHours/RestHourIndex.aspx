<%@ Page Title="Work/Rest Hours Index" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" EnableEventValidation="false" CodeFile="RestHourIndex.aspx.cs"
    Inherits="RestHourIndex" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
        <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
       <%--<script src="../../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        var lastExecutor = null;


        function asyncGet_RestHourExceptions(RestHourID, Vessel_ID, evt, objthis, isclicked, pageheader) {


            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'asyncGet_RestHourExceptions', false, { "RestHourID": RestHourID, "Vessel_ID": Vessel_ID }, onSuccess_asyncGet_RestHourExceptions, Onfail, new Array(evt, objthis, isclicked, pageheader));

            lastExecutor = service.get_executor();

        }
        function Onfail(msg) {

            alert(msg._message);
        }

        function onSuccess_asyncGet_RestHourExceptions(retVal, Args) {

            js_ShowToolTip_Fixed(retVal, Args[0], Args[1], Args[3]);
        }

        //        function OpenDetails(ID, Vessel_ID) {

        //            window.open("../RestHours/RestHourDetails.aspx?ID=" + ID + "&Vessel_ID" + Vessel_ID, "_blank");
        //        
        //        }
        
        
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <center>
        <div style="width: 1000px; color: Black;">
            <div class="page-title">
                Work/Rest Hours Index
            </div>
            <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="ImgExpExcel" />
                </Triggers>
                <ContentTemplate>
                    <div id="dvpage-content" class="page-content-main" style="padding: 1px">
                        <table cellpadding="2" cellspacing="0" width="100%">
                            <tr>
                                <td align="right">
                                    Fleet :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDLFleet" runat="server" Width="150px" AppendDataBoundItems="True"
                                        AutoPostBack="True" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">SELECT ALL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    Crew :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtCrew"  runat="server"  ></asp:TextBox>
                                </td>
                                <td align="right">
                                    From Date :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtfrom" AutoPostBack="true" runat="server" OnTextChanged="txtfrom_TextChanged"></asp:TextBox>
                                    <cc1:CalendarExtender ID="calfrom" Format="dd-MM-yyyy" TargetControlID="txtfrom"
                                        runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                    <asp:Button ID="btnRetrieve" runat="server" Font-Size="11px" Height="22px" OnClick="btnRetrieve_Click"
                                        Text="Search" Width="80px" />
                                </td>
                                <td style="width: 10%" align="center">
                                    <a href="RestHourRule.aspx" target="_blank">Rules</a>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Vessel :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlvessel" Width="150px" runat="server"  
                                         >
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    Rank :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlRank" runat="server" Width="115px"  >
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    To Date :&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtto"   runat="server"  ></asp:TextBox>
                                    <cc1:CalendarExtender ID="calto" Format="dd-MM-yyyy" TargetControlID="txtto" runat="server">
                                    </cc1:CalendarExtender>
                                </td>
                                <td>
                                    <asp:Button ID="btnClearFilter" runat="server" Font-Size="11px" Height="20px" OnClick="btnClearFilter_Click"
                                        Text="Clear Filters" Width="80px" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to Excel" OnClick="btnExport_Click"
                                        Width="16px" ImageUrl="~/Images/Excel-icon.png" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div style="border: 0px solid gray; margin-top: 0px; cursor: pointer; height: 700px;">
                            <asp:GridView ID="gvDeckLogBook" runat="server" EmptyDataText="No record found !"
                                AutoGenerateColumns="False" Width="100%" CssClass="GridView-css" GridLines="None"
                                CellPadding="4" AllowSorting="True" Style="margin-right: 0px; cursor: pointer;"
                                OnSorting="gvDeckLogBook_Sorting" OnRowDataBound="gvDeckLogBook_RowDataBound"
                                OnRowCreated="gvDeckLogBook_RowCreated" >
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vessel Name">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblVesseleNameHeader" runat="server" ForeColor="Black" CommandArgument="Vessel_Name"
                                                CommandName="Sort">Vessel&nbsp;</asp:LinkButton>
                                            <img id="Vessel_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVSLName" Text='<%#Eval("Vessel_Name")%>' runat="server" class='vesselinfo'
                                                vid='<%# Eval("Vessel_ID")%>' vname='<%# Eval("Vessel_Short_Name")%>' onclick='<%# "window.open(&#39;../RestHours/RestHourDetails.aspx?ID="+Eval("ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,&#39;_blank&#39;)" %>' ></asp:Label>
                                            <asp:Label ID="lblDeckLogBookID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                            <asp:Label ID="lblVesselID" Visible="false" runat="server" Text='<%# Eval("Vessel_ID")%>' ></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblDateFromHeader" runat="server" ForeColor="Black" CommandArgument="REST_HOURS_DATE"
                                                CommandName="Sort"> Date&nbsp;</asp:LinkButton>
                                            <img id="REST_HOURS_DATE" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblREST_HOURS_DATE" runat="server" Text='<%# Eval("REST_HOURS_DATE", "{0:dd/MM/yyyy}")%>'  
                                            onclick='<%# "window.open(&#39;../RestHours/RestHourDetails.aspx?ID="+Eval("ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,&#39;_blank&#39;)" %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblStaff_CodeHeader" runat="server" ForeColor="Black" CommandArgument="Staff_Code"
                                                CommandName="Sort"
                                               
                                                >Code</asp:LinkButton>
                                            <img id="Staff_Code" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lblStaff_Code" runat="server" Text='<%# Eval("Staff_Code") %>' CssClass="staffInfo" NavigateUrl='<%# "~/Crew/CrewDetails.aspx?ID="+Eval("Crew_ID").ToString() %>' Target="_blank" ></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Crew Name">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblStaff_NameHeader" runat="server" ForeColor="Black" CommandArgument="Staff_Name"
                                                CommandName="Sort">Crew Name</asp:LinkButton>
                                            <img id="Staff_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStaff_Name" runat="server" Text='<%# Eval("Staff_Name")%>' onclick='<%# "window.open(&#39;../RestHours/RestHourDetails.aspx?ID="+Eval("ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,&#39;_blank&#39;)" %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rank">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblStaff_RankHeader" runat="server" ForeColor="Black" CommandArgument="Staff_Rank"
                                                CommandName="Sort">Rank</asp:LinkButton>
                                            <img id="Staff_Rank" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStaff_Rank" runat="server" Text='<%# Eval("Staff_rank_Code")%>' onclick='<%# "window.open(&#39;../RestHours/RestHourDetails.aspx?ID="+Eval("ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,&#39;_blank&#39;)" %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ship's Clocked Hours">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSHIPS_CLOCKED_HOURSHeader" runat="server">Ship's Clocked Hours</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSHIPS_CLOCKED_HOURS" runat="server" Text='<%# Eval("SHIPS_CLOCKED_HOURS")%>' onclick='<%# "window.open(&#39;../RestHours/RestHourDetails.aspx?ID="+Eval("ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,&#39;_blank&#39;)" %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Work Hours">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblWORKING_HOURSHeader" runat="server">Work Hours</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblWORKING_HOURS" runat="server" Text='<%# Eval("WORKING_HOURS")%>' onclick='<%# "window.open(&#39;../RestHours/RestHourDetails.aspx?ID="+Eval("ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,&#39;_blank&#39;)" %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rest Hours">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblREST_HOURSHeader" runat="server">Rest Hours</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblREST_HOURS" runat="server" Text='<%# Eval("REST_HOURS")%>' onclick='<%# "window.open(&#39;../RestHours/RestHourDetails.aspx?ID="+Eval("ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,&#39;_blank&#39;)" %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rest Hours">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblREST_HOURSHeader24" runat="server">Rest Hours(Any 24)</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblREST_HOURS24" runat="server" Text='<%# Eval("REST_HOURS_ANY24")%>' onclick='<%# "window.open(&#39;../RestHours/RestHourDetails.aspx?ID="+Eval("ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,&#39;_blank&#39;)" %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHOverTime" runat="server">Over Time</asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOverTime" runat="server" Text='<%# Eval("OverTime_HOURS")%>' onclick='<%# "window.open(&#39;../RestHours/RestHourDetails.aspx?ID="+Eval("ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,&#39;_blank&#39;)" %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Seafarer's Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSeafarer_Remarks" runat="server" Text='<%# Eval("Seafarer_Remarks")%>' onclick='<%# "window.open(&#39;../RestHours/RestHourDetails.aspx?ID="+Eval("ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,&#39;_blank&#39;)" %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Verifier's Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVerifier_Remarks" runat="server" Text='<%# Eval("Verifier_Remarks")%>' onclick='<%# "window.open(&#39;../RestHours/RestHourDetails.aspx?ID="+Eval("ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,&#39;_blank&#39;)" %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Exceptions.">
                                        <HeaderTemplate>
                                            Exception
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                          
                                            <asp:HyperLink ID="hlnkPB" runat="server" NavigateUrl="#" Style="cursor: pointer"
                                                ImageUrl="~/Images/bullet-red-icon.png" Text="PT" onclick='<%#"asyncGet_RestHourExceptions(&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Vessel_ID").ToString()+"&#39;,event,this,1,&#39;Attachments&#39;);" %>'
                                                Visible='<%#Eval("ImgVisibility").ToString()=="No"?false:true%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem=" BindGrid"
                                PageSize="20" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </center>
</asp:Content>
