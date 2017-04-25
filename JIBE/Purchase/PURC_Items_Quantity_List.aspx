<%@ Page Title="Min-Max Quantity" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PURC_Items_Quantity_List.aspx.cs" Inherits="Purchase_PURC_Items_Quantity_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .tdh
        {
            font-size: 12px;
            text-align: right;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
        }
        .tdd
        {
            font-size: 12px;
            text-align: left;
            height: 20px;
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">




        function Onfail(retval) {
            alert(retval._message);


        }


        ///////////////////////////////

        var lastExecutorMinMaxQty_M = null;

        var UPD_Item_Quantity_Vessel_ID_M, UPD_Item_Quantity_Item_Ref_Code_M, UPD_Item_Quantity_ID_M, UPD_Min_Qty_ID, UPD_Max_Qty_ID, UPD_Eff_Date_ID, UPD_Item_Quantity_USER_ID_M;

        function ShowdivUPD_Item_Quantity_M(Vessel_ID, Item_Ref_Code, ID, USER_ID, evt, objthis) {

            SetBackgroundColor_selecteddRow("transparent");

            UPD_Min_Qty_ID = objthis.id.replace(/imgEdit/, 'lblMinQty');
            UPD_Max_Qty_ID = objthis.id.replace(/imgEdit/, 'lblMaxQty');
            UPD_Eff_Date_ID = objthis.id.replace(/imgEdit/, 'lblEffectiveDate');

            document.getElementById('txtNewMinQty').value = document.getElementById(UPD_Min_Qty_ID).innerHTML;
            document.getElementById('txtNewMaxQty').value = document.getElementById(UPD_Max_Qty_ID).innerHTML;
            document.getElementById('ctl00_MainContent_txtNewEffDate').value = document.getElementById(UPD_Eff_Date_ID).innerHTML;

            SetBackgroundColor_selecteddRow("yellow");


            UPD_Item_Quantity_Vessel_ID_M = Vessel_ID;
            UPD_Item_Quantity_Item_Ref_Code_M = Item_Ref_Code;
            UPD_Item_Quantity_ID_M = ID;
            UPD_Item_Quantity_USER_ID_M = USER_ID

            document.getElementById('dvChangeMinmaxQty_M').style.display = "block";
            SetPosition_Relative(evt, 'dvChangeMinmaxQty_M');

        }


        function SetBackgroundColor_selecteddRow(color) {

            if (UPD_Min_Qty_ID) {
                document.getElementById(UPD_Min_Qty_ID).style.background = color;
                document.getElementById(UPD_Max_Qty_ID).style.background = color;
                document.getElementById(UPD_Eff_Date_ID).style.background = color;
            }

        }



        function asyncUPD_Item_Quantity_M() {

            if (lastExecutorMinMaxQty_M != null)
                lastExecutorMinMaxQty_M.abort();

            var newMinQty = document.getElementById('txtNewMinQty').value;
            var newMaxQty = document.getElementById('txtNewMaxQty').value;
            var newEffDate = document.getElementById('ctl00_MainContent_txtNewEffDate').value;

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncUPD_Item_Quantity', false, { "Vessel_ID": UPD_Item_Quantity_Vessel_ID_M, "Item_Ref_Code": UPD_Item_Quantity_Item_Ref_Code_M, "ID": UPD_Item_Quantity_ID_M, "Min_Qty": newMinQty, "Max_Qty": newMaxQty, "Effective_Date": newEffDate, "User_ID": UPD_Item_Quantity_USER_ID_M }, onSuccasyncUPD_Item_Quantity_M, Onfail, new Array(newMinQty, newMaxQty, newEffDate));

            lastExecutorMinMaxQty_M = service.get_executor();

        }

        function onSuccasyncUPD_Item_Quantity_M(retVal, Args) {



            if (retVal == "1") {
                document.getElementById('dvChangeMinmaxQty_M').style.display = "none";
                SetBackgroundColor_selecteddRow("transparent");

                document.getElementById(UPD_Min_Qty_ID).innerHTML = Args[0];
                document.getElementById(UPD_Max_Qty_ID).innerHTML = Args[1];
                document.getElementById(UPD_Eff_Date_ID).innerHTML = Args[2];

            }
            else if (retVal == "2") {

                document.getElementById('dvChangeMinmaxQty_M').style.display = "none";
                SetBackgroundColor_selecteddRow("transparent");

                document.getElementById("ctl00_MainContent_btnSearch").click();


            }

            else {
                alert('Error occured during update !');
            }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="page-title">
        Min-Max Quantity
    </div>
    <div class="page-content">
        <asp:UpdatePanel ID="updFilter" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
            <ContentTemplate>
                <table cellspacing="0" width="100%" cellpadding="5px" style="border: 1px solid  #cccccc">
                    <tr>
                        <td>
                            <table align="center" cellpadding="5" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td class="tdh">
                                        Fleet :
                                    </td>
                                    <td class="tdd">
                                        <auc:CustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                            Height="150" Width="160" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdh">
                                        Vessel :
                                    </td>
                                    <td class="tdd">
                                        <auc:CustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" Height="200"
                                            Width="160" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 2px">
                            &nbsp
                        </td>
                        <td>
                            <table align="center" cellpadding="5" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td class="tdh">
                                        Department Type:
                                    </td>
                                    <td class="tdd">
                                        <asp:RadioButtonList ID="rbtnDeptType" runat="server" Width="200px" RepeatDirection="Horizontal"
                                            AutoPostBack="True" OnSelectedIndexChanged="rbtnDeptType_SelectedIndexChanged"
                                            ForeColor="Black" TabIndex="2">
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdh">
                                        Department :
                                    </td>
                                    <td class="tdd">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" Width="160px"
                                            OnSelectedIndexChanged="ddlDepartment_OnSelectedIndexChanged" Font-Size="12px"
                                            TabIndex="3" AppendDataBoundItems="True">
                                            <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table align="center" cellpadding="5" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td class="tdh">
                                        Catalogue :
                                    </td>
                                    <td class="tdd">
                                        <asp:DropDownList ID="ddlCatalogue" runat="server" AutoPostBack="true" Width="160px"
                                            OnSelectedIndexChanged="ddlCatalogue_OnSelectedIndexChanged" Font-Size="12px"
                                            TabIndex="3" AppendDataBoundItems="True">
                                            <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdh">
                                        Sub Catalogue :
                                    </td>
                                    <td class="tdd">
                                        <asp:DropDownList ID="ddlSubCatalogue" runat="server" Width="160px" Font-Size="12px"
                                            TabIndex="3" AppendDataBoundItems="True">
                                            <asp:ListItem Selected="True" Value="0">--SELECT ALL--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table align="center" cellpadding="5" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td class="tdh">
                                        Search Text :
                                    </td>
                                    <td class="tdd">
                                        <asp:TextBox ID="txtSearchItems" Width="150px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdd">
                                        <asp:CheckBox ID="chkLatest" ForeColor="Blue" runat="server" Text="Latest" Checked="true" />
                                    </td>
                                    <td align="left" valign="bottom">
                                        <asp:Button ID="btnSearch" ToolTip="Search" runat="server" Width="100px" Text="Search" OnClick="btnSearch_Click" />
                                        &nbsp;
                                        <asp:ImageButton ToolTip="Export To Excel" ID="btnExport" ImageUrl="~/Images/XLS.jpg" Height="25px" OnClick="btnExport_Click"
                                            ImageAlign="AbsBottom" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updGrid" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <table width="100%" style="border: 1px solid  #cccccc; margin-top: 2px">
                    <tr>
                        <td>
                            <asp:GridView ID="gvPurcItems" runat="server" AutoGenerateColumns="false" Width="100%"
                                EmptyDataText="No record found !" EmptyDataRowStyle-ForeColor="Maroon" CellPadding="5"
                                CssClass="GridView-css" GridLines="None">
                                <Columns>
                                    <asp:BoundField DataField="Vessel_Name" HeaderText="Vessel" />
                                    <asp:BoundField DataField="System_Description" ItemStyle-Width="150px" HeaderText="Catalogue" />
                                    <asp:BoundField DataField="Subsystem_Description" ItemStyle-Width="150px" HeaderText="Sub Catalogue" />
                                    <asp:BoundField DataField="Drawing_Number" HeaderText="Draw. No." />
                                    <asp:BoundField DataField="Part_Number" HeaderText="Part Number" />
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesc" Text='<%#Convert.ToString(Eval("Short_Description")).Length>50?Convert.ToString(Eval("Short_Description")).Substring(0,49)+"..." : Convert.ToString(Eval("Short_Description")) %>'
                                                onmousemove='<%# "js_ShowToolTip(&#39;<b>Short Desc :</b>"+Convert.ToString(Eval("Short_Description"))+"<br><b>Long Desc :</b>"+Convert.ToString(Eval("Long_Description"))+"&#39;,event,this)" %>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Unit_and_Packings" ItemStyle-HorizontalAlign="Center"
                                        HeaderText="Unit" />
                                    <asp:TemplateField HeaderText="Min Qty" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMinQty" Text='<%#Eval("Min_Qty")%>' Style="padding: 0px 2px 0px 2px;
                                                background-color: transparent; color: Navy" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Max Qty" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaxQty" Text='<%#Eval("Max_Qty") %>' Style="padding: 0px 2px 0px 2px;
                                                background-color: transparent; color: Navy" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Effective Date" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEffectiveDate" Text='<%#Eval("Effective_Date") %>' Style="padding: 0px 2px 0px 2px;
                                                background-color: transparent; color: Navy" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Image ID="imgEdit" onclick='<%# "ShowdivUPD_Item_Quantity_M("+Eval("Vessel_ID").ToString()+",&#39;"+Eval("Item_Intern_Ref").ToString()+"&#39;,"+Eval("ID").ToString()+","+ Session["userid"].ToString()+",event,this)" %>'
                                                ImageUrl="~/Images/edit.gif" runat="server" AlternateText="edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;PURC_DTL_Items_MinMax&#39;,&#39; id="+Eval("id")+" and Vessel_id="+Eval("Vessel_id")+"&#39;,event,this)" %>'
                                                AlternateText="info" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            </asp:GridView>
                            <auc:CustomPager ID="ucpItems" OnBindDataItem="BindItems" runat="server" />
                        </td>
                    </tr>
                </table>
                <div id="dvChangeMinmaxQty_M" style="display: none; border: 4px solid orange; background-color: #FFFFFF;
                    position: absolute; z-index: 100; border-radius: 3px; width: 200px">
                    <table cellpadding="2" cellspacing="0" style="margin: 3px; width: 198px">
                        <tr>
                            <td class="tdh">
                                Min Qty :
                            </td>
                            <td class="tdd">
                                <input type="text" id="txtNewMinQty" style="width: 85px; height: 16px; background-color: #FFFFFF;
                                    border: 1px solid #D3D3D3" maxlength="10" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdh">
                                Max Qty :
                            </td>
                            <td class="tdd">
                                <input type="text" id="txtNewMaxQty" style="width: 85px; height: 16px; background-color: #FFFFFF;
                                    border: 1px solid #D3D3D3" maxlength="10" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdh">
                                Effct. Date :
                            </td>
                            <td class="tdd">
                                <asp:TextBox ID="txtNewEffDate" runat="server" Style="width: 85px; height: 16px;
                                    background-color: #FFFFFF; border: 1px solid #D3D3D3" MaxLength="10"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalNewEffDate" runat="server" TargetControlID="txtNewEffDate"
                                    Format="dd-MM-yyyy">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" colspan="2" style="text-align: right; padding-right: 12px; padding-top: 3px">
                                <img src="../Images/save.gif" onclick="asyncUPD_Item_Quantity_M()" title="Save" alt="save" />
                                &nbsp;
                                <img src="../Images/close.gif" title="Cancel" onclick=" SetBackgroundColor_selecteddRow('transparent');document.getElementById('dvChangeMinmaxQty_M').style.display = 'none';"
                                    alt="close" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
