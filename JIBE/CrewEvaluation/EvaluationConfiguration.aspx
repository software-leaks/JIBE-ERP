 
<%@ Page Title="Evaluation Configuration" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="EvaluationConfiguration.aspx.cs" Inherits="CrewEvaluation_EvaluationConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    	<script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
 
	<script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
	  <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript">
      function LoadAfterCheckBox() {

          $('#<%=chkLstSelectRank.ClientID%>').each(function () {

              $(this).find('input[type=checkbox]:first').addClass("SelectAll");

              $('.SelectAll').click(function () {
                  // If Checked
                  if (this.checked) {
                      $(this).closest('table').find('input[type=checkbox]').prop('checked', true);
                  }
                  // If Unchecked
                  else {
                      $(this).closest('table').find('input[type=checkbox]').prop('checked', false);
                  }
              });
          });

          $('#<%=chkVerifiersRank.ClientID%>').each(function () {

              $(this).find('input[type=checkbox]:first').addClass("SelectAll");

              $('.SelectAll').click(function () {
                  // If Checked
                  if (this.checked) {
                      $(this).closest('table').find('input[type=checkbox]').prop('checked', true);
                  }
                  // If Unchecked
                  else {
                      $(this).closest('table').find('input[type=checkbox]').prop('checked', false);
                  }
              });
          });
          
      }


    
      function alertmsg(indx) {
          if (indx == 1) {
              alert("Rank Configuration has been updated successfully!");
          }
      }
  </script>
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">
        <asp:UpdatePanel ID="UpdatePanel_Filter" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="upUpdateProgress" runat="server">
                    <ProgressTemplate>
                        <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                            color: black">
                            <img src="../Images/loaderbar.gif" alt="Please Wait" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>
          <div class="page-title">
            Rank To Rank Configuration
          </div>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <div id="Div1" style="margin: -3px 2px 2px 2px; border: 1px solid #cccccc; padding: 5px;">
                    <table border="0" cellpadding="0" cellspacing="5" width="100%">
                        <tr>
                            <td style="width: 20%;" class="gradiant-css-orange">
                            Configuration Type: <asp:RadioButton ID="rdbEval" Text="Evaluation" runat="server" 
                                    GroupName="Type" Checked="true" oncheckedchanged="rdbEval_CheckedChanged"  AutoPostBack="true"/>  <asp:RadioButton ID="rdbRest" Text="RestHours" runat="server" GroupName="Type" oncheckedchanged="rdbEval_CheckedChanged" AutoPostBack="true"/> 
                                    <asp:RadioButton ID="rdbHandover" Text="HandOver" runat="server" GroupName="Type" oncheckedchanged="rdbEval_CheckedChanged" AutoPostBack="true"/> 
                            </td>
                            <td style="width: 20%;" class="gradiant-css-orange">
                            </td>
                             <td style="width: 20%;" class="gradiant-css-orange">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight:bold;font-size:large" colspan="2">
                                                         <asp:Label id="lblTitle" runat="server" Text="configuration for showing evations to evaluator based on the crew rank"></asp:Label>
                            </td>
                              
                            <td>
                                <asp:Button ID="btnAssign" runat="server" Text="Save Evaluation Configurations" OnClick="btnAssign_Click" />
                            </td>
                        </tr>
                         <tr>
                            <td>
                              <%--  Search User:
                                <asp:TextBox ID="txtSearchUser" runat="server" AutoPostBack="true"></asp:TextBox>--%>
                            </td>
                            <td>
                                
                            </td>
                             <td>
                                
                            </td>
                        </tr>
                         <tr>
                            <td style="font-weight:bold;">
                             <asp:Label ID="col1" runat="server" Text="Evaluator Rank:"></asp:Label> 
                            </td>
                           
                            <td style="font-weight:bold;">
                             <asp:Label ID="col2" runat="server" Text=" Evaluation Rank:"></asp:Label> 
                             
                            </td>
                             <td style="font-weight:bold;"><asp:Label ID="col3" runat="server" Text=" Verifiable Ranks:" Visible="false"></asp:Label> </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:ListBox ID="lstParentRanks" runat="server" Height="600px" Width="300px"  
                                    AutoPostBack="true" DataTextField="Rank_Name" DataValueField="ID"  
                                    onselectedindexchanged="lstParentRanks_SelectedIndexChanged" >
                                </asp:ListBox>
                               
                            </td>
                            <td style="vertical-align: top;">
                            <div style="height:600px;width:300px;overflow-y:scroll;border-width:thin;border-color:Black; border-style:ridge;">
                            <asp:CheckBoxList ID="chkLstSelectRank" runat="server"   style="width:100%" DataTextField="Rank_Name" DataValueField="ID"> 
									</asp:CheckBoxList></td>
                                    <td style="vertical-align: top;">
                                    </div>
                            <div style="height:600px;width:300px;overflow-y:scroll;border-width:thin;border-color:Black; border-style:ridge;" runat="server" id="ver" visible="false" clientidmode="Static">
                            <asp:CheckBoxList ID="chkVerifiersRank" runat="server"   style="width:100%"
									  DataTextField="Rank_Name" DataValueField="ID" Visible="false"> 
									</asp:CheckBoxList></td>
                            </div>
             		
                           
                         
                              
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
