<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_Training.aspx.cs"
    Inherits="Crew_CrewDetails_Training" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="jQUpload/Scripts/jquery-1.2.6.min.js"></script>
    <script type="text/javascript" src="jQUpload/Scripts/jquery.ajax_upload.0.6.min.js"></script>
    <script src="/smslog/Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">/*<![CDATA[*/
		$(document).ready(function(){
			/* example 1 */
			var button = $('#button1'), interval;
			$.ajax_upload(button,{
				action: 'jQUpload/FileHandler.ashx',
				name: 'myfile',
				onSubmit : function(file, ext){
					// change button text, when user selects file			
					button.text('Uploading');
			
					// If you want to allow uploading only 1 file at time,
					// you can disable upload button
					this.disable();
			
					// Uploding -> Uploading. -> Uploading...
					interval = window.setInterval(function(){
						var text = button.text();
						if (button.text().length < 13){
							button.text(button.text() + '.');					
						} else {
							button.text('Uploading');				
						}
					}, 200);
				},
				onComplete: function(file, response){
					button.text('Select File');
					// Although plugins emulates hover effect automatically,
					// it doens't work when button is disabled
					button.removeClass('hover');			
					window.clearInterval(interval);
											
					// enable upload button
					this.enable();			
					if(response.indexOf('error') < 0)
					{
						// add file to the list//
						$('<li></li>').appendTo('#example1 .files').text(file);
						response = response.replace('<pre>','');
						response = response.replace('</pre>','');
						response = response.replace('<PRE>','');
						response = response.replace('</PRE>','');
						$('[id$=HiddenField_SelectedFiles]').val($('[id$=HiddenField_SelectedFiles]').val() + ',' + response);
					}
				}
			});
	
	
		});/*]]>*/
    </script>
    <script type="text/javascript">

        //-- attachments---
        function uploadComplete() { }

        var lastExecutor_WebServiceProxy = null;
		   
    </script>
    <style type="text/css">
        h1
        {
            color: #C7D92C;
            font-size: 18px;
            font-weight: 400;
        }
        a
        {
            color: white;
        }
        #text
        {
            margin: 25px;
        }
        ul
        {
            list-style: none;
        }
        
        .example
        {
            padding: 0 0px;
            float: left;
            width: 230px;
            height: 200px;
        }
        
        .wrapper
        {
            width: 133px; /* Centering button will not work, so we need to use additional div */
            margin: 0 0;
        }
        
        div.button
        {
            height: 19px;
            width: 133px;
            background: url(jqupload/button.png) 0 0;
            font-size: 14px;
            color: #C7D92C;
            text-align: center;
            padding-top: 5px;
        }
        /* 
We can't use ":hover" preudo-class because we have
invisible file input above, so we have to simulate
hover effect with javascript. 
 */
        div.button.hover
        {
            background: url(jqupload/button.png) 0 56px;
            color: #95A226;
        }
        .files
        {
            color: Black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="HiddenField_CrewID" runat="server" />
    <asp:HiddenField ID="HiddenField_SelectedFiles" runat="server" />
    <div id="dvTraining">
        <asp:Panel ID="pnlFragmentTool" runat="server" Visible="false">
            <div id="fragment-14-tool" style="text-align: right;">
                <table style="width: 100%;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                        </td>
                        <td style="width: 120px; text-align: right;">
                            <asp:ImageButton runat="server" ID="ImgAddTraining" ImageUrl="~/Images/Add-Icon.png"
                                Visible="false" OnClientClick="AndCrewTraining($('[id$=HiddenField_CrewID]').val());return false;" />
                        </td>
                        <td style="width: 20px; text-align: right;">
                            <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Images/reload.png"
                                OnClientClick="GetCrewTrainingLog($('[id$=HiddenField_CrewID]').val());return false;" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlViewTrainings" runat="server" Visible="false">
            <asp:GridView ID="GridView_Trainings" runat="server" AllowSorting="False" AutoGenerateColumns="false"
                GridLines="None" CellPadding="3" CellSpacing="1" Width="100%" CssClass="GridView-css"
                OnRowDataBound="GridView_Trainings_OnRowDataBound">
                <Columns>
                    <asp:BoundField DataField="StartDate" HeaderText="Training Start" ItemStyle-Width="150px" />
                    <asp:BoundField DataField="EndDate" HeaderText="Training End" ItemStyle-Width="150px" />
                    <asp:BoundField DataField="PROGRAM_NAME" HeaderText="Program" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="CHAPTER_DESCRIPTION" HeaderText="Chapter" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="TrainerName" HeaderText="Trainer" ItemStyle-Width="150px" />
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                    <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name" />
                    <asp:TemplateField HeaderText="Attachments" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/attach-icon.png"
                                Visible='<%#Eval("Attach_Count").ToString() =="0"?false:true%>' AlternateText='<%#Eval("Attach_FileName")%>'
                                OnClientClick='<%#"OpenAttachmentWindow(" + Eval("CrewID").ToString()+ "," + Eval("ID").ToString() +"); return false;" %>'>
                            </asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"
                        Visible="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="lnkEdit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                Visible="false" AlternateText="Edit" OnClientClick='<%#"EditTraining(" + Eval("CrewID").ToString()+ "," + Eval("ID").ToString() +"); return false;" %>'>
                            </asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="HeaderStyle-css" />
                <PagerStyle CssClass="PagerStyle-css" />
                <RowStyle CssClass="RowStyle-css" />
                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="pnlEditTraining" runat="server" Visible="false">
            <table style="width: 100%">
                <tr>
                    <td>
                        Date of Training
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateofTraining" runat="server" Text='' Width="150px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDateofTraining">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Training Type
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTrainingType" runat="server" Width="156px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Trainer
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTrainer" runat="server" Width="156px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Result
                    </td>
                    <td>
                        <asp:TextBox ID="txtResult" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Remarks
                    </td>
                    <td>
                        <asp:TextBox ID="txtRemarks" runat="server" Text='' Width="300px" TextMode="MultiLine"
                            Height="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">
                        Attachments
                    </td>
                    <td style="text-align: left">
                        <div id="example1" class="example">
                            <div class="wrapper">
                                <div id="button1" class="button">
                                    Select File</div>
                            </div>
                            <ol class="files" style="color: Blue">
                                <asp:Repeater ID="rptAttachmentse" runat="server">
                                    <ItemTemplate>
                                        <li><a href='/smslog/uploads/crewdocuments/<%#Eval("AttachmentName") %>' target="_blank"
                                            style="color: Blue">
                                            <%#Eval("DisplayName") %></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ol>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btnSaveAndClose" runat="server" Text="Save and Close" OnClick="btnSaveAndClose_Click" />
                        <asp:Button ID="btnSaveAndAdd" runat="server" Text="Save and Add New" OnClick="btnSaveAndAdd_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="parent.hideModal('dvPopupFrame')" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlAtt" runat="server" Visible="false">
            <ol class="files" style="color: Blue; height: 100%">
                <asp:Repeater ID="rptAttachments" runat="server">
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink ID="hyplAttachment" NavigateUrl='<%# Eval("AttachmentName","../uploads/crewdocuments/{0}") %>'
                                Text='<%#Eval("DisplayName") %>' runat="server" Target="_blank" Style="color: Blue"></asp:HyperLink></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ol>
        </asp:Panel>
    </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", "#<%=btnSaveAndClose.ClientID %>,#<%=btnSaveAndAdd.ClientID %>", function () {
                if ($.trim($("#<%=txtDateofTraining.ClientID %>").val()) == "") {
                    alert("Enter Date of Training");
                    $("#<%=txtDateofTraining.ClientID %>").focus();
                    return false;
                }
                if (IsInvalidDate($("#<%=txtDateofTraining.ClientID %>").val(), '<%=UDFLib.GetDateFormat() %>')) {
                    alert("Enter valid Date of Training<%=UDFLib.DateFormatMessage() %>");
                    $("#<%=txtDateofTraining.ClientID %>").focus();
                    return false;
                }
            });
        });
    </script>
</body>
</html>
