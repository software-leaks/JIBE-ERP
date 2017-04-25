<%@ Page Title="Oil Majors" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="OilMajors.aspx.cs" Inherits="Crew_Libraries_OilMajors" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <%--<script>
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Tab
        specialKeys.push(46); //Delete
        specialKeys.push(36); //Home
        specialKeys.push(35); //End
        specialKeys.push(37); //Left
        specialKeys.push(39); //Right
        function IsAlphaNumeric(e) {
            var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
            var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (keyCode == 32) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
            return ret;
        }
    </script>--%>
    <style type="text/css">
        .gridmain-css tr
        {
            height: 30px;
        }
        .gridmain-css tr:hover
        {
            background-color: #feecec;
        }
        #cke_show_borders p
        {
            margin: 8px 8px 8px 8px !important;
        }
        .page
        {
            width: 1000px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <center>
        <div style="font-family: Tahoma; font-size: 12px; width: 100%; height: 100%;">
            <div class="page-title">
                Oil Majors
            </div>
            <div style="min-height: 400px; width: 100%; color: Black;">
                <asp:UpdatePanel ID="UpdUserType" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                            <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFilter">
                                <table width="100%" cellpadding="4" cellspacing="4">
                                    <tr>
                                        <td align="right" style="width: 15%">
                                            Oil Major :&nbsp;
                                        </td>
                                        <td align="left" style="width: 30%">
                                            <asp:TextBox ID="txtfilter" runat="server" Width="100%"></asp:TextBox>
                                            <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                                WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                        </td>
                                        <td align="center" style="width: 1%">
                                            <asp:ImageButton ID="btnFilter" runat="server" TabIndex="0" OnClick="btnFilter_Click"
                                                ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                        </td>
                                        <td align="center" style="width: 1%">
                                            <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                                ImageUrl="~/Images/Refresh-icon.png" />
                                        </td>
                                        <td align="center" style="width: 1%">
                                            <asp:ImageButton ImageUrl="~/Images/Add-icon.png" ID="ImgAdd" runat="server" Style="cursor: pointer;"
                                                ToolTip="Add new oil major" ClientIDMode="Static" /><%--OnClick="ImgAddOnClick" --%>
                                        </td>
                                        <td style="width: 1%">
                                            <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                                ImageUrl="~/Images/Exptoexcel.png" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center; margin-bottom:20px;">
                            <div>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvOilMajors" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                            OnRowDataBound="gvOilMajors_RowDataBound" DataKeyNames="ID" CellPadding="0" CellSpacing="2" ShowHeaderWhenEmpty="true" 
                                            Width="100%" GridLines="None" OnSorting="gvOilMajors_Sorting" AllowSorting="true"
                                            CssClass="gridmain-css">
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                            <PagerStyle CssClass="PMSPagerStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Oil Major Name">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblReasonHeader" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                            CommandArgument="Oil_Major_Name" ForeColor="Black">Oil Major</asp:LinkButton>
                                                        
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOilMajorName" Text='<%#Eval("Oil_Major_Name")%>' rel='<%#Eval("ID").ToString() %>'
                                                            runat="server" Style="margin: 5px; color: Black;" />
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="20%" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Display Name">
                                                   <HeaderTemplate>
                                                     <asp:LinkButton ID="lblReasonHeade1" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                                CommandArgument="Display_Name" ForeColor="Black">Display Name</asp:LinkButton>
                                                  </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDisplayName" Text='<%#Eval("Display_Name")%>' rel='<%#Eval("ID").ToString() %>'
                                                            runat="server" Style="margin: 5px; color: Black;" />
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <HeaderTemplate>
                                                        Remarks
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemarks" Text="View" runat="server" CssClass="lblRemarks" Style="margin: 5px;
                                                            color: Black; cursor: pointer; " />
                                                        <div id="divRemark" runat="server" style="display: none;">
                                                            <%#Eval("Remarks")%>
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                    <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Active" >
                                                    <HeaderTemplate>
                                                        Active
                                                    </HeaderTemplate>
                                                    <HeaderStyle  HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                       
                                                         <asp:Label ID="lblStatus" Text=' <%# Convert.ToBoolean(Eval("Active_Status")) == true ? "Y" : "N"%>' 
                                                            runat="server" Style="margin: 5px; color: Black;" />
                                                        <asp:HiddenField ID="hdnActiveStatus" runat="server" Value='<%#Eval("Active_Status")%>' />
                                                    </ItemTemplate>
                                                     
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Image">
                                                    <HeaderTemplate>
                                                        Image
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgOilMajor" runat="server" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.Oil_Major_Logo") %>'
                                                            Height="20px" />
                                                      
                                                           
                                                    </ItemTemplate>
                                                     <ItemStyle  HorizontalAlign="center" Width="20%" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" >
                                                    <HeaderTemplate>
                                                        Action
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Edit" CssClass="edit" ImageUrl="~/Images/Edit.gif" ForeColor="Black"
                                                            ToolTip="Edit" runat="server" rel='<%#Eval("ID").ToString() %>' Height="16px"
                                                            Width="16px" Visible='<%# uaEditFlag %>' Style="cursor: pointer;" /><%--OnCommand="onEdit"--%>
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" Visible='<%# uaDeleteFlage %>'
                                                            OnCommand="onDelete" OnClientClick="return confirm('Are you sure, you want to delete?')"
                                                            CommandArgument='<%#Eval("[ID]")%>' ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                            Height="16px" Width="16px"></asp:ImageButton>
                                                        <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                            Height="16px" Style="vertical-align: top; cursor: pointer;" Width="16px" runat="server"
                                                            onclick='<%# "Get_Record_Information(&#39;CRW_LIB_CM_OilMajors&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="left" Width="10%" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindOilMajors" />
                                       
                                        <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <br />
                        </div>
                        <div id="divadd" title='<%=OperationMode %>' style="display: none; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 50%; top: 250px">
                            <asp:UpdatePanel runat="server" ID="upoilmajor">
                                <ContentTemplate>
                                    <table width="100%" cellpadding="2" cellspacing="2">
                                        <tr>
                                            <td align="left" style="width: 12%; padding-left: 15px;">
                                                Oil Major Name&nbsp;:&nbsp;
                                            </td>
                                            <td style="color: #FF0000; width: 1%" align="right">
                                                *
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtOilMajorName" ClientIDMode="Static" CssClass="txtInput" MaxLength="200"
                                                    Width="250px" runat="server"></asp:TextBox>
                                                <asp:HiddenField ClientIDMode="Static" ID="hdnOilMajorID" runat="server" Value="0" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 12%; padding-left: 15px;">
                                                Display Name&nbsp;:&nbsp;
                                            </td>
                                            <td style="color: #FF0000; width: 1%" align="right">
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDisplayName" ClientIDMode="Static" CssClass="txtInput" MaxLength="200"
                                                    Width="250px" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 12%; padding-left: 15px;">
                                                Remarks&nbsp;:&nbsp;
                                            </td>
                                            <td style="color: #FF0000; width: 1%" align="right">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <CKEditor:CKEditorControl ID="txtRemarks" runat="server"></CKEditor:CKEditorControl>
                                                <asp:HiddenField ClientIDMode="Static" ID="hdnRemarks" runat="server" Value="0" />
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 12%; padding-left: 15px;">
                                                Image&nbsp;:&nbsp;
                                            </td>
                                            <td align="right">
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                            <asp:HiddenField ID="hdnUploadFileName" runat="server" Value="" />
                                                            <%--<asp:Button ID="btnAttach" runat="server" OnClick="btnAttach_Click" Text="Attachment"
                                                    OnClientClick="return OnbtnAttachClick();" Width="80px" />--%>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="imgLogo" runat="server" style="height:20px;border-width:0px;" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 12%; padding-left: 15px;">
                                                Active &nbsp;:&nbsp;
                                            </td>
                                            <td style="color: #FF0000; width: 1%" align="right">
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="font-size: 11px; text-align: center; background-color: #d8d8d8;">
                                                <asp:Button ID="btnsave" Width="75px" runat="server" Text="Save" ClientIDMode="Static"
                                                    OnClick="btnsave_OnClick" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:AsyncPostBackTrigger ControlID="btnsave" EventName="Click" />--%>
                                    <asp:PostBackTrigger ControlID="btnsave"  />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="dvPopupAddAttachment" style="display: none; width: 520px;" title=''>
            <iframe id="iFrmCopyJobs" src="" frameborder="0" style="height: 200px; width: 100%">
            </iframe>
        </div>
    </center>
    <script type="text/javascript">
        function OnbtnAttachClick() {
            document.getElementById('iFrmCopyJobs').src = "OM_FileUploader.aspx?OM_ID=" + document.getElementById($('[id$=hdnOilMajorID]').attr('id')).value;
            $("#dvPopupAddAttachment").prop('title', 'Add Attachment');
            showModal('dvPopupAddAttachment');
            return false;
        }

        $(document).ready(function () {
            $("body").on("click", "#ImgAdd", function () {
                if (this.id == "ImgAdd") {
                    $("#hdnOilMajorID").val(0);
                    $("#txtOilMajorName").val('');
                    $("#txtDisplayName").val('');
                    $("#<%= txtRemarks.ClientID%>").html('');
                    showModal('divadd', false);
                    CKEDITOR.instances["<%= txtRemarks.ClientID%>"].setData('')
                    $('#<%=chkIsActive.ClientID %>').attr('checked', true);
                    $("#divadd_dvModalPopupTitle").text("Add/Edit Oil Major");
                    $("#<%=imgLogo.ClientID %>").hide();
                }
                else {
                    $("#hdnOilMajorID").val(parseInt($(this).attr("rel")));
                    $("#txtOilMajorName").val($(this).text());
                    $("#txtDisplayName").val($(this).text());
                    $("#<%= txtRemarks.ClientID%>").val($(this).text());
                    showModal('divadd', false);
                    $("#divadd_dvModalPopupTitle").text("Add/Edit Oil Major");
                }
                return false;
            });

            $("body").on("click", ".edit", function () {

                var EditID = $(this)[0].id.replace("ctl00_MainContent_gvOilMajors_", "");
                EditID = EditID.replace("_Edit", "");


                if ($.trim($("#ctl00_MainContent_gvOilMajors_" + EditID + "_hdnActiveStatus").val()) == "1")
                    $("#<%=chkIsActive.ClientID %>").prop("checked", true);
                else
                    $("#<%=chkIsActive.ClientID %>").prop("checked", false);

                $("#hdnOilMajorID").val($(this).attr("rel"));
                var html = $("#ctl00_MainContent_gvOilMajors_" + EditID + "_divRemark").html();
                CKEDITOR.instances["<%= txtRemarks.ClientID%>"].setData(html);

                $("#txtOilMajorName").val($("#ctl00_MainContent_gvOilMajors_" + EditID + "_lblOilMajorName").text());
                $("#txtDisplayName").val($("#ctl00_MainContent_gvOilMajors_" + EditID + "_lblDisplayName").text());


                //Displaying image from gridview to image field ---start
                if ($("#ctl00_MainContent_gvOilMajors_" + EditID + "_imgOilMajor").length > 0) {
                    var Path = $("#ctl00_MainContent_gvOilMajors_" + EditID + "_imgOilMajor").attr("src");
                    $("#<%=imgLogo.ClientID %>").show();
                    $("#<%=imgLogo.ClientID %>").attr("src", Path);
                }
                else {
                    $("#<%=imgLogo.ClientID %>").hide();
                }
                ////Displaying image from gridview to image field ---End

                showModal('divadd', false);
                $("#divadd_dvModalPopupTitle").text("Edit Oil Major");

                return false;
            });

            $("body").on("click", "#btnsave", function () {
                if ($.trim($("#txtOilMajorName").val()) == "") {
                    $("#txtOilMajorName").val('');
                    $("#txtOilMajorName").focus();
                    alert("Please enter oil major name");
                    return false;
                }

                //                if ($.trim($("#txtDisplayName").val()) == "") {
                //                    $("#txtDisplayName").val('');
                //                    $("#txtDisplayName").focus();
                //                    alert("Please enter oil major display name");
                //                    return false;
                //                }

                if ($("#<%=FileUpload1.ClientID %>")[0].value != "") {
                    var sFileName = $("#<%=FileUpload1.ClientID %>")[0].value
                    //alert(sFileName);
                    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
                    var blnValid = false;
                    for (var j = 0; j < _validFileExtensions.length; j++) {
                        var sCurExtension = _validFileExtensions[j];
                        if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                            blnValid = true;
                            $("#<%=hdnUploadFileName.ClientID %>").val(sFileName);
                            break;
                        }
                    }
                    if (!blnValid) {
                        alert("Only jpg, jpeg, bmp, gif, png image are allow to upload");
                        return false;
                    }
                }
                hideModal('divadd');
            });

            $("body").on("mouseover", ".lblRemarks", function () {
                var ID = $(this)[0].id;
                var replacestring = ID.replace("lblRemarks", "");
                var Remark = $("#" + replacestring + "divRemark")[0].innerHTML;

                js_ShowToolTip(Remark, evt, objthis)
            });

            $("body").on("mouseout", ".lblRemarks", function () {
                $("#__divMsgTooltip").hide();
                $("#__divMsgTooltip").html('');
            });
        });

    </script>
</asp:Content>
