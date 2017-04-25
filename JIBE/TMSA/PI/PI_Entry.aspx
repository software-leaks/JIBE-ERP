<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PI_Entry.aspx.cs" Inherits="PI_Entry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />

   
  <script type="text/javascript">

      $(document).ready(function () {
               toggleAdvSearch();
      });


     function toggleAdvSearch() {

         if ($('#chkIsWorklist').is(':checked')) {

             $("#dvWorkList").show();
             $("#dvInspection").hide();
             $("#dvVettingType").hide();
             $("#dvObservation").hide();
         }
         else {
             $("#dvWorkList").hide();
         }
      if ($('#chkInspection').is(':checked')) {

          $("#dvInspection").show();
          $("#dvWorkList").hide();
          $("#dvVettingType").hide();
          $("#dvObservation").hide();
      }
          else {

              $("#dvInspection").hide();

          }

          if ($('#chkVetting').is(':checked')) {

              $("#dvInspection").hide();
              $("#dvWorkList").hide();
              $("#dvVettingType").show();
              $("#dvObservation").hide();
          }
          else {

              $("#dvVettingType").hide();

          }

          if ($('#chkObservation').is(':checked')) {

              $("#dvInspection").hide();
              $("#dvWorkList").hide();
              $("#dvVettingType").hide();
              $("#dvObservation").show();
              
          }
          else {

              $("#dvObservation").hide();

          }


          
      }


      function ValidateSetting() {


          if ($("#txtName").val() == "") {
              alert('PI Name required!')
              return false;
          }
          else if ($("#txtPICode").val() == "") {

              alert('PI Code required!')
              return false;
          }

          else if ($('#ddlInterval :selected').length == 0 || $('#ddlInterval :selected').val() == '0') {
              alert('Please select an Interval')
              return false;
          }

          else if ($('#ddlUOM :selected').length == 0 || $('#ddlUOM :selected').val() == '0') {
              alert('Please select an Unit')
              return false;
          }

          else if ($('#ddlStatus :selected').length == 0 || $('#ddlStatus :selected').val() == '2') {
              alert('Please select a Status')
              return false;
          }

          else if ($('#chkIsWorklist').is(':checked')) {

              if ($("#txtEffectivedate").val() != "") {
                  var date1 = document.getElementById("txtEffectivedate").value;
                  if ($.trim($("#txtEffectivedate").val()) != "") {
                      if (IsInvalidDate(date1, strDateFormat)) {
                          alert("Invalid migration date!");
                          return false;
                      }
                  }

              }

              if ($('#lstJOB :selected').length == 0) {

                  alert('Please select atleast one Job Type !')
                  return false;
              }


              else if ($('#ddlAssignor :selected').length == 0) {
                  alert('Please select atleast one Assigned by!')
                  return false;
              }
              else if ($('#ddlNature :selected').length == 0) {
                  alert('Please select atleast one Nature category!')
                  return false;
              }
              else if ($('#ddlPrimary :selected').length == 0) {
                  alert('Please select atleast one Primary category!')
                  return false;
              }
              else if ($('#ddlSecondary :selected').length == 0) {
                  alert('Please select atleast one Secondary category!')
                  return false;
              }
              else if ($('#ddlMinor :selected').length == 0) {
                  alert('Please select atleast one Minor category !')
                  return false;
              }

          }
          else if ($('#chkInspection').is(':checked')) {

          if ($("#txtMigrateFormInspection").val() != "") {
              var date1 = document.getElementById("txtMigrateFormInspection").value;
              if ($.trim($("#txtMigrateFormInspection").val()) != "") {
                  if (IsInvalidDate(date1, strDateFormat)) {
                      alert("Invalid migration date!");
                      return false;
                  }
              }
          }
            if ($('#ddlInspectionType :selected').length == 0) {

                  alert('Please select atleast one Inspection Type!')
                  return false;
              }

          }

          else if ($('#chkVetting').is(':checked')) {

              if ($("#txtMigrateFormVetting").val() != "") {
                  var date1 = document.getElementById("txtMigrateFormVetting").value;
                  if ($.trim($("#txtMigrateFormVetting").val()) != "") {
                      if (IsInvalidDate(date1, strDateFormat)) {
                          alert("Invalid vmigration date! ");
                          return false;
                      }
                  }
              }
              if ($('#ddlVettingType :selected').length == 0) {

                  alert('Please select atleast one Vetting Type !')
                  return false;
              }

          }


          else if ($('#chkObservation').is(':checked')) {

              if ($("#txtMigrateFormObservation").val() != "") {
                  var date1 = document.getElementById("txtMigrateFormObservation").value;
                  if ($.trim($("#txtMigrateFormObservation").val()) != "") {
                      if (IsInvalidDate(date1, strDateFormat)) {
                          alert("Invalid vmigration date! ");
                          return false;
                      }
                  }
              }
              if ($('#lstObservationVettingType :selected').length == 0) {

                  alert('Please select atleast one Vetting Type !')
                  return false;
              }

              if ($('#lstObservationCategory :selected').length == 0) {

                  alert('Please select atleast one category !')
                  return false;
              }
              if ($('#lstObservationType :selected').length == 0) {

                  alert('Please select atleast one observation Type !')
                  return false;
              }
              if ($('#lstRiskLevel :selected').length == 0) {

                  alert('Please select atleast one risk level !')
                  return false;
              }

          }


          else
              return true;
        }

  </script>
    <style type="text/css">
        .style1
        {
            width: 1%;
        }
          .hide
        {
            display: none;
        }
    </style>
   
  
