<%@ Page Title="Training/Drill  List" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="LMS_Program_List.aspx.cs" Inherits="LMS_Program_List" %>

<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="auc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/Wizard.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.highlight.js" type="text/javascript"></script>
    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            width: 120px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 0px 2px 0px 3px;
            height: 20px;
            vertical-align: middle;
        }
        
        .tdrow
        {
            font-size: 11px;
            text-align: left;
            padding: 2px;
            vertical-align: top;
            line-height: 16px;
        }
    </style>
    <script type="text/javascript">


        $(document).ready(function () {
            $("body").click(function () {

                $('#dvTrainingScheduling').attr('title', 'Training Scheduling');
            });
        });
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

        function Validation_Delete(program_status) {

            if (program_status.toString().length > 0) {
                alert('Training/Drill already has scheduled, so you can not delete Training/Drill');
                return false;
            }

            var con = confirm('Are you sure want to delete this record?');
            if (con) {
                return true;
            }
            else
                return false;
        }


        function Check_NoOfCHAPTER_Exist_In_Program(Total_Chapter) {

            if (Total_Chapter == 0) {
                alert('Training/Drill does not have any chapter, so you can not schedule Training/Drill.');
                return false;
            }
        }
        function LoadAfterCheckBox() {

            $('#<%=chkLstSelectVessel.ClientID%>').each(function () {

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
			

	   
 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Training/Drill List
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
                        <td class="tdd" style=" text-align: left;font-weight:bold">
                            Training/Drill Name : &nbsp;&nbsp;
                            </td>
                            <td>  <asp:TextBox ID="txtProgramName" Width="200px" runat="server"></asp:TextBox>
                            &nbsp;&nbsp;</td>
                        <td class="tdh" style=" text-align: left;font-weight:bold">
                            Category :
                        </td>
                        <td class="tdd">
                            <asp:DropDownList ID="ddlProgramCategory" runat="server" BackColor="#FFFFCC" Font-Size="11px"
                                Height="20px" Visible="true" Width="160px">
                            </asp:DropDownList>
                        </td>
                       <td>
                          
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                Width="80px" CssClass="btnCSS" />
                            &nbsp;
                            <asp:Button ID="btnClearFilter" runat="server" OnClick="btnClearFilter_Click" Text="Clear Filter"
                                Width="80px" CssClass="btnCSS" />
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnAddNewProgram" runat="server" Text="Add New Training/Drill" OnClientClick="javascript:window.open('LMS_Program_Details.aspx');return false;"
                                Style="margin-right: 20px" Width="150" CssClass="btnCSS" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="vertical-align: top;">
                            <asp:GridView ID="gvProgram_ListDetails" AutoGenerateColumns="false" runat="server"
                                Width="100%" CssClass="gridmain-css" CellPadding="4" CellSpacing="0" GridLines="None"
                                EmptyDataText="No Records Found !!" 
                                onrowdatabound="gvProgram_ListDetails_RowDataBound">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Training/Drill Name" ItemStyle-Width="250px">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlProgramDetails" Text='<%#Eval("PROGRAM_Name").ToString().Length>=46?Eval("PROGRAM_Name").ToString().Substring(0, 44)+"...":Eval("PROGRAM_Name").ToString()  %>'
                                                runat="server" ToolTip='<%#Eval("PROGRAM_Name").ToString()%>' NavigateUrl='<%# "~/LMS/LMS_Program_Details.aspx?Program_ID="+Eval("Program_ID").ToString()%>'
                                                Target="_blank"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Training/Drill Description" ItemStyle-Width="800px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProgramDescCon" Text='<%#Eval("PROGRAM_DESCRIPTION").ToString().Length>=150?Eval("PROGRAM_DESCRIPTION").ToString().Substring(0, 148)+".....":Eval("PROGRAM_DESCRIPTION").ToString()  %>'
                                                runat="server"  ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DURATION" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"
                                        HeaderText="Duration" />
                                    <asp:BoundField DataField="Total_Chapter" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"
                                        HeaderText="Total Chapter" />
                                          <asp:TemplateField  Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFreq" Text='<%#Eval("Frequency_Type")  %>'
                                                runat="server"  ></asp:Label>
                                                 <asp:Label ID="lblEff" Text='<%#Eval("EffectiveStartDate")  %>'
                                                runat="server"  ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      
                                    <asp:TemplateField HeaderText="Scheduling" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Button ID="btnScheduling" runat="server" CommandArgument='<%#Eval("PROGRAM_ID") + ";" + Eval("PROGRAM_NAME")+";"+Eval("Frequency_Type")+";"+Eval("EffectiveStartDate")+";"+Eval("EffectiveEndDate")%>'
                                                Enabled='<%# ViewState["admin"].ToString()=="0"?false:true %>' OnClientClick='<%# "return Check_NoOfCHAPTER_Exist_In_Program(&#39;"+Eval("Total_Chapter").ToString()+"&#39;)" %>'
                                                Text="Schedule" OnClick="btnScheduling_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" CssClass="grid-col-fixed" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblActionHeader" Visible='<%# GetAccessInfo("d","","0") %>' runat="server"
                                                Text='Action'></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--  OnClientClick='<%# "return Validation_Delete(&#39;"+Eval("TRG_STATUS").ToString()+"&#39;)" %>'--%>
                                            <table cellpadding="1" cellspacing="0">
                                                <tr align="center">
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="ProgramDelete"
                                                            Visible='<%# (GetAccessInfo("d","","0") && Eval("SchCnt").ToString()=="0" ) %>' CommandArgument='<%#Eval("PROGRAM_ID") %>'
                                                            ForeColor="Black" ToolTip="Remove Item" OnClientClick="return confirm('Are you sure want to delete?')"
                                                            ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <auc:CustomPager ID="ucCustomPagerProgram_List" OnBindDataItem="BindProgramItemInGrid"
                                AlwaysGetRecordsCount="true" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="divMessage" align="center">
            <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
        </div>
        <br />
    </div>
    <div id="dvTrainingScheduling" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 30%; top: 10%; width: 600px; z-index: 1000; color: black" title="Training Scheduling">
        <%--<div class="header">
			<div style="right: 0px; position: absolute; cursor: pointer;">
				<img src="../Images/Close.gif" onclick="closeDiv('dvTrainingScheduling');" alt="Close" />
			</div>
			<h4>
				Training Scheduling</h4>
		</div>--%>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                <ContentTemplate>
                    <table style="width: 100%; background-color: White; cursor: default;">
                        <tr>
                            <td colspan="2" style="font-size: 11px; text-align: left; font-weight: bold">
                                Training/Drill Name :
                                <asp:Label ID="lblProgramName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; text-align: left; font-weight: bold">
                                Vessel List
                            </td>
                            <td style="font-size: 11px; text-align: left; font-weight: bold; vertical-align: top;">
                                Frequency
                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid #cccccc; vertical-align: top;">
                                <div style="overflow: auto; height: 300px; background-color: #">
                                    <asp:CheckBoxList ID="chkLstSelectVessel" runat="server" OnDataBound="chkLstSelectVessel_DataBound"
                                        DataTextField="Vessel_Name" DataValueField="Vessel_ID">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                            <td style="border: 1px solid #cccccc; vertical-align: top; height: 500px;">
                                <table style="overflow: auto; padding: 10px;">
                                    <tr>
                                        <td style="font-size: 11px; text-align: left; font-weight: bold; vertical-align: top;
                                            background-color: #00FFBF; padding: 2px;">
                                            Effective Start Date
                                        </td>
                                    </tr>
                                    <tr>
                                    <td>
                                     <asp:TextBox ID="txtEffectiveStartDate"  runat="server" Width="120px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="calStart" runat="server" Enabled="True" TargetControlID="txtEffectiveStartDate"
                                                                Format="dd/MM/yyyy">
                                                            </cc1:CalendarExtender><span style="color:Red">*</span>
                                                            </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 11px; text-align: left; font-weight: bold; vertical-align: top;
                                            background-color: #00FFBF; padding: 2px;">
                                            Effective End Date
                                        </td>
                                    </tr>
                                           <tr>
                                    <td>
                                     <asp:TextBox ID="txtEffectiveEndDate"  runat="server" Width="120px"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="calEnd" runat="server" Enabled="True" TargetControlID="txtEffectiveEndDate"
                                                                Format="dd/MM/yyyy">
                                                            </cc1:CalendarExtender>
                                                            </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 11px; text-align: left; font-weight: bold; vertical-align: top;
                                            background-color: #00FFBF; padding: 2px;">
                                            <asp:RadioButton ID="rdbMontPanel" runat="server" GroupName="rule" OnCheckedChanged="rdbMontPanel_CheckedChanged"
                                                AutoPostBack="true" />Recursive on selected month(s)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList ID="lstSelectedMonth" CssClass="textbox-css" runat="server" RepeatColumns="3"
                                                RepeatDirection="Horizontal" CellPadding="2">
                                                <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 11px; text-align: left; font-weight: bold; vertical-align: top;
                                            margin-top: 10px; background-color: #00FFBF; padding: 2px;">
                                            <asp:RadioButton ID="rdbDaysPanel" runat="server" GroupName="rule" OnCheckedChanged="rdbDaysPanel_CheckedChanged"
                                                AutoPostBack="true" />
                                        Based on the Rule: </div>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlDays" AutoPostBack="true" runat="server">
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                <asp:ListItem Text="32" Value="32"></asp:ListItem>
                                                <asp:ListItem Text="33" Value="33"></asp:ListItem>
                                                <asp:ListItem Text="34" Value="34"></asp:ListItem>
                                                <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                                <asp:ListItem Text="36" Value="36"></asp:ListItem>
                                                <asp:ListItem Text="37" Value="37"></asp:ListItem>
                                                <asp:ListItem Text="38" Value="38"></asp:ListItem>
                                                <asp:ListItem Text="39" Value="39"></asp:ListItem>
                                                <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                <asp:ListItem Text="41" Value="41"></asp:ListItem>
                                                <asp:ListItem Text="42" Value="42"></asp:ListItem>
                                                <asp:ListItem Text="43" Value="43"></asp:ListItem>
                                                <asp:ListItem Text="44" Value="44"></asp:ListItem>
                                                <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                                <asp:ListItem Text="46" Value="46"></asp:ListItem>
                                                <asp:ListItem Text="47" Value="47"></asp:ListItem>
                                                <asp:ListItem Text="48" Value="48"></asp:ListItem>
                                                <asp:ListItem Text="49" Value="49"></asp:ListItem>
                                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                <asp:ListItem Text="51" Value="51"></asp:ListItem>
                                                <asp:ListItem Text="52" Value="52"></asp:ListItem>
                                                <asp:ListItem Text="53" Value="53"></asp:ListItem>
                                                <asp:ListItem Text="54" Value="54"></asp:ListItem>
                                                <asp:ListItem Text="55" Value="55"></asp:ListItem>
                                                <asp:ListItem Text="56" Value="56"></asp:ListItem>
                                                <asp:ListItem Text="57" Value="57"></asp:ListItem>
                                                <asp:ListItem Text="58" Value="58"></asp:ListItem>
                                                <asp:ListItem Text="59" Value="59"></asp:ListItem>
                                                <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                                <asp:ListItem Text="61" Value="61"></asp:ListItem>
                                                <asp:ListItem Text="62" Value="62"></asp:ListItem>
                                                <asp:ListItem Text="63" Value="63"></asp:ListItem>
                                                <asp:ListItem Text="64" Value="64"></asp:ListItem>
                                                <asp:ListItem Text="65" Value="65"></asp:ListItem>
                                                <asp:ListItem Text="66" Value="66"></asp:ListItem>
                                                <asp:ListItem Text="67" Value="67"></asp:ListItem>
                                                <asp:ListItem Text="68" Value="68"></asp:ListItem>
                                                <asp:ListItem Text="69" Value="69"></asp:ListItem>
                                                <asp:ListItem Text="70" Value="70"></asp:ListItem>
                                                <asp:ListItem Text="71" Value="71"></asp:ListItem>
                                                <asp:ListItem Text="72" Value="72"></asp:ListItem>
                                                <asp:ListItem Text="73" Value="73"></asp:ListItem>
                                                <asp:ListItem Text="74" Value="74"></asp:ListItem>
                                                <asp:ListItem Text="75" Value="75"></asp:ListItem>
                                                <asp:ListItem Text="76" Value="76"></asp:ListItem>
                                                <asp:ListItem Text="77" Value="77"></asp:ListItem>
                                                <asp:ListItem Text="78" Value="78"></asp:ListItem>
                                                <asp:ListItem Text="79" Value="79"></asp:ListItem>
                                                <asp:ListItem Text="80" Value="80"></asp:ListItem>
                                                <asp:ListItem Text="81" Value="81"></asp:ListItem>
                                                <asp:ListItem Text="82" Value="82"></asp:ListItem>
                                                <asp:ListItem Text="83" Value="83"></asp:ListItem>
                                                <asp:ListItem Text="84" Value="84"></asp:ListItem>
                                                <asp:ListItem Text="85" Value="85"></asp:ListItem>
                                                <asp:ListItem Text="86" Value="86"></asp:ListItem>
                                                <asp:ListItem Text="87" Value="87"></asp:ListItem>
                                                <asp:ListItem Text="88" Value="88"></asp:ListItem>
                                                <asp:ListItem Text="89" Value="89"></asp:ListItem>
                                                <asp:ListItem Text="90" Value="90"></asp:ListItem>
                                                <asp:ListItem Text="91" Value="91"></asp:ListItem>
                                                <asp:ListItem Text="92" Value="92"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;Day(s)
                                        </td>
                                    </tr>
                                    <%--<tr>
										<td>
											<asp:CheckBoxList ID="lstSelectedRules" CssClass="textbox-css" runat="server" RepeatDirection="Vertical"
												OnSelectedIndexChanged="lstSelectedRulesIndex_Changed" AutoPostBack="true" CellPadding="2"
												CausesValidation="True">
												<asp:ListItem Text="3 Months" Value="90"></asp:ListItem>
												<asp:ListItem Text="6 Months" Value="180"></asp:ListItem>
											</asp:CheckBoxList>
										</td>
									</tr>--%>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 11px; text-align: center;">
                                <%--	<asp:Button ID="btnSaveScheduleAddNew" CssClass="button-css" runat="server" Text="Save & Add New"
									OnClick="btnSaveScheduleAddNew_Click" />&nbsp;&nbsp;--%>
                                <asp:Button ID="btnSaveAndCloseSchedule" CssClass="button-css" runat="server" Text="Save & Close"
                                    OnClick="btnSaveAndCloseSchedule_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClick="btncancel_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
