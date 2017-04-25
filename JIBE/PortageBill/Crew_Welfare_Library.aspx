<%@ Page Title="Crew Welfare Library" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Crew_Welfare_Library.aspx.cs" Inherits="PortageBill_Crew_Welfare_Library" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">


        function ShowDetails(Vessel_Name, Welfare_ID, Effective_Date, Welfare_Amount) {

            if (Vessel_Name == null) {
                $(document.getElementById("<%=lblVesselName.ClientID%>")).hide();
                $(document.getElementById("<%=ddlVesselUpd.ClientID%>")).show();
                document.getElementById("<%=hdfWelfare_ID.ClientID%>").value = 0;
                document.getElementById("<%=ddlVesselUpd.ClientID%>").value = 0;
                document.getElementById("<%=txtWelfareAmount.ClientID%>").value = "";
                document.getElementById("<%=txtEffectiveDate.ClientID%>").value = "";
            }
            else {
                $(document.getElementById("<%=lblVesselName.ClientID%>")).show();
                document.getElementById("<%=lblVesselName.ClientID%>").innerHTML = Vessel_Name;
                $(document.getElementById("<%=ddlVesselUpd.ClientID%>")).hide();

                document.getElementById("<%=hdfWelfare_ID.ClientID%>").value = Welfare_ID;
                document.getElementById("<%=txtWelfareAmount.ClientID%>").value = Welfare_Amount;
                document.getElementById("<%=txtEffectiveDate.ClientID%>").value = Effective_Date;
            }
            showModal('dvWelfare');
            document.getElementById('dvWelfare').title = "Welfare Details"
            return false;
        }

       
    </script>
    <style type="text/css">
        .tdh
        {
            font-size: 12px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
        }
        .tdd
        {
            font-size: 12px;
            text-align: left;
            padding: 0px 2px 0px 3px;
            height: 20px;
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Crew Welfare Amount
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
    <div class="page-content">
        <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:UpdatePanel ID="updFilter" runat="server">
                    <ContentTemplate>
                        <table width="70%" cellpadding="0" cellspacing="0" style="margin: 10px 0px 10px 0px">
                            <tr>
                                <td class="tdh">
                                    Fleet
                                </td>
                                <td class="tdd">
                                    <asp:DropDownList ID="ddlFleet" runat="server" Width="156px" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td class="tdh">
                                    Vessel
                                </td>
                                <td class="tdd">
                                    <asp:DropDownList ID="ddlVessel" runat="server" Width="156px" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td class="tdh">
                                    Effective Date
                                </td>
                                <td class="tdd">
                                    <asp:DropDownList ID="ddlEectiveDates" Width="150px" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                                </td>
                                <td>
                                    <asp:Image ID="btnAddNew" runat="server" ToolTip="Add welfare Details" ImageUrl="~/images/add.GIF"
                                        onclick="ShowDetails()" Width="16px" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="updData" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <table width="70%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvWelfare" runat="server" AutoGenerateColumns="false" CellPadding="5"
                                        Width="100%" GridLines="None" CssClass="GridView-css">
                                        <Columns>
                                            <asp:BoundField DataField="Vessel_Name" HeaderText="Vessel" />
                                            <asp:BoundField DataField="Welfare_Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="Effective_Date" HeaderText="Effective Date" ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgEdit" ToolTip="Edit" runat="server" onclick='<%# "ShowDetails(&#39;"+Eval("Vessel_Name").ToString()+"&#39;,"+Eval("Welfare_ID")+",&#39;"+Eval("Effective_Date")+"&#39;,"+Eval("Welfare_Amount")+")" %>'
                                                        ImageAlign="Bottom" Style="cursor: pointer;" ImageUrl="~/Images/edit.gif" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle ForeColor="Maroon"></EmptyDataRowStyle>
                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    </asp:GridView>
                                    <auc:CustomPager ID="pagerWf" runat="server" OnBindDataItem="BindItems" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="width: 400px; display: none" title="Welfare Details" id="dvWelfare">
                    <table style="width: 100%; margin: 14px" cellpadding="5">
                        <tr>
                            <td>
                                Vessel Name
                            </td>
                            <td>
                                <asp:Label ID="lblVesselName" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlVesselUpd" runat="server" Width="156px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlVesselUpd" runat="server" Display="None" ErrorMessage="Required Field!"
                                    ValidationGroup="savewelfare" ControlToValidate="ddlVesselUpd" InitialValue="0"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="reqddlVesselUpd">
                                </cc1:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount
                            </td>
                            <td>
                                <asp:TextBox ID="txtWelfareAmount" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqTxtAmt" runat="server" ErrorMessage="Required Field!"
                                    ValidationGroup="savewelfare" Display="None" ControlToValidate="txtWelfareAmount"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="amvtxtWelfareAmount" runat="server" Type="Double" Operator="DataTypeCheck"
                                    ValidationGroup="savewelfare" ControlToValidate="txtWelfareAmount" ErrorMessage="Please enter a valid amount."
                                    Display="None">
                                </asp:CompareValidator>
                                <cc1:ValidatorCalloutExtender ID="vcotxtWelfareAmount" runat="server" TargetControlID="amvtxtWelfareAmount">
                                </cc1:ValidatorCalloutExtender>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="reqTxtAmt">
                                </cc1:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Effective Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtEffectiveDate" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="calEfftv" runat="server" TargetControlID="txtEffectiveDate"
                                    Format="dd-MM-yyyy">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnSaveDetails" runat="server" ClientIDMode="Static" Text="Save"
                                    OnClick="btnSaveDetails_Click" />
                                <asp:HiddenField ID="hdfWelfare_ID" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script type="text/javascript">
            $(document).ready(function () {
                $("body").on("click", "#<%=btnSaveDetails.ClientID %>", function () {
                    var MSG = "";

                    if ($("#<%=ddlVesselUpd.ClientID %>").is(":visible")) {
                        if ($("#<%=ddlVesselUpd.ClientID %> option:selected").val() == "0") {
                            MSG = "Select Vessel\n"
                        }
                    }

                    if ($.trim($("#<%=txtWelfareAmount.ClientID %>").val()) == "") {
                        MSG += "Enter Amount\n";
                    }
                    if ($.trim($("#<%=txtEffectiveDate.ClientID %>").val()) == "") {
                        MSG += "Enter Effective Date\n";
                    }
                    else {
                        if (IsInvalidDate($("#<%=txtEffectiveDate.ClientID %>").val(), '<%=DFormat %>')) {
                            MSG += "Enter valid Effective Date<%=UDFLib.DateFormatMessage() %>";
                        }
                    }

                    if (MSG != "") {
                        alert(MSG);
                        return false;
                    }
                });
            });
        </script>
    </div>
</asp:Content>
