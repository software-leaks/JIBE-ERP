 

<%@ Page Title="Risk Assessment Index" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" 
    CodeFile="RiskAssessmentIndex.aspx.cs" Inherits="JRA_RiskAssessmentIndex" %>

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
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
      
    <script language="javascript" type="text/javascript">

        function Validation() {

            if (document.getElementById('<%= txtRemark.ClientID %>').value.trim() == "") {
                alert("Hazard Description is mandatory.");
                document.getElementById('<%= txtRemark.ClientID %>').focus();
                return false;
            }
            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <center>    
 <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;
            height: 100%;">
             <div class="page-title">
                Risk Assessment
            </div>
            <div style="height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 80px;">
                            <table width="100%" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td style="width: 15%" align="right">
                                        Parent Work Category :&nbsp;
                                    </td>
                                    <td style="width: 20%" align="left">
                                        <asp:DropDownList ID="ddlParentWorkCateg" Width="98%" runat="server"  AutoPostBack="true" 
                                            onselectedindexchanged="ddlParentWorkCateg_SelectedIndexChanged" 
                                            >
                                          
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 8%" align="right">
                                        Work Category :&nbsp;
                                    </td>
                                    <td style="width: 20%" align="left">
                                        <asp:DropDownList ID="ddlChildWorkCateg" Width="98%" runat="server" > 
                                        </asp:DropDownList>
                                    </td>
                                      <td style="width: 8%" align="right">
                                        Status :&nbsp;
                                    </td>
                                    <td style="width: 20%" align="left">
                                        <asp:DropDownList ID="ddlAssessmentStatus" Width="98%" runat="server" > 
                                        <asp:ListItem Value="0" Text="-- SELECT ALL --"></asp:ListItem>
                                                       <asp:ListItem Value="Approval Pending" Text="Approval Pending"></asp:ListItem>
                                                        <asp:ListItem Value="My Approval Pending" Text="My Approval Pending"></asp:ListItem>
                                                       <asp:ListItem Value="Rework" Text="Rework"></asp:ListItem>
                                                       <asp:ListItem Value="Approved" Text="Approved"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Clear and Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" style="height: 22px" />
                                    </td>
                                  
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" style="width: 24px" />
                                    </td>
                                </tr>
                                <tr>
                                  <td style="width: 15%" align="right">
                                        Assessment Date From :&nbsp;
                                    </td>
                                    <td style="width: 20%" align="left">
                                        <asp:TextBox ID="txtFromDate" Width="98%" runat="server"   >
                                          
                                        </asp:TextBox>
                                         <cc1:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 15%" align="right">
                                        Assessment Date To :&nbsp;
                                    </td>
                                    <td style="width: 20%" align="left">
                                        <asp:TextBox ID="txtToDate" Width="98%" runat="server"  AutoPostBack="true" >
                                          
                                        </asp:TextBox>
                                        <cc1:CalendarExtender ID="calToDate" runat="server" Enabled="True" TargetControlID="txtToDate"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 15%" align="right">
                                       Vessel :&nbsp;
                                    </td>
                                    <td style="width: 20%" align="left">
                                     <asp:DropDownList ID="DDLVessel" runat="server" UseInHeader="false"    
                                                                      Width="98%" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="gvAssessment" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                                    OnRowDataBound="gvAssessment_RowDataBound" DataKeyNames="Assessment_ID" CellPadding="1" CellSpacing="0"
                                    Width="100%" GridLines="both" OnSorting="gvAssessment_Sorting" AllowSorting="true" CssClass="gridmain-css">
                                  <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                 <RowStyle CssClass="RowStyle-css" />
                                 <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField  >
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblVessel_NameHead" runat="server" CommandName="Sort" CommandArgument="Vessel_Name"
                                                    ForeColor="Black">Vessel_Name&nbsp;</asp:LinkButton>
                                                <img id="Vessel_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVessel_NameView" runat="server"  
                                                    Text='<%#Eval("Vessel_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField >
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblWork_Categ_NameView" runat="server" CommandName="Sort" CommandArgument="Work_Category_Name"
                                                    ForeColor="Black">Job Assessed&nbsp;</asp:LinkButton>
                                                <img id="Work_Category_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblWork_Categ_NameView" runat="server" Text='<%#Eval("Work_Category_Name")%>' 
                                                NavigateUrl='<%# "~/JRA/RiskAssessmentDetails.aspx?Assessment_ID="+Eval("Assessment_ID").ToString()+"&Vessel_ID="+Eval("Vessel_ID").ToString()
                                                 %>' 
                                                
                                                Target="_blank" ></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  >
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblWork_Categ_ValueHEader" runat="server" CommandName="Sort" CommandArgument="Work_Categ_Value"
                                                    ForeColor="Black">Assessment No.&nbsp;</asp:LinkButton>
                                                <img id="Work_Categ_Value" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblWork_Categ_ValueView" runat="server" Text='<%#Eval("Work_Categ_Value")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblCurrent_Assessment_DateHeader" runat="server" CommandName="Sort" CommandArgument="Current_Assessment_Date"
                                                    ForeColor="Black">Current Assessment Date&nbsp;</asp:LinkButton>
                                                <img id="Current_Assessment_Date" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrent_Assessment_DateView" runat="server" Text='<%# Convert.ToDateTime(Eval("Current_Assessment_Date")).ToString("dd/MMM/yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  >
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblLast_Assessment_Date_HEader" runat="server" CommandName="Sort" CommandArgument="Last_Assessment_Date"
                                                    ForeColor="Black">Last_Assessment_Date&nbsp;</asp:LinkButton>
                                                <img id="Last_Assessment_Date" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLast_Assessment_Date_View" runat="server" Text='<%# Eval("Last_Assessment_Date").ToString()!=""?Convert.ToDateTime(Eval("Last_Assessment_Date")).ToString("dd/MMM/yyyy"):""%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  >
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblAssessment_StatusHeader" runat="server" CommandName="Sort" CommandArgument="Assessment_Status"
                                                    ForeColor="Black">Assessment Status&nbsp;</asp:LinkButton>
                                                <img id="Assessment_Status" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAssessment_StatusView" runat="server" Text='<%# Eval("Assessment_Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Verification Type">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblVerificationType" runat="server" Text='<%# Eval("Verification_Type").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="80px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                      <%--  <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[Assessment_ID]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>--%>
                                                       

                                                        <td>
                                                            <asp:ImageButton ID="imgApporveReject" runat="server" Text="Update" OnCommand="onUpdate" ToolTip="Approve or Rework Assessment"
                                                                 CommandArgument='<%#Eval("Assessment_ID")+";"+Eval("Vessel_ID")+";"+Eval("Work_Categ_ID")%>' ForeColor="Black"  Visible="false"
                                                                  ImageUrl="~/Images/acceptreject.png" Height="16px"></asp:ImageButton>
                                                                <%--  Visible='<%#  Convert.ToBoolean(Eval("ValidUser")) %>'--%>
                                                        </td>

                                                        <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;JRA_DTL_RISK_ASSESSMENTS&#39;,&#39;Assessment_ID="+Eval("Assessment_ID").ToString()+" and Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindAssessmentTemplateGrid" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                            <br />
                        </div>
                         <div id="divadd" title="Approve/Rework Assessment" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 40%;">
                            <table width="100%" cellpadding="2" cellspacing="2">
                              <tr>
                              <td align="right" style="width: 6%" valign="top"   >
                                        Remark &nbsp;:&nbsp;
                                    </td>
                                    <td style="color: #FF0000; width: 1%" align="right" valign="top"   >   *
                                    </td>
                                    <td align="left" style="width: 34%">
                                        <asp:TextBox ID="txtRemark" CssClass="txtInput" Width="95%" MaxLength="1000" 
                                            runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                              </tr>
                                <tr>
                                    <td colspan="6" style="font-size: 11px; text-align: center; border-style: solid;
                                        border-color: Silver; border-width: 1px">
                                        <asp:Button ID="bntApprove" runat="server" Text="Approve" 
                                            OnClientClick="return Validation();" onclick="bntApprove_Click" />
                                        <asp:Button ID="btnRework" runat="server" Text="Rework"  
                                            OnClientClick="return Validation();" style="margin-left:24px" 
                                            onclick="btnRework_Click" />
                                         <asp:Button ID="btnCloese" runat="server" Text="Close"  
                                             style="margin-left:24px; height: 25px;" 
                                            onclick="btnCloese_Click" />
                                          <asp:HiddenField ID="hfAssessentKey" runat="server" />
                                    </td>
                                </tr>
                                
                               
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
