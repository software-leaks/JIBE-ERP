<%@ Page Title="New Travel Request" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="NewRequest.aspx.cs" Inherits="NewRequest" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/uc_Vessel_List.ascx" TagName="uc_Vessel_List" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ctlAirPortList.ascx" TagName="AirPortList" TagPrefix="ucAirPortList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/EGSoft.js" type="text/javascript"></script>
    <link href="../Styles/EGSoft.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.alerts.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        var retstaffname = ""; var blnRetVal = false; var vpaxnamehtml = "";

        function OnCheckPersonTravelledInFiveDays(id) {

            if (!Page_ClientValidate('vDeptdt'))
                return false;

            var staffids = document.getElementById("ctl00_MainContent_hdf_StaffIds").value;
            if (staffids == ",") {
                alert("at-least one staff required!");
                return false;
            }

            GePersonNameTravelInFiveDays(staffids);
            var fun = "OnCheckPersonTravelledInFiveDays_Child('" + id + "')";   
            setTimeout(fun, 500);
            return false;


        }


        function Func1Delay() {
            setTimeout("Func1()", 1000);
        }


        function Func1() {
            alert("Delayed 1   seconds");
        }


        function GePersonNameTravelInFiveDays(staffids) {

            TravelService.CheckPersonTravelWithinFiveDays(staffids, OnResult);
        }

        function OnResult(htmltableresult) {

            vpaxnamehtml = htmltableresult
            //            retstaffname = htmltableresult;
        }


        function OnCheckPersonTravelledInFiveDays_Child(id) {


            var findstr = vpaxnamehtml.indexOf("No record found !");

            if (findstr < 0) {

                $.alerts.okButton = " Yes ";
                $.alerts.cancelButton = " No ";

                var strMsg = "Jibe has found the following tickets for the same pax within the last 5 days" + "\n\n"
                                       + vpaxnamehtml + "\n\n"
                                       + "Do you still want to continue & raise a new travel request for this same person ?";

                var aa = jConfirm(strMsg, ' Confirmation Required !', function (r) {

                    if (r) {


                        var postBackstr = "__doPostBack('" + id.replace(/_/gi, '$') + "','" + id.replace(/_/gi, '$') + "_Click')";
                        window.setTimeout(postBackstr, 0, 'JavaScript');

                        blnRetVal = true;
                        return true;

                    }
                    else {
                        blnRetVal = false;
                        return false;
                    }
                }

            );
                blnRetVal = false;
                return false;

            }
            else {

                var postBackstr = "__doPostBack('" + id.replace(/_/gi, '$') + "','" + id.replace(/_/gi, '$') + "_Click')";
                window.setTimeout(postBackstr, 0, 'JavaScript');

                return true;
            }

        }

        function OnGetPersonListInFiveDays() {

            var staffids = document.getElementById("ctl00_MainContent_hdf_StaffIds").value;

            if (staffids != "") {
                GePersonNameTravelInFiveDays(staffids);
                alert(vpaxnamehtml);
            }



        }

        function ShowDiv() {
            var txtsearch= $('[id$=txtSearch]').val();
            $('[id$=txtSearch]').val($('[id$=txtSearchPax]').val());
            showModal('dvpopup');
         //   if ($('[id$=txtSearch]').val() != txtsearch)
                $('[id$=cmdGet]').click();
        }

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                 
                   <img src="../Images/loaderbar.gif"alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
            
   <div class="page-title">
       <asp:Label ID="lblPageTitle" runat="server" Text="New Travel Request"></asp:Label>
    
  </div>
 
    <input type="hidden" id="hdstaffid" name="hdstaffid" />
    <input type="hidden" id="hdpersonal" name="hdpersonal" />
    <div id="page-content" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
        border-bottom: 1px solid #cccccc; border-top: 0; min-height: 620px; overflow: auto;">
        <asp:ScriptManagerProxy ID="smp1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/TravelService.asmx" />
            </Services>
        </asp:ScriptManagerProxy>
        <div id="dvMain" style="padding: 5px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <uc1:uc_Vessel_List ID="Vessel_List" Width="150px" runat="server" />
                            </td>
                            <td style="text-align: right">
                                <asp:TextBox ID="txtSearchPax" runat="server" OnTextChanged="txtSearchPax_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                                <img src="Images/SearchAndAddPax.png" onclick="ShowDiv();" style="vertical-align: middle"
                                    alt="" />
                            </td>
                        </tr>
                    </table>
                    <div style="font-weight: bold; color: Black; padding-bottom: 5px; background-color: Yellow;
                        width: 80px;">
                        ON-Signers:</div>
                    <asp:GridView ID="GridView1" runat="server" Width="100%" OnRowDataBound="GridView1_RowDataBound"
                        AutoGenerateColumns="false" DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting">
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <Columns>
                            <asp:BoundField HeaderText="S/Code" DataField="Code" ItemStyle-Width="60px" />
                            <asp:BoundField HeaderText="Staff Name" DataField="Name" ItemStyle-Width="300px" />
                            <asp:BoundField HeaderText="Rank" DataField="Rank" ItemStyle-Width="50px" />
                            <asp:BoundField HeaderText="Nationality" DataField="Nationality" ItemStyle-Width="80px" />
                            <asp:BoundField HeaderText="D.O.B" DataField="DateOfBirth" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" />
                            <asp:BoundField HeaderText="Passport No." DataField="PPNo" ItemStyle-Width="80px" />
                            <asp:BoundField HeaderText="Passport Expiry" DataField="PPExpiry" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" />
                            <asp:BoundField HeaderText="Place of Issue" DataField="PlaceOfIssue" ItemStyle-Width="100px" />
                            <asp:BoundField HeaderText="Seaman Book No." DataField="SBNo" ItemStyle-Width="80px" />
                            <asp:BoundField HeaderText="S.B. Expiry" DataField="SBExpiry" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lbeventSts" runat="server" Text='<%#Eval("ON_OFF").ToString()=="1"?"ON-Signer":"OFF-Signer"%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Remove" DeleteImageUrl="~/Travel/Images/Delete.gif"
                                ShowDeleteButton="true" ButtonType="Image" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                    <br />
                    <div style="font-weight: bold; color: Black; padding-bottom: 5px; background-color: Yellow;
                        width: 80px;">
                        OFF-Signers:</div>
                    <asp:GridView ID="GridView2" runat="server" Width="100%" OnRowDataBound="GridView1_RowDataBound"
                        AutoGenerateColumns="false" DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting">
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <Columns>
                            <asp:BoundField HeaderText="S/Code" DataField="Code" ItemStyle-Width="60px" />
                            <asp:BoundField HeaderText="Staff Name" DataField="Name" ItemStyle-Width="300px" />
                            <asp:BoundField HeaderText="Rank" DataField="Rank" ItemStyle-Width="50px" />
                            <asp:BoundField HeaderText="Nationality" DataField="Nationality" ItemStyle-Width="80px" />
                            <asp:BoundField HeaderText="D.O.B" DataField="DateOfBirth" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" />
                            <asp:BoundField HeaderText="Passport No." DataField="PPNo" ItemStyle-Width="80px" />
                            <asp:BoundField HeaderText="Passport Expiry" DataField="PPExpiry" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" />
                            <asp:BoundField HeaderText="Place of Issue" DataField="PlaceOfIssue" ItemStyle-Width="100px" />
                            <asp:BoundField HeaderText="Seaman Book No." DataField="SBNo" ItemStyle-Width="80px" />
                            <asp:BoundField HeaderText="S.B. Expiry" DataField="SBExpiry" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lbeventSts" runat="server" Text='<%#Eval("ON_OFF").ToString()=="1"?"ON-Signer":"OFF-Signer"%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Remove" DeleteImageUrl="~/Travel/Images/Delete.gif"
                                ShowDeleteButton="true" ButtonType="Image" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                    <br />
                    <div id="Div1" style="border: 1px solid #cccccc; height: 20px; vertical-align: baseline;
                        background: url(../Images/bg.png) left -1024px repeat-x; color: Black; text-align: left;
                        padding: 2px; background-color: #F6CEE3; text-align: center; font-weight: bold">
                        <asp:Label ID="Label1" runat="server" Text="Flight Details"></asp:Label>
                    </div>
                    <div id="Div2" style="border-left: 1px solid #cccccc; border-right: 1px solid #cccccc;
                        border-bottom: 1px solid #cccccc; padding: 5px; overflow: auto;">
                        <table cellpadding="4px" cellspacing="0px" border="0">
                            <tr style="font-weight: bold;">
                                <td style="width: 100px">
                                    Travel Class
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbTravelClass" runat="server">
                                        <asp:ListItem Text="Economy" Value="Economy"></asp:ListItem>
                                        <asp:ListItem Text="Business" Value="Business"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkSeaman" runat="server" BorderWidth="0px" Text="Seaman?" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkReturn" runat="server" Text="Return" Visible="false" />
                                </td>
                            </tr>
                        </table>
                        <div id="dvFlightList">
                            <table class="grid-row-data" cellpadding="2px" cellspacing="0px" style="width: 100%;
                                text-align: left;">
                            </table>
                            <asp:GridView ID="GrdFlight" Width="98%" AutoGenerateColumns="False" runat="server"
                                BackColor="LightGray" CellSpacing="1" CellPadding="3" GridLines="None" ShowFooter="true"
                                OnRowDataBound="GrdFlight_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="From">
                                        <ItemTemplate>
                                            <ucAirPortList:AirPortList ID="txtFrom1" Text='<%#Eval("travelOrigin") %>' runat="server" />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To">
                                        <ItemTemplate>
                                            <ucAirPortList:AirPortList ID="txtTo1" Text='<%#Eval("travelDestination") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Departure Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDepDate1" runat="server" Text='<%#Eval("departureDate") %>' Width="100px"></asp:TextBox>
                                             <ajaxToolkit:CalendarExtender ID="CEDepDate" runat="server" TargetControlID="txtDepDate1"
                                                Format="dd-MMM-yyyy">
                                            </ajaxToolkit:CalendarExtender>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtDepDate1" ControlToValidate="txtDepDate1"
                                                ValidationGroup="vDeptdt" Display="None"   runat="server" ErrorMessage="Please select Date !"></asp:RequiredFieldValidator>

                                          

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Preferred Time(Hrs&nbsp;Min)">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbDepHours1" DataSourceID="ObjectDataSourceHour" DataTextField="HrText"
                                                DataValueField="HrValue" runat="server" />
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorcmbDepHours1" ControlToValidate="cmbDepHours1"
                                                ValidationGroup="vDeptdt" Display="None"   runat="server" ErrorMessage="Please select hour !"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidatorcmbDepHours1">
                                            </cc1:ValidatorCalloutExtender>--%>
                                            <asp:DropDownList ID="cmbDepMins1" DataSourceID="ObjectDataSourceMinute" DataTextField="MnText"
                                                DataValueField="MnValue" runat="server" />
                                         <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cmbDepMins1"
                                                  ValidationGroup="vDeptdt" Display="None" runat="server" ErrorMessage="Please select minute !"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
                                            </cc1:ValidatorCalloutExtender>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="rdoReturn1" runat="server" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rdoReturn_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="One Way" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Return" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Add New" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtndeleteFlight" runat="server" ImageUrl="~/Images/delete.gif"
                                                OnClick="imgbtndeleteFlight_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="RowStyle-css" BackColor="WhiteSmoke" />
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <FooterStyle BackColor="WhiteSmoke" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ObjectDataSourceHour" TypeName="SMS.Business.TRAV.BLL_TRV_QuoteRequest"
                                SelectMethod="AddHour" runat="server"></asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ObjectDataSourceMinute" TypeName="SMS.Business.TRAV.BLL_TRV_QuoteRequest"
                                SelectMethod="AddMinute" runat="server"></asp:ObjectDataSource>
                        </div>
                    </div>
                    <br />
                    <div>
                        Remarks:<br />
                        <asp:TextBox ID="txtRequestRemarks" Font-Size="11px" runat="server" Width="99%" TextMode="MultiLine"
                            Font-Names="Tahoma" Height="80px"></asp:TextBox>
                    </div>
                    <center>
                        <asp:Button ID="cmdSaveRequest" runat="server" Text="Save Request" Width="100px"
                            ValidationGroup="vDeptdt" OnClientClick="return OnCheckPersonTravelledInFiveDays(id)"
                            OnClick="cmdSaveRequest_Click" />
                              <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                    ShowSummary="false" ValidationGroup="vDeptdt" />
                        <asp:HiddenField ID="hdf_StaffIds" runat="server" />
                    </center>
                    <asp:Literal ID="ltText" runat="server" Text=""></asp:Literal>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="dvpopup" title="Add Pax" style="width: 900px; display: none;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table cellpadding="1" cellspacing="1" style="text-align: center; width: 100%; background-color: White;">
                        <caption style="background-color: #32426F; font-weight: bold; color: White;">
                            Search - Staff code/Name/Rank
                        </caption>
                        <tr style="font-weight: bold;">
                            <td style="width: 100px">
                                <span>Search</span>
                            </td>
                            <td style="text-align: left">
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="cmdGet">
                                    <asp:TextBox ID="txtSearch" runat="server" Text="" Width="260px" Height="15px"></asp:TextBox>
                                    <asp:ImageButton ID="cmdGet" runat="server" ImageUrl="Images/Search.png" OnClick="cmdGet_Click"
                                        ImageAlign="AbsMiddle" />
                                    <%--<asp:Button ID="cmdGet" runat="server" Width="100px" Text="Go->" OnClick="cmdGet_Click" />--%>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div id="dvPaxList" style="max-height: 500px; min-height: 400px; background-color: #FFF;
                        overflow: auto;">
                        <asp:GridView ID="GrdCrew" Width="98%" AutoGenerateColumns="False" runat="server"  EmptyDataText="No Records Found !!"
                            CellPadding="6" CellSpacing="6" OnRowCommand="GrdCrew_RowCommand" OnRowDataBound="GrdCrew_RowDataBound"
                            AllowPaging="true" PageSize="15" OnPageIndexChanging="GrdCrew_PageIndexChanging">
                            <HeaderStyle HorizontalAlign="Left" CssClass="grid-row-header" />
                            <SelectedRowStyle BackColor="#f8f8f8" ForeColor="Red" />
                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode" onclick='window.open("../crew/crewdetails.aspx?id=<%#Eval("id") %>")'
                                            runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Staff_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Staff_FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRank" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Rank_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Passport No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSPPNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Passport_Number") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expiry Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSPPExpiry" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Passport_Expiry_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PlaceOf Issue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSPPO" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Passport_PlaceOf_Issue") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DOB">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSDOB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Staff_Birth_Date", "{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false" HeaderText="Sel">
                                    <ItemTemplate>
                                        <input type="checkbox" id='chk<%#DataBinder.Eval(Container.DataItem, "id") %>' onclick="addStaffToList(this)"
                                            style="border: 0px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgSelect" runat="server" CommandName="ADDPAX" Height="22px"
                                            Width="22px" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "ID") %>'
                                            CssClass="clickable" ToolTip="Add pax" ImageUrl="~/Travel/images/add-pax.png" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgAddnClose" runat="server" CommandName="ADDPAXANDCLOSE" Height="22px"
                                            Width="22px" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "ID") %>'
                                            CssClass="clickable" ToolTip="Add pax and Close" ImageUrl="~/Travel/images/add-n-close-pax.png" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                    <center>
                        <asp:Button ID="cmdClose" runat="server" Text="Close" OnClientClick="hideModal('dvpopup');" />
                    </center>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:ObjectDataSource ID="objCrew" runat="server" SelectMethod="Get_SearchCrew" TypeName="SMS.Business.TRAV.BLL_TRV_TravelRequest">
            <SelectParameters>
                <asp:Parameter Name="crewid" Type="Int32" DefaultValue="0" />
                <asp:Parameter DefaultValue="0" Name="userid" Type="Int32" />
                <asp:Parameter DefaultValue="" Name="Search_Text" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
