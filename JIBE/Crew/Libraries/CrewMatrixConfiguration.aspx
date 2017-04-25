<%@ Page Title="Crew Matrix Configuration" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CrewMatrixConfiguration.aspx.cs" Inherits="Crew_Libraries_CrewMatrixConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

  
    </script>
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .HideRow
        {
            display: none;
        }
        .ajax__tab_header
        {
            margin-top: 10px;
        }
        #tblTanker tr:hover
        {
            background-color: #feecec;
        }
        .AlternatingRowStyle-css
        {
            background-color: #f6f6f6;
        }
        .HeaderStyle-css
        {
            height: 25px;
        }
        .gridmain-css{color:#000 !important;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content">
        <div class="page-title">
            Crew Matrix & Group Configuration
        </div>
        <ajaxToolkit:TabContainer runat="server">
            <ajaxToolkit:TabPanel runat="server" ID="rt1" HeaderText="Crew Matrix Configuration">
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div align="center" style="min-height: 700px">
                                <div align="center" style="font-weight: bold; font-size: 12px;">
                                    <u>Default Settings</u>
                                </div>
                                <div style="height: 10px">
                                </div>
                                <table id="ctl00_MainContent_gvOilMajors" class="gridmain-css" cellspacing="2" cellpadding="0"
                                    border="1" style="width: 55%;" rules="all">
                                    <tr class="HeaderStyle-css">
                                        <th>
                                            Field
                                        </th>
                                        <th colspan="2">
                                            Value
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px;">
                                            Tanker Certification
                                        </td>
                                        <td colspan="2">
                                            <asp:Repeater runat="server" ID="rptTanker">
                                                <HeaderTemplate>
                                                    <table width="100%" border="0" id="tblTanker" class="tblTanker">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="RowStyle-css">
                                                        <td width="230px">
                                                            <asp:CheckBox rel='<%# Eval("ID") %>' Checked='<%# Convert.ToInt16(Eval("DefaultValue"))==1?true:false %>'
                                                                Text='<%# Eval("Parameters") %>' ID="chkTanker" runat="server" />
                                                            <asp:TextBox MaxLength="30" Width="95px" rel='<%# Eval("ID") %>' runat="server" Text='<%# Eval("Parameters") %>'
                                                                Style="display: none;" ID="txtTanker" />
                                                            <asp:Button ID="btnSaveTank" CssClass="btnSaveTank" runat="server" rel='<%# Eval("ID") %>'
                                                                Text="Save" Style="display: none;" OnClick="btnSaveTank_Click"  Visible="<%# editDeleteAccess %>"/>
                                                            <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" rel='<%# Eval("ID") %>'
                                                                Text="Cancel" Style="display: none;"  Visible="<%# editDeleteAccess %>"/>
                                                        </td>
                                                        <td width="100px">
                                                            <asp:Image ID="Edit" rel='<%# Eval("ID") %>' CssClass="edit" ImageUrl="~/Images/Edit.gif"
                                                                ForeColor="Black" ToolTip="Edit" runat="server" Height="16px" Style="cursor: pointer;
                                                                vertical-align: bottom; margin-left: 35px;" Visible="<%# editDeleteAccess %>" />
                                                            <asp:ImageButton rel='<%# Eval("ID") %>' ID="ImgDelete" runat="server" Text="Delete"
                                                                OnClientClick="return confirm('Are you sure, you want to delete?')" CssClass="delete"
                                                                ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px"
                                                                OnClick="btnDeleteTank_Click" Style="vertical-align: bottom;"  Visible="<%# editDeleteAccess %>" ></asp:ImageButton>
                                                            <asp:Image rel='<%# Eval("ID") %>' ID="imgRecordInfo" CssClass="information" ImageAlign="AbsMiddle"
                                                                ImageUrl="~/Images/RecordInformation.png" Height="16px" Width="16px" runat="server"
                                                                onclick='<%# "Get_Record_Information(&#39;CRW_LIB_MATRIX_Configuration&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="AlternatingRowStyle-css">
                                                        <td width="230px">
                                                            <asp:CheckBox rel='<%# Eval("ID") %>' Checked='<%# Convert.ToInt16(Eval("DefaultValue"))==1?true:false %>'
                                                                Text='<%# Eval("Parameters") %>' ID="chkTanker" runat="server" />
                                                            <asp:TextBox MaxLength="30" Width="95px" rel='<%# Eval("ID") %>' runat="server" Text='<%# Eval("Parameters") %>'
                                                                Style="display: none;" ID="txtTanker"  Visible="<%# editDeleteAccess %>"/>
                                                            <asp:Button ID="btnSaveTank" CssClass="btnSaveTank" runat="server" rel='<%# Eval("ID") %>'
                                                                Text="Save" Style="display: none;" OnClick="btnSaveTank_Click"  Visible="<%# editDeleteAccess %>"/>
                                                            <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" rel='<%# Eval("ID") %>'
                                                                Text="Cancel" Style="display: none;"  Visible="<%# editDeleteAccess %>"/>
                                                        </td>
                                                        <td width="100px">
                                                            <asp:Image ID="Edit" rel='<%# Eval("ID") %>' CssClass="edit" ImageUrl="~/Images/Edit.gif"
                                                                ForeColor="Black" ToolTip="Edit" runat="server" Height="16px" Style="cursor: pointer;
                                                                vertical-align: bottom; margin-left: 35px;" Visible="<%# editDeleteAccess %>"  />
                                                            <asp:ImageButton rel='<%# Eval("ID") %>' ID="ImgDelete" runat="server" Text="Delete"
                                                                OnClientClick="return confirm('Are you sure, you want to delete?')" CssClass="delete"
                                                                ForeColor="Black" ToolTip="Delete" Style="vertical-align: bottom;" ImageUrl="~/Images/delete.png"
                                                                Height="16px" OnClick="btnDeleteTank_Click"  Visible="<%# editDeleteAccess %>" ></asp:ImageButton>
                                                            <asp:Image rel='<%# Eval("ID") %>' ID="imgRecordInfo" CssClass="information" ImageAlign="AbsMiddle"
                                                                ImageUrl="~/Images/RecordInformation.png" Height="16px" Width="16px" 
                                                                runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_LIB_MATRIX_Configuration&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    <tr id="trAddNewTanker" runat="server" visible="<%# trAddNewTanker%>">
                                                        <td colspan="2">
                                                            Add New:
                                                            <asp:TextBox MaxLength="30" Width="95px" runat="server" ID="txtAddNewTanker" ClientIDMode="Static" />
                                                            <asp:Button ID="btnSaveTank" runat="server" Text="Save" ClientIDMode="Static" OnClick="btnSaveNewTank_Click" />
                                                        </td>
                                                    </tr>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <asp:HiddenField ID="hdnTankerName" Value="" ClientIDMode="Static" runat="server" />
                                            <asp:HiddenField ID="hdnOrignalTankerName" Value="" ClientIDMode="Static" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px;">
                                            STCW V Para
                                        </td>
                                        <td>
                                            <asp:Repeater runat="server" ID="rptSTCW">
                                                <HeaderTemplate>
                                                    <table width="100%" border="0" id="tblTanker" class="tblSTCW">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="RowStyle-css">
                                                        <td width="230px">
                                                            <asp:RadioButton rel='<%# Eval("ID") %>' Checked='<%# Convert.ToInt16(Eval("DefaultValue"))==1?true:false %>'
                                                                Text='<%# Eval("Parameters") %>' ID="chkTanker" runat="server" />
                                                            <asp:TextBox MaxLength="30" Width="95px" rel='<%# Eval("ID") %>' runat="server" Text='<%# Eval("Parameters") %>'
                                                                Style="display: none;" ID="txtTanker"  Visible="<%# editDeleteAccess %>"/>
                                                            <asp:Button ID="btnSaveTank" CssClass="btnSaveTank" runat="server" rel='<%# Eval("ID") %>'
                                                                Text="Save" Style="display: none;" OnClick="btnSaveSTCW_Click"  Visible="<%# editDeleteAccess %>"/>
                                                            <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" rel='<%# Eval("ID") %>'
                                                                Text="Cancel" Style="display: none;" Visible="<%# editDeleteAccess %>" />
                                                        </td>
                                                        <td width="100px">
                                                            <asp:Image ID="Edit" rel='<%# Eval("ID") %>' CssClass="edit" ImageUrl="~/Images/Edit.gif"
                                                                ForeColor="Black" ToolTip="Edit" runat="server" Height="16px" Style="cursor: pointer;
                                                                vertical-align: bottom; margin-left: 35px;"  Visible="<%# editDeleteAccess %>" />
                                                            <asp:ImageButton rel='<%# Eval("ID") %>' ID="ImgDelete" runat="server" Text="Delete"
                                                                OnClientClick="return confirm('Are you sure, you want to delete?')" CssClass="delete"
                                                                ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png" Height="16px"
                                                                OnClick="btnDeleteSTCW_Click" Style="vertical-align: bottom;"  Visible="<%# editDeleteAccess %>" ></asp:ImageButton>
                                                            <asp:Image rel='<%# Eval("ID") %>' ID="imgRecordInfo" CssClass="information" ImageAlign="AbsMiddle"
                                                                ImageUrl="~/Images/RecordInformation.png" Height="16px" Width="16px" runat="server"
                                                                onclick='<%# "Get_Record_Information(&#39;CRW_LIB_MATRIX_Configuration&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="AlternatingRowStyle-css">
                                                        <td width="230px">
                                                            <asp:RadioButton rel='<%# Eval("ID") %>' Checked='<%# Convert.ToInt16(Eval("DefaultValue"))==1?true:false %>'
                                                                Text='<%# Eval("Parameters") %>' ID="chkTanker" runat="server" />
                                                            <asp:TextBox MaxLength="30" Width="95px" rel='<%# Eval("ID") %>' runat="server" Text='<%# Eval("Parameters") %>'
                                                                Style="display: none;" ID="txtTanker"  Visible="<%# editDeleteAccess %>"/>
                                                            <asp:Button ID="btnSaveTank" CssClass="btnSaveTank" runat="server" rel='<%# Eval("ID") %>'
                                                                Text="Save" Style="display: none;" OnClick="btnSaveSTCW_Click"  Visible="<%# editDeleteAccess %>"/>
                                                            <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" rel='<%# Eval("ID") %>'
                                                                Text="Cancel" Style="display: none;"  Visible="<%# editDeleteAccess %>"/>
                                                        </td>
                                                        <td width="100px">
                                                            <asp:Image ID="Edit" rel='<%# Eval("ID") %>' CssClass="edit" ImageUrl="~/Images/Edit.gif"
                                                                ForeColor="Black" ToolTip="Edit" runat="server" Height="16px" Style="cursor: pointer;
                                                                vertical-align: bottom; margin-left: 35px;" Visible="<%# editDeleteAccess %>"  />
                                                            <asp:ImageButton rel='<%# Eval("ID") %>' ID="ImgDelete" runat="server" Text="Delete"
                                                                OnClientClick="return confirm('Are you sure, you want to delete?')" CssClass="delete"
                                                                ForeColor="Black" ToolTip="Delete" Style="vertical-align: bottom;" ImageUrl="~/Images/delete.png"
                                                                Height="16px" OnClick="btnDeleteSTCW_Click"  Visible="<%# editDeleteAccess %>" ></asp:ImageButton>
                                                            <asp:Image rel='<%# Eval("ID") %>' ID="imgRecordInfo" CssClass="information" ImageAlign="AbsMiddle"
                                                                ImageUrl="~/Images/RecordInformation.png" Height="16px" Width="16px" 
                                                                runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_LIB_MATRIX_Configuration&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    <tr  id="trAddNewSTWC" runat="server" visible="<%# trAddNewSTWC%>">
                                                        <td colspan="2">
                                                            Add New:
                                                            <asp:TextBox MaxLength="30" Width="95px"  runat="server" ID="txtAddNewSTWC" ClientIDMode="Static" />
                                                            <asp:Button ID="btnSaveSTWC" runat="server" Text="Save" ClientIDMode="Static" OnClick="btnSaveNewSTWC_Click" />
                                                        </td>
                                                    </tr>
                                                    </table>
                                                </FooterTemplate> 
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px;">
                                            Future Date Restriction
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtDate" MaxLength="3" style="margin-left: 10px;" Width="40px" onkeypress="return isNumberKey(event)" runat="server"></asp:TextBox> Days
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="tdSave" runat="server" colspan="3" align="center">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSaveMatrix_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="rt2" runat="server" HeaderText="Crew Matrix Rank Configuration">
                <ContentTemplate>
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div align="center">
                                <table width="50%">
                                    <tr>
                                        <td valign="top" style="width: 50%;">
                                            <div align="center" style="font-size: large;">
                                                <u>Groups</u></div>
                                            <div style="height: 10px">
                                            </div>
                                            <div style="height: 600px; overflow: auto; border-style: outset;">
                                                <asp:ListBox ID="listGroup" Width="100%" Height="600px" runat="server" AutoPostBack="true"
                                                    CssClass="listbox-centered" OnSelectedIndexChanged="listGroup_SelectedIndexChanged">
                                                    <asp:ListItem Value="1" Text="Group 1"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Group 2"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Group 3"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Group 4"></asp:ListItem>
                                                    <%--<asp:ListItem Value="5" Text="Group 5"></asp:ListItem>--%>
                                                </asp:ListBox>
                                            </div>
                                        </td>
                                        <td style="width: 20%; height: 20%">
                                            <div align="center" style="font-size: large">
                                                <span id="spnRanksGroup"></span><u>Ranks</u>
                                            </div>
                                            <div style="height: 10px">
                                            </div>
                                            <div style="border-style: outset; height: 600px; overflow: auto; overflow-x: hidden;">
                                                <asp:CheckBoxList ID="chkRank" runat="server" RepeatDirection="Vertical" Width="400px">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGroupSave" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />
                    <center>
                        <asp:Button ID="btnGroupSave" Text="Save" Width="100px" Height="25px" runat="server"
                            CssClass="changePassword" OnClick="btnSave_Click" />
                    </center>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </div>
    <script type="text/javascript">
        function HideRanks() {
            var Data = $("#ctl00_MainContent_ctl00_rt2_chkRank span[disabled='disabled']");
            Data.each(function (index) {
                $(this).parent().parent().addClass("HideRow");
            });
            $("#spnRanksGroup").text($("#ctl00_MainContent_ctl00_rt2_listGroup option:selected").text() + ": ");
        }

        $(document).ready(function () {
            $("body").on("click", ".edit", function () {
                var rel = $(this).attr("rel");
                $(this).hide();
                $("input[type='text'][rel='" + rel + "']").show();
                $("#hdnOrignalTankerName").val($("input[type='text'][rel='" + rel + "']").val())

                $("input[type='submit'][rel='" + rel + "']").show();
                $("span[rel='" + rel + "']").hide();
                $(".delete[rel='" + rel + "']").hide();
                $(".information[rel='" + rel + "']").hide();
            });

            $("body").on("click", ".btnCancel", function () {
                var rel = $(this).attr("rel");
                $(this).hide();
                $("input[type='text'][rel='" + rel + "']").hide();
                $("input[type='text'][rel='" + rel + "']").val($("span[rel='" + rel + "'] label").text());
                $("input[type='submit'][rel='" + rel + "']").hide();
                $("span[rel='" + rel + "']").show();
                $(".delete[rel='" + rel + "']").show();
                $(".information[rel='" + rel + "']").show();
                $(".edit[rel='" + rel + "']").show();
                return false;
            });

            $("body").on("click", ".delete", function () {
                var rel = $(this).attr("rel");
                $("#hdnOrignalTankerName").val($("input[type='text'][rel='" + rel + "']").val())
            });

            $("body").on("click", "#btnSaveTank", function () {
                if ($.trim($("#txtAddNewTanker").val()) == "") {
                    $("#txtAddNewTanker").focus();
                    alert("Please enter text");
                    return false;
                }


                ///check whether Tanker Certification already exists
                var result = false;
                var Data = $(".tblTanker span label");
                Data.each(function (index) {
                    if ($.trim(Data[index].innerHTML.toLowerCase()) == $.trim($("#txtAddNewTanker").val().toLowerCase())) {
                        result = true;
                    }
                });
                if (result) {
                    $("#txtAddNewTanker").val($.trim($("#txtAddNewTanker").val()));
                    $("#txtAddNewTanker").focus();
                    alert("Tanker Certification already exists");
                    return false;
                }

                $("#hdnTankerName").val($("#txtAddNewTanker").val());
                $("#hdnOrignalTankerName").val('');
            });

            $("body").on("click", "#btnSaveSTWC", function () {
                if ($.trim($("#txtAddNewSTWC").val()) == "") {
                    $("#txtAddNewSTWC").focus();
                    alert("Please enter text");
                    return false;
                }


                ///check whether Tanker Certification already exists
                var result = false;
                var Data = $(".tblSTCW span label");
                Data.each(function (index) {
                    if ($.trim(Data[index].innerHTML.toLowerCase()) == $.trim($("#txtAddNewSTWC").val().toLowerCase())) {
                        result = true;
                    }
                });
                if (result) {
                    $("#txtAddNewSTWC").val($.trim($("#txtAddNewSTWC").val()));
                    $("#txtAddNewSTWC").focus();
                    alert("STCW V Para already exists");
                    return false;
                }

                $("#hdnTankerName").val($("#txtAddNewSTWC").val());
                $("#hdnOrignalTankerName").val('');
            });


            $("body").on("click", ".btnSaveTank", function () {
                var rel = $(this).attr("rel");
                if ($.trim($("input[type='text'][rel='" + rel + "']").val()) == "") {
                    $("input[type='text'][rel='" + rel + "']").focus();
                    alert("Please enter text");
                    return false;
                }

                ///check whether Tanker Certification already exists
                var result = false;
                var Data = $(".tblTanker span[rel!='" + rel + "'] label");
                Data.each(function (index) {
                    if ($.trim(Data[index].innerHTML).toLowerCase() == $.trim($("input[type='text'][rel='" + rel + "']").val().toLowerCase())) {
                        result = true;
                    }
                });
                if (result) {
                    $("input[type='text'][rel='" + rel + "']").val($.trim($("input[type='text'][rel='" + rel + "']").val()));
                    $("input[type='text'][rel='" + rel + "']").focus();
                    alert("Tanker Certification already exists");
                    return false;
                }

                ///check whether Tanker Certification already exists
                var Data = $(".tblSTCW span[rel!='" + rel + "'] label");
                Data.each(function (index) {
                    if ($.trim(Data[index].innerHTML.toLowerCase()) == $.trim($("input[type='text'][rel='" + rel + "']").val().toLowerCase())) {
                        result = true;
                    }
                });
                if (result) {
                    $("input[type='text'][rel='" + rel + "']").val($.trim($("input[type='text'][rel='" + rel + "']").val()));
                    $("input[type='text'][rel='" + rel + "']").focus();
                    alert("STCW V Para already exists");
                    return false;
                }

                $("#hdnTankerName").val($("input[type='text'][rel='" + rel + "']").val());
            });

            $("body").on("click", "#tblTanker input[type='radio']", function () {
                $("#tblTanker input[type='radio']").prop("checked", false);
                $(this).prop("checked", true);
            });

            $("body").on("click", "#ctl00_MainContent_ctl00_rt1_btnSave", function () {
                if ($("#tblTanker input[type='checkbox']:checked").length == 0) {
                    alert("Please select atleast one Tanker Certification as default");
                    return false;
                }

                if ($("#tblTanker input[type='radio']:checked").length == 0) {
                    alert("Please select atleast one STCW V Para as default");
                    return false;
                }

                if ($.trim($("#ctl00_MainContent_ctl00_rt1_txtDate").val()) == "") {
                    $("#ctl00_MainContent_ctl00_rt1_txtDate").focus();
                    alert("Please enter value for Future Date Restriction");
                    return false;
                }
            });
        });
    </script>
</asp:Content>
