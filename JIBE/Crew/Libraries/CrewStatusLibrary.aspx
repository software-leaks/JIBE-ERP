<%@ Page Title="Crew Status Library" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="CrewStatusLibrary.aspx.cs" Inherits="Crew_Libraries__CrewStatusLibrary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
     <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/crew_interview_css.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
           body, html
        {
            overflow-x: hidden;
        }
        .header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
           
        }
        h4
        {
            font-size: 1.2em;
            color: #ffffff;
            font-weight: bold;
            margin: 0 0 0 5px;
        }
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        .watermarked
        {
            color: #cccccc;
        }
        
        .linkbtn a
        {
            color: Black;
        }
        
        .linkbtn
        {
            color: black;
            background-color: white;
            text-decoration: none;
            border-left: 1px solid #cccccc;
            padding-left: 10px;
            border-top: 1px solid #cccccc;
            padding-top: 5px;
            border-right: 1px solid #cccccc;
            padding-right: 10px;
            border-bottom: 1px solid #cccccc;
            padding-bottom: 3px;
            background-color: #F1F8E0;
        }
        #dvAddNewStatus
        {
            cursor: move;
        }      
        #dvAddNewType
        {
            cursor: move;
        }
    </style>
    <style id="TooltipStyle" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: Tahoma;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .grid-row
        {
            padding: 6px;
        }
        .grid-col-fixed
        {
            border: 1px solid #cccccc;
        }
        .grid-col
        {
            border: 1px solid #cccccc;
        }
        .gradiant-css-browne
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#81F79F',EndColorStr='#088A4B');
            background: -webkit-gradient(linear, left top, left bottom, from(#81F79F), to(#088A4B));
            background: -moz-linear-gradient(top,  #81F79F,  #088A4B);
            color: Black;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
    </style>
    <script language="javascript" type="text/javascript">
     function Divaddnewlink() {
            document.getElementById("divadd").style.display = "block";
        }

        function validation() {

            if (document.getElementById("ctl00_MainContent_txtStatus").value.trim() == "") {
                alert("Please enter status name.");
                document.getElementById("ctl00_MainContent_txtStatus").focus();
                return false;
            }       

            if (document.getElementById("ctl00_MainContent_txtValue").value.trim() == "") {
                alert("Please enter status value.");
                document.getElementById("ctl00_MainContent_txtValue").focus();
                return false;
            }               
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 
    <div class="page-title">
        Crew Status</div>
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
 
    <div id="page-content" style="height: 620px; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>&nbsp;Status :</legend>
                        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                            <td style="text-align: left">
                                                Filter:&nbsp;<asp:TextBox ID="txtfilter" runat="server" AutoPostBack="true" OnTextChanged="txtfilter_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                                    WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                            </td>
                                                <td align="center" style="width: 5%">
                                             <asp:ImageButton ID="ibtnRefStatus" runat="server" OnClick="ibtnRefStatus_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                          </td>
                                          <td align="center" style="width: 5%">                                    
                                                <asp:ImageButton ID="ImgAddStatus" runat="server" ToolTip="Add New Status" OnClick="ImgAddStatus_Click"
                                            ImageUrl="~/Images/Add-icon.png" />
                                            </td>
                                        </tr>
                                    </table>
                                   <div style="width: 100%; height: 400px; overflow: scroll">
                                    <asp:GridView ID="GridView_Status" runat="server" AutoGenerateColumns="False" OnRowUpdating="GridView_Status_RowUpdating"
                                        OnRowEditing="GridView_Status_RowEditing"
                                        OnRowCancelingEdit="GridView_Status_RowCancelEdit" DataKeyNames="ID" EmptyDataText="No Record Found"
                                        AllowSorting="True" CaptionAlign="Bottom" CellPadding="4" 
                                        GridLines="None" Width="100%" CssClass="gridmain-css">
                                       <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                       <RowStyle CssClass="RowStyle-css" />
                                       <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus_Name" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtStatus_Name" Font-Size="11px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("Name")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Value ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValue" runat="server" Text='<%#Eval("Value")%>'></asp:Label>
                                                </ItemTemplate>
                                          <%--      <EditItemTemplate>
                                                    <asp:TextBox ID="txtValue" Font-Size="11px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("Value")%>'></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="50px">
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnAccept" ToolTip="Update" runat="server" ImageUrl="~/images/accept.png"
                                                        CausesValidation="False" CommandName="Update" AlternateText="Update"></asp:ImageButton>
                                                    <asp:ImageButton ID="ImgBtnCancel" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                                        CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                                        CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>                               
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                    </div> 
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </td>
                 <td style="width: 50%; vertical-align: top;">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Sub Status:</legend>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="text-align: center">
                                   <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                        <td style="text-align: left">
                                                Filter:&nbsp;<asp:TextBox ID="txtfilterCal_Status" runat="server" AutoPostBack="true" OnTextChanged="txtfilterCal_Status_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtfilterCal_Status"
                                                    WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                            </td>
                                    <td align="center" style="width: 5%">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                           <td align="center" style="width: 5%">                      
                                          <asp:ImageButton ID="ImgAdd_C_Status" runat="server" ToolTip="Add New Sub Status" 
                                            ImageUrl="~/Images/Add-icon.png" onclick="ImgAdd_C_Status_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="width: 100%; height: 400px; overflow: scroll">
                                    <asp:GridView ID="GridView_Calc_Status" runat="server" AutoGenerateColumns="False"
                                        OnRowUpdating="GridView_Calc_Status_RowUpdating" 
                                        OnRowEditing="GridView_Calc_Status_RowEditing" OnRowCancelingEdit="GridView_Calc_Status_RowCancelEdit"
                                        DataKeyNames="ID" EmptyDataText="No Record Found" AllowSorting="True" CaptionAlign="Bottom"
                                        CellPadding="4"  GridLines="None" Width="100%" CssClass="grd" >
                                      <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                      <RowStyle CssClass="RowStyle-css" />
                                      <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sub Status ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCalculated_Status" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEvaluation_Type" Font-Size="11px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("Name")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Value ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValue" runat="server" Text='<%#Eval("Value")%>'></asp:Label>
                                                </ItemTemplate>
                                         <%--       <EditItemTemplate>
                                                    <asp:TextBox ID="txtValue" Font-Size="11px" MaxLength="50" runat="server"
                                                        Text='<%#Bind("Value")%>'></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-Width="50px">
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnAccept" ToolTip="Update" runat="server" ImageUrl="~/images/accept.png"
                                                        CausesValidation="False" CommandName="Update" AlternateText="Update"></asp:ImageButton>
                                                    <asp:ImageButton ID="ImgBtnCancel" ToolTip="Cancel" runat="server" ImageUrl="~/images/reject.png"
                                                        CausesValidation="False" CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton2" ToolTip="Edit" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                                        CommandName="Edit" AlternateText="Edit"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:TemplateField>                           
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </div>
                                </div> 
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </td>
            </tr>
            </table>

           <%-- -----  Add status -------%>
           <asp:UpdatePanel  ID="UpdatePanel1" runat="server" >
                                           <ContentTemplate>
                 <div id="divadd" title="<%= OperationMode %>" style="display:none; font-family: Tahoma;
                            text-align: left; font-size: 12px; color: Black; width: 30%">
                            <table border="0" style="width: 100%;" cellpadding="10">
                              
                            <tr>
                                <td style="font-size: 11px; font-weight: bold" align="right">
                                    Status :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStatus" Width="250px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td style="font-size: 11px; font-weight: bold" align="right">
                                    &nbsp;Value :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtValue" Width="250px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveAndAdd" CssClass="button-css" runat="server" Text="Save And Add New" Visible="false"/>&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndClose" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndClose_Click" OnClientClick="return validation();" />&nbsp;&nbsp; 
                                        <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                                    <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClick="btncancel_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>
                     
                        </div>
                 
                            </ContentTemplate>           
                </asp:UpdatePanel>
          <%-- ----- END --------%>

       </div>
</asp:Content>
