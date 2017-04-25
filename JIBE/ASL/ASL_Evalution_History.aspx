<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_Evalution_History.aspx.cs"
    Inherits="ASL_ASL_Evalution_History" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <script language="javascript" type="text/javascript">
        function OpenScreen2(ID, Eval_ID) {

            var Supp_ID = document.getElementById('txtSupplier_Code').value;
            var url = 'ASL_Supplier_Remarks.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_Evalution', 'Supplier Remarks', url, 'popup', 700, 1080, null, null, false, false, true, null);
        }
        function OpenScreen12(ID, Eval_ID) {

            var Supp_ID = document.getElementById('txtSupplier_Code').value;
            var url = 'ASL_Supplier_Statistics.aspx?Supp_ID=' + Supp_ID + '&ID=' + Eval_ID;
            OpenPopupWindowBtnID('ASL_Supplier_Statistics', 'Supplier Statistics', url, 'popup', 700, 1080, null, null, false, false, true, null);
        }
    </script>
     <script type="text/javascript">
         /*The Following Code added To adjust height of the popup when open after entering search criteria.*/
         $(document).ready(function () {
             window.parent.$("#ASL_Evalution").css("height", (parseInt($("#pnlEvaluation").height()) + 50) + "px");
             window.parent.$(".xfCon").css("height", (parseInt($("#pnlEvaluation").height()) + 50) + "px").css("top", "50px");
         });
    </script>
