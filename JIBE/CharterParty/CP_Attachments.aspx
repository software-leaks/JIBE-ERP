<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="true" CodeFile="CP_Attachments.aspx.cs" Inherits="CP_Attachments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
   
   <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
      <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
   <script>
       function fn_OnClose() {
           $('[id$=btnLoadFiles]').trigger('click');
       }

       function showDivAddFolder() {
           showModal('dvAddFolder', true, dvAddFolder_onClose);
       }
       function dvAddFolder_onClose() {
         
       }
       function closeDivAddDocType() {

           hideModal('dvAddFolder');
       }
       function previewDocument(docPath) {
           document.getElementById("ifrmDocPreview").src = docPath;
        
       }

   </script>
 
</head>


<body>
<form runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%-- <div class="page-title">
     Attached Documents
    </div>--%>
 


 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
     <div id="dvAddFolder" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
        border-style: solid; border-width: 1px; width: 450px; position: absolute; left: 40%;
        top: 15%; z-index: 1; color: black" class="ui-widget-content" title="Add Attachment">    
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="border-style: solid; border-color: Silver; border-width: 1px; width: 100%;">
                  <tr>
                  <td><asp:Label ID="lblPrimary" runat="server" Text="FileType : ">
                  </asp:Label>
                 
                </td>
                <td>  <asp:DropDownList ID="ddlDepartment" runat="server" Width="150px" >
                      
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="rfvDept" runat="server"  InitialValue="0"
                                            ErrorMessage="*" ControlToValidate="ddlDepartment" ValidationGroup="vgSubmit"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                  </tr>
                  <tr>
                  <td><asp:Label ID="lblTitle" runat="server" Text="Title : ">
                  </asp:Label></td>
                  <td>
                      <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                  <td><asp:Label ID="lblContent" runat="server" Text="Content : ">
                  </asp:Label></td>
                     <td>
                      <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                  </tr>
                  <tr>
                  <td colspan="2">
                   <tlk4:AjaxFileUpload ID="AjaxFileUpload2" runat="server" Padding-Bottom="2" Padding-Left="2" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber"  MaximumNumberOfFiles="1"  />
                  </td>
                  </tr>
                  <tr>
                   <td colspan="2" align="center" ><asp:Button ID="btnSave" runat="server" BackColor="#3498DB" Height="22px" 
                                                                     Text="Save"  ValidationGroup="vgSubmit"
                    Visible="true" Width="106px" onclick="btnSave_Click" /></td>
                    <td>
                      <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="vgSubmit" />
                    </td>
                  </tr>
                 
                    </table>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="AjaxFileUpload2" EventName="UploadComplete" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
      
    <div id="page-content"  style="border: 1px solid gray; z-index: -2; overflow: auto; width: 100%; height: 100%;">
        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
            <ContentTemplate>
                <div class="error-message">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
                <div style="margin-left: auto; margin-right: auto; text-align: center;">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                 <tr>
                                   
                                    <td style="width: 10%" align="left">
                                        
                                            
                                  <%--    <asp:ImageButton ID="btnNew" runat="server" Text="Create New Folder" 
                                            ToolTip="Create New Folder" OnClientClick="showDivAddFolder();" 
                                            ImageUrl="~/Images/Add-icon.png"/>--%>
                                            <asp:ImageButton runat="server" Text="Add Attachment" ID="btnAdd" 
                                            onclick="btnAdd_Click" ImageUrl="~/Images/addicon2.png" Height="24px" ToolTip="Add Attachment" />
                                      &nbsp;
                                  
                                    </td>
                                    
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 30%; text-align: left">
                                <asp:GridView ID="gvAttachment" runat="server" AutoGenerateColumns="False"  
                                AllowPaging="true" PageSize="5" DataKeyNames="Folder_Name"
                                                      EmptyDataText="NO RECORDS FOUND" GridLines="Both" 
                                                      PagerStyle-Mode="NextPrevAndNumeric" Width="100%" 
                                                      onrowdatabound="gvAttachment_RowDataBound" onpageindexchanging="gvAttachment_PageIndexChanging" 
                               >
                                                      <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                      <RowStyle CssClass="RowStyle-css" />
                                                      <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                                      <SelectedRowStyle BackColor="#FFFFCC" />
                                                      <EmptyDataRowStyle Font-Bold="true" Font-Size="12px" ForeColor="Red" 
                                                          HorizontalAlign="Center" />
                                                      <%--  <RowStyle Wrap="False" />--%>
                                                      <Columns>
                                                       <asp:TemplateField HeaderText="File Attachments" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                              <table>
                                                              <tr>
                                                               <td>   <asp:Label ID="LabelId1" runat="server" Text='<%#Eval("File_id")%>' 
                                                                      Visible="false"> </asp:Label></td>
                                                            </tr>
                                                            <tr><td>
                                                                  <asp:Label ID="LabelId2" runat="server" Text='<%#Eval("File_Name")%>' 
                                                                      > </asp:Label>
                                                          
                                                          </td></tr>  <tr><td>
                                                                  <asp:Label ID="lblName2" runat="server" Text='<%#Eval("Info_Content")%>'> </asp:Label>
                                                           
                                                          </td></tr>  <tr><td>
                                                                  <asp:Label ID="lblCreation" runat="server" Text='<%#Eval("Created_Date")%>'> </asp:Label>
                                                             
                                                          </td></tr>  <tr><td>
                                                               <asp:ImageButton ID="imgAttachment" runat="server"  style="width: 20px; height: 20px" CommandArgument='<%#Eval("[File_Path]")%>' OnCommand="ImgAttachment_Click" 
                                                                 ForeColor="Black" ToolTip="Attachment" ImageUrl="~/Images/Attachment.png"></asp:ImageButton>
                                                              
                                                          </td></tr>  <tr><td>
                                                               <asp:Label ID="imgAtffftachment" runat="server" Visible="false" Text='<%#Eval("[File_Path]")%>' 
                                                                 ForeColor="Black"  ></asp:Label>
                                                               
                                                          </td></tr>  <tr><td>
                                                               <asp:ImageButton ID="imgEdit" runat="server"  style="width: 20px; height: 20px" CommandArgument='<%#Eval("Info_Title") + "," +Eval("Info_Content")+"," +Eval("Info_ID")+","+Eval("Folder_Name")%>' OnCommand="ImgEdit_Click" />
                                                          
                                                          </td></tr>  <tr><td>
                                                               <asp:ImageButton ID="imgDelete" Visible="false"  runat="server"  style="width: 20px; height: 20px" CommandArgument='<%#Eval("Info_Title") + "," +Eval("Info_Content")+"," +Eval("Info_ID")+","+Eval("Folder_Name")%>' OnCommand="ImgEdit_Click" 
                                                                 ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/Delete.png"></asp:ImageButton>
                                                                  </td></tr>
                                                                 </table>
                                                              </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="5%" HorizontalAlign="Left"  />
                                                        </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="File_id_sequence" Visible="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                            
                                                               <HeaderStyle HorizontalAlign="Center" />
                                                              <ItemStyle Width="5%" />
                                                          </asp:TemplateField>
                                                      </Columns>
                                                  </asp:GridView>
                     
                                                  </td>
                            <td width="70%" valign="top">  <div>
          

             <iframe id="ifrmDocPreview"  marginheight="0px" runat="server"
                                                    src="../Images/previewAttach.png" 
                                                    style="vertical-align: middle; height: 500px; text-align: center;" width="100%"></iframe>
                                                     
    </div></td>
                        </tr>
                    </table>
                    <asp:Button ID="btnExit" Width="100px" Text="Exit" Visible="false" runat="server" OnClientClick="refreshAndClose();" />
                </div>

     
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </ContentTemplate>
            </asp:UpdatePanel>
    </form>
</body>
</html>



