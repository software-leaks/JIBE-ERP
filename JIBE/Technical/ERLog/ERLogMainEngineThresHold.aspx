<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ERLogMainEngineThresHold.aspx.cs" Inherits="ERLogMainEngineThresHold" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ViewEdit(Id) {
            var value = document.getElementById('<%=lblLogId.ClientID%>').value;
            window.open("ERLogEdit.aspx?ViewID=" + Id + "&LOGID=" + value, "Test", "", "");
        }
        function MaskMoney(evt) {
            if (!(evt.keyCode == 9 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 37 || evt.keyCode == 39 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105))) {
                return false;
            }
            var parts = evt.srcElement.value.split(',');

            if (parts.length > 2) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
           
        }
    </script>
     <style type="text/css">
        .CellClass1
        {
            background-color:#FA5858;
            color:White;
        }
        .CellClass0
        {
        }
         .HeaderCellColor
        {
            background-color:#3399CC;
            color: White;
        }
         .HeaderCellColor1
        {
            background-color: #BCF5A9;
            color:Black;
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
    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
        height: 100%;">
        <div id="page-header" class="page-title">
          
              <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 95%; text-align: center;">
                          <b> MAIN ENGINES  THRESHOLD </b>
                    </td>
                    <td style="width: 5%; text-align: right; border-right: 2px solid Transparent">
                        <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" BackColor="#AED7FF"
                            BorderStyle="None" Width="70px" Font-Size="11px" Font-Bold="false" Font-Names="verdana"
                            Height="20px" ForeColor="Blue" />
                    </td>
                </tr></table>
                 <asp:TextBox ID="lblLogId" runat="server"></asp:TextBox>
        </div>
     
    </div>
    <div style="text-align: left; overflow: scroll" id="dvPageContent" class="page-content-main">
        <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
            border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
            <tr>
                <td style="width: 100%; border-right: solid; border-right-color: Gray; border-right-width: 1px"
                    align="left" valign="top">
                    <asp:FormView ID="FormView1" runat="server" Height="60px" Width="100%" BorderWidth="0px"
                        Font-Size="Small" OnDataBound="FormView1_DataBound">
                        <RowStyle CssClass="PMSGridRowStyle-css" Height="18" />
                        <ItemTemplate>
                            <table cellspacing="0" cellpadding="3" border="0.5" style="background-color: White;
                                border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                               
                                <tr>
                                    <td width="100%" colspan="3" valign="top">
                                        <asp:Repeater ID="rpEngine2" runat="server">
                                            <HeaderTemplate>
                                                <table cellspacing="1" cellpadding="3" rules="all" border="3" style="background-color: White;
                                                    border-color: #efefef; width: 100%;">
                                                    <tr  class = "HeaderCellColor1" >
                                                        <td colspan="31" align="center">
                                                            MAIN ENGINES
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr align="center"  class = "HeaderCellColor">
                                                        <td rowspan="4">
                                                            
                                                        </td>
                                                        <td colspan="7">
                                                            TEMPERATURE C
                                                        </td>
                                                      <td rowspan="4">
                                                            <label id="Label176" class="verticaltext5">
                                                                FUEL VISC
                                                            </label>
                                                        </td>
                                                        <td colspan="12">
                                                            HELTH EXCHANGERS TEMPERATURES C
                                                        </td>
                                                        <td colspan="9">
                                                            PRESSURE kg/cm2
                                                        </td>
                                                    </tr>
                                                    <tr align="center"  class = "HeaderCellColor">
                                                        <td colspan="2">
                                                            MAIN BEARING
                                                        </td>
                                                        <td colspan="2">
                                                            JACKET COOLING
                                                        </td>
                                                        <td colspan="2">
                                                            PISTON COOLING
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label190" class="verticaltext">
                                                                FULE OIL</label>
                                                        </td>
                                                         <td colspan="4">
                                                            JACKET COOLER
                                                        </td>
                                                        <td colspan="4">
                                                            L O COOLER
                                                        </td>
                                                        <td colspan="4">
                                                            PISTON COOLER
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label191" class="verticaltext">
                                                                Sea Water</label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label192" class="verticaltext">
                                                                Jaket Water</label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label193" class="verticaltext">
                                                                Bearing & X-hd LO</label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label194" class="verticaltext">
                                                                Canshaft LO</label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label195" class="verticaltext">
                                                                F V Cooling</label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label196" class="verticaltext">
                                                                Fuel Oil</label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label197" class="verticaltext">
                                                                Piston Cooling</label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label198" class="verticaltext">
                                                                Control Air</label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label199" class="verticaltext">
                                                                Service Air
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center"  class = "HeaderCellColor">
                                                        <td rowspan="2">
                                                            IN
                                                        </td>
                                                        <td rowspan="2">
                                                            OUTLET
                                                        </td>
                                                        <td rowspan="2">
                                                            IN
                                                        </td>
                                                        <td rowspan="2">
                                                            OUTLET
                                                        </td>
                                                        <td rowspan="2">
                                                            IN
                                                        </td>
                                                        <td rowspan="2">
                                                            OUTLET
                                                        </td>
                                                      
                                                     <td colspan="2">
                                                            SW
                                                        </td>
                                                        <td colspan="2">
                                                            FW
                                                        </td>
                                                        <td colspan="2">
                                                            SW
                                                        </td>
                                                        <td colspan="2">
                                                            LO
                                                        </td>
                                                        <td colspan="2">
                                                            SW
                                                        </td>
                                                        <td colspan="2">
                                                            SW/FW
                                                        </td></tr>
                                                    <tr align="center"  class = "HeaderCellColor">
                                                      
                                                      
                                                         <td width="30px">
                                                            IN
                                                        </td>
                                                        <td width="30px">
                                                            OUT
                                                        </td>
                                                        <td width="30px">
                                                            IN
                                                        </td>
                                                        <td width="30px">
                                                            OUT
                                                        </td>
                                                        <td width="30px">
                                                            IN
                                                        </td>
                                                        <td width="30px">
                                                            OUT
                                                        </td>
                                                        <td width="30px">
                                                            IN
                                                        </td>
                                                        <td width="30px">
                                                            OUT
                                                        </td>
                                                        <td width="30px">
                                                            IN
                                                        </td>
                                                        <td width="30px">
                                                            OUT
                                                        </td>
                                                        <td width="30px">
                                                            IN
                                                        </td>
                                                        <td width="30px">
                                                            OUT
                                                        </td>                                                       
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                 <tr style="border: 1px solid ActiveBorder">
                                                    <td align="left" style="height: 19px;background-color: #BCF5A9"">
                                                        <asp:Label ID="Label159" runat="server" Text='Min' Width ="50px"> </asp:Label>
                                                        <asp:Label ID="lblid" Width="60px" runat="server" Visible="false" Text=''> </asp:Label>
                                                        <asp:Label ID="lblLogId" Width="60px" runat="server" Visible="false" Text=''> </asp:Label>
                                                        <asp:Label ID="lblVessel" Width="60px" runat="server" Visible="false" Text=''> </asp:Label>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt1" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "MB_IN_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt2" runat="server"   Width ="30px"  CssClass="input centeralinment"  MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_Min")%>'> </asp:TextBox>
                                                    </td>
                                                   
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt3" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "JC_IN_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt4" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_Min")%>'> </asp:TextBox>
                                                    </td>
                                                   
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt5" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "PC_IN_min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt6" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_Min")%>'> </asp:TextBox>
                                                    </td>
                                                   
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt7" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "FUEL_OIL_Min")%>'></asp:TextBox>
                                                    </td>
                                                  <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt8" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "FUEL_VISC_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt9" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "JC_SW_IN_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt10" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "JC_SW_OUT_MIN")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt11" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "JC_FW_IN_MIN")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt12" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "JC_FW_OUT_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt13" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "LC_SW_IN_MIN")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt14" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "LC_SW_OUT_MIN")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt15" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "LC_LO_IN_MIN")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt16" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "LC_LO_OUT_MIN")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt17" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "PC_SW_IN_MIN")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt18" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "PC_SW_OUT_MIN")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt19" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "PC_LO_IN_MIN")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt20" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "PC_LO_OUT_MIN")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt21" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_SEA_WATER_MIN")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt22" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_JACKET_WATER_MIN")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt23" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_BEARING_XND_LO_MIN")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt24" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_CAMSHAFT_LO_MIN")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt25" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_FV_COOLING_MIN")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt26" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_FUEL_OIL_MIN")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt27" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_PISTON_COOLING_MIN")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt28" runat="server"   Width ="30px"  CssClass="input centeralinment"  MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_CONTROL_AIR_MIN")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt29" runat="server"   Width ="30px"  CssClass="input centeralinment"  MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_SERVICE_AIR_MIN")%>'></asp:TextBox>
                                                    </td>
                                                </tr>
                                                   <tr style="border: 1px solid ActiveBorder">
                                                    <td align="left" style="height: 19px; background-color: #F78181"">
                                                        <asp:Label ID="Label1" runat="server" Text='Max'> </asp:Label>                                                       
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt01" runat="server"   Width ="30px"  CssClass="input centeralinment"  MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "MB_IN_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt02" runat="server"   Width ="30px"  CssClass="input centeralinment"  MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "MB_OUT_Max")%>'> </asp:TextBox>
                                                    </td>
                                                   
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt03" runat="server"   Width ="30px"  CssClass="input centeralinment"  MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "JC_IN_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt04" runat="server"   Width ="30px"  CssClass="input centeralinment"  MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "JC_OUT_Max")%>'> </asp:TextBox>
                                                    </td>
                                                   
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt05" runat="server"   Width ="30px"  CssClass="input centeralinment"  MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "PC_IN_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt06" runat="server"   Width ="30px"  CssClass="input centeralinment"  MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "PC_OUT_Max")%>'> </asp:TextBox>
                                                    </td>
                                                   
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt07" runat="server"   Width ="30px"  CssClass="input centeralinment"  MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "FUEL_OIL_Max")%>'></asp:TextBox>
                                                    </td>
                                                  <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt08" runat="server"   Width ="30px"  CssClass="input centeralinment"  MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "FUEL_VISC_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt09" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "JC_SW_IN_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt010" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "JC_SW_OUT_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt011" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "JC_FW_IN_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt012" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "JC_FW_OUT_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt013" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "LC_SW_IN_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt014" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "LC_SW_OUT_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt015" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "LC_LO_IN_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt016" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "LC_LO_OUT_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt017" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "PC_SW_IN_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt018" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "PC_SW_OUT_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt019" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "PC_LO_IN_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt020" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "PC_LO_OUT_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt021" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_SEA_WATER_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt022" runat="server"   Width ="30px"  CssClass="input centeralinment"  MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "P_JACKET_WATER_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt023" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_BEARING_XND_LO_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt024" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_CAMSHAFT_LO_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt025" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_FV_COOLING_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt026" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_FUEL_OIL_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt027" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_PISTON_COOLING_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt028" runat="server"   Width ="30px"  CssClass="input centeralinment"  MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_CONTROL_AIR_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt029" runat="server"   Width ="30px"  CssClass="input centeralinment" MaxLength="4" onKeydown="JavaScript:return MaskMoney(event);"  Text='<%# DataBinder.Eval(Container.DataItem, "P_SERVICE_AIR_Max")%>'></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                    
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
