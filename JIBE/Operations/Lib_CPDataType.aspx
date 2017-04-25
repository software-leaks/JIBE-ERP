<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Lib_CPDataType.aspx.cs" Inherits="Operation_Lib_CPDataType" Title="CP Data Type Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="headcontent" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />

    

    <style type="text/css">
        #pageTitle
        {
            background-color: gray;
            color: White;
            font-size: 12px;
            text-align: center;
            padding: 2px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="contentmain" ContentPlaceHolderID="MainContent" runat="server">
    
    
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
                    <ProgressTemplate>
                        <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 2;
                            color: black">
                            <img src="../Images/loaderbar.gif" alt="Please Wait" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
    <div id="pageTitle">
        <asp:Label ID="lblPageTitle" runat="server" Text="CP DataType"></asp:Label>
    </div>
    <asp:LinkButton ID="lbtnaddentry" runat="server">Add CP DataType</asp:LinkButton>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>
                <div style="color: #333333">
                    
                    <div id="innerData">
                        <asp:GridView ID="gvCPDataType" runat="server" EmptyDataText="No record found !" AutoGenerateColumns="False"
                            DataSourceID="ObjectDataSourceCPDataType" AllowPaging="True" PageSize="15" CellPadding="4"
                            AllowSorting="True" >
                            <HeaderStyle CssClass="HeaderStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" BackColor="White" />
                            <PagerSettings Mode="NumericFirstLast" />
                            <RowStyle CssClass="RowStyle-css" />
                            <PagerStyle CssClass="PagerStyle-css" />
                            <PagerStyle Font-Size="Large" CssClass="pager" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="id" Visible="false" />
                                <asp:BoundField DataField="Datatype" HeaderText="Data Type" HeaderStyle-Width="90px"
                                    ItemStyle-HorizontalAlign="Left" SortExpression="datatype">
                                    <HeaderStyle Width="90px" ForeColor="White" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="data_code" HeaderText="Data Code" HeaderStyle-Width="70px"
                                    SortExpression="data_code">
                                    <HeaderStyle Width="70px" ForeColor="White" />
                                </asp:BoundField>
                                
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ObjectDataSourceCPDataType" TypeName="SMS.Business.Operation.BLL_OPS_VoyageReports"
        SelectMethod="OPS_Get_CPDtaType" runat="server">
        
    </asp:ObjectDataSource>
    <div id="dvCPData" style="height: 170px; width: 300px; background-color: Gray" runat="server">
        <div id="dvdrag" style="width: 300px; height: 30px; background-color: #7795BD; vertical-align: bottom;
            font-weight: bold; text-align: right; color: White" runat="server">
            <div style="float: left; vertical-align: bottom; height:25px;padding-top:5px;color:Black">
                CP DataType</div>
            <div style="float: right">
                <asp:ImageButton ID="imgbtnCancel" Height="29px" Width="30px" ImageUrl="~/Images/close.gif" ToolTip="Close" AlternateText="Close"
                    runat="server" />
            </div>
        </div>
        <div style="text-align: left">
            <asp:UpdatePanel runat="server" ID="updadd">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td style="color: White">
                                Data Type:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataType" CssClass="textbox-css" Width="145px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: White">
                                Data Short Code:
                            </td>
                            <td>
                               <asp:TextBox ID="txtDataCode" CssClass="textbox-css" Width="145px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        
                        
                        <tr>
                            <td>
                            </td>
                            <td align="left">
                                <asp:Button ID="btnsave" runat="server" Height="30px" Width="70px" OnClick="btnsave_Click"
                                    Text="Save" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" PopupDragHandleControlID="dvdrag"
        CancelControlID="imgbtnCancel" TargetControlID="lbtnaddentry" PopupControlID="dvCPData"
        runat="server">
    </cc1:ModalPopupExtender>
</asp:Content>

