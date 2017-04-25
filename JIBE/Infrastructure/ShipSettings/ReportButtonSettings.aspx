<%@ Page Language="C#" Title="Button Settings" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="ReportButtonSettings.aspx.cs"
    Inherits="ReportButtonSettings" EnableEventValidation="false" %>

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

          .style1
          {
              width: 3%;
          }

          .style2
          {
              width: 268435376px;
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
                             Report Button Settings
                             </div> 
                             </td>
            </tr>
            <tr>
                <td  class="style2" style="width:15%">                 
                                <div id="DivProjectGridHolder" style="overflow-x: hidden; overflow-y: scroll; width:100%; height: 450px;
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
<%--                                                            <asp:LinkButton ID="lblProjectNameHeader" runat="server" CommandName="Sort" CommandArgument="Project_Name"
                                                                ForeColor="Black">Projects&nbsp;</asp:LinkButton>
                                                            <img id="Project_Name" runat="server" visible="false" />                                                         --%>
                                                            Project
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnbProjectName" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Project_Name") %>'></asp:LinkButton>
                                                            <asp:Label ID="lblProjectID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Project_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="350px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>                      
                </td>   
                                             
                <td class="style2" style="width:40%" >
                                <div style="overflow-x: hidden; overflow-y: scroll; width:100%; height: 450px; border: 1px solid #cccccc;">
                                    <asp:UpdatePanel runat="server" ID="UpdScreenGrid" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvScreens" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvScreens_RowDataBound"
                                                Width="100%" AllowSorting="true"
                                                OnSorting="gvScreens_Sorting" DataKeyNames="Screen_ID">
                                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                                <RowStyle Font-Size="12px" CssClass="PMSGridRowStyle-css" />
                                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                                <SelectedRowStyle BackColor="#D8F6CE" />
                                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Screen Name">
                                                        <HeaderTemplate>
                                                            Screen Name&nbsp;
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblScreenName" Visible="true" runat="server" OnCommand="SelectScreen" CommandArgument='<%#Eval("Screen_ID")%>'
                                                                Text='<%# DataBinder.Eval(Container,"DataItem.Screen_Name") %>'></asp:LinkButton>
                                                            <asp:Label ID="lblScreenID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Screen_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="200px" CssClass="PMSGridItemStyle-css">
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
                                                    <asp:TemplateField >
                                                     <HeaderTemplate>
                                                            <asp:Label ID="lblAddButton" runat="server" ForeColor="White">Add Key&nbsp;</asp:Label>
                                                        </HeaderTemplate>
                                                    <ItemTemplate>
                                                    <asp:ImageButton ID="ImgbtnAdd" runat="server" ToolTip="Add Buttons" OnCommand="onAddButton" CommandArgument='<%#Eval("Screen_ID")%>'
                                            ImageUrl="~/Images/Add-icon.png" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="PMSGridItemStyle-css">
                                                   </ItemStyle>
                                            </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server"  Visible="false" OnBindDataItem="BindScreen"/>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                </td>   
                                                  
                <td  class="style2" style="width:45%">
                                <div style="overflow-x: hidden; overflow-y: scroll; width:100%; height: 450px; border: 1px solid #cccccc;">
                                    <asp:UpdatePanel runat="server" ID="upDetails" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False"
                                                Width="100%" AllowSorting="true" OnRowDeleting="gvDetails_RowDeleting" 
                                                OnSorting="gvScreens_Sorting" DataKeyNames="Screen_ID,Details_ID">
                                                <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                                <RowStyle Font-Size="12px" CssClass="PMSGridRowStyle-css" />
                                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                                <SelectedRowStyle BackColor="#D8F6CE" />
                                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Screen Name">
                                                        <HeaderTemplate>
                                                            Screen Name&nbsp;
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblScreenName" Visible="true" runat="server" CommandName="SelectScreen"
                                                                Text='<%# DataBinder.Eval(Container,"DataItem.Screen_Name") %>'></asp:Label>
                                                            <asp:Label ID="lblScreenID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Screen_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" Width="300px" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>                                                  
                                                      <asp:TemplateField HeaderText="Rank Name">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblKeyHeader" runat="server" ForeColor="White">Key Name&nbsp;</asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblKey" Visible="true" runat="server" Width="180px" Text='<%# DataBinder.Eval(Container,"DataItem.Key") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Rank Name">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblRank_NameHeader" runat="server" ForeColor="White">Rank Name&nbsp;</asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClassName" Visible="true" runat="server" Width="180px" Text='<%# DataBinder.Eval(Container,"DataItem.Rank_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" CssClass="PMSGridItemStyle-css">
                                                        </ItemStyle>
                                                    </asp:TemplateField>                                                    
                                                     <asp:TemplateField HeaderText="Edit">
                                                      <HeaderTemplate>
                                                            <asp:Label ID="lblEdit" runat="server" ForeColor="White">Edit&nbsp;</asp:Label>
                                                        </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit" CausesValidation="False" OnCommand="Edit" CommandArgument='<%# Eval("Id")%>'  
                                                    ImageUrl="~/images/edit.gif" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                               <HeaderTemplate>
                                                            <asp:Label ID="lblDelete" runat="server" ForeColor="White">Delete&nbsp;</asp:Label>
                                                        </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
                                                        CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                        AlternateText="Delete"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" CssClass="PMSGridItemStyle-css"></ItemStyle>
                                            </asp:TemplateField>                       
                                                </Columns>
                                            </asp:GridView>
                                            <uc1:ucCustomPager ID="ucCustomPager1" runat="server" OnBindDataItem="BindScreen" Width="50px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                </td>
               </tr>     
                 </table >                    
        <div id="dvAddButton" title="Add Button" class="modal-popup-container" 
               style="width: 400px; left: 40%; top: 30%; display:none;">    
               <asp:UpdatePanel ID="UpdScreenEntry" runat="server"  UpdateMode="Conditional"> 
                <ContentTemplate>
                   <div class="modal-popup-content">
                <div class="error-message" onclick="javascript:this.style.display='none';">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Visible="false" Text="" ></asp:Label>
                </div>
                <table border="0" style="width: 100%;" cellpadding="10">
                        <tr>
                        <td style="font-size: 11px; font-weight: bold">
                            Button Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtButtonName" Width="250px" runat="server" Text=""></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvContractName" runat="server"
                            ValidationGroup="Validate" ErrorMessage="*"
                            ControlToValidate="txtButtonName" InitialValue=""></asp:RequiredFieldValidator>  
                            <br/>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Only alphabets are allowed" ControlToValidate="txtButtonName"  ValidationGroup="Validate"
                             ValidationExpression="[a-zA-Z]+"></asp:RegularExpressionValidator>                           
                        </td>
                        </tr>
                        <tr>
                         <td style="font-size: 11px; font-weight: bold">
                             &nbsp;</td>
                         <td style="font-size: 11px; font-weight: bold" >
                             Select Rank</td>
                        </tr>
                        <tr>
                        <td style="font-size: 11px; font-weight: bold align="right" >
                            
                            &nbsp;</td>
                        <td style="font-size: 11px; font-weight:normal">
                            <div style="height:480px; overflow:auto;">
                                <asp:CheckBoxList ID="chkRanksList" runat="server" 
                                    onselectedindexchanged="chkRanksList_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </div>
                            </td>
                        <tr>
                            <td>
                                &nbsp;</td>                          
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveAndClose" runat="server" CssClass="button-css" 
                                         Text="Save" ValidationGroup="Validate" onclick="btnSaveAndClose_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btncancel" runat="server" CssClass="button-css" 
                                        OnClientClick="hideModal('dvAddButton');" Text="Close" />
                                </td>
                            </tr>
                        </tr>
                        
                </table>                
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
       
    </div>
    </asp:Content>

   

