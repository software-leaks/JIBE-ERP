 



<%@ Page Title="Severity Consequences" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SeverityConsequences.aspx.cs" Inherits="JRA_Libraries_SeverityConsequences" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function Validation() {
         

            return true;
        }
      

      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;
            height: 100%;">
            <div class="page-title">
                Severity Consequences
            </div>
            <div style="height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 240px;">
                            <table width="40%" cellpadding="4" cellspacing="4">
                                 <tr>
                                    <td align="left" valign="top" valign="top">
                                        Severity &nbsp;:&nbsp;
                                    </td>
                                    
                                    <td valign="top" align="left">
                                        <asp:DropDownList ID="ddlSeverity" CssClass="txtInput"   runat="server"
                                              AutoPostBack="true" 
                                            onselectedindexchanged="ddlSeverity_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                  <tr>
                                    <td align="left" valign="top" valign="top" align="left">
                                        Consequence &nbsp;:&nbsp;
                                    </td>
                                     
                                    <td valign="top" align="left">
                                        <asp:DropDownList ID="ddlCons" CssClass="txtInput"   runat="server"
                                           AutoPostBack="true" onselectedindexchanged="ddlCons_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="left" valign="top" valign="top" align="left">
                                        Description &nbsp;:&nbsp;
                                    </td>
                                     
                                    <td valign="top" align="left">
                                       <asp:TextBox ID="txtDesc" Text="" runat="server" MaxLength="500" Height="78px" Width="250px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                <td></td><td align="left">  <asp:Button ID="btnSave" runat="server" Text="Save" 
                                        onclick="btnSave_Click" /></td>
                                  
                                </tr>
                            </table>
                        </div>
                         

                       

                    </ContentTemplate>
                  
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
