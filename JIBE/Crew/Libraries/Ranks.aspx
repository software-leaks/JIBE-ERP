<%@ Page Title="Crew Ranks" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Ranks.aspx.cs" Inherits="Crew_Libraries_Ranks" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
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

        function validationOnSave() {

            if (document.getElementById("ctl00_MainContent_txtRankName").value.trim() == "") {
                alert("Please enter rank name.");
                document.getElementById("ctl00_MainContent_txtRankName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_txtRankShortName").value.trim() == "") {
                alert("Please enter rank short name.");
                document.getElementById("ctl00_MainContent_txtRankShortName").focus();
                return false;
            }

            if (document.getElementById("ctl00_MainContent_ddlRankCategory").value == "0") {
                alert("Please select rank category.");
                document.getElementById("ctl00_MainContent_ddlRankCategory").focus();
                return false;
            }

            return true;
        }
           
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

  
 <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>

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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 80%;
            height: 100%;">
           <div class="page-title">
                 Crew Rank
          </div>
            <div style="height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="padding-top: 5px; padding-bottom: 5px; height: 35px;">
                            <table width="100%" cellpadding="2" cellspacing="1">
                                <tr>
                                    <td align="right" style="width: 15%">
                                        Name / Short Name :&nbsp;
                                    </td>
                                    <td align="left" style="width: 20%">
                                        <asp:TextBox ID="txtfilter" runat="server" Width="80%" CssClass="txtInput"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                            WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                    </td>
                                    <td align="right" style="width: 8%">
                                        Category :&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="DDLRankCategoryFilter" Width="150px" runat="server"
                                            AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center">
                                        <asp:RadioButtonList ID="optDeckEnginefilter" runat="server" Font-Size="11px" RepeatDirection="Horizontal"
                                            CssClass="txtInput">
                                            <asp:ListItem Selected="True" Text="ALL&nbsp;" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Deck&nbsp;" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Engine&nbsp;" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" ToolTip="Search"
                                            ImageUrl="~/Images/SearchButton.png" />
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            ImageUrl="~/Images/Refresh-icon.png" />
                                    </td>
                                    <td align="center" style="width: 30px">
                                        <asp:ImageButton ID="ImgAddRank" runat="server" ToolTip="Add New Rank" OnClick="ImgAddRank_Click"
                                            ImageUrl="~/Images/Add-icon.png" />
                                    </td>
                                    <td style="width: 5%">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <div>
                                <asp:GridView ID="GridViewRank" runat="server" EmptyDataText="NO RECORDS FOUND" ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="False" OnRowDataBound="GridViewRank_RowDataBound" DataKeyNames="ID"
                                    OnRowCommand="GridViewRank_RowCommand" CellPadding="2" CellSpacing="0" Width="100%"
                                    GridLines="both" OnSorting="GridViewRank_Sorting" AllowSorting="true" CssClass="gridmain-css">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                   <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />

                                    <PagerStyle CssClass="PMSPagerStyle-css" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Order"  >
                                           <HeaderTemplate>
                                                <asp:LinkButton ID="lblRankOrder" runat="server" CommandName="Sort" CommandArgument="Rank_Sort_Order"
                                                    ForeColor="Black">Order&nbsp;</asp:LinkButton>
                                                <img id="Rank_Sort_Order" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgBtnMoveUp" runat="server" ImageUrl="~/images/Arrow2 - Up.png"
                                                                CommandName="MoveUp" CommandArgument='<%#Eval("ID")%>' AlternateText="Up"></asp:ImageButton>
                                                        </td>
                                                        <td style="padding-left:6px;">
                                                            <asp:Label ID="lblRandSortNo" runat="server"  Text='<%#Eval("Rank_Sort_Order")%>' ></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;<asp:ImageButton ID="ImgBtnMoveDown" runat="server" ImageUrl="~/images/Arrow2 - Down.png"
                                                                CommandName="MoveDown" CommandArgument='<%#Eval("ID")%>' AlternateText="Down">
                                                            </asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="30px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblRankNameHeader" runat="server" CommandName="Sort" CommandArgument="Rank_Name"
                                                    ForeColor="Black">Name&nbsp;</asp:LinkButton>
                                                <img id="Rank_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblRankName" Style="color: Black" runat="server" Text='<%#Eval("Rank_Name")%>'
                                                    CommandArgument='<%#Eval("ID")%>' OnCommand="onUpdate"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="250px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Short Name">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblRank_Short_NameHeader" runat="server" CommandName="Sort" CommandArgument="Rank_Short_Name"
                                                    ForeColor="Black">Short Name&nbsp;</asp:LinkButton>
                                                <img id="Rank_Short_Name" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRank_Short_Name" runat="server" Text='<%#Eval("Rank_Short_Name")%>'  onclick='<%# "Get_Record_Information(&#39;CRW_LIB_Crew_Ranks&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("category_Name") %>'  onclick='<%# "Get_Record_Information(&#39;CRW_LIB_Crew_Ranks&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Deck/Engine">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeckEngine" runat="server" Text='<%# Eval("DeckOrEngine") %>'  onclick='<%# "Get_Record_Information(&#39;CRW_LIB_Crew_Ranks&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                       </asp:TemplateField>

                                         <asp:TemplateField HeaderText="IsDeckOfficer" >
                                            <ItemTemplate>
                                            <asp:CheckBox runat="server" Enabled="false" ID="chkOfficer" Checked='<%#Convert.ToBoolean(Eval("DeckOfficer")) %>'/>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                                <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                    Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black"
                                                                    ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                          </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='false' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[ID]")%>' ForeColor="Black" ToolTip="Delete" ImageUrl="~/Images/delete.png"
                                                                Height="16px"></asp:ImageButton>
                                                        </td>
                                                         <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;CRW_LIB_Crew_Ranks&#39;,&#39;ID="+Eval("ID").ToString()+"&#39;,event,this)" %>' />                                                                                                        
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" OnBindDataItem="BindRankGrid" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                        </div>
                        <div id="dvAddNewRank" title="<%= OperationMode %>" style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 20%;">
                            <center>
                                <table width="98%" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td align="right" style="width: 18%; padding: 5px">
                                            Rank Name &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left" style="width: 25%">
                                            <asp:TextBox ID="txtRankName" CssClass="txtInput" Width="90%" MaxLength="140" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Rank Short Name &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtRankShortName" CssClass="txtInput" Width="90%" MaxLength="100" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Category &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                            *
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlRankCategory" runat="server" CssClass="txtInput" Width="92%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>

                                     <tr>
                                        <td align="right">
                                            Deck/Engine &nbsp;:&nbsp;
                                        </td>
                                        <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                        <td align="left">
                                        <asp:RadioButtonList ID="optDeckEngine" runat="server" Font-Size="11px" RepeatDirection="Horizontal"
                                            CssClass="txtInput">
                                         
                                            <asp:ListItem Text="Deck&nbsp;" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Engine&nbsp;" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="None&nbsp;" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td align="right">
                                    Deck Officer &nbsp;:&nbsp;
                                    </td>
                                     <td style="color: #FF0000; width: 1%" align="right">
                                        </td>
                                         <td align="left">
                                          <asp:CheckBox ID="chkDeckOfficer" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center" style="font-size: 11px; border-width: 1px; padding: 10px">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validationOnSave();" />&nbsp;&nbsp;
                                            <asp:TextBox ID="txtRankID" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                            <asp:TextBox ID="txtRankOrder" runat="server" Visible="false" Width="1px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="right" style="color: #FF0000; font-size: small;">
                                            * Mandatory fields
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
