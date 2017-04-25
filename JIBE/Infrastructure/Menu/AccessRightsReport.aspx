<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="AccessRightsReport.aspx.cs"
   Inherits="Infrastructure_Menu_AccessRightsReport" Title="Access Rights Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .page
        {    
            width:100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        .pagestyle
        {
            border-left: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
            border-bottom: 1px solid #cccccc;  /*padding: 5px;  margin-top: -1px;*/
            min-height: 600px;
        }        
    </style>
    <script language="javascript" type="text/javascript">
        var X = 0;
        var Y = 0;
        function show_MenuDescription(data) {
            var Data2 = JSON.stringify({ 'menuID': data });
            $.ajax({
                url: 'AccessRightsReport.aspx/SearchMenuData',
                type: "POST",
                dataType: "json",
                data: Data2,
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (response) {
                    //If data returned then display div
                    if (response["d"] != '') {
                        js_ShowToolTip(response["d"],event, null);
                    }
                },
                error: function (xhr) {
                }
            });
        }     
        function hideheadertip() {
            divToolTip.style.display = 'none';
        }
        function showprogressbar() {
            setTimeout(function () { document.getElementById('blur-on-updateprogress').style.display = 'block'; }, 20);
        }
        function hideprogressbar() {
            document.getElementById('blur-on-updateprogress').style.display = 'none';
        }
</script>	

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress" style="display:block;">
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201; color: black">
               <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div style="border: 1px solid #cccccc" class="page-title">
                Access Rights Changes Report
            </div>
            <div id="dvpage-content" class="pagestyle" style="overflow:auto;">
            <div>
                <table style="width: 100%;">
                    <tr style="vertical-align: middle;">
                    <td align="right">
                                Module:
                            </td>
                            <td align="left">
                            <asp:DropDownList ID="ddlModule" runat="server" Width="150px" CssClass="txtInput"></asp:DropDownList>
                            </td>
                             <td align="right">
                                Page Description:
                            </td>
                            <td align="left"><asp:TextBox ID="txtPage" runat="server" Width="150px" CssClass="txtInput"></asp:TextBox></td>
                            <td align="right">
                                User:
                            </td>
                            <td align="left">                            
                            <asp:DropDownList ID="ddlUser" runat="server" Width="150px" CssClass="txtInput"></asp:DropDownList>
                            </td>
                            <td align="right">
                                From Date:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFrom" runat="server" Width="150px" CssClass="txtInput"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="caltxtFrom" runat="server" TargetControlID="txtFrom"><%-- Format='<%#Convert.ToString(Session["User_DateFormat"]) %>'>--%>
                                </ajaxToolkit:CalendarExtender>
                            
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtFrom" runat="server" ValidationGroup="saveFZ"
                                    Display="None" ErrorMessage="Please select From Date!" ControlToValidate="txtFrom"
                                    InitialValue=""></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtFromR" TargetControlID="RequiredFieldValidatortxtFrom"
                                    runat="server">
                                </ajaxToolkit:ValidatorCalloutExtender>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtendertxtFrom" TargetControlID="txtFrom"
                                WatermarkText="Enter From Date." runat="server" WatermarkCssClass="watermarked" >
                                </ajaxToolkit:TextBoxWatermarkExtender>
                            </td>
                            <td align="right">
                                To Date:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtTo" runat="server" Width="150px" CssClass="txtInput"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="caltxtTo" runat="server" TargetControlID="txtTo" Format='<%#Convert.ToString(Session["User_DateFormat"]) %>' > <%-- Format='<%#UDFLib.ConvertUserDateFormat(Eval(caltxtTo.value))%>'--%>
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtTo" runat="server" ValidationGroup="saveFZ" Display="None" ErrorMessage="Please select To Date!" ControlToValidate="txtTo" InitialValue=""></asp:RequiredFieldValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtToR" TargetControlID="RequiredFieldValidatortxtTo" runat="server"></ajaxToolkit:ValidatorCalloutExtender>
                                 <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtendertxtTo" TargetControlID="txtTo" WatermarkText="Enter To Date." runat="server" WatermarkCssClass="watermarked" >
                                </ajaxToolkit:TextBoxWatermarkExtender> 
                            </td>
                           <td></td>
                       <td style="width: 4%;"align="center"><asp:ImageButton ID="imgBtnSearch" runat="server" ValidationGroup="saveFZ"
                               ImageUrl="~/Images/SearchButton.png" onclick="imgBtnSearch_Click" ToolTip="Search" /></td>
                       <td style="width: 4%;" align="center"><asp:ImageButton ID="imgBtnrefresh" runat="server" 
                               ImageUrl="~/Images/Reload.png" onclick="imgBtnrefresh_Click" ToolTip="Refresh"/></td>
                       <td align="center" style="width: 4%;"><asp:ImageButton ID="imgBtnExport" runat="server" ValidationGroup="saveFZ"
                               ImageUrl="~/images/Exptoexcel.png" onclick="imgBtnExport_Click" ToolTip="Export to Excel"/></td>
                        <td align="center" style="width: 4%;"><asp:ImageButton ID="imgBtnRunDaemon" runat="server" 
                               ImageUrl="~/images/RunDaemon.png" onclick="imgBtnRunDaemon_Click" ToolTip="Run Daemon"/></td>
                    </tr>
                </table>
                </div>
                 <div id="divToolTip" style="display:none; position: absolute; font-style: normal; border: 4px solid rgb(255, 165, 0); padding: 4px; font-size: 11px; color: rgb(0, 0, 0); background: rgb(245, 245, 245); border-radius: 7px;" ></div>

                 <div>
                <asp:GridView ID="grdAccessReport" runat="server" CellPadding="2" OnRowDataBound="grdAccessReport_RowDataBound"
                                    OnSorting="grdAccessReport_Sorting" 
                        EmptyDataText="NO RECORDS FOUND!" AllowSorting="True"
                                    AutoGenerateColumns="False" Width="100%" GridLines="None" 
                        DataKeyNames="ID" >
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ModifiedDate" ItemStyle-Width="5%"  >
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblDateHeader" runat="server" CommandName="Sort" CommandArgument="ModifiedDate"
                                                    ForeColor="Black">Date&nbsp;</asp:LinkButton>
                                                <img id="ModifiedDate" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldate" Height="35px"  runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Date")))%>' ></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="User Name" ItemStyle-Width="8%"  >
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblUser_NameHeader" runat="server" CommandName="Sort" CommandArgument="User_name"
                                                    ForeColor="Black">User Name&nbsp;</asp:LinkButton>
                                                <img id="User_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                        <asp:Label ID="lblUser_name" runat="server" Text='<%#Eval("User_name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="First Name" ItemStyle-Width="8%"  >
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblFirst_NameHeader" runat="server" CommandName="Sort" CommandArgument="First_Name"
                                                    ForeColor="Black">First Name&nbsp;</asp:LinkButton>
                                                <img id="First_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                  <asp:Label ID="lblFirst_Name" runat="server" Text='<%#Eval("First_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Name" ItemStyle-Width="8%"  >
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblLast_NameHeader" runat="server" CommandName="Sort" CommandArgument="Last_Name"
                                                    ForeColor="Black">Last Name&nbsp;</asp:LinkButton>
                                                <img id="Last_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLast_Name" runat="server" Text='<%#Eval("Last_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Module" ItemStyle-Width="8%"  >
                                        <HeaderTemplate>
                                        <asp:LinkButton ID="lblModuleheader" runat="server" CommandName="Sort" CommandArgument="Module"
                                                    ForeColor="Black">Module&nbsp;</asp:LinkButton>
                                            <img id="Module" runat="server" visible="false" />
                                        </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblModule" runat="server" Text='<%#Eval("Module")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description" ItemStyle-Width="10%"  >
                                        <HeaderTemplate>
                                        <asp:LinkButton ID="lblDescriptionHeader" runat="server" CommandName="Sort" CommandArgument="Description"
                                                    ForeColor="Black">Page Description&nbsp;</asp:LinkButton>
                                            <img id="Description" runat="server" visible="false" />
                                        </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldescriptionHeader" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Page" ItemStyle-Width="15%" >
                                         <HeaderTemplate>
                                        <asp:LinkButton ID="lblPageHeader" runat="server" CommandName="Sort" CommandArgument="Page"
                                                    ForeColor="Black">Page Link&nbsp;</asp:LinkButton>
                                            <img id="Page" runat="server" visible="false" />
                                        </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPage" runat="server" Text='<%#Eval("Page")%>' onmouseover='<%#"show_MenuDescription(" + Eval("ID").ToString() + ",this);"%>' onmouseout="hideheadertip();" ></asp:Label><%-- --%>
                                                                                               
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Changes" ItemStyle-Width="20%"  >
                                        <HeaderTemplate>
                                        <asp:LinkButton ID="lblChangesHeader" runat="server" CommandName="Sort" CommandArgument="Changes"
                                                    ForeColor="Black">Changes&nbsp;</asp:LinkButton>
                                            <img id="Changes" runat="server" visible="false" />
                                        </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblChanges" runat="server" Text='<%#Eval("Changes")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chaged_By" ItemStyle-Width="8%">
                                        <HeaderTemplate>
                                        <asp:LinkButton ID="lblChaged_ByHeader" runat="server" CommandName="Sort" CommandArgument="Chaged_By"
                                                    ForeColor="Black">Changed By&nbsp;</asp:LinkButton>
                                            <img id="Chaged_By" runat="server" visible="false" />
                                        </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblChaged_By" runat="server" Text='<%#Eval("Chaged_By")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UserIP" ItemStyle-Width="10%"  >
                                        <HeaderTemplate>
                                        <asp:LinkButton ID="lblUserIPHeader" runat="server" CommandName="Sort" CommandArgument="UserIP"
                                                    ForeColor="Black">User IP&nbsp;</asp:LinkButton>
                                            <img id="UserIP" runat="server" visible="false" />
                                        </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserIP" runat="server" Text='<%#Eval("UserIP")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>                                        
                                    </Columns>
                                </asp:GridView>
                <uc1:ucCustomPager ID="ucCustomPagerItems" Visible="True" runat="server" PageSize="100" OnBindDataItem="BindAccessGrid" />
                </div>
            </div>
            </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnExport" />
            </Triggers>
            </asp:UpdatePanel>
</asp:Content>
