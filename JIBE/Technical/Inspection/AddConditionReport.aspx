<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="AddConditionReport.aspx.cs"
    Inherits="Technical_Worklist_AddConditionReport" EnableViewState="true" EnableEventValidation="false" %>

<html>
<head runat="server">
    <title>OT03-Work List</title>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />

 
    <script type="text/javascript">

        function ValidatePage() {

            var hdnMode = document.getElementById($('[id$=hdnOPMode]').attr('id')); ;
           // alert(hdnMode.value);
            var ddlType = document.getElementById($('[id$=ddlType]').attr('id'));
            var txtDesc = document.getElementById($('[id$=txtDesc]').attr('id'));
            var ddlDeptShp = document.getElementById($('[id$=ddlDeptShip]').attr('id'));
            //  alert(ddlDeptShp.id);
            var ddlDeptOf = document.getElementById($('[id$=ddlDeptOff]').attr('id'));
            if (hdnMode.value == "ADD") {
              
                if (ddlType != null) {

                    //if (ddlType.value == "-Select-") {
                    if (ddlType.value == "0") {

                        alert("Select type.");
                        return false;
                    }


                }
                if (txtDesc != null) {

                    if (txtDesc.value == "") {

                        alert("Enter description.");
                        return false;
                    }

                }
                if (ddlDeptShp != null) {

                    if (ddlDeptShp.value == "0") {

                        alert("Select department on ship.");
                        return false;
                    }

                }
                if (ddlDeptOf != null) {

                    if (ddlDeptOf.value == "0") {

                        alert("Select department in office.");
                        return false;
                    }

                }
            }
            else if (hdnMode.value == "EDIT") {

                if (ddlType != null) {

                     if (ddlType.value == "0")
                     {

                        alert("Select type.");
                        return false;
                    }


                }
             
                if (ddlDeptShp != null) {

                    if (ddlDeptShp.value == "0") {

                        alert("Select department on ship.");
                        return false;
                    }

                }
                if (ddlDeptOf != null) {

                    if (ddlDeptOf.value == "0") {

                        alert("Select department in office.");
                        return false;
                    }

                }
            }

            return true;
        }
    </script>
    <style type="text/css">
        .Rating-HeaderStyle-css
        {
            /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #333333;
            font-size: 12px;
            padding: 5px;
            text-align: left;
            vertical-align: middle;
            background-color: #CCCCCC;
            border: 1px solid #000;
            border-collapse: collapse;
        }
        .Rating-HeaderStyle-css th
        {
            border: 1px solid #000;
            border-collapse: collapse;
        }
        
          .Rating-ASCHeaderStyle-css
        {
            /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #333333;
       
        }
        
          .Rating-DESCHeaderStyle-css
        {
            /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: red;
           
        }
     /*   .RatingSortedAscendingCellStyle-css
        {
            background-color: #F1F1F1;
        }
        .RatingSortedAscendingHeaderStyle-css
        {
            background-color: #594B9C;
        }
        .RatingSortedDescendingCellStyle-css
        {
            background-color: #CAC9C9;
        }
        .RatingSortedDescendingHeaderStyle
        {
            background-color: #33276A;
        }*/
         .LinkButton
        {
            text-decoration: none;
            color:Black;
        }
        .LinkButton:hover
        {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="frmCondReport" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <script type="text/javascript">

         function ShowUploader() {

             if (ValidatePage() == true) {

                 __doPostBack("<%=BtnSave1.UniqueID %>", "onclick");

                // showModal('dvPopupAddAttachment');
                 return false;
             }
             return false;
         }


         function Rebind() {


             __doPostBack("<%=btnRefresh.UniqueID %>", "onclick");
             
             
             return false;
         }

        
    </script>

    <asp:UpdatePanel ID="UpdatePanelport" runat="server">
        <ContentTemplate>
            <div id="dvDefectCond" title="Defect/Condition Report" style="width:100%; height:100%;">
                <table width="100%"  >
                    <tr>
                        <td>
                            <asp:Label ID="lblType" runat="server" Text="Type:" Font-Names="Calibri" Font-Size="14px"
                                Font-Bold="true"></asp:Label><b style="color:Red">*</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" Width="150px" Font-Names="Calibri"
                                Font-Size="14px">
                               <%-- <asp:ListItem Value="-Select-">-Select-</asp:ListItem>--%>
                                <%--<asp:ListItem Value="Defect">Defect</asp:ListItem>
                                <asp:ListItem Value="Condition Report">Condition Report</asp:ListItem>--%>

                              <%--  <asp:ListItem Value="Defect">NON OT03</asp:ListItem>
                                <asp:ListItem Value="Condition Report">OT03</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDesc" runat="server" Text="Description:" Font-Names="Calibri" Font-Size="14px"
                                Font-Bold="true"></asp:Label><b style="color:Red">*</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="400px" Height="100px"
                                Font-Names="Calibri" Font-Size="14px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAssignedDept" runat="server" Text="Assigned departments:" Font-Names="Calibri"
                                Font-Size="14px" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDeptShip" runat="server" Text="Dept on ship:" Font-Names="Calibri"
                                Font-Size="14px" Font-Bold="true"></asp:Label><b style="color:Red">*</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDeptShip" runat="server" Width="150px" Font-Names="Calibri"
                                Font-Size="14px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDeptOff" runat="server" Text="Dept in office:" Font-Names="Calibri"
                                Font-Size="14px" Font-Bold="true"></asp:Label><b style="color:Red">*</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDeptOff" runat="server" Width="150px" Font-Names="Calibri"
                                Font-Size="14px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblExpComp" runat="server" Text="Estimated completion date:" Font-Names="Calibri"
                                Font-Size="14px" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtExpectedComp" runat="server" Width="100px"></asp:TextBox>
                            <tlk4:CalendarExtender ID="calExpectedComp" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="txtExpectedComp">
                            </tlk4:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblComp" runat="server" Text="Attachments:" Font-Names="Calibri" Font-Size="14px"
                                Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="updJobAttachment" runat="server" >
                                <ContentTemplate>
                                    <asp:Button ID="btnRefreshJobAttachment" runat="server" Style="display: none" Text="RefreshJobAttachment"
                                        OnClick="btnRefreshPMSJobAttachment_Click" />
                                    <asp:DataList ID="gvWLJobAttachment" runat="server" RepeatColumns="5" RepeatDirection="Vertical"
                                        RepeatLayout="Table" CellSpacing="2">
                                        <ItemTemplate>
                                            <div style="background-color: #C3EBFF; border-radius: 2px; padding: 1px; border: 1px solid #ACC9C9">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:HyperLink ID="lblAssemblyName" runat="server" Text='<%# Eval("Attach_Name")%>'
                                                                NavigateUrl='<%# "../../Uploads/Technical/" + Eval("Attach_Path").ToString()%>'
                                                                Target="_blank" Font-Size="12px" Font-Names="Calibri"></asp:HyperLink>
                                                            <asp:Label ID="lblATTACHMENT_PATH" Visible="false" runat="server" Text='<%# Eval("Attach_Path")%>' ></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton ID="imgbtnDeleteAssembly" runat="server" OnCommand="imgbtnDeleteAssembly_Click"
                                                                CommandArgument='<%# Eval("Attach_Path")+";"+Eval("Attachment_ID")+";"+Eval("WORKLIST_ID")+";"+Eval("VESSEL_ID")+";"+Eval("WL_OFFICE_ID") %>' AlternateText="delete" ImageAlign="AbsMiddle"
                                                                ImageUrl="~/Images/Delete.png" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                       <td colspan="2" align="center">
                              
                                  <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Calibri" Font-Size="14px"
                                Font-Bold="true" OnClick="BtnSave_Click" 
                                      OnClientClick="return ValidatePage();" />

                                      <asp:Button ID="BtnSave1" runat="server" Text="Save" Font-Names="Calibri" Font-Size="14px" Visible="false"
                                Font-Bold="true" OnClick="BtnSave1_Click" />

                                  <asp:Button ID="BtnAttach" runat="server" Text="Attachments" Font-Names="Calibri"
                                Font-Size="14px" Font-Bold="true" OnClientClick="return ShowUploader();" 
                                      />

                                <asp:Button ID="BtnVerify" runat="server" Text="Verify" 
                                      Font-Names="Calibri" Font-Size="14px"
                                Font-Bold="true" OnClick="BtnVerify_Click" 
                                      OnClientClick="return ValidatePage();" Enabled="False" />

                                 <asp:Button ID="BtnClear" runat="server" Text="Clear" Font-Names="Calibri" Font-Size="14px"
                                Font-Bold="true" OnClick="BtnClear_Click"  />

                                <asp:Button ID="btnRefresh" runat="server" Text="refresh" Font-Names="Calibri" Font-Size="14px" Visible="false"
                                Font-Bold="true" OnClick="btnRefresh_Click"  />
                        </td>
                     
                    </tr>
                    <tr>
                        <td colspan="2">
                        <div style=" height:300px; " >
                        
                            <asp:GridView ID="grdWLJobs" CellPadding="4" runat="server" ShowFooter="true" AutoGenerateColumns="false"
                                BorderWidth="1px"  Width="100%"  
                                OnRowDataBound="grdWLJobs_RowDataBound" BorderColor="Black" 
                                BorderStyle="Solid" AllowSorting="false" onsorting="grdWLJobs_Sorting" >
                                <HeaderStyle CssClass="Rating-HeaderStyle-css" Font-Names="Calibri" />
                                <RowStyle CssClass="RowStyle-css" Font-Size="12px" Font-Names="Calibri" BorderStyle="Solid" BorderColor="#000" BorderWidth="1px" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                               
								<SortedAscendingHeaderStyle ForeColor="ActiveBorder"/>
								
								<SortedDescendingHeaderStyle ForeColor="ActiveBorder" />
                                
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        <HeaderTemplate>
                                            Worklist ID
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkWLID" CommandName="Select" ToolTip="Select" runat="server" 
                                                ForeColor="GrayText" Font-Bold="true" CssClass="LinkButton" Text='<%# Bind("WLID_DISPLAy") %>'
                                                CommandArgument='<%# Eval("WORKLIST_ID")+";"+Eval("VESSEL_ID")+";"+Eval("OFFICE_ID") %>' OnCommand="lnkRating_Click"></asp:LinkButton>
                                            <%--<asp:HiddenField ID="hdnActiveStatus" runat="server" Value='<%# Bind("Active_Status") %>' />--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="ItemStyle-css" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                        HeaderText="Vessel" SortExpression="Vessel_Short_Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVesselShortName" runat="server" Text='<%#Eval("Vessel_Name") %>'
                                                Style="white-space: nowrap"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="PaddingCellCss" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Type" DataField="Type" ItemStyle-Width="80px" >
                                    <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Description" DataField="JOB_DESCRIPTION" 
                                        ItemStyle-Width="300px" >
                                    <ItemStyle Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Dept. On Ship" DataField="ONSHIP_DEPT" 
                                        ItemStyle-Width="80px" >
                                    <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Dept.in Office" DataField="INOFFICE_DEPT" 
                                        ItemStyle-Width="100px" >
                                    <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Estm. Compln" SortExpression="DATE_ESTMTD_CMPLTN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDATE_ESTMTD_CMPLTN" runat="server" Text='<%# Eval("DATE_ESTMTD_CMPLTN","{0:dd/MMM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_ESTMTD_CMPLTN","{0:d/MMM/yy}") %>'></asp:Label>
                                            </ItemTemplate>
                                              <HeaderStyle HorizontalAlign="Center" />
                                              <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="PaddingCellCss" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                        HeaderText="Completed" SortExpression="DATE_COMPLETED">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrdCompletedOn" runat="server" Text='<%# Eval("DATE_COMPLETED","{0:dd/MMM/yy}").ToString() == "01/01/1900" ? "" : Eval("DATE_COMPLETED","{0:d/MMM/yy}") %>'></asp:Label>
                                            </ItemTemplate>
                                              <HeaderStyle HorizontalAlign="Center" />
                                              <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="PaddingCellCss" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="0">
                                                <tr>
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgEditJob" runat="server" ForeColor="Black" ToolTip="Edit"
                                                            ImageUrl="~/purchase/Image/Edit.gif" OnCommand="ImgEditJob_Click" CommandArgument='<%# Eval("WORKLIST_ID")+";"+Eval("VESSEL_ID")+";"+Eval("OFFICE_ID") %>'
                                                            Width="16px" Height="16px" Visible='<%# Eval("DATE_COMPLETED","{0:dd/MMM/yy}").ToString() == "" ? true : false %>'></asp:ImageButton>
                                                    </td>
                                                     <td style="border-color: transparent">
                                            <asp:ImageButton ID="ImgRatingDelete" runat="server" ForeColor="Black" ToolTip="Delete"
                                                ImageUrl="~/purchase/Image/Delete.gif" OnClientClick="return confirm('Are you sure you want to delete this?')"
                                                OnCommand="ImgDeleteJob_Click" CommandArgument='<%# Eval("WORKLIST_ID")+";"+Eval("VESSEL_ID")+";"+Eval("OFFICE_ID") %>' Width="16px"
                                                Height="16px"></asp:ImageButton>
                                        </td>
                                      <%--  <td style="border-color: transparent">
                                            <asp:ImageButton ID="ImgRatingRestore" runat="server" ForeColor="Black" ToolTip="Restore"
                                                ImageUrl="~/purchase/Image/restore.png"  OnClientClick="return confirm('Are you sure, you want to  restore ?')" OnCommand="ImgRatingRestore_Click" CommandArgument='<%# Bind("ID") %>'
                                                Width="16px" Height="16px"></asp:ImageButton>
                                        </td>--%>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="PaddingCellCss" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Records Found
                                </EmptyDataTemplate>
                            </asp:GridView>
                          
                            <auc:CustomPager ID="ucCustomPagerctp" OnBindDataItem="Search_Worklist" AlwaysGetRecordsCount="true"
                                            PageSize="10" RecordCountCaption="Total Jobs" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dvPopupAddAttachment" title="Add Attachments" style="display: none; width: 500px;">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                <tr>
                <td>
                    <asp:Label ID="lblUploadError" runat="server" Text="" Font-Bold="true" Font-Size="14px" ForeColor="red"></asp:Label>
                </td>
                </tr>
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
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvReworkClose" style="display: none; height: 200px; width: 400">
        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <table width="100%" cellpadding="5" cellspacing="0" border="0">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblWorklistTitle" runat="server" Font-Size="11" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Remark :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtWorklistStatusRemark" runat="server" TextMode="MultiLine" MaxLength="8000"
                                Height="60px" Width="300px"> </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfRemark" runat="server" ControlToValidate="txtWorklistStatusRemark"
                                ErrorMessage="Please enter remark !" ValidationGroup="worklistgrp"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table width="100%" cellpadding="5" cellspacing="0" border="0">
            <tr>
                <td align="center">
                    <asp:Button ID="btnSaveStatus" OnClick="btnSaveStatus_Click" Height="30px" Width="100px"
                        ValidationGroup="worklistgrp" Text="Save" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <asp:HiddenField ID="hdnOPMode" runat="server" />

        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
