<%@ Page Title="Equipment Structure" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="PMSEquipmentStructure.aspx.cs" Inherits="Technical_PMS_PMSEquipmentStructure"
    EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/ucAsyncPager.ascx" TagName="ucAsyncPager" TagPrefix="auc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
  
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <link href="../../Scripts/JsTree/themes/default/style.min.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JsTree/libs/jquery.js" type="text/javascript"></script>
    <script src="../../Scripts/JsTree/jstree.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min1.11.0.js" type="text/javascript"></script>
    <link href="../../Styles/jquery-ui1.11.0.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/PMSEqpStructure.js" type="text/javascript"> </script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/jsasyncpager.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
     <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .page
        {
            min-width: 600px;
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="page-title" style="margin-bottom: 3px;">
        Equiment Structure
    </div>
    <div align="center">
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Font-Bold=true></asp:Label>
    </div>
    <div>
        <div style="float: left; border: 1px solid gray; border-radius: 5px; width: 20%;
            min-width: 250px; max-height: 800px; overflow: hidden;">
            <div style="padding: 3px; background-color: #DCDCDC">
                <table>
                    <tr>
                        <td style="width: 200px;">
                            <asp:DropDownList ID="DDlVessel_List" runat="server" Font-Size="11px" AutoPostBack="false"
                                Width="200px" DataTextField="Vessel_Name" DataValueField="Vessel_ID" onChange="DDlVessel_selectionChange()">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:ImageButton ID="ImgRefresh" runat="server" Height="20px" ToolTip="Refresh" ImageUrl="~/Images/Refresh-icon.png"
                                OnClientClick="return refreshPage()" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="FunctionalTree" style="min-width: 250px; height: 750px; max-height: 750px;
                        overflow: scroll; padding-top: 3px">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="float: right; width: 79.6%;">
            <div id="dvEditDetails">
            </div>
            <div id="dvMachineryDetails" style="height: 100px; border: 1px solid gray; border-radius: 5px;
                padding: 5px">
            </div>
            <div id="tabs" style="height: 600px; border: 1px solid gray">
                <ul>
                    <li><a href="#joblibrarytab"><span>Job Library</span></a></li>
                    <li><a href="#spareconstab"><span>Spare Items</span></a></li>
                </ul>
                <div id="alljobs" style="padding: 2px; display: none">
                </div>
                <div id="joblibrarytab">
                    <div id="jobtabhead" style="display: none;">
                        <table style="width: 97%;">
                            <tr>
                                <td style="width: 15%;">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                        <ContentTemplate>
                                            <asp:RadioButtonList ID="rdJobStatus" runat="server" RepeatDirection="Horizontal"
                                                onclick="return onFilterJob(this)" AutoPostBack="True">
                                                <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Deleted" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td align="left" style="width: 5%;">
                                    <asp:Button ID="btnCopyJobs" runat="server" Text="Copy jobs" OnClientClick="return onCopyJobs();" />
                                </td>
                                <td align="left" style="width: 5%;">
                                    <asp:Button ID="btnMoveJobs" runat="server" Text="Move jobs" OnClientClick="return onMoveJobs();" />
                                </td>
                                <td align="right" style="width: 75%;">
                                    <asp:CheckBox ID="chkCheckAllJobs" Text="Check ALL" runat="server" onclick="return onCheckAllJobs();" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="joblibrary" style="padding: 2px; display: none;">
                        loading....
                    </div>
                    <br />
                    <div id="jobspager" style="display: none;">
                        <auc:ucAsyncPager ID="ucAsyncPager1" runat="server" />
                    </div>
                </div>
                <div id="spareconstab" style="display: none;">
                    <div id="sparecontabhead" style="display: none;">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="rdItemStatus" runat="server" RepeatDirection="Horizontal"
                                    onclick="return onFilterSpare(this)" AutoPostBack="True">
                                    <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Deleted" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="sparecons" style="padding: 2px; display: none;">
                        loading....
                    </div>
                    <br />
                    <div id="sparepager" style="display: none;">
                        <auc:ucAsyncPager ID="ucAsyncPager2" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
     <asp:UpdatePanel runat="server" ID="UpdGroup" UpdateMode="Conditional">
        <ContentTemplate>

       <div id="divAddGroup" style="width: 370px; display: none; border: 1px solid #cccccc;
                background-color: #E0ECF8;" title="Add System">
                <table cellpadding="1" cellspacing="1" style="width: 370px;   ">
                    <tr>
                        <td align="left">
                            Functions : &nbsp;&nbsp;
                        </td>
                        <td colspan="3" align="left">
                            <%--<ucFunction:ctlFunctionList ID="ddlCatalogFunction" runat="server" Width="120%" CssClass="txtInput" />--%>
                            <asp:DropDownList ID="ddlFunction" runat="server" Width="180px" CssClass="txtInput" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Group Name:<asp:Label ID="lbl1" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtGroupName" CssClass="txtInput" runat="server" Width="90px"></asp:TextBox>
                        </td>
                       
                       
                    </tr>
                   <tr>
                    <td  align="left">
                            Group Description :&nbsp;
                        </td>
                        <td valign="top" align="left">
                            <asp:TextBox ID="txtGroupDesc" runat="server" CssClass="txtInput" TextMode="MultiLine" Width="180px" Height="50px"          ></asp:TextBox>
                        </td>
                   </tr>
                    <tr>
                        <td align="left">
                            Maker :
                        </td>
                        <td colspan="3" align="left">
                            <asp:DropDownList ID="ddlMaker" runat="server" CssClass="txtInput"  Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td align="left" valign="middle">
                            <asp:ImageButton ID="imbAddMaker" ImageUrl="~/Images/AddMaker.GIF" Height="15px"
                                ToolTip="Add Maker" runat="server" OnClientClick="return OnAddMaker();" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Model :
                        </td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtModel" MaxLength="255" CssClass="txtInput" runat="server"
                                Width="99%">
                            </asp:TextBox>
                        </td>
                    </tr>
                
                    <tr>
                        <td colspan="4" align="right">
                           
                             
                            <asp:Button ID="btnSaveGroup" Text="Save" runat="server" Width="70px"   OnClientClick="return onSaveGroup();" />
                          
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            <asp:Label ID="lblCatalogueErrorMsg" Text="" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                       <%-- <td>
                            <asp:HiddenField ID="hdfCatlogueOperationMode" runat="server" />
                        </td>
                        <td>
                            <asp:HiddenField ID="hdfCatelogScrollPos" runat="server" Value="0" />
                        </td>--%>
                        <td>
                            <input type="hidden" value="1" id="hsb">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        </asp:UpdatePanel>

        <div>
         <asp:HiddenField ID="hdnGroupOperationMode" runat="server" />
        <asp:HiddenField ID="hdnEquipmentOperationMode" runat="server" />
         <asp:HiddenField ID="hdnUserID" runat="server" />
        </div>
    <script type="text/javascript">
        $(document).ready(initTab);      
       
    </script>
</asp:Content>
