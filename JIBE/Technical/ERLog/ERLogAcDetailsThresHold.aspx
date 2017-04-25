<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ERLogAcDetailsThresHold.aspx.cs" Inherits="ERLogAcDetailsThresHold" %>

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

        function MaskMoney_Minus(evt) {
            if (!(evt.keyCode == 189 || evt.keyCode == 9 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 37 || evt.keyCode == 39 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105))) {
                return false;
            }
            var parts = evt.srcElement.value.split(',');
            
            if (evt.srcElement.value.length > 0 && evt.keyCode == 189) return false;
            if (parts.length > 2) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
           
        }
    </script>
      <style type="text/css">
        .CellClass1
        {
            background-color: Red;
            color: White;
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        .CellClass0
        {
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        
        .HeaderCellColor
        {
            background-color: #3399CC;
            color: White;
        }
        .HeaderCellColor1
        {
            background-color: #BCF5A9;
            color: Black ;
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
                        <b>ENGINE ROOM LOG BOOK THRESHOLD </b>
                    </td>
                    <td style="width: 5%; text-align: right; border-right: 2px solid Transparent">
                        <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" BackColor="#AED7FF"
                            BorderStyle="None" Width="70px" Font-Size="11px" Font-Bold="false" Font-Names="verdana"
                            Height="20px" ForeColor="Blue" />
                    </td>
                </tr>
            </table>
             <asp:TextBox ID="lblLogId" runat="server"></asp:TextBox>
        </div>     
    </div>
    <div style="text-align: left; overflow: scroll" id="dvPageContent" class="page-content-main">
        <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
            border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
            <tr>
                <td style="width: 100%; border-right: solid; border-right-color: Gray; border-right-width: 1px"
                    align="center" valign="top">
                    <asp:FormView ID="FormView1" runat="server" Height="60px" Width="80%" BorderWidth="0px"
                        Font-Size="Small" OnDataBound="FormView1_DataBound">
                        <RowStyle CssClass="PMSGridRowStyle-css" Height="18" />
                        <ItemTemplate>
                            <table cellspacing="0" cellpadding="3" border="0.5" style="background-color: White;
                                border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                <tr>
                                    <td width="100%" valign="top">
                                        <asp:Repeater ID="rpEngine3" runat="server">
                                            <HeaderTemplate>
                                                <table cellspacing="1" cellpadding="3" rules="all" border="3" style="background-color: White;
                                                    border-color: #efefef; width: 100%;">
                                                    <tr align="center" class="HeaderCellColor1">
                                                        <td >
                                                            
                                                        </td>
                                                        <td colspan="12">
                                                            AIR CONDITIONING
                                                        </td>
                                                        <td colspan="7">
                                                            REFRIGERATION PLANT
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class="HeaderCellColor" >
                                                     <td rowspan="3">
                                                            
                                                        </td>
                                                        <td colspan="8">
                                                            Pressure-kg/cm2
                                                        </td>
                                                        <td colspan="4">
                                                            Air Temp C
                                                        </td>
                                                        <td colspan="4">
                                                            Pressure-kg/cm2
                                                        </td>
                                                        <td colspan="3">
                                                            Temp C
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class="HeaderCellColor" >
                                                        <td colspan="4">
                                                            P
                                                        </td>
                                                        <td colspan="4">
                                                            S
                                                        </td>
                                                        <td colspan="2">
                                                            P
                                                        </td>
                                                        <td colspan="2">
                                                            S
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label210" class="verticaltext1">
                                                                Comp. No.
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label211" class="verticaltext1">
                                                                Suct
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label212" class="verticaltext1">
                                                                Disch
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label213" class="verticaltext1">
                                                                L. O.
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label214" class="verticaltext1">
                                                                Meat
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label215" class="verticaltext1">
                                                                Fish
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label216" class="verticaltext1">
                                                                Veg/Labby
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class="HeaderCellColor" >
                                                        <td>
                                                            <label id="Label227" class="verticaltext2">
                                                                Suct
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label id="Label228" class="verticaltext2">
                                                                Disch
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label id="Label229" class="verticaltext2">
                                                                L. O.
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label id="Label230" class="verticaltext2">
                                                                Cooling Water
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label id="Label231" class="verticaltext2">
                                                                Suct
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label id="Label233" class="verticaltext2">
                                                                Disch
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label id="Label237" class="verticaltext2">
                                                                L. O.
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label id="Label238" class="verticaltext2">
                                                                Cooling Water
                                                            </label>
                                                        </td>
                                                        <td width="50px">
                                                            IN
                                                        </td>
                                                        <td width="50px">
                                                            OUT
                                                        </td>
                                                        <td width="40px">
                                                            IN
                                                        </td>
                                                        <td width="40px">
                                                            OUT
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="border: 1px solid ActiveBorder">
                                                    <td align="left" style="height: 19px; width:60px;background-color: #BCF5A9">
                                                        <asp:Label ID="Label159" runat="server" Width="20px"> Min </asp:Label>
                                                        <asp:Label ID="Label559" runat="server" Visible="false" Width="60px" Text=''> </asp:Label>
                                                        <asp:Label ID="lblid" Width="60px" runat="server" Visible="false" Text=''> </asp:Label>
                                                        <asp:Label ID="lblLogId" Width="60px" runat="server" Visible="false" Text=''> </asp:Label>
                                                        <asp:Label ID="lblVessel" Width="60px" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "VESSEL_ID")%>'> </asp:Label>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt1" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_SUCT_PRESS_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt2" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_DISCH_PRESS_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt3" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_LO_PRESS_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt4" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_CW_PRESS_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt5" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_SUCT_PRESS_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt6" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_DISCH_PRESS_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt7" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_LO_PRESS_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt8" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_CW_PRESS_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt9" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_IN_AIR_TEMP_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt10" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_OUT_AIR_TEMP_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt11" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_IN_AIR_TEMP_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt12" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_OUT_AIR_TEMP_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt13" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_COMP_NO_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt14" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_SUCT_PRESS_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt15" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_DISCH_PRESS_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt16" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_LO_PRESS_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt17" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_MEAT_TEMP_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt18" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_FISH_TEMP_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt19" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_VEG_LOBBY_TEMP_Min")%>'> </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="border: 1px solid ActiveBorder">
                                                    <td align="left" style="height: 19px; background-color: #F78181"">
                                                        <asp:Label ID="Label5" runat="server" Width="60px">Max</asp:Label>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt01" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_SUCT_PRESS_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt02" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_DISCH_PRESS_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt03" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_LO_PRESS_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt04" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_CW_PRESS_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt05" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_SUCT_PRESS_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt06" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_DISCH_PRESS_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt07" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_LO_PRESS_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt08" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_CW_PRESS_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt09" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_IN_AIR_TEMP_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt010" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_P_OUT_AIR_TEMP_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt011" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_IN_AIR_TEMP_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt012" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "AC_S_OUT_AIR_TEMP_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt013" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_COMP_NO_Max_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt014" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_SUCT_PRESS_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt015" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_DISCH_PRESS_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt016" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_LO_PRESS_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt017" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_MEAT_TEMP_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt018" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_FISH_TEMP_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt019" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "REF_VEG_LOBBY_TEMP_Max")%>'> </asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%" valign="top">
                                        <asp:Repeater ID="Repeater2" runat="server">
                                            <HeaderTemplate>
                                                <table cellspacing="1" cellpadding="3" rules="all" border="3" style="background-color: White;
                                                    border-color: #efefef; width: 100%;">
                                                    <tr align="center"  class="HeaderCellColor1">
                                                        <td >
                                                           
                                                        </td>
                                                        <td colspan="9" >
                                                            FRESH WATER GENERATOR
                                                        </td>
                                                        <td colspan="4">
                                                            BOILER
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class="HeaderCellColor" >
                                                     <td rowspan="3">
                                                            
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label192" class="verticaltext">
                                                                Running Hours
                                                            </label>
                                                        </td>
                                                        <td colspan="4">
                                                            Heat Exchanger C
                                                        </td>
                                                        <td rowspan="3" c>
                                                            <label id="Label200" class="verticaltext">
                                                                Vacuum cm. Hg
                                                            </label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label201" class="verticaltext">
                                                                Shell Temp C
                                                            </label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label202" class="verticaltext">
                                                                Salinity PPM
                                                            </label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label203" class="verticaltext">
                                                                Flowmeter
                                                            </label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label204" class="verticaltext">
                                                                Oile Finning Hours
                                                            </label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label205" class="verticaltext">
                                                                Stream Press.</label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label206" class="verticaltext">
                                                                Feed Water temp.
                                                            </label>
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label207" class="verticaltext">
                                                                EGE soot Bown
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class="HeaderCellColor" >
                                                        <td colspan="2">
                                                            FW
                                                        </td>
                                                        <td colspan="2">
                                                            SW
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class="HeaderCellColor" >
                                                        <td width="40px">
                                                            IN
                                                        </td>
                                                        <td width="40px">
                                                            OUT
                                                        </td>
                                                        <td width="40px">
                                                            IN
                                                        </td>
                                                        <td width="40px">
                                                            OUT
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="border: 1px solid ActiveBorder">
                                                    <td align="left" style="height: 19px; width:60px; background-color: #BCF5A9">
                                                        <asp:Label ID="Label159" runat="server" Width="60px"> Min </asp:Label>
                                                        <asp:Label ID="Label559" runat="server" Visible="false" Width="60px" Text=''> </asp:Label>
                                                        <asp:Label ID="lblid" Width="60px" runat="server" Visible="false" Text=''> </asp:Label>
                                                        <asp:Label ID="lblLogId" Width="60px" runat="server" Visible="false" Text=''> </asp:Label>
                                                        <asp:Label ID="lblVessel" Width="60px" runat="server" Visible="false" Text=''> </asp:Label>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt20" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_RH_min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt21" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_FW_IN_min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt22" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_FW_OUT_min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt23" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SW_IN_min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt24" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SW_OUT_min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt25" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_VACCUM_min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt26" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SHELL_TEMP_min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt27" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SALINITY_PPM_min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt28" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_FLOWMETER_min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt29" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_OIL_FIRING_HRS_min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt30" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_STEAM_PRESS_min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt31" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_FEED_WTR_TEMP_min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt32" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_EGE_SOOT_BLOWN_min")%>'> </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="border: 1px solid ActiveBorder">
                                                    <td align="left" style="height: 19px; background-color: #F78181"">
                                                        <asp:Label ID="Label5" runat="server" Width="60px">Max</asp:Label>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt020" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_RH_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt021" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_FW_IN_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt022" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_FW_OUT_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt023" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SW_IN_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt024" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SW_OUT_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt025" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_VACCUM_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt026" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SHELL_TEMP_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt027" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_SALINITY_PPM_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt028" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "FWGEN_FLOWMETER_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt029" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_OIL_FIRING_HRS_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt030" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_STEAM_PRESS_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt031" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_FEED_WTR_TEMP_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt032" runat="server" Width="50px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "BLR_EGE_SOOT_BLOWN_Max")%>'> </asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%" valign="top">
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <HeaderTemplate>
                                                <table cellspacing="1" cellpadding="3" rules="all" border="3" style="background-color: White;
                                                    border-color: #efefef; width: 100%;">
                                                    <tr align="center"  class="HeaderCellColor1">
                                                        <td >
                                                           
                                                        </td>
                                                        <td colspan="5">
                                                            PURIFIERS
                                                        </td>
                                                        <td colspan="11">
                                                            MISCELLANEOUS
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class="HeaderCellColor" >
                                                     <td rowspan="3">
                                                            
                                                        </td>
                                                        <td colspan="2">
                                                            HO
                                                        </td>
                                                        <td colspan="2">
                                                            LO
                                                        </td>
                                                        <td>
                                                            DO
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label208" class="verticaltext">
                                                                Staff grounding
                                                            </label>
                                                        </td>
                                                        <td colspan="9">
                                                            Temperature C
                                                        </td>
                                                        <td rowspan="3">
                                                            <label id="Label209" class="verticaltext">
                                                                incinerator Run Hrs.
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class="HeaderCellColor" >
                                                        <td rowspan="2">
                                                            <label id="Label217" class="verticaltext1">
                                                                Hrs. run
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label218" class="verticaltext1">
                                                                Temp
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label219" class="verticaltext1">
                                                                Hrs. run
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label220" class="verticaltext1">
                                                                Temp
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label221" class="verticaltext1">
                                                                Temp
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label222" class="verticaltext1">
                                                                Thust bng.
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label223" class="verticaltext1">
                                                                intam big
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label224" class="verticaltext1">
                                                                Stem tube Oil
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label225" class="verticaltext1">
                                                                Sea water
                                                            </label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <label id="Label226" class="verticaltext1">
                                                                E R
                                                            </label>
                                                        </td>
                                                        <td colspan="2" width="40px">
                                                            H.O. Sett.
                                                        </td>
                                                        <td colspan="2" width="40px">
                                                            H.O. Serv.
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class="HeaderCellColor" >
                                                        <td width="40px">
                                                            1
                                                        </td>
                                                        <td width="40px">
                                                            2
                                                        </td>
                                                        <td width="40px">
                                                            1
                                                        </td>
                                                        <td width="40px">
                                                            2
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="border: 1px solid ActiveBorder">
                                                    <td align="left" style="height: 19px; Width:60px; background-color: #BCF5A9"">
                                                        <asp:Label ID="Label159" runat="server" Width="60px" Text='Min'> </asp:Label>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt33" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_HO_RH_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt34" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_HO_TEMP_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt35" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_LO_RH_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt36" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_LO_TEMP_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt37" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_DO_TEMP_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt38" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_SFT_GROUNDING_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt39" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_THRUST_BRG_TEMP_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt40" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_INTERM_BRG_TEMP_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt41" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_STERN_TUBE_OIL_TEMP_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt42" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_SEA_WTR_TEMP_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt43" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_ER_TEMP_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt44" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SETT_1_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt45" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SETT_2_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt46" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SERV_1_Min")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt47" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SERV_2_Min")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt48" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_INCINERATOR_RH_Min")%>'> </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="border: 1px solid ActiveBorder">
                                                    <td align="left" style="height: 19px; background-color: #F78181"">
                                                        <asp:Label ID="Label6" runat="server" Width="60px" Text='MAX'> </asp:Label>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt033" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_HO_RH_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt034" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_HO_TEMP_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt035" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_LO_RH_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt036" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_LO_TEMP_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt037" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "PUR_DO_TEMP_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt038" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_SFT_GROUNDING_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt039" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_THRUST_BRG_TEMP_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt040" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_INTERM_BRG_TEMP_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt041" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_STERN_TUBE_OIL_TEMP_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt042" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_SEA_WTR_TEMP_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt043" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_ER_TEMP_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt044" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SETT_1_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt045" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SETT_2_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt046" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SERV_1_Max")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt047" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_HO_SERV_2_Max")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt048" runat="server" Width="40px" CssClass="input centeralinment"
                                                            MaxLength="7" onKeydown="JavaScript:return MaskMoney_Minus(event);" Text='<%# DataBinder.Eval(Container.DataItem, "MISC_INCINERATOR_RH_Max")%>'> </asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" rowspan="3">
                                        <table cellspacing="0" cellpadding="3" rules="all" height="340px" border="3" style="background-color: White;
                                            border-color: #efefef; width: 100%; border-collapse: collapse;">
                                            <tr align="center"  class="HeaderCellColor1" >
                                                <td colspan="11" align="center" style="font-weight: bold">
                                                    BOILER/ COOLING WATER TEST
                                                </td>
                                            </tr>
                                            <tr align="center" class="HeaderCellColor" >
                                                <td rowspan="2">
                                                </td>
                                                <td rowspan="2" colspan="2">
                                                    BLR
                                                </td>
                                                <td colspan="4">
                                                    M/E
                                                </td>
                                                <td rowspan="2" colspan="2">
                                                    A/E
                                                </td>
                                                <td rowspan="2" colspan="2">
                                                    COMPR
                                                </td>
                                            </tr>
                                            <tr align="center" class="HeaderCellColor" >
                                                <td colspan="2">
                                                    JKT
                                                </td>
                                                <td colspan="2">
                                                    PIST
                                                </td>
                                            </tr>
                                            <tr>
                                            <td></td>
                                                <td style ="background-color: #BCF5A9">
                                                    Min
                                                </td>
                                                <td style ="background-color: #F78181">
                                                    Max
                                                </td>
                                                <td style ="background-color: #BCF5A9">
                                                    Min
                                                </td>
                                                <td style ="background-color: #F78181">
                                                    Max
                                                </td>
                                                 <td style ="background-color: #BCF5A9">
                                                    Min
                                                </td>
                                                <td style ="background-color: #F78181">
                                                    Max
                                                </td>
                                                 <td style ="background-color: #BCF5A9">
                                                    Min
                                                </td>
                                                <td style ="background-color: #F78181">
                                                    Max
                                                </td>
                                                 <td style ="background-color: #BCF5A9">
                                                    Min
                                                </td>
                                                <td style ="background-color: #F78181">
                                                    Max
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    CHLORIDES/<br />
                                                    CONDUCTIVITY
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_CHLORIDES_BLR" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_CHLORIDES_BLR_MIN") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_CHLORIDES_BLR_max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_CHLORIDES_BLR_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_CHLORIDES_MEJW" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_CHLORIDES_MEJW_MIN") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_CHLORIDES_MEJW_max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_CHLORIDES_MEJW_MAX") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_CHLORIDES_MEPW" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_CHLORIDES_MEPW_MIN") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_CHLORIDES_MEPW_max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_CHLORIDES_MEPW_MAX") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_CHLORIDES_AE" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_CHLORIDES_AE_MIN") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_CHLORIDES_AE_max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_CHLORIDES_AE_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_CHLORIDES_CMPR" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_CHLORIDES_CMPR_Min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_CHLORIDES_CMPR_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_CHLORIDES_CMPR_MAX") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    ALKALINITY
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_ALKALINITY_BLR" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_ALKALINITY_BLR_Min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_ALKALINITY_BLR_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_ALKALINITY_BLR_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_ALKALINITY_MEJW" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_ALKALINITY_MEJW_Min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_ALKALINITY_MEJW_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_ALKALINITY_MEJW_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_ALKALINITY_MEPW" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_ALKALINITY_MEPW_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_ALKALINITY_MEPW_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_ALKALINITY_MEPW_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_ALKALINITY_AE" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_ALKALINITY_AE_Min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_ALKALINITY_AE_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_ALKALINITY_AE_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_ALKALINITY_CMPR" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_ALKALINITY_CMPR_Min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_ALKALINITY_CMPR_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_ALKALINITY_CMPR_Max") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    TOTAL
                                                    <br />
                                                    ALKALINITY
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_TALKALINITY_BLR" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_TALKALINITY_BLR_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_TALKALINITY_BLR_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_TALKALINITY_BLR_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_TALKALINITY_MEJW" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_TALKALINITY_MEJW_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_TALKALINITY_MEJW_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_TALKALINITY_MEJW_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_TALKALINITY_MEPW" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_TALKALINITY_MEPW_mn") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_TALKALINITY_MEPW_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_TALKALINITY_MEPW_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_TALKALINITY_AE" runat="server" Width="60px" CssClass="input centeralinment"
                                                        Text='<%# Bind("BLR_CW_TALKALINITY_AE_Min") %>' MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_TALKALINITY_AE_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_TALKALINITY_AE_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_TALKALINITY_CMPR" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_TALKALINITY_CMPR_Min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_TALKALINITY_CMPR_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_TALKALINITY_CMPR_Max") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    PHOSPHATES
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_PHOSPHATES_BLR" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_PHOSPHATES_BLR_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_PHOSPHATES_BLR_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        Text='<%# Bind("BLR_CW_PHOSPHATES_BLR_Max") %>' MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_PHOSPHATES_MEJW" runat="server" Width="60px" CssClass="input centeralinment"
                                                        Text='<%# Bind("BLR_CW_PHOSPHATES_MEJW_min") %>' MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_PHOSPHATES_MEJW_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_PHOSPHATES_MEJW_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_PHOSPHATES_MEPW" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_PHOSPHATES_MEPW_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_PHOSPHATES_MEPW_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_PHOSPHATES_MEPW_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_PHOSPHATES_AE" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_PHOSPHATES_AE_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_PHOSPHATES_AE_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_PHOSPHATES_AE_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_PHOSPHATES_CMPR" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_PHOSPHATES_CMPR_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_PHOSPHATES_CMPR_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_PHOSPHATES_CMPR_Max") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    BLOWDOWN
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_BLOWDOWN_BLR" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_BLOWDOWN_BLR_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_BLOWDOWN_BLR_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_BLOWDOWN_BLR_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_BLOWDOWN_MEJW" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_BLOWDOWN_MEJW_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_BLOWDOWN_MEJW_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_BLOWDOWN_MEJW_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_BLOWDOWN_MEPW" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_BLOWDOWN_MEPW_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_BLOWDOWN_MEPW_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_BLOWDOWN_MEPW_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_BLOWDOWN_AE" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_BLOWDOWN_AE_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_BLOWDOWN_AE_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_BLOWDOWN_AE_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_BLOWDOWN_CMPR" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_BLOWDOWN_CMPR_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_BLOWDOWN_CMPR_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_BLOWDOWN_CMPR_Max") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    NITRITES
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_NITRITES_BLR" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_NITRITES_BLR_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_NITRITES_BLR_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_NITRITES_BLR_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_NITRITES_MEJW" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_NITRITES_MEJW_Min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_NITRITES_MEJW_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_NITRITES_MEJW_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_NITRITES_MEPW" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_NITRITES_MEPW_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_NITRITES_MEPW_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_NITRITES_MEPW_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_NITRITES_AE" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_NITRITES_AE_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_NITRITES_AE_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_NITRITES_AE_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_NITRITES_CMPR" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_NITRITES_CMPR_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_NITRITES_CMPR_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_NITRITES_CMPR_Max") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    DOSAGE
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_DOSAGE_BLR" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_DOSAGE_BLR_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_DOSAGE_BLR_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_DOSAGE_BLR_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_DOSAGE_MEJW" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_DOSAGE_MEJW_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_DOSAGE_MEJW_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_DOSAGE_MEJW_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_DOSAGE_MEPW" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_DOSAGE_MEPW_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_DOSAGE_MEPW_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_DOSAGE_MEPW_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_DOSAGE_AE" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_DOSAGE_AE_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_DOSAGE_AE_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_DOSAGE_AE_Max") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_DOSAGE_CMPR" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_DOSAGE_CMPR_min") %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBLR_CW_DOSAGE_CMPR_Max" runat="server" Width="60px" CssClass="input centeralinment"
                                                        MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# Bind("BLR_CW_DOSAGE_CMPR_Max") %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
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
