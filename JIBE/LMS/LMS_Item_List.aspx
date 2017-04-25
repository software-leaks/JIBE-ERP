<%@ Page Title="Item List" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="LMS_Item_List.aspx.cs" Inherits="LMS_Training_Item_List" %>

<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="auc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 0px 3px 0px 0px;
            width: 120px;
            height: 20px;
            vertical-align: middle;
            font-weight: bold;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 0px 2px 0px 3px;
            height: 20px;
            vertical-align: middle;
        }
        
        .tdrow
        {
            font-size: 11px;
            text-align: left;
            padding: 2px;
            vertical-align: top;
            line-height: 16px;
        }
    </style>
    <script type="text/javascript">

        function fn_OnClose() {

        }
        function showDiv(dv) {
            if (dv) {
                $('#' + dv).show();
            }

        }
        function closeDiv(dv) {
            if (dv) {
                $('#' + dv).hide();
            }

        }
        function checkDelte() {
            if ('<%=ViewState["del"].ToString()%>' == "0") {
                alert('You dont have Delete rights for the Item List!');
                return false;

            }
            else {
                return confirm('Are you sure want to delete this record?');

            }
        }
           

             
       

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Item List
    </div>
    <div class="page-content-main">
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 40%; top: 30px; z-index: 2;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td align="left">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblItemName" Text="Item Name :" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSearchItemName" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlItemType" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            Width="100px" Height="20px" Font-Size="11px" BackColor="#FFFFCC">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearchItem" runat="server" Text="Search" OnClick="btnSearchItem_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnClear" runat="server" Text="Clear Filter" OnClick="btnClear_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnNewItem" runat="server" Text="Add New Item" Font-Names="Tahoma"
                                Height="24px" OnClick="btnNewItem_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvTrainingItems" AutoGenerateColumns="false" runat="server" Width="100%"
                                CssClass="gridmain-css" CellPadding="4" CellSpacing="0" EmptyDataText="No Records Found !!"
                                GridLines="None">
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-Width="250px">
                                        <ItemTemplate>
                                            <asp:Image ImageUrl='<%# "../Images/DocTree/"+(Eval("ITEM_TYPE").ToString()!="FBM"? (System.IO.File.Exists(Server.MapPath(@"~/Images/DocTree/"+System.IO.Path.GetExtension(Eval("ITEM_PATH").ToString()).Replace(".", "")+".png"))?(System.IO.Path.GetExtension(Eval("ITEM_PATH").ToString()).Replace(".", "")+".png"):"noneimg.png") : ("noneimg.png")) %>'
                                                ID="imgFile" runat="server" />
                                            <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("ITEM_NAME_SHORT") %>' onmouseover='<%# "js_ShowToolTip(&#39;"+Eval("ITEM_NAME").ToString()+"&#39;,event,this)" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Description" ItemStyle-Width="350px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemDescription" runat="server" Text='<%#Eval("ITEM_Description_SHORT") %>'
                                                onmouseover='<%# "js_ShowToolTip(&#39;"+Eval("ITEM_Description").ToString()+"&#39;,event,this)" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ITEM_TYPE" ItemStyle-Width="100px" HeaderText="Item Type" />
                                    <asp:BoundField DataField="DURATION" HeaderText="Duration(in Minutes)" ItemStyle-Width="80px"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Attachment" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlAttachmentDetails" Text='<%#Eval("ITEM_TYPE").ToString()!="FBM"?Eval("Attachment_Name"):Eval("Item_Name") %>'
                                                runat="server" NavigateUrl='<%#    Eval("ITEM_TYPE").ToString()!="FBM"?(System.IO.File.Exists(Server.MapPath("~/Uploads/TrainingItems/"+Eval("ITEM_PATH").ToString()))?"~/Uploads/TrainingItems/"+Eval("ITEM_PATH").ToString():"~/FileNotFound.aspx"):("../QMS/FBM/"+Eval("ITEM_PATH").ToString())    %>'
                                                Target="_blank"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="1" cellspacing="0">
                                                <tr align="center">
                                                    <td style="border-color: transparent">
                                                        <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete" Visible='<%# Eval("Item_Status").ToString()=="0"?true:false%>'
                                                            OnClientClick="return checkDelte();" CommandArgument='<%#Eval("ID") %>' ForeColor="Black"
                                                            ToolTip="Remove Item" ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                    </td>
                                                     
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true" HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" Width="20px" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <auc:CustomPager ID="ucCustomPagerAllStatus" OnBindDataItem="BindTrainingItems" AlwaysGetRecordsCount="true"
                                runat="server" />
                        </td>
                    </tr>
                </table>
                <div id="divUrl" title="Item Detials - Associated Page" style="display: none; border: 1px solid Gray;
                    font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 20%;">
                    <table width="100%" cellpadding="2" cellspacing="2" style="white-space:nowrap">
                        <tr>
                            <td align="right" style="width: 16%" valign="top">
                                Associated Page &nbsp;:&nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right" valign="top">
                                &nbsp;
                            </td>
                            
                            <td align="left" style="width: 50%">
                                <asp:DropDownList ID="ddlMenuLink" Width="150px" runat="server">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hfItemId" runat="server" />
                            </td>
                            </tr><tr>
                            <td style="color: #FF0000; width: 1%" align="right" valign="top">
                                &nbsp;
                            </td>
                            <td style="color: #FF0000; width: 1%" align="right" valign="top">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 34%">
                                <asp:Button ID="btnSave" Text="Save" Width="150px" runat="server" OnClick="btnSave_Click">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="divMessage" align="center">
            <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
        </div>
        <br />
    </div>
</asp:Content>
