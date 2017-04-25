<%@ Page Language="C#" 
    AutoEventWireup="true" CodeFile="Vetting_ExternalInspector.aspx.cs" Inherits="Technical_Vetting_Vetting_ExternalInspector"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>

    <script language="javascript" type="text/javascript">


        function AddExternalInspector() {
            var Mode;
            document.getElementById('IframeAddExternalInspector').src = "Vetting_AddExternalIspector.aspx?Mode=Add&Page=ExternalLib";
            $("#dvAddExternalInspector").prop('title', 'Add Inspector');
            showModal('dvAddExternalInspector');
            return false;
        }


        function EditExternalInspector(InspectorID) {
            var Mode;
            document.getElementById('IframeAddExternalInspector').src = "Vetting_AddExternalIspector.aspx?InspectorId=" + InspectorID + "&Mode=Edit&Page=ExternalLib";
            $("#dvAddExternalInspector").prop('title', 'Edit Inspector');
            showModal('dvAddExternalInspector');
            return false;
        }

        function UpdatePage() {


          //  hideModal("dvAddExternalInspector");
            __doPostBack("<%=btnFilter.UniqueID %>", "onclick");

        }
       

    </script>
    <style type="text/css">
        .gridmain-css tr
        {
            height: 30px;
        }
        .gridmain-css tr:hover
        {
            background-color: #feecec;
        }
        #cke_show_borders p
        {
            margin: 8px 8px 8px 8px !important;
        }
       body {  
   
        font-family: Tahoma;
        font-size: 12px;
        margin: 0;
        padding: 0;
       }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

   <div id="divLoggout" runat="server" style="color: red; font-size: 14px; text-align: center;">
                Session expired!! Please log out and login again
            </div>
       <div align="center" id="MainContent" runat="server">
     <div id="AccessMsgDiv" runat="server" style="color: Red; font-size: 14px; text-align: center;">
            You don't have sufficient privilege to access the requested page.</div>
            <div id="MainDiv" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 1200px">
                    <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFilter">
                        <table cellpadding="2" cellspacing="4" style="float: left;" width="100%">
                            <tr>
                                <td align="right" style="width: 10%">
                                    Inspector Name :&nbsp;
                                </td>
                                <td align="left" style="width: 40%">
                                    <asp:TextBox ID="txtfilter" runat="server" Width="100%" Height="18px"></asp:TextBox>
                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                        WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                </td>
                                <td align="center" style="width: 1%">
                                    <asp:ImageButton ID="btnFilter" runat="server" TabIndex="0" OnClick="btnFilter_Click"
                                        ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                </td>
                                <td align="center" style="width: 1%">
                                    <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                        ImageUrl="~/Images/Refresh-icon.png" />
                                </td>
                                   <td align="center" style="width: 1%">
                                    <asp:ImageButton ImageUrl="~/Images/Add-icon.png" ClientIDMode="Static" ID="ImgAdd"
                                        runat="server" Style="cursor: pointer; height: 20px;" ToolTip="Add new inspector"
                                        OnClientClick="return AddExternalInspector();" />
                                </td>
                                <td style="width: 1%">
                                    <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                        ImageUrl="~/Images/Exptoexcel.png" />
                                </td>
                             
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div align="center" style="width: 1200px">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvExternalInspector" runat="server" EmptyDataText="NO RECORDS FOUND"  ShowHeaderWhenEmpty="true"
                                CellPadding="0" CellSpacing="2" Width="100%" AllowSorting="true" CssClass="gridmain-css"
                                AutoGenerateColumns="false" OnSorting="gvExternalInspector_Sorting" OnRowDataBound="gvExternalInspector_RowDataBound">
                                <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Inspector Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInspector_ID" runat="server" Text='<%#Eval("Inspector_ID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inspector Name">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblReasonHeader" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                CommandArgument="Inspector_Name" ForeColor="Black">Inspector Name</asp:LinkButton>
                                            <img id="Inspector_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                        <ItemTemplate>

                                            <asp:Label ID="lblInspectorName" runat="server" Text='<%#Eval("Inspector_Name")%>'></asp:Label>                                         
                                            <asp:Label ID="lblInspector_EditID" runat="server" Text='<%#Eval("Inspector_ID")%>'
                                                Style="display: none;"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="15%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company Name">
                                    <HeaderTemplate>
                                     <asp:LinkButton ID="lblReasonHeader1" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                CommandArgument="Company_Name" ForeColor="Black">Company Name</asp:LinkButton>

                                                 <img id="Company_Name" runat="server" visible="false" />
                                    </HeaderTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompany_Name" runat="server" Text='<%#Eval("Company_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="15%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Type">
                                     <HeaderTemplate>
                                     <asp:LinkButton ID="lblReasonHeader2" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                CommandArgument="Document_Type" ForeColor="Black">Document Type</asp:LinkButton>
                                                 <img id="Document_Type" runat="server" visible="false" />
                                    </HeaderTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocument_Type" runat="server" Text='<%#Eval("Document_Type")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="15%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Number">
                                     <HeaderTemplate>
                                     <asp:LinkButton ID="lblReasonHeader3" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                CommandArgument="Document_Number" ForeColor="Black">Document Number</asp:LinkButton>
                                                 <img id="Document_Number" runat="server" visible="false" />
                                    </HeaderTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocument_Number" runat="server" Text='<%#Eval("Document_Number")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="15%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vetting Type" Visible="true">
                                    <HeaderTemplate>
                                     <asp:LinkButton ID="lblReasonHeader4" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                CommandArgument="Vetting_Type_Name" ForeColor="Black">Vetting Type</asp:LinkButton>
                                                 <img id="Vetting_Type_Name" runat="server" visible="false" />
                                    </HeaderTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblVetting_Type" runat="server" Text='<%#Eval("Vetting_Type_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="15%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image" Visible="true">
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Image ID="imgIspector" runat="server" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.Image") %>'
                                                Height="20px" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Edit" CssClass="edit" ImageUrl="~/Images/Edit.gif" ForeColor="Black"
                                                ToolTip="Edit" runat="server" rel='<%#Eval("Inspector_ID").ToString() %>' Height="16px" Visible='<%# uaEditFlag %>'
                                                Width="16px" Style="cursor: pointer;" OnClick='<%# "EditExternalInspector(&#39;"+Eval("Inspector_ID").ToString()+"&#39;)" %>'>
                                            </asp:ImageButton>
                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                OnClientClick="return confirm('Are you sure, you want to delete?')" CommandArgument='<%#Eval("[Inspector_ID]")%>'
                                                ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px" Width="16px" Visible='<%# uaDeleteFlage %>'></asp:ImageButton>
                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                Height="16px" Style="vertical-align: top; cursor: pointer;" Width="16px" runat="server"
                                                onclick='<%# "Get_Record_Information(&#39;VET_LIB_ExternalInspector&#39;,&#39;Inspector_ID="+Eval("Inspector_ID").ToString()+"&#39;,event,this)" %>' />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="20" OnBindDataItem="BindGrid" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ImgExpExcel" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <div id="dvAddExternalInspector" style="display: none; width: 500px;" align="left"
            title="Add Inspector">
            <iframe id="IframeAddExternalInspector" src="" frameborder="0" style="height: 300px;
                width: 500px;"></iframe>
        </div>
        <asp:HiddenField ID="hdnInspectorId" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="HiddenFlagAdd" runat="server" />
    </div>
    </div>
</form>
</body>
</html>
