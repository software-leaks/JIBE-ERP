<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ODM_Detail.aspx.cs" Inherits="ODM_ODM_Detail" %>

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
           text-align: Right;width: 40%;background-color:WindowFrame;
        }
    </style>
   
  
</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;">
     
    <script language="javascript" type="text/javascript">

        function DocOpen(filename) {
             window.open('..'+ filename);
        }
       
    </script>

    <form id="form1" runat="server" >
    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;height:100% "  >
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
        <div id="dialog" style="display: none"></div>
           <div style="border: 1px solid #cccccc" class="page-title">
                Detail Message
            </div>
            <table width="100%">
                        <tr>
                   <td class="style1" colspan="2" align="center">
                     <asp:Label ID="lblCreatedBy"  runat="server" ></asp:Label> &nbsp;
  
                    <asp:Label ID="lblSentOn" runat ="server"></asp:Label>
                      </td>
            
                </tr>
                 <tr>

                    <td class="style1">
                      From :
                    </td>
                    <td align="left" style="width: 60%">
                    <asp:Label ID="lblDep" runat="server" ></asp:Label>
                    </td>
                  
                  </tr>
                 <tr style="height:60px">

                    <td  class="style1">
                       Vessel(To) :
                    </td>
                    <td align="left" style="width: 60%">
                    <asp:TextBox ID="lblVesselName" width="98%" height="100%" TextMode="MultiLine" Enabled="false" runat="server"></asp:TextBox>
                       </td>
                                     
                  </tr>
                <tr>
                    <td  class="style1">
                        Subject :
                    </td>

                    <td align="left" style="width: 60%">
                         <asp:TextBox ID="lblSubject" TextMode="MultiLine" Enabled="false" width="98%" height="100%" runat="server"></asp:TextBox>
                    </td>
 
                </tr>
                <tr style="height:100px">
                    <td  class="style1">
                        Message :
                    </td>
                    <td align="left" style="width: 60%;height:auto" >
                        <asp:TextBox ID="lblDescription" TextMode="MultiLine" width="98%" height="100%" Enabled="false" runat="server"></asp:TextBox>
                         </td>
                </tr>
                <tr>
                    <td align="right" style="width: 40%">
                    </td>
                    <td align="right">
                    </td>
                </tr>
                <tr>
                <td colspan="2">
                
                        <div>
                            <asp:GridView ID="gvAttachment" runat="server" AutoGenerateColumns="False"
                                GridLines="Both"
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
    </div>
        </center>

    </form>
</body>
</html>
