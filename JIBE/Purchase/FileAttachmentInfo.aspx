<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FileAttachmentInfo.aspx.cs"
    Inherits="Technical_INV_FileAttachmentInfo" Title="Attached File Information" %>

<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="headcontent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="../Styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">


        function DocOpen(docpath) 
        {
            window.open(docpath);
        }

        function previewDocument(docPath) {
            document.getElementById("ifrmDocPreview").src = docPath;
        }
        function ValidationOnRetrieve() {
            //         debugger; 
            var cmdFleet = document.getElementById("ctl00_MainContent_DDLFleet").value;
            var cmdVessels = document.getElementById("ctl00_MainContent_DDLVessel").value;

            if (cmdFleet == "0" || cmdFleet == null) {
                alert("Select fleet, vessel and click Retrieve button.");
                return false;
            }

            if (cmdVessels == "ALL" || cmdVessels == null) {
                alert("Select vessel and click Retrieve button.");
                return false;
            }
            return true
        }

        function getImageopen(str) {
            window.open(str, "file", "menubar=0,resizable=0,width=750,height=550,resizeable=yes");
        }


        function OnAddAttachment() {
            var cmdFleet = document.getElementById("ctl00_MainContent_DDLFleet").value;
            var cmdVessels = document.getElementById("ctl00_MainContent_DDLVessel").value;



            if (cmdFleet == "0" || cmdFleet == null) {
                alert("Select fleet, vessel and click Attach button.");
                return false;
            }

            if (cmdVessels == "ALL" || cmdVessels == null) {
                alert("Select vessel and click Attach button.");
                return false;
            }


            return true;
        }

        function fn_OnClose() {
            $('[id$=btnLoadFiles]').trigger('click');
            //__doPostBack('ctl00_MainContent_btnLoadFiles', true);            
        }
    </script>
    <style type="text/css">
        .tdh
        {
            text-align: right;
            padding: 1px 3px 1px 3px;
            width: 250px;
        }
        .tdd
        {
            text-align: left;
            padding: 1px 3px 1px 3px;
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Attached File Information
    </div>
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
    <asp:UpdatePanel ID="updGridAttach" runat="server">
        <ContentTemplate>
            <div style="color: Black">
                <div id="dvpage-content" class="page-content-main" style="padding: 10px">
                    <div style="padding-top: 2px; padding-bottom: 2px; width: 100%">
                        <table cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td style="width: 75%; text-align: left;">
                                    <table cellpadding="0" cellspacing="0" style="font-size: 12px">
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="tdh">
                                                Fleet :
                                            </td>
                                            <td class="tdd">
                                                <asp:DropDownList ID="DDLFleet" runat="server" Width="154px" AppendDataBoundItems="True"
                                                    Font-Size="12px" AutoPostBack="True" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">ALL</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                            <td class="tdd">
                                                <asp:Button ID="btnRetrieve" runat="server" OnClick="btnRetrieve_Click" Text="Search"
                                                    Width="100px" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="tdh">
                                                Vessel :
                                            </td>
                                            <td class="tdd">
                                                <asp:DropDownList ID="DDLVessel" runat="server" Width="154px" Font-Size="12px" AppendDataBoundItems="True"
                                                    OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="tdh">
                                                Category
                                            </td>
                                            <td class="tdd">
                                                <asp:DropDownList ID="DDLCategory" runat="server" Font-Size="12px" Width="150px"
                                                    AppendDataBoundItems="True">
                                                    <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td class="tdd">
                                                <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Clear Filters"
                                                    Width="100px" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td class="tdh">
                                                Reqsn No :
                                            </td>
                                            <td class="tdd">
                                                <asp:TextBox ID="txtRequisition" runat="server" Width="150px"></asp:TextBox>
                                            </td>
                                            <td class="tdh" style="width: 250px">
                                                Supplier :
                                            </td>
                                            <td class="tdd">
                                                <asp:DropDownList ID="DDLSupplier" runat="server" AppendDataBoundItems="true" Font-Size="12px"
                                                    Width="150px">
                                                    <asp:ListItem Text="--SELECT ALL--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td class="tdd">
                                                <asp:Button ID="btnBack" runat="server" OnClientClick="javascript:window.close();"
                                                    Text="Close" Width="100px" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%;">
                                    <div style="height: 80px; overflow: auto">
                                        <img src="../Images/AddAttachment.png" onclick="showModal('dvPopupAddAttachment',true,fn_OnClose);" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="2" cellspacing="0" style="width: 100%; border: 1px solid #cccccc;
                            margin-top: 2px; height: 500px; vertical-align: top">
                            <tr>
                                <td style="width: 60%; text-align: left; vertical-align: top;">
                                    <asp:GridView ID="rgdFileInfo" runat="server" AutoGenerateColumns="False" EmptyDataText="NO RECORDS FOUND"
                                        PagerStyle-Mode="NextPrevAndNumeric" OnDataBound="rgdFileInfo_DataBound" OnItemDataBound="rgdFileInfo_ItemDataBound"
                                        Width="100%" GridLines="Both">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <%--  <RowStyle Wrap="False" />--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSelect" Text="Select" runat="server" CommandName="Select"
                                                        Font-Bold="true" CommandArgument='<%#Eval("Requisition_Code")+","+Eval("Vessel_Code") %>'
                                                        OnClick="lbtnSelect_Click"> </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reqn_Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReq" runat="server" Text='<%#Eval("Requisition_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attached Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Assigned to Supplier">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%; border-bottom-color: transparent">
                                                        <tr>
                                                            <td align="center" style="border: 0px solid transparent">
                                                                <asp:Label ID="lblSupp" Text='<%#Eval("SuppCount") %>' Visible="true" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="center" style="border: 0px solid transparent">
                                                                <asp:ImageButton ID="imgbtnAssignedToSupp" Height="12px" Width="12px" runat="server"
                                                                    AlternateText="send to supp" CommandArgument='<%#Eval("File_Name")%>' ImageUrl="~/Images/add.GIF"
                                                                    OnClick="imgbtnAssignedToSupp_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="60px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File_Name">
                                                <ItemTemplate>
                                                <%--<asp:HyperLink ID="lnkAtt" Target="_blank" runat="server" NavigateUrl='<%#"../Uploads/Purchase/"+System.IO.Path.GetFileName(Convert.ToString(Eval("File_Path")))%>'> <%# Eval("File_Name")%>  </asp:HyperLink>--%>
                                                    <asp:LinkButton ID="lbtnPreview" runat="server" Width="148px" OnClick="lbtnPreview_Click"
                                                        ToolTip="Preview" Text='<%#Eval("File_Name")%>' 
                                                        CommandArgument='<%#Eval("File_Path")%>'> </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="350px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" width="100%" style="border:none">
                                                        <tr>
                                                            <td>
                                                                <img style="width: 12px; height: 12px" alt="Open in new window" onclick="DocOpen('<%#"../Uploads/Purchase/"+System.IO.Path.GetFileName(Convert.ToString(Eval("File_Path")))%>')"
                                                                    src="Image/DownLoad.gif" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="imgbtnDelete" ImageUrl="~/Purchase/Image/Close.gif" runat="server"
                                                                    OnClick="imgbtnDelete_Click" Visible="false" CommandArgument='<%#Eval("id")+","+Eval("File_Path") %>' />
                                                            </td>
                                                            <td align="center">
                                                                <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                    Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;PURC_DTL_FILE_ATTACH&#39;,&#39; id="+Eval("id")+" and Vessel_Code="+Eval("Vessel_Code")+"&#39;,event,this)" %>'
                                                                    AlternateText="info" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center"/>
                                            </asp:TemplateField>

                                        
                                        </Columns>
                                    </asp:GridView>
                                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="10" OnBindDataItem="BindAttchmentInfo" />
                                    <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                </td>
                                <td style="width: 40%; height: 100%; vertical-align: top; overflow: hidden; border: 1px solid #cccccc">
                                    <iframe id="ifrmDocPreview" width="99%" height="100%" marginheight="0px" src="../Images/previewAttach.png"
                                        style="vertical-align: middle; text-align: center"></iframe>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="updsupp" runat="server">
        <ContentTemplate>
            <div id="dvAssigneToSupp" style="position: fixed; height: 300px; width: 400px; top: 35%;
                text-align: center; left: 35%" runat="server" class="popup-css">
                <table style="text-align: left">
                    <tr>
                        <td>
                            File Name:
                        </td>
                        <td>
                            <asp:Label ID="lblFineName" Width="300px" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Date Attached:
                        </td>
                        <td>
                            <asp:Label ID="lbldate" Width="150px" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Reqsn Code:
                        </td>
                        <td>
                            <asp:Label ID="lblReqsn" Width="150px" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="text-align: center">
                    <tr>
                        <td style="text-align: center">
                            <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="false">
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIsSent" Checked='<%#Eval("IsAssigned") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSuppName" Text='<%#Eval("Full_NAME") %>' Width="300px" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSuppcode" Text='<%#Eval("QUOTATION_SUPPLIER") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #FF3300; font-size: small;"
        Width="624px" Height="16px"></asp:Label>
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
                    <tr>
                        <td style="text-align: left">
                            <asp:Button ID="btnLoadFiles" OnClick="btnLoadFiles_Click" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
