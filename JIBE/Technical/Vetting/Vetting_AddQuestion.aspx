<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="Vetting_AddQuestion.aspx.cs" Inherits="Technical_Vetting_Vetting_AddQuestion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />   
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script> 
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/jscript"></script>    
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <style type="text/css">
    body {
  
   
    font-family: Tahoma;
    font-size: 12px;
    margin: 0;
    padding: 0;
}
    </style>
    <script language="javascript" type="text/javascript">

       
        function OnLevel2TextChange(L2) {
            var Level2 = document.getElementById($('[id$=txtLevl2]').attr('id')).value.trim();
            if (Level2 != "") {
                document.getElementById($('[id$=txtLevl3]').attr('id')).disabled = false;

            }
            else {
                document.getElementById($('[id$=txtLevl3]').attr('id')).disabled = true;
               document.getElementById($('[id$=txtLevl4]').attr('id')).disabled = true;
                document.getElementById($('[id$=txtLevl3]').attr('id')).value = "";
                document.getElementById($('[id$=txtLevl4]').attr('id')).value = "";

            }
        }
        function OnLevel3TextChange(L3) {
            var Level3 = document.getElementById($('[id$=txtLevl3]').attr('id')).value.trim();
            if (Level3 != "") {
                document.getElementById($('[id$=txtLevl4]').attr('id')).disabled = false;
               

               
            }
            else {
                document.getElementById($('[id$=txtLevl4]').attr('id')).disabled = true;
                document.getElementById($('[id$=txtLevl4]').attr('id')).value = "";

            }
        }

        function jsfun_tbSearchName_onchange() {
            var Level3 = document.getElementById($('[id$=txtLevl3]').attr('id')).value.trim();
            if (Level3 != "") {
                document.getElementById($('[id$=txtLevl4]').attr('id')).disabled = false;
                


            }
            else {
                document.getElementById($('[id$=txtLevl4]').attr('id')).disabled = true;
                document.getElementById($('[id$=txtLevl4]').attr('id')).value = "";

            }
            return false;
        }
        function OnLoadEnable() {
         var Level2 = document.getElementById($('[id$=txtLevl2]').attr('id')).value.trim();
         var Level3 = document.getElementById($('[id$=txtLevl3]').attr('id')).value.trim();
         if (Level2 != "") {
             document.getElementById($('[id$=txtLevl3]').attr('id')).disabled = false;

         }
         else {
             document.getElementById($('[id$=txtLevl3]').attr('id')).disabled = true;
             document.getElementById($('[id$=txtLevl4]').attr('id')).disabled = true;
         }
         if (Level3 != "") {
             document.getElementById($('[id$=txtLevl4]').attr('id')).disabled = false;



         }
         else {
             document.getElementById($('[id$=txtLevl4]').attr('id')).disabled = true;
             document.getElementById($('[id$=txtLevl4]').attr('id')).value = "";

         }
         return false;

        }
       

        function onSaveQuestion() {
            var Section = document.getElementById($('[id$=txtSection]').attr('id')).value.trim();
            var Question = document.getElementById($('[id$=txtQuestion]').attr('id')).value.trim();
            var Level2 = document.getElementById($('[id$=txtLevl2]').attr('id')).value.trim();
            
            if (Section == "") {
                alert("Section is required.");
                document.getElementById($('[id$=txtSection]').attr('id')).focus();
                return false;
            }
            if (Level2 == "") {
                alert("Level 2 is required.");
                document.getElementById($('[id$=txtLevl2]').attr('id')).focus();
                return false;
            }
            if (isNaN($.trim($("#" + $('[id$=txtSection]').attr('id')).val()))) {
                alert("Invalid Section Number");
                $("#" + $('[id$=txtSection]').attr('id')).focus();
                return false;
            }
            if (isNaN($.trim($("#" + $('[id$=txtLevl2]').attr('id')).val()))) {
                alert("Invalid Level 2");
                $("#" + $('[id$=txtLevl2]').attr('id')).focus();
                return false;
            }
            if (isNaN($.trim($("#" + $('[id$=txtLevl3]').attr('id')).val()))) {
                alert("Invalid Level 3");
                $("#" + $('[id$=txtLevl3]').attr('id')).focus();
                return false;
            }
            if (isNaN($.trim($("#" + $('[id$=txtLevl4]').attr('id')).val()))) {
                alert("Invalid Level 4");
                $("#" + $('[id$=txtLevl4]').attr('id')).focus();
                return false;
            }

            if (Question == "") {
                alert("Question is required.");
                document.getElementById($('[id$=txtQuestion]').attr('id')).focus();
                return false;
            }

        }
     
    </script>
 </head>
