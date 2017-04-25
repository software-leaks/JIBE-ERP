<%@ Page Language="C#" Title="Ship Nav Project Mgmt" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="ShipNavProjectMgmt.aspx.cs"
    Inherits="ShipNavProjectMgmt" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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


        function ValidationOnProject() {

            if (document.getElementById("ctl00_MainContent_txtProject").value == "") {
                document.getElementById("ctl00_MainContent_txtProject").focus();
                alert("Please enter project.");
                return false;
            }


            if (document.getElementById("ctl00_MainContent_ddlProjectTemplete").value == "0") {
                document.getElementById("ctl00_MainContent_ddlProjectTemplete").focus();
                alert("Please select project templete.");
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

            if (document.getElementById("ctl00_MainContent_ddlModuleScreen").value == "0") {
                document.getElementById("ctl00_MainContent_ddlModuleScreen").focus();
                alert("Please select screen class.");
                return false;
            }


            return true;
        }


        function ValidationOnScreen() {


            if (document.getElementById("txtScreen").value == "") {
                alert("Please enter screen.");
                document.getElementById("txtScreen").focus();
                return false;
            }

            if (document.getElementById("txtAssembly").value == "") {
                alert("Please enter Assembly.");
                document.getElementById("txtAssembly").focus();
                return false;
            }

            if (document.getElementById("txtClass").value == "") {
                alert("Please enter Class.");
                document.getElementById("txtClass").focus();
                return false;
            }

            if (document.getElementById("txtImagePath").value == "") {
                alert("Please enter image path.");
                document.getElementById("txtImagePath").focus();
                return false;
            }

            return true;
        }


        function RefreshFromchild() {

            document.getElementById("btnHiddenSubmit").click();
        }


        function OpenAssignScreen(Module_ID, class_Name) {
            var url = 'ShipChildScreenAssignment.aspx?Class_Name=' + class_Name + '&Nav_Module_ID=' + Module_ID;
            OpenPopupWindow('AssignScreen', 'Assign Child Screen', url, 'popup', 530, 600, null, null, true, false, true, AssignScreen_Closed);
        }

        function AssignScreen_Closed() {

            return true;
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
              
                    <div style="background-color: #5588BB; color: #5588BB; text-align: center;">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr style="height: 20px">
                                <td align="left" style="color: #FFFFFF">
                                    <asp:LinkButton ID="lnbHome" Width="550px" runat="server" ForeColor="Yellow" OnClick="lnbHome_Click">Home</asp:LinkButton>
                                    <b>Navigation Project /Module Settings</b>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr style="height: 40px">
                            <td align="right" style="width: 10%;">
                                Search :&nbsp;&nbsp;
                            </td>
                            <td align="left" style="width: 45%;">
                                <asp:TextBox ID="txtSearchProject" runat="server" Width="100%"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                    TargetControlID="txtSearchProject" WatermarkText="Type to search Project/Class/Assembly"
                                    WatermarkCssClass="watermarked" />
                            </td>
                            <td align="center" style="width: 10%">
                                <asp:ImageButton ID="imgProjectSearch" ImageUrl="~/Purchase/Image/preview.gif" runat="server"
                                    OnClick="imgProjectSearch_Click" />
                            </td>
                            <td align="center" style="width: 35%">
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
                                                            <asp:LinkButton ID="lblProjectNameHeader" runat="server" CommandName="Sort" CommandArgument="Name"
                                                                ForeColor="White">Projects&nbsp;</asp:LinkButton>
                                                            <img id="Project_Name" runat="server" visible="false" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnbProjectName" CommandName="Select" Width="200px" runat="server"
                                                                Text='<%# DataBinder.Eval(Container,"DataItem.Name") %>'></asp:LinkButton>
                                                            <asp:Label ID="lblProjectID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Project_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lblProjClassNameHeader" runat="server" CommandName="Sort" CommandArgument="Class"
                                                                ForeColor="White">Class&nbsp;</asp:LinkButton>
                                                            <img id="Class" runat="server" visible="false" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjClassName" Visible="true" runat="server" Width="250px" Text='<%# DataBinder.Eval(Container,"DataItem.Class") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Assembly">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lblProjAssemblyHeader" runat="server" CommandName="Sort" CommandArgument="Assembly"
                                                                ForeColor="White">Assembly&nbsp;</asp:LinkButton>
                                                            <img id="Assembly" runat="server" visible="false" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjAssembly" Visible="true" runat="server" Width="150px" Text='<%# DataBinder.Eval(Container,"DataItem.Assembly") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ImagePath">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lblProjImagePathHeader" runat="server" CommandName="Sort" CommandArgument="Image_Path"
                                                                ForeColor="White">Image Path&nbsp;</asp:LinkButton>
                                                            <img id="Image_Path" runat="server" visible="false" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjImagePath" Visible="true" Width="150px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Image_Path") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
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
                            <td align="right" style="width: 10%;">
                                Search :&nbsp;&nbsp;
                            </td>
                            <td align="left" style="width: 45%;">
                                <asp:TextBox ID="txtSearchModule" Width="100%" runat="server"></asp:TextBox>
                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                    TargetControlID="txtSearchModule" WatermarkText="Type to search Project/Class/Assembly"
                                    WatermarkCssClass="watermarked" />
                            </td>
                            <td align="center" style="width: 10%">
                                <asp:ImageButton ID="imgModuleSearch" ImageUrl="~/Purchase/Image/preview.gif" runat="server"
                                    OnClick="imgModuleSearch_Click" />
                            </td>
                            <td align="center" style="width: 35%">
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
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <table cellpadding="2" cellspacing="0">
                                                                <tr>
                                                                    <td style="border-color: transparent">
                                                                        <asp:ImageButton ID="ImgDefaultModule" runat="server" ForeColor="Black" 
                                                                            ImageUrl="~/Images/Default.png" OnCommand="ImgDefaultModule_Click" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.Module_ID") %>'
                                                                            Width="16px" Height="12px"></asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="35px" CssClass="PMSGridItemStyle-css" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Module">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lblModuleNameHeader" runat="server" CommandName="Sort" CommandArgument="Name"
                                                                ForeColor="White">Module&nbsp;</asp:LinkButton>
                                                            <img id="Module_Name" runat="server" visible="false" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnbModuleName" CommandName="Select" Width="200px" runat="server"
                                                                Text='<%# DataBinder.Eval(Container,"DataItem.Name") %>'></asp:LinkButton>
                                                            <asp:Label ID="lblModuleID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Module_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lblModuleClassNameHeader" runat="server" CommandName="Sort" CommandArgument="Class"
                                                                ForeColor="White">Class&nbsp;</asp:LinkButton>
                                                            <img id="Class" runat="server" visible="false" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblModuleClassName" Visible="true" Width="250px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Class") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Assembly">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lblModuleAssemblyHeader" runat="server" CommandName="Sort" CommandArgument="Assembly"
                                                                ForeColor="White">Assembly&nbsp;</asp:LinkButton>
                                                            <img id="Assembly" runat="server" visible="false" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblModuleAssembly" Visible="true" Width="150px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Assembly") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ImagePath">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lblModuleImagePathHeader" runat="server" CommandName="Sort" CommandArgument="Image_Path"
                                                                ForeColor="White">Image Path&nbsp;</asp:LinkButton>
                                                            <img id="Image_Path" runat="server" visible="false" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblModuleImagePath" Visible="true" Width="150px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Image_Path") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css"></ItemStyle>
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

                                                                    <td style="border-color: transparent">
                                                                       <img src="../../images/e-ticket.gif" alt="E-Tickets"  onclick="OpenAssignScreen('<%# DataBinder.Eval(Container,"DataItem.Module_ID")%>','<%# DataBinder.Eval(Container,"DataItem.Class") %>' );return false;" />
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
            </tr>
            <tr>
                <td style="width: 25%; vertical-align: top">
                    <asp:UpdatePanel runat="server" ID="UpdProjectEntry" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="border: 1px solid #cccccc; background-color: #E0F8F1; width: 100%; height: 220px">
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
                                        <td style="width: 18%" align="right">
                                            Project Template :&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlProjectTemplete" Width="252px" runat="server" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%" align="right">
                                            Image Path :&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtProjImagePath" runat="server" Width="250px" CssClass="txtInput"></asp:TextBox>
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
                            <div style="border: 1px solid #cccccc; background-color: #E0F8F1; width: 100%; height: 220px">
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
                                        <td align="right">
                                            Screen Class :&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlModuleScreen" runat="server" Width="250px" CssClass="txtInput">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Image Path :&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtModuleImagePath" runat="server" Width="250px" CssClass="txtInput"></asp:TextBox>
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
            </tr>
        </table>
    </div>
     </asp:Content>

