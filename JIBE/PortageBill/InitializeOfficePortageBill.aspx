<%@ Page Title="Initialize Office PortageBill" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InitializeOfficePortageBill.aspx.cs" Inherits="PortageBill_InitializeOfficePortageBill" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
   <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
<style type="text/css">
        .HeaderStyle-css th
        {
            border: 1px solid #959EAF;
        }
    </style>

     <script type="text/javascript" language="javascript">
         function ValidateOnSave() {
          
             if ($("#ddlVessel").val() == '0') {
                  alert('Please Select Vessel.');
                  $("#ddlVessel").focus();
                  return false;
              }
                          
             if ($("#txtInitialDate").val().trim() == '') {
                 alert('Please Enter Portage Bill Date<%=TodayDateFormat %>.');
                 $("#txtInitialDate").focus();
                 return false;
             }
         }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="page-title">
        Initialize Office Portage Bill
     </div>
        <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
                <ContentTemplate>
   <div>    
        <br />
        <table align="center">
         <tr>
            <td align="center"><asp:Label ID="lblMessage" Style="color: #FF0000;" runat="server"></asp:Label></td>
            </tr>
        </table>
    
    <table>
            <tr>
                <td align="right" style="width: 536px">
                    Vessel
                </td>
                 <td style="color:Red;">*</td>
                <td>
                    <asp:DropDownList ID="ddlVessel" runat="server" Width="156px" ClientIDMode="Static">
                    </asp:DropDownList>                     
                </td>
                </tr>
                <tr>
                <td align="right" style="width: 536px">
                    Portage Bill Date
                </td>
                <td style="color:Red;">*</td>
                <td>                     
                                        <asp:TextBox ID="txtInitialDate" CssClass="textbox-css" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtenderDt" runat="server" TargetControlID="txtInitialDate"
                                            Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorDt" ValidationGroup="aa" runat="server"
                                            ControlToValidate="txtInitialDate" ErrorMessage="Please Enter Initial Date !!"></asp:RequiredFieldValidator>--%>
                </td>
                </tr>
 </table>
                <div style="text-align: center">
                <asp:Button ID="btnSave" runat="server" Text="Save" 
                                            onclick="btnSave_Click" OnClientClick="javascript:return ValidateOnSave();"/>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                            onclick="btnCancel_Click" />
                </div>              
       
               
        </div>
        </ContentTemplate></asp:UpdatePanel>
</asp:Content>

