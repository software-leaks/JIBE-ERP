<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="InspectionWorklistReport.aspx.cs" Inherits="Technical_Worklist_InspectionWorklistReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
  <style type="text/css">
        .page
        {
            width: 1440px;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        .Newdropdown-css
        {
            font-size: 11px;
            font-family: Tahoma;
            background-color: White;
            text-align: center;
        }
        .LinkButton
        {
            text-decoration: none;
        }
        .LinkButton:hover
        {
            text-decoration: underline;
        }
        .Rating-FooterStyle-css
        {
            /* background: url(../../Images/gridheaderbg-silver-image.png) left -0px repeat-x;*/
            color: #333333;
            background-color: #fff;
            font-weight: bold;
            font-size: 11px;
            border: 1px solid #000;
        }
        .Rating-FooterStyle-css td
        {
            border: 1px solid #000;
            border-collapse: collapse;
        }
        .Rating-HeaderStyle-css
        {
            /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #333333;
            font-size: 11px;
            padding: 5px;
            text-align: left;
            vertical-align: middle;
            background-color: #CCCCCC;
            border: 1px solid #000;
            border-collapse: collapse;
        }
        .Rating-HeaderStyle-css th
        {
            border: 1px solid #000;
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; white-space: nowrap;">
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; height: 100%;">
            <div id="page-header" class="page-title">
                <b>Worklist Report</b>
            </div>
            </div>
<table style="width:100%;" border=""1">
<tr>
<td style="width:10%;" align="center">
    <asp:Label ID="lblVesselName" runat="server" Text="VesselName" Font-Bold="true" Font-Size="16px" Font-Names="Tahoma" ></asp:Label>
</td>
<td  style="width:70%;" align="center">
    <asp:Label ID="lblPortName" runat="server" Text="Port Name" Font-Bold="true" Font-Size="16px" Font-Names="Tahoma" ></asp:Label>
</td>
<td>
    <asp:Label ID="lblFrom" runat="server" Text="From" Font-Bold="true" Font-Size="16px" Font-Names="Tahoma" ></asp:Label>:
</td>
<td>
    <asp:Label ID="lblFromDate" runat="server" Text="FromDate" Font-Bold="true" Font-Size="16px" Font-Names="Tahoma" ></asp:Label>
</td>
</tr>
<tr>
<td  >
   &nbsp;
</td>
<td>
   &nbsp;
</td>
<td>
    <asp:Label ID="Label1" runat="server" Text="To" Font-Bold="true" Font-Size="16px" Font-Names="Tahoma" ></asp:Label>:
</td>
<td >
    <asp:Label ID="lblToDate" runat="server" Text="ToDate" Font-Bold="true" Font-Size="16px" Font-Names="Tahoma" ></asp:Label>
</td>
</tr>
<tr>
<td colspan="4">

<div id="dvWorklist">

<div id="dvReport" runat="server">


</div>

<br />

 <%--<asp:UpdatePanel ID="updCat" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="dvCategory">
                                        <asp:GridView ID="grdWorklistReport" GridLines="Vertical" CellPadding="8" runat="server"
                                            ShowFooter="False" AutoGenerateColumns="false"
                                            BorderWidth="1px" Width="100%" >
                                            <HeaderStyle CssClass="Rating-HeaderStyle-css" />
                                            <FooterStyle CssClass="Rating-FooterStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" BorderStyle="Solid" BorderColor="#000" BorderWidth="1px" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <SelectedRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Sr.No." DataField="RNO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" />
                                                <asp:BoundField HeaderText="Worklist ID" DataField="WORKLISTID" ItemStyle-Width="50px"  ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Description" DataField="Description" ItemStyle-Width="300px" />
                                                <asp:BoundField HeaderText="Followed By" DataField="FollowedBy" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" />
                                                <asp:BoundField HeaderText="Priority" DataField="PRIORITY" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" />
                                                <asp:BoundField HeaderText="Target Date" DataField="TargetDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px" />
                                                <asp:BoundField HeaderText="Status" DataField="Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No Records Found
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>--%>



<div style="border:1px solid black;" runat="server" id="tblFoot" >
    <table width="100%">
        <tr>
            <td width="100px">
            </td>
            <td style="width: 200px;font-size:12px; font-family:Tahoma; font-weight:bold;" align="center">
                Master
            </td>
            <td style="font-size:12px; font-family:Tahoma; font-weight:bold;" align="center">
                Chief Engineer
            </td>
            <td style="width: 200px;font-size:12px; font-family:Tahoma; font-weight:bold;" align="center">
                Inspector
            </td>
        </tr>
        <tr>
            <td style="font-size:12px; font-family:Tahoma; font-weight:bold;">
                Name:
            </td>
            <td  align="center">
               
            </td>
            <td  align="center">
            </td>
            <td  align="center">
             <asp:Label ID="lblInspectorName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="font-size:12px; font-family:Tahoma; font-weight:bold;">
                Signature:
            </td>
            <td  align="center">
            </td>
            <td  align="center">
            </td>
            <td  align="center">
            </td>
        </tr>
        <tr>
            <td style="width: 100px;font-size:12px; font-family:Tahoma; font-weight:bold;">
                Date:
            </td>
            <td  align="center">
                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
            </td>
            <td  align="center">
            </td>
            <td  align="center">
            </td>
        </tr>
    </table>

</div>
</div>

</td>

</tr>

</table>

</div>

</asp:Content>

