<%@ Page Title="Chapter Details" Language="C#" AutoEventWireup="true" CodeFile="LMS_Chapter_Details.aspx.cs" EnableEventValidation="false"
    Inherits="LMS_Chapter_Details" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="auc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <title></title>
    <style type="text/css">
        body
        {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            background-color: White;
        }
        
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 3px 3px 3px 0px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
            border-color: #C9C9CF;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 3px 2px 3px 3px;
            height: 20px;
            vertical-align: middle;
            border-color: #C9C9CF;
        }
        
        input
        {
            font-family: verdana;
        }
    </style>
    <script type="text/javascript">

//        window.onbeforeunload = confirmExit;
//        function confirmExit() {
//            parent.ReloadParent_ByButtonID();
//            return false;
//        }

        function fn_OnClose() {


        }


        function showDiv(dv) {
            if (dv) {
                $('#' + dv).show();
            }
        }
        function closeDiv(dv) {
            if (dv) {
                $('#' + dv).hide();
            }
        }


        function CheckIsNumeric(Duration) {
            if (isNaN(Duration.value)) {
                alert("Duration should be numeric.")
                Duration.value = '';
                return false;
            }
        }

        function Check_Duplicate_AttachmentFile(Attachment) {
            if (Attachment.value != '') {
                var retVal = confirm("Attached item already exist, Do you want to continue ?");
                if (retVal == false) {
                    window.open(Attachment);
                    return false;
                }

                else {
                    $(".csssaveitem").click();

                }

            }

        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptmngPO" runat="server">
    </asp:ScriptManager>
    <div class="page-title">
        <asp:Label ID="lblProgramName" runat="server" Font-Bold="true"></asp:Label>
    </div>
    <div class="page-content-main">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 40%; top: 30px; z-index: 2;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td class="tdh">
                            Chapter Name :
                        </td>
                        <td class="tdd">
                            <asp:TextBox ID="txtChapterName" Width="200px" MaxLength="200" ValidationGroup="savechapter" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFD1" ControlToValidate="txtChapterName" runat="server"
                                ValidationGroup="savechapter" ErrorMessage="Required Field!"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                        <td class="tdh" style="text-align: right">
                             <asp:Button ID="btnDelete" runat="server" target="_blank" Text="Delete" OnClick="btnDelete_Click"
                                 OnClientClick="return confirm('Are you sure want to delete this record?')"/> &nbsp;
                            <asp:Button ID="btnAddNewItem" runat="server" target="_blank" Text="Add New Item"
                                OnClick="btnNewItem_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdh">
                            Item Name :
                        </td>
                        <td class="tdd">
                        <table>
                        <tr>
                        <td>
                        <asp:TextBox ID="txtSearchItemName" runat="server"></asp:TextBox> 
                            
                        </td>
                        <td>
                          <asp:DropDownList ID="ddlItemType" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                         Width="100px" Height="20px"
                                        Font-Size="11px" BackColor="#FFFFCC">
                                    </asp:DropDownList>
                        </td>
                        <td>
                        <asp:Button ID="btnSearchItem" runat="server" OnClick="btnSearchItem_Click" Text="Search" /> 
                        </td>
                        
                        </tr>
                        </table>
                            
                        </td>
                       
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: top; width: 60%">
                            <asp:GridView ID="gvItemList" AutoGenerateColumns="false" runat="server" Width="100%"  
                                EmptyDataText="No record found" DataKeyNames="ID" CssClass="gridmain-css" CellPadding="4" 
                                CellSpacing="0" GridLines="None" 
                                 >
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                      <asp:Image ImageUrl='<%# "../Images/DocTree/"+(Eval("ITEM_TYPE").ToString()!="FBM"? (System.IO.File.Exists(Server.MapPath(@"~/Images/DocTree/"+System.IO.Path.GetExtension(Eval("ITEM_PATH").ToString()).Replace(".", "")+".png"))?(System.IO.Path.GetExtension(Eval("ITEM_PATH").ToString()).Replace(".", "")+".png"):"noneimg.png") : ("noneimg.png")) %>' ID="imgFile" runat="server"  />
                                            <asp:HyperLink ID="ITEM_NAME" runat="server" DataNavigateUrlFields="ITEM_NAME"
                                       NavigateUrl='<%#    Eval("ITEM_TYPE").ToString()!="FBM"?(System.IO.File.Exists(Server.MapPath("~/Uploads/TrainingItems/"+Eval("ITEM_PATH").ToString()))?"~/Uploads/TrainingItems/"+Eval("ITEM_PATH").ToString():"~/FileNotFound.aspx"):("../QMS/FBM/"+Eval("ITEM_PATH").ToString())    %>'
                                                Text='<%# Eval("ITEM_NAME") %>' Target="_blank"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ITEM_Description" HeaderText="Item Description" />
                                    <asp:BoundField DataField="ITEM_TYPE" HeaderText="Item Type" />
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Select
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelected" Checked='<%#Convert.ToBoolean(Eval("Selected"))%>' AutoPostBack="true"  
                                                runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <auc:CustomPager ID="ucCustomPagerChapterDetails" OnBindDataItem="BindChapterItem"
                                AlwaysGetRecordsCount="true" runat="server" />
                        </td>
                        <td style="vertical-align: top;">
                            <div style="width: 100%; overflow: auto; max-height: 500px">
                                <asp:GridView ID="gvTrainerRank" AutoGenerateColumns="false" runat="server" Width="100%"
                                    CssClass="gridmain-css" CellPadding="4" CellSpacing="0" GridLines="None" DataKeyNames="ID"
                                    Style="height: 20px; overflow: auto">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <Columns>
                                        <asp:BoundField DataField="Rank_Short_Name" HeaderText="Trainer Rank">
                                            <HeaderStyle Width="50%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                Select
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelected"  Checked='<%#Convert.ToBoolean(Eval("Selected"))%>'
                                                    runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" Width="80px" />
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnsaveandclose" runat="server" Text=" Save & Close  " Width="120px" ValidationGroup="savechapter"
                                OnClick="btnSaveandClose_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text=" Cancel " Width="120px" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="divMessage" align="center">
            <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
        </div>
        <div id="dvPopupAddAttachment" title="Add Attachments" style="display: none; width: 500px;">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: left">
                                <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                                <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                                    Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                    MaximumNumberOfFiles="10" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
    </div>
    </form>
</body>
</html>