<body onload="return OnLoadEnable();">
<form id="form1" runat="server">
<div>
   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
       
        <asp:UpdatePanel ID="UpdPnlAddSectionAndQuestion" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <div style="padding-top: 2px; padding-bottom: 2px; "  id="div1">
                <table width="500px" cellpadding="4" cellspacing="2">
               
                    <tr>
                        <td align="left" >
                            <asp:Label ID="lblType" runat="server" Text=" Type :" Font-Bold="true"></asp:Label>  
                        </td>
                         <td colspan="2">                           
                        </td>
                        <td align="left" >
                            <asp:Label ID="lblQuestionText" runat="server" Text="Question"   Width="150px" ></asp:Label>  
                        </td>
                    </tr>
                    <tr>
                        <td align="left" >
                            <asp:Label ID="lblSection" runat="server" Text=" Section No :"  width="75px" Font-Bold="true"></asp:Label>  
                        </td>
                        <td colspan="2">
                         <asp:Label ID="lblSectionMandatry" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td align="left" >
                              <asp:TextBox ID="txtSection" runat="server"  Width="150px"  Font-Size="12px" ></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td align="left" >
                            <asp:Label ID="lblLevel2" runat="server" Text="Level 2 :"  Font-Bold="true"></asp:Label>  
                        </td>
                        <td colspan="2">
                        <asp:Label ID="lblLevel2Mandatry" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>                                               
                        <td align="left" >
                               <asp:TextBox ID="txtLevl2" runat="server"   Width="150px" 
                                   oninput="OnLevel2TextChange(this)"  Font-Size="12px" ></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td align="left" >
                            <asp:Label ID="lblLevel3" runat="server" Text="Level 3 :" Font-Bold="true"></asp:Label>     
                        </td>
                         <td colspan="2">                           
                        </td>
                        <td align="left" >
                           <asp:TextBox ID="txtLevl3" runat="server"    Width="150px"
                                oninput="OnLevel3TextChange(this);"  Font-Size="12px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" >
                            <asp:Label ID="lblLevel4" runat="server" Text="Level 4 :" Font-Bold="true"></asp:Label>  
                        </td>
                         <td colspan="2">                           
                        </td>
                        <td align="left"  >
                            <asp:TextBox ID="txtLevl4" runat="server"   Width="150px" 
                                 Font-Size="12px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td align="left"  style="padding-bottom:62px">
                            <asp:Label ID="lblQuestion" runat="server" Text="Question :" Font-Bold="true"></asp:Label> 
                        </td>
                        <td colspan="2"  style="padding-bottom:62px">
                        <asp:Label ID="lblQuestionMandatry" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtQuestion" runat="server" TextMode="MultiLine" 
                                Width="300px" Height="70px" Font-Size="12px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left"  style="padding-bottom:62px">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks :" Font-Bold="true"></asp:Label>   
                        </td>
                         <td colspan="2">                           
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" 
                                  Width="300px" Height="70px" Font-Size="12px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" style="margin-top:7px">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="70px" 
                                onclick="btnCancel_Click"  Height="25px"/>
                        </td>
                        <td align="right" colspan="2" >
                            <asp:Button ID="btnSaveAndClose" runat="server" Text="Save and Close" 
                                Width="120px" onclick="btnSaveAndClose_Click" 
                                OnClientClick="return onSaveQuestion();"  Height="25px"/>
                       
                            <asp:Button ID="btnSaveAndAdd" runat="server" Text="Save and add questions" 
                                Width="160px" onclick="btnSaveAndAdd_Click"  
                                OnClientClick="return onSaveQuestion();"  Height="25px"/>
                        </td>
                        </tr>
                </table>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
               
                </div>
                    </form>
</body>
</html>
