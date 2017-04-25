<%@ Page Title="Survey Certificates" Language="C#" MasterPageFile="SurveyMaster.master"
    AutoEventWireup="true" CodeFile="SurveyCertificateLib.aspx.cs" Inherits="SurveyCertificateLib" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function validation() {


            if (document.getElementById("ctl00_MainContent_txtCertificateName").value.trim() == "") {
                alert("Please enter certificate name.");
                document.getElementById("ctl00_MainContent_txtCertificateName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_ddlSurvey_MainCategory").value == "0") {
                alert("Please select main category.");
                document.getElementById("ctl00_MainContent_ddlSurvey_MainCategory").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_ddlSurvey_Category").value == "0") {
                alert("Please select sub category.");
                document.getElementById("ctl00_MainContent_ddlSurvey_Category").focus();
                return false;
            }


            if (document.getElementById("ctl00_MainContent_txtTerm").value != "") {
                if (document.getElementById("ctl00_MainContent_txtTerm").value <= 0) {
                    alert("Term field can not be zero or less than zero");
                    document.getElementById("ctl00_MainContent_txtTerm").focus();
                    return false
                }

            }

            if (document.getElementById("ctl00_MainContent_txtGraceRange").value != "") {
                if ((document.getElementById("ctl00_MainContent_txtGraceRange").value <= 0) || (document.getElementById("ctl00_MainContent_txtGraceRange").value > 12)) {
                    alert('Range field should not less than 1 and more than 12.');
                    document.getElementById("ctl00_MainContent_txtGraceRange").focus();
                    return false;
                }

            }

            return true;
        }

        function Filter() {
            $("#<%=btnFilter.ClientID %>").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <div id="AccessMsgDiv" runat="server" style="color: Red; font-size: 14px; text-align: center;">
            You don't have sufficient privilege to access the requested page.</div>
        <div id="MainDiv" runat="server">
            <asp:UpdateProgress ID="upUpdateProgress" runat="server" AssociatedUpdatePanelID="UpdUserType">
                <ProgressTemplate>
                    <div id="blur-on-updateprogress">
                        &nbsp;</div>
                    <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                        color: black">
                        <img src="../Images/loaderbar.gif" alt="Please Wait" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div style="font-family: Tahoma; font-size: 12px; height: 100%;">
                <div style="height: 650px; width: 100%; color: Black;">
                    <asp:UpdatePanel ID="UpdUserType" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlsearch" runat="server" DefaultButton="btnFilter">
                                <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 100%">
                                    <table width="100%" cellpadding="2" cellspacing="2">
                                        <tr>
                                            <td align="center" style="width: 120px;">
                                                Certificate Name:
                                            </td>
                                            <td align="left" style="width: 100px;">
                                                <asp:TextBox ID="txtfilter" runat="server" Width="100px"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                                    WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                            </td>
                                            <td align="center" style="width: 120px;">
                                                Main Category:
                                            </td>
                                            <td style="width: 200px;" align="left">
                                                <asp:DropDownList ID="ddlMainCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMainCategory_SelectedIndexChanged"
                                                    Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 120px">
                                                Sub Category
                                            </td>
                                            <td style="width: 170px;" align="left">
                                                <asp:DropDownList ID="ddlCategoryFilter" runat="server" CssClass="txtInput" AppendDataBoundItems="true"
                                                    Width="170px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 20px">
                                                <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                                    ImageUrl="~/Images/SearchButton.png" />
                                            </td>
                                            <td align="center" style="width: 20px">
                                                <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                                    ImageUrl="~/Images/Refresh-icon.png" />
                                            </td>
                                            <td align="center" style="width: 20px">
                                                <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add New Survey Certificate"
                                                    OnClick="ImgAdd_Click" ImageUrl="~/Images/Add-icon.png" />
                                            </td>
                                            <td style="width: 20px">
                                                <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                                    ImageUrl="~/Images/Exptoexcel.png" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                <div>
                                    <asp:GridView ID="GridView_Certificate" runat="server" EmptyDataText="NO RECORDS FOUND"
                                        AutoGenerateColumns="False" OnRowDataBound="GridView_Certificate_RowDataBound"
                                        DataKeyNames="Surv_ID" CellPadding="1" CellSpacing="0" Width="100%" GridLines="both"
                                        OnSorting="GridView_Certificate_Sorting" AllowSorting="true" CssClass="gridmain-css">
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                        <PagerStyle CssClass="PMSPagerStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Certificate Name" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblCertificate_NameHeader" runat="server" CommandName="Sort"
                                                        CommandArgument="Survey_Cert_Name" ForeColor="Black">Certificate Name&nbsp;</asp:LinkButton>
                                                    <img id="Survey_Cert_Name" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblCertificate_Name" runat="server" CommandArgument='<%#Eval("Surv_ID")%>'
                                                        Text='<%#Eval("Survey_Cert_Name")%>' OnCommand="onUpdate" Style="color: Black"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Height="28px" Width="200px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Certificate Remarks" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSurvey_Cert_remarks" runat="server" Text='<%#Eval("Survey_Cert_remarks")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Term" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%#Eval("Term")%>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Main Category" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblMainCategory_NameHeader" runat="server" CommandName="Sort"
                                                        CommandArgument="Survey_MainCategory" ForeColor="Black">Main Category&nbsp;</asp:LinkButton>
                                                    <img id="Survey_MainCategory" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMainCategory_Name" runat="server" Text='<%#Eval("Survey_MainCategory")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lblCategory_NameHeader" runat="server" CommandName="Sort" CommandArgument="Survey_Category"
                                                        ForeColor="Black">Sub-Category&nbsp;</asp:LinkButton>
                                                    <img id="Survey_Category" runat="server" visible="false" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory_Name" runat="server" Text='<%#Eval("Survey_Category")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Alert (Insurance)">
                                                <ItemTemplate>
                                                    <%# string.IsNullOrEmpty(Convert.ToString(Eval("Alert_Insurance")))?"": Convert.ToBoolean(Eval("Alert_Insurance")) == true ? "Y" : ""%>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="60px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Range(Months)" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGraceRange" runat="server" Text='<%# Eval("GraceRange").ToString() == "0" ? "" : Eval("GraceRange")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inspection Required?" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# string.IsNullOrEmpty(Convert.ToString(Eval("InspectionRequired"))) ? "" : Convert.ToBoolean(Eval("InspectionRequired")) == true ? "Y" : ""%>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderTemplate>
                                                    Action
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                        Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Surv_ID]")%>' ForeColor="Black"
                                                        ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                    <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                        Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure you want to delete?')"
                                                        CommandArgument='<%#Eval("[Surv_ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                        Height="16px"></asp:ImageButton>
                                                    <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                        Height="16px" Style="vertical-align: top; cursor: pointer;" Width="16px" runat="server"
                                                        onclick='<%# "Get_Record_Information(&#39;TBLSURV_LIB&#39;,&#39;Surv_ID="+Eval("Surv_ID").ToString()+"&#39;,event,this)" %>' />
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="65px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="100" OnBindDataItem="BindSurveyCertificate" />
                                    <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                </div>
                                <br />
                            </div>
                            <asp:Panel ID="pnladd" runat="server" DefaultButton="btnsave">
                                <div id="divadd" title="<%= OperationMode %>" style="display: none; font-family: Tahoma;
                                    text-align: left; font-size: 12px; color: Black; width: 30%">
                                    <table width="100%" cellpadding="2" cellspacing="2">
                                        <tr>
                                            <td align="right" style="width: 7%">
                                                Certificate Name:
                                            </td>
                                            <td style="color: #FF0000; width: 1%" align="right">
                                                *
                                            </td>
                                            <td align="left" style="width: 20%">
                                                <asp:TextBox ID="txtCertificateName" CssClass="txtInput" Width="90%" runat="server"
                                                    MaxLength="250"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Main Category Name:
                                            </td>
                                            <td style="color: #FF0000;" align="right">
                                                *
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlSurvey_MainCategory" runat="server" Width="91%" CssClass="txtInput"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSurvey_MainCategory_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Sub Category Name:
                                            </td>
                                            <td style="color: #FF0000;" align="right">
                                                *
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlSurvey_Category" runat="server" Width="91%" CssClass="txtInput">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Term:
                                            </td>
                                            <td>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtTerm" Width="50px" MaxLength="9" runat="server"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    TargetControlID="txtTerm" FilterType="Numbers">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Certificate Remark:
                                            </td>
                                            <td>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSurvey_Cert_remarks" Width="90%" runat="server"
                                                    TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Alert(Insurance) &nbsp;:&nbsp;
                                            </td>
                                            <td>
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="chkAlert" Style="margin-left: -3px;" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Range &nbsp;:&nbsp;
                                            </td>
                                            <td>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtGraceRange" Width="50px" runat="server" MaxLength="2"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="flttxtRange" runat="server" TargetControlID="txtGraceRange"
                                                    FilterType="Numbers">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                &nbsp; Months
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Inspection Required? &nbsp;:&nbsp;
                                            </td>
                                            <td>
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="chkInspectionRequired" Style="margin-left: -3px;" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="font-size: 11px; text-align: center;">
                                                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" OnClientClick="return validation();" />
                                                <asp:TextBox ID="txtCertificateID" runat="server" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                                * Mandatory fields
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="ImgExpExcel" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </center>
</asp:Content>
