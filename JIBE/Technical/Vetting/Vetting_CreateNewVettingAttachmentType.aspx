<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vetting_CreateNewVettingAttachmentType.aspx.cs" Inherits="Technical_Vetting_Vetting_CreateNewVettingAttachmentType" %>

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

    <style type="text/css">
       
       body {  
   
        font-family: Tahoma;
        font-size: 12px;
        margin: 0;
        padding: 0;
       }
        .gridmain-css tr:hover
        {
            background-color: #feecec;
        }
      .gridmain-css tr
        {
            height: 25px;
        }
    </style>
     <script language="javascript" type="text/javascript">
         function showAddVettingAttchmentType() {
             $("#divVetAtt").prop('title', 'Add Attachment Type');
             document.getElementById($('[id$=hdnVetAttTypeID]').attr('id')).value = "";
             document.getElementById($('[id$=txtVettingAttTypeName]').attr('id')).value = "";
             showModal('divVetAtt');

             return true;
         }
        
               
         function onEditClick(VetAttTypeID, VetAttTypeName) {
             $("#divVetAtt").prop('title', 'Edit Attachment Type');
             showModal('divVetAtt');
             document.getElementById($('[id$=hdnVetAttTypeID]').attr('id')).value = VetAttTypeID;
             document.getElementById($('[id$=txtVettingAttTypeName]').attr('id')).value = VetAttTypeName;

             return true;
         }

         function ValidateVettingAttType() {
             var VettingAttTypeName = document.getElementById($('[id$=txtVettingAttTypeName]').attr('id')).value.trim();

             if (VettingAttTypeName == "") {
                 alert('Enter Vetting Attachment Type');
                 return false;
             }

         }
         function SetTitleonEdit() {
             $("#divVetAtt").prop('title', 'Edit Attachment Type');
         }

         function SetTitleonAdd() {
             $("#divVetAtt").prop('title', 'Add Attachment Type');
         }
     </script>
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
            <asp:Panel ID="pnlVetAttFilter" runat="server" DefaultButton="btnFilter">
                        <table cellpadding="2" cellspacing="4" style="float: left;" width="100%">
                            <tr>
                                    <td align="right" style="width: 10%">
                                        Attachment Type :&nbsp;
                                    </td>
                                    <td align="left" style="width: 40%">
                                        <asp:TextBox ID="txtVetAttfilter" runat="server" Width="100%" Height="18px"></asp:TextBox>
                                        
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtVetAttfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" Enabled="True" />
                                    </td>
                                    <td align="center" style="width: 1%">
                                        <asp:ImageButton ID="btnFilter" runat="server" ToolTip="Search" 
                                            ImageUrl="~/Images/SearchButton.png" onclick="btnFilter_Click" />
                                    </td>
                                    <td align="center" style="width: 1%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" ToolTip="Refresh" 
                                            ImageUrl="~/Images/Refresh-icon.png" onclick="btnRefresh_Click" />
                                    </td>
                                    <td align="center" style="width: 1%">
                                        <asp:ImageButton ImageUrl="~/Images/Add-icon.png" ID="ImgVetAttAdd" Height="22px"
                                            runat="server" Style="cursor: pointer;" ToolTip="Add New Attachment Type"   OnClientClick="return showAddVettingAttchmentType();"/>
                                    </td>
                                    <td style="width: 1%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel"
                                            ImageUrl="~/Images/Exptoexcel.png" onclick="ImgExpExcel_Click" />
                                    </td>
                                </tr>
                        </table>
                    </asp:Panel>
                    </div>
                        <div align="center" style="width: 950px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                          <asp:GridView ID="gvVettingAttachment" runat="server" EmptyDataText="NO RECORDS FOUND"
                                    CellPadding="0" ShowHeaderWhenEmpty="true" CellSpacing="2" Width="100%" AllowSorting="true"
                                    CssClass="gridmain-css" AutoGenerateColumns="false" OnSorting="gvVettingAttachment_Sorting"
                                    OnRowDataBound="gvVettingAttachment_RowDataBound">
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vetting Attachment">
                                        <HeaderStyle  Width="870px"/>
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lblReasonHeader" Style="margin-left: 2px; text-decoration: none;"
                                                runat="server" CommandName="Sort" CommandArgument="Vetting_Attachmt_Type_Name" ForeColor="Black">Attachment Type</asp:LinkButton>
                                            <img id="Vetting_Attachmt_Type_Name" runat="server" visible="false" />
                                        </HeaderTemplate>
                                         <HeaderStyle Wrap="true" HorizontalAlign="left"  />
                                            <ItemTemplate>
                                                <asp:Label ID="lblVetAttachment" runat="server" Text='<%#Eval("Vetting_Attachmt_Type_Name")%>'></asp:Label>
                                                <asp:Label ID="lblVetAttId" runat="server" Visible="false" Text='<%#Eval("Vetting_Attachmt_Type_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="870px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                           
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Action">
                                       <HeaderStyle  Width="80px"/>
                                            <ItemTemplate>
                                              <table cellpadding="1" cellspacing="0">
                                                <tr align="center">
                                                    <td style="border-color: transparent; width: 10px">
                                                            <asp:ImageButton ID="ImgEdit" CssClass="edit" ImageUrl="~/Images/Edit.gif" ForeColor="Black" Visible='<%# uaEditFlag %>'
                                                                ToolTip="Edit" runat="server" Height="16px"  Style="cursor: pointer;" />
                                                       </td>
                                                       <td style="border-color: transparent; width: 10px">
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"  Visible='<%# uaDeleteFlage %>'
                                                                OnClientClick="return confirm('Are you sure, you want to delete?')" CommandArgument='<%#Eval("[Vetting_Attachmt_Type_ID]")%>'
                                                                ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px" >
                                                            </asp:ImageButton>
                                                       </td>
                                                       <td style="border-color: transparent; width: 10px">
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Style="vertical-align: top; cursor: pointer;"  runat="server"
                                                                onclick='<%# "Get_Record_Information(&#39;VET_LIB_VettingAttachementType&#39;,&#39;Vetting_Attachmt_Type_ID="+Eval("Vetting_Attachmt_Type_ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                        </tr>
                                                        </table>
                                                    
                                            </ItemTemplate>
                                             <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                           
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
           <div id="divVetAtt" style="display: none; font-family: Tahoma; text-align: left;
            font-size: 12px; color: Black; width: 400px; height: 120px;">
                     <asp:UpdatePanel runat="server" ID="upVetAtt" UpdateMode="Conditional">
                <ContentTemplate>
                
                 <table cellpadding="2" cellspacing="2" style="padding-top: 20px;">
                        <tr>
                            <td align="left" style="width: 150px; padding-left: 7px; vertical-align: top;">
                               Attachment Type :
                            </td>
                            <td style="color: #FF0000;" align="left">
                                *
                            </td>
                            <td align="left" style="padding-left: 5px;">
                                <asp:TextBox ID="txtVettingAttTypeName" MaxLength="200" Width="200px" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hdnVetAttTypeID" runat="server" />
                            </td>
                            </tr>
                             <tr>
                            <td colspan="3" align="center" style="font-size: 11px; text-align: center; padding-top: 20px;">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidateVettingAttType();" />
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

