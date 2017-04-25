<%@ Page Language="C#" Title="Ship project Mgmt" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="ShipProjectMgmt.aspx.cs"
    Inherits="ShipProjectMgmt" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ctlFunctionList.ascx" TagName="ctlFunctionList" TagPrefix="ucFunction" %>
<%@ Register Src="~/UserControl/ctlVesselLocationList.ascx" TagName="ctlVesselLocationList"
    TagPrefix="ucVesslLocation" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
     <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
      <style type="text/css">
       .page

        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }

  .page
        {
            width: 100%;
           
        }

       </style>
    <script language="javascript" type="text/javascript">

        //ctl00_MainContent
        function ValidationOnProject() {

            if (document.getElementById("ctl00_MainContent_txtProject").value == "") {
                document.getElementById("ctl00_MainContent_txtProject").focus();
                alert("Please enter project.");
                return false;
            }

            return true;
        }


        function ValidationOnModule() {

            if (document.getElementById("ctl00_MainContent_txtModule").value == "") {
                alert("Please enter Module.");
                document.getElementById("ctl00_MainContent_txtModule").focus();
                return false;
            }

            return true;
        }


        function ValidationOnScreen() {


            if (document.getElementById("ctl00_MainContent_ddlScreenType").value == "0") {
                alert("Please select screen type.");
                document.getElementById("ctl00_MainContent_ddlScreenType").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtScreen").value == "") {
                alert("Please enter screen.");
                document.getElementById("ctl00_MainContent_txtScreen").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtAssembly").value == "") {
                alert("Please enter Assembly.");
                document.getElementById("ctl00_MainContent_txtAssembly").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtClass").value == "") {
                alert("Please enter Class.");
                document.getElementById("ctl00_MainContent_txtClass").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtImagePath").value == "") {
                alert("Please enter image path.");
                document.getElementById("ctl00_MainContent_txtImagePath").focus();
                return false;
            }

            return true;
        }


        function RefreshFromchild() {

            document.getElementById("btnHiddenSubmit").click();
        }
      
    </script>
   </asp:Content>
   <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
   <div style="font-family: Tahoma; font-size: 12px; border: 1px solid #cccccc; padding: 0px;
        margin-top: 0px;">
        <table cellpadding="1" cellspacing="1" width="100%">
            <tr>
                <td colspan="3">
                
                      <div class="page-title">
                             Ship Project ;Module; Screen Settings
    
                
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr style="height: 40px">
                            <td align="right" style="width: 10%;">
                                Search :&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSearchProject" runat="server" Width="100%"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                    TargetControlID="txtSearchProject" WatermarkText="Type to search Project" WatermarkCssClass="watermarked" />
                            </td>
                            <td align="center" style="width: 10%">
                                <asp:ImageButton ID="imgProjectSearch" ImageUrl="~/Purchase/Image/preview.gif" runat="server"
                                    OnClick="imgProjectSearch_Click" />
                            </td>
                            <td align="center" style="width: 10%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div id="DivProjectGridHolder" style="overflow-x: hidden; overflow-y: scroll; height: 450px;
                                    border: 1px solid #cccccc;">
                                    <asp:UpdatePanel runat="server" ID="UpdProjectGrid" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvProject" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvProject_RowDataBound"
                                                Width="100%" GridLines="Both" OnSelectedIndexChanging="gvProject_SelectedIndexChanging"
                                                AllowSorting="true" OnSorting="gvProject_Sorting" DataKeyNames="Project_ID">
                                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                                <RowStyle Font-Size="12px" CssClass="PMSGridRowStyle-css" />
                                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                                <SelectedRowStyle BackColor="#D8F6CE" />
                                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Project">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lblProjectNameHeader" runat="server" CommandName="Sort" CommandArgument="Project_Name"
                                                                ForeColor="White">Projects&nbsp;</asp:LinkButton>
                                                            <img id="Project_Name" runat="server" visible="false" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnbProjectName" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Project_Name") %>'></asp:LinkButton>
                                                            <asp:Label ID="lblProjectID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Project_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="350px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <table cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td style="border-color: transparent">
                                                                        <asp:ImageButton ID="ImgProjectDelete" runat="server" ForeColor="Black" ToolTip="Delete"
                                                                            ImageUrl="~/purchase/Image/Delete.gif" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                                            OnCommand="ImgProjectDelete_Click" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.Project_ID") %>'
                                                                            Width="16px" Height="12px"></asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 25%">
                    <table cellpadding="1" cellspacing="1" width="100%">
                        <tr style="height: 40px">
                            <td align="right" style="width: 15%; height: 20px;">
                                Search :&nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSearchModule" Width="100%" runat="server" CssClass="txtInput"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                    TargetControlID="txtSearchModule" WatermarkText="Type to search Module" WatermarkCssClass="watermarked" />
                            </td>
                            <td align="center" style="width: 10%">
                                <asp:ImageButton ID="imgModuleSearch" ImageUrl="~/Purchase/Image/preview.gif" runat="server"
                                    OnClick="imgModuleSearch_Click" />
                            </td>
                            <td align="center" style="width: 10%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div id="DivModuleGridHolder" style="overflow-x: hidden; overflow-y: scroll; height: 450px;
                                    border: 1px solid #cccccc;">
                                    <asp:UpdatePanel runat="server" ID="UpdModuleGrid" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvModule" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvModule_RowDataBound"
                                                Width="100%" GridLines="Both" OnSelectedIndexChanging="gvModule_SelectedIndexChanging"
                                                AllowSorting="true" OnSorting="gvModule_Sorting" DataKeyNames="Module_ID">
                                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                                <RowStyle Font-Size="12px" CssClass="PMSGridRowStyle-css" />
                                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                                <SelectedRowStyle BackColor="#D8F6CE" />
                                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Module">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lblModuleNameHeader" runat="server" CommandName="Sort" CommandArgument="Module_Name"
                                                                ForeColor="White">Module&nbsp;</asp:LinkButton>
                                                            <img id="Module_Name" runat="server" visible="false" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnbModuleName" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Module_Name") %>'></asp:LinkButton>
                                                            <asp:Label ID="lblModuleID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Module_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="350px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <table cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td style="border-color: transparent">
                                                                        <asp:ImageButton ID="ImgModuleDelete" runat="server" ForeColor="Black" ToolTip="Delete"
                                                                            ImageUrl="~/purchase/Image/Delete.gif" OnCommand="ImgModuleDelete_Click" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.Module_ID") %>' Width="16px"
                                                                            Height="12px"></asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%">
                    <table cellpadding="1" cellspacing="1" width="100%">
                        <tr style="height: 40px">
                            <td align="right" style="width: 10%; height: 20px">
                                Search : &nbsp;&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSearchScreenName" Width="100%" runat="server"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtSearchScreenName"
                                    WatermarkText="Type to search Screen /Assembly" WatermarkCssClass="watermarked" />
                            </td>
                            <td align="center" style="width: 10%">
                                <asp:ImageButton ID="imgScreenSearch" ImageUrl="~/Purchase/Image/preview.gif" runat="server"
                                    OnClick="imgScreenSearch_Click" />
                            </td>
                            <td align="center" style="width: 10%">
                            </td>
                            <td style="width: 40%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <div style="overflow-x: hidden; overflow-y: scroll; height: 450px; border: 1px solid #cccccc;">
                                    <asp:UpdatePanel runat="server" ID="UpdScreenGrid" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvScreens" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvScreens_RowDataBound"
                                                Width="100%" OnSelectedIndexChanging="gvScreens_SelectedIndexChanging" AllowSorting="true"
                                                OnSorting="gvScreens_Sorting" DataKeyNames="Screen_ID">
                                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                                <RowStyle Font-Size="12px" CssClass="PMSGridRowStyle-css" />
                                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                                <SelectedRowStyle BackColor="#D8F6CE" />
                                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Screen Name">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lblScreenNameHeader" runat="server" CommandName="Sort" CommandArgument="Screen_Name"
                                                                ForeColor="White">Screen Name&nbsp;</asp:LinkButton>
                                                            <img id="Screen_Name" runat="server" visible="false" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblScreenName" Visible="true" runat="server" CommandName="Select"
                                                                Text='<%# DataBinder.Eval(Container,"DataItem.Screen_Name") %>'></asp:LinkButton>
                                                            <asp:Label ID="lblScreenID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Screen_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="400px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class_Name">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblClassNameHeader" runat="server" ForeColor="White">Class&nbsp;</asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClassName" Visible="true" runat="server" Width="180px" Text='<%# DataBinder.Eval(Container,"DataItem.Class_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Assembly">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblAssemblyNameHeader" runat="server" ForeColor="White">Assembly&nbsp;</asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAssemblyName" Visible="true" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container,"DataItem.Assembly_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Image_Path">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblImagePathHeader" runat="server" ForeColor="White">Image Path&nbsp;</asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblImagePath" Visible="true" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container,"DataItem.Image_Path") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <table cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td style="border-color: transparent">
                                                                        <asp:ImageButton ID="ImgScreenDelete" runat="server" ForeColor="Black" ToolTip="Delete"
                                                                            ImageUrl="~/purchase/Image/Delete.gif" OnCommand="ImgScreenDelete_Click" CommandArgument='<%#Eval("Screen_ID")%>'
                                                                            OnClientClick="return confirm('Are you sure, you want to  delete ?')" Width="16px"
                                                                            Height="12px"></asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindScreen" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 25%; vertical-align: top">
                    <asp:UpdatePanel runat="server" ID="UpdProjectEntry" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="border: 1px solid #cccccc; background-color: #E0F8F1; width: 100%; height: 210px">
                                <table cellpadding="2" cellspacing="2" width="100%">
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%" align="right">
                                            Project :&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtProject" runat="server" Width="250px" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblProjectCreatedBy" runat="server" Font-Size="9" ForeColor="#000099"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblProjectModifiedBy" runat="server" Font-Size="9" ForeColor="#000099"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblProjectDeletedBy" runat="server" Font-Size="9" ForeColor="#000099"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="2" align="left">
                                            <asp:Button ID="btnProjectAddNew" runat="server" Text="Add New" CssClass="button-css"
                                                OnClick="btnProjectAddNew_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnProjectSave" runat="server" Text="Save" OnClientClick="return ValidationOnProject();"
                                                CssClass="button-css" OnClick="btnProjectSave_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 25%; vertical-align: top">
                    <asp:UpdatePanel runat="server" ID="UpdModuleEntry" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="border: 1px solid #cccccc; background-color: #E0F8F1; width: 100%; height: 210px">
                                <table cellpadding="2" cellspacing="2" width="100%">
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%" align="right">
                                            Module :&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtModule" runat="server" Width="250px" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblModuleCreatedBy" runat="server" ForeColor="#000099"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblModuleModifiedBy" runat="server" ForeColor="#000099"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblModuleDeletedBy" runat="server" ForeColor="#000099"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="2" align="left">
                                            <asp:Button ID="btnModuleAddNew" runat="server" Text="Add New" CssClass="button-css"
                                                OnClick="btnModuleAddNew_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnModuleSave" runat="server" Text="Save" CssClass="button-css" OnClientClick="return ValidationOnModule();"
                                                OnClick="btnModuleSave_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 50%; vertical-align: top">
                    <div style="border: 1px solid #cccccc; background-color: #E0F8F1; width: 100%; height: 210px">
                        <asp:UpdatePanel runat="server" ID="UpdScreenEntry" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table cellpadding="2" cellspacing="2" width="100%">
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%" align="right">
                                            Screen Type :&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlScreenType" runat="server" Width="254px" CssClass="txtInput">
                                                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Project Templete"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Screen"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%" align="right">
                                            Screen Name :&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtScreen" runat="server" Width="250px" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Class :&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtClass" runat="server" Width="250px" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Assembly :&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAssembly" runat="server" Width="250px" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Image Path :&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtImagePath" runat="server" Width="250px" CssClass="txtInput"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblScreenCreatedBy" runat="server" ForeColor="#000099"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblScreenModifiedby" runat="server" ForeColor="#000099"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblScreenDeletedby" runat="server" ForeColor="#000099"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td colspan="2" align="left">
                                            <asp:Button ID="btnScreenAddNew" runat="server" Text="Add New" CssClass="button-css"
                                                OnClick="btnScreenAddNew_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnScreenSave" runat="server" Text="Save" CssClass="button-css" OnClientClick="return ValidationOnScreen();"
                                                OnClick="btnScreenSave_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </asp:Content>

   