</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;height:90%">
     


    <form id="form1" runat="server">

    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB; "  >
        <center>
        <asp:ScriptManager ID="ScriptManager1" runat="server">        </asp:ScriptManager>
            <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <center>
        <div id="dialog" style="display: none" ></div>
           <div style="border: 1px solid #cccccc" class="page-title">
                PI Details
            </div>
            <table border="0" cellpadding="2" cellspacing="2" width="800px">
            <tr>

                    <td align="right" style="width: 15%">
                        PI Name &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left" style="width: 35%">
                        <asp:TextBox ID="txtName" runat="server" Width="250px" MaxLength="150" CssClass="txtInput"> </asp:TextBox>
                         <%--<asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtName" Display="None"  ErrorMessage="PI Name required!" ValidationGroup="vgSubmitt" runat="server"></asp:RequiredFieldValidator>--%>
                    </td>
                       <td align="right" style="width: 15%">
                        PI Code &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left" style="width: 35%">
                        <asp:TextBox ID="txtPICode" runat="server" Width="100px" MaxLength="10" CssClass="txtInput"> </asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvPIcode" ControlToValidate="txtPICode" Display="None"  ErrorMessage="PI Code required!" ValidationGroup="vgSubmitt" runat="server"></asp:RequiredFieldValidator>--%>
                    </td>
            
            </tr>
               <tr>
                    <td align="right" style="width: 15%">
                        Interval &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                    *
                    </td>
                    <td align="left" style="width: 35%">
                        <asp:DropDownList ID="ddlInterval" runat="server" Width="100px" CssClass="txtInput">
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="rfvInterval" ControlToValidate="ddlInterval" Display="None"  InitialValue="0" ErrorMessage="Select interval!" ValidationGroup="vgSubmitt" runat="server"></asp:RequiredFieldValidator>--%>
                    </td>
                    <td align="right" style="width: 15%">
                        Unit &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left" style="width: 35%">
                     <asp:DropDownList ID="ddlUOM" runat="server" Width="100px" CssClass="txtInput">
                        </asp:DropDownList>

                         <%--<asp:RequiredFieldValidator ID="rfvUOM" ControlToValidate="ddlUOM" Display="None"  InitialValue="0" ErrorMessage="Select an unit!" ValidationGroup="vgSubmitt" runat="server"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>

             <tr>
                    <td align="right" style="width: 15%">
                        Description &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
  
                    </td>
                    <td align="left" colspan="4">
                        <asp:TextBox ID="txtDescription" runat="server" Width="450px" MaxLength="500" TextMode ="MultiLine" CssClass="txtInput"> </asp:TextBox>

                        <asp:RegularExpressionValidator ID="rgDescription" ValidationGroup="vgSubmitt"
                                ControlToValidate="txtDescription" ErrorMessage="Description can't exceed 500 characters"
                                ValidationExpression="^[\s\S]{0,500}$" runat="server" Display="None"  SetFocusOnError="true" /> 

                    </td>

                </tr>

      <tr>
                    <td align="right" style="width: 15%">
                        Context &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
  
                    </td>
               <td align="left" colspan="4">
                        <asp:TextBox ID="txtContext" runat="server" Width="450px" MaxLength="250" TextMode ="MultiLine" CssClass="txtInput"> </asp:TextBox>
                    </td>

                </tr>
                <tr>
                <td align="right"> <asp:Label ID="lblListSource" runat="server" Text="Data Source:" ></asp:Label> </td>
                <td>
                    &nbsp;
                </td>
                <td align="left" colspan="2">
                <asp:DropDownList ID="ddlListSource" runat="server"  Width="90%"  AutoPostBack="true" >
                                </asp:DropDownList>
                                </td>
                </tr>

                <tr>
                
                 <td align="right" style="width: 15%">
                    For SBU &nbsp;:&nbsp;
                    </td>
               <td style="color: #FF0000; " align="right" class="style1">
                         &nbsp;
                    </td>
                <td align="left">
                <asp:CheckBox ID = "chkMeasuredForSBU" runat="server" />
                </td>
                </tr>
                
                <tr>
                
                 <td align="right" style="width: 15%">
                     Ballast/Laden &nbsp;:&nbsp;
                    </td>
                     <td style="color: #FF0000; " align="right" class="style1">
                         &nbsp;
                    </td>
                <td  align="left">
                <asp:CheckBox ID = "chkBallastLaden" runat="server" />
                </td>
                </tr>
               <tr>
                    <td align="right" style="width: 15%">
                 PI Status
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="156px" CssClass="control-edit">
                            <asp:ListItem Text="-Select-" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorddlStatus" runat="server" ValidationGroup="vgSubmitt"
                            Display="None" ErrorMessage="Please select Status !" ControlToValidate="ddlStatus"
                            InitialValue="2"></asp:RequiredFieldValidator>--%>   
                    </td>
                    <td align="left" colspan="3"> 
                    <asp:CheckBox ID="chkIsWorklist" Text="For worklist"  OnCheckedChanged="chkIsWorklist_OnCheckedChanged" AutoPostBack="true" runat="server" />
                    <asp:CheckBox ID="chkInspection" Text="For inspection"  OnCheckedChanged="chkInspection_OnCheckedChanged" AutoPostBack="true" runat="server" />
                    <br />
                    <asp:CheckBox ID="chkVetting" Text="For vetting type"  OnCheckedChanged="chkVetting_OnCheckedChanged" AutoPostBack="true" runat="server" />
                     <asp:CheckBox ID="chkObservation" Text="Vetting observation"  OnCheckedChanged="chkObservation_OnCheckedChanged" AutoPostBack="true" runat="server" />

                     <asp:HiddenField ID="hdPIID" runat="server" />
                      <input type="hidden" runat="server" id="hdnNature" value="0" />
                        <input type="hidden" runat="server" id="hdnPrimary" value="0" />
                        <input type="hidden" runat="server" id="hdnSecondary" value="0" />
                        <input type="hidden" runat="server" id="hdnMinor" value="0" />
                        <input type="hidden" runat="server" id="hdnInspection" value="0" />
                    </td>
                </tr>


                <tr>
                
                <td colspan="6" align="left">

                <div id="dvWorkList" style="background-color: #efefef;width:90%;" class="hide">
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
                        <tr>
                            <td valign="top" style="border: 1px solid #aabbdd;" align="left">
                                <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                    <tr style="background-color: #aabbdd">
                                        <td style="text-align: left;" colspan="6">
                                            <asp:Label ID="abc" runat="server" Text="Worklist Setting" Font-Bold="true"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td  style="text-align: right; width:25%">
                                     Activate Scheduler:
                                    
                                    </td>
                                    <td>
                                    &nbsp;
                                    </td>
                                    <td>
                                    <asp:CheckBox ID="chkActivate_Scheduler" runat="server" />
                                    
                                    </td>
                                        <td style="text-align: right; width:25%">
                                            Migrate From:
                                        </td>                      
                                    <td>&nbsp;</td>
                                    <td>
   
                                         <asp:TextBox ID="txtEffectivedate" runat="server" onkeypress="return false;" ></asp:TextBox>                    
                                                <ajaxToolkit:CalendarExtender ID="cteEffectiveDate" runat="server"  Format="dd-MM-yyyy"  TargetControlID="txtEffectivedate">
                                                </ajaxToolkit:CalendarExtender>
                                      </td>
                                    </tr>
                                    <tr>
                                  <td  align="right">
                                            Job Type:
                                        </td>
                                         <td style="color: #FF0000; " align="right" class="style1">
                                            *
                                        </td>
                                    <td style="text-align: left; width:24%">
                                    <asp:ListBox ID="lstJOB" runat="server" Width="150px" SelectionMode="Multiple" >
                                      <asp:ListItem Text="JOB" Value="JOB" ></asp:ListItem>
                                    <asp:ListItem Text="NCR" Value="NCR" ></asp:ListItem>
                                    <asp:ListItem Text="PMS" Value="PMS" ></asp:ListItem> 
                                              
                                      </asp:ListBox>


                                    
                                    </td>
                                    
                                  
                                   <td style="text-align: right; width:25%">
                                            Assigned By:
                                        </td>
                                        <td style="color: #FF0000; " align="right" class="style1">
                                            *
                                        </td>
                                        <td style="text-align: left; width:24%">
                                            <asp:ListBox ID="ddlAssignor" runat="server" Width="150px" SelectionMode="Multiple"
                                              ></asp:ListBox>
                             
                                        </td>
                                    
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width:25%">
                                            Nature:
                                        </td>
                                        <td style="color: #FF0000; " align="right" class="style1">
                                            *
                                        </td>
                                        <td style="text-align: left;width:24%">
                                            <asp:ListBox ID="ddlNature" runat="server" Width="150px"  AutoPostBack="True"  SelectionMode="Multiple"
                                                OnSelectedIndexChanged="ddlNature_SelectedIndexChanged">
                                               
                                            </asp:ListBox>
                                        </td>
                                        <td style="text-align: right; width:25%">
                                            Primary:
                                        </td>
                                        <td style="color: #FF0000; " align="right" class="style1">
                                            *
                                        </td>
                                        <td style="text-align: left;width:24%">
                                            <asp:ListBox ID="ddlPrimary" runat="server" Width="150px"  AutoPostBack="True"  SelectionMode="Multiple"
                                                OnSelectedIndexChanged="ddlPrimary_SelectedIndexChanged">
                                               
                                            </asp:ListBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="text-align: right;width:25%">
                                            Secondary:
                                        </td>
                                        <td style="color: #FF0000; " align="right" class="style1">
                                            *
                                        </td>
                                        <td style="text-align: left;width:24%">
                                            <asp:ListBox ID="ddlSecondary" runat="server" Width="150px"  AutoPostBack="True"  SelectionMode="Multiple"
                                                OnSelectedIndexChanged="ddlSecondary_SelectedIndexChanged">
                                               
                                            </asp:ListBox>
                                        </td>
                                         <td style="text-align: right;width:25%">
                                            Minor:
                                        </td>
                                        <td style="color: #FF0000; " align="right" class="style1">
                                            *
                                        </td>
                                        <td style="text-align: left;width:24%">
                                            <asp:ListBox ID="ddlMinor" runat="server" Width="150px"  AutoPostBack="True"  SelectionMode="Multiple">
                                              
                                            </asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>



                        </tr>
                    </table>
                </div>
                <div id="dvInspection" style="background-color: #efefef;width:90%;" class="hide">
                
                
                             <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
                        <tr>
                            <td valign="top" style="border: 1px solid #aabbdd;" align="left">
                                <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                    <tr style="background-color: #aabbdd">
                                        <td style="text-align: left;" colspan="6">
                                            <asp:Label ID="Label1" runat="server" Text="Inspection Setting" Font-Bold="true"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td  style="text-align: right; width:25%">
                                     Activate Scheduler:
                                    
                                    </td>
                                    <td>
                                    &nbsp;
                                    </td>
                                    <td>
                                    <asp:CheckBox ID="chkActivateInspectionScheduler" runat="server" />
                                    
                                    </td>
                                        <td style="text-align: right; width:25%">
                                            Migrate From:
                                        </td>                      
                                    <td>&nbsp;</td>
                                    <td>
   
                                         <asp:TextBox ID="txtMigrateFormInspection" runat="server" onkeypress="return false;" ></asp:TextBox>                    
                                                <ajaxToolkit:CalendarExtender ID="ceMigrateFormInspection" runat="server"  Format="dd-MM-yyyy"  TargetControlID="txtMigrateFormInspection">
                                                </ajaxToolkit:CalendarExtender>
                                      </td>
                                    </tr>
                                    <tr>

                                    
                                  
                                   <td style="text-align: right; width:25%">
                                            Inspection Type:
                                        </td>
                                        <td style="color: #FF0000; " align="right" class="style1">
                                            *
                                        </td>
                                        <td style="text-align: left; width:24%">
                                            <asp:ListBox ID="ddlInspectionType" runat="server" Width="150px" SelectionMode="Multiple"
                                              ></asp:ListBox>
                             
                                        </td>
                                    
                                    </tr>


                                </table>
                            </td>



                        </tr>
                    </table>
                
                
                </div>

                <div id="dvVettingType" style="background-color: #efefef;width:90%;" class="hide">
                
                
                             <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
                        <tr>
                            <td valign="top" style="border: 1px solid #aabbdd;" align="left">
                                <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                    <tr style="background-color: #aabbdd">
                                        <td style="text-align: left;" colspan="6">
                                            <asp:Label ID="lblVetting" runat="server" Text="Vetting Type Setting" Font-Bold="true"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td  style="text-align: right; width:25%">
                                     Activate Scheduler:
                                    
                                    </td>
                                    <td>
                                    &nbsp;
                                    </td>
                                    <td>
                                    <asp:CheckBox ID="chkActivateVettingScheduler" runat="server" />
                                    
                                    </td>
                                        <td style="text-align: right; width:25%">
                                            Migrate From:
                                        </td>                      
                                    <td>&nbsp;</td>
                                    <td>
   
                                         <asp:TextBox ID="txtMigrateFormVetting" runat="server" onkeypress="return false;" ></asp:TextBox>                    
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd-MM-yyyy"  TargetControlID="txtMigrateFormVetting">
                                                </ajaxToolkit:CalendarExtender>
                                      </td>
                                    </tr>
                                    <tr>

                                    
                                  
                                   <td style="text-align: right; width:25%">
                                            Vetting Type:
                                        </td>
                                        <td style="color: #FF0000; " align="right" class="style1">
                                            *
                                        </td>
                                        <td style="text-align: left; width:24%">
                                            <asp:ListBox ID="ddlVettingType" runat="server" Width="150px" SelectionMode="Multiple"
                                              ></asp:ListBox>
                             
                                        </td>
                                    
                                    </tr>


                                </table>
                            </td>



                        </tr>
                    </table>
                
                
                </div>


                   <div id="dvObservation" style="background-color: #efefef;width:90%;" class="hide">
                
                
                             <table border="0" cellpadding="1" cellspacing="1" style="width: 100%;">
                        <tr>
                            <td valign="top" style="border: 1px solid #aabbdd;" align="left">
                                <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                    <tr style="background-color: #aabbdd">
                                        <td style="text-align: left;" colspan="6">
                                            <asp:Label ID="Label2" runat="server" Text="Vetting Observation Setting" Font-Bold="true"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td  style="text-align: right; width:25%">
                                     Activate Scheduler:
                                    
                                    </td>
                                    <td>
                                    &nbsp;
                                    </td>
                                    <td>
                                    <asp:CheckBox ID="chkActivateObservationScheduler" runat="server" />
                                    
                                    </td>
                                        <td style="text-align: right; width:25%">
                                            Migrate From:
                                        </td>                      
                                    <td>&nbsp;</td>
                                    <td>
   
                                         <asp:TextBox ID="txtMigrateFormObservation" runat="server" onkeypress="return false;" ></asp:TextBox>                    
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd-MM-yyyy"  TargetControlID="txtMigrateFormObservation">
                                                </ajaxToolkit:CalendarExtender>
                                      </td>
                                    </tr>
                                    <tr>

                                    
                                  
                                   <td style="text-align: right; width:25%">
                                            Vetting Type:
                                        </td>
                                        <td style="color: #FF0000; " align="right" class="style1">
                                            *
                                        </td>
                                        <td style="text-align: left; width:24%">
                                            <asp:ListBox ID="lstObservationVettingType" runat="server" Width="150px" SelectionMode="Multiple"
                                              ></asp:ListBox>
                             
                                        </td>


                                        <td style="text-align: right; width:25%">
                                            Observation Category:
                                        </td>
                                        <td style="color: #FF0000; " align="right" class="style1">
                                            *
                                        </td>
                                        <td style="text-align: left; width:24%">
                                            <asp:ListBox ID="lstObservationCategory" runat="server" Width="150px" SelectionMode="Multiple"
                                              ></asp:ListBox>
                             
                                        </td>

                                    
                                    </tr>

                                    <tr>

                                    
                                  
                                   <td style="text-align: right; width:25%">
                                            Observation Type:
                                        </td>
                                        <td style="color: #FF0000; " align="right" class="style1">
                                            *
                                        </td>
                                        <td style="text-align: left; width:24%">
                                            <asp:ListBox ID="lstObservationType" runat="server" Width="150px" SelectionMode="Multiple"
                                              >
                                                <asp:ListItem Text="-SELECT ALL-" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Observations" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Notes" Value="1"></asp:ListItem>
                                              
                                              </asp:ListBox>
                             
                                        </td>


                                        <td style="text-align: right; width:25%">
                                           Risk Level:
                                        </td>
                                        <td style="color: #FF0000; " align="right" class="style1">
                                          *
                                        </td>
                                        <td style="text-align: left; width:24%">
                                            <asp:ListBox ID="lstRiskLevel" runat="server" Width="150px" SelectionMode="Multiple"
                                              >
                                              
                                              <asp:ListItem Value="0" Text="-SELECT ALL-" ></asp:ListItem>
                                              <asp:ListItem Value="1" Text="1" ></asp:ListItem>
                                              <asp:ListItem Value="2" Text="2" ></asp:ListItem>
                                              <asp:ListItem Value="3" Text="3" ></asp:ListItem>
                                               <asp:ListItem Value="4" Text="4" ></asp:ListItem>
                                                <asp:ListItem Value="5" Text="5" ></asp:ListItem>
                                              </asp:ListBox>
                             
                                        </td>

                                    
                                    </tr>
                                </table>
                            </td>



                        </tr>
                    </table>
                
                
                </div>




                </td>
                </tr>

                <tr>
               
                    <td colspan="4" align="left" style="color: #FF0000; font-size: small;">
                        * Mandatory fields &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                <td colspan="4" align="center" style="color: #FF0000; font-size: small;">
                        <asp:Label ID="lbl1" runat="server" Text=""></asp:Label>
                        <%--<asp:ValidationSummary ID="vsSubmitt" runat="server" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" ValidationGroup="vgSubmitt" />--%>
                    </td>
                   
                </tr>

            </table>
                    <div style="background-color: #d8d8d8; text-align: center">
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnsave" runat="server"   causesValidation="true"  OnClientClick="return ValidateSetting();"
                            Text="Save" OnClick="btnsave_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Label ID="lblMessage" Style="color: #FF0000;" runat="server"></asp:Label>
        </div>


        </center>
    </div>


    </form>
</body>
</html>
