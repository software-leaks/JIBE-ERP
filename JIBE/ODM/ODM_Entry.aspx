<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="true" CodeFile="ODM_Entry.aspx.cs" Inherits="ODM_ODM_Entry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
   
  
    <style type="text/css">
        .style1
        {
            width: 1%;
        }
    </style>
   
  
</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;">
     
    <script language="javascript" type="text/javascript">

        function refreshAndClose() {
            window.parent.ReloadParent_ByButtonID();
            window.close();
        }

        function DocOpen(filename) {
             window.open('..'+ filename);
        }
        function LimtCharacters(CharLength)
         {
             var txtMsg = document.getElementById("txtDescription"); 
             chars = txtMsg.value.length;
            document.getElementById("lblmsg").innerHTML = "4000 Characters allowed per message. " + (CharLength - chars).toString() + " characters left.";
        }

        function validation() 
        {

            if (document.getElementById("ddlDepartment").value == "0")
             {
                alert("Please select a Department.");
                return false;
            }



            if (document.getElementById("lblVesselName") == null || document.getElementById("lblVesselName").value == "") {
                if (document.getElementById("chkVesselAll").checked == false) {
                    checked = false;
                    var count = 0
                    var c = document.getElementsByTagName('input');
                    for (var i = 1; i < c.length; i++) {
                        if (c[i].type == 'checkbox') {
                            if (c[i].checked) {
                                count = count + 1;
                            }

                        }
                    }

                    if (count == 0) {
                        alert("Please check atleast one vessel");
                        return false
                    }
                }
            }

            if (document.getElementById("txtSubject").value == "") {
                alert("Please Enter Subject Name.");
                document.getElementById("txtSubject").focus();
                return false;

            }
   
            if (document.getElementById("txtDescription").value == "") {
                alert("Please Enter Notification Description.");
                document.getElementById("txtDescription").focus();
                return false;
            }


            return true;

        }

        function validateddlVessel() {
            if (document.getElementById("ddlVessel").value == "0") {
                alert("Please select a Vessel to add.");
                return false;
            }
        }
    </script>
   <script type="text/javascript">
       function ShowPopup(message) {
           $(function () {
               $("#dialog").html(message);
               $("#dialog").dialog({
                   title: "jQuery Dialog Popup",
                   buttons: {
                       Close: function () {
                           $(this).dialog('close');
                       }
                   },
                   modal: true
               });
           });
       };
