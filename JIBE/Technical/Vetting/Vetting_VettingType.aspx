<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vetting_VettingType.aspx.cs" Inherits="Technical_Vetting_Vetting_VettingType" %>

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
        function showAddVettingType() {
            $("#divVettingType").prop('title', 'Add Vetting Type');
            document.getElementById($('[id$=hdnVetTypeID]').attr('id')).value = "";
            document.getElementById($('[id$=txtVettingTypeName]').attr('id')).value = "";
            document.getElementById($('[id$=txtExInDays]').attr('id')).value = "";
            document.getElementById($('[id$=chkEIsInternal]').attr('id')).checked = false;
            showModal('divVettingType');




            return true;
        }

        function ValidateVettingType() {
            var VettingTypeName = document.getElementById($('[id$=txtVettingTypeName]').attr('id')).value.trim();
            var chkIsApp = document.getElementById($('[id$=chkIsApp]').attr('id'));
            var txtExInDays = document.getElementById($('[id$=txtExInDays]').attr('id'))
            if (VettingTypeName == "") {
                alert('Enter Vetting Type name');
                return false;
            }
            if (chkIsApp.checked == true) {
                if (txtExInDays.value == "") {
                    alert('Enter Valid For days');
                    return false;
                }
            }
        }
        function SetTitleonEdit() {
            $("#divVettingType").prop('title', 'Edit Vetting Type');
        }

        function SetTitleonAdd() {
            $("#divVettingType").prop('title', 'Add Vetting Type');
        }


        function onEditClick(VetTypeID, VetTypeName, ExInDays, IsInternal, IsActive) {
            $("#divVettingType").prop('title', 'Edit Vetting Type');
            showModal('divVettingType');
            document.getElementById($('[id$=hdnVetTypeID]').attr('id')).value = VetTypeID;
            document.getElementById($('[id$=txtVettingTypeName]').attr('id')).value = VetTypeName;

            if (ExInDays == "N/A") {
                document.getElementById($('[id$=chkIsApp]').attr('id')).checked = false;
                document.getElementById($('[id$=txtExInDays]').attr('id')).disabled = true;
                document.getElementById($('[id$=txtExInDays]').attr('id')).value = "";

            }
            else {
                document.getElementById($('[id$=txtExInDays]').attr('id')).value = ExInDays;
                document.getElementById($('[id$=txtExInDays]').attr('id')).disabled = false;
                document.getElementById($('[id$=chkIsApp]').attr('id')).checked = true;
            }
            if (IsInternal == "True") {
                document.getElementById($('[id$=chkEIsInternal]').attr('id')).checked = true;
            }
            else {
                document.getElementById($('[id$=chkEIsInternal]').attr('id')).checked = false;
            }
            if (IsActive == "Y") {
                document.getElementById($('[id$=chkActive]').attr('id')).checked = true;
                document.getElementById($('[id$=txtVettingTypeName]').attr('id')).disabled = false;
                document.getElementById($('[id$=txtExInDays]').attr('id')).disabled = false;
                document.getElementById($('[id$=chkEIsInternal]').attr('id')).disabled = false;
                document.getElementById($('[id$=chkIsApp]').attr('id')).disabled = false;
            }
            else {
                document.getElementById($('[id$=chkActive]').attr('id')).checked = false;

                document.getElementById($('[id$=txtVettingTypeName]').attr('id')).disabled = true;
                document.getElementById($('[id$=txtExInDays]').attr('id')).disabled = true;
                document.getElementById($('[id$=chkEIsInternal]').attr('id')).disabled = true;
                document.getElementById($('[id$=chkIsApp]').attr('id')).disabled = true;
            }


            return true;
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
                <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 950px">
                    <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFilter">
                        <table cellpadding="2" cellspacing="4" style="float: left;" width="100%">
                            <tr>
                                <td align="right" style="width: 10%">
                                    Vetting Type Name :&nbsp;
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
                                        Height="22px" runat="server" Style="cursor: pointer;" ToolTip="Add New Vetting Type"
                                        OnClientClick="return showAddVettingType();" />
                                </td>
                                <td style="width: 1%">
                                    <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                        ImageUrl="~/Images/Exptoexcel.png" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div align="center" style="width: 950px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvVettingType" runat="server" EmptyDataText="NO RECORDS FOUND"
                                CellPadding="0" ShowHeaderWhenEmpty="true" CellSpacing="2" Width="100%" AllowSorting="true"
                                CssClass="gridmain-css" AutoGenerateColumns="false" OnSorting="gvVettingType_Sorting"
                                OnRowDataBound="gvVettingType_RowDataBound">
                                <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Vetting Type ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVetTypeId" runat="server" Text='<%#Eval("Vetting_Type_ID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vetting Type">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblReasonHeader" Style="margin-left: 2px; text-decoration: none;"
                                                runat="server" CommandName="Sort" CommandArgument="Vetting_Type_Name" ForeColor="Black">Vetting Type</asp:LinkButton>
                                            <img id="Vetting_Type_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="left" Width="20%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblVetTypeName" runat="server" Text='<%#Eval("Vetting_Type_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="20%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active">
                                        <HeaderTemplate>
                                            Active
                                        </HeaderTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="center" Width="2%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsActive" runat="server" Text='<%#Eval("IsActive")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="center" Width="2%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valid for(Months)">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblReasonHeader1" Style="margin-left: 2px; text-decoration: none;"
                                                runat="server" CommandName="Sort" CommandArgument="Expires_In_Days" ForeColor="Black">Valid for(Validity days)</asp:LinkButton>
                                            <img id="Expires_In_Days" runat="server" visible="false" />
                                        </HeaderTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="center" Width="5%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblExInDays" runat="server" Text='<%#Eval("Expires_In_Days")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="center" Width="5%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Internal">
                                        <HeaderTemplate>
                                            IsInternal
                                        </HeaderTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="center" Width="2%" />
                                        <ItemTemplate>                                           
                                            <asp:CheckBox ID="chkIsInternal" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsInternal").ToString()=="NO" ? false : true%>'
                                                Enabled="false" />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="center" Width="2%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 80%;" align="left">
                                                <tr>
                                                    <td style="width: 10%;">
                                                        <asp:ImageButton ID="Edit" CssClass="edit" ImageUrl="~/Images/Edit.gif" ForeColor="Black"
                                                            ToolTip="Edit" runat="server" Height="16px" Width="16px" Style="cursor: pointer;" Visible='<%# uaEditFlag %>'/>
                                                    </td>
                                                    <td style="width: 10%;">
                                                       <div id="divDelete"  Visible='<%# uaDeleteFlage %>' runat="server">
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                            OnClientClick="return confirm('Are you sure, you want to delete?')" CommandArgument='<%#Eval("[Vetting_Type_ID]")%>'
                                                            ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px" Width="16px" Visible='<%# DataBinder.Eval(Container.DataItem, "IsActive").ToString()=="N" ? false : true%>'>
                                                        </asp:ImageButton>
                                                        </div>
                                                    </td>
                                                    <td style="width: 10%;">
                                                        <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                            Height="16px" Style="vertical-align: top; cursor: pointer;" Width="16px" runat="server"
                                                            onclick='<%# "Get_Record_Information(&#39;VET_LIB_VettingType&#39;,&#39;Vetting_Type_ID="+Eval("Vetting_Type_ID").ToString()+"&#39;,event,this)" %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="3%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Width="3%" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                            </asp:GridView>
                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="10" OnBindDataItem="BindGrid" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ImgExpExcel" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <div id="divVettingType" style="display: none; font-family: Tahoma; text-align: left;
            font-size: 12px; color: Black; width: 380px; height: 220px;">
            <asp:UpdatePanel runat="server" ID="upoilmajor" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellpadding="2" cellspacing="2" style="padding-top: 20px;">
                        <tr>
                            <td align="left" style="width: 120px; padding-left: 20px; vertical-align: top;">
                                Vetting Type Name :
                            </td>
                            <td style="color: #FF0000;" align="left">
                                *
                            </td>
                            <td align="left" style="padding-left: 5px;">
                                <asp:TextBox ID="txtVettingTypeName" MaxLength="200" Width="200px" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hdnVetTypeID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100px; padding-left: 20px; vertical-align: top;" colspan="2">
                                Valid For Is Applicable :
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="chkIsApp" runat="server" OnCheckedChanged="chkIsApp_CheckedChanged"
                                    AutoPostBack="True" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100px; padding-left: 20px; vertical-align: top;" colspan="2">
                                Valid For :
                            </td>
                            <td align="left" style="padding-left: 5px;">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel3" >
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtExInDays" MaxLength="200" Width="200px" runat="server" Enabled="false"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100px; padding-left: 20px; vertical-align: top;" colspan="2">
                                Is Internal :
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="chkEIsInternal" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100px; padding-left: 20px; vertical-align: top;" colspan="2">
                                Is Active :
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="chkActive" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" style="font-size: 11px; text-align: center;">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidateVettingType();" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        </div>
    </div>
</form>
</body>
</html>
