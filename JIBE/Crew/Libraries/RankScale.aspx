<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RankScale.aspx.cs" Inherits="Crew_Libraries_RankScale"  MasterPageFile="~/Site.master" Title="Rank Scale"  %>

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
    <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function showDiv(dv) {
            $('#lblMsg').val('');
            showModal(dv);
        }
        function closeDiv(dv) {
            hideModal(dv);
        }
        $(document).ready(function () {
            $('#dvAddNewRankScale').draggable();
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="page-title">
        Rank Scale
    </div>
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
 
    <div id="dvPageContent" style="height: 620px; border: 1px solid #CEE3F6; z-index: -2;
        margin-top: -1px; overflow: auto;"  runat="server">
        
        <table style="width: 70%;">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px;">
                        <legend>Rank Scale:</legend>
                        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <table cellpadding="0" cellspacing="5" style="width: 100%;">
                                        <tr>                                          
                                            <td align="left">
                                                Rank
                                            </td>
                                            <td class="data">
                                                <asp:DropDownList ID="ddlRank1" runat="server" Width="156px" CssClass="control-edit required" OnSelectedIndexChanged="ddlRank1_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                            
                                            <td style="text-align: right">
                                                <asp:LinkButton ID="lnkAddNewGroup" OnClientClick="javascript:showDiv('dvAddNewRankScale');return false;"
                                                    runat="server" CssClass="linkbtn">Add New Rank Scale</asp:LinkButton>
                                            </td>
                                        </tr>
                                       
                                    </table>
                                    <asp:GridView ID="gvRankScale" runat="server" AutoGenerateColumns="False" OnRowUpdating="gvRankScale_RowUpdating"
                                        OnRowDeleting="gvRankScale_RowDeleting" OnRowEditing="gvRankScale_RowEditing"
                                        OnRowCancelingEdit="gvRankScale_RowCancelEdit" DataKeyNames="Id" EmptyDataText="No Record Found"
                                        AllowSorting="True" CaptionAlign="Bottom" CellPadding="4" 
                                        GridLines="None" Width="100%" CssClass="gridmain-css">
                                       <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                       <RowStyle CssClass="RowStyle-css" />
                                       <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                        <Columns>
                                            
                                            <asp:TemplateField HeaderText="Rank">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRank_Name" runat="server" Text='<%#Eval("Rank_Name")%>'></asp:Label>                                                   
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"  Width="200px"  />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rank Scale">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRankScaleName" runat="server" Text='<%#Eval("RankScaleName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblRankId" runat="server" Text='<%#Eval("RankId")%>' Visible="false" ></asp:Label>
                                                    <asp:TextBox ID="txtRank_Name" Font-Size="11px" MaxLength="50" runat="server" Text='<%#Bind("RankScaleName")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"  Width="200px" />
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
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1del" ToolTip="Delete" runat="server" ImageUrl="~/images/delete.png"
                                                        CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure, you want to  delete ?')"
                                                        AlternateText="Delete"></asp:ImageButton>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </td>
            </tr>
        </table>

         <div id="dvAddNewRankScale" title="Add New Rank Scale" class="modal-popup-container"
               style="width: 550px; left: 40%; top: 30%;">    
           <div class="modal-popup-content">
               <%-- <asp:UpdatePanel ID="UpdatePanelnew" runat="server">
                    <ContentTemplate>--%>
                       <div class="error-message" onclick="javascript:this.style.display='none';">
                            <asp:Label ID="lblMsg" runat="server" Visible="false" Text="" ></asp:Label>
                        </div>
                       <table border="0" style="width: 100%;" cellpadding="10">
                            <tr>
                                <td style="font-size: 11px; font-weight: bold">
                                    Rank
                                </td>
                                <td class="data">
                                    <asp:DropDownList ID="ddlRank" runat="server" Width="156px" CssClass="control-edit required" >
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 11px; font-weight: bold">
                                    Rank Scale Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRankScaleName" Width="250px" runat="server" Text=""></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="rfvRankScaleName" runat="server"
                                    ValidationGroup="Validate" Display="None" ErrorMessage="Rank Scale Name is mandatory!"
                                    ControlToValidate="txtRankScaleName" InitialValue=""></asp:RequiredFieldValidator>
                                    <tlk4:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rfvRankScaleName"
                                                runat="server">
                                     </tlk4:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: 11px; text-align: center;">
                                    <asp:Button ID="btnSaveAndAdd" CssClass="button-css" runat="server" Text="Save And Add New"
                                        OnClick="btnsave_Click"  ValidationGroup="Validate"/>&nbsp;&nbsp;
                                    <asp:Button ID="btnSaveAndClose" CssClass="button-css" runat="server" Text="Save And Close"
                                        OnClick="btnSaveAndClose_Click" ValidationGroup="Validate" />&nbsp;&nbsp;
                                    <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Close" OnClientClick="closeDiv('dvAddNewRankScale')" />
                                </td>
                            </tr>
                        </table>
                 <%--   </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
</asp:Content>