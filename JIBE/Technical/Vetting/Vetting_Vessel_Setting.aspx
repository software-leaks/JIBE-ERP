<%@ Page Language="C#"  AutoEventWireup="true"
    CodeFile="Vetting_Vessel_Setting.aspx.cs" Inherits="Technical_Vetting_Vetting_VesselVettingSetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
   <style type="text/css">
        body {  
   
        font-family: Tahoma;
        font-size: 12px;
        margin: 0;
        padding: 0;
       }
   </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divLoggout" runat="server" style="color: red; font-size: 14px; text-align: center;">
                Session expired!! Please log out and login again
            </div>
       <div align="center" id="MainContent" runat="server">
     <div id="AccessMsgDiv" runat="server" style="color: Red; font-size: 14px; text-align: center;">
            You don't have sufficient privilege to access the requested page.</div>
   <div id="MainDiv" runat="server">
    <asp:UpdatePanel ID="UpdUserType" runat="server">
        <ContentTemplate>
            <div align="center">
                
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div style="padding-top: 5px; padding-bottom: 5px; height: 40px; width: 1000px">
                                <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFilter">
                                    <table cellpadding="2" cellspacing="4" style="float: left; width:100%;">
                                        <tr>
                                            <td align="right" style="width: 10%">
                                                Vessel Name :&nbsp;
                                            </td>
                                            <td align="left" style="width:40%">
                                                <asp:TextBox ID="txtfilter" runat="server" Width="100%" Height="18px"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TBWE2" runat="server" TargetControlID="txtfilter"
                                                    WatermarkText="Type to Search" WatermarkCssClass="watermarked" />
                                            </td>
                                            <td align="center" style="width: 1%">
                                                <asp:ImageButton ID="btnFilter" runat="server" TabIndex="0" OnClick="btnFilter_Click"
                                                    ToolTip="Search" ImageUrl="~/Images/SearchButton.png" />
                                            </td>
                                            <td align="center" style="width: 1%">
                                                <asp:ImageButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                                    ImageUrl="~/Images/Refresh-icon.png" />
                                            </td>
                                             <td style="width: 1%">
                                               
                                                <asp:ImageButton ID="btnSave" runat="server" ToolTip="Save" OnClick="btnSave_Click"
                                                    ImageUrl="~/Images/Save-icon.png" />
                                            </td>
                                            <td style="width: 1%">
                                                <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                                    ImageUrl="~/Images/Exptoexcel.png" />
                                            </td>
                                           
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                            <div align="center" style="width: 1000px; ">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvVslVtngStng" runat="server" EmptyDataText="NO RECORDS FOUND"
                                            CellPadding="2" CellSpacing="2" Width="100%" AllowSorting="true" CssClass="gridmain-css" AllowPaging="false" 
                                            AutoGenerateColumns="false" OnRowDataBound="gvVslVtngStng_RowDataBound" ShowHeaderWhenEmpty="true"
                                            onsorting="gvVslVtngStng_Sorting" >
                                            <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <PagerStyle CssClass="PMSPagerStyle-css" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Vessel Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVesselId" runat="server" Text='<%#Eval("Vessel_ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vessel Name">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lblReasonHeader" Style="margin-left: 2px; text-decoration:none;" runat="server" CommandName="Sort"
                                                            CommandArgument="Vessel_Name" ForeColor="Black">Vessel Name</asp:LinkButton>
                                                            <img id="Vessel_Name" runat="server" visible="false" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vetting Type">
                                                    <ItemTemplate>
                                                        <asp:CheckBoxList ID="chkVTType" runat="server" RepeatDirection="Horizontal" RepeatColumns="10" RepeatLayout="Table">
                                                        </asp:CheckBoxList>
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                                    </ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                        </asp:GridView>
                                       
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
               
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ImgExpExcel" />
        </Triggers>
    </asp:UpdatePanel>
    </div>
    </div>
</form>
</body>
</html>
