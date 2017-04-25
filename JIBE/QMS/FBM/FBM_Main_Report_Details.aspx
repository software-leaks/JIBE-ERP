<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FBM_Main_Report_Details.aspx.cs"
    Inherits="QMS_FBM_FBM_Main_Report_Details" Title="Fleet Broadcast Message Details"
    EnableEventValidation="false" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Purc_Functions_Common.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <link href="../../Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.alerts.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>

    <style type="text/css">
        .ajax__htmleditor_editor_bottomtoolbar
        {
            display: none;
        }
        
        .cke_show_borders body
        {
            background: #FFFFCC;
            color: black;
        }
      
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }    
   
    </style>
    <script language="javascript" type="text/javascript">



        function ValidationOnMakeActive(id) {

            // $.alerts.okButton = " Yes ";
            // $.alerts.cancelButton = " No ";

            var fbmNumber = document.getElementById("ctl00_MainContent_txtFBMNumber").value;
            var caption = document.getElementById("ctl00_MainContent_btnInActive").value;
            var Action = "";

            if (caption == "Make InActive")
                Action = "InActive";
            else
                Action = "Active";


            var strMsg = "FBM No - <b>" + fbmNumber + "</b> will be " + Action + "\n\n"
                          + "Are you sure want to continue ?";

            var aa = jConfirm(strMsg, ' Confirmation Required !', function (r) {

                if (r) {

                    var postBackstr = "__doPostBack('" + id.replace(/_/gi, '$') + "','" + id.replace(/_/gi, '$') + "_Click')";
                    window.setTimeout(postBackstr, 0, 'JavaScript');
                    return true;


                }
                else {
                    return false;
                }
            }

            );

            return false;
        }



        function DocOpen(filename) {

            var filepath = "../../uploads/fbm/";
            //alert(filepath + filename);
            window.open(filepath + filename);
        }
        function ValidationOnSave() {


            var Department = document.getElementById("ctl00_MainContent_DDLOfficeDept").value;
            var PrimaryCategory = document.getElementById("ctl00_MainContent_DDLPrimaryCategory").value;
            var SecondryCategory = document.getElementById("ctl00_MainContent_DDLSecondryCategory").value;
            var Subject = document.getElementById("ctl00_MainContent_txtSubject").value;
            var eMailBody = document.getElementById("ctl00_MainContent_txtMailBody").value;


            if ((document.getElementById("ctl00_MainContent_optForUser_0").checked == false) && (document.getElementById("ctl00_MainContent_optForUser_1").checked == false) && (document.getElementById("ctl00_MainContent_optForUser_2").checked == false)) {
                alert('Please Select the For user option.');
                return false;
            }

            if (Department == "0") {
                alert('Department is required.')
                return false;
            }

            if (PrimaryCategory == "0") {
                alert('Primary Category is required.')
                return false;
            }


            if (SecondryCategory == "0") {
                alert('Secondry Category is required.')
                return false;
            }

            if (Subject == "") {
                alert('Subject is required.')
                return false;
            }

            return true;
        }

        function CloseDiv() {
            var control = document.getElementById("ctl00_MainContent_divSendApproval");
            alert('hi');
            control.style.visibility = "hidden";
        }

        function ValidationOnSendApproval() {

            var btnApproved = document.getElementById("ctl00_MainContent_ddlFbmApprover").value;
            if (btnApproved == "0") {
                alert('Pleas select the approver.')
                return false;
            }
        }




    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="border: 1px solid  #006699; font-family: Tahoma; font-size: 12px; color: Black;
            width: 100%; height: 860px">
            <div class="page-title">
                   Fleet Broadcast Message
             </div>
               <%--<div style="padding: 1px; background-color: #5588BB; color: #FFFFFF; text-align: center;">--%>
            <%--<table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 95%">
                        <b>
                            <asp:Label ID="lblTitle" Text="Fleet Broadcast Message" runat="server"></asp:Label>
                        </b>
                    </td>
                    <td style="width: 5%">
                        
                    </td>
                </tr>
            </table>--%>
         
           <%-- </div>--%>
             <div>
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div>
                        <table cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td align="right" style="width: 12%">
                                    Department : &nbsp;
                                </td>
                                <td align="left" style="width: 10%">
                                    <asp:DropDownList ID="DDLOfficeDept" runat="server" AppendDataBoundItems="True" Width="180px"
                                        Height="20px" Font-Size="11px">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 10%">
                                    Date Sent : &nbsp;
                                </td>
                                <td align="left" style="width: 6%">
                                    <asp:TextBox ID="txtDateSent" runat="server" Font-Size="11px" ReadOnly="true" Width="125px"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 40%">
                                    <asp:FileUpload ID="FBMFileUploader" Style="width: 70%; height: 18px; background-color: #F2F2F2;
                                        border: 1px solid #cccccc; font-size: 12px; cursor: pointer" runat="server" />&nbsp;<asp:Button
                                            ID="btnUpload" Text="Add Attachment" Width="100px" Height="20px" Style="height: 18px;
                                            border: 1px solid #cccccc; font-size: 12px; cursor: pointer" runat="server" OnClick="Upload_Click" />
                                </td>
                                <td style="width: 5%">
                                    <asp:Button ID="btnDraft" runat="server" Width="100px" Text="Save As Draft" OnClientClick="return ValidationOnSave();"
                                        OnClick="btnDraft_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Primary Category : &nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDLPrimaryCategory" runat="server" AppendDataBoundItems="True"
                                        BackColor="#FFFFCC" AutoPostBack="True" OnSelectedIndexChanged="DDLPrimaryCategory_SelectedIndexChanged"
                                        Width="180px" Height="20px" Font-Size="11px">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    FBM Number : &nbsp;
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFBMNumber" runat="server" Font-Size="11px" ReadOnly="true" Width="125px"></asp:TextBox>
                                </td>
                                <td align="left" rowspan="5">
                                    <div style="vertical-align: top">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:ListBox ID="lstfbmAttachment" Height="60px" Width="520px" runat="server" BackColor="#FFFFCC">
                                                    </asp:ListBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImgTempFBMAttDelete" runat="server" Height="14px" ForeColor="Black"
                                                        ImageUrl="~/Images/Delete.png" OnClick="ImgTempFBMAttDelete_click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="overflow-y: scroll;height:100px;">
                                        <asp:GridView ID="gvFBMAtt" runat="server" AutoGenerateColumns="False" Width="100%"
                                            GridLines="Both" OnRowCommand="gvFBMAtt_RowCommand" OnRowDataBound="gvFBMAtt_RowDataBound">
                                            <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                            <RowStyle CssClass="PMSGridRowStyle-css" />
                                            <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="false" Font-Size="12px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Attachments">
                                                    <HeaderTemplate>
                                                        Attached File(s)
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblfileName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILENAME") %>'></asp:Label>
                                                        <asp:Label ID="lblfilePath" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILEPATH") %>'></asp:Label>
                                                        <asp:Label ID="lblFileId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <img style="border: 0; width: 14px; height: 14px" alt="Open in new window" onclick="DocOpen('<%#Eval("FILEPATH")%>')"
                                                            src="../../Purchase/Image/DownLoad.gif" title="Click to View file" />
                                                        <asp:ImageButton ID="ImgFBMAttDelete" OnCommand="ImgFBMAttDelete_Click" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") + "," + DataBinder.Eval(Container,"DataItem.FILEPATH")  %>'
                                                            runat="server" Height="14px" ForeColor="Black" ImageUrl="~/Images/Delete.png">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                                <td>
                                    <asp:Button ID="btnSent" runat="server" Width="100px" Text="Send FBM" OnClientClick="return ValidationOnSave();"
                                        OnClick="btnSent_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Secondry Category : &nbsp;
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DDLSecondryCategory" runat="server" AppendDataBoundItems="True"
                                        BackColor="#FFFFCC" Width="180px" Height="20px" Font-Size="11px">
                                        <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnInActive" runat="server" Width="100px" Text="Make InActive" OnClientClick="return ValidationOnMakeActive(id);"
                                        OnClick="btnInActive_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    For User : &nbsp;
                                </td>
                                <td align="left">
                                    <div style="border: 1px solid Gray; width: 180px; background-color: #FFFFCC">
                                        <asp:RadioButtonList ID="optForUser" runat="server" Font-Size="11px" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Company" Value="COMPANY"></asp:ListItem>
                                            <asp:ListItem Text="Office" Value="OFFICE"></asp:ListItem>
                                            <asp:ListItem Text="Ships" Value="SHIP"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </td>
                                <td align="right">
                                    Priority : &nbsp;
                                </td>
                                <td align="left">
                                    <div style="border: 1px solid Gray; width: 120px; background-color: #FFFFCC">
                                        <asp:RadioButtonList ID="optPriority" runat="server" Font-Size="11px" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Normal" Selected="True" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Urgent" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </td>
                                <td>
                                    <asp:Button ID="btnApproved" runat="server" Text="Approve" Width="100px" OnClick="btnApproved_Click" />
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td align="left" colspan="3">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnRework" runat="server" Text="Rework" Width="100px" OnClick="btnRework_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Subject : &nbsp;
                                </td>
                                <td align="left" colspan="3">
                                    <asp:TextBox ID="txtSubject" runat="server" MaxLength="500" Width="400px" BackColor="#FFFFCC"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td align="left" colspan="5">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divSendApproval" runat="server" style="border: 1px solid gray; background-color: #E0E0E0;
                        font-family: Tahoma; position: absolute; left: 40%; top: 30%; color: black; height: 100px;
                        width: 300px;" class="popup-css">
                        <center>
                            <table cellpadding="2" cellspacing="3">
                                <tr>
                                    <td colspan="2">
                                        Send FBM for Approval
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Approval
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFbmApprover" runat="server" AppendDataBoundItems="True"
                                            Width="180px" Height="20px" Font-Size="11px">
                                            <asp:ListItem Selected="True" Value="0">-- SELECT --</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnDivSendApproval" runat="server" Text="Send For Approval" OnClientClick="return ValidationOnSendApproval();"
                                            OnClick="btnDivSendApproval_click" />
                                        &nbsp;&nbsp
                                        <asp:Button ID="btnDivCancel" runat="server" Text="Cancel" OnClick="btnDivCancel_click" />
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpload" />
                </Triggers>
            </asp:UpdatePanel>
            <div>
                <table cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td align="right" valign="baseline" style="width: 10%">
                            Body : &nbsp;
                        </td>
                        <td align="right" colspan="5" style="background-color: #FFFFCC">
                            <CKEditor:CKEditorControl ID="txtMailBody" CssClass="cke_show_borders" runat="server"></CKEditor:CKEditorControl>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </center>
</asp:Content>
