<%@ Page  Language="C#" AutoEventWireup="true" CodeFile="Vetting_Attachments.aspx.cs"
    Inherits="Technical_Vetting_Vetting_Attachments" EnableEventValidation="false"   %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
   <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>




    <script type="text/javascript">
      
        
        function JSDropdownValidate() {
            if (document.getElementById('<%=DDLAttachment.ClientID%>').selectedIndex == 0) {
                alert("Please select Attachment Type");
                return false;
            }
            if (document.getElementById($('[id$=FileUpload1]').attr('id')).value == "") {
                    alert("Please select file to upload.");
                    return false;
                }

           
            return true;
        }
     
        function ConfirmMsg() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(" The file type you are trying to upload already contains a file. Would you like to replace the file?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <style type="text/css">
        body
        {
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
    <asp:UpdatePanel ID="updGridAttach" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <div style="padding-top: 2px; padding-bottom: 2px; height: 200px; overflow-y: scroll; ">
                <table cellpadding="2" cellspacing="0" style="border: 1px solid #cccccc; margin-top: 2px;
                    vertical-align: top; width: 520px;  margin-left:4px;">
                    <tr style="background-color: #aabbdd;">
                    <td>
                        <asp:Label ID="lblAttachments" runat="server" Text="Attachments:" Font-Bold="true"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; vertical-align: top;">
                            <asp:GridView ID="gvAttachment" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" EmptyDataText="No attachment found."
                                CellPadding="2" Width="520px" GridLines="None" CssClass="GridView-css" Style="padding-left: 5px; "> 
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="30px"  />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle  Width="180px"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttTypeName" runat="server" Text='<%# Eval("Attachmt_Type_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Attachment" HeaderStyle-HorizontalAlign="Left">
                                     <HeaderStyle  Width="240px"/>
                                        <ItemTemplate>
                                            
                                              <a href='<%#"../../Uploads/Vetting/VetAtt/"+System.IO.Path.GetFileName(Convert.ToString(Eval("Attachement_Path")))%>'
                                                target="_blank">
                                                <asp:Label ID="lblAttName" runat="server" Text='<%# Eval("Attachment_Name") %>'></asp:Label></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                            OnClientClick="return confirm('Are you sure you wish to continue?')"
                                                            CommandArgument='<%#Eval("[Vetting_Attachmt_ID]")%>' ForeColor="Black" ToolTip="Delete" 
                                                            ImageUrl="~/Images/delete.png" Height="16px" ></asp:ImageButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center"  CssClass="PMSGridItemStyle-css" >
                                            </ItemStyle>
                                        </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="updDDLAttachment" runat="server" >
        <ContentTemplate>
            <table width="530px" cellpadding="2" cellspacing="2" style="margin-top: 8px; margin-left:4px;
                    vertical-align: top;">
                <tr style="background-color: #aabbdd;">
                    <td align="left" colspan="1">
                        <asp:Label ID="lblUpload" runat="server" Font-Bold="true" Text="Upload New File :"> </asp:Label>
                    </td>
                   
                </tr>
                <tr>
                    <td  align="left" colspan="1">
                     <asp:Label ID="lblMandatry" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        <asp:Label ID="lblAttachment" runat="server" Text="Attachment Type:" Font-Bold="true"></asp:Label>
                    
                        <asp:DropDownList ID="DDLAttachment" runat="server" Font-Size="11px" 
                            OnSelectedIndexChanged="DDLAttachment_SelectedIndexChanged"  AutoPostBack="true" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                <td align="left" >
                    <asp:FileUpload ID="FileUpload1" runat="server" Padding-Bottom="2"  Padding-Right="1" Width="457px" Height="23px" Padding-Top="2"/>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" 
                        onclick="btnUpload_Click" OnClientClick="return JSDropdownValidate();"/>
                
             
                </td>
                </tr>
                <tr>
                <td align="center">
                <asp:Button ID="btnClose" Text="Close" runat="server" onclick="btnClose_Click"  />
              
                </td>                  
                </tr>
            </table>
     </ContentTemplate>
      <Triggers>
<asp:PostBackTrigger ControlID="btnUpload" />

</Triggers>
</asp:UpdatePanel> 
    
    </form>
</body>
</html>
