<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Log_Admin.aspx.cs" Inherits="PO_LOG_PO_Log_Admin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
            width: 25%;
            height: 24px;
        }
        .style2
        {
            width: 1%;
            height: 24px;
        }
        .style3
        {
            height: 24px;
        }
    </style>
    <script>
        function refreshAndClose() {
//            window.parent.ReloadParent_ByButtonID();
            //            window.close();
            window.parent.location.reload(true);
            window.close();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <center>
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-title" class="page-title">
                Admin
            </div>
             <asp:UpdatePanel ID="UpdatePanelRemarks" runat="server">
                                        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="2">
               
                <tr>
                    <td align="right" class="style1">
                        Vessel :
                    </td>
                    <td style="color: #FF0000; " align="right" class="style2">
                        
                    </td>
                    <td align="left" class="style3">
                         <asp:DropDownList ID="ddlVessel" runat="server" CssClass="txtInput" AutoPostBack="true"
                                                Width="200px" 
                             onselectedindexchanged="ddlVessel_SelectedIndexChanged">
                                            </asp:DropDownList>
                                           
                    </td>
                    <td align="right">
                       Owner :
                    </td>
                
                    <td align="left" class="style3">
                         <asp:DropDownList ID="ddlOwnerCode" runat="server" CssClass="txtInput" 
                                                 Width="200px"  >
                                            </asp:DropDownList>
                                         
                    </td>
                </tr>
                 <tr>
                    <td align="right" style="width: 25%">
                        PO Type :
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        
                    </td>
                    <td align="left">
                         <asp:DropDownList ID="ddlPOType" runat="server" CssClass="txtInput" AutoPostBack="true"
                                                Width="200px" 
                             onselectedindexchanged="ddlPOType_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                           
                    </td>
                    <td align="right" style="width: 10%">
                       Currency :
                    </td>
                   
                    <td align="left">
                         <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="txtInput" 
                                                 Width="200px" >
                                            </asp:DropDownList>
                                           
                    </td>
                </tr>
                 <tr>
                    <td align="right" style="width: 25%">
                        PO Issue By :
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        
                    </td>
                    <td align="left">
                         <asp:DropDownList ID="ddlIssueBy" runat="server" CssClass="txtInput" AutoPostBack="true"
                                                Width="200px"  onselectedindexchanged="ddlVessel_SelectedIndexChanged">
                                            </asp:DropDownList>
                                           
                    </td>
                    <td align="right" style="width: 25%">
                       Supplier :
                    </td>
                
                    <td align="left">
                         <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="txtInput" 
                                                 Width="200px"  >
                                            </asp:DropDownList>
                                          
                    </td>
                </tr>
                 <tr>
                    <td align="right" style="width: 25%">
                        Account Type :
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        
                    </td>
                    <td align="left">
                         <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="txtInput" AutoPostBack="true"
                                                 Width="200px"  onselectedindexchanged="ddlVessel_SelectedIndexChanged">
                                            </asp:DropDownList>
                                           
                    </td>
                    <td align="right" style="width: 25%">
                       Account Classification :
                    </td>
                   
                    <td align="left">
                         <asp:DropDownList ID="ddlAccClassifictaion" runat="server" CssClass="txtInput" 
                                                 Width="200px"  >
                                            </asp:DropDownList>
                                           
                    </td>
                </tr>
                 <tr>
                    <td align="right" style="width: 25%">
                        Charter Party :
                    </td>
                    <td >
                        
                    </td>
                    <td align="left">
                         <asp:DropDownList ID="ddlCharterParty" runat="server" CssClass="txtInput" AutoPostBack="true"
                                                 Width="200px"  >
                                            </asp:DropDownList>
                                            
                    </td>
                    <td align="right" style="width: 25%">
                       Terms :
                    </td>
                    
                    <td align="left">
                         <asp:DropDownList ID="ddlTerms" runat="server" CssClass="txtInput" 
                                                 Width="200px"  >
                                            </asp:DropDownList>
                                        
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 25%">
                       Payment Priority :
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="rdbPayment" runat="server" 
                            RepeatDirection="Horizontal" >
                            <asp:ListItem Value="Normal" >Normal</asp:ListItem>
                            <asp:ListItem Value="Immediate" >Immediate</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="right" style="width: 25%">
                       Hide PO :
                    </td>
                    <td style="color: #FF0000; width: 1%" align="left">
                        <asp:CheckBox ID="chkHide" runat="server" Text="Hide PO from Supplier Online Invoice Page" />
                    </td>
                   
                </tr>
                <tr>
                    <td align="right" style="width: 25%">
                      Po Auto Close Date :
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        
                    </td>
                    <td align="left">
                      <asp:TextBox ID="txtCloseDate"  runat="server" Width="100px" BackColor="#FFFFCC"></asp:TextBox>
                                     <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCloseDate"
                                        Format="dd/MM/yyyy">  </ajaxToolkit:CalendarExtender>
                         <asp:Button ID="btnUnClose"  runat="server" Width="100px" ValidationGroup="vgSubmit" 
                                    Text="Unclose PO" onclick="btnUnClose_Click"   />
                                      
                    </td>
                    <td align="right" style="width: 25%">
                        
                                  
                    </td>
                    <td style="color: #FF0000; width: 1%" align="left">
                         <asp:Button ID="btnClose"  runat="server" Width="150px" ValidationGroup="vgSubmit" 
                                    Text="Close PO Immediately" onclick="btnClose_Click"  />
                                        
                    </td>
                  
                </tr>
                 <tr>
                    <td align="right" style="width: 25%">
                      Remarks :
                    </td>
                    <td style="color: #FF0000; width: 1%" align="right">
                        
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="50px" Width="400px" ></asp:TextBox>
                    </td>
                   
                </tr>
                
                 <tr>
                    <td  style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                         
                    </td>
                     <td colspan="4" 
                         style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                         <asp:Button ID="btnUpdate"  runat="server" Width="100px" 
                                    Text="Update" onclick="btnUpdate_Click"   />
                                    <asp:Button ID="btnAdminDelete"  runat="server" Width="100px" OnClientClick="javascript:return confirm('Confirm Delete PO from DataBase?')" 
                                    Text="Admin Delete" onclick="btnAdminDelete_Click"   />
                                    <asp:Button ID="btnExit"  runat="server" Width="100px" 
                                    Text="Exit" OnClientClick="refreshAndClose();"   />
                                   
                    </td>
                    <td>
                    
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center" style="color: #FF0000; font-size: small;">
                       <asp:Button ID="btnCalculate"  runat="server" Width="200px"
                                    Text="Calculate Outstanding" onclick="btnCalculate_Click"  />
                    </td>
                </tr>
            </table>
       
        <div style="display:none;" >
         <asp:TextBox ID="txtSupplyID" runat="server"  Width="1px"></asp:TextBox>
          <asp:TextBox ID="txtPOCode" runat="server"  Width="1px"></asp:TextBox>
        </div>
        </ContentTemplate></asp:UpdatePanel>
         </div>
    </center>
  
    </form>
</body>
</html>