</script>
    <form id="form1" runat="server" style="background-color:Orange">
    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB; "  >
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
        <div id="dialog" style="display: none"></div>
           <div style="border: 1px solid #cccccc" class="page-title">
                Add New Message
            </div>
            <table border="0" cellpadding="2" cellspacing="2" width="800px">

            <tr>
                   <td style="text-align: Right;" colspan="4">
                     <asp:Label ID="lblCreatedBy" Visible="false" runat="server" ></asp:Label> &nbsp;
  
                    <asp:Label ID="lblUpdatedBy" Visible="false" runat ="server"></asp:Label>
                      </td>
            
                </tr>
                 <tr>

                    <td style="text-align: Right;">
                      From :
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <biv>
                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="200px" CssClass="txtInput">
                        </asp:DropDownList>
        
                        </biv>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                  
                    </td>
                  
                  </tr>
                 <tr>

                    <td style="text-align: Right;">
                      Select Vessel(To) :
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left" colspan="2">
                          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <biv>
                        <asp:DropDownList ID="ddlVessel" runat="server" Width="200px"  CssClass="txtInput">
                        </asp:DropDownList>

                        <asp:Button ID="btnVesselAdd" runat="server" Text="Add Vessel" OnClientClick="return validateddlVessel();"
                                        onclick="btnVesselAdd_Click" />
                          <asp:CheckBox ID="chkVesselAll" runat="server" Autopostback="true" Text="Select All" Checked="false" oncheckedchanged="chkVesselAll_CheckedChanged" />
                        <br />
                                 <asp:Label ID = "lblVesselName" visible runat="server"></asp:Label>            
                                <div id="dvVesselList" style="float: left; text-align: left; width: 500px; height: 60px; overflow-x: hidden;
                                    border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                    background-color: #ffffff;" runat="server">
                                    <asp:CheckBoxList ID="chkVessel" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                        runat="server">
                                    </asp:CheckBoxList>
                                </div>
                                                   
                        </biv>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td></td>
                  
                  </tr>
                <tr>
                    <td align="right" style="width: 39%">
                        Subject &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtSubject" runat="server" Width="500px" MaxLength="250" 
                            CssClass="txtInput" >
                        </asp:TextBox>
                    </td>
 
                </tr>
                <tr>
                    <td align="right" style="width: 39%">
                        Message Text &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left" style="width: 50%" colspan="2">
                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" Width="500px" Height= "100px"  MaxLength="4000"  runat="server"></asp:TextBox>
                         </td>
                </tr>

                  <tr>

                    <td colspan="4" align ="center"> <asp:Label ID = "lblmsg"  runat="server"></asp:Label></td>
                  
                  </tr>

                <tr>
                    <td align="right" style="width: 39%">
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                    
                    </td>
                    <td align="left">
                            <asp:HiddenField ID="hdODMID" runat="server" />
                    </td>
                    <td align="right">
                    </td>
                </tr>
                <tr>
               
                    <td colspan="4" align="right" style="color: #FF0000; font-size: small;">
                        * Mandatory fields &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                <td colspan="4" align="center" style="color: #FF0000; font-size: small;">
                        <asp:Label ID="lbl1" runat="server" Text=""></asp:Label>
                    </td>
                   
                </tr>

                <tr>
                    <td align="center" colspan="2">
                      
                    </td>
                    <td align="center">
                      <asp:Button ID="btnsave" runat="server" OnClientClick="return validation();"
                            Text="Save" OnClick="btnsave_Click" />&nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="false" 
                            onclick="btnDelete_Click"/>
                    </td>
                    <td  align="center" ></td>
                </tr>
                <tr>
                <td colspan="4" align="center">
                <asp:Label ID ="lblNotificatin" runat="server" Font-Italic="true" Text="Save first to add attachment. " ForeColor="Red"></asp:Label>
                 <asp:Label ID ="lblFileSize" runat="server" Font-Italic="true" Text="File size should not excceed 1 MB." ForeColor="Red"></asp:Label>
                 
                </td>
                </tr>
                <tr>
                <td align = "right">
                <asp:Label ID="lblUpload" runat="server" Visible="false" Text ="Upload File :"></asp:Label>
                </td>
                <td colspan="3" align="left">
                        <asp:FileUpload ID="File_Upload" runat="server" Enabled="false" Visible= "false" Style="width: 50%;
                            height: 18px; background-color: #F2F2F2; border: 1px solid #cccccc; font-size: 12px;
                            cursor: pointer" />
                        &nbsp;<asp:Button ID="btnUpload" runat="server" Height="20px" OnClick="Upload1_Click"
                            Style="height: 18px; border: 1px solid #cccccc; font-size: 12px; cursor: pointer"
                            Text="Add Attachment" Width="100px" Visible="false" />
                 </td>
                </tr>
                <tr>
                <td></td>
                <td colspan="3">
                
                        <div>
                            <asp:GridView ID="gvAttachment" runat="server" AutoGenerateColumns="False"
                                GridLines="Both" OnRowCommand="gvAttachment_RowCommand" OnRowDataBound="gvAttachment_RowDataBound"
                                Width="100%">
                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle Font-Bold="false" Font-Size="12px" ForeColor="Red" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Attachments">
                                        <HeaderTemplate>
                                            Attached File(s)
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblfileName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ATTACHMENT_NAME") %>'
                                                Visible="true"></asp:Label>
                                            <asp:Label ID="Label1" runat="server" Text=" has been uploaded." Visible="true"></asp:Label>
                                            <asp:Label ID="lblfilePath" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ATTACHMENT_PATH") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblFileId" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="left" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText ="Size(KB)" DataField="Sent_Size" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <img style="border: 0; width: 14px; height: 14px" alt="Open in new window" onclick="DocOpen('<%# Eval("ATTACHMENT_PATH")%>')"
                                                src="../Images/Download-icon.png" title="Click to View file" />
                                            <asp:ImageButton ID="ImgAttDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") + "," + DataBinder.Eval(Container,"DataItem.ATTACHMENT_PATH")  %>'
                                                ForeColor="Black" Height="14px" ImageUrl="~/Images/Delete.png" OnClientClick="return confirm('Are you want delete ? ');" OnCommand="ImgAttDelete_Click" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Center" Width="50px"
                                            Wrap="False" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
               
                </td>
                </tr>
                
            </table>
                    <div style="background-color: #d8d8d8; text-align: center">
        </div>
        <div>
            <asp:Label ID="lblMessage" Style="color: #FF0000;" runat="server"></asp:Label>

             

        </div>
        <div>
           <asp:Button ID="btnExit" Width="100px" Text="Exit" runat="server" OnClientClick="refreshAndClose();" />

    </div>
        </center>

    </form>
</body>
</html>