</head>
<body style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 99%;">
<table  style="border: 0px solid #cccccc; font-family: Tahoma; overflow: Auto; font-size: 12px; width: 100%;">
                                 <tr><td>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptM" runat="server">
    </asp:ScriptManager>
    <center>
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlEvaluation" runat="server" Visible="true">
            <div id="Div1" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
                color: Black; height: 100%;">
                <div id="Div2" class="page-title">
                    Supplier Evaluation Form
                </div>
                <div style="display:none">
                <table width="100%" cellpadding="2" cellspacing="0">
                    <tr>
                        <td style="width: 50%;" align="left" valign="top">
                            <table width="100%">
                                <tr>
                                    <td align="right" style="width: 20%;">
                                        Supplier Code :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSupplierCode" Enabled="false" ReadOnly="true" runat="server"
                                            MaxLength="2000" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 20%;">
                                        Supplier Type :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtType" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                            Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 20%;">
                                        Company Name :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCompanyName" Enabled="false" ReadOnly="true" runat="server" MaxLength="2000"
                                            Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" style="width: 20%;">
                                        Address :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAddress" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                            TextMode="MultiLine" Width="400px" Height="50px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="top" style="width: 20%">
                                        Email :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtEmail" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                            Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 20%;">
                                        Contact Name :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPICName" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                            Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 20%">
                                        Contact Phone :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPICPhone" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                            Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%;" align="left" valign="top">
                            <table width="100%">
                                <tr>
                                    <td style="width: 20%" align="right">
                                        Status :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtStatus" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                            Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%" align="right">
                                        Counterparty Type :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSubType" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                            Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%" align="right">
                                        Incorporation :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtIncorporation" Enabled="false" ReadOnly="true" runat="server"
                                            MaxLength="2000" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%" align="right" valign="top">
                                        Phone :
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtPhone" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                            Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 20%">
                                        Fax :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtFax" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                            Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%" align="right">
                                        Contact Email :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPICEmail" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                            Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 20%;">
                                        Registered Data :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtRegistedData" ReadOnly="true" Enabled="false" runat="server"
                                            MaxLength="2000" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="height: 10px;">
                                        <asp:Button ID="btnRegisteredData" Visible="false" runat="server" Text="Registered Data"
                                            OnClick="btnRegisteredData_Click" />&nbsp;
                                        <asp:Button ID="btnRemarks" runat="server" Text="Supplier Remarks" OnClientClick='OpenScreen2(null,null);return false;' />&nbsp;
                                        <asp:Button ID="btnStatistics" runat="server" Text="Statistics" OnClientClick='OpenScreen12(null,null);return false;' />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%; height: 10px;">
                        </td>
                        <td style="color: #FF0000; width: 1%" align="left">
                        </td>
                        <td align="right" colspan="2">
                        </td>
                    </tr>
                </table>
                </div>
            </div>
            <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
                color: Black; height: 100%;">
                <asp:UpdatePanel ID="panel1" runat="server">
                    <contenttemplate>
                  <table width="100%" cellpadding="2" cellspacing="0">
                          <tr>
                          <td valign="top" align="left" style="width:50%;" >
                          <table width="100%">
                          <tr>
                           <td align="right" style="width:25%;" >
                                    Created By :
                                </td>
                                <td>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="lblCreatedby" Enabled="false" runat="server" MaxLength="2000" Width="150px"
                                        ></asp:TextBox> &nbsp; &nbsp;
                                         <asp:TextBox ID="txtCreatedDate" Enabled="false" runat="server" MaxLength="2000" Width="100px"
                                        ></asp:TextBox>&nbsp; &nbsp;&nbsp; &nbsp;
                                        <asp:CheckBox ID="chkUrgent" runat="server" Enabled="false" Text="Urgent" />
                                </td>
                          </tr>
                          <tr>
                           <td align="right" style="width:25%;">
                                    Proposed Status :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="right">
                                    *
                                </td>
                                <td align="left">
                                   <asp:TextBox ID="txtProposedStatus" Enabled="false" runat="server" MaxLength="2000" Width="350px"
                                        ></asp:TextBox>
                                </td>
                          
                          </tr>
                          <tr>
                           <td  style="width: 25%" align="right">
                                    For period of :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="right">
                                    *
                                </td>
                                <td align="left">
                                   <asp:TextBox ID="txtPeriod" Enabled="false" runat="server" MaxLength="2000" Width="350px"
                                        ></asp:TextBox>
                                </td>
                          </tr>
                          <tr>
                          <td align="right" style="width: 25%">
                                    Justification Remarks :</td>
                                <td align="right" style="color: #FF0000; width: 1%">
                                    *</td>
                                <td align="left">
                                      <asp:TextBox ID="txtRemarks" runat="server" Enabled="false" MaxLength="2000" TextMode="MultiLine"
                                        Width="350px" Height="50px" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ReqRemarks" runat="server" Display="None" ErrorMessage=" Justification Ramarks is mandatory field."
                                        ControlToValidate="txtRemarks" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator></td>
                               
                          </tr>
                          <tr>
                           
                            <td align="right" style="width: 25%">
                                    Supplier Scope :
                                </td>
                                <td align="right" style="color: #FF0000; width: 1%">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <div style="float: left; text-align: left; width: 350px; height: 60px; overflow-x: hidden;
                                        border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                        background-color: #ffffff;">
                                        <asp:CheckBoxList ID="chkScope" Enabled="false"  RepeatLayout="Flow" RepeatDirection="Horizontal"
                                            runat="server">
                                        </asp:CheckBoxList>
                                    </div>
                                </td>
                          </tr>
                             
                                <tr>
                              
                                 <td align="right" style="width: 25%">
                                     Port of Operation/Supply :</td>
                                 <td align="right" style="color: #FF0000; width: 1%">
                                     &nbsp;</td>
                                 <td align="left">
                                     
                                                <div style="float: left; text-align: left; width: 350px; height: 60px; overflow-x: hidden;
                                                    border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                    background-color: #ffffff;">
                                                    <asp:CheckBoxList ID="chkPort" Enabled="false" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                                    </asp:CheckBoxList>
                                                </div>
                                     </td>
                                
                            </tr>
                               
                          </table>
                          </td>
                          <td valign="top" align="left" style="width:50%;">
                          <table width="100%">
                          <tr>
                            <td align="right" style="width: 25%">
                                    Evaluation Status :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="left">
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="lblEvalStatus" Enabled="false" runat="server" MaxLength="2000" Width="350px"
                                        ></asp:TextBox>
                                </td>
                          
                          </tr>
                       
                          <tr>
                            <td colspan="3">
                             <div  style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px;
                color: Black; height: 100%;">
                            <table width="100%">
                            
                            <tr>
                              <td align="right" style="width: 25%">
                                    1st Approver Name :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="right">
                                    *
                                </td>
                                <td align="left">
                                      <asp:TextBox ID="txtApproverName" Enabled="false" runat="server" MaxLength="2000" Width="350px"
                                        ></asp:TextBox>
                                </td>
                            
                            </tr> 
                              <tr>
                                 <td align="right" valign="middle" style="width: 25%">
                                     1st Approver Remarks :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="right">
                                    
                                </td>
                                <td align="left">
                                  <asp:TextBox ID="txtVerificationRemrks" Enabled="false" runat="server" MaxLength="2000"
                                        TextMode="MultiLine" Width="350px" Height="50px" ></asp:TextBox>
                                </td>
                            </tr>
                              <tr>
                                  <td align="right" valign="middle" style="width: 25%">
                                      1st Approved Date :</td>
                                  <td align="right" style="color: #FF0000; width: 1%">
                                      &nbsp;</td>
                                  <td align="left">
                                      <asp:TextBox ID="txtApprovedDate" Enabled="false" runat="server" MaxLength="2000" Width="350px"
                                        ></asp:TextBox></td>
                              </tr>
                            
                             </table>
                             </div>
                              </td>
                              
                             </tr>
                         <tr>
                            <td colspan="3">
                             <div  style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px;
                color: Black; height: 100%;">
                            <table width="100%">
                           
                           <tr>
                                <td style="width: 25%" align="right">
                                    Final Approver Name :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="right">
                                    *
                                </td>
                                <td align="left">
                                     <asp:TextBox ID="txtFinalApproverName" Enabled="false" runat="server" MaxLength="2000" Width="350px"
                                        ></asp:TextBox>
                                </td>
                            </tr>
                              <tr>
                               
                                <td style="width: 25%" align="right">
                                    Final Approver Remarks :
                                </td>
                                <td style="color: #FF0000; width: 1%" align="right">
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtApprovalRemrks" Enabled="false" runat="server" MaxLength="2000"
                                        TextMode="MultiLine" Width="350px" Height="50px" ></asp:TextBox>
                                </td>
                            </tr>
                              <tr>
                                  <td align="right" style="width: 25%">
                                     Final Approved Date :</td>
                                  <td align="right" style="color: #FF0000; width: 1%">
                                      &nbsp;</td>
                                  <td align="left" valign="top">
                                         <asp:TextBox ID="txtFinalApprovedDate" Enabled="false" runat="server" MaxLength="2000" Width="350px"
                                        ></asp:TextBox></td>
                              </tr>
                              </table>
                             </div>
                              </td>
                              
                             </tr>
                             
                             <tr id="tr1" runat="server" visible="false" >
                                <td align="right">
                                    Supplier Description :</td>
                                <td align="right" style="color: #FF0000; width: 1%">
                                    &nbsp;</td>
                                <td align="left">
                                    <asp:TextBox ID="txtSupplierDesc" runat="server"  MaxLength="2000" TextMode="MultiLine"
                                        Width="350px" Height="50px" CssClass="txtInput"></asp:TextBox>
                                   </td>
                               <td></td>
                            </tr>
                          </table> 
                          </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 10%; height: 30px;">
                                </td>
                                <td style="color: #FF0000; width: 1%" align="left">
                                </td>
                               
                            </tr>
                         
                           
                        </table>
                      
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td colspan="6">
                            <asp:GridView ID="gvEvalHistory" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" DataKeyNames="ID,EvalID" CellPadding="1" CellSpacing="0"
                                Width="100%" GridLines="both" AllowSorting="true">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <SelectedRowStyle BackColor="Yellow" />
                                 <SelectedRowStyle BackColor="#FFFF00" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ID">
                                        <HeaderTemplate>
                                            Evaluation ID
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierName" runat="server" Text='<%#Eval("EvalID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="20px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Proposed By">
                                        <HeaderTemplate>
                                            Proposed By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPropose_By" runat="server" Text='<%#Eval("Proposed_By")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText=" Created Dated">
                                        <HeaderTemplate>
                                            Created Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedDate" runat="server" Text='<%#Eval("Created_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <HeaderTemplate>
                                            Proposed Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPropose_Status" runat="server" Text='<%#Eval("Propose_Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Period Days">
                                        <HeaderTemplate>
                                            Period(In Days)
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFor_Period" runat="server" Text='<%#Eval("For_Period")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Evaluation Status">
                                        <HeaderTemplate>
                                            Evaluation Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEval_Status" runat="server" Text='<%#Eval("Eval_Status")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Verified by">
                                        <HeaderTemplate>
                                           1st Approved By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVerified_By" runat="server" Text='<%#Eval("Approved_By")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Approved Date">
                                        <HeaderTemplate>
                                          1st Approved Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved_Date" runat="server" Text='<%#Eval("Approved_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Verifier remarks">
                                        <HeaderTemplate>
                                           1st Approver Remarks
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVerifierRemarks" runat="server" Text='<%#Eval("Verifier_Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Evaluation Status">
                                        <HeaderTemplate>
                                            Final Approved By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved_By" runat="server" Text='<%#Eval("FinalApproved_By")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approver remarks">
                                        <HeaderTemplate>
                                            Final Approved Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblApprovedDate" runat="server" Text='<%#Eval("FinalApproved_Date","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approver remarks">
                                        <HeaderTemplate>
                                            Final Approver Remarks
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproverRemarks" runat="server" Text='<%#Eval("Approver_Remarks")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           
                                                        <asp:ImageButton ID="ImgView" runat="server" OnCommand="btnView_Click" Text="View" CommandArgument='<%#Eval("[ID]") + "," + Eval("[EvalID]") %>'
                                                            CommandName="Select" ForeColor="Black"  ToolTip="View"
                                                            ImageUrl="~/Images/asl_view.png" Height="16px"></asp:ImageButton>
                                                   
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                       
                    </td>
                </tr>
            </table>
              <div style="display: none;">
                            <asp:TextBox ID="txtEvalID" runat="server" Width="1px"></asp:TextBox>

                        </div>
             </contenttemplate>
                </asp:UpdatePanel>
            </div>
              <div style="display: none;">
                <asp:TextBox ID="txtSupplier_Code" Width="1px" runat="server"></asp:TextBox>
            </div>
        </asp:Panel>
    </center>
    </form>
     </td></tr></table>
</body>
</html>
