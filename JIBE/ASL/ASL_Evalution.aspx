<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_Evalution.aspx.cs" Inherits="ASL_Evalution" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
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
    <style type="text/css">
        .style1
        {
            width: 553px;
        }
    </style>
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
                <table width="100%" cellpadding="3" cellspacing="0">
                  <tr>
                  <td style="width:33%;" align="left" valign="top" >
                  <table width="100%">
                  <tr>
                    <td align="right" style="width:40%;"  >
                            Supplier Code :
                        </td>
                    <td align="left" style="width:60%;">
                            <asp:TextBox ID="txtSupplierCode" Enabled="false" ReadOnly="true" runat="server"
                                MaxLength="2000" Width="200px"></asp:TextBox>
                        </td>
                  </tr>
                
                  <tr>
                  <td align="right" style="width:40%;">
                            Company Name :
                        </td>
                        <td align="left" style="width:60%;">
                          <%--  <asp:TextBox ID="txtCompanyName" Enabled="false" ReadOnly="true" runat="server" MaxLength="2000"
                                Width="200px"></asp:TextBox>--%>
                                <asp:TextBox ID="txtCompanyName" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                TextMode="MultiLine" Width="200px" Height="50px" ></asp:TextBox>
                        </td>
                  </tr>
                  <tr>
                    <td align="right" valign="top" style="width:40%;">
                            Address :
                        </td>
                        <td align="left" style="width:60%;">
                            <asp:TextBox ID="txtAddress" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                TextMode="MultiLine" Width="200px" Height="50px" ></asp:TextBox>
                        </td>
                  </tr>
                 
            
                  </table>
                  
                  
                  </td>
                  <td style="width:33%;" align="left" valign="top">
                  <table width="100%">
                  <tr>
                      
                        <td style="width: 40%" align="right">
                            Status :
                        </td>
                        <td align="left" style="width: 60%">
                            <asp:TextBox ID="txtStatus" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                  <tr>
                        <td style="width: 40%" align="right">
                            Counterparty Type :
                        </td>
                        <td align="left" style="width: 60%">
                            <asp:TextBox ID="txtSubType" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                  
                  <td align="right" valign="top" style="width: 40%">
                            Email :
                        </td>
                        <td align="left" style="width: 60%">
                            <asp:TextBox ID="txtEmail" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                Width="200px"></asp:TextBox>
                        </td>
                  </tr>
                    <tr>
                      
                        <td style="width: 40%" align="right" valign="top">
                            Phone :
                        </td>
                        <td align="left" valign="top" style="width: 60%">
                            <asp:TextBox ID="txtPhone" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    
                     
                      <tr>
                  <td align="right" style="width: 40%">
                            Contact Phone :
                        </td>
                        <td align="left" style="width: 60%">
                            <asp:TextBox ID="txtPICPhone" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                Width="200px"></asp:TextBox>
                        </td>
                  </tr>
              
                        <tr>
                  <td align="right" style="width:40%;">
                            Contact Name :
                        </td>
                        <td align="left" style="width: 60%">
                            <asp:TextBox ID="txtPICName" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                Width="200px"></asp:TextBox>
                        </td>
                  </tr>
                 
                  </table>
                  </td>
                   <td style="width:33%;" align="left" valign="top">
                  <table width="100%">
                  <tr>
                   <td align="right" style="width:40%;">
                            Supplier Type :
                        </td>
                        <td align="left" style="width: 60%">
                            <asp:TextBox ID="txtType" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                Width="200px"></asp:TextBox>
                        </td>
                  </tr>
                   <tr>
                        
                        <td style="width: 40%" align="right">
                            Incorporation :
                        </td>
                        <td align="left" style="width: 60%">
                            <asp:TextBox ID="txtIncorporation" Enabled="false" ReadOnly="true" runat="server"
                                MaxLength="2000" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        
                        <td align="right" style="width: 40%">
                            Fax :
                        </td>
                        <td align="left" style="width: 60%">
                            <asp:TextBox ID="txtFax" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        
                        <td style="width: 40%" align="right">
                            Contact Email :
                        </td>
                        <td align="left" style="width: 60%">
                            <asp:TextBox ID="txtPICEmail" Enabled="false" runat="server" ReadOnly="true" MaxLength="2000"
                                Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                          <tr>
                        
                        <td align="right" style="width:40%;">
                            Registered Data :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRegistedData" ReadOnly="true" Enabled="false" runat="server"
                                MaxLength="2000" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                  </table>
                  </td>
                  </tr>

                    <tr>
                        <td align="right" style="width: 10%; height: 10px;">
                        </td>
                        
                        <td align="right" colspan="2">
                        </td>
                    </tr>
                    
                </table>
                <table width="100%">
                <tr>
                <td align="center" style="color: #FF0000; width: 1%" align="left">
                            <asp:Button ID="btnRegisteredData" Visible="false" runat="server" Text="Registered Data"
                                OnClick="btnRegisteredData_Click" />&nbsp;
                            <%--<asp:Button ID="btnRemarks" runat="server" Text="Supplier Remarks" OnClientClick='OpenScreen2(null,null);return false;' />--%>
                             <%-- <asp:Button ID="btnStatistics" runat="server" Text="Statistics" OnClientClick='OpenScreen12(null,null);return false;' />--%>
                            <asp:Button ID="btnRemarks" runat="server" Text="Supplier Remarks" 
                            onclick="btnRemarks_Click"  />&nbsp;
                              <asp:Button ID="btnStatistics" runat="server" Text="Statistics" 
                            onclick="btnStatistics_Click"  />
                        </td>
                </tr>
                </table>
                <table width="100%">
                <tr>
                <td width="50%" valign="top">
                <div id="divRemarks" visible="false" runat="server" style="height:200px;overflow-y:auto;">      
                             <asp:GridView ID="gvRemarks" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                DataKeyNames="ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                AllowSorting="true" OnRowDataBound="gvRemarks_RowDataBound">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Entry Date">
                                        <HeaderTemplate>
                                            Entry Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEntryDate" runat="server" Text='<%#Eval("CreatedDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Created By">
                                        <HeaderTemplate>
                                            Created By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("CREATED_BY")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks Type">
                                        <HeaderTemplate>
                                            Remarks Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table>
                                                <td>
                                                    <asp:ImageButton ID="ImgAmend" Width="20px" Height="20px" Visible="false" runat="server"
                                                        Text="Update" ForeColor="Black" ToolTip="Amendments" ImageUrl="~/Images/Pencil_Edit.png">
                                                    </asp:ImageButton>
                                                    <asp:ImageButton ID="imgGeneral" Width="20px" Height="20px" Visible="false" runat="server"
                                                        Text="Update" ForeColor="Black" ToolTip="General" ImageUrl="~/Images/Blue_Square_Flag.jpg">
                                                    </asp:ImageButton>
                                                    <asp:ImageButton ID="imgWarning" Width="20px" Height="20px" Visible="false" runat="server"
                                                        Text="Update" ForeColor="Black" ToolTip="Yellow Card" ImageUrl="~/Images/Orange_Square_Flag.jpg">
                                                    </asp:ImageButton>
                                                    <asp:ImageButton ID="imageRed" Width="20px" Height="20px" runat="server" Text="Update"
                                                        ForeColor="Black" ToolTip="Red Card" Visible="false" ImageUrl="~/Images/Red_Square_Flag.jpg">
                                                    </asp:ImageButton>
                                                </td>
                                            </table>
                                            <%-- <asp:Label ID="lblRemarksType" runat="server" Text='<%#Eval("REMARKS_TYPE")%>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <HeaderTemplate>
                                            Remarks
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("REMRAKS")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                
                                </Columns>
                            </asp:GridView>
                            </div>

                </td>
                                <td width="50%" valign="top"  >
                                <div id="divStatistics" visible="false" runat="server" style="height:200px; overflow-y:auto;">
                                <asp:Literal ID="ltSupplierStatistics" runat ="server"></asp:Literal>
                                </div>
                                </td>
                </tr>
                </table>
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
                                        <asp:CheckBox ID="chkUrgent" runat="server" Text="Urgent" />
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
                                    <asp:DropDownList ID="ddlProposedStatus" runat="server" Width="350px" CssClass="txtInput">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvProposedStatus" runat="server" Display="None"
                                        InitialValue="0" ErrorMessage="Proposed Status is mandatory field." ControlToValidate="ddlProposedStatus"
                                        ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                    <asp:DropDownList ID="ddlforPeriod" runat="server" Width="350px" CssClass="txtInput">
                                        <asp:ListItem Value="30" Text="30 days" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60 days"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90 days"></asp:ListItem>
                                        <asp:ListItem Value="180" Text="180 days"></asp:ListItem>
                                        <asp:ListItem Value="365" Text="365 days"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="ReqPeriod" runat="server" InitialValue="0" Display="None"
                                        ErrorMessage="For period of is mandatory field." ControlToValidate="ddlforPeriod"
                                        ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                          </tr>
                          <tr>
                          <td align="right" style="width: 25%">
                                    Justification Remarks :</td>
                                <td align="right" style="color: #FF0000; width: 1%">
                                    *</td>
                                <td align="left">
                                      <asp:TextBox ID="txtRemarks" runat="server" Enabled="false" MaxLength="2000" TextMode="MultiLine"
                                        Width="350px" Height="50px" CssClass="txtInput"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ReqRemarks" runat="server" Display="None" ErrorMessage=" Justification Remarks is mandatory field."
                                        ControlToValidate="txtRemarks" ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator></td>
                               
                          </tr>
                          <tr>
                            <td colspan="3">
                             <div  style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px;
                color: Black; height: 100%;">
                            <table width="100%">
                          <tr>
                            <td align="right" style="width: 25%">
                                    Supplier Scope :
                                </td>
                                <td align="right" style="color: #FF0000; width: 1%">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <biv>
                                                <asp:DropDownList ID="ddlScope" runat="server" Width="250px" CssClass="txtInput">
                                                </asp:DropDownList>
                                             
                                                <asp:Button ID="btnScopeAdd" runat="server" Text="Add Scope" OnClick="btnAdd_Click" />
                                                <asp:Button ID="btnScopeRemove" runat="server" Text="Remove Scope" Visible="false"
                                                    OnClick="btnRemove_Click" />
                                                <br />
                                             
                                                        <div style="float: left; text-align: left; width: 350px; height: 60px; overflow-x: hidden;
                                                            border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                            background-color: #ffffff;">
                                                            <asp:CheckBoxList ID="chkScope"  RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                                runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                   
                                                </biv>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                          </tr>
                              </table>
                             </div>
                              </td></tr>
                                <tr>
                                <td colspan="3">
                             <div  style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px;
                color: Black; height: 100%;">
                            <table width="100%">
                            <tr>
                                 <td align="right" style="width: 25%">
                                     Port of Operation/Supply :</td>
                                 <td align="right" style="color: #FF0000; width: 1%">
                                     &nbsp;</td>
                                 <td align="left">
                                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div>
                                                <asp:DropDownList ID="ddlPort" runat="server" Width="250px" CssClass="txtInput">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnPortAdd" runat="server" Text="Add Port" OnClick="btnPortAdd_Click" />
                                                <asp:Button ID="btnPortRemove" runat="server" Text="Remove Port" Visible="false"
                                                    OnClick="btnPortRemove_Click" />
                                                <br />
                                                <div style="float: left; text-align: left; width: 350px; height: 60px; overflow-x: hidden;
                                                    border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                                    background-color: #ffffff;">
                                                    <asp:CheckBoxList ID="chkPort" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel></td>
                                
                            </tr>
                                </table>
                             </div>
                              </td></tr>
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
                                    <asp:DropDownList ID="ddlApproverName" runat="server" Width="350px" CssClass="txtInput">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="ReqApproverName" runat="server" InitialValue="0"
                                        Display="None" ErrorMessage="Approval Name is mandatory field." ControlToValidate="ddlApproverName"
                                        ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                        TextMode="MultiLine" Width="350px" Height="50px" CssClass="txtInput"></asp:TextBox>
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
                                    <asp:DropDownList ID="ddlFinalApproverName" runat="server" Width="350px" CssClass="txtInput">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="ReqFinalApproverName" runat="server" InitialValue="0"
                                        Display="None" ErrorMessage="Final Approval Name is mandatory field." ControlToValidate="ddlFinalApproverName"
                                        ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                        TextMode="MultiLine" Width="350px" Height="50px" CssClass="txtInput"></asp:TextBox>
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
                             
                             <tr>
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
                            <tr>
                                <td colspan="2" align="center">
                                    
                                    <asp:Button ID="btnSaveEvalution" Text="Save Evaluation" runat="server" Visible="false"
                                        ValidationGroup="vgSubmit" OnClick="btnSaveEvalution_Click" />&nbsp;
                                    <asp:Button ID="btnVerify" Text="Submit For Approval" runat="server" Visible="false"
                                        ValidationGroup="vgSubmit" OnClick="btnVerify_Click" />&nbsp;
                                    <asp:Button ID="btnRecallVerification" Text="Recall Approval" Visible="false"
                                        runat="server"  OnClick="btnRecallVerification_Click" />
                                    <asp:Button ID="btnforApproval" Text="Submit For Final Approval" Visible="false" ValidationGroup="vgSubmit1"
                                        runat="server" OnClick="btnforApproval_Click" UseSubmitBehavior="false" />&nbsp;
                                         <asp:Button ID="btnRework" Text="Re-Work" Visible="false" ValidationGroup="vgSubmit"
                                        runat="server" OnClick="btnRework_Click" />&nbsp;
                                    <asp:Button ID="btnRecallApproval" Text="Recall Final Approval" Visible="false" runat="server"
                                         OnClick="btnRecallApproval_Click" />&nbsp;
                                    <asp:Button ID="btnApproval" Text="Approve" Visible="false" ValidationGroup="vgSubmit2"
                                        runat="server" OnClick="btnApproval_Click" />&nbsp;
                                         <asp:Button ID="btnRejected" Text="Reject" Visible="false" ValidationGroup="vgSubmit"
                                        runat="server" OnClick="btnRejected_Click" />&nbsp;
                                    <asp:Button ID="btnDelete" Text="Delete Evaluation" Visible="false" OnClientClick="return confirm('Are you sure want to delete?')" runat="server"
                                        ForeColor="Red" ValidationGroup="vgSubmit" OnClick="btnDelete_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 10%; height: 20px;">
                                    <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="vgSubmit" />
                                </td>
                                <td style="color: #FF0000; width: 1%" align="left">
                                </td>
                               
                            </tr>
                        </table>
                        <div runat="server" id="dvEvalProgress" visible="false">
                            <asp:GridView ID="gvEvalProgress" runat="server" EmptyDataText="NO RECORDS FOUND"
                                AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="1" CellSpacing="0"
                                Width="100%" GridLines="both" AllowSorting="true" OnRowDataBound="gvEvalProgress_RowDataBound">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ID">
                                        <HeaderTemplate>
                                            Eval. ID
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierName" runat="server" Text='<%#Eval("EvalID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created By">
                                        <HeaderTemplate>
                                            Created By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPropose_By" runat="server" Text='<%#Eval("Proposed_By")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
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
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
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
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Approved By">
                                        <HeaderTemplate>
                                          1st Approved By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproved_By" runat="server" Text='<%#Eval("Approved_By")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
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
                                            <asp:Label ID="lblFinalApproved_By" runat="server" Text='<%#Eval("FinalApproved_By")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
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
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                       
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
